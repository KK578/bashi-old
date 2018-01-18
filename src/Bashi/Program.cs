using System;
using Autofac;

namespace Bashi
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var token = args[0];

            var container = new BashiContainer().RegisterEnvironment(token).Build();
            var bashi = container.Resolve<BashiApp>();

            bashi.Connect();

            Console.ReadLine();
        }
    }
}
