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

#region Assembly Resource Attribute
[assembly: System.Web.UI.WebResource("AjaxControlToolkit.HoverMenu.HoverMenuBehavior.js", "text/javascript")]
#endregion

namespace AjaxControlToolkit
{
    /// <summary>
    /// Extender class for associating the HoverMenu behavior with a server element.
    /// 
    /// The HoverMenuExtender allows a control to be shown as a popup next to another control when the mouse 
    /// pointer is moved over it.  This popup control can have any server content in it.
    /// </summary>
    [Designer("AjaxControlToolkit.HoverMenuDesigner, AjaxControlToolkit")]
    [ClientScriptResource("AjaxControlToolkit.HoverMenuBehavior", "AjaxControlToolkit.HoverMenu.HoverMenuBehavior.js")]
    [RequiredScript(typeof(CommonToolkitScripts))]    
    [RequiredScript(typeof(HoverExtender))]
    [RequiredScript(typeof(PopupExtender))]
    [TargetControlType(typeof(Control))]
    public class HoverMenuExtender : DynamicPopulateExtenderControlBase
    {
        public HoverMenuExtender() {
        }
                
        // Constant strings for each property name
        private const string stringPopupControlID = "PopupControlID";

        /// <summary>
        /// The server ID of the control that will be displayed when the mouse hovers
        /// over the target element.
        /// </summary>
        [IDReferenceProperty(typeof(WebControl))]
        [RequiredProperty()]
        [DefaultValue("")]
        [ExtenderControlProperty()]
        [ClientPropertyName("popupElement")]
        [ElementReference()]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase", Justification = "Following ASP.NET AJAX pattern")]
        public string PopupControlID {
            get {
                return GetPropertyValue(stringPopupControlID, "");
            }
            set {
                SetPropertyValue(stringPopupControlID, value);
            }
        }

        /// <summary>
        /// The CSS class to apply to the target element when the mouse is over it or the popup element.
        /// </summary>
        [DefaultValue("")]
        [ExtenderControlProperty()]
        public string HoverCssClass {
            get {
                return GetPropertyValue("HoverCssClass", "");
            }
            set {
                SetPropertyValue("HoverCssClass", value);
            }
        }

        /// <summary>
        /// The additional offset to apply between the target element and the popup element when it is displayed.  
        /// By default, the elements will be positioned with no space between them.  By specifying a value here,
        /// that amount of space, in pixels, will be added to the positioning.  The number can be negative to cause an overlap.
        /// </summary>
        [DefaultValue(0)]
        [ExtenderControlProperty()]
        public int OffsetX {
            get {
                return GetPropertyValue("OffsetX", 0);
            }
            set {
                SetPropertyValue("OffsetX", value);
            }
        }

        /// <summary>
        /// The additional offset to apply between the target element and the popup element when it is displayed.  
        /// By default, the elements will be positioned with no space between them.  By specifying a value here,
        /// that amount of space, in pixels, will be added to the positioning.  The number can be negative to cause an overlap.
        /// </summary>
        [DefaultValue(0)]
        [ExtenderControlProperty()]
        public int OffsetY {
            get {
                return GetPropertyValue("OffsetY", 0);
            }
            set {
                SetPropertyValue("OffsetY", value);
            }
        }

        /// <summary>
        /// The time, in milliseconds between then the mouse pointer exits the target element and when the
        /// popup element is hidden.
        /// </summary>
        [DefaultValue(0)]
        [ExtenderControlProperty()]
        public int PopDelay {
            get {
                return GetPropertyValue("PopDelay", 0);
            }
            set {
                SetPropertyValue("PopDelay", value);
            }
        }

        /// <summary>
        /// The position of the popup element when it is shown.  This value can be any of the values 
        /// in the HoverMenuPopupPosition enumeration, and the position can be modified by setting the OffsetX and/or OffsetY property.
        /// </summary>
        [DefaultValue(HoverMenuPopupPosition.Center)]
        [ExtenderControlProperty()]
        public HoverMenuPopupPosition PopupPosition {
            get {
                return GetPropertyValue("Position", HoverMenuPopupPosition.Center);
            }
            set {
                SetPropertyValue("Position", value);
            }
        }
    }
}
