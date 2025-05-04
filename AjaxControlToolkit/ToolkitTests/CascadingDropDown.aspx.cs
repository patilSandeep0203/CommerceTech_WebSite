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
using System.Web.Services;
using System.Collections.Specialized;

public partial class Automated_CascadingDropDown : System.Web.UI.Page
{
    protected void Button1_Click(object sender, EventArgs e)
    {
        Label3.Text = string.Format("{0}:{1}:{2}", DropDownList8.SelectedValue, DropDownList8.SelectedItem.Value, DropDownList8.SelectedItem.Text);
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Label1.Text = "SelectedIndexChanged";
    }
    protected void DropDownList7_SelectedIndexChanged(object sender, EventArgs e)
    {
        Label2.Text = "SelectedIndexChanged";
    }
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        Label4.Text = string.Join(",", new string[] { DropDownList1.SelectedItem.Text, DropDownList2.SelectedItem.Text, DropDownList3.SelectedItem.Text, DropDownList3b.SelectedItem.Text });
    }
}
