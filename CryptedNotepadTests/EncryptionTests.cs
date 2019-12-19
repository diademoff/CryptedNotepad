﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace CryptedNotepad.Tests
{
    [TestClass()]
    public class EncryptionTests
    {
        [TestMethod()]
        public void EncryptionStringTest()
        {
            Encryption encryption = new Encryption();

            string key = RandomString(rnd.Next(5, 16));
            string clear_str = RandomString(rnd.Next(100, 500));
            byte[] enc = encryption.EncryptString(clear_str, key);
            string dec = encryption.DecryptString(enc, key);

            Assert.AreEqual(clear_str, dec);
        }

        Random rnd = new Random();
        string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[rnd.Next(s.Length)]).ToArray());
        }
    }
}