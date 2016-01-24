using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace PrettyCats.Helpers
{
	public static class SecurityHelper
	{
		private const string ADMIN_NAME = "Serg";
		private const string ADMIN_PASS = "pass43";

		public static bool LogInAdmin(string name, string password)
		{
			bool result = false;

			if (password == ADMIN_PASS && name == ADMIN_NAME)
			{
				FormsAuthentication.SetAuthCookie(name, true);
				result = true;
			}

			return result;
		}

		public static bool LogOutAdmin()
		{
			return SessionHelper.DeleteSessionObject(ADMIN_NAME);
		}
	}
}