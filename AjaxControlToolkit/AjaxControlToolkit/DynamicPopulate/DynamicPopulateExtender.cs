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
[assembly: System.Web.UI.WebResource("AjaxControlToolkit.DynamicPopulate.DynamicPopulateBehavior.js", "text/javascript")]
#endregion

namespace AjaxControlToolkit
{
    /// <summary>
    /// This extender allows you to dynamically populate the innerHTML of a control based on a 
    /// web service callback.
    /// </summary>
    [Designer("AjaxControlToolkit.DynamicPopulateDesigner, AjaxControlToolkit")]
    [RequiredScript(typeof(BlockingScripts))]
    [ClientScriptResource("AjaxControlToolkit.DynamicPopulateBehavior", "AjaxControlToolkit.DynamicPopulate.DynamicPopulateBehavior.js")]
    [TargetControlType(typeof(Control))]
    public class DynamicPopulateExtender : ExtenderControlBase
    {
        [DefaultValue(true)]
        [ExtenderControlProperty()]
        public bool ClearContentsDuringUpdate {
            get {
                return GetPropertyValue("ClearContentsDuringUpdate", true);
            }
            set {
                SetPropertyValue("ClearContentsDuringUpdate", value);
            }
        }

        /// <summary>
        /// An key that you can pass to the web service call, such as an ID.  This is optional.
        /// </summary>
        [DefaultValue("")]
        [ExtenderControlProperty()]
        public string ContextKey {
            get {
                return GetPropertyValue("ContextKey", "");
            }
            set {
                SetPropertyValue("ContextKey", value);
            }
        }

        /// <summary>
        /// The ID of a control to trigger the population of the target.  The population will
        /// be triggered by this controls "click" event.
        /// </summary>
        [IDReferenceProperty(typeof(Control))]
        [ExtenderControlProperty()]
        [ClientPropertyName("PopulateTriggerID")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase", Justification = "Following ASP.NET AJAX pattern")]
        public string PopulateTriggerControlID {
            get {
                return GetPropertyValue("PopulateTriggerControlID", "");
            }
            set {
                SetPropertyValue("PopulateTriggerControlID", value);
            }
        }

        /// <summary>
        /// The method name of the web service of page method to call.
        /// </summary>
        [DefaultValue("")]
        [ExtenderControlProperty()]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", Justification = "Assembly is not localized")]
        public string ServiceMethod {
            get {
                return GetPropertyValue("ServiceMethod", "");
            }
            set {

                if (!string.IsNullOrEmpty(CustomScript)) {
                    throw new System.InvalidOperationException("ServiceMethod can not be set if a CustomScript is set.");
                }

                SetPropertyValue("ServiceMethod", value);
            }
        }

        /// <summary>
        /// The path of the web service to call, or if this is blank, a page method will be
        /// called instead.
        /// </summary>        
        [ExtenderControlProperty()]
        [UrlProperty()]
        [TypeConverter(typeof(ServicePathConverter))]
        public string ServicePath {
            get {

                return GetPropertyValue("ServicePath", "");
            }
            set {
                SetPropertyValue("ServicePath", value);
            }
        }

        private bool ShouldSerializeServicePath() {
            return !string.IsNullOrEmpty(ServiceMethod);
        }

        /// <summary>
        /// A CSS style to apply while the update is in progress
        /// </summary>
        [DefaultValue("")]
        [ExtenderControlProperty()]
        public string UpdatingCssClass {
            get {
                return GetPropertyValue("UpdatingCss", "");
            }
            set {
                SetPropertyValue("UpdatingCss", value);
            }
        }

        /// <summary>
        /// A CustomScript can be used to eval a javascript function that will return a string to populate the control.  This script method
        /// must return a string and will be called instead of the Service or Page method.
        /// </summary>
        [DefaultValue("")]
        [ExtenderControlProperty()]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", Justification = "Assembly is not localized")]
        public string CustomScript {
            get {
                return GetPropertyValue("CustomScript", "");
            }
            set {
                if (!string.IsNullOrEmpty(ServiceMethod)) {
                    throw new System.InvalidOperationException("CustomScript can not be set if a ServiceMethod is set.");
                }

                SetPropertyValue("CustomScript", value);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", Justification = "Assembly is not localized")]        
        protected override bool CheckIfValid(bool throwException) {
            string errorMessage = null;

            if (string.IsNullOrEmpty(CustomScript) && string.IsNullOrEmpty(ServiceMethod)) {
                errorMessage = "CustomScript or ServiceMethod must be set.";
            }

            if (errorMessage != null) {
                if (throwException) {
                    throw new System.InvalidOperationException(errorMessage);
                } else {
                    return false;
                }
            }

            return base.CheckIfValid(throwException);
        }
    }
}
