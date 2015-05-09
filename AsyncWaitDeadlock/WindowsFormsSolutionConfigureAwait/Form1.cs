using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsyncWaitDeadlock
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void downloadButton_Click(object sender, EventArgs e)
        {
            this.textBox1.Clear();
            textBox1.Text = DoWork().Result;
        }

        private async Task<string> DoWork()
        {
            using (var client = new HttpClient())
            {
                var str = await client.GetStringAsync("http://www.google.com").ConfigureAwait(continueOnCapturedContext: false);

                // Throws InvalidOperationException:
                // Cross -thread operation not valid: Control 'textBox1' accessed from a thread other than the thread it was created on.
                // textBox1.Text = str;                
                return str;
            }
        }
    }
}
