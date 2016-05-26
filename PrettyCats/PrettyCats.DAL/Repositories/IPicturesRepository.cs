using PrettyCats.DAL.Entities;

namespace PrettyCats.DAL.Repositories
{
	public interface IPicturesRepository: IRepository<Pictures>
	{
		string GetNewNumberOfImage(string kittenName, bool small = false);

		void SetNewOrderForPicture(int id, int newOrder);
	}
}
