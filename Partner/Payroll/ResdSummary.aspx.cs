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

public partial class Payroll_ResdSummary : System.Web.UI.Page
{
    private static ResidualsAdminBL Resd = new ResidualsAdminBL();
    
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
                MonthBL Mon = new MonthBL();
               DataSet dsMon = Mon.GetMonthListForReports(1, "Residuals");

                if (dsMon.Tables[0].Rows.Count > 0)
                {
                    lstMonth.DataSource = dsMon;
                    lstMonth.DataTextField = "Mon";
                    lstMonth.DataValueField = "Mon";
                    lstMonth.DataBind();
                }

            }//end try
            catch (Exception err)
            {
                //CreateLog Log = new CreateLog();
                //Log.ErrorLog(Server.MapPath("~/ErrorLog"), err.Message);
                DisplayMessage(err.Message);
            }
        }//end if not postback
    }//end page load

    //This function populates summary
    public void PopulateSummary(string Month)
    {
        lnkPrint.Visible = true;
        lnkPrint.NavigateUrl = "ResdSummaryPrint.aspx?Month=" + Month;

        double SubTotal = 0;
        double outSubTotal = 0;
        double FinalTotal = 0;
        //Get Employees Summary        
        Resd = new ResidualsAdminBL(Month);
        DataSet dsEmp = Resd.GetResidualPaymentSummaryByDD(1, 0);
        if (dsEmp.Tables[0].Rows.Count > 0)
        {
            pnlEmployees.Visible = true;
            grdEmployeeSummary.DataSource = dsEmp;
            grdEmployeeSummary.DataBind();
            //sum up all the payment totals in the grid
            for (int i = 0; i < grdEmployeeSummary.Rows.Count; i++)
            {
                if (Double.TryParse(grdEmployeeSummary.Rows[i].Cells[10].Text.ToString(), out outSubTotal))
                    SubTotal = SubTotal + Convert.ToDouble(grdEmployeeSummary.Rows[i].Cells[10].Text.Trim());
            }
        }
        else
        {
            SubTotal = 0;
            outSubTotal = 0;
            grdEmployeeSummary.DataSource = null;
            pnlEmployees.Visible = false;
        }        
        
        lblEmployeeTotal.Text = "Payment Sub Total: " + SubTotal.ToString();
        FinalTotal = FinalTotal + SubTotal;

        //Get Partners Summary        
        SubTotal = 0;
        outSubTotal = 0;
        DataSet ds = Resd.GetResidualPaymentSummaryByDD(0, 1);
        if (ds.Tables[0].Rows.Count > 0)
        {
            pnlPartners.Visible = true;
            pnlDDPartner.Visible = true;
            grdSummaryDD.DataSource = ds;
            grdSummaryDD.DataBind();
            for (int i = 0; i < grdSummaryDD.Rows.Count; i++)
            {
                if (Double.TryParse(grdSummaryDD.Rows[i].Cells[10].Text.ToString(), out outSubTotal))
                    SubTotal = SubTotal + Convert.ToDouble(grdSummaryDD.Rows[i].Cells[10].Text.Trim());
            }
        }
        else
        {
            SubTotal = 0;
            outSubTotal = 0;
            grdSummaryDD.DataSource = null;
            pnlDDPartner.Visible = false;
        }
                
        lblDDTotal.Text = "Payment Sub Total: " + SubTotal.ToString();
        FinalTotal = FinalTotal + SubTotal;

        SubTotal = 0;
        outSubTotal = 0;

        DataSet dsBP = Resd.GetResidualPaymentSummaryByDD(0, 0);
        if (dsBP.Tables[0].Rows.Count > 0)
        {
            pnlPartners.Visible = true;
            pnlBPPartners.Visible = true;
            grdSummaryBP.DataSource = dsBP;
            grdSummaryBP.DataBind();
            for (int i = 0; i < grdSummaryBP.Rows.Count; i++)
            {
                if (Double.TryParse(grdSummaryBP.Rows[i].Cells[10].Text.ToString(), out outSubTotal))
                    SubTotal = SubTotal + Convert.ToDouble(grdSummaryBP.Rows[i].Cells[10].Text.Trim());
            }
        }
        else
        {
            SubTotal = 0;
            outSubTotal = 0;
            grdSummaryBP.DataSource = null;
            pnlBPPartners.Visible = false;
        }
        
        lblBPTotal.Text = "Payment Sub Total: " + SubTotal.ToString();

        if ((ds.Tables[0].Rows.Count <= 0) && (dsBP.Tables[0].Rows.Count <= 0))
            pnlPartners.Visible = false;
        

        FinalTotal = FinalTotal + SubTotal;
        lblFinalTotal.Text = "Payment Total: " + FinalTotal.ToString();
    }

    //This function populates info to be updated
    public void PopulateUpdate(int AffiliateID)
    {
        if (User.IsInRole("Admin"))
        {
            AffiliatesBL Aff = new AffiliatesBL(AffiliateID);
            DataSet ds = Aff.GetResidualPaymentByAffiliateID(lstMonth.SelectedItem.Text);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                lblAffiliateID.Text = AffiliateID.ToString();
                lblCompanyName.Text = dr["CompanyName"].ToString().Trim();
                txtConfirmationCode.Text = dr["ResdConfirmNum"].ToString().Trim();
                txtNote.Text = dr["ResdNote"].ToString().Trim();
                txtCarryOver.Text = dr["CarryOverResd"].ToString().Trim();
                if (dr["ResdConfirmDate"].ToString().Trim() != "")
                    txtDatePaid.Text = dr["ResdConfirmDate"].ToString().Trim();
                else
                    txtDatePaid.Text = DateTime.Now.Date.ToString();
            }//end if count not 0
            else
                DisplayMessage("Rep information not found");
        }//end if user is in role
    }//end function PopulateUpdate

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (User.IsInRole("Admin"))
            {
                //Update product information
                int iRetVal = Resd.InsertUpdateResdConfirm(lblAffiliateID.Text.Trim(), txtConfirmationCode.Text.Trim(), txtNote.Text.Trim(),Convert.ToDecimal(txtCarryOver.Text.Trim()),Convert.ToDecimal(txtDatePaid.Text.Trim()),"");
                pnlConfirmation.Visible = false;
                PopulateSummary(lstMonth.SelectedItem.Text);
            }//end if access
            else
                DisplayMessage("Cannot Update");
        }//end try
        catch (Exception err)
        {
            DisplayMessage(err.Message);
        }
    }

    //This function displays error message on a label
    protected void DisplayMessage(string errText)
    {
        lblError.Visible = true;
        lblError.Text = errText;
    }//end function set error message

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        lblError.Visible = false;
        pnlConfirmation.Visible = false;
    }

    protected void grdSummaryDD_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                double ResdPay = 0;
                double CarryOver = 0;
                if (Double.TryParse(e.Row.Cells[7].Text.Trim(), out ResdPay))
                    ResdPay = Convert.ToDouble(e.Row.Cells[7].Text.Trim());
                if (Double.TryParse(e.Row.Cells[8].Text.Trim(), out CarryOver))
                    CarryOver = Convert.ToDouble(e.Row.Cells[8].Text.Trim());
                e.Row.Cells[9].Text = Convert.ToString(ResdPay + CarryOver);
                if  ((ResdPay + CarryOver)  > 0)
                    e.Row.Cells[10].Text = Convert.ToString(ResdPay + CarryOver);
                else
                    e.Row.Cells[10].Text = "0";
            }//end if DataRow
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
                double ResdPay = 0;
                double CarryOver = 0;
                if (Double.TryParse(e.Row.Cells[7].Text.Trim(), out ResdPay))
                    ResdPay = Convert.ToDouble(e.Row.Cells[7].Text.Trim());
                if (Double.TryParse(e.Row.Cells[8].Text.Trim(), out CarryOver))
                    CarryOver = Convert.ToDouble(e.Row.Cells[8].Text.Trim());
                e.Row.Cells[9].Text = Convert.ToString(ResdPay + CarryOver);
                if ((ResdPay + CarryOver) > 0)
                    e.Row.Cells[10].Text = Convert.ToString(ResdPay + CarryOver);
                else
                    e.Row.Cells[10].Text = "0";
            }//end if DataRow
        }//end try
        catch (Exception err)
        {
            //CreateLog Log = new CreateLog();
            //Log.ErrorLog(Server.MapPath("~/ErrorLog"), err.Message);
            DisplayMessage(err.Message);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            lblMonth.Text = "Residual Payments for the month of: " + lstMonth.SelectedItem.Text;
            PopulateSummary(lstMonth.SelectedItem.Text.Trim());
        }//end try
        catch (Exception err)
        {
            DisplayMessage(err.Message);
        }
    }

    protected void grdEmployeeSummary_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                double ResdPay = 0;
                double CarryOver = 0;
                if (Double.TryParse(e.Row.Cells[7].Text.Trim(), out ResdPay))
                    ResdPay = Convert.ToDouble(e.Row.Cells[7].Text.Trim());
                if (Double.TryParse(e.Row.Cells[8].Text.Trim(), out CarryOver))
                    CarryOver = Convert.ToDouble(e.Row.Cells[8].Text.Trim());
                e.Row.Cells[9].Text = Convert.ToString(ResdPay + CarryOver);
                if ((ResdPay + CarryOver) > 0)
                    e.Row.Cells[10].Text = Convert.ToString(ResdPay + CarryOver);
                else
                    e.Row.Cells[10].Text = "0";

            }//end if DataRow
        }//end try
        catch (Exception err)
        {
            //CreateLog Log = new CreateLog();
            //Log.ErrorLog(Server.MapPath("~/ErrorLog"), err.Message);
            DisplayMessage(err.Message);
        }
    }

    protected void grdEmployeeSummary_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.ToString() == "Confirmation")
            {
                pnlConfirmation.Visible = true;
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow grdRow = grdEmployeeSummary.Rows[index];
                int AffiliateID = Convert.ToInt16 (Server.HtmlDecode(grdRow.Cells[4].Text) );
                pnlConfirmation.Visible = true;
                PopulateUpdate(AffiliateID);
            }
        }//end try
        catch (Exception err)
        {
            DisplayMessage(err.Message);
        }
    }

    protected void grdSummaryDD_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.ToString() == "Confirmation")
            {
                pnlConfirmation.Visible = true;
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow grdRow = grdSummaryDD.Rows[index];
                int AffiliateID = Convert.ToInt16(Server.HtmlDecode(grdRow.Cells[4].Text));
                pnlConfirmation.Visible = true;
                PopulateUpdate(AffiliateID);
            }
        }//end try
        catch (Exception err)
        {
            DisplayMessage(err.Message);
        }
    }

    protected void grdSummaryBP_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.ToString() == "Confirmation")
            {
                pnlConfirmation.Visible = true;
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow grdRow = grdSummaryBP.Rows[index];
                int AffiliateID = Convert.ToInt16(Server.HtmlDecode(grdRow.Cells[4].Text) );
                pnlConfirmation.Visible = true;
                PopulateUpdate(AffiliateID);
            }
        }//end try
        catch (Exception err)
        {
            DisplayMessage(err.Message);
        }
    }
}
