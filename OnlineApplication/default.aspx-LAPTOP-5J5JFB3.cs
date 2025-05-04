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
using DataLayer;

public partial class start : System.Web.UI.Page
{
    private static string strReplace = "";
    private static int AppId = 0;
    private static string strRedirectFlag = "Merchant";    
    private static int PID = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
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
            if (Request.Params.Get("SessionExpired") != null)
            {
                if (Request.Params.Get("SessionExpired").ToString() == "true")
                    SetErrorMessage("Your session has expired. Please log in again.");
            }
            txtLoginName.Focus();
            if ((Request.Params.Get("AppId") != null))
            {
                if (Int32.TryParse(Request.Params.Get("AppId").ToString(), out AppId))
                    //ReferralID = Convert.ToInt32(Request.Params.Get("Referral"));
                    Session["AppId"] = AppId;
                else
                    Response.Redirect("DefaultError.aspx");

                //Session["AppId"] = Convert.ToInt32(Request.Params.Get("AppId"));
                //AppId = Convert.ToInt32(Request.Params.Get("AppId"));
                PopulateLoginName();
            }

            if ((Request.Params.Get("Country") != null ))            
                Session["Country"] = Convert.ToString(Request.Params.Get("Country"));
            
            //Set session variables
            if (Request.Params.Get("Referral") != null)
                Session["Referral"] = Request.Params.Get("Referral");            
            if (Request.Params["PID"] != null)
                Session["PID"] = Request.Params.Get("PID");
            else
            {
                //If there is no PID in the URL, check if there is a ReferralID and query the Affliates
                //table to get the PID for the specified ReferralID
                if (Request.Params.Get("Referral") != null)
                {
                    PackageInfo Package = new PackageInfo();
                    PID = Package.ReturnPID(Convert.ToInt32(Request.Params.Get("Referral")));
                    Session["PID"] = PID;                    
                }
            }
            if (Request.Params.Get("RepNum") != null)
                Session["RepNum"] = Request.Params.Get("RepNum");
        }//end if not post bacl
    }//end function page load

    //This function handles the submit button click event
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                //Validate data
                bool retValidate = ValidateData();
                //ValidateEmailReqd.Validate();
                //REValidatorEmail.Validate();
                //ValidatePassword.Validate();
                if (retValidate)
                {

                    int iValidate = ValidateUser(txtLoginName.Text.Trim().Replace("'", strReplace), txtPassword.Text.Trim().Replace("'", strReplace));
                    if (iValidate == 1)
                        SetErrorMessage("Your account has been locked because of too many invalid login attempts. Please contact Commerce Technologies at 1-800-477-5363 for further information.");
                    else if (iValidate == 2)
                        SetErrorMessage("Login Name and/or Password incorrect. Please try again.");
                    else if (iValidate == 3)
                    {
                        //Session["MerchantLogin"] = "1";
                        //Create cookie with user info and set timeout for 30 minutes
                        //CreateCookie();
                        FormsAuthentication.SetAuthCookie(txtLoginName.Text.Trim(), false);

                        string strRedirectURL = Request["ReturnURL"];
                        if (strRedirectURL == null)
                        {
                            NewAppInfo AppInfo = new NewAppInfo(AppId);
                            string Status = AppInfo.ReturnStatus();
                            string StatusGW = AppInfo.ReturnStatusGW();

                            CompanyInfo Company = new CompanyInfo(AppId);
                            int companyCom = Company.CheckCompanyComplete();

                            BusinessInfo Business = new BusinessInfo(AppId);
                            int busCom = Business.CheckBusinessComplete("Merchant");
                            int busComGW = Business.CheckBusinessComplete("Gateway");

                            PrincipalInfo Principal1 = new PrincipalInfo(AppId);
                            int principalCom = Principal1.CheckPrincipalComplete("Merchant");
                            int principalComGW = Principal1.CheckPrincipalComplete("Gateway");

                            CardPCT Card = new CardPCT(AppId);
                            int cardPCTCom = Card.CheckCardPCTComplete();


                            OtherInfo other = new OtherInfo(AppId);
                            int otherCom = other.CheckOtherInfoComplete("Merchant");
                            int uploadCom = other.CheckUploadComplete();

                            BankingInfo Banking = new BankingInfo(AppId);
                            int bankCom = Banking.CheckBankingComplete("Merchant");
                            int bankComGW = Banking.CheckBankingComplete("Gateway");

                            ReprogramInfo Reprogram = new ReprogramInfo(AppId);
                            int ReprogramComGW = Reprogram.CheckReprogramComplete();

                            if (Session["Locked"].ToString().Trim() == "Yes")
                            {
                                if (Status.ToLower().Contains("review"))
                                {
                                    if (uploadCom == 1)
                                        Response.Redirect("/Application/notice.aspx", false);
                                    else
                                    {
                                        Response.Redirect("/Application/UploadDocuSignDocs.aspx", false);
                                    }
                                }
                                else
                                {
                                    Response.Redirect("/Application/locked.aspx", false);
                                }
                            }
                            //else if (strRedirectFlag == "cardPCT")
                            //    Response.Redirect("/Application/Merchant/cardpct.aspx", false);
                            else if ((strRedirectFlag == "Merchant") && (Status.ToLower().Contains("completed")))
                            {
                                if (uploadCom == 1)
                                    Response.Redirect("/Application/notice.aspx", false);
                                else {
                                    Response.Redirect("/Application/UploadDocuSignDocs.aspx", false);
                                }
                            }
                            else if ((strRedirectFlag == "Merchant") && (!(Status.ToLower().Contains("completed"))))
                            {
                                if (cardPCTCom == 0)
                                {
                                    Response.Redirect("/Application/Merchant/cardpct.aspx", false);
                                }
                                else if (companyCom == 0)
                                {
                                    Response.Redirect("/Application/Merchant/companyinfo.aspx?EditCompany=True", false);
                                }
                                else if (busCom == 0)
                                {
                                    Response.Redirect("/Application/Merchant/businessinfo.aspx", false);
                                }
                                else if (principalCom == 0)
                                {
                                    Response.Redirect("/Application/Merchant/principal.aspx", false);
                                }
                                else if (otherCom == 0)
                                {
                                    Response.Redirect("/Application/Merchant/bankinginfo.aspx", false);
                                }
                                else if (uploadCom == 0)
                                {
                                    HttpContext.Current.Response.Redirect("/Application/UploadDocuSignDocs.aspx", false);
                                }
                                else
                                {
                                    Response.Redirect("/Application/notice.aspx", false);
                                }
                            }
                            else if ((strRedirectFlag == "Gateway") && (StatusGW.ToLower().Contains("completed")))
                            {
                                Response.Redirect("/Application/notice.aspx", false);
                            }
                            else if ((strRedirectFlag == "Gateway") && (StatusGW.ToLower().Contains("incomplete")))
                            {
                                Response.Redirect("/Application/Gateway/company.aspx", false);
                            }
                            else if ((strRedirectFlag == "Merchant") && (!(StatusGW.ToLower().Contains("completed"))))
                            {
                                if (companyCom == 0)
                                {
                                    Response.Redirect("/Application/Gateway/companyinfo.aspx", false);
                                }
                                else if (busComGW == 0)
                                {
                                    Response.Redirect("/Application/Gateway/businessinfo.aspx?EditBusiness=True", false);
                                }
                                else if (principalComGW == 0)
                                {
                                    Response.Redirect("/Application/Gateway/principalinfo.aspx?EditPrincipal=True", false);
                                }
                                else if (bankComGW == 0)
                                {
                                    Response.Redirect("/Application/Gateway/banking.aspx", false);
                                }
                                else if (ReprogramComGW == 0)
                                {
                                    Response.Redirect("/Application/Gateway/reprogram.aspx?EditPlatform=True", false);
                                }
                            }
                            else if (strRedirectFlag == "Equipment")
                                Response.Redirect("/Application/Merchant/cardpct.aspx", false);
                            else if (strRedirectFlag == "QuickBooks")
                            {
                                if (Status.ToLower().Contains("completed"))
                                {
                                    // Response.Redirect("/Application/Merchant/bankinginfo.aspx", false);
                                    AppInfo.checkPageRedirect();
                                }
                                else
                                {
                                    if (companyCom == 0)
                                    {
                                        Response.Redirect("/Application/Gateway/companyinfo.aspx", false);
                                    }
                                    else if (busComGW == 0)
                                    {
                                        Response.Redirect("/Application/Gateway/businessinfo.aspx?EditBusiness=True", false);
                                    }
                                    else if (principalComGW == 0)
                                    {
                                        Response.Redirect("/Application/Gateway/principalinfo.aspx?EditPrincipal=True", false);
                                    }
                                    else if (bankComGW == 0)
                                    {
                                        Response.Redirect("/Application/Gateway/banking.aspx", false);
                                    }
                                    else if (ReprogramComGW == 0)
                                    {
                                        Response.Redirect("/Application/Gateway/reprogram.aspx?EditPlatform=True", false);
                                    }
                                }
                            }
                            //Response.Redirect("http://merchantaccount.quickbooks.com/j/mas/signup?p_prioritycode=QB5536", false);
                            //Response.Redirect("http://www.apps.commercetech.com/quickbooks", false);
                            //Response.Redirect("AddlServices.aspx?QB=Yes", false);
                            else if (strRedirectFlag == "WorldPay")
                                //Response.Redirect("http://merchantaccount.quickbooks.com/j/mas/signup?p_prioritycode=QB5536", false);
                                //Response.Redirect("https://secure.worldpay.com/app/application.pl?brand=ctc&Scheme=1", false);
                                Response.Redirect("https://secure.worldpay.com/app/application.pl?brand=ctc&amp;scheme=2", false);
                            else
                                Response.Redirect("default.aspx", false);
                        }
                        else
                        {
                            Response.Redirect(strRedirectURL);
                        }
                    }//end if user validated
                    else
                    {
                        SetErrorMessage("Error Processing Request. Please contact Technical Support: <a href=mailto:support@commercetech.com?Subject=Online App Error, App ID: " + Session["AppId"].ToString() + "> support@commercetech.com. </a>");
                    }
                }
                else {
                    SetErrorMessage("Error Processing Request. Please contact Technical Support: <a href=mailto:support@commercetech.com?Subject=Online App Error, App ID: " + Session["AppId"].ToString() + "> support@commercetech.com. </a>");
                }//end if validate data
            }//end if page valid
        }//end try
        catch (Exception err)
        {
            CreateOnlineAppLog Log = new CreateOnlineAppLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "default.aspx - " + err.Message);
            SetErrorMessage("Error Processing Request. Please contact Technical Support: <a href=mailto:support@commercetech.com?Subject=Online App Error, App ID: " + Convert.ToString(Session["AppId"]) + "> support@commercetech.com. </a>");
        }        
    }//end function submit button click

    //This function create new cookie
    protected void CreateCookie()
    {
        FormsAuthenticationTicket AuthTicket;
        string strcookie;
        HttpCookie ck;
        AuthTicket = new FormsAuthenticationTicket(1, txtLoginName.Text.Trim(), DateTime.Now, DateTime.Now.AddMinutes(60), false, "Session in progress");
        strcookie = FormsAuthentication.Encrypt(AuthTicket);
        //ck = new HttpCookie(FormsAuthentication.FormsCookieName, strcookie);
        ck = new HttpCookie("firstaffiliates", strcookie);
        ck.Expires = AuthTicket.Expiration;
        ck.Domain = ".apps.commercetech.com";        
        ck.Path = FormsAuthentication.FormsCookiePath;
        Response.Cookies.Add(ck);
    }//end function create cookie

    //This function populates Login Name based on APPID in the database
    protected void PopulateLoginName()
    {
        OnlineAppProfile Profile = new OnlineAppProfile(AppId);
        string LoginName = Profile.ReturnLoginName();
        if (LoginName != "")
        {
            txtLoginName.Text = LoginName;
            txtPassword.Focus();
        }
    }//end function populate Login Name
    
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
                    txtBox.BackColor = System.Drawing.Color.Ivory;
                    txtBox.Focus();
                    SetErrorMessage("You cannot use apostrophes, #, or semi-colons in any of the following fields.");
                    return false;
                }
            }
        }
        return true;
    }//end function validate data

    //This function validates user with database
    protected int ValidateUser(string strUserName, string strPassword)
    {
        //First get the salt for the specified Login Name
        OnlineAppAccess GetSalt = new OnlineAppAccess();
        string Salt = GetSalt.ReturnSalt(strUserName);
        string hash = "";
        if ( Salt != "" )
            hash = FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text.Trim() + Salt, "sha1");
        else
            hash = strPassword;
        
        //Check login
        string IPAddress = Request.ServerVariables["REMOTE_ADDR"].ToString();
        OnlineAppAccess CheckLoginInfo = new OnlineAppAccess();
        AppId = CheckLoginInfo.AuthticateUser(strUserName, hash, IPAddress);

        OtherInfoTable NewOnlineAppOther = new OtherInfoTable();
        NewOnlineAppOther.UpdateDocusignAccess(AppId, "Admin"); 

        if (AppId == -1)
            return 1;
        else if (AppId == 0)
            return 2;
        else
        {
            Session["AppId"] = AppId;
            Session["UserLoggedIn"] = "Yes";
            //Get AcctType for the AppId
            OnlineAppProfile Profile = new OnlineAppProfile(AppId);
            int AcctType = 0;
         
            DataSet ds = Profile.GetProfileData();            
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                AcctType = Convert.ToInt32(dr["AcctType"]);
            }
            
            if (AcctType == 0)
                return 4;
            Session["AcctType"] = AcctType;
            Session["AffiliateID"] = 0;            

            //Check whether the application is locked before redirecting
            OnlineAppStatus Locked = new OnlineAppStatus(AppId);
            string strLocked = Locked.GetLocked();
            Session["Locked"] = strLocked;

            //Get current PID for user and set session variable
            string Redirect = string.Empty;
            NewAppInfo OnlineApp = new NewAppInfo(AppId);
            DataSet dsPID = OnlineApp.GetNewAppData();
      
            if (dsPID.Tables[0].Rows.Count > 0)
            {
                DataRow drPID = dsPID.Tables[0].Rows[0];
                Session["PID"] = drPID["PID"].ToString().Trim();
                Session["Referral"] = drPID["ReferralID"].ToString().Trim();
                Redirect = drPID["Redirect"].ToString();
            }
            
            //redirect based on account type
            if ((AcctType == 1) || ( AcctType == 4))
            {
                strRedirectFlag = "Merchant";
                //CardPCT CardPCTInfo = new CardPCT(AppId);
                //DataSet dsCardPCTInfo = CardPCTInfo..GetCardPCT();
                //if (dsCardPCTInfo.Tables[0].Rows.Count == 0)
                //if(CardPCTInfo.CheckCardPCTComplete() == 0)
                //    strRedirectFlag = "cardPCT";
                //else
                //    strRedirectFlag = "Merchant";
                //close readers                    

            }
            else if (AcctType == 2)
                strRedirectFlag = "Gateway";
            else if (AcctType == 3)
                strRedirectFlag = "Equipment";

            if (Redirect == "QuickBooks")
                strRedirectFlag = "QuickBooks";
            else if (Redirect == "WorldPay")
                strRedirectFlag = "WorldPay";
            return 3;
        }//end if AppId is valid     
    }//end function validate user

    //This function displays error message on a label
    protected void SetErrorMessage(string errText)
    {
        lblError.Visible = true;
        lblError.Text = errText;
    }//end function set error message
}
