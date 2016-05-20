using System.Collections.Generic;
using PrettyCats.DAL.Enteties;

namespace PrettyCats.DAL.Repositories
{
	public interface IKittensRepository: IRepository<Pets>
	{
		Pets GetKittenByName(string name);

		IEnumerable<Pets> GetKittensByBreed(int breedId, bool fromArhive = false);

		bool IsKittenExistsWithAnotherId(Pets kitten);

		bool IsKittenExists(Pets kitten);

		bool IsKittenExistsWithParent(Pets parent);
		void AddPictureForTheKitten(string kittenName, Pictures picture);
	}
}
