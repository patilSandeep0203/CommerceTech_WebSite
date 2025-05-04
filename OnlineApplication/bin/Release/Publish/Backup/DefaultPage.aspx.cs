using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Demo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void onlineApplication_Click(object sender, EventArgs e)
    {
        int AffiliateID = 0;
        string urlOnlineApp = "";

        HttpCookie ck = Request.Cookies["AffiliateWizAffiliateID"];
        if (ck != null)
            AffiliateID = Convert.ToInt32(ck["AffiliateID"].ToString().Trim());
        if ((AffiliateID != 0) && (AffiliateID != null))
        {


            Response.Redirect("consult.aspx");
        }
        else { Response.Redirect("consult.aspx"); }

    }

    protected void onlinePromo_Click(object sender, EventArgs e)
    {
        int AffiliateID = 0;
        string urlOnlineApp = "";

        HttpCookie ck = Request.Cookies["AffiliateWizAffiliateID"];
        if (ck != null)
            AffiliateID = Convert.ToInt32(ck["AffiliateID"].ToString().Trim());
        if ((AffiliateID != 0) && (AffiliateID != null))
        {


            Response.Redirect("CashAdvance.aspx");
        }
        else { Response.Redirect("CashAdvance.aspx"); }

    }

    protected void lnkNotes_click(object sender, EventArgs e)
    {

    }

    protected void lstNewsletterChanged(Object sender, EventArgs e)
    {

    }
}