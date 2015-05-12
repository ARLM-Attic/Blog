using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace _4_ConsoleApplicationWait
{
    class Program
    {
        private static async Task<string> DoWork(string url)
        {
            using (var client = new HttpClient())
            {
                var str = await client.GetStringAsync(url);
                //Console.WriteLine(str);
                return str;
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Start");

            // None of these cause a deadlock
            Console.WriteLine("DoWork().Wait()");
            DoWork("http://www.google.com").Wait();

            Console.WriteLine("DoWork().Result");
            var str = DoWork("http://www.google.com").Result;

            Console.WriteLine("Task.WaitAll");
            Task.WaitAll(DoWork("http://www.google.com"), DoWork("http://www.amazon.com"));

            Console.WriteLine("Finish");

            Console.ReadKey();
        }
    }
}
