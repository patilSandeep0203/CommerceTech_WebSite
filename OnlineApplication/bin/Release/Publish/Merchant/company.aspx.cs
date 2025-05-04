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

public partial class Merchant_company : System.Web.UI.Page
{
    private static int AppId = 0;
    private static string strSW = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session.IsNewSession)
            Response.Redirect("/Application/default.aspx?SessionExpired=True");

        Response.CacheControl = "no-cache";
        Response.AddHeader("Pragma", "no-cache");
        Response.Expires = -1;
        
        AppId = Convert.ToInt32(Session["AppId"]);
        
        Style errLabel = new Style();
        errLabel.BackColor = System.Drawing.Color.FromArgb(255, 192, 192);
        errLabel.ForeColor = System.Drawing.Color.Black;
        errLabel.BorderColor = System.Drawing.Color.Red;
        errLabel.BorderStyle = BorderStyle.Solid;
        errLabel.BorderWidth = Unit.Pixel(1);
        errLabel.Font.Size = FontUnit.Small;
        lblError.ApplyStyle(errLabel);

        if (!IsPostBack)
        {
            /*HttpCookie ck = Request.Cookies.Get("firstaffiliates");
            if (ck == null)
                Response.Redirect("/Application/default.aspx?AuthenticationCookie=False");
             */
            if (!User.Identity.IsAuthenticated)
                Response.Redirect("/Application/default.aspx?Authentication=False");
            else
            {
                try
                {
                    if (!Locked())
                    {
                        if (Request.Params.Get("RatesChanged") != null)
                        {
                            if (Request.Params.Get("RatesChanged").ToString().Trim() == "true")
                            {
                                SetErrorMessage("Your Visa/MC Discount Rate and Per Authorization rates have changed. Please click on view rates to view your new rates.");
                            }
                        }
                        PopulatePage();
                    }
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
                    Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + Session["AppId"].ToString() + " - " + "Merchant company.aspx Page Load Error - " + err.Message);
                     SetErrorMessage("Error Processing Request. Please contact Technical Support: <a href='mailto:support@commercetech.com?Subject=Online Application Error'> support@commercetech.com. </a>.");
                }
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


    //This function populates the page with data from the database
    protected void PopulatePage()
    {
       
        //Get company info from onlineappcompanyinfo
        CompanyInfo Company = new CompanyInfo(AppId);
        DataSet dsCompanyInfo = Company.GetCompanyInfo();
        if (dsCompanyInfo.Tables["OnlineAppCompanyInfo"].Rows.Count > 0)
        {
            DataRow drCompanyInfo = dsCompanyInfo.Tables["OnlineAppCompanyInfo"].Rows[0];

            lblCompanyName.Text = Server.HtmlEncode(drCompanyInfo["CompanyName"].ToString().Trim());
            lblDBA.Text = Server.HtmlEncode(drCompanyInfo["DBA"].ToString().Trim());
            lblAddress.Text = Server.HtmlEncode(drCompanyInfo["Address"].ToString().Trim());
            lblAddress2.Text = Server.HtmlEncode(drCompanyInfo["Address2"].ToString().Trim());
            lblCity.Text = Server.HtmlEncode(drCompanyInfo["City"].ToString().Trim());
            lblZip.Text = Server.HtmlEncode(drCompanyInfo["ZipCode"].ToString().Trim());

            lblState.Text = Server.HtmlEncode(drCompanyInfo["State"].ToString().Trim());
            lblCountry.Text =  Server.HtmlEncode(drCompanyInfo["Country"].ToString().Trim());
            lblRegion.Text = Server.HtmlEncode(drCompanyInfo["Region"].ToString().Trim());
            lblTIBLYears.Text = Server.HtmlEncode(drCompanyInfo["YABL"].ToString().Trim());
            lblTIBLMonths.Text = Server.HtmlEncode(drCompanyInfo["MABL"].ToString().Trim());
            lblBusinessHours.Text = Server.HtmlEncode(drCompanyInfo["BusinessHours"].ToString().Trim());
            lblPhone.Text = Server.HtmlEncode(drCompanyInfo["BusinessPhone"].ToString().Trim());
            lblExt.Text = Server.HtmlEncode(drCompanyInfo["BusinessPhoneExt"].ToString().Trim());
            lblWebsite.Text = Server.HtmlEncode(drCompanyInfo["Website"].ToString().Trim());
            lblFax.Text = Server.HtmlEncode(drCompanyInfo["Fax"].ToString().Trim());
            lblCustomerServicePhone.Text = Server.HtmlEncode(drCompanyInfo["CustServPhone"].ToString().Trim());

        }//end if count not 0
    }//end function populate page

    /*protected void btnPOS2013Yes_Click(object sender, EventArgs e)
    {
        Response.Redirect("ContactPOS2013.aspx", false);
    }*/

    /*protected void btnPOS2013No_Click(object sender, EventArgs e)
    {
        Response.Redirect("https://merchant.intuit.com/signup/?a=chkt&api_key=inTuIt&b=posmswoc&offer_code=posms_023_n-445-16463", false);
    }*/

    //This function handles submit button click event
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        CreatePDF proc = new CreatePDF(AppId);
        string processor = proc.ReturnProcessorName();
        string pid = proc.ReturnPackageId();

        try
        {
            CommonFunctions General = new CommonFunctions(AppId);
            General.SetPageCount();
            General.SetGWPageCount(); //update gateway page count since information recorded is either same or more than those required for Gateway Account
            
                //Response.Redirect("businessinfo.aspx", false);
            NewAppInfo AppInfo = new NewAppInfo(AppId);
            string Status = AppInfo.ReturnStatus();

            if ((pid == "254") && (Status.ToLower().Contains("completed")))
            {
                Response.Redirect("ContactPOS2013.aspx", false);
                ////lblUser.Text = "Are you using or planing to use Intuit QuickBooks POS V.11 or 2013 for this account?";
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
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + Session["AppId"].ToString() + " - " + "Merchant company.aspx Error - " + err.Message);
            SetErrorMessage("Error Processing Request. Please contact Technical Support: <a href='mailto:support@commercetech.com?Subject=Online Application Error'> support@commercetech.com. </a>.");
        }
    }//end function submit button click

    //This function displays error messages
    protected void SetErrorMessage(string errText)
    {
        lblError.Visible = true;
        lblError.Text = errText;
    }//end function set error message    
}
