using EO.WebBrowser;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TCS.Properties;
using static System.Net.WebRequestMethods;

namespace TCS
{

    public partial class Form1 : Form
    {
        int i = 0;
        string last_link;
        int mic;
        public Form1()
        {
            InitializeComponent();

            
            pictureBox9.Visible = false;
            last_link = Settings.Default["ll"].ToString();
            mic = (int)Settings.Default["mic"];
            checkBox1.Checked = (bool)Settings.Default["Windows"];

        }



        private void Form1_Load(object sender, EventArgs e)
        {
            string url = last_link;
            webView1.Url = url;
            panel2.Visible = false;
            panel3.Visible = false;

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            pictureBox1.Visible = false;
            pictureBox9.Visible = true;
            webControl1.Location = new Point(0, 28);
            webControl1.Size = new Size(1182, 631);
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            pictureBox1.Visible = true;
            pictureBox9.Visible = false;
            webControl1.Location = new Point(0, 43);
            webControl1.Size = new Size(1185, 616);
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel4.Visible = false;
            pictureBox9.Visible = false;
            panel2.Visible = true;
            webControl1.Dock = DockStyle.Fill;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            string url = "https://discord.com/login";
            webView1.Url = url;
            Settings.Default["ll"] = "https://discord.com/login";
            Settings.Default.Save();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            string url = "https://teams.live.com/_";
            webView1.Url = url;
            Settings.Default["ll"] = "https://teams.live.com/_";
            Settings.Default.Save();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            string url = "https://web.whatsapp.com/";
            webView1.Url = url;
            Settings.Default["ll"] = "https://web.whatsapp.com/";
            Settings.Default.Save();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            string url = "https://www.instagram.com/direct/inbox/";
            webView1.Url = url;
            Settings.Default["ll"] = "https://www.instagram.com/direct/inbox/";
            Settings.Default.Save();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.D)
            {
                pictureBox2_Click(sender: null, e: null);
            }

            if (e.Control && e.KeyCode == Keys.T)
            {
                pictureBox3_Click(sender: null, e: null);
            }

            if (e.Control && e.KeyCode == Keys.W)
            {
                pictureBox4_Click(sender: null, e: null);
            }

            if (e.Control && e.KeyCode == Keys.I)
            {
                pictureBox5_Click(sender: null, e: null);
            }
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            if (mic == 0)
            {
                string newPath = "c:\\TCS_Saves\\Voice_Record\\";
                System.IO.Directory.CreateDirectory(newPath);
                Settings.Default["mic"] = 1;
                Settings.Default.Save();
            }

            if (mic == 1)
            {
                Form3 form3 = new Form3();
                form3.Show();
            }

        }

        private void webView1_RequestPermissions(object sender, EO.WebBrowser.RequestPermissionEventArgs e)
        {
            if (e.Permissions == Permissions.Microphone)
            {

                e.Allow();

            }

            if (e.Permissions == Permissions.WebCam)
            {
                e.Allow();
            }
            if (e.Permissions == Permissions.DesktopVideoCapture)
            {

                e.Allow();
            }
        }

        private void webView1_NewWindow(object sender, NewWindowEventArgs e)
        {
            System.Diagnostics.Process.Start(e.TargetUrl);
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            pictureBox7_Click(sender, e);
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            pictureBox2_Click(sender, e);
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            pictureBox3_Click(sender, e);
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            pictureBox4_Click(sender, e);
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            pictureBox5_Click(sender, e);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == false)
            {
                RegistryKey reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                reg.DeleteValue("TNBS", true);

                Settings.Default["Windows"] = checkBox1.Checked = false;
                Settings.Default.Save();


            }

            if (checkBox1.Checked == true)
            {
                RegistryKey reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                reg.SetValue("TNBS", Application.ExecutablePath.ToString());

                Settings.Default["Windows"] = checkBox1.Checked = true;
                Settings.Default.Save();
            }
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            panel3.Visible = true;
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            pictureBox8_Click(sender, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Shortcut(linkName: null);
        }
        private void Shortcut(string linkName)
        {
            string deskDir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            using (StreamWriter writer = new StreamWriter(deskDir + "\\" + linkName + "TCS.url"))
            {
                string app = System.Reflection.Assembly.GetExecutingAssembly().Location;
                writer.WriteLine("[InternetShortcut]");
                writer.WriteLine("URL=file:///" + app);
                writer.WriteLine("%SystemRoot%\\System32\\SHELL32.dll");
                string icon = app.Replace('\\', '/');
                writer.WriteLine("IconFile=" + icon);
            }
        }
    }
}
