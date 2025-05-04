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

public partial class AddlServices : System.Web.UI.Page
{
    private static int AppId = 0;
    private static string strSW = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        chkGiftCard.Enabled = false;
        if (Session.IsNewSession)
            Response.Redirect("/Application/default.aspx?SessionExpired=True");

        Style errLabel = new Style();
        errLabel.BackColor = System.Drawing.Color.FromArgb(255, 192, 192);
        errLabel.ForeColor = System.Drawing.Color.Black;
        errLabel.BorderStyle = BorderStyle.Solid;
        errLabel.BorderWidth = Unit.Pixel(1);
        errLabel.Font.Size = FontUnit.Small;
        lblError.ApplyStyle(errLabel);

        Response.CacheControl = "no-cache";
        Response.AddHeader("Pragma", "no-cache");
        Response.Expires = -1;
        if ((Request.Params.Get("SW1") != null))
        {
            strSW = Request.Params.Get("SW1");
        }

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
                        Populate();
                    else
                    {
                        //SetErrorMessage("The status of your application prevents it from being edited");

                        Populate();
                        //btnContinue.Enabled = false;
                    }
                }//end try
                catch (Exception err)
                {
                    CreateOnlineAppLog Log = new CreateOnlineAppLog();
                    Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + Session["AppId"].ToString() + " - " + "AddlServices.aspx Page Load Error - " + err.Message);
                     SetErrorMessage("Error Processing Request. Please contact Technical Support: <a href='mailto:support@commercetech.com?Subject=Online Application Error'> support@commercetech.com. </a>.");                    
                }
            }//end if authenticated
        }//end if postback
    }

    //This function checks if the application is locked
    protected bool Locked()
    {
        //if (Session["Locked"].ToString().Trim() == "Yes")

        OnlineAppStatusTable Locked = new OnlineAppStatusTable();
        DataSet dsLocked = Locked.GetStatusInfo(AppId);
        string strGiftLocked = "";
        string strCheckLocked = "";
        string strMCALocked = "";
        string strPayrollLocked = "";
        string strLeaseLocked = "";

        if (dsLocked.Tables[0].Rows.Count > 0)
        {
            DataRow drSummary = dsLocked.Tables[0].Rows[0];
            strCheckLocked = drSummary["CheckServiceLocked"].ToString().Trim();
            strGiftLocked = drSummary["GiftcardLocked"].ToString().Trim();
            strMCALocked = drSummary["MCALocked"].ToString().Trim();
            strPayrollLocked = drSummary["PayrollLocked"].ToString().Trim();
            strLeaseLocked = drSummary["LeaseLocked"].ToString().Trim();

            if (strCheckLocked == "Yes" || strGiftLocked == "Yes" || strMCALocked == "Yes" || strPayrollLocked == "Yes" || strLeaseLocked == "Yes")
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
        else
            return false;
    }

    public void Populate()
    {
        NewAppInfo AddlServ = new NewAppInfo(AppId);
        DataSet ds = AddlServ.GetNewAppData();
        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr = ds.Tables[0].Rows[0];
            //pnlMerchantFunding.Visible = true;
            if ((Session["AcctType"].ToString() == "1") || (Session["AcctType"].ToString() == "4"))
            {
                ProcessingInfo CPCNP = new ProcessingInfo(AppId);
                string CardPresent = CPCNP.ReturnCardPresent();
                string Processor = CPCNP.ReturnProcessorName();

                if (CardPresent == "CP")
                {
                    pnlCPServices.Visible = true;
                    chkOnlineDebit.Checked = Convert.ToBoolean(dr["OnlineDebit"]);
                    chkEBT.Checked = Convert.ToBoolean(dr["EBT"]);
                    if (chkEBT.Checked)
                    {
                        //pnlUSDA.Visible = true;
                        lblUSDA.Visible = true;
                        txtUSDA.Visible = true;
                    }
                    else
                    {
                        //pnlUSDA.Visible = false;
                        lblUSDA.Visible = false;
                        txtUSDA.Visible = false;
                    }
                }
                else
                    pnlCPServices.Visible = false;
            }
            else
                pnlCPServices.Visible = false;
                
            pnlServices.Visible = true;
            chkCheckServices.Checked = Convert.ToBoolean(dr["CheckServices"]);
            //lstCheckServices.SelectedIndex = lstCheckServices.Items.IndexOf(lstCheckServices.Items.FindByText(AddlServ.ReturnCheckService()));

            if (chkCheckServices.Checked)
            {
                lblCheckServicesList.Visible = true;
                lstCheckServices.Visible = true;

                //if check service item is not in list then add it and select it
                System.Web.UI.WebControls.ListItem lstItem = new System.Web.UI.WebControls.ListItem();
                lstItem = lstCheckServices.Items.FindByValue(AddlServ.ReturnCheckService());
                if (lstItem == null)
                    lstCheckServices.Items.Add(AddlServ.ReturnCheckService());

                //select check service
                lstCheckServices.SelectedValue = lstCheckServices.Items.FindByValue(AddlServ.ReturnCheckService()).Value;
            }

            chkGiftCard.Checked = Convert.ToBoolean(dr["GiftCard"]);
            if (chkGiftCard.Checked)
            {
                lblGiftCardType.Visible = true;
                txtGiftCardType.Visible = true;
                txtGiftCardType.Text = AddlServ.ReturnGiftCardType();
            }
            else
            {
                lblGiftCardType.Visible = false;
                txtGiftCardType.Visible = false;
                txtGiftCardType.Text = "";
            }

            chkWirelessServices.Checked = Convert.ToBoolean(dr["Wireless"]);
            if (chkWirelessServices.Checked)
                pnlWireless.Visible = true;

            chkMerchantFunding.Checked = Convert.ToBoolean(dr["MerchantFunding"]);
            if (chkMerchantFunding.Checked)
            {
                lblMCAType.Visible = true;
                lstMCAType.Visible = true;
                lblMCACashDesired.Visible = true;
                txtCashDesired.Visible = true;
                //txtMCAType.Visible = true;
                //txtMCAType.Text = AddlServ.ReturnMCAType();
                //if MCAtype item is not in list then add it and select it
                System.Web.UI.WebControls.ListItem lstItem = new System.Web.UI.WebControls.ListItem();
                lstItem = lstMCAType.Items.FindByValue(AddlServ.ReturnMCAType());
                if (!Convert.IsDBNull(AddlServ.ReturnMCAAmount()))
                {
                    txtCashDesired.Text = Convert.ToString(AddlServ.ReturnMCAAmount());
                }
                else
                {
                    txtCashDesired.Text = "";
                }
                if (lstItem == null)
                    lstMCAType.Items.Add(AddlServ.ReturnMCAType());

                //select check service
                lstMCAType.SelectedValue = lstMCAType.Items.FindByValue(AddlServ.ReturnMCAType()).Value;
            }
            else
            {
                lblMCAType.Visible = false;
                lstMCAType.Visible = false;
                lblMCACashDesired.Visible = false;
                txtCashDesired.Visible = false;
                txtCashDesired.Text = "";
                lstMCAType.Text = "";
            }
            
            chkLease.Checked = Convert.ToBoolean(dr["Lease"]);
            if (chkLease.Checked)
            {
                pnlLease.Visible = false;
                //pnlLease.Visible = true;
                lblLeaseCompany.Visible = false;
                txtLeaseCompany.Visible = false;
                txtLeaseCompany.Text = AddlServ.ReturnLeaseCompany();
            }
            else
            {
                pnlLease.Visible = false;
                lblLeaseCompany.Visible = false;
                txtLeaseCompany.Visible = false;
                txtLeaseCompany.Text = "";
            }

            chkPayroll.Checked = Convert.ToBoolean(dr["Payroll"]);
            if (chkPayroll.Checked)
            {
                lblPayrollType.Visible = true;
                lstPayrollType.Visible = true;
                //txtPayrollType.Visible = true;
                //txtPayrollType.Text = AddlServ.ReturnPayrollType();
                System.Web.UI.WebControls.ListItem lstItem = new System.Web.UI.WebControls.ListItem();
                lstItem = lstPayrollType.Items.FindByValue(AddlServ.ReturnPayrollType());
                if (lstItem == null)
                    lstPayrollType.Items.Add(AddlServ.ReturnPayrollType());

                //select check service
                lstPayrollType.SelectedValue = lstPayrollType.Items.FindByValue(AddlServ.ReturnPayrollType()).Value;
            }
            else
            {
                lblPayrollType.Visible = false;
                lstPayrollType.Visible = false;
                lstPayrollType.Text = "";
            }

            OnlineAppStatusTable Locked = new OnlineAppStatusTable();
            DataSet dsLocked = Locked.GetStatusInfo(AppId);
            string strGiftLocked = "";
            string strCheckLocked = "";
            string strMCALocked = "";
            string strPayrollLocked = "";
            string strLeaseLocked = "";

            if (dsLocked.Tables[0].Rows.Count > 0)
            {
                DataRow drSummary = dsLocked.Tables[0].Rows[0];
                strCheckLocked = drSummary["CheckServiceLocked"].ToString().Trim();
                strGiftLocked = drSummary["GiftcardLocked"].ToString().Trim();
                strMCALocked = drSummary["MCALocked"].ToString().Trim();
                strPayrollLocked = drSummary["PayrollLocked"].ToString().Trim();
                strLeaseLocked = drSummary["LeaseLocked"].ToString().Trim();

                /*if (strCheckLocked == "Yes" || strGiftLocked == "Yes" || strMCALocked == "Yes" || strPayrollLocked == "Yes" || strLeaseLocked == "Yes")
                {
                    return true;
                }
                else
                {
                    return false;
                }*/

            }

            if (strCheckLocked == "Yes")
            {
                chkCheckServices.Enabled = false;
                lblCheckServicesList.Visible = false;
                lstCheckServices.Visible = false;
            }

            if (strGiftLocked == "Yes")
            {
                chkGiftCard.Enabled = false;
                lblGiftCardType.Visible = false;
                txtGiftCardType.Visible = false;
            }

            if (strMCALocked == "Yes")
            {
                chkMerchantFunding.Enabled = false;
                lblMCAType.Visible = false;
                lstMCAType.Visible = false;
                txtCashDesired.Visible = false;
                lblMCACashDesired.Visible = false;
            }

            if (strLeaseLocked == "Yes")
            {
                chkLease.Enabled = false;

                pnlLease.Visible = false;
                lblLeaseCompany.Visible = false;

            }

            if (strPayrollLocked == "Yes")
            {
                chkPayroll.Enabled = false;
                lblPayrollType.Visible = false;
                lstPayrollType.Visible = false;
            }
        }//end if count not 0 
    }//end function Populate

    protected void btnContinue_Click(object sender, EventArgs e)
    {
        try
        {
            NewAppInfo AddlServ = new NewAppInfo(AppId);
            AddlServ.UpdateAdditionalServices( chkGiftCard.Checked, chkMerchantFunding.Checked,
                    chkWirelessServices.Checked, chkCheckServices.Checked, chkPayroll.Checked, chkLease.Checked);
            
            //AddlServ.UpdateCheckServiceBit(chkCheckServices.Checked);

            OnlineAppStatusTable Locked = new OnlineAppStatusTable();
            DataSet dsLocked = Locked.GetStatusInfo(AppId);
            string strGiftLocked = "";
            string strCheckLocked = "";
            string strMCALocked = "";
            string strPayrollLocked = "";
            string strLeaseLocked = "";

            if (dsLocked.Tables[0].Rows.Count > 0)
            {
                DataRow drSummary = dsLocked.Tables[0].Rows[0];
                strCheckLocked = drSummary["CheckServiceLocked"].ToString().Trim();
                strGiftLocked = drSummary["GiftcardLocked"].ToString().Trim();
                strMCALocked = drSummary["MCALocked"].ToString().Trim();
                strPayrollLocked = drSummary["PayrollLocked"].ToString().Trim();
                strLeaseLocked = drSummary["LeaseLocked"].ToString().Trim();
            }

            if (chkGiftCard.Checked)
            {
                if (strGiftLocked == "No")
                {
                    AddlServ.UpdateGiftCardType(txtGiftCardType.Text.ToString().Trim());
                }
            }
            else
            {
                if (strGiftLocked == "No")
                {
                    AddlServ.UpdateGiftCardType("");
                }
            }

            if (chkPayroll.Checked)
            {
                if (strPayrollLocked == "No")
                {
                    AddlServ.UpdatePayrollType(lstPayrollType.SelectedItem.Text.ToString().Trim());
                }
            }
            else
            {
                if (strPayrollLocked == "No")
                {
                    AddlServ.UpdatePayrollType("");
                }
            }

            if (chkMerchantFunding.Checked)
            {
                if (strMCALocked == "No")
                {
                    AddlServ.UpdateMCAType(lstMCAType.SelectedItem.Text.ToString().Trim());
                    if (!Convert.IsDBNull(txtCashDesired.Text))
                    {
                        if (Convert.ToString(txtCashDesired.Text) != "")
                        {
                            AddlServ.UpdateMCAAmount(Convert.ToInt32(txtCashDesired.Text));
                        }
                        else
                        {
                            AddlServ.UpdateMCAAmount(" ");
                        }
                    }
                }
            }
            else
            {
                if (strMCALocked == "No")
                {
                    AddlServ.UpdateMCAType("");
                }
            }


            if (chkLease.Checked)
            {
                if (strLeaseLocked == "No")
                {
                    OAPDFBL NortherLeaseData = new OAPDFBL(AppId);
                    DataSet ds = NortherLeaseData.GetNorthernLeaseData();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow dr = ds.Tables[0].Rows[0];
                        AddlServ.InsertUpdateLeaseInfo(txtLeaseCompany.Text.ToString().Trim(), dr["LeasePayment"].ToString().Trim(), dr["LeaseTerm"].ToString().Trim());
                    }
                    else
                    {
                        AddlServ.InsertUpdateLeaseInfo(txtLeaseCompany.Text.ToString().Trim(), "", "");
                    }
                }
            }
            else
            {
                if (strLeaseLocked == "No")
                {
                    AddlServ.InsertUpdateLeaseInfo("", "", "");
                }
            }
            
            if ((Session["AcctType"].ToString() == "1") || (Session["AcctType"].ToString() == "4"))
            {                
                AddlServ.UpdateUSDANum(txtUSDA.Text.Trim());
            }

            //Insert Additional Services Sales Opps
            SalesOppsBL Sales = new SalesOppsBL(AppId);
            Sales.CreateAddlServSalesOpps(chkOnlineDebit.Checked, chkGiftCard.Checked,
                chkEBT.Checked, chkPayroll.Checked);
            
            //Insert Check Services Sales Opps
            Sales.CreateCheckServicesSalesOpps(chkCheckServices.Checked, lstCheckServices.SelectedItem.Text);

            //Insert Merchant Cash Advance Sales Opps
            Sales.CreateMCASalesOpps(chkMerchantFunding.Checked, lstMCAType.SelectedItem.Text);

            LogBL LogData = new LogBL(AppId);
            LogData.InsertLogData(Convert.ToInt32(Session["AffiliateID"]), "Additional Services Updated");

            /*string strRatesChanged = "False";
            if (Request.Params.Get("RatesChanged") != null)
                strRatesChanged = Request.Params.Get("RatesChanged");*/
            /*bool bQB = false;
            if ((Request.Params.Get("QB") != null))
                bQB = true;*/
            

            //Adding redirect for QB accounts since we no longer receive the PDF application.
            //We will collect info from merchant and then send them to Intuit to submit formal app.
            //if ( bQB )
           
         
                if (strSW == "QB")
                    //global::System.Windows.Forms.MessageBox.Show("Test");
                    Response.Redirect("Merchant/cardpct.aspx", false);
           
            //Response.Redirect("https://www.apps.commercetech.com/quickbooks", false);
            else if ((Session["AcctType"].ToString() == "1") || (Session["AcctType"].ToString() == "4"))
            {
                Response.Redirect("/Application/AddlServicesPDF.aspx", false);
            }
            else
            {
                Response.Redirect("/Application/AddlServicesPDF.aspx", false);
            }

                if (chkCheckServices.Checked)
                {
                    AddlServ.UpdateCheckService(lstCheckServices.SelectedItem.Text);
                    if (Convert.ToString(lstCheckServices.SelectedItem.Text).ToLower().Contains("echeck") )
                    {
                        if ((Session["AcctType"].ToString() == "1") || (Session["AcctType"].ToString() == "3"))
                        {
                            Response.Redirect("/Application/UpdateAcctGW.aspx", false);
                        }
                        else if ((Session["AcctType"].ToString() == "2"))
                        {
                            Response.Redirect("/Application/Merchant/cardpct.aspx", false);
                        }
                            else if ((Session["AcctType"].ToString() == "4"))
                        {
                            Response.Redirect("/Application/AddlServicesPDF.aspx", false);
                        }
                        //OnlineAppClassLibrary.Gateway GatewayInfo2 = new OnlineAppClassLibrary.Gateway(AppId);
                        //bool retVal = GatewayInfo2.UpdateGatewayInfo("Authorize.net", "15", "25", "0.10");
                    }
                    if (Convert.ToString(lstCheckServices.SelectedItem.Text).ToLower().Contains("eft"))
                    {
                        if ((Session["AcctType"].ToString() == "1") || (Session["AcctType"].ToString() == "3"))
                        {
                            Response.Redirect("/Application/UpdateAcctGW.aspx", false);
                        }
                        else if ((Session["AcctType"].ToString() == "2") || (Session["AcctType"].ToString() == "4"))
                        {
                            Response.Redirect("/Application/AddlServicesPDF.aspx", false);
                        }
                    }
                }
                else
                    AddlServ.UpdateCheckService("");
        }//end try
        catch (Exception err)
        {
            CreateOnlineAppLog Log = new CreateOnlineAppLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + Session["AppId"].ToString() + " - " + "Addl Services Error - " + err.Message);
            SetErrorMessage("Error Processing Request. Please contact Technical Support: <a href='mailto:support@commercetech.com?Subject=Online Application Error'> support@commercetech.com</a>.");
        }
    }

    //This function displays error messages
    protected void SetErrorMessage(string errText)
    {
        lblError.Visible = true;
        lblError.Text = errText;
    }//end function set error message

    protected void chkCheckServices_CheckedChanged(object sender, EventArgs e)
    {
        if (chkCheckServices.Checked)
        {
            lblCheckServicesList.Visible = true;
            lstCheckServices.Visible = true;

            //Get Gateway name and populate check service appropriately.
            Gateway GatewayInfo = new Gateway(AppId);
            DataSet dsGateway = GatewayInfo.GetGatewayInfo();
            if (dsGateway.Tables[0].Rows.Count > 0)
            {
                DataRow drGateway = dsGateway.Tables[0].Rows[0];
                string Gateway = drGateway["Gateway"].ToString();

                System.Web.UI.WebControls.ListItem lstItem = new System.Web.UI.WebControls.ListItem();
                if (Gateway.ToString().Trim().Contains("Optimal"))
                {
                    lstItem.Text = "Direct Debit";
                    lstItem.Value = "Direct Debit";
                    lstCheckServices.Items.Add(lstItem);
                    lstCheckServices.SelectedValue = lstCheckServices.Items.FindByValue(lstItem.Value).Value;
                }
                else if (Gateway.ToString().Trim().Contains("Authorize"))
                {
                    lstItem.Text = "eCheck.net";
                    lstItem.Value = "eCheck.net";
                    lstCheckServices.Items.Add(lstItem);
                    lstCheckServices.SelectedValue = lstCheckServices.Items.FindByValue(lstItem.Value).Value;
                }
                //else
                    //lstCheckServices.SelectedValue = lstCheckServices.Items.FindByValue("Sage EFT Check").Value;
            }
            else {
                //lstCheckServices.SelectedValue = lstCheckServices.Items.FindByValue("Sage EFT Check").Value;
            }
        
        }
        else
        {
            lblCheckServicesList.Visible = false;
            lstCheckServices.Visible = false;
        }
    }

    protected void chkGiftCard_CheckedChanged(object sender, EventArgs e)
    {
        ProcessingInfo CPCNP = new ProcessingInfo(AppId);
        string Processor = CPCNP.ReturnProcessorName();
        if (chkGiftCard.Checked)
        {
            lblGiftCardType.Visible = true;
            txtGiftCardType.Visible = true;
            if (Processor.Trim().ToLower().Contains("ims"))
                txtGiftCardType.Text = "Innovative Gift & Rewards";
            else if (Processor.Trim().ToLower().Contains("intuit"))
                txtGiftCardType.Text = "Innovative Gift & Rewards";
            else if (Processor.Trim().ToLower().Contains("ips"))
                txtGiftCardType.Text = "Innovative Gift & Rewards";
            else
                txtGiftCardType.Text = "Global eTelecom";
        }
        else
        {
            lblGiftCardType.Visible = false;
            txtGiftCardType.Visible = false;
        }
    }

    protected void chkEBT_CheckedChanged(object sender, EventArgs e)
    {
        if (chkEBT.Checked)
        {
            //pnlUSDA.Visible = true;
            lblUSDA.Visible = true;
            txtUSDA.Visible = true;
        }
        else
        {
            //pnlUSDA.Visible = false;
            lblUSDA.Visible = false;
            txtUSDA.Visible = false;
        }
    }

    protected void chkPayroll_CheckedChanged(object sender, EventArgs e)
    {
        ProcessingInfo CPCNP = new ProcessingInfo(AppId);
        string Processor = CPCNP.ReturnProcessorName();
        if (chkPayroll.Checked)
        {
            System.Web.UI.WebControls.ListItem lstItem = new System.Web.UI.WebControls.ListItem();
            lblPayrollType.Visible = true;
            lstPayrollType.Visible = true;
            if (Processor.Trim().ToLower().Contains("ims (quickbooks)"))
            {
                lstItem.Text = "Intuit QuickBooks Payroll Assisted";
                lstItem.Value = "Intuit QuickBooks Payroll Assisted";
                lstPayrollType.Items.Add(lstItem);
                lstPayrollType.SelectedValue = lstPayrollType.Items.FindByValue(lstItem.Value).Value;
                //txtPayrollType.Text = "Intuit QuickBooks Payroll Assisted";
            }
            else if (Processor.Trim().ToLower().Contains("intuit payment solutions (quickbooks)"))
            {
                lstItem.Text = "Intuit QuickBooks Payroll Assisted";
                lstItem.Value = "Intuit QuickBooks Payroll Assisted";
                lstPayrollType.Items.Add(lstItem);
                lstPayrollType.SelectedValue = lstPayrollType.Items.FindByValue(lstItem.Value).Value;
            }
            //txtPayrollType.Text = "Intuit QuickBooks Payroll Assisted";
            //else
                //lstPayrollType.SelectedValue = lstPayrollType.Items.FindByValue("Intuit Payroll Full Service").Value;  
                //txtPayrollType.Text = "Intuit Online Payroll";
        }
        else
        {
            lblPayrollType.Visible = false;
            lstPayrollType.Visible = false;
        }
    }

    protected void chkMCA_CheckedChanged(object sender, EventArgs e)
    {
        if (chkMerchantFunding.Checked)
        {
            System.Web.UI.WebControls.ListItem lstItem = new System.Web.UI.WebControls.ListItem();
            lblMCAType.Visible = true;
            lstMCAType.Visible = true;
            //lstItem.Text = "AdvanceMe, Inc.";
            //lstItem.Value = "AdvanceMe, Inc.";
            //lstMCAType.Items.Add(lstItem);
            //lstMCAType.SelectedValue = lstMCAType.Items.FindByValue("AdvanceMe, Inc.").Value;
            //txtMCAType.Text = "AdvanceMe, Inc.";
        }
        else
        {
            lblMCAType.Visible = false;
            lstMCAType.Visible = false;
        }
    }

    protected void chkLease_CheckedChanged(object sender, EventArgs e)
    {
        if (chkLease.Checked)
        {
            lblLeaseCompany.Visible = true;
            txtLeaseCompany.Visible = true;
            txtLeaseCompany.Text = "Northern Leasing Systems, Inc.";
        }
        else
        {
            lblLeaseCompany.Visible = false;
            txtLeaseCompany.Visible = false;
            txtLeaseCompany.Text = "";
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        try
        {
            string strRatesChanged = "False";
            if (Request.Params.Get("RatesChanged") != null)
                strRatesChanged = Request.Params.Get("RatesChanged");
            if ((Session["AcctType"].ToString() == "1") || (Session["AcctType"].ToString() == "4"))
            {
                Response.Redirect("/Application/Merchant/CardPct.aspx", false);
            }
            else
            {
                Response.Redirect("/Application/Gateway/companyinfo.aspx?RatesChanged=" + strRatesChanged, false);
            }
        }//end try
        catch (Exception err)
        {
            CreateOnlineAppLog Log = new CreateOnlineAppLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + Session["AppId"].ToString() + " - " + "Addl Services Back Button Error - " + err.Message);
            SetErrorMessage("Error Processing Request. Please contact Technical Support: <a href='mailto:support@commercetech.com?Subject=Online Application Error'> support@commercetech.com. </a>.");
        }
    }
}
