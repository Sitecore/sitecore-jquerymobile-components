using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SitecoreMobile.DemoContent.Controllers
{
    public class DemoMobileFormController : Controller
    {
        // GET: DemoForm
        public ActionResult Index()
        {
            return View();
        }

        // form handler controller actions
        // form handler actions runs first and can run validation and check/set route data for other controllers
        // form handlers should generally return null so that the standard sitecore controller can execute next
        public ActionResult SubmitFormHandler (string field1)
        {
            return null;
        }
        
        // form content controller actions
        // content actions return content results for use with generic mobile form field components
        [HttpGet]
        public ActionResult SubmitFormContent()
        {
            return new ContentResult() { Content = string.Empty }; 
            /* ContentEncoding = System.Text.Encoding.UTF8, ContentType = "application/text"  */
        }

        [HttpPost]
        public ActionResult SubmitFormContent(string field1)
        {
            return new ContentResult() { Content = field1 };
            /* ContentEncoding = System.Text.Encoding.UTF8, ContentType = "application/text"  */
        }
        

        
        // controller rendering actions
        // controller rendering actions can be bound with route data and call partial views directly
        // controller rendering actions do not go through the full controller lifecycle and other parts of the page might already have been rendered

        [HttpGet]
        public ActionResult SubmitFormRendering()
        {
            return PartialView((object)string.Empty);
        }
        
        [HttpPost]
        public ActionResult SubmitFormRendering(string field1)
        {
            return PartialView((object)field1);
        }
    }
}