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
using System.Xml;
using BusinessLayer;
using DLPartner;
using DLPartner.PartnerDSTableAdapters;
using OnlineAppClassLibrary;
using System.Text;
using System.IO;
//using System.Web.Mail;
using System.Net.Mail;




using System.Collections.Generic;
using System.Text;
using System.Data;
using DLPartner;
using OnlineAppClassLibrary;
using DLPartner.PartnerDSTableAdapters;
using System.Web.Security;
using System.Security.Cryptography;
using DataLayer;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.SessionState;
using System.Xml;

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;



//using iTextSharp.text;
//using iTextSharp.text.pdf;
using System.Net;
using System.Collections.Specialized;
using System.Xml;
//using Newtonsoft.Json;

public partial class Edit : Loader
{
    void Page_Init(object sender, EventArgs e)
    {
        ViewStateUserKey = Session.SessionID;
        if (!User.IsInRole("Admin") && !User.IsInRole("Employee"))
            Tabs.Controls.Remove(TabHistory);
        PopulateAddlServices();
        //populateEnvelopeStatus();
        //PopulateDBEBT();
    }

    private static string ConnStringACT = ConfigurationManager.AppSettings["ConnectionStringACT"].ToString();

    private static int AppId = 0;
    private static int AffiliateID = 0;
    private static int AcctType = 0;
    private static string StatusCheckServ = "";
    private static string StatusGift = "";
    private static string StatusLease = "";
    private static string StatusMCA = "";
    private static string StatusPayroll = "";
    //string StatusLease = string.Empty;

    PartnerLogBL LogData = new PartnerLogBL();

   //private static string CardPresent;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session.IsNewSession)
            Response.Redirect("~/login.aspx");

        //Resellers and Affiliates do not have access to this page. Redirect them to the login page.
        if (User.IsInRole("Reseller") || User.IsInRole("Affiliate"))
            Response.Redirect("~/login.aspx?Authentication=False");

        if ((Request.Params.Get("AppId") != null))
        {
            //Get AppId from the URL and check if it is an integer. If not, redirect to login page
            if (Int32.TryParse(Request.Params.Get("AppId").ToString(), out AppId))
                AppId = Convert.ToInt32(Request.Params.Get("AppId"));
            else
                Response.Redirect("~/login.aspx?InvalidRequest=True");
        }//end if appid not null
        else
            Response.Redirect("~/login.aspx");

        if (!IsPostBack)
        {
            if (!User.Identity.IsAuthenticated)
                Response.Redirect("~/login.aspx?Authentication=False");

            if (AppId == 0)
                Response.Redirect("~/login.aspx");

            try
            {
                #region STYLES
                Style TextArea = new Style();
                TextArea.Width = new Unit(200);
                TextArea.Height = new Unit(60);
                TextArea.Font.Size = FontUnit.Point(8);
                TextArea.Font.Name = "Arial";
                txtNotes.ApplyStyle(TextArea);
                #endregion

                #region CHECK ACCESS
                //********************************CHECK ACCESS********************************
                //Check if the partner logged in has access to the App. If not redirect to login.aspx
                bool bAccess = false;
                if (User.IsInRole("Employee") || User.IsInRole("Admin") || User.IsInRole("Office"))
                    bAccess = true;
                else
                {
                    OnlineAppBL CheckRepAccess = new OnlineAppBL(AppId);
                    bAccess = CheckRepAccess.CheckAccess(Session["AffiliateID"].ToString());
                }
                if (!bAccess)
                    Response.Redirect("~/login.aspx?Authentication=False");

                //********************************END CHECK ACCESS********************************
                #endregion

                #region URL COMMAND
                string Task = string.Empty;
                if (Request.Params.Get("Task") != null)
                    Task = Request.Params.Get("Task");

                if (Task == "AddToACT")
                {
                    try
                    {
                        if (User.IsInRole("Employee") || User.IsInRole("Admin"))
                            AddRecordToACT(AppId);
                        else
                            Response.Redirect("~/login.aspx?Authentication=False", false);
                    }//end try                 
                    catch (Exception err)
                    {
                        CreateLog Log = new CreateLog();
                        Log.ErrorLog(Server.MapPath("~/ErrorLog"), "Add To ACT! - " + err.Message);
                        DisplayMessage(err.Message);
                    }
                }
                else if (Task == "UpdateInACT")
                {
                    try
                    {
                        if (User.IsInRole("Employee") || User.IsInRole("Admin"))
                            UpdateInACT(AppId);
                        else
                            Response.Redirect("~/login.aspx?Authentication=False", false);
                    }//end try
                    catch (Exception err)
                    {
                        CreateLog Log = new CreateLog();
                        Log.ErrorLog(Server.MapPath("~/ErrorLog"), "Update In ACT! - " + err.Message);
                        DisplayMessage(err.Message);
                    }

                }
                /*else if (Task == "CreatePDF")
                {
                    if (User.IsInRole("Employee") || User.IsInRole("Admin"))
                    {
                        try
                        {
                            OnlineAppBL OnlineApp = new OnlineAppBL();
                            string strProcessor = OnlineApp.GetProcessorName(AppId);
                            if (strProcessor.ToLower() == "ims")
                                CreateIMSPDF(false);
                            else if (strProcessor.ToLower().Contains("ipayment"))
                                CreateIPayPDF(false);
                            else if (strProcessor.ToLower().Contains("merrick"))
                                CreateMerrickPDF();
                            else if (strProcessor.ToLower().Contains("chase"))
                                pnlChasePDF.Visible = true;
                        }//end try
                        catch (Exception err)
                        {
                            CreateLog Log = new CreateLog();
                            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "Create PDF - " + err.Message);
                            DisplayMessage(err.Message);
                        }

                    }
                    else
                        Response.Redirect("~/login.aspx?Authentication=False");
                }*/
                #endregion

                #region PAGE LOAD FUNCTIONS
                //Populate page with application data
                PopulateFields();
                //LoadNotes();
                PopulateHistory();
                PopulateRates();
                //PopulateAddlServices();
                PopulateSalesOpps();
                PopulateStatus();
                //PopulateCardPCT();
                PopulateDBEBT();
                populateEnvelopeStatus();
                #endregion

                //Check if application is locked
                if (Locked())
                {
                    //DisplayMessage("The application is locked because the Merchant status or Gateway Status prevents it from being edited.");
                    if (User.IsInRole("Admin"))
                    {
                        lnkAddSalesOpps.Visible = true;
                        btnSubmit.Enabled = true; //Status Submit button
                        //btnSubmitCardPCT.Enabled = true;
                        imgAddToACT.Enabled = true;
                        imgUpdateInACT.Enabled = true;
                        pnlDelDocuSignEnv.Enabled = true;
                    }
                    else if (User.IsInRole("Employee"))
                    {
                        lnkAddSalesOpps.Visible = true;
                        //btnSubmit.Enabled = false; //Status Submit button
                        //btnSubmitCardPCT.Enabled = false;
                        lstMerchantStatus.Enabled = false;
                        lstPlatform.Enabled = false;
                        imgAddToACT.Enabled = false;
                        imgAddToACT.ImageUrl = "~/Images/AddToACT_gray.gif";
                        imgAddToACT.ToolTip = "Cannot add this application because it is locked.";
                        imgUpdateInACT.Enabled = true;
                        pnlDelDocuSignEnv.Enabled = false;
                        lnkDelDocuSignEnv.Enabled = false;
                        pnlDelDocuSignEnv.Visible = true;
                        lnkDelDocuSignEnv.Visible = true;
                    }
                    else if (User.IsInRole("Agent"))
                    {
                        lnkAddSalesOpps.Visible = true;
                        //btnSubmit.Enabled = false; //Status Submit button
                        //btnSubmitCardPCT.Enabled = false;
                        lstGatewayStatus.Enabled = false;
                        lstPlatform.Enabled = false;
                        imgAddToACT.Enabled = false;
                        imgAddToACT.ImageUrl = "~/Images/AddToACT_gray.gif";
                        imgAddToACT.ToolTip = "Cannot add this application because it is locked.";
                        imgUpdateInACT.Enabled = false;
                        imgUpdateInACT.ImageUrl = "~/Images/UpdateInACT_gray.gif";
                        imgUpdateInACT.ToolTip = "Cannot update this application because it is locked.";
                    }
                    else
                    {
                        lnkAddSalesOpps.Visible = true;
                        //btnSubmit.Enabled = false; //Status Submit button
                        //btnSubmitCardPCT.Enabled = false;
                        lstMerchantStatus.Enabled = false;
                        lstPlatform.Enabled = false;
                        imgAddToACT.Enabled = false;
                        imgAddToACT.ImageUrl = "~/Images/AddToACT_gray.gif";
                        imgAddToACT.ToolTip = "Cannot add this application because it is locked.";
                        imgUpdateInACT.Enabled = false;
                        imgUpdateInACT.ImageUrl = "~/Images/UpdateInACT_gray.gif";
                        imgUpdateInACT.ToolTip = "Cannot update this application because it is locked.";
                        pnlDelDocuSignEnv.Enabled = false;
                        lnkDelDocuSignEnv.Enabled = false;
                        pnlDelDocuSignEnv.Visible = false;
                        lnkDelDocuSignEnv.Visible = false;
                    }
                }

                if (GatewayLocked())
                {
                    //DisplayMessage("The application is locked because the Merchant status or Gateway Status prevents it from being edited.");
                    if (User.IsInRole("Admin"))
                    {
                        lnkAddSalesOpps.Visible = true;
                        btnSubmit.Enabled = true; //Status Submit button
                        //btnSubmitCardPCT.Enabled = true;
                        imgAddToACT.Enabled = true;
                        imgUpdateInACT.Enabled = true;
                    }
                    else if (User.IsInRole("Employee"))
                    {
                        lnkAddSalesOpps.Visible = true;
                        //btnSubmit.Enabled = false; //Status Submit button
                        //btnSubmitCardPCT.Enabled = false;
                        lstMerchantStatus.Enabled = false;
                        lstPlatform.Enabled = false;
                        imgAddToACT.Enabled = false;
                        imgAddToACT.ImageUrl = "~/Images/AddToACT_gray.gif";
                        imgAddToACT.ToolTip = "Cannot add this application because it is locked.";
                        imgUpdateInACT.Enabled = true;
                    }
                    else if (User.IsInRole("Agent"))
                    {
                        lnkAddSalesOpps.Visible = true;
                        //btnSubmit.Enabled = false; //Status Submit button
                        //btnSubmitCardPCT.Enabled = false;
                        lstGatewayStatus.Enabled = false;
                        lstPlatform.Enabled = false;
                        imgAddToACT.Enabled = false;
                        imgAddToACT.ImageUrl = "~/Images/AddToACT_gray.gif";
                        imgAddToACT.ToolTip = "Cannot add this application because it is locked.";
                        imgUpdateInACT.Enabled = false;
                        imgUpdateInACT.ImageUrl = "~/Images/UpdateInACT_gray.gif";
                        imgUpdateInACT.ToolTip = "Cannot update this application because it is locked.";
                    }
                    else
                    {
                        lnkAddSalesOpps.Visible = true;
                        //btnSubmit.Enabled = false; //Status Submit button
                        //btnSubmitCardPCT.Enabled = false;
                        lstGatewayStatus.Enabled = false;
                        lstPlatform.Enabled = false;
                        imgAddToACT.Enabled = false;
                        imgAddToACT.ImageUrl = "~/Images/AddToACT_gray.gif";
                        imgAddToACT.ToolTip = "Cannot add this application because it is locked.";
                        imgUpdateInACT.Enabled = false;
                        imgUpdateInACT.ImageUrl = "~/Images/UpdateInACT_gray.gif";
                        imgUpdateInACT.ToolTip = "Cannot update this application because it is locked.";
                    }
                }

                if (User.IsInRole("Admin"))
                {
                    NewAppInfo newApp = new NewAppInfo(AppId);
                    bool docuSignStatus = newApp.docuSignStatus();
                    if (docuSignStatus == true)
                    {
                        CreatePDF PDFFile = new CreatePDF(AppId);
                        string strProcessor = PDFFile.ReturnProcessorName();
                        string docuSignProcessor = newApp.GetDocuSignProcessor();
                        string envelopeId = newApp.GetDocuSignEnvId();
                        if (envelopeId != "")
                        {
                            pnlDelDocuSignEnv.Enabled = true;
                            pnlDelDocuSignEnv.Visible = true;
                            pnlEnvStatus.Visible = true;
                            imgCreatePDF.Enabled = true;
                            lnkSageAgreement1.Enabled = true;
                            lnkAmendment1.Enabled = true;
                        }
                        else
                        {
                            pnlDelDocuSignEnv.Enabled = false;
                            pnlDelDocuSignEnv.Visible = false;
                            pnlEnvStatus.Visible = false;
                        }
                    }
                    else {
                        pnlDelDocuSignEnv.Enabled = false;
                        pnlDelDocuSignEnv.Visible = false;
                        pnlEnvStatus.Visible = false;
                    }
                }
                else if (User.IsInRole("Employee"))
                {
                    NewAppInfo newApp = new NewAppInfo(AppId);
                    bool docuSignStatus = newApp.docuSignStatus();
                    if (docuSignStatus == true)
                    {
                        CreatePDF PDFFile = new CreatePDF(AppId);
                        string strProcessor = PDFFile.ReturnProcessorName();
                        string docuSignProcessor = newApp.GetDocuSignProcessor();
                        string envelopeId = newApp.GetDocuSignEnvId();
                        if (envelopeId != "")
                        {
                            pnlEnvStatus.Visible = true;
                            imgCreatePDF.Enabled = true;
                            lnkSageAgreement1.Enabled = true;
                            lnkAmendment1.Enabled = true;
                            pnlDelDocuSignEnv.Enabled = false;
                            pnlDelDocuSignEnv.Visible = true;

                        }
                        else
                        {
                            pnlEnvStatus.Visible = false;
                        }
                    }
                    else
                    {
                        pnlEnvStatus.Visible = false;
                    }

                    pnlDelDocuSignEnv.Enabled = false;
                    lnkDelDocuSignEnv.Enabled = false;
                    pnlDelDocuSignEnv.Visible = true;
                    lnkDelDocuSignEnv.Visible = true;
                }
                else
                {

                    NewAppInfo newApp = new NewAppInfo(AppId);
                    bool docuSignStatus = newApp.docuSignStatus();
                    if (docuSignStatus == true)
                    {
                        CreatePDF PDFFile = new CreatePDF(AppId);
                        string strProcessor = PDFFile.ReturnProcessorName();
                        string docuSignProcessor = newApp.GetDocuSignProcessor();
                        string envelopeId = newApp.GetDocuSignEnvId();
                        if (envelopeId != "")
                        {
                            pnlEnvStatus.Visible = true;
                            imgCreatePDF.Enabled = true;
                            lnkSageAgreement1.Enabled = true;
                            lnkAmendment1.Enabled = true;
                        }
                        else
                        {
                            pnlEnvStatus.Visible = false;
                        }
                    }
                    else
                    {
                        pnlEnvStatus.Visible = false;
                    }

                    pnlDelDocuSignEnv.Enabled = false;
                    lnkDelDocuSignEnv.Enabled = false;
                    pnlDelDocuSignEnv.Visible = false;
                    lnkDelDocuSignEnv.Visible = false;
                    //pnlEnvStatus.Visible = false;
                }

                OnlineAppProfile AppInfo = new OnlineAppProfile(AppId);
                DataSet dsNewApp = AppInfo.GetProfileData();
                DataTable dtNewApp = dsNewApp.Tables[0];

                int PackageID = 0;
                NewAppInfo ReturnApp = new NewAppInfo(AppId);
                PackageID = ReturnApp.ReturnPID();

                if (dtNewApp.Rows.Count > 0)
                {
                    DataRow drNewApp = dtNewApp.Rows[0];
                    int AcctType = Convert.ToInt32(drNewApp["AcctType"]);
                    //if gateway only or equipment only account then don't show create PDF button
                    if ((AcctType == 2) || (AcctType == 3))
                        pnlPDF.Visible = false;
                    else
                        pnlPDF.Visible = true;
                }

                //if QB merchant services account or QB POS account or IPS GoPayment account then don't show create PDF button
                if ((PackageID == 255) || (PackageID == 254) || (PackageID == 253))
                {
                    pnlPDF.Visible = false;
                }

                #region Mouseover events
                //Assign mouse over and mouse out attributes to all images
                imgCreatePDF.Attributes.Add("onmouseover", "this.src = '../Images/CreateIMSPDF_Mouseover.gif'");
                imgCreatePDF.Attributes.Add("onmouseout", "this.src = '../Images/CreateIMSPDF.gif'");
                imgDelete.Attributes.Add("onmouseover", "this.src = '../Images/Delete_Mouseover.gif'");
                imgDelete.Attributes.Add("onmouseout", "this.src = '../Images/Delete.gif'");
                imgAddToACT.Attributes.Add("onmouseover", "this.src = '../Images/AddToACT_Mouseover.gif'");
                imgAddToACT.Attributes.Add("onmouseout", "this.src = '../Images/AddToACT.gif'");
                imgUpdateInACT.Attributes.Add("onmouseover", "this.src = '../Images/UpdateInACT_Mouseover.gif'");
                imgUpdateInACT.Attributes.Add("onmouseout", "this.src = '../Images/UpdateInACT.gif'");
                //imgUpdateRates.Attributes.Add("onmouseover", "this.src = '../Images/UpdateRates_Mouseover.gif'");
                //imgUpdateRates.Attributes.Add("onmouseout", "this.src = '../Images/UpdateRates.gif'");
                #endregion
            }//end try
            catch (Exception err)
            {
                CreateLog Log = new CreateLog();
                Log.ErrorLog(Server.MapPath("~/ErrorLog"), "Edit Page Load - " + err.Message);
                DisplayMessage("Error Populating Page");
            }
        }
        //else {
            //PopulateAddlServices();
        //}//end if post back
    }//end if page load

    #region POPULATE
    //This function populates fields
    public void PopulateFields()

    {
        try
        {
            pndAddlServicesPDF.Visible = false;

            //Check access and disable buttons
            if (!User.IsInRole("Admin") && !User.IsInRole("Employee"))
            {
                pnlACT.Visible = false;
                pnlDeleteApp.Visible = false;
                //imgUpdateRates.Visible = false;
            }

            //Disable delete for Employees
            if (User.IsInRole("Employee"))
                pnlDeleteApp.Visible = false;

            if (Convert.ToInt32(AppId) == 1)
            {
                pnlDeleteApp.Visible = false;
            }
            
            string strProcessor = string.Empty;
            bool boolMerchantFunding = false;
            bool boolLease = false;
            bool boolGiftCard = false;
            string strMCA = string.Empty;
            string strLease = string.Empty;
            string strGiftCard = string.Empty;
            int LoginAttempts = 0;
            //Get Application information from OnlineAppNewApp table and populate fields
            OnlineAppBL App = new OnlineAppBL(AppId);
            DataSet ds = App.GetEditInfo();
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow drNewApp = ds.Tables[0].Rows[0];

                lblAppId.Text = Server.HtmlEncode(drNewApp["AppId"].ToString());
                lblTitleValue.Text = Server.HtmlEncode(drNewApp["Title"].ToString().Trim());
                lblFirstNameValue.Text = Server.HtmlEncode(drNewApp["FirstName"].ToString().Trim());
                lblLastNameValue.Text = Server.HtmlEncode(drNewApp["LastName"].ToString().Trim());
                lblPhoneValue.Text = Server.HtmlEncode(drNewApp["Phone"].ToString().Trim());
                lblPhoneExtValue.Text = Server.HtmlEncode(drNewApp["PhoneExt"].ToString().Trim());
                lblMobilePhoneValue.Text = Server.HtmlEncode(drNewApp["MobilePhone"].ToString().Trim());
                lblHomePhoneValue.Text = Server.HtmlEncode(drNewApp["HomePhone"].ToString().Trim());
                strProcessor = drNewApp["Processor"].ToString().Trim();
                LoginAttempts = Convert.ToInt32(drNewApp["LoginAttempts"]);
                boolMerchantFunding = Convert.ToBoolean(drNewApp["MerchantFunding"]);
                boolLease = Convert.ToBoolean(drNewApp["Lease"]);
                boolGiftCard = Convert.ToBoolean(drNewApp["GiftCard"]);
                strMCA = Convert.ToString(drNewApp["MCAType"]);
                strLease = Convert.ToString(drNewApp["LeaseCompany"]);
                strGiftCard = Convert.ToString(drNewApp["GiftCardType"]);

            }//end if count not 0

            if (User.IsInRole("Agent") || User.IsInRole("T1Agent"))
            {
                lnkbtnModify.Visible = false;
                if (LoginAttempts == 5)
                {
                    pnlLoginAttempts.Visible = true;
                }
                else
                {
                    pnlLoginAttempts.Visible = false;
                }
            }
            

            //Check if application is locked for too many login attempts
            if (User.IsInRole("Admin") || (User.IsInRole("Employee")))
            {
                if (LoginAttempts == 5)
                    pnlLoginAttempts.Visible = true;
                else
                    pnlLoginAttempts.Visible = false;
            }

            if (strProcessor.ToLower().Contains("sage"))
            {
                //lblProcessorPDF.Text = "Sage Payment Solution";
                lnkSageAgreement1.Visible = true;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow drNewApp = ds.Tables[0].Rows[0];
                    if (Convert.ToString(drNewApp["ContractTerm"]) == "1 month")
                    {
                        lnkAmendment1.Visible = true;
                    }
                    else if (Convert.ToString(drNewApp["ContractTerm"]) == "1 year")
                    {
                        lnkAmendment1.Visible = true;
                    }
                }
                //if 
            }
            else if (strProcessor.ToLower().Contains("intuit"))
            {
                //lblProcessorPDF.Text = "IPS";
            }
            else if (strProcessor.ToLower().Contains("ipayment"))
            {
                //lblProcessorPDF.Text = "iPayment";
            }
            else if (strProcessor.ToLower().Contains("optimal-merrick"))
            {
                //lblProcessorPDF.Text = "Optimal-Merrick";
            }
            else if (strProcessor.ToLower().Contains("canada"))
            {
                //lblProcessorPDF.Text = "Canada";
            }
            else if ((strProcessor.ToLower().Contains("international")) || (strProcessor.ToLower().Contains("cal")))
            {
                //lblProcessorPDF.Text = "Optimal Cal";
            }
            else if (strProcessor.ToLower().Contains("CardConnect"))
            {
                //lblProcessorPDF.Text = "CardConnect";
            }
            else if (strProcessor.ToLower().Contains("kitts"))
            {
                //lblProcessorPDF.Text = "St. Kitts";
            }
            else if (strProcessor.ToLower().Contains("payvision"))
            {
                //lblProcessorPDF.Text = "Payvision";
            }
            else
            {
                pnlPDF.Visible = false;
            }

            if (boolMerchantFunding || boolLease || boolGiftCard)
            {
                pndAddlServicesPDF.Visible = false;
                lblLeasePDF.Visible = false;
                lblGiftcardPDF.Visible = false;
                lblMCAPDF.Visible = false;
                ImageMCAPDF.Visible = false;
                ImageLeasePDF.Visible = false;
                ImageGiftCardPDF.Visible = false;
                HelpMCAPDF.Visible = false;
                HelpLeasePDF.Visible = false;
                HelpGiftPDF.Visible = false;
            }

            if (boolMerchantFunding)
            {
                lblMCAPDF.Visible = false;
                ImageMCAPDF.Visible = false;
                HelpMCAPDF.Visible = false;
                if (strMCA.ToLower().Contains("advanceme"))
                {
                    lblMCAPDF.Text = "Create AdvanceMe PDF";
                }
                else if (strMCA.ToLower().Contains("business"))
                {
                    lblMCAPDF.Text = "Create BFS PDF";
                }
                else if (strMCA.ToLower().Contains("rapidadvance"))
                {
                    lblMCAPDF.Text = "Create RapidAdvance PDF";
                }
            }

            if (boolLease)
            {
                lblLeasePDF.Visible = false;
                ImageLeasePDF.Visible = false;
                HelpLeasePDF.Visible = false;
                if (strLease.ToLower().Contains("northern"))
                {
                    lblLeasePDF.Text = "Create Northern Lease PDF";
                }
            }

            if (boolGiftCard)
            {
                lblGiftcardPDF.Visible = false;
                ImageGiftCardPDF.Visible = false;
                HelpGiftPDF.Visible = false;
                lblGiftcardPDF.Text = "Create " + strGiftCard + " PDF";
            }


            
        }//end try
        catch (Exception err)
        {
            CreateLog Log = new CreateLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "Populate Edit Page - " + err.Message);
            DisplayMessage("Error loading page. Please contact Technical Support.");
        }
    }//end function populate fields

    //This function checks if the application is locked
    protected bool Locked()
    {
        //Check whether the application is locked
        OnlineAppStatusBL Status = new OnlineAppStatusBL(AppId);
        string strLocked = Status.ReturnLocked();
        if (strLocked == "Yes")
            return true;
        else
            return false;
    }

    protected bool GatewayLocked()
    {
        //Check whether the application is locked
        OnlineAppStatusBL Status = new OnlineAppStatusBL(AppId);
        string strLocked = Status.ReturnGatewayLocked();
        if (strLocked == "Yes")
            return true;
        else
            return false;
    }

    protected void populateEnvelopeStatus()
    {

        //string username = "twu@commercetech.com";
        //string password = "Commerce1";
        //string username = "jscott@commercetech.com";
        //string password = "1Commerce11";
        string username = "information@commercetech.com";
        string password = "1Success11";
        string integratorKey = "ECOM-21cf1b09-3dbf-41fc-9910-13c11df86eb5";
        //string url = "https://demo.docusign.net/restapi/v2/login_information";
        string url = "https://na2.docusign.net/restapi/v2/login_information";
        string baseURL = "";	// we will retrieve this
        string accountId = "";	// will retrieve
        string envelopeId = "";	// will retrieve
        string envelopeStatus = "";
        string uri = "";	// will retrieve
        string requestBody = "";
        string authenticateStr = "<DocuSignCredentials>" +
    "<Username>" + username + "</Username>" +
    "<Password>" + password + "</Password>" +
    "<IntegratorKey>" + integratorKey + "</IntegratorKey>" +
    "</DocuSignCredentials>";
        NewAppInfo newApp = new NewAppInfo(AppId);
        bool docuSignStatus = newApp.docuSignStatus();
        if (docuSignStatus == true)
        {
            CreatePDF PDFFile = new CreatePDF(AppId);
            string strProcessor = PDFFile.ReturnProcessorName();
            string docuSignProcessor = newApp.GetDocuSignProcessor();
            string strEnvelopeStatus = "";
            if (docuSignProcessor == strProcessor)
            {
                envelopeId = newApp.GetDocuSignEnvId();
                if (envelopeId != "")
                {
                    DataSet ds = new DataSet();
                    ds = newApp.docusginSignerInfo();
                    uri = "/envelopes/" + envelopeId;
                    docusignGetEnvelopeInfo(url, authenticateStr, envelopeId, out envelopeStatus);
                    if(Convert.ToString(envelopeStatus).ToLower().Contains("complete"))
                    {
                        strEnvelopeStatus = "Signed";
                    }else if (Convert.ToString(envelopeStatus).ToLower().Contains("sent"))
                    {
                        strEnvelopeStatus = "Viewed";
                    }else{
                        strEnvelopeStatus = Convert.ToString(envelopeStatus);
                    }
                    lblEnvStatus.Text = "Envelope status: " + Convert.ToString(strEnvelopeStatus);
                }
            }
        }
    }

    protected void docusignGetEnvelopeInfo(string url, string authenticateStr, string envelopeId, out string envelopeStatus)
    {
        //string username = "twu@commercetech.com";
        //string password = "Commerce1";
        //string username = "jscott@commercetech.com";
        //string password = "1Commerce11";
        string username = "information@commercetech.com";
        string password = "1Success11";
        string integratorKey = "ECOM-21cf1b09-3dbf-41fc-9910-13c11df86eb5";
        //string url = "https://demo.docusign.net/restapi/v2/login_information";
        //string baseURL = "";	// we will retrieve this
        //string accountId = "";	// will retrieve
        string accountId1 = "";
        string baseURL1 = "";
        //string envelopeId = "";
        string envelopeUri = "/envelopes/" + envelopeId;
        string envelopeStatus1 = "";

        authenticateStr =
            "<DocuSignCredentials>" +
            "<Username>" + username + "</Username>" +
            "<Password>" + password + "</Password>" +
            "<IntegratorKey>" + integratorKey + "</IntegratorKey>" +
            "</DocuSignCredentials>";

        // 
        // STEP 1 - Login
        //
        try
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("X-DocuSign-Authentication", authenticateStr);
            request.Accept = "application/xml";
            request.Method = "GET";
            HttpWebResponse webResponse = (HttpWebResponse)request.GetResponse();
            StreamReader sr = new StreamReader(webResponse.GetResponseStream());
            string responseText = sr.ReadToEnd();
            using (XmlReader reader = XmlReader.Create(new StringReader(responseText)))
            {
                while (reader.Read())
                {	// Parse the xml response body
                    if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "accountId"))
                        accountId1 = reader.ReadString();
                    if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "baseUrl"))
                        baseURL1 = reader.ReadString();
                }
            }

            //--- display results
            //Console.WriteLine("accountId = " + accountId + "\nbaseUrl = " + baseURL);

            //
            // STEP 2 - Get Envelope Info
            //
            // use baseURL value + envelopeUri for url of this request
            request = (HttpWebRequest)WebRequest.Create(baseURL1 + envelopeUri);
            request.Headers.Add("X-DocuSign-Authentication", authenticateStr);
            request.ContentType = "application/xml";
            request.Accept = "application/xml";
            request.Method = "GET";
            // read the response
            webResponse = (HttpWebResponse)request.GetResponse();
            sr.Close();
            responseText = "";
            sr = new StreamReader(webResponse.GetResponseStream());
            responseText = sr.ReadToEnd();
            using (XmlReader reader = XmlReader.Create(new StringReader(responseText)))
            {
                while (reader.Read())
                {
                    if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "status"))
                    {
                        //reader.Read();
                        envelopeStatus1 = reader.ReadString();
                    }
                }
            }

            //--- display results
            //Console.WriteLine("Envelope Info Received!\n  Response is --> " + responseText);

            //LogBL LogData = new LogBL(AppId);
            //LogData.InsertLogData(AffiliateID, responseText);
            /*
            using (XmlReader reader = XmlReader.Create(new StringReader(responseText)))
            {
                while (reader.Read())
                {	// Parse the xml response body
                    LogBL LogData = new LogBL(AppId);
                    LogData.InsertLogData(AffiliateID, responseText);
                }
            }*/

            //DisplayMessage(responseText);
        }
        catch (WebException err)
        {
            using (WebResponse response = err.Response)
            {
                HttpWebResponse httpResponse = (HttpWebResponse)response;
                Console.WriteLine("Error code: {0}", httpResponse.StatusCode);
                using (Stream data = response.GetResponseStream())
                {
                    string text = new StreamReader(data).ReadToEnd();
                    CreateOnlineAppLog Log = new CreateOnlineAppLog();
                    Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + AppId.ToString() + " - " + "DocuSign Error - " + text);
                    //SetErrorMessage("An error occured. Please contact Commerce Technologies Technical Support: <a href=mailto:support@commercetech.com?Subject=Online App Error, App ID: " + Session["AppId"].ToString() + "> support@commercetech.com </a> or call 1-800-477-5363 for assistance.");
                    //LogBL LogData = new LogBL(AppId);
                    //LogData.InsertLogData(AffiliateID, "Failed to retrieve envelope status.");
                }
            }
        }
        envelopeStatus = envelopeStatus1;
        //lblEnvStatus.Text = Convert.ToString(envelopeStatus);

        /*
        string accountId1 = "";
        string baseURL1 = "";
        try
        {

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("X-DocuSign-Authentication", authenticateStr);
            request.Accept = "application/xml";
            request.Method = "GET";
            HttpWebResponse webResponse = (HttpWebResponse)request.GetResponse();
            StreamReader sr = new StreamReader(webResponse.GetResponseStream());
            string responseText = sr.ReadToEnd();
            SetErrorMessage(responseText);
            using (Stream data = webResponse.GetResponseStream())
            {
                string text = new StreamReader(data).ReadToEnd();
                Console.WriteLine(text);
            }
            using (XmlReader reader = XmlReader.Create(new StringReader(responseText)))
            {
                while (reader.Read())
                {	// Parse the xml response body
                    if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "accountId"))
                        accountId1 = reader.ReadString();
                    if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "baseUrl"))
                        baseURL1 = reader.ReadString();
                }
            }

        }
        catch (WebException err)
        {
            using (WebResponse response = err.Response)
            {
                HttpWebResponse httpResponse = (HttpWebResponse)response;
                Console.WriteLine("Error code: {0}", httpResponse.StatusCode);
                using (Stream data = response.GetResponseStream())
                {
                    string text = new StreamReader(data).ReadToEnd();
                    CreateOnlineAppLog Log = new CreateOnlineAppLog();
                    Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + AppId.ToString() + " - " + "DocuSign Error - " + text);
                    SetErrorMessage("An error occured. Please contact Commerce Technologies Technical Support: <a href=mailto:support@commercetech.com?Subject=Online App Error, App ID: " + Session["AppId"].ToString() + "> support@commercetech.com </a> or call 1-800-477-5363 for assistance.");
                    LogBL LogData = new LogBL(AppId);
                    LogData.InsertLogData(AffiliateID, "Authorization failed.");
                }
            }
        }
        accountId = accountId1;
        baseURL = baseURL1;*/
    }

    protected bool CheckServiceLocked()
    {
        //Check whether the application is locked
        OnlineAppStatusBL Status = new OnlineAppStatusBL(AppId);
        string strLocked = Status.ReturnCheckServiceLocked();
        if (strLocked == "Yes")
            return true;
        else
            return false;
    }

    protected bool GiftLocked()
    {
        //Check whether the application is locked
        OnlineAppStatusBL Status = new OnlineAppStatusBL(AppId);
        string strLocked = Status.ReturnGiftLocked();
        if (strLocked == "Yes")
            return true;
        else
            return false;
    }

    protected bool LeaseLocked()
    {
        //Check whether the application is locked
        OnlineAppStatusBL Status = new OnlineAppStatusBL(AppId);
        string strLocked = Status.ReturnLeaseLocked();
        if (strLocked == "Yes")
            return true;
        else
            return false;
    }

    protected bool MCALocked()
    {
        //Check whether the application is locked
        OnlineAppStatusBL Status = new OnlineAppStatusBL(AppId);
        string strLocked = Status.ReturnMCALocked();
        if (strLocked == "Yes")
            return true;
        else
            return false;
    }

    protected bool PayrollLocked()
    {
        //Check whether the application is locked
        OnlineAppStatusBL Status = new OnlineAppStatusBL(AppId);
        string strLocked = Status.ReturnPayrollLocked();
        if (strLocked == "Yes")
            return true;
        else
            return false;
    }

    protected void lnkbtnUnlock_Click(object sender, EventArgs e)
    {
        try
        {
            OnlineAppBL App = new OnlineAppBL(AppId);
            int iRetVal = App.ResetLoginAttemptCount();
            if (iRetVal > 0)
                pnlLoginAttempts.Visible = false;
            DisplayMessage("Application unlocked");
        }//end try
        catch (Exception err)
        {
            CreateLog Log = new CreateLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "Unlock App from edit.aspx - " + err.Message);
            DisplayMessage("Error loading page. Please contact Technical Support.");
        }
    }

    #endregion

    #region ADD TO ACT
    protected void imgAddToACT_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            bool retVal = AddRecordToACT(AppId);
        }//end try
        catch (Exception err)
        {
            CreateLog Log = new CreateLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "Add To ACT - " + err.Message);
            DisplayMessage("Error adding information to act");
        }
    }

    //This function adds the record to ACT
    private bool AddRecordToACT(int AppId)
    {
        if (User.IsInRole("Admin") || User.IsInRole("Employee"))
        {
            if (AppId != 0)
            {
                //First check if the AppID exists in ACT! (as a Primary Record, denoted by TYPENUM = 0)
                ACTDataBL ACT = new ACTDataBL();
           
      
                bool bRetVal = ACT.CheckAppIDExists(AppId);//Checks if AppId exists in ACT
                if (bRetVal)
                    DisplayMessage("This App ID already exists in ACT!");
                else
                {
                    //Checks for all fields
                    ExportActBL ExportACT = new ExportActBL();
                    string strRetVal = ExportACT.CheckRecordExists(AppId);
                    if (strRetVal != "Success")
                    {
                        //If record exists in ACT, then display the confirm create new record panel
                        pnlConfirm.Visible = true;
                        lblErrorMessage.Text = strRetVal + System.Environment.NewLine;
                    }
                    else
                    {
                        //Add information to ACT
                        strRetVal = ExportACT.AddInfoToACT(AppId);
                        if (strRetVal.Trim() == "Add Successful")
                        {
                            DisplayMessage(strRetVal);

                            //Update last modified date in OnlineAppProcessing table
                            OnlineAppProcessingBL Processing = new OnlineAppProcessingBL(AppId);
                            bool retVal = Processing.UpdateLastSyncDate();

                            //Create log entry for this action
                            PartnerLogBL LogData = new PartnerLogBL();
                            retVal = LogData.InsertLogData(AppId, Convert.ToInt32(Session["AffiliateID"]), "Application Added to ACT!");
                        }
                        else
                            DisplayMessage(strRetVal);
                    }//end checkrecordexists
                }//end check AppId
            }//End If AppID not 0
        }//end if user is in role
        return true;
    }//end function AddRecordToACT

    protected void btnCreateRecordYes_Click(object sender, EventArgs e)
    {
        try
        {
            pnlConfirm.Visible = false;
            //Add Record information to ACT on create new record confirmation
            OtherInfo otherInfo = new OtherInfo(AppId);
            bool retValUploadDate = otherInfo.UpdatePrevUploadDate();

            ExportActBL ExportedAct = new ExportActBL();
            string strRetVal = ExportedAct.AddInfoToACT(AppId);
            if (strRetVal.Trim() == "Add Successful")
            {
                DisplayMessage(strRetVal);
                //Update last modified date in OnlineAppProcessing table
                OnlineAppProcessingBL UpdateDate = new OnlineAppProcessingBL(AppId);
                bool retVal = UpdateDate.UpdateLastSyncDate();

                //Create log entry for this action
                PartnerLogBL LogData = new PartnerLogBL();
                retVal = LogData.InsertLogData(AppId, Convert.ToInt32(Session["AffiliateID"]), "Application Added to ACT!");
            }
            else
                DisplayMessage(strRetVal);
        }//end try
        catch (Exception err)
        {
            CreateLog Log = new CreateLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "Add To ACT! Create Record Confirm - " + err.Message);
            DisplayMessage("Error adding record to ACT!");
        }
    }

    protected void btnCreateRecordNo_Click(object sender, EventArgs e)
    {
        pnlConfirm.Visible = false;
    }

    #endregion

    #region UPDATE IN ACT

    protected void imgUpdateInACT_Click(object sender, ImageClickEventArgs e)
    {
        bool retVal = UpdateInACT(AppId);
    }

    //CALLED BY ExportActBL.AddIntoToACT, ExportActBL.UpdateAct
    public bool InsertUpdatebillingMethod(string ContactID, string Billing)
    {
        SqlConnection ConnACT = new SqlConnection(ConnStringACT);
        try
        {
            SqlCommand cmd = new SqlCommand("sp_InsertUpdatebillingMethod", ConnACT);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter pContactID = cmd.Parameters.Add("@ContactID", SqlDbType.VarChar);
            SqlParameter pBilling = cmd.Parameters.Add("@billing", SqlDbType.VarChar);

            pContactID.Value = ContactID;
            pBilling.Value = Billing;

            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            ConnACT.Close();
            ConnACT.Dispose();
        }//end try
        catch (SqlException err)
        {
            throw err;
        }
        finally
        {
            ConnACT.Close();
            ConnACT.Dispose();
        }
        return true;
    }//end function InsertUpdateContactEmail

    public DataSet GetACTEditDate(string ContactID, DateTime OnlineAppEditDate)
    {
        ACTDataDL ACTData = new ACTDataDL();
        DataSet ds = ACTData.GetACTEditDate(ContactID, OnlineAppEditDate);
        return ds;

    }

    #region UPDATE ACT
    //This function Update online application (not including Card Number) information in ACT!
    //CALLED BY Edit.aspx

       public string UpdateAct(int AppId, int partnerID, string Platform)
        {




            OnlineAppACTFieldsTableAdapter OnlineAppACTFieldsAdapter = new OnlineAppACTFieldsTableAdapter();
            PartnerDS.OnlineAppACTFieldsDataTable dt = OnlineAppACTFieldsAdapter.GetData(Convert.ToInt16(AppId));


            string strComplianceFee = "";

            OnlineAppBL Compliance = new OnlineAppBL(AppId);

            strComplianceFee = Compliance.GetComplianceFee();



            bool retVal = false;
            if (dt.Rows.Count > 0)
            {
                retVal = true;
                //Update last sync date in OnlineAppNewApp
                OnlineAppBL App = new OnlineAppBL(AppId);


                //Insert info in TBL_CONTACT in ACT
                string P1FullName = "";
                if (dt[0].P1MidName == "")
                    P1FullName = dt[0].P1FirstName + " " + dt[0].P1LastName;
                else
                    P1FullName = dt[0].P1FirstName + " " + dt[0].P1MidName + " " + dt[0].P1LastName;
                string CreateDate = DateTime.Now.ToString();


                //Get ContactID for the record to be updated
                ACTDataDL ACT = new ACTDataDL();
                //Get ContactID that was just inserted
                string ContactID = ACT.ReturnContactID(AppId);
                if (ContactID == "")
                    return "Contact ID not found";
                
                OnlineAppDL onlineAppData = new OnlineAppDL();
                string strOnlineAppEditDate = onlineAppData.GetOnlineAppEditDate(AppId);
                string strOnlineAppLastSynchDate = onlineAppData.GetOnlineAppLastSynchDate(AppId);
                DateTime onlineAppUpdateDate;

                DateTime OnlineAppEditDate = Convert.ToDateTime(strOnlineAppEditDate);
                DateTime OnlineAppLastSynchDate = Convert.ToDateTime(strOnlineAppLastSynchDate);

                if (OnlineAppLastSynchDate >= OnlineAppEditDate)
                {
                    onlineAppUpdateDate = OnlineAppLastSynchDate;
                }
                else {
                    onlineAppUpdateDate = OnlineAppEditDate;
                }

                DataSet dsACTEditDate = GetACTEditDate(ContactID, onlineAppUpdateDate);

                


                //if (dsACTEditDate.Tables[0].Rows.Count == 0)
                //if ((dsACTEditDate == null) || dsACTEditDate.Tables.Count == 0)
                {
                    //Insert information into Backup Table before updating
                    ACT.InsertActRecordBackup(ContactID);

                    

                    retVal = ACT.AddDataContact(CreateDate, dt[0].FirstName + " " + dt[0].LastName, dt[0].LegalStatus,
                    dt[0].FirstName, dt[0].LastName, dt[0].P1FirstName, dt[0].P1LastName,
                    dt[0].P1MidName, dt[0].P1Title, dt[0].Title,
                    dt[0].CompanyName, dt[0].DBA,
                    dt[0].COMPANYWEBADDRESS, dt[0].ReferredBy,
                    dt[0].AffiliateReferral, dt[0].SalesRep,
                    dt[0].Gateway, dt[0].Processor,
                    dt[0].YIB, dt[0].MIB,
                    dt[0].YABL, dt[0].MABL,
                    dt[0].BusHours, dt[0].NumOfLocs,
                    dt[0].NumOfDaysProdDel, dt[0].ProdServSold,
                    dt[0].AddlComments, dt[0].Bankruptcy,
                    dt[0].MAAddr1, dt[0].MAAddr2,
                    dt[0].MACity, dt[0].MAState,
                    dt[0].MAZip, dt[0].MACountry,
                    dt[0].FedTaxID, Platform,
                    dt[0].AnnualFee, dt[0].P1SocialSecurity,
                    dt[0].P1OwnPct, dt[0].P1LivingStatus,
                    dt[0].P1LOR, dt[0].P1DLNum,
                    dt[0].P1DLState, dt[0].P1DLExpDate,
                    dt[0].P1DOB, dt[0].RefundPolicy,
                    dt[0].BankName, dt[0].BankCity,
                    dt[0].BankState, dt[0].BankZip,
                    dt[0].RoutingNum, dt[0].CheckingAcctNum,
                    dt[0].CustServFee, dt[0].MonMin, dt[0].InternetStmt,
                    dt[0].DiscQNP, dt[0].DiscMQ,
                    dt[0].DiscNQ, dt[0].AmexDiscRateQual, dt[0].AmexDiscRateMidQual, dt[0].AmexDiscRateNonQual, dt[0].DiscQD,
                    dt[0].DebitQualNP, dt[0].DebitMidQual, dt[0].DebitNonQual,
                    dt[0].DiscQP, dt[0].TransFee,
                    dt[0].RetrievalFee, dt[0].VoiceAuth,
                    dt[0].BatchHeader, dt[0].AVS,
                    dt[0].NBCTFee, dt[0].CBFee,
                    Convert.ToInt32(dt[0].AcctType),
                    dt[0].MonVol, dt[0].AvgTicket,
                    dt[0].JCBNum, dt[0].AmexNum,
                    dt[0].DiscoverNum,
                    dt[0].PctSwp, dt[0].PctKWI,
                    dt[0].PctKWOI, dt[0].PctRet,
                    dt[0].PctRest, dt[0].PctServ,
                    dt[0].PctMail, dt[0].PctInt,
                    dt[0].PctOth, dt[0].GWMonFee,
                    dt[0].GWTransFee, dt[0].GWSetupFee,
                    dt[0].ProcBCBefore, dt[0].CTMFMatch,
                    dt[0].MerchantStatus, dt[0].GatewayStatus,
                    dt[0].MerchantID, dt[0].MerchantNum,
                    dt[0].RepNum, dt[0].MCCCategoryCode,
                    dt[0].PayrollType, dt[0].Payroll, dt[0].PayrollStatus, dt[0].MCAType, dt[0].ContractTerm, AppId);

                    InsertUpdatebillingMethod(ContactID, dt[0].LeaseCompany);
                    
                    if (!Convert.IsDBNull(dt[0].DiscountPaid))
                    {
                        ACT.UpdateDiscountPaid(dt[0].DiscountPaid, AppId);
                    }

                    ACT.UpdateComplianceFee(strComplianceFee, AppId);

                    if (!Convert.IsDBNull(dt[0].PinDebitDisount))
                    {
                        ACT.UpdatePinDebitDiscount(dt[0].PinDebitDisount, AppId);
                    }

                     /*if (!Convert.IsDBNull(dt[0].MCAAmount))
                     {
                         ACT.UpdateMCAAmount(Convert.ToInt32(dt[0].MCAAmount), AppId);
                     }*/
                    

                    //returns 1 if multiple records with same appid is found
                    if (!retVal)
                        return "Multiple records with the same App ID found in ACT! Update failed.";

                    //Insert info in secondary contact table in ACT
                    string FullName = FullName = dt[0].FirstName + " " + dt[0].LastName;

                    //Update Signup Person as a Secondary Contact in ACT (Principal is regarded as the Primary in ACT!)
                    /*retVal = ACT.InsertUpdateSecContactInfo(ContactID, AppId, "Signup Contact",
                        dt[0].Title, FullName, dt[0].FirstName,
                        dt[0].LastName, dt[0].SignupEmail,
                        dt[0].Phone, dt[0].PhoneExt, dt[0].SecMobilePhone, dt[0].SecHomePhone);*/

                    if (!retVal)
                        return "Secondary Contact cannot be added.";

                    //Insert or Update Contact's EMAIL IN THE EMAIL TABLE 	
                    retVal = ACT.InsertUpdateContactEmail(ContactID, dt[0].SignupEmail);

                    

                    //INSERT p1's EMAIL IN THE EMAIL TABLE 	
                    retVal = ACT.InsertUpdatep1Email(ContactID, dt[0].P1Email);

                    //CreateLog Log = new CreateLog();
                    //Log.ErrorLog(Server.MapPath("~/ErrorLog"), "Update In ACT! - Test1 ");

                    //INSERT BUSINESS ADDRESS
                    retVal = ACT.InsertUpdateBusinessAddress(ContactID, dt[0].LINE1, dt[0].LINE2, dt[0].City, dt[0].State, dt[0].POSTALCODE, dt[0].COUNTRYNAME);

                    //INSERT P1 HOME ADDRESS
                    ACTDataDL AddP1HomeAddress = new ACTDataDL();
                    retVal = AddP1HomeAddress.InsertUpdateP1HomeAddress(ContactID, dt[0].P1HomeLINE1, dt[0].P1HomeLINE2, dt[0].P1HomeCITY, dt[0].P1HomeSTATE, dt[0].P1HomePOSTALCODE, dt[0].P1HomeCOUNTRYNAME);

                    //Insert (P1's) BUSINESS PHONE NUMBER
                    //retVal = ACT.InsertUpdateP1BusinessPhone(ContactID, dt[0].BUSINESS_PHONE, dt[0].Business_PhoneEXT);
                    if (!retVal)
                        return "Cannot insert P1 Business Phone Number.";

                    //**************************** Insert all phone numbers ************************
                    //Insert CUSTOMER SERVICE PHONE NUMBER


                    int iRetVal = ACT.InsertUpdatePhone("CustServ", ContactID, dt[0].CustServPhone);

                    //Insert Fax
                    iRetVal = ACT.InsertUpdatePhone("Fax", ContactID, dt[0].Fax);

                    //Insert HOME PHONE NUMBER
                    iRetVal = ACT.InsertUpdatePhone("Home", ContactID, dt[0].HOME_PHONE);

                    //Insert P1 HOME PHONE NUMBER
                    iRetVal = ACT.InsertUpdatePhone("P1Home", ContactID, dt[0].HOME_PHONE);

                    //Insert PRINCIPAL 2 HOME PHONE NUMBER
                     iRetVal = ACT.InsertUpdatePhone("P2Home", ContactID, dt[0].P2HomePhone);

                    //Insert Mobile PHONE NUMBER
                     iRetVal = ACT.InsertUpdatePhone("Mobile", ContactID, dt[0].SecMobilePhone);

                    //Insert P1 Mobile PHONE NUMBER
                    iRetVal = ACT.InsertUpdatePhone("P1Mobile", ContactID, dt[0].P1MobilePhone);

                    //Insert P2 Mobile PHONE NUMBER
                     iRetVal = ACT.InsertUpdatePhone("P2Mobile", ContactID, dt[0].P2MobilePhone);

                    //Insert BANK PHONE NUMBER
                     iRetVal = ACT.InsertUpdatePhone("Bank", ContactID, dt[0].BankPhone);


                    try {
                        bool iRetValbool = ACT.InsertUpdatePricingStructure(ContactID, dt[0].BillingMethod);
                    } catch(Exception err) {
                        CreateLog Log = new CreateLog();
                        Log.ErrorLog(Server.MapPath("~/ErrorLog"), "Update In ACT! - Error updating billing method - " + err.Message);
                        // DisplayMessage("Error Updating in ACT!"); // silently fail
                    }
                    
                    
                    //************************************************************

                    //Add Notes Into ACT
                    /*OnlineAppNotesTableAdapter OnlineAppNotesAdapter = new OnlineAppNotesTableAdapter();
                    PartnerDS.OnlineAppNotesDataTable dtNotes = OnlineAppNotesAdapter.GetData(Convert.ToInt16(AppId));
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtNotes.Rows.Count; i++)
                        {
                            ACT.InsertNotes(dtNotes[i].NoteID.ToString(), ContactID,
                                dtNotes[i].ActUserID.ToString(), dtNotes[i].NoteText.Replace("'", ""), dtNotes[i].DateRecorded);
                        }
                    }*/

                    if (AppId != 1)
                    {
                        //Add Reminder scheduled by Jay for Jay into ACT
                        string strRegarding = "Online App Updated. Please follow up.";
                        retVal = ACT.InsertReminder(ContactID, strRegarding);
                    }

                    /*if (!Convert.IsDBNull(dt[0].MCAAmount))
                    {
                        MCAAmount = Convert.ToDecimal(dt[0].MCAAmount);
                    }
                    else {
                        MCAAmount = Convert.ToDecimal(dt[0].MCAAmount);
                    }*/

                    /*if (!(Convert.IsDBNull(dt[0].MCAAmount)))
                    {
                        MCAAmount = Convert.ToInt32(dt[0].MCAAmount);
                    }
                    else {
                        MCAAmount = dt[0].MCAAmount;
                    }*/

                    retVal = ACT.InsertUpdateActCust(ContactID, dt[0].AddlComments,
                        dt[0].OtherRefund, dt[0].NameOfPrevProc,
                        dt[0].FormerMerchantNums, dt[0].ReasonLeavingProc,
                        dt[0].P2LastName, dt[0].P2FirstName,
                        dt[0].P2Title, dt[0].P2OwnPct,
                        dt[0].P2LOR, dt[0].P2HomeAddr1,
                        dt[0].P2HomeCity, dt[0].P2HomeState,
                        dt[0].P2HomeZip, dt[0].P2HomeCountry,
                        dt[0].P2SocialSecurity, dt[0].P2LivingStatus,
                        dt[0].P2DLNum, dt[0].P2DLState,
                        dt[0].P2DLExpDate, dt[0].P2DOB,
                        dt[0].BankAddr, dt[0].TerminalID,
                        dt[0].LoginID, dt[0].BankIDNum,
                        dt[0].AgentBankIDNum, dt[0].AgentChainNum,
                        dt[0].MCCCategoryCode, dt[0].StoreNum,
                        dt[0].MaxTicket,
                        Convert.ToBoolean(dt[0].OnlineDebit), Convert.ToBoolean(dt[0].GiftCard),
                        Convert.ToBoolean(dt[0].CheckService), dt[0].CheckServiceName,
                        Convert.ToBoolean(dt[0].EBT), Convert.ToBoolean(dt[0].MerchantFunding),
                        dt[0].USDANum, dt[0].DebitMonFee,
                        dt[0].DebitTransFee, dt[0].CGMonFee, dt[0].CGTransFee,
                        dt[0].CGMonMin, dt[0].CGDiscRate, dt[0].GiftCardType, dt[0].GCMonFee, dt[0].GCTransFee,
                        dt[0].EBTMonFee, dt[0].EBTTransFee, dt[0].WirelessAccess,
                        dt[0].WirelessTransFee, Convert.ToBoolean(dt[0].Interchange),
                        Convert.ToBoolean(dt[0].Assessments), dt[0].RollingReserve, dt[0].Lease, dt[0].LeaseCompany,
                        dt[0].LeasePayment, dt[0].LeaseTerm, Convert.ToString(dt[0].MCAAmount), Convert.ToString(dt[0].MCAStatus));

                    if (!retVal)
                        return "Cannot add information to ActCust table.";

                    //INSERT P2's EMAIL IN THE EMAIL TABLE         
                    retVal = ACT.InsertUpdateP2Email(ContactID, dt[0].P2Email);

                    //Insert note stating record for updated
                    //ACT.AddUpdateNoteToACT(ContactID, "Merchant Information Updated from the Partner Portal");

                    //Check to see which fields were changed to record histories                
                    //Compare the two entries in the tables (via a View) to see which fields will record histories

                    //Get the Preupdated Contact Information
                    DataSet dsPostUpdate = ACT.GetActRecord(ContactID);
                    DataSet dsPreUpdate = ACT.GetActRecordBackup(ContactID);

                    //number of columns in the Act Record View
                    int colCount = dsPreUpdate.Tables[0].Columns.Count;

                    DataRow drPreUpdate = dsPreUpdate.Tables[0].Rows[0];
                    DataRow drPostUpdate = dsPostUpdate.Tables[0].Rows[0];
                    String PrevValue, ColNamePre, NewValue, ColNamePost;

                    //Loop through every column in the Pre-updated and Post Act Record
                    for (int i = 0; i < colCount; i++)
                    {
                        DataColumn dcPre = dsPreUpdate.Tables[0].Columns[i];
                        DataColumn dcPost = dsPostUpdate.Tables[0].Columns[i];
                        ColNamePre = dcPre.ColumnName;
                        ColNamePost = dcPost.ColumnName;

                        //If the column NAMES in the data sets are the same
                        if (ColNamePre == ColNamePost)
                        {
                            //PartnerLogBL LogData = new PartnerLogBL();
                            //string PortalUserID = LogData.ReturnPortalUserID(Convert.ToInt32(Session["AffiliateID"]));

                            PrevValue = drPreUpdate[i].ToString();
                            NewValue = drPostUpdate[i].ToString();
                            //if the fields do not contain the same VALUE
                            if (PrevValue.ToLower().Replace(" ", "") != NewValue.ToLower().Replace(" ", ""))
                            {
                                //Record a Field Change History in ACT
                                ACT.InsertHistoryFieldChange(ContactID, ColNamePost, PrevValue, NewValue, partnerID);
                            }
                        }
                        //Delete the data for this Contact in the Backup Table created in the Update
                        ACT.DeleteActBackup(ContactID);
                    }

                    retVal = LogData.InsertLogData(AppId, Convert.ToInt32(HttpContext.Current.Session["AffiliateID"]), "Application Updated in ACT!");

                    App.UpdateLastSyncDate();
                }/*
                else { 
                ACTOnlineAppFieldsTableAdapter ACTOnlineAppFieldsAdapter = new ACTOnlineAppFieldsTableAdapter();
                PartnerDS.ACTOnlineAppFieldsDataTable dtACT = ACTOnlineAppFieldsAdapter.GetData(new Guid(ContactID));
                 //Get the Data from the ACT record

                if (dtACT.Rows.Count > 0)
                {
                    string Email = dtACT[0].EMail;
                    string FirstName = dtACT[0].FIRSTNAME;
                    string LastName = dtACT[0].LASTNAME;
                    string Phone = dtACT[0].Phone;
                    string OtherReferral = dtACT[0].OtherReferral;
                    int ReferralID = 0;
                    int NewAppId = AppId;
                    if (dtACT[0].ReferralID != "")
                        ReferralID = Convert.ToInt32(dtACT[0].ReferralID);

                    int PID = 0;

                    NewAppInfo AppInfo = new NewAppInfo(AppId);
                    string Status = AppInfo.ReturnStatus();
                    string StatusGW = AppInfo.ReturnStatusGW();

                    CompanyInfo Company1 = new CompanyInfo(AppId);
                    int companyCom = Company1.CheckCompanyComplete();
                    bool boolCompanyCom = false;
                    if(companyCom == 1)
                    {
                        boolCompanyCom = true;
                    }

                    BusinessInfo Business1 = new BusinessInfo(AppId);
                    int busCom = Business1.CheckBusinessComplete("Merchant");
                    int busComGW = Business1.CheckBusinessComplete("Gateway");
                    bool boolBusCom = false;
                    bool boolBusComGW = false;

                    if(busCom == 1)
                    {
                        boolBusCom = true;
                    }

                    if (busComGW == 1)
                    {
                        boolBusComGW = true;
                    }

                    PrincipalInfo Principal11 = new PrincipalInfo(AppId);
                    int principalCom = Principal11.CheckPrincipalComplete("Merchant");
                    int principalComGW = Principal11.CheckPrincipalComplete("Gateway");
                    bool boolprincipalCom = false;
                    bool boolprincipalComGW = false;

                    if (principalCom == 1)
                    {
                        boolprincipalCom = true;
                    }

                    if (principalComGW == 1)
                    {
                        boolprincipalComGW = true;
                    }

                    Principal2Info Principal22 = new Principal2Info(AppId);
                    int principal2Com = Principal22.CheckPrincipal2Complete("Merchant");
                    bool boolprincipal2Com = false;

                    if (principal2Com == 1)
                    {
                        boolprincipal2Com = true;
                    }

                    CardPCT Card = new CardPCT(AppId);
                    int cardPCTCom = Card.CheckCardPCTComplete();
                    bool boolcardPCTCom = false;

                    if (cardPCTCom == 1)
                    {
                        boolcardPCTCom = true;
                    }


                    OtherInfo other = new OtherInfo(AppId);
                    int otherCom = other.CheckOtherInfoComplete("Merchant");
                    bool boolotherCom = false;

                    if (otherCom == 1)
                    {
                        boolotherCom = true;
                    }

                    BankingInfo Banking1 = new BankingInfo(AppId);
                    int bankCom = Banking1.CheckBankingComplete("Merchant");
                    int bankComGW = Banking1.CheckBankingComplete("Gateway");
                    bool boolbankCom = false;
                    bool boolbankComGW = false;

                    if (bankCom == 1)
                    {
                        boolbankCom = true;
                    }

                    if (bankComGW == 1)
                    {
                        boolbankComGW = true;
                    }


                    ReprogramInfo Reprogram = new ReprogramInfo(AppId);
                    int ReprogramComGW = Reprogram.CheckReprogramComplete();
                    bool boolReprogramComGW = false;

                    if (ReprogramComGW == 1)
                    {
                        boolReprogramComGW = true;
                    }

                    //Insert Data in OnlineAppNewApp  
                    //First get repnum based on Rep Name in the ACT Record
                    RepInfoDL Rep = new RepInfoDL();
                    string strRepNumber = Rep.ReturnMasterNum(dtACT[0].RepName);
                    OnlineAppDL OnlineApp = new OnlineAppDL();

                    OnlineAppClassLibrary.CompanyInfo Company = new OnlineAppClassLibrary.CompanyInfo(NewAppId);
                    retVal = Company.UpdateCompanyInfo(dtACT[0].Company, dtACT[0].CustServPhone,
                        dtACT[0].DBA, dtACT[0].CompanyAddress, dtACT[0].CompanyAddress2,
                        dtACT[0].CompanyCity, dtACT[0].CompanyState, "",
                        dtACT[0].CompanyZip, dtACT[0].CompanyCountry, dtACT[0].YABL, dtACT[0].MABL,
                        dtACT[0].BusinessHours, dtACT[0].BusinessPhone, dtACT[0].BusinessPhoneExt,
                        dtACT[0].BusinessFax, dtACT[0].Website, boolCompanyCom);

                    if (!retVal)
                        return "Could not insert data in OnlineAppCompanyInfo.";

                    //Insert data in OnlineAppCardPCT
                    OnlineAppClassLibrary.CardPCT CardPCT = new OnlineAppClassLibrary.CardPCT(NewAppId);
                    retVal = CardPCT.UpdateCardPCT(dtACT[0].Retail,
                        dtACT[0].Restaurant, dtACT[0].MailOrder, dtACT[0].Internet,
                        dtACT[0].Swiped, dtACT[0].KeyedWith, dtACT[0].KeyedWithout,
                        dtACT[0].Service, dtACT[0].Others, boolcardPCTCom);

                    if (!retVal)
                        return "Could not insert data in OnlineAppCardPCT.";

                    //Insert data in OnlineAppBusinessInfo
                    OnlineAppClassLibrary.BusinessInfo Business = new OnlineAppClassLibrary.BusinessInfo(NewAppId);
                    Business.UpdateBusinessInfo(dtACT[0].BillingAddress,
                        dtACT[0].BillingAddress2, dtACT[0].BillingCity, dtACT[0].BillingState,
                        "", dtACT[0].BillingZip, dtACT[0].BillingCountry, dtACT[0].TaxID,
                        dtACT[0].YearsInBusiness, dtACT[0].MonthsinBusiness, dtACT[0].NumberOfLocations,
                        dtACT[0].TypeOwnership, dtACT[0].TypeProduct, dtACT[0].NumDaysDelivered,
                        dtACT[0].AddlComments, dtACT[0].RefundID, dtACT[0].OtherRefund,
                        dtACT[0].FiledBankruptcy, dtACT[0].Processed, dtACT[0].PrevProcessor, "",
                        dtACT[0].PrevMerchantAcctNo, 0, dtACT[0].ReasonForLeaving, dtACT[0].Terminated, boolBusCom);

                    if (!retVal)
                        return "Could not insert data in OnlineAppBusinessInfo.";

                    //Insert data in OnlineAppPrincipalInfo
                    OnlineAppClassLibrary.PrincipalInfo Principal1 = new OnlineAppClassLibrary.PrincipalInfo(NewAppId);
                    string hasSecondPrincipal = "No";
                    if (dtACT[0].P1OwnershipPercent.ToString().Trim() != "100")
                        hasSecondPrincipal = "Yes";
                    retVal = Principal1.UpdatePrincipal1Info(dtACT[0].P1FirstName,
                        dtACT[0].P1LastName, dtACT[0].P1MidName, Email, dtACT[0].P1Title,
                        dtACT[0].P1Address, "", dtACT[0].P1State, dtACT[0].P1City,
                        dtACT[0].P1ZipCode, "", dtACT[0].P1Country, dtACT[0].P1YearsAtAddress, "",
                        dtACT[0].P1PhoneNumber, dtACT[0].P1MobilePhone, dtACT[0].P1DriversLicenseNo, dtACT[0].P1DriversLicenseState,
                        dtACT[0].P1DriversLicenseExpiry, dtACT[0].P1DOB, dtACT[0].P1LivingStatus, dtACT[0].P1OwnershipPercent,
                        dtACT[0].P1SSN, boolprincipalCom, hasSecondPrincipal);
                    if (!retVal)
                        return "Could not insert data in OnlineAppPrincipalInfo.";

                    //Insert data in OnlineAppPrincipal2Info
                    if (dtACT[0].P1OwnershipPercent.ToString().Trim() != "100")
                    {
                        OnlineAppClassLibrary.Principal2Info Principal2 = new OnlineAppClassLibrary.Principal2Info(NewAppId);
                        retVal = Principal2.UpdatePrincipal2Info(dtACT[0].P2FirstName,
                            dtACT[0].P2LastName, "", dtACT[0].P2Email, dtACT[0].P2Title,
                            dtACT[0].P2Address, "", dtACT[0].P2State, dtACT[0].P2City,
                            dtACT[0].P2ZipCode, "", dtACT[0].P2Country, dtACT[0].P2YearsAtAddress, "",
                            dtACT[0].P2PhoneNumber, dtACT[0].P2MobilePhone, dtACT[0].P2DriversLicenseNo, dtACT[0].P2DriversLicenseState,
                            dtACT[0].P2DriversLicenseExpiry, dtACT[0].P2DOB, dtACT[0].P2LivingStatus, dtACT[0].P2OwnershipPercent,
                            dtACT[0].P2SSN, boolprincipal2Com);

                        if (!retVal)
                            return "Could not insert data in OnlineAppPrincipal2Info.";
                    }

                    string CardPresent = "";
                    int SwipedPCT = 0;
                    if (dtACT[0].Swiped != "")
                        SwipedPCT = Convert.ToInt32(dtACT[0].Swiped);

                    if (SwipedPCT >= 70)
                        CardPresent = "CP";
                    else
                        CardPresent = "CNP";

                    if (!Convert.IsDBNull(dtACT[0].DiscountPaid))
                    {
                        if (Convert.ToString(dtACT[0].DiscountPaid) == "Monthly")
                        {
                            DiscountPaid = "Monthly";
                        }
                    }

                    //Insert data in OnlineAppProcessing only if a Processor is selected
                    if (dtACT[0].Processor.ToString().Trim() != "")
                    {
                        OnlineAppClassLibrary.ProcessingInfo Proc = new OnlineAppClassLibrary.ProcessingInfo(NewAppId);
                        retVal = Proc.UpdateProcessingInfo(dtACT[0].Processor, CardPresent,
                            dtACT[0].CustServFee, dtACT[0].InternetStmt, dtACT[0].TransactionFee, dtACT[0].DRQualPres,
                            dtACT[0].DRQualNP, dtACT[0].DRMidQual, dtACT[0].DRNonQual,
                            dtACT[0].DRQualDebit, dtACT[0].ChargebackFee, dtACT[0].RetrievalFee,
                            dtACT[0].VoiceAuth, dtACT[0].BatchHeader, dtACT[0].AVS,
                            dtACT[0].MonMin, dtACT[0].NBCTransFee, dtACT[0].AnnualFee,
                            dtACT[0].WirelessAccessFee, dtACT[0].WirelessTransFee,
                            "", "", dtACT[0].DebitMonFee,
                            dtACT[0].DebitTransFee, dtACT[0].CGMonFee,
                            dtACT[0].CGTransFee, dtACT[0].CGMonMin,
                            dtACT[0].CGDiscRate, dtACT[0].GCMonFee,
                            dtACT[0].GCTransFee, dtACT[0].EBTMonFee,
                            dtACT[0].EBTTransFee, DiscountPaid, ComplianceFee, "");

                        NewAppTable newAppTable = new NewAppTable();
                        newAppTable.setRatesUpdatedBit(NewAppId, true);

                        if (!retVal)
                            return "Could not insert data in OnlineAppProcessing.";

                        retVal = Proc.InsertUpdateCheckServiceName(dtACT[0].CheckService);

                        if (!retVal)
                            return "Could not insert Check Service.";

                        OnlineAppProcessingBL Processing = new OnlineAppProcessingBL(NewAppId);
                        Processing.UpdateOtherProcessing(Convert.ToBoolean(dtACT[0].Interchange), Convert.ToBoolean(dtACT[0].BillingAssessments), dtACT[0].RollingReserve.ToString());
                        //Set the Last Sync Date since it is an exported App
                        Processing.UpdateLastSyncDate();
                        if (!retVal)
                            return "Could not insert other Processing data in OnlineAppProcessing.";
                    }

                    if (dtACT[0].Gateway.ToString().Trim() != "")
                    {
                        OnlineAppClassLibrary.Gateway Gateway = new OnlineAppClassLibrary.Gateway(NewAppId);
                        retVal = Gateway.UpdateGatewayInfo(
                            dtACT[0].Gateway, dtACT[0].GatewayMonFee,
                            "", dtACT[0].GatewayTransFee);

                        if (!retVal)
                            return "Could not insert data in OnlineAppGateway.";
                    }

                    //Insert data in OnlineAppBankingInfo
                    OnlineAppClassLibrary.BankingInfo Banking = new OnlineAppClassLibrary.BankingInfo(NewAppId);
                    retVal = Banking.UpdateBankingInfo(dtACT[0].BankName, "",
                        dtACT[0].BankAddress, dtACT[0].BankZip, dtACT[0].BankCity,
                        dtACT[0].BankState, "", dtACT[0].CompanyCountry, dtACT[0].Company, dtACT[0].BankAccountNumber,
                        dtACT[0].BankRoutingNumber, dtACT[0].BankPhone, boolbankCom);
                    if (!retVal)
                        return "Could not insert data in OnlineAppBankingInfo.";

                    //Insert data in OnlineAppOtherInfo
                    string AmexApplied = "";
                    string AmexNum = "";
                    long result = 0;
                    if (Int64.TryParse(dtACT[0].AmexNum.ToString(), out result))
                    {
                        AmexNum = dtACT[0].AmexNum.ToString();
                        AmexApplied = "Yes - Existing";
                    }
                    else if ((dtACT[0].AmexNum.ToString().ToLower().Contains("yes")) || (dtACT[0].AmexNum.ToString().ToLower().Contains("submit")))
                    {
                        AmexApplied = "Yes";
                    }
                    else if ((dtACT[0].AmexNum.ToString().ToLower().Contains("opted")) || (dtACT[0].AmexNum.ToString().ToLower().Contains("declined")) || (dtACT[0].AmexNum.ToString().ToLower().Contains("cancelled")))
                    {
                        AmexApplied = "No";
                    }
                    else {
                        AmexApplied = "";
                    }

                    string DiscApplied = "";
                    string DiscNum = "";
                    if (Int64.TryParse(dtACT[0].DiscoverNum.ToString(), out result))
                    {
                        DiscNum = dtACT[0].DiscoverNum.ToString();
                        DiscApplied = "Yes - Existing";
                    }
                    else if ((dtACT[0].DiscoverNum.ToString().ToLower().Contains("yes")) || (dtACT[0].DiscoverNum.ToString().ToLower().Contains("submit")))
                        DiscApplied = "Yes";
                    else if ((dtACT[0].DiscoverNum.ToString().ToLower().Contains("opted")) || (dtACT[0].DiscoverNum.ToString().ToLower().Contains("declined")) || (dtACT[0].DiscoverNum.ToString().ToLower().Contains("cancelled")) || (dtACT[0].DiscoverNum.ToString().ToLower().Contains("International")) || (dtACT[0].DiscoverNum.ToString().ToLower().Contains("MAP")))
                        DiscApplied = "No";
                    else DiscApplied = "";

                    string JCBApplied = "";
                    string JCBNum = "";
                    if (Int64.TryParse(dtACT[0].JCBNum.ToString(), out result))
                    {
                        JCBNum = dtACT[0].JCBNum.ToString();
                        JCBApplied = "Yes - Existing";
                    }
                    else if ((dtACT[0].JCBNum.ToString().ToLower().Contains("yes")) || (dtACT[0].JCBNum.ToString().ToLower().Contains("submit")))
                        JCBApplied = "Yes";
                    else if ((dtACT[0].JCBNum.ToString().ToLower().Contains("opted")) || (dtACT[0].JCBNum.ToString().ToLower().Contains("declined")) || (dtACT[0].JCBNum.ToString().ToLower().Contains("cancelled")))
                        JCBApplied = "No";
                    else JCBApplied = "";

                    OnlineAppClassLibrary.OtherInfo OtherInfo = new OnlineAppClassLibrary.OtherInfo(NewAppId);
                    retVal = OtherInfo.UpdateOtherInfo(DiscApplied, AmexApplied, JCBApplied, DiscNum, AmexNum, "",
                        dtACT[0].MaxTicket, dtACT[0].AvgTicket, dtACT[0].MonVol, boolotherCom);

                    if (!retVal)
                        return "Could not insert data in OnlineAppOtherInfo.";

                    //Export Numbers to NBC Table
                    if ((dtACT[0].DiscoverNum != "") || (dtACT[0].AmexNum != "") || (dtACT[0].JCBNum != ""))
                    {
                        retVal = OnlineApp.InsertUpdateNBC(NewAppId.ToString(), dtACT[0].DiscoverNum,
                             dtACT[0].AmexNum, dtACT[0].JCBNum);
                    }
                    if (!retVal)
                        return "Could not insert data in OnlineAppNonBankcard.";

                    //If Platform Info Exists in Act, update Reprogram table
                    if ((dtACT[0].Platform != "") || (dtACT[0].VisaMasterNum != ""))
                    {
                        OnlineAppClassLibrary.ReprogramInfo RPG = new OnlineAppClassLibrary.ReprogramInfo(NewAppId);
                        RPG.UpdateReprogramInfo(dtACT[0].Platform,
                            "",//dt[0].VisaMasterNum - since two accounts will never have same merchant number
                            dtACT[0].MerchantID,
                            "", 
                            "", //PFLoginID
                            dtACT[0].BIN,
                            dtACT[0].AgentBankNum,
                            dtACT[0].AgentChainNum,
                            dtACT[0].MCCCode,
                            dtACT[0].StoreNum,
                            boolReprogramComGW, true);
                    }

                    //set the Last Sync Date for the App again after all tables are created
                    OnlineApp.UpdateLastSyncDate(NewAppId);

                    //Get Data from SalesOpps in ACT

                    ACTOnlineAppSalesOppsTableAdapter ACTOnlineAppSalesOppsAdapter = new ACTOnlineAppSalesOppsTableAdapter();
                    PartnerDS.ACTOnlineAppSalesOppsDataTable dtSalesOpps = ACTOnlineAppSalesOppsAdapter.GetDataByContact(new Guid(ContactID));
       
                    //Create a Sales Opp Object
                    SalesOppDL SalesOpp = new SalesOppDL();
                    if (dtSalesOpps.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtSalesOpps.Rows.Count; i++)
                        {
                            //Insert data in OnlineAppSalesOpps          
                            //Get RepNum Based on RepName
                            string strRepNum = Rep.ReturnMasterNum(dtSalesOpps[i].RepName);
                            retVal = SalesOpp.InsertUpdateSalesOpps(NewAppId.ToString(), dtSalesOpps[i].CODE,
                                dtSalesOpps[i].UNITPRICE, dtSalesOpps[i].UNITCOST, dtSalesOpps[i].QUANTITY,
                                dtSalesOpps[i].TerminalID, dtSalesOpps[i].SerialNumber, dtSalesOpps[i].STATUS,
                                "{" + dtSalesOpps[i].CREATEUSERID + "}", "{" + dtSalesOpps[i].ID + "}",
                                strRepNum, dtSalesOpps[i].Stage, dtSalesOpps[i].LastModified,
                                dtSalesOpps[i].CREATEDATE, dtSalesOpps[i].Reprogram, dtSalesOpps[i].PaymentMethod);
                            if (!retVal)
                                return "Could not insert data in OnlineAppSalesOpps.";
                        }                        
                    }//end if count not 0

                    retVal = LogData.InsertLogData(AppId, Convert.ToInt32(HttpContext.Current.Session["AffiliateID"]), "Application Updated from ACT!");

                    string strretVal = ExportACTStatus(NewAppId, Convert.ToInt32(dt[0].AcctType));
                }
                else
                    return "Record not found.";

                }*/

                return "Update Successful";
            }//end if row count not 0
            return "Update Unsuccessful";
        }//end function UpdateAct
        #endregion


    //This function updates the record in ACT
    private bool UpdateInACT(int AppId)
    {
        try
        {
            if (User.IsInRole("Admin") || User.IsInRole("Employee"))
            {
                if (AppId != 0)
                {
                    OtherInfo otherInfo = new OtherInfo(AppId);
                    bool retValUploadDate = otherInfo.UpdatePrevUploadDate();



                    //First check if the appid exists in act (as a Primary Record, denoted by TYPENUM = 0)
                    ACTDataBL ACT = new ACTDataBL();
                    int partnerID = Convert.ToInt32(Session["AffiliateID"]);
                    PartnerLogBL LogData = new PartnerLogBL();
                    //string PortalUserID = LogData.ReturnPortalUserID((Convert.ToInt32(Session["AffiliateID"])));
                    //lblPortalUIDMessage.Text = "The Portal UserID is " + PortalUserID;
                    bool retVal = ACT.CheckAppIDExists(AppId);
                    if (!retVal)
                        DisplayMessage("This application has not been added to ACT!. Please add it to ACT first.");
                    else
                    {
                        ExportActBL ExportACT = new ExportActBL();
                        //Update information in ACT

                        string Platform = "";

                        

                        if (!Convert.IsDBNull(lstPlatform))
                        {

                            
                            if (!Convert.IsDBNull(lstPlatform.SelectedItem))
                            {

                                CreateLog Log = new CreateLog();
                                Log.ErrorLog(Server.MapPath("~/ErrorLog"), "Update In ACT! - Test ");
                                Platform = Convert.ToString(lstPlatform.SelectedItem);
                            }
                        }



                        string strRetVal = UpdateAct(AppId, partnerID, Platform);
                        if (strRetVal.Trim() == "Update Successful")
                        {
                            //bool bRetVal = ExportACT.UpdateRatesInACT(AppId);
                            //if (bRetVal)
                            //{
                                DisplayMessage(strRetVal);
                                //Update the sync date in OnlineAppProcessing
                                OnlineAppProcessingBL OnlineAppProc = new OnlineAppProcessingBL(AppId);
                                retVal = OnlineAppProc.UpdateLastSyncDate();
                                //ExportACT.AddUpdateNoteToACT(AppId, "Application Updated from the Partner Portal");     
                                
                                //Create log entry for this action
                                //PartnerLogBL LogData = new PartnerLogBL();
                                //retVal = LogData.InsertLogData(AppId, Convert.ToInt32(Session["AffiliateID"]), "Application Updated in ACT!");
                                
                           // }
                        }
                        else
                            DisplayMessage(strRetVal);
                    }
                }//End If AppID
                return true;
            }//end if user is in role            
        }//end try
        catch (Exception err)
        {
            CreateLog Log = new CreateLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "Update In ACT! - " + err.Message);
            DisplayMessage("Error Updating in ACT!");
        }
        return false;
    }//end function UpdateInACT

    #endregion

    //Everything is updated by the Update all button, so update rates is not needed.
    #region UPDATE RATES
    /*
    //This function handles update rates button click event
    protected void imgUpdateRates_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (User.IsInRole("Admin") || User.IsInRole("Employee"))
            {
                //First check if the appid exists in act (as a Primary Record)
                ACTDataBL ACT = new ACTDataBL();
                bool retVal = ACT.CheckAppIDExists(AppId);
                if (!retVal)
                    DisplayMessage("This application has not been added to ACT!. Please add it to ACT first.");
                else
                {
                    //Get rates from OnlineAppProcessing based on AppId and update rates in ACT!
                    ExportActBL ExportACT = new ExportActBL();
                    OnlineAppBL OnlineApp = new OnlineAppBL(AppId);
                    OnlineAppProcessingBL OnlineAppProc = new OnlineAppProcessingBL(AppId);
                    retVal = ExportACT.UpdateRatesInACT(AppId);
                    if (retVal)
                    {
                        DisplayMessage("Rates Updated in ACT!");
                        //Update the sync date in OnlineAppProcessing
                        retVal = OnlineAppProc.UpdateLastSyncDate();
                        //Add action to Log table
                        PartnerLogBL LogData = new PartnerLogBL();
                        retVal = LogData.InsertLogData(AppId, Convert.ToInt32(Session["AffiliateID"]), "Application Rates Updated in ACT!");
                    }
                }//end if retval
            }//end if user is in role
            else
                DisplayMessage("You are not authorized to perform this operation");
        }//end try
        catch (Exception err)
        {
            CreateLog Log = new CreateLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "Update Rates - " + err.Message);
            DisplayMessage("Error updating rates in ACT!");
        }
    }*/
    #endregion

    #region DELETE RECORD

    protected void imgDelete_Click(object sender, ImageClickEventArgs e)
    {
        imgDelete.Visible = false;
        pnlDeleteConfirm.Visible = true;
    }

    //This function handles the delete YES button click event
    protected void btnDeleteYes_Click(object sender, EventArgs e)
    {
        try
        {
            if (User.IsInRole("Admin"))
            {
                //Delete app from partner portal database
                OnlineAppBL OnlineApp = new OnlineAppBL(AppId);
                int iRetVal = OnlineApp.DeleteAppInfo();
                if (iRetVal > 0)
                    DisplayMessage("Application Deleted Successfully. Please close this window.");
                else
                    DisplayMessage("Application cannot be deleted.");
                pnlDeleteConfirm.Visible = false;
                pnlACT.Visible = false;
                pnlDeleteApp.Visible = false;
                pnlPDF.Visible = false;
            }//end if user is in role            
        }//end try
        catch (Exception err)
        {
            CreateLog Log = new CreateLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "Delete Record - " + err.Message);
            DisplayMessage("Error deleting record");
        }
    }

    protected void btnDeleteNo_Click(object sender, EventArgs e)
    {
        pnlDeleteConfirm.Visible = false;
        imgDelete.Visible = true;
    }

    #endregion


    protected void btnCreateIPayXML_Click(object sender, EventArgs e)
    {
        Server.Transfer("iPayXML.aspx?AppId=" + AppId);
    }//end create ipayment XML button click

        // Unfinished code for creating the IMS XML
        
    #region CREATE PDF

    protected void imgCreatePDF_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            CreatePDF PDFFile = new CreatePDF(AppId);
            string strProcessor = PDFFile.ReturnProcessorName();
            
            MemoryStream mStream = new MemoryStream();
            string FileName = "";
            if (strProcessor.ToLower().Contains("sage"))
            {
                OAPDFBL SageData = new OAPDFBL(AppId);
                DataSet ds = SageData.GetSageData();
                DataRow dr = ds.Tables[0].Rows[0];
                //pnlSagePDF.Visible = true;

                //Check to ensure correct BETs are being used
                decimal midQualStep = Convert.ToDecimal(dr["DiscRateMidQual"].ToString().Trim());
                decimal nonQualStep = Convert.ToDecimal(dr["DiscRateNonQual"].ToString().Trim());
                if (dr["Interchange"].ToString() != "True")
                {
                    if ((midQualStep != 0.8m) || (nonQualStep != 2.05m))
                    {
                        if ((midQualStep != 1m) || (nonQualStep != 1.5m))
                        {
                            if ((midQualStep != 0.5m) || (nonQualStep != 1m))
                            {
                                DisplayMessage("Only the following combinations of MidQualSteps and NonQualSteps can be used: 0.80, 2.05; 1.00, 1.50; 0.50, 1.00. Please correct MidQual and NonQual rates.");
                                //pnlSagePDF.Visible = false;
                            }
                        }
                    }
                }
                /*if (Convert.ToDecimal(dr["ProcessPctSwiped"].ToString().Trim()) >= 70)
                    btnSageMOTO.Visible = false;
                else
                    btnSageMOTO.Visible = true;*/
                //pnlSagePDF.Visible = true;

                mStream = new MemoryStream();

                string strSageAppPath = "~/PDF/Paya Application.pdf";

                FileName = Server.MapPath(strSageAppPath);
                mStream = PDFFile.CreateSagePDF(FileName);

                if (mStream != null)
                {
                    //LogBL LogData = new LogBL(AppId);
                    //LogData.InsertLogData(AffiliateID, "Paya PDF Created");

                    PartnerLogBL LogData = new PartnerLogBL();
                    LogData.InsertLogData(AppId, Convert.ToInt32(Session["AffiliateID"]), "Paya PDF Created");

                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.ContentType = "application/pdf";
                    Response.AppendHeader("content-disposition", "filename=Paya Application.pdf");
                    Response.AppendHeader("Content-Length", mStream.GetBuffer().Length.ToString());
                    
                    Response.OutputStream.Write(mStream.GetBuffer(), 0, Convert.ToInt32(mStream.GetBuffer().Length));

                    Response.Flush();
                    Response.Close();
                }
                else
                    DisplayMessage("Data not found for this record.");
            }

            else if ((strProcessor.ToLower().Contains("intuit")) && (!(strProcessor.ToLower().Contains("QuickBooks"))))
            {
                OAPDFBL IPSData = new OAPDFBL(AppId);
                DataSet ds = IPSData.GetIPSData();
                DataRow dr = ds.Tables[0].Rows[0];
                if ((dr["Interchange"].ToString() == "True") || (dr["Assessments"].ToString() == "True"))
                    FileName = Server.MapPath("~/PDF/IPS Application Interchange.pdf");
                else
                    FileName = Server.MapPath("~/PDF/IPS Application.pdf");
                mStream = PDFFile.CreateIPSPDF(FileName);

                if (mStream != null)
                {
                    //LogBL LogData = new LogBL(AppId);
                    //LogData.InsertLogData(AffiliateID, "IPS PDF Created");

                    PartnerLogBL LogData = new PartnerLogBL();
                    LogData.InsertLogData(AppId, Convert.ToInt32(Session["AffiliateID"]), "IPS PDF Created");

                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.ContentType = "application/pdf";
                    Response.AppendHeader("content-disposition", "filename=IPS Application.pdf");
                    Response.AppendHeader("Content-Length", mStream.GetBuffer().Length.ToString());
                    Response.OutputStream.Write(mStream.GetBuffer(), 0, Convert.ToInt32(mStream.GetBuffer().Length));

                    Response.Flush();
                    Response.Close();
                }
                else
                    DisplayMessage("Data not found for this record.");
            }
            else if (strProcessor.ToLower().Contains("ipayment"))
            {
                OAPDFBL iPayPDFData = new OAPDFBL(AppId);
                DataSet ds = iPayPDFData.GetIPaymentData();
                DataRow dr = ds.Tables[0].Rows[0];

                string striPayAppPath = "~/PDF/iPayment application.pdf";

                FileName = Server.MapPath(striPayAppPath);
                mStream = PDFFile.CreateIPayPDF(FileName);

                if (mStream != null)
                {
                    //LogBL LogData = new LogBL(AppId);
                    //LogData.InsertLogData(AffiliateID, "iPayment PDF Created");

                    PartnerLogBL LogData = new PartnerLogBL();
                    LogData.InsertLogData(AppId, Convert.ToInt32(Session["AffiliateID"]), "iPayment PDF Created");

                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.ContentType = "application/pdf";
                    Response.AppendHeader("content-disposition", "filename=iPayment Application.pdf");
                    Response.AppendHeader("Content-Length", mStream.GetBuffer().Length.ToString());
                    Response.OutputStream.Write(mStream.GetBuffer(), 0, Convert.ToInt32(mStream.GetBuffer().Length));

                    Response.Flush();
                    Response.Close();
                }
                else
                    DisplayMessage("Data not found for this record.");
            }
            else if (strProcessor.ToLower().Contains("optimal-merrick"))
            {
                FileName = Server.MapPath("~/PDF/CNP_Merrick.pdf");
                mStream = PDFFile.CreateMerrickPDF(FileName);

                if (mStream != null)
                {
                    //LogBL LogData = new LogBL(AppId);
                    //LogData.InsertLogData(AffiliateID, "Merrick PDF Created");

                    PartnerLogBL LogData = new PartnerLogBL();
                    LogData.InsertLogData(AppId, Convert.ToInt32(Session["AffiliateID"]), "Merrick PDF Created");

                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.ContentType = "application/pdf";
                    Response.AppendHeader("content-disposition", "filename=Merrick Application.pdf");
                    Response.AppendHeader("Content-Length", mStream.GetBuffer().Length.ToString());
                    Response.OutputStream.Write(mStream.GetBuffer(), 0, Convert.ToInt32(mStream.GetBuffer().Length));

                    Response.Flush();
                    Response.Close();
                }
                else
                    DisplayMessage("Data not found for this record.");
            }
            else if (strProcessor.ToLower().Contains("canada"))
            {

                FileName = Server.MapPath("~/PDF/Optimal_Canada_App.pdf");
                mStream = PDFFile.CreateCanadaPDF(FileName);

                if (mStream != null)
                {
                    //LogBL LogData = new LogBL(AppId);
                    //LogData.InsertLogData(AffiliateID, "Optimal Canada PDF Created");

                    PartnerLogBL LogData = new PartnerLogBL();
                    LogData.InsertLogData(AppId, Convert.ToInt32(Session["AffiliateID"]), "Optimal Canada PDF Created");

                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.ContentType = "application/pdf";
                    Response.AppendHeader("content-disposition", "filename=Optimal CA Application.pdf");
                    Response.AppendHeader("Content-Length", mStream.GetBuffer().Length.ToString());
                    Response.OutputStream.Write(mStream.GetBuffer(), 0, Convert.ToInt32(mStream.GetBuffer().Length));

                    Response.Flush();
                    Response.Close();
                }
                else
                    DisplayMessage("Data not found for this record.");
            }
            else if ((strProcessor.ToLower().Contains("international")) || (strProcessor.ToLower().Contains("cal")))
            {
                FileName = Server.MapPath("~/PDF/CAL_Application_NA.pdf");
                mStream = PDFFile.CreateInternationalPDF(FileName);

                if (mStream != null)
                {
                    //LogBL LogData = new LogBL(AppId);
                    //LogData.InsertLogData(AffiliateID, "Optimal CAL PDF Created");

                    PartnerLogBL LogData = new PartnerLogBL();
                    LogData.InsertLogData(AppId, Convert.ToInt32(Session["AffiliateID"]), "Optimal CAL PDF Created");

                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.ContentType = "application/pdf";
                    Response.AppendHeader("content-disposition", "filename=Optimal Cal App.pdf");
                    Response.AppendHeader("Content-Length", mStream.GetBuffer().Length.ToString());
                    Response.OutputStream.Write(mStream.GetBuffer(), 0, Convert.ToInt32(mStream.GetBuffer().Length));

                    Response.Flush();
                    Response.Close();
                }
                else
                    DisplayMessage("Data not found for this record.");
            }
            else if (strProcessor.ToLower().Contains("cardconnect"))
            {
                OAPDFBL ChaseData = new OAPDFBL(AppId);
                DataSet ds = ChaseData.GetChaseData();
                DataRow dr = ds.Tables[0].Rows[0];
                if ((Convert.ToBoolean(dr["Interchange"])) || (Convert.ToBoolean(dr["Assessments"])))
                {
                    btnChaseFS3Tier.Visible = false;
                    btnChaseFSInterchangePlus.Visible = false;
                    btnChaseFSInterchangePlus_Click(sender, e);
                }
                else
                {
                    btnChaseFS3Tier.Visible = false;
                    btnChaseFSInterchangePlus.Visible = false;
                    btnChaseFS3Tier_Click(sender, e);
                }
                //pnlChasePDF.Visible = true;
            }
            else if (strProcessor.ToLower().Contains("kitts"))
            {
                FileName = Server.MapPath("~/PDF/St_Kitts_Application.pdf");
                mStream = PDFFile.CreateStKittsPDF(FileName);

                if (mStream != null)
                {
                    //LogBL LogData = new LogBL(AppId);
                    //LogData.InsertLogData(AffiliateID, "Optimal St. Kitts PDF Created");

                    PartnerLogBL LogData = new PartnerLogBL();
                    LogData.InsertLogData(AppId, Convert.ToInt32(Session["AffiliateID"]), "Optimal St. Kitts PDF Created");

                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.ContentType = "application/pdf";
                    Response.AppendHeader("content-disposition", "filename=Optimal St. Kitts App.pdf");
                    Response.AppendHeader("Content-Length", mStream.GetBuffer().Length.ToString());
                    Response.OutputStream.Write(mStream.GetBuffer(), 0, Convert.ToInt32(mStream.GetBuffer().Length));

                    Response.Flush();
                    Response.Close();
                }
                else
                    DisplayMessage("Data not found for this record.");
            }
            else if (strProcessor.ToLower().Contains("payvision"))
            {
                FileName = Server.MapPath("~/PDF/Payvision Application.pdf");
                mStream = PDFFile.CreatePayvisionPDF(FileName);

                if (mStream != null)
                {
                    //LogBL LogData = new LogBL(AppId);
                    //LogData.InsertLogData(AffiliateID, "Payvision PDF Created");

                    PartnerLogBL LogData = new PartnerLogBL();
                    LogData.InsertLogData(AppId, Convert.ToInt32(Session["AffiliateID"]), "Payvision PDF Created");

                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.ContentType = "application/pdf";
                    Response.AppendHeader("content-disposition", "filename=Payvision App.pdf");
                    Response.AppendHeader("Content-Length", mStream.GetBuffer().Length.ToString());
                    Response.OutputStream.Write(mStream.GetBuffer(), 0, Convert.ToInt32(mStream.GetBuffer().Length));

                    Response.Flush();
                    Response.Close();
                }
                else
                    DisplayMessage("Data not found for this record.");
            }
            else
                DisplayMessage("Invalid Processor for PDF creation assigned to this Record. PDF cannot be created.");

        }//end try
        catch (Exception err)
        {
            CreateLog Log = new CreateLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "Create PDF " + AppId + err.Message);
            DisplayMessage("Create PDF " + err.Message);
        }
    }

    /*protected void imgAddlServicesPDF_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            bool boolMerchantFunding = false;
            bool boolLease = false;
            bool boolGiftCard = false;
            string strMCA = string.Empty;
            string strLease = string.Empty;
            string strGiftCard = string.Empty;

            OnlineAppBL App = new OnlineAppBL(AppId);
            DataSet ds = App.GetEditInfo();
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow drNewApp = ds.Tables[0].Rows[0];
                boolMerchantFunding = Convert.ToBoolean(drNewApp["MerchantFunding"]);
                boolLease = Convert.ToBoolean(drNewApp["Lease"]);
                boolGiftCard = Convert.ToBoolean(drNewApp["GiftCard"]);
                strMCA = Convert.ToString(drNewApp["MCAType"]);
                strLease = Convert.ToString(drNewApp["LeaseCompany"]);
                strGiftCard = Convert.ToString(drNewApp["GiftCardType"]);
            }//end if count not 0

            


            if (boolMerchantFunding)
            {
                
                if (strMCA.ToLower().Contains("advanceme"))
                {
                    CreatePDF PDFFile = new CreatePDF(AppId);
                    string FileName = Server.MapPath("~/PDF/CAN Pre_Qual_Form.pdf");
                    MemoryStream mStream = PDFFile.CreateAMIPDF(FileName);
                    if (mStream != null)
                    {
                        LogBL LogData = new LogBL(AppId);
                        LogData.InsertLogData(AffiliateID, "AdvanceMe, Inc. PDF Created");

                        Response.ClearContent();
                        Response.ClearHeaders();
                        Response.ContentType = "application/pdf";
                        Response.AppendHeader("content-disposition", "AdvanceMe Application.pdf");
                        Response.OutputStream.Write(mStream.GetBuffer(), 0, Convert.ToInt32(mStream.Length));

                        Response.Flush();
                    }
                }
                else if (strMCA.ToLower().Contains("business financial services"))
                {
                    CreatePDF PDFFile = new CreatePDF(AppId);
                    string FileName = Server.MapPath("~/PDF/BFS_fax_application.pdf");
                    MemoryStream mStream = PDFFile.CreateBFSPDF(FileName); 
                    if (mStream != null)
                    {
                        LogBL LogData = new LogBL(AppId);
                        LogData.InsertLogData(AffiliateID, "Business Financial Services PDF Created");

                        Response.ClearContent();
                        Response.ClearHeaders();
                        Response.ContentType = "application/pdf";
                        Response.AppendHeader("content-disposition", "BFS_fax_application.pdf");
                        Response.OutputStream.Write(mStream.GetBuffer(), 0, Convert.ToInt32(mStream.Length));

                        Response.Flush();
                    }
                    else
                    DisplayMessage("Data not found for this record.");
                }
               
            }

            //Create Lease PDF 
            if (boolLease)
            {
                if (strLease.ToLower().Contains("northern"))
                    {
                        CreatePDF PDFFile = new CreatePDF(AppId);
                        string strLeaseCompany = PDFFile.ReturnLeaseCompany();
                        string strBusinessState = PDFFile.ReturnBusinessState();
                        if ((strBusinessState.ToLower().Contains("sd")) || (strBusinessState.ToLower().Contains("ks")) || (strBusinessState.ToLower().Contains("tn"))
                        || (strBusinessState.ToLower().Contains("pa")) || (strBusinessState.ToLower().Contains("vt")))
                        {
                            string FileName = Server.MapPath("~/PDF/Northern Leasing Agreement - SD, KS, TN, PA & VT.pdf");
                            MemoryStream mStream = PDFFile.CreateNorthernLeasePDF(FileName);

                            if (mStream != null)
                            {
                                LogBL LogData = new LogBL(AppId);
                                LogData.InsertLogData(AffiliateID, "Lease PDF Created");

                                Response.ClearContent();
                                Response.ClearHeaders();
                                Response.ContentType = "application/pdf";
                                Response.AppendHeader("content-disposition", "Northern Leasing Agreement - SD, KS, TN, PA & VT.pdf");
                                Response.OutputStream.Write(mStream.GetBuffer(), 0, Convert.ToInt32(mStream.Length));

                                Response.Flush();
                            }
                            else
                                DisplayMessage("Data not found for this record.");
                        }
                        else
                        {
                            string FileName = Server.MapPath("~/PDF/Northern Leasing Agreement - Standard.pdf");
                            MemoryStream mStream = PDFFile.CreateNorthernLeasePDF(FileName);

                            if (mStream != null)
                            {
                                LogBL LogData = new LogBL(AppId);
                                LogData.InsertLogData(AffiliateID, "Lease PDF Created");

                                Response.ClearContent();
                                Response.ClearHeaders();
                                Response.ContentType = "application/pdf";
                                Response.AppendHeader("content-disposition", "filename=Northern Leasing Agreement - Standard.pdf");
                                Response.OutputStream.Write(mStream.GetBuffer(), 0, Convert.ToInt32(mStream.Length));

                                Response.Flush();
                            }
                            else
                                DisplayMessage("Data not found for this record.");
                        }
                    }
                }

                if (boolGiftCard)
                {
                    if (strGiftCard.ToLower().Contains("global"))
                    {
                        CreatePDF PDFFile = new CreatePDF(AppId);
                        string FileName = Server.MapPath("/Application/GETI_Gift_Merchant_App.pdf");
                        MemoryStream mStream = PDFFile.CreateGETIPDF(FileName); ;
                        if (mStream != null)
                        {
                            LogBL LogData = new LogBL(AppId);
                            LogData.InsertLogData(AffiliateID, "Gift Card PDF Created");

                            Response.ClearContent();
                            Response.ClearHeaders();
                            Response.ContentType = "application/pdf";
                            Response.AppendHeader("content-disposition", "GETI_Gift_Merchant_App.pdf");
                            Response.OutputStream.Write(mStream.GetBuffer(), 0, Convert.ToInt32(mStream.Length));

                            Response.Flush();
                        }
                        else
                            DisplayMessage("Data not found for this record.");
                    }
                }


        }//end try
        catch (Exception err)
        {
            CreateLog Log = new CreateLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "Create PDF " + AppId + err.Message);
            DisplayMessage("Create PDF " + err.Message);
        }
    }*/

    protected void imgMCAPDF_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            bool boolMerchantFunding = false;
            bool boolLease = false;
            bool boolGiftCard = false;
            string strMCA = string.Empty;
            string strLease = string.Empty;
            string strGiftCard = string.Empty;

            OnlineAppBL App = new OnlineAppBL(AppId);
            DataSet ds = App.GetEditInfo();
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow drNewApp = ds.Tables[0].Rows[0];
                boolMerchantFunding = Convert.ToBoolean(drNewApp["MerchantFunding"]);
                boolLease = Convert.ToBoolean(drNewApp["Lease"]);
                boolGiftCard = Convert.ToBoolean(drNewApp["GiftCard"]);
                strMCA = Convert.ToString(drNewApp["MCAType"]);
                strLease = Convert.ToString(drNewApp["LeaseCompany"]);
                strGiftCard = Convert.ToString(drNewApp["GiftCardType"]);
            }//end if count not 0




            if (boolMerchantFunding)
            {

                if (strMCA.ToLower().Contains("advanceme"))
                {
                    CreatePDF PDFFile = new CreatePDF(AppId);
                    string FileName = Server.MapPath("~/PDF/CAN Pre_Qual_Form.pdf");
                    MemoryStream mStream = PDFFile.CreateAMIPDF(FileName);
                    if (mStream != null)
                    {
                        LogBL LogData = new LogBL(AppId);
                        LogData.InsertLogData(AffiliateID, "AdvanceMe, Inc. PDF Created");

                        Response.ClearContent();
                        Response.ClearHeaders();
                        Response.ContentType = "application/pdf";
                        Response.AppendHeader("content-disposition", "AdvanceMe Application.pdf");
                        Response.OutputStream.Write(mStream.GetBuffer(), 0, Convert.ToInt32(mStream.Length));

                        Response.Flush();
                    }
                }
                else if (strMCA.ToLower().Contains("business financial services"))
                {
                    CreatePDF PDFFile = new CreatePDF(AppId);
                    string FileName = Server.MapPath("~/PDF/BFS_fax_application.pdf");
                    MemoryStream mStream = PDFFile.CreateBFSPDF(FileName);
                    if (mStream != null)
                    {
                        LogBL LogData = new LogBL(AppId);
                        LogData.InsertLogData(AffiliateID, "Business Financial Services PDF Created");

                        Response.ClearContent();
                        Response.ClearHeaders();
                        Response.ContentType = "application/pdf";
                        Response.AppendHeader("content-disposition", "BFS_fax_application.pdf");
                        Response.OutputStream.Write(mStream.GetBuffer(), 0, Convert.ToInt32(mStream.Length));

                        Response.Flush();
                    }
                    else
                        DisplayMessage("Data not found for this record.");
                }
                else if (strMCA.ToLower().Contains("rapidadvance"))
                {
                    CreatePDF PDFFile = new CreatePDF(AppId);
                    string FileName = Server.MapPath("~/PDF/Rapid Advance Application.pdf");
                    MemoryStream mStream = PDFFile.CreateRapidAdvancePDF(FileName);
                    if (mStream != null)
                    {
                        LogBL LogData = new LogBL(AppId);
                        LogData.InsertLogData(AffiliateID, "RapidAdvance PDF Created");

                        Response.ClearContent();
                        Response.ClearHeaders();
                        Response.ContentType = "application/pdf";
                        Response.AppendHeader("content-disposition", "Rapid Advance Application.pdf");
                        Response.OutputStream.Write(mStream.GetBuffer(), 0, Convert.ToInt32(mStream.Length));

                        Response.Flush();
                    }
                    else
                        DisplayMessage("Data not found for this record.");
                }

            }
        }//end try
        catch (Exception err)
        {
            CreateLog Log = new CreateLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "Create PDF " + AppId + err.Message);
            DisplayMessage("Create PDF " + err.Message);
        }
    }

    protected void imgLeasePDF_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            bool boolMerchantFunding = false;
            bool boolLease = false;
            bool boolGiftCard = false;
            string strMCA = string.Empty;
            string strLease = string.Empty;
            string strGiftCard = string.Empty;

            OnlineAppBL App = new OnlineAppBL(AppId);
            DataSet ds = App.GetEditInfo();
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow drNewApp = ds.Tables[0].Rows[0];
                boolMerchantFunding = Convert.ToBoolean(drNewApp["MerchantFunding"]);
                boolLease = Convert.ToBoolean(drNewApp["Lease"]);
                boolGiftCard = Convert.ToBoolean(drNewApp["GiftCard"]);
                strMCA = Convert.ToString(drNewApp["MCAType"]);
                strLease = Convert.ToString(drNewApp["LeaseCompany"]);
                strGiftCard = Convert.ToString(drNewApp["GiftCardType"]);
            }//end if count not 0


            //Create Lease PDF 
            if (boolLease)
            {
                if (strLease.ToLower().Contains("northern"))
                {
                    CreatePDF PDFFile = new CreatePDF(AppId);
                    string strLeaseCompany = PDFFile.ReturnLeaseCompany();
                    string strBusinessState = PDFFile.ReturnBusinessState();
                    if ((strBusinessState.ToLower().Contains("sd")) || (strBusinessState.ToLower().Contains("ks")) || (strBusinessState.ToLower().Contains("tn"))
                    || (strBusinessState.ToLower().Contains("pa")) || (strBusinessState.ToLower().Contains("vt")))
                    {
                        string FileName = Server.MapPath("~/PDF/Northern Leasing Agreement - SD, KS, TN, PA & VT.pdf");
                        MemoryStream mStream = PDFFile.CreateNorthernLeasePDF(FileName);

                        if (mStream != null)
                        {
                            LogBL LogData = new LogBL(AppId);
                            LogData.InsertLogData(AffiliateID, "Lease PDF Created");

                            Response.ClearContent();
                            Response.ClearHeaders();
                            Response.ContentType = "application/pdf";
                            Response.AppendHeader("content-disposition", "Northern Leasing Agreement - SD, KS, TN, PA & VT.pdf");
                            Response.OutputStream.Write(mStream.GetBuffer(), 0, Convert.ToInt32(mStream.Length));

                            Response.Flush();
                        }
                        else
                            DisplayMessage("Data not found for this record.");
                    }
                    else
                    {
                        string FileName = Server.MapPath("~/PDF/Northern Leasing Agreement - Standard.pdf");
                        MemoryStream mStream = PDFFile.CreateNorthernLeasePDF(FileName);

                        if (mStream != null)
                        {
                            LogBL LogData = new LogBL(AppId);
                            LogData.InsertLogData(AffiliateID, "Lease PDF Created");

                            Response.ClearContent();
                            Response.ClearHeaders();
                            Response.ContentType = "application/pdf";
                            Response.AppendHeader("content-disposition", "filename=Northern Leasing Agreement - Standard.pdf");
                            Response.OutputStream.Write(mStream.GetBuffer(), 0, Convert.ToInt32(mStream.Length));

                            Response.Flush();
                        }
                        else
                            DisplayMessage("Data not found for this record.");
                    }
                }
            }


        }//end try
        catch (Exception err)
        {
            CreateLog Log = new CreateLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "Create PDF " + AppId + err.Message);
            DisplayMessage("Create PDF " + err.Message);
        }
    }

    protected void imgGiftCardPDF_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            bool boolMerchantFunding = false;
            bool boolLease = false;
            bool boolGiftCard = false;
            string strMCA = string.Empty;
            string strLease = string.Empty;
            string strGiftCard = string.Empty;

            OnlineAppBL App = new OnlineAppBL(AppId);
            DataSet ds = App.GetEditInfo();
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow drNewApp = ds.Tables[0].Rows[0];
                boolMerchantFunding = Convert.ToBoolean(drNewApp["MerchantFunding"]);
                boolLease = Convert.ToBoolean(drNewApp["Lease"]);
                boolGiftCard = Convert.ToBoolean(drNewApp["GiftCard"]);
                strMCA = Convert.ToString(drNewApp["MCAType"]);
                strLease = Convert.ToString(drNewApp["LeaseCompany"]);
                strGiftCard = Convert.ToString(drNewApp["GiftCardType"]);
            }//end if count not 0

            if (boolGiftCard)
            {
                if (strGiftCard.ToLower().Contains("sage eft"))
                {
                    CreatePDF PDFFile = new CreatePDF(AppId);
                    string FileName = Server.MapPath("~/PDF/GETI_Gift_Merchant_App.pdf");
                    MemoryStream mStream = PDFFile.CreateGETIPDF(FileName); ;
                    if (mStream != null)
                    {
                        LogBL LogData = new LogBL(AppId);
                        LogData.InsertLogData(AffiliateID, "Gift Card PDF Created");

                        Response.ClearContent();
                        Response.ClearHeaders();
                        Response.ContentType = "application/pdf";
                        Response.AppendHeader("content-disposition", "filename=GETI_Gift_Merchant_App.pdf");
                        Response.OutputStream.Write(mStream.GetBuffer(), 0, Convert.ToInt32(mStream.Length));

                        Response.Flush();
                    }
                    else
                        DisplayMessage("Data not found for this record.");
                }
            }


        }//end try
        catch (Exception err)
        {
            CreateLog Log = new CreateLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "Create PDF " + AppId + err.Message);
            DisplayMessage("Create PDF " + err.Message);
        }
    }

    protected void lnkAmendment_Click(object sender, EventArgs e)
    {
        try
        {
            bool boolMerchantFunding = false;
            bool boolLease = false;
            bool boolGiftCard = false;
            string strMCA = string.Empty;
            string strLease = string.Empty;
            string strGiftCard = string.Empty;


            OnlineAppBL App = new OnlineAppBL(AppId);
            DataSet ds = App.GetEditInfo();
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow drNewApp = ds.Tables[0].Rows[0];
                boolMerchantFunding = Convert.ToBoolean(drNewApp["MerchantFunding"]);
                boolLease = Convert.ToBoolean(drNewApp["Lease"]);
                boolGiftCard = Convert.ToBoolean(drNewApp["GiftCard"]);
                strMCA = Convert.ToString(drNewApp["MCAType"]);
                strLease = Convert.ToString(drNewApp["LeaseCompany"]);
                strGiftCard = Convert.ToString(drNewApp["GiftCardType"]);
            }//end if count not 0

            //if (boolGiftCard)
            {
                //if (strGiftCard.ToLower().Contains("sage eft"))
                {

                    DataRow drNewApp = ds.Tables[0].Rows[0];

                    CreatePDF PDFFile = new CreatePDF(AppId);
                    string FileName = "";
                    if (Convert.ToString(drNewApp["ContractTerm"]) == "1 month")
                    {
                        FileName = Server.MapPath("~/PDF/Merchant Amendment - Month to Month.pdf");
                    }
                    else if (Convert.ToString(drNewApp["ContractTerm"]) == "1 year")
                    {
                        FileName = Server.MapPath("~/PDF/Merchant Amendment - Single Year.pdf");
                    }

                    MemoryStream mStream = PDFFile.CreateSageMonthToMonthPDF(FileName); ;
                    if (mStream != null)
                    {
                        LogBL LogData = new LogBL(AppId);
                        LogData.InsertLogData(AffiliateID, "Amendment Created");

                        Response.ClearContent();
                        Response.ClearHeaders();
                        Response.ContentType = "application/pdf";
                        Response.AppendHeader("content-disposition", "filename=GETI_Gift_Merchant_App.pdf");
                        Response.OutputStream.Write(mStream.GetBuffer(), 0, Convert.ToInt32(mStream.Length));

                        Response.Flush();
                    }
                    else
                        DisplayMessage("Data not found for this record.");
                }
            }


        }//end try
        catch (Exception err)
        {
            CreateLog Log = new CreateLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "Create PDF " + AppId + err.Message);
            DisplayMessage("Create PDF " + err.Message);
        }
    }

    #region Sage PDFs
    protected void btnSageApp_Click(object sender, EventArgs e)
    {
        try
        {
            MemoryStream mStream = new MemoryStream();
            CreatePDF PDFFile = new CreatePDF(AppId);
            string FileName = Server.MapPath("~/PDF/Paya Application.pdf");
            mStream = PDFFile.CreateSagePDF(FileName);

            if (mStream != null)
            {
                LogBL LogData = new LogBL(AppId);
                LogData.InsertLogData(AffiliateID, "Paya PDF Created");

                Response.ClearContent();
                Response.ClearHeaders();
                Response.ContentType = "application/pdf";
                Response.AppendHeader("content-disposition", "filename=Paya Application.pdf");
                Response.OutputStream.Write(mStream.GetBuffer(), 0, Convert.ToInt32(mStream.Length));

                Response.Flush();
                Response.Close();
            }
            else
                DisplayMessage("Data not found for this record.");
        }
        catch (Exception err)
        {
            DisplayMessage(err.Message);
        }
    }
    protected void btnSageMOTO_Click(object sender, EventArgs e)
    {
        try
        {
            MemoryStream mStream = new MemoryStream();
            CreatePDF PDFFile = new CreatePDF(AppId);
            string FileName = Server.MapPath("~/PDF/Sage MOTO-Internet Question.pdf");
            mStream = PDFFile.CreateSageMOTO(FileName);

            if (mStream != null)
            {
                LogBL LogData = new LogBL(AppId);
                LogData.InsertLogData(AffiliateID, "Sage MOTO PDF Created");

                Response.ClearContent();
                Response.ClearHeaders();
                Response.ContentType = "application/pdf";
                Response.AppendHeader("content-disposition", "filename=Sage MOTO-Internet Question.pdf");
                Response.OutputStream.Write(mStream.GetBuffer(), 0, Convert.ToInt32(mStream.Length));

                Response.Flush();
                Response.Close();
            }
            else
                DisplayMessage("Data not found for this record.");
        }
        catch (Exception err)
        {
            DisplayMessage(err.Message);
        }  
    }
    #endregion

    #region Chase PDFs

    public void btnChaseMPA_Click(object sender, EventArgs e)
    {
        try
        {
            string FileName = Server.MapPath("/Application/ChaseMPA.pdf");

            CreatePDF PDFFile = new CreatePDF(AppId);
            MemoryStream mStream = PDFFile.CreateChaseMPAPDF(FileName);
            if (mStream != null)
            {
                Response.ClearContent();
                Response.ClearHeaders();
                Response.ContentType = "application/pdf";
                Response.AppendHeader("content-disposition", "filename=ChaseMPA.pdf");
                Response.AppendHeader("Content-Length", mStream.GetBuffer().Length.ToString());
                Response.OutputStream.Write(mStream.GetBuffer(), 0, Convert.ToInt32(mStream.GetBuffer().Length));

                Response.Flush();
                Response.Close();

                LogBL LogData = new LogBL(AppId);
                LogData.InsertLogData(AffiliateID, "Chase MPA PDF Created");
            }
            else
                DisplayMessage("Data not found for this record.");
        }//end try
        catch (Exception err)
        {
            DisplayMessage("Create Chase MPA PDF Error - " + err.Message);
            CreateOnlineAppLog Log = new CreateOnlineAppLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + AppId.ToString() + " - " + "Merchant PDF - Create Chase MPA PDF Error - " + err.Message);
        }
    }

    public void btnChaseFS3Tier_Click(object sender, EventArgs e)
    {
        try
        {
            string FileName = Server.MapPath("~/PDF/CardConnect Application.pdf");

            CreatePDF PDFFile = new CreatePDF(AppId);
            MemoryStream mStream = PDFFile.CreateChaseFS3TierPDF(FileName);
            if (mStream != null)
            {
                Response.ClearContent();
                Response.ClearHeaders();
                Response.ContentType = "application/pdf";
                Response.AppendHeader("content-disposition", "filename=CardConnect Application.pdf");
                Response.AppendHeader("Content-Length", mStream.GetBuffer().Length.ToString());
                Response.OutputStream.Write(mStream.GetBuffer(), 0, Convert.ToInt32(mStream.GetBuffer().Length));

                Response.Flush();
                Response.Close();

                LogBL LogData = new LogBL(AppId);
                LogData.InsertLogData(AffiliateID, "CardConnect PDF Created");
            }
            else
                DisplayMessage("Data not found for this record.");
        }//end try
        catch (Exception err)
        {
            DisplayMessage("Create CardConnect PDF Error - " + err.Message);
            CreateOnlineAppLog Log = new CreateOnlineAppLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + AppId.ToString() + " - " + "Merchant PDF - Create CardConnect PDF Error - " + err.Message);
        }
    }

    public void btnChaseFSInterchangePlus_Click(object sender, EventArgs e)
    {
        try
        {
            string FileName = Server.MapPath("~/PDF/CardConnect Application.pdf");

            CreatePDF PDFFile = new CreatePDF(AppId);
            MemoryStream mStream = PDFFile.CreateChaseFSInterchangePlusPDF(FileName);
            if (mStream != null)
            {
                Response.ClearContent();
                Response.ClearHeaders();
                Response.ContentType = "application/pdf";
                Response.AppendHeader("content-disposition", "filename=Chase Fee schedule interchange plus.pdf.pdf");
                Response.AppendHeader("Content-Length", mStream.GetBuffer().Length.ToString());
                Response.OutputStream.Write(mStream.GetBuffer(), 0, Convert.ToInt32(mStream.GetBuffer().Length));

                Response.Flush();
                Response.Close();

                LogBL LogData = new LogBL(AppId);
                LogData.InsertLogData(AffiliateID, "CardConnect PDF Created");
            }
            else
                DisplayMessage("Data not found for this record.");
        }//end try
        catch (Exception err)
        {
            DisplayMessage("Create Chase Fee schedule interchange plus PDF Error - " + err.Message);
            CreateOnlineAppLog Log = new CreateOnlineAppLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + AppId.ToString() + " - " + "Merchant PDF - Create CardConnect PDF Error - " + err.Message);
        }
    }

    #endregion

    #region Chase Old PDFs
    protected void btnChaseAbout_Click(object sender, EventArgs e)
    {
        try
        {
            MemoryStream mStream = new MemoryStream();
            CreatePDF PDFFile = new CreatePDF(AppId);
            string FileName = Server.MapPath("~/PDF/Chase About Merchant.pdf");

            mStream = PDFFile.CreateChasePDFAbout(FileName);

            if (mStream != null)
            {
                LogBL LogData = new LogBL(AppId);
                LogData.InsertLogData(AffiliateID, "Chase About PDF Created");

                Response.ClearContent();
                Response.ClearHeaders();
                Response.ContentType = "application/pdf";
                Response.AppendHeader("content-disposition", "filename=Chase About.pdf");
                Response.OutputStream.Write(mStream.GetBuffer(), 0, Convert.ToInt32(mStream.Length));
                Response.Flush();

            }
            else
                DisplayMessage("Data not found for this record.");   
        }
        catch (Exception err)
        {
            DisplayMessage(err.Message);
        }     
     }
    protected void btnChaseFee_Click(object sender, EventArgs e)
    {
         try
        {
            MemoryStream mStream = new MemoryStream();
            CreatePDF PDFFile = new CreatePDF(AppId);
            string FileName = Server.MapPath("~/PDF/Chase Fee Schedule.pdf");
            mStream = PDFFile.CreateChasePDFFS(FileName);

            if (mStream != null)
            {
                LogBL LogData = new LogBL(AppId);
                LogData.InsertLogData(AffiliateID, "Chase Fee Schedule Created");

                Response.ClearContent();
                Response.ClearHeaders();
                Response.ContentType = "application/pdf";
                Response.AppendHeader("content-disposition", "filename=Chase Fee Schedule.pdf");
                Response.OutputStream.Write(mStream.GetBuffer(), 0, Convert.ToInt32(mStream.Length));
                Response.Flush();
            }
            else
                DisplayMessage("Data not found for this record."); 
        }
        catch (Exception err)
        {
            DisplayMessage(err.Message);
        }     
    }
    protected void btnChaseMP_Click(object sender, EventArgs e)
    {
        try
        {
            MemoryStream mStream = new MemoryStream();
            CreatePDF PDFFile = new CreatePDF(AppId);
            string FileName = Server.MapPath("~/PDF/Chase MPA.pdf");
            mStream = PDFFile.CreateChasePDFMP(FileName);

            if (mStream != null)
            {
                LogBL LogData = new LogBL(AppId);
                LogData.InsertLogData(AffiliateID, "Chase MPA Created");

                Response.ClearContent();
                Response.ClearHeaders();
                Response.ContentType = "application/pdf";
                Response.AppendHeader("content-disposition", "filename=Chase Multiple Locations.pdf");
                Response.OutputStream.Write(mStream.GetBuffer(), 0, Convert.ToInt32(mStream.Length));
                Response.Flush();
            }
            else
                DisplayMessage("Data not found for this record.");
        }//end try
        catch (Exception err)
        {
            DisplayMessage(err.Message);
        }
    }
    protected void btnChaseCreditAdd_Click(object sender, EventArgs e)
    {
        try
        {
            MemoryStream mStream = new MemoryStream();
            CreatePDF PDFFile = new CreatePDF(AppId);
            string FileName = Server.MapPath("~/PDF/Chase Credit Addendum.pdf");
            mStream = PDFFile.CreateChasePDFCreditAdd(FileName);

            if (mStream != null)
            {
                LogBL LogData = new LogBL(AppId);
                LogData.InsertLogData(AffiliateID, "Chase Credit Addendum Created");

                Response.ClearContent();
                Response.ClearHeaders();
                Response.ContentType = "application/pdf";
                Response.AppendHeader("content-disposition", "filename=Chase Credit Addendum.pdf");
                Response.OutputStream.Write(mStream.GetBuffer(), 0, Convert.ToInt32(mStream.Length));
                Response.Flush();
            }
            else
                DisplayMessage("Data not found for this record.");
        }
        catch (Exception err)
        {
            DisplayMessage(err.Message);
        }
    }
    #endregion

    #endregion

    #region HISTORY
    //This function populates history grid
    public void PopulateHistory()
    {
        if (User.IsInRole("Admin") || User.IsInRole("Employee"))
        {
            PartnerLogBL LogData = new PartnerLogBL();
            DataSet ds = LogData.GetLogData(AppId, "ALL");
            if (ds.Tables[0].Rows.Count > 0)
            {
                grdHistory.DataSource = ds;
                grdHistory.DataBind();
            }//end if count not 0
        }
    }//end function PopulateHistory
    #endregion

    #region NOTES
    //This function displays notes
    public void LoadNotes()
    {
        try
        {
            OnlineAppBL App = new OnlineAppBL(AppId);
            PartnerDS.OnlineAppNotesDataTable dtNotes = App.GetOnlineAppNotes();
            if (dtNotes.Rows.Count > 0)
            {
                grdNotes.DataSource = dtNotes;
                grdNotes.DataBind();
            }
        }
        catch(Exception err)
        {
            CreateLog Log = new CreateLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "Load Notes - " + err.Message);
            DisplayMessage("Error Loading Notes");
        }
    }//end function LoadNotes

    protected void btnAddNote_Click(object sender, EventArgs e)
    {
        try
        {
            //if (lstNoteType.SelectedValue == "0")
            {
                if (txtNotes.Text.Trim() != "")
                {
                    ACTDataDL ACT = new ACTDataDL();
                    int AffiliateID = Convert.ToInt32(Session["AffiliateID"]);
                    string partnerID = Convert.ToString(Session["AffiliateID"]);
                    string partnerContactID = ACT.ReturnContactID(partnerID);
                    AffiliatesBL Aff = new AffiliatesBL(AffiliateID);
                    //Add note that status has been changed
                    //string ActUserID = Aff.ReturnACTUserID();

                    OnlineAppStatusBL App = new OnlineAppStatusBL(AppId);
                    bool retVal = App.InsertNote(partnerContactID, txtNotes.Text.Trim());
                    //some online apps do not have an affiliate ID, so retVal is false
                    /*if (!retVal)
                        DisplayMessage("Error Inserting Note");*/

                    //Send email
                    if (chkNotify.Checked)
                        SendEmail(txtNotes.Text);

                    txtNotes.Text = "";
                    //LoadNotes();
                }//end if note not blank
                else
                    DisplayMessage("Enter Note Text");
            }
            //else if (lstNoteType.SelectedValue == "1")
            {
                if (txtNotes.Text.Trim() != "")
                {
                    ACTDataDL ACT = new ACTDataDL();
                    int AffiliateID = Convert.ToInt32(Session["AffiliateID"]);
                    string partnerID = Convert.ToString(Session["AffiliateID"]);
                    string partnerContactID = ACT.ReturnContactID(partnerID);
                    AffiliatesBL Aff = new AffiliatesBL(AffiliateID);
                    //Add note that status has been changed
                    //string ActUserID = Aff.ReturnACTUserID();

                    OnlineAppStatusBL App = new OnlineAppStatusBL(AppId);
                    bool retVal = App.InsertTrackingNumber(partnerContactID, txtNotes.Text.Trim());
                    //some online apps do not have an affiliate ID, so retVal is false
                    /*if (!retVal)
                        DisplayMessage("Error Inserting Note");*/

                    //Send email
                    if (chkNotify.Checked)
                        SendEmail(txtNotes.Text);

                    txtNotes.Text = "";
                    //LoadNotes();
                }//end if note not blank
                else
                    DisplayMessage("Enter Note Text");
            }
            //else if (lstNoteType.SelectedValue == "2")
            {
                if (txtNotes.Text.Trim() != "")
                {
                    ACTDataDL ACT = new ACTDataDL();
                    int AffiliateID = Convert.ToInt32(Session["AffiliateID"]);
                    string partnerID = Convert.ToString(Session["AffiliateID"]);
                    string partnerContactID = ACT.ReturnContactID(partnerID);
                    AffiliatesBL Aff = new AffiliatesBL(AffiliateID);
                    //Add note that status has been changed
                    //string ActUserID = Aff.ReturnACTUserID();

                    OnlineAppStatusBL App = new OnlineAppStatusBL(AppId);
                    bool retVal = App.InsertPendingReason(partnerContactID, txtNotes.Text.Trim());
                    //some online apps do not have an affiliate ID, so retVal is false
                    /*if (!retVal)
                        DisplayMessage("Error Inserting Note");*/

                    //Send email
                    if (chkNotify.Checked)
                        SendEmail(txtNotes.Text);

                    txtNotes.Text = "";
                    //LoadNotes();
                }//end if note not blank
                else
                    DisplayMessage("Note entered");
            }
        }//end try
        catch (Exception err)
        {
            CreateLog Log = new CreateLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "Add Note - " + err.Message);
            DisplayMessage("Error adding notes.");
        }
    }

    protected void grdNotes_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            if (User.IsInRole("Admin"))
            {
                GridViewRow grdRow = grdNotes.Rows[e.RowIndex];
                string ID = Server.HtmlDecode(grdRow.Cells[3].Text);
                OnlineAppStatusBL Notes = new OnlineAppStatusBL(AppId);
                Notes.DeleteNote(ID);
                //Add action to Log table
                PartnerLogBL LogData = new PartnerLogBL();
                LogData.InsertLogData(AppId, Convert.ToInt32(Session["AffiliateID"]), "Note deleted");
                //LoadNotes();
            }//end if user is admin
        }//end try
        catch (Exception err)
        {
            CreateLog Log = new CreateLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "Delete Note - " + err.Message);
            DisplayMessage("Error deleting note");
        }
    }

    protected void grdNotes_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (!User.IsInRole("Admin"))
            {
                e.Row.Cells[3].Visible = false;
                e.Row.Cells[4].Visible = false;
            }
        }//end try
        catch (Exception err)
        {
            CreateLog Log = new CreateLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), err.Message);
            DisplayMessage("Error Loading Notes");
        }
    }

    //This function emails Admin
    public void SendEmail(string strMail)
    {
        try
        {
            //Send Email to admin if notify administrator is checked
            string strSubject = "Urgent Note Added for App ID " + AppId + " on " + DateTime.Now.ToString(); ;
            MailMessage msg = new MailMessage();
            //msg.To = "service@commercetech.com";
            msg.To.Add(new MailAddress("service@commercetech.com"));
            //msg.From = "information@commercetech.com";
            msg.From = new MailAddress("sales@commercetech.com");
            msg.Subject = strSubject;
            msg.Body = Session["AffiliateName"].ToString().Trim() + " has added a note: " + strMail;
            //SmtpMail.Send(msg);
            SmtpClient mSmtpClient = new SmtpClient();
            mSmtpClient.Send(msg);
        }
        catch (Exception err)
        {
            CreateLog Log = new CreateLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), err.Message);
            DisplayMessage("Error Processing Request. Please contact technical support");
        }
    }//end function send email
    #endregion

    #region SALES OPPS
    //This function Populates Sales Opps lists
    public void PopulateSalesOppsLists()
    {
        try
        {
            if (pnlEditSalesOpp.Visible)
            {
                //Get list of products for Reprogram field in the Edit Sales Opp panel
                ACTDataBL Products = new ACTDataBL();
                DataSet dsProductsReprogram = Products.GetList("Reprogram");
                if (dsProductsReprogram.Tables[0].Rows.Count > 0)
                {
                    lstReprogram.DataSource = dsProductsReprogram;
                    lstReprogram.DataTextField = "ProductName";
                    lstReprogram.DataValueField = "ProductName";
                    lstReprogram.DataBind();
                }
                System.Web.UI.WebControls.ListItem lstItem = new System.Web.UI.WebControls.ListItem();
                lstItem.Text = "";
                lstItem.Value = "";
                lstReprogram.Items.Add(lstItem);
                lstReprogram.SelectedValue = lstReprogram.Items.FindByText("").Value;

                //Get Sales Rep list for Edit Sales Opp Panel
                ListBL SalesRepList = new ListBL();
                DataSet dsRep = SalesRepList.GetSalesRepList();
                if (dsRep.Tables[0].Rows.Count > 0)
                {
                    lstRepName.DataSource = dsRep;
                    lstRepName.DataTextField = "RepName";
                    lstRepName.DataValueField = "MasterNum";
                    lstRepName.DataBind();
                }
                lstItem = new System.Web.UI.WebControls.ListItem();
                lstItem.Text = "";
                lstItem.Value = "";
                lstRepName.Items.Add(lstItem);
                lstRepName.SelectedValue = lstRepName.Items.FindByValue("").Value;

                lblCOG.Enabled = false;

                if (!User.IsInRole("Admin"))
                {
                    lstRepNameAdd.SelectedValue = lstRepNameAdd.Items.FindByValue(Session["MasterNum"].ToString()).Value;
                    lstRepNameAdd.Enabled = false;
                    txtAddCOG.Enabled = false;
                    txtAddSubtotal.Enabled = false;
                    lblCOG.Enabled = false;
                }

                if (User.IsInRole("Employee"))
                {
                    //lblCOG.Text = Convert.ToString(User.IsInRole("Admin"));
                    lblCOG.Enabled = true;
                    //lblCOG.Text = Convert.ToString(User.IsInRole("Admin"));
                }


            }
            else
            {
                BusinessLayer.SalesOppsBL Products = new BusinessLayer.SalesOppsBL();
                DataSet dsProducts = Products.GetProducts();
                if (dsProducts.Tables[0].Rows.Count > 0)
                {
                    //Get list of products for the Add Product panel
                    lstProductName.DataSource = dsProducts;
                    lstProductName.DataTextField = "ProductName";
                    lstProductName.DataValueField = "ProductCode";
                    lstProductName.DataBind();

                    //Get list of products for Reprogram field in the Edit Sales Opp panel
                    ACTDataBL ProductsReprogram = new ACTDataBL();
                    DataSet dsProductsReprogram = ProductsReprogram.GetList("Reprogram"); 
                    lstAddReprogram.DataSource = dsProductsReprogram;
                    lstAddReprogram.DataTextField = "ProductName";
                    lstAddReprogram.DataValueField = "ProductName";
                    lstAddReprogram.DataBind();
                }

                System.Web.UI.WebControls.ListItem lstItem = new System.Web.UI.WebControls.ListItem();
                lstItem.Text = "";
                lstItem.Value = "";
                lstProductName.Items.Add(lstItem);
                lstProductName.SelectedValue = lstProductName.Items.FindByText("").Value;

                lstAddReprogram.Items.Add(lstItem);
                lstAddReprogram.SelectedValue = lstAddReprogram.Items.FindByText("").Value;

                

                //Get Sales Rep list for Add Product Panel
                ListBL SalesRepList = new ListBL();
                DataSet dsRep = SalesRepList.GetSalesRepList();
                if (dsRep.Tables[0].Rows.Count > 0)
                {
                    lstRepNameAdd.DataSource = dsRep;
                    lstRepNameAdd.DataTextField = "RepName";
                    lstRepNameAdd.DataValueField = "MasterNum";
                    lstRepNameAdd.DataBind();
                }
                lstItem = new System.Web.UI.WebControls.ListItem();
                lstItem.Text = "";
                lstItem.Value = "";
                lstRepNameAdd.Items.Add(lstItem);
                lstRepNameAdd.SelectedValue = lstRepNameAdd.Items.FindByValue("").Value;
                
                //Default Repname to Online App Rep Name
                OnlineAppProfile GeneralInfo = new OnlineAppProfile(AppId);
                DataSet dsNewApp = GeneralInfo.GetProfileData();
                DataRow drNewApp = dsNewApp.Tables[0].Rows[0];
                lstRepNameAdd.SelectedValue = lstRepNameAdd.Items.FindByValue(drNewApp["RepNum"].ToString().Trim()).Value;

                //lblCOG.Enabled = false;

                if (!User.IsInRole("Admin"))
                {
                    lstRepNameAdd.SelectedValue = lstRepNameAdd.Items.FindByValue(Session["MasterNum"].ToString()).Value;
                    lstRepNameAdd.Enabled = false;
                    txtAddCOG.Enabled = false;
                    txtAddSubtotal.Enabled = false;
                    lblCOG.Enabled = false;
                }
                //if (User.IsInRole("Admin"))
                //{

                  //  lblCOG.Enabled = true;
                //}
            }
        }
        catch (Exception err)
        {
            CreateLog Log = new CreateLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), err.Message);
            DisplayMessage("Error populating Sales Opp Lists - " + err.Message);
        }
    }

    //This function Populates Sales Opps
    public void PopulateSalesOpps()
    {
        
        ACTDataDL ACTSalesOppID = new ACTDataDL();
        DataSet dsACTSaleOppID = ACTSalesOppID.GetACTSaleOppsID(AppId);
        OnlineAppSummaryDL unlinkSalesOpp = new OnlineAppSummaryDL();
        if (dsACTSaleOppID.Tables[0].Rows.Count > 0)
        {
            
            int iDelRet = unlinkSalesOpp.DeleteACTSalesOppID(AppId);
            for (int i = 0; i < dsACTSaleOppID.Tables[0].Rows.Count; i++)
            {
                DataRow drACTSaleOppID = dsACTSaleOppID.Tables[0].Rows[i];
                if (!Convert.IsDBNull(drACTSaleOppID["ID"]))
                {
                    string strACTSalesOppID = Convert.ToString(drACTSaleOppID["ID"]);
                    int iRet = unlinkSalesOpp.updateUnlinkedSalesOpp(AppId, strACTSalesOppID);
                    //int retVal = unlinkSalesOpp.unlinkedSalesOpps(AppId, strACTSalesOppID);
                }
            }
        }

        DataSet dsUnlinked = new DataSet();
        dsUnlinked = unlinkSalesOpp.GetOnlineAppSalesOppID(AppId);
        if (dsUnlinked.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < dsUnlinked.Tables[0].Rows.Count; i++)
            {
                DataRow drUnlinked = dsUnlinked.Tables[0].Rows[i];
                if (!Convert.IsDBNull(drUnlinked["ID"]))
                {
                    string strACTSalesOppID = Convert.ToString(drUnlinked["ID"]);
                    int retVal = unlinkSalesOpp.unlinkedSalesOpps(AppId, strACTSalesOppID);
                }
            }
        }
        

        //Get Sales Opp information from equipment table
        BusinessLayer.SalesOppsBL SalesOpps = new BusinessLayer.SalesOppsBL();
        PartnerDS.OnlineAppSalesOppsDataTable dt = SalesOpps.GetSalesOpps(AppId);
        if (dt.Rows.Count > 0)
        {
            grdSalesOpps.DataSource = dt;
            grdSalesOpps.DataBind();
        }//end if count not 0
    }//end function PopulateSalesOpps

    //This function handles add new sales opp button click
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (lstProductName.SelectedItem.Text != "")
            {
                int AffiliateID = Convert.ToInt32(Session["AffiliateID"]);
                AffiliatesBL Aff = new AffiliatesBL(AffiliateID);
                string strActUserID = Aff.ReturnACTUserID();
                BusinessLayer.SalesOppsBL Opp = new BusinessLayer.SalesOppsBL();
                bool retVal = Opp.AddSalesOpp(lstProductName.SelectedItem.Value.Trim(),
                    txtAddSellPrice.Text.Trim(), txtAddCOG.Text.Trim(), lstAddQuantity.SelectedItem.Value,
                    strActUserID, AppId, txtAddSubtotal.Text.Trim(), lstRepNameAdd.SelectedItem.Value,
                    lstRepNameAdd.SelectedItem.Text, lstPayment.SelectedItem.Value, lstAddReprogram.SelectedItem.Value, txtTerminalID.Text.Trim());
                if (!retVal)
                    DisplayMessage("Error inserting Sales Opp");
                else
                {
                    //Add action to Log table
                    PartnerLogBL LogData = new PartnerLogBL();
                    retVal = LogData.InsertLogData(AppId, Convert.ToInt32(Session["AffiliateID"]), "Sales Opp (" + lstProductName.SelectedItem.Text.Trim() + ") Added");
                }

                //Reset all fields
                lstProductName.SelectedValue = lstProductName.Items.FindByText("").Value;
                txtAddCOG.Text = "";
                txtAddSellPrice.Text = "";
                txtAddSubtotal.Text = "";
                lstAddQuantity.SelectedValue = lstAddQuantity.Items.FindByValue("1").Value;
                lstPayment.SelectedValue = lstPayment.Items.FindByText("Invoice Merchant").Value;
                lstAddReprogram.SelectedValue = lstAddReprogram.Items.FindByValue("").Value;

                pnlAddOpp.Visible = false;
                lnkAddSalesOpps.Visible = true;

                //Populate the sales opp table
                PopulateSalesOpps();
            }//end if product selected is not blank
            else
                DisplayMessage("Please select product");
        }//end try
        catch (Exception err)
        {
            CreateLog Log = new CreateLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), err.Message);
            DisplayMessage("Error adding Sales Opp");
        }
    }//end add sales opp click

    //This function populates product info based on product selected
    protected void lstProductName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            /*
            PartnerDS.ProductInfoDataTable dt = new PartnerDS.ProductInfoDataTable();
            ProductsBL salesOppProduct = new ProductsBL();
            dt = salesOppProduct.GetProductInfo(Convert.ToInt32(listEditProductName.SelectedItem.Value.Trim()));
            if (dt.Rows.Count > 0)
            {
                txtAddCOG.Text = dt[0].COG.ToString();
                txtAddSellPrice.Text = dt[0].SellPrice.ToString();
                //txtAddSubtotal.Text = Convert.ToString(Convert.ToDecimal(dt[0].SellPrice.ToString()) * QuantitySelected);
               
            }*/
            PopulateAddSalesOpp(Convert.ToInt16(lstProductName.SelectedItem.Value.Trim()), 1);

            string ConnStringPartner = ConfigurationManager.AppSettings["ConnectionStringPartner"].ToString();
            SqlConnection Conn = new SqlConnection(ConnStringPartner);
            // get all product codes of type terminal or pos
            string strQuery = "SELECT [ProductCode] FROM [AgentPortal].[dbo].[Product] WHERE CategoryID IN (0, 4, 5, 6, 7, 32, 21, 22, 11, 12, 13)";
            SqlCommand cmd = new SqlCommand(strQuery, Conn);
            cmd.Connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            bool enableQuant = true;

            for(int i=0; i<ds.Tables[0].Rows.Count; i++) {
                DataRow dr = ds.Tables[0].Rows[i];
                if(Convert.ToString(dr["ProductCode"]) == lstProductName.SelectedItem.Value) {
                    enableQuant = false;
                    break;
                }
            }
            
            if(enableQuant) {
                lstAddQuantity.Enabled = true;
            } else {
                lstAddQuantity.Enabled = false;
            }

            //PopulateAddSalesOpp(Convert.ToInt16(lstProductName.SelectedItem.Value.Trim()), 1);
        }//end try
        catch (Exception err)
        {
            CreateLog Log = new CreateLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), err.Message);
            DisplayMessage("Error populating product information");
        }
    }//end product name changed

    //This function populates Product info when adding sales opp
    public void PopulateAddSalesOpp(int ProductCode, int QuantitySelected)
    {
        ProductsBL ProductInfo = new ProductsBL();
        PartnerDS.ProductInfoDataTable dt  = ProductInfo.GetProductInfo(ProductCode);
        if (dt.Rows.Count > 0)
        {
            txtAddCOG.Text = dt[0].COG.ToString();
            txtAddSellPrice.Text = dt[0].SellPrice.ToString();
            txtAddSubtotal.Text = Convert.ToString(Convert.ToDecimal(dt[0].SellPrice.ToString()) * QuantitySelected);

            if ((dt[0].ProductName.ToString().Trim() == "Merchant Number") || (dt[0].ProductName.ToString().Trim() == "Online Debit") || (dt[0].ProductName.ToString().Trim() == "Reprogram"))
                lstAddReprogram.Enabled = true;
            else
            {
                lstAddReprogram.Enabled = false;
                lstAddReprogram.SelectedValue = lstAddReprogram.Items.FindByText("").Value;
            }
        }//end if count not 0
    }//end PopulateAddSalesOpp

    protected void lstAddQuantity_SelectedIndexChanged(object sender, EventArgs e)
    {
        PopulateAddSalesOpp(Convert.ToInt16(lstProductName.SelectedItem.Value.Trim()), Convert.ToInt32(lstAddQuantity.SelectedItem.Text));
    }

    protected void lnkAddSalesOpps_Click(object sender, EventArgs e)
    {
        try
        {
            pnlAddOpp.Visible = true;
            lstAddQuantity.Enabled = false;
            lnkAddSalesOpps.Visible = false;
            if (lstProductName.Items.Count == 0)
            PopulateSalesOppsLists();
        }
        catch (Exception err)
        {
            CreateLog Log = new CreateLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), err.Message);
            DisplayMessage("Add Sales Opp Link Error - " + err.Message);
        }
    }

    protected void btnCancelAdd_Click(object sender, EventArgs e)
    {
        pnlAddOpp.Visible = false;
        lnkAddSalesOpps.Visible = true;
    }

    protected void btnEditCancel_Click(object sender, EventArgs e)
    {
        pnlEditSalesOpp.Visible = false;
        lnkAddSalesOpps.Visible = true;
    }

    /*protected void txtSellPrice_TextChanged(object sender, EventArgs e)
    {
        //if Sell Price is changed then update Subtotal
        double subtotal = Convert.ToDouble(lblQuantity.Text.ToString().Trim()) * Convert.ToDouble(txtSellPrice.Text.ToString().Trim());
        lblSubtotal.Text = subtotal.ToString().Trim();            
    }*/

    protected void lst_ProductNameChanged(object sender, EventArgs e)
    {
        /*
        ProductsBL ProductInfo = new ProductsBL();
        PartnerDS.ProductInfoDataTable dt = ProductInfo.GetProductInfo(Convert.ToInt16(lstProductName.SelectedItem.Value.Trim()));
        if (dt.Rows.Count > 0)
        {
            txtAddCOG.Text = dt[0].COG.ToString();
            txtAddSellPrice.Text = dt[0].SellPrice.ToString();
            txtAddSubtotal.Text = Convert.ToString(Convert.ToDecimal(dt[0].SellPrice.ToString()) * QuantitySelected);

            if ((dt[0].ProductName.ToString().Trim() == "Merchant Number") || (dt[0].ProductName.ToString().Trim() == "Online Debit") || (dt[0].ProductName.ToString().Trim() == "Reprogram"))
                lstAddReprogram.Enabled = true;
            else
            {
                lstAddReprogram.Enabled = false;
                lstAddReprogram.SelectedValue = lstAddReprogram.Items.FindByText("").Value;
            }
        }*/
          //end if count not 0


        ProductsBL salesOppProduct = new ProductsBL();
        PartnerDS.ProductInfoDataTable dt = new PartnerDS.ProductInfoDataTable();
        
        dt = salesOppProduct.GetProductInfo(Convert.ToInt16(listEditProductName.SelectedItem.Value.Trim()));
        if (dt.Rows.Count > 0)
        {
            lblCOG.Text = dt[0].COG.ToString();
            txtSellPrice.Text = dt[0].SellPrice.ToString();
            //txtAddSubtotal.Text = Convert.ToString(Convert.ToDecimal(dt[0].SellPrice.ToString()) * QuantitySelected);
            //PopulateAddSalesOpp(Convert.ToInt16(lstProductName.SelectedItem.Value.Trim()), Convert.ToInt32(lstAddQuantity.SelectedItem.Text));
        }

        //lblCOG.Text = dt[0].COG.ToString();
        //txtSellPrice.Text = dt[0].SellPrice.ToString();
        //PopulateAddSalesOpp(Convert.ToInt16(listEditProductName.SelectedItem.Value.Trim()), 1);
    }

    protected void btnEditSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string SalesOppID = lblID.Text.ToString().Trim();

            BusinessLayer.SalesOppsBL Opp = new BusinessLayer.SalesOppsBL();
            bool retVal = Opp.EditSalesOpp(listEditProductName.SelectedItem.Value.Trim(), txtSellPrice.Text.Trim(), lblCOG.Text.Trim(), lstQuantity.SelectedItem.Value,
                lstEditPaymentMethod.SelectedItem.Value, lstReprogram.SelectedItem.Value,
                lstRepName.SelectedItem.Value, SalesOppID, txtTerminalIDEdit.Text.Trim());
            if (!retVal)
                DisplayMessage("Error Updating Sales Opp");
            else
            {
                //Add action to Log table
                PartnerLogBL LogData = new PartnerLogBL();
                retVal = LogData.InsertLogData(AppId, Convert.ToInt32(Session["AffiliateID"]), "Sales Opp (" + listEditProductName.Text.Trim() + ") Updated");
                DisplayMessage("Sales Opp Updated");           
            }

            pnlAddOpp.Visible = false;
            pnlEditSalesOpp.Visible = false;
            lnkAddSalesOpps.Visible = true;

            //Populate the sales opp table
            PopulateSalesOpps();
        }//end try
        catch (Exception err)
        {
            CreateLog Log = new CreateLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), err.Message);
            DisplayMessage("Error Updating Sales Opp - " + err.Message);
        }
    }

    //This function populates Product info when editing sales opp
    public void PopulateEditSalesOpp(string SalesOppID)
    {
        OnlineAppSalesOppsTableAdapter OnlineAppSalesOppsAdapter = new OnlineAppSalesOppsTableAdapter();
        PartnerDS.OnlineAppSalesOppsDataTable dt = OnlineAppSalesOppsAdapter.GetDataByOppID(new Guid(SalesOppID));

        if (dt.Rows.Count > 0)
        {

            listEditProductName.Items.Add(new ListItem("", ""));

            BusinessLayer.SalesOppsBL Products = new BusinessLayer.SalesOppsBL();
            DataSet dsProducts = Products.GetProducts();
            if (dsProducts.Tables[0].Rows.Count > 0)
            {
                
                //Get list of products for the Add Product panel
                listEditProductName.DataSource = dsProducts;
                listEditProductName.DataTextField = "ProductName";
                listEditProductName.DataValueField = "ProductCode";
                listEditProductName.DataBind();
            }

            if (listEditProductName.Items.FindByValue(dt[0].ProductCode.ToString().Trim()) != null)
            {
                
                listEditProductName.SelectedItem.Text = dt[0].Product.ToString().Trim();
                listEditProductName.SelectedValue = dt[0].ProductCode;  
            }
            

            lblID.Text = dt[0].ID.ToString().Trim();
            //lblProductName.Text = dt[0].Product.ToString().Trim();
            txtSellPrice.Text = dt[0].Price.ToString().Trim();
            lblCOG.Text = dt[0].CostOfGoods.ToString();
            lstQuantity.SelectedValue = lstQuantity.Items.FindByValue(dt[0].Quantity.ToString().Trim()).Value;
            lblSubtotal.Text = dt[0].Subtotal.ToString().Trim();
            lstRepName.SelectedValue = lstRepName.Items.FindByValue(dt[0].RepNum.ToString().Trim()).Value;
            lstEditPaymentMethod.SelectedValue = dt[0].PaymentMethod.ToString();
            if ((dt[0].Product.ToString().Trim() == "Merchant Number") || (dt[0].Product.ToString().Trim() == "Online Debit") || (dt[0].Product.ToString().Trim() == "Reprogram") || (dt[0].Product.ToString().Trim() == "EBT"))
            {
                lstReprogram.Enabled = true;
                lstReprogram.SelectedValue = dt[0].Reprogram.ToString();
            }
            else
            {
                lstReprogram.Enabled = false;
                lstReprogram.SelectedValue = lstReprogram.Items.FindByText("").Value;
            }

            string ConnStringPartner = ConfigurationManager.AppSettings["ConnectionStringPartner"].ToString();
            SqlConnection Conn = new SqlConnection(ConnStringPartner);
            // get all product codes of type terminal or pos
            string strQuery = "SELECT [ProductCode] FROM [AgentPortal].[dbo].[Product] WHERE CategoryID IN (0, 4, 5, 6, 7, 32, 21, 22, 11, 12, 13)";
            SqlCommand cmd = new SqlCommand(strQuery, Conn);
            cmd.Connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            bool enableQuant = true;

            for(int i=0; i<ds.Tables[0].Rows.Count; i++) {
                DataRow dr = ds.Tables[0].Rows[i];
                if(Convert.ToString(dr["ProductCode"]) == Convert.ToString(listEditProductName.SelectedValue)) {
                    enableQuant = false;
                    break;
                }
            }
            
            if(enableQuant) {
                lstQuantity.Enabled = true;
            } else {
                lstQuantity.Enabled = false;
            }
        }//end if count not 0
    }//end PopulateAddSalesOpp

    protected void grdSalesOpps_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "AddToACT")
            {
                //The Last Sync Date is not being updated here because the other sales opps which are added to ACT 
                //will have older sync dates than the new one being added to ACT. So the check for max sync date to set the 
                //linked bit will fail and display all the other sales opps in red.
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow grdRow = grdSalesOpps.Rows[index];
                string ID = Server.HtmlDecode(grdRow.Cells[0].Text);
                //First check if the appid exists in act (as a Primary Record, denoted by TYPENUM = 0)
                ACTDataBL AppIDExists = new ACTDataBL();
                bool retVal = AppIDExists.CheckAppIDExists(AppId);
                if (!retVal)
                    DisplayMessage("This application has not been added to ACT!. Please add it to ACT first.");
                else
                {
                    BusinessLayer.SalesOppsBL SalesOpp = new BusinessLayer.SalesOppsBL();
                    retVal = SalesOpp.InsertSalesOppsInACT(AppId, ID);
                    if (retVal)
                        DisplayMessage("Sales Opps Inserted/Updated in ACT!");
                    else
                        DisplayMessage("Error Inserting/Updating Sales Opp in ACT!");

                    //Populate the sales opp table

                    PopulateSalesOpps();
                }
            }//end if commandname
            if (e.CommandName == "RowEdit")
            {
                pnlAddOpp.Visible = false;
                lnkAddSalesOpps.Visible = false;
                pnlEditSalesOpp.Visible = true;
                PopulateSalesOppsLists();
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow grdRow = grdSalesOpps.Rows[index];
                string SalesOppID = Server.HtmlDecode(grdRow.Cells[0].Text);
                PopulateEditSalesOpp(SalesOppID);
            }

        }//end try
        catch (Exception err)
        {
            CreateLog Log = new CreateLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), err.Message);
            DisplayMessage("Error populating product information");
        }
    }

    protected void grdSalesOpps_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            e.Row.Cells[0].Visible = false;
            //if Admin then delete, edit and Add/Update are visible
            if (User.IsInRole("Admin"))
            {
                if (e.Row.Cells[7].Text == "Sent to Payroll")
                {
                    e.Row.Cells[13].Visible = false;
                    e.Row.Cells[14].Visible = false;
                    e.Row.Cells[15].Visible = false;
                }
                else
                {
                    e.Row.Cells[13].Visible = true;
                    e.Row.Cells[14].Visible = true;
                    e.Row.Cells[15].Visible = true;
                }
            }
            //if Employee then delete, edit and Add/Update are visible only when app is not locked
            else if (User.IsInRole("Employee"))
            {
                if ( e.Row.Cells[7].Text == "Sent to Payroll")
                {
                    e.Row.Cells[13].Visible = false;
                    e.Row.Cells[14].Visible = false;
                    e.Row.Cells[15].Visible = false;
                }
                else if (Locked()) 
                {
                    e.Row.Cells[13].Visible = false;
                    e.Row.Cells[14].Visible = true;
                    e.Row.Cells[15].Visible = true;
                }
                else
                {
                    if (e.Row.Cells[12].Text == "True")
                    {
                        e.Row.Cells[13].Enabled = false;
                    }
                    else {
                        e.Row.Cells[13].Enabled = true;
                    }
                    e.Row.Cells[14].Visible = true;
                    e.Row.Cells[15].Visible = true;
                }
            }
            //if any other user then delete, and Add/Update are not visible
            else
            {
                if (Locked() || e.Row.Cells[7].Text == "Sent to Payroll")
                    e.Row.Cells[13].Visible = false;
                else
                    e.Row.Cells[13].Visible = true;

                e.Row.Cells[12].Visible = false;
                e.Row.Cells[13].Visible = false;
                e.Row.Cells[15].Visible = false;
            }            
        }//end try
        catch (Exception err)
        {
            CreateLog Log = new CreateLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), err.Message);
            DisplayMessage("Error populating product information");
        }
    }

    protected void grdSalesOpps_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            GridViewRow grdRow = grdSalesOpps.Rows[e.RowIndex];
            string ID = Server.HtmlDecode(grdRow.Cells[0].Text);
            BusinessLayer.SalesOppsBL DeleteOpp = new BusinessLayer.SalesOppsBL();
            bool retVal = DeleteOpp.DeleteSalesOpp(ID);
            if (retVal)
            {

                //Add action to Log table
                PartnerLogBL LogData = new PartnerLogBL();
                retVal = LogData.InsertLogData(AppId, Convert.ToInt32(Session["AffiliateID"]), "Sales Opportunity (Name: " + grdRow.Cells[1].Text + ") deleted");
                //Populate the sales opp table

                PopulateSalesOpps();
                DisplayMessage("Sales Opportunity Deleted");
            }
        }//end try
        catch (Exception err)
        {
            CreateLog Log = new CreateLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), err.Message);
            DisplayMessage("Error populating product information");
        }
    }
    #endregion

    #region RATES
    //This function displays rates for this application
    public void PopulateRates()
    {
        try
        {
            if (AppId != 0)
            {
                lnkModifyRates.NavigateUrl = "SetRates.aspx?AppId=" + AppId;
                int PackageID = 0;
                NewAppInfo ReturnApp = new NewAppInfo(AppId);
                PackageID = ReturnApp.ReturnPID();
                OnlineAppProfile AppInfo = new OnlineAppProfile(AppId);
                DataSet dsNewApp = AppInfo.GetProfileData();
                DataTable dtNewApp = dsNewApp.Tables[0];
                if (dtNewApp.Rows.Count > 0)
                {
                    DataRow drNewApp = dtNewApp.Rows[0];
                    int AcctType = Convert.ToInt32(drNewApp["AcctType"]);
                    if (AcctType == 1)
                    {
                        //This is a merchant account only
                        pnlGateway.Visible = false;
                        pnlMerchant.Visible = true;
                        PopulateMerchantRates();
                    }
                    else if (AcctType == 2)
                    {
                        //This is a gateway account only
                        pnlGateway.Visible = true;
                        pnlMerchant.Visible = false;
                        PopulateGatewayRates();
                    }
                    else if (AcctType == 4)
                    {
                        //This is a merchant and a gateway account
                        pnlGateway.Visible = true;
                        pnlMerchant.Visible = true;
                        PopulateMerchantRates();
                        PopulateGatewayRates();
                    }
                    else
                    {
                        pnlGateway.Visible = false;
                        pnlMerchant.Visible = false;
                    }

                    if ((PackageID == 254) || (PackageID == 253))
                    {
                        pnlGateway.Visible = false;
                    }

                    PopulateAddlServices();

                    lblAppFee.Visible = false;
                    lblApplicationFee.Visible = false;
                    lblAppSetupFee.Visible = false;
                    lblApplicationSetupFee.Visible = false;
                    //lblSetupFee.Visible = false;
                    //lblGatewaySetupFee.Visible = false;

                }//end if count not 0            
            }//end if appid not 0
        }//end try
        catch (Exception err)
        {
            DisplayMessage("Error Processing Request. Please contact technical support");
            CreateLog Log = new CreateLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "PopulateRates - " + err.Message);
        }
    }//end function display rates

    //This function populates merchant account rates
    protected void PopulateMerchantRates()
    {
        ProcessingInfo Processing = new ProcessingInfo(AppId);
        DataSet dsProcessingInfo = Processing.GetProcessingInfo();
        OnlineAppClassLibrary.SalesOppsBL SalesOppsFee = new OnlineAppClassLibrary.SalesOppsBL(AppId);
        if (dsProcessingInfo.Tables[0].Rows.Count > 0)
        {
            DataRow drProcessingInfo = dsProcessingInfo.Tables[0].Rows[0];
            //lblApplicationFee.Text = SalesOppsFee.GetAppFeeSalesOpps();//drProcessingInfo["AppFee"].ToString().Trim();
            //lblApplicationSetupFee.Text = SalesOppsFee.GetAppSetupFeeSalesOpps(); //drProcessingInfo["AppSetupFee"].ToString().Trim();
            if (drProcessingInfo["AppFee"] != DBNull.Value)
            {
                if (Convert.ToInt32(drProcessingInfo["AppFee"]) != 0)
                {
                    lblApplicationFee.Text = drProcessingInfo["AppFee"].ToString().Trim();
                }
                else
                {
                    lblApplicationFee.Text = SalesOppsFee.GetAppFeeSalesOpps();
                }
            }

            if (drProcessingInfo["AppSetupFee"] != DBNull.Value)
            {
                if (Convert.ToInt32(drProcessingInfo["AppSetupFee"]) != 0)
                {
                    lblApplicationSetupFee.Text = drProcessingInfo["AppSetupFee"].ToString().Trim();
                }
                else
                {
                    lblApplicationSetupFee.Text = SalesOppsFee.GetAppSetupFeeSalesOpps();
                }
            }

            if (drProcessingInfo["CardPresent"].ToString().Trim() == "CP")
                lblDiscountRate.Text = drProcessingInfo["DiscRateQualPres"].ToString().Trim();
            else
                lblDiscountRate.Text = drProcessingInfo["DiscRateQualNP"].ToString().Trim();
            lblDebitRate.Text = drProcessingInfo["DiscRateQualDebit"].ToString().Trim();
            lblPerAuth.Text = drProcessingInfo["TransactionFee"].ToString().Trim();
            lblMonMin.Text = drProcessingInfo["MonMin"].ToString().Trim();
            lblTollFreeService.Text = drProcessingInfo["CustServFee"].ToString().Trim();
        }//end if count not 0
    }//end function populate merchant rates

    //This function populates gateway rates
    protected void PopulateGatewayRates()
    {
        Gateway GatewayInfo = new Gateway(AppId);
        DataSet dsGatewayInfo = GatewayInfo.GetGatewayInfo();
        OnlineAppClassLibrary.SalesOppsBL SalesOppsFee = new OnlineAppClassLibrary.SalesOppsBL(AppId);
        if (dsGatewayInfo.Tables[0].Rows.Count > 0)
        {
            DataRow drGatewayInfo = dsGatewayInfo.Tables[0].Rows[0];
            lblGateway.Text = drGatewayInfo["Gateway"].ToString().Trim();
            /*if (Convert.ToInt32(drGatewayInfo["GatewaySetupFee"]) != 0)
            {
                lblGatewaySetupFee.Text = drGatewayInfo["GatewaySetupFee"].ToString().Trim();
            }
            else {
                lblGatewaySetupFee.Text = SalesOppsFee.GetGWSetupFeeSalesOpps();
            }*/
            lblGatewayTransFee.Text = drGatewayInfo["GatewayTransFee"].ToString().Trim();
            lblMonthlyGatewayAccess.Text = drGatewayInfo["GatewayMonFee"].ToString().Trim();
        }//end if count not 0
    }//end function populate gateway rates

    protected void PopulateDBEBT()
    {
        Style ValueLabel = new Style();
        ValueLabel.ForeColor = System.Drawing.Color.Black;
        ValueLabel.Font.Size = FontUnit.XSmall;
        ValueLabel.Font.Name = "Arial";

        Style ValueLabelHeader = new Style();
        ValueLabelHeader.ForeColor = System.Drawing.Color.White;
        ValueLabelHeader.Font.Size = FontUnit.Small;
        ValueLabelHeader.Font.Bold = true;
        ValueLabelHeader.Font.Name = "Arial";

         NewAppInfo DBEBT = new NewAppInfo(AppId);
         DataSet dsDBEBT = DBEBT.GetNewAppData();
         if (dsDBEBT.Tables[0].Rows.Count > 0)
        {
            DataRow dr = dsDBEBT.Tables[0].Rows[0];
            ProcessingInfo Processing = new ProcessingInfo(AppId);
            DataSet dsProcessingInfo = Processing.GetAddlServices();

            //Get Setup Fees from Sales Opps
            OnlineAppClassLibrary.SalesOppsBL SalesOpps = new OnlineAppClassLibrary.SalesOppsBL(AppId);
            if (dsProcessingInfo.Tables[0].Rows.Count > 0)
            {
                DataRow drProcessingInfo = dsProcessingInfo.Tables[0].Rows[0];

                TableRow tr = new TableRow();
                //tr.ID = "tr1";
                TableCell td;
                Label lblValue;
                //DropDownList lstValue;
                //Control myControl1;

                ProcessingInfo Proc = new ProcessingInfo(AppId);
                string CardPresent = Proc.ReturnCardPresent();
                
                if (Convert.ToBoolean(dr["OnlineDebit"]) || Convert.ToBoolean(dr["EBT"]))
                {
                    Control ControlHeader = tblAddlServices.FindControl("lblDBEBT");
                    if (ControlHeader == null)
                    {
                        tr = new TableRow();
                        Label lblHearder = new Label();
                        //lblHearder.Text = "Additional Services";
                        lblHearder.ID = "lblDBEBT";
                        td = new TableCell();
                        td.Attributes.Add("align", "center");
                        td.Attributes.Add("colspan", "2");
                        lblHearder.ApplyStyle(ValueLabel);
                        td.Controls.Add(lblHearder);
                        lblHearder.Font.Bold = true;
                        tr.Cells.Add(td);
                        tblDBEBT.Rows.Add(tr);
                    }
                }

                #region OnlineDebit
                if (CardPresent == "CP")
                {
                    if (Convert.ToBoolean(dr["OnlineDebit"]))
                    {
                        tr = new TableRow();
                        //Online Debit Header
                        lblValue = new Label();
                        lblValue.Text = "Online Debit";
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        td.Attributes.Add("colspan", "2");
                        lblValue.ApplyStyle(ValueLabel);
                        td.Controls.Add(lblValue);
                        lblValue.Font.Bold = true;
                        tr.Cells.Add(td);
                        tblDBEBT.Rows.Add(tr);

                        tr = new TableRow();
                        //DebitSetupFeeHeader
                        lblValue = new Label();
                        lblValue.Text = "Setup Fee";
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        td.Attributes.Add("width", "70%");
                        lblValue.ApplyStyle(ValueLabel);
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        //DebitSetupFee
                        lblValue = new Label();
                        lblValue.Text = "$ " + SalesOpps.GetODSetupFeeSalesOpps();
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        lblValue.ApplyStyle(ValueLabel);
                        lblValue.Font.Bold = true;
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        tblDBEBT.Rows.Add(tr);

                        tr = new TableRow();
                        //DebitMonFeeHeader
                        lblValue = new Label();
                        lblValue.Text = "Monthly Fee";
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        td.Attributes.Add("width", "70%");
                        lblValue.ApplyStyle(ValueLabel);
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        //DebitMonFee
                        lblValue = new Label();
                        lblValue.Text = "$ " + drProcessingInfo["DebitMonFee"].ToString().Trim();
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        lblValue.ApplyStyle(ValueLabel);
                        lblValue.Font.Bold = true;
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        tblDBEBT.Rows.Add(tr);

                        tr = new TableRow();
                        //DebitMonFeeHeader
                        lblValue = new Label();
                        lblValue.Text = "Transaction Fee";
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        td.Attributes.Add("width", "70%");
                        lblValue.ApplyStyle(ValueLabel);
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        //DebitMonFee
                        lblValue = new Label();
                        lblValue.Text = "$ " + drProcessingInfo["DebitTransFee"].ToString().Trim();
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        lblValue.ApplyStyle(ValueLabel);
                        lblValue.Font.Bold = true;
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        tblDBEBT.Rows.Add(tr);
                    }//end if online debit                    
                }//end if card present
                #endregion

                #region EBT
                if (CardPresent == "CP")
                {
                    if (Convert.ToBoolean(dr["EBT"]))
                    {
                        tr = new TableRow();
                        //EBT Header
                        lblValue = new Label();
                        lblValue.Text = "Electronic Benefits Transfer (EBT)";
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        td.Attributes.Add("colspan", "2");
                        lblValue.ApplyStyle(ValueLabel);
                        td.Controls.Add(lblValue);
                        lblValue.Font.Bold = true;
                        tr.Cells.Add(td);
                        tblDBEBT.Rows.Add(tr);

                        tr = new TableRow();
                        //EBTSetupFeeHeader                        
                        lblValue = new Label();
                        lblValue.Text = "Setup Fee";
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        td.Attributes.Add("width", "70%");
                        lblValue.ApplyStyle(ValueLabel);
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        //EBTSetupFee
                        lblValue = new Label();
                        lblValue.Text = "$ " + SalesOpps.GetEBTSetupFeeSalesOpps();
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        lblValue.ApplyStyle(ValueLabel);
                        lblValue.Font.Bold = true;
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        tblDBEBT.Rows.Add(tr);

                        tr = new TableRow();
                        //DebitMonFeeHeader
                        lblValue = new Label();
                        lblValue.Text = "Monthly Fee";
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        lblValue.ApplyStyle(ValueLabel);
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        //DebitMonFee
                        lblValue = new Label();
                        lblValue.Text = "$ " + drProcessingInfo["EBTMonFee"].ToString().Trim();
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        lblValue.ApplyStyle(ValueLabel);
                        lblValue.Font.Bold = true;
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        tblDBEBT.Rows.Add(tr);

                        tr = new TableRow();
                        //DebitMonFeeHeader
                        lblValue = new Label();
                        lblValue.Text = "Transaction Fee";
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        lblValue.ApplyStyle(ValueLabel);
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        //DebitMonFee
                        lblValue = new Label();
                        lblValue.Text = "$ " + drProcessingInfo["EBTTransFee"].ToString().Trim();
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        lblValue.ApplyStyle(ValueLabel);
                        lblValue.Font.Bold = true;
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        tblDBEBT.Rows.Add(tr);
                    }//end if EBT
                }//end if card present

                #endregion
            
            
            }
        }
    }

    //This function populates Additional services rates
    protected void PopulateAddlServices()
    {
        Style ValueLabel = new Style();
        ValueLabel.ForeColor = System.Drawing.Color.Black;
        ValueLabel.Font.Size = FontUnit.XSmall;
        ValueLabel.Font.Name = "Arial";

        Style ValueLabelHeader = new Style();
        ValueLabelHeader.ForeColor = System.Drawing.Color.White;
        ValueLabelHeader.Font.Size = FontUnit.Small;
        ValueLabelHeader.Font.Bold = true;
        ValueLabelHeader.Font.Name = "Arial";

        //Get Additional Services Status
       
        /*
        if (!User.IsInRole("Admin") && !User.IsInRole("Employee"))
        {
            OnlineAppStatusBL Status = new OnlineAppStatusBL(AppId);
            DataSet dsStatus = Status.GetStatusList("Agent", "Merchant");
            if (dsStatus.Tables[0].Rows.Count > 0)
            {
                lstMerchantStatus.DataSource = dsStatus;
                lstMerchantStatus.DataTextField = "Status";
                lstMerchantStatus.DataValueField = "Status";
                lstMerchantStatus.DataBind();
            }

            DataSet dsStatusGW = Status.GetStatusList("Agent", "Gateway");
            if (dsStatusGW.Tables[0].Rows.Count > 0)
            {
                lstGatewayStatus.DataSource = dsStatusGW;
                lstGatewayStatus.DataTextField = "Status";
                lstGatewayStatus.DataValueField = "Status";
            }

            lstMerchantStatus.Enabled = false;
            lstGatewayStatus.Enabled = false;
            lstPlatform.Enabled = false;
            btnSubmit.Enabled = false;
            btnSubmit.Visible = false;
        }
        else
        {
            //Load full status list for admins and employees
            OnlineAppStatusBL Status = new OnlineAppStatusBL(AppId);
            DataSet dsStatus = Status.GetStatusList("Admin", "Merchant");
            if (dsStatus.Tables[0].Rows.Count > 0)
            {
                lstMerchantStatus.DataSource = dsStatus;
                lstMerchantStatus.DataTextField = "Status";
                lstMerchantStatus.DataValueField = "Status";
                lstMerchantStatus.DataBind();
            }

            DataSet dsStatusGW = Status.GetStatusList("Admin", "Gateway");
            if (dsStatusGW.Tables[0].Rows.Count > 0)
            {
                lstGatewayStatus.DataSource = dsStatusGW;
                lstGatewayStatus.DataTextField = "Status";
                lstGatewayStatus.DataValueField = "Status";
                lstGatewayStatus.DataBind();
            }
        }*/

        //Get Additional services info from online app new app
        NewAppInfo AddlServ = new NewAppInfo(AppId);
        DataSet dsAddl = AddlServ.GetNewAppData();
        if (dsAddl.Tables[0].Rows.Count > 0)
        {
            DataRow dr = dsAddl.Tables[0].Rows[0];
            ProcessingInfo Processing = new ProcessingInfo(AppId);
            DataSet dsProcessingInfo = Processing.GetAddlServices();

            //Get Setup Fees from Sales Opps
            OnlineAppClassLibrary.SalesOppsBL SalesOpps = new OnlineAppClassLibrary.SalesOppsBL(AppId);
            if (dsProcessingInfo.Tables[0].Rows.Count > 0)
            {
                DataRow drProcessingInfo = dsProcessingInfo.Tables[0].Rows[0];

                TableRow tr = new TableRow();
                //tr.ID = "tr1";
                TableCell td;
                Label lblValue;
                DropDownList lstValue;
                //Control myControl1;
                ImageButton imgButtonAddlGift;
                ImageButton imgButtonAddlLease;
                ImageButton imgButtonAddlMCA;

                ProcessingInfo Proc = new ProcessingInfo(AppId);
                string CardPresent = Proc.ReturnCardPresent();


                if (Convert.ToBoolean(dr["OnlineDebit"]) || Convert.ToBoolean(dr["CheckServices"]) || Convert.ToBoolean(dr["GiftCard"])
                    || Convert.ToBoolean(dr["EBT"]) || Convert.ToBoolean(dr["Payroll"]) || Convert.ToBoolean(dr["MerchantFunding"]) || Convert.ToBoolean(dr["Lease"]))
                {
                    Control ControlHeader = tblAddlServices.FindControl("lblAdditionalServices");
                    if (ControlHeader == null)
                    {
                        tr = new TableRow();
                        Label lblHearder = new Label();
                        lblHearder.Text = "Additional Services";
                        lblHearder.ID = "lblAdditionalServices";
                        td = new TableCell();
                        td.Attributes.Add("align", "center");
                        td.Attributes.Add("colspan", "2");
                        lblHearder.ApplyStyle(ValueLabel);
                        td.Controls.Add(lblHearder);
                        lblHearder.Font.Bold = true;
                        tr.Cells.Add(td);
                        tblAddlServices.Rows.Add(tr);
                    }
                }
                /*
                #region OnlineDebit
                if (CardPresent == "CP")
                {
                    if (Convert.ToBoolean(dr["OnlineDebit"]))
                    {
                        tr = new TableRow();
                        //Online Debit Header
                        lblValue = new Label();
                        lblValue.Text = "Online Debit";
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        td.Attributes.Add("colspan", "2");
                        lblValue.ApplyStyle(ValueLabel);
                        td.Controls.Add(lblValue);
                        lblValue.Font.Bold = true;
                        tr.Cells.Add(td);
                        tblAddlServices.Rows.Add(tr);

                        tr = new TableRow();
                        //DebitSetupFeeHeader
                        lblValue = new Label();
                        lblValue.Text = "Setup Fee";
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        td.Attributes.Add("width", "70%");
                        lblValue.ApplyStyle(ValueLabel);
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        //DebitSetupFee
                        lblValue = new Label();
                        lblValue.Text = "$ " + SalesOpps.GetODSetupFeeSalesOpps();
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        lblValue.ApplyStyle(ValueLabel);
                        lblValue.Font.Bold = true;
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        tblAddlServices.Rows.Add(tr);

                        tr = new TableRow();
                        //DebitMonFeeHeader
                        lblValue = new Label();
                        lblValue.Text = "Monthly Fee";
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        td.Attributes.Add("width", "70%");
                        lblValue.ApplyStyle(ValueLabel);
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        //DebitMonFee
                        lblValue = new Label();
                        lblValue.Text = "$ " + drProcessingInfo["DebitMonFee"].ToString().Trim();
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        lblValue.ApplyStyle(ValueLabel);
                        lblValue.Font.Bold = true;
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        tblAddlServices.Rows.Add(tr);

                        tr = new TableRow();
                        //DebitMonFeeHeader
                        lblValue = new Label();
                        lblValue.Text = "Transaction Fee";
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        td.Attributes.Add("width", "70%");
                        lblValue.ApplyStyle(ValueLabel);
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        //DebitMonFee
                        lblValue = new Label();
                        lblValue.Text = "$ " + drProcessingInfo["DebitTransFee"].ToString().Trim();
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        lblValue.ApplyStyle(ValueLabel);
                        lblValue.Font.Bold = true;
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        tblAddlServices.Rows.Add(tr);
                    }//end if online debit                    
                }//end if card present
               #endregion

                #region EBT
                if (CardPresent == "CP")
                {


                    if (Convert.ToBoolean(dr["EBT"]))
                    {
                        tr = new TableRow();
                        //EBT Header
                        lblValue = new Label();
                        lblValue.Text = "EBT";
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        td.Attributes.Add("colspan", "2");
                        lblValue.ApplyStyle(ValueLabel);
                        td.Controls.Add(lblValue);
                        lblValue.Font.Bold = true;
                        tr.Cells.Add(td);
                        tblAddlServices.Rows.Add(tr);

                        tr = new TableRow();
                        //EBTSetupFeeHeader                        
                        lblValue = new Label();
                        lblValue.Text = "Setup Fee";
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        td.Attributes.Add("width", "70%");
                        lblValue.ApplyStyle(ValueLabel);
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        //EBTSetupFee
                        lblValue = new Label();
                        lblValue.Text = "$ " + SalesOpps.GetEBTSetupFeeSalesOpps();
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        lblValue.ApplyStyle(ValueLabel);
                        lblValue.Font.Bold = true;
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        tblAddlServices.Rows.Add(tr);

                        tr = new TableRow();
                        //DebitMonFeeHeader
                        lblValue = new Label();
                        lblValue.Text = "Monthly Fee";
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        lblValue.ApplyStyle(ValueLabel);
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        //DebitMonFee
                        lblValue = new Label();
                        lblValue.Text = "$ " + drProcessingInfo["EBTMonFee"].ToString().Trim();
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        lblValue.ApplyStyle(ValueLabel);
                        lblValue.Font.Bold = true;
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        tblAddlServices.Rows.Add(tr);

                        tr = new TableRow();
                        //DebitMonFeeHeader
                        lblValue = new Label();
                        lblValue.Text = "Transaction Fee";
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        lblValue.ApplyStyle(ValueLabel);
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        //DebitMonFee
                        lblValue = new Label();
                        lblValue.Text = "$ " + drProcessingInfo["EBTTransFee"].ToString().Trim();
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        lblValue.ApplyStyle(ValueLabel);
                        lblValue.Font.Bold = true;
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        tblAddlServices.Rows.Add(tr);
                    }//end if EBT
                }//end if card present

#endregion
                */

                #region CheckServices
                if (Convert.ToBoolean(dr["CheckServices"]))
                {

                    Control ControlCheckService = tblAddlServices.FindControl("lstStatusCheckService");
                    //System.Diagnostics.Debugger.Break();
                    if (ControlCheckService == null)
                    {
                       tr = new TableRow();
                        //Check Guarantee Header
                        lblValue = new Label();
                        lblValue.Text = "Check Services";
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        td.Attributes.Add("colspan", "2");
                        lblValue.ApplyStyle(ValueLabel);
                        td.Controls.Add(lblValue);
                        lblValue.Font.Bold = true;
                        tr.Cells.Add(td);
                        tblAddlServices.Rows.Add(tr);
                        /*
                        tr = new TableRow();
                        //CSSetupFeeHeader
                        lblValue = new Label();
                        lblValue.Text = "Setup Fee";

                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        td.Attributes.Add("width", "70%");
                        lblValue.ApplyStyle(ValueLabel);
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        //CSSetupFee
                        lblValue = new Label();
                        lblValue.Text = "$ " + SalesOpps.GetCSSetupFeeSalesOpps();
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        lblValue.ApplyStyle(ValueLabel);
                        lblValue.Font.Bold = true;
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        tblAddlServices.Rows.Add(tr);*/

                        tr = new TableRow();
                        //DebitMonFeeHeader
                        lblValue = new Label();
                        lblValue.Text = "Discount Rate";
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        td.Attributes.Add("width", "70%");
                        lblValue.ApplyStyle(ValueLabel);
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        //DebitMonFee
                        lblValue = new Label();
                        lblValue.Text = "$ " + drProcessingInfo["CGDiscRate"].ToString().Trim();
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        lblValue.ApplyStyle(ValueLabel);
                        lblValue.Font.Bold = true;
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        tblAddlServices.Rows.Add(tr);

                        tr = new TableRow();
                        //DebitMonFeeHeader
                        lblValue = new Label();
                        lblValue.Text = "Monthly Fee";
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        td.Attributes.Add("width", "70%");
                        lblValue.ApplyStyle(ValueLabel);
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        //DebitMonFee
                        lblValue = new Label();
                        lblValue.Text = "$ " + drProcessingInfo["CGMonFee"].ToString().Trim();
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        lblValue.ApplyStyle(ValueLabel);
                        lblValue.Font.Bold = true;
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        tblAddlServices.Rows.Add(tr);

                        tr = new TableRow();
                        //DebitMonFeeHeader
                        lblValue = new Label();
                        lblValue.Text = "Monthly Minimum";
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        td.Attributes.Add("width", "70%");
                        lblValue.ApplyStyle(ValueLabel);
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        //DebitMonFee
                        lblValue = new Label();
                        lblValue.Text = "$ " + drProcessingInfo["CGMonMin"].ToString().Trim();
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        lblValue.ApplyStyle(ValueLabel);
                        lblValue.Font.Bold = true;
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        tblAddlServices.Rows.Add(tr);

                        tr = new TableRow();
                        //DebitMonFeeHeader
                        lblValue = new Label();
                        lblValue.Text = "Transaction Fee";
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        td.Attributes.Add("width", "70%");
                        lblValue.ApplyStyle(ValueLabel);
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        //DebitMonFee
                        lblValue = new Label();
                        lblValue.Text = "$ " + drProcessingInfo["CGTransFee"].ToString().Trim();
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        lblValue.ApplyStyle(ValueLabel);
                        lblValue.Font.Bold = true;
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        tblAddlServices.Rows.Add(tr);

                        //lstValue = myControl1;

                        tr = new TableRow();
                        DropDownList lstValueCheckServ = new DropDownList();
                        lstValueCheckServ.ID = "lstStatusCheckService";

                        if (!User.IsInRole("Admin") && !User.IsInRole("Employee"))
                        {
                            OnlineAppStatusBL Status = new OnlineAppStatusBL(AppId);
                            DataSet dsStatusGW = Status.GetStatusList("Agent", "CheckServ");
                            if (dsStatusGW.Tables[0].Rows.Count > 0)
                            {
                                lstValueCheckServ.DataSource = dsStatusGW;
                                lstValueCheckServ.DataTextField = "Status";
                                lstValueCheckServ.DataValueField = "Status";
                                lstValueCheckServ.DataBind();
                            }
                        }
                        else
                        {
                            OnlineAppStatusBL Status = new OnlineAppStatusBL(AppId);
                            DataSet dsStatusGW = Status.GetStatusList("Admin", "CheckServ");
                            if (dsStatusGW.Tables[0].Rows.Count > 0)
                            {
                                lstValueCheckServ.DataSource = dsStatusGW;
                                lstValueCheckServ.DataTextField = "Status";
                                lstValueCheckServ.DataValueField = "Status";
                                lstValueCheckServ.DataBind();
                            }
                        }
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        lstValueCheckServ.Font.Bold = true;
                        lstValueCheckServ.ID = "lstStatusCheckService";
                        //this.form1.Controls.Add(lstValueCheckServ);
                        td.Controls.Add(lstValueCheckServ);
                        tr.Cells.Add(td);
                        
                        tblAddlServices.Rows.Add(tr);
                    }
                    else if (CheckServiceLocked())
                    {
                        ListControl lstControlCheck = (ListControl)tblAddlServices.FindControl("lstStatusCheckService");
                        if (User.IsInRole("Admin"))
                        {
                            lstControlCheck.Enabled = true;
                        }
                        else
                        {
                            lstControlCheck.Enabled = false;
                        }
                    }
                }
                #endregion

                #region GiftCard
                if (Convert.ToBoolean(dr["GiftCard"]))
                {


                    Control ControlGift = tblAddlServices.FindControl("lstStatusGift");
                    if (ControlGift == null)
                    {
                        tr = new TableRow();
                        //Gift Card Header
                        lblValue = new Label();
                        lblValue.Text = "Gift Card";
                        imgButtonAddlGift = new ImageButton();
                        imgButtonAddlGift.ImageUrl = "~/Images/CreateIMSPDF.gif";
                        imgButtonAddlGift.OnClientClick = "form1.target ='_blank';";
                        imgButtonAddlGift.Click += new ImageClickEventHandler(this.imgGiftCardPDF_Click);
                        imgButtonAddlGift.ID = "imgButtonGift";
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        td.Attributes.Add("colspan", "2");
                        lblValue.ApplyStyle(ValueLabel);
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);
                        td = new TableCell();
                        td.Attributes.Add("align", "right");
                        td.Attributes.Add("colspan", "1");
                        td.Controls.Add(imgButtonAddlGift);
                        lblValue.Font.Bold = true;
                        tr.Cells.Add(td);
                        
                        tblAddlServices.Rows.Add(tr);
                        /*
                        tr = new TableRow();
                        //GCSetupFeeHeader
                        lblValue = new Label();
                        lblValue.Text = "Setup Fee";
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        td.Attributes.Add("width", "70%");
                        lblValue.ApplyStyle(ValueLabel);
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        //GCSetupFee
                        lblValue = new Label();
                        lblValue.Text = "$ " + SalesOpps.GetGCSetupFeeSalesOpps();
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        lblValue.ApplyStyle(ValueLabel);
                        lblValue.Font.Bold = true;
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        tblAddlServices.Rows.Add(tr);*/

                        tr = new TableRow();
                        //DebitMonFeeHeader
                        lblValue = new Label();
                        lblValue.Text = "Monthly Fee";
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        td.Attributes.Add("width", "70%");
                        lblValue.ApplyStyle(ValueLabel);
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        //DebitMonFee
                        lblValue = new Label();
                        lblValue.Text = "$ " + drProcessingInfo["GCMonFee"].ToString().Trim();
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        lblValue.ApplyStyle(ValueLabel);
                        lblValue.Font.Bold = true;
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        tblAddlServices.Rows.Add(tr);

                        tr = new TableRow();
                        //DebitMonFeeHeader
                        lblValue = new Label();
                        lblValue.Text = "Transaction Fee";
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        td.Attributes.Add("width", "70%");
                        lblValue.ApplyStyle(ValueLabel);
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        //DebitMonFee
                        lblValue = new Label();
                        lblValue.Text = "$ " + drProcessingInfo["GCTransFee"].ToString().Trim();
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        lblValue.ApplyStyle(ValueLabel);
                        lblValue.Font.Bold = true;
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        tblAddlServices.Rows.Add(tr);

                        tr = new TableRow();
                        DropDownList lstGift = new DropDownList();
                        if (!User.IsInRole("Admin") && !User.IsInRole("Employee"))
                        {
                            OnlineAppStatusBL Status = new OnlineAppStatusBL(AppId);
                            DataSet dsStatusGW = Status.GetStatusList("Agent", "Gift");
                            if (dsStatusGW.Tables[0].Rows.Count > 0)
                            {
                                lstGift.DataSource = dsStatusGW;
                                lstGift.DataTextField = "Status";
                                lstGift.DataValueField = "Status";
                                lstGift.DataBind();
                            }
                        }
                        else
                        {
                            OnlineAppStatusBL Status = new OnlineAppStatusBL(AppId);
                            DataSet dsStatusGW = Status.GetStatusList("Admin", "Gift");
                            if (dsStatusGW.Tables[0].Rows.Count > 0)
                            {
                                lstGift.DataSource = dsStatusGW;
                                lstGift.DataTextField = "Status";
                                lstGift.DataValueField = "Status";
                                lstGift.DataBind();
                            }
                        }
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        lstGift.Font.Bold = true;
                        lstGift.ID = "lstStatusGift";
                        //this.form1.Controls.Add(lstGift);
                        td.Controls.Add(lstGift);
                        tr.Cells.Add(td);

                        tblAddlServices.Rows.Add(tr);
                    }
                    else if (GiftLocked())
                    {
                        ListControl lstControlGift = (ListControl)tblAddlServices.FindControl("lstStatusGift");
                        if (lstControlGift != null)
                        {
                            if (User.IsInRole("Admin"))
                            {
                                lstControlGift.Enabled = true;
                            }
                            else
                            {
                                lstControlGift.Enabled = false;
                            }
                        }
                    }
                }//end if giftcard
                #endregion

                #region lease
                if (Convert.ToBoolean(dr["Lease"]))
                {

                    Control ControlLease = tblAddlServices.FindControl("lstStatusLease");
                    if (ControlLease == null)
                    {
                        tr = new TableRow();
                        //Lease Header
                        lblValue = new Label();
                        lblValue.Text = "Lease";
                        imgButtonAddlLease = new ImageButton();
                        imgButtonAddlLease.ImageUrl = "~/Images/CreateIMSPDF.gif";
                        imgButtonAddlLease.OnClientClick = "form1.target ='_blank';";
                        imgButtonAddlLease.Click += new ImageClickEventHandler(this.imgLeasePDF_Click);
                        imgButtonAddlLease.ID = "imgButtonLease";
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        td.Attributes.Add("colspan", "2");
                        lblValue.ApplyStyle(ValueLabel);
                        td.Controls.Add(lblValue);
                        lblValue.Font.Bold = true;
                        tr.Cells.Add(td);
                        td = new TableCell();
                        td.Attributes.Add("align", "right");
                        td.Attributes.Add("colspan", "1");
                        td.Controls.Add(imgButtonAddlLease);
                        tr.Cells.Add(td);
                        tblAddlServices.Rows.Add(tr);

                        /*
                        tr = new TableRow();
                        //EBTSetupFeeHeader                        
                        lblValue = new Label();
                        lblValue.Text = "Setup Fee";
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        td.Attributes.Add("width", "70%");
                        lblValue.ApplyStyle(ValueLabel);
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        //EBTSetupFee
                        lblValue = new Label();
                        lblValue.Text = "$ " + SalesOpps.GetEBTSetupFeeSalesOpps();
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        lblValue.ApplyStyle(ValueLabel);
                        lblValue.Font.Bold = true;
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        tblAddlServices.Rows.Add(tr);*/

                        tr = new TableRow();
                        //DebitMonFeeHeader
                        lblValue = new Label();
                        lblValue.Text = "Lease Payment";
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        lblValue.ApplyStyle(ValueLabel);
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        //DebitMonFee
                        lblValue = new Label();
                        lblValue.Text = "$ " + drProcessingInfo["LeasePayment"].ToString().Trim();
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        lblValue.ApplyStyle(ValueLabel);
                        lblValue.Font.Bold = true;
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        tblAddlServices.Rows.Add(tr);

                        tr = new TableRow();
                        //DebitMonFeeHeader
                        lblValue = new Label();
                        lblValue.Text = "Lease Term";
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        lblValue.ApplyStyle(ValueLabel);
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        //DebitMonFee
                        lblValue = new Label();
                        lblValue.Text = drProcessingInfo["LeaseTerm"].ToString().Trim();
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        lblValue.ApplyStyle(ValueLabel);
                        lblValue.Font.Bold = true;
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        tblAddlServices.Rows.Add(tr);

                        tr = new TableRow();
                        //tr.ID = "tr2";
                        //this.form1.Controls.Add(tr);
                        DropDownList lstLease = new DropDownList();
                        if (!User.IsInRole("Admin") && !User.IsInRole("Employee"))
                        {
                            OnlineAppStatusBL Status = new OnlineAppStatusBL(AppId);
                            DataSet dsStatusGW = Status.GetStatusList("Agent", "Lease");
                            if (dsStatusGW.Tables[0].Rows.Count > 0)
                            {
                                lstLease.DataSource = dsStatusGW;
                                lstLease.DataTextField = "Status";
                                lstLease.DataValueField = "Status";
                                lstLease.DataBind();
                            }
                        }
                        else
                        {
                            OnlineAppStatusBL Status = new OnlineAppStatusBL(AppId);
                            DataSet dsStatusGW = Status.GetStatusList("Admin", "Lease");
                            if (dsStatusGW.Tables[0].Rows.Count > 0)
                            {
                                lstLease.DataSource = dsStatusGW;
                                lstLease.DataTextField = "Status";
                                lstLease.DataValueField = "Status";
                                lstLease.DataBind();
                            }
                        }
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        lstLease.SelectedIndexChanged += lstLease_SelectedIndexChanged;
                        lstLease.ID = "lstStatusLease";
                        lstLease.Font.Bold = true;
                        //this.form1.TabRates.pnlAdditionalServices.Controls.Add(lstLease);
                        td.Controls.Add(lstLease);
                        tr.Cells.Add(td);
                        
                        tblAddlServices.Rows.Add(tr);
                    }

                }
                else if (LeaseLocked())
                {
                    ListControl lstControlLease = (ListControl)tblAddlServices.FindControl("lstStatusLease");
                    if (lstControlLease != null)
                    {
                        if (User.IsInRole("Admin"))
                        {
                            lstControlLease.Enabled = true;
                        }
                        else
                        {
                            lstControlLease.Enabled = false;
                        }
                    }
                }
                #endregion

                #region Merchant Cash Advance
                if (Convert.ToBoolean(dr["MerchantFunding"]))
                {


                    Control ControlMCA = tblAddlServices.FindControl("lstStatusMCA");
                    if (ControlMCA == null)
                    {
                        tr = new TableRow();
                        //Lease Header
                        lblValue = new Label();
                        lblValue.Text = "Cash Advance";
                        imgButtonAddlMCA = new ImageButton();
                        imgButtonAddlMCA.ImageUrl = "~/Images/CreateIMSPDF.gif";
                        imgButtonAddlMCA.OnClientClick = "form1.target ='_blank';";
                        imgButtonAddlMCA.Click += new ImageClickEventHandler(this.imgMCAPDF_Click);
                        imgButtonAddlMCA.ID = "imgButtonMCA";
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        td.Attributes.Add("colspan", "1");
                        lblValue.ApplyStyle(ValueLabel);
                        td.Controls.Add(lblValue);
                        lblValue.Font.Bold = true;
                        tr.Cells.Add(td);
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        td.Attributes.Add("colspan", "1");
                        Label lblValueMCACompany = new Label();
                        lblValueMCACompany.Text = drProcessingInfo["MCAType"].ToString().Trim();
                        lblValue.ApplyStyle(ValueLabel);
                        td.Controls.Add(lblValueMCACompany);
                        lblValueMCACompany.Font.Bold = true;
                        tr.Cells.Add(td);
                        td = new TableCell();
                        td.Attributes.Add("align", "right");
                        td.Attributes.Add("colspan", "1");
                        td.Controls.Add(imgButtonAddlMCA);
                        tr.Cells.Add(td);
                        tblAddlServices.Rows.Add(tr);

                        /*
                        tr = new TableRow();
                        //EBTSetupFeeHeader                        
                        lblValue = new Label();
                        lblValue.Text = "Setup Fee";
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        td.Attributes.Add("width", "70%");
                        lblValue.ApplyStyle(ValueLabel);
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        //EBTSetupFee
                        lblValue = new Label();
                        lblValue.Text = "$ " + SalesOpps.GetEBTSetupFeeSalesOpps();
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        lblValue.ApplyStyle(ValueLabel);
                        lblValue.Font.Bold = true;
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        tblAddlServices.Rows.Add(tr);*/

                        /*
                        tr = new TableRow();
                        //DebitMonFeeHeader
                        lblValue = new Label();
                        lblValue.Text = "MCA Company";
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        lblValue.ApplyStyle(ValueLabel);
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        //DebitMonFee
                        lblValue = new Label();
                        lblValue.Text = drProcessingInfo["MCAType"].ToString().Trim();
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        lblValue.ApplyStyle(ValueLabel);
                        lblValue.Font.Bold = true;
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        tblAddlServices.Rows.Add(tr);*/

                        tr = new TableRow();
                        //DebitMonFeeHeader
                        lblValue = new Label();
                        lblValue.Text = "Cash Advance Amount";
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        lblValue.ApplyStyle(ValueLabel);
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        //DebitMonFee
                        lblValue = new Label();
                        lblValue.Text = "$ " + drProcessingInfo["MCAAmount"].ToString().Trim();
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        lblValue.ApplyStyle(ValueLabel);
                        lblValue.Font.Bold = true;
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        tblAddlServices.Rows.Add(tr);

                        tr = new TableRow();
                        DropDownList lstMCA = new DropDownList();
                        if (!User.IsInRole("Admin") && !User.IsInRole("Employee"))
                        {
                            OnlineAppStatusBL Status = new OnlineAppStatusBL(AppId);
                            DataSet dsStatusGW = Status.GetStatusList("Agent", "MerchFund");
                            if (dsStatusGW.Tables[0].Rows.Count > 0)
                            {
                                lstMCA.DataSource = dsStatusGW;
                                lstMCA.DataTextField = "Status";
                                lstMCA.DataValueField = "Status";
                                lstMCA.DataBind();
                            }
                        }
                        else
                        {
                            OnlineAppStatusBL Status = new OnlineAppStatusBL(AppId);
                            DataSet dsStatusGW = Status.GetStatusList("Admin", "MerchFund");
                            if (dsStatusGW.Tables[0].Rows.Count > 0)
                            {
                                lstMCA.DataSource = dsStatusGW;
                                lstMCA.DataTextField = "Status";
                                lstMCA.DataValueField = "Status";
                                lstMCA.DataBind();
                            }
                        }
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        lstMCA.Font.Bold = true;
                        lstMCA.ID = "lstStatusMCA";
                        //this.form1.Controls.Add(lstMCA);
                        td.Controls.Add(lstMCA);
                        tr.Cells.Add(td);
                        tblAddlServices.Rows.Add(tr);
                    }
                    else if (MCALocked())
                    {
                        ListControl lstControlMCA = (ListControl)tblAddlServices.FindControl("lstStatusMCA");
                        if (lstControlMCA != null)
                        {
                            if (User.IsInRole("Admin"))
                            {
                                lstControlMCA.Enabled = true;
                            }
                            else
                            {
                                lstControlMCA.Enabled = false;
                            }
                        }
                    }
                }
                #endregion

                #region payroll
                if (Convert.ToBoolean(dr["Payroll"]))
                {


                    Control ControlPayroll = tblAddlServices.FindControl("lstStatusPayroll");
                    if (ControlPayroll == null)
                    {

                        tr = new TableRow();
                        //Lease Header
                        lblValue = new Label();
                        lblValue.Text = "Payroll";
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        td.Attributes.Add("colspan", "2");
                        lblValue.ApplyStyle(ValueLabel);
                        td.Controls.Add(lblValue);
                        lblValue.Font.Bold = true;
                        tr.Cells.Add(td);
                        tblAddlServices.Rows.Add(tr);

                        /*
                        tr = new TableRow();
                        //EBTSetupFeeHeader                        
                        lblValue = new Label();
                        lblValue.Text = "Setup Fee";
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        td.Attributes.Add("width", "70%");
                        lblValue.ApplyStyle(ValueLabel);
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        //EBTSetupFee
                        lblValue = new Label();
                        lblValue.Text = "$ " + SalesOpps.GetEBTSetupFeeSalesOpps();
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        lblValue.ApplyStyle(ValueLabel);
                        lblValue.Font.Bold = true;
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        tblAddlServices.Rows.Add(tr);*/

                        tr = new TableRow();
                        //DebitMonFeeHeader
                        lblValue = new Label();
                        lblValue.Text = "Payroll";
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        lblValue.ApplyStyle(ValueLabel);
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        //DebitMonFee
                        lblValue = new Label();
                        lblValue.Text = drProcessingInfo["PayrollType"].ToString().Trim();
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        lblValue.ApplyStyle(ValueLabel);
                        lblValue.Font.Bold = true;
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        tblAddlServices.Rows.Add(tr);

                        tr = new TableRow();
                        DropDownList lstPayroll= new DropDownList();
                        if (!User.IsInRole("Admin") && !User.IsInRole("Employee"))
                        {
                            OnlineAppStatusBL Status = new OnlineAppStatusBL(AppId);
                            DataSet dsStatusGW = Status.GetStatusList("Agent", "Payroll");
                            if (dsStatusGW.Tables[0].Rows.Count > 0)
                            {
                                lstPayroll.DataSource = dsStatusGW;
                                lstPayroll.DataTextField = "Status";
                                lstPayroll.DataValueField = "Status";
                                lstPayroll.DataBind();
                            }
                        }
                        else
                        {
                            OnlineAppStatusBL Status = new OnlineAppStatusBL(AppId);
                            DataSet dsStatusGW = Status.GetStatusList("Admin", "Payroll");
                            if (dsStatusGW.Tables[0].Rows.Count > 0)
                            {
                                lstPayroll.DataSource = dsStatusGW;
                                lstPayroll.DataTextField = "Status";
                                lstPayroll.DataValueField = "Status";
                                lstPayroll.DataBind();
                            }
                        }
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        lstPayroll.Font.Bold = true;
                        lstPayroll.ID = "lstStatusPayroll";
                        //this.form1.Controls.Add(lstPayroll);
                        td.Controls.Add(lstPayroll);
                        
                        tr.Cells.Add(td);
                        //this.form1.Controls.Add(lstPayroll);
                        tblAddlServices.Rows.Add(tr);
                    }
                    else if (PayrollLocked())
                    {
                        ListControl lstControlPayroll = (ListControl)tblAddlServices.FindControl("lstStatusPayroll");
                        if (lstControlPayroll != null)
                        {
                            if (User.IsInRole("Admin"))
                            {
                                lstControlPayroll.Enabled = true;
                            }
                            else
                            {
                                lstControlPayroll.Enabled = false;
                            }
                        }
                    }
                }
                #endregion

                OnlineAppStatusBL StatusInfo = new OnlineAppStatusBL(AppId);
                PartnerDS.OnlineAppStatusFieldsDataTable dt = StatusInfo.GetStatusFields();
                if (dt.Rows.Count > 0)
                {
                    //lblContact.Text = dt[0].FirstName.ToString().Trim() + " " + dt[0].LastName.ToString().Trim();
                    //lblAccountType.Text = dt[0]AcctTypeDesc.ToString().Trim();
                    //lstGatewayStatus.DataBind();

                    ListControl lstControlCheckService = (ListControl)tblAddlServices.FindControl("lstStatusCheckService");
                    if (lstControlCheckService != null)
                    {
                        if (lstControlCheckService.Items.FindByValue(dt[0].StatusCheckService.ToString()) == null)
                            lstControlCheckService.Items.Add(dt[0].StatusCheckService.ToString());
                        lstControlCheckService.SelectedValue = dt[0].StatusCheckService.ToString();
                    }

                    ListControl lstControlGift = (ListControl)tblAddlServices.FindControl("lstStatusGift");
                    if (lstControlGift != null)
                    {
                        if (lstControlGift.Items.FindByValue(dt[0].StatusGiftCard.ToString()) == null)
                            lstControlGift.Items.Add(dt[0].StatusGiftCard.ToString());
                        lstControlGift.SelectedValue = dt[0].StatusGiftCard.ToString();
                    }

                    ListControl lstControlLease = (ListControl)tblAddlServices.FindControl("lstStatusLease");
                    if (lstControlLease != null)
                    {
                        if (lstControlLease.Items.FindByValue(dt[0].StatusLease.ToString()) == null)
                            lstControlLease.Items.Add(dt[0].StatusLease.ToString());
                        /*
                        ListItem selectedListItem = lstControlLease.Items.FindByValue(dt[0].StatusLease.ToString());

                        if (selectedListItem != null)
                        {
                            selectedListItem.Selected = true;
                        };*/

                        lstControlLease.SelectedValue = dt[0].StatusLease.ToString();
                    }

                    ListControl lstControlMCA = (ListControl)tblAddlServices.FindControl("lstStatusMCA");
                    if (lstControlMCA != null)
                    {
                        if (lstControlMCA.Items.FindByValue(dt[0].StatusMerchantFunding.ToString()) == null)
                            lstControlMCA.Items.Add(dt[0].StatusMerchantFunding.ToString());
                        lstControlMCA.SelectedValue = dt[0].StatusMerchantFunding.ToString();
                    }

                    ListControl lstControlPayroll = (ListControl)tblAddlServices.FindControl("lstStatusPayroll");
                    if (lstControlPayroll != null)
                    {
                        if (lstControlPayroll.Items.FindByValue(dt[0].StatusPayroll.ToString()) == null)
                            lstControlPayroll.Items.Add(dt[0].StatusPayroll.ToString());
                        lstControlPayroll.SelectedValue = dt[0].StatusPayroll.ToString();
                    }

                    //add the already existing Merchant Status for the agent to this dropdown
                    /*if (lstMerchantStatus.Items.FindByValue(dt[0].Status.ToString()) == null)
                        lstMerchantStatus.Items.Add(dt[0].Status.ToString());
                    lstMerchantStatus.SelectedValue = dt[0].Status.ToString();*/

                    //add the already existing Gateway Status for the agent to this dropdown
                    /*if (lstGatewayStatus.Items.FindByValue(dt[0].StatusGW.ToString()) == null)
                        lstGatewayStatus.Items.Add(dt[0].StatusGW.ToString());
                    lstGatewayStatus.SelectedValue = dt[0].StatusGW.ToString();*/
                }

            }//end if count not 0
        }//end if count not 0
    }
    #endregion

    #region MODIFY PROFILE
    protected void lnkbtnModify_Click(object sender, EventArgs e)
    {
        //Server.Transfer("Modify.aspx?AppId=" + AppId);
        try
        {
            if (lstSalesRep.Items.Count == 0)
                PopulateGeneralInfo();
            pnlDisplayGeneralInfo.Visible = false;
            pnlModifyGeneralInfo.Visible = true;
        }//end try
        catch (Exception err)
        {
            CreateLog Log = new CreateLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "Modify Profile Load Error - " + err.Message);
            DisplayMessage("Error loading Modify Profile");
        }
    }

    protected void lnkDelDocuSignEnv_Click(object sender, EventArgs e) {
        lnkDelDocuSignEnv.Visible = false;
        pnlDelDocuEnvConfirm.Visible = true;
    }

    protected void btnDelDocuSignEnvYes_Click(object sender, EventArgs e)
    {
        try
        {
            if (User.IsInRole("Admin"))
            {

                bool retVal = false;
                //Delete app from partner portal database
                OnlineAppBL OnlineApp = new OnlineAppBL(AppId);
                PartnerLogBL LogData = new PartnerLogBL();
                int iRetVal = OnlineApp.DelDocuSignEnv();
                if (iRetVal > 0)
                {
                    DisplayMessage("DocuSign Envelope Deleted Successfully.");
                    retVal = LogData.InsertLogData(AppId, Convert.ToInt32(Session["AffiliateID"]), "DocuSign Envelope deleted");
                }
                else
                    DisplayMessage("DocuSign Envelope cannot be deleted.");

                pnlDelDocuEnvConfirm.Visible = false;
                lnkDelDocuSignEnv.Visible = true;

                
                
            }//end if user is in role            
        }//end try
        catch (Exception err)
        {
            CreateLog Log = new CreateLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "Delete Record - " + err.Message);
            DisplayMessage("Error deleting record");
        }

    }

    protected void btnDelDocuSignEnvNo_Click(object sender, EventArgs e)
    {
        pnlDelDocuEnvConfirm.Visible = false;
        lnkDelDocuSignEnv.Visible = true;
    }

    protected void lstLease_SelectedIndexChanged(object sender, EventArgs e)
    {
        ListControl lstControlLease = (ListControl)tblAddlServices.FindControl("lstStatusLease");
        if (lstControlLease != null)
        {
            StatusLease = lstControlLease.SelectedItem.Text.ToString();
        }

        //StatusLease = lstStatusLease.SelectedValue;
    }

    //This function populates fields
    public void PopulateGeneralInfo()
    {
        if (User.IsInRole("Admin") || User.IsInRole("Employee"))
        {
            //Populate rep list
            ListBL SalesRepList = new ListBL();
            DataSet ds = SalesRepList.GetSalesRepList();
            if (ds.Tables[0].Rows.Count > 0)
            {
                lstSalesRep.DataSource = ds.Tables[0];
                lstSalesRep.DataTextField = "RepName";
                lstSalesRep.DataValueField = "MasterNum";
                lstSalesRep.DataBind();
            }
            System.Web.UI.WebControls.ListItem lstItem = new System.Web.UI.WebControls.ListItem();
            lstItem.Text = "";
            lstItem.Value = "";
            lstSalesRep.Items.Add(lstItem);

            //Populate other referral list
            ACTDataBL OtherRefList = new ACTDataBL();
            ds = OtherRefList.GetOtherReferralList();
            if (ds.Tables[0].Rows.Count > 0)
            {
                lstOtherReferral.DataSource = ds;
                lstOtherReferral.DataTextField = "DBA";
                lstOtherReferral.DataValueField = "DBA";
                lstOtherReferral.DataBind();
            }
            lstItem = new System.Web.UI.WebControls.ListItem();
            lstItem.Text = "";
            lstItem.Value = "";
            lstOtherReferral.Items.Add(lstItem);

            //Get registration info from newapp table and populate fields
            OnlineAppProfile GeneralInfo = new OnlineAppProfile(AppId);
            DataSet dsNewApp = GeneralInfo.GetProfileData();
            if (dsNewApp.Tables[0].Rows.Count > 0)
            {
                int AffiliateID = Convert.ToInt32(Session["AffiliateID"]);
                AffiliatesBL Affiliates = new AffiliatesBL(AffiliateID);
                DataSet dsAffiliates = Affiliates.GetAffiliateList();
                if (dsAffiliates.Tables[0].Rows.Count > 0)
                {
                    DataRow dr;
                    for (int i = 0; i < dsAffiliates.Tables[0].Rows.Count; i++)
                    {
                        dr = dsAffiliates.Tables[0].Rows[i];
                        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem();
                        item.Text = dr["DBA"].ToString().Trim() + " - " + dr["CompanyName"].ToString().Trim() + " - (" + dr["AffiliateID"].ToString().Trim() + ")";
                        item.Value = dr["AffiliateID"].ToString().Trim();
                        lstReferredBy.Items.Add(item);
                    }
                    System.Web.UI.WebControls.ListItem ReferralItem = new System.Web.UI.WebControls.ListItem();
                    ReferralItem.Text = "OTHER";
                    ReferralItem.Value = "0";
                    lstReferredBy.Items.Add(ReferralItem);

                }//end if count not 0

                DataRow drNewApp = dsNewApp.Tables[0].Rows[0];
                txtLoginName.Text = drNewApp["LoginName"].ToString().Trim();
                txtTitle.Text = drNewApp["Title"].ToString().Trim();
                txtFirstName.Text = drNewApp["FirstName"].ToString().Trim();
                txtLastName.Text = drNewApp["LastName"].ToString().Trim();
                txtPhone.Text = drNewApp["Phone"].ToString().Trim();
                txtPhoneExt.Text = drNewApp["PhoneExt"].ToString().Trim();
                txtHomePhone.Text = drNewApp["HomePhone"].ToString().Trim();
                txtMobilePhone.Text = drNewApp["MobilePhone"].ToString().Trim();
         
                lstReferredBy.SelectedValue = lstReferredBy.Items.FindByValue(drNewApp["ReferralID"].ToString()).Value;           
                lstSalesRep.SelectedValue = lstSalesRep.Items.FindByValue(drNewApp["RepNum"].ToString().Trim()).Value;

                lstItem = lstOtherReferral.Items.FindByValue(drNewApp["OtherReferral"].ToString());

                //If Other Referral does not exist in the dropdown, add it and select it
                if (lstItem == null)
                    lstOtherReferral.Items.Add(drNewApp["OtherReferral"].ToString());
                lstOtherReferral.SelectedValue = lstOtherReferral.Items.FindByValue(drNewApp["OtherReferral"].ToString()).Value; 
                               
                txtEmail.Text = drNewApp["SignupEmail"].ToString().Trim();

            }//end if count not 0
        }//end if User is admin or employee
        if (User.IsInRole("Admin"))
        {
            lstSalesRep.Enabled = true;
            lstReferredBy.Enabled = true;
            lstOtherReferral.Enabled = true;
        }
        else
        {
            lstSalesRep.Enabled = false;
            lstReferredBy.Enabled = false;
            lstOtherReferral.Enabled = false;
        }
    }//end function populate

    //This function handles submit button click event
    protected void btnContinue_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {          
                //Validate data
                bool retValidate = ValidateData();
                if (retValidate)
                {
                    bool bLogin = false;
                    //Check if LoginName already exists and display message if exists
                    if (User.IsInRole("Admin") && txtLoginName.Enabled)
                    {
                        //First check if the current login name in onlineappaccess is blank
                        OnlineAppBL App = new OnlineAppBL(AppId);
                        string LoginName = App.ReturnLoginName();

                        if (LoginName == "")
                            App.InsertUpdateLoginName(txtLoginName.Text);
                        else if (LoginName != txtLoginName.Text.Trim())
                            App.InsertUpdateLoginName(txtLoginName.Text);
                        else if (LoginName == txtLoginName.Text.Trim())
                        {
                            bLogin = true;
                            DisplayMessage("The Login Name you chose already exists. Please try a different Login Name. Information was not updated.");
                        }
                    }//end if user is admin

                    if (!bLogin)
                    {
                        OnlineAppProfile GeneralInfo = new OnlineAppProfile(AppId);
                        DataSet dsNewApp = GeneralInfo.GetProfileData();
                        DataRow drNewApp = dsNewApp.Tables[0].Rows[0];
                        AcctType = Convert.ToInt32(drNewApp["acctType"].ToString().Trim());

                        //Update profile information in OnlineappProfile
                        OnlineAppProfile Profile = new OnlineAppProfile(AppId);
                        bool retVal = Profile.IUProfile(
                            txtFirstName.Text.Trim(),
                            txtLastName.Text.Trim(),
                            txtEmail.Text.Trim(),
                            txtTitle.Text.Trim(),
                            txtPhone.Text.Trim(),
                            txtPhoneExt.Text.Trim(),
                            txtHomePhone.Text.Trim(),
                            txtMobilePhone.Text.Trim(),
                            AcctType);

                        //Update NewAppInformation
                        OnlineAppBL NewApp = new OnlineAppBL(AppId);
                        NewApp.UpdateReferral(Convert.ToInt32(lstReferredBy.SelectedValue), lstOtherReferral.SelectedItem.Text);

                        //Update RepNum 
                        NewApp.UpdateNewAppInfo(lstSalesRep.SelectedValue.ToString());

                        //Add action to Log table
                        PartnerLogBL LogData = new PartnerLogBL();
                        retVal = LogData.InsertLogData(AppId, Convert.ToInt32(Session["AffiliateID"]), "Application Profile Updated");

                        pnlModifyGeneralInfo.Visible = false;
                        pnlDisplayGeneralInfo.Visible = true;
                        PopulateFields();
                        DisplayMessage("Information Updated.");

                    }//end if login already exists
                }//end if validated
            }//end if page valid
        }//end try
        catch (Exception err)
        {
            CreateLog Log = new CreateLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "Modify Profile Submit Click - " + err.Message);
            DisplayMessage("Error updating general information");
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
                if ((txtBox.Text.Contains("--")) || (txtBox.Text.Contains("#")) || (txtBox.Text.Contains(";")) || (txtBox.Text.Contains("'")))
                {
                    DisplayMessage("You cannot use hyphens, apostrophe, # or semi-colons in any of the following fields.");
                    return false;
                }
            }
        }//end for        
        return true;
    }//end function validate data

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        pnlModifyGeneralInfo.Visible = false;
        pnlDisplayGeneralInfo.Visible = true;
    }

    protected void lnkbtnUpdateLoginName_Click(object sender, EventArgs e)
    {
        if (lnkbtnUpdateLoginName.Text == "Edit")
        {
            txtLoginName.Enabled = true;
            lnkbtnUpdateLoginName.Text = "Cancel";
        }
        else if (lnkbtnUpdateLoginName.Text == "Cancel")
        {
            txtLoginName.Enabled = false;
            lnkbtnUpdateLoginName.Text = "Edit";
        }
    }

    #endregion

    #region STATUS
    //This function populates fields
    public void PopulateStatus()
    {
        if (Locked() && (User.IsInRole("Agent") || User.IsInRole("T1Agent")))
            btnSubmit.Enabled = false;

        //Load limited status list for agents   
        if (!User.IsInRole("Admin") && !User.IsInRole("Employee"))
        {
            OnlineAppStatusBL Status = new OnlineAppStatusBL(AppId);
            DataSet dsStatus = Status.GetStatusList("Agent","Merchant");
            if (dsStatus.Tables[0].Rows.Count > 0)
            {
                lstMerchantStatus.DataSource = dsStatus;
                lstMerchantStatus.DataTextField = "Status";
                lstMerchantStatus.DataValueField = "Status";
                lstMerchantStatus.DataBind();
            }

            DataSet dsStatusGW = Status.GetStatusList("Agent", "Gateway");
            if (dsStatusGW.Tables[0].Rows.Count > 0)
            {
                lstGatewayStatus.DataSource = dsStatusGW;
                lstGatewayStatus.DataTextField = "Status";
                lstGatewayStatus.DataValueField = "Status";
            }
            
            lstMerchantStatus.Enabled = false;
            lstGatewayStatus.Enabled = false;
            lstPlatform.Enabled = false;
            btnSubmit.Enabled = false;
            btnSubmit.Visible = false;
        }
        else
        {                
            //Load full status list for admins and employees
            OnlineAppStatusBL Status = new OnlineAppStatusBL(AppId);
            DataSet dsStatus = Status.GetStatusList("Admin", "Merchant");
            if (dsStatus.Tables[0].Rows.Count > 0)
            {
                lstMerchantStatus.DataSource = dsStatus;
                lstMerchantStatus.DataTextField = "Status";
                lstMerchantStatus.DataValueField = "Status";
                lstMerchantStatus.DataBind();
            }

            DataSet dsStatusGW = Status.GetStatusList("Admin", "Gateway");
            if (dsStatusGW.Tables[0].Rows.Count > 0)
            {
                lstGatewayStatus.DataSource = dsStatusGW;
                lstGatewayStatus.DataTextField = "Status";
                lstGatewayStatus.DataValueField = "Status";
                lstGatewayStatus.DataBind();
            }            
        }

        //Get Status information
        OnlineAppStatusBL StatusInfo = new OnlineAppStatusBL(AppId);
        PartnerDS.OnlineAppStatusFieldsDataTable dt = StatusInfo.GetStatusFields();
        if (dt.Rows.Count > 0)
        {
            //lblContact.Text = dt[0].FirstName.ToString().Trim() + " " + dt[0].LastName.ToString().Trim();
            //lblAccountType.Text = dt[0]AcctTypeDesc.ToString().Trim();
            lstGatewayStatus.DataBind();

            //add the already existing Merchant Status for the agent to this dropdown
            if (lstMerchantStatus.Items.FindByValue(dt[0].Status.ToString()) == null)
                lstMerchantStatus.Items.Add(dt[0].Status.ToString());
            lstMerchantStatus.SelectedValue = dt[0].Status.ToString();

            //add the already existing Gateway Status for the agent to this dropdown
            if (lstGatewayStatus.Items.FindByValue(dt[0].StatusGW.ToString()) == null)
                lstGatewayStatus.Items.Add(dt[0].StatusGW.ToString());
            lstGatewayStatus.SelectedValue = dt[0].StatusGW.ToString();  

            AcctType = Convert.ToInt32(dt[0].AcctType);


            if (!dt[0].DiscoverNum.ToString().Trim().Contains("Opted") && !dt[0].DiscoverNum.ToString().Trim().Contains("Submitted"))
            {
                //lblDiscoverText.Text = MaskNumbers(Server.HtmlEncode(dt[0].DiscoverNum.ToString().Trim()));
                lblDiscoverText.Text = Server.HtmlEncode(dt[0].DiscoverNum.ToString().Trim());
            }
            else
                lblDiscoverText.Text = Server.HtmlEncode(dt[0].DiscoverNum.ToString().Trim());

            if (!dt[0].AmexNum.ToString().Trim().Contains("Opted") && !dt[0].AmexNum.ToString().Trim().Contains("Submitted"))
            {
                //lblAmexText.Text = MaskNumbers(Server.HtmlEncode(dt[0].AmexNum.ToString().Trim()));
                lblAmexText.Text = Server.HtmlEncode(dt[0].AmexNum.ToString().Trim());
            }
            else
                lblAmexText.Text = Server.HtmlEncode(dt[0].AmexNum.ToString().Trim());

            if (!dt[0].JCBNum.ToString().Trim().Contains("Opted") && !dt[0].JCBNum.ToString().Trim().Contains("Submitted"))
            {
                //lblJCBText.Text = MaskNumbers(Server.HtmlEncode(dt[0].JCBNum.ToString().Trim()));
                lblJCBText.Text = Server.HtmlEncode(dt[0].JCBNum.ToString().Trim());
            }
            else
                lblJCBText.Text = Server.HtmlEncode(dt[0].JCBNum.ToString().Trim());
            lblLoginUserIDText.Text = dt[0].GatewayUserID.ToString().Trim();
            //lblGatewayText.Text = dt[0].Gateway.ToString().Trim();
            lblProcessorText.Text = dt[0].Processor.ToString().Trim();
        }//end if count not 0

        //Check if Account has reprogram info
        OnlineAppStatusBL Check = new OnlineAppStatusBL(AppId);
        bool retVal = Check.CheckReprogram();
        if (retVal)
        {
            rdbYes.Checked = true;
        }
        else
        {
            rdbNo.Checked = true;
        }

        //Show panels based on Account type.
        if (AcctType == 1)
        {
            pnlMerchantInfo.Visible = true;
            pnlPlatform.Visible = true;
            pnlGatewayInfo.Visible = false;
            pnlReprogramQuestion.Visible = false;
        }
        else if (AcctType == 2)
        {
            pnlMerchantInfo.Visible = false;
            pnlPlatform.Visible = false;
            pnlGatewayInfo.Visible = true;            
            pnlReprogramQuestion.Visible = true;

            //Check if Account has reprogram info
            if (retVal)
            {
                pnlReprogram.Visible = true;
            }
            else
            {
                pnlReprogram.Visible = false;
            }
        }
        else if (AcctType == 4)
        {
            pnlMerchantInfo.Visible = true;
            pnlPlatform.Visible = true;
            pnlGatewayInfo.Visible = true;
            pnlReprogramQuestion.Visible = false;
        }

        //Get Platform info
        OnlineAppStatusBL PlatformInfo = new OnlineAppStatusBL(AppId);
        DataSet dsPlatform = PlatformInfo.GetPlatformInfo();
        PopulatePlatformList();
        if (dsPlatform.Tables[0].Rows.Count > 0)
        {            
            DataRow dr = dsPlatform.Tables[0].Rows[0];

                    ListItem lstItem = new System.Web.UI.WebControls.ListItem();

                    /*if (dr["Platform"].ToString() == "Paymentech")
                    {
                        lstItem.Text = "Paymentech";
                        lstItem.Value = "Paymentech";
                        lstPlatform.Items.Add(lstItem);
                    }*/

            //lblVisaMasterNumberText.Text = MaskNumbers(Server.HtmlEncode(dr["MerchantNum"].ToString().Trim()));
            lblVisaMasterNumberText.Text = Server.HtmlEncode(dr["MerchantNum"].ToString().Trim());
            lstPlatform.SelectedValue = dr["Platform"].ToString();

            //lblPlatformACT.Text = dr["Platform"].ToString().Trim();
            //lblMerchantIDText.Text = MaskNumbers(Server.HtmlEncode(dr["MerchantID"].ToString().Trim()));
            //lblLoginIDText.Text = MaskNumbers(Server.HtmlEncode(dr["LoginID"].ToString().Trim()));
            //lblTerminalIDText.Text = MaskNumbers(Server.HtmlEncode(dr["TerminalID"].ToString().Trim()));
            //lblBINNumberText.Text = MaskNumbers(Server.HtmlEncode(dr["BankIDNum"].ToString().Trim()));
            //lblAgentBankNumberText.Text = MaskNumbers(Server.HtmlEncode(dr["AgentBankIDNum"].ToString().Trim()));
            //lblAgentChainNumberText.Text = MaskNumbers(Server.HtmlEncode(dr["AgentChainNum"].ToString().Trim()));
            //lblMCCCodeText.Text = MaskNumbers(Server.HtmlEncode(dr["MCCCategoryCode"].ToString().Trim()));
            //lblStoreNumberText.Text = MaskNumbers(Server.HtmlEncode(dr["StoreNum"].ToString().Trim()));

            lblMerchantIDText.Text = Server.HtmlEncode(dr["MerchantID"].ToString().Trim());
            lblLoginIDText.Text = Server.HtmlEncode(dr["LoginID"].ToString().Trim());
            lblTerminalIDText.Text = Server.HtmlEncode(dr["TerminalID"].ToString().Trim());

            lblBINNumberText.Text = Server.HtmlEncode(dr["BankIDNum"].ToString().Trim());
            lblAgentBankNumberText.Text = Server.HtmlEncode(dr["AgentBankIDNum"].ToString().Trim());
            lblAgentChainNumberText.Text = Server.HtmlEncode(dr["AgentChainNum"].ToString().Trim());
            lblMCCCodeText.Text = Server.HtmlEncode(dr["MCCCategoryCode"].ToString().Trim());
            lblStoreNumberText.Text = Server.HtmlEncode(dr["StoreNum"].ToString().Trim());
        }//end if count not 0
        else
            lstPlatform.SelectedValue = "";
            //lblPlatformACT.Text = "None";

        //Get reprogram info
        OnlineAppStatusBL ReprogramInfo = new OnlineAppStatusBL(AppId);
        DataSet dsReprogram = ReprogramInfo.GetReprogramInfo();
        if (dsReprogram.Tables[0].Rows.Count > 0)
        {
            DataRow dr = dsReprogram.Tables[0].Rows[0];
            if (lblVisaMasterNumberText.Text == "")
                lblVisaMasterNumberText.Text = MaskNumbers(Server.HtmlEncode(dr["MerchantNum"].ToString().Trim()));
            lblRPlatformACT.Text = dr["Platform"].ToString().Trim();
            lblRMerchantIDText.Text = MaskNumbers(Server.HtmlEncode(dr["MerchantID"].ToString().Trim()));
            lblRLoginIDText.Text = MaskNumbers(Server.HtmlEncode(dr["LoginID"].ToString().Trim()));
            lblRTerminalIDText.Text = MaskNumbers(Server.HtmlEncode(dr["TerminalID"].ToString().Trim()));
            lblRBINNumberText.Text = MaskNumbers(Server.HtmlEncode(dr["BankIDNum"].ToString().Trim()));
            lblRAgentBankNumberText.Text = MaskNumbers(Server.HtmlEncode(dr["AgentBankIDNum"].ToString().Trim()));
            lblRAgentChainNumberText.Text = MaskNumbers(Server.HtmlEncode(dr["AgentChainNum"].ToString().Trim()));
            lblRMCCCodeText.Text = MaskNumbers(Server.HtmlEncode(dr["MCCCategoryCode"].ToString().Trim()));
            lblRStoreNumberText.Text = MaskNumbers(Server.HtmlEncode(dr["StoreNum"].ToString().Trim()));

 
            //lstPlatform.SelectedValue = dr["Platform"].ToString();
        }//end if count not 0
        else if (Convert.ToString(lblProcessorText.Text.Trim()).ToLower().Contains("sage"))
        {


            lstPlatform.SelectedValue = "TSYS/Vital";



        }
        //else if (Convert.ToString(lblProcessorText.Text.Trim()).ToLower().Contains("ipayment"))
        {
          //  lstPlatform.SelectedValue = "First Data Omaha";
        }  
        //else
           // lblRPlatformACT.Text = "None";


    }//end function Populate

    protected void rdbYes_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbYes.Checked)
            pnlReprogram.Visible = true;
        else if (rdbNo.Checked)
            pnlReprogram.Visible = false;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string PrevMerchantStatus = string.Empty;
            string PrevGatewayStatus = string.Empty;
            string PrevPlatform = string.Empty;
            string StatusOnlineDebit = string.Empty;
            string StatusGiftCard = string.Empty;
            string StatusCheckService = string.Empty;
            string StatusEBT = string.Empty;
            string StatusMerchantFunding = string.Empty;
            //string StatusLease = string.Empty;
            string StatusPayroll = string.Empty;
            OnlineAppStatusBL StatusInfo = new OnlineAppStatusBL(AppId);
            PartnerDS.OnlineAppStatusFieldsDataTable dt = StatusInfo.GetStatusFields();
            OnlineAppDL OnlineApp = new OnlineAppDL();
            if (dt.Rows.Count > 0)
            {
                PrevMerchantStatus = dt[0].Status.ToString().Trim();
                PrevGatewayStatus = dt[0].StatusGW.ToString().Trim();
            }//end if count not 0

            DataSet dsPlatform = StatusInfo.GetPlatformInfo();
            if (dsPlatform.Tables[0].Rows.Count > 0)
            {
                DataRow dr = dsPlatform.Tables[0].Rows[0];
                PrevPlatform = dr["Platform"].ToString();
            }

            int AffiliateID = Convert.ToInt32(Session["AffiliateID"]);
            string MerchantStatus = lstMerchantStatus.SelectedItem.Text;
            string GatewayStatus = lstGatewayStatus.SelectedItem.Text;
            string Platform = lstPlatform.SelectedItem.Text;
            StatusOnlineDebit = string.Empty;
            StatusEBT = string.Empty;


            ListControl lstControlCheck = (ListControl)tblAddlServices.FindControl("lstStatusCheckService");
            if (lstControlCheck != null)
            {
                StatusCheckService = Convert.ToString(lstControlCheck.SelectedItem.Text.Trim());
            }

            ListControl lstControlGift = (ListControl)tblAddlServices.FindControl("lstStatusGift");
            if (lstControlGift != null)
            {
                StatusGiftCard = Convert.ToString(lstControlGift.SelectedItem.Text.Trim());
            }


            /*ListControl lstControlLease = (ListControl)tblAddlServices.FindControl("lstStatusLease");
            if (lstControlLease != null)
            {
                StatusLease = lstControlLease.SelectedItem.Text;
            }*/


            ListControl lstControlMCA = (ListControl)tblAddlServices.FindControl("lstStatusMCA");
            if (lstControlMCA != null)
            {
                StatusMerchantFunding = lstControlMCA.SelectedItem.Text;
            }


            ListControl lstControlPayroll = (ListControl)tblAddlServices.FindControl("lstStatusPayroll");
            if (lstControlPayroll != null)
            {
                StatusPayroll = lstControlPayroll.SelectedItem.Text;
            }


            if ((AcctType == 2) || (AcctType == 3))
            {
                MerchantStatus = "";
                Platform = "";
            }
            if ((AcctType == 1) || (AcctType == 3))
                GatewayStatus = "";
            //Update status information
            string strRetVal = StatusInfo.UpdateStatusInformation(MerchantStatus, GatewayStatus, Platform, AffiliateID);

            //Update additional services status
            int intStatusRetVal = OnlineApp.UpdateNewAppExportedAddlServicesStatus(AppId, StatusGiftCard,
            StatusCheckService, StatusMerchantFunding, StatusLease, StatusPayroll);
            DisplayMessage(strRetVal);

            //Add action to Log table
            PartnerLogBL LogData = new PartnerLogBL();
            if (PrevMerchantStatus != MerchantStatus)
            {
                //Add History
                string NoteText = "Merchant Status Updated to " + MerchantStatus;
                LogData.InsertLogData(AppId, Convert.ToInt32(Session["AffiliateID"]), NoteText);

                //Add note that status has been changed to ACT
                /*AffiliatesBL Aff = new AffiliatesBL(AffiliateID);
                string ActUserID = Aff.ReturnACTUserID();
                NoteText = "Merchant Status changed to " + MerchantStatus;
                StatusInfo.InsertNote(ActUserID, NoteText);*/
            }
            if (PrevGatewayStatus != GatewayStatus)
            {
                //Add History
                string NoteText = "Gateway Status Updated to " + GatewayStatus;
                LogData.InsertLogData(AppId, Convert.ToInt32(Session["AffiliateID"]), NoteText);

                //Add note that status has been changed to ACT
                /*AffiliatesBL Aff = new AffiliatesBL(AffiliateID);
                string ActUserID = Aff.ReturnACTUserID();
                NoteText = "Gateway Status changed to " + GatewayStatus;
                StatusInfo.InsertNote(ActUserID, NoteText);*/
            }
            if (PrevPlatform != Platform)
            {
                //Add History
                string NoteText = "Platform Updated to " + Platform;
                LogData.InsertLogData(AppId, Convert.ToInt32(Session["AffiliateID"]), NoteText);
            }
            PopulateStatus();
            PopulateSalesOpps();
            PopulateRates();
            PopulateAddlServices();
            PopulateHistory();
            //LoadNotes();
            //Check if application is locked
            if (Locked())
            {
                //DisplayMessage("The application is locked because the Merchant status or Gateway Status prevents it from being edited.");
                if (User.IsInRole("Admin"))
                {
                    lnkAddSalesOpps.Visible = true;
                    btnSubmit.Enabled = true; //Status Submit button
                    //btnSubmitCardPCT.Enabled = true;
                    imgAddToACT.Enabled = true;
                    imgUpdateInACT.Enabled = true;
                }
                else if (User.IsInRole("Employee"))
                {
                    lnkAddSalesOpps.Visible = true;
                    //btnSubmit.Enabled = false; //Status Submit button
                    //btnSubmitCardPCT.Enabled = false;
                    lstMerchantStatus.Enabled = false;
                    lstPlatform.Enabled = false;
                    imgAddToACT.Enabled = false;
                    imgAddToACT.ImageUrl = "~/Images/AddToACT_gray.gif";
                    imgAddToACT.ToolTip = "Cannot add this application because it is locked.";
                    imgUpdateInACT.Enabled = true;
                }
                else
                {
                    lnkAddSalesOpps.Visible = true;
                    //btnSubmit.Enabled = false; //Status Submit button
                    //btnSubmitCardPCT.Enabled = false;
                    lstMerchantStatus.Enabled = false;
                    lstPlatform.Enabled = false;
                    imgAddToACT.Enabled = false;
                    imgAddToACT.ImageUrl = "~/Images/AddToACT_gray.gif";
                    imgAddToACT.ToolTip = "Cannot add this application because it is locked.";
                    imgUpdateInACT.Enabled = false;
                    imgUpdateInACT.ImageUrl = "~/Images/UpdateInACT_gray.gif";
                    imgUpdateInACT.ToolTip = "Cannot update this application because it is locked.";
                }
            }

            if (GatewayLocked())
            {
                //DisplayMessage("The application is locked because the Merchant status or Gateway Status prevents it from being edited.");
                if (User.IsInRole("Admin"))
                {
                    lnkAddSalesOpps.Visible = true;
                    btnSubmit.Enabled = true; //Status Submit button
                    //btnSubmitCardPCT.Enabled = true;
                    imgAddToACT.Enabled = true;
                    imgUpdateInACT.Enabled = true;
                }
                else if (User.IsInRole("Employee"))
                {
                    lnkAddSalesOpps.Visible = true;
                    //btnSubmit.Enabled = false; //Status Submit button
                    //btnSubmitCardPCT.Enabled = false;
                    lstMerchantStatus.Enabled = false;
                    lstPlatform.Enabled = false;
                    imgAddToACT.Enabled = false;
                    imgAddToACT.ImageUrl = "~/Images/AddToACT_gray.gif";
                    imgAddToACT.ToolTip = "Cannot add this application because it is locked.";
                    imgUpdateInACT.Enabled = true;
                }
                else
                {
                    lnkAddSalesOpps.Visible = true;
                    //btnSubmit.Enabled = false; //Status Submit button
                    //btnSubmitCardPCT.Enabled = false;
                    lstGatewayStatus.Enabled = false;
                    lstPlatform.Enabled = false;
                    imgAddToACT.Enabled = false;
                    imgAddToACT.ImageUrl = "~/Images/AddToACT_gray.gif";
                    imgAddToACT.ToolTip = "Cannot add this application because it is locked.";
                    imgUpdateInACT.Enabled = false;
                    imgUpdateInACT.ImageUrl = "~/Images/UpdateInACT_gray.gif";
                    imgUpdateInACT.ToolTip = "Cannot update this application because it is locked.";
                }
            }

            CommonFunctions GeneralInfo = new CommonFunctions(AppId);
            GeneralInfo.UpdateLastModified();
        }//end try
        catch (Exception err)
        {
            CreateLog Log = new CreateLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), err.Message);
            DisplayMessage("Error modifying status information.");
        }
    }//end submit button click

    public void PopulatePlatformList()
    {
        //Get Platform list
        OnlineAppStatusBL Platforms = new OnlineAppStatusBL();
        DataSet ds = Platforms.GetPlatforms(lblProcessorText.Text);
        if (ds.Tables[0].Rows.Count > 0)
        {
            lstPlatform.DataSource = ds;
            lstPlatform.DataTextField = "Platform";
            lstPlatform.DataValueField = "Platform";
            lstPlatform.DataBind();
        }


        /*ListItem lstItem = new System.Web.UI.WebControls.ListItem();
        lstItem.Text = "";
        lstItem.Value = "";
        lstPlatform.Items.Add(lstItem);*/
        if (Convert.ToString(lblProcessorText.Text.Trim()).ToLower().Contains("sage"))
        {
            lstPlatform.SelectedValue = "TSYS/Vital";
        }

        if (Convert.ToString(lblProcessorText.Text.Trim()).ToLower().Contains("ipayment"))
        {
            lstPlatform.SelectedValue = "First Data Omaha";
        }

        if (Convert.ToString(lblProcessorText.Text.Trim()).ToLower().Contains("cardconnect"))
        {
            lstPlatform.SelectedValue = "First Data North";
        }
        //lstPlatform.SelectedIndex = lstPlatform.Items.IndexOf(lstPlatform.Items.FindByText("None"));
    }
    #endregion

    //This function masks first few digits with x
    protected string MaskNumbers(string strNumber)
    {
        string Number = strNumber;
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
        
    //This function displays error message on a label
    protected void DisplayMessage(string errText)
    {
        lblError.Visible = true;
        lblError.Text = errText;
    }//end function set error message
}