// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Permissive License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
// All other rights reserved.
// 
// Product      : MaskedEdit Extend/Validator Control
// Version      : 1.0.0.0
// Date         : 10/23/2006
// Development  : Fernando Cerqueira 
// 
using System;
using System.Data;
using System.Configuration;
using System.Globalization;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;

namespace AjaxControlToolkit
{
    public static class MaskedEditCommon
    {
        const string _charEscape = "\\";
        const string _CharsSpecialMask = "/:.,";
        const string _CharsEditMask = "9L$CAN?";
        const string _charNumbers = "0123456789";
        private const string DefaultCulture = "en-US";

        public static CultureInfo GetCultureBrowser(string preferred)
        {
            CultureInfo culture = null;
            if (!string.IsNullOrEmpty(preferred))
            {
                culture = GetSpecificCultureInfo(preferred);
            }
            if (culture == null)
            {
                if (HttpContext.Current.Request.UserLanguages == null)
                {
                    culture = GetSpecificCultureInfo(DefaultCulture);
                }
                else
                {
                    foreach (string name in HttpContext.Current.Request.UserLanguages)
                    {
                        culture = GetSpecificCultureInfo(name);
                        if (culture != null)
                            break;
                    }
                }
            }
            return culture ?? GetSpecificCultureInfo(DefaultCulture);
        }

        /// <summary>
        /// Get a specific culture info
        /// </summary>
        /// <param name="name">Name of the culture</param>
        /// <returns>Specific CultureInfo, or null if no matching specific CultureInfo could be found</returns>
        private static CultureInfo GetSpecificCultureInfo(string name)
        {
            try
            {
                CultureInfo culture = CultureInfo.CreateSpecificCulture(name);
                if (culture != null && !culture.IsNeutralCulture)
                    return culture;
            }
            catch (ArgumentException)
            {
            }
            return null;
        }

        static public int GetFirstMaskPosition(string text)
        {
            bool flagescape = false;
            text = ConvertMask(text);
            int i;
            for (i = 0 ; i < text.Length ; i++) 
            {
                if (text.Substring(i,1) == _charEscape && !flagescape) 
                {
                    flagescape = true;
                }
                else if (_CharsEditMask.IndexOf(text.Substring(i, 1)) != -1 && !flagescape) 
                {
                    return i;
                } 
                else if (flagescape) 
                {
                    flagescape = false;
                } 
            }
            return -1;
        }
        static public int GetLastMaskPosition(string text)
        {
            bool flagescape = false;
            text = ConvertMask(text);
            int LastPos = -1;
            int i;
            for (i = 0; i < text.Length; i++)
            {
                if (text.Substring(i,1) == _charEscape && !flagescape)
                {
                    flagescape = true;
                }
                else if (_CharsEditMask.IndexOf(text.Substring(i, 1)) != -1 && !flagescape)
                {
                    LastPos = i;
                }
                else if (flagescape)
                {
                    flagescape = false;
                }
            }
            return LastPos;
        }
        static public string GetValidMask(string text)
        {
            int i = 0;
            bool flagescape = false;
            StringBuilder _maskValid = new StringBuilder();
            text = ConvertMask(text);
            while (i < text.Length)
            {
                if (text.Substring(i, 1) == "\\" && flagescape == false)
                {
                    flagescape = true;
                }
                else if (_CharsEditMask.IndexOf(text.Substring(i, 1)) == -1)
                {
                    if (flagescape)
                    {
                        flagescape = false;
                    }
                    else
                    {
                        if (_CharsSpecialMask.IndexOf(text.Substring(i, 1)) != -1)
                        {
                            _maskValid.Append(text.Substring(i, 1));
                        }
                    }
                }
                else
                {
                    if (flagescape == true)
                    {
                        flagescape = false;
                    }
                    else
                    {
                        _maskValid.Append(text.Substring(i, 1));
                    }
                }
                i++;
            }
            return _maskValid.ToString();
        }
        static public string ConvertMask(string text)
        {
            if (null == text)
            {
                throw new ArgumentNullException("text");
            }
            StringBuilder MaskConv = new StringBuilder();
            StringBuilder qtdmask = new StringBuilder();
            string maskchar = "";
            int i = 0;
            for (i = 0; i < text.Length; i++)
            {
                if (_CharsEditMask.IndexOf(text.Substring(i, 1)) != -1)
                {
                    if (qtdmask.Length == 0)
                    {
                        MaskConv.Append(text.Substring(i,  1));
                        qtdmask.Length = 0;
                        maskchar = text.Substring(i, 1);
                    }
                    else if (text.Substring(i,  1) == "9")
                    {
                        qtdmask.Append("9");
                    }
                    else if (text.Substring(i, 1) == "0")
                    {
                        qtdmask.Append("0");
                    }
                }
                else if (_CharsEditMask.IndexOf(text.Substring(i, 1)) == -1 && text.Substring(i, 1) != "{" && text.Substring(i, 1) != "}")
                {
                    if (qtdmask.Length == 0)
                    {
                        MaskConv.Append(text.Substring(i,  1));
                        qtdmask.Length = 0;
                        maskchar = "";
                    }
                    else
                    {
                        if (_charNumbers.IndexOf(text.Substring(i,  1)) != -1)
                        {
                            qtdmask.Append(text.Substring(i, 1));
                        }
                    }
                }
                else if (text.Substring(i, 1) == "{" && qtdmask.Length == 0)
                {
                    qtdmask.Length = 0;
                    qtdmask.Append("0");
                }
                else if (text.Substring(i, 1) == "}" && qtdmask.Length != 0)
                {
                    int qtddup = int.Parse(qtdmask.ToString(), CultureInfo.InvariantCulture) - 1;
                    if (qtddup > 0)
                    {
                        int q = 0;
                        for (q = 0; q < qtddup; q++)
                        {
                            MaskConv.Append(maskchar);
                        }
                    }
                    qtdmask.Length = 0;
                    qtdmask.Append("0");
                    maskchar = "";
                }
            }
            return MaskConv.ToString();
        }
    }
}
