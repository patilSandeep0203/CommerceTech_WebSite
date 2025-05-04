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
using System.Data.SqlClient;
using OnlineAppClassLibrary;

public partial class Gateway_reprog : System.Web.UI.Page
{
    private static int AppId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session.IsNewSession)
            Response.Redirect("/Application/default.aspx?SessionExpired=True");

        Style errLabel = new Style();
        errLabel.BackColor = System.Drawing.Color.FromArgb(255, 192, 192);
        errLabel.ForeColor = System.Drawing.Color.Black;
        errLabel.BorderColor = System.Drawing.Color.Red;
        errLabel.BorderStyle = BorderStyle.Solid;
        errLabel.BorderWidth = Unit.Pixel(1);
        errLabel.Font.Size = FontUnit.Small;
        lblError.ApplyStyle(errLabel);        

        Response.CacheControl = "no-cache";
        Response.AddHeader("Pragma", "no-cache");
        Response.Expires = -1;

        AppId = Convert.ToInt32(Session["AppId"]);

        if (!IsPostBack)
        {
            if (!User.Identity.IsAuthenticated)
                Response.Redirect("/Application/default.aspx?Authentication=False");
            else
            {
                try
                {
                    if (!Locked())                    
                        PopulatePage();
                    else
                    {
                        PopulatePage();
                        SetErrorMessage("The status of your application prevents it from being edited");
                        btnEditInfo.Enabled = false;
                        btnSubmit.Enabled = false;
                    }
                }
                catch (Exception err)
                {
                    CreateOnlineAppLog Log = new CreateOnlineAppLog();
                    Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + Session["AppId"].ToString() + " - " + "Gateway reprog.aspx Page Load Error - " + err.Message);
                     SetErrorMessage("Error Processing Request. Please contact Technical Support: <a href='mailto:support@commercetech.com?Subject=Online Application Error'> support@commercetech.com. </a>.");
                }
            }
        }//end if post back
    }

    //This function checks if the application is locked
    protected bool Locked()
    {
        if (Session["Locked"].ToString().Trim() == "Yes")
            return true;
        else
            return false;
    }

    //This function populates form fields
    protected void PopulatePage()
    {
        if (AppId != 0)
        {
            //Get info from reprogram table
            ReprogramInfo Reprogram = new ReprogramInfo(AppId);
            DataSet dsReprogramInfo = Reprogram.GetReprogramInfo();
            if (dsReprogramInfo.Tables[0].Rows.Count > 0)
            {
                DataRow drReprogramInfo = dsReprogramInfo.Tables[0].Rows[0];
                if (Convert.ToBoolean(drReprogramInfo["Reprogram"]) == true)
                    rdbYes.Checked = true;
                else                
                    rdbNo.Checked = true;                    
                lblPlatform.Text = drReprogramInfo["Platform"].ToString().Trim();
                lblMerchantNumber.Text = MaskNumbers(Server.HtmlEncode(drReprogramInfo["MerchantNum"].ToString().Trim()));
                lblMerchantIDNumber.Text = MaskNumbers(Server.HtmlEncode(drReprogramInfo["MerchantID"].ToString().Trim()));
                lblLoginIDNumber.Text = Server.HtmlEncode(drReprogramInfo["LoginID"].ToString().Trim());
                lblTerminalIDNumber.Text = Server.HtmlEncode(drReprogramInfo["TerminalID"].ToString().Trim());
                lblBankIDNumber.Text = Server.HtmlEncode(drReprogramInfo["BankIDNum"].ToString().Trim());
                lblAgentBankNumber.Text = Server.HtmlEncode(drReprogramInfo["AgentBankIDNum"].ToString().Trim());
                lblAgentChainNumber.Text = Server.HtmlEncode(drReprogramInfo["AgentChainNum"].ToString().Trim());
                lblMCCCategoryCode.Text = Server.HtmlEncode(drReprogramInfo["MCCCategoryCode"].ToString().Trim());
                lblStoreNumber.Text = Server.HtmlEncode(drReprogramInfo["StoreNum"].ToString().Trim());                
            }//end if reprogram count not 0            
        }//end if appid not 0
    }//end function populate

    //This function masks first few digits with x
    protected string MaskNumbers(string strNumber)
    {
        string Number = strNumber;
        string Num = "";
        int j = 0;
        if (Number.Length >= 4)
        {
            for (int i = 0; i < Number.Length - 4; i++)
            {
                Num += "x";
                j++;
            }
        }
        Number = Number.Substring(j);
        Num += Number;
        return Num;
    }

    //This function handles submit button click event
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            CommonFunctions UpdateGeneralInfo = new CommonFunctions(AppId);
            UpdateGeneralInfo.SetGWPageCount();
            Response.Redirect("../notice.aspx", false);
        }
        catch (Exception err)
        {
            CreateOnlineAppLog Log = new CreateOnlineAppLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + Session["AppId"].ToString() + " - " + "Gateway reprog.aspx Error - " + err.Message);
             SetErrorMessage("Error Processing Request. Please contact Technical Support: <a href='mailto:support@commercetech.com?Subject=Online Application Error'> support@commercetech.com. </a>.");
        }
    }//end function submit button clicked
    
    //This function displays error messages
    protected void SetErrorMessage(string errText)
    {
        lblError.Visible = true;
        lblError.Text = errText;
    }//end function set error message
}
