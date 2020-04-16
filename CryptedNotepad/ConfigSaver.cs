using Microsoft.Win32;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CryptedNotepad
{
    public class ConfigSaver
    {
        public Font Font { get; set; }
        public Size FormSize { get; set; }
        public bool IsAssociated { get; set; }

        public ConfigSaver()
        {
            try
            {
                RegistryKey reg = Registry.CurrentUser.CreateSubKey("Software\\ctxt", true);
                FormSize = new Size((int)reg.GetValue("SizeX"), (int)reg.GetValue("SizeY"));
                Font = ByteToFont(reg.GetValue("Font") as byte[]);
                IsAssociated = (int)reg.GetValue("IsAssociated") == 1 ? true : false;
                reg.Flush();
                reg.Close();
            }
            catch
            {
                FormSize = new Size(815, 485);
                Font = new Font("Arial", 12);
                IsAssociated = false;
            }
        }

        public void Save()
        {
            try
            {
                RegistryKey reg = Registry.CurrentUser.CreateSubKey("Software\\ctxt", true);
                reg.SetValue("SizeX", FormSize.Width);
                reg.SetValue("SizeY", FormSize.Height);
                reg.SetValue("Font", FontToByte(Font));
                reg.SetValue("IsAssociated", IsAssociated ? 1 : 0);
                reg.Flush();
                reg.Close();
            }
            catch { }
        }

        public void DeleteRegistryKey()
        {
            Registry.CurrentUser.DeleteSubKeyTree("Software\\ctxt");
        }

        private byte[] FontToByte(Font obj)
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

        private Font ByteToFont(byte[] arrBytes)
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            object obj = binForm.Deserialize(memStream);

            return obj as Font;
        }
    }
}
