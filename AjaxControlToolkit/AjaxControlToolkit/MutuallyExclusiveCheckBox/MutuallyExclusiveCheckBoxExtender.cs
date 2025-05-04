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
[assembly: System.Web.UI.WebResource("AjaxControlToolkit.MutuallyExclusiveCheckBox.MutuallyExclusiveCheckBoxBehavior.js", "text/javascript")]
#endregion

namespace AjaxControlToolkit
{
    [Designer("AjaxControlToolkit.MutuallyExclusiveCheckBoxDesigner, AjaxControlToolkit")]
    [TargetControlType(typeof(ICheckBoxControl))]
    [ClientScriptResource("AjaxControlToolkit.MutuallyExclusiveCheckBoxBehavior", "AjaxControlToolkit.MutuallyExclusiveCheckBox.MutuallyExclusiveCheckBoxBehavior.js")]
    public class MutuallyExclusiveCheckBoxExtender : ExtenderControlBase
    {
        [ExtenderControlProperty()]  
        [RequiredProperty]
        public string Key
        {
            get { return GetPropertyValue("Key", string.Empty); }
            set { SetPropertyValue("Key", value); }
        }
    }
}


