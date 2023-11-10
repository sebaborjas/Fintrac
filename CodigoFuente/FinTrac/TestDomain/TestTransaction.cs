using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.DataTypes;
using Domain.Exceptions;

namespace TestDomain
{
    [TestClass]
    public class TestTransaction
    {
        Transaction transaction;
        Category category;

        [TestInitialize]
        public void TestInitialize()
        {
            transaction = new Transaction();
            category = new Category();
        }

        [TestMethod]
        public void EqualsTransaction()
        {
            Transaction otherTransaction = transaction;

            Assert.AreEqual(transaction, transaction);
        }

        [TestMethod]
        public void NotEqualsUsers()
        {
            Transaction otherTransaction = new Transaction { Title = "Test" };

            Assert.AreNotEqual(otherTransaction, transaction);
        }

        [TestMethod]
        public void TestTitle()
        {
            transaction.Title = "Test";
            Assert.AreEqual("Test", transaction.Title);
        }

        [TestMethod]
        [ExpectedException(typeof(EmptyFieldException))]
        public void TestTitleEmpty()
        {
            transaction.Title = "";
        }

        [TestMethod]
        public void TestCreationDate()
        {
            transaction.CreationDate = DateTime.Today;
            Assert.AreEqual(DateTime.Today, transaction.CreationDate);
        }

        [TestMethod]
        public void TestCreationDateAnterior()
        {
            transaction.CreationDate = DateTime.Today.AddDays(-1);
            Assert.AreEqual(DateTime.Today.AddDays(-1), transaction.CreationDate);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreationDatePosterior()
        {
            transaction.CreationDate = DateTime.Today.AddDays(1);
        }

        [TestMethod]
        public void TestAmount()
        {
            transaction.Amount = 100;
            Assert.AreEqual(100, transaction.Amount);
        }

        [TestMethod]
        public void TestAmountDecimal()
        {
            transaction.Amount = 100.5;
            Assert.AreEqual(100.5, transaction.Amount);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestAmountNegative()
        {
            transaction.Amount = -100;
        }

        [TestMethod]
        public void TestCurrency()
        {
            transaction.Currency = CurrencyType.UYU;
            Assert.AreEqual(CurrencyType.UYU, transaction.Currency);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestCurrencyInvalid()
        {
            transaction.Currency = (CurrencyType)3;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAccountNull()
        {
            transaction.Account = null;
        }

        [TestMethod]
        public void TestPersonalAccount()
        {
            User newUser = new User { Name = "Test", LastName = "Test", Email = "a@a.com", Password = "12345678909" };
            Workspace workSpace = new Workspace{ UserAdmin = newUser, Name = $"Espacio personal de {newUser.Name} {newUser.LastName}" };
            Account account = new PersonalAccount { Name = "Test", StartingAmount = 0, WorkSpace = workSpace, Currency = CurrencyType.UYU };
            transaction.Account = account;
            Assert.AreEqual(account, transaction.Account);
        }

        [TestMethod]
        public void TestCreditCardAccount()
        {
            User newUser = new User { Name = "Test", LastName = "Test", Email = "a@a.com", Password = "12345678909" };
            Workspace workSpace = new Workspace{ UserAdmin = newUser, Name = $"Espacio personal de {newUser.Name} {newUser.LastName}" };
            Account account = new CreditCard
            {
                BankName = "Test",
                AvailableCredit = 1,
                Name = "creditTest",
                LastDigits = "1234",
                DeadLine = 27,
                WorkSpace = workSpace,
            };
            transaction.Account = account;
            Assert.AreEqual(account, transaction.Account);
        }

        [TestMethod]
        public void TestCategoryActive()
        {
            category.Status = CategoryStatus.Active;
            transaction.Category = category;
            Assert.AreEqual(CategoryStatus.Active, transaction.Category.Status);
        }

        [TestMethod]
        [ExpectedException(typeof(InactiveCategoryException))]
        public void TestCategoryInactive()
        {
            category.Status = CategoryStatus.Inactive;
            transaction.Category = category;
        }

        [TestMethod]
        public void TestValidCategoryType()
        {
            category.Type = CategoryType.Cost;
            transaction.Category = category;
            Assert.AreEqual(CategoryType.Cost, transaction.Category.Type);
        }

        [TestMethod]
        public void TestTransactionCurrencyEqualsAccountCurrency()
        {
            User newUser = new User { Name = "Test", LastName = "Test", Email = "a@a.com", Password = "12345678909" };
            Workspace workSpace = new Workspace{ UserAdmin = newUser, Name = $"Espacio personal de {newUser.Name} {newUser.LastName}" };
            Account account = new PersonalAccount { Name = "Test", StartingAmount = 0, WorkSpace = workSpace, Currency = CurrencyType.UYU };

            transaction.Account = account;

            transaction.Currency = CurrencyType.UYU;

            Assert.AreEqual(account.Currency, transaction.Currency);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidTransactionCurrencyException))]
        public void TestTransactionCurrencyNotEqualsAccountCurrency()
        {
            User newUser = new User { Name = "Test", LastName = "Test", Email = "a@a.com", Password = "12345678909" };
            Workspace workSpace = new Workspace{ UserAdmin = newUser, Name = $"Espacio personal de {newUser.Name} {newUser.LastName}" };
            Account account = new PersonalAccount { Name = "Test", StartingAmount = 0, WorkSpace = workSpace, Currency = CurrencyType.UYU };

            transaction.Currency = CurrencyType.USD;
            transaction.Account = account;


        }
    }
}
