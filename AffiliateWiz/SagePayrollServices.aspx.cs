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

public partial class SagePayrollServices : System.Web.UI.Page
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
            lnkSagePayroll.NavigateUrl = "http://na.sage.com/~/media/Category/Accounting/peachtree/Assets/lp/sageAdvisor/html_gettingstarted/006_mng_emp_payrl/gs-06-mng_emp_pyrl.html";
        }
    }

    protected void lnkNotes_click(object sender, EventArgs e)
    {
        if (pnlNotes.Visible)
            pnlNotes.Visible = false;
        else
            pnlNotes.Visible = true;
    }
}

