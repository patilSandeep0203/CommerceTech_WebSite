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

public partial class Questions : System.Web.UI.Page
{
    //bool chkMFChanged = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        Style errLabel = new Style();
        errLabel.BackColor = System.Drawing.Color.FromArgb(255, 192, 192);
        errLabel.ForeColor = System.Drawing.Color.Black;
        errLabel.BorderColor = System.Drawing.Color.Red;
        errLabel.BorderStyle = BorderStyle.Solid;
        errLabel.BorderWidth = Unit.Pixel(1);
        errLabel.Font.Size = FontUnit.Small;
        errLabel.Font.Name = "Arial";
        lblError.ApplyStyle(errLabel);

        chkOD.Enabled = false;
        chkEBT.Enabled = false;
        chkWireless.Enabled = false;

    }

    protected void chkCS_CheckChanged(object sender, EventArgs e)
    {
        if (chkCS.Checked)
        {
            chkGateway.Enabled = true;
            chkGateway.Checked = true;
        }
        else
        {
            chkGateway.Checked = true;
        }
    }

    protected void chkMA_CheckChanged(object sender, EventArgs e)
    {
        if (chkMA.Checked)
        {
            chkOD.Enabled = true;
            chkEBT.Enabled = true;
            chkWireless.Enabled = true;
        }
        else
        {
            chkOD.Checked = false;
            chkOD.Enabled = false;
            chkEBT.Checked = false;
            chkEBT.Enabled = false;
            chkWireless.Checked = false;
            chkWireless.Enabled = false;
        }
    }

    protected void chkMF_CheckChanged(object sender, EventArgs e)
    {
        //if (chkMA.Checked)
        //{

        string strchkMFChanged = Convert.ToString(Session["chkMFChanged"]);
            if (chkMF.Checked)
            {
                if (chkMA.Checked)
                {
                    chkMA.Enabled = false;
                    chkMA.Checked = true;
                    chkOD.Enabled = true;
                    chkEBT.Enabled = true;
                    chkWireless.Enabled = true;
                }
                else
                {
                    chkMA.Enabled = false;
                    chkMA.Checked = true;
                    chkOD.Enabled = true;
                    chkEBT.Enabled = true;
                    chkWireless.Enabled = true;
                    //chkMFChanged = true;
                    Session["chkMFChanged"] = 1;
                }
            }
            
            else
            {
                if ((chkMA.Checked) && (Convert.ToString(Session["chkMFChanged"]) == "0"))
                {
                    chkMA.Enabled = true;
                    chkMA.Checked = true;
                    chkOD.Enabled = true;
                    chkEBT.Enabled = true;
                    chkWireless.Enabled = true;
                }
                else if ((chkMA.Checked) && (Convert.ToString(Session["chkMFChanged"]) == "1"))
                {
                    chkMA.Enabled = true;
                    chkMA.Checked = false;
                    chkOD.Enabled = false;
                    chkEBT.Enabled = false;
                    chkWireless.Enabled = false;
                    chkOD.Checked = false;
                    chkEBT.Checked = false;
                    chkWireless.Checked = false;
                }
                else if (!(chkMA.Checked))
                {
                    chkMA.Enabled = true;
                    chkMA.Checked = false;
                    chkOD.Enabled = true;
                    chkEBT.Enabled = true;
                    chkWireless.Enabled = true;
                }
            }
       // }
        /*
        else
        {
            if (chkMF.Checked)
            {
                chkMA.Enabled = false;
                chkMA.Checked = true;
                chkOD.Enabled = true;
                chkEBT.Enabled = true;
                chkWireless.Enabled = true;
            }
            else
            {
                chkMA.Enabled = true;
                chkMA.Checked = false;
                chkOD.Enabled = true;
                chkEBT.Enabled = true;
                chkWireless.Enabled = true;
            }
        }*/
       
    }

    protected void chkGateway_CheckChanged(object sender, EventArgs e)
    {
        if (chkGateway.Checked)
        {
            chkOD.Checked = false;
            chkOD.Enabled = false;
            chkEBT.Checked = false;
            chkEBT.Enabled = false;
            chkWireless.Enabled = true;
        }
        else
        {
            if (chkMA.Checked)
            {
                chkOD.Enabled = true;
                chkEBT.Enabled = true;
                chkWireless.Enabled = true;
            }
        }
    }

    protected void btnContinue_Click(object sender, EventArgs e)
    {
        string Serv = "";
        if (chkMA.Checked)
            Serv += "MerchantAccount/";
        if (chkOD.Checked)
            Serv += "OnlineDebit/";
        if (chkEBT.Checked)
            Serv += "EBT/";        
        //if (chkWireless.Checked)
        //    Serv += "Wireless/";
        if (chkGateway.Checked)
            Serv += "Gateway/";
        if (chkCS.Checked)
        {
            Serv += "Gateway/";
            Serv += "CheckServices/";
        }
        if (chkGC.Checked)
            Serv += "GiftCard/";
        if (chkMF.Checked)
            Serv += "MerchantFunding/";
        if (chkPayroll.Checked)
            Serv += "Payroll/";
        //if (chkLease.Checked)
        //    Serv += "Lease/";

        if (chkMA.Checked)
            Response.Redirect("start3.aspx?Serv=" + Serv); //domestic Merchant App Questions
        else if (chkGateway.Checked)
            Response.Redirect("startGateway.aspx?Serv=" + Serv); //Gateway App Questions
        else if (chkMF.Checked)
            Response.Redirect("startMCA.aspx?Serv=" + Serv); //Merchant Cash Advance Questions
        else if ((!chkMA.Checked) && (!chkOD.Checked) && (!chkEBT.Checked) && (!chkGateway.Checked) &&
                (!chkCS.Checked) && (!chkGC.Checked) && (!chkMF.Checked) && (!chkPayroll.Checked))
            SetErrorMessage("Please select a service below to Continue");
        else
            Response.Redirect("register.aspx?Serv=" + Serv);
    }
    
    protected void SetErrorMessage(string errText)
    {
        lblError.Visible = true;
        lblError.Text = errText;
    }//end function set error message

    protected void btnStart_Click(object sender, EventArgs e)
    {
        Response.Redirect("Start.aspx");
    }

}
