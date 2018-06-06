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
                .Include("~/Content/toastr/css/toastr.min.css")
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
                "~/Content/toastr/js/toastr.min.js",
                "~/Content/customer/common.js",
                "~/Views/Home/index.js"
                ));


            bundles.Add(new StyleBundle("~/Bundles/frame/css")
                .Include("~/Content/bootstrap-3.3.7-dist/css/bootstrap.min.css", new CssRewriteUrlTransform())
                .Include("~/Content/bootstrap-table/bootstrap-table.min.css")
                .Include("~/Content/bootstrap-datetimepicker/css/bootstrap-datetimepicker.min.css")
                .Include("~/Content/bootstrap-iconpicker/css/bootstrap-iconpicker.min.css")
                .Include("~/Content/select2/css/select2.min.css")
                .Include("~/Content/toastr/css/toastr.min.css")
                .Include("~/Content/zTree/css/metroStyle/metroStyle.css")
                .Include("~/Content/customerMain.css")
                .Include("~/Content/customer/loader.css")
                .Include("~/Content/Font-Awesome-3.2.1/css/font-awesome.min.css", new CssRewriteUrlTransform())
                //.Include("~/Content/customer/loader.css", new CssRewriteUrlTransform())
                );
            #region 分类
            string[] jqueryValidate = {
                "~/Content/jquery-validate/jquery.validate.min.js",
                "~/Content/jquery-validate/jquery.validate.unobtrusive.min.js",
                "~/Content/jquery-validate/jquery.unobtrusive-ajax.min.js"
            };
            string[] bootstrapDatetimepicker = {
                "~/Content/bootstrap-datetimepicker/js/bootstrap-datetimepicker.min.js",
                "~/Content/bootstrap-datetimepicker/js/locales/bootstrap-datetimepicker.zh-CN.js"
            };
            string[] bootstrapIconpicker = {
                "~/Content/bootstrap-iconpicker/js/bootstrap-iconpicker-iconset-all.min.js",
                "~/Content/bootstrap-iconpicker/js/bootstrap-iconpicker.min.js"
            };
            string[] bootstrapTable = {
                "~/Content/bootstrap-table/bootstrap-table.js",
                "~/Content/bootstrap-table/locale/bootstrap-table-zh-CN.min.js"
            };
            string[] bootstrapTableExport = {
                "~/Content/bootstrap-table/extensions/bootstrap-table-export.js",
                "~/Content/tableExport.jquery.plugin/libs/FileSaver/FileSaver.min.js",
                "~/Content/tableExport.jquery.plugin/tableExport.min.js"
            };
            string[] select2 = {
                "~/Content/select2/js/select2.full.js",
                "~/Content/select2/js/i18n/zh-CN.js"
            };
            string[] knockout = {
                "~/Content/knockout/knockout-3.4.2.js",
                "~/Content/knockout/extensions/knockout.mapping-latest.js"
            };
            #endregion
            bundles.Add(new ScriptBundle("~/Bundles/frame/js").Include(
                "~/Content/jquery/jquery-3.3.1.min.js",
                "~/Content/bootstrap-3.3.7-dist/js/bootstrap.min.js",
                "~/Content/toastr/js/toastr.min.js",
                "~/Content/bootbox/bootbox.min.js",
                "~/Content/zTree/js/jquery.ztree.all.min.js"
                )
                .Include(jqueryValidate)
                .Include(bootstrapDatetimepicker)
                .Include(bootstrapIconpicker)
                .Include(bootstrapTable)
                .Include(bootstrapTableExport)
                .Include(select2)
                .Include(knockout)
                .Include(
                "~/Content/customer/dataInit.js",
                "~/Content/customer/common.js",
                "~/Content/customer/frame-common.js",
                "~/Content/bootstrap-table/extensions/bootstraptable.formatter.js",
                "~/Content/customer/knockout.bootstraptable.js"
                ));

#if DEBUG
#else
            BundleTable.EnableOptimizations = true;  //是否打包压缩
#endif
        }
    }
}