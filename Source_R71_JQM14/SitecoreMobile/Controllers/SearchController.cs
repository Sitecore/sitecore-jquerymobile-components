using Sitecore;
using Sitecore.Buckets.Util;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.SearchTypes;
using Sitecore.ContentSearch.Security;
using Sitecore.ContentSearch.Utilities;
using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;
using Sitecore.StringExtensions;
using SitecoreMobile.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace SitecoreMobile.Controllers
{

    public class CustomSearchResultItem : SearchResultItem
    {
        public CustomSearchResultItem()
            : base()
        {
        }

        [IndexField("_path")]
        public string GuidPath { get; set; }
        
        //[IndexField("_content")]
        //public string Content { get; set; }

    }

    public class JsonItem
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Path { get; set; }
        public string RelativeLink { get; set; }

        public string PageTitle { get; set; }
        public string ButtonTitle { get; set; }

    }

    public class SearchController : Controller
    {
        public ActionResult Test(string echo)
        {
            var q = new List<string>();
            q.AddRange(
                Enumerable.Range(1, echo.Length).Select(i => string.Format("{0}-{1}", echo, i)));
            return Json(q, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AutoComplete(string searchText)
        {
            var q = new List<JsonItem>();
            var contextItem = PageContext.Current.Item;
            
            using (IProviderSearchContext searchContext = SearchIndex.CreateSearchContext(SearchSecurityOptions.EnableSecurityCheck))
            {
                var contextItemShortId = contextItem.ID.ToShortID().ToString().ToLowerInvariant();
                var queryable = searchContext.GetQueryable<CustomSearchResultItem>();
                queryable = queryable.Where(i => 
                    i.GuidPath.Equals(contextItemShortId) 
                    && (
                        i.Name.StartsWith(searchText)
                        || i.Content.Contains(searchText)
                    )).Take(100);
                queryable.ToList().ForEach(r => 
                    {
                        var i = r.GetItem();
                        q.Add(new JsonItem()
                        {
                            ID = i.ID.Guid,
                            Name = i.Name,
                            DisplayName = i.DisplayName,
                            Path = i.Paths.FullPath,
                            RelativeLink = Sitecore.Links.LinkManager.GetItemUrl(i),
                            PageTitle = !string.IsNullOrEmpty(i["PageTitle"]) ? i["PageTitle"] : i.DisplayName,
                            ButtonTitle = !string.IsNullOrEmpty(i["ButtonTitle"]) ? i["ButtonTitle"] : i.DisplayName
                        });
                    });
            }
            return Json(q, JsonRequestBehavior.AllowGet);
        }


        protected Item ContextItem
        {
            get
            {
                return PageContext.Current.Item;
            }
        }

        private ISearchIndex _searchIndex;
        private ISearchIndex SearchIndex
        {
            get
            {
                return _searchIndex ?? (_searchIndex = ContentSearchManager.GetIndex((IIndexable)new SitecoreIndexableItem(ContextItem)));
            }
        }


    }
}
