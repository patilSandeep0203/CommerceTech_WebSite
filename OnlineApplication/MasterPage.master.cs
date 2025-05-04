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
using BLAffiliateWiz;
//using System.Web.Mail;
using System.Net.Mail;
using System.Xml;
using System.Xml.XPath;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //ValidatetxtName2.Enabled = false;
        SetMetaContent();
        if (!IsPostBack)
        {
            try
            {
                int AffiliateID = 0;

                HttpCookie ck = Request.Cookies["AffiliateWizAffiliateID"];
                if (ck != null)
                    AffiliateID = Convert.ToInt32(ck["AffiliateID"].ToString().Trim());

                if ((AffiliateID != 0) && (AffiliateID != null))
                {
                    pnlPageHeading.Visible = true;

                    //Get Affiliate Name from Affiliate ID
                    AffiliatesWizBL Affiliate = new AffiliatesWizBL();
                    DataSet ds = Affiliate.GetAffiliateInfoBasic(AffiliateID);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow dr = ds.Tables[0].Rows[0];
                        if (!Convert.IsDBNull(dr["DisplayLogo"]))
                        {
                            if (Convert.ToBoolean(dr["DisplayLogo"]) == true)
                            {
                                string strUrlLogo = "../PartnerPortal/PartnerLogo/" + Convert.ToString(AffiliateID) + "/" + Convert.ToString(dr["LogoFileName"]);
                                //string strUrlLogo = "//Server-R310/Shared/website/home/PartnerPortal/PartnerLogo/" + Convert.ToString(AffiliateID) + "/" + Convert.ToString(dr["LogoFileName"]);

                                if (!Convert.IsDBNull(dr["LogoFileName"]))
                                {
                                    if (Convert.ToString(dr["LogoFileName"]) != "")
                                    {
                                        lblPartnerName.Visible = false;
                                        //imgPartnerLogo.Attributes["src"] = ResolveUrl(strUrlLogo);
                                        lblPartnerType.Text = dr["Category"].ToString().Trim();
                                    }
                                    else
                                    {
                                        lblPartnerLogo.Visible = false;
                                        lblPartnerName.Text = dr["DBA"].ToString().Trim();
                                        lblPartnerType.Text = dr["Category"].ToString().Trim();
                                    }
                                }
                                else
                                {
                                    lblPartnerLogo.Visible = false;
                                    lblPartnerName.Text = dr["DBA"].ToString().Trim();
                                    lblPartnerType.Text = dr["Category"].ToString().Trim();
                                }
                            }
                            else
                            {
                                lblPartnerLogo.Visible = false;
                                lblPartnerName.Text = dr["DBA"].ToString().Trim();
                                lblPartnerType.Text = dr["Category"].ToString().Trim();
                            }
                        }
                        else
                        {
                            lblPartnerLogo.Visible = false;
                            lblPartnerName.Text = dr["DBA"].ToString().Trim();
                            lblPartnerType.Text = dr["Category"].ToString().Trim();
                        }

                    }
                    else
                        lblPartnerName.Text = "Commerce Technologies";

                    //lnkOnlineApp.NavigateUrl = "https://www.firstaffiliates.com/OnlineApplication/Start.aspx?Referral=" + AffiliateID.ToString();
                    //lnkOnlineApp.Target = "_blank";
                }
                else
                {

                    if ((Request.Params.Get("A") != null))
                        AffiliateID = Convert.ToInt32(Request.Params.Get("A"));

                    if ((AffiliateID != 0) && (AffiliateID != null))
                    {
                        pnlPageHeading.Visible = true;

                        //Get Affiliate Name from Affiliate ID
                        AffiliatesWizBL Affiliate = new AffiliatesWizBL();
                        DataSet ds = Affiliate.GetAffiliateInfoBasic(AffiliateID);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            if (!Convert.IsDBNull(dr["DisplayLogo"]))
                            {
                                if (Convert.ToBoolean(dr["DisplayLogo"]) == true)
                                {
                                    string strUrlLogo = "../PartnerPortal/PartnerLogo/" + Convert.ToString(AffiliateID) + "/" + Convert.ToString(dr["LogoFileName"]);
                                    //string strUrlLogo = "//Server-R310/Shared/website/home/PartnerPortal/PartnerLogo/" + Convert.ToString(AffiliateID) + "/" + Convert.ToString(dr["LogoFileName"]);

                                    if (!Convert.IsDBNull(dr["LogoFileName"]))
                                    {
                                        if (Convert.ToString(dr["LogoFileName"]) != "")
                                        {
                                            lblPartnerName.Visible = false;
                                            //imgPartnerLogo.Attributes["src"] = ResolveUrl(strUrlLogo);
                                            lblPartnerType.Text = dr["Category"].ToString().Trim();
                                        }
                                        else
                                        {
                                            lblPartnerLogo.Visible = false;
                                            lblPartnerName.Text = dr["DBA"].ToString().Trim();
                                            lblPartnerType.Text = dr["Category"].ToString().Trim();
                                        }
                                    }
                                    else
                                    {
                                        lblPartnerLogo.Visible = false;
                                        lblPartnerName.Text = dr["DBA"].ToString().Trim();
                                        lblPartnerType.Text = dr["Category"].ToString().Trim();
                                    }
                                }
                                else
                                {
                                    lblPartnerLogo.Visible = false;
                                    lblPartnerName.Text = dr["DBA"].ToString().Trim();
                                    lblPartnerType.Text = dr["Category"].ToString().Trim();
                                }
                            }
                            else
                            {
                                lblPartnerLogo.Visible = false;
                                lblPartnerName.Text = dr["DBA"].ToString().Trim();
                                lblPartnerType.Text = dr["Category"].ToString().Trim();
                            }

                        }
                        else
                            lblPartnerName.Text = "Commerce Technologies";

                        //lnkOnlineApp.NavigateUrl = "https://www.firstaffiliates.com/OnlineApplication/Start.aspx?Referral=" + AffiliateID.ToString();
                        //lnkOnlineApp.Target = "_blank";
                    }
                }
                
                //Referral info transferred to Online App


                //Don't show free report signup panel when on these pages
                //if (Request.Url.AbsoluteUri.ToString().Contains("apply.aspx") || Request.Url.AbsoluteUri.ToString().Contains("consult.aspx") ||
                    //Request.Url.AbsoluteUri.ToString().Contains("contactus.aspx") || Request.Url.AbsoluteUri.ToString().Contains("AboutUs.aspx"))
                    //pnlFreeReport.Visible = false;
                //else
                    //pnlFreeReport.Visible = true;
                /*
                if (Request.Url.AbsoluteUri.ToString().Contains("SageAccountingSoftware.aspx"))
                {
                    string myKeyWords = "Sage 50 Accounting Software, Peachtree, Sage 50, Accounting Software , Accounting solutions, Sage 50 Quantum, top accounting software, compare accounting software, sage payroll software, small business accounting software";
                    HtmlMeta keywords = new HtmlMeta();
                    keywords.Name = "keywords";
                    keywords.Content = myKeyWords;
                    Head1.Controls.Add(keywords);
                    
                }*/
            }//end try
            catch (Exception err)
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = "Error Loading Master Page";
            }
        }//end is postback
    }



    protected void onlineApplication_Click(object sender, EventArgs e)
    {
        int AffiliateID = 0;
        string urlOnlineApp = "";

        HttpCookie ck = Request.Cookies["AffiliateWizAffiliateID"];
        if (ck != null)
            AffiliateID = Convert.ToInt32(ck["AffiliateID"].ToString().Trim());

        if ((Request.Params.Get("A") != null))
            AffiliateID = Convert.ToInt32(Request.Params.Get("A"));
        if ((AffiliateID != 0) && (AffiliateID != null))
        {
            urlOnlineApp = "https://www.firstaffiliates.com/OnlineApplication/Start.aspx?Referral=" + AffiliateID.ToString();
            Response.Redirect(urlOnlineApp);
        }
        else { Response.Redirect("https://www.firstaffiliates.com/OnlineApplication/Start.aspx"); }
        //Response.Redirect("https://www.firstaffiliates.com/OnlineApplication/Start.aspx");        
    }

    /*protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                string strError = ValidateData();
                if (strError == "")
                {
                    int AffiliateID = 11;
                    HttpCookie ck = Request.Cookies["AffiliateWizAffiliateID"];
                    if (ck != null)
                        AffiliateID = Convert.ToInt32(ck["AffiliateID"].ToString().Trim());
                    //Insert data
                    ReportsBL Reports = new ReportsBL();
                    Reports.InsertFreeReport(AffiliateID.ToString(), txtName.Text.Trim(), txtEmail.Text.Trim());

                    //Send Email
                    SendEmailToUser();
                    //SendEmailToAdmin();
                    SendEmailToPartner();
                    Response.Redirect("FreeLeads.aspx");
                }
                else
                {
                    lblErrorMessage.Visible = true;
                    lblErrorMessage.Text = strError;
                }
            }//end if page is valid
        }
        catch (Exception err)
        {
            lblErrorMessage.Visible = true;
            lblErrorMessage.Text = "Error Processing Request. Please contact Technical Support";
        }
    }*/

    //This function emails user
    /*public void SendEmailToUser()
    {
        try
        {
            //Send Email to agent after successful registration before redirecting user
            string strSubject = "Commerce Technologies - Thank You";
            MailMessage msg = new MailMessage();
            //msg.To = txtEmail.Text.Trim();
            //msg.To.Add(new MailAddress(txtEmail.Text.Trim()));
            //msg.From = "information@ecenow.com";
            msg.From = new MailAddress("information@ecenow.com");
            msg.Subject = strSubject;
            msg.Body = GetBody();
            msg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            //SmtpMail.Send(msg);
            SmtpClient mSmtpClient = new SmtpClient();
            mSmtpClient.Send(msg);
        }
        catch (Exception err)
        {
            lblErrorMessage.Visible = true;
            lblErrorMessage.Text = err.Message;
        }
    }*/
    //end function send email

    //This function emails Admin
    /*public void SendEmailToAdmin()
    {
        try
        {
            int AffiliateID = 11;
            HttpCookie ck = Request.Cookies["AffiliateWizAffiliateID"];
            if (ck != null)
                AffiliateID = Convert.ToInt32(ck["AffiliateID"].ToString().Trim());
            string CompanyName = string.Empty;
            //Get Affiliate Name from Affiliate ID
            AffiliatesWizBL Affiliate = new AffiliatesWizBL();
            DataSet ds = Affiliate.GetAffiliateInfoBasic(AffiliateID);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                CompanyName = dr["CompanyName"].ToString().Trim();
            }
            //Send Email to agent after successful registration before redirecting user
            string strSubject = "The Following User Submitted the Information for Free Report - Referral Source: " + CompanyName;
            MailMessage msg = new MailMessage();
            //msg.To = "information@ecenow.com";
            msg.To.Add(new MailAddress("information@ecenow.com"));
            //msg.To.Add(new MailAddress("twu@ecenow.com"));
            //msg.From = "information@ecenow.com";
            //msg.From = new MailAddress("information@ecenow.com");
            msg.From = new MailAddress(txtEmail.Text.ToString().Trim());
            msg.Subject = strSubject;
            msg.Body = "User Submitted the Following Information" + System.Environment.NewLine + System.Environment.NewLine + "Name : " + txtName.Text.Trim() + System.Environment.NewLine + "Email : " + txtEmail.Text.Trim() + System.Environment.NewLine;
            //SmtpMail.Send(msg);
            SmtpClient mSmtpClient = new SmtpClient();
            mSmtpClient.Send(msg);
        }
        catch (Exception err)
        {
            lblErrorMessage.Visible = true;
            lblErrorMessage.Text = err.Message;
        }
    }
    */
    //end function send email

    //This function emails Partner
    /*public void SendEmailToPartner()
    {
        try
        {
            int AffiliateID = 11;
            HttpCookie ck = Request.Cookies["AffiliateWizAffiliateID"];
            if (ck != null)
                AffiliateID = Convert.ToInt32(ck["AffiliateID"].ToString().Trim());
            string CompanyName = string.Empty;
            string Email = "information@ecenow.com";
            MailMessage msg = new MailMessage();
            //Get Affiliate Name from Affiliate ID
            AffiliatesWizBL Affiliate = new AffiliatesWizBL();
            DataSet ds = Affiliate.GetAffiliateInfoBasic(AffiliateID);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                if (dr["SendEmailNotification"].ToString().Trim() == "True")
                {
                    CompanyName = dr["CompanyName"].ToString().Trim();
                    Email = dr["Email"].ToString().Trim();
                    msg.CC.Add(new MailAddress("information@ecenow.com"));
                }
            }
            //Send Email to agent after successful registration before redirecting user
            string strSubject = "The Following User Submitted the Information for Free Report - Referral Source: " + CompanyName;
            //msg.To = Email;
            msg.To.Add(new MailAddress(Email));
            //msg.To.Add(new MailAddress("twu@ecenow.com"));
            //msg.From = "information@ecenow.com";
            //msg.From = new MailAddress("information@ecenow.com");
            msg.From = new MailAddress(txtEmail.Text.ToString().Trim());
            msg.Subject = strSubject;
            msg.Body = "User Submitted the Following Information" + System.Environment.NewLine + System.Environment.NewLine + "Name : " + txtName.Text.Trim() + System.Environment.NewLine + "Email : " + txtEmail.Text.Trim() + System.Environment.NewLine;
            //SmtpMail.Send(msg);
            SmtpClient mSmtpClient = new SmtpClient();
            mSmtpClient.Send(msg);
        }
        catch (Exception err)
        {
            lblErrorMessage.Visible = true;
            lblErrorMessage.Text = err.Message;
        }
    }*///end function send email

    //This function creates email body
    /*public string GetBody()
    {
        int AffiliateID = 11;
        HttpCookie ck = Request.Cookies["AffiliateWizAffiliateID"];
        if (ck != null)
            AffiliateID = Convert.ToInt32(ck["AffiliateID"].ToString().Trim());
        string Body = "Thank you for taking the time to fill out our form." + System.Environment.NewLine + "UNDERSTAND E-COMMERCE IN 2 MINUTES" + System.Environment.NewLine + "by Commerce Technologies" + System.Environment.NewLine + System.Environment.NewLine;
        Body = Body + "This brief report is designed to give you a quick overview of how e-commerce works." + System.Environment.NewLine + "CRITICAL COMPONENTS FOR E-COMMERCE" + System.Environment.NewLine + System.Environment.NewLine;
        Body = Body + "1) Website : Your online connection to your prospects and customers. There are a number of places on the web to get 'free' websites - although you may be required to display an advertisement. The design of your site is critical to the success of your business! If it does not look like a professional, legitimate business - NO ONE WILL ORDER ANYTHING - no matter how great the products you have. If you are serious about doing business on the web, either go back to school and learn html, java, cgi, etc., or hire a professional to design your site." + System.Environment.NewLine;
        Body = Body + "2) Merchant account : Bank authorized account which allows you to accept major credit cards, electronic checks, etc. Many banks will not give you a merchant account if you do business on the internet because they may classify it as high risk. Be careful when looking for a merchant account provider! Watch out for hidden fees, high setup fees, and other unscrupulous business practices." + System.Environment.NewLine;
        Body = Body + "3) Payment Transaction Software : Software which actually processes customer order information, address, credit card number, etc. The data is sent to a credit card authorization network which verifies that the credit card is valid and verifies that the shipping address matches the billing address. A possible warning sign that the card has been stolen is that the billing and shipping addresses do not match." + System.Environment.NewLine;
        Body = Body + "4) Secure Server Connection - https:// : Link to a special computer which encrypts confidential ordering data for customer protection. You know you are on a secure server when the URL in your browser says 'https://'. The 's' stands for 'secure.' If ordering information is not sent via a secure server it can be intercepted by computer hackers." + System.Environment.NewLine;
        Body = Body + "5) Shopping cart : Software which allows you to accept product orders for multiple products from your website. This software automatically calculates and totals orders for your customers. Some setup must be done in the html code of your website, and the shopping cart software must be installed on the server which hosts your site or on the secure server which accepts sensitive ordering information." + System.Environment.NewLine + System.Environment.NewLine + "To understand how the money actually flows from acustomer to a merchant's bank account, read on.." + System.Environment.NewLine + System.Environment.NewLine + "HOW THE MONEY FLOWS" + System.Environment.NewLine + "The following example is based on Commerce Technologies's payment transaction solution. To learn more about e-Commerce, visit http://www.ecenow.com, or call (800) 477 5363." + System.Environment.NewLine;
        Body = Body + "1. Customer places order : A customer enters an order on a merchant's website. The transaction information is instantly encrypted and transmitted to the e-Commerce secure server, which is linked via a dedicated, leased data line to the credit card authorization network. A dedicated data line means faster transaction times." + System.Environment.NewLine;
        Body = Body + "2. e-Commerce verifies with bank : The transaction information is sent by the e-Commerce secure server via leased data line to the credit card authorization network where the validity of the card is checked and the availability of funds on that account is verified. An authorization code is returned via leased data line to the e-Commerce secure server. The authorization is encrypted by the e-Commerce server and transmitted in encrypted form back to the web server of the merchant which in turn triggers the fulfillment process of the order. " + System.Environment.NewLine;
        Body = Body + "3. Receipt is issued and merchant gets money : An e-mail receipt is sent to the customer and the merchant (either or both can be disabled at merchant's option). Within 48 - 72 hours the merchant receives the money! " + System.Environment.NewLine + System.Environment.NewLine + "INCREASE YOUR SALES FROM 30% TO 100%!" + System.Environment.NewLine;
        Body = Body + "-Studies have shown that accepting credit cards can increase your sales from 30% to 100%." + System.Environment.NewLine + "- Take advantage of customer impulse buying " + System.Environment.NewLine + "- Improve your competitive edge" + System.Environment.NewLine + "- Enhance your website's credibility with credit card acceptance." + System.Environment.NewLine + "- Make money while you sleep." + System.Environment.NewLine + "- Increase customer loyalty. " + System.Environment.NewLine + "- Make it convenient for your customers to buy. " + System.Environment.NewLine + "- Level the playing field - compete with the big guys." + System.Environment.NewLine + "- Expand your market - accept orders worldwide." + System.Environment.NewLine + "- Offer customers peace of mind with secure payment options." + System.Environment.NewLine + System.Environment.NewLine + "WHY CHOOSE Commerce Technologies?" + System.Environment.NewLine;
        Body = Body + "Commerce Technologies is one of the largest providers of credit card payment solutions for small businesses nationwide. Visit http://www.ecenow.com, or call (800) 477-5363." + System.Environment.NewLine + "- 12 years in merchant account industry" + System.Environment.NewLine + "- 1500+ new merchant accounts each month" + System.Environment.NewLine + "- Accept all forms of payment - credit cards, checks, ATM/Debit, etc." + System.Environment.NewLine + "- Get a COMPLETE e-commerce solution - not just software" + System.Environment.NewLine;
        Body = Body + "1) Merchant account" + System.Environment.NewLine + "2) Shopping cart (if needed)" + System.Environment.NewLine + "3) Secure server connection for encrypted internet transactions" + System.Environment.NewLine + "4) Powerful software to automatically process all electronic transactions quickly and easily" + System.Environment.NewLine + "5) Secure payment server connection is a dedicated leased data line - payment transactions are processed in 3-5 seconds." + System.Environment.NewLine + "- Merchant accounts provided for a wide variety of businesses -from non-traditional to high-risk: Internet, home-based,mail / phone order, startup, etc." + System.Environment.NewLine + "- 95% merchant application approval rate." + System.Environment.NewLine + "- 89% of all applicants are approved within 24 hours." + System.Environment.NewLine + "- Quick setup: 1-3 working days start to finish." + System.Environment.NewLine + System.Environment.NewLine;
        Body = Body + "REQUEST A FREE E-COMMERCE CONSULTATION OR APPLY FREE AND SAVE $195! THERE IS ABSOLUTELY NO OBLIGATION!" + System.Environment.NewLine + System.Environment.NewLine + "Thank you for requesting our Free E-commerce Report! If you apply now to accept credit cards on your website, we'll waive the $195 application fee. It's our way of saying, 'Thank you!'" + System.Environment.NewLine + "Visit http://www.ecenow.com, or call (800) 477-5363." + System.Environment.NewLine + System.Environment.NewLine + "Sincerely," + System.Environment.NewLine + System.Environment.NewLine + "The E-Commerce Team - http://www.ecenow.com" + System.Environment.NewLine + "Transaction Processing for the Web" + System.Environment.NewLine + "e-Commerce Affiliate Program - https://www.firstaffiliates.com 'Make money by promoting e-commerce through your existing website." + System.Environment.NewLine + System.Environment.NewLine + "P.S. This report is one of a series of letters which will be sent to you to help you understand the importance of accepting credit cards and checks on your website with e-Commerce. Use e-commerce to your advantage today!" + System.Environment.NewLine + System.Environment.NewLine;
        Body = Body + "==========You have submitted the following Information========" + System.Environment.NewLine;
        Body = Body + "Name : " + txtName.Text.Trim() + System.Environment.NewLine + "Email : " + txtEmail.Text.Trim() + System.Environment.NewLine + System.Environment.NewLine;
        Body = Body + "Sign up for a Merchant Account or a Payment Gateway with Commerce Technologies by clicking on the following link:" + System.Environment.NewLine;

        Body = Body + "https://www.firstaffiliates.com/onlineapplication/start.aspx?Referral=" + AffiliateID.ToString();

        return Body;
    }*/
    //end function GetBody

    /*public string ValidateData()
    {
        string strError = string.Empty;
        if (txtName.Text.Trim() == "")
            strError = "Please enter your name." + System.Environment.NewLine;
        if (txtEmail.Text.Trim() == "")
            strError += "Please enter your email address." + System.Environment.NewLine;

        if ((txtName.Text.Contains("--")) || (txtName.Text.Contains("#")) || (txtName.Text.Contains(";"))
            || (txtName.Text.Contains("'")) || (txtEmail.Text.Contains("--")) || (txtEmail.Text.Contains("#"))
            || (txtEmail.Text.Contains(";")) || (txtEmail.Text.Contains("'")) || (txtName.Text.ToLower().Contains("script"))
            || (txtEmail.Text.ToLower().Contains("script")))
            strError += "You cannot use hyphens, apostrophe, # or semi-colons in any of the following fields." + System.Environment.NewLine;
        ReportsBL AllLeads = new ReportsBL();
        bool retval = AllLeads.CheckEmailExists(txtEmail.Text.Trim());
        if (retval)
            //This email address already exists
            strError += "This Email Address already exists in our system. Please use a different email address.";

        return strError;
    }*/
    //end function ValidateDate

    public void SetMetaContent()
    {

        string pageDefault = ConfigurationManager.AppSettings["defaultPage"].ToString();
        XmlDocument doc1;
        //if (Cache["PageXml"] == null)
        //{
            doc1 = new XmlDocument();
            doc1.Load(Server.MapPath(@"App_Data\Page.xml"));
        //HttpContext.Current.Request.MapPath
            //doc1.Load(System.Web.HttpContext.Current.Server.MapPath(@"App_Data\Page.xml"));
        
            // xml document added to cache and set its cache dependency to file dependency
            // note: Keep Xml File in App_Data folder for Security reasons.
            Cache.Insert("PageXml", doc1, new System.Web.Caching.CacheDependency(Server.MapPath(@"App_Data\Page.xml")));
        //}
        /*
        else
        {
            doc1 = (XmlDocument)Cache["PageXml"];
        }*/

        XPathNavigator xpathNav = doc1.CreateNavigator();
        //Compile the XPath expression
        XPathExpression xpathExpr = xpathNav.Compile("/Pages/Page[PageName='" + Request.Path.ToString().ToLower().Replace("affiliatewiz/", "").Trim().ToLower().Replace("/", "") + "']");
        XPathNodeIterator nodeIter = xpathNav.Select(xpathExpr);

        // if page information not found in xml file use default page title, keyword and description
        if (nodeIter.Count == 0)
        {
            xpathExpr = xpathNav.Compile("/Pages/Page[PageName='" + pageDefault.Trim().ToLower() + "']");
            nodeIter = xpathNav.Select(xpathExpr);
        }
        while (nodeIter.MoveNext())
        {
            xpathNav = nodeIter.Current;
            if (xpathNav.MoveToFirstChild())
            {
                do
                {
                    switch (xpathNav.Name.ToUpper())
                    {
                        case "TITLE":
                            metaTitle.Text = HttpUtility.HtmlEncode(xpathNav.Value);
                            break;
                        case "DESCRIPTION":
                            metaDesc.Content = HttpUtility.HtmlEncode(xpathNav.Value);
                            break;
                        case "KEYWORDS":
                            metaKeyword.Content = HttpUtility.HtmlEncode(xpathNav.Value);
                            break;

                    }
                }
                while ((xpathNav.MoveToNext()));
            }
        }//end while
    }//end set meta content

    protected override void Render(System.Web.UI.HtmlTextWriter writer)
    {
        System.IO.StringWriter stringWriter = new System.IO.StringWriter();
        HtmlTextWriter htmlWriter = new HtmlTextWriter(stringWriter);
        base.Render(htmlWriter);
        string html = stringWriter.ToString();
        int StartPoint = html.IndexOf("<input type=\"hidden\" name=\"__VIEWSTATE\"");
        if (StartPoint >= 0) //does __viewstate exist?
        {
            int EndPoint = html.IndexOf("/>", StartPoint) + 2;
            string viewstateInput = html.Substring(StartPoint, EndPoint - StartPoint);
            html = html.Remove(StartPoint, EndPoint - StartPoint);
            int FormEndStart = html.IndexOf("</form>") - 1;
            if (FormEndStart >= 0)
                html = html.Insert(FormEndStart, viewstateInput);
        }
        writer.Write(html);
    }
}
