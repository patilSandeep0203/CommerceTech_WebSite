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
using BLAffiliateWiz;

public partial class contactus : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                int AffiliateID = 0;
                HttpCookie ck = Request.Cookies["AffiliateWizAffiliateID"];
                if (ck != null)
                    AffiliateID = Convert.ToInt32(ck["AffiliateID"].ToString().Trim());

                if (AffiliateID != 0)
                {
                    string strEmail = string.Empty;
                    //Get Affiliate Name from Affiliate ID
                    AffiliatesWizBL Affiliate = new AffiliatesWizBL();
                    DataSet ds = Affiliate.GetAffiliateInfoBasic(AffiliateID);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow dr = ds.Tables[0].Rows[0];
                        //show partner address only if partner signup type is Agent. Reseller and Affiliate page wil show our address info.
                        if (dr["Category"].ToString().Trim() == "Agent")
                        {
                            if (dr["CompanyAddress"].ToString().Trim() != null)
                            {
                                pnlPartnerAddressInfo.Visible = true;
                                pnlECEAddressInfo.Visible = false;
                                lblCompanyAddress.Text = "<b>" + dr["CompanyName"].ToString().Trim() + "</b><br />" + dr["CompanyAddress"].ToString().Trim() + "<br />"
                                    + dr["City"].ToString().Trim() + ", " + dr["State"].ToString().Trim() + " " + dr["Zip"].ToString().Trim() +
                                    "<br />Tel: " + dr["Telephone"].ToString().Trim();
                                lblMailingAddress.Text = "<b>Mailing Address</b><br />" + dr["MailingAddress"].ToString().Trim() + "<br />"
                                    + dr["MailingCity"].ToString().Trim() + ", " + dr["MailingState"].ToString().Trim() + " " + dr["MailingZip"].ToString().Trim();
                            }
                        }
                    }
                }

                /*if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    if (dr["SendEmailNotification"].ToString().Trim() == "Yes")
                        strEmail = dr["Email"].ToString().Trim();
                }

                if (strEmail != "")
                {
                    lnkSales.NavigateUrl = "mailto:information@ecenow.com?subject=Sales Inquiry&cc=" + strEmail;
                }
                else
                {
                    lnkSales.NavigateUrl = "mailto:information@ecenow.com?subject=Sales Inquiry";
                }*/
                

            }//end try
            catch (Exception err)
            {
                lblError.Visible = true;
                lblError.Text = "Error loading page";
            }
        }//end if not postback
    }
}
