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

public partial class Payroll_ResdCommPaymentPrint : System.Web.UI.Page
{
    void Page_Init(object sender, EventArgs e)
    {
        ViewStateUserKey = Session.SessionID;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session.IsNewSession)
            Response.Redirect("~/login.aspx");

        if (!User.IsInRole("Admin") && !User.IsInRole("Employee"))
            Response.Redirect("~/login.aspx");

        if (!Page.IsPostBack)
        {
            if (!User.Identity.IsAuthenticated)
                Response.Redirect("~/login.aspx?Authentication=False");
            try
            {
                string Month = string.Empty;
                string Period = string.Empty;
                string resdMon = string.Empty;
                if (Request.Params.Get("Month") != null)
                    Month = Request.Params.Get("Month");
                if (Request.Params.Get("Period") != null)
                    Period = Request.Params.Get("Period");
                if (Request.Params.Get("ResdMon") != null)
                    resdMon = Request.Params.Get("ResdMon");
                
                int iPeriod = 0;
                if (Period == "Full Month")
                    iPeriod = 0;
                else if (Period == "First Half")
                    iPeriod = 1;
                else if (Period == "Second Half")
                    iPeriod = 2;

                lblMonth.Text = "Residual Payments for the month of: " + resdMon + " & Commissions Payments for the month of: " + Month + "-" + Period;

                PopulateSummary(Month, iPeriod);

            }//end try
            catch (Exception err)
            {
                //CreateLog Log = new CreateLog();
                //Log.ErrorLog(Server.MapPath("~/ErrorLog"), err.Message);
                DisplayMessage(err.Message);
            }
        }//end if not postback
    }//end page load

    decimal CommSubTotal_emp = 0;
    decimal RefSubTotal_emp = 0;
    decimal ResdSubTotal_emp = 0;
    decimal COSubTotal_emp = 0;
    decimal TotalSubTotal_emp = 0;
    decimal PaymentSubTotal_emp = 0;

    decimal CommSubTotal_PrevEmp = 0;
    decimal RefSubTotal_PrevEmp = 0;
    decimal ResdSubTotal_PrevEmp = 0;
    decimal COSubTotal_PrevEmp = 0;
    decimal TotalSubTotal_PrevEmp = 0;
    decimal PaymentSubTotal_PrevEmp = 0;
    decimal BonusSubTotal_PrevEmp = 0;

    decimal CommSubTotal_dd = 0;
    decimal RefSubTotal_dd = 0;
    decimal ResdSubTotal_dd = 0;
    decimal COSubTotal_dd = 0;
    decimal TotalSubTotal_dd = 0;
    decimal PaymentSubTotal_dd = 0;
    decimal BonusSubTotal_dd = 0;

    decimal CommSubTotal_office_DD = 0;
    decimal BonusSubTotal_office_DD = 0;
    decimal RefSubTotal_office_DD = 0;
    decimal ResdSubTotal_office_DD = 0;
    decimal COSubTotal_office_DD = 0;
    decimal TotalSubTotal_office_DD = 0;
    decimal PaymentSubTotal_office_DD = 0;

    decimal BonusSubTotal_bp = 0;

    decimal CommSubTotal_bp = 0;
    decimal RefSubTotal_bp = 0;
    decimal ResdSubTotal_bp = 0;
    decimal COSubTotal_bp = 0;
    decimal TotalSubTotal_bp = 0;
    decimal PaymentSubTotal_bp = 0;

    //This function populates summary
    public void PopulateSummary(string Month, int Period)
    {
        //Style info
        Style ValueLabel = new Style();
        ValueLabel.ForeColor = System.Drawing.Color.Black;
        ValueLabel.Font.Name = "Arial";
        ValueLabel.Font.Size = 14;
        Label lblValue;
        TableRow tr = new TableRow();
        TableCell td = new TableCell();

        decimal outSubTotal = 0;
        decimal FinalCommTotal = 0;
        decimal FinalRefTotal = 0;
        decimal FinalResdTotal = 0;
        decimal FinalCOTotal = 0;
        decimal FinalTotal = 0;
        decimal FinalPaymentTotal = 0;
        decimal FinalBonusTotal = 0;

        //Get Employees Summary        
        CommissionsBL CommSummary = new CommissionsBL();
        DataSet dsEmp = new DataSet();
        dsEmp = CommSummary.GetResdCommPaymentByMonPeriod(1, 0, Month, Period);

        DataSet dsOfficeDD = new DataSet();
        dsOfficeDD = CommSummary.GetResdCommPaymentByMonPeriodOffice(1, Month, Period);

        if (dsEmp.Tables[0].Rows.Count > 0)
        {
            pnlEmployees.Visible = true;
            pnlCurrEmployees.Visible = true;
            grdEmployeeSummary.DataSource = dsEmp;
            grdEmployeeSummary.DataBind();
            for (int i = 0; i < grdEmployeeSummary.Rows.Count; i++)
            {
                if (Decimal.TryParse(grdEmployeeSummary.Rows[i].Cells[6].Text.ToString(), out outSubTotal))
                    CommSubTotal_emp += Convert.ToDecimal(grdEmployeeSummary.Rows[i].Cells[6].Text.Trim());
                if (Decimal.TryParse(grdEmployeeSummary.Rows[i].Cells[8].Text.ToString(), out outSubTotal))
                    RefSubTotal_emp += Convert.ToDecimal(grdEmployeeSummary.Rows[i].Cells[8].Text.Trim());
                if (Decimal.TryParse(grdEmployeeSummary.Rows[i].Cells[9].Text.ToString(), out outSubTotal))
                    ResdSubTotal_emp += Convert.ToDecimal(grdEmployeeSummary.Rows[i].Cells[9].Text.Trim());
                if (Decimal.TryParse(grdEmployeeSummary.Rows[i].Cells[10].Text.ToString(), out outSubTotal))
                    COSubTotal_emp += Convert.ToDecimal(grdEmployeeSummary.Rows[i].Cells[10].Text.Trim());
                if (Decimal.TryParse(grdEmployeeSummary.Rows[i].Cells[11].Text.ToString(), out outSubTotal))
                    TotalSubTotal_emp += Convert.ToDecimal(grdEmployeeSummary.Rows[i].Cells[11].Text.Trim());
                if (Decimal.TryParse(grdEmployeeSummary.Rows[i].Cells[12].Text.ToString(), out outSubTotal))
                    PaymentSubTotal_emp += Convert.ToDecimal(grdEmployeeSummary.Rows[i].Cells[12].Text.Trim());
                grdEmployeeSummary.DataBind();
            }
        }
        else
        {
            CommSubTotal_emp = 0;
            RefSubTotal_emp = 0;
            ResdSubTotal_emp = 0;
            COSubTotal_emp = 0;
            TotalSubTotal_emp = 0;
            PaymentSubTotal_emp = 0;
            outSubTotal = 0;
            grdEmployeeSummary.DataSource = null;
            pnlCurrEmployees.Visible = false;
        }

        FinalCommTotal += CommSubTotal_emp;
        FinalRefTotal += RefSubTotal_emp;
        FinalResdTotal += ResdSubTotal_emp;
        FinalCOTotal += COSubTotal_emp;
        FinalTotal += TotalSubTotal_emp;
        FinalPaymentTotal += PaymentSubTotal_emp;
        

        //Get Previous Employees Summary   
        outSubTotal = 0;
        CommSummary = new CommissionsBL();
        //DataSet dsPrevEmp = CommSummary.GetCommRefPaymentByDD(2, 0, Month);
        DataSet dsPrevEmp = new DataSet();
        dsPrevEmp = CommSummary.GetResdCommPaymentByMonPeriod(2, 0, Month, Period);

        if (dsPrevEmp.Tables[0].Rows.Count > 0)
        {
            pnlEmployees.Visible = true;
            pnlPrevEmployees.Visible = true;
            grdPrevEmployeeSummary.DataSource = dsPrevEmp;
            grdPrevEmployeeSummary.DataBind();
            for (int i = 0; i < grdPrevEmployeeSummary.Rows.Count; i++)
            {
                if (Decimal.TryParse(grdPrevEmployeeSummary.Rows[i].Cells[6].Text.ToString(), out outSubTotal))
                    CommSubTotal_PrevEmp += Convert.ToDecimal(grdPrevEmployeeSummary.Rows[i].Cells[6].Text.Trim());
                if (Decimal.TryParse(grdPrevEmployeeSummary.Rows[i].Cells[7].Text.ToString(), out outSubTotal))
                    BonusSubTotal_PrevEmp += Convert.ToDecimal(grdPrevEmployeeSummary.Rows[i].Cells[7].Text.Trim());
                if (Decimal.TryParse(grdPrevEmployeeSummary.Rows[i].Cells[8].Text.ToString(), out outSubTotal))
                    RefSubTotal_PrevEmp += Convert.ToDecimal(grdPrevEmployeeSummary.Rows[i].Cells[8].Text.Trim());
                if (Decimal.TryParse(grdPrevEmployeeSummary.Rows[i].Cells[9].Text.ToString(), out outSubTotal))
                    ResdSubTotal_PrevEmp += Convert.ToDecimal(grdPrevEmployeeSummary.Rows[i].Cells[9].Text.Trim());
                if (Decimal.TryParse(grdPrevEmployeeSummary.Rows[i].Cells[10].Text.ToString(), out outSubTotal))
                    COSubTotal_PrevEmp += Convert.ToDecimal(grdPrevEmployeeSummary.Rows[i].Cells[10].Text.Trim());
                if (Decimal.TryParse(grdPrevEmployeeSummary.Rows[i].Cells[11].Text.ToString(), out outSubTotal))
                    TotalSubTotal_PrevEmp += Convert.ToDecimal(grdPrevEmployeeSummary.Rows[i].Cells[11].Text.Trim());
                if (Decimal.TryParse(grdPrevEmployeeSummary.Rows[i].Cells[12].Text.ToString(), out outSubTotal))
                    PaymentSubTotal_PrevEmp += Convert.ToDecimal(grdPrevEmployeeSummary.Rows[i].Cells[12].Text.Trim());
                grdPrevEmployeeSummary.DataBind();
            }
        }
        else
        {
            CommSubTotal_PrevEmp = 0;
            RefSubTotal_PrevEmp = 0;
            ResdSubTotal_PrevEmp = 0;
            COSubTotal_PrevEmp = 0;
            TotalSubTotal_PrevEmp = 0;
            PaymentSubTotal_PrevEmp = 0;
            BonusSubTotal_PrevEmp = 0;
            outSubTotal = 0;
            grdPrevEmployeeSummary.DataSource = null;
            pnlPrevEmployees.Visible = false;
        }

        FinalCommTotal += CommSubTotal_PrevEmp;
        FinalRefTotal += RefSubTotal_PrevEmp;
        FinalResdTotal += ResdSubTotal_PrevEmp;
        FinalCOTotal += COSubTotal_PrevEmp;
        FinalTotal += TotalSubTotal_PrevEmp;
        FinalPaymentTotal += PaymentSubTotal_PrevEmp;
        FinalBonusTotal += BonusSubTotal_PrevEmp;

        if ((dsEmp.Tables[0].Rows.Count <= 0) && (dsPrevEmp.Tables[0].Rows.Count <= 0))
            pnlEmployees.Visible = false;

        //Get Partners Summary        
        outSubTotal = 0;
        CommSummary = new CommissionsBL();
        //DataSet ds = CommSummary.GetCommRefPaymentByDD(0, 1, Month);
        DataSet ds = new DataSet();
        ds = CommSummary.GetResdCommPaymentByMonPeriod(0, 1, Month, Period);
        if (ds.Tables[0].Rows.Count > 0)
        {
            pnlPartners.Visible = true;
            pnlDDPartner.Visible = true;
            grdSummaryDD.DataSource = ds;
            grdSummaryDD.DataBind();
            for (int i = 0; i < grdSummaryDD.Rows.Count; i++)
            {
                if (Decimal.TryParse(grdSummaryDD.Rows[i].Cells[6].Text.ToString(), out outSubTotal))
                    CommSubTotal_dd += Convert.ToDecimal(grdSummaryDD.Rows[i].Cells[6].Text.Trim());
                if (Decimal.TryParse(grdSummaryDD.Rows[i].Cells[8].Text.ToString(), out outSubTotal))
                    RefSubTotal_dd += Convert.ToDecimal(grdSummaryDD.Rows[i].Cells[8].Text.Trim());
                if (Decimal.TryParse(grdSummaryDD.Rows[i].Cells[9].Text.ToString(), out outSubTotal))
                    ResdSubTotal_dd += Convert.ToDecimal(grdSummaryDD.Rows[i].Cells[9].Text.Trim());
                if (Decimal.TryParse(grdSummaryDD.Rows[i].Cells[10].Text.ToString(), out outSubTotal))
                    COSubTotal_dd += Convert.ToDecimal(grdSummaryDD.Rows[i].Cells[10].Text.Trim());
                if (Decimal.TryParse(grdSummaryDD.Rows[i].Cells[11].Text.ToString(), out outSubTotal))
                    TotalSubTotal_dd += Convert.ToDecimal(grdSummaryDD.Rows[i].Cells[11].Text.Trim());
                if (Decimal.TryParse(grdSummaryDD.Rows[i].Cells[12].Text.ToString(), out outSubTotal))
                    PaymentSubTotal_dd += Convert.ToDecimal(grdSummaryDD.Rows[i].Cells[12].Text.Trim());
                if (Decimal.TryParse(grdSummaryDD.Rows[i].Cells[7].Text.ToString(), out outSubTotal))
                    BonusSubTotal_dd += Convert.ToDecimal(grdSummaryDD.Rows[i].Cells[7].Text.Trim());

                COSubTotal_dd = Convert.ToDecimal(Math.Round(COSubTotal_dd, 2));
                
                grdSummaryDD.DataBind();
            }
        }
        else
        {
            CommSubTotal_dd = 0;
            RefSubTotal_dd = 0;
            ResdSubTotal_dd = 0;
            COSubTotal_dd = 0;
            TotalSubTotal_dd = 0;
            PaymentSubTotal_dd = 0;
            outSubTotal = 0;
            BonusSubTotal_dd = 0;
            grdSummaryDD.DataSource = null;
            pnlDDPartner.Visible = false;
        }

        FinalCommTotal += CommSubTotal_dd;
        FinalRefTotal += RefSubTotal_dd;
        FinalResdTotal += ResdSubTotal_dd;
        FinalCOTotal += COSubTotal_dd;
        FinalTotal += TotalSubTotal_dd;
        FinalPaymentTotal += PaymentSubTotal_dd;
        FinalBonusTotal += BonusSubTotal_dd;


        if (dsOfficeDD.Tables[0].Rows.Count > 0)
        {
            pnlPartners.Visible = true;
            pnlOfficeDD.Visible = true;
            grdSummaryOfficeDD.DataSource = dsOfficeDD;
            grdSummaryOfficeDD.DataBind();
            for (int i = 0; i < grdSummaryOfficeDD.Rows.Count; i++)
            {
                if (Decimal.TryParse(grdSummaryOfficeDD.Rows[i].Cells[6].Text.ToString(), out outSubTotal))
                    CommSubTotal_office_DD += Convert.ToDecimal(grdSummaryOfficeDD.Rows[i].Cells[6].Text.Trim());
                if (Decimal.TryParse(grdSummaryOfficeDD.Rows[i].Cells[8].Text.ToString(), out outSubTotal))
                    RefSubTotal_office_DD += Convert.ToDecimal(grdSummaryOfficeDD.Rows[i].Cells[8].Text.Trim());
                if (Decimal.TryParse(grdSummaryOfficeDD.Rows[i].Cells[9].Text.ToString(), out outSubTotal))
                    ResdSubTotal_office_DD += Convert.ToDecimal(grdSummaryOfficeDD.Rows[i].Cells[9].Text.Trim());
                if (Decimal.TryParse(grdSummaryOfficeDD.Rows[i].Cells[10].Text.ToString(), out outSubTotal))
                    COSubTotal_office_DD += Convert.ToDecimal(grdSummaryOfficeDD.Rows[i].Cells[10].Text.Trim());
                if (Decimal.TryParse(grdSummaryOfficeDD.Rows[i].Cells[11].Text.ToString(), out outSubTotal))
                    TotalSubTotal_office_DD += Convert.ToDecimal(grdSummaryOfficeDD.Rows[i].Cells[11].Text.Trim());
                if (Decimal.TryParse(grdSummaryOfficeDD.Rows[i].Cells[12].Text.ToString(), out outSubTotal))
                    PaymentSubTotal_office_DD += Convert.ToDecimal(grdSummaryOfficeDD.Rows[i].Cells[12].Text.Trim());

                COSubTotal_dd = Convert.ToDecimal(Math.Round(COSubTotal_dd, 2));
            }

            grdSummaryOfficeDD.DataBind();
        }




        //Bill Pay
        outSubTotal = 0;
        CommSummary = new CommissionsBL();
        //DataSet dsBP = CommSummary.GetCommRefPaymentByDD(0, 0, Month);
        DataSet dsBP = new DataSet();
        dsBP = CommSummary.GetResdCommPaymentByMonPeriod(0, 0, Month, Period);
        if (dsBP.Tables[0].Rows.Count > 0)
        {
            pnlPartners.Visible = true;
            pnlBPPartners.Visible = true;
            grdSummaryBP.DataSource = dsBP;
            grdSummaryBP.DataBind();
            for (int i = 0; i < grdSummaryBP.Rows.Count; i++)
            {
                if (Decimal.TryParse(grdSummaryBP.Rows[i].Cells[6].Text.ToString(), out outSubTotal))
                    CommSubTotal_bp += Convert.ToDecimal(grdSummaryBP.Rows[i].Cells[6].Text.Trim());
                if (Decimal.TryParse(grdSummaryBP.Rows[i].Cells[7].Text.ToString(), out outSubTotal))
                    BonusSubTotal_bp += Convert.ToDecimal(grdSummaryBP.Rows[i].Cells[7].Text.Trim());
                if (Decimal.TryParse(grdSummaryBP.Rows[i].Cells[8].Text.ToString(), out outSubTotal))
                    RefSubTotal_bp += Convert.ToDecimal(grdSummaryBP.Rows[i].Cells[8].Text.Trim());
                if (Decimal.TryParse(grdSummaryBP.Rows[i].Cells[9].Text.ToString(), out outSubTotal))
                    ResdSubTotal_bp += Convert.ToDecimal(grdSummaryBP.Rows[i].Cells[9].Text.Trim());
                if (Decimal.TryParse(grdSummaryBP.Rows[i].Cells[10].Text.ToString(), out outSubTotal))
                    COSubTotal_bp += Convert.ToDecimal(grdSummaryBP.Rows[i].Cells[10].Text.Trim());
                if (Decimal.TryParse(grdSummaryBP.Rows[i].Cells[11].Text.ToString(), out outSubTotal))
                    TotalSubTotal_bp += Convert.ToDecimal(grdSummaryBP.Rows[i].Cells[11].Text.Trim());
                if (Decimal.TryParse(grdSummaryBP.Rows[i].Cells[12].Text.ToString(), out outSubTotal))
                    PaymentSubTotal_bp += Convert.ToDecimal(grdSummaryBP.Rows[i].Cells[12].Text.Trim());
                grdSummaryBP.DataBind();
            }
        }
        else
        {
            CommSubTotal_bp = 0;
            RefSubTotal_bp = 0;
            BonusSubTotal_bp = 0;
            ResdSubTotal_bp = 0;
            COSubTotal_bp = 0;
            TotalSubTotal_bp = 0;
            PaymentSubTotal_bp = 0;
            outSubTotal = 0;
            grdSummaryBP.DataSource = null;
            pnlBPPartners.Visible = false;
        }

        FinalCommTotal += CommSubTotal_bp;
        FinalRefTotal += RefSubTotal_bp;
        FinalResdTotal += ResdSubTotal_bp;
        FinalCOTotal += COSubTotal_bp;
        FinalTotal += TotalSubTotal_bp;
        FinalPaymentTotal += PaymentSubTotal_bp;
        FinalBonusTotal += BonusSubTotal_bp;

        if ((ds.Tables[0].Rows.Count <= 0) && (dsBP.Tables[0].Rows.Count <= 0))
            pnlPartners.Visible = false;

        if ((Convert.ToDouble(FinalCommTotal) == 0.00) && (Convert.ToDouble(FinalRefTotal) == 0.00) && (Convert.ToDouble(FinalResdTotal) == 0.00) && (Convert.ToDouble(FinalCOTotal) == 0.00) && (Convert.ToDouble(FinalTotal) == 0.00))
        {
            tblBPTotal.Visible = false;
            DisplayMessage("No Payment details found.");
        }
        else
        {
            tr = new TableRow();
            td = new TableCell();
            td.Attributes.Add("ColSpan", "6");
            //tr.Cells.Add(td);
            tblBPTotal.Rows.Add(tr);

            lblValue = new Label();
            lblValue.Text = "GRAND TOTALS: ";
            td = new TableCell();
            //td.Width = 100;
            //lblValue.ApplyStyle(ValueLabel);
            td.Attributes.Add("ColSpan", "6");
            td.Attributes.Add("align", "left");
            lblValue.Font.Size = FontUnit.Point(10);
            lblValue.Font.Bold = true;
            lblValue.Font.Name = "Arial";
            td.Controls.Add(lblValue);
            tr.Cells.Add(td);

            lblValue = new Label();
            lblValue.Text = "$" + FinalCommTotal.ToString();
            td = new TableCell();
            //lblValue.ApplyStyle(ValueLabel);
            lblValue.Font.Size = 9;
            lblValue.Font.Bold = true;
            lblValue.Font.Name = "Arial";
            td.Width = 80;
            td.Attributes.Add("align", "center");
            td.Controls.Add(lblValue);
            tr.Cells.Add(td);

            lblValue = new Label();
            lblValue.Text = "$" + FinalBonusTotal.ToString();
            td = new TableCell();
            //lblValue.ApplyStyle(ValueLabel);
            lblValue.Font.Size = 9;
            lblValue.Font.Bold = true;
            lblValue.Font.Name = "Arial";
            td.Width = 50;
            td.Attributes.Add("align", "center");
            td.Controls.Add(lblValue);
            tr.Cells.Add(td);



            lblValue = new Label();
            lblValue.Text = "$" + FinalRefTotal.ToString();
            td = new TableCell();
            //lblValue.ApplyStyle(ValueLabel);
            lblValue.Font.Size = 9;
            lblValue.Font.Bold = true;
            lblValue.Font.Name = "Arial";
            td.Width = 65;
            td.Attributes.Add("align", "center");
            td.Controls.Add(lblValue);
            tr.Cells.Add(td);

            lblValue = new Label();
            lblValue.Text = "$" + FinalResdTotal.ToString();
            td = new TableCell();
            //lblValue.ApplyStyle(ValueLabel);
            lblValue.Font.Size = 9;
            lblValue.Font.Bold = true;
            lblValue.Font.Name = "Arial";
            td.Width = 65;
            td.Attributes.Add("align", "center");
            td.Controls.Add(lblValue);
            tr.Cells.Add(td);

            lblValue = new Label();
            //lblValue.Text = "$" + FinalCOTotal.ToString();
            lblValue.Text = "$" + Convert.ToString(Math.Round(FinalCOTotal, 2));
            td = new TableCell();
            //lblValue.ApplyStyle(ValueLabel);
            lblValue.Font.Size = 9;
            lblValue.Font.Bold = true;
            lblValue.Font.Name = "Arial";
            td.Width = 65;
            td.Attributes.Add("align", "center");
            td.Controls.Add(lblValue);
            tr.Cells.Add(td);

            lblValue = new Label();
            lblValue.Text = "$" + FinalTotal.ToString();
            td = new TableCell();
            //lblValue.ApplyStyle(ValueLabel);
            lblValue.Font.Size = 9;
            lblValue.Font.Bold = true;
            lblValue.Font.Name = "Arial";
            td.Width = 50;
            td.Attributes.Add("align", "center");
            td.Controls.Add(lblValue);
            tr.Cells.Add(td);

            lblValue = new Label();
            lblValue.Text = "$" + FinalPaymentTotal.ToString();
            td = new TableCell();
            //lblValue.ApplyStyle(ValueLabel);
            lblValue.Font.Size = 9;
            lblValue.Font.Bold = true;
            lblValue.Font.Name = "Arial";
            td.Width = 65;
            td.Attributes.Add("align", "center");
            td.Controls.Add(lblValue);
            tr.Cells.Add(td);
        }
    }

    //This function displays error message on a label
    protected void DisplayMessage(string errText)
    {
        lblError.Visible = true;
        lblError.Text = errText;
    }//end function set error message

    protected void grdSummaryDD_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                decimal CommTotal = 0;
                decimal BonusTotal = 0;
                decimal ReferralTotal = 0;
                decimal ResidualTotal = 0;
                decimal CarryOver = 0;

                if (Decimal.TryParse(e.Row.Cells[6].Text.Trim(), out CommTotal))
                    CommTotal = Convert.ToDecimal(e.Row.Cells[6].Text.Trim());
                if (Decimal.TryParse(e.Row.Cells[7].Text.Trim(), out BonusTotal))
                    BonusTotal = Convert.ToDecimal(e.Row.Cells[7].Text.Trim());
                if (Decimal.TryParse(e.Row.Cells[8].Text.Trim(), out ReferralTotal))
                    ReferralTotal = Convert.ToDecimal(e.Row.Cells[8].Text.Trim());
                if (Decimal.TryParse(e.Row.Cells[9].Text.Trim(), out ResidualTotal))
                    ResidualTotal = Convert.ToDecimal(e.Row.Cells[9].Text.Trim());
                if (Decimal.TryParse(e.Row.Cells[10].Text.Trim(), out CarryOver))
                    CarryOver = Convert.ToDecimal(e.Row.Cells[10].Text.Trim());
                e.Row.Cells[11].Text = Convert.ToString(CommTotal + BonusTotal + ReferralTotal + ResidualTotal + CarryOver);
                /*if ((CommTotal + BonusTotal + ReferralTotal + CarryOver) > 0)
                    e.Row.Cells[12].Text = Convert.ToString(CommTotal + BonusTotal + ReferralTotal + CarryOver);
                else
                    e.Row.Cells[12].Text = "0";*/
            }//end if DataRow
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[6].Text = "$" + Convert.ToString(CommSubTotal_dd);
                e.Row.Cells[8].Text = "$" + Convert.ToString(RefSubTotal_dd);
                e.Row.Cells[9].Text = "$" + Convert.ToString(ResdSubTotal_dd);
                e.Row.Cells[10].Text = "$" + Convert.ToString(COSubTotal_dd);
                e.Row.Cells[11].Text = "$" + Convert.ToString(TotalSubTotal_dd);
                e.Row.Cells[12].Text = "$" + Convert.ToString(PaymentSubTotal_dd);
            }
        }//end try
        catch (Exception err)
        {
            //CreateLog Log = new CreateLog();
            //Log.ErrorLog(Server.MapPath("~/ErrorLog"), err.Message);
            DisplayMessage(err.Message);
        }
    }

    protected void grdSummaryDD_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.ToString() == "Confirmation")
            {
                //pnlConfirmation.Visible = true;
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow grdRow = grdSummaryDD.Rows[index];
                int AffiliateID = Convert.ToInt16(Server.HtmlDecode(grdRow.Cells[4].Text));
                //pnlConfirmation.Visible = true;
                //PopulateUpdate(AffiliateID, Convert.ToInt32(lstPeriod.SelectedItem.Value));
            }
        }//end try
        catch (Exception err)
        {
            DisplayMessage(err.Message);
        }
    }

    protected void grdSummaryOfficeDD_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                decimal CommTotal = 0;
                decimal BonusTotal = 0;
                decimal ReferralTotal = 0;
                decimal ResidualTotal = 0;
                decimal CarryOver = 0;

                //e.Row.Cells[6].Text = Convert.ToString(officeCompany);

                if (Decimal.TryParse(e.Row.Cells[6].Text.Trim(), out CommTotal))
                    CommTotal = Convert.ToDecimal(e.Row.Cells[6].Text.Trim());
                if (Decimal.TryParse(e.Row.Cells[7].Text.Trim(), out BonusTotal))
                    BonusTotal = Convert.ToDecimal(e.Row.Cells[7].Text.Trim());
                if (Decimal.TryParse(e.Row.Cells[8].Text.Trim(), out ReferralTotal))
                    ReferralTotal = Convert.ToDecimal(e.Row.Cells[8].Text.Trim());
                if (Decimal.TryParse(e.Row.Cells[9].Text.Trim(), out ResidualTotal))
                    ResidualTotal = Convert.ToDecimal(e.Row.Cells[9].Text.Trim());
                if (Decimal.TryParse(e.Row.Cells[10].Text.Trim(), out CarryOver))
                    CarryOver = Convert.ToDecimal(e.Row.Cells[10].Text.Trim());
                e.Row.Cells[11].Text = Convert.ToString(CommTotal + BonusTotal + ReferralTotal + ResidualTotal + CarryOver);
                /*if ((CommTotal + BonusTotal + ReferralTotal + CarryOver) > 0)
                    e.Row.Cells[12].Text = Convert.ToString(CommTotal + BonusTotal + ReferralTotal + CarryOver);
                else 
                    e.Row.Cells[12].Text = "0";*/
            }//end if DataRow

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[6].Text = "$" + Convert.ToString(CommSubTotal_office_DD);
                e.Row.Cells[7].Text = "$" + Convert.ToString(BonusSubTotal_office_DD);
                e.Row.Cells[8].Text = "$" + Convert.ToString(RefSubTotal_office_DD);
                e.Row.Cells[9].Text = "$" + Convert.ToString(ResdSubTotal_office_DD);
                e.Row.Cells[10].Text = "$" + Convert.ToString(COSubTotal_office_DD);
                e.Row.Cells[11].Text = "$" + Convert.ToString(TotalSubTotal_office_DD);
                e.Row.Cells[12].Text = "$" + Convert.ToString(PaymentSubTotal_office_DD);
            }


        }//end try
        catch (Exception err)
        {
            //CreateLog Log = new CreateLog();
            //Log.ErrorLog(Server.MapPath("~/ErrorLog"), err.Message);
            DisplayMessage(err.Message);
        }
    }

    protected void grdSummaryBP_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                decimal CommTotal = 0;
                decimal BonusTotal = 0;
                decimal ReferralTotal = 0;
                decimal ResidualTotal = 0;
                decimal CarryOver = 0;
                if (Decimal.TryParse(e.Row.Cells[6].Text.Trim(), out CommTotal))
                    CommTotal = Convert.ToDecimal(e.Row.Cells[6].Text.Trim());
                if (Decimal.TryParse(e.Row.Cells[7].Text.Trim(), out BonusTotal))
                    BonusTotal = Convert.ToDecimal(e.Row.Cells[7].Text.Trim());
                if (Decimal.TryParse(e.Row.Cells[8].Text.Trim(), out ReferralTotal))
                    ReferralTotal = Convert.ToDecimal(e.Row.Cells[8].Text.Trim());
                if (Decimal.TryParse(e.Row.Cells[9].Text.Trim(), out ResidualTotal))
                    ResidualTotal = Convert.ToDecimal(e.Row.Cells[9].Text.Trim());
                if (Decimal.TryParse(e.Row.Cells[10].Text.Trim(), out CarryOver))
                    CarryOver = Convert.ToDecimal(e.Row.Cells[10].Text.Trim());
                e.Row.Cells[11].Text = Convert.ToString(decimal.Round(Convert.ToDecimal(CommTotal + BonusTotal + ReferralTotal + ResidualTotal + CarryOver), 2));
                /*if ((CommTotal + BonusTotal + ReferralTotal + CarryOver) > 0)
                    e.Row.Cells[12].Text = Convert.ToString(CommTotal + BonusTotal + ReferralTotal + CarryOver);
                else
                    e.Row.Cells[12].Text = "0";*/
            }//end if DataRow
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[6].Text = "$" + Convert.ToString(CommSubTotal_bp);
                e.Row.Cells[8].Text = "$" + Convert.ToString(RefSubTotal_bp);
                e.Row.Cells[9].Text = "$" + Convert.ToString(ResdSubTotal_bp);
                e.Row.Cells[10].Text = "$" + Convert.ToString(COSubTotal_bp);
                e.Row.Cells[11].Text = "$" + Convert.ToString(TotalSubTotal_bp);
                e.Row.Cells[12].Text = "$" + Convert.ToString(PaymentSubTotal_bp);
            }
        }//end try
        catch (Exception err)
        {
            //CreateLog Log = new CreateLog();
            //Log.ErrorLog(Server.MapPath("~/ErrorLog"), err.Message);
            DisplayMessage(err.Message);
        }
    }

    protected void grdEmployeeSummary_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                decimal CommTotal = 0;
                decimal BonusTotal = 0;
                decimal ReferralTotal = 0;
                decimal ResidualTotal = 0;
                decimal CarryOver = 0;
                if (Decimal.TryParse(e.Row.Cells[6].Text.Trim(), out CommTotal))
                    CommTotal = Convert.ToDecimal(e.Row.Cells[6].Text.Trim());
                if (Decimal.TryParse(e.Row.Cells[7].Text.Trim(), out BonusTotal))
                    BonusTotal = Convert.ToDecimal(e.Row.Cells[7].Text.Trim());
                if (Decimal.TryParse(e.Row.Cells[8].Text.Trim(), out ReferralTotal))
                    ReferralTotal = Convert.ToDecimal(e.Row.Cells[8].Text.Trim());
                if (Decimal.TryParse(e.Row.Cells[9].Text.Trim(), out ResidualTotal))
                    ResidualTotal = Convert.ToDecimal(e.Row.Cells[9].Text.Trim());
                if (Decimal.TryParse(e.Row.Cells[10].Text.Trim(), out CarryOver))
                    CarryOver = Convert.ToDecimal(e.Row.Cells[10].Text.Trim());
                e.Row.Cells[11].Text = Convert.ToString(CommTotal + BonusTotal + ReferralTotal + ResidualTotal + CarryOver);
                /*if ((CommTotal + BonusTotal + ReferralTotal + CarryOver) > 0)
                    e.Row.Cells[12].Text = Convert.ToString(CommTotal + BonusTotal + ReferralTotal + CarryOver);
                else
                    e.Row.Cells[12].Text = "0";*/
            }//end if DataRow
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[6].Text = "$" + Convert.ToString(CommSubTotal_emp);
                e.Row.Cells[8].Text = "$" + Convert.ToString(RefSubTotal_emp);
                e.Row.Cells[9].Text = "$" + Convert.ToString(ResdSubTotal_emp);
                e.Row.Cells[10].Text = "$" + Convert.ToString(COSubTotal_emp);
                e.Row.Cells[11].Text = "$" + Convert.ToString(TotalSubTotal_emp);
                e.Row.Cells[12].Text = "$" + Convert.ToString(PaymentSubTotal_emp);
            }
        }//end try
        catch (Exception err)
        {
            //CreateLog Log = new CreateLog();
            //Log.ErrorLog(Server.MapPath("~/ErrorLog"), err.Message);
            DisplayMessage(err.Message);
        }
    }

    protected void grdPrevEmployeeSummary_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                decimal CommTotal = 0;
                decimal BonusTotal = 0;
                decimal ReferralTotal = 0;
                decimal ResidualTotal = 0;
                decimal CarryOver = 0;
                if (Decimal.TryParse(e.Row.Cells[6].Text.Trim(), out CommTotal))
                    CommTotal = Convert.ToDecimal(e.Row.Cells[6].Text.Trim());
                if (Decimal.TryParse(e.Row.Cells[7].Text.Trim(), out BonusTotal))
                    BonusTotal = Convert.ToDecimal(e.Row.Cells[7].Text.Trim());
                if (Decimal.TryParse(e.Row.Cells[8].Text.Trim(), out ReferralTotal))
                    ReferralTotal = Convert.ToDecimal(e.Row.Cells[8].Text.Trim());
                if (Decimal.TryParse(e.Row.Cells[9].Text.Trim(), out ResidualTotal))
                    ResidualTotal = Convert.ToDecimal(e.Row.Cells[9].Text.Trim());
                if (Decimal.TryParse(e.Row.Cells[10].Text.Trim(), out CarryOver))
                    CarryOver = Convert.ToDecimal(e.Row.Cells[10].Text.Trim());
                e.Row.Cells[11].Text = Convert.ToString(CommTotal + BonusTotal + ReferralTotal + ResidualTotal + CarryOver);
                /*if ((CommTotal + BonusTotal + ReferralTotal + CarryOver) > 0)
                    e.Row.Cells[12].Text = Convert.ToString(CommTotal + BonusTotal + ReferralTotal + CarryOver);
                else
                    e.Row.Cells[12].Text = "0";*/
            }//end if DataRow
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[6].Text = "$" + Convert.ToString(CommSubTotal_PrevEmp);
                e.Row.Cells[8].Text = "$" + Convert.ToString(RefSubTotal_PrevEmp);
                e.Row.Cells[9].Text = "$" + Convert.ToString(ResdSubTotal_PrevEmp);
                e.Row.Cells[10].Text = "$" + Convert.ToString(COSubTotal_PrevEmp);
                e.Row.Cells[11].Text = "$" + Convert.ToString(TotalSubTotal_PrevEmp);
                e.Row.Cells[12].Text = "$" + Convert.ToString(PaymentSubTotal_PrevEmp);
            }
        }//end try
        catch (Exception err)
        {
            //CreateLog Log = new CreateLog();
            //Log.ErrorLog(Server.MapPath("~/ErrorLog"), err.Message);
            DisplayMessage(err.Message);
        }
    }
}
