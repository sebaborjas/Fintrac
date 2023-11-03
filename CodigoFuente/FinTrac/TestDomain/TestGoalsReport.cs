using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Domain;
using Domain.DataTypes;
using Domain.Exceptions;

namespace TestDomain
{
    [TestClass]
    public class TestGoalsReport
    {
        private GoalsReport goalsReport;
        private Workspace workSpace;
        private List<Transaction> transactionList;
        private List<Transaction> transactionListUSD;
        private Account account;
        private Account accountUSD;
        private Goal goal;

        [TestInitialize]
        public void SetUp()
        {
            User user = new User { Email = "test@test.com", Name = "Test", LastName = "Test", Password = "1234567890" };

            workSpace = new Workspace(user, "TestWorkspace");

            user.WorkspaceList.Add(workSpace);
            Exchange exchange = new Exchange { Date = DateTime.Today.AddDays(-5), DollarValue = 35 };
            workSpace.ExchangeList.Add(exchange);

            account = new PersonalAccount { Name = "TestPersonalAccount", Currency = CurrencyType.UYU, WorkSpace = workSpace, StartingAmount = 20000 };
            accountUSD = new PersonalAccount { Name = "TestPersonalAccountUSD", Currency = CurrencyType.USD, WorkSpace = workSpace, StartingAmount = 20000 };

            Category category = new Category { Name = "Cosas personales", Status = CategoryStatus.Active, Type = CategoryType.Cost, Workspace = workSpace };
            Category categoryHome = new Category { Name = "Hogar", Status = CategoryStatus.Active, Type = CategoryType.Cost, Workspace = workSpace };

            transactionList = new List<Transaction>();

            Transaction firstTransaction = new Transaction { Title = "Gasto 1", Amount = 2000, Account = account, Category = category, Currency = CurrencyType.UYU };
            Transaction secondTransaction = new Transaction { Title = "Gasto 2", Amount = 1000, Account = account, Category = category, Currency = CurrencyType.UYU };
            Transaction thirdTransaction = new Transaction { Title = "Gasto 3", Amount = 1000, Account = account, Category = category, Currency = CurrencyType.UYU };

            transactionList.Add(firstTransaction);
            transactionList.Add(secondTransaction);
            transactionList.Add(thirdTransaction);

            account.TransactionList = transactionList;

            workSpace.AccountList.Add(account);

            transactionListUSD = new List<Transaction>();

            Transaction firstTransactionUSD = new Transaction { Title = "Gasto 1", Amount = 20, Currency = CurrencyType.USD, Account = accountUSD, Category = categoryHome };
            Transaction secondTransactionUSD = new Transaction { Title = "Gasto 2", Amount = 35, Currency = CurrencyType.USD, Account = accountUSD, Category = categoryHome, };
            Transaction thirdTransactionUSD = new Transaction { Title = "Gasto 3", Amount = 40, Currency = CurrencyType.USD, Account = accountUSD, Category = categoryHome };

            transactionListUSD.Add(firstTransactionUSD);
            transactionListUSD.Add(secondTransactionUSD);
            transactionListUSD.Add(thirdTransactionUSD);

            accountUSD.TransactionList = transactionListUSD;

            workSpace.AccountList.Add(accountUSD);

            goal = new Goal { Title = "Ahorro", MaxAmount = 10000, Workspace = workSpace };
            goal.Categories.Add(category);
            goal.Categories.Add(categoryHome);

            goalsReport = new GoalsReport { Currency = CurrencyType.UYU, WorkSpace = workSpace, Goal = goal };
        }

        [TestMethod]
        public void GoalAchieved()
        {
            goalsReport.CalculateReport();
            double expectedAmount = 4000 + 3325;
            Assert.IsTrue(goalsReport.GoalAchieved);
            Assert.AreEqual(expectedAmount, goalsReport.AmountSpent);
        }

        [TestMethod]
        public void GoalNotAchieved()
        {
            goal.MaxAmount = 5000;
            goalsReport.Goal = goal;
            goalsReport.CalculateReport();
            double expectedAmount = 4000 + 3325;
            Assert.IsFalse(goalsReport.GoalAchieved);
            Assert.AreEqual(expectedAmount, goalsReport.AmountSpent);
        }


    }
}
