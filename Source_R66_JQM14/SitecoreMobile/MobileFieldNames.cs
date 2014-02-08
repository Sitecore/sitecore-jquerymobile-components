using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitecoreMobile
{
    public static class MobileFieldNames
    {
        public static class PageItems
        {
            public static readonly string PageTitle = "PageTitle";
            public static readonly string PageContent = "PageContent";
            public static readonly string PageTheme = "PageTheme";
            public static readonly string PageHeaderPosition = "PageHeaderPosition";
            public static readonly string PageFooterPosition = "PageFooterPosition";
            public static readonly string PageHeaderTheme = "PageHeaderTheme";
            public static readonly string PageFooterTheme = "PageFooterTheme";
            public static readonly string HidePageFromNavigation = "HidePageFromNavigation";

        }

        public static class ButtonItems
        {
            public static readonly string ButtonTitle = "ButtonTitle";
            public static readonly string ButtonIcon = "ButtonIcon";
            public static readonly string ButtonTheme = "ButtonTheme";
            public static readonly string ButtonInline = "ButtonInline";
        }

        public static class TableColumnItems
        {
            public static readonly string ColumnType = "ColumnType";
            
        }

        public static class StandardViewRenderingParameters
        {
            public static readonly string FieldName = "FieldName";
            public static readonly string HiddenEditingField = "HiddenEditingField";
            public static readonly string DisplayFieldTitle = "DisplayFieldTitle";
            public static readonly string DisplayEditingFieldTitle = "DisplayEditingFieldTitle";
        
        }

        public static class HeadingViewRenderingParameters
        {
            public static readonly string HeadingTagName = "HeadingTagName";

        }

        public static class PlaceholderViewRenderingParameters
        {
            public static readonly string PlaceholderPrefix = "PlaceholderPrefix";
            public static readonly string PlaceholderCount = "PlaceholderCount";

            
            public static readonly string PlaceholderName = "PlaceholderName";


        }


        public static class PanelViewRenderingParameters
        {
            public static readonly string PanelName = "PanelName";

        }


        public static class PopupViewRenderingParameters
        {
            public static readonly string PopupName = "PopupName";
            public static readonly string PopupDismissible = "PopupDismissible";
            public static readonly string PopupOverlayTheme = "PopupOverlayTheme";

        }

        public static class SlideshowViewRenderingParameters
        {
            public static readonly string ImageFieldName = "ImageFieldName";
            public static readonly string CaptionFieldName = "CaptionFieldName";

            public static readonly string ImageWidth = "ImageWidth";
            public static readonly string ImageHeight = "ImageHeight";
            
            public static readonly string JsonParameters = "JsonParameters";

            public static readonly string MaxSlideshowLength = "MaxSlideshowLength";

        }

        public static class SelectViewRenderingParameters
        {
            public static readonly string SelectDataSource = "SelectDataSource";
            public static readonly string SelectIncludeEmptyOption = "SelectIncludeEmptyOption";

            public static readonly string DataInline = "DataInline";
            public static readonly string DataMini = "DataMini";
            public static readonly string DataRole = "DataRole";
            
        }

        public static class SliderViewRenderingParameters
        {
            public static readonly string SliderMinValue = "SliderMinValue";
            public static readonly string SliderMaxValue = "SliderMaxValue";

        }

        public static class ButtonViewRenderingParameters
        {
            public static readonly string GeneralLink = "GeneralLink";
            public static readonly string LinkFieldName = "LinkFieldName";
            public static readonly string TextFieldName = "TextFieldName";
            public static readonly string ButtonIcon = "ButtonIcon";
            public static readonly string ButtonTheme = "ButtonTheme";
            public static readonly string ButtonInline = "ButtonInline";
            public static readonly string ButtonMini = "ButtonMini";
            public static readonly string ButtonIconPosition = "ButtonIconPosition";
            public static readonly string ButtonIconShadow = "ButtonIconShadow";
            public static readonly string ButtonCorners = "ButtonCorners";
            public static readonly string ButtonShadow = "ButtonShadow";
            public static readonly string ButtonPopupOverlay = "ButtonPopupOverlay";
            public static readonly string ButtonTransition = "ButtonTransition";
            public static readonly string ButtonMode = "ButtonMode";

        }

        public static class ViewRenderingFields
        {
            public static readonly string ParametersTemplate = "Parameters Template";

        }


        public static class ListViewRenderingParameters
        {
            public static readonly string ListInset = "ListInset";
            public static readonly string ListMini = "ListMini";
            public static readonly string ListCorners = "ListCorners";
            public static readonly string ListIcon = "ListIcon";
            public static readonly string ListIconPosition = "ListIconPosition";
            public static readonly string ListTheme = "ListTheme";

            public static readonly string ListRoundedCorners = "ListRoundedCorners";
            public static readonly string ListCountTheme = "ListCountTheme";
            public static readonly string ListDividerTheme = "ListDividerTheme";
            public static readonly string ListFilter = "ListFilter";
            public static readonly string ListFilterDefaultText = "ListFilterDefaultText";
            public static readonly string ListFilterTheme = "ListFilterTheme";
            public static readonly string ListHeaderTheme = "ListHeaderTheme";
            public static readonly string ListSplitIcon = "ListSplitIcon";
            public static readonly string ListSplitTheme = "ListSplitTheme";
            public static readonly string MaxDisplayItems = "MaxDisplayItems";
            public static readonly string DisplayDatasourceChildrensChildren = "DisplayDatasourceChildrensChildren";
            public static readonly string DisplayDatasource = "DisplayDatasource";
            
        }


        public static class ControlGroupViewRenderingParameters
        {
            public static readonly string GroupOrientation = "GroupOrientation";


        }

        public static class GridViewRenderingParameters
        {
            public static readonly string PlaceholderColumnCount = "PlaceholderColumnCount";
            public static readonly string PlaceholderRowCount = "PlaceholderRowCount";
            public static readonly string GridResponsiveLayout = "GridResponsiveLayout";
            public static readonly string GroupOrientation = "GroupOrientation";


        }

        public static class TableViewRenderingParameters
        {
            public static readonly string TableSchema = "TableSchema";

        }
    }
}