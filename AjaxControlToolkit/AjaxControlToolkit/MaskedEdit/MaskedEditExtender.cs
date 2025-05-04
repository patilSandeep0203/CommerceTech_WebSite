// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Permissive License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
// All other rights reserved.
// Product      : MaskedEdit Extender
// Version      : 1.0.0.0
// Date         : 11/08/2006
// Development  : Fernando Cerqueira 
//

using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

[assembly: System.Web.UI.WebResource("AjaxControlToolkit.MaskedEdit.MaskedEditBehavior.js", "text/javascript")]
[assembly: System.Web.UI.WebResource("AjaxControlToolkit.MaskedEdit.MaskedEditValidator.js", "text/javascript")]
namespace AjaxControlToolkit
{

    [Designer("AjaxControlToolkit.MaskedEditDesigner, AjaxControlToolkit")]
    [ClientScriptResource("AjaxControlToolkit.MaskedEditBehavior", "AjaxControlToolkit.MaskedEdit.MaskedEditValidator.js")]
    [ClientScriptResource("AjaxControlToolkit.MaskedEditBehavior", "AjaxControlToolkit.MaskedEdit.MaskedEditBehavior.js")]
    [TargetControlType(typeof(TextBox))]
    public class MaskedEditExtender : ExtenderControlBase
    {

        /// <summary>
        /// Enable client state for communicating default focus
        /// </summary>
        public MaskedEditExtender()
        {
            EnableClientState = true;
        }
        // OnLoad override to to see browser culture / If this textbox has default focus, use ClientState to let it know
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            ((TextBox)this.FindControl(TargetControlID)).MaxLength = 0;

            System.Globalization.CultureInfo BrCult = MaskedEditCommon.GetCultureBrowser("");
            if (!string.IsNullOrEmpty(this.CultureName))
            {
                BrCult = MaskedEditCommon.GetCultureBrowser(this.CultureName);
            }
            SetPropertyValue("Culture", BrCult.ToString());
            SetPropertyValue("CultureAMPMPlaceholder", BrCult.DateTimeFormat.AMDesignator + ";" + BrCult.DateTimeFormat.PMDesignator);
            SetPropertyValue("CultureCurrencySymbolPlaceholder", BrCult.NumberFormat.CurrencySymbol);
            string[] FMT = BrCult.DateTimeFormat.ShortDatePattern.Split(BrCult.DateTimeFormat.DateSeparator.ToCharArray());
            SetPropertyValue("CultureDateFMT", FMT[0].Substring(0, 1).ToUpper(BrCult) + FMT[1].Substring(0, 1).ToUpper(BrCult) + FMT[2].Substring(0, 1).ToUpper(BrCult));
            SetPropertyValue("CultureDatePlaceholder", BrCult.DateTimeFormat.DateSeparator);
            SetPropertyValue("CultureDecimalPlaceholder", BrCult.NumberFormat.NumberDecimalSeparator);
            SetPropertyValue("CultureThousandsPlaceholder", BrCult.NumberFormat.NumberGroupSeparator);
            SetPropertyValue("CultureTimePlaceholder", BrCult.DateTimeFormat.TimeSeparator);
            // If this textbox has default focus, use ClientState to let it know
            ClientState = (string.Compare(Page.Form.DefaultFocus, TargetControlID, StringComparison.InvariantCultureIgnoreCase) == 0) ? "Focused" : null;
        }
        
        [RequiredProperty()]
        [DefaultValue("")]
        [ExtenderControlProperty]
        public string Mask
        {
            get
            {
                return GetPropertyValue("Mask", "");
            }
            set
            {
                if (!validateMaskType())
                {
                    throw new ArgumentException("Validate Type and/or Mask is invalid!");
                }
                SetPropertyValue("Mask", value);
            }
        }
        [DefaultValue(MaskedEditType.None)]
        [ExtenderControlProperty]
        public MaskedEditType MaskType
        {
            get
            {
                return GetPropertyValue("MaskType", MaskedEditType.None);
            }
            set
            {
                SetPropertyValue("MaskType", value);
                switch (value)
                {
                    case MaskedEditType.Date:
                        {
                            SetPropertyValue("AcceptAmPm", false);
                            SetPropertyValue("AcceptNegative", MaskedEditShowSymbol.None);
                            SetPropertyValue("DisplayMoney", MaskedEditShowSymbol.None);
                            break;
                        }
                    case MaskedEditType.None:
                        {
                            SetPropertyValue("AcceptAmPm", false);
                            SetPropertyValue("AcceptNegative", MaskedEditShowSymbol.None);
                            SetPropertyValue("DisplayMoney", MaskedEditShowSymbol.None);
                            break;
                        }
                    case MaskedEditType.Time:
                        {
                            SetPropertyValue("AcceptNegative", MaskedEditShowSymbol.None);
                            SetPropertyValue("DisplayMoney", MaskedEditShowSymbol.None);
                            break;
                        }
                    case MaskedEditType.Number:
                        {
                            SetPropertyValue("AcceptAmPm", false);
                            break;
                        }
                }
            }   
        }
        [DefaultValue(true)]
        [ExtenderControlProperty]
        public bool MessageValidatorTip
        {
            get
            {
                return GetPropertyValue("MessageValidatorTip", true);
            }
            set
            {
                SetPropertyValue("MessageValidatorTip", value);
            }
        }
        [DefaultValue(true)]
        [ExtenderControlProperty]
        public bool AutoComplete
        {
            get
            {
                return GetPropertyValue("AutoComplete", true);
            }
            set
            {
                SetPropertyValue("AutoComplete", value);
            }
        }
        [DefaultValue(false)]
        [ExtenderControlProperty]
        public bool ClearTextOnInvalid
        {
            get
            {
                return GetPropertyValue("ClearTextOnInvalid", false);
            }
            set
            {
                SetPropertyValue("ClearTextOnInvalid", value);
            }
        }
        [DefaultValue("")]
        [ExtenderControlProperty]
        public string AutoCompleteValue
        {
            get
            {
                return GetPropertyValue("AutoCompleteValue", "");
            }
            set
            {
                SetPropertyValue("AutoCompleteValue", value);
            }
        }
        [DefaultValue("")]
        [ExtenderControlProperty]
        public string Filtered
        {
            get
            {
                return GetPropertyValue("Filtered", "");
            }
            set
            {
                SetPropertyValue("Filtered", value);
            }
        }
        [DefaultValue(MaskedEditInputDirection.LeftToRight)]
        [ExtenderControlProperty]
        public MaskedEditInputDirection InputDirection
        {
            get
            {
                return GetPropertyValue<MaskedEditInputDirection>("InputDirection", MaskedEditInputDirection.LeftToRight);
            }
            set
            {
                SetPropertyValue<MaskedEditInputDirection>("InputDirection", value);
            }
        }
        [DefaultValue("_")]
        [ExtenderControlProperty]
        public string PromptCharacter
        {
            get
            {
                return GetPropertyValue("PromptChar", "_");
            }
            set
            {
                SetPropertyValue("PromptChar", value);
            }
        }
        [DefaultValue("MaskedEditFocus")]
        [ExtenderControlProperty]
        public string OnFocusCssClass
        {
            get
            {
                return GetPropertyValue("OnFocusCssClass", "MaskedEditFocus");
            }
            set
            {
                SetPropertyValue("OnFocusCssClass", value);
            }
        }

        [DefaultValue("MaskedEditError")]
        [ExtenderControlProperty]
        public string OnInvalidCssClass
        {
            get
            {
                return GetPropertyValue("OnInvalidCssClass", "MaskedEditError");
            }
            set
            {
                SetPropertyValue("OnInvalidCssClass", value);
            }
        }
        [DefaultValue(true)]
        [ExtenderControlProperty]
        public bool ClearMaskOnLostFocus
        {
            get
            {
                return GetPropertyValue("ClearMaskOnLostfocus", true);
            }
            set
            {
                SetPropertyValue("ClearMaskOnLostfocus", value);
            }
        }
        [DefaultValue("")]
        [ExtenderControlProperty]
        public string CultureName
        {
            get
            {
                return GetPropertyValue("Culture", "");
            }
            set
            {
                SetPropertyValue("Culture", value);
            }
        }
        [Browsable(false)]
        [ExtenderControlProperty]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string CultureDatePlaceholder
        {
            get
            {
                return GetPropertyValue("CultureDatePlaceholder", "/");
            }
            set
            {
                SetPropertyValue("CultureDatePlaceholder", value);
            }
        }
        [Browsable(false)]
        [ExtenderControlProperty]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string CultureTimePlaceholder
        {
            get
            {
                return GetPropertyValue("CultureTimePlaceholder", ":");
            }
            set
            {
                SetPropertyValue("CultureTimePlaceholder", value);
            }

        }
        [Browsable(false)]
        [ExtenderControlProperty]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string CultureDecimalPlaceholder
        {
            get
            {
                return GetPropertyValue("CultureDecimalPlaceholder", ".");
            }
            set
            {
                SetPropertyValue("CultureDecimalPlaceholder", value);
            }
        }
        [Browsable(false)]
        [ExtenderControlProperty]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string CultureThousandsPlaceholder
        {
            get
            {
                return GetPropertyValue("CultureThousandsPlaceholder", ",");
            }
            set
            {
                SetPropertyValue("CultureThousandsPlaceholder", value);
            }
        }
        [Browsable(false)]
        [ExtenderControlProperty]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string CultureDateFormat
        {
            get
            {
                return GetPropertyValue("CultureDateFMT", "MDY");
            }
            set
            {
                SetPropertyValue("CultureDateFMT", value);
            }
        }
        [Browsable(false)]
        [ExtenderControlProperty]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string CultureCurrencySymbolPlaceholder
        {
            get
            {
                return GetPropertyValue("CultureCurrencySymbolPlaceholder", "");
            }
            set
            {
                SetPropertyValue("CultureCurrencySymbolPlaceholder", value);
            }
        }
        [Browsable(false)]
        [ExtenderControlProperty]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member", Justification = "Alternative of AmPm violates another rule")]
        public string CultureAMPMPlaceholder
        {
            get
            {
                return GetPropertyValue("CultureAMPMPlaceholder", "AM;PM");
            }
            set
            {
                SetPropertyValue("CultureAMPMPlaceholder", value);
            }
        }
        [DefaultValue(false)]
        [ExtenderControlProperty]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member", Justification="Alternative of AmPm violates another rule")]
        public bool AcceptAMPM
        {
            get
            {
                return GetPropertyValue("AcceptAmPm", false);
            }
            set
            {
                if (MaskType == MaskedEditType.Time)
                {
                    SetPropertyValue("AcceptAmPm", value);
                }
            }
        }
        [DefaultValue(MaskedEditShowSymbol.None)]
        [ExtenderControlProperty]
        public MaskedEditShowSymbol AcceptNegative
        {
            get
            {
                return GetPropertyValue("AcceptNegative", MaskedEditShowSymbol.None);
            }
            set
            {
                if (MaskType == MaskedEditType.Number)
                {
                    SetPropertyValue("AcceptNegative", value);
                }
            }
        }
        [DefaultValue("MaskedEditFocusNegative")]
        [ExtenderControlProperty]
        public string OnFocusCssNegative
        {
            get
            {
                return GetPropertyValue("OnFocusCssNegative", "MaskedEditFocusNegative");
            }
            set
            {
                SetPropertyValue("OnFocusCssNegative", value);
            }
        }
        [DefaultValue("MaskedEditBlurNegative")]
        [ExtenderControlProperty]
        public string OnBlurCssNegative
        {
            get
            {
                return GetPropertyValue("OnBlurCssNegative", "MaskedEditBlurNegative");
            }
            set
            {
                SetPropertyValue("OnBlurCssNegative", value);
            }
        }
        [DefaultValue(MaskedEditShowSymbol.None)]
        [ExtenderControlProperty]
        public MaskedEditShowSymbol DisplayMoney
        {
            get
            {
                return GetPropertyValue("DisplayMoney", MaskedEditShowSymbol.None);
            }
            set
            {
                if (MaskType == MaskedEditType.Number)
                {
                    SetPropertyValue("DisplayMoney", value);
                }
            }
        }
        [DefaultValue(1900)]
        [ExtenderControlProperty]
        public int Century
        {
            get
            {
                return GetPropertyValue("Century", 1900);
            }
            set
            {
                if (value.ToString(CultureInfo.InvariantCulture).Length != 4)
                {
                    throw new ArgumentException("The Century must have 4 digits.");
                }
                else
                {
                    SetPropertyValue("Century", value);
                }
            }
        }
        private bool validateMaskType()
        {
            bool ret = true;
            string text = Mask;
            MaskedEditType tpvld = MaskType;
            if (!string.IsNullOrEmpty(text) && (tpvld == MaskedEditType.Date || tpvld == MaskedEditType.Time))
            {
                string _maskValid = MaskedEditCommon.GetValidMask(text);
                //date
                if (tpvld == MaskedEditType.Date)
                {
                    ret = false;
                    switch (_maskValid)
                    {
                        case "99/99/9999":
                        case "99/9999/99":
                        case "9999/99/99":
                        case "99/99/99":
                            ret = true;
                            break;
                    }
                }
                //time
                else if (tpvld == MaskedEditType.Time)
                {
                    ret = false;
                    switch (_maskValid)
                    {
                        case "99:99:99":
                        case "99:99":
                            ret = true;
                            break;
                    }
                }
                //Number
                else if (tpvld == MaskedEditType.Number)
                {
                    ret = true;
                    for (int i = 0; i < _maskValid.Length; i++)
                    {
                        if (_maskValid.Substring(i,1) != "9" && _maskValid.Substring(i,1) != "." && _maskValid.Substring(i,1) != ",")
                        {
                            ret = false;
                            break;
                        }
                    }
                }
            }
            return ret;
        }
    }
}
