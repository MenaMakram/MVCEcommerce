using System.Web;
using System.Web.Optimization;

namespace MVCEcommerce
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

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
            bundles.Add(new StyleBundle("~/Content/css1").Include(
                  "~/Content/css/bootstrap.min.css",
                  "~/Content/site1.css"));


            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                     "~/Scripts/js/popper.min.js", "~/Scripts/bootstrap.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css"));
            bundles.Add(new StyleBundle("~/Content/font").Include(
                  "~/Content/fonts/icomoon/style.css"
                ));
            bundles.Add(new StyleBundle("~/Content/mystyle").Include(

                      "~/Content/css/jquery-ui.css",
                      "~/Content/magnific-popup.css",
                      "~/Content/css/owl.carousel.min.css",
                      "~/Content/css/owl.theme.default.min.css",
                      "~/Content/css/aos.css",
                      "~/Content/site.css"
                ));
            bundles.Add(new ScriptBundle("~/Scripts/js").Include("~/Scripts/js/owl.carousel.min.js",

               "~/Scripts/jquery.magnific-popup.min.js", "~/Scripts/js/aos.js", "~/Scripts/js/main.js"));
        }
    }
}
