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

public partial class Affiliate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                int AffiliateID = 11;
                HttpCookie ck = Request.Cookies["AffiliateWizAffiliateID"];
                if (ck != null)
                    AffiliateID = Convert.ToInt32(ck["AffiliateID"].ToString().Trim());

                //Get Affiliate Name from Affiliate ID
                AffiliatesWizBL Affiliate = new AffiliatesWizBL();
                DataSet ds = Affiliate.GetAffiliateInfoBasic(AffiliateID);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];                    
                    /*if (dr["Category"].ToString().Trim().ToLower().Contains("reseller"))
                        imgEceLogo.ImageUrl = "images/eceresellerlogo.gif";
                    else if (dr["Category"].ToString().Trim().ToLower().Contains("agent"))
                        imgEceLogo.ImageUrl = "images/eceagentlogo.gif";*/
                }
            }//end try
            catch (Exception err)
            {
                lblError.Visible = true;
                lblError.Text = "Error Loading Index";
            }
        }//end if ispostback
    }
}
