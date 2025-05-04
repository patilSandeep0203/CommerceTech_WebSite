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

public partial class locked : System.Web.UI.Page
{
    private static int AppId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        AppId = Convert.ToInt32(Session["AppId"]);
        if (Session.IsNewSession)
            Response.Redirect("/OnlineApplication/default.aspx?SessionExpired=True");

        if ( !IsPostBack)
        {
            if (!User.Identity.IsAuthenticated)
                Response.Redirect("default.aspx?Authentication=False");
            else
            {
                try
                {
                    OnlineAppStatus Status = new OnlineAppStatus(AppId);
                    int AcctType = Convert.ToInt32(Session["AcctType"]);
                    if (AcctType == 1)
                    {
                        string strStatus = Status.GetStatus( "Merchant");
                        lblStatus.Text = strStatus;
                        lblStatusGW.Visible = false;
                        lblStatusGWHeader.Visible = false;
                    }
                    else if (AcctType == 2)
                    {
                        string strStatusGW = Status.GetStatus( "Gateway");
                        lblStatusGW.Text = strStatusGW;
                        lblStatus.Visible = false;
                        lblStatusHeader.Visible = false;
                    }
                    else if (AcctType == 4)
                    {
                        string strStatus = Status.GetStatus("Merchant");
                        string strStatusGW = Status.GetStatus( "Gateway");
                        lblStatus.Text = strStatus.Trim();
                        lblStatusGW.Text = strStatusGW.Trim();
                    }
                }//end try
                catch (Exception err)
                {
                    CreateOnlineAppLog Log = new CreateOnlineAppLog();
                    Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + Session["AppId"].ToString() + " - " + "Locked Page Load Error - " + err.Message);                    
                }
            }//end identity authenticated
        }//end if postback
    }
}
