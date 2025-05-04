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

public partial class Edit : Loader
{
    void Page_Init(object sender, EventArgs e)
    {
        ViewStateUserKey = Session.SessionID;
        if (!User.IsInRole("Admin") && !User.IsInRole("Employee"))
            Tabs.Controls.Remove(TabHistory);
    }

    private static int AppId = 0;
    private static int AffiliateID = 0;
    private static int AcctType = 0;
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
                if (User.IsInRole("Employee") || User.IsInRole("Admin"))
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
                LoadNotes();
                PopulateHistory();
                PopulateRates();
                PopulateSalesOpps();
                PopulateStatus();
                //PopulateCardPCT();
                #endregion

                //Check if application is locked
                if (Locked())
                {
                    DisplayMessage("The application is locked because the Merchant status or Gateway Status prevents it from being edited.");
                    if (User.IsInRole("Admin"))
                    {
                        lnkAddSalesOpps.Visible = true;
                        btnSubmit.Enabled = true; //Status Submit button
                        //btnSubmitCardPCT.Enabled = true;
                        imgAddToACT.Enabled = true;
                        imgUpdateInACT.Enabled = true;
                    }
                    else
                    {
                        lnkAddSalesOpps.Visible = false;
                        btnSubmit.Enabled = false; //Status Submit button
                        //btnSubmitCardPCT.Enabled = false;
                        imgAddToACT.Enabled = false;
                        imgAddToACT.ImageUrl = "~/Images/AddToACT_gray.gif";
                        imgAddToACT.ToolTip = "Cannot add this application because it is locked.";
                        imgUpdateInACT.Enabled = false;
                        imgUpdateInACT.ImageUrl = "~/Images/UpdateInACT_gray.gif";
                        imgUpdateInACT.ToolTip = "Cannot update this application because it is locked.";
                    }
                }

                OnlineAppProfile AppInfo = new OnlineAppProfile(AppId);
                DataSet dsNewApp = AppInfo.GetProfileData();
                DataTable dtNewApp = dsNewApp.Tables[0];
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
        }//end if post back
    }//end if page load

    #region POPULATE
    //This function populates fields
    public void PopulateFields()
    {
        try
        {
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

            if (User.IsInRole("Agent") || User.IsInRole("T1Agent"))
            {
                lnkbtnModify.Visible = false;
                pnlLoginAttempts.Visible = false;
            }

            string strProcessor = string.Empty;
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
            }//end if count not 0

            //Check if application is locked for too many login attempts
            if (User.IsInRole("Admin") || (User.IsInRole("Employee")))
            {
                if (LoginAttempts == 5)
                    pnlLoginAttempts.Visible = true;
                else
                    pnlLoginAttempts.Visible = false;
            }

            if (strProcessor.ToLower().Contains("sage"))
                lblProcessorPDF.Text = "Sage"; 
            else if (strProcessor.ToLower() == "ims")
                lblProcessorPDF.Text = "IMS";
            else if (strProcessor.ToLower().Contains("ipayment"))
                lblProcessorPDF.Text = "iPayment";
            else if (strProcessor.ToLower().Contains("merrick"))
                lblProcessorPDF.Text = "Merrick";
            else if (strProcessor.ToLower().Contains("canada"))
                lblProcessorPDF.Text = "Canada";
            else if ((strProcessor.ToLower().Contains("international")) || (strProcessor.ToLower().Contains("cal")))
                lblProcessorPDF.Text = "Optimal Cal";
            else if (strProcessor.ToLower().Contains("chase"))
                lblProcessorPDF.Text = "Chase";
            else if (strProcessor.ToLower().Contains("kitts"))
                lblProcessorPDF.Text = "St. Kitts";
            else
                pnlPDF.Visible = false;
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
                        DisplayMessage(strRetVal);

                        //Update last modified date in OnlineAppProcessing table
                        OnlineAppProcessingBL Processing = new OnlineAppProcessingBL(AppId);
                        bool retVal = Processing.UpdateLastSyncDate();

                        //Create log entry for this action
                        PartnerLogBL LogData = new PartnerLogBL();
                        retVal = LogData.InsertLogData(AppId, Convert.ToInt32(Session["AffiliateID"]), "Application Added to ACT!");
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
            ExportActBL ExportedAct = new ExportActBL();
            string strRetVal = ExportedAct.AddInfoToACT(AppId);

            //Update last modified date in OnlineAppProcessing table
            OnlineAppProcessingBL UpdateDate = new OnlineAppProcessingBL();
            bool retVal = UpdateDate.UpdateLastSyncDate();

            //Create log entry for this action
            PartnerLogBL LogData = new PartnerLogBL();
            retVal = LogData.InsertLogData(AppId, Convert.ToInt32(Session["AffiliateID"]), "Application Added to ACT!");

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

    //This function updates the record in ACT
    private bool UpdateInACT(int AppId)
    {
        try
        {
            if (User.IsInRole("Admin") || User.IsInRole("Employee"))
            {
                if (AppId != 0)
                {
                    //First check if the appid exists in act (as a Primary Record, denoted by TYPENUM = 0)
                    ACTDataBL ACT = new ACTDataBL();
                    bool retVal = ACT.CheckAppIDExists(AppId);
                    if (!retVal)
                        DisplayMessage("This application has not been added to ACT!. Please add it to ACT first.");
                    else
                    {
                        ExportActBL ExportACT = new ExportActBL();
                        //Update information in ACT
                        string strRetVal = ExportACT.UpdateAct(AppId);
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
                                PartnerLogBL LogData = new PartnerLogBL();
                                retVal = LogData.InsertLogData(AppId, Convert.ToInt32(Session["AffiliateID"]), "Application Updated in ACT!");
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
    #region CREATE IMS XML
    /**
    protected void btnCreateIMSApp_Click(object sender, EventArgs e)
    {
        if (User.IsInRole("Admin") || User.IsInRole("Employee"))
        {
            IMSXML IMS = new IMSXML();
            DataSet ds = IMS.GetIMSData(AppId);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow drIMS = ds.Tables[0].Rows[0];
                try
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load("C:\\Documents and Settings\\Harshpatwardhan\\My Documents\\Visual Studio 2005\\WebSites\\Partner\\IMS.xml");

                    #region General Information
                    XmlNodeList nodelist = doc.GetElementsByTagName("EmailAddress");
                    XmlNode node = nodelist.Item(0);
                    node.InnerText = drIMS["EmailAddress"].ToString().Trim();

                    nodelist = doc.GetElementsByTagName("AgentReferralID");
                    node = nodelist.Item(0);
                    node.InnerText = drIMS["AgentReferralID"].ToString().Trim();

                    nodelist = doc.GetElementsByTagName("ExternalAgentUserID");
                    node = nodelist.Item(0);
                    node.InnerText = drIMS["ExternalAgentUserID"].ToString().Trim();

                    nodelist = doc.GetElementsByTagName("LegalName");
                    node = nodelist.Item(0);
                    node.InnerText = drIMS["LegalName"].ToString().Trim();

                    nodelist = doc.GetElementsByTagName("DBA");
                    node = nodelist.Item(0);
                    node.InnerText = drIMS["DBA"].ToString().Trim();
                    #endregion

                    #region Location Address
                    //Location Address
                    nodelist = doc.GetElementsByTagName("LocationAddress");
                    node = nodelist.Item(0);
                    XmlNode childNode = node.ChildNodes.Item(0);
                    childNode.InnerText = drIMS["Address1"].ToString().Trim();
                    childNode = node.ChildNodes.Item(1);
                    childNode.InnerText = drIMS["Address2"].ToString().Trim();
                    childNode = node.ChildNodes.Item(2);
                    childNode.InnerText = drIMS["City"].ToString().Trim();
                    childNode = node.ChildNodes.Item(3);
                    childNode.InnerText = drIMS["State"].ToString().Trim();
                    childNode = node.ChildNodes.Item(4);
                    childNode.InnerText = drIMS["PostalCode"].ToString().Trim();
                    #endregion

                    #region Mail Address
                    //Mail Address
                    nodelist = doc.GetElementsByTagName("MailAddress");
                    node = nodelist.Item(0);
                    childNode = node.ChildNodes.Item(0);
                    childNode.InnerText = drIMS["BillingAddress1"].ToString().Trim();
                    childNode = node.ChildNodes.Item(1);
                    childNode.InnerText = drIMS["BillingAddress2"].ToString().Trim();
                    childNode = node.ChildNodes.Item(2);
                    childNode.InnerText = drIMS["BillingCity"].ToString().Trim();
                    childNode = node.ChildNodes.Item(3);
                    childNode.InnerText = drIMS["BillingState"].ToString().Trim();
                    childNode = node.ChildNodes.Item(4);
                    childNode.InnerText = drIMS["BillingPostalCode"].ToString().Trim();
                    #endregion

                    #region General Information
                    nodelist = doc.GetElementsByTagName("PhoneNumber");
                    node = nodelist.Item(0);
                    node.InnerText = drIMS["PhoneNumber"].ToString().Trim();

                    nodelist = doc.GetElementsByTagName("FaxNumber");
                    node = nodelist.Item(0);
                    node.InnerText = drIMS["FaxNumber"].ToString().Trim();

                    nodelist = doc.GetElementsByTagName("URL");
                    node = nodelist.Item(0);
                    node.InnerText = drIMS["URL"].ToString().Trim();

                    nodelist = doc.GetElementsByTagName("FederalTaxID");
                    node = nodelist.Item(0);
                    node.InnerText = drIMS["FederalTaxID"].ToString().Trim();

                    nodelist = doc.GetElementsByTagName("Type");
                    node = nodelist.Item(0);
                    node.InnerText = drIMS["Type"].ToString().Trim();

                    nodelist = doc.GetElementsByTagName("EstablishmentDate");
                    node = nodelist.Item(0);
                    node.InnerText = drIMS["EstablishmentDate"].ToString().Trim();

                    nodelist = doc.GetElementsByTagName("ProductLine");
                    node = nodelist.Item(0);
                    node.InnerText = drIMS["ProductLine"].ToString().Trim();

                    nodelist = doc.GetElementsByTagName("Retail");
                    node = nodelist.Item(0);
                    node.InnerText = drIMS["Retail"].ToString();

                    nodelist = doc.GetElementsByTagName("Restaurant");
                    node = nodelist.Item(0);
                    node.InnerText = drIMS["Restaurant"].ToString();

                    nodelist = doc.GetElementsByTagName("Service");
                    node = nodelist.Item(0);
                    node.InnerText = drIMS["Service"].ToString();

                    nodelist = doc.GetElementsByTagName("MailPhoneOrder");
                    node = nodelist.Item(0);
                    node.InnerText = drIMS["MailPhoneOrder"].ToString();

                    nodelist = doc.GetElementsByTagName("Internet");
                    node = nodelist.Item(0);
                    node.InnerText = drIMS["Internet"].ToString();

                    nodelist = doc.GetElementsByTagName("Other");
                    node = nodelist.Item(0);
                    node.InnerText = drIMS["Other"].ToString();

                    nodelist = doc.GetElementsByTagName("Swiped");
                    node = nodelist.Item(0);
                    node.InnerText = drIMS["Swiped"].ToString();

                    nodelist = doc.GetElementsByTagName("KeyedWithImprint");
                    node = nodelist.Item(0);
                    node.InnerText = drIMS["KeyedWithoutImprint"].ToString();

                    nodelist = doc.GetElementsByTagName("AverageSaleAmount");
                    node = nodelist.Item(0);
                    node.InnerText = drIMS["AverageSaleAmount"].ToString();

                    nodelist = doc.GetElementsByTagName("AverageMonthlyVolume");
                    node = nodelist.Item(0);
                    node.InnerText = drIMS["AverageMonthlyVolume"].ToString();
                    #endregion

                    #region Principal
                    //Principal Information
                    nodelist = doc.GetElementsByTagName("Principal");
                    //First Principal
                    node = nodelist.Item(0);
                    childNode = node.ChildNodes.Item(0);
                    childNode.InnerText = drIMS["P1OwnershipPercent"].ToString();
                    childNode = node.ChildNodes.Item(1);
                    childNode.InnerText = drIMS["P1FirstName"].ToString().Trim();
                    childNode = node.ChildNodes.Item(2);
                    childNode.InnerText = drIMS["P1MidName"].ToString().Trim();
                    childNode = node.ChildNodes.Item(3);
                    childNode.InnerText = drIMS["P1LastName"].ToString().Trim();
                    childNode = node.ChildNodes.Item(4);
                    childNode.InnerText = "";
                    childNode = node.ChildNodes.Item(5);
                    childNode.InnerText = drIMS["P1DOB"].ToString().Trim();
                    childNode = node.ChildNodes.Item(6);
                    childNode.InnerText = drIMS["P1SSN"].ToString().Trim();
                    childNode = node.ChildNodes.Item(7);
                    childNode.InnerText = drIMS["P1DriversLicenseNo"].ToString().Trim();
                    childNode = node.ChildNodes.Item(8);
                    childNode.InnerText = drIMS["P1DriversLicenseState"].ToString().Trim();
                    childNode = node.ChildNodes.Item(9);
                    childNode.InnerText = drIMS["P1DriversLicenseExpiry"].ToString().Trim();
                    childNode = node.ChildNodes.Item(10);
                    childNode.InnerText = drIMS["P1PhoneNumber"].ToString().Trim();

                    //P1 Residence Address
                    childNode = node.ChildNodes.Item(11);
                    XmlNode P1Node = childNode.ChildNodes.Item(0);
                    P1Node.InnerText = drIMS["P1Address1"].ToString().Trim();
                    P1Node = childNode.ChildNodes.Item(1);
                    P1Node.InnerText = drIMS["P1Address2"].ToString().Trim();
                    P1Node = childNode.ChildNodes.Item(2);
                    P1Node.InnerText = drIMS["P1City"].ToString().Trim();
                    P1Node = childNode.ChildNodes.Item(3);
                    P1Node.InnerText = drIMS["P1State"].ToString().Trim();
                    P1Node = childNode.ChildNodes.Item(4);
                    P1Node.InnerText = drIMS["P1PostalCode"].ToString().Trim();

                    childNode = node.ChildNodes.Item(12);
                    childNode.InnerText = drIMS["P1LivingStatus"].ToString().Trim();
                    childNode = node.ChildNodes.Item(13);
                    childNode.InnerText = "0001-01-01";

                    //Second Principal
                    node = nodelist.Item(1);
                    childNode = node.ChildNodes.Item(0);
                    childNode.InnerText = drIMS["P2OwnershipPercent"].ToString();
                    childNode = node.ChildNodes.Item(1);
                    childNode.InnerText = drIMS["P2FirstName"].ToString().Trim();
                    childNode = node.ChildNodes.Item(2);
                    childNode.InnerText = drIMS["P2MidName"].ToString().Trim();
                    childNode = node.ChildNodes.Item(3);
                    childNode.InnerText = drIMS["P2LastName"].ToString().Trim();
                    childNode = node.ChildNodes.Item(4);
                    childNode.InnerText = "";
                    childNode = node.ChildNodes.Item(5);
                    childNode.InnerText = drIMS["P2DOB"].ToString().Trim();
                    childNode = node.ChildNodes.Item(6);
                    childNode.InnerText = drIMS["P2SSN"].ToString().Trim();
                    childNode = node.ChildNodes.Item(7);
                    childNode.InnerText = drIMS["P2DriversLicenseNo"].ToString().Trim();
                    childNode = node.ChildNodes.Item(8);
                    childNode.InnerText = drIMS["P2DriversLicenseState"].ToString().Trim();
                    childNode = node.ChildNodes.Item(9);
                    childNode.InnerText = drIMS["P2DriversLicenseExpiry"].ToString().Trim();
                    childNode = node.ChildNodes.Item(10);
                    childNode.InnerText = drIMS["P2PhoneNumber"].ToString().Trim();

                    //P2 Residence Address
                    childNode = node.ChildNodes.Item(11);
                    XmlNode P2Node = childNode.ChildNodes.Item(0);
                    P2Node.InnerText = drIMS["P2Address1"].ToString().Trim();
                    P2Node = childNode.ChildNodes.Item(1);
                    P2Node.InnerText = drIMS["P2Address2"].ToString().Trim();
                    P2Node = childNode.ChildNodes.Item(2);
                    P2Node.InnerText = drIMS["P2City"].ToString().Trim();
                    P2Node = childNode.ChildNodes.Item(3);
                    P2Node.InnerText = drIMS["P2State"].ToString().Trim();
                    P2Node = childNode.ChildNodes.Item(4);
                    P2Node.InnerText = drIMS["P2PostalCode"].ToString().Trim();

                    childNode = node.ChildNodes.Item(12);
                    childNode.InnerText = drIMS["P2LivingStatus"].ToString().Trim();
                    childNode = node.ChildNodes.Item(13);
                    childNode.InnerText = "0001-01-01";

                    #endregion

                    #region History
                    //History
                    /*nodelist = doc.GetElementsByTagName("HadPriorCardAcquirer");
                    node = nodelist.Item(0);
                    node.InnerText = drIMS["HadPriorCardAcquirer"].ToString().Trim();

                    nodelist = doc.GetElementsByTagName("PriorCardAcquirerName");
                    node = nodelist.Item(0);
                    node.InnerText = drIMS["PriorCardAcquirerName"].ToString().Trim();

                    nodelist = doc.GetElementsByTagName("PriorCardAcquirerDepartureReason");
                    node = nodelist.Item(0);
                    node.InnerText = drIMS["PriorCardAcquirerDepartureReason"].ToString().Trim();

                    node.InnerText = drIMS["TerminatedMerchantFile"].ToString().Trim();
                    node = nodelist.Item(0);
                    node.InnerText = drIMS["TerminatedMerchantFile"].ToString().Trim();
                    #endregion

                    #region Financial
                    //Financial
                    nodelist = doc.GetElementsByTagName("CreditCard");
                    childNode = nodelist.Item(0);
                    XmlNode CreditCardNode = childNode.ChildNodes.Item(0);
                    CreditCardNode.InnerText = drIMS["CardholderName"].ToString().Trim();
                    CreditCardNode = childNode.ChildNodes.Item(1);
                    CreditCardNode.InnerText = drIMS["AccountNumber"].ToString().Trim();
                    CreditCardNode = childNode.ChildNodes.Item(2);
                    CreditCardNode.InnerText = drIMS["ExpirationDate"].ToString().Trim();

                    nodelist = doc.GetElementsByTagName("Bank");
                    childNode = nodelist.Item(0);
                    XmlNode BankNode = childNode.ChildNodes.Item(0);
                    BankNode.InnerText = drIMS["BankName"].ToString().Trim();

                    //Bank Address
                    BankNode = childNode.ChildNodes.Item(1);
                    XmlNodeList BankAddressList = BankNode.ChildNodes;
                    XmlNode BankAddressNode = BankAddressList.Item(0);
                    BankAddressNode.InnerText = drIMS["BankAddress1"].ToString().Trim();
                    BankAddressNode = BankAddressList.Item(1);
                    BankAddressNode.InnerText = drIMS["BankAddress2"].ToString().Trim();
                    BankAddressNode = BankAddressList.Item(2);
                    BankAddressNode.InnerText = drIMS["BankCity"].ToString().Trim();
                    BankAddressNode = BankAddressList.Item(3);
                    BankAddressNode.InnerText = drIMS["BankState"].ToString().Trim();
                    BankAddressNode = BankAddressList.Item(4);
                    BankAddressNode.InnerText = drIMS["BankPostalCode"].ToString().Trim();

                    BankNode = childNode.ChildNodes.Item(2);
                    BankNode.InnerText = drIMS["BankRoutingNumber"].ToString().Trim();
                    BankNode = childNode.ChildNodes.Item(3);
                    BankNode.InnerText = drIMS["BankAccountNumber"].ToString().Trim();
                    #endregion

                    #region CardTypes
                    //Insert CardTypes
                    nodelist = doc.GetElementsByTagName("CardType");
                    //first card type
                    node = nodelist.Item(0);
                    XmlNode CardTypeNode = node.ChildNodes.Item(0);
                    CardTypeNode.InnerText = "AmericanExpress";
                    CardTypeNode = node.ChildNodes.Item(1);
                    CardTypeNode.InnerText = drIMS["AmexAccept"].ToString().Trim();
                    CardTypeNode = node.ChildNodes.Item(2);
                    CardTypeNode.InnerText = drIMS["AmexNum"].ToString().Trim();
                    //second card type
                    node = nodelist.Item(1);
                    CardTypeNode = node.ChildNodes.Item(0);
                    CardTypeNode.InnerText = "Discover";
                    CardTypeNode = node.ChildNodes.Item(1);
                    CardTypeNode.InnerText = drIMS["DiscoverAccept"].ToString().Trim();
                    CardTypeNode = node.ChildNodes.Item(2);
                    CardTypeNode.InnerText = drIMS["DiscoverNum"].ToString().Trim();
                    //third card type
                    node = nodelist.Item(2);
                    CardTypeNode = node.ChildNodes.Item(0);
                    CardTypeNode.InnerText = "Diners";
                    CardTypeNode = node.ChildNodes.Item(1);
                    CardTypeNode.InnerText = drIMS["DinersAccept"].ToString().Trim();
                    CardTypeNode = node.ChildNodes.Item(2);
                    CardTypeNode.InnerText = drIMS["DinersNum"].ToString().Trim();
                    #endregion

                    doc.Save("C:\\IMS" + AppId + ".xml");
                    DisplayMessage("XML Created.");
                }
                catch (Exception err)
                {
                    CreateLog Log = new CreateLog();
                    Log.ErrorLog(Server.MapPath("~/ErrorLog"), "Create IMS App - " + err.Message);
                    DisplayMessage("Error creating XML");
                }
            }//end if count not 0
            else
                DisplayMessage("XML Creation Failed.");
        }//end if user is in role
    }//end btnCreateIMSApp_Click


*/
#endregion
        
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
                pnlSagePDF.Visible = true;

                //Check to ensure correct BETs are being used
                decimal midQualStep = Convert.ToDecimal(dr["DiscRateMidQual"].ToString().Trim());
                decimal nonQualStep = Convert.ToDecimal(dr["DiscRateNonQual"].ToString().Trim());

                if ((midQualStep != 0.8m) || (nonQualStep != 1.9m))
                {
                    if ((midQualStep != 1m) || (nonQualStep != 1.5m))
                    {
                        if ((midQualStep != 0.5m) || (nonQualStep != 1m))
                        {
                            DisplayMessage("Only the following combinations of MidQualSteps and NonQualSteps can be used: 0.80, 1.90; 1.00, 1.50; 0.50, 1.00. Please correct MidQual and NonQual rates.");
                            pnlSagePDF.Visible = false;
                        }
                    }
                }
                if (Convert.ToDecimal(dr["ProcessPctSwiped"].ToString().Trim()) >= 70)
                    btnSageMOTO.Visible = false;
                else
                    btnSageMOTO.Visible = true;
                //pnlSagePDF.Visible = true;
            }

            else if (strProcessor.ToLower() == "ims")
            {
                OAPDFBL IMSData = new OAPDFBL(AppId);
                DataSet ds = IMSData.GetIMSData();
                DataRow dr = ds.Tables[0].Rows[0];
                if ((dr["Interchange"].ToString() == "True") || (dr["Assessments"].ToString() == "True"))
                    FileName = Server.MapPath("~/PDF/IMS Application Interchange.pdf");
                else
                    FileName = Server.MapPath("~/PDF/IMS Application.pdf");
                mStream = PDFFile.CreateIMSPDF(FileName);

                if (mStream != null)
                {
                    LogBL LogData = new LogBL(AppId);
                    LogData.InsertLogData(AffiliateID, "IMS PDF Created");

                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.ContentType = "application/pdf";
                    Response.AppendHeader("content-disposition", "filename=IMS Application.pdf");
                    Response.OutputStream.Write(mStream.GetBuffer(), 0, Convert.ToInt32(mStream.Length));

                    Response.Flush();
                    Response.Close();
                }
                else
                    DisplayMessage("Data not found for this record.");
            }
            else if (strProcessor.ToLower().Contains("ipayment"))
            {
                FileName = Server.MapPath("~/PDF/iPayment application.pdf");
                mStream = PDFFile.CreateIPayPDF(FileName);

                if (mStream != null)
                {
                    LogBL LogData = new LogBL(AppId);
                    LogData.InsertLogData(AffiliateID, "iPayment PDF Created");

                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.ContentType = "application/pdf";
                    Response.AppendHeader("content-disposition", "filename=iPayment Application.pdf");
                    Response.OutputStream.Write(mStream.GetBuffer(), 0, Convert.ToInt32(mStream.Length));

                    Response.Flush();
                    Response.Close();
                }
                else
                    DisplayMessage("Data not found for this record.");
            }
            else if (strProcessor.ToLower().Contains("merrick"))
            {
                FileName = Server.MapPath("~/PDF/CNP_Merrick.pdf");
                mStream = PDFFile.CreateMerrickPDF(FileName);

                if (mStream != null)
                {
                    LogBL LogData = new LogBL(AppId);
                    LogData.InsertLogData(AffiliateID, "Merrick PDF Created");

                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.ContentType = "application/pdf";
                    Response.AppendHeader("content-disposition", "filename=Merrick Application.pdf");
                    Response.OutputStream.Write(mStream.GetBuffer(), 0, Convert.ToInt32(mStream.Length));

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
                    LogBL LogData = new LogBL(AppId);
                    LogData.InsertLogData(AffiliateID, "Optimal Canada PDF Created");

                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.ContentType = "application/pdf";
                    Response.AppendHeader("content-disposition", "filename=Optimal CA Application.pdf");
                    Response.OutputStream.Write(mStream.GetBuffer(), 0, Convert.ToInt32(mStream.Length));

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
                    LogBL LogData = new LogBL(AppId);
                    LogData.InsertLogData(AffiliateID, "Optimal CAL PDF Created");

                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.ContentType = "application/pdf";
                    Response.AppendHeader("content-disposition", "filename=Optimal Cal App.pdf");
                    Response.OutputStream.Write(mStream.GetBuffer(), 0, Convert.ToInt32(mStream.Length));

                    Response.Flush();
                    Response.Close();
                }
                else
                    DisplayMessage("Data not found for this record.");
            }
            else if (strProcessor.ToLower().Contains("chase"))
            {
                OAPDFBL ChaseData = new OAPDFBL(AppId);
                DataSet ds = ChaseData.GetChaseData();
                DataRow dr = ds.Tables[0].Rows[0];
                if ((Convert.ToBoolean(dr["Interchange"])) || (Convert.ToBoolean(dr["Assessments"])))
                {
                    btnChaseFS3Tier.Visible = false;
                    btnChaseFSInterchangePlus.Visible = true;
                }
                else
                {
                    btnChaseFS3Tier.Visible = true;
                    btnChaseFSInterchangePlus.Visible = false;
                }
                pnlChasePDF.Visible = true;
            }
            else if (strProcessor.ToLower().Contains("kitts"))
            {
                FileName = Server.MapPath("~/PDF/St_Kitts_Application.pdf");
                mStream = PDFFile.CreateStKittsPDF(FileName);

                if (mStream != null)
                {
                    LogBL LogData = new LogBL(AppId);
                    LogData.InsertLogData(AffiliateID, "Optimal St. Kitts PDF Created");

                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.ContentType = "application/pdf";
                    Response.AppendHeader("content-disposition", "filename=Optimal St. Kitts App.pdf");
                    Response.OutputStream.Write(mStream.GetBuffer(), 0, Convert.ToInt32(mStream.Length));

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

    #region Sage PDFs
    protected void btnSageApp_Click(object sender, EventArgs e)
    {
        try
        {
            MemoryStream mStream = new MemoryStream();
            CreatePDF PDFFile = new CreatePDF(AppId);
            string FileName = Server.MapPath("~/PDF/Sage Application.pdf");
            mStream = PDFFile.CreateSagePDF(FileName);

            if (mStream != null)
            {
                LogBL LogData = new LogBL(AppId);
                LogData.InsertLogData(AffiliateID, "Sage PDF Created");

                Response.ClearContent();
                Response.ClearHeaders();
                Response.ContentType = "application/pdf";
                Response.AppendHeader("content-disposition", "filename=Sage Application.pdf");
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
                Response.OutputStream.Write(mStream.GetBuffer(), 0, Convert.ToInt32(mStream.Length));

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
            string FileName = Server.MapPath("/Application/Chase Fee schedule 3 tier.pdf");

            CreatePDF PDFFile = new CreatePDF(AppId);
            MemoryStream mStream = PDFFile.CreateChaseFS3TierPDF(FileName);
            if (mStream != null)
            {
                Response.ClearContent();
                Response.ClearHeaders();
                Response.ContentType = "application/pdf";
                Response.AppendHeader("content-disposition", "filename=Chase Fee schedule 3 tier.pdf");
                Response.OutputStream.Write(mStream.GetBuffer(), 0, Convert.ToInt32(mStream.Length));

                Response.Flush();
                Response.Close();

                LogBL LogData = new LogBL(AppId);
                LogData.InsertLogData(AffiliateID, "Chase Fee schedule 3 tier Created");
            }
            else
                DisplayMessage("Data not found for this record.");
        }//end try
        catch (Exception err)
        {
            DisplayMessage("Create Chase Fee schedule 3 tier PDF Error - " + err.Message);
            CreateOnlineAppLog Log = new CreateOnlineAppLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + AppId.ToString() + " - " + "Merchant PDF - Create Chase Fee schedule 3 tier PDF Error - " + err.Message);
        }
    }

    public void btnChaseFSInterchangePlus_Click(object sender, EventArgs e)
    {
        try
        {
            string FileName = Server.MapPath("/Application/Chase Fee schedule interchange plus.pdf");

            CreatePDF PDFFile = new CreatePDF(AppId);
            MemoryStream mStream = PDFFile.CreateChaseFSInterchangePlusPDF(FileName);
            if (mStream != null)
            {
                Response.ClearContent();
                Response.ClearHeaders();
                Response.ContentType = "application/pdf";
                Response.AppendHeader("content-disposition", "filename=Chase Fee schedule interchange plus.pdf.pdf");
                Response.OutputStream.Write(mStream.GetBuffer(), 0, Convert.ToInt32(mStream.Length));

                Response.Flush();
                Response.Close();

                LogBL LogData = new LogBL(AppId);
                LogData.InsertLogData(AffiliateID, "Chase Fee schedule interchange plus PDF Created");
            }
            else
                DisplayMessage("Data not found for this record.");
        }//end try
        catch (Exception err)
        {
            DisplayMessage("Create Chase Fee schedule interchange plus PDF Error - " + err.Message);
            CreateOnlineAppLog Log = new CreateOnlineAppLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + AppId.ToString() + " - " + "Merchant PDF - Create Chase Fee schedule interchange plus PDF Error - " + err.Message);
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
            if (txtNotes.Text.Trim() != "")
            {
                int AffiliateID = Convert.ToInt32(Session["AffiliateID"]);
                AffiliatesBL Aff = new AffiliatesBL(AffiliateID);
                //Add note that status has been changed
                string ActUserID = Aff.ReturnACTUserID();

                OnlineAppStatusBL App = new OnlineAppStatusBL(AppId);
                bool retVal = App.InsertNote(ActUserID, txtNotes.Text.Trim());
                //some online apps do not have an affiliate ID, so retVal is false
                /*if (!retVal)
                    DisplayMessage("Error Inserting Note");*/

                //Send email
                if (chkNotify.Checked)
                    SendEmail(txtNotes.Text);

                txtNotes.Text = "";
                LoadNotes();
            }//end if note not blank
            else
                DisplayMessage("Enter Note Text");
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
                LoadNotes();
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
            msg.From = new MailAddress("information@commercetech.com");
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
                BusinessLayer.SalesOppsBL Products = new BusinessLayer.SalesOppsBL();
                DataSet dsProducts = Products.GetProducts();
                if (dsProducts.Tables[0].Rows.Count > 0)
                {
                    lstReprogram.DataSource = dsProducts;
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

                    //Get list of products for Reprogram field in the Add Product panel
                    lstAddReprogram.DataSource = dsProducts;
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

                if (!User.IsInRole("Admin") && !User.IsInRole("Employee"))
                {
                    lstRepNameAdd.SelectedValue = lstRepNameAdd.Items.FindByValue(Session["MasterNum"].ToString()).Value;
                    lstRepNameAdd.Enabled = false;
                    txtAddCOG.Enabled = false;
                    txtAddSubtotal.Enabled = false;
                }
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
                    lstRepNameAdd.SelectedItem.Text, lstPayment.SelectedItem.Value, lstAddReprogram.SelectedItem.Value);
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
            PopulateAddSalesOpp(Convert.ToInt16(lstProductName.SelectedItem.Value.Trim()), 1);
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

            if ((dt[0].ProductName.ToString().Trim() == "Merchant Number") || (dt[0].ProductName.ToString().Trim() == "Online Debit"))
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

    protected void btnEditSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string SalesOppID = lblID.Text.ToString().Trim();

            BusinessLayer.SalesOppsBL Opp = new BusinessLayer.SalesOppsBL();
            bool retVal = Opp.EditSalesOpp(txtSellPrice.Text.Trim(), lstQuantity.SelectedItem.Value,
                lstEditPaymentMethod.SelectedItem.Value, lstReprogram.SelectedItem.Value,
                lstRepName.SelectedItem.Value, SalesOppID);
            if (!retVal)
                DisplayMessage("Error Updating Sales Opp");
            else
            {
                //Add action to Log table
                PartnerLogBL LogData = new PartnerLogBL();
                retVal = LogData.InsertLogData(AppId, Convert.ToInt32(Session["AffiliateID"]), "Sales Opp (" + lblProductName.Text.Trim() + ") Updated");
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
            lblID.Text = dt[0].ID.ToString().Trim();
            lblProductName.Text = dt[0].Product.ToString().Trim();
            txtSellPrice.Text = dt[0].Price.ToString().Trim();
            lblCOG.Text = dt[0].CostOfGoods.ToString();
            lstQuantity.SelectedValue = lstQuantity.Items.FindByValue(dt[0].Quantity.ToString().Trim()).Value;
            lblSubtotal.Text = dt[0].Subtotal.ToString().Trim();
            lstRepName.SelectedValue = lstRepName.Items.FindByValue(dt[0].RepNum.ToString().Trim()).Value;
            lstEditPaymentMethod.SelectedValue = dt[0].PaymentMethod.ToString();
            if ((dt[0].Product.ToString().Trim() == "Merchant Number") || (dt[0].Product.ToString().Trim() == "Online Debit"))
            {
                lstReprogram.Enabled = true;
                lstReprogram.SelectedValue = dt[0].Reprogram.ToString();
            }
            else
            {
                lstReprogram.Enabled = false;
                lstReprogram.SelectedValue = lstReprogram.Items.FindByText("").Value;
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
                if (Locked() || e.Row.Cells[7].Text == "Sent to Payroll")
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
            //if any other user then delete, edit and Add/Update are not visible
            else
            {
                if (Locked() || e.Row.Cells[7].Text == "Sent to Payroll")
                    e.Row.Cells[13].Visible = false;
                else
                    e.Row.Cells[13].Visible = true;

                e.Row.Cells[12].Visible = false;
                e.Row.Cells[14].Visible = false;
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
                    PopulateAddlServices();
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
        if (dsProcessingInfo.Tables[0].Rows.Count > 0)
        {
            DataRow drProcessingInfo = dsProcessingInfo.Tables[0].Rows[0];
            lblApplicationFee.Text = drProcessingInfo["AppFee"].ToString().Trim();
            lblApplicationSetupFee.Text = drProcessingInfo["AppSetupFee"].ToString().Trim();
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
        if (dsGatewayInfo.Tables[0].Rows.Count > 0)
        {
            DataRow drGatewayInfo = dsGatewayInfo.Tables[0].Rows[0];
            lblGateway.Text = drGatewayInfo["Gateway"].ToString().Trim();
            lblGatewaySetupFee.Text = drGatewayInfo["GatewaySetupFee"].ToString().Trim();
            lblGatewayTransFee.Text = drGatewayInfo["GatewayTransFee"].ToString().Trim();
            lblMonthlyGatewayAccess.Text = drGatewayInfo["GatewayMonFee"].ToString().Trim();
        }//end if count not 0
    }//end function populate gateway rates

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

        //Get Additional services info from online app new app
        NewAppInfo AddlServ = new NewAppInfo(AppId);
        DataSet dsAddl = AddlServ.GetNewAppData();
        if (dsAddl.Tables[0].Rows.Count > 0)
        {
            DataRow dr = dsAddl.Tables[0].Rows[0];
            ProcessingInfo Processing = new ProcessingInfo(AppId);
            DataSet dsProcessingInfo = Processing.GetAddlServices();
            if (dsProcessingInfo.Tables[0].Rows.Count > 0)
            {
                DataRow drProcessingInfo = dsProcessingInfo.Tables[0].Rows[0];

                TableRow tr;
                TableCell td;
                Label lblValue;

                ProcessingInfo Proc = new ProcessingInfo(AppId);
                string CardPresent = Proc.ReturnCardPresent();


                if (Convert.ToBoolean(dr["OnlineDebit"]) || Convert.ToBoolean(dr["CheckServices"]) || Convert.ToBoolean(dr["GiftCard"])
                    || Convert.ToBoolean(dr["EBT"]))
                {
                    tr = new TableRow();
                    //Online Debit Header
                    lblValue = new Label();
                    lblValue.Text = "Additional Services";
                    td = new TableCell();
                    td.Attributes.Add("align", "center");
                    td.Attributes.Add("colspan", "2");
                    lblValue.ApplyStyle(ValueLabel);
                    td.Controls.Add(lblValue);
                    lblValue.Font.Bold = true;
                    tr.Cells.Add(td);
                    tblAddlServices.Rows.Add(tr);
                }

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

                if (Convert.ToBoolean(dr["CheckServices"]))
                {
                    tr = new TableRow();
                    //Check Guarantee Header
                    lblValue = new Label();
                    lblValue.Text = "Check Guarantee";
                    td = new TableCell();
                    td.Attributes.Add("align", "left");
                    td.Attributes.Add("colspan", "2");
                    lblValue.ApplyStyle(ValueLabel);
                    td.Controls.Add(lblValue);
                    lblValue.Font.Bold = true;
                    tr.Cells.Add(td);
                    tblAddlServices.Rows.Add(tr);

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
                }


                if (CardPresent == "CP")
                {
                    if (Convert.ToBoolean(dr["GiftCard"]))
                    {
                        tr = new TableRow();
                        //Gift Card Header
                        lblValue = new Label();
                        lblValue.Text = "Gift Card";
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        td.Attributes.Add("colspan", "2");
                        lblValue.ApplyStyle(ValueLabel);
                        td.Controls.Add(lblValue);
                        lblValue.Font.Bold = true;
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
                    }//end if giftcard


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
                lstAppType.SelectedValue =  lstAppType.Items.FindByValue(drNewApp["Redirect"].ToString().Trim()).Value;
                lstItem = lstOtherReferral.Items.FindByValue(drNewApp["OtherReferral"].ToString());

                //If Other Referral does not exist in the dropdown, add it and select it
                if (lstItem == null)
                    lstOtherReferral.Items.Add(drNewApp["OtherReferral"].ToString());
                lstOtherReferral.SelectedValue = lstOtherReferral.Items.FindByValue(drNewApp["OtherReferral"].ToString()).Value; 
                
                lstAccountType.SelectedValue = lstAccountType.Items.FindByValue(drNewApp["acctType"].ToString()).Value;
               
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
                            Convert.ToInt32(lstAccountType.SelectedValue) );

                        //Update NewAppInformation
                        OnlineAppBL NewApp = new OnlineAppBL(AppId);
                        NewApp.UpdateReferral(Convert.ToInt32(lstReferredBy.SelectedValue), lstOtherReferral.SelectedItem.Text);

                        //Update RepNum 
                        NewApp.UpdateNewAppInfo(lstSalesRep.SelectedValue.ToString(), lstAppType.SelectedValue.ToString());

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
                lblDiscoverText.Text = MaskNumbers(Server.HtmlEncode(dt[0].DiscoverNum.ToString().Trim()));
            else
                lblDiscoverText.Text = Server.HtmlEncode(dt[0].DiscoverNum.ToString().Trim());

            if (!dt[0].AmexNum.ToString().Trim().Contains("Opted") && !dt[0].AmexNum.ToString().Trim().Contains("Submitted"))
                lblAmexText.Text = MaskNumbers(Server.HtmlEncode(dt[0].AmexNum.ToString().Trim()));
            else
                lblAmexText.Text = Server.HtmlEncode(dt[0].AmexNum.ToString().Trim());

            if (!dt[0].JCBNum.ToString().Trim().Contains("Opted") && !dt[0].JCBNum.ToString().Trim().Contains("Submitted"))
                lblJCBText.Text = MaskNumbers(Server.HtmlEncode(dt[0].JCBNum.ToString().Trim()));
            else
                lblJCBText.Text = Server.HtmlEncode(dt[0].JCBNum.ToString().Trim());
            lblLoginUserIDText.Text = dt[0].GatewayUserID.ToString().Trim();
            lblGatewayText.Text = dt[0].Gateway.ToString().Trim();
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
        if (dsPlatform.Tables[0].Rows.Count > 0)
        {

            PopulatePlatformList();
            DataRow dr = dsPlatform.Tables[0].Rows[0];
            lblVisaMasterNumberText.Text = MaskNumbers(Server.HtmlEncode(dr["MerchantNum"].ToString().Trim()));
            lstPlatform.SelectedValue = dr["Platform"].ToString();

            //lblPlatformACT.Text = dr["Platform"].ToString().Trim();
            lblMerchantIDText.Text = MaskNumbers(Server.HtmlEncode(dr["MerchantID"].ToString().Trim()));
            lblLoginIDText.Text = MaskNumbers(Server.HtmlEncode(dr["LoginID"].ToString().Trim()));
            lblTerminalIDText.Text = MaskNumbers(Server.HtmlEncode(dr["TerminalID"].ToString().Trim()));
            lblBINNumberText.Text = MaskNumbers(Server.HtmlEncode(dr["BankIDNum"].ToString().Trim()));
            lblAgentBankNumberText.Text = MaskNumbers(Server.HtmlEncode(dr["AgentBankIDNum"].ToString().Trim()));
            lblAgentChainNumberText.Text = MaskNumbers(Server.HtmlEncode(dr["AgentChainNum"].ToString().Trim()));
            lblMCCCodeText.Text = MaskNumbers(Server.HtmlEncode(dr["MCCCategoryCode"].ToString().Trim()));
            lblStoreNumberText.Text = MaskNumbers(Server.HtmlEncode(dr["StoreNum"].ToString().Trim()));
        }//end if count not 0
        else
            lstPlatform.SelectedValue = "None";
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
        }//end if count not 0
        else
            lblRPlatformACT.Text = "None";
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
            OnlineAppStatusBL StatusInfo = new OnlineAppStatusBL(AppId);
            PartnerDS.OnlineAppStatusFieldsDataTable dt = StatusInfo.GetStatusFields();
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

            if ((AcctType == 2) || (AcctType == 3))
            {
                MerchantStatus = "";
                Platform = "";
            }
            if ((AcctType == 1) || (AcctType == 3))
                GatewayStatus = "";
            //Update status information
            string strRetVal = StatusInfo.UpdateStatusInformation(MerchantStatus, GatewayStatus, Platform,
                AffiliateID);
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
            PopulateHistory();
            LoadNotes();
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
        ListItem lstItem = new System.Web.UI.WebControls.ListItem();
        lstItem.Text = "None";
        lstItem.Value = "None";
        lstPlatform.Items.Add(lstItem);
        //lstPlatform.SelectedIndex = lstPlatform.Items.IndexOf(lstPlatform.Items.FindByText("None"));
    }
    #endregion

    #region CARDPCT - Removed
    /*//This function populates card percentages
    public void PopulateCardPCT()
    {
        System.Web.UI.WebControls.ListItem lstItem = new System.Web.UI.WebControls.ListItem();
        for (int i = 0; i < 101; i = i + 1)
        {
            lstItem = new System.Web.UI.WebControls.ListItem();
            lstItem.Text = i.ToString();
            lstItem.Value = i.ToString();
            lstInternet.Items.Add(lstItem);
        }

        for (int i = 0; i < 101; i = i + 1)
        {
            lstItem = new System.Web.UI.WebControls.ListItem();
            lstItem.Text = i.ToString();
            lstItem.Value = i.ToString();
            lstSwiped.Items.Add(lstItem);
        }

        for (int i = 0; i < 101; i = i + 1)
        {
            lstItem = new System.Web.UI.WebControls.ListItem();
            lstItem.Text = i.ToString();
            lstItem.Value = i.ToString();
            lstOther.Items.Add(lstItem);
        }

        for (int i = 0; i < 101; i = i + 1)
        {
            lstItem = new System.Web.UI.WebControls.ListItem();
            lstItem.Text = i.ToString();
            lstItem.Value = i.ToString();
            lstMail.Items.Add(lstItem);
        }

        for (int i = 0; i < 101; i = i + 1)
        {
            lstItem = new System.Web.UI.WebControls.ListItem();
            lstItem.Text = i.ToString();
            lstItem.Value = i.ToString();
            lstKeyedWith.Items.Add(lstItem);
        }

        for (int i = 0; i < 101; i = i + 1)
        {
            lstItem = new System.Web.UI.WebControls.ListItem();
            lstItem.Text = i.ToString();
            lstItem.Value = i.ToString();
            lstKeyedWithout.Items.Add(lstItem);
        }

        for (int i = 0; i < 101; i = i + 1)
        {
            lstItem = new System.Web.UI.WebControls.ListItem();
            lstItem.Text = i.ToString();
            lstItem.Value = i.ToString();
            lstRestaurant.Items.Add(lstItem);
        }

        for (int i = 0; i < 101; i = i + 1)
        {
            lstItem = new System.Web.UI.WebControls.ListItem();
            lstItem.Text = i.ToString();
            lstItem.Value = i.ToString();
            lstRetail.Items.Add(lstItem);
        }

        for (int i = 0; i < 101; i = i + 1)
        {
            lstItem = new System.Web.UI.WebControls.ListItem();
            lstItem.Text = i.ToString();
            lstItem.Value = i.ToString();
            lstService.Items.Add(lstItem);
        }

        CardPCT CardPCTInfo = new CardPCT(AppId);
        DataSet dsCardPCTInfo = CardPCTInfo.GetCardPCT();
        if (dsCardPCTInfo.Tables[0].Rows.Count > 0)
        {
            DataRow drCardPCT = dsCardPCTInfo.Tables["OnlineAppCardPCT"].Rows[0];

			if (lstInternet.Items.FindByValue(drCardPCT["BusinessPctInternet"].ToString()) != null)
				lstInternet.SelectedValue = drCardPCT["BusinessPctInternet"].ToString();
			if ( lstMail.Items.FindByValue(drCardPCT["BusinessPctMailOrder"].ToString()) != null)
				lstMail.SelectedValue = drCardPCT["BusinessPctMailOrder"].ToString();
			if ( lstRestaurant.Items.FindByValue(drCardPCT["BusinessPctRestaurant"].ToString())!= null)
			    lstRestaurant.SelectedValue = drCardPCT["BusinessPctRestaurant"].ToString();
            if (lstOther.Items.FindByValue(drCardPCT["BusinessPctOther"].ToString()) != null)
				lstOther.SelectedValue = drCardPCT["BusinessPctOther"].ToString();
			if (  lstRetail.Items.FindByValue(drCardPCT["BusinessPctRetail"].ToString()) != null)
				lstRetail.SelectedValue = drCardPCT["BusinessPctRetail"].ToString();
			if ( lstService.Items.FindByValue(drCardPCT["BusinessPctService"].ToString()) != null)
				lstService.SelectedValue = drCardPCT["BusinessPctService"].ToString();
			if (  lstSwiped.Items.FindByValue(drCardPCT["ProcessPctSwiped"].ToString()) != null)
				lstSwiped.SelectedValue = drCardPCT["ProcessPctSwiped"].ToString();
            if ( lstKeyedWith.Items.FindByValue(drCardPCT["ProcessPctKeyedwImprint"].ToString()) != null)
				lstKeyedWith.SelectedValue = drCardPCT["ProcessPctKeyedwImprint"].ToString();
			if ( lstKeyedWithout.Items.FindByValue(drCardPCT["ProcessPctKeyedwoImprint"].ToString()) != null )
				lstKeyedWithout.SelectedValue = drCardPCT["ProcessPctKeyedwoImprint"].ToString();

    
        }//end if count not 0
    }//end function populatepage

    //This function handles submit button click event
    protected void btnSubmitCardPCT_Click(object sender, EventArgs e)
    {
        OnlineAppProfile GeneralProfileInfo = new OnlineAppProfile(AppId);
        DataSet dsNewApp = GeneralProfileInfo.GetProfileData();
        DataRow drNewApp = dsNewApp.Tables[0].Rows[0];  
        int ProcessingPercentages = (Convert.ToInt32(lstKeyedWith.SelectedItem.Value.ToString())) + (Convert.ToInt32(lstKeyedWithout.SelectedItem.Value.ToString())) + (Convert.ToInt32(lstSwiped.SelectedItem.Value.ToString()));
        int BusinessPercentages = (Convert.ToInt32(lstService.SelectedItem.Value.ToString())) + (Convert.ToInt32(lstInternet.SelectedItem.Value.ToString())) + (Convert.ToInt32(lstRetail.SelectedItem.Value.ToString())) +
            (Convert.ToInt32(lstMail.SelectedItem.Value.ToString())) + (Convert.ToInt32(lstRestaurant.SelectedItem.Value.ToString())) + (Convert.ToInt32(lstOther.SelectedItem.Value.ToString()));
        int KeyedPercentages = (Convert.ToInt32(lstKeyedWith.SelectedItem.Value.ToString())) + (Convert.ToInt32(lstKeyedWithout.SelectedItem.Value.ToString()));
        int InternetPercentages = (Convert.ToInt32(lstInternet.SelectedItem.Value.ToString()) );
        int MailPercentages = (Convert.ToInt32(lstMail.SelectedItem.Value.ToString()));
        if ((ProcessingPercentages < 100) || (ProcessingPercentages > 100))
            DisplayMessage("Please make sure your Processing Percentages sum up to 100%");
        else if ((BusinessPercentages < 100) || (BusinessPercentages > 100))
            DisplayMessage("Please make sure your Business Percentages sum up to 100%");
        //Constraint to check for keyed percentages. Quickbooks account do not have this constraint.
        else if (((InternetPercentages + MailPercentages) > KeyedPercentages) && (drNewApp["Redirect"].ToString().Trim() != "QuickBooks"))
            DisplayMessage("Your combined Mail/Phone and Internet Percentage cannot be greater than your combined Keyed Percentages of " + KeyedPercentages.ToString() + "\n%. Please adjust either your Processing Percentages, Mail/Phone or Internet Percentages.");
        else
        {
            try
            {
                CardPCT CardPCTInfo = new CardPCT(AppId);
                bool retVal = CardPCTInfo.UpdateCardPCT(lstRetail.SelectedItem.Value.ToString().Trim(), lstRestaurant.SelectedItem.Value.ToString().Trim(),
                    lstMail.SelectedItem.Value.ToString().Trim(), lstInternet.SelectedItem.Value.ToString().Trim(), lstSwiped.SelectedItem.Value.ToString().Trim(),
                    lstKeyedWith.SelectedItem.Value.ToString().Trim(), lstKeyedWithout.SelectedItem.Value.ToString().Trim(), lstService.SelectedItem.Value.ToString().Trim(),
                    lstOther.SelectedItem.Value.ToString().Trim(), true);

                if (retVal)
                {
                    LogBL LogData = new LogBL(AppId);
                    LogData.InsertLogData(Convert.ToInt32(Session["AffiliateID"]), "Merchant Card Percentages Information Updated");
                }

                NewAppInfo NewApp = new NewAppInfo(AppId);

                int OLDPID = NewApp.ReturnPID();

                PackageInfo Pack = new PackageInfo();
                int PID = Pack.ReturnNewPID(AppId, Convert.ToInt32(lstSwiped.SelectedItem.Value.Trim()));
                if (OLDPID != PID)
                {
                    NewApp.UpdatePID(PID);

                    PackageInfo Package = new PackageInfo();
                    DataSet dsPackageInfo = Package.GetPackageInfo(PID);
                    DataTable dtPackageInfo = dsPackageInfo.Tables[0];
                    if (dtPackageInfo.Rows.Count > 0)
                    {
                        DataRow drPackageInfo = dtPackageInfo.Rows[0];
                        if ((AcctType == 1) || (AcctType == 2) || (AcctType == 4))
                        {
                            //Insert rates in Processing table
                            ProcessingInfo ProcessingRates = new ProcessingInfo(AppId);
                            bool retVal2 = ProcessingRates.UpdateProcessingInfo(
                                drPackageInfo["Processor"].ToString().Trim(), drPackageInfo["CardPresent"].ToString().Trim(),
                                drPackageInfo["CustServFee"].ToString().Trim(),
                                drPackageInfo["InternetStmt"].ToString().Trim(), drPackageInfo["TransactionFee"].ToString().Trim(),
                                drPackageInfo["DiscRateQualPres"].ToString().Trim(), drPackageInfo["DiscRateQualNP"].ToString().Trim(),
                                drPackageInfo["DiscRateMidQual"].ToString().Trim(), drPackageInfo["DiscRateNonQual"].ToString().Trim(),
                                drPackageInfo["DiscRateQualDebit"].ToString().Trim(), drPackageInfo["ChargebackFee"].ToString().Trim(),
                                drPackageInfo["RetrievalFee"].ToString().Trim(), drPackageInfo["VoiceAuth"].ToString().Trim(),
                                drPackageInfo["BatchHeader"].ToString().Trim(), drPackageInfo["AVS"].ToString().Trim(),
                                drPackageInfo["MonMin"].ToString().Trim(), drPackageInfo["NBCTransFee"].ToString().Trim(),
                                drPackageInfo["AnnualFee"].ToString().Trim(), drPackageInfo["WirelessAccessFee"].ToString().Trim(),
                                drPackageInfo["WirelessTransFee"].ToString().Trim(), drPackageInfo["AppSetupFee"].ToString().Trim(),
                                drPackageInfo["AppFee"].ToString().Trim(), drPackageInfo["DebitMonFee"].ToString().Trim(),
                                drPackageInfo["DebitTransFee"].ToString().Trim(), drPackageInfo["CGMonFee"].ToString().Trim(),
                                drPackageInfo["CGTransFee"].ToString().Trim(), drPackageInfo["CGMonMin"].ToString().Trim(),
                                drPackageInfo["CGDiscRate"].ToString().Trim(), drPackageInfo["GCMonFee"].ToString().Trim(),
                                drPackageInfo["GCTransFee"].ToString().Trim(), drPackageInfo["EBTMonFee"].ToString().Trim(),
                                drPackageInfo["EBTTransFee"].ToString().Trim());

                            //Insert rates in gateway info table
                            Gateway GatewayInfo = new Gateway(AppId);
                            bool retVal3 = GatewayInfo.UpdateGatewayInfo(drPackageInfo["Gateway"].ToString().Trim(),
                                drPackageInfo["GatewayMonFee"].ToString().Trim(), drPackageInfo["GatewaySetupFee"].ToString().Trim(),
                                drPackageInfo["GatewayTransFee"].ToString().Trim());

                        }//end if AcctType 1
                    }//end if count not 0
                }//end if old pid not equal to new pid

                //Insert merchant account sales opp
                OnlineAppClassLibrary.SalesOppsBL SalesOpp = new OnlineAppClassLibrary.SalesOppsBL(AppId);
                retVal = SalesOpp.CreateSalesOpps();

                //Insert Gateway Sales Opp if account type is merchant and gateway
                if (AcctType == 4)
                {
                    SalesOpp = new OnlineAppClassLibrary.SalesOppsBL(AppId);
                    retVal = SalesOpp.CreateSalesOppsGW();
                }

                //Set last modified date in newapp table
                CommonFunctions GeneralInfo = new CommonFunctions(AppId);
                GeneralInfo.UpdateLastModified();
                DisplayMessage("Card Percentages Updated");
            }//end try
            catch (Exception err)
            {
                CreateLog Log = new CreateLog();
                Log.ErrorLog(Server.MapPath("~/ErrorLog"), "Update CardPCT - " + err.Message);
                DisplayMessage("Error modifying status information.");
            }
        }//end else
    }//end function submit button click*/
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