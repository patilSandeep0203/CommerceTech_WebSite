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
using System.Net.Mail;



using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Text;
using System.IO;
using System.Net;
using System.Collections.Specialized;

using System.Xml;
using Newtonsoft.Json;

public partial class Email : System.Web.UI.Page
{
    private static string EmailType = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        txtBody.Attributes.Add("onKeyPress", "return textboxMultilineMaxNumber(this,250);");
        Style TextArea = new Style();
        TextArea.Width = new Unit(300);
        TextArea.Height = new Unit(200);
        TextArea.Font.Size = FontUnit.Point(8);
        txtBody.ApplyStyle(TextArea);

        if ((Request.Params.Get("EmailType") != null))
            EmailType = Request.Params.Get("EmailType");//CS = Customer Support; NA = New Accounts; Sales
                                                        //PS = Partner Support; Accounting; TS = Tech Support

        lblEmailHeader.Attributes.Add("style", "font-weight: bold");

        if (EmailType == "Sales")
        {
            lblEmailHeader.Text = "Sales Inquiries";
            txtSubject.Text = "Sales Inquiry";
        }
        else if (EmailType == "CS")
        {
            lblEmailHeader.Text = "Customer Support Inquiries";
            txtSubject.Text = "Customer Support Inquiry";
        }
        else if (EmailType == "NA")
        {
            lblEmailHeader.Text = "New Accounts Inquiries";
            txtSubject.Text = "New Account(s) Inquiry";
        }
        else if (EmailType == "PS")
        {
            lblEmailHeader.Text = "Partner Support Inquiries";
            txtSubject.Text = "Partner Support Inquiry";
        }
        else if (EmailType == "Accounting")
        {
            lblEmailHeader.Text = "Accounting/HR Inquiries";
            txtSubject.Text = "Accounting/HR Inquiry";
        }
        else if (EmailType == "TS")
        {
            lblEmailHeader.Text = "Technical Support Inquiries";
            txtSubject.Text = "Technical Support Inquiry";
        }
        else
        {
            lblEmailHeader.Text = "General Inquiries";
            txtSubject.Text = "General Inquiry";
        }

        //If the request has a subject then use that subject
        if ((Request.Params.Get("EmailSubject") != null))
            txtSubject.Text = Request.Params.Get("EmailSubject");
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {

                string repCatResult = repCatValidate();

                if (repCatResult == "True")
                {

                    if (ValidateData())
                    {
                        bool retVal = SendEmail();
                        if (retVal)
                        {
                            Response.Redirect("freeleads.aspx");
                        }
                        else
                        {
                            lblError.Visible = true;
                            lblError.Text = "Email send failed. Please try again.";
                        }

                    }//end if validate data
                }
                else
                {
                    lblError.Text = "Please take a moment to verify that you are not a robot.";
                }
            }//end  if page valid
        }//end try
        catch (Exception err)
        {
            lblError.Visible = true;
            lblError.Text = err.Message;//"Error sending email. Please contact Commerce Technologies for further assistance.";                    
        }
    }

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

    //Sends Email
    public bool SendEmail()
    {
        try
        {
            MailMessage msg = new MailMessage();

            //CC email to partner if they have an email address in the database and they have Email Notification enabled
            int AffiliateID = 0;
            HttpCookie ck = Request.Cookies["AffiliateWizAffiliateID"];
            if (ck != null)
                AffiliateID = Convert.ToInt32(ck["AffiliateID"].ToString().Trim());

            string strEmail = string.Empty;
            //Get Affiliate Name from Affiliate ID
            AffiliatesWizBL Partner = new AffiliatesWizBL();
            DataSet ds = Partner.GetAffiliateInfoBasic(AffiliateID);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                if (dr["SendEmailNotification"].ToString().Trim() == "Yes")
                    strEmail = dr["Email"].ToString().Trim();
            }

            if (strEmail != "")
                msg.CC.Add(new MailAddress(strEmail));

            //Based on the type of email inquiry select the appropriate email address
            string Email = "information@ecenow.com";
            if (EmailType == "Sales")
                Email = "information@ecenow.com";
            else if (EmailType == "NA")
                Email = "applications@ecenow.com";
            else if (EmailType == "CS")
                Email = "service@ecenow.com";
            else if (EmailType == "PS")
                Email = "applications@ecenow.com";
            else if (EmailType == "TS")
                Email = "service@ecenow.com";
            else if (EmailType == "Accounting")
                Email = "accounting@ecenow.com";

            msg.To.Add(new MailAddress(Email.Trim()));
            msg.From = new MailAddress(txtEmailForm.Text.ToString().Trim());

            //CC to emailer if box is checked
            //if (chkCC.Checked)
                //msg.CC.Add(new MailAddress(txtEmailForm.Text.ToString().Trim()));

            msg.Subject = txtSubject.Text.ToString().Trim();
            msg.Body = txtBody.Text.ToString().Trim() + "\n\nFull Name: " + txtFullName.Text.ToString().Trim();
            SmtpClient mSmtpClient = new SmtpClient();
            //mSmtpClient.Port = 1025;
            mSmtpClient.Send(msg);
            return true;
        }//end try
        catch (Exception err)
        {
            lblError.Visible = true;
            lblError.Text = err.Message;
            return false;
        }
    }//end function send email

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
}
