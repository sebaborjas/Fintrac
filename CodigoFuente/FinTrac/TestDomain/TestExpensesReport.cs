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

        List<Transaction> transactionList;
        
        [TestInitialize]
        public void SetUp() 
        {
            User newUser = new User { Name = "Test", LastName = "Test", Email = "a@a.com", Password = "12345678909" };
            workSpace = new Workspace{ UserAdmin = newUser, Name = $"Espacio personal de {newUser.Name} {newUser.LastName}" };


            PersonalAccount account = new PersonalAccount { Name = "Cuenta personal", StartingAmount = 700, WorkSpace = workSpace, Currency = CurrencyType.UYU, CreationDate = DateTime.Today.AddDays(-10) };
            PersonalAccount commerceAccount = new PersonalAccount { Name = "Cuenta comercio", StartingAmount = 0, WorkSpace = workSpace, Currency = CurrencyType.UYU, CreationDate = DateTime.Today.AddDays(-8) };


            Category categoryCost = new Category { Name = "Egresos", Type = CategoryType.Cost, Status = CategoryStatus.Active, CreationDate = DateTime.Today.AddDays(-10) };
            Category categoryIncome = new Category { Name = "Ingresos", Type = CategoryType.Income, Status = CategoryStatus.Active, CreationDate = DateTime.Today.AddDays(-7) };

            Transaction firstTransaction = new Transaction { Title = "Gasto panaderia", Amount = 100, Currency = CurrencyType.UYU, Category = categoryCost, Account = account, CreationDate = DateTime.Today.AddDays(-6) };
            Transaction secondTransaction = new Transaction { Title = "Gasto super", Amount = 200, Currency = CurrencyType.UYU, Category = categoryCost, Account = account, CreationDate = DateTime.Today.AddDays(-5) };

            Transaction thirdRransaction = new Transaction { Title = "Venta remera", Amount = 600, Currency = CurrencyType.UYU, Category = categoryIncome, Account = commerceAccount, CreationDate = DateTime.Today.AddDays(-2) };

            transactionList = new List<Transaction> { firstTransaction, secondTransaction };

            

            account.Transactions.Add(firstTransaction);
            account.Transactions.Add(secondTransaction);
            commerceAccount.Transactions.Add(thirdRransaction);

            workSpace.Accounts.Add(account);
            workSpace.Accounts.Add(commerceAccount);

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
            List<Transaction> transactionListsFromMethod = expensesReport.Expenses();


            CollectionAssert.AreEqual(transactionList, transactionListsFromMethod);
        }

        [TestMethod]
        public void ShowExpensesFilteredByExistingCaegory()
        {

            string categoryName = "Egresos";

            List<Transaction> transactionListsFromMethod = expensesReport.ExpensesByCategory(categoryName);

            List<Transaction> transactionListEgresos = transactionList.Where(transaction => transaction.Category.Name == categoryName).ToList();


            CollectionAssert.AreEqual(transactionListEgresos, transactionListsFromMethod);
        }

        [TestMethod]
        public void ShowExpensesFilteredByNotExistingCaegory()
        {

            string categoryName = "Gastos Mayo";

            List<Transaction> transactionListsFromMethod = expensesReport.ExpensesByCategory(categoryName);


            Assert.AreEqual(0,transactionListsFromMethod.Count);
        }

        [TestMethod]
        public void ShowExpensesFilteredByDate()
        {

            DateTime firstDate = DateTime.Today.AddDays(-6);
            DateTime secondDate = DateTime.Today.AddDays(-4);


            List<Transaction> transactionListsFromMethod = expensesReport.ExpensesByDate(firstDate, secondDate);

            List<Transaction> transactionListEgresos = transactionList.Where(transaction => transaction.CreationDate >= firstDate && transaction.CreationDate <= secondDate).ToList();


            CollectionAssert.AreEqual(transactionListsFromMethod, transactionListEgresos);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDateException))]
        public void ShowExpensesFilteredByInvalidDate()
        {

            DateTime firstDate = DateTime.Today.AddDays(-4);
            DateTime secondDate = DateTime.Today.AddDays(-6);


            List<Transaction> transactionListsFromMethod = expensesReport.ExpensesByDate(firstDate, secondDate);
        }

        [TestMethod]
        public void ShowExpensesFilteredByAccount()
        {

            string accountName = "Cuenta Personal";

            List<Transaction> transactionListsFromMethod = expensesReport.ExpensesByAccount(accountName);

            List<Transaction> transactionsPersonalAccount = transactionList.Where(transaction => transaction.Account.Name == accountName).ToList();


            CollectionAssert.AreEqual(transactionListsFromMethod, transactionsPersonalAccount);
        }

        [TestMethod]
        public void ShowExpensesFilteredByNotExistingAccount()
        {

            string accountName = "Cuenta Personal";

            List<Transaction> transactionListsFromMethod = expensesReport.ExpensesByAccount(accountName);


            Assert.AreEqual(0,transactionListsFromMethod.Count);
        }

    }
}
