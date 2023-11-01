using Domain;
using BusinessLogic;
using Domain.Exceptions;

namespace TestBusinessLogic
{
	[TestClass]
	public class TestUserService
	{
		private UserService _service;
		[TestInitialize]
		public void SetUp()
		{
			_service = new UserService(new MemoryDatabase());
			
		}
		[TestMethod]
		public void AddUser()
		{
			var user = new User { Email = "test@test.com", Name = "Name", LastName = "LastName" };
			_service.Add(user);
			Assert.AreEqual(user, _service.Get(user.Email));

		}

		[TestMethod]
		[ExpectedException(typeof(UserAlreadyExistsException))]
		public void AddUserAlreadyExists()
		{
			var user = new User { Email = "test@test.com", Name = "Name", LastName = "LastName" };
			_service.Add(user);
			_service.Add(user);
		}


		[TestMethod]
		public void GetUser()
		{
			string email = "test@test.com";
			var user = new User { Email = "test@test.com", Name = "Name", LastName = "LastName" };
			_service.Add(user);
			Assert.AreEqual(email, _service.Get(email).Email);
		}

		[TestMethod]
		public void DeleteUser()
		{
			string email = "test@test.com";
			var user = new User { Email = "test@test.com", Name = "Name", LastName = "LastName" };
			_service.Add(user);

			_service.DeleteUser(user);
			Assert.IsNull(_service.Get(email));
		}

		[TestMethod]
		public void ValidUserAndPassword()
		{
            var user = new User { Email = "test@test.com", Name = "Name", LastName = "LastName", Password="1234567890"};
			string email = "test@test.com";
			string password = "1234567890";
			_service.Add(user);			
			Assert.IsTrue(_service.Login(email, password));
        }

        [TestMethod]
		[ExpectedException(typeof(InvalidUserException))]
        public void InvalidUserAndPassword()
        {
            var user = new User { Email = "test@test.com", Name = "Name", LastName = "LastName", Password = "1234567890" };
            string email = "test@test.com";
            string password = "12341234";
            _service.Add(user);
			_service.Login(email, password);
        }
    }
}