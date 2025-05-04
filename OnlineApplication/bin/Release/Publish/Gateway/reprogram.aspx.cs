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
    bool merchAcctExist = false;
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
                        PopulatePage();
                        int AcctType = Convert.ToInt32(Session["AcctType"]);
                        if (AcctType == 4)
                        {
                            lblMessage.Visible = true;
                            //rdbYes.Enabled = false;
                            //rdbNo.Checked = true;
                            //DisableControls();
                        }
                    }
                    else
                    {
                        Response.Redirect("reprog.aspx", false);
                        SetErrorMessage("The status of your application prevents it from being edited");
                        btnSubmit.Enabled = false;
                    }
                }
                catch (Exception err)
                {
                    CreateOnlineAppLog Log = new CreateOnlineAppLog();
                    Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + Session["AppId"].ToString() + " - " + "Gateway reprogram.aspx Page Load Error - " + err.Message);
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
                {
                    rdbNo.Checked = true;
                    DisableControls();
                    ValidatePlatform.Enabled = false;
                }
                int index = lstPlatform.Items.IndexOf(lstPlatform.Items.FindByText(drReprogramInfo["Platform"].ToString().Trim()));
                lstPlatform.SelectedIndex = index;
                //SwitchPlatforms();
                
                txtMerchantNumber.Text = drReprogramInfo["MerchantNum"].ToString().Trim();
                txtMerchantIDNumber.Text = drReprogramInfo["MerchantID"].ToString().Trim();
                txtLoginIDNumber.Text = drReprogramInfo["LoginID"].ToString().Trim();
                txtTerminalIDNumber.Text = drReprogramInfo["TerminalID"].ToString().Trim();
                txtBankNumber.Text = drReprogramInfo["BankIDNum"].ToString().Trim();
                //txtBINNumber.Text = drReprogramInfo["BINNum"].ToString().Trim();
                txtAgentBankNumber.Text = drReprogramInfo["AgentBankIDNum"].ToString().Trim();
                txtAgentChainNumber.Text = drReprogramInfo["AgentChainNum"].ToString().Trim();
                txtMCCCategoryCode.Text = drReprogramInfo["MCCCategoryCode"].ToString().Trim();
                txtStoreNumber.Text = drReprogramInfo["StoreNum"].ToString().Trim();
                DisablePlatformFields();
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
                    {
                        bReprogram = true;
                        merchAcctExist = true;
                    }
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
                    if (merchAcctExist)
                    {
                        Response.Redirect("../notice.aspx", false);
                    }
                    else 
                    {
                        Response.Redirect("/Application/UpdateAcct.aspx", false);
                    }
                }//end if ValidateData
            }//end if page valid
            else
                DisablePlatformFields();
        }//end try
        catch (Exception err)
        {
            CreateOnlineAppLog Log = new CreateOnlineAppLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + Session["AppId"].ToString() + " - " + "Gateway reprogram.aspx Error - " + err.Message);
             SetErrorMessage("Error Processing Request. Please contact Technical Support: <a href='mailto:support@commercetech.com?Subject=Online Application Error'> support@commercetech.com. </a>.");
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
                if (txtBox.Enabled == true)
                {
                    if ((txtBox.Text.Contains("--")) || (txtBox.Text.Contains("#")) || (txtBox.Text.Contains(";")) || (txtBox.Text.Contains("'")) || (txtBox.Text.ToString().Trim() == ""))
                    {
                        txtBox.BackColor = System.Drawing.Color.Ivory;
                        txtBox.Focus();
                        SetErrorMessage("You cannot use apostrophe, # or semi-colons in any of the following fields.");
                        return false;
                    }
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
            merchAcctExist = true;
        }
        else
        {
            DisableControls();
            ValidatePlatform.Enabled = false;
            merchAcctExist = false;
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
            txtMerchantIDNumber.MaxLength = 11;
            RFMID.Enabled = true;

            txtTerminalIDNumber.Enabled = true;
            txtTerminalIDNumber.MaxLength = 11;
            RFTID.Enabled = true;

            txtMCCCategoryCode.MaxLength = 4;
            txtMCCCategoryCode.Enabled = true;
            RFMCC.Enabled = true;

            txtTerminalIDNumber.BackColor = System.Drawing.Color.White;
            txtMerchantIDNumber.BackColor = System.Drawing.Color.White;            
            txtMCCCategoryCode.BackColor = System.Drawing.Color.White;

            lblError.Text = "";

            if ((txtMerchantIDNumber.Text.Length < 6) || (txtMerchantIDNumber.Text.Length > 11))
            {
                lblError.Visible = true;
                lblError.Text += "Merchant ID should be between 6 and 11 digits.<br/>";
            }

            if ((txtTerminalIDNumber.Text.Length < 6) || (txtTerminalIDNumber.Text.Length > 11))
            {
                lblError.Visible = true;
                lblError.Text += "Terminal ID should be between 6 and 11 digits.<br/>";
            }

            if ((txtMCCCategoryCode.Text.Length < 4))
            {
                lblError.Visible = true;
                lblError.Text += "MCC Category Code should be 4 digits.<br/>";
            }

            txtMerchantNumber.Enabled = false;
            RFMerchantNum.Enabled = false;
            txtMerchantNumber.BackColor = System.Drawing.Color.DarkGray;
            txtAgentBankNumber.Enabled = false;
            RFABN.Enabled = false;
            txtAgentBankNumber.BackColor = System.Drawing.Color.DarkGray;
            txtAgentChainNumber.Enabled = false; 
            RFACN.Enabled = false;
            txtAgentChainNumber.BackColor = System.Drawing.Color.DarkGray;
            txtBankNumber.Enabled = false;
            RFBankNum.Enabled = false;
            txtBankNumber.BackColor = System.Drawing.Color.DarkGray;
            txtLoginIDNumber.Enabled = false;
            RFLoginID.Enabled = false;
            txtLoginIDNumber.BackColor = System.Drawing.Color.DarkGray;
            txtStoreNumber.Enabled = false;
            RFStoreNum.Enabled = false;
            txtStoreNumber.BackColor = System.Drawing.Color.DarkGray;              
            
        }//end if nashville
        else if (lstPlatform.SelectedItem.Text.Contains("Vital"))
        {
            txtMerchantIDNumber.Enabled = true;
            txtMerchantIDNumber.MaxLength = 12;
            RFMID.Enabled = true;

            txtBankNumber.Enabled = true;
            txtBankNumber.MaxLength = 6;
            RFBankNum.Enabled = true;

            txtAgentBankNumber.Enabled = true;
            txtAgentBankNumber.MaxLength = 6;
            RFABN.Enabled = true;

            txtAgentChainNumber.Enabled = true;
            txtAgentChainNumber.MaxLength = 6;
            RFACN.Enabled = true;

            txtMCCCategoryCode.Enabled = true;
            txtMCCCategoryCode.MaxLength = 4;
            RFMCC.Enabled = true;

            txtStoreNumber.Enabled = true;
            txtStoreNumber.MaxLength = 4;
            RFStoreNum.Enabled = true;

            txtTerminalIDNumber.Enabled = true;
            txtTerminalIDNumber.MaxLength = 4;
            RFTID.Enabled = true;

            txtAgentBankNumber.BackColor = System.Drawing.Color.White;
            txtAgentChainNumber.BackColor = System.Drawing.Color.White;
            txtBankNumber.BackColor = System.Drawing.Color.White;
            txtMCCCategoryCode.BackColor = System.Drawing.Color.White;
            txtMerchantIDNumber.BackColor = System.Drawing.Color.White;
            txtStoreNumber.BackColor = System.Drawing.Color.White;
            txtTerminalIDNumber.BackColor = System.Drawing.Color.White;

            lblError.Text = "";

            if ((txtMerchantIDNumber.Text.Length < 12) || (txtMerchantIDNumber.Text.Length > 12))
            {
                lblError.Visible = true;
                lblError.Text += "Merchant ID length must be 12 characters long.<br/>";
            }


            if ((txtTerminalIDNumber.Text.Length < 4) || (txtTerminalIDNumber.Text.Length > 4))
            {
                lblError.Visible = true;
                lblError.Text += "Terminal ID length must be 4 characters long.<br/>";
            }

            if ((txtBankNumber.Text.Length < 6) || (txtBankNumber.Text.Length > 6))
            {
                lblError.Visible = true;
                lblError.Text += "BIN Number length must be 6 characters long.<br/>";
            }

            if ((txtAgentBankNumber.Text.Length < 6) || (txtAgentBankNumber.Text.Length > 6))
            {
                lblError.Visible = true;
                lblError.Text += "Agent Bank Number length must be 6 characters long.<br/>";
            }

            if ((txtAgentChainNumber.Text.Length < 6) || (txtAgentChainNumber.Text.Length > 6))
            {
                lblError.Visible = true;
                lblError.Text += "Agent Chain Number length must be 6 characters long.<br/>";
            }

            if ((txtMCCCategoryCode.Text.Length < 4) || (txtMCCCategoryCode.Text.Length > 4))
            {
                lblError.Visible = true;
                lblError.Text += "MCC Code length must be 4 characters long.<br/>";
            }            

            if ((txtStoreNumber.Text.Length < 4) || (txtStoreNumber.Text.Length > 4))
            {
                lblError.Visible = true;
                lblError.Text += "Store Number length must be 4 characters long.<br/>";
            }

            txtLoginIDNumber.Enabled = false;
            RFLoginID.Enabled = false;
            txtLoginIDNumber.BackColor = System.Drawing.Color.DarkGray;
            txtMerchantNumber.Enabled = false;
            RFMerchantNum.Enabled = false;
            txtMerchantNumber.BackColor = System.Drawing.Color.DarkGray;                  
            
        }//end if vital
        else if (lstPlatform.SelectedItem.Text.Contains("Nova"))
        {            
            txtBankNumber.Enabled = true;
            txtBankNumber.MaxLength = 6;
            RFBankNum.Enabled = true;

            txtTerminalIDNumber.Enabled = true;
            txtTerminalIDNumber.MaxLength = 16;
            RFTID.Enabled = true;

            txtMCCCategoryCode.MaxLength = 4;
            txtMCCCategoryCode.Enabled = true;
            RFMCC.Enabled = true;

            txtBankNumber.BackColor = System.Drawing.Color.White;
            txtTerminalIDNumber.BackColor = System.Drawing.Color.White;
            txtMCCCategoryCode.BackColor = System.Drawing.Color.White;

            lblError.Text = "";

            if ((txtTerminalIDNumber.Text.Length < 6) || (txtTerminalIDNumber.Text.Length > 16))
            {
                lblError.Visible = true;
                lblError.Text += "Terminal ID length must be between 6 and 16 characters.<br/>";
            }

            if ((txtBankNumber.Text.Length < 6) || (txtBankNumber.Text.Length > 6))
            {
                lblError.Visible = true;
                lblError.Text += "Bank Number length must be 6 characters long.<br/>";
            }

            if ((txtMCCCategoryCode.Text.Length < 4) || (txtMCCCategoryCode.Text.Length > 4))
            {
                lblError.Visible = true;
                lblError.Text += "MCC Category Code length must be between 6 and 16 characters.<br/>";
            }

            txtAgentBankNumber.Enabled = false;
            RFABN.Enabled = false;
            txtAgentBankNumber.BackColor = System.Drawing.Color.DarkGray;
            txtAgentChainNumber.Enabled = false;
            RFACN.Enabled = false;
            txtAgentChainNumber.BackColor = System.Drawing.Color.DarkGray;
            txtLoginIDNumber.Enabled = false;
            RFLoginID.Enabled = false;
            txtLoginIDNumber.BackColor = System.Drawing.Color.DarkGray;
            txtMerchantIDNumber.Enabled = false;
            RFMID.Enabled = false;
            txtMerchantIDNumber.BackColor = System.Drawing.Color.DarkGray;
            txtMerchantNumber.Enabled = false;
            RFMerchantNum.Enabled = false;
            txtMerchantNumber.BackColor = System.Drawing.Color.DarkGray;
            txtStoreNumber.Enabled = false;
            RFStoreNum.Enabled = false;
            txtStoreNumber.BackColor = System.Drawing.Color.DarkGray;
            
        }//end if nova
        else if (lstPlatform.SelectedItem.Text.Contains("Omaha"))
        {
            txtMerchantNumber.MaxLength = 16;
            txtMerchantNumber.Enabled = true;
            RFMerchantNum.Enabled = true;

            txtMCCCategoryCode.Enabled = true;
            txtMCCCategoryCode.MaxLength = 4;
            RFMCC.Enabled = true;

            txtMerchantNumber.BackColor = System.Drawing.Color.White;
            txtMCCCategoryCode.BackColor = System.Drawing.Color.White;

            lblError.Text = "";
            if ((txtMerchantNumber.Text.Length < 7) || (txtMerchantNumber.Text.Length > 16))
            {
                lblError.Visible = true;
                lblError.Text += "Merchant Number length must be between 7 and 16 characters.<br/>";
            }
            if ((txtMCCCategoryCode.Text.Length < 4) || (txtMCCCategoryCode.Text.Length > 4))
            {
                lblError.Visible = true;
                lblError.Text += "MCC Code length must be 4 characters long.<br/>";
            }

            txtAgentBankNumber.Enabled = false;       
            RFABN.Enabled = false;      
            txtAgentBankNumber.BackColor = System.Drawing.Color.DarkGray;
            txtAgentChainNumber.Enabled = false;
            RFACN.Enabled = false;      
            txtAgentChainNumber.BackColor = System.Drawing.Color.DarkGray;
            txtBankNumber.Enabled = false;
            RFBankNum.Enabled = false;
            txtBankNumber.BackColor = System.Drawing.Color.DarkGray;
            txtLoginIDNumber.Enabled = false;
            RFLoginID.Enabled = false;
            txtLoginIDNumber.BackColor = System.Drawing.Color.DarkGray;
            txtMerchantIDNumber.Enabled = false;
            RFMID.Enabled = false;
            txtMerchantIDNumber.BackColor = System.Drawing.Color.DarkGray;
            txtStoreNumber.Enabled = false;
            RFStoreNum.Enabled = false;
            txtStoreNumber.BackColor = System.Drawing.Color.DarkGray;
            txtTerminalIDNumber.Enabled = false;
            RFTID.Enabled = false;
            txtTerminalIDNumber.BackColor = System.Drawing.Color.DarkGray;
        }
        else if (lstPlatform.SelectedItem.Text.Contains("Global Payments"))
        {
            txtMerchantIDNumber.Enabled = true;
            RFMID.Enabled = true;
            txtMerchantIDNumber.MaxLength = 15;

            txtBankNumber.Enabled = true;
            RFBankNum.Enabled = true;
            txtBankNumber.MaxLength = 6;

            txtMCCCategoryCode.MaxLength = 4;
            txtMCCCategoryCode.Enabled = true;
            RFMCC.Enabled = true;

            txtMerchantIDNumber.BackColor = System.Drawing.Color.White;
            txtBankNumber.BackColor = System.Drawing.Color.White;
            txtMCCCategoryCode.BackColor = System.Drawing.Color.White;

            lblError.Text = "";

            if ((txtMerchantIDNumber.Text.Length < 3) || (txtMerchantIDNumber.Text.Length > 15))
            {
                lblError.Visible = true;
                lblError.Text += "Merchant ID length must be between 3 and 15 characters.<br/>";
            }

            if ((txtBankNumber.Text.Length < 4) || (txtBankNumber.Text.Length > 6))
            {
                lblError.Visible = true;
                lblError.Text += "Bank Number length must be between 4 and 6 characters.<br/>";
            }

            if ((txtMCCCategoryCode.Text.Length < 4) || (txtMCCCategoryCode.Text.Length > 4))
            {
                lblError.Visible = true;
                lblError.Text += "MCC Code length must be 4 characters long.<br/>";
            }

            txtMerchantNumber.Enabled = false;
            RFMerchantNum.Enabled = false;
            txtMerchantNumber.BackColor = System.Drawing.Color.DarkGray;
            txtMCCCategoryCode.Enabled = false;
            RFACN.Enabled = false;
            txtAgentChainNumber.BackColor = System.Drawing.Color.DarkGray;
            txtAgentBankNumber.Enabled = false;
            RFABN.Enabled = false;
            txtAgentBankNumber.BackColor = System.Drawing.Color.DarkGray;
            txtLoginIDNumber.Enabled = false;
            RFLoginID.Enabled = false;
            txtLoginIDNumber.BackColor = System.Drawing.Color.DarkGray;
            txtStoreNumber.Enabled = false;
            RFStoreNum.Enabled = false;
            txtStoreNumber.BackColor = System.Drawing.Color.DarkGray;
            txtTerminalIDNumber.Enabled = false;
            RFTID.Enabled = false;
            txtTerminalIDNumber.BackColor = System.Drawing.Color.DarkGray;
        }
        else if (lstPlatform.SelectedItem.Text.Contains("Paymentech"))
        {
            txtMerchantIDNumber.MaxLength = 12;
            txtMerchantIDNumber.Enabled = true;
            RFMID.Enabled = true;

            txtTerminalIDNumber.MaxLength = 3;
            txtTerminalIDNumber.Enabled = true;
            RFTID.Enabled = true;

            txtStoreNumber.MaxLength = 4;
            txtStoreNumber.Enabled = true;
            RFStoreNum.Enabled = true;

            txtMCCCategoryCode.MaxLength = 4;
            txtMCCCategoryCode.Enabled = true;
            RFMCC.Enabled = true;

            txtMerchantIDNumber.BackColor = System.Drawing.Color.White;
            txtTerminalIDNumber.BackColor = System.Drawing.Color.White;
            txtStoreNumber.BackColor = System.Drawing.Color.White;
            txtMCCCategoryCode.BackColor = System.Drawing.Color.White;

            lblError.Text = "";
            if ((txtMerchantIDNumber.Text.Length < 11) || (txtMerchantIDNumber.Text.Length > 12))
            {
                lblError.Visible = true;
                lblError.Text += "Merchant ID Number length must be 11 or 12 characters long.<br/>";
            }

            if ((txtTerminalIDNumber.Text.Length < 2) || (txtTerminalIDNumber.Text.Length > 3))
            {
                lblError.Visible = true;
                lblError.Text += "Terminal ID length must be 2 or 3 characters long. <br/>";
            }

            if ((txtMCCCategoryCode.Text.Length < 4) || (txtMCCCategoryCode.Text.Length > 4))
            {
                lblError.Visible = true;
                lblError.Text += "MCC Code length must be 4 characters long.<br/>";
            }

            if ((txtStoreNumber.Text.Length < 3) || (txtStoreNumber.Text.Length > 4))
            {
                lblError.Visible = true;
                lblError.Text += "Store/Client Number length must be 3 or 4 characters long.<br/>";
            }


            txtAgentBankNumber.Enabled = false;
            RFABN.Enabled = false;
            txtAgentBankNumber.BackColor = System.Drawing.Color.DarkGray;
            txtAgentChainNumber.Enabled = false;
            RFACN.Enabled = false;
            txtAgentChainNumber.BackColor = System.Drawing.Color.DarkGray;
            txtBankNumber.Enabled = false;
            RFBankNum.Enabled = false;
            txtBankNumber.BackColor = System.Drawing.Color.DarkGray;
            txtLoginIDNumber.Enabled = false;
            RFLoginID.Enabled = false;
            txtLoginIDNumber.BackColor = System.Drawing.Color.DarkGray;
            txtMerchantNumber.Enabled = false;
            RFMerchantNum.Enabled = false;
            txtMerchantNumber.BackColor = System.Drawing.Color.DarkGray;

        }//end if Paymentech
        else if (lstPlatform.SelectedItem.Text.Contains("North"))
        {
            txtMerchantNumber.Enabled = true;
            txtMerchantNumber.MaxLength = 12;
            RFMerchantNum.Enabled = true;

            txtLoginIDNumber.Enabled = true;
            RFLoginID.Enabled = true;

            txtBankNumber.Enabled = true;
            txtBankNumber.MaxLength = 6;
            RFBankNum.Enabled = true;

            txtAgentBankNumber.Enabled = true;
            txtAgentBankNumber.MaxLength = 6;
            RFABN.Enabled = true;

            txtAgentChainNumber.Enabled = true;
            txtAgentChainNumber.MaxLength = 6;
            RFACN.Enabled = true;

            txtMCCCategoryCode.Enabled = true;
            txtMCCCategoryCode.MaxLength = 4;
            RFMCC.Enabled = true;

            txtStoreNumber.Enabled = true;
            txtStoreNumber.MaxLength = 4;
            RFStoreNum.Enabled = true;

            txtTerminalIDNumber.Enabled = true;
            txtTerminalIDNumber.MaxLength = 4;
            RFTID.Enabled = true;

            txtMerchantIDNumber.Enabled = true;
            RFMID.Enabled = true;

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
            RFABN.Enabled = false;
            txtAgentBankNumber.BackColor = System.Drawing.Color.DarkGray;
            RFACN.Enabled = false;
            txtAgentChainNumber.BackColor = System.Drawing.Color.DarkGray;
            RFBankNum.Enabled = false;
            txtBankNumber.BackColor = System.Drawing.Color.DarkGray;
            RFLoginID.Enabled = false;
            txtLoginIDNumber.BackColor = System.Drawing.Color.DarkGray;
            RFMCC.Enabled = false;
            txtMCCCategoryCode.BackColor = System.Drawing.Color.DarkGray;
            RFMID.Enabled = false;
            txtMerchantIDNumber.BackColor = System.Drawing.Color.DarkGray;
            RFMerchantNum.Enabled = false;
            txtMerchantNumber.BackColor = System.Drawing.Color.DarkGray;
            RFStoreNum.Enabled = false;
            txtStoreNumber.BackColor = System.Drawing.Color.DarkGray;
            RFTID.Enabled = false;
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
        RFABN.Enabled = false;
        txtAgentChainNumber.Enabled = false;
        RFACN.Enabled = false;
        txtBankNumber.Enabled = false;
        RFBankNum.Enabled = false;
        txtLoginIDNumber.Enabled = false;
        RFLoginID.Enabled = false;
        txtMCCCategoryCode.Enabled = false;
        RFMCC.Enabled = false;
        txtMerchantIDNumber.Enabled = false;
        RFMID.Enabled = false;
        txtMerchantNumber.Enabled = false;
        RFMerchantNum.Enabled = false;
        txtStoreNumber.Enabled = false;
        RFStoreNum.Enabled = false;
        txtTerminalIDNumber.Enabled = false;
        RFTID.Enabled = false;

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
