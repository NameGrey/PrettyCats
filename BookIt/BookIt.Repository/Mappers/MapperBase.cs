using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookIt.Repository.Mappers
{
	public abstract class MapperBase<T, E>
	{
		public abstract T Map(E entity);

		public ICollection<T> MapAll(IEnumerable<E> entities)
		{
			ICollection<T> collection = new Collection<T>();

			if (entities != null)
			{
				foreach (E entity in entities)
				{
					collection.Add(Map(entity));
				}
			}
			return collection;
		}
	}
}

