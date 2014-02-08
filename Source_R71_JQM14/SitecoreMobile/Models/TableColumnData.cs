using Sitecore.ContentSearch.SearchTypes;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitecoreMobile.Models
{
    public class TableColumnData
    {
        public TableColumn Column { get; set; }
        public SitecoreUISearchResultItem DataSource { get; set; }
    }
}