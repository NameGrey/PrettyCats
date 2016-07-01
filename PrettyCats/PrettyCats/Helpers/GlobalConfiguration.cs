
namespace PrettyCats.Helpers
{
	public static class GlobalAppConfiguration
	{
#if DEBUG
		public const string BaseServerUrl = "http://localhost:53820/";
#else
		public const string BaseServerUrl = "http://artduviks.ru/";
#endif

	}
}