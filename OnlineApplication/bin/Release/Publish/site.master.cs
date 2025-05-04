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
using System.Xml;
using System.Xml.XPath;
using BLAffiliateWiz;

public partial class site : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SetMetaContent();
        if (!IsPostBack)
        {
            int AffiliateID = 0;

            
            if (Request.Params.Get("Referral") != null)
            {
                ProcessRequest();
            }

            /*if (Request.Params.Get("Referral") != null)
            {
                if (Int32.TryParse(Request.Params.Get("Referral").ToString(), out ReferralID))
                    //ReferralID = Convert.ToInt32(Request.Params.Get("Referral"));
                    Session["Referral"] = ReferralID;
                else
                    Response.Redirect("DefaultError.aspx");
            }*/

            
            HttpCookie ck = Request.Cookies["AffiliateWizAffiliateID"];
            if (ck != null)
                AffiliateID = Convert.ToInt32(ck["AffiliateID"].ToString().Trim());

            if ((AffiliateID != 0) && (AffiliateID != null))
            {
                pnlPageHeading.Visible = true;
                lblPartnerType.CssClass = "BannerText";
                lblPartnerName.CssClass = "BannerText";

                //Get Affiliate Name from Affiliate ID
                AffiliatesWizBL Affiliate = new AffiliatesWizBL();
                DataSet ds = Affiliate.GetAffiliateInfoBasic(AffiliateID);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];

                    lnkHomePage.NavigateUrl = "https://commercetech.com/" + AffiliateID.ToString();
                    lnkHomePage.Target = "_blank";

                    if (!Convert.IsDBNull(dr["DisplayLogo"]))
                    {
                        if (Convert.ToBoolean(dr["DisplayLogo"]) == true)
                        {
                            //string strUrlLogo = "Partner/PartnerLogo/" + Convert.ToString(AffiliateID) + "/" + Convert.ToString(dr["LogoFileName"]);
                            string strUrlLogo = "../PartnerPortal/PartnerLogo/" + Convert.ToString(AffiliateID) + "/" + Convert.ToString(dr["LogoFileName"]);
                            if (!Convert.IsDBNull(dr["LogoFileName"]))
                            {
                                if (Convert.ToString(dr["LogoFileName"]) != "")
                                {
                                    lblPartnerName.Visible = false;
                                    imgPartnerLogo.Attributes["src"] = ResolveUrl(strUrlLogo);
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
            }
            else
            { 
                lnkHomePage.NavigateUrl = "https://commercetech.com/";
                    lnkHomePage.Target = "_blank";
            }


            string strURL = Request.Url.ToString();
            if ((strURL.Contains("'")) || (strURL.Contains("--")) || (strURL.Contains("#")) || (strURL.Contains(";")) || (strURL.Contains("exec")) || (strURL.Contains("EXEC")) || (strURL.Contains("<")) || (strURL.Contains(">")) || (strURL.ToLower().Contains("script")))
            {
                Response.Redirect("/Application/DefaultError.aspx");
            }

            if (strURL.ToLower().Contains("register.aspx"))
            {
                pnlTopBar.Visible = false;
            }

            Style headerLabel = new Style();
            headerLabel.ForeColor = System.Drawing.Color.White;
            headerLabel.Font.Size = 9;

            lblHeader.ApplyStyle(headerLabel);
            if (strURL.ToLower().Contains("start2.aspx"))
                lblHeader.Text = "International Merchant Account Application";
            else if (strURL.ToLower().Contains("start3.aspx"))
                lblHeader.Text = "U. S. Merchant Account Application";
            else if (strURL.ToLower().Contains("startmca.aspx"))
                lblHeader.Text = "Merchant Cash Advance Application";
            else
                lblHeader.Text = "";
        }
    }

    public void ProcessRequest()
    {
        int iAffiliateID = 0;
        int iGroupID = 0;
        int iBannerID = 0;
        bool retVal = false;

        if ((Request.Params.Get("Task") == "Get") && (Request["Browser"] == "N"))
        {
            if (Request.ServerVariables["HTTP_USER_AGENT"].ToUpper().Contains("MSIE"))
            {
                Response.Buffer = true;
                Response.ContentType = "application/x-javascript";
                Response.Write("document.write(' '); ");
            }
        }

        #region Retrieve AffiliateID
        //Retrieve AffiliateID	    
        if (Request.Params.Get("AffiliateID") != null)
        {
            if (!Int32.TryParse(Request.Params.Get("AffiliateID").ToString(), out iAffiliateID))
                Response.Redirect("defaulterror.htm");
        }
        else
        {
            if (Request.Params.Get("Referral") != null)
                retVal = Int32.TryParse(Request.Params.Get("Referral").ToString(), out iAffiliateID);
            if (!retVal)
                Response.Redirect("defaulterror.htm");
        }
        #endregion

        #region Retrieve GroupID
        //Retrieve GroupID if this is a banner group
        if (Request.Params.Get("GroupID") != null)
        {
            if (!Int32.TryParse(Request.Params.Get("GroupID").ToString(), out iGroupID))
                Response.Redirect("defaulterror.htm");
        }
        else
        {
            if (Request.Params.Get("G") != null)
                if (!Int32.TryParse(Request.Params.Get("G").ToString(), out iGroupID))
                    Response.Redirect("defaulterror.htm");
        }
        #endregion

        #region Retrieve BannerID
        //Retrieve BannerID if this is a banner group
        if (Request.Params.Get("BannerID") != null)
        {
            if (!Int32.TryParse(Request.Params.Get("BannerID").ToString(), out iBannerID))
                Response.Redirect("defaulterror.htm");
        }
        else
        {
            if (Request.Params.Get("B") != null)
                if (!Int32.TryParse(Request.Params.Get("B").ToString(), out iBannerID))
                    Response.Redirect("defaulterror.htm");
        }
        #endregion

        #region Task Is Get Or Click
        if (iAffiliateID != 0)
        {
            //Check if affiliate exists
            AffiliatesWizBL Check = new AffiliatesWizBL();
            bool bExists = Check.CheckAffiliateExists(iAffiliateID);
            if (bExists)
            {
                CreateCookie(iAffiliateID);
                //Response.Redirect("start.aspx");
            }
            //else
                //SetMessage("This Request is Invalid. Please check your URL and try again.");
        }//end task is click or get        
        #endregion
    }//end function ProcessRequest

    public void SetMetaContent()
    {

        string pageDefault = ConfigurationManager.AppSettings["defaultPage"].ToString();
        XmlDocument doc1;
        if (Cache["PageXml"] == null)
        {
            doc1 = new XmlDocument();
            doc1.Load(Server.MapPath(@"App_Data\Page.xml"));
            // xml document added to cache and set its cache dependency to file dependency
            // note: Keep Xml File in App_Data folder for Security reasons.
            Cache.Insert("PageXml", doc1, new System.Web.Caching.CacheDependency(Server.MapPath(@"App_Data\Page.xml")));
        }
        else
        {
            doc1 = (XmlDocument)Cache["PageXml"];
        }

        XPathNavigator xpathNav = doc1.CreateNavigator();
        //Compile the XPath expression
        XPathExpression xpathExpr = xpathNav.Compile("/Pages/Page[PageName='" + Request.Path.ToString().ToLower().Replace("onlineapplication/", "").Trim().ToLower().Replace("/", "") + "']");
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
        }
    }

    protected void CreateCookie(int iAffiliateID)
    {
        FormsAuthenticationTicket AuthTicket;
        string strcookie;
        //HttpCookie ck;
        //AuthTicket = new FormsAuthenticationTicket(1, iAffiliateID.ToString(), DateTime.Now, DateTime.Now.AddMinutes(60), false, "Session in progress");
        //strcookie = FormsAuthentication.Encrypt(AuthTicket);
        //ck = new HttpCookie(FormsAuthentication.FormsCookieName, strcookie);
        //ck = new HttpCookie("AffiliateWizAffiliateID", strcookie);
        //ck.Expires = AuthTicket.Expiration;
        //ck.Path = FormsAuthentication.FormsCookiePath;


        HttpCookie ck = new HttpCookie("AffiliateWizAffiliateID");
        ck.Values.Add("AffiliateID", iAffiliateID.ToString());
        ck.Expires = DateTime.Now.AddMinutes(60);
        //ck.Path = "/Affiliatewiz";
        //ck.Domain = "apps.commercetech.com";
        ck.Secure = true;

        Response.Cookies.Add(ck);
        //Response.Cookies["domain"].Domain = "apps.commercetech.com";
    }//end function create cookie

    /*public void SetMessage(string Text)
    {
        lblError.Visible = true;
        lblError.Text += Text + System.Environment.NewLine;
    }*/
}
