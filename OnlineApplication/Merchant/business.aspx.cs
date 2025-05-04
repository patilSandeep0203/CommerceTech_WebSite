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
using System.Data.SqlClient;
using OnlineAppClassLibrary;

public partial class Merchant_business : System.Web.UI.Page
{
    private static int AppId = 0;
    DataSet dsBusinessInfo = null;
    DataRow drBusinessInfo = null;
    protected void Page_Load(object sender, EventArgs e)
    {
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

        Response.CacheControl = "no-cache";
        Response.AddHeader("Pragma", "no-cache");
        Response.Expires = -1;

        AppId = Convert.ToInt32(Session["AppId"]);

        if (!IsPostBack)
        {
            if (!User.Identity.IsAuthenticated)
                Response.Redirect("/Application/default.aspx?Authentication=False");
            else
                try
                {
                    if (!Locked())
                        PopulatePage();
                    else
                    {
                        PopulatePage();
                        SetErrorMessage("The status of your application prevents it from being edited");
                        btnEditInfo.Enabled = false;
                        btnSubmit.Enabled = false;
                    }
                }
                catch (Exception err)
                {
                    CreateOnlineAppLog Log = new CreateOnlineAppLog();
                    Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + Session["AppId"].ToString() + " - " + "Merchant business.aspx Page Load Error - " + err.Message);
                    SetErrorMessage("Error Processing Request. Please contact Technical Support: <a href=mailto:support@commercetech.com?Subject=Online App Error, App ID: " + Session["AppId"].ToString() + "> support@commercetech.com. </a>: <a href=mailto:support@commercetech.com?Subject=Online App Error, App ID: " + Session["AppId"].ToString() + "> support@commercetech.com. </a>.");
                }
        }
    }

    //This function checks if the application is locked
    protected bool Locked()
    {
        if (Session["Locked"].ToString().Trim() == "Yes")
            return true;
        else
            return false;
    }

    //This function populates page
    protected void PopulatePage()
    {
        //Get business info from onlineappbusinessinfo
        BusinessInfo Business = new BusinessInfo(AppId);
        dsBusinessInfo = Business.GetBusinessInfo();
        if (dsBusinessInfo.Tables[0].Rows.Count > 0)
        {
            drBusinessInfo = dsBusinessInfo.Tables[0].Rows[0];

            lblAddress.Text = Server.HtmlEncode(drBusinessInfo["BillingAddress"].ToString().Trim());
            lblAddress2.Text = Server.HtmlEncode(drBusinessInfo["BillingAddress2"].ToString().Trim());
            lblCity.Text = Server.HtmlEncode(drBusinessInfo["BillingCity"].ToString().Trim());
            lblZipCode.Text = Server.HtmlEncode(drBusinessInfo["BillingZipCode"].ToString().Trim());
            lblRegion.Text = Server.HtmlEncode(drBusinessInfo["BillingRegion"].ToString().Trim());
            lblCountry.Text = Server.HtmlEncode(drBusinessInfo["BillingCountry"].ToString().Trim());
            lblState.Text = Server.HtmlEncode(drBusinessInfo["BillingState"].ToString().Trim());
      
            lblOwnership.Text = Server.HtmlEncode(drBusinessInfo["LegalStatus"].ToString().Trim());
            lblYIB.Text = Server.HtmlEncode(drBusinessInfo["YearsinBusiness"].ToString().Trim());
            lblMIB.Text = Server.HtmlEncode(drBusinessInfo["MonthsinBusiness"].ToString().Trim());
            lblApproxDays.Text = Server.HtmlEncode(drBusinessInfo["NumDaysDelivered"].ToString().Trim());
            lblNumLocs.Text = Server.HtmlEncode(drBusinessInfo["NumberofLocations"].ToString().Trim());
            lblOtherProcessor.Text = Server.HtmlEncode(drBusinessInfo["OtherProcessor"].ToString().Trim());
            
            lblProductsSold.Text = Server.HtmlEncode(drBusinessInfo["ProductSold"].ToString().Trim());
            lblReasonForLeaving.Text = Server.HtmlEncode(drBusinessInfo["ReasonforLeaving"].ToString().Trim());
            
            lblAddlComments.Text = Server.HtmlEncode(drBusinessInfo["AddlComments"].ToString().Trim());
            lblRefund.Text = Server.HtmlEncode(drBusinessInfo["RefundPolicy"].ToString().Trim());
            lblOtherRefund.Text = Server.HtmlEncode(drBusinessInfo["OtherRefund"].ToString().Trim());
            lblBankruptcy.Text = Server.HtmlEncode(drBusinessInfo["Bankruptcy"].ToString().Trim());
            lblPrevProcessed.Text = Server.HtmlEncode(drBusinessInfo["PrevProcessed"].ToString().Trim());
            lblCTMF.Text = Server.HtmlEncode(drBusinessInfo["CTMF"].ToString().Trim());
            
            if (drBusinessInfo["PrevProcessed"].ToString().Trim() == "Yes")
                pnlPrevProcessed.Visible = true;
            else if (drBusinessInfo["PrevProcessed"].ToString().Trim() == "No")
                pnlPrevProcessed.Visible = false;
            else
                pnlPrevProcessed.Visible = false;

            lblPrevProcessor.Text = Server.HtmlEncode(drBusinessInfo["PrevProcessor"].ToString().Trim());

            //This code displays only the last 4 digits of the Federal Tax ID
            string TaxID = Server.HtmlEncode(drBusinessInfo["FederalTaxID"].ToString().Trim());

            if (TaxID.Length > 0)
                TaxID = "xxxxxx" + TaxID;
   
            lblTaxID.Text = TaxID;

            if (drBusinessInfo["PrevMerchantAcctNo"].ToString().Trim() != "")
                lblPrevMerchantAcctNo.Text = "xxxxxxxxxxxx" + drBusinessInfo["PrevMerchantAcctNo"].ToString().Trim();            

        }//end if business table count not 0       
    }//end function populate

    protected void btnPOS2013Yes_Click(object sender, EventArgs e)
    {
        Response.Redirect("ContactPOS2013.aspx", false);
    }

    protected void btnPOS2013No_Click(object sender, EventArgs e)
    {
        Response.Redirect("https://merchant.intuit.com/signup/?a=chkt&api_key=inTuIt&b=posmswoc&offer_code=posms_023_n-445-16463", false);
    }

    //This function handles the submit button click event
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        CreatePDF proc = new CreatePDF(AppId);
        string processor = proc.ReturnProcessorName();
        string pid = proc.ReturnPackageId();
        try
        {
            CommonFunctions UpdateGeneralInfo = new CommonFunctions(AppId);
            UpdateGeneralInfo.SetPageCount();
           // Response.Redirect("principalinfo.aspx", false);
            NewAppInfo AppInfo = new NewAppInfo(AppId);
            string Status = AppInfo.ReturnStatus();

            if ((pid == "254") && (Status.ToLower().Contains("completed")))
            {
                Response.Redirect("ContactPOS2013.aspx", false);
                //lblUser.Text = "Are you using or planing to use Intuit QuickBooks POS V.11 or 2013 for this account?";
                //ModalPopupExtender1.Show();
                //ClientScript.RegisterStartupScript(this.GetType(), "PopupScript", "ConfirmPOS2013();", true);
            }
            else
            {
                AppInfo.checkPageRedirect();
            }
        }
        catch (Exception err)
        {
            CreateOnlineAppLog Log = new CreateOnlineAppLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + Session["AppId"].ToString() + " - " + "Merchant business.aspx Error - " + err.Message);
            SetErrorMessage("Error Processing Request. Please contact Technical Support: <a href=mailto:support@commercetech.com?Subject=Online App Error, App ID: " + Session["AppId"].ToString() + "> support@commercetech.com. </a>: <a href=mailto:support@commercetech.com?Subject=Online App Error, App ID: " + Session["AppId"].ToString() + "> support@commercetech.com. </a>: <a href=mailto:support@commercetech.com?Subject=Online App Error, App ID: " + Session["AppId"].ToString() + "> support@commercetech.com. </a>.");
        }
    }//end function submit button click

    //This function displays error messages
    protected void SetErrorMessage(string errText)
    {
        lblError.Visible = true;
        lblError.Text = errText;
    }//end function set error message
}
