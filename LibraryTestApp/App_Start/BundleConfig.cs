using System.Web;
using System.Web.Optimization;

namespace LibraryTestApp
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));
            
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/*.css"));

            bundles.Add(new StyleBundle("~/Content/DataTables").Include(
                "~/Content/DataTables/*.css"));

            bundles.Add(new ScriptBundle("~/bundles/datatable").Include(
                "~/Content/DataTables/*.js"));

            bundles.Add(new ScriptBundle("~/bundles/custom").Include(
                "~/Scripts/CustomScripts/*.js"));

            bundles.Add(new ScriptBundle("~/bundles/chosenjs").Include(
                "~/Scripts/chosen_v1.8.3/chosen.jquery.min.js"));

            bundles.Add(new StyleBundle("~/Content/Chosen").Include(
                "~/Scripts/chosen_v1.8.3/chosen.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                "~/Scripts/jquery-ui-{version}.js"));
            
            bundles.Add(new StyleBundle("~/Content/cssjqueryui").Include(
                "~/Content/themes/base/jquery-ui.css"));
        }
    }
}
