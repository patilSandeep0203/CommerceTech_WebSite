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

public partial class IntuitPayroll : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int AffiliateID = 11;
            HttpCookie ck = Request.Cookies["AffiliateWizAffiliateID"];
            if (ck != null)
                AffiliateID = Convert.ToInt32(ck["AffiliateID"].ToString().Trim());
            lnkOnlineApplication.NavigateUrl = "https://www.firstaffiliates.com/OnlineApplication/Start.aspx?Referral=" + AffiliateID.ToString();
            lnkIntuitPayroll.NavigateUrl = "https://www.youtube.com/watch?v=b2DXG8-1rQM&feature=youtu.be";
            lnkFAQs.NavigateUrl = "https://firstaffiliates.com/Affiliatewiz/faqs.aspx#PR1";
        }
    }
}
