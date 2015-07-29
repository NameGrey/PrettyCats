using System;
using System.Collections.Generic;
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
	}
}
