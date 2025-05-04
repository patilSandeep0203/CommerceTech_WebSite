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

public partial class Glossary_Glossary : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    #region Leter Clicks Events
    protected void lnkA_Click(object sender, EventArgs e)
    {
        Populate("A");
    }//end link button

    protected void lnkB_Click(object sender, EventArgs e)
    {
        Populate("B");
    }//end link button

    protected void lnkC_Click(object sender, EventArgs e)
    {
        Populate("C");
    }//end link button

    protected void lnkD_Click(object sender, EventArgs e)
    {
        Populate("D");
    }//end link button

    protected void lnkE_Click(object sender, EventArgs e)
    {
        Populate("E");
    }//end link button

    protected void lnkF_Click(object sender, EventArgs e)
    {
        Populate("F");
    }//end link button

    protected void lnkG_Click(object sender, EventArgs e)
    {
        Populate("G");
    }//end link button

    protected void lnkH_Click(object sender, EventArgs e)
    {
        Populate("H");
    }//end link button

    protected void lnkI_Click(object sender, EventArgs e)
    {
        Populate("I");
    }//end link button

    protected void lnkJ_Click(object sender, EventArgs e)
    {
        Populate("J");
    }//end link button

    protected void lnkK_Click(object sender, EventArgs e)
    {
        Populate("K");
    }//end link button

    protected void lnkL_Click(object sender, EventArgs e)
    {
        Populate("L");
    }//end link button

    protected void lnkM_Click(object sender, EventArgs e)
    {
        Populate("M");
    }//end link button

    protected void lnkN_Click(object sender, EventArgs e)
    {
        Populate("N");
    }//end link button

    protected void lnkO_Click(object sender, EventArgs e)
    {
        Populate("O");
    }//end link button

    protected void lnkP_Click(object sender, EventArgs e)
    {
        Populate("P");
    }//end link button

    protected void lnkQ_Click(object sender, EventArgs e)
    {
        Populate("Q");
    }//end link button

    protected void lnkR_Click(object sender, EventArgs e)
    {
        Populate("R");
    }//end link button

    protected void lnkS_Click(object sender, EventArgs e)
    {
        Populate("S");
    }//end link button

    protected void lnkT_Click(object sender, EventArgs e)
    {
        Populate("T");
    }//end link button

    protected void lnkU_Click(object sender, EventArgs e)
    {
        Populate("U");
    }//end link button

    protected void lnkV_Click(object sender, EventArgs e)
    {
        Populate("V");
    }//end link button

    protected void lnkW_Click(object sender, EventArgs e)
    {
        Populate("W");
    }//end link button

    protected void lnkX_Click(object sender, EventArgs e)
    {
        Populate("X");
    }//end link button

    protected void lnkY_Click(object sender, EventArgs e)
    {
        Populate("Y");
    }//end link button

    protected void lnkZ_Click(object sender, EventArgs e)
    {
        Populate("Z");
    }//end link button
    #endregion

    public void Populate(string Letter)
    {
        tblGlossary.Visible = true;
        pnlDetail.Visible = false;

        Style ValueLabelHeader = new Style();
        ValueLabelHeader.CssClass = "LabelsHeader";

        Style ValueLabelDesc = new Style();
        ValueLabelDesc.CssClass = "LabelsBody";

        AffiliatesWizBL Aff = new AffiliatesWizBL();
        DataSet ds = Aff.GetGlossarybyAlphabet(Letter);
        if (ds.Tables[0].Rows.Count > 0)
        {
            TableRow tr = new TableRow();//Table Row declation
            TableCell td = new TableCell();//Table Cell declaration

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow dr = ds.Tables[0].Rows[i];
                tr = new TableRow();
                td = new TableCell();
                Label lblWord = new Label();
                lblWord.ApplyStyle(ValueLabelHeader);
                lblWord.Font.Bold = true;
                lblWord.Text = dr["Word"].ToString().Trim();
                td.Controls.Add(lblWord);
                td.Attributes.Add("align", "left");
                td.Attributes.Add("valign", "top");
                td.Attributes.Add("width", "15%");
                tr.Cells.Add(td);

                td = new TableCell();
                Label lblDesc = new Label();
                lblDesc.ApplyStyle(ValueLabelDesc);
                lblDesc.Text = dr["Definition"].ToString().Trim();
                td.Controls.Add(lblDesc);
                td.Attributes.Add("align", "left");
                td.Attributes.Add("valign", "top");
                tr.Cells.Add(td);
                tblGlossary.Rows.Add(tr);

                //space
                tr = new TableRow();
                tr.Height = 5;
                td = new TableCell();
                td.Attributes.Add("align", "center");
                td.Attributes.Add("colspan", "2");
                lblWord = new Label();
                td.Controls.Add(lblWord);
                tr.Cells.Add(td);
                tblGlossary.Rows.Add(tr);
            }
        }

    }
}

