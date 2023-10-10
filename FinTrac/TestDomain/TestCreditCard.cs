using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.Exceptions;
using Domain.DataTypes;

namespace TestDomain
{
    [TestClass]
    public class TestCreditCard
    {
        private CreditCard creditCard;
        [TestInitialize]
        public void Setup()
        {
            creditCard = new CreditCard();
        }

        [TestMethod]
        [ExpectedException(typeof(EmptyFieldException))]
        public void EmptyNameException()
        {
            creditCard.Name = "";
        }

        [TestMethod]
        public void ValidName()
        {
            creditCard.Name = "Banco Santander";
            Assert.AreEqual("Banco Santander", creditCard.Name);
        }

        [TestMethod]
        public void DefaultCurrencyUYU()
        {
            Assert.AreEqual(CurrencyType.UYU, creditCard.Currency);
        }

        [TestMethod]
        public void ChangeCurrencyToUSD()
        {
            creditCard.Currency = CurrencyType.USD;
            Assert.AreEqual(CurrencyType.USD, creditCard.Currency);
        }

        [TestMethod]
        public void SetWorkSpace()
        {
            User newUser = new User { Name = "Test", LastName = "Test", Email = "a@a.com", Password = "12345678909" };
            Workspace workSpace = new Workspace(newUser, "Test");
            creditCard.WorkSpace = workSpace;
            Assert.AreEqual(workSpace, creditCard.WorkSpace);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EmptyWorkSpace()
        {
            creditCard.WorkSpace = null;
        }

        [TestMethod]
        public void ValidBankName()
        {
            creditCard.BankName = "Banco Santander";
            Assert.AreEqual("Banco Santander", creditCard.BankName);
        }

        [TestMethod]
        [ExpectedException(typeof(EmptyFieldException))]
        public void InvalidBankName()
        {
            creditCard.BankName = "";
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void LastDigitsLengthThree()
        {
            creditCard.LastDigits = 123;
        }

        [TestMethod]
        public void ValidLastDigits()
        {
            creditCard.LastDigits = 1234;
            Assert.AreEqual(1234, creditCard.LastDigits);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void LastDigitsLengthFive()
        {
            creditCard.LastDigits = 12345;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NegativeAvailableCredit()
        {
            creditCard.AvailableCredit= -1;
        }
        [TestMethod]
        public void ZeroAvailableCredit()
        {
            creditCard.AvailableCredit = 0;
            Assert.AreEqual(0, creditCard.AvailableCredit);
        }

        [TestMethod]
        public void DecimalAvailableCredit()
        {
            creditCard.AvailableCredit = 100.10;
            Assert.AreEqual(100.10, creditCard.AvailableCredit);
        }

        [TestMethod]
        public void ValidDeadline()
        {
            creditCard.DeadLine = 10;
            Assert.AreEqual(10, creditCard.DeadLine);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ZeroDeadline()
        {
            creditCard.DeadLine = 0;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TwentyNineDeadline()
        {
            creditCard.DeadLine = 29;
        }








    }
}
