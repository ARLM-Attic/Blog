using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace _4_CustomAwaiters
{
    public static class Extensions
    {
        public static TaskAwaiter GetAwaiter(this int delay)
        {
            return Task.Delay(delay).GetAwaiter();
        }
    }    

    class Program
    {
        public static async Task Async1()
        {
            await 5000;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Start");

            Async1().Wait();

            Console.WriteLine("End");
            Console.ReadLine();
        }
    }
}
