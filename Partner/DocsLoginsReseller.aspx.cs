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

public partial class DocsLoginsReseller : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (!Session.IsNewSession)
        {
            if (Session.Keys.Count == 0)
                Response.Redirect("../logout.aspx");
            if (User.IsInRole("Reseller"))
                Page.MasterPageFile = "~/AgentMisc.master";
            else if (User.IsInRole("T1Reseller"))
                Page.MasterPageFile = "~/T1Agent.master";
        }
    }

    void Page_Init(object sender, EventArgs e)
    {
        ViewStateUserKey = Session.SessionID;
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session.IsNewSession)
            Response.Redirect("logout.aspx?Authentication=False");

        if (!User.IsInRole("Reseller") && !User.IsInRole("T1Reseller"))
            Response.Redirect("logout.aspx?Authentication=False");

        if (!IsPostBack)
        {
            if (!User.Identity.IsAuthenticated)
                Response.Redirect("logout.aspx?Authentication=False");
        }
    }
}
