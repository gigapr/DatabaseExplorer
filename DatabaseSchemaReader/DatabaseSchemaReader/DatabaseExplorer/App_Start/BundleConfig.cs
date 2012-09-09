using System.Web.Optimization;

namespace DatabaseExplorer.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-1.*")
                                                            .Include("~/Scripts/knockout-2.1.0.debug.js")
                       );            

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));           
        }
    }
}