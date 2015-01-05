using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitecoreMobile.Models.ViewRenderingModels
{
    public class SearchViewRendering : BaseListViewRendering
    {

        public string Rounded { get; protected set; }
        public string Theme { get; protected set; }
        public string DataIcon { get; protected set; }

        public SearchViewRendering()
            : base()
        {
        }

        public override void Initialize(Sitecore.Mvc.Presentation.Rendering rendering)
        {
            base.Initialize(rendering);
            InitializeRenderingParameters();
        }

        protected override void InitializeListDisplayItems()
        {
            base.InitializeListDisplayItems();
        }

        private void InitializeRenderingParameters()
        {
            
            Rounded = Rendering.Parameters[MobileFieldNames.ListViewRenderingParameters.ListRoundedCorners] == "0" ? null : "true";
            Theme = Rendering.Parameters[MobileFieldNames.ListViewRenderingParameters.ListTheme] ?? null;
           
            DataIcon = Rendering.Parameters[MobileFieldNames.ListViewRenderingParameters.ListIcon] ?? null;

        }

        
    }
}