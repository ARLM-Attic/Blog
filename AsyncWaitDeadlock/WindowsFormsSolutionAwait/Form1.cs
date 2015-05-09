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

        private async void downloadButton_Click(object sender, EventArgs e)
        {
            this.textBox1.Clear();
            await DoWork();
        }

        private async Task DoWork()
        {
            using (var client = new HttpClient())
            {
                this.textBox1.Text = await client.GetStringAsync("http://www.google.com");
            }
        }
    }
}
