using Sitecore.Mvc.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SitecoreMobile.Controllers
{
    public class TestController : Controller
    {
        //
        // GET: /Test/

        public class TestModel : Sitecore.Analytics.Tracking.ModelExtension
        {
            public String TestValue
            {
                get
                {
                    return this.Attributes.Get("TestValue");
                }
                set
                {
                    this.Attributes.Set("TestValue", value);
                }
            }

        }


        private static readonly string testKey = "echo";

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TestIndex()
        {
            return Index();
        }

        public ActionResult Test(object echo)
        {
            var q = new { @result = echo };

            //var q = new List<string>();
            //q.AddRange(
            //    Enumerable.Range(1, echo.Length).Select(i => string.Format("{0}-{1}", echo, i)));

            return Json(q, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TestInteractionAddCustomValue(string echo)
        {
            Sitecore.Analytics.Tracker.Current.Interaction.CustomValues.Add(testKey, echo);
            return Test(echo);
        }
        public ActionResult TestInteractionGetCustomValue()
        {
            var echo = Sitecore.Analytics.Tracker.Current.Interaction.CustomValues[testKey] as string;
            return Test(echo);
        }

        public ActionResult TestSessionDeviceAddCustomValue(string echo)
        {
            Sitecore.Analytics.Tracker.Current.Session.Device.CustomValues.Add(testKey, echo);
            return Test(echo);
        }
        public ActionResult TestSessionDeviceGetCustomValue()
        {
            var echo = Sitecore.Analytics.Tracker.Current.Session.Device.CustomValues[testKey] as string;
            return Test(echo);
        }
        
        public ActionResult TestContactIdentifiers()
        {
            var i = Sitecore.Analytics.Tracker.Current.Contact.Identifiers;
            return Test(new { i.Identifier, i.IdentificationLevel, i.AuthenticationLevel });
        }

        public ActionResult TestContactAddAttachment(string echo)
        {
            Sitecore.Analytics.Tracker.Current.Contact.Attachments.Add(testKey, echo);
            return Test(echo);
        }
        public ActionResult TestContactGetAttachment()
        {
            var echo = Sitecore.Analytics.Tracker.Current.Contact.Attachments[testKey] as string;
            return Test(echo);
        }

        public ActionResult TestContactAddSimpleValue(string echo)
        {
            Sitecore.Analytics.Tracker.Current.Contact.Extensions.SimpleValues[testKey] = echo;
            return Test(echo);
        }
        public ActionResult TestContactGetSimpleValue()
        {
            var echo = Sitecore.Analytics.Tracker.Current.Contact.Extensions.SimpleValues[testKey];
            return Test(echo);
        }

        public ActionResult TestContactAddTestModelValue(string echo)
        {
            var model = Sitecore.Analytics.Tracker.Current.Session.Contact.Extensions.Get<TestModel>(testKey);
            if (model == null)
            {
                model = Sitecore.Analytics.Tracker.Current.Session.Contact.Extensions.Create<TestModel>(testKey);
            }
            model.TestValue = echo;

            return Test(model.TestValue);
        }
        public ActionResult TestContactGetTestModelValue()
        {
            var echo = Sitecore.Analytics.Tracker.Current.Session.Contact.Extensions.Get<TestModel>(testKey);
            return Test(echo.TestValue);
        }
        
        public ActionResult TestContactAddTag(string echo)
        {
            Sitecore.Analytics.Tracker.Current.Contact.Tags.Set(testKey, echo);
            return Test(echo);
        }
        public ActionResult TestContactGetTag()
        {
            var echo = Sitecore.Analytics.Tracker.Current.Contact.Tags[testKey];
            return Test(echo);
        }

        public ActionResult TestContactAddCustomSessionData(string echo)
        {
            Sitecore.Analytics.Tracker.Current.Session.CustomData.Add(testKey, echo);
            return Test(echo);
        }
        public ActionResult TestContactGetCustomSessionData()
        {
            var echo = Sitecore.Analytics.Tracker.Current.Session.CustomData[testKey] as string;
            return Test(echo);
        }

        // this will not work if the session is actio on the current delivery cluster
        public ActionResult TestRepoContactAddTestModelValue(string contactid, string echo)
        {
            Sitecore.Analytics.Data.ContactRepository repo = new Sitecore.Analytics.Data.ContactRepository();

            var c = repo.TryLoadContact(
                contactid,
                new Sitecore.Analytics.Model.LeaseOwner(
                    "TestController",
                    Sitecore.Analytics.Model.LeaseOwnerType.WebCluster),
                TimeSpan.FromSeconds(10));

            var model = c.Object.Extensions.Create<TestModel>(testKey);
            model.TestValue = echo;

            repo.SaveContact(c.Object, c.LockedBy, true);

            return Test(model.TestValue);
        }

        public ActionResult TestRepoContactGetTestModelValue(string contactid)
        {
            Sitecore.Analytics.Data.ContactRepository repo = new Sitecore.Analytics.Data.ContactRepository();

            var c = repo.LoadContactReadOnly(contactid);

            var echo = c.Extensions.Get<TestModel>(testKey);

            return Test(echo.TestValue);
        }


        
    }
}
