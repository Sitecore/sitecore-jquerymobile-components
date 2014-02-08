using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitecoreMobile.Models.ViewRenderingModels
{
    public class ListViewViewRendering : ListViewRendering
    {
        public class ListViewDisplayItem
        {
            public ListViewDisplayItem(Item item, bool displayChildren)
            {
                DisplayItem = item;
                DisplayChildren = displayChildren;
            }

            public Item DisplayItem { get; protected set; }
            public bool DisplayChildren { get; protected set; }

        }


        public ListViewViewRendering()
            : base()
        {
        }

        public ListViewDisplayItem[] ListViewDisplayItems { get; protected set; }

        public override void Initialize(Sitecore.Mvc.Presentation.Rendering rendering)
        {
            base.Initialize(rendering);

            DisplayDatasourceChildrensChildren = Rendering.Parameters[MobileFieldNames.ListViewRenderingParameters.DisplayDatasourceChildrensChildren] != "1" ? false : true;
            DisplayDatasource = Rendering.Parameters[MobileFieldNames.ListViewRenderingParameters.DisplayDatasource] != "1" ? false : true;

            List<ListViewDisplayItem> displayItems = new List<ListViewDisplayItem>();

            if (DisplayDatasource)
            {
                displayItems.Add(new ListViewDisplayItem(Rendering.Item, true));
            }      
            
            //DisplayItems.ToList().ForEach(item => displayItems.Add(new ListViewDisplayItem(item, false)));

            var childDisplayItems = GetListViewDisplayItems(DisplayItems, DisplayDatasourceChildrensChildren);

            childDisplayItems.ToList().ForEach(d => displayItems.Add(d));
                        
            ListViewDisplayItems = displayItems.ToArray();

            Rounded = Rendering.Parameters[MobileFieldNames.ListViewRenderingParameters.ListRoundedCorners] == "0" ? null : "true";
            Theme = Rendering.Parameters[MobileFieldNames.ListViewRenderingParameters.ListTheme] ?? null;
            DataCountTheme = Rendering.Parameters[MobileFieldNames.ListViewRenderingParameters.ListCountTheme] ?? null;
            DataDividerTheme = Rendering.Parameters[MobileFieldNames.ListViewRenderingParameters.ListDividerTheme] ?? null;
            DataFilter = Rendering.Parameters[MobileFieldNames.ListViewRenderingParameters.ListFilter] == "1" ? "true" : null;
            DataFilterDefaultText = Rendering.Parameters[MobileFieldNames.ListViewRenderingParameters.ListFilterDefaultText] ?? null;
            DataFilterTheme = Rendering.Parameters[MobileFieldNames.ListViewRenderingParameters.ListFilterTheme] ?? null;
            DataHeaderTheme = Rendering.Parameters[MobileFieldNames.ListViewRenderingParameters.ListHeaderTheme] ?? null;
            DataIcon = Rendering.Parameters[MobileFieldNames.ListViewRenderingParameters.ListIcon] ?? null;
            DataSplitIcon = Rendering.Parameters[MobileFieldNames.ListViewRenderingParameters.ListSplitIcon] ?? null;
            DataSplitTheme = Rendering.Parameters[MobileFieldNames.ListViewRenderingParameters.ListSplitTheme] ?? null;


        }

        private ListViewDisplayItem[] GetListViewDisplayItems(Item[] items, bool displayChildren)
        {
            List<ListViewDisplayItem> resultDisplayItems = new List<ListViewDisplayItem>();
            foreach (Item item in items)
            {
                resultDisplayItems.Add(new ListViewDisplayItem(item, displayChildren));
                if (displayChildren && item.HasChildren) // && DisplayDatasourceChildrensChildren
                {
                    Item[] children = GetVisibleListChildren(item.Children.ToArray());
                    ListViewDisplayItem[] childDisplayItems = GetListViewDisplayItems(children, false);
                    childDisplayItems.ToList().ForEach(d => resultDisplayItems.Add(d));
                }
            }
            return resultDisplayItems.ToArray();
        }

        public string Rounded { get; protected set; }
        public string Theme { get; protected set; }
        public string DataCountTheme { get; protected set; }
        public string DataDividerTheme { get; protected set; }
        public string DataFilter { get; protected set; }
        public string DataFilterDefaultText { get; protected set; }
        public string DataFilterTheme { get; protected set; }
        public string DataHeaderTheme { get; protected set; }
        public string DataIcon { get; protected set; }
        public string DataSplitIcon { get; protected set; }
        public string DataSplitTheme { get; protected set; }

        public bool DisplayDatasourceChildrensChildren { get; protected set; }
        public bool DisplayDatasource { get; protected set; }
    }
}