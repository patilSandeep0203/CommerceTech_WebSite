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
//using System.Web.Mail;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Text;
using System.IO;
using System.Net.Mail;
using System.Text.RegularExpressions;


public partial class register : System.Web.UI.Page
{
    private static string strReplace = "";
    private static string strMerchantGateway = "";
    private static int AppId = 0;
    private static int Referral;
    private static int PID = 0;
    private static int AcctType = 0;
    //private static string Country = "";
    private static string strSW = string.Empty;
    private static string Serv = "";
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
        pnlRegistrationInfo.Visible = true;
        pnlSameLoginID.Visible = false;
        //pnlRDB.Visible = true;

        Style headerLabel = new Style();
        headerLabel.ForeColor = System.Drawing.Color.White;
        headerLabel.Font.Size = 9;

        lblRegistrationHeader.ApplyStyle(headerLabel);

        if ((Request.Params.Get("SW") != null))
        {
            strSW = Request.Params.Get("SW");
        }

        if ((Request.Params.Get("Serv") != null))
        {
            Serv = Request.Params.Get("Serv");
        }

        //Serv = "";

        string Country = string.Empty;
        if ((Request.Params.Get("Country") != null))
            Country = Request.Params.Get("Country");
        if (Country == "B S")
            Country = "B+S";

        

        //if (!IsPostBack)
        {
            try
            {
                //btnContinue.Attributes.Add("onclick", "this.disabled='true';");
                //btnContinue.Attributes.Add("onload", "this.disabled='false';");

                btnContinue.Attributes.Add("onclick", " this.disabled = true; " + ClientScript.GetPostBackEventReference(btnContinue, null) + ";");

                AcctType = 0;
                strMerchantGateway = "";

                if ((Serv.Contains("MerchantAccount")) && (Serv.Contains("Gateway")))
                {
                    AcctType = 4;
                    strMerchantGateway = "MerchantAndGateway";
                    rdbBoth.Checked = true;
                }

                if ((Serv.Contains("MerchantAccount")) && (!Serv.Contains("Gateway")))
                {
                    AcctType = 1;
                    strMerchantGateway = "Merchant";
                    rdbMerchant.Checked = true;
                }

                if ((!Serv.Contains("MerchantAccount")) && (Serv.Contains("Gateway")))
                {
                    AcctType = 2;
                    strMerchantGateway = "Gateway";
                    rdbGateway.Checked = true;
                }

                if ((!Serv.Contains("MerchantAccount")) && (!Serv.Contains("Gateway")))
                {
                    AcctType = 3;
                    strMerchantGateway = "Equipment";
                    rdbEquipment.Checked = true;
                }

                //A QuickBooks account comes with an IPS Merchant Account. So select Merchant and Gateway and disable other options
                if (strSW == "QB")
                {
                    strMerchantGateway = "MerchantAndGateway";
                    AcctType = 4;
                    rdbBoth.Checked = true;
                    rdbEquipment.Enabled = false;
                    rdbBoth.Enabled = false;
                    rdbGateway.Enabled = false;
                    rdbMerchant.Enabled = false;
                }

                if (strSW == "GOPAY")
                {
                    strMerchantGateway = "MerchantAndGateway";
                    AcctType = 4;
                    rdbBoth.Checked = true;
                    rdbEquipment.Enabled = false;
                    rdbBoth.Enabled = false;
                    rdbGateway.Enabled = false;
                    rdbMerchant.Enabled = false;
                }
                if (strSW == "QBPOS")
                {
                    strMerchantGateway = "Merchant";
                    AcctType = 1;
                    rdbMerchant.Checked = true;
                    rdbEquipment.Enabled = false;
                    rdbBoth.Enabled = false;
                    rdbGateway.Enabled = false;
                    rdbMerchant.Enabled = false;
                }

                if (strSW == "POS")
                {
                    strMerchantGateway = "Merchant";
                    AcctType = 1;
                    rdbMerchant.Checked = true;
                    rdbEquipment.Enabled = false;
                    rdbBoth.Enabled = false;
                    rdbGateway.Enabled = false;
                    rdbMerchant.Enabled = false;
                }

                if (strSW == "SageYes")
                {
                    strMerchantGateway = "MerchantAndGateway";
                    AcctType = 4;
                    rdbBoth.Checked = true;
                    rdbEquipment.Enabled = false;
                    rdbBoth.Enabled = false;
                    rdbGateway.Enabled = false;
                    rdbMerchant.Enabled = false;
                }

                if ((Country == "WorldPay") || (Country == "Kitts") || (Country == "CalApp") || 
                    (Country == "BarclaysApp") || (Country == "Canada") || (Country == "B+S"))
                {
                    strMerchantGateway = "MerchantAndGateway";
                    AcctType = 4;
                    rdbBoth.Checked = true;
                    rdbEquipment.Enabled = false;
                    rdbBoth.Enabled = false;
                    rdbGateway.Enabled = false;
                    rdbMerchant.Enabled = false;
                }

                /*if ((Request.Params.Get("Country") != null))
                    Session["Country"] = Convert.ToString(Request.Params.Get("Country"));*/


                //set up the rates for the online app
                if (strSW == "")
                    PID = 0;
                if (strSW == "QB")
                    //PID = 255;//Apply QuickBooks rates
                    PID = 201;//Apply Sage rates
                if (strSW == "GOPAY")
                    PID = 253;//Apply QuickBooks rates
                if (strSW == "QBPOS")
                    PID = 254;//Apply QuickBooks POS rates
                if (strSW == "POS")
                    //PID = 191;//Apply iPayment default rates
                    PID = 203;//Apply Sage default rates
                if (strSW == "SageYes")
                    PID = 203;//Apply Sage default rates
                if (strSW == "QBNo")
                    PID = 203;//Apply Sage default rates unless the merchants select QB integration


                //Check if cookie already exists
                HttpCookie cookie = Request.Cookies["OnlineAppInfo"];
                if (cookie != null)
                {
                    //If cookie exists, then get information from cookie and assign to session variables                
                    //Session["RepNum"] = cookie["RepNum"].ToString();
                    Session["Referral"] = cookie["ReferralID"].ToString();
                    Referral = Convert.ToInt32(cookie["ReferralID"].ToString());
                    if (Convert.ToInt32(cookie["PID"]) == 0)
                    {
                        //If there is no PID in the URL, check if there is a ReferralID and query the Affliates
                        //table to get the PID for the specified ReferralID
                        if (Referral != 0)
                        {
                            PackageInfo Package = new PackageInfo();
                            PID = Package.ReturnPID(Convert.ToInt32(cookie["ReferralID"]));
                            Session["PID"] = PID;
                        }
                    }
                    else
                    {
                        Session["PID"] = Convert.ToInt32(cookie["PID"]);
                        PID = Convert.ToInt32(cookie["PID"]);
                    }
                }//end if cookie not null
                else
                {
                    //Get parameters from the URL if any and set session variables                
                    if (Request.Params.Get("Referral") != null)
                        Session["Referral"] = Request.Params.Get("Referral");

                    if (Request.Params["PID"] != null)
                    {
                        Session["PID"] = Request.Params.Get("PID");
                        PID = Convert.ToInt32(cookie["PID"]);
                    }
                    else
                    {
                        //If there is no PID in the URL, check if there is a ReferralID and query the Affliates
                        //table to get the PID for the specified ReferralID
                        if (Request.Params.Get("Referral") != null)
                        {
                            Session["Referral"] = Request.Params.Get("Referral");
                            //Get PID based on Referral
                            PackageInfo Pack = new PackageInfo();
                            PID = Pack.ReturnPID(Convert.ToInt32(Request.Params.Get("Referral")));
                            Session["PID"] = PID;
                        }
                    }//end else pid not null
                }//end else cookie is null

                if (Country == "WorldPay")
                    PID = 178;//Apply WorldPay rates
                if (Country == "Canada")
                    PID = 190; //Apply Canada App Rates
                if (Country == "CalApp")
                    PID = 182; //Apply Cal App Rates
                if (Country == "BarclaysApp")
                    PID = 243; //Apply Barclays App Rates
                if (Country == "B+S")
                    PID = 247; //Apply B+S App Rates
                if (Country == "Kitts")
                    PID = 196; //Apply St. Kitts Rates

                Session["PID"] = PID;

                PopulatePage();

                Referral = Convert.ToInt32(Session["Referral"]);
                //If Referral Source exists, autoselect the referral from dropdown and lock it                
                if (Referral != 0) //&& (Country != "Canada"))
                {
                    //lstReferral.SelectedIndex = Referral;
                    lstReferral.SelectedIndex = lstReferral.Items.IndexOf(lstReferral.Items.FindByValue(Convert.ToString(Referral)));
                    lstReferral.Enabled = false;
                    txtOtherReferral.Enabled = false;
                    ValidateOtherReferral.Enabled = false;
                }
                else
                    //No Referral Source, populate it with OTHER
                    lstReferral.SelectedIndex = lstReferral.Items.IndexOf(lstReferral.Items.FindByText("OTHER"));

                PID = Convert.ToInt32(Session["PID"]);

                //Country = Convert.ToString(Session["Country"]);

                txtFirstName.Focus();
            }//end try
            catch (Exception err)
            {
                CreateOnlineAppLog Log = new CreateOnlineAppLog();
                Log.ErrorLog(Server.MapPath("~/ErrorLog"), "Registration Page Load Error - " + err.Message);
                SetErrorMessage("Error Processing Request. Please contact Technical Support: <a href=mailto:support@commercetech.com?Subject=Online App Error, App ID: " + Session["AppId"].ToString() + "> support@commercetech.com. </a>");
            }
        }//end if ispostback
    }//end page load
    
    //This function populates the page with data from the database
    protected void PopulatePage()
    {
        //Get affiliates
        CommonListData Affiliates = new CommonListData();
        DataSet dsAffiliates = Affiliates.GetReferralList();
        if (dsAffiliates.Tables[0].Rows.Count > 0)
        {
            lstReferral.DataSource = dsAffiliates.Tables[0];
            lstReferral.DataTextField = "DBA";
            lstReferral.DataValueField = "AffiliateID";
            lstReferral.DataBind();
        }//end if count not 0

        System.Web.UI.WebControls.ListItem lstItem = new System.Web.UI.WebControls.ListItem();
        lstItem.Text = "OTHER";
        lstItem.Value = "0";
        lstReferral.Items.Add(lstItem);
        //lstReferral.SelectedIndex = lstReferral.Items.IndexOf(lstReferral.Items.FindByText("OTHER"));
    }//end function populate page

    //This function referral list selection changed event
    protected void lstReferral_SelectedIndexChanged(object sender, EventArgs e)
    {
        if ((lstReferral.SelectedItem.Text.Trim() == "Other") || (lstReferral.SelectedItem.Text.Trim() == "OTHER"))
        {
            txtOtherReferral.BackColor = System.Drawing.Color.White;
            txtOtherReferral.Enabled = true;
            ValidateOtherReferral.Enabled = true; 
        }
        else
        {
            txtOtherReferral.BackColor = System.Drawing.Color.DarkGray;
            txtOtherReferral.Text = "";
            txtOtherReferral.Enabled = false;
            ValidateOtherReferral.Enabled = false;
        }
    }//end function referral list selection changed

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

    //This function handles checking and inserting data in tables on submit button click event
    protected void btnContinue_Click(object sender, EventArgs e)
    {

        ValidateSummary.Enabled = false;
        RegularExpressionValidatorPhone.Enabled = false;
        //RegularExpressionValidator1.Enabled = false;
        RegularExpressionValidatorMobile.Enabled = false;

        string strPhoneTxt = txtPhone.Text.Trim();
        string strHomePhoneTxt = txtHomePhone.Text.Trim();
        string strMobilePhoneTxt = txtMobilePhone.Text.Trim();
        //try
        //{
        Regex regexObj = new Regex(@"[^0-9]");

        if (strPhoneTxt != "")
        {
            strPhoneTxt = regexObj.Replace(strPhoneTxt, "").Trim();

            strPhoneTxt = strPhoneTxt.Insert(3, "-");

            strPhoneTxt = strPhoneTxt.Insert(7, "-");

            txtPhone.Text = strPhoneTxt;
        }

        if (strHomePhoneTxt != "")
        {

            strHomePhoneTxt = regexObj.Replace(strHomePhoneTxt, "").Trim();

            strHomePhoneTxt = strHomePhoneTxt.Insert(3, "-");

            strHomePhoneTxt = strHomePhoneTxt.Insert(7, "-");

            txtHomePhone.Text = strHomePhoneTxt;
        }

        if (strMobilePhoneTxt != "")
        {
            strMobilePhoneTxt = regexObj.Replace(strMobilePhoneTxt, "").Trim();

            strMobilePhoneTxt = strMobilePhoneTxt.Insert(3, "-");

            strMobilePhoneTxt = strMobilePhoneTxt.Insert(7, "-");

            txtMobilePhone.Text = strMobilePhoneTxt;
        }

        try
        {
            if (Page.IsValid)
            {
                //Validate data
                bool retValidate = ValidateData();
                if (retValidate)
                {
                    if (strMerchantGateway == "")
                    {
                        lblError.Visible = true;
                        lblError.Text = "Please select signup type.";
                    }
                    else
                    {
                        //Check if LoginName already exists and display message if exists
                        NewAppInfo App = new NewAppInfo(AppId);
                        bool bRetVal = App.CheckLoginNameExists(txtLoginName.Text.Trim());
                        if (bRetVal)
                        {
                            //This email address already exists
                            //SetErrorMessage("The Login Name you chose already exists. Please sign up using a different Login Name");
                            //Response.Redirect("register2.aspx", false);
                            pnlRegistrationInfo.Visible = false;
                            pnlSameLoginID.Visible = true;
                            //pnlRDB.Visible = false;
                        }
                        else
                        {
                            //Check if country is canada or st.kitts
                            string Country = string.Empty;
                            if ((Request.Params.Get("Country") != null))
                                Country = Request.Params.Get("Country");
                            else
                                Country = "CTC";

                            if (Country == "B S")
                                Country = "B+S";

                            //bool QuickBooks = false;

                            if (strSW == "QB")
                            {
                                //QuickBooks = true;
                                Country = "QuickBooks";
                            }

                            //Insert information in the NewApp table
                            bool OnlineDebit = false;
                            bool EBT = false;
                            bool Wireless = false;
                            bool CheckServices = false;
                            bool GiftCard = false;
                            bool MerchantFunding = false;
                            bool Payroll = false;
                            bool Lease = false;

                            if (Serv.Contains("EBT"))
                            {
                                EBT = true;
                            }

                            if (Serv.Contains("OnlineDebit"))
                            {
                                OnlineDebit = true;
                            }

                            if ((Country == "WorldPay") || (Country == "Canada") || (Country == "CalApp") || (Country == "BarclaysApp") || (Country == "B+S") || (Country == "Kitts"))
                            {
                                OnlineDebit = false;
                                EBT = false;
                            } 
                            
                            if ((strSW == "QB")||(strSW == "GOPAY"))
                            {
                                OnlineDebit = false;
                            }


                            if (Serv.Contains("Wireless"))
                                Wireless = true;
                            if (Serv.Contains("CheckServices"))
                                CheckServices = true;//  chkCheckGuarantee.Checked
                            if (Serv.Contains("GiftCard"))
                                GiftCard = true;
                            if (Serv.Contains("AMI") || Serv.Contains("BFS") || Serv.Contains("RapidAdvance"))
                                MerchantFunding = true;
                            if (Serv.Contains("Payroll"))
                                Payroll = true;
                            if (Serv.Contains("Lease"))
                                Lease = true;

                            //Set Merchant Cash Advance Type
                            string MCAType = "";

                                if (Serv.Contains("BFS"))
                                    MCAType = "Business Financial Services";
                                else if (Serv.Contains("AMI"))
                                    MCAType = "AdvanceMe, Inc.";
                                else if (Serv.Contains("RapidAdvance"))
                                    MCAType = "RapidAdvance";


                            NewAppInfo NewApp = new NewAppInfo(txtLoginName.Text.Trim(),
                                PID, Convert.ToInt32(lstReferral.SelectedItem.Value), txtOtherReferral.Text, AcctType,
                                OnlineDebit, CheckServices, GiftCard, EBT, Wireless, MerchantFunding, Payroll, Lease, Country);
                            AppId = NewApp.ReturnAppID();
                                                        
                            if (AppId != 0)
                            {                                
                                string salt = GetSalt();
                                string hash = GenerateHash(salt);

                                //Update password in OnlineAppAccess
                                OnlineAppAccess Access = new OnlineAppAccess(AppId);
                                bool retVal = Access.InsertUpdatePassword(txtLoginName.Text.Trim(), Server.HtmlEncode(txtPassword.Text.Trim().Replace("'", strReplace)), hash, salt);

                                //Return the AppId of the new application so that the session for the new user can be set            
                                //AppId = InsertApp.ReturnAppId(txtLoginName.Text.Trim());
                            
                                //Insert profile information in OnlineappProfile
                                OnlineAppProfile Profile = new OnlineAppProfile(AppId);
                                retVal = Profile.IUProfile(
                                    Server.HtmlEncode(txtFirstName.Text.Trim().Replace("'", strReplace)),
                                    Server.HtmlEncode(txtLastName.Text.Trim().Replace("'", strReplace)),
                                    txtEmail.Text.Trim().Replace("'", strReplace),
                                    Server.HtmlEncode(txtTitle.Text.Trim().Replace("'", strReplace)),
                                    Server.HtmlEncode(txtPhone.Text.Trim()),
                                    Server.HtmlEncode(txtPhoneExt.Text.Trim()),
                                    Server.HtmlEncode(txtHomePhone.Text.Trim()),
                                    Server.HtmlEncode(txtMobilePhone.Text.Trim()),
                                    AcctType);

                                /*if (strSW == "QB" && Serv.Contains("Gateway/"))
                                {
                                    OnlineAppAccess SalesOpp = new OnlineAppAccess(AppId);
                                    retVal = SalesOpp.InsertQBOpp();
                                }*/
                                                                
                                //Create cookie with user info
                                //CreateCookie();
                                FormsAuthentication.SetAuthCookie(txtLoginName.Text.Trim(), false);
                                //set session variables
                                Session["AppId"] = AppId;
                                Session["UserLoggedIn"] = "Yes";
                                Session["Access"] = "Merchant";
                                Session["AcctType"] = AcctType;
                                Session["Locked"] = "No";

                                //Insert AppId in processing and gateway based on acctType
                                InsertProcessing();

                                
                                if ((OnlineDebit == true) || (EBT == true))
                                {
                                    if (strSW == "QB")
                                        PID = 265;//Apply Sage rates
                                    if (strSW == "GOPAY")
                                        PID = 253;//Apply QuickBooks rates
                                    if (strSW == "QBPOS")
                                        PID = 254;//Apply QuickBooks POS rates
                                    if (strSW == "POS")
                                        PID = 204;//Apply Sage default rates
                                    if (strSW == "SageYes")
                                        PID = 204;//Apply Sage default rates
                                    if (strSW == "QBNo")
                                    {
                                        if (!(Serv.Contains("ROAMpayGateway")))
                                        {
                                            PID = 204;
                                        }//Apply Sage default rates
                                        else {
                                            PID = 266;
                                        }//Apply RoamPay default rates
                                    }
                                }
                                else
                                {
                                    if (strSW == "QB")
                                        PID = 201;//Apply QuickBooks rates
                                    if (strSW == "GOPAY")
                                        PID = 253;//Apply QuickBooks rates
                                    if (strSW == "QBPOS")
                                        PID = 254;//Apply QuickBooks POS rates
                                    if (strSW == "POS")
                                        //PID = 191;//Apply iPayment default rates
                                        PID = 203;//Apply Sage default rates
                                    if (strSW == "SageYes")
                                        PID = 203;//Apply Sage default rates
                                    if (strSW == "QBNo")
                                    {
                                        if (!(Serv.Contains("ROAMpayGateway")))
                                        {
                                            PID = 203;
                                        }//Apply Sage default rates
                                        else
                                        {
                                            PID = 266;
                                        }//Apply RoamPay default rates
                                    }
                                        //PID = 203;//Apply Sage default rates
                                }

                                //In this senario no merchant account is selected
                                if (strSW == "")
                                    PID = 0; //no merchant account
                               
                                if (Country == "WorldPay")
                                    PID = 178;//Apply WorldPay rates
                                if (Country == "Canada")
                                    PID = 190; //Apply Canada App Rates
                                if (Country == "CalApp")
                                    PID = 182; //Apply Cal App Rates
                                if (Country == "BarclaysApp")
                                    PID = 243; //Apply Barclays App Rates
                                if (Country == "B+S")
                                    PID = 247; //Apply B+S App Rates
                                if (Country == "Kitts")
                                    PID = 196; //Apply St. Kitts Rates

                                if (Referral != 11 && Convert.ToString(Referral) != "")
                                {
                                    GetPackageInfo();
                                }
                                Session["PID"] = PID;

                                //PID = Convert.ToInt32(Session["PID"]);
                                UpdateProcessing();

                                if (Serv.Contains("Gateway/"))
                                    UpdateGatewayRates();
                                
                                /*if (strSW == "QB")
                                {
                                    //Insert merchant account sales opp
                                    SalesOppsBL SalesOppMerchantNum = new SalesOppsBL(AppId);
                                    bool retVal2 = SalesOppMerchantNum.CreateSalesOpps();
                                }*/

                                if (AcctType == 1)
                                {
                                    SalesOppsBL SalesOppMerchantNum = new SalesOppsBL(AppId);
                                    bool retVal2 = SalesOppMerchantNum.CreateSalesOpps();
                                }


                                if (AcctType == 4)
                                {
                                    SalesOppsBL SalesOppMerchantAndGW = new SalesOppsBL(AppId);
                                    SalesOppMerchantAndGW.CreateSalesOpps();
                                    SalesOppMerchantAndGW.CreateSalesOppsGW();
                                }

                                //Insert Gateway Sales opp if account type is gateway only
                                if (AcctType == 2)
                                {
                                    SalesOppsBL SalesOppGW = new SalesOppsBL(AppId);
                                    SalesOppGW.CreateSalesOppsGW();
                                }

                                //Update Additional Services Types
                                NewAppInfo AddlServ = new NewAppInfo(AppId);

                                if (CheckServices)
                                    AddlServ.UpdateCheckService("default");

                                if (GiftCard)
                                    AddlServ.UpdateGiftCardType("default");

                                if (Payroll)
                                    AddlServ.UpdatePayrollType("default");

                                if (MerchantFunding)
                                    AddlServ.UpdateMCAType(MCAType);

                                if (Lease)
                                    AddlServ.InsertUpdateLeaseInfo("default", "", "");

                                //Insert Additional Services Sales Opps
                                SalesOppsBL Sales = new SalesOppsBL(AppId);
                                //Sales.CreateAddlServSalesOpps(OnlineDebit, GiftCard, EBT, Payroll);

                                //Insert EBT Sales Opps

                                Sales.CreateEBTSalesOpps(EBT);

                                //Insert Online Debit Sales Opps

                                Sales.CreateOnlineDebitSalesOpps(OnlineDebit);

                                //Insert Payroll Sales Opps

                                Sales.CreatePayrollSalesOpps(Payroll, "Intuit QuickBooks Payroll Assisted");

                                //Insert GiftCard Sales Opps

                                Sales.CreateGiftCardSalesOpps(GiftCard, "default");

                                //Insert Check Services Sales Opps
                                Sales.CreateCheckServicesSalesOpps(CheckServices, "default");

                                //Insert Merchant Cash Advance Sales Opps
                                Sales.CreateMCASalesOpps(MerchantFunding, MCAType);

                                // send out emails to notify about the new online application.
                                SendEmail(AppId);

                                //Send out email to customer
                                SendEmailCust(AppId);

                                //Open page link to BFS website
                                if (Serv.Contains("MerchantFunding"))
                                {
                                    string url = "https://www.businessfinancialservices.com/applyaff.cfm?trackid=2087";
                                    string fullURL = "window.open('" + url + "', '_blank')"; 
                                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
                                    //string sTargetURL = "https://www.businessfinancialservices.com/applyaff.cfm?trackid=2087"; 
                                    //Response.Write("<script>open('" + sTargetURL + "');</script>");
                                }

                                //if (strSW == "QB")
                                    //Response.Redirect("AddlServices.aspx?SW1" + strSW, false);
       
                          
                                else if (Country == "WorldPay")
                                {
                                    //Insert WorldPay Sales Opp
                                    OnlineAppAccess SalesOpp = new OnlineAppAccess(AppId);
                                    retVal = SalesOpp.InsertWPOpp();
                                    SalesOppsBL SalesOppMerchantNum = new SalesOppsBL(AppId);
                                    bool retVal2 = SalesOppMerchantNum.CreateSalesOpps();
                                    //Response.Redirect("https://secure.worldpay.com/app/application.pl?brand=ctc&Scheme=1", false);
                                    Response.Redirect("https://secure.worldpay.com/app/application.pl?brand=ctc&amp;scheme=2", false);
                                }
                                //else if (Country == "CalApp")
                                //    CreateCalPDF();
                                //else if (Country == "Kitts")
                                //{
                                //    pnlLink.Visible = true;
                                //    lnkDownloadApp.NavigateUrl = "https://www.apps.commercetech.com/Application/St  Kitts MOTO_app.doc";
                                //    lnkDownloadApp.Target = "_blank";
                                //}
                                else if ((AcctType == 1) || (AcctType == 4))
                                    Response.Redirect("Merchant/cardpct.aspx", false);
                                else if (AcctType == 2)
                                    Response.Redirect("Gateway/companyinfo.aspx", false);
                                else if (AcctType == 3)
                                    Response.Redirect("AddlServices.aspx", false);

                                
                            }//end if AppId not 0
                            else
                            {
                                SetErrorMessage("Registration Error. Please contact Technical Support: <a href='mailto:support@commercetech.com?Subject=Online Application Error'> support@commercetech.com. </a>.");
                            }                            
                        }//end if newapptable count not 0               
                    }//end else strMerchantGateway not null
                }//end if bool
            }//end if page valid
        }//end try
        catch (Exception err)
        {
            CreateOnlineAppLog Log = new CreateOnlineAppLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "Registration Error - " + err.Message);
            SetErrorMessage("Registration Error. Please contact Technical Support: <a href='mailto:support@commercetech.com?Subject=Online Application Error'> support@commercetech.com. </a>.");
        }
    }//end function btn click

    protected void NewLogin_Click(object sender, EventArgs e)
    {
        txtLoginName.Text = "";
        pnlRegistrationInfo.Visible = true;
        pnlSameLoginID.Visible = false;
        //pnlRDB.Visible = true;
    }

    //This function inserts AppId in processing and gateway based on AcctType
    public void InsertProcessing()
    {
        if (AcctType == 1)
        {
            //Insert appid in processing table for the merchant account
            ProcessingInfo Processing = new ProcessingInfo(AppId);
            bool retVal = Processing.UpdateProcessingInfo("", "", "", "",
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "","", "","", "","", "");   
        }
        else if (AcctType == 2)
        {
            //Insert appid in gateway table for the gateway account
            Gateway GatewayInfo = new Gateway(AppId);
            bool retVal = GatewayInfo.UpdateGatewayInfo( "", "", "", "");
        }
        else if (AcctType == 4)
        {
            //Insert appid in processing table for the merchant account
            ProcessingInfo Processing = new ProcessingInfo(AppId);
            bool retVal = Processing.UpdateProcessingInfo("", "", "", "",
                "", "", "", "", "",  "", "", "", "",  "", "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "","", "","","","","");

            //Insert appid in gateway table for the gateway account
            Gateway GatewayInfo = new Gateway(AppId);
            retVal = GatewayInfo.UpdateGatewayInfo( "", "", "", "");
        }
    }

    //This function creates a form authentication cookie
    public void CreateCookie()
    {
        FormsAuthenticationTicket AuthTicket;
        string strcookie;
        HttpCookie ck;
        AuthTicket = new FormsAuthenticationTicket(1, txtEmail.Text.Trim(), DateTime.Now, DateTime.Now.AddMinutes(60), true, "Session in progress");
        strcookie = FormsAuthentication.Encrypt(AuthTicket);
        ck = new HttpCookie(FormsAuthentication.FormsCookieName, strcookie);
        ck.Expires = AuthTicket.Expiration;
        ck.Path = FormsAuthentication.FormsCookiePath;
        Response.Cookies.Add(ck);
    }//end function create cookie

    //This function emails Agents
    public void SendEmail(int AppId)
    {
        try
        {
            //Send Email to agent after successful registration before redirecting user            

            string strSubject = "Commerce Technologies - New Online Application";
            MailMessage msg = new MailMessage();
            //msg.To = "sales@commercetech.com";
            msg.From = new MailAddress("sales@commercetech.com");
            //msg.From = "sales@commercetech.com";
            msg.Subject = strSubject;

            //based on the rep category send email to applications@commercetech.com or sales@commercetech.com
            NewAppInfo RepNum = new NewAppInfo(AppId);
            DataSet ds = RepNum.GetNewAppData();
            DataRow dr = ds.Tables[0].Rows[0];
            int MasterRepNum = Convert.ToInt32(dr["RepNum"]);

            CommonListData Summary = new CommonListData();
            DataSet dsRepCat = Summary.GetRepInfo(MasterRepNum);
            DataRow drRepCat = dsRepCat.Tables[0].Rows[0];
            if (ds.Tables[0].Rows.Count > 0)
            {
                string RepCategory = drRepCat["RepCat"].ToString().Trim();
                if ((RepCategory == "A") || (RepCategory == "R"))
                {
                    //msg.To = "applications@commercetech.com; sales@commercetech.com";
                    msg.To.Add(new MailAddress("sales@commercetech.com"));
                    msg.To.Add(new MailAddress("applications@commercetech.com"));

                }
                else
                {
                    //msg.To = "sales@commercetech.com";
                    msg.To.Add(new MailAddress("sales@commercetech.com"));
                    msg.To.Add(new MailAddress("applications@commercetech.com"));
                }
            }

            msg.Body = GetBody(txtFirstName.Text.Trim(), txtLastName.Text.Trim(), txtEmail.Text.Trim(), txtPhone.Text.Trim(), txtPhoneExt.Text.Trim());


            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage("sales@commercetech.com", "sales@commercetech.com", "New Online Application", msg.Body);
            //pasing the Gmail credentials to send the email


            System.Net.NetworkCredential mailAuthenticaion = new System.Net.NetworkCredential("sales@commercetech.com", "1Success11!");

            System.Net.Mail.SmtpClient mSmtpClient = new System.Net.Mail.SmtpClient("192.168.1.136", 25);

            mSmtpClient.EnableSsl = false;
            mSmtpClient.UseDefaultCredentials = false;
            mSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            //mSmtpClient.Credentials = mailAuthenticaion;
            msg.IsBodyHtml = true;
            //mSmtpClient.Credentials = new System.Net.NetworkCredential("sales@commercetech.com", "1Success11!");
            mSmtpClient.Send(msg);

        }
        catch (Exception err)
        {
            CreateOnlineAppLog Log = new CreateOnlineAppLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "Registration Email Error - " + err.Message);
            SetErrorMessage("Error Processing Request. Please contact Technical Support: <a href=mailto:support@commercetech.com?Subject=Online App Error, App ID: " + Session["AppId"].ToString() + "> support@commercetech.com. </a>");
        }
    }//end function send email

    //This function emails Agents
    public void SendEmailCust(int AppId)
    {
        try
        {
            string strSubject = "Commerce Technologies - Your Application has been submitted!";
            MailMessage msg = new MailMessage();
            msg.Subject = strSubject;
            //based on the rep category send email to applications@commercetech.com or sales@commercetech.com
            NewAppInfo RepNum = new NewAppInfo(AppId);
            DataSet ds = RepNum.GetNewAppData();
            DataRow dr = ds.Tables[0].Rows[0];
            int MasterRepNum = Convert.ToInt32(dr["RepNum"]);
            int AcctType = Convert.ToInt32(dr["AcctType"]);
            string AccountType = "";
            if (AcctType == 1)
            {
                AccountType = "Merchant";
            }
            else if (AcctType == 4)
            {
                AccountType = "Merchant & Gateway";
            }
            else if (AcctType == 2)
            {
                AccountType = "Gateway";
            }
            else
            {
                AccountType = "Equipment";
            }

            CommonListData Summary = new CommonListData();
            DataSet dsRepCat = Summary.GetRepInfo(MasterRepNum);
            DataRow drRepCat = dsRepCat.Tables[0].Rows[0];
            string RepName = drRepCat["RepName"].ToString().Trim();
            string repEmail = drRepCat["Email"].ToString().Trim();
            if (ds.Tables[0].Rows.Count > 0)
            {
                msg.To.Add(new MailAddress(txtEmail.Text.Trim()));
                string RepCategory = drRepCat["RepCat"].ToString().Trim();
                if ((RepCategory == "A") || (RepCategory == "R"))
                {
                    //msg.To = "applications@commercetech.com; sales@commercetech.com";
                    msg.To.Add(new MailAddress("sales@commercetech.com"));
                    //msg.To.Add(new MailAddress("twu@commercetech.com"));
                    msg.To.Add(new MailAddress("applications@commercetech.com"));
                }
                else
                {
                    //msg.To = "sales@commercetech.com";
                    msg.To.Add(new MailAddress("sales@commercetech.com"));
                    //msg.To.Add(new MailAddress("twu@commercetech.com"));
                }
            }

            msg.Body = GetBodyCust(txtFirstName.Text.Trim(), txtLastName.Text.Trim(), AppId, AccountType, RepName);

            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage(repEmail, txtEmail.Text.Trim(), "Commerce Technologies - New Online Application", msg.Body);
            //pasing the Gmail credentials to send the email


            System.Net.NetworkCredential mailAuthenticaion = new System.Net.NetworkCredential("sales@commercetech.com", "1Success11!");

            System.Net.Mail.SmtpClient mSmtpClient = new System.Net.Mail.SmtpClient("192.168.1.136", 25);

            mSmtpClient.EnableSsl = false;
            mSmtpClient.UseDefaultCredentials = false;
            mSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            //mSmtpClient.Credentials = mailAuthenticaion;
            msg.IsBodyHtml = true;
            mSmtpClient.Send(msg);
            msg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            //SmtpMail.Send(msg);
            //mSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

        }
        catch (Exception err)
        {
            CreateOnlineAppLog Log = new CreateOnlineAppLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "Registration Email Error - " + err.Message);
            SetErrorMessage("Error Processing Request. Please contact Technical Support: <a href=mailto:support@commercetech.com?Subject=Online App Error, App ID: " + Session["AppId"].ToString() + "> support@commercetech.com. </a>");
        }
    }//end function send email

    //This function gets package info based on PID
    public void GetPackageInfo()
    {
        //Get package information from PackageInfo based on PID
        PackageInfo Package = new PackageInfo();
        //If PID = 0 and if Rep does not have a default CNP package then apply 191 (iPay CNP)        
        PackageInfo Pack = new PackageInfo();

        int newPID1 = Pack.ReturnPID(Referral);
        if (newPID1 == 0)
            PID = 191;
        else
            PID = newPID1;
        Session["PID"] = PID;

        if (PID == 0)
        {
            int newPID = Pack.ReturnPID(Referral);
            if (newPID == 0)
                PID = 191;
            else
                PID = newPID;

            Session["PID"] = PID;
        }


    }//end function get package info

    //This function inserts rates in processing info table
    public void UpdateProcessing()
    {
        NewAppInfo NewPID = new NewAppInfo(AppId);
        NewPID.UpdatePID(PID);
        PackageInfo Package = new PackageInfo();
        DataSet dsPackageInfo = Package.GetPackageInfo(PID);
        DataTable dtPackageInfo = dsPackageInfo.Tables[0];

        string OnlineDebitMonthFee = "";
        string OnlineDebitTransFee = "";
        string EBTMonthFee = "";
        string EBTTransFee = "";



        if (dtPackageInfo.Rows.Count > 0)
        {
            DataRow drPackageInfo = dtPackageInfo.Rows[0];
            if ((AcctType == 1) || (AcctType == 2) || (AcctType == 4))
            {
                if (Serv.Contains("EBT"))
                {
                    EBTMonthFee = drPackageInfo["EBTMonFee"].ToString().Trim();
                    EBTTransFee = drPackageInfo["EBTTransFee"].ToString().Trim();
                }

                if (Serv.Contains("OnlineDebit"))
                {
                    OnlineDebitMonthFee = drPackageInfo["DebitMonFee"].ToString().Trim();
                    OnlineDebitTransFee = drPackageInfo["DebitTransFee"].ToString().Trim();
                }

                string discountPaid = "Monthly";

                if ((Convert.ToString(drPackageInfo["Processor"]).Contains("Intuit")))
                {
                    discountPaid = "Daily";
                }

                //Insert rates in Processing table
                ProcessingInfo Processing = new ProcessingInfo(AppId);
                bool retVal = Processing.UpdateProcessingInfo(
                    drPackageInfo["Processor"].ToString().Trim(), drPackageInfo["CardPresent"].ToString().Trim(),
                    drPackageInfo["CustServFee"].ToString().Trim(), drPackageInfo["InternetStmt"].ToString().Trim(), drPackageInfo["TransactionFee"].ToString().Trim(),
                    drPackageInfo["DiscRateQualPres"].ToString().Trim(), drPackageInfo["DiscRateQualNP"].ToString().Trim(),
                    drPackageInfo["DiscRateMidQual"].ToString().Trim(), drPackageInfo["DiscRateNonQual"].ToString().Trim(),
                    drPackageInfo["DiscRateQualDebit"].ToString().Trim(), drPackageInfo["DebitQualNP"].ToString().Trim(), drPackageInfo["DebitMidQual"].ToString().Trim(),
                    drPackageInfo["DebitNonQual"].ToString().Trim(), drPackageInfo["AmexDiscRateQual"].ToString().Trim(), drPackageInfo["AmexDiscRateMidQual"].ToString().Trim(), drPackageInfo["AmexDiscRateNonQual"].ToString().Trim(), drPackageInfo["ChargebackFee"].ToString().Trim(),
                    drPackageInfo["RetrievalFee"].ToString().Trim(), drPackageInfo["VoiceAuth"].ToString().Trim(),
                    drPackageInfo["BatchHeader"].ToString().Trim(), drPackageInfo["AVS"].ToString().Trim(),
                    drPackageInfo["MonMin"].ToString().Trim(), drPackageInfo["NBCTransFee"].ToString().Trim(),
                    drPackageInfo["AnnualFee"].ToString().Trim(), drPackageInfo["WirelessAccessFee"].ToString().Trim(),
                    drPackageInfo["WirelessTransFee"].ToString().Trim(), drPackageInfo["AppSetupFee"].ToString().Trim(),
                    drPackageInfo["AppFee"].ToString().Trim(), drPackageInfo["DebitMonFee"].ToString().Trim(),
                    drPackageInfo["DebitTransFee"].ToString().Trim(), drPackageInfo["CGMonFee"].ToString().Trim(),
                    drPackageInfo["CGTransFee"].ToString().Trim(), drPackageInfo["CGMonMin"].ToString().Trim(),
                    drPackageInfo["CGDiscRate"].ToString().Trim(), drPackageInfo["GCMonFee"].ToString().Trim(),
                    drPackageInfo["GCTransFee"].ToString().Trim(), EBTMonthFee,
                    EBTTransFee, discountPaid, Convert.ToString(drPackageInfo["ContractTerm"]).Trim(), drPackageInfo["ComplianceFee"].ToString().Trim(), "");
                
                if ((AcctType == 2) || (AcctType == 4))
                {
                    //Insert rates in gateway info table
                    Gateway GatewayInfo = new Gateway(AppId);
                    bool retVal2 = GatewayInfo.UpdateGatewayInfo(drPackageInfo["Gateway"].ToString().Trim(),
                        drPackageInfo["GatewayMonFee"].ToString().Trim(), drPackageInfo["GatewaySetupFee"].ToString().Trim(),
                        drPackageInfo["GatewayTransFee"].ToString().Trim());
                }
            }
        }//end if count not 0
    }//end function UpdateProcessing

    //This function Gateway rates
    public void UpdateGatewayRates()
    {
        Gateway Gateway = new Gateway(AppId);        
        string GatewayName = "";

        if ((strSW == "QB") && (Serv.Contains("Gateway/")))
            GatewayName = "Authorize.net";
        if (Serv.Contains("AuthnetGateway/") || Serv.Contains("AuthnetVPOSGateway/"))
            GatewayName = "Authorize.net";
        else if (Serv.Contains("PnPGateway/"))
            GatewayName = "Plug'n Pay";
        else if (Serv.Contains("SageGateway/"))
            GatewayName = "Sage Gateway";
        else if (Serv.Contains("ROAMpayGateway/"))
            GatewayName = "ROAMpay";

        DataSet ds = Gateway.GetGatewayRates(GatewayName);
        DataRow dr = ds.Tables[0].Rows[0];

        Gateway GatewayInfo = new Gateway(AppId);
        bool retVal = GatewayInfo.UpdateGatewayInfo(GatewayName, dr["GatewayMonFeeDef"].ToString().Trim(), 
            dr["GatewaySetupFeeDef"].ToString().Trim(), dr["GatewayTransFeeDef"].ToString().Trim());
    }

    //This function creates email for customers

    public string GetBody(string strFirstName, string strLastName, string strEmail, string strPhone, string strPhoneExt)
    {
        string strBody = "You have received a new online application. The applicant information is as follows: " + System.Environment.NewLine;
        strBody = strBody + "Contact First Name: " + strFirstName + System.Environment.NewLine;
        strBody = strBody + "Contact Last Name: " + strLastName + System.Environment.NewLine;
        strBody = strBody + "Contact Email: " + strEmail + System.Environment.NewLine;
        strBody = strBody + "Contact Phone Number: " + strPhone;
        strBody = strBody + " Ext: " + strPhoneExt + System.Environment.NewLine;
        strBody = strBody + System.Environment.NewLine;
        strBody = strBody + System.Environment.NewLine;
        strBody = strBody + "Thank You, " + System.Environment.NewLine;
        strBody = strBody + "Commerce Technologies" + System.Environment.NewLine;
  

        return strBody;
    }//end function GetBodyCust

    //This function creates email body
    public string GetBodyCust(string strFirstName, string strLastName, int AppId, string AcctType, string RepName)
    {
        /*string strBody = "You have received a new online application. The applicant information is as follows: " + System.Environment.NewLine;
        strBody = strBody + "Contact First Name: " + strFirstName + System.Environment.NewLine;
        strBody = strBody + "Contact Last Name: " + strLastName + System.Environment.NewLine;
        strBody = strBody + "Contact Email: " + strEmail + System.Environment.NewLine;
        strBody = strBody + "Contact Phone Number: " + strPhone;
        strBody = strBody + " Ext: " + strPhoneExt + System.Environment.NewLine;
        strBody = strBody + System.Environment.NewLine;
        strBody = strBody + System.Environment.NewLine;
        strBody = strBody + "Thank You, " + System.Environment.NewLine;
        strBody = strBody + "Commerce Technologies" + System.Environment.NewLine;*/

        string strRepName = "Jay Scott";
        if (RepName != "")
        {
            strRepName = RepName;
        }

        string strMessage = "Dear " + strFirstName + " " + strLastName + ", " + System.Environment.NewLine + System.Environment.NewLine;
        strMessage = strMessage + "Thank you for starting your online application for a " + AcctType + " Account with Commerce Technologies!" + System.Environment.NewLine;
        strMessage = strMessage + "Your Online Application information is not yet complete." + System.Environment.NewLine;
        strMessage = strMessage + "Please log in to your application by clicking on the following link: " + System.Environment.NewLine;
        strMessage = strMessage + "https://www.apps.commercetech.com/Application/default.aspx?AppId=" + AppId + System.Environment.NewLine + System.Environment.NewLine;
        strMessage = strMessage + "For any additional questions or concerns please visit " + "https://commercetech.com/" + " or call (800)-477-5363" +  System.Environment.NewLine;
        strMessage = strMessage + "Thank you, " + System.Environment.NewLine + System.Environment.NewLine;
        strMessage = strMessage + strRepName;


        return strMessage;
    }//end function GetBody


    //This function handles radio button checked event
    protected void rdbGateway_CheckedChanged(object sender, EventArgs e)
    {
        if (sender.Equals(rdbMerchant))
        {
            strMerchantGateway = "Merchant";
            AcctType = 1;
            lblRegistrationHeader.Text = "Merchant Account Registration";
        }
        if (sender.Equals(rdbGateway))
        {
            strMerchantGateway = "Gateway";
            AcctType = 2;
            lblRegistrationHeader.Text = "Gateway Registration";
        }
        if (sender.Equals(rdbBoth))
        {
            strMerchantGateway = "MerchantAndGateway";
            AcctType = 4;
            lblRegistrationHeader.Text = "Merchant Account and Gateway Registration";
        }
        if (sender.Equals(rdbEquipment))
        {
            strMerchantGateway = "Equipment";
            AcctType = 3;
            lblRegistrationHeader.Text = "Equipment Registration";
        }
    }//end function radio button check changed

    //This function validates data in text fields
    protected bool ValidateData()
    {
        TextBox txtBox = new TextBox();
        for (int i = 0; i < pnlMainPage.Controls.Count; i++)
        {
            if (pnlMainPage.Controls[i].GetType() == txtBox.GetType())
            {
                txtBox = (TextBox)pnlMainPage.Controls[i];
                if ((txtBox.Text.Contains("--")) || (txtBox.Text.Contains("#")) || (txtBox.Text.Contains(";")) || (txtBox.Text.Contains("'")) || (txtBox.Text.Contains("#")) || (txtBox.Text.Contains("<")) || (txtBox.Text.Contains(">")) || (txtBox.Text.ToLower().Contains("script")))
                {
                    txtBox.BackColor = System.Drawing.Color.Ivory;
                    txtBox.Focus();
                    lblError.Visible = true;
                    lblError.Text = "You cannot use apostrophes, #, or semi-colons in any of the following fields.";
                    return false;
                }
                else
                    txtBox.BackColor = System.Drawing.Color.White;
            }
        }//end for
        if ((txtPassword.Text.Contains("--"))  || (txtPassword.Text.Contains("#")) || (txtPassword.Text.Contains(";")) || (txtPassword.Text.Contains("'")))
        {
            SetErrorMessage("You cannot use hyphens, apostrophe, # or semi-colons in the Password field.");
            return false;
        }
        //Make sure length of password is atleast 5 characters long
        if (txtPassword.Text.Trim().Length < 5)
        {
            SetErrorMessage("Please choose a password which is at least 5 characters long. You cannot use characters like semi-colons, #, hyphens or apostrophes.");
            return false;
        }
        //Make sure length of login is atleast 5 characters long
        if (txtLoginName.Text.Trim().Length < 5)
        {
            SetErrorMessage("Please choose a login name which is at least 5 characters long. You cannot use characters like semi-colons, #, hyphens or apostrophes.");
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

}
