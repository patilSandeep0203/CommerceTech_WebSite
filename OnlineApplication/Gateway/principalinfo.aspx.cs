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
using DataLayer;

public partial class Gateway_principalinfo : System.Web.UI.Page
{
    private static string strReplace = "";
    private string strP1SSN = "";
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
                        //Check if principal info has already been entered. If yes, 
                        //then check if user wants to edit info
                        //else redirect to principal.aspx.
                        PrincipalInfo PrincipalComplete = new PrincipalInfo(AppId);
                        if (PrincipalComplete.CheckPrincipalComplete("Gateway") > 0)
                        {
                            if (Request.Params.Get("EditPrincipal") != null)
                            {
                                //Do not display the SSN, Drivers License Number
                            }
                            else
                                Response.Redirect("principal.aspx", false);
                        }
                        PopulatePage();
                    }
                    else
                    {
                        Response.Redirect("principal.aspx", false);
                        SetErrorMessage("The status of your application prevents it from being edited");
                        btnSubmit.Enabled = false;
                    }
                }
                catch (Exception err)
                {
                    CreateOnlineAppLog Log = new CreateOnlineAppLog();
                    Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + Session["AppId"].ToString() + " - " + "Gateway principalinfo.aspx Page Load Error - " + err.Message);
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

    //This function populates form fields
    protected void PopulatePage()
    {
        //Get Countries
        CommonListData Countries = new CommonListData();
        DataSet dsCountry = Countries.GetCommonData("Countries");
        if (dsCountry.Tables["Countries"].Rows.Count > 0)
        {
            lstP1Country.DataSource = dsCountry.Tables["Countries"];
            lstP1Country.DataTextField = "Country";
            lstP1Country.DataValueField = "Country";
            lstP1Country.DataBind();
        }//end if count not 0
        lstP1Country.SelectedItem.Text = "United States";
        lstP1Country.SelectedItem.Value = "United States";

        //Get states
        CommonListData States = new CommonListData();
        DataSet dsStates = States.GetCommonData("States");
        if (dsStates.Tables["States"].Rows.Count > 0)
        {
            lstP1State.DataSource = dsStates.Tables["States"];
            lstP1State.DataTextField = "StateID";
            lstP1State.DataValueField = "StateID";
            lstP1State.DataBind();
        }//end if count not 0

        PrincipalInfo Principal = new PrincipalInfo(AppId);
         DataSet dsPrincipal1Info = Principal.GetPrincipalInfo();
        if (dsPrincipal1Info.Tables[0].Rows.Count > 0 )
        {
            DataRow drPrincipal1Info = dsPrincipal1Info.Tables[0].Rows[0];
            //Populate Principal 1 information
            txtP1FirstName.Text = drPrincipal1Info["P1FirstName"].ToString().Trim();
            txtP1LastName.Text = drPrincipal1Info["P1LastName"].ToString().Trim();
            txtP1MiddleName.Text = drPrincipal1Info["P1MidName"].ToString().Trim();
            txtP1Email.Text = drPrincipal1Info["P1Email"].ToString().Trim();
            txtP1JobTitle.Text = drPrincipal1Info["P1Title"].ToString().Trim();
            txtP1Address.Text = drPrincipal1Info["P1Address"].ToString().Trim();
            txtP1Address2.Text = drPrincipal1Info["P1Address2"].ToString().Trim();
            txtP1City.Text = drPrincipal1Info["P1City"].ToString().Trim();
            int index = lstP1State.Items.IndexOf(lstP1State.Items.FindByText(drPrincipal1Info["P1State"].ToString().Trim()));
            lstP1State.SelectedIndex = index;
            txtP1ZipCode.Text = drPrincipal1Info["P1ZipCode"].ToString().Trim();
            txtP1Region.Text = drPrincipal1Info["P1Region"].ToString().Trim();
            index = lstP1Country.Items.IndexOf(lstP1Country.Items.FindByText(drPrincipal1Info["P1Country"].ToString().Trim()));
            lstP1Country.SelectedIndex = index;
            txtP1Phone.Text = drPrincipal1Info["P1PhoneNumber"].ToString().Trim();
            txtP1MobilePhone.Text = drPrincipal1Info["P1MobilePhone"].ToString().Trim();

            //txtP1SSN.Text = drPrincipal1Info["P1SSN"].ToString().Trim();
            if (drPrincipal1Info["P1SSN"].ToString().Trim() == "")
                txtP1SSN.Enabled = true;  //enable this field
            else //contains previous data, do not enable editing and show the Edit link
            {
                ValidateP1SSN.Enabled = false;
                REP1SSN.Enabled = false;
                txtP1SSN.Enabled = false;
                lnkP1SSN.Visible = true;
                //display the masked value
                txtP1SSN.Text = "xxxxxx" + drPrincipal1Info["P1SSN"].ToString().Trim();
            }
        }//end if principal count not 0
    }//end function populate principal 1 info

    //This function handles the submit button click event
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
                    if (txtP1SSN.Text.Trim().Length < 9)
                    {
                        SetErrorMessage("The Social Security Number field must be 9 characters long. (Ex. 123456789)");
                    }
                    else
                    {
                        if (txtP1SSN.Enabled)
                            strP1SSN = txtP1SSN.Text.Trim().Replace("'", strReplace);
                        else //set to value to denote to database NOT to Update
                            strP1SSN = "-";

                        //Insert/Update first principal information
                        PrincipalInfo Principal = new PrincipalInfo(AppId);
                        bool retVal = Principal.UpdateGatewayPrincipalInfo( txtP1FirstName.Text.Trim().Replace("'", strReplace), txtP1LastName.Text.Trim().Replace("'", strReplace), txtP1MiddleName.Text.Trim().Replace("'", strReplace),
                            txtP1Email.Text.Trim().Replace("'", strReplace), txtP1JobTitle.Text.Trim().Replace("'", strReplace), txtP1Address.Text.Trim().Replace("'", strReplace), txtP1Address2.Text.Trim().Replace("'", strReplace), lstP1State.SelectedItem.Text.Trim().Replace("'", strReplace),
                            txtP1City.Text.Trim().Replace("'", strReplace), txtP1ZipCode.Text.Trim().Replace("'", strReplace), txtP1Region.Text.Trim().Replace("'", strReplace), lstP1Country.SelectedItem.Text.Trim().Replace("'", strReplace),
                            txtP1Phone.Text.Trim().Replace("'", strReplace), txtP1MobilePhone.Text.Trim().Replace("'", strReplace), strP1SSN, true, "No");

                        //Set last modified date in newapp table
                        CommonFunctions GeneralInfo = new CommonFunctions(AppId);
                        GeneralInfo.UpdateLastModified();

                        GeneralInfo.SetGWPageCount();

                        LogBL LogData = new LogBL(AppId);
                        LogData.InsertLogData(Convert.ToInt32(Session["AffiliateID"]), "Gateway Principal Information Updated");

                        Response.Redirect("bankinginfo.aspx", false);
                    }
                }//end if ValidateData
            }//end if page valid
        }//end try
        catch (Exception err)
        {
            CreateOnlineAppLog Log = new CreateOnlineAppLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + Session["AppId"].ToString() + " - " + "Gateway principalinfo.aspx Error - " + err.Message);
             SetErrorMessage("Error Processing Request. Please contact Technical Support: <a href='mailto:support@commercetech.com?Subject=Online Application Error'> support@commercetech.com. </a>.");
        }
    }//end function submit button click

    //This function handles the save button click event
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            ValidateSummary.Enabled = false;

            string strP1Phone = txtP1Phone.Text.Trim();
            string strP1Mobile = txtP1MobilePhone.Text.Trim();
            //try
            //{


            if ((lstP1Country.SelectedItem.Text == "United States") || lstP1Country.SelectedItem.Text.Contains("Canada"))
            {
                Regex regexObj = new Regex(@"[^0-9]");

                if (strP1Phone != "")
                {
                    strP1Phone = regexObj.Replace(strP1Phone, "").Trim();

                    strP1Phone = strP1Phone.Insert(3, "-");

                    strP1Phone = strP1Phone.Insert(7, "-");

                    txtP1Phone.Text = strP1Phone;
                }

                if (strP1Mobile != "")
                {

                    strP1Mobile = regexObj.Replace(strP1Mobile, "").Trim();

                    strP1Mobile = strP1Mobile.Insert(3, "-");

                    strP1Mobile = strP1Mobile.Insert(7, "-");

                    txtP1MobilePhone.Text = strP1Mobile;
                }

                if (strP1Phone.Trim().Length == 12)
                {

                    RegularExpressionValidatorPhone.Enabled = false;
                }
                else
                {

                    RegularExpressionValidatorPhone.Enabled = true;

                }

            }

            if (strP1Mobile.Trim().Length == 12)
            {

                RegularExpressionValidatorMobilePhone1.Enabled = false;
            }
            else
            {

                RegularExpressionValidatorMobilePhone1.Enabled = true;

            }

            if (txtP1SSN.Enabled == true)
            {
                string strP1SSN = txtP1SSN.Text.Trim();
                //try
                //{
                Regex regexObj = new Regex(@"[^0-9]");

                strP1SSN = regexObj.Replace(strP1SSN, "").Trim();

                txtP1SSN.Text = strP1SSN;

                REP1SSN.Enabled = false;
            }

            //Validate data
            bool retValidate = ValidateData();
            if (retValidate)
            {
                if (txtP1SSN.Enabled)
                    strP1SSN = txtP1SSN.Text.Trim().Replace("'", strReplace);
                else //set to value to denote to database NOT to Update
                    strP1SSN = "-";

                //Insert/Update first principal information
                PrincipalInfo Principal = new PrincipalInfo(AppId);
                bool retVal = Principal.UpdateGatewayPrincipalInfo(txtP1FirstName.Text.Trim().Replace("'", strReplace), txtP1LastName.Text.Trim().Replace("'", strReplace), txtP1MiddleName.Text.Trim().Replace("'", strReplace),
                    txtP1Email.Text.Trim().Replace("'", strReplace), txtP1JobTitle.Text.Trim().Replace("'", strReplace), txtP1Address.Text.Trim().Replace("'", strReplace), txtP1Address2.Text.Trim().Replace("'", strReplace), lstP1State.SelectedItem.Text.Trim().Replace("'", strReplace),
                    txtP1City.Text.Trim().Replace("'", strReplace), txtP1ZipCode.Text.Trim().Replace("'", strReplace), txtP1Region.Text.Trim().Replace("'", strReplace), lstP1Country.SelectedItem.Text.Trim().Replace("'", strReplace),
                    txtP1Phone.Text.Trim().Replace("'", strReplace), txtP1MobilePhone.Text.Trim().Replace("'", strReplace), strP1SSN, false, "No");

                //Set last modified date in newapp table
                CommonFunctions GeneralInfo = new CommonFunctions(AppId);
                GeneralInfo.UpdateLastModified();

                GeneralInfo.SetGWPageCount();

                LogBL LogData = new LogBL(AppId);
                LogData.InsertLogData(Convert.ToInt32(Session["AffiliateID"]), "Gateway Principal Information Saved");

                SetErrorMessage("Information saved. You may proceed to the next page by navigating through the top menu.");
            }                
        }//end try
        catch (Exception err)
        {
            CreateOnlineAppLog Log = new CreateOnlineAppLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + Session["AppId"].ToString() + " - " + "Gateway principalinfo.aspx Error - " + err.Message);
            SetErrorMessage("Error Processing Request. Please contact Technical Support: <a href='mailto:support@commercetech.com?Subject=Online Application Error'> support@commercetech.com. </a>.");
        }
    }//end function save button click

    //This function handles the radio button check changed events
    protected void btnP1SameAsBusiness_CheckedChanged(object sender, EventArgs e)
    {
        if (sender.Equals(btnP1SameAsBilling))
            PopulateBillingAddress();
        else if (sender.Equals(btnP1SameAsBusiness))
            PopulateBusinessAddress();
        else if (sender.Equals(btnP1Other))
        {
            txtP1Address.Text = "";
            txtP1Address2.Text = "";
            txtP1City.Text = "";
            txtP1ZipCode.Text = "";
            txtP1Region.Text = "";
            lstP1State.SelectedIndex = 0;
        }
    }//end function P1address same check changed

    //This function populates Billing address
    public void PopulateBillingAddress()
    {
        BusinessInfo Business = new BusinessInfo(AppId);
       DataSet dsBusinessInfo = Business.GetBusinessInfo();
       if (dsBusinessInfo.Tables["OnlineAppBusinessInfo"].Rows.Count > 0)
       {
           DataRow drBusinessInfo = dsBusinessInfo.Tables["OnlineAppBusinessInfo"].Rows[0];
           txtP1Address.Text = drBusinessInfo["BillingAddress"].ToString().Trim();
           txtP1Address2.Text = drBusinessInfo["BillingAddress2"].ToString().Trim();
           txtP1City.Text = drBusinessInfo["BillingCity"].ToString().Trim();
           txtP1ZipCode.Text = drBusinessInfo["BillingZipCode"].ToString().Trim();
           txtP1Region.Text = drBusinessInfo["BillingRegion"].ToString().Trim();
           int index = lstP1Country.Items.IndexOf(lstP1Country.Items.FindByText(drBusinessInfo["BillingCountry"].ToString().Trim()));
           lstP1Country.SelectedIndex = index;
           index = lstP1State.Items.IndexOf(lstP1State.Items.FindByText(drBusinessInfo["BillingState"].ToString().Trim()));
           lstP1State.SelectedIndex = index;
       }//end if count not 0
       else
       {
           txtP1Address.Text = "";
           txtP1Address2.Text = "";
           txtP1City.Text = "";
           txtP1ZipCode.Text = "";
           txtP1Region.Text = "";
           lstP1State.SelectedIndex = 0;
       }
    }//end function populate billing address

    //This function populates Business address
    public void PopulateBusinessAddress()
    {
        CompanyInfo Company = new CompanyInfo(AppId);
       DataSet dsCompanyInfo  = Company.GetCompanyInfo();
       if (dsCompanyInfo.Tables["OnlineAppCompanyInfo"].Rows.Count > 0)
       {
           DataRow drCompanyInfo = dsCompanyInfo.Tables["OnlineAppCompanyInfo"].Rows[0];
           txtP1Address.Text = drCompanyInfo["Address"].ToString().Trim();
           txtP1Address2.Text = drCompanyInfo["Address2"].ToString().Trim();
           txtP1City.Text = drCompanyInfo["City"].ToString().Trim();
           txtP1ZipCode.Text = drCompanyInfo["ZipCode"].ToString().Trim();
           txtP1Region.Text = drCompanyInfo["Region"].ToString().Trim();
           int index = lstP1Country.Items.IndexOf(lstP1Country.Items.FindByText(drCompanyInfo["Country"].ToString().Trim()));
           lstP1Country.SelectedIndex = index;
           index = lstP1State.Items.IndexOf(lstP1State.Items.FindByText(drCompanyInfo["State"].ToString().Trim()));
           lstP1State.SelectedIndex = index;
       }//end while
       else
       {
           txtP1Address.Text = "";
           txtP1Address2.Text = "";
           txtP1City.Text = "";
           txtP1ZipCode.Text = "";
           txtP1Region.Text = "";
           lstP1State.SelectedIndex = 0;
       }
    }//end function populate business address

    /*protected void lnkP1SSN_Click(object sender, EventArgs e)
    {
        txtP1SSN.Enabled = true;
        txtP1SSN.Text = "";
        lnkP1SSN.Visible = false;
        ValidateP1SSN.Enabled = true;
        REP1SSN.Enabled = true;
    }*/

    protected void lnkP1SSN_Click(object sender, EventArgs e)
    {
        /*
        txtP1SSN.Enabled = true;
        txtP1SSN.Text = "";
        lnkP1SSN.Visible = false;
        ValidateP1SSN.Enabled = true;
        REP1SSN.Enabled = true;*/
        //PrincipalInfo Principal = new PrincipalInfo(AppId);
        Principal1Table PrincipalTable = new Principal1Table();
        DataSet dsPrincipal1Info = PrincipalTable.GetPrincipal1FullInfo(AppId);
        //if information already prsent
        if (dsPrincipal1Info.Tables[0].Rows.Count > 0)
        {
            DataRow drPrincipal1Info = dsPrincipal1Info.Tables[0].Rows[0];
            if ((drPrincipal1Info["P1SSN"].ToString().Trim() == "") && (drPrincipal1Info["P1Country"].ToString() == "United States"))
            {
                txtP1SSN.Enabled = true;
            }//enable this field
            else if ((drPrincipal1Info["P1SSN"].ToString().Trim() != "") && (drPrincipal1Info["P1Country"].ToString() == "United States"))//contains previous data, do not enable editing and show the Edit link
            {
                ValidateP1SSN.Enabled = false;
                REP1SSN.Enabled = false;
                txtP1SSN.Enabled = true;
                lnkP1SSN.Visible = true;
                //display the masked value
                txtP1SSN.Text = drPrincipal1Info["P1SSN"].ToString().Trim();
            }
            else if ((drPrincipal1Info["P1SSN"].ToString().Trim() != "") && (drPrincipal1Info["P1Country"].ToString() != "United States"))
            {
                ValidateP1SSN.Enabled = false;
                REP1SSN.Enabled = false;
                txtP1SSN.Enabled = true;
                lnkP1SSN.Visible = true;
                //display the masked value
                txtP1SSN.Text = drPrincipal1Info["P1SSN"].ToString().Trim();
            }
            else
            {
                ValidateP1SSN.Enabled = false;
                REP1SSN.Enabled = false;
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
