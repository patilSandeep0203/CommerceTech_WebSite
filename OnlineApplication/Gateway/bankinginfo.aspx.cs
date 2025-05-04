using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Security.Cryptography;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using OnlineAppClassLibrary;
using System.Text.RegularExpressions;
using DataLayer;

public partial class Gateway_bankinginfo : System.Web.UI.Page
{
    private string strBankRoutingNum = "-";
    private string strBankAccountNum = "-";
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
                        //Check if banking info has already been entered. If yes, 
                        //then check if user wants to edit info
                        //else redirect to banking.aspx.
                        OtherInfo other = new OtherInfo(AppId);
                        if (other.CheckOtherInfoComplete("Gateway") > 0)
                        {
                            if (Request.Params.Get("EditBanking") != null)
                            {
                                pnlBanking.Visible = true;
                                pnlNBC.Visible = false;                                
                            }
                            else if (Request.Params.Get("EditProcessing") != null)
                            {
                                pnlBanking.Visible = false;
                                pnlNBC.Visible = true;
                            }
                            else
                                Response.Redirect("banking.aspx", false);
                        }
                        PopulatePage();
                    }
                    else
                    {
                        Response.Redirect("banking.aspx", false);
                        SetErrorMessage("The status of your application prevents it from being edited");
                        btnSubmit.Enabled = false;
                    }
                }
                catch (Exception err)
                {
                    CreateOnlineAppLog Log = new CreateOnlineAppLog();
                    Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + Session["AppId"].ToString() + " - " + "Gateway bankinginfo.aspx Page Load Error - " + err.Message);
                    SetErrorMessage("Error Processing Request. Please contact Technical Support: <a href=mailto:support@commercetech.com?Subject=Online App Error, App ID: " + Session["AppId"].ToString() + "> support@commercetech.com. </a>: <a href=mailto:support@commercetech.com?Subject=Online App Error, App ID: " + Session["AppId"].ToString() + "> support@commercetech.com. </a>");
                }
            }
        }//end if
    }

    //This function checks if the application is locked
    protected bool Locked()
    {
        if (Session["Locked"].ToString().Trim() == "Yes")
            return true;
        else
            return false;
    }

    //This function populates fields with data from Banking info and other info tables
    protected void PopulatePage()
    {
        //Get states
        CommonListData States = new CommonListData();
        DataSet dsStates = States.GetCommonData("States");
        if (dsStates.Tables["States"].Rows.Count > 0)
        {
            lstState.DataSource = dsStates.Tables["States"];
            lstState.DataTextField = "StateID";
            lstState.DataValueField = "StateID";
            lstState.DataBind();

        }//end if count not 0

        //Get Banks
        CommonListData Banks = new CommonListData();
        DataSet dsBanks = Banks.GetCommonData("Banks");
        if (dsBanks.Tables["Banks"].Rows.Count > 0)
        {
            lstBankName.DataSource = dsBanks.Tables["Banks"];
            lstBankName.DataTextField = "BankName";
            lstBankName.DataValueField = "BankName";
            lstBankName.DataBind();

        }//end if count not 0

        //Get Banking Info
        BankingInfo BankInfo = new BankingInfo(AppId);
        DataSet ds = BankInfo.GetBankingInfo();
        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow drBank = ds.Tables[0].Rows[0];
            lstBankName.SelectedIndex = lstBankName.Items.IndexOf(lstBankName.Items.FindByText(drBank["BankName"].ToString().Trim()));
            if (drBank["BankName"].ToString().Trim().ToLower().Contains("other"))
            {
                txtOtherBank.Enabled = true;
                txtOtherBank.Text = drBank["BankName"].ToString().Trim();
            }
            txtBankAddress.Text = drBank["BankAddress"].ToString().Trim();
            txtCity.Text = drBank["BankCity"].ToString().Trim();
            txtRegion.Text = drBank["BankRegion"].ToString().Trim();
            txtZipCode.Text = drBank["BankZip"].ToString().Trim();
            lstState.SelectedIndex = lstState.Items.IndexOf(lstState.Items.FindByText(drBank["BankState"].ToString().Trim()));
            txtPhone.Text = drBank["BankPhone"].ToString().Trim();
            txtNameOnChecking.Text = drBank["NameOnCheckingAcct"].ToString().Trim();

            if (drBank["BankRoutingNumber"].ToString().Trim() == "")
                txtBankRoutingNumber.Enabled = true;
            else //contains previous data, do not enable editing and show the Edit link
            {
                txtBankRoutingNumber.Text = "xxxxxx" + drBank["BankRoutingNumber"].ToString().Trim();
                txtBankRoutingNumber.Enabled = false;
                lnkRoutingNum.Visible = true;
                ValidateRoutingNo.Enabled = false;
                RERoutingNum.Enabled = false;
            }
            //if no previous data
            if (drBank["BankAccountNumber"].ToString().Trim() == "")
                txtAcctNumber.Enabled = true;
            else //contains previous data, do not enable editing and show the Edit link
            {
                txtAcctNumber.Text = "xxxxxx" + drBank["BankAccountNumber"].ToString().Trim();
                txtAcctNumber.Enabled = false;
                lnkAccountNum.Visible = true;
                ValidateAcctNo.Enabled = false;
                REAcctNum.Enabled = false;
            }
        }//end if count not 0

        //Get info from otherinfo table
        OtherInfo OtherInfoTable = new OtherInfo(AppId);
        DataSet dtOtherInfo = OtherInfoTable.GetOtherInfo();
        if (dtOtherInfo.Tables[0].Rows.Count > 0)
        {
            DataRow dsOtherInfo = dtOtherInfo.Tables[0].Rows[0];

            int index = lstDiscover.Items.IndexOf(lstDiscover.Items.FindByText(dsOtherInfo["DiscoverApplied"].ToString().Trim()));
            lstDiscover.SelectedIndex = index;
            index = lstAmex.Items.IndexOf(lstAmex.Items.FindByText(dsOtherInfo["AmexApplied"].ToString().Trim()));
            lstAmex.SelectedIndex = index;
            index = lstJCB.Items.IndexOf(lstJCB.Items.FindByText(dsOtherInfo["JCBApplied"].ToString().Trim()));
            lstJCB.SelectedIndex = index;

            if (dsOtherInfo["DiscoverApplied"].ToString().Trim() == "Yes - Existing")
            {
                pnlDiscoverNum.Visible = true;
                txtDiscoverNum.Enabled = true;
                txtDiscoverNum.BackColor = System.Drawing.Color.White;
                ValidateDiscoverNum.Enabled = true;
            }
            else
            {
                pnlDiscoverNum.Visible = false;
                txtDiscoverNum.Enabled = false;
                txtDiscoverNum.BackColor = System.Drawing.Color.DarkGray;
                ValidateDiscoverNum.Enabled = false;
            }

            if (dsOtherInfo["AmexApplied"].ToString().Trim() == "Yes - Existing")
            {
                pnlAmexNum.Visible = true;
                txtAmexNum.Enabled = true;
                txtAmexNum.BackColor = System.Drawing.Color.White;
                ValidateAmexNum.Enabled = true;
            }
            else
            {
                pnlAmexNum.Visible = false;
                txtAmexNum.Enabled = false;
                txtAmexNum.BackColor = System.Drawing.Color.DarkGray;
                ValidateAmexNum.Enabled = false;
            }

            if (dsOtherInfo["JCBApplied"].ToString().Trim() == "Yes - Existing")
            {
                pnlJCBNum.Visible = true;
                txtJCBNum.Enabled = true;
                txtJCBNum.BackColor = System.Drawing.Color.White;
                ValidateJCBNum.Enabled = true;
            }
            else
            {
                pnlJCBNum.Visible = false;
                txtJCBNum.Enabled = false;
                txtJCBNum.BackColor = System.Drawing.Color.DarkGray;
                ValidateJCBNum.Enabled = false;
            }
        }//end if
        else
        {
            pnlDiscoverNum.Visible = false;
            pnlAmexNum.Visible = false;
            pnlJCBNum.Visible = false;
            ValidateJCBNum.Enabled = false;
            ValidateAmexNum.Enabled = false;
            ValidateDiscoverNum.Enabled = false;
        }
    }//end function populate page

    //This function handles submit button click event
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        btnSave_Click(sender, e);

        try
        {
            if (Page.IsValid)
            {
                //Validate data
                bool retValidate = ValidateData();
                if (retValidate)
                {
                    if (Request.Params.Get("EditBanking") != null)
                    {
                        if ((txtBankRoutingNumber.Text.Trim().Length < 9) || (txtBankRoutingNumber.Text.Trim().Length > 9))
                        {
                            SetErrorMessage("Length of the Bank Routing Number must be 9 characters long.");
                        }
                        else
                        {
                            bool RetValBanking = ValidateRoutingNumber();
                            if (RetValBanking)
                            {
                                InsertUpdateBankingInformation(true);
                                UpdateCommonInformation();
                                Response.Redirect("reprogram.aspx", false);
                            }
                        }
                    }
                    else if (Request.Params.Get("EditProcessing") != null)
                    {
                        InsertUpdateOtherInformation(true);
                        UpdateCommonInformation();
                        Response.Redirect("reprogram.aspx", false);
                    }
                    else
                    {
                        bool RetValBanking = ValidateRoutingNumber();
                        if (RetValBanking)
                        {
                            InsertUpdateBankingInformation(true);
                            InsertUpdateOtherInformation(true);
                            UpdateCommonInformation();
                            Response.Redirect("reprogram.aspx", false);
                        }
                        else
                            SetErrorMessage("Length of the Bank Routing Number must be 9 characters long.");    
                    }
                }//end if validate user        
            }//end if page valid            
        }//end try
        catch (Exception err)
        {
            CreateOnlineAppLog Log = new CreateOnlineAppLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + Session["AppId"].ToString() + " - " + "Gateway bankinginfo.aspx Error - " + err.Message);
            SetErrorMessage("Error Processing Request. Please contact Technical Support: <a href=mailto:support@commercetech.com?Subject=Online App Error, App ID: " + Session["AppId"].ToString() + "> support@commercetech.com. </a> at <a href=mailto:support@commercetech.com?Subject=Online App Error, App ID: " + Session["AppId"].ToString() + "> support@commercetech.com. </a>");
        }        
    }//end submit button click

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            ValidateSummary.Enabled = false;

            string strPhone = txtPhone.Text.Trim();
            //try
            //{

            Regex regexObj = new Regex(@"[^0-9]");

            if (strPhone != "")
            {
                strPhone = regexObj.Replace(strPhone, "").Trim();

                strPhone = strPhone.Insert(3, "-");

                strPhone = strPhone.Insert(7, "-");

                txtPhone.Text = strPhone;
            }

            //Validate data for commas, apostrophes
            bool retValidate = ValidateData();

            if ((retValidate) && (ValidateRoutingNumber() == true))
            {
                InsertUpdateBankingInformation(false);
                InsertUpdateOtherInformation(false);
                UpdateCommonInformationSave();
                SetErrorMessage("Information saved. You may proceed to the next page by navigating through the top menu.");
            }
            else
                lblError.Visible = true;

        }//end try
        catch (Exception err)
        {
            CreateOnlineAppLog Log = new CreateOnlineAppLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + Session["AppId"].ToString() + " - " + "Merchant bankinginfo.aspx Page - " + err.Message);
            SetErrorMessage("Error Processing Request. Please contact Technical Support: <a href='mailto:support@commercetech.com?Subject=Online Application Error'> support@commercetech.com. </a>.");
        }
    }

    //This function checks length of routing number
    protected bool ValidateRoutingNumber()
    {
        if ((txtBankRoutingNumber.Text.Trim().Length < 9) && txtBankRoutingNumber.Text.Trim() != "")
        {
            SetErrorMessage("Routing Number must be exactly 9 characters");
            return false;
        }
        return true;
    }

        //This function updates page count, last modified
    protected void UpdateCommonInformation()
    {
        //Set last modified date in newapp table
        CommonFunctions UpdateGeneralInfo = new CommonFunctions(AppId);
        UpdateGeneralInfo.UpdateLastModified();

        UpdateGeneralInfo.SetGWPageCount();

        LogBL LogData = new LogBL(AppId);
        LogData.InsertLogData(Convert.ToInt32(Session["AffiliateID"]), "Gateway Banking Information Updated");
    }

    protected void UpdateCommonInformationSave()
    {
        //Set last modified date in newapp table
        CommonFunctions UpdateGeneralInfo = new CommonFunctions(AppId);
        UpdateGeneralInfo.UpdateLastModified();

        UpdateGeneralInfo.SetGWPageCount();

        LogBL LogData = new LogBL(AppId);
        LogData.InsertLogData(Convert.ToInt32(Session["AffiliateID"]), "Gateway Banking Information Saved");
    }

    //This function Inserts/Updates Banking Information
    protected void InsertUpdateBankingInformation(bool bComplete)
    {
        if (txtAcctNumber.Enabled == true)
            strBankAccountNum = txtAcctNumber.Text.Trim();
        if (txtBankRoutingNumber.Enabled == true)
            strBankRoutingNum = txtBankRoutingNumber.Text.Trim();

        BankingInfo BankInfo = new BankingInfo(AppId);
        bool retVal = BankInfo.UpdateGWBankingInfo(lstBankName.SelectedItem.Text.Trim().Replace("'", strReplace),
            txtOtherBank.Text.Trim().Replace("'", strReplace), txtBankAddress.Text.Trim().Replace("'", strReplace), txtZipCode.Text.Trim().Replace("'", strReplace),
            txtCity.Text.Trim().Replace("'", strReplace), lstState.SelectedItem.Text.Trim().Replace("'", strReplace), txtNameOnChecking.Text.Trim().Replace("'", strReplace), strBankAccountNum,
            strBankRoutingNum, txtPhone.Text.Trim().Replace("'", strReplace), txtRegion.Text.Trim().Replace("'", strReplace), bComplete);
    }

    //This function Inserts/Updates Other Information
    protected void InsertUpdateOtherInformation(bool bComplete)
    {
        OtherInfo OtherInfoTable = new OtherInfo(AppId);
        bool retVal2 = OtherInfoTable.UpdateGatewayOtherInfo( lstDiscover.SelectedItem.Text.Trim().Replace("'", strReplace),
            lstAmex.SelectedItem.Text.Trim().Replace("'", strReplace), lstJCB.SelectedItem.Text.Trim().Replace("'", strReplace), txtDiscoverNum.Text.Trim(),
            txtAmexNum.Text.Trim(), txtJCBNum.Text.Trim(), bComplete);
    }

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
                    SetErrorMessage("You cannot use apostrophes, #, or semi-colons in any of the following fields.");
                    return false;
                }
            }
        }
        return true;
    }//end function validate data

    //This function handles the bank list selection changed event
    protected void lstBankName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lstBankName.SelectedItem.Text.Trim() == "OTHER")
        {
            txtOtherBank.Enabled = true;
            txtOtherBank.BackColor = System.Drawing.Color.White;
            ValidateOtherBank.Enabled = true;
        }
        else
        {
            txtOtherBank.Enabled = false;
            txtOtherBank.BackColor = System.Drawing.Color.DarkGray;
            txtOtherBank.Text = "";
            ValidateOtherBank.Enabled = false;
        }
    }//end function selection changed

    //This function displays error messages
    protected void SetErrorMessage(string errText)
    {
        lblError.Visible = true;
        lblError.Text = errText;
    }//end function set error message

    protected void lstDiscover_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lstDiscover.SelectedItem.Text.Trim() == "Yes - Existing")
        {
            pnlDiscoverNum.Visible = true;
            txtDiscoverNum.Enabled = true;
            txtDiscoverNum.BackColor = System.Drawing.Color.White;
            ValidateDiscoverNum.Enabled = true;
        }
        else
        {
            pnlDiscoverNum.Visible = false;
            txtDiscoverNum.Text = "";
            txtDiscoverNum.Enabled = false;
            txtDiscoverNum.BackColor = System.Drawing.Color.DarkGray;
            ValidateDiscoverNum.Enabled = false;
        }
    }

    protected void lstAmex_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lstAmex.SelectedItem.Text.Trim() == "Yes - Existing")
        {
            pnlAmexNum.Visible = true;
            txtAmexNum.Enabled = true;
            txtAmexNum.BackColor = System.Drawing.Color.White;
            ValidateAmexNum.Enabled = true;
        }
        else
        {
            pnlAmexNum.Visible = false;
            txtAmexNum.Text = "";
            txtAmexNum.Enabled = false;
            txtAmexNum.BackColor = System.Drawing.Color.DarkGray;
            ValidateAmexNum.Enabled = false;
        }
    }

    protected void lstJCB_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lstJCB.SelectedItem.Text.Trim() == "Yes - Existing")
        {
            pnlJCBNum.Visible = true;
            txtJCBNum.Enabled = true;
            txtJCBNum.BackColor = System.Drawing.Color.White;
            ValidateJCBNum.Enabled = true;
        }
        else
        {
            pnlJCBNum.Visible = false;
            txtJCBNum.Text = "";
            txtJCBNum.Enabled = false;
            txtJCBNum.BackColor = System.Drawing.Color.DarkGray;
            ValidateJCBNum.Enabled = false;
        }
    }

    protected void lnkRoutingNum_Click(object sender, EventArgs e)
    {
        lnkRoutingNum.Visible = false;
        ValidateRoutingNo.Enabled = true;
        txtBankRoutingNumber.Enabled = true;
        txtBankRoutingNumber.Text = "";
        RERoutingNum.Enabled = true;
    }

    protected void lnkAccountNum_Click(object sender, EventArgs e)
    {
        lnkAccountNum.Visible = false;
        ValidateCheckingAcct.Enabled = true;
        txtAcctNumber.Enabled = true;
        txtAcctNumber.Text = "";
        REAcctNum.Enabled = true;
    }
}
