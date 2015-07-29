using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookIt.DAL;

namespace BookIt.Repository.Mappers
{
	public class CategoryMapper:EnumMapperBase<BLL.Category,Category>
	{
		public override BLL.Category Map(Category dbCategory)
		{
			return (BLL.Category)Enum.Parse(typeof(BLL.Category), Enum.GetName(typeof(Category), dbCategory));
		}

		public override Category UnMap(BLL.Category bllCategory)
		{
			return (Category)Enum.Parse(typeof(Category), Enum.GetName(typeof(BLL.Category), bllCategory));
		}
	}
}
