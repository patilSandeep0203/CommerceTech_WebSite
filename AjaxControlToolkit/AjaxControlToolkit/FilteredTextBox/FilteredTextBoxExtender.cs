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
[assembly: System.Web.UI.WebResource("AjaxControlToolkit.FilteredTextBox.FilteredTextBoxBehavior.js", "text/javascript")]
#endregion

namespace AjaxControlToolkit
{
    [Designer("AjaxControlToolkit.FilteredTextBoxDesigner, AjaxControlToolkit")]
    [ClientScriptResource("AjaxControlToolkit.FilteredTextBoxBehavior", "AjaxControlToolkit.FilteredTextBox.FilteredTextBoxBehavior.js")]
    [TargetControlType(typeof(TextBox))]
    [DefaultProperty("ValidChars")]
    public class FilteredTextBoxExtender : ExtenderControlBase
    {
        [ExtenderControlProperty]
        [DefaultValue(FilterTypes.Custom)]
        public FilterTypes FilterType
        {
            get
            {
                return GetPropertyValue<FilterTypes>("FilterType", FilterTypes.Custom);
            }
            set
            {
                SetPropertyValue<FilterTypes>("FilterType", value);
            }
        }

        /// <summary>
        /// A string consisting of all valid chars for the text field
        /// </summary>
        [ExtenderControlProperty]
        [DefaultValue("")]
        public string ValidChars
        {
            get
            {
                return GetPropertyValue("ValidChars", "");
            }
            set
            {
                SetPropertyValue("ValidChars", value);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", Justification = "Assembly is not localized")]
        protected override bool CheckIfValid(bool throwException)
        {
            if (base.CheckIfValid(throwException))
            {
                if (FilterType == FilterTypes.Custom && ValidChars.Length == 0)
                {
                    if (throwException)
                    {
                        throw new System.InvalidOperationException("If FilterTypes.Custom is specified, please provide a value for ValidChars");
                    }
                    else
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }
    }
}
