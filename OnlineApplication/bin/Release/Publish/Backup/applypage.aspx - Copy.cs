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
using BLAffiliateWiz;
//using System.Web.Mail;
using System.Net.Mail;

public partial class apply : System.Web.UI.Page
{

    private static string fn = string.Empty;
    private static string ln = "";
    private static string em = "";
    private static string pn = "";
    private static string cp = "";
    private static string ad = "";
    private static string ct = "";
    private static string st = "";
    private static string po = "";
    private static string co = "";
    private static string we = "";
    private static string fb = "";
    private static string strAffiliateID = "11";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            /*Style TextArea = new Style();
            TextArea.Width = new Unit(200);
            TextArea.Height = new Unit(80);
            TextArea.Font.Size = FontUnit.Point(8);
            TextArea.Font.Name = "Arial";
            txtComments.ApplyStyle(TextArea);*/

           

            /*Style errLabel = new Style();
            errLabel.BackColor = System.Drawing.Color.FromArgb(255, 192, 192);
            errLabel.ForeColor = System.Drawing.Color.Black;
            errLabel.BorderColor = System.Drawing.Color.Red;
            errLabel.BorderStyle = BorderStyle.Solid;
            errLabel.BorderWidth = Unit.Pixel(1);
            errLabel.Font.Size = FontUnit.Small;
            lblError.ApplyStyle(errLabel);*/
            try
            {

                if ((Request.Params.Get("AffiliateID") != null))
                {
                    strAffiliateID  = Request.Params.Get("AffiliateID");
                }

                if ((Request.Params.Get("fn") != null))
                {
                    fn = Request.Params.Get("fn");
                }

                if ((Request.Params.Get("ln") != null))
                {
                    ln = Request.Params.Get("ln");
                }

                if ((Request.Params.Get("em") != null))
                {
                    em = Request.Params.Get("em");
                }

                if ((Request.Params.Get("pn") != null))
                {
                    pn = Request.Params.Get("pn");
                }

                if ((Request.Params.Get("cp") != null))
                {
                    cp = Request.Params.Get("cp");
                }

                if ((Request.Params.Get("ad") != null))
                {
                    ad = Request.Params.Get("ad");
                }

                if ((Request.Params.Get("ct") != null))
                {
                    ct = Request.Params.Get("ct");
                }

                if ((Request.Params.Get("st") != null))
                {
                    st = Request.Params.Get("st");
                }

                if ((Request.Params.Get("po") != null))
                {
                    po = Request.Params.Get("po");
                }

                if ((Request.Params.Get("co") != null))
                {
                    co = Request.Params.Get("co");
                }

                if ((Request.Params.Get("we") != null))
                {
                    we = Request.Params.Get("we");
                }

                if ((Request.Params.Get("fb") != null))
                {
                    fb = Request.Params.Get("fb");
                }

                ReportsBL Reports = new ReportsBL();
                //if (!Reports.CheckEmailExists(txtApplyEmail.Text.Trim()))

                Reports.InsertFreeApply(strAffiliateID.ToString(), Convert.ToString(fn).Trim(), Convert.ToString(ln).Trim(),
                        Convert.ToString(em).Trim(), "1", Convert.ToString(pn).Trim(), Convert.ToString(pn).Trim(),
                            Convert.ToString(pn).Trim(), Convert.ToString(cp).Trim(), Convert.ToString(ad).Trim(),
                            Convert.ToString(ct).Trim(), Convert.ToString(st).Trim(), "",
                            Convert.ToString(po).Trim(), Convert.ToString(co).Trim(), "",
                            "", Convert.ToString(fb).Trim());

                Response.Redirect("FreeLeads.aspx");


                /*Reports.InsertFreeApply(AffiliateID.ToString(), txtFirstName.Text.Trim(), txtLastName.Text.Trim(),
                            txtApplyEmail.Text.Trim(), "1", Convert.ToString(pn).Trim(), Convert.ToString(pn).Trim(),
                            Convert.ToString(pn).Trim(), Convert.ToString(cp).Trim(), Convert.ToString(ad).Trim(),
                            Convert.ToString(ct).Trim(), Convert.ToString(st).Trim(), "",
                            Convert.ToString(po).Trim(), Convert.ToString(co).Trim(), "",
                            "", Convert.ToString(fb).Trim());*/

                //Populate();
            }
            catch (Exception err)
            {
                lblError.Visible = true;
                lblError.Text = err.Message;
            }
        }
    }

    /*public void Populate()
    {
        //Get states
        CommonListData States = new CommonListData();
        DataSet dsStates = States.GetCommonData("States");
        if (dsStates.Tables["States"].Rows.Count > 0)
        {
            lstState.DataSource = dsStates.Tables["States"];
            lstState.DataTextField = "StateID";
            lstState.DataValueField = "StateID";
            lstState.DataBind();
        }//end if count not 0

        //Get Countries
        CommonListData Countries = new CommonListData();
        DataSet dsCountry = Countries.GetCommonData("Countries");
        if (dsCountry.Tables["Countries"].Rows.Count > 0)
        {
            lstCountry.DataSource = dsCountry.Tables["Countries"];
            lstCountry.DataTextField = "Country";
            lstCountry.DataValueField = "Country";
            lstCountry.DataBind();
        }//end if count not 0
        lstCountry.SelectedItem.Text = "United States";
        lstCountry.SelectedItem.Value = "United States";
    }*/

    /*
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                if (ValidateData())
                {
                    string strCart = "No";
                    if (rdbYes.Checked)
                        strCart = "Yes";

                    int AffiliateID = 11;
                    HttpCookie ck = Request.Cookies["AffiliateWizAffiliateID"];
                    if (ck != null)
                        AffiliateID = Convert.ToInt32(ck["AffiliateID"].ToString().Trim());
                    //Insert Free Apply Report
                    ReportsBL Reports = new ReportsBL();
                    if (!Reports.CheckEmailExists(txtApplyEmail.Text.Trim()))
                    {
                        
                        //send emails
                        //SendEmailToUser();
                        //SendEmailToAdmin();
                        //SendEmailToPartner();
                        Response.Redirect("FreeLeads.aspx");
                    }
                    else
                    {
                        lblError.Visible = true;
                        lblError.Text = "This Email Address already exists in our system. Please use a different email address.";
                    }
                    
                }//end if validate data
            }//end if page valid
        }//end try
        catch (Exception err)
        {
            lblError.Visible = true;
            lblError.Text = err.Message;
        }
    }
    */
    //This function validates data in text fields


}
