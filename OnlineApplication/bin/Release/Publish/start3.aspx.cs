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

public partial class Questions3 : System.Web.UI.Page
{
    private static string strSW = "";
    //private static string strPOS = "";
    private static string strBusiness = "";
    private static string parameters = "";
    private static string Serv = "";
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
            //strPOS = "";
            strSW = "";
        }
        if ((Request.Params.Get("Serv") != null))
        {
            Serv = Request.Params.Get("Serv");
        }
    }

    //This function handles the submit button click event
    protected void btnContinue_Click(object sender, EventArgs e)
    {
        if ((strBusiness == "") || (!rbtnUnaccBusN.Checked && !rbtnUnaccBusY.Checked))
        {
            SetErrorMessage("Please select one of the options listed below.");
        }
        else
        {
            if (strBusiness == "Unacceptable")
                SetErrorMessage("We cannot process your application if your business is one of the Unacceptable Businesses.");
            else if (strBusiness == "International")
            {
                Response.Redirect("start2.aspx");
            }
            else
            {
                if (strSW == "")
                    SetErrorMessage("Please select one of the options listed below.");
                else
                {
                    if (Serv.Contains("Gateway") && ((strSW != "QB") && (strSW != "GOPAY") && (strSW != "QBPOS") && (strSW != "POS")))
                    {
                        Response.Redirect("startGateway.aspx?SW=" + strSW + "&Serv=" + Serv);
                    }
                    else if (Serv.Contains("Gateway") && ((strSW != "QB") || (strSW != "GOPAY") || (strSW != "QBPOS") || (strSW != "POS")))
                    {
                        Serv = Serv.Replace("Gateway/","");
                        Response.Redirect("register.aspx?SW=" + strSW + "&Serv=" + Serv);
                        //Response.Redirect("startGateway.aspx?SW=" + strSW + "&Serv=" + Serv);
                    }
                    else if (Serv.Contains("MerchantFunding"))
                    {
                        Response.Redirect("startMCA.aspx?SW=" + strSW + "&Serv=" + Serv);
                    }
                    else
                    {
                        Response.Redirect("register.aspx?SW=" + strSW + "&Serv=" + Serv);
                    }
                }
            }
        }        
    }//end function submit button clicked

    protected void btnStart_Click(object sender, EventArgs e)
    {
        Response.Redirect("Start.aspx");
    }

    //This function handles the radio button check changed event
    protected void rdbPOS_CheckedChanged(object sender, EventArgs e)
    {        
        if (sender.Equals(rbtnUnaccBusN))
        {
            strBusiness = "";
            pnl50Pct.Visible = true;
            rbtn50PctY.Checked = false;
            rbtn50PctN.Checked = false;
            /*pnlFEIN.Visible = false;
            rbtnFEINY.Checked = false;
            rbtnFEINN.Checked = false;*/
            pnlOtherCurrencies.Visible = false;
            rbtnOtherCurrenciesY.Checked = false;
            rbtnOtherCurrenciesN.Checked = false;
            pnlSage.Visible = false;
            rdbSageY.Checked = false;
            rdbSageN.Checked = false;
            pnlQB1.Visible = false;
            rdbQBYes.Checked = false;
            rdbQBNo.Checked = false;
            pnlQB2.Visible = false;
            rdbQB.Checked = false;
            rdbGOPAY.Checked = false;
            rdbQBPOS.Checked = false;
            rdbPOS.Checked = false;
            pnlQB3.Visible = false;
            rdbINT.Checked = false;
            rdbGP.Checked = false;
        }
        if (sender.Equals(rbtnUnaccBusY))
        {
            strBusiness = "Unacceptable";
            pnl50Pct.Visible = false;
            rbtn50PctY.Checked = false;
            rbtn50PctN.Checked = false;
            /*pnlFEIN.Visible = false;
            rbtnFEINY.Checked = false;
            rbtnFEINN.Checked = false;*/
            pnlOtherCurrencies.Visible = false;
            rbtnOtherCurrenciesY.Checked = false;
            rbtnOtherCurrenciesN.Checked = false;
            pnlSage.Visible = false;
            rdbSageY.Checked = false;
            rdbSageN.Checked = false;
            pnlQB1.Visible = false;
            rdbQBYes.Checked = false;
            rdbQBNo.Checked = false;
            pnlQB2.Visible = false;
            rdbQB.Checked = false;
            rdbGOPAY.Checked = false;
            rdbQBPOS.Checked = false;
            rdbPOS.Checked = false;
            pnlQB3.Visible = false;
            rdbINT.Checked = false;
            rdbGP.Checked = false;
        }
        if (sender.Equals(rbtn50PctY))
        {
            strBusiness = "";
            /*pnlFEIN.Visible = true;
            rbtnFEINY.Checked = false;
            rbtnFEINN.Checked = false;*/
            pnlOtherCurrencies.Visible = true;
            rbtnOtherCurrenciesY.Checked = false;
            rbtnOtherCurrenciesN.Checked = false;
            pnlSage.Visible = false;
            rdbSageY.Checked = false;
            rdbSageN.Checked = false;
            pnlQB1.Visible = false;
            rdbQBYes.Checked = false;
            rdbQBNo.Checked = false;
            pnlQB2.Visible = false;
            rdbQB.Checked = false;
            rdbGOPAY.Checked = false;
            rdbQBPOS.Checked = false;
            rdbPOS.Checked = false;
            pnlQB3.Visible = false;
            rdbINT.Checked = false;
            rdbGP.Checked = false;
        }

        if (sender.Equals(rbtn50PctN))
        {
            strBusiness = "International";
            /*pnlFEIN.Visible = false;
            rbtnFEINY.Checked = false;
            rbtnFEINN.Checked = false;*/
            pnlOtherCurrencies.Visible = false;
            rbtnOtherCurrenciesY.Checked = false;
            rbtnOtherCurrenciesN.Checked = false;
            pnlSage.Visible = false;
            rdbSageY.Checked = false;
            rdbSageN.Checked = false;
            pnlQB1.Visible = false;
            rdbQBYes.Checked = false;
            rdbQBNo.Checked = false;
            pnlQB2.Visible = false;
            rdbQB.Checked = false;
            rdbGOPAY.Checked = false;
            rdbQBPOS.Checked = false;
            rdbPOS.Checked = false;
            pnlQB3.Visible = false;
            rdbINT.Checked = false;
            rdbGP.Checked = false;
        }

        if (sender.Equals(rbtnOtherCurrenciesY))
        {
            strBusiness = "International";
            pnlSage.Visible = false;
            rdbSageY.Checked = false;
            rdbSageN.Checked = false;
            pnlQB1.Visible = false;
            rdbQBYes.Checked = false;
            rdbQBNo.Checked = false;
            pnlQB2.Visible = false;
            rdbQB.Checked = false;
            rdbGOPAY.Checked = false;
            rdbQBPOS.Checked = false;
            rdbPOS.Checked = false;
            pnlQB3.Visible = false;
            rdbINT.Checked = false;
            rdbGP.Checked = false;
        }

        if (sender.Equals(rbtnOtherCurrenciesN))
        {
            strBusiness = "";
            pnlSage.Visible = true;
            rdbSageY.Checked = false;
            rdbSageN.Checked = false;
            pnlQB1.Visible = false;
            rdbQBYes.Checked = false;
            rdbQBNo.Checked = false;
            pnlQB2.Visible = false;
            rdbQB.Checked = false;
            rdbGOPAY.Checked = false;
            rdbQBPOS.Checked = false;
            rdbPOS.Checked = false;
            pnlQB3.Visible = false;
            rdbINT.Checked = false;
            rdbGP.Checked = false;
        }

        if (sender.Equals(rdbSageY))
        {
            strBusiness = "Domestic";
            strSW = "SageYes";
            //strPOS = "";
            pnlQB1.Visible = false;
            rdbQBYes.Checked = false;
            rdbQBNo.Checked = false;
            pnlQB2.Visible = false;
            rdbQB.Checked = false;
            rdbGOPAY.Checked = false;
            rdbQBPOS.Checked = false;
            rdbPOS.Checked = false;
            pnlQB3.Visible = false;
            rdbINT.Checked = false;
            rdbGP.Checked = false;
        }

        if (sender.Equals(rdbSageN))
        {
            strBusiness = "";
            strSW = "SageNo";
            //strPOS = "";
            pnlQB1.Visible = true;
            rdbQBYes.Checked = false;
            rdbQBNo.Checked = false;
            pnlQB2.Visible = false;
            rdbQB.Checked = false;
            rdbGOPAY.Checked = false;
            rdbQBPOS.Checked = false;
            rdbPOS.Checked = false;
            pnlQB3.Visible = false;
            rdbINT.Checked = false;
            rdbGP.Checked = false;
        }

        if (sender.Equals(rdbQBYes))
        {
            strBusiness = "";
            strSW = "QBYes";
            pnlQB2.Visible = true;
            rdbQB.Checked = false;
            rdbGOPAY.Checked = false;
            rdbQBPOS.Checked = false;
            rdbPOS.Checked = false;
            pnlQB3.Visible = false;
            rdbINT.Checked = false;
            rdbGP.Checked = false;
        }

        if (sender.Equals(rdbQBNo))
        {
            strBusiness = "Domestic";
            strSW = "QBNo";
            //strPOS = "No";
            pnlQB2.Visible = false;
            rdbQB.Checked = false;
            rdbGOPAY.Checked = false;
            rdbQBPOS.Checked = false;
            rdbPOS.Checked = false;
            pnlQB3.Visible = false;
            rdbINT.Checked = false;
            rdbGP.Checked = false;
        }

        if (sender.Equals(rdbQB))
        {
            strSW = "QB";
            strBusiness = "Domestic";
            pnlQB3.Visible = true;
            rdbINT.Checked = false;
            rdbGP.Checked = false;
        }

        if (sender.Equals(rdbINT) || sender.Equals(rdbGP))
        {
            strSW = "QB";
            strBusiness = "Domestic";
        }
        if (sender.Equals(rdbINT) && sender.Equals(rdbGP))
        {
            strSW = "QB";
            strBusiness = "Domestic";
        }
        if (sender.Equals(rdbGOPAY))
        {
            strSW = "GOPAY";
            strBusiness = "Domestic";
            pnlQB3.Visible = false;
            rdbINT.Checked = false;
            rdbGP.Checked = false;
        }
        if (sender.Equals(rdbQBPOS))
        {
            strSW = "QBPOS";
            strBusiness = "Domestic";
            pnlQB3.Visible = false;
            rdbINT.Checked = false;
            rdbGP.Checked = false;
        }
        if (sender.Equals(rdbPOS))
        {
            strSW = "POS";
            strBusiness = "Domestic";
            pnlQB3.Visible = false;
            rdbINT.Checked = false;
            rdbGP.Checked = false;
        }

        
    }//end function radio button check changed

    //This function displays error messages
    protected void SetErrorMessage(string errText)
    {
        lblError.Visible = true;
        lblError.Text = errText;
    }//end function set error message
}
