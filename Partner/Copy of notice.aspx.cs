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

public partial class notice : System.Web.UI.Page
{
    private static int AppId = 0;
    private static int AffiliateID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        AppId = Convert.ToInt32(Session["AppId"]);
        AffiliateID = Convert.ToInt32(Session["AffiliateID"]);
        if (Session.IsNewSession)
            Response.Redirect("/OnlineApplication/default.aspx?SessionExpired=True");

        Response.CacheControl = "no-cache";
        Response.AddHeader("Pragma", "no-cache");
        Response.Expires = -1;

        //imgCreatePDF.Attributes.Add("onmouseover", "this.src = '/OnlineApplication/Images/CreatePDF_Mouseover.gif'");
        //imgCreatePDF.Attributes.Add("onmouseout", "this.src = '/OnlineApplication/Images/CreatePDF.gif'");

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

        if (!IsPostBack)
        {
            if (!User.Identity.IsAuthenticated)
                Response.Redirect("/OnlineApplication/default.aspx?Authentication=False");
            else
            {
                try
                {
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
                    }
                    else
                    {
                        SetErrorMessage("The status of your application prevents it from being edited");
                        btnCreatePDF.Enabled = false;
                    }
                }//end try
                catch (Exception err)
                {
                    CreateOnlineAppLog Log = new CreateOnlineAppLog();
                    Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + Session["AppId"].ToString() + " - " + "Notice.aspx Page Load Error - " + err.Message);
                    SetErrorMessage("Error Processing Request. Please contact Technical Support: <a href='mailto:support@commercetech.com?Subject=Online Application Error'> support@commercetech.com. </a>.");
                }
                lblAppId.Text = Convert.ToString(Session["AppId"]);
                lblAppId.Font.Size = FontUnit.Point(10);
                lblAppId.ForeColor = System.Drawing.Color.Red;
            }
        }//end if postback
    }//end page load

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

                pnlCreatePDF.Visible = true;
                pnlMA.Visible = true;
            }
            else
            {
                pnlIncomplete.Visible = false;
                //lblComplete.Visible = true;
                //lblCompleteMsg.Visible = true;
                //Display the Create PDF button
                pnlCreatePDF.Visible = true;
                pnlMA.Visible = true;
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
        if (strProcessor.ToLower().Contains("quickbooks") )
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

    protected void btnCreatePDF_Click(object sender, EventArgs e)
    {
        try
        {
            //string strMachineName = "www.firstaffiliates.com/OnlineApplication";
            CreatePDF PDFFile = new CreatePDF(AppId);
            string strProcessor = PDFFile.ReturnProcessorName();
            MemoryStream mStream;
            string FileName = "";
            if (strProcessor.ToLower().Contains("sage"))
            {
                FileName = Server.MapPath("/OnlineApplication/Paya Application.pdf");
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
                    FileName = Server.MapPath("/OnlineApplication/IPS Application Interchange.pdf");
                else
                    FileName = Server.MapPath("/OnlineApplication/IPS Application.pdf");
                mStream = PDFFile.CreateIMSPDF(FileName);

                if (mStream != null)
                {
                    LogBL LogData = new LogBL(AppId);
                    LogData.InsertLogData(AffiliateID, "IPS PDF Created");

                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.ContentType = "application/pdf";
                    Response.AppendHeader("content-disposition", "filename=IPS Application.pdf");
                    Response.OutputStream.Write(mStream.GetBuffer(), 0, Convert.ToInt32(mStream.Length));

                    Response.Flush();
                    Response.Close();
                }
                else
                    SetErrorMessage("Data not found for this record.");
            }
            else if (strProcessor.ToLower().Contains("ipayment"))
            {
                FileName = Server.MapPath("/OnlineApplication/ipayment application.pdf");

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
                    SetErrorMessage("Data not found for this record.");
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
            else if (strProcessor.ToLower().Contains("optimal-merrick"))
            {
                FileName = Server.MapPath("/OnlineApplication/CNP_Merrick.pdf");
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
                    SetErrorMessage("Data not found for this record.");
            }
            else if (strProcessor.ToLower().Contains("canada"))
            {
                //error at this location because PDF file is invalid
                FileName = Server.MapPath("/OnlineApplication/Optimal_Canada_App.pdf");
                mStream = PDFFile.CreateCanadaPDF(FileName);
                if (mStream != null)
                {
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.ContentType = "application/pdf";
                    Response.AppendHeader("content-disposition", "filename=Optimal_Canadian_App.pdf");
                    Response.OutputStream.Write(mStream.GetBuffer(), 0, Convert.ToInt32(mStream.Length));

                    Response.Flush();
                    Response.Close();

                    LogBL LogData = new LogBL(AppId);
                    LogData.InsertLogData(AffiliateID, "Optimal Canada PDF Created");
                }
                else
                    SetErrorMessage("Data not found for this record.");
            }
            else if ((strProcessor.ToLower().Contains("cal")) || (strProcessor.ToLower().Contains("international")))
            {
                FileName = Server.MapPath("/OnlineApplication/CAL_Application_NA.pdf");
                mStream = PDFFile.CreateInternationalPDF(FileName);

                if (mStream != null)
                {
                    LogBL LogData = new LogBL(AppId);
                    LogData.InsertLogData(AffiliateID, "Optimal CAL PDF Created");

                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.ContentType = "application/pdf";
                    Response.AppendHeader("content-disposition", "filename=Optimal Cal Application.pdf");
                    Response.OutputStream.Write(mStream.GetBuffer(), 0, Convert.ToInt32(mStream.Length));

                    Response.Flush();
                    Response.Close();
                }
                else
                    SetErrorMessage("Data not found for this record.");
            }
            else if (strProcessor.ToLower().Contains("barclays"))
            {
                FileName = Server.MapPath("/OnlineApplication/Barclays Bank Agreement.pdf");
                mStream = PDFFile.CreateBarclaysPDF(FileName);

                if (mStream != null)
                {
                    LogBL LogData = new LogBL(AppId);
                    LogData.InsertLogData(AffiliateID, "Optimal Barclays PDF Created");

                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.ContentType = "application/pdf";
                    Response.AppendHeader("content-disposition", "filename=Optimal Barclays Application.pdf");
                    Response.OutputStream.Write(mStream.GetBuffer(), 0, Convert.ToInt32(mStream.Length));

                    Response.Flush();
                    Response.Close();
                }
                else
                    SetErrorMessage("Data not found for this record.");
            }
            else if (strProcessor.ToLower().Contains("kitts"))
            {
                FileName = Server.MapPath("/OnlineApplication/St_Kitts_Application.pdf");
                mStream = PDFFile.CreateStKittsPDF(FileName);

                if (mStream != null)
                {
                    LogBL LogData = new LogBL(AppId);
                    LogData.InsertLogData(AffiliateID, "Optimal St. Kitts PDF Created");

                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.ContentType = "application/pdf";
                    Response.AppendHeader("content-disposition", "filename=Optimal St. Kitts Application.pdf");
                    Response.OutputStream.Write(mStream.GetBuffer(), 0, Convert.ToInt32(mStream.Length));

                    Response.Flush();
                    Response.Close();
                }
                else
                    SetErrorMessage("Data not found for this record.");
            }
            else if (strProcessor.ToLower().Contains("payvision"))
            {
                FileName = Server.MapPath("/OnlineApplication/Payvision Application.pdf");
                mStream = PDFFile.CreatePayvisionPDF(FileName);

                if (mStream != null)
                {
                    LogBL LogData = new LogBL(AppId);
                    LogData.InsertLogData(AffiliateID, "Payvision PDF Created");

                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.ContentType = "application/pdf";
                    Response.AppendHeader("content-disposition", "filename=Payvision Application.pdf");
                    Response.OutputStream.Write(mStream.GetBuffer(), 0, Convert.ToInt32(mStream.Length));

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
            SetErrorMessage("Error Creating PDF. Please contact Technical Support: <a href=mailto:support@commercetech.com?Subject=Online App Error, App ID: " + Session["AppId"].ToString() + "> support@commercetech.com. </a> for assistance.");
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
            string FileName = Server.MapPath("/OnlineApplication/Paya Application.pdf");
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
                Response.OutputStream.Write(mStream.GetBuffer(), 0, Convert.ToInt32(mStream.Length));

                Response.Flush();
                Response.Close();
            }
            else
                SetErrorMessage("Data not found for this record.");
        }
        catch (Exception err)
        {
            SetErrorMessage("Error Creating PDF. Please contact Technical Support: <a href=mailto:support@commercetech.com?Subject=Online App Error, App ID: " + Session["AppId"].ToString() + "> support@commercetech.com. </a> for assistance.");
            CreateOnlineAppLog Log = new CreateOnlineAppLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + AppId.ToString() + " - " + "Notice.aspx.cs Create Sage App PDF Error - " + err.Message);
        }
    }
    
    public void btnSageMOTO_Click(object sender, EventArgs e)
    {
        try
        {
            string FileName = Server.MapPath("/OnlineApplication/Sage MOTO-Internet Question.pdf");
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
                Response.OutputStream.Write(mStream.GetBuffer(), 0, Convert.ToInt32(mStream.Length));

                Response.Flush();
                Response.Close();
            }
            else
                SetErrorMessage("Data not found for this record.");
        }
        catch (Exception err)
        {
            SetErrorMessage("Error Creating PDF. Please contact Technical Support: <a href=mailto:support@commercetech.com?Subject=Online App Error, App ID: " + Session["AppId"].ToString() + "> support@commercetech.com. </a> for assistance.");
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
            string FileName = Server.MapPath("/OnlineApplication/ChaseMPA.pdf");

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
                SetErrorMessage("Data not found for this record.");
        }//end try
        catch (Exception err)
        {
            SetErrorMessage("Error Creating PDF. Please contact Technical Support: <a href=mailto:support@commercetech.com?Subject=Online App Error, App ID: " + Session["AppId"].ToString() + "> support@commercetech.com. </a> for assistance.");
            CreateOnlineAppLog Log = new CreateOnlineAppLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + AppId.ToString() + " - " + "Merchant PDF - Create Chase MPA PDF Error - " + err.Message);
        }
    }

    public void btnChaseFS3Tier_Click(object sender, EventArgs e)
    {
        try
        {
            string FileName = Server.MapPath("/OnlineApplication/CardConnect Application.pdf");

            CreatePDF PDFFile = new CreatePDF(AppId);
            MemoryStream mStream = PDFFile.CreateChaseFS3TierPDF(FileName);
            if (mStream != null)
            {
                Response.ClearContent();
                Response.ClearHeaders();
                Response.ContentType = "application/pdf";
                Response.AppendHeader("content-disposition", "filename=CardConnect Application.pdf");
                Response.OutputStream.Write(mStream.GetBuffer(), 0, Convert.ToInt32(mStream.Length));

                Response.Flush();
                Response.Close();

                LogBL LogData = new LogBL(AppId);
                LogData.InsertLogData(AffiliateID, "Chase Fee schedule 3 tier Created");
            }
            else
                SetErrorMessage("Data not found for this record.");
        }//end try
        catch (Exception err)
        {
            SetErrorMessage("Error Creating PDF. Please contact Technical Support: <a href=mailto:support@commercetech.com?Subject=Online App Error, App ID: " + Session["AppId"].ToString() + "> support@commercetech.com. </a> for assistance.");
            CreateOnlineAppLog Log = new CreateOnlineAppLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + AppId.ToString() + " - " + "Merchant PDF - Create Chase Fee schedule 3 tier PDF Error - " + err.Message);
        }
    }

    public void btnChaseFSInterchangePlus_Click(object sender, EventArgs e)
    {
        try
        {
            string FileName = Server.MapPath("/OnlineApplication/Chase Fee schedule interchange plus.pdf");

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
                SetErrorMessage("Data not found for this record.");
        }//end try
        catch (Exception err)
        {
            SetErrorMessage("Error Creating PDF. Please contact Technical Support: <a href=mailto:support@commercetech.com?Subject=Online App Error, App ID: " + Session["AppId"].ToString() + "> support@commercetech.com. </a> for assistance.");
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
