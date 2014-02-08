using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitecoreMobile.Models
{
    public class ButtonModel
    {
        public Item Item { get; set; }

        public String CssClass { get; set; }

        public String TextFieldName { get; set; }
        public String LinkFieldName { get; set; }

        public String ButtonTarget { get; set; }
        public String ButtonText { get; set; }
        public String ButtonLink { get; set; }

        public String ButtonIcon { get; set; }
        public String ButtonIconPosition { get; set; }
        public String ButtonTheme { get; set; }
        public String ButtonTransition { get; set; }
        public String ButtonMode { get; set; }
        public String ButtonRole { get; set; }

        public Nullable<Boolean> ButtonInline { get; set; }
        public Nullable<Boolean> ButtonMini { get; set; }
        public Nullable<Boolean> ButtonIconShadow { get; set; }
        public Nullable<Boolean> ButtonCorners { get; set; }
        public Nullable<Boolean> ButtonShadow { get; set; }

        public Nullable<Boolean> ButtonPopupOverlay { get; set; }

    }
}