using System;
using Autofac;

namespace Bashi
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var token = args[0];

            var container = BashiContainer.Build();
            var bashi = container.Resolve<BashiApp>();

            bashi.Connect(token);

            Console.ReadLine();
        }
    }
}
