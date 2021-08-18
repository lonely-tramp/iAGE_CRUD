using System;
using System.Collections.Generic;
using iAge_CRUD.Actions;
using iAGE_CRUD.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SimpleUnitTest
{
    [TestClass]
    public class TestValidationArguments
    {
        [TestMethod]
        public void TestValidationArgumentsForAdding()
        {

            var validArgunemts = new Dictionary<string, string>
            {
                {"FirstName", Guid.NewGuid().ToString()},
                {"LastName", Guid.NewGuid().ToString()},
                {"Salary", "250"}
            };

            var invalidArgunemts = new Dictionary<string, string>
            {
                {"FirstName-", Guid.NewGuid().ToString()},
                {"LastName", Guid.NewGuid().ToString()},
                {"Salary", "250"}
            };

            var invalidArgunemts2 = new Dictionary<string, string>
            {
                {"FirstName", Guid.NewGuid().ToString()},
                {"", Guid.NewGuid().ToString()},
                {"Salary", "250"}
            };

            var invalidArgunemts3 = new Dictionary<string, string>
            {
                {"Id", "123"},
                {"FirstName", Guid.NewGuid().ToString()},
                {"LastName", Guid.NewGuid().ToString()},
                {"Salary", "250"}
            };

            var invalidArgunemts4 = new Dictionary<string, string>
            {
                {"FirstName", Guid.NewGuid().ToString()},
                {"LastName", Guid.NewGuid().ToString()},
                {"SomeOne", Guid.NewGuid().ToString()},
                {"Salary", "250"}
            };

            var valid = ArgumentsValidator.IsValidArguments(validArgunemts, ActionsEnum.Add);
            var invalid = ArgumentsValidator.IsValidArguments(invalidArgunemts, ActionsEnum.Add);
            var invalid2 = ArgumentsValidator.IsValidArguments(invalidArgunemts2, ActionsEnum.Add);
            var invalid3 = ArgumentsValidator.IsValidArguments(invalidArgunemts3, ActionsEnum.Add);
            var invalid4 = ArgumentsValidator.IsValidArguments(invalidArgunemts4, ActionsEnum.Add);

            Assert.IsTrue(valid);
            Assert.IsFalse(invalid);
            Assert.IsFalse(invalid2);
            Assert.IsFalse(invalid3);
            Assert.IsFalse(invalid4);
        }

        [TestMethod]
        public void TestValidationValuesForAdding()
        {
            var validArgunemts = new Dictionary<string, string>
            {
                {"Id", "1"},
                {"FirstName", Guid.NewGuid().ToString()},
                {"LastName", Guid.NewGuid().ToString()},
                {"Salary", "250"}
            };

            var invalidArgunemts = new Dictionary<string, string>
            {
                {"Id", "-1"},
                {"FirstName", Guid.NewGuid().ToString()},
                {"LastName", Guid.NewGuid().ToString()},
                {"Salary", "250"}
            };

            var invalidArgunemts2 = new Dictionary<string, string>
            {
                {"Id", "0"},
                {"FirstName", Guid.NewGuid().ToString()},
                {"LastName", Guid.NewGuid().ToString()},
                {"Salary", "2.50"}
            };

            var invalidArgunemts3 = new Dictionary<string, string>
            {
                {"Id", "1"},
                {"FirstName", Guid.NewGuid().ToString()},
                {"LastName", Guid.NewGuid().ToString()},
                {"Salary", "-250"}
            };

            var invalidArgunemts4 = new Dictionary<string, string>
            {
                {"Id", "1"},
                {"FirstName", ""},
                {"LastName", Guid.NewGuid().ToString()},
                {"Salary", "25.0"}
            };


            var valid = ArgumentsValidator.IsValidValues(validArgunemts, out _);
            var invalid = ArgumentsValidator.IsValidValues(invalidArgunemts, out _);
            var invalid2 = ArgumentsValidator.IsValidValues(invalidArgunemts2, out _);
            var invalid3 = ArgumentsValidator.IsValidValues(invalidArgunemts3, out _);
            var invalid4 = ArgumentsValidator.IsValidValues(invalidArgunemts4, out _);

            Assert.IsTrue(valid);
            Assert.IsFalse(invalid);
            Assert.IsFalse(invalid2);
            Assert.IsFalse(invalid3);
            Assert.IsFalse(invalid4);
        }
    }
}
