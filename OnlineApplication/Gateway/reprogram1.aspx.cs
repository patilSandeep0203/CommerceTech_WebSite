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


public partial class Gateway_reprogram : System.Web.UI.Page
{
    private static string strReplace = "";
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
        lblMessage.ApplyStyle(errLabel);

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
                    {
                        //Check if principal info has already been entered. If yes, 
                        //then check if user wants to edit info
                        //else redirect to principal.aspx.
                        ReprogramInfo ReprogramComplete = new ReprogramInfo(AppId);
                        if (ReprogramComplete.CheckReprogramComplete() > 0)
                        {
                            if (Request.Params.Get("EditPlatform") != null)
                            {
                                //Do not display numbers
                            }
                            else
                                Response.Redirect("reprog.aspx", false);
                        }

                        int AcctType = Convert.ToInt32(Session["AcctType"]);
                        if (AcctType == 4)
                        {
                            lblMessage.Visible = true;
                        }
                        PopulatePage();
                    }
                    else
                    {
                        SetErrorMessage("The status of your application prevents it from being edited");
                        btnSubmit.Enabled = false;
                    }
                }
                catch (Exception err)
                {
                    CreateOnlineAppLog Log = new CreateOnlineAppLog();
                    Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + Session["AppId"].ToString() + " - " + "Gateway reprogram.aspx Page Load Error - " + err.Message);
                     SetErrorMessage("Error Processing Request. Please contact Technical Support: <a href='mailto:support@ecenow.com?Subject=Online Application Error'> support@ecenow.com. </a>.");
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
                {
                    rdbNo.Checked = true;
                    DisableControls();
                    ValidatePlatform.Enabled = false;
                }
                int index = lstPlatform.Items.IndexOf(lstPlatform.Items.FindByText(drReprogramInfo["Platform"].ToString().Trim()));
                lstPlatform.SelectedIndex = index;
                //SwitchPlatforms();
                DisablePlatformFields();
                /*txtMerchantNumber.Text = drReprogramInfo["MerchantNum"].ToString().Trim();
                txtMerchantIDNumber.Text = drReprogramInfo["MerchantID"].ToString().Trim();
                txtLoginIDNumber.Text = drReprogramInfo["LoginID"].ToString().Trim();
                txtTerminalIDNumber.Text = drReprogramInfo["TerminalID"].ToString().Trim();
                txtBankNumber.Text = drReprogramInfo["BankAcctNum"].ToString().Trim();
                txtBINNumber.Text = drReprogramInfo["BINNum"].ToString().Trim();
                txtAgentBankNumber.Text = drReprogramInfo["AgentBankNum"].ToString().Trim();
                txtAgentChainNumber.Text = drReprogramInfo["AgentChainNum"].ToString().Trim();
                txtMCCCategoryCode.Text = drReprogramInfo["MCCCategoryCode"].ToString().Trim();
                txtStoreNumber.Text = drReprogramInfo["StoreNum"].ToString().Trim();*/
            }//end if reprogram count not 0
            else
            {
                DisableControls();
            }            
        }//end if appid not 0
    }//end function populate

    //This function handles submit button click event
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                //Validate data
                bool retValidate = ValidateData();
                if (retValidate)
                {
                    bool bReprogram = false;
                    if (rdbYes.Checked)
                        bReprogram = true;
                    ReprogramInfo UpdateReprogram = new ReprogramInfo(AppId);
                    bool retVal = UpdateReprogram.UpdateReprogramInfo(lstPlatform.SelectedItem.Text.Trim().Replace("'", strReplace),
                            txtMerchantNumber.Text.Trim().Replace("'", strReplace), txtMerchantIDNumber.Text.Trim().Replace("'", strReplace), txtTerminalIDNumber.Text.Trim().Replace("'", strReplace),
                            txtLoginIDNumber.Text.Trim().Replace("'", strReplace), txtBankNumber.Text.Trim().Replace("'", strReplace), 
                            txtAgentBankNumber.Text.Trim().Replace("'", strReplace), txtAgentChainNumber.Text.Trim().Replace("'", strReplace), txtMCCCategoryCode.Text.Trim().Replace("'", strReplace),
                            txtStoreNumber.Text.Trim().Replace("'", strReplace), true, bReprogram);


                    //Set last modified date in newapp table
                    CommonFunctions UpdateGeneralInfo = new CommonFunctions(AppId);
                    UpdateGeneralInfo.UpdateLastModified();

                    UpdateGeneralInfo.SetGWPageCount();

                    LogBL LogData = new LogBL(AppId);
                    LogData.InsertLogData(Convert.ToInt32(Session["AffiliateID"]), "Gateway Reprogram Information Updated");

                    Response.Redirect("notice.aspx", false);
                }//end if ValidateData
            }//end if page valid
        }//end try
        catch (Exception err)
        {
            CreateOnlineAppLog Log = new CreateOnlineAppLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + Session["AppId"].ToString() + " - " + "Gateway reprogram.aspx Error - " + err.Message);
             SetErrorMessage("Error Processing Request. Please contact Technical Support: <a href='mailto:support@ecenow.com?Subject=Online Application Error'> support@ecenow.com. </a>.");
        }
    }//end function submit button clicked

    //This function validates data in text fields
    protected bool ValidateData()
    {
        TextBox txtBox = new TextBox();
        for (int i = 0; i < pnlMainPage.Controls.Count; i++)
        {
            if (pnlMainPage.Controls[i].GetType() == txtBox.GetType())
            {
                txtBox = (TextBox)pnlMainPage.Controls[i];
                if ((txtBox.Text.Contains("--")) || (txtBox.Text.Contains("#")) || (txtBox.Text.Contains(";")) || (txtBox.Text.Contains("'")))
                {
                    txtBox.BackColor = System.Drawing.Color.Ivory;
                    txtBox.Focus();
                    SetErrorMessage("You cannot use apostrophe, # or semi-colons in any of the following fields.");
                    return false;
                }
            }
        }
        return true;
    }//end function validate data

    //This function handles yes/no radiobutton click event
    protected void rdbYes_CheckedChanged(object sender, EventArgs e)
    {
        if (sender.Equals(rdbYes))
        {
            ValidatePlatform.Enabled = true;
            lstPlatform.Enabled = true;
            lstPlatform.BackColor = System.Drawing.Color.White;
            //SwitchPlatforms();
            DisablePlatformFields();
        }
        else
        {
            DisableControls();
            ValidatePlatform.Enabled = false;
        }
    }//end function yes/no radiobutton click

    //This function handles platform list selection changed event
    protected void lstPlatform_SelectedIndexChanged(object sender, EventArgs e)
    {
        //SwitchPlatforms();        
        ResetPlatformControls();
        DisablePlatformFields();
    }//end function lstPlatform selection changed


    //This function disables fields based on reprogram platform selected
    public void DisablePlatformFields()
    {
        if (lstPlatform.SelectedItem.Text.Contains("Nashville"))
        {
            //Merchant ID
            txtMerchantIDNumber.Enabled = true;
            txtMerchantIDNumber.MaxLength = 7;
            txtTerminalIDNumber.Enabled = true;
            txtTerminalIDNumber.MaxLength = 7;
            txtMerchantNumber.MaxLength = 16;
            txtMerchantNumber.Enabled = true;

            txtStoreNumber.Enabled = false;
            txtMCCCategoryCode.Enabled = false;
            
            txtBankNumber.Enabled = false;
            txtAgentChainNumber.Enabled = false;
            txtAgentBankNumber.Enabled = false;
            txtLoginIDNumber.Enabled = false;

            txtAgentBankNumber.BackColor = System.Drawing.Color.DarkGray;
            txtAgentChainNumber.BackColor = System.Drawing.Color.DarkGray;
            txtBankNumber.BackColor = System.Drawing.Color.DarkGray;

            txtLoginIDNumber.BackColor = System.Drawing.Color.DarkGray;
            txtMCCCategoryCode.BackColor = System.Drawing.Color.DarkGray;
            txtMerchantIDNumber.BackColor = System.Drawing.Color.White;
            txtMerchantNumber.BackColor = System.Drawing.Color.White;
            txtStoreNumber.BackColor = System.Drawing.Color.DarkGray;
            txtTerminalIDNumber.BackColor = System.Drawing.Color.White;
        }//end if nashville
        else if (lstPlatform.SelectedItem.Text.Contains("Vital"))
        {
            txtMerchantIDNumber.Enabled = true;
            txtMerchantIDNumber.MaxLength = 12;
            txtBankNumber.Enabled = true;
            txtBankNumber.MaxLength = 6;
            txtAgentBankNumber.Enabled = true;
            txtAgentBankNumber.MaxLength = 6;
            txtAgentChainNumber.Enabled = true;
            txtAgentChainNumber.MaxLength = 6;
            txtMCCCategoryCode.Enabled = true;
            txtMCCCategoryCode.MaxLength = 4;
            txtStoreNumber.Enabled = true;
            txtStoreNumber.MaxLength = 4;
            txtTerminalIDNumber.Enabled = true;
            txtTerminalIDNumber.MaxLength = 4;
            txtLoginIDNumber.Enabled = false;
            txtMerchantNumber.Enabled = false;
                   
            txtAgentBankNumber.BackColor = System.Drawing.Color.White;
            txtAgentChainNumber.BackColor = System.Drawing.Color.White;
            txtBankNumber.BackColor = System.Drawing.Color.White;
            txtLoginIDNumber.BackColor = System.Drawing.Color.DarkGray;
            txtMCCCategoryCode.BackColor = System.Drawing.Color.White;
            txtMerchantNumber.BackColor = System.Drawing.Color.DarkGray;
            txtMerchantIDNumber.BackColor = System.Drawing.Color.White;
            txtStoreNumber.BackColor = System.Drawing.Color.White;
            txtTerminalIDNumber.BackColor = System.Drawing.Color.White;
        }//end if vital
        else if (lstPlatform.SelectedItem.Text.Contains("Nova"))
        {
            txtLoginIDNumber.Enabled = false;
            txtBankNumber.Enabled = true;
            txtBankNumber.MaxLength = 6;
            txtTerminalIDNumber.Enabled = true;
            txtTerminalIDNumber.MaxLength = 16;

            txtMerchantNumber.Enabled = false;
            txtStoreNumber.Enabled = false;
            txtMerchantIDNumber.Enabled = false;
            txtMCCCategoryCode.Enabled = false;            
            txtAgentChainNumber.Enabled = false;
            txtAgentBankNumber.Enabled = false;

            txtAgentBankNumber.BackColor = System.Drawing.Color.DarkGray;
            txtAgentChainNumber.BackColor = System.Drawing.Color.DarkGray;
            txtBankNumber.BackColor = System.Drawing.Color.White;
            txtLoginIDNumber.BackColor = System.Drawing.Color.DarkGray;
            txtMCCCategoryCode.BackColor = System.Drawing.Color.DarkGray;
            txtMerchantIDNumber.BackColor = System.Drawing.Color.DarkGray;
            txtMerchantNumber.BackColor = System.Drawing.Color.DarkGray;
            txtStoreNumber.BackColor = System.Drawing.Color.DarkGray;
            txtTerminalIDNumber.BackColor = System.Drawing.Color.White;
        }//end if nova
        else if (lstPlatform.SelectedItem.Text.Contains("Omaha"))
        {
            txtMerchantNumber.MaxLength = 16;
            txtMerchantNumber.Enabled = true;
            txtTerminalIDNumber.Enabled = false;
            txtStoreNumber.Enabled = false;
            txtMerchantIDNumber.Enabled = false;
            txtMCCCategoryCode.Enabled = true;
            txtMCCCategoryCode.MaxLength = 4;
            txtLoginIDNumber.Enabled = false;            
            txtBankNumber.Enabled = false;
            txtAgentChainNumber.Enabled = false;
            txtAgentBankNumber.Enabled = false;

            txtAgentBankNumber.BackColor = System.Drawing.Color.DarkGray;
            txtAgentChainNumber.BackColor = System.Drawing.Color.DarkGray;
            txtBankNumber.BackColor = System.Drawing.Color.DarkGray;
            txtLoginIDNumber.BackColor = System.Drawing.Color.DarkGray;
            txtMCCCategoryCode.BackColor = System.Drawing.Color.White;
            txtMerchantIDNumber.BackColor = System.Drawing.Color.DarkGray;
            txtMerchantNumber.BackColor = System.Drawing.Color.White;
            txtStoreNumber.BackColor = System.Drawing.Color.DarkGray;
            txtTerminalIDNumber.BackColor = System.Drawing.Color.DarkGray;
        }
        else if (lstPlatform.SelectedItem.Text.Contains("Global Payments"))
        {
            txtMerchantNumber.Enabled = true;
            txtMerchantNumber.BackColor = System.Drawing.Color.White;
            txtMerchantIDNumber.Enabled = false;
            txtMerchantIDNumber.BackColor = System.Drawing.Color.DarkGray;
            txtMCCCategoryCode.Enabled = false;
            txtMCCCategoryCode.BackColor = System.Drawing.Color.DarkGray;
            txtBankNumber.Enabled = true;
            txtBankNumber.BackColor = System.Drawing.Color.White;
            txtAgentChainNumber.Enabled = false;
            txtAgentChainNumber.BackColor = System.Drawing.Color.DarkGray;
            txtAgentBankNumber.Enabled = false;
            txtAgentBankNumber.BackColor = System.Drawing.Color.DarkGray;
            txtLoginIDNumber.Enabled = false;
            txtLoginIDNumber.BackColor = System.Drawing.Color.DarkGray;
            txtStoreNumber.Enabled = false;
            txtStoreNumber.BackColor = System.Drawing.Color.DarkGray;
            txtTerminalIDNumber.Enabled = false;
            txtTerminalIDNumber.BackColor = System.Drawing.Color.DarkGray;
        }
        else if (lstPlatform.SelectedItem.Text.Contains("North"))
        {
            txtMerchantNumber.MaxLength = 12;
            txtLoginIDNumber.Enabled = true;
            txtBankNumber.Enabled = true;
            txtBankNumber.MaxLength = 6;
            txtAgentBankNumber.Enabled = true;
            txtAgentBankNumber.MaxLength = 6;
            txtAgentChainNumber.Enabled = true;
            txtAgentChainNumber.MaxLength = 6;
            txtMCCCategoryCode.Enabled = true;
            txtMCCCategoryCode.MaxLength = 4;
            txtStoreNumber.Enabled = true;
            txtStoreNumber.MaxLength = 4;
            txtTerminalIDNumber.Enabled = true;
            txtTerminalIDNumber.MaxLength = 4;

            txtMerchantIDNumber.Enabled = true;

            txtAgentBankNumber.BackColor = System.Drawing.Color.White;
            txtAgentChainNumber.BackColor = System.Drawing.Color.White;
            txtBankNumber.BackColor = System.Drawing.Color.White;
            txtLoginIDNumber.BackColor = System.Drawing.Color.White;
            txtMCCCategoryCode.BackColor = System.Drawing.Color.White;
            txtMerchantIDNumber.BackColor = System.Drawing.Color.White;
            txtMerchantNumber.BackColor = System.Drawing.Color.White;
            txtStoreNumber.BackColor = System.Drawing.Color.White;
            txtTerminalIDNumber.BackColor = System.Drawing.Color.White;
        }//end if vital
        else if (lstPlatform.SelectedItem.Text.Contains("Paymentech"))
        {
            txtMerchantIDNumber.MaxLength = 12;
            txtMerchantIDNumber.Enabled = true;
            txtTerminalIDNumber.MaxLength = 3; 
            txtTerminalIDNumber.Enabled = true;
            txtStoreNumber.MaxLength = 4;
            txtStoreNumber.Enabled = true;
                        
            txtMerchantIDNumber.BackColor = System.Drawing.Color.White;
            txtTerminalIDNumber.BackColor = System.Drawing.Color.White;
            txtStoreNumber.BackColor = System.Drawing.Color.White;

            txtAgentBankNumber.Enabled = false;
            txtAgentBankNumber.BackColor = System.Drawing.Color.DarkGray;
            txtAgentChainNumber.Enabled = false;
            txtAgentChainNumber.BackColor = System.Drawing.Color.DarkGray;
            txtBankNumber.Enabled = false;
            txtBankNumber.BackColor = System.Drawing.Color.DarkGray;
            txtLoginIDNumber.Enabled = false;
            txtLoginIDNumber.BackColor = System.Drawing.Color.DarkGray;
            txtMCCCategoryCode.Enabled = false;
            txtMCCCategoryCode.BackColor = System.Drawing.Color.DarkGray;
            txtMerchantNumber.Enabled = false;
            txtMerchantNumber.BackColor = System.Drawing.Color.DarkGray;
            
        }//end if vital
        else //if (lstPlatform.SelectedItem.Text.Contains("None"))
        {
            txtMerchantNumber.Enabled = false;
            txtTerminalIDNumber.Enabled = false;
            txtStoreNumber.Enabled = false;
            txtMerchantIDNumber.Enabled = false;
            txtMCCCategoryCode.Enabled = false;
            txtLoginIDNumber.Enabled = false;            
            txtBankNumber.Enabled = false;
            txtAgentChainNumber.Enabled = false;
            txtAgentBankNumber.Enabled = false;

            //lstPlatform.BackColor = System.Drawing.Color.DarkGray;
            txtAgentBankNumber.BackColor = System.Drawing.Color.DarkGray;
            txtAgentChainNumber.BackColor = System.Drawing.Color.DarkGray;
            txtBankNumber.BackColor = System.Drawing.Color.DarkGray;
            txtLoginIDNumber.BackColor = System.Drawing.Color.DarkGray;
            txtMCCCategoryCode.BackColor = System.Drawing.Color.DarkGray;
            txtMerchantIDNumber.BackColor = System.Drawing.Color.DarkGray;
            txtMerchantNumber.BackColor = System.Drawing.Color.DarkGray;
            txtStoreNumber.BackColor = System.Drawing.Color.DarkGray;
            txtTerminalIDNumber.BackColor = System.Drawing.Color.DarkGray;
        }

    }//end function DisablePlatformFields

    //This function resets Platform controls
    public void ResetPlatformControls()
    {
        TextBox txtBox = new TextBox();
        for (int i = 0; i < pnlPlatform.Controls.Count; i++)
        {
            if (pnlPlatform.Controls[i].GetType() == txtBox.GetType())
            {
                txtBox = (TextBox)pnlPlatform.Controls[i];
                txtBox.Text = "";
            }
        }//end for        
    }

    //This function disables all controls
    protected void DisableControls()
    {        
        rdbNo.Checked = true;
        lstPlatform.Enabled = false;
        lstPlatform.SelectedIndex = 0;
        txtAgentBankNumber.Enabled = false;
        txtAgentChainNumber.Enabled = false;
        txtBankNumber.Enabled = false;        
        txtLoginIDNumber.Enabled = false;
        txtMCCCategoryCode.Enabled = false;
        txtMerchantIDNumber.Enabled = false;
        txtMerchantNumber.Enabled = false;
        txtStoreNumber.Enabled = false;
        txtTerminalIDNumber.Enabled = false;

        lstPlatform.BackColor = System.Drawing.Color.DarkGray;
        txtAgentBankNumber.BackColor = System.Drawing.Color.DarkGray;
        txtAgentChainNumber.BackColor = System.Drawing.Color.DarkGray;
        txtBankNumber.BackColor = System.Drawing.Color.DarkGray;
        txtLoginIDNumber.BackColor = System.Drawing.Color.DarkGray;
        txtMCCCategoryCode.BackColor = System.Drawing.Color.DarkGray;
        txtMerchantIDNumber.BackColor = System.Drawing.Color.DarkGray;
        txtMerchantNumber.BackColor = System.Drawing.Color.DarkGray;
        txtStoreNumber.BackColor = System.Drawing.Color.DarkGray;
        txtTerminalIDNumber.BackColor = System.Drawing.Color.DarkGray;


        txtAgentBankNumber.Text = "";
        txtAgentChainNumber.Text = "";
        txtBankNumber.Text = "";        
        txtLoginIDNumber.Text = "";
        txtMCCCategoryCode.Text = "";
        txtMerchantIDNumber.Text = "";
        txtMerchantNumber.Text = "";
        txtStoreNumber.Text = "";
        txtTerminalIDNumber.Text = "";
    }//end function disable all controls


    //The following functions enable/disable controls based on platform selected
    /*
    protected void SwitchPlatforms()
    {
        switch (lstPlatform.SelectedItem.Text.Trim())
        {
            case "Omaha":
                {
                    Omaha();
                }
                break;
            case "Nashville":
                {
                    Nashville();
                }
                break;
            case "Vital":
                {
                    Vital();
                }
                break;
            case "Global Payments":
                {
                    GlobalPayments();
                }
                break;
            case "Nova":
                {
                    Nova();
                }
                break;
        }//end switch
    }//end function switch platforms

    //This function enables/disables controls if Omaha is selected
    protected void Omaha()
    {
        txtMerchantNumber.Enabled = true;
        txtMerchantNumber.BackColor = System.Drawing.Color.White;
        txtMerchantIDNumber.Enabled = false;
        txtMerchantIDNumber.BackColor = System.Drawing.Color.DarkGray;
        txtMCCCategoryCode.Enabled = false;
        txtMCCCategoryCode.BackColor = System.Drawing.Color.DarkGray;
        txtBankNumber.Enabled = false;
        txtBankNumber.BackColor = System.Drawing.Color.DarkGray;
        txtAgentChainNumber.Enabled = false;
        txtAgentChainNumber.BackColor = System.Drawing.Color.DarkGray;
        txtAgentBankNumber.Enabled = false;
        txtAgentBankNumber.BackColor = System.Drawing.Color.DarkGray;
        txtLoginIDNumber.Enabled = false;
        txtLoginIDNumber.BackColor = System.Drawing.Color.DarkGray;
        txtStoreNumber.Enabled = false;
        txtStoreNumber.BackColor = System.Drawing.Color.DarkGray;
        txtTerminalIDNumber.Enabled = false;
        txtTerminalIDNumber.BackColor = System.Drawing.Color.DarkGray;

        txtMerchantNumber.Text = "";
        txtMerchantIDNumber.Text = "";
        txtMCCCategoryCode.Text = "";        
        txtBankNumber.Text = "";
        txtAgentChainNumber.Text = "";
        txtAgentBankNumber.Text = "";
        txtLoginIDNumber.Text = "";
        txtStoreNumber.Text = "";
        txtTerminalIDNumber.Text = "";
    }//end function Omaha

    //This function enables/disables controls if Nashville is selected
    protected void Nashville()
    {
        txtMerchantNumber.Enabled = true;
        txtMerchantNumber.BackColor = System.Drawing.Color.White;
        txtMerchantIDNumber.Enabled = true;
        txtMerchantIDNumber.BackColor = System.Drawing.Color.White;
        txtMCCCategoryCode.Enabled = false;
        txtMCCCategoryCode.BackColor = System.Drawing.Color.DarkGray;
        txtBankNumber.Enabled = false;
        txtBankNumber.BackColor = System.Drawing.Color.DarkGray;
        txtAgentChainNumber.Enabled = false;
        txtAgentChainNumber.BackColor = System.Drawing.Color.DarkGray;
        txtAgentBankNumber.Enabled = false;
        txtAgentBankNumber.BackColor = System.Drawing.Color.DarkGray;
        txtLoginIDNumber.Enabled = false;
        txtLoginIDNumber.BackColor = System.Drawing.Color.DarkGray;
        txtStoreNumber.Enabled = false;
        txtStoreNumber.BackColor = System.Drawing.Color.DarkGray;
        txtTerminalIDNumber.Enabled = true;
        txtTerminalIDNumber.BackColor = System.Drawing.Color.White;


        txtMerchantNumber.Text = "";
        txtMerchantIDNumber.Text = "";
        txtMCCCategoryCode.Text = "";        
        txtBankNumber.Text = "";
        txtAgentChainNumber.Text = "";
        txtAgentBankNumber.Text = "";
        txtLoginIDNumber.Text = "";
        txtStoreNumber.Text = "";
        txtTerminalIDNumber.Text = "";
    }//end function Nashville

    //This function enables/disables controls if Vital is selected
    protected void Vital()
    {
        txtMerchantNumber.Enabled = true;
        txtMerchantNumber.BackColor = System.Drawing.Color.White;
        txtMerchantIDNumber.Enabled = false;
        txtMerchantIDNumber.BackColor = System.Drawing.Color.DarkGray;
        txtMCCCategoryCode.Enabled = true;
        txtMCCCategoryCode.BackColor = System.Drawing.Color.White;        
        txtBankNumber.Enabled = true;
        txtBankNumber.BackColor = System.Drawing.Color.White;
        txtAgentChainNumber.Enabled = true;
        txtAgentChainNumber.BackColor = System.Drawing.Color.White;
        txtAgentBankNumber.Enabled = true;
        txtAgentBankNumber.BackColor = System.Drawing.Color.White;
        txtLoginIDNumber.Enabled = false;
        txtLoginIDNumber.BackColor = System.Drawing.Color.DarkGray;
        txtStoreNumber.Enabled = true;
        txtStoreNumber.BackColor = System.Drawing.Color.White;
        txtTerminalIDNumber.Enabled = true;
        txtTerminalIDNumber.BackColor = System.Drawing.Color.White;

        txtMerchantNumber.Enabled = true;
        txtMerchantIDNumber.Enabled = false;
        txtMCCCategoryCode.Enabled = true;        
        txtBankNumber.Enabled = true;
        txtAgentChainNumber.Enabled = true;
        txtAgentBankNumber.Enabled = true;
        txtLoginIDNumber.Enabled = false;
        txtStoreNumber.Enabled = true;
        txtTerminalIDNumber.Enabled = true;

        txtMerchantNumber.Text = "";
        txtMerchantIDNumber.Text = "";
        txtMCCCategoryCode.Text = "";
        txtBankNumber.Text = "";
        txtAgentChainNumber.Text = "";
        txtAgentBankNumber.Text = "";
        txtLoginIDNumber.Text = "";
        txtStoreNumber.Text = "";
        txtTerminalIDNumber.Text = "";
    }//end function Vital

    //This function enables/disables controls if GlobalPayments is selected
    protected void GlobalPayments()
    {
        txtMerchantNumber.Enabled = true;
        txtMerchantNumber.BackColor = System.Drawing.Color.White;
        txtMerchantIDNumber.Enabled = false;
        txtMerchantIDNumber.BackColor = System.Drawing.Color.DarkGray;
        txtMCCCategoryCode.Enabled = false;
        txtMCCCategoryCode.BackColor = System.Drawing.Color.DarkGray;        
        txtBankNumber.Enabled = true;
        txtBankNumber.BackColor = System.Drawing.Color.White;
        txtAgentChainNumber.Enabled = false;
        txtAgentChainNumber.BackColor = System.Drawing.Color.DarkGray;
        txtAgentBankNumber.Enabled = false;
        txtAgentBankNumber.BackColor = System.Drawing.Color.DarkGray;
        txtLoginIDNumber.Enabled = false;
        txtLoginIDNumber.BackColor = System.Drawing.Color.DarkGray;
        txtStoreNumber.Enabled = false;
        txtStoreNumber.BackColor = System.Drawing.Color.DarkGray;
        txtTerminalIDNumber.Enabled = false;
        txtTerminalIDNumber.BackColor = System.Drawing.Color.DarkGray;

        txtMerchantNumber.Text = "";
        txtMerchantIDNumber.Text = "";
        txtMCCCategoryCode.Text = "";        
        txtBankNumber.Text = "";
        txtAgentChainNumber.Text = "";
        txtAgentBankNumber.Text = "";
        txtLoginIDNumber.Text = "";
        txtStoreNumber.Text = "";
        txtTerminalIDNumber.Text = "";
    }//end function GlobalPayments

    //This function enables/disables controls if Nova is selected
    protected void Nova()
    {
        txtMerchantNumber.Enabled = true;
        txtMerchantNumber.BackColor = System.Drawing.Color.White;
        txtMerchantIDNumber.Enabled = false;
        txtMerchantIDNumber.BackColor = System.Drawing.Color.DarkGray;
        txtMCCCategoryCode.Enabled = false;
        txtMCCCategoryCode.BackColor = System.Drawing.Color.DarkGray;        
        txtBankNumber.Enabled = true;
        txtBankNumber.BackColor = System.Drawing.Color.White;
        txtAgentChainNumber.Enabled = false;
        txtAgentChainNumber.BackColor = System.Drawing.Color.DarkGray;
        txtAgentBankNumber.Enabled = false;
        txtAgentBankNumber.BackColor = System.Drawing.Color.DarkGray;
        txtLoginIDNumber.Enabled = true;
        txtLoginIDNumber.BackColor = System.Drawing.Color.White;
        txtStoreNumber.Enabled = false;
        txtStoreNumber.BackColor = System.Drawing.Color.DarkGray;
        txtTerminalIDNumber.Enabled = true;
        txtTerminalIDNumber.BackColor = System.Drawing.Color.White;

        txtMerchantNumber.Enabled = true;
        txtMerchantIDNumber.Enabled = false;
        txtMCCCategoryCode.Enabled = false;        
        txtBankNumber.Enabled = true;
        txtAgentChainNumber.Enabled = false;
        txtAgentBankNumber.Enabled = false;
        txtLoginIDNumber.Enabled = true;
        txtStoreNumber.Enabled = false;
        txtTerminalIDNumber.Enabled = true;

        txtMerchantNumber.Text = "";
        txtMerchantIDNumber.Text = "";
        txtMCCCategoryCode.Text = "";        
        txtBankNumber.Text = "";
        txtAgentChainNumber.Text = "";
        txtAgentBankNumber.Text = "";
        txtLoginIDNumber.Text = "";
        txtStoreNumber.Text = "";
        txtTerminalIDNumber.Text = "";
    }//end function Nova
    */
    //This function displays error messages
    protected void SetErrorMessage(string errText)
    {
        lblError.Visible = true;
        lblError.Text = errText;
    }//end function set error message
}
