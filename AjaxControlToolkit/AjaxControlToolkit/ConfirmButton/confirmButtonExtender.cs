// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Permissive License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
// All other rights reserved.

using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

[assembly: System.Web.UI.WebResource("AjaxControlToolkit.ConfirmButton.confirmButtonBehavior.js", "text/javascript")]

namespace AjaxControlToolkit
{
    /// <summary>
    /// ConfirmButton extender class definition
    /// </summary>
    [Designer("AjaxControlToolkit.ConfirmButtonDesigner, AjaxControlToolkit")]
    [ClientScriptResource("AjaxControlToolkit.ConfirmButtonBehavior", "AjaxControlToolkit.ConfirmButton.confirmButtonBehavior.js")]
    [TargetControlType(typeof(IButtonControl))]
    public class ConfirmButtonExtender : ExtenderControlBase
    {
        /// <summary>
        /// Text to display in the confirm dialog
        /// </summary>
        /// 
        [ExtenderControlProperty]        
        [RequiredProperty]
        public string ConfirmText
        {
            get
            {
                return GetPropertyValue("ConfirmText", "");
            }
            set
            {
                SetPropertyValue("ConfirmText", value);
            }
        }
    }
}
