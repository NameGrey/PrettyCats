using System.Web.Security;

namespace PrettyCats.Helpers
{
	public static class SecurityHelper
	{
		private const string AdminName = "Serg";
		private const string AdminPass = "pass43";

		public static bool LogInAdmin(string name, string password)
		{
			bool result = false;

			if (password == AdminPass && name == AdminName)
			{
				FormsAuthentication.SetAuthCookie(name, true);
				result = true;
			}

			return result;
		}

		public static bool LogOutAdmin()
		{
			return SessionHelper.DeleteSessionObject(AdminName);
		}
	}
}