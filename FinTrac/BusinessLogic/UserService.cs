using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Domain;
namespace BusinessLogic
{
	public class UserService
	{
		private readonly MemoryDatabase _memoryDatabase;

		public UserService(MemoryDatabase memoryDatabase)
		{
			this._memoryDatabase = memoryDatabase;
		}	

		public void Add(User u)
		{

		}
	}
}
