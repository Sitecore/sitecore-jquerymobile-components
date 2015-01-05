using Sitecore.ContentSearch.SearchTypes;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitecoreMobile.Models
{
    public class Table
    {
        public TableColumn[] Columns { get; set; }
        public List<SitecoreUISearchResultItem> DataSource { get; set; }
    }
}