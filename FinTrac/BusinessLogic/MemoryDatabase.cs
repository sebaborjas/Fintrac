using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Interfaces;

namespace BusinessLogic
{
	public class MemoryDatabase : IRepository
	{
		public void Add<T>(T entity) where T : class
		{
			throw new NotImplementedException();
		}

		public void Delete<T>(T entity) where T : class
		{
			throw new NotImplementedException();
		}

		public T Get<T>(int id) where T : class
		{
			throw new NotImplementedException();
		}

		public void Update<T>(T entity) where T : class
		{
			throw new NotImplementedException();
		}
	}	
	{
	}
}
