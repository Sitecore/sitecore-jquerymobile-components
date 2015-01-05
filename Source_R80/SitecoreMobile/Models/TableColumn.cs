using Sitecore.ContentSearch.SearchTypes;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitecoreMobile.Models
{
    public class TableColumn
    {
        public String ColumnName { get; set; }
        public String DisplayName { get; set; }
        public String ColumnType { get; set; }
        public SitecoreUISearchResultItem DataSource { get; set; }

    }
}