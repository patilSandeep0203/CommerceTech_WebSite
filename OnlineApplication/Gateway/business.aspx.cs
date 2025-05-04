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

public partial class Gateway_business : System.Web.UI.Page
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
                    Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + Session["AppId"].ToString() + " - " + "Gateway business.aspx Page Load Error - " + err.Message);
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
            lblProductsSold.Text = Server.HtmlEncode(drBusinessInfo["ProductSold"].ToString().Trim());
            
            //This code displays only the last 4 digits of the Federal Tax ID
            string TaxID = Server.HtmlEncode(drBusinessInfo["FederalTaxID"].ToString().Trim());
            string TID = "";
            int j = 0;
            if (TaxID.Length >= 4)
            {
                for (int i = 0; i < TaxID.Length - 4; i++)
                {
                    TID += "x";
                    j++;
                }
            }
            TaxID = TaxID.Substring(j);
            TID += TaxID;
            lblTaxID.Text = TID;
        }//end if business table count not 0       
    }//end function populate

    //This function handles the submit button click event
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            CommonFunctions UpdateGeneralInfo = new CommonFunctions(AppId);
            UpdateGeneralInfo.SetGWPageCount();
            Response.Redirect("principalinfo.aspx", false);
        }
        catch (Exception err)
        {
            CreateOnlineAppLog Log = new CreateOnlineAppLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + Session["AppId"].ToString() + " - " + "Gateway business.aspx Error - " + err.Message);
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

