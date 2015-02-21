using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _3_UnobservedExceptions
{
    class Program
    {
        public static async void DoWorkAsync()
        {
            Task.Run(() =>
            {
                // this exception will be swallowed
                throw new InvalidOperationException("Unable to perform task.");
            });
        }

        public static async void DoWorkAsync1()
        {
            await Task.Delay(100);
            throw new Exception("Unable to perform task.");
        }

        public static async Task MainAsync()
        {
            TaskScheduler.UnobservedTaskException += (o, e) =>
            {
                Console.WriteLine("Unobserved exception: \n\r {0} \n\r {1}", e.Exception.Message, e.Exception.InnerException.Message);
            };

            try
            {
                DoWorkAsync();
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }

            // Wait for the task to finish without observing the exception.
            Thread.Sleep(100);

            // Collect the Task with GC.
            GC.Collect();
            GC.WaitForPendingFinalizers();

            Console.ReadLine();
        }
    

        public static void Main()
        {
            MainAsync().Wait();
        }
    }
}
