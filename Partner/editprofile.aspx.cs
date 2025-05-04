using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Security.Cryptography;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using OnlineAppClassLibrary;

public partial class editprofile : System.Web.UI.Page
{
    private static int AppId = 0;
    private static string strReplace = "";
    static string referrer = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        AppId = Convert.ToInt32(Session["AppId"]);
        if (Session.IsNewSession)
            Response.Redirect("/OnlineApplication/default.aspx?SessionExpired=True");

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

        if (!IsPostBack)
        {
            referrer = Request.UrlReferrer.AbsoluteUri.ToString();
            if (!User.Identity.IsAuthenticated)
                Response.Redirect("default.aspx?Authentication=False");
            else
            {
                try
                {

                    Populate();
                    /*
                    if (!Locked())
                        Populate();
                    else
                    {
                        Populate();
                        SetErrorMessage("The status of your application prevents it from being edited");
                        btnContinue.Enabled = false;
                    }*/
                }
                catch (Exception err)
                {
                    CreateOnlineAppLog Log = new CreateOnlineAppLog();
                    Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + Session["AppId"].ToString() + " - " + "Edit Profile Page Load Error - " + err.Message);
                     SetErrorMessage("Error Processing Request. Please contact Technical Support: <a href='mailto:support@commercetech.com?Subject=Online Application Error'> support@commercetech.com. </a>.");
                }
            }
            
        }
    }

    //This function populates fields
    public void Populate()
    {

        //Get registration info from newapp table and populate fields
        OnlineAppProfile NewAppProfile = new OnlineAppProfile(AppId);
        DataSet dsNewApp = NewAppProfile.GetProfileData();
        DataTable dtNewApp = dsNewApp.Tables[0];
        if (dtNewApp.Rows.Count > 0)
        {
            DataRow drNewApp = dtNewApp.Rows[0];
            txtTitle.Text = drNewApp["Title"].ToString().Trim();            
            txtFirstName.Text = drNewApp["FirstName"].ToString().Trim();
            txtLastName.Text = drNewApp["LastName"].ToString().Trim();
            txtPhone.Text = drNewApp["Phone"].ToString().Trim();
            txtHomePhone.Text = drNewApp["HomePhone"].ToString().Trim();
            txtPhoneExt.Text = drNewApp["PhoneExt"].ToString().Trim();
            txtMobilePhone.Text = drNewApp["MobilePhone"].ToString().Trim();
            lblLoginName.Text = Server.HtmlEncode(drNewApp["LoginName"].ToString().Trim());
            txtEmail.Text = drNewApp["SignupEmail"].ToString().Trim();
        }//end if count not 0
    }//end function populate

    //This function generates a hash to encrypt the password
    protected string GenerateHash(string salt)
    {
        string hash = FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text.Trim() + salt, "sha1");
        return hash;
    }

    //This function gets a salt to encrypt the password which can be stored in the database
    protected string GetSalt()
    {
        RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
        byte[] buffer = new byte[5];
        rng.GetBytes(buffer);
        return Convert.ToBase64String(buffer);
    }

    //This function handles submit button click event
    protected void btnContinue_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                //Make sure length of password is atleast 5 characters long
                if (txtPassword.Text.Trim().Length < 5)
                {
                    SetErrorMessage("Please choose a password which is atleast 5 characters long. You cannot use characters like semi-colons, #, hyphens or apostrophes in the password.");
                }
                else
                {
                    //Validate data
                    bool retValidate = ValidateData();
                    if (retValidate)
                    {

                        string salt = GetSalt();
                        string hash = GenerateHash(salt);

                        //Insert profile information in OnlineappProfile
                        OnlineAppProfile Profile = new OnlineAppProfile(AppId);
                        bool retVal = Profile.IUProfile(
                            txtFirstName.Text.Trim().Replace("'", strReplace),
                            txtLastName.Text.Trim().Replace("'", strReplace),
                            txtEmail.Text.Trim().Replace("'", strReplace),
                            txtTitle.Text.Trim().Replace("'", strReplace),
                            txtPhone.Text.Trim(),
                            txtPhoneExt.Text.Trim(),
                            txtHomePhone.Text.Trim(),
                            txtMobilePhone.Text.Trim(),
                            Convert.ToInt32(Session["AcctType"]));

                        LogBL LogData = new LogBL(AppId);
                        LogData.InsertLogData( Convert.ToInt32(Session["AffiliateID"]), "Merchant Profile Updated");

                        //Update password in OnlineAppAccess
                        OnlineAppAccess UpdatePassword = new OnlineAppAccess(AppId);
                        retVal = UpdatePassword.InsertUpdatePassword(lblLoginName.Text.Trim(), txtPassword.Text.Trim().Replace("'", strReplace), hash, salt);
                        if (retVal)
                        {
                            Response.Redirect(referrer, false);
                        }
                    }//end if validated
                }//end if length of password is < 5
            }//end if page valid
        }//end try
        catch (Exception err)
        {
            CreateOnlineAppLog Log = new CreateOnlineAppLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + Session["AppId"].ToString() + " - " + "Edit Profile Error - " + err.Message);
             SetErrorMessage("Error Processing Request. Please contact Technical Support: <a href='mailto:support@commercetech.com?Subject=Online Application Error'> support@commercetech.com. </a>.");
        }
    }//end button click

    //This function validates data in text fields
    protected bool ValidateData()
    {
        TextBox txtBox = new TextBox();
        for (int i = 0; i < pnlMainPage.Controls.Count; i++)
        {
            if (pnlMainPage.Controls[i].GetType() == txtBox.GetType())
            {
                txtBox = (TextBox)pnlMainPage.Controls[i];
                if ( ( txtBox.Text.Contains("--")) || ( txtBox.Text.Contains("#")) || ( txtBox.Text.Contains(";")) || ( txtBox.Text.Contains("'")))
                {
                    txtBox.BackColor = System.Drawing.Color.Ivory;
                    txtBox.Focus();
                    SetErrorMessage("You cannot use apostrophes, #, or semi-colons in any of the following fields.");
                    return false;
                }
            }
        }//end for
        if ((txtPassword.Text.Contains("--")) || (txtPassword.Text.Contains("#")) || (txtPassword.Text.Contains(";")) || (txtPassword.Text.Contains("'")))
        {
            SetErrorMessage("You cannot use hyphens, apostrophe, # or semi-colons in the Password field.");
            return false;
        }
        return true;
    }//end function validate data

    //This function displays error messages
    protected void SetErrorMessage(string errText)
    {
        lblError.Visible = true;
        lblError.Text = errText;
    }//end function set error message

    //This function checks if the application is locked
    protected bool Locked()
    {
        if (Session["Locked"].ToString().Trim() == "Yes")
            return true;
        else
            return false;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(referrer, false);
    }
}
