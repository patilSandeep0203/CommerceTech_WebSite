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

public partial class Sage50Comparision : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btn2_click(object sender, EventArgs e)
    {
        if (pnl2.Visible)
            pnl2.Visible = false;
        else
            pnl2.Visible = true;
    }
    protected void btn3_click(object sender, EventArgs e)
    {
        if (pnl3.Visible)
            pnl3.Visible = false;
        else
            pnl3.Visible = true;
    }
    protected void btn4_click(object sender, EventArgs e)
    {
        if (pnl4.Visible)
            pnl4.Visible = false;
        else
            pnl4.Visible = true;
    }
    protected void btn5_click(object sender, EventArgs e)
    {
        if (pnl5.Visible)
            pnl5.Visible = false;
        else
            pnl5.Visible = true;
    }
    protected void btn7_click(object sender, EventArgs e)
    {
        if (pnl7.Visible)
            pnl7.Visible = false;
        else
            pnl7.Visible = true;
    }
    protected void btn8_click(object sender, EventArgs e)
    {
        if (pnl8.Visible)
            pnl8.Visible = false;
        else
            pnl8.Visible = true;
    }

    protected void lnkNotes_click(object sender, EventArgs e)
    {
        if (pnlNotes.Visible)
            pnlNotes.Visible = false;
        else
            pnlNotes.Visible = true;
    }
}
