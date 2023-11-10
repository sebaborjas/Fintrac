using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Transactions;
using Domain;
using Domain.Exceptions;

namespace TestDomain
{
    [TestClass]
    public class TestGoal
    {
        Goal goal;

        [TestInitialize]
        public void Setup()
        {
            goal = new Goal();
        }

        public void EqualsGoals()
        {
            goal.Title = "Test";
            Goal otherGoal = new Goal { Title = "Test" };

            Assert.AreEqual(goal, otherGoal);
        }

        [TestMethod]
        public void NotEqualsGoals()
        {
            goal.Title = "Test";
            Goal otherGoal = new Goal { Title = "Night" };

            Assert.AreNotEqual(goal, otherGoal);
        }

        [TestMethod]
        public void AddTitle()
        {
            goal.Title = "Test";
            Assert.AreEqual("Test", goal.Title);
        }

        [TestMethod]
        [ExpectedException(typeof(EmptyFieldException))]
        public void EmptyTitleException()
        {
            goal.Title = "";
        }

        [TestMethod]
        public void AddMaxAmount()
        {
            goal.MaxAmount = 1000;
            Assert.AreEqual(1000, goal.MaxAmount);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidMaxAmountException()
        {
            goal.MaxAmount = 0;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NegativeMaxAmountException()
        {
            goal.MaxAmount = -1;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestWorkSpaceNull()
        {
            goal.Workspace = null;
        }

        [TestMethod]
        public void TestWorkSpace()
        {
            User admin = new User
            {
                Name = "Test",
                LastName = "LastName",
                Address = "",
                Password = "1234567891",
                Email = "test@test.com"
            };
            Workspace workspace = new Workspace{ UserAdmin = admin, Name = $"Espacio personal de {admin.Name} {admin.LastName}" };
            goal.Workspace = workspace;
            Assert.AreEqual(workspace, goal.Workspace);
        }

        [TestMethod]
        public void TestCategoriesNotNull()
        {
            Assert.IsNotNull(goal.Categories);
        }

        [TestMethod]
        public void GoalEmptyListCategories()
        {
            Assert.IsTrue(goal.Categories.Count == 0);
        }

    }
}
