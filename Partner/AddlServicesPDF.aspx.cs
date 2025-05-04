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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Text;
using System.IO;
using DataLayer;

public partial class AddlServicesPDF : System.Web.UI.Page
{
    private static int AppId = 0;
    private static int AffiliateID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session.IsNewSession)
            Response.Redirect("/OnlineApplication/default.aspx?SessionExpired=True");

        Style errLabel = new Style();
        errLabel.BackColor = System.Drawing.Color.FromArgb(255, 192, 192);
        errLabel.ForeColor = System.Drawing.Color.Black;
        errLabel.BorderStyle = BorderStyle.Solid;
        errLabel.BorderWidth = Unit.Pixel(1);
        errLabel.Font.Size = FontUnit.Small;
        lblError.ApplyStyle(errLabel);

        Response.CacheControl = "no-cache";
        Response.AddHeader("Pragma", "no-cache");
        Response.Expires = -1;

        AppId = Convert.ToInt32(Session["AppId"]);
        AffiliateID = Convert.ToInt32(Session["AffiliateID"]);

        if (!IsPostBack)
        {
            if (!User.Identity.IsAuthenticated)
                Response.Redirect("/OnlineApplication/default.aspx?Authentication=False");
            else
            {
                try
                {
                    //if (!Locked())
                        Populate();
                    /*else
                    {
                        SetErrorMessage("The status of your application prevents it from being edited");
                        //btnContinue.Enabled = false;
                    }*/
                }//end try
                catch (Exception err)
                {
                    CreateOnlineAppLog Log = new CreateOnlineAppLog();
                    Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + Session["AppId"].ToString() + " - " + "AddlServices.aspx Page Load Error - " + err.Message);
                    SetErrorMessage("Error Processing Request. Please contact Technical Support: <a href='mailto:support@commercetech.com?Subject=Online Application Error'> support@commercetech.com. </a>.");
                }
            }//end if authenticated
        }//end if postback
    }
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

    public void Populate()
    {
        NewAppInfo AddlServ = new NewAppInfo(AppId);
        DataSet ds = AddlServ.GetNewAppData();
        DataRow dr = ds.Tables[0].Rows[0];
        CreatePDF PDFFile = new CreatePDF(AppId);
        string strCheckService = PDFFile.ReturnCheckServiceName();
        if ((strCheckService.ToLower().Contains("direct debit") && (Convert.ToBoolean(dr["CheckServices"]) == true)))
        {
            pnlCreateDDPDF.Visible = false; //change this to true when we have the correct pdf
            pnlMerchantFundingBFS.Visible = false;
            pnlMsg.Visible = false;
            btnMerchantFunding.Visible = false;
        }
        else
            pnlCreateDDPDF.Visible = false;
        pnlMerchantFundingBFS.Visible = false;

        if (Convert.ToBoolean(dr["CheckServices"]) == true)
        {
            pnlCheckServices.Visible = true;
        }
        else
            pnlCheckServices.Visible = false;
            
            //pnlMsg.Visible = false;

        string strLeaseCompany = PDFFile.ReturnLeaseCompany();
        if ((strLeaseCompany.ToLower().Contains("northern")) && (Convert.ToBoolean(dr["Lease"]) == true))
        {
            pnlCreateLeasingPDF.Visible = true;
            pnlPDFNote.Visible = true;
            pnlMerchantFundingBFS.Visible = false;
            btnMerchantFunding.Visible = false;
        }
        else
            pnlCreateLeasingPDF.Visible = false;
        pnlMerchantFundingBFS.Visible = false;
            

        //Gift Card Services
        string strGiftCardType = PDFFile.ReturnGiftCardType();
        //if ((strGiftCardType.ToLower().Contains("EFT")) && (Convert.ToBoolean(dr["GiftCard"]) == true))
            if (Convert.ToBoolean(dr["GiftCard"]) == true)
        {
            pnlCreateGiftCardPDF.Visible = true;
            pnlPDFNote.Visible = true;
            pnlMerchantFundingBFS.Visible = false;
            btnMerchantFunding.Visible = false;
        }
        else
            pnlCreateGiftCardPDF.Visible = false;
            pnlMerchantFundingBFS.Visible = false;
            

        //Merchant Cash Advance
        /*
        string strMCAType = PDFFile.ReturnMCAType();
        if ((Convert.ToBoolean(dr["MerchantFunding"]) == true) && (strMCAType == "AdvanceMe, Inc."))
        {
            pnlMerchantFundingAMI.Visible = true;
            pnlMerchantFundingBFS.Visible = false;
            pnlMerchantFundingRapidAdvance.Visible = false;
        }
        else if ((Convert.ToBoolean(dr["MerchantFunding"]) == true) && (strMCAType == "Business Financial Services"))
        {
            pnlMerchantFundingBFS.Visible = true;
            pnlMerchantFundingAMI.Visible = false;
            pnlMerchantFundingRapidAdvance.Visible = false;
        }
        else if ((Convert.ToBoolean(dr["MerchantFunding"]) == true) && (strMCAType == "RapidAdvance"))
        {
            pnlMerchantFundingRapidAdvance.Visible = true;
            pnlMerchantFundingBFS.Visible = false;
            pnlMerchantFundingAMI.Visible = false;
        }
        else {
            pnlMerchantFundingRapidAdvance.Visible = false;
            pnlMerchantFundingBFS.Visible = false;
            pnlMerchantFundingAMI.Visible = false;
        }*/

            string strMCAType = PDFFile.ReturnMCAType();
            if ((Convert.ToBoolean(dr["MerchantFunding"]) == true) && (strMCAType == "AdvanceMe, Inc."))
            {
                pnlMerchantFundingAMI.Visible = true;
                pnlMerchantFundingBFS.Visible = false;
                pnlRapidAdvance.Visible = false;
            }
            else if ((Convert.ToBoolean(dr["MerchantFunding"]) == true) && (strMCAType == "Business Financial Services"))
            {
                pnlMerchantFundingBFS.Visible = true;
                pnlMerchantFundingAMI.Visible = false;
                pnlRapidAdvance.Visible = false;

            }
            else if ((Convert.ToBoolean(dr["MerchantFunding"]) == true) && (strMCAType == "RapidAdvance"))
            {
                pnlRapidAdvance.Visible = true;
                pnlMerchantFundingBFS.Visible = false;
                pnlMerchantFundingAMI.Visible = false;
            }
            else
            {
                pnlRapidAdvance.Visible = false;
                pnlMerchantFundingBFS.Visible = false;
                pnlMerchantFundingAMI.Visible = false;
            }

        if (Convert.ToBoolean(dr["Payroll"]) == true)
        {
            if ((!(Convert.ToBoolean(dr["MerchantFunding"]) == true)) && (!(Convert.ToBoolean(dr["GiftCard"]) == true)) && (!(Convert.ToBoolean(dr["CheckServices"]) == true)))
            {
                pnlMsg.Visible = false;
            }
            pnlPayroll.Visible = true;
            pnlMerchantFundingBFS.Visible = false;

            string PayrollType = AddlServ.ReturnPayrollType();
            /*if (PayrollType.ToLower().Trim() == "intuit quickbooks payroll assisted")
            {
                btnPayroll.Visible = false;
                pnlQBPayroll.Visible = true;
                lblPayroll.Text = "Payroll - ";
            }
            else*/
            {
                //btnPayroll.Visible = true;
                pnlQBPayroll.Visible = false;
                //lblPayroll.Text = "You will be contacted by a payroll specialist within 24 hours.";
            }
        }
        else
            pnlPayroll.Visible = false;

        if ((!pnlCreateDDPDF.Visible) && (!pnlCreateLeasingPDF.Visible) && (!pnlCreateGiftCardPDF.Visible) && (!pnlMerchantFundingAMI.Visible) && (!pnlMerchantFundingBFS.Visible) && (!pnlRapidAdvance.Visible)&&(!pnlPayroll.Visible))
            pnlNoAddlServ.Visible = true;
        else
            pnlNoAddlServ.Visible = false;
    }

    public void imgCreateDDPDF_Click(object sender, EventArgs e)
    {
        try
        {
            //string strMachineName = "www.firstaffiliates.com/OnlineApplication";
            CreatePDF PDFFile = new CreatePDF(AppId);
            string strCheckService = PDFFile.ReturnCheckServiceName();
            string strLocation = PDFFile.ReturnBusinessLocation();
            MemoryStream mStream;
            string FileName = "";
            if (strCheckService.ToLower().Contains("direct debit"))
            {
                if (strLocation.ToLower().Contains("united states"))
                {
                    FileName = Server.MapPath("/OnlineApplication/DirectDebitUS.pdf");
                    mStream = PDFFile.CreateDirectDebitPDF(FileName);

                    if (mStream != null)
                    {
                        LogBL LogData = new LogBL(AppId);
                        LogData.InsertLogData(AffiliateID, "Direct Debit PDF Created");

                        Response.ClearContent();
                        Response.ClearHeaders();
                        Response.ContentType = "application/pdf";
                        Response.AppendHeader("content-disposition", "filename=Direct Debit US.pdf");
                        Response.OutputStream.Write(mStream.GetBuffer(), 0, Convert.ToInt32(mStream.Length));

                        Response.Flush();
                    }
                    else
                        SetErrorMessage("Data not found for this record.");
                }
                else
                {
                    FileName = Server.MapPath("/OnlineApplication/Direct Debit CA.pdf");
                    mStream = PDFFile.CreateDirectDebitPDF(FileName);

                    if (mStream != null)
                    {
                        LogBL LogData = new LogBL(AppId);
                        LogData.InsertLogData(AffiliateID, "Direct Debit PDF Created");

                        Response.ClearContent();
                        Response.ClearHeaders();
                        Response.ContentType = "application/pdf";
                        Response.AppendHeader("content-disposition", "filename=Direct Debit CA.pdf");
                        Response.OutputStream.Write(mStream.GetBuffer(), 0, Convert.ToInt32(mStream.Length));

                        Response.Flush();
                    }
                    else
                        SetErrorMessage("Data not found for this record.");
                }
            }
        }//end try
        catch (Exception err)
        {
            CreateOnlineAppLog Log = new CreateOnlineAppLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + AppId.ToString() + " - " + "AddlServicesPDF.aspx.cs Create Check Service PDF Error - " + err.Message);
            SetErrorMessage("Error Creating Check Service PDF. Please contact Technical Support: <a href=mailto:support@commercetech.com?Subject=Online App Error, App ID: " + Session["AppId"].ToString() + "> support@commercetech.com</a> for assistance.");
        }
    }

    public void imgCreateGiftCardPDF_Click(object sender, EventArgs e)
    {
        try
        {
            //string strMachineName = "www.firstaffiliates.com/OnlineApplication";
            CreatePDF PDFFile = new CreatePDF(AppId);
            string strGiftCardType = PDFFile.ReturnGiftCardType();
            //if (strGiftCardType.ToLower().Contains("EFT"))
            {
                string FileName = Server.MapPath("/OnlineApplication/GETI_Gift_Merchant_App.pdf");
                MemoryStream mStream = PDFFile.CreateGETIPDF(FileName); ;
                if (mStream != null)
                {
                    LogBL LogData = new LogBL(AppId);
                    LogData.InsertLogData(AffiliateID, "Gift Card PDF Created");

                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.ContentType = "application/pdf";
                    Response.AppendHeader("content-disposition", "filename=GETI_Gift_Merchant_App.pdf");
                    Response.AppendHeader("Content-Length", mStream.GetBuffer().Length.ToString());
                    Response.OutputStream.Write(mStream.GetBuffer(), 0, Convert.ToInt32(mStream.GetBuffer().Length));

                    Response.Flush();
                    Response.Close();
                }
                else
                    SetErrorMessage("Data not found for this record.");
            }
                
        }//end try
        catch (Exception err)
        {
            CreateOnlineAppLog Log = new CreateOnlineAppLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + AppId.ToString() + " - " + "AddlServicesPDF.aspx.cs Create GiftCard PDF Error - " + err.Message);
            SetErrorMessage("Error Creating Gift Card PDF. Please contact Technical Support: <a href=mailto:support@commercetech.com?Subject=Online App Error, App ID: " + Session["AppId"].ToString() + "> support@commercetech.com</a> for assistance.");
        }
    }

    public void imgCreateAMIPDF_Click(object sender, EventArgs e)
    {
        try
        {
            //string strMachineName = "www.firstaffiliates.com/OnlineApplication";
            CreatePDF PDFFile = new CreatePDF(AppId);
            string strMCAType = PDFFile.ReturnMCAType();
            if (strMCAType.ToLower().Contains("advanceme"))
            {
                string FileName = Server.MapPath("/OnlineApplication/CAN Pre_Qual_Form.pdf");
                MemoryStream mStream = PDFFile.CreateAMIPDF(FileName); ;
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
                else
                    SetErrorMessage("Data not found for this record.");
            }

        }//end try
        catch (Exception err)
        {
            CreateOnlineAppLog Log = new CreateOnlineAppLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + AppId.ToString() + " - " + "AddlServicesPDF.aspx.cs Create GiftCard PDF Error - " + err.Message);
            SetErrorMessage("Error Advance Me PDF. Please contact Technical Support: <a href=mailto:support@commercetech.com?Subject=Online App Error, App ID: " + Session["AppId"].ToString() + "> support@commercetech.com</a> for assistance.");
        }
    }

    public void imgCreateBFSPDF_Click(object sender, EventArgs e)
    {
        try
        {
            //string strMachineName = "www.firstaffiliates.com/OnlineApplication";
            CreatePDF PDFFile = new CreatePDF(AppId);
            string strMCAType = PDFFile.ReturnMCAType();
            if (strMCAType.ToLower().Contains("business financial services"))
            {
                string FileName = Server.MapPath("/OnlineApplication/BFS_fax_application.pdf");
                MemoryStream mStream = PDFFile.CreateBFSPDF(FileName); ;
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
                    SetErrorMessage("Data not found for this record.");
            }

        }//end try
        catch (Exception err)
        {
            CreateOnlineAppLog Log = new CreateOnlineAppLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + AppId.ToString() + " - " + "AddlServicesPDF.aspx.cs Create GiftCard PDF Error - " + err.Message);
            SetErrorMessage("Error Business Financial Services PDF. Please contact Technical Support: <a href=mailto:support@commercetech.com?Subject=Online App Error, App ID: " + Session["AppId"].ToString() + "> support@commercetech.com</a> for assistance.");
        }
    }

    public void imgCreateRapidAdvancePDF_Click(object sender, EventArgs e)
    {
        try
        {
            //string strMachineName = "www.firstaffiliates.com/OnlineApplication";
            CreatePDF PDFFile = new CreatePDF(AppId);
            string strMCAType = PDFFile.ReturnMCAType();
            if (strMCAType.ToLower().Contains("rapidadvance"))
            {
                string FileName = Server.MapPath("/OnlineApplication/Rapid Advance Application.pdf");
                MemoryStream mStream = PDFFile.CreateRapidAdvancePDF(FileName); ;
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
                    SetErrorMessage("Data not found for this record.");
            }

        }//end try
        catch (Exception err)
        {
            CreateOnlineAppLog Log = new CreateOnlineAppLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + AppId.ToString() + " - " + "AddlServicesPDF.aspx.cs Create GiftCard PDF Error - " + err.Message);
            SetErrorMessage("Error RapidAdvance PDF. Please contact Technical Support: <a href=mailto:support@commercetech.com?Subject=Online App Error, App ID: " + Session["AppId"].ToString() + "> support@commercetech.com</a> for assistance.");
        }
    }

    public void imgCreateLeasingPDF_Click(object sender, EventArgs e)
    {
        try
        {
            //string strMachineName = "www.firstaffiliates.com/OnlineApplication";
            CreatePDF PDFFile = new CreatePDF(AppId);
            string strLeaseCompany = PDFFile.ReturnLeaseCompany();
            string strBusinessState = PDFFile.ReturnBusinessState();
            MemoryStream mStream;
            string FileName = "";
            if (strLeaseCompany.ToLower().Contains("northern"))
            {
                if ((strBusinessState.ToLower().Contains("sd")) || (strBusinessState.ToLower().Contains("ks")) || (strBusinessState.ToLower().Contains("tn"))
                || (strBusinessState.ToLower().Contains("pa")) || (strBusinessState.ToLower().Contains("vt")))
                {
                    FileName = Server.MapPath("/OnlineApplication/Northern Leasing Agreement - SD, KS, TN, PA & VT.pdf");
                    mStream = PDFFile.CreateNorthernLeasePDF(FileName);

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
                        SetErrorMessage("Data not found for this record.");
                }
                else
                {
                    FileName = Server.MapPath("/OnlineApplication/Northern Leasing Agreement - Standard.pdf");
                    mStream = PDFFile.CreateNorthernLeasePDF(FileName);

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
                        SetErrorMessage("Data not found for this record.");
                }
            }
        }//end try
        catch (Exception err)
        {
            CreateOnlineAppLog Log = new CreateOnlineAppLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + AppId.ToString() + " - " + "AddlServicesPDF.aspx.cs Create Lease PDF Error - " + err.Message);
            SetErrorMessage("Error Creating Lease PDF. Please contact Technical Support: <a href=mailto:support@commercetech.com?Subject=Online App Error, App ID: " + Session["AppId"].ToString() + "> support@commercetech.com</a> for assistance.");
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        try
        {
            string strRatesChanged = "False";
            if (Request.Params.Get("RatesChanged") != null)
                strRatesChanged = Request.Params.Get("RatesChanged");
            if ((Session["AcctType"].ToString() == "1") || (Session["AcctType"].ToString() == "4"))
            {
                Response.Redirect("/OnlineApplication/Merchant/CardPct.aspx", false);
            }
            else if (Session["AcctType"].ToString() == "2")
            {
                Response.Redirect("/OnlineApplication/Gateway/companyinfo.aspx?RatesChanged=" + strRatesChanged, false);
            }
            else
            {
                Response.Redirect("/OnlineApplication/Merchant/CardPct.aspx", false);
            }
        }//end try
        catch (Exception err)
        {
            CreateOnlineAppLog Log = new CreateOnlineAppLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + Session["AppId"].ToString() + " - " + "Addl ServicesPDF Error - " + err.Message);
             SetErrorMessage("Error Processing Request. Please contact Technical Support: <a href='mailto:support@commercetech.com?Subject=Online Application Error'> support@commercetech.com. </a>.");
        }
    }
}
