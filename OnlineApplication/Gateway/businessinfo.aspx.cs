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
using DataLayer;
using System.Text.RegularExpressions;

public partial class Gateway_businessinfo : System.Web.UI.Page
{
    private static string strReplace = "";
    private string strFederalTaxID = "";
    private static int AppId = 0;
    DataSet dsBusinessInfo = null;
    DataRow drBusinessInfo = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session.IsNewSession)
            Response.Redirect("/Application/default.aspx?SessionExpired=True");

        Style TextArea = new Style();
        TextArea.Width = new Unit(220);
        TextArea.Height = new Unit(50);
        TextArea.Font.Size = FontUnit.Point(8);
        txtProductsSold.ApplyStyle(TextArea);

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
                        //Check if business info has already been entered. If yes, 
                        //then check if user wants to edit info
                        //else redirect to business.aspx.
                        BusinessInfo BusinessComplete = new BusinessInfo(AppId);
                        if (BusinessComplete.CheckBusinessComplete( "Gateway") > 0)
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
                    Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + Session["AppId"].ToString() + " - " + "Gateway businessinfo.aspx Page Load Error - " + err.Message);
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


        string[] arrOwnership = {"", "Corporation", "Government", "International Organization", "Legal/Medical Corp.", "LLC", "NonProfit", "Others", "Partnership", "Sole Proprietorship", "Tax Exempt", "Trust/Estate/Ass" };
        for (int i = 0; i < arrOwnership.Length; i++)
        {
            ListItem lstItem = new ListItem();
            lstItem.Text = arrOwnership[i];
            lstItem.Value = arrOwnership[i];
            lstOwnership.Items.Add(lstItem);
        }

        try
        {
            //Get business info from onlineappbusinessinfo
            BusinessInfo BusinessAdapter = new BusinessInfo(AppId);
            DataSet dsBusinessInfo = BusinessAdapter.GetBusinessInfo();
            if (dsBusinessInfo.Tables[0].Rows.Count > 0)
            {
                drBusinessInfo = dsBusinessInfo.Tables[0].Rows[0];
                int BusAddrSame = Convert.ToInt32(drBusinessInfo["BillingAddrSame"]);
                if (BusAddrSame == 0)
                {
                    DisableAddressControls();
                    chkBillingAddrSame.Checked = true;
                }
                PopulateAddress(BusAddrSame);
                int index = lstOwnership.Items.IndexOf(lstOwnership.Items.FindByText(drBusinessInfo["LegalStatus"].ToString().Trim()));
                lstOwnership.SelectedIndex = index;
                //txtFederalID.Text = drBusinessInfo["FederalTaxID"].ToString().Trim();
                txtYIB.Text = drBusinessInfo["YearsinBusiness"].ToString().Trim();
                txtMIB.Text = drBusinessInfo["MonthsinBusiness"].ToString().Trim();
                txtProductsSold.Text = drBusinessInfo["ProductSold"].ToString().Trim();

                //if no previous data
                if (drBusinessInfo["FederalTaxID"].ToString().Trim() == "")
                    txtFederalID.Enabled = true;
                else //contains previous data, do not enable editing and show the Edit link
                {
                    txtFederalID.Text = "xxxxxx" + drBusinessInfo["FederalTaxID"].ToString().Trim();
                    txtFederalID.Enabled = false;
                    lnkFederalTaxID.Visible = true;
                    ValidateTaxID.Enabled = false;
                    RETaxID.Enabled = false;
                }
            
            }//end if count not 0
        }//end try
        catch (SqlException)
        {
            SetErrorMessage("Error Processing Request. Please contact Technical Support: <a href=mailto:support@commercetech.com?Subject=Online App Error, App ID: " + Session["AppId"].ToString() + "> support@commercetech.com. </a>");
        }
    }//end function populate

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

    //This function populates address if billing address same check box is checked
    protected void PopulateAddress(int p_BillingAddrSame)
    {
        if (p_BillingAddrSame == 1)
        {
            txtAddress.Text = drBusinessInfo["BillingAddress"].ToString().Trim();
            txtAddress2.Text = drBusinessInfo["BillingAddress2"].ToString().Trim();
            txtCity.Text = drBusinessInfo["BillingCity"].ToString().Trim();
            txtZipCode.Text = drBusinessInfo["BillingZipCode"].ToString().Trim();
            txtRegion.Text = drBusinessInfo["BillingRegion"].ToString().Trim();
            int index = lstCountry.Items.IndexOf(lstCountry.Items.FindByText(drBusinessInfo["BillingCountry"].ToString().Trim()));
            lstCountry.SelectedIndex = index;
            index = lstState.Items.IndexOf(lstState.Items.FindByText(drBusinessInfo["BillingState"].ToString().Trim()));
            lstState.SelectedIndex = index;
        }
        else
        {
            CompanyInfo Company = new CompanyInfo(AppId);
            DataSet dsCompanyInfo = Company.GetCompanyInfo();
            if (dsCompanyInfo.Tables[0].Rows.Count > 0)
            {
                DataRow drCompanyInfo = dsCompanyInfo.Tables[0].Rows[0];
                txtAddress.Text = drCompanyInfo["Address"].ToString().Trim();
                txtAddress2.Text = drCompanyInfo["Address2"].ToString().Trim();
                txtCity.Text = drCompanyInfo["City"].ToString().Trim();
                txtZipCode.Text = drCompanyInfo["ZipCode"].ToString().Trim();
                txtRegion.Text = drCompanyInfo["Region"].ToString().Trim();
                int index = lstCountry.Items.IndexOf(lstCountry.Items.FindByText(drCompanyInfo["Country"].ToString().Trim()));
                lstCountry.SelectedIndex = index;
                index = lstState.Items.IndexOf(lstState.Items.FindByText(drCompanyInfo["State"].ToString().Trim()));
                lstState.SelectedIndex = index;
            }//end if count not 0
        }
    }//end function populate address

    //This function handles billing address check changed event
    protected void chk_CheckedChanged(object sender, EventArgs e)
    {
        if (chkBillingAddrSame.Checked)
        {
            DisableAddressControls();
            PopulateAddress(0);
        }
        else
        {
            txtAddress.Enabled = true;
            txtAddress2.Enabled = true;
            txtCity.Enabled = true;
            txtZipCode.Enabled = true;
            txtRegion.Enabled = true;
            lstState.Enabled = true;
            lstCountry.Enabled = true;

            txtAddress.Text = "";
            txtAddress2.Text = "";
            txtCity.Text = "";
            lstCountry.SelectedValue = "United States";
            txtRegion.Text = "";
            txtZipCode.Text = "";
        }
    }//end function billing check box changed

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
                    if (txtFederalID.Text.Trim().Length < 9)
                    {
                        SetErrorMessage("The Federal Tax ID/ Social Security Number must be 9 characters long. (Ex. 123456789)");
                    }
                    else
                    {
                        if (txtFederalID.Enabled == true)
                            strFederalTaxID = txtFederalID.Text.Trim().Replace("'", strReplace);
                        else //set to value to denote to Database NOT to Update
                            strFederalTaxID = "-";

                        BusinessInfo Business = new BusinessInfo(AppId);
                        int BillingAddrSame = 1;
                        if (chkBillingAddrSame.Checked)
                            BillingAddrSame = 0;
                        string YearsInBusiness = "0";
                        if (txtYIB.Text.Trim().Replace("'", strReplace) != "")
                            YearsInBusiness = txtYIB.Text.Trim().Replace("'", strReplace);
                        string MonthsInBusiness = "0";
                        if (txtMIB.Text.Trim().Replace("'", strReplace) != "")
                            MonthsInBusiness = txtMIB.Text.Trim().Replace("'", strReplace);
                        bool retVal = Business.UpdateGatewayBusinessInfo(BillingAddrSame, txtAddress.Text.Trim().Replace("'", strReplace), txtAddress2.Text.Trim().Replace("'", strReplace), txtCity.Text.Trim().Replace("'", strReplace),
                            lstState.SelectedValue.Trim().Replace("'", strReplace), txtRegion.Text.Trim().Replace("'", strReplace), txtZipCode.Text.Trim().Replace("'", strReplace), lstCountry.SelectedValue.Trim().Replace("'", strReplace),
                            strFederalTaxID, YearsInBusiness, MonthsInBusiness, lstOwnership.SelectedValue.Trim().Replace("'", strReplace),
                            txtProductsSold.Text.Trim().Replace("'", strReplace), true);

                        //Set last modified date in newapp table
                        CommonFunctions Common = new CommonFunctions(AppId);
                        Common.UpdateLastModified();

                        Common.SetGWPageCount();

                        LogBL LogData = new LogBL(AppId);
                        LogData.InsertLogData( Convert.ToInt32(Session["AffiliateID"]), "Gateway Business Information Updated");

                        Response.Redirect("principalinfo.aspx", false);
                    }
                }//end if validatedata
            }//end if page valid
        }//end try
        catch (Exception err)
        {
            CreateOnlineAppLog Log = new CreateOnlineAppLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + Session["AppId"].ToString() + " - " + "Gateway businessinfo.aspx Error - " + err.Message);
             SetErrorMessage("Error Processing Request. Please contact Technical Support: <a href='mailto:support@commercetech.com?Subject=Online Application Error'> support@commercetech.com. </a>.");
        }
    }//end function submit button click

    //This function handles save button click event
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtFederalID.Enabled == true)
            strFederalTaxID = txtFederalID.Text.Trim().Replace("'", strReplace);
        else //set to value to denote to Database NOT to Update
            strFederalTaxID = "-";

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

        try
        {
            ValidateSummary.Enabled = false;
            //Validate data
            bool retValidate = ValidateData();
            if (retValidate)
            {


                BusinessInfo Business = new BusinessInfo(AppId);
                int BillingAddrSame = 1;
                if (chkBillingAddrSame.Checked)
                    BillingAddrSame = 0;
                string YearsInBusiness = "0";
                if (txtYIB.Text.Trim().Replace("'", strReplace) != "")
                    YearsInBusiness = txtYIB.Text.Trim().Replace("'", strReplace);
                string MonthsInBusiness = "0";
                if (txtMIB.Text.Trim().Replace("'", strReplace) != "")
                    MonthsInBusiness = txtMIB.Text.Trim().Replace("'", strReplace);
                bool retVal = Business.UpdateGatewayBusinessInfo(BillingAddrSame, txtAddress.Text.Trim().Replace("'", strReplace), txtAddress2.Text.Trim().Replace("'", strReplace), txtCity.Text.Trim().Replace("'", strReplace),
                    lstState.SelectedValue.Trim().Replace("'", strReplace), txtRegion.Text.Trim().Replace("'", strReplace), txtZipCode.Text.Trim().Replace("'", strReplace), lstCountry.SelectedValue.Trim().Replace("'", strReplace),
                    strFederalTaxID, YearsInBusiness, MonthsInBusiness, lstOwnership.SelectedValue.Trim().Replace("'", strReplace),
                    txtProductsSold.Text.Trim().Replace("'", strReplace), false);

                //Set last modified date in newapp table
                CommonFunctions Common = new CommonFunctions(AppId);
                Common.UpdateLastModified();

                Common.SetGWPageCount();

                LogBL LogData = new LogBL(AppId);
                LogData.InsertLogData(Convert.ToInt32(Session["AffiliateID"]), "Gateway Business Information Saved");

                SetErrorMessage("Information saved. You may proceed to the next page by navigating through the top menu.");

            }//end if validatedata
        }//end try
        catch (Exception err)
        {
            CreateOnlineAppLog Log = new CreateOnlineAppLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + Session["AppId"].ToString() + " - " + "Gateway businessinfo.aspx Error - " + err.Message);
            SetErrorMessage("Error Processing Request. Please contact Technical Support: <a href='mailto:support@commercetech.com?Subject=Online Application Error'> support@commercetech.com. </a>.");
        }
    }//end function save button click

    /*protected void lnkFederalTaxID_Click(object sender, EventArgs e)
    {
        txtFederalID.Text = "";
        txtFederalID.Enabled = true;
        lnkFederalTaxID.Visible = false;
        ValidateTaxID.Enabled = true;
        RETaxID.Enabled = true;
    }*/

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
                if ((txtBox.Text.Contains("--")) || (txtBox.Text.Contains("#")) || (txtBox.Text.Contains(";")))
                {
                    txtBox.BackColor = System.Drawing.Color.Ivory;
                    txtBox.Focus();
                    SetErrorMessage("You cannot use hyphens, # or semi-colons in any of the following fields.");
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
}
