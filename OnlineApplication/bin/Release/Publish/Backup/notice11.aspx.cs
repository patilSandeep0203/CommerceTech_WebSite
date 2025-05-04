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
using OnlineAppClassLibrary;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Text;
using System.IO;
using System.Net;
using System.Collections.Specialized;
using DataLayer;
using System.Xml;
using Newtonsoft.Json;
using BusinessLayer;


/*using System.Xml;
using System.Diagnostics;
using System.Web.Services;
using System.ComponentModel;
using System.Web.Services.Protocols;
using System.Xml.Serialization;*/
//using System.Web.Services.Protocols.SoapHttpClientProtocol;

public partial class notice : System.Web.UI.Page
{
    private static int AppId = 0;
    private static int AffiliateID = 0;
    private static int PID = 0;

    protected void Page_Load(object sender, EventArgs e)
    {

        OAPDFBL SageData = new OAPDFBL(AppId);
        DataSet dsSageMonth = SageData.GetSageData();

        Response.Cache.SetNoStore();
        /*
        Response.Buffer = true;
        Response.ExpiresAbsolute = DateTime.Now.AddHours(-1);
        Response.Expires = 0;
        Response.CacheControl = "no-cache";*/

        string strCountry = "";

        AppId = Convert.ToInt32(Session["AppId"]);
        AffiliateID = Convert.ToInt32(Session["AffiliateID"]);
        if (Session.IsNewSession)
            Response.Redirect("/Application/default.aspx?SessionExpired=True");

        Response.CacheControl = "no-cache";
        Response.AddHeader("Pragma", "no-cache");
        Response.Expires = -1;

        //imgCreatePDF.Attributes.Add("onmouseover", "this.src = '/Application/Images/CreatePDF_Mouseover.gif'");
        //imgCreatePDF.Attributes.Add("onmouseout", "this.src = '/Application/Images/CreatePDF.gif'");

        Style errLabel = new Style();
        errLabel.BackColor = System.Drawing.Color.FromArgb(255, 192, 192);
        errLabel.ForeColor = System.Drawing.Color.Black;
        errLabel.BorderColor = System.Drawing.Color.Red;
        errLabel.BorderStyle = BorderStyle.Solid;
        errLabel.BorderWidth = Unit.Pixel(1);
        errLabel.Font.Size = FontUnit.Small;
        lblError.ApplyStyle(errLabel);
        //lblIncomplete.ApplyStyle(errLabel);

        lblIncomplete.Font.Size = FontUnit.Point(10);
        lblIncomplete.ForeColor = System.Drawing.Color.Red;

        lblIncompleteGW.Font.Size = FontUnit.Point(10);
        lblIncompleteGW.ForeColor = System.Drawing.Color.Red;

        lblGatewayOnly.Font.Size = FontUnit.Point(10);

        /*
        if (Session["DocusignRole"].ToString().Trim() == "Agent")
        {
            pnlDigital.Visible = false;
            pnlDigitalSig.Visible = false;
            btnDigitalSignature.Enabled = false;
            btnDigitalSignature.Visible = false;
            pnlDigSignNote.Visible = false;
        }*/



        if (!IsPostBack)
        {
            if (!User.Identity.IsAuthenticated)
                Response.Redirect("/Application/default.aspx?Authentication=False");
            else
            {
                try
                {
                    CreatePDF PDFFile = new CreatePDF(AppId);
                    string strProcessor = PDFFile.ReturnProcessorName();

                    if (!Locked())
                    {

                        lblHeading.CssClass = "MenuHeader";
                        if (Convert.ToInt32(Session["AcctType"]) == 1)
                            lblHeading.Text = "Merchant Account Signup Information";
                        else if (Convert.ToInt32(Session["AcctType"]) == 2)
                            lblHeading.Text = "Payment Gateway Signup Information";
                        else
                            lblHeading.Text = "Merchant Account & Payment Gateway Signup Information";

                        CheckAppStatus();

                        //CreatePDF PDFFile = new CreatePDF(AppId);
                        //string strProcessor = PDFFile.ReturnProcessorName();
                        string strLeaseCompany = PDFFile.ReturnLeaseCompany();
                        if (strProcessor.ToLower().Contains("sage"))
                        {
                            pnlSagePDF.Visible = true;
                            pnlSageDnCDocuSign.Visible = true;
                            if (dsSageMonth.Tables[0].Rows.Count > 0)
                            {
                                DataRow drSageMonth = dsSageMonth.Tables[0].Rows[0];
                                if (Convert.ToString(drSageMonth["ContractTerm"]) == "1 month")
                                {
                                    lnkAmendment1.Visible = true;
                                }
                                else if (Convert.ToString(drSageMonth["ContractTerm"]) == "1 year")
                                {
                                    lnkAmendment1.Visible = true;
                                }
                            }
                        }

                        if ((!strProcessor.ToLower().Contains("sage")) && (!strProcessor.ToLower().Contains("ipayment")) && (!strProcessor.ToLower().Contains("cardconnect")))
                        {
                            pnlDigital.Visible = false;
                            pnlDigitalSig.Visible = false;
                            pnlDigSignNote.Visible = false;
                        }
                        else
                        {
                            pnlDigital.Visible = true;
                            pnlDigitalSig.Visible = true;
                            pnlDigSignNote.Visible = true;
                        }

                        NewAppInfo newApp = new NewAppInfo(AppId);
                        bool docuSignStatus = newApp.docuSignStatus();
                        if (docuSignStatus == true)
                        {

                            string docuSignProcessor = newApp.GetDocuSignProcessor();
                            string envelopeId = newApp.GetDocuSignEnvId();
                            if (envelopeId != "")
                            {
                                btnCreatePDF.Enabled = true;
                                lnkSageAgreement.Enabled = true;
                                lnkAmendment1.Enabled = true;
                            }

                        }


                        pnlThankyou.Visible = false;
                        
                        
                        /*
                      if (strLeaseCompany.ToLower().Contains("northern"))
                      {
                          pnlLeaseDigSign.Visible = true;
                      }
                      else
                      {
                          pnlLeaseDigSign.Visible = false;
                      }*/
                    }
                    else
                    {
                        NewAppInfo AppInfo = new NewAppInfo(AppId);
                        string Status = AppInfo.ReturnStatus();
                        string StatusGW = AppInfo.ReturnStatusGW();

                        if (Status.ToLower().Contains("review"))
                        {
                            //CreatePDF PDFFile = new CreatePDF(AppId);
                            //string strProcessor = PDFFile.ReturnProcessorName();

                            /*pnlCreatePDF.Visible = true;
                            pnlMA.Visible = true;
                            pnlDigital.Visible = true;
                            pnlDigitalSig.Visible = true;
                            btnCreatePDF.Enabled = true;
                            btnDigitalSignature.Enabled = true;*/

                            /*
                             * if (strProcessor.ToLower().Contains("sage"))
                            {
                                pnlSagePDF.Visible = true;
                                pnlSageDnCDocuSign.Visible = true;
                                if (dsSageMonth.Tables[0].Rows.Count > 0)
                                {
                                    DataRow drSageMonth = dsSageMonth.Tables[0].Rows[0];
                                    if (Convert.ToString(drSageMonth["ContractTerm"]) == "1 month")
                                    {
                                        lnkAmendment1.Visible = true;
                                    }
                                    else if (Convert.ToString(drSageMonth["ContractTerm"]) == "1 year")
                                    {
                                        lnkAmendment1.Visible = true;
                                    }
                                }
                            }*/

                            //if ((!strProcessor.ToLower().Contains("sage")) && (!strProcessor.ToLower().Contains("ipayment")) && (!strProcessor.ToLower().Contains("cardconnect")))
                            {
                                pnlDigital.Visible = false;
                                pnlDigitalSig.Visible = false;
                                pnlDigSignNote.Visible = false;
                            }

                            pnlCreatePDF.Visible = false;
                            pnlMA.Visible = false;
                            pnlDigital.Visible = false;
                            pnlDigitalSig.Visible = false;
                            btnCreatePDF.Enabled = false;
                            btnDigitalSignature.Enabled = false;

                            pnlThankyou.Visible = true;
                            //
                            /*else
                            {
                                pnlDigital.Visible = true;
                                pnlDigitalSig.Visible = true;
                                pnlDigSignNote.Visible = true;
                            }*/
                        }
                        else
                        {
                            SetErrorMessage("The status of your application prevents it from being edited");
                            /*pnlCreatePDF.Visible = true;
                            pnlMA.Visible = true;
                            pnlDigital.Visible = true;
                            pnlDigitalSig.Visible = true;
                            btnCreatePDF.Enabled = false;
                            btnDigitalSignature.Enabled = false;
                            lnkUploadDocusign.Enabled = true;*/

                            pnlCreatePDF.Visible = false;
                            pnlMA.Visible = false;
                            pnlDigital.Visible = false;
                            pnlDigitalSig.Visible = false;
                            btnCreatePDF.Enabled = false;
                            btnDigitalSignature.Enabled = false;

                            pnlDigital.Visible = false;
                            pnlDigitalSig.Visible = false;
                            pnlDigSignNote.Visible = false;

                            pnlThankyou.Visible = true;
                            
                        }

                        /*if (strProcessor.ToLower().Contains("sage"))
                        {
                            pnlSagePDF.Visible = true;
                            pnlSageDnCDocuSign.Visible = true;
                        }*/

                        NewAppInfo newApp = new NewAppInfo(AppId);
                        bool docuSignStatus = newApp.docuSignStatus();
                        if (docuSignStatus == true)
                        {

                            string docuSignProcessor = newApp.GetDocuSignProcessor();
                            string envelopeId = newApp.GetDocuSignEnvId();
                            if (envelopeId != "")
                            {
                                /*btnCreatePDF.Enabled = true;
                                lnkSageAgreement.Enabled = true;
                                lnkAmendment1.Enabled = true;*/

                                btnCreatePDF.Enabled = false;
                                lnkSageAgreement.Enabled = false;
                                lnkAmendment1.Enabled = false;
                            }

                        }
                    }
                }//end try
                catch (Exception err)
                {
                    CreateOnlineAppLog Log = new CreateOnlineAppLog();
                    //Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + Session["AppId"].ToString() + " - " + "Notice.aspx Page Load Error - " + err.Message);
                    //SetErrorMessage("Error Processing Request. Please contact Technical Support: <a href='mailto:support@commercetech.com?Subject=Online Application Error'> support@commercetech.com. </a>.");
                }
                lblAppId.Text = Convert.ToString(Session["AppId"]);
                lblAppId.Font.Size = FontUnit.Point(10);
                lblAppId.ForeColor = System.Drawing.Color.Red;
            }
        }//end if postback
    }//end page load

    protected void Populate()
    {
        OnlineAppStatus appLocked = new OnlineAppStatus(AppId);
        string strLocked = appLocked.GetLocked();
        Session["Locked"] = strLocked;


        AppId = Convert.ToInt32(Session["AppId"]);
        AffiliateID = Convert.ToInt32(Session["AffiliateID"]);
        if (Session.IsNewSession)
            Response.Redirect("/Application/default.aspx?SessionExpired=True");

        Response.CacheControl = "no-cache";
        Response.AddHeader("Pragma", "no-cache");
        Response.Expires = -1;

        //imgCreatePDF.Attributes.Add("onmouseover", "this.src = '/Application/Images/CreatePDF_Mouseover.gif'");
        //imgCreatePDF.Attributes.Add("onmouseout", "this.src = '/Application/Images/CreatePDF.gif'");

        Style errLabel = new Style();
        errLabel.BackColor = System.Drawing.Color.FromArgb(255, 192, 192);
        errLabel.ForeColor = System.Drawing.Color.Black;
        errLabel.BorderColor = System.Drawing.Color.Red;
        errLabel.BorderStyle = BorderStyle.Solid;
        errLabel.BorderWidth = Unit.Pixel(1);
        errLabel.Font.Size = FontUnit.Small;
        lblError.ApplyStyle(errLabel);
        //lblIncomplete.ApplyStyle(errLabel);

        lblIncomplete.Font.Size = FontUnit.Point(10);
        lblIncomplete.ForeColor = System.Drawing.Color.Red;

        lblIncompleteGW.Font.Size = FontUnit.Point(10);
        lblIncompleteGW.ForeColor = System.Drawing.Color.Red;

        lblGatewayOnly.Font.Size = FontUnit.Point(10);

        //if (!IsPostBack)
        //{
        if (!User.Identity.IsAuthenticated)
            Response.Redirect("/Application/default.aspx?Authentication=False");
        else
        {
            try
            {
                CreatePDF PDFFile = new CreatePDF(AppId);
                string strProcessor = PDFFile.ReturnProcessorName();

                if (!Locked())
                {
                    lblHeading.CssClass = "MenuHeader";
                    if (Convert.ToInt32(Session["AcctType"]) == 1)
                        lblHeading.Text = "Merchant Account Signup Information";
                    else if (Convert.ToInt32(Session["AcctType"]) == 2)
                        lblHeading.Text = "Payment Gateway Signup Information";
                    else
                        lblHeading.Text = "Merchant Account & Payment Gateway Signup Information";

                    CheckAppStatus();


                    if (!(strProcessor.ToLower().Contains("sage") || strProcessor.ToLower().Contains("ipayment") || strProcessor.ToLower().Contains("cardconnect")))
                    {
                        pnlDigital.Visible = false;
                        pnlDigitalSig.Visible = false;
                        btnDigitalSignature.Enabled = false;
                    }
                    if (strProcessor.ToLower().Contains("sage"))
                    {
                        pnlSagePDF.Visible = true;
                        pnlSageDnCDocuSign.Visible = true;
                    }
                }
                else
                {
                    SetErrorMessage("The status of your application prevents it from being edited");
                    pnlCreatePDF.Visible = true;
                    //pnlMA.Visible = false;
                    pnlDigital.Visible = false;
                    pnlDigitalSig.Visible = false;
                    btnCreatePDF.Enabled = false;
                    btnDigitalSignature.Enabled = false;

                    if (strProcessor.ToLower().Contains("sage"))
                    {
                        pnlSagePDF.Visible = true;
                        pnlSageDnCDocuSign.Visible = true;
                    }
                }
            }//end try
            catch (Exception err)
            {
                CreateOnlineAppLog Log = new CreateOnlineAppLog();
                Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + Session["AppId"].ToString() + " - " + "Notice.aspx Page Load Error - " + err.Message);
                SetErrorMessage(err.Message);
            }
            lblAppId.Text = Convert.ToString(Session["AppId"]);
            lblAppId.Font.Size = FontUnit.Point(10);
            lblAppId.ForeColor = System.Drawing.Color.Red;
        }
        //}//end if postback
    }
    //This function checks app status
    protected void CheckAppStatus()
    {
        NewAppInfo AppInfo = new NewAppInfo(AppId);
        string Status = "";
        if ((Convert.ToInt32(Session["AcctType"]) == 1) || (Convert.ToInt32(Session["AcctType"]) == 4))
        {
            //Check status of app and display appropriate label
            Status = AppInfo.ReturnStatus();
            if ((Status == "") || (Status == "INCOMPLETE") || (Status == "Incomplete"))
            {
                pnlIncomplete.Visible = true;
                //lblComplete.Visible = false;
                //lblCompleteMsg.Visible = false;
                pnlThankyou.Visible = false;

                pnlCreatePDF.Visible = true;
                pnlMA.Visible = true;
                pnlDigital.Visible = false;
                pnlDigitalSig.Visible = false;
                pnlDigSignNote.Visible = false;
            }
            else
            {
                pnlIncomplete.Visible = false;
                //lblComplete.Visible = true;
                //lblCompleteMsg.Visible = true;
                //Display the Create PDF button
                pnlCreatePDF.Visible = true;
                pnlMA.Visible = true;
                CompanyInfo Company = new CompanyInfo(AppId);
                DataSet dsCompanyInfo = Company.GetCompanyInfo();
                if (dsCompanyInfo.Tables[0].Rows.Count > 0)
                {
                    DataRow drCompanyInfo = dsCompanyInfo.Tables["OnlineAppCompanyInfo"].Rows[0];

                    PID = Convert.ToInt32(Session["PID"]);

                    if ((PID == 178) || (PID == 190) || (PID == 182) || (PID == 243) || (PID == 247) || (PID == 196) || (drCompanyInfo["Country"].ToString() != "United States"))
                    {
                        pnlDigital.Visible = false;
                        pnlDigitalSig.Visible = false;
                        pnlDigSignNote.Visible = false;
                    }
                    else
                    {
                        pnlDigital.Visible = true;
                        pnlDigitalSig.Visible = true;
                    }
                }
                //pnlDigital.Visible = true;
                //pnlDigitalSig.Visible = true;
            }
        }

        else if (Convert.ToInt32(Session["AcctType"]) == 2)
        {
            btnCreatePDF.Visible = false;
            imgPDFHelp1.Visible = false;
            pnlPDFNote.Visible = false;
            pnlMA.Visible = false;
            Status = AppInfo.ReturnStatusGW();
            if ((Status == "") || (Status == "INCOMPLETE") || (Status == "Incomplete"))
            {
                pnlIncompleteGW.Visible = true;
                CheckPrevMerchant();
            }
            else
            {
                pnlIncompleteGW.Visible = false;
                pnlGatewayOnly.Visible = true;
                CheckPrevMerchant();
            }
        }

        else if (Convert.ToInt32(Session["AcctType"]) == 3)
        {
            lnkMerchantSignup.Visible = true;
        }

        CreatePDF PDFFile = new CreatePDF(AppId);
        string strProcessor = PDFFile.ReturnProcessorName();
        if (strProcessor.ToLower().Contains("quickbooks"))
        {
            btnCreatePDF.Visible = false;
            imgPDFHelp1.Visible = false;
            pnlPDFNote.Visible = false;
        }
        /* else if (strProcessor.ToLower().Contains("intuit"))
         {
             btnCreatePDF.Visible = false;
             imgPDFHelp1.Visible = false;
             pnlPDFNote.Visible = false;
         }*/

    }//end function check app status

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
            /*if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow drNewApp = ds.Tables[0].Rows[0];
                boolMerchantFunding = Convert.ToBoolean(drNewApp["MerchantFunding"]);
                boolLease = Convert.ToBoolean(drNewApp["Lease"]);
                boolGiftCard = Convert.ToBoolean(drNewApp["GiftCard"]);
                strMCA = Convert.ToString(drNewApp["MCAType"]);
                strLease = Convert.ToString(drNewApp["LeaseCompany"]);
                strGiftCard = Convert.ToString(drNewApp["GiftCardType"]);
            }*/
            //end if count not 0

            //if (boolGiftCard)
            {
                // if (strGiftCard.ToLower().Contains("sage eft"))
                {
                    CreatePDF PDFFile = new CreatePDF(AppId);
                    DataRow drNewApp = ds.Tables[0].Rows[0];

                    string FileName = "";
                    if (Convert.ToString(drNewApp["ContractTerm"]) == "1 month")
                    {
                        FileName = Server.MapPath("/Application/Merchant Amendment - Month to Month.pdf");
                    }
                    else if (Convert.ToString(drNewApp["ContractTerm"]) == "1 year")
                    {
                        FileName = Server.MapPath("/Application/Merchant Amendment - Single Year.pdf");
                    }
                    MemoryStream mStream = PDFFile.CreateSageMonthToMonthPDF(FileName); ;
                    if (mStream != null)
                    {
                        LogBL LogData = new LogBL(AppId);
                        LogData.InsertLogData(AffiliateID, "Amendment");

                        Response.ClearContent();
                        Response.ClearHeaders();
                        Response.ContentType = "application/pdf";
                        Response.AppendHeader("content-disposition", "filename=GETI_Gift_Merchant_App.pdf");
                        Response.OutputStream.Write(mStream.GetBuffer(), 0, Convert.ToInt32(mStream.Length));

                        Response.Flush();
                    }
                    /*else
                        DisplayMessage("Data not found for this record.");*/
                }
            }


        }//end try
        catch (Exception err)
        {
            CreateLog Log = new CreateLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "Create PDF " + AppId + err.Message);
            //DisplayMessage("Create PDF " + err.Message);
        }
    }

    //This function checks whether the customer has a previous merchant account
    protected void CheckPrevMerchant()
    {
        ReprogramInfo Reprogram = new ReprogramInfo(AppId);
        DataSet dsReprogramInfo = Reprogram.GetReprogramInfo();
        string strReprogram = "No";
        if (dsReprogramInfo.Tables[0].Rows.Count > 0)
        {
            DataRow drReprogramInfo = dsReprogramInfo.Tables[0].Rows[0];
            if (Convert.ToBoolean(drReprogramInfo["Reprogram"]) == true)
                strReprogram = "Yes";
        }

        if (strReprogram == "Yes")
            lnkMerchantSignup.Visible = false;
        else
        {
            if (Convert.ToInt32(Session["AcctType"]) != 4)
                lnkMerchantSignup.Visible = true;
        }
    }

    public static void RedirectAndPOST(Page page, string destinationUrl,
                                   NameValueCollection data)
    {
        //Prepare the Posting form
        string strForm = PreparePOSTForm(destinationUrl, data);
        //Add a literal control the specified page holding 
        //the Post Form, this is to submit the Posting form with the request.
        page.Controls.Add(new LiteralControl(strForm));
    }

    private static String PreparePOSTForm(string url, NameValueCollection data)
    {
        //Set a name for the form
        string formID = "PostForm";
        //Build the form using the specified data to be posted.
        StringBuilder strForm = new StringBuilder();
        strForm.Append("<form id=\"" + formID + "\" name=\"" +
                   formID + "\" action=\"" + url +
                   "\" method=\"POST\">");

        foreach (string key in data)
        {
            strForm.Append("<input type=\"hidden\" name=\"" + key +
                           "\" value=\"" + data[key] + "\">");
        }

        strForm.Append("</form>");
        //Build the JavaScript which will do the Posting operation.
        StringBuilder strScript = new StringBuilder();
        strScript.Append("<script language='javascript'>");
        strScript.Append("var v" + formID + " = document." +
                     formID + ";");
        strScript.Append("v" + formID + ".submit();");
        strScript.Append("</script>");
        //Return the form and the script concatenated.
        //(The order is important, Form then JavaScript)
        return strForm.ToString() + strScript.ToString();
    }

    protected void btnDigitalSignature_Click1(object sender, EventArgs e)
    {
        //Response.Redirect("notice1.aspx",false);
        string newStatus = "SUBMITTED FOR REVIEW";
        string accountType = "Merchant";
        NewAppInfo NewOnlineApp = new NewAppInfo(AppId);
        bool updatedStatus = NewOnlineApp.UpdateStatus(newStatus, accountType);
        OnlineAppStatus Locked = new OnlineAppStatus(AppId);
        string strLocked = Locked.GetLocked();
        Session["Locked"] = strLocked;
        Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "SCRIPT", "window.open('notice1.aspx','_blank');window.location = 'notice.aspx';", true);
    }

    protected void btnDigitalSignature_Click(object sender, EventArgs e)
    {
        // Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "SCRIPT", "window.open('notice1.aspx','','');window.location = 'notice.aspx';", true);

        try
        {
            //ArrayList theQueryData;
            if (AppId == 1)
            {
                btnDigitalSignature_ClickNew(sender, e);
                //btnDigitalSignature_Click(sender, e);
            }
            else
            {
                pnlCreatePDF.Visible = false;
                pnlMA.Visible = false;
                pnlDigital.Visible = false;
                pnlDigitalSig.Visible = false;
                btnCreatePDF.Enabled = false;
                btnDigitalSignature.Enabled = false;
                pnlDigSignNote.Visible = false;

                string newStatus = "SUBMITTED FOR REVIEW";
                string accountType = "Merchant";
                NewAppInfo NewOnlineApp = new NewAppInfo(AppId);
                bool updatedStatus = NewOnlineApp.UpdateStatus(newStatus, accountType);
                OnlineAppStatus Locked = new OnlineAppStatus(AppId);
                string strLocked = Locked.GetLocked();
                Session["Locked"] = strLocked;
                CreatePDF PDFFile = new CreatePDF(AppId);
                string strProcessor = PDFFile.ReturnProcessorName();
                string strNewValue = "";
                string strResponse = "";

                //Response.AppendHeader("Refresh", "0;URL=notice.aspx");

                //Response.Redirect("notice.aspx", false);

                #region sage application digital signature

                if (strProcessor.ToLower().Contains("sage"))
                {
                    OAPDFBL SageData = new OAPDFBL(AppId);
                    DataSet ds = SageData.GetSageData();

                    string platForm = "";

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow dr = ds.Tables[0].Rows[0];


                        //string platForm = "";

                        //string strUrl = "https://na2.docusign.net/MEMBER/PowerFormSigning.aspx?PowerFormId=74b2bc6f-8c49-4d1f-ac73-bc41fcd7646a&Signer_UserName=" + dr["P1FirstName"].ToString().Trim() + " " + dr["P1LastName"].ToString().Trim() + "&Signer_Email=" + dr["Email"].ToString().Trim();
                        string strUrl = "https://na2.docusign.net/MEMBER/PowerFormSigning.aspx?PowerFormId=2f74ef0c-0c9c-4954-9558-fbc099af708d&Signer_UserName=" + dr["P1FirstName"].ToString().Trim() + " " + dr["P1LastName"].ToString().Trim() + "&Signer_Email=" + dr["Email"].ToString().Trim();

                        //string strUrl = "https://demo.docusign.net/MEMBER/PowerFormSigning.aspx?PowerFormId=8f949e1d-0a1c-4be3-8d0a-e6b93a0fef44&Signer_UserName=" + dr["P1FirstName"].ToString().Trim() + " " + dr["P1LastName"].ToString().Trim() + "&Signer_Email=" + dr["Email"].ToString().Trim();

                        if (!Convert.IsDBNull(dr["P2FirstName"]))
                        {
                            if (dr["P2FirstName"].ToString().Trim() != "")
                            {
                                //strUrl = "https://na2.docusign.net/MEMBER/PowerFormSigning.aspx?PowerFormId=56a5b324-bdee-4896-87af-8118747f4081&Signer_UserName=" + dr["P1FirstName"].ToString().Trim() + " " + dr["P1LastName"].ToString().Trim() + "&Signer_Email=" + dr["Email"].ToString().Trim() + "&CoSigner_UserName=" + dr["P2FirstName"].ToString().Trim() + " " + dr["P2LastName"].ToString().Trim() + "&CoSigner_Email=" + dr["P2Email"].ToString().Trim();
                                strUrl = "https://na2.docusign.net/MEMBER/PowerFormSigning.aspx?PowerFormId=ddcb92dd-4b3d-4926-82ba-f9dea45fff0b&Signer_UserName=" + dr["P1FirstName"].ToString().Trim() + " " + dr["P1LastName"].ToString().Trim() + "&Signer_Email=" + dr["Email"].ToString().Trim() + "&CoSigner_UserName=" + dr["P2FirstName"].ToString().Trim() + " " + dr["P2LastName"].ToString().Trim() + "&CoSigner_Email=" + dr["P2Email"].ToString().Trim();
                                //strUrl = "https://demo.docusign.net/Member/PowerFormSigning.aspx?PowerFormId=a0899860-f6a0-4aab-a636-d7f1ce3887f2&Signer_UserName=" + dr["P1FirstName"].ToString().Trim() + " " + dr["P1LastName"].ToString().Trim() + "&Signer_Email=" + dr["Email"].ToString().Trim() + "&CoSigner_UserName=" + dr["P2FirstName"].ToString().Trim() + " " + dr["P2LastName"].ToString().Trim() + "&CoSigner_Email=" + dr["P2Email"].ToString().Trim();
                            }
                        }


                        NameValueCollection data = new NameValueCollection();

                        #region general info

                        if (dr["Platform"].ToString().Trim().Contains("Paymentech"))
                            platForm = "Paymentech";
                        else
                            platForm = "Visanet/TSYS";

                        data.Add("Authorization Network", platForm);
                        data.Add("Contractor Name", Convert.ToString(dr["RepName"]).Trim());
                        data.Add("Legal Business Name", dr["COMPANYNAME"].ToString().Trim());
                        data.Add("MA_Address", dr["BillingAddress"].ToString().Trim());
                        data.Add("MA_City", dr["BillingCity"].ToString().Trim());
                        data.Add("MA_State", dr["BillingState"].ToString().Trim());
                        data.Add("MA_Zip", dr["BillingZipCode"].ToString().Trim());
                        data.Add("Contact Name", dr["ContactName"].ToString().Trim());
                        //data.Add("Contact Title", dr["Title"].ToString().Trim());
                        data.Add("Phone", dr["BusinessPhone"].ToString().Trim());
                        data.Add("Email", dr["Email"].ToString().Trim());

                        if ((dr["MIB"].ToString().Trim() != "") && (dr["YIB"].ToString().Trim() != ""))
                        {
                            DateTime BusinessDate = DateTime.Now.AddMonths((Convert.ToInt32(dr["MIB"].ToString().Trim())) * -1);
                            DateTime BusinessOpenDate = BusinessDate.AddYears((Convert.ToInt32(dr["YIB"].ToString().Trim())) * -1);
                            string BusinessOpenMonth = BusinessOpenDate.Month.ToString().Trim();
                            string BusinessOpenYear = BusinessOpenDate.Year.ToString().Trim();
                            data.Add("Business Open Date", BusinessOpenMonth + "/" + BusinessOpenYear);

                            string LengthOfOwnership = "";
                            if (dr["YIB"].ToString().Trim() != "")
                                LengthOfOwnership = dr["YIB"].ToString().Trim() + " Years ";
                            else
                                LengthOfOwnership = "0 Years ";

                            if (dr["MIB"].ToString().Trim() != "")
                                LengthOfOwnership += "and " + dr["MIB"].ToString().Trim() + " Months";

                            data.Add("Length Of Ownership", LengthOfOwnership);
                        }

                        if (dr["PrevProcessor"].ToString().Contains("Sage"))
                            data.Add("Existing Sage MID", dr["PrevMerchantAcctNo"].ToString().Trim());

                        data.Add("Business Name DBA", dr["DBA"].ToString().Trim());
                        data.Add("Address", dr["Address"].ToString().Trim());
                        data.Add("City", dr["CITY"].ToString().Trim());
                        data.Add("State", dr["STATE"].ToString().Trim());
                        data.Add("Zip", dr["ZipCode"].ToString().Trim());
                        data.Add("Phone_2", dr["BusinessPhone"].ToString().Trim());
                        data.Add("Fax_2", dr["Fax"].ToString().Trim());
                        data.Add("Web Site", dr["Website"].ToString().Trim());
                        data.Add("Customer Service Phone", dr["CustServPhone"].ToString().Trim());
                        data.Add("Number of Locations", dr["NumberofLocations"].ToString().Trim());

                        data.Add("Fed Tax ID", dr["FederalTaxID"].ToString().Trim());

                        if (dr["LegalStatus"].ToString().ToLower().Contains("trust"))
                            data.Add("OwnershipTypeAsso", "x");
                        else if (dr["LegalStatus"].ToString().ToLower().Contains("llc"))
                            data.Add("OwnershipTypellc", "x");
                        else if ((dr["LegalStatus"].ToString().ToLower().Contains("non-profit")) || (dr["LegalStatus"].ToString().ToLower().Contains("tax exempt")))
                            data.Add("OwnershipTypeTaxExmpt", "x");
                        else if (dr["LegalStatus"].ToString().ToLower().Contains("corporation"))
                            data.Add("OwnershipTypeCorp", "x");
                        else if (dr["LegalStatus"].ToString().ToLower().Contains("medical"))
                            data.Add("OwnershipTypemedical", "x");
                        else if (dr["LegalStatus"].ToString().ToLower().Contains("government"))
                            data.Add("OwnershipTypeGov", "x");
                        else if (dr["LegalStatus"].ToString().ToLower().Contains("int'l"))
                            data.Add("OwnershipTypeintl", "x");
                        else if (dr["LegalStatus"].ToString().ToLower().Contains("partnership"))
                            data.Add("OwnershipTypepartnership", "x");
                        else if (dr["LegalStatus"].ToString().ToLower().Contains("sole proprietorship"))
                            data.Add("OwnershipTypeSol", "x");
                        #endregion

                        #region principals
                        //Principal #1
                        data.Add("P1 Ownership%", dr["P1OwnershipPercent"].ToString().Trim());
                        data.Add("P1 Full Name", dr["P1FirstName"].ToString().Trim() + " " + dr["P1LastName"].ToString().Trim());
                        data.Add("P1 Title", dr["P1Title"].ToString().Trim());
                        data.Add("P1 Address", dr["P1Address"].ToString().Trim());
                        data.Add("P1 City, State, Zip", dr["P1City"].ToString().Trim() + ", " +
                                        dr["P1State"].ToString().Trim() + " " + dr["P1ZipCode"].ToString().Trim());
                        data.Add("P1 Phone", dr["P1PhoneNumber"].ToString().Trim());
                        data.Add("P1 Email", dr["Email"].ToString().Trim());
                        data.Add("P1 SSN", dr["P1SSN"].ToString().Trim());
                        data.Add("P1 DOB", dr["P1DOB"].ToString().Trim());

                        //Principal #2
                        if (!Convert.IsDBNull(dr["P2FirstName"]))
                        {
                            if (dr["P2FirstName"].ToString().Trim() != "")
                            {
                                data.Add("P2 Ownership%", dr["P2OwnershipPercent"].ToString().Trim());
                                data.Add("P2 Full Name", dr["P2FirstName"].ToString().Trim() + " " + dr["P2LastName"].ToString().Trim());
                                data.Add("P2 Title", dr["P2Title"].ToString().Trim());
                                data.Add("P2 Address", dr["p2Address"].ToString().Trim());
                                data.Add("P2 City, State, Zip", dr["P2City"].ToString().Trim() + ", " +
                                        dr["P2State"].ToString().Trim() + " " + dr["P2ZipCode"].ToString().Trim());
                                data.Add("P2 Phone", dr["p2PhoneNumber"].ToString().Trim());
                                data.Add("P2 Email", dr["P2Email"].ToString().Trim());
                                data.Add("P2 SSN", dr["P2SSN"].ToString().Trim());
                                data.Add("P2 DOB", dr["P2DOB"].ToString().Trim());
                            }
                        }
                        #endregion

                        #region General Underwriting Profile

                        if (dr["BusinessPctMailOrder"].ToString().Trim() == "100")
                            data.Add("BusinessTypeMoto", "x");
                        else if (dr["BusinessPctInternet"].ToString().Trim() == "100")
                            data.Add("BusinessTypeInternet", "x");
                        else if (dr["BusinessPctRetail"].ToString().Trim() == "100")
                            data.Add("BusinessTypeRetail", "x");
                        else if (dr["BusinessPctRestaurant"].ToString().Trim() == "100")
                            data.Add("BusinessTypeRestaurant", "x");

                        data.Add("Products Sold", dr["ProductSold"].ToString().Trim());

                        if (dr["RefundPolicy"].ToString().Trim() == "Refund within 30 days")
                            data.Add("Return Policy", "30 Days Money Back Guarantee");
                        else if (dr["RefundPolicy"].ToString().Trim() == "Exchange Only")
                            data.Add("Return Policy", "30 Days Exchange Only");
                        else if (dr["RefundPolicy"].ToString().Trim() == "No Refund")
                            data.Add("Return Policy", "No Refund");
                        else if (dr["RefundPolicy"].ToString().Trim().Contains("Other"))
                            data.Add("Return Policy", "Other");

                        //data.Add("Days Until Product Delivery", dr["NumDaysDelivered"].ToString().Trim());
                        #endregion

                        #region Credit Card Underwriting Profile
                        data.Add("Monthly Volume", dr["MonthlyVolume"].ToString().Trim());
                        data.Add("Average Ticket", dr["AverageTicket"].ToString().Trim());
                        data.Add("Highest Ticket", dr["MaxTicket"].ToString().Trim());
                        data.Add("Discount Paid Monthly", "x");
                        data.Add("Current Processor", dr["PrevProcessor"].ToString().Trim());

                        data.Add("Card Present Swiped", dr["ProcessPctSwiped"].ToString().Trim());
                        data.Add("Card Present Imprint", dr["ProcessPctKeyedWImprint"].ToString().Trim());
                        data.Add("Card Not Present", dr["ProcessPctKeyedWoImprint"].ToString().Trim());
                        #endregion

                        #region MOTO/Internet Questionnaire
                        data.Add("Days Until Product Delivery", dr["NumDaysDelivered"].ToString().Trim());
                        #endregion

                        #region ACH Bank
                        //Baking
                        data.Add("ACH Bank Name", dr["BankName"].ToString().Trim());
                        data.Add("ACH Address 1", dr["BankAddress"].ToString().Trim());
                        data.Add("ACH City", dr["BankCity"].ToString().Trim());
                        data.Add("ACH State", dr["BankState"].ToString().Trim());
                        data.Add("ACH Zip", dr["BankZip"].ToString().Trim());
                        //data.Add("ACH Phone", dr["BankPhone"].ToString().Trim());
                        data.Add("ACH Routing Number", dr["BankRoutingNumber"].ToString().Trim());
                        data.Add("ACH Account Number", dr["BankAccountNumber"].ToString().Trim());
                        #endregion

                        #region Rates
                        data.Add("Visa Rate1", dr["DiscountRate"].ToString().Trim());
                        data.Add("MasterCard Rate1", dr["DiscountRate"].ToString().Trim());
                        data.Add("Disc Rate1", dr["DiscountRate"].ToString().Trim());

                        if (dr["DebitStatus"].ToString().ToLower().Contains("yes"))
                        {
                            data.Add("PinDebitY", "Yes");
                            data.Add("DPTY", "x");
                            data.Add("PinDebit Rate1", dr["DiscountRate"].ToString().Trim());
                        }
                        else
                        {
                            data.Add("PinDebitN", "No");
                        }
                        /*
                        if (dr["DebitStatus"].ToString().ToLower().Contains("yes"))
                        {
                            data.Add("PinDebit", "x");
                            data.Add("DPT", "x");
                        }
                        else
                            data.Add("PinDebit", "No");*/

                        data.Add("Visa Rate2", dr["DiscRateMidQual"].ToString().Trim());
                        data.Add("MasterCard Rate2", dr["DiscRateMidQual"].ToString().Trim());
                        data.Add("Disc Rate2", dr["DiscRateMidQual"].ToString().Trim());

                        data.Add("Visa Rate3", dr["DiscRateNonQual"].ToString().Trim());
                        data.Add("MasterCard Rate3", dr["DiscRateNonQual"].ToString().Trim());
                        data.Add("Disc Rate3", dr["DiscRateNonQual"].ToString().Trim());

                        data.Add("Visa INTL/NS1", "1.85");
                        data.Add("MC INTL/NS1", "1.65");
                        data.Add("Visa CheckCard INTL/NS1", "1.85");
                        data.Add("MC CheckCard INTL/NS1", "1.65");
                        data.Add("Disc INTL/NS1", "1.65");





                        data.Add("Visa INTL/NS Surcharge", "0.20");
                        data.Add("MC INTL/NS Surcharge", "0.20");
                        data.Add("Visa CheckCard INTL/NS Surcharge", "0.20");
                        data.Add("MC CheckCard INTL/NS Surcharge", "0.20");
                        data.Add("Disc INTL/NS Surcharge", "0.20");

                        data.Add("Disc Business", dr["DiscRateNonQual"].ToString().Trim());
                        data.Add("Visa Business", dr["DiscRateNonQual"].ToString().Trim());
                        data.Add("MC Business", dr["DiscRateNonQual"].ToString().Trim());

                        if (dr["BillingMethod"].ToString() == "Interchange")
                        {
                            data.Add("DiscIPT", "x");
                            data.Add("VisaIPT", "x");
                            data.Add("MCIPT", "x");



                            data.Add("Visa INTL/NS1", "0.00");
                            data.Add("MC INTL/NS1", "0.00");
                            data.Add("Visa CheckCard INTL/NS1", "0.00");
                            data.Add("MC CheckCard INTL/NS1", "0.00");
                            data.Add("Disc INTL/NS1", "0.00");


                            data.Add("Visa INTL/NS Surcharge", "0.20");
                            data.Add("MC INTL/NS Surcharge", "0.20");
                            data.Add("Visa CheckCard INTL/NS Surcharge", "0.20");
                            data.Add("MC CheckCard INTL/NS Surcharge", "0.20");
                            data.Add("Disc INTL/NS Surcharge", "0.20");

                        }


                        //American Express
                        if (dr["AmexApplied"].ToString().Trim() == "0")
                            data.Add("American Express Other", "None");
                        else if ((dr["AmexApplied"].ToString().Trim() == "1") && (dr["PrevAmexNum"].ToString().Trim() == ""))
                            data.Add("American Express Other", "New");
                        else if ((dr["AmexApplied"].ToString().Trim() == "1") && (dr["PrevAmexNum"].ToString().Trim() != ""))
                        {
                            data.Add("American Express Other", "Existing");
                            data.Add("American Express Existing #", dr["PrevAmexNum"].ToString().Trim());
                        }

                        //Discover
                        /*if (dr["DiscoverApplied"].ToString().Trim() == "0")
                            data.Add("Discover Other", "None");
                        else if ((dr["DiscoverApplied"].ToString().Trim() == "1") && (dr["PrevDiscoverNum"].ToString().Trim() == ""))
                            data.Add("Discover Other", "New");
                        else if ((dr["DiscoverApplied"].ToString().Trim() == "1") && (dr["PrevDiscoverNum"].ToString().Trim() != ""))
                        {
                            data.Add("Discover Other", "Existing");
                            data.Add("Discover Existing #", dr["PrevDiscoverNum"].ToString().Trim());
                        }

                        //JCB
                        if (dr["PrevJCBNum"].ToString().Trim() == "Opted Out")
                            data.Add("JCB Other", "None");
                        else if ((dr["PrevJCBNum"].ToString().Trim() == "Submitted") || (dr["PrevJCBNum"].ToString().Trim() == "Yes"))
                            data.Add("JCB Other", "New");
                        else if (dr["PrevJCBNum"].ToString().Trim() != "")
                        {
                            data.Add("JCB Other", "Existing");
                            data.Add("JCB Existing #", dr["PrevJCBNum"].ToString().Trim());
                        }*/

                        data.Add("ARU Auth Fee", dr["VoiceAuth"].ToString().Trim());
                        data.Add("EBT Auth Fee", dr["EBTTransFee"].ToString().Trim());
                        data.Add("PIN Debit Auth Fee", dr["DebitTransFee"].ToString().Trim());
                        data.Add("Voice Authorization", dr["VoiceAuth"].ToString().Trim());

                        data.Add("Application Credit", dr["AppFee"].ToString().Trim());
                        if ((dr["WirelessAccessFee"].ToString() != "") || (dr["WirelessTransFee"].ToString() != ""))
                            data.Add("Wireless Set Up1", "35.00");

                        if (dr["Gateway"].ToString().ToLower().Contains("roampay"))
                        {
                            data.Add("Sage Mobile Payments Setup1", "25.00");
                            data.Add("Sage Mobile Payments Access", dr["GatewayMonFee"].ToString().Trim());
                            decimal TransFee = Convert.ToDecimal(dr["TransactionFee"].ToString().Trim()) + Convert.ToDecimal(dr["GatewayTransFee"].ToString().Trim());
                            string strTransFee = TransFee.ToString();
                            data.Add("Visa MC Auth Fee", strTransFee);
                            decimal NBCTransFee = Convert.ToDecimal(dr["NBCTransFee"].ToString().Trim()) + Convert.ToDecimal(dr["GatewayTransFee"].ToString().Trim());
                            string strNBCTransFee = NBCTransFee.ToString();
                            data.Add("Discover Auth Fee", strNBCTransFee);
                            data.Add("American Express Auth Fee", strNBCTransFee);
                            data.Add("Carte Blanche Auth Fee", strNBCTransFee);
                            data.Add("Diners Club Auth Fee", strNBCTransFee);
                            data.Add("JCB Auth Fee", strNBCTransFee);
                        }
                        else if (dr["Gateway"].ToString().ToLower().Contains("sage gateway"))
                        {
                            data.Add("Sage Mobile Payments Setup1", "25.00");
                            data.Add("Sage Mobile Payments Access", dr["GatewayMonFee"].ToString().Trim());
                            decimal TransFee = Convert.ToDecimal(dr["TransactionFee"].ToString().Trim()) + Convert.ToDecimal(dr["GatewayTransFee"].ToString().Trim());
                            string strTransFee = TransFee.ToString();
                            data.Add("Visa MC Auth Fee", strTransFee);
                            decimal NBCTransFee = Convert.ToDecimal(dr["NBCTransFee"].ToString().Trim()) + Convert.ToDecimal(dr["GatewayTransFee"].ToString().Trim());
                            string strNBCTransFee = NBCTransFee.ToString();
                            data.Add("Discover Auth Fee", strNBCTransFee);
                            data.Add("American Express Auth Fee", strNBCTransFee);
                            data.Add("Carte Blanche Auth Fee", strNBCTransFee);
                            data.Add("Diners Club Auth Fee", strNBCTransFee);
                            data.Add("JCB Auth Fee", strNBCTransFee);
                        }
                        else
                        {
                            data.Add("Visa MC Auth Fee", dr["TransactionFee"].ToString().Trim());
                            data.Add("Discover Auth Fee", dr["NBCTransFee"].ToString().Trim());
                            data.Add("American Express Auth Fee", dr["NBCTransFee"].ToString().Trim());
                            data.Add("Carte Blanche Auth Fee", dr["NBCTransFee"].ToString().Trim());
                            data.Add("Diners Club Auth Fee", dr["NBCTransFee"].ToString().Trim());
                            data.Add("JCB Auth Fee", dr["NBCTransFee"].ToString().Trim());
                        }

                        data.Add("Statement", "0.00");
                        data.Add("Monthly Support", dr["CustServFee"].ToString().Trim());
                        data.Add("Monthly Minimum", dr["MonMin"].ToString().Trim());
                        if (dr["Gateway"].ToString().Contains("Sage") || dr["Gateway"].ToString().Contains("Roam"))
                            data.Add("Gateway Access", dr["GatewayMonFee"].ToString().Trim());
                        data.Add("Debit Access", dr["DebitMonFee"].ToString().Trim());
                        data.Add("Wireless Access", dr["WirelessAccessFee"].ToString().Trim());

                        if (dr["AnnualFeeCP"].ToString().Trim() != "")
                            data.Add("Annual Assessment", dr["AnnualFeeCP"].ToString().Trim());
                        else
                            data.Add("Annual Assessment", dr["AnnualFeeCNP"].ToString().Trim());
                        data.Add("Chargeback", dr["ChargebackFee"].ToString().Trim());
                        data.Add("Signature Rate", dr["DiscRateQualDebit"].ToString().Trim());
                        if (dr["Gateway"].ToString().Trim() != "")
                            data.Add("Terminal/Software Type", dr["Gateway"].ToString().Trim());
                        else
                            data.Add("Terminal/Software Type", dr["Equipment"].ToString().Trim());

                        #endregion

                        RedirectAndPOST(this.Page, strUrl, data);

                        LogBL LogData = new LogBL(AppId);
                        LogData.InsertLogData(AffiliateID, "Sage DocuSign document created.");

                    }

                }
                #endregion

                #region iPayment application digital signature

                else if (strProcessor.ToLower().Contains("ipayment"))
                {
                    //Get data for IPayment Application from ACT! based on AppId
                    OAPDFBL IPayData = new OAPDFBL(AppId);
                    DataSet ds = IPayData.GetIPaymentData();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow dr = ds.Tables[0].Rows[0];


                        //string platForm = "";

                        string strUrl = "https://na2.docusign.net/MEMBER/PowerFormSigning.aspx?PowerFormId=d89486c5-2a99-4eda-9691-dc959eef5714&Signer_UserName=" + dr["P1FirstName"].ToString().Trim() + " " + dr["P1LastName"].ToString().Trim() + "&Signer_Email=" + dr["Email"].ToString().Trim();

                        //string strUrl = "https://demo.docusign.net/Member/PowerFormSigning.aspx?PowerFormId=8cfd83c9-82aa-4231-b17c-5243f4bc501c&Signer_UserName=" + dr["P1FirstName"].ToString().Trim() + " " + dr["P1LastName"].ToString().Trim() + "&Signer_Email=" + dr["Email"].ToString().Trim();

                        if (!Convert.IsDBNull(dr["P2FirstName"]))
                        {
                            if (dr["P2FirstName"].ToString().Trim() != "")
                            {
                                strUrl = "https://na2.docusign.net/MEMBER/PowerFormSigning.aspx?PowerFormId=9bcc56f0-614d-48ca-aa0f-edbbe3285592&Signer_UserName=" + dr["P1FirstName"].ToString().Trim() + " " + dr["P1LastName"].ToString().Trim() + "&Signer_Email=" + dr["Email"].ToString().Trim() + "&CoSigner_UserName=" + dr["P2FirstName"].ToString().Trim() + " " + dr["P2LastName"].ToString().Trim() + "&CoSigner_Email=" + dr["P2Email"].ToString().Trim();
                                //strUrl = "https://demo.docusign.net/Member/PowerFormSigning.aspx?PowerFormId=1867a760-7a9b-4f78-b71a-703378e638fa&Signer_UserName=" + dr["P1FirstName"].ToString().Trim() + " " + dr["P1LastName"].ToString().Trim() + "&Signer_Email=" + dr["Email"].ToString().Trim() + "&CoSigner_UserName=" + dr["P2FirstName"].ToString().Trim() + " " + dr["P2LastName"].ToString().Trim() + "&CoSigner_Email=" + dr["P2Email"].ToString().Trim();
                            }
                        }

                        NameValueCollection data = new NameValueCollection();

                        #region General Information
                        data.Add("app.RepName", dr["RepName"].ToString().Trim());
                        data.Add("app.LegalName", dr["CompanyName"].ToString().Trim());
                        data.Add("app.DBA", dr["DBA"].ToString().Trim());
                        data.Add("app.ApplicantDBA", dr["DBA"].ToString().Trim());
                        data.Add("app.EMail", dr["Email"].ToString().Trim());
                        data.Add("app.ContactName", dr["ContactName"].ToString().Trim());
                        data.Add("app.Website", dr["Website"].ToString().Trim());
                        data.Add("app.MailingAddress", dr["BillingAddress"].ToString().Trim());
                        data.Add("app.MCityState", dr["BillingCity"].ToString().Trim() + ", " + dr["BillingState"].ToString().Trim() + ", " + dr["BillingZipCode"].ToString().Trim());
                        data.Add("app.BusinessAddress", dr["Address"].ToString().Trim());
                        data.Add("app.CityState", dr["City"].ToString().Trim() + ", " + dr["State"].ToString().Trim() + ", " + dr["ZipCode"].ToString().Trim());
                        data.Add("app.Region", dr["Country"].ToString().Trim());
                        data.Add("app.HowLong", dr["TABL"].ToString().Trim());
                        data.Add("app.TIBYears", dr["YIB"].ToString().Trim());
                        data.Add("app.TIBMonths", dr["MIB"].ToString().Trim());
                        data.Add("app.Fax", dr["Fax"].ToString().Trim());
                        data.Add("app.BusinessPhone", dr["BusinessPhone"].ToString().Trim());
                        data.Add("app.CustServPhone", dr["CustServPhone"].ToString().Trim());
                        data.Add("app.BusinessHours", dr["BusinessHours"].ToString().Trim());

                        data.Add("app.ProductsSold", dr["ProductSold"].ToString().Trim());
                        data.Add("app.PrevProcessor", dr["PrevProcessor"].ToString().Trim());
                        data.Add("app.PrevMerchantNum", dr["PrevMerchantAcctNo"].ToString().Trim());
                        data.Add("app.RepNum", dr["RepNum"].ToString().Trim());
                        data.Add("app.RepPhone", dr["RepPhone"].ToString().Trim());
                        //data.Add("app.OtherRefund", dr["OtherRefund"].ToString().Trim());
                        data.Add("app.AddlComments", dr["AddlComments"].ToString().Trim());
                        data.Add("app.NumLocs", dr["NumberofLocations"].ToString().Trim());
                        data.Add("app.BusinessPhoneExt", dr["BusinessPhoneExt"].ToString().Trim());
                        data.Add("app.NumDaysDel", dr["NumDaysDelivered"].ToString().Trim());

                        if ((dr["FederalTaxID"].ToString().Trim() != "") && (dr["P1SSN"].ToString().Trim() != ""))
                        {
                            if ((dr["FederalTaxID"].ToString().Trim() == null) || (dr["FederalTaxID"].ToString().Trim() == dr["P1SSN"].ToString().Trim()))
                            {
                                data.Add("app.SSNCheckbox", "x");
                                data.Add("app.SSNorTaxID2", dr["P1SSN"].ToString().Trim());
                            }
                            else
                            {
                                data.Add("app.EINCheckbox", "x");
                                data.Add("app.SSNorTaxID", dr["FederalTaxID"].ToString().Trim());
                            }
                        }
                        if (dr["CTMF"].ToString().Trim() == "Yes")
                        {
                            data.Add("app.chkCTMFYes", "x");

                        }
                        else
                        {
                            data.Add("app.chkCTMFNo", "x");
                        }

                        if (dr["PrevProcessed"].ToString().Trim() == "Yes")
                        {
                            data.Add("app.chkPrevProcessedYes", "x");
                        }
                        else
                        {
                            data.Add("app.chkPrevProcessedNo", "x");
                        }

                        //if (dr["Reprogram"].ToString().Trim() == "Yes")
                        //data.Add("app.chkReprogram", "Yes");

                        if ((dr["RefundPolicy"].ToString().Trim() == "Refund within 30 days") || (dr["RefundPolicy"].ToString().Trim() == "Refund Within 30 Days"))
                            data.Add("app.chkRefund30Days", "x");
                        else if (dr["RefundPolicy"].ToString().Trim() == "Exchange Only")
                            data.Add("app.chkExchangeOnly", "x");
                        else if (dr["RefundPolicy"].ToString().Trim() == "No Refund")
                        {
                            data.Add("app.chkRefundOther", "x");
                            data.Add("app.OtherRefund", "No Refund");
                        }
                        else if (dr["RefundPolicy"].ToString().Trim().Contains("Other"))
                        {
                            data.Add("app.chkRefundOther", "x");
                            data.Add("app.OtherRefund", dr["OtherRefund"].ToString().Trim());
                        }

                        if (dr["LegalStatus"].ToString().Trim() == "Sole Proprietorship")
                            data.Add("app.chkSole", "x");
                        if (dr["LegalStatus"].ToString().Trim() == "Corporation")
                            data.Add("app.chkCorp", "x");
                        if (dr["LegalStatus"].ToString().Trim() == "Partnership")
                            data.Add("app.chkPartnership", "x");
                        if (dr["LegalStatus"].ToString().Trim() == "Non-Profit")
                            data.Add("app.chkNonProfit", "x");
                        if (dr["LegalStatus"].ToString().Trim() == "Legal/Medical Corp.")
                            data.Add("app.chkLegaMedical", "x");
                        if (dr["LegalStatus"].ToString().Trim() == "Government")
                            data.Add("app.chkGovt", "x");
                        if (dr["LegalStatus"].ToString().Trim() == "Tax Exempt")
                            data.Add("app.chkTaxExempt", "x");
                        if (dr["LegalStatus"].ToString().Trim() == "Others")
                            data.Add("app.chkOwnershipOther", "x");
                        if (dr["LegalStatus"].ToString().Trim() == "LLC")
                            data.Add("app.chkLLC", "x");

                        if (dr["Equipment"].ToString().Trim() != "")
                        {
                            string equipment = dr["Equipment"].ToString().Trim();
                            data.Add("app.EquipModel", equipment);
                            if (equipment.Contains("Nurit"))
                                data.Add("app.chkNurit", "x");
                            else if (equipment.Contains("Verifone"))
                                data.Add("app.chkVerifone", "x");
                            else if (equipment.Contains("Hypercom"))
                                data.Add("app.chkHypercom", "x");
                            else
                                data.Add("app.chkOther", "x");
                        }
                        #endregion

                        #region CardPCT
                        data.Add("app.Swiped", dr["ProcessPctSwiped"].ToString().Trim());
                        data.Add("app.Keyed", dr["ProcessPctKeyed"].ToString().Trim());
                        data.Add("app.MailOrder", dr["BusinessPctMailOrder"].ToString().Trim());
                        data.Add("app.Internet", dr["BusinessPctInternet"].ToString().Trim());
                        #endregion

                        #region Principal #1
                        //Principal #1
                        data.Add("app.P1ZipCode", dr["P1ZipCode"].ToString().Trim());
                        data.Add("app.P1State", dr["P1State"].ToString().Trim());
                        data.Add("app.P1City", dr["P1City"].ToString().Trim());
                        data.Add("app.P1Address", dr["P1Address"].ToString().Trim());
                        data.Add("app.P1Title", dr["P1Title"].ToString().Trim());
                        data.Add("app.P1SSN", dr["P1SSN"].ToString().Trim());
                        data.Add("app.P1Name", dr["P1FirstName"].ToString().Trim() + " " + dr["P1LastName"].ToString().Trim());
                        data.Add("app.P1Ownership", dr["P1OwnershipPercent"].ToString().Trim());
                        data.Add("app.P1DOB", dr["P1DOB"].ToString().Trim());
                        data.Add("app.P1DState", dr["P1DriversLicenseState"].ToString().Trim());
                        data.Add("app.P1DriversLicense", dr["P1DriversLicenseNo"].ToString().Trim());
                        data.Add("app.P1HomePhone", dr["P1PhoneNumber"].ToString().Trim());
                        data.Add("app.P1TimeAtAddress", dr["P1TimeAtAddress"].ToString().Trim());
                        if (dr["P1LivingStatus"].ToString().Trim() == "Rent")
                            data.Add("app.chkP1Rent", "x");
                        if (dr["P1LivingStatus"].ToString().Trim() == "Own")
                            data.Add("app.chkP1Own", "x");
                        #endregion

                        #region Principal #2
                        //Principal #2
                        data.Add("app.P2ZipCode", dr["P2ZipCode"].ToString().Trim());
                        data.Add("app.P2State", dr["P2State"].ToString().Trim());
                        data.Add("app.P2City", dr["P2City"].ToString().Trim());
                        data.Add("app.P2Address", dr["P2Address"].ToString().Trim());
                        data.Add("app.P2Title", dr["P2Title"].ToString().Trim());
                        data.Add("app.P2SSN", dr["P2SSN"].ToString().Trim());
                        data.Add("app.P2Name", dr["P2FirstName"].ToString().Trim() + " " + dr["P2LastName"].ToString().Trim());
                        data.Add("app.P2Ownership", dr["P2OwnershipPercent"].ToString().Trim());
                        data.Add("app.P2DOB", dr["P2DOB"].ToString().Trim());
                        data.Add("app.P2DState", dr["P2DriversLicenseState"].ToString().Trim());
                        data.Add("app.P2DriversLicense", dr["P2DriversLicenseNo"].ToString().Trim());
                        data.Add("app.P2HomePhone", dr["P2PhoneNumber"].ToString().Trim());
                        data.Add("app.P2TimeAtAddress", dr["P2TimeAtAddress"].ToString().Trim());
                        if (dr["P2LivingStatus"].ToString().Trim() == "Rent")
                            data.Add("app.chkP2Rent", "x");
                        if (dr["P2LivingStatus"].ToString().Trim() == "Own")
                            data.Add("app.chkP2Own", "x");
                        #endregion

                        #region Rates
                        //Rates
                        data.Add("app.AvgTicket", dr["AverageTicket"].ToString().Trim());
                        data.Add("app.MonthlySalesProcessingLimit", dr["MonthlyVolume"].ToString().Trim());

                        data.Add("app.QualifiedFee", dr["DiscountRate"].ToString().Trim());
                        data.Add("app.DebitQualifiedFee", dr["DiscRateQualDebit"].ToString().Trim());
                        if ((dr["DiscountRate"].ToString().Trim() != "") && (dr["DiscRateMidQual"].ToString().Trim() != ""))
                            data.Add("app.MidQualifiedFee", Convert.ToString(Convert.ToDecimal(dr["DiscRateMidQual"]) - Convert.ToDecimal(dr["DiscountRate"])));
                        if ((dr["DiscountRate"].ToString().Trim() != "") && (dr["DiscRateNonQual"].ToString().Trim() != ""))
                            data.Add("app.NonQualifiedFee", Convert.ToString(Convert.ToDecimal(dr["DiscRateNonQual"]) - Convert.ToDecimal(dr["DiscountRate"])));

                        data.Add("app.TransactionFee", dr["TransactionFee"].ToString().Trim());

                        if (dr["AnnualFeeCP"].ToString().Trim() != "")
                            data.Add("app.AnnualFee", dr["AnnualFeeCP"].ToString().Trim());
                        else
                            data.Add("app.AnnualFee", dr["AnnualFeeCNP"].ToString().Trim());
                        data.Add("app.CustServFee", dr["CustServFee"].ToString().Trim());
                        data.Add("app.MonthlyMinDiscFee", dr["MonMin"].ToString().Trim());
                        data.Add("app.RetrievalRequest", dr["RetrievalFee"].ToString().Trim());
                        data.Add("app.ChargeBacks", dr["ChargebackFee"].ToString().Trim());
                        data.Add("app.ApplicationFee", dr["AppFee"].ToString().Trim());
                        data.Add("app.SetupFee", dr["AppSetupFee"].ToString().Trim());
                        data.Add("app.AVS", dr["AVS"].ToString().Trim());
                        data.Add("app.BatchHeader", dr["BatchHeader"].ToString().Trim());
                        data.Add("app.VoiceAuth", dr["VoiceAuth"].ToString().Trim());

                        if ((dr["WirelessAccessFee"].ToString().Trim() != "") && (dr["WirelessTransFee"].ToString().Trim() != ""))
                        {
                            data.Add("app.WirelessMonthlyGatewayFee", dr["WirelessAccessFee"].ToString().Trim());
                            data.Add("app.WirelessPerAuthFee", dr["WirelessTransFee"].ToString().Trim());
                            data.Add("app.WirelessSetupfee", "35.00");
                            data.Add("app.WirelessSetupQuantity", "1");
                            data.Add("app.WirelessMonthlyAccessQuantity", "1");
                        }

                        data.Add("app.NBCTransactionFee", dr["NBCTransFee"].ToString().Trim());
                        data.Add("app.MCC", dr["MCCCategoryCode"].ToString().Trim());

                        if ((dr["DebitMonFee"].ToString().Trim() != "") && (dr["DebitTransFee"].ToString().Trim() != ""))
                        {
                            data.Add("app.DebitCardAccessFee", dr["DebitMonFee"].ToString().Trim());
                            data.Add("app.Debit", dr["DebitTransFee"].ToString().Trim());
                            data.Add("app.chkDebitCard", "x");
                        }

                        if (dr["Gateway"].ToString().Trim() != "")
                        {
                            data.Add("app.GatewayCheckbox", "x");
                            data.Add("app.Gateway", dr["Gateway"].ToString().Trim());
                        }
                        data.Add("app.GatewayMonthlyAccess", dr["GatewayMonFee"].ToString().Trim());
                        data.Add("app.GatewayTransationFee", dr["GatewayTransFee"].ToString().Trim());
                        data.Add("app.GatewaySetupFee", dr["GatewaySetupFee"].ToString().Trim());

                        if (dr["InternetStmt"].ToString().Trim() == "14.95")
                            data.Add("app.iAccessSingle", "x");
                        else if (dr["InternetStmt"].ToString().Trim() == "30.00")
                            data.Add("app.iAccessChain", "x");

                        if (dr["Interchange"].ToString().Trim() == "True")
                        {
                            data.Add("app.InterchangePlus", dr["DiscRateQualDebit"].ToString().Trim());
                        }

                        #endregion

                        #region Banking
                        //Baking
                        data.Add("app.DiscoverNum", dr["PrevDiscoverNum"].ToString().Trim());
                        if ((dr["AmexNum"].ToString().Trim() != "") && (dr["AmexNum"].ToString().Trim() != "No"))
                        {
                            if (dr["AmexNum"].ToString().Trim() == "Yes" || Convert.ToDouble(dr["ProcessPctSwiped"].ToString().Trim()) >= 70)
                            {
                                data.Add("CheckBox230", "x"); //Check AmEx OnePoint
                                data.Add("app.DiscRateCheckBox", "x");
                                if (Convert.ToDouble(dr["ProcessPctSwiped"].ToString().Trim()) >= 60)
                                {
                                    data.Add("app.AmexDiscountRate", "2.89");
                                }
                                else
                                {
                                    data.Add("app.AmexDiscountRate", "3.50");
                                }
                            }
                            // Existing Amex number
                            {
                                data.Add("app.AmexNum", dr["PrevAmexNum"].ToString());
                                data.Add("Check Box229", "x"); //Check AmEx Direct
                                data.Add("app.MonthlyFFCheckBox", "x");
                            }

                            /*if (Convert.ToDouble(dr["ProcessPctSwiped"].ToString().Trim()) >= 70)
                                data.Add("app.DiscRateCheckBox", "Yes");
                            else
                                data.Add("app.MonthlyFFCheckBox", "Yes");*/
                        }

                        data.Add("app.JCBNum", dr["PrevJCBNum"].ToString().Trim()); data.Add("app.BankName", dr["BankName"].ToString().Trim());
                        data.Add("app.BankAddress", dr["BankAddress"].ToString().Trim());
                        data.Add("app.BankCity", dr["BankCity"].ToString().Trim());
                        data.Add("app.BankState", dr["BankState"].ToString().Trim());
                        data.Add("app.BankZip", dr["BankZip"].ToString().Trim());
                        data.Add("app.BankPhone", dr["BankPhone"].ToString().Trim());
                        data.Add("app.RoutingNum", dr["BankRoutingNumber"].ToString().Trim());
                        data.Add("app.AcctNum", dr["BankAccountNumber"].ToString().Trim());
                        //data.Add("app.BankContactName", dr["NameOnCheckingAcct"].ToString().Trim());
                        #endregion

                        #region Platform
                        if (dr["Platform"].ToString().Trim().Contains("Omaha"))
                            data.Add("app.chkOmaha", "x");
                        else if (dr["Platform"].ToString().Contains("Nashville"))
                            data.Add("app.chkNashville", "x");
                        else if (dr["Platform"].ToString().Contains("Buypass"))
                            data.Add("app.chkBuypass", "x");
                        else if ((dr["Platform"].ToString() != "") && (dr["Platform"].ToString().ToLower().Trim() != "none"))
                        {
                            data.Add("app.chkFrontEndOther", "x");
                            data.Add("app.OtherPlatform", dr["Platform"].ToString().Trim());
                        }
                        #endregion

                        RedirectAndPOST(this.Page, strUrl, data);

                        LogBL LogData = new LogBL(AppId);
                        LogData.InsertLogData(AffiliateID, "iPayment DocuSign document created.");

                    }
                }
                else
                {
                    //SetErrorMessage("Digital Signature works with Sage or iPayment application only.");
                }
                #endregion
            }
        }
        catch (Exception err)
        {
            CreateOnlineAppLog Log = new CreateOnlineAppLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + AppId.ToString() + " - " + "main.master.cs Create PDF Error - " + err.Message);
            //SetErrorMessage("Error Creating PDF. Please contact Technical Support: <a href=mailto:support@commercetech.com?Subject=Online App Error, App ID: " + Session["AppId"].ToString() + "> support@commercetech.com. </a> for assistance.");
        }
    }

    protected void btnLeaseDigitalSignature_Click(object sender, EventArgs e)
    {
        // Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "SCRIPT", "window.open('notice1.aspx','','');window.location = 'notice.aspx';", true);

        try
        {
            //ArrayList theQueryData;

            pnlCreatePDF.Visible = false;
            pnlMA.Visible = false;
            pnlDigital.Visible = false;
            pnlDigitalSig.Visible = false;
            btnCreatePDF.Enabled = false;
            btnDigitalSignature.Enabled = false;
            pnlDigSignNote.Visible = false;

            string newStatus = "SUBMITTED FOR REVIEW";
            string accountType = "Merchant";
            NewAppInfo NewOnlineApp = new NewAppInfo(AppId);
            bool updatedStatus = NewOnlineApp.UpdateStatus(newStatus, accountType);
            OnlineAppStatus Locked = new OnlineAppStatus(AppId);
            string strLocked = Locked.GetLocked();
            Session["Locked"] = strLocked;
            CreatePDF PDFFile = new CreatePDF(AppId);
            string strLeaseCompany = PDFFile.ReturnLeaseCompany();
            string strBusinessState = PDFFile.ReturnBusinessState();
            //string strProcessor = PDFFile.ReturnProcessorName();
            string strNewValue = "";
            string strResponse = "";

            //Response.AppendHeader("Refresh", "0;URL=notice.aspx");

            //Response.Redirect("notice.aspx", false);

            #region Northern Lease application digital signature

            //if (strProcessor.ToLower().Contains("sage"))
            //{
            OAPDFBL NortherLeaseData = new OAPDFBL(AppId);
            DataSet ds = NortherLeaseData.GetNorthernLeaseData();

            string platForm = "";

            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];


                //string platForm = "";

                //string strUrl = "https://na2.docusign.net/MEMBER/PowerFormSigning.aspx?PowerFormId=2f74ef0c-0c9c-4954-9558-fbc099af708d&Signer_UserName=" + dr["P1FirstName"].ToString().Trim() + " " + dr["P1LastName"].ToString().Trim() + "&Signer_Email=" + dr["Email"].ToString().Trim();

                string strUrl = "https://demo.docusign.net/MEMBER/PowerFormSigning.aspx?PowerFormId=3c552f78-8bc1-4c10-a254-ad25e3edb7d4&Signer_UserName=" + dr["FULLNAME"].ToString().Trim() + "&Signer_Email=" + dr["Email"].ToString().Trim();

                if ((strBusinessState.ToLower().Contains("sd")) || (strBusinessState.ToLower().Contains("ks")) || (strBusinessState.ToLower().Contains("tn"))
               || (strBusinessState.ToLower().Contains("pa")) || (strBusinessState.ToLower().Contains("vt")))
                {
                    strUrl = "https://demo.docusign.net/MEMBER/PowerFormSigning.aspx?PowerFormId=699e2c3c-30e9-4545-ba10-cb707802017b&Signer_UserName=" + dr["FULLNAME"].ToString().Trim() + "&Signer_Email=" + dr["Email"].ToString().Trim();
                }


                NameValueCollection data = new NameValueCollection();

                #region About Your Business
                //data.Add("Contractor Name", Convert.ToString(dr["RepName"]).Trim());
                data.Add("CompanyName", dr["CompanyName"].ToString().Trim());
                data.Add("DBA", dr["DBA"].ToString().Trim());
                data.Add("BillingAddress", dr["MailingAddress"].ToString().Trim());
                data.Add("City", dr["MACity"].ToString().Trim());
                data.Add("State", dr["MAState"].ToString().Trim());
                data.Add("Zip", dr["MAZip"].ToString().Trim());
                data.Add("BusinessAreaCode", dr["AreaCode"].ToString().Trim());
                data.Add("BusinessPhone", dr["Phone"].ToString().Trim());
                string Phone = "(" + dr["AreaCode"].ToString().Trim() + ") " + dr["Phone"].ToString().Trim();
                data.Add("Phone", Phone);
                data.Add("TypeOfBusiness", dr["LegalStatus"].ToString().Trim());
                if (dr["LegalStatus"].ToString().ToLower().Trim().Contains("corporation"))
                    data.Add("Corporation", "Yes");
                else if (dr["LegalStatus"].ToString().ToLower().Trim().Contains("proprietorship"))
                    data.Add("Proprietorship", "Yes");
                else if (dr["LegalStatus"].ToString().ToLower().Trim().Contains("partnership"))
                    data.Add("Partnership", "Yes");
                data.Add("Email", dr["Email"].ToString().Trim());
                data.Add("YearsInBusiness", dr["YIB"].ToString().Trim());
                data.Add("BusinessAddress", dr["BusinessAddress"].ToString().Trim());
                data.Add("BusinessAddressCity", dr["City"].ToString().Trim());
                data.Add("BusinessAddressState", dr["State"].ToString().Trim());
                data.Add("BusinessAddressZip", dr["Zip"].ToString().Trim());
                data.Add("P1Name", dr["FULLNAME"].ToString().Trim());
                data.Add("P1Title", dr["Title"].ToString().Trim());
                data.Add("P1Address", dr["P1Address"].ToString().Trim());
                data.Add("P1City", dr["P1City"].ToString().Trim());
                data.Add("P1State", dr["P1State"].ToString().Trim());
                data.Add("P1Zip", dr["P1Zip"].ToString().Trim());
                data.Add("P1AreaCode", dr["P1AreaCode"].ToString().Trim());
                data.Add("P1Phone", dr["P1HomePhone"].ToString().Trim());
                data.Add("P1SSN", dr["P1SSN"].ToString().Trim());
                #endregion

                #region Equipment Information
                data.Add("Equipment", dr["Equipment"].ToString().Trim());
                #endregion

                #region Payment Info
                data.Add("MonthlyPayment", dr["LeasePayment"].ToString().Trim());
                data.Add("LeaseTerm", dr["LeaseTerm"].ToString().Trim());
                #endregion

                #region Bank Info
                data.Add("BankName", dr["BankName"].ToString().Trim());
                data.Add("RoutingNumber", dr["RoutingNum"].ToString().Trim());
                data.Add("AccountNumber", dr["CheckingAcctNum"].ToString().Trim());
                #endregion

                RedirectAndPOST(this.Page, strUrl, data);

                LogBL LogData = new LogBL(AppId);
                LogData.InsertLogData(AffiliateID, "Sage DocuSign document created.");

            }

            //}
            #endregion


        }
        catch (Exception err)
        {
            CreateOnlineAppLog Log = new CreateOnlineAppLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + AppId.ToString() + " - " + "main.master.cs Create PDF Error - " + err.Message);
            //SetErrorMessage("Error Creating PDF. Please contact Technical Support: <a href=mailto:support@commercetech.com?Subject=Online App Error, App ID: " + Session["AppId"].ToString() + "> support@commercetech.com. </a> for assistance.");
        }
    }


    protected void docusignGetEnvelopeInfo(string url, string authenticateStr, string envelopeId, out string envelopeStatus)
    {
        //string username = "twu@commercetech.com";
        //string password = "Commerce1";
        //string username = "jscott@commercetech.com";
        //string password = "1Commerce11";
        string username = "sales@commercetech.com";
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
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

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



    protected void populateEnvelopeStatus()
    {

        //string username = "twu@commercetech.com";
        //string password = "Commerce1";
        //string username = "jscott@commercetech.com";
        //string password = "1Commerce11";
        string username = "sales@commercetech.com";
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
                    if (Convert.ToString(envelopeStatus).ToLower().Contains("complete"))
                    {
                        strEnvelopeStatus = "Signed";
                    }
                    else if (Convert.ToString(envelopeStatus).ToLower().Contains("sent"))
                    {
                        strEnvelopeStatus = "Viewed";
                    }
                    else
                    {
                        strEnvelopeStatus = Convert.ToString(envelopeStatus);
                    }
                    //lblEnvStatus.Text = "Envelope status: " + Convert.ToString(strEnvelopeStatus);
                }
            }
        }
    }


    protected void btnDigitalSignature_ClickNew(object sender, EventArgs e)
    {
        //string username = "twu@commercetech.com";
        //string password = "Commerce1";
        //string password = "Comtech540910";
        //string username = "jscott@commercetech.com";
        //string password = "1Commerce11";




        CreatePDF proc = new CreatePDF(AppId);
        string processor = proc.ReturnProcessorName();
        string pid = proc.ReturnPackageId();

        OnlineAppProfile Profile = new OnlineAppProfile(AppId);
        int AcctType = 0;

        DataSet dsStatus = Profile.GetProfileData();
        if (dsStatus.Tables[0].Rows.Count > 0)
        {
            DataRow dr = dsStatus.Tables[0].Rows[0];
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
        /*
        if (Session["Locked"].ToString().Trim() == "Yes")
        {
        }*/


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
        else
        {






            string username = "sales@commercetech.com";
            string password = "1Success11";
            string integratorKey = "ECOM-21cf1b09-3dbf-41fc-9910-13c11df86eb5";
            //string url = "https://demo.docusign.net/restapi/v2/login_information";
            string url = "https://na2.docusign.net/restapi/v2/login_information";
            string baseURL = "";	// we will retrieve this
            string accountId = "";	// will retrieve
            string envelopeId = "";	// will retrieve
            string uri = "";	// will retrieve
            string requestBody = "";
            string signerEmail = "";
            string signerName = "";
            string strP1Name = "";
            string strP1Email = "";
            string strP2Name = "";
            string strP2Email = "";
            string authenticateStr = "<DocuSignCredentials>" +
        "<Username>" + username + "</Username>" +
        "<Password>" + password + "</Password>" +
        "<IntegratorKey>" + integratorKey + "</IntegratorKey>" +
        "</DocuSignCredentials>";


            CompanyTable ComTable1 = new CompanyTable();
            ComTable1.InsertUpdateSigningInfo(true, true, true, AppId);

            NewAppInfo newApp = new NewAppInfo(AppId);
            bool docuSignStatus = newApp.docuSignStatus();
            if (docuSignStatus == true)
            {
                CreatePDF PDFFile = new CreatePDF(AppId);
                string strProcessor = PDFFile.ReturnProcessorName();
                string docuSignProcessor = newApp.GetDocuSignProcessor();
                if (docuSignProcessor == strProcessor)
                {
                    envelopeId = newApp.GetDocuSignEnvId();
                    if (envelopeId != "")
                    {
                        DataSet ds = new DataSet();
                        ds = newApp.docusginSignerInfo();
                        uri = "/envelopes/" + envelopeId;
                        accountId = "4400185";
                        baseURL = "https://na2.docusign.net/restapi/v2/accounts/4400185";

                        docusignAuth(url, authenticateStr, out accountId, out baseURL);

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            strP1Name = Convert.ToString(dr["P1FirstName"]).Trim() + " " + Convert.ToString(dr["P1LastName"]).Trim();
                            strP1Email = Convert.ToString(dr["P1Email"]).Trim();
                        }
                        string reqBody = "{\"authenticationMethod\": \"email\"," +
                                        "\"email\":\"" + strP1Email + "\"," +	 	// NOTE: Use different email address if username provided in non-email format!
                                        "\"clientUserId\":\"" + strP1Name + "\"," +
                                        "\"returnUrl\":\"http://www.docusign.com\"," +  // username can be in email format or an actual ID string+
                                        "\"userName\":\"" + strP1Name + "\"}";
                        docusignEmbededSign(baseURL, uri, reqBody, out url);
                        Response.Redirect(url, false);

                        //Set last modified date in newapp table
                        CommonFunctions GeneralInfo = new CommonFunctions(AppId);
                        GeneralInfo.UpdateLastModified();

                        string newStatus = "SUBMITTED FOR REVIEW";
                        string accountType = "Merchant";
                        NewAppInfo NewOnlineApp = new NewAppInfo(AppId);
                        bool updatedStatus = NewOnlineApp.UpdateStatus(newStatus, accountType);
                        OnlineAppStatus Locked = new OnlineAppStatus(AppId);
                        string strLocked = Locked.GetLocked();
                        Session["Locked"] = strLocked;

                        LogBL LogData = new LogBL(AppId);
                        LogData.InsertLogData(AffiliateID, "DocuSign envelope accessed.");

                    }
                }
                else
                {
                    newApp.updateDocuSign("", 0);

                    try
                    {

                        // STEP 1 - Login
                        accountId = "4400185";
                        baseURL = "https://na2.docusign.net/restapi/v2/accounts/4400185";


                        //docusignAuth(url, authenticateStr, out accountId, out baseURL);

                        //CreatePDF PDFFile = new CreatePDF(AppId);
                        //string strProcessor = PDFFile.ReturnProcessorName();

                        DataSet ds;
                        DataRow dr;

                        if (strProcessor.ToLower().Contains("sage"))
                        {
                            OAPDFBL SageData = new OAPDFBL(AppId);
                            ds = SageData.GetSageData();

                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                dr = ds.Tables[0].Rows[0];

                                //signerEmail = Convert.ToString(dr["Email"]).Trim();

                                signerEmail = Convert.ToString(dr["P1Email"]).Trim();
                                signerName = Convert.ToString(dr["P1FirstName"]).Trim() + " " + Convert.ToString(dr["P1LastName"]).Trim();

                                // Construct an outgoing JSON request body

                                requestBody = SageDocusignRequest(dr);

                            }
                        }
                        else if (strProcessor.ToLower().Contains("ipay"))
                        {
                            OAPDFBL iPayData = new OAPDFBL(AppId);
                            ds = iPayData.GetIPaymentData();

                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                dr = ds.Tables[0].Rows[0];

                                //signerEmail = Convert.ToString(dr["Email"]).Trim();
                                signerEmail = Convert.ToString(dr["P1Email"]).Trim();
                                signerName = Convert.ToString(dr["P1FirstName"]).Trim() + " " + Convert.ToString(dr["P1LastName"]).Trim();

                                // Construct an outgoing JSON request body

                                requestBody = iPayDocusignRequest(dr);

                            }
                        }
                        else if (strProcessor.ToLower().Contains("cardconnect"))
                        {
                            OAPDFBL CardConnectData = new OAPDFBL(AppId);
                            ds = CardConnectData.GetChaseData();

                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                dr = ds.Tables[0].Rows[0];

                                signerEmail = Convert.ToString(dr["P1Email"]).Trim();
                                signerName = Convert.ToString(dr["P1FullName"]).Trim();

                                // Construct an outgoing JSON request body

                                requestBody = CardConncetDocusignRequest(dr);

                            }
                        }


                        // STEP 2 - Request Envelope Result
                        docusignSendEnvelop(baseURL, requestBody, out envelopeId, out uri);

                        //STEP3 Embeded Signing

                        string reqBody = "{\"authenticationMethod\": \"email\"," +
                                        "\"email\":\"" + signerEmail + "\"," +	 	// NOTE: Use different email address if username provided in non-email format!
                                        "\"clientUserId\":\"" + signerName + "\"," +
                                        "\"returnUrl\":\"http://www.docusign.com\"," +  // username can be in email format or an actual ID string+
                                        "\"userName\":\"" + signerName + "\"}";

                        docusignEmbededSign(baseURL, uri, reqBody, out url);

                        int docuSignbit = 1;
                        newApp.updateDocuSign(envelopeId, docuSignbit);

                        Response.Redirect(url, false);

                        //Set last modified date in newapp table
                        CommonFunctions GeneralInfo = new CommonFunctions(AppId);
                        GeneralInfo.UpdateLastModified();

                        string newStatus = "SUBMITTED FOR REVIEW";
                        string accountType = "Merchant";
                        NewAppInfo NewOnlineApp = new NewAppInfo(AppId);
                        bool updatedStatus = NewOnlineApp.UpdateStatus(newStatus, accountType);
                        OnlineAppStatus Locked = new OnlineAppStatus(AppId);
                        string strLocked = Locked.GetLocked();
                        Session["Locked"] = strLocked;


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
                                Console.WriteLine(text);
                                CreateOnlineAppLog Log = new CreateOnlineAppLog();
                                Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + AppId.ToString() + " - " + "DocuSign Error - " + text);
                                SetErrorMessage(text);
                                LogBL LogData = new LogBL(AppId);
                                LogData.InsertLogData(AffiliateID, "Failed to create DocuSign envelope.");
                            }
                        }
                    }
                    finally
                    {
                        newApp.UpdateDocuSignProcessor(strProcessor);
                    }
                }
            }
            else
            {
                try
                {

                    // STEP 1 - Login

                    // docusignAuth(url, authenticateStr, out accountId, out baseURL);

                    accountId = "4400185";
                    baseURL = "https://na2.docusign.net/restapi/v2/accounts/4400185";


                    CreatePDF PDFFile = new CreatePDF(AppId);
                    string strProcessor = PDFFile.ReturnProcessorName();

                    DataSet ds;
                    DataRow dr;

                    if (strProcessor.ToLower().Contains("sage"))
                    {
                        OAPDFBL SageData = new OAPDFBL(AppId);
                        ds = SageData.GetSageData();

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            dr = ds.Tables[0].Rows[0];

                            signerEmail = Convert.ToString(dr["P1Email"]).Trim();
                            signerName = Convert.ToString(dr["P1FirstName"]).Trim() + " " + Convert.ToString(dr["P1LastName"]).Trim();

                            // Construct an outgoing JSON request body

                            requestBody = SageDocusignRequest(dr);

                        }
                    }
                    else if (strProcessor.ToLower().Contains("ipay"))
                    {
                        OAPDFBL iPayData = new OAPDFBL(AppId);
                        ds = iPayData.GetIPaymentData();

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            dr = ds.Tables[0].Rows[0];

                            signerEmail = Convert.ToString(dr["P1Email"]).Trim();
                            signerName = Convert.ToString(dr["P1FirstName"]).Trim() + " " + Convert.ToString(dr["P1LastName"]).Trim();

                            // Construct an outgoing JSON request body

                            requestBody = iPayDocusignRequest(dr);

                        }
                    }
                    else if (strProcessor.ToLower().Contains("cardconnect"))
                    {
                        OAPDFBL CardConnectData = new OAPDFBL(AppId);
                        ds = CardConnectData.GetChaseData();

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            dr = ds.Tables[0].Rows[0];

                            signerEmail = Convert.ToString(dr["P1Email"]).Trim();
                            signerName = Convert.ToString(dr["P1FullName"]).Trim();

                            // Construct an outgoing JSON request body

                            requestBody = CardConncetDocusignRequest(dr);

                        }
                    }


                    // STEP 2 - Request Envelope Result
                    docusignSendEnvelop(baseURL, requestBody, out envelopeId, out uri);

                    //STEP3 Embeded Signing

                    string reqBody = "{\"authenticationMethod\": \"email\"," +
                                    "\"email\":\"" + signerEmail + "\"," +	 	// NOTE: Use different email address if username provided in non-email format!
                                    "\"clientUserId\":\"" + signerName + "\"," +
                                    "\"returnUrl\":\"http://www.docusign.com\"," +  // username can be in email format or an actual ID string+
                                    "\"userName\":\"" + signerName + "\"}";

                    docusignEmbededSign(baseURL, uri, reqBody, out url);

                    int docuSignbit = 1;
                    newApp.updateDocuSign(envelopeId, docuSignbit);

                    Response.Redirect(url, false);

                    //Set last modified date in newapp table
                    CommonFunctions GeneralInfo = new CommonFunctions(AppId);
                    GeneralInfo.UpdateLastModified();

                    string newStatus = "SUBMITTED FOR REVIEW";
                    string accountType = "Merchant";
                    NewAppInfo NewOnlineApp = new NewAppInfo(AppId);
                    bool updatedStatus = NewOnlineApp.UpdateStatus(newStatus, accountType);
                    OnlineAppStatus Locked = new OnlineAppStatus(AppId);
                    string strLocked = Locked.GetLocked();
                    Session["Locked"] = strLocked;

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
                            Console.WriteLine(text);
                            CreateOnlineAppLog Log = new CreateOnlineAppLog();
                            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + AppId.ToString() + " - " + "DocuSign Error - " + text);
                            SetErrorMessage(text);
                            LogBL LogData = new LogBL(AppId);
                            LogData.InsertLogData(AffiliateID, "Failed to create DocuSign envelope.");
                        }
                    }
                }
                finally
                {
                    CreatePDF PDFFile = new CreatePDF(AppId);
                    string strProcessor = PDFFile.ReturnProcessorName();
                    newApp.UpdateDocuSignProcessor(strProcessor);
                }
            }
        }
    }

    protected void btnGetEnvelopeInfo_Click(Object sender, EventArgs e)
    {

        //string username = "twu@commercetech.com";
        //string password = "Commerce1";
        //string username = "jscott@commercetech.com";
        //string password = "1Commerce11";
        string username = "sales@commercetech.com";
        string password = "1Success11";
        string integratorKey = "ECOM-21cf1b09-3dbf-41fc-9910-13c11df86eb5";
        //string url = "https://demo.docusign.net/restapi/v2/login_information";
        string url = "https://na2.docusign.net/restapi/v2/login_information";
        string baseURL = "";	// we will retrieve this
        string accountId = "";	// will retrieve
        string envelopeId = "";	// will retrieve
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
            if (docuSignProcessor == strProcessor)
            {
                envelopeId = newApp.GetDocuSignEnvId();
                if (envelopeId != "")
                {
                    DataSet ds = new DataSet();
                    ds = newApp.docusginSignerInfo();
                    uri = "/envelopes/" + envelopeId;
                    docusignGetEnvelopeInfo(url, authenticateStr, envelopeId, out accountId, out baseURL);
                }
            }
        }
    }

    protected void docusignGetEnvelopeInfo(string url, string authenticateStr, string envelopeId, out string accountId, out string baseURL)
    {
        //string username = "twu@commercetech.com";
        //string password = "Commerce1";
        //string username = "jscott@commercetech.com";
        //string password = "1Commerce11";
        string username = "sales@commercetech.com";
        string password = "1Success11";
        string integratorKey = "ECOM-21cf1b09-3dbf-41fc-9910-13c11df86eb5";
        //string url = "https://demo.docusign.net/restapi/v2/login_information";
        //string baseURL = "";	// we will retrieve this
        //string accountId = "";	// will retrieve
        string accountId1 = "";
        string baseURL1 = "";
        //string envelopeId = "";
        string envelopeStatus1 = "";
        string envelopeUri = "/envelopes/" + envelopeId;

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
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

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

            SetErrorMessage(responseText);
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
                    SetErrorMessage(text);
                    LogBL LogData = new LogBL(AppId);
                    LogData.InsertLogData(AffiliateID, "Failed to retrieve envelope status.");
                }
            }
        }
        string envelopeStatus = envelopeStatus1;
        accountId = accountId1;
        baseURL = baseURL1;

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

    protected void docusignAuth(string url, string authenticateStr, out string accountId, out string baseURL)
    {
        string accountId1 = "";
        string baseURL1 = "";
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
                    SetErrorMessage(text);
                    //LogBL LogData = new LogBL(AppId);
                    //LogData.InsertLogData(AffiliateID, "Authorization failed.");
                }
            }
        }
        accountId = accountId1;
        baseURL = baseURL1;
    }

    protected string SageDocusignRequest(DataRow dr)
    {
        string strCheckBox = "";
        string platForm = "";

        //string templateId = "C0EDFA45-98E2-4F7F-8B0D-39FFDA17389F";
        //string templateId = "2D538500-CF72-441F-B62A-27F5886D265D";
        string templateId = "E70E9E1B-F50E-45A3-8B3F-FE45B0B7457B";

        string strMonthToMonth = "";

        if (Convert.ToString(dr["ContractTerm"]).Trim() == "1 month")
        {
            templateId = "70E9F539-5676-4FCD-BA4F-ED81CDB3C6DB";

            strMonthToMonth =
                 "{" +
                    "\"tabLabel\":\"LegalNameMTM\"," +
                    "\"name\":\"LegalNameMTM\"," +
                    "\"value\":\"" + Convert.ToString(dr["COMPANYNAME"]).Trim() + "\"" +
                    "}," +

                    "{" +
                    "\"tabLabel\":\"P1 First Name MTM\"," +
                    "\"name\":\"P1 First Name MTM\"," +
                    "\"value\":\"" + Convert.ToString(dr["P1FirstName"]).Trim() + "\"" +
                    "}," +

                    "{" +
                    "\"tabLabel\":\"P1 Last Name MTM\"," +
                    "\"name\":\"P1 Last Name MTM\"," +
                    "\"value\":\"" + Convert.ToString(dr["P1LastName"]).Trim() + "\"" +
                    "}," +

                    "{" +
                    "\"tabLabel\":\"P1 Title MTM\"," +
                    "\"name\":\"P1 Title MTM\"," +
                    "\"value\":\"" + Convert.ToString(dr["P1Title"]).Trim() + "\"" +
                    "},";
        }
        else if (Convert.ToString(dr["ContractTerm"]).Trim() == "1 year")
        {
            templateId = "2748cecc-2234-46cb-ace8-f013ec900d65";

            strMonthToMonth =
                 "{" +
                    "\"tabLabel\":\"LegalNameMTM\"," +
                    "\"name\":\"LegalNameMTM\"," +
                    "\"value\":\"" + Convert.ToString(dr["COMPANYNAME"]).Trim() + "\"" +
                    "}," +

                    "{" +
                    "\"tabLabel\":\"P1 First Name MTM\"," +
                    "\"name\":\"P1 First Name MTM\"," +
                    "\"value\":\"" + Convert.ToString(dr["P1FirstName"]).Trim() + "\"" +
                    "}," +

                    "{" +
                    "\"tabLabel\":\"P1 Last Name MTM\"," +
                    "\"name\":\"P1 Last Name MTM\"," +
                    "\"value\":\"" + Convert.ToString(dr["P1LastName"]).Trim() + "\"" +
                    "}," +

                    "{" +
                    "\"tabLabel\":\"P1 Title MTM\"," +
                    "\"name\":\"P1 Title MTM\"," +
                    "\"value\":\"" + Convert.ToString(dr["P1Title"]).Trim() + "\"" +
                    "},";
        }

        if ((dr["AmexApplied"].ToString().Trim() == "1") && (dr["PrevAmexNum"].ToString().Trim() == ""))
        {
            //templateId = "E6DF32A5-2AA0-4354-A864-565968838E15";
            templateId = "E70E9E1B-F50E-45A3-8B3F-FE45B0B7457B";
            //templateId = "2D538500-CF72-441F-B62A-27F5886D265D";

            if (Convert.ToString(dr["ContractTerm"]).Trim() == "1 month")
            {
                templateId = "70E9F539-5676-4FCD-BA4F-ED81CDB3C6DB";

                strMonthToMonth =
                 "{" +
                    "\"tabLabel\":\"LegalNameMTM\"," +
                    "\"name\":\"LegalNameMTM\"," +
                    "\"value\":\"" + Convert.ToString(dr["COMPANYNAME"]).Trim() + "\"" +
                    "}," +

                    "{" +
                    "\"tabLabel\":\"P1 First Name MTM\"," +
                    "\"name\":\"P1 First Name MTM\"," +
                    "\"value\":\"" + Convert.ToString(dr["P1FirstName"]).Trim() + "\"" +
                    "}," +

                    "{" +
                    "\"tabLabel\":\"P1 Last Name MTM\"," +
                    "\"name\":\"P1 Last Name MTM\"," +
                    "\"value\":\"" + Convert.ToString(dr["P1LastName"]).Trim() + "\"" +
                    "}," +

                    "{" +
                    "\"tabLabel\":\"P1 Title MTM\"," +
                    "\"name\":\"P1 Title MTM\"," +
                    "\"value\":\"" + Convert.ToString(dr["P1Title"]).Trim() + "\"" +
                    "},";
            }
            else if (Convert.ToString(dr["ContractTerm"]).Trim() == "1 year")
            {
                templateId = "2748cecc-2234-46cb-ace8-f013ec900d65";

                strMonthToMonth =
                     "{" +
                        "\"tabLabel\":\"LegalNameMTM\"," +
                        "\"name\":\"LegalNameMTM\"," +
                        "\"value\":\"" + Convert.ToString(dr["COMPANYNAME"]).Trim() + "\"" +
                        "}," +

                        "{" +
                        "\"tabLabel\":\"P1 First Name MTM\"," +
                        "\"name\":\"P1 First Name MTM\"," +
                        "\"value\":\"" + Convert.ToString(dr["P1FirstName"]).Trim() + "\"" +
                        "}," +

                        "{" +
                        "\"tabLabel\":\"P1 Last Name MTM\"," +
                        "\"name\":\"P1 Last Name MTM\"," +
                        "\"value\":\"" + Convert.ToString(dr["P1LastName"]).Trim() + "\"" +
                        "}," +

                        "{" +
                        "\"tabLabel\":\"P1 Title MTM\"," +
                        "\"name\":\"P1 Title MTM\"," +
                        "\"value\":\"" + Convert.ToString(dr["P1Title"]).Trim() + "\"" +
                        "},";
            }
        }

        string strCoSigner = "";
        if (!Convert.IsDBNull(dr["P2FirstName"]))
        {
            if (dr["P2FirstName"].ToString().Trim() != "")
            {
                templateId = "C18A94EF-FAE3-4F06-BA59-EF258A84A042";
                //templateId = "55F1CF39-BC55-4600-8594-1261568B89D8";
                if (Convert.ToString(dr["ContractTerm"]).Trim() == "1 month")
                {
                    templateId = "9A66684F-8BF6-43B0-8073-DE859533BE8B";

                    strMonthToMonth =
                 "{" +
                    "\"tabLabel\":\"LegalNameMTM\"," +
                    "\"name\":\"LegalNameMTM\"," +
                    "\"value\":\"" + Convert.ToString(dr["COMPANYNAME"]).Trim() + "\"" +
                    "}," +

                    "{" +
                    "\"tabLabel\":\"P1 First Name MTM\"," +
                    "\"name\":\"P1 First Name MTM\"," +
                    "\"value\":\"" + Convert.ToString(dr["P1FirstName"]).Trim() + "\"" +
                    "}," +

                    "{" +
                    "\"tabLabel\":\"P1 Last Name MTM\"," +
                    "\"name\":\"P1 Last Name MTM\"," +
                    "\"value\":\"" + Convert.ToString(dr["P1LastName"]).Trim() + "\"" +
                    "}," +

                    "{" +
                    "\"tabLabel\":\"P1 Title MTM\"," +
                    "\"name\":\"P1 Title MTM\"," +
                    "\"value\":\"" + Convert.ToString(dr["P1Title"]).Trim() + "\"" +
                    "},";
                }
                else if (Convert.ToString(dr["ContractTerm"]).Trim() == "1 year")
                {
                    templateId = "09e38730-204a-4bb7-ad8b-1ae14f3208cb";

                    strMonthToMonth =
                 "{" +
                    "\"tabLabel\":\"LegalNameMTM\"," +
                    "\"name\":\"LegalNameMTM\"," +
                    "\"value\":\"" + Convert.ToString(dr["COMPANYNAME"]).Trim() + "\"" +
                    "}," +

                    "{" +
                    "\"tabLabel\":\"P1 First Name MTM\"," +
                    "\"name\":\"P1 First Name MTM\"," +
                    "\"value\":\"" + Convert.ToString(dr["P1FirstName"]).Trim() + "\"" +
                    "}," +

                    "{" +
                    "\"tabLabel\":\"P1 Last Name MTM\"," +
                    "\"name\":\"P1 Last Name MTM\"," +
                    "\"value\":\"" + Convert.ToString(dr["P1LastName"]).Trim() + "\"" +
                    "}," +

                    "{" +
                    "\"tabLabel\":\"P1 Title MTM\"," +
                    "\"name\":\"P1 Title MTM\"," +
                    "\"value\":\"" + Convert.ToString(dr["P1Title"]).Trim() + "\"" +
                    "},";
                }
                if ((dr["AmexApplied"].ToString().Trim() == "1") && (dr["PrevAmexNum"].ToString().Trim() == ""))
                {

                    //templateId = "E8998D21-8CE6-420F-AA8C-3E717320B0B9";
                    templateId = "C18A94EF-FAE3-4F06-BA59-EF258A84A042";
                    //templateId = "55F1CF39-BC55-4600-8594-1261568B89D8";
                    if (Convert.ToString(dr["ContractTerm"]).Trim() == "1 month")
                    {
                        //templateId = "9A66684F-8BF6-43B0-8073-DE859533BE8B";
                        templateId = "9A66684F-8BF6-43B0-8073-DE859533BE8B";

                        strMonthToMonth =
                 "{" +
                    "\"tabLabel\":\"LegalNameMTM\"," +
                    "\"name\":\"LegalNameMTM\"," +
                    "\"value\":\"" + Convert.ToString(dr["COMPANYNAME"]).Trim() + "\"" +
                    "}," +

                    "{" +
                    "\"tabLabel\":\"P1 First Name MTM\"," +
                    "\"name\":\"P1 First Name MTM\"," +
                    "\"value\":\"" + Convert.ToString(dr["P1FirstName"]).Trim() + "\"" +
                    "}," +

                    "{" +
                    "\"tabLabel\":\"P1 Last Name MTM\"," +
                    "\"name\":\"P1 Last Name MTM\"," +
                    "\"value\":\"" + Convert.ToString(dr["P1LastName"]).Trim() + "\"" +
                    "}," +

                    "{" +
                    "\"tabLabel\":\"P1 Title MTM\"," +
                    "\"name\":\"P1 Title MTM\"," +
                    "\"value\":\"" + Convert.ToString(dr["P1Title"]).Trim() + "\"" +
                    "},";
                    }
                    else if (Convert.ToString(dr["ContractTerm"]).Trim() == "1 year")
                    {
                        templateId = "09e38730-204a-4bb7-ad8b-1ae14f3208cb";

                        strMonthToMonth =
                     "{" +
                        "\"tabLabel\":\"LegalNameMTM\"," +
                        "\"name\":\"LegalNameMTM\"," +
                        "\"value\":\"" + Convert.ToString(dr["COMPANYNAME"]).Trim() + "\"" +
                        "}," +

                        "{" +
                        "\"tabLabel\":\"P1 First Name MTM\"," +
                        "\"name\":\"P1 First Name MTM\"," +
                        "\"value\":\"" + Convert.ToString(dr["P1FirstName"]).Trim() + "\"" +
                        "}," +

                        "{" +
                        "\"tabLabel\":\"P1 Last Name MTM\"," +
                        "\"name\":\"P1 Last Name MTM\"," +
                        "\"value\":\"" + Convert.ToString(dr["P1LastName"]).Trim() + "\"" +
                        "}," +

                        "{" +
                        "\"tabLabel\":\"P1 Title MTM\"," +
                        "\"name\":\"P1 Title MTM\"," +
                        "\"value\":\"" + Convert.ToString(dr["P1Title"]).Trim() + "\"" +
                        "},";
                    }
                }

                strCoSigner =
                    "{" +
                    "\"email\":\"" + Convert.ToString(dr["P2Email"]).Trim() + "\"," +
                    "\"name\":\"" + Convert.ToString(dr["P2FirstName"]).Trim() + " " + Convert.ToString(dr["P2LastName"]).Trim() + "\"," +
                    "\"roleName\":\"CoSigner\"," +
                    "}";
            }
        }





        if (dr["Platform"].ToString().Trim().Contains("Paymentech"))
            platForm = "Paymentech";
        else
            platForm = "Visanet/TSYS";

        string BusinessOpenMonth = "";
        string BusinessOpenYear = "";
        string LengthOfOwnership = "";
        string prevProcNum = "";

        if ((dr["MIB"].ToString().Trim() != "") && (dr["YIB"].ToString().Trim() != ""))
        {
            DateTime BusinessDate = DateTime.Now.AddMonths((Convert.ToInt32(dr["MIB"].ToString().Trim())) * -1);
            DateTime BusinessOpenDate = BusinessDate.AddYears((Convert.ToInt32(dr["YIB"].ToString().Trim())) * -1);
            BusinessOpenMonth = BusinessOpenDate.Month.ToString().Trim();
            BusinessOpenYear = BusinessOpenDate.Year.ToString().Trim();
            //data.Add("Business Open Date", BusinessOpenMonth + "/" + BusinessOpenYear);

            LengthOfOwnership = "";
            if (dr["YIB"].ToString().Trim() != "")
                LengthOfOwnership = dr["YIB"].ToString().Trim() + " Years ";
            else
                LengthOfOwnership = "0 Years ";

            if (dr["MIB"].ToString().Trim() != "")
                LengthOfOwnership += "and " + dr["MIB"].ToString().Trim() + " Months";
        }

        if (dr["PrevProcessor"].ToString().Contains("Sage"))
        {
            prevProcNum = Convert.ToString(dr["PrevMerchantAcctNo"]).Trim();
        }

        string strOwnershipType = "";


        if (dr["LegalStatus"].ToString().ToLower().Contains("trust"))
        {

            strCheckBox = strCheckBox + "{" +
                       "\"tabLabel\":\"OwnershipTypeTrust\"," +
                       "\"name\":\"OwnershipTypeTrust\"," +
                       "\"selected\":\"" + "true" + "\"" +
                       "},";

            strOwnershipType = "{" +
                    "\"tabLabel\":\"OwnershipTypeTrust\"," +
                    "\"name\":\"OwnershipTypeTrust\"," +
                    "\"value\":\"" + "x" + "\"" +
                    "},";
        }
        else if (dr["LegalStatus"].ToString().ToLower().Contains("llc"))
        {
            strCheckBox = strCheckBox + "{" +
                       "\"tabLabel\":\"OwnershipTypellc\"," +
                       "\"name\":\"OwnershipTypellc\"," +
                       "\"selected\":\"" + "true" + "\"" +
                       "},";

            strOwnershipType = "{" +
                    "\"tabLabel\":\"OwnershipTypellc\"," +
                    "\"name\":\"OwnershipTypellc\"," +
                    "\"value\":\"" + "x" + "\"" +
                    "},";
        }
        else if ((dr["LegalStatus"].ToString().ToLower().Contains("non-profit")) || (dr["LegalStatus"].ToString().ToLower().Contains("tax exempt")))
        {
            strCheckBox = strCheckBox + "{" +
                       "\"tabLabel\":\"OwnershipTypeNonProfit\"," +
                       "\"name\":\"OwnershipTypeNonProfit\"," +
                       "\"selected\":\"" + "true" + "\"" +
                       "},";

            strOwnershipType = "{" +
                    "\"tabLabel\":\"OwnershipTypeNonProfit\"," +
                    "\"name\":\"OwnershipTypeNonProfit\"," +
                    "\"value\":\"" + "x" + "\"" +
                    "},";
        }
        else if (dr["LegalStatus"].ToString().ToLower().Contains("corporation"))
        {
            strCheckBox = strCheckBox + "{" +
                       "\"tabLabel\":\"OwnershipTypeCorp\"," +
                       "\"name\":\"OwnershipTypeCorp\"," +
                       "\"selected\":\"" + "true" + "\"" +
                       "},";

            strOwnershipType = "{" +
                    "\"tabLabel\":\"OwnershipTypeCorp\"," +
                    "\"name\":\"OwnershipTypeCorp\"," +
                    "\"value\":\"" + "x" + "\"" +
                    "},";
        }
        else if (dr["LegalStatus"].ToString().ToLower().Contains("medical"))
        {
            strCheckBox = strCheckBox + "{" +
                       "\"tabLabel\":\"OwnershipTypemedical\"," +
                       "\"name\":\"OwnershipTypemedical\"," +
                       "\"selected\":\"" + "true" + "\"" +
                       "},";

            strOwnershipType = "{" +
                    "\"tabLabel\":\"OwnershipTypemedical\"," +
                    "\"name\":\"OwnershipTypemedical\"," +
                    "\"value\":\"" + "x" + "\"" +
                    "},";
        }
        else if (dr["LegalStatus"].ToString().ToLower().Contains("government"))
        {
            strCheckBox = strCheckBox + "{" +
                       "\"tabLabel\":\"OwnershipTypeGov\"," +
                       "\"name\":\"OwnershipTypeGov\"," +
                       "\"selected\":\"" + "true" + "\"" +
                       "},";

            strOwnershipType = "{" +
                        "\"tabLabel\":\"OwnershipTypeGov\"," +
                        "\"name\":\"OwnershipTypeGov\"," +
                        "\"value\":\"" + "x" + "\"" +
                        "},";
        }
        else if (dr["LegalStatus"].ToString().ToLower().Contains("int'l"))
        {
            strCheckBox = strCheckBox + "{" +
                       "\"tabLabel\":\"OwnershipTypeintl\"," +
                       "\"name\":\"OwnershipTypeintl\"," +
                       "\"selected\":\"" + "true" + "\"" +
                       "},";

            strOwnershipType = "{" +
                        "\"tabLabel\":\"OwnershipTypeintl\"," +
                        "\"name\":\"OwnershipTypeintl\"," +
                        "\"value\":\"" + "x" + "\"" +
                        "},";
        }
        else if (dr["LegalStatus"].ToString().ToLower().Contains("partnership"))
        {
            strCheckBox = strCheckBox + "{" +
                       "\"tabLabel\":\"OwnershipTypepartnership\"," +
                       "\"name\":\"OwnershipTypepartnership\"," +
                       "\"selected\":\"" + "true" + "\"" +
                       "},";

            strOwnershipType = "{" +
                        "\"tabLabel\":\"OwnershipTypepartnership\"," +
                        "\"name\":\"OwnershipTypepartnership\"," +
                        "\"value\":\"" + "x" + "\"" +
                        "},";
        }
        else if (dr["LegalStatus"].ToString().ToLower().Contains("sole proprietorship"))
        {
            strCheckBox = strCheckBox + "{" +
                       "\"tabLabel\":\"OwnershipTypeSol\"," +
                       "\"name\":\"OwnershipTypeSol\"," +
                       "\"selected\":\"" + "true" + "\"" +
                       "},";

            strOwnershipType = "{" +
                        "\"tabLabel\":\"OwnershipTypeSol\"," +
                        "\"name\":\"OwnershipTypeSol\"," +
                        "\"value\":\"" + "x" + "\"" +
                        "},";
        }


        string strP2 = "";

        if (!Convert.IsDBNull(dr["P2FirstName"]))
        {
            if (dr["P2FirstName"].ToString().Trim() != "")
            {
                strP2 = "{" +
                    "\"tabLabel\":\"P2 Ownership%\"," +
                    "\"name\":\"P2 Ownership%\"," +
                    "\"value\":\"" + Convert.ToString(dr["P2OwnershipPercent"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"P2 Last Name\"," +
                    "\"name\":\"P2 Last Name\"," +
                    "\"value\":\"" + Convert.ToString(dr["P2LastName"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"P2 First Name\"," +
                    "\"name\":\"P2 First Name\"," +
                    "\"value\":\"" + Convert.ToString(dr["P2FirstName"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"P2 Full Name\"," +
                    "\"name\":\"P2 Full Name\"," +
                    "\"value\":\"" + Convert.ToString(dr["P2FirstName"]).Trim() + " " + Convert.ToString(dr["P2LastName"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"P2 Full Name 2\"," +
                    "\"name\":\"P2 Full Name 2\"," +
                    "\"value\":\"" + Convert.ToString(dr["P2FirstName"]).Trim() + " " + Convert.ToString(dr["P2LastName"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"P2 Full Name 3\"," +
                    "\"name\":\"P2 Full Name 3\"," +
                    "\"value\":\"" + Convert.ToString(dr["P2FirstName"]).Trim() + " " + Convert.ToString(dr["P2LastName"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"P2 Title\"," +
                    "\"name\":\"P2 Title\"," +
                    "\"value\":\"" + Convert.ToString(dr["P2Title"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"P2 Title 2\"," +
                    "\"name\":\"P2 Title 2\"," +
                    "\"value\":\"" + Convert.ToString(dr["P2Title"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"P2 Address\"," +
                    "\"name\":\"P2 Address\"," +
                    "\"value\":\"" + Convert.ToString(dr["p2Address"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"P2 Zip\"," +
                    "\"name\":\"P2 Zip\"," +
                    "\"value\":\"" + Convert.ToString(dr["P1ZipCode"]).Trim() + "\"" +
                    "}," +
                     "{" +
                    "\"tabLabel\":\"P2 City, State\"," +
                    "\"name\":\"P2 City, State\"," +
                    "\"value\":\"" + Convert.ToString(dr["P1City"]).Trim() + ", " +
                        Convert.ToString(dr["P1State"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"P2 City, State, Zip\"," +
                    "\"name\":\"P2 City, State, Zip\"," +
                    "\"value\":\"" + Convert.ToString(dr["P2City"]).Trim() + ", " + Convert.ToString(dr["P2State"]).Trim() + " " + Convert.ToString(dr["P2ZipCode"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"P2 Phone\"," +
                    "\"name\":\"P2 Phone\"," +
                    "\"value\":\"" + Convert.ToString(dr["p2PhoneNumber"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"P2 Email\"," +
                    "\"name\":\"P2 Email\"," +
                    "\"value\":\"" + Convert.ToString(dr["P2Email"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"P2 SSN\"," +
                    "\"name\":\"P2 SSN\"," +
                    "\"value\":\"" + Convert.ToString(dr["P2SSN"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"P2 DOB\"," +
                    "\"name\":\"P2 DOB\"," +
                    "\"value\":\"" + Convert.ToString(dr["P2DOB"]).Trim() + "\"" +
                    "},"
                    ;
            }
        }
        string strBusinessType = "";

        if (Convert.ToInt32(dr["BusinessPctMailOrder"]) >= 50)
        {
            strCheckBox = strCheckBox + "{" +
                       "\"tabLabel\":\"BusinessTypeMoto\"," +
                       "\"name\":\"BusinessTypeMoto\"," +
                       "\"selected\":\"" + "true" + "\"" +
                       "},";

            strBusinessType = "{" +
                    "\"tabLabel\":\"BusinessTypeMoto\"," +
                    "\"name\":\"BusinessTypeMoto\"," +
                    "\"value\":\"" + "x" + "\"" +
                    "},";
        }
        else if (Convert.ToInt32(dr["BusinessPctInternet"]) >= 50)
        {
            strCheckBox = strCheckBox + "{" +
                       "\"tabLabel\":\"BusinessTypeInternet\"," +
                       "\"name\":\"BusinessTypeInternet\"," +
                       "\"selected\":\"" + "true" + "\"" +
                       "},";

            strBusinessType = "{" +
                    "\"tabLabel\":\"BusinessTypeInternet\"," +
                    "\"name\":\"BusinessTypeInternet\"," +
                    "\"value\":\"" + "x" + "\"" +
                    "},";
        }
        else if (Convert.ToInt32(dr["BusinessPctRetail"]) >= 50)
        {
            strCheckBox = strCheckBox + "{" +
                       "\"tabLabel\":\"BusinessTypeRetail\"," +
                       "\"name\":\"BusinessTypeRetail\"," +
                       "\"selected\":\"" + "true" + "\"" +
                       "},";

            strBusinessType = "{" +
                    "\"tabLabel\":\"BusinessTypeRetail\"," +
                    "\"name\":\"BusinessTypeRetail\"," +
                    "\"value\":\"" + "x" + "\"" +
                    "},";
        }
        else if (Convert.ToInt32(dr["BusinessPctRestaurant"]) >= 50)
        {
            strCheckBox = strCheckBox + "{" +
                       "\"tabLabel\":\"BusinessTypeRestaurant\"," +
                       "\"name\":\"BusinessTypeRestaurant\"," +
                       "\"selected\":\"" + "true" + "\"" +
                       "},";

            strBusinessType = "{" +
                    "\"tabLabel\":\"BusinessTypeRestaurant\"," +
                    "\"name\":\"BusinessTypeRestaurant\"," +
                    "\"value\":\"" + "x" + "\"" +
                    "},";
        }

        string strReturnPolicy = "";

        if (dr["RefundPolicy"].ToString().Trim() == "Refund within 30 days")
        {
            strReturnPolicy = "{" +
                "\"tabLabel\":\"Return Policy\"," +
                "\"name\":\"Return Policy\"," +
                "\"value\":\"" + "30 Days Money Back Guarantee" + "\"" +
                "},";
        }
        else if (dr["RefundPolicy"].ToString().Trim() == "Exchange Only")
        {
            strReturnPolicy = "{" +
                "\"tabLabel\":\"Return Policy\"," +
                "\"name\":\"Return Policy\"," +
                "\"value\":\"" + "30 Days Exchange Only" + "\"" +
                "},";
        }
        else if (dr["RefundPolicy"].ToString().Trim() == "No Refund")
        {
            strReturnPolicy = "{" +
                "\"tabLabel\":\"Return Policy\"," +
                "\"name\":\"Return Policy\"," +
                "\"value\":\"" + "No Refund" + "\"" +
                "},";
        }
        else if (dr["RefundPolicy"].ToString().Trim().Contains("Other"))
        {
            strReturnPolicy = "{" +
                "\"tabLabel\":\"Return Policy\"," +
                "\"name\":\"Return Policy\"," +
                "\"value\":\"" + "Other" + "\"" +
                "},";
        }

        string strDebit = "";

        if (dr["DebitStatus"].ToString().ToLower().Contains("yes"))
        {
            strCheckBox = strCheckBox + "{" +
                       "\"tabLabel\":\"PinDebitY\"," +
                       "\"name\":\"PinDebitY\"," +
                       "\"selected\":\"" + "true" + "\"" +
                       "}," +
                "{" +
                "\"tabLabel\":\"DPTY\"," +
                "\"name\":\"DPTY\"," +
                "\"selected\":\"" + "true" + "\"" +
                "}," +
                "{" +
                       "\"tabLabel\":\"chkPinDebit\"," +
                       "\"name\":\"chkPinDebit\"," +
                       "\"selected\":\"" + "true" + "\"" +
                       "}," +




                       "{" +

                       "\"tabLabel\":\"PinDebitTransFeeChecked\"," +
                       "\"name\":\"PinDebitTransFeeChecked\"," +
                       "\"selected\":\"" + "true" + "\"" +
                       "},"
                ;

            if (Convert.ToString(dr["Interchange"]).Trim() == "True")
            {
                strCheckBox = strCheckBox + "{" +
                       "\"tabLabel\":\"PinDebitIPT\"," +
                       "\"name\":\"PinDebitIPT\"," +
                       "\"selected\":\"" + "true" + "\"" +
                       "},";
            }

            strDebit = "{" +
                "\"tabLabel\":\"PinDebitY\"," +
                "\"name\":\"PinDebitY\"," +
                "\"value\":\"" + "x" + "\"" +
                "}," +
                "{" +
                "\"tabLabel\":\"DPTY\"," +
                "\"name\":\"DPTY\"," +
                "\"value\":\"" + "x" + "\"" +
                "}," +
                "{" +
                "\"tabLabel\":\"PinDebit Rate1\"," +
                "\"name\":\"PinDebit Rate1\"," +
                "\"value\":\"" + Convert.ToString(dr["PinDebitDiscount"]).Trim() + "\"" +
                "},";

            if (Convert.ToString(dr["Interchange"]).Trim() == "True")
            {
                strDebit = strDebit +
                       "{" +
                "\"tabLabel\":\"PinDebitIPT\"," +
                "\"name\":\"PinDebitIPT\"," +
                "\"value\":\"" + "x" + "\"" +
                "},";
            }

        }
        else
        {
            strCheckBox = strCheckBox + "{" +
                       "\"tabLabel\":\"PinDebitN\"," +
                       "\"name\":\"PinDebitN\"," +
                       "\"selected\":\"" + "true" + "\"" +
                       "},";

            strDebit = "{" +
                "\"tabLabel\":\"PinDebitN\"," +
                "\"name\":\"PinDebitN\"," +
                "\"value\":\"" + "x" + "\"" +
                "},";
        }

        string strInterchange = "";
        if (Convert.ToString(dr["Interchange"]).Trim() == "True")
        {
            strCheckBox = strCheckBox + "{" +
                       "\"tabLabel\":\"DiscIPT\"," +
                       "\"name\":\"DiscIPT\"," +
                       "\"selected\":\"" + "true" + "\"" +
                       "}," +
                     "{" +
                     "\"tabLabel\":\"VisaIPT\"," +
                     "\"name\":\"VisaIPT\"," +
                     "\"selected\":\"" + "true" + "\"" +
                     "}," +
                     "{" +
                     "\"tabLabel\":\"VisaCheckCardIPT\"," +
                     "\"name\":\"VisaCheckCardIPT\"," +
                     "\"selected\":\"" + "true" + "\"" +
                     "}," +
                     "{" +
                     "\"tabLabel\":\"MCIPT\"," +
                     "\"name\":\"MCIPT\"," +
                     "\"selected\":\"" + "true" + "\"" +
                     "}," +
                     "{" +
                     "\"tabLabel\":\"MCCheckCardIPT\"," +
                     "\"name\":\"MCCheckCardIPT\"," +
                     "\"selected\":\"" + "true" + "\"" +
                     "}," +
                     "{" +
                     "\"tabLabel\":\"AmexIPT\"," +
                     "\"name\":\"AmexIPT\"," +
                     "\"selected\":\"" + "true" + "\"" +
                     "},"


                     ;

            strInterchange = "{" +
                     "\"tabLabel\":\"DiscIPT\"," +
                     "\"name\":\"DiscIPT\"," +
                     "\"value\":\"" + "x" + "\"" +
                     "}," +
                     "{" +
                     "\"tabLabel\":\"VisaIPT\"," +
                     "\"name\":\"VisaIPT\"," +
                     "\"value\":\"" + "x" + "\"" +
                     "}," +
                     "{" +
                     "\"tabLabel\":\"VisaCheckCardIPT\"," +
                     "\"name\":\"VisaCheckCardIPT\"," +
                     "\"value\":\"" + "x" + "\"" +
                     "}," +
                     "{" +
                     "\"tabLabel\":\"MCIPT\"," +
                     "\"name\":\"MCIPT\"," +
                     "\"value\":\"" + "x" + "\"" +
                     "}," +
                     "{" +
                     "\"tabLabel\":\"MCCheckCardIPT\"," +
                     "\"name\":\"MCCheckCardIPT\"," +
                     "\"value\":\"" + "x" + "\"" +
                     "}," +
                     "{" +
                     "\"tabLabel\":\"AmexIPT\"," +
                     "\"name\":\"AmexIPT\"," +
                     "\"value\":\"" + "x" + "\"" +
                     "}," +

                     "{" +
                     "\"tabLabel\":\"Visa INTL/NS\"," +
                     "\"name\":\"Visa INTL/NS\"," +
                     "\"value\":\"" + "0.00" + "\"" +
                     "}," +
                     "{" +
                     "\"tabLabel\":\"Visa CheckCard INTL/NS\"," +
                     "\"name\":\"Visa CheckCard INTL/NS\"," +
                     "\"value\":\"" + "0.00" + "\"" +
                     "}," +
                     "{" +
                     "\"tabLabel\":\"Visa CheckCard Rate2\"," +
                     "\"name\":\"Visa CheckCard Rate2\"," +
                     "\"value\":\"" + Convert.ToString(dr["DiscRateMidQualDebit"]).Trim() + "\"" +
                     "}," +
                     "{" +
                     "\"tabLabel\":\"Visa INTL/NS\"," +
                     "\"name\":\"Visa INTL/NS\"," +
                     "\"value\":\"" + "0.00" + "\"" +
                     "}," +
                     "{" +
                     "\"tabLabel\":\"MC INTL/NS\"," +
                     "\"name\":\"MC INTL/NS\"," +
                     "\"value\":\"" + "0.00" + "\"" +
                     "}," +
                     "{" +
                     "\"tabLabel\":\"MC CheckCard INTL/NS\"," +
                     "\"name\":\"MC CheckCard INTL/NS\"," +
                     "\"value\":\"" + "0.00" + "\"" +
                     "}," +
                     "{" +
                     "\"tabLabel\":\"MasterCard CheckCard Rate2\"," +
                     "\"name\":\"MasterCard CheckCard Rate2\"," +
                     "\"value\":\"" + Convert.ToString(dr["DiscRateMidQualDebit"]).Trim() + "\"" +
                     "}," +
                     "{" +
                     "\"tabLabel\":\"Disc INTL/NS\"," +
                     "\"name\":\"Disc INTL/NS\"," +
                     "\"value\":\"" + "0.00" + "\"" +
                     "}," +
                     "{" +
                     "\"tabLabel\":\"Visa INTL/NS Surcharge\"," +
                     "\"name\":\"Visa INTL/NS Surcharge\"," +
                     "\"value\":\"" + "0.00" + "\"" +
                     "}," +
                     "{" +
                     "\"tabLabel\":\"Visa CheckCard INTL/NS Surcharge\"," +
                     "\"name\":\"Visa CheckCard INTL/NS Surcharge\"," +
                     "\"value\":\"" + "0.00" + "\"" +
                     "}," +
                     "{" +
                     "\"tabLabel\":\"MC CheckCard INTL/NS Surcharge\"," +
                     "\"name\":\"Visa CheckCard INTL/NS Surcharge\"," +
                     "\"value\":\"" + "0.00" + "\"" +
                     "}," +
                      "{" +
                     "\"tabLabel\":\"MasterCard CheckCard Rate3\"," +
                     "\"name\":\"MasterCard CheckCard Rate3\"," +
                     "\"value\":\"" + Convert.ToString(dr["DiscRateNonQualDebit"]).Trim() + "\"" +
                     "}," +
                     "{" +
                     "\"tabLabel\":\"Visa CheckCard Rate3\"," +
                     "\"name\":\"Visa CheckCard Rate3\"," +
                     "\"value\":\"" + Convert.ToString(dr["DiscRateNonQualDebit"]).Trim() + "\"" +
                     "}," +
                     "{" +
                     "\"tabLabel\":\"Disc INTL/NS Surcharge\"," +
                     "\"name\":\"Disc INTL/NS Surcharge\"," +
                     "\"value\":\"" + "0.00" + "\"" +
                     "},";
        }

        string strAmexApplied = "";
        /*if (dr["AmexApplied"].ToString().Trim() == "0")
        {
            strAmexApplied = "{" +
                    "\"tabLabel\":\"American Express Other\"," +
                    "\"name\":\"American Express Other\"," +
                    "\"value\":\"" + "None" + "\"" +
                    "},";
        }*/
        //else if ((dr["AmexApplied"].ToString().Trim() == "1"))
        {
            strAmexApplied = "{" +
                    "\"tabLabel\":\"American Express Other\"," +
                    "\"name\":\"American Express Other\"," +
                    "\"value\":\"" + "New" + "\"" +
                    "},";

            strAmexApplied = strAmexApplied + "{" +
                  "\"tabLabel\":\"AmexOptBlueContact\"," +
                  "\"name\":\"AmexOptBlueContact\"," +
                  "\"value\":\"" + dr["P1FirstName"].ToString().Trim() + " " + dr["P1LastName"].ToString().Trim() + "\"" +
                  "}," + "{" +
                   "\"tabLabel\":\"AmexOptBlueLegalName\"," +
                   "\"name\":\"AmexOptBlueLegalName\"," +
                   "\"value\":\"" + dr["COMPANYNAME"].ToString().Trim() + "\"" +
                   "},";

            //if (Convert.ToDouble(dr["ProcessPctSwiped"].ToString().Trim()) >= 70)
            {
                strAmexApplied = strAmexApplied + "{" +
                   "\"tabLabel\":\"Amex Rate1\"," +
                   "\"name\":\"Amex Rate1\"," +
                   "\"value\":\"" + Convert.ToString(dr["AmexDiscRateQual"]) + "\"" +
                   "}," +
                   "{" +
                   "\"tabLabel\":\"Amex Rate2\"," +
                   "\"name\":\"Amex Rate2\"," +
                   "\"value\":\"" + Convert.ToString(Convert.ToDecimal(dr["AmexDiscRateMidQual"]) - Convert.ToDecimal(dr["AmexDiscRateQual"])) + "\"" +
                   "}," +
                   "{" +
                   "\"tabLabel\":\"Amex Rate3\"," +
                   "\"name\":\"Amex Rate3\"," +
                   "\"value\":\"" + Convert.ToString(Convert.ToDecimal(dr["AmexDiscRateNonQual"]) - Convert.ToDecimal(dr["AmexDiscRateQual"])) + "\"" +
                   "},"
                   ;
            }/*
            else
            {
                strAmexApplied = strAmexApplied + "{" +
   "\"tabLabel\":\"Amex Discount Rate\"," +
   "\"name\":\"Amex Discount Rate\"," +
   "\"value\":\"" + "3.50" + "\"" +
   "}," +
   "{" +
   "\"tabLabel\":\"Amex Trans Fee\"," +
   "\"name\":\"Amex Trans Fee\"," +
   "\"value\":\"" + "0.15" + "\"" +
   "}," +
   "{" +
   "\"tabLabel\":\"Prepaid Amex Discount Rate\"," +
   "\"name\":\"Prepaid Amex Discount Rate\"," +
   "\"value\":\"" + "2.25" + "\"" +
   "}," +
        "{" +
   "\"tabLabel\":\"Prepaid Amex Trans Fee\"," +
   "\"name\":\"Prepaid Amex Trans Fee\"," +
   "\"value\":\"" + "0.20" + "\"" +
   "},"
   ;
            }*/
        }
        if ((dr["AmexApplied"].ToString().Trim() == "1") && (dr["PrevAmexNum"].ToString().Trim() != ""))
        {
            strAmexApplied = "{" +
                    "\"tabLabel\":\"American Express Other\"," +
                    "\"name\":\"American Express Other\"," +
                    "\"value\":\"" + "Existing" + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"American Express Existing #\"," +
                    "\"name\":\"American Express Existing #\"," +
                    "\"value\":\"" + Convert.ToString(dr["PrevAmexNum"]).Trim() + "\"" +
                    "},";
        }

        string strAmexInitial = "";

        if ((dr["AmexApplied"].ToString().Trim() == "1") && (dr["PrevAmexNum"].ToString().Trim() == ""))
        {

            strAmexInitial = "\"initialHereTabs\": [" +
                         "{" +
                         "\"recipientId\":\"1\"," +
                         "\"pageNumber\":\"2\"," +
                    "\"xPosition\":\"250\"," +
                    "\"yPosition\":\"100\"," +
                    "}," +
                        "],";
        }

        string sageAVS = "";

        if (!Convert.IsDBNull(dr["AVS"]))
        {
            if (Convert.ToString(dr["AVS"]) != "")
            {
                sageAVS = "{" +
                    "\"tabLabel\":\"AVS\"," +
                    "\"name\":\"AVS\"," +
                    "\"value\":\"" + Convert.ToString(dr["AVS"]) + "\"" +
                    "},";
            }
        }



        string strWirelessSetup = "";

        if ((dr["WirelessAccessFee"].ToString() != "") || (dr["WirelessTransFee"].ToString() != ""))
        {
            strWirelessSetup = "{" +
                    "\"tabLabel\":\"Wireless Set Up\"," +
                    "\"name\":\"Wireless Set Up\"," +
                    "\"value\":\"" + "35.00" + "\"" +
                    "}," +
                     "{" +
                     "\"tabLabel\":\"MonthlyWirelessAccessChecked\"," +
                     "\"name\":\"MonthlyWirelessAccessChecked\"," +
                     "\"selected\":\"" + "true" + "\"" +
                     "},";
        }

        string strMobile = "";
        if (dr["Gateway"].ToString().ToLower().Contains("roampay"))
        {
            decimal TransFee = Convert.ToDecimal(dr["TransactionFee"].ToString().Trim()) + Convert.ToDecimal(dr["GatewayTransFee"].ToString().Trim());
            string strTransFee = TransFee.ToString();

            decimal NBCTransFee = Convert.ToDecimal(dr["NBCTransFee"].ToString().Trim()) + Convert.ToDecimal(dr["GatewayTransFee"].ToString().Trim());
            string strNBCTransFee = NBCTransFee.ToString();

            strMobile = "{" +
                    "\"tabLabel\":\"Sage Mobile Payments Setup\"," +
                    "\"name\":\"Sage Mobile Payments Setup\"," +
                    "\"value\":\"" + "25.00" + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Sage Mobile Payments Access\"," +
                    "\"name\":\"Sage Mobile Payments Access\"," +
                    "\"value\":\"" + Convert.ToString(dr["GatewayMonFee"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Visa MC Auth Fee\"," +
                    "\"name\":\"Visa MC Auth Fee\"," +
                    "\"value\":\"" + strTransFee + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Discover Auth Fee\"," +
                    "\"name\":\"Discover Auth Fee\"," +
                    "\"value\":\"" + strNBCTransFee + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"American Express Auth Fee\"," +
                    "\"name\":\"American Express Auth Fee\"," +
                    "\"value\":\"" + strNBCTransFee + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Carte Blanche Auth Fee\"," +
                    "\"name\":\"Carte Blanche Auth Fee\"," +
                    "\"value\":\"" + strNBCTransFee + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Diners Club Auth Fee\"," +
                    "\"name\":\"Diners Club Auth Fee\"," +
                    "\"value\":\"" + strNBCTransFee + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"JCB Auth Fee\"," +
                    "\"name\":\"JCB Auth Fee\"," +
                    "\"value\":\"" + strNBCTransFee + "\"" +
                    "},";
        }
        else
        {
            strMobile = "{" +
                    "\"tabLabel\":\"Visa MC Auth Fee\"," +
                    "\"name\":\"Visa MC Auth Fee\"," +
                    "\"value\":\"" + Convert.ToString(dr["TransactionFee"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Discover Auth Fee\"," +
                    "\"name\":\"Discover Auth Fee\"," +
                    "\"value\":\"" + Convert.ToString(dr["NBCTransFee"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"American Express Auth Fee\"," +
                    "\"name\":\"American Express Auth Fee\"," +
                    "\"value\":\"" + Convert.ToString(dr["NBCTransFee"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Carte Blanche Auth Fee\"," +
                    "\"name\":\"Carte Blanche Auth Fee\"," +
                    "\"value\":\"" + Convert.ToString(dr["NBCTransFee"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Diners Club Auth Fee\"," +
                    "\"name\":\"Diners Club Auth Fee\"," +
                    "\"value\":\"" + Convert.ToString(dr["NBCTransFee"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"JCB Auth Fee\"," +
                    "\"name\":\"JCB Auth Fee\"," +
                    "\"value\":\"" + Convert.ToString(dr["NBCTransFee"]).Trim() + "\"" +
                    "},";
        }

        string strGatewayAccess = "";
        if (dr["Gateway"].ToString().Contains("Sage") || dr["Gateway"].ToString().Contains("Roam"))
        {
            strGatewayAccess = "{" +
                    "\"tabLabel\":\"Gateway Access\"," +
                    "\"name\":\"Gateway Access\"," +
                    "\"value\":\"" + Convert.ToString(dr["GatewayMonFee"]).Trim() + "\"" +
                    "},";
        }

        string strAnnualFeeCP = "";
        if (dr["AnnualFeeCP"].ToString().Trim() != "")
        {
            strAnnualFeeCP = "{" +
                   "\"tabLabel\":\"Annual Assessment\"," +
                   "\"name\":\"Annual Assessment\"," +
                   "\"value\":\"" + Convert.ToString(dr["AnnualFeeCP"]).Trim() + "\"" +
                   "},";
        }
        else
        {
            strAnnualFeeCP = "{" +
                   "\"tabLabel\":\"Annual Assessment\"," +
                   "\"name\":\"Annual Assessment\"," +
                   "\"value\":\"" + Convert.ToString(dr["AnnualFeeCNP"]).Trim() + "\"" +
                   "},";
        }


        string strDiscountPaid = "";

        if (Convert.ToString(dr["DiscountPaid"]).Trim() == "Daily")
        {

            strDiscountPaid = "{" +
                   "\"tabLabel\":\"DailyDisc\"," +
                   "\"name\":\"DailyDisc\"," +
                   "\"selected\":\"" + "true" + "\"" +
                   "},";
        }
        else if (Convert.ToString(dr["DiscountPaid"]).Trim() == "Monthly")
        {
            strDiscountPaid = "{" +
                   "\"tabLabel\":\"MonthlyDisc\"," +
                   "\"name\":\"MonthlyDisc\"," +
                   "\"selected\":\"" + "true" + "\"" +
                   "},";
        }


        string DebitChecked = "";
        if (Convert.ToString(dr["DebitMonFee"]).Trim() != "")
        {

            strCheckBox = strCheckBox + "{" +
                       "\"tabLabel\":\"MonthlyDebitAccessChecked\"," +
                       "\"name\":\"MonthlyDebitAccessChecked\"," +
                       "\"selected\":\"" + "true" + "\"" +
                       "},";

            DebitChecked = "{" +
    "\"tabLabel\":\"MonthlyDebitAccessChecked\"," +
    "\"name\":\"MonthlyDebitAccessChecked\"," +
    "\"value\":\"" + "x" + "\"" +
    "},";
        }

        string strTerminalType = "";

        if (dr["Gateway"].ToString().Trim() != "")
        {
            strTerminalType = "{" +
                   "\"tabLabel\":\"Terminal/Software Type\"," +
                   "\"name\":\"Terminal/Software Type\"," +
                   "\"value\":\"" + Convert.ToString(dr["Gateway"]).Trim() + "\"" +
                   "},";
        }
        else
        {
            strTerminalType = "{" +
                   "\"tabLabel\":\"Terminal/Software Type\"," +
                   "\"name\":\"Terminal/Software Type\"," +
                   "\"value\":\"" + Convert.ToString(dr["Equipment"]).Trim() + "\"" +
                   "},";
        }

        string test = strMonthToMonth;




        string requestBody = "{" +
                    "\"status\":\"sent\"," +
                    "\"emailBlurb\":\"Please review and sign your document with Commerce Technologies.\"," +
                    "\"emailSubject\": \"Please review and sign your document with Commerce Technologies.\"," +
                    "\"templateId\":\"" + templateId + "\"," +
                    "\"templateRoles\":[" +

                    "{" +
                    "\"email\":\"" + Convert.ToString(dr["P1Email"]).Trim() + "\"," +
                    "\"name\":\"" + Convert.ToString(dr["P1FirstName"]).Trim() + " " + Convert.ToString(dr["P1LastName"]).Trim() + "\"," +
                    "\"roleName\":\"Signer\"," +
                    "\"clientUserId\":\"" + Convert.ToString(dr["P1FirstName"]).Trim() + " " + Convert.ToString(dr["P1LastName"]).Trim() + "\"," +
                    "\"tabs\": {" +
                         "\"checkboxTabs\": [" +
                         strCheckBox +
                         strDiscountPaid +
                        "]," +
                    "\"textTabs\": [" +
                    "{" +
                    "\"tabLabel\":\"Authorization Network\"," +
                    "\"name\":\"Authorization Network\"," +
                    "\"value\":\"" + platForm + "\"" +
                    "}," +
                     "{" +
                    "\"tabLabel\":\"Contractor Name\"," +
                    "\"name\":\"Contractor Name\"," +
                    "\"value\":\"" + Convert.ToString(dr["RepName"]).Trim() + "\"" +
                    "}," +
                     "{" +
                    "\"tabLabel\":\"Contractor Name2\"," +
                    "\"name\":\"Contractor Name2\"," +
                    "\"value\":\"" + Convert.ToString(dr["RepName"]).Trim() + "\"" +
                    "}," +
                    sageAVS +
                    DebitChecked +
                    "{" +
                    "\"tabLabel\":\"Bus Info Legal Business Name\"," +
                    "\"name\":\"Bus Info Legal Business Name\"," +
                    "\"value\":\"" + Convert.ToString(dr["COMPANYNAME"]).Trim() + "\"" +
                    "}," +

                     "{" +
                     "\"tabLabel\":\"PCIComplianceChecked\"," +
                     "\"name\":\"PCIComplianceChecked\"," +
                     "\"value\":\"" + "x" + "\"" +
                     "}," +
                    "{" +
                    "\"tabLabel\":\"Merch Legal Business Name\"," +
                    "\"name\":\"Merch Legal Business Name\"," +
                    "\"value\":\"" + Convert.ToString(dr["COMPANYNAME"]).Trim() + "\"" +
                    "}," +
                     "{" +
                    "\"tabLabel\":\"MA_Address\"," +
                    "\"name\":\"MA_Address\"," +
                    "\"value\":\"" + Convert.ToString(dr["BillingAddress"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Merch MA_Address\"," +
                    "\"name\":\"Merch MA_Address\"," +
                    "\"value\":\"" + Convert.ToString(dr["BillingAddress"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Merch MA_City\"," +
                    "\"name\":\"Merch MA_City\"," +
                    "\"value\":\"" + Convert.ToString(dr["BillingCity"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"MA_City\"," +
                    "\"name\":\"MA_City\"," +
                    "\"value\":\"" + Convert.ToString(dr["BillingCity"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Merch MA_State\"," +
                    "\"name\":\"Merch MA_State\"," +
                    "\"value\":\"" + Convert.ToString(dr["BillingState"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"MA_State\"," +
                    "\"name\":\"MA_State\"," +
                    "\"value\":\"" + Convert.ToString(dr["BillingState"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"MA_Zip\"," +
                    "\"name\":\"MA_Zip\"," +
                    "\"value\":\"" + Convert.ToString(dr["BillingZipCode"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Merch MA_Zip\"," +
                    "\"name\":\"Merch MA_Zip\"," +
                    "\"value\":\"" + Convert.ToString(dr["BillingZipCode"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Contact Name\"," +
                    "\"name\":\"Contact Name\"," +
                    "\"value\":\"" + Convert.ToString(dr["ContactName"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Phone\"," +
                    "\"name\":\"Phone\"," +
                    "\"value\":\"" + Convert.ToString(dr["BusinessPhone"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Phone 2\"," +
                    "\"name\":\"Phone 2\"," +
                    "\"value\":\"" + Convert.ToString(dr["BusinessPhone"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Email\"," +
                    "\"name\":\"Email\"," +
                    "\"value\":\"" + Convert.ToString(dr["Email"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Legal Business Name\"," +
                    "\"name\":\"Legal Business Name\"," +
                    "\"value\":\"" + Convert.ToString(dr["COMPANYNAME"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Business Open Date\"," +
                    "\"name\":\"Business Open Date\"," +
                    "\"value\":\"" + BusinessOpenMonth + "/" + BusinessOpenYear + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Length Of Ownership\"," +
                    "\"name\":\"Length Of Ownership\"," +
                    "\"value\":\"" + LengthOfOwnership + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Existing Sage MID\"," +
                    "\"name\":\"Existing Sage MID\"," +
                    "\"value\":\"" + prevProcNum + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Business Name DBA\"," +
                    "\"name\":\"Business Name DBA\"," +
                    "\"value\":\"" + Convert.ToString(dr["DBA"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Address\"," +
                    "\"name\":\"Address\"," +
                    "\"value\":\"" + Convert.ToString(dr["Address"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"City\"," +
                    "\"name\":\"City\"," +
                    "\"value\":\"" + Convert.ToString(dr["CITY"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"State\"," +
                    "\"name\":\"State\"," +
                    "\"value\":\"" + Convert.ToString(dr["STATE"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Zip\"," +
                    "\"name\":\"Zip\"," +
                    "\"value\":\"" + Convert.ToString(dr["ZipCode"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Phone_2\"," +
                    "\"name\":\"Phone_2\"," +
                    "\"value\":\"" + Convert.ToString(dr["BusinessPhone"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Fax_2\"," +
                    "\"name\":\"Fax_2\"," +
                    "\"value\":\"" + Convert.ToString(dr["Fax"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Web Site\"," +
                    "\"name\":\"Web Site\"," +
                    "\"value\":\"" + Convert.ToString(dr["Website"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Customer Service Phone\"," +
                    "\"name\":\"Customer Service Phone\"," +
                    "\"value\":\"" + Convert.ToString(dr["CustServPhone"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Number of Locations\"," +
                    "\"name\":\"Number of Locations\"," +
                    "\"value\":\"" + Convert.ToString(dr["NumberofLocations"]).Trim() + "\"" +
                    "}," +
                    strOwnershipType +
                    "{" +
                    "\"tabLabel\":\"Fed Tax ID\"," +
                    "\"name\":\"Fed Tax ID\"," +
                    "\"value\":\"" + Convert.ToString(dr["FederalTaxID"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"P1 Ownership%\"," +
                    "\"name\":\"P1 Ownership%\"," +
                    "\"value\":\"" + Convert.ToString(dr["P1OwnershipPercent"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"P1 Last Name\"," +
                    "\"name\":\"P1 Last Name\"," +
                    "\"value\":\"" + Convert.ToString(dr["P1LastName"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"P1 First Name\"," +
                    "\"name\":\"P1 First Name\"," +
                    "\"value\":\"" + Convert.ToString(dr["P1FirstName"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"P1 Full Name\"," +
                    "\"name\":\"P1 Full Name\"," +
                    "\"value\":\"" + Convert.ToString(dr["P1FirstName"]).Trim() + " " + Convert.ToString(dr["P1LastName"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"P1 Full Name 2\"," +
                    "\"name\":\"P1 Full Name 2\"," +
                    "\"value\":\"" + Convert.ToString(dr["P1FirstName"]).Trim() + " " + Convert.ToString(dr["P1LastName"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"P1 Full Name 3\"," +
                    "\"name\":\"P1 Full Name 3\"," +
                    "\"value\":\"" + Convert.ToString(dr["P1FirstName"]).Trim() + " " + Convert.ToString(dr["P1LastName"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"P1 Full Name 4\"," +
                    "\"name\":\"P1 Full Name 4\"," +
                    "\"value\":\"" + Convert.ToString(dr["P1FirstName"]).Trim() + " " + Convert.ToString(dr["P1LastName"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"P1 Title\"," +
                    "\"name\":\"P1 Title\"," +
                    "\"value\":\"" + Convert.ToString(dr["P1Title"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"P1 Title 2\"," +
                    "\"name\":\"P1 Title 2\"," +
                    "\"value\":\"" + Convert.ToString(dr["P1Title"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"P1 Title 3\"," +
                    "\"name\":\"P1 Title 3\"," +
                    "\"value\":\"" + Convert.ToString(dr["P1Title"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"P1 Address\"," +
                    "\"name\":\"P1 Address\"," +
                    "\"value\":\"" + Convert.ToString(dr["P1Address"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"P1 Zip\"," +
                    "\"name\":\"P1 Zip\"," +
                    "\"value\":\"" + Convert.ToString(dr["P1ZipCode"]).Trim() + "\"" +
                    "}," +
                     "{" +
                    "\"tabLabel\":\"P1 City, State\"," +
                    "\"name\":\"P1 City, State\"," +
                    "\"value\":\"" + Convert.ToString(dr["P1City"]).Trim() + ", " +
                        Convert.ToString(dr["P1State"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"P1 City, State, Zip\"," +
                    "\"name\":\"P1 City, State, Zip\"," +
                    "\"value\":\"" + Convert.ToString(dr["P1City"]).Trim() + ", " +
                        Convert.ToString(dr["P1State"]).Trim() + " " + Convert.ToString(dr["P1ZipCode"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"P1 Phone\"," +
                    "\"name\":\"P1 Phone\"," +
                    "\"value\":\"" + Convert.ToString(dr["P1PhoneNumber"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"P1 Email\"," +
                    "\"name\":\"P1 Email\"," +
                    "\"value\":\"" + Convert.ToString(dr["P1Email"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"P1 SSN\"," +
                    "\"name\":\"P1 SSN\"," +
                    "\"value\":\"" + Convert.ToString(dr["P1SSN"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"P1 DOB\"," +
                    "\"name\":\"P1 DOB\"," +
                    "\"value\":\"" + Convert.ToString(dr["P1DOB"]).Trim() + "\"" +
                    "}," +
                    strP2 +
                    strBusinessType +
                    strReturnPolicy +
                    "{" +
                    "\"tabLabel\":\"Products Sold\"," +
                    "\"name\":\"Products Sold\"," +
                    "\"value\":\"" + Convert.ToString(dr["ProductSold"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Monthly Volume\"," +
                    "\"name\":\"Monthly Volume\"," +
                    "\"value\":\"" + Convert.ToString(dr["MonthlyVolume"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Average Ticket\"," +
                    "\"name\":\"Average Ticket\"," +
                    "\"value\":\"" + Convert.ToString(dr["AverageTicket"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Highest Ticket\"," +
                    "\"name\":\"Highest Ticket\"," +
                    "\"value\":\"" + Convert.ToString(dr["MaxTicket"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Discount Paid Monthly\"," +
                    "\"name\":\"Discount Paid Monthly\"," +
                    "\"value\":\"" + "x" + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Current Processor\"," +
                    "\"name\":\"Current Processor\"," +
                    "\"value\":\"" + Convert.ToString(dr["PrevProcessor"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Card Present Swiped\"," +
                    "\"name\":\"Card Present Swiped\"," +
                    "\"value\":\"" + Convert.ToString(dr["ProcessPctSwiped"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Card Present Imprint\"," +
                    "\"name\":\"Card Present Imprint\"," +
                    "\"value\":\"" + Convert.ToString(dr["ProcessPctKeyedWImprint"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Card Not Present\"," +
                    "\"name\":\"Card Not Present\"," +
                    "\"value\":\"" + Convert.ToString(dr["ProcessPctKeyedWoImprint"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Days Until Product Delivery\"," +
                    "\"name\":\"Days Until Product Delivery\"," +
                    "\"value\":\"" + Convert.ToString(dr["NumDaysDelivered"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"ACH Bank Name\"," +
                    "\"name\":\"ACH Bank Name\"," +
                    "\"value\":\"" + Convert.ToString(dr["BankName"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"ACH Address 1\"," +
                    "\"name\":\"ACH Address 1\"," +
                    "\"value\":\"" + Convert.ToString(dr["BankAddress"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"ACH City\"," +
                    "\"name\":\"ACH City\"," +
                    "\"value\":\"" + Convert.ToString(dr["BankCity"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"ACH State\"," +
                    "\"name\":\"ACH State\"," +
                    "\"value\":\"" + Convert.ToString(dr["BankState"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"ACH Zip\"," +
                    "\"name\":\"ACH Zip\"," +
                    "\"value\":\"" + Convert.ToString(dr["BankZip"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"ACH Routing Number\"," +
                    "\"name\":\"ACH Routing Number\"," +
                    "\"value\":\"" + Convert.ToString(dr["BankRoutingNumber"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"ACH Account Number\"," +
                    "\"name\":\"ACH Account Number\"," +
                    "\"value\":\"" + Convert.ToString(dr["BankAccountNumber"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Visa Rate1\"," +
                    "\"name\":\"Visa Rate1\"," +
                    "\"value\":\"" + Convert.ToString(dr["DiscountRate"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Visa CheckCard Rate1\"," +
                    "\"name\":\"Visa CheckCard Rate1\"," +
                    "\"value\":\"" + Convert.ToString(dr["DiscountRateDebit"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"MasterCard Rate1\"," +
                    "\"name\":\"MasterCard Rate1\"," +
                    "\"value\":\"" + Convert.ToString(dr["DiscountRate"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"MasterCard CheckCard Rate1\"," +
                    "\"name\":\"MasterCard CheckCard Rate1\"," +
                    "\"value\":\"" + Convert.ToString(dr["DiscountRateDebit"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Disc Rate1\"," +
                    "\"name\":\"Disc Rate1\"," +
                    "\"value\":\"" + Convert.ToString(dr["DiscountRate"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Visa Rate2\"," +
                    "\"name\":\"Visa Rate2\"," +
                    "\"value\":\"" + Convert.ToString(dr["DiscRateMidQual"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Visa CheckCard Rate2\"," +
                    "\"name\":\"Visa CheckCard Rate2\"," +
                    "\"value\":\"" + Convert.ToString(dr["DiscRateMidQualDebit"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"MasterCard Rate2\"," +
                    "\"name\":\"MasterCard Rate2\"," +
                    "\"value\":\"" + Convert.ToString(dr["DiscRateMidQual"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"MasterCard CheckCard Rate2\"," +
                    "\"name\":\"MasterCard CheckCard Rate2\"," +
                    "\"value\":\"" + Convert.ToString(dr["DiscRateMidQualDebit"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Disc Rate2\"," +
                    "\"name\":\"Disc Rate2\"," +
                    "\"value\":\"" + Convert.ToString(dr["DiscRateMidQual"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Visa Rate3\"," +
                    "\"name\":\"Visa Rate3\"," +
                    "\"value\":\"" + Convert.ToString(dr["DiscRateNonQual"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Visa CheckCard Rate3\"," +
                    "\"name\":\"Visa CheckCard Rate3\"," +
                    "\"value\":\"" + Convert.ToString(dr["DiscRateNonQualDebit"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"MasterCard Rate3\"," +
                    "\"name\":\"MasterCard Rate3\"," +
                    "\"value\":\"" + Convert.ToString(dr["DiscRateNonQual"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"MasterCard CheckCard Rate3\"," +
                    "\"name\":\"MasterCard CheckCard Rate3\"," +
                    "\"value\":\"" + Convert.ToString(dr["DiscRateNonQualDebit"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Disc Rate3\"," +
                    "\"name\":\"Disc Rate3\"," +
                    "\"value\":\"" + Convert.ToString(dr["DiscRateNonQual"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Visa INTL/NS\"," +
                    "\"name\":\"Visa INTL/NS\"," +
                    "\"value\":\"" + "1.85" + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Visa CheckCard INTL/NS\"," +
                    "\"name\":\"Visa CheckCard INTL/NS\"," +
                    "\"value\":\"" + "1.85" + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"MC INTL/NS\"," +
                    "\"name\":\"MC INTL/NS\"," +
                    "\"value\":\"" + "1.65" + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"MC CheckCard INTL/NS\"," +
                    "\"name\":\"MC CheckCard INTL/NS\"," +
                    "\"value\":\"" + "1.65" + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Disc INTL/NS\"," +
                    "\"name\":\"Disc INTL/NS\"," +
                    "\"value\":\"" + "1.65" + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Amex INTL/NS\"," +
                    "\"name\":\"Amex INTL/NS\"," +
                    "\"value\":\"" + "1.00" + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Visa INTL/NS Surcharge\"," +
                    "\"name\":\"Visa INTL/NS Surcharge\"," +
                    "\"value\":\"" + "0.20" + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Visa CheckCard INTL/NS Surcharge\"," +
                    "\"name\":\"Visa CheckCard INTL/NS Surcharge\"," +
                    "\"value\":\"" + "0.20" + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"MC INTL/NS Surcharge\"," +
                    "\"name\":\"MC INTL/NS Surcharge\"," +
                    "\"value\":\"" + "0.20" + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"MC CheckCard INTL/NS Surcharge\"," +
                    "\"name\":\"MC CheckCard INTL/NS Surcharge\"," +
                    "\"value\":\"" + "0.20" + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Disc INTL/NS Surcharge\"," +
                    "\"name\":\"Disc INTL/NS Surcharge\"," +
                    "\"value\":\"" + "0.20" + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Amex INTL/NS Surcharge\"," +
                    "\"name\":\"Amex INTL/NS Surcharge\"," +
                    "\"value\":\"" + "0.00" + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Disc Business\"," +
                    "\"name\":\"Disc Business\"," +
                    "\"value\":\"" + Convert.ToString(dr["DiscRateNonQual"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Visa Business\"," +
                    "\"name\":\"Visa Business\"," +
                    "\"value\":\"" + Convert.ToString(dr["DiscRateNonQual"]).Trim() + "\"" +
                    "}," +

                    "{" +
                    "\"tabLabel\":\"Visa CheckCard Business\"," +
                    "\"name\":\"Visa CheckCard Business\"," +
                    "\"value\":\"" + Convert.ToString(dr["DiscRateNonQual"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"MC Business\"," +
                    "\"name\":\"MC Business\"," +
                    "\"value\":\"" + Convert.ToString(dr["DiscRateNonQual"]).Trim() + "\"" +
                    "}," +
                     "{" +
                    "\"tabLabel\":\"MC CheckCard Business\"," +
                    "\"name\":\"MC CheckCard Business\"," +
                    "\"value\":\"" + Convert.ToString(dr["DiscRateNonQual"]).Trim() + "\"" +
                    "}," +
                    strInterchange +
                    strAmexApplied +
                    strMonthToMonth +
                    "{" +
                    "\"tabLabel\":\"ARU Auth Fee\"," +
                    "\"name\":\"ARU Auth Fee\"," +
                    "\"value\":\"" + Convert.ToString(dr["VoiceAuth"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"EBT Auth Fee\"," +
                    "\"name\":\"EBT Auth Fee\"," +
                    "\"value\":\"" + Convert.ToString(dr["EBTTransFee"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"PIN Debit Auth Fee\"," +
                    "\"name\":\"PIN Debit Auth Fee\"," +
                    "\"value\":\"" + Convert.ToString(dr["DebitTransFee"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Voice Authorization\"," +
                    "\"name\":\"Voice Authorization\"," +
                    "\"value\":\"" + Convert.ToString(dr["VoiceAuth"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Application Credit\"," +
                    "\"name\":\"Application Credit\"," +
                    "\"value\":\"" + Convert.ToString(dr["AppFee"]).Trim() + "\"" +
                    "}," +
                    strWirelessSetup +
                    strMobile +
                    "{" +
                    "\"tabLabel\":\"Statement\"," +
                    "\"name\":\"Statement\"," +
                    "\"value\":\"" + "0.00" + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Monthly Support\"," +
                    "\"name\":\"Monthly Support\"," +
                    "\"value\":\"" + Convert.ToString(dr["CustServFee"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Monthly Minimum\"," +
                    "\"name\":\"Monthly Minimum\"," +
                    "\"value\":\"" + Convert.ToString(dr["MonMin"]).Trim() + "\"" +
                    "}," +
                    strGatewayAccess +
                    "{" +
                    "\"tabLabel\":\"Debit Access\"," +
                    "\"name\":\"Debit Access\"," +
                    "\"value\":\"" + Convert.ToString(dr["DebitMonFee"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Wireless Access\"," +
                    "\"name\":\"Wireless Access\"," +
                    "\"value\":\"" + Convert.ToString(dr["WirelessAccessFee"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Chargeback\"," +
                    "\"name\":\"Chargeback\"," +
                    "\"value\":\"" + Convert.ToString(dr["ChargebackFee"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Signature Rate\"," +
                    "\"name\":\"Signature Rate\"," +
                    "\"value\":\"" + Convert.ToString(dr["DiscRateQualDebit"]).Trim() + "\"" +
                    "}," +
                    strAnnualFeeCP +
                    strGatewayAccess +
                    strTerminalType +
                    strDebit +
                    "]," +
                    "}" +
                    "}," +
                    strCoSigner +


                    "]" +
                    "}";

        return requestBody;
    }

    protected string iPayDocusignRequest(DataRow dr)
    {
        string requestBody = "";
        string strCheckBox = "";

        try
        {
            //string templateId = "94CBDFF5-C37F-4951-9BE8-5F43C1331846";
            string templateId = "F217DC26-12AE-4C67-A566-F15EC9076DDC";
            if ((dr["AmexNum"].ToString().Trim() != "") && (dr["AmexNum"].ToString().Trim() != "No"))
            {
                if (dr["AmexNum"].ToString().Trim() == "Yes" || Convert.ToDouble(dr["ProcessPctSwiped"].ToString().Trim()) >= 70)
                {
                    //templateId = "008E6DD7-9C84-4D8C-AAF3-659CABDC3722";
                    templateId = "F217DC26-12AE-4C67-A566-F15EC9076DDC";
                    //templateId = "F3C560F4-307E-48DC-840F-34828C294D6B";
                }
            }
            string strCoSigner = "";
            if (!Convert.IsDBNull(dr["P2FirstName"]))
            {
                if (dr["P2FirstName"].ToString().Trim() != "")
                {
                    templateId = "03F18065-7B5A-4507-88E9-E2C9263CFDDD";
                    //templateId = "B903078C-AFC2-4577-8E88-E7CF147E438A";
                    if ((dr["AmexNum"].ToString().Trim() != "") && (dr["AmexNum"].ToString().Trim() != "No"))
                    {
                        if (dr["AmexNum"].ToString().Trim() == "Yes" || Convert.ToDouble(dr["ProcessPctSwiped"].ToString().Trim()) >= 70)
                        {
                            //templateId = "6B2EDC25-A012-4099-9C67-51186C9F6F4C";
                            templateId = "03F18065-7B5A-4507-88E9-E2C9263CFDDD";
                            //templateId = "E142A498-88F4-4D61-B91D-B0906B2B5860";
                        }
                    }

                    strCoSigner =
                        "{" +
                        "\"email\":\"" + Convert.ToString(dr["P2Email"]).Trim() + "\"," +
                        "\"name\":\"" + Convert.ToString(dr["P2FirstName"]).Trim() + " " + Convert.ToString(dr["P2LastName"]).Trim() + "\"," +
                        "\"roleName\":\"CoSigner\"," +
                        "}";
                }
            }

            string strSSNorFedTaxID = "";

            if ((dr["FederalTaxID"].ToString().Trim() != "") && (dr["P1SSN"].ToString().Trim() != ""))
            {
                if ((dr["FederalTaxID"].ToString().Trim() == null) || (dr["FederalTaxID"].ToString().Trim() == dr["P1SSN"].ToString().Trim()))
                {
                    strCheckBox = strCheckBox + "{" +
                       "\"tabLabel\":\"app.SSNCheckbox\"," +
                       "\"name\":\"app.SSNCheckbox\"," +
                       "\"selected\":\"" + "true" + "\"" +
                       "},";

                    strSSNorFedTaxID = "{" +
                       "\"tabLabel\":\"app.SSNCheckbox\"," +
                       "\"name\":\"app.SSNCheckbox\"," +
                       "\"value\":\"" + "x" + "\"" +
                       "}," +
                       "{" +
                       "\"tabLabel\":\"app.SSNorTaxID\"," +
                       "\"name\":\"app.SSNorTaxID\"," +
                       "\"value\":\"" + dr["P1SSN"].ToString().Trim() + "\"" +
                       "},";
                }
                else
                {
                    strCheckBox = strCheckBox + "{" +
                       "\"tabLabel\":\"app.EINCheckbox\"," +
                       "\"name\":\"app.EINCheckbox\"," +
                       "\"selected\":\"" + "true" + "\"" +
                       "},";

                    strSSNorFedTaxID = "{" +
                       "\"tabLabel\":\"app.EINCheckbox\"," +
                       "\"name\":\"app.EINCheckbox\"," +
                       "\"value\":\"" + "x" + "\"" +
                       "}," +
                       "{" +
                       "\"tabLabel\":\"app.TaxID\"," +
                       "\"name\":\"app.TaxID\"," +
                       "\"value\":\"" + dr["FederalTaxID"].ToString().Trim() + "\"" +
                       "}," +
                       "{" +
                       "\"tabLabel\":\"app.TaxID2\"," +
                       "\"name\":\"app.TaxID2\"," +
                       "\"value\":\"" + dr["FederalTaxID"].ToString().Trim() + "\"" +
                       "},";
                }
            }

            string strchkCTMFYes = "";
            string strchkCTMFNo = "";
            string strchkPrevProcessedYes = "";
            string strchkPrevProcessedNo = "";
            string strchkInterchangeAmexChk = "";
            string strchkInterchangePlusChk = "";
            string strchkTieredAmexChk = "";
            string strchkTieredPlusChk = "";
            string strchkReprogram = "";

            if (dr["Gateway"].ToString().Trim() == "Authorize.net")
            {
                strCheckBox = strCheckBox + "{" +
                       "\"tabLabel\":\"app.chkReprogram\"," +
                       "\"name\":\"app.chkReprogram\"," +
                       "\"selected\":\"" + "true" + "\"" +
                       "},";
                strchkReprogram = "{" +
                       "\"tabLabel\":\"app.chkReprogram\"," +
                       "\"name\":\"app.chkReprogram\"," +
                       "\"value\":\"" + "x" + "\"" +
                       "},";
            }

            if (dr["CTMF"].ToString().Trim() == "Yes")
            {
                strCheckBox = strCheckBox + "{" +
                       "\"tabLabel\":\"app.chkCTMFYes\"," +
                       "\"name\":\"app.chkCTMFYes\"," +
                       "\"selected\":\"" + "true" + "\"" +
                       "},";

                strchkCTMFYes = "{" +
                  "\"tabLabel\":\"app.chkCTMFYes\"," +
                  "\"name\":\"app.chkCTMFYes\"," +
                  "\"value\":\"" + "x" + "\"" +
                  "},";

            }
            else
            {
                strCheckBox = strCheckBox + "{" +
                       "\"tabLabel\":\"app.chkCTMFNo\"," +
                       "\"name\":\"app.chkCTMFNo\"," +
                       "\"selected\":\"" + "true" + "\"" +
                       "},";

                strchkCTMFNo = "{" +
                   "\"tabLabel\":\"app.chkCTMFNo\"," +
                   "\"name\":\"app.chkCTMFNo\"," +
                   "\"value\":\"" + "x" + "\"" +
                   "},";
            }

            if (dr["PrevProcessed"].ToString().Trim() == "Yes")
            {
                strCheckBox = strCheckBox + "{" +
                       "\"tabLabel\":\"app.chkPrevProcessedYes\"," +
                       "\"name\":\"app.chkPrevProcessedYes\"," +
                       "\"selected\":\"" + "true" + "\"" +
                       "},";

                strchkPrevProcessedYes = "{" +
                       "\"tabLabel\":\"app.chkPrevProcessedYes\"," +
                       "\"name\":\"app.chkPrevProcessedYes\"," +
                       "\"value\":\"" + "x" + "\"" +
                       "},";
            }
            else
            {
                strCheckBox = strCheckBox + "{" +
                       "\"tabLabel\":\"app.chkPrevProcessedNo\"," +
                       "\"name\":\"app.chkPrevProcessedNo\"," +
                       "\"selected\":\"" + "true" + "\"" +
                       "},";

                strchkPrevProcessedNo = "{" +
                       "\"tabLabel\":\"app.chkPrevProcessedNo\"," +
                       "\"name\":\"app.chkPrevProcessedNo\"," +
                       "\"value\":\"" + "x" + "\"" +
                       "},";
            }
            if (dr["Interchange"].ToString().Trim() == "True")
            {
                strCheckBox = strCheckBox + "{" +
                       "\"tabLabel\":\"app.InterchangeAmexChk\"," +
                       "\"name\":\"app.InterchangeAmexChk\"," +
                       "\"selected\":\"" + "true" + "\"" +
                       "},";
                strchkInterchangeAmexChk = "{" +
                       "\"tabLabel\":\"app.InterchangeAmexChk\"," +
                       "\"name\":\"app.InterchangeAmexChk\"," +
                       "\"value\":\"" + "x" + "\"" +
                       "},";
                strCheckBox = strCheckBox + "{" +
                        "\"tabLabel\":\"app.InterchangePlusChk\"," +
                        "\"name\":\"app.InterchangePlusChk\"," +
                        "\"selected\":\"" + "true" + "\"" +
                        "},";
                strchkInterchangePlusChk = "{" +
                       "\"tabLabel\":\"app.InterchangePlusChk\"," +
                       "\"name\":\"app.InterchangePlusChk\"," +
                       "\"value\":\"" + "x" + "\"" +
                       "},";
            }
            else
            {
                strCheckBox = strCheckBox + "{" +
                       "\"tabLabel\":\"app.TieredAmexChk\"," +
                       "\"name\":\"app.TieredAmexChk\"," +
                       "\"selected\":\"" + "true" + "\"" +
                       "},";
                strchkTieredAmexChk = "{" +
                       "\"tabLabel\":\"app.TieredAmexChk\"," +
                       "\"name\":\"app.TieredAmexChk\"," +
                       "\"value\":\"" + "x" + "\"" +
                       "},";
                strCheckBox = strCheckBox + "{" +
                    "\"tabLabel\":\"app.TieredPlusChk\"," +
                    "\"name\":\"app.TieredPlusChk\"," +
                    "\"selected\":\"" + "true" + "\"" +
                    "},";
                strchkTieredPlusChk = "{" +
                       "\"tabLabel\":\"app.TieredPlusChk\"," +
                       "\"name\":\"app.TieredPlusChk\"," +
                       "\"value\":\"" + "x" + "\"" +
                       "},";
            }

            string strComment1 = "";
            //"Discount billing type: " + Convert.ToString(dr["DiscountPaid"]).Trim() + " ; " + " Max ticket: " + Convert.ToString(dr["MaxTicket"]).Trim() + ";";

            /*if (!(dr["Interchange"].ToString().Trim() == "True"))
            {
                strComment1 = strComment1 + " plus Dues & Assessments";
            }*/

            if (!Convert.IsDBNull(dr["ContractTerm"]))
            {
                if (Convert.ToString(dr["ContractTerm"]).Trim() == "1 Month")
                {
                    strComment1 = strComment1 + Convert.ToString(dr["ContractTerm"]) + " contract";
                }
                else if (Convert.ToString(dr["ContractTerm"]).Trim() == "1 year")
                {
                    strComment1 = strComment1 + Convert.ToString(dr["ContractTerm"]) + " contract";
                }

                strComment1 = strComment1 + Convert.ToString(dr["ContractTerm"]) + " contract";
            }




            string strComment = "{" +
                       "\"tabLabel\":\"Text220\"," +
                       "\"name\":\"Text220\"," +
                       "\"value\":\"" + strComment1 + "\"" +
                       "},";


            string strchkRefund30Days = "";
            string strchkExchangeOnly = "";
            string strchkRefundOther = "";
            string strOtherRefund = "";
            string strDebitCard = "";


            if ((dr["DebitMonFee"].ToString().Trim() != "") && (dr["DebitTransFee"].ToString().Trim() != ""))
            {
                strCheckBox = strCheckBox + "{" +
                       "\"tabLabel\":\"app.chkDebitCard\"," +
                       "\"name\":\"app.chkDebitCard\"," +
                       "\"selected\":\"" + "true" + "\"" +
                       "},";

                strDebitCard = "{" +
                       "\"tabLabel\":\"app.chkDebitCard\"," +
                       "\"name\":\"app.chkDebitCard\"," +
                       "\"value\":\"" + "x" + "\"" +
                       "},";
            }

            if (dr["RefundPolicy"].ToString().Trim() == "Refund within 30 days")
            {
                strCheckBox = strCheckBox + "{" +
                       "\"tabLabel\":\"app.chkRefund30Days\"," +
                       "\"name\":\"app.chkRefund30Days\"," +
                       "\"selected\":\"" + "true" + "\"" +
                       "},";

                strchkRefund30Days = "{" +
                       "\"tabLabel\":\"app.chkRefund30Days\"," +
                       "\"name\":\"app.chkRefund30Days\"," +
                       "\"value\":\"" + "x" + "\"" +
                       "},";
            }
            else if (dr["RefundPolicy"].ToString().Trim() == "Exchange Only")
            {
                strCheckBox = strCheckBox + "{" +
                       "\"tabLabel\":\"app.chkExchangeOnly\"," +
                       "\"name\":\"app.chkExchangeOnly\"," +
                       "\"selected\":\"" + "true" + "\"" +
                       "},";

                strchkExchangeOnly = "{" +
                       "\"tabLabel\":\"app.chkExchangeOnly\"," +
                       "\"name\":\"app.chkExchangeOnly\"," +
                       "\"value\":\"" + "x" + "\"" +
                       "},";
            }
            else if (dr["RefundPolicy"].ToString().Trim() == "No Refund")
            {
                strCheckBox = strCheckBox + "{" +
                       "\"tabLabel\":\"app.chkRefundOther\"," +
                       "\"name\":\"app.chkRefundOther\"," +
                       "\"selected\":\"" + "true" + "\"" +
                       "},";

                strchkRefundOther = "{" +
                       "\"tabLabel\":\"app.chkRefundOther\"," +
                       "\"name\":\"app.chkRefundOther\"," +
                       "\"value\":\"" + "x" + "\"" +
                       "},";

                strOtherRefund = "{" +
                       "\"tabLabel\":\"app.OtherRefund\"," +
                       "\"name\":\"app.OtherRefund\"," +
                       "\"value\":\"" + "No Refund" + "\"" +
                       "},";
            }
            else if (dr["RefundPolicy"].ToString().Trim().Contains("Other"))
            {
                strCheckBox = strCheckBox + "{" +
                       "\"tabLabel\":\"app.chkRefundOther\"," +
                       "\"name\":\"app.chkRefundOther\"," +
                       "\"selected\":\"" + "true" + "\"" +
                       "},";

                strchkRefundOther = "{" +
                       "\"tabLabel\":\"app.chkRefundOther\"," +
                       "\"name\":\"app.chkRefundOther\"," +
                       "\"value\":\"" + "x" + "\"" +
                       "},";

                strOtherRefund = "{" +
                       "\"tabLabel\":\"app.OtherRefund\"," +
                       "\"name\":\"app.OtherRefund\"," +
                       "\"value\":\"" + dr["OtherRefund"].ToString().Trim() + "\"" +
                       "},";
            }


            string strLegalStatus = "";
            if (dr["LegalStatus"].ToString().Trim() == "Sole Proprietorship")
            {
                strCheckBox = strCheckBox + "{" +
                       "\"tabLabel\":\"app.chkSole\"," +
                       "\"name\":\"app.chkSole\"," +
                       "\"selected\":\"" + "true" + "\"" +
                       "},";

                strLegalStatus = "{" +
                       "\"tabLabel\":\"app.chkSole\"," +
                       "\"name\":\"app.chkSole\"," +
                       "\"value\":\"" + "x" + "\"" +
                       "},";
            }
            else if (dr["LegalStatus"].ToString().Trim() == "Corporation")
            {
                strCheckBox = strCheckBox + "{" +
                       "\"tabLabel\":\"app.chkCorp\"," +
                       "\"name\":\"app.chkCorp\"," +
                       "\"selected\":\"" + "true" + "\"" +
                       "},";

                strLegalStatus = "{" +
                       "\"tabLabel\":\"app.chkCorp\"," +
                       "\"name\":\"app.chkCorp\"," +
                       "\"value\":\"" + "x" + "\"" +
                       "},";
            }
            else if (dr["LegalStatus"].ToString().Trim() == "Partnership")
            {
                strCheckBox = strCheckBox + "{" +
                       "\"tabLabel\":\"app.chkPartnership\"," +
                       "\"name\":\"app.chkPartnership\"," +
                       "\"selected\":\"" + "true" + "\"" +
                       "},";

                strLegalStatus = "{" +
                       "\"tabLabel\":\"app.chkPartnership\"," +
                       "\"name\":\"app.chkPartnership\"," +
                       "\"value\":\"" + "x" + "\"" +
                       "},";
            }
            else if (dr["LegalStatus"].ToString().Trim() == "Non-Profit")
            {
                strCheckBox = strCheckBox + "{" +
                       "\"tabLabel\":\"app.chkNonProfit\"," +
                       "\"name\":\"app.chkNonProfit\"," +
                       "\"selected\":\"" + "true" + "\"" +
                       "},";

                strLegalStatus = "{" +
                       "\"tabLabel\":\"app.chkNonProfit\"," +
                       "\"name\":\"app.chkNonProfit\"," +
                       "\"value\":\"" + "x" + "\"" +
                       "},";
            }
            else if (dr["LegalStatus"].ToString().Trim() == "Legal/Medical Corp.")
            {
                strCheckBox = strCheckBox + "{" +
                       "\"tabLabel\":\"app.chkLegaMedical\"," +
                       "\"name\":\"app.chkLegaMedical\"," +
                       "\"selected\":\"" + "true" + "\"" +
                       "},";

                strLegalStatus = "{" +
                       "\"tabLabel\":\"app.chkLegaMedical\"," +
                       "\"name\":\"app.chkLegaMedical\"," +
                       "\"value\":\"" + "x" + "\"" +
                       "},";
            }
            else if (dr["LegalStatus"].ToString().Trim() == "Government")
            {
                strCheckBox = strCheckBox + "{" +
                       "\"tabLabel\":\"app.chkGovt\"," +
                       "\"name\":\"app.chkGovt\"," +
                       "\"selected\":\"" + "true" + "\"" +
                       "},";

                strLegalStatus = "{" +
                       "\"tabLabel\":\"app.chkGovt\"," +
                       "\"name\":\"app.chkGovt\"," +
                       "\"value\":\"" + "x" + "\"" +
                       "},";
            }
            else if (dr["LegalStatus"].ToString().Trim() == "Tax Exempt")
            {
                strCheckBox = strCheckBox + "{" +
                       "\"tabLabel\":\"app.chkTaxExempt\"," +
                       "\"name\":\"app.chkTaxExempt\"," +
                       "\"selected\":\"" + "true" + "\"" +
                       "},";

                strLegalStatus = "{" +
                       "\"tabLabel\":\"app.chkTaxExempt\"," +
                       "\"name\":\"app.chkTaxExempt\"," +
                       "\"value\":\"" + "x" + "\"" +
                       "},";
            }
            else if (dr["LegalStatus"].ToString().Trim() == "Others")
            {
                strCheckBox = strCheckBox + "{" +
                       "\"tabLabel\":\"app.chkOwnershipOther\"," +
                       "\"name\":\"app.chkOwnershipOther\"," +
                       "\"selected\":\"" + "true" + "\"" +
                       "},";

                strLegalStatus = "{" +
                       "\"tabLabel\":\"app.chkOwnershipOther\"," +
                       "\"name\":\"app.chkOwnershipOther\"," +
                       "\"value\":\"" + "x" + "\"" +
                       "},";
            }
            else if (dr["LegalStatus"].ToString().Trim() == "LLC")
            {
                strCheckBox = strCheckBox + "{" +
                       "\"tabLabel\":\"app.chkLLC\"," +
                       "\"name\":\"app.chkLLC\"," +
                       "\"selected\":\"" + "true" + "\"" +
                       "},";

                strLegalStatus = "{" +
                       "\"tabLabel\":\"app.chkLLC\"," +
                       "\"name\":\"app.chkLLC\"," +
                       "\"value\":\"" + "x" + "\"" +
                       "},";
            }



            if (Convert.ToInt32(dr["BusinessPctInternet"]) >= 50)
            {
                strCheckBox = strCheckBox + "{" +
                       "\"tabLabel\":\"App.BusTypeInternet\"," +
                       "\"name\":\"App.BusTypeInternet\"," +
                       "\"selected\":\"" + "true" + "\"" +
                       "},";
            }
            else if (Convert.ToInt32(dr["BusinessPctService"]) >= 50)
            {
                strCheckBox = strCheckBox + "{" +
                        "\"tabLabel\":\"App.RetailTip\"," +
                        "\"name\":\"App.RetailTip\"," +
                        "\"selected\":\"" + "true" + "\"" +
                         "},";
                //acroFields.SetField("App.RetailTip", "Yes");
            }
            else if (Convert.ToInt32(dr["BusinessPctRetail"]) >= 50)
            {
                strCheckBox = strCheckBox + "{" +
        "\"tabLabel\":\"App.Retail\"," +
        "\"name\":\"App.Retail\"," +
        "\"selected\":\"" + "true" + "\"" +
         "},";
                //acroFields.SetField("App.Retail", "Yes");
            }
            else if (Convert.ToInt32(dr["BusinessPctMailOrder"]) >= 50)
            {
                strCheckBox = strCheckBox + "{" +
        "\"tabLabel\":\"App.Moto\"," +
        "\"name\":\"App.Moto\"," +
        "\"selected\":\"" + "true" + "\"" +
         "},";
                //acroFields.SetField("App.Moto", "Yes");
            }
            else if (Convert.ToInt32(dr["BusinessPctRestaurant"]) >= 50)
            {
                strCheckBox = strCheckBox + "{" +
        "\"tabLabel\":\"App.Restaurant\"," +
        "\"name\":\"App.Restaurant\"," +
        "\"selected\":\"" + "true" + "\"" +
         "},";
                //acroFields.SetField("App.Restaurant", "Yes");
            }


            string strEquipmentModel = "";
            if (dr["Equipment"].ToString().Trim() != "")
            {
                string equipment = dr["Equipment"].ToString().Trim();

                strEquipmentModel = "{" +
                       "\"tabLabel\":\"app.EquipModel\"," +
                       "\"name\":\"app.EquipModel\"," +
                       "\"value\":\"" + equipment + "\"" +
                       "},";


                if (equipment.Contains("Nurit"))
                {
                    strCheckBox = strCheckBox + "{" +
                       "\"tabLabel\":\"app.chkNurit\"," +
                       "\"name\":\"app.chkNurit\"," +
                       "\"selected\":\"" + "true" + "\"" +
                       "},";

                    strEquipmentModel = strEquipmentModel + "{" +
                       "\"tabLabel\":\"app.chkNurit\"," +
                       "\"name\":\"app.chkNurit\"," +
                       "\"value\":\"" + "x" + "\"" +
                       "},";
                }
                else if (equipment.Contains("Verifone"))
                {
                    strCheckBox = strCheckBox + "{" +
                       "\"tabLabel\":\"app.chkVerifone\"," +
                       "\"name\":\"app.chkVerifone\"," +
                       "\"selected\":\"" + "true" + "\"" +
                       "},";

                    strEquipmentModel = strEquipmentModel + "{" +
                       "\"tabLabel\":\"app.chkVerifone\"," +
                       "\"name\":\"app.chkVerifone\"," +
                       "\"value\":\"" + "x" + "\"" +
                       "},";
                }
                else if (equipment.Contains("Hypercom"))
                {
                    strCheckBox = strCheckBox + "{" +
                       "\"tabLabel\":\"app.chkHypercom\"," +
                       "\"name\":\"app.chkHypercom\"," +
                       "\"selected\":\"" + "true" + "\"" +
                       "},";

                    strEquipmentModel = strEquipmentModel + "{" +
                       "\"tabLabel\":\"app.chkHypercom\"," +
                       "\"name\":\"app.chkHypercom\"," +
                       "\"value\":\"" + "x" + "\"" +
                       "},";
                }
                else
                {
                    strCheckBox = strCheckBox + "{" +
                       "\"tabLabel\":\"app.chkOther\"," +
                       "\"name\":\"app.chkOther\"," +
                       "\"selected\":\"" + "true" + "\"" +
                       "},";

                    strEquipmentModel = strEquipmentModel + "{" +
                       "\"tabLabel\":\"app.chkOther\"," +
                       "\"name\":\"app.chkOther\"," +
                       "\"value\":\"" + "x" + "\"" +
                       "},";
                }
            }

            string strP1LivingStatus = "";
            if (dr["P1LivingStatus"].ToString().Trim() == "Rent")
            {
                strCheckBox = strCheckBox + "{" +
                       "\"tabLabel\":\"app.chkP1Rent\"," +
                       "\"name\":\"app.chkP1Rent\"," +
                       "\"selected\":\"" + "true" + "\"" +
                       "},";

                strP1LivingStatus = "{" +
                        "\"tabLabel\":\"app.chkP1Rent\"," +
                        "\"name\":\"app.chkP1Rent\"," +
                        "\"value\":\"" + "x" + "\"" +
                        "},";
            }
            else if (dr["P1LivingStatus"].ToString().Trim() == "Own")
            {
                strCheckBox = strCheckBox + "{" +
                       "\"tabLabel\":\"app.chkP1Own\"," +
                       "\"name\":\"app.chkP1Own\"," +
                       "\"selected\":\"" + "true" + "\"" +
                       "},";

                strP1LivingStatus = "{" +
                        "\"tabLabel\":\"app.chkP1Own\"," +
                        "\"name\":\"app.chkP1Own\"," +
                        "\"value\":\"" + "x" + "\"" +
                        "},";
            }

            string strP2 = "";

            if (!Convert.IsDBNull(dr["P2FirstName"]))
            {
                if (dr["P2FirstName"].ToString().Trim() != "")
                {
                    string strP2LivingStatus = "";

                    if (dr["P2LivingStatus"].ToString().Trim() == "Rent")
                    {
                        strCheckBox = strCheckBox + "{" +
                       "\"tabLabel\":\"app.chkP2Rent\"," +
                       "\"name\":\"app.chkP2Rent\"," +
                       "\"selected\":\"" + "true" + "\"" +
                       "},";

                        strP2LivingStatus = "{" +
                                "\"tabLabel\":\"app.chkP2Rent\"," +
                                "\"name\":\"app.chkP2Rent\"," +
                                "\"value\":\"" + "x" + "\"" +
                                "},";
                    }
                    else if (dr["P2LivingStatus"].ToString().Trim() == "Own")
                    {
                        strCheckBox = strCheckBox + "{" +
                       "\"tabLabel\":\"app.chkP2Own\"," +
                       "\"name\":\"app.chkP2Own\"," +
                       "\"selected\":\"" + "true" + "\"" +
                       "},";

                        strP2LivingStatus = "{" +
                                "\"tabLabel\":\"app.chkP2Own\"," +
                                "\"name\":\"app.chkP2Own\"," +
                                "\"value\":\"" + "x" + "\"" +
                                "},";
                    }


                    strP2 = "{" +
                        "\"tabLabel\":\"app.P2ZipCode\"," +
                        "\"name\":\"app.P2ZipCode\"," +
                        "\"value\":\"" + dr["P2ZipCode"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.P2State\"," +
                        "\"name\":\"app.P2State\"," +
                        "\"value\":\"" + dr["P2State"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.P2City\"," +
                        "\"name\":\"app.P2City\"," +
                        "\"value\":\"" + dr["P2City"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.P2Address\"," +
                        "\"name\":\"app.P2Address\"," +
                        "\"value\":\"" + dr["P2Address"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.P2Title\"," +
                        "\"name\":\"app.P2Title\"," +
                        "\"value\":\"" + dr["P2Title"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.P2SSN\"," +
                        "\"name\":\"app.P2SSN\"," +
                        "\"value\":\"" + dr["P2SSN"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.P2Name\"," +
                        "\"name\":\"app.P2Name\"," +
                        "\"value\":\"" + dr["P2FirstName"].ToString().Trim() + " " + dr["P2LastName"].ToString().Trim() + "\"" +
                        "}," +
                         "{" +
                        "\"tabLabel\":\"app.P2Name1\"," +
                        "\"name\":\"app.P2Name1\"," +
                        "\"value\":\"" + dr["P2FirstName"].ToString().Trim() + " " + dr["P2LastName"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.P2Ownership\"," +
                        "\"name\":\"app.P2Ownership\"," +
                        "\"value\":\"" + dr["P2OwnershipPercent"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.P2DOB\"," +
                        "\"name\":\"app.P2DOB\"," +
                        "\"value\":\"" + dr["P2DOB"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.P2DState\"," +
                        "\"name\":\"app.P2DState\"," +
                        "\"value\":\"" + dr["P2DriversLicenseState"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.P2DriversLicense\"," +
                        "\"name\":\"app.P2DriversLicense\"," +
                        "\"value\":\"" + dr["P2DriversLicenseNo"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.P2HomePhone\"," +
                        "\"name\":\"app.P2HomePhone\"," +
                        "\"value\":\"" + dr["P2PhoneNumber"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.P2TimeAtAddress\"," +
                        "\"name\":\"app.P2TimeAtAddress\"," +
                        "\"value\":\"" + dr["P2TimeAtAddress"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.P2TimeAtAddress\"," +
                        "\"name\":\"app.P2TimeAtAddress\"," +
                        "\"value\":\"" + dr["P2TimeAtAddress"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.P2Email\"," +
                        "\"name\":\"app.P2Email\"," +
                        "\"value\":\"" + dr["P2Email"].ToString().Trim() + "\"" +
                        "}," +
                        strP2LivingStatus
                        ;
                }
            }


            string strDiscountRate = "";

            if ((dr["DiscountRate"].ToString().Trim() != "") && (dr["Interchange"].ToString().Trim() != "True"))
            {
                strDiscountRate = "{" +
                        "\"tabLabel\":\"app.QualifiedFee\"," +
                        "\"name\":\"app.QualifiedFee\"," +
                        "\"value\":\"" + Convert.ToString(dr["DiscountRate"]).Trim() + "\"" +
                        "},";
            }

            string strMidQual = "";

            if ((dr["DiscountRate"].ToString().Trim() != "") && (dr["DiscRateMidQual"].ToString().Trim() != "") && (dr["Interchange"].ToString().Trim() != "True"))
            {
                strMidQual = "{" +
                        "\"tabLabel\":\"app.MidQualifiedFee\"," +
                        "\"name\":\"app.MidQualifiedFee\"," +
                        "\"value\":\"" + Convert.ToString(Convert.ToDecimal(dr["DiscRateMidQual"]) - Convert.ToDecimal(dr["DiscountRate"])) + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.MidQualifiedFee1\"," +
                        "\"name\":\"app.MidQualifiedFee1\"," +
                        "\"value\":\"" + Convert.ToString(Convert.ToDecimal(dr["DiscRateMidQual"]) - Convert.ToDecimal(dr["DiscountRate"])) + "\"" +
                        "},";
            }

            string strNonQual = "";

            if ((dr["DiscountRate"].ToString().Trim() != "") && (dr["DiscRateMidQual"].ToString().Trim() != "") && (dr["Interchange"].ToString().Trim() != "True"))
            {
                strNonQual = "{" +
                        "\"tabLabel\":\"app.NonQualifiedFee\"," +
                        "\"name\":\"app.NonQualifiedFee\"," +
                        "\"value\":\"" + Convert.ToString(Convert.ToDecimal(dr["DiscRateNonQual"]) - Convert.ToDecimal(dr["DiscountRate"])) + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.NonQualifiedFee1\"," +
                        "\"name\":\"app.NonQualifiedFee1\"," +
                        "\"value\":\"" + Convert.ToString(Convert.ToDecimal(dr["DiscRateNonQual"]) - Convert.ToDecimal(dr["DiscountRate"])) + "\"" +
                        "},";
            }


            string strWireless = "";

            if ((dr["WirelessAccessFee"].ToString().Trim() != "") && (dr["WirelessTransFee"].ToString().Trim() != ""))
            {

                strWireless = "{" +
                       "\"tabLabel\":\"app.WirelessMonthlyGatewayFee\"," +
                       "\"name\":\"app.WirelessMonthlyGatewayFee\"," +
                       "\"value\":\"" + Convert.ToString(dr["WirelessAccessFee"]).Trim() + "\"" +
                       "}," +
                       "{" +
                       "\"tabLabel\":\"app.WirelessPerAuthFee\"," +
                       "\"name\":\"app.WirelessPerAuthFee\"," +
                       "\"value\":\"" + Convert.ToString(dr["WirelessTransFee"]).Trim() + "\"" +
                       "}," +
                       "{" +
                       "\"tabLabel\":\"app.WirelessSetupfee\"," +
                       "\"name\":\"app.WirelessSetupfee\"," +
                       "\"value\":\"" + "35.00" + "\"" +
                       "}," +
                       "{" +
                       "\"tabLabel\":\"app.WirelessSetupQuantity\"," +
                       "\"name\":\"app.WirelessSetupQuantity\"," +
                       "\"value\":\"" + "1" + "\"" +
                       "}," +
                       "{" +
                       "\"tabLabel\":\"app.WirelessMonthlyAccessQuantity\"," +
                       "\"name\":\"app.WirelessMonthlyAccessQuantity\"," +
                       "\"value\":\"" + "1" + "\"" +
                       "},"
                       ;
            }

            string strEBT = "";

            if (dr["EBTTransFee"].ToString().Trim() != "")
            {
                strCheckBox = strCheckBox + "{" +
                       "\"tabLabel\":\"app.chkEBT\"," +
                       "\"name\":\"app.chkEBT\"," +
                       "\"selected\":\"" + "true" + "\"" +
                       "},";



                strEBT =
                "{" +
                "\"tabLabel\":\"app.chkEBT\"," +
                "\"name\":\"app.chkEBT\"," +
                "\"value\":\"" + "x" + "\"" +
                "}," +

                "{" +
                "\"tabLabel\":\"app.EBTTransFee\"," +
                "\"name\":\"app.EBTTransFee\"," +
                "\"value\":\"" + dr["EBTTransFee"].ToString().Trim() + "\"" +
                "}," +

                "{" +
                "\"tabLabel\":\"app.USDANum\"," +
                "\"name\":\"app.USDANum\"," +
                "\"value\":\"" + Convert.ToString(dr["USDANum"]) + "\"" +
                "},";

            }

            string strDebit = "";

            if ((dr["DebitMonFee"].ToString().Trim() != "") && (dr["DebitTransFee"].ToString().Trim() != ""))
            {
                
                strDebit = "{" +
                "\"tabLabel\":\"app.DebitCardAccessFee\"," +
                "\"name\":\"app.DebitCardAccessFee\"," +
                "\"value\":\"" + Convert.ToString(dr["DebitMonFee"]).Trim() + "\"" +
                "}," +
                "{" +
                "\"tabLabel\":\"app.Debit\"," +
                "\"name\":\"app.Debit\"," +
                "\"value\":\"" + Convert.ToString(dr["DebitTransFee"]).Trim() + "\"" +
                "}," +
                "{" +
                "\"tabLabel\":\"app.chkDebitCard\"," +
                "\"name\":\"app.chkDebitCard\"," +
                "\"value\":\"" + "x" + "\"" +
                "}," +
                "{" +
                "\"tabLabel\":\"app.DebitDiscountRate\"," +
                "\"name\":\"app.DebitDiscountRate\"," +
                "\"value\":\"" + Convert.ToString(dr["PinDebitDiscount"]).Trim() + "\"" +
                "},"

                ;

            }
            string strAnnualFee = "";


            if (dr["AnnualFeeCP"].ToString().Trim() != "")
            {
                strAnnualFee = "{" +
                "\"tabLabel\":\"app.AnnualFee\"," +
                "\"name\":\"app.AnnualFee\"," +
                "\"value\":\"" + Convert.ToString(dr["AnnualFeeCP"]).Trim() + "\"" +
                "},";
            }
            else
            {
                strAnnualFee = "{" +
                "\"tabLabel\":\"app.AnnualFee\"," +
                "\"name\":\"app.AnnualFee\"," +
                "\"value\":\"" + Convert.ToString(dr["AnnualFeeCNP"]).Trim() + "\"" +
                "},";
            }

            string strGateway = "";

            if (dr["Gateway"].ToString().Trim() != "")
            {
                strCheckBox = strCheckBox + "{" +
                       "\"tabLabel\":\"app.GatewayCheckbox\"," +
                       "\"name\":\"app.GatewayCheckbox\"," +
                       "\"selected\":\"" + "true" + "\"" +
                       "},";

                strGateway =
                "{" +
                "\"tabLabel\":\"app.Gateway\"," +
                "\"name\":\"app.Gateway\"," +
                "\"value\":\"" + Convert.ToString(dr["Gateway"]).Trim() + "\"" +
                "},"
                ;
            }


            string strInternetStmt = "";

            if (dr["InternetStmt"].ToString().Trim() == "14.95")
            {
                strCheckBox = strCheckBox + "{" +
                       "\"tabLabel\":\"app.iAccessSingle\"," +
                       "\"name\":\"app.iAccessSingle\"," +
                       "\"selected\":\"" + "true" + "\"" +
                       "},";

                strInternetStmt =
                "{" +
                "\"tabLabel\":\"app.iAccessSingle\"," +
                "\"name\":\"app.iAccessSingle\"," +
                "\"value\":\"" + "x" + "\"" +
                "},"
                ;
            }
            else if (dr["InternetStmt"].ToString().Trim() == "30.00")
            {
                strCheckBox = strCheckBox + "{" +
                       "\"tabLabel\":\"app.iAccessChain\"," +
                       "\"name\":\"app.iAccessChain\"," +
                       "\"selected\":\"" + "true" + "\"" +
                       "},";

                strInternetStmt =
                "{" +
                "\"tabLabel\":\"app.iAccessChain\"," +
                "\"name\":\"app.iAccessChain\"," +
                "\"value\":\"" + "x" + "\"" +
                "},"
                ;
            }

            string strInterchange = "";
            if (Convert.ToString(dr["Interchange"]).Trim() == "True")
            {
                strInterchange = "{" +
                         "\"tabLabel\":\"DiscIPT\"," +
                         "\"name\":\"DiscIPT\"," +
                         "\"value\":\"" + "x" + "\"" +
                         "}," +
                         "{" +
                         "\"tabLabel\":\"VisaIPT\"," +
                         "\"name\":\"VisaIPT\"," +
                         "\"value\":\"" + "x" + "\"" +
                         "}," +
                         "{" +
                         "\"tabLabel\":\"MCIPT\"," +
                         "\"name\":\"MCIPT\"," +
                         "\"value\":\"" + "x" + "\"" +
                         "}," +
                         "{" +
                         "\"tabLabel\":\"Visa INTL/NS\"," +
                         "\"name\":\"Visa INTL/NS\"," +
                         "\"value\":\"" + "0.00" + "\"" +
                         "}," +
                         "{" +
                         "\"tabLabel\":\"Visa INTL/NS\"," +
                         "\"name\":\"Visa INTL/NS\"," +
                         "\"value\":\"" + "0.00" + "\"" +
                         "}," +
                         "{" +
                         "\"tabLabel\":\"MC INTL/NS\"," +
                         "\"name\":\"MC INTL/NS\"," +
                         "\"value\":\"" + "0.00" + "\"" +
                         "}," +
                         "{" +
                         "\"tabLabel\":\"Disc INTL/NS\"," +
                         "\"name\":\"Disc INTL/NS\"," +
                         "\"value\":\"" + "0.00" + "\"" +
                         "}," +
                         "{" +
                         "\"tabLabel\":\"Visa INTL/NS Surcharge\"," +
                         "\"name\":\"Visa INTL/NS Surcharge\"," +
                         "\"value\":\"" + "0.00" + "\"" +
                         "}," +
                         "{" +
                         "\"tabLabel\":\"MC INTL/NS Surcharge\"," +
                         "\"name\":\"MC INTL/NS Surcharge\"," +
                         "\"value\":\"" + "0.00" + "\"" +
                         "}," +
                         "{" +
                         "\"tabLabel\":\"Disc INTL/NS Surcharge\"," +
                         "\"name\":\"Disc INTL/NS Surcharge\"," +
                         "\"value\":\"" + "0.00" + "\"" +
                         "}," +

                         "{" +
                        "\"tabLabel\":\"app.InterchangePlusDebit\"," +
                        "\"name\":\"app.InterchangePlusDebit\"," +
                        "\"value\":\"" + Convert.ToString(dr["DiscountRate"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.InterchangePlusCredit\"," +
                        "\"name\":\"app.InterchangePlusCredit\"," +
                        "\"value\":\"" + Convert.ToString(dr["DiscountRate"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.AmexQualifedFee\"," +
                        "\"name\":\"app.AmexQualifiedFee\"," +
                        "\"value\":\"" + Convert.ToString(dr["DiscountRate"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.AmexInterchangePlus\"," +
                        "\"name\":\"app.AmexInterchangePlus\"," +
                        "\"value\":\"" + Convert.ToString(dr["DiscountRate"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.AmexInterchangeMidPlus\"," +
                        "\"name\":\"app.AmexInterchangeMidPlus\"," +
                        "\"value\":\"" + "0.00" + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.AmexInterchangeNonQualPlus\"," +
                        "\"name\":\"app.AmexInterchangeNonQualPlus\"," +
                        "\"value\":\"" + "0.00" + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"AMEX Mid-Qualfieid\"," +
                        "\"name\":\"AMEX Mid-Qualfieid\"," +
                        "\"value\":\"" + "0.00" + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"AMEX Non-Qualfieid\"," +
                        "\"name\":\"AMEX Non-Qualfieid\"," +
                        "\"value\":\"" + "0.00" + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"CreditCardNonQualifiedPerItemFee\"," +
                        "\"name\":\"CreditCardNonQualifiedPerItemFee\"," +
                        "\"value\":\"" + "" + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"CreditCardMidQualifiedPerItemFee\"," +
                        "\"name\":\"CreditCardMidQualifiedPerItemFee\"," +
                        "\"value\":\"" + "" + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"CreditCardNonQualifiedPerItemFee1\"," +
                        "\"name\":\"CreditCardNonQualifiedPerItemFee1\"," +
                        "\"value\":\"" + "" + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"CreditCardMidQualifiedPerItemFee1\"," +
                        "\"name\":\"CreditCardMidQualifiedPerItemFee1\"," +
                        "\"value\":\"" + "" + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.InterchangePricingChk\"," +
                        "\"name\":\"app.InterchangePricingChk\"," +
                        "\"value\":\"" + "x" + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.InterchangePlusChk\"," +
                        "\"name\":\"app.InterchangePlusChk\"," +
                        "\"value\":\"" + "x" + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.InterchangeAmexChk\"," +
                        "\"name\":\"app.InterchangeAmexChk\"," +
                        "\"value\":\"" + "x" + "\"" +
                        "},";
            }



            /*string strInterchange = "";

            if (dr["Interchange"].ToString().Trim() == "True")
            {

                strInterchange =
                "{" +
                "\"tabLabel\":\"app.InterchangePlus\"," +
                "\"name\":\"app.InterchangePlus\"," +
                "\"value\":\"" + dr["DiscountRate"].ToString().Trim() + "\"" +
                "},"
                ;

            }*/


            string strAmex = "";



            if ((dr["AmexNum"].ToString().Trim() != "") && (dr["AmexNum"].ToString().Trim() != "No"))
            {
                if (dr["AmexNum"].ToString().Trim() == "Yes" || Convert.ToDouble(dr["ProcessPctSwiped"].ToString().Trim()) >= 70)
                {
                    strCheckBox = strCheckBox + "{" +
                       "\"tabLabel\":\"app.DiscRateCheckBox\"," +
                       "\"name\":\"app.DiscRateCheckBox\"," +
                       "\"selected\":\"" + "true" + "\"" +
                       "},";

                    strCheckBox = strCheckBox + "{" +
                       "\"tabLabel\":\"CheckBox230\"," +
                       "\"name\":\"CheckBox230\"," +
                       "\"selected\":\"" + "true" + "\"" +
                       "},";

                    strAmex =

                    "{" +
                    "\"tabLabel\":\"Text 225\"," +
                    "\"name\":\"Text 225\"," +
                    "\"value\":\"" + Convert.ToString(Convert.ToInt32(dr["MonthlyVolume"]) * 12).Trim() + "\"" +
                    "}," +
                     "{" +
                    "\"tabLabel\":\"Text 226\"," +
                    "\"name\":\"Text 226\"," +
                    "\"value\":\"" + Convert.ToString(dr["AverageTicket"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"app.DiscRateCheckBox\"," +
                    "\"name\":\"app.DiscRateCheckBox\"," +
                    "\"selected\":\"" + "true" + "\"" +
                    "},"
                    ;


                    if ((dr["DiscountRate"].ToString().Trim() != "") && (dr["Interchange"].ToString().Trim() != "True"))
                    {
                        strAmex = strAmex + "{" +
                "\"tabLabel\":\"app.AmexQualifiedFee\"," +
                "\"name\":\"app.AmexQualifiedFee\"," +
                "\"value\":\"" + dr["DiscountRate"].ToString().Trim() + "\"" +
                "},";

                    }


                    if ((dr["DiscountRate"].ToString().Trim() != "") && (dr["DiscRateMidQual"].ToString().Trim() != "") && (dr["Interchange"].ToString().Trim() != "True"))
                    {

                        strAmex = strAmex + "{" +
                "\"tabLabel\":\"app.AmexMidQualifiedFee\"," +
                "\"name\":\"app.AmexMidQualifiedFee\"," +
                "\"value\":\"" + Convert.ToString(Convert.ToDecimal(dr["DiscRateMidQual"])) + "\"" +
                "},";
                    }

                    if ((dr["DiscountRate"].ToString().Trim() != "") && (dr["DiscRateNonQual"].ToString().Trim() != "") && (dr["Interchange"].ToString().Trim() != "True"))
                    {
                        strAmex = strAmex + "{" +
                "\"tabLabel\":\"app.AmexNonQualifiedFee\"," +
                "\"name\":\"app.AmexNonQualifiedFee\"," +
                "\"value\":\"" + Convert.ToString(Convert.ToDecimal(dr["DiscRateNonQual"])) + "\"" +
                "},";


                    }

                    strAmex = strAmex + "{" +
                "\"tabLabel\":\"app.AmexInterchangePlus\"," +
                "\"name\":\"app.AmexInterchangePlus\"," +
                "\"value\":\"" + dr["DiscountRate"].ToString().Trim() + "\"" +
                "},";

                    if ((dr["DiscountRate"].ToString().Trim() != "") && (dr["Interchange"].ToString().Trim() == "True"))
                    {
                        strAmex = strAmex + "{" +
                "\"tabLabel\":\"app.AmexInterchangePlus\"," +
                "\"name\":\"app.AmexInterchangePlus\"," +
                "\"value\":\"" + dr["DiscountRate"].ToString().Trim() + "\"" +
                "},";

                    }

                    /*
                    if (Convert.ToDouble(dr["ProcessPctSwiped"].ToString().Trim()) >= 60)
                    {
                        strAmex = strAmex + "{" +
                "\"tabLabel\":\"app.AmexDiscountRate\"," +
                "\"name\":\"app.AmexDiscountRate\"," +
                "\"value\":\"" + "2.89" + "\"" +
                "},";

                    }
                    else
                    {

                        strAmex = strAmex + "{" +
                       "\"tabLabel\":\"app.AmexDiscountRate\"," +
                       "\"name\":\"app.AmexDiscountRate\"," +
                       "\"value\":\"" + "3.50" + "\"" +
                       "},";
                    }*/
                }
                else // Existing Amex number
                {
                    strCheckBox = strCheckBox + "{" +
                        "\"tabLabel\":\"app.MonthlyFFCheckBox\"," +
                        "\"name\":\"app.MonthlyFFCheckBox\"," +
                        "\"selected\":\"" + "true" + "\"" +
                        "},";

                    strAmex =
                "{" +
                "\"tabLabel\":\"app.AmexNum\"," +
                "\"name\":\"app.AmexNum\"," +
                "\"value\":\"" + dr["PrevAmexNum"].ToString() + "\"" +
                "}," +
                "{" +
                "\"tabLabel\":\"Check Box229\"," +
                "\"name\":\"Check Box229\"," +
                "\"value\":\"" + "x" + "\"" +
                "}," +
                "{" +
                "\"tabLabel\":\"app.MonthlyFFCheckBox\"," +
                "\"name\":\"app.MonthlyFFCheckBox\"," +
                "\"value\":\"" + "x" + "\"" +
                "},"
                ;
                }

            }

            string strPlatform = "";

            if (dr["Platform"].ToString().Trim().Contains("Omaha"))
            {
                strCheckBox = strCheckBox + "{" +
                        "\"tabLabel\":\"app.chkOmaha\"," +
                        "\"name\":\"app.chkOmaha\"," +
                        "\"selected\":\"" + "true" + "\"" +
                        "},";


            }
            else if (dr["Platform"].ToString().Contains("Nashville"))
            {
                strCheckBox = strCheckBox + "{" +
                        "\"tabLabel\":\"app.chkNashville\"," +
                        "\"name\":\"app.chkNashville\"," +
                        "\"selected\":\"" + "true" + "\"" +
                        "},";


            }
            else if (dr["Platform"].ToString().Contains("Buypass"))
            {
                strCheckBox = strCheckBox + "{" +
                        "\"tabLabel\":\"app.chkBuypass\"," +
                        "\"name\":\"app.chkBuypass\"," +
                        "\"selected\":\"" + "true" + "\"" +
                        "},";


            }
            else if ((dr["Platform"].ToString() != "") && (dr["Platform"].ToString().ToLower().Trim() != "none"))
            {

                strCheckBox = strCheckBox + "{" +
                        "\"tabLabel\":\"app.OtherPlatform\"," +
                        "\"name\":\"app.OtherPlatform\"," +
                        "\"selected\":\"" + "true" + "\"" +
                        "},";

                strPlatform =
                "{" +
                "\"tabLabel\":\"app.chkFrontEndOther\"," +
                "\"name\":\"app.chkFrontEndOther\"," +
                "\"value\":\"" + "x" + "\"" +
                "},"
                ;
            }


            string strDailyDiscount = "";

            string strDailyDiscount1 = "";

            if (!Convert.IsDBNull(dr["DiscountPaid"]))
            {
                if (Convert.ToString(dr["DiscountPaid"]).Trim() == "Monthly")
                {

                    strCheckBox = strCheckBox + "{" +
                        "\"tabLabel\":\"MonthlyDiscount\"," +
                        "\"name\":\"MonthlyDiscount\"," +
                        "\"selected\":\"" + "true" + "\"" +
                        "},";

                    strDailyDiscount1 = strDailyDiscount1 + "{" +
                        "\"tabLabel\":\"MonthlyDiscount\"," +
                        "\"name\":\"MonthlyDiscount\"," +
                        "\"value\":\"" + "x" + "\"" +
                        "},";


                }
                else if  (Convert.ToString(dr["DiscountPaid"]).Trim() == "Daily")
                {

                    strCheckBox = strCheckBox + "{" +
                        "\"tabLabel\":\"DailyDiscount\"," +
                        "\"name\":\"DailyDiscount\"," +
                        "\"selected\":\"" + "true" + "\"" +
                        "},";

                    strDailyDiscount1 = strDailyDiscount1 + "{" +
                        "\"tabLabel\":\"DailyDiscount\"," +
                        "\"name\":\"DailyDiscount\"," +
                        "\"value\":\"" + "x" + "\"" +
                        "},";


                }
            
            }

            string str111 = strCheckBox;
            string str112 = strDailyDiscount1;

            string offlineDebt = "";



            if (dr["Interchange"].ToString().Trim() != "True")
            {
                offlineDebt = offlineDebt + "{" +
                       "\"tabLabel\":\"app.DebitQualifiedFee\"," +
                       "\"name\":\"app.DebitQualifiedFee\"," +
                       "\"value\":\"" + Convert.ToString(dr["DiscRateQualDebit"]).Trim() + "\"" +
                       "},";
            }


            requestBody = "{" +
                        "\"status\":\"sent\"," +
                        "\"emailBlurb\":\"Please review and sign you document with Commerce Technologies\"," +
                        "\"emailSubject\": \"Please review and sign you document with Commerce Technologies\"," +
                        "\"templateId\":\"" + templateId + "\"," +
                        "\"templateRoles\":[" +
                        "{" +
                        "\"email\":\"" + Convert.ToString(dr["P1Email"]).Trim() + "\"," +
                        "\"name\":\"" + Convert.ToString(dr["P1FirstName"]).Trim() + " " + Convert.ToString(dr["P1LastName"]).Trim() + "\"," +
                        "\"roleName\":\"Signer\"," +
                        "\"clientUserId\":\"" + Convert.ToString(dr["P1FirstName"]).Trim() + " " + Convert.ToString(dr["P1LastName"]).Trim() + "\"," +
                        "\"tabs\": {" +
                         "\"checkboxTabs\": [" +
                         strCheckBox +
                        "]," +
                        "\"textTabs\": [" +
                        "{" +
                    "\"tabLabel\":\"app.MonthlyAmexVolume\"," +
                    "\"name\":\"app.MonthlyAmexVolume\"," +
                    "\"value\":\"" + Convert.ToString(Convert.ToInt32(dr["MonthlyVolume"]) / 4).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"app.AmexQualifiedFee\"," +
                        "\"name\":\"app.AmexQualifiedFee\"," +
                        "\"value\":\"" + Convert.ToString(dr["DiscountRate"]).Trim() + "\"" +
                        "}," +
                    "{" +
                    "\"tabLabel\":\"app.AmexInterchangeMidPlus\"," +
                        "\"name\":\"app.AmexInterchangeMidPlus\"," +
                        "\"value\":\"" + Convert.ToString(Convert.ToDecimal(dr["DiscRateMidQual"]) - Convert.ToDecimal(dr["DiscountRate"])) + "\"" +
                        "}," +
                        "{" +
                    "\"tabLabel\":\"app.AmexInterchangeNonQualPlus\"," +
                        "\"name\":\"app.AmexInterchangeNonQualPlus\"," +
                        "\"value\":\"" + Convert.ToString(Convert.ToDecimal(dr["DiscRateNonQual"]) - Convert.ToDecimal(dr["DiscountRate"])) + "\"" +
                        "}," +
                        "{" +
                    "\"tabLabel\":\"app.TieredPlusChk\"," +
                        "\"name\":\"app.TieredPlusChk\"," +
                        "\"value\":\"" + "x" + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.TieredAmexChk\"," +
                    "\"name\":\"app.TieredAmexChk\"," +
                        "\"value\":\"" + "x" + "\"" +
                        "}," +
                         "{" +
                        "\"tabLabel\":\"app.RepName\"," +
                        "\"name\":\"app.RepName\"," +
                        "\"value\":\"" + Convert.ToString(dr["RepName"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.RepName1\"," +
                        "\"name\":\"app.RepName1\"," +
                        "\"value\":\"" + Convert.ToString(dr["RepName"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.RepNamebd\"," +
                        "\"name\":\"app.RepNamebd\"," +
                        "\"value\":\"" + Convert.ToString(dr["RepName"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.LegalName\"," +
                        "\"name\":\"app.LegalName\"," +
                        "\"value\":\"" + Convert.ToString(dr["CompanyName"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.LegalName1\"," +
                        "\"name\":\"app.LegalName1\"," +
                        "\"value\":\"" + Convert.ToString(dr["CompanyName"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.LegalName122\"," +
                        "\"name\":\"app.LegalName122\"," +
                        "\"value\":\"" + Convert.ToString(dr["P1FirstName"]).Trim() + " " + Convert.ToString(dr["P1LastName"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.LegalName2\"," +
                        "\"name\":\"app.LegalName2\"," +
                        "\"value\":\"" + Convert.ToString(dr["CompanyName"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.LegalNamebd\"," +
                        "\"name\":\"app.LegalNamebd\"," +
                        "\"value\":\"" + Convert.ToString(dr["CompanyName"]).Trim() + "\"" +
                        "}," +
                         "{" +
                        "\"tabLabel\":\"app.Website\"," +
                        "\"name\":\"app.Website\"," +
                        "\"value\":\"" + Convert.ToString(dr["Website"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.DBA\"," +
                        "\"name\":\"app.DBA\"," +
                        "\"value\":\"" + Convert.ToString(dr["DBA"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.DBA1\"," +
                        "\"name\":\"app.DBA1\"," +
                        "\"value\":\"" + Convert.ToString(dr["DBA"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.ApplicantDBA\"," +
                        "\"name\":\"app.ApplicantDBA\"," +
                        "\"value\":\"" + Convert.ToString(dr["DBA"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.EMail\"," +
                        "\"name\":\"app.EMail\"," +
                        "\"value\":\"" + Convert.ToString(dr["Email"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.ContactName\"," +
                        "\"name\":\"app.ContactName\"," +
                        "\"value\":\"" + Convert.ToString(dr["ContactName"]).Trim() + "\"" +
                        "}," +
                        "{" +
                       "\"tabLabel\":\"app.MailingAddress\"," +
                       "\"name\":\"app.MailingAddress\"," +
                       "\"value\":\"" + Convert.ToString(dr["BillingAddress"]).Trim() + "\"" +
                       "}," +
                        "{" +
                        "\"tabLabel\":\"app.MCityState\"," +
                        "\"name\":\"app.MCityState\"," +
                        "\"value\":\"" + Convert.ToString(dr["BillingCity"]).Trim() + ", " + Convert.ToString(dr["BillingState"]).Trim() + ", " + Convert.ToString(dr["BillingZipCode"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.BusinessAddress\"," +
                        "\"name\":\"app.BusinessAddress\"," +
                        "\"value\":\"" + Convert.ToString(dr["Address"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.BusinessAddressbd\"," +
                        "\"name\":\"app.BusinessAddressbd\"," +
                        "\"value\":\"" + Convert.ToString(dr["Address"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.BusinessAddress1\"," +
                        "\"name\":\"app.BusinessAddress1\"," +
                        "\"value\":\"" + Convert.ToString(dr["Address"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.Regulatory\"," +
                        "\"name\":\"app.Regulatory\"," +
                        "\"value\":\"" + Convert.ToString(dr["RegulatoryFee"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.CityState\"," +
                        "\"name\":\"app.CityState\"," +
                        "\"value\":\"" + dr["City"].ToString().Trim() + ", " + dr["State"].ToString().Trim() + ", " + dr["ZipCode"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.CityState1\"," +
                        "\"name\":\"app.CityState1\"," +
                        "\"value\":\"" + dr["City"].ToString().Trim() + ", " + dr["State"].ToString().Trim() + ", " + dr["ZipCode"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.CityStatebd\"," +
                        "\"name\":\"app.CityStatebd\"," +
                        "\"value\":\"" + dr["City"].ToString().Trim() + ", " + dr["State"].ToString().Trim() + ", " + dr["ZipCode"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.Region\"," +
                        "\"name\":\"app.Region\"," +
                        "\"value\":\"" + Convert.ToString(dr["Country"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.HowLong\"," +
                        "\"name\":\"app.HowLong\"," +
                        "\"value\":\"" + dr["TABL"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.TIBYears\"," +
                        "\"name\":\"app.TIBYears\"," +
                        "\"value\":\"" + dr["YIB"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.TIBMonths\"," +
                        "\"name\":\"app.TIBMonths\"," +
                        "\"value\":\"" + dr["MIB"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.Fax\"," +
                        "\"name\":\"app.Fax\"," +
                        "\"value\":\"" + Convert.ToString(dr["Fax"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.BusinessPhone\"," +
                        "\"name\":\"app.BusinessPhone\"," +
                        "\"value\":\"" + Convert.ToString(dr["BusinessPhone"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.BusinessPhone1\"," +
                        "\"name\":\"app.BusinessPhone1\"," +
                        "\"value\":\"" + Convert.ToString(dr["BusinessPhone"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.BusinessPhonebd\"," +
                        "\"name\":\"app.BusinessPhonebd\"," +
                        "\"value\":\"" + Convert.ToString(dr["BusinessPhone"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.BusinessHours\"," +
                        "\"name\":\"app.BusinessHours\"," +
                        "\"value\":\"" + Convert.ToString(dr["BusinessHours"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.ProductsSold\"," +
                        "\"name\":\"app.ProductsSold\"," +
                        "\"value\":\"" + Convert.ToString(dr["ProductSold"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.PrevProcessor\"," +
                        "\"name\":\"app.PrevProcessor\"," +
                        "\"value\":\"" + Convert.ToString(dr["PrevProcessor"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.PrevMerchantNum\"," +
                        "\"name\":\"app.PrevMerchantNum\"," +
                        "\"value\":\"" + Convert.ToString(dr["PrevMerchantAcctNo"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.CustServPhone\"," +
                        "\"name\":\"app.CustServPhone\"," +
                        "\"value\":\"" + Convert.ToString(dr["CustServPhone"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.RepNum\"," +
                        "\"name\":\"app.RepNum\"," +
                        "\"value\":\"" + Convert.ToString(dr["RepNum"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.RepPhone\"," +
                        "\"name\":\"app.RepPhone\"," +
                        "\"value\":\"" + Convert.ToString(dr["RepPhone"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.AddlComments\"," +
                        "\"name\":\"app.AddlComments\"," +
                        "\"value\":\"" + Convert.ToString(dr["AddlComments"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.NumLocs\"," +
                        "\"name\":\"app.NumLocs\"," +
                        "\"value\":\"" + Convert.ToString(dr["NumberofLocations"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.BusinessPhoneExt\"," +
                        "\"name\":\"app.BusinessPhoneExt\"," +
                        "\"value\":\"" + Convert.ToString(dr["BusinessPhoneExt"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.NumDaysDel\"," +
                        "\"name\":\"app.NumDaysDel\"," +
                        "\"value\":\"" + Convert.ToString(dr["NumDaysDelivered"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.MaxTicket\"," +
                        "\"name\":\"app.MaxTicket\"," +
                        "\"value\":\"" + Convert.ToString(dr["MaxTicket"]).Trim() + "\"" +
                        "}," +
                        strSSNorFedTaxID +
                        strchkCTMFYes +
                        strchkCTMFNo +
                        strchkPrevProcessedYes +
                        strchkPrevProcessedNo +
                        strchkCTMFYes +
                        strchkCTMFNo +
                        strchkPrevProcessedYes +
                        strchkPrevProcessedNo +
                        strchkRefund30Days +
                        strchkExchangeOnly +
                        strchkRefundOther +
                        strOtherRefund +
                        strLegalStatus +
                        strEquipmentModel +
                        strAnnualFee +
                        strComment +
                        strDailyDiscount +
                        strDailyDiscount1 +
                        "{" +
                        "\"tabLabel\":\"app.Keyed\"," +
                        "\"name\":\"app.Keyed\"," +
                        "\"value\":\"" + dr["ProcessPctKeyedWImprint"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.KeyedWO\"," +
                        "\"name\":\"app.KeyedWO\"," +
                        "\"value\":\"" + dr["ProcessPctKeyedwoImprint"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.Swiped\"," +
                        "\"name\":\"app.Swiped\"," +
                        "\"value\":\"" + dr["ProcessPctSwiped"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.MailOrder\"," +
                        "\"name\":\"app.MailOrder\"," +
                        "\"value\":\"" + dr["BusinessPctMailOrder"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.RetailOrder\"," +
                        "\"name\":\"app.RetailOrder\"," +
                        "\"value\":\"" + dr["BusinessPctRetailOrder"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.Internet\"," +
                        "\"name\":\"app.Internet\"," +
                        "\"value\":\"" + dr["BusinessPctInternet"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.P1ZipCodecp\"," +
                        "\"name\":\"app.P1ZipCodecp\"," +
                        "\"value\":\"" + Convert.ToString(dr["P1ZipCode"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.P1ZipCode\"," +
                        "\"name\":\"app.P1ZipCode\"," +
                        "\"value\":\"" + Convert.ToString(dr["P1ZipCode"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.P1State\"," +
                        "\"name\":\"app.P1State\"," +
                        "\"value\":\"" + Convert.ToString(dr["P1State"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.P1Statecp\"," +
                        "\"name\":\"app.P1Statecp\"," +
                        "\"value\":\"" + Convert.ToString(dr["P1State"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.P1City\"," +
                        "\"name\":\"app.P1City\"," +
                        "\"value\":\"" + Convert.ToString(dr["P1City"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.P1Citycp\"," +
                        "\"name\":\"app.P1Citycp\"," +
                        "\"value\":\"" + Convert.ToString(dr["P1City"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.P1Address\"," +
                        "\"name\":\"app.P1Address\"," +
                        "\"value\":\"" + Convert.ToString(dr["P1Address"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.P1Addresscp\"," +
                        "\"name\":\"app.P1Addresscp\"," +
                        "\"value\":\"" + Convert.ToString(dr["P1Address"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.P1SSN\"," +
                        "\"name\":\"app.P1SSN\"," +
                        "\"value\":\"" + Convert.ToString(dr["P1SSN"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.P1SSNcp\"," +
                        "\"name\":\"app.P1SSNcp\"," +
                        "\"value\":\"" + Convert.ToString(dr["P1SSN"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.P1Title\"," +
                        "\"name\":\"app.P1Title\"," +
                        "\"value\":\"" + Convert.ToString(dr["P1Title"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.P1Title1\"," +
                        "\"name\":\"app.P1Title1\"," +
                        "\"value\":\"" + Convert.ToString(dr["P1Title"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.P1Title1bd\"," +
                        "\"name\":\"app.P1Title1bd\"," +
                        "\"value\":\"" + Convert.ToString(dr["P1Title"]).Trim() + "\"" +
                        "}," +
                         "{" +
                        "\"tabLabel\":\"app.P1Titlecp\"," +
                        "\"name\":\"app.P1Titlecp\"," +
                        "\"value\":\"" + Convert.ToString(dr["P1Title"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.P1Title2\"," +
                        "\"name\":\"app.P1Title2\"," +
                        "\"value\":\"" + Convert.ToString(dr["P1Title"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.P1Name\"," +
                        "\"name\":\"app.P1Name\"," +
                        "\"value\":\"" + dr["P1FirstName"].ToString().Trim() + " " + dr["P1LastName"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.P1Name1\"," +
                        "\"name\":\"app.P1Name1\"," +
                        "\"value\":\"" + dr["P1FirstName"].ToString().Trim() + " " + dr["P1LastName"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.P1Name2\"," +
                        "\"name\":\"app.P1Name2\"," +
                        "\"value\":\"" + dr["P1FirstName"].ToString().Trim() + " " + dr["P1LastName"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.P1Name3\"," +
                        "\"name\":\"app.P1Name3\"," +
                        "\"value\":\"" + dr["P1FirstName"].ToString().Trim() + " " + dr["P1LastName"].ToString().Trim() + "\"" +
                        "}," +
                         "{" +
                        "\"tabLabel\":\"app.P1Name4\"," +
                        "\"name\":\"app.P1Name4\"," +
                        "\"value\":\"" + dr["P1FirstName"].ToString().Trim() + " " + dr["P1LastName"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.P1Namecp\"," +
                        "\"name\":\"app.P1Namecp\"," +
                        "\"value\":\"" + dr["P1FirstName"].ToString().Trim() + " " + dr["P1LastName"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.P1Ownershipcp\"," +
                        "\"name\":\"app.P1Ownershipcp\"," +
                        "\"value\":\"" + Convert.ToString(dr["P1OwnershipPercent"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.P1Ownership\"," +
                        "\"name\":\"app.P1Ownership\"," +
                        "\"value\":\"" + Convert.ToString(dr["P1OwnershipPercent"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.P1DOB\"," +
                        "\"name\":\"app.P1DOB\"," +
                        "\"value\":\"" + Convert.ToString(dr["P1DOB"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.P1DOBcp\"," +
                        "\"name\":\"app.P1DOBcp\"," +
                        "\"value\":\"" + Convert.ToString(dr["P1DOB"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.P1DState\"," +
                        "\"name\":\"app.P1DState\"," +
                        "\"value\":\"" + Convert.ToString(dr["P1DriversLicenseState"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.P1DriversLicense\"," +
                        "\"name\":\"app.P1DriversLicense\"," +
                        "\"value\":\"" + Convert.ToString(dr["P1DriversLicenseNo"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.P1HomePhone\"," +
                        "\"name\":\"app.P1HomePhone\"," +
                        "\"value\":\"" + Convert.ToString(dr["P1PhoneNumber"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.P1HomePhonecp\"," +
                        "\"name\":\"app.P1HomePhonecp\"," +
                        "\"value\":\"" + Convert.ToString(dr["P1PhoneNumber"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.P1TimeAtAddress\"," +
                        "\"name\":\"app.P1TimeAtAddress\"," +
                        "\"value\":\"" + dr["P1TimeAtAddress"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.P1TimeAtAddresscp\"," +
                        "\"name\":\"app.P1TimeAtAddresscp\"," +
                        "\"value\":\"" + dr["P1TimeAtAddress"].ToString().Trim() + "\"" +
                        "}," +

                         "{" +
                        "\"tabLabel\":\"app.P1Email\"," +
                        "\"name\":\"app.P1Email\"," +
                        "\"value\":\"" + dr["P1Email"].ToString().Trim() + "\"" +
                        "}," +

                         strP1LivingStatus +
                         strP2 +
                        "{" +
                        "\"tabLabel\":\"app.AvgTicket\"," +
                        "\"name\":\"app.AvgTicket\"," +
                        "\"value\":\"" + Convert.ToString(dr["AverageTicket"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.MonthlySalesProcessingLimit\"," +
                        "\"name\":\"app.MonthlySalesProcessingLimit\"," +
                        "\"value\":\"" + Convert.ToString(dr["MonthlyVolume"]).Trim() + "\"" +
                        "}," +
                        strDiscountRate
                        +
                        offlineDebt
                        +
                        "{" +
                        "\"tabLabel\":\"app.TransactionFee\"," +
                        "\"name\":\"app.TransactionFee\"," +
                        "\"value\":\"" + Convert.ToString(dr["TransactionFee"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.AmexTransactionFee\"," +
                        "\"name\":\"app.AmexTransactionFee\"," +
                        "\"value\":\"" + Convert.ToString(dr["TransactionFee"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.AmexTransactionFeePT\"," +
                        "\"name\":\"app.AmexTransactionFeePT\"," +
                        "\"value\":\"" + Convert.ToString(dr["TransactionFee"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.CustServFee\"," +
                        "\"name\":\"app.CustServFee\"," +
                        "\"value\":\"" + Convert.ToString(dr["CustServFee"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.MonthlyMinDiscFee\"," +
                        "\"name\":\"app.MonthlyMinDiscFee\"," +
                        "\"value\":\"" + Convert.ToString(dr["MonMin"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.RetrievalRequest\"," +
                        "\"name\":\"app.RetrievalRequest\"," +
                        "\"value\":\"" + Convert.ToString(dr["RetrievalFee"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.ChargeBacks\"," +
                        "\"name\":\"app.ChargeBacks\"," +
                        "\"value\":\"" + Convert.ToString(dr["ChargebackFee"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.ApplicationFee\"," +
                        "\"name\":\"app.ApplicationFee\"," +
                        "\"value\":\"" + Convert.ToString(dr["AppFee"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.SetupFee\"," +
                        "\"name\":\"app.SetupFee\"," +
                        "\"value\":\"" + Convert.ToString(dr["AppSetupFee"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.AVS\"," +
                        "\"name\":\"app.AVS\"," +
                        "\"value\":\"" + Convert.ToString(dr["AVS"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.BatchHeader\"," +
                        "\"name\":\"app.BatchHeader\"," +
                        "\"value\":\"" + Convert.ToString(dr["BatchHeader"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.VoiceAuth\"," +
                        "\"name\":\"app.VoiceAuth\"," +
                        "\"value\":\"" + Convert.ToString(dr["VoiceAuth"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.NBCTransactionFee\"," +
                        "\"name\":\"app.NBCTransactionFee\"," +
                        "\"value\":\"" + Convert.ToString(dr["NBCTransFee"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.MCC\"," +
                        "\"name\":\"app.MCC\"," +
                        "\"value\":\"" + Convert.ToString(dr["MCCCategoryCode"]).Trim() + "\"" +
                        "}," +
                        strMidQual +
                        strNonQual +
                        strWireless +
                        strDebit +
                        strEBT +
                        strGateway +
                        strInternetStmt +
                        strInterchange +

                        /*
                        "{" +
                        "\"tabLabel\":\"app.GatewayMonthlyAccess\"," +
                        "\"name\":\"app.GatewayMonthlyAccess\"," +
                        "\"value\":\"" + Convert.ToString(dr["GatewayMonFee"]).Trim() + "\"" +
                        "}," + 
                        "{" +
                        "\"tabLabel\":\"app.GatewayTransationFee\"," +
                        "\"name\":\"app.GatewayTransationFee\"," +
                        "\"value\":\"" + Convert.ToString(dr["GatewayTransFee"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.GatewaySetupFee\"," +
                        "\"name\":\"app.GatewaySetupFee\"," +
                        "\"value\":\"" + Convert.ToString(dr["GatewaySetupFee"]).Trim() + "\"" +
                        "}," +
                        */

                        "{" +
                        "\"tabLabel\":\"app.JCBNum\"," +
                        "\"name\":\"app.JCBNum\"," +
                        "\"value\":\"" + Convert.ToString(dr["PrevJCBNum"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.BankName\"," +
                        "\"name\":\"app.BankName\"," +
                        "\"value\":\"" + Convert.ToString(dr["BankName"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.BankAddress\"," +
                        "\"name\":\"app.BankAddress\"," +
                        "\"value\":\"" + Convert.ToString(dr["BankAddress"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.BankCity\"," +
                        "\"name\":\"app.BankCity\"," +
                        "\"value\":\"" + Convert.ToString(dr["BankCity"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.BankState\"," +
                        "\"name\":\"app.BankState\"," +
                        "\"value\":\"" + Convert.ToString(dr["BankState"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.BankZip\"," +
                        "\"name\":\"app.BankZip\"," +
                        "\"value\":\"" + Convert.ToString(dr["BankZip"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.BankPhone\"," +
                        "\"name\":\"app.BankPhone\"," +
                        "\"value\":\"" + Convert.ToString(dr["BankPhone"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.RoutingNum\"," +
                        "\"name\":\"app.RoutingNum\"," +
                        "\"value\":\"" + Convert.ToString(dr["BankRoutingNumber"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.AcctNum\"," +
                        "\"name\":\"app.AcctNum\"," +
                        "\"value\":\"" + Convert.ToString(dr["BankAccountNumber"]).Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"app.DiscoverNum\"," +
                        "\"name\":\"app.DiscoverNum\"," +
                        "\"value\":\"" + Convert.ToString(dr["PrevDiscoverNum"]).Trim() + "\"" +
                        "}," +
                        strAmex +
                        strPlatform +
                        strDailyDiscount +
                        strDailyDiscount1 +
                        "]," +
                        "}" +
                    "}," +
                        strCoSigner +
                        "]" +
                        "}";


        }
        catch (Exception err)
        {
            //using (WebResponse response = err.Response)
            //{
            //HttpWebResponse httpResponse = (HttpWebResponse)response;
            //Console.WriteLine("Error code: {0}", httpResponse.StatusCode);
            //using (Stream data = response.GetResponseStream())
            //{
            CreateOnlineAppLog Log = new CreateOnlineAppLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "SetRates Page Load - " + err.Message);
            SetErrorMessage(err.Message);
            //}
            //}
        }

        return requestBody;
    }

    protected string CardConncetDocusignRequest(DataRow dr)
    {
        string requestBody = "";
        string strCheckBox = "";

        try
        {
            //string templateId = "0D30D4FE-4D1F-49E3-8F0A-A2B6C5F996BB"; //Test Template
            string templateId = "1B41DFA1-A102-474A-9477-4AEEE45241AC";
            if ((dr["LegalStatus"].ToString().Trim() == "Corporation") || (dr["LegalStatus"].ToString().Trim() == "LLC"))
            {
                templateId = "B91D5D56-FC1F-485A-AAAA-94458926303A";
            }



            string strAmex = "";
            string strDiscover = "";
            string strP2 = "";

            if (dr["AmexAccept"].ToString().Trim() == "Yes - Existing")
            {
                strAmex = "{" +
                            "\"tabLabel\":\"AMX #\"," +
                            "\"name\":\"AMX #\"," +
                            "\"value\":\"" + dr["AmexNum"].ToString().Trim() + "\"" +
                            "},";
            }

            if (dr["AmexAccept"].ToString().Trim() == "Yes")
            {
                if ((dr["Interchange"].ToString().Trim() != "True"))
                {
                    strAmex = "{" +
                               "\"tabLabel\":\"Amex Disc\"," +
                               "\"name\":\"Amex Disc\"," +
                               "\"value\":\"" + dr["DiscountRate"].ToString().Trim() + "\"" +
                               "}," +
                               "{" +
                               "\"tabLabel\":\"Amex Disc mid\"," +
                               "\"name\":\"Amex Disc mid\"," +
                               "\"value\":\"" + dr["DiscRateMidQual"].ToString().Trim() + "\"" +
                               "}," +
                                "{" +
                               "\"tabLabel\":\"Amex Disc Non\"," +
                               "\"name\":\"Amex Disc Non\"," +
                               "\"value\":\"" + dr["DiscRateNonQual"].ToString().Trim() + "\"" +
                               "}," +
                               "{" +
                               "\"tabLabel\":\"Amex Tran\"," +
                               "\"name\":\"Amex Tran\"," +
                               "\"value\":\"" + dr["TransactionFee"].ToString().Trim() + "\"" +
                               "}," +
                               "{" +
                               "\"tabLabel\":\"Amex Tran Mid\"," +
                               "\"name\":\"Amex Tran Mid\"," +
                               "\"value\":\"" + dr["TransactionFee"].ToString().Trim() + "\"" +
                               "}," +
                               "{" +
                               "\"tabLabel\":\"Amex Tran Non\"," +
                               "\"name\":\"Amex Tran Non\"," +
                               "\"value\":\"" + dr["TransactionFee"].ToString().Trim() + "\"" +
                               "},"
                               ;
                }

                else
                {
                    strAmex = "{" +
                            "\"tabLabel\":\"Amex Disc int\"," +
                            "\"name\":\"Amex Disc int\"," +
                            "\"value\":\"" + dr["DiscountRate"].ToString().Trim() + "\"" +
                            "},";
                }
            }


            if (dr["DiscoverAccept"].ToString().Trim() == "Yes - Existing")
            {
                strAmex = "{" +
                            "\"tabLabel\":\"discover #\"," +
                            "\"name\":\"discover #\"," +
                            "\"value\":\"" + dr["DiscoverNum"].ToString().Trim() + "\"" +
                            "},";
            }

            int iPctMOTO = Convert.ToInt32(dr["BusinessPctMailOrder"].ToString().Trim()) + Convert.ToInt32(dr["BusinessPctPhoneOrder"].ToString().Trim());

            if (dr["PrevProcessed"].ToString().Trim() == "Yes")
            {
                strCheckBox = strCheckBox + "{" +
                      "\"tabLabel\":\"processedYes\"," +
                      "\"name\":\"processedYes\"," +
                      "\"selected\":\"" + "true" + "\"" +
                      "},";
            }
            else if (dr["PrevProcessed"].ToString().Trim() == "No")
            {
                strCheckBox = strCheckBox + "{" +
                      "\"tabLabel\":\"processedNo\"," +
                      "\"name\":\"processedNo\"," +
                      "\"selected\":\"" + "true" + "\"" +
                      "},";
            }




            string strMailphonedays = "";
            int iNumDaysProdDel = Convert.ToInt32(dr["NumOfDaysProdDel"].ToString().Trim());
            //if (iNumDaysProdDel == 0)
            //acroFields.SetField("% mail/phone  days 0", "100");
            if ((iNumDaysProdDel >= 0) || (iNumDaysProdDel <= 7))
            {
                strMailphonedays = strMailphonedays + "{" +
                      "\"tabLabel\":\"% mail/phone  days 1\"," +
                      "\"name\":\"% mail/phone  days 1\"," +
                      "\"selected\":\"" + "100" + "\"" +
                      "},";
            }
            else if ((iNumDaysProdDel >= 8) || (iNumDaysProdDel <= 14))
            {
                strMailphonedays = strMailphonedays + "{" +
                      "\"tabLabel\":\"% mail/phone  days 8\"," +
                      "\"name\":\"% mail/phone  days 8\"," +
                      "\"selected\":\"" + "100" + "\"" +
                      "},";
            }
            else if ((iNumDaysProdDel >= 15) || (iNumDaysProdDel <= 30))
            {
                strMailphonedays = strMailphonedays + "{" +
                      "\"tabLabel\":\"% mail/phone  days 15\"," +
                      "\"name\":\"% mail/phone  days 15\"," +
                      "\"selected\":\"" + "100" + "\"" +
                      "},";
            }
            else if (iNumDaysProdDel > 30)
            {
                strMailphonedays = strMailphonedays + "{" +
                      "\"tabLabel\":\"% mail/phone  days 30\"," +
                      "\"name\":\"% mail/phone  days 30\"," +
                      "\"selected\":\"" + "100" + "\"" +
                      "},";
            }

            string strRefundPolicy = "";
            if (dr["RefundPolicy"].ToString().Trim() == "Exchange Only")
            {
                strRefundPolicy = "{" +
                        "\"tabLabel\":\"SD refund policy\"," +
                        "\"name\":\"SD refund policy\"," +
                        "\"value\":\"" + "exchange" + "\"" +
                        "},";
            }
            else if (dr["RefundPolicy"].ToString().Trim() == "Refund within 30 days")
            {
                strRefundPolicy = "{" +
                        "\"tabLabel\":\"SD refund policy\"," +
                        "\"name\":\"SD refund policy\"," +
                        "\"value\":\"" + "MC/visacredit" + "\"" +
                        "},";
            }
            else
            {
                strRefundPolicy = "{" +
                        "\"tabLabel\":\"SD refund policy\"," +
                        "\"name\":\"SD refund policy\"," +
                        "\"value\":\"" + "other" + "\"" +
                        "},";
            }

            string strBankruptcy = "";
            if (dr["Bankruptcy"].ToString().Trim().ToLower() == "yes")
                strBankruptcy = "{" +
                        "\"tabLabel\":\"bankruptcy\"," +
                        "\"name\":\"bankruptcy\"," +
                        "\"value\":\"" + "Yes" + "\"" +
                        "},";
            else
            {
                strBankruptcy = "{" +
                        "\"tabLabel\":\"bankruptcy\"," +
                        "\"name\":\"bankruptcy\"," +
                        "\"value\":\"" + "no" + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"bankruptcy 2nd\"," +
                        "\"name\":\"bankruptcy 2nd\"," +
                        "\"value\":\"" + "no" + "\"" +
                        "},"
                        ;
            }

            if (dr["LegalStatus"].ToString().Trim() == "Sole Proprietorship")
            {
                strCheckBox = strCheckBox + "{" +
                      "\"tabLabel\":\"Sole proprietor\"," +
                      "\"name\":\"Sole proprietor\"," +
                      "\"selected\":\"" + "true" + "\"" +
                      "},";
            }
            else if (dr["LegalStatus"].ToString().Trim() == "Partnership")
            {
                strCheckBox = strCheckBox + "{" +
                      "\"tabLabel\":\"Partnership\"," +
                      "\"name\":\"Partnership\"," +
                      "\"selected\":\"" + "true" + "\"" +
                      "},";
            }
            else if (dr["LegalStatus"].ToString().Trim() == "LLC")
            {
                strCheckBox = strCheckBox + "{" +
                      "\"tabLabel\":\"LLC\"," +
                      "\"name\":\"LLC\"," +
                      "\"selected\":\"" + "true" + "\"" +
                      "},";
            }
            else if (dr["LegalStatus"].ToString().Trim() == "Corporation")
            {
                strCheckBox = strCheckBox + "{" +
                      "\"tabLabel\":\"Corporation\"," +
                      "\"name\":\"Corporation\"," +
                      "\"selected\":\"" + "true" + "\"" +
                      "},";
            }
            else if (dr["LegalStatus"].ToString().Trim() == "Government")
            {
                strCheckBox = strCheckBox + "{" +
                      "\"tabLabel\":\"Government\"," +
                      "\"name\":\"Government\"," +
                      "\"selected\":\"" + "true" + "\"" +
                      "},";
            }
            else if (dr["LegalStatus"].ToString().Trim() == "Non-Profit")
            {
                strCheckBox = strCheckBox + "{" +
                      "\"tabLabel\":\"Non-Profit\"," +
                      "\"name\":\"Non-Profit\"," +
                      "\"selected\":\"" + "true" + "\"" +
                      "},";
            }
            else
            {
                strCheckBox = strCheckBox + "{" +
                      "\"tabLabel\":\"Other\"," +
                      "\"name\":\"Other\"," +
                      "\"selected\":\"" + "true" + "\"" +
                      "},";
            }

            if (!Convert.IsDBNull(dr["Gateway"]))
            {
                if (Convert.ToString(dr["Gateway"]).Trim() != "")
                {
                    if (Convert.ToString(dr["Gateway"]).Trim().ToLower().Contains("authorize.net"))
                    {
                        strCheckBox = strCheckBox + "{" +
                              "\"tabLabel\":\"AuthnetChk\"," +
                                   "\"name\":\"AuthnetChk\"," +
                                   "\"value\":\"" + "true" + "\"" +
                                   "}," +
                                   "{" +
                            "\"tabLabel\":\"AuthnetChk\"," +
                            "\"name\":\"AuthnetChk\"," +
                            "\"value\":\"" + "Yes" + "\"" +
                            "},";
                    }
                    else if (Convert.ToString(dr["Gateway"]).Trim().ToLower().Contains("roampay"))
                    {
                        strCheckBox = strCheckBox + "{" +
                             "\"tabLabel\":\"RoampayChk\"," +
                                  "\"name\":\"RoampayChk\"," +
                                  "\"value\":\"" + "true" + "\"" +
                                  "}," +

                                  "{" +
                            "\"tabLabel\":\"RoampayChk\"," +
                            "\"name\":\"RoampayChk\"," +
                            "\"value\":\"" + "Yes" + "\"" +
                            "},";
                    }
                }
            }


            string strRates = "";

            if ((Convert.ToBoolean(dr["Interchange"])) || (Convert.ToBoolean(dr["Assessments"])))
            {
                string debitTrans = "";
                if (Convert.ToBoolean(dr["OnlineDebit"]) == true)
                {
                    debitTrans = "{" +
                    "\"tabLabel\":\"trans fee 12\"," +
                    "\"name\":\"trans fee 12\"," +
                    "\"value\":\"" + dr["DebitTransFee"].ToString().Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"disc rate 2\"," +
                    "\"name\":\"disc rate 2\"," +
                    "\"value\":\"" + dr["DiscountRateDebit"].ToString().Trim() + "\"" +
                    "},"
                    ;
                }

                strRates = "{" +
                            "\"tabLabel\":\"interchangeYes\"," +
                            "\"name\":\"interchangeYes\"," +
                            "\"value\":\"" + "Yes" + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"interchange disc rate 1\"," +
                            "\"name\":\"interchange disc rate 1\"," +
                            "\"value\":\"" + dr["DiscountRate"].ToString().Trim() + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"debit disc rate int\"," +
                            "\"name\":\"debit disc rate int\"," +
                            "\"value\":\"" + dr["DiscountRateDebit"].ToString().Trim() + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"interchange disc rate 1\"," +
                            "\"name\":\"interchange disc rate 1\"," +
                            "\"value\":\"" + dr["DiscountRate"].ToString().Trim() + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"debit disc rate int\"," +
                            "\"name\":\"debit disc rate int\"," +
                            "\"value\":\"" + dr["DiscountRateDebit"].ToString().Trim() + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"interchange disc rate 1\"," +
                            "\"name\":\"interchange disc rate 1\"," +
                            "\"value\":\"" + dr["DiscountRate"].ToString().Trim() + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"debit disc rate int\"," +
                            "\"name\":\"debit disc rate int\"," +
                            "\"value\":\"" + dr["DiscountRateDebit"].ToString().Trim() + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"trans fee interchange\"," +
                            "\"name\":\"trans fee interchange\"," +
                            "\"value\":\"" + dr["DebitTransFee"].ToString().Trim() + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"trans fee interchange\"," +
                            "\"name\":\"trans fee interchange\"," +
                            "\"value\":\"" + dr["TransactionFee"].ToString().Trim() + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"auth fee 6\"," +
                            "\"name\":\"auth fee 6\"," +
                            "\"value\":\"" + dr["NBCTransFee"].ToString().Trim() + "\"" +
                            "}," +
                             "{" +
                            "\"tabLabel\":\"auth fee 7\"," +
                            "\"name\":\"auth fee 7\"," +
                            "\"value\":\"" + dr["NBCTransFee"].ToString().Trim() + "\"" +
                            "}," +
                            debitTrans +
                            "{" +
                            "\"tabLabel\":\"OTHER account fee\"," +
                            "\"name\":\"OTHER account fee\"," +
                            "\"value\":\"" + dr["AppSetupFee"].ToString().Trim() + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"OTHER ACH 2fee\"," +
                            "\"name\":\"OTHER ACH 2fee\"," +
                            "\"value\":\"" + "10.00" + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"OTHER annual mem3 fee\"," +
                            "\"name\":\"OTHER annual mem3 fee\"," +
                            "\"value\":\"" + dr["AnnualFee"].ToString().Trim() + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"OTHER 4 batch settlement\"," +
                            "\"name\":\"OTHER 4 batch settlement\"," +
                            "\"value\":\"" + dr["BatchHeader"].ToString().Trim() + "\"" +
                            "}," +
                             "{" +
                            "\"tabLabel\":\"OTHER 5chargeback\"," +
                            "\"name\":\"OTHER 5chargeback\"," +
                            "\"value\":\"" + dr["ChargebackFee"].ToString().Trim() + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"OTHER 9\"," +
                            "\"name\":\"OTHER 9\"," +
                            "\"value\":\"" + "5.00" + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"OTHER 10\"," +
                            "\"name\":\"OTHER 10\"," +
                            "\"value\":\"" + dr["MonMin"].ToString().Trim() + "\"" +
                            "}," +

                            /*
                            "{" +
                            "\"tabLabel\":\"Gateway Item fee\"," +
                            "\"name\":\"Gateway Item fee\"," +
                            "\"value\":\"" + "" + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"Gateway Setup\"," +
                            "\"name\":\"Gateway Setup\"," +
                            "\"value\":\"" + dr["GatewaySetupFee"].ToString().Trim() + "\"" +
                            "}," +
                            */

                            "{" +
                            "\"tabLabel\":\"O Resource fee\"," +
                            "\"name\":\"O Resource fee\"," +
                            "\"value\":\"" + dr["InternetStmt"].ToString().Trim() + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"OTHER retrieval fee\"," +
                            "\"name\":\"OTHER retrieval fee\"," +
                            "\"value\":\"" + dr["RetrievalFee"].ToString().Trim() + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"OTHER statement fee\"," +
                            "\"name\":\"OTHER statement fee\"," +
                            "\"value\":\"" + dr["CustServFee"].ToString().Trim() + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"OTHER voyager fee\"," +
                            "\"name\":\"OTHER voyager fee\"," +
                            "\"value\":\"" + "0.02" + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"OTHER wireless monthly fee\"," +
                            "\"name\":\"OTHER wireless monthly fee\"," +
                            "\"value\":\"" + "0.02" + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"OTHER wireless set up fee.0\"," +
                            "\"name\":\"OTHER wireless set up fee.0\"," +
                            "\"value\":\"" + "0.10" + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"OTHER wireless set up fee.1\"," +
                            "\"name\":\"OTHER wireless set up fee.1\"," +
                            "\"value\":\"" + "0.40" + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"OTHER wireless set up fee.2x\"," +
                            "\"name\":\"OTHER wireless set up fee.2x\"," +
                            "\"value\":\"" + dr["WirelessAccess"].ToString().Trim() + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"O A1\"," +
                            "\"name\":\"O A1\"," +
                            "\"value\":\"" + "0.95" + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"O A3\"," +
                            "\"name\":\"O A3\"," +
                            "\"value\":\"" + dr["VoiceAuth"].ToString().Trim() + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"O A4\"," +
                            "\"name\":\"O A4\"," +
                            "\"value\":\"" + dr["AVS"].ToString().Trim() + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"O A5\"," +
                            "\"name\":\"O A5\"," +
                            "\"value\":\"" + "2.00" + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"O A6\"," +
                            "\"name\":\"O A6\"," +
                            "\"value\":\"" + dr["WirelessTransFee"].ToString().Trim() + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"merchant DBA name\"," +
                            "\"name\":\"merchant DBA name\"," +
                            "\"value\":\"" + dr["DBA"].ToString().Trim() + "\"" +
                            "},"
                            ;
            }
            else
            {
                string debitTrans = "";
                if (Convert.ToBoolean(dr["OnlineDebit"]) == true)
                {
                    debitTrans = "{" +
                    "\"tabLabel\":\"trans fee 12\"," +
                    "\"name\":\"trans fee 12\"," +
                    "\"value\":\"" + dr["DebitTransFee"].ToString().Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"disc rate 2\"," +
                    "\"name\":\"disc rate 2\"," +
                    "\"value\":\"" + dr["DiscountRateDebit"].ToString().Trim() + "\"" +
                    "},"
                    ;
                }

                string DiscMQDebit = Convert.ToString(Convert.ToDouble(dr["DiscountRateDebit"].ToString().Trim()) + (Convert.ToDouble(dr["DiscRateMidQualDebit"].ToString().Trim())));
                string DiscNQDebit = Convert.ToString(Convert.ToDouble(dr["DiscountRateDebit"].ToString().Trim()) + (Convert.ToDouble(dr["DiscRateNonQualDebit"].ToString().Trim())));

                strRates = "{" +
                            "\"tabLabel\":\"disc rate 1\"," +
                            "\"name\":\"disc rate 1\"," +
                            "\"value\":\"" + dr["DiscountRate"].ToString().Trim() + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"disc rate 3\"," +
                            "\"name\":\"disc rate 3\"," +
                            "\"value\":\"" + dr["DiscountRate"].ToString().Trim() + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"disc rate debit\"," +
                            "\"name\":\"disc rate debit\"," +
                            "\"value\":\"" + dr["DiscountRateDebit"].ToString().Trim() + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"disc rate 4\"," +
                            "\"name\":\"disc rate 4\"," +
                            "\"value\":\"" + dr["DiscountRateDebit"].ToString().Trim() + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"disc rate 5\"," +
                            "\"name\":\"disc rate 5\"," +
                            "\"value\":\"" + dr["DiscountRate"].ToString().Trim() + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"disc rate 6\"," +
                            "\"name\":\"disc rate 6\"," +
                            "\"value\":\"" + dr["DiscountRateDebit"].ToString().Trim() + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"disc rate 6\"," +
                            "\"name\":\"disc rate 6\"," +
                            "\"value\":\"" + dr["DiscountRateDebit"].ToString().Trim() + "\"" +
                            "}," +
                             "{" +
                            "\"tabLabel\":\"disc rate m1\"," +
                            "\"name\":\"disc rate m1\"," +
                            "\"value\":\"" + dr["DiscRateMidQual"].ToString().Trim() + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"disc rate m2\"," +
                            "\"name\":\"disc rate m2\"," +
                            "\"value\":\"" + DiscMQDebit + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"disc rate m3\"," +
                            "\"name\":\"disc rate m3\"," +
                            "\"value\":\"" + dr["DiscRateMidQual"].ToString().Trim() + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"disc rate m4\"," +
                            "\"name\":\"disc rate m4\"," +
                            "\"value\":\"" + DiscMQDebit + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"disc rate m5\"," +
                            "\"name\":\"disc rate m5\"," +
                            "\"value\":\"" + dr["DiscRateMidQual"].ToString().Trim() + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"disc rate m6\"," +
                            "\"name\":\"disc rate m6\"," +
                            "\"value\":\"" + DiscMQDebit + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"disc rate n1\"," +
                            "\"name\":\"disc rate n1\"," +
                            "\"value\":\"" + dr["DiscRateNonQual"].ToString().Trim() + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"disc rate n1\"," +
                            "\"name\":\"disc rate n1\"," +
                            "\"value\":\"" + dr["DiscRateNonQual"].ToString().Trim() + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"disc rate n2\"," +
                            "\"name\":\"disc rate n2\"," +
                            "\"value\":\"" + DiscNQDebit + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"disc rate n3\"," +
                            "\"name\":\"disc rate n3\"," +
                            "\"value\":\"" + dr["DiscRateNonQual"].ToString().Trim() + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"disc rate n4\"," +
                            "\"name\":\"disc rate n4\"," +
                            "\"value\":\"" + DiscNQDebit + "\"" +
                            "}," +
                             "{" +
                            "\"tabLabel\":\"disc rate n5\"," +
                            "\"name\":\"disc rate n5\"," +
                            "\"value\":\"" + dr["DiscRateNonQual"].ToString().Trim() + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"disc rate n6\"," +
                            "\"name\":\"disc rate n6\"," +
                            "\"value\":\"" + DiscNQDebit + "\"" +
                            "}," + debitTrans +
                            "{" +
                            "\"tabLabel\":\"trans fee 1\"," +
                            "\"name\":\"trans fee 1\"," +
                            "\"value\":\"" + dr["TransactionFee"].ToString().Trim() + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"trans fee 3\"," +
                            "\"name\":\"trans fee 3\"," +
                            "\"value\":\"" + dr["TransactionFee"].ToString().Trim() + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"trans fee 2\"," +
                            "\"name\":\"trans fee 2\"," +
                            "\"value\":\"" + dr["TransactionFee"].ToString().Trim() + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"trans fee 4\"," +
                            "\"name\":\"trans fee 4\"," +
                            "\"value\":\"" + dr["TransactionFee"].ToString().Trim() + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"trans fee 5\"," +
                            "\"name\":\"trans fee 5\"," +
                            "\"value\":\"" + dr["TransactionFee"].ToString().Trim() + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"trans fee 6\"," +
                            "\"name\":\"trans fee 6\"," +
                            "\"value\":\"" + dr["TransactionFee"].ToString().Trim() + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"trans fee m1\"," +
                            "\"name\":\"trans fee m1\"," +
                            "\"value\":\"" + dr["TransactionFee"].ToString().Trim() + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"trans fee m2\"," +
                            "\"name\":\"trans fee m2\"," +
                            "\"value\":\"" + dr["TransactionFee"].ToString().Trim() + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"trans fee m3\"," +
                            "\"name\":\"trans fee m3\"," +
                            "\"value\":\"" + dr["TransactionFee"].ToString().Trim() + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"trans fee m4\"," +
                            "\"name\":\"trans fee m4\"," +
                            "\"value\":\"" + dr["TransactionFee"].ToString().Trim() + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"trans fee m5\"," +
                            "\"name\":\"trans fee m5\"," +
                            "\"value\":\"" + dr["TransactionFee"].ToString().Trim() + "\"" +
                            "}," +
                             "{" +
                            "\"tabLabel\":\"trans fee m6\"," +
                            "\"name\":\"trans fee m6\"," +
                            "\"value\":\"" + dr["TransactionFee"].ToString().Trim() + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"trans fee n1\"," +
                            "\"name\":\"trans fee n1\"," +
                            "\"value\":\"" + dr["TransactionFee"].ToString().Trim() + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"trans fee n2\"," +
                            "\"name\":\"trans fee n2\"," +
                            "\"value\":\"" + dr["TransactionFee"].ToString().Trim() + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"trans fee n3\"," +
                            "\"name\":\"trans fee n3\"," +
                            "\"value\":\"" + dr["TransactionFee"].ToString().Trim() + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"trans fee n4\"," +
                            "\"name\":\"trans fee n4\"," +
                            "\"value\":\"" + dr["TransactionFee"].ToString().Trim() + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"trans fee n5\"," +
                            "\"name\":\"trans fee n5\"," +
                            "\"value\":\"" + dr["TransactionFee"].ToString().Trim() + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"trans fee n6\"," +
                            "\"name\":\"trans fee n6\"," +
                            "\"value\":\"" + dr["TransactionFee"].ToString().Trim() + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"trans fee 13\"," +
                            "\"name\":\"trans fee 13\"," +
                            "\"value\":\"" + dr["EBTTransFee"].ToString().Trim() + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"auth fee 1\"," +
                            "\"name\":\"auth fee 1\"," +
                            "\"value\":\"" + dr["TransactionFee"].ToString().Trim() + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"auth fee 2\"," +
                            "\"name\":\"auth fee 2\"," +
                            "\"value\":\"" + dr["TransactionFee"].ToString().Trim() + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"auth fee 5\"," +
                            "\"name\":\"auth fee 5\"," +
                            "\"value\":\"" + dr["NBCTransFee"].ToString().Trim() + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"auth fee 7\"," +
                            "\"name\":\"auth fee 7\"," +
                            "\"value\":\"" + dr["NBCTransFee"].ToString().Trim() + "\"" +
                            "}," +
                             "{" +
                            "\"tabLabel\":\"OTHER account fee\"," +
                            "\"name\":\"OTHER account fee\"," +
                            "\"value\":\"" + dr["AppSetupFee"].ToString().Trim() + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"OTHER ACH 2fee\"," +
                            "\"name\":\"OTHER ACH 2fee\"," +
                            "\"value\":\"" + "10.00" + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"OTHER annual mem3 fee\"," +
                            "\"name\":\"OTHER annual mem3 fee\"," +
                            "\"value\":\"" + dr["AnnualFee"].ToString().Trim() + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"OTHER 4 batch settlement\"," +
                            "\"name\":\"OTHER 4 batch settlement\"," +
                            "\"value\":\"" + dr["BatchHeader"].ToString().Trim() + "\"" +
                            "}," +
                             "{" +
                            "\"tabLabel\":\"OTHER 5chargeback\"," +
                            "\"name\":\"OTHER 5chargeback\"," +
                            "\"value\":\"" + dr["ChargebackFee"].ToString().Trim() + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"OTHER 9\"," +
                            "\"name\":\"OTHER 9\"," +
                            "\"value\":\"" + "5.00" + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"OTHER 10\"," +
                            "\"name\":\"OTHER 10\"," +
                            "\"value\":\"" + dr["MonMin"].ToString().Trim() + "\"" +
                            "}," +

                            /*
                            "{" +
                            "\"tabLabel\":\"Gateway Item fee\"," +
                            "\"name\":\"Gateway Item fee\"," +
                            "\"value\":\"" + "" + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"Gateway Setup\"," +
                            "\"name\":\"Gateway Setup\"," +
                            "\"value\":\"" + dr["GatewaySetupFee"].ToString().Trim() + "\"" +
                            "}," +
                             */

                            "{" +
                            "\"tabLabel\":\"O Resource fee\"," +
                            "\"name\":\"O Resource fee\"," +
                            "\"value\":\"" + dr["InternetStmt"].ToString().Trim() + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"OTHER retrieval fee\"," +
                            "\"name\":\"OTHER retrieval fee\"," +
                            "\"value\":\"" + dr["RetrievalFee"].ToString().Trim() + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"OTHER statement fee\"," +
                            "\"name\":\"OTHER statement fee\"," +
                            "\"value\":\"" + dr["CustServFee"].ToString().Trim() + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"OTHER voyager fee\"," +
                            "\"name\":\"OTHER voyager fee\"," +
                            "\"value\":\"" + "0.02" + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"OTHER wireless monthly fee\"," +
                            "\"name\":\"OTHER wireless monthly fee\"," +
                            "\"value\":\"" + "0.02" + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"OTHER wireless set up fee.0\"," +
                            "\"name\":\"OTHER wireless set up fee.0\"," +
                            "\"value\":\"" + "0.10" + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"OTHER wireless set up fee.1\"," +
                            "\"name\":\"OTHER wireless set up fee.1\"," +
                            "\"value\":\"" + "0.40" + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"OTHER wireless set up fee.2x\"," +
                            "\"name\":\"OTHER wireless set up fee.2x\"," +
                            "\"value\":\"" + dr["WirelessAccess"].ToString().Trim() + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"O A1\"," +
                            "\"name\":\"O A1\"," +
                            "\"value\":\"" + "0.95" + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"O A3\"," +
                            "\"name\":\"O A3\"," +
                            "\"value\":\"" + dr["VoiceAuth"].ToString().Trim() + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"O A4\"," +
                            "\"name\":\"O A4\"," +
                            "\"value\":\"" + dr["AVS"].ToString().Trim() + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"O A5\"," +
                            "\"name\":\"O A5\"," +
                            "\"value\":\"" + "2.00" + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"O A6\"," +
                            "\"name\":\"O A6\"," +
                            "\"value\":\"" + dr["WirelessTransFee"].ToString().Trim() + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"merchant DBA name\"," +
                            "\"name\":\"merchant DBA name\"," +
                            "\"value\":\"" + dr["DBA"].ToString().Trim() + "\"" +
                            "}," +
                            "{" +
                            "\"tabLabel\":\"AuthnetChk\"," +
                            "\"name\":\"AuthnetChk\"," +
                            "\"value\":\"" + "Yes" + "\"" +
                            "},"
                            ;

            }

            /*
            if ((dr["AmexNum"].ToString().Trim() != "") && (dr["AmexNum"].ToString().Trim() != "No"))
            {
                if (dr["AmexNum"].ToString().Trim() == "Yes" || Convert.ToDouble(dr["ProcessPctSwiped"].ToString().Trim()) >= 70)
                {
                    templateId = "008E6DD7-9C84-4D8C-AAF3-659CABDC3722";
                    //templateId = "F3C560F4-307E-48DC-840F-34828C294D6B";
                }
            }*/

            string strCoSigner = "";
            if (!Convert.IsDBNull(dr["P2FullName"]))
            {
                if (dr["P2FullName"].ToString().Trim() != "")
                {
                    //templateId = "2AEF4857-8B73-4BA4-9FE9-9BCD16BF9DF9";
                    templateId = "1FB2886D-F178-45F8-BECE-9D6C1806F6E8";
                    if ((dr["LegalStatus"].ToString().Trim() == "Corporation") || (dr["LegalStatus"].ToString().Trim() == "LLC"))
                    {
                        templateId = "12E447F7-386D-4168-983F-FC13BD699CA8";
                    }
                    /*
                    if ((dr["AmexNum"].ToString().Trim() != "") && (dr["AmexNum"].ToString().Trim() != "No"))
                    {
                        if (dr["AmexNum"].ToString().Trim() == "Yes" || Convert.ToDouble(dr["ProcessPctSwiped"].ToString().Trim()) >= 70)
                        {
                            templateId = "6B2EDC25-A012-4099-9C67-51186C9F6F4C";
                            //templateId = "E142A498-88F4-4D61-B91D-B0906B2B5860";
                        }
                    }*/

                    strCoSigner =
                        "{" +
                        "\"email\":\"" + Convert.ToString(dr["P2Email"]).Trim() + "\"," +
                        "\"name\":\"" + Convert.ToString(dr["P2FullName"]).Trim() + "\"," +
                        "\"roleName\":\"CoSigner\"," +
                        "}";
                }
            }

            strP2 = "{" +
            "\"tabLabel\":\"o 2 name\"," +
            "\"name\":\"o 2 name\"," +
            "\"value\":\"" + dr["P2FullName"].ToString().Trim() + "\"" +
            "}," +
            "{" +
            "\"tabLabel\":\"o 2 title\"," +
            "\"name\":\"o 2 title\"," +
            "\"value\":\"" + dr["P2Title"].ToString().Trim() + "\"" +
            "}," +
            "{" +
            "\"tabLabel\":\"o 2 %ownership\"," +
            "\"name\":\"o 2 %ownership\"," +
            "\"value\":\"" + dr["P2OwnershipPercent"].ToString().Trim() + "\"" +
            "}," +
            "{" +
            "\"tabLabel\":\"o 2 res address\"," +
            "\"name\":\"o 2 res address\"," +
            "\"value\":\"" + dr["P2Address"].ToString().Trim() + "\"" +
            "}," +
            "{" +
            "\"tabLabel\":\"o 2 city\"," +
            "\"name\":\"o 2 city\"," +
            "\"value\":\"" + dr["P2City"].ToString().Trim() + "\"" +
            "}," +
            "{" +
            "\"tabLabel\":\"o 2 state\"," +
            "\"name\":\"o 2 state\"," +
            "\"value\":\"" + dr["P2State"].ToString().Trim() + "\"" +
            "}," +
            "{" +
            "\"tabLabel\":\"o 2 zip\"," +
            "\"name\":\"o 2 zip\"," +
            "\"value\":\"" + dr["P2ZipCode"].ToString().Trim() + "\"" +
            "}," +
            "{" +
            "\"tabLabel\":\"o 2 home phone\"," +
            "\"name\":\"o 2 home phone\"," +
            "\"value\":\"" + dr["P2PhoneNumber"].ToString().Trim() + "\"" +
            "}," +
            "{" +
            "\"tabLabel\":\"o 2 ss#\"," +
            "\"name\":\"o 2 ss#\"," +
            "\"value\":\"" + dr["P2SSN"].ToString().Trim() + "\"" +
            "}," +
            "{" +
            "\"tabLabel\":\"o 2 date of birth\"," +
            "\"name\":\"o 2 date of birth\"," +
            "\"value\":\"" + dr["P2DOB"].ToString().Trim() + "\"" +
            "}," +
            "{" +
            "\"tabLabel\":\"o 2 date of birth\"," +
            "\"name\":\"o 2 date of birth\"," +
            "\"value\":\"" + dr["P2DOB"].ToString().Trim() + "\"" +
            "}," +
            "{" +
            "\"tabLabel\":\"o 2 drivers lic\"," +
            "\"name\":\"o 2 drivers lic\"," +
            "\"value\":\"" + dr["P2DriversLicenseNo"].ToString().Trim() + "\"" +
            "}," +
            "{" +
            "\"tabLabel\":\"o 2 state of drivers lic\"," +
            "\"name\":\"o 2 state of drivers lic\"," +
            "\"value\":\"" + dr["P2DriversLicenseState"].ToString().Trim() + "\"" +
            "},";

            requestBody = "{" +
                        "\"status\":\"sent\"," +
                        "\"emailBlurb\":\"Please review and sign you document with Commerce Technologies\"," +
                        "\"emailSubject\": \"Please review and sign you document with Commerce Technologies\"," +
                        "\"templateId\":\"" + templateId + "\"," +
                        "\"templateRoles\":[" +
                        "{" +
                        "\"email\":\"" + Convert.ToString(dr["P1Email"]).Trim() + "\"," +
                        "\"name\":\"" + Convert.ToString(dr["P1FullName"]).Trim() + "\"," +
                        "\"roleName\":\"Signer\"," +
                        "\"clientUserId\":\"" + Convert.ToString(dr["P1FullName"]).Trim() + "\"," +
                        "\"tabs\": {" +
                         "\"checkboxTabs\": [" +
                         strCheckBox +
                        "]," +
               
                        "\"textTabs\": [" +
                         "{" +
                        "\"tabLabel\":\"Discount frequency\"," +
                        "\"name\":\"Discount frequency\"," +
                        "\"value\":\"" + "Monthly" + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"Pricing Method\"," +
                        "\"name\":\"Pricing Method\"," +
                        "\"value\":\"" + "Gross" + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"debit network Interchange\"," +
                        "\"name\":\"debit network Interchange\"," +
                        "\"value\":\"" + "Yes" + "\"" +
                        "}," +




                        "{" +
                        "\"tabLabel\":\"sales rep\"," +
                        "\"name\":\"sales rep\"," +
                        "\"value\":\"" + dr["RepName"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"business legal name\"," +
                        "\"name\":\"business legal name\"," +
                        "\"value\":\"" + dr["CompanyName"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"business legal name 1\"," +
                        "\"name\":\"business legal name 1\"," +
                        "\"value\":\"" + dr["CompanyName"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"business legal name 2\"," +
                        "\"name\":\"business legal name 2\"," +
                        "\"value\":\"" + dr["CompanyName"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"mailing address\"," +
                        "\"name\":\"mailing address\"," +
                        "\"value\":\"" + dr["BillingAddress"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"city\"," +
                        "\"name\":\"city\"," +
                        "\"value\":\"" + dr["BillingCity"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"State\"," +
                        "\"name\":\"State\"," +
                        "\"value\":\"" + dr["BillingState"].ToString().Trim() + "\"" +
                        "}," +
                         "{" +
                        "\"tabLabel\":\"MI zip +4 = limit 10 char only\"," +
                        "\"name\":\"MI zip +4 = limit 10 char only\"," +
                        "\"value\":\"" + dr["BillingZipCode"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"m phone\"," +
                        "\"name\":\"m phone\"," +
                        "\"value\":\"" + dr["BusinessPhone"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"dba fax#\"," +
                        "\"name\":\"dba fax#\"," +
                        "\"value\":\"" + dr["Fax"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"fax#\"," +
                        "\"name\":\"fax#\"," +
                        "\"value\":\"" + dr["Fax"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"dba fax#1\"," +
                        "\"name\":\"dba fax#1\"," +
                        "\"value\":\"" + dr["Fax"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"tax ID #\"," +
                        "\"name\":\"tax ID #\"," +
                        "\"value\":\"" + dr["FederalTaxID"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"total # locations\"," +
                        "\"name\":\"total # locations\"," +
                        "\"value\":\"" + dr["NumOfLocs"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"merchant  doing business as\"," +
                        "\"name\":\"merchant  doing business as\"," +
                        "\"value\":\"" + dr["DBA"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"business start date\"," +
                        "\"name\":\"business start date\"," +
                        "\"value\":\"" + dr["StartYear"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"how long at location\"," +
                        "\"name\":\"how long at location\"," +
                        "\"value\":\"" + dr["YIB"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"location address\"," +
                        "\"name\":\"location address\"," +
                        "\"value\":\"" + dr["Address1"].ToString().Trim() + " " + dr["Address2"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"location city\"," +
                        "\"name\":\"location city\"," +
                        "\"value\":\"" + dr["City"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"location state\"," +
                        "\"name\":\"location state\"," +
                        "\"value\":\"" + dr["State"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"location zip\"," +
                        "\"name\":\"location zip\"," +
                        "\"value\":\"" + dr["ZipCode"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"Country\"," +
                        "\"name\":\"Country\"," +
                        "\"value\":\"" + dr["Country"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"location phone\"," +
                        "\"name\":\"location phone\"," +
                        "\"value\":\"" + dr["BusinessPhone"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"location phone1\"," +
                        "\"name\":\"location phone1\"," +
                        "\"value\":\"" + dr["BusinessPhone"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"primary merchant contact\"," +
                        "\"name\":\"primary merchant contact\"," +
                        "\"value\":\"" + dr["P1FullName"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"primary merchant contact1\"," +
                        "\"name\":\"primary merchant contact1\"," +
                        "\"value\":\"" + dr["P1FullName"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"primary merchant contact2\"," +
                        "\"name\":\"primary merchant contact2\"," +
                        "\"value\":\"" + dr["P1FullName"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"m location email address\"," +
                        "\"name\":\"m location email address\"," +
                        "\"value\":\"" + dr["Email"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"website\"," +
                        "\"name\":\"website\"," +
                        "\"value\":\"" + dr["Website"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"lengthInBus\"," +
                        "\"name\":\"lengthInBus\"," +
                        "\"value\":\"" + dr["YIB"].ToString().Trim() + " years and " + dr["MonthsinBusiness"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"website\"," +
                        "\"name\":\"website\"," +
                        "\"value\":\"" + dr["Website"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"m email address\"," +
                        "\"name\":\"m email address\"," +
                        "\"value\":\"" + dr["Email"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"meerchandise sold\"," +
                        "\"name\":\"meerchandise sold\"," +
                        "\"value\":\"" + dr["ProductSold"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"software coding info\"," +
                        "\"name\":\"software coding info\"," +
                        "\"value\":\"" + dr["Gateway"].ToString().Trim() + "\"" +
                        "}," +
                        strAmex +
                        strDiscover +
                        "{" +
                        "\"tabLabel\":\"% mail/phone\"," +
                        "\"name\":\"% mail/phone\"," +
                        "\"value\":\"" + iPctMOTO.ToString().Trim() + "\"" +
                        "}," +
                         "{" +
                        "\"tabLabel\":\"% internet\"," +
                        "\"name\":\"% internet\"," +
                        "\"value\":\"" + dr["BusinessPctInternet"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"% card swipe\"," +
                        "\"name\":\"% card swipe\"," +
                        "\"value\":\"" + dr["ProcessPctSwiped"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"% hand keyed\"," +
                        "\"name\":\"% hand keyed\"," +
                        "\"value\":\"" + dr["PctFaceToFace"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"% keyed imprint\"," +
                        "\"name\":\"% keyed imprint\"," +
                        "\"value\":\"" + dr["ProcessPctKeyed"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"% keyed wo imprint\"," +
                        "\"name\":\"% keyed wo imprint\"," +
                        "\"value\":\"" + dr["ProcessPctKeyedwoImprint"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"% keyed wo imprint\"," +
                        "\"name\":\"% keyed wo imprint\"," +
                        "\"value\":\"" + dr["ProcessPctKeyedwoImprint"].ToString().Trim() + "\"" +
                        "}," +
                        strMailphonedays +
                         "{" +
                        "\"tabLabel\":\"refund\"," +
                        "\"name\":\"refund\"," +
                        "\"value\":\"" + dr["VisaMasterRefund"].ToString().Trim() + "\"" +
                        "}," +
                        strRefundPolicy +
                        "{" +
                        "\"tabLabel\":\"o name\"," +
                        "\"name\":\"o name\"," +
                        "\"value\":\"" + dr["P1FullName"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"o title\"," +
                        "\"name\":\"o title\"," +
                        "\"value\":\"" + dr["P1Title"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"o %ownership\"," +
                        "\"name\":\"o %ownership\"," +
                        "\"value\":\"" + dr["P1OwnershipPercent"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"o res address\"," +
                        "\"name\":\"o res address\"," +
                        "\"value\":\"" + dr["P1Address"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"o city\"," +
                        "\"name\":\"o city\"," +
                        "\"value\":\"" + dr["P1City"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"o state\"," +
                        "\"name\":\"o state\"," +
                        "\"value\":\"" + dr["P1State"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"o zip\"," +
                        "\"name\":\"o zip\"," +
                        "\"value\":\"" + dr["P1ZipCode"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"o home phone\"," +
                        "\"name\":\"o home phone\"," +
                        "\"value\":\"" + dr["P1PhoneNumber"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"o home phone\"," +
                        "\"name\":\"o home phone\"," +
                        "\"value\":\"" + dr["P1PhoneNumber"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"o ss#\"," +
                        "\"name\":\"o ss#\"," +
                        "\"value\":\"" + dr["P1SSN"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"o date of birth\"," +
                        "\"name\":\"o date of birth\"," +
                        "\"value\":\"" + dr["P1DOB"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"o drivers lic\"," +
                        "\"name\":\"o drivers lic\"," +
                        "\"value\":\"" + dr["P1DriversLicenseNo"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"o state of drivers lic\"," +
                        "\"name\":\"o state of drivers lic\"," +
                        "\"value\":\"" + dr["P1DriversLicenseState"].ToString().Trim() + "\"" +
                        "}," +
                        strP2 +
                        "{" +
                        "\"tabLabel\":\"c annual visa/mastercard vol\"," +
                        "\"name\":\"c annual visa/mastercard vol\"," +
                        "\"value\":\"" + (Convert.ToInt32(dr["AnnualVol"]) / 12).ToString().Trim() + "\"" +
                        "}," +

                        "{" +
                        "\"tabLabel\":\"c aveage credit card ticket\"," +
                        "\"name\":\"c aveage credit card ticket\"," +
                        "\"value\":\"" + dr["AvgTicket"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"c high credit card ticket\"," +
                        "\"name\":\"c high credit card ticket\"," +
                        "\"value\":\"" + dr["MaxTicket"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"del time frame\"," +
                        "\"name\":\"del time frame\"," +
                        "\"value\":\"" + dr["NumOfDaysProdDel"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"bank name\"," +
                        "\"name\":\"bank name\"," +
                        "\"value\":\"" + dr["BankName"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"transit routing\"," +
                        "\"name\":\"transit routing\"," +
                        "\"value\":\"" + dr["BankRoutingNumber"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"account #\"," +
                        "\"name\":\"account #\"," +
                        "\"value\":\"" + dr["BankAccountNumber"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"BR address\"," +
                        "\"name\":\"BR address\"," +
                        "\"value\":\"" + dr["BankAddress"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"BR city\"," +
                        "\"name\":\"BR city\"," +
                        "\"value\":\"" + dr["BankCity"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"BR state\"," +
                        "\"name\":\"BR state\"," +
                        "\"value\":\"" + dr["BankState"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"BR zip\"," +
                        "\"name\":\"BR zip\"," +
                        "\"value\":\"" + dr["BankZip"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"processing bank\"," +
                        "\"name\":\"processing bank\"," +
                        "\"value\":\"" + dr["PrevProcessor"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"leaving\"," +
                        "\"name\":\"leaving\"," +
                        "\"value\":\"" + dr["ReasonforLeaving"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"leaving\"," +
                        "\"name\":\"leaving\"," +
                        "\"value\":\"" + dr["ReasonforLeaving"].ToString().Trim() + "\"" +
                        "}," +
                        strBankruptcy +
                        "{" +
                        "\"tabLabel\":\"business legal name pg2\"," +
                        "\"name\":\"business legal name pg2\"," +
                        "\"value\":\"" + dr["CompanyName"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"M B title\"," +
                        "\"name\":\"M B title\"," +
                        "\"value\":\"" + dr["P1Title"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"M B title1\"," +
                        "\"name\":\"M B title1\"," +
                        "\"value\":\"" + dr["P1Title"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"M B print name\"," +
                        "\"name\":\"M B print name\"," +
                        "\"value\":\"" + dr["P1FullName"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"M B title 2\"," +
                        "\"name\":\"M B title 2\"," +
                        "\"value\":\"" + dr["P2Title"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"M B print name 2\"," +
                        "\"name\":\"M B print name 2\"," +
                        "\"value\":\"" + dr["P2FullName"].ToString().Trim() + "\"" +
                        "}," +
                        "{" +
                        "\"tabLabel\":\"M B print name 2\"," +
                        "\"name\":\"M B print name 2\"," +
                        "\"value\":\"" + dr["P2FullName"].ToString().Trim() + "\"" +
                        "}," +
                        strRates +
                        "]," +
                        "}" +
                    "}," +
                        strCoSigner +
                        "]" +
                        "}";

        }
        catch (Exception err)
        {
            //using (WebResponse response = err.Response)
            //{
            //HttpWebResponse httpResponse = (HttpWebResponse)response;
            //Console.WriteLine("Error code: {0}", httpResponse.StatusCode);
            //using (Stream data = response.GetResponseStream())
            //{
            CreateOnlineAppLog Log = new CreateOnlineAppLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "SetRates Page Load - " + err.Message);
            SetErrorMessage(err.Message);
            //}
            //}
        }

        return requestBody;
    }

    protected string iPaymentDocusignRequest(DataRow dr)
    {
        string platForm = "";
        string templateId = "008E6DD7-9C84-4D8C-AAF3-659CABDC3722";
        //string templateId = "F3C560F4-307E-48DC-840F-34828C294D6B";
        string strCoSigner = "";
        if (!Convert.IsDBNull(dr["P2FirstName"]))
        {
            if (dr["P2FirstName"].ToString().Trim() != "")
            {
                templateId = "6B2EDC25-A012-4099-9C67-51186C9F6F4C";
                //templateId = "E142A498-88F4-4D61-B91D-B0906B2B5860";
                strCoSigner =
                    "{" +
                    "\"email\":\"" + Convert.ToString(dr["P2Email"]).Trim() + "\"," +
                    "\"name\":\"" + Convert.ToString(dr["P2FirstName"]).Trim() + " " + Convert.ToString(dr["P2LastName"]).Trim() + "\"," +
                    "\"roleName\":\"CoSigner\"," +
                    "}";
            }
        }





        if (dr["Platform"].ToString().Trim().Contains("Paymentech"))
            platForm = "Paymentech";
        else
            platForm = "Visanet/TSYS";

        string BusinessOpenMonth = "";
        string BusinessOpenYear = "";
        string LengthOfOwnership = "";
        string prevProcNum = "";

        if ((dr["MIB"].ToString().Trim() != "") && (dr["YIB"].ToString().Trim() != ""))
        {
            DateTime BusinessDate = DateTime.Now.AddMonths((Convert.ToInt32(dr["MIB"].ToString().Trim())) * -1);
            DateTime BusinessOpenDate = BusinessDate.AddYears((Convert.ToInt32(dr["YIB"].ToString().Trim())) * -1);
            BusinessOpenMonth = BusinessOpenDate.Month.ToString().Trim();
            BusinessOpenYear = BusinessOpenDate.Year.ToString().Trim();
            //data.Add("Business Open Date", BusinessOpenMonth + "/" + BusinessOpenYear);

            LengthOfOwnership = "";
            if (dr["YIB"].ToString().Trim() != "")
                LengthOfOwnership = dr["YIB"].ToString().Trim() + " Years ";
            else
                LengthOfOwnership = "0 Years ";

            if (dr["MIB"].ToString().Trim() != "")
                LengthOfOwnership += "and " + dr["MIB"].ToString().Trim() + " Months";
        }

        if (dr["PrevProcessor"].ToString().Contains("Sage"))
        {
            prevProcNum = Convert.ToString(dr["PrevMerchantAcctNo"]).Trim();
        }

        string strOwnershipType = "";


        if (dr["LegalStatus"].ToString().ToLower().Contains("trust"))
        {

            strOwnershipType = "{" +
                    "\"tabLabel\":\"OwnershipTypeAsso\"," +
                    "\"name\":\"OwnershipTypeAsso\"," +
                    "\"value\":\"" + "x" + "\"" +
                    "},";
        }
        else if (dr["LegalStatus"].ToString().ToLower().Contains("llc"))
        {
            strOwnershipType = "{" +
                    "\"tabLabel\":\"OwnershipTypellc\"," +
                    "\"name\":\"OwnershipTypellc\"," +
                    "\"value\":\"" + "x" + "\"" +
                    "},";
        }
        else if ((dr["LegalStatus"].ToString().ToLower().Contains("non-profit")) || (dr["LegalStatus"].ToString().ToLower().Contains("tax exempt")))
        {
            strOwnershipType = "{" +
                    "\"tabLabel\":\"OwnershipTypeTaxExmpt\"," +
                    "\"name\":\"OwnershipTypeTaxExmpt\"," +
                    "\"value\":\"" + "x" + "\"" +
                    "},";
        }
        else if (dr["LegalStatus"].ToString().ToLower().Contains("corporation"))
        {
            strOwnershipType = "{" +
                    "\"tabLabel\":\"OwnershipTypeCorp\"," +
                    "\"name\":\"OwnershipTypeCorp\"," +
                    "\"value\":\"" + "x" + "\"" +
                    "},";
        }
        else if (dr["LegalStatus"].ToString().ToLower().Contains("medical"))
        {
            strOwnershipType = "{" +
                    "\"tabLabel\":\"OwnershipTypemedical\"," +
                    "\"name\":\"OwnershipTypemedical\"," +
                    "\"value\":\"" + "x" + "\"" +
                    "},";
        }
        else if (dr["LegalStatus"].ToString().ToLower().Contains("government"))
        {
            strOwnershipType = "{" +
                        "\"tabLabel\":\"OwnershipTypeGov\"," +
                        "\"name\":\"OwnershipTypeGov\"," +
                        "\"value\":\"" + "x" + "\"" +
                        "},";
        }
        else if (dr["LegalStatus"].ToString().ToLower().Contains("int'l"))
        {
            strOwnershipType = "{" +
                        "\"tabLabel\":\"OwnershipTypeintl\"," +
                        "\"name\":\"OwnershipTypeintl\"," +
                        "\"value\":\"" + "x" + "\"" +
                        "},";
        }
        else if (dr["LegalStatus"].ToString().ToLower().Contains("partnership"))
        {
            strOwnershipType = "{" +
                        "\"tabLabel\":\"OwnershipTypepartnership\"," +
                        "\"name\":\"OwnershipTypepartnership\"," +
                        "\"value\":\"" + "x" + "\"" +
                        "},";
        }
        else if (dr["LegalStatus"].ToString().ToLower().Contains("sole proprietorship"))
        {
            strOwnershipType = "{" +
                        "\"tabLabel\":\"OwnershipTypeSol\"," +
                        "\"name\":\"OwnershipTypeSol\"," +
                        "\"value\":\"" + "x" + "\"" +
                        "},";
        }

        string strP2 = "";

        if (!Convert.IsDBNull(dr["P2FirstName"]))
        {
            if (dr["P2FirstName"].ToString().Trim() != "")
            {
                strP2 = "{" +
                    "\"tabLabel\":\"P2 Ownership\"," +
                    "\"name\":\"P2 Ownership\"," +
                    "\"value\":\"" + Convert.ToString(dr["P2OwnershipPercent"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"P2 Full Name\"," +
                    "\"name\":\"P2 Full Name\"," +
                    "\"value\":\"" + Convert.ToString(dr["P2FirstName"]).Trim() + " " + Convert.ToString(dr["P2LastName"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"P2 Title\"," +
                    "\"name\":\"P2 Title\"," +
                    "\"value\":\"" + Convert.ToString(dr["P2Title"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"P2 Address\"," +
                    "\"name\":\"P2 Address\"," +
                    "\"value\":\"" + Convert.ToString(dr["p2Address"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"P2 City, State, Zip\"," +
                    "\"name\":\"P2 City, State, Zip\"," +
                    "\"value\":\"" + Convert.ToString(dr["P2City"]).Trim() + ", " + Convert.ToString(dr["P2State"]).Trim() + " " + Convert.ToString(dr["P2ZipCode"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"P2 Phone\"," +
                    "\"name\":\"P2 Phone\"," +
                    "\"value\":\"" + Convert.ToString(dr["p2PhoneNumber"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"P2 Email\"," +
                    "\"name\":\"P2 Email\"," +
                    "\"value\":\"" + Convert.ToString(dr["P2Email"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"P2 SSN\"," +
                    "\"name\":\"P2 SSN\"," +
                    "\"value\":\"" + Convert.ToString(dr["P2SSN"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"P2 DOB\"," +
                    "\"name\":\"P2 DOB\"," +
                    "\"value\":\"" + Convert.ToString(dr["P2DOB"]).Trim() + "\"" +
                    "},"
                    ;
            }
        }
        string strBusinessType = "";

        if (Convert.ToString(dr["BusinessPctMailOrder"]).Trim() == "100")
        {
            strBusinessType = "{" +
                    "\"tabLabel\":\"BusinessTypeMoto\"," +
                    "\"name\":\"BusinessTypeMoto\"," +
                    "\"value\":\"" + "x" + "\"" +
                    "},";
        }
        else if (dr["BusinessPctInternet"].ToString().Trim() == "100")
        {
            strBusinessType = "{" +
                    "\"tabLabel\":\"BusinessTypeInternet\"," +
                    "\"name\":\"BusinessTypeInternet\"," +
                    "\"value\":\"" + "x" + "\"" +
                    "},";
        }
        else if (dr["BusinessPctRetail"].ToString().Trim() == "100")
        {
            strBusinessType = "{" +
                    "\"tabLabel\":\"BusinessTypeRetail\"," +
                    "\"name\":\"BusinessTypeRetail\"," +
                    "\"value\":\"" + "x" + "\"" +
                    "},";
        }
        else if (dr["BusinessPctRestaurant"].ToString().Trim() == "100")
        {
            strBusinessType = "{" +
                    "\"tabLabel\":\"BusinessTypeRestaurant\"," +
                    "\"name\":\"BusinessTypeRestaurant\"," +
                    "\"value\":\"" + "x" + "\"" +
                    "},";
        }

        string strReturnPolicy = "";

        if (dr["RefundPolicy"].ToString().Trim() == "Refund within 30 days")
        {
            strReturnPolicy = "{" +
                "\"tabLabel\":\"Return Policy\"," +
                "\"name\":\"Return Policy\"," +
                "\"value\":\"" + "30 Days Money Back Guarantee" + "\"" +
                "},";
        }
        else if (dr["RefundPolicy"].ToString().Trim() == "Exchange Only")
        {
            strReturnPolicy = "{" +
                "\"tabLabel\":\"Return Policy\"," +
                "\"name\":\"Return Policy\"," +
                "\"value\":\"" + "30 Days Exchange Only" + "\"" +
                "},";
        }
        else if (dr["RefundPolicy"].ToString().Trim() == "No Refund")
        {
            strReturnPolicy = "{" +
                "\"tabLabel\":\"Return Policy\"," +
                "\"name\":\"Return Policy\"," +
                "\"value\":\"" + "No Refund" + "\"" +
                "},";
        }
        else if (dr["RefundPolicy"].ToString().Trim().Contains("Other"))
        {
            strReturnPolicy = "{" +
                "\"tabLabel\":\"Return Policy\"," +
                "\"name\":\"Return Policy\"," +
                "\"value\":\"" + "Other" + "\"" +
                "},";
        }

        string strDebit = "";

        if (dr["DebitStatus"].ToString().ToLower().Contains("yes"))
        {
            strDebit = "{" +
                "\"tabLabel\":\"PinDebitY\"," +
                "\"name\":\"PinDebitY\"," +
                "\"value\":\"" + "Yes" + "\"" +
                "}," +
                "{" +
                "\"tabLabel\":\"chkPinDebit\"," +
                "\"name\":\"chkPinDebit\"," +
                "\"value\":\"" + "Yes" + "\"" +
                "}," +

                "{" +
                "\"tabLabel\":\"DPTY\"," +
                "\"name\":\"DPTY\"," +
                "\"value\":\"" + "x" + "\"" +
                "}," +


                "{" +
                "\"tabLabel\":\"PinDebitTransFeeChecked\"," +
                "\"name\":\"PinDebitTransFeeChecked\"," +
                "\"value\":\"" + "Yes" + "\"" +
                "}," +


                "{" +
                "\"tabLabel\":\"PinDebit Rate1\"," +
                "\"name\":\"PinDebit Rate1\"," +
                "\"value\":\"" + Convert.ToString(dr["DiscountRate"]).Trim() + "\"" +
                "},";
        }
        else
        {
            strDebit = "{" +
                "\"tabLabel\":\"PinDebitN\"," +
                "\"name\":\"PinDebitN\"," +
                "\"value\":\"" + "No" + "\"" +
                "},";
        }

        string strInterchange = "";
        string strTiered = "";
        string strFlatRate = "";
        if (Convert.ToString(dr["Interchange"]).Trim() == "True")
        {
            strInterchange = "{" +
                     "\"tabLabel\":\"DiscIPT\"," +
                     "\"name\":\"DiscIPT\"," +
                     "\"value\":\"" + "x" + "\"" +
                     "}," +
                     "{" +
                     "\"tabLabel\":\"VisaIPT\"," +
                     "\"name\":\"VisaIPT\"," +
                     "\"value\":\"" + "x" + "\"" +
                     "}," +
                     "{" +
                     "\"tabLabel\":\"MCIPT\"," +
                     "\"name\":\"MCIPT\"," +
                     "\"value\":\"" + "x" + "\"" +
                     "}," +
                     "{" +
                     "\"tabLabel\":\"Visa INTL/NS\"," +
                     "\"name\":\"Visa INTL/NS\"," +
                     "\"value\":\"" + "0.00" + "\"" +
                     "}," +
                     "{" +
                     "\"tabLabel\":\"Visa INTL/NS\"," +
                     "\"name\":\"Visa INTL/NS\"," +
                     "\"value\":\"" + "0.00" + "\"" +
                     "}," +
                     "{" +
                     "\"tabLabel\":\"MC INTL/NS\"," +
                     "\"name\":\"MC INTL/NS\"," +
                     "\"value\":\"" + "0.00" + "\"" +
                     "}," +
                     "{" +
                     "\"tabLabel\":\"Disc INTL/NS\"," +
                     "\"name\":\"Disc INTL/NS\"," +
                     "\"value\":\"" + "0.00" + "\"" +
                     "}," +
                     "{" +
                     "\"tabLabel\":\"Visa INTL/NS Surcharge\"," +
                     "\"name\":\"Visa INTL/NS Surcharge\"," +
                     "\"value\":\"" + "0.00" + "\"" +
                     "}," +
                     "{" +
                     "\"tabLabel\":\"MC INTL/NS Surcharge\"," +
                     "\"name\":\"MC INTL/NS Surcharge\"," +
                     "\"value\":\"" + "0.00" + "\"" +
                     "}," +
                     "{" +
                     "\"tabLabel\":\"Disc INTL/NS Surcharge\"," +
                     "\"name\":\"Disc INTL/NS Surcharge\"," +
                     "\"value\":\"" + "0.00" + "\"" +
                     "}," +

                     "{" +
                    "\"tabLabel\":\"Interchange Plus Credit\"," +
                    "\"name\":\"Interchange Plus Credit\"," +
                    "\"value\":\"" + Convert.ToString(dr["DiscountRate"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Interchange Plus Debit\"," +
                    "\"name\":\"Interchange Plus Debit\"," +
                    "\"value\":\"" + Convert.ToString(dr["DiscountRate"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Amex Interchange Plus\"," +
                    "\"name\":\"Amex Interchange Plus\"," +
                    "\"value\":\"" + Convert.ToString(dr["DiscountRate"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Amex Interchange Mid Plus\"," +
                    "\"name\":\"Amex Interchange Mid Plus\"," +
                    "\"value\":\"" + Convert.ToString(dr["DiscountRate"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Amex Interchange Non Qual Plus\"," +
                    "\"name\":\"Amex Interchange Non Qual Plus\"," +
                    "\"value\":\"" + Convert.ToString(dr["DiscountRate"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"AMEX Mid-Qualfieid\"," +
                    "\"name\":\"AMEX Mid-Qualfieid\"," +
                    "\"value\":\"" + "0.00" + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"AMEX Non-Qualfieid\"," +
                    "\"name\":\"AMEX Non-Qualfieid\"," +
                    "\"value\":\"" + "0.00" + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Credit Card Non Qualified Fee\"," +
                    "\"name\":\"Credit Card Non Qualified Fee\"," +
                    "\"value\":\"" + "" + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Credit Card Mid Qualified Fee\"," +
                    "\"name\":\"Credit Card Mid Qualified Fee\"," +
                    "\"value\":\"" + "" + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Credit Card Non Qualified Fee1\"," +
                    "\"name\":\"Credit Card Non Qualified Fee1\"," +
                    "\"value\":\"" + "" + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Credit Card Mid Qualified Fee1\"," +
                    "\"name\":\"Credit Card Mid Qualified Fee1\"," +
                    "\"value\":\"" + "" + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Interchange Pricing Check\"," +
                    "\"name\":\"Interchange Pricing Check\"," +
                    "\"value\":\"" + "Yes" + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Interchange Plus Check\"," +
                    "\"name\":\"Interchange Plus Check\"," +
                    "\"value\":\"" + "Yes" + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Interchange Amex Check\"," +
                    "\"name\":\"Interchange Amex Check\"," +
                    "\"value\":\"" + "Yes" + "\"" +
                    "},";
        }
        else if ((dr["DiscountRate"].ToString().Trim() != "") && (dr["Interchange"].ToString().Trim() != "True"))
        {
            strTiered = "{" +
                "\"tabLabel\":\"Tiered Plus Check\"," +
                    "\"name\":\"Tiered Plus Check\"," +
                    "\"value\":\"" + "Yes" + "\"" +
                    "}," +
            "{" +
            "\"tabLabel\":\"Tiered Amex Check\"," +
                    "\"name\":\"Tiered Amex Check\"," +
                    "\"value\":\"" + "Yes" + "\"" +
                    "},";
        }
        else
        {
            strFlatRate = "{" +
                "\"tabLabel\":\"Flat Rate Plus Check\"," +
                    "\"name\":\"Flat Rate Plus Check\"," +
                    "\"value\":\"" + "Yes" + "\"" +
                    "}," +
            "{" +
            "\"tabLabel\":\"Flat Rate Amex Check\"," +
                    "\"name\":\"Flat Rate Amex Check\"," +
                    "\"value\":\"" + "Yes" + "\"" +
                    "},";
        }

        string strAmexApplied = "";
        if (dr["AmexApplied"].ToString().Trim() == "0")
        {
            strAmexApplied = "{" +
                    "\"tabLabel\":\"American Express Other\"," +
                    "\"name\":\"American Express Other\"," +
                    "\"value\":\"" + "None" + "\"" +
                    "},";
        }
        else if ((dr["AmexApplied"].ToString().Trim() == "1") && (dr["PrevAmexNum"].ToString().Trim() == ""))
        {
            strAmexApplied = "{" +
                    "\"tabLabel\":\"American Express Other\"," +
                    "\"name\":\"American Express Other\"," +
                    "\"value\":\"" + "New" + "\"" +
                    "},";
        }
        else if ((dr["AmexApplied"].ToString().Trim() == "1") && (dr["PrevAmexNum"].ToString().Trim() != ""))
        {
            strAmexApplied = "{" +
                    "\"tabLabel\":\"American Express Other\"," +
                    "\"name\":\"American Express Other\"," +
                    "\"value\":\"" + "Existing" + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"American Express Existing #\"," +
                    "\"name\":\"American Express Existing #\"," +
                    "\"value\":\"" + Convert.ToString(dr["PrevAmexNum"]).Trim() + "\"" +
                    "},";
        }

        string strWirelessSetup = "";

        if ((dr["WirelessAccessFee"].ToString() != "") || (dr["WirelessTransFee"].ToString() != ""))
        {
            strWirelessSetup = "{" +
                    "\"tabLabel\":\"Wireless Set Up\"," +
                    "\"name\":\"Wireless Set Up\"," +
                    "\"value\":\"" + "35.00" + "\"" +
                    "},";
        }

        string strMobile = "";
        if (dr["Gateway"].ToString().ToLower().Contains("roampay"))
        {
            decimal TransFee = Convert.ToDecimal(dr["TransactionFee"].ToString().Trim()) + Convert.ToDecimal(dr["GatewayTransFee"].ToString().Trim());
            string strTransFee = TransFee.ToString();

            decimal NBCTransFee = Convert.ToDecimal(dr["NBCTransFee"].ToString().Trim()) + Convert.ToDecimal(dr["GatewayTransFee"].ToString().Trim());
            string strNBCTransFee = NBCTransFee.ToString();

            strMobile = "{" +
                    "\"tabLabel\":\"Sage Mobile Payments Setup\"," +
                    "\"name\":\"Sage Mobile Payments Setup\"," +
                    "\"value\":\"" + "25.00" + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Sage Mobile Payments Access\"," +
                    "\"name\":\"Sage Mobile Payments Access\"," +
                    "\"value\":\"" + Convert.ToString(dr["GatewayMonFee"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Visa MC Auth Fee\"," +
                    "\"name\":\"Visa MC Auth Fee\"," +
                    "\"value\":\"" + strTransFee + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Discover Auth Fee\"," +
                    "\"name\":\"Discover Auth Fee\"," +
                    "\"value\":\"" + strNBCTransFee + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"American Express Auth Fee\"," +
                    "\"name\":\"American Express Auth Fee\"," +
                    "\"value\":\"" + strNBCTransFee + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Carte Blanche Auth Fee\"," +
                    "\"name\":\"Carte Blanche Auth Fee\"," +
                    "\"value\":\"" + strNBCTransFee + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Diners Club Auth Fee\"," +
                    "\"name\":\"Diners Club Auth Fee\"," +
                    "\"value\":\"" + strNBCTransFee + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"JCB Auth Fee\"," +
                    "\"name\":\"JCB Auth Fee\"," +
                    "\"value\":\"" + strNBCTransFee + "\"" +
                    "},";
        }
        else
        {
            strMobile = "{" +
                    "\"tabLabel\":\"Visa MC Auth Fee\"," +
                    "\"name\":\"Visa MC Auth Fee\"," +
                    "\"value\":\"" + Convert.ToString(dr["TransactionFee"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Discover Auth Fee\"," +
                    "\"name\":\"Discover Auth Fee\"," +
                    "\"value\":\"" + Convert.ToString(dr["NBCTransFee"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"American Express Auth Fee\"," +
                    "\"name\":\"American Express Auth Fee\"," +
                    "\"value\":\"" + Convert.ToString(dr["NBCTransFee"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Carte Blanche Auth Fee\"," +
                    "\"name\":\"Carte Blanche Auth Fee\"," +
                    "\"value\":\"" + Convert.ToString(dr["NBCTransFee"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Diners Club Auth Fee\"," +
                    "\"name\":\"Diners Club Auth Fee\"," +
                    "\"value\":\"" + Convert.ToString(dr["NBCTransFee"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"JCB Auth Fee\"," +
                    "\"name\":\"JCB Auth Fee\"," +
                    "\"value\":\"" + Convert.ToString(dr["NBCTransFee"]).Trim() + "\"" +
                    "},";
        }

        string strGatewayAccess = "";
        if (dr["Gateway"].ToString().Contains("Sage") || dr["Gateway"].ToString().Contains("Roam"))
        {
            strGatewayAccess = "{" +
                    "\"tabLabel\":\"Gateway Access\"," +
                    "\"name\":\"Gateway Access\"," +
                    "\"value\":\"" + Convert.ToString(dr["GatewayMonFee"]).Trim() + "\"" +
                    "},";
        }

        string strAnnualFeeCP = "";
        if (dr["AnnualFeeCP"].ToString().Trim() != "")
        {
            strAnnualFeeCP = "{" +
                   "\"tabLabel\":\"Annual Assessment\"," +
                   "\"name\":\"Annual Assessment\"," +
                   "\"value\":\"" + Convert.ToString(dr["AnnualFeeCP"]).Trim() + "\"" +
                   "},";
        }
        else
        {
            strAnnualFeeCP = "{" +
                   "\"tabLabel\":\"Annual Assessment\"," +
                   "\"name\":\"Annual Assessment\"," +
                   "\"value\":\"" + Convert.ToString(dr["AnnualFeeCNP"]).Trim() + "\"" +
                   "},";
        }


        string strTerminalType = "";

        if (dr["Gateway"].ToString().Trim() != "")
        {
            strTerminalType = "{" +
                   "\"tabLabel\":\"Terminal/Software Type\"," +
                   "\"name\":\"Terminal/Software Type\"," +
                   "\"value\":\"" + Convert.ToString(dr["Gateway"]).Trim() + "\"" +
                   "},";
        }
        else
        {

            strTerminalType = "{" +
                   "\"tabLabel\":\"Terminal/Software Type\"," +
                   "\"name\":\"Terminal/Software Type\"," +
                   "\"value\":\"" + Convert.ToString(dr["Equipment"]).Trim() + "\"" +
                   "},";

        }

        string requestBody = "{" +
                    "\"status\":\"sent\"," +
                    "\"emailBlurb\":\"Please review and sign your document with Commerce Technologies.\"," +
                    "\"emailSubject\": \"Please review and sign your document with Commerce Technologies.\"," +
                    "\"templateId\":\"" + templateId + "\"," +
                    "\"templateRoles\":[" +

                    "{" +
                    "\"email\":\"" + Convert.ToString(dr["Email"]).Trim() + "\"," +
                    "\"name\":\"" + Convert.ToString(dr["P1FirstName"]).Trim() + " " + Convert.ToString(dr["P1LastName"]).Trim() + "\"," +
                    "\"roleName\":\"Signer\"," +
                    "\"clientUserId\":\"" + Convert.ToString(dr["P1FirstName"]).Trim() + " " + Convert.ToString(dr["P1LastName"]).Trim() + "\"," +
                    "\"tabs\": {" +
                    "\"textTabs\": [" +
                    "{" +
                    "\"tabLabel\":\"Authorization Network\"," +
                    "\"name\":\"Authorization Network\"," +
                    "\"value\":\"" + platForm + "\"" +
                    "}," +
                     "{" +
                    "\"tabLabel\":\"Contractor Name\"," +
                    "\"name\":\"Contractor Name\"," +
                    "\"value\":\"" + Convert.ToString(dr["RepName"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Legal Business Name\"," +
                    "\"name\":\"Legal Business Name\"," +
                    "\"value\":\"" + Convert.ToString(dr["COMPANYNAME"]).Trim() + "\"" +
                    "}," +
                     "{" +
                    "\"tabLabel\":\"MA_Address\"," +
                    "\"name\":\"MA_Address\"," +
                    "\"value\":\"" + Convert.ToString(dr["BillingAddress"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"MA_City\"," +
                    "\"name\":\"MA_City\"," +
                    "\"value\":\"" + Convert.ToString(dr["BillingCity"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"MA_State\"," +
                    "\"name\":\"MA_State\"," +
                    "\"value\":\"" + Convert.ToString(dr["BillingState"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"MA_Zip\"," +
                    "\"name\":\"MA_Zip\"," +
                    "\"value\":\"" + Convert.ToString(dr["BillingZipCode"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Contact Name\"," +
                    "\"name\":\"Contact Name\"," +
                    "\"value\":\"" + Convert.ToString(dr["ContactName"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Phone\"," +
                    "\"name\":\"Phone\"," +
                    "\"value\":\"" + Convert.ToString(dr["BusinessPhone"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Email\"," +
                    "\"name\":\"Email\"," +
                    "\"value\":\"" + Convert.ToString(dr["Email"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Business Open Date\"," +
                    "\"name\":\"Business Open Date\"," +
                    "\"value\":\"" + BusinessOpenMonth + "/" + BusinessOpenYear + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Length Of Ownership\"," +
                    "\"name\":\"Length Of Ownership\"," +
                    "\"value\":\"" + LengthOfOwnership + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Existing Sage MID\"," +
                    "\"name\":\"Existing Sage MID\"," +
                    "\"value\":\"" + prevProcNum + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Business Name DBA\"," +
                    "\"name\":\"Business Name DBA\"," +
                    "\"value\":\"" + Convert.ToString(dr["DBA"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Address\"," +
                    "\"name\":\"Address\"," +
                    "\"value\":\"" + Convert.ToString(dr["Address"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"City\"," +
                    "\"name\":\"City\"," +
                    "\"value\":\"" + Convert.ToString(dr["CITY"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"State\"," +
                    "\"name\":\"State\"," +
                    "\"value\":\"" + Convert.ToString(dr["STATE"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Zip\"," +
                    "\"name\":\"Zip\"," +
                    "\"value\":\"" + Convert.ToString(dr["ZipCode"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Phone_2\"," +
                    "\"name\":\"Phone_2\"," +
                    "\"value\":\"" + Convert.ToString(dr["BusinessPhone"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Fax_2\"," +
                    "\"name\":\"Fax_2\"," +
                    "\"value\":\"" + Convert.ToString(dr["Fax"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Web Site\"," +
                    "\"name\":\"Web Site\"," +
                    "\"value\":\"" + Convert.ToString(dr["Website"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Customer Service Phone\"," +
                    "\"name\":\"Customer Service Phone\"," +
                    "\"value\":\"" + Convert.ToString(dr["CustServPhone"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Number of Locations\"," +
                    "\"name\":\"Number of Locations\"," +
                    "\"value\":\"" + Convert.ToString(dr["NumberofLocations"]).Trim() + "\"" +
                    "}," +
                    strOwnershipType +
                    "{" +
                    "\"tabLabel\":\"Fed Tax ID\"," +
                    "\"name\":\"Fed Tax ID\"," +
                    "\"value\":\"" + Convert.ToString(dr["FederalTaxID"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"P1 Ownership\"," +
                    "\"name\":\"P1 Ownership\"," +
                    "\"value\":\"" + Convert.ToString(dr["P1OwnershipPercent"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"P1 Full Name\"," +
                    "\"name\":\"P1 Full Name\"," +
                    "\"value\":\"" + Convert.ToString(dr["P1FirstName"]).Trim() + " " + Convert.ToString(dr["P1LastName"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"P1 Title\"," +
                    "\"name\":\"P1 Title\"," +
                    "\"value\":\"" + Convert.ToString(dr["P1Title"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"P1 Address\"," +
                    "\"name\":\"P1 Address\"," +
                    "\"value\":\"" + Convert.ToString(dr["P1Address"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"P1 City, State, Zip\"," +
                    "\"name\":\"P1 City, State, Zip\"," +
                    "\"value\":\"" + Convert.ToString(dr["P1City"]).Trim() + ", " +
                        Convert.ToString(dr["P1State"]).Trim() + " " + Convert.ToString(dr["P1ZipCode"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"P1 Phone\"," +
                    "\"name\":\"P1 Phone\"," +
                    "\"value\":\"" + Convert.ToString(dr["P1PhoneNumber"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"P1 Email\"," +
                    "\"name\":\"P1 Email\"," +
                    "\"value\":\"" + Convert.ToString(dr["Email"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"P1 SSN\"," +
                    "\"name\":\"P1 SSN\"," +
                    "\"value\":\"" + Convert.ToString(dr["P1SSN"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"P1 DOB\"," +
                    "\"name\":\"P1 DOB\"," +
                    "\"value\":\"" + Convert.ToString(dr["P1DOB"]).Trim() + "\"" +
                    "}," +
                    strP2 +
                    strBusinessType +
                    strReturnPolicy +
                    "{" +
                    "\"tabLabel\":\"Products Sold\"," +
                    "\"name\":\"Products Sold\"," +
                    "\"value\":\"" + Convert.ToString(dr["ProductSold"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Monthly Volume\"," +
                    "\"name\":\"Monthly Volume\"," +
                    "\"value\":\"" + Convert.ToString(dr["MonthlyVolume"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Average Ticket\"," +
                    "\"name\":\"Average Ticket\"," +
                    "\"value\":\"" + Convert.ToString(dr["AverageTicket"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Highest Ticket\"," +
                    "\"name\":\"Highest Ticket\"," +
                    "\"value\":\"" + Convert.ToString(dr["MaxTicket"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Discount Paid Monthly\"," +
                    "\"name\":\"Discount Paid Monthly\"," +
                    "\"value\":\"" + "x" + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Current Processor\"," +
                    "\"name\":\"Current Processor\"," +
                    "\"value\":\"" + Convert.ToString(dr["PrevProcessor"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Card Present Swiped\"," +
                    "\"name\":\"Card Present Swiped\"," +
                    "\"value\":\"" + Convert.ToString(dr["ProcessPctSwiped"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Card Present Imprint\"," +
                    "\"name\":\"Card Present Imprint\"," +
                    "\"value\":\"" + Convert.ToString(dr["ProcessPctKeyedWImprint"]).Trim() + "\"" +
                    "}," +

                    "{" +
                    "\"tabLabel\":\"Card Not Present\"," +
                    "\"name\":\"Card Not Present\"," +
                    "\"value\":\"" + Convert.ToString(dr["ProcessPctKeyedWoImprint"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Days Until Product Delivery\"," +
                    "\"name\":\"Days Until Product Delivery\"," +
                    "\"value\":\"" + Convert.ToString(dr["NumDaysDelivered"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"ACH Bank Name\"," +
                    "\"name\":\"ACH Bank Name\"," +
                    "\"value\":\"" + Convert.ToString(dr["BankName"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"ACH Address 1\"," +
                    "\"name\":\"ACH Address 1\"," +
                    "\"value\":\"" + Convert.ToString(dr["BankAddress"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"ACH City\"," +
                    "\"name\":\"ACH City\"," +
                    "\"value\":\"" + Convert.ToString(dr["BankCity"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"ACH State\"," +
                    "\"name\":\"ACH State\"," +
                    "\"value\":\"" + Convert.ToString(dr["BankState"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"ACH Zip\"," +
                    "\"name\":\"ACH Zip\"," +
                    "\"value\":\"" + Convert.ToString(dr["BankZip"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"ACH Routing Number\"," +
                    "\"name\":\"ACH Routing Number\"," +
                    "\"value\":\"" + Convert.ToString(dr["BankRoutingNumber"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"ACH Account Number\"," +
                    "\"name\":\"ACH Account Number\"," +
                    "\"value\":\"" + Convert.ToString(dr["BankAccountNumber"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Visa Rate1\"," +
                    "\"name\":\"Visa Rate1\"," +
                    "\"value\":\"" + Convert.ToString(dr["DiscountRate"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"MasterCard Rate1\"," +
                    "\"name\":\"MasterCard Rate1\"," +
                    "\"value\":\"" + Convert.ToString(dr["DiscountRate"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Disc Rate1\"," +
                    "\"name\":\"Disc Rate1\"," +
                    "\"value\":\"" + Convert.ToString(dr["DiscountRate"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Visa Rate2\"," +
                    "\"name\":\"Visa Rate2\"," +
                    "\"value\":\"" + Convert.ToString(dr["DiscRateMidQual"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"MasterCard Rate2\"," +
                    "\"name\":\"MasterCard Rate2\"," +
                    "\"value\":\"" + Convert.ToString(dr["DiscRateMidQual"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Disc Rate2\"," +
                    "\"name\":\"Disc Rate2\"," +
                    "\"value\":\"" + Convert.ToString(dr["DiscRateMidQual"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Visa Rate3\"," +
                    "\"name\":\"Visa Rate3\"," +
                    "\"value\":\"" + Convert.ToString(dr["DiscRateNonQual"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"MasterCard Rate3\"," +
                    "\"name\":\"MasterCard Rate3\"," +
                    "\"value\":\"" + Convert.ToString(dr["DiscRateNonQual"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Disc Rate3\"," +
                    "\"name\":\"Disc Rate3\"," +
                    "\"value\":\"" + Convert.ToString(dr["DiscRateNonQual"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Visa INTL/NS\"," +
                    "\"name\":\"Visa INTL/NS\"," +
                    "\"value\":\"" + "1.15" + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"MC INTL/NS\"," +
                    "\"name\":\"MC INTL/NS\"," +
                    "\"value\":\"" + "1.15" + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Disc INTL/NS\"," +
                    "\"name\":\"Disc INTL/NS\"," +
                    "\"value\":\"" + "1.15" + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Visa INTL/NS Surcharge\"," +
                    "\"name\":\"Visa INTL/NS Surcharge\"," +
                    "\"value\":\"" + "0.20" + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"MC INTL/NS Surcharge\"," +
                    "\"name\":\"MC INTL/NS Surcharge\"," +
                    "\"value\":\"" + "0.20" + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Disc INTL/NS Surcharge\"," +
                    "\"name\":\"Disc INTL/NS Surcharge\"," +
                    "\"value\":\"" + "0.20" + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Disc Business\"," +
                    "\"name\":\"Disc Business\"," +
                    "\"value\":\"" + Convert.ToString(dr["DiscRateNonQual"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Visa Business\"," +
                    "\"name\":\"Visa Business\"," +
                    "\"value\":\"" + Convert.ToString(dr["DiscRateNonQual"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"MC Business\"," +
                    "\"name\":\"MC Business\"," +
                    "\"value\":\"" + Convert.ToString(dr["DiscRateNonQual"]).Trim() + "\"" +
                    "}," +
                    strInterchange +
                    strTiered +
                    strFlatRate +
                    strAmexApplied +
                    "{" +
                    "\"tabLabel\":\"ARU Auth Fee\"," +
                    "\"name\":\"ARU Auth Fee\"," +
                    "\"value\":\"" + Convert.ToString(dr["VoiceAuth"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"EBT Auth Fee\"," +
                    "\"name\":\"EBT Auth Fee\"," +
                    "\"value\":\"" + Convert.ToString(dr["EBTTransFee"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"PIN Debit Auth Fee\"," +
                    "\"name\":\"PIN Debit Auth Fee\"," +
                    "\"value\":\"" + Convert.ToString(dr["DebitTransFee"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Voice Authorization\"," +
                    "\"name\":\"Voice Authorization\"," +
                    "\"value\":\"" + Convert.ToString(dr["VoiceAuth"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Application Credit\"," +
                    "\"name\":\"Application Credit\"," +
                    "\"value\":\"" + Convert.ToString(dr["AppFee"]).Trim() + "\"" +
                    "}," +
                    strWirelessSetup +
                    strMobile +
                    "{" +
                    "\"tabLabel\":\"Statement\"," +
                    "\"name\":\"Statement\"," +
                    "\"value\":\"" + "Statement" + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Monthly Support\"," +
                    "\"name\":\"Monthly Support\"," +
                    "\"value\":\"" + Convert.ToString(dr["CustServFee"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Monthly Minimum\"," +
                    "\"name\":\"Monthly Minimum\"," +
                    "\"value\":\"" + Convert.ToString(dr["MonMin"]).Trim() + "\"" +
                    "}," +
                    strGatewayAccess +
                    "{" +
                    "\"tabLabel\":\"Debit Access\"," +
                    "\"name\":\"Debit Access\"," +
                    "\"value\":\"" + Convert.ToString(dr["DebitMonFee"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Wireless Access\"," +
                    "\"name\":\"Wireless Access\"," +
                    "\"value\":\"" + Convert.ToString(dr["WirelessAccessFee"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Chargeback\"," +
                    "\"name\":\"Chargeback\"," +
                    "\"value\":\"" + Convert.ToString(dr["ChargebackFee"]).Trim() + "\"" +
                    "}," +
                    "{" +
                    "\"tabLabel\":\"Signature Rate\"," +
                    "\"name\":\"Signature Rate\"," +
                    "\"value\":\"" + Convert.ToString(dr["DiscRateQualDebit"]).Trim() + "\"" +
                    "}," +
                    strGatewayAccess +
                    strTerminalType +
                    strDebit +
                    "]," +
                    "}" +
                    "}" +
                    strCoSigner +
                    "]" +
                    "}";

        return requestBody;
    }

    protected void docusignEmbededSign(string baseURL, string uri, string reqBody, out string url)
    {
        string url1 = "";
        //tring username = "twu@commercetech.com";
        //string password = "Commerce1";
        //string password = "Comtech540910";
        //string username = "jscott@commercetech.com";
        //string password = "1Commerce11";


        string username = "sales@commercetech.com";
        string password = "1Success11";
        string integratorKey = "ECOM-21cf1b09-3dbf-41fc-9910-13c11df86eb5";
        string authenticateStr =
                    "<DocuSignCredentials>" +
                    "<Username>" + username + "</Username>" +
                    "<Password>" + password + "</Password>" +
                    "<IntegratorKey>" + integratorKey + "</IntegratorKey>" +
                    "</DocuSignCredentials>";
        try
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseURL + uri + "/views/recipient");
            request.Headers.Add("X-DocuSign-Authentication", authenticateStr);
            request.ContentType = "application/json";
            request.Accept = "application/json";
            request.ContentLength = reqBody.Length;
            request.Method = "POST";
            // write the body of the request
            byte[] body2 = System.Text.Encoding.UTF8.GetBytes(reqBody);
            Stream dataStream2 = request.GetRequestStream();
            dataStream2.Write(body2, 0, reqBody.Length);
            dataStream2.Close();
            // read the response
            HttpWebResponse webResponse = (HttpWebResponse)request.GetResponse();
            StreamReader sr = new StreamReader(webResponse.GetResponseStream());
            string responseText = sr.ReadToEnd();

            using (JsonTextReader reader = new JsonTextReader(new StringReader(responseText)))
            {
                while (reader.Read())
                {
                    if (reader.TokenType == JsonToken.PropertyName)
                    {
                        if (reader.Value.ToString() == "url")
                        {
                            reader.Read();
                            url1 = reader.Value.ToString();
                        }
                    }
                }
            }

            LogBL LogData = new LogBL(AppId);

            LogData.InsertLogData(AffiliateID, "Viewed envelope in embeded signing.");

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
                    Console.WriteLine(text);
                    CreateOnlineAppLog Log = new CreateOnlineAppLog();
                    Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + AppId.ToString() + " - " + "DocuSign Error - " + text);
                    SetErrorMessage(text);

                    CreatePDF PDFFile = new CreatePDF(AppId);
                    string strProcessor = PDFFile.ReturnProcessorName();
                    LogBL LogData = new LogBL(AppId);
                    if (strProcessor.ToLower().Contains("sage"))
                    {
                        LogData.InsertLogData(AffiliateID, "Embeded signing for Sage failed.");
                    }
                    else if (strProcessor.ToLower().Contains("ipay"))
                    {
                        LogData.InsertLogData(AffiliateID, "Embeded signing for iPayment failed.");
                    }
                }
            }
        }

        url = url1;

    }

    protected void docusignSendEnvelop(string baseURL, string requestBody, out string envelopeId, out string uri)
    {
        //string username = "twu@commercetech.com";
        //string password = "Commerce1";
        //string password = "Comtech540910";
        //string username = "jscott@commercetech.com";
        //string password = "1Commerce11";


        string username = "sales@commercetech.com";
        string password = "1Success11";
        string integratorKey = "ECOM-21cf1b09-3dbf-41fc-9910-13c11df86eb5";
        string uri1 = "";
        string envelopeId1 = "";

        string authenticateStr =
                    "<DocuSignCredentials>" +
                    "<Username>" + username + "</Username>" +
                    "<Password>" + password + "</Password>" +
                    "<IntegratorKey>" + integratorKey + "</IntegratorKey>" +
                    "</DocuSignCredentials>";

        try
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseURL + "/envelopes");
            request = (HttpWebRequest)WebRequest.Create("https://na2.docusign.net/restapi/v2/accounts/4400185/envelopes");
            request.Headers.Add("X-DocuSign-Authentication", authenticateStr);
            request.ContentType = "application/json";
            request.Accept = "application/json";
            //request.ContentDisposition = "form-data";
            request.ContentLength = requestBody.Length;
            request.Method = "POST";
            // write the body of the request
            byte[] body = System.Text.Encoding.UTF8.GetBytes(requestBody);
            Stream dataStream = request.GetRequestStream();

            dataStream.Write(body, 0, requestBody.Length);
            dataStream.Close();
            // read the response
            HttpWebResponse webResponse = (HttpWebResponse)request.GetResponse();
            /*
            using (Stream data = webResponse.GetResponseStream())
            {
                string text = new StreamReader(data).ReadToEnd();
                Console.WriteLine(text);
                SetErrorMessage(text);
            }*/
            StreamReader sr = new StreamReader(webResponse.GetResponseStream());
            string responseText = sr.ReadToEnd();
            //JsonTextReader
            //using (Stream s = request.GetResponse().GetResponseStream())

            //using (StreamReader reader = StreamReader.Create(new StringReader(responseText)))

            using (JsonTextReader reader = new JsonTextReader(new StringReader(responseText)))
            {
                while (reader.Read())
                {
                    if (reader.TokenType == JsonToken.PropertyName)
                    {
                        //if (reader.TokenType == JsonToken.String && reader.Value.Equals("envelopeId"))
                        if (reader.Value.ToString() == "envelopeId")
                        {
                            reader.Read();
                            envelopeId1 = reader.Value.ToString();
                        }
                        if (reader.Value.ToString() == "uri")
                        {
                            reader.Read();
                            uri1 = reader.Value.ToString();
                        }
                    }
                }
            }

            CreatePDF PDFFile = new CreatePDF(AppId);
            string strProcessor = PDFFile.ReturnProcessorName();
            //string docuSignProcessor = newApp.GetDocuSignProcessor();

            LogBL LogData = new LogBL(AppId);
            if (strProcessor.ToLower().Contains("sage"))
            {
                LogData.InsertLogData(AffiliateID, "Sage DocuSign envelope created.");
            }
            else if (strProcessor.ToLower().Contains("ipay"))
            {
                LogData.InsertLogData(AffiliateID, "iPayment DocuSign envelope created.");
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
                    Console.WriteLine(text);
                    CreateOnlineAppLog Log = new CreateOnlineAppLog();
                    Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + AppId.ToString() + " - " + "DocuSign Error - " + text);
                    SetErrorMessage(text);

                    CreatePDF PDFFile = new CreatePDF(AppId);
                    string strProcessor = PDFFile.ReturnProcessorName();
                    LogBL LogData = new LogBL(AppId);
                    if (strProcessor.ToLower().Contains("sage"))
                    {
                        LogData.InsertLogData(AffiliateID, "Failed to create Sage envelope.");
                    }
                    else if (strProcessor.ToLower().Contains("ipay"))
                    {
                        LogData.InsertLogData(AffiliateID, "Failed to create iPayment envelope.");
                    }

                }
            }
        }

        envelopeId = envelopeId1;
        uri = uri1;

    }

    protected void btnCreatePDF_Click(object sender, EventArgs e)
    {
        try
        {

            //string strMachineName = "www.apps.commercetech.com/OnlineApplication";
            CreatePDF PDFFile = new CreatePDF(AppId);
            string strProcessor = PDFFile.ReturnProcessorName();
            MemoryStream mStream;
            string FileName = "";
            CommonFunctions GeneralInfo = new CommonFunctions(AppId);
            if (strProcessor.ToLower().Contains("sage"))
            {
                FileName = Server.MapPath("/Application/Paya Application.pdf");
                mStream = PDFFile.CreateSagePDF(FileName);

                if (mStream != null)
                {
                    GeneralInfo.UpdateLastModified();
                    LogBL LogData = new LogBL(AppId);
                    LogData.InsertLogData(AffiliateID, "Paya PDF Created");


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
                    SetErrorMessage("Data not found for this record.");

                /*OAPDFBL SageData = new OAPDFBL(AppId);
                DataSet ds = SageData.GetSageData();
                DataRow dr = ds.Tables[0].Rows[0];
                if (Convert.ToDecimal(dr["ProcessPctSwiped"].ToString().Trim()) >= 70)
                    btnSageMOTO.Visible = false;
                else
                    btnSageMOTO.Visible = true;
                pnlSagePDF.Visible = true;*/
            }
            else if (strProcessor.ToLower().Contains("intuit"))
            {
                OAPDFBL IMSData = new OAPDFBL(AppId);
                DataSet ds = IMSData.GetIMSData();
                DataRow dr = ds.Tables[0].Rows[0];
                if ((dr["Interchange"].ToString() == "True") || (dr["Assessments"].ToString() == "True"))
                    FileName = Server.MapPath("/Application/IPS Application Interchange.pdf");
                else
                    FileName = Server.MapPath("/Application/IPS Application.pdf");
                mStream = PDFFile.CreateIPSPDF(FileName);

                if (mStream != null)
                {
                    GeneralInfo.UpdateLastModified();
                    LogBL LogData = new LogBL(AppId);
                    LogData.InsertLogData(AffiliateID, "IPS PDF Created");
                    GeneralInfo.UpdateLastModified();

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
                    SetErrorMessage("Data not found for this record.");
            }
            else if (strProcessor.ToLower().Contains("ipayment"))
            {
                FileName = Server.MapPath("/Application/ipayment application.pdf");

                mStream = PDFFile.CreateIPayPDF(FileName);

                if (mStream != null)
                {
                    LogBL LogData = new LogBL(AppId);
                    LogData.InsertLogData(AffiliateID, "iPayment PDF Created");
                    GeneralInfo.UpdateLastModified();

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
                    SetErrorMessage("Data not found for this record.");
            }
            else if (strProcessor.ToLower().Contains("cardconnect"))
            {
                OAPDFBL ChaseData = new OAPDFBL(AppId);
                DataSet ds = ChaseData.GetChaseData();
                DataRow dr = ds.Tables[0].Rows[0];

                if ((Convert.ToBoolean(dr["Interchange"])) || (Convert.ToBoolean(dr["Assessments"])))
                {
                    btnChaseFSInterchangePlus_Click(sender, e);
                    GeneralInfo.UpdateLastModified();
                    //btnChaseMPA_Click(sender, e);
                    //btnChaseFS3Tier.Visible = false;
                    //btnChaseFSInterchangePlus.Visible = true;
                }
                else
                {
                    btnChaseFS3Tier_Click(sender, e);
                    GeneralInfo.UpdateLastModified();
                    //btnChaseMPA_Click(sender, e);
                    //btnChaseFS3Tier.Visible = true;
                    //btnChaseFSInterchangePlus.Visible = false;
                }
                pnlChasePDF.Visible = true;
            }
            else if (strProcessor.ToLower().Contains("optimal-merrick"))
            {
                FileName = Server.MapPath("/Application/CNP_Merrick.pdf");
                mStream = PDFFile.CreateMerrickPDF(FileName);

                if (mStream != null)
                {
                    LogBL LogData = new LogBL(AppId);
                    LogData.InsertLogData(AffiliateID, "Merrick PDF Created");
                    GeneralInfo.UpdateLastModified();

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
                    SetErrorMessage("Data not found for this record.");
            }
            else if (strProcessor.ToLower().Contains("canada"))
            {
                //error at this location because PDF file is invalid
                FileName = Server.MapPath("/Application/Optimal_Canada_App.pdf");
                mStream = PDFFile.CreateCanadaPDF(FileName);
                if (mStream != null)
                {
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.ContentType = "application/pdf";
                    Response.AppendHeader("content-disposition", "filename=Optimal_Canadian_App.pdf");
                    Response.AppendHeader("Content-Length", mStream.GetBuffer().Length.ToString());
                    Response.OutputStream.Write(mStream.GetBuffer(), 0, Convert.ToInt32(mStream.GetBuffer().Length));

                    Response.Flush();
                    Response.Close();

                    LogBL LogData = new LogBL(AppId);
                    LogData.InsertLogData(AffiliateID, "Optimal Canada PDF Created");
                    GeneralInfo.UpdateLastModified();
                }
                else
                    SetErrorMessage("Data not found for this record.");
            }
            else if ((strProcessor.ToLower().Contains("cal")) || (strProcessor.ToLower().Contains("international")))
            {
                FileName = Server.MapPath("/Application/CAL_Application_NA.pdf");
                mStream = PDFFile.CreateInternationalPDF(FileName);

                if (mStream != null)
                {
                    LogBL LogData = new LogBL(AppId);
                    LogData.InsertLogData(AffiliateID, "Optimal CAL PDF Created");
                    GeneralInfo.UpdateLastModified();

                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.ContentType = "application/pdf";
                    Response.AppendHeader("content-disposition", "filename=Optimal Cal Application.pdf");
                    Response.AppendHeader("Content-Length", mStream.GetBuffer().Length.ToString());
                    Response.OutputStream.Write(mStream.GetBuffer(), 0, Convert.ToInt32(mStream.GetBuffer().Length));

                    Response.Flush();
                    Response.Close();
                }
                else
                    SetErrorMessage("Data not found for this record.");
            }
            else if (strProcessor.ToLower().Contains("barclays"))
            {
                FileName = Server.MapPath("/Application/Barclays Bank Agreement.pdf");
                mStream = PDFFile.CreateBarclaysPDF(FileName);

                if (mStream != null)
                {
                    LogBL LogData = new LogBL(AppId);
                    LogData.InsertLogData(AffiliateID, "Optimal Barclays PDF Created");
                    GeneralInfo.UpdateLastModified();

                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.ContentType = "application/pdf";
                    Response.AppendHeader("content-disposition", "filename=Optimal Barclays Application.pdf");
                    Response.AppendHeader("Content-Length", mStream.GetBuffer().Length.ToString());
                    Response.OutputStream.Write(mStream.GetBuffer(), 0, Convert.ToInt32(mStream.GetBuffer().Length));

                    Response.Flush();
                    Response.Close();
                }
                else
                    SetErrorMessage("Data not found for this record.");
            }
            else if (strProcessor.ToLower().Contains("kitts"))
            {
                FileName = Server.MapPath("/Application/St_Kitts_Application.pdf");
                mStream = PDFFile.CreateStKittsPDF(FileName);

                if (mStream != null)
                {
                    LogBL LogData = new LogBL(AppId);
                    LogData.InsertLogData(AffiliateID, "Optimal St. Kitts PDF Created");
                    GeneralInfo.UpdateLastModified();

                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.ContentType = "application/pdf";
                    Response.AppendHeader("content-disposition", "filename=Optimal St. Kitts Application.pdf");
                    Response.AppendHeader("Content-Length", mStream.GetBuffer().Length.ToString());
                    Response.OutputStream.Write(mStream.GetBuffer(), 0, Convert.ToInt32(mStream.GetBuffer().Length));

                    Response.Flush();
                    Response.Close();
                }
                else
                    SetErrorMessage("Data not found for this record.");
            }
            else if (strProcessor.ToLower().Contains("payvision"))
            {
                FileName = Server.MapPath("/Application/Payvision Application.pdf");
                mStream = PDFFile.CreatePayvisionPDF(FileName);

                if (mStream != null)
                {
                    LogBL LogData = new LogBL(AppId);
                    LogData.InsertLogData(AffiliateID, "Payvision PDF Created");
                    GeneralInfo.UpdateLastModified();

                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.ContentType = "application/pdf";
                    Response.AppendHeader("content-disposition", "filename=Payvision Application.pdf");
                    Response.AppendHeader("Content-Length", mStream.GetBuffer().Length.ToString());
                    Response.OutputStream.Write(mStream.GetBuffer(), 0, Convert.ToInt32(mStream.GetBuffer().Length));

                    Response.Flush();
                    Response.Close();
                }
                else
                    SetErrorMessage("Data not found for this record.");
            }
            else
                SetErrorMessage("Processor not valid for PDF creation.");
        }//end try
        catch (Exception err)
        {
            CreateOnlineAppLog Log = new CreateOnlineAppLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + AppId.ToString() + " - " + "main.master.cs Create PDF Error - " + err.Message);
            SetErrorMessage(err.Message);
        }
        //Call the same Create PDF function as the Master Page
        //Main MainPg = (Main)this.Master;
        //MainPg.lnkbtnCreatePDF_Click(sender, e);

    }

    #region Sage App
    public void btnSageApp_Click(object sender, EventArgs e)
    {
        try
        {
            string FileName = Server.MapPath("/Application/Paya Application.pdf");
            CreatePDF PDFFile = new CreatePDF(AppId);
            MemoryStream mStream = PDFFile.CreateSagePDF(FileName);

            if (mStream != null)
            {
                LogBL LogData = new LogBL(AppId);
                LogData.InsertLogData(AffiliateID, "Paya PDF Created");

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
                SetErrorMessage("Data not found for this record.");
        }
        catch (Exception err)
        {
            SetErrorMessage(err.Message);
            CreateOnlineAppLog Log = new CreateOnlineAppLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + AppId.ToString() + " - " + "Notice.aspx.cs Create Sage App PDF Error - " + err.Message);
        }
    }

    public void btnSageMOTO_Click(object sender, EventArgs e)
    {
        try
        {
            string FileName = Server.MapPath("/Application/Sage MOTO-Internet Question.pdf");
            CreatePDF PDFFile = new CreatePDF(AppId);
            MemoryStream mStream = PDFFile.CreateSageMOTO(FileName);

            if (mStream != null)
            {
                LogBL LogData = new LogBL(AppId);
                LogData.InsertLogData(AffiliateID, "Sage MOTO PDF Created");

                Response.ClearContent();
                Response.ClearHeaders();
                Response.ContentType = "application/pdf";
                Response.AppendHeader("content-disposition", "filename=Sage MOTO Application.pdf");
                Response.AppendHeader("Content-Length", mStream.GetBuffer().Length.ToString());
                Response.OutputStream.Write(mStream.GetBuffer(), 0, Convert.ToInt32(mStream.GetBuffer().Length));

                Response.Flush();
                Response.Close();
            }
            else
                SetErrorMessage("Data not found for this record.");
        }
        catch (Exception err)
        {
            SetErrorMessage(err.Message);
            CreateOnlineAppLog Log = new CreateOnlineAppLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + AppId.ToString() + " - " + "Notice.aspx.cs Create Sage MOTO PDF Error - " + err.Message);
        }
    }
    #endregion

    #region Chase App

    public void btnChaseMPA_Click(object sender, EventArgs e)
    {
        try
        {
            string FileName = Server.MapPath("/Application/CardConnect Application.pdf");

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
                SetErrorMessage("Data not found for this record.");
        }//end try
        catch (Exception err)
        {
            SetErrorMessage(err.Message);
            CreateOnlineAppLog Log = new CreateOnlineAppLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + AppId.ToString() + " - " + "Merchant PDF - Create Chase MPA PDF Error - " + err.Message);
        }
    }

    public void btnChaseFS3Tier_Click(object sender, EventArgs e)
    {
        try
        {
            string FileName = Server.MapPath("/Application/CardConnect Application.pdf");

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
                SetErrorMessage("Data not found for this record.");
        }//end try
        catch (Exception err)
        {
            SetErrorMessage(err.Message);
            CreateOnlineAppLog Log = new CreateOnlineAppLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + AppId.ToString() + " - " + "Merchant PDF - Create Chase Fee schedule 3 tier PDF Error - " + err.Message);
        }
    }

    public void btnChaseFSInterchangePlus_Click(object sender, EventArgs e)
    {
        try
        {
            string FileName = Server.MapPath("/Application/CardConnect Application.pdf");

            CreatePDF PDFFile = new CreatePDF(AppId);
            MemoryStream mStream = PDFFile.CreateChaseFSInterchangePlusPDF(FileName);
            if (mStream != null)
            {
                Response.ClearContent();
                Response.ClearHeaders();
                Response.ContentType = "application/pdf";
                Response.AppendHeader("content-disposition", "filename=CardConnect Application.pdf.pdf");
                Response.AppendHeader("Content-Length", mStream.GetBuffer().Length.ToString());
                Response.OutputStream.Write(mStream.GetBuffer(), 0, Convert.ToInt32(mStream.GetBuffer().Length));

                Response.Flush();
                Response.Close();

                LogBL LogData = new LogBL(AppId);
                LogData.InsertLogData(AffiliateID, "CardConnect PDF Created");
                //LogBL LogData = new LogBL(AppId);
                //LogData.InsertLogData(AffiliateID, "Merrick PDF Created");
                //GeneralInfo.UpdateLastModified();
            }
            else
                SetErrorMessage("Data not found for this record.");
        }//end try
        catch (Exception err)
        {
            SetErrorMessage(err.Message);
            CreateOnlineAppLog Log = new CreateOnlineAppLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + AppId.ToString() + " - " + "Merchant PDF - Create Chase Fee schedule interchange plus PDF Error - " + err.Message);
        }
    }

    #endregion

    //This function checks if the application is locked
    protected bool Locked()
    {
        if (Session["Locked"].ToString().Trim() == "Yes")
            return true;
        else
            return false;
    }

    //This function displays error messages
    protected void SetErrorMessage(string errText)
    {
        lblError.Visible = true;
        lblError.Text = errText;
    }//end function set error message
}
