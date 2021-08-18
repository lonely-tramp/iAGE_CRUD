using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using iAGE_CRUD.Storage;

namespace SimpleUnitTest
{
    [TestClass]
    public class TestFileStorage
    {
        [TestMethod]
        public void TestAddEmployee()
        {
            var rnd = new Random();
            var storage = new FileStorageManager();
            var firstName = "Вася-" + rnd.Next();
            var lastName = "Пупкин-" + rnd.Next();
            decimal salary = rnd.Next();

            var newE = storage.Insert(firstName, lastName, salary);
            var addedE = new FileStorageManager().Get(newE.Id);

            Assert.AreEqual(firstName, addedE.FirstName);
            Assert.AreEqual(lastName, addedE.LastName);
            Assert.AreEqual(salary, addedE.SalaryPerHour);
        }

        [TestMethod]
        public void TestCounter()
        {
            var rnd = new Random();
            var storage = new FileStorageManager();
            var lastE = storage.Get().OrderByDescending(i => i.Id).FirstOrDefault();
            int lastId = 0;
            if (lastE != null)
                lastId = lastE.Id;
            var firstName = "Игорь-" + rnd.Next();
            var lastName = "Фляжкин-" + rnd.Next();
            decimal salary = rnd.Next();

            var newE = storage.Insert(firstName, lastName, salary);
            var addedE = new FileStorageManager().Get(newE.Id);

            Assert.IsTrue((addedE.Id - lastId) == 1);
        }

        [TestMethod]
        public void TestUpdate()
        {
            TestAddEmployee();
            var storage = new FileStorageManager();
            var employee = storage.Get().First();

            string firstName = "Кирилл";
            string lastName = null;
            decimal salary = 250;

            storage.Update(employee.Id, firstName, lastName, salary);
            var addedE = new FileStorageManager().Get(employee.Id);

            Assert.AreEqual(firstName, addedE.FirstName);
            Assert.AreNotEqual(lastName, addedE.LastName);
            Assert.AreEqual(salary, addedE.SalaryPerHour);
        }

        [TestMethod]
        public void TestDelete()
        {
            TestAddEmployee();
            var storage = new FileStorageManager();
            var employee = storage.Get().First();

            storage.Remove(employee.Id);
            var addedE = new FileStorageManager().Get(employee.Id);

            Assert.AreEqual(addedE, null);
        }
    }
}
