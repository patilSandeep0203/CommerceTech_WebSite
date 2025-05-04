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

public partial class Questions2 : System.Web.UI.Page
{
    private static string strCountry = "";    
    private static string parameters = "";
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
            strCountry = "";
    }

    protected void btnStart_Click(object sender, EventArgs e)
    {
        Response.Redirect("Start.aspx");
    }
    
    //This function handles submit button click event
    protected void btnContinue_Click(object sender, EventArgs e)
    {
        if (strCountry == "")
        {
            SetErrorMessage("Please select one of the options listed below.");
        }
        else if (strCountry == "Unacceptable")
        {
            SetErrorMessage("We cannot process your application since your business is listed in the Unacceptable Businesses list.");
        }
        else if (strCountry == "UnacceptableCountry")
        {
            SetErrorMessage("We cannot process your application since your business is located in one of the Unacceptable Countries.");
        }
        else if (strCountry == "AppFeeNo")
        {
            SetErrorMessage("The $200 application fee is required for international merchants. We cannot process your application unless you agree to pay the application fee.");
        }
        else if (strCountry == "MonVolNo")
        {
            SetErrorMessage("For merchants outside the United States or Canada, a mininum average credit card volume of <br> $50,000 is required. We cannot process your application since your average credit card volume is less than $50,000.");
        }
        else
        {
            //pnlLink.Visible = false;
            Response.Redirect("register.aspx?Country=" + strCountry +"&Serv=International");
            /*if (strCountry == "WorldPay")
                Response.Redirect("https://secure.worldpay.com/app/application.pl?brand=ctc&Scheme=1");
            else if (strCountry == "Canada")
                Response.Redirect("register.aspx?Country=" + strCountry + "&" + parameters);
            else if (strCountry == "CalApp")
            {
                pnlLink.Visible = true;
                lnkDownloadApp.NavigateUrl = "http://www.firstaffiliates.com/OnlineApplication/CAL_Application_NA_forms.pdf";
                lnkDownloadApp.Target = "_blank";
            }
            else if (strCountry == "Kitts")
            {
                pnlLink.Visible = true;
                lnkDownloadApp.NavigateUrl = "http://www.firstaffiliates.com/OnlineApplication/St  Kitts MOTO_app.doc";
                lnkDownloadApp.Target = "_blank";
            }*/
        }
    }//end function submit button click

    //This function handles radio button check changed event
    protected void rdb_CheckedChanged(object sender, EventArgs e)
    {
        if (sender.Equals(rbtnUnaccBusN))
        {
            pnlCanada.Visible = true;
            rdbCanadaYes.Checked = false;
            rdbCanadaNo.Checked = false;
            pnl_AppFee.Visible = false;
            rdb_AppFeeYes.Checked = false;
            rdb_AppFeeNo.Checked = false;
            pnl_MonVolume.Visible = false;
            rdbMonVolYes.Checked = false;
            rdbMonVolNo.Checked = false;
            pnlOtherCountries.Visible = false;
            rdbOtherYes.Checked = false;
            rdbOtherNo.Checked = false;
            pnlUK.Visible = false;
            rdbUKYes.Checked = false;
            rdbUKNo.Checked = false;
            pnlUnAcceptableCountries.Visible = false;
            rdbUnAccCountryYes.Checked = false;
            rdbUnAccCountryNo.Checked = false;            
            pnlKitts.Visible = false;
            rdbKittsYes.Checked = false;
            rdbKittsNo.Checked = false;
            pnlLinks.Visible = false;
            strCountry = "";
        }
        if (sender.Equals(rbtnUnaccBusY))
        {
            pnlCanada.Visible = false;
            rdbCanadaYes.Checked = false;
            rdbCanadaNo.Checked = false;
            pnl_AppFee.Visible = false;
            rdb_AppFeeYes.Checked = false;
            rdb_AppFeeNo.Checked = false;
            pnl_MonVolume.Visible = false;
            rdbMonVolYes.Checked = false;
            rdbMonVolNo.Checked = false;
            pnlOtherCountries.Visible = false;
            rdbOtherYes.Checked = false;
            rdbOtherNo.Checked = false;
            pnlUK.Visible = false;
            rdbUKYes.Checked = false;
            rdbUKNo.Checked = false;
            pnlUnAcceptableCountries.Visible = false;
            rdbUnAccCountryYes.Checked = false;
            rdbUnAccCountryNo.Checked = false;
            pnlKitts.Visible = false;
            rdbKittsYes.Checked = false;
            rdbKittsNo.Checked = false;
            pnlLinks.Visible = false;
            strCountry = "Unacceptable";
        }
        if (sender.Equals(rdbCanadaYes))
        {
            pnl_AppFee.Visible = false;
            rdb_AppFeeYes.Checked = false;
            rdb_AppFeeNo.Checked = false;
            pnl_MonVolume.Visible = false;
            rdbMonVolYes.Checked = false;
            rdbMonVolNo.Checked = false;
            pnlOtherCountries.Visible = false;
            rdbOtherYes.Checked = false;
            rdbOtherNo.Checked = false;
            pnlUK.Visible = false;
            rdbUKYes.Checked = false;
            rdbUKNo.Checked = false;
            pnlUnAcceptableCountries.Visible = false;
            rdbUnAccCountryYes.Checked = false;
            rdbUnAccCountryNo.Checked = false;
            pnlKitts.Visible = false;
            rdbKittsYes.Checked = false;
            rdbKittsNo.Checked = false;
            pnlLinks.Visible = false;
            strCountry = "Canada";
        }

        if (sender.Equals(rdbCanadaNo))
        {
            pnl_AppFee.Visible = true;
            rdb_AppFeeYes.Checked = false;
            rdb_AppFeeNo.Checked = false;
            pnl_MonVolume.Visible = false;
            rdbMonVolYes.Checked = false;
            rdbMonVolNo.Checked = false;
            pnlOtherCountries.Visible = false;
            rdbOtherYes.Checked = false;
            rdbOtherNo.Checked = false;
            pnlUK.Visible = false;
            rdbUKYes.Checked = false;
            rdbUKNo.Checked = false;
            pnlUnAcceptableCountries.Visible = false;
            rdbUnAccCountryYes.Checked = false;
            rdbUnAccCountryNo.Checked = false;
            pnlKitts.Visible = false;
            rdbKittsYes.Checked = false;
            rdbKittsNo.Checked = false;
            pnlLinks.Visible = false;
            strCountry = "";
        }

        if (sender.Equals(rdb_AppFeeYes))
        {
            pnl_MonVolume.Visible = true;
            rdbMonVolYes.Checked = false;
            rdbMonVolNo.Checked = false;
            pnlOtherCountries.Visible = false;
            rdbOtherYes.Checked = false;
            rdbOtherNo.Checked = false;
            pnlUK.Visible = false;
            rdbUKYes.Checked = false;
            rdbUKNo.Checked = false;
            pnlUnAcceptableCountries.Visible = false;
            rdbUnAccCountryYes.Checked = false;
            rdbUnAccCountryNo.Checked = false;
            pnlKitts.Visible = false;
            rdbKittsYes.Checked = false;
            rdbKittsNo.Checked = false;
            pnlLinks.Visible = false;
            strCountry = "";
        }

        if (sender.Equals(rdb_AppFeeNo))
        {
            pnl_MonVolume.Visible = false;
            rdbMonVolYes.Checked = false;
            rdbMonVolNo.Checked = false;
            pnlOtherCountries.Visible = false;
            rdbOtherYes.Checked = false;
            rdbOtherNo.Checked = false;
            pnlUK.Visible = false;
            rdbUKYes.Checked = false;
            rdbUKNo.Checked = false;
            pnlUnAcceptableCountries.Visible = false;
            rdbUnAccCountryYes.Checked = false;
            rdbUnAccCountryNo.Checked = false;
            pnlKitts.Visible = false;
            rdbKittsYes.Checked = false;
            rdbKittsNo.Checked = false;
            pnlLinks.Visible = false;
            strCountry = "AppFeeNo";
        }


        if (sender.Equals(rdbMonVolYes))
        {
            pnlOtherCountries.Visible = true;
            rdbOtherYes.Checked = false;
            rdbOtherNo.Checked = false;
            pnlUK.Visible = false;
            rdbUKYes.Checked = false;
            rdbUKNo.Checked = false;
            pnlUnAcceptableCountries.Visible = false;
            rdbUnAccCountryYes.Checked = false;
            rdbUnAccCountryNo.Checked = false;
            pnlKitts.Visible = false;
            rdbKittsYes.Checked = false;
            rdbKittsNo.Checked = false;
            pnlLinks.Visible = false;
            strCountry = "";
        }

        if (sender.Equals(rdbMonVolNo))
        {
            pnlOtherCountries.Visible = false;
            rdbOtherYes.Checked = false;
            rdbOtherNo.Checked = false;
            pnlUK.Visible = false;
            rdbUKYes.Checked = false;
            rdbUKNo.Checked = false;
            pnlUnAcceptableCountries.Visible = false;
            rdbUnAccCountryYes.Checked = false;
            rdbUnAccCountryNo.Checked = false;
            pnlKitts.Visible = false;
            rdbKittsYes.Checked = false;
            rdbKittsNo.Checked = false;
            pnlLinks.Visible = false;
            strCountry = "MonVolNo";
        }

        if (sender.Equals(rdbOtherYes))
        {
            pnlUK.Visible = true;
            rdbUKYes.Checked = false;
            rdbUKNo.Checked = false;
            pnlUnAcceptableCountries.Visible = false;
            rdbUnAccCountryYes.Checked = false;
            rdbUnAccCountryNo.Checked = false;
            pnlKitts.Visible = false;
            rdbKittsYes.Checked = false;
            rdbKittsNo.Checked = false;
            pnlLinks.Visible = false;
            strCountry = "";
        }

        if (sender.Equals(rdbOtherNo))
        {
            pnlUK.Visible = false;
            rdbUKYes.Checked = false;
            rdbUKNo.Checked = false;
            pnlUnAcceptableCountries.Visible = true;
            rdbUnAccCountryYes.Checked = false;
            rdbUnAccCountryNo.Checked = false;
            pnlKitts.Visible = false;
            rdbKittsYes.Checked = false;
            rdbKittsNo.Checked = false;
            pnlLinks.Visible = false;
            strCountry = "";
        }

        if (sender.Equals(rdbUKYes))
        {
            pnlUnAcceptableCountries.Visible = false;
            rdbUnAccCountryYes.Checked = false;
            rdbUnAccCountryNo.Checked = false;
            pnlKitts.Visible = false;
            rdbKittsYes.Checked = false;
            rdbKittsNo.Checked = false;
            pnlLinks.Visible = false;
            strCountry = "B+S";
        }

        if (sender.Equals(rdbUKNo))
        {
            pnlUnAcceptableCountries.Visible = true;
            rdbUnAccCountryYes.Checked = false;
            rdbUnAccCountryNo.Checked = false;
            pnlKitts.Visible = false;
            rdbKittsYes.Checked = false;
            rdbKittsNo.Checked = false;
            pnlLinks.Visible = false;
            strCountry = "";
        }

        if (sender.Equals(rdbUnAccCountryYes))
        {
            pnlKitts.Visible = false;
            rdbKittsYes.Checked = false;
            rdbKittsNo.Checked = false;
            pnlLinks.Visible = false;
            strCountry = "UnacceptableCountry";
        }

        if (sender.Equals(rdbUnAccCountryNo))
        {
            pnlKitts.Visible = true;
            rdbKittsYes.Checked = false;
            rdbKittsNo.Checked = false;
            pnlLinks.Visible = false;
            strCountry = "";
        }
                
        if (sender.Equals(rdbKittsYes))
        {
            pnlLinks.Visible = false;
            strCountry = "Kitts";
        }

        if (sender.Equals(rdbKittsNo))
        {
            pnlLinks.Visible = true;
            rdbLinksEU.Checked = false;
            rdbLinksStKitts.Checked = false;
            strCountry = "";
        }

        if (sender.Equals(rdbLinksEU))
        {
            strCountry = "B+S";
        }

        if (sender.Equals(rdbLinksStKitts))
        {
            strCountry = "Kitts";
        }

    }//end function radio button check changed

    //This function displays error messages
    protected void SetErrorMessage(string errText)
    {
        lblError.Visible = true;
        lblError.Text = errText;
    }//end function set error message
}
