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

using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Text;
using System.IO;
using System.Net;
using System.Collections.Specialized;

using System.Xml;
using Newtonsoft.Json;

public partial class consult : System.Web.UI.Page
{

    private static string strReplace = "";
    private static string strMerchantGateway = "";
    private static int AppId = 0;
    private static int Referral;
    private static int PID = 0;
    private static int AcctType = 0;
    //private static string Country = "";
    private static string fn = string.Empty;
    private static string ln = "";
    private static string em = "";
    private static string pn = "";
    private static string name = "";
    private static string strAffiliateID = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            
            if ((Request.Params.Get("fn") != null))
            {
                fn = Request.Params.Get("fn");
            }

            if ((Request.Params.Get("ln") != null))
            {
                ln = Request.Params.Get("ln");
            }

            name = fn + " " + ln;
            if ((Request.Params.Get("em") != null))
            {
                em = Request.Params.Get("em");
            }

            if ((Request.Params.Get("pn") != null))
            {
                pn = Request.Params.Get("pn");
            }

            ReportsBL Report = new ReportsBL();

            Report.InsertFreeReport("", name, em);


            //Send Email                    
            //SendEmailToUser();
            //SendEmailToAdmin();
            //SendEmailToPartner();
            //Response.Redirect("https://commercetech.com/thank-you/");
        }
    }
    

}
