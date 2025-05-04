// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Permissive License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
// All other rights reserved.

using System;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.ComponentModel;
using System.ComponentModel.Design;

#region Assembly Resource Attribute
[assembly: System.Web.UI.WebResource("AjaxControlToolkit.ValidatorCallout.ValidatorCalloutBehavior.js", "text/javascript")]
[assembly: System.Web.UI.WebResource("AjaxControlToolkit.ValidatorCallout.alert-large.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("AjaxControlToolkit.ValidatorCallout.alert-small.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("AjaxControlToolkit.ValidatorCallout.close.gif", "image/gif")]
#endregion

namespace AjaxControlToolkit
{
    [Designer("AjaxControlToolkit.ValidatorCalloutDesigner, AjaxControlToolkit")]
    [RequiredScript(typeof(CommonToolkitScripts))]
    [RequiredScript(typeof(PopupExtender))]
    [TargetControlType(typeof(IValidator))]
    [ClientScriptResource("AjaxControlToolkit.ValidatorCalloutBehavior", "AjaxControlToolkit.ValidatorCallout.ValidatorCalloutBehavior.js")]
    public class ValidatorCalloutExtender : ExtenderControlBase
    {
        [DefaultValue("")]
        [UrlProperty]
        [ExtenderControlProperty]
        [ClientPropertyName("warningIconImageUrl")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Justification = "Using string to avoid Uri complications")]
        public string WarningIconImageUrl
        {
            get { return GetPropertyValue("WarningIconImageUrl", (string) null) ?? Page.ClientScript.GetWebResourceUrl(typeof(ValidatorCalloutExtender), "AjaxControlToolkit.ValidatorCallout.alert-large.gif"); }
            set { SetPropertyValue("WarningIconImageUrl", value); }
        }

        [DefaultValue("")]
        [UrlProperty]
        [ExtenderControlProperty]
        [ClientPropertyName("closeImageUrl")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Justification = "Using string to avoid Uri complications")]
        public string CloseImageUrl
        {
            get { return GetPropertyValue("CloseImageUrl", (string) null) ?? Page.ClientScript.GetWebResourceUrl(typeof(ValidatorCalloutExtender), "AjaxControlToolkit.ValidatorCallout.close.gif"); }
            set { SetPropertyValue("CloseImageUrl", value); }
        }

        [DefaultValue("")]
        [ExtenderControlProperty]
        [ClientPropertyName("highlightCssClass")]
        public string HighlightCssClass
        {
            get { return GetPropertyValue("HighlightCssClass", string.Empty); }
            set { SetPropertyValue("HighlightCssClass", value); }
        }

        [DefaultValue(typeof(Unit), "")]
        [ExtenderControlProperty]
        [ClientPropertyName("width")]
        public Unit Width
        {
            get { return GetPropertyValue("Width", Unit.Empty); }
            set { SetPropertyValue("Width", value); }
        }
    }
}


