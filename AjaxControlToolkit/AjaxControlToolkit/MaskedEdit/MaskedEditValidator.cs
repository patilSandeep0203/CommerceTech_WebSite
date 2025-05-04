// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Permissive License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
// All other rights reserved.
// Product      : MaskedEdit Validator Control
// Version      : 1.0.0.0
// Date         : 10/23/2006
// Development  : Fernando Cerqueira 
// 
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.ComponentModel;
using System.Collections;
using System.Globalization;

[assembly: System.Web.UI.WebResource("AjaxControlToolkit.MaskedEdit.MaskedEditValidator.js", "text/javascript")]

namespace AjaxControlToolkit
{
    public class MaskedEditValidator : System.Web.UI.WebControls.BaseValidator
    {
        bool _IsValidEmpty = true;
        string _MessageTip = "";
        string _MessageInvalid = "";
        string _MessageEmpty = "";
        string _MessageMax = "";
        string _MessageMin = "";
        string _InitialValue = "";
        string _ValidationExpression = "";
        string _ClientValidationFunction = "";
        string _MaximumValue = "";
        string _MinimumValue = "";
        string _ControlExtender = "";
        public event EventHandler<ServerValidateEventArgs> MaskedEditServerValidator;

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        new public string ErrorMessage
        {
            get
            {
                return base.ErrorMessage;
            }
            set
            {
                base.ErrorMessage = value;
            }
        }
        [DefaultValue(true)]
        [Category("MaskedEdit")]
        public bool IsValidEmpty
        {
            get
            {
                return _IsValidEmpty;
            }
            set
            {
                _IsValidEmpty = value;
            }
        }
        [DefaultValue("")]
        [Category("MaskedEdit")]
        public string TooltipMessage
        {
            get
            {
                if (_MessageTip == null)
                {
                    return string.Empty;
                }
                return _MessageTip;
            }
            set
            {
                _MessageTip = value;
            }
        }
        [DefaultValue("")]
        [Category("MaskedEdit")]
        public string EmptyValueMessage
        {
            get
            {
                if (_MessageEmpty == null)
                {
                    return string.Empty;
                }
                return _MessageEmpty;
            }
            set
            {
                _MessageEmpty = value;
            }
        }
        [DefaultValue("")]
        [Category("MaskedEdit")]
        public string InvalidValueMessage
        {
            get
            {
                if (_MessageInvalid == null)
                {
                    return string.Empty;
                }
                return _MessageInvalid;
            }
            set
            {
                _MessageInvalid = value;
            }
        }
        [DefaultValue("")]
        [Category("MaskedEdit")]
        public string MaximumValue
        {
            get
            {
                if (_MaximumValue == null)
                {
                    return string.Empty;
                }
                return _MaximumValue;
            }
            set
            {
                _MaximumValue = value;
            }
        }
        [DefaultValue("")]
        [Category("MaskedEdit")]
        public string MaximumValueMessage
        {
            get
            {
                if (_MessageMax == null)
                {
                    return string.Empty;
                }
                return _MessageMax;
            }
            set
            {
                _MessageMax = value;
            }
        }
        [DefaultValue("")]
        [Category("MaskedEdit")]
        public string ClientValidationFunction
        {
            get
            {
                if (_ClientValidationFunction == null)
                {
                    return string.Empty;
                }
                return _ClientValidationFunction;
            }
            set
            {
                _ClientValidationFunction = value;
            }
        }
        [DefaultValue("")]
        [Category("MaskedEdit")]
        public string InitialValue
        {
            get
            {
                if (_InitialValue == null)
                {
                    return string.Empty;
                }
                return _InitialValue;
            }
            set
            {
                _InitialValue = value;
            }
        }
        [DefaultValue("")]
        [Category("MaskedEdit")]
        public string ValidationExpression
        {
            get
            {
                if (_ValidationExpression == null)
                {
                    return string.Empty;
                }
                return _ValidationExpression;
            }
            set
            {
                _ValidationExpression = value;
            }
        }
        [DefaultValue("")]
        [Category("MaskedEdit")]
        public string MinimumValue
        {
            get
            {
                if (_MinimumValue == null)
                {
                    return string.Empty;
                }
                return _MinimumValue;
            }
            set
            {
                _MinimumValue = value;
            }
        }
        [DefaultValue("")]
        [Category("MaskedEdit")]
        public string MinimumValueMessage
        {
            get
            {
                if (_MessageMin == null)
                {
                    return string.Empty;
                }
                return _MessageMin;
            }
            set
            {
                _MessageMin = value;
            }
        }
        [DefaultValue("")]
        [TypeConverter(typeof(MaskedEditTypeConvert))]
        [RequiredProperty()]
        [Category("MaskedEdit")]
        public string ControlExtender
        {
            get
            {
                if (_ControlExtender == null)
                {
                    return string.Empty;
                }
                return _ControlExtender;
            }
            set
            {
                _ControlExtender = value;
            }
        }
        protected override void OnPreRender(System.EventArgs e )
        {
            if(this.EnableClientScript)
            {
                // register Script Resource at current page
                this.Page.ClientScript.RegisterClientScriptResource(typeof(MaskedEditValidator), "AjaxControlToolkit.MaskedEdit.MaskedEditValidator.js");

                MaskedEditExtender MaskExt = (MaskedEditExtender)FindControl(ControlExtender);
                TextBox Target = (TextBox)MaskExt.FindControl(ControlToValidate);
                
                int FirstMaskPos = -1;
                int LastMaskPosition = -1;
                if (MaskExt.ClearMaskOnLostFocus)
                {
                    FirstMaskPos = 0;
                    LastMaskPosition = MaskedEditCommon.GetValidMask(MaskExt.Mask).Length + 1;
                }
                else
                {
                    FirstMaskPos = MaskedEditCommon.GetFirstMaskPosition(MaskExt.Mask);
                    LastMaskPosition = MaskedEditCommon.GetLastMaskPosition(MaskExt.Mask) + 1;
                }
                this.Attributes.Add("IsMaskedEdit", true.ToString().ToLower(CultureInfo.InvariantCulture));
                this.Attributes.Add("ValidEmpty", this.IsValidEmpty.ToString().ToLower(CultureInfo.InvariantCulture));
                this.Attributes.Add("MaximumValue", this.MaximumValue);
                this.Attributes.Add("MinimumValue", this.MinimumValue);
                this.Attributes.Add("InitialValue", this.InitialValue);
                this.Attributes.Add("ValidationExpression", this.ValidationExpression);
                this.Attributes.Add("ClientValidationFunction", this.ClientValidationFunction);
                this.Attributes.Add("TargetValidator", Target.ClientID);
                this.Attributes.Add("EmptyValueMessage", this.EmptyValueMessage);
                this.Attributes.Add("MaximumValueMessage", this.MaximumValueMessage);
                this.Attributes.Add("MinimumValueMessage", this.MinimumValueMessage);
                this.Attributes.Add("InvalidValueMessage", this.InvalidValueMessage);
                this.Attributes.Add("InvalidValueCssClass", MaskExt.OnInvalidCssClass);
                this.Attributes.Add("CssBlurNegative", MaskExt.OnBlurCssNegative);
                this.Attributes.Add("CssFocus", MaskExt.OnFocusCssClass);
                this.Attributes.Add("CssFocusNegative", MaskExt.OnFocusCssNegative);
                this.Attributes.Add("TooltipMessage", this.TooltipMessage);
                this.Attributes.Add("FirstMaskPosition", FirstMaskPos.ToString(CultureInfo.InvariantCulture));
                switch (MaskExt.MaskType)
                {
                    case MaskedEditType.None:
                        {
                            this.Attributes.Add("evaluationfunction", "MaskedEditValidatorNone");
                            this.Attributes.Add("LastMaskPosition",  LastMaskPosition.ToString(CultureInfo.InvariantCulture));
                            break;
                        }
                    case MaskedEditType.Number:
                        {
                            if (MaskExt.DisplayMoney !=  MaskedEditShowSymbol.None)
                            {
                                LastMaskPosition += MaskExt.CultureCurrencySymbolPlaceholder.Length + 1;
                            }
                            if (MaskExt.AcceptNegative != MaskedEditShowSymbol.None)
                            {
                                if (MaskExt.DisplayMoney != MaskedEditShowSymbol.None) 
                                {
                                    LastMaskPosition++;
                                }
                                else
                                {
                                    LastMaskPosition+=2;
                                }
                            }
                            this.Attributes.Add("Money", MaskExt.CultureCurrencySymbolPlaceholder);
                            this.Attributes.Add("Decimal", MaskExt.CultureDecimalPlaceholder);
                            this.Attributes.Add("Thousands", MaskExt.CultureThousandsPlaceholder);
                            this.Attributes.Add("evaluationfunction", "MaskedEditValidatorNumber");
                            this.Attributes.Add("LastMaskPosition", LastMaskPosition.ToString(CultureInfo.InvariantCulture));
                            break;
                        }
                    case MaskedEditType.Date:
                        {
                            this.Attributes.Add("DateSeparator", MaskExt.CultureDatePlaceholder);
                            this.Attributes.Add("DateFormat", MaskExt.CultureDateFormat);
                            this.Attributes.Add("Century", MaskExt.Century.ToString(CultureInfo.InvariantCulture));
                            this.Attributes.Add("evaluationfunction", "MaskedEditValidatorDate");
                            this.Attributes.Add("LastMaskPosition", LastMaskPosition.ToString(CultureInfo.InvariantCulture));
                            break;
                        }
                    case MaskedEditType.Time:
                        {
                            if (MaskExt.AcceptAMPM)
                            {
                                string[] ASymMask = MaskExt.CultureAMPMPlaceholder.Split(";".ToCharArray());
                                if (ASymMask[0].Length != 0)
                                {
                                    LastMaskPosition += ASymMask[0].Length + 1;
                                }
                            }
                            this.Attributes.Add("TimeSeparator", MaskExt.CultureTimePlaceholder);
                            this.Attributes.Add("AmPmSymbol", MaskExt.CultureAMPMPlaceholder);
                            this.Attributes.Add("evaluationfunction", "MaskedEditValidatorTime");
                            this.Attributes.Add("LastMaskPosition", LastMaskPosition.ToString(CultureInfo.InvariantCulture));
                            break;
                        }
                }
            }
            base.OnPreRender(e);
        }
        protected override bool ControlPropertiesValid()
        {
            return (FindControl(ControlToValidate) is System.Web.UI.WebControls.TextBox);
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification="Control author's attempt to simplify the catching of numerous isolated parsing exceptions")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Cyclomatic complexity issues not currently being addressed")]
        protected override bool EvaluateIsValid()
        {
            MaskedEditExtender MaskExt = (MaskedEditExtender)FindControl(ControlExtender);
            TextBox Target = (TextBox)MaskExt.FindControl(ControlToValidate);
            base.ErrorMessage = "";
            string cssError = "";
            bool ok = true;
            if (!this.IsValidEmpty)
            {
                if (Target.Text.Trim() == this.InitialValue)
                {
                    base.ErrorMessage = this.EmptyValueMessage;
                    cssError = MaskExt.OnInvalidCssClass;
                    ok = false;
                }
            }
            if (ok && Target.Text.Length != 0 && this.ValidationExpression.Length != 0)
            {
                try
                {
                    System.Text.RegularExpressions.Regex Regex = new System.Text.RegularExpressions.Regex(this.ValidationExpression);
                    ok = Regex.IsMatch(Target.Text);
                }
                catch
                {
                    ok = false;
                }
            }
            if (ok && Target.Text.Length != 0)
            {
                string Culture = MaskExt.CultureName;
                if (string.IsNullOrEmpty(Culture))
                {
                    Culture = MaskedEditCommon.GetCultureBrowser("").ToString();
                }
                string CultureAMPMP = "";
                if (!string.IsNullOrEmpty(System.Globalization.CultureInfo.GetCultureInfo(Culture).DateTimeFormat.AMDesignator) && !string.IsNullOrEmpty(System.Globalization.CultureInfo.GetCultureInfo(Culture).DateTimeFormat.PMDesignator))
                {
                    CultureAMPMP = System.Globalization.CultureInfo.GetCultureInfo(Culture).DateTimeFormat.AMDesignator + ";" + System.Globalization.CultureInfo.GetCultureInfo(Culture).DateTimeFormat.PMDesignator;
                }
                switch (MaskExt.MaskType)
                {
                    case MaskedEditType.Number:
                        try
                        {
                            System.Globalization.CultureInfo CultControl = System.Globalization.CultureInfo.GetCultureInfo(Culture);
                            decimal numval = System.Decimal.Parse(Target.Text, CultControl);
                        }
                        catch
                        {
                            ok = false;
                        }
                        break;
                    case MaskedEditType.Date:
                    case MaskedEditType.Time:
                        int tamtext = Target.Text.Length;
                        if (MaskExt.MaskType == MaskedEditType.Time && !string.IsNullOrEmpty(CultureAMPMP))
                        {

                            char[] charSeparators = new char[] { ';' };
                            string[] ArrAMPM = CultureAMPMP.Split(charSeparators);
                            if (ArrAMPM[0].Length != 0)
                            {
                                tamtext -= (ArrAMPM[0].Length + 1);
                            }
                        }
                        if ( MaskedEditCommon.GetValidMask(MaskExt.Mask).Length != tamtext)
                        {
                            ok = false;
                        }
                        if (ok)
                        {
                            try
                            {
                                System.Globalization.CultureInfo CultControl = System.Globalization.CultureInfo.GetCultureInfo(Culture);
                                DateTime dtval = System.DateTime.Parse(Target.Text, CultControl);
                            }
                            catch
                            {
                                ok = false;
                            }
                        }
                        break;
                }
                if (!ok)
                {
                    base.ErrorMessage = this.InvalidValueMessage;
                    cssError = MaskExt.OnInvalidCssClass;
                }
                if (ok && (!string.IsNullOrEmpty(this.MaximumValue) || !string.IsNullOrEmpty(this.MinimumValue)))
                {
                    System.Globalization.CultureInfo CultControl = System.Globalization.CultureInfo.GetCultureInfo(Culture);
                    switch (MaskExt.MaskType)
                    {
                        case MaskedEditType.None:
                            {
                                System.Int32 lenvalue;
                                if (!string.IsNullOrEmpty(this.MaximumValue))
                                {
                                    try
                                    {
                                        lenvalue = System.Int32.Parse(this.MaximumValue, CultControl);
                                        ok = (lenvalue >= Target.Text.Length);
                                    }
                                    catch
                                    {
                                        base.ErrorMessage = this.InvalidValueMessage;
                                        ok = false;
                                    }
                                    if (!ok)
                                    {
                                        base.ErrorMessage = this.MaximumValueMessage;
                                        cssError = MaskExt.OnInvalidCssClass;
                                    }
                                }
                                if (!string.IsNullOrEmpty(this.MinimumValue))
                                {
                                    try
                                    {
                                        lenvalue = System.Int32.Parse(this.MinimumValue, CultControl);
                                        ok = (lenvalue <= Target.Text.Length);
                                    }
                                    catch
                                    {
                                        base.ErrorMessage = this.InvalidValueMessage;
                                        ok = false;
                                    }
                                    if (!ok)
                                    {
                                        base.ErrorMessage = this.MinimumValueMessage;
                                        cssError = MaskExt.OnInvalidCssClass;
                                    }
                                }
                                break;
                            }
                        case MaskedEditType.Number:
                            {
                                decimal numval = System.Decimal.Parse(Target.Text, CultControl);
                                decimal Compval;
                                if (!string.IsNullOrEmpty(this.MaximumValue))
                                {
                                    try
                                    {
                                        Compval = System.Decimal.Parse(this.MaximumValue, CultControl);
                                        ok = (Compval >= numval);
                                    }
                                    catch
                                    {
                                        ok = false;
                                    }
                                    if (!ok)
                                    {
                                        base.ErrorMessage = this.MaximumValueMessage;
                                        cssError = MaskExt.OnInvalidCssClass;
                                    }
                                }
                                if (!string.IsNullOrEmpty(this.MinimumValue))
                                {
                                    try
                                    {
                                        Compval = System.Decimal.Parse(this.MinimumValue, CultControl);
                                        ok = (Compval <= numval);
                                    }
                                    catch
                                    {
                                        ok = false;
                                    }
                                    if (!ok)
                                    {
                                        base.ErrorMessage = this.MinimumValueMessage;
                                        cssError = MaskExt.OnInvalidCssClass;
                                    }
                                }
                                break;
                            }
                        case MaskedEditType.Date:
                        case MaskedEditType.Time:
                            {
                                DateTime dtval = System.DateTime.Parse(Target.Text, CultControl);
                                DateTime dtCompval;
                                if (!string.IsNullOrEmpty(this.MaximumValue))
                                {
                                    try
                                    {
                                        dtCompval = System.DateTime.Parse(this.MaximumValue, CultControl);
                                        ok = (dtCompval >= dtval);
                                    }
                                    catch
                                    {
                                        ok = false;
                                    }
                                    if (!ok)
                                    {
                                        base.ErrorMessage = this.MaximumValueMessage;
                                        cssError = MaskExt.OnInvalidCssClass;
                                    }
                                }
                                if (!string.IsNullOrEmpty(this.MinimumValue))
                                {
                                    try
                                    {
                                        dtCompval = System.DateTime.Parse(this.MinimumValue, CultControl);
                                        ok = (dtCompval <= dtval);
                                    }
                                    catch
                                    {
                                        ok = false;
                                    }
                                    if (!ok)
                                    {
                                        base.ErrorMessage = this.MinimumValueMessage;
                                        cssError = MaskExt.OnInvalidCssClass;
                                    }
                                }
                                break;
                            }
                    }
                }
            }
            if (ok && MaskedEditServerValidator != null)
            {
                ServerValidateEventArgs serverValidateEventArgs = new ServerValidateEventArgs(Target.Text, ok);
                MaskedEditServerValidator(Target, serverValidateEventArgs);
                ok = serverValidateEventArgs.IsValid;
            }
            if (!ok)
            {
                // set CSS at server for browser with not implement client validator script (FF, others)   
                //MaskedEditSetCssClass(value,CSS)
                string Script = "MaskedEditSetCssClass(" + this.ClientID + ",'" + cssError + "');";
                ScriptManager.RegisterStartupScript(this, typeof(MaskedEditValidator), "MaskedEditServerValidator_" + this.ID, Script, true);
            }
            return ok;
        }
    }
}
