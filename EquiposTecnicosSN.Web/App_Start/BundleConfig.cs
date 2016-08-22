using System.Web;
using System.Web.Optimization;

namespace EquiposTecnicosSN.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/myBundle").Include(
                        "~/Scripts/sb-admin-2.js",
                        "~/Scripts/autocomplete.js",
                        "~/Scripts/ddls-info-hardware.js",
                        "~/Scripts/ddl-traslado.js",
                        "~/Scripts/modal-detalle-solicitud.js",
                        "~/Scripts/start-home.js",
                        "~/Scripts/add-gasto.js",
                        "~/Scripts/close-solicitud.js",
                        "~/Scripts/tooltips.js",
                        "~/Scripts/zingchart.min.js",
                        "~/Scripts/indicadores.js"));

            bundles.Add(new ScriptBundle("~/bundles/zingCharts").Include(
                        "~/Scripts/zingchart.min.js",
                        "~/Scripts/zingmodules/zingchart-pareto.min.js",
                        "~/Scripts/zingmodules/zingchart-grid.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/datepicker").Include(
                        "~/Scripts/bootstrap-datepicker*",
                        "~/Scripts/locales/bootstrap-datepicker.es.min.js",
                        "~/Scripts/date-picker-ready.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/bootstrap-switch.js",
                      "~/Scripts/bootstrap-filestyle.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/timeline.css",
                        "~/Content/bootstrap*",
                        "~/Content/site.css",
                        "~/Content/themes/base/all.css",
                        "~/Content/bootstrap-switch.css",
                        "~/Content/sb-admin-2.css"));

            bundles.Add(new StyleBundle("~/lib/bower_components/css").Include(
                        "~/lib/bower_components/font-awesome/css/font-awesome.css",
                        "~/lib/bower_components/metisMenu/dist/metisMenu.css"//,
//                         "~/lib/bower_components/datatables/media/js/dataTables.bootstrap.css",
//                         "~/lib/bower_components/datatables/media/js/jquery.dataTables.css"
                         ));

            bundles.Add(new ScriptBundle("~/lib/bower_components").Include(
                        "~/lib/bower_components/metisMenu/dist/metisMenu.js",
                        "~/lib/bower_components/datatables/media/js/jquery.dataTables.js"//,
//                        "~/lib/bower_components/datatables/media/js/dataTables.bootstrap.js"
                        ));

        }
    }
}
