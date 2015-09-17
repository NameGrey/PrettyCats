using System.Web.Optimization;

namespace PrettyCats
{
	public class BundleConfig
	{
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
						"~/Scripts/jquery-1.9.1.js",
						"~/Scripts/jquery-1.9.1.min.js"));

			bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
						"~/Scripts/jquery.validate*"));

			bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
					  "~/Scripts/bootstrap.js",
					  "~/Scripts/respond.js"));

			bundles.Add(new StyleBundle("~/Content/css").Include(
					  "~/Content/bootstrap.css",
					  "~/Content/site.css",
					  "~/Content/SlideShow.css"));

			bundles.Add(new StyleBundle("~/Content/js").Include(
					  "~/Content/SlideShow.js"));

			BundleTable.EnableOptimizations = true;
		}
	}
}