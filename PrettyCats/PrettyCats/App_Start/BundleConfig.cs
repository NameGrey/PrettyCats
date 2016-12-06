using System.Web.Optimization;

namespace PrettyCats
{
	public class BundleConfig
	{
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
						"~/Scripts/jquery/jquery-2.1.4.min.js",
						"~/Scripts/jquery/jquery.unobtrusive - ajax.min.js"));

			bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
					  "~/Scripts/bootstrap/bootstrap.js"));

			//TODO: need to add all the scripts from old version
			bundles.Add(new ScriptBundle("~/bundles/kittenMainPageScripts").Include(
					  "~/Scripts/Slider/jssor.slider.mini.js",
					  "~/Scripts/Slider/jssor.slider.min.js",
					  "~/Scripts/Slider/slideshow.js",
					  "~/Scripts/flowtype.js",
					  "~/Scripts/kittenMainPage.js",
					  "~/Scripts/DialogForm.js"));
			//TODO: need to add all the css from old version
			//bundles.Add(new StyleBundle("~/bundles/kittenMainPageContent").Include(
			//		  "~/Scripts/respond.js",
			//		  "~/Scripts/site.js"));

			bundles.Add(new StyleBundle("~/bundles/appContent").Include(
					  "~/Content/bootstrap/bootstrap.min.css",
					  "~/Content/app/site.css",
					  "~/Content/app/DialogForm.css",
					  "~/Content/SlideShow.css"));

			BundleTable.EnableOptimizations = false;
		}
	}
}