using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookIt.Repository.Mappers
{
	public abstract class EnumMapperBase<T, E>
		where T : struct
		where E : struct, IConvertible
	{
	
		public abstract T Map(E dbEntity);
		public abstract E UnMap(T bllEntity);

		public ICollection<T> MapAll()
		{
			ICollection<T> collection = new Collection<T>();
			var enumItems = Enum.GetNames(typeof(E));
			foreach (string enumName in enumItems)
			{
				var enumItem = (E)Enum.Parse(typeof(E), enumName);
				collection.Add(Map(enumItem));
			}
			return collection;

		}
	}
}
