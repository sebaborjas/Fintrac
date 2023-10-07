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
		[ExpectedException(typeof(UserAlreadyExistsException))]
		public void UpdateEmailAlreadyExists() 
		{
			
			var user = new User { Email = "test@test.com", Name = "Name", LastName = "LastName" };
			_service.Add(user);

			var userTwo = new User { Email = "second@test.com", Name = "Name", LastName = "LastName" };
			_service.Add(userTwo);

			string userEmail = user.Email;

			_service.UpdateEmail(userTwo, userEmail);

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
			Assert.AreEqual(null, _service.Get(email));
		}
	}
}