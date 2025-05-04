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
using System.Text.RegularExpressions;


public partial class Merchant_companyinfo : System.Web.UI.Page
{
    private static string strReplace="";
    private static int AppId = 0;
    private static int PID = 0;
    private static string strSW = string.Empty;
    private static string nextNavigatePage = "";
    private bool textChanged = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        lblError.Visible = false;
     
        if (Session.IsNewSession)
            Response.Redirect("/Application/default.aspx?SessionExpired=True");


        Response.CacheControl = "no-cache";
        Response.AddHeader("Pragma", "no-cache");
        Response.Expires = -1;

        AppId = Convert.ToInt32(Session["AppId"]);
        

        Style errLabel = new Style();
        errLabel.BackColor = System.Drawing.Color.FromArgb(255, 192, 192);
        errLabel.ForeColor = System.Drawing.Color.Black;
        errLabel.BorderColor = System.Drawing.Color.Red;
        errLabel.BorderStyle = BorderStyle.Solid;
        errLabel.BorderWidth = Unit.Pixel(1);
        errLabel.Font.Size = FontUnit.Small;
        lblError.ApplyStyle(errLabel);

        if (!IsPostBack)
        {
            if (!User.Identity.IsAuthenticated)
                Response.Redirect("/Application/default.aspx?Authentication=False");
            
            try
            {
                if (!Locked())
                {
                    if (Request.Params.Get("RatesChanged") != null)
                    {
                        if (Request.Params.Get("RatesChanged").ToString().Trim() == "true")
                        {
                            SetErrorMessage("Your Visa/MC Discount Rate and Per Authorization rates have changed. Please click on view rates to view your new rates.");
                        }
                    }
                    //Check if business info has already been entered. If yes, 
                    //then check if user wants to edit info
                    //else redirect to business.aspx.
                    CompanyInfo Company = new CompanyInfo(AppId);
                    if (Company.CheckCompanyComplete() > 0)
                    {
                        if (Request.Params.Get("EditCompany") != null)
                        {
                            //Do not display the federal tax id, merchant number                               
                        }
                        else
                            Response.Redirect("company.aspx", false);
                    }
                    PopulatePage();
                }
                else
                {
                    Response.Redirect("company.aspx", false);
                    SetErrorMessage("The status of your application prevents it from being edited");
                    btnSubmit.Enabled = false;
                }
            }//end try
            catch (Exception err)
            {
                CreateOnlineAppLog Log = new CreateOnlineAppLog();
                Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + Session["AppId"].ToString() + " - " + "Merchant companyinfo.aspx Page Load Error - " + err.Message);
                 SetErrorMessage("Error Processing Request. Please contact Technical Support: <a href='mailto:support@commercetech.com?Subject=Online Application Error'> support@commercetech.com. </a>.");
            }
        }//end if postback
    }//end page load

    //This function checks if the application is locked
    protected bool Locked()
    {
        if (Session["Locked"].ToString().Trim() == "Yes")
            return true;
        else
            return false;
    }


    //This function populates the page with data from the database
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
            lstState.Items.Add("");
        }//end if count not 0


        //Get Countries
        CommonListData Countries = new CommonListData();
        DataSet dsCountry = Countries.GetCommonData("Countries");
        if (dsCountry.Tables["Countries"].Rows.Count > 0)
        {
            lstCountry.DataSource = dsCountry.Tables["Countries"];
            lstCountry.DataTextField = "Country";
            lstCountry.DataValueField = "Country";
            lstCountry.DataBind();
        }//end if count not 0
        lstCountry.SelectedItem.Text = "United States";
        lstCountry.SelectedItem.Value = "United States";

        
        //Get company info from onlineappcompanyinfo
        CompanyInfo Company = new CompanyInfo(AppId);
        DataSet dsCompanyInfo = Company.GetCompanyInfo();
        PID = Convert.ToInt32(Session["PID"]);
        if ((PID == 178) || (PID == 182) || (PID == 243) || (PID == 247) || (PID == 196))
        {
            lstState.Enabled = false;
            ValidateState.Enabled = false;
        }

        if (dsCompanyInfo.Tables["OnlineAppCompanyInfo"].Rows.Count > 0)
        {
            DataRow drCompanyInfo = dsCompanyInfo.Tables["OnlineAppCompanyInfo"].Rows[0];

            txtCompanyName.Text = drCompanyInfo["CompanyName"].ToString().Trim();
            txtDBA.Text = drCompanyInfo["DBA"].ToString().Trim();
            txtAddress.Text = drCompanyInfo["Address"].ToString().Trim();
            txtAddress2.Text = drCompanyInfo["Address2"].ToString().Trim();
            txtCity.Text = drCompanyInfo["City"].ToString().Trim();
            txtZipCode.Text = drCompanyInfo["ZipCode"].ToString().Trim();
            
            lstState.SelectedIndex = lstState.Items.IndexOf(lstState.Items.FindByText(drCompanyInfo["State"].ToString().Trim()));


            if (drCompanyInfo["Country"].ToString() == "United States")
                lstCountry.SelectedValue = "United States";
            else if (drCompanyInfo["Country"].ToString() == "Canada")
            {
                lstState.Enabled = true;
                ValidateState.Enabled = false;
                lstCountry.SelectedValue = "Canada";
            }
            else {
                lstState.Enabled = false;
                ValidateState.Enabled = false;
                lstCountry.SelectedIndex = lstCountry.Items.IndexOf(lstCountry.Items.FindByText(drCompanyInfo["Country"].ToString().Trim()));
                lblIntlZip.Visible = true;
            }

            if (lstCountry.SelectedValue == "United States")
            {
                lblStateStar.Visible = true;
                lblIntlZip.Visible = false;
                lstState.Enabled = true;
                ValidateState.Enabled = false;
                RegularExpressionValidatorBusinessPhone.Enabled = true;
                RegularExpressionValidatorFax.Enabled = true;
                RegularExpressionValidatorCustServPhone.Enabled = true;
                txtRegion.Enabled = false;
            }
            else if (lstCountry.SelectedValue == "Canada")
            {
                lblStateStar.Visible = true;
                lstState.Enabled = true;
                ValidateState.Enabled = false;
                lblIntlZip.Visible = true;
                RegularExpressionValidatorBusinessPhone.Enabled = true;
                RegularExpressionValidatorFax.Enabled = true;
                RegularExpressionValidatorCustServPhone.Enabled = true;
                txtRegion.Enabled = false;
            }
            else
            {
                lblStateStar.Visible = false;
                lstState.SelectedValue = "";
                lstState.Enabled = false;
                ValidateState.Enabled = false;
                lblIntlZip.Visible = true;
                txtZipCode.Text = "00000";
                RegularExpressionValidatorBusinessPhone.Enabled = false;
                RegularExpressionValidatorFax.Enabled = false;
                RegularExpressionValidatorCustServPhone.Enabled = false;
                txtRegion.Enabled = true;
            }


            txtRegion.Text = drCompanyInfo["Region"].ToString().Trim();
            txtTBLYears.Text = drCompanyInfo["YABL"].ToString().Trim();
            txtTBLMonths.Text = drCompanyInfo["MABL"].ToString().Trim();
            txtBusinessHours.Text = drCompanyInfo["BusinessHours"].ToString().Trim();
            txtPhone.Text = drCompanyInfo["BusinessPhone"].ToString().Trim();
            txtPhoneExt.Text = drCompanyInfo["BusinessPhoneExt"].ToString().Trim();
            txtWebsite.Text = drCompanyInfo["Website"].ToString().Trim();
            txtFax.Text = drCompanyInfo["Fax"].ToString().Trim();
            txtCustServPhone.Text = drCompanyInfo["CustServPhone"].ToString().Trim();

        }//end if count not 0
    }//end function populate page

    protected void textChangedEventHandler(object sender, EventArgs e)
    {
        textChanged = true;
    }

    protected void btnPOS2013Yes_Click(object sender, EventArgs e)
    {
        Response.Redirect("ContactPOS2013.aspx", false);
    }

    protected void btnPOS2013No_Click(object sender, EventArgs e)
    {
        Response.Redirect("https://merchant.intuit.com/signup/?a=chkt&api_key=inTuIt&b=posmswoc&offer_code=posms_023_n-445-16463", false);
    }

    //This function handles submit button click event
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        btnSave_Click(sender, e);

        CreatePDF proc = new CreatePDF(AppId);
        string processor = proc.ReturnProcessorName();
        string pid = proc.ReturnPackageId();

        ValidateSummary.Enabled = false;
        RegularExpressionValidatorBusinessPhone.Enabled = false;
        RegularExpressionValidatorCustServPhone.Enabled = false;
        
        string strPhoneTxt = txtPhone.Text.Trim();
        string strFaxTxt = txtFax.Text.Trim();
        string strCustServPhoneTxt = txtCustServPhone.Text.Trim();
        //try
        //{
        Regex regexObj = new Regex(@"[^0-9]");

        if (lstCountry.SelectedItem.Text.Contains("United States") || lstCountry.SelectedItem.Text.Contains("Canada"))
        {
            if (strPhoneTxt != "")
            {
                strPhoneTxt = regexObj.Replace(strPhoneTxt, "").Trim();

                strPhoneTxt = strPhoneTxt.Insert(3, "-");

                strPhoneTxt = strPhoneTxt.Insert(7, "-");

                txtPhone.Text = strPhoneTxt;
            }

            if (strCustServPhoneTxt != "")
            {

                strCustServPhoneTxt = regexObj.Replace(strCustServPhoneTxt, "").Trim();

                strCustServPhoneTxt = strCustServPhoneTxt.Insert(3, "-");

                strCustServPhoneTxt = strCustServPhoneTxt.Insert(7, "-");

                txtCustServPhone.Text = strCustServPhoneTxt;
            }

            if (strFaxTxt != "")
            {
                strFaxTxt = regexObj.Replace(strFaxTxt, "").Trim();

                strFaxTxt = strFaxTxt.Insert(3, "-");

                strFaxTxt = strFaxTxt.Insert(7, "-");

                txtFax.Text = strFaxTxt;
            }

            RegularExpressionValidatorBusinessPhone.Enabled = true;
            RegularExpressionValidatorCustServPhone.Enabled = true;
            RegularExpressionValidatorFax.Enabled = true;
            ValidateCS.Enabled = true;
            ValidatePhone.Enabled = true;
        }
        else {
            RegularExpressionValidatorBusinessPhone.Enabled = false;
            RegularExpressionValidatorCustServPhone.Enabled = false;
            RegularExpressionValidatorFax.Enabled = false;
            ValidateCS.Enabled = false;
            ValidatePhone.Enabled = false;
        }

        //}
        //catch (ArgumentException ex)
        //{
        // Syntax error in the regular expression
        //}
        /*
        if (strPhoneTxt.Trim().Length == 12)
        {

            RegularExpressionValidatorBusinessPhone.Enabled = false;
        }
        else
        {

            RegularExpressionValidatorBusinessPhone.Enabled = true;

        }

        if (strCustServPhoneTxt.Trim().Length == 12)
        {

            RegularExpressionValidatorCustServPhone.Enabled = false;
        }
        else
        {

            RegularExpressionValidatorCustServPhone.Enabled = true;

        }*/

        ValidateSummary.Enabled = true;   
        try
        {    
            if (Page.IsValid)
            {
                //Validate data
                bool retValidate = ValidateData();
                if (retValidate)
                {
                    //Call stored procedure to insert or update company information
                    CompanyInfo Company = new CompanyInfo(AppId);
                    //OnlineAppCompanyInfoTableAdapter Adapter = new OnlineAppCompanyInfoTableAdapter();
                    bool retVal = Company.UpdateCompanyInfo(txtCompanyName.Text.Trim().Replace("'", strReplace),
                    txtCustServPhone.Text.Trim().Replace("'", strReplace), txtDBA.Text.Trim().Replace("'", strReplace), txtAddress.Text.Trim().Replace("'", strReplace), txtAddress2.Text.Trim().Replace("'", strReplace),
                    txtCity.Text.Trim().Replace("'", strReplace), lstState.SelectedItem.Text.Trim().Replace("'", strReplace), txtRegion.Text.Trim().Replace("'", strReplace), txtZipCode.Text.Trim().Replace("'", strReplace),
                    lstCountry.SelectedItem.Text.Trim().Replace("'", strReplace), txtTBLYears.Text.Trim().Replace("'", strReplace), 
                    txtTBLMonths.Text.Trim().Replace("'", strReplace), txtBusinessHours.Text.Trim().Replace("'", strReplace), txtPhone.Text.Trim().Replace("'", strReplace),
                    txtPhoneExt.Text.Trim().Replace("'", strReplace), txtFax.Text.Trim().Replace("'", strReplace), txtWebsite.Text.Trim().Replace("'", strReplace), true);

                    //Set last modified date in newapp table
                    CommonFunctions GeneralInfo = new CommonFunctions(AppId);
                    
                    GeneralInfo.SetPageCount();
                    GeneralInfo.SetGWPageCount(); //update gateway page count since information recorded is either same or more than those required for Gateway Account

                    if (textChanged == true)
                    {
                        GeneralInfo.UpdateLastModified();
                        LogBL LogData = new LogBL(AppId);
                        LogData.InsertLogData(Convert.ToInt32(Session["AffiliateID"]), "Merchant Company Information Updated");
                        //Response.Redirect("businessinfo.aspx", false);
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
                }//end function ValidateDate
            }//end if page valid
        }//end try
        catch (Exception err)
        {
            CreateOnlineAppLog Log = new CreateOnlineAppLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + Session["AppId"].ToString() + " - " + "Merchant companyinfo.aspx Error - " + err.Message);
             SetErrorMessage("Error Processing Request. Please contact Technical Support: <a href='mailto:support@commercetech.com?Subject=Online Application Error'> support@commercetech.com. </a>.");
        }
    }//end function submit button click

    //save the App without checking for Required Fields
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            ValidateSummary.Enabled = false;
            
            string strPhoneTxt = txtPhone.Text.Trim();
            string strFaxTxt = txtFax.Text.Trim();
            string strCustServPhoneTxt = txtCustServPhone.Text.Trim();
            //try
            //{
            Regex regexObj = new Regex(@"[^0-9]");

            if ((lstCountry.SelectedItem.Text == "United States")||lstCountry.SelectedItem.Text.Contains("Canada"))
            {
                if (strPhoneTxt != "")
                {
                    strPhoneTxt = regexObj.Replace(strPhoneTxt, "").Trim();

                    strPhoneTxt = strPhoneTxt.Insert(3, "-");

                    strPhoneTxt = strPhoneTxt.Insert(7, "-");

                    txtPhone.Text = strPhoneTxt;
                }

                if (strCustServPhoneTxt != "")
                {

                    strCustServPhoneTxt = regexObj.Replace(strCustServPhoneTxt, "").Trim();

                    strCustServPhoneTxt = strCustServPhoneTxt.Insert(3, "-");

                    strCustServPhoneTxt = strCustServPhoneTxt.Insert(7, "-");

                    txtCustServPhone.Text = strCustServPhoneTxt;
                }

                if (strFaxTxt != "")
                {
                    strFaxTxt = regexObj.Replace(strFaxTxt, "").Trim();

                    strFaxTxt = strFaxTxt.Insert(3, "-");

                    strFaxTxt = strFaxTxt.Insert(7, "-");

                    txtFax.Text = strFaxTxt;
                }
            }

            //}
            //catch (ArgumentException ex)
            //{
                // Syntax error in the regular expression
            //}
            if (strPhoneTxt.Trim().Length == 12)
            {

                RegularExpressionValidatorBusinessPhone.Enabled = false;
            }
            else
            {

                RegularExpressionValidatorBusinessPhone.Enabled = true;

            }

            if (strCustServPhoneTxt.Trim().Length == 12)
            {

                RegularExpressionValidatorCustServPhone.Enabled = false;
            }
            else
            {

                RegularExpressionValidatorCustServPhone.Enabled = true;

            }
            

            //Validate data
            bool retValidate = ValidateData();
            if (retValidate)
            {
                //Call stored procedure to insert or update company information
                CompanyInfo Company = new CompanyInfo(AppId);
                //Update information with Complete bit set to false
                bool retVal = Company.UpdateCompanyInfo(txtCompanyName.Text.Trim().Replace("'", strReplace),
                txtCustServPhone.Text.Trim().Replace("'", strReplace), txtDBA.Text.Trim().Replace("'", strReplace), txtAddress.Text.Trim().Replace("'", strReplace), txtAddress2.Text.Trim().Replace("'", strReplace),
                txtCity.Text.Trim().Replace("'", strReplace), lstState.SelectedItem.Text.Trim().Replace("'", strReplace), txtRegion.Text.Trim().Replace("'", strReplace), txtZipCode.Text.Trim().Replace("'", strReplace),
                lstCountry.SelectedItem.Text.Trim().Replace("'", strReplace), txtTBLYears.Text.Trim().Replace("'", strReplace),
                txtTBLMonths.Text.Trim().Replace("'", strReplace), txtBusinessHours.Text.Trim().Replace("'", strReplace), txtPhone.Text.Trim().Replace("'", strReplace),
                txtPhoneExt.Text.Trim().Replace("'", strReplace), txtFax.Text.Trim().Replace("'", strReplace), txtWebsite.Text.Trim().Replace("'", strReplace), false);

                //Set last modified date in newapp table
                CommonFunctions GeneralInfo = new CommonFunctions(AppId);
                //GeneralInfo.UpdateLastModified();
                GeneralInfo.SetPageCount();
                GeneralInfo.SetGWPageCount(); //update gateway page count since information recorded is either same or more than those required for Gateway Account
                if (textChanged == true)
                {
                    GeneralInfo.UpdateLastModified();
                    LogBL LogData = new LogBL(AppId);
                    LogData.InsertLogData(Convert.ToInt32(Session["AffiliateID"]), "Merchant Company Information Updated");
                    //Response.Redirect("businessinfo.aspx", false);
                }
                
                /*
                if (textChanged == true)
                {
                    LogBL LogData = new LogBL(AppId);
                    LogData.InsertLogData(Convert.ToInt32(Session["AffiliateID"]), "Merchant Company Information Saved");
                }*/
                //SetErrorMessage("Information saved. You may proceed to the next page by navigating through the top menu.");
            }//end function ValidateDate

        }//end try
        
        catch (Exception err)
        {
            CreateOnlineAppLog Log = new CreateOnlineAppLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + Session["AppId"].ToString() + " - " + "Merchant companyinfo.aspx Error - " + err.Message);
            SetErrorMessage("Error Processing Request. Please contact Technical Support: <a href='mailto:support@commercetech.com?Subject=Online Application Error'> support@commercetech.com. </a>.");
        }
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
                    SetErrorMessage("You cannot use apostrophes, #, dashes, or semi-colons in any of the following fields.");
                    return false;
                }
            }
        }
        return true;
    }//end function validate data

    //This function displays error messages
    protected void SetErrorMessage(string errText)
    {
        lblError.Visible = true;
        lblError.Text = errText;
    }//end function set error message    

    protected void lstCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
       if (lstCountry.SelectedItem.Text.Trim() == "United States")
        {
            lblStateStar.Visible = true;
            lblIntlZip.Visible = false;
            lstState.Enabled = true;
            ValidateState.Enabled = false;
            RegularExpressionValidatorBusinessPhone.Enabled = true;
            RegularExpressionValidatorFax.Enabled = true;
            RegularExpressionValidatorCustServPhone.Enabled = true;
            txtRegion.Enabled = false;
        }
        else if (lstCountry.SelectedItem.Text.Trim() == "Canada")
        {
            lblStateStar.Visible = true;
            lstState.Enabled = true;
            ValidateState.Enabled = false;
            lblIntlZip.Visible = true;
            RegularExpressionValidatorBusinessPhone.Enabled = true;
            RegularExpressionValidatorFax.Enabled = true;
            RegularExpressionValidatorCustServPhone.Enabled = true;
            txtRegion.Enabled = false;
        }
       else{
           lblStateStar.Visible = false;
            lstState.SelectedValue = "";
            lstState.Enabled = false;
            ValidateState.Enabled = false;
            lblIntlZip.Visible = true;
            txtZipCode.Text = "00000";
            RegularExpressionValidatorBusinessPhone.Enabled = false;
            RegularExpressionValidatorFax.Enabled = false;
            RegularExpressionValidatorCustServPhone.Enabled = false;
            txtRegion.Enabled = true;
        }

        textChangedEventHandler(sender, e);
    }

    protected void btnNavigateNext_Click(object sender, EventArgs e)
    {
        if (nextNavigatePage == "Percentage")
        {
            Response.Redirect("/Application/Merchant/cardpct.aspx", false);
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
        Response.Redirect("/Application/Merchant/companyinfo.aspx", false);
    }

    protected bool returnCompanyComplete() {
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

        bool companyComplete = false;
        if (companyCom == 0)
        {
            companyComplete = false;
        }
        else
        {
            companyComplete = true;
        }

        return companyComplete;
    }

    protected void linkCardPctClick(object sender, EventArgs e)
    {
        btnSubmit_Click(sender, e);
        /*
        bool companyComplete = returnCompanyComplete();
        if (companyComplete == false)
        {
            nextNavigatePage = "Percentage";
            lblUserNavigateUnsaved.Text = "The current page is not complete. Are you sure to leave this page?";
            NavigatePopupModalExtender.Show();
        }
        else*/
        
            Response.Redirect("/Application/Merchant/cardpct.aspx", false);
        
    }

    protected void linkCompanyClick(object sender, EventArgs e)
    {
        
        nextNavigatePage = "Company";
        lblUserNavigateUnsaved.Text = "Are you sure to leave this page?";
        NavigatePopupModalExtender.Show();
    }

    protected void linkBusinessClick(object sender, EventArgs e)
    {
        btnSubmit_Click(sender, e);
        /*
         bool companyComplete = returnCompanyComplete();
         if (companyComplete == false)
         {
             nextNavigatePage = "Business";
             lblUserNavigateUnsaved.Text = "The current page is not complete. Are you sure to leave this page?";
             NavigatePopupModalExtender.Show();
         }
         else {*/
             Response.Redirect("/Application/Merchant/businessinfo.aspx", false);
         //}
    }

    protected void linkPrincipalClick(object sender, EventArgs e)
    {
        btnSubmit_Click(sender, e);
        /*
        bool companyComplete = returnCompanyComplete();
        if (companyComplete == false)
        {
            nextNavigatePage = "Principal";
            lblUserNavigateUnsaved.Text = "The current page is not complete. Are you sure to leave this page?";
            NavigatePopupModalExtender.Show();
        }
        else*/
        
            Response.Redirect("/Application/Merchant/principalinfo.aspx", false);
        
    }

    protected void linkBankingClick(object sender, EventArgs e)
    {
        btnSubmit_Click(sender, e);
        /*
        nextNavigatePage = "Banking";
        lblUserNavigateUnsaved.Text = "The current page is not complete. Are you sure to leave this page?";
        NavigatePopupModalExtender.Show();*/

        Response.Redirect("/Application/Merchant/bankinginfo.aspx", false);
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
