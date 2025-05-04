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
using System.Text;

public partial class QuickBooks : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    // Do not edit below this line
    // ---------------------------------------------------------------------------
    public void SVShowContent()
    {
        try
        {
            string strURLToGet = "http://intuit.sharedvue.net/sharedvue/pull/";
            string strQuerystring = Request.QueryString.ToString();
            string strReturn = "";
            string strSVHost = "";

            if (strQuerystring.Length > 0) { strSVHost += System.Web.HttpUtility.UrlEncode("?" + strQuerystring); }

            strURLToGet += "?svhost=" + "ecenow.com" + Request.ServerVariables["script_name"] + strSVHost;

            System.Net.WebRequest reqSVRemote = System.Net.WebRequest.Create(new Uri(strURLToGet));
            System.Net.WebResponse resSVRemote = reqSVRemote.GetResponse();
            System.IO.Stream strmSVContent = resSVRemote.GetResponseStream();
            System.IO.StreamReader srSVContent = new System.IO.StreamReader(strmSVContent, Encoding.ASCII);
            strReturn = srSVContent.ReadToEnd().Trim();

            strmSVContent.Close();
            resSVRemote.Close();

            Response.Write(strReturn);
        }
        catch (Exception ex)
        {
            Response.Write("<!-- SharedVue Output: " + ex.Message + " -->");
        }
        return;
    }
    // ---------------------------------------------------------------------------
}
