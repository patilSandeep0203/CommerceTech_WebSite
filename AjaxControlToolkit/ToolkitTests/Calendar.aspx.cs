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

public partial class Automated_Calendar : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        ScriptManager scriptManager = ScriptManager.GetCurrent(this);
        scriptManager.EnableScriptLocalization = true;
        scriptManager.EnableScriptGlobalization = true;

    }

}
