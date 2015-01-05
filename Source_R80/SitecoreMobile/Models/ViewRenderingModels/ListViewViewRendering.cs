using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitecoreMobile.Models.ViewRenderingModels
{
    public class ListViewViewRendering : BaseListViewRendering
    {
        public ListViewDisplayItem[] ListViewDisplayItems { get; protected set; }

        public string CssClass { get; protected set; }
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

        public string DisplayEmptyMessageDatasource { get; protected set; }
        public string DisplayEmptyMessageDatasourceFieldName { get; protected set; }
        public Sitecore.Data.Items.Item DisplayEmptyMessageItem { get; protected set; }

        public string DisplayField01 { get; protected set; }
        public string DisplayField02 { get; protected set; }

        public string CountPlaceholderName { get; protected set; }
        public string CustomPlaceholderName { get; protected set; }
        public string ChildPlaceholderName { get; protected set; }

        public ListViewViewRendering()
            : base()
        {
        }


        public ListViewViewRendering(ListViewDisplayItem[] displayItems)
            : base(displayItems.Select(d => d.DisplayItem).ToArray())
        {
            ListViewDisplayItems = displayItems;
        }


        public override void Initialize(Sitecore.Mvc.Presentation.Rendering rendering)
        {
            base.Initialize(rendering);

            InitializeRenderingParameters();

            InitializeListViewDisplayItems();

        }

        protected override void InitializeListDisplayItems()
        {
            base.InitializeListDisplayItems();
        }

        protected virtual void InitializeListViewDisplayItems()
        {
            if (ListViewDisplayItems == null)
            {
                List<ListViewDisplayItem> displayItems = new List<ListViewDisplayItem>();

                if (DisplayDatasource)
                {
                    displayItems.Add(new ListViewDisplayItem(Rendering.Item, true));
                }

                //DisplayItems.ToList().ForEach(item => displayItems.Add(new ListViewDisplayItem(item, false)));

                var childDisplayItems = GetListViewDisplayItems(DisplayItems, DisplayDatasourceChildrensChildren);

                childDisplayItems.ToList().ForEach(d => displayItems.Add(d));

                ListViewDisplayItems = displayItems.ToArray();
            }
        }

        private void InitializeRenderingParameters()
        {
            DisplayDatasourceChildrensChildren = Rendering.Parameters[MobileFieldNames.ListViewRenderingParameters.DisplayDatasourceChildrensChildren] != "1" ? false : true;
            DisplayDatasource = Rendering.Parameters[MobileFieldNames.ListViewRenderingParameters.DisplayDatasource] != "1" ? false : true;

            DisplayEmptyMessageDatasourceFieldName = "RichText";
            DisplayEmptyMessageDatasource = Rendering.Parameters[MobileFieldNames.ListViewRenderingParameters.DisplayEmptyMessageDatasource];
            DisplayEmptyMessageItem = (!string.IsNullOrEmpty(DisplayEmptyMessageDatasource)) ? this.PageItem.Database.GetItem(new Sitecore.Data.ID(DisplayEmptyMessageDatasource)) : null;

            CssClass = Rendering.Parameters[MobileFieldNames.StandardViewRenderingParameters.CssClass] ?? null;
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

            DisplayField01 = Rendering.Parameters[MobileFieldNames.ListViewRenderingParameters.DisplayField01] ?? null;
            DisplayField02 = Rendering.Parameters[MobileFieldNames.ListViewRenderingParameters.DisplayField02] ?? null;


            CountPlaceholderName = Rendering.Parameters[MobileFieldNames.ListViewRenderingParameters.CountPlaceholderName] ?? null;
            CustomPlaceholderName = Rendering.Parameters[MobileFieldNames.ListViewRenderingParameters.CustomPlaceholderName] ?? null;
            ChildPlaceholderName = Rendering.Parameters[MobileFieldNames.ListViewRenderingParameters.ChildPlaceholderName] ?? null;
        }

        protected ListViewDisplayItem[] GetListViewDisplayItems(Item[] items, bool displayChildren)
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
    }
}