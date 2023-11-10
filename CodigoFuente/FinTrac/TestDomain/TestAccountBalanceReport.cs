using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.DataTypes;

namespace TestDomain
{
    [TestClass]
    public class TestAccountBalanceReport
    {
        AccountBalanceReport accountBalanceReport;
        Workspace workSpace;

        [TestInitialize]
        public void Setup()
        {
            User newUser = new User { Name = "Test", LastName = "Test", Email = "a@a.com", Password = "12345678909" };
            workSpace = new Workspace { UserAdmin = newUser, Name = $"Espacio personal de {newUser.Name} {newUser.LastName}" };
            PersonalAccount account = new PersonalAccount { Name = "Test", StartingAmount = 0, WorkSpace = workSpace, Currency = CurrencyType.UYU, CreationDate = DateTime.Today.AddDays(-5) };
            workSpace.AccountList.Add(account);

            accountBalanceReport = new AccountBalanceReport { Account = account };
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void NotWorkspaceException()
        {
            accountBalanceReport.WorkSpace = null;
        }

        [TestMethod]
        public void SetCorrectWorkspace()
        {
            User newUser = new User { Name = "Test", LastName = "Test", Address = "", Password = "12345678901", Email = "test@test.com" };
            Workspace newWorkspace = new Workspace{ UserAdmin = newUser, Name = $"Espacio personal de {newUser.Name} {newUser.LastName}" };
            accountBalanceReport.WorkSpace = newWorkspace;
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void NotAccountException()
        {
            accountBalanceReport.Account = null;
        }

        [TestMethod]
        public void SetCorrectAccount()
        {
            User newUser = new User { Name = "Test", LastName = "Test", Address = "", Password = "12345678901", Email = "test@test.com" };
            Workspace newWorkspace = new Workspace { UserAdmin = newUser, Name = $"Espacio personal de {newUser.Name} {newUser.LastName}" };
            PersonalAccount newAccount = new PersonalAccount { Name = "Nuevo", WorkSpace = newWorkspace, StartingAmount = 0, CreationDate = DateTime.Today.AddDays(-5) };
            accountBalanceReport.Account = newAccount;
        }

        [TestMethod]
        public void CorrectBalanceOperationUYU()
        {
            int montoInicial = 500;
            int ingresos = 300;
            int costos = 600;
            int balance = montoInicial + ingresos - costos;

            Category categoriaIngreso = new Category { Name = "Ingreso", Type = CategoryType.Income, Status = CategoryStatus.Active, CreationDate = DateTime.Today };
            Category categoriaCostos = new Category { Name = "Costo", Type = CategoryType.Cost, Status = CategoryStatus.Active, CreationDate = DateTime.Today };

            Transaction trasaccionIngreso = new Transaction { 
                Title = "Transaction 1",
                Amount = ingresos,
                Currency = CurrencyType.UYU,
                Category = categoriaIngreso,
            };

            Transaction trasaccionCosto = new Transaction
            {
                Title = "Transaction 2",
                Amount = costos,
                Currency = CurrencyType.UYU,
                Category = categoriaCostos,
            };

            accountBalanceReport.Account.StartingAmount = montoInicial;
            accountBalanceReport.Account.TransactionList.Add(trasaccionIngreso);
            accountBalanceReport.Account.TransactionList.Add(trasaccionCosto);

            Assert.AreEqual(balance, accountBalanceReport.CalculateBalance());
        }

        [TestMethod]
        public void CorrectBalanceOperationUSD()
        {
            int montoInicial = 100;
            int ingresos = 50;
            int costos = 30;
            double DollarToday = 50;
            double DollarBefore = 47.4;
            double balance = montoInicial * DollarBefore + ingresos * DollarToday - costos * DollarBefore;
            accountBalanceReport.Currency = CurrencyType.USD;
            Category categoriaIngreso = new Category { Name = "Ingreso", Type = CategoryType.Income, Status = CategoryStatus.Active, CreationDate = DateTime.Today };
            Category categoriaCostos = new Category { Name = "Costo", Type = CategoryType.Cost, Status = CategoryStatus.Active, CreationDate = DateTime.Today };

            Transaction trasaccionIngreso = new Transaction
            {
                Title = "Transaction 1",
                Amount = ingresos,
                Currency = CurrencyType.USD,
                Category = categoriaIngreso,
            };

            Transaction trasaccionCosto = new Transaction
            {
                Title = "Transaction 2",
                Amount = costos,
                Currency = CurrencyType.USD,
                Category = categoriaCostos,
                CreationDate = DateTime.Today.AddDays(-2)
            };

            Exchange exchangeToday = new Exchange { DollarValue = DollarToday, Date = DateTime.Today, Workspace = accountBalanceReport.WorkSpace };
            Exchange exchangeYesterday = new Exchange { DollarValue = DollarBefore, Date = DateTime.Today.AddDays(-5), Workspace = accountBalanceReport.WorkSpace };

            workSpace.ExchangeList.Add(exchangeToday);
            workSpace.ExchangeList.Add(exchangeYesterday);

            accountBalanceReport.Account.StartingAmount = montoInicial;
            accountBalanceReport.Account.TransactionList.Add(trasaccionIngreso);
            accountBalanceReport.Account.TransactionList.Add(trasaccionCosto);

            Assert.AreEqual(balance, accountBalanceReport.CalculateBalance());
        }
    }
}
