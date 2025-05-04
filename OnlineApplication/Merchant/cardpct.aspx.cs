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
using BusinessLayer;
//using BusinessLayer;
using DLPartner;
using DLPartner.PartnerDSTableAdapters;

public partial class Merchant_cardpct : System.Web.UI.Page
{
    private static int AppId = 0;
    private static int PID = 0;
    private static string strSW = string.Empty;
    private static string nextNavigatePage = "";
    private bool textChanged = false;

    private OnlineAppSalesOppsTableAdapter _OnlineAppSalesOppsAdapter = null;
    protected OnlineAppSalesOppsTableAdapter OnlineAppSalesOppsAdapter
    {
        get
        {
            if (_OnlineAppSalesOppsAdapter == null)
                _OnlineAppSalesOppsAdapter = new OnlineAppSalesOppsTableAdapter();

            return _OnlineAppSalesOppsAdapter;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        lstService.Enabled = false;
        lstKeyedWith.Enabled = false;

        lblError.Visible = false;
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

        AppId = Convert.ToInt32(Session["AppId"]);
        /*if ((Request.Params.Get("SW2") != null))
        {
            strSW = Request.Params.Get("SW2");
        }*/

        if (!IsPostBack)
        {
            if (!User.Identity.IsAuthenticated)
                Response.Redirect("/Application/default.aspx?Authentication=False");
            else
            {
               try
                {
                    if (!Locked())
                        PopulatePage();
                    else
                    {
                        PopulatePage();
                        SetErrorMessage("The status of your application prevents it from being edited");
                        btnEditInfo.Enabled = false;
                        btnSubmit.Enabled = false;
                        btnSave.Visible = false;
                        lblSave.Visible = false;
                    }
                }
                catch (Exception err)
                {
                    CreateOnlineAppLog Log = new CreateOnlineAppLog();
                    Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + Session["AppId"].ToString() + " - " + "Merchant cardpct.aspx Page Load Error - " + err.Message);
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

    //This function populates card percentages
    public void PopulatePage()
    {

        CreatePDF proc = new CreatePDF(AppId);
        string processor = proc.ReturnProcessorName();

        if (Session["AffiliateID"] != "")
            btnSave.Visible = true;

        /*ListItem lstItemBlank = new ListItem();
        lstItemBlank.Text = "";
        lstItemBlank.Value = "";
        lstInternet.Items.Add(lstItemBlank);
        lstSwiped.Items.Add(lstItemBlank);
        lstOther.Items.Add(lstItemBlank);
        lstMail.Items.Add(lstItemBlank);
        lstKeyedWith.Items.Add(lstItemBlank);
        lstKeyedWithout.Items.Add(lstItemBlank);
        lstRestaurant.Items.Add(lstItemBlank);
        lstRetail.Items.Add(lstItemBlank);
        lstService.Items.Add(lstItemBlank);*/

        ListItem lstItemInternet = new ListItem();
        lstItemInternet.Text = "";
        lstItemInternet.Value = "";
        lstInternet.Items.Add(lstItemInternet);

        for (int i = 0; i <= 100; i = i + 1)
        {
            ListItem lstItem = new ListItem();
            lstItem.Text = i.ToString();
            lstItem.Value = i.ToString();
            lstInternet.Items.Add(lstItem);
        }

        ListItem lstItemSwiped = new ListItem();
        lstItemSwiped.Text = "";
        lstItemSwiped.Value = "";
        lstSwiped.Items.Add(lstItemSwiped);

        for (int i = 0; i <= 100; i = i + 1)
        {
            ListItem lstItem = new ListItem();
            lstItem.Text = i.ToString();
            lstItem.Value = i.ToString();
            lstSwiped.Items.Add(lstItem);
        }

        ListItem lstItemOther = new ListItem();
        lstItemOther.Text = "";
        lstItemOther.Value = "";
        lstOther.Items.Add(lstItemOther);

        for (int i = 0; i <= 100; i = i + 1)
        {
            ListItem lstItem = new ListItem();
            lstItem.Text = i.ToString();
            lstItem.Value = i.ToString();
            lstOther.Items.Add(lstItem);             
        }

        ListItem lstItemMail = new ListItem();
        lstItemMail.Text = "";
        lstItemMail.Value = "";
        lstMail.Items.Add(lstItemMail);

        for (int i = 0; i <= 100; i = i + 1)
        {
            ListItem lstItem = new ListItem();
            lstItem.Text = i.ToString();
            lstItem.Value = i.ToString();
            lstMail.Items.Add(lstItem);            
        }

        ListItem lstItemKeyedWith = new ListItem();
        lstItemKeyedWith.Text = "";
        lstItemKeyedWith.Value = "";
        lstKeyedWith.Items.Add(lstItemKeyedWith);

        for (int i = 0; i <= 100; i = i + 1)
        {
            ListItem lstItem = new ListItem();
            lstItem.Text = i.ToString();
            lstItem.Value = i.ToString();
            lstKeyedWith.Items.Add(lstItem);
        }

        ListItem lstItemKeyedWithout = new ListItem();
        lstItemKeyedWithout.Text = "";
        lstItemKeyedWithout.Value = "";
        lstKeyedWithout.Items.Add(lstItemKeyedWithout);

        for (int i = 0; i <= 100; i = i + 1)
        {
            ListItem lstItem = new ListItem();
            lstItem.Text = i.ToString();
            lstItem.Value = i.ToString();
            lstKeyedWithout.Items.Add(lstItem);
        }

        ListItem lstItemRestaurant = new ListItem();
        lstItemRestaurant.Text = "";
        lstItemRestaurant.Value = "";
        lstRestaurant.Items.Add(lstItemRestaurant);

        for (int i = 0; i <= 100; i = i + 1)
        {
            ListItem lstItem = new ListItem();
            lstItem.Text = i.ToString();
            lstItem.Value = i.ToString();
            lstRestaurant.Items.Add(lstItem);
        }

        ListItem lstItemRetail = new ListItem();
        lstItemRetail.Text = "";
        lstItemRetail.Value = "";
        lstRetail.Items.Add(lstItemRetail);

        for (int i = 0; i <= 100; i = i + 1)
        {
            ListItem lstItem = new ListItem();
            lstItem.Text = i.ToString();
            lstItem.Value = i.ToString();
            lstRetail.Items.Add(lstItem);
        }

        ListItem lstItemService = new ListItem();
        lstItemService.Text = "";
        lstItemService.Value = "";
        lstService.Items.Add(lstItemService);

        for (int i = 0; i <= 100; i = i + 1)
        {
            ListItem lstItem = new ListItem();
            lstItem.Text = i.ToString();
            lstItem.Value = i.ToString();
            lstService.Items.Add(lstItem);
        }

        /*for (int i = 0; i <= 100; i = i + 1)
        {
            ListItem lstItem = new ListItem();
            lstItem.Text = i.ToString();
            lstItem.Value = i.ToString();
            lstService.Items.Add(lstItem);
        }*/

        CardPCT CardPCTInfo = new CardPCT(AppId);
        DataSet dsCardPCTInfo = CardPCTInfo.GetCardPCT();
        if (dsCardPCTInfo.Tables[0].Rows.Count > 0)
        {
            DataRow drCardPCT = dsCardPCTInfo.Tables["OnlineAppCardPCT"].Rows[0];

            if (Convert.IsDBNull(drCardPCT["BusinessPctService"]))
            {
                lstService.SelectedItem.Value = "0";
                lstService.SelectedItem.Text = "0";
            }
            else if (Convert.ToString(drCardPCT["BusinessPctService"]) == "")
            {
                lstService.SelectedItem.Value = "0";
                lstService.SelectedItem.Text = "0";
            }
            else if (Convert.ToString(drCardPCT["BusinessPctService"]) != "")
            {
                lstService.SelectedItem.Value = Convert.ToString(drCardPCT["BusinessPctService"]).Trim();
                lstService.SelectedItem.Text = Convert.ToString(drCardPCT["BusinessPctService"]).Trim();
            }

            if (Convert.IsDBNull(drCardPCT["BusinessPctOther"]))
            {
                lstOther.SelectedItem.Value = "0";
                lstOther.SelectedItem.Text = "0";
            }
            else if (Convert.ToString(drCardPCT["BusinessPctOther"]) == "")
            {
                lstOther.SelectedItem.Value = "0";
                lstOther.SelectedItem.Text = "0";
            }
            else if (Convert.ToString(drCardPCT["BusinessPctOther"]) != "")
            {
                lstOther.SelectedItem.Value = Convert.ToString(drCardPCT["BusinessPctService"]).Trim();
                lstOther.SelectedItem.Text = Convert.ToString(drCardPCT["BusinessPctService"]).Trim();
            }

            if (Convert.IsDBNull(drCardPCT["ProcessPctKeyedwImprint"]))
            {
                lstKeyedWith.SelectedItem.Value = "0";
                lstKeyedWith.SelectedItem.Text = "0";
            }
            else if (Convert.ToString(drCardPCT["ProcessPctKeyedwImprint"]) == "")
            {
                lstKeyedWith.SelectedItem.Value = "0";
                lstKeyedWith.SelectedItem.Text = "0";
            }
            else if (Convert.ToString(drCardPCT["ProcessPctKeyedwImprint"]) != "")
            {
                lstKeyedWith.SelectedItem.Value = Convert.ToString(drCardPCT["ProcessPctKeyedwImprint"]).Trim();
                lstKeyedWith.SelectedItem.Text = Convert.ToString(drCardPCT["ProcessPctKeyedwImprint"]).Trim();
            }

            
            lstInternet.SelectedValue = drCardPCT["BusinessPctInternet"].ToString().Trim();
            lstMail.SelectedValue = drCardPCT["BusinessPctMailOrder"].ToString().Trim();
            lstRestaurant.SelectedValue = drCardPCT["BusinessPctRestaurant"].ToString().Trim();
            lstOther.SelectedValue = drCardPCT["BusinessPctOther"].ToString().Trim();
            lstRetail.SelectedValue = drCardPCT["BusinessPctRetail"].ToString().Trim();
            
            lstSwiped.SelectedValue = drCardPCT["ProcessPctSwiped"].ToString().Trim();
            //lstKeyedWith.SelectedValue = drCardPCT["ProcessPctKeyedwImprint"].ToString().Trim();
            lstKeyedWithout.SelectedValue = drCardPCT["ProcessPctKeyedwoImprint"].ToString().Trim();
            /*int index = lstInternet.Items.IndexOf(lstInternet.Items.FindByText(drCardPCT["BusinessPctInternet"].ToString().Trim()));
            lstInternet.SelectedIndex = index;
            index = lstMail.Items.IndexOf(lstMail.Items.FindByText(drCardPCT["BusinessPctMailOrder"].ToString().Trim()));
            lstMail.SelectedIndex = index;
            index = lstRestaurant.Items.IndexOf(lstRestaurant.Items.FindByText(drCardPCT["BusinessPctRestaurant"].ToString().Trim()));
            lstRestaurant.SelectedIndex = index;
            index = lstOther.Items.IndexOf(lstOther.Items.FindByText(drCardPCT["BusinessPctOther"].ToString().Trim()));
            lstOther.SelectedIndex = index;
            index = lstRetail.Items.IndexOf(lstRetail.Items.FindByText(drCardPCT["BusinessPctRetail"].ToString().Trim()));
            lstRetail.SelectedIndex = index;
            index = lstService.Items.IndexOf(lstService.Items.FindByText(drCardPCT["BusinessPctService"].ToString().Trim()));
            lstService.SelectedIndex = index;
            index = lstSwiped.Items.IndexOf(lstSwiped.Items.FindByText(drCardPCT["ProcessPctSwiped"].ToString().Trim()));
            lstSwiped.SelectedIndex = index;
            index = lstKeyedWith.Items.IndexOf(lstKeyedWith.Items.FindByText(drCardPCT["ProcessPctKeyedwImprint"].ToString().Trim()));
            lstKeyedWith.SelectedIndex = index;
            index = lstKeyedWithout.Items.IndexOf(lstKeyedWithout.Items.FindByText(drCardPCT["ProcessPctKeyedwoImprint"].ToString().Trim()));
            lstKeyedWithout.SelectedIndex = index;*/

        }
        else {
            lstService.SelectedItem.Value = "0";
            lstService.SelectedItem.Text = "0";
            lstOther.SelectedItem.Value = "0";
            lstOther.SelectedItem.Text = "0";
            lstKeyedWith.SelectedItem.Value = "0";
            lstKeyedWith.SelectedItem.Text = "0";
        }
        
        /*else {
            lstInternet.Text = " ";
            lstMail.Text = " ";
            lstRestaurant.Text  = " ";
            lstOther.Text = " ";
            lstRetail.Text = " ";
            lstService.Text = " ";
            lstSwiped.Text = " ";
            lstKeyedWith.Text = " ";
            lstKeyedWithout.Text = " ";
        }*/
        //end if count not 0

        //disable all fields when Card Percentages are complete
        PID = Convert.ToInt32(Session["PID"]);
        AppId = Convert.ToInt32(Session["AppId"]);
        CardPCT CardPct = new CardPCT(AppId);
        if (CardPct.CheckCardPCTComplete() > 0)
        {
            lstInternet.Enabled = false;
            lstMail.Enabled = false;
            lstRestaurant.Enabled = false;
            lstOther.Enabled = false;
            lstRetail.Enabled = false;
            lstService.Enabled = false;
            lstSwiped.Enabled = false;
            lstKeyedWith.Enabled = false;
            lstKeyedWithout.Enabled = false;
            lnkEditInfo.Visible = true;
        }
        else
        {
            lblHeader.Visible = true;
            lblEdit.Visible = false;
            btnEditInfo.Visible = false;
            lstInternet.Enabled = true;
            lstMail.Enabled = true;
            lstRestaurant.Enabled = true;
            lstOther.Enabled = false;
            lstRetail.Enabled = true;
            lstService.Enabled = false;
            lstSwiped.Enabled = true;
            lstKeyedWith.Enabled = false;
            lstKeyedWithout.Enabled = true;
            lnkEditInfo.Visible = false;
        }

        if ((PID == 178) || (PID == 182) || (PID == 243) || (PID == 247) || (PID == 196)|| (PID == 190))
        {
            lstSwiped.SelectedItem.Text = "0";
            lstSwiped.SelectedItem.Value = "0";
            lstSwiped.Enabled = false;
            lstRetail.SelectedItem.Text = "0";
            lstRetail.SelectedItem.Value = "0";
            lstRetail.Enabled = false;
        }
        else if (!Convert.IsDBNull(processor))
        {
            if ((Convert.ToString(processor).Contains("WorldPay")) || (Convert.ToString(processor).Contains("Optimal-Cal")) || (Convert.ToString(processor).Contains("Optimal-Barclays")) || (Convert.ToString(processor).Contains("Payvision-B+S")) || (Convert.ToString(processor).Contains("Optimal-St. Kitts")) || (Convert.ToString(processor).Contains("Optimal-Canada")))
            {
                lstSwiped.SelectedItem.Text = "0";
                lstSwiped.SelectedItem.Value = "0";
                lstSwiped.Enabled = false;
                lstRetail.SelectedItem.Text = "0";
                lstRetail.SelectedItem.Value = "0";
                lstRetail.Enabled = false;
            }
        }


    }//end function populatepage

    protected void textChangedEventHandler(object sender, EventArgs e)
    {
        textChanged = true;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if ((lstSwiped.SelectedItem.Value != "") && (lstKeyedWith.SelectedItem.Value != "") && (lstKeyedWithout.SelectedItem.Value != "") && (lstService.SelectedItem.Value != "") && (lstRetail.SelectedItem.Value != "") && (lstRestaurant.SelectedItem.Value != "") && (lstMail.SelectedItem.Value != "") && (lstInternet.SelectedItem.Value != "") && (lstOther.SelectedItem.Value != ""))
        {
            int KeyedPercentages = (Convert.ToInt32(lstKeyedWith.SelectedItem.Value.ToString())) + (Convert.ToInt32(lstKeyedWithout.SelectedItem.Value.ToString()));
            int InternetPercentages = (Convert.ToInt32(lstInternet.SelectedItem.Value.ToString()));
            int MailPercentages = (Convert.ToInt32(lstMail.SelectedItem.Value.ToString()));
            int ProcessingPercentages = (Convert.ToInt32(lstKeyedWith.SelectedItem.Value.ToString())) + (Convert.ToInt32(lstKeyedWithout.SelectedItem.Value.ToString())) + (Convert.ToInt32(lstSwiped.SelectedItem.Value.ToString()));
            int BusinessPercentages = (Convert.ToInt32(lstService.SelectedItem.Value.ToString())) + (Convert.ToInt32(lstInternet.SelectedItem.Value.ToString())) + (Convert.ToInt32(lstRetail.SelectedItem.Value.ToString())) +
                (Convert.ToInt32(lstMail.SelectedItem.Value.ToString())) + (Convert.ToInt32(lstRestaurant.SelectedItem.Value.ToString())) + (Convert.ToInt32(lstOther.SelectedItem.Value.ToString()));

            bool bitComplete;
            if ((ProcessingPercentages < 100) || (ProcessingPercentages > 100) || (BusinessPercentages < 100) || (BusinessPercentages > 100) || ((InternetPercentages + MailPercentages) > KeyedPercentages))
            {
                bitComplete = false;
            }
            else
            {
                bitComplete = true;
            }


            ValidateSummary.Enabled = false;

            CardPCT CardPCTInfo = new CardPCT(AppId);
            bool retVal = CardPCTInfo.UpdateCardPCT(lstRetail.SelectedItem.Text.ToString().Trim(), lstRestaurant.SelectedItem.Text.ToString().Trim(),
                lstMail.SelectedItem.Text.ToString().Trim(), lstInternet.SelectedItem.Text.ToString().Trim(), lstSwiped.SelectedItem.Text.ToString().Trim(),
                lstKeyedWith.SelectedItem.Text.ToString().Trim(), lstKeyedWithout.SelectedItem.Text.ToString().Trim(), lstService.SelectedItem.Text.ToString().Trim(),
                lstOther.SelectedItem.Text.ToString().Trim(), bitComplete);

            CommonFunctions GeneralInfo = new CommonFunctions(AppId);
            //GeneralInfo.UpdateLastModified();
            GeneralInfo.SetPageCount();
            GeneralInfo.SetGWPageCount();
            if (textChanged == true)
            {
                GeneralInfo.UpdateLastModified();
                LogBL LogData = new LogBL(AppId);
                LogData.InsertLogData(Convert.ToInt32(Session["AffiliateID"]), "Merchant Card Percentages Information Updated");
            }

            //SetErrorMessage("Information saved. You may proceed to the next page by navigating through the top menu.");

            /*if (retVal)
            {
                LogBL LogData = new LogBL(AppId);
                LogData.InsertLogData(Convert.ToInt32(Session["AffiliateID"]), "Merchant Card Percentages Information Updated");
            }*/
        }
        else
        {

            bool bitComplete = false;

            ValidateSummary.Enabled = false;

            CardPCT CardPCTInfo = new CardPCT(AppId);
            bool retVal = CardPCTInfo.UpdateCardPCT(lstRetail.SelectedItem.Text.ToString().Trim(), lstRestaurant.SelectedItem.Text.ToString().Trim(),
                lstMail.SelectedItem.Text.ToString().Trim(), lstInternet.SelectedItem.Text.ToString().Trim(), lstSwiped.SelectedItem.Text.ToString().Trim(),
                lstKeyedWith.SelectedItem.Text.ToString().Trim(), lstKeyedWithout.SelectedItem.Text.ToString().Trim(), lstService.SelectedItem.Text.ToString().Trim(),
                lstOther.SelectedItem.Text.ToString().Trim(), bitComplete);

            CommonFunctions GeneralInfo = new CommonFunctions(AppId);
            //GeneralInfo.UpdateLastModified();
            GeneralInfo.SetPageCount();
            GeneralInfo.SetGWPageCount();
            if (textChanged == true)
            {
                GeneralInfo.UpdateLastModified();
                LogBL LogData = new LogBL(AppId);
                LogData.InsertLogData(Convert.ToInt32(Session["AffiliateID"]), "Merchant Card Percentages Information Updated");
            }

            //SetErrorMessage("Information saved. You may proceed to the next page by navigating through the top menu.");

            /*if (retVal)
            {
                LogBL LogData = new LogBL(AppId);
                LogData.InsertLogData(Convert.ToInt32(Session["AffiliateID"]), "Merchant Card Percentages Information Saved");
            }*/
        }

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

        ValidateSummary.Enabled = true;
        if ((lstSwiped.SelectedItem.Value != "") && (lstKeyedWith.SelectedItem.Value != "") && (lstKeyedWithout.SelectedItem.Value != "") && (lstService.SelectedItem.Value != "") && (lstRetail.SelectedItem.Value != "") && (lstRestaurant.SelectedItem.Value != "") && (lstMail.SelectedItem.Value != "") && (lstInternet.SelectedItem.Value != "") && (lstOther.SelectedItem.Value != ""))
        {
            int KeyedPercentages = (Convert.ToInt32(lstKeyedWith.SelectedItem.Value.ToString())) + (Convert.ToInt32(lstKeyedWithout.SelectedItem.Value.ToString()));
            int InternetPercentages = (Convert.ToInt32(lstInternet.SelectedItem.Value.ToString()));
            int MailPercentages = (Convert.ToInt32(lstMail.SelectedItem.Value.ToString()));
            int ProcessingPercentages = (Convert.ToInt32(lstKeyedWith.SelectedItem.Value.ToString())) + (Convert.ToInt32(lstKeyedWithout.SelectedItem.Value.ToString())) + (Convert.ToInt32(lstSwiped.SelectedItem.Value.ToString()));
            int BusinessPercentages = (Convert.ToInt32(lstService.SelectedItem.Value.ToString())) + (Convert.ToInt32(lstInternet.SelectedItem.Value.ToString())) + (Convert.ToInt32(lstRetail.SelectedItem.Value.ToString())) +
                (Convert.ToInt32(lstMail.SelectedItem.Value.ToString())) + (Convert.ToInt32(lstRestaurant.SelectedItem.Value.ToString())) + (Convert.ToInt32(lstOther.SelectedItem.Value.ToString()));
            if ((ProcessingPercentages < 100) || (ProcessingPercentages > 100))
            {
                SetErrorMessage("Please make sure your Processing Percentages sum up to 100%");
            }
            else if ((BusinessPercentages < 100) || (BusinessPercentages > 100))
            {
                SetErrorMessage("Please make sure your Business Percentages sum up to 100%");
            }
            else if ((InternetPercentages + MailPercentages) > KeyedPercentages)
            {
                SetErrorMessage("Your combined Mail/Phone and Internet Percentage cannot be greater than your combined Keyed Percentages of " + KeyedPercentages.ToString() + "\n%. Please adjust either your Processing Percentages, Mail/Phone or Internet Percentages.");
            }
            else
            {
                string bRatesChanged = "false";
                bool bCP = false;

                try
                {
                    //Change percentages
                    /*CardPCT CardPCTInfo = new CardPCT(AppId);
                    bool retVal = CardPCTInfo.UpdateCardPCT(lstRetail.SelectedItem.Text.ToString().Trim(), lstRestaurant.SelectedItem.Text.ToString().Trim(),
                        lstMail.SelectedItem.Text.ToString().Trim(), lstInternet.SelectedItem.Text.ToString().Trim(), lstSwiped.SelectedItem.Text.ToString().Trim(),
                        lstKeyedWith.SelectedItem.Text.ToString().Trim(), lstKeyedWithout.SelectedItem.Text.ToString().Trim(), lstService.SelectedItem.Text.ToString().Trim(),
                        lstOther.SelectedItem.Text.ToString().Trim(), true);

                    if (retVal)
                    {
                        LogBL LogData = new LogBL(AppId);
                        LogData.InsertLogData(Convert.ToInt32(Session["AffiliateID"]), "Merchant Card Percentages Information Updated");
                    }*/

                    /*OnlineAppProcessingBL Processing = new OnlineAppProcessingBL(AppId);
                    PartnerDS.OnlineAppRatesDataTable dt = Processing.GetRates();*/
                    
                    bool retVal = false;
                    NewAppInfo NewApp = new NewAppInfo(AppId);

                    NewAppTable NewAppTable = new NewAppTable();
                    int bitRate = NewAppTable.GetRatesUpdatedBit(AppId);

                    string strCardPresent = NewAppTable.GetOnlineAppCardPresent(AppId);

                    int OLDPID = NewApp.ReturnPID();

                    PackageInfo newPackagePID = new PackageInfo();
                    int PID = newPackagePID.ReturnNewPID(AppId, Convert.ToInt32(lstSwiped.SelectedItem.Value.Trim()));
                    //if (OLDPID != PID)
                    {
                        bRatesChanged = "true";

                        NewApp.UpdatePID(PID);

                        string OnlineDebitMonthFee = "";
                        string OnlineDebitTransFee = "";
                        string EBTMonthFee = "";
                        string EBTTransFee = "";

                        PackageInfo Package = new PackageInfo();
                        DataSet dsPackageInfo = Package.GetPackageInfo(PID);
                        DataTable dtPackageInfo = dsPackageInfo.Tables[0];
                        if (dtPackageInfo.Rows.Count > 0)
                        {
                            int AcctType = Convert.ToInt32(Session["AcctType"]);
                            DataRow drPackageInfo = dtPackageInfo.Rows[0];
                            if (drPackageInfo["CardPresent"].ToString().Trim() == "CP")
                                bCP = true;

                            //Test: Create and delete old gwy Sales opps

                            if (AcctType == 4)
                            {

                                //Insert Gateway sales opp
                                //OnlineAppClassLibrary.SalesOppsBL SalesOppGW = new OnlineAppClassLibrary.SalesOppsBL(AppId);
                                //retVal = SalesOppGW.CreateSalesOppsGW();

                                //PackageInfo Package = new PackageInfo();
                                //DataSet dsPackageInfo = Package.GetPackageInfo(PID);
                                //DataTable dtPackageInfo = dsPackageInfo.Tables[0];
                                //DataRow drPackageInfo = dtPackageInfo.Rows[0];

                                Gateway GWRates = new Gateway(AppId);
                                DataSet dsGateway = GWRates.GetGatewayInfo();
                                if (dsGateway.Tables[0].Rows.Count > 0)
                                {
                                    DataRow drGateway = dsGateway.Tables[0].Rows[0];
                                    /*if (drPackageInfo["Gateway"].ToString().Trim() != drGateway["Gateway"].ToString().Trim())
                                    {
                                        //BusinessLayer.SalesOppsBL SalesOpps = new BusinessLayer.SalesOppsBL();
                                        PartnerDS.OnlineAppSalesOppsDataTable dtGwySalesOpp = OnlineAppSalesOppsAdapter.GetData(Convert.ToInt16(AppId));
                                        //PartnerDS.OnlineAppSalesOppsDataTable dtGwySalesOpp = PartnerDS.OnlineAppSalesOppsTableAdapter.GetData(Convert.ToInt16(AppId));
                                        if (dtGwySalesOpp.Rows.Count > 0)
                                        {
                                            string GwySalesOppID = "";
                                            for (int i = 0; i < dtGwySalesOpp.Rows.Count; i++)
                                            {
                                                DataRow drGwySalesOpp = dtGwySalesOpp.Rows[i];

                                                //if ((drGwySalesOpp["Product"].ToString().Trim()) == (drGateway["Gateway"].ToString().Trim()))
                                                GwySalesOppID = drGwySalesOpp["ID"].ToString().Trim();
                                                SalesOppDL GwySalesOppsDL = new SalesOppDL();
                                                OnlineAppClassLibrary.Gateway GatewayInfo = new OnlineAppClassLibrary.Gateway(AppId);
                                                retVal = GatewayInfo.UpdateGatewayInfo(drPackageInfo["Gateway"].ToString().Trim(),
                                            drPackageInfo["GatewayMonFee"].ToString().Trim(), drPackageInfo["GatewaySetupFee"].ToString().Trim(),
                                            drPackageInfo["GatewayTransFee"].ToString().Trim());
                                                OnlineAppClassLibrary.SalesOppsBL SalesOppGW = new OnlineAppClassLibrary.SalesOppsBL(AppId);
                                                //SalesOppGW.CreateSalesOppsGW();
                                                if ((Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 5) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 20) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 36) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 60) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 74) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 96) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 150) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 253) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 282))
                                                {
                                                    int retvalSaleACT = GwySalesOppsDL.SalesOppAddedToACT(GwySalesOppID);
                                                    if (retvalSaleACT == 0)
                                                    {
                                                        //BusinessLayer.SalesOppsBL DeleteOpp = new BusinessLayer.SalesOppsBL();
                                                        SalesOppDL DeleteOpp = new SalesOppDL();
                                                        bool retDelete = DeleteOpp.DeleteSalesOpp(GwySalesOppID);
                                                        bool test = retDelete;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            OnlineAppClassLibrary.Gateway GatewayInfo = new OnlineAppClassLibrary.Gateway(AppId);
                                            retVal = GatewayInfo.UpdateGatewayInfo(drPackageInfo["Gateway"].ToString().Trim(),
                                            drPackageInfo["GatewayMonFee"].ToString().Trim(), drPackageInfo["GatewaySetupFee"].ToString().Trim(),
                                            drPackageInfo["GatewayTransFee"].ToString().Trim());
                                            OnlineAppClassLibrary.SalesOppsBL SalesOppGW = new OnlineAppClassLibrary.SalesOppsBL(AppId);
                                            SalesOppGW.CreateSalesOppsGW();
                                        }

                                    }*/
                                }
                                else
                                {
                                    //BusinessLayer.SalesOppsBL SalesOpps = new BusinessLayer.SalesOppsBL();
                                    PartnerDS.OnlineAppSalesOppsDataTable dtGwySalesOpp = OnlineAppSalesOppsAdapter.GetData(Convert.ToInt16(AppId));
                                    //PartnerDS.OnlineAppSalesOppsDataTable dtGwySalesOpp = PartnerDS.OnlineAppSalesOppsTableAdapter.GetData(Convert.ToInt16(AppId));
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
                                            retVal = GatewayInfo.UpdateGatewayInfo(drPackageInfo["Gateway"].ToString().Trim(),
                                            drPackageInfo["GatewayMonFee"].ToString().Trim(), drPackageInfo["GatewaySetupFee"].ToString().Trim(),
                                            drPackageInfo["GatewayTransFee"].ToString().Trim());
                                            OnlineAppClassLibrary.SalesOppsBL SalesOppGW = new OnlineAppClassLibrary.SalesOppsBL(AppId);
                                            SalesOppGW.CreateSalesOppsGW();
                                            if ((Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 5) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 20) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 36) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 60) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 74) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 96) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 150) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 253) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 282))
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
                                        retVal = GatewayInfo.UpdateGatewayInfo(drPackageInfo["Gateway"].ToString().Trim(),
                                            drPackageInfo["GatewayMonFee"].ToString().Trim(), drPackageInfo["GatewaySetupFee"].ToString().Trim(),
                                            drPackageInfo["GatewayTransFee"].ToString().Trim());
                                        OnlineAppClassLibrary.SalesOppsBL SalesOppGW = new OnlineAppClassLibrary.SalesOppsBL(AppId);
                                        SalesOppGW.CreateSalesOppsGW();
                                    }
                                }
                            }

                            if ((AcctType == 1) || (AcctType == 2) || (AcctType == 4))
                            {
                                //Explictly set certain Additional Services to False if it is a Card Not Present Account
                                NewAppInfo App = new NewAppInfo(AppId);
                                bool retVal4 = App.UpdateAddlServCNP(drPackageInfo["CardPresent"].ToString().Trim());

                                NewAppTable NewOnlineAppInfo = new NewAppTable();
                                DataSet dsNewOnlineAppInfo = NewOnlineAppInfo.GetNewAppInfo(AppId);
                                DataTable dtNewOnlineAppInfo = dsNewOnlineAppInfo.Tables[0];
                                if (dtNewOnlineAppInfo.Rows.Count > 0)
                                {
                                    //int AcctType = Convert.ToInt32(Session["AcctType"]);
                                    DataRow drNewOnlineAppInfo = dtNewOnlineAppInfo.Rows[0];

                                    if (Convert.ToBoolean(drNewOnlineAppInfo["OnlineDebit"]))
                                    { 
                                        OnlineDebitMonthFee = drPackageInfo["DebitMonFee"].ToString().Trim();
                                        OnlineDebitTransFee = drPackageInfo["DebitTransFee"].ToString().Trim();
                                    }

                                    if (Convert.ToBoolean(drNewOnlineAppInfo["EBT"]))
                                    {
                                        EBTMonthFee = drPackageInfo["EBTMonFee"].ToString().Trim();
                                        EBTTransFee = drPackageInfo["EBTTransFee"].ToString().Trim();
                                    }

                                    string pinDebitDiscount = "";
                                    if (Convert.ToBoolean(drNewOnlineAppInfo["OnlineDebit"]))
                                    {
                                        pinDebitDiscount = Convert.ToString(drPackageInfo["PinDebitDiscount"]).Trim();
                                    }
                                }

                                bool retValCorrateCP = false;
                                bool retValCorrateCNP = false;

                                

                                string CardPresentUpdated = drPackageInfo["CardPresent"].ToString().Trim();

                                string discountPaid = "Monthly";

                                if (Convert.ToString(drPackageInfo["Processor"]).Contains("Intuit"))
                                {
                                    discountPaid = "Daily";
                                }
                                
                                if (bitRate == 0)
                                {

                                    //Insert rates in Processing table
                                    ProcessingInfo ProcessingRates = new ProcessingInfo(AppId);
                                    bool retVal2 = ProcessingRates.UpdateProcessingInfo(
                                        drPackageInfo["Processor"].ToString().Trim(), drPackageInfo["CardPresent"].ToString().Trim(),
                                        drPackageInfo["CustServFee"].ToString().Trim(),
                                        drPackageInfo["InternetStmt"].ToString().Trim(),
                                        drPackageInfo["TransactionFee"].ToString().Trim(),
                                        drPackageInfo["DiscRateQualPres"].ToString().Trim(), drPackageInfo["DiscRateQualPres"].ToString().Trim(),
                                        drPackageInfo["DiscRateMidQual"].ToString().Trim(), drPackageInfo["DiscRateNonQual"].ToString().Trim(),
                                        drPackageInfo["DiscRateQualDebit"].ToString().Trim(),
                                        drPackageInfo["DiscRateQualDebit"].ToString().Trim(), drPackageInfo["DebitMidQual"].ToString().Trim(),
                                        drPackageInfo["DebitNonQual"].ToString().Trim(),
                                        drPackageInfo["AmexDiscRateQual"].ToString().Trim(), drPackageInfo["AmexDiscRateMidQual"].ToString().Trim(), drPackageInfo["AmexDiscRateNonQual"].ToString().Trim(), drPackageInfo["ChargebackFee"].ToString().Trim(),
                                        drPackageInfo["RetrievalFee"].ToString().Trim(), drPackageInfo["VoiceAuth"].ToString().Trim(),
                                        drPackageInfo["BatchHeader"].ToString().Trim(), drPackageInfo["AVS"].ToString().Trim(),
                                        drPackageInfo["MonMin"].ToString().Trim(), drPackageInfo["NBCTransFee"].ToString().Trim(),
                                        drPackageInfo["AnnualFee"].ToString().Trim(), drPackageInfo["WirelessAccessFee"].ToString().Trim(),
                                        drPackageInfo["WirelessTransFee"].ToString().Trim(), drPackageInfo["AppSetupFee"].ToString().Trim(),
                                        drPackageInfo["AppFee"].ToString().Trim(), OnlineDebitMonthFee,
                                        OnlineDebitTransFee, drPackageInfo["CGMonFee"].ToString().Trim(),
                                        drPackageInfo["CGTransFee"].ToString().Trim(), drPackageInfo["CGMonMin"].ToString().Trim(),
                                        drPackageInfo["CGDiscRate"].ToString().Trim(), drPackageInfo["GCMonFee"].ToString().Trim(),
                                        drPackageInfo["GCTransFee"].ToString().Trim(), EBTMonthFee,
                                        EBTTransFee, discountPaid, Convert.ToString(drPackageInfo["ContractTerm"]).Trim(), Convert.ToString(drPackageInfo["ComplianceFee"]).Trim(), "");

                                    //Insert rates in gateway info table
                                    Gateway GatewayInfo = new Gateway(AppId);
                                    bool retVal3 = GatewayInfo.UpdateGatewayInfo(drPackageInfo["Gateway"].ToString().Trim(),
                                        drPackageInfo["GatewayMonFee"].ToString().Trim(), drPackageInfo["GatewaySetupFee"].ToString().Trim(),
                                        drPackageInfo["GatewayTransFee"].ToString().Trim());
                                }
                                else {
                                    
                                    ProcessingInfo ProcessingRates = new ProcessingInfo(AppId);
                                    DataSet dsproc = ProcessingRates.GetProcessingInfo();
                                    DataTable dtproc = dsproc.Tables[0];
                                    if (dtproc.Rows.Count > 0)
                                    {
                                        DataRow drproc = dtproc.Rows[0];

                                        string CardPresent = CardPresentUpdated;

                                        if (CardPresent == "CP")
                                        {
                                            if (Convert.IsDBNull(drproc["DiscRateQualPres"]))
                                            {

                                                retValCorrateCP = ProcessingRates.CorrectRatesCP(CardPresent);

                                                /*if (!(Convert.IsDBNull(drproc["DiscRateQualNP"])))
                                                {
                                                    if (drproc["DiscRateQualNP"].ToString().Trim() != "")
                                                    {
                                                        retValCorrateCP = ProcessingRates.CorrectRatesCP(CardPresent);
                                                    }
                                                }*/
                                            }
                                            else if (drproc["DiscRateQualPres"].ToString().Trim() == "")
                                            {

                                                retValCorrateCP = ProcessingRates.CorrectRatesCP(CardPresent);
                                                /*
                                                if (!(Convert.IsDBNull(drproc["DiscRateQualNP"])))
                                                {
                                                    if (drproc["DiscRateQualNP"].ToString().Trim() != "")
                                                    {
                                                        retValCorrateCP = ProcessingRates.CorrectRatesCP(CardPresent);
                                                    }
                                                }*/
                                            }
                                        }
                                        else if (CardPresent == "CNP")
                                        {

                                            if (Convert.IsDBNull(drproc["DiscRateQualPres"]))
                                            {

                                                retValCorrateCP = ProcessingRates.CorrectRatesCP(CardPresent);

                                                /*if (!(Convert.IsDBNull(drproc["DiscRateQualNP"])))
                                                {
                                                    if (drproc["DiscRateQualNP"].ToString().Trim() != "")
                                                    {
                                                        retValCorrateCP = ProcessingRates.CorrectRatesCP(CardPresent);
                                                    }
                                                }*/
                                            }
                                            else if (drproc["DiscRateQualPres"].ToString().Trim() == "")
                                            {

                                                retValCorrateCP = ProcessingRates.CorrectRatesCP(CardPresent);
                                                /*
                                                if (!(Convert.IsDBNull(drproc["DiscRateQualNP"])))
                                                {
                                                    if (drproc["DiscRateQualNP"].ToString().Trim() != "")
                                                    {
                                                        retValCorrateCP = ProcessingRates.CorrectRatesCP(CardPresent);
                                                    }
                                                }*/
                                            }

                                            /*if (Convert.IsDBNull(drproc["DiscRateQualNP"]))
                                            {
                                                if (!(Convert.IsDBNull(drproc["DiscRateQualPres"])))
                                                {
                                                    if (drproc["DiscRateQualPres"].ToString().Trim() != "")
                                                    {
                                                        retValCorrateCNP = ProcessingRates.CorrectRatesCP(CardPresent);
                                                    }
                                                }
                                            }
                                            else if (drproc["DiscRateQualNP"].ToString().Trim() == "")
                                            {
                                                if (!(Convert.IsDBNull(drproc["DiscRateQualPres"])))
                                                {
                                                    if (drproc["DiscRateQualPres"].ToString().Trim() != "")
                                                    {
                                                        retValCorrateCNP = ProcessingRates.CorrectRatesCP(CardPresent);
                                                    }
                                                }
                                            }*/
                                        }
                                    }
                                }


                                bool boolUpdateCP = false ;

                                if (strCardPresent != CardPresentUpdated)
                                {
                                    boolUpdateCP = NewAppTable.UpdateCardPresent(AppId, drPackageInfo["CardPresent"].ToString().Trim());
                                }

                                if (boolUpdateCP)
                                {
                                    LogBL LogData = new LogBL(AppId);
                                    LogData.InsertLogData(Convert.ToInt32(Session["AffiliateID"]), "Card Present type changed from " + strCardPresent + " to " + drPackageInfo["CardPresent"].ToString().Trim());
                                }

                                if (retValCorrateCP)
                                {
                                    LogBL LogData = new LogBL(AppId);
                                    LogData.InsertLogData(Convert.ToInt32(Session["AffiliateID"]), "Discount rate present changed to match Card Present Type." );
                                }

                                if (retValCorrateCNP)
                                {
                                    LogBL LogData = new LogBL(AppId);
                                    LogData.InsertLogData(Convert.ToInt32(Session["AffiliateID"]), "Discount rate NP changed to match Card Present Type.");
                                }

                            }//end if AcctType 1

                            
                        }//end if count not 0
                    }//end if old pid not equal to new pid

                    //Insert merchant account sales opp
                    OnlineAppClassLibrary.SalesOppsBL SalesOpp = new OnlineAppClassLibrary.SalesOppsBL(AppId);
                    retVal = SalesOpp.CreateSalesOpps();
                    
                    if (Convert.ToInt32(Session["AcctType"]) == 4)
                    {

                        //Insert Gateway sals opp
                        OnlineAppClassLibrary.SalesOppsBL SalesOppGW = new OnlineAppClassLibrary.SalesOppsBL(AppId);
                        retVal = SalesOppGW.CreateSalesOppsGW();
                    }

//Insert Gateway Sales Opp if account type is merchant and gateway
/*
if (Convert.ToInt32(Session["AcctType"]) == 4)
{

    //Insert Gateway sals opp
    //OnlineAppClassLibrary.SalesOppsBL SalesOppGW = new OnlineAppClassLibrary.SalesOppsBL(AppId);
    //retVal = SalesOppGW.CreateSalesOppsGW();

    PackageInfo Package = new PackageInfo();
    DataSet dsPackageInfo = Package.GetPackageInfo(PID);
    DataTable dtPackageInfo = dsPackageInfo.Tables[0];
    DataRow drPackageInfo = dtPackageInfo.Rows[0];

    Gateway GWRates = new Gateway(AppId);
    DataSet dsGateway = GWRates.GetGatewayInfo();
    if (dsGateway.Tables[0].Rows.Count > 0)
    {
        DataRow drGateway = dsGateway.Tables[0].Rows[0];
        if (drPackageInfo["Gateway"].ToString().Trim() != drGateway["Gateway"].ToString().Trim())
        {
            BusinessLayer.SalesOppsBL SalesOpps = new BusinessLayer.SalesOppsBL();
            PartnerDS.OnlineAppSalesOppsDataTable dtGwySalesOpp = OnlineAppSalesOppsAdapter.GetData(Convert.ToInt16(AppId));
            //PartnerDS.OnlineAppSalesOppsDataTable dtGwySalesOpp = PartnerDS.OnlineAppSalesOppsTableAdapter.GetData(Convert.ToInt16(AppId));
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
                    retVal = GatewayInfo.UpdateGatewayInfo(drPackageInfo["Gateway"].ToString().Trim(),
                drPackageInfo["GatewayMonFee"].ToString().Trim(), drPackageInfo["GatewaySetupFee"].ToString().Trim(),
                drPackageInfo["GatewayTransFee"].ToString().Trim());
                    OnlineAppClassLibrary.SalesOppsBL SalesOppGW = new OnlineAppClassLibrary.SalesOppsBL(AppId);
                    SalesOppGW.CreateSalesOppsGW();
                    if ((Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 5) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 20) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 36) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 60) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 74) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 96) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 150) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 253) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 282))
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
                retVal = GatewayInfo.UpdateGatewayInfo(drPackageInfo["Gateway"].ToString().Trim(),
                drPackageInfo["GatewayMonFee"].ToString().Trim(), drPackageInfo["GatewaySetupFee"].ToString().Trim(),
                drPackageInfo["GatewayTransFee"].ToString().Trim());
                OnlineAppClassLibrary.SalesOppsBL SalesOppGW = new OnlineAppClassLibrary.SalesOppsBL(AppId);
                SalesOppGW.CreateSalesOppsGW();
            }

        }
    }
    else
    {
        BusinessLayer.SalesOppsBL SalesOpps = new BusinessLayer.SalesOppsBL();
        PartnerDS.OnlineAppSalesOppsDataTable dtGwySalesOpp = OnlineAppSalesOppsAdapter.GetData(Convert.ToInt16(AppId));
        //PartnerDS.OnlineAppSalesOppsDataTable dtGwySalesOpp = PartnerDS.OnlineAppSalesOppsTableAdapter.GetData(Convert.ToInt16(AppId));
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
                retVal = GatewayInfo.UpdateGatewayInfo(drPackageInfo["Gateway"].ToString().Trim(),
                drPackageInfo["GatewayMonFee"].ToString().Trim(), drPackageInfo["GatewaySetupFee"].ToString().Trim(),
                drPackageInfo["GatewayTransFee"].ToString().Trim());
                OnlineAppClassLibrary.SalesOppsBL SalesOppGW = new OnlineAppClassLibrary.SalesOppsBL(AppId);
                SalesOppGW.CreateSalesOppsGW();
                if ((Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 5) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 20) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 36) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 60) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 74) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 96) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 150) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 253) || (Convert.ToInt16(drGwySalesOpp["ProductCode"]) == 282))
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
            retVal = GatewayInfo.UpdateGatewayInfo(drPackageInfo["Gateway"].ToString().Trim(),
                drPackageInfo["GatewayMonFee"].ToString().Trim(), drPackageInfo["GatewaySetupFee"].ToString().Trim(),
                drPackageInfo["GatewayTransFee"].ToString().Trim());
            OnlineAppClassLibrary.SalesOppsBL SalesOppGW = new OnlineAppClassLibrary.SalesOppsBL(AppId);
            SalesOppGW.CreateSalesOppsGW();
        }
    }
}*/

                    //Set last modified date in newapp table
                    /*
                    CommonFunctions GeneralInfo = new CommonFunctions(AppId);
                    GeneralInfo.UpdateLastModified();

                    GeneralInfo.SetPageCount();
                    GeneralInfo.SetGWPageCount();*/

                }//end try
                catch (Exception err)
                {
                    CreateOnlineAppLog Log = new CreateOnlineAppLog();
                    Log.ErrorLog(Server.MapPath("~/ErrorLog"), "AppId: " + Session["AppId"].ToString() + " - " + "Merchant cardpct.aspx Error - " + err.Message);
                    SetErrorMessage("Error Processing Request. Please contact Technical Support: <a href='mailto:support@commercetech.com?Subject=Online Application Error'> support@commercetech.com. </a>.");
                }
                /*if (bCP)
                    Response.Redirect("../AddlServices.aspx?RatesChanged=" + bRatesChanged);
                else*/

                bool retValRedirect = true;
                OnlineAppClassLibrary.ProcessingInfo OnlineAppProcessingInfo = new OnlineAppClassLibrary.ProcessingInfo(AppId);
                DataSet dsOnlineAppProcessingInfo = OnlineAppProcessingInfo.GetProcessingInfo();
                if (dsOnlineAppProcessingInfo.Tables[0].Rows.Count > 0)
                {
                    DataRow drOnlineAppProcessingInfo = dsOnlineAppProcessingInfo.Tables[0].Rows[0];
                    if (drOnlineAppProcessingInfo["Processor"].ToString().Trim().Contains("Sage"))
                    {
                        if ((Convert.ToInt32(lstSwiped.SelectedItem.Value) % 5 != 0) || (Convert.ToInt32(lstKeyedWith.SelectedItem.Value) % 5 != 0) || (Convert.ToInt32(lstKeyedWithout.SelectedItem.Value) % 5 != 0) || (Convert.ToInt32(lstService.SelectedItem.Value) % 5 != 0) || (Convert.ToInt32(lstRetail.SelectedItem.Value) % 5 != 0) || (Convert.ToInt32(lstRestaurant.SelectedItem.Value) % 5 != 0) || (Convert.ToInt32(lstMail.SelectedItem.Value) % 5 != 0) || (Convert.ToInt32(lstInternet.SelectedItem.Value) % 5 != 0) || (Convert.ToInt32(lstOther.SelectedItem.Value) % 5 != 0))
                        {
                            SetErrorMessage("For Sage mechant account application, any of the percentages can only be increment of 5. Please adjust the percentages and submit again.");
                            retValRedirect = false;
                        }
                    }

                }

                if (retValRedirect)
                {
                    CardPCT CardPCTInfo = new CardPCT(AppId);
                    bool retVal = CardPCTInfo.UpdateCardPCT(lstRetail.SelectedItem.Text.ToString().Trim(), lstRestaurant.SelectedItem.Text.ToString().Trim(),
                        lstMail.SelectedItem.Text.ToString().Trim(), lstInternet.SelectedItem.Text.ToString().Trim(), lstSwiped.SelectedItem.Text.ToString().Trim(),
                        lstKeyedWith.SelectedItem.Text.ToString().Trim(), lstKeyedWithout.SelectedItem.Text.ToString().Trim(), lstService.SelectedItem.Text.ToString().Trim(),
                        lstOther.SelectedItem.Text.ToString().Trim(), true);

                    //Set last modified date in newapp table
                    CommonFunctions GeneralInfo = new CommonFunctions(AppId);
                    

                    GeneralInfo.SetPageCount();
                    GeneralInfo.SetGWPageCount();

                    if (retVal)
                    {
                        if (textChanged == true)
                        {
                            GeneralInfo.UpdateLastModified();
                            LogBL LogData = new LogBL(AppId);
                            LogData.InsertLogData(Convert.ToInt32(Session["AffiliateID"]), "Merchant Card Percentages Information Updated");
                        }
                    }

                    OnlineAppProfile Profile = new OnlineAppProfile(AppId);
                    int AcctType = 0;

                    //Response.Redirect("companyinfo.aspx?RatesChanged=" + bRatesChanged);
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
                        lblUser.Text = "Are you using or planing to use Intuit QuickBooks POS V.11 or 2013 for this account?";
                        ModalPopupExtender1.Show();
                        //ClientScript.RegisterStartupScript(this.GetType(), "PopupScript", "ConfirmPOS2013();", true);
                    }
                    else
                    {
                        AppInfo.checkPageRedirect();
                    }
                }


            }//end else
        }

    }//end function submit button click

    //This function handles submit button click event
    protected void lnkEditInfo_Click(object sender, EventArgs e)
    {
        lblHeader.Visible = true;
        lblEdit.Visible = false;
        btnEditInfo.Visible = false;
        lstInternet.Enabled = true;
        lstMail.Enabled = true;
        lstRestaurant.Enabled = true;
        lstOther.Enabled = false;
        lstRetail.Enabled = true;
        lstService.Enabled = false;
        lstSwiped.Enabled = true;
        lstKeyedWith.Enabled = false;
        lstKeyedWithout.Enabled = true;
        lnkEditInfo.Visible = false;
    }//end function submit button click

    //This function displays error messages
    protected void SetErrorMessage(string errText)
    {
        lblError.Visible = true;
        lblError.Text = errText;
    }//end function set error message

    protected void btnNavigateNext_Click(object sender, EventArgs e)
    {
        if (nextNavigatePage == "Percentage")
        {
            Response.Redirect("/Application/Merchant/cardpct.aspx", false);
        }else if (nextNavigatePage == "Company")
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
        if (cardPCTCom == 0)
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
        //btnSubmit_Click(sender, e);

        /*
        bool companyComplete = returnComplete();
        if (companyComplete == false)
        {
            nextNavigatePage = "Percentage";
            lblUserNavigateUnsaved.Text = "The current page is not complete. Are you sure to leave this page?";
            NavigatePopupModalExtender.Show();
        }
        else*/
        
            //Response.Redirect("/Application/Merchant/cardpct.aspx", false);
        
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
        
        Response.Redirect("/Application/Merchant/companyinfo.aspx", false);
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
            Response.Redirect("/Application/Merchant/businessinfo.aspx", false);
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
        
            Response.Redirect("/Application/Merchant/principalinfo.aspx", false);
        
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
