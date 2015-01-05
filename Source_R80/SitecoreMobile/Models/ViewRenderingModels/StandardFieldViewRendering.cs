using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitecoreMobile.Models.ViewRenderingModels
{
    public class StandardFieldViewRendering : Sitecore.Mvc.Presentation.RenderingModel
    {
        public string FieldName { get; protected set; }

        public Sitecore.Data.Items.Item DisplayItem { get; protected set; }

        public override void Initialize(Sitecore.Mvc.Presentation.Rendering rendering)
        {
            base.Initialize(rendering);
            FieldName = Rendering.Parameters[SitecoreMobile.MobileFieldNames.StandardViewRenderingParameters.FieldName];
            InitializeDisplayItem();
        }

        protected virtual void InitializeDisplayItem()
        {
            DisplayItem = Sitecore.Mvc.Presentation.RenderingContext.Current.ContextItem;
        }

    }
}