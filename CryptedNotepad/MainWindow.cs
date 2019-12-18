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
        public Encryption Encryption = new Encryption();
        string FilePath;
        string Password;
        public MainWindow(string path)
        {
            InitializeComponent();

            FileAssociation.Associate("Crypted txt", Assembly.GetExecutingAssembly().Location);
            FileAssociation.AddToContextMenuNew();

            //All events are here
            richTextBox.TextChanged += (s, e) =>
            {
                lbl_status.Text = $"Total chars: {richTextBox.Text.Length}";
            };
            tool_new.Click += new System.EventHandler(tool_new_Click);
            tool_open.Click += new EventHandler(tool_open_Click);
            tool_save.Click += new EventHandler(tool_save_Click);
            tool_saveAs.Click += new System.EventHandler(tool_saveAs_Click);
            tool_fontSettings.Click += new EventHandler(tool_fontSettings_Click);
            tool_replace.Click += new EventHandler(tool_replace_Click);
            tool_about.Click += new EventHandler(tool_about_Click);
            tool_exit.Click += new System.EventHandler(tool_exit_Click);
            Shown += (s, e) =>
            {
                //update ui
                #region
                new Task(() =>
                {
                    while (true)
                    {
                        try
                        {
                            this.Invoke(new MethodInvoker(() =>
                            {
                                this.Text = Path.GetFileName(FilePath);
                                try
                                {
                                    progressBar.Maximum = Encryption.MaxValueProgress;
                                    progressBar.Value = Encryption.ValueProgress;
                                }
                                catch { }
                            }));
                            Thread.Sleep(250);
                        }
                        catch { }
                    }
                }).Start();
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
                Filter = "Crypted txt (*.ctxt)|*.ctxt|Text files (*.txt)|*.txt"
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
                    Filter = "Crypted txt (*.ctxt)|*.ctxt"
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
                Filter = "Crypted txt (*.ctxt)|*.ctxt"
            };
            if (sfd.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            FilePath = sfd.FileName;
            Thread thread = new Thread(() => SaveFile(FilePath));
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
                Text = "Find string",
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
                Text = "Find"
            });
            findForm.Controls["txtbx"].KeyDown += (s, ee) =>
            {
                if (ee.KeyCode == Keys.Enter)
                {
                    (findForm.Controls["btn_find"] as Button).PerformClick();
                }
            };
            findForm.Controls.Add(new ProgressBar()
            {
                Name = "progressBar",
                Dock = DockStyle.Bottom
            });
            findForm.Controls["btn_find"].Click += (s, ee) =>
            {
                FindText(findForm.Controls["txtbx"].Text, findForm.Controls["progressBar"] as ProgressBar);
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
                Text = "Replace string",
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
                Text = "Replace"
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
            MessageBox.Show("Free open source software by diademoff\n" +
                            "github.com/diademoff");
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
                Text = "Enter password",
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
                Text = "Apply"
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
                    MessageBox.Show("Passwords are not equal", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                Text = "Enter password",
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
                Text = "Apply"
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
        void FindText(string textToFind, ProgressBar progressBar)
        {
            int len = this.richTextBox.TextLength;
            int index = 0;
            int lastIndex = this.richTextBox.Text.LastIndexOf(textToFind);
            progressBar.Maximum = lastIndex + 1;
            while (index < lastIndex)
            {
                this.richTextBox.Find(textToFind, index, len, RichTextBoxFinds.None);
                this.richTextBox.SelectionBackColor = Color.Yellow;
                index = this.richTextBox.Text.IndexOf(textToFind, index) + 1;
                progressBar.Value = index;
            }
        }
        void UnlockProgram()
        {
            this.Invoke(new MethodInvoker(() =>
            {
                tool_File.Enabled = true;
                tool_edit.Enabled = true;
                tool_info.Enabled = true;
            }));
        }
        void LockProgram()
        {
            this.Invoke(new MethodInvoker(() =>
            {
                tool_File.Enabled = false;
                tool_edit.Enabled = false;
                tool_info.Enabled = false;
            }));
        }
        void LoadFile(string filePath)
        {
            if (Path.GetExtension(filePath) == ".txt")
            {
                #region simple load txt
                richTextBox.Text = File.ReadAllText(filePath, GetEncoding(filePath));
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
                    new Task(() =>
                    {
                        try
                        {
                            richTextBox.Invoke(new MethodInvoker(() =>
                            {
                                try
                                {
                                    string text = Encryption.DecryptString(dataFile, Password);
                                    richTextBox.Text = text;
                                    FilePath = filePath;
                                }
                                catch { MessageBox.Show("Access denied"); }
                            }));
                            UnlockProgram();
                        }
                        catch { }
                    }).Start();
                    #endregion
                }
                catch { MessageBox.Show("An error occurred while decrypting file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }
        void SaveFile(string savePath)
        {
            LockProgram();
            new Task(() =>
            {
                richTextBox.Invoke(new MethodInvoker(() =>
                {
                    File.WriteAllBytes(savePath, Encryption.EncryptString(richTextBox.Text, Password));
                    MessageBox.Show("File saved", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }));
                UnlockProgram();
            }).Start();
        }
    }
}