using System;
using System.IO;
using System.Linq;
using Bashi.Config.File;
using NUnit.Framework;

namespace Bashi.Config.Test
{
    public class BashiConfigFileTest
    {
        private BashiConfigFile subject;

        private readonly string filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                                                        "Data/ConfigFileTest.txt");

        [SetUp]
        public void SetUp()
        {
            subject = new BashiConfigFile(filepath);
        }

        [Test]
        public void ReadLines_ReadsDataFromFile()
        {
            var result = Enumerable.ToList<string>(subject.ReadLines());

            Assert.That(result.Count, Is.EqualTo(3));
            Assert.That(result[0], Is.EqualTo("A"));
            Assert.That(result[1], Is.EqualTo("B"));
            Assert.That(result[2], Is.EqualTo("C"));
        }
    }
}
