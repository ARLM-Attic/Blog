using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2_SynchronizationContextForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SynchronizationContext winFormsSynchronizationContext = SynchronizationContext.Current;
            DoBackgroundWork(winFormsSynchronizationContext);
        }

        public void DoBackgroundWork(SynchronizationContext context)
        {
            Task.Run(() =>
            {
                Console.WriteLine("Running on Thread:{0}, IsThradPoolThread:{1}",
                            Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread);

                int threadId = Thread.CurrentThread.ManagedThreadId;
                bool isThreadPool = Thread.CurrentThread.IsThreadPoolThread;

                SendOrPostCallback hello =
                    (state) =>
                    {
                        Thread.Sleep(1000);
                        Console.WriteLine("Running on Thread:{0}, IsThradPoolThread:{1}",
                            Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread);

                        textBox1.Text = string.Format("Hello from Thread:{0}, IsThradPoolThread:{1}", threadId,
                            isThreadPool);
                    };

                Console.WriteLine("About to post Task");
                context.Post(hello, null);
                Console.WriteLine("Task posted");
            });
        }
    }
}
