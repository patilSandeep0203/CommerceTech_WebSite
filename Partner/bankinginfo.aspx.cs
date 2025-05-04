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
using System.Text.RegularExpressions;

public partial class Merchant_bankinginfo : System.Web.UI.Page
{
    private string strBankRoutingNum = "-";
    private string strBankAccountNum = "-";
    private string strPrevAmexNum = "-";
    private string strPrevDiscoverNum = "-";

    private bool onlineDebitBit;
    private bool ebtBit;
    private bool textChanged = false;

    private static string strReplace = "";
    private static int PID = 0;
    private static int AppId = 0;    
    private static string strError="";
    private static string strSW = string.Empty;
    private static string nextNavigatePage = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        lblUSDA.Visible = false;
        txtUSDA.Visible = false;
        lblError.Visible = false;
        ValidateSummary.Enabled = true;
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
                Response.Redirect("/Application/default.aspx?Authentication=False");
            else
            {
                try
                {
                    if (!Locked())
                    {
                        //Check if banking info has already been entered. If yes, 
                        //then check if user wants to edit info
                        //else redirect to banking.aspx.
                        OtherInfo other = new OtherInfo(AppId);
                        
                        if (other.CheckOtherInfoComplete( "Merchant") > 0)
                        {
                            if (Request.Params.Get("EditBanking") != null)
                            {
                                pnlBankingInfo.Visible = true;
                                pnlNBC.Visible = false;
                                pnlProcessingLimits.Visible = false;
                            }
                            else if (Request.Params.Get("EditProcessing") != null)
                            {
                                pnlBankingInfo.Visible = false;
                                pnlNBC.Visible = false;
                                pnlProcessingLimits.Visible = true;
                            }
                            else if (Request.Params.Get("EditNBC") != null)
                            {
                                pnlBankingInfo.Visible = false;
                                pnlNBC.Visible = true;
                                pnlProcessingLimits.Visible = false;
                            }
                            else
                                Response.Redirect("banking.aspx", false);
                        }
                        
                        PopulatePage();
                    }
                    else
                    {
                        Response.Redirect("banking.aspx", false);
                        SetErrorMessage("The status of your application prevents it from being edited");
                        btnSubmit.Enabled = false;
                    }
                }
                catch (Exception err)
                {
                    CreateOnlineAppLog Log = new CreateOnlineAppLog();
                    Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + Session["AppId"].ToString() + " - " + "Merchant bankinginfo.aspx Page Load Error - " + err.Message);
                     SetErrorMessage("Error Processing Request. Please contact Technical Support: <a href='mailto:support@commercetech.com?Subject=Online Application Error'> support@commercetech.com. </a>.");
                }
            }
        }//end if
    }//end page load

    //This function checks if the application is locked
    protected bool Locked()
    {
        if (Session["Locked"].ToString().Trim() == "Yes")
            return true;
        else
            return false;
    }

    protected void lstEBT_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lstEBT.SelectedItem.Text == "Yes - Existing")
        {
            //pnlUSDA.Visible = true;
            lblUSDA.Visible = true;
            txtUSDA.Visible = true;
        }
        else
        {
            //pnlUSDA.Visible = false;
            lblUSDA.Visible = false;
            txtUSDA.Visible = false;
        }

        textChangedEventHandler(sender, e);
    }

    //This function populates page
    protected void PopulatePage()
    {
        lnkPrevAmex.Visible = false;
        lnkPrevDisc.Visible = false;
        lnkRoutingNum.Visible = false;
        lnkAccountNum.Visible = false;
        lblUSDA.Visible = false;
        txtUSDA.Visible = false;
        //Get states
        CommonListData Data = new CommonListData();
        DataSet dsStates = Data.GetCommonData("States");
        if (dsStates.Tables["States"].Rows.Count > 0)
        {
            lstState.DataSource = dsStates.Tables["States"];
            lstState.DataTextField = "StateID";
            lstState.DataValueField = "StateID";
            lstState.DataBind();
        }//end if count not 0
        lstState.Items.Add("");

        DataSet dsCountries = Data.GetCommonData("Countries");
        if (dsCountries.Tables["Countries"].Rows.Count > 0)
        {
            lstCountry.DataSource = dsCountries.Tables["Countries"];
            lstCountry.DataTextField = "Country";
            lstCountry.DataValueField = "Country";
            lstCountry.DataBind();
        }//end if count not 0
        lstCountry.Items.Add("");
        lstCountry.SelectedValue = "United States";

        //lstOnlineDebit.SelectedValue = "No";
        //lstEBT.SelectedValue = "No";

        PID = Convert.ToInt32(Session["PID"]);
        if ((PID == 178) || (PID == 182) || (PID == 243) || (PID == 247) || (PID == 196))
        {
            lstState.Enabled = false;
            ValidateState.Enabled = false;
        }

        //Get Banks
        CommonListData Banks = new CommonListData();
        DataSet dsBanks = Banks.GetCommonData("Banks");
        if (dsBanks.Tables["Banks"].Rows.Count > 0)
        {
            lstBankName.DataSource = dsBanks.Tables["Banks"];
            lstBankName.DataTextField = "BankName";
            lstBankName.DataValueField = "BankName";
            lstBankName.DataBind();
        }//end if count not 0
        ListItem lstItemBank = new ListItem();
        lstItemBank.Text = "OTHER";
        lstItemBank.Value = "OTHER";
        lstBankName.Items.Add(lstItemBank); 
        lstBankName.Items.Add(""); 
		lstBankName.SelectedValue = "";
		
        //Get Banking Info
        BankingInfo BankInfo = new BankingInfo(AppId);
        DataSet ds = BankInfo.GetBankingInfo();
        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow drBank = ds.Tables[0].Rows[0];
            lstBankName.SelectedIndex = lstBankName.Items.IndexOf(lstBankName.Items.FindByText(drBank["BankName"].ToString().Trim()));
            if (drBank["BankName"].ToString().Trim().ToLower().Contains("other"))
            {
                txtOtherBank.Enabled = true;
                txtOtherBank.Text = drBank["OtherBank"].ToString().Trim();
            }
            txtBankAddress.Text = drBank["BankAddress"].ToString().Trim();
            txtCity.Text = drBank["BankCity"].ToString().Trim();
            txtRegion.Text = drBank["BankRegion"].ToString().Trim();
            txtZipCode.Text = drBank["BankZip"].ToString().Trim();

            if (drBank["BankCountry"].ToString() == "United States")
            {
                lstCountry.SelectedValue = "United States";
            }
            else if (drBank["BankCountry"].ToString() == "Canada")
            { 
                lstCountry.SelectedValue = "Canada"; 
            }
            else
            {
                lstState.Enabled = false;
                ValidateState.Enabled = false;
                lstCountry.SelectedIndex = lstCountry.Items.IndexOf(lstCountry.Items.FindByText(drBank["BankCountry"].ToString().Trim()));
            }

            lstState.SelectedIndex = lstState.Items.IndexOf(lstState.Items.FindByText(drBank["BankState"].ToString().Trim()));
            txtPhone.Text = drBank["BankPhone"].ToString().Trim();
            txtNameOnChecking.Text = drBank["NameOnCheckingAcct"].ToString().Trim();
            //if no previous data
            if (drBank["BankRoutingNumber"].ToString().Trim() == "")
                txtBankRoutingNumber.Enabled = true;
            else //contains previous data, do not enable editing and show the Edit link
            {
                txtBankRoutingNumber.Text = "xxxxxx" + drBank["BankRoutingNumber"].ToString().Trim();
                txtBankRoutingNumber.Enabled = false;
                lnkRoutingNum.Visible = true;
                ValidateRoutingNo.Enabled = false;
                RERoutingNum.Enabled = false;
            }

            //if no previous data
            if (drBank["BankAccountNumber"].ToString().Trim() == "")
            {
                txtAcctNumber.Enabled = true;
            }
            else //contains previous data, do not enable editing and show the Edit link
            {
                txtAcctNumber.Text = "xxxxxx" + drBank["BankAccountNumber"].ToString().Trim();
                txtAcctNumber.Enabled = false;
                lnkAccountNum.Visible = true;
                ValidateAcctNo.Enabled = false;
                REAcctNum.Enabled = false;
            }
        }//end if count not 0

        NewAppInfo AddlServ = new NewAppInfo(AppId);
        DataSet ds1 = AddlServ.GetNewAppData();
        if (ds1.Tables[0].Rows.Count > 0)
        {
            DataRow dr1 = ds1.Tables[0].Rows[0];
            if ((Session["AcctType"].ToString() == "1") || (Session["AcctType"].ToString() == "4"))
            {
                ProcessingInfo CPCNP = new ProcessingInfo(AppId);
                string CardPresent = CPCNP.ReturnCardPresent();
                string Processor = CPCNP.ReturnProcessorName();
                if (CardPresent == "CP")
                {
                    lstOnlineDebit.Enabled = true;
                    lstEBT.Enabled = true;
                    RequiredFieldValidatorEBT.Enabled = true;
                    RequiredFieldValidatorOnlineDebit.Enabled = true;
                }
                else
                {
                    RequiredFieldValidatorEBT.Enabled = false;
                    pnlOnlineDebit.Visible = false;
                    pnlEBT.Visible = false;
                    lstOnlineDebit.Enabled = false;
                    lstEBT.Enabled = false;
                    RequiredFieldValidatorOnlineDebit.Enabled = false;
                }

            }
        }

        NewAppTable newAppTable = new NewAppTable();
        DataSet dsDBEBT = new DataSet();
        dsDBEBT = newAppTable.GetDebitEBTInfo(AppId);

        string USDANum = "";

        if (dsDBEBT.Tables["OnlineAppNewApp"].Rows.Count > 0)
        {
            DataRow drDBEBT = dsDBEBT.Tables["OnlineAppNewApp"].Rows[0];
            if ((Convert.ToBoolean(drDBEBT["OnlineDebit"]) == true))
            {

                 lstOnlineDebit.SelectedValue = "Yes";

            }
           
            else if (Convert.ToBoolean(drDBEBT["OnlineDebit"]) == false)
            {
                lstOnlineDebit.SelectedValue = "No";
            }

            if ((Convert.ToBoolean(drDBEBT["EBT"]) == true) && (Convert.ToString(drDBEBT["StatusEBT"]) == "Yes"))
            {
                lstEBT.SelectedValue = "Yes";
            }
            else if ((Convert.ToBoolean(drDBEBT["EBT"]) == true) && (Convert.ToString(drDBEBT["StatusEBT"]) == "Yes - Existing"))
            {
                lstEBT.SelectedValue = "Yes - Existing";
            }
            else if (Convert.ToBoolean(drDBEBT["EBT"]) == false)
            {
                lstEBT.SelectedValue = "No";
            }
            

            lstEBT.SelectedValue = Convert.ToString(drDBEBT["StatusEBT"]);
            USDANum = Convert.ToString(drDBEBT["USDANum"]);
        }

        

        if (lstEBT.SelectedItem.Text == "Yes - Existing")
        {
            //pnlUSDA.Visible = true;
            lblUSDA.Visible = true;
            txtUSDA.Visible = true;
            txtUSDA.Text = USDANum;
            //lstEBT.SelectedValue = "Yes - Existing";
        }
        else
        {
            //pnlUSDA.Visible = false;
            lblUSDA.Visible = false;
            txtUSDA.Visible = false;
        }
               
        //Get info from otherinfo table
        OtherInfo OtherInfo = new OtherInfo(AppId);
        DataSet dtOtherInfo = OtherInfo.GetOtherInfo();
        if (dtOtherInfo.Tables["OnlineAppOtherInfo"].Rows.Count > 0)
        {
            DataRow dsOtherInfo = dtOtherInfo.Tables["OnlineAppOtherInfo"].Rows[0];

            txtAvgTicket.Text = dsOtherInfo["AverageTicket"].ToString().Trim();
            txtMaxTicket.Text = dsOtherInfo["MaxTicket"].ToString().Trim();
            txtMonthlyVol.Text = dsOtherInfo["MonthlyVolume"].ToString().Trim();

            int index = 0;

            //If Processor is Merrick, iPayment, IMS or Sage then default Discover and JCB to MAP
            ProcessingInfo Processor = new ProcessingInfo(AppId);
            DataSet dsProcessor = Processor.GetProcessingInfo();
            DataRow drProcessor = dsProcessor.Tables[0].Rows[0];
            string processorName = drProcessor["Processor"].ToString().Trim();
            if (processorName.Contains("Merrick") || processorName.Contains("iPayment") || processorName.Contains("Intuit") || processorName.Contains("IMS") || processorName.Contains("Sage"))
            {
                ListItem lstItem = new ListItem();
                BankingInfo BankingInfoFull = new BankingInfo(AppId);
                DataSet dsBankingInfoFull = BankingInfoFull.GetBankingInfoFull();
                if (dsBankingInfoFull.Tables[0].Rows.Count > 0)
                {
                    DataRow drBankFull = dsBankingInfoFull.Tables[0].Rows[0];
                    if (drBankFull["PrevDiscoverNum"].ToString().Trim() == "")
                    {
                        //lstItem.Value = "MAP";
                        //add to Discover list
                        lstDiscover.Items.Add(lstItem);
                        lstDiscover.SelectedValue = "Yes";
                        lstDiscover.Enabled = false;
                        pnlJCB1.Enabled = false;
                        //lstDiscover.Enabled = true;
                    }
                    else{
                        //lstItem.Value = "MAP";
                        //add to Discover list
                        lstDiscover.Items.Add(lstItem);
                        lstDiscover.SelectedValue = "Yes - Existing";
                        //lstDiscover.Enabled = false;
                        lstDiscover.Enabled = true;
                    }
                    //add to JCB list
                    if (drBankFull["PrevJCBNum"].ToString().Trim() == "")
                    {
                        lstJCB.Items.Add(lstItem);
                        lstJCB.SelectedValue = "Yes";
                        lstJCB.Enabled = false;
                    }
                    else
                    {
                        //lstJCB.Items.Add(lstItem);
                        //lstJCB.SelectedValue = "Yes - Existing";
                        //lstJCB.Enabled = true;

                        lstJCB.Items.Add(lstItem);
                        lstJCB.SelectedValue = "Yes";
                        lstJCB.Enabled = false;
                    }
                }
            }
            else
            {
                index = lstDiscover.Items.IndexOf(lstDiscover.Items.FindByText(dsOtherInfo["DiscoverApplied"].ToString().Trim()));
                lstDiscover.SelectedIndex = index;
            }
            index = lstAmex.Items.IndexOf(lstAmex.Items.FindByText(dsOtherInfo["AmexApplied"].ToString().Trim()));
            lstAmex.SelectedIndex = index;
            index = lstJCB.Items.IndexOf(lstJCB.Items.FindByText(dsOtherInfo["JCBApplied"].ToString().Trim()));
            lstJCB.SelectedIndex = index;
            //txtJCBNum.Text = dsOtherInfo["PrevJCBNum"].ToString().Trim();

            if (dsOtherInfo["DiscoverApplied"].ToString().Trim() == "Yes - Existing")
            {
                pnlDiscoverNum.Visible = true;
                txtDiscoverNum.BackColor = System.Drawing.Color.White;
                if (dsOtherInfo["PrevDiscoverNum"].ToString().Trim() != "")
                {
                    //disable and show masked text and Edit Link
                    txtDiscoverNum.Enabled = false;
                    txtDiscoverNum.Text = "xxxxxxxxxxx" + dsOtherInfo["PrevDiscoverNum"].ToString().Trim();
                    lnkPrevDisc.Visible = true;
                    ValidateDiscoverNum.Enabled = false;
                    REDiscNum.Enabled = false;
                }
            }
            else if (dsOtherInfo["DiscoverApplied"].ToString().Trim() == "")
            {
                pnlDiscoverNum.Visible = false;
                txtDiscoverNum.Enabled = false;
                lstDiscover.Enabled = true;
                ValidateDiscoverNum.Enabled = false;
                REDiscNum.Enabled = false;
            }
            else
            {
                pnlDiscoverNum.Visible = false;
                txtDiscoverNum.Enabled = false;
                txtDiscoverNum.BackColor = System.Drawing.Color.DarkGray;
                ValidateDiscoverNum.Enabled = false;
                REDiscNum.Enabled = false;
            }

            if (dsOtherInfo["AmexApplied"].ToString().Trim() == "Yes - Existing")
            {
                pnlAmexNum.Visible = true; 
                txtAmexNum.BackColor = System.Drawing.Color.White;
                //if data previously entered
                if (dsOtherInfo["PrevAmexNum"].ToString().Trim() != "")
                {
                    //disable and show masked text and Edit Link
                    txtAmexNum.Enabled = false;
                    txtAmexNum.Text = "xxxxxx" + dsOtherInfo["PrevAmexNum"].ToString().Trim();
                    lnkPrevAmex.Visible = true;
                    ValidateAmexNum.Enabled = false;
                    REAmexNum.Enabled = false;
                }
            }
            else if (dsOtherInfo["AmexApplied"].ToString().Trim() == "")
            {
                pnlAmexNum.Visible = false;
                txtAmexNum.Enabled = false;
                lstAmex.Enabled = true;
                txtAmexNum.BackColor = System.Drawing.Color.DarkGray;
                ValidateAmexNum.Enabled = false;
                REAmexNum.Enabled = false;
            }
            else
            {
                pnlAmexNum.Visible = false;
                txtAmexNum.Enabled = false;
                txtAmexNum.BackColor = System.Drawing.Color.DarkGray;
                ValidateAmexNum.Enabled = false;
                REAmexNum.Enabled = false;
            }

            if (dsOtherInfo["JCBApplied"].ToString().Trim() == "Yes - Existing")
            {
                pnlJCBNum.Visible = true;
                txtJCBNum.Enabled = true;
                txtJCBNum.BackColor = System.Drawing.Color.White;
                ValidateJCBNum.Enabled = true;
            }
            else if (dsOtherInfo["JCBApplied"].ToString().Trim() == "Yes")
            {
                pnlJCBNum.Visible = false;
                txtJCBNum.Enabled = false;
                lstJCB.Enabled = true;
                txtJCBNum.BackColor = System.Drawing.Color.DarkGray;
                ValidateJCBNum.Enabled = false;
            }
            else
            {
                lstJCB.SelectedValue = "Yes";
                pnlJCBNum.Visible = false;
                txtJCBNum.Enabled = false;
                txtJCBNum.BackColor = System.Drawing.Color.DarkGray;
                ValidateJCBNum.Enabled = false;
            }
        
        }//end if
        else
        {
            pnlDiscoverNum.Visible = false;
            pnlAmexNum.Visible = false;
            pnlJCBNum.Visible = false;
            txtDiscoverNum.Enabled = false;
            txtAmexNum.Enabled = false;
            txtJCBNum.Enabled = false;
            txtDiscoverNum.BackColor = System.Drawing.Color.DarkGray;
            txtJCBNum.BackColor = System.Drawing.Color.DarkGray;
            txtAmexNum.BackColor = System.Drawing.Color.DarkGray;
            ValidateJCBNum.Enabled = false;
            ValidateAmexNum.Enabled = false;
            ValidateDiscoverNum.Enabled = false;

            ProcessingInfo Processor = new ProcessingInfo(AppId);
            DataSet dsProcessor = Processor.GetProcessingInfo();
            DataRow drProcessor = dsProcessor.Tables[0].Rows[0];
            string processorName = drProcessor["Processor"].ToString().Trim();
            if (processorName.Contains("Merrick") || processorName.Contains("iPayment") || processorName.Contains("Intuit") || processorName.Contains("IMS") || processorName.Contains("Sage"))
            {
                ListItem lstItem = new ListItem();
                lstItem.Value = "MAP";

                //add to Discover list
                lstDiscover.Items.Add(lstItem);
                lstDiscover.SelectedValue = "Yes";
                lstDiscover.Enabled = false;
                //lstDiscover.Enabled = true;

                //add to JCB list
                lstJCB.Items.Add(lstItem);
                lstJCB.SelectedValue = "Yes";
                lstJCB.Enabled = false;
            }
            else
            {
                lstDiscover.Enabled = true;
                lstJCB.Enabled = true;
            }
        }
    }//end fuction populate page

    protected void textChangedEventHandler(object sender, EventArgs e)
    {
        textChanged = true;
    }

    //This function generates a hash to encrypt the password
    protected string GenerateHash(string salt, string strToEncode)
    {
        string hash = FormsAuthentication.HashPasswordForStoringInConfigFile(strToEncode + salt, "sha1");
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
        if (Convert.ToString(lstEBT.SelectedItem.Text).Contains("Yes"))
        {
            ebtBit = true;
        }
        else if (Convert.ToString(lstEBT.SelectedItem.Text).Contains("No"))
        {
            ebtBit = false;
        }
        else {
            ebtBit = false;
        }
        if (Convert.ToString(lstOnlineDebit.SelectedItem.Text).Contains("Yes"))
        {
            onlineDebitBit = true;
        }
        else if (Convert.ToString(lstOnlineDebit.SelectedItem.Text).Contains("No"))
        {
            onlineDebitBit = false;
        }
        else {
            onlineDebitBit = false;
        }

        btnSave_Click(sender, e);
        //Adding redirect for QB accounts since we no longer receive the PDF application.
        //We will collect info from merchant and then send them to Intuit to submit formal app.
        CreatePDF proc = new CreatePDF(AppId);
        string processor = proc.ReturnProcessorName();
        string pid = proc.ReturnPackageId();

        ValidateSummary.Enabled = true;
        try
        {
            if (Page.IsValid)
            {
                //Validate data
                bool retValidate = ValidateData();
                
                if (retValidate)
                {
                    string salt = GetSalt();
                    if (Request.Params.Get("EditBanking") != null)
                    {
                        if (ValidateRoutingNumber() == true)
                        {
                            InsertUpdateBankingInformation(true);
                            UpdateCommonInformation(true);

                            CommonFunctions UpdateGeneralInfo = new CommonFunctions(AppId);
                            UpdateGeneralInfo.SetPageCount();
                            UpdateGeneralInfo.SetGWPageCount(); //update gateway page count since information recorded is either same or more than those required for Gateway Account

                            OnlineAppProfile Profile = new OnlineAppProfile(AppId);
                            int AcctType = 0;

                            DataSet ds = Profile.GetProfileData();
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                DataRow dr = ds.Tables[0].Rows[0];
                                AcctType = Convert.ToInt32(dr["AcctType"]);
                            }

                            NewAppInfo AppInfo = new NewAppInfo(AppId);
                            string Status = AppInfo.ReturnStatus();

                            string keepStatus = AppInfo.ReturnStatus();

                            if ((Status.ToLower().Contains("completed")) && (AcctType == 4))
                            {
                                AppInfo.UpdateStatus(Status, "Gateway");
                            }


                            if (Status.ToLower().Contains("completed"))
                            {
                                AppInfo.UpdateStatus(Status, "Gateway");
                            }

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

                            if (keepStatus == "SUBMITTED FOR REVIEW" || keepStatus == "SUBMITTED TO UNDERWRITING" || keepStatus == "DECLINED" || keepStatus == "PENDING" || keepStatus == "ACTIVE" || keepStatus == "ACTIVE RESIDUAL" || keepStatus == "CLOSED RESIDUAL" || keepStatus == "CLOSED" || keepStatus == "DECLINED - NO OTHER OPTIONS" || keepStatus == "CANCELLED")
                            {
                                AppInfo.UpdateStatus(keepStatus, "Gateway");
                            }

                            /*
                            else if (Status.ToLower().Contains("incomplete")) 
                                SetErrorMessage("Please complete the application before submitting.");
                            else
                                Response.Redirect("../notice.aspx", false);*/
                            CommonFunctions GeneralInfo = new CommonFunctions(AppId);
                            GeneralInfo.SetPageCount();
                            if (textChanged == true)
                            {
                                GeneralInfo.UpdateLastModified();
                                LogBL LogData = new LogBL(AppId);
                                LogData.InsertLogData(Convert.ToInt32(Session["AffiliateID"]), "Merchant Business Information Updated");
                            }
                        }
                    }
                    else if (Request.Params.Get("EditProcessing") != null)
                    {
                        bool retTicket = ValidateTicket();
                        if (retTicket)
                        {
                            UpdateProcessing(true);
                            UpdateCommonInformation(true);

                            InsertUpdateBankingInformation(true);

                            CommonFunctions UpdateGeneralInfo = new CommonFunctions(AppId);
                            UpdateGeneralInfo.SetPageCount();
                            UpdateGeneralInfo.SetGWPageCount(); //update gateway page count since information recorded is either same or more than those required for Gateway Account

                            OnlineAppProfile Profile = new OnlineAppProfile(AppId);
                            int AcctType = 0;

                            DataSet ds = Profile.GetProfileData();
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                DataRow dr = ds.Tables[0].Rows[0];
                                AcctType = Convert.ToInt32(dr["AcctType"]);
                            }

                            NewAppInfo AppInfo = new NewAppInfo(AppId);
                            string Status = AppInfo.ReturnStatus();

                            string keepStatus = AppInfo.ReturnStatus();

                            if ((Status.ToLower().Contains("completed")) && (AcctType == 4))
                            {
                                AppInfo.UpdateStatus(Status, "Gateway");
                            }

                            if ((pid == "254") && (Status.ToLower().Contains("completed")))
                            {
                                lblUser.Text = "Are you using or planing to use Intuit QuickBooks POS V.11 or 2013 for this account?";
                                ModalPopupExtender1.Show();
                                //ClientScript.RegisterStartupScript(this.GetType(), "PopupScript", "ConfirmPOS2013();", true);
                            }
                            else
                            {
                                AppInfo.checkPageRedirect();
                            }

                            if (keepStatus == "SUBMITTED FOR REVIEW" || keepStatus == "SUBMITTED TO UNDERWRITING" || keepStatus == "DECLINED" || keepStatus == "PENDING" || keepStatus == "ACTIVE" || keepStatus == "ACTIVE RESIDUAL" || keepStatus == "CLOSED RESIDUAL" || keepStatus == "CLOSED" || keepStatus == "DECLINED - NO OTHER OPTIONS" || keepStatus == "CANCELLED")
                            {
                                AppInfo.UpdateStatus(keepStatus, "Gateway");
                            }

                            CommonFunctions GeneralInfo = new CommonFunctions(AppId);
                            GeneralInfo.SetPageCount();
                            if (textChanged == true)
                            {
                                GeneralInfo.UpdateLastModified();
                                LogBL LogData = new LogBL(AppId);
                                LogData.InsertLogData(Convert.ToInt32(Session["AffiliateID"]), "Merchant Business Information Updated");
                            }
                        }//end if retTicket true
                        else
                        {
                            lblError.Visible = true;
                            lblError.Text = strError;
                        }
                    }
                    else if (Request.Params.Get("EditNBC") != null)
                    {
                        bool retNums = ValidateNumbers();
                        if (retNums)
                        {
                            InsertUpdateBankingInformation(true);

                            NewAppInfo AppInfo = new NewAppInfo(AppId);

                            string EBTStatus = Convert.ToString(lstEBT.SelectedItem.Text.Trim());

                            AppInfo.UpdateODBEBT(onlineDebitBit, ebtBit, EBTStatus);
                            //AppInfo.UpdateODBEBT(onlineDebitBit, ebtBit);

                            UpdateNBC(true);
                            UpdateCommonInformation(true);

                            CommonFunctions UpdateGeneralInfo = new CommonFunctions(AppId);
                            UpdateGeneralInfo.SetPageCount();
                            UpdateGeneralInfo.SetGWPageCount(); //update gateway page count since information recorded is either same or more than those required for Gateway Account

                            OnlineAppProfile Profile = new OnlineAppProfile(AppId);
                            int AcctType = 0;

                            DataSet ds = Profile.GetProfileData();
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                DataRow dr = ds.Tables[0].Rows[0];
                                AcctType = Convert.ToInt32(dr["AcctType"]);
                            }

                            if ((AcctType == 1) || (AcctType == 4))
                            {
                                AppInfo.UpdateUSDANum(txtUSDA.Text.Trim());

                            }

                            string Status = AppInfo.ReturnStatus();

                            string keepStatus = Status;

                            if ((Status.ToLower().Contains("completed")) && (AcctType == 4))
                            {
                                AppInfo.UpdateStatus(Status, "Gateway");
                            }

                            if ((pid == "254") && (Status.ToLower().Contains("completed")))
                            {
                                lblUser.Text = "Are you using or planing to use Intuit QuickBooks POS V.11 or 2013 for this account?";
                                ModalPopupExtender1.Show();
                                //ClientScript.RegisterStartupScript(this.GetType(), "PopupScript", "ConfirmPOS2013();", true);
                            }
                            else
                            {
                                AppInfo.checkPageRedirect();
                            }

                            if (keepStatus == "SUBMITTED FOR REVIEW" || keepStatus == "SUBMITTED TO UNDERWRITING" || keepStatus == "DECLINED" || keepStatus == "PENDING" || keepStatus == "ACTIVE" || keepStatus == "ACTIVE RESIDUAL" || keepStatus == "CLOSED RESIDUAL" || keepStatus == "CLOSED" || keepStatus == "DECLINED - NO OTHER OPTIONS" || keepStatus == "CANCELLED")
                            {
                                AppInfo.UpdateStatus(keepStatus, "Gateway");
                            }
                        }
                        else
                        {
                            lblError.Visible = true;
                            lblError.Text = strError;
                        }

                        CommonFunctions GeneralInfo = new CommonFunctions(AppId);
                        GeneralInfo.SetPageCount();
                        if (textChanged == true)
                        {
                            GeneralInfo.UpdateLastModified();
                            LogBL LogData = new LogBL(AppId);
                            LogData.InsertLogData(Convert.ToInt32(Session["AffiliateID"]), "Merchant Business Information Updated");
                        }
                    }
                    else
                    {

                        string EBTStatus = Convert.ToString(lstOnlineDebit.SelectedValue.Trim());

                        NewAppInfo AppInfo1 = new NewAppInfo(AppId);
                        AppInfo1.UpdateODBEBT(onlineDebitBit, ebtBit, EBTStatus);
                        //AppInfo1.UpdateODBEBT(onlineDebitBit, ebtBit);


                        bool RetValBanking = ValidateRoutingNumber();

                        if (RetValBanking)
                            InsertUpdateBankingInformation(true);
                        else
                            SetErrorMessage("Length of the Bank Routing Number must be 9 characters long.");

                        bool retTicket = ValidateTicket();
                        bool retNums = ValidateNumbers();
                        if ((retTicket) && (retNums))
                        {
                            InsertUpdateOtherInformation(true);
                        }
                        else
                        {
                            lblError.Visible = true;
                            lblError.Text = strError;
                        }

                        if ((RetValBanking) && (retTicket) && (retNums))
                        {
                            UpdateCommonInformation(true);

                            CommonFunctions UpdateGeneralInfo = new CommonFunctions(AppId);
                            UpdateGeneralInfo.SetPageCount();
                            UpdateGeneralInfo.SetGWPageCount(); //update gateway page count since information recorded is either same or more than those required for Gateway Account
                            NewAppInfo AppInfo = new NewAppInfo(AppId);
                            string Status = AppInfo.ReturnStatus();
                            string keepStatus = Status;
                            if (Status.ToLower().Contains("completed"))
                            {
                                AppInfo.UpdateStatus(Status, "Gateway");
                            }

                            if ((pid == "254") && (Status.ToLower().Contains("completed")))
                            {
                                lblUser.Text = "Are you using or planing to use Intuit QuickBooks POS V.11 or 2013 for this account?";
                                ModalPopupExtender1.Show();
                                //ClientScript.RegisterStartupScript(this.GetType(), "PopupScript", "ConfirmPOS2013();", true);
                            }
                            else
                            {
                                AppInfo.checkPageRedirect();
                            }

                            if (keepStatus == "SUBMITTED FOR REVIEW" || keepStatus == "SUBMITTED TO UNDERWRITING" || keepStatus == "DECLINED" || keepStatus == "PENDING" || keepStatus == "ACTIVE" || keepStatus == "ACTIVE RESIDUAL" || keepStatus == "CLOSED RESIDUAL" || keepStatus == "CLOSED" || keepStatus == "DECLINED - NO OTHER OPTIONS" || keepStatus == "CANCELLED")
                            {
                                AppInfo.UpdateStatus(keepStatus, "Gateway");
                            }
                        }

                        OnlineAppProfile Profile = new OnlineAppProfile(AppId);
                        int AcctType = 0;

                        DataSet ds = Profile.GetProfileData();
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            AcctType = Convert.ToInt32(dr["AcctType"]);
                        }

                        if ((AcctType == 1) || (AcctType == 4))
                        {
                            AppInfo1.UpdateUSDANum(txtUSDA.Text.Trim());

                        }

                        CommonFunctions GeneralInfo = new CommonFunctions(AppId);
                        GeneralInfo.SetPageCount();
                        if (textChanged == true)
                        {
                            GeneralInfo.UpdateLastModified();
                            LogBL LogData = new LogBL(AppId);
                            LogData.InsertLogData(Convert.ToInt32(Session["AffiliateID"]), "Merchant Business Information Updated");
                        }
                    }
                }//end if ValidateDate
            }//end if Page valid
        }//end try
        catch (Exception err)
        {
            CreateOnlineAppLog Log = new CreateOnlineAppLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + Session["AppId"].ToString() + " - " + "Merchant bankinginfo.aspx Page - " + err.Message);
            SetErrorMessage("Error Processing Request. Please contact Technical Support: <a href='mailto:support@commercetech.com?Subject=Online Application Error'> support@commercetech.com. </a>.");
        }       
    }//end function submit button click

    //This function checks length of routing number
    protected bool ValidateRoutingNumber()
    {
        //if Routing Number entered is fewer than 9 chars and is not BLANK
        if ((txtBankRoutingNumber.Text.Trim().Length < 9) && txtBankRoutingNumber.Text.Trim() != "")
        {
            SetErrorMessage("Routing Number must be exactly 9 characters");
            return false;
        }
        return true;
    }

    public void checkPageRedirect()
    {
        OnlineAppProfile Profile = new OnlineAppProfile(AppId);
        int AcctType = 0;

        DataSet ds = Profile.GetProfileData();
        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr = ds.Tables[0].Rows[0];
            AcctType = Convert.ToInt32(dr["AcctType"]);
        }

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
            HttpContext.Current.Response.Redirect("/Application/locked.aspx", false);
        }
        else if ((AcctType == 1) && (Status.ToLower().Contains("completed")))
        {
            HttpContext.Current.Response.Redirect("/Application/notice.aspx", false);
        }
        else if (((AcctType == 1) || (AcctType == 4)) && ((Status.ToLower().Contains("incomplete"))))
        {
            if (cardPCTCom == 0)
            {
                HttpContext.Current.Response.Redirect("/Application/Merchant/cardpct.aspx", false);
            }
            else if (companyCom == 0)
            {
                HttpContext.Current.Response.Redirect("/Application/Merchant/companyinfo.aspx?EditCompany=True", false);
            }
            else if (busCom == 0)
            {
                HttpContext.Current.Response.Redirect("/Application/Merchant/businessinfo.aspx?EditBusiness=True", false);
            }
            else if (principalCom == 0)
            {
                HttpContext.Current.Response.Redirect("/Application/Merchant/principalinfo.aspx?EditPrincipal=True", false);
            }
            else if (otherCom == 0)
            {
                HttpContext.Current.Response.Redirect("/Application/Merchant/bankinginfo.aspx", false);
            }
        }
        else if ((AcctType == 4) && (Status.ToLower().Contains("incomplete")))
        {
            if (StatusGW.ToLower().Contains("completed"))
            {
                HttpContext.Current.Response.Redirect("/Application/notice.aspx", false);
            }
            else if (StatusGW.ToLower().Contains("incomplete"))
            {
                if (companyCom == 0)
                {
                    HttpContext.Current.Response.Redirect("/Application/Gateway/companyinfo.aspx?EditCompany=True", false);
                }
                else if (busComGW == 0)
                {
                    HttpContext.Current.Response.Redirect("/Application/Gateway/businessinfo.aspx?EditBusiness=True", false);
                }
                else if (principalComGW == 0)
                {
                    HttpContext.Current.Response.Redirect("/Application/Gateway/principalinfo.aspx?EditPrincipal=True", false);
                }
                else if (bankComGW == 0)
                {
                    HttpContext.Current.Response.Redirect("/Application/Gateway/bankinginfo.aspx?EditBanking=True", false);
                }
                else if (bankComGW == 0)
                {
                    HttpContext.Current.Response.Redirect("/Application/Gateway/bankinginfo.aspx?EditBanking=True", false);
                }   
            }
        }
    }

    //This function updates page count, last modified
    protected void UpdateCommonInformation(bool bComplete)
    {
        //If Merchant and Gateway account then update Gateway Reprogram to completed too
        if (Session["AcctType"].ToString() == "4")
        {
            ReprogramInfo UpdateReprogram = new ReprogramInfo(AppId);
            bool retVal = UpdateReprogram.UpdateReprogramInfo("", "", "", "", "", "", "", "", "", "", bComplete, false);
        }

        //Set last modified date in newapp table
        CommonFunctions GeneralInfo = new CommonFunctions(AppId);


        GeneralInfo.SetPageCount();
        GeneralInfo.SetGWPageCount(); //update gateway page count since information recorded is either same or more than those required for Gateway Account

        if (textChanged == true)
        {
            GeneralInfo.UpdateLastModified();
            LogBL LogData = new LogBL(AppId);
            LogData.InsertLogData(Convert.ToInt32(Session["AffiliateID"]), "Merchant Banking Information Updated");
        }      
    }

    //This function Inserts/Updates Banking Information
    protected void InsertUpdateBankingInformation(bool bComplete)
    {
        if (txtAcctNumber.Enabled == true)
            strBankAccountNum = txtAcctNumber.Text.Trim();
        if (txtBankRoutingNumber.Enabled == true)
            strBankRoutingNum = txtBankRoutingNumber.Text.Trim();

        BankingInfo BankInfo = new BankingInfo(AppId);
        bool retVal = BankInfo.UpdateBankingInfo(lstBankName.SelectedItem.Text.Trim().Replace("'", strReplace),
            txtOtherBank.Text.Trim().Replace("'", strReplace), txtBankAddress.Text.Trim().Replace("'", strReplace), 
            txtZipCode.Text.Trim().Replace("'", strReplace), txtCity.Text.Trim().Replace("'", strReplace),
            lstState.SelectedItem.Text.Trim().Replace("'", strReplace), txtRegion.Text.Trim().Replace("'", strReplace), 
            lstCountry.SelectedItem.Text.Trim().Replace("'", strReplace), txtNameOnChecking.Text.Trim().Replace("'", strReplace),
            strBankAccountNum, strBankRoutingNum, txtPhone.Text.Trim().Replace("'", strReplace),  bComplete);        
    }

    //This function Inserts/Updates Other Information
    protected void InsertUpdateOtherInformation(bool bComplete)
    {
        if (txtAmexNum.Enabled == true)
            strPrevAmexNum = txtAmexNum.Text.Trim().Replace("'", strReplace);
        
        if (txtDiscoverNum.Enabled == true)
            strPrevDiscoverNum = txtDiscoverNum.Text.Trim().Replace("'", strReplace);

        OtherInfo OtherInfoTable = new OtherInfo(AppId);
        bool retVal2 = OtherInfoTable.UpdateOtherInfo( lstDiscover.SelectedItem.Text.Trim().Replace("'", strReplace),
                lstAmex.SelectedItem.Text.Trim().Replace("'", strReplace), lstJCB.SelectedItem.Text.Trim().Replace("'", strReplace),
                strPrevDiscoverNum, strPrevAmexNum, txtJCBNum.Text.Trim().Replace("'", strReplace),
                txtMaxTicket.Text.Trim().Replace("'", strReplace), txtAvgTicket.Text.Trim().Replace("'", strReplace),
                txtMonthlyVol.Text.Trim().Replace("'", strReplace), bComplete);
    }

    public void UpdateProcessing(bool bComplete)
    {
        OtherInfo OtherInfoTable = new OtherInfo(AppId);
        int iRetVal = OtherInfoTable.UpdateOtherInfoProcessing(
            txtMaxTicket.Text.Trim().Replace("'", strReplace), txtAvgTicket.Text.Trim().Replace("'", strReplace),
            txtMonthlyVol.Text.Trim().Replace("'", strReplace), bComplete);
    }

    public void UpdateNBC(bool bComplete)
    {
        //if (txtAmexNum.Enabled == true)
            strPrevAmexNum = txtAmexNum.Text.Trim().Replace("'", strReplace);

        //if (txtDiscoverNum.Enabled == true)
            strPrevDiscoverNum = txtDiscoverNum.Text.Trim().Replace("'", strReplace);

        OtherInfo OtherInfoTable = new OtherInfo(AppId);
        int iRetVal = OtherInfoTable.UpdateOtherInfoNBC(
            lstDiscover.SelectedItem.Text.Trim().Replace("'", strReplace),
            lstAmex.SelectedItem.Text.Trim().Replace("'", strReplace),
            lstJCB.SelectedItem.Text.Trim().Replace("'", strReplace),
            strPrevDiscoverNum,
            strPrevAmexNum, txtJCBNum.Text.Trim().Replace("'", strReplace),
            bComplete);

    }

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
                    SetErrorMessage("You cannot use apostrophe, # or semi-colons in any of the following fields.");
                    return false;
                }
            }
        }
        return true;
    }//end function validate data

    //This function validates average, monthly and max ticket values
    protected bool ValidateTicket()
    {
        if (Convert.ToInt32(txtMaxTicket.Text.Trim()) < Convert.ToInt32(txtAvgTicket.Text.Trim()))
        {
            strError = "You have entered a Maximum Ticket amount that is less than your Average Ticket amount.\n";
            return false;
        }

        if (Convert.ToInt32(txtMonthlyVol.Text.Trim()) < Convert.ToInt32(txtMaxTicket.Text.Trim()))
        {
            strError = "You have entered a Monthly Volume that is less than your Maximum Ticket amount.\n";
            return false;
        }

        if (Convert.ToInt32(txtMonthlyVol.Text.Trim() ) <= Convert.ToInt32(txtAvgTicket.Text.Trim()))
        {
            strError = "You have entered a monthly volume that is less than or equal to your average transaction.\nYour average transaction is the average amount of one sale.\nYour monthly volume is your average transaction times the number of sales in a month.\nYour monthly volume should almost always be more than your average transaction value.\n\nPlease double check these values before you continue.";
            return false;
        }
        
        return true;
    }//end function validate ticket

    //This function validates average, monthly and max ticket values
    protected bool ValidateNumbers()
    {
        if (lstDiscover.SelectedItem.Text == "Yes - Existing")
        {
            if (txtDiscoverNum.Text.Trim().Length < 15)
            {
                strError = "Discover Merchant Numbers must be 15 characters long. Please check your Discover Mechant Number again.";
                return false;
            }
        }
        if (lstAmex.SelectedItem.Text == "Yes - Existing")
        {
            if (txtAmexNum.Text.Trim().Length < 10)
            {
                strError = "Amex Merchant Numbers must be 10 characters long. Please check your Amex Mechant Number again.";
                return false;
            }
        }
        return true;
    }//end function validate ticket

    //This function handles discover list selection changed event
    protected void lstDiscover_SelectedIndexChanged(object sender, EventArgs e)
    {
        BankingInfo BankingInfoFull = new BankingInfo(AppId);
        DataSet dsBankingInfoFull = BankingInfoFull.GetBankingInfoFull();
        if (dsBankingInfoFull.Tables[0].Rows.Count > 0)
        {
            DataRow drBankFull = dsBankingInfoFull.Tables[0].Rows[0];

            if (drBankFull["PrevDiscoverNum"].ToString().Trim() == "")
            {

                if (lstDiscover.SelectedItem.Text.Trim() == "Yes - Existing")
                {
                    pnlDiscoverNum.Visible = true;
                    txtDiscoverNum.Enabled = true;
                    txtDiscoverNum.BackColor = System.Drawing.Color.White;
                    ValidateDiscoverNum.Enabled = true;
                }
                else
                {
                    pnlDiscoverNum.Visible = false;
                    //pnlDiscoverNum.Visible = true;
                    txtDiscoverNum.Text = "";
                    txtDiscoverNum.Enabled = false;
                    //txtDiscoverNum.Enabled = true;
                    txtDiscoverNum.BackColor = System.Drawing.Color.DarkGray;
                    ValidateDiscoverNum.Enabled = false;
                }
            }else {
                if (lstDiscover.SelectedItem.Text.Trim() == "Yes - Existing")
                {
                    pnlDiscoverNum.Visible = true;
                    txtDiscoverNum.Enabled = true;
                    txtDiscoverNum.BackColor = System.Drawing.Color.White;
                    ValidateDiscoverNum.Enabled = true;
                    txtDiscoverNum.Text = drBankFull["PrevDiscoverNum"].ToString().Trim();
                }
                else
                {
                    //pnlDiscoverNum.Visible = false;
                    pnlDiscoverNum.Visible = true;
                    txtDiscoverNum.Text = "";
                    //txtDiscoverNum.Enabled = false;
                    txtDiscoverNum.Enabled = true;
                    txtDiscoverNum.BackColor = System.Drawing.Color.DarkGray;
                    ValidateDiscoverNum.Enabled = false;
                    //txtDiscoverNum.Text = drBankFull["PrevDiscoverNum"].ToString().Trim();
                }
            }
        }

        textChangedEventHandler(sender, e);
    }//end function discover list selection changed

    //This function handles Amex list selection changed event
    protected void lstAmex_SelectedIndexChanged(object sender, EventArgs e)
    {
         BankingInfo BankingInfoFull = new BankingInfo(AppId);
        DataSet dsBankingInfoFull = BankingInfoFull.GetBankingInfoFull();
        if (dsBankingInfoFull.Tables[0].Rows.Count > 0)
        {
            DataRow drBankFull = dsBankingInfoFull.Tables[0].Rows[0];
            if (drBankFull["PrevAmexNum"].ToString().Trim() == "")
            {
                if (lstAmex.SelectedItem.Text.Trim() == "Yes - Existing")
                {
                    pnlAmexNum.Visible = true;
                    txtAmexNum.Enabled = true;
                    txtAmexNum.BackColor = System.Drawing.Color.White;
                    ValidateAmexNum.Enabled = true;
                }
                else
                {
                    pnlAmexNum.Visible = false;
                    txtAmexNum.Text = "";
                    txtAmexNum.Enabled = false;
                    txtAmexNum.BackColor = System.Drawing.Color.DarkGray;
                    ValidateAmexNum.Enabled = false;
                }
            }
            else
            {
                if (lstAmex.SelectedItem.Text.Trim() == "Yes - Existing")
                {
                    pnlAmexNum.Visible = true;
                    txtAmexNum.Enabled = true;
                    txtAmexNum.BackColor = System.Drawing.Color.White;
                    ValidateAmexNum.Enabled = true;
                    txtAmexNum.Text = drBankFull["PrevAmexNum"].ToString().Trim();
                }
                else
                {
                    //pnlAmexNum.Visible = false;
                    pnlAmexNum.Visible = true;
                    txtAmexNum.Text = "";
                    txtAmexNum.Enabled = false;
                    txtAmexNum.BackColor = System.Drawing.Color.DarkGray;
                    ValidateAmexNum.Enabled = false;
                }
            }
        }

        textChangedEventHandler(sender, e);
    }//end function Amex list selection changed event

    //This function handles JCB list selection changed event
    protected void lstJCB_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lstJCB.SelectedItem.Text.Trim() == "Yes - Existing")
        {
            pnlJCBNum.Visible = true;
            txtJCBNum.Enabled = true;
            txtJCBNum.BackColor = System.Drawing.Color.White;
            ValidateJCBNum.Enabled = true;
        }
        else
        {
            pnlJCBNum.Visible = false;
            txtJCBNum.Text = "";
            txtJCBNum.Enabled = false;
            txtJCBNum.BackColor = System.Drawing.Color.DarkGray;
            ValidateJCBNum.Enabled = false;
        }

        textChangedEventHandler(sender, e);
    }//end function JCB list selection changed event

    //This function handles bank list selection changed event
    protected void lstBankName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lstBankName.SelectedItem.Text.Trim() == "OTHER")
        {
            txtOtherBank.Enabled = true;
            txtOtherBank.BackColor = System.Drawing.Color.White;
            ValidateOtherBank.Enabled = true;
        }
        else
        {
            txtOtherBank.Enabled = false;
            txtOtherBank.BackColor = System.Drawing.Color.DarkGray;
            txtOtherBank.Text = "";
            ValidateOtherBank.Enabled = false;
        }

        textChangedEventHandler(sender, e);
    }//end function Bank list selection changed event

    //This function displays error messages
    protected void SetErrorMessage(string errText)
    {       
        lblError.Visible = true;
        lblError.Text = errText;
    }//end function set error message

    protected void  btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            ValidateSummary.Enabled = false;

            if ((lstCountry.SelectedItem.Text == "United States") || lstCountry.SelectedItem.Text.Contains("Canada"))
            {

                string strPhone = txtPhone.Text.Trim();
                //try
                //{

                Regex regexObj = new Regex(@"[^0-9]");

                if (strPhone != "")
                {
                    strPhone = regexObj.Replace(strPhone, "").Trim();

                    strPhone = strPhone.Insert(3, "-");

                    strPhone = strPhone.Insert(7, "-");

                    txtPhone.Text = strPhone;
                }
            }
            //Validate data for commas, apostrophes
            bool retValidate = ValidateData();

            if ((retValidate)
                && (ValidateRoutingNumber() == true
                && REMonVol.IsValid == true
                && REAvgTicket.IsValid == true
                && REMaxTicket.IsValid == true))
            {
                InsertUpdateBankingInformation(false);
                InsertUpdateOtherInformation(false);
                UpdateCommonInformation(false);
                //SetErrorMessage("Information saved. You may proceed to the next page by navigating through the top menu.");

                OnlineAppProfile Profile = new OnlineAppProfile(AppId);
                int AcctType = 0;

                NewAppInfo AppInfo1 = new NewAppInfo(AppId);

                string EBTStatus = Convert.ToString(lstEBT.SelectedItem.Text.Trim());

                AppInfo1.UpdateODBEBT(onlineDebitBit, ebtBit, EBTStatus);

                DataSet ds = Profile.GetProfileData();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    AcctType = Convert.ToInt32(dr["AcctType"]);
                }

                if ((AcctType == 1) || (AcctType == 4))
                {
                    AppInfo1.UpdateUSDANum(txtUSDA.Text.Trim());

                }
            }
            else
                lblError.Visible = true;

        }//end try
        catch (Exception err)
        {
            CreateOnlineAppLog Log = new CreateOnlineAppLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + Session["AppId"].ToString() + " - " + "Merchant bankinginfo.aspx Page - " + err.Message);
            SetErrorMessage("Error Processing Request. Please contact Technical Support: <a href='mailto:support@commercetech.com?Subject=Online Application Error'> support@commercetech.com. </a>.");
        }              
    }

    protected void lstCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        if ((lstCountry.SelectedItem.Text.Trim() != "United States") && (lstCountry.SelectedItem.Text.Trim() != "Canada"))
        {
            lstState.SelectedValue = "";
            ValidateState.Enabled = false;
            lstState.Enabled = false;
        }
        else
        {
            lstState.Enabled = true;
            ValidateState.Enabled = true;
        }

        textChangedEventHandler(sender, e);
      
    }
    protected void lnkRoutingNum_Click(object sender, EventArgs e)
    {
        lnkRoutingNum.Visible = false;
        ValidateRoutingNo.Enabled = true;
        txtBankRoutingNumber.Enabled = true;
        txtBankRoutingNumber.Text = "";
        RERoutingNum.Enabled = true;
    }

    protected void lnkAccountNum_Click(object sender, EventArgs e)
    {
        lnkAccountNum.Visible = false;
        ValidateCheckingAcct.Enabled = true;
        txtAcctNumber.Enabled = true;
        txtAcctNumber.Text = "";
        REAcctNum.Enabled = true;
    }
    protected void lnkPrevDisc_Click(object sender, EventArgs e)
    {
                BankingInfo BankingInfoFull = new BankingInfo(AppId);
        DataSet dsBankingInfoFull = BankingInfoFull.GetBankingInfoFull();
        if (dsBankingInfoFull.Tables[0].Rows.Count > 0)
        {
            DataRow drBankFull = dsBankingInfoFull.Tables[0].Rows[0];

            if (drBankFull["PrevDiscoverNum"].ToString().Trim() == "")
            {
                lnkPrevDisc.Visible = false;
                ValidateDiscoverNum.Enabled = true;
                REDiscNum.Enabled = true;
                txtDiscoverNum.Enabled = true;
                txtDiscoverNum.Text = "";
            }
            else
            {
                lnkPrevDisc.Visible = false;
                ValidateDiscoverNum.Enabled = true;
                REDiscNum.Enabled = true;
                txtDiscoverNum.Enabled = true;
                txtDiscoverNum.Text = drBankFull["PrevDiscoverNum"].ToString().Trim();
            }
        }
    }
    protected void lnkPrevAmex_Click(object sender, EventArgs e)
    {         
        BankingInfo BankingInfoFull = new BankingInfo(AppId);
        DataSet dsBankingInfoFull = BankingInfoFull.GetBankingInfoFull();
        if (dsBankingInfoFull.Tables[0].Rows.Count > 0)
        {
            DataRow drBankFull = dsBankingInfoFull.Tables[0].Rows[0];
            if (drBankFull["PrevAmexNum"].ToString().Trim() == "")
            {
                lnkPrevAmex.Visible = false;
                ValidateAmexNum.Enabled = true;
                REAmexNum.Enabled = true;
                txtAmexNum.Enabled = true;
                txtAmexNum.Text = "";
            }
            else
            {
                lnkPrevAmex.Visible = false;
                ValidateAmexNum.Enabled = true;
                REAmexNum.Enabled = true;
                txtAmexNum.Enabled = true;
                txtAmexNum.Text = drBankFull["PrevAmexNum"].ToString().Trim();
            }
        }
    }


    protected void btnNavigateNext_Click(object sender, EventArgs e)
    {
        if (nextNavigatePage == "Percentage")
        {
            Response.Redirect("/Application/Merchant/cardpct.aspx", false);
        }
        else if (nextNavigatePage == "Company")
        {
            Response.Redirect("/Application/Merchant/companyinfo.aspx", false);
        }
        else if (nextNavigatePage == "Business")
        {
            Response.Redirect("/Application/Merchant/businessinfo.aspx", false);
        }
        else if (nextNavigatePage == "Principal")
        {
            Response.Redirect("/Application/Merchant/principalinfo.aspx", false);
        }
        else if (nextNavigatePage == "Banking")
        {
            Response.Redirect("/Application/Merchant/bankinginfo.aspx", false);
        }
    }


    protected void btnDoNotNavigate_Click(object sender, EventArgs e)
    {

    }

    protected bool returnComplete()
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

        bool Complete = false;
        if (busCom == 0)
        {
            Complete = false;
        }
        else
        {
            Complete = true;
        }

        return Complete;
    }

    protected void linkCardPctClick(object sender, EventArgs e)
    {
        btnSubmit_Click(sender, e);
        /*
        bool companyComplete = returnComplete();
        if (companyComplete == false)
        {
            nextNavigatePage = "Percentage";
            lblUserNavigateUnsaved.Text = "The current page is not complete. Are you sure to leave this page?";
            NavigatePopupModalExtender.Show();
        }
        else*/
        //{
            Response.Redirect("/Application/Merchant/cardpct.aspx", false);
        //}
    }

    protected void linkCompanyClick(object sender, EventArgs e)
    {
        btnSubmit_Click(sender, e);
        /*
        bool companyComplete = returnComplete();
        if (companyComplete == false)
        {
            nextNavigatePage = "Company";
            lblUserNavigateUnsaved.Text = "The current page is not complete. Are you sure to leave this page?";
            NavigatePopupModalExtender.Show();
        }
        else*/
        //{
            Response.Redirect("/Application/Merchant/companyinfo.aspx", false);
        //}
    }

    protected void linkBusinessClick(object sender, EventArgs e)
    {
        btnSubmit_Click(sender, e);
        /*
        bool companyComplete = returnComplete();
        if (companyComplete == false)
        {
            nextNavigatePage = "Business";
            lblUserNavigateUnsaved.Text = "The current page is not complete. Are you sure to leave this page?";
            NavigatePopupModalExtender.Show();
        }
        else*/
        //{
            Response.Redirect("/Application/Merchant/businessinfo.aspx", false);
        //}
    }

    protected void linkPrincipalClick(object sender, EventArgs e)
    {
        btnSubmit_Click(sender, e);
        /*
        bool companyComplete = returnComplete();
        if (companyComplete == false)
        {
            nextNavigatePage = "Principal";
            lblUserNavigateUnsaved.Text = "The current page is not complete. Are you sure to leave this page?";
            NavigatePopupModalExtender.Show();
        }
        else*/
        //{
            Response.Redirect("/Application/Merchant/principalinfo.aspx", false);
        //}
    }

    protected void linkBankingClick(object sender, EventArgs e)
    {
       //btnSubmit_Click(sender, e);
        /*
        bool companyComplete = returnComplete();
        if (companyComplete == false)
        {
            nextNavigatePage = "Banking";
            lblUserNavigateUnsaved.Text = "The current page is not complete. Are you sure to leave this page?";
            NavigatePopupModalExtender.Show();
        }
        else*/
        //{
            //Response.Redirect("/Application/Merchant/bankinginfo.aspx", false);
        //}
    }

    protected void linkPDFClick(object sender, EventArgs e)
    {
        btnSubmit_Click(sender, e);
        /*
        bool companyComplete = returnComplete();
        if (companyComplete == false)
        {
            nextNavigatePage = "Banking";
            lblUserNavigateUnsaved.Text = "The current page is not complete. Are you sure to leave this page?";
            NavigatePopupModalExtender.Show();
        }
        else*/
        //{
        Response.Redirect("/Application/notice.aspx", false);
        //}
    }
}
