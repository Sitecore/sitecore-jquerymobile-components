using Sitecore.ContentSearch;
using Sitecore.ContentSearch.SearchTypes;
using Sitecore.ContentSearch.Security;
using Sitecore.ContentSearch.Utilities;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitecoreMobile.Common
{
    public class DataSourceExtensions
    {
        public static List<SitecoreUISearchResultItem> DataSourceSearchResults(Item contextItem, string dataSource)
        {
            ISearchIndex index = ContentSearchManager.GetIndex((IIndexable)new SitecoreIndexableItem(contextItem));
            if (index == null)
            {
                return new List<SitecoreUISearchResultItem>(0);
            }
            if (Sitecore.StringExtensions.StringExtensions.IsNullOrEmpty(dataSource))
            {
                return new List<SitecoreUISearchResultItem>(0);
            }
            List<SearchStringModel> enumerable = Sitecore.ContentSearch.Utilities.SearchStringModel.ParseDatasourceString(dataSource).ToList();
            
            // correct the sort operation for each search string part
            CorrectSort(enumerable);
            
            using (IProviderSearchContext searchContext = index.CreateSearchContext(SearchSecurityOptions.EnableSecurityCheck))
            {
                var query = LinqHelper.CreateQuery<SitecoreUISearchResultItem>(searchContext, enumerable, contextItem, null);
                return query.ToList();
            }   

        }

        private static void CorrectSort(List<SearchStringModel> enumerable)
        {
            // correct the sort operation for each search string part
            // if the operation is "not", then the string had a -sort and we reverse the sort direction
            enumerable.ForEach(ss =>
            {
                var isSort = ss.Type.Equals("sort", StringComparison.InvariantCultureIgnoreCase);
                var isDesc = isSort && ss.Operation.Equals("not", StringComparison.InvariantCultureIgnoreCase);
                var sortDirection = (isDesc) ? "desc" : null;
                ss.Operation = (isSort) ? sortDirection : ss.Operation;
            });
        }
    }
}