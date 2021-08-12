using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using iAGE_CRUD;

namespace SimpleUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestAddEmployee()
        {
            var rnd = new Random();
            var em = new EmployeesManager();
            var firstName = "Вася-" + rnd.Next();
            var lastName = "Пупкин-" + rnd.Next();
            decimal salary = rnd.Next();

            var newE = em.Insert(firstName, lastName, salary);
            var addedE = em.Get(newE.Id);

            Assert.AreEqual(firstName, addedE.FirstName);
            Assert.AreEqual(lastName, addedE.LastName);
            Assert.AreEqual(salary, addedE.SalaryPerHour);
        }

        [TestMethod]
        public void TestCounter()
        {
            var rnd = new Random();
            var em = new EmployeesManager();
            var lastE = em.Get().OrderByDescending(i => i.Id).FirstOrDefault();
            int lastId = 0;
            if (lastE != null)
                lastId = lastE.Id;
            var firstName = "Игорь-" + rnd.Next();
            var lastName = "Фляжкин-" + rnd.Next();
            decimal salary = rnd.Next();

            var newE = em.Insert(firstName, lastName, salary);
            var addedE = em.Get(newE.Id);

            Assert.IsTrue((addedE.Id - lastId) == 1);
        }
    }
}
