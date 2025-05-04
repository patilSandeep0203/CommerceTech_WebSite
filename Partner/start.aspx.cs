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
using OnlineAppClassLibrary;

public partial class Questions : System.Web.UI.Page
{
    private static string parameters = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Style errLabel = new Style();
            errLabel.BackColor = System.Drawing.Color.FromArgb(255, 192, 192);
            errLabel.ForeColor = System.Drawing.Color.Black;
            errLabel.BorderColor = System.Drawing.Color.Red;
            errLabel.BorderStyle = BorderStyle.Solid;
            errLabel.BorderWidth = Unit.Pixel(1);
            errLabel.Font.Size = FontUnit.Small;
            lblError.ApplyStyle(errLabel);

            parameters = HttpContext.Current.Request.Url.ToString();
            if ( parameters.IndexOf("?") != 0 )
                parameters = parameters.Substring(parameters.IndexOf("?") + 1);

            //Get Paramaters from URL
            int PID = 0;
            int ReferralID = 0;
            //string RepNum = string.Empty;
            int BID = 0;

            Session.Add("chkMFChanged", "0");

            if (Request.Params.Get("Referral") != null)
            {
                if (Int32.TryParse(Request.Params.Get("Referral").ToString(), out ReferralID))
                    //ReferralID = Convert.ToInt32(Request.Params.Get("Referral"));
                    Session["Referral"] = ReferralID;
                else
                    Response.Redirect("DefaultError.aspx");
            }

            if (Request.Params.Get("BID") != null)
            {
                if (Int32.TryParse(Request.Params.Get("BID").ToString(), out BID))
                {
                    CommonListData Impressions = new CommonListData();
                    Impressions.UpdateImpressions(ReferralID.ToString(), BID.ToString());
                }
                else
                    Response.Redirect("DefaultError.aspx");
            }

            if (Request.Params["PID"] != null)
            {
                if (Int32.TryParse(Request.Params.Get("PID").ToString(), out PID))
                    //PID = Convert.ToInt32(Request.Params.Get("PID"));
                    Session["PID"] = Request.Params.Get("PID");
                else
                    Response.Redirect("DefaultError.aspx");
            }

            //get referralid from affiliatewiz cookie if it exists
            HttpCookie ckPartner = Request.Cookies["AffiliateWizAffiliateID"];
            if (ckPartner != null)
                ReferralID = Convert.ToInt32(ckPartner["AffiliateID"].ToString().Trim());

            //Check if cookie already exists before creating new cookie
            //Create cookie if cookie does not exists so as not to overwrite previous cookie
            //HttpCookie ck = Request.Cookies["OnlineAppInfo"];
            //if (ck == null)
            //{
                //Overwrite existing cookie
                HttpCookie cookie = new HttpCookie("OnlineAppInfo");
                cookie["ReferralID"] = Convert.ToString(ReferralID);
                cookie["PID"] = Convert.ToString(PID);
                cookie.Secure = true;

                cookie.Expires = DateTime.Now.AddMinutes(30);

                Response.Cookies.Add(cookie);
            //}
            
        }//end try
        catch (Exception err)
        {
            CreateOnlineAppLog Log = new CreateOnlineAppLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "start.aspx Error - " + err.Message);
            SetErrorMessage("Error Processing Request. Please contact Technical Support: <a href='mailto:support@commercetech.com?Subject=Online Application Error'> support@commercetech.com. </a>.");
        }
    }
    
    //This function handles the submit button click event
    protected void  btnContinue_Click(object sender, EventArgs e)
    {                
        if (((!rbtn1Y.Checked) && (!rbtn1N.Checked)) || ((!rbtn2Y.Checked) && (!rbtn2N.Checked)) || 
                 ((!rbtn3Y.Checked) && (!rbtn3N.Checked)))
                 //|| ((!rbtn4Y.Checked) && (!rbtn4N.Checked)) || ((!rbtn5Y.Checked) && (!rbtn5N.Checked))) || ((!rbtn6Y.Checked) && (!rbtn6N.Checked))
            SetErrorMessage("Please answer all questions below.");
        else if ((rbtn1Y.Checked) && (rbtn2Y.Checked) && (rbtn3Y.Checked))// && (rbtn5Y.Checked) && (rbtn4Y.Checked) && (rbtn6Y.Checked))
            Response.Redirect("start1.aspx"); //Domestic
        else if ((rbtn1N.Checked) || (rbtn2N.Checked) || (rbtn3N.Checked))// || (rbtn5N.Checked) || (rbtn4N.Checked) || (rbtn6N.Checked))
            Response.Redirect("start2.aspx"); //International
    }//end function submit button click

    //This function displays error messages
    protected void SetErrorMessage(string errText)
    {
        lblError.Visible = true;
        lblError.Text = errText;
    }//end function set error message
}
