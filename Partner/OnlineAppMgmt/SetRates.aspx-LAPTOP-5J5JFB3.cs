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
using BusinessLayer;
using OnlineAppClassLibrary;
using DLPartner;
using DataLayer;
using DLPartner.PartnerDSTableAdapters;

using System.Collections.Generic;
using System.Text;

using System.Data.SqlClient;
using System.Configuration;


public partial class SetRates : System.Web.UI.Page
{
    void Page_Init(object sender, EventArgs e)
    {
        ViewStateUserKey = Session.SessionID;
    }

    private static int AppId = 0;
    private static int AcctType = 0;
    private static string CardPresent;

    private static string ConnString = ConfigurationManager.AppSettings["ConnectionString"].ToString();
    private static string ConnStringPartner = ConfigurationManager.AppSettings["ConnectionStringPartner"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.CacheControl = "no-cache";
        Response.AddHeader("Pragma", "no-cache");
        Response.Expires = -1;
        //Dampen the option for gift/loyalty.
        chkGiftCard.Enabled = false;

        if (Session.IsNewSession)
            Response.Redirect("~/login.aspx");

        if (User.IsInRole("Reseller") || User.IsInRole("Affiliate"))
            Response.Redirect("~/login.aspx?Authentication=False");

        if ((Request.Params.Get("AppId") != null))
        {
            if (Int32.TryParse(Request.Params.Get("AppId").ToString(), out AppId))
                AppId = Convert.ToInt32(Request.Params.Get("AppId"));
            else
                DisplayMessage("Invalid request");
        }

        pnlGiftCard.Visible = true;

        if (!Page.IsPostBack)
        {
            if (!User.Identity.IsAuthenticated)
                Response.Redirect("~/login.aspx?Authentication=False");
            if (AppId == 0)
                Response.Redirect("~/login.aspx");

            try
            {
                //********************************CHECK ACCESS********************************
                //Check if the partner logged in has access to the App. If not redirect to login.aspx
                bool bAccess = false;
                if (User.IsInRole("Employee") || User.IsInRole("Admin") || User.IsInRole("Office"))
                    bAccess = true;
                else
                {
                    OnlineAppBL App = new OnlineAppBL(AppId);
                    bAccess = App.CheckAccess(Session["AffiliateID"].ToString());
                }
                if (!bAccess)
                    Response.Redirect("~/login.aspx?Authentication=False");

                //********************************END CHECK ACCESS********************************

                if (Locked())
                {
                    //DisplayMessage("The application is locked because the Merchant status or the Gateway Status prevents it from being edited.");
                    if (User.IsInRole("Admin"))
                    {
                        btnApplyPackage.Enabled = true;
                        btnSubmit.Enabled = true;
                    }
                    else
                    {
                        btnApplyPackage.Enabled = false;
                        btnSubmit.Enabled = true;
                        chkMA.Enabled = false;
                        pnlMerchantRates.Enabled = false;
                        pnlProcessor.Enabled = false;
                        pnlMerchantDetails.Enabled = false;
                    }
                    //lnkBack.NavigateUrl = "Edit.aspx?AppId=" + AppId.ToString();          
                }

                if (GatewayLocked())
                {
                    //DisplayMessage("The application is locked because the Merchant status or the Gateway Status prevents it from being edited.");
                    if (User.IsInRole("Admin"))
                    {
                        btnApplyPackage.Enabled = true;
                        btnSubmit.Enabled = true;
                    }
                    else
                    {
                        btnApplyPackage.Enabled = false;
                        btnSubmit.Enabled = true;
                        chkGateway.Enabled = false;
                        pnlGatewayRates.Enabled = false;
                        pnlGatewaylist.Enabled = false;
                    }
                    //lnkBack.NavigateUrl = "Edit.aspx?AppId=" + AppId.ToString();          
                }

                if (CheckServiceLocked())
                {
                    if (User.IsInRole("Admin"))
                    {
                        pnlCheckGuarantee.Enabled = true;
                        btnSubmit.Enabled = true;
                    }
                    else
                    {
                        pnlCheckGuarantee.Enabled = false;
                        //btnSubmit.Enabled = false;
                    }
                }

                if (GiftLocked())
                {
                    if (User.IsInRole("Admin"))
                    {
                        pnlGiftCard.Enabled = true;
                        btnSubmit.Enabled = true;
                    }
                    else
                    {
                        pnlGiftCard.Enabled = false;
                        //btnSubmit.Enabled = false;
                    }
                }
                else
                {
                    pnlGiftCard.Enabled = true;
                    pnlGiftCard.Visible = true;
                }

                if (LeaseLocked())
                {
                    if (User.IsInRole("Admin"))
                    {
                        pnlLease.Enabled = true;
                        btnSubmit.Enabled = true;
                    }
                    else
                    {
                        pnlLease.Enabled = false;
                        //btnSubmit.Enabled = false;
                    }
                }

                if (MCALocked())
                {
                    if (User.IsInRole("Admin"))
                    {
                        pnlMerchantFunding.Enabled = true;
                        btnSubmit.Enabled = true;
                    }
                    else
                    {
                        pnlMerchantFunding.Enabled = false;
                        //btnSubmit.Enabled = false;
                    }
                }

                if (PayrollLocked())
                {
                    if (User.IsInRole("Admin"))
                    {
                        pnlPayroll.Enabled = true;
                        btnSubmit.Enabled = true;
                    }
                    else
                    {
                        pnlPayroll.Enabled = false;
                        //btnSubmit.Enabled = false;
                    }
                }


                /*if (rdbCP.Checked == true)
                {
                    CardPresent = "CP";
                }else if (rdbCNP.Checked == true)
                {
                    CardPresent = "CNP";
                }*/

                Populate();

                txtEBTMonFee.Enabled = false;
                chkLease.Enabled = false;
                lstLeaseCompany.Enabled = false;
                txtLeasePayment.Enabled = false;
                lstLeaseTerm.Enabled = false;
                //TestFunctionReturnProcessor();
            }//end try
            catch (Exception err)
            {
                CreateLog Log = new CreateLog();
                Log.ErrorLog(Server.MapPath("~/ErrorLog"), "SetRates Page Load - " + err.Message);
                DisplayMessage("Error retrieving rates");
            }
        }//end if not post back
        else
        {
            if (!User.Identity.IsAuthenticated)
                Response.Redirect("~/login.aspx?Authentication=False");
            if (AppId == 0)
                Response.Redirect("~/login.aspx");

            OnlineAppProcessingBL Processing = new OnlineAppProcessingBL(AppId);

            try
            {
                //********************************CHECK ACCESS********************************
                //Check if the partner logged in has access to the App. If not redirect to login.aspx
                bool bAccess = false;
                if (User.IsInRole("Employee") || User.IsInRole("Admin"))
                    bAccess = true;
                else
                {
                    OnlineAppBL App = new OnlineAppBL(AppId);
                    bAccess = App.CheckAccess(Session["AffiliateID"].ToString());
                }
                if (!bAccess)
                    Response.Redirect("~/login.aspx?Authentication=False");

                //********************************END CHECK ACCESS********************************

                PartnerDS.OnlineAppRatesDataTable dt = Processing.GetRates();

                string strComplianceFee = "";

                OnlineAppBL Compliance = new OnlineAppBL(AppId);

                strComplianceFee = Compliance.GetComplianceFee();



                if (Locked())
                {
                    //DisplayMessage("The application is locked because the Merchant status or the Gateway Status prevents it from being edited.");
                    if (User.IsInRole("Admin"))
                    {
                        btnApplyPackage.Enabled = true;
                        btnSubmit.Enabled = true;
                    }
                    else
                    {
                        btnApplyPackage.Enabled = false;
                        btnSubmit.Enabled = true;
                        chkMA.Enabled = false;
                        pnlMerchantRates.Enabled = false;
                    }
                    //lnkBack.NavigateUrl = "Edit.aspx?AppId=" + AppId.ToString();          
                }

                if (GatewayLocked())
                {
                    //DisplayMessage("The application is locked because the Merchant status or the Gateway Status prevents it from being edited.");
                    if (User.IsInRole("Admin"))
                    {
                        btnApplyPackage.Enabled = true;
                        btnSubmit.Enabled = true;
                    }
                    else
                    {
                        btnApplyPackage.Enabled = false;
                        btnSubmit.Enabled = true;
                        chkGateway.Enabled = false;
                        pnlGatewayRates.Enabled = false;
                        pnlGatewaylist.Visible = false;
                    }
                    //lnkBack.NavigateUrl = "Edit.aspx?AppId=" + AppId.ToString();          
                }

                if (CheckServiceLocked())
                {
                    if (User.IsInRole("Admin"))
                    {
                        pnlCheckGuarantee.Enabled = true;
                        btnSubmit.Enabled = true;
                    }
                    else
                    {
                        pnlCheckGuarantee.Enabled = false;
                        //btnSubmit.Enabled = false;
                    }
                }

                if (GiftLocked())
                {
                    if (User.IsInRole("Admin"))
                    {
                        pnlGiftCard.Enabled = true;
                        btnSubmit.Enabled = true;
                    }
                    else
                    {
                        pnlGiftCard.Enabled = false;
                        //btnSubmit.Enabled = false;
                    }
                }
                else
                {
                    pnlGiftCard.Visible = true;
                    pnlGiftCard.Enabled = true;
                }

                if (LeaseLocked())
                {
                    if (User.IsInRole("Admin"))
                    {
                        pnlLease.Enabled = true;
                        btnSubmit.Enabled = true;
                    }
                    else
                    {
                        pnlLease.Enabled = false;
                        //btnSubmit.Enabled = false;
                    }
                }

                if (MCALocked())
                {
                    if (User.IsInRole("Admin"))
                    {
                        pnlMerchantFunding.Enabled = true;
                        btnSubmit.Enabled = true;
                    }
                    else
                    {
                        pnlMerchantFunding.Enabled = false;
                        //btnSubmit.Enabled = false;
                    }
                }

                if (PayrollLocked())
                {
                    if (User.IsInRole("Admin"))
                    {
                        pnlPayroll.Enabled = true;
                        btnSubmit.Enabled = true;
                    }
                    else
                    {
                        pnlPayroll.Enabled = false;
                        //btnSubmit.Enabled = false;
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    if (AcctType == 1)
                    {
                        //Load upperlower bounds for processor only since its a Merchant Account
                        LoadUpperLowerBounds(lstProcessorNames.SelectedItem.Text.Trim(), CardPresent);
                        //LoadAnnualFees();

                        if (lstRateNames.SelectedItem.Text.Contains("Interchange") == true)
                        {
                            LoadInterchangeBounds();
                        }

                        //chkMA.Checked = true;
                        pnlMerchantRates.Visible = true;
                        pnlProcessor.Visible = true;
                        pnlMerchantDetails.Visible = true;
                        //pnlGatewayRates.Visible = false;
                        //lblSelectProcessor.Visible = true;
                        //rdbCNP.Visible = true;
                        //rdbCP.Visible = true;
                        //lstProcessorNames.Visible = true;
                        //chkApplyInterchange.Visible = true;
                        //chkBillAssessment.Visible = true;
                        LoadUpperLowerBoundsCS(lstCheckService.SelectedItem.Text);
                        LoadUpperLowerBoundsGiftCard(lstGCType.SelectedItem.Text);
                    }//end if account is 1
                    if (AcctType == 2)
                    {
                        //LoadAnnualFees();
                        //Load upperlower bounds for gateway only since its a gateway only Account

                        if (chkMA.Checked == true)
                        {
                            LoadUpperLowerBounds(lstProcessorNames.SelectedItem.Text.Trim(), CardPresent);
                        }
                        LoadUpperLowerBoundsGW(lstGatewayNames.SelectedItem.Text);
                        pnlMerchantRates.Visible = true;
                        pnlProcessor.Visible = false;
                        pnlMerchantDetails.Visible = false;
                        //chkGateway.Checked = true;
                        pnlGatewayRates.Visible = true;
                        pnlGatewaylist.Visible = true;
                        //lblSelectProcessor.Visible = false;
                        //rdbCNP.Visible = false;
                        //rdbCP.Visible = false;
                        //lstProcessorNames.Visible = false;
                        //chkApplyInterchange.Visible = false;
                        //chkBillAssessment.Visible = false;
                        LoadUpperLowerBoundsCS(lstCheckService.SelectedItem.Text);
                        LoadUpperLowerBoundsGiftCard(lstGCType.SelectedItem.Text);
                    }//end if account is 2
                    if (AcctType == 4)
                    {
                        //Load the lower and upper bounds for both processor and gateway
                        LoadUpperLowerBounds(lstProcessorNames.SelectedItem.Text.Trim(), CardPresent);
                        if (lstRateNames.SelectedItem.Text.Contains("Interchange") == true)
                        {
                            LoadInterchangeBounds();
                        }

                        //LoadAnnualFees();
                        LoadUpperLowerBoundsGW(lstGatewayNames.SelectedItem.Text);




                        pnlMerchantRates.Visible = true;
                        pnlProcessor.Visible = true;
                        pnlMerchantDetails.Visible = true;
                        //lblSelectProcessor.Visible = true;
                        //rdbCNP.Visible = true;
                        //rdbCP.Visible = true;
                        //lstProcessorNames.Visible = true;
                        //chkApplyInterchange.Visible = true;
                        //chkBillAssessment.Visible = true;
                        LoadUpperLowerBoundsCS(lstCheckService.SelectedItem.Text);

                        LoadUpperLowerBoundsGiftCard(lstGCType.SelectedItem.Text);
                    }//end if account is 4

                    if (AcctType == 3)
                    {

                        if (chkMA.Checked == true)
                        {
                            LoadUpperLowerBounds(lstProcessorNames.SelectedItem.Text.Trim(), CardPresent);
                        }

                        if (chkGateway.Checked == true)
                        {
                            LoadUpperLowerBoundsGW(lstGatewayNames.SelectedItem.Text);
                        }

                        pnlMerchantRates.Visible = true;
                        pnlGatewayRates.Visible = true;
                        pnlGatewaylist.Visible = false;
                        pnlProcessor.Visible = false;
                        pnlMerchantDetails.Visible = false;
                        //lblSelectProcessor.Visible = false;
                        //lstProcessorNames.Visible = false;
                        //chkApplyInterchange.Visible = false;
                        LoadUpperLowerBoundsCS(lstCheckService.SelectedItem.Text);
                        LoadUpperLowerBoundsGiftCard(lstGCType.SelectedItem.Text);
                    }
                }
                //TestFunctionReturnProcessor();
            }//end try
            catch (Exception err)
            {
                CreateLog Log = new CreateLog();
                Log.ErrorLog(Server.MapPath("~/ErrorLog"), "SetRates Page Load - " + err.Message);
                DisplayMessage("Error retrieving rates");
            }
        }

        pnlMerchantFunding.Visible = true;
    }//end page load

    //test to show the Customer Service fee low field
    /*void TestValueHiddenField_ValueChanged()
    {

        // Display the value of the HiddenField control.
        CustServLowMessage.Text = "The value of the Customer Service fee low is " + CustServLow.Value + ".";

    }*/

    //test to show the processor field
    /*void TestFunctionReturnProcessor()
    {

        // Display the value of the HiddenField control.
        TestReturnProcessorName.Text = "The processor is " + lstProcessorNames.SelectedItem.Text.Trim() + ".";

    }*/
    //This function checks if the application is locked
    protected bool Locked()
    {
        //Check whether the application is locked before redirecting
        OnlineAppStatusBL Locked = new OnlineAppStatusBL(AppId);
        string strLocked = Locked.ReturnLocked();
        if (strLocked == "Yes")
            return true;
        else
            return false;
    }

    protected bool GatewayLocked()
    {
        //Check whether the application is locked
        OnlineAppStatusBL Status = new OnlineAppStatusBL(AppId);
        string strLocked = Status.ReturnGatewayLocked();
        if (strLocked == "Yes")
            return true;
        else
            return false;
    }

    protected bool CheckServiceLocked()
    {
        //Check whether the application is locked
        OnlineAppStatusBL Status = new OnlineAppStatusBL(AppId);
        string strLocked = Status.ReturnCheckServiceLocked();
        if (strLocked == "Yes")
            return true;
        else
            return false;
    }

    protected bool GiftLocked()
    {
        //Check whether the application is locked
        OnlineAppStatusBL Status = new OnlineAppStatusBL(AppId);
        string strLocked = Status.ReturnGiftLocked();
        if (strLocked == "Yes")
            return true;
        else
            return false;
    }

    protected bool LeaseLocked()
    {
        //Check whether the application is locked
        OnlineAppStatusBL Status = new OnlineAppStatusBL(AppId);
        string strLocked = Status.ReturnLeaseLocked();
        if (strLocked == "Yes")
            return true;
        else
            return false;
    }

    protected bool MCALocked()
    {
        //Check whether the application is locked
        OnlineAppStatusBL Status = new OnlineAppStatusBL(AppId);
        string strLocked = Status.ReturnMCALocked();
        if (strLocked == "Yes")
            return true;
        else
            return false;
    }

    protected bool PayrollLocked()
    {
        //Check whether the application is locked
        OnlineAppStatusBL Status = new OnlineAppStatusBL(AppId);
        string strLocked = Status.ReturnPayrollLocked();
        if (strLocked == "Yes")
            return true;
        else
            return false;
    }

    protected void lstCheckServicePopulateItems()
    {
        lstCheckService.Items.Clear();

        lstCheckService.Items.Add(new ListItem(""));

        if (chkCheckGuarantee.Checked)
        {
            if (chkGateway.Checked == true && (lstGatewayNames.SelectedItem.Text.ToLower().Contains("authorize.net")))
            {
                lstCheckService.Items.Add(new ListItem("eCheck.Net", "eCheck.Net"));
            }
            else
            {
                if (lstProcessorNames.SelectedItem.Text.ToLower().Contains("ipay") || lstProcessorNames.SelectedItem.Text.ToLower().Contains("cardconnect"))
                {
                    lstCheckService.Items.Add(new ListItem("Telecheck", "Telecheck"));
                    lstCheckService.Items.Add(new ListItem("CrossCheck", "CrossCheck"));
                    lstCheckService.Items.Add(new ListItem("Sage EFT Check", "Sage EFT Check"));
                }
                else if (lstProcessorNames.SelectedItem.Text.ToLower().Contains("Sage"))
                {
                    lstCheckService.Items.Add(new ListItem("Sage EFT Check", "Sage EFT Check"));
                }
                else
                {
                    lstCheckService.Items.Add(new ListItem("Telecheck", "Telecheck"));
                    lstCheckService.Items.Add(new ListItem("CrossCheck", "CrossCheck"));
                    lstCheckService.Items.Add(new ListItem("Sage EFT Check", "Sage EFT Check"));
                }
            }
        }
    }

    //This function Populates Rates
    public void Populate()
    {

        pnlGiftCard.Visible = true;
        //Get Package list
        PackageBL RepPackages = new PackageBL();
        DataSet dsPackages;

        //Rate Packages shown should include Sales Rep, User logged in and ALL.
        dsPackages = RepPackages.GetPackagesForRep(Convert.ToString(Session["MasterNum"]));

        if (dsPackages.Tables[0].Rows.Count > 0)
        {
            lstPackageNames.DataSource = dsPackages;
            lstPackageNames.DataTextField = "PackageName";
            lstPackageNames.DataValueField = "PackageID";
            lstPackageNames.DataBind();
        }

        //Get CardPCT swiped
        OnlineAppProcessingBL Processing = new OnlineAppProcessingBL(AppId);
        string iPctSwiped = Processing.ReturnSwipedPercent();

        //Get Processor list. The list retrieved will depend on the Card PCT swiped
        DataSet dsProcessors = Processing.GetProcessorNames(iPctSwiped);
        if (dsProcessors.Tables[0].Rows.Count > 0)
        {
            lstProcessorNames.DataSource = dsProcessors;
            lstProcessorNames.DataTextField = "Processor";
            lstProcessorNames.DataValueField = "Processor";
            lstProcessorNames.DataBind();
        }

        if (lstRateNames.SelectedItem.Text.Contains("Interchange"))
        {
            txtDRMQ.Enabled = false;
            txtDRNQ.Enabled = false;
        }

        //Get rates based on AppId from OnlineAppProcessingBL
        PartnerDS.OnlineAppRatesDataTable dt = Processing.GetRates();

        string strComplianceFee = "";

        OnlineAppBL Compliance = new OnlineAppBL(AppId);

        strComplianceFee = Compliance.GetComplianceFee();

        if (dt.Rows.Count > 0)
        {
            Style AppIDLabel = new Style();
            AppIDLabel.ForeColor = System.Drawing.Color.White;
            AppIDLabel.Font.Size = FontUnit.Small;
            AppIDLabel.Font.Bold = true;

            lblContact.Text = "(" + dt[0].FirstName.ToString().Trim() + " " + dt[0].LastName.ToString().Trim() + ")";
            lblAppId.Text = AppId.ToString();
            lblAppId.ApplyStyle(AppIDLabel);
            lblContact.ApplyStyle(AppIDLabel);

            lblLastModifiedDate.Text = dt[0].LastModified.ToString();

            AcctType = Convert.ToInt32(dt[0].AcctType);


            lstRateNames.SelectedValue = Convert.ToString(dt[0].Interchange);


            //if Rate Package does not exist in the dropdown list, add it
            if (lstPackageNames.Items.FindByValue(dt[0].PackageID.ToString()) == null)
            {
                ListItem item = new ListItem();
                item.Value = dt[0].PackageID.ToString();
                item.Text = dt[0].PackageName.ToString();
                lstPackageNames.Items.Add(item);
            }

            if (lstPackageNames.Items.FindByValue(dt[0].PackageID.ToString()) != null)
                lstPackageNames.SelectedValue = dt[0].PackageID.ToString();

            //if Processor does not exist in the dropdown list, add it
            if (lstProcessorNames.Items.FindByValue(dt[0].Processor.ToString()) == null)
            {
                ListItem item = new ListItem();
                item.Value = dt[0].Processor.ToString();
                item.Text = dt[0].Processor.ToString();
                lstProcessorNames.Items.Add(item);
            }

            if (lstProcessorNames.Items.FindByValue(dt[0].Processor.ToString()) != null)
                lstProcessorNames.SelectedValue = dt[0].Processor.ToString();

            if (dt[0].Processor.ToString().Contains("Sage"))
            {
                txtDebitTransFee.Visible = false;
                lstDebitTransFee.Visible = true;
                txtAVS.Visible = false;
                lstAVS.Visible = true;
            }
            else
            {
                txtDebitTransFee.Visible = true;
                lstDebitTransFee.Visible = false;
                txtAVS.Visible = true;
                lstAVS.Visible = false;
            }

            if (dt[0].Processor.ToString().Contains("Sage"))
            {
                txtAVS.Enabled = false;
                txtAVS.Visible = false;

                lstAVS.Enabled = true;
                lstAVS.Visible = true;
                lstAVS.SelectedItem.Text = "0.00";
                //txtRetrievalFee.Attributes.Add("readonly", "readonly");
                //txtRetrievalFee.Text = "0.00";
                txtInternetStmt.Enabled = false;
                txtInternetStmt.Text = "0.00";
                txtBatchHeader.Enabled = false;
                if (Convert.ToString(txtTransFee.Text) != "")
                {
                    txtBatchHeader.Text = Convert.ToString(txtTransFee.Text);
                }
                else { txtBatchHeader.Text = "0.00"; }

                txtComplianceFee.Enabled = false;
                txtComplianceFee.Text = "0.00";
                /*
                if ((txtWirelessAccess.Text != "") && (txtWirelessTransFee.Text != ""))
                {
                    //txtGWTransFee.Text = "0.00";
                    //txtGWTransFee.Enabled = false;
                    lstGatewayNames.Items.Remove("ROAMpay");
                }
                else if (lstGatewayNames.SelectedItem.Text.ToLower().Contains("roampay"))
                {
                    txtWirelessAccess.Text = "";
                    txtWirelessAccess.Enabled = false;
                    txtWirelessTransFee.Text = "";
                    txtWirelessTransFee.Enabled = false;
                }*/
            }

            //Check for CP OR CNP and display addl services
            if (dt[0].CardPresent.ToString().Trim() == "CP")
            {
                rdbCP.Checked = true;
                rdbCNP.Checked = false;
                CardPresent = "CP";
                pnlOnlineDebit.Visible = true;
                pnlEBT.Visible = true;
                chkOnlineDebit.Enabled = true;
                chkEBT.Enabled = true;
            }

            if (dt[0].CardPresent.ToString().Trim() == "CNP")
            {
                rdbCNP.Checked = true;
                rdbCP.Checked = false;
                //pnlOnlineDebit.Visible = true;
                CardPresent = "CNP";
                pnlOnlineDebit.Visible = true;
                pnlEBT.Visible = true;
                chkOnlineDebit.Enabled = false;
                chkOnlineDebit.Checked = false;
                chkEBT.Enabled = false;
            }


            PopulateGatewayList();

            //PopulateChkList();

            //if (lstGatewayNames.Items.FindByValue(dt[0].Gateway.ToString()) != null)
            //lstGatewayNames.SelectedValue = dt[0].Gateway.ToString();
            if (lstGatewayNames.Items.FindByValue(dt[0].Gateway.ToString()) == null)
            {
                ListItem gwyItem = new ListItem();
                gwyItem.Value = dt[0].Gateway.ToString();
                gwyItem.Text = dt[0].Gateway.ToString();
                lstGatewayNames.Items.Add(gwyItem);
            }

            if (lstGatewayNames.Items.FindByValue(dt[0].Gateway.ToString()) != null)
                lstGatewayNames.SelectedValue = dt[0].Gateway.ToString();

            CheckProcessor();//Checks the Processor and enable or disable fields accordingly




            //Merchant Account Rates
            CardPresent = Server.HtmlEncode(dt[0].CardPresent.ToString());
            txtCustomerService.Text = Server.HtmlEncode(dt[0].CustServFee.ToString());
            txtInternetStmt.Text = Server.HtmlEncode(dt[0].InternetStmt.ToString());
            txtMonMin.Text = Server.HtmlEncode(dt[0].MonMin.ToString());
            txtTransFee.Text = Server.HtmlEncode(dt[0].TransactionFee.ToString());
            txtWirelessAccess.Text = Server.HtmlEncode(dt[0].WirelessAccessFee.ToString());
            txtWirelessTransFee.Text = Server.HtmlEncode(dt[0].WirelessTransFee.ToString());
            txtDRQP.Text = Server.HtmlEncode(dt[0].DiscRateQualPres.ToString());
            //txtDRQNP.Text = Server.HtmlEncode(dt[0].DiscRateQualNP.ToString());
            txtDRMQ.Text = Server.HtmlEncode(dt[0].DiscRateMidQual.ToString());
            txtDRNQ.Text = Server.HtmlEncode(dt[0].DiscRateNonQual.ToString());
            txtDRQD.Text = Server.HtmlEncode(dt[0].DiscRateQualDebit.ToString());

            //txtQualNPDebit.Text = Server.HtmlEncode(dt[0].DebitQualNP.ToString());
            txtMidQualDebit.Text = Server.HtmlEncode(dt[0].DebitMidQual.ToString());
            txtNonQualDebit.Text = Server.HtmlEncode(dt[0].DebitNonQual.ToString());

            txtAmexQual.Text = Server.HtmlEncode(dt[0].AmexDiscRateQual.ToString());
            txtAmexMidQual.Text = Server.HtmlEncode(dt[0].AmexDiscRateMidQual.ToString());
            txtAmexNonQual.Text = Server.HtmlEncode(dt[0].AmexDiscRateNonQual.ToString());
            txtChargebackFee.Text = Server.HtmlEncode(dt[0].ChargebackFee.ToString());
            txtRolling.Text = Server.HtmlEncode(dt[0].RollingReserve.ToString());
            txtRetrievalFee.Text = Server.HtmlEncode(dt[0].RetrievalFee.ToString());
            txtVoiceAuth.Text = Server.HtmlEncode(dt[0].VoiceAuth.ToString());
            txtBatchHeader.Text = Server.HtmlEncode(dt[0].BatchHeader.ToString());
            txtAVS.Text = Server.HtmlEncode(dt[0].AVS.ToString());
            lstAVS.SelectedItem.Text = Server.HtmlEncode(dt[0].AVS.ToString());
            txtNBCTransFee.Text = Server.HtmlEncode(dt[0].NBCTransFee.ToString());
            txtApplicationFee.Text = Server.HtmlEncode(dt[0].AppFee.ToString());
            txtSetupFee.Text = Server.HtmlEncode(dt[0].AppSetupFee.ToString());
            //Gateway Rates
            txtGWSetupFee.Text = Server.HtmlEncode(dt[0].GatewaySetupFee.ToString());
            txtGWMonthlyFee.Text = Server.HtmlEncode(dt[0].GatewayMonFee.ToString());
            txtGWTransFee.Text = Server.HtmlEncode(dt[0].GatewayTransFee.ToString());

            lstDiscountPaid.SelectedIndex = lstDiscountPaid.Items.IndexOf(lstDiscountPaid.Items.FindByText(dt[0].DiscountPaid.ToString()));
            lstContractTerm.SelectedIndex = lstContractTerm.Items.IndexOf(lstContractTerm.Items.FindByText(Convert.ToString(dt[0].ContractTerm).Trim()));

            txtComplianceFee.Text = Server.HtmlEncode(strComplianceFee);


            lstCheckServicePopulateItems();

            #region ADDITIONAL SERVICES

            //Get Additional service options from NewApp table
            OnlineAppBL AddlServ = new OnlineAppBL(AppId);
            DataSet dsAddl = AddlServ.GetAddlServiceBits();
            DataSet dsLeaseInfo = AddlServ.ReturnAddlServices();

            if (dsAddl.Tables[0].Rows.Count > 0)
            {
                DataRow drAddl = dsAddl.Tables[0].Rows[0];
                //Online Debit
                if (Convert.ToBoolean(drAddl["OnlineDebit"]))
                {
                    chkOnlineDebit.Checked = true;
                    txtDebitMonFee.Enabled = true;
                    if (lstProcessorNames.SelectedItem.Text.ToLower().Contains("Sage"))
                    {

                        lstDebitTransFee.Enabled = true;
                    }
                    else
                    {
                        txtDebitTransFee.Enabled = true;
                    }
                    //lstDebitTransFee.Enabled = true;
                    txtPinDebitDiscount.Enabled = true;
                    txtPinDebitDiscount.Text = Server.HtmlEncode(dt[0].PinDebitDiscount.ToString());
                    txtDebitMonFee.Text = Server.HtmlEncode(dt[0].DebitMonFee.ToString());
                    if (lstProcessorNames.SelectedItem.Text.ToLower().Contains("Sage"))
                    {

                        lstDebitTransFee.SelectedItem.Text = Server.HtmlEncode(Convert.ToString(dt[0].DebitTransFee));
                    }
                    else
                    {
                        txtDebitTransFee.Text = Server.HtmlEncode(Convert.ToString(dt[0].DebitTransFee));
                    }

                    /*
                    if (lstDebitTransFee.Items.FindByText(Convert.ToString(dt[0].DebitTransFee).Trim()) != null)
                    {
                        lstDebitTransFee.SelectedIndex = lstDebitTransFee.Items.IndexOf(lstContractTerm.Items.FindByText(Convert.ToString(dt[0].DebitTransFee).Trim()));
                    }
                    else if (lstDebitTransFee.Items.FindByText(Convert.ToString(dt[0].DebitTransFee).Trim()) == null)
                    {
                        ListItem item = new ListItem();
                        item.Value = dt[0].DebitTransFee.ToString();
                        item.Text = dt[0].DebitTransFee.ToString();
                        lstProcessorNames.Items.Add(item);
                    }*/
                    if (dt[0].CardPresent.ToString().Trim() == "CNP")
                    {
                        chkOnlineDebit.Checked = false;
                    }
                }
                else
                {

                    txtPinDebitDiscount.Enabled = false;
                    chkOnlineDebit.Checked = false;
                    txtDebitMonFee.Enabled = false;
                    lstDebitTransFee.Enabled = false;
                    txtDebitTransFee.Enabled = false;
                    txtDebitMonFee.Text = Server.HtmlEncode(dt[0].DebitMonFee.ToString());
                    lstDebitTransFee.SelectedItem.Text = Server.HtmlEncode(Convert.ToString(dt[0].DebitTransFee));
                    txtDebitTransFee.Text = Server.HtmlEncode(Convert.ToString(dt[0].DebitTransFee));
                    /*
                    if (lstDebitTransFee.Items.FindByText(Convert.ToString(dt[0].DebitTransFee).Trim()) != null)
                    {
                        lstDebitTransFee.SelectedIndex = lstDebitTransFee.Items.IndexOf(lstContractTerm.Items.FindByText(Convert.ToString(dt[0].DebitTransFee).Trim()));
                    }
                    else if (lstDebitTransFee.Items.FindByText(Convert.ToString(dt[0].DebitTransFee).Trim()) == null)
                    {
                        ListItem item = new ListItem();
                        item.Value = dt[0].DebitTransFee.ToString();
                        item.Text = dt[0].DebitTransFee.ToString();
                        lstProcessorNames.Items.Add(item);
                    }*/
                    //txtDebitMonFee.Text = "";
                    //txtDebitTransFee.Text = "";
                }


                //Check Guarantee
                if (Convert.ToBoolean(drAddl["CheckServices"]))
                {
                    chkCheckGuarantee.Checked = true;
                    lstCheckService.Enabled = true;
                    txtCGDiscRate.Enabled = true;
                    txtCGMonFee.Enabled = true;
                    txtCGMonMin.Enabled = true;
                    txtCGTransFee.Enabled = true;
                    //lstCheckService.SelectedIndex = lstCheckService.Items.IndexOf(lstCheckService.Items.FindByText(Server.HtmlEncode(dt[0].CheckService.ToString().Trim())));
                    //lstCheckService.Selected 
                    lstCheckService.SelectedItem.Text = Convert.ToString(dt[0].CheckService).Trim();
                    lstCheckService.SelectedItem.Value = Convert.ToString(dt[0].CheckService).Trim();
                    txtCGDiscRate.Text = Server.HtmlEncode(dt[0].CGDiscRate.ToString());
                    txtCGMonFee.Text = Server.HtmlEncode(dt[0].CGMonFee.ToString());
                    txtCGMonMin.Text = Server.HtmlEncode(dt[0].CGMonMin.ToString());
                    txtCGTransFee.Text = Server.HtmlEncode(dt[0].CGTransFee.ToString());
                    /*if ((txtCGDiscRate.Text == "") && (txtCGMonFee.Text == "") && (txtCGMonMin.Text == "") && (txtCGTransFee.Text == ""))
                    {
                        BoundsBL Bounds = new BoundsBL();
                        DataSet ds = Bounds.GetCSBounds(lstCheckService.SelectedItem.Text);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            txtCGMonFee.Text = dr["CGMonFeeDef"].ToString().Trim();
                            txtCGTransFee.Text = dr["CGTransFeeDef"].ToString().Trim();
                            txtCGMonMin.Text = dr["CGMonMinDef"].ToString().Trim();
                            txtCGDiscRate.Text = dr["CGDiscRateDef"].ToString().Trim();
                        }//end if count not 0
                    }*/
                }
                else
                {
                    chkCheckGuarantee.Checked = false;
                    lstCheckService.Enabled = false;
                    txtCGDiscRate.Enabled = false;
                    txtCGMonFee.Enabled = false;
                    txtCGMonMin.Enabled = false;
                    txtCGTransFee.Enabled = false;
                    lstCheckService.SelectedIndex = lstCheckService.Items.IndexOf(lstCheckService.Items.FindByText(""));
                    txtCGDiscRate.Text = "";
                    txtCGMonFee.Text = "";
                    txtCGMonMin.Text = "";
                    txtCGTransFee.Text = "";
                }

                //Gift Card
                if (Convert.ToBoolean(drAddl["GiftCard"]))
                {
                    pnlGiftCard.Visible = true;
                    chkGiftCard.Checked = true;
                    txtGCMonFee.Enabled = true;
                    txtGCTransFee.Enabled = true;
                    lstGCType.Enabled = true;
                    lstGCType.SelectedIndex = lstGCType.Items.IndexOf(lstGCType.Items.FindByText(dt[0].GCType.ToString()));
                    txtGCMonFee.Text = dt[0].GCMonFee.ToString();
                    txtGCTransFee.Text = dt[0].GCTransFee.ToString();
                }
                else
                {
                    lstGCType.Items.Add("");
                    pnlGiftCard.Visible = true;
                    chkGiftCard.Checked = false;
                    lstGCType.Enabled = false;
                    txtGCMonFee.Enabled = false;
                    txtGCTransFee.Enabled = false;
                    lstGCType.SelectedValue = "";
                    txtGCMonFee.Text = "";
                    txtGCTransFee.Text = "";

                    lstGCType.SelectedIndex = lstLeaseTerm.Items.IndexOf(lstLeaseTerm.Items.FindByText(""));



                    lstGCType.Text = "";
                }


                //EBT
                if (Convert.ToBoolean(drAddl["EBT"]))
                {
                    chkEBT.Checked = true;
                    txtEBTMonFee.Enabled = true;
                    txtEBTTransFee.Enabled = true;
                    txtEBTMonFee.Text = Server.HtmlEncode(dt[0].EBTMonFee.ToString());
                    txtEBTMonFee.Text = "0.00";
                    //txtEBTMonFee.Text = "";
                    txtEBTTransFee.Text = Server.HtmlEncode(dt[0].EBTTransFee.ToString());
                }
                else
                {

                    chkEBT.Checked = false;
                    txtEBTMonFee.Enabled = false;
                    txtEBTTransFee.Enabled = false;
                    //txtEBTMonFee.Text = Server.HtmlEncode(dt[0].EBTMonFee.ToString());
                    txtEBTMonFee.Text = "";
                    //txtEBTTransFee.Text = Server.HtmlEncode(dt[0].EBTTransFee.ToString());
                    //txtEBTTransFee.Text = "";
                    //txtEBTMonFee.Text = "";
                }


                //Merchant Funding
                if (Convert.ToBoolean(drAddl["MerchantFunding"]))
                {
                    chkMerchantFunding.Checked = true;
                    lstMCAType.Enabled = true;
                    lstMCAType.SelectedIndex = lstMCAType.Items.IndexOf(lstMCAType.Items.FindByText(dt[0].MCAType.ToString()));


                    if (!Convert.IsDBNull(dt[0].MCAAmount))
                    {
                        if (Convert.ToString(dt[0].MCAAmount) != "")
                        {
                            txtCashDesired.Text = Convert.ToString(Convert.ToInt32(dt[0].MCAAmount));
                        }
                        else
                        {
                            txtCashDesired.Text = "";
                        }
                        //Convert.ToInt32(dt[0].MCAAmount).ToString("#,#", CultureInfo.InvariantCulture));

                    }
                    else
                    {
                        txtCashDesired.Text = "";
                    }
                }
                else
                {
                    lstMCAType.Items.Add("");

                    chkMerchantFunding.Checked = false;
                    lstMCAType.Enabled = false;
                    lstMCAType.SelectedValue = "";
                    if (!Convert.IsDBNull(dt[0].MCAAmount))
                    {
                        txtCashDesired.Text = Convert.ToString(dt[0].MCAAmount);
                    }
                    else
                    {
                        txtCashDesired.Text = "";
                    }
                    txtCashDesired.Enabled = false;
                    lstMCAType.SelectedIndex = lstLeaseTerm.Items.IndexOf(lstLeaseTerm.Items.FindByText(""));
                    lstMCAType.Text = "";
                }

                //Payroll
                if (Convert.ToBoolean(drAddl["Payroll"]))
                {
                    chkPayroll.Checked = true;
                    lstPayrollType.Enabled = true;
                    lstPayrollType.SelectedIndex = lstPayrollType.Items.IndexOf(lstPayrollType.Items.FindByText(dt[0].PayrollType.ToString()));
                }
                else
                {
                    lstPayrollType.Items.Add("");
                    lstPayrollType.Enabled = false;
                    lstPayrollType.SelectedValue = "";
                    lstPayrollType.SelectedIndex = lstLeaseTerm.Items.IndexOf(lstLeaseTerm.Items.FindByText(""));
                    lstPayrollType.Text = "";
                }

                //Lease
                if (Convert.ToBoolean(drAddl["Lease"]))
                {
                    DataRow drLeaseInfo = dsLeaseInfo.Tables[0].Rows[0];
                    chkLease.Checked = true;
                    lstLeaseCompany.Enabled = true;
                    txtLeasePayment.Enabled = true;
                    lstLeaseTerm.Enabled = true;
                    lstLeaseCompany.SelectedIndex = lstLeaseCompany.Items.IndexOf(lstLeaseCompany.Items.FindByText(drLeaseInfo["LeaseCompany"].ToString()));
                    txtLeasePayment.Text = drLeaseInfo["LeasePayment"].ToString();
                    lstLeaseTerm.SelectedIndex = lstLeaseTerm.Items.IndexOf(lstLeaseTerm.Items.FindByText(drLeaseInfo["LeaseTerm"].ToString()));
                }
                else
                {
                    lstLeaseCompany.Items.Add("");
                    chkLease.Checked = false;
                    lstLeaseCompany.Enabled = false;
                    txtLeasePayment.Enabled = false;
                    lstLeaseTerm.Enabled = false;
                    lstLeaseCompany.SelectedValue = "";
                    txtLeasePayment.Text = "";
                    lstLeaseTerm.SelectedIndex = lstLeaseTerm.Items.IndexOf(lstLeaseTerm.Items.FindByText(""));
                    lstLeaseCompany.Text = "";
                }

                #region eCheck.Net
                if ((dt[0].Gateway.ToString().Trim().Contains("Authorize")) && (Convert.ToBoolean(drAddl["CheckServices"])))
                {
                    //chkECheck.Visible = true;

                    PartnerDS.OnlineAppECheckRatesDataTable dtECheck = Processing.GetECheckRates();
                    //if ECheck information already set
                    if (dtECheck.Rows.Count > 0)
                    {
                        //chkECheck.Checked = true;
                        pnlECheckRates.Visible = true;
                        txteSIRMonMin.Text = dtECheck[0].eSIRMonMin.ToString();
                        txteSIRDiscountRate1.Text = dtECheck[0].eSIRDiscountRate1.ToString();
                        txteSIRDiscountRate2.Text = dtECheck[0].eSIRDiscountRate2.ToString();
                        txteSIRDiscountRate3.Text = dtECheck[0].eSIRDiscountRate3.ToString();
                        txteSIRDiscountRate4.Text = dtECheck[0].eSIRDiscountRate4.ToString();
                        txteSIRTransFee1.Text = dtECheck[0].eSIRTransFee1.ToString();
                        txteSIRTransFee2.Text = dtECheck[0].eSIRTransFee2.ToString();
                        txteSIRTransFee3.Text = dtECheck[0].eSIRTransFee3.ToString();
                        txteSIRTransFee4.Text = dtECheck[0].eSIRTransFee4.ToString();
                        txtePIRMonMin.Text = dtECheck[0].ePIRMonMin.ToString();
                        txtePIRDiscountRate1.Text = dtECheck[0].ePIRDiscountRate1.ToString();
                        txtePIRTransFee1.Text = dtECheck[0].ePIRTransFee1.ToString();
                    }
                }
                //else
                //    chkECheck.Visible = false;

                #endregion
            }//end if count not 0 for addl services
            #endregion


            if (AcctType == 1)
            {
                //Load upperlower bounds for processor only since its a Merchant Account
                LoadUpperLowerBounds(lstProcessorNames.SelectedItem.Text.Trim(), CardPresent);
                LoadAnnualFees();

                chkMA.Checked = true;
                pnlMerchantRates.Visible = true;
                pnlProcessor.Visible = true;
                pnlMerchantDetails.Visible = true;
                pnlGatewayRates.Visible = true;
                pnlGatewaylist.Visible = false;
                //lblSelectProcessor.Visible = true;
                //rdbCNP.Visible = true;
                //rdbCP.Visible = true;
                //lstProcessorNames.Visible = true;
                //chkApplyInterchange.Visible = true;
                //chkBillAssessment.Visible = true;
                LoadUpperLowerBoundsCS(lstCheckService.SelectedItem.Text);
                LoadUpperLowerBoundsGiftCard(lstGCType.SelectedItem.Text);
            }//end if account is 1
            if (AcctType == 2)
            {
                LoadAnnualFees();
                //Load upperlower bounds for gateway only since its a gateway only Account
                LoadUpperLowerBoundsGW(lstGatewayNames.SelectedItem.Text);
                pnlMerchantRates.Visible = true;
                pnlProcessor.Visible = false;
                pnlMerchantDetails.Visible = false;
                chkGateway.Checked = true;
                pnlGatewayRates.Visible = true;
                pnlGatewaylist.Visible = true;
                //lblSelectProcessor.Visible = false;
                //rdbCNP.Visible = false;
                //rdbCP.Visible = false;
                //lstProcessorNames.Visible = false;
                //chkApplyInterchange.Visible = false;
                //chkBillAssessment.Visible = false;
                LoadUpperLowerBoundsCS(lstCheckService.SelectedItem.Text);
                LoadUpperLowerBoundsGiftCard(lstGCType.SelectedItem.Text);
            }//end if account is 2

            if (AcctType == 4)
            {
                //Load the lower and upper bounds for both processor and gateway
                LoadUpperLowerBounds(lstProcessorNames.SelectedItem.Text.Trim(), CardPresent);
                //LoadAnnualFees();
                LoadUpperLowerBoundsGW(lstGatewayNames.SelectedItem.Text);

                chkMA.Checked = true;
                if ((dt[0].PackageID.ToString().Trim().Equals("252")))
                {
                    chkGateway.Checked = false;
                    pnlGatewayRates.Visible = true;
                    pnlGatewaylist.Visible = false;
                }
                else if ((dt[0].PackageID.ToString().Trim().Equals("253")) || (dt[0].PackageID.ToString().Trim().Equals("254")))
                {
                    pnlGatewayRates.Visible = false;
                    chkGateway.Visible = true;
                    pnlGatewaylist.Visible = false;
                }
                else
                {
                    chkGateway.Checked = true;
                    pnlGatewayRates.Visible = true;
                    pnlGatewaylist.Visible = true;
                }
                pnlMerchantRates.Visible = true;
                pnlProcessor.Visible = true;
                pnlMerchantDetails.Visible = true;
                //lblSelectProcessor.Visible = true;
                //rdbCNP.Visible = true;
                //rdbCP.Visible = true;
                //lstProcessorNames.Visible = true;
                //chkApplyInterchange.Visible = true;
                //chkBillAssessment.Visible = true;
                LoadUpperLowerBoundsCS(lstCheckService.SelectedItem.Text);

                LoadUpperLowerBoundsGiftCard(lstGCType.SelectedItem.Text);
            }//end if account is 4

            if (AcctType == 3)
            {
                pnlMerchantRates.Visible = true;
                pnlProcessor.Visible = false;
                pnlMerchantDetails.Visible = false;
                pnlGatewayRates.Visible = true;
                pnlGatewaylist.Visible = false;
                //lblSelectProcessor.Visible = false;
                //lstProcessorNames.Visible = false;
                //chkApplyInterchange.Visible = false;
                LoadUpperLowerBoundsCS(lstCheckService.SelectedItem.Text);
                LoadUpperLowerBoundsGiftCard(lstGCType.SelectedItem.Text);
            }

            //If current Annual Fee is not part of the dropdown list, add it
            if (lstAnnualFee.Items.FindByValue(dt[0].AnnualFee.ToString()) == null)
                lstAnnualFee.Items.Add(dt[0].AnnualFee.ToString());
            lstAnnualFee.SelectedValue = dt[0].AnnualFee.ToString();
            //dimFields();



            if (dt[0].PackageID.ToString().Trim().Equals("266"))
            {
                lstContractTerm.Enabled = false;
            }

            txtApplicationFee.Visible = false;
            txtSetupFee.Visible = false;
            txtGWSetupFee.Visible = false;

            //if (Convert.ToBoolean(dt[0].Assessments.ToString() ))
            //    chkBillAssessment.Checked = true;

            //dim other fields such as Discount Rate
        }//end if count not 0
    }//end function Populate

    //This function loads upper and lower bounds for a processor
    public void LoadUpperLowerBounds(string Processor, string CardPresent)
    {
        BoundsBL Bounds = new BoundsBL();
        PartnerDS.ProcessorBoundsDataTable dt = Bounds.GetProcessorBounds(Processor, CardPresent);

        //TestReturnProcessorName.Text = "The processor is: " + Processor + ".";
        //TestReturnCardPresent.Text = "The CardPresent is: " + CardPresent + ".";
        if (dt.Rows.Count > 0)
        {
            //CustServLow.Value = dt[0].CustServLow.ToString();

            CustServLow.Value = Convert.ToString(dt[0].CustServLow);
            //CustServLowMessage.Text = "The value of the Customer Service fee low is " + CustServLow.Value + ".";
            InternetStmtLow.Value = dt[0].InternetStmtLow.ToString();
            MonMinLow.Value = dt[0].MonMinLow.ToString();
            TransFeeLow.Value = dt[0].TransFeeLow.ToString();
            DiscRateQualPresLow.Value = dt[0].DiscRateQualPresLow.ToString();
            //DiscRateQualNPLow.Value = dt[0].DiscRateQualNPLow.ToString();
            DiscRateMidQualLow.Value = dt[0].DiscRateMidQualLow.ToString();
            DiscRateNonQualLow.Value = dt[0].DiscRateNonQualLow.ToString();

            AmexDiscRateQualLow.Value = dt[0].AmexDiscRateQualLow.ToString();
            AmexDiscRateMidQualLow.Value = dt[0].AmexDiscRateMidQualLow.ToString();
            AmexDiscRateNonQualLow.Value = dt[0].AmexDiscRateNonQualLow.ToString();

            //DebitQualNPLow.Value = dt[0].DiscRateQualDebitNPLow.ToString();
            DebitMidQualLow.Value = dt[0].DiscRateQualDebitMidLow.ToString();
            DebitNonQualLow.Value = dt[0].DiscRateQualDebitNonLow.ToString();

            DiscRateQualDebitLow.Value = dt[0].DiscRateQualDebitLow.ToString();
            ChargebackFeeLow.Value = dt[0].ChargebackFeeLow.ToString();
            RetrievalFeeLow.Value = dt[0].RetrievalFeeLow.ToString();
            VoiceAuthLow.Value = dt[0].VoiceAuthLow.ToString();
            BatchHeaderLow.Value = dt[0].BatchHeaderLow.ToString();
            AVSLow.Value = dt[0].AVSLow.ToString();
            AnnualFeeLow.Value = dt[0].AnnualFeeLow.ToString();
            NBCTransFeeLow.Value = dt[0].NBCTransFeeLow.ToString();
            WirelessAccessFeeLow.Value = dt[0].WirelessAccessFeeLow.ToString();
            WirelessTransFeeLow.Value = dt[0].WirelessTransFeeLow.ToString();

            PinDebitDiscountLow.Value = dt[0].PinDebitDiscountLow.ToString();
            DebitMonFeeLow.Value = dt[0].DebitMonFeeLow.ToString();
            DebitTransFeeLow.Value = dt[0].DebitTransFeeLow.ToString();
            GCMonFeeLow.Value = dt[0].GCMonFeeLow.ToString();
            GCTransFeeLow.Value = dt[0].GCTransFeeLow.ToString();
            EBTMonFeeLow.Value = dt[0].EBTMonFeeLow.ToString();
            EBTTransFeeLow.Value = dt[0].EBTTransFeeLow.ToString();

            DiscRateMidQualStep.Value = dt[0].DiscRateMidQualStep.ToString();
            //if (lstProcessorNames.SelectedValue == "Optimal-Merrick")
            //    DiscRateNonQualStep.Value = Convert.ToString(Convert.ToDouble(dt[0].DiscRateNonQualStep) + Convert.ToDouble(dt[0].DiscRateMidQualStep));
            //else
            DiscRateNonQualStep.Value = dt[0].DiscRateNonQualStep.ToString();
            DisableFees();

        }
        //else { CustServLowMessage.Text = "The value of the Customer Service fee low is " + CustServLow.Value + "."; }

        //TestValueHiddenField_ValueChanged();//end if count not 0        
    }//end function LoadUpperLowerBounds

    //This function reloads upper and lower bounds and also reloads the Default values in the text boxes
    public void ReloadBounds()
    {
        string strCardPresent;
        OnlineAppProcessingBL Processing = new OnlineAppProcessingBL(AppId);
        PartnerDS.OnlineAppRatesDataTable onlineappdt = Processing.GetRates();
        if (onlineappdt.Rows.Count > 0)
        {
            AcctType = Convert.ToInt32(onlineappdt[0].AcctType);
        }

        /*if (lstPackageNames.Items.FindByValue(onlineappdt[0].PackageID.ToString()) == null)
        {
            ListItem item = new ListItem();
            item.Value = onlineappdt[0].PackageID.ToString();
            item.Text = onlineappdt[0].PackageName.ToString();
            lstPackageNames.Items.Add(item);
        }*/

        if (rdbCP.Checked)
            strCardPresent = "CP";
        else
            strCardPresent = "CNP";

        CardPresent = strCardPresent;

        BoundsBL Bounds = new BoundsBL();
        PartnerDS.ProcessorBoundsDataTable dt = Bounds.GetProcessorBounds(lstProcessorNames.SelectedItem.Text, strCardPresent);

        if (dt.Rows.Count > 0)
        {

            if ((AcctType != 1) && (AcctType != 4))
            {


                //Merchant Account Rates
                /*CardPresent = Server.HtmlEncode(dt[0].CardPresent.ToString().Trim());
                if ((CardPresent == "CNP") || (lstProcessor.SelectedItem.Value.ToLower().Contains("quickbooks")))
                    pnlAdditionalServices.Visible = false;
                else
                {
                    pnlAdditionalServices.Visible = true;
                    txtDebitMonFee.Text = Server.HtmlEncode(dt[0].DebitMonFeeDef.ToString());
                    txtDebitTransFee.Text = Server.HtmlEncode(dt[0].DebitTransFeeDef.ToString());
                    txtEBTMonFee.Text = Server.HtmlEncode(dt[0].EBTMonFeeDef.ToString());
                    txtEBTTransFee.Text = Server.HtmlEncode(dt[0].EBTTransFeeDef.ToString());
                }
                Processor = Server.HtmlEncode(dt[0].Processor.ToString().Trim());
                lblProcessor.Text = Server.HtmlEncode(dt[0].Processor.ToString().Trim()) + "(" + CardPresent + ")";
                lblLastModified.Text = Server.HtmlEncode(dt[0].LastModified.ToString().Trim());

                txtDRQP.Text = Server.HtmlEncode(dt[0].DiscRateQualPresDef.ToString());
                txtDRQNP.Text = Server.HtmlEncode(dt[0].DiscRateQualNPDef.ToString());
                txtDRMQ.Text = Server.HtmlEncode(dt[0].DiscRateMidQualDef.ToString());
                txtDRNQ.Text = Server.HtmlEncode(dt[0].DiscRateNonQualDef.ToString());
                txtDRQD.Text = Server.HtmlEncode(dt[0].DiscRateQualDebitDef.ToString());
                txtCustomerService.Text = Server.HtmlEncode(dt[0].CustServDef.ToString());
                txtInternetStmt.Text = Server.HtmlEncode(dt[0].InternetStmtDef.ToString());
                txtMonMin.Text = Server.HtmlEncode(dt[0].MonMinDef.ToString());
                txtTransFee.Text = Server.HtmlEncode(dt[0].TransFeeDef.ToString());
                txtWirelessAccess.Text = Server.HtmlEncode(dt[0].WirelessAccessFeeDef.ToString());
                txtWirelessTransFee.Text = Server.HtmlEncode(dt[0].WirelessTransFeeDef.ToString());
                txtChargebackFee.Text = Server.HtmlEncode(dt[0].ChargebackFeeDef.ToString());
                txtRetrievalFee.Text = Server.HtmlEncode(dt[0].RetrievalFeeDef.ToString());
                txtVoiceAuth.Text = Server.HtmlEncode(dt[0].VoiceAuthDef.ToString());
                txtBatchHeader.Text = Server.HtmlEncode(dt[0].BatchHeaderDef.ToString());
                txtAVS.Text = Server.HtmlEncode(dt[0].AVSDef.ToString());
                txtNBCTransFee.Text = Server.HtmlEncode(dt[0].NBCTransFeeDef.ToString());
                txtApplicationFee.Text = Server.HtmlEncode(dt[0].AppFeeDef.ToString());
                LoadAnnualFees();
                if (lstAnnualFee.Items.FindByValue(dt[0].AnnualFeeDef.ToString()) != null)
                    lstAnnualFee.SelectedValue = dt[0].AnnualFeeDef.ToString();

                DiscRateQualPresLow.Value = Server.HtmlEncode(dt[0].DiscRateQualPresLow.ToString());
                DiscRateQualNPLow.Value = Server.HtmlEncode(dt[0].DiscRateQualNPLow.ToString());
                DiscRateMidQualLow.Value = Server.HtmlEncode(dt[0].DiscRateMidQualLow.ToString());
                DiscRateNonQualLow.Value = Server.HtmlEncode(dt[0].DiscRateNonQualLow.ToString());
                DiscRateQualDebitLow.Value = Server.HtmlEncode(dt[0].DiscRateQualDebitLow.ToString());

                CustServLow.Value = Server.HtmlEncode(dt[0].CustServLow.ToString());
                MonMinLow.Value = Server.HtmlEncode(dt[0].MonMinLow.ToString());
                TransFeeLow.Value = Server.HtmlEncode(dt[0].TransFeeLow.ToString());
                WirelessAccessFeeLow.Value = Server.HtmlEncode(dt[0].WirelessAccessFeeLow.ToString());
                WirelessTransFeeLow.Value = Server.HtmlEncode(dt[0].WirelessTransFeeLow.ToString());
                ChargebackFeeLow.Value = Server.HtmlEncode(dt[0].ChargebackFeeLow.ToString());
                RetrievalFeeLow.Value = Server.HtmlEncode(dt[0].RetrievalFeeLow.ToString());
                VoiceAuthLow.Value = Server.HtmlEncode(dt[0].VoiceAuthLow.ToString());
                BatchHeaderLow.Value = Server.HtmlEncode(dt[0].BatchHeaderLow.ToString());
                AVSLow.Value = Server.HtmlEncode(dt[0].AVSLow.ToString());
                NBCTransFeeLow.Value = Server.HtmlEncode(dt[0].NBCTransFeeLow.ToString());
                AnnualFeeLow.Value = Server.HtmlEncode(dt[0].AnnualFeeLow.ToString());
                AppFeeLow.Value = Server.HtmlEncode(dt[0].AppFeeLow.ToString());
                //SetupFeeLow.Value = Server.HtmlEncode(dt[0].AppSetupFeeDef.ToString());

            }*/
                //Load the default values to the form
                txtDRMQ.Text = dt[0].DiscRateMidQualDef.ToString();
                txtDRNQ.Text = dt[0].DiscRateNonQualDef.ToString();
                txtDRQD.Text = dt[0].DiscRateQualDebitDef.ToString();
                //txtDRQNP.Text = dt[0].DiscRateQualNPDef.ToString();
                txtDRQP.Text = dt[0].DiscRateQualPresDef.ToString();

                txtCustomerService.Text = dt[0].CustServDef.ToString();
                txtMonMin.Text = dt[0].MonMinDef.ToString();
                txtInternetStmt.Text = dt[0].InternetStmtDef.ToString();
                txtWirelessAccess.Text = dt[0].WirelessAccessFeeDef.ToString();
                txtWirelessTransFee.Text = dt[0].WirelessTransFeeDef.ToString();

                txtComplianceFee.Text = Convert.ToString(dt[0].NonComplianceFeeDef);

                txtTransFee.Text = dt[0].TransFeeDef.ToString();
                txtBatchHeader.Text = dt[0].BatchHeaderDef.ToString();
                txtNBCTransFee.Text = dt[0].NBCTransFeeDef.ToString();
                txtAVS.Text = dt[0].AVSDef.ToString();
                lstAVS.SelectedItem.Text = dt[0].AVSDef.ToString();
                txtVoiceAuth.Text = dt[0].VoiceAuthDef.ToString();

                txtChargebackFee.Text = dt[0].ChargebackFeeDef.ToString();
                txtRetrievalFee.Text = dt[0].RetrievalFeeDef.ToString();
                txtApplicationFee.Text = dt[0].AppFeeDef.ToString();
                txtSetupFee.Text = dt[0].AppSetupFeeDef.ToString();

                LoadAnnualFees();
                if (lstAnnualFee.Items.FindByValue(dt[0].AnnualFeeDef.ToString()) != null)
                    lstAnnualFee.SelectedValue = dt[0].AnnualFeeDef.ToString();

                CustServLow.Value = dt[0].CustServLow.ToString();
                MonMinLow.Value = dt[0].MonMinLow.ToString();
                TransFeeLow.Value = dt[0].TransFeeLow.ToString();
                DiscRateQualPresLow.Value = dt[0].DiscRateQualPresLow.ToString();
                DiscRateQualNPLow.Value = dt[0].DiscRateQualNPLow.ToString();
                DiscRateMidQualLow.Value = dt[0].DiscRateMidQualLow.ToString();
                DiscRateNonQualLow.Value = dt[0].DiscRateNonQualLow.ToString();
                DiscRateQualDebitLow.Value = dt[0].DiscRateQualDebitLow.ToString();
                InternetStmtLow.Value = dt[0].InternetStmtLow.ToString();
                ChargebackFeeLow.Value = dt[0].ChargebackFeeLow.ToString();
                RetrievalFeeLow.Value = dt[0].RetrievalFeeLow.ToString();
                VoiceAuthLow.Value = dt[0].VoiceAuthLow.ToString();
                BatchHeaderLow.Value = dt[0].BatchHeaderLow.ToString();
                AVSLow.Value = dt[0].AVSLow.ToString();
                AnnualFeeLow.Value = dt[0].AnnualFeeLow.ToString();
                NBCTransFeeLow.Value = dt[0].NBCTransFeeLow.ToString();
                WirelessAccessFeeLow.Value = dt[0].WirelessAccessFeeLow.ToString();
                WirelessTransFeeLow.Value = dt[0].WirelessTransFeeLow.ToString();

                DebitMonFeeLow.Value = dt[0].DebitMonFeeLow.ToString();
                DebitTransFeeLow.Value = dt[0].DebitTransFeeLow.ToString();
                GCMonFeeLow.Value = dt[0].GCMonFeeLow.ToString();
                GCTransFeeLow.Value = dt[0].GCTransFeeLow.ToString();
                EBTMonFeeLow.Value = dt[0].EBTMonFeeLow.ToString();
                EBTTransFeeLow.Value = dt[0].EBTTransFeeLow.ToString();

                DiscRateMidQualStep.Value = dt[0].DiscRateMidQualStep.ToString();

                //if (lstProcessorNames.SelectedValue == "Optimal-Merrick")
                //    DiscRateNonQualStep.Value = Convert.ToString(Convert.ToDouble(dt[0].DiscRateNonQualStep) + Convert.ToDouble(dt[0].DiscRateMidQualStep));
                //else
                DiscRateNonQualStep.Value = dt[0].DiscRateNonQualStep.ToString();
                DisableFees();
            }
            else
            {
                //if ((lstPackageNames.Items.FindByValue(onlineappdt[0].PackageID.ToString()) == null) || (lstPackageNames.Items.FindByValue(onlineappdt[0].PackageID.ToString()).Text == "none"))
                //{
                //Load the default values to the form
                /*txtDRMQ.Text = dt[0].DiscRateMidQualDef.ToString();
                txtDRNQ.Text = dt[0].DiscRateNonQualDef.ToString();
                txtDRQD.Text = dt[0].DiscRateQualDebitDef.ToString();
                txtDRQNP.Text = dt[0].DiscRateQualNPDef.ToString();
                txtDRQP.Text = dt[0].DiscRateQualPresDef.ToString();

                txtCustomerService.Text = dt[0].CustServDef.ToString();
                txtMonMin.Text = dt[0].MonMinDef.ToString();
                txtInternetStmt.Text = dt[0].InternetStmtDef.ToString();
                txtWirelessAccess.Text = dt[0].WirelessAccessFeeDef.ToString();
                txtWirelessTransFee.Text = dt[0].WirelessTransFeeDef.ToString();

                txtTransFee.Text = dt[0].TransFeeDef.ToString();
                txtBatchHeader.Text = dt[0].BatchHeaderDef.ToString();
                txtNBCTransFee.Text = dt[0].NBCTransFeeDef.ToString();
                txtAVS.Text = dt[0].AVSDef.ToString();
                txtVoiceAuth.Text = dt[0].VoiceAuthDef.ToString();

                txtChargebackFee.Text = dt[0].ChargebackFeeDef.ToString();
                txtRetrievalFee.Text = dt[0].RetrievalFeeDef.ToString();
                txtApplicationFee.Text = dt[0].AppFeeDef.ToString();
                txtSetupFee.Text = dt[0].AppSetupFeeDef.ToString();*/


                //txtComplianceFee.Text = Convert.ToString(dt[0].NonComplianceFeeDef);

                //LoadAnnualFees();


                /*CustServLow.Value = dt[0].CustServLow.ToString();
                MonMinLow.Value = dt[0].MonMinLow.ToString();
                TransFeeLow.Value = dt[0].TransFeeLow.ToString();
                DiscRateQualPresLow.Value = dt[0].DiscRateQualPresLow.ToString();
                DiscRateQualNPLow.Value = dt[0].DiscRateQualNPLow.ToString();
                DiscRateMidQualLow.Value = dt[0].DiscRateMidQualLow.ToString();
                DiscRateNonQualLow.Value = dt[0].DiscRateNonQualLow.ToString();
                DiscRateQualDebitLow.Value = dt[0].DiscRateQualDebitLow.ToString();
                InternetStmtLow.Value = dt[0].InternetStmtLow.ToString();
                ChargebackFeeLow.Value = dt[0].ChargebackFeeLow.ToString();
                RetrievalFeeLow.Value = dt[0].RetrievalFeeLow.ToString();
                VoiceAuthLow.Value = dt[0].VoiceAuthLow.ToString();
                BatchHeaderLow.Value = dt[0].BatchHeaderLow.ToString();
                AVSLow.Value = dt[0].AVSLow.ToString();
                AnnualFeeLow.Value = dt[0].AnnualFeeLow.ToString();
                NBCTransFeeLow.Value = dt[0].NBCTransFeeLow.ToString();
                WirelessAccessFeeLow.Value = dt[0].WirelessAccessFeeLow.ToString();
                WirelessTransFeeLow.Value = dt[0].WirelessTransFeeLow.ToString();

                DebitMonFeeLow.Value = dt[0].DebitMonFeeLow.ToString();
                DebitTransFeeLow.Value = dt[0].DebitTransFeeLow.ToString();
                GCMonFeeLow.Value = dt[0].GCMonFeeLow.ToString();
                GCTransFeeLow.Value = dt[0].GCTransFeeLow.ToString();
                EBTMonFeeLow.Value = dt[0].EBTMonFeeLow.ToString();
                EBTTransFeeLow.Value = dt[0].EBTTransFeeLow.ToString();

                DiscRateMidQualStep.Value = dt[0].DiscRateMidQualStep.ToString();

                //if (lstProcessorNames.SelectedValue == "Optimal-Merrick")
                //    DiscRateNonQualStep.Value = Convert.ToString(Convert.ToDouble(dt[0].DiscRateNonQualStep) + Convert.ToDouble(dt[0].DiscRateMidQualStep));
                //else
                DiscRateNonQualStep.Value = dt[0].DiscRateNonQualStep.ToString();
                DisableFees();*/
                //}
                //else {

                txtComplianceFee.Text = Convert.ToString(dt[0].NonComplianceFeeDef);

                LoadAnnualFees();
                if (lstAnnualFee.Items.FindByValue(dt[0].AnnualFeeDef.ToString()) != null)
                    lstAnnualFee.SelectedValue = dt[0].AnnualFeeDef.ToString();

                CustServLow.Value = dt[0].CustServLow.ToString();
                MonMinLow.Value = dt[0].MonMinLow.ToString();
                TransFeeLow.Value = dt[0].TransFeeLow.ToString();
                DiscRateQualPresLow.Value = dt[0].DiscRateQualPresLow.ToString();
                DiscRateQualNPLow.Value = dt[0].DiscRateQualNPLow.ToString();
                DiscRateMidQualLow.Value = dt[0].DiscRateMidQualLow.ToString();
                DiscRateNonQualLow.Value = dt[0].DiscRateNonQualLow.ToString();
                DiscRateQualDebitLow.Value = dt[0].DiscRateQualDebitLow.ToString();
                InternetStmtLow.Value = dt[0].InternetStmtLow.ToString();
                ChargebackFeeLow.Value = dt[0].ChargebackFeeLow.ToString();
                RetrievalFeeLow.Value = dt[0].RetrievalFeeLow.ToString();
                VoiceAuthLow.Value = dt[0].VoiceAuthLow.ToString();
                BatchHeaderLow.Value = dt[0].BatchHeaderLow.ToString();
                AVSLow.Value = dt[0].AVSLow.ToString();
                AnnualFeeLow.Value = dt[0].AnnualFeeLow.ToString();
                NBCTransFeeLow.Value = dt[0].NBCTransFeeLow.ToString();
                WirelessAccessFeeLow.Value = dt[0].WirelessAccessFeeLow.ToString();
                WirelessTransFeeLow.Value = dt[0].WirelessTransFeeLow.ToString();

                DebitMonFeeLow.Value = dt[0].DebitMonFeeLow.ToString();
                DebitTransFeeLow.Value = dt[0].DebitTransFeeLow.ToString();
                GCMonFeeLow.Value = dt[0].GCMonFeeLow.ToString();
                GCTransFeeLow.Value = dt[0].GCTransFeeLow.ToString();
                EBTMonFeeLow.Value = dt[0].EBTMonFeeLow.ToString();
                EBTTransFeeLow.Value = dt[0].EBTTransFeeLow.ToString();

                DiscRateMidQualStep.Value = dt[0].DiscRateMidQualStep.ToString();

                //if (lstProcessorNames.SelectedValue == "Optimal-Merrick")
                //    DiscRateNonQualStep.Value = Convert.ToString(Convert.ToDouble(dt[0].DiscRateNonQualStep) + Convert.ToDouble(dt[0].DiscRateMidQualStep));
                //else
                DiscRateNonQualStep.Value = dt[0].DiscRateNonQualStep.ToString();
                DisableFees();
                //}
            }
        }//end if count not 0
    }//end function ReloadBounds

    public void LoadAnnualFees()
    {
        //Get annual fees
        OnlineAppProcessingBL AnnualFees = new OnlineAppProcessingBL();
        DataSet dsAnnualFee = AnnualFees.GetAnnualFee(lstProcessorNames.SelectedItem.Text, CardPresent);
        if (dsAnnualFee.Tables[0].Rows.Count > 0)
        {
            lstAnnualFee.DataSource = dsAnnualFee;
            lstAnnualFee.DataTextField = "AnnualFee";
            lstAnnualFee.DataValueField = "AnnualFee";
            lstAnnualFee.DataBind();
        }

    }//end function AnnualFees

    //This function loads upper and lower bounds for gateway account
    public void LoadUpperLowerBoundsGW(string Gateway)
    {
        BoundsBL Bounds = new BoundsBL();
        PartnerDS.GatewayBoundsDataTable dt = Bounds.GetGatewayBounds(Gateway);
        if (dt.Rows.Count > 0)
        {
            GatewayMonFeeLow.Value = dt[0].GatewayMonFeeLow.ToString();
            GatewayTransFeeLow.Value = dt[0].GatewayTransFeeLow.ToString();
            GatewaySetupFeeLow.Value = dt[0].GatewaySetupFeeLow.ToString();
            //populate defaults in the fields
            //txtGWSetupFee.Text = dt[0].GatewaySetupFeeDef.ToString().Trim();
            //txtGWMonthlyFee.Text = dt[0].GatewayMonFeeDef.ToString().Trim();
            //txtGWTransFee.Text = dt[0].GatewayTransFeeDef.ToString().Trim();
        }//end if count not 0
        /*if (chkCheckGuarantee.Checked)
        {
            if (lstGatewayNames.SelectedItem.Text.Contains("Optimal"))
                lstCheckService.SelectedIndex = lstCheckService.Items.IndexOf(lstCheckService.Items.FindByText("Direct Debit"));
            else if (lstGatewayNames.SelectedItem.Text.Contains("Authorize"))
                lstCheckService.SelectedIndex = lstCheckService.Items.IndexOf(lstCheckService.Items.FindByText("eCheck.Net"));
        }*/
    }//end function LoadUpperLowerBoundsGW

    //This function loads upper and lower bounds for Check Services account
    public void LoadUpperLowerBoundsCS(string CheckService)
    {
        BoundsBL Bounds = new BoundsBL();
        DataSet ds = Bounds.GetCSBounds(CheckService);
        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr = ds.Tables[0].Rows[0];
            CGMonFeeLow.Value = dr["CGMonFeeLow"].ToString().Trim();
            CGTransFeeLow.Value = dr["CGTransFeeLow"].ToString().Trim();
            CGMonMinLow.Value = dr["CGMonMinLow"].ToString().Trim();
            CGDiscRateLow.Value = dr["CGDiscRateLow"].ToString().Trim();

            //txtCGDiscRate.Text = dr["CGDiscRateDef"].ToString().Trim();
            //txtCGMonFee.Text = dr["CGMonFeeDef"].ToString().Trim();
            //txtCGMonMin.Text = dr["CGMonMinDef"].ToString().Trim();
            //txtCGTransFee.Text = dr["CGTransFeeDef"].ToString().Trim();

        }//end if count not 0
    }//end function LoadUpperLowerBoundsCS

    public void LoadUpperLowerBoundsGiftCard(string GiftCard)
    {

        BoundsBL Rates = new BoundsBL();
        DataSet ds = Rates.GiftCardBounds(GiftCard);
        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr = ds.Tables[0].Rows[0];
            //Gift Card Rates
            //txtGCMonFee.Text = dr["GiftCardMonFeeDef"].ToString().Trim();
            //txtGCTransFee.Text = dr["GiftCardTransFeeDef"].ToString().Trim();

            GCMonFeeLow.Value = dr["GiftCardMonFeeLow"].ToString().Trim();
            GCTransFeeLow.Value = dr["GiftCardTransFeeLow"].ToString().Trim();
        }//end if count not 0
    }//end function LoadUpperLowerBoundsCS

    //This function loads upper and lower bounds for gateway account
    public void LoadDefaultRateGW(string Gateway)
    {
        BoundsBL Bounds = new BoundsBL();
        PartnerDS.GatewayBoundsDataTable dt = Bounds.GetGatewayBounds(Gateway);
        if (dt.Rows.Count > 0)
        {
            //GatewayMonFeeLow.Value = dt[0].GatewayMonFeeLow.ToString();
            //GatewayTransFeeLow.Value = dt[0].GatewayTransFeeLow.ToString();
            //GatewaySetupFeeLow.Value = dt[0].GatewaySetupFeeLow.ToString();

            //populate defaults in the fields
            txtGWSetupFee.Text = dt[0].GatewaySetupFeeDef.ToString().Trim();
            txtGWMonthlyFee.Text = dt[0].GatewayMonFeeDef.ToString().Trim();
            txtGWTransFee.Text = dt[0].GatewayTransFeeDef.ToString().Trim();

        }//end if count not 0
        /*if (chkCheckGuarantee.Checked)
        {
            if (lstGatewayNames.SelectedItem.Text.Contains("Optimal"))
                lstCheckService.SelectedIndex = lstCheckService.Items.IndexOf(lstCheckService.Items.FindByText("Direct Debit"));
            else if (lstGatewayNames.SelectedItem.Text.Contains("Authorize"))
                lstCheckService.SelectedIndex = lstCheckService.Items.IndexOf(lstCheckService.Items.FindByText("eCheck.Net"));
        }*/
    }//end function LoadUpperLowerBoundsGW

    //This function loads upper and lower bounds for Check Services account
    public void LoadDefaultRateCS(string CheckService)
    {
        BoundsBL Bounds = new BoundsBL();
        DataSet ds = Bounds.GetCSBounds(CheckService);
        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr = ds.Tables[0].Rows[0];
            //CGMonFeeLow.Value = dr["CGMonFeeLow"].ToString().Trim();
            //CGTransFeeLow.Value = dr["CGTransFeeLow"].ToString().Trim();
            //CGMonMinLow.Value = dr["CGMonMinLow"].ToString().Trim();
            //CGDiscRateLow.Value = dr["CGDiscRateLow"].ToString().Trim();

            txtCGDiscRate.Text = dr["CGDiscRateDef"].ToString().Trim();
            txtCGMonFee.Text = dr["CGMonFeeDef"].ToString().Trim();
            txtCGMonMin.Text = dr["CGMonMinDef"].ToString().Trim();
            txtCGTransFee.Text = dr["CGTransFeeDef"].ToString().Trim();

        }//end if count not 0
    }//end function LoadUpperLowerBoundsCS

    public void LoadDefaultRateGiftCard(string GiftCard)
    {

        BoundsBL Rates = new BoundsBL();
        DataSet ds = Rates.GiftCardBounds(GiftCard);
        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr = ds.Tables[0].Rows[0];
            //Gift Card Rates
            txtGCMonFee.Text = dr["GiftCardMonFeeDef"].ToString().Trim();
            txtGCTransFee.Text = dr["GiftCardTransFeeDef"].ToString().Trim();

            //GCMonFeeLow.Value = dr["GiftCardMonFeeLow"].ToString().Trim();
            //GCTransFeeLow.Value = dr["GiftCardTransFeeLow"].ToString().Trim();
        }//end if count not 0
    }//end function LoadUpperLowerBoundsCS

    //This function disables fees textbox based on processor selected
    public void DisableFees()
    {
        #region Enable all fees before Disabling

        txtDRQP.Enabled = true;
        txtCustomerService.Enabled = true;
        txtTransFee.Enabled = true;
        lstAnnualFee.Enabled = true;
        txtDRQNP.Enabled = false;
        txtMonMin.Enabled = true;
        txtBatchHeader.Enabled = true;
        txtChargebackFee.Enabled = true;
        txtDRMQ.Enabled = true;
        txtInternetStmt.Enabled = true;
        txtNBCTransFee.Enabled = true;
        txtRetrievalFee.Enabled = true;
        txtDRNQ.Enabled = true;
        txtWirelessAccess.Enabled = true;
        txtAVS.Enabled = true;
        txtRolling.Enabled = true;
        txtDRQD.Enabled = true;
        txtWirelessTransFee.Enabled = true;
        txtVoiceAuth.Enabled = true;
        txtApplicationFee.Enabled = true;
        txtSetupFee.Enabled = true;
        txtQualNPDebit.Enabled = true;
        txtDRQD.Enabled = true;
        txtQualNPDebit.Enabled = true;
        txtMidQualDebit.Enabled = true;
        txtNonQualDebit.Enabled = true;
        txtComplianceFee.Enabled = true;

        #endregion

        if (AVSLow.Value != "")
            txtAVS.Enabled = true;
        else
        {
            txtAVS.Enabled = false;
            txtAVS.Text = "";
        }

        if (NBCTransFeeLow.Value != "")
            txtNBCTransFee.Enabled = true;
        else
        {
            txtNBCTransFee.Enabled = false;
            txtNBCTransFee.Text = "";
        }

        /*if (WirelessAccessFeeLow.Value != "")
            txtWirelessAccess.Enabled = true;
        else
        {
            txtWirelessAccess.Enabled = true;
            txtWirelessAccess.Text = "";
        }*/

        /*if (WirelessTransFeeLow.Value != "")
            txtWirelessTransFee.Enabled = true;
        else
        {
            txtWirelessTransFee.Enabled = true;
            txtWirelessTransFee.Text = "";
        }*/

        if (DiscRateMidQualLow.Value != "")
            txtDRMQ.Enabled = true;
        else
        {
            txtDRMQ.Enabled = false;
            //txtDRMQ.Text = "";
        }

        if (CustServLow.Value != "")
            txtCustomerService.Enabled = true;
        else
        {
            txtCustomerService.Enabled = false;
            txtCustomerService.Text = "";
        }

        if (DiscRateQualPresLow.Value != "")
            txtDRQP.Enabled = true;
        else
        {
            txtDRQP.Enabled = true;
            //txtDRQP.Text = "";
        }

        if (AmexDiscRateQualLow.Value != "")
            txtAmexQual.Enabled = true;
        else
        {
            txtAmexQual.Enabled = false;
            //txtAmexQual.Text = "";
        }

        if (AmexDiscRateMidQualLow.Value != "")
            txtAmexMidQual.Enabled = true;
        else
        {
            txtAmexMidQual.Enabled = false;
            //txtAmexMidQual.Text = "";
        }

        if (AmexDiscRateNonQualLow.Value != "")
            txtAmexNonQual.Enabled = true;
        else
        {
            txtAmexNonQual.Enabled = false;
            //txtAmexNonQual.Text = "";
        }

        if (DiscRateQualPresLow.Value != "")
            txtDRQD.Enabled = true;
        else
        {
            txtDRQD.Enabled = true;
            //txtDRQP.Text = "";
        }

        if (DiscRateQualNPLow.Value != "")
            txtQualNPDebit.Enabled = true;
        else
        {
            txtQualNPDebit.Enabled = false;
            //txtQualNPDebit.Text = "";
        }

        /*
        if (DiscRateQualNPLow.Value != "")
            txtDRQNP.Enabled = true;
        else
        {
            txtDRQNP.Enabled = false;
            
        }*/

        if (DiscRateNonQualLow.Value != "")
            txtDRNQ.Enabled = true;
        else
        {
            txtDRNQ.Enabled = false;
            //txtDRNQ.Text = "";
        }

        if (DiscRateQualDebitLow.Value != "")
        {
            txtDRQD.Enabled = true;
            txtBatchHeader.Enabled = true;
        }
        else
        {
            txtDRQD.Enabled = true;
            txtBatchHeader.Enabled = true;
        }

        //retrieval fee disabled for all processors except ipayment
        if (lstProcessorNames.SelectedItem.Text.Contains("iPayment"))
            txtRetrievalFee.Enabled = true;

        if (lstProcessorNames.SelectedItem.Text.Contains("iPayment") || lstProcessorNames.SelectedItem.Text.Contains("chase") || lstProcessorNames.SelectedItem.Text.Contains("CardConnect"))
        {
            txtChargebackFee.Enabled = true;
            //txtRetrievalFee.Enabled = true;
            txtVoiceAuth.Enabled = true;
            txtComplianceFee.Enabled = true;
            //txtComplianceFee.Enabled = false;
        }

        if (lstProcessorNames.SelectedItem.Text == "IMS")
        {
            //txtDRQD.Enabled = false;
            //txtChargebackFee.Text = "20";
            txtChargebackFee.Enabled = false;
            //txtRetrievalFee.Text = "20";
            //txtRetrievalFee.Enabled = false;
            //txtVoiceAuth.Text = "0.95";
            txtVoiceAuth.Enabled = false;
            //txtBatchHeader.Enabled = false;
        }

        if (lstProcessorNames.SelectedItem.Text.Contains("Intuit"))
        {
            //txtDRQD.Enabled = false;
            //txtChargebackFee.Text = "20";
            txtChargebackFee.Enabled = false;
            //txtRetrievalFee.Text = "20";
            //txtRetrievalFee.Enabled = false;
            //txtVoiceAuth.Text = "0.95";
            txtVoiceAuth.Enabled = false;
            //txtBatchHeader.Enabled = false;
            txtComplianceFee.Enabled = false;
            txtComplianceFee.Text = "0.00";
        }

        if (lstProcessorNames.SelectedItem.Text.Contains("Optimal-Merrick"))
        {
            txtDRQD.Enabled = true;
            //txtDRQD.Text = "";
            txtVoiceAuth.Enabled = false;
            txtVoiceAuth.Text = "";
            txtBatchHeader.Enabled = false;
            txtBatchHeader.Text = "0.00";
            txtComplianceFee.Enabled = false;
            txtComplianceFee.Text = "0.00";
        }

        if ((lstProcessorNames.SelectedItem.Text.Contains("Payvision-Merrick")) || (lstProcessorNames.SelectedItem.Text.Contains("Payvision-B+S")))
        {
            txtBatchHeader.Enabled = true;
            //txtBatchHeader.Text = "0.00";
        }

        if (lstProcessorNames.SelectedItem.Text.Contains("Sage"))
        {
            txtAVS.Enabled = false;
            txtAVS.Visible = false;

            lstAVS.Enabled = true;
            lstAVS.Visible = true;
            lstAVS.SelectedItem.Text = "0.00";
            //txtRetrievalFee.Attributes.Add("readonly", "readonly");
            //txtRetrievalFee.Text = "0.00";
            txtInternetStmt.Enabled = false;
            txtInternetStmt.Text = "0.00";
            txtBatchHeader.Enabled = false;
            if (Convert.ToString(txtTransFee.Text) != "")
            {
                txtBatchHeader.Text = Convert.ToString(txtTransFee.Text);
            }
            else { txtBatchHeader.Text = "0.00"; }

            txtComplianceFee.Enabled = false;
            txtComplianceFee.Text = "0.00";
            /*
            if ((txtWirelessAccess.Text != "") && (txtWirelessTransFee.Text != ""))
            {
                //txtGWTransFee.Text = "0.00";
                //txtGWTransFee.Enabled = false;
                lstGatewayNames.Items.Remove("ROAMpay");
            }
            else if (lstGatewayNames.SelectedItem.Text.ToLower().Contains("roampay"))
            {
                txtWirelessAccess.Text = "";
                txtWirelessAccess.Enabled = false;
                txtWirelessTransFee.Text = "";
                txtWirelessTransFee.Enabled = false;
            }*/
        }

        if (User.IsInRole("Agent") && lstProcessorNames.SelectedItem.Text.Contains("QuickBooks"))
        {
            txtDRQP.Enabled = true;
            txtCustomerService.Enabled = false;
            txtTransFee.Enabled = false;
            lstAnnualFee.Enabled = false;
            txtDRQNP.Enabled = false;
            txtMonMin.Enabled = false;
            txtBatchHeader.Enabled = false;
            txtChargebackFee.Enabled = false;
            txtDRMQ.Enabled = false;
            txtInternetStmt.Enabled = false;
            txtNBCTransFee.Enabled = false;
            txtRetrievalFee.Enabled = false;
            txtDRNQ.Enabled = false;
            txtWirelessAccess.Enabled = false;
            txtAVS.Enabled = false;
            txtRolling.Enabled = false;
            txtDRQD.Enabled = true;
            txtWirelessTransFee.Enabled = false;
            txtVoiceAuth.Enabled = false;
            txtApplicationFee.Enabled = false;
            txtSetupFee.Enabled = false;
            txtComplianceFee.Enabled = false;
            txtComplianceFee.Text = "0.00";
        }

    }//end function DisableFees

    protected void chkECheck_CheckedChanged(object sender, EventArgs e)
    {
        if (((CheckBox)sender).Checked)
        {
            pnlECheckRates.Visible = true;

            //if no previous text in field
            if (txteSIRMonMin.Text.Trim() == "")
                txteSIRMonMin.Text = "10.00";

            //if no previous text in field
            if (txteSIRDiscountRate1.Text.Trim() == "")
                txteSIRDiscountRate1.Text = "1.75";
            //if no previous text in field
            if (txteSIRDiscountRate2.Text.Trim() == "")
                txteSIRDiscountRate2.Text = "1.50";
            //if no previous text in field
            if (txteSIRDiscountRate3.Text.Trim() == "")
                txteSIRDiscountRate3.Text = "1.00";
            //if no previous text in field
            if (txteSIRDiscountRate4.Text.Trim() == "")
                txteSIRDiscountRate4.Text = "0.50";

            //if no previous text in field
            if (txteSIRTransFee1.Text.Trim() == "")
                txteSIRTransFee1.Text = "0.30";
            //if no previous text in field
            if (txteSIRTransFee2.Text.Trim() == "")
                txteSIRTransFee2.Text = "0.30";
            //if no previous text in field
            if (txteSIRTransFee3.Text.Trim() == "")
                txteSIRTransFee3.Text = "0.30";
            //if no previous text in field
            if (txteSIRTransFee4.Text.Trim() == "")
                txteSIRTransFee4.Text = "0.30";

            //if no previous text in field
            if (txtePIRMonMin.Text.Trim() == "")
                txtePIRMonMin.Text = "10.00";


            //if no previous text in field
            if (txtePIRDiscountRate1.Text.Trim() == "")
                txtePIRDiscountRate1.Text = "1.75";

            //if no previous text in field
            if (txtePIRTransFee1.Text.Trim() == "")
                txtePIRTransFee1.Text = "0.30";

        }
        else
            pnlECheckRates.Visible = false;
    }

    #region APPLY PACKAGE BUTTON CLICK
    protected void btnApplyPackage_Click(object sender, EventArgs e)
    {
        pnlApplyPackage.Visible = true;
    }

    protected void btnApplyPkgYes_Click(object sender, EventArgs e)
    {
        try
        {
            pnlApplyPackage.Visible = false;
            if (lstPackageNames.SelectedItem.Text != "")
            {
                chkMA.Checked = true;
                OnlineAppProfile AType = new OnlineAppProfile(AppId);
                int iAType = 0;
                if ((chkMA.Checked) && (chkGateway.Checked))
                    iAType = 4;
                else if (chkMA.Checked)
                    iAType = 1;
                else if (chkGateway.Checked)
                    iAType = 2;
                else
                    iAType = 3;
                AType.UpdateAcctType(iAType);
                //Apply the Rates Package

                OnlineAppProcessingBL ApplyPackage = new OnlineAppProcessingBL(AppId);
                string retVal = ApplyPackage.ApplyPackageCheckOnlineDebitEBT(Convert.ToInt32(lstPackageNames.SelectedValue), false, false);
                //string retVal = ApplyPackage.ApplyPackageCheckOnlineDebitEBT(Convert.ToInt32(lstPackageNames.SelectedValue), chkOnlineDebit.Checked, chkEBT.Checked);
                DisplayMessage(retVal);
                //change account type



                //Add action to Log table
                PartnerLogBL LogData = new PartnerLogBL();
                Populate();
                if (!retVal.Contains("could not be applied"))
                {
                    LogData.InsertLogData(AppId, Convert.ToInt32(Session["AffiliateID"]), "New Rates Package (" + lstPackageNames.SelectedItem.Text + ") Applied");
                }
            }//end if package select not blank
            else
                DisplayMessage("Please select a Package from the list before applying.");
        }//end try
        catch (Exception err)
        {
            CreateLog Log = new CreateLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), err.Message);
            DisplayMessage("Error applying package.");
        }
    }//end apply package button click

    protected void btnApplyPkgNo_Click(object sender, EventArgs e)
    {
        pnlApplyPackage.Visible = false;
    }
    #endregion

    #region SUBMIT BUTTON CLICK
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string strCardPresent;
            if (rdbCP.Checked)
                strCardPresent = "CP";
            else
                strCardPresent = "CNP";
            string errMessage = "";

            string pinDebitTransFee = "";

            string AVSFee = "";

            errMessage = ValidateRates();

            //errMessage = ValidateProcessorRates();

            //errMessage = ValidateGatewayRates();

            //OnlineAppProcessingBL Processing1 = new OnlineAppProcessingBL(AppId);
            //PartnerDS.OnlineAppRatesDataTable dt = Processing1.GetRates();
            /*if (dt[0].CardPresent.ToString().Trim() == "CP")
            {
                rdbCP.Checked = true;
                CardPresent = "CP";
                pnlOnlineDebit.Visible = true;
                pnlEBT.Visible = true;
            }
            else
            {
                rdbCNP.Checked = true;
                //pnlOnlineDebit.Visible = true;
                CardPresent = "CNP";
                pnlOnlineDebit.Visible = true;
                pnlEBT.Visible = true;
            }*/

            if (errMessage == "")
            {
                //Update NewApp table Addl Services bits
                OnlineAppBL UpdateAddlServ = new OnlineAppBL(AppId);
                bool retVal = UpdateAddlServ.UpdateAddlServices(chkOnlineDebit.Checked, chkCheckGuarantee.Checked,
                    chkGiftCard.Checked, chkEBT.Checked, chkLease.Checked, chkMerchantFunding.Checked, chkPayroll.Checked);



                string BatchHeader = txtBatchHeader.Text.Trim();
                if (lstProcessorNames.SelectedItem.Text.ToLower().Contains("ims"))
                    BatchHeader = txtTransFee.Text.Trim();
                if (lstProcessorNames.SelectedItem.Text.ToLower().Contains("intuit"))
                    BatchHeader = txtTransFee.Text.Trim();

                if (lstProcessorNames.SelectedItem.Text.ToLower().Contains("Sage"))
                {

                    pinDebitTransFee = lstDebitTransFee.SelectedItem.Text;
                    AVSFee = lstAVS.SelectedItem.Text;
                }
                else
                {
                    pinDebitTransFee = txtDebitTransFee.Text;
                    AVSFee = txtAVS.Text;
                }

                //Update AcctType based on check boxes on this page
                OnlineAppProfile AType = new OnlineAppProfile(AppId);
                int iAType = 0;
                if ((chkMA.Checked) && (chkGateway.Checked))
                    iAType = 4;
                else if (chkMA.Checked)
                    iAType = 1;
                else if (chkGateway.Checked)
                    iAType = 2;
                else
                    iAType = 3;

                AType.UpdateAcctType(iAType);

                //Check Gateway sales opps and update it.
                Gateway GWRates = new Gateway(AppId);
                DataSet dsGateway = GWRates.GetGatewayInfo();
                if (chkGateway.Checked)
                {
                    string strAType = "Gateway";
                    NewAppInfo AppInfo = new NewAppInfo(AppId);
                    string MerchStatus = AppInfo.ReturnStatus();
                    if ((MerchStatus.ToLower().Contains("completed")) || (MerchStatus.ToLower().Contains("submitted")) || (MerchStatus.ToLower().Contains("active")))
                    //if (!(MerchStatus.ToLower().Contains("incomplete")))
                    {
                        AppInfo.UpdateReprogramStatus(1);
                        AppInfo.UpdateStatus("COMPLETED", strAType);
                    }
                    else if (MerchStatus.ToLower().Contains("incomplete"))
                        AppInfo.UpdateStatus("INCOMPLETE", strAType);


                    OnlineAppClassLibrary.Gateway GatewayInfo2 = new OnlineAppClassLibrary.Gateway(AppId);
                    retVal = GatewayInfo2.UpdateGatewayInfo(lstGatewayNames.SelectedItem.Text, txtGWMonthlyFee.Text.Trim(), txtGWSetupFee.Text.Trim(), txtGWTransFee.Text.Trim());


                    CommonFunctions UpdateGeneralInfo = new CommonFunctions(AppId);
                    //UpdateGeneralInfo.SetPageCount();
                    UpdateGeneralInfo.SetGWPageCount();

                    CommonFunctions Common = new CommonFunctions(AppId);
                    Common.UpdateLastModified();
                    Common.SetGWPageCount();

                    if (dsGateway.Tables[0].Rows.Count > 0)
                    {
                        DataRow drGateway = dsGateway.Tables[0].Rows[0];
                        if (lstGatewayNames.SelectedItem.Text != drGateway["Gateway"].ToString().Trim())
                        {
                            BusinessLayer.SalesOppsBL SalesOpps = new BusinessLayer.SalesOppsBL();
                            PartnerDS.OnlineAppSalesOppsDataTable dtGwySalesOpp = SalesOpps.GetSalesOpps(AppId);
                            if (dtGwySalesOpp.Rows.Count > 0)
                            {
                                string GwySalesOppID = "";
                                for (int i = 0; i < dtGwySalesOpp.Rows.Count; i++)
                                {
                                    DataRow drGwySalesOpp = dtGwySalesOpp.Rows[i];

                                    //if ((drGwySalesOpp["Product"].ToString().Trim()) == (drGateway["Gateway"].ToString().Trim()))
                                    GwySalesOppID = drGwySalesOpp["ID"].ToString().Trim();
                                    DLPartner.SalesOppDL GwySalesOppsDL = new DLPartner.SalesOppDL();
                                    OnlineAppClassLibrary.Gateway GatewayInfo = new OnlineAppClassLibrary.Gateway(AppId);
                                    retVal = GatewayInfo.UpdateGatewayInfo(lstGatewayNames.SelectedItem.Text, txtGWMonthlyFee.Text.Trim(), txtGWSetupFee.Text.Trim(), txtGWTransFee.Text.Trim());
                                    OnlineAppClassLibrary.SalesOppsBL SalesOppGW = new OnlineAppClassLibrary.SalesOppsBL(AppId);
                                    SalesOppGW.CreateSalesOppsGW();
                                    if ((Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 5) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 20) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 36) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 60) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 65) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 74) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 96) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 150) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 253) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 282))
                                    {
                                        int retvalSaleACT = GwySalesOppsDL.SalesOppAddedToACT(GwySalesOppID);
                                        if (retvalSaleACT == 0)
                                        {
                                            BusinessLayer.SalesOppsBL DeleteOpp = new BusinessLayer.SalesOppsBL();
                                            bool retDelete = DeleteOpp.DeleteSalesOpp(GwySalesOppID);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                OnlineAppClassLibrary.Gateway GatewayInfo = new OnlineAppClassLibrary.Gateway(AppId);
                                retVal = GatewayInfo.UpdateGatewayInfo(lstGatewayNames.SelectedItem.Text, txtGWMonthlyFee.Text.Trim(), txtGWSetupFee.Text.Trim(), txtGWTransFee.Text.Trim());
                                OnlineAppClassLibrary.SalesOppsBL SalesOppGW = new OnlineAppClassLibrary.SalesOppsBL(AppId);
                                SalesOppGW.CreateSalesOppsGW();
                            }

                        }
                    }
                    else
                    {
                        BusinessLayer.SalesOppsBL SalesOpps = new BusinessLayer.SalesOppsBL();
                        PartnerDS.OnlineAppSalesOppsDataTable dtGwySalesOpp = SalesOpps.GetSalesOpps(AppId);
                        if (dtGwySalesOpp.Rows.Count > 0)
                        {
                            //if ((drGwySalesOpp["Product"].ToString().Trim()) != (lstGatewayNames.SelectedItem.Text))
                            string GwySalesOppID = "";
                            for (int i = 0; i < dtGwySalesOpp.Rows.Count; i++)
                            {
                                DataRow drGwySalesOpp = dtGwySalesOpp.Rows[i];

                                GwySalesOppID = drGwySalesOpp["ID"].ToString().Trim();
                                DLPartner.SalesOppDL GwySalesOppsDL = new DLPartner.SalesOppDL();
                                OnlineAppClassLibrary.Gateway GatewayInfo = new OnlineAppClassLibrary.Gateway(AppId);
                                retVal = GatewayInfo.UpdateGatewayInfo(lstGatewayNames.SelectedItem.Text, txtGWMonthlyFee.Text.Trim(), txtGWSetupFee.Text.Trim(), txtGWTransFee.Text.Trim());
                                OnlineAppClassLibrary.SalesOppsBL SalesOppGW = new OnlineAppClassLibrary.SalesOppsBL(AppId);
                                SalesOppGW.CreateSalesOppsGW();
                                if ((Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 5) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 20) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 36) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 60) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 65) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 74) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 96) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 150) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 253) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 282))
                                {
                                    int retvalSaleACT = GwySalesOppsDL.SalesOppAddedToACT(GwySalesOppID);
                                    if (retvalSaleACT == 0)
                                    {
                                        BusinessLayer.SalesOppsBL DeleteOpp = new BusinessLayer.SalesOppsBL();
                                        bool retDelete = DeleteOpp.DeleteSalesOpp(GwySalesOppID);
                                    }
                                }
                            }
                        }
                        else
                        {
                            OnlineAppClassLibrary.Gateway GatewayInfo = new OnlineAppClassLibrary.Gateway(AppId);
                            retVal = GatewayInfo.UpdateGatewayInfo(lstGatewayNames.SelectedItem.Text, txtGWMonthlyFee.Text.Trim(), txtGWSetupFee.Text.Trim(), txtGWTransFee.Text.Trim());
                            OnlineAppClassLibrary.SalesOppsBL SalesOppGW = new OnlineAppClassLibrary.SalesOppsBL(AppId);
                            SalesOppGW.CreateSalesOppsGW();
                        }
                    }
                }
                else
                {
                    //if (dsGateway.Tables[0].Rows.Count > 0)
                    //{
                    //DataRow drGateway = dsGateway.Tables[0].Rows[0];
                    BusinessLayer.SalesOppsBL SalesOpps = new BusinessLayer.SalesOppsBL();
                    PartnerDS.OnlineAppSalesOppsDataTable dtGwySalesOpp = SalesOpps.GetSalesOpps(AppId);
                    if (dtGwySalesOpp.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtGwySalesOpp.Rows.Count; i++)
                        {
                            string GwySalesOppID = "";
                            DataRow drGwySalesOpp = dtGwySalesOpp.Rows[i];
                            GwySalesOppID = drGwySalesOpp["ID"].ToString().Trim();
                            DLPartner.SalesOppDL GwySalesOppsDL = new DLPartner.SalesOppDL();
                            //OnlineAppClassLibrary.Gateway GatewayInfo = new OnlineAppClassLibrary.Gateway(AppId);
                            //retVal = GatewayInfo.UpdateGatewayInfo(lstGatewayNames.SelectedItem.Text, txtGWMonthlyFee.Text.Trim(), txtGWSetupFee.Text.Trim(), txtGWTransFee.Text.Trim());
                            if ((Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 5) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 20) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 36) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 60) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 65) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 74) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 96) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 150) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 253) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 282))
                            {
                                int retvalSaleACT = GwySalesOppsDL.SalesOppAddedToACT(GwySalesOppID);
                                if (retvalSaleACT == 0)
                                {
                                    BusinessLayer.SalesOppsBL DeleteOpp = new BusinessLayer.SalesOppsBL();
                                    bool retDelete = DeleteOpp.DeleteSalesOpp(GwySalesOppID);
                                }
                            }

                        }
                    }
                    //}
                }

                //Update Payroll and Payroll sales opp

                ProcessingInfo AddlProcessing = new ProcessingInfo(AppId);
                DataSet dsAddlProcessing = AddlProcessing.GetAddlServices();

                if (chkPayroll.Checked)
                {
                    if (dsAddlProcessing.Tables[0].Rows.Count > 0)
                    {
                        DataRow drPayroll = dsAddlProcessing.Tables[0].Rows[0];
                        if (lstPayrollType.SelectedItem.Text != drPayroll["PayrollType"].ToString().Trim())
                        {
                            BusinessLayer.SalesOppsBL SalesOpps = new BusinessLayer.SalesOppsBL();
                            PartnerDS.OnlineAppSalesOppsDataTable dtPayrollSalesOpp = SalesOpps.GetSalesOpps(AppId);
                            if (dtPayrollSalesOpp.Rows.Count > 0)
                            {
                                string PayrollSalesOppID = "";
                                for (int i = 0; i < dtPayrollSalesOpp.Rows.Count; i++)
                                {
                                    DataRow drPayrollSalesOpp = dtPayrollSalesOpp.Rows[i];

                                    //if ((drGwySalesOpp["Product"].ToString().Trim()) == (drGateway["Gateway"].ToString().Trim()))
                                    PayrollSalesOppID = drPayrollSalesOpp["ID"].ToString().Trim();
                                    DLPartner.SalesOppDL PayrollSalesOppsDL = new DLPartner.SalesOppDL();
                                    NewAppInfo PayrollInfo = new OnlineAppClassLibrary.NewAppInfo(AppId);
                                    retVal = PayrollInfo.UpdatePayrollType(lstPayrollType.SelectedItem.Text);
                                    OnlineAppClassLibrary.SalesOppsBL SalesOppPayroll = new OnlineAppClassLibrary.SalesOppsBL(AppId);
                                    SalesOppPayroll.CreatePayrollSalesOpps(chkPayroll.Checked, lstPayrollType.SelectedItem.Text);
                                    if ((Convert.ToInt16(drPayrollSalesOpp["ProductCode"]) == 233) || (Convert.ToInt16(drPayrollSalesOpp["ProductCode"]) == 234))
                                    {
                                        int retvalSaleACT = PayrollSalesOppsDL.SalesOppAddedToACT(PayrollSalesOppID);
                                        if (retvalSaleACT == 0)
                                        {
                                            BusinessLayer.SalesOppsBL DeleteOpp = new BusinessLayer.SalesOppsBL();
                                            bool retDelete = DeleteOpp.DeleteSalesOpp(PayrollSalesOppID);
                                        }
                                    }
                                }

                            }
                            else
                            {
                                NewAppInfo PayrollInfo = new OnlineAppClassLibrary.NewAppInfo(AppId);
                                retVal = PayrollInfo.UpdatePayrollType(lstPayrollType.SelectedItem.Text);
                                OnlineAppClassLibrary.SalesOppsBL SalesOppPayroll = new OnlineAppClassLibrary.SalesOppsBL(AppId);
                                SalesOppPayroll.CreatePayrollSalesOpps(chkPayroll.Checked, lstPayrollType.SelectedItem.Text);
                            }

                        }
                    }
                    else
                    {
                        BusinessLayer.SalesOppsBL SalesOpps = new BusinessLayer.SalesOppsBL();
                        PartnerDS.OnlineAppSalesOppsDataTable dtPayrollSalesOpp = SalesOpps.GetSalesOpps(AppId);
                        if (dtPayrollSalesOpp.Rows.Count > 0)
                        {
                            //if ((drGwySalesOpp["Product"].ToString().Trim()) != (lstGatewayNames.SelectedItem.Text))
                            string PayrollSalesOppID = "";
                            for (int i = 0; i < dtPayrollSalesOpp.Rows.Count; i++)
                            {
                                DataRow drPayrollSalesOpp = dtPayrollSalesOpp.Rows[i];

                                PayrollSalesOppID = drPayrollSalesOpp["ID"].ToString().Trim();
                                DLPartner.SalesOppDL PayrollSalesOppsDL = new DLPartner.SalesOppDL();
                                NewAppInfo PayrollInfo = new OnlineAppClassLibrary.NewAppInfo(AppId);
                                retVal = PayrollInfo.UpdatePayrollType(lstPayrollType.SelectedItem.Text);
                                OnlineAppClassLibrary.SalesOppsBL SalesOppPayroll = new OnlineAppClassLibrary.SalesOppsBL(AppId);
                                SalesOppPayroll.CreatePayrollSalesOpps(chkPayroll.Checked, lstPayrollType.SelectedItem.Text);
                                if ((Convert.ToInt16(drPayrollSalesOpp["ProductCode"]) == 233) || (Convert.ToInt16(drPayrollSalesOpp["ProductCode"]) == 234))
                                {
                                    int retvalSaleACT = PayrollSalesOppsDL.SalesOppAddedToACT(PayrollSalesOppID);
                                    if (retvalSaleACT == 0)
                                    {
                                        BusinessLayer.SalesOppsBL DeleteOpp = new BusinessLayer.SalesOppsBL();
                                        bool retDelete = DeleteOpp.DeleteSalesOpp(PayrollSalesOppID);
                                    }
                                }
                            }
                        }
                        else
                        {
                            NewAppInfo PayrollInfo = new OnlineAppClassLibrary.NewAppInfo(AppId);
                            retVal = PayrollInfo.UpdatePayrollType(lstPayrollType.SelectedItem.Text);
                            OnlineAppClassLibrary.SalesOppsBL SalesOppPayroll = new OnlineAppClassLibrary.SalesOppsBL(AppId);
                            SalesOppPayroll.CreatePayrollSalesOpps(chkPayroll.Checked, lstPayrollType.SelectedItem.Text);
                        }
                    }
                }
                else
                {
                    //if (dsGateway.Tables[0].Rows.Count > 0)
                    //{
                    //DataRow drGateway = dsGateway.Tables[0].Rows[0];
                    BusinessLayer.SalesOppsBL SalesOpps = new BusinessLayer.SalesOppsBL();
                    PartnerDS.OnlineAppSalesOppsDataTable dtPayrollSalesOpp = SalesOpps.GetSalesOpps(AppId);
                    if (dtPayrollSalesOpp.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtPayrollSalesOpp.Rows.Count; i++)
                        {
                            string PayrollSalesOppID = "";
                            DataRow drPayrollSalesOpp = dtPayrollSalesOpp.Rows[i];
                            PayrollSalesOppID = drPayrollSalesOpp["ID"].ToString().Trim();
                            DLPartner.SalesOppDL PayrollSalesOppsDL = new DLPartner.SalesOppDL();
                            //OnlineAppClassLibrary.Gateway GatewayInfo = new OnlineAppClassLibrary.Gateway(AppId);
                            //retVal = GatewayInfo.UpdateGatewayInfo(lstGatewayNames.SelectedItem.Text, txtGWMonthlyFee.Text.Trim(), txtGWSetupFee.Text.Trim(), txtGWTransFee.Text.Trim());
                            if ((Convert.ToInt16(drPayrollSalesOpp["ProductCode"]) == 233) || (Convert.ToInt16(drPayrollSalesOpp["ProductCode"]) == 234))
                            {
                                int retvalSaleACT = PayrollSalesOppsDL.SalesOppAddedToACT(PayrollSalesOppID);
                                if (retvalSaleACT == 0)
                                {
                                    BusinessLayer.SalesOppsBL DeleteOpp = new BusinessLayer.SalesOppsBL();
                                    bool retDelete = DeleteOpp.DeleteSalesOpp(PayrollSalesOppID);
                                }
                            }

                        }
                    }
                    //}
                }

                if (chkCheckGuarantee.Checked)
                {
                    if (dsAddlProcessing.Tables[0].Rows.Count > 0)
                    {
                        DataRow drCheckService = dsAddlProcessing.Tables[0].Rows[0];
                        if (lstCheckService.SelectedItem.Text != drCheckService["CheckService"].ToString().Trim())
                        {
                            BusinessLayer.SalesOppsBL SalesOpps = new BusinessLayer.SalesOppsBL();
                            PartnerDS.OnlineAppSalesOppsDataTable dtCheckServiceSalesOpp = SalesOpps.GetSalesOpps(AppId);
                            if (dtCheckServiceSalesOpp.Rows.Count > 0)
                            {
                                string CheckServiceSalesOppID = "";
                                for (int i = 0; i < dtCheckServiceSalesOpp.Rows.Count; i++)
                                {
                                    DataRow drCheckServiceSalesOpp = dtCheckServiceSalesOpp.Rows[i];

                                    //if ((drGwySalesOpp["Product"].ToString().Trim()) == (drGateway["Gateway"].ToString().Trim()))
                                    CheckServiceSalesOppID = drCheckServiceSalesOpp["ID"].ToString().Trim();
                                    DLPartner.SalesOppDL CheckServiceSalesOppsDL = new DLPartner.SalesOppDL();
                                    NewAppInfo CheckServiceInfo = new OnlineAppClassLibrary.NewAppInfo(AppId);
                                    //retVal = CheckServiceInfo.UpdateCheckService(lstCheckService.SelectedItem.Text);
                                    retVal = CheckServiceInfo.UpdateCheckService(lstCheckService.SelectedItem.Text, Convert.ToDecimal(txtCGDiscRate.ToString().Trim()), Convert.ToDecimal(txtCGMonFee.ToString().Trim()), Convert.ToDecimal(txtCGTransFee.ToString().Trim()), Convert.ToDecimal(txtCGMonMin.ToString().Trim()));
                                    OnlineAppClassLibrary.SalesOppsBL SalesOppCheckService = new OnlineAppClassLibrary.SalesOppsBL(AppId);
                                    SalesOppCheckService.CreateCheckServicesSalesOpps(chkCheckGuarantee.Checked, lstCheckService.SelectedItem.Text);
                                    if ((Convert.ToInt16(drCheckServiceSalesOpp["ProductCode"]) == 11) || (Convert.ToInt16(drCheckServiceSalesOpp["ProductCode"]) == 134) || (Convert.ToInt16(drCheckServiceSalesOpp["ProductCode"]) == 179) || (Convert.ToInt16(drCheckServiceSalesOpp["ProductCode"]) == 178) || (Convert.ToInt16(drCheckServiceSalesOpp["ProductCode"]) == 42))
                                    {
                                        int retvalSaleACT = CheckServiceSalesOppsDL.SalesOppAddedToACT(CheckServiceSalesOppID);
                                        if (retvalSaleACT == 0)
                                        {
                                            BusinessLayer.SalesOppsBL DeleteOpp = new BusinessLayer.SalesOppsBL();
                                            bool retDelete = DeleteOpp.DeleteSalesOpp(CheckServiceSalesOppID);
                                        }
                                    }
                                }

                            }
                            else
                            {
                                NewAppInfo CheckServiceInfo = new OnlineAppClassLibrary.NewAppInfo(AppId);
                                //retVal = CheckServiceInfo.UpdateCheckService(lstCheckService.SelectedItem.Text);
                                retVal = CheckServiceInfo.UpdateCheckService(lstCheckService.SelectedItem.Text, Convert.ToDecimal(txtCGDiscRate.ToString().Trim()), Convert.ToDecimal(txtCGMonFee.ToString().Trim()), Convert.ToDecimal(txtCGTransFee.ToString().Trim()), Convert.ToDecimal(txtCGMonMin.ToString().Trim()));
                                OnlineAppClassLibrary.SalesOppsBL SalesOppCheckService = new OnlineAppClassLibrary.SalesOppsBL(AppId);
                                SalesOppCheckService.CreateCheckServicesSalesOpps(chkCheckGuarantee.Checked, lstCheckService.SelectedItem.Text);
                            }

                        }
                    }
                    else
                    {
                        BusinessLayer.SalesOppsBL SalesOpps = new BusinessLayer.SalesOppsBL();
                        PartnerDS.OnlineAppSalesOppsDataTable dtCheckServiceSalesOpp = SalesOpps.GetSalesOpps(AppId);
                        if (dtCheckServiceSalesOpp.Rows.Count > 0)
                        {
                            //if ((drGwySalesOpp["Product"].ToString().Trim()) != (lstGatewayNames.SelectedItem.Text))
                            string CheckServiceSalesOppID = "";
                            for (int i = 0; i < dtCheckServiceSalesOpp.Rows.Count; i++)
                            {
                                DataRow drCheckServiceSalesOpp = dtCheckServiceSalesOpp.Rows[i];

                                CheckServiceSalesOppID = drCheckServiceSalesOpp["ID"].ToString().Trim();
                                DLPartner.SalesOppDL CheckServiceSalesOppsDL = new DLPartner.SalesOppDL();
                                NewAppInfo CheckServiceInfo = new OnlineAppClassLibrary.NewAppInfo(AppId);
                                //retVal = CheckServiceInfo.UpdateCheckService(lstCheckService.SelectedItem.Text);
                                retVal = CheckServiceInfo.UpdateCheckService(lstCheckService.SelectedItem.Text, Convert.ToDecimal(txtCGDiscRate.ToString().Trim()), Convert.ToDecimal(txtCGMonFee.ToString().Trim()), Convert.ToDecimal(txtCGTransFee.ToString().Trim()), Convert.ToDecimal(txtCGMonMin.ToString().Trim()));
                                OnlineAppClassLibrary.SalesOppsBL SalesOppCheckService = new OnlineAppClassLibrary.SalesOppsBL(AppId);
                                SalesOppCheckService.CreateCheckServicesSalesOpps(chkCheckGuarantee.Checked, lstCheckService.SelectedItem.Text);
                                if ((Convert.ToInt16(drCheckServiceSalesOpp["ProductCode"]) == 11) || (Convert.ToInt16(drCheckServiceSalesOpp["ProductCode"]) == 134) || (Convert.ToInt16(drCheckServiceSalesOpp["ProductCode"]) == 179) || (Convert.ToInt16(drCheckServiceSalesOpp["ProductCode"]) == 178) || (Convert.ToInt16(drCheckServiceSalesOpp["ProductCode"]) == 42))
                                {
                                    int retvalSaleACT = CheckServiceSalesOppsDL.SalesOppAddedToACT(CheckServiceSalesOppID);
                                    if (retvalSaleACT == 0)
                                    {
                                        BusinessLayer.SalesOppsBL DeleteOpp = new BusinessLayer.SalesOppsBL();
                                        bool retDelete = DeleteOpp.DeleteSalesOpp(CheckServiceSalesOppID);
                                    }
                                }
                            }
                        }
                        else
                        {
                            NewAppInfo CheckServiceInfo = new OnlineAppClassLibrary.NewAppInfo(AppId);
                            //retVal = CheckServiceInfo.UpdateCheckService(lstCheckService.SelectedItem.Text);
                            retVal = CheckServiceInfo.UpdateCheckService(lstCheckService.SelectedItem.Text, Convert.ToDecimal(txtCGDiscRate.ToString().Trim()), Convert.ToDecimal(txtCGMonFee.ToString().Trim()), Convert.ToDecimal(txtCGTransFee.ToString().Trim()), Convert.ToDecimal(txtCGMonMin.ToString().Trim()));
                            OnlineAppClassLibrary.SalesOppsBL SalesOppCheckService = new OnlineAppClassLibrary.SalesOppsBL(AppId);
                            SalesOppCheckService.CreateCheckServicesSalesOpps(chkCheckGuarantee.Checked, lstCheckService.SelectedItem.Text);
                        }
                    }
                }
                else
                {
                    //if (dsGateway.Tables[0].Rows.Count > 0)
                    //{
                    //DataRow drGateway = dsGateway.Tables[0].Rows[0];
                    BusinessLayer.SalesOppsBL SalesOpps = new BusinessLayer.SalesOppsBL();
                    PartnerDS.OnlineAppSalesOppsDataTable dtCheckServiceSalesOpp = SalesOpps.GetSalesOpps(AppId);
                    if (dtCheckServiceSalesOpp.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtCheckServiceSalesOpp.Rows.Count; i++)
                        {
                            string CheckServiceSalesOppID = "";
                            DataRow drCheckServiceSalesOpp = dtCheckServiceSalesOpp.Rows[i];
                            CheckServiceSalesOppID = drCheckServiceSalesOpp["ID"].ToString().Trim();
                            DLPartner.SalesOppDL CheckServiceSalesOppsDL = new DLPartner.SalesOppDL();
                            //OnlineAppClassLibrary.Gateway GatewayInfo = new OnlineAppClassLibrary.Gateway(AppId);
                            //retVal = GatewayInfo.UpdateGatewayInfo(lstGatewayNames.SelectedItem.Text, txtGWMonthlyFee.Text.Trim(), txtGWSetupFee.Text.Trim(), txtGWTransFee.Text.Trim());
                            if ((Convert.ToInt16(drCheckServiceSalesOpp["ProductCode"]) == 11) || (Convert.ToInt16(drCheckServiceSalesOpp["ProductCode"]) == 134) || (Convert.ToInt16(drCheckServiceSalesOpp["ProductCode"]) == 179) || (Convert.ToInt16(drCheckServiceSalesOpp["ProductCode"]) == 178) || (Convert.ToInt16(drCheckServiceSalesOpp["ProductCode"]) == 42))
                            {
                                int retvalSaleACT = CheckServiceSalesOppsDL.SalesOppAddedToACT(CheckServiceSalesOppID);
                                if (retvalSaleACT == 0)
                                {
                                    BusinessLayer.SalesOppsBL DeleteOpp = new BusinessLayer.SalesOppsBL();
                                    bool retDelete = DeleteOpp.DeleteSalesOpp(CheckServiceSalesOppID);
                                }
                            }

                        }
                    }
                    //}
                }

                if (chkMerchantFunding.Checked)
                {
                    if (dsAddlProcessing.Tables[0].Rows.Count > 0)
                    {
                        DataRow drMCA = dsAddlProcessing.Tables[0].Rows[0];
                        if (lstMCAType.SelectedItem.Text != drMCA["CheckService"].ToString().Trim())
                        {
                            BusinessLayer.SalesOppsBL SalesOpps = new BusinessLayer.SalesOppsBL();
                            PartnerDS.OnlineAppSalesOppsDataTable dtMCASalesOpp = SalesOpps.GetSalesOpps(AppId);
                            if (dtMCASalesOpp.Rows.Count > 0)
                            {
                                string MCASalesOppID = "";
                                for (int i = 0; i < dtMCASalesOpp.Rows.Count; i++)
                                {
                                    DataRow drMCASalesOpp = dtMCASalesOpp.Rows[i];

                                    //if ((drGwySalesOpp["Product"].ToString().Trim()) == (drGateway["Gateway"].ToString().Trim()))
                                    MCASalesOppID = drMCASalesOpp["ID"].ToString().Trim();
                                    DLPartner.SalesOppDL MCASalesOppsDL = new DLPartner.SalesOppDL();
                                    NewAppInfo MCAInfo = new OnlineAppClassLibrary.NewAppInfo(AppId);
                                    retVal = MCAInfo.UpdateMCAType(lstMCAType.SelectedItem.Text);
                                    OnlineAppClassLibrary.SalesOppsBL SalesOppMCA = new OnlineAppClassLibrary.SalesOppsBL(AppId);
                                    SalesOppMCA.CreateMCASalesOpps(chkMerchantFunding.Checked, lstMCAType.SelectedItem.Text);
                                    if ((Convert.ToInt16(drMCASalesOpp["ProductCode"]) == 113) || (Convert.ToInt16(drMCASalesOpp["ProductCode"]) == 263) || (Convert.ToInt16(drMCASalesOpp["ProductCode"]) == 315))
                                    {
                                        int retvalSaleACT = MCASalesOppsDL.SalesOppAddedToACT(MCASalesOppID);
                                        if (retvalSaleACT == 0)
                                        {
                                            BusinessLayer.SalesOppsBL DeleteOpp = new BusinessLayer.SalesOppsBL();
                                            bool retDelete = DeleteOpp.DeleteSalesOpp(MCASalesOppID);
                                        }
                                    }
                                }

                            }
                            else
                            {
                                NewAppInfo MCAInfo = new OnlineAppClassLibrary.NewAppInfo(AppId);
                                retVal = MCAInfo.UpdateMCAType(lstMCAType.SelectedItem.Text);
                                OnlineAppClassLibrary.SalesOppsBL SalesOppMCA = new OnlineAppClassLibrary.SalesOppsBL(AppId);
                                SalesOppMCA.CreateMCASalesOpps(chkMerchantFunding.Checked, lstMCAType.SelectedItem.Text);
                            }

                        }
                    }
                    else
                    {
                        BusinessLayer.SalesOppsBL SalesOpps = new BusinessLayer.SalesOppsBL();
                        PartnerDS.OnlineAppSalesOppsDataTable dtMCASalesOpp = SalesOpps.GetSalesOpps(AppId);
                        if (dtMCASalesOpp.Rows.Count > 0)
                        {
                            //if ((drGwySalesOpp["Product"].ToString().Trim()) != (lstGatewayNames.SelectedItem.Text))
                            string MCASalesOppID = "";
                            for (int i = 0; i < dtMCASalesOpp.Rows.Count; i++)
                            {
                                DataRow drMCASalesOpp = dtMCASalesOpp.Rows[i];

                                MCASalesOppID = drMCASalesOpp["ID"].ToString().Trim();
                                DLPartner.SalesOppDL MCASalesOppsDL = new DLPartner.SalesOppDL();
                                NewAppInfo MCAInfo = new OnlineAppClassLibrary.NewAppInfo(AppId);
                                retVal = MCAInfo.UpdateMCAType(lstMCAType.SelectedItem.Text);
                                OnlineAppClassLibrary.SalesOppsBL SalesOppMCA = new OnlineAppClassLibrary.SalesOppsBL(AppId);
                                SalesOppMCA.CreateMCASalesOpps(chkMerchantFunding.Checked, lstMCAType.SelectedItem.Text);
                                if ((Convert.ToInt16(drMCASalesOpp["ProductCode"]) == 113) || (Convert.ToInt16(drMCASalesOpp["ProductCode"]) == 263) || (Convert.ToInt16(drMCASalesOpp["ProductCode"]) == 315))
                                {
                                    int retvalSaleACT = MCASalesOppsDL.SalesOppAddedToACT(MCASalesOppID);
                                    if (retvalSaleACT == 0)
                                    {
                                        BusinessLayer.SalesOppsBL DeleteOpp = new BusinessLayer.SalesOppsBL();
                                        bool retDelete = DeleteOpp.DeleteSalesOpp(MCASalesOppID);
                                    }
                                }
                            }
                        }
                        else
                        {
                            NewAppInfo MCAInfo = new OnlineAppClassLibrary.NewAppInfo(AppId);
                            retVal = MCAInfo.UpdateMCAType(lstMCAType.SelectedItem.Text);
                            OnlineAppClassLibrary.SalesOppsBL SalesOppMCA = new OnlineAppClassLibrary.SalesOppsBL(AppId);
                            SalesOppMCA.CreateMCASalesOpps(chkMerchantFunding.Checked, lstMCAType.SelectedItem.Text);
                        }
                    }
                }
                else
                {
                    //if (dsGateway.Tables[0].Rows.Count > 0)
                    //{
                    //DataRow drGateway = dsGateway.Tables[0].Rows[0];
                    BusinessLayer.SalesOppsBL SalesOpps = new BusinessLayer.SalesOppsBL();
                    PartnerDS.OnlineAppSalesOppsDataTable dtMCASalesOpp = SalesOpps.GetSalesOpps(AppId);
                    if (dtMCASalesOpp.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtMCASalesOpp.Rows.Count; i++)
                        {
                            string MCASalesOppID = "";
                            DataRow drMCASalesOpp = dtMCASalesOpp.Rows[i];
                            MCASalesOppID = drMCASalesOpp["ID"].ToString().Trim();
                            DLPartner.SalesOppDL MCASalesOppsDL = new DLPartner.SalesOppDL();
                            //OnlineAppClassLibrary.Gateway GatewayInfo = new OnlineAppClassLibrary.Gateway(AppId);
                            //retVal = GatewayInfo.UpdateGatewayInfo(lstGatewayNames.SelectedItem.Text, txtGWMonthlyFee.Text.Trim(), txtGWSetupFee.Text.Trim(), txtGWTransFee.Text.Trim());
                            if ((Convert.ToInt16(drMCASalesOpp["ProductCode"]) == 113) || (Convert.ToInt16(drMCASalesOpp["ProductCode"]) == 263) || (Convert.ToInt16(drMCASalesOpp["ProductCode"]) == 315))
                            {
                                int retvalSaleACT = MCASalesOppsDL.SalesOppAddedToACT(MCASalesOppID);
                                if (retvalSaleACT == 0)
                                {
                                    BusinessLayer.SalesOppsBL DeleteOpp = new BusinessLayer.SalesOppsBL();
                                    bool retDelete = DeleteOpp.DeleteSalesOpp(MCASalesOppID);
                                }
                            }

                        }
                    }
                    //}
                }

                //Update rates for this AppId
                OnlineAppProcessingBL Processing = new OnlineAppProcessingBL(AppId);
                /*retVal = Processing.ApplyRates(lstProcessorNames.SelectedItem.Text,
                    strCardPresent, txtCustomerService.Text.Trim(), txtInternetStmt.Text.Trim(), txtTransFee.Text.Trim(),
                    txtDRQP.Text.Trim(), txtDRQNP.Text.Trim(), txtDRMQ.Text.Trim(),
                    txtDRNQ.Text.Trim(), txtDRQD.Text.Trim(), txtQualNPDebit.Text.Trim(), txtMidQualDebit.Text.Trim(), txtNonQualDebit.Text.Trim(), txtAmexQual.Text.Trim(), txtAmexMidQual.Text.Trim(), txtAmexNonQual.Text.Trim(), txtChargebackFee.Text.Trim(),
                    txtRetrievalFee.Text.Trim(), txtVoiceAuth.Text.Trim(),
                    BatchHeader, AVSFee, txtMonMin.Text.Trim(),
                    txtNBCTransFee.Text.Trim(), lstAnnualFee.SelectedItem.Text.Trim(), txtWirelessAccess.Text.Trim(),
                    txtWirelessTransFee.Text.Trim(), txtSetupFee.Text.Trim(), txtApplicationFee.Text.Trim(), txtRolling.Text.Trim(),
                    txtGWMonthlyFee.Text.Trim(), txtGWTransFee.Text.Trim(), txtGWSetupFee.Text.Trim(),
                    lstGatewayNames.SelectedItem.Text, txtDebitMonFee.Text.Trim(), pinDebitTransFee,
                    lstCheckService.SelectedItem.Text.Trim() ,txtCGDiscRate.Text.Trim(), txtCGMonFee.Text.Trim(), txtCGMonMin.Text.Trim(),
                    txtCGTransFee.Text.Trim(), lstGCType.SelectedItem.Text.Trim(), txtGCMonFee.Text.Trim(), txtGCTransFee.Text.Trim(),
                    txtEBTMonFee.Text.Trim(), txtEBTTransFee.Text.Trim(), lstLeaseCompany.SelectedItem.Text.Trim(), 
                    txtLeasePayment.Text.Trim(), lstLeaseTerm.SelectedItem.Text.Trim(), lstPayrollType.SelectedItem.Text.Trim(),
                    lstMCAType.SelectedItem.Text.Trim(), lstDiscountPaid.SelectedItem.Text.Trim(), lstContractTerm.SelectedItem.Text.Trim(), txtComplianceFee.Text.Trim(), txtPinDebitDiscount.Text.Trim());*/

                if (lstProcessorNames.SelectedItem.Text.ToLower().Contains("Sage"))
                {
                    retVal = Processing.ApplyRates(lstProcessorNames.SelectedItem.Text,
                        strCardPresent, txtCustomerService.Text.Trim(), txtInternetStmt.Text.Trim(), txtTransFee.Text.Trim(),
                        txtDRQP.Text.Trim(), txtDRQNP.Text.Trim(), txtDRMQ.Text.Trim(),
                        txtDRNQ.Text.Trim(), txtDRQD.Text.Trim(), txtQualNPDebit.Text.Trim(), txtMidQualDebit.Text.Trim(), txtNonQualDebit.Text.Trim(), txtAmexQual.Text.Trim(), txtAmexMidQual.Text.Trim(), txtAmexNonQual.Text.Trim(), txtChargebackFee.Text.Trim(),
                        txtRetrievalFee.Text.Trim(), txtVoiceAuth.Text.Trim(),
                        BatchHeader, lstAVS.SelectedItem.Text.Trim(), txtMonMin.Text.Trim(),
                        txtNBCTransFee.Text.Trim(), lstAnnualFee.SelectedItem.Text.Trim(), txtWirelessAccess.Text.Trim(),
                        txtWirelessTransFee.Text.Trim(), txtSetupFee.Text.Trim(), txtApplicationFee.Text.Trim(), txtRolling.Text.Trim(),
                        txtGWMonthlyFee.Text.Trim(), txtGWTransFee.Text.Trim(), txtGWSetupFee.Text.Trim(),
                        lstGatewayNames.SelectedItem.Text, txtDebitMonFee.Text.Trim(), pinDebitTransFee,
                        lstCheckService.SelectedItem.Text.Trim(), txtCGDiscRate.Text.Trim(), txtCGMonFee.Text.Trim(), txtCGMonMin.Text.Trim(),
                        txtCGTransFee.Text.Trim(), lstGCType.SelectedItem.Text.Trim(), txtGCMonFee.Text.Trim(), txtGCTransFee.Text.Trim(),
                        txtEBTMonFee.Text.Trim(), txtEBTTransFee.Text.Trim(), lstLeaseCompany.SelectedItem.Text.Trim(),
                        txtLeasePayment.Text.Trim(), lstLeaseTerm.SelectedItem.Text.Trim(), lstPayrollType.SelectedItem.Text.Trim(),
                        lstMCAType.SelectedItem.Text.Trim(), lstDiscountPaid.SelectedItem.Text.Trim(), lstContractTerm.SelectedItem.Text.Trim(), txtComplianceFee.Text.Trim(), txtPinDebitDiscount.Text.Trim());
                }
                else
                {
                    retVal = Processing.ApplyRates(lstProcessorNames.SelectedItem.Text,
                        strCardPresent, txtCustomerService.Text.Trim(), txtInternetStmt.Text.Trim(), txtTransFee.Text.Trim(),
                        txtDRQP.Text.Trim(), txtDRQNP.Text.Trim(), txtDRMQ.Text.Trim(),
                        txtDRNQ.Text.Trim(), txtDRQD.Text.Trim(), txtQualNPDebit.Text.Trim(), txtMidQualDebit.Text.Trim(), txtNonQualDebit.Text.Trim(), txtAmexQual.Text.Trim(), txtAmexMidQual.Text.Trim(), txtAmexNonQual.Text.Trim(), txtChargebackFee.Text.Trim(),
                        txtRetrievalFee.Text.Trim(), txtVoiceAuth.Text.Trim(),
                        BatchHeader, txtAVS.Text.Trim(), txtMonMin.Text.Trim(),
                        txtNBCTransFee.Text.Trim(), lstAnnualFee.SelectedItem.Text.Trim(), txtWirelessAccess.Text.Trim(),
                        txtWirelessTransFee.Text.Trim(), txtSetupFee.Text.Trim(), txtApplicationFee.Text.Trim(), txtRolling.Text.Trim(),
                        txtGWMonthlyFee.Text.Trim(), txtGWTransFee.Text.Trim(), txtGWSetupFee.Text.Trim(),
                        lstGatewayNames.SelectedItem.Text, txtDebitMonFee.Text.Trim(), pinDebitTransFee,
                        lstCheckService.SelectedItem.Text.Trim(), txtCGDiscRate.Text.Trim(), txtCGMonFee.Text.Trim(), txtCGMonMin.Text.Trim(),
                        txtCGTransFee.Text.Trim(), lstGCType.SelectedItem.Text.Trim(), txtGCMonFee.Text.Trim(), txtGCTransFee.Text.Trim(),
                        txtEBTMonFee.Text.Trim(), txtEBTTransFee.Text.Trim(), lstLeaseCompany.SelectedItem.Text.Trim(),
                        txtLeasePayment.Text.Trim(), lstLeaseTerm.SelectedItem.Text.Trim(), lstPayrollType.SelectedItem.Text.Trim(),
                        lstMCAType.SelectedItem.Text.Trim(), lstDiscountPaid.SelectedItem.Text.Trim(), lstContractTerm.SelectedItem.Text.Trim(), txtComplianceFee.Text.Trim(), txtPinDebitDiscount.Text.Trim());
                }

                NewAppTable newAppTable = new NewAppTable();
                newAppTable.setRatesUpdatedBit(AppId, true);

                //Update Interchange and Assessment bits in the Processing Table
                UpdateOtherProcessing(AppId,Convert.ToString(lstRateNames.SelectedItem.Text.Trim()), chkBillAssessment.Checked, txtRolling.Text.Trim());

                //Insert Additional Services Sales Opps
                OnlineAppClassLibrary.SalesOppsBL Sales = new OnlineAppClassLibrary.SalesOppsBL(AppId);
                if (chkMA.Checked)
                {
                    bool retVal2 = Sales.CreateSalesOpps();
                }

                /*if (chkGateway.Checked)
                {
                    Sales.CreateSalesOppsGW();
                }*/
                /*Sales.CreateAddlServSalesOpps(chkOnlineDebit.Checked, chkGiftCard.Checked,
                    chkEBT.Checked, chkPayroll.Checked);*/

                // Insert/Delete EBT/Online Debit Sales Opps
                if (strCardPresent.ToString().Trim() == "CP")
                //if (dt[0].CardPresent.ToString().Trim() == "CP")
                {

                    //Insert EBT Sales Opps

                    Sales.CreateEBTSalesOpps(chkEBT.Checked);

                    //Insert Online Debit Sales Opps

                    Sales.CreateOnlineDebitSalesOpps(chkOnlineDebit.Checked);
                }
                if ((strCardPresent.ToString().Trim() == "CNP") && (chkEBT.Checked))
                { DisplayMessage("EBT can only be selected for Card Present accounts."); }
                if ((strCardPresent.ToString().Trim() == "CNP") && (chkOnlineDebit.Checked))
                { DisplayMessage("Online Debit can only be selected for Card Present accounts."); }

                if (!chkEBT.Checked)
                {
                    BusinessLayer.SalesOppsBL SalesOpps = new BusinessLayer.SalesOppsBL();
                    PartnerDS.OnlineAppSalesOppsDataTable dtEBTSalesOpp = SalesOpps.GetSalesOpps(AppId);
                    if (dtEBTSalesOpp.Rows.Count > 0)
                    {

                        for (int i = 0; i < dtEBTSalesOpp.Rows.Count; i++)
                        {
                            string EBTSalesOppID = "";
                            DataRow drEBTSalesOpp = dtEBTSalesOpp.Rows[i];
                            EBTSalesOppID = drEBTSalesOpp["ID"].ToString().Trim();
                            DLPartner.SalesOppDL EBTSalesOppsDL = new DLPartner.SalesOppDL();
                            if (Convert.ToInt16(drEBTSalesOpp["ProductCode"]) == 112)
                            {
                                int retvalSaleACT = EBTSalesOppsDL.SalesOppAddedToACT(EBTSalesOppID);
                                if (retvalSaleACT == 0)
                                {
                                    BusinessLayer.SalesOppsBL DeleteOpp = new BusinessLayer.SalesOppsBL();
                                    bool retDelete = DeleteOpp.DeleteSalesOpp(EBTSalesOppID);
                                }
                            }
                        }
                    }
                }

                if (!chkOnlineDebit.Checked)
                {
                    BusinessLayer.SalesOppsBL SalesOpps = new BusinessLayer.SalesOppsBL();
                    PartnerDS.OnlineAppSalesOppsDataTable dtOnlineDebitSalesOpp = SalesOpps.GetSalesOpps(AppId);
                    if (dtOnlineDebitSalesOpp.Rows.Count > 0)
                    {

                        for (int i = 0; i < dtOnlineDebitSalesOpp.Rows.Count; i++)
                        {
                            string OnlineDebitSalesOppID = "";
                            DataRow drOnlineDebitSalesOpp = dtOnlineDebitSalesOpp.Rows[i];
                            OnlineDebitSalesOppID = drOnlineDebitSalesOpp["ID"].ToString().Trim();
                            DLPartner.SalesOppDL OnlineDebitSalesOppsDL = new DLPartner.SalesOppDL();
                            if (Convert.ToInt16(drOnlineDebitSalesOpp["ProductCode"]) == 77)
                            {
                                int retvalSaleACT = OnlineDebitSalesOppsDL.SalesOppAddedToACT(OnlineDebitSalesOppID);
                                if (retvalSaleACT == 0)
                                {
                                    BusinessLayer.SalesOppsBL DeleteOpp = new BusinessLayer.SalesOppsBL();
                                    bool retDelete = DeleteOpp.DeleteSalesOpp(OnlineDebitSalesOppID);
                                }
                            }
                        }
                    }
                }

                //Insert Payroll Sales Opps
                //Sales.CreatePayrollSalesOpps(chkPayroll.Checked, lstPayrollType.SelectedItem.Text);

                //Insert Gift Card Sales Opps
                Sales.CreateGiftCardSalesOpps(chkGiftCard.Checked, lstGCType.SelectedItem.Text);

                //Insert Check Services Sales Opps
                //Sales.CreateCheckServicesSalesOpps(chkCheckGuarantee.Checked, lstCheckService.SelectedItem.Text);

                //Insert MCA Sales Opps
                //Sales.CreateMCASalesOpps(chkMerchantFunding.Checked, lstMCAType.SelectedItem.Text);


                if (!chkGiftCard.Checked)
                {
                    BusinessLayer.SalesOppsBL SalesOpps = new BusinessLayer.SalesOppsBL();
                    PartnerDS.OnlineAppSalesOppsDataTable dtGiftCardSalesOpp = SalesOpps.GetSalesOpps(AppId);
                    if (dtGiftCardSalesOpp.Rows.Count > 0)
                    {

                        for (int i = 0; i < dtGiftCardSalesOpp.Rows.Count; i++)
                        {
                            string GiftCardSalesOppID = "";
                            DataRow drGiftCardSalesOpp = dtGiftCardSalesOpp.Rows[i];
                            GiftCardSalesOppID = drGiftCardSalesOpp["ID"].ToString().Trim();
                            DLPartner.SalesOppDL GiftCardSalesOppsDL = new DLPartner.SalesOppDL();
                            if (Convert.ToInt16(drGiftCardSalesOpp["ProductCode"]) == 174)
                            {
                                int retvalSaleACT = GiftCardSalesOppsDL.SalesOppAddedToACT(GiftCardSalesOppID);
                                if (retvalSaleACT == 0)
                                {
                                    BusinessLayer.SalesOppsBL DeleteOpp = new BusinessLayer.SalesOppsBL();
                                    bool retDelete = DeleteOpp.DeleteSalesOpp(GiftCardSalesOppID);
                                }
                            }
                        }
                    }
                }

                CommonFunctions GeneralInfo = new CommonFunctions(AppId);
                GeneralInfo.UpdateLastModified();



                if (retVal)
                {
                    DisplayMessage("All services updated successfully.");
                    //Add action to Log table
                    PartnerLogBL LogData = new PartnerLogBL();
                    retVal = LogData.InsertLogData(AppId, Convert.ToInt32(Session["AffiliateID"]), "All services updated");

                    Populate();
                }
                else
                    DisplayMessage("Rates cannot be applied.");
            }
            else
                DisplayMessage(errMessage);
        }//end try
        catch (Exception err)
        {
            CreateLog Log = new CreateLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), err.Message);
            DisplayMessage("Error applying rates.");
        }
    }//end submit button click
    #endregion

    public int UpdateOtherProcessing(int AppId, string billingMethod, bool bAssessments, string RollingReserve)
    {
        SqlConnection Conn = new SqlConnection(ConnStringPartner);
        try
        {
            SqlCommand cmd = new SqlCommand("sp_UpdateOtherProcessingBillMethod", Conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@AppId", AppId));
            cmd.Parameters.Add(new SqlParameter("@BillingMethod", billingMethod));
            cmd.Parameters.Add(new SqlParameter("@Assessments", bAssessments));
            cmd.Parameters.Add(new SqlParameter("@RollingReserve", RollingReserve));
            cmd.Connection.Open();
            int iRetVal = cmd.ExecuteNonQuery();
            Conn.Close();
            Conn.Dispose();
            return iRetVal;
        }
        catch (SqlException sqlerr)
        {
            throw sqlerr;
        }
        finally
        {
            Conn.Close();
            Conn.Dispose();
        }
    }//end UpdateInterchange

    #region PROCESSOR SUBMIT BUTTON CLICK
    protected void btnProcessorSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string strCardPresent;
            string pinDebitTransFee;

            if (rdbCP.Checked)
                strCardPresent = "CP";
            else
                strCardPresent = "CNP";
            string errMessage = "";

            string AVSFee = "";

            errMessage = ValidateProcessorRates();

            if (errMessage == "")
            {
                //Update NewApp table Addl Services bits
                OnlineAppBL UpdateAddlServ = new OnlineAppBL(AppId);
                bool retVal = UpdateAddlServ.UpdateODEBT(chkOnlineDebit.Checked, chkEBT.Checked);

                string BatchHeader = txtBatchHeader.Text.Trim();
                if (lstProcessorNames.SelectedItem.Text.ToLower().Contains("ims"))
                    BatchHeader = txtTransFee.Text.Trim();
                if (lstProcessorNames.SelectedItem.Text.ToLower().Contains("intuit"))
                    BatchHeader = txtTransFee.Text.Trim();
                if (lstProcessorNames.SelectedItem.Text.ToLower().Contains("sage"))
                    BatchHeader = txtTransFee.Text.Trim();

                //Update AcctType based on check boxes on this page
                OnlineAppProfile AType = new OnlineAppProfile(AppId);
                int iAType = 0;
                if ((chkMA.Checked) && (chkGateway.Checked))
                    iAType = 4;
                else if (chkMA.Checked)
                    iAType = 1;
                else if (chkGateway.Checked)
                    iAType = 2;
                else
                    iAType = 3;

                AType.UpdateAcctType(iAType);

                //Update Payroll and Payroll sales opp
                ProcessingInfo AddlProcessing = new ProcessingInfo(AppId);
                DataSet dsAddlProcessing = AddlProcessing.GetAddlServices();

                if (lstProcessorNames.SelectedItem.Text.ToLower().Contains("sage"))
                {
                    pinDebitTransFee = lstDebitTransFee.SelectedItem.Text;
                    AVSFee = lstAVS.SelectedItem.Text;
                }
                else
                {
                    pinDebitTransFee = txtDebitTransFee.Text;
                    AVSFee = txtAVS.Text;
                }

                //Update rates for this AppId
                OnlineAppProcessingBL Processing = new OnlineAppProcessingBL(AppId);

                if (lstProcessorNames.SelectedItem.Text.ToLower().Contains("sage"))
                {
                    retVal = Processing.ApplyRates(lstProcessorNames.SelectedItem.Text,
                        strCardPresent, txtCustomerService.Text.Trim(), txtInternetStmt.Text.Trim(), txtTransFee.Text.Trim(),
                        txtDRQP.Text.Trim(), txtDRQNP.Text.Trim(), txtDRMQ.Text.Trim(),
                        txtDRNQ.Text.Trim(), txtDRQD.Text.Trim(), txtQualNPDebit.Text.Trim(), txtMidQualDebit.Text.Trim(), txtNonQualDebit.Text.Trim(), txtAmexQual.Text.Trim(), txtAmexMidQual.Text.Trim(), txtAmexNonQual.Text.Trim(), txtChargebackFee.Text.Trim(),
                        txtRetrievalFee.Text.Trim(), txtVoiceAuth.Text.Trim(),
                        BatchHeader, lstAVS.SelectedItem.Text.Trim(), txtMonMin.Text.Trim(),
                        txtNBCTransFee.Text.Trim(), lstAnnualFee.SelectedItem.Text.Trim(), txtWirelessAccess.Text.Trim(),
                        txtWirelessTransFee.Text.Trim(), txtSetupFee.Text.Trim(), txtApplicationFee.Text.Trim(), txtRolling.Text.Trim(),
                        txtGWMonthlyFee.Text.Trim(), txtGWTransFee.Text.Trim(), txtGWSetupFee.Text.Trim(),
                        lstGatewayNames.SelectedItem.Text, txtDebitMonFee.Text.Trim(), pinDebitTransFee,
                        lstCheckService.SelectedItem.Text.Trim(), txtCGDiscRate.Text.Trim(), txtCGMonFee.Text.Trim(), txtCGMonMin.Text.Trim(),
                        txtCGTransFee.Text.Trim(), lstGCType.SelectedItem.Text.Trim(), txtGCMonFee.Text.Trim(), txtGCTransFee.Text.Trim(),
                        txtEBTMonFee.Text.Trim(), txtEBTTransFee.Text.Trim(), lstLeaseCompany.SelectedItem.Text.Trim(),
                        txtLeasePayment.Text.Trim(), lstLeaseTerm.SelectedItem.Text.Trim(), lstPayrollType.SelectedItem.Text.Trim(),
                        lstMCAType.SelectedItem.Text.Trim(), lstDiscountPaid.SelectedItem.Text.Trim(), lstContractTerm.SelectedItem.Text.Trim(), txtComplianceFee.Text.Trim(), txtPinDebitDiscount.Text.Trim());
                }
                else
                {
                    retVal = Processing.ApplyRates(lstProcessorNames.SelectedItem.Text,
                        strCardPresent, txtCustomerService.Text.Trim(), txtInternetStmt.Text.Trim(), txtTransFee.Text.Trim(),
                        txtDRQP.Text.Trim(), txtDRQNP.Text.Trim(), txtDRMQ.Text.Trim(),
                        txtDRNQ.Text.Trim(), txtDRQD.Text.Trim(), txtQualNPDebit.Text.Trim(), txtMidQualDebit.Text.Trim(), txtNonQualDebit.Text.Trim(), txtAmexQual.Text.Trim(), txtAmexMidQual.Text.Trim(), txtAmexNonQual.Text.Trim(), txtChargebackFee.Text.Trim(),
                        txtRetrievalFee.Text.Trim(), txtVoiceAuth.Text.Trim(),
                        BatchHeader, txtAVS.Text.Trim(), txtMonMin.Text.Trim(),
                        txtNBCTransFee.Text.Trim(), lstAnnualFee.SelectedItem.Text.Trim(), txtWirelessAccess.Text.Trim(),
                        txtWirelessTransFee.Text.Trim(), txtSetupFee.Text.Trim(), txtApplicationFee.Text.Trim(), txtRolling.Text.Trim(),
                        txtGWMonthlyFee.Text.Trim(), txtGWTransFee.Text.Trim(), txtGWSetupFee.Text.Trim(),
                        lstGatewayNames.SelectedItem.Text, txtDebitMonFee.Text.Trim(), pinDebitTransFee,
                        lstCheckService.SelectedItem.Text.Trim(), txtCGDiscRate.Text.Trim(), txtCGMonFee.Text.Trim(), txtCGMonMin.Text.Trim(),
                        txtCGTransFee.Text.Trim(), lstGCType.SelectedItem.Text.Trim(), txtGCMonFee.Text.Trim(), txtGCTransFee.Text.Trim(),
                        txtEBTMonFee.Text.Trim(), txtEBTTransFee.Text.Trim(), lstLeaseCompany.SelectedItem.Text.Trim(),
                        txtLeasePayment.Text.Trim(), lstLeaseTerm.SelectedItem.Text.Trim(), lstPayrollType.SelectedItem.Text.Trim(),
                        lstMCAType.SelectedItem.Text.Trim(), lstDiscountPaid.SelectedItem.Text.Trim(), lstContractTerm.SelectedItem.Text.Trim(), txtComplianceFee.Text.Trim(), txtPinDebitDiscount.Text.Trim());
                }
                NewAppTable newAppTable = new NewAppTable();
                newAppTable.setRatesUpdatedBit(AppId, true);

                //Update Interchange and Assessment bits in the Processing Table
                UpdateOtherProcessing(AppId, Convert.ToString(lstRateNames.SelectedItem.Text.Trim()), chkBillAssessment.Checked, txtRolling.Text.Trim());

                OnlineAppClassLibrary.SalesOppsBL Sales = new OnlineAppClassLibrary.SalesOppsBL(AppId);
                if (chkMA.Checked)
                {
                    bool retVal2 = Sales.CreateSalesOpps();
                }

                // Insert/Delete EBT/Online Debit Sales Opps
                if (strCardPresent.ToString().Trim() == "CP")
                {
                    //Insert EBT Sales Opps
                    Sales.CreateEBTSalesOpps(chkEBT.Checked);

                    //Insert Online Debit Sales Opps
                    Sales.CreateOnlineDebitSalesOpps(chkOnlineDebit.Checked);
                }
                if ((strCardPresent.ToString().Trim() == "CNP") && (chkEBT.Checked))
                { DisplayMessage("EBT can only be selected for Card Present accounts."); }
                if ((strCardPresent.ToString().Trim() == "CNP") && (chkOnlineDebit.Checked))
                { DisplayMessage("Online Debit can only be selected for Card Present accounts."); }

                CommonFunctions GeneralInfo = new CommonFunctions(AppId);
                GeneralInfo.UpdateLastModified();

                if (retVal)
                {
                    DisplayMessage("Merchant Account Updated successfully.");
                    //Add action to Log table
                    PartnerLogBL LogData = new PartnerLogBL();
                    retVal = LogData.InsertLogData(AppId, Convert.ToInt32(Session["AffiliateID"]), "Merchant Account Updated");

                    Populate();
                }
                else
                    DisplayMessage("Rates cannot be applied.");
            }
            else
                DisplayMessage(errMessage);
        }//end try
        catch (Exception err)
        {
            CreateLog Log = new CreateLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), err.Message);
            DisplayMessage("Error applying rates ah drats.");
        }
    }//end submit button click
    #endregion

    #region Gateway Submit Button

    protected void btnGatewaySubmit_Click(object sender, EventArgs e)
    {
        bool retVal = false;

        string errMessage = "";

        errMessage = ValidateGatewayRates();

        if (errMessage == "")
        {

            OnlineAppProfile AType = new OnlineAppProfile(AppId);
            int iAType = 0;
            if ((chkMA.Checked) && (chkGateway.Checked))
                iAType = 4;
            else if (chkMA.Checked)
                iAType = 1;
            else if (chkGateway.Checked)
                iAType = 2;
            else
                iAType = 3;

            AType.UpdateAcctType(iAType);

            //Check Gateway sales opps and update it.
            Gateway GWRates = new Gateway(AppId);
            DataSet dsGateway = GWRates.GetGatewayInfo();
            if (chkGateway.Checked)
            {
                string strAType = "Gateway";
                NewAppInfo AppInfo = new NewAppInfo(AppId);
                string MerchStatus = AppInfo.ReturnStatus();
                //if ((MerchStatus.ToLower().Contains("completed")) || (MerchStatus.ToLower().Contains("submitted")) || (MerchStatus.ToLower().Contains("active")))
                if (!(MerchStatus.ToLower().Contains("incomplete")))
                {
                    AppInfo.UpdateReprogramStatus(1);
                    AppInfo.UpdateStatus("COMPLETED", strAType);
                }
                else if (MerchStatus.ToLower().Contains("incomplete"))
                    AppInfo.UpdateStatus("INCOMPLETE", strAType);

                OnlineAppClassLibrary.Gateway GatewayInfo2 = new OnlineAppClassLibrary.Gateway(AppId);
                retVal = GatewayInfo2.UpdateGatewayInfo(lstGatewayNames.SelectedItem.Text, txtGWMonthlyFee.Text.Trim(), txtGWSetupFee.Text.Trim(), txtGWTransFee.Text.Trim());

                CommonFunctions UpdateGeneralInfo = new CommonFunctions(AppId);
                //UpdateGeneralInfo.SetPageCount();
                UpdateGeneralInfo.SetGWPageCount();


                CommonFunctions Common = new CommonFunctions(AppId);
                Common.UpdateLastModified();
                Common.SetGWPageCount();

                if (dsGateway.Tables[0].Rows.Count > 0)
                {
                    DataRow drGateway = dsGateway.Tables[0].Rows[0];
                    if (lstGatewayNames.SelectedItem.Text != drGateway["Gateway"].ToString().Trim())
                    {
                        BusinessLayer.SalesOppsBL SalesOpps = new BusinessLayer.SalesOppsBL();
                        PartnerDS.OnlineAppSalesOppsDataTable dtGwySalesOpp = SalesOpps.GetSalesOpps(AppId);
                        if (dtGwySalesOpp.Rows.Count > 0)
                        {
                            string GwySalesOppID = "";
                            for (int i = 0; i < dtGwySalesOpp.Rows.Count; i++)
                            {
                                DataRow drGwySalesOpp = dtGwySalesOpp.Rows[i];

                                //if ((drGwySalesOpp["Product"].ToString().Trim()) == (drGateway["Gateway"].ToString().Trim()))
                                GwySalesOppID = drGwySalesOpp["ID"].ToString().Trim();
                                DLPartner.SalesOppDL GwySalesOppsDL = new DLPartner.SalesOppDL();
                                OnlineAppClassLibrary.Gateway GatewayInfo = new OnlineAppClassLibrary.Gateway(AppId);
                                retVal = GatewayInfo.UpdateGatewayInfo(lstGatewayNames.SelectedItem.Text, txtGWMonthlyFee.Text.Trim(), txtGWSetupFee.Text.Trim(), txtGWTransFee.Text.Trim());
                                OnlineAppClassLibrary.SalesOppsBL SalesOppGW = new OnlineAppClassLibrary.SalesOppsBL(AppId);
                                SalesOppGW.CreateSalesOppsGW();
                                if ((Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 5) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 20) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 36) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 60) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 65) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 74) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 96) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 150) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 253) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 282))
                                {
                                    int retvalSaleACT = GwySalesOppsDL.SalesOppAddedToACT(GwySalesOppID);
                                    if (retvalSaleACT == 0)
                                    {
                                        BusinessLayer.SalesOppsBL DeleteOpp = new BusinessLayer.SalesOppsBL();
                                        bool retDelete = DeleteOpp.DeleteSalesOpp(GwySalesOppID);
                                    }
                                }
                            }
                        }
                        else
                        {
                            OnlineAppClassLibrary.Gateway GatewayInfo = new OnlineAppClassLibrary.Gateway(AppId);
                            retVal = GatewayInfo.UpdateGatewayInfo(lstGatewayNames.SelectedItem.Text, txtGWMonthlyFee.Text.Trim(), txtGWSetupFee.Text.Trim(), txtGWTransFee.Text.Trim());
                            OnlineAppClassLibrary.SalesOppsBL SalesOppGW = new OnlineAppClassLibrary.SalesOppsBL(AppId);
                            SalesOppGW.CreateSalesOppsGW();
                        }

                    }
                }
                else
                {
                    BusinessLayer.SalesOppsBL SalesOpps = new BusinessLayer.SalesOppsBL();
                    PartnerDS.OnlineAppSalesOppsDataTable dtGwySalesOpp = SalesOpps.GetSalesOpps(AppId);
                    if (dtGwySalesOpp.Rows.Count > 0)
                    {
                        //if ((drGwySalesOpp["Product"].ToString().Trim()) != (lstGatewayNames.SelectedItem.Text))
                        string GwySalesOppID = "";
                        for (int i = 0; i < dtGwySalesOpp.Rows.Count; i++)
                        {
                            DataRow drGwySalesOpp = dtGwySalesOpp.Rows[i];

                            GwySalesOppID = drGwySalesOpp["ID"].ToString().Trim();
                            DLPartner.SalesOppDL GwySalesOppsDL = new DLPartner.SalesOppDL();
                            OnlineAppClassLibrary.Gateway GatewayInfo = new OnlineAppClassLibrary.Gateway(AppId);
                            retVal = GatewayInfo.UpdateGatewayInfo(lstGatewayNames.SelectedItem.Text, txtGWMonthlyFee.Text.Trim(), txtGWSetupFee.Text.Trim(), txtGWTransFee.Text.Trim());
                            OnlineAppClassLibrary.SalesOppsBL SalesOppGW = new OnlineAppClassLibrary.SalesOppsBL(AppId);
                            SalesOppGW.CreateSalesOppsGW();
                            if ((Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 5) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 20) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 36) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 60) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 65) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 74) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 96) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 150) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 253) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 282))
                            {
                                int retvalSaleACT = GwySalesOppsDL.SalesOppAddedToACT(GwySalesOppID);
                                if (retvalSaleACT == 0)
                                {
                                    BusinessLayer.SalesOppsBL DeleteOpp = new BusinessLayer.SalesOppsBL();
                                    bool retDelete = DeleteOpp.DeleteSalesOpp(GwySalesOppID);
                                }
                            }
                        }
                    }
                    else
                    {
                        OnlineAppClassLibrary.Gateway GatewayInfo = new OnlineAppClassLibrary.Gateway(AppId);
                        retVal = GatewayInfo.UpdateGatewayInfo(lstGatewayNames.SelectedItem.Text, txtGWMonthlyFee.Text.Trim(), txtGWSetupFee.Text.Trim(), txtGWTransFee.Text.Trim());
                        OnlineAppClassLibrary.SalesOppsBL SalesOppGW = new OnlineAppClassLibrary.SalesOppsBL(AppId);
                        SalesOppGW.CreateSalesOppsGW();
                    }
                }
            }
            else
            {
                //if (dsGateway.Tables[0].Rows.Count > 0)
                //{
                //DataRow drGateway = dsGateway.Tables[0].Rows[0];
                BusinessLayer.SalesOppsBL SalesOpps = new BusinessLayer.SalesOppsBL();
                PartnerDS.OnlineAppSalesOppsDataTable dtGwySalesOpp = SalesOpps.GetSalesOpps(AppId);
                if (dtGwySalesOpp.Rows.Count > 0)
                {
                    for (int i = 0; i < dtGwySalesOpp.Rows.Count; i++)
                    {
                        string GwySalesOppID = "";
                        DataRow drGwySalesOpp = dtGwySalesOpp.Rows[i];
                        GwySalesOppID = drGwySalesOpp["ID"].ToString().Trim();
                        DLPartner.SalesOppDL GwySalesOppsDL = new DLPartner.SalesOppDL();
                        //OnlineAppClassLibrary.Gateway GatewayInfo = new OnlineAppClassLibrary.Gateway(AppId);
                        //retVal = GatewayInfo.UpdateGatewayInfo(lstGatewayNames.SelectedItem.Text, txtGWMonthlyFee.Text.Trim(), txtGWSetupFee.Text.Trim(), txtGWTransFee.Text.Trim());
                        if ((Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 5) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 20) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 36) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 60) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 65) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 74) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 96) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 150) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 253) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 282))
                        {
                            int retvalSaleACT = GwySalesOppsDL.SalesOppAddedToACT(GwySalesOppID);
                            if (retvalSaleACT == 0)
                            {
                                BusinessLayer.SalesOppsBL DeleteOpp = new BusinessLayer.SalesOppsBL();
                                bool retDelete = DeleteOpp.DeleteSalesOpp(GwySalesOppID);
                            }
                        }

                    }
                }
                //}
            }

            OnlineAppClassLibrary.Gateway GatewayInfo1 = new OnlineAppClassLibrary.Gateway(AppId);
            retVal = GatewayInfo1.UpdateGatewayInfo(lstGatewayNames.SelectedItem.Text, txtGWMonthlyFee.Text, txtGWSetupFee.Text, txtGWTransFee.Text);

            CommonFunctions GeneralInfo = new CommonFunctions(AppId);
            GeneralInfo.UpdateLastModified();

            if (retVal)
            {
                DisplayMessage("Gateway updated successfully.");
                //Add action to Log table
                PartnerLogBL LogData = new PartnerLogBL();
                retVal = LogData.InsertLogData(AppId, Convert.ToInt32(Session["AffiliateID"]), "Gateway updated");

                Populate();
            }
            else
                DisplayMessage("Gateway not updated.");
        }
        else
            DisplayMessage(errMessage);
    }

    #endregion

    #region Additional Services Submit Button

    protected void btnAddlServSubmit_Click(object sender, EventArgs e)
    {



        bool retVal = false;

        bool leasechecked = false;

        OnlineAppBL UpdateAddlServ = new OnlineAppBL(AppId);
        retVal = UpdateAddlServ.UpdateAddlServices(chkOnlineDebit.Checked, chkCheckGuarantee.Checked,
            chkGiftCard.Checked, chkEBT.Checked, leasechecked, chkMerchantFunding.Checked, chkPayroll.Checked);

        OnlineAppClassLibrary.ProcessingInfo Processing = new OnlineAppClassLibrary.ProcessingInfo(AppId);
        retVal = Processing.UpdateAddlServices(txtWirelessAccess.Text.Trim(),
            txtWirelessTransFee.Text.Trim(), txtCGMonFee.Text.Trim(),
            txtCGTransFee.Text.Trim(), txtCGMonMin.Text.Trim(), txtCGDiscRate.Text.Trim(),
            txtGCMonFee.Text.Trim(), txtGCTransFee.Text.Trim());

        OnlineAppClassLibrary.NewAppInfo AddlServ = new OnlineAppClassLibrary.NewAppInfo(AppId);
        //retVal = AddlServ.UpdateCheckService(Convert.ToString(lstCheckService.SelectedItem).Trim());
        if ((txtCGDiscRate.Text != "") && (txtCGMonFee.Text != "") && (txtCGMonMin.Text != "") && (txtCGTransFee.Text != ""))
        {
            retVal = AddlServ.UpdateCheckService(Convert.ToString(lstCheckService.SelectedItem), Convert.ToDecimal(txtCGDiscRate.Text), Convert.ToDecimal(txtCGMonFee.Text), Convert.ToDecimal(txtCGTransFee.Text), Convert.ToDecimal(txtCGMonMin.Text));
        }
        else
        {
            retVal = AddlServ.UpdateCheckService(Convert.ToString(lstCheckService.SelectedItem).Trim());
        }
        retVal = AddlServ.UpdateGiftCardType(Convert.ToString(lstGCType.SelectedItem).Trim());

        retVal = AddlServ.UpdatePayrollType(lstPayrollType.SelectedItem.Text);

        retVal = AddlServ.UpdateMCAType(Convert.ToString(lstMCAType.SelectedItem).Trim());

        //retVal = AddlServ.UpdateMCAAmount(Convert.ToInt32(txtCashDesired.Text));

        //retVal = AddlServ.InsertUpdateLeaseInfo(Convert.ToString(lstLeaseCompany.SelectedItem).Trim(), Convert.ToString(txtLeasePayment).Trim(), Convert.ToString(lstLeaseTerm.SelectedItem).Trim());//update lease info

        OnlineAppProfile AType = new OnlineAppProfile(AppId);
        int iAType = 0;
        if ((chkMA.Checked) && (chkGateway.Checked))
            iAType = 4;
        else if (chkMA.Checked)
            iAType = 1;
        else if (chkGateway.Checked)
            iAType = 2;
        else
            iAType = 3;



        AType.UpdateAcctType(iAType);

        ProcessingInfo AddlProcessing = new ProcessingInfo(AppId);
        DataSet dsAddlProcessing = AddlProcessing.GetAddlServices();

        if (chkPayroll.Checked)
        {
            if (dsAddlProcessing.Tables[0].Rows.Count > 0)
            {
                DataRow drPayroll = dsAddlProcessing.Tables[0].Rows[0];
                if (lstPayrollType.SelectedItem.Text != drPayroll["PayrollType"].ToString().Trim())
                {
                    BusinessLayer.SalesOppsBL SalesOpps = new BusinessLayer.SalesOppsBL();
                    PartnerDS.OnlineAppSalesOppsDataTable dtPayrollSalesOpp = SalesOpps.GetSalesOpps(AppId);
                    if (dtPayrollSalesOpp.Rows.Count > 0)
                    {
                        string PayrollSalesOppID = "";
                        for (int i = 0; i < dtPayrollSalesOpp.Rows.Count; i++)
                        {
                            DataRow drPayrollSalesOpp = dtPayrollSalesOpp.Rows[i];

                            //if ((drGwySalesOpp["Product"].ToString().Trim()) == (drGateway["Gateway"].ToString().Trim()))
                            PayrollSalesOppID = drPayrollSalesOpp["ID"].ToString().Trim();
                            DLPartner.SalesOppDL PayrollSalesOppsDL = new DLPartner.SalesOppDL();
                            NewAppInfo PayrollInfo = new OnlineAppClassLibrary.NewAppInfo(AppId);
                            retVal = PayrollInfo.UpdatePayrollType(lstPayrollType.SelectedItem.Text);
                            OnlineAppClassLibrary.SalesOppsBL SalesOppPayroll = new OnlineAppClassLibrary.SalesOppsBL(AppId);
                            SalesOppPayroll.CreatePayrollSalesOpps(chkPayroll.Checked, lstPayrollType.SelectedItem.Text);
                            if ((Convert.ToInt16(drPayrollSalesOpp["ProductCode"]) == 233) || (Convert.ToInt16(drPayrollSalesOpp["ProductCode"]) == 234))
                            {
                                int retvalSaleACT = PayrollSalesOppsDL.SalesOppAddedToACT(PayrollSalesOppID);
                                if (retvalSaleACT == 0)
                                {
                                    BusinessLayer.SalesOppsBL DeleteOpp = new BusinessLayer.SalesOppsBL();
                                    bool retDelete = DeleteOpp.DeleteSalesOpp(PayrollSalesOppID);
                                }
                            }
                        }

                    }
                    else
                    {
                        NewAppInfo PayrollInfo = new OnlineAppClassLibrary.NewAppInfo(AppId);
                        retVal = PayrollInfo.UpdatePayrollType(lstPayrollType.SelectedItem.Text);
                        OnlineAppClassLibrary.SalesOppsBL SalesOppPayroll = new OnlineAppClassLibrary.SalesOppsBL(AppId);
                        SalesOppPayroll.CreatePayrollSalesOpps(chkPayroll.Checked, lstPayrollType.SelectedItem.Text);
                    }

                }
            }
            else
            {
                BusinessLayer.SalesOppsBL SalesOpps = new BusinessLayer.SalesOppsBL();
                PartnerDS.OnlineAppSalesOppsDataTable dtPayrollSalesOpp = SalesOpps.GetSalesOpps(AppId);
                if (dtPayrollSalesOpp.Rows.Count > 0)
                {
                    //if ((drGwySalesOpp["Product"].ToString().Trim()) != (lstGatewayNames.SelectedItem.Text))
                    string PayrollSalesOppID = "";
                    for (int i = 0; i < dtPayrollSalesOpp.Rows.Count; i++)
                    {
                        DataRow drPayrollSalesOpp = dtPayrollSalesOpp.Rows[i];

                        PayrollSalesOppID = drPayrollSalesOpp["ID"].ToString().Trim();
                        DLPartner.SalesOppDL PayrollSalesOppsDL = new DLPartner.SalesOppDL();
                        NewAppInfo PayrollInfo = new OnlineAppClassLibrary.NewAppInfo(AppId);
                        retVal = PayrollInfo.UpdatePayrollType(lstPayrollType.SelectedItem.Text);
                        OnlineAppClassLibrary.SalesOppsBL SalesOppPayroll = new OnlineAppClassLibrary.SalesOppsBL(AppId);
                        SalesOppPayroll.CreatePayrollSalesOpps(chkPayroll.Checked, lstPayrollType.SelectedItem.Text);
                        if ((Convert.ToInt16(drPayrollSalesOpp["ProductCode"]) == 233) || (Convert.ToInt16(drPayrollSalesOpp["ProductCode"]) == 234))
                        {
                            int retvalSaleACT = PayrollSalesOppsDL.SalesOppAddedToACT(PayrollSalesOppID);
                            if (retvalSaleACT == 0)
                            {
                                BusinessLayer.SalesOppsBL DeleteOpp = new BusinessLayer.SalesOppsBL();
                                bool retDelete = DeleteOpp.DeleteSalesOpp(PayrollSalesOppID);
                            }
                        }
                    }
                }
                else
                {
                    NewAppInfo PayrollInfo = new OnlineAppClassLibrary.NewAppInfo(AppId);
                    retVal = PayrollInfo.UpdatePayrollType(lstPayrollType.SelectedItem.Text);
                    OnlineAppClassLibrary.SalesOppsBL SalesOppPayroll = new OnlineAppClassLibrary.SalesOppsBL(AppId);
                    SalesOppPayroll.CreatePayrollSalesOpps(chkPayroll.Checked, lstPayrollType.SelectedItem.Text);
                }
            }
        }
        /*else
        {
            //if (dsGateway.Tables[0].Rows.Count > 0)
            //{
            //DataRow drGateway = dsGateway.Tables[0].Rows[0];
            BusinessLayer.SalesOppsBL SalesOpps = new BusinessLayer.SalesOppsBL();
            PartnerDS.OnlineAppSalesOppsDataTable dtPayrollSalesOpp = SalesOpps.GetSalesOpps(AppId);
            if (dtPayrollSalesOpp.Rows.Count > 0)
            {
                for (int i = 0; i < dtPayrollSalesOpp.Rows.Count; i++)
                {
                    string PayrollSalesOppID = "";
                    DataRow drPayrollSalesOpp = dtPayrollSalesOpp.Rows[i];
                    PayrollSalesOppID = drPayrollSalesOpp["ID"].ToString().Trim();
                    DLPartner.SalesOppDL PayrollSalesOppsDL = new DLPartner.SalesOppDL();
                    //OnlineAppClassLibrary.Gateway GatewayInfo = new OnlineAppClassLibrary.Gateway(AppId);
                    //retVal = GatewayInfo.UpdateGatewayInfo(lstGatewayNames.SelectedItem.Text, txtGWMonthlyFee.Text.Trim(), txtGWSetupFee.Text.Trim(), txtGWTransFee.Text.Trim());
                    if ((Convert.ToInt16(drPayrollSalesOpp["ProductCode"]) == 233) || (Convert.ToInt16(drPayrollSalesOpp["ProductCode"]) == 234))
                    {
                        int retvalSaleACT = PayrollSalesOppsDL.SalesOppAddedToACT(PayrollSalesOppID);
                        if (retvalSaleACT == 0)
                        {
                            BusinessLayer.SalesOppsBL DeleteOpp = new BusinessLayer.SalesOppsBL();
                            bool retDelete = DeleteOpp.DeleteSalesOpp(PayrollSalesOppID);
                        }
                    }

                }
            }
            //}
        }*/



        if (chkCheckGuarantee.Checked)
        {
            if (dsAddlProcessing.Tables[0].Rows.Count > 0)
            {
                DataRow drCheckService = dsAddlProcessing.Tables[0].Rows[0];
                if (Convert.ToString(lstCheckService.SelectedItem) != Convert.ToString(drCheckService["CheckService"]).Trim())
                {
                    BusinessLayer.SalesOppsBL SalesOpps = new BusinessLayer.SalesOppsBL();
                    PartnerDS.OnlineAppSalesOppsDataTable dtCheckServiceSalesOpp = SalesOpps.GetSalesOpps(AppId);
                    if (dtCheckServiceSalesOpp.Rows.Count > 0)
                    {
                        string CheckServiceSalesOppID = "";
                        for (int i = 0; i < dtCheckServiceSalesOpp.Rows.Count; i++)
                        {
                            DataRow drCheckServiceSalesOpp = dtCheckServiceSalesOpp.Rows[i];

                            //if ((drGwySalesOpp["Product"].ToString().Trim()) == (drGateway["Gateway"].ToString().Trim()))
                            CheckServiceSalesOppID = drCheckServiceSalesOpp["ID"].ToString().Trim();
                            DLPartner.SalesOppDL CheckServiceSalesOppsDL = new DLPartner.SalesOppDL();
                            NewAppInfo CheckServiceInfo = new OnlineAppClassLibrary.NewAppInfo(AppId);
                            //retVal = CheckServiceInfo.UpdateCheckService(lstCheckService.SelectedItem.Text, Convert.ToDecimal(txtCGDiscRate.ToString().Trim()), Convert.ToDecimal(txtCGMonFee.ToString().Trim()), Convert.ToDecimal(txtCGTransFee.ToString().Trim()), Convert.ToDecimal(txtCGMonMin.ToString().Trim()));
                            OnlineAppClassLibrary.SalesOppsBL SalesOppCheckService = new OnlineAppClassLibrary.SalesOppsBL(AppId);
                            SalesOppCheckService.CreateCheckServicesSalesOpps(chkCheckGuarantee.Checked, lstCheckService.SelectedItem.Text);
                            if ((Convert.ToInt16(drCheckServiceSalesOpp["ProductCode"]) == 11) || (Convert.ToInt16(drCheckServiceSalesOpp["ProductCode"]) == 134) || (Convert.ToInt16(drCheckServiceSalesOpp["ProductCode"]) == 179) || (Convert.ToInt16(drCheckServiceSalesOpp["ProductCode"]) == 178) || (Convert.ToInt16(drCheckServiceSalesOpp["ProductCode"]) == 42))
                            {
                                int retvalSaleACT = CheckServiceSalesOppsDL.SalesOppAddedToACT(CheckServiceSalesOppID);
                                if (retvalSaleACT == 0)
                                {
                                    BusinessLayer.SalesOppsBL DeleteOpp = new BusinessLayer.SalesOppsBL();
                                    bool retDelete = DeleteOpp.DeleteSalesOpp(CheckServiceSalesOppID);
                                }
                            }
                        }

                    }
                    else
                    {
                        NewAppInfo CheckServiceInfo = new OnlineAppClassLibrary.NewAppInfo(AppId);
                        retVal = CheckServiceInfo.UpdateCheckService(lstCheckService.SelectedItem.Text, Convert.ToDecimal(txtCGDiscRate.ToString().Trim()), Convert.ToDecimal(txtCGMonFee.ToString().Trim()), Convert.ToDecimal(txtCGTransFee.ToString().Trim()), Convert.ToDecimal(txtCGMonMin.ToString().Trim()));
                        OnlineAppClassLibrary.SalesOppsBL SalesOppCheckService = new OnlineAppClassLibrary.SalesOppsBL(AppId);
                        SalesOppCheckService.CreateCheckServicesSalesOpps(chkCheckGuarantee.Checked, lstCheckService.SelectedItem.Text);
                    }

                }
            }
            else
            {
                BusinessLayer.SalesOppsBL SalesOpps = new BusinessLayer.SalesOppsBL();
                PartnerDS.OnlineAppSalesOppsDataTable dtCheckServiceSalesOpp = SalesOpps.GetSalesOpps(AppId);
                if (dtCheckServiceSalesOpp.Rows.Count > 0)
                {
                    //if ((drGwySalesOpp["Product"].ToString().Trim()) != (lstGatewayNames.SelectedItem.Text))
                    string CheckServiceSalesOppID = "";
                    for (int i = 0; i < dtCheckServiceSalesOpp.Rows.Count; i++)
                    {
                        DataRow drCheckServiceSalesOpp = dtCheckServiceSalesOpp.Rows[i];

                        CheckServiceSalesOppID = drCheckServiceSalesOpp["ID"].ToString().Trim();
                        DLPartner.SalesOppDL CheckServiceSalesOppsDL = new DLPartner.SalesOppDL();
                        NewAppInfo CheckServiceInfo = new OnlineAppClassLibrary.NewAppInfo(AppId);
                        retVal = CheckServiceInfo.UpdateCheckService(lstCheckService.SelectedItem.Text, Convert.ToDecimal(txtCGDiscRate.ToString().Trim()), Convert.ToDecimal(txtCGMonFee.ToString().Trim()), Convert.ToDecimal(txtCGTransFee.ToString().Trim()), Convert.ToDecimal(txtCGMonMin.ToString().Trim()));
                        OnlineAppClassLibrary.SalesOppsBL SalesOppCheckService = new OnlineAppClassLibrary.SalesOppsBL(AppId);
                        SalesOppCheckService.CreateCheckServicesSalesOpps(chkCheckGuarantee.Checked, lstCheckService.SelectedItem.Text);
                        if ((Convert.ToInt16(drCheckServiceSalesOpp["ProductCode"]) == 11) || (Convert.ToInt16(drCheckServiceSalesOpp["ProductCode"]) == 134) || (Convert.ToInt16(drCheckServiceSalesOpp["ProductCode"]) == 179) || (Convert.ToInt16(drCheckServiceSalesOpp["ProductCode"]) == 178) || (Convert.ToInt16(drCheckServiceSalesOpp["ProductCode"]) == 42))
                        {
                            int retvalSaleACT = CheckServiceSalesOppsDL.SalesOppAddedToACT(CheckServiceSalesOppID);
                            if (retvalSaleACT == 0)
                            {
                                BusinessLayer.SalesOppsBL DeleteOpp = new BusinessLayer.SalesOppsBL();
                                bool retDelete = DeleteOpp.DeleteSalesOpp(CheckServiceSalesOppID);
                            }
                        }
                    }
                }
                else
                {
                    NewAppInfo CheckServiceInfo = new OnlineAppClassLibrary.NewAppInfo(AppId);
                    //retVal = CheckServiceInfo.UpdateCheckService(lstCheckService.SelectedItem.Text);
                    retVal = CheckServiceInfo.UpdateCheckService(lstCheckService.SelectedItem.Text, Convert.ToDecimal(txtCGDiscRate.ToString().Trim()), Convert.ToDecimal(txtCGMonFee.ToString().Trim()), Convert.ToDecimal(txtCGTransFee.ToString().Trim()), Convert.ToDecimal(txtCGMonMin.ToString().Trim()));
                    OnlineAppClassLibrary.SalesOppsBL SalesOppCheckService = new OnlineAppClassLibrary.SalesOppsBL(AppId);
                    SalesOppCheckService.CreateCheckServicesSalesOpps(chkCheckGuarantee.Checked, lstCheckService.SelectedItem.Text);
                }
            }
        }
        else
        {
            //if (dsGateway.Tables[0].Rows.Count > 0)
            //{
            //DataRow drGateway = dsGateway.Tables[0].Rows[0];


            //NewAppInfo CheckServiceInfo = new OnlineAppClassLibrary.NewAppInfo(AppId);
            //retVal = CheckServiceInfo.UpdateCheckService(lstCheckService.SelectedItem.Text, Convert.ToDecimal(txtCGDiscRate.ToString().Trim()), Convert.ToDecimal(txtCGMonFee.ToString().Trim()), Convert.ToDecimal(txtCGTransFee.ToString().Trim()), Convert.ToDecimal(txtCGMonMin.ToString().Trim()));


            BusinessLayer.SalesOppsBL SalesOpps = new BusinessLayer.SalesOppsBL();
            PartnerDS.OnlineAppSalesOppsDataTable dtCheckServiceSalesOpp = SalesOpps.GetSalesOpps(AppId);
            if (dtCheckServiceSalesOpp.Rows.Count > 0)
            {
                for (int i = 0; i < dtCheckServiceSalesOpp.Rows.Count; i++)
                {
                    string CheckServiceSalesOppID = "";
                    DataRow drCheckServiceSalesOpp = dtCheckServiceSalesOpp.Rows[i];
                    CheckServiceSalesOppID = Convert.ToString(drCheckServiceSalesOpp["ID"]).Trim();
                    DLPartner.SalesOppDL CheckServiceSalesOppsDL = new DLPartner.SalesOppDL();
                    NewAppInfo CheckServiceInfo = new OnlineAppClassLibrary.NewAppInfo(AppId);
                    //OnlineAppClassLibrary.Gateway GatewayInfo = new OnlineAppClassLibrary.Gateway(AppId);
                    //retVal = GatewayInfo.UpdateGatewayInfo(lstGatewayNames.SelectedItem.Text, txtGWMonthlyFee.Text.Trim(), txtGWSetupFee.Text.Trim(), txtGWTransFee.Text.Trim());
                    retVal = AddlServ.UpdateCheckService(Convert.ToString(lstCheckService.SelectedItem).Trim());
                    //retVal = CheckServiceInfo.UpdateCheckService(lstCheckService.SelectedItem.Text, Convert.ToDecimal(txtCGDiscRate.ToString().Trim()), Convert.ToDecimal(txtCGMonFee.ToString().Trim()), Convert.ToDecimal(txtCGTransFee.ToString().Trim()), Convert.ToDecimal(txtCGMonMin.ToString().Trim()));
                    //if ((Convert.ToInt16(drCheckServiceSalesOpp["ProductCode"]) == 11) || (Convert.ToInt16(drCheckServiceSalesOpp["ProductCode"]) == 134) || (Convert.ToInt16(drCheckServiceSalesOpp["ProductCode"]) == 179) || (Convert.ToInt16(drCheckServiceSalesOpp["ProductCode"]) == 178) || (Convert.ToInt16(drCheckServiceSalesOpp["ProductCode"]) == 42))
                    {
                        int retvalSaleACT = CheckServiceSalesOppsDL.SalesOppAddedToACT(CheckServiceSalesOppID);
                        if (retvalSaleACT == 0)
                        {
                            BusinessLayer.SalesOppsBL DeleteOpp = new BusinessLayer.SalesOppsBL();
                            bool retDelete = DeleteOpp.DeleteSalesOpp(CheckServiceSalesOppID);
                        }
                    }

                }
            }
            //}
        }



        if (chkMerchantFunding.Checked)
        {
            if (dsAddlProcessing.Tables[0].Rows.Count > 0)
            {
                DataRow drMCA = dsAddlProcessing.Tables[0].Rows[0];
                //if (lstMCAType.SelectedItem.Text != Convert.ToString(drMCA["MCAType"]).Trim())
                {
                    BusinessLayer.SalesOppsBL SalesOpps = new BusinessLayer.SalesOppsBL();
                    PartnerDS.OnlineAppSalesOppsDataTable dtMCASalesOpp = SalesOpps.GetSalesOpps(AppId);
                    if (dtMCASalesOpp.Rows.Count > 0)
                    {
                        string MCASalesOppID = "";
                        for (int i = 0; i < dtMCASalesOpp.Rows.Count; i++)
                        {
                            DataRow drMCASalesOpp = dtMCASalesOpp.Rows[i];

                            //if ((drGwySalesOpp["Product"].ToString().Trim()) == (drGateway["Gateway"].ToString().Trim()))
                            MCASalesOppID = drMCASalesOpp["ID"].ToString().Trim();
                            DLPartner.SalesOppDL MCASalesOppsDL = new DLPartner.SalesOppDL();
                            NewAppInfo MCAInfo = new OnlineAppClassLibrary.NewAppInfo(AppId);
                            retVal = MCAInfo.UpdateMCAType(lstMCAType.SelectedItem.Text);
                            if (Convert.ToString(txtCashDesired.Text) != "")
                            {
                                retVal = MCAInfo.UpdateMCAAmount(Convert.ToInt32(txtCashDesired.Text));
                            }
                            else
                            {
                                retVal = MCAInfo.UpdateMCAAmount(" ");
                            }
                            OnlineAppClassLibrary.SalesOppsBL SalesOppMCA = new OnlineAppClassLibrary.SalesOppsBL(AppId);
                            SalesOppMCA.CreateMCASalesOpps(chkMerchantFunding.Checked, lstMCAType.SelectedItem.Text);
                            if ((Convert.ToInt16(drMCASalesOpp["ProductCode"]) == 113) || (Convert.ToInt16(drMCASalesOpp["ProductCode"]) == 263) || (Convert.ToInt16(drMCASalesOpp["ProductCode"]) == 315))
                            {
                                int retvalSaleACT = MCASalesOppsDL.SalesOppAddedToACT(MCASalesOppID);
                                if (retvalSaleACT == 0)
                                {
                                    BusinessLayer.SalesOppsBL DeleteOpp = new BusinessLayer.SalesOppsBL();
                                    bool retDelete = DeleteOpp.DeleteSalesOpp(MCASalesOppID);
                                }
                            }
                        }
                    }
                    else
                    {
                        NewAppInfo MCAInfo = new OnlineAppClassLibrary.NewAppInfo(AppId);
                        retVal = MCAInfo.UpdateMCAType(lstMCAType.SelectedItem.Text);
                        if (Convert.ToString(txtCashDesired.Text) != "")
                        {
                            retVal = MCAInfo.UpdateMCAAmount(Convert.ToInt32(txtCashDesired.Text));
                        }
                        else
                        {
                            retVal = MCAInfo.UpdateMCAAmount(" ");
                        }
                        OnlineAppClassLibrary.SalesOppsBL SalesOppMCA = new OnlineAppClassLibrary.SalesOppsBL(AppId);
                        SalesOppMCA.CreateMCASalesOpps(chkMerchantFunding.Checked, lstMCAType.SelectedItem.Text);
                    }

                }
            }
            else
            {
                BusinessLayer.SalesOppsBL SalesOpps = new BusinessLayer.SalesOppsBL();
                PartnerDS.OnlineAppSalesOppsDataTable dtMCASalesOpp = SalesOpps.GetSalesOpps(AppId);
                if (dtMCASalesOpp.Rows.Count > 0)
                {
                    //if ((drGwySalesOpp["Product"].ToString().Trim()) != (lstGatewayNames.SelectedItem.Text))
                    string MCASalesOppID = "";
                    for (int i = 0; i < dtMCASalesOpp.Rows.Count; i++)
                    {
                        DataRow drMCASalesOpp = dtMCASalesOpp.Rows[i];

                        MCASalesOppID = drMCASalesOpp["ID"].ToString().Trim();
                        DLPartner.SalesOppDL MCASalesOppsDL = new DLPartner.SalesOppDL();
                        NewAppInfo MCAInfo = new OnlineAppClassLibrary.NewAppInfo(AppId);
                        retVal = MCAInfo.UpdateMCAType(lstMCAType.SelectedItem.Text);
                        if (Convert.ToString(txtCashDesired.Text) != "")
                        {
                            retVal = MCAInfo.UpdateMCAAmount(Convert.ToInt32(txtCashDesired.Text));
                        }
                        else
                        {
                            retVal = MCAInfo.UpdateMCAAmount(" ");
                        }

                        retVal = MCAInfo.UpdateMCAAmount(" ");

                        OnlineAppClassLibrary.SalesOppsBL SalesOppMCA = new OnlineAppClassLibrary.SalesOppsBL(AppId);
                        SalesOppMCA.CreateMCASalesOpps(chkMerchantFunding.Checked, lstMCAType.SelectedItem.Text);
                        if ((Convert.ToInt16(drMCASalesOpp["ProductCode"]) == 113) || (Convert.ToInt16(drMCASalesOpp["ProductCode"]) == 263) || (Convert.ToInt16(drMCASalesOpp["ProductCode"]) == 315))
                        {
                            int retvalSaleACT = MCASalesOppsDL.SalesOppAddedToACT(MCASalesOppID);
                            if (retvalSaleACT == 0)
                            {
                                BusinessLayer.SalesOppsBL DeleteOpp = new BusinessLayer.SalesOppsBL();
                                bool retDelete = DeleteOpp.DeleteSalesOpp(MCASalesOppID);
                            }
                        }
                    }
                }
                else
                {
                    NewAppInfo MCAInfo = new OnlineAppClassLibrary.NewAppInfo(AppId);
                    retVal = MCAInfo.UpdateMCAType(lstMCAType.SelectedItem.Text);


                    if (Convert.ToString(txtCashDesired.Text) != "")
                    {
                        retVal = MCAInfo.UpdateMCAAmount(Convert.ToInt32(txtCashDesired.Text));
                    }
                    else
                    {
                        retVal = MCAInfo.UpdateMCAAmount(" ");
                    }

                    retVal = MCAInfo.UpdateMCAAmount(" ");
                    OnlineAppClassLibrary.SalesOppsBL SalesOppMCA = new OnlineAppClassLibrary.SalesOppsBL(AppId);
                    SalesOppMCA.CreateMCASalesOpps(chkMerchantFunding.Checked, lstMCAType.SelectedItem.Text);
                }
            }
        }
        else
        {
            //if (dsGateway.Tables[0].Rows.Count > 0)
            //{
            //DataRow drGateway = dsGateway.Tables[0].Rows[0];
            BusinessLayer.SalesOppsBL SalesOpps = new BusinessLayer.SalesOppsBL();
            PartnerDS.OnlineAppSalesOppsDataTable dtMCASalesOpp = SalesOpps.GetSalesOpps(AppId);
            if (dtMCASalesOpp.Rows.Count > 0)
            {
                for (int i = 0; i < dtMCASalesOpp.Rows.Count; i++)
                {
                    string MCASalesOppID = "";
                    DataRow drMCASalesOpp = dtMCASalesOpp.Rows[i];
                    MCASalesOppID = drMCASalesOpp["ID"].ToString().Trim();
                    DLPartner.SalesOppDL MCASalesOppsDL = new DLPartner.SalesOppDL();
                    //OnlineAppClassLibrary.Gateway GatewayInfo = new OnlineAppClassLibrary.Gateway(AppId);
                    //retVal = GatewayInfo.UpdateGatewayInfo(lstGatewayNames.SelectedItem.Text, txtGWMonthlyFee.Text.Trim(), txtGWSetupFee.Text.Trim(), txtGWTransFee.Text.Trim());
                    if ((Convert.ToInt16(drMCASalesOpp["ProductCode"]) == 113) || (Convert.ToInt16(drMCASalesOpp["ProductCode"]) == 263) || (Convert.ToInt16(drMCASalesOpp["ProductCode"]) == 315))
                    {
                        int retvalSaleACT = MCASalesOppsDL.SalesOppAddedToACT(MCASalesOppID);
                        if (retvalSaleACT == 0)
                        {
                            BusinessLayer.SalesOppsBL DeleteOpp = new BusinessLayer.SalesOppsBL();
                            bool retDelete = DeleteOpp.DeleteSalesOpp(MCASalesOppID);
                        }
                    }

                }
            }
            //}
        }

        CommonFunctions GeneralInfo = new CommonFunctions(AppId);
        GeneralInfo.UpdateLastModified();



        if (retVal)
        {
            DisplayMessage("Additional services updated successfully.");
            //Add action to Log table
            PartnerLogBL LogData = new PartnerLogBL();
            retVal = LogData.InsertLogData(AppId, Convert.ToInt32(Session["AffiliateID"]), "Additional services updated");

            Populate();
        }
        else
            DisplayMessage("Additional services not updated.");


    }

    #endregion

    #region PROCESSOR NAME CHANGED
    protected void lstProcessorNames_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string strAnnualFee = lstAnnualFee.SelectedValue.ToString();
            OnlineAppProcessingBL Processing = new OnlineAppProcessingBL(AppId);

            CheckProcessor();
            ReloadBounds();
            DisableFees();
            PopulateGatewayList();
            PartnerDS.OnlineAppRatesDataTable dt = Processing.GetRates();
            if (dt.Rows.Count > 0)
            {
                if (lstGatewayNames.Items.FindByValue(dt[0].Gateway.ToString()) == null)
                {
                    ListItem gwyItem = new ListItem();
                    gwyItem.Value = dt[0].Gateway.ToString();
                    gwyItem.Text = dt[0].Gateway.ToString();
                    lstGatewayNames.Items.Add(gwyItem);
                }

                if (lstGatewayNames.Items.FindByValue(dt[0].Gateway.ToString()) != null)
                {
                    lstGatewayNames.SelectedValue = dt[0].Gateway.ToString();
                    //lstGatewayNames.Selecteditem.Text = dt[0].Gateway.ToString();
                }
            }

            if (lstProcessorNames.SelectedItem.Text.ToLower().Contains("Sage"))
            {
                lstDebitTransFee.Visible = true;
                lstAVS.Visible = true;
                txtDebitTransFee.Visible = false;
                txtAVS.Visible = false;
            }
            else
            {
                txtDebitTransFee.Visible = true;
                txtAVS.Visible = true;
                lstDebitTransFee.Visible = false;
                lstAVS.Visible = false;
            }
            if (lstRateNames.SelectedItem.Text.Contains("Interchange"))
                LoadInterchangeBounds();

            if (lstAnnualFee.Items.FindByValue(strAnnualFee) != null)
                lstAnnualFee.SelectedValue = strAnnualFee;

            lstCheckServicePopulateItems();

            OnlineAppBL AddlServ = new OnlineAppBL(AppId);
            DataSet dsAddl = AddlServ.GetAddlServiceBits();
            DataSet dsLeaseInfo = AddlServ.ReturnAddlServices();

            if (dsAddl.Tables[0].Rows.Count > 0)
            {
                DataRow drAddl = dsAddl.Tables[0].Rows[0];
                if (Convert.ToBoolean(drAddl["CheckServices"]))
                {
                    chkCheckGuarantee.Checked = true;
                    lstCheckService.Enabled = true;
                    txtCGDiscRate.Enabled = true;
                    txtCGMonFee.Enabled = true;
                    txtCGMonMin.Enabled = true;
                    txtCGTransFee.Enabled = true;
                    //lstCheckService.SelectedIndex = lstCheckService.Items.IndexOf(lstCheckService.Items.FindByText(Server.HtmlEncode(dt[0].CheckService.ToString().Trim())));
                    //lstCheckService.Selected 
                    lstCheckService.SelectedItem.Text = Convert.ToString(dt[0].CheckService).Trim();
                    lstCheckService.SelectedItem.Value = Convert.ToString(dt[0].CheckService).Trim();
                    txtCGDiscRate.Text = Server.HtmlEncode(dt[0].CGDiscRate.ToString());
                    txtCGMonFee.Text = Server.HtmlEncode(dt[0].CGMonFee.ToString());
                    txtCGMonMin.Text = Server.HtmlEncode(dt[0].CGMonMin.ToString());
                    txtCGTransFee.Text = Server.HtmlEncode(dt[0].CGTransFee.ToString());
                    if ((txtCGDiscRate.Text == "") && (txtCGMonFee.Text == "") && (txtCGMonMin.Text == "") && (txtCGTransFee.Text == ""))
                    {
                        BoundsBL Bounds = new BoundsBL();
                        DataSet ds = Bounds.GetCSBounds(lstCheckService.SelectedItem.Text);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            txtCGMonFee.Text = dr["CGMonFeeDef"].ToString().Trim();
                            txtCGTransFee.Text = dr["CGTransFeeDef"].ToString().Trim();
                            txtCGMonMin.Text = dr["CGMonMinDef"].ToString().Trim();
                            txtCGDiscRate.Text = dr["CGDiscRateDef"].ToString().Trim();
                        }//end if count not 0
                    }
                }
            }

            //Populate();
        }//end try
        catch (Exception err)
        {
            CreateLog Log = new CreateLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), err.Message);
            DisplayMessage("Error loading processor bounds.");
        }
    }//end processor selected index changed
    #endregion

    //Fill MidQual, NonQual and DiscRateDebit based on DiscRatePres or DiscRateNotPres
    protected void txtDRQP_TextChanged(object sender, EventArgs e)
    {
        // helps to convert to double below otherwise throws input string error
        if (DiscRateMidQualStep.Value == "")
            DiscRateMidQualStep.Value = "0.00";
        if (DiscRateNonQualStep.Value == "")
            DiscRateNonQualStep.Value = "0.00";
        OnlineAppProcessingBL Processing = new OnlineAppProcessingBL(AppId);
        PartnerDS.OnlineAppRatesDataTable dt = Processing.GetRates();
        if (rdbCP.Checked)
        {
            if (lstRateNames.SelectedItem.Text.Contains("Interchange"))
            {
                txtDRMQ.Text = txtDRQP.Text;
                txtDRNQ.Text = txtDRQP.Text;
                //txtDRQD.Text = txtDRQP.Text;
            }
            else
            {
                txtDRMQ.Text = Convert.ToString(Convert.ToDouble(txtDRQP.Text.ToString()) + Convert.ToDouble(DiscRateMidQualStep.Value));
                txtDRNQ.Text = Convert.ToString(Convert.ToDouble(txtDRQP.Text.ToString()) + Convert.ToDouble(DiscRateNonQualStep.Value));
                //txtDRQD.Text = txtDRQP.Text;
            }
        }
        else if (rdbCNP.Checked)
        {
            if (lstRateNames.SelectedItem.Text.Contains("Interchange"))
            {
                txtDRMQ.Text = txtDRQP.Text;
                txtDRNQ.Text = txtDRQP.Text;
                //txtDRQD.Text = txtDRQNP.Text;
            }
            else
            {
                txtDRMQ.Text = Convert.ToString(Convert.ToDouble(txtDRQP.Text.ToString()) + Convert.ToDouble(DiscRateMidQualStep.Value));
                txtDRNQ.Text = Convert.ToString(Convert.ToDouble(txtDRQP.Text.ToString()) + Convert.ToDouble(DiscRateNonQualStep.Value));
                //txtDRQD.Text = txtDRQNP.Text;
            }
        }
        DisableFees();
    }

    protected void txtAmexQual_TextChanged(object sender, EventArgs e)
    {
        // helps to convert to double below otherwise throws input string error
        if (DiscRateMidQualStep.Value == "")
            DiscRateMidQualStep.Value = "0.00";
        if (DiscRateNonQualStep.Value == "")
            DiscRateNonQualStep.Value = "0.00";
        OnlineAppProcessingBL Processing = new OnlineAppProcessingBL(AppId);
        PartnerDS.OnlineAppRatesDataTable dt = Processing.GetRates();
        if (rdbCP.Checked)
        {
            if (lstRateNames.SelectedItem.Text.Contains("Interchange"))
            {
                txtAmexMidQual.Text = txtAmexQual.Text;
                txtAmexNonQual.Text = txtAmexQual.Text;
            }
            else
            {
                txtAmexMidQual.Text = Convert.ToString(Convert.ToDouble(txtAmexQual.Text.ToString()) + Convert.ToDouble(DiscRateMidQualStep.Value));
                txtAmexNonQual.Text = Convert.ToString(Convert.ToDouble(txtAmexQual.Text.ToString()) + Convert.ToDouble(DiscRateNonQualStep.Value));
                //Text = txtDRQP.Text;
            }
        }
        else if (rdbCNP.Checked)
        {
            if (lstRateNames.SelectedItem.Text.Contains("Interchange"))
            {
                txtAmexMidQual.Text = txtAmexQual.Text;
                txtAmexNonQual.Text = txtAmexQual.Text;
            }
            else
            {
                txtAmexMidQual.Text = Convert.ToString(Convert.ToDouble(txtAmexQual.Text.ToString()) + Convert.ToDouble(DiscRateMidQualStep.Value));
                txtAmexNonQual.Text = Convert.ToString(Convert.ToDouble(txtAmexQual.Text.ToString()) + Convert.ToDouble(DiscRateNonQualStep.Value));
                //txtDRQD.Text = txtDRQNP.Text;
            }
        }
        DisableFees();
    }

    protected void txtDRQD_TextChanged(object sender, EventArgs e)
    {
        // helps to convert to double below otherwise throws input string error
        if (DiscRateMidQualStep.Value == "")
            DiscRateMidQualStep.Value = "0.00";
        if (DiscRateNonQualStep.Value == "")
            DiscRateNonQualStep.Value = "0.00";
        OnlineAppProcessingBL Processing = new OnlineAppProcessingBL(AppId);
        PartnerDS.OnlineAppRatesDataTable dt = Processing.GetRates();
        if (rdbCP.Checked)
        {
            if (lstRateNames.SelectedItem.Text.Contains("Interchange"))
            {
                txtMidQualDebit.Text = txtDRQD.Text;
                txtNonQualDebit.Text = txtDRQD.Text;
                //txtDRQD.Text = txtDRQD.Text;
            }
            else
            {
                txtMidQualDebit.Text = Convert.ToString(Convert.ToDouble(txtDRQD.Text.ToString()) + Convert.ToDouble(DiscRateMidQualStep.Value));
                txtNonQualDebit.Text = Convert.ToString(Convert.ToDouble(txtDRQD.Text.ToString()) + Convert.ToDouble(DiscRateNonQualStep.Value));
                //txtDRQD.Text = txtDRQP.Text;
            }
        }
        else if (rdbCNP.Checked)
        {
            if (lstRateNames.SelectedItem.Text.Contains("Interchange"))
            {
                txtMidQualDebit.Text = txtDRQD.Text;
                txtNonQualDebit.Text = txtDRQD.Text;

            }
            else
            {
                txtMidQualDebit.Text = Convert.ToString(Convert.ToDouble(txtDRQD.Text.ToString()) + Convert.ToDouble(DiscRateMidQualStep.Value));
                txtNonQualDebit.Text = Convert.ToString(Convert.ToDouble(txtDRQD.Text.ToString()) + Convert.ToDouble(DiscRateNonQualStep.Value));
                //txtDRQD.Text = txtDRQNP.Text;
            }
        }
        DisableFees();
    }

    protected void txtQualNPDebit_TextChanged(object sender, EventArgs e)
    {
        // helps to convert to double below otherwise throws input string error
        if (DiscRateMidQualStep.Value == "")
            DiscRateMidQualStep.Value = "0.00";
        if (DiscRateNonQualStep.Value == "")
            DiscRateNonQualStep.Value = "0.00";
        OnlineAppProcessingBL Processing = new OnlineAppProcessingBL(AppId);
        PartnerDS.OnlineAppRatesDataTable dt = Processing.GetRates();
        if (rdbCP.Checked)
        {
            if (lstRateNames.SelectedItem.Text.Contains("Interchange"))
            {
                txtMidQualDebit.Text = txtDRQD.Text;
                txtNonQualDebit.Text = txtDRQD.Text;
                //txtDRQD.Text = txtDRQD.Text;
            }
            else
            {
                txtMidQualDebit.Text = Convert.ToString(Convert.ToDouble(txtDRQD.Text.ToString()) + Convert.ToDouble(DiscRateMidQualStep.Value));
                txtNonQualDebit.Text = Convert.ToString(Convert.ToDouble(txtDRQD.Text.ToString()) + Convert.ToDouble(DiscRateNonQualStep.Value));
                //txtDRQD.Text = txtDRQP.Text;
            }
        }
        else if (rdbCNP.Checked)
        {
            if (lstRateNames.SelectedItem.Text.Contains("Interchange"))
            {
                txtMidQualDebit.Text = txtDRQD.Text;
                txtNonQualDebit.Text = txtDRQD.Text;

            }
            else
            {
                txtMidQualDebit.Text = Convert.ToString(Convert.ToDouble(txtDRQD.Text.ToString()) + Convert.ToDouble(DiscRateMidQualStep.Value));
                txtNonQualDebit.Text = Convert.ToString(Convert.ToDouble(txtDRQD.Text.ToString()) + Convert.ToDouble(DiscRateNonQualStep.Value));
                //txtDRQD.Text = txtDRQNP.Text;
            }
        }
        DisableFees();
    }

    protected void txtWirelessFee_TextChanged(object sender, EventArgs e)
    {
        try
        {
            /*
            if ((txtWirelessAccess.Text != "") && (txtWirelessTransFee.Text != ""))
            {
                //txtGWTransFee.Text = "0.00";
                //txtGWTransFee.Enabled = false;
                lstGatewayNames.Items.Remove("ROAMpay");
            }*/
        }
        catch (Exception err)
        {
            CreateLog Log = new CreateLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "Error Updating Wireless Fees - " + err.Message);
            DisplayMessage("Error Updating Wireless Fees.");
        }
    }
    //Fill Batch Header and NBCTransaction Fee when Transaction Fee and Internet Statement are changed
    protected void txtTransFee_TextChanged(object sender, EventArgs e)
    {
        try
        {
            //These Processors don't have a Batch Fee so set it to 0
            if ((lstProcessorNames.SelectedItem.Text.ToString().Trim().Contains("Sage")) ||
                lstProcessorNames.SelectedItem.Text.ToString().Trim().Contains("Optimal-Merrick") ||
                lstProcessorNames.SelectedItem.Text.ToString().Trim().Contains("Payvision-Merrick") || lstProcessorNames.SelectedItem.Text.ToString().Trim().Contains("Payvision-B+S"))
            {
                //txtBatchHeader.Text = "0.00";
                txtBatchHeader.Text = Convert.ToString(txtTransFee.Text).Trim();
                txtNBCTransFee.Text = txtTransFee.Text.ToString().Trim();
            }
            else
            {
                txtBatchHeader.Text = txtTransFee.Text.ToString().Trim();
                txtNBCTransFee.Text = txtTransFee.Text.ToString().Trim();
            }
        }
        catch (Exception err)
        {
            CreateLog Log = new CreateLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "Error Updating Transaction Fee - " + err.Message);
            DisplayMessage("Error Updating Transaction Fee.");
        }
    }

    //Fill Retreival Fee when Chargeback Fee is changed
    protected void txtChargebackFee_TextChanged(object sender, EventArgs e)
    {
        try
        {
            txtRetrievalFee.Text = txtChargebackFee.Text.ToString().Trim();
        }
        catch (Exception err)
        {
            CreateLog Log = new CreateLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "Error Updating Retrieval Fee - " + err.Message);
            DisplayMessage("Error Updating Retrieval Fee.");
        }
    }

    //checks the Processor selected and enables and disables Form Fields accordingly
    public void CheckProcessor()
    {

        OnlineAppProcessingBL Processing = new OnlineAppProcessingBL(AppId);
        PartnerDS.OnlineAppRatesDataTable dt = Processing.GetRates();

        //Enable or disable Interchange and Assessments chk box
        if (User.IsInRole("Admin") && lstProcessorNames.SelectedItem.Text.Contains("Intuit Payment Solutions (QuickBooks)"))
        {
            //chkApplyInterchange.Visible = true;
            chkBillAssessment.Visible = false;
        }
        else if (lstProcessorNames.SelectedItem.Text.Contains("Optimal-Canada")
            || lstProcessorNames.SelectedItem.Text.Contains("Optimal-Cal")
            || lstProcessorNames.SelectedItem.Text.Contains("Optimal-Barclays")
            || lstProcessorNames.SelectedItem.Text.Contains("IMS (QuickBooks)")
            || lstProcessorNames.SelectedItem.Text.Contains("Intuit Payment Solutions (QuickBooks)")
            || lstProcessorNames.SelectedItem.Text.Contains("WorldPay")
            || lstProcessorNames.SelectedItem.Text.Contains("Kitts"))
        {
            //chkApplyInterchange.Visible = false;
            chkBillAssessment.Visible = false;
        }
        else
        {
            //chkApplyInterchange.Visible = true;
            //chkBillAssessment.Visible = true;
            //lstRateNames.SelectedItem.Text.Contains("Interchange") = false;
            chkBillAssessment.Checked = false;
        }

        //Enable or disable Card Present option based on Processor
        if (lstProcessorNames.SelectedItem.Text.Contains("QuickBooks"))
        {
            rdbCP.Checked = true;
            rdbCNP.Checked = false;
            rdbCNP.Enabled = false;
            rdbCP.Enabled = false;
        }
        else if
        (lstProcessorNames.SelectedItem.Text.Contains("Optimal-Cal")
            || lstProcessorNames.SelectedItem.Text.Contains("Barclays")
            || (lstProcessorNames.SelectedItem.Text.Contains("WorldPay"))
            || lstProcessorNames.SelectedItem.Text.Contains("Optimal-Canada")
            || lstProcessorNames.SelectedItem.Text.Contains("Kitts")
            || lstProcessorNames.SelectedItem.Text.Contains("Payvision-B+S"))
        {
            rdbCP.Checked = false;
            rdbCNP.Checked = true;
            rdbCNP.Enabled = false;
            rdbCP.Enabled = false;
        }
        else
        {

            rdbCNP.Enabled = true;
            rdbCP.Enabled = true;

            if (dt[0].CardPresent.ToString().Trim() == "CP")
            {
                rdbCP.Checked = true;
                rdbCNP.Checked = false;
                //CardPresent = "CP";
                //pnlOnlineDebit.Visible = true;
                //pnlEBT.Visible = true;

            }
            if (dt[0].CardPresent.ToString().Trim() == "CNP")
            {
                rdbCNP.Checked = true;
                rdbCP.Checked = false;
                //pnlOnlineDebit.Visible = true;
                //CardPresent = "CNP";
                //pnlOnlineDebit.Visible = false;
                //pnlEBT.Visible = false;

            }

            if (dt[0].CardPresent.ToString().Trim() == "")
            {
                rdbCNP.Checked = true;
                rdbCP.Checked = false;
            }
        }

        NewAppInfo App = new NewAppInfo(AppId);
        int PID = App.ReturnPID();

        if (rdbCNP.Checked == true)
        {

            //Disable Other Services except Check Guarantee, disable rest
            pnlOnlineDebit.Visible = false;
            pnlEBT.Visible = false;
            //pnlGiftCard.Visible = false;
            //pnlLease.Visible = false;
            chkOnlineDebit.Checked = false;
            chkEBT.Checked = false;
            //chkGiftCard.Checked = false;
            //chkLease.Checked = false;
            //txtDebitMonFee.Text = "";
            //txtDebitTransFee.Text = "";
            //lstGCType.SelectedItem.Text = "";
            //txtGCMonFee.Text = "";
            //txtGCTransFee.Text = "";
            txtEBTMonFee.Text = "";
            txtEBTTransFee.Text = "";
            lstLeaseCompany.Text = "";
            txtLeasePayment.Text = "";
            lstLeaseTerm.Text = "";
        }
        else
        {

            pnlEBT.Visible = true;
            pnlGiftCard.Visible = true;
            pnlLease.Visible = true;
        }

        //txtGWMonthlyFee.Enabled = true;

        //If CNP account or Optimal International or Optimal Canada
        if (lstProcessorNames.SelectedItem.Text.Contains("Optimal-Canada")
            || lstProcessorNames.SelectedItem.Text.Contains("Optimal-Barclays")
            || lstProcessorNames.SelectedItem.Text.Contains("Optimal-Cal")
            || (lstProcessorNames.SelectedItem.Text.Contains("WorldPay"))
            || lstProcessorNames.SelectedItem.Text.Contains("Kitts")
            || lstProcessorNames.SelectedItem.Text.Contains("IMS (QuickBooks)")
            || lstProcessorNames.SelectedItem.Text.Contains("Payvision-B+S")
            || lstProcessorNames.SelectedItem.Text.Contains("Payvision-Merrick")
            || ((lstProcessorNames.SelectedItem.Text.Contains("Intuit Payment Solutions (QuickBooks)")) && (PID == 255))
            || lstPackageNames.SelectedItem.Text.Contains("GoPayment")
            //|| rdbCNP.Checked == true
            )
        {

            //Disable Other Services except Check Guarantee, disable rest
            pnlOnlineDebit.Visible = true;
            pnlEBT.Visible = true;
            chkEBT.Enabled = false;
            chkOnlineDebit.Enabled = false;
            //pnlGiftCard.Visible = false;
            //pnlLease.Visible = false;
            chkOnlineDebit.Checked = false;
            chkEBT.Checked = false;
            lstContractTerm.Enabled = false;
            //txtGWMonthlyFee.Enabled = false;

            //chkGiftCard.Checked = false;
            //chkLease.Checked = false;
            //txtDebitMonFee.Text = "";
            //txtDebitTransFee.Text = "";
            //lstGCType.SelectedItem.Text = "";
            //txtGCMonFee.Text = "";
            //txtGCTransFee.Text = "";
            txtEBTMonFee.Text = "";
            txtEBTTransFee.Text = "";
            lstLeaseCompany.Text = "";
            txtLeasePayment.Text = "";
            lstLeaseTerm.Text = "";
            pnlLease.Visible = false;
        }
        else
        {
            //txtGWMonthlyFee.Enabled = true;
            pnlOnlineDebit.Visible = true;
            pnlEBT.Visible = true;
            chkEBT.Enabled = true;
            chkOnlineDebit.Enabled = true;
            pnlGiftCard.Visible = false;
            pnlLease.Visible = true;
        }
        if (chkGiftCard.Checked)
        {
            if (lstProcessorNames.SelectedItem.Text.Contains("Intuit") || lstProcessorNames.SelectedItem.Text.Contains("IMS"))
                lstGCType.SelectedValue = "Sage EFT Gift & Loyalty";
            else
                lstGCType.SelectedValue = "Sage EFT Gift & Loyalty";
        }
        else
            lstGCType.SelectedValue = "";
    }

    public void PopulateGatewayList()
    {
        //Get Gateway list
        OnlineAppProcessingBL Processing = new OnlineAppProcessingBL(AppId);
        PartnerDS.OnlineAppRatesDataTable dt = Processing.GetRates();

        OnlineAppProcessingBL Gateways = new OnlineAppProcessingBL();
        //PartnerDS.OnlineAppRatesDataTable dt = Gateways.GetRates();
        //DataSet dsGateways = Gateways.GetGateways(lstProcessorNames.SelectedItem.Value);
        DataSet dsGateways = Gateways.GetGateways(lstProcessorNames.SelectedItem.Text.Trim());
        if (dsGateways.Tables[0].Rows.Count > 0)
        {
            lstGatewayNames.DataSource = dsGateways;
            lstGatewayNames.DataTextField = "Gateway";
            lstGatewayNames.DataValueField = "Gateway";
            lstGatewayNames.DataBind();
        }
        else
        {
            ListItem lstItem = new System.Web.UI.WebControls.ListItem();
            lstItem.Text = "Authorize.net";
            lstItem.Value = "Authorize.net";
            lstGatewayNames.Items.Add(lstItem);
            ListItem lstItem1 = new System.Web.UI.WebControls.ListItem();
            lstItem1.Text = "Plug'n Pay";
            lstItem1.Value = "Plug'n Pay";
            lstGatewayNames.Items.Add(lstItem1);
        }
        /*ListItem lstItem = new System.Web.UI.WebControls.ListItem();
        lstItem.Text = "None";
        lstItem.Value = "None";
        lstGatewayNames.Items.Add(lstItem);*/
        //lstGatewayNames.SelectedIndex = lstGatewayNames.Items.IndexOf(lstGatewayNames.Items.FindByText("None"));

        /*if (lstGatewayNames.SelectedItem.Text.Contains("Authorize"))
            chkECheck.Visible = true;
        else
            chkECheck.Visible = false;*/

        if (dt.Rows.Count > 0)
        {
            if (lstGatewayNames.Items.FindByValue(dt[0].Gateway.ToString()) == null)
            {
                ListItem gwyItem = new ListItem();
                gwyItem.Value = dt[0].Gateway.ToString();
                gwyItem.Text = dt[0].Gateway.ToString();
                lstGatewayNames.Items.Add(gwyItem);
            }

            if (lstGatewayNames.Items.FindByValue(dt[0].Gateway.ToString()) != null)
            {
                lstGatewayNames.SelectedValue = dt[0].Gateway.ToString();
                // lstGatewayNames.SelectedItem.Text = dt[0].Gateway.ToString();
            }
        }

        LoadUpperLowerBoundsGW(lstGatewayNames.SelectedItem.Text);
    }

    public void PopulateChkList()
    {
        ListItem lstItem = new System.Web.UI.WebControls.ListItem();
        lstItem.Text = " ";
        lstItem.Value = " ";
        lstCheckService.Items.Add(lstItem);
        ListItem lstItem1 = new System.Web.UI.WebControls.ListItem();
        lstItem1.Text = "CrossCheck";
        lstItem1.Value = "CrossCheck";
        lstCheckService.Items.Add(lstItem1);
        ListItem lstItem2 = new System.Web.UI.WebControls.ListItem();
        lstItem2.Text = "Direct Debit";
        lstItem2.Value = "Direct Debit";
        lstCheckService.Items.Add(lstItem2);
        ListItem lstItem3 = new System.Web.UI.WebControls.ListItem();
        lstItem3.Text = "eCheck.Net";
        lstItem3.Value = "eCheck.Net";
        lstCheckService.Items.Add(lstItem3);
        ListItem lstItem4 = new System.Web.UI.WebControls.ListItem();
        lstItem4.Text = "Sage EFT Check";
        lstItem4.Value = "Sage EFT Check";
        lstCheckService.Items.Add(lstItem4);
        ListItem lstItem5 = new System.Web.UI.WebControls.ListItem();
        lstItem5.Text = "Telecheck";
        lstItem5.Value = "Telecheck";
        lstCheckService.Items.Add(lstItem5);
    }

    protected void lstGatewayNames_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadDefaultRateGW(lstGatewayNames.SelectedItem.Text);
        /*
        if (lstGatewayNames.SelectedItem.Text.ToLower().Contains("roampay"))
        {
            txtWirelessAccess.Text = "";
            txtWirelessAccess.Enabled = false;
            txtWirelessTransFee.Text = "";
            txtWirelessTransFee.Enabled = false;
        }*/


        LoadUpperLowerBoundsGW(lstGatewayNames.SelectedItem.Text);
    }
    //ROBERT LOOK HERE!!!
    protected void lstRateNames_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (lstRateNames.SelectedItem.Text.Contains("Interchange"))
            {

                LoadInterchangeBounds();


                chkBillAssessment.Checked = true;
                if (rdbCP.Checked)
                {
                    txtDRMQ.Text = txtDRQP.Text.Trim();
                    txtDRNQ.Text = txtDRQP.Text.Trim();
                    txtMidQualDebit.Text = txtDRQD.Text.Trim();
                    txtNonQualDebit.Text = txtDRQD.Text.Trim();
                }
                else if (rdbCNP.Checked)
                {
                    txtDRMQ.Text = txtDRQP.Text.Trim();
                    txtDRNQ.Text = txtDRQP.Text.Trim();
                    txtMidQualDebit.Text = txtDRQD.Text.Trim();
                    txtNonQualDebit.Text = txtDRQD.Text.Trim();
                }
                txtAmexMidQual.Text = txtAmexQual.Text.Trim();
                txtAmexNonQual.Text = txtAmexQual.Text.Trim();
                txtAmexMidQual.Enabled = false;
                txtAmexNonQual.Enabled = false;
                txtDRMQ.Enabled = false;
                txtDRNQ.Enabled = false;
                txtMidQualDebit.Enabled = false;
                txtNonQualDebit.Enabled = false;
            }
            else if (lstRateNames.SelectedItem.Text.Contains("Tiered"))
            {
                LoadTieredBounds();

                chkBillAssessment.Checked = false;
                string strCardPresent = "CP";
                if (rdbCNP.Checked)
                    strCardPresent = "CNP";
                txtDRMQ.Enabled = true;
                txtDRNQ.Enabled = true;
                txtMidQualDebit.Enabled = true;
                txtNonQualDebit.Enabled = true;
                LoadUpperLowerBounds(lstProcessorNames.SelectedItem.Text, strCardPresent);
                LoadAnnualFees();
            }
            else if (lstRateNames.SelectedItem.Text.Contains("Flat Rate"))
            {

                LoadFlatRateBounds();

                chkBillAssessment.Checked = true;
                if (rdbCP.Checked)
                {
                    txtDRMQ.Text = txtDRQP.Text.Trim();
                    txtDRNQ.Text = txtDRQP.Text.Trim();
                    txtMidQualDebit.Text = txtDRQD.Text.Trim();
                    txtNonQualDebit.Text = txtDRQD.Text.Trim();
                }
                else if (rdbCNP.Checked)
                {
                    txtDRMQ.Text = txtDRQP.Text.Trim();
                    txtDRNQ.Text = txtDRQP.Text.Trim();
                    txtMidQualDebit.Text = txtDRQD.Text.Trim();
                    txtNonQualDebit.Text = txtDRQD.Text.Trim();
                }
                txtAmexMidQual.Text = txtAmexQual.Text.Trim();
                txtAmexNonQual.Text = txtAmexQual.Text.Trim();
            }
        }//end try
        catch (Exception err)
        {
            CreateLog Log = new CreateLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "Apply Interchange Rates - " + err.Message);
            DisplayMessage("Error loading data.");
        }
    }

    protected void lstCheckService_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadDefaultRateCS(lstCheckService.SelectedItem.Text.ToString().Trim());
    }

    protected void lstGiftCard_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadDefaultRateGiftCard(lstGCType.SelectedItem.Text.ToString().Trim());
    }

    public void PopulateGiftCardRates()
    {
        //Get rates
        BoundsBL Rates = new BoundsBL();
        DataSet ds = Rates.GiftCardBoundsbyID(Convert.ToInt32(lstGCType.SelectedItem.Value));
        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr = ds.Tables[0].Rows[0];
            //Gift Card Rates
            txtGCMonFee.Text = dr["GiftCardMonFeeDef"].ToString().Trim();
            txtGCTransFee.Text = dr["GiftCardTransFeeDef"].ToString().Trim();
        }//end if count not 0
    }//end function Populate

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Edit.aspx?AppId=" + AppId);
    }

    public void PopulatePlatformList()
    {
        //Get Platform list
        OnlineAppStatusBL Platforms = new OnlineAppStatusBL();
        DataSet ds = Platforms.GetPlatforms(lstProcessorNames.SelectedItem.Text.Trim());
        if (ds.Tables[0].Rows.Count > 0)
        {
            lstPlatform.DataSource = ds;
            lstPlatform.DataTextField = "Platform";
            lstPlatform.DataValueField = "Platform";
            lstPlatform.DataBind();
        }
        ListItem lstItem = new System.Web.UI.WebControls.ListItem();
        lstItem.Text = "";
        lstItem.Value = "";
        lstPlatform.Items.Add(lstItem);
        if (Convert.ToString(lstProcessorNames.SelectedItem.Text.Trim()).ToLower().Contains("Sage"))
        {
            lstPlatform.SelectedValue = "TSYS/Vital";
        }

        if (Convert.ToString(lstProcessorNames.SelectedItem.Text.Trim()).ToLower().Contains("ipayment"))
        {
            lstPlatform.SelectedValue = "First Data Omaha";
        }
        //lstPlatform.SelectedIndex = lstPlatform.Items.IndexOf(lstPlatform.Items.FindByText("None"));
    }

    protected void rdbCP_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            ReloadBounds();
            if (rdbCP.Checked)
            {
                pnlGiftCard.Visible = true;
                pnlOnlineDebit.Visible = true;
                pnlEBT.Visible = true;
                pnlLease.Visible = true;
                chkOnlineDebit.Enabled = true;
                chkEBT.Enabled = true;
                txtQualNPDebit.Text = "";
                txtDRQNP.Text = "";


                //Get CardPCT swiped and processingPCT and compare
                OnlineAppProcessingBL Processing = new OnlineAppProcessingBL(AppId);
                string iPctSwiped = Processing.ReturnSwipedPercent();
                int iCPPctSwipedLow = Processing.GetProcessingPCT(lstProcessorNames.SelectedItem.Text);
                if (iPctSwiped != "")
                {
                    if ((Convert.ToInt32(iPctSwiped) < iCPPctSwipedLow) || (Convert.ToInt32(iPctSwiped) == 0))
                    //Swiped Percentage not high enough to be a Card Present Account
                    {
                        DisplayMessage("Processing Pct Swiped on this App (" + iPctSwiped + "%) must be at least " + iCPPctSwipedLow + "% for this Processor on a Card Present account.");
                        rdbCNP.Checked = true;
                        rdbCP.Checked = false;
                        //pnlGiftCard.Visible = false;
                        pnlOnlineDebit.Visible = true;
                        pnlEBT.Visible = true;
                        chkOnlineDebit.Enabled = false;
                        chkEBT.Enabled = false;
                        //pnlLease.Visible = false;
                    }
                    else
                    {
                        //ReloadBounds();
                        //dimFields();
                        if (lstRateNames.SelectedItem.Text.Contains("Interchange"))
                            LoadInterchangeBounds();
                    }
                }//end if swiped is not null
                else
                {
                    //ReloadBounds();
                    //dimFields();
                    if (lstRateNames.SelectedItem.Text.Contains("Interchange"))
                        LoadInterchangeBounds();
                }
            }//end if cpchecked        
            else
            {
                //pnlGiftCard.Visible = false;
                pnlOnlineDebit.Visible = true;
                pnlEBT.Visible = true;
                chkEBT.Enabled = false;
                chkOnlineDebit.Enabled = false;
                chkEBT.Checked = false;
                chkOnlineDebit.Checked = false;
                txtPinDebitDiscount.Enabled = false;
                txtDebitMonFee.Enabled = false;

                txtDRQD.Text = "";
                txtDRQP.Text = "";

                if (lstProcessorNames.SelectedItem.Text.ToLower().Contains("Sage"))
                {

                    lstDebitTransFee.Enabled = false;
                }
                else
                {
                    txtDebitTransFee.Enabled = false;
                }
                //lstDebitTransFee.Enabled = false;
                txtPinDebitDiscount.Text = "";
                txtDebitMonFee.Text = "";
                lstDebitTransFee.SelectedItem.Text = "";
                txtDebitTransFee.Text = "";
                //pnlLease.Visible = false;
                //ReloadBounds();
                //dimFields();
                if (lstRateNames.SelectedItem.Text.Contains("Interchange"))
                    LoadInterchangeBounds();
            }
        }//end try
        catch (Exception err)
        {
            CreateLog Log = new CreateLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), err.Message);
            DisplayMessage("Error loading data.");
        }
    }//end CPCNP check changed

    /*protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Write("<script language='JavaScript'> { self.close() }</script>");
    }*/


    public void LoadFlatRateBounds()
    {
        if (lstRateNames.SelectedItem.Text.Contains("Flat Rate"))
        {
            DiscRateQualDebitLow.Value = "0.00";
            DiscRateQualPresLow.Value = "0.00";
            DiscRateQualNPLow.Value = "";
            DiscRateQualPresLow.Value = "0.00";
            DebitQualNPLow.Value = "";
            AmexDiscRateQualLow.Value = "0.00";

        }
        else
        {
            DiscRateQualDebitLow.Value = "";
            DiscRateQualPresLow.Value = "";
            DiscRateQualNPLow.Value = "0.00";
            DiscRateQualPresLow.Value = "";
            DebitQualNPLow.Value = "0.00";
            AmexDiscRateQualLow.Value = "0.00";
        }
        if (lstProcessorNames.SelectedItem.Text.Equals("IMS"))
        {
            DiscRateMidQualLow.Value = "";
            DebitMidQualLow.Value = "";
            AmexDiscRateMidQualLow.Value = "";
            AmexDiscRateNonQualLow.Value = "";
        }
        else
        {
            DiscRateMidQualLow.Value = "0.00";
            DebitMidQualLow.Value = "0.00";
            AmexDiscRateMidQualLow.Value = "0.00";
            AmexDiscRateNonQualLow.Value = "0.00";
        }
        if (lstProcessorNames.SelectedItem.Text.Equals("Intuit Payment Solutions"))
        {
            DiscRateMidQualLow.Value = "";
            DebitMidQualLow.Value = "";
            AmexDiscRateMidQualLow.Value = "";
            AmexDiscRateNonQualLow.Value = "";
        }
        else
        {
            DiscRateMidQualLow.Value = "0.00";
            DebitMidQualLow.Value = "0.00";
            AmexDiscRateMidQualLow.Value = "0.00";
            AmexDiscRateNonQualLow.Value = "0.00";
        }
        DiscRateNonQualLow.Value = "0.00";
        DebitMidQualLow.Value = "0.00";
        DebitNonQualLow.Value = "0.00";
        AmexDiscRateMidQualLow.Value = "0.00";
        AmexDiscRateNonQualLow.Value = "0.00";
    }

    public void LoadInterchangeBounds()
    {
        //TransFeeLow.Value = "0.00";
        if (rdbCP.Checked)
        {
            DiscRateQualDebitLow.Value = "0.00";
            DiscRateQualPresLow.Value = "0.00";
            DiscRateQualNPLow.Value = ""; //this causes the field to be disabled in DisableFees()
            DiscRateQualPresLow.Value = "0.00";
            DebitQualNPLow.Value = ""; //this causes the field to be disabled in DisableFees()
            AmexDiscRateQualLow.Value = "0.00";
        }
        else
        {
            DiscRateQualDebitLow.Value = "";
            DiscRateQualPresLow.Value = ""; //this causes the field to be disabled in DisableFees()
            DiscRateQualNPLow.Value = "0.00";
            DiscRateQualPresLow.Value = ""; //this causes the field to be disabled in DisableFees()
            DebitQualNPLow.Value = "0.00";
            AmexDiscRateQualLow.Value = "0.00";
        }

        if (lstProcessorNames.SelectedItem.Text.Equals("IMS"))
        {
            DiscRateMidQualLow.Value = "";
            DebitMidQualLow.Value = "";
            AmexDiscRateMidQualLow.Value = "";
            AmexDiscRateNonQualLow.Value = "";
        }
        else
        {
            DiscRateMidQualLow.Value = "0.00";
            DebitMidQualLow.Value = "0.00";
            AmexDiscRateMidQualLow.Value = "0.00";
            AmexDiscRateNonQualLow.Value = "0.00";
        }

        if (lstProcessorNames.SelectedItem.Text.Equals("Intuit Payment Solutions"))
        {
            DiscRateMidQualLow.Value = "";
            DebitMidQualLow.Value = "";
            AmexDiscRateMidQualLow.Value = "";
            AmexDiscRateNonQualLow.Value = "";
        }
        else
        {
            DiscRateMidQualLow.Value = "0.00";
            DebitMidQualLow.Value = "0.00";
            AmexDiscRateMidQualLow.Value = "0.00";
            AmexDiscRateNonQualLow.Value = "0.00";
        }


        DiscRateNonQualLow.Value = "0.00";
        DebitMidQualLow.Value = "0.00";
        DebitNonQualLow.Value = "0.00";
        AmexDiscRateMidQualLow.Value = "0.00";
        AmexDiscRateNonQualLow.Value = "0.00";


    }

    public void LoadTieredBounds()
    {

        pnlGiftCard.Visible = true;
        //Get Package list
        //This is the code that I am going to be writing 
        PackageBL RepPackages = new PackageBL();
        DataSet dsPackages;

        //Rate Packages shown should include Sales Rep, User logged in and ALL.
        dsPackages = RepPackages.GetPackagesForRep(Convert.ToString(Session["MasterNum"]));

        if (dsPackages.Tables[0].Rows.Count > 0)
        {
            lstPackageNames.DataSource = dsPackages;
            lstPackageNames.DataTextField = "PackageName";
            lstPackageNames.DataValueField = "PackageID";
            lstPackageNames.DataBind();
        }

        //Get CardPCT swiped
        OnlineAppProcessingBL Processing = new OnlineAppProcessingBL(AppId);
        string iPctSwiped = Processing.ReturnSwipedPercent();

        //Get Processor list. The list retrieved will depend on the Card PCT swiped
        DataSet dsProcessors = Processing.GetProcessorNames(iPctSwiped);
        if (dsProcessors.Tables[0].Rows.Count > 0)
        {
            lstProcessorNames.DataSource = dsProcessors;
            lstProcessorNames.DataTextField = "Processor";
            lstProcessorNames.DataValueField = "Processor";
            lstProcessorNames.DataBind();
        }

        if (lstRateNames.SelectedItem.Text.Contains("Interchange"))
        {
            txtDRMQ.Enabled = false;
            txtDRNQ.Enabled = false;
        }

        //Get rates based on AppId from OnlineAppProcessingBL
        PartnerDS.OnlineAppRatesDataTable dt = Processing.GetRates();

        string strComplianceFee = "";

        OnlineAppBL Compliance = new OnlineAppBL(AppId);

        strComplianceFee = Compliance.GetComplianceFee();

        if (dt.Rows.Count > 0)
        {
            Style AppIDLabel = new Style();
            AppIDLabel.ForeColor = System.Drawing.Color.White;
            AppIDLabel.Font.Size = FontUnit.Small;
            AppIDLabel.Font.Bold = true;

            lblContact.Text = "(" + dt[0].FirstName.ToString().Trim() + " " + dt[0].LastName.ToString().Trim() + ")";
            lblAppId.Text = AppId.ToString();
            lblAppId.ApplyStyle(AppIDLabel);
            lblContact.ApplyStyle(AppIDLabel);

            lblLastModifiedDate.Text = dt[0].LastModified.ToString();

            AcctType = Convert.ToInt32(dt[0].AcctType);




            //if Rate Package does not exist in the dropdown list, add it
            if (lstPackageNames.Items.FindByValue(dt[0].PackageID.ToString()) == null)
            {
                ListItem item = new ListItem();
                item.Value = dt[0].PackageID.ToString();
                item.Text = dt[0].PackageName.ToString();
                lstPackageNames.Items.Add(item);
            }

            if (lstPackageNames.Items.FindByValue(dt[0].PackageID.ToString()) != null)
                lstPackageNames.SelectedValue = dt[0].PackageID.ToString();

            //if Processor does not exist in the dropdown list, add it
            if (lstProcessorNames.Items.FindByValue(dt[0].Processor.ToString()) == null)
            {
                ListItem item = new ListItem();
                item.Value = dt[0].Processor.ToString();
                item.Text = dt[0].Processor.ToString();
                lstProcessorNames.Items.Add(item);
            }

            if (lstProcessorNames.Items.FindByValue(dt[0].Processor.ToString()) != null)
                lstProcessorNames.SelectedValue = dt[0].Processor.ToString();

            if (dt[0].Processor.ToString().Contains("Sage"))
            {
                txtDebitTransFee.Visible = false;
                lstDebitTransFee.Visible = true;
                txtAVS.Visible = false;
                lstAVS.Visible = true;
            }
            else
            {
                txtDebitTransFee.Visible = true;
                lstDebitTransFee.Visible = false;
                txtAVS.Visible = true;
                lstAVS.Visible = false;
            }

            if (dt[0].Processor.ToString().Contains("Sage"))
            {
                txtAVS.Enabled = false;
                txtAVS.Visible = false;

                lstAVS.Enabled = true;
                lstAVS.Visible = true;
                lstAVS.SelectedItem.Text = "0.00";
                //txtRetrievalFee.Attributes.Add("readonly", "readonly");
                //txtRetrievalFee.Text = "0.00";
                txtInternetStmt.Enabled = false;
                txtInternetStmt.Text = "0.00";
                txtBatchHeader.Enabled = false;
                if (Convert.ToString(txtTransFee.Text) != "")
                {
                    txtBatchHeader.Text = Convert.ToString(txtTransFee.Text);
                }
                else { txtBatchHeader.Text = "0.00"; }

                txtComplianceFee.Enabled = false;
                txtComplianceFee.Text = "0.00";
                /*
                if ((txtWirelessAccess.Text != "") && (txtWirelessTransFee.Text != ""))
                {
                    //txtGWTransFee.Text = "0.00";
                    //txtGWTransFee.Enabled = false;
                    lstGatewayNames.Items.Remove("ROAMpay");
                }
                else if (lstGatewayNames.SelectedItem.Text.ToLower().Contains("roampay"))
                {
                    txtWirelessAccess.Text = "";
                    txtWirelessAccess.Enabled = false;
                    txtWirelessTransFee.Text = "";
                    txtWirelessTransFee.Enabled = false;
                }*/
            }

            //Check for CP OR CNP and display addl services
            if (dt[0].CardPresent.ToString().Trim() == "CP")
            {
                rdbCP.Checked = true;
                rdbCNP.Checked = false;
                CardPresent = "CP";
                pnlOnlineDebit.Visible = true;
                pnlEBT.Visible = true;
                chkOnlineDebit.Enabled = true;
                chkEBT.Enabled = true;
            }

            if (dt[0].CardPresent.ToString().Trim() == "CNP")
            {
                rdbCNP.Checked = true;
                rdbCP.Checked = false;
                //pnlOnlineDebit.Visible = true;
                CardPresent = "CNP";
                pnlOnlineDebit.Visible = true;
                pnlEBT.Visible = true;
                chkOnlineDebit.Enabled = false;
                chkOnlineDebit.Checked = false;
                chkEBT.Enabled = false;
            }


            PopulateGatewayList();

            //PopulateChkList();

            //if (lstGatewayNames.Items.FindByValue(dt[0].Gateway.ToString()) != null)
            //lstGatewayNames.SelectedValue = dt[0].Gateway.ToString();
            if (lstGatewayNames.Items.FindByValue(dt[0].Gateway.ToString()) == null)
            {
                ListItem gwyItem = new ListItem();
                gwyItem.Value = dt[0].Gateway.ToString();
                gwyItem.Text = dt[0].Gateway.ToString();
                lstGatewayNames.Items.Add(gwyItem);
            }

            if (lstGatewayNames.Items.FindByValue(dt[0].Gateway.ToString()) != null)
                lstGatewayNames.SelectedValue = dt[0].Gateway.ToString();

            CheckProcessor();//Checks the Processor and enable or disable fields accordingly




            //Merchant Account Rates
            CardPresent = Server.HtmlEncode(dt[0].CardPresent.ToString());
            txtCustomerService.Text = Server.HtmlEncode(dt[0].CustServFee.ToString());
            txtInternetStmt.Text = Server.HtmlEncode(dt[0].InternetStmt.ToString());
            txtMonMin.Text = Server.HtmlEncode(dt[0].MonMin.ToString());
            txtTransFee.Text = Server.HtmlEncode(dt[0].TransactionFee.ToString());
            txtWirelessAccess.Text = Server.HtmlEncode(dt[0].WirelessAccessFee.ToString());
            txtWirelessTransFee.Text = Server.HtmlEncode(dt[0].WirelessTransFee.ToString());
            txtDRQP.Text = Server.HtmlEncode(dt[0].DiscRateQualPres.ToString());
            //txtDRQNP.Text = Server.HtmlEncode(dt[0].DiscRateQualNP.ToString());
            txtDRMQ.Text = Server.HtmlEncode(dt[0].DiscRateMidQual.ToString());
            txtDRNQ.Text = Server.HtmlEncode(dt[0].DiscRateNonQual.ToString());
            txtDRQD.Text = Server.HtmlEncode(dt[0].DiscRateQualDebit.ToString());

            //txtQualNPDebit.Text = Server.HtmlEncode(dt[0].DebitQualNP.ToString());
            txtMidQualDebit.Text = Server.HtmlEncode(dt[0].DebitMidQual.ToString());
            txtNonQualDebit.Text = Server.HtmlEncode(dt[0].DebitNonQual.ToString());

            txtAmexQual.Text = Server.HtmlEncode(dt[0].AmexDiscRateQual.ToString());
            txtAmexMidQual.Text = Server.HtmlEncode(dt[0].AmexDiscRateMidQual.ToString());
            txtAmexNonQual.Text = Server.HtmlEncode(dt[0].AmexDiscRateNonQual.ToString());
            txtChargebackFee.Text = Server.HtmlEncode(dt[0].ChargebackFee.ToString());
            txtRolling.Text = Server.HtmlEncode(dt[0].RollingReserve.ToString());
            txtRetrievalFee.Text = Server.HtmlEncode(dt[0].RetrievalFee.ToString());
            txtVoiceAuth.Text = Server.HtmlEncode(dt[0].VoiceAuth.ToString());
            txtBatchHeader.Text = Server.HtmlEncode(dt[0].BatchHeader.ToString());
            txtAVS.Text = Server.HtmlEncode(dt[0].AVS.ToString());
            lstAVS.SelectedItem.Text = Server.HtmlEncode(dt[0].AVS.ToString());
            txtNBCTransFee.Text = Server.HtmlEncode(dt[0].NBCTransFee.ToString());
            txtApplicationFee.Text = Server.HtmlEncode(dt[0].AppFee.ToString());
            txtSetupFee.Text = Server.HtmlEncode(dt[0].AppSetupFee.ToString());
            //Gateway Rates
            txtGWSetupFee.Text = Server.HtmlEncode(dt[0].GatewaySetupFee.ToString());
            txtGWMonthlyFee.Text = Server.HtmlEncode(dt[0].GatewayMonFee.ToString());
            txtGWTransFee.Text = Server.HtmlEncode(dt[0].GatewayTransFee.ToString());

            lstDiscountPaid.SelectedIndex = lstDiscountPaid.Items.IndexOf(lstDiscountPaid.Items.FindByText(dt[0].DiscountPaid.ToString()));
            lstContractTerm.SelectedIndex = lstContractTerm.Items.IndexOf(lstContractTerm.Items.FindByText(Convert.ToString(dt[0].ContractTerm).Trim()));


        }//end if count not 0
    }//end function LoadTieredBounds

    #region ValidateRates
    public string ValidateRates()
    {
        string strError = "";
        if ((AcctType == 1) || (AcctType == 4))
        {

            //Customer Service
            if (CustServLow.Value != "")
            {
                if ((txtCustomerService.Text == "") || (Convert.ToDecimal(txtCustomerService.Text) < Convert.ToDecimal(CustServLow.Value)))
                    strError += "Enter at least $" + CustServLow.Value + " for Customer Service Fee." + System.Environment.NewLine;
            }

            //Internet Statement can be BLANK or at or above the Minimum
            if ((InternetStmtLow.Value != "") && (txtInternetStmt.Text != ""))
            {
                if (Convert.ToDecimal(txtInternetStmt.Text) < Convert.ToDecimal(InternetStmtLow.Value))
                    strError += "Enter at least $" + InternetStmtLow.Value + " for Internet Statement Fee or leave blank if it does not apply." + System.Environment.NewLine;
            }

            //Monthly Minimum
            if (MonMinLow.Value != "")
            {
                if ((txtMonMin.Text == "") || (Convert.ToDecimal(txtMonMin.Text) < Convert.ToDecimal(MonMinLow.Value)))
                    strError += "Enter at least $" + MonMinLow.Value + " for Monthly Minimum." + System.Environment.NewLine;
            }
            //Transaction Fee
            if (TransFeeLow.Value != "")
            {
                if ((txtTransFee.Text == "") || (Convert.ToDecimal(txtTransFee.Text) < Convert.ToDecimal(TransFeeLow.Value)))
                    strError += "Enter at least $" + TransFeeLow.Value + " for Transaction Fee." + System.Environment.NewLine;
            }
            //DiscRateQualPres
            if (DiscRateQualPresLow.Value != "")
            {
                if ((txtDRQP.Text == "") || (Convert.ToDecimal(txtDRQP.Text) < Convert.ToDecimal(DiscRateQualPresLow.Value)))
                {
                    strError += "Enter at least " + DiscRateQualPresLow.Value + "% for Disc Rate Qual Pres." + System.Environment.NewLine;
                }
            }
            //DiscRateQualNP
            /*
            if (DiscRateQualNPLow.Value != "")
            {
                if ((txtDRQNP.Text == "") || (Convert.ToDecimal(txtDRQNP.Text) < Convert.ToDecimal(DiscRateQualNPLow.Value)))
                    strError += "Enter at least " + DiscRateQualNPLow.Value + "% for Disc Rate Qual NP." + System.Environment.NewLine;
            }
            */


            //Debit DiscRateQualNP
            /*
            if (DebitQualNPLow.Value != "")
            {
                if ((txtQualNPDebit.Text == "") || (Convert.ToDecimal(txtQualNPDebit.Text) < Convert.ToDecimal(DebitQualNPLow.Value)))
                    strError += "Enter at least " + DebitQualNPLow.Value + "% for Debit Disc Rate Qual NP." + System.Environment.NewLine;
            }*/


            //Debit DiscRateMidQual
            if (lstRateNames.SelectedItem.Text.Contains("Tiered") && DebitMidQualLow.Value != "")
            {
                if ((txtMidQualDebit.Text == "") || (Convert.ToDecimal(txtMidQualDebit.Text) < Convert.ToDecimal(DebitMidQualLow.Value)))
                    strError += "Enter at least " + DebitMidQualLow.Value + "% for Debit Disc Rate Mid Qual." + System.Environment.NewLine;
            }

            //Debit DiscRateNonQual
            if (lstRateNames.SelectedItem.Text.Contains("Tiered") && DebitNonQualLow.Value != "")
            {
                if ((txtNonQualDebit.Text == "") || (Convert.ToDecimal(txtNonQualDebit.Text) < Convert.ToDecimal(DebitNonQualLow.Value)))
                    strError += "Enter at least " + DebitNonQualLow.Value + "% for Debit Disc Rate Non Qual." + System.Environment.NewLine;
            }


            //Amex DiscRateQualNP
            if (lstRateNames.SelectedItem.Text.Contains("Tiered") && AmexDiscRateQualLow.Value != "")
            {
                if ((txtAmexQual.Text == "") || (Convert.ToDecimal(txtAmexQual.Text) < Convert.ToDecimal(AmexDiscRateQualLow.Value)))
                    strError += "Enter at least " + AmexDiscRateQualLow.Value + "% for American Express Disc Rate Qual." + System.Environment.NewLine;
            }

            //Amex DiscRateMidQual
            if (lstRateNames.SelectedItem.Text.Contains("Tiered") && AmexDiscRateMidQualLow.Value != "")
            {
                if ((txtAmexMidQual.Text == "") || (Convert.ToDecimal(txtAmexMidQual.Text) < Convert.ToDecimal(AmexDiscRateMidQualLow.Value)))
                    strError += "Enter at least " + AmexDiscRateMidQualLow.Value + "% for American Express Disc Rate Mid Qual." + System.Environment.NewLine;
            }

            //Amex DiscRateNonQual
            if (lstRateNames.SelectedItem.Text.Contains("Tiered") && AmexDiscRateNonQualLow.Value != "")
            {
                if ((txtAmexNonQual.Text == "") || (Convert.ToDecimal(txtAmexNonQual.Text) < Convert.ToDecimal(AmexDiscRateNonQualLow.Value)))
                    strError += "Enter at least " + AmexDiscRateNonQualLow.Value + "% for American Express Disc Rate Non Qual." + System.Environment.NewLine;
            }


            //If IMS/IPS and Card Present overwrite the Mid Qual Low to be the Qual Pres PLUS the specified Mid Qual Step in the database 
            if ((CardPresent == "CP") && (lstProcessorNames.SelectedItem.Text == "IMS"))
            {
                if ((txtDRQP.Text != "") && (DiscRateMidQualStep.Value != ""))
                    DiscRateMidQualLow.Value = Convert.ToString(Convert.ToDecimal(txtDRQP.Text) + Convert.ToDecimal(DiscRateMidQualStep.Value));
            }

            if ((CardPresent == "CP") && (lstProcessorNames.SelectedItem.Text == "Intuit Payment Solutions"))
            {
                if ((txtDRQP.Text != "") && (DiscRateMidQualStep.Value != ""))
                    DiscRateMidQualLow.Value = Convert.ToString(Convert.ToDecimal(txtDRQP.Text) + Convert.ToDecimal(DiscRateMidQualStep.Value));
            }
            //DiscRateMidQual
            if (lstRateNames.SelectedItem.Text.Contains("Tiered") && DiscRateMidQualLow.Value != "")
            {
                if ((txtDRMQ.Text == "") || (Convert.ToDecimal(txtDRMQ.Text) < Convert.ToDecimal(DiscRateMidQualLow.Value)))
                    strError += "Enter at least " + DiscRateMidQualLow.Value + "% for Disc Rate Mid Qual." + System.Environment.NewLine;
            }
            //DiscRateNonQual
            if (lstRateNames.SelectedItem.Text.Contains("Tiered") && DiscRateNonQualLow.Value != "")
            {
                if ((txtDRNQ.Text == "") || (Convert.ToDecimal(txtDRNQ.Text) < Convert.ToDecimal(DiscRateNonQualLow.Value)))
                    strError += "Enter at least " + DiscRateNonQualLow.Value + "% for Disc Rate Non Qual." + System.Environment.NewLine;
            }
            //DiscRateQualDebit        
            if (DiscRateQualDebitLow.Value != "")
            {
                if ((txtDRQD.Text == "") || (Convert.ToDecimal(txtDRQD.Text) < Convert.ToDecimal(DiscRateQualDebitLow.Value)))
                {
                    strError += "Enter at least " + DiscRateQualDebitLow.Value + "% for Disc Rate Qual Debit." + System.Environment.NewLine;
                }
            }

            //DiscRateQualAmex       
            if (AmexDiscRateQualLow.Value != "")
            {
                if ((txtAmexQual.Text == "") || (Convert.ToDecimal(txtAmexQual.Text) < Convert.ToDecimal(AmexDiscRateQualLow.Value)))
                {
                    strError += "Enter at least " + AmexDiscRateQualLow.Value + "% for Disc Rate American Express." + System.Environment.NewLine;
                }
            }

            //DiscRateMidQualAmex       
            if (AmexDiscRateMidQualLow.Value != "")
            {
                if ((txtAmexMidQual.Text == "") || (Convert.ToDecimal(txtAmexMidQual.Text) < Convert.ToDecimal(AmexDiscRateMidQualLow.Value)))
                {
                    strError += "Enter at least " + AmexDiscRateMidQualLow.Value + "% for Disc Rate American Express." + System.Environment.NewLine;
                }
            }

            //DiscRateNonQualAmex       
            if (AmexDiscRateNonQualLow.Value != "")
            {
                if ((txtAmexNonQual.Text == "") || (Convert.ToDecimal(txtAmexNonQual.Text) < Convert.ToDecimal(AmexDiscRateNonQualLow.Value)))
                {
                    strError += "Enter at least " + AmexDiscRateNonQualLow.Value + "% for Disc Rate American Express." + System.Environment.NewLine;
                }
            }

            //ChargebackFee
            if (ChargebackFeeLow.Value != "")
            {
                if ((txtChargebackFee.Text == "") || (Convert.ToDecimal(txtChargebackFee.Text) < Convert.ToDecimal(ChargebackFeeLow.Value)))
                {
                    strError += "Enter at least $" + ChargebackFeeLow.Value + " for Chargeback Fee." + System.Environment.NewLine;
                }
            }
            //RetrievalFee
            if (RetrievalFeeLow.Value != "")
            {
                if ((txtRetrievalFee.Text == "") || (Convert.ToDecimal(txtRetrievalFee.Text) < Convert.ToDecimal(RetrievalFeeLow.Value)))
                    strError += "Enter at least $" + RetrievalFeeLow.Value + " for Retrieval Fee." + System.Environment.NewLine;
            }
            //VoiceAuth
            if (VoiceAuthLow.Value != "")
            {
                if ((txtVoiceAuth.Text == "") || (Convert.ToDecimal(txtVoiceAuth.Text) < Convert.ToDecimal(VoiceAuthLow.Value)))
                    strError += "Enter at least $" + VoiceAuthLow.Value + " for Voice Authorization Fee." + System.Environment.NewLine;
            }
            //BatchHeader
            if (BatchHeaderLow.Value != "")
            {
                if ((txtBatchHeader.Text == "") || (Convert.ToDecimal(txtBatchHeader.Text) < Convert.ToDecimal(BatchHeaderLow.Value)))
                    strError += "Enter at least $" + BatchHeaderLow.Value + " for Batch Header Fee." + System.Environment.NewLine;
            }
            //AVS
            if (AVSLow.Value != "")
            {
                OnlineAppProcessingBL Processing = new OnlineAppProcessingBL(AppId);
                PartnerDS.OnlineAppRatesDataTable dt = Processing.GetRates();



                if (dt.Rows.Count > 0)
                {
                    if (lstProcessorNames.SelectedItem.Text.ToLower().Contains("Sage"))
                    {
                        if ((lstAVS.SelectedItem.Text == "") || (Convert.ToDecimal(lstAVS.SelectedItem.Text) < Convert.ToDecimal(AVSLow.Value)))
                        {
                            strError += "Enter at least $" + AVSLow.Value + " for AVS Fee." + System.Environment.NewLine;
                        }
                    }
                    else if ((txtAVS.Text == "") || (Convert.ToDecimal(txtAVS.Text) < Convert.ToDecimal(AVSLow.Value)))
                        strError += "Enter at least $" + AVSLow.Value + " for AVS Fee." + System.Environment.NewLine;
                }
            }
            //WirelessAccessFee can be BLANK or at or above the Minimum
            if ((WirelessAccessFeeLow.Value != "") && (txtWirelessAccess.Text != ""))
            {
                if ((Convert.ToDecimal(txtWirelessAccess.Text) < Convert.ToDecimal(WirelessAccessFeeLow.Value)))
                    strError += "Enter at least $" + WirelessAccessFeeLow.Value + " for Wireless Access Fee or leave blank if it does not apply." + System.Environment.NewLine;
            }
            //WirelessTransFee can be BLANK or at or above the Minimum
            if ((WirelessTransFeeLow.Value != "") && (txtWirelessTransFee.Text != ""))
            {
                if ((Convert.ToDecimal(txtWirelessTransFee.Text) < Convert.ToDecimal(WirelessTransFeeLow.Value)))
                    strError += "Enter at least $" + WirelessTransFeeLow.Value + " for Wireless Trans Fee or leave blank if it does not apply." + System.Environment.NewLine;
            }
            //NBCTransFee
            if (NBCTransFeeLow.Value != "")
            {
                if ((txtNBCTransFee.Text == "") || (Convert.ToDecimal(txtNBCTransFee.Text) < Convert.ToDecimal(NBCTransFeeLow.Value)))
                {
                    strError += "Enter at least $" + NBCTransFeeLow.Value + " for Non-Bankcard Trans Fee." + System.Environment.NewLine;
                }
            }

            //Check to ensure correct BETs are being used
            if ((lstProcessorNames.SelectedItem.Text.Contains("Sage")) && (lstRateNames.SelectedItem.Text.Contains("Tiered")))
            {
                string discountRate = "";
                if (rdbCNP.Checked)
                    discountRate = txtDRQP.Text.ToString().Trim();
                else
                    discountRate = txtDRQP.Text.ToString().Trim();

                decimal midQualStep = Convert.ToDecimal(txtDRMQ.Text.ToString().Trim()) - Convert.ToDecimal(discountRate.Trim());
                decimal nonQualStep = Convert.ToDecimal(txtDRNQ.Text.ToString().Trim()) - Convert.ToDecimal(discountRate.Trim());
                //if ((midQualStep != 1m) && (nonQualStep != 2m))
                //{
                /*if ((midQualStep != 0.8m) || (nonQualStep != 2.05m))
                {
                    if ((midQualStep != 1m) || (nonQualStep != 1.5m))
                    {
                        if ((midQualStep != 0.5m) || (nonQualStep != 1m))
                        {
                            if ((midQualStep != 0.72m) && (nonQualStep != 1.67m))
                            {
                                strError += "Only the following combinations of MidQualSteps and NonQualSteps can be used: 0.80, 2.05; 1.00, 1.50; 0.50, 1.00. Please correct MidQual and NonQual rates." + System.Environment.NewLine;
                            }
                        }
                    }
                }*/
                //}
                if ((midQualStep != 1m) || (nonQualStep != 2m))
                {
                    if ((midQualStep != 0.8m) || (nonQualStep != 2.05m))
                    {
                        if ((midQualStep != 1m) || (nonQualStep != 1.5m))
                        {
                            if ((midQualStep != 0.5m) || (nonQualStep != 1m))
                            {
                                strError += "Only the following combinations of MidQualSteps and NonQualSteps can be used: 1.00, 2.00; 0.80, 2.05; 1.00, 1.50; 0.50, 1.00. Please correct MidQual and NonQual rates." + System.Environment.NewLine;
                            }
                        }
                    }
                }
            }
        }//end if accttype

        if ((AcctType == 2) || (AcctType == 4))
        {
            if (lstGatewayNames.SelectedItem.Text.ToLower() != "none")
            {
                //GatewayTransFee
                if (GatewayTransFeeLow.Value != "")
                {
                    if ((txtGWTransFee.Text == "") || (Convert.ToDecimal(txtGWTransFee.Text) < Convert.ToDecimal(GatewayTransFeeLow.Value)))
                        strError += "Enter at least $" + GatewayTransFeeLow.Value + " for Gateway Transaction Fee." + System.Environment.NewLine;
                }
                //GatewayMonFee
                if (GatewayMonFeeLow.Value != "")
                {
                    if ((txtGWMonthlyFee.Text == "") || (Convert.ToDecimal(txtGWMonthlyFee.Text) < Convert.ToDecimal(GatewayMonFeeLow.Value)))
                        strError += "Enter at least $" + GatewayMonFeeLow.Value + " for Gateway Monthly Fee." + System.Environment.NewLine;
                }
                //GatewaySetupFee
                if (GatewaySetupFeeLow.Value != "")
                {
                    if ((txtGWSetupFee.Text == "") || (Convert.ToDecimal(txtGWSetupFee.Text) < Convert.ToDecimal(GatewaySetupFeeLow.Value)))
                        strError += "Enter at least " + GatewaySetupFeeLow.Value + " for Gateway Setup Fee" + System.Environment.NewLine;
                }

                //if E-Check enabled
                /*if (lstCheckService.SelectedItem.Text.ToLower().Contains("eCheck.Net"))
                {
                    if ((txteSIRMonMin.Text == "") || (Convert.ToDecimal(txteSIRMonMin.Text) < Convert.ToDecimal(eSIRMonMinLow.Value)))
                        strError += "Enter at least $" + eSIRMonMinLow.Value + " for E-Check Miniumum Monthly (Standard Industry Rates)." + System.Environment.NewLine;

                    if ((txteSIRTransFee1.Text == "") || (Convert.ToDecimal(txteSIRTransFee1.Text) < Convert.ToDecimal(eSIRTransFee1Low.Value)))
                        strError += "Enter at least $" + eSIRTransFee1Low.Value + " for E-Check Transaction Fee (Standard Industry Rates)." + System.Environment.NewLine;

                    if ((txteSIRTransFee2.Text == "") || (Convert.ToDecimal(txteSIRTransFee2.Text) < Convert.ToDecimal(eSIRTransFee2Low.Value)))
                        strError += "Enter at least $" + eSIRTransFee2Low.Value + " for E-Check Transaction Fee 2(Standard Industry Rates)." + System.Environment.NewLine;

                    if ((txteSIRTransFee3.Text == "") || (Convert.ToDecimal(txteSIRTransFee3.Text) < Convert.ToDecimal(eSIRTransFee3Low.Value)))
                        strError += "Enter at least $" + eSIRTransFee3Low.Value + " for E-Check Transaction Fee 3(Standard Industry Rates)." + System.Environment.NewLine;

                    if ((txteSIRTransFee4.Text == "") || (Convert.ToDecimal(txteSIRTransFee4.Text) < Convert.ToDecimal(eSIRTransFee4Low.Value)))
                        strError += "Enter at least $" + eSIRTransFee4Low.Value + " for E-Check Transaction Fee 4(Standard Industry Rates)." + System.Environment.NewLine;

                    if ((txteSIRDiscountRate1.Text == "") || (Convert.ToDecimal(txteSIRDiscountRate1.Text) < Convert.ToDecimal(eSIRDiscountRate1Low.Value)))
                        strError += "Enter at least $" + eSIRDiscountRate1Low.Value + " for E-Check Transaction Fee (Standard Industry Rates)." + System.Environment.NewLine;

                    if ((txteSIRDiscountRate2.Text == "") || (Convert.ToDecimal(txteSIRDiscountRate2.Text) < Convert.ToDecimal(eSIRDiscountRate2Low.Value)))
                        strError += "Enter at least $" + eSIRDiscountRate2Low.Value + " for E-Check Transaction Fee 2(Standard Industry Rates)." + System.Environment.NewLine;

                    if ((txteSIRDiscountRate3.Text == "") || (Convert.ToDecimal(txteSIRDiscountRate3.Text) < Convert.ToDecimal(eSIRDiscountRate3Low.Value)))
                        strError += "Enter at least $" + eSIRDiscountRate3Low.Value + " for E-Check Transaction Fee 3(Standard Industry Rates)." + System.Environment.NewLine;

                    if ((txteSIRDiscountRate4.Text == "") || (Convert.ToDecimal(txteSIRDiscountRate4.Text) < Convert.ToDecimal(eSIRDiscountRate4Low.Value)))
                        strError += "Enter at least $" + eSIRDiscountRate4Low.Value + " for E-Check Transaction Fee 4(Standard Industry Rates)." + System.Environment.NewLine;

                    if ((txtePIRMonMin.Text == "") || (Convert.ToDecimal(txtePIRMonMin.Text) < Convert.ToDecimal(ePIRMonMinLow.Value)))
                        strError += "Enter at least $" + ePIRMonMinLow.Value + " for E-Check Miniumum Monthly (Standard Industry Rates)." + System.Environment.NewLine;

                    if ((txtePIRTransFee1.Text == "") || (Convert.ToDecimal(txtePIRTransFee1.Text) < Convert.ToDecimal(ePIRTransFee1Low.Value)))
                        strError += "Enter at least $" + ePIRTransFee1Low.Value + " for E-Check Transaction Fee (Standard Industry Rates)." + System.Environment.NewLine;

                    if ((txtePIRDiscountRate1.Text == "") || (Convert.ToDecimal(txtePIRDiscountRate1.Text) < Convert.ToDecimal(ePIRDiscountRate1Low.Value)))
                        strError += "Enter at least $" + ePIRDiscountRate1Low.Value + " for E-Check Transaction Fee (Standard Industry Rates)." + System.Environment.NewLine;

                }*/

            }
        }

        //Additional Services Checks

        //Debit Mon Fee
        if ((DebitMonFeeLow.Value != ""))
        {
            if (chkOnlineDebit.Checked == true)
            {
                if ((txtDebitMonFee.Text == "") || (Convert.ToDecimal(txtDebitMonFee.Text) < Convert.ToDecimal(DebitMonFeeLow.Value)))
                {
                    strError += "Enter at least $" + DebitMonFeeLow.Value + " for Debit Mon Fee." + "<BR/>";
                }
            }
        }
        //DebitTransFee
        if ((DebitTransFeeLow.Value != ""))
        {
            if (chkOnlineDebit.Checked == true)
            {
                if ((lstDebitTransFee.SelectedItem.Text == "") || (Convert.ToDecimal(lstDebitTransFee.SelectedItem.Text) < Convert.ToDecimal(DebitTransFeeLow.Value)))
                    strError += "Enter at least $" + DebitTransFeeLow.Value + " for Debit Trans Fee." + "<BR/>";

                if ((txtDebitTransFee.Text == "") || (Convert.ToDecimal(txtDebitTransFee.Text) < Convert.ToDecimal(DebitTransFeeLow.Value)))
                    strError += "Enter at least $" + DebitTransFeeLow.Value + " for Debit Trans Fee." + "<BR/>";
            }
        }

        //CGDiscRate
        if ((CGDiscRateLow.Value != "") && (txtCGDiscRate.Text != ""))
        {
            if ((Convert.ToDecimal(txtCGDiscRate.Text) < Convert.ToDecimal(CGDiscRateLow.Value)))
                strError += "Enter at least $" + CGDiscRateLow.Value + " for Check Service Disc Rate or leave blank if it does not apply." + "<BR/>";
        }
        //CGMonFee
        if ((CGMonFeeLow.Value != "") && (txtCGMonFee.Text != ""))
        {
            if ((Convert.ToDecimal(txtCGMonFee.Text) < Convert.ToDecimal(CGMonFeeLow.Value)))
                strError += "Enter at least $" + CGMonFeeLow.Value + " for Check Service Mon Fee or leave blank if it does not apply." + "<BR/>";
        }
        //CGMonMin
        if ((CGMonMinLow.Value != "") && (txtCGMonMin.Text != ""))
        {
            if ((Convert.ToDecimal(txtCGMonMin.Text) < Convert.ToDecimal(CGMonMinLow.Value)))
                strError += "Enter at least $" + CGMonMinLow.Value + " for Check Service Mon Min or leave blank if it does not apply." + "<BR/>";
        }
        //CGTransFee
        if ((CGTransFeeLow.Value != "") && (txtCGTransFee.Text != ""))
        {
            if ((Convert.ToDecimal(txtCGTransFee.Text) < Convert.ToDecimal(CGTransFeeLow.Value)))
                strError += "Enter at least $" + CGTransFeeLow.Value + " for Check Service Trans Fee or leave blank if it does not apply." + "<BR/>";
        }
        //GCMonMin
        if ((GCMonFeeLow.Value != "") && (txtGCMonFee.Text != ""))
        {
            if ((Convert.ToDecimal(txtGCMonFee.Text) < Convert.ToDecimal(GCMonFeeLow.Value)))
            {
                strError += "Enter at least $" + GCMonFeeLow.Value + " for Gift Card Mon Min or leave blank if it does not apply." + "<BR/>";
            }
        }
        //GCTransFee
        if ((GCTransFeeLow.Value != "") && (txtGCTransFee.Text != ""))
        {
            if ((Convert.ToDecimal(txtGCTransFee.Text) < Convert.ToDecimal(GCTransFeeLow.Value)))
                strError += "Enter at least $" + GCTransFeeLow.Value + " for Gift Card Trans Fee or leave blank if it does not apply." + "<BR/>";
        }
        //EBTMonMin
        if ((EBTMonFeeLow.Value != ""))
        {
            if (chkEBT.Checked == true)
            {
                if ((txtEBTMonFee.Text != "") || (Convert.ToDecimal(txtEBTMonFee.Text) < Convert.ToDecimal(EBTMonFeeLow.Value)))
                    strError += "Enter at least $" + EBTMonFeeLow.Value + " for EBT Mon Min." + "<BR/>";
            }
        }
        //EBTTransFee
        if ((EBTTransFeeLow.Value != ""))
        {
            if (chkEBT.Checked == true)
            {
                if ((txtEBTTransFee.Text != "") || (Convert.ToDecimal(txtEBTTransFee.Text) < Convert.ToDecimal(EBTTransFeeLow.Value)))
                    strError += "Enter at least $" + EBTTransFeeLow.Value + " for EBT Trans Fee." + "<BR/>";
            }
        }
        //AnnualFee
        /*if (AnnualFeeLow.Value != "")
        {
            if ((txtAnnualFee.Text == "") || (Convert.ToDecimal(txtAnnualFee.Text) < Convert.ToDecimal(AnnualFeeLow.Value)))
            {
                strError += "Enter at least $" + AnnualFeeLow.Value + " for Annual Fee." + System.Environment.NewLine;
            }
        }*/
        return strError;
    }//end function ValidateRates
    #endregion

    #region ValidateProcessorRates
    public string ValidateProcessorRates()
    {


        string strError = "";

        if ((AcctType == 1) || (AcctType == 4))
        {
            //Customer Service



            if (CustServLow.Value != "")
            {
                if ((txtCustomerService.Text == "") || (Convert.ToDecimal(txtCustomerService.Text) < Convert.ToDecimal(CustServLow.Value)))
                    //if ((txtCustomerService.Text == "") || (Convert.ToDecimal(txtCustomerService.Text) < Convert.ToDecimal(5.0)))
                    strError += "Enter at least $" + CustServLow.Value + " for Customer Service Fee." + System.Environment.NewLine;
            }



            //Internet Statement can be BLANK or at or above the Minimum
            if ((InternetStmtLow.Value != "") && (txtInternetStmt.Text != ""))
            {
                if (Convert.ToDecimal(txtInternetStmt.Text) < Convert.ToDecimal(InternetStmtLow.Value))
                    strError += "Enter at least $" + InternetStmtLow.Value + " for Internet Statement Fee or leave blank if it does not apply." + System.Environment.NewLine;
            }




            //Monthly Minimum
            if (MonMinLow.Value != "")
            {
                if ((txtMonMin.Text == "") || (Convert.ToDecimal(txtMonMin.Text) < Convert.ToDecimal(MonMinLow.Value)))
                    strError += "Enter at least $" + MonMinLow.Value + " for Monthly Minimum." + System.Environment.NewLine;
            }


            //Transaction Fee
            if (TransFeeLow.Value != "")
            {
                if ((txtTransFee.Text == "") || (Convert.ToDecimal(txtTransFee.Text) < Convert.ToDecimal(TransFeeLow.Value)))
                    strError += "Enter at least $" + TransFeeLow.Value + " for Transaction Fee." + System.Environment.NewLine;
            }



            //DiscRateQualPres
            if (DiscRateQualPresLow.Value != "")
            {
                if ((txtDRQP.Text == "") || (Convert.ToDecimal(txtDRQP.Text) < Convert.ToDecimal(DiscRateQualPresLow.Value)))
                {
                    strError += "Enter at least " + DiscRateQualPresLow.Value + "% for Disc Rate Qual Pres." + System.Environment.NewLine;
                }
            }
            //DiscRateQualNP
            /*
            if (DiscRateQualNPLow.Value != "")
            {
                if ((txtDRQNP.Text == "") || (Convert.ToDecimal(txtDRQNP.Text) < Convert.ToDecimal(DiscRateQualNPLow.Value)))
                    strError += "Enter at least " + DiscRateQualNPLow.Value + "% for Disc Rate Qual NP." + System.Environment.NewLine;
            }*/





            //Debit DiscRateQualNP
            /*
            if (DebitQualNPLow.Value != "")
            {
                if ((txtQualNPDebit.Text == "") || (Convert.ToDecimal(txtQualNPDebit.Text) < Convert.ToDecimal(DebitQualNPLow.Value)))
                    strError += "Enter at least " + DebitQualNPLow.Value + "% for Debit Disc Rate Qual NP." + System.Environment.NewLine;
            }*/


            //Debit DiscRateMidQual
            if (lstRateNames.SelectedItem.Text.Contains("Tiered") && DebitMidQualLow.Value != "")
            {
                if ((txtMidQualDebit.Text == "") || (Convert.ToDecimal(txtMidQualDebit.Text) < Convert.ToDecimal(DebitMidQualLow.Value)))
                    strError += "Enter at least " + DebitMidQualLow.Value + "% for Debit Disc Rate Mid Qual." + System.Environment.NewLine;
            }

            //Debit DiscRateNonQual
            if (lstRateNames.SelectedItem.Text.Contains("Tiered") && DebitNonQualLow.Value != "")
            {
                if ((txtNonQualDebit.Text == "") || (Convert.ToDecimal(txtNonQualDebit.Text) < Convert.ToDecimal(DebitNonQualLow.Value)))
                    strError += "Enter at least " + DebitNonQualLow.Value + "% for  Debit Disc Rate Non Qual." + System.Environment.NewLine;
            }


            //Amex DiscRateQualNP
            if (lstRateNames.SelectedItem.Text.Contains("Tiered") && AmexDiscRateQualLow.Value != "")
            {
                if ((txtAmexQual.Text == "") || (Convert.ToDecimal(txtAmexQual.Text) < Convert.ToDecimal(AmexDiscRateQualLow.Value)))
                    strError += "Enter at least " + AmexDiscRateQualLow.Value + "% for American Express Disc Rate Qual." + System.Environment.NewLine;
            }

            //Amex DiscRateMidQual
            if (lstRateNames.SelectedItem.Text.Contains("Tiered") && AmexDiscRateMidQualLow.Value != "")
            {
                if ((txtAmexMidQual.Text == "") || (Convert.ToDecimal(txtAmexMidQual.Text) < Convert.ToDecimal(AmexDiscRateMidQualLow.Value)))
                    strError += "Enter at least " + AmexDiscRateMidQualLow.Value + "% for American Express Disc Rate Mid Qual." + System.Environment.NewLine;
            }

            //Amex DiscRateNonQual
            if (lstRateNames.SelectedItem.Text.Contains("Tiered") && AmexDiscRateNonQualLow.Value != "")
            {
                if ((txtAmexNonQual.Text == "") || (Convert.ToDecimal(txtAmexNonQual.Text) < Convert.ToDecimal(AmexDiscRateNonQualLow.Value)))
                    strError += "Enter at least " + AmexDiscRateNonQualLow.Value + "% for American Express Disc Rate Non Qual." + System.Environment.NewLine;
            }



            //If IMS/IPS and Card Present overwrite the Mid Qual Low to be the Qual Pres PLUS the specified Mid Qual Step in the database 
            if ((CardPresent == "CP") && (lstProcessorNames.SelectedItem.Text == "IMS"))
            {
                if ((txtDRQP.Text != "") && (DiscRateMidQualStep.Value != ""))
                    DiscRateMidQualLow.Value = Convert.ToString(Convert.ToDecimal(txtDRQP.Text) + Convert.ToDecimal(DiscRateMidQualStep.Value));
            }

            if ((CardPresent == "CP") && (lstProcessorNames.SelectedItem.Text == "Intuit Payment Solutions"))
            {
                if ((txtDRQP.Text != "") && (DiscRateMidQualStep.Value != ""))
                    DiscRateMidQualLow.Value = Convert.ToString(Convert.ToDecimal(txtDRQP.Text) + Convert.ToDecimal(DiscRateMidQualStep.Value));
            }
            //DiscRateMidQual
            if (lstRateNames.SelectedItem.Text.Contains("Tiered") && DiscRateMidQualLow.Value != "")
            {
                if ((txtDRMQ.Text == "") || (Convert.ToDecimal(txtDRMQ.Text) < Convert.ToDecimal(DiscRateMidQualLow.Value)))
                    strError += "Enter at least " + DiscRateMidQualLow.Value + "% for Disc Rate Mid Qual." + System.Environment.NewLine;
            }
            //DiscRateNonQual
            if (lstRateNames.SelectedItem.Text.Contains("Tiered") && DiscRateNonQualLow.Value != "")
            {
                if ((txtDRNQ.Text == "") || (Convert.ToDecimal(txtDRNQ.Text) < Convert.ToDecimal(DiscRateNonQualLow.Value)))
                    strError += "Enter at least " + DiscRateNonQualLow.Value + "% for Disc Rate Non Qual." + System.Environment.NewLine;
            }
            //DiscRateQualDebit        
            if (lstRateNames.SelectedItem.Text.Contains("Tiered") && DiscRateQualDebitLow.Value != "")
            {
                if ((txtDRQD.Text == "") || (Convert.ToDecimal(txtDRQD.Text) < Convert.ToDecimal(DiscRateQualDebitLow.Value)))
                {
                    strError += "Enter at least " + DiscRateQualDebitLow.Value + "% for Disc Rate Qual Debit." + System.Environment.NewLine;
                }
            }
            //ChargebackFee
            if (ChargebackFeeLow.Value != "")
            {
                if ((txtChargebackFee.Text == "") || (Convert.ToDecimal(txtChargebackFee.Text) < Convert.ToDecimal(ChargebackFeeLow.Value)))
                {
                    strError += "Enter at least $" + ChargebackFeeLow.Value + " for Chargeback Fee." + System.Environment.NewLine;
                }
            }
            //RetrievalFee
            if (RetrievalFeeLow.Value != "")
            {
                if ((txtRetrievalFee.Text == "") || (Convert.ToDecimal(txtRetrievalFee.Text) < Convert.ToDecimal(RetrievalFeeLow.Value)))
                    strError += "Enter at least $" + RetrievalFeeLow.Value + " for Retrieval Fee." + System.Environment.NewLine;
            }
            //VoiceAuth
            if (VoiceAuthLow.Value != "")
            {
                if ((txtVoiceAuth.Text == "") || (Convert.ToDecimal(txtVoiceAuth.Text) < Convert.ToDecimal(VoiceAuthLow.Value)))
                    strError += "Enter at least $" + VoiceAuthLow.Value + " for Voice Authorization Fee." + System.Environment.NewLine;
            }
            //BatchHeader
            if (BatchHeaderLow.Value != "")
            {
                if ((txtBatchHeader.Text == "") || (Convert.ToDecimal(txtBatchHeader.Text) < Convert.ToDecimal(BatchHeaderLow.Value)))
                    strError += "Enter at least $" + BatchHeaderLow.Value + " for Batch Header Fee." + System.Environment.NewLine;
            }
            //AVS
            if (AVSLow.Value != "")
            {

                OnlineAppProcessingBL Processing = new OnlineAppProcessingBL(AppId);
                PartnerDS.OnlineAppRatesDataTable dt = Processing.GetRates();



                if (dt.Rows.Count > 0)
                {
                    if (lstProcessorNames.SelectedItem.Text.ToLower().Contains("Sage"))
                    {
                        if ((lstAVS.SelectedItem.Text == "") || (Convert.ToDecimal(lstAVS.SelectedItem.Text) < Convert.ToDecimal(AVSLow.Value)))
                        {
                            strError += "Enter at least $" + AVSLow.Value + " for AVS Fee." + System.Environment.NewLine;
                        }
                    }
                    else if ((txtAVS.Text == "") || (Convert.ToDecimal(txtAVS.Text) < Convert.ToDecimal(AVSLow.Value)))
                        strError += "Enter at least $" + AVSLow.Value + " for AVS Fee." + System.Environment.NewLine;
                }
            }
            //WirelessAccessFee can be BLANK or at or above the Minimum
            if ((WirelessAccessFeeLow.Value != "") && (txtWirelessAccess.Text != ""))
            {
                if ((Convert.ToDecimal(txtWirelessAccess.Text) < Convert.ToDecimal(WirelessAccessFeeLow.Value)))
                    strError += "Enter at least $" + WirelessAccessFeeLow.Value + " for Wireless Access Fee or leave blank if it does not apply." + System.Environment.NewLine;
            }
            //WirelessTransFee can be BLANK or at or above the Minimum
            if ((WirelessTransFeeLow.Value != "") && (txtWirelessTransFee.Text != ""))
            {
                if ((Convert.ToDecimal(txtWirelessTransFee.Text) < Convert.ToDecimal(WirelessTransFeeLow.Value)))
                    strError += "Enter at least $" + WirelessTransFeeLow.Value + " for Wireless Trans Fee or leave blank if it does not apply." + System.Environment.NewLine;
            }
            //NBCTransFee
            if (NBCTransFeeLow.Value != "")
            {
                if ((txtNBCTransFee.Text == "") || (Convert.ToDecimal(txtNBCTransFee.Text) < Convert.ToDecimal(NBCTransFeeLow.Value)))
                {
                    strError += "Enter at least $" + NBCTransFeeLow.Value + " for Non-Bankcard Trans Fee." + System.Environment.NewLine;
                }
            }

            //Check to ensure correct BETs are being used
            if ((lstProcessorNames.SelectedItem.Text.Contains("Sage")) && (!(lstRateNames.SelectedItem.Text.Contains("Interchange")) && !(lstRateNames.SelectedItem.Text.Contains("Flat Rate"))))
            {
                string discountRate = "";
                if (rdbCNP.Checked)
                    discountRate = txtDRQP.Text.ToString().Trim();
                else
                    discountRate = txtDRQP.Text.ToString().Trim();

                decimal midQualStep = Convert.ToDecimal(txtDRMQ.Text.ToString().Trim()) - Convert.ToDecimal(discountRate.Trim());
                decimal nonQualStep = Convert.ToDecimal(txtDRNQ.Text.ToString().Trim()) - Convert.ToDecimal(discountRate.Trim());
                //if ((midQualStep != 1m) && (nonQualStep != 2m))
                //{
                /*if ((midQualStep != 0.8m) || (nonQualStep != 2.05m))
                {
                    if ((midQualStep != 1m) || (nonQualStep != 1.5m))
                    {
                        if ((midQualStep != 0.5m) || (nonQualStep != 1m))
                        {
                            if ((midQualStep != 0.72m) && (nonQualStep != 1.67m))
                            {
                                strError += "Only the following combinations of MidQualSteps and NonQualSteps can be used: 0.80, 2.05; 1.00, 1.50; 0.50, 1.00. Please correct MidQual and NonQual rates." + System.Environment.NewLine;
                            }
                        }
                    }
                }*/
                //}
                if ((midQualStep != 1m) || (nonQualStep != 2m))
                {
                    if ((midQualStep != 0.8m) || (nonQualStep != 2.05m))
                    {
                        if ((midQualStep != 1m) || (nonQualStep != 1.5m))
                        {
                            if ((midQualStep != 0.5m) || (nonQualStep != 1m))
                            {
                                strError += "Only the following combinations of MidQualSteps and NonQualSteps can be used: 1.00, 2.00; 0.80, 2.05; 1.00, 1.50; 0.50, 1.00. Please correct MidQual and NonQual rates." + System.Environment.NewLine;
                            }
                        }
                    }
                }
            }
        }

        //end if accttype



        //Additional Services Checks
        //DebitMonFee


        if ((PinDebitDiscountLow.Value != ""))
        {
            if (chkOnlineDebit.Checked == true)
            {
                if ((txtPinDebitDiscount.Text == "") || (Convert.ToDecimal(txtPinDebitDiscount.Text) < Convert.ToDecimal(PinDebitDiscountLow.Value)))
                {
                    strError += "Enter at least " + PinDebitDiscountLow.Value + "% for Debit Discount Fee." + "<BR/>";
                }
            }
        }

        if ((DebitMonFeeLow.Value != ""))
        {
            if (chkOnlineDebit.Checked == true)
            {
                if ((txtDebitMonFee.Text == "") || (Convert.ToDecimal(txtDebitMonFee.Text) < Convert.ToDecimal(DebitMonFeeLow.Value)))
                {
                    strError += "Enter at least $" + DebitMonFeeLow.Value + " for Debit Mon Fee." + "<BR/>";
                }
            }
        }
        //DebitTransFee

        if ((DebitTransFeeLow.Value != ""))
        {
            if (chkOnlineDebit.Checked == true)
            {
                if (lstProcessorNames.SelectedItem.Text.Contains("Sage"))
                {
                    if ((lstDebitTransFee.SelectedItem.Text == "") || (Convert.ToDecimal(lstDebitTransFee.SelectedItem.Text) < Convert.ToDecimal(DebitTransFeeLow.Value)))
                        strError += "Enter at least $" + DebitTransFeeLow.Value + " for Debit Trans Fee." + "<BR/>";
                }
                else
                {
                    if ((txtDebitTransFee.Text == "") || (Convert.ToDecimal(txtDebitTransFee.Text) < Convert.ToDecimal(DebitTransFeeLow.Value)))
                        strError += "Enter at least $" + DebitTransFeeLow.Value + " for Debit Trans Fee." + "<BR/>";
                }
            }
        }

        //EBTMonMin
        if ((EBTMonFeeLow.Value != ""))
        {
            if (chkEBT.Checked == true)
            {
                if ((txtEBTMonFee.Text == "") || (Convert.ToDecimal(txtEBTMonFee.Text) < Convert.ToDecimal(EBTMonFeeLow.Value)))
                    strError += "Enter at least $" + EBTMonFeeLow.Value + " for EBT Mon Min." + "<BR/>";
            }
        }
        //EBTTransFee
        if ((EBTTransFeeLow.Value != ""))
        {
            if (chkEBT.Checked == true)
            {
                if ((txtEBTTransFee.Text == "") || (Convert.ToDecimal(txtEBTTransFee.Text) < Convert.ToDecimal(EBTTransFeeLow.Value)))
                    strError += "Enter at least $" + EBTTransFeeLow.Value + " for EBT Trans Fee." + "<BR/>";
            }
        }
        //AnnualFee
        /*if (AnnualFeeLow.Value != "")
        {
            if ((txtAnnualFee.Text == "") || (Convert.ToDecimal(txtAnnualFee.Text) < Convert.ToDecimal(AnnualFeeLow.Value)))
            {
                strError += "Enter at least $" + AnnualFeeLow.Value + " for Annual Fee." + System.Environment.NewLine;
            }
        }*/
        return strError;
    }//end function ValidateRates
    #endregion

    #region ValidateGatewayRates
    public string ValidateGatewayRates()
    {
        string strError = "";
        if ((AcctType == 2) || (AcctType == 4))
        {
            if (lstGatewayNames.SelectedItem.Text.ToLower() != "none")
            {
                //GatewayTransFee
                if (GatewayTransFeeLow.Value != "")
                {
                    if ((txtGWTransFee.Text == "") || (Convert.ToDecimal(txtGWTransFee.Text) < Convert.ToDecimal(GatewayTransFeeLow.Value)))
                        strError += "Enter at least $" + GatewayTransFeeLow.Value + " for Gateway Transaction Fee." + System.Environment.NewLine;
                }
                //GatewayMonFee
                if (GatewayMonFeeLow.Value != "")
                {
                    if ((txtGWMonthlyFee.Text == "") || (Convert.ToDecimal(txtGWMonthlyFee.Text) < Convert.ToDecimal(GatewayMonFeeLow.Value)))
                        strError += "Enter at least $" + GatewayMonFeeLow.Value + " for Gateway Monthly Fee." + System.Environment.NewLine;
                }
                //GatewaySetupFee
                if (GatewaySetupFeeLow.Value != "")
                {
                    if ((txtGWSetupFee.Text == "") || (Convert.ToDecimal(txtGWSetupFee.Text) < Convert.ToDecimal(GatewaySetupFeeLow.Value)))
                        strError += "Enter at least " + GatewaySetupFeeLow.Value + " for Gateway Setup Fee" + System.Environment.NewLine;
                }

                //if E-Check enabled
                /*if (lstCheckService.SelectedItem.Text.ToLower().Contains("eCheck.Net"))
                {
                    if ((txteSIRMonMin.Text == "") || (Convert.ToDecimal(txteSIRMonMin.Text) < Convert.ToDecimal(eSIRMonMinLow.Value)))
                        strError += "Enter at least $" + eSIRMonMinLow.Value + " for E-Check Miniumum Monthly (Standard Industry Rates)." + System.Environment.NewLine;

                    if ((txteSIRTransFee1.Text == "") || (Convert.ToDecimal(txteSIRTransFee1.Text) < Convert.ToDecimal(eSIRTransFee1Low.Value)))
                        strError += "Enter at least $" + eSIRTransFee1Low.Value + " for E-Check Transaction Fee (Standard Industry Rates)." + System.Environment.NewLine;

                    if ((txteSIRTransFee2.Text == "") || (Convert.ToDecimal(txteSIRTransFee2.Text) < Convert.ToDecimal(eSIRTransFee2Low.Value)))
                        strError += "Enter at least $" + eSIRTransFee2Low.Value + " for E-Check Transaction Fee 2(Standard Industry Rates)." + System.Environment.NewLine;

                    if ((txteSIRTransFee3.Text == "") || (Convert.ToDecimal(txteSIRTransFee3.Text) < Convert.ToDecimal(eSIRTransFee3Low.Value)))
                        strError += "Enter at least $" + eSIRTransFee3Low.Value + " for E-Check Transaction Fee 3(Standard Industry Rates)." + System.Environment.NewLine;

                    if ((txteSIRTransFee4.Text == "") || (Convert.ToDecimal(txteSIRTransFee4.Text) < Convert.ToDecimal(eSIRTransFee4Low.Value)))
                        strError += "Enter at least $" + eSIRTransFee4Low.Value + " for E-Check Transaction Fee 4(Standard Industry Rates)." + System.Environment.NewLine;

                    if ((txteSIRDiscountRate1.Text == "") || (Convert.ToDecimal(txteSIRDiscountRate1.Text) < Convert.ToDecimal(eSIRDiscountRate1Low.Value)))
                        strError += "Enter at least $" + eSIRDiscountRate1Low.Value + " for E-Check Transaction Fee (Standard Industry Rates)." + System.Environment.NewLine;

                    if ((txteSIRDiscountRate2.Text == "") || (Convert.ToDecimal(txteSIRDiscountRate2.Text) < Convert.ToDecimal(eSIRDiscountRate2Low.Value)))
                        strError += "Enter at least $" + eSIRDiscountRate2Low.Value + " for E-Check Transaction Fee 2(Standard Industry Rates)." + System.Environment.NewLine;

                    if ((txteSIRDiscountRate3.Text == "") || (Convert.ToDecimal(txteSIRDiscountRate3.Text) < Convert.ToDecimal(eSIRDiscountRate3Low.Value)))
                        strError += "Enter at least $" + eSIRDiscountRate3Low.Value + " for E-Check Transaction Fee 3(Standard Industry Rates)." + System.Environment.NewLine;

                    if ((txteSIRDiscountRate4.Text == "") || (Convert.ToDecimal(txteSIRDiscountRate4.Text) < Convert.ToDecimal(eSIRDiscountRate4Low.Value)))
                        strError += "Enter at least $" + eSIRDiscountRate4Low.Value + " for E-Check Transaction Fee 4(Standard Industry Rates)." + System.Environment.NewLine;

                    if ((txtePIRMonMin.Text == "") || (Convert.ToDecimal(txtePIRMonMin.Text) < Convert.ToDecimal(ePIRMonMinLow.Value)))
                        strError += "Enter at least $" + ePIRMonMinLow.Value + " for E-Check Miniumum Monthly (Standard Industry Rates)." + System.Environment.NewLine;

                    if ((txtePIRTransFee1.Text == "") || (Convert.ToDecimal(txtePIRTransFee1.Text) < Convert.ToDecimal(ePIRTransFee1Low.Value)))
                        strError += "Enter at least $" + ePIRTransFee1Low.Value + " for E-Check Transaction Fee (Standard Industry Rates)." + System.Environment.NewLine;

                    if ((txtePIRDiscountRate1.Text == "") || (Convert.ToDecimal(txtePIRDiscountRate1.Text) < Convert.ToDecimal(ePIRDiscountRate1Low.Value)))
                        strError += "Enter at least $" + ePIRDiscountRate1Low.Value + " for E-Check Transaction Fee (Standard Industry Rates)." + System.Environment.NewLine;

                }*/

            }
        }



        return strError;
    }//end function ValidateRates
    #endregion


    //This function displays error message on a label
    protected void DisplayMessage(string errText)
    {
        lblError.Visible = true;
        lblError.Text = errText;
    }//end function set error message

    protected void chkMA_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (chkMA.Checked)
            {
                pnlMerchantRates.Visible = true;
                pnlProcessor.Visible = true;
                pnlMerchantDetails.Visible = true;
            }
            else
            {
                pnlMerchantRates.Visible = true;
                pnlProcessor.Visible = false;
                pnlMerchantDetails.Visible = false;
            }
        }//end try
        catch (Exception err)
        {
            CreateLog Log = new CreateLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), err.Message);
            DisplayMessage("Error loading data.");
        }
    }//end check changed

    protected void chkGateway_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (chkGateway.Checked)
            {
                pnlGatewayRates.Visible = true;
                pnlGatewaylist.Visible = true;
                pnlGatewayratessetup.Visible = false;
                NewAppInfo App = new NewAppInfo(AppId);
                int PID = App.ReturnPID();
                PackageInfo Package = new PackageInfo();
                DataSet dsPackageInfo = Package.GetPackageInfo(PID);
                DataTable dtPackageInfo = dsPackageInfo.Tables[0];
                if (dtPackageInfo.Rows.Count > 0)
                {
                    //Insert GW info in Gateway Table
                    DataRow drPackageInfo = dtPackageInfo.Rows[0];
                    Gateway GatewayInfo = new Gateway(AppId);

                    lstGatewayNames.SelectedValue = drPackageInfo["Gateway"].ToString().Trim();
                    /*
                    ListItem lstItem = new System.Web.UI.WebControls.ListItem();
                    lstItem.Text = "ROAMpay";
                    lstItem.Value = "ROAMpay";
                    lstGatewayNames.Items.Add(lstItem);*/

                    txtGWSetupFee.Text = drPackageInfo["GatewaySetupFee"].ToString().Trim();
                    txtGWMonthlyFee.Text = drPackageInfo["GatewayMonFee"].ToString().Trim();
                    txtGWTransFee.Text = drPackageInfo["GatewayTransFee"].ToString().Trim();

                    //Insert GW Sales Opps
                    /*OnlineAppClassLibrary.SalesOppsBL SalesOppsGW = new OnlineAppClassLibrary.SalesOppsBL(AppId);
                    SalesOppsGW.CreateSalesOppsGW();*/
                }
            }
            else
            {
                pnlGatewayRates.Visible = true;
                pnlGatewaylist.Visible = false;
                pnlGatewayratessetup.Visible = false;
                //lstGatewayNames.Value = "";
                /*GatewayTransFeeLow.Value = "";
                GatewaySetupFeeLow.Value = "";
                GatewayMonFeeLow.Value = "";*/
            }
        }//end try
        catch (Exception err)
        {
            CreateLog Log = new CreateLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), err.Message);
            DisplayMessage("Error loading data.");
        }
    }//end check changed

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

    protected void chkOnlineDebit_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (chkOnlineDebit.Checked)
            {

                OnlineAppProcessingBL Processing = new OnlineAppProcessingBL(AppId);
                PartnerDS.OnlineAppRatesDataTable dt = Processing.GetRates();

                if (dt.Rows.Count > 0)
                {
                    AcctType = Convert.ToInt32(dt[0].AcctType);
                }

                string strCardPresent = Convert.ToString(AcctType);

                BoundsBL Bounds = new BoundsBL();
                PartnerDS.ProcessorBoundsDataTable dtBounds = Bounds.GetProcessorBounds(lstProcessorNames.SelectedItem.Text, strCardPresent);

                txtPinDebitDiscount.Enabled = true;
                txtDebitMonFee.Enabled = true;
                lstDebitTransFee.Enabled = true;


                //txtPinDebitDiscount.Text = Convert.ToString(dtBounds[0].PinDebitDiscountDef);
                //txtDebitMonFee.Text = Convert.ToString(dtBounds[0].DebitMonFeeDef);
                //lstDebitTransFee.SelectedItem.Text = Convert.ToString(dtBounds[0].DebitTransFeeDef);

                if (lstProcessorNames.SelectedItem.Text.ToLower().Contains("Sage"))
                {

                    lstDebitTransFee.Enabled = true;
                    if ((dt.Rows.Count > 0) && (!Convert.IsDBNull(dt[0].PinDebitDiscount)))
                    {
                        txtPinDebitDiscount.Text = Convert.ToString(dt[0].PinDebitDiscount);
                        txtDebitMonFee.Text = Convert.ToString(dt[0].DebitMonFee);
                        lstDebitTransFee.SelectedItem.Text = Convert.ToString(dt[0].DebitTransFee);
                    }
                    else if (dtBounds.Rows.Count > 0)
                    {
                        txtPinDebitDiscount.Text = Convert.ToString(dtBounds[0].PinDebitDiscountDef);
                        txtDebitMonFee.Text = Convert.ToString(dtBounds[0].DebitMonFeeDef);
                        lstDebitTransFee.SelectedItem.Text = Convert.ToString(dtBounds[0].DebitTransFeeDef);
                    }
                }
                else
                {
                    txtDebitTransFee.Enabled = true;
                    if ((dt.Rows.Count > 0) && (!Convert.IsDBNull(dt[0].PinDebitDiscount)))

                    {

                        txtPinDebitDiscount.Text = Convert.ToString(dt[0].PinDebitDiscount);
                        txtDebitMonFee.Text = Convert.ToString(dt[0].DebitMonFee);
                        txtDebitTransFee.Text = Convert.ToString(dt[0].DebitTransFee);
                    }
                    else if (dtBounds.Rows.Count > 0)
                    {
                        txtPinDebitDiscount.Text = Convert.ToString(dtBounds[0].PinDebitDiscountDef);
                        txtDebitMonFee.Text = Convert.ToString(dtBounds[0].DebitMonFeeDef);
                        txtDebitTransFee.Text = Convert.ToString(dtBounds[0].DebitTransFeeDef);
                    }
                }



            }
            else
            {
                txtPinDebitDiscount.Enabled = false;
                txtDebitMonFee.Enabled = false;
                if (lstProcessorNames.SelectedItem.Text.ToLower().Contains("Sage"))
                {

                    lstDebitTransFee.Enabled = false;

                }
                else
                {
                    txtDebitTransFee.Enabled = false;
                }
                //lstDebitTransFee.Enabled = false;
                txtPinDebitDiscount.Text = "";
                txtDebitMonFee.Text = "";
                lstDebitTransFee.SelectedItem.Text = "";
                txtDebitTransFee.Text = "";
            }

        }//end try
        catch (Exception err)
        {
            CreateLog Log = new CreateLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), err.Message);
            DisplayMessage("Error loading data.");
        }
    }//end check changed
    protected void chkCheckGuarantee_CheckedChanged(object sender, EventArgs e)
    {
        if (chkCheckGuarantee.Checked)
        {

            lstCheckServicePopulateItems();

            lstCheckService.Enabled = true;
            txtCGDiscRate.Enabled = true;
            txtCGMonFee.Enabled = true;
            txtCGMonMin.Enabled = true;
            txtCGTransFee.Enabled = true;
            if (lstGatewayNames.SelectedItem.Text.Contains("Optimal"))
                lstCheckService.SelectedIndex = lstCheckService.Items.IndexOf(lstCheckService.Items.FindByText("Direct Debit"));
            else if (lstGatewayNames.SelectedItem.Text.Contains("Authorize.net"))
            {
                lstCheckService.SelectedIndex = lstCheckService.Items.IndexOf(lstCheckService.Items.FindByText("eCheck.Net"));
                //pnlECheckRates.Visible = true;
            }
            else
                lstCheckService.SelectedIndex = lstCheckService.Items.IndexOf(lstCheckService.Items.FindByText("Sage EFT Check"));

            BoundsBL Bounds = new BoundsBL();
            DataSet ds = Bounds.GetCSBounds(lstCheckService.SelectedItem.Text);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                txtCGMonFee.Text = dr["CGMonFeeDef"].ToString().Trim();
                txtCGTransFee.Text = dr["CGTransFeeDef"].ToString().Trim();
                txtCGMonMin.Text = dr["CGMonMinDef"].ToString().Trim();
                txtCGDiscRate.Text = dr["CGDiscRateDef"].ToString().Trim();
            }//end if count not 0
        }
        else
        {
            lstCheckService.Enabled = false;
            txtCGDiscRate.Enabled = false;
            txtCGMonFee.Enabled = false;
            txtCGMonMin.Enabled = false;
            txtCGTransFee.Enabled = false;
            lstCheckService.SelectedIndex = lstCheckService.Items.IndexOf(lstCheckService.Items.FindByText(""));
            txtCGDiscRate.Text = "";
            txtCGMonFee.Text = "";
            txtCGMonMin.Text = "";
            txtCGTransFee.Text = "";
        }
    }
    protected void chkGiftCard_CheckedChanged(object sender, EventArgs e)
    {
        string Processor = lstProcessorNames.SelectedItem.Text.Trim();
        if (chkGiftCard.Checked)
        {
            //lstCheckServicePopulateItems();
            lstGCType.Items.Clear();
            lstGCType.Items.Add("Sage EFT Gift & Loyalty");
            lstGCType.Enabled = true;
            txtGCMonFee.Enabled = true;
            txtGCTransFee.Enabled = true;
            if (Processor.Trim().ToLower().Contains("intuit") || Processor.Trim().ToLower().Contains("ims"))
                lstGCType.SelectedIndex = lstGCType.Items.IndexOf(lstGCType.Items.FindByText("Sage EFT Gift & Loyalty"));
            //lstGCType.SelectedIndex = lstGCType.Items.IndexOf(lstGCType.Items.FindByText(""));
            else
                lstGCType.SelectedIndex = lstGCType.Items.IndexOf(lstGCType.Items.FindByText("Sage EFT Gift & Loyalty"));

            lstGCType.Items.Clear();
            lstGCType.Items.Add("Sage EFT Gift & Loyalty");

            //lstGCType.DataSource = testlist;

            LoadDefaultRateGiftCard(lstGCType.SelectedItem.Text.ToString().Trim());
        }
        else
        {
            lstGCType.Items.Clear();
            lstGCType.Items.Add("");
            lstGCType.Items.Add("Sage EFT Gift & Loyalty");
            lstGCType.Enabled = false;
            txtGCMonFee.Enabled = false;
            txtGCTransFee.Enabled = false;
            lstGCType.SelectedIndex = lstGCType.Items.IndexOf(lstGCType.Items.FindByText(""));
            lstGCType.Text = "";
            txtGCMonFee.Text = "";
            txtGCTransFee.Text = "";
        }
    }
    protected void chkPayroll_CheckedChanged(object sender, EventArgs e)
    {
        string Processor = lstProcessorNames.SelectedItem.Text.Trim();
        if (chkPayroll.Checked)
        {
            lstPayrollType.Enabled = true;
            if (Processor.Trim().ToLower().Contains("intuit payment solutions (quickbooks)") || Processor.Trim().ToLower().Contains("ims (quickbooks)"))
                lstPayrollType.SelectedIndex = lstPayrollType.Items.IndexOf(lstPayrollType.Items.FindByText("Intuit QuickBooks Payroll Assisted"));
            else
                lstPayrollType.SelectedIndex = lstPayrollType.Items.IndexOf(lstPayrollType.Items.FindByText("Intuit Payroll Full Service"));
        }
        else
        {
            //lstPayrollType.Items.Clear();
            lstPayrollType.Items.Add("");
            lstPayrollType.Enabled = false;
            lstPayrollType.SelectedIndex = lstPayrollType.Items.IndexOf(lstPayrollType.Items.FindByText(""));
            lstPayrollType.Text = "";
        }
    }
    protected void chkMerchantFunding_CheckedChanged(object sender, EventArgs e)
    {
        string Processor = lstProcessorNames.SelectedItem.Text.Trim();
        if (chkMerchantFunding.Checked)
        {
            txtCashDesired.Enabled = true;
            lstMCAType.Enabled = true;
            lstMCAType.SelectedIndex = lstMCAType.Items.IndexOf(lstMCAType.Items.FindByText("Business Financial Services"));

        }
        else
        {

            //lstMCAType.Items.Clear();
            lstMCAType.Items.Add("");
            txtCashDesired.Enabled = false;
            lstMCAType.Enabled = false;
            lstMCAType.SelectedValue = "";
            lstMCAType.Text = "";
            txtCashDesired.Text = "";

        }
    }
    protected void chkEBT_CheckedChanged(object sender, EventArgs e)
    {
        if (chkEBT.Checked)
        {
            txtEBTMonFee.Enabled = false;
            txtEBTTransFee.Enabled = true;
            txtEBTMonFee.Text = "0.00";
            OnlineAppProcessingBL Processing = new OnlineAppProcessingBL(AppId);
            PartnerDS.OnlineAppRatesDataTable dt = Processing.GetRates();
            if (dt.Rows.Count > 0)
            {

                txtEBTMonFee.Text = Convert.ToString(dt[0].EBTMonFee);
                txtEBTTransFee.Text = Convert.ToString(dt[0].EBTTransFee);
                txtEBTMonFee.Text = "0.00";
            }
        }
        else
        {
            txtEBTMonFee.Enabled = false;
            txtEBTTransFee.Enabled = false;

            //txtEBTMonFee.Text = "0.00";
            txtEBTTransFee.Text = "";
        }
    }
    protected void chkLease_CheckedChanged(object sender, EventArgs e)
    {
        if (chkLease.Checked)
        {
            lstLeaseCompany.Items.Clear();
            lstLeaseCompany.Items.Add("Northern Leasing Systems, Inc.");
            lstLeaseCompany.Enabled = true;
            txtLeasePayment.Enabled = true;
            lstLeaseTerm.Enabled = true;
            lstLeaseCompany.SelectedIndex = lstLeaseCompany.Items.IndexOf(lstLeaseCompany.Items.FindByText("Northern Leasing Systems, Inc."));
        }
        else
        {
            lstLeaseCompany.Items.Clear();
            lstLeaseCompany.Items.Add("");
            lstLeaseCompany.Items.Add("Northern Leasing Systems, Inc.");
            lstLeaseCompany.Enabled = false;
            txtLeasePayment.Enabled = false;
            lstLeaseTerm.Enabled = false;
            lstLeaseCompany.SelectedIndex = lstLeaseCompany.Items.IndexOf(lstLeaseCompany.Items.FindByText(""));
            txtLeasePayment.Text = "";
            lstLeaseTerm.SelectedIndex = lstLeaseTerm.Items.IndexOf(lstLeaseTerm.Items.FindByText(""));
        }
    }
}
