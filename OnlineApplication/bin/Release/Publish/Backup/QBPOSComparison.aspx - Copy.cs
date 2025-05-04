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

public partial class QBPOSComparison : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void lnkNotes_click(object sender, EventArgs e)
    {
        if (pnlNotes.Visible)
            pnlNotes.Visible = false;
        else
            pnlNotes.Visible = true;
    }
}
