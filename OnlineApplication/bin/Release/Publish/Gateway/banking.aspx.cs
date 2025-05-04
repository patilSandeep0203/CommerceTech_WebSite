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

public partial class Gateway_banking : System.Web.UI.Page
{
    private static int AppId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session.IsNewSession)
            Response.Redirect("/Application/default.aspx?SessionExpired=True");

        Style errLabel = new Style();
        errLabel.BackColor = System.Drawing.Color.FromArgb(255, 192, 192);
        errLabel.BorderColor = System.Drawing.Color.Red;
        errLabel.ForeColor = System.Drawing.Color.Black;
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
                Response.Redirect("/Application/default.aspx?Authentication=False", false);
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
                        btnEditNBC.Enabled = false;
                        btnSubmit.Enabled = false;
                    }
                }
                catch (Exception err)
                {
                    CreateOnlineAppLog Log = new CreateOnlineAppLog();
                    Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + Session["AppId"].ToString() + " - " + "Gateway banking.aspx Page Load Error - " + err.Message);
                     SetErrorMessage("Error Processing Request. Please contact Technical Support: <a href='mailto:support@commercetech.com?Subject=Online Application Error'> support@commercetech.com. </a>.");
                }
            }
        }//end if
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
        //Get banking info from onlineappbanking
        BankingInfo Banking = new BankingInfo(AppId);
        DataSet dsBankingInfo = Banking.GetBankingInfo();
        if (dsBankingInfo.Tables[0].Rows.Count > 0)
        {
            DataRow drBankingInfo = dsBankingInfo.Tables[0].Rows[0];

            lblBankName.Text = Server.HtmlEncode(drBankingInfo["BankName"].ToString().Trim());
            lblOtherBank.Text = Server.HtmlEncode(drBankingInfo["OtherBank"].ToString().Trim());
            lblBankAddress.Text = Server.HtmlEncode(drBankingInfo["BankAddress"].ToString().Trim());
            //lblBankAddress2.Text = drBankingInfo["Address2"].ToString().Trim();
            lblZipCode.Text = Server.HtmlEncode(drBankingInfo["BankZip"].ToString().Trim());
            lblPhone.Text = Server.HtmlEncode(drBankingInfo["BankPhone"].ToString().Trim());
            lblRegion.Text = Server.HtmlEncode(drBankingInfo["BankRegion"].ToString().Trim());
            lblState.Text = Server.HtmlEncode(drBankingInfo["BankState"].ToString().Trim());
            lblCity.Text = Server.HtmlEncode(drBankingInfo["BankCity"].ToString().Trim());
            lblNameOnChecking.Text = Server.HtmlEncode(drBankingInfo["NameonCheckingAcct"].ToString().Trim());

            //string hash = FormsAuthentication.HashPasswordForStoringInConfigFile(lblPassword.Text.Trim() + GetSalt(), "sha1");
            //This code displays only the last 4 digits of the account number
            string AcctNumber = Server.HtmlEncode(drBankingInfo["BankAccountNumber"].ToString().Trim());
            string ANum = "";
            int j = 0;
            if (AcctNumber.Length >= 4)
            {
                for (int i = 0; i < AcctNumber.Length - 4; i++)
                {
                    ANum += "x";
                    j++;
                }
            }
            AcctNumber = AcctNumber.Substring(j);
            ANum += AcctNumber;
            lblAcctNumber.Text = ANum;//drBankingInfo["BankAccountNumber"].ToString().Trim();
            //This code displays only the last 4 digits of the routing number
            string RoutingNumber = Server.HtmlEncode(drBankingInfo["BankRoutingNumber"].ToString().Trim());
            string RNum = "";
            j = 0;
            for (int i = 0; i < RoutingNumber.Length - 4; i++)
            {
                RNum += "x";
                j++;
            }
            RoutingNumber = RoutingNumber.Substring(j);
            RNum += RoutingNumber;
            lblBankRoutingNumber.Text = RNum;//drBankingInfo["BankRoutingNumber"].ToString().Trim();

        }//end if count not 0


        //get info from otherinfo table
        OtherInfo OtherInfo = new OtherInfo(AppId);
        DataSet dtOtherInfo = OtherInfo.GetOtherInfo();
        if (dtOtherInfo.Tables[0].Rows.Count > 0)
        {
            DataRow dsOtherInfo = dtOtherInfo.Tables[0].Rows[0];

            lblDiscover.Text = Server.HtmlEncode(dsOtherInfo["DiscoverApplied"].ToString().Trim());
            lblDiscoverNum.Text = MaskNumbers(dsOtherInfo["PrevDiscoverNum"].ToString().Trim());
            lblAmex.Text = Server.HtmlEncode(dsOtherInfo["AmexApplied"].ToString().Trim());
            lblAmexNum.Text = MaskNumbers(dsOtherInfo["PrevAmexNum"].ToString().Trim());
            lblJCB.Text = Server.HtmlEncode(dsOtherInfo["JCBApplied"].ToString().Trim());
            lblJCBNum.Text = MaskNumbers(dsOtherInfo["PrevJCBNum"].ToString().Trim());

            if (dsOtherInfo["DiscoverApplied"].ToString().Trim() == "Yes - Existing")
            {
                pnlDiscoverNum.Visible = true;
            }
            else
            {
                pnlDiscoverNum.Visible = false;
            }

            if (dsOtherInfo["AmexApplied"].ToString().Trim() == "Yes - Existing")
            {
                pnlAmexNum.Visible = true;
            }
            else
            {
                pnlAmexNum.Visible = false;
            }

            if (dsOtherInfo["JCBApplied"].ToString().Trim() == "Yes - Existing")
            {
                pnlJCBNum.Visible = true;
            }
            else
            {
                pnlJCBNum.Visible = false;
            }
        }//end if
    }//end fuction populate page
    
    //This function masks numbers
    protected string MaskNumbers(string strValue)
    {
        string Number = Server.HtmlEncode(strValue);
        string Num = "";
        int j = 0;
        if (Number.Length >= 4)
        {
            for (int i = 0; i < Number.Length - 4; i++)
            {
                Num += "x";
                j++;
            }
        }
        Number = Number.Substring(j);
        Num += Number;
        return Num;
    }

    //This function handles the submit button click event
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            CommonFunctions UpdateGeneralInfo = new CommonFunctions(AppId);
            UpdateGeneralInfo.SetGWPageCount();
            Response.Redirect("reprogram.aspx", false);
        }
        catch (Exception err)
        {
            CreateOnlineAppLog Log = new CreateOnlineAppLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + Session["AppId"].ToString() + " - " + "Gateway banking.aspx Error - " + err.Message);
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