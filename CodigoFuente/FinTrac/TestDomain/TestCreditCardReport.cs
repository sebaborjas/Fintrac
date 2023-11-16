using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DataTypes;
using Domain;

namespace TestDomain
{
    [TestClass]
    public class TestCreditCardReport
    {
        private CreditCardReport creditCardReport;
        private Workspace workSpace;
        private List<Transaction> transactionList;
        CreditCard account;

        [TestInitialize]
        public void SetUp()
        {
            User newUser = new User { Name = "Test", LastName = "Test", Email = "a@a.com", Password = "12345678909" };
            workSpace = new Workspace{ UserAdmin = newUser, Name = $"Espacio personal de {newUser.Name} {newUser.LastName}" };


            account = new CreditCard { Name = "Cuenta personal", CreationDate = DateTime.Today.AddDays(-60), WorkSpace = workSpace, BankName = "Santander", LastDigits = "1234", AvailableCredit = 300, DeadLine = 20 };
            


            Category categoryCost = new Category { Name = "Egresos", Type = CategoryType.Cost, Status = CategoryStatus.Active, CreationDate = DateTime.Today.AddDays(-59) };
            Category categoryIncome = new Category { Name = "Ingresos", Type = CategoryType.Income, Status = CategoryStatus.Active, CreationDate = DateTime.Today.AddDays(-7) };

            Transaction firstTransaction = new Transaction { Title = "Gasto panaderia", Amount = 100, Currency = CurrencyType.UYU, Category = categoryCost, Account = account, CreationDate = DateTime.Today.AddMonths(-2) };
            Transaction secondTransaction = new Transaction { Title = "Gasto super", Amount = 200, Currency = CurrencyType.UYU, Category = categoryCost, Account = account, CreationDate = DateTime.Today.AddMonths(-3) };
            Transaction thirdTransaction = new Transaction { Title = "Gasto super", Amount = 200, Currency = CurrencyType.UYU, Category = categoryCost, Account = account, CreationDate = DateTime.Today };

            Transaction fourthRransaction = new Transaction { Title = "Venta remera", Amount = 600, Currency = CurrencyType.UYU, Category = categoryIncome, Account = account, CreationDate = DateTime.Today };

            transactionList = new List<Transaction> { thirdTransaction };



            account.Transactions.Add(firstTransaction);
            account.Transactions.Add(secondTransaction);
            account.Transactions.Add(thirdTransaction);
            account.Transactions.Add(fourthRransaction);


            workSpace.Accounts.Add(account);
            
            creditCardReport = new CreditCardReport { WorkSpace = workSpace };
        }

        [TestMethod]
        public void ShowCostsOfMonth() 
        {
            List<Transaction> costsOfMonth = creditCardReport.CalculateCreditCardReport(account);

            CollectionAssert.AreEqual(costsOfMonth, transactionList);
        }
    }
}
