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

public partial class QuestionsMCA : System.Web.UI.Page
{
    private static string strBusiness = "";
    private static string parameters = "";
    private static string Serv = "";
    private static string SW = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Style errLabel = new Style();
        errLabel.BackColor = System.Drawing.Color.FromArgb(255, 192, 192);
        errLabel.ForeColor = System.Drawing.Color.Black;
        errLabel.BorderColor = System.Drawing.Color.Red;
        errLabel.BorderStyle = BorderStyle.Solid;
        errLabel.BorderWidth = Unit.Pixel(1);
        errLabel.Font.Size = FontUnit.Small;
        lblError.ApplyStyle(errLabel);

        parameters = HttpContext.Current.Request.Url.ToString();
        parameters = parameters.Substring(parameters.IndexOf("?") + 1);

        if (!IsPostBack)
        {
            strBusiness = "";
        }

        if ((Request.Params.Get("Serv") != null))
        {
            Serv = Request.Params.Get("Serv");
        }
        if ((Request.Params.Get("SW") != null))
        {
            SW = Request.Params.Get("SW");
        }
    }

    //This function handles the submit button click event
    protected void btnContinue_Click(object sender, EventArgs e)
    {
        if ((strBusiness == "") || (!rdbMCABankruptN.Checked && !rdbMCABankruptY.Checked))
        {
            SetErrorMessage("Please select one of the options listed below.");
        }
        else
        {
            if (strBusiness == "OpenBankruptcy")
            {
                SetErrorMessage("We cannot process your Merchant Cash Advance application since you have an open bankruptcy in the last 12 months. Please click the Skip button to continue without applying for Merchant Cash Advance.");
            }
            else if (strBusiness == "LowVolumn")
            {
                SetErrorMessage("We cannot process your Merchant Cash Advance application since your average credit card volume is less than $3500. Please click the Skip button to continue without applying for Merchant Cash Advance.");
            }
            else if (strBusiness == "Unacceptable")
            {
                SetErrorMessage("We cannot process your Merchant Cash Advance application if your business is one of the Unacceptable Businesses. Please click the Skip button to continue without applying for Merchant Cash Advance.");
            }
            else if (strBusiness == "Below3mon")
            {
                SetErrorMessage("We cannot process your Merchant Cash Advance application if you are in business for less than three months. Please click the Skip button to continue without applying for Merchant Cash Advance.");
            }
                            else if (strBusiness == "BelowFourthousand")
            {
                SetErrorMessage("For starter programs your average credit card volume must be greater than $4000. We cannot process your Merchant Cash Advance application since your average credit card volume is less than $4000. Please click the Skip button to continue without applying for Merchant Cash Advance.");
            }
            else if (strBusiness == "AMI")
            {
                string strServ = Serv.Replace("MerchantFunding/", "AMI/");
                Response.Redirect("register.aspx?SW=" + SW + "&Serv=" + strServ);
            }
            else if (strBusiness == "BFS")
            {
                string strServ = Serv.Replace("MerchantFunding/", "BFS/");
                Response.Redirect("register.aspx?SW=" + SW + "&Serv=" + strServ);
            }
            else if (strBusiness == "RapidAdvance")
            {
                string strServ = Serv.Replace("MerchantFunding/", "RapidAdvance/");
                Response.Redirect("register.aspx?SW=" + SW + "&Serv=" + strServ);
            }

            
        }
    }//end function submit button clicked


    protected void btnSkip_Click(object sender, EventArgs e)
    {
        string strServ = Serv.Replace("MerchantFunding/", "");
        if (strServ != "")
            Response.Redirect("register.aspx?SW=" + SW + "&Serv=" + strServ);
        else
            Response.Redirect("start1.aspx");
    }

    protected void btnStart_Click(object sender, EventArgs e)
    {
        Response.Redirect("Start.aspx");
    }

    //This function handles the radio button check changed event
    protected void rdbMCA_CheckedChanged(object sender, EventArgs e)
    {
        if (sender.Equals(rdbMCABankruptY))
        {
            strBusiness = "OpenBankruptcy";
            pnlMCAVolumn.Visible = false;
            rdbMCAVolumnY.Checked = false;
            rdbMCAVolumnN.Checked = false;
            pnlMCAOver1yr.Visible = false;
            rdbMCAOver1yrY.Checked = false;
            rdbMCAOver1yrN.Checked = false;
            pnlMCAOver9mon.Visible = false;
            rdbMCAOver9monY.Checked = false;
            rdbMCAOver9monN.Checked = false;
            pnlRapidAdvanceList.Visible = false;
            rdbRapidAdvanceListY.Checked = false;
            rdbRapidAdvanceListN.Checked = false;
            pnlAdvanceMeList.Visible = false;
            rdbAdvanceMeListY.Checked = false;
            rdbAdvanceMeListN.Checked = false;
            pnlMCAOver6mon.Visible = false;
            rdbMCAOver6monY.Checked = false;
            rdbMCAOver6monN.Checked = false;
            pnlBFSList.Visible = false;
            rdbBFSListY.Checked = false;
            rdbBFSListN.Checked = false;
            pnlMCAOver3mon.Visible = false;
            rdbMCAOver3monY.Checked = false;
            rdbMCAOver3monN.Checked = false;
            pnlProcesstenthousand.Visible = false;
            rdbProcesstenthousandY.Checked = false;
            rdbProcesstenthousandN.Checked = false;
            pnlRapidAdvanceListStarter.Visible = false;
            rdbRapidAdvanceListStarterY.Checked = false;
            rdbRapidAdvanceListStarterN.Checked = false;
            pnlBFSListStarter.Visible = false;
            rdbBFSListStarterY.Checked = false;
            rdbBFSListStarterN.Checked = false;
            pnlProcessFourthousand.Visible = false;
            rdbProcessFourthousandY.Checked = false;
            rdbProcessFourthousandN.Checked = false;
            pnlBFSListStater4000.Visible = false;
            rdbBFSListStater4000Y.Checked = false;
            rdbBFSListStater4000N.Checked = false;
        }
        if (sender.Equals(rdbMCABankruptN))
        {
            strBusiness = "";
            pnlMCAVolumn.Visible = true;
            rdbMCAVolumnY.Checked = false;
            rdbMCAVolumnN.Checked = false;
            pnlMCAOver1yr.Visible = false;
            rdbMCAOver1yrY.Checked = false;
            rdbMCAOver1yrN.Checked = false;
            pnlMCAOver9mon.Visible = false;
            rdbMCAOver9monY.Checked = false;
            rdbMCAOver9monN.Checked = false;
            pnlRapidAdvanceList.Visible = false;
            rdbRapidAdvanceListY.Checked = false;
            rdbRapidAdvanceListN.Checked = false;
            pnlAdvanceMeList.Visible = false;
            rdbAdvanceMeListY.Checked = false;
            rdbAdvanceMeListN.Checked = false;
            pnlMCAOver6mon.Visible = false;
            rdbMCAOver6monY.Checked = false;
            rdbMCAOver6monN.Checked = false;
            pnlBFSList.Visible = false;
            rdbBFSListY.Checked = false;
            rdbBFSListN.Checked = false;
            pnlMCAOver3mon.Visible = false;
            rdbMCAOver3monY.Checked = false;
            rdbMCAOver3monN.Checked = false;
            pnlProcesstenthousand.Visible = false;
            rdbProcesstenthousandY.Checked = false;
            rdbProcesstenthousandN.Checked = false;
            pnlRapidAdvanceListStarter.Visible = false;
            rdbRapidAdvanceListStarterY.Checked = false;
            rdbRapidAdvanceListStarterN.Checked = false;
            pnlBFSListStarter.Visible = false;
            rdbBFSListStarterY.Checked = false;
            rdbBFSListStarterN.Checked = false;
            pnlProcessFourthousand.Visible = false;
            rdbProcessFourthousandY.Checked = false;
            rdbProcessFourthousandN.Checked = false;
            pnlBFSListStater4000.Visible = false;
            rdbBFSListStater4000Y.Checked = false;
            rdbBFSListStater4000N.Checked = false;
        }
        if (sender.Equals(rdbMCAVolumnY))
        {
            strBusiness = "";
            pnlMCAOver1yr.Visible = true;
            rdbMCAOver1yrY.Checked = false;
            rdbMCAOver1yrN.Checked = false;
            pnlMCAOver9mon.Visible = false;
            rdbMCAOver9monY.Checked = false;
            rdbMCAOver9monN.Checked = false;
            pnlRapidAdvanceList.Visible = false;
            rdbRapidAdvanceListY.Checked = false;
            rdbRapidAdvanceListN.Checked = false;
            pnlAdvanceMeList.Visible = false;
            rdbAdvanceMeListY.Checked = false;
            rdbAdvanceMeListN.Checked = false;
            pnlMCAOver6mon.Visible = false;
            rdbMCAOver6monY.Checked = false;
            rdbMCAOver6monN.Checked = false;
            pnlBFSList.Visible = false;
            rdbBFSListY.Checked = false;
            rdbBFSListN.Checked = false;
            pnlMCAOver3mon.Visible = false;
            rdbMCAOver3monY.Checked = false;
            rdbMCAOver3monN.Checked = false;
            pnlProcesstenthousand.Visible = false;
            rdbProcesstenthousandY.Checked = false;
            rdbProcesstenthousandN.Checked = false;
            pnlRapidAdvanceListStarter.Visible = false;
            rdbRapidAdvanceListStarterY.Checked = false;
            rdbRapidAdvanceListStarterN.Checked = false;
            pnlBFSListStarter.Visible = false;
            rdbBFSListStarterY.Checked = false;
            rdbBFSListStarterN.Checked = false;
            pnlProcessFourthousand.Visible = false;
            rdbProcessFourthousandY.Checked = false;
            rdbProcessFourthousandN.Checked = false;
            pnlBFSListStater4000.Visible = false;
            rdbBFSListStater4000Y.Checked = false;
            rdbBFSListStater4000N.Checked = false;
        }
        if (sender.Equals(rdbMCAVolumnN))
        {
            strBusiness = "LowVolumn";
            pnlMCAOver1yr.Visible = false;
            rdbMCAOver1yrY.Checked = false;
            rdbMCAOver1yrN.Checked = false;
            pnlMCAOver9mon.Visible = false;
            rdbMCAOver9monY.Checked = false;
            rdbMCAOver9monN.Checked = false;
            pnlRapidAdvanceList.Visible = false;
            rdbRapidAdvanceListY.Checked = false;
            rdbRapidAdvanceListN.Checked = false;
            pnlAdvanceMeList.Visible = false;
            rdbAdvanceMeListY.Checked = false;
            rdbAdvanceMeListN.Checked = false;
            pnlMCAOver6mon.Visible = false;
            rdbMCAOver6monY.Checked = false;
            rdbMCAOver6monN.Checked = false;
            pnlBFSList.Visible = false;
            rdbBFSListY.Checked = false;
            rdbBFSListN.Checked = false;
            pnlMCAOver3mon.Visible = false;
            rdbMCAOver3monY.Checked = false;
            rdbMCAOver3monN.Checked = false;
            pnlProcesstenthousand.Visible = false;
            rdbProcesstenthousandY.Checked = false;
            rdbProcesstenthousandN.Checked = false;
            pnlRapidAdvanceListStarter.Visible = false;
            rdbRapidAdvanceListStarterY.Checked = false;
            rdbRapidAdvanceListStarterN.Checked = false;
            pnlBFSListStarter.Visible = false;
            rdbBFSListStarterY.Checked = false;
            rdbBFSListStarterN.Checked = false;
            pnlProcessFourthousand.Visible = false;
            rdbProcessFourthousandY.Checked = false;
            rdbProcessFourthousandN.Checked = false;
            pnlBFSListStater4000.Visible = false;
            rdbBFSListStater4000Y.Checked = false;
            rdbBFSListStater4000N.Checked = false;
        }
        if (sender.Equals(rdbMCAOver1yrN))
        {
            //strBusiness = "AMI";
            pnlMCAOver9mon.Visible = true;
            rdbMCAOver9monY.Checked = false;
            rdbMCAOver9monN.Checked = false;
            pnlRapidAdvanceList.Visible = false;
            rdbRapidAdvanceListY.Checked = false;
            rdbRapidAdvanceListN.Checked = false;
            pnlAdvanceMeList.Visible = false;
            rdbAdvanceMeListY.Checked = false;
            rdbAdvanceMeListN.Checked = false;
            pnlMCAOver6mon.Visible = false;
            rdbMCAOver6monY.Checked = false;
            rdbMCAOver6monN.Checked = false;
            pnlBFSList.Visible = false;
            rdbBFSListY.Checked = false;
            rdbBFSListN.Checked = false;
            pnlMCAOver3mon.Visible = false;
            rdbMCAOver3monY.Checked = false;
            rdbMCAOver3monN.Checked = false;
            pnlProcesstenthousand.Visible = false;
            rdbProcesstenthousandY.Checked = false;
            rdbProcesstenthousandN.Checked = false;
            pnlRapidAdvanceListStarter.Visible = false;
            rdbRapidAdvanceListStarterY.Checked = false;
            rdbRapidAdvanceListStarterN.Checked = false;
            pnlBFSListStarter.Visible = false;
            rdbBFSListStarterY.Checked = false;
            rdbBFSListStarterN.Checked = false;
            pnlProcessFourthousand.Visible = false;
            rdbProcessFourthousandY.Checked = false;
            rdbProcessFourthousandN.Checked = false;
            pnlBFSListStater4000.Visible = false;
            rdbBFSListStater4000Y.Checked = false;
            rdbBFSListStater4000N.Checked = false;
        }
        if (sender.Equals(rdbMCAOver1yrY))
        {
            strBusiness = "";
            pnlMCAOver9mon.Visible = false;
            rdbMCAOver9monY.Checked = false;
            rdbMCAOver9monN.Checked = false;
            pnlRapidAdvanceList.Visible = true;
            rdbRapidAdvanceListY.Checked = false;
            rdbRapidAdvanceListN.Checked = false;
            pnlAdvanceMeList.Visible = false;
            rdbAdvanceMeListY.Checked = false;
            rdbAdvanceMeListN.Checked = false;
            pnlMCAOver6mon.Visible = false;
            rdbMCAOver6monY.Checked = false;
            rdbMCAOver6monN.Checked = false;
            pnlBFSList.Visible = false;
            rdbBFSListY.Checked = false;
            rdbBFSListN.Checked = false;
            pnlMCAOver3mon.Visible = false;
            rdbMCAOver3monY.Checked = false;
            rdbMCAOver3monN.Checked = false;
            pnlProcesstenthousand.Visible = false;
            rdbProcesstenthousandY.Checked = false;
            rdbProcesstenthousandN.Checked = false;
            pnlRapidAdvanceListStarter.Visible = false;
            rdbRapidAdvanceListStarterY.Checked = false;
            rdbRapidAdvanceListStarterN.Checked = false;
            pnlBFSListStarter.Visible = false;
            rdbBFSListStarterY.Checked = false;
            rdbBFSListStarterN.Checked = false;
            pnlProcessFourthousand.Visible = false;
            rdbProcessFourthousandY.Checked = false;
            rdbProcessFourthousandN.Checked = false;
            pnlBFSListStater4000.Visible = false;
            rdbBFSListStater4000Y.Checked = false;
            rdbBFSListStater4000N.Checked = false;
        }

        if (sender.Equals(rdbRapidAdvanceListY))
        {
            strBusiness = "";
            pnlAdvanceMeList.Visible = true;
            rdbAdvanceMeListY.Checked = false;
            rdbAdvanceMeListN.Checked = false;
            pnlMCAOver6mon.Visible = false;
            rdbMCAOver6monY.Checked = false;
            rdbMCAOver6monN.Checked = false;
            pnlBFSList.Visible = false;
            rdbBFSListY.Checked = false;
            rdbBFSListN.Checked = false;
            pnlMCAOver3mon.Visible = false;
            rdbMCAOver3monY.Checked = false;
            rdbMCAOver3monN.Checked = false;
            pnlProcesstenthousand.Visible = false;
            rdbProcesstenthousandY.Checked = false;
            rdbProcesstenthousandN.Checked = false;
            pnlRapidAdvanceListStarter.Visible = false;
            rdbRapidAdvanceListStarterY.Checked = false;
            rdbRapidAdvanceListStarterN.Checked = false;
            pnlBFSListStarter.Visible = false;
            rdbBFSListStarterY.Checked = false;
            rdbBFSListStarterN.Checked = false;
            pnlProcessFourthousand.Visible = false;
            rdbProcessFourthousandY.Checked = false;
            rdbProcessFourthousandN.Checked = false;
            pnlBFSListStater4000.Visible = false;
            rdbBFSListStater4000Y.Checked = false;
            rdbBFSListStater4000N.Checked = false;

        }
        if (sender.Equals(rdbRapidAdvanceListN))
        {
            strBusiness = "RapidAdvance";
            pnlAdvanceMeList.Visible = false;
            rdbAdvanceMeListY.Checked = false;
            rdbAdvanceMeListN.Checked = false;
            pnlMCAOver6mon.Visible = false;
            rdbMCAOver6monY.Checked = false;
            rdbMCAOver6monN.Checked = false;
            pnlBFSList.Visible = false;
            rdbBFSListY.Checked = false;
            rdbBFSListN.Checked = false;
            pnlMCAOver3mon.Visible = false;
            rdbMCAOver3monY.Checked = false;
            rdbMCAOver3monN.Checked = false;
            pnlProcesstenthousand.Visible = false;
            rdbProcesstenthousandY.Checked = false;
            rdbProcesstenthousandN.Checked = false;
            pnlRapidAdvanceListStarter.Visible = false;
            rdbRapidAdvanceListStarterY.Checked = false;
            rdbRapidAdvanceListStarterN.Checked = false;
            pnlBFSListStarter.Visible = false;
            rdbBFSListStarterY.Checked = false;
            rdbBFSListStarterN.Checked = false;
            pnlProcessFourthousand.Visible = false;
            rdbProcessFourthousandY.Checked = false;
            rdbProcessFourthousandN.Checked = false;
            pnlBFSListStater4000.Visible = false;
            rdbBFSListStater4000Y.Checked = false;
            rdbBFSListStater4000N.Checked = false;
        }

        if (sender.Equals(rdbAdvanceMeListY))
        {
            strBusiness = "Unacceptable";
            pnlAdvanceMeList.Visible = true;
            rdbAdvanceMeListY.Checked = true;
            rdbAdvanceMeListN.Checked = false;
            pnlMCAOver6mon.Visible = false;
            rdbMCAOver6monY.Checked = false;
            rdbMCAOver6monN.Checked = false;
            pnlBFSList.Visible = false;
            rdbBFSListY.Checked = false;
            rdbBFSListN.Checked = false;
            pnlMCAOver3mon.Visible = false;
            rdbMCAOver3monY.Checked = false;
            rdbMCAOver3monN.Checked = false;
            pnlProcesstenthousand.Visible = false;
            rdbProcesstenthousandY.Checked = false;
            rdbProcesstenthousandN.Checked = false;
            pnlRapidAdvanceListStarter.Visible = false;
            rdbRapidAdvanceListStarterY.Checked = false;
            rdbRapidAdvanceListStarterN.Checked = false;
            pnlBFSListStarter.Visible = false;
            rdbBFSListStarterY.Checked = false;
            rdbBFSListStarterN.Checked = false;
            pnlProcessFourthousand.Visible = false;
            rdbProcessFourthousandY.Checked = false;
            rdbProcessFourthousandN.Checked = false;
            pnlBFSListStater4000.Visible = false;
            rdbBFSListStater4000Y.Checked = false;
            rdbBFSListStater4000N.Checked = false;

        }

        if (sender.Equals(rdbAdvanceMeListN))
        {
            strBusiness = "AMI";
            pnlAdvanceMeList.Visible = true;
            rdbAdvanceMeListY.Checked = false;
            rdbAdvanceMeListN.Checked = true;
            pnlMCAOver6mon.Visible = false;
            rdbMCAOver6monY.Checked = false;
            rdbMCAOver6monN.Checked = false;
            pnlBFSList.Visible = false;
            rdbBFSListY.Checked = false;
            rdbBFSListN.Checked = false;
            pnlMCAOver3mon.Visible = false;
            rdbMCAOver3monY.Checked = false;
            rdbMCAOver3monN.Checked = false;
            pnlProcesstenthousand.Visible = false;
            rdbProcesstenthousandY.Checked = false;
            rdbProcesstenthousandN.Checked = false;
            pnlRapidAdvanceListStarter.Visible = false;
            rdbRapidAdvanceListStarterY.Checked = false;
            rdbRapidAdvanceListStarterN.Checked = false;
            pnlBFSListStarter.Visible = false;
            rdbBFSListStarterY.Checked = false;
            rdbBFSListStarterN.Checked = false;
            pnlProcessFourthousand.Visible = false;
            rdbProcessFourthousandY.Checked = false;
            rdbProcessFourthousandN.Checked = false;
            pnlBFSListStater4000.Visible = false;
            rdbBFSListStater4000Y.Checked = false;
            rdbBFSListStater4000N.Checked = false;

        }

        if (sender.Equals(rdbMCAOver9monN))
        {
            strBusiness = "";
            pnlMCAOver6mon.Visible = true;
            rdbMCAOver6monY.Checked = false;
            rdbMCAOver6monN.Checked = false;
            pnlBFSList.Visible = false;
            rdbBFSListY.Checked = false;
            rdbBFSListN.Checked = false;
            pnlMCAOver3mon.Visible = false;
            rdbMCAOver3monY.Checked = false;
            rdbMCAOver3monN.Checked = false;
            pnlProcesstenthousand.Visible = false;
            rdbProcesstenthousandY.Checked = false;
            rdbProcesstenthousandN.Checked = false;
            pnlRapidAdvanceListStarter.Visible = false;
            rdbRapidAdvanceListStarterY.Checked = false;
            rdbRapidAdvanceListStarterN.Checked = false;
            pnlBFSListStarter.Visible = false;
            rdbBFSListStarterY.Checked = false;
            rdbBFSListStarterN.Checked = false;
            pnlProcessFourthousand.Visible = false;
            rdbProcessFourthousandY.Checked = false;
            rdbProcessFourthousandN.Checked = false;
            pnlBFSListStater4000.Visible = false;
            rdbBFSListStater4000Y.Checked = false;
            rdbBFSListStater4000N.Checked = false;
        }

        if (sender.Equals(rdbMCAOver9monY))
        {
            strBusiness = "";
            pnlBFSList.Visible = true;
            rdbBFSListY.Checked = false;
            rdbBFSListN.Checked = false;
            pnlMCAOver6mon.Visible = false;
            rdbMCAOver6monY.Checked = false;
            rdbMCAOver6monN.Checked = false;
            pnlMCAOver3mon.Visible = false;
            rdbMCAOver3monY.Checked = false;
            rdbMCAOver3monN.Checked = false;
            pnlProcesstenthousand.Visible = false;
            rdbProcesstenthousandY.Checked = false;
            rdbProcesstenthousandN.Checked = false;
            pnlRapidAdvanceListStarter.Visible = false;
            rdbRapidAdvanceListStarterY.Checked = false;
            rdbRapidAdvanceListStarterN.Checked = false;
            pnlBFSListStarter.Visible = false;
            rdbBFSListStarterY.Checked = false;
            rdbBFSListStarterN.Checked = false;
            pnlProcessFourthousand.Visible = false;
            rdbProcessFourthousandY.Checked = false;
            rdbProcessFourthousandN.Checked = false;
            pnlBFSListStater4000.Visible = false;
            rdbBFSListStater4000Y.Checked = false;
            rdbBFSListStater4000N.Checked = false;
        }

        if (sender.Equals(rdbBFSListY))
        {
            strBusiness = "Unacceptable";
            pnlMCAOver6mon.Visible = false;
            rdbMCAOver6monY.Checked = false;
            rdbMCAOver6monN.Checked = false;
            pnlMCAOver3mon.Visible = false;
            rdbMCAOver3monY.Checked = false;
            rdbMCAOver3monN.Checked = false;
            pnlProcesstenthousand.Visible = false;
            rdbProcesstenthousandY.Checked = false;
            rdbProcesstenthousandN.Checked = false;
            pnlRapidAdvanceListStarter.Visible = false;
            rdbRapidAdvanceListStarterY.Checked = false;
            rdbRapidAdvanceListStarterN.Checked = false;
            pnlBFSListStarter.Visible = false;
            rdbBFSListStarterY.Checked = false;
            rdbBFSListStarterN.Checked = false;
            pnlProcessFourthousand.Visible = false;
            rdbProcessFourthousandY.Checked = false;
            rdbProcessFourthousandN.Checked = false;
            pnlBFSListStater4000.Visible = false;
            rdbBFSListStater4000Y.Checked = false;
            rdbBFSListStater4000N.Checked = false;
        }

        if (sender.Equals(rdbBFSListN))
        {
            strBusiness = "BFS";
            pnlMCAOver6mon.Visible = false;
            rdbMCAOver6monY.Checked = false;
            rdbMCAOver6monN.Checked = false;
            pnlMCAOver3mon.Visible = false;
            rdbMCAOver3monY.Checked = false;
            rdbMCAOver3monN.Checked = false;
            pnlProcesstenthousand.Visible = false;
            rdbProcesstenthousandY.Checked = false;
            rdbProcesstenthousandN.Checked = false;
            pnlRapidAdvanceListStarter.Visible = false;
            rdbRapidAdvanceListStarterY.Checked = false;
            rdbRapidAdvanceListStarterN.Checked = false;
            pnlBFSListStarter.Visible = false;
            rdbBFSListStarterY.Checked = false;
            rdbBFSListStarterN.Checked = false;
            pnlProcessFourthousand.Visible = false;
            rdbProcessFourthousandY.Checked = false;
            rdbProcessFourthousandN.Checked = false;
            pnlBFSListStater4000.Visible = false;
            rdbBFSListStater4000Y.Checked = false;
            rdbBFSListStater4000N.Checked = false;
        }

        if (sender.Equals(rdbMCAOver6monN))
        {
            strBusiness = "";
            pnlMCAOver3mon.Visible = true;
            rdbMCAOver3monY.Checked = false;
            rdbMCAOver3monN.Checked = false;
            pnlProcesstenthousand.Visible = false;
            rdbProcesstenthousandY.Checked = false;
            rdbProcesstenthousandN.Checked = false;
            pnlRapidAdvanceListStarter.Visible = false;
            rdbRapidAdvanceListStarterY.Checked = false;
            rdbRapidAdvanceListStarterN.Checked = false;
            pnlProcessFourthousand.Visible = false;
            rdbProcessFourthousandY.Checked = false;
            rdbProcessFourthousandN.Checked = false;
            pnlBFSListStarter.Visible = false;
            rdbBFSListStarterY.Checked = false;
            rdbBFSListStarterN.Checked = false;

            pnlBFSListStater4000.Visible = false;
            rdbBFSListStater4000Y.Checked = false;
            rdbBFSListStater4000N.Checked = false;
        }

        if (sender.Equals(rdbMCAOver6monY))
        {
            strBusiness = "";
            pnlMCAOver3mon.Visible = false;
            rdbMCAOver3monY.Checked = false;
            rdbMCAOver3monN.Checked = false;
            pnlProcesstenthousand.Visible = true;
            rdbProcesstenthousandY.Checked = false;
            rdbProcesstenthousandN.Checked = false;
            pnlRapidAdvanceListStarter.Visible = false;
            rdbRapidAdvanceListStarterY.Checked = false;
            rdbRapidAdvanceListStarterN.Checked = false;
            pnlProcessFourthousand.Visible = false;
            rdbProcessFourthousandY.Checked = false;
            rdbProcessFourthousandN.Checked = false;
            pnlBFSListStarter.Visible = false;
            rdbBFSListStarterY.Checked = false;
            rdbBFSListStarterN.Checked = false;

            pnlBFSListStater4000.Visible = false;
            rdbBFSListStater4000Y.Checked = false;
            rdbBFSListStater4000N.Checked = false;
        }

        if (sender.Equals(rdbProcesstenthousandY))
        {
            strBusiness = "";
            pnlRapidAdvanceListStarter.Visible = true;
            rdbRapidAdvanceListStarterY.Checked = false;
            rdbRapidAdvanceListStarterN.Checked = false;
            pnlProcessFourthousand.Visible = false;
            rdbProcessFourthousandY.Checked = false;
            rdbProcessFourthousandN.Checked = false;
            pnlBFSListStarter.Visible = false;
            rdbBFSListStarterY.Checked = false;
            rdbBFSListStarterN.Checked = false;
            pnlBFSListStater4000.Visible = false;
            rdbBFSListStater4000Y.Checked = false;
            rdbBFSListStater4000N.Checked = false;
        }

        if (sender.Equals(rdbProcesstenthousandN))
        {
            strBusiness = "";
            pnlRapidAdvanceListStarter.Visible = false;
            rdbRapidAdvanceListStarterY.Checked = false;
            rdbRapidAdvanceListStarterN.Checked = false;
            pnlProcessFourthousand.Visible = true;
            rdbProcessFourthousandY.Checked = false;
            rdbProcessFourthousandN.Checked = false;
            pnlBFSListStarter.Visible = false;
            rdbBFSListStarterY.Checked = false;
            rdbBFSListStarterN.Checked = false;
            pnlBFSListStater4000.Visible = false;
            rdbBFSListStater4000Y.Checked = false;
            rdbBFSListStater4000N.Checked = false;
        }

        if (sender.Equals(rdbRapidAdvanceListStarterY))
        {
            strBusiness = "";
            pnlBFSListStarter.Visible = true;
            rdbRapidAdvanceListStarterN.Checked = false;
            rdbBFSListStarterY.Checked = false;
            rdbBFSListStarterN.Checked = false;
            pnlBFSListStater4000.Visible = false;
            rdbBFSListStater4000Y.Checked = false;
            rdbBFSListStater4000N.Checked = false;
        }

        if (sender.Equals(rdbRapidAdvanceListStarterN))
        {
            strBusiness = "RapidAdvance";
            rdbRapidAdvanceListStarterY.Checked = false;
            pnlBFSListStarter.Visible = false;
            rdbBFSListStarterY.Checked = false;
            rdbBFSListStarterN.Checked = false;
            pnlBFSListStater4000.Visible = false;
            rdbBFSListStater4000Y.Checked = false;
            rdbBFSListStater4000N.Checked = false;
        }

        if (sender.Equals(rdbBFSListStarterY))
        {
            strBusiness = "Unacceptable";
            rdbBFSListStarterN.Checked = false;
            pnlBFSListStater4000.Visible = false;
            rdbBFSListStater4000Y.Checked = false;
            rdbBFSListStater4000N.Checked = false;
        }

        if (sender.Equals(rdbBFSListStarterN))
        {
            strBusiness = "BFS";
            rdbBFSListStarterY.Checked = false;
            pnlBFSListStater4000.Visible = false;
            rdbBFSListStater4000Y.Checked = false;
            rdbBFSListStater4000N.Checked = false;
        }

        if (sender.Equals(rdbProcessFourthousandN))
        {
            strBusiness = "BelowFourthousand";
            pnlProcessFourthousand.Visible = true;
            rdbProcessFourthousandY.Checked = false;
            pnlBFSListStater4000.Visible = false;
            rdbBFSListStater4000Y.Checked = false;
            rdbBFSListStater4000N.Checked = false;
        }

        if (sender.Equals(rdbProcessFourthousandY))
        {
            strBusiness = "";
            pnlBFSListStater4000.Visible = true;
            rdbBFSListStater4000Y.Checked = false;
            rdbBFSListStater4000N.Checked = false;
        }

        if (sender.Equals(rdbBFSListStater4000Y))
        {
            strBusiness = "Unacceptable";
        }

        if (sender.Equals(rdbBFSListStater4000N))
        {
            strBusiness = "BFS";
        }

        if (sender.Equals(rdbMCAOver3monN))
        {
            strBusiness = "Below3mon";
            pnlProcesstenthousand.Visible = false;
            rdbProcesstenthousandY.Checked = false;
            rdbProcesstenthousandN.Checked = false;
            pnlRapidAdvanceListStarter.Visible = false;
            rdbRapidAdvanceListStarterY.Checked = false;
            rdbRapidAdvanceListStarterN.Checked = false;
            pnlProcessFourthousand.Visible = false;
            rdbProcessFourthousandY.Checked = false;
            rdbProcessFourthousandN.Checked = false;
            pnlBFSListStarter.Visible = false;
            rdbBFSListStarterY.Checked = false;
            rdbBFSListStarterN.Checked = false;
            pnlBFSListStater4000.Visible = false;
            rdbBFSListStater4000Y.Checked = false;
            rdbBFSListStater4000N.Checked = false;
        }

        if (sender.Equals(rdbMCAOver3monY))
        {
            strBusiness = "";
            pnlProcesstenthousand.Visible = true;
            rdbProcesstenthousandY.Checked = false;
            rdbProcesstenthousandN.Checked = false;
            pnlRapidAdvanceListStarter.Visible = false;
            rdbRapidAdvanceListStarterY.Checked = false;
            rdbRapidAdvanceListStarterN.Checked = false;
            pnlProcessFourthousand.Visible = false;
            rdbProcessFourthousandY.Checked = false;
            rdbProcessFourthousandN.Checked = false;
            pnlBFSListStarter.Visible = false;
            rdbBFSListStarterY.Checked = false;
            rdbBFSListStarterN.Checked = false;
            pnlBFSListStater4000.Visible = false;
            rdbBFSListStater4000Y.Checked = false;
            rdbBFSListStater4000N.Checked = false;
        }


    }//end function radio button check changed

    //This function displays error messages
    protected void SetErrorMessage(string errText)
    {
        lblError.Visible = true;
        lblError.Text = errText;
    }//end function set error message
}
