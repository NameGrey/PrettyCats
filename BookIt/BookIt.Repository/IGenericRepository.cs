using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BookIt.Repository
{
	public interface IGenericRepository<TEntity> where TEntity : class
	{
		IEnumerable<TEntity> Get();

		TEntity GetByID(object id);
		void Insert(TEntity entity);
		void Delete(object id);
		void Delete(TEntity entityToDelete);
		void Update(TEntity entityToUpdate);
	}
}