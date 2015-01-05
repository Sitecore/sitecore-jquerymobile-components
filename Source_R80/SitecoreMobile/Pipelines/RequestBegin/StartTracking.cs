using Sitecore.Analytics;
using Sitecore.Diagnostics;
using Sitecore.Sites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitecoreMobile.Pipelines.RequestBegin
{
    public class StartTracking : Sitecore.Mvc.Pipelines.Request.RequestBegin.RequestBeginProcessor
    {
        public override void Process(Sitecore.Mvc.Pipelines.Request.RequestBegin.RequestBeginArgs args)
        {
            Tracker.StartTracking();
        }
    }
}