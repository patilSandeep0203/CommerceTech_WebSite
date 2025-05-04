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

[assembly: System.Web.UI.WebResource("AjaxControlToolkit.PopupControl.PopupControlBehavior.js", "text/javascript")]

namespace AjaxControlToolkit
{
    /// <summary>
    /// PopupControl extender class definition
    /// </summary>
    [Designer("AjaxControlToolkit.PopupControlDesigner, AjaxControlToolkit")]
    [ClientScriptResource("AjaxControlToolkit.PopupControlBehavior", "AjaxControlToolkit.PopupControl.PopupControlBehavior.js")]
    [RequiredScript(typeof(PopupExtender))]
    [RequiredScript(typeof(CommonToolkitScripts))]
    [TargetControlType(typeof(Control))]
    public class PopupControlExtender : DynamicPopulateExtenderControlBase
    {
        private Page _proxyForCurrentPopup;

        /// <summary>
        /// Gets a proxy PopupControlExtender representing the currently active popup on the specified page
        /// </summary>
        /// <remarks>
        /// Only the Cancel and Commit methods should be called on the proxy
        /// </remarks>
        public static PopupControlExtender GetProxyForCurrentPopup(Page page)
        {
            PopupControlExtender popupControlExtender = new PopupControlExtender();
            popupControlExtender._proxyForCurrentPopup = page;
            return popupControlExtender;
        }

        /// <summary>
        /// Cancels the popup control and hides it, abandoning any results it has
        /// </summary>
        public void Cancel()
        {
            // Pass the magic cancel string as the result
            Close("$$CANCEL$$");
        }

        /// <summary>
        /// Commits the popup control and hides it, applying the specified result
        /// </summary>
        /// <param name="result">result of popup</param>
        public void Commit(string result)
        {
            Close(result);
        }

        /// <summary>
        /// Closes the popup control, applying the specified result or abandoning it
        /// </summary>
        /// <param name="result">result of popup</param>
        private void Close(string result)
        {
            if (null == _proxyForCurrentPopup)
            {
                // Normal call - Simply register the relevant data item for the TargetControl
                ScriptManager.GetCurrent(Page).RegisterDataItem(TargetControl, result);
            }
            else
            {
                // Proxy call - Add a LiteralControl to pass the information down to the interested PopupControlExtender
                LiteralControl literalControl = new LiteralControl();
                literalControl.ID = "_PopupControl_Proxy_ID_";
                _proxyForCurrentPopup.Controls.Add(literalControl);
                ScriptManager.GetCurrent(_proxyForCurrentPopup).RegisterDataItem(literalControl, result);
            }
        }

        // Constant strings for each property name
        private const string stringEntenderControlID = "ExtenderControlID";
        private const string stringPopupControlID = "PopupControlID";
        private const string stringCommitProperty = "CommitProperty";
        private const string stringCommitScript = "CommitScript";
        private const string stringPosition = "Position";
        private const string stringOffsetX = "OffsetX";
        private const string stringOffsetY = "OffsetY";

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase", Justification = "Following ASP.NET AJAX pattern")]
        public string ExtenderControlID
        {
            get
            {
                return GetPropertyValue(stringEntenderControlID, "");
            }
            set
            {
                SetPropertyValue(stringEntenderControlID, value);
            }
        }

        /// <summary>
        /// ID of the control that pops up
        /// </summary>
        [ExtenderControlProperty()]
        [IDReferenceProperty(typeof(WebControl))]
        [RequiredProperty()]
        [DefaultValue("")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase", Justification = "Following ASP.NET AJAX pattern")]
        public string PopupControlID
        {
            get
            {
                return GetPropertyValue(stringPopupControlID, "");
            }
            set
            {
                SetPropertyValue(stringPopupControlID, value);
            }
        }

        /// <summary>
        /// Optional name of the target control's property to set with the result of the popup
        /// </summary>
        /// <remarks>
        /// If not present, the default "value" property will be used
        /// </remarks>
        /// 
        [ExtenderControlProperty()]
        [DefaultValue("")]
        public string CommitProperty
        {
            get
            {
                return GetPropertyValue(stringCommitProperty, "");
            }
            set
            {
                SetPropertyValue(stringCommitProperty, value);
            }
        }

        /// <summary>
        /// Optional script to run after setting the target control's property
        /// </summary>
        /// <remarks>
        /// Allows more complicated actions to be performed based on the popup results
        /// </remarks>
        /// 
        [ExtenderControlProperty()]
        [DefaultValue("")]
        public string CommitScript
        {
            get
            {
                return GetPropertyValue(stringCommitScript, "");
            }
            set
            {
                SetPropertyValue(stringCommitScript, value);
            }
        }

        /// <summary>
        /// Optional position for the popup window (relative to the target control)
        /// </summary>
        [ExtenderControlProperty()]
        [DefaultValue(PopupControlPopupPosition.Center)]
        public PopupControlPopupPosition Position
        {
            get
            {
                return GetPropertyValue(stringPosition, PopupControlPopupPosition.Center);
            }
            set
            {
                SetPropertyValue(stringPosition, value);
            }
        }

        /// <summary>
        /// Optional X (horizontal) offset for the popup window (relative to the target control)
        /// </summary>
        [ExtenderControlProperty()]
        [DefaultValue(0)]
        public int OffsetX
        {
            get
            {
                return GetPropertyValue(stringOffsetX, 0);
            }
            set
            {
                SetPropertyValue(stringOffsetX, value);
            }
        }

        /// <summary>
        /// Optional Y (vertical) offset for the popup window (relative to the target control)
        /// </summary>
        [ExtenderControlProperty()]
        [DefaultValue(0)]
        public int OffsetY
        {
            get
            {
                return GetPropertyValue(stringOffsetY, 0);
            }
            set
            {
                SetPropertyValue(stringOffsetY, value);
            }
        }
    }

    /// <summary>
    /// Enumeration of valid values for the Position property
    /// </summary>
    public enum PopupControlPopupPosition
    {
        Center = 0,
        Top = 1,
        Left = 2,
        Bottom = 3,
        Right = 4
    }
}
