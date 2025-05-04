// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Permissive License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
// All other rights reserved.
// 
// Product      : MaskedEdit Extend Control
// Version      : 1.0.0.0
// Date         : 10/23/2006
// Development  : Fernando Cerqueira 
// 
using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using AjaxControlToolkit;

namespace AjaxControlToolkit
{
    public enum MaskedEditType
    {
        None = 0,
        Date = 1,
        Number = 2,
        Time = 3
    }
    public enum MaskedEditInputDirection
    {
        LeftToRight = 0,
        RightToLeft = 1
    }
    public enum MaskedEditShowSymbol
    {
        None = 0,
        Left = 1,
        Right = 2
    }
}

