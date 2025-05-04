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

public partial class aw : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                ProcessRequest();
            }//end try
            catch (Exception err)
            {
                lblError.Visible = true;
                lblError.Text = err.Message;
            }
        }//end if not post back
    }

    public void ProcessRequest()
    {
        int iAffiliateID = 0;
        int iGroupID = 0;
        int iBannerID = 0;
        bool retVal = false;

        if ((Request.Params.Get("Task")=="Get") && (Request["Browser"]=="N" ))
        {
		    if (Request.ServerVariables["HTTP_USER_AGENT"].ToUpper().Contains("MSIE"))
            {
			    Response.Buffer=true;
			    Response.ContentType="application/x-javascript";
			    Response.Write ("document.write(' '); ");
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
            if (Request.Params.Get("A") != null)
                retVal = Int32.TryParse(Request.Params.Get("A").ToString(), out iAffiliateID);
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
                Response.Redirect("default.aspx");
            }
            else
                SetMessage("This Request is Invalid. Please check your URL and try again.");
        }//end task is click or get        
        #endregion        
    }//end function ProcessRequest

    //This function create new cookie
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
        //HttpCookie ck = new HttpCookie("AffiliateWizAffiliateID");
        HttpCookie ck = new HttpCookie("getAffiliateWizAffiliateID");
        ck.Values.Add("AffiliateID", iAffiliateID.ToString());
        ck.Expires = DateTime.Now.AddMinutes(60);
        //ck.Path = "/Affiliatewiz";
        //ck.Domain = "firstaffiliates.com";
        ck.Secure = true;
        
        Response.Cookies.Add(ck);
        //Response.Cookies["domain"].Domain = "firstaffiliates.com";
    }//end function create cookie

    public void SetMessage(string Text)
    {
        lblError.Visible = true;
        lblError.Text += Text + System.Environment.NewLine;
    }
}
