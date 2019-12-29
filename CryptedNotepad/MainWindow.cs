using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptedNotepad
{
    public partial class MainWindow : Form
    {
        ConfigSaver ConfigSaver;
        public Encryption Encryption = new Encryption();
        string FilePath;
        string Password;
        bool saved = true;
        public static int MaxValueProgress = 0;
        public static int ValueProgress = 0;
        public MainWindow(string path)
        {
            InitializeComponent();

            FileAssociation.Associate($"{LocalStrings.Description}", Assembly.GetExecutingAssembly().Location);
            FileAssociation.AddToContextMenuNew();

            ConfigSaver = new ConfigSaver();
            this.Width = ConfigSaver.FormSize.Width;
            this.Height = ConfigSaver.FormSize.Height;
            richTextBox.Font = ConfigSaver.Font;

            //All events are here
            richTextBox.TextChanged += (s, e) =>
            {
                saved = false;
                lbl_status.Text = $"{LocalStrings.TotalChars}: {richTextBox.Text.Length}";
            };
            tool_new.Click += new System.EventHandler(tool_new_Click);
            tool_open.Click += new EventHandler(tool_open_Click);
            tool_save.Click += new EventHandler(tool_save_Click);
            tool_saveAs.Click += new System.EventHandler(tool_saveAs_Click);
            tool_fontSettings.Click += new EventHandler(tool_fontSettings_Click);
            tool_replace.Click += new EventHandler(tool_replace_Click);
            tool_about.Click += new EventHandler(tool_about_Click);
            tool_exit.Click += new System.EventHandler(tool_exit_Click);
            tool_deleteProgram.Click += new System.EventHandler(tool_deleteProgram_Click);
            Shown += (s, e) =>
            {
                //update ui
                #region
                new Thread(() =>
                {
                    while (true)
                    {
                        try
                        {
                            this.Invoke(new MethodInvoker(() =>
                            {
                                string t = (saved) ? "" : "*";
                                this.Text = Path.GetFileName(FilePath) + t;
                                try
                                {
                                    progressBar.Maximum = MaxValueProgress;
                                    progressBar.Value = ValueProgress;
                                }
                                catch { }
                            }));
                            Thread.Sleep(250);
                        }
                        catch { }
                    }
                })
                { IsBackground = true }.Start();
                #endregion
                if (path == "")
                {
                    return;
                }
                LoadFile(path);
            };
        }

        void tool_open_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = $"{LocalStrings.Crypted_txt} (*.ctxt)|*.ctxt|{LocalStrings.Text_files} (*.txt)|*.txt"
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                LoadFile(ofd.FileName);
            }
        }
        void tool_save_Click(object sender, EventArgs e)
        {
            string savePath = "";
            if (FilePath != null)
            {
                savePath = FilePath;
            }
            else
            {
                SaveFileDialog sfd = new SaveFileDialog
                {
                    Filter = $"{LocalStrings.Crypted_txt} (*.ctxt)|*.ctxt"
                };
                if (sfd.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                FilePath = sfd.FileName;
                savePath = sfd.FileName;
            }
            if (Password == null)
            {
                string password;
                try
                {
                    password = AskDoublePassword();
                    Password = password;
                }
                catch { return; }
            }

            SaveFile(savePath);
        }
        void tool_saveAs_Click(object sender, EventArgs e)
        {
            try
            {
                Password = AskDoublePassword();
            }
            catch { return; }
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = $"{LocalStrings.Crypted_txt} (*.ctxt)|*.ctxt"
            };
            if (sfd.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            FilePath = sfd.FileName;
            Thread thread = new Thread(() => SaveFile(FilePath)) { IsBackground = true };
            thread.Start();
        }
        void tool_fontSettings_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            if (fd.ShowDialog() == DialogResult.OK)
            {
                richTextBox.Font = fd.Font;
            }
        }
        void tool_find_Click(object sender, EventArgs e)
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            Form findForm = new Form()
            {
                AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font,
                ClientSize = new System.Drawing.Size(400, 225),
                Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon"))),
                Text = $"{LocalStrings.Find_string}",
                MaximizeBox = false,
                MaximumSize = new System.Drawing.Size(400, 225),
                MinimumSize = new System.Drawing.Size(400, 225),
                StartPosition = FormStartPosition.CenterScreen
            };
            findForm.Controls.Add(new TextBox()
            {
                Name = "txtbx",
                Location = new Point(57, 50),
                Width = 285
            });
            findForm.Controls.Add(new Button()
            {
                Name = "btn_find",
                Location = new Point(77, 150),
                Width = 245,
                Text = $"{LocalStrings.Find}"
            });
            findForm.Controls["txtbx"].KeyDown += (s, ee) =>
            {
                if (ee.KeyCode == Keys.Enter)
                {
                    (findForm.Controls["btn_find"] as Button).PerformClick();
                }
            };
            findForm.Controls["btn_find"].Click += (s, ee) =>
            {
                new Thread(() => FindText(findForm.Controls["txtbx"].Text)) { IsBackground = true }.Start();
            };
            findForm.Show();
            findForm.FormClosing += (s, ee) =>
            {
                int temp = richTextBox.SelectionStart;
                richTextBox.SelectAll();
                richTextBox.SelectionBackColor = Color.White;
                richTextBox.SelectionStart = temp;
            };
        }
        void tool_replace_Click(object sender, EventArgs e)
        {
            #region
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            Form askPassForm = new Form()
            {
                AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font,
                ClientSize = new System.Drawing.Size(400, 225),
                Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon"))),
                Text = $"{LocalStrings.Replace_string}",
                MaximizeBox = false,
                MaximumSize = new System.Drawing.Size(400, 225),
                MinimumSize = new System.Drawing.Size(400, 225),
                StartPosition = FormStartPosition.CenterScreen
            };

            askPassForm.Controls.Add(new TextBox()
            {
                Name = "txtbx1",
                Location = new Point(57, 50),
                Width = 285
            });
            askPassForm.Controls["txtbx1"].KeyDown += (s, ee) =>
            {
                if (ee.KeyCode == Keys.Enter)
                {
                    askPassForm.Controls["txtbx2"].Focus();
                }
            };
            askPassForm.Controls.Add(new TextBox()
            {
                Name = "txtbx2",
                Location = new Point(57, 100),
                Width = 285
            });
            askPassForm.Controls.Add(new Button()
            {
                Name = "btn_replace",
                Location = new Point(77, 150),
                Width = 245,
                Text = $"{LocalStrings.Replace}"
            });
            askPassForm.Controls["btn_replace"].Click += (s, ee) =>
            {
                askPassForm.DialogResult = DialogResult.OK;
                askPassForm.Close();
            };
            askPassForm.Controls["txtbx2"].KeyDown += (s, ee) =>
            {
                if (ee.KeyCode == Keys.Enter)
                {
                    (askPassForm.Controls["btn_replace"] as Button).PerformClick();
                }
            };
            if (askPassForm.ShowDialog() == DialogResult.OK)
            {
                richTextBox.Text = richTextBox.Text.Replace(askPassForm.Controls["txtbx1"].Text, askPassForm.Controls["txtbx2"].Text);
            }
            #endregion
        }
        void tool_exit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
        void tool_new_Click(object sender, EventArgs e)
        {
            Process.Start(Assembly.GetExecutingAssembly().Location);
        }
        void tool_about_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"{LocalStrings.about}\n" +
                             "github.com/diademoff");
        }
        void tool_deleteProgram_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(LocalStrings.SureDelete, LocalStrings.Warning, MessageBoxButtons.YesNo) != DialogResult.Yes)
            {
                return;
            }

            ConfigSaver.DeleteRegistryKey();
            FileAssociation.Remove();
            Cmd($"taskkill /f /pid \"{Process.GetCurrentProcess().Id}\" &" +
                $"del /f \"{Assembly.GetExecutingAssembly().Location}\"");
        }

        /// <summary>
        /// Get encoding of file
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        Encoding GetEncoding(string srcFile)
        {
            #region
            // *** Use Default of Encoding.Default (Ansi CodePage)
            Encoding enc = Encoding.Default;

            // *** Detect byte order mark if any - otherwise assume default
            byte[] buffer = new byte[10];
            FileStream file = new FileStream(srcFile, FileMode.Open);
            file.Read(buffer, 0, 10);
            file.Close();

            if (buffer[0] == 0xef && buffer[1] == 0xbb && buffer[2] == 0xbf)
            {
                enc = Encoding.UTF8;
            }
            else if (buffer[0] == 0xfe && buffer[1] == 0xff)
            {
                enc = Encoding.Unicode;
            }
            else if (buffer[0] == 0 && buffer[1] == 0 && buffer[2] == 0xfe && buffer[3] == 0xff)
            {
                enc = Encoding.UTF32;
            }
            else if (buffer[0] == 0x2b && buffer[1] == 0x2f && buffer[2] == 0x76)
            {
                enc = Encoding.UTF7;
            }
            else if (buffer[0] == 0xFE && buffer[1] == 0xFF)
            {
                // 1201 unicodeFFFE Unicode (Big-Endian)
                enc = Encoding.GetEncoding(1201);
            }
            else if (buffer[0] == 0xFF && buffer[1] == 0xFE)
            {
                // 1200 utf-16 Unicode
                enc = Encoding.GetEncoding(1200);
            }

            return enc;
            #endregion
        }
        string AskDoublePassword()
        {
            #region
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            Form askPassForm = new Form()
            {
                AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font,
                ClientSize = new System.Drawing.Size(400, 225),
                Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon"))),
                Text = $"{LocalStrings.Enter_password}",
                MaximizeBox = false,
                MaximumSize = new System.Drawing.Size(400, 225),
                MinimumSize = new System.Drawing.Size(400, 225),
                StartPosition = FormStartPosition.CenterScreen
            };

            askPassForm.Controls.Add(new TextBox()
            {
                Name = "txtbx1",
                Location = new Point(57, 50),
                Width = 285,
                PasswordChar = '•'
            });
            askPassForm.Controls["txtbx1"].KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    askPassForm.Controls["txtbx2"].Focus();
                }
            };
            askPassForm.Controls.Add(new TextBox()
            {
                Name = "txtbx2",
                Location = new Point(57, 100),
                Width = 285,
                PasswordChar = '•'
            });
            askPassForm.Controls.Add(new Button()
            {
                Name = "btn_apply",
                Location = new Point(77, 150),
                Width = 245,
                Text = $"{LocalStrings.Apply}"
            });
            askPassForm.Controls["btn_apply"].Click += (s, e) =>
            {
                if (askPassForm.Controls["txtbx1"].Text == askPassForm.Controls["txtbx2"].Text)
                {
                    askPassForm.DialogResult = DialogResult.OK;
                    askPassForm.Close();
                }
                else
                {
                    MessageBox.Show($"{LocalStrings.Passwords_are_not_equal}", $"{LocalStrings.Warning}", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };
            askPassForm.Controls["txtbx2"].KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    (askPassForm.Controls["btn_apply"] as Button).PerformClick();
                }
            };
            if (askPassForm.ShowDialog() == DialogResult.OK)
            {
                return askPassForm.Controls["txtbx1"].Text;
            }
            else
            {
                throw new Exception("No password entered");
            }
            #endregion
        }
        string AskSinglePassword()
        {
            #region
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            Form askPassForm = new Form()
            {
                AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font,
                ClientSize = new System.Drawing.Size(400, 225),
                Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon"))),
                Text = $"{LocalStrings.Enter_password}",
                MaximizeBox = false,
                MaximumSize = new System.Drawing.Size(400, 225),
                MinimumSize = new System.Drawing.Size(400, 225),
                StartPosition = FormStartPosition.CenterScreen
            };

            askPassForm.Controls.Add(new TextBox()
            {
                Name = "txtbx1",
                Location = new Point(57, 50),
                Width = 285,
                PasswordChar = '•'
            });

            askPassForm.Controls.Add(new Button()
            {
                Name = "btn_apply",
                Location = new Point(77, 150),
                Width = 245,
                Text = $"{LocalStrings.Apply}"
            });

            askPassForm.Controls["txtbx1"].KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    (askPassForm.Controls["btn_apply"] as Button).PerformClick();
                }
            };

            askPassForm.Controls["btn_apply"].Click += (s, e) =>
            {
                askPassForm.DialogResult = DialogResult.OK;
                askPassForm.Close();
            };
            if (askPassForm.ShowDialog() == DialogResult.OK)
            {
                return askPassForm.Controls["txtbx1"].Text;
            }
            else
            {
                throw new Exception("No password entered");
            }
            #endregion
        }
        void FindText(string textToFind)
        {
            int len = -1;
            int index = -1;
            int lastIndex = -1;
            this.Invoke(new MethodInvoker(() =>
            {
                len = this.richTextBox.TextLength;
                index = 0;
                lastIndex = this.richTextBox.Text.LastIndexOf(textToFind);
            }));
            MaxValueProgress = lastIndex + 1;
            while (index < lastIndex)
            {
                this.Invoke(new MethodInvoker(() =>
                {
                    this.richTextBox.Find(textToFind, index, len, RichTextBoxFinds.None);
                    this.richTextBox.SelectionBackColor = Color.Yellow;
                    index = this.richTextBox.Text.IndexOf(textToFind, index) + 1;
                    ValueProgress = index;
                }));
            }
            MainWindow.ValueProgress = MainWindow.MaxValueProgress = 0;
        }
        void UnlockProgram()
        {
            this.Invoke(new MethodInvoker(() =>
            {
                tool_File.Enabled = true;
                tool_edit.Enabled = true;
                tool_info.Enabled = true;
                richTextBox.Enabled = true;
            }));
        }
        void LockProgram()
        {
            this.Invoke(new MethodInvoker(() =>
            {
                tool_File.Enabled = false;
                tool_edit.Enabled = false;
                tool_info.Enabled = false;
                richTextBox.Enabled = false;
            }));
        }
        void LoadFile(string filePath)
        {
            if (Path.GetExtension(filePath) == ".txt")
            {
                #region simple load txt
                richTextBox.Text = File.ReadAllText(filePath, GetEncoding(filePath));
                saved = true;
                #endregion 
            }
            else
            {

                if (new FileInfo(filePath).Length == 0)
                {
                    // if file is empty dont ask password
                    FilePath = filePath;
                    return;
                }
                #region ask password
                try
                {
                    Password = AskSinglePassword();
                }
                catch { return; }
                #endregion
                byte[] dataFile = File.ReadAllBytes(filePath);
                try
                {
                    #region decrypt file
                    LockProgram();
                    string text = "";
                    richTextBox.Invoke(new MethodInvoker(() =>
                    {
                        text = richTextBox.Text;
                    }));
                    new Thread(() =>
                    {
                        try
                        {
                            string decrypted = Encryption.DecryptString(dataFile, Password);
                            richTextBox.Invoke(new MethodInvoker(() =>
                            {
                                richTextBox.Text = decrypted;
                            }));
                            FilePath = filePath;
                            saved = true;
                            UnlockProgram();
                        }
                        catch { MessageBox.Show($"{LocalStrings.Access_denied}"); }
                    })
                    { IsBackground = true }.Start();
                    #endregion
                }
                catch { MessageBox.Show($"{LocalStrings.An_error_decrypting_file}", $"{LocalStrings.Error}", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }
        void SaveFile(string savePath)
        {
            LockProgram();
            new Thread(() =>
            {
                string text = "";
                richTextBox.Invoke(new MethodInvoker(() =>
                {
                    text = richTextBox.Text;
                }));

                var crypted = Encryption.EncryptString(text, Password);
                File.WriteAllBytes(savePath, crypted);
                MessageBox.Show($"{LocalStrings.File_saved}", $"{LocalStrings.Info}", MessageBoxButtons.OK, MessageBoxIcon.Information);
                UnlockProgram();
                saved = true;
            })
            { IsBackground = true }.Start();
        }
        void Cmd(string line)
        {
            Process.Start(
                new ProcessStartInfo
                {
                    FileName = "cmd",
                    Arguments = $"/c {line}",
                    WindowStyle = ProcessWindowStyle.Hidden
                }).WaitForExit();
        }
    }
}