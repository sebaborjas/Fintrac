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
    public class TestExpensesReport
    {
        ExpensesReport expensesReport;
        Workspace workSpace;

        List<Transactions> transactionList;
        
        [TestInitialize]
        public void SetUp() 
        {
            User newUser = new User { Name = "Test", LastName = "Test", Email = "a@a.com", Password = "12345678909" };
            workSpace = new Workspace{ UserAdmin = newUser, Name = $"Espacio personal de {newUser.Name} {newUser.LastName}" };


            PersonalAccount account = new PersonalAccount { Name = "Cuenta personal", StartingAmount = 700, WorkSpace = workSpace, Currency = CurrencyType.UYU, CreationDate = DateTime.Today.AddDays(-10) };
            PersonalAccount commerceAccount = new PersonalAccount { Name = "Cuenta comercio", StartingAmount = 0, WorkSpace = workSpace, Currency = CurrencyType.UYU, CreationDate = DateTime.Today.AddDays(-8) };


            Category categoryCost = new Category { Name = "Egresos", Type = CategoryType.Cost, Status = CategoryStatus.Active, CreationDate = DateTime.Today.AddDays(-10) };
            Category categoryIncome = new Category { Name = "Ingresos", Type = CategoryType.Income, Status = CategoryStatus.Active, CreationDate = DateTime.Today.AddDays(-7) };

            Transactions firstTransaction = new Transactions { Title = "Gasto panaderia", Amount = 100, Currency = CurrencyType.UYU, Category = categoryCost, Account = account, CreationDate = DateTime.Today.AddDays(-6) };
            Transactions secondTransaction = new Transactions { Title = "Gasto super", Amount = 200, Currency = CurrencyType.UYU, Category = categoryCost, Account = account, CreationDate = DateTime.Today.AddDays(-5) };

            Transactions thirdRransaction = new Transactions { Title = "Venta remera", Amount = 600, Currency = CurrencyType.UYU, Category = categoryIncome, Account = commerceAccount, CreationDate = DateTime.Today.AddDays(-2) };

            transactionList = new List<Transactions> { firstTransaction, secondTransaction };

            

            account.TransactionList.Add(firstTransaction);
            account.TransactionList.Add(secondTransaction);
            commerceAccount.TransactionList.Add(thirdRransaction);

            workSpace.AccountList.Add(account);
            workSpace.AccountList.Add(commerceAccount);

            expensesReport = new ExpensesReport { WorkSpace = workSpace };



        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void NotWorkspaceException()
        {
            expensesReport.WorkSpace = null;
        }

        [TestMethod]
        public void SetCorrectWorkspace()
        {
            User newUser = new User { Name = "Test", LastName = "Test", Address = "", Password = "12345678901", Email = "test@test.com" };
            Workspace newWorkspace = new Workspace{ UserAdmin = newUser, Name = $"Espacio personal de {newUser.Name} {newUser.LastName}" };
            expensesReport.WorkSpace = newWorkspace;
        }

        [TestMethod]
        public void ListExpenses()
        {
            List<Transactions> transactionListsFromMethod = expensesReport.ListExpenses();


            CollectionAssert.AreEqual(transactionList, transactionListsFromMethod);
        }

        [TestMethod]
        public void ShowExpensesFilteredByExistingCaegory()
        {

            string categoryName = "Egresos";

            List<Transactions> transactionListsFromMethod = expensesReport.ListExpensesByCategory(categoryName);

            List<Transactions> transactionListEgresos = transactionList.Where(transaction => transaction.Category.Name == categoryName).ToList();


            CollectionAssert.AreEqual(transactionListEgresos, transactionListsFromMethod);
        }

        [TestMethod]
        public void ShowExpensesFilteredByNotExistingCaegory()
        {

            string categoryName = "Gastos Mayo";

            List<Transactions> transactionListsFromMethod = expensesReport.ListExpensesByCategory(categoryName);


            Assert.AreEqual(0,transactionListsFromMethod.Count);
        }

        [TestMethod]
        public void ShowExpensesFilteredByDate()
        {

            DateTime firstDate = DateTime.Today.AddDays(-6);
            DateTime secondDate = DateTime.Today.AddDays(-4);


            List<Transactions> transactionListsFromMethod = expensesReport.ListExpensesByDate(firstDate, secondDate);

            List<Transactions> transactionListEgresos = transactionList.Where(transaction => transaction.CreationDate >= firstDate && transaction.CreationDate <= secondDate).ToList();


            CollectionAssert.AreEqual(transactionListsFromMethod, transactionListEgresos);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDateException))]
        public void ShowExpensesFilteredByInvalidDate()
        {

            DateTime firstDate = DateTime.Today.AddDays(-4);
            DateTime secondDate = DateTime.Today.AddDays(-6);


            List<Transactions> transactionListsFromMethod = expensesReport.ListExpensesByDate(firstDate, secondDate);
        }

        [TestMethod]
        public void ShowExpensesFilteredByAccount()
        {

            string accountName = "Cuenta Personal";

            List<Transactions> transactionListsFromMethod = expensesReport.ListExpensesByAccount(accountName);

            List<Transactions> transactionsPersonalAccount = transactionList.Where(transaction => transaction.Account.Name == accountName).ToList();


            CollectionAssert.AreEqual(transactionListsFromMethod, transactionsPersonalAccount);
        }

        [TestMethod]
        public void ShowExpensesFilteredByNotExistingAccount()
        {

            string accountName = "Cuenta Personal";

            List<Transactions> transactionListsFromMethod = expensesReport.ListExpensesByAccount(accountName);


            Assert.AreEqual(0,transactionListsFromMethod.Count);
        }

    }
}
