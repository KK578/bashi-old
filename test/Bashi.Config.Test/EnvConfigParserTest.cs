using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Bashi.Config.Test
{
    public class EnvConfigParserTest
    {
        private EnvConfigParser subject;

        [SetUp]
        public void SetUp()
        {
            subject = new EnvConfigParser();
        }

        [Test]
        public void Parse_MultipleWhitespaceStrings_ReturnsNothing()
        {
            var keyPairs = subject.Parse(CreateInput()).ToList();

            Assert.That(keyPairs.Count, Is.EqualTo(0));

            IEnumerable<string> CreateInput()
            {
                yield return "";
                yield return " ";
                yield return "\t";
            }
        }

        [Test]
        public void Parse_LineWithComment_ReturnsNothing()
        {
            var keyPairs = subject.Parse(CreateInput()).ToList();

            Assert.That(keyPairs.Count, Is.EqualTo(0));

            IEnumerable<string> CreateInput()
            {
                yield return "#ABC=123";
            }
        }

        [Test]
        public void Parse_LineWithoutEquals_ReturnsNothing()
        {
            foreach (var unused in subject.Parse(CreateInput()))
            {
                Assert.Fail();
            }

            IEnumerable<string> CreateInput()
            {
                yield return "ABC123";
            }
        }

        [Test]
        public void Parse_LineWithCommentCharacter_ReturnsKeyPairValue()
        {
            var keyPairs = subject.Parse(CreateInput()).ToList();

            Assert.That(keyPairs.Count, Is.EqualTo(1));
            Assert.That(keyPairs[0].Key, Is.EqualTo("ABC#"));
            Assert.That(keyPairs[0].Value, Is.EqualTo("123"));

            IEnumerable<string> CreateInput()
            {
                yield return "ABC#=123";
            }
        }

        [Test]
        public void Parse_LineWithEquals_ReturnsKeyPairValue()
        {
            var keyPairs = subject.Parse(CreateInput()).ToList();

            Assert.That(keyPairs.Count, Is.EqualTo(1));
            Assert.That(keyPairs[0].Key, Is.EqualTo("ABC"));
            Assert.That(keyPairs[0].Value, Is.EqualTo("123"));

            IEnumerable<string> CreateInput()
            {
                yield return "ABC=123";
            }
        }

        [Test]
        public void Parse_LineWithoutActualValue_ReturnsKeyPairValue()
        {
            var keyPairs = subject.Parse(CreateInput()).ToList();

            Assert.That(keyPairs.Count, Is.EqualTo(1));
            Assert.That(keyPairs[0].Key, Is.EqualTo("ABC"));
            Assert.That(keyPairs[0].Value, Is.EqualTo(""));

            IEnumerable<string> CreateInput()
            {
                yield return "ABC=";
            }
        }

        [Test]
        public void Parse_MultipleLinesWithEquals_ReturnsKeyPairValue()
        {
            var keyPairs = subject.Parse(CreateInput()).ToList();

            Assert.That(keyPairs.Count, Is.EqualTo(2));
            Assert.That(keyPairs[0].Key, Is.EqualTo("ABC"));
            Assert.That(keyPairs[0].Value, Is.EqualTo("123"));
            Assert.That(keyPairs[1].Key, Is.EqualTo("DEF"));
            Assert.That(keyPairs[1].Value, Is.EqualTo("456"));

            IEnumerable<string> CreateInput()
            {
                yield return "ABC=123";
                yield return "DEF=456";
            }
        }

        [Test]
        public void Parse_LineWithSurroundingWhitespace_ReturnsCorrectlyTrimmedKeyPairValue()
        {
            var keyPairs = subject.Parse(CreateInput()).ToList();

            Assert.That(keyPairs.Count, Is.EqualTo(1));
            Assert.That(keyPairs[0].Key, Is.EqualTo("ABC"));
            Assert.That(keyPairs[0].Value, Is.EqualTo("123"));

            IEnumerable<string> CreateInput()
            {
                yield return "  ABC = 123   ";
            }
        }
    }
}
