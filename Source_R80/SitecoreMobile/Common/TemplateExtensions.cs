using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Items;
namespace SitecoreMobile.Common
{
    public static class TemplateExtensions
    {
        public static bool ItemTemplateInheritsFromTemplate(
            this TemplateItem checkTemplate,
            TemplateItem baseTemplate)
        {
            bool result = baseTemplate.ID.Equals(checkTemplate.ID);
            if (!result && checkTemplate.BaseTemplates.Length > 0)
            {
                TemplateItem[] templateList = checkTemplate.BaseTemplates;
                for (int i = 0; i < templateList.Length && !result; i++)
                {
                    result = templateList[i].ItemTemplateInheritsFromTemplate(baseTemplate);
                }
            }
            return result;
        }

    }
}