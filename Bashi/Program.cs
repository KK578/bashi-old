using System;

namespace Bashi
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var token = args[0];
            var container = BashiContainer.Build();
            var bashi = new BashiApp(token, container);

            bashi.Connect();

            Console.ReadLine();
        }
    }
}
