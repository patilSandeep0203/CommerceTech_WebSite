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
[assembly: System.Web.UI.WebResource("AjaxControlToolkit.PopupExtender.PopupBehavior.js", "text/javascript")]
#endregion

namespace AjaxControlToolkit {
    [Designer("AjaxControlToolkit.PopupExtenderDesigner, AjaxControlToolkit")]
    [ClientScriptResource("AjaxControlToolkit.PopupBehavior", "AjaxControlToolkit.PopupExtender.PopupBehavior.js")]
    [TargetControlType(typeof(Control))]    
    [RequiredScript(typeof(BlockingScripts))]
    [RequiredScript(typeof(CommonToolkitScripts))]
    public class PopupExtender : ExtenderControlBase {

        [ExtenderControlProperty()]
        [IDReferenceProperty()]
        [RequiredProperty()]
        [ClientPropertyName("parentElement")]
        [ElementReference()]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase", Justification = "Following ASP.NET AJAX pattern")]
        public string ParentElementID
        {
            get {
                return GetPropertyValue("ParentElementID", "");
            }
            set {
                SetPropertyValue("ParentElementID", value);
            }
        }

        [ExtenderControlProperty()]
        [DefaultValue(0)]
        [ClientPropertyName("x")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "X", Justification = "Common term")]
        public int X
        {
            get {
                return GetPropertyValue("X", 0);
            }
            set {
                SetPropertyValue("X", value);
            }
        }

        [ExtenderControlProperty()]
        [DefaultValue(0)]
        [ClientPropertyName("y")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Y", Justification = "Common term")]
        public int Y
        {
            get {
                return GetPropertyValue("Y", 0);
            }
            set {
                SetPropertyValue("Y", value);
            }
        }

        [ExtenderControlProperty()]
        [DefaultValue(PositioningMode.Absolute)]
        [ClientPropertyName("positioningMode")]
        public PositioningMode PositioningMode {
            get {
                return GetPropertyValue("PositioningMode", PositioningMode.Absolute);
            }
            set {
                SetPropertyValue("PositioningMode", value);
            }
        }

        [ExtenderControlProperty()]
        [DefaultValue(false)]
        [ClientPropertyName("reparent")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Reparent", Justification="Common term")]
        public bool Reparent
        {
            get { 
                return GetPropertyValue("Reparent", false); 
            }
            set { 
                SetPropertyValue("Reparent", value); 
            }
        }
    }
}
