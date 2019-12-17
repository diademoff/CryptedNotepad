using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;

namespace CryptedNotepad
{
    public class Encryption
    {

        [Serializable]
        private class CryptElement
        {
            public char Symbol { get; private set; }
            public List<int> Indexes { get; set; }

            #region constructor
            public CryptElement(char symbol)
            {
                Symbol = symbol;
                Indexes = new List<int>();
            }
            #endregion

            #region Get hash code
            public override int GetHashCode()
            {
                return Symbol;
            }
            #endregion
        }

        public int ValueProgress { get; private set; } = 0;
        public int MaxValueProgress { get; private set; } = 0;

        public byte[] EncryptString(string input, string password)
        {
            HashSet<CryptElement> unique = new HashSet<CryptElement>();
            MaxValueProgress = input.Length;
            for (int i = 0; i < input.Length; i++)
            {
                ValueProgress = i;
                CryptElement current = new CryptElement(input[i]);
                if (unique.Contains(current))
                {
                    unique.TryGetValue(current, out CryptElement element);
                    element.Indexes.Add(i);
                }
                else
                {
                    current.Indexes.Add(i);
                    unique.Add(current);
                }
            }
            byte[] arr = ObjectToByteArray(unique);
            byte[] encrypted = EncryptByteArray(arr, password);
            encrypted = EncryptByteArray(encrypted, string.Join("", password.Reverse()));
            ValueProgress = MaxValueProgress = 0;
            return encrypted;
        }

        public string DecryptString(byte[] input, string password)
        {
            byte[] decryped = DecryptByteArray(input, string.Join("", password.Reverse()));
            decryped = DecryptByteArray(decryped, password);
            HashSet<CryptElement> unique = ByteArrayToObject(decryped) as HashSet<CryptElement>;
            MaxValueProgress = unique.Count;
            int i = 0;
            List<char> result = new List<char>();
            foreach (CryptElement chr in unique)
            {
                string symbol = chr.Symbol.ToString();
                foreach (int index in chr.Indexes)
                {
                    while (result.Count <= index)
                    {
                        result.Add('?');
                    }
                    result[index] = symbol[0];
                }
                i++;
                ValueProgress = i;
            }
            ValueProgress = MaxValueProgress = 0;
            return string.Join("", result);
        }

        byte[] EncryptByteArray(byte[] bytesToEncrypt, string password)
        {
            byte[] ivSeed = Guid.NewGuid().ToByteArray();

            var rfc = new Rfc2898DeriveBytes(password, ivSeed);
            byte[] Key = rfc.GetBytes(16);
            byte[] IV = rfc.GetBytes(16);

            byte[] encrypted;
            using (MemoryStream mstream = new MemoryStream())
            {
                using (AesCryptoServiceProvider aesProvider = new AesCryptoServiceProvider())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(mstream, aesProvider.CreateEncryptor(Key, IV), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(bytesToEncrypt, 0, bytesToEncrypt.Length);
                    }
                }
                encrypted = mstream.ToArray();
            }

            var messageLengthAs32Bits = Convert.ToInt32(bytesToEncrypt.Length);
            var messageLength = BitConverter.GetBytes(messageLengthAs32Bits);

            encrypted = encrypted.Prepend(ivSeed);
            encrypted = encrypted.Prepend(messageLength);

            return encrypted;
        }
        public static byte[] DecryptByteArray(byte[] bytesToDecrypt, string password)
        {
            (byte[] messageLengthAs32Bits, byte[] bytesWithIv) = bytesToDecrypt.Shift(4); // get the message length
            (byte[] ivSeed, byte[] encrypted) = bytesWithIv.Shift(16);                    // get the initialization vector

            var length = BitConverter.ToInt32(messageLengthAs32Bits, 0);

            var rfc = new Rfc2898DeriveBytes(password, ivSeed);
            byte[] Key = rfc.GetBytes(16);
            byte[] IV = rfc.GetBytes(16);

            byte[] decrypted;
            using (MemoryStream mStream = new MemoryStream(encrypted))
            {
                using (AesCryptoServiceProvider aesProvider = new AesCryptoServiceProvider())
                {
                    aesProvider.Padding = PaddingMode.None;
                    using (CryptoStream cryptoStream = new CryptoStream(mStream, aesProvider.CreateDecryptor(Key, IV), CryptoStreamMode.Read))
                    {
                        cryptoStream.Read(encrypted, 0, length);
                    }
                }
                decrypted = mStream.ToArray().Take(length).ToArray();
            }
            return decrypted;
        }
        byte[] PerformCryptography(byte[] data, ICryptoTransform cryptoTransform)
        {
            using (MemoryStream ms = new MemoryStream())
            using (CryptoStream cryptoStream = new CryptoStream(ms, cryptoTransform, CryptoStreamMode.Write))
            {
                cryptoStream.Write(data, 0, data.Length);
                cryptoStream.FlushFinalBlock();

                return ms.ToArray();
            }
        }


        // Convert an object to a byte array
        byte[] ObjectToByteArray(object obj)
        {
            if (obj == null)
            {
                return null;
            }

            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, obj);

            return ms.ToArray();
        }

        // Convert a byte array to an Object
        object ByteArrayToObject(byte[] arrBytes)
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            object obj = binForm.Deserialize(memStream);

            return obj;
        }
    }
}
