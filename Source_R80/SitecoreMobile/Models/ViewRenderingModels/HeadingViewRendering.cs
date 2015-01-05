using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitecoreMobile.Models.ViewRenderingModels
{
    public class HeadingViewRendering : StandardFieldViewRendering
    {
        public string HeadingTagName { get; protected set; }

        public override void Initialize(Sitecore.Mvc.Presentation.Rendering rendering)
        {
            base.Initialize(rendering);

            HeadingTagName = Rendering.Parameters[SitecoreMobile.MobileFieldNames.HeadingViewRenderingParameters.HeadingTagName] ?? "h1";
            
        }


    }
}