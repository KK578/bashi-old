using System;

namespace Bashi
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var token = args[0];
            var bashi = new BashiApp(token);

            bashi.Connect();

            Console.ReadLine();
        }
    }
}
