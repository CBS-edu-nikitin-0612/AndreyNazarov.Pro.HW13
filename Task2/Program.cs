using System;
using System.Threading;

namespace Task2
{
    class Program
    {
        private AsyncCallback cb = callback;
        static void Main(string[] args)
        {
            Func<int, int> func = Do;

            func.BeginInvoke(100, callback, null);
        }

        static int Do(int input)
        {
            Thread.Sleep(4000);
            return input;
        }

        static void callback(IAsyncResult result)
        {
            Console.WriteLine("Вызов 24 строки");
            Console.WriteLine((result.AsyncState as Func<int, int>).EndInvoke(result));
        }
    }
}