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
using DLPartner;
//using BLPartner;
using DataLayer;

public partial class Merchant_banking : System.Web.UI.Page
{
    private static int AppId = 0;
    private static string strSW = string.Empty;
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
        if ((Request.Params.Get("SW11") != null))
        {
            strSW = Request.Params.Get("SW11");
        }
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
                        btnEditNBC.Enabled = false;
                        btnEditProcessing.Enabled = false;
                        btnSubmit.Enabled = false;
                    }
                }
                catch (Exception err)
                {
                    CreateOnlineAppLog Log = new CreateOnlineAppLog();
                    Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + Session["AppId"].ToString() + " - " + "Merchant banking.aspx Page Load Error - " + err.Message);
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
            lblCountry.Text = Server.HtmlEncode(drBankingInfo["BankCountry"].ToString().Trim());                     
            lblNameOnChecking.Text = Server.HtmlEncode(drBankingInfo["NameonCheckingAcct"].ToString().Trim());
          
            //Add x's to the already-truncated numbers
            lblAcctNumber.Text = MaskNumbers(drBankingInfo["BankAccountNumber"].ToString().Trim());                     
            lblBankRoutingNumber.Text = MaskNumbers(drBankingInfo["BankRoutingNumber"].ToString().Trim());
          
        }//end if count not 0


        NewAppTable newAppTable = new NewAppTable();
        DataSet dsDBEBT = new DataSet();
        dsDBEBT = newAppTable.GetDebitEBTInfo(AppId);
        string USDANum = "";



                if ((Session["AcctType"].ToString() == "1") || (Session["AcctType"].ToString() == "4"))
            {
                ProcessingInfo CPCNP = new ProcessingInfo(AppId);
                string CardPresent = CPCNP.ReturnCardPresent();
                string Processor = CPCNP.ReturnProcessorName();
                if (CardPresent == "CP")
                {
                    lblOnlineDebit.Enabled = true;
                     lblEBT.Enabled = true;
                     pnlOnlineDebit.Visible = true;
                    pnlEBT.Visible = true;
                }
                else
                {

                    pnlOnlineDebit.Visible = false;
                    pnlEBT.Visible = false;
                    lblOnlineDebit.Visible= false;
                     lblEBT.Visible = false;
                     imgOD.Visible = false;
                     imgEBT.Visible = false;
                }

            }
        

        if (dsDBEBT.Tables["OnlineAppNewApp"].Rows.Count > 0)
        {
            DataRow drDBEBT = dsDBEBT.Tables["OnlineAppNewApp"].Rows[0];
            if (Convert.ToBoolean(drDBEBT["OnlineDebit"]) == true)
            {
                lblOnlineDebit.Text = "Yes";
            }
            else if (Convert.ToBoolean(drDBEBT["OnlineDebit"]) == false)
            {
                lblOnlineDebit.Text = "No";
            }

            if (Convert.ToBoolean(drDBEBT["EBT"]) == true)
            {
                lblEBT.Text = "Yes";
            }
            else if (Convert.ToBoolean(drDBEBT["EBT"]) == false)
            {
                lblEBT.Text = "No";
            }

            lblEBT.Text = Convert.ToString(drDBEBT["StatusEBT"]);
            USDANum = Convert.ToString(drDBEBT["USDANum"]);
            lblUSDANum.Text = USDANum;

            if (Convert.ToBoolean(drDBEBT["EBT"]) == true)
            {
                if (!Convert.IsDBNull(drDBEBT["USDANum"]))
                {
                    if (Convert.ToString(drDBEBT["USDANum"]) != "")
                    {
                        pnlEBTNum.Visible = true;
                        lblUSDANum.Visible = true;
                    }
                    else {
                        pnlEBTNum.Visible = false;
                        lblUSDANum.Visible = false;
                    }
                }
                else {
                    pnlEBTNum.Visible = false;
                    lblUSDANum.Visible = false;
                }

            }else 
            {
                pnlEBTNum.Visible = false;
                lblUSDANum.Visible = false;
            }

             
        }

        //get info from otherinfo table
        OtherInfo OtherInfo = new OtherInfo(AppId);
        DataSet dtOtherInfo = OtherInfo.GetOtherInfo();
        if (dtOtherInfo.Tables[0].Rows.Count > 0)
        {
            DataRow dsOtherInfo = dtOtherInfo.Tables[0].Rows[0];

            lblAvgTicket.Text = Server.HtmlEncode(dsOtherInfo["AverageTicket"].ToString().Trim());
            lblMaxTicket.Text = Server.HtmlEncode(dsOtherInfo["MaxTicket"].ToString().Trim());
            lblMonthlyVol.Text = Server.HtmlEncode(dsOtherInfo["MonthlyVolume"].ToString().Trim());
            lblDiscover.Text = Server.HtmlEncode(dsOtherInfo["DiscoverApplied"].ToString().Trim());
            lblDiscoverNum.Text = MaskNumbers(dsOtherInfo["PrevDiscoverNum"].ToString().Trim());
            lblAmex.Text = Server.HtmlEncode(dsOtherInfo["AmexApplied"].ToString().Trim());
            lblAmexNum.Text = MaskNumbers(dsOtherInfo["PrevAmexNum"].ToString().Trim());            
            lblJCB.Text = Server.HtmlEncode(dsOtherInfo["JCBApplied"].ToString().Trim());
            lblJCBNum.Text = MaskNumbers(dsOtherInfo["PrevJCBNum"].ToString().Trim());

            if (dsOtherInfo["DiscoverApplied"].ToString().Trim() == "Yes - Existing")
                pnlDiscoverNum.Visible = true;
            else
                pnlDiscoverNum.Visible = false;

            if (dsOtherInfo["AmexApplied"].ToString().Trim() == "Yes - Existing")
                pnlAmexNum.Visible = true;
            else
                pnlAmexNum.Visible = false;

            if (dsOtherInfo["JCBApplied"].ToString().Trim() == "Yes - Existing")
                pnlJCBNum.Visible = true;               
            else
                pnlJCBNum.Visible = false;                

        }//end if
        else
        {
            pnlDiscoverNum.Visible = false;
            pnlAmexNum.Visible = false;
            pnlJCBNum.Visible = false;            
        }

        /*OnlineAppStatusBL StatusInfo = new OnlineAppStatusBL(AppId);
        PartnerDS.OnlineAppStatusFieldsDataTable dt = StatusInfo.GetStatusFields();
        if (dt.Rows.Count > 0)
        {

        }*/
    }//end fuction populate page

    //This function masks numbers
    protected string MaskNumbers( string strValue)
    {
        return "xxxxxx" + strValue;
    }

    protected void btnPOS2013Yes_Click(object sender, EventArgs e)
    {
        Response.Redirect("ContactPOS2013.aspx", false);
    }

    protected void btnPOS2013No_Click(object sender, EventArgs e)
    {
        Response.Redirect("https://merchant.intuit.com/signup/?a=chkt&api_key=inTuIt&b=posmswoc&offer_code=posms_023_n-445-16463", false);
    }

    //This function handles the submit button click event
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        CreatePDF proc = new CreatePDF(AppId);
        string processor = proc.ReturnProcessorName();
        string pid = proc.ReturnPackageId();
        try
        {
            /*
            CommonFunctions UpdateGeneralInfo = new CommonFunctions(AppId);
            UpdateGeneralInfo.SetPageCount();
            UpdateGeneralInfo.SetGWPageCount();*/ //update gateway page count since information recorded is either same or more than those required for Gateway Account

            NewAppInfo AppInfo = new NewAppInfo(AppId);
            string Status = AppInfo.ReturnStatus();
            
            if ((pid == "254") && (Status.ToLower().Contains("completed")))
            {
                //lblUser.Text = "Are you using or planing to use Intuit QuickBooks POS V.11 or 2013 for this account?";
                //ModalPopupExtender1.Show();
                Response.Redirect("ContactPOS2013.aspx", false);
                //ClientScript.RegisterStartupScript(this.GetType(), "PopupScript", "ConfirmPOS2013();", true);
            }
            else
            {
                AppInfo.checkPageRedirect();
            }
            
            /*
            else if (Status.ToLower().Contains("incomplete"))
            {
                SetErrorMessage("Please complete the application before submitting.");
            }
            else {
                Response.Redirect("../notice.aspx", false);
            }*/
        }
        catch (Exception err)
        {
            CreateOnlineAppLog Log = new CreateOnlineAppLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + Session["AppId"].ToString() + " - " + "Merchant banking.aspx Error - " + err.Message);
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
