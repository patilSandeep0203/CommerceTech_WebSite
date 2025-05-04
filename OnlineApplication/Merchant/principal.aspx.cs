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

public partial class Merchant_principal : System.Web.UI.Page
{
    private static int AppId = 0;
    private static string strSW = string.Empty;
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
                    Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + Session["AppId"].ToString() + " - " + "Merchant principal.aspx Page Load Error - " + err.Message);
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

    //This function populates form fields
    protected void PopulatePage()
    {        
        //Get principal info from onlineappprincipal info
        PrincipalInfo Principal = new PrincipalInfo(AppId);
        DataSet dsPrincipal1Info = Principal.GetPrincipalInfo();
        if (dsPrincipal1Info.Tables[0].Rows.Count > 0)
        {
            DataRow drPrincipal1Info = dsPrincipal1Info.Tables[0].Rows[0];
            //Populate Principal 1 information
            lblP1FirstName.Text = drPrincipal1Info["P1FirstName"].ToString().Trim();
            lblP1LastName.Text = drPrincipal1Info["P1LastName"].ToString().Trim();
            lblP1MiddleName.Text = drPrincipal1Info["P1MidName"].ToString().Trim();
            lblP1Email.Text = drPrincipal1Info["P1Email"].ToString().Trim();
            lblP1Title.Text = drPrincipal1Info["P1Title"].ToString().Trim();
            lblP1Address.Text = drPrincipal1Info["P1Address"].ToString().Trim();
            lblP1Address2.Text = drPrincipal1Info["P1Address2"].ToString().Trim();
            lblP1City.Text = drPrincipal1Info["P1City"].ToString().Trim();
            lblP1State.Text = drPrincipal1Info["P1State"].ToString().Trim();
            lblP1ZipCode.Text = drPrincipal1Info["P1ZipCode"].ToString().Trim();
            lblP1Region.Text = drPrincipal1Info["P1Region"].ToString().Trim();
            lblP1Country.Text = drPrincipal1Info["P1Country"].ToString().Trim();
            lblP1YearsAtAddress.Text = drPrincipal1Info["P1YearsatAddress"].ToString().Trim();
            lblP1MonthsAtAddress.Text = drPrincipal1Info["P1MonthsatAddress"].ToString().Trim();
            lblP1HomePhone.Text = drPrincipal1Info["P1PhoneNumber"].ToString().Trim();
            lblP1MobilePhone.Text = drPrincipal1Info["P1MobilePhone"].ToString().Trim();
            lblP1DriversLicenseState.Text = drPrincipal1Info["P1DriversLicenseState"].ToString().Trim();
            lblP1DriversLicenseExp.Text = drPrincipal1Info["P1driversLicenseExpiry"].ToString().Trim();
            lblP1DOB.Text = drPrincipal1Info["P1DOB"].ToString().Trim();
            lblP1RentOwn.Text = drPrincipal1Info["P1LivingStatus"].ToString().Trim();            
            lblP1Ownership.Text = drPrincipal1Info["P1OwnershipPercent"].ToString().Trim();

            string SecondPrincipal = drPrincipal1Info["HasSecondPrincipal"].ToString().Trim();
            if (SecondPrincipal == "Yes")
            {
                btnYes.Checked = true;
                pnlPrincipal2.Visible = true;
                //get principal 2 info
                PopulateSecondPrincipal();                
            }
            else
            {
                btnNo.Checked = true;
                pnlPrincipal2.Visible = false;                
            }

            //This code displays only the last 4 digits of the SSN
            string P1SSN = Server.HtmlEncode(drPrincipal1Info["P1SSN"].ToString().Trim());
            if (P1SSN.Length > 0)
                P1SSN = "xxxxxx" + P1SSN;
            lblP1SSN.Text = P1SSN;

            //This code displays only the last 4 digits of the Drivers License Number
            string P1DLN = Server.HtmlEncode(drPrincipal1Info["P1driversLicenseNo"].ToString().Trim());
            if (P1DLN.Length > 0)
                P1DLN = "xxxxxx" + P1DLN;          
            lblP1DriversLicenseNo.Text = P1DLN;

        }//end if principal count not 0
        else
        {
            btnNo.Checked = true;
            pnlPrincipal2.Visible = false;
        }
    }//end function populate principal 1 info

    protected void btnPOS2013Yes_Click(object sender, EventArgs e)
    {
        Response.Redirect("ContactPOS2013.aspx", false);
    }

    protected void btnPOS2013No_Click(object sender, EventArgs e)
    {
        Response.Redirect("https://merchant.intuit.com/signup/?a=chkt&api_key=inTuIt&b=posmswoc&offer_code=posms_023_n-445-16463", false);
    }

    //This function populates second principal information
    protected void PopulateSecondPrincipal()
    {
        Principal2Info Principal2 = new Principal2Info(AppId);
        DataSet dsPrincipal2Info = Principal2.GetPrincipal2Info();
        if (dsPrincipal2Info.Tables[0].Rows.Count > 0)
        {
            DataRow drPrincipal2Info = dsPrincipal2Info.Tables[0].Rows[0];
            //Populate Principal 2 information
            lblP2FirstName.Text = drPrincipal2Info["P2FirstName"].ToString().Trim();
            lblP2LastName.Text = drPrincipal2Info["P2LastName"].ToString().Trim();
            lblP2MiddleName.Text = drPrincipal2Info["P2MidName"].ToString().Trim();
            lblP2Email.Text = drPrincipal2Info["P2Email"].ToString().Trim();
            lblP2Title.Text = drPrincipal2Info["P2Title"].ToString().Trim();
            lblP2Address.Text = drPrincipal2Info["P2Address"].ToString().Trim();
            lblP2Address2.Text = drPrincipal2Info["P2Address2"].ToString().Trim();
            lblP2City.Text = drPrincipal2Info["P2City"].ToString().Trim();
            lblP2State.Text = drPrincipal2Info["P2State"].ToString().Trim();
            lblP2ZipCode.Text = drPrincipal2Info["P2ZipCode"].ToString().Trim();
            lblP2Region.Text = drPrincipal2Info["P2Region"].ToString().Trim();
            lblP2Country.Text = drPrincipal2Info["P2Country"].ToString().Trim();
            lblP2YearsAtAddress.Text = drPrincipal2Info["P2YearsatAddress"].ToString().Trim();
            lblP2MonthsAtAddress.Text = drPrincipal2Info["P2MonthsatAddress"].ToString().Trim();
            lblP2HomePhone.Text = drPrincipal2Info["P2PhoneNumber"].ToString().Trim();
            lblP2MobilePhone.Text = drPrincipal2Info["P2MobilePhone"].ToString().Trim();
            lblP2DriversLicenseState.Text = drPrincipal2Info["P2DriversLicenseState"].ToString().Trim();
            lblP2DriversLicenseExp.Text = drPrincipal2Info["P2driversLicenseExpiry"].ToString().Trim();
            lblP2DOB.Text = drPrincipal2Info["P2DOB"].ToString().Trim();
            lblP2RentOwn.Text = drPrincipal2Info["P2LivingStatus"].ToString().Trim();            
            lblP2Ownership.Text = drPrincipal2Info["P2OwnershipPercent"].ToString().Trim();

            //Precede the truncated data with X's
            string P2SSN = Server.HtmlEncode(drPrincipal2Info["P2SSN"].ToString().Trim());
            if (P2SSN.Length > 0)
                P2SSN = "xxxxxx" + P2SSN;
            lblP2SSN.Text = P2SSN;

            //Precede the truncated data with X's
            string P2DLN = Server.HtmlEncode(drPrincipal2Info["P2driversLicenseNo"].ToString().Trim());
            if (P2DLN.Length > 0)
                P2DLN = "xxxxxx" + P2DLN; 
            lblP2DriversLicenseNo.Text = P2DLN;
        }//end if principal count not 0
    }//end function populate principal 2 info
    
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
            UpdateGeneralInfo.SetGWPageCount(); //update gateway page count since information recorded is either same or more than those required for Gateway Account

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

                //Response.Redirect("bankinginfo.aspx", false);
        }
        catch (Exception err)
        {
            CreateOnlineAppLog Log = new CreateOnlineAppLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + Session["AppId"].ToString() + " - " + "Merchant principal.aspx Error - " + err.Message);
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