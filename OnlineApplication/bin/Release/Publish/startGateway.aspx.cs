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

public partial class QuestionsGateway : System.Web.UI.Page
{
    private static string strBusiness = "";
    private static string parameters = "";
    private static string Serv = "";
    private static string SW = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Style errLabel = new Style();
        errLabel.BackColor = System.Drawing.Color.FromArgb(255, 192, 192);
        errLabel.ForeColor = System.Drawing.Color.Black;
        errLabel.BorderColor = System.Drawing.Color.Red;
        errLabel.BorderStyle = BorderStyle.Solid;
        errLabel.BorderWidth = Unit.Pixel(1);
        errLabel.Font.Size = FontUnit.Small;
        lblError.ApplyStyle(errLabel);

        parameters = HttpContext.Current.Request.Url.ToString();
        parameters = parameters.Substring(parameters.IndexOf("?") + 1);

        if (!IsPostBack)
        {
            strBusiness = "";
        }

        if ((Request.Params.Get("Serv") != null))
        {
            Serv = Request.Params.Get("Serv");
        }
        if ((Request.Params.Get("SW") != null))
        {
            SW = Request.Params.Get("SW");
        }
    }

    //This function handles the submit button click event
    protected void btnContinue_Click(object sender, EventArgs e)
    {
        string strServ = "";

        if ((strBusiness == "") || (!rdbPG1a.Checked && !rdbPG1b.Checked && !rdbPG1c.Checked && !rdbPG1d.Checked))
        {
            SetErrorMessage("Please select one of the options listed below.");
        }
        else
        {
            if (strBusiness == "Authnet")
                strServ = Serv.Replace("Gateway/", "AuthnetGateway/");
            else if (strBusiness == "AuthnetVPOS")
                strServ = Serv.Replace("Gateway/", "AuthnetVPOSGateway/");
            else if (strBusiness == "ROAMpay")
                strServ = Serv.Replace("Gateway/", "Authnet/");
            else if (strBusiness == "PnP")
                strServ = Serv.Replace("Gateway/", "PnPGateway/");
            else if (strBusiness == "SageGateway")
                strServ = Serv.Replace("Gateway/", "SageGateway/");

            if (Serv.Contains("MerchantFunding"))
                Response.Redirect("startMCA.aspx?SW=" + SW + "&Serv=" + strServ);
            else
                Response.Redirect("register.aspx?SW=" + SW + "&Serv=" + strServ);
            
        }
    }//end function submit button clicked

    protected void btnSkip_Click(object sender, EventArgs e)
    {
        string strServ = Serv.Replace("Gateway/", "");
        if (strServ != "")
            Response.Redirect("register.aspx?SW=" + SW + "&Serv=" + strServ);
        else
            Response.Redirect("start1.aspx");
    }

    protected void btnStart_Click(object sender, EventArgs e)
    {
        Response.Redirect("Start.aspx");
    }

    //This function handles the radio button check changed event
    protected void rdbPG_CheckedChanged(object sender, EventArgs e)
    {
        if (SW.Contains("SageYes"))
        {
            if (sender.Equals(rdbPG1a) || sender.Equals(rdbPG1d) || sender.Equals(rdbPG1c)) // E-Commerce, POS or BackOffice
            {
                strBusiness = "Authnet"; //Sage gateway
                pnlPG2.Visible = false;
                rdbPG2Y.Checked = false;
                rdbPG2N.Checked = false;
            }
            if (sender.Equals(rdbPG1b)) //Wireless
            {
                strBusiness = "Authnet";
                pnlPG2.Visible = false;
                rdbPG2Y.Checked = false;
                rdbPG2N.Checked = false;
            }
        }
        else
        {
            if (sender.Equals(rdbPG1a) || sender.Equals(rdbPG1d)) // E-Commerce, POS or BackOffice
            {
                strBusiness = "Authnet"; //Authnet gateway
                pnlPG2.Visible = false;
                rdbPG2Y.Checked = false;
                rdbPG2N.Checked = false;
            }
            if (sender.Equals(rdbPG1b)) //Wireless
            {
                strBusiness = "Authnet";
                pnlPG2.Visible = false;
                rdbPG2Y.Checked = false;
                rdbPG2N.Checked = false;
            }
            if (sender.Equals(rdbPG1c))
            {
                strBusiness = "Authnet"; //Authnet gateway
                pnlPG2.Visible = false;
                rdbPG2Y.Checked = false;
                rdbPG2N.Checked = false;
            }
            if (sender.Equals(rdbPG2N))
            {
                if (rdbPG1c.Checked)
                    strBusiness = "AuthnetVPOS";
                else
                    strBusiness = "Authnet";
            }
            if (sender.Equals(rdbPG2Y))
            {
                strBusiness = "PnP";
            }
        }
    }//end function radio button check changed

    //This function displays error messages
    protected void SetErrorMessage(string errText)
    {
        lblError.Visible = true;
        lblError.Text = errText;
    }//end function set error message
}
