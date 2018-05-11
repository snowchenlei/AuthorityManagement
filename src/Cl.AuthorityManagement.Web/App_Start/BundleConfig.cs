using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Cl.AuthorityManagement.Web.App_Start
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Bundles/home/css")
                .Include("~/Content/Font-Awesome-3.2.1/css/font-awesome.min.css", new CssRewriteUrlTransform())
                .Include("~/Content/ace/font-awesome/4.5.0/css/font-awesome.min.css", new CssRewriteUrlTransform())
                .Include("~/Content/ace/css/fonts.googleapis.com.css", new CssRewriteUrlTransform())
                .Include("~/Content/ace/css/bootstrap.min.css", new CssRewriteUrlTransform())
                .Include("~/Content/ace/css/ace.min.css", new CssRewriteUrlTransform())
                .Include("~/Content/ace/css/ace-skins.min.css", new CssRewriteUrlTransform())
                .Include("~/Content/ace/css/ace-rtl.min.css", new CssRewriteUrlTransform())
                );
            bundles.Add(new ScriptBundle("~/Bundles/home/js").Include(
                "~/Content/ace/js/ace-extra.min.js",
                "~/Content/ace/js/jquery-2.1.4.min.js",
                "~/Content/ace/js/bootstrap.min.js",
                "~/Content/ace/js/jquery-ui.custom.min.js",
                "~/Content/ace/js/jquery.ui.touch-punch.min.js",
                "~/Content/ace/js/jquery.easypiechart.min.js",
                "~/Content/ace/js/jquery.sparkline.index.min.js",
                "~/Content/ace/js/ace-elements.min.js",
                "~/Content/ace/js/ace.min.js",
                "~/Content/fullscreen/jquery.fullscreen.js",
                "~/Content/customer/bootstrap-tab.js",
                "~/Content/customer/common.js",
                "~/Views/Home/index.js"
                ));

#if DEBUG
#else
            BundleTable.EnableOptimizations = true;  //是否打包压缩
#endif
        }
    }
}