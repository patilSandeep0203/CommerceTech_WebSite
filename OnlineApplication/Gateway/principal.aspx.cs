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

public partial class Gateway_principal : System.Web.UI.Page
{
    private static int AppId = 0;
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
                    Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + Session["AppId"].ToString() + " - " + "Gateway principal.aspx Page Load Error - " + err.Message);
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
        PrincipalInfo Principal = new PrincipalInfo(AppId);
        DataSet dsPrincipal1Info = Principal.GetPrincipalInfo();
        if (dsPrincipal1Info.Tables[0].Rows.Count > 0)
        {
            DataRow drPrincipal1Info = dsPrincipal1Info.Tables[0].Rows[0];
            //Populate Principal 1 information
            lblP1FirstName.Text = Server.HtmlEncode(drPrincipal1Info["P1FirstName"].ToString().Trim());
            lblP1LastName.Text = Server.HtmlEncode(drPrincipal1Info["P1LastName"].ToString().Trim());
            lblP1MiddleName.Text = Server.HtmlEncode(drPrincipal1Info["P1MidName"].ToString().Trim());
            lblP1Email.Text = Server.HtmlEncode(drPrincipal1Info["P1Email"].ToString().Trim());
            lblP1Title.Text = Server.HtmlEncode(drPrincipal1Info["P1Title"].ToString().Trim());
            lblP1Address.Text = Server.HtmlEncode(drPrincipal1Info["P1Address"].ToString().Trim());
            lblP1Address2.Text = Server.HtmlEncode(drPrincipal1Info["P1Address2"].ToString().Trim());
            lblP1City.Text = Server.HtmlEncode(drPrincipal1Info["P1City"].ToString().Trim());            
            lblP1State.Text = Server.HtmlEncode(drPrincipal1Info["P1State"].ToString().Trim());
            lblP1ZipCode.Text = Server.HtmlEncode(drPrincipal1Info["P1ZipCode"].ToString().Trim());
            lblP1Region.Text = Server.HtmlEncode(drPrincipal1Info["P1Region"].ToString().Trim());            
            lblP1Country.Text = Server.HtmlEncode(drPrincipal1Info["P1Country"].ToString().Trim());
            lblP1HomePhone.Text = Server.HtmlEncode(drPrincipal1Info["P1PhoneNumber"].ToString().Trim());
            lblP1MobilePhone.Text = Server.HtmlEncode(drPrincipal1Info["P1MobilePhone"].ToString().Trim());
            //lblP1SSN.Text = drPrincipal1Info["P1SSN"].ToString().Trim();
            //This code displays only the last 4 digits of the SSN
            string P1SSN = Server.HtmlEncode(drPrincipal1Info["P1SSN"].ToString().Trim());
            string SSN = "";
            int j = 0;
            if (P1SSN.Length >= 4)
            {
                for (int i = 0; i < P1SSN.Length - 4; i++)
                {
                    SSN += "x";
                    j++;
                }
            }
            P1SSN = P1SSN.Substring(j);
            SSN += P1SSN;
            lblP1SSN.Text = SSN;
        }//end if principal count not 0
    }//end function populate principal 1 info

    //This function handles the submit button click event
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            CommonFunctions UpdateGeneralInfo = new CommonFunctions(AppId);
            UpdateGeneralInfo.SetGWPageCount();
            Response.Redirect("bankinginfo.aspx", false);
        }
        catch (Exception err)
        {
            CreateOnlineAppLog Log = new CreateOnlineAppLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + Session["AppId"].ToString() + " - " + "Gateway principal.aspx Error - " + err.Message);
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
