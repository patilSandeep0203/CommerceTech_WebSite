using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Net.Mail;


public partial class forgotPassword : System.Web.UI.Page
{
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string uniqueCode = string.Empty;
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringPartner"].ConnectionString);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            // get the records matching the supplied username or email id.         
            //cmd = new SqlCommand("select * from OnlineAppAccess INNER JOIN OnlineAppProfile where LoginName COLLATE Latin1_general_CS_AS=@username", con);
            //cmd = new SqlCommand("select * from OnlineAppProfile where Email COLLATE Latin1_general_CS_AS=@emailId", con);
            //cmd = new SqlCommand("select LoginName from OnlineAppAccess where LoginName COLLATE Latin1_general_CS_AS=@username", con);
            cmd = new SqlCommand("SELECT LoginName FROM dbo.OnlineAppProfile INNER JOIN dbo.OnlineAppAccess ON dbo.OnlineAppProfile.AppID = dbo.OnlineAppAccess.AppID WHERE LoginName=@username", con);
            cmd.Parameters.AddWithValue("@username", Convert.ToString(txtUserName.Text.Trim()));

            //cmd.Parameters.AddWithValue("@emailId", Convert.ToString(txtEmailId.Text.Trim()));
            dr = cmd.ExecuteReader();
            cmd.Dispose();
            if (dr.HasRows)
            {
                dr.Read();
                //generate unique code
                uniqueCode = Convert.ToString(System.Guid.NewGuid());

                //Updating an unique random code in then UniquCode field of the database table;
                cmd = new SqlCommand("SELECT AppID FROM dbo.OnlineAppAccess Where LoginName=@username", con);
                cmd.Parameters.AddWithValue("@username", Convert.ToString(txtUserName.Text.Trim()));

                SqlDataReader rdr = cmd.ExecuteReader();
                rdr.Read();
                string appNum;
                appNum = rdr.GetSqlValue(0).ToString();

                cmd = new SqlCommand("UPDATE dbo.OnlineAppProfile set UniqueCode=@uniqueCode where Appid=@AppID", con);
                cmd.Parameters.AddWithValue("@AppID", appNum);
                cmd.Parameters.AddWithValue("@uniqueCode", uniqueCode);

                cmd = new SqlCommand("SELECT Email FROM dbo.OnlineAppProfile INNER JOIN dbo.OnlineAppAccess ON dbo.OnlineAppProfile.AppID = dbo.OnlineAppAccess.AppID WHERE LoginName=@username", con);
                cmd.Parameters.AddWithValue("@username", Convert.ToString(txtUserName.Text.Trim()));
                SqlDataReader rdr2 = cmd.ExecuteReader();
                rdr2.Read();
                string email;
                email = rdr2.GetSqlValue(0).ToString();


                StringBuilder strBody = new StringBuilder();
                //Passing emailid,username and generated unique code via querystring. For testing pass your localhost number and while making online pass your domain name instead of localhost path.
                strBody.Append("<a href=https://apps.commercetech.com/Application/ResetPassword.aspx?emailId=" + email + "&uCode=" + uniqueCode + "&uApp=" + appNum + ">Click here to change your password</a>");
                //strBody.Append("<a href=http://localhost:53599/ResetPassword.aspx?emailId=" + email + "&uCode=" + uniqueCode + "&uApp=" + appNum + ">Click here to change your password</a>");
                // sbody.Append("&uCode=" + uniqueCode + "&uName=" + txtUserName.Text + ">Click here to change your password</a>");


                SendEmailCust(email,strBody.ToString());


                dr.Close();
                dr.Dispose();
                //cmd.ExecuteReader();

                cmd.Dispose();
                con.Close();
                lblStatus.Text = "Reset password link has been sent to your email address";

                txtUserName.Text = string.Empty;
            }
            else
            {
                lblStatus.Text = "Please enter valid  username";

                txtUserName.Text = string.Empty;
                con.Close();
                return;
            }
        }
        //catch (Exception ex)
        //{
        //    lblStatus.Text = "Error Occured: " + ex.Message.ToString();
        //}
        finally
        {
            cmd.Dispose();
        }


    }

    public void SendEmailCust(string email, string body)
    {
        System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage("web@commercetech.com", email, "Reset Your Password", body);
        //pasing the Gmail credentials to send the email

        //System.Net.NetworkCredential mailAuthenticaion = new System.Net.NetworkCredential("sales@commercetech.com", "1Success11!");

        System.Net.Mail.SmtpClient mailclient = new System.Net.Mail.SmtpClient("192.168.1.136", 25);

        mailclient.EnableSsl = false;
        mailclient.UseDefaultCredentials = false;
        mailclient.DeliveryMethod = SmtpDeliveryMethod.Network;
        //mailclient.Credentials = mailAuthenticaion;
        mail.IsBodyHtml = true;
        mailclient.Send(mail);

    }

}



