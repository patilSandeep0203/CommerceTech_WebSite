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

public partial class UpdateAcctGW : System.Web.UI.Page
{
    static string referrer = string.Empty;
    private static int AppId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        AppId = Convert.ToInt32(Session["AppId"]);
        if (Session.IsNewSession)
            Response.Redirect("/Application/default.aspx?SessionExpired=True");

        Style errLabel = new Style();
        errLabel.BackColor = System.Drawing.Color.FromArgb(255, 192, 192);
        errLabel.ForeColor = System.Drawing.Color.Black;
        errLabel.BorderColor = System.Drawing.Color.Red;
        errLabel.BorderStyle = BorderStyle.Solid;
        errLabel.BorderWidth = Unit.Pixel(1);
        errLabel.Font.Size = FontUnit.Small;
        lblError.ApplyStyle(errLabel);

        Style headerLabel = new Style();
        headerLabel.ForeColor = System.Drawing.Color.White;
        lblRegistrationHeader.ApplyStyle(headerLabel);

        if (!IsPostBack)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                    Response.Redirect("/Application/default.aspx?Authentication=False");

                if (Request.UrlReferrer != null)
                    referrer = Request.UrlReferrer.AbsoluteUri.ToString();
                else
                    Response.Redirect("defaulterror.aspx", false);
                if (referrer.Contains("Gateway"))
                {
                    lblRegistrationHeader.Text = "Merchant Account Registration";
                    lblFooter.Text = "Click on Continue to sign up for a Merchant Account with the above information.";
                }
                else if (referrer.Contains("Merchant"))
                {
                    lblRegistrationHeader.Text = "Payment Gateway Registration";
                    lblFooter.Text = "Click on Continue to sign up for a Payment Gateway with the above information.";
                }
                else
                    lblRegistrationHeader.Text = "Registration";

                PopulateFields();
            }//end try
            catch (Exception err)
            {
                CreateOnlineAppLog Log = new CreateOnlineAppLog();
                Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + Session["AppId"].ToString() + " - " + "Update Account Error - " + err.Message);
                SetErrorMessage("Error Processing Request. Please contact Technical Support: <a href='mailto:support@commercetech.com?Subject=Online Application Error'> support@commercetech.com. </a>.");
            }
        }//end if ispostback
    }//end page load

    //This function registers users for both merchant and gateway accounts and sets accounttype to 4
    public void RegisterGatewayMerchant()
    {       
        string AType = "";
        //Update Status to incomplete in online app new app
        if ((Convert.ToInt32(Session["AcctType"]) == 1)||(Convert.ToInt32(Session["AcctType"]) == 3))
        {
            AType = "Gateway";

            NewAppInfo App = new NewAppInfo(AppId);
            int PID = App.ReturnPID();
            PackageInfo Package = new PackageInfo();
            DataSet dsPackageInfo = Package.GetPackageInfo(PID);
            DataTable dtPackageInfo = dsPackageInfo.Tables[0];

            //Update Acct Type in online app profile
            OnlineAppProfile UpdateAType = new OnlineAppProfile(AppId);

            if (Convert.ToInt32(Session["AcctType"]) == 1)
            {
                bool retVal = UpdateAType.UpdateAcctType(4);
            }
            else if (Convert.ToInt32(Session["AcctType"]) == 3)
            {
                bool retVal = UpdateAType.UpdateAcctType(2);
            }

            if (dtPackageInfo.Rows.Count > 0)
            {
                //Insert GW info in Gateway Table
                DataRow drPackageInfo = dtPackageInfo.Rows[0];
                Gateway GatewayInfo = new Gateway(AppId);
                if (Convert.ToString(drPackageInfo["Gateway"]) != "")
                {
                    GatewayInfo.UpdateGatewayInfo(drPackageInfo["Gateway"].ToString().Trim(),
                        drPackageInfo["GatewayMonFee"].ToString().Trim(), drPackageInfo["GatewaySetupFee"].ToString().Trim(),
                        drPackageInfo["GatewayTransFee"].ToString().Trim());
                }

                //Insert GW Sales Opps
                SalesOppsBL SalesOppsGW = new SalesOppsBL(AppId);
                SalesOppsGW.CreateSalesOppsGW();


            } 
        }
        else
            Response.Redirect("defaulterror.aspx", false);

        NewAppInfo AppInfo = new NewAppInfo(AppId);
        string MerchStatus = AppInfo.ReturnStatus();
        //if ((MerchStatus.ToLower().Contains("completed")) || (MerchStatus.ToLower().Contains("submitted")) || (MerchStatus.ToLower().Contains("active")))
        if (!(MerchStatus.ToLower().Contains("incomplete")))
        {
            AppInfo.UpdateReprogramStatus(1);
            AppInfo.UpdateStatus("COMPLETED", AType);
        }
        else if (MerchStatus.ToLower().Contains("incomplete"))
            AppInfo.UpdateStatus("INCOMPLETE", AType);

        CommonFunctions Common = new CommonFunctions(AppId);
        Common.UpdateLastModified();
        Common.SetGWPageCount();


    }    

    //This function handles checking and inserting data in tables on submit button click event
    protected void btnContinue_Click(object sender, EventArgs e)
    {
        try
        {
            //Get cardpct info to check if card pct have been entered. Used for redirecting
            //user to cardpct.aspx or companyinfo.aspx
            CardPCT CardPCTInfo = new CardPCT(AppId);
            DataSet dsCardPCTInfo = CardPCTInfo.GetCardPCT();

            RegisterGatewayMerchant();
            //Session["AcctType"] = 4;

            if ((Convert.ToInt32(Session["AcctType"]) == 1) || (Convert.ToInt32(Session["AcctType"]) == 3))
            {
                Response.Redirect("/Application/Gateway/companyinfo.aspx", false);
            }
            /*
            if (referrer.Contains("Gateway"))
            {
                if (dsCardPCTInfo.Tables["OnlineAppCardPCT"].Rows.Count == 0)
                    Response.Redirect("/Application/Merchant/cardpct.aspx", false);
                else
                    Response.Redirect("/Application/Merchant/companyinfo.aspx", false);
            }
            else if (referrer.Contains("Merchant"))
                Response.Redirect("/Application/Gateway/companyinfo.aspx", false);
            else
            {
                if (dsCardPCTInfo.Tables["OnlineAppCardPCT"].Rows.Count == 0)
                    Response.Redirect("/Application/Merchant/cardpct.aspx", false);
                else
                    Response.Redirect("/Application/Merchant/companyinfo.aspx", false);
            }*/

            if (Convert.ToInt32(Session["AcctType"]) == 2)
            {
                Session["AcctType"] = 4;
            }
            else if (Convert.ToInt32(Session["AcctType"]) == 3)
            {
                Session["AcctType"] = 2;
            }

        }//end try
        catch (Exception err)
        {
            CreateOnlineAppLog Log = new CreateOnlineAppLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + Session["AppId"].ToString() + " - " + "Update Account Error - " + err.Message);
            SetErrorMessage("Error Processing Request. Please contact Technical Support: <a href='mailto:support@commercetech.com?Subject=Online Application Error'> support@commercetech.com. </a>.");
        }

    }//end function btn click
  
    //This function populates fields if AcctType is 4
    public void PopulateFields()
    {
        int AppId = Convert.ToInt32(Session["AppId"]);
        //Get registration info from newapp table and populate fields
        OnlineAppProfile NewAppProfile = new OnlineAppProfile(AppId);
        DataSet dsNewApp = NewAppProfile.GetProfileData();        
        if (dsNewApp.Tables[0].Rows.Count > 0)
        {
            DataRow drNewApp = dsNewApp.Tables[0].Rows[0];

            lblTitleValue.Text = Server.HtmlEncode(drNewApp["Title"].ToString().Trim());
            lblFirstNameValue.Text = Server.HtmlEncode(drNewApp["FirstName"].ToString().Trim());
            lblLastNameValue.Text = Server.HtmlEncode(drNewApp["LastName"].ToString().Trim());
            lblPhoneValue.Text = Server.HtmlEncode(drNewApp["Phone"].ToString().Trim());
            lblPhoneExtValue.Text = Server.HtmlEncode(drNewApp["PhoneExt"].ToString().Trim());
            lblMobilePhoneValue.Text = Server.HtmlEncode(drNewApp["MobilePhone"].ToString().Trim());
            lblHomePhoneValue.Text = Server.HtmlEncode(drNewApp["HomePhone"].ToString().Trim());
        }//end if count not 0        
    }//end function populate fields

    //This function displays error messages
    protected void SetErrorMessage(string errText)
    {
        lblError.Visible = true;
        lblError.Text = errText;
    }//end function set error message

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(referrer, false);
    }
}
