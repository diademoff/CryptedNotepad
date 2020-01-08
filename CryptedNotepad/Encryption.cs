using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace CryptedNotepad
{
    public class Encryption
    {
        public byte[] EncryptString(string input, string password)
        {
            string cryptoPass = sha256(password);

            byte[] result = Encoding.UTF8.GetBytes(input);
            result = result.Reverse().ToArray();
            result = EncryptByteArray(result, cryptoPass);
            result = Compress(result);
            result = result.Reverse().ToArray();
            return result;
        }
        public string DecryptString(byte[] input, string password)
        {
            string cryptoPass = sha256(password);
            input = input.Reverse().ToArray();
            input = Decompress(input);
            input = DecryptByteArray(input, cryptoPass);
            input = input.Reverse().ToArray();
            return Encoding.UTF8.GetString(input);
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
        byte[] DecryptByteArray(byte[] bytesToDecrypt, string password)
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
        byte[] Compress(byte[] data)
        {
            MemoryStream output = new MemoryStream();
            using (DeflateStream dstream = new DeflateStream(output, CompressionLevel.Optimal))
            {
                dstream.Write(data, 0, data.Length);
            }
            return output.ToArray();
        }
        byte[] Decompress(byte[] data)
        {
            MemoryStream input = new MemoryStream(data);
            MemoryStream output = new MemoryStream();
            using (DeflateStream dstream = new DeflateStream(input, CompressionMode.Decompress))
            {
                dstream.CopyTo(output);
            }
            return output.ToArray();
        }
        string sha256(string str)
        {
            var crypt = new SHA256Managed();
            var hash = new StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(str));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }
    }
}
