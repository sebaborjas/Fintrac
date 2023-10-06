using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Domain;
using Domain.Exceptions;

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
		public void EqualsUsers()
        {
            user.Email = "a@a.com";
            User otherUser = new User { Email = "a@a.com"};

            Assert.AreEqual(user, otherUser);
        }

		[TestMethod]
		public void NotEqualsUsers()
		{
			user.Email = "a@a.com";
			User otherUser = new User { Email = "b@a.com" };

			Assert.AreNotEqual(user, otherUser);
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
        [ExpectedException(typeof(EmptyFieldException))]
        public void EmptyLastNameException()
        {
            user.LastName = "";
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
            string validEmail = "email@email.com";

			user.Email = validEmail;
            Assert.AreEqual(validEmail, user.Email);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidEmailExcpetion()
        {
            user.Email = "email";
        }

        [TestMethod]
        public void ModifyPassword()
        {
            user.Password = "New Password";
            Assert.AreEqual("New Password", user.Password);
        }

        [TestMethod]
        [ExpectedException(typeof(EmptyFieldException))]
        public void EmptyNameException()
        {
            String newName = "";
            user.Name = newName;

        }
        [TestMethod]
        [ExpectedException(typeof(EmptyFieldException))]
        public void EmptyEmailException()
        {
            String newMail = "";
            user.Email = newMail;
        }

        [TestMethod]
        [ExpectedException(typeof(EmptyFieldException))]
        public void EmptyPasswordException()
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
        public void SetUserPasswordThirtyFiveCharacters()
        {
            user.Password = "1234567891234567891234567787678765";
        }
    }
}
