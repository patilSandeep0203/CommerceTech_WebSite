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

public partial class Gateway_companyinfo : System.Web.UI.Page
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
        lblError.ApplyStyle(errLabel); ;

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
                        CompanyInfo CompanyComplete = new CompanyInfo(AppId);
                        if (CompanyComplete.CheckCompanyComplete() > 0)
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
                }
                catch (Exception err)
                {
                    CreateOnlineAppLog Log = new CreateOnlineAppLog();
                    Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + Session["AppId"].ToString() + " - " + "Gateway companyinfo.aspx Page Load Error - " + err.Message);
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

     
        //Get company info from companyinfo table
        CompanyInfo Company = new CompanyInfo(AppId);
        DataSet dsCompanyInfo = Company.GetCompanyInfo();
        if (dsCompanyInfo.Tables["OnlineAppCompanyInfo"].Rows.Count > 0)
        {
            DataRow drCompanyInfo = dsCompanyInfo.Tables["OnlineAppCompanyInfo"].Rows[0];

            txtCompanyName.Text = drCompanyInfo["CompanyName"].ToString().Trim();
            txtDBA.Text = drCompanyInfo["DBA"].ToString().Trim();
            txtAddress.Text = drCompanyInfo["Address"].ToString().Trim();
            txtAddress2.Text = drCompanyInfo["Address2"].ToString().Trim();
            txtCity.Text = drCompanyInfo["City"].ToString().Trim();
            txtZipCode.Text = drCompanyInfo["ZipCode"].ToString().Trim();
            if (drCompanyInfo["State"].ToString() != "")
            {
                int index = lstState.Items.IndexOf(lstState.Items.FindByText(drCompanyInfo["State"].ToString().Trim()));
                lstState.SelectedIndex = index;
            }
            else
            {
                lstState.SelectedIndex = 1;
            }
            if (drCompanyInfo["Country"].ToString() != "")
            {
                int index = lstCountry.Items.IndexOf(lstCountry.Items.FindByText(drCompanyInfo["Country"].ToString().Trim()));
                lstCountry.SelectedIndex = index;
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


    
    //This function handles submit button click event
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        btnSave_Click(sender, e);

        ValidateSummary.Enabled = true;

        RegularExpressionValidatorBusinessPhone.Enabled = false;
        RegularExpressionValidatorCustServPhone.Enabled = false;
        RegularExpressionValidatorFax.Enabled = false;
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
                    bool retVal = Company.UpdateCompanyInfo(txtCompanyName.Text.Trim().Replace("'", strReplace),
                    txtCustServPhone.Text.Trim().Replace("'", strReplace), txtDBA.Text.Trim().Replace("'", strReplace), txtAddress.Text.Trim().Replace("'", strReplace), txtAddress2.Text.Trim().Replace("'", strReplace),
                    txtCity.Text.Trim().Replace("'", strReplace), lstState.SelectedItem.Text.Trim().Replace("'", strReplace), txtRegion.Text.Trim().Replace("'", strReplace), txtZipCode.Text.Trim().Replace("'", strReplace),
                    lstCountry.SelectedItem.Text.Trim().Replace("'", strReplace), txtTBLYears.Text.Trim().Replace("'", strReplace), txtTBLMonths.Text.Trim().Replace("'", strReplace), 
                    txtBusinessHours.Text.Trim().Replace("'", strReplace), txtPhone.Text.Trim().Replace("'", strReplace),
                    txtPhoneExt.Text.Trim().Replace("'", strReplace), txtFax.Text.Trim().Replace("'", strReplace), txtWebsite.Text.Trim().Replace("'", strReplace), true );


                    //Set last modified date in newapp table
                    CommonFunctions GeneralInfo = new CommonFunctions(AppId);
                    GeneralInfo.UpdateLastModified();

                    GeneralInfo.SetGWPageCount();

                    LogBL LogData = new LogBL(AppId);
                    LogData.InsertLogData( Convert.ToInt32(Session["AffiliateID"]), "Gateway Company Information Updated");

                    Response.Redirect("businessinfo.aspx", false);
                }//end if ValidateData         
            }//end if page valid
        }//end try
        catch (Exception err)
        {
            CreateOnlineAppLog Log = new CreateOnlineAppLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + Session["AppId"].ToString() + " - " + "Gateway companyinfo.aspx Error - " + err.Message);
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

            if ((lstCountry.SelectedItem.Text == "United States") || lstCountry.SelectedItem.Text.Contains("Canada"))
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
                GeneralInfo.UpdateLastModified();
                GeneralInfo.SetGWPageCount();

                LogBL LogData = new LogBL(AppId);
                LogData.InsertLogData(Convert.ToInt32(Session["AffiliateID"]), "Gateway Company Information Saved");

                SetErrorMessage("Information saved. You may proceed to the next page by navigating through the top menu.");

            }//end function ValidateDate

        }//end try

        catch (Exception err)
        {
            CreateOnlineAppLog Log = new CreateOnlineAppLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + Session["AppId"].ToString() + " - " + "Gateway companyinfo.aspx Error - " + err.Message);
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
                    SetErrorMessage("You cannot use apostrophe, # or semi-colons in any of the following fields.");
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
