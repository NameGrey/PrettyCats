using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrettyCats.Helpers
{
	public static class SessionHelper
	{
		public static void CreateOrUpdateSessionObject(string objName, object objValue)
		{
			if (HttpContext.Current.Session[objName] != null)
				HttpContext.Current.Session.Add(objName, objValue);
			else
				HttpContext.Current.Session[objName] = objValue;
		}

		public static bool DeleteSessionObject(string objName)
		{
			bool result = false;

			if (IsObjectExists(objName))
			{
				HttpContext.Current.Session.Remove(objName);
				result = true;
			}

			return result;
		}

		public static bool IsObjectExists(string objName)
		{
			return HttpContext.Current.Session[objName] != null;
		}
	}
}