using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace DMS.Extensions
{
    public static class ViewExtensions
    {
        public static string ConvertToString(this PartialViewResult partialView,
                                                  ControllerContext controllerContext)
        {
            using (var sw = new StringWriter())
            {
                partialView.View = ViewEngines.Engines
                  .FindPartialView(controllerContext, partialView.ViewName).View;

                var vc = new ViewContext(
                  controllerContext, partialView.View, partialView.ViewData, partialView.TempData, sw);
                partialView.View.Render(vc, sw);

                var partialViewString = sw.GetStringBuilder().ToString();

                return partialViewString;
            }
        }
        //public static string RenderPartialToString(string controlName, object viewData)
        //{
        //    ViewPage viewPage = new ViewPage() { ViewContext = new ViewContext() };

        //    viewPage.ViewData = new ViewDataDictionary(viewData);
        //    viewPage.Controls.Add(viewPage.LoadControl(controlName));

        //    StringBuilder sb = new StringBuilder();
        //    using (StringWriter sw = new StringWriter(sb))
        //    {
        //        using (HtmlTextWriter tw = new HtmlTextWriter(sw))
        //        {
        //            viewPage.RenderControl(tw);
        //        }
        //    }

        //    return sb.ToString();
        //}
    }
}