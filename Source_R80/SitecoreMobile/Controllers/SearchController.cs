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

            if (ContextItem == null)
            {
                return HttpNotFound();
            }

            if (string.IsNullOrEmpty(searchText) || string.IsNullOrWhiteSpace(searchText))
            {
                return HttpNotFound();
            }

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
                        string buttonTitle =  null;

                        if (!string.IsNullOrEmpty(i["SessionTitle"]))
                        {
                            buttonTitle = string.Concat(i["SessionCode"], " ", i["SessionTitle"]);
                        }
                        if (!string.IsNullOrEmpty(i["SpeakerName"]))
                        {
                            buttonTitle = string.Concat(i["SpeakerName"], " ", i["CompanyName"]);
                        }
                        if (!string.IsNullOrEmpty(i["ButtonTitle"]))
                        {
                            buttonTitle = string.Concat(i["ButtonTitle"]);
                        }
                        if (string.IsNullOrEmpty(buttonTitle))
                        {
                            buttonTitle = i.DisplayName;
                        }    

                        q.Add(new JsonItem()
                        {
                            ID = i.ID.Guid,
                            Name = i.Name,
                            DisplayName = i.DisplayName,
                            Path = i.Paths.FullPath,
                            RelativeLink = Sitecore.Links.LinkManager.GetItemUrl(i),
                            PageTitle = !string.IsNullOrEmpty(i["PageTitle"]) ? i["PageTitle"] : i.DisplayName,
                            // ButtonTitle = !string.IsNullOrEmpty(i["ButtonTitle"]) ? i["ButtonTitle"] : i.DisplayName
                            ButtonTitle = buttonTitle
                        });
                    });
            }
            return Json(q, JsonRequestBehavior.AllowGet);
        }

        private Item contextItem = null;

        protected Item ContextItem
        {
            get
            {
                if (contextItem == null && PageContext.Current != null && PageContext.Current.Item != null)
                {
                    contextItem = PageContext.Current.Item;
                }
                else if (contextItem == null && Sitecore.Sites.SiteContext.Current != null && Sitecore.Sites.SiteContext.Current.RootPath != null)
                {
                    contextItem = Sitecore.Context.Database.GetItem(String.Concat(Sitecore.Sites.SiteContext.Current.RootPath));
                }
                return contextItem;
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
