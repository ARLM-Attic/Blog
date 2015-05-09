using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
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
            DoWork().Wait();
        }

        private async Task DoWork()
        {
            using (var client = new HttpClient())
            {
                var syncContext = WindowsFormsSynchronizationContext.Current;
                var executionContext = Thread.CurrentThread.ExecutionContext;              

                this.textBox1.Text = await client.GetStringAsync("http://www.google.com");
            }
        }
    }
}
