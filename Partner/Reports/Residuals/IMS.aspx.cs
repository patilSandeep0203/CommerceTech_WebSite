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
using DLPartner.PartnerDSTableAdapters;

public partial class Residuals_IMS : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (!Session.IsNewSession)
        {
            if (User.IsInRole("Agent") || User.IsInRole("T1Agent"))
                Page.MasterPageFile = "../Agent.master";
            else if (User.IsInRole("Reseller") || User.IsInRole("Affiliate"))
                Page.MasterPageFile = "../Agent.master";
            else if (User.IsInRole("Employee"))
                Page.MasterPageFile = "../Employee.master";
            else if (User.IsInRole("Admin"))
                Page.MasterPageFile = "../Admin.master";
        }
    }

    void Page_Init(object sender, EventArgs e)
    {
        ViewStateUserKey = Session.SessionID;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session.IsNewSession)
            Response.Redirect("~/login.aspx");

        if (!Page.IsPostBack)
        {
            if (!User.Identity.IsAuthenticated)
                Response.Redirect("~/login.aspx?Authentication=False");

            try
            {
                MonthBL Mon = new MonthBL();

                if (User.IsInRole("Admin") || User.IsInRole("Employee"))
                {
                    lstRepList.Visible = true;
                    DataSet dsMon = Mon.GetMonthListForReports(1, "residuals");
                    if (dsMon.Tables[0].Rows.Count > 0)
                    {
                        lstMonth.DataSource = dsMon;
                        lstMonth.DataTextField = "Mon";
                        lstMonth.DataValueField = "Mon";
                        lstMonth.DataBind();
                    }

                    ListBL RepList = new ListBL();
                    DataSet dsRep = RepList.GetRepListForVendor("ims");
                    if (dsRep.Tables[0].Rows.Count > 0)
                    {
                        lstRepList.DataSource = dsRep;
                        lstRepList.DataTextField = "RepName";
                        lstRepList.DataValueField = "MasterNum";
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

                    if ((Month != "") && (MasterNum != "") && (User.IsInRole("Admin") || User.IsInRole("Employee")))
                    {
                        try
                        {
                            lblError.Visible = false;
                            lstMonth.SelectedValue = lstMonth.Items.FindByValue(Month).Value;
                            lblError.Visible = false;
                            lblMonth.Text = "IMS Residuals for the month of: " + lstMonth.SelectedItem.Text;
                            if (lstRepList.Items.FindByValue(MasterNum) != null)
                                lstRepList.SelectedValue = MasterNum;
                            Populate(MasterNum, lstMonth.SelectedItem.Value.ToString().Trim(), false, "");
                        }
                        catch (Exception err)
                        {
                            CreateLog Log = new CreateLog();
                            Log.ErrorLog(Server.MapPath("~/ErrorLog"), err.Message);
                            DisplayMessage("Error Processing Request. Please contact technical support");
                        }
                    }//end if month not null

                }//end if
                else if (User.IsInRole("Office"))
                {
                    lstRepList.Visible = true;
                    //lblRepName.Visible = true;

                    ListItem itemAllOffice = new ListItem();
                    itemAllOffice.Text = "ALL Office";
                    itemAllOffice.Value = "ALL Office";
                    //MonthBL Mon = new MonthBL();
                    ListBL List = new ListBL();
                    RepInfoDL RepListOffice = new RepInfoDL();
                    DataSet dsMon = Mon.GetMonthListForReports(2, "residuals");
                    if (dsMon.Tables[0].Rows.Count > 0)
                    {
                        lstMonth.DataSource = dsMon;
                        lstMonth.DataTextField = "Mon";
                        lstMonth.DataValueField = "Mon";
                        lstMonth.DataBind();
                    }

                    DataSet dsRep = RepListOffice.GetSalesRepListByOffice(Session["MasterNum"].ToString());
                    if (dsRep.Tables[0].Rows.Count > 0)
                    {
                        lstRepList.DataSource = dsRep;
                        lstRepList.DataTextField = "RepName";
                        lstRepList.DataValueField = "MasterNum";
                        lstRepList.DataBind();
                        //lstRepList.SelectedValue = "";
                    }

                    lstRepList.Items.Add(itemAllOffice);

                    string MasterNum = "";
                    string Month = "";
                    if (Request.Params.Get("Month") != null)
                        Month = Request.Params.Get("Month");

                    if (Request.Params.Get("MasterNum") != null)
                        MasterNum = Request.Params.Get("MasterNum");

                    if ((Month != "") && (MasterNum != "") && (User.IsInRole("Admin") || User.IsInRole("Employee") || User.IsInRole("Office")))
                    {
                        try
                        {
                            lblError.Visible = false;
                            lstMonth.SelectedValue = lstMonth.Items.FindByValue(Month).Value;
                            lblError.Visible = false;
                            lblMonth.Text = "IMS Residuals for the month of: " + lstMonth.SelectedItem.Text;
                            if (lstRepList.Items.FindByValue(MasterNum) != null)
                                lstRepList.SelectedValue = MasterNum;
                            Populate(MasterNum, lstMonth.SelectedItem.Value.ToString().Trim(), false, "");
                        }
                        catch (Exception err)
                        {
                            CreateLog Log = new CreateLog();
                            Log.ErrorLog(Server.MapPath("~/ErrorLog"), err.Message);
                            DisplayMessage("Error Processing Request. Please contact technical support");
                        }
                    }//end if month not null

                }
                else if (User.IsInRole("T1Agent"))
                {
                    Tabs.Tabs.Remove(TabPanelLookup);
               
                    DataSet dsMon = Mon.GetMonthListForReports(2, "residuals");
                    if (dsMon.Tables[0].Rows.Count > 0)
                    {
                        lstMonth.DataSource = dsMon;
                        lstMonth.DataTextField = "Mon";
                        lstMonth.DataValueField = "Mon";
                        lstMonth.DataBind();
                    }

                    //Get Tier 2 list for Tier 1 Agent
                    ListBL RepList = new ListBL();
                    DataSet dsRep = RepList.GetRepListForVendorByTier("ims", Session["MasterNum"].ToString());
                    if (dsRep.Tables[0].Rows.Count > 0)
                    {
                        lstRepList.DataSource = dsRep;
                        lstRepList.DataTextField = "RepName";
                        lstRepList.DataValueField = "MasterNum";
                        lstRepList.DataBind();
                    }
                    ListItem item = new ListItem();
                    item.Text = "ALL";
                    item.Value = "ALL";
                    lstRepList.Items.Add(item);
                    //lstRepList.SelectedIndex = lstRepList.Items.IndexOf(item);
                }
                else
                {
                    //Tabs.Tabs.Remove(TabPanelLookup);
                    DataSet dsMon = Mon.GetMonthListForReports(2, "residuals");
                    if (dsMon.Tables[0].Rows.Count > 0)
                    {
                        lstMonth.DataSource = dsMon;
                        lstMonth.DataTextField = "Mon";
                        lstMonth.DataValueField = "Mon";
                        lstMonth.DataBind();
                    }

                    lstRepList.Visible = false;
                    lblSelectRepName.Visible = false;
                }
            }//end try
            catch (Exception)
            {
                DisplayMessage("Error Processing Request. Please contact technical support");
            }
        }//end if not postback

        else
        {
            if (!User.IsInRole("Admin") && !User.IsInRole("Employee"))
                Tabs.Tabs.Remove(TabPanelLookup);
        }
    }//end page load

    //This function populates ipayment residuals
    public void Populate(string MasterNum, string Month, bool bDBA, string DBA)
    {
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

        double ECETotal = 0;
        double RepTotal = 0;

        PartnerDS.IMSDataTable dt = null;
        if (!bDBA)
        {
            ResidualsBL IMSResd = new ResidualsBL(Month, MasterNum);
            DataSet dsTotals = IMSResd.GetIMSTotals();
            if (dsTotals.Tables[0].Rows.Count > 0)
            {
                DataRow dr = dsTotals.Tables[0].Rows[0];
                ECETotal = Convert.ToDouble(dr["IMSecetotal"]);
                RepTotal = Convert.ToDouble(dr["IMSreptotal"]);
            }//end if count not 0
            dt = IMSResd.GetIMSResiduals();
        }
        else
        {
            ResidualsAdminBL IMS = new ResidualsAdminBL(Month);
            dt = IMS.GetIMSResidualsByDBA(DBA);
        }
 
        if (dt.Rows.Count > 0)
        {
            TableRow tr = new TableRow();
            TableCell td = new TableCell();
            Label lblValue;
                        
            string IDHead = "";
            string IDNext = "";            
            string IDHeadMon = "";
            string IDNextMon = "";

            for (int i = 0; i < dt.Rows.Count; )
            {
     
                tr = new TableRow();

                IDHead = dt[i].MerchantID.ToString().Trim();
                IDNext = IDHead;

                IDHeadMon = dt[i].Mon.ToString().Trim();
                IDNextMon = IDHeadMon;

                //tblHeaderInfo = new Table();
                //RepName
                lblValue = new Label();
                if (MasterNum == "ALL")
                    lblValue.Text = dt[i].RepName.ToString().Trim();
                else
                    lblValue.Text = "";
                if (bDBA)
                    lblValue.Text = dt[i].RepName.ToString().Trim() + " (" + dt[i].Mon.ToString().Trim() + ")";
                td = new TableCell();
                lblValue.ApplyStyle(ValueLabel);
                td.Attributes.Add("height", "50px");
                td.Controls.Add(lblValue);
                lblValue.Font.Bold = true;
                tr.Cells.Add(td);

                //Merchant ID
                lblValue = new Label();
                lblValue.Text = "Merchant ID: " + dt[i].trMerchantId.ToString().Trim();
                td = new TableCell();
                td.Attributes.Add("colspan", "8");
                lblValue.ApplyStyle(ValueLabel);
                td.Controls.Add(lblValue);
                lblValue.Font.Bold = true;
                tr.Cells.Add(td);

                //DBA
                lblValue = new Label();
                lblValue.Text = "DBA: " + dt[i].DBA.ToString().Trim();
                td = new TableCell();
                td.Attributes.Add("colspan", "8");
                lblValue.ApplyStyle(ValueLabel);
                td.Controls.Add(lblValue);
                lblValue.Font.Bold = true;
                tr.Cells.Add(td);
                tr.BackColor = System.Drawing.Color.WhiteSmoke;
                tblResiduals.Rows.Add(tr);
                
                #region Header Row

                string[] arrColumns = { "Card Type", "Billing Element", "Volume", "Sold Rate", "Buy Rate", "Profit", 
                "ECE Split with Bank", "ECE Total", "Rep Split", "Rep Total"};

                tr = new TableRow();
                tr.BackColor = System.Drawing.Color.FromArgb(93, 123, 157);
                for (int k = 0; k < arrColumns.Length; k++)
                {
                    td = new TableCell();
                    td.Text = arrColumns[k].ToString();
                    td.Style["font-family"] = "Arial";
                    td.Style["font-size"] = "Small";
                    td.Style["font-weight"] = "Bold";
                    td.Style["Color"] = "White";
                    tr.Cells.Add(td);
                }
                tblResiduals.Rows.Add(tr);

                #endregion

                if (bDBA)
                {
                    ECETotal = 0;
                    RepTotal = 0;
                }
                do
                {                    
                    tr = new TableRow();
                    lblValue = new Label();
                    lblValue.Text = dt[i].CardType.ToString().Trim();
                    td = new TableCell();
                    lblValue.ApplyStyle(ValueLabel);
                    td.Controls.Add(lblValue);
                    tr.Cells.Add(td);

                    lblValue = new Label();
                    lblValue.Text = dt[i].BillingElement.ToString().Trim();
                    td = new TableCell();
                    lblValue.ApplyStyle(ValueLabel);
                    td.Controls.Add(lblValue);
                    tr.Cells.Add(td);

                    lblValue = new Label();
                    lblValue.Text = dt[i].Multiplier.ToString().Trim();
                    td = new TableCell();
                    lblValue.ApplyStyle(ValueLabel);
                    td.Controls.Add(lblValue);
                    tr.Cells.Add(td);

                    lblValue = new Label();
                    lblValue.Text = dt[i].SoldRate.ToString();
                    td = new TableCell();
                    lblValue.ApplyStyle(ValueLabel);
                    td.Controls.Add(lblValue);
                    tr.Cells.Add(td);

                    lblValue = new Label();
                    lblValue.Text = dt[i].BuyRate.ToString();
                    td = new TableCell();
                    lblValue.ApplyStyle(ValueLabel);
                    td.Controls.Add(lblValue);
                    tr.Cells.Add(td);

                    lblValue = new Label();
                    lblValue.Text = dt[i].Profit.ToString();
                    td = new TableCell();
                    lblValue.ApplyStyle(ValueLabel);
                    td.Controls.Add(lblValue);
                    tr.Cells.Add(td);

                    lblValue = new Label();
                    lblValue.Text = dt[i].PaymentTypeValue.ToString().Trim() + "%";
                    td = new TableCell();
                    lblValue.ApplyStyle(ValueLabel);
                    td.Controls.Add(lblValue);
                    tr.Cells.Add(td);

                    lblValue = new Label();
                    lblValue.Text = dt[i].EceTotal.ToString();
                    if (bDBA)
                        ECETotal = ECETotal + Convert.ToDouble(dt[i].EceTotal);
                    td = new TableCell();
                    lblValue.ApplyStyle(ValueLabel);
                    td.Controls.Add(lblValue);
                    tr.Cells.Add(td);

                    lblValue = new Label();
                    lblValue.Text = dt[i].RepSplit.ToString() + "%";
                    td = new TableCell();
                    lblValue.ApplyStyle(ValueLabel);
                    td.Controls.Add(lblValue);
                    tr.Cells.Add(td);

                    lblValue = new Label();
                    lblValue.Text = dt[i].RepTotal.ToString();
                    if (bDBA)
                        RepTotal = RepTotal + Convert.ToDouble(dt[i].RepTotal);
                    td = new TableCell();
                    lblValue.ApplyStyle(ValueLabel);
                    td.Controls.Add(lblValue);
                    tr.Cells.Add(td);
                                   

                    tblResiduals.Rows.Add(tr);
                
                    i = i + 1;
                    if (i == dt.Rows.Count)
                        break;
                    IDNext = dt[i].MerchantID.ToString().Trim();
                    IDNextMon = dt[i].Mon.ToString().Trim();

                    //if ((IDHeadMon != IDNextMon) && bDBA)
                      //  break;

                } while ((IDHead == IDNext) && (IDHeadMon == IDNextMon));//end while current merchant id and next merchant id are same

                //if (!bDBA)
                //{
                    tr = new TableRow();
                    td = new TableCell();
                    td.Attributes.Add("colspan", "6");
                    tr.Cells.Add(td);

                    lblValue = new Label();
                    lblValue.Text = "Sub Total";
                    td = new TableCell();
                    lblValue.ApplyStyle(ValueLabel);
                    lblValue.Font.Bold = true;
                    td.Controls.Add(lblValue);
                    tr.Cells.Add(td);

                    //Get Subtotal
                    ResidualsBL SubTotals = new ResidualsBL(Month, MasterNum);
                    DataSet dsSubTotals = SubTotals.GetIMSSubTotals(IDHead);
                    if (dsSubTotals.Tables[0].Rows.Count > 0)
                    {
                        DataRow drTotals = dsSubTotals.Tables[0].Rows[0];

                        lblValue = new Label();
                        lblValue.Text = "$" + Convert.ToDouble(drTotals["ecetotal"]).ToString();
                        td = new TableCell();
                        lblValue.ApplyStyle(ValueLabel);
                        lblValue.Font.Bold = true;
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        td = new TableCell();
                        tr.Cells.Add(td);

                        lblValue = new Label();
                        lblValue.Text = "$" + Convert.ToDouble(drTotals["reptotal"]).ToString();
                        td = new TableCell();
                        lblValue.ApplyStyle(ValueLabel);
                        lblValue.Font.Bold = true;
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);
                        
                        tblResiduals.Rows.Add(tr);
                    
						//Row for Tier Split and Tier Total Header for the Merchant ID				
						tr = new TableRow();
						td = new TableCell();
						td.Attributes.Add("colspan", "8");
						tr.Cells.Add(td);
					
						lblValue = new Label();
		                lblValue.Text = "Tier Split";
						td = new TableCell();
				        lblValue.ApplyStyle(ValueLabel);
					    lblValue.Font.Bold = true;
						td.Controls.Add(lblValue);
						tr.Cells.Add(td);
						
						lblValue = new Label();
						lblValue.Text = "Tier Total";
						td = new TableCell();
						lblValue.ApplyStyle(ValueLabel);
						lblValue.Font.Bold = true;
						td.Controls.Add(lblValue);
						tr.Cells.Add(td);
						  
                        tblResiduals.Rows.Add(tr);
                        
						//Row for Tier Split and Tier Total Header for the Merchant ID				
						tr = new TableRow();
						td = new TableCell();
						td.Attributes.Add("colspan", "8");
						tr.Cells.Add(td);
										
						lblValue = new Label();
						lblValue.Text = Convert.ToDouble(drTotals["TierSplit"]).ToString() + "%";
						td = new TableCell();
						lblValue.ApplyStyle(ValueLabel);
						lblValue.Font.Bold = true;
						td.Controls.Add(lblValue);
						tr.Cells.Add(td);

				
						lblValue = new Label();
						lblValue.Text = "$" + Convert.ToDouble(drTotals["TierTotal"]).ToString();
						td = new TableCell();
						lblValue.ApplyStyle(ValueLabel);
						lblValue.Font.Bold = true;
						td.Controls.Add(lblValue);
						tr.Cells.Add(td);
						
                                              
                    }//end if subtotals count not 0

                    if (bDBA)
                    {
                        lblValue = new Label();
                        lblValue.Text = "$" + ECETotal.ToString();
                        td = new TableCell();
                        lblValue.ApplyStyle(ValueLabel);
                        lblValue.Font.Bold = true;
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);

                        td = new TableCell();
                        tr.Cells.Add(td);

                        lblValue = new Label();
                        lblValue.Text = "$" + RepTotal.ToString();
                        td = new TableCell();
                        lblValue.ApplyStyle(ValueLabel);
                        lblValue.Font.Bold = true;
                        td.Controls.Add(lblValue);
                        tr.Cells.Add(td);
                    }                       

					tblResiduals.Rows.Add(tr);
					
                    tr = new TableRow();
                    td = new TableCell();
                    td.Attributes.Add("height", "30px");
                    td.Attributes.Add("colspan", "10");
                    tr.Cells.Add(td);
                    tblResiduals.Rows.Add(tr);
                    
                //}
            }//end for

            if (!bDBA)
            {
                tr = new TableRow();
                td = new TableCell();
                td.Attributes.Add("colspan", "6");
                tr.Cells.Add(td);

                lblValue = new Label();
                lblValue.Text = "Total";
                td = new TableCell();
                lblValue.ApplyStyle(ValueLabel);
                lblValue.Font.Bold = true;
                td.Controls.Add(lblValue);
                tr.Cells.Add(td);

                lblValue = new Label();
                lblValue.Text = "$" + ECETotal.ToString();
                td = new TableCell();
                lblValue.ApplyStyle(ValueLabel);
                lblValue.Font.Bold = true;
                td.Controls.Add(lblValue);
                tr.Cells.Add(td);

                td = new TableCell();
                tr.Cells.Add(td);

                lblValue = new Label();
                lblValue.Text = "$" + RepTotal.ToString();
                td = new TableCell();
                lblValue.ApplyStyle(ValueLabel);
                lblValue.Font.Bold = true;
                td.Controls.Add(lblValue);
                tr.Cells.Add(td);

                tr.BackColor = System.Drawing.Color.WhiteSmoke;
                tblResiduals.Rows.Add(tr);
            }
        }//end if count not 0
        else
            DisplayMessage("No Records found.");
    }//end function Populate

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (User.IsInRole("Admin") || User.IsInRole("Employee"))
            {
                lstRepList.Visible = true;
            }

            string Rep = Session["MasterNum"].ToString();
            //if the Rep List is visible, set the Rep to be searched
            if (lstRepList.Visible == true)
                Rep = lstRepList.SelectedValue;
            lblError.Visible = false;
            lblMonth.Visible = true;
            lblMonth.Text = "IMS Residuals for the month of: " + lstMonth.SelectedItem.Text;
            Populate(Rep, lstMonth.SelectedItem.Value.ToString().Trim(), false, "");
        }//end try
        catch (Exception err)
        {
            CreateLog Log = new CreateLog();
            Log.ErrorLog(Server.MapPath("~/ErrorLog"), err.Message);
            DisplayMessage("Error Processing Request. Please contact technical support");
        }
    }

    //This function displays error message on a label
    protected void DisplayMessage(string errText)
    {
        lblError.Visible = true;
        lblError.Text = errText;
    }//end function set error message

    protected void btnLookup_Click(object sender, EventArgs e)
    {
        try
        {
            lblError.Visible = false;
            lblMonth.Visible = false;
            Populate("", "", true, txtLookup.Text.Trim());
        }
        catch (Exception err)
        {
            DisplayMessage(err.Message);
        }
    }
}
