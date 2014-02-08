using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;
using SitecoreMobile.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitecoreMobile.Models.ViewRenderingModels
{
    public abstract class ListViewRendering: Sitecore.Mvc.Presentation.RenderingModel
    {
        public ListViewRendering()
            : base()
        {
        }

        public override void Initialize(Sitecore.Mvc.Presentation.Rendering rendering)
        {
            base.Initialize(rendering);

            int l = 999;
            int.TryParse(Rendering.Parameters[MobileFieldNames.ListViewRenderingParameters.MaxDisplayItems], out l);
            MaxDisplayItems = l;

            DataSourceQuery = !string.IsNullOrEmpty(Rendering.DataSource) && !Rendering.DataSource.StartsWith("{") ? Rendering.DataSource : null;
            
            EditingEnabled = (Sitecore.Context.Site.DisplayMode == Sitecore.Sites.DisplayMode.Edit);

            if (string.IsNullOrEmpty(DataSourceQuery) && Rendering.Item != null && Rendering.Item.Children.Count > 0)
            {
                var children = Rendering.Item.Children.ToArray();
                var displayItems = GetVisibleListChildren(children);
                DisplayItems = displayItems;
            }
            else if (!string.IsNullOrEmpty(DataSourceQuery))
            {
                var dataSourceResult = DataSourceExtensions.DataSourceSearchResults(Rendering.Item, Rendering.DataSource);
                DisplayItems = dataSourceResult.Select(r => r.GetItem()).ToArray();
            }
            else
            {
                DisplayItems = new Item[0];
            }

        }

        protected virtual Item[] GetVisibleListChildren(Item[] children)
        {
            var displayItems = children.Where(i =>
                i[MobileFieldNames.PageItems.HidePageFromNavigation] != "1").ToArray(); // Take(MaxDisplayItems).
            return displayItems;
        }

        public int MaxDisplayItems { get; protected set; }

        public string DataSourceQuery { get; protected set; }

        public Item[] DisplayItems { get; protected set; }

        public bool EditingEnabled { get; protected set; }

        public string GetId()
        {
            return string.Format("list_{0}", Rendering.UniqueId.ToString("N"));
        }

    }

    
}