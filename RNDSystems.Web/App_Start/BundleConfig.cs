using System.Web.Optimization;

namespace RNDSystems.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));


            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/fuelux.min.js",
                      "~/Scripts/bootstrapValidator.min.js",
                      "~/Scripts/bootbox.min.js",
                      "~/Scripts/bootstrap-select.min.js",
                      "~/Scripts/bootstrap-datepicker.min.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/fuelux.min.css",
                      "~/Content/bootstrapValidator.min.css",
                      "~/Content/bootstrap-select.min.css",
                      "~/Content/bootstrap-datepicker.min.css",
                      "~/Content/navmenu.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/Utilities").Include(
                      "~/Scripts/Utilities.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/GridUtil").Include(
                      "~/Scripts/GridUtil.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/UserLogins").Include(
                      "~/Scripts/UserLogins.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/SecuityConfig").Include(
                      "~/Scripts/SecuityConfig.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/WorkStudyList").Include(
                      "~/Scripts/pages/workstudylist.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/SaveWorkStudy").Include(
                      "~/Scripts/pages/saveworkstudy.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/AssignMaterial").Include(
                      "~/Scripts/pages/AssignMeterial.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/SaveAssignMaterial").Include(
                      "~/Scripts/pages/SaveAssignMaterial.js"
                      ));

            BundleTable.EnableOptimizations = false;
            bundles.Add(new ScriptBundle("~/bundles/RegisteredUserList").Include(
                      "~/Scripts/pages/RegisteredUserList.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/SaveRegisteredUser").Include(
                      "~/Scripts/pages/SaveRegisteredUser.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/ProcessingMaterial").Include(
                      "~/Scripts/pages/ProcessingMaterial.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/SaveProcessingMaterial").Include(
                      "~/Scripts/pages/SaveProcessingMaterial.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/TestingMaterial").Include(
                     "~/Scripts/pages/TestingMaterial.js"
                     ));

            bundles.Add(new ScriptBundle("~/bundles/SaveTestingMaterial").Include(
                      "~/Scripts/pages/SaveTestingMaterial.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/ImportData").Include(
                     "~/Scripts/pages/ImportData.js"
                     ));

            bundles.Add(new ScriptBundle("~/bundles/Reports").Include(
                      "~/Scripts/pages/Reports.js"
                      ));
            BundleTable.EnableOptimizations = false;
        }
    }
}
