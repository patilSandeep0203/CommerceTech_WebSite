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
using DLPartner;
using BusinessLayer;
using System.Security.Cryptography;
using System.Net.Mail;

public partial class index : System.Web.UI.Page
{
    private static string ConnStringToken = ConfigurationManager.AppSettings["eSecurityConnectString"].ToString();
    private static string strReplace = "";    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if ((Request.Params.Get("Authorization") != null))
            {
                if (Request.Params.Get("Authorization") == "False")
                {
                    FormsAuthentication.SignOut();
                    SetErrorMessage("You are not authorized to view the page. Please log in again.");
                }
            }
            else if ((Request.Params.Get("Logout") != null))
            {
                Style logoutLabel = new Style();
                logoutLabel.ForeColor = System.Drawing.Color.SteelBlue;
                logoutLabel.Font.Size = FontUnit.Medium;
                lblLogoutTrue.ApplyStyle(logoutLabel);
                lblLogoutTrue.Visible = true;
            }
            if (Request.Params.Get("NewSignup") != null)
            {
                if (Request.Params.Get("NewSignup") == "True")
                {
                    lblNewSignup.Visible = true;
                    lblNewSignup.Text = "Thank you for Joining the Commerce Technologies - " + Request.Params.Get("SignupType") + " Program. You can login with the User Name and Password you entered when you signed up.";
                }
            }
            txtLogin.Focus();

            Style errLabel = new Style();
            errLabel.BackColor = System.Drawing.Color.FromArgb(255, 192, 192);
            errLabel.ForeColor = System.Drawing.Color.Black;
            errLabel.BorderColor = System.Drawing.Color.Red;
            errLabel.BorderStyle = BorderStyle.Solid;
            errLabel.BorderWidth = Unit.Pixel(1);
            errLabel.Font.Size = FontUnit.Small;
            lblError.ApplyStyle(errLabel);

            errLabel = new Style();
            errLabel.ForeColor = System.Drawing.Color.OrangeRed;
            errLabel.Font.Size = FontUnit.Point(12);
            lblWelcome.ApplyStyle(errLabel);
            lblNewSignup.ApplyStyle(errLabel);
        }//end if not post back
    }

    //This function handles the submit button click event
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        ValidateSummary.Visible = true;
        try
        {
            if (Page.IsValid)
            {
                lblError.Visible = false;                
                //Validate data
                bool retValidate = ValidateData();
                if (retValidate)
                {
                    int iValidate = ValidateUser(txtLogin.Text.Trim().Replace("'", strReplace), txtPassword.Text.Trim().Replace("'", strReplace));
                    if (iValidate == 1)
                    {
                        CreateCookie();
                        //FormsAuthentication.Initialize();
                        //FormsAuthentication.SetAuthCookie(txtLogin.Text.Trim(), false);

                        UsersBL User = new UsersBL();
                        int AffiliateID = User.GetAffiliateIDbyLoginID(Session["Login"].ToString());

                        SetErrorMessage("User Validated");
                        // Create a Token for this user/app
                        string token = String.Empty;
                        token = User.CreateLoginToken(
                          "Partner Home",
                          Session["Login"].ToString(),
                          Convert.ToInt16(AffiliateID),
                          1);
                        //Response.Redirect("AppList.aspx", false);
                        Response.Redirect("login.aspx" + "?Token=" + token, false);
                    }//end if valid user
                    else
                    {
                        if ( iValidate == 2 )
                            SetErrorMessage("Login Name and/or Password incorrect1. Please try again");
                        txtPassword.Focus();
                    }
                }//end if validatedata
            }//end if page valid
        }//end try
        catch (Exception err)
        {
            CreateLog Log = new CreateLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "Login Error - " + err.Message);
            SetErrorMessage("Error Processing Request. Please contact technical support");
        }
    }//end function submit button click

    protected void lnkBtnForgotPassword_Click(object sender, EventArgs e)
    {
        ValidateSummary.Visible = false;
        lblError.Visible = false;
        int AffiliateID = 0;

        if (txtLogin.Text.Trim() != "")
        {
            UsersBL LoginInfo = new UsersBL();
            DataSet dsUser = LoginInfo.GetLoginInfoByLoginID(txtLogin.Text.Trim().Replace("'", strReplace));
            if (dsUser.Tables[0].Rows.Count > 0)
            {
                DataRow drUser = dsUser.Tables[0].Rows[0];

                AffiliateID = Convert.ToInt16(drUser["iUserID"].ToString());

                AffiliatesBL Aff = new AffiliatesBL(AffiliateID);
                PartnerDS.AffiliatesDataTable affDT = Aff.GetAffiliate();
                if (affDT.Rows.Count > 0)
                {
                    //if (affDT[0].Phrase.ToString().Trim() != "")
                    {
                        pnlPassphrase.Visible = true;
                        //lblPasswordHint.Text = affDT[0].Phrase.ToString().Trim();
                    }
                    //else
                    //{
                    //pnlPassphrase.Visible = false;
                    //SetErrorMessage("Your partner account does not have a password hint set up.<br>Please call Partner Support at 800.477.5363 to retrieve your password.");
                    //}
                }
            }
            else
            {
                SetErrorMessage("Could not find this login id. Please enter you email address.");
                pnlPassphrase.Visible = false;
                pnlResetPasswordEmail.Visible = true;
            }
        }
        else
            SetErrorMessage("Please enter Login to retrieve Password.");        
    }

    protected void btnSendPasswordtoEmail_Click(object sender, EventArgs e)
    {
        ValidateSummary.Visible = false;
        lblError.Visible = false;
        int AffiliateID = 0;

        if (txtResetPasswordEmail.Text.Trim() != "")
        {


            FirstAffiliatesLeadsDL Aff = new FirstAffiliatesLeadsDL();

                //AffiliateID = Convert.ToInt16(drUser["iUserID"].ToString());

                //AffiliatesBL Aff = new AffiliatesBL(AffiliateID);
            DataSet affDS = Aff.GetAffiliatesByEmail(txtResetPasswordEmail.Text.Trim());
            if (affDS.Tables[0].Rows.Count > 0)
            {
                DataRow drAff = affDS.Tables[0].Rows[0];
                if (drAff["Email"].ToString().Trim() != "")
                {
                    //pnlPassphrase.Visible = true;
                    SendEmail(drAff["Email"].ToString().Trim());
                    //lblPasswordHint.Text = affDT[0].Phrase.ToString().Trim();
                    SetErrorMessage("Your login information has been sent to your email address.");
                }
                else
                {
                    pnlPassphrase.Visible = false;
                    SetErrorMessage("Your partner account does not have a email set up.<br>Please call Partner Support at 800.477.5363 to retrieve your password.");
                }
            }
            else
            {
                pnlPassphrase.Visible = false;
                SetErrorMessage("This email address is not in the system.<br>Please call Partner Support at 800.477.5363 to retrieve your password.");
            }


        }
        else
            SetErrorMessage("Please enter Login to retrieve Password.");
    }

    protected void btnRestPasswordYes_Click(object sender, EventArgs e)
    {
        ValidateSummary.Visible = false;
        lblError.Visible = false;
        int AffiliateID = 0;

        if (txtLogin.Text.Trim() != "")
        {
            UsersBL LoginInfo = new UsersBL();
            DataSet dsUser = LoginInfo.GetLoginInfoByLoginID(txtLogin.Text.Trim().Replace("'", strReplace));
            if (dsUser.Tables[0].Rows.Count > 0)
            {
                DataRow drUser = dsUser.Tables[0].Rows[0];

                AffiliateID = Convert.ToInt16(drUser["iUserID"].ToString());

                AffiliatesBL Aff = new AffiliatesBL(AffiliateID);
                PartnerDS.AffiliatesDataTable affDT = Aff.GetAffiliate();
                if (affDT.Rows.Count > 0)
                {
                    if (affDT[0].Email.ToString().Trim() != "")
                    {
                        pnlPassphrase.Visible = true;
                        SendEmail(affDT[0].Email.ToString());
                        //lblPasswordHint.Text = affDT[0].Phrase.ToString().Trim();
                        SetErrorMessage("Your login information has been sent to your email address.");
                    }
                    else
                    {
                        pnlPassphrase.Visible = false;
                        SetErrorMessage("Your partner account does not have a email set up.<br>Please call Partner Support at 800.477.5363 to retrieve your password.");
                    }
                }
            }
            else
            {
                SetErrorMessage("Could not find this login id. Please enter you email address.");
                pnlPassphrase.Visible = false;
                pnlResetPasswordEmail.Visible = true;
            };
        }
        else
            SetErrorMessage("Please enter Login to retrieve Password.");
    }

    protected void btnRestPasswordNo_Click(object sender, EventArgs e)
    {
        pnlPassphrase.Visible = false;
        pnlResetPasswordEmail.Visible = false;
    }

    //This function create new cookie
    protected void CreateCookie()
    {
        FormsAuthenticationTicket AuthTicket;
        string strcookie;
        HttpCookie ck;
        AuthTicket = new FormsAuthenticationTicket(1, txtLogin.Text.Trim(), DateTime.Now, DateTime.Now.AddMinutes(60), false, "Session in progress");
        strcookie = FormsAuthentication.Encrypt(AuthTicket);
        //ck = new HttpCookie(FormsAuthentication.FormsCookieName, strcookie);
        ck = new HttpCookie("firstaffiliates", strcookie);
        ck.Expires = AuthTicket.Expiration;
        //ck.Domain = ".apps.commercetech.com";
        ck.Path = FormsAuthentication.FormsCookiePath;
        Response.Cookies.Add(ck);
    }//end function create cookie
    
    //This function validates data in text fields
    protected bool ValidateData()
    {
        TextBox txtBox = new TextBox();
        for (int i = 0; i < pnlMainPage.Controls.Count; i++)
        {
            if (pnlMainPage.Controls[i].GetType() == txtBox.GetType())
            {
                txtBox = (TextBox)pnlMainPage.Controls[i];
                if ((txtBox.Text.Contains("--")) || (txtBox.Text.Contains("#")) || (txtBox.Text.Contains(";")) || (txtBox.Text.Contains("'")))
                {
                    SetErrorMessage("You cannot use hyphens, apostrophe, # or semi-colons in any of the following fields.");
                    return false;
                }
            }
        }
        return true;
    }//end function validate data

    //This function validates user with database
    protected int ValidateUser(string strUserName, string strPassword)
    {
        UsersBL User = new UsersBL();
        bool bLogin = User.CheckLogin(strUserName, strPassword);        
        if (bLogin)
        {
            Session["Login"] = strUserName;            
            return 1;
        }
        else
            return 2;
    }//end function validate user    

    //This function displays error message on a label
    protected void SetErrorMessage(string errText)
    {
        lblError.Visible = true;
        lblError.Text = errText;
    }//end function set error message

    //This function emails Admin
    public void SendEmail(string strEmail)
    {
        try
        {
            //Send Email to agent after successful registration before redirecting user
            string strSubject = "Commerce Technologies - Partner Information Updated";
            MailMessage msg = new MailMessage();
            msg.Subject = strSubject;
            //msg.To = "information@commercetech.com; accounting@commercetech.com";
            //msg.From = "information@commercetech.com";
            //msg.To.Add(new MailAddress("information@commercetech.com"));
            //msg.To.Add(new MailAddress("accounting@commercetech.com"));
            msg.To.Add(new MailAddress(strEmail));
            msg.From = new MailAddress("support@commercetech.com");
            msg.Body = GetBody();
            //SmtpMail.Send(msg);
            SmtpClient mSmtpClient = new SmtpClient();
            mSmtpClient.Send(msg);
        }
        catch (Exception err)
        {
            CreateLog Log = new CreateLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), err.Message);
           // DisplayMessage("Error Processing Request. Please contact technical support");
        }
    }//end function send email

    //This function creates email body
    public string GetBody()
    {
        //string strBody = "Information for "  + " (PartnerID: " + Session["AffiliateID"].ToString() + ") has been updated. The updated information is as follows: " + System.Environment.NewLine;
        
        //strBody = strBody + System.Environment.NewLine;

        string strBody = "You login with Commerce Technologies: ";

        int AffiliateID;

        string pswd;

        string login;


        strBody = strBody + System.Environment.NewLine;


        FirstAffiliatesLeadsDL Aff = new FirstAffiliatesLeadsDL();

                //AffiliateID = Convert.ToInt16(drUser["iUserID"].ToString());

                //AffiliatesBL Aff = new AffiliatesBL(AffiliateID);
            DataSet affDS = Aff.GetAffiliatesByEmail(txtResetPasswordEmail.Text.Trim());
        
            if (affDS.Tables[0].Rows.Count > 0)
            {
                DataRow drAff = affDS.Tables[0].Rows[0];
                AffiliateID = Convert.ToInt32(drAff["AffiliateID"]);

                UsersDL usr = new UsersDL();

                DataSet usrDS = usr.GetLoginPassword(AffiliateID);

                if (usrDS.Tables[0].Rows.Count > 0)
                { 
                    DataRow drusr = usrDS.Tables[0].Rows[0];
                    pswd = Convert.ToString(drusr["password"]);
                    login = Convert.ToString(drusr["sLoginId"]);

                    strBody = strBody + "User Name: " + login;

                    strBody = strBody + System.Environment.NewLine;

                    strBody = strBody + "Password: " + pswd;

                }
            }

        //Check which fields have been modified

            

        strBody = strBody + System.Environment.NewLine;
        strBody = strBody + System.Environment.NewLine;
        strBody = strBody + "Thank You, " + System.Environment.NewLine;
        strBody = strBody + "Commerce Technologies" + System.Environment.NewLine;

        return strBody;
    }//end function GetBody

}