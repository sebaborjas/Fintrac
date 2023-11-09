using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Transactions;
using Domain;
using Domain.Exceptions;
using Domain.DataTypes;

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
                Currency = CurrencyType.USD
            };
        }

        [TestMethod]
        public void EqualsExchanges()
        {
            Exchange otherExchange = new Exchange { Date = DateTime.Today, Currency = CurrencyType.USD };

            Assert.AreEqual(exchange, otherExchange);
        }

        [TestMethod]
        public void NotEqualsExchanges()
        {
            Exchange otherExchange = new Exchange { Date = DateTime.Today.AddDays(-1), Currency = CurrencyType.USD };

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
        public void TestCurrencyValueInt()
        {
            exchange.CurrencyValue = 40;
            Assert.AreEqual(40, exchange.CurrencyValue);
        }

        [TestMethod]
        public void TestCurrencyValueDecimal()
        {
            exchange.CurrencyValue = 40.5;
            Assert.AreEqual(40.5, exchange.CurrencyValue);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCurrencyValueNegativo()
        {
            exchange.CurrencyValue = -100;
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
