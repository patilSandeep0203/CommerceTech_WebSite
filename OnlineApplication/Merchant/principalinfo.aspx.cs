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

public partial class Merchant_principalinfo : System.Web.UI.Page
{
    private static string strReplace = "";
    private string strP1SSN = "";
    private string strP1DLNum = "";
    private string strP2SSN = "";
    private string strP2DLNum = "";
    private static int AppId = 0;
    private static int PID = 0;
    private static string nextNavigatePage = "";
    private static string strSW = string.Empty;
    private bool textChanged = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        lblError.Visible = false;

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

        ValidateP1License.Enabled = false;

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
                        if (PrincipalComplete.CheckPrincipalComplete("Merchant") > 0)
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
                    Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + Session["AppId"].ToString() + " - " + "Merchant principalinfo.aspx Page Load Error - " + err.Message);
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
     
        lnkP2DLNum.Visible = false;
        lnkP2SSN.Visible = false;
        ValidateP2DriversExpDate.Enabled = false;

        //Get states
        CommonListData States = new CommonListData();
        DataSet dsStates = States.GetCommonData("States");
        if (dsStates.Tables["States"].Rows.Count > 0)
        {
            lstP1State.DataSource = dsStates.Tables["States"];
            lstP1State.DataTextField = "StateID";
            lstP1State.DataValueField = "StateID";
            lstP1State.DataBind();
            lstP1State.Items.Add("");

            lstP2State.DataSource = dsStates.Tables["States"];
            lstP2State.DataTextField = "StateID";
            lstP2State.DataValueField = "StateID";
            lstP2State.DataBind();
            lstP2State.Items.Add("");

            lstP1DriverState.DataSource = dsStates.Tables["States"];
            lstP1DriverState.DataTextField = "StateID";
            lstP1DriverState.DataValueField = "StateID";
            lstP1DriverState.DataBind();
            lstP1DriverState.Items.Add("");

            lstP2DriverState.DataSource = dsStates.Tables["States"];
            lstP2DriverState.DataTextField = "StateID";
            lstP2DriverState.DataValueField = "StateID";
            lstP2DriverState.DataBind();
            lstP2DriverState.Items.Add("");
        }//end if count not 0
        
        //Get Countries
        CommonListData Countries = new CommonListData();
        DataSet dsCountry = Countries.GetCommonData("Countries");
        if (dsCountry.Tables["Countries"].Rows.Count > 0)
        {
            lstP1Country.DataSource = dsCountry.Tables["Countries"];
            lstP1Country.DataTextField = "Country";
            lstP1Country.DataValueField = "Country";
            lstP1Country.DataBind();

            lstP2Country.DataSource = dsCountry.Tables["Countries"];
            lstP2Country.DataTextField = "Country";
            lstP2Country.DataValueField = "Country";
            lstP2Country.DataBind();

        }//end if count not 0

        lstP1Country.SelectedItem.Text = "United States";
        lstP1Country.SelectedItem.Value = "United States";
        lstP2Country.SelectedItem.Text = "United States";
        lstP2Country.SelectedItem.Value = "United States";

        PID = Convert.ToInt32(Session["PID"]);
        if ((PID == 178) || (PID == 182) || (PID == 243) || (PID == 247) || (PID == 196))
        {
            lstP1State.Enabled = false;
            lstP2State.Enabled = false;
            ValidateP1State.Enabled = false;
            ValidateP2State.Enabled = false;
            //txtP1DriversLicense.Enabled = false;
            ValidateP1License.Enabled = false;
            //lstP1DriverState.Enabled = false;
            RequiredFieldValidator2.Enabled = false;
            //txtP1ExpDate.Enabled = false;
            ValidateP1DriversExpDate.Enabled = false;
            ValidateP2DriversLicense.Enabled = false;
            RequiredFieldValidator1.Enabled = false;
            ValidateP2DriversExpDate.Enabled = false;
            ValidateP2SSN.Enabled = false;
            ValidateP1SSN.Enabled = false;
            REP1SSN.Enabled = false;
            REP2SSN.Enabled = false;
        }

        if (PID == 190)
        {
            //txtP1DriversLicense.Enabled = false;
            ValidateP1License.Enabled = false;
            //lstP1DriverState.Enabled = false;
            RequiredFieldValidator2.Enabled = false;
            //txtP1ExpDate.Enabled = false;
            ValidateP1DriversExpDate.Enabled = false;
            ValidateP2DriversLicense.Enabled = false;
            RequiredFieldValidator1.Enabled = false;
            ValidateP2DriversExpDate.Enabled = false;
            ValidateP2SSN.Enabled = false;
            ValidateP1SSN.Enabled = false;
            REP1SSN.Enabled = false;
            REP2SSN.Enabled = false;
        }

        //Get principal info from onlineappprincipal info
        //PrincipalInfo Principal = new PrincipalInfo(AppId);
        //DataSet dsPrincipal1Info = Principal.GetPrincipalInfo();

        Principal1Table PrincipalTable = new Principal1Table();
        DataSet dsPrincipal1Info = PrincipalTable.GetPrincipal1FullInfo(AppId);


        //if information already prsent
        if (dsPrincipal1Info.Tables[0].Rows.Count > 0)
        {
            //make visible the links for Editing the protected field
            lnkP1DLNum.Visible = true;
            lnkP1SSN.Visible = true;
   
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
            lstP1State.SelectedIndex = lstP1State.Items.IndexOf(lstP1State.Items.FindByText(drPrincipal1Info["P1State"].ToString().Trim()));
            //lstP1State.SelectedIndex = index;
            txtP1ZipCode.Text = drPrincipal1Info["P1ZipCode"].ToString().Trim();
            txtP1Region.Text = drPrincipal1Info["P1Region"].ToString().Trim();

            if ((drPrincipal1Info["P1Country"].ToString() == "United States") || (drPrincipal1Info["P1Country"].ToString() == ""))
                lstP1Country.SelectedValue = "United States";
            else if (drPrincipal1Info["P1Country"].ToString() == "Canada") 
            { lstP1Country.SelectedValue = "Canada"; }
            else
            {
                lstP1State.Enabled = false;
                ValidateP1State.Enabled = false;
                lstP1Country.SelectedIndex = lstP1Country.Items.IndexOf(lstP1Country.Items.FindByText(drPrincipal1Info["P1Country"].ToString().Trim()));
            }


            if ((lstP1Country.SelectedValue != "United States") && (lstP1Country.SelectedValue != "Canada"))
            {
                lblP1StateStar.Visible = false;
                lstP1State.SelectedValue = "";
                lstP1State.Enabled = false;
                ValidateP1State.Enabled = false;
                lblP1DLStar.Visible = false;
                txtP1DriversLicense.Enabled = false;
                ValidateP1License.Enabled = false;
                lblP1DLStateStar.Visible = false;
                lstP1DriverState.SelectedValue = "";
                lstP1DriverState.Enabled = false;
                RequiredFieldValidator2.Enabled = false;
                lblP1DLExpStar.Visible = false;
                txtP1ExpDate.Enabled = false;
                RegularExpressionValidatorP1DrDate.Enabled = false;
                ValidateP1DriversExpDate.Enabled = false;
                lblP1SSNStar.Visible = false;
                txtP1SSN.Enabled = false;
                ValidateP1SSN.Enabled = false;
                REP1SSN.Enabled = false;
                RegularExpressionP1Phone.Enabled = false;
                RegularExpressionValidatorMobilePhone1.Enabled = false;
                txtP1Region.Enabled = true;
            }
            else
            {
                lblP1StateStar.Visible = true;
                lstP1State.Enabled = true;
                ValidateP1State.Enabled = true;
                lblP1DLStar.Visible = true;
                lblP1DLStar.Visible = true;
                txtP1DriversLicense.Enabled = true;
                ValidateP1License.Enabled = false;
                lblP1DLStateStar.Visible = true;
                lstP1DriverState.SelectedValue = "";
                lstP1DriverState.Enabled = true;
                RequiredFieldValidator2.Enabled = false;
                txtP1ExpDate.Enabled = true;
                RegularExpressionValidatorP1DrDate.Enabled = true;
                ValidateP1DriversExpDate.Enabled = false;
                lblP1SSNStar.Visible = true;
                txtP1SSN.Enabled = true;
                ValidateP1SSN.Enabled = true;
                REP1SSN.Enabled = true;
                RegularExpressionP1Phone.Enabled = true;
                RegularExpressionValidatorMobilePhone1.Enabled = true;
                txtP1Region.Enabled = false;
            }

            txtP1YearsAtAddress.Text = drPrincipal1Info["P1YearsatAddress"].ToString().Trim();
            int index = lstP1Months.Items.IndexOf(lstP1Months.Items.FindByText(drPrincipal1Info["P1MonthsatAddress"].ToString().Trim()));
            lstP1Months.SelectedIndex = index;
            txtP1Phone.Text = drPrincipal1Info["P1PhoneNumber"].ToString().Trim();
            txtP1MobilePhone.Text = drPrincipal1Info["P1MobilePhone"].ToString().Trim();
            
            //if no previous data
            if ((drPrincipal1Info["P1driversLicenseNo"].ToString().Trim() == "") && (drPrincipal1Info["P1Country"].ToString() == "United States"))
            {
                txtP1DriversLicense.Enabled = true;  //enable this field
                lnkP1DLNum.Visible = false;
            }
            else if ((drPrincipal1Info["P1driversLicenseNo"].ToString().Trim() != "") && (drPrincipal1Info["P1Country"].ToString() == "United States"))//contains previous data, do not enable editing and show the Edit link
            {
                txtP1DriversLicense.Text = drPrincipal1Info["P1driversLicenseNo"].ToString().Trim();
                txtP1DriversLicense.Enabled = false;
                lnkP1DLNum.Visible = true;
                ValidateP1License.Enabled = false;
                //display the masked value

            }
            else if ((drPrincipal1Info["P1driversLicenseNo"].ToString().Trim() != "") && (drPrincipal1Info["P1Country"].ToString() != "United States"))//contains previous data, do not enable editing and show the Edit link
            {
                txtP1DriversLicense.Text = drPrincipal1Info["P1driversLicenseNo"].ToString().Trim();
                txtP1DriversLicense.Enabled = false;
                lnkP1DLNum.Visible = true;
                ValidateP1License.Enabled = false;
                //display the masked value

            }
            else 
            {
                ValidateP1License.Enabled = false;
                RequiredFieldValidator2.Enabled = false;
                ValidateP1DriversExpDate.Enabled = false;
            }
         
            index = lstP1DriverState.Items.IndexOf(lstP1DriverState.Items.FindByText(drPrincipal1Info["P1DriversLicenseState"].ToString().Trim()));
            lstP1DriverState.SelectedIndex = index;
            txtP1ExpDate.Text = drPrincipal1Info["P1driversLicenseExpiry"].ToString().Trim();
            txtP1DOB.Text = drPrincipal1Info["P1DOB"].ToString().Trim();
            if (drPrincipal1Info["P1LivingStatus"].ToString().Trim() == "Rent")
            {
                lstP1RentOwn.SelectedIndex = 1;
            }
            else if (drPrincipal1Info["P1LivingStatus"].ToString().Trim() == "Own")
            {
                lstP1RentOwn.SelectedIndex = 2;
            }
            else lstP1RentOwn.SelectedIndex = 0;
            txtP1OwnershipPCT.Text = drPrincipal1Info["P1OwnershipPercent"].ToString().Trim();
            //if no previous data

            if ((drPrincipal1Info["P1SSN"].ToString().Trim() == "") && (drPrincipal1Info["P1Country"].ToString() == "United States"))
            {
                txtP1SSN.Enabled = true;
            }//enable this field
            else if ((drPrincipal1Info["P1SSN"].ToString().Trim() != "") && (drPrincipal1Info["P1Country"].ToString() == "United States"))//contains previous data, do not enable editing and show the Edit link
            {
                ValidateP1SSN.Enabled = false;
                REP1SSN.Enabled = false;
                txtP1SSN.Enabled = false;
                lnkP1SSN.Visible = true;
                //display the masked value
                txtP1SSN.Text = drPrincipal1Info["P1SSN"].ToString().Trim();
            }
            else if ((drPrincipal1Info["P1SSN"].ToString().Trim() != "") && (drPrincipal1Info["P1Country"].ToString() != "United States"))
            {
                ValidateP1SSN.Enabled = false;
                REP1SSN.Enabled = false;
                txtP1SSN.Enabled = false;
                lnkP1SSN.Visible = true;
                //display the masked value
                txtP1SSN.Text = drPrincipal1Info["P1SSN"].ToString().Trim();
            }
            else
            {
                ValidateP1SSN.Enabled = false;
                REP1SSN.Enabled = false;
            }
  
  
            string SecondPrincipal = drPrincipal1Info["HasSecondPrincipal"].ToString().Trim();
            if (SecondPrincipal == "Yes")
            {
                btnYes.Checked = true;
                pnlPrincipal2.Visible = true;
                //get principal 2 info
                PopulateSecondPrincipal();
                ValidateSecondPrincipal(1);
            }
            else
            {
                btnNo.Checked = true;
                pnlPrincipal2.Visible = false;
                ValidateSecondPrincipal(0);
            }
        }//end if principal count not 0
        else
        {
            btnNo.Checked = true;
            pnlPrincipal2.Visible = false;
        }
    }//end function populate principal 1 info

    protected void textChangedEventHandler(object sender, EventArgs e)
    {
        textChanged = true;
    }

    //This function handles check changed event to check if second principal exists
    protected void rdb_CheckedChanged(object sender, EventArgs e)
    {
        if (sender.Equals(btnNo))
        {
            pnlPrincipal2.Visible = false;
            ValidateSecondPrincipal(0);
        }
        if (sender.Equals(btnYes))
        {
            pnlPrincipal2.Visible = true;
            ValidateSecondPrincipal(1);
            PopulateSecondPrincipal();
        }

        textChangedEventHandler(sender, e);
    }//end function second principal radio button selection changed

    //This function populates second principal information
    protected void PopulateSecondPrincipal()
    {

        Principal2Table Principal2Table = new Principal2Table();
        DataSet dsPrincipal2Info = Principal2Table.GetPrincipal2FullInfo(AppId);

        //Principal2Info Principal2 = new Principal2Info(AppId);
        //DataSet dsPrincipal2Info = Principal2.GetPrincipal2Info();
        if (dsPrincipal2Info.Tables[0].Rows.Count > 0)
        {
            //Make visible links for editing  P2 DL and SSN
            lnkP2DLNum.Visible = true;
            lnkP2SSN.Visible = true;
            lstP2State.Enabled = true;

            DataRow drPrincipal2Info = dsPrincipal2Info.Tables[0].Rows[0];
            //Populate Principal 2 information
            txtP2FirstName.Text = drPrincipal2Info["P2FirstName"].ToString().Trim();
            txtP2LastName.Text = drPrincipal2Info["P2LastName"].ToString().Trim();
            txtP2MiddleName.Text = drPrincipal2Info["P2MidName"].ToString().Trim();
            txtP2Email.Text = drPrincipal2Info["P2Email"].ToString().Trim();
            txtP2JobTitle.Text = drPrincipal2Info["P2Title"].ToString().Trim();
            txtP2Address.Text = drPrincipal2Info["P2Address"].ToString().Trim();
            txtP2Address2.Text = drPrincipal2Info["P2Address2"].ToString().Trim();
            txtP2City.Text = drPrincipal2Info["P2City"].ToString().Trim();
            int index = lstP2State.Items.IndexOf(lstP2State.Items.FindByText(drPrincipal2Info["P2State"].ToString().Trim()));
            //lstP2State.SelectedIndex = index;
            lstP2State.SelectedValue = drPrincipal2Info["P2State"].ToString().Trim();
            string P2State = "";
            P2State = drPrincipal2Info["P2State"].ToString().Trim();
            lstP2State.SelectedValue = P2State;
            txtP2ZipCode.Text = drPrincipal2Info["P2ZipCode"].ToString().Trim();
            txtP2Region.Text = drPrincipal2Info["P2Region"].ToString().Trim();

            if (drPrincipal2Info["P2Country"].ToString() == "United States")
                lstP2Country.SelectedValue = "United States";
            else if (drPrincipal2Info["P2Country"].ToString() == "Canada")
            { lstP2Country.SelectedValue = "Canada"; }

            if ((Convert.ToString(lstP2Country.SelectedValue).Trim() != "United States") && (Convert.ToString(lstP2Country.SelectedValue).Trim() != "Canada") && (Convert.ToString(lstP2Country.SelectedValue).Trim() != ""))
            {
                lstP2State.Enabled = false;
                ValidateP2State.Enabled = false;
                lstP2Country.SelectedIndex = lstP2Country.Items.IndexOf(lstP2Country.Items.FindByText(drPrincipal2Info["P2Country"].ToString().Trim()));
            }

            if ((lstP2Country.SelectedValue != "United States") && (lstP2Country.SelectedValue != "Canada"))
            {
                lblP2StateStar.Visible = false;
                lstP2State.SelectedValue = "";
                lstP2State.Enabled = false;
                ValidateP2State.Enabled = false;
                lblP2DLStar.Visible = false;
                txtP2DriversLicense.Enabled = false;
                ValidateP2DriversLicense.Enabled = false;
                lblP2DLStateStar.Visible = false;
                lstP2DriverState.SelectedValue = "";
                lstP2DriverState.Enabled = false;
                RequiredFieldValidator1.Enabled = false;
                lblP2DLExpStar.Visible = false;
                txtP2ExpDate.Enabled = false;
                RegularExpressionValidatorP2DRDate.Enabled = false;
                ValidateP2DriversExpDate.Enabled = false;
                lblP2SSNStar.Visible = false;
                txtP2SSN.Enabled = false;
                ValidateP2SSN.Enabled = false;
                REP2SSN.Enabled = false;
                ValidateP2HomePhone.Enabled = false;
                RegularExpressionValidatorPhone2.Enabled = false;
                RegularExpressionValidatorMobilePhone2.Enabled = false;
                txtP2Region.Enabled = true;
            }
            else
            {
                lblP2StateStar.Visible = true;
                //lstP2State.SelectedValue = "";
                lstP2State.Enabled = true;
                ValidateP2State.Enabled = true;
                lblP2DLStar.Visible = true;
                txtP2DriversLicense.Enabled = true;
                ValidateP2DriversLicense.Enabled = true;
                lblP2DLStateStar.Visible = true;
                lstP2DriverState.SelectedValue = "";
                lstP2DriverState.Enabled = true;
                RequiredFieldValidator1.Enabled = false;
                lblP2DLExpStar.Visible = true;
                txtP2ExpDate.Enabled = true;
                RegularExpressionValidatorP2DRDate.Enabled = false;
                ValidateP2DriversExpDate.Enabled = false;
                lblP2SSNStar.Visible = true;
                txtP2SSN.Enabled = true;
                ValidateP2SSN.Enabled = true;
                REP2SSN.Enabled = true;
                ValidateP2HomePhone.Enabled = true;
                RegularExpressionValidatorPhone2.Enabled = true;
                RegularExpressionValidatorMobilePhone2.Enabled = true;
                txtP2Region.Enabled = false;
            }

            txtP2YearsAtAddress.Text = drPrincipal2Info["P2YearsatAddress"].ToString().Trim();
            index = lstP2Months.Items.IndexOf(lstP2Months.Items.FindByText(drPrincipal2Info["P2MonthsatAddress"].ToString().Trim()));
            lstP2Months.SelectedIndex = index;
            txtP2Phone.Text = drPrincipal2Info["P2PhoneNumber"].ToString().Trim();
            txtP2MobilePhone.Text = drPrincipal2Info["P2MobilePhone"].ToString().Trim();
            index = lstP2DriverState.Items.IndexOf(lstP2DriverState.Items.FindByText(drPrincipal2Info["P2DriversLicenseState"].ToString().Trim()));
            lstP2DriverState.SelectedIndex = index;
            lstP2State.SelectedValue = drPrincipal2Info["P2State"].ToString().Trim();
            txtP2ExpDate.Text = drPrincipal2Info["P2driversLicenseExpiry"].ToString().Trim();
            txtP2DOB.Text = drPrincipal2Info["P2DOB"].ToString().Trim();
            if (drPrincipal2Info["P2LivingStatus"].ToString().Trim() == "Rent")
            {
                lstP2RentOwn.SelectedIndex = 1;
            }
            else if (drPrincipal2Info["P2LivingStatus"].ToString().Trim() == "Own")
            {
                lstP2RentOwn.SelectedIndex = 2;
            }
            else { lstP2RentOwn.SelectedIndex = 0; }
            txtP2OwnershipPCT.Text = drPrincipal2Info["P2OwnershipPercent"].ToString().Trim();

            if ((drPrincipal2Info["P2SSN"].ToString().Trim() == "") && (drPrincipal2Info["P2Country"].ToString() == "United States"))
            {
                txtP2SSN.Enabled = true;  //enable this field
            }
            else if ((drPrincipal2Info["P2SSN"].ToString().Trim() != "") && (drPrincipal2Info["P2Country"].ToString() == "United States"))//contains previous data, do not enable editing and show the Edit link
            {
                ValidateP2SSN.Enabled = false;
                REP2SSN.Enabled = false;
                txtP2SSN.Enabled = false;
                lnkP2SSN.Visible = true;
                //display the masked value
                txtP2SSN.Text = drPrincipal2Info["P2SSN"].ToString().Trim();
            }
            else if ((drPrincipal2Info["P2SSN"].ToString().Trim() != "") && (drPrincipal2Info["P2Country"].ToString() != "United States"))//contains previous data, do not enable editing and show the Edit link
            {
                ValidateP2SSN.Enabled = false;
                REP2SSN.Enabled = false;
                txtP2SSN.Enabled = false;
                lnkP2SSN.Visible = true;
                //display the masked value
                txtP2SSN.Text =  drPrincipal2Info["P2SSN"].ToString().Trim();
            }
            else 
            {
                ValidateP2SSN.Enabled = false;
                REP2SSN.Enabled = false;
            }

            //if no previous data
            if ((drPrincipal2Info["P2driversLicenseNo"].ToString().Trim() == "")&&(drPrincipal2Info["P2Country"].ToString() == "United States"))
            {
                txtP2DriversLicense.Enabled = true;  //enable this field
                lnkP2DLNum.Visible = false;
            }
            else if ((drPrincipal2Info["P2driversLicenseNo"].ToString().Trim() != "") && (drPrincipal2Info["P2Country"].ToString() == "United States"))//contains previous data, do not enable editing and show the Edit link
            {
                txtP2DriversLicense.Enabled = false;
                lnkP2DLNum.Visible = true;
                //display the masked value
                txtP2DriversLicense.Text = drPrincipal2Info["P2driversLicenseNo"].ToString().Trim();
            }
            else if ((drPrincipal2Info["P2driversLicenseNo"].ToString().Trim() != "") && (drPrincipal2Info["P2Country"].ToString() != "United States"))//contains previous data, do not enable editing and show the Edit link
            {
                txtP2DriversLicense.Enabled = false;
                lnkP2DLNum.Visible = true;
                //display the masked value
                txtP2DriversLicense.Text = drPrincipal2Info["P2driversLicenseNo"].ToString().Trim();
            }
            else 
            {
                ValidateP2DriversLicense.Enabled = false;
                RequiredFieldValidator1.Enabled = false;
                ValidateP2DriversExpDate.Enabled = false;
            }

        }//end if principal count not 0
    }//end function populate principal 2 info


    //This function enables/disables validation for principal 2 depending on whether principal 2 exists or not
    protected void ValidateSecondPrincipal(int flag)
    {
        if (flag == 0)
        {
            ValidateP2Address.Enabled = false;
            ValidateP2City.Enabled = false;
            ValidateP2Country.Enabled = false;
            ValidateP2DOB.Enabled = false;
            ValidateP2DriversExpDate.Enabled = false;
            ValidateP2DriversLicense.Enabled = false;
            ValidateP2FirstName.Enabled = false;
            ValidateP2HomePhone.Enabled = false;
            ValidateP2JobTitle.Enabled = false;
            ValidateP2LastName.Enabled = false;
            ValidateP2Ownership.Enabled = false;
            ValidateP2RentOwn.Enabled = false;
            ValidateP2SSN.Enabled = false;
            ValidateP2State.Enabled = false;
            ValidateP2YearsatAddress.Enabled = false;
            ValidateP2ZipCode.Enabled = false;
            ValidateREP2TABL.Enabled = false;
            
        }
        else
        {
            ValidateP2Address.Enabled = true;
            ValidateP2City.Enabled = true;
            ValidateP2Country.Enabled = true;
            ValidateP2DOB.Enabled = true;
            ValidateP2DriversExpDate.Enabled = false;
            ValidateP2DriversLicense.Enabled = false;
            ValidateP2FirstName.Enabled = true;
            ValidateP2HomePhone.Enabled = true;
            ValidateP2JobTitle.Enabled = true;
            ValidateP2LastName.Enabled = true;
            ValidateP2Ownership.Enabled = true;
            ValidateP2RentOwn.Enabled = false;
            ValidateP2SSN.Enabled = true;
            ValidateP2State.Enabled = true;
            ValidateP2YearsatAddress.Enabled = false;
            ValidateP2ZipCode.Enabled = true;
            ValidateREP2TABL.Enabled = true;
        }//end if flag 0
    }//end function validate second principal

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
        btnSave_Click(sender, e);

        CreatePDF proc = new CreatePDF(AppId);
        string processor = proc.ReturnProcessorName();
        string pid = proc.ReturnPackageId();
        //if (txtP1SSN.Enabled == true)
        {
            string strP1SSN = txtP1SSN.Text.Trim();
            //try
            //{
            Regex regexObj = new Regex(@"[^0-9]");

            strP1SSN = regexObj.Replace(strP1SSN, "").Trim();

            txtP1SSN.Text = strP1SSN;

            REP1SSN.Enabled = false;
        }

        //if (txtP2SSN.Enabled == true)
        {
            string strP2SSN = txtP2SSN.Text.Trim();
            //try
            //{
            Regex regexObj = new Regex(@"[^0-9]");

            strP2SSN = regexObj.Replace(strP2SSN, "").Trim();

            txtP2SSN.Text = strP2SSN;

            REP2SSN.Enabled = false;
        }

        ValidateSummary.Enabled = true;      
        string SecondPrincipal = "No";
        if (btnYes.Checked)
            SecondPrincipal = "Yes";
        bool retVal1 = false;
        bool retVal2 = false;
        try
        {
            if (Page.IsValid)
            {
                CommonFunctions GeneralInfo = new CommonFunctions(AppId);
                //Validate data
                bool retValidate = ValidateData();
                if (retValidate)
                {
                    if ((txtP1SSN.Text.Trim().Length != 9) && ((lstP1Country.SelectedItem.Text.Trim() == "United States") || (lstP1Country.SelectedItem.Text.Trim() == "Canada")))
                    {
                        SetErrorMessage("The Social Security Number field for Principal 1 must be 9 characters long. (Ex. 123456789)");
                    }
                    else
                    {

                        if (txtP1SSN.Enabled)
                            strP1SSN = txtP1SSN.Text.Trim().Replace("'", strReplace);
                        else //set to value to denote to database NOT to Update
                            strP1SSN = "-";

                        if (txtP1DriversLicense.Enabled)
                            strP1DLNum = txtP1DriversLicense.Text.Trim().Replace("'", strReplace);
                        else //set to value to denote to database NOT to Update
                            strP1DLNum = "-";

                        //Set first principal information
                        PrincipalInfo Principal1 = new PrincipalInfo(AppId);
                        retVal1 = Principal1.UpdatePrincipal1Info(txtP1FirstName.Text.Trim().Replace("'", strReplace), txtP1LastName.Text.Trim().Replace("'", strReplace), txtP1MiddleName.Text.Trim().Replace("'", strReplace),
                            txtP1Email.Text.Trim().Replace("'", strReplace), txtP1JobTitle.Text.Trim().Replace("'", strReplace), txtP1Address.Text.Trim().Replace("'", strReplace), txtP1Address2.Text.Trim().Replace("'", strReplace),
                            lstP1State.SelectedItem.Text.Trim().Replace("'", strReplace), txtP1City.Text.Trim().Replace("'", strReplace), txtP1ZipCode.Text.Trim().Replace("'", strReplace), txtP1Region.Text.Trim().Replace("'", strReplace),
                            lstP1Country.SelectedItem.Text.Trim().Replace("'", strReplace), txtP1YearsAtAddress.Text.Trim().Replace("'", strReplace), lstP1Months.SelectedItem.Value, txtP1Phone.Text.Trim().Replace("'", strReplace), txtP1MobilePhone.Text.Trim().Replace("'", strReplace),
                            strP1DLNum, lstP1DriverState.SelectedItem.Text.Trim().Replace("'", strReplace), txtP1ExpDate.Text.Trim().Replace("'", strReplace),
                            txtP1DOB.Text.Trim().Replace("'", strReplace), lstP1RentOwn.SelectedItem.Text.Trim().Replace("'", strReplace), txtP1OwnershipPCT.Text.Trim().Replace("'", strReplace),
                            strP1SSN, true, SecondPrincipal);

                        if (retVal1)
                        {
                            if (textChanged == true)
                            {
                                LogBL LogData = new LogBL(AppId);
                                LogData.InsertLogData(Convert.ToInt32(Session["AffiliateID"]), "Merchant Principal #1 Information Updated");
                                GeneralInfo.UpdateLastModified();
                            }
                        }
                    }

                    if (SecondPrincipal == "Yes")
                    {
                        if (txtP2SSN.Text.Trim().Length != 9)
                        {
                            SetErrorMessage("The Social Security Number field for Principal 2 must be 9 characters long. (Ex. 123456789)");
                        }
                        else
                        {
                            if (txtP2SSN.Enabled)
                                strP2SSN = txtP2SSN.Text.Trim().Replace("'", strReplace);
                            else //set to value to denote to database NOT to Update
                                strP2SSN = "-";

                            if (txtP2DriversLicense.Enabled)
                                strP2DLNum = txtP2DriversLicense.Text.Trim().Replace("'", strReplace);
                            else //set to value to denote to database NOT to Update
                                strP2DLNum = "-";

                            //Insert/Update second principal information
                            Principal2Info Principal2 = new Principal2Info(AppId);
                            retVal2 = Principal2.UpdatePrincipal2Info(txtP2FirstName.Text.Trim().Replace("'", strReplace), txtP2LastName.Text.Trim().Replace("'", strReplace), txtP2MiddleName.Text.Trim().Replace("'", strReplace),
                            txtP2Email.Text.Trim().Replace("'", strReplace), txtP2JobTitle.Text.Trim().Replace("'", strReplace), txtP2Address.Text.Trim().Replace("'", strReplace), txtP2Address2.Text.Trim().Replace("'", strReplace),
                            lstP2State.SelectedItem.Text.Trim().Replace("'", strReplace), txtP2City.Text.Trim().Replace("'", strReplace), txtP2ZipCode.Text.Trim().Replace("'", strReplace), txtP2Region.Text.Trim().Replace("'", strReplace),
                            lstP2Country.SelectedItem.Text.Trim().Replace("'", strReplace), txtP2YearsAtAddress.Text.Trim().Replace("'", strReplace), lstP2Months.SelectedItem.Value, txtP2Phone.Text.Trim().Replace("'", strReplace), txtP2MobilePhone.Text.Trim().Replace("'", strReplace),
                            strP2DLNum, lstP2DriverState.SelectedItem.Text.Trim().Replace("'", strReplace), txtP2ExpDate.Text.Trim().Replace("'", strReplace),
                            txtP2DOB.Text.Trim().Replace("'", strReplace), lstP2RentOwn.SelectedItem.Text.Trim().Replace("'", strReplace), txtP2OwnershipPCT.Text.Trim().Replace("'", strReplace),
                            strP2SSN, true);

                            if (retVal2)
                            {
                                if (textChanged == true)
                                {
                                    LogBL LogData = new LogBL(AppId);
                                    LogData.InsertLogData(Convert.ToInt32(Session["AffiliateID"]), "Merchant Principal #2 Information Updated");
                                    GeneralInfo.UpdateLastModified();
                                }
                            }
                        }
                    }
                    else
                    {
                        if (Convert.ToInt32(txtP1OwnershipPCT.Text.Trim()) < 51)
                        {
                            //txtP1OwnershipPCT.Text.Trim();
                            //throw ("Only one principal is entered. At least 100% of ownership for principal is required.");
                            retVal1 = false;
                            SetErrorMessage("Only one principal is entered. At least 51% of ownership for principal is required.");
                        }
                        else
                        {

                            Principal2Info Principal2 = new Principal2Info(AppId);
                            retVal2 = Principal2.UpdatePrincipal2Info("", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                                "", "", "", "", "", "", "", "", false);
                        }
                    }


                    //Set last modified date in newapp table
                    
                    

                    GeneralInfo.SetPageCount();
                    GeneralInfo.SetGWPageCount(); //update gateway page count since information recorded is either same or more than those required for Gateway Account

                }//end if ValidateData
            }//end if page valid
        }//end try
        catch (Exception err)
        {
            CreateOnlineAppLog Log = new CreateOnlineAppLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + Session["AppId"].ToString() + " - " + "Merchant principalinfo.aspx Error - " + err.Message);
            SetErrorMessage("Error Processing Request. Please contact Technical Support: <a href='mailto:support@commercetech.com?Subject=Online Application Error'> support@commercetech.com. </a>.");
        }
        if (retVal1)
        {
            //Response.Redirect("bankinginfo.aspx");
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
        }
    }//end function submit button click

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

    //This function handles the radio button check changed events
    protected void btnP1SameAsBusiness_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            textChangedEventHandler(sender, e);

            if (sender.Equals(btnP1SameAsBilling))
                PopulateBillingAddress(1);
            else if (sender.Equals(btnP1SameAsBusiness))
                PopulateBusinessAddress(1);
            else if (sender.Equals(btnP1Other))
            {
                txtP1Address.Text = "";
                txtP1Address2.Text = "";
                txtP1City.Text = "";
                txtP1ZipCode.Text = "";
                txtP1Region.Text = "";
                lstP1State.SelectedIndex = 0;
            }
            else if (sender.Equals(btnP2SameAsBilling))
                PopulateBillingAddress(2);
            else if (sender.Equals(btnP2SameAsBusiness))
                PopulateBusinessAddress(2);
            else if (sender.Equals(btnP2Other))
            {
                txtP2Address.Text = "";
                txtP2Address2.Text = "";
                txtP2City.Text = "";
                txtP2ZipCode.Text = "";
                txtP2Region.Text = "";
                lstP2State.SelectedIndex = 0;
            }
            else if (sender.Equals(btnP2SameasPrincipal))
                PopulatePrincipal1AddressinP2();
        }//end try
        catch (Exception err)
        {
            CreateOnlineAppLog Log = new CreateOnlineAppLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + Session["AppId"].ToString() + " - " + "Merchant principalinfo.aspx Address Same Error - " + err.Message);
             SetErrorMessage("Error Processing Request. Please contact Technical Support: <a href='mailto:support@commercetech.com?Subject=Online Application Error'> support@commercetech.com. </a>.");
        }
    }//end function address same radio button selection changed

    //This function populates principal 1 address in principal 2
    public void PopulatePrincipal1AddressinP2()
    {
        txtP2Address.Text = txtP1Address.Text;
        txtP2Address2.Text = txtP1Address2.Text;
        txtP2City.Text = txtP1City.Text;
        txtP2ZipCode.Text = txtP1ZipCode.Text;
        txtP2Region.Text = txtP1Region.Text;
        lstP2State.SelectedIndex = lstP1State.SelectedIndex;
        lstP2Country.SelectedIndex = lstP1Country.SelectedIndex;
    }//end function populate principal 1 address in principal 2

    //This function populates Billing address
    public void PopulateBillingAddress(int principal)
    {
        BusinessInfo Business = new BusinessInfo(AppId);
        DataSet dsBusinessInfo = Business.GetBusinessInfo();
        if (dsBusinessInfo.Tables[0].Rows.Count > 0)
        {
            DataRow drBusinessInfo = dsBusinessInfo.Tables[0].Rows[0];
            if (principal == 1)
            {
                txtP1Address.Text = drBusinessInfo["BillingAddress"].ToString().Trim();
                txtP1Address2.Text = drBusinessInfo["BillingAddress2"].ToString().Trim();
                txtP1City.Text = drBusinessInfo["BillingCity"].ToString().Trim();
                txtP1ZipCode.Text = drBusinessInfo["BillingZipCode"].ToString().Trim();
                txtP1Region.Text = drBusinessInfo["BillingRegion"].ToString().Trim();
                int index = lstP1Country.Items.IndexOf(lstP1Country.Items.FindByText(drBusinessInfo["BillingCountry"].ToString().Trim()));
                lstP1Country.SelectedIndex = index;
                index = lstP1State.Items.IndexOf(lstP1State.Items.FindByText(drBusinessInfo["BillingState"].ToString().Trim()));
                lstP1State.SelectedIndex = index;
            }
            else
            {
                txtP2Address.Text = drBusinessInfo["BillingAddress"].ToString().Trim();
                txtP2Address2.Text = drBusinessInfo["BillingAddress2"].ToString().Trim();
                txtP2City.Text = drBusinessInfo["BillingCity"].ToString().Trim();
                txtP2ZipCode.Text = drBusinessInfo["BillingZipCode"].ToString().Trim();
                txtP2Region.Text = drBusinessInfo["BillingRegion"].ToString().Trim();
                int index = lstP2Country.Items.IndexOf(lstP2Country.Items.FindByText(drBusinessInfo["BillingCountry"].ToString().Trim()));
                lstP2Country.SelectedIndex = index;
                index = lstP2State.Items.IndexOf(lstP2State.Items.FindByText(drBusinessInfo["BillingState"].ToString().Trim()));
                lstP2State.SelectedIndex = index;
            }
        }//end if count not 0
        else
        {
            if (principal == 1)
            {
                txtP1Address.Text = "";
                txtP1Address2.Text = "";
                txtP1City.Text = "";
                txtP1ZipCode.Text = "";
                txtP1Region.Text = "";
                lstP1State.SelectedIndex = 0;
                lstP1Country.SelectedIndex = 0;
            }
            else
            {
                txtP2Address.Text = "";
                txtP2Address2.Text = "";
                txtP2City.Text = "";
                txtP2ZipCode.Text = "";
                txtP2Region.Text = "";
                lstP2State.SelectedIndex = 0;
                lstP2Country.SelectedIndex = 0;
            }
        }
    }//end function populate billing address

    //This function populates Business address
    public void PopulateBusinessAddress(int principal)
    {
        CompanyInfo Company = new CompanyInfo(AppId);
        DataSet dsCompanyInfo = Company.GetCompanyInfo();
        if (dsCompanyInfo.Tables[0].Rows.Count > 0)
        {
            DataRow drCompanyInfo = dsCompanyInfo.Tables[0].Rows[0];
            if (principal == 1)
            {
                txtP1Address.Text = drCompanyInfo["Address"].ToString().Trim();
                txtP1Address2.Text = drCompanyInfo["Address2"].ToString().Trim();
                txtP1City.Text = drCompanyInfo["City"].ToString().Trim();
                txtP1ZipCode.Text = drCompanyInfo["ZipCode"].ToString().Trim();
                txtP1Region.Text = drCompanyInfo["Region"].ToString().Trim();
                int index = lstP1Country.Items.IndexOf(lstP1Country.Items.FindByText(drCompanyInfo["Country"].ToString().Trim()));
                lstP1Country.SelectedIndex = index;
                index = lstP1State.Items.IndexOf(lstP1State.Items.FindByText(drCompanyInfo["State"].ToString().Trim()));
                lstP1State.SelectedIndex = index;
            }
            else
            {
                txtP2Address.Text = drCompanyInfo["Address"].ToString().Trim();
                txtP2Address2.Text = drCompanyInfo["Address2"].ToString().Trim();
                txtP2City.Text = drCompanyInfo["City"].ToString().Trim();
                txtP2ZipCode.Text = drCompanyInfo["ZipCode"].ToString().Trim();
                txtP2Region.Text = drCompanyInfo["Region"].ToString().Trim();
                int index = lstP2Country.Items.IndexOf(lstP2Country.Items.FindByText(drCompanyInfo["Country"].ToString().Trim()));
                lstP2Country.SelectedIndex = index;
                index = lstP2State.Items.IndexOf(lstP2State.Items.FindByText(drCompanyInfo["State"].ToString().Trim()));
                lstP2State.SelectedIndex = index;
            }
        }//end if count not 0
        else
        {
            if (principal == 1)
            {
                txtP1Address.Text = "";
                txtP1Address2.Text = "";
                txtP1City.Text = "";
                txtP1ZipCode.Text = "";
                txtP1Region.Text = "";
                lstP1State.SelectedIndex = 0;
                lstP1Country.SelectedIndex = 0;
            }
            else
            {
                txtP2Address.Text = "";
                txtP2Address2.Text = "";
                txtP2City.Text = "";
                txtP2ZipCode.Text = "";
                txtP2Region.Text = "";
                lstP2State.SelectedIndex = 0;
                lstP2Country.SelectedIndex = 0;
            }
        }
    }//end function populate business address

    //This function displays error messages
    protected void SetErrorMessage(string errText)
    {
        lblError.Visible = true;
        lblError.Text = errText;
    }//end function set error message
    protected void btnSave_Click(object sender, EventArgs e)
    {


        ValidateSummary.Enabled = false;

        string strP1Phone = txtP1Phone.Text.Trim();
        string strP1Mobile = txtP1MobilePhone.Text.Trim();
        string strP2Phone = txtP2Phone.Text.Trim();
        string strP2Mobile = txtP2MobilePhone.Text.Trim();
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

            if (strP2Phone != "")
            {
                strP2Phone = regexObj.Replace(strP2Phone, "").Trim();

                strP2Phone = strP2Phone.Insert(3, "-");

                strP2Phone = strP2Phone.Insert(7, "-");

                txtP2Phone.Text = strP2Phone;
            }

            if (strP2Mobile != "")
            {

                strP2Mobile = regexObj.Replace(strP2Mobile, "").Trim();

                strP2Mobile = strP2Mobile.Insert(3, "-");

                strP2Mobile = strP2Mobile.Insert(7, "-");

                txtP2MobilePhone.Text = strP2Mobile;
            }

            if (strP1Phone.Trim().Length == 12)
            {

                RegularExpressionP1Phone.Enabled = false;
            }
            else
            {

                RegularExpressionP1Phone.Enabled = true;

            }

        }

        //}
        //catch (ArgumentException ex)
        //{
        // Syntax error in the regular expression
        //}


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

        //if (txtP2SSN.Enabled == true)
        {
            string strP2SSN = txtP2SSN.Text.Trim();
            //try
            //{
            Regex regexObj = new Regex(@"[^0-9]");

            strP2SSN = regexObj.Replace(strP2SSN, "").Trim();

            txtP2SSN.Text = strP2SSN;

            REP2SSN.Enabled = false;
        }


        ValidateSummary.Enabled = false;
        string SecondPrincipal = "No";
        if (btnYes.Checked)
            SecondPrincipal = "Yes";
        bool retVal1 = false;
        bool retVal2 = false;
        try
        {
            //Validate data
            bool retValidate = ValidateData();
            if (retValidate)
            {
                if (txtP1SSN.Enabled)
                    strP1SSN = txtP1SSN.Text.Trim().Replace("'", strReplace);
                else //set to value to denote to database NOT to Update
                    strP1SSN = "-";

                if (txtP1DriversLicense.Enabled)
                    strP1DLNum = txtP1DriversLicense.Text.Trim().Replace("'", strReplace);
                else //set to value to denote to database NOT to Update
                    strP1DLNum = "-";

                //Insert/Update first principal information
                PrincipalInfo Principal1 = new PrincipalInfo(AppId);
                retVal1 = Principal1.UpdatePrincipal1Info(txtP1FirstName.Text.Trim().Replace("'", strReplace), txtP1LastName.Text.Trim().Replace("'", strReplace), txtP1MiddleName.Text.Trim().Replace("'", strReplace),
                    txtP1Email.Text.Trim().Replace("'", strReplace), txtP1JobTitle.Text.Trim().Replace("'", strReplace), txtP1Address.Text.Trim().Replace("'", strReplace), txtP1Address2.Text.Trim().Replace("'", strReplace),
                    lstP1State.SelectedItem.Text.Trim().Replace("'", strReplace), txtP1City.Text.Trim().Replace("'", strReplace), txtP1ZipCode.Text.Trim().Replace("'", strReplace), txtP1Region.Text.Trim().Replace("'", strReplace),
                    lstP1Country.SelectedItem.Text.Trim().Replace("'", strReplace), txtP1YearsAtAddress.Text.Trim().Replace("'", strReplace), lstP1Months.SelectedItem.Value, txtP1Phone.Text.Trim().Replace("'", strReplace), txtP1MobilePhone.Text.Trim().Replace("'", strReplace),
                    strP1DLNum, lstP1DriverState.SelectedItem.Text.Trim().Replace("'", strReplace), txtP1ExpDate.Text.Trim().Replace("'", strReplace),
                    txtP1DOB.Text.Trim().Replace("'", strReplace), lstP1RentOwn.SelectedItem.Text.Trim().Replace("'", strReplace), txtP1OwnershipPCT.Text.Trim().Replace("'", strReplace),
                    strP1SSN, false, SecondPrincipal);

                /*if (retVal1)
                {
                    LogBL LogData = new LogBL(AppId);
                    LogData.InsertLogData(Convert.ToInt32(Session["AffiliateID"]), "Merchant Principal #1 Information Updated");
                }*/

                if (SecondPrincipal == "Yes")
                {

                    if (txtP2SSN.Enabled)
                        strP2SSN = txtP2SSN.Text.Trim().Replace("'", strReplace);
                    else //set to value to denote to database NOT to Update
                        strP2SSN = "-";

                    if (txtP2DriversLicense.Enabled)
                        strP2DLNum = txtP2DriversLicense.Text.Trim().Replace("'", strReplace);
                    else //set to value to denote to database NOT to Update
                        strP2DLNum = "-";

                    //Insert/Update second principal information
                    Principal2Info Principal2 = new Principal2Info(AppId);
                    retVal2 = Principal2.UpdatePrincipal2Info(txtP2FirstName.Text.Trim().Replace("'", strReplace), txtP2LastName.Text.Trim().Replace("'", strReplace), txtP2MiddleName.Text.Trim().Replace("'", strReplace),
                    txtP2Email.Text.Trim().Replace("'", strReplace), txtP2JobTitle.Text.Trim().Replace("'", strReplace), txtP2Address.Text.Trim().Replace("'", strReplace), txtP2Address2.Text.Trim().Replace("'", strReplace),
                    lstP2State.SelectedItem.Text.Trim().Replace("'", strReplace), txtP2City.Text.Trim().Replace("'", strReplace), txtP2ZipCode.Text.Trim().Replace("'", strReplace), txtP2Region.Text.Trim().Replace("'", strReplace),
                    lstP2Country.SelectedItem.Text.Trim().Replace("'", strReplace), txtP2YearsAtAddress.Text.Trim().Replace("'", strReplace), lstP2Months.SelectedItem.Value, txtP2Phone.Text.Trim().Replace("'", strReplace), txtP2MobilePhone.Text.Trim().Replace("'", strReplace),
                    strP2DLNum, lstP2DriverState.SelectedItem.Text.Trim().Replace("'", strReplace), txtP2ExpDate.Text.Trim().Replace("'", strReplace),
                    txtP2DOB.Text.Trim().Replace("'", strReplace), lstP2RentOwn.SelectedItem.Text.Trim().Replace("'", strReplace), txtP2OwnershipPCT.Text.Trim().Replace("'", strReplace),
                    strP2SSN, false);

                    /*if (retVal2)
                    {
                        LogBL LogData = new LogBL(AppId);
                        LogData.InsertLogData(Convert.ToInt32(Session["AffiliateID"]), "Merchant Principal #2 Information Updated");
                    }*/
                }

                //Set last modified date in newapp table
                CommonFunctions GeneralInfo = new CommonFunctions(AppId);
                //GeneralInfo.UpdateLastModified();

                if (textChanged == true)
                {
                    LogBL LogData = new LogBL(AppId);
                    LogData.InsertLogData(Convert.ToInt32(Session["AffiliateID"]), "Merchant Principal #1 Information Updated");
                    GeneralInfo.UpdateLastModified();
                }

                GeneralInfo.SetPageCount();
                GeneralInfo.SetGWPageCount(); //update gateway page count since information recorded is either same or more than those required for Gateway Account

                //SetErrorMessage("Information saved. You may proceed to the next page by navigating through the top menu.");
            }//end if validate data
        }//end try
        catch (Exception err)
        {
            CreateOnlineAppLog Log = new CreateOnlineAppLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + Session["AppId"].ToString() + " - " + "Merchant principalinfo.aspx Error - " + err.Message);
            SetErrorMessage("Error Processing Request. Please contact Technical Support: <a href='mailto:support@commercetech.com?Subject=Online Application Error'> support@commercetech.com. </a>.");
        }
    }
    protected void lstP1Country_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if ((lstP1Country.SelectedItem.Text.Trim() != "United States") && (lstP1Country.SelectedItem.Text.Trim() != "Canada"))
            {
                lblP1StateStar.Visible = false;
                lstP1State.SelectedValue = "";
                lstP1State.Enabled = false;
                ValidateP1State.Enabled = false;
                lblP1DLStar.Visible = false;
                txtP1DriversLicense.Enabled = false;
                ValidateP1License.Enabled = false;
                lblP1DLStateStar.Visible = false;
                lstP1DriverState.SelectedValue = "";
                lstP1DriverState.Enabled = false;
                RequiredFieldValidator2.Enabled = false;
                lblP1DLExpStar.Visible = false;
                txtP1ExpDate.Enabled = false;
                RegularExpressionValidatorP1DrDate.Enabled = false;
                ValidateP1DriversExpDate.Enabled = false;
                lblP1SSNStar.Visible = false;
                txtP1SSN.Enabled = false;
                ValidateP1SSN.Enabled = false;
                REP1SSN.Enabled = false;
                RegularExpressionP1Phone.Enabled = false;
                RegularExpressionValidatorMobilePhone1.Enabled = false;
                txtP1Region.Enabled = true;
            }
            else
            {
                lblP1StateStar.Visible = true;
                lstP1State.Enabled = true;
                ValidateP1State.Enabled = true;
                lblP1DLStar.Visible = true;
                lblP1DLStar.Visible = true;
                txtP1DriversLicense.Enabled = true;
                ValidateP1License.Enabled = false;
                lblP1DLStateStar.Visible = true;
                lstP1DriverState.SelectedValue = "";
                lstP1DriverState.Enabled = true;
                RequiredFieldValidator2.Enabled = false;
                txtP1ExpDate.Enabled = true;
                RegularExpressionValidatorP1DrDate.Enabled = true;
                ValidateP1DriversExpDate.Enabled = false;
                lblP1SSNStar.Visible = true;
                txtP1SSN.Enabled = true;
                ValidateP1SSN.Enabled = true;
                REP1SSN.Enabled = true;
                RegularExpressionP1Phone.Enabled = true;
                RegularExpressionValidatorMobilePhone1.Enabled = true;
                txtP1Region.Enabled = false;
            }

            textChangedEventHandler(sender, e);
        }
        catch (Exception err)
        {
            CreateOnlineAppLog Log = new CreateOnlineAppLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + Session["AppId"].ToString() + " - " + "Merchant principalinfo.aspx Error - " + err.Message);
            SetErrorMessage("Error Processing Request. Please contact Technical Support: <a href='mailto:support@commercetech.com?Subject=Online Application Error'> support@commercetech.com. </a>.");
        }
    }
    protected void lstP2Country_SelectedIndexChanged(object sender, EventArgs e)
    {
        if ((lstP2Country.SelectedItem.Text.Trim() != "United States") && (lstP2Country.SelectedItem.Text.Trim() != "Canada"))
        {
            lblP2StateStar.Visible = false;
            lstP2State.SelectedValue = "";
            lstP2State.Enabled = false;
            ValidateP2State.Enabled = false;
            lblP2DLStar.Visible = false;
            txtP2DriversLicense.Enabled = false;
            ValidateP2DriversLicense.Enabled = false;
            lblP2DLStateStar.Visible = false;
            lstP2DriverState.SelectedValue = "";
            lstP2DriverState.Enabled = false;
            RequiredFieldValidator1.Enabled = false;
            lblP2DLExpStar.Visible = false;
            txtP2ExpDate.Enabled = false;
            RegularExpressionValidatorP2DRDate.Enabled = false;
            ValidateP2DriversExpDate.Enabled = false;
            lblP2SSNStar.Visible = false;
            txtP2SSN.Enabled = false;
            ValidateP2SSN.Enabled = false;
            REP2SSN.Enabled = false;
            ValidateP2HomePhone.Enabled = false;
            RegularExpressionValidatorPhone2.Enabled = false;
            RegularExpressionValidatorMobilePhone2.Enabled = false;
            txtP2Region.Enabled = true;
        }
        else
        {
            lblP2StateStar.Visible = false;
            lstP2State.SelectedValue = "";
            lstP2State.Enabled = false;
            ValidateP2State.Enabled = false;
            lblP2DLStar.Visible = false;
            txtP2DriversLicense.Enabled = false;
            ValidateP2DriversLicense.Enabled = false;
            lblP2DLStateStar.Visible = false;
            lstP2DriverState.SelectedValue = "";
            lstP2DriverState.Enabled = false;
            RequiredFieldValidator1.Enabled = false;
            lblP2DLExpStar.Visible = false;
            txtP2ExpDate.Enabled = false;
            RegularExpressionValidatorP2DRDate.Enabled = false;
            ValidateP2DriversExpDate.Enabled = false;
            lblP2SSNStar.Visible = false;
            txtP2SSN.Enabled = false;
            ValidateP2SSN.Enabled = false;
            REP2SSN.Enabled = false;
            ValidateP2HomePhone.Enabled = false;
            RegularExpressionValidatorPhone2.Enabled = false;
            RegularExpressionValidatorMobilePhone2.Enabled = false;
            txtP2Region.Enabled = false;
        }

        textChangedEventHandler(sender, e);
    }
    protected void lnkP1DLNum_Click(object sender, EventArgs e)
    {
        /*
        txtP1DriversLicense.Enabled = true;
        txtP1DriversLicense.Text = "";
        lnkP1DLNum.Visible = false;*/
        Principal1Table PrincipalTable = new Principal1Table();
        DataSet dsPrincipal1Info = PrincipalTable.GetPrincipal1FullInfo(AppId);
        if (dsPrincipal1Info.Tables[0].Rows.Count > 0)
        {
            DataRow drPrincipal1Info = dsPrincipal1Info.Tables[0].Rows[0];

            if ((drPrincipal1Info["P1driversLicenseNo"].ToString().Trim() == "") && (drPrincipal1Info["P1Country"].ToString() == "United States"))
            {
                txtP1DriversLicense.Enabled = true;  //enable this field
                lnkP1DLNum.Visible = false;
            }
            else if ((drPrincipal1Info["P1driversLicenseNo"].ToString().Trim() != "") && (drPrincipal1Info["P1Country"].ToString() == "United States"))//contains previous data, do not enable editing and show the Edit link
            {
                txtP1DriversLicense.Text = drPrincipal1Info["P1driversLicenseNo"].ToString().Trim();
                txtP1DriversLicense.Enabled = true;
                lnkP1DLNum.Visible = true;
                ValidateP1License.Enabled = false;
                //display the masked value

            }
            else if ((drPrincipal1Info["P1driversLicenseNo"].ToString().Trim() != "") && (drPrincipal1Info["P1Country"].ToString() != "United States"))//contains previous data, do not enable editing and show the Edit link
            {
                txtP1DriversLicense.Text = drPrincipal1Info["P1driversLicenseNo"].ToString().Trim();
                txtP1DriversLicense.Enabled = true;
                lnkP1DLNum.Visible = true;
                ValidateP1License.Enabled = false;
                //display the masked value

            }
            else
            {
                ValidateP1License.Enabled = false;
                RequiredFieldValidator2.Enabled = false;
                ValidateP1DriversExpDate.Enabled = false;
            }
        }

    }
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
    protected void lnkP2DLNum_Click(object sender, EventArgs e)
    {
        /*
        txtP2DriversLicense.Enabled = true;
        txtP2DriversLicense.Text = "";
        lnkP2DLNum.Visible = false;*/
        Principal2Table Principal2Table = new Principal2Table();
        DataSet dsPrincipal2Info = Principal2Table.GetPrincipal2FullInfo(AppId);
        if (dsPrincipal2Info.Tables[0].Rows.Count > 0)
        {
            DataRow drPrincipal2Info = dsPrincipal2Info.Tables[0].Rows[0];
            if ((drPrincipal2Info["P2driversLicenseNo"].ToString().Trim() == "") && (drPrincipal2Info["P2Country"].ToString() == "United States"))
            {
                txtP2DriversLicense.Enabled = true;  //enable this field
                lnkP2DLNum.Visible = false;
            }
            else if ((drPrincipal2Info["P2driversLicenseNo"].ToString().Trim() != "") && (drPrincipal2Info["P2Country"].ToString() == "United States"))//contains previous data, do not enable editing and show the Edit link
            {
                txtP2DriversLicense.Enabled = true;
                lnkP2DLNum.Visible = true;
                //display the masked value
                txtP2DriversLicense.Text = drPrincipal2Info["P2driversLicenseNo"].ToString().Trim();
            }
            else if ((drPrincipal2Info["P2driversLicenseNo"].ToString().Trim() != "") && (drPrincipal2Info["P2Country"].ToString() != "United States"))//contains previous data, do not enable editing and show the Edit link
            {
                txtP2DriversLicense.Enabled = true;
                lnkP2DLNum.Visible = true;
                //display the masked value
                txtP2DriversLicense.Text = drPrincipal2Info["P2driversLicenseNo"].ToString().Trim();
            }
            else
            {
                ValidateP2DriversLicense.Enabled = false;
                RequiredFieldValidator1.Enabled = false;
                ValidateP2DriversExpDate.Enabled = false;
            }
        }
        
    }
    protected void lnkP2SSN_Click(object sender, EventArgs e)
    {
        /*
        txtP2SSN.Enabled = true;
        txtP2SSN.Text = "";
        lnkP2SSN.Visible = false;
        ValidateP2SSN.Enabled = true;
        REP2SSN.Enabled = true;*/

        Principal2Table Principal2Table = new Principal2Table();
        DataSet dsPrincipal2Info = Principal2Table.GetPrincipal2FullInfo(AppId);
        //if information already prsent
        if (dsPrincipal2Info.Tables[0].Rows.Count > 0)
        {
            DataRow drPrincipal2Info = dsPrincipal2Info.Tables[0].Rows[0];
            if ((drPrincipal2Info["P2SSN"].ToString().Trim() == "") && (drPrincipal2Info["P2Country"].ToString() == "United States"))
            {
                txtP2SSN.Enabled = true;
            }//enable this field
            else if ((drPrincipal2Info["P2SSN"].ToString().Trim() != "") && (drPrincipal2Info["P2Country"].ToString() == "United States"))//contains previous data, do not enable editing and show the Edit link
            {
                ValidateP1SSN.Enabled = false;
                REP2SSN.Enabled = false;
                txtP2SSN.Enabled = true;
                lnkP2SSN.Visible = true;
                //display the masked value
                txtP2SSN.Text = drPrincipal2Info["P2SSN"].ToString().Trim();
            }
            else if ((drPrincipal2Info["P2SSN"].ToString().Trim() != "") && (drPrincipal2Info["P2Country"].ToString() != "United States"))
            {
                ValidateP1SSN.Enabled = false;
                REP2SSN.Enabled = false;
                txtP2SSN.Enabled = true;
                lnkP2SSN.Visible = true;
                //display the masked value
                txtP2SSN.Text = drPrincipal2Info["P2SSN"].ToString().Trim();
            }
            else
            {
                ValidateP2SSN.Enabled = false;
                REP2SSN.Enabled = false;
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
        btnSubmit_Click(sender, e);
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
            Response.Redirect("/Application/Merchant/businessinfo.aspx", false);
        //}
    }

    protected void linkPrincipalClick(object sender, EventArgs e)
    {
        //btnSubmit_Click(sender, e);
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
            //Response.Redirect("/Application/Merchant/principalinfo.aspx", false);
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
   
