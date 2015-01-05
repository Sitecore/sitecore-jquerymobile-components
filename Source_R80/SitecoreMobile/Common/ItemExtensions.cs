using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitecoreMobile.Common
{
    public static class ItemExtensions
    {
        public static string GetRoutePathInfo(this Sitecore.Data.Items.Item item, Sitecore.Links.UrlOptions urlOptions = null)
        {
            return GetItemUrl(item, urlOptions).TrimStart(new char[] { '/' });
        }
        public static string GetRoutePathInfo(this Sitecore.Data.Items.Item item)
        {
            return GetItemUrl(item).TrimStart(new char[] { '/' });
        }
        public static string GetItemUrl(this Sitecore.Data.Items.Item item, Sitecore.Links.UrlOptions urlOptions = null)
        {
            return Sitecore.Links.LinkManager.GetItemUrl(item, urlOptions ?? Sitecore.Links.UrlOptions.DefaultOptions);
        }
    }
}