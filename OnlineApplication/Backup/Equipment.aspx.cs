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

public partial class Equipment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.CacheControl = "no-cache";
        Response.AddHeader("Pragma", "no-cache");
        Response.Expires = -1;

        if (!Page.IsPostBack)
        {
            try
            {
                Populate();
            }
            catch (Exception err)
            {
                lblError.Visible = true;
                lblError.Text = "Error loading information - " + err.ToString();
            }
        }//end if postback
    }

    public void Populate()
    {
        Style ValueLabelHeader = new Style();
        ValueLabelHeader.CssClass = "LabelsHeader";
                
        //tblEquipment.Attributes.Add("style", "width: 95%; border-right: silver 1px solid;border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid");

        #region Applications
        //Create Retail - 2
        //Get Product list for Retail
        //string[] arr = {"", "Mobile Applications", "Retail Application", "Software Applications", "Accessories"};
        //string[] arr = {"", "Terminals - Dial Up", "Terminals - IP", "Terminals - Wireless", "Pin Pads", "Check Readers & Scanners",
        //                "Contactless Readers", "Card Readers", "Printers", "POS Systems & Equipment", "Software", "Accessories", "Supplies", ""};

        AffiliatesWizBL Aff = new AffiliatesWizBL();
        DataSet dsCat = Aff.GetProductCategories();
        DataTable dtCat = dsCat.Tables[0];
        //arr[0] = "";
        /*for (int i = 0; i < arr.Length; i++)
        {
            DataRow drCat = dtCat.Rows[i];
            arr[i] = drCat["CategoryName"].ToString().Trim();
        }*/

        Table tblInnerTable;
        TableRow trInner;
        TableCell tdInner;
        Label lblValue;
        Label lblMore;
        Image imgEquip;
        DataSet ds = new DataSet();
        TableRow tr;
        TableCell td;
        for (int j = 0; j < dtCat.Rows.Count; j++)
        {
            DataRow drCat = dtCat.Rows[j];

            tr = new TableRow();
            td = new TableCell();
            td.Text = "<div align=\"left\">"
                        + "&nbsp;<img src=\"images/bullet.gif\" height=\"8\" width=\"8\" border=\"0\">"
                         + "&nbsp;<a class=\"LabelsHeader\" href=\"#" + drCat["CategoryID"].ToString().Trim() + "\">" + drCat["CategoryName"].ToString().Trim() + "</a></div>";
            //Add this cell to main table row
            tr.Cells.Add(td);

            j++;

            if (j < dtCat.Rows.Count)
            {
                td = new TableCell();
                drCat = dtCat.Rows[j];
                td.Text = "<div align=\"left\">"
                            + "&nbsp;<img src=\"images/bullet.gif\" height=\"8\" width=\"8\" border=\"0\">"
                             + "&nbsp;<a class=\"LabelsHeader\" href=\"#" + drCat["CategoryID"].ToString().Trim() + "\">" + drCat["CategoryName"].ToString().Trim() + "</a></div>";
                //Add this cell to main table row
                tr.Cells.Add(td);
            }

            j++;
            if (j < dtCat.Rows.Count)
            {
                td = new TableCell();
                drCat = dtCat.Rows[j];
                td.Text = "<div align=\"left\">"
                            + "&nbsp;<img src=\"images/bullet.gif\" height=\"8\" width=\"8\" border=\"0\">"
                             + "&nbsp;<a class=\"LabelsHeader\" href=\"#" + drCat["CategoryID"].ToString().Trim() + "\">" + drCat["CategoryName"].ToString().Trim() + "</a></div>";
            }
            
            //Add this cell to main table row
            tr.Cells.Add(td);

            //Add this row to main table
            tblEquipment.Rows.Add(tr);
        }

        for (int j = 0; j < dtCat.Rows.Count; j++)
        {            
            DataRow drCat = dtCat.Rows[j];
            ds = GetProductList(drCat["CategoryID"].ToString().Trim());
            if (ds.Tables[0].Rows.Count > 0)
            {                
                #region Header Row
                //*********************CREATE HEADER ROW******************
                tr = new TableRow();//Table Row declation
                td = new TableCell();//Table Cell declaration

                tr.CssClass = "DivHeader";
                td = new TableCell();//Create a new cell for each column
                td.Text = "<a name=\"" + drCat["CategoryID"] + "\"></a><b>" + drCat["CategoryName"].ToString().Trim() + "</b>";
                td.CssClass = "MenuHeader";
                //td.Style["Height"] = "20px";
                td.ColumnSpan = 3;
                tr.Cells.Add(td);//Add the cell created to the row

                tblEquipment.Rows.Add(tr);//Add the Header row to the table
                //*********************END HEADER ROW**********************
                #endregion

                #region Rows
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];
                    HyperLink lnkEquip;
                    tr = new TableRow();
                    td = new TableCell();
                    td.ColumnSpan = 3;

                    #region Inner Table
                    tblInnerTable = new Table();
                    tblInnerTable.Width = Unit.Percentage(100);

                    #region First Row
                    trInner = new TableRow();

                    //Image
                    tdInner = new TableCell();
                    imgEquip = new Image();
                    imgEquip.ImageUrl = "Images/ProductImages/" + dr["ImageURL"].ToString().Trim();
                    imgEquip.Height = Unit.Pixel(125);
                    imgEquip.Width = Unit.Pixel(125);
                    imgEquip.ToolTip = dr["ProductName"].ToString().Trim();
                    tdInner.Controls.Add(imgEquip);
                    tdInner.Attributes.Add("align", "center");
                    //tdInner.Style["Width"] = "131px";
                    tdInner.Attributes.Add("width", "131px");
                    trInner.Cells.Add(tdInner);

                    //Description
                    tdInner = new TableCell();
                    lblValue = new Label();
                    lblValue.CssClass = "LabelsBodySmall";
                    //Hyperlink for image
                    lblMore = new Label();
                    if (dr["TargetURL"].ToString().Trim() != "")
                        lblMore.Text = "<a class=\"LabelsHeader\" href=\"" + dr["TargetURL"].ToString().Trim() + "\" target=\"_blank\">more...</a>";
                    else
                        lblMore.Text = "";
                    lblValue.Text = "<div style=\"width: 90%; height: 90%;\" >" + dr["SellDescription"].ToString().Trim() + " " + lblMore.Text + "</div>";
                    tdInner.Controls.Add(lblValue);
                    tdInner.Attributes.Add("align", "left");
                    tdInner.Attributes.Add("valign", "middle");
                    trInner.Cells.Add(tdInner);

                    tblInnerTable.Rows.Add(trInner);
                    #endregion

                    #region Second Row
                    trInner = new TableRow();                                        
                    tdInner = new TableCell();
                    //Product Name
                    lblValue = new Label();
                    lblValue.ApplyStyle(ValueLabelHeader);
                    lblValue.Font.Bold = true;
                    lblValue.Text = dr["ProductName"].ToString().Trim() + "<br/>";
                    tdInner.Controls.Add(lblValue);                    
                    trInner.Cells.Add(tdInner);

                    //Buy Image
                    if ((dr["ProstoresCode"].ToString().Trim() != "") && (dr["ProstoresCode"].ToString().Trim() != "0"))
                    {
                        lblValue = new Label();
                        lblValue.Text = "<a href=\"http://store.ecenow.com/cart.php?action=add&product_id=" + dr["ProstoresCode"].ToString().Trim() + "&amp;qty=1\" onmouseover=\"document.Buy" + dr["ProductCode"].ToString().Trim() + ".src='Images/buy_mouseover.jpg'\" onmouseout=\"document.Buy" + dr["ProductCode"].ToString().Trim() + ".src='Images/buy.jpg'\" target=\"_blank\"><img name=\"Buy" + dr["ProductCode"].ToString().Trim() + "\" alt=\"\" src=\"Images/buy.jpg\" border=\"0\" /></a>";
                        tdInner.Controls.Add(lblValue);
                    }
                    tdInner.Attributes.Add("align", "center");
                    tdInner.Attributes.Add("width", "131px");
                    trInner.Cells.Add(tdInner);                    

                    tblInnerTable.Rows.Add(trInner);

                    //Horizontal Line
                    trInner = new TableRow();
                    tdInner = new TableCell();
                    tdInner.Attributes.Add("align", "center");
                    tdInner.Attributes.Add("colspan", "2");
                    lblValue = new Label();
                    lblValue.Text = "<hr noshade=\"noshade\" size=\"1\" width=\"90%\" />";
                    tdInner.Controls.Add(lblValue);
                    trInner.Cells.Add(tdInner);

                    tblInnerTable.Rows.Add(trInner);

                    #endregion

                    #endregion

                    //Add inner table to main table cell
                    td.Controls.Add(tblInnerTable);

                    //Add this cell to main table row
                    tr.Cells.Add(td);

                    //Add this row to main table
                    tblEquipment.Rows.Add(tr);
                }//end for
                #endregion
            }//end if retail count not 0
        }//end j
        #endregion
    }

    public DataSet GetProductList(string Cat)
    {
        int AffiliateID = 11;
        HttpCookie ck = Request.Cookies["AffiliateWizAffiliateID"];
        if (ck != null)
            AffiliateID = Convert.ToInt32(ck["AffiliateID"].ToString().Trim());
        AffiliatesWizBL Aff = new AffiliatesWizBL();
        DataSet dsList = Aff.GetProductsByAffiliateID(AffiliateID, Cat);
        return dsList;
    }
}
