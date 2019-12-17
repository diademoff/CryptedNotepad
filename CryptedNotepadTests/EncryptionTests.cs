using Microsoft.VisualStudio.TestTools.UnitTesting;
using CryptedNotepad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptedNotepad.Tests
{
    [TestClass()]
    public class EncryptionTests
    {
        [TestMethod()]
        public void EncryptDecryptStringTest()
        {
            string pass = RandomString(rnd.Next(10, 400));
            string str = RandomString(rnd.Next(10, 400));
            Encryption encryption = new Encryption();
            var encrypted = encryption.EncryptString(str, pass);
            var decrypted = encryption.DecryptString(encrypted, pass);

            Assert.AreEqual(encrypted, decrypted);
        }

        
    }
}