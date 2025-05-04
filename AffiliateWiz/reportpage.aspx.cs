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

using System.Data.SqlClient;


public partial class consult : System.Web.UI.Page
{


    private static string fn = string.Empty;
    private static string ln = "";
    private static string em = "";
    private static string pn = "";
    private static string name = "";
    private static string ind = "";
    private static string cmp = "";
    private static string co = "";
    private static string refr = "";
    private static string strAffiliateID = "";

    private static string ConnStringPartner = ConfigurationManager.AppSettings["ConnectionStringPartner"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {


        if (!IsPostBack)
        {

            try
            {
                if ((Request.Params.Get("first") != null))
                {
                    fn = Request.Params.Get("first");
                }

                if ((Request.Params.Get("ln") != null))
                {
                    ln = Request.Params.Get("ln");
                }

                name = fn + " " + ln;
                if ((Request.Params.Get("em") != null))
                {
                    em = Request.Params.Get("em");
                }

                if ((Request.Params.Get("pn") != null))
                {
                    pn = Request.Params.Get("pn");
                }

                if ((Request.Params.Get("ind") != null))
                {
                    pn = Request.Params.Get("ind");
                }

                if ((Request.Params.Get("co") != null))
                {
                    pn = Request.Params.Get("co");
                }

                if ((Request.Params.Get("refr") != null))
                {
                    refr = Request.Params.Get("refr");
                }

                //ReportsBL Report = new ReportsBL();
                //Report.InsertFreeReport("", name, em);

                InsertFreeReport(refr, name, em, pn, cmp, ind, co);

                //SendEmailToAdmin(strAffiliateID, fn, ln, em);
                //SendEmailToUser(strAffiliateID, fn, ln, em);
                //Send Email                    
                //SendEmailToPartner();
                Response.Redirect("https://commercetech.com/thank-you/");
            }
            catch(Exception err)
            {
                lblError.Visible = true;
                lblError.Text = err.Message;
            }


        }
    }

    public int InsertFreeReport(string ReferralID, string Name, string Email, string Phone, string Company, string Industry, string Country)
    {
        SqlConnection Conn = new SqlConnection(ConnStringPartner);
        try
        {
            SqlCommand cmdReports = new SqlCommand("SP_InsertFreeReport", Conn);
            cmdReports.CommandType = CommandType.StoredProcedure;
            cmdReports.Parameters.Add(new SqlParameter("@ReferralID", ReferralID));
            cmdReports.Parameters.Add(new SqlParameter("@Contact", Name));
            cmdReports.Parameters.Add(new SqlParameter("@Email", Email));
            cmdReports.Parameters.Add(new SqlParameter("@Phone", Phone));
            cmdReports.Parameters.Add(new SqlParameter("@Company", Company));
            cmdReports.Parameters.Add(new SqlParameter("@Industry", Industry));
            cmdReports.Parameters.Add(new SqlParameter("@Country", Country));
            cmdReports.Connection.Open();
            int iRetVal = cmdReports.ExecuteNonQuery();
            SendEmailToAdmin(ReferralID, Name, Email);
            SendEmailToUser(ReferralID, Name, Email);
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
    }//end InsertFreeReport

    public void SendEmailToUser(string strAffiliateID, string fisrt, string email)
    {
            try
            {
                //Send Email to agent after successful registration before redirecting user
                string strSubject = "Commerce Technologies - Thank You";
                MailMessage msg = new MailMessage();
                //msg.To = txtConsultEmail.Text.Trim();
                msg.To.Add(new MailAddress(email));
                //msg.From = "sales@commercetech.com";
                msg.From = new MailAddress("sales@commercetech.com");
                msg.Subject = strSubject;
                msg.Body = GetBody(strAffiliateID, fisrt, email);
              
            //SmtpMail.Send(msg);
            System.Net.Mail.SmtpClient mSmtpClient = new System.Net.Mail.SmtpClient("192.168.1.136", 25);
            mSmtpClient.EnableSsl = false;
            mSmtpClient.UseDefaultCredentials = false;
            mSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            mSmtpClient.Send(msg);
            msg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
        }//end try
            catch (Exception err)
            {
                lblError.Visible = true;
                lblError.Text = err.Message;
            }
     }//end function send email

    //This function emails Admin
    public void SendEmailToAdmin(string strAffiliateID, string fisrt, string email)
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
            string strSubject = "Commerce Technologies Free Report - New Lead from Referral Source: " + CompanyName;
            MailMessage msg = new MailMessage();
            //msg.To = "sales@commercetech.com";
            msg.To.Add(new MailAddress("sales@commercetech.com"));
            //msg.From = "sales@commercetech.com";
            msg.From = new MailAddress("sales@commercetech.com");
            msg.Subject = strSubject;
            msg.Body = GetBody(strAffiliateID, fisrt, email);
            //SmtpMail.Send(msg);
            System.Net.Mail.SmtpClient mSmtpClient = new System.Net.Mail.SmtpClient("192.168.1.136", 25);
            mSmtpClient.EnableSsl = false;
            mSmtpClient.UseDefaultCredentials = false;
            mSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            mSmtpClient.Send(msg);
            msg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
        }//end try
        catch (Exception err)
        {
            lblError.Visible = true;
            lblError.Text = err.Message;
        }
    }//end function send email

    //This function creates email body
    public string GetBody(string strAffiliateID, string first, string email)
    {
        int AffiliateID = 11;
        //HttpCookie ck = Request.Cookies["AffiliateWizAffiliateID"];
        //if (ck != null)
        AffiliateID = Convert.ToInt32(strAffiliateID);
        string Body = "Thank You Very Much For Taking Time to Fill Out Our Form" + System.Environment.NewLine + System.Environment.NewLine
            + "Within 24 hours, you will be contacted by a Sales Consultant to help you obtain any additional information to establish a merchant account or any of our other products or services. If you would like to speak with someone now you may contact us at 1-800-477-5363 " + System.Environment.NewLine;
        Body = Body + "You Have Submitted the Following Information" + System.Environment.NewLine + System.Environment.NewLine

            + "Name : " + first + System.Environment.NewLine
            + "Email : " + email + System.Environment.NewLine
            + "" + System.Environment.NewLine;

        Body = Body + "For any additional questions or concerns please visit " + "https://commercetech.com/" + " or call (800)-477-5363" + System.Environment.NewLine;

        return Body;
    }//end function GetBody


}
