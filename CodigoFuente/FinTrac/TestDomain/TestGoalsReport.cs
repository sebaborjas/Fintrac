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
        private List<Transactions> transactionList;
        private List<Transactions> transactionListUSD;
        private Account account;
        private Account accountUSD;
        private Goal goal;

        [TestInitialize]
        public void SetUp()
        {
            User user = new User { Email = "test@test.com", Name = "Test", LastName = "Test", Password = "1234567890" };

            workSpace = new Workspace
            {
                UserAdmin = user,
                Name = "TestWorkspace"
            };

            user.Workspaces.Add(workSpace);
            Exchange exchange = new Exchange { Currency = CurrencyType.USD ,Date = DateTime.Today.AddDays(-5), CurrencyValue = 35 };
            workSpace.Exchanges.Add(exchange);

            account = new PersonalAccount { Name = "TestPersonalAccount", Currency = CurrencyType.UYU, WorkSpace = workSpace, StartingAmount = 20000 };
            accountUSD = new PersonalAccount { Name = "TestPersonalAccountUSD", Currency = CurrencyType.USD, WorkSpace = workSpace, StartingAmount = 20000 };

            Category category = new Category { Name = "Cosas personales", Status = CategoryStatus.Active, Type = CategoryType.Cost, Workspace = workSpace };
            Category categoryHome = new Category { Name = "Hogar", Status = CategoryStatus.Active, Type = CategoryType.Cost, Workspace = workSpace };

            transactionList = new List<Transactions>();

            Transactions firstTransaction = new Transactions { Title = "Gasto 1", Amount = 2000, Account = account, Category = category, Currency = CurrencyType.UYU };
            Transactions secondTransaction = new Transactions { Title = "Gasto 2", Amount = 1000, Account = account, Category = category, Currency = CurrencyType.UYU };
            Transactions thirdTransaction = new Transactions { Title = "Gasto 3", Amount = 1000, Account = account, Category = category, Currency = CurrencyType.UYU };

            transactionList.Add(firstTransaction);
            transactionList.Add(secondTransaction);
            transactionList.Add(thirdTransaction);

            account.Transactions = transactionList;

            workSpace.Accounts.Add(account);

            transactionListUSD = new List<Transactions>();

            Transactions firstTransactionUSD = new Transactions { Title = "Gasto 1", Amount = 20, Currency = CurrencyType.USD, Account = accountUSD, Category = categoryHome };
            Transactions secondTransactionUSD = new Transactions { Title = "Gasto 2", Amount = 35, Currency = CurrencyType.USD, Account = accountUSD, Category = categoryHome, };
            Transactions thirdTransactionUSD = new Transactions { Title = "Gasto 3", Amount = 40, Currency = CurrencyType.USD, Account = accountUSD, Category = categoryHome };

            transactionListUSD.Add(firstTransactionUSD);
            transactionListUSD.Add(secondTransactionUSD);
            transactionListUSD.Add(thirdTransactionUSD);

            accountUSD.Transactions = transactionListUSD;

            workSpace.Accounts.Add(accountUSD);

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
