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
using System.Data.SqlClient;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Text;
using System.IO;
using DataLayer;

public partial class Main : System.Web.UI.MasterPage
{
    private static int AppId = 0;
    private static int AffiliateID = 0;
    private static int AType = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        AppId = Convert.ToInt32(Session["AppId"]);
        AffiliateID = Convert.ToInt32(Session["AffiliateID"]);
        AType = Convert.ToInt32(Session["AcctType"]);
        //If session has expired, then redirect to default.aspx
        if (Session.IsNewSession)
            Response.Redirect("/Application/default.aspx?SessionExpired=true");

        string strURL = Request.Url.ToString();
        if ((strURL.Contains("'")) || (strURL.Contains("--")) || (strURL.Contains("#")) || (strURL.Contains(";")) || (strURL.Contains("exec")) || (strURL.Contains("EXEC")) || (strURL.Contains("<")) || (strURL.Contains(">")) || (strURL.ToLower().Contains("script")))
            Response.Redirect("/Application/DefaultError.aspx");

        if (!IsPostBack)
        {
            Style statusStyle = new Style();
            statusStyle.Font.Bold = true;
            statusStyle.CssClass = "LabelsBodySmall";
            lblStatus.ApplyStyle(statusStyle);
            lblStatusGW.ApplyStyle(statusStyle);
                  
            string referrer = Request.Url.AbsoluteUri.ToString();
           
            string strAcctType = "Merchant";
            if (referrer.Contains("Gateway"))
                strAcctType = "Gateway";
            else if (referrer.Contains("Merchant"))
                strAcctType = "Merchant";

            OnlineAppStatus Status = new OnlineAppStatus(AppId);
            string strStatus = Status.GetStatus("Merchant");
            string strStatusGW = Status.GetStatus("Gateway");
            
            #region CHECK COMPLETE
            CardPCT CardPct = new CardPCT(AppId);
            if (CardPct.CheckCardPCTComplete() > 0)
            {
                imgCardPctStatus.ImageUrl = "/Application/Images/Complete.gif";
            }
            else
            {
                imgCardPctStatus.ImageUrl = "/Application/Images/Attention.gif";
            }

            CompanyTable ComTable = new CompanyTable();

            CompanyInfo Company = new CompanyInfo(AppId);
            if (Company.CheckCompanyComplete()> 0)
            {
                imgCompanyStatus.ImageUrl = "/Application/Images/Complete.gif";
                imgGCompanyStatus.ImageUrl = "/Application/Images/Complete.gif";
            }
            else
            {
                imgCompanyStatus.ImageUrl = "/Application/Images/Attention.gif";
                imgGCompanyStatus.ImageUrl = "/Application/Images/Attention.gif";
            }

            BusinessInfo Business = new BusinessInfo(AppId);
            if (Business.CheckBusinessComplete( strAcctType) > 0)
            {
                imgBusinessStatus.ImageUrl = "/Application/Images/Complete.gif";
                imgGBusinessStatus.ImageUrl = "/Application/Images/Complete.gif";
            }
            else
            {
                imgBusinessStatus.ImageUrl = "/Application/Images/Attention.gif";
                imgGBusinessStatus.ImageUrl = "/Application/Images/Attention.gif";
            }

            PrincipalInfo Principal = new PrincipalInfo(AppId);
            if (Principal.CheckPrincipalComplete(strAcctType) > 0)
            {
                imgPrincipalStatus.ImageUrl = "/Application/Images/Complete.gif";
                imgGPrincipalStatus.ImageUrl = "/Application/Images/Complete.gif";
            }
            else
            {
                imgPrincipalStatus.ImageUrl = "/Application/Images/Attention.gif";
                imgGPrincipalStatus.ImageUrl = "/Application/Images/Attention.gif";
            }

            if (ComTable.CheckSigningComplete(AppId) > 0)
            {
                imgSigningStatus.ImageUrl = "/Application/Images/Complete.gif";
                //imgGSigning.ImageUrl = "/Application/Images/Complete.gif";
            }
            else
            {
                imgSigningStatus.ImageUrl = "/Application/Images/Attention.gif";
                //imgGSigningStatus.ImageUrl = "/Application/Images/Attention.gif";
            }

            OtherInfo Other1 = new OtherInfo(AppId);
            if (Other1.CheckUploadComplete(strAcctType) > 0)
            {

                imgUploadStatus.ImageUrl = "/Application/Images/Complete.gif";
                //imgGBankingStatus.ImageUrl = "/Application/Images/Complete.gif";
            }
            else
            {
                imgUploadStatus.ImageUrl = "/Application/Images/Attention.gif";
                //imgGBankingStatus.ImageUrl = "/Application/Images/Attention.gif";
            }

            if (referrer.Contains("Merchant"))
            {
                OtherInfo Other = new OtherInfo(AppId);
                if (Other.CheckOtherInfoComplete(strAcctType) > 0)
                {
                    imgBankingStatus.ImageUrl = "/Application/Images/Complete.gif";
                    imgGBankingStatus.ImageUrl = "/Application/Images/Complete.gif";
                }
                else
                {
                    imgBankingStatus.ImageUrl = "/Application/Images/Attention.gif";
                    imgGBankingStatus.ImageUrl = "/Application/Images/Attention.gif";
                }

                if (Other.CheckUploadComplete(strAcctType) > 0)
                {

                    imgUploadStatus.ImageUrl = "/Application/Images/Complete.gif";
                    //imgGBankingStatus.ImageUrl = "/Application/Images/Complete.gif";
                }
                else
                {
                    imgUploadStatus.ImageUrl = "/Application/Images/Attention.gif";
                    //imgGBankingStatus.ImageUrl = "/Application/Images/Attention.gif";
                }





                
            }

            if (referrer.Contains("Gateway"))
            {
                BankingInfo Banking = new BankingInfo(AppId);
                if (Banking.CheckBankingComplete(strAcctType) > 0)
                {
                    imgBankingStatus.ImageUrl = "/Application/Images/Complete.gif";
                    imgGBankingStatus.ImageUrl = "/Application/Images/Complete.gif";
                }
                else
                {
                    imgBankingStatus.ImageUrl = "/Application/Images/Attention.gif";
                    imgGBankingStatus.ImageUrl = "/Application/Images/Attention.gif";
                }
            }
            else
            {
                if ((AType == 4) || (AType == 1))
                {
                    OtherInfo Other = new OtherInfo(AppId);
                    if (Other.CheckOtherInfoComplete(strAcctType) > 0)
                    {
                        imgBankingStatus.ImageUrl = "/Application/Images/Complete.gif";
                        imgGBankingStatus.ImageUrl = "/Application/Images/Complete.gif";
                    }
                    else
                    {
                        imgBankingStatus.ImageUrl = "/Application/Images/Attention.gif";
                        imgGBankingStatus.ImageUrl = "/Application/Images/Attention.gif";
                    }
                }
                else
                {
                    BankingInfo Banking = new BankingInfo(AppId);
                    if (Banking.CheckBankingComplete(strAcctType) > 0)
                    {
                        imgBankingStatus.ImageUrl = "/Application/Images/Complete.gif";
                        imgGBankingStatus.ImageUrl = "/Application/Images/Complete.gif";
                    }
                    else
                    {
                        imgBankingStatus.ImageUrl = "/Application/Images/Attention.gif";
                        imgGBankingStatus.ImageUrl = "/Application/Images/Attention.gif";
                    }
                }
            }

            ReprogramInfo Reprogram = new ReprogramInfo(AppId);
            if (Reprogram.CheckReprogramComplete() > 0  )
            {
                imgGPlatformStatus.ImageUrl = "/Application/Images/Complete.gif";
                imgGPlatformStatus.ImageUrl = "/Application/Images/Complete.gif";
            }
            else
            {
                imgGPlatformStatus.ImageUrl = "/Application/Images/Attention.gif";
                imgGPlatformStatus.ImageUrl = "/Application/Images/Attention.gif";
            }
            #endregion


            #region PANEL DISPLAY

            int PackageID = 0;
            NewAppInfo ReturnApp = new NewAppInfo(AppId);
            PackageID = ReturnApp.ReturnPID();

            if (referrer.Contains("Merchant"))
            {
                pnlMerchantStatus.Visible = true;
                pnlGatewayStatus.Visible = false;
                pnlViewGatewayInfo.Visible = true;
                pnlViewMerchantInfo.Visible = false;
                pnlStatusGW.Visible = false;
                pnlStatus.Visible = true;                
                lblStatus.Text = Server.HtmlEncode(strStatus);
                lblStatus.Visible = true;
            }
            //else get the gateway status
            if (referrer.Contains("Gateway"))
            {
                pnlMerchantStatus.Visible = false;
                pnlGatewayStatus.Visible = true;
                pnlViewGatewayInfo.Visible = false;
                pnlViewMerchantInfo.Visible = true;
                pnlStatusGW.Visible = true;
                pnlStatus.Visible = false;                
                lblStatusGW.Text = Server.HtmlEncode(strStatusGW);
                lblStatusGW.Visible = true;
            }
            if (AType == 4)
            {
                pnlGatewaySignup.Visible = false;
                pnlMerchantSignup.Visible = false;
                pnlAddlServicesSignup.Visible = true;
                pnlGeneratePDF.Visible = true;
            }
            else if (AType == 1)
            {
                pnlGatewaySignup.Visible = true;
                pnlMerchantSignup.Visible = false;
                pnlViewGatewayInfo.Visible = false;
                pnlAddlServicesSignup.Visible = true;
                pnlGeneratePDF.Visible = true;
            }
            else if (AType == 2)
            {
                pnlGatewaySignup.Visible = false;
                pnlMerchantSignup.Visible = true;
                pnlViewMerchantInfo.Visible = false;
                pnlAddlServicesSignup.Visible = true;
                pnlGeneratePDF.Visible = false;
            }

            else if (AType == 3)
            {
                pnlGatewaySignup.Visible = true;
                pnlMerchantSignup.Visible = true;
                pnlViewMerchantInfo.Visible = false;
                pnlViewGatewayInfo.Visible = false;
                pnlAddlServicesSignup.Visible = true;
                pnlGeneratePDF.Visible = false;

                pnlStatus.Visible = false;
            }

            if (referrer.ToLower().Contains("updateacct.aspx") || referrer.ToLower().Contains("editprofile.aspx") || referrer.ToLower().Contains("equipment.aspx") || referrer.ToLower().Contains("locked.aspx") || referrer.ToLower().Contains("addlservices.aspx") || referrer.ToLower().Contains("addlservicespdf.aspx") || referrer.ToLower().Contains("contactus.aspx") || referrer.ToLower().Contains("aboutus.aspx") || referrer.ToLower().Contains("updateacctgw.aspx"))
            {
                pnlMerchantStatus.Visible = false;
                pnlGatewayStatus.Visible = false;
                pnlViewGatewayInfo.Visible = false;
                pnlViewMerchantInfo.Visible = false;
                pnlStatus.Visible = false;
                pnlStatusGW.Visible = false;
                if ((AType == 1) || (AType == 4))
                {
                    lblStatus.Visible = true;
                    lblStatus.Text = Server.HtmlEncode(strStatus);
                    pnlStatus.Visible = true;
                    pnlViewMerchantInfo.Visible = true;
                }
                if ((AType == 2) || (AType == 4))
                {
                    lblStatusGW.Visible = true;
                    lblStatusGW.Text = Server.HtmlEncode(strStatusGW);
                    pnlStatusGW.Visible = true;
                    pnlViewGatewayInfo.Visible = true;
                }

                /*if (AType == 1)
                {                    
                    lblStatus.Text = Server.HtmlEncode(strStatus);
                    pnlStatus.Visible = true;
                }
                if (AType == 2)
                {
                    lblStatusGW.Text = Server.HtmlEncode(strStatusGW);
                    pnlStatusGW.Visible = true;
                }
                if (AType == 3)
                {
                    pnlStatus.Visible = false;                    
                    pnlStatusGW.Visible = false;                    
                }
                if (AType == 4)
                {
                    lblStatus.Text = Server.HtmlEncode(strStatus);
                    pnlStatus.Visible = true;
                    lblStatusGW.Text = Server.HtmlEncode(strStatusGW);
                    pnlStatusGW.Visible = true;
                }*/
                pnlSignups.Visible = true;
                pnlUserInfo.Visible = true;
                if (referrer.ToLower().Contains("UpdateAcctGW.aspx"))
                {
                    pnlGeneratePDF.Visible = true;
                }else {
                    pnlGeneratePDF.Visible = false;
                }
                
            }

            if (referrer.ToLower().Contains("onlineapplication/notice.aspx"))
            {
                pnlMerchantStatus.Visible = false;
                pnlGatewayStatus.Visible = false;
                
                lblStatus.Text = Server.HtmlEncode(strStatus);
                pnlStatus.Visible = true;
                lblStatus.Visible = true;
                pnlStatusGW.Visible = false;
                pnlUserInfo.Visible = true;
                pnlGeneratePDF.Visible = false;
                if (AType == 4)
                {
                    pnlMerchantStatus.Visible = true;
                    pnlGatewayStatus.Visible = true;
                    
                    pnlViewMerchantInfo.Visible = false;
                    pnlViewGatewayInfo.Visible = false;

                    pnlSignups.Visible = true;
                }
                else if (AType == 1)
                {
                    pnlMerchantStatus.Visible = true;
                    pnlGatewayStatus.Visible = false;
                    
                    pnlViewMerchantInfo.Visible = false;
                    pnlViewGatewayInfo.Visible = false;

                    pnlSignups.Visible = true;                    
                    pnlMerchantSignup.Visible = false;
                    pnlGatewaySignup.Visible = true;
                }
                else if (AType == 2)
                {
                    pnlMerchantStatus.Visible = false;
                    pnlGatewayStatus.Visible = true;
                    
                    pnlViewMerchantInfo.Visible = false;
                    pnlViewGatewayInfo.Visible = false;

                    pnlSignups.Visible = true;                    
                    pnlMerchantSignup.Visible = true;
                    pnlGatewaySignup.Visible = false;
                }
                else if (AType == 3)
                {
                    pnlViewGatewayInfo.Visible = false;
                    pnlViewMerchantInfo.Visible = false;
                    pnlStatus.Visible = false;
                    pnlStatusGW.Visible = false;
                    pnlSignups.Visible = true;
                    pnlGatewaySignup.Visible = true;
                    pnlMerchantSignup.Visible = true;
                }
            }

            //if QB merchant services account or QB POS account or IPS GoPayment account then don't show create PDF button

            if ((PackageID == 255) )
            {
                pnlGeneratePDF.Visible = false;
                pnlViewGatewayInfo.Visible = true;
            }
            else if ((PackageID == 254) || (PackageID == 253))
            {
                pnlGeneratePDF.Visible = false;
                pnlViewGatewayInfo.Visible = false;
            }

            
            CreatePDF PDFFile = new CreatePDF(AppId);
            string strProcessor = PDFFile.ReturnProcessorName();
            if (strProcessor.ToLower().Contains("quickbooks"))
                pnlGeneratePDF.Visible = false;
            /*
            if ((Convert.ToString(PDFFile.ReturnLeaseCompany()) != "") || (Convert.ToString(PDFFile.ReturnGiftCardType()) != "") || (Convert.ToString(PDFFile.ReturnMCAType()) != ""))
            { pndAddlPDF.Visible = true;}
            else { pndAddlPDF.Visible = false; }*/

            #endregion
        }//end if postback
    }//end function page load

    protected void linkCardPctClick(object sender, EventArgs e)
    {
        Response.Redirect("/Application/Merchant/cardpct.aspx", false);
    }

    protected void linkCompanyClick(object sender, EventArgs e)
    {
        Response.Redirect("/Application/Merchant/company.aspx", false);
    }

    protected void linkBusinessClick(object sender, EventArgs e)
    {
        Response.Redirect("/Application/Merchant/businessinfo.aspx", false);
    }

    protected void linkPrincipalClick(object sender, EventArgs e)
    {
        Response.Redirect("/Application/Merchant/principal.aspx", false);
    }

    protected void linkBankingClick(object sender, EventArgs e)
    {
        Response.Redirect("/Application/Merchant/bankinginfo.aspx", false);
    }


    protected void linkPDFClick(object sender, EventArgs e)
    {
        Response.Redirect("/Application/notice.aspx", false);
    }

    protected void linkUploadClick(object sender, EventArgs e)
    {
        Response.Redirect("/Application/UploadDocuSignDocs.aspx", false);
    }
    //This function displays error messages
    protected void SetMessage(string errText)
    {
        lblErrorMsg.Visible = true;
        lblErrorMsg.Text = errText;
    }//end function set error message
}
