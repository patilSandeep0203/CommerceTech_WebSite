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
using DLPartner;

using System.Data.SqlClient;
using System.Configuration;



public partial class CommAdmin : System.Web.UI.Page
{ 
    void Page_Init(object sender, EventArgs e)
    {
        ViewStateUserKey = Session.SessionID;
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session.IsNewSession)
            Response.Redirect("~/login.aspx");        
        if (!User.IsInRole("Admin"))
            Response.Redirect("~/login.aspx");

        if (!Page.IsPostBack)
        {
            if (!User.Identity.IsAuthenticated)
                Response.Redirect("~/login.aspx?Authentication=False");
            try
            {           
                CommissionsBL Rep = new CommissionsBL();
                MonthBL Mon = new MonthBL();
                DataSet dsRep = Rep.GetRepList();
                
                if (dsRep.Tables[0].Rows.Count > 0)
                {
                    lstRepList.DataSource = dsRep;
                    lstRepList.DataTextField = "RepName";
                    lstRepList.DataValueField = "RepNum";                    
                    lstRepList.DataBind();
                }
                ListItem item = new ListItem();
                item.Text = "ALL";
                item.Value = "ALL";
                lstRepList.Items.Add(item);
                lstRepList.SelectedIndex = lstRepList.Items.IndexOf(item);

                string MasterNum = "";
                string Month = "";
                if (Request.Params.Get("Month") != null)
                    Month = Request.Params.Get("Month");

                if (Request.Params.Get("MasterNum") != null)
                    MasterNum = Request.Params.Get("MasterNum");
                                
                DataSet dsMon = Mon.GetMonthListForReports(1, "Commissions");
                if (dsMon.Tables[0].Rows.Count > 0)
                {
                    lstMonth.DataSource = dsMon;
                    lstMonth.DataTextField = "Mon";
                    lstMonth.DataValueField = "Mon";
                    lstMonth.DataBind();
                }
                            
                if ((Month != "") && (MasterNum != "") && (User.IsInRole("Admin")))
                {
                    try
                    {                        
                        lblError.Visible = false;
                        lstRepList.SelectedValue = lstRepList.Items.FindByValue(MasterNum).Value;
                        lstMonth.SelectedValue = lstMonth.Items.FindByValue(Month).Value;
                        lblError.Visible = false;
                        lblMonth.Text = "Commissions for the month of: " + lstMonth.SelectedItem.Text + "-" + lstPeriod.SelectedItem.Text;
                        PopulateSummary();
                        double fRepTotalSum = PopulateCommissions(MasterNum, Month);
                        PopulateBonus(fRepTotalSum);
                    }
                    catch (Exception err)
                    {
                        CreateLog Log = new CreateLog();
                        Log.ErrorLog(Server.MapPath("~/ErrorLog"), err.Message);
                        DisplayMessage("Error Processing Request. Please contact technical support");
                    }
                }

                string DBA = "";
                string MonthVerify = "";
                if (Request.Params.Get("MonthVerify") != null)
                    MonthVerify = Request.Params.Get("MonthVerify");

                if (Request.Params.Get("DBA") != null)
                    DBA = Request.Params.Get("DBA");

                if ((MonthVerify != "") && (DBA != "") && (User.IsInRole("Admin")))
                {
                    //lblHistory.Text = "Commissions history for " + DBA + " excluding the month of " + Month;
                    CommissionsBL Comm= new CommissionsBL();
                    PartnerDS.CommissionsDataTable dt = Comm.GetCommissionsByDBA(DBA, MonthVerify);
                    if (dt.Rows.Count > 0)
                    {
                        pnlFeatures.Visible = true;
                        pnlGrid.ScrollBars = ScrollBars.Both;
                        pnlGrid.Height = Unit.Pixel(150);
                        grdComm.Visible = true;
                        grdComm.DataSource = dt;
                        grdComm.DataBind();
                        lblError.Visible = false;
                        lblMonth.Text = "Commissions for the month of: " + lstMonth.SelectedItem.Text;
                        lstMonth.SelectedValue = lstMonth.Items.FindByValue(MonthVerify).Value;
                        PopulateSummary();
                        double fRepTotalSum = PopulateCommissions(lstRepList.SelectedItem.Value, lstMonth.SelectedItem.Value);
                        PopulateBonus(fRepTotalSum);
                    }
                    else
                        DisplayMessage("No Records found.");
                }//end if
                
            }//end try
            catch (Exception err)
            {
                CreateLog Log = new CreateLog();
                Log.ErrorLog(Server.MapPath("~/ErrorLog"), err.Message);
                DisplayMessage("Error Processing Request. Please contact technical support");
            }
        }//end if not postback
    }//end page load

    //This function populates commissions summary
    public void PopulateSummary()
    {
        CommissionsBL Comm= new CommissionsBL();
        DataSet ds = new DataSet();
        if (lstPeriod.SelectedValue.ToString().Trim() == "0")
            ds = Comm.GetFundedCount(lstRepList.SelectedItem.Value.ToString().Trim(), lstMonth.SelectedItem.Value.ToString().Trim());
        else
            ds = Comm.GetFundedCountPeriod(lstRepList.SelectedItem.Value.ToString().Trim(), lstMonth.SelectedItem.Value.ToString().Trim(), lstPeriod.SelectedItem.Value.ToString().Trim());
        if (ds.Tables[0].Rows.Count > 0)
        {
            //pnlResetComm.Visible = true;

            Style ValueLabel = new Style();
            ValueLabel.ForeColor = System.Drawing.Color.Black;
            ValueLabel.Font.Size = FontUnit.Small;
            ValueLabel.Font.Name = "Arial";

            Style ValueLabelHeader = new Style();
            ValueLabelHeader.ForeColor = System.Drawing.Color.White;
            ValueLabelHeader.Font.Size = FontUnit.Small;
            ValueLabelHeader.Font.Bold = true;
            ValueLabelHeader.Font.Name = "Arial";

            TableRow tr = new TableRow();
            TableCell td = new TableCell();
            Label lblValue;
            string[] arrColumns = { "RepName", "Funded Accounts"};
            tr.BackColor = System.Drawing.Color.FromArgb(86, 82, 148);
            for (int i = 0; i < arrColumns.Length; i++)
            {
                td = new TableCell();
                td.Text = arrColumns[i].ToString();
                td.Style["font-weight"] = "Bold";
                td.CssClass = "MenuHeader";
                tr.Cells.Add(td);
            }
            tblCommSummary.Rows.Add(tr);
            double iTotal = 0;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow dr = ds.Tables[0].Rows[i];
                tr = new TableRow();
                
                //Rep Name
                lblValue = new Label();
                lblValue.Text = dr["RepName"].ToString().Trim();
                td = new TableCell();
                lblValue.ApplyStyle(ValueLabel);
                td.Controls.Add(lblValue);
                tr.Cells.Add(td);

                //Funded Accounts
                lblValue = new Label();
                lblValue.Text = dr["MerchFundedCount"].ToString().Trim();
                if ( dr["MerchFundedCount"].ToString().Trim() != "" )
                    iTotal = iTotal + Convert.ToDouble(dr["MerchFundedCount"]);
                td = new TableCell();
                lblValue.ApplyStyle(ValueLabel);
                td.Controls.Add(lblValue);
                tr.Cells.Add(td);

                if (i % 2 == 0)
                    tr.BackColor = System.Drawing.Color.FromArgb(230, 237, 245);
                tblCommSummary.Rows.Add(tr);
            }//end for

            tr = new TableRow();
            tr.BackColor = System.Drawing.Color.FromArgb(230, 237, 245);
            //Total Funded Header
            lblValue = new Label();
            lblValue.Text = "Total Funded";
            td = new TableCell();
            lblValue.ApplyStyle(ValueLabel);
            lblValue.Font.Bold = true;
            td.Controls.Add(lblValue);
            tr.Cells.Add(td);

            //Total
            lblValue = new Label();
            lblValue.Text = iTotal.ToString();
            td = new TableCell();
            lblValue.ApplyStyle(ValueLabel);
            lblValue.Font.Bold = true;
            td.Controls.Add(lblValue);
            tr.Cells.Add(td);

            tblCommSummary.Rows.Add(tr);
        }//end if count not 0
        else
            DisplayMessage("No Records found for this month");
    }//end function PopulateSummary

    //This function populates the Commissions detail
    public double PopulateCommissions(string MasterNum, string Month)
    {

        double fFundedValueSum = 0;
        double fRepTotalSum = 0;
        double fRefTotalSum = 0;
            
        Style ValueLabel = new Style();
        ValueLabel.ForeColor = System.Drawing.Color.Black;
        ValueLabel.Font.Size = FontUnit.Point(8);
        ValueLabel.Font.Name = "Arial";

        Style ValueLabelHeader = new Style();
        ValueLabelHeader.ForeColor = System.Drawing.Color.White;
        ValueLabelHeader.Font.Size = FontUnit.Small;
        ValueLabelHeader.Font.Bold = true;
        ValueLabelHeader.Font.Name = "Arial";

        Style sHyperLink = new Style();
        sHyperLink.Font.Bold = true;
        sHyperLink.Font.Size = FontUnit.Point(8);
        sHyperLink.Font.Name = "Arial";
        sHyperLink.CssClass = "One";

        Style HalfFundedValue = new Style();
        HalfFundedValue.ForeColor = System.Drawing.Color.Green;
        HalfFundedValue.Font.Size = FontUnit.Small;
        HalfFundedValue.Font.Bold = true;
        HalfFundedValue.Font.Name = "Arial";

        CommissionsBL Comm = new CommissionsBL();
        PartnerDS.CommissionsDataTable dt = new PartnerDS.CommissionsDataTable();
        if (lstPeriod.SelectedValue.ToString().Trim() == "0")
            dt = Comm.GetCommissionsByRepMon(MasterNum, Month);
        else
            dt = Comm.GetCommissionsByRepMonPeriod(MasterNum, Month, Convert.ToInt32(lstPeriod.SelectedItem.Value));
       
        if (dt.Rows.Count == 0)
        {
            DisplayMessage("No Commissions found for the current month");
        }
         
        if (dt.Rows.Count > 0)
        {
            TableRow tr = new TableRow();
            TableCell td = new TableCell();
            Label lblValue;
            Label lblStatusSTR;
            Label lblFunded;

            int iCount = 0;
            string[] arrColumns = { "Merchant Legal","Merchant DBA", "Merchant Number", "Rep Name", "Referred By DBA", "Product", "Qty", "Price (Per Unit)", "Total", "COG (Per Unit)", "Comm", "Rep Total", "Referral Paid","Funded Value",  "Update"};
            if (User.IsInRole("Admin"))
                iCount = arrColumns.Length;
            else
                iCount = arrColumns.Length -1;
            
            tr.BackColor = System.Drawing.Color.FromArgb(86, 82, 148);
            for (int i = 0; i < iCount; i++)
            {
                td = new TableCell();
                td.Text = arrColumns[i].ToString();
                td.Style["font-weight"] = "Bold";
                td.CssClass = "MenuHeader";
                tr.Cells.Add(td);
            }

            tblCommissions.Rows.Add(tr);
            string strRepTotalCurr = "";            
            
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                HyperLink lnkExport;
                tr = new TableRow();
                lblStatusSTR = new Label();                
                lblFunded = new Label();
                
                //if (Convert.ToDouble(dt[i].FundedValue) == 1.00)
                if (dt[i].FundedValue.ToString().Trim() == "1.00")
                {
                    Style StatusLabel = new Style();
                    StatusLabel.ForeColor = System.Drawing.Color.Red;
                    StatusLabel.Font.Size = FontUnit.Medium;
                    //StatusLabel.Font.Bold = true;
                    StatusLabel.Font.Name = "Arial";
                    lblStatusSTR.Text = "+";
                    lblStatusSTR.ApplyStyle(StatusLabel);
                }
                else
                    lblStatusSTR.Text = "";
                
                if (dt[i].FundedValue.ToString().Trim() == "0.50")
                {
                    lblFunded.Text = "#";
                    lblFunded.ApplyStyle(HalfFundedValue);
                }                
                else
                    lblFunded.Text = "";

                if (dt[i].FundedValue.ToString().Trim() == "0.25")
                {
                    Style StatusLabel = new Style();
                    StatusLabel.ForeColor = System.Drawing.Color.Red;
                    StatusLabel.Font.Size = FontUnit.Medium;
                    StatusLabel.Font.Bold = true;
                    StatusLabel.Font.Name = "Arial";
                    lblStatusSTR.Text = "- ";
                    lblStatusSTR.ApplyStyle(StatusLabel);
                }
                else
                    lblFunded.Text = "";


                if (dt[i].OtherComm.ToString().Trim() == "Yes")
                {
                    //DBA
                    lnkExport = new HyperLink();
                    lnkExport.Text = dt[i].Company.ToString().Trim();
                    //string strURL = "commAdmin.aspx?DBA=" + dt[i].DBA.ToString().Trim() + "&MonthVerify=" + Month;
                    //string strURL2 = "window.open('" + strURL + "', 'rates','width=720,height=300,toolbar=no,location=no,status=no,menubar=no,scrollbars=no,resizable=yes');";
                    //lnkExport.Attributes.Add("onClick", strURL2);
                    lnkExport.NavigateUrl = "commAdmin.aspx?DBA=" + dt[i].DBA.ToString().Trim() + "&MonthVerify=" + Month;
                    //lnkExport.Target = "_blank";                    
                    lnkExport.ApplyStyle(sHyperLink);
                    td = new TableCell();
                    td.Controls.Add(lblStatusSTR);
                    td.Controls.Add(lblFunded);
                    lnkExport.ForeColor = System.Drawing.Color.Blue;
                    td.Controls.Add(lnkExport);
                    td.Attributes.Add("align", "left");
                    tr.Cells.Add(td);
                }
                else
                {
                    //DBA
                    lblValue = new Label();
                    lblValue.Text = dt[i].Company.ToString().Trim();
                    td = new TableCell();
                    lblValue.ApplyStyle(ValueLabel);
                    td.Controls.Add(lblStatusSTR);
                    td.Controls.Add(lblFunded);
                    td.Controls.Add(lblValue);
                    td.Attributes.Add("align", "left");
                    tr.Cells.Add(td);
                }
                
                if (dt[i].OtherComm.ToString().Trim() == "Yes")
                {
                    //DBA
                    lnkExport = new HyperLink();
                    lnkExport.Text = dt[i].DBA.ToString().Trim();
                    //string strURL = "commAdmin.aspx?DBA=" + dt[i].DBA.ToString().Trim() + "&MonthVerify=" + Month;
                    //string strURL2 = "window.open('" + strURL + "', 'rates','width=720,height=300,toolbar=no,location=no,status=no,menubar=no,scrollbars=no,resizable=yes');";
                    //lnkExport.Attributes.Add("onClick", strURL2);
                    lnkExport.NavigateUrl = "commAdmin.aspx?DBA=" + dt[i].DBA.ToString().Trim() + "&MonthVerify=" + Month;
                    //lnkExport.Target = "_blank";                    
                    lnkExport.ApplyStyle(sHyperLink);
                    td = new TableCell();
                    td.Controls.Add(lblStatusSTR);
                    td.Controls.Add(lblFunded);
                    lnkExport.ForeColor = System.Drawing.Color.Blue;
                    td.Controls.Add(lnkExport);
                    td.Attributes.Add("align", "left");
                    tr.Cells.Add(td);
                }
                else
                {
                    //DBA
                    lblValue = new Label();
                    lblValue.Text = dt[i].DBA.ToString().Trim();
                    td = new TableCell();
                    lblValue.ApplyStyle(ValueLabel);
                    td.Controls.Add(lblStatusSTR);
                    td.Controls.Add(lblFunded);
                    td.Controls.Add(lblValue);
                    td.Attributes.Add("align", "left");
                    tr.Cells.Add(td);
                }
                
                //Merchant Number
                lblValue = new Label();
                lblValue.Text = dt[i].MerchantID.ToString().Trim();
                td = new TableCell();
                lblValue.ApplyStyle(ValueLabel);
                td.Controls.Add(lblValue);
                tr.Cells.Add(td);

                //RepName
                lblValue = new Label();
                lblValue.Text = dt[i].RepName.ToString().Trim();
                td = new TableCell();
                lblValue.ApplyStyle(ValueLabel);
                td.Controls.Add(lblValue);
                tr.Cells.Add(td);

                //Referred By
                lblValue = new Label();
                lblValue.Text = dt[i].ReferredBy.ToString().Trim();
                td = new TableCell();
                lblValue.ApplyStyle(ValueLabel);
                td.Controls.Add(lblValue);
                tr.Cells.Add(td);
                
                //Product
                lblValue = new Label();
                lblValue.Text = dt[i].Product.ToString().Trim();
                td = new TableCell();
                lblValue.ApplyStyle(ValueLabel);
                td.Controls.Add(lblValue);
                tr.Cells.Add(td);

                //Units
                lblValue = new Label();
                lblValue.Text = Convert.ToInt32(dt[i].Units).ToString().Trim();
                td = new TableCell();
                lblValue.ApplyStyle(ValueLabel);
                td.Controls.Add(lblValue);
                tr.Cells.Add(td);
                
                //Price                
                lblValue = new Label();
                lblValue.Text = Convert.ToDouble(dt[i].Price).ToString().Trim();
                td = new TableCell();
                lblValue.ApplyStyle(ValueLabel);
                td.Controls.Add(lblValue);
                tr.Cells.Add(td);
                
                //Total
                lblValue = new Label();
                lblValue.Text = Convert.ToDouble(dt[i].Total).ToString().Trim();
                td = new TableCell();
                lblValue.ApplyStyle(ValueLabel);
                td.Controls.Add(lblValue);
                tr.Cells.Add(td);
              
                //COG
                lblValue = new Label();
                lblValue.Text = Convert.ToDouble(dt[i].COG).ToString().Trim();
                td = new TableCell();
                lblValue.ApplyStyle(ValueLabel);
                td.Controls.Add(lblValue);
                tr.Cells.Add(td);
                
                //Commission
                lblValue = new Label();
                lblValue.Text = Convert.ToInt32(dt[i].Commission).ToString() + "%";
                td = new TableCell();
                lblValue.ApplyStyle(ValueLabel);
                td.Controls.Add(lblValue);
                tr.Cells.Add(td);
                
                //RepTotal
                lblValue = new Label();                
                if (dt[i].RepTotal.ToString().Trim() != "")
                    strRepTotalCurr = "$" + Convert.ToDouble(dt[i].RepTotal).ToString();
                lblValue.Text = strRepTotalCurr;
                fRepTotalSum = fRepTotalSum + Convert.ToDouble(dt[i].RepTotal);
                td = new TableCell();
                lblValue.ApplyStyle(ValueLabel);
                td.Controls.Add(lblValue);
                tr.Cells.Add(td);
                
                //RefTotal
                lblValue = new Label();
                lblValue.Text = Convert.ToString(Convert.ToDouble(dt[i].RefTotal));
                fRefTotalSum = fRefTotalSum + Convert.ToDouble(dt[i].RefTotal);
                td = new TableCell();
                lblValue.ApplyStyle(ValueLabel);
                td.Controls.Add(lblValue);
                tr.Cells.Add(td);

                //FundedValue
                lblValue = new Label();
                lblValue.Text = Convert.ToString(Convert.ToDouble(dt[i].FundedValue));
                fFundedValueSum = fFundedValueSum + Convert.ToDouble(dt[i].FundedValue);
                td = new TableCell();
                lblValue.ApplyStyle(ValueLabel);
                td.Controls.Add(lblValue);
                tr.Cells.Add(td);
                
                if (User.IsInRole("Admin"))
                {
                    //Update
                    lnkExport = new HyperLink();
                    lnkExport.Text = "Update";
                    string CommissionID = dt[i].CommissionID.ToString().Trim();
                    lnkExport.NavigateUrl = "commupdate.aspx?Task=Update&CommID=" + CommissionID;
                    lnkExport.ApplyStyle(sHyperLink);
                    td = new TableCell();
                    td.Controls.Add(lnkExport);
                    tr.Cells.Add(td);
                }
                
                if (i % 2 == 0)
                    tr.BackColor = System.Drawing.Color.FromArgb(230, 237, 245);
                tblCommissions.Rows.Add(tr);
            }//end for rows
            tr = new TableRow();

            td = new TableCell();
            td.Attributes.Add("ColSpan", "9");
            tr.Cells.Add(td);

            lblValue = new Label();
            lblValue.Text = "Sub Total: ";
            td = new TableCell();
            lblValue.ApplyStyle(ValueLabel);
            td.Attributes.Add("ColSpan", "2");
            lblValue.Font.Size = FontUnit.Point(10);
            lblValue.Font.Bold = true;
            td.Controls.Add(lblValue);
            tr.Cells.Add(td);


            lblValue = new Label();
            lblValue.Text = "$" + decimal.Round(Convert.ToDecimal(fRepTotalSum),2).ToString();
            td = new TableCell();
            lblValue.ApplyStyle(ValueLabel);
            lblValue.Font.Size = FontUnit.Point(10);
            lblValue.Font.Bold = true;
            td.Controls.Add(lblValue);
            tr.Cells.Add(td);

            lblValue = new Label();
            lblValue.Text = "$" + decimal.Round(Convert.ToDecimal(fRefTotalSum)).ToString();
            td = new TableCell();
            lblValue.ApplyStyle(ValueLabel);
            lblValue.Font.Size = FontUnit.Point(10);
            lblValue.Font.Bold = true;
            td.Controls.Add(lblValue);
            tr.Cells.Add(td);

            lblValue = new Label();
            lblValue.Text = Convert.ToDecimal(fFundedValueSum).ToString();
            td = new TableCell();
            lblValue.ApplyStyle(ValueLabel);
            lblValue.Font.Size = FontUnit.Point(10);
            lblValue.Font.Bold = true;
            td.Controls.Add(lblValue);
            tr.Cells.Add(td);

            if (User.IsInRole("Admin"))
            {
                HyperLink lnkPrint = new HyperLink();
                td = new TableCell();
                lnkPrint.Text = "PRINT";
                lnkPrint.NavigateUrl = "commprint.aspx?RepNum=" + lstRepList.SelectedItem.Value + "&Month=" + lstMonth.SelectedItem.Value + "&Period=" + lstPeriod.SelectedItem.Text;
                lnkPrint.ApplyStyle(sHyperLink);
                lnkPrint.Target = "_Blank";
                lnkPrint.Font.Size = FontUnit.Point(10);
                td.Controls.Add(lnkPrint);
                tr.Cells.Add(td);
            }

            tr.BackColor = System.Drawing.Color.FromArgb(230, 237, 245);
            tblCommissions.Rows.Add(tr);

        }//end if count not 0
        return fRepTotalSum + fRefTotalSum;
    }//end populate commissions

    //This function populates Bonus detail
    public void PopulateBonus(double fRepTotalSum)
    {
        CommissionsBL Bonus = new CommissionsBL();
        DataSet ds = Bonus.GetBonusInfo(lstRepList.SelectedItem.Value.ToString().Trim(), lstMonth.SelectedItem.Value.ToString().Trim(), Convert.ToInt32(lstPeriod.SelectedItem.Value));
        Style ValueLabel = new Style();
        ValueLabel.ForeColor = System.Drawing.Color.Black;
        ValueLabel.Font.Size = FontUnit.Small;
        ValueLabel.Font.Name = "Arial";

        Style ValueLabelHeader = new Style();
        ValueLabelHeader.ForeColor = System.Drawing.Color.White;
        ValueLabelHeader.Font.Size = FontUnit.Small;
        ValueLabelHeader.Font.Bold = true;
        ValueLabelHeader.Font.Name = "Arial";

        TableRow tr = new TableRow();
        TableCell td = new TableCell();
        Label lblValue;
        double fBonusTotal = 0;
        string[] arrColumns = { "Bonuses/Discover Commissions For the Month", "Reason", "Month", "RepNum", "RepTotal" };
        tr.BackColor = System.Drawing.Color.FromArgb(93, 123, 157);
        for (int i = 0; i < arrColumns.Length; i++)
        {
            td = new TableCell();
            td.Text = arrColumns[i].ToString();
            td.Style["font-family"] = "Arial";
            td.Style["font-size"] = "Small";
            td.Style["font-weight"] = "Bold";
            td.Style["Color"] = "White";
            tr.Cells.Add(td);
        }

        if (ds.Tables[0].Rows.Count > 0)
        {            
            tblBonus.Rows.Add(tr);            
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow dr = ds.Tables[0].Rows[i];
                tr = new TableRow();

                //Bonus Desc
                lblValue = new Label();
                lblValue.Text = dr["BonusDesc"].ToString().Trim();
                td = new TableCell();
                lblValue.ApplyStyle(ValueLabel);
                td.Controls.Add(lblValue);
                tr.Cells.Add(td);

                //Reason
                lblValue = new Label();
                lblValue.Text = dr["Reason"].ToString().Trim();                
                td = new TableCell();
                lblValue.ApplyStyle(ValueLabel);
                td.Controls.Add(lblValue);
                tr.Cells.Add(td);

                //Month
                lblValue = new Label();
                lblValue.Text = dr["Mon"].ToString().Trim();
                td = new TableCell();
                lblValue.ApplyStyle(ValueLabel);
                td.Controls.Add(lblValue);
                tr.Cells.Add(td);

                //RepNum
                lblValue = new Label();
                lblValue.Text = dr["RepNum"].ToString().Trim();
                td = new TableCell();
                lblValue.ApplyStyle(ValueLabel);
                td.Controls.Add(lblValue);
                tr.Cells.Add(td);

                //RepTotal
                lblValue = new Label();
                lblValue.Text = "$" + Convert.ToDouble (dr["RepTotal"]).ToString();
                fBonusTotal = fBonusTotal + Convert.ToDouble(dr["RepTotal"]);
                td = new TableCell();
                lblValue.ApplyStyle(ValueLabel);
                td.Controls.Add(lblValue);
                tr.Cells.Add(td);

                if (i % 2 == 0)
                    tr.BackColor = System.Drawing.Color.FromArgb(230, 237, 245);
                tblBonus.Rows.Add(tr);
            }//end for
            
            tr = new TableRow();
            td = new TableCell();
            td.Attributes.Add("ColSpan", "3");
            tr.Cells.Add(td);

            //Bonus Total Header
            lblValue = new Label();
            lblValue.Text = "Bonus Total: ";
            td = new TableCell();
            lblValue.ApplyStyle(ValueLabel);
            lblValue.Font.Size = FontUnit.Point(10);
            lblValue.Font.Bold = true;
            td.Controls.Add(lblValue);
            tr.Cells.Add(td);

            //Bonus Total
            lblValue = new Label();
            lblValue.Text = "$" + fBonusTotal.ToString();
            td = new TableCell();
            lblValue.ApplyStyle(ValueLabel);
            lblValue.Font.Size = FontUnit.Point(10);
            lblValue.Font.Bold = true;
            td.Controls.Add(lblValue);
            tr.Cells.Add(td);

            tr.BackColor = System.Drawing.Color.FromArgb(230, 237, 245);
            tblBonus.Rows.Add(tr);
        }//end if count not 0

        double fRepFinal = fRepTotalSum + fBonusTotal;
        //Create Add Button
        tr = new TableRow();     
        
        Style sHyperLink = new Style();
        sHyperLink.Font.Bold = true;
        sHyperLink.Font.Size = FontUnit.Point(10);
        sHyperLink.Font.Name = "Arial";

        if (!lstPeriod.SelectedItem.Text.ToString().Contains("Full Month"))
        {
            HyperLink lnkAdd = new HyperLink();
            lnkAdd.ApplyStyle(sHyperLink);
            lnkAdd.Text = "Add Bonus";
            lnkAdd.NavigateUrl = "CommUpdate.aspx?Task=AddBonus&Month=" + lstMonth.SelectedItem.Text + "&Period=" + lstPeriod.SelectedItem.Text + "&MasterNum=" + lstRepList.SelectedItem.Value;
            td = new TableCell();
            td.Attributes.Add("ColSpan", "6");
            td.Controls.Add(lnkAdd);
            tr.Cells.Add(td);
            tr.BackColor = System.Drawing.Color.FromArgb(230, 237, 245);
            tblBonus.Rows.Add(tr);
        }

        //Display Monthly Total
        tr = new TableRow();
        td = new TableCell();
        td.Attributes.Add("ColSpan", "3");
        tr.Cells.Add(td);

        //Monthly Total With Bonus Header
        lblValue = new Label();
        lblValue.Text = "Monthly Total With Bonus: ";
        td = new TableCell();
        lblValue.ApplyStyle(ValueLabel);
        lblValue.Font.Size = FontUnit.Point(10);
        lblValue.Font.Bold = true;
        td.Controls.Add(lblValue);
        tr.Cells.Add(td);

        //Bonus Total
        lblValue = new Label();
        lblValue.Text = "$" + fRepFinal.ToString();
        td = new TableCell();
        lblValue.ApplyStyle(ValueLabel);
        lblValue.Font.Size = FontUnit.Point(10);
        lblValue.Font.Bold = true;
        td.Controls.Add(lblValue);
        tr.Cells.Add(td);

        tr.BackColor = System.Drawing.Color.FromArgb(230, 237, 245);
        tblBonus.Rows.Add(tr);
    }//end function PopulateBonus

    
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            pnlDrag.Visible = false;
            pnlFeatures.Visible = false;
            pnlGrid.Visible = false;
            lblError.Visible = false;
            lblMonth.Text = "Commissions for the month of: " + lstMonth.SelectedItem.Text + "-" + lstPeriod.SelectedItem.Text;
            PopulateSummary();
            double fRepTotalSum = PopulateCommissions(lstRepList.SelectedItem.Value, lstMonth.SelectedItem.Value);
            PopulateBonus(fRepTotalSum);
        }
        catch (Exception err)
        {
            CreateLog Log = new CreateLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), err.Message);
            DisplayMessage("Error populating commissions");
        }
    }

    //This function displays error message on a label
    protected void DisplayMessage(string errText)
    {
        lblError.Visible = true;
        lblError.Text = errText;
    }//end function set error message

    protected void btnResetComm_Click(object sender, EventArgs e)
    {
        try
        {
            CommissionsBL Reset = new CommissionsBL();
            bool retVal = Reset.ResetComm(lstRepList.SelectedItem.Value, lstMonth.SelectedItem.Value);
            if (retVal)
                DisplayMessage("Commissions reset");
            else
                DisplayMessage("Could not reset Commissions");
            PopulateSummary();
            double fRepTotalSum = PopulateCommissions(lstRepList.SelectedItem.Value, lstMonth.SelectedItem.Value);
            PopulateBonus(fRepTotalSum);
        }//end try
        catch (Exception err)
        {
            CreateLog Log = new CreateLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), err.Message);
            DisplayMessage("Error resetting commissions");
        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        pnlUploadComm.Visible = true;
        btnUpload.Visible = false;
    }    
    
    protected void btnUploadYes_Click(object sender, EventArgs e)
    {
        try
        {
            btnUpload.Visible = true;
            pnlUploadComm.Visible = false;
           // ACTDataBL Act = new ACTDataBL();
            CommissionsBL Comm = new CommissionsBL();
            CommissionsDL CommDL = new CommissionsDL();
            CommDL.GetPartnerType(Convert.ToInt16(Session["AffiliateID"]));
            //string strMonth = Comm.ReturnCurrMonth();
            string strMonth = lstMonth.SelectedItem.Text;
            string strRetVal = UploadCommissions(strMonth);
            DisplayMessage(strRetVal);
        }//end try
        catch (Exception err)
        {
            CreateLog Log = new CreateLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), err.Message);
            DisplayMessage(err.Message);
        }
    }

    private static string ConnStringACT = ConfigurationManager.AppSettings["ConnectionStringACT"].ToString();

    private static string ConnStringPartner = ConfigurationManager.AppSettings["ConnectionStringPartner"].ToString();
    private static string ConnString = ConfigurationManager.AppSettings["ConnectionString"].ToString();


    public int DeleteCommissions(string Month)
    {
        SqlConnection Conn = new SqlConnection(ConnString);
        try
        {
            SqlCommand cmdComm = new SqlCommand("SP_DeleteCommissions", Conn);
            cmdComm.CommandType = CommandType.StoredProcedure;
            cmdComm.Parameters.Add(new SqlParameter("@Mon", Month));
            cmdComm.Connection.Open();
            int iRetVal = cmdComm.ExecuteNonQuery();
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
    }//end function UpdateCommissionFunds

    //This function Uploads commissions info from ACT to Partner Portal



    public int InsertCommissions(string Month, string RepName, string Company, string DBA, string MerchantID,
                            string ReferralID, string NonAffiliateReferral,
                            string Product, string ProductCode, string COG, string Price,
                            string Units, string CloseDate, string ReferalCategory, string Processor)
    {
        SqlConnection Conn = new SqlConnection(ConnString);
        try
        {
            SqlCommand cmdComm = new SqlCommand("sp_InsertCommissions", Conn);
            cmdComm.CommandType = CommandType.StoredProcedure;
            cmdComm.Parameters.Add(new SqlParameter("@Mon", Month));
            cmdComm.Parameters.Add(new SqlParameter("@RepName", RepName));
            cmdComm.Parameters.Add(new SqlParameter("@Company", Company));
            cmdComm.Parameters.Add(new SqlParameter("@DBA", DBA));
            cmdComm.Parameters.Add(new SqlParameter("@MerchantID", MerchantID));
            cmdComm.Parameters.Add(new SqlParameter("@ReferralID", ReferralID));
            cmdComm.Parameters.Add(new SqlParameter("@NonAffiliateReferral", NonAffiliateReferral));
            cmdComm.Parameters.Add(new SqlParameter("@Product", Product));
            cmdComm.Parameters.Add(new SqlParameter("@ProductCode", ProductCode));
            cmdComm.Parameters.Add(new SqlParameter("@COG", COG));
            cmdComm.Parameters.Add(new SqlParameter("@Price", Price));
            cmdComm.Parameters.Add(new SqlParameter("@Units", Units));
            cmdComm.Parameters.Add(new SqlParameter("@CloseDate", CloseDate));
            cmdComm.Parameters.Add(new SqlParameter("@ReferralCategory", ReferalCategory));
            cmdComm.Parameters.Add(new SqlParameter("@Processor", Processor));

            cmdComm.Connection.Open();
            int iRetVal = cmdComm.ExecuteNonQuery();
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
    }//end function InsertCommissions


    public string UploadCommissions(string Month)
    {

        int ReferralID = 0;
        //Delete Existing commissions data for the current month
        CommissionsDL Comm = new CommissionsDL();
        Comm.DeleteCommissions(Month);

        string Processor;

        //Get Commissions data from ACT
        //ACTDataBL ACT = new ACTDataBL();
        DataSet ds = GetCommissionsFromACT(Month);
        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr = null;
            int iRetval = 0;
            int i = 0;



            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                //Insert Commissions data in Partner Portal DB
                dr = ds.Tables[0].Rows[i];

                Processor = Convert.ToString(dr["Processor"]);

                if (!Convert.IsDBNull(dr["ReferralID"]))
                {
                    if (Convert.ToString(dr["ReferralID"]) != "")
                    {
                        ReferralID = Convert.ToInt32(dr["ReferralID"]);
                    }
                }
                //Get partner Category
                string strReferalCategory = Comm.GetPartnerType(ReferralID);


                // check to ensure that the total = price * quantity since Adjusted Price in ACT Sales Opp
                // might be changed by Sales Rep
                double price = Convert.ToDouble(dr["Price"].ToString().Trim());
                double quantity = Convert.ToDouble(dr["Units"].ToString().Trim());
                decimal pricedec = Convert.ToDecimal(dr["Price"].ToString().Trim());
                decimal cogs = Convert.ToDecimal(dr["COG"].ToString().Trim());
                double subtotal = Math.Round((price * quantity), 2);
                double total = Math.Round(Convert.ToDouble(dr["Total"].ToString().Trim()), 2);
                if (subtotal != total)
                {
                    //Delete commissions uploaded
                    DeleteCommissions(Month);
                    return "Error Uploading Commissions - Recheck " + dr["Product"].ToString().Trim()
                        + " Sales Opportunity for " + dr["Company"].ToString().Trim() + ". Total should equal Price x Quantity.";
                }
                else
                {
                    iRetval = InsertCommissions(
                        Month,
                        dr["RepName"].ToString().Trim(),
                        dr["Company"].ToString().Trim(),
                        dr["DBA"].ToString().Trim(),
                        dr["MerchantID"].ToString().Trim(),
                        dr["ReferralID"].ToString().Trim(),
                        dr["NonAffiliateReferral"].ToString().Trim(),
                        dr["Product"].ToString().Trim(),
                        dr["ProductCode"].ToString().Trim(),
                        String.Format("{0:0.00}", cogs.ToString().Trim()),
                        String.Format("{0:0.00}", pricedec.ToString().Trim()),
                        dr["Units"].ToString().Trim(),
                        dr["ActualCloseDate"].ToString().Trim(),
                        strReferalCategory,
                        Processor);

                    int iRetAgRef = Comm.UpdateAgentReferal(Month, strReferalCategory);
                    if (iRetval == 0)
                        return "Error Uploading Commission - " + dr["Product"].ToString().Trim()
                        + " Sales Opportunity for " + dr["Company"].ToString().Trim() + ".";
                }
            }//end for

            //Reset Commission %
            CommissionsDL Reset = new CommissionsDL();
            Reset.ResetCommissions("ALL", Month);

            //Encrypt Merchant ID
            CommissionsDL EncrID = new CommissionsDL();
            int iRetVal = EncrID.EncryptCommissions(Month);
            if (iRetVal <= 0)
                return "Error Encrypting MerchantIDs";

            //Update Funded Values  



            CommissionsDL Funded = new CommissionsDL();
            UpdateCommissionValues(Month);
            Funded.UpdateCommBeforeJan2012();
            /*
            CommissionsBL Comm = new CommissionsBL();
            PartnerDS.CommissionsDataTable dt = new PartnerDS.CommissionsDataTable();


            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count > 0)
                {
                        
                }
            }
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow drIntuit = null;
                    int iRetval1 = 0;
                    int i1 = 0;

                        

                    for (i1 = 0; i1 < ds.Tables[0].Rows.Count; i++)
                    {
                        string Processor;

                        drIntuit = ds.Tables[0].Rows[i];

                        Processor = Convert.ToString(dr["Processor"]);

                        if (Processor.ToLower().Contains("intuit"))
                        { 
                            UpdateCommissionIntuitValues
                        }
                    }
                }*/

            if (iRetVal <= 0)
                return "Error Updating Commissions Funds";

            //Return Success Message
            return "Successfully Uploaded " + i.ToString() + " Records";
        }//end if count not 0
        else
        {
            return "Cannot find Commissions data in ACT for the Month of " + Month;
        }
    }//end function InsertCommissions


    public int UpdateCommissionValues(string Month)
    {
        SqlConnection Conn = new SqlConnection(ConnString);
        try
        {
            SqlCommand cmdComm = new SqlCommand("SP_UpdateCommissionValues", Conn);
            cmdComm.CommandType = CommandType.StoredProcedure;
            cmdComm.Parameters.Add(new SqlParameter("@Mon", Month));
            //cmdComm.Parameters.Add(new SqlParameter("@Processor", Processor));
            cmdComm.Connection.Open();
            int iRetVal = cmdComm.ExecuteNonQuery();
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
    }//end function UpdateCommissionFunds

    public DataSet GetCommissionsFromACT(string strMonth)
    {


        SqlConnection Conn = new SqlConnection(ConnStringACT);
        try
        {
            /*
            string sqlQuery = "SELECT  * from  VW_SalesOppsCurrMonth WHERE (VW_SalesOppsCurrMonth.ACTUALCLOSEDATE >= CONVERT(DATETIME, CAST(DATEPART(year, CONVERT(DATETIME, @Month)) AS varchar(4)) + '-' + CAST(DATEPART(month, CONVERT(DATETIME, @Month)) AS varchar(2)) + '-1', 102)) AND ( VW_SalesOppsCurrMonth.ACTUALCLOSEDATE< (CASE WHEN DATEPART(month, GETDATE()) = 12 THEN CONVERT(DATETIME, CAST(DATEPART(year, CONVERT(DATETIME, @Month)) + 1 AS varchar(4)) + '-01-01', 102) ELSE CONVERT(DATETIME, CAST(DATEPART(year, CONVERT(DATETIME, @Month)) AS varchar(4))  + '-' + CAST(DATEPART(month, CONVERT(DATETIME, @Month)) + 1 AS varchar(2)) + '-1', 102) END)) AND (VW_SalesOppsCurrMonth.STATUSNUM = 1)";
            SqlCommand cmd = new SqlCommand(sqlQuery, Conn);
            cmd.Connection.Open();
            cmd.Parameters.Add(new SqlParameter("@Month", strMonth));

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            return ds;
            */

            SqlCommand cmdComm = new SqlCommand("SP_GetSalesOppsCurrMonth", Conn);
            cmdComm.CommandType = CommandType.StoredProcedure;

            cmdComm.Parameters.AddWithValue("@Month", strMonth);
            cmdComm.Parameters["@Month"].Value = strMonth;
            //cmdComm.Connection.Open();

            SqlDataAdapter adapter = new SqlDataAdapter(cmdComm);
            //adapter.SelectCommand = cmdComm;
            //DataSet ds = cmdComm.ExecuteNonQuery();

            DataSet ds = new DataSet();
            adapter.Fill(ds);
            return ds;
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
    }//end function ReturnCommissionsFromACT

    protected void btnUploadNo_Click(object sender, EventArgs e)
    {
        pnlUploadComm.Visible = false;
        btnUpload.Visible = true;
    }
}
