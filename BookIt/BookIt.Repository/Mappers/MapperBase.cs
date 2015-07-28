using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookIt.Repository.Mappers
{
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T">BLL object</typeparam>
	/// <typeparam name="E">database object</typeparam>
	public abstract class MapperBase<T, E>
	{
		public abstract T Map(E dbEntity);
		public abstract void UnMap(T  bllEntity, E dbEntity);

		public ICollection<T> MapAll(IEnumerable<E> dbEntities)
		{
			ICollection<T> collection = new Collection<T>();

			if (dbEntities != null)
			{
				foreach (E entity in dbEntities)
				{
					collection.Add(Map(entity));
				}
			}
			return collection;
		}

	
		
	}
}

