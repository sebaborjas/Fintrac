using Domain;
namespace TestDomain
{
    [TestClass]
    public class TestUser
    {
        User user;

        [TestInitialize]
        public void Setup()
        {
            user = new User();
        }

        [TestMethod]
        public void CreateUser()
        {
            User u2 = new User();
            Assert.AreEqual(u2, u2);
        }

        [TestMethod]
        // CRUD method?
        public void CreateUserAlreadyExists()
        {
        }

        [TestMethod]
        public void ModifyName()
        {
            user.Name = "Test";
            Assert.AreEqual("Test", user.Name);
        }

        [TestMethod]
        public void ModifyLastName()
        {
            user.LastName = "Marotta";
            Assert.AreEqual("Marotta", user.LastName);
        }

        [TestMethod]
        public void ModifyAddress()
        {
            user.Address = "New Address";
            Assert.AreEqual("New Address", user.Address);

        }

        [TestMethod]
        public void ModifyEmail()
        {
            user.Email = "New Email";
            Assert.AreEqual("New Email", user.Email);
        }

        [TestMethod]
        public void ModifyPassword()
        {
            user.Password = "New Password";
            Assert.AreEqual("New Password", user.Password);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ModifyEmptyNameException()
        {
            String newName = "";
            user.Name = newName;

        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ModifyEmptyEmailException()
        {
            String newMail = "";
            user.Email = newMail;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ModifyEmptyPasswordException()
        {
            String newPassword = "";
            user.Password = newPassword;
        }

        [TestMethod]
        public void PasswordBetweenTenAndTwentyCharacters()
        {
            bool correctLength = user.Password.Length > 9 && user.Password.Length < 20;
            Assert.IsTrue(correctLength);
        }
    }
}
