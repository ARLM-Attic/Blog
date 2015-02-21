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

namespace _1_SimpleAsyncForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {           
            await DownloadUrlAsync("http://www.google.com");
        }

        private async Task DownloadUrlAsync(string url)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                textBox1.Text = await httpClient.GetStringAsync(url);
            }            
        }    
    }
}
