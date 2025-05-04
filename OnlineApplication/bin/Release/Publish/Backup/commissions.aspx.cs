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
using AjaxControlToolkit;

public partial class commissions : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Submit_Click(object sender, EventArgs e)
    {
        try
        {
            lblError.Visible = false;
            double referralsMAMonth = 0;
            if (MAReferrals.Text.Trim() != "")
                referralsMAMonth = Convert.ToDouble(MAReferrals.Text);
            //double DealsPerMonth = DirectRefferalsMonth;
            double monthlyEarnings1 = (referralsMAMonth) * 100;

            double referralsGwyMonth = 0;
            if (MAReferrals.Text.Trim() != "")
                referralsGwyMonth = Convert.ToDouble(GatewayReferrals.Text);
            //double DealsPerMonth = DirectRefferalsMonth;
            double monthlyGatewayEarnings1 = (referralsGwyMonth) * 25;


            double referralsASMonth = 0;
            if (ASReferrals.Text.Trim() != "")
                referralsASMonth = Convert.ToDouble(ASReferrals.Text);
            double monthlyEarnings2 = (referralsASMonth) * 25;

            double referrals2MAMonth = 0;
            if (MAReferrals2.Text.Trim() != "")
                referrals2MAMonth = Convert.ToDouble(MAReferrals2.Text);
            double monthlyAffEarnings1 = (referrals2MAMonth) * 50;

            double referralsGwyMonth2 = 0;
            if (GatewayReferrals.Text.Trim() != "")
                referralsGwyMonth2 = Convert.ToDouble(GatewayReferrals2.Text);
            //double DealsPerMonth = DirectRefferalsMonth;
            double monthlyGatewayEarnings2 = (referralsGwyMonth) * 12.50;

            double referrals2ASMonth = 0;
            if (ASReferrals2.Text.Trim() != "")
                referrals2ASMonth = Convert.ToDouble(ASReferrals2.Text);
            double monthlyAffEarnings2 = (referrals2ASMonth) * 12.50;

            double monthlyMemberEarnings = monthlyEarnings1 + monthlyEarnings2 + monthlyGatewayEarnings1 + monthlyGatewayEarnings2 + monthlyAffEarnings1 + monthlyAffEarnings2;

            //EarningsPerDay.Text = Convert.ToString(Math.Ceiling(monthlyMemberEarnings / 30));
            EarningsPerMonth.Text = "$" + Convert.ToString(monthlyMemberEarnings);
            EarningsPerYear.Text = "$" + Convert.ToString(monthlyMemberEarnings * 12);
        }//end try
        catch (Exception err)
        {
            lblError.Visible = true;
            lblError.Text = "Enter numbers only for input";
        }
    }
}
