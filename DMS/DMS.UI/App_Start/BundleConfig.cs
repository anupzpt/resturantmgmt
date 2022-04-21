﻿using System.Web.Optimization;

namespace DMS.UI
{
    using System.Web;
    using System.Web.Optimization;

        public class BundleConfig
        {
            // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
            public static void RegisterBundles(BundleCollection bundles)
            {
                bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                            "~/Scripts/jquery-{version}.js",
                            "~/Scripts/DataTables/jquery.dataTables.js",
                            "~/Scripts/nepali.datepicker.v2.2/nepali.datepicker.v2.2.min.js",
                            "~/Scripts/IconPicker/bootstrap-iconpicker-iconset-all.min.js",
                            "~/Scripts/IconPicker/bootstrap-iconpicker.min.js",
                            "~/Scripts/jquery.unobtrusive-ajax.js",
                            "~/Scripts/nepaliNumberToWord.js"
                              ));

                bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                            "~/Scripts/jquery.validate*"));

                // Use the development version of Modernizr to develop with and learn from. Then, when you're
                // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
                bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                            "~/Scripts/modernizr-*"));

                bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                          "~/Scripts/bootstrap.js",
                          "~/Scripts/respond.js",
                          "~/Scripts/select2/js/select2.min.js"
                          ));

                bundles.Add(new StyleBundle("~/Content/css").Include(
                          "~/Content/bootstrap.css",
                          "~/Content/site.css",
                          "~/Scripts/select2/css/select2.min.css",
                          "~/Scripts/nepali.datepicker.v2.2/nepali.datepicker.v2.2.min.css",
                          "~/Content/DataTables/css/jquery.dataTables.css",
                          "~/Content/fontawesome/css/font-awesome.min.css"

                          ));
            }
        }
    }

