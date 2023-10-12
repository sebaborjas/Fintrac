using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Transactions;
using Domain;
using Domain.Exceptions;

namespace TestDomain
{
    [TestClass]
    public class TestExchange
    {
        Exchange exchange;

        [TestInitialize]
        public void Setup()
        {
            exchange = new Exchange
            {
                Date = DateTime.Today,
            };
        }

        [TestMethod]
        public void EqualsExchanges()
        {
            Exchange otherExchange = new Exchange { Date = DateTime.Today };

            Assert.AreEqual(exchange, otherExchange);
        }

        [TestMethod]
        public void NotEqualsExchanges()
        {
            Exchange otherExchange = new Exchange { Date = DateTime.Today.AddDays(-1) };

            Assert.AreNotEqual(exchange, otherExchange);
        }

        [TestMethod]
        public void NotNullDate()
        {
            Assert.AreEqual(DateTime.Today, exchange.Date);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestDateGreaterToday()
        {
            exchange.Date = DateTime.Today.AddDays(1);
        }

        [TestMethod]
        public void TestDollarValueInt()
        {
            exchange.DollarValue = 40;
            Assert.AreEqual(40, exchange.DollarValue);
        }

        [TestMethod]
        public void TestDollarValueDecimal()
        {
            exchange.DollarValue = 40.5;
            Assert.AreEqual(40.5, exchange.DollarValue);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestDollarValueNegativo()
        {
            exchange.DollarValue = -100;
        }

        [TestMethod]
        public void NotNullWorkspace()
        {
            User userAdmin = new User
            {
                Name = "Test",
                LastName = "TestLastName",
                Address = "",
                Password = "1234123412",
                Email = "test@test.com"
            };
            Workspace workspace = new Workspace(userAdmin, "Personal");
            exchange.Workspace = workspace;
            Assert.IsNotNull(exchange.Workspace);
        }

    }
}
