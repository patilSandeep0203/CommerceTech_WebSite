// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Permissive License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
// All other rights reserved.

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using AjaxControlToolkit;


public partial class Automated_PopupControl : System.Web.UI.Page
{
    protected void Button1_Click(object sender, EventArgs e)
    {
        PopupControlExtender1.Commit("Commit");
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        PopupControlExtender1.Commit("");
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        PopupControlExtender1.Cancel();
    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        PopupControlExtender.GetProxyForCurrentPopup(Page).Commit("Commit");
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        PopupControlExtender.GetProxyForCurrentPopup(Page).Commit("");
    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        PopupControlExtender.GetProxyForCurrentPopup(Page).Cancel();
    }
}
