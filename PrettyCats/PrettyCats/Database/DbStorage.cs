
namespace PrettyCats.Database
{
    public class DbStorage
    {
        public static Storage Instance { get; private set; }

        private DbStorage()
        {

        }

        static DbStorage()
        {
            Instance = new Storage();
        }
    }
}