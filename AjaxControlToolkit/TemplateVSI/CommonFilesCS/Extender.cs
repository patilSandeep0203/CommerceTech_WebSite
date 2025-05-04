using System;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.ComponentModel;
using System.ComponentModel.Design;
using AjaxControlToolkit;

[assembly: System.Web.UI.WebResource("$rootnamespace$.$baseitemname$Behavior.js", "text/javascript")]

namespace $rootnamespace$
{
    [Designer(typeof($baseitemname$Designer))]
    [ClientScriptResource("$rootnamespace$.$baseitemname$Behavior", "$rootnamespace$.$baseitemname$Behavior.js")]
    [TargetControlType(typeof(Control))]
    public class $baseitemname$Extender : ExtenderControlBase
    {
        // TODO: Add your property accessors here.
        //
        [ExtenderControlProperty]
        [DefaultValue("")]
        public string MyProperty
        {
            get
            {
                return GetPropertyValue("MyProperty", "");
            }
            set
            {
                SetPropertyValue("MyProperty", value);
            }
        }
    }
}
