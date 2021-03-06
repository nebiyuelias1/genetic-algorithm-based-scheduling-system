﻿using System.Web;
using System.Web.Optimization;

namespace SchedulingSystemWeb
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/lib").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/bootbox.js",
                        "~/Scripts/respond.js",
                        "~/scripts/datatables/jquery.datatables.js",
                        "~/scripts/datatables/datatables.bootstrap.js",
                        "~/scripts/spectrum.js",
                        "~/scripts/moment.js",
                        "~/scripts/fullcalendar/fullcalendar.js", 
                        "~/scripts/fullcalendar/scheduler.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/bootstrap.css",
                        "~/content/datatables/css/datatables.bootstrap.css",
                        "~/Content/site.css",
                        "~/Content/spectrum.css",
                        "~/Content/fullcalendar/fullcalendar.css",
                        "~/Content/fullcalendar/scheduler.css",
                        "~/Content/font-awesome.css"));
        }
    }
}
