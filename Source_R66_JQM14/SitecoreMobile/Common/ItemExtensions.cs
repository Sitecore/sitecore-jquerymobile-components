using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitecoreMobile.Common
{
    public static class ItemExtensions
    {
        public static string GetRoutePathInfo(this Sitecore.Data.Items.Item item)
        {
            return Sitecore.Links.LinkManager.GetItemUrl(item).TrimStart(new char[] { '/' });
        }
    }
}