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
//using System.Windows.Forms;
using System.Text.RegularExpressions;
using DataLayer;

public partial class Merchant_businessinfo : System.Web.UI.Page
{
    private static string strReplace = "";
    private string strFederalTaxID = "";
    private string strPrevMerchantNum = "";
    private static int AppId = 0;
    private static int PID = 0;
    private static string nextNavigatePage = "";
    private bool textChanged = false;

    DataSet dsBusinessInfo = null;
    DataRow drBusinessInfo = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        lblError.Visible = false;

        if (Session.IsNewSession)
            Response.Redirect("/Application/default.aspx?SessionExpired=True");

        Style TextArea = new Style();
        TextArea.Width = new Unit(220);
        TextArea.Height = new Unit(50);
        TextArea.Font.Size = FontUnit.Point(8);
        txtReasonForLeaving.ApplyStyle(TextArea);
        txtProductsSold.ApplyStyle(TextArea);
        txtAddlComments.ApplyStyle(TextArea);

        Style errLabel = new Style();
        errLabel.BackColor = System.Drawing.Color.FromArgb(255, 192, 192);
        errLabel.ForeColor = System.Drawing.Color.Black;
        errLabel.BorderColor = System.Drawing.Color.Red;
        errLabel.BorderStyle = System.Web.UI.WebControls.BorderStyle.Solid;
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
                        //Check if business info has already been entered. If yes, 
                        //then check if user wants to edit info
                        //else redirect to business.aspx.
                        BusinessInfo BusinessComplete = new BusinessInfo(AppId);
                        if (BusinessComplete.CheckBusinessComplete("Merchant") > 0)
                        {
                            if (Request.Params.Get("EditBusiness") != null)
                            {
                                //Do not display the federal tax id, merchant number                               
                            }
                            else
                                Response.Redirect("business.aspx", false);
                        }
                        PopulatePage();
                    }
                    else
                    {
                        Response.Redirect("business.aspx", false);
                        SetErrorMessage("The status of your application prevents it from being edited");
                        btnSubmit.Enabled = false;
                    }
                }
                catch (Exception err)
                {
                    CreateOnlineAppLog Log = new CreateOnlineAppLog();
                    Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + Session["AppId"].ToString() + " - " + "Merchant businessinfo.aspx Page Load Error - " + err.Message);
                    SetErrorMessage("Error Processing Request. Please contact Technical Support: <a href='mailto:support@commercetech.com?Subject=Online Application Error'> support@commercetech.com. </a>.");
                }
            }
        }
    }

    //This function checks if the application is locked
    protected bool Locked()
    {
        if (Session["Locked"].ToString().Trim() == "Yes")
            return true;
        else
            return false;
    }

    //This function populates page
    protected void PopulatePage()
    {
        lnkFederalTaxID.Visible = false;
        lnkPrevMerchNum.Visible = false;
        //Get Previous processor list        
        CommonListData PrevProcessorList = new CommonListData();
        DataSet dsPrevProcessorList = PrevProcessorList.GetCommonData("PrevProcessorList");
        if (dsPrevProcessorList.Tables["PrevProcessorList"].Rows.Count > 0)
        {
            lstProcessor.DataSource = dsPrevProcessorList.Tables["PrevProcessorList"];
            lstProcessor.DataTextField = "ProcessorName";
            lstProcessor.DataValueField = "ProcessorName";
            lstProcessor.DataBind();
        }//end if count not 0
        lstProcessor.Items.Add("");
        lstProcessor.Items.Add("OTHER");


        //Get states
        CommonListData Data = new CommonListData();
        DataSet dsStates = Data.GetCommonData("States");
        if (dsStates.Tables["States"].Rows.Count > 0)
        {
            lstState.DataSource = dsStates.Tables["States"];
            lstState.DataTextField = "StateID";
            lstState.DataValueField = "StateID";
            lstState.DataBind();
            lstState.Items.Add("");
        }//end if count not 0

        //Get Countries
        DataSet dsCountry = Data.GetCommonData("Countries");
        if (dsCountry.Tables["Countries"].Rows.Count > 0)
        {
            lstCountry.DataSource = dsCountry.Tables["Countries"];
            lstCountry.DataTextField = "Country";
            lstCountry.DataValueField = "Country";
            lstCountry.DataBind();
        }//end if count not 0
        lstCountry.SelectedItem.Text = "United States";
        lstCountry.SelectedItem.Value = "United States";

        PID = Convert.ToInt32(Session["PID"]);
        if ((PID == 178) || (PID == 182) || (PID == 243) || (PID == 247) || (PID == 196))
        {
            lstState.Enabled = false;
            ValidateState.Enabled = false;
            txtFederalID.Enabled = false;
            ValidateTaxID.Enabled = false;
        }

        if (PID == 190)
        {
            txtFederalID.Enabled = false;
            ValidateTaxID.Enabled = false;
        }

        string[] arrOwnership = { "", "Corporation", "Government", "International Organization", "Legal/Medical Corp.", "LLC", "Non-Profit", "Others", "Partnership", "Sole Proprietorship", "Tax Exempt", "Associtation/Estate/Trust" };
        for (int i = 0; i < arrOwnership.Length; i++)
        {
            ListItem lstItem = new ListItem();
            lstItem.Text = arrOwnership[i];
            lstItem.Value = arrOwnership[i];
            lstOwnership.Items.Add(lstItem);
        }

        //Get business info from onlineappbusinessinfo
        BusinessInfo Business = new BusinessInfo(AppId);
        dsBusinessInfo = Business.GetBusinessInfo();
        if (dsBusinessInfo.Tables[0].Rows.Count > 0)
        {
            drBusinessInfo = dsBusinessInfo.Tables[0].Rows[0];

            int BusAddrSame = Convert.ToInt32(drBusinessInfo["BillingAddrSame"]);
            if (BusAddrSame == 0)
                chkBillingAddrSame.Checked = true;
            //DisableAddressControls();

            txtAddress.Text = drBusinessInfo["BillingAddress"].ToString().Trim();
            txtAddress2.Text = drBusinessInfo["BillingAddress2"].ToString().Trim();
            txtCity.Text = drBusinessInfo["BillingCity"].ToString().Trim();
            txtZipCode.Text = drBusinessInfo["BillingZipCode"].ToString().Trim();
            txtRegion.Text = drBusinessInfo["BillingRegion"].ToString().Trim();

            if (drBusinessInfo["BillingCountry"].ToString() == "United States")
                lstCountry.SelectedValue = "United States";
            else if (drBusinessInfo["BillingCountry"].ToString() == "Canada")
            { lstCountry.SelectedValue = "Canada"; }
            else
            {
                txtFederalID.Enabled = false;
                ValidateTaxID.Enabled = false;
                lstState.Enabled = false;
                ValidateState.Enabled = false;
                lstCountry.SelectedIndex = lstCountry.Items.IndexOf(lstCountry.Items.FindByText(drBusinessInfo["BillingCountry"].ToString().Trim()));
                lblIntlZip.Visible = true;
            }
            lstState.SelectedIndex = lstState.Items.IndexOf(lstState.Items.FindByText(drBusinessInfo["BillingState"].ToString().Trim()));
            int index = lstOwnership.Items.IndexOf(lstOwnership.Items.FindByText(drBusinessInfo["LegalStatus"].ToString().Trim()));
            lstOwnership.SelectedIndex = index;
            txtYIB.Text = drBusinessInfo["YearsinBusiness"].ToString().Trim();
            txtMIB.Text = drBusinessInfo["MonthsinBusiness"].ToString().Trim();

            if (lstCountry.SelectedValue == "United States")
            {
                lblStateStar.Visible = true;
                lblFedTaxIDStar.Visible = true;
                lblIntlZip.Visible = true;
                lstState.Enabled = true;
                ValidateState.Enabled = false;
                txtRegion.Enabled = false;
            }
            else if (lstCountry.SelectedValue == "Canada")
            {
                lblStateStar.Visible = true;
                lblFedTaxIDStar.Visible = true;
                lstState.Enabled = true;
                ValidateState.Enabled = false;
                lblIntlZip.Visible = true;
                txtRegion.Enabled = false;
            }
            else
            {
                lblStateStar.Visible = false;
                lstState.SelectedValue = "";
                lstState.Enabled = false;
                ValidateState.Enabled = false;
                lblFedTaxIDStar.Visible = false;
                txtFederalID.Enabled = false;
                ValidateTaxID.Enabled = false;
                lblIntlZip.Visible = true;
                txtRegion.Enabled = true;
            }

            //if no previous data
            if ((drBusinessInfo["FederalTaxID"].ToString().Trim() == "") && ((drBusinessInfo["BillingCountry"].ToString() == "Canada") || (drBusinessInfo["BillingCountry"].ToString() == "United States")))
            {
                txtFederalID.Enabled = true;
            }
            else if ((drBusinessInfo["FederalTaxID"].ToString().Trim() != "") && ((drBusinessInfo["BillingCountry"].ToString() == "Canada") || (drBusinessInfo["BillingCountry"].ToString() == "United States")))//contains previous data, do not enable editing and show the Edit link
            {
                txtFederalID.Text = "xxxxxx" + drBusinessInfo["FederalTaxID"].ToString().Trim();
                lnkFederalTaxID.Visible = true;
                txtFederalID.Enabled = false;
                ValidateTaxID.Enabled = false;
                RETaxID.Enabled = false;
            }
            else
            {
                lnkFederalTaxID.Visible = false;
                txtFederalID.Enabled = false;
                ValidateTaxID.Enabled = false;
                RETaxID.Enabled = false;
            }

            txtNumDays.Text = drBusinessInfo["NumDaysDelivered"].ToString().Trim();
            txtNumLocs.Text = drBusinessInfo["NumberofLocations"].ToString().Trim();
            txtOtherProcessor.Text = drBusinessInfo["OtherProcessor"].ToString().Trim();
            txtProductsSold.Text = drBusinessInfo["ProductSold"].ToString().Trim();
            txtReasonForLeaving.Text = drBusinessInfo["ReasonforLeaving"].ToString().Trim();
            txtAddlComments.Text = drBusinessInfo["AddlComments"].ToString().Trim();
            index = lstRefundPolicy.Items.IndexOf(lstRefundPolicy.Items.FindByText(drBusinessInfo["RefundPolicy"].ToString().Trim()));
            lstRefundPolicy.SelectedIndex = index;
            txtRefundOther.Text = drBusinessInfo["OtherRefund"].ToString().Trim();
            if (drBusinessInfo["Bankruptcy"].ToString().Trim() == "Yes")
                lstBankruptcy.SelectedValue = "Yes";
            else if (drBusinessInfo["Bankruptcy"].ToString().Trim() == "No")
                lstBankruptcy.SelectedValue = "No";

            if (drBusinessInfo["CTMF"].ToString().Trim() == "Yes")
                lstCTMF.SelectedValue = "Yes";
            else if (drBusinessInfo["CTMF"].ToString().Trim() == "No")
                lstCTMF.SelectedValue = "No";

            if (drBusinessInfo["PrevProcessed"].ToString().Trim() == "Yes")
            {
                pnlPrevProcessed.Visible = true;
                lstPrevProcessed.SelectedIndex = 1;

                //if no previous data
                if (drBusinessInfo["PrevMerchantAcctNo"].ToString().Trim() == "")
                    txtPrevMerchant.Enabled = true;
                else //contains previous data, do not enable editing and show the Edit link
                {
                    txtPrevMerchant.Text = "xxxxxxxxxxxxxxx" + drBusinessInfo["PrevMerchantAcctNo"].ToString().Trim();
                    txtPrevMerchant.Enabled = false;
                    lnkPrevMerchNum.Visible = true;
                    ValidatePrevMerchant.Enabled = false;
                    REPrevMerchant.Enabled = false;
                }

            }
            else if (drBusinessInfo["PrevProcessed"].ToString().Trim() == "No")
            {
                pnlPrevProcessed.Visible = false;
                lstPrevProcessed.SelectedIndex = 2;
            }
            else
            {
                pnlPrevProcessed.Visible = false;
                lstPrevProcessed.SelectedIndex = 0;
            }

            index = lstProcessor.Items.IndexOf(lstProcessor.Items.FindByText(drBusinessInfo["PrevProcessor"].ToString().Trim()));
            lstProcessor.SelectedIndex = index;

            if (drBusinessInfo["PrevProcessed"].ToString().Trim() == "Yes")
            {
                EnableControl(true);
                if (lstProcessor.SelectedItem.Text.Trim() == "OTHER")
                {
                    txtOtherProcessor.Enabled = true;
                    txtOtherProcessor.BackColor = System.Drawing.Color.White;
                    ValidateOtherProcessor.Enabled = true;
                }
                else
                {
                    txtOtherProcessor.Text = "";
                    txtOtherProcessor.Enabled = false;
                    txtOtherProcessor.BackColor = System.Drawing.Color.DarkGray;
                    ValidateOtherProcessor.Enabled = false;
                }
                lstProcessor.BackColor = System.Drawing.Color.White;
                //txtPrevMerchant.BackColor = System.Drawing.Color.White;
                txtReasonForLeaving.BackColor = System.Drawing.Color.White;
                ValidateReasonLeaving.Enabled = true;
                ValidatePrevMerchant.Enabled = true;
            }
            else
            {
                EnableControl(false);
                txtOtherProcessor.Enabled = false;
                txtOtherProcessor.Text = "";
                txtOtherProcessor.BackColor = System.Drawing.Color.DarkGray;
                lstProcessor.BackColor = System.Drawing.Color.DarkGray;
                txtPrevMerchant.BackColor = System.Drawing.Color.DarkGray;
                txtReasonForLeaving.BackColor = System.Drawing.Color.DarkGray;
                ValidateReasonLeaving.Enabled = false;
                ValidatePrevMerchant.Enabled = false;
                ValidateOtherProcessor.Enabled = false;
                txtOtherProcessor.Enabled = false;
            }

            if ((drBusinessInfo["RefundPolicy"].ToString().Trim() == "Other") || (drBusinessInfo["RefundPolicy"].ToString().Trim() == "OTHER"))
            {
                txtRefundOther.Enabled = true;
                txtRefundOther.BackColor = System.Drawing.Color.White;
                ValidateOtherRefund.Enabled = true;
            }
            else
            {
                txtRefundOther.BackColor = System.Drawing.Color.DarkGray;
                txtRefundOther.Enabled = false;
                ValidateOtherRefund.Enabled = false;
            }

        }//end if business table count not 0
        else
        {
            ValidateOtherRefund.Enabled = false;
            ValidateOtherProcessor.Enabled = false;
            txtRefundOther.Enabled = false;
            txtRefundOther.BackColor = System.Drawing.Color.DarkGray;
            txtOtherProcessor.Enabled = false;
            txtOtherProcessor.BackColor = System.Drawing.Color.DarkGray;
        }
    }//end function populate

    protected void textChangedEventHandler(object sender, EventArgs e)
    {
        textChanged = true;
    }

    //This function disables billing address controls
    protected void DisableAddressControls()
    {
        txtAddress.Enabled = false;
        txtAddress2.Enabled = false;
        txtCity.Enabled = false;
        txtZipCode.Enabled = false;
        txtRegion.Enabled = false;
        lstState.Enabled = false;
        lstCountry.Enabled = false;
    }//end function DisableAddressControls
    /*
    bool allowSpace = false;

    // Restricts the entry of characters to digits (including hex), the negative sign,
    // the decimal point, and editing keystrokes (backspace).
    protected void KeyPressFedTaxID(KeyPressEventArgs e)
    {
        base.OnKeyPress(e);

        NumberFormatInfo numberFormatInfo = System.Globalization.CultureInfo.CurrentCulture.NumberFormat;
        string decimalSeparator = numberFormatInfo.NumberDecimalSeparator;
        string groupSeparator = numberFormatInfo.NumberGroupSeparator;
        string negativeSign = numberFormatInfo.NegativeSign;

        string keyInput = e.KeyChar.ToString();

        if (Char.IsDigit(e.KeyChar))
        {
            // Digits are OK
        }
        else if (keyInput.Equals(decimalSeparator) || keyInput.Equals(groupSeparator) ||
         keyInput.Equals(negativeSign))
        {
            // Decimal separator is OK
        }
        else if (e.KeyChar == '\b')
        {
            // Backspace key is OK
        }
        //    else if ((ModifierKeys & (Keys.Control | Keys.Alt)) != 0)
        //    {
        //     // Let the edit control handle control and alt key combinations
        //    }
        else if (this.allowSpace && e.KeyChar == ' ')
        {

        }
        else
        {
            // Swallow this invalid key and beep
            e.Handled = true;
            //    MessageBeep();
        }
    }

    public int IntValue
    {
        get
        {
            return Int32.Parse(this.Text);
        }
    }

    public decimal DecimalValue
    {
        get
        {
            return Decimal.Parse(this.Text);
        }
    }

    public bool AllowSpace
    {
        set
        {
            this.allowSpace = value;
        }

        get
        {
            return this.allowSpace;
        }
    }*/

    //This function populates address based on same billing check box is checked
    protected void PopulateAddress()
    {
        CompanyInfo Company = new CompanyInfo(AppId);
        DataSet dsCompanyInfo = Company.GetCompanyInfo();
        if (dsCompanyInfo.Tables[0].Rows.Count > 0)
        {
            DataRow drCompanyInfo = dsCompanyInfo.Tables["OnlineAppCompanyInfo"].Rows[0];
            txtAddress.Text = drCompanyInfo["Address"].ToString().Trim();
            txtAddress2.Text = drCompanyInfo["Address2"].ToString().Trim();
            txtCity.Text = drCompanyInfo["City"].ToString().Trim();
            txtZipCode.Text = drCompanyInfo["ZipCode"].ToString().Trim();
            txtRegion.Text = drCompanyInfo["Region"].ToString().Trim();
            int index = lstState.Items.IndexOf(lstState.Items.FindByText(drCompanyInfo["State"].ToString().Trim()));
            lstState.SelectedIndex = index;

            if (drCompanyInfo["Country"].ToString() == "United States")
                lstCountry.SelectedValue = "United States";
            else if (drCompanyInfo["Country"].ToString() == "Canada")
            { lstCountry.SelectedValue = "Canada"; }
            else
            {
                lstState.Enabled = false;
                ValidateState.Enabled = false;
                lstCountry.SelectedValue = drCompanyInfo["Country"].ToString().Trim();
            }

        }//end if count not 0        

    }//end function populate address

    protected void btnPOS2013Yes_Click(object sender, EventArgs e)
    {
        Response.Redirect("ContactPOS2013.aspx", false);
    }

    protected void btnPOS2013No_Click(object sender, EventArgs e)
    {
        Response.Redirect("https://merchant.intuit.com/signup/?a=chkt&api_key=inTuIt&b=posmswoc&offer_code=posms_023_n-445-16463", false);
    }

    //This function handles the submit button click event
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        // Call the save functionality before performing full validation
        btnSave_Click(sender, e);

        CreatePDF proc = new CreatePDF(AppId);
        string processor = proc.ReturnProcessorName();
        string pid = proc.ReturnPackageId();

        if (txtFederalID.Enabled == true)
        {
            string strFedTaxID = txtFederalID.Text.Trim();
            //try
            //{
            Regex regexObj = new Regex(@"[^0-9]");

            strFedTaxID = regexObj.Replace(strFedTaxID, "").Trim();

            txtFederalID.Text = strFedTaxID;

            RETaxID.Enabled = false;
            //}
            //catch (ArgumentException ex)
            //{
            // Syntax error in the regular expression
            //}
        }

        if (txtPrevMerchant.Enabled == true)
        {
            string strPrevMerchant = txtPrevMerchant.Text.Trim();
            //try
            //{
            Regex regexObj = new Regex(@"[^0-9]");

            strPrevMerchant = regexObj.Replace(strPrevMerchant, "").Trim();

            txtPrevMerchant.Text = strPrevMerchant;

            REPrevMerchant.Enabled = false;
            //}
            //catch (ArgumentException ex)
            //{
            // Syntax error in the regular expression
            //}
        }

        ValidateSummary.Enabled = true;
        try
        {
            if (Page.IsValid)
            {
                //Validate data
                bool retValidate = ValidateData();
                if (retValidate)
                {
                    if ((txtFederalID.Text.Trim().Length != 9) && (lstCountry.SelectedItem.Text == "United States"))
                        SetErrorMessage("The Federal Tax ID/ Social Security Number must be 9 characters long. (Ex. 123456789)");
                    else
                    {
                        BusinessInfo Business = new BusinessInfo(AppId);

                        if (txtFederalID.Enabled == true)
                            strFederalTaxID = txtFederalID.Text.Trim().Replace("'", strReplace);
                        else //set to value to denote to Database NOT to Update
                            strFederalTaxID = "-";

                        if (txtPrevMerchant.Enabled == true)
                            strPrevMerchantNum = txtPrevMerchant.Text.Trim().Replace("'", strReplace);
                        else
                            strPrevMerchantNum = "-";

                        int BillingAddrSame = 1;
                        if (chkBillingAddrSame.Checked)
                            BillingAddrSame = 0;

                        bool retVal = Business.UpdateBusinessInfo(
                            txtAddress.Text.Trim().Replace("'", strReplace),
                            txtAddress2.Text.Trim().Replace("'", strReplace),
                            txtCity.Text.Trim().Replace("'", strReplace),
                            lstState.SelectedValue.Trim().Replace("'", strReplace),
                            txtRegion.Text.Trim().Replace("'", strReplace),
                            txtZipCode.Text.Trim().Replace("'", strReplace),
                            lstCountry.SelectedValue.Trim().Replace("'", strReplace),
                            strFederalTaxID,
                            txtYIB.Text.Trim().Replace("'", strReplace),
                            txtMIB.Text.Trim().Replace("'", strReplace),
                            txtNumLocs.Text.Trim().Replace("'", strReplace),
                            lstOwnership.SelectedValue.Trim().Replace("'", strReplace),
                            txtProductsSold.Text.Trim().Replace("'", strReplace),
                            txtNumDays.Text.Trim().Replace("'", strReplace),
                            txtAddlComments.Text.Trim().Replace("'", strReplace),
                            lstRefundPolicy.SelectedValue.Trim().Replace("'", strReplace),
                            txtRefundOther.Text.Trim().Replace("'", strReplace),
                            lstBankruptcy.SelectedValue.Trim().Replace("'", strReplace),
                            lstPrevProcessed.SelectedValue.Trim().Replace("'", strReplace),
                            lstProcessor.SelectedValue.Trim().Replace("'", strReplace),
                            txtOtherProcessor.Text.Trim().Replace("'", strReplace),
                            strPrevMerchantNum,
                            BillingAddrSame,
                            txtReasonForLeaving.Text.Trim().Replace("'", strReplace),
                            lstCTMF.Text.Trim().Replace("'", strReplace),
                            true);

                        //Set last modified date in newapp table
                        CommonFunctions GeneralInfo = new CommonFunctions(AppId);
                        GeneralInfo.SetPageCount();
                        if (textChanged == true)
                        {
                            GeneralInfo.UpdateLastModified();
                            LogBL LogData = new LogBL(AppId);
                            LogData.InsertLogData(Convert.ToInt32(Session["AffiliateID"]), "Merchant Business Information Updated");
                        }

                        OnlineAppProfile Profile = new OnlineAppProfile(AppId);
                        int AcctType = 0;

                        DataSet ds = Profile.GetProfileData();
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            AcctType = Convert.ToInt32(dr["AcctType"]);
                        }

                        NewAppInfo AppInfo = new NewAppInfo(AppId);
                        string Status = AppInfo.ReturnStatus();

                        if ((Status.ToLower().Contains("completed")) && (AcctType == 4))
                        {
                            AppInfo.UpdateStatus(Status, "Gateway");
                        }

                        if ((pid == "254") && (Status.ToLower().Contains("completed")))
                        {
                            Response.Redirect("ContactPOS2013.aspx", false);
                            //lblUser.Text = "Are you using or planing to use Intuit QuickBooks POS V.11 or 2013 for this account?";
                            //ModalPopupExtender1.Show();
                            //ClientScript.RegisterStartupScript(this.GetType(), "PopupScript", "ConfirmPOS2013();", true);
                        }
                        else
                        {
                            AppInfo.checkPageRedirect();
                        }
                        //Response.Redirect("principalinfo.aspx", false);
                    }
                }//end if ValidateData
            }//end if page valid
        }//end try
        catch (Exception err)
        {
            CreateOnlineAppLog Log = new CreateOnlineAppLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + Session["AppId"].ToString() + " - " + "Merchant businessinfo.aspx Error - " + err.Message);
            SetErrorMessage("Error Processing Request. Please contact Technical Support: <a href='mailto:support@commercetech.com?Subject=Online Application Error'> support@commercetech.com. </a>.");
        }
    }
    //end function submit button click

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

    //This function handles billing address check changed event
    protected void chk_CheckedChanged(object sender, EventArgs e)
    {
        if (chkBillingAddrSame.Checked)
        {
            //DisableAddressControls();
            PopulateAddress();
            txtAddress.Enabled = true;
            txtAddress2.Enabled = true;
            txtCity.Enabled = true;
            txtZipCode.Enabled = true;
            txtRegion.Enabled = true;
            lstState.Enabled = true;
            lstCountry.Enabled = true;
        }
        else
        {
            PopulateAddress();
            txtAddress.Enabled = true;
            txtAddress2.Enabled = true;
            txtCity.Enabled = true;
            txtZipCode.Enabled = true;
            txtRegion.Enabled = true;
            lstState.Enabled = true;
            lstCountry.Enabled = true;
            /*
            txtAddress.Text = "";
            txtAddress2.Text = "";
            txtCity.Text = "";            
            lstCountry.SelectedValue = "United States";
            txtRegion.Text = "";
            txtZipCode.Text = "";*/
        }

        textChangedEventHandler(sender, e);
    }//end function billing address check changed

    //This function handles previous processed selection changed event
    protected void lstPrevProcessed_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lstPrevProcessed.SelectedItem.Text == "Yes")
        {
            pnlPrevProcessed.Visible = true;
            int index = lstProcessor.Items.IndexOf(lstProcessor.Items.FindByText(""));
            lstProcessor.SelectedIndex = index;
            EnableControl(true);
            lstProcessor.BackColor = System.Drawing.Color.White;
            txtPrevMerchant.BackColor = System.Drawing.Color.White;
            txtReasonForLeaving.BackColor = System.Drawing.Color.White;
            ValidateReasonLeaving.Enabled = true;
            ValidatePrevMerchant.Enabled = true;
        }
        else
        {
            pnlPrevProcessed.Visible = false;
            int index = lstProcessor.Items.IndexOf(lstProcessor.Items.FindByText(""));
            lstProcessor.SelectedIndex = index;
            EnableControl(false);
            txtOtherProcessor.Text = "";
            txtOtherProcessor.Enabled = false;
            txtOtherProcessor.BackColor = System.Drawing.Color.DarkGray;
            lstProcessor.BackColor = System.Drawing.Color.DarkGray;
            txtPrevMerchant.BackColor = System.Drawing.Color.DarkGray;
            txtPrevMerchant.Text = "";
            txtReasonForLeaving.BackColor = System.Drawing.Color.DarkGray;
            txtReasonForLeaving.Text = "";
            ValidateReasonLeaving.Enabled = false;
            ValidatePrevMerchant.Enabled = false;
            ValidateOtherProcessor.Enabled = false;
        }


        textChangedEventHandler(sender, e);
    }//end function prev processed selection changed

    //This function sets enabled attribute
    protected void EnableControl(bool flag)
    {
        lstProcessor.Enabled = flag;
        txtReasonForLeaving.Enabled = flag;
        //txtPrevMerchant.Enabled = flag;
    }

    //This function handles refund policy selection changed event

    protected void lstRefundPolicy_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lstRefundPolicy.SelectedItem.Text.Trim() == "Other")
        {
            txtRefundOther.Enabled = true;
            txtRefundOther.BackColor = System.Drawing.Color.White;
            ValidateOtherRefund.Enabled = true;
        }
        else
        {
            txtRefundOther.BackColor = System.Drawing.Color.DarkGray;
            txtRefundOther.Enabled = false;
            txtRefundOther.Text = "";
            ValidateOtherRefund.Enabled = false;
        }

        textChangedEventHandler(sender, e);
    }//end function refund policy selection changed


    //This function handles processor list selection changed event
    protected void lstProcessor_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lstProcessor.SelectedItem.Text.Trim() != "OTHER")
        {
            txtOtherProcessor.Enabled = false;
            txtOtherProcessor.Text = "";
            txtOtherProcessor.BackColor = System.Drawing.Color.DarkGray;
            ValidateOtherProcessor.Enabled = false;
        }
        else
        {
            txtOtherProcessor.Enabled = true;
            txtOtherProcessor.BackColor = System.Drawing.Color.White;
            ValidateOtherProcessor.Enabled = true;
        }

        textChangedEventHandler(sender, e);
    }//end function processor list selection changed

    //This function displays error messages
    protected void SetErrorMessage(string errText)
    {
        lblError.Visible = true;
        lblError.Text = errText;
    }//end function set error message
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            /*
            if (txtFederalID.Enabled == true)
                strFederalTaxID = txtFederalID.Text.Trim().Replace("'", strReplace);
            else //set to value to denote to Database NOT to Update
                strFederalTaxID = "-";

            if (txtPrevMerchant.Enabled == true)
                strPrevMerchantNum = txtPrevMerchant.Text.Trim().Replace("'", strReplace);
            else
                strPrevMerchantNum = "-";*/
            if (txtFederalID.Enabled == true)
            {
                string strFedTaxID = txtFederalID.Text.Trim();
                //try
                //{
                Regex regexObj = new Regex(@"[^0-9]");

                strFedTaxID = regexObj.Replace(strFedTaxID, "").Trim();

                txtFederalID.Text = strFedTaxID;

                RETaxID.Enabled = false;

                //}
                //catch (ArgumentException ex)
                //{
                // Syntax error in the regular expression
                //}

                /*
                if (strFedTaxID.Trim().Length == 9)
                {

                    RETaxID.Enabled = false;
                }
                else
                {

                    RETaxID.Enabled = true;

                }*/
            }

            if (txtPrevMerchant.Enabled == true)
            {
                string strPrevMerchant = txtPrevMerchant.Text.Trim();
                //try
                //{
                Regex regexObj = new Regex(@"[^0-9]");

                strPrevMerchant = regexObj.Replace(strPrevMerchant, "").Trim();

                txtPrevMerchant.Text = strPrevMerchant;

                REPrevMerchant.Enabled = false;

                //}
                //catch (ArgumentException ex)
                //{
                // Syntax error in the regular expression
                //}

                /*
                if (strPrevMerchant.Trim().Length == 16)
                {

                    REPrevMerchant.Enabled = false;
                }
                else
                {

                    REPrevMerchant.Enabled = true;

                }*/
            }



            ValidateSummary.Enabled = false;
            //Validate data
            bool retValidate = ValidateData();
            if (retValidate)
            {

                //  SetErrorMessage("The Federal Tax ID/ Social Security Number must be 9 characters long. (Ex. 123456789)");
                //else
                {
                    BusinessInfo Business = new BusinessInfo(AppId);

                    if (txtFederalID.Enabled == true)
                        strFederalTaxID = txtFederalID.Text.Trim().Replace("'", strReplace);
                    else //set to value to denote to Database NOT to Update
                        strFederalTaxID = "-";

                    if (txtPrevMerchant.Enabled == true)
                        strPrevMerchantNum = txtPrevMerchant.Text.Trim().Replace("'", strReplace);
                    else
                        strPrevMerchantNum = "-";

                    int BillingAddrSame = 1;
                    if (chkBillingAddrSame.Checked)
                        BillingAddrSame = 0;
                    bool retVal = Business.UpdateBusinessInfo(txtAddress.Text.Trim().Replace("'", strReplace), txtAddress2.Text.Trim().Replace("'", strReplace), txtCity.Text.Trim().Replace("'", strReplace),
                        lstState.SelectedValue.Trim().Replace("'", strReplace), txtRegion.Text.Trim().Replace("'", strReplace), txtZipCode.Text.Trim().Replace("'", strReplace), lstCountry.SelectedValue.Trim().Replace("'", strReplace),
                        strFederalTaxID, txtYIB.Text.Trim().Replace("'", strReplace), txtMIB.Text.Trim().Replace("'", strReplace), txtNumLocs.Text.Trim().Replace("'", strReplace),
                        lstOwnership.SelectedValue.Trim().Replace("'", strReplace), txtProductsSold.Text.Trim().Replace("'", strReplace), txtNumDays.Text.Trim().Replace("'", strReplace), txtAddlComments.Text.Trim().Replace("'", strReplace),
                        lstRefundPolicy.SelectedValue.Trim().Replace("'", strReplace), txtRefundOther.Text.Trim().Replace("'", strReplace), lstBankruptcy.SelectedValue.Trim().Replace("'", strReplace),
                        lstPrevProcessed.SelectedValue.Trim().Replace("'", strReplace), lstProcessor.SelectedValue.Trim().Replace("'", strReplace), txtOtherProcessor.Text.Trim().Replace("'", strReplace),
                        strPrevMerchantNum, BillingAddrSame, txtReasonForLeaving.Text.Trim().Replace("'", strReplace), lstCTMF.Text.Trim().Replace("'", strReplace),
                        false);

                    //if ((txtFederalID.Text.Trim().Length != 9) && (lstCountry.SelectedItem.Text == "United States"))
                }

                //Set last modified date in newapp table

                /*
                if (textChanged == true)
                {
                    CommonFunctions GeneralInfo = new CommonFunctions(AppId);
                    GeneralInfo.UpdateLastModified();
                    GeneralInfo.SetPageCount();
                    LogBL LogData = new LogBL(AppId);
                    LogData.InsertLogData(Convert.ToInt32(Session["AffiliateID"]), "Merchant Business Information Updated");
                }*/
                //SetErrorMessage("Information saved. You may proceed to the next page by navigating through the top menu.");

            }//end if Validate

        }//end try
        catch (Exception err)
        {
            CreateOnlineAppLog Log = new CreateOnlineAppLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + Session["AppId"].ToString() + " - " + "Merchant businessinfo.aspx Error - " + err.Message);
            SetErrorMessage("Error Processing Request. Please contact Technical Support: <a href='mailto:support@commercetech.com?Subject=Online Application Error'> support@commercetech.com. </a>.");
        }
    }
    protected void lstCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lstCountry.SelectedItem.Text.Trim() == "United States")
        {
            lblStateStar.Visible = true;
            lblFedTaxIDStar.Visible = true;
            lblIntlZip.Visible = true;
            lstState.Enabled = true;
            ValidateState.Enabled = false;
            txtRegion.Enabled = false;
        }
        else if (lstCountry.SelectedItem.Text.Trim() == "Canada")
        {
            lblStateStar.Visible = true;
            lblFedTaxIDStar.Visible = true;
            lstState.Enabled = true;
            ValidateState.Enabled = false;
            lblIntlZip.Visible = true;
            txtRegion.Enabled = false;
        }
        else
        {
            lblStateStar.Visible = false;
            lstState.SelectedValue = "";
            lstState.Enabled = false;
            ValidateState.Enabled = false;
            lblFedTaxIDStar.Visible = false;
            txtFederalID.Enabled = false;
            ValidateTaxID.Enabled = false;
            lblIntlZip.Visible = true;
            txtRegion.Enabled = true;
        }
    }
    protected void lnkFederalTaxID_Click(object sender, EventArgs e)
    {
        BusinessTable BusinessTable = new BusinessTable();
        dsBusinessInfo = BusinessTable.GetBusinessFullInfo(AppId);

        if (dsBusinessInfo.Tables[0].Rows.Count > 0)
        {
            drBusinessInfo = dsBusinessInfo.Tables[0].Rows[0];

            if ((drBusinessInfo["FederalTaxID"].ToString().Trim() == "") && ((drBusinessInfo["BillingCountry"].ToString() == "Canada") || (drBusinessInfo["BillingCountry"].ToString() == "United States")))
            {
                txtFederalID.Enabled = true;
                lnkFederalTaxID.Visible = false;
                ValidateTaxID.Enabled = true;
                //RETaxID.Enabled = true;
            }
            else if ((drBusinessInfo["FederalTaxID"].ToString().Trim() != "") && ((drBusinessInfo["BillingCountry"].ToString() == "Canada") || (drBusinessInfo["BillingCountry"].ToString() == "United States")))//contains previous data, do not enable editing and show the Edit link
            {
                txtFederalID.Text = drBusinessInfo["FederalTaxID"].ToString().Trim();
                lnkFederalTaxID.Visible = false;
                txtFederalID.Enabled = true;
                ValidateTaxID.Enabled = true;
                RETaxID.Enabled = false;
            }
            else
            {
                txtFederalID.Enabled = true;
                lnkFederalTaxID.Visible = false;
                ValidateTaxID.Enabled = true;
            }
        }

        textChangedEventHandler(sender, e);

    }
    protected void lnkPrevMerchNum_Click(object sender, EventArgs e)
    {
        BusinessTable BusinessTable = new BusinessTable();
        dsBusinessInfo = BusinessTable.GetBusinessFullInfo(AppId);

        if (dsBusinessInfo.Tables[0].Rows.Count > 0)
        {
            drBusinessInfo = dsBusinessInfo.Tables[0].Rows[0];

            if ((drBusinessInfo["PrevMerchantAcctNo"].ToString().Trim() == "") && ((drBusinessInfo["BillingCountry"].ToString() == "Canada") || (drBusinessInfo["BillingCountry"].ToString() == "United States")))
            {
                txtPrevMerchant.Enabled = true;
                lnkPrevMerchNum.Visible = false;
                ValidatePrevMerchant.Enabled = true;
                REPrevMerchant.Enabled = false;
            }
            else if ((drBusinessInfo["PrevMerchantAcctNo"].ToString().Trim() != "") && ((drBusinessInfo["BillingCountry"].ToString() == "Canada") || (drBusinessInfo["BillingCountry"].ToString() == "United States")))//contains previous data, do not enable editing and show the Edit link
            {
                txtPrevMerchant.Text = drBusinessInfo["PrevMerchantAcctNo"].ToString().Trim();
                lnkPrevMerchNum.Visible = false;
                txtPrevMerchant.Enabled = true;
                ValidatePrevMerchant.Enabled = true;
                REPrevMerchant.Enabled = false;
            }
            else
            {
                txtPrevMerchant.Enabled = true;
                lnkPrevMerchNum.Visible = false;
                ValidatePrevMerchant.Enabled = true;
                REPrevMerchant.Enabled = false;
            }
        }
    }

    protected void btnNavigateNext_Click(object sender, EventArgs e)
    {
        if (nextNavigatePage == "Percentage")
        {
            Response.Redirect("/Application/Merchant/cardpct.aspx", false);
        }
        else if (nextNavigatePage == "Company")
        {
            Response.Redirect("/Application/Merchant/companyinfo.aspx", false);
        }
        else if (nextNavigatePage == "Business")
        {
            Response.Redirect("/Application/Merchant/businessinfo.aspx", false);
        }
        else if (nextNavigatePage == "Principal")
        {
            Response.Redirect("/Application/Merchant/principalinfo.aspx", false);
        }
        else if (nextNavigatePage == "Banking")
        {
            Response.Redirect("/Application/Merchant/bankinginfo.aspx", false);
        }
    }


    protected void btnDoNotNavigate_Click(object sender, EventArgs e)
    {

    }

    protected bool returnComplete()
    {
        NewAppInfo AppInfo = new NewAppInfo(AppId);
        string Status = AppInfo.ReturnStatus();
        string StatusGW = AppInfo.ReturnStatusGW();

        CompanyInfo Company = new CompanyInfo(AppId);
        int companyCom = Company.CheckCompanyComplete();

        BusinessInfo Business = new BusinessInfo(AppId);
        int busCom = Business.CheckBusinessComplete("Merchant");
        int busComGW = Business.CheckBusinessComplete("Gateway");

        PrincipalInfo Principal1 = new PrincipalInfo(AppId);
        int principalCom = Principal1.CheckPrincipalComplete("Merchant");
        int principalComGW = Principal1.CheckPrincipalComplete("Gateway");

        CardPCT Card = new CardPCT(AppId);
        int cardPCTCom = Card.CheckCardPCTComplete();


        OtherInfo other = new OtherInfo(AppId);
        int otherCom = other.CheckOtherInfoComplete("Merchant");

        BankingInfo Banking = new BankingInfo(AppId);
        int bankComGW = Banking.CheckBankingComplete("Gateway");

        ReprogramInfo Reprogram = new ReprogramInfo(AppId);
        int ReprogramComGW = Reprogram.CheckReprogramComplete();

        bool Complete = false;
        if (busCom == 0)
        {
            Complete = false;
        }
        else
        {
            Complete = true;
        }

        return Complete;
    }

    protected void linkCardPctClick(object sender, EventArgs e)
    {
        btnSubmit_Click(sender, e);
        /*
        bool companyComplete = returnComplete();
        if (companyComplete == false)
        {
            nextNavigatePage = "Percentage";
            lblUserNavigateUnsaved.Text = "The current page is not complete. Are you sure to leave this page?";
            NavigatePopupModalExtender.Show();
        }
        else*/
        //{
        Response.Redirect("/Application/Merchant/cardpct.aspx", false);
        //}
    }

    protected void linkCompanyClick(object sender, EventArgs e)
    {
        btnSubmit_Click(sender, e);
        /*
        bool companyComplete = returnComplete();
        if (companyComplete == false)
        {
            nextNavigatePage = "Company";
            lblUserNavigateUnsaved.Text = "The current page is not complete. Are you sure to leave this page?";
            NavigatePopupModalExtender.Show();
        }
        else*/
        //{
        Response.Redirect("/Application/Merchant/companyinfo.aspx", false);
        //}
    }

    protected void linkBusinessClick(object sender, EventArgs e)
    {
        //btnSubmit_Click(sender, e);
        /*
        bool companyComplete = returnComplete();
        if (companyComplete == false)
        {
            nextNavigatePage = "Business";
            lblUserNavigateUnsaved.Text = "The current page is not complete. Are you sure to leave this page?";
            NavigatePopupModalExtender.Show();
        }
        else*/
        //{
        //Response.Redirect("/Application/Merchant/businessinfo.aspx", false);
        //}
    }

    protected void linkPrincipalClick(object sender, EventArgs e)
    {
        btnSubmit_Click(sender, e);
        /*
        bool companyComplete = returnComplete();
        if (companyComplete == false)
        {
            nextNavigatePage = "Principal";
            lblUserNavigateUnsaved.Text = "The current page is not complete. Are you sure to leave this page?";
            NavigatePopupModalExtender.Show();
        }
        else*/
        //{
        Response.Redirect("/Application/Merchant/principalinfo.aspx", false);
        //}
    }

    protected void linkBankingClick(object sender, EventArgs e)
    {
        btnSubmit_Click(sender, e);
        /*
        bool companyComplete = returnComplete();
        if (companyComplete == false)
        {
            nextNavigatePage = "Banking";
            lblUserNavigateUnsaved.Text = "The current page is not complete. Are you sure to leave this page?";
            NavigatePopupModalExtender.Show();
        }
        else*/
        //{
        Response.Redirect("/Application/Merchant/bankinginfo.aspx", false);
        //}
    }

    protected void linkPDFClick(object sender, EventArgs e)
    {
        btnSubmit_Click(sender, e);
        /*
        bool companyComplete = returnComplete();
        if (companyComplete == false)
        {
            nextNavigatePage = "Banking";
            lblUserNavigateUnsaved.Text = "The current page is not complete. Are you sure to leave this page?";
            NavigatePopupModalExtender.Show();
        }
        else*/
        //{
        Response.Redirect("/Application/notice.aspx", false);
        //}
    }

}
//checking the all code came
