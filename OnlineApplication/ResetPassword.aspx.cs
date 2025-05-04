using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Security;
using System.Security.Cryptography;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using OnlineAppClassLibrary;


public partial class ResetPassword : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringPartner"].ConnectionString);
    SqlCommand cmd = new SqlCommand();
    private static string strReplace = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            SqlDataReader dr;
            try
            {
                //Here we will check from the passed querystring that if the email id/username and generated unique code is same then the panel for resetting password will be visible otherwise not
                cmd = new SqlCommand("select Email,UniqueCode from dbo.OnlineAppProfile        where UniqueCode=@uniqueCode or (Email=@emailid)", con);
                cmd.Parameters.AddWithValue("@uniqueCode", Convert.ToString(Request.QueryString["uCode"]));
                cmd.Parameters.AddWithValue("@emailid", Convert.ToString(Request.QueryString["emailId"]));

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    ResetPwdPanel.Visible = true;
                }
                else
                {
                    ResetPwdPanel.Visible = false;
                    lblExpired.Text = "Reset password link has expired.It was for one time use only";
                    return;
                }
                dr.Close();
                dr.Dispose();
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Error Occured: " + ex.Message.ToString();
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }
        }
    }

    //This function generates a hash to encrypt the password
    protected string GenerateHash(string salt)
    {
        string hash = FormsAuthentication.HashPasswordForStoringInConfigFile(txtNewPwd.Text.Trim() + salt, "sha1");
        return hash;
    }

    //This function gets a salt to encrypt the password which can be stored in the database
    protected string GetSalt()
    {
        RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
        byte[] buffer = new byte[5];
        rng.GetBytes(buffer);
        return Convert.ToBase64String(buffer);
    }

    protected void btnChangePwd_Click(object sender, EventArgs e)
    {
        try
        {
            con.Open();

            if (txtNewPwd.Text.Trim().Length < 5)
            {
                lblStatus.Text = "Please choose a password which is atleast 5 characters long. You cannot use characters like semi-colons, #, hyphens or apostrophes in the password.";
            }
            else
            {
                //Validate data
                bool retValidate = ValidateData();
                if (retValidate)
                {
                    string salt = GetSalt();
                    string hash = GenerateHash(salt);
                    bool retVal;
                    cmd = new SqlCommand("SELECT AppID  FROM dbo.OnlineAppAccess Where AppID=@appid", con);

                    cmd.Parameters.AddWithValue("@appid", Convert.ToString(Request.QueryString["uApp"]));

                    SqlDataReader rdr = cmd.ExecuteReader();
                    rdr.Read();

                    string appNum;
                    appNum = rdr.GetSqlValue(0).ToString();

                    int appID = Int32.Parse(appNum);

                    cmd = new SqlCommand("SELECT LoginName  FROM dbo.OnlineAppAccess Where Appid=@appid", con);
                    cmd.Parameters.AddWithValue("@appid", Convert.ToString(Request.QueryString["uApp"]));
                    SqlDataReader rdr2 = cmd.ExecuteReader();
                    rdr2.Read();
                    string username;
                    username = rdr2.GetSqlValue(0).ToString();

                    //Update password in OnlineAppAccess
                    OnlineAppAccess UpdatePassword = new OnlineAppAccess(appID);
                    retVal = UpdatePassword.InsertUpdatePassword(username, txtNewPwd.Text.Trim().Replace("'", strReplace), hash, salt);

                    rdr.Close();
                    rdr.Dispose();
                    rdr2.Close();
                    rdr.Dispose();

                }

            }
            cmd.Dispose();
            con.Close();



        }
        catch (Exception ex)
        {
            lblStatus.Text = "Error Occured : " + ex.Message.ToString();
        }
        finally
        {
            cmd.Dispose();
            con.Close();
        }
    }

    protected bool ValidateData()
    {
        TextBox txtBox = new TextBox();
        for (int i = 0; i < ResetPwdPanel.Controls.Count; i++)
        {
            if (ResetPwdPanel.Controls[i].GetType() == txtBox.GetType())
            {
                txtBox = (TextBox)ResetPwdPanel.Controls[i];
                if ((txtBox.Text.Contains("--")) || (txtBox.Text.Contains("#")) || (txtBox.Text.Contains(";")) || (txtBox.Text.Contains("'")))
                {
                    txtBox.BackColor = System.Drawing.Color.Ivory;
                    txtBox.Focus();
                    lblStatus.Text = "You cannot use apostrophes, #, or semi-colons in any of the following fields.";
                    return false;
                }
            }
        }//end for
        if ((txtNewPwd.Text.Contains("--")) || (txtNewPwd.Text.Contains("#")) || (txtNewPwd.Text.Contains(";")) || (txtNewPwd.Text.Contains("'")))
        {
            lblStatus.Text = "You cannot use hyphens, apostrophe, # or semi-colons in the Password field.";
            return false;
        }
        return true;
    }


}