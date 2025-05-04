using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using BusinessLayer;
using OnlineAppClassLibrary;
using DLPartner;
//using System.Web.Mail;
using System.Net.Mail;
using System.IO;

namespace netimageupload
{
    /// <summary>
    /// Summary description for WebForm1.
    /// </summary>
    public partial class uploadfiles : System.Web.UI.Page
    {
        /*protected System.Web.UI.HtmlControls.HtmlInputFile filUpload;
        protected System.Web.UI.WebControls.Image imgPicture;
        protected System.Web.UI.WebControls.Label lblOutput;
        protected System.Web.UI.WebControls.Button btnUpload;*/

        private void Page_Load(object sender, System.EventArgs e)
        {
            if (Session.IsNewSession)
                Response.Redirect("~/default.aspx");

            //Resellers and Affiliates do not have access to this page. Redirect them to the login page.
            /*if (User.IsInRole("Reseller") || User.IsInRole("Affiliate") || User.IsInRole("Agent") || User.IsInRole("Employee") || User.IsInRole("T1Agent"))
                Response.Redirect("~/default.aspx?Authentication=False");*/

            if (!IsPostBack)
            {
                if (!User.Identity.IsAuthenticated)
                    Response.Redirect("~/default.aspx?Authentication=False");
            }
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

        private void btnUpload_Click(object sender, System.EventArgs e)
        {
            
            int AffiliateID = Convert.ToInt32(Session["AffiliateID"]);
            int AppId = Convert.ToInt32(Session["AppId"]);
            string strP1FirstName = "";
            string strP1LastName = "";
            string strP1Initial = "";
            NewAppInfo newApp = new NewAppInfo(AppId);
            DataSet ds = new DataSet();
            ds = newApp.docusginSignerInfo();
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                strP1FirstName = Convert.ToString(dr["P1FirstName"]).Trim();
                strP1LastName = Convert.ToString(dr["P1LastName"]).Trim();
                if (!Convert.IsDBNull(strP1LastName))
                {
                    if (Convert.ToString(dr["P1LastName"]).Trim() != "")
                    {
                        strP1Initial = Convert.ToString(dr["P1LastName"]).Substring(0,1);
                    }
                }
            }

            // Initialize variables
            string sSavePath;
            string sNewPath;
            bool Completed = false;

            // Set constant values
            //sNewPath = "C:/website/home/PartnerPortal/PartnerLogo/" + Convert.ToString(AffiliateID) + "/";
            sNewPath = "//192.168.1.136/Shared/Customers/" + strP1Initial + "/" + strP1LastName + ", " + strP1FirstName + "/";
            Directory.CreateDirectory(sNewPath);
            //sSavePath = "/PartnerUploades/100";
            //sSavePath = "../AffiliateWiz/Images/ProductImages/";
            //sSavePath = "https://prostores5.carrierzone.com/stores/ecenowcom/catalog/";
            //sSavePath = "Images/";
            //sThumbExtension = "_thumb";
            //intThumbWidth = 160;
            //intThumbHeight = 120;

            //sSavePath = "PartnerLogo/" + Convert.ToString(AffiliateID) + "/";
            sSavePath = "//192.168.1.136/Shared/Customers/" + strP1Initial + "/" + strP1LastName + ", " + strP1FirstName + "/";
            // If file field isn't empty

            if (filUpload.PostedFile != null)
            {
                // Check file size (mustn't be 0)
                HttpPostedFile myFile = filUpload.PostedFile;
                int nFileLen = myFile.ContentLength;
                if (nFileLen == 0)
                {
                    lblOutput.Text = "Please Upload a Voided or Cancelled Check.";
                    lblOutput.Style["font-weight"] = "bold";
                    lblOutput.ForeColor = System.Drawing.Color.Red;  
                    return;
                }

                if ((System.IO.Path.GetExtension(myFile.FileName).ToLower() != ".jpg") && (System.IO.Path.GetExtension(myFile.FileName).ToLower() != ".jpeg") && (System.IO.Path.GetExtension(myFile.FileName).ToLower() != ".pdf") && (System.IO.Path.GetExtension(myFile.FileName).ToLower() != ".tif") && (System.IO.Path.GetExtension(myFile.FileName).ToLower() != ".doc") && (System.IO.Path.GetExtension(myFile.FileName).ToLower() != ".docx") && (System.IO.Path.GetExtension(myFile.FileName).ToLower() != ".png") && (System.IO.Path.GetExtension(myFile.FileName).ToLower() != ".txt") && (System.IO.Path.GetExtension(myFile.FileName).ToLower() != ".xlsx") && (System.IO.Path.GetExtension(myFile.FileName).ToLower() != ".xls"))
                {
                    lblOutput.Text = "Please upload files in supported format.";
                    lblOutput.Style["font-weight"] = "bold";
                    lblOutput.ForeColor = System.Drawing.Color.Red;  
                    return;
                }
                if (filUpload2.PostedFile == null)
                {
                    lblOutput.Text = "Please Upload a Business License, Phone or Utility Bill";
                    return;
                }

                // Read file into a data stream
                byte[] myData = new Byte[nFileLen];
                myFile.InputStream.Read(myData, 0, nFileLen);

                // Make sure a duplicate file doesn't exist.  If it does, keep on appending an incremental numeric until it is unique
                string sFilename = System.IO.Path.GetFileName(myFile.FileName);
                int file_append = 0;
                //while (System.IO.File.Exists(Server.MapPath(sSavePath + sFilename)))
                while (System.IO.File.Exists(sSavePath + sFilename))
                {
                    file_append++;
                    //sFilename = System.IO.Path.GetFileNameWithoutExtension(myFile.FileName) + file_append.ToString() + ".jpg";
                    sFilename = System.IO.Path.GetFileNameWithoutExtension(myFile.FileName) + file_append.ToString() + System.IO.Path.GetExtension(myFile.FileName);
                }

                // Save the stream to disk
                //System.IO.FileStream newFile = new System.IO.FileStream(Server.MapPath(sSavePath + sFilename), System.IO.FileMode.Create);
                System.IO.FileStream newFile = new System.IO.FileStream(sSavePath + sFilename, System.IO.FileMode.Create);
                newFile.Write(myData, 0, myData.Length);
                newFile.Close();


                Completed = true;

                lblOutput.Text = "Files uploaded successfully!";

                OtherInfo otherInfo = new OtherInfo(AppId);
                bool retVal = otherInfo.UpdatePrevUploadDate();

                int retVal1 = otherInfo.InsertUpdateUploaded();

                bool retVal2 = otherInfo.UpdateLastUploadDate();

                //Set last modified date in newapp table
                CommonFunctions GeneralInfo = new CommonFunctions(AppId);
                GeneralInfo.UpdateLastModified();

                LogBL LogData = new LogBL(AppId);
                LogData.InsertLogData(AffiliateID, "Document \"" + sFilename + "\" uploaded.");

                NewAppInfo AppInfo = new NewAppInfo(AppId);
                string Status = AppInfo.ReturnStatus();
                AppInfo.checkPageRedirect();
            }
            if (filUpload2.PostedFile != null)
            {
                // Check file size (mustn't be 0)
                HttpPostedFile myFile2 = filUpload2.PostedFile;
                int nFileLen2 = myFile2.ContentLength;
                if (nFileLen2 == 0)
                {
                    lblOutput.Text = "Please Upload your Business License, Phone or Utility Bill or Previous Processing Statement.";
                    lblOutput.Style["font-weight"] = "bold";
                    lblOutput.ForeColor = System.Drawing.Color.Red;
                    return;
                }


                if ((System.IO.Path.GetExtension(myFile2.FileName).ToLower() != ".jpg") && (System.IO.Path.GetExtension(myFile2.FileName).ToLower() != ".jpeg") && (System.IO.Path.GetExtension(myFile2.FileName).ToLower() != ".pdf") && (System.IO.Path.GetExtension(myFile2.FileName).ToLower() != ".tif") && (System.IO.Path.GetExtension(myFile2.FileName).ToLower() != ".doc") && (System.IO.Path.GetExtension(myFile2.FileName).ToLower() != ".docx") && (System.IO.Path.GetExtension(myFile2.FileName).ToLower() != ".png") && (System.IO.Path.GetExtension(myFile2.FileName).ToLower() != ".txt") && (System.IO.Path.GetExtension(myFile2.FileName).ToLower() != ".xlsx") && (System.IO.Path.GetExtension(myFile2.FileName).ToLower() != ".xls"))
                {
                    lblOutput.Text = "Please upload files in supported format.";
                    lblOutput.Style["font-weight"] = "bold";
                    lblOutput.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                // Read file into a data stream
                byte[] myData = new Byte[nFileLen2];
                myFile2.InputStream.Read(myData, 0, nFileLen2);

                // Make sure a duplicate file doesn't exist.  If it does, keep on appending an incremental numeric until it is unique
                string sFilename = System.IO.Path.GetFileName(myFile2.FileName);
                int file_append = 0;
                //while (System.IO.File.Exists(Server.MapPath(sSavePath + sFilename)))
                while (System.IO.File.Exists(sSavePath + sFilename))
                {
                    file_append++;
                    //sFilename = System.IO.Path.GetFileNameWithoutExtension(myFile.FileName) + file_append.ToString() + ".jpg";
                    sFilename = System.IO.Path.GetFileNameWithoutExtension(myFile2.FileName) + file_append.ToString() + System.IO.Path.GetExtension(myFile2.FileName);
                }

                // Save the stream to disk
                //System.IO.FileStream newFile = new System.IO.FileStream(Server.MapPath(sSavePath + sFilename), System.IO.FileMode.Create);
                System.IO.FileStream newFile = new System.IO.FileStream(sSavePath + sFilename, System.IO.FileMode.Create);
                newFile.Write(myData, 0, myData.Length);
                newFile.Close();

                lblOutput.Text = "Files uploaded successfully!";
                

                OtherInfo otherInfo = new OtherInfo(AppId);
                bool retVal = otherInfo.UpdatePrevUploadDate();

                bool retVal2 = otherInfo.UpdateLastUploadDate();

                //Set last modified date in newapp table
                CommonFunctions GeneralInfo = new CommonFunctions(AppId);
                GeneralInfo.UpdateLastModified();

                LogBL LogData = new LogBL(AppId);
                LogData.InsertLogData(AffiliateID, "Document \"" + sFilename + "\" uploaded.");
            }
        }

        public bool ThumbnailCallback()
        {
            return false;
        }
    }
}