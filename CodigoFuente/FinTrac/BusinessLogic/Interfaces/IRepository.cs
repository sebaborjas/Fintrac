using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
	public interface IRepository<T>
	{
		public void Create(T entity);

		public void Delete(T entity);
		public void Update(T entity);
		public T Get(int id);
	}
}
