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

public partial class ViewRates : System.Web.UI.Page
{
    private static int AppId = 0;
    static int AcctType = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session.IsNewSession)
            Response.Redirect("/Application/default.aspx?SessionExpired=True");

        Response.CacheControl = "no-cache";
        Response.AddHeader("Pragma", "no-cache");
        Response.Expires = -1;

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
                Response.Redirect("default.aspx?Authentication=False");
            else
            {
                try
                {
                    DisplayRates();
                }
                catch (Exception err)
                {
                    CreateOnlineAppLog Log = new CreateOnlineAppLog();
                    Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + Session["AppId"].ToString() + " - " + "View Rates - " + err.Message);
                    SetErrorMessage("Error Processing Request. Please contact Technical Support: <a href=mailto:support@commercetech.com?Subject=Online App Error, App ID: " + Session["AppId"].ToString() + "> support@commercetech.com. </a>");
                }
            }
        }
    }

    //This function displays rates for this application
    public void DisplayRates()
    {
        try
        {
            AppId = Convert.ToInt32(Session["AppId"]);
            CardPCT CardPct = new CardPCT(AppId);

            //If Card Percentages is complete then show rates.
            if (AppId != 0)
            {
                OnlineAppProfile AppInfo = new OnlineAppProfile(AppId);
                DataSet dsNewApp = AppInfo.GetProfileData();
                DataTable dtNewApp = dsNewApp.Tables[0];
                if (dtNewApp.Rows.Count > 0)
                {
                    DataRow drNewApp = dtNewApp.Rows[0];
                    AcctType = Convert.ToInt32(drNewApp["AcctType"]);
                    if (AcctType == 1)
                    {
                        //Display message if Card Percentages page is not complete
                        if (CardPct.CheckCardPCTComplete() == 0)
                            SetErrorMessage("Please complete the Card Percentages Page to View Rates.");

                        //This is a merchant account only
                        pnlGateway.Visible = false;
                        pnlMerchant.Visible = true;
                        PopulateMerchantRates();
                    }
                    else if (AcctType == 2)
                    {
                        //This is a gateway account only
                        pnlGateway.Visible = true;
                        pnlMerchant.Visible = false;
                        PopulateGatewayRates();
                    }
                    else if (AcctType == 4)
                    {
                        //Display message if Card Percentages page is not complete
                        if (CardPct.CheckCardPCTComplete() == 0)
                            SetErrorMessage("Please complete the Card Percentages Page to View Rates.");

                        //This is a merchant and a gateway account
                        pnlGateway.Visible = true;
                        pnlMerchant.Visible = true;
                        PopulateMerchantRates();
                        PopulateGatewayRates();
                    }
                    PopulateAddlServices();
                    PopulateDBEBT();
                    PopulateProducts();

                    lblApplicationFee.Visible = false;
                    lblAppSetupFee.Visible = false;
                    lblGatewaySetupFee.Visible = false;
                }//end if count not 0            
            }//end if appid not 0
        }//end try
        catch (Exception err)
        {
            CreateOnlineAppLog Log = new CreateOnlineAppLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + Session["AppId"].ToString() + " - " + "View Rates - " + err.Message);
            SetErrorMessage("Error Processing Request. Please contact Technical Support: <a href=mailto:support@commercetech.com?Subject=Online App Error, App ID: " + Session["AppId"].ToString() + "> support@commercetech.com. </a>");
        }
    }//end function display rates

    //This function populates merchant account rates
    protected void PopulateMerchantRates()
    {
        ProcessingInfo Processing = new ProcessingInfo(AppId);
        DataSet dsProcessingInfo = Processing.GetProcessingInfo();
        SalesOppsBL SalesOppsFee = new SalesOppsBL(AppId);
        if (dsProcessingInfo.Tables[0].Rows.Count > 0)
        {
            DataRow drProcessingInfo = dsProcessingInfo.Tables[0].Rows[0];
            lblApplicationFee.Text = "$ " + SalesOppsFee.GetAppFeeSalesOpps();//drProcessingInfo["AppFee"].ToString().Trim();
            lblAppSetupFee.Text = "$ " + SalesOppsFee.GetAppSetupFeeSalesOpps();//drProcessingInfo["AppSetupFee"].ToString().Trim();
            if (drProcessingInfo["CardPresent"].ToString().Trim() == "CP")
                lblDiscountRate.Text = drProcessingInfo["DiscRateQualPres"].ToString().Trim() + " %";
            else
                lblDiscountRate.Text = drProcessingInfo["DiscRateQualPres"].ToString().Trim() + " %";
                //lblDiscountRate.Text = drProcessingInfo["DiscRateQualNP"].ToString().Trim() + " %";
            lblDebitRate.Text = drProcessingInfo["DiscRateQualDebit"].ToString().Trim() + " %";
            lblPerAuth.Text = "$ " + drProcessingInfo["TransactionFee"].ToString().Trim();
            lblMonMin.Text = "$ " + drProcessingInfo["MonMin"].ToString().Trim();
            lblTollFreeService.Text = "$ " + drProcessingInfo["CustServFee"].ToString().Trim();            
        }//end if count not 0
    }//end function populate merchant rates

    //This function populates gateway rates
    protected void PopulateGatewayRates()
    {
        Gateway GatewayInfo = new Gateway(AppId);
        DataSet dsGatewayInfo = GatewayInfo.GetGatewayInfo();
        SalesOppsBL SalesOppsFee = new SalesOppsBL(AppId);
        if (dsGatewayInfo.Tables[0].Rows.Count > 0)
        {
            DataRow drGatewayInfo = dsGatewayInfo.Tables[0].Rows[0];
            lblGateway.Text = drGatewayInfo["Gateway"].ToString().Trim();
            lblGatewaySetupFee.Text = "$ " + SalesOppsFee.GetGWSetupFeeSalesOpps();
            lblGatewayTransFee.Text = "$ " + drGatewayInfo["GatewayTransFee"].ToString().Trim();
            lblMonthlyGatewayAccess.Text = "$ " + drGatewayInfo["GatewayMonFee"].ToString().Trim();
        }//end if count not 0

        DataSet dsECheck = GatewayInfo.GetECheckInfo();
        if (dsECheck.Tables[0].Rows.Count > 0)
        {
            pnlECheck.Visible = true;
            DataRow drECheck = dsECheck.Tables[0].Rows[0];
            lblECheckMonMin.Text = "$ " + drECheck["ESIRMonMin"].ToString().Trim();
            lblECheckTransFee.Text = "$ " + drECheck["ESIRTransFee1"].ToString().Trim();
        } //end if count not 0
        else
            pnlECheck.Visible = false;
  

    }//end function populate gateway rates

    protected void PopulateDBEBT()
    {
        Style ValueLabel = new Style();
        ValueLabel.ForeColor = System.Drawing.Color.Black;
        ValueLabel.Font.Size = FontUnit.Point(8);
        ValueLabel.Font.Name = "Arial";

        Style ValueLabelHeader = new Style();
        ValueLabelHeader.ForeColor = System.Drawing.Color.FromArgb(0, 123, 121);
        ValueLabelHeader.Font.Size = FontUnit.Point(8);
        ValueLabelHeader.Font.Bold = true;
        ValueLabelHeader.Font.Name = "Arial";

        NewAppInfo DBEBT = new NewAppInfo(AppId);
        DataSet dsDBEBT = DBEBT.GetNewAppData();
        if (dsDBEBT.Tables[0].Rows.Count > 0)
        {
            DataRow dr = dsDBEBT.Tables[0].Rows[0];
            ProcessingInfo Processing = new ProcessingInfo(AppId);
            DataSet dsProcessingInfo = Processing.GetAddlServices();

            //Get Setup Fees from Sales Opps
            OnlineAppClassLibrary.SalesOppsBL SalesOpps = new OnlineAppClassLibrary.SalesOppsBL(AppId);
            if (dsProcessingInfo.Tables[0].Rows.Count > 0)
            {
                DataRow drProcessingInfo = dsProcessingInfo.Tables[0].Rows[0];

                TableRow tr = new TableRow();
                //tr.ID = "tr1";
                TableCell td;
                Label lblValue;
                //DropDownList lstValue;
                //Control myControl1;

                ProcessingInfo Proc = new ProcessingInfo(AppId);
                string CardPresent = Proc.ReturnCardPresent();

                if (Convert.ToBoolean(dr["OnlineDebit"]) || Convert.ToBoolean(dr["EBT"]))
                {
                    Control ControlHeader = tblAddlServices.FindControl("lblDBEBT");
                    if (ControlHeader == null)
                    {
                        tr = new TableRow();
                        Label lblHearder = new Label();
                        //lblHearder.Text = "Additional Services";
                        lblHearder.ID = "lblDBEBT";
                        td = new TableCell();
                        td.Attributes.Add("align", "center");
                        td.Attributes.Add("colspan", "2");
                        lblHearder.ApplyStyle(ValueLabel);
                        td.Controls.Add(lblHearder);
                        lblHearder.Font.Bold = true;
                        tr.Cells.Add(td);
                        tblDBEBT.Rows.Add(tr);
                    }
                }

                #region OnlineDebit
                if (CardPresent == "CP")
                {
                    if (Convert.ToBoolean(dr["OnlineDebit"]))
                    {
                        tr = new TableRow();
                        //Online Debit Header
                        lblValue = new Label();
                        lblValue.Text = "Online Debit";
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        td.Attributes.Add("colspan", "2");
                        lblValue.ApplyStyle(ValueLabel);
                        td.Controls.Add(lblValue);
                        lblValue.Font.Bold = true;
                        tr.Cells.Add(td);
                        tblDBEBT.Rows.Add(tr);

                        tr = new TableRow();
                        //DebitSetupFeeHeader
                        lblValue = new Label();
                        lblValue.Text = "Setup Fee";
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        td.Attributes.Add("width", "70%");
                        lblValue.ApplyStyle(ValueLabel);
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        //DebitSetupFee
                        lblValue = new Label();
                        lblValue.Text = "$ " + SalesOpps.GetODSetupFeeSalesOpps();
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        lblValue.ApplyStyle(ValueLabel);
                        lblValue.Font.Bold = true;
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        tblDBEBT.Rows.Add(tr);

                        tr = new TableRow();
                        //DebitMonFeeHeader
                        lblValue = new Label();
                        lblValue.Text = "Monthly Fee";
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        td.Attributes.Add("width", "70%");
                        lblValue.ApplyStyle(ValueLabel);
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        //DebitMonFee
                        lblValue = new Label();
                        lblValue.Text = "$ " + drProcessingInfo["DebitMonFee"].ToString().Trim();
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        lblValue.ApplyStyle(ValueLabel);
                        lblValue.Font.Bold = true;
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        tblDBEBT.Rows.Add(tr);

                        tr = new TableRow();
                        //DebitMonFeeHeader
                        lblValue = new Label();
                        lblValue.Text = "Transaction Fee";
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        td.Attributes.Add("width", "70%");
                        lblValue.ApplyStyle(ValueLabel);
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        //DebitMonFee
                        lblValue = new Label();
                        lblValue.Text = "$ " + drProcessingInfo["DebitTransFee"].ToString().Trim();
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        lblValue.ApplyStyle(ValueLabel);
                        lblValue.Font.Bold = true;
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        tblDBEBT.Rows.Add(tr);
                    }//end if online debit                    
                }//end if card present
                #endregion

                #region EBT
                if (CardPresent == "CP")
                {
                    if (Convert.ToBoolean(dr["EBT"]))
                    {
                        tr = new TableRow();
                        //EBT Header
                        lblValue = new Label();
                        lblValue.Text = "EBT";
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        td.Attributes.Add("colspan", "2");
                        lblValue.ApplyStyle(ValueLabel);
                        td.Controls.Add(lblValue);
                        lblValue.Font.Bold = true;
                        tr.Cells.Add(td);
                        tblDBEBT.Rows.Add(tr);

                        tr = new TableRow();
                        //EBTSetupFeeHeader                        
                        lblValue = new Label();
                        lblValue.Text = "Setup Fee";
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        td.Attributes.Add("width", "70%");
                        lblValue.ApplyStyle(ValueLabel);
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        //EBTSetupFee
                        lblValue = new Label();
                        lblValue.Text = "$ " + SalesOpps.GetEBTSetupFeeSalesOpps();
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        lblValue.ApplyStyle(ValueLabel);
                        lblValue.Font.Bold = true;
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        tblDBEBT.Rows.Add(tr);

                        tr = new TableRow();
                        //DebitMonFeeHeader
                        lblValue = new Label();
                        lblValue.Text = "Monthly Fee";
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        lblValue.ApplyStyle(ValueLabel);
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        //DebitMonFee
                        lblValue = new Label();
                        lblValue.Text = "$ " + drProcessingInfo["EBTMonFee"].ToString().Trim();
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        lblValue.ApplyStyle(ValueLabel);
                        lblValue.Font.Bold = true;
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        tblDBEBT.Rows.Add(tr);

                        tr = new TableRow();
                        //DebitMonFeeHeader
                        lblValue = new Label();
                        lblValue.Text = "Transaction Fee";
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        lblValue.ApplyStyle(ValueLabel);
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        //DebitMonFee
                        lblValue = new Label();
                        lblValue.Text = "$ " + drProcessingInfo["EBTTransFee"].ToString().Trim();
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        lblValue.ApplyStyle(ValueLabel);
                        lblValue.Font.Bold = true;
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        tblDBEBT.Rows.Add(tr);
                    }//end if EBT
                }//end if card present

                #endregion


            }
        }
    }

    //This function populates Additional services rates
    protected void PopulateAddlServices()
    {
        Style ValueLabel = new Style();
        ValueLabel.ForeColor = System.Drawing.Color.Black;
        ValueLabel.Font.Size = FontUnit.Point(8);
        ValueLabel.Font.Name = "Arial";

        Style ValueLabelHeader = new Style();
        ValueLabelHeader.ForeColor = System.Drawing.Color.FromArgb(86, 82, 148);
        ValueLabelHeader.Font.Size = FontUnit.Point(8);
        ValueLabelHeader.Font.Bold = true;
        ValueLabelHeader.Font.Name = "Arial";

        //Get Additional services info from online app new app
        NewAppInfo AddlServ = new NewAppInfo(AppId);
        DataSet dsAddl = AddlServ.GetNewAppData();
        if (dsAddl.Tables[0].Rows.Count > 0)
        {
            DataRow dr = dsAddl.Tables[0].Rows[0];
            ProcessingInfo Processing = new ProcessingInfo(AppId);
            DataSet dsProcessingInfo = Processing.GetAddlServices();

            //Get Setup Fees from Sales Opps
            SalesOppsBL SalesOpps = new SalesOppsBL(AppId);
            if (dsProcessingInfo.Tables[0].Rows.Count > 0)
            {
                DataRow drProcessingInfo = dsProcessingInfo.Tables[0].Rows[0];

                TableRow tr;
                TableCell td;
                Label lblValue;

                string CardPresent = Processing.ReturnCardPresent();

                if (Convert.ToBoolean(dr["OnlineDebit"]) || Convert.ToBoolean(dr["CheckServices"]) || Convert.ToBoolean(dr["GiftCard"])
                    || Convert.ToBoolean(dr["EBT"]))
                {
                    pnlAdditionalServices.Visible = true;                    
                }

                if (CardPresent == "CP")
                {
                    /*
                    if (Convert.ToBoolean(dr["OnlineDebit"]))
                    {
                        tr = new TableRow();
                        tr.Height = 10;
                        tblAddlServices.Rows.Add(tr);

                        tr = new TableRow();
                        //Online Debit Header
                        lblValue = new Label();
                        lblValue.Text = "Online Debit";
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        td.Attributes.Add("colspan", "2");
                        lblValue.ApplyStyle(ValueLabelHeader);
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);
                        tblAddlServices.Rows.Add(tr);

                        tr = new TableRow();
                        //DebitSetupFeeHeader
                        lblValue = new Label();
                        lblValue.Text = "Setup Fee";
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        td.Attributes.Add("width", "70%");
                        lblValue.ApplyStyle(ValueLabel);
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        //DebitSetupFee
                        lblValue = new Label();
                        lblValue.Text = "$ " + SalesOpps.GetODSetupFeeSalesOpps();
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        lblValue.ApplyStyle(ValueLabel);
                        lblValue.Font.Bold = true;
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        tblAddlServices.Rows.Add(tr);

                        tr = new TableRow();
                        //DebitMonFeeHeader
                        lblValue = new Label();
                        lblValue.Text = "Monthly Fee";
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        td.Attributes.Add("width", "70%");
                        lblValue.ApplyStyle(ValueLabel);
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        //DebitMonFee
                        lblValue = new Label();
                        lblValue.Text = "$ " + drProcessingInfo["DebitMonFee"].ToString().Trim();
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        lblValue.ApplyStyle(ValueLabel);
                        lblValue.Font.Bold = true;
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        tblAddlServices.Rows.Add(tr);

                        tr = new TableRow();
                        //DebitMonFeeHeader
                        lblValue = new Label();
                        lblValue.Text = "Transaction Fee";
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        td.Attributes.Add("width", "70%");
                        lblValue.ApplyStyle(ValueLabel);
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        //DebitMonFee
                        lblValue = new Label();
                        lblValue.Text = "$ " + drProcessingInfo["DebitTransFee"].ToString().Trim();
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        lblValue.ApplyStyle(ValueLabel);
                        lblValue.Font.Bold = true;
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        tblAddlServices.Rows.Add(tr);
                    }//end if online debit  */  

                    /*
                    if (Convert.ToBoolean(dr["EBT"]))
                    {
                        tr = new TableRow();
                        tr.Height = 10;
                        tblAddlServices.Rows.Add(tr);

                        tr = new TableRow();
                        //EBT Header
                        lblValue = new Label();
                        lblValue.Text = "EBT";
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        td.Attributes.Add("colspan", "2");
                        lblValue.ApplyStyle(ValueLabelHeader);
                        td.Controls.Add(lblValue);
                        lblValue.Font.Bold = true;
                        tr.Cells.Add(td);
                        tblAddlServices.Rows.Add(tr);

                        tr = new TableRow();
                        //EBTSetupFeeHeader                        
                        lblValue = new Label();
                        lblValue.Text = "Setup Fee";
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        td.Attributes.Add("width", "70%");
                        lblValue.ApplyStyle(ValueLabel);
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        //EBTSetupFee
                        lblValue = new Label();
                        lblValue.Text = "$ " + SalesOpps.GetEBTSetupFeeSalesOpps();
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        lblValue.ApplyStyle(ValueLabel);
                        lblValue.Font.Bold = true;
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        tblAddlServices.Rows.Add(tr);

                        tr = new TableRow();
                        //EBTMonFeeHeader
                        lblValue = new Label();
                        lblValue.Text = "Monthly Fee";
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        lblValue.ApplyStyle(ValueLabel);
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        //EBTMonFee
                        lblValue = new Label();
                        lblValue.Text = "$ " + drProcessingInfo["EBTMonFee"].ToString().Trim();
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        lblValue.ApplyStyle(ValueLabel);
                        lblValue.Font.Bold = true;
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        tblAddlServices.Rows.Add(tr);

                        tr = new TableRow();
                        //EBTMonFeeHeader
                        lblValue = new Label();
                        lblValue.Text = "Transaction Fee";
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        lblValue.ApplyStyle(ValueLabel);
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        //EBTMonFee
                        lblValue = new Label();
                        lblValue.Text = "$ " + drProcessingInfo["EBTTransFee"].ToString().Trim();
                        td = new TableCell();
                        td.Attributes.Add("align", "left");
                        lblValue.ApplyStyle(ValueLabel);
                        lblValue.Font.Bold = true;
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        tblAddlServices.Rows.Add(tr);
                    }//end if EBT*/
                }//end if card present
                                
                if (Convert.ToBoolean(dr["CheckServices"]))
                {
                    tr = new TableRow();
                    tr.Height = 10;
                    tblAddlServices.Rows.Add(tr);

                    tr = new TableRow();
                    //Check Guarantee Header
                    lblValue = new Label();
                    lblValue.Text = "Check Services";
                    td = new TableCell();
                    td.Attributes.Add("align", "left");
                    td.Attributes.Add("colspan", "2");
                    lblValue.ApplyStyle(ValueLabelHeader);
                    td.Controls.Add(lblValue);
                    lblValue.Font.Bold = true;
                    tr.Cells.Add(td);
                    tblAddlServices.Rows.Add(tr);

                    tr = new TableRow();
                    //CSSetupFeeHeader
                    lblValue = new Label();
                    lblValue.Text = "Setup Fee";
                    td = new TableCell();
                    td.Attributes.Add("align", "left");
                    td.Attributes.Add("width", "70%");
                    lblValue.ApplyStyle(ValueLabel);
                    td.Controls.Add(lblValue);
                    tr.Cells.Add(td);

                    //CSSetupFee
                    lblValue = new Label();
                    lblValue.Text = "$ " + SalesOpps.GetCSSetupFeeSalesOpps();
                    td = new TableCell();
                    td.Attributes.Add("align", "left");
                    lblValue.ApplyStyle(ValueLabel);
                    lblValue.Font.Bold = true;
                    td.Controls.Add(lblValue);
                    tr.Cells.Add(td);

                    tblAddlServices.Rows.Add(tr);

                    tr = new TableRow();
                    //DebitMonFeeHeader
                    lblValue = new Label();
                    lblValue.Text = "Discount Rate";
                    td = new TableCell();
                    td.Attributes.Add("align", "left");
                    td.Attributes.Add("width", "70%");
                    lblValue.ApplyStyle(ValueLabel);                    
                    td.Controls.Add(lblValue);
                    tr.Cells.Add(td);

                    //DebitMonFee
                    lblValue = new Label();
                    lblValue.Text = drProcessingInfo["CGDiscRate"].ToString().Trim() + " %";
                    td = new TableCell();
                    td.Attributes.Add("align", "left");
                    lblValue.ApplyStyle(ValueLabel);
                    lblValue.Font.Bold = true;
                    td.Controls.Add(lblValue);
                    tr.Cells.Add(td);

                    tblAddlServices.Rows.Add(tr);

                    tr = new TableRow();
                    //DebitMonFeeHeader
                    lblValue = new Label();
                    lblValue.Text = "Monthly Fee";
                    td = new TableCell();
                    td.Attributes.Add("align", "left");
                    td.Attributes.Add("width", "70%");
                    lblValue.ApplyStyle(ValueLabel);                    
                    td.Controls.Add(lblValue);
                    tr.Cells.Add(td);

                    //DebitMonFee
                    lblValue = new Label();
                    lblValue.Text = "$ " + drProcessingInfo["CGMonFee"].ToString().Trim();
                    td = new TableCell();
                    td.Attributes.Add("align", "left");
                    lblValue.ApplyStyle(ValueLabel);
                    lblValue.Font.Bold = true;
                    td.Controls.Add(lblValue);
                    tr.Cells.Add(td);

                    tblAddlServices.Rows.Add(tr);

                    tr = new TableRow();
                    //DebitMonFeeHeader
                    lblValue = new Label();
                    lblValue.Text = "Monthly Minimum";
                    td = new TableCell();
                    td.Attributes.Add("align", "left");
                    td.Attributes.Add("width", "70%");
                    lblValue.ApplyStyle(ValueLabel);                    
                    td.Controls.Add(lblValue);
                    tr.Cells.Add(td);

                    //DebitMonFee
                    lblValue = new Label();
                    lblValue.Text = "$ " + drProcessingInfo["CGMonMin"].ToString().Trim();
                    td = new TableCell();
                    td.Attributes.Add("align", "left");
                    lblValue.ApplyStyle(ValueLabel);
                    lblValue.Font.Bold = true;
                    td.Controls.Add(lblValue);
                    tr.Cells.Add(td);

                    tblAddlServices.Rows.Add(tr);

                    tr = new TableRow();
                    //DebitMonFeeHeader
                    lblValue = new Label();
                    lblValue.Text = "Transaction Fee";
                    td = new TableCell();
                    td.Attributes.Add("align", "left");
                    td.Attributes.Add("width", "70%");
                    lblValue.ApplyStyle(ValueLabel);                    
                    td.Controls.Add(lblValue);
                    tr.Cells.Add(td);

                    //DebitMonFee
                    lblValue = new Label();
                    lblValue.Text = "$ " + drProcessingInfo["CGTransFee"].ToString().Trim();
                    td = new TableCell();
                    td.Attributes.Add("align", "left");
                    lblValue.ApplyStyle(ValueLabel);
                    lblValue.Font.Bold = true;
                    td.Controls.Add(lblValue);
                    tr.Cells.Add(td);

                    tblAddlServices.Rows.Add(tr);
                }

                if (Convert.ToBoolean(dr["GiftCard"]))
                {
                    tr = new TableRow();
                    tr.Height = 10;
                    tblAddlServices.Rows.Add(tr);

                    tr = new TableRow();
                    //Gift Card Header
                    lblValue = new Label();
                    lblValue.Text = "Gift Card";
                    td = new TableCell();
                    td.Attributes.Add("align", "left");
                    td.Attributes.Add("colspan", "2");
                    lblValue.ApplyStyle(ValueLabelHeader);
                    td.Controls.Add(lblValue);
                    lblValue.Font.Bold = true;
                    tr.Cells.Add(td);
                    tblAddlServices.Rows.Add(tr);

                    tr = new TableRow();
                    //GCSetupFeeHeader
                    lblValue = new Label();
                    lblValue.Text = "Setup Fee";
                    td = new TableCell();
                    td.Attributes.Add("align", "left");
                    td.Attributes.Add("width", "70%");
                    lblValue.ApplyStyle(ValueLabel);
                    td.Controls.Add(lblValue);
                    tr.Cells.Add(td);

                    //GCSetupFee
                    lblValue = new Label();
                    lblValue.Text = "$ " + SalesOpps.GetGCSetupFeeSalesOpps();
                    td = new TableCell();
                    td.Attributes.Add("align", "left");
                    lblValue.ApplyStyle(ValueLabel);
                    lblValue.Font.Bold = true;
                    td.Controls.Add(lblValue);
                    tr.Cells.Add(td);

                    tblAddlServices.Rows.Add(tr);

                    tr = new TableRow();
                    //DebitMonFeeHeader
                    lblValue = new Label();
                    lblValue.Text = "Monthly Fee";
                    td = new TableCell();
                    td.Attributes.Add("align", "left");
                    td.Attributes.Add("width", "70%");
                    lblValue.ApplyStyle(ValueLabel);
                    td.Controls.Add(lblValue);
                    tr.Cells.Add(td);

                    //DebitMonFee
                    lblValue = new Label();
                    lblValue.Text = "$ " + drProcessingInfo["GCMonFee"].ToString().Trim();
                    td = new TableCell();
                    td.Attributes.Add("align", "left");
                    lblValue.ApplyStyle(ValueLabel);
                    lblValue.Font.Bold = true;
                    td.Controls.Add(lblValue);
                    tr.Cells.Add(td);

                    tblAddlServices.Rows.Add(tr);

                    tr = new TableRow();
                    //DebitMonFeeHeader
                    lblValue = new Label();
                    lblValue.Text = "Transaction Fee";
                    td = new TableCell();
                    td.Attributes.Add("align", "left");
                    td.Attributes.Add("width", "70%");
                    lblValue.ApplyStyle(ValueLabel);
                    td.Controls.Add(lblValue);
                    tr.Cells.Add(td);

                    //DebitMonFee
                    lblValue = new Label();
                    lblValue.Text = "$ " + drProcessingInfo["GCTransFee"].ToString().Trim();
                    td = new TableCell();
                    td.Attributes.Add("align", "left");
                    lblValue.ApplyStyle(ValueLabel);
                    lblValue.Font.Bold = true;
                    td.Controls.Add(lblValue);
                    tr.Cells.Add(td);

                    tblAddlServices.Rows.Add(tr);
                }//end if giftcard                              
            }//end if count not 0
        }//end if count not 0
    }

    protected void PopulateProducts()
    {
        Style ValueLabel = new Style();
        ValueLabel.ForeColor = System.Drawing.Color.Black;
        ValueLabel.Font.Size = FontUnit.Point(8);
        ValueLabel.Font.Name = "Arial";

        Style ValueLabelHeader = new Style();
        ValueLabelHeader.ForeColor = System.Drawing.Color.FromArgb(86, 82, 148);
        ValueLabelHeader.Font.Size = FontUnit.Point(8);
        ValueLabelHeader.Font.Bold = true;
        ValueLabelHeader.Font.Name = "Arial";

        //Get Products Sales Opps info from online app
        SalesOppsBL SalesOpps = new SalesOppsBL(AppId);
        DataSet ds = SalesOpps.GetProductSalesOpps();
        if (ds.Tables[0].Rows.Count > 0)
        {
            pnlProducts.Visible = true;

            TableRow tr;
            TableCell td;
            Label lblValue;
            
            tr = new TableRow();
            lblValue = new Label();
            lblValue.Text = "Other";
            td = new TableCell();
            td.Attributes.Add("align", "center");
            td.Attributes.Add("colspan", "2");
            lblValue.ApplyStyle(ValueLabelHeader);
            td.Controls.Add(lblValue);
            lblValue.Font.Bold = true;
            tr.Cells.Add(td);
            tblProducts.Rows.Add(tr);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow dr = ds.Tables[0].Rows[i];
                
                //Product name
                tr = new TableRow();
                lblValue = new Label();
                lblValue.Text = dr["Product"].ToString().Trim();
                td = new TableCell();
                td.Attributes.Add("align", "left");
                td.Attributes.Add("width", "70%");
                lblValue.ApplyStyle(ValueLabel);
                td.Controls.Add(lblValue);
                tr.Cells.Add(td);

                //Product Price
                lblValue = new Label();
                lblValue.Text = "$ " + dr["Price"].ToString().Trim();
                td = new TableCell();
                td.Attributes.Add("align", "left");
                lblValue.ApplyStyle(ValueLabel);
                lblValue.Font.Bold = true;
                td.Controls.Add(lblValue);
                tr.Cells.Add(td);

                tblProducts.Rows.Add(tr);
            }
        }//end if count not 0
    }//end if count not 0

    //This function closes the window when close button is clicked
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Write("<script language='javascript'> { self.close() }</script>");
    }//end function close click

    //This function displays error messages
    protected void SetErrorMessage(string errText)
    {
        lblError.Visible = true;
        lblError.Text = errText;
    }//end function set error message
}
