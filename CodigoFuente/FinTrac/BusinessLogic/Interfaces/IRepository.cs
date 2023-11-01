using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
	public interface IRepository
	{
		public void Create<T>(T entity) where T : class;
		public void Delete<T>(T entity) where T : class;
		public void Update<T>(T entity) where T : class;
		public T Get<T>(int id) where T : class;
	}
}
