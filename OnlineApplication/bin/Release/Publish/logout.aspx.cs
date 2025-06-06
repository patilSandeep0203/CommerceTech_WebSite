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

public partial class logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FormsAuthentication.SignOut();
            Session["AppId"] = 0;
            Session["UserLoggedIn"] = "No";
            Session.RemoveAll();
            Session.Abandon();
            Response.Redirect("default.aspx");
        }
    }
}
