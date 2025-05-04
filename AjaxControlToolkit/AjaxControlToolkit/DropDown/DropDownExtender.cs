// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Permissive License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
// All other rights reserved.

using System;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;

#region Assembly Resource Attribute
[assembly: System.Web.UI.WebResource("AjaxControlToolkit.DropDown.drop-arrow.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("AjaxControlToolkit.DropDown.DropDown.css", "text/css", PerformSubstitution=true)]
[assembly: System.Web.UI.WebResource("AjaxControlToolkit.DropDown.DropDownBehavior.js", "text/javascript")]
#endregion

namespace AjaxControlToolkit
{
    [Designer("AjaxControlToolkit.DropDownDesigner, AjaxControlToolkit")]
    [TargetControlType(typeof(Control))]
    [RequiredScript(typeof(CommonToolkitScripts))]
    [RequiredScript(typeof(PopupExtender))]
    [RequiredScript(typeof(HoverExtender))]    
    [RequiredScript(typeof(DynamicPopulateExtender))]
    [ClientCssResource("AjaxControlToolkit.DropDown.DropDown.css")]
    [ClientScriptResource("AjaxControlToolkit.DropDownBehavior", "AjaxControlToolkit.DropDown.DropDownBehavior.js")]
    public class DropDownExtender : ExtenderControlBase
    {
        [DefaultValue("")]
        [IDReferenceProperty(typeof(Control))]
        [ExtenderControlProperty]
        [ElementReference]
        [ClientPropertyName("dropDownControl")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase", Justification = "Following ASP.NET AJAX pattern")]
        public string DropDownControlID
        {
            get { return (string)(ViewState["DropDownControlID"] ?? string.Empty); }
            set { ViewState["DropDownControlID"] = value; }
        }

        [DefaultValue(typeof(Color), "")]
        [ExtenderControlProperty]
        [ClientPropertyName("highlightBorderColor")]
        public Color HighlightBorderColor
        {
            get { return (Color)(ViewState["HighlightBorderColor"] ?? Color.Empty); }
            set { ViewState["HighlightBorderColor"] = value; }
        }

        [DefaultValue(typeof(Color), "")]
        [ExtenderControlProperty]
        [ClientPropertyName("highlightBackgroundColor")]
        public Color HighlightBackColor
        {
            get { return (Color)(ViewState["HighlightBackColor"] ?? Color.Empty); }
            set { ViewState["HighlightBackColor"] = value; }
        }

        [DefaultValue(typeof(Color), "")]
        [ExtenderControlProperty]
        [ClientPropertyName("dropArrowBackgroundColor")]
        public Color DropArrowBackColor
        {
            get { return (Color)(ViewState["DropArrowBackColor"] ?? Color.Empty); }
            set { ViewState["DropArrowBackColor"] = value; }
        }

        [DefaultValue("")]
        [UrlProperty]
        [ExtenderControlProperty]
        [ClientPropertyName("dropArrowImageUrl")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Justification = "Using string to avoid Uri complications")]
        public string DropArrowImageUrl
        {
            get { return (string)(ViewState["DropArrowImageUrl"] ?? String.Empty); }
            set { ViewState["DropArrowImageUrl"] = value; }
        }

        [DefaultValue(typeof(Unit), "")]
        [ExtenderControlProperty]
        [ClientPropertyName("dropArrowWidth")]
        public Unit DropArrowWidth
        {
            get { return (Unit)(ViewState["DropArrowWidth"] ?? Unit.Empty); }
            set { ViewState["DropArrowWidth"] = value; }
        }

        [Category("Behavior")]
        [ExtenderControlProperty]
        [ClientPropertyName("dynamicServicePath")]
        [UrlProperty]
        [TypeConverter(typeof(ServicePathConverter))]
        public string DynamicServicePath
        {
            get { return (string)(ViewState["DynamicServicePath"] ?? string.Empty); }
            set { ViewState["DynamicServicePath"] = value; }
        }

        [DefaultValue(true)]
        [ExtenderControlProperty]
        [ClientPropertyName("cacheDynamicResults")]
        public bool CacheDynamicResults
        {
            get { return (bool)(ViewState["CacheDynamicResults"] ?? true); }
            set { ViewState["CacheDynamicResults"] = value; }
        }

        private bool ShouldSerializeDynamicServicePath()
        {
            return !String.IsNullOrEmpty(DynamicServiceMethod);
        }

        [DefaultValue("")]
        [Category("Behavior")]
        [ExtenderControlProperty]
        [ClientPropertyName("dynamicServiceMethod")]
        public string DynamicServiceMethod
        {
            get { return (string)(ViewState["DynamicServiceMethod"] ?? string.Empty); }
            set { ViewState["DynamicServiceMethod"] = value; }
        }

        [DefaultValue("")]
        [Category("Behavior")]
        [ExtenderControlProperty]
        [ClientPropertyName("dynamicContextKey")]
        public string DynamicContextKey
        {
            get { return (string)(ViewState["DynamicContextKey"] ?? string.Empty); }
            set { ViewState["DynamicContextKey"] = value; }
        }

        [DefaultValue("")]
        [Category("Behavior")]
        [ExtenderControlEvent]
        [ClientPropertyName("popup")]
        public string OnClientPopup
        {
            get { return (string)(ViewState["OnClientPopup"] ?? string.Empty); }
            set { ViewState["OnClientPopup"] = value; }
        }

        [DefaultValue("")]
        [Category("Behavior")]
        [ExtenderControlEvent]
        [ClientPropertyName("populating")]
        public string OnClientPopulating
        {
            get { return (string)(ViewState["OnClientPopulating"] ?? string.Empty); }
            set { ViewState["OnClientPopulating"] = value; }
        }

        [DefaultValue("")]
        [Category("Behavior")]
        [ExtenderControlEvent]
        [ClientPropertyName("populated")]
        public string OnClientPopulated
        {
            get { return (string)(ViewState["OnClientPopulated"] ?? string.Empty); }
            set { ViewState["OnClientPopulated"] = value; }
        }
    }
}


