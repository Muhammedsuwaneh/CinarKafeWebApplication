using System.Web;
using System.Web.Optimization;

namespace CinarKafe
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new StyleBundle("~/Content/css").Include(
                  "~/Content/bootstrap.css",
                  "~/Content/site.css",
                  "~/Content/typeahead.css",
                  "~/Content/toastr.css"
            ));

            bundles.Add(new StyleBundle("~/Fonts/css").Include(
                      "~/Fonts/font-awesome/css/all.css"
                ));
            bundles.Add(new ScriptBundle("~/bundles/lib").Include(
                        "~/Scripts/jquery-{version}.js",
                         "~/Scripts/bootstrap.js",
                         "~/scripts/bootbox.js",
                         "~/scripts/typeahead.bundle.js",
                         "~/scripts/toastr.js",
                         "~/scripts/popper.js",
                         "~/scripts/filternumbers.js",
                         "~/scripts/deleteGarson.js",
                         "~/scripts/deleteMasa.js",
                         "~/scripts/deleteUrun.js",
                         "~/scripts/siperisData.js",
                         "~/scripts/fetchOrderData.js",
                         "~/scripts/postSiperisData.js",
                         "~/scripts/OdemeBilgisi.js"
                         ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));
        }
    }
}
