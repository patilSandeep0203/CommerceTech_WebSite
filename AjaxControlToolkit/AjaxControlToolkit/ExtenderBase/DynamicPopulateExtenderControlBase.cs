// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Permissive License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
// All other rights reserved.

using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AjaxControlToolkit
{
    [RequiredScript(typeof(DynamicPopulateExtender))]
    public abstract class DynamicPopulateExtenderControlBase : AnimationExtenderControlBase
    {
        private const string stringDynamicControlID = "DynamicControlID";
        private const string stringDynamicContextKey = "DynamicContextKey";
        private const string stringDynamicServicePath = "DynamicServicePath";
        private const string stringDynamicServiceMethod = "DynamicServiceMethod";

        [ExtenderControlProperty()]
        [DefaultValue("")]
        [IDReferenceProperty(typeof(WebControl))]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase", Justification = "Following ASP.NET AJAX pattern")]
        public string DynamicControlID
        {
            get { return GetPropertyValue(stringDynamicControlID, ""); }
            set { SetPropertyValue(stringDynamicControlID, value); }
        }

        [ExtenderControlProperty()]
        [DefaultValue("")]
        public string DynamicContextKey
        {
            get { return GetPropertyValue(stringDynamicContextKey, ""); }
            set { SetPropertyValue(stringDynamicContextKey, value); }
        }

        [ExtenderControlProperty()]
        [TypeConverter(typeof(ServicePathConverter))]
        [UrlProperty]
        public string DynamicServicePath
        {
            get { return GetPropertyValue(stringDynamicServicePath, ""); }
            set { SetPropertyValue(stringDynamicServicePath, value); }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        private bool ShouldSerializeServicePath() {
            return !string.IsNullOrEmpty(DynamicServiceMethod);
        }

        [ExtenderControlProperty()]
        [DefaultValue("")]
        public string DynamicServiceMethod
        {
            get { return GetPropertyValue(stringDynamicServiceMethod, ""); }
            set { SetPropertyValue(stringDynamicServiceMethod, value); }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", Justification = "Assembly is not localized")]
        public override void EnsureValid()
        {
            base.EnsureValid();
            // If any properties are set, they must all be valid
            if (!string.IsNullOrEmpty(DynamicControlID) || !string.IsNullOrEmpty(DynamicContextKey) || !string.IsNullOrEmpty(DynamicServicePath) || !string.IsNullOrEmpty(DynamicServiceMethod))
            {
                if (string.IsNullOrEmpty(DynamicControlID))
                {
                    throw new ArgumentException("DynamicControlID must be set");
                }
                // DynamicContextKey optional
                // DynamicServicePath optional
                if (string.IsNullOrEmpty(DynamicServiceMethod))
                {
                    throw new ArgumentException("DynamicServiceMethod must be set");
                }
            }
        }
    }
}
