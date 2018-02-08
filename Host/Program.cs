using Microsoft.Owin.Hosting;
using System;

namespace Host
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseAddress = "http://127.0.0.1:8082";

            using (WebApp.Start<Startup>(url: baseAddress))
            {
                Console.WriteLine($"Running... on {baseAddress}");
                Console.ReadKey(true);
            }
        }
    }
}
