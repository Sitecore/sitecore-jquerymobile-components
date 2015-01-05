using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitecoreMobile.Models.ViewRenderingModels
{
    public class SiteRootListViewViewRendering : ListViewViewRendering
    {
        public Sitecore.Data.Items.Item SiteContextItem { get; set; }

        public override void Initialize(Sitecore.Mvc.Presentation.Rendering rendering)
        {
            base.Initialize(rendering);
        }

        protected override void InitializeListDisplayItems()
        {
            // skip default list initialization
            return;
        }

        protected override void InitializeListViewDisplayItems()
        {
            if (ListViewDisplayItems == null)
            {
                SiteContextItem = PageItem.Database.GetItem(String.Concat(Sitecore.Context.Site.RootPath, Sitecore.Context.Site.StartItem)); // , Sitecore.Context.Site.StartItem

                var displayItems = GetVisibleListChildren(SiteContextItem.Children.ToArray());
                DisplayItems = displayItems;

                List<ListViewDisplayItem> listViewDisplayItems = new List<ListViewDisplayItem>();

                if (DisplayDatasource)
                {
                    listViewDisplayItems.Add(new ListViewDisplayItem(Rendering.Item, true));
                }

                var childDisplayItems = GetListViewDisplayItems(DisplayItems, DisplayDatasourceChildrensChildren);

                childDisplayItems.ToList().ForEach(d => listViewDisplayItems.Add(d));

                ListViewDisplayItems = listViewDisplayItems.ToArray();
            }
        }

        protected override Sitecore.Data.Items.Item[] GetVisibleListChildren(Sitecore.Data.Items.Item[] children)
        {
            return base.GetVisibleListChildren(children);
        }


    }
}