﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
    this.textBox1.Text = DoWork().Result;
}

private async Task<string> DoWork()
{
    using(new NoSyncrhonizationContext())
    using (var client = new HttpClient())
    {
        var context = SynchronizationContext.Current;
        Debug.Assert(context == null);

        var str = await client.GetStringAsync("http://www.google.com");
        return str;
    }
}

        public sealed class NoSyncrhonizationContext : IDisposable
        {
            private SynchronizationContext previousSyncrhonizationContext;

            public NoSyncrhonizationContext()
            {
                previousSyncrhonizationContext = SynchronizationContext.Current;
                SynchronizationContext.SetSynchronizationContext(null);
            }

            public void Dispose()
            {
                SynchronizationContext.SetSynchronizationContext(previousSyncrhonizationContext);
            }
        }
    }
}
