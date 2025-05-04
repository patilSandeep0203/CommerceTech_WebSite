// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Permissive License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
// All other rights reserved.

using System;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.ComponentModel;
using AjaxControlToolkit;


[assembly: WebResource("AjaxControlToolkit.ListSearch.ListSearchBehavior.js", "text/javascript")]

namespace AjaxControlToolkit
{
    /// <summary>
    /// ListSearch extender class definition
    /// </summary>
    [Description("Lets users search incrementally within ListBoxes")]
    [Designer(typeof(ListSearchDesigner))]
    [ClientScriptResource("AjaxControlToolkit.ListSearchBehavior", "AjaxControlToolkit.ListSearch.ListSearchBehavior.js")]
    [TargetControlType(typeof(ListControl))]
    [RequiredScript(typeof(CommonToolkitScripts), 0)]
    [RequiredScript(typeof(PopupControlExtender), 1)]
    public class ListSearchExtender : ExtenderControlBase
    {
        public ListSearchExtender()
        {
            EnableClientState = true;
        }

        /// <summary>
        /// OnLoad checks to see if it's focused by default
        /// </summary>
        /// <param name="e">arguments</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // If this textbox has default focus, use ClientState to let it know
            ClientState = (string.Compare(Page.Form.DefaultFocus, TargetControlID, StringComparison.InvariantCultureIgnoreCase) == 0) ? "Focused" : null;
        }


        /// <summary>
        ///The prompt text displayed when user clicks the list.
        /// </summary>
        /// <value>The prompt text.</value>
        [Description("The prompt text displayed when user clicks the list")]
        [ExtenderControlProperty]
        [DefaultValue("Type to search")]
        [ClientPropertyName("promptText")]
        public string PromptText
        {
            get
            {
                return GetPropertyValue("promptText", "Type to search");
            }
            set
            {
                SetPropertyValue("promptText", value);
            }
        }

        /// <summary>
        /// CSS class applied to prompt when user clicks list.
        /// </summary>
        /// <value>The prompt CSS class.</value>
        [Description("CSS class applied to prompt when user clicks list")]
        [DefaultValue("")]
        [ExtenderControlProperty()]
        [ClientPropertyName("promptCssClass")]
        public string PromptCssClass
        {
            get
            {
                return GetPropertyValue("promptCssClass", "");
            }
            set
            {
                SetPropertyValue("promptCssClass", value);
            }
        }

        /// <summary>
        /// Indicates where you want the prompt message displayed when the user clicks on the list.
        /// </summary>
        /// <value>The prompt position.</value>
        [Description("Indicates where you want the prompt message displayed when the user clicks on the list.")]
        [DefaultValue(ListSearchPromptPosition.Top)]
        [ExtenderControlProperty()]
        [ClientPropertyName("promptPosition")]
        public ListSearchPromptPosition PromptPosition
        {
            get
            {
                return GetPropertyValue("promptPosition", ListSearchPromptPosition.Top);
            }
            set
            {
                SetPropertyValue("promptPosition", value);
            }
        }

    }
}
