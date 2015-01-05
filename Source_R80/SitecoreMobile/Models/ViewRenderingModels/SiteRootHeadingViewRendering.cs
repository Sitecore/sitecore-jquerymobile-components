using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitecoreMobile.Models.ViewRenderingModels
{
    public class SiteRootHeadingViewRendering : HeadingViewRendering
    {

        public override void Initialize(Sitecore.Mvc.Presentation.Rendering rendering)
        {
            base.Initialize(rendering);
        }

        protected override void InitializeDisplayItem()
        {
            var siteContextItem = PageItem.Database.GetItem(String.Concat(Sitecore.Context.Site.RootPath, Sitecore.Context.Site.StartItem)); // , Sitecore.Context.Site.StartItem

            DisplayItem = siteContextItem;

        }
    }
}