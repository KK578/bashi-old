using System;
using Autofac;

namespace Bashi
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var container = BashiContainer.Build();
            var bashi = container.Resolve<BashiApp>();

            bashi.Connect();

            Console.ReadLine();
        }
    }
}
