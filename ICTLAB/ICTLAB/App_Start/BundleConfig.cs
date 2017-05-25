using System.Web;
using System.Web.Optimization;

namespace ICTLAB
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/angularJS")
                .Include("~/Scripts/angular.js")
                .IncludeDirectory("~/app/", "*.js", true));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/highcharts")
                .Include("~/Scripts/highcharts.src.js"));

            bundles.Add(new ScriptBundle("~/bundles/threeJS")
                .Include("~/Scripts/Three.js/Three.js")
                .Include("~/Scripts/Three.js/ColladaLoader.js")
                .Include("~/Scripts/Three.js/OBJLoader.js"));

            bundles.Add(new StyleBundle("~/Content/css")
                .IncludeDirectory("~/Content", "*.css", true));
        }
    }
}
