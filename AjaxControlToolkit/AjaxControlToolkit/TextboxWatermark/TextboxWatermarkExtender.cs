// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Permissive License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
// All other rights reserved.

using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

[assembly: System.Web.UI.WebResource("AjaxControlToolkit.TextboxWatermark.TextboxWatermark.js", "text/javascript")]

namespace AjaxControlToolkit
{
    /// <summary>
    /// TextBoxWatermark extender class definition
    /// </summary>
    [Designer("AjaxControlToolkit.TextBoxWatermarkExtenderDesigner, AjaxControlToolkit")]
    [ClientScriptResource("AjaxControlToolkit.TextBoxWatermarkBehavior", "AjaxControlToolkit.TextboxWatermark.TextboxWatermark.js")]
    [TargetControlType(typeof(IEditableTextControl))]
    public class TextBoxWatermarkExtender : ExtenderControlBase
    {
        public TextBoxWatermarkExtender()
        {
            EnableClientState = true;
        }

        /// <summary>
        /// OnLoad override to register a submit script for each TextBoxWatermark behavior as well as check
        /// to see if it's focused by default
        /// </summary>
        /// <param name="e">arguments</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            string key;
            string script;

            // OnSubmit code runs to clear the watermark text prior to submit so it's not sent to the server
            key = string.Format(CultureInfo.InvariantCulture, "{0}_onSubmit", ID);
            script = string.Format(CultureInfo.InvariantCulture, "var o = $find('{0}'); if(o) {{ o._onSubmit(); }}", BehaviorID);
            ScriptManager.RegisterOnSubmitStatement(this, typeof(TextBoxWatermarkExtender), key, script);

            // If this textbox has default focus, use ClientState to let it know
            ClientState = (string.Compare(Page.Form.DefaultFocus, TargetControlID, StringComparison.InvariantCultureIgnoreCase) == 0) ? "Focused" : null;
        }

        // Constant strings for each property name
        private const string stringWatermarkText = "WatermarkText";
        private const string stringWatermarkCssClass = "WatermarkCssClass";

        /// <summary>
        /// Text to place in the control when watermarked
        /// </summary>
        [ExtenderControlProperty()]
        [RequiredProperty()]
        [DefaultValue("")]
        public string WatermarkText
        {
            get
            {
                return GetPropertyValue(stringWatermarkText, "");
            }
            set
            {
                SetPropertyValue(stringWatermarkText, value);
            }
        }

        /// <summary>
        /// CSS class to apply to the control when watermarked
        /// </summary>
        [ExtenderControlProperty()]
        [DefaultValue("")]
        public string WatermarkCssClass
        {
            get
            {
                return GetPropertyValue(stringWatermarkCssClass, "");
            }
            set
            {
                SetPropertyValue(stringWatermarkCssClass, value);
            }
        }
    }
}
