using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Media;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace TCS
{
    public partial class Form3 : Form
    {
        [DllImport("winmm.dll", EntryPoint = "mciSendStringA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern int mciSendString(string lpstrCommand, string lpstrReturnString, int uReturnLength, int hwndCallback);
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mciSendString("open new Type waveaudio Alias recsound", "", 0, 0);
            mciSendString("record recsound", "", 0, 0);
            button2.Click += new EventHandler(this.button2_Click);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            int i = random.Next(1, 1000000);
            mciSendString("save recsound c:\\TCS_Saves\\Voice_Record\\" + i + ".wav", "", 0, 0);
            mciSendString("close recsound", "", 0, 0);

            System.Diagnostics.Process.Start("c:\\TCS_Saves\\Voice_Record");

            Close();

        }
    }
}
