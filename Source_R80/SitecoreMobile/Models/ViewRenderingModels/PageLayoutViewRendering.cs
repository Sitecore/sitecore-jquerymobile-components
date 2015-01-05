using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitecoreMobile.Models.ViewRenderingModels
{
    public class PageLayoutViewRendering : Sitecore.Mvc.Presentation.RenderingModel
    {
        public bool PageEditEnabled { get; set; }

        public Sitecore.Data.Items.Item SiteContextItem { get; set; }

        public string SiteStyleBundleName { get; set; }

        public string SiteScriptBundleName { get; set; }

        public string ThemeName { get; set; }

        public string Title { get; set; }

        public string HeaderPos { get; set; }

        public string FooterPos { get; set; }

        public string HeaderTheme { get; set; }

        public string FooterTheme { get; set; }

        public override void Initialize(Sitecore.Mvc.Presentation.Rendering rendering)
        {
            base.Initialize(rendering);

            PageEditEnabled = (Sitecore.Context.Site.DisplayMode == Sitecore.Sites.DisplayMode.Edit);

            SiteContextItem = PageItem.Database.GetItem(String.Concat(Sitecore.Context.Site.RootPath, Sitecore.Context.Site.StartItem)); // 

            var styleBundleNameField = SiteContextItem.Fields["SiteStyleBundleName"];
            SiteStyleBundleName = styleBundleNameField != null && !string.IsNullOrEmpty(styleBundleNameField.Value) ? styleBundleNameField.Value : null;

            var scriptBundleNameField = SiteContextItem.Fields["SiteScriptBundleName"];
            SiteScriptBundleName = scriptBundleNameField != null && !string.IsNullOrEmpty(scriptBundleNameField.Value) ? scriptBundleNameField.Value : null;

            var themeField = Item.Fields["PageTheme"];
            ThemeName = themeField != null && !string.IsNullOrEmpty(themeField.Value) ? themeField.Value : null;

            var titleField = Item.Fields["PageTitle"];
            Title = titleField != null && !string.IsNullOrEmpty(titleField.Value) ? titleField.Value : null;

            var headerPosField = Item.Fields["PageHeaderPosition"];
            HeaderPos = headerPosField != null && !string.IsNullOrEmpty(headerPosField.Value) ? headerPosField.Value : null;

            var footerPosField = Item.Fields["PageFooterPosition"];
            FooterPos = footerPosField != null && !string.IsNullOrEmpty(footerPosField.Value) ? footerPosField.Value : null;

            var headerThemeField = Item.Fields["PageHeaderTheme"];
            HeaderTheme = headerThemeField != null && !string.IsNullOrEmpty(headerThemeField.Value) ? headerThemeField.Value : null;

            var footerThemeField = Item.Fields["PageFooterTheme"];
            FooterTheme = footerThemeField != null && !string.IsNullOrEmpty(footerThemeField.Value) ? footerThemeField.Value : null;
    


        }


        


    }
}