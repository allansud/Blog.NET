using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Blog.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/jquery").Include(
                "~/Scripts/jquery-1.8.2.min.js",
                "~/Scripts/jquery.flexslider-min.js"));

            bundles.Add(new StyleBundle("~/Content/knockout").Include(
                 "~/Content/style.css",
                 "~/Content/PagedList.css"));
        }
    }
}