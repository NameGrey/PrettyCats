using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookIt.DAL;

namespace BookIt.Repository.Mappers
{
	public class CategoriesMapper:EnumMapperBase<BLL.Entities.CategoryTypes,Category>
	{
		public override BLL.Entities.CategoryTypes Map(Category dbCategory)
		{
			return (BLL.Entities.CategoryTypes)Enum.Parse(typeof(BLL.Entities.CategoryTypes), Enum.GetName(typeof(Category), dbCategory));
		}

		public override Category UnMap(BLL.Entities.CategoryTypes bllCategory)
		{
			return (Category)Enum.Parse(typeof(Category), Enum.GetName(typeof(BLL.Entities.CategoryTypes), bllCategory));
		}

	}
}
