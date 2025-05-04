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

public partial class agent : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ProcessRequest();
            }//end if not post back
        }
        catch (Exception err)
        {
            lblError.Visible = true;
            lblError.Text = err.Message;
        }
    }

    public void ProcessRequest()
    {
        int iAffiliateID = 0;
        bool retVal = false;
        
        #region Retrieve AffiliateID
        //Retrieve AffiliateID	    
        if (Request.Params.Get("AffiliateID") != null)
        {
            if (!Int32.TryParse(Request.Params.Get("AffiliateID").ToString(), out iAffiliateID))
                Response.Redirect("defaulterror.htm");
        }
        else if (Request.Params.Get("A") != null)
        {
            retVal = Int32.TryParse(Request.Params.Get("A").ToString(), out iAffiliateID);
            if (!retVal)
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
            }
            else
                SetMessage("This Request is Invalid. Please check your URL and try again.");
        }//end task is click or get        
        #endregion
    }
    //This function create new cookie
    protected void CreateCookie(int iAffiliateID)
    {
        FormsAuthenticationTicket AuthTicket;
        string strcookie;
        HttpCookie ck = new HttpCookie("AffiliateWizAffiliateID");
        ck.Values.Add("AffiliateID", iAffiliateID.ToString());
        ck.Expires = DateTime.Now.AddMinutes(60);

        ck.Secure = true;

        Response.Cookies.Add(ck);
    }//end function create cookie

    public void SetMessage(string Text)
    {
        lblError.Visible = true;
        lblError.Text += Text + System.Environment.NewLine;
    }
}
