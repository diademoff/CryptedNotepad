﻿using Microsoft.Win32;
using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace CryptedNotepad
{
    public static class FileAssociation
    {
        private const string FILE_EXTENSION = ".ctxt";//TODO: write extension here
        private const long SHCNE_ASSOCCHANGED = 0x8000000L;
        private const uint SHCNF_IDLIST = 0x0U;

        public static void Associate(string description, string icon)
        {
            Registry.ClassesRoot.CreateSubKey(FILE_EXTENSION).SetValue("", Application.ProductName);

            if (Application.ProductName != null && Application.ProductName.Length > 0)
            {
                using (RegistryKey key = Registry.ClassesRoot.CreateSubKey(Application.ProductName))
                {
                    if (description != null)
                    {
                        key.SetValue("", description);
                    }

                    if (icon != null)
                    {
                        key.CreateSubKey("DefaultIcon").SetValue("", ToShortPathName(icon));
                    }

                    key.CreateSubKey(@"Shell\Open\Command").SetValue("", ToShortPathName(Application.ExecutablePath) + " \"%1\"");
                }
            }

            try
            {
                SHChangeNotify(SHCNE_ASSOCCHANGED, SHCNF_IDLIST, IntPtr.Zero, IntPtr.Zero);
            }
            catch { }
        }

        public static bool IsAssociated => (Registry.ClassesRoot.OpenSubKey(FILE_EXTENSION, false) != null);

        public static void Remove()
        {
            Registry.ClassesRoot.DeleteSubKeyTree(FILE_EXTENSION);
            Registry.ClassesRoot.DeleteSubKeyTree(Application.ProductName);
        }

        [DllImport("shell32.dll", SetLastError = true)]
        private static extern void SHChangeNotify(long wEventId, uint uFlags, IntPtr dwItem1, IntPtr dwItem2);

        [DllImport("Kernel32.dll")]
        private static extern uint GetShortPathName(string lpszLongPath, [Out]StringBuilder lpszShortPath, uint cchBuffer);

        private static string ToShortPathName(string longName)
        {
            StringBuilder s = new StringBuilder(1000);
            uint iSize = (uint)s.Capacity;
            uint iRet = GetShortPathName(longName, s, iSize);
            return s.ToString();
        }
    }
}
