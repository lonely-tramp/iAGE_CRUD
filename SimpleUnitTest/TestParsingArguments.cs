using iAGE_CRUD.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SimpleUnitTest
{
    [TestClass]
    public class TestParsingArguments
    {
        [TestMethod]
        public void TestParsingArguments1()
        {
            var validArgs1 = new string[] {"key:value"};
            var validArgs2 = new string[] {"key::value"};
            var invalidArgs1 = new string[] {"keyvalue"};
            var invalidArgs2 = new string[] {""};

            var parser = new ArgumentsParser(':');

            var valid1 = parser.TryParse(validArgs1, out _);
            var valid2 = parser.TryParse(validArgs2, out _);
            var invalid1 = parser.TryParse(invalidArgs1, out _);
            var invalid2 = parser.TryParse(invalidArgs2, out _);

            Assert.IsTrue(valid1);
            Assert.IsTrue(valid2);
            Assert.IsFalse(invalid1);
            Assert.IsFalse(invalid2);
        }
    }
}
