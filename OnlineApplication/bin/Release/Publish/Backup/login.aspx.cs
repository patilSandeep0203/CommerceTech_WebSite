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
using System.Security.Cryptography;

public partial class login : System.Web.UI.Page
{
    private static int AppId = 0;
    private static string strRedirectFlag = "Merchant";

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

        if ((Request.Params.Get("AppId") != null))
        {
            Session["AppId"] = Convert.ToInt32(Request.Params.Get("AppId"));
            AppId = Convert.ToInt32(Request.Params.Get("AppId"));
        }
        if (!IsPostBack)
        {
            try
            {
                if (Request.QueryString["Token"] != null)
                {                    
                    UserBL User = new UserBL();
                    DataSet ds = User.VerifyOnlineAppToken(Request.QueryString["Token"].ToString());
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow dr = ds.Tables[0].Rows[0];
                        int iValidate = ValidateUser(dr["iUserID"].ToString());
                        if (iValidate == 1)
                        {
                            int returnVal = SetSessionVars();
                            if (returnVal == 0)
                            {
                                FormsAuthentication.SetAuthCookie(dr["sLoginID"].ToString(), false);
                                CheckRedirect();
                            }//end if set session vars
                        }
                        else if (iValidate == 3)
                            SetErrorMessage("Login Name and/or password incorrect");
                    }//end if count not 0
                }//end if appid not 0 and token not null
                else
                    SetErrorMessage("Invalid Request.");
                Session["AppId"] = AppId;
            }//end try
            catch (Exception err)
            {
                CreateOnlineAppLog Log = new CreateOnlineAppLog();
                Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + Session["AppId"].ToString() + " - " + "login.aspx Error - " + err.Message);
                 SetErrorMessage("Error Processing Request. Please contact Technical Support: <a href='mailto:support@commercetech.com?Subject=Online Application Error'> support@commercetech.com. </a>.");                
            }
        }//end if not post back
    }


    public void CheckRedirect()
    {   /*
        NewAppInfo AppInfo = new NewAppInfo(AppId);
        string Status = AppInfo.ReturnStatus();
        string StatusGW = AppInfo.ReturnStatusGW();

        if (Session["Locked"].ToString().Trim() == "Yes")
        {
            if (Status.ToLower().Contains("review"))
            {
                Response.Redirect("/Application/notice.aspx", false);
            }
            else
            {
                Response.Redirect("/Application/locked.aspx", false);
            }
        }
        //else if (strRedirectFlag == "cardPCT")
        //    Response.Redirect("/Application/Merchant/cardpct.aspx", false);
        else if (strRedirectFlag == "Merchant")
            Response.Redirect("/Application/Merchant/cardpct.aspx", false);
        else if (strRedirectFlag == "Gateway")
            Response.Redirect("/Application/Gateway/companyinfo.aspx", false);
        else if (strRedirectFlag == "Equipment")
            Response.Redirect("/Application/Merchant/cardpct.aspx", false);
        else
            Response.Redirect("default.aspx", false);*/
        //CreateCookie();
        //FormsAuthentication.SetAuthCookie(txtLoginName.Text.Trim(), false);

        //NewAppInfo AppInfo = new NewAppInfo(AppId);

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

            BankingInfo Banking = new BankingInfo(AppId);
            int bankComGW = Banking.CheckBankingComplete("Gateway");

            ReprogramInfo Reprogram = new ReprogramInfo(AppId);
            int ReprogramComGW = Reprogram.CheckReprogramComplete();

            if (Session["Locked"].ToString().Trim() == "Yes")
            {
                if (Status.ToLower().Contains("review"))
                {
                    Response.Redirect("/Application/notice.aspx", false);
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
                //if ()
                Response.Redirect("/Application/notice.aspx", false);
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
            else if ((strRedirectFlag == "Gateway") && (StatusGW.ToLower().Contains("activation")))
            {
                Response.Redirect("/Application/notice.aspx", false);
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
                    //Response.Redirect("/Application/Merchant/bankinginfo.aspx", false);
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
    }
        
    //This function validates user with database
    protected int ValidateUser(string iUserID)
    {
        UserBL User = new UserBL();
        DataSet dsAccess = User.CheckOnlineAppLogin(iUserID);             
        if (dsAccess.Tables[0].Rows.Count > 0)
        {
            DataRow drAccess = dsAccess.Tables[0].Rows[0];
            //Session["Access"] = drAccess["Access"].ToString().Trim();
            Session["AffiliateID"] = iUserID;
            return 1;
        }
        else
            return 3;        
    }//end function validate user

    //This function sets session variable and the redirection URL
    public int SetSessionVars()
    {
        //Get accttype from newapp table
        OnlineAppProfile NewAppInfo = new OnlineAppProfile(AppId);
        DataSet dsNewApp = NewAppInfo.GetProfileData();
        DataTable dtNewApp = dsNewApp.Tables[0];
        if (dtNewApp.Rows.Count > 0)
        {
            DataRow drNewApp = dtNewApp.Rows[0];
            Session["UserLoggedIn"] = "Yes";
            Session["AppId"] = Convert.ToInt32(drNewApp["AppId"]);
            Session["AcctType"] = Convert.ToInt32(drNewApp["AcctType"]);
            //Check whether the application is locked before redirecting
            OnlineAppStatus Locked = new OnlineAppStatus(AppId);
            string strLocked = Locked.GetLocked();
            Session["Locked"] = strLocked;

            //redirect based on account type
            if ((Convert.ToInt32(drNewApp["AcctType"]) == 1) || (Convert.ToInt32(drNewApp["AcctType"]) == 4))
            {
                strRedirectFlag = "Merchant";
                /*CardPCT CardPCTInfo = new CardPCT(AppId);
                DataSet dsCardPCTInfo = CardPCTInfo.GetCardPCT();
                DataTable dtCardPCT = dsCardPCTInfo.Tables["OnlineAppCardPCT"];
                if (dtCardPCT.Rows.Count > 0)
                {
                    DataRow dr = dsCardPCTInfo.Tables[0].Rows[0];
                    if ( Convert.ToBoolean(dr["Complete"]) == false )
                        strRedirectFlag = "cardPCT";
                    else
                        strRedirectFlag = "Merchant";
                }
                else if (dtCardPCT.Rows.Count == 0)
                    strRedirectFlag = "cardPCT";
                else
                    strRedirectFlag = "Merchant";
                //close readers  */
            }
            else if (Convert.ToInt32(drNewApp["AcctType"]) == 2)
                strRedirectFlag = "Gateway";
            else if (Convert.ToInt32(drNewApp["AcctType"]) == 3)
                strRedirectFlag = "Equipment";
            return 0;
        }//end if count not 0    
        else
            return 1;
    }

    //This function displays error message on a label
    protected void SetErrorMessage(string errText)
    {
        lblError.Visible = true;
        lblError.Text = errText;
    }//end function set error message
}