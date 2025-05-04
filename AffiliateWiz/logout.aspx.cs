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
            HttpCookie ck = Response.Cookies.Get("firstaffiliates");
            if (ck != null )
            {                
                ck.Domain = ".firstaffiliates.com";
                ck.Expires = DateTime.Now.AddYears(-3);
                Response.Cookies.Add(ck);
            }
            Session.RemoveAll();
            Session.Abandon();
            if ((Request.Params.Get("Authorization") != null))
            {
                if (Request.Params.Get("Authorization") == "False")
                    Response.Redirect("login.aspx?Authorization=False");
            }
            Response.Redirect("login.aspx?Logout=True");            
        }
    }
}
