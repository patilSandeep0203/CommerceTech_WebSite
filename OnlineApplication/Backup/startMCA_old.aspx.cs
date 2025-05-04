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

public partial class QuestionsMCA : System.Web.UI.Page
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
        if ((strBusiness == "") || (!rdbMCA1N.Checked && !rdbMCA1Y.Checked))
        {
            SetErrorMessage("Please select one of the options listed below.");
        }
        else
        {
            if (strBusiness == "LowVolume")
            {
                SetErrorMessage("We cannot process your Merchant Cash Advance application since your average credit card volume is less than $5000. Please click the Skip button to continue without applying for Merchant Cash Advance.");
            }
            else if (strBusiness == "Unacceptable")
            {
                SetErrorMessage("We cannot process your Merchant Cash Advance application if your business is one of the Unacceptable Businesses. Please click the Skip button to continue without applying for Merchant Cash Advance.");
            }
            else if (strBusiness == "AMI")
            {
                string strServ = Serv.Replace("MerchantFunding/", "AMI/");
                Response.Redirect("register.aspx?SW=" + SW + "&Serv=" + strServ);
            }
            else if (strBusiness == "BFS")
            {
                string strServ = Serv.Replace("MerchantFunding/", "BFS/");
                Response.Redirect("register.aspx?SW=" + SW + "&Serv=" + strServ);
            }
        }
    }//end function submit button clicked


    protected void btnSkip_Click(object sender, EventArgs e)
    {
        string strServ = Serv.Replace("MerchantFunding/", "");
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
    protected void rdbMCA_CheckedChanged(object sender, EventArgs e)
    {
        if (sender.Equals(rdbMCA1N))
        {
            strBusiness = "LowVolume";
            pnlMCA2.Visible = false;
            rdbMCA2Y.Checked = false;
            rdbMCA2N.Checked = false;
            pnlMCA3.Visible = false;
            rdbMCA3Y.Checked = false;
            rdbMCA3N.Checked = false;
            pnlMCA4.Visible = false;
            rdbMCA4Y.Checked = false;
            rdbMCA4N.Checked = false;
        }
        if (sender.Equals(rdbMCA1Y))
        {
            strBusiness = "";
            pnlMCA2.Visible = true;
            rdbMCA2Y.Checked = false;
            rdbMCA2N.Checked = false;
            pnlMCA3.Visible = false;
            rdbMCA3Y.Checked = false;
            rdbMCA3N.Checked = false;
            pnlMCA4.Visible = false;
            rdbMCA4Y.Checked = false;
            rdbMCA4N.Checked = false;
        }
        if (sender.Equals(rdbMCA2Y))
        {
            strBusiness = "";
            pnlMCA3.Visible = true;
            rdbMCA3Y.Checked = false;
            rdbMCA3N.Checked = false;
            pnlMCA4.Visible = false;
            rdbMCA4Y.Checked = false;
            rdbMCA4N.Checked = false;
        }
        if (sender.Equals(rdbMCA2N))
        {
            strBusiness = "";
            pnlMCA3.Visible = false;
            rdbMCA3Y.Checked = false;
            rdbMCA3N.Checked = false;
            pnlMCA4.Visible = true;
            rdbMCA4Y.Checked = false;
            rdbMCA4N.Checked = false;
        }
        if (sender.Equals(rdbMCA3N))
        {
            strBusiness = "AMI";
            pnlMCA4.Visible = false;
            rdbMCA4Y.Checked = false;
            rdbMCA4N.Checked = false;
        }
        if (sender.Equals(rdbMCA3Y))
        {
            strBusiness = "";
            pnlMCA4.Visible = true;
            rdbMCA4Y.Checked = false;
            rdbMCA4N.Checked = false;
        }

        if (sender.Equals(rdbMCA4N))
        {
            strBusiness = "BFS";
        }

        if (sender.Equals(rdbMCA4Y))
        {
            strBusiness = "Unacceptable";
        }
    }//end function radio button check changed

    //This function displays error messages
    protected void SetErrorMessage(string errText)
    {
        lblError.Visible = true;
        lblError.Text = errText;
    }//end function set error message
}
