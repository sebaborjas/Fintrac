using Domain;
using BusinessLogic;
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
			var user = new User { Email = "test@test.com" ,Name = "Name", LastName = "LastName"};
			_service.Add(user);
			Assert.AreEqual(1, _service.GetAll().Count());

		}
	}
}