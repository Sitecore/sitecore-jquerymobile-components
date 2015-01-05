using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitecoreMobile.Pipelines.Response.RenderRendering
{
    public class ResetRenderingModel : Sitecore.Mvc.Pipelines.Response.RenderRendering.RenderRenderingProcessor
    {
        public override void Process(Sitecore.Mvc.Pipelines.Response.RenderRendering.RenderRenderingArgs args)
        {
            args.Rendering.Item = null;
            args.Rendering.Model = null;
        }
    }
}