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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Style TextArea = new Style();
            TextArea.Width = new Unit(200);
            TextArea.Height = new Unit(80);
            TextArea.Font.Size = FontUnit.Point(8);
            TextArea.Font.Name = "Arial";
            txtComments.ApplyStyle(TextArea);

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
                Populate();
            }
            catch (Exception err)
            {
                lblError.Visible = true;
                lblError.Text = err.Message;
            }
        }
    }

    public void Populate()
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
    }

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
                        Reports.InsertFreeApply(AffiliateID.ToString(), txtFirstName.Text.Trim(), txtLastName.Text.Trim(),
                            txtApplyEmail.Text.Trim(), txtCountryCodeHome.Text.Trim(), txtHomePhone.Text.Trim(), txtWorkPhone.Text.Trim(),
                            txtMobilePhone.Text.Trim(), txtCompany.Text.Trim(), txtAddress.Text.Trim(),
                            txtCity.Text.Trim(), lstState.SelectedItem.Text.Trim(), txtRegion.Text.Trim(),
                            txtZipCode.Text.Trim(), lstCountry.SelectedItem.Text.Trim(), txtURL.Text.Trim(),
                            strCart, txtComments.Text.Trim());
                        //send emails
                        SendEmailToUser();
                        SendEmailToAdmin();
                        SendEmailToPartner();
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

    //This function validates data in text fields
    protected bool ValidateData()
    {
        TextBox txtBox = new TextBox();
        for (int i = 0; i < pnlMainPage.Controls.Count; i++)
        {
            if (pnlMainPage.Controls[i].GetType() == txtBox.GetType())
            {
                txtBox = (TextBox)pnlMainPage.Controls[i];
                if ((txtBox.Text.Contains("--")) || (txtBox.Text.Contains("#")) || (txtBox.Text.Contains(";")) || (txtBox.Text.Contains("'")) || (txtBox.Text.ToLower().Contains("script")))
                {
                    txtBox.BackColor = System.Drawing.Color.Ivory;
                    lblError.Visible = true;
                    lblError.Text = "You cannot use hyphens, apostrophe, # or semi-colons in any of the following fields.";
                    txtBox.Focus();
                    return false;
                }//end if
            }//end if
        }//end for
        return true;
    }//end function validate data

    //This function emails user
    public void SendEmailToUser()
    {
        try
        {
            //Send Email to agent after successful registration before redirecting user
            string strSubject = "Commerce Technologies - Thank You";
            MailMessage msg = new MailMessage();
            //msg.To = txtApplyEmail.Text.Trim();
            msg.To.Add(new MailAddress(txtApplyEmail.Text.Trim()));
            msg.From = new MailAddress("information@ecenow.com");
            msg.Subject = strSubject;
            msg.Body = GetBody();
            msg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            SmtpClient mSmtpClient = new SmtpClient();
            mSmtpClient.Send(msg);
        }//end try
        catch (Exception err)
        {
            lblError.Visible = true;
            lblError.Text = err.Message;
        }
    }//end function send email

    //This function emails Admin
    public void SendEmailToAdmin()
    {
        try
        {
            string strCart = "No";
            if (rdbYes.Checked)
                strCart = "Yes";
            int AffiliateID = 11;
            HttpCookie ck = Request.Cookies["AffiliateWizAffiliateID"];
            if (ck != null)
                AffiliateID = Convert.ToInt32(ck["AffiliateID"].ToString().Trim());
            string CompanyName = string.Empty;
            //Get Affiliate Name from Affiliate ID
            AffiliatesWizBL Affiliate = new AffiliatesWizBL();
            DataSet ds = Affiliate.GetAffiliateInfoBasic(AffiliateID);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                CompanyName = dr["CompanyName"].ToString().Trim();
            }
            //Send Email to agent after successful registration before redirecting user
            string strSubject = "Commerce Technologies Free Consultation - New Lead from Referral Source: " + CompanyName;
            MailMessage msg = new MailMessage();
            msg.To.Add(new MailAddress("web@ecenow.com"));
            //msg.From = "information@ecenow.com";
            msg.From = new MailAddress("web@ecenow.com");
            msg.Subject = strSubject;
            msg.Body = "User Submitted the Following Information" + System.Environment.NewLine + System.Environment.NewLine
                + "First Name : " + txtFirstName.Text.Trim() + System.Environment.NewLine
                + "Last Name : " + txtLastName.Text.Trim() + System.Environment.NewLine
                + "Email : " + txtApplyEmail.Text.Trim() + System.Environment.NewLine
                + "Home Phone : " + txtCountryCodeHome.Text.Trim() + "-" + txtHomePhone.Text.Trim() + System.Environment.NewLine
                + "Work Phone : " + txtCountryCodeHome.Text.Trim() + "-" + txtWorkPhone.Text.Trim() + System.Environment.NewLine
                + "Mobile Phone : " + txtCountryCodeHome.Text.Trim() + "-" + txtMobilePhone.Text.Trim() + System.Environment.NewLine
                + "Company : " + txtCompany.Text.Trim() + System.Environment.NewLine
                + "Address : " + txtAddress.Text.Trim() + System.Environment.NewLine
                + "City : " + txtCity.Text.Trim() + System.Environment.NewLine
                + "State : " + lstState.SelectedItem.Text + System.Environment.NewLine
                + "Zip : " + txtZipCode.Text.Trim() + System.Environment.NewLine
                + "Country : " + lstCountry.SelectedItem.Text.Trim() + System.Environment.NewLine
                + "Web Site : " + txtURL.Text.Trim() + System.Environment.NewLine
                + "Shopping Cart : " + strCart + System.Environment.NewLine
                + "Comments : " + txtComments.Text.Trim() + System.Environment.NewLine;
            //SmtpMail.Send(msg);
            SmtpClient mSmtpClient = new SmtpClient();
            mSmtpClient.Send(msg);
        }//end try
        catch (Exception err)
        {
            lblError.Visible = true;
            lblError.Text = err.Message;
        }
    }//end function send email

    //This function emails Partner
    public void SendEmailToPartner()
    {
        try
        {
            string strCart = "No";
            if (rdbYes.Checked)
                strCart = "Yes";
            int AffiliateID = 11;
            HttpCookie ck = Request.Cookies["AffiliateWizAffiliateID"];
            if (ck != null)
                AffiliateID = Convert.ToInt32(ck["AffiliateID"].ToString().Trim());
            string CompanyName = string.Empty;
            string Email = "web@ecenow.com";
            //Get Affiliate Name from Affiliate ID
            AffiliatesWizBL Affiliate = new AffiliatesWizBL();
            DataSet ds = Affiliate.GetAffiliateInfoBasic(AffiliateID);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                CompanyName = dr["CompanyName"].ToString().Trim();
                Email = dr["Email"].ToString().Trim();
            }
            //Send Email to agent after successful registration before redirecting user
            string strSubject = "Commerce Technologies Free Consultation - New Lead from Referral Source: " + CompanyName;
            MailMessage msg = new MailMessage();
            //msg.To = Email;
            msg.To.Add(new MailAddress(Email));
            //msg.From = "information@ecenow.com";
            msg.From = new MailAddress("web@ecenow.com");
            msg.Subject = strSubject;
            msg.Body = "User Submitted the Following Information" + System.Environment.NewLine + System.Environment.NewLine
                + "First name : " + txtFirstName.Text.Trim() + System.Environment.NewLine
                + "Last Name : " + txtLastName.Text.Trim() + System.Environment.NewLine
                + "Email : " + txtApplyEmail.Text.Trim() + System.Environment.NewLine
                + "Home Phone : " + txtCountryCodeHome.Text.Trim() + " " + txtHomePhone.Text.Trim() + System.Environment.NewLine
                + "Work Phone : " + txtCountryCodeHome.Text.Trim() + "-" + txtWorkPhone.Text.Trim() + System.Environment.NewLine
                + "Mobile Phone : " + txtCountryCodeHome.Text.Trim() + "-" + txtMobilePhone.Text.Trim() + System.Environment.NewLine
                + "Company : " + txtCompany.Text.Trim() + System.Environment.NewLine
                + "Address : " + txtAddress.Text.Trim() + System.Environment.NewLine
                + "City : " + txtCity.Text.Trim() + System.Environment.NewLine
                + "State : " + lstState.SelectedItem.Text + System.Environment.NewLine
                + "Zip : " + txtZipCode.Text.Trim() + System.Environment.NewLine
                + "Country : " + lstCountry.SelectedItem.Text.Trim() + System.Environment.NewLine
                + "Web Site : " + txtURL.Text.Trim() + System.Environment.NewLine
                + "Shopping Cart : " + strCart + System.Environment.NewLine
                + "Comments : " + txtComments.Text.Trim() + System.Environment.NewLine;
            //SmtpMail.Send(msg);
            SmtpClient mSmtpClient = new SmtpClient();
            mSmtpClient.Send(msg);
        }//end try
        catch (Exception err)
        {
            lblError.Visible = true;
            lblError.Text = err.Message;
        }
    }//end function send email

    //This function creates email body
    public string GetBody()
    {
        string strCart = "No";
        if (rdbYes.Checked)
            strCart = "Yes";
        int AffiliateID = 11;
        HttpCookie ck = Request.Cookies["AffiliateWizAffiliateID"];
        if (ck != null)
            AffiliateID = Convert.ToInt32(ck["AffiliateID"].ToString().Trim());
        string Body = "Thank You Very Much For Taking Time to Fill Out Our Form" + System.Environment.NewLine + System.Environment.NewLine
            + "Within 24 hours, you will be contacted by a Sales Consultant to discuss the detail of establishing a merchant account either domestically or international, allowing your business to accept credit cards as soon as possible. If you do not hear from someone in a timely manner or need any additional information please contact our sales department at 1-800-477-5363 or 310-321-5400. Don't forget to ask about our other products such as Point of Sale Equipment including wireless solutions, PC Software, Secure Payment Gateways and services such as Debit Card, Gift Card, EBT, ACH and Check Guarantee." + System.Environment.NewLine + System.Environment.NewLine;
        Body = Body + "You Have Submitted the Following Information" + System.Environment.NewLine + System.Environment.NewLine
            + "First name : " + txtFirstName.Text.Trim() + System.Environment.NewLine
            + "Last Name : " + txtLastName.Text.Trim() + System.Environment.NewLine
            + "Email : " + txtApplyEmail.Text.Trim() + System.Environment.NewLine
            + "Home Phone : " + txtCountryCodeHome.Text.Trim() + " " + txtHomePhone.Text.Trim() + System.Environment.NewLine
            + "Work Phone : " + txtCountryCodeHome.Text.Trim() + "-" + txtWorkPhone.Text.Trim() + System.Environment.NewLine
            + "Mobile Phone : " + txtCountryCodeHome.Text.Trim() + "-" + txtMobilePhone.Text.Trim() + System.Environment.NewLine
            + "Company : " + txtCompany.Text.Trim() + System.Environment.NewLine 
            + "Address : " + txtAddress.Text.Trim() + System.Environment.NewLine 
            + "City : " + txtCity.Text.Trim() + System.Environment.NewLine 
            + "State : " + lstState.SelectedItem.Text + System.Environment.NewLine 
            + "Zip : " + txtZipCode.Text.Trim() + System.Environment.NewLine
            + "Country : " + lstCountry.SelectedItem.Text.Trim() + System.Environment.NewLine
            + "Web Site : " + txtURL.Text.Trim() + System.Environment.NewLine 
            + "Shopping Cart : "  + strCart + System.Environment.NewLine 
            + "Comments : " + txtComments.Text.Trim() + System.Environment.NewLine + System.Environment.NewLine;
        Body = Body + "Sign up for a Merchant Account or a Payment Gateway with Commerce Technologies by clicking on the following link:" + System.Environment.NewLine;
        Body = Body + "https://www.firstaffiliates.com/onlineapplication/start.aspx?Referral=" + AffiliateID.ToString();
        return Body;
    }//end function GetBody

}
