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
using System.Text.RegularExpressions;
//using Newtonsoft.Json;


using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Text;
using System.IO;
using System.Net;
using System.Collections.Specialized;

using System.Xml;
using Newtonsoft.Json;


public partial class Join : System.Web.UI.Page
{
    private string strOtherReferral = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Style errLabel = new Style();
            errLabel.BackColor = System.Drawing.Color.FromArgb(255, 192, 192);
            errLabel.ForeColor = System.Drawing.Color.Black;
            errLabel.BorderColor = System.Drawing.Color.Red;
            errLabel.BorderStyle = BorderStyle.Solid;
            errLabel.BorderWidth = Unit.Pixel(1);
            errLabel.Font.Size = FontUnit.Small;
            lblError.ApplyStyle(errLabel);

            Style TextArea = new Style();
            TextArea.Width = new Unit(140);
            TextArea.Height = new Unit(50);
            TextArea.Font.Size = FontUnit.Point(8);
            TextArea.Font.Name = "Arial";
            txtComments.ApplyStyle(TextArea);

            Style ValueLabelHeader = new Style();
            ValueLabelHeader.ForeColor = System.Drawing.Color.White;
            ValueLabelHeader.Font.Size = FontUnit.Small;
            ValueLabelHeader.Font.Bold = true;
            ValueLabelHeader.Font.Name = "Arial";

            string SignupType = "Affiliate";
            if (Request.Params.Get("SignupType") != null)
                SignupType = Request.Params.Get("SignupType");
            if ((SignupType != "Reseller") && (SignupType != "Agent"))
                SignupType = "Affiliate";
            lblSignup.Text = SignupType;
            lblSignup.ApplyStyle(ValueLabelHeader);
            lblType.ApplyStyle(ValueLabelHeader);

            if ((SignupType == "Agent") || (SignupType == "Reseller"))
            {                
                lnkAuthorizationForm.Visible = true;
                Label2.Visible = true;
            }

            if (SignupType == "Agent")
                lnkAgreement.NavigateUrl = "https://commercetechnologies.sharepoint.com/:w:/s/Sales/EaSRNiAetoVKnkJGDY5HSRwB5Onunn4N0HCUSX9uEDNepA?e=Srvs2a";
            else if (SignupType == "Reseller")
                lnkAgreement.NavigateUrl = "https://commercetechnologies.sharepoint.com/:w:/s/Sales/EQc3e28devxBhR9Tei08_rIBfD-4VlPfpacuNLQr-OBLGg?e=w6nAqh";
            else // (SignupType == "Affiliate")
            {
                lnkAgreement.NavigateUrl = "agreement.aspx";
                Label6.Text = "Open, read and click 'I Agree' on the Agreement.";
            }
            try
            {
                Populate();
            }
            catch (Exception err)
            {
                SetErrorMessage("Error Processing Request. Please contact Technical Support for assistance." + err.Message);
            }

        }//end if postback
        else
        {
            txtPassword.Attributes.Add("value", txtPassword.Text);
            txtPasswordConfirm.Attributes.Add("value", txtPasswordConfirm.Text);
        }
    }

    public string repCatValidate()
    {
        string Response = Request["g-recaptcha-response"];//Getting Response String Append to Post Method
        bool Valid = false;
        string test = "";
        //Request to Google Server
        HttpWebRequest req = (HttpWebRequest)WebRequest.Create
        (" https://www.google.com/recaptcha/api/siteverify?secret=6Ld4rqEaAAAAALIzt5eRSGBZDiE4GrzENpCP0CvP&response=" + Response);
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

    //This function populates the form fields
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

            lstBankState.DataSource = dsStates.Tables["States"];
            lstBankState.DataTextField = "StateID";
            lstBankState.DataValueField = "StateID";
            lstBankState.DataBind();

            lstShippingState.DataSource = dsStates.Tables["States"];
            lstShippingState.DataTextField = "StateID";
            lstShippingState.DataValueField = "StateID";
            lstShippingState.DataBind();

        }//end if count not 0

        //Get Countries
        CommonListData Countries = new CommonListData();
        DataSet dsCountry = Countries.GetList("Countries");
        if (dsCountry.Tables["Countries"].Rows.Count > 0)
        {
            lstCountry.DataSource = dsCountry.Tables["Countries"];
            lstCountry.DataTextField = "Country";
            lstCountry.DataValueField = "Country";
            lstCountry.DataBind();

            lstShippingCountry.DataSource = dsCountry.Tables["Countries"];
            lstShippingCountry.DataTextField = "Country";
            lstShippingCountry.DataValueField = "Country";
            lstShippingCountry.DataBind();
        }//end if count not 0

        lstCountry.SelectedItem.Text = "United States";
        lstCountry.SelectedItem.Value = "United States";

        lstShippingCountry.SelectedItem.Text = "United States";
        lstShippingCountry.SelectedItem.Value = "United States";

        //Get Referral
        CommonListData Referrals = new CommonListData();
        DataSet dsReferrals = Referrals.GetList("Referrals");
        if (dsReferrals.Tables["Referrals"].Rows.Count > 0)
        {
            lstReferral.DataSource = dsReferrals.Tables["Referrals"];
            lstReferral.DataTextField = "Referrals";
            lstReferral.DataValueField = "Referrals";
            lstReferral.DataBind();
        }//end if count not 0
        lstReferral.SelectedItem.Text = "Other";
        lstReferral.SelectedItem.Value = "Other";
        txtSpecify.Enabled = true;

        //if session has no cookie (Referral ID not specified)
        //stringQuery
        string queryString = Request.QueryString["A"];
        //Request.Cookies["AffiliateWizAffiliateID"]
        if (queryString == "null")
        {
            //Enable Panel for specifying referral source and enable its validation
            pnlReferral.Visible = true;
            RFVReferral.Enabled = true;
        }

    }//end function Populate
    
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        //string repCatResult = repCatValidate();
        string repCatResult = "True";

        if (txtPhone.Enabled == true)
        {
            string strPhone = txtPhone.Text.Trim();
            //try
            //{
            Regex regexObj = new Regex(@"[^0-9]");

            strPhone = regexObj.Replace(strPhone, "").Trim();
            if (strPhone.Length == 10)
            {
                strPhone = strPhone.Insert(3, "-");

                strPhone = strPhone.Insert(7, "-");
            }

            txtPhone.Text = strPhone;

            REValidatePhone.Enabled = false;

            //}
            //catch (ArgumentException ex)
            //{
            // Syntax error in the regular expression
            //}
        }

        if (txtHomePhone.Enabled == true)
        {
            string strHomePhone = txtHomePhone.Text.Trim();
            //try
            //{
            Regex regexObj = new Regex(@"[^0-9]");

            strHomePhone = regexObj.Replace(strHomePhone, "").Trim();

            if (strHomePhone.Length >= 10)
            {
                strHomePhone = strHomePhone.Insert(3, "-");

                strHomePhone = strHomePhone.Insert(7, "-");
            }


            txtHomePhone.Text = strHomePhone;

            //}
            //catch (ArgumentException ex)
            //{
            // Syntax error in the regular expression
            //}
        }

        if (txtMobilePhone.Enabled == true)
        {
            string strMobilePhone = txtMobilePhone.Text.Trim();
            //try
            //{
            Regex regexObj = new Regex(@"[^0-9]");

            strMobilePhone = regexObj.Replace(strMobilePhone, "").Trim();

            if (strMobilePhone.Length >= 10)
            {
                strMobilePhone = strMobilePhone.Insert(3, "-");

                strMobilePhone = strMobilePhone.Insert(7, "-");
            }


            txtMobilePhone.Text = strMobilePhone;

            //}
            //catch (ArgumentException ex)
            //{
            // Syntax error in the regular expression
            //}
        }

        if (txtFax.Enabled == true)
        {
            string strFax = txtFax.Text.Trim();
            //try
            //{
            Regex regexObj = new Regex(@"[^0-9]");

            strFax = regexObj.Replace(strFax, "").Trim();

            if (strFax.Length >= 10)
            {

                strFax = strFax.Insert(3, "-");

                strFax = strFax.Insert(7, "-");
            }


            txtFax.Text = strFax;

            RegularExpressionValidator1.Enabled = false;

            //}
            //catch (ArgumentException ex)
            //{
            // Syntax error in the regular expression
            //}
        }

        if (txtTaxSSN.Enabled == true)
        {
            string strTaxSSN = txtTaxSSN.Text.Trim();
            //try
            //{
            Regex regexObj = new Regex(@"[^0-9]");

            strTaxSSN = regexObj.Replace(strTaxSSN, "").Trim();

            txtTaxSSN.Text = strTaxSSN;

            RESSN.Enabled = false;
        }

        try
        {
            lblError.Visible = false;
            if (Page.IsValid)
            {

                if (repCatResult == "True")
                {
                    if (Validate())
                    {
                        if (txtTaxSSN.Text.Trim().Length != 9)
                        {
                            RESSN.Enabled = true;
                            SetErrorMessage("The Federal Tax ID/ Social Security Number must be 9 characters long. (Ex. 123456789)");
                        }
                        else
                        {
                            int AffiliateID = 0;
                            if ((Request.Params.Get("rf") != null))
                            {
                                AffiliateID = Convert.ToInt32(Request.Params.Get("rf"));
                            }

                            //HttpCookie ck = Request.Cookies["AffiliateWizAffiliateID"];
                            //if (ck != null)
                            //AffiliateID = Convert.ToInt32(ck["AffiliateID"].ToString().Trim());
                            string queryString = Request.QueryString["A"];
                            if (queryString != null)
                            {
                                AffiliateID = Int32.Parse(queryString);
                            }

                            string strMail = string.Empty;
                            strMail += "Referral ID: " + AffiliateID.ToString() + System.Environment.NewLine; ;
                            strMail += "Signup Category: " + lblSignup.Text + System.Environment.NewLine; ;
                            strMail += "First Name: " + txtFirstName.Text.Trim() + System.Environment.NewLine;
                            strMail += "Last Name: " + txtLastName.Text.Trim() + System.Environment.NewLine;
                            strMail += "Password Phrase: " + txtPasswordPhrase.Text.Trim() + System.Environment.NewLine;
                            strMail += "DBA: " + txtDBAName.Text.Trim() + System.Environment.NewLine;
                            strMail += "Legal Status: " + lstLegalStatus.SelectedItem.Value.Trim() + System.Environment.NewLine;
                            strMail += "Email: " + txtEmail.Text.Trim() + System.Environment.NewLine;
                            strMail += "Address: " + txtAddress.Text.Trim() + System.Environment.NewLine;
                            strMail += "City: " + txtCity.Text.Trim() + System.Environment.NewLine;
                            strMail += "State: " + lstState.SelectedItem.Value.Trim() + System.Environment.NewLine;
                            strMail += "Region: " + txtBusRegion.Text.Trim() + System.Environment.NewLine;
                            strMail += "Zip: " + txtZip.Text.Trim() + System.Environment.NewLine;
                            strMail += "Country: " + lstCountry.SelectedItem.Value.Trim() + System.Environment.NewLine;
                            //Shipping Address Check
                            strMail += "Shipping Address: " + txtShippingAddress.Text.Trim() + System.Environment.NewLine;
                            strMail += "Shipping City: " + txtShippingCity.Text.Trim() + System.Environment.NewLine;
                            strMail += "Shipping State: " + lstShippingState.SelectedItem.Value.Trim() + System.Environment.NewLine;
                            strMail += "Shipping Region: " + txtShippingRegion.Text.Trim() + System.Environment.NewLine;
                            strMail += "Shipping Zip: " + txtShippingZip.Text.Trim() + System.Environment.NewLine;
                            strMail += "Shipping Country: " + lstShippingCountry.SelectedItem.Value.Trim() + System.Environment.NewLine;
                            strMail += "Business Phone: " + txtPhone.Text.Trim() + System.Environment.NewLine;
                            strMail += "Home Phone: " + txtHomePhone.Text.Trim() + System.Environment.NewLine;
                            strMail += "Mobile Phone: " + txtMobilePhone.Text.Trim() + System.Environment.NewLine;
                            strMail += "Fax: " + txtFax.Text.Trim() + System.Environment.NewLine;
                            strMail += "Website URL: " + txtURL.Text.Trim() + System.Environment.NewLine;
                            strMail += "Comments: " + txtComments.Text.Trim() + System.Environment.NewLine;
                            string strCheckPayable = string.Empty;
                            if (rdbDBA.Checked)
                                strCheckPayable = "DBA";
                            else
                                strCheckPayable = "LegalName";
                            strMail += "Check Payable: " + strCheckPayable + System.Environment.NewLine;


                            string strTaxSSN = string.Empty;
                            string FederalTaxID = string.Empty;
                            string SocialSecurity = string.Empty;
                            if (rdbTaxID.Checked)
                            {
                                strTaxSSN = "TaxID";
                                FederalTaxID = txtTaxSSN.Text.Trim();
                            }
                            else
                            {
                                strTaxSSN = "SSN";
                                SocialSecurity = txtTaxSSN.Text.Trim();
                            }
                            /*
                            if (rdbTaxID.Checked)
                            {
                                strMail += "Social Security: " + System.Environment.NewLine;
                                strMail += "Tax ID: " + FederalTaxID + System.Environment.NewLine;
                            }
                            if (rdbSSN.Checked)
                            {
                                strMail += "Tax ID: " + System.Environment.NewLine;
                                strMail += "Social Security: " + SocialSecurity + System.Environment.NewLine;
                            }
                            */
                            strMail += "Notify by Email: " + lstNotify.SelectedValue.ToString() + System.Environment.NewLine;

                            //Send Email
                            if (strMail != "")
                                SendEmail(strMail);

                            //Add Information to database
                            bool bNotify = false;
                            if (lstNotify.SelectedItem.Text == "Yes")
                                bNotify = true;
                            AffiliatesWizBL AffWiz = new AffiliatesWizBL();

                            //if the Referral source is to be selected
                            if (pnlReferral.Visible == true)
                            {
                                if (txtSpecify.Enabled == true)
                                    strOtherReferral = txtSpecify.Text.Trim();
                                else
                                    strOtherReferral = lstReferral.SelectedValue.ToString();
                            }
                            else
                                strOtherReferral = "";

                            if ((Request.Params.Get("rf") != null))
                            {
                                AffiliateID = Convert.ToInt32(Request.Params.Get("rf"));
                            }

                            string NewAffiliateID = AffWiz.AddAffiliate(AffiliateID, txtUserName.Text.Trim(),
                                txtPassword.Text.Trim(), txtPasswordPhrase.Text.Trim(), txtFirstName.Text.Trim(),
                                txtLastName.Text.Trim(), txtDBAName.Text.Trim(), txtCompanyName.Text.Trim(),
                                strCheckPayable, txtEmail.Text.Trim(), strOtherReferral, strTaxSSN, FederalTaxID, SocialSecurity,
                                txtAddress.Text.Trim(), txtCity.Text.Trim(), lstState.SelectedItem.Value.Trim(),
                                txtBusRegion.Text.Trim(), txtZip.Text.Trim(), lstCountry.SelectedItem.Value.Trim(),
                                txtShippingAddress.Text.Trim(), txtShippingCity.Text.Trim(),
                                lstShippingState.SelectedItem.Value.Trim(), txtShippingRegion.Text.Trim(),
                                txtShippingZip.Text.Trim(), lstShippingCountry.SelectedItem.Value.Trim(),
                                txtPhone.Text.Trim(), txtHomePhone.Text.Trim(), txtMobilePhone.Text.Trim(),
                                txtFax.Text.Trim(), txtURL.Text.Trim(),
                                txtComments.Text.Trim(), bNotify, lblSignup.Text,
                                lstLegalStatus.SelectedItem.Value.Trim(), rdbDDYes.Checked);

                            //Insert Banking Info only if Direct Deposit option is Yes
                            if (rdbDDYes.Checked)
                            {
                                if ((txtBankRoutingNumber.Text.Trim().Length < 9) || (txtBankRoutingNumber.Text.Trim().Length > 9))
                                    SetErrorMessage("Routing number must be exactly 9 characters long");
                                else
                                {
                                    string strReplace = "";
                                    AffWiz.InsertBankingInfo(NewAffiliateID, lstBankName.SelectedItem.Text.Trim().Replace("'", strReplace),
                                        txtOtherBank.Text.Trim().Replace("'", strReplace), txtBankAddress.Text.Trim().Replace("'", strReplace), txtZipCode.Text.Trim().Replace("'", strReplace), txtCity.Text.Trim().Replace("'", strReplace),
                                        lstState.SelectedItem.Text.Trim().Replace("'", strReplace), txtNameOnChecking.Text.Trim().Replace("'", strReplace), txtAcctNumber.Text.Trim(),
                                        txtBankRoutingNumber.Text.Trim(), txtPhone.Text.Trim().Replace("'", strReplace));
                                }
                            }//end if direct deposit checked

                            Response.Redirect("../Portal/index.aspx?NewSignup=True&SignupType=" + lblSignup.Text, false);
                        }
                    }//end if validate
                }
                else
                {
                    SetErrorMessage("Please take a moment to verify that you are not a robot.");
                }
            }//end if page valid
        }//end try
        catch (Exception err)
        {
            //SetErrorMessage("Error Processing Request. Please contact technical support");
            SetErrorMessage(err.Message);
        }
    }//end button click


    /**
    //This function emails Admin
    public void SendEmail(string strMail)
    {
        try
        {
            //Send Email to agent after successful registration before redirecting user
            MailMessage msg = new MailMessage();
            string strSubject = "Commerce Technologies - New " + lblSignup.Text + " Signup";
            msg.To.Add(new MailAddress("sales@commercetech.com"));

            int PartnerID = 0;
            HttpCookie ck = Request.Cookies["AffiliateWizAffiliateID"];
            if (ck != null)
                PartnerID = Convert.ToInt32(ck["AffiliateID"].ToString().Trim());
            
            AffiliatesWizBL ReferralPartner = new AffiliatesWizBL();
            DataSet ds = ReferralPartner.GetAffiliateInfoBasic(PartnerID);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                if (dr["SendEmailNotification"].ToString().Trim() == "True")
                {
                    string email = dr["Email"].ToString().Trim();
                    msg.To.Add(new MailAddress(email));
                    msg.CC.Add(new MailAddress("sales@commercetech.com"));
                }
            }

            msg.From = new MailAddress("sales@commercetech.com");
            msg.Subject = strSubject;
            msg.Body = GetBody(strMail);
            System.Net.Mail.SmtpClient mSmtpClient = new System.Net.Mail.SmtpClient("192.168.1.136", 25);
            mSmtpClient.EnableSsl = false;
            mSmtpClient.UseDefaultCredentials = false;
            mSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            mSmtpClient.Send(msg);
            msg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
        }
        catch (Exception err)
        {
            SetErrorMessage("Error Processing Request. Please contact technical support");
        }
    }//end function send email
    */

    public void SendEmail(string strMail)
    {
        try
        {
            // Prepare the email message
            MailMessage msg = new MailMessage();
            string strSubject = "Commerce Technologies - New " + lblSignup.Text + " Signup";
            msg.To.Add(new MailAddress("accounting@commercetech.com"));

            // Optional: Add additional recipients
            int PartnerID = 0;
            HttpCookie ck = Request.Cookies["AffiliateWizAffiliateID"];
            if (ck != null)
                PartnerID = Convert.ToInt32(ck["AffiliateID"].ToString().Trim());

            AffiliatesWizBL ReferralPartner = new AffiliatesWizBL();
            DataSet ds = ReferralPartner.GetAffiliateInfoBasic(PartnerID);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                if (dr["SendEmailNotification"].ToString().Trim() == "True")
                {
                    string email = dr["Email"].ToString().Trim();
                    msg.To.Add(new MailAddress(email));
                    msg.CC.Add(new MailAddress("accounting@commercetech.com"));
                }
            }

            // Set email sender and content
            msg.From = new MailAddress("accounting@commercetech.com");
            msg.Subject = strSubject;
            msg.Body = GetBody(strMail);
            msg.IsBodyHtml = true;

            // Configure SMTP client
            SmtpClient mSmtpClient = new SmtpClient("smtp.office365.com", 587); // Office 365 SMTP server
            mSmtpClient.Credentials = new System.Net.NetworkCredential("accounting@commercetech.com", "1Success11"); // Use email and password
            mSmtpClient.EnableSsl = false; // Ensure SSL is enabled
            mSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

            // Send the email
            mSmtpClient.Send(msg);
            msg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
        }
        catch (Exception err)
        {
            // Display error message for debugging
            SetErrorMessage("Error Sending Email: " + err.Message);
        }
    }


    /**
    public void SendEmail(string strMail)
    {
        try
        {
            // Prepare the email message
            MailMessage msg = new MailMessage();
            string strSubject = "Commerce Technologies - New " + lblSignup.Text + " Signup";
            msg.To.Add(new MailAddress("sales@commercetech.com"));

            // Optional: Add additional recipients
            int PartnerID = 0;
            HttpCookie ck = Request.Cookies["AffiliateWizAffiliateID"];
            if (ck != null)
                PartnerID = Convert.ToInt32(ck["AffiliateID"].ToString().Trim());

            AffiliatesWizBL ReferralPartner = new AffiliatesWizBL();
            DataSet ds = ReferralPartner.GetAffiliateInfoBasic(PartnerID);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                if (dr["SendEmailNotification"].ToString().Trim() == "True")
                {
                    string email = dr["Email"].ToString().Trim();
                    msg.To.Add(new MailAddress(email));
                    msg.CC.Add(new MailAddress("sales@commercetech.com"));
                }
            }

            // Set email sender and content
            msg.From = new MailAddress("sales@commercetech.com");
            msg.Subject = strSubject;
            msg.Body = GetBody(strMail);
            msg.IsBodyHtml = true;

            // Configure SMTP client
            SmtpClient mSmtpClient = new SmtpClient("smtp.office365.com", 587); // Office 365 SMTP server
            mSmtpClient.Credentials = new System.Net.NetworkCredential("sales@commercetech.com", "1Success11"); // Use email and password
            mSmtpClient.EnableSsl = true; // Ensure SSL is enabled
            mSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

            // Send the email
            mSmtpClient.Send(msg);
            msg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
        }
        catch (Exception err)
        {
            // Display error message for debugging
            SetErrorMessage($"Error Sending Email: {err.Message}");
        }
    }
    */

    //This function creates email body
    public string GetBody(string strMail)
    {
        string strBody = "Information for " + txtFirstName.Text.Trim() + " " + txtLastName.Text.Trim() + " is as follows: " + System.Environment.NewLine;
        strBody = strBody + System.Environment.NewLine;
        strBody = strBody + strMail + System.Environment.NewLine;
        strBody = strBody + "For any additional questions or concerns please visit " + "https://commercetech.com/" + " or call (800)-477-5363" + System.Environment.NewLine;
        strBody = strBody + System.Environment.NewLine;
        strBody = strBody + System.Environment.NewLine;
        strBody = strBody + "Thank You, " + System.Environment.NewLine;
        strBody = strBody + "Commerce Technologies" + System.Environment.NewLine;

        return strBody;
    }//end function GetBody

    protected void chkAgree_CheckedChanged(object sender, EventArgs e)
    {
        if (chkAgree.Checked)
            btnSubmit.Enabled = true;
        else
            btnSubmit.Enabled = false;
    }

    protected void chkShippinhSame_CheckedChanged(object sender, EventArgs e)
    {
        if (chkShippinhSame.Checked)
        {
            txtShippingAddress.Text = txtAddress.Text;
            txtShippingCity.Text = txtCity.Text;
            lstShippingState.SelectedIndex = lstState.SelectedIndex;
            txtShippingZip.Text = txtZip.Text;
            lstShippingCountry.SelectedIndex = lstCountry.SelectedIndex;
            txtShippingRegion.Text = txtBusRegion.Text;
        }
        else
        {
            txtShippingAddress.Text = "";
            txtShippingCity.Text = "";
            lstShippingState.SelectedIndex = 0;
            txtShippingZip.Text = "";
            lstShippingCountry.SelectedIndex = 0;
            txtShippingRegion.Text = "";
        }
    }//end if check changed

    //This function validates data in text fields    
    public new bool Validate()
    {
        TextBox txtBox = new TextBox();
        for (int i = 0; i < pnlMain.Controls.Count; i++)
        {
            if (pnlMain.Controls[i].GetType() == txtBox.GetType())
            {
                txtBox = (TextBox)pnlMain.Controls[i];
                if ((txtBox.Text.Contains("--")) || (txtBox.Text.Contains("#")) || (txtBox.Text.Contains(";")) || (txtBox.Text.Contains("'")) || (txtBox.Text.ToLower().Contains("script")))
                {
                    txtBox.BackColor = System.Drawing.Color.Ivory;
                    txtBox.Focus();
                    SetErrorMessage("You cannot use apostrophe, # or semi-colons in any of the following fields.");
                    return false;
                }
            }
        }//end for        

        if (txtUserName.Text.Trim().Length < 5)
        {
            SetErrorMessage("User Name must be at least 5 characters long.");
            return false;
        }
        if (txtPassword.Text.Trim().Length < 5)
        {
            SetErrorMessage("Password must be at least 5 characters long.");
            return false;
        }
 
        return true;
    }//end function Validate

    //This function displays error message on a label
    protected void SetErrorMessage(string errText)
    {
        lblError.Visible = true;
        lblError.Text = errText;
    }//end function set error message

    protected void rdbDDYes_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbDDNo.Checked)
            pnlBanking.Visible = false;
        else if (rdbDDYes.Checked)
        {
            //Get Banks
            CommonListData Banks = new CommonListData();
            DataSet dsBanks = Banks.GetCommonData("Banks");
            if (dsBanks.Tables[0].Rows.Count > 0)
            {
                lstBankName.DataSource = dsBanks.Tables[0];
                lstBankName.DataTextField = "BankName";
                lstBankName.DataValueField = "BankName";
                lstBankName.DataBind();

            }//end if count not 0
            pnlBanking.Visible = true;
        }
    }

    //This function handles bank list selection changed event
    protected void lstBankName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lstBankName.SelectedItem.Text.Trim() == "OTHER")
        {
            txtOtherBank.Enabled = true;
            txtOtherBank.BackColor = System.Drawing.Color.White;
            ValidateOtherBank.Enabled = true;
        }
        else
        {
            txtOtherBank.Enabled = false;
            txtOtherBank.BackColor = System.Drawing.Color.DarkGray;
            txtOtherBank.Text = "";
            ValidateOtherBank.Enabled = false;
        }
    }//end function Bank list selection changed event    

    protected override void Render(System.Web.UI.HtmlTextWriter writer)
    {
        System.IO.StringWriter stringWriter = new System.IO.StringWriter();
        HtmlTextWriter htmlWriter = new HtmlTextWriter(stringWriter);
        base.Render(htmlWriter);
        string html = stringWriter.ToString();
        int StartPoint = html.IndexOf("<input type=\"hidden\" name=\"__VIEWSTATE\"");
        if (StartPoint >= 0) //does __viewstate exist?
        {
            int EndPoint = html.IndexOf("/>", StartPoint) + 2;
            string viewstateInput = html.Substring(StartPoint, EndPoint - StartPoint);
            html = html.Remove(StartPoint, EndPoint - StartPoint);
            int FormEndStart = html.IndexOf("</form>") - 1;
            if (FormEndStart >= 0)
                html = html.Insert(FormEndStart, viewstateInput);
        }
        writer.Write(html);
    }
    protected void lstCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        /*if (lstCountry.SelectedValue == "United States")
        {
            CommonListData States = new CommonListData();
            DataSet dsStates = States.GetCommonData("States");
            lstState.Items.Clear();
            if (dsStates.Tables["States"].Rows.Count > 0)
            {
                lstState.DataSource = dsStates.Tables["States"];
                lstState.DataTextField = "State";
                lstState.DataValueField = "StateID";
                lstState.DataBind();
            }
        }

        else if (lstCountry.SelectedValue == "Canada")
        {
            CommonListData Data = new CommonListData();
            DataSet ds = Data.GetCommonData("Provinces");
            lstState.Items.Clear();
            if (ds.Tables["Provinces"].Rows.Count > 0)
            {
                lstState.DataSource = ds.Tables["Provinces"];
                lstState.DataTextField = "Province";
                lstState.DataValueField = "ProvinceID";
                lstState.DataBind();
            }
        }
        else
            lstState.SelectedValue = "  ";*/

        //Disable TaxID for international customers
        if (lstCountry.SelectedItem.Text.Trim() != "United States")
        {
            txtTaxSSN.Enabled = false;
            txtTaxSSN.Text = "";
            RequiredFieldValidator5.Enabled = false;
            if (lstCountry.SelectedItem.Text.Trim() != "Canada") 
                lstState.SelectedValue = "  ";
        }
        else
        {
            txtTaxSSN.Enabled = true;
            RequiredFieldValidator5.Enabled = true;
        }
    }
    protected void lstShippingCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        /*if (lstCountry.SelectedValue == "United States")
        {
            lstShippingState.Enabled = true;
            CommonListData States = new CommonListData();
            DataSet dsStates = States.GetCommonData("States");
            lstShippingState.Items.Clear();
            if (dsStates.Tables["ShippingStates"].Rows.Count > 0)
            {
                lstShippingState.DataSource = dsStates.Tables["States"];
                lstShippingState.DataTextField = "State";
                lstShippingState.DataValueField = "StateID";
                lstShippingState.DataBind();
            }
        }
        else if (lstCountry.SelectedValue == "Canada")
        {
            lstShippingState.Enabled = true;            
            CommonListData Data = new CommonListData();
            DataSet ds = Data.GetCommonData("Provinces");
            lstShippingState.Items.Clear();
            if (ds.Tables["Provinces"].Rows.Count > 0)
            {
                lstShippingState.DataSource = ds.Tables["Provinces"];
                lstShippingState.DataTextField = "Province";
                lstShippingState.DataValueField = "ProvinceID";
                lstShippingState.DataBind();
            }
        }
        else
        {
            lstShippingState.Enabled = false ;
            lstShippingState.SelectedValue = "  ";
        }*/
        if (lstShippingCountry.SelectedItem.Text.Trim() != "United States")
        {
            txtTaxSSN.Enabled = false;
            txtTaxSSN.Text = "";
            RequiredFieldValidator5.Enabled = false;
            if (lstShippingCountry.SelectedItem.Text.Trim() != "Canada")
                lstShippingState.SelectedValue = "  ";
        }
        else
        {
            txtTaxSSN.Enabled = true;
            RequiredFieldValidator5.Enabled = true;
        }
    
    }
    protected void lstReferral_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lstReferral.SelectedValue == "Other")
        {
            txtSpecify.Enabled = true;
            RFVSpecify.Enabled = true;
        }
        else
        {
            txtSpecify.Enabled = false;
            txtSpecify.Text = "";
            RFVSpecify.Enabled = false;
        }
    }
}
