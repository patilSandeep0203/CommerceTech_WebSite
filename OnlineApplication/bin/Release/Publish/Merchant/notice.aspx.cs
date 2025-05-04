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
using OnlineAppClassLibrary;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Text;
using System.IO;

public partial class Merchant_notice : System.Web.UI.Page
{
    private static int AppId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        AppId = Convert.ToInt32(Session["AppId"]);
        if (Session.IsNewSession)
            Response.Redirect("/Application/default.aspx?SessionExpired=True");

        Response.CacheControl = "no-cache";
        Response.AddHeader("Pragma", "no-cache");
        Response.Expires = -1;

        imgCreatePDF.Attributes.Add("onmouseover", "this.src = '/Application/Images/CreatePDF_Mouseover.gif'");
        imgCreatePDF.Attributes.Add("onmouseout", "this.src = '/Application/Images/CreatePDF.gif'");

        Style errLabel = new Style();
        errLabel.BackColor = System.Drawing.Color.FromArgb(255, 192, 192);
        errLabel.ForeColor = System.Drawing.Color.Black;
        errLabel.BorderColor = System.Drawing.Color.Red;
        errLabel.BorderStyle = BorderStyle.Solid;
        errLabel.BorderWidth = Unit.Pixel(1);
        errLabel.Font.Size = FontUnit.Small;
        lblError.ApplyStyle(errLabel);
        lblIncomplete.ApplyStyle(errLabel);

        if (!IsPostBack)
        {
            if (!User.Identity.IsAuthenticated)
                Response.Redirect("/Application/default.aspx?Authentication=False");
            else
            {
                try
                {
                    CheckAppStatus();
                }//end try
                catch (Exception err)
                {
                    CreateOnlineAppLog Log = new CreateOnlineAppLog();
                    Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + Session["AppId"].ToString() + " - " + "Merchant notice.aspx Page Load Error - " + err.Message);
                     SetErrorMessage("Error Processing Request. Please contact Technical Support: <a href='mailto:support@commercetech.com?Subject=Online Application Error'> support@commercetech.com. </a>.");
                }
                lblAppId.Text = Convert.ToString(Session["AppId"]);
                lblAppId.Font.Size = FontUnit.Point(10);
                lblAppId.ForeColor = System.Drawing.Color.Salmon;
            }
        }//end if postback
    }//end page load

    //This function checks app status
    protected void CheckAppStatus()
    {
        //Check status of app and display appropriate label
        NewAppInfo AppInfo = new NewAppInfo(AppId);
        string Status = AppInfo.ReturnStatus();
        if ((Status == "") || (Status == "INCOMPLETE") || (Status == "Incomplete"))
        {
            lblIncomplete.Visible = true;
            //lblComplete.Visible = false;
            //lblCompleteMsg.Visible = false;
            pnlCreatePDF.Visible = false;
        }
        else
        {
            lblIncomplete.Visible = false;
            //lblComplete.Visible = true;
            //lblCompleteMsg.Visible = true;
            //Display the Create PDF button
            pnlCreatePDF.Visible = true;
        }

        if (Convert.ToInt32(Session["AcctType"]) == 4)
        {
            Status = AppInfo.ReturnStatusGW();
            if ((Status == "") || (Status == "INCOMPLETE") || (Status == "Incomplete"))
                pnlGatewayIncomplete.Visible = true;
            else
                pnlGatewayIncomplete.Visible = false;
        }

    }//end function check app status

    //This function displays error messages
    protected void SetErrorMessage(string errText)
    {
        lblError.Visible = true;
        lblError.Text = errText;
    }//end function set error message

    protected void imgCreatePDF_Click(object sender, ImageClickEventArgs e)
    {
        //Call the same Create PDF function as the Master Page
    //    Main MainPg = (Main)this.Master;        
    //    MainPg.lnkbtnCreatePDF_Click(sender, e);
               
    }

    protected void btnChaseAbout_Click(object sender, EventArgs e)
    {
    //    Main MainPg = (Main)this.Master;
    //    MainPg.btnChaseAbout_Click(sender, e);
 
    }

    protected void btnChaseFee_Click(object sender, EventArgs e)
    {
    //    Main MainPg = (Main)this.Master;
    //    MainPg.btnChaseFee_Click(sender, e);
    }

    protected void btnChaseMP_Click(object sender, EventArgs e)
    {
    //    Main MainPg = (Main)this.Master;
    //    MainPg.btnChaseMP_Click(sender, e);
    }

    protected void btnChaseCreditAdd_Click(object sender, EventArgs e)
    {
    //    Main MainPg = (Main)this.Master;
    //    MainPg.btnChaseCreditAdd_Click(sender, e);
    }//end function Chase Credit Addendum Button Click
         
}
