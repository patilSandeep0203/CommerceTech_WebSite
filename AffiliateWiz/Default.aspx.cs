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
            //urlOnlineApp = "https://www.firstaffiliates.com/OnlineApplication/Start.aspx?Referral=" + AffiliateID.ToString();

            Response.Redirect("consult.aspx");
        }
        else { Response.Redirect("consult.aspx"); }
        //Response.Redirect("https://www.firstaffiliates.com/OnlineApplication/Start.aspx");        
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
            //urlOnlineApp = "https://www.firstaffiliates.com/OnlineApplication/Start.aspx?Referral=" + AffiliateID.ToString();

            Response.Redirect("RevelPOS.aspx");
        }
        else { Response.Redirect("RevelPOS.aspx"); }
        //Response.Redirect("https://www.firstaffiliates.com/OnlineApplication/Start.aspx");        
    }

    protected void lnkNotes_click(object sender, EventArgs e)
    {
        if (pnlNotes.Visible)
            pnlNotes.Visible = false;
        else
            pnlNotes.Visible = true;
    }

    protected void lstNewsletterChanged(Object sender, EventArgs e)
    {
        //ClientScript.RegisterStartupScript(this.GetType(), "window", "<script language=""Javascript"">window.open (""" + DropDownList1.SelectedValue + """, ""mywindow"",""location=0,status=0,scrollbars=0,width=400,height=400"");</script>"); 

        if (DropDownListNewsletters.SelectedItem.Value == "GiftLoyalty")
        {
            Response.Redirect("https://www.firstaffiliates.com/Newsletters/GETI_GiftCards-NonCust.html");
        }
        else if (DropDownListNewsletters.SelectedItem.Value == "POSSoftware")
        {
            Response.Redirect("https://www.firstaffiliates.com/Newsletters/POS_Solutions-NonCust.htm");
        }
        else if (DropDownListNewsletters.SelectedItem.Value == "POSEquipment")
        {
            Response.Redirect("https://www.firstaffiliates.com/Newsletters/POS_Equipment_NonCust.html");
        }
        else if (DropDownListNewsletters.SelectedItem.Value == "MerchantServices")
        {
            Response.Redirect("https://www.firstaffiliates.com/Newsletters/Merchant Services_NonCust.html");
        }
        else if (DropDownListNewsletters.SelectedItem.Value == "WirelessServices")
        {
            Response.Redirect("https://www.firstaffiliates.com/Newsletters/Merchant Services_NonCust.html");
        }
        else if (DropDownListNewsletters.SelectedItem.Value == "Sage50Accounting")
        {
            Response.Redirect("https://www.firstaffiliates.com/Newsletters/Sage50_NonCust.html");
        }
        else if (DropDownListNewsletters.SelectedItem.Value == "MerchantCashAdvance")
        {
            Response.Redirect("https://www.firstaffiliates.com/Newsletters/MCA-Loan-NonCustomers.htm");
        }
        else if (DropDownListNewsletters.SelectedItem.Value == "PaymentGateways")
        {
            Response.Redirect("https://www.firstaffiliates.com/Newsletters/Payment_Gateways-NonCust.htm");
        }
        else if (DropDownListNewsletters.SelectedItem.Value == "CheckServices")
        {
            Response.Redirect("https://www.firstaffiliates.com/Newsletters/Check_Services-NonCust.htm");
        }
        else if (DropDownListNewsletters.SelectedItem.Value == "PayrollServices")
        {
            Response.Redirect("https://www.firstaffiliates.com/Newsletters/Payroll_NonCust.html");
        }
        else if (DropDownListNewsletters.SelectedItem.Value == "pcAmerica")
        {
            Response.Redirect("https://www.firstaffiliates.com/Newsletters/pcAmerica_noncust.html");
        }
        else if (DropDownListNewsletters.SelectedItem.Value == "IntuitQuickbooksAccounting")
        {
            Response.Redirect("https://www.firstaffiliates.com/Newsletters/QuickBooks_NonCust.html");
        }
    }
}