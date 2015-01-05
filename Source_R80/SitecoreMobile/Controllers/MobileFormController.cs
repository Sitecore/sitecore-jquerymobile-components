using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SitecoreMobile.Controllers
{

    public abstract class MobileFormController : Controller
    {
        public abstract ActionResult AjaxFormFieldHandler(string formName, string formFieldName, string formFieldValue);
    }

    public class StandardMobileFormController : MobileFormController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult StandardFormFieldHandler(string formName, string formFieldName, string formFieldValue)
        {
            return null;

            // return new ContentResult() { Content = string.Empty, ContentEncoding = System.Text.Encoding.UTF8, ContentType = "application/text" };
        }

        public override ActionResult AjaxFormFieldHandler(string formName, string formFieldName, string formFieldValue)
        {
            return new ContentResult() { Content = formFieldValue, ContentEncoding = System.Text.Encoding.UTF8, ContentType = "application/text" };
        }

    }

    

    
}