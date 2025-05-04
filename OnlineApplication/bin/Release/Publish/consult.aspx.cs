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

using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Text;
using System.IO;
using System.Net;
using System.Collections.Specialized;

using System.Xml;
using Newtonsoft.Json;

public partial class consult : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

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
                    txtBox.Focus();
                    txtBox.BackColor = System.Drawing.Color.Ivory;
                    lblError.Visible = true;
                    lblError.Text = "You cannot use hyphens, apostrophe, # or semi-colons in any of the following fields.";
                    return false;
                }//end if
            }//end if
        }//end if
        return true;
    }//end function validate data

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        string repCatResult = repCatValidate();

        try
        {
            if (Page.IsValid)
            {
                if (repCatResult == "True")
                {
                    if (ValidateData())
                    {
                        //Check if email exists
                        ReportsBL Report = new ReportsBL();
                        bool bEmail = Report.CheckEmailExists(txtConsultEmail.Text.Trim());
                        if (!bEmail)
                        {
                            int AffiliateID = 11;
                            HttpCookie ck = Request.Cookies["AffiliateWizAffiliateID"];
                            if (ck != null)
                                AffiliateID = Convert.ToInt32(ck["AffiliateID"].ToString().Trim());
                            //Insert Data
                            Report.InsertFreeConsult(AffiliateID.ToString(), txtFirstName.Text.Trim(),
                                txtLastName.Text.Trim(), txtConsultEmail.Text.Trim(),
                                txtCountryCode.Text.Trim(), txtHomePhone.Text.Trim(), txtWorkPhone.Text.Trim());

                            //Send Email                    
                            SendEmailToUser();
                            //SendEmailToAdmin();
                            SendEmailToPartner();
                            Response.Redirect("FreeLeads.aspx");
                        }
                        else
                        {
                            lblError.Visible = true;
                            lblError.Text = "This Email Address already exists in our system. Please use a different email address.";//"Error sending email. Please contact Commerce Technologies for further assistance.
                        }
                    }//end if validate data
                }
                else {
                    lblError.Text = "Please take a moment to verify that you are not a robot.";
                }
            }//end  if page valid
        }//end try
        catch (Exception err)
        {
            lblError.Visible = true;
            //lblError.Text = "Error Processing Request. Please contact technical support";
            lblError.Text = err.Message;//"Error sending email. Please contact Commerce Technologies for further assistance.";                    
        }
    }

    //This function emails user
    public void SendEmailToUser()
    {
        try
        {
            //Send Email to agent after successful registration before redirecting user
            string strSubject = "Commerce Technologies - Thank You";
            MailMessage msg = new MailMessage();
            //msg.To = txtConsultEmail.Text.Trim();
            msg.To.Add(new MailAddress(txtConsultEmail.Text.Trim()));
            //msg.From = "information@ecenow.com";
            msg.From = new MailAddress("information@ecenow.com");
            msg.Subject = strSubject;
            msg.Body = GetBody();
            msg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
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

    //This function emails Admin
    public void SendEmailToAdmin()
    {
        try
        {
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
            //msg.To = "information@ecenow.com";
            msg.To.Add(new MailAddress("information@ecenow.com"));
            //msg.From = "information@ecenow.com";
            msg.From = new MailAddress("information@ecenow.com");
            msg.Subject = strSubject;
            msg.Body = "User Submitted the Following Information" + System.Environment.NewLine + System.Environment.NewLine
                + "First name : " + txtFirstName.Text.Trim() + System.Environment.NewLine
                + "Last Name : " + txtLastName.Text.Trim() + System.Environment.NewLine
                + "Email : " + txtConsultEmail.Text.Trim() + System.Environment.NewLine
                + "Phone : " + txtCountryCode.Text.Trim() + "-" + txtHomePhone.Text.Trim() + System.Environment.NewLine
                + "Work Phone : " + txtCountryCode.Text.Trim() + "-" + txtWorkPhone.Text.Trim() + System.Environment.NewLine;
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

    public string repCatValidate()
    {
        string Response = Request["g-recaptcha-response"];//Getting Response String Append to Post Method
        bool Valid = false;
        string test = "";
        //Request to Google Server
        HttpWebRequest req = (HttpWebRequest)WebRequest.Create
        (" https://www.google.com/recaptcha/api/siteverify?secret=6LdWm_8SAAAAACmh-whon6eOJtoawooV_pvsVa3r&response=" + Response);
        try
        {
            //Google recaptcha Response
            using (WebResponse webResponse = req.GetResponse())
            {

                using (StreamReader sr = new StreamReader(webResponse.GetResponseStream()))
                /*{
                    string jsonResponse = readStream.ReadToEnd();

                    JavaScriptSerializer js = new JavaScriptSerializer();
                    MyObject data = js.Deserialize<MyObject>(jsonResponse);// Deserialize Json

                    Valid = Convert.ToBoolean(data.success);
                }*/

                //HttpWebResponse webResponse = (HttpWebResponse)req.GetResponse();
                //StreamReader sr = new StreamReader(webResponse.GetResponseStream());
                {
                    string responseText = sr.ReadToEnd();

                    using (JsonTextReader reader = new JsonTextReader(new StringReader(responseText)))
                    {
                        while (reader.Read())
                        {
                            if (reader.TokenType == JsonToken.PropertyName)
                            {
                                if (reader.Value.ToString() == "success")
                                {
                                    reader.Read();
                                    test = reader.Value.ToString();
                                    //url1 = reader.Value.ToString();
                                }
                            }
                        }
                    }
                }
            }

            return test;
        }
        catch (WebException ex)
        {
            throw ex;
        }
    }

    //This function emails Partner and CCs Admin
    public void SendEmailToPartner()
    {
        try
        {
            int AffiliateID = 11;
            HttpCookie ck = Request.Cookies["AffiliateWizAffiliateID"];
            if (ck != null)
                AffiliateID = Convert.ToInt32(ck["AffiliateID"].ToString().Trim());
            string CompanyName = string.Empty;
            MailMessage msg = new MailMessage();
            string Email = "information@ecenow.com";
            //Get Affiliate Name from Affiliate ID
            AffiliatesWizBL Affiliate = new AffiliatesWizBL();
            DataSet ds = Affiliate.GetAffiliateInfoBasic(AffiliateID);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                if (dr["SendEmailNotification"].ToString().Trim() == "True")
                {                    
                    CompanyName = dr["CompanyName"].ToString().Trim();
                    Email = dr["Email"].ToString().Trim();
                    msg.CC.Add(new MailAddress("information@ecenow.com"));
                    //msg.CC.Add(new MailAddress("support@ecenow.com"));
                }
            }
            //Send Email to agent after successful registration before redirecting user
            string strSubject = "Commerce Technologies Free Consultation - New Lead from Referral Source: " + CompanyName;
            
            //msg.To = Email;
            msg.To.Add(new MailAddress(Email));
            //msg.From = "information@ecenow.com";
            msg.From = new MailAddress("information@ecenow.com");
            msg.Subject = strSubject;
            msg.Body = "User Submitted the Following Information" + System.Environment.NewLine + System.Environment.NewLine
                + "First name : " + txtFirstName.Text.Trim() + System.Environment.NewLine
                + "Last Name : " + txtLastName.Text.Trim() + System.Environment.NewLine
                + "Email : " + txtConsultEmail.Text.Trim() + System.Environment.NewLine
                + "Phone : " + txtCountryCode.Text.Trim() + "-" + txtHomePhone.Text.Trim() + System.Environment.NewLine
                + "Work Phone : " + txtCountryCode.Text.Trim() + "-" + txtWorkPhone.Text.Trim() + System.Environment.NewLine;
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
        int AffiliateID = 11;
        HttpCookie ck = Request.Cookies["AffiliateWizAffiliateID"];
        if (ck != null)
            AffiliateID = Convert.ToInt32(ck["AffiliateID"].ToString().Trim());
        string Body = 	"Thank You Very Much For Taking Time to Fill Out Our Form"  + System.Environment.NewLine  + System.Environment.NewLine
            + "Within 24 hours, you will be contacted by a Sales Consultant to help you obtain any additional information to establish a merchant account or any of our other products or services. If you would like to speak with someone now you may contact us at 1-800-477-5363 or you can begin the application process by clicking" + System.Environment.NewLine + System.Environment.NewLine;
        Body = Body + "https://www.firstaffiliates.com/onlineapplication/start.aspx?Referral=" + AffiliateID.ToString() + System.Environment.NewLine + System.Environment.NewLine;
        Body = Body + "You Have Submitted the Following Information" + System.Environment.NewLine + System.Environment.NewLine
            + "First name : " + txtFirstName.Text.Trim() + System.Environment.NewLine
            + "Last Name : " + txtLastName.Text.Trim() + System.Environment.NewLine
            + "Email : " + txtConsultEmail.Text.Trim() + System.Environment.NewLine
            + "Phone : " + txtCountryCode.Text.Trim() + "-" + txtHomePhone.Text.Trim() + System.Environment.NewLine
            + "Work Phone : " + txtCountryCode.Text.Trim() + "-" + txtWorkPhone.Text.Trim() + System.Environment.NewLine;

        Body = Body + "For any additional questions or concers please visit " + "https://commercetech.com/" + " or call (800)-477-5363" + System.Environment.NewLine;

        return Body;
    }//end function GetBody

}
