using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace TestDomain
{
    [TestClass]
    public class TestAccountBalanceReport
    {
        AccountBalanceReport accountBalanceReport;

        [TestInitialize]
        public void Setup()
        {
            PersonalAccount a = new PersonalAccount{ StartingAmount = 0 };
            accountBalanceReport = new AccountBalanceReport { Account = a };
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
            Workspace newWorkspace = new Workspace { Name = "Nuevo", UserAdmin = newUser };
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
            Workspace newWorkspace = new Workspace { Name = "Nuevo", UserAdmin = newUser };
            PersonalAccount newAccount = new PersonalAccount { Name = "Nuevo", WorkSpace = newWorkspace, StartingAmount = 0 };
            accountBalanceReport.Account = newAccount;
        }

        [TestMethod]
        public void CorrectBalanceOperation()
        {
            int montoInicial = 500;
            int ingresos = 300;
            int costos = 600;
            int balance = montoInicial + ingresos - costos;

            Assert.AreEqual(balance, accountBalanceReport.CalculateBalance());
        }
    }
}
