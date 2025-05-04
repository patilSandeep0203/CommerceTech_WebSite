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

public partial class Gateway_company : System.Web.UI.Page
{
    private static int AppId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session.IsNewSession)
            Response.Redirect("/Application/default.aspx?SessionExpired=True");

        Response.CacheControl = "no-cache";
        Response.AddHeader("Pragma", "no-cache");
        Response.Expires = -1;

        if ( Session.IsNewSession )
            Response.Redirect("/Application/default.aspx?SessionExpired=True");
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
            if (!User.Identity.IsAuthenticated)
                Response.Redirect("/Application/default.aspx?Authentication=False");
            else
            {
                try
                {
                    if (!Locked())
                    {                                              
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
                    Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + Session["AppId"].ToString() + " - " + "Gateway company.aspx Page Load Error - " + err.Message);
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
            lblCountry.Text = Server.HtmlEncode(drCompanyInfo["Country"].ToString().Trim());
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

    //This function handles submit button click event
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            CommonFunctions UpdateGeneralInfo = new CommonFunctions(AppId);
            UpdateGeneralInfo.SetGWPageCount();
            Response.Redirect("businessinfo.aspx", false);
        }
        catch (Exception err)
        {
            CreateOnlineAppLog Log = new CreateOnlineAppLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + Session["AppId"].ToString() + " - " + "Gateway company.aspx Error - " + err.Message);
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
