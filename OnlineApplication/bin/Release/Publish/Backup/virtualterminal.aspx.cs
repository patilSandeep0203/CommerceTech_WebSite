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

public partial class virtualterminal : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void lnkbtnExample_Click(object sender, EventArgs e)
    {
        if (lnkbtnExample.Text.Contains("View"))
        {
            pnlExample.Visible = true;
            lnkbtnExample.Text = "Hide Example";
        }
        else if (lnkbtnExample.Text.Contains("Hide"))
        {
            pnlExample.Visible = false;
            lnkbtnExample.Text = "View Example";
        }
    }
}
