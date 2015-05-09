using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AsyncBridge;

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
                        
    using (var asyncBridge = AsyncHelper.Wait)
    {
        asyncBridge.Run(DoWork());
    }
}

private async Task DoWork()
{
    using (var client = new HttpClient())
    {                
        var str = await client.GetStringAsync("http://www.google.com");
        this.textBox1.Text = str;
    }
}
    }
}
