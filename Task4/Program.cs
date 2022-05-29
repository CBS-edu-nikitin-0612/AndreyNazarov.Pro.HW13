using System;
using System.Linq;

namespace Task4
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] array = new int[1000000];
            Random rnd = new Random();
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = rnd.Next();
            }

            var result = array.AsParallel()
                .Where(a => 0 != a % 2)
                .OrderBy(a => a);

            Console.WriteLine(string.Join(" ", result.Take(200)));
        }
    }
}
