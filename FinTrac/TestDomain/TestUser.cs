using System.Runtime.Serialization;
using Domain;
using Domain.Excepciones;

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

        //[TestMethod]
        //public void CreateUser()
        //{
        //    User u2 = new User();
        //}

        //[TestMethod]
        //public void CreateUserAlreadyExists()
        //{
        //}

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
        [ExpectedException(typeof(EmptyFieldException))]
        public void ModifyEmptyNameException()
        {
            String newName = "";
            user.Name = newName;

        }
        [TestMethod]
        [ExpectedException(typeof(EmptyFieldException))]
        public void ModifyEmptyEmailException()
        {
            String newMail = "";
            user.Email = newMail;
        }

        [TestMethod]
        [ExpectedException(typeof(EmptyFieldException))]
        public void ModifyEmptyPasswordException()
        {
            String newPassword = "";
            user.Password = newPassword;
        }

        [TestMethod]
        public void SetUserPasswordTwelveCharacters()
        {
            user.Password = "123456789123";
            Assert.AreEqual(12, user.Password.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SetUserPasswordEightCharacters()
        {
            user.Password = "12345678";
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SetUserPasswordTwentyFiveCharacters()
        {
            user.Password = "1234567891234567891234567";
        }
    }
}
