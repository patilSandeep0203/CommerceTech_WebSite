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

public partial class contactus : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session.Keys.Count != 0)
            Page.MasterPageFile = "~/Main.master";
        else
            Page.MasterPageFile = "~/site.master";
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}
