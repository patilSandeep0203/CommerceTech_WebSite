<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RevelPOSComparison.aspx.cs" 
    Inherits="QuickBooksComparison" Title="QuickBooks Comparison" Theme="Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
<br />
    <span class="LabelsHeaderLarge"><strong>Revel pricing</strong></span><br /><br />        
    <table align="right" width="98%" cellspacing=2 cellpadding=0 border=0 class="DivWhiteSmoke;">
        <tr>
            <td align="left" class="DivHeader" valign="middle" width="440px">
                <span class="MenuHeader"><b>&nbsp;</b></span></td>
            <td align="center"  class="DivHeader" valign="middle" width="120px">
                <span class="MenuHeader"><b>Revel</b></span></td>            
            <td align="center"  valign="middle"  width="120px">
                </td>
            <td align="center"  valign="middle" width="120px">
               </td>

        </tr>
        <tr height=25>
            <td align="left" valign="middle" colspan=4>
                <span class="LabelsHeader"><strong>Price</strong></span>
                <asp:LinkButton ID="btn1" CssClass="Link" runat="server" Text="Show/Hide" onclick="btn1_click" />                               
            </td>
        </tr>
        <asp:Panel ID="pnl1" runat="server" Visible="true">
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody"><b>Price</b></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">$1,749-$1,999+$119/Month</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
        </tr>
        
        
                
        
               

        <tr ><td height="5px"></td></tr>
        </asp:Panel>
        <tr height=25>
            <td align="left" valign="middle" colspan=4>
                <span class="LabelsHeader"><strong>Hardware</strong></span>
                <asp:LinkButton ID="btn2" CssClass="Link" runat="server" Text="Show/Hide" onclick="btn2_click" />                               
            </td>
        </tr>
        <asp:Panel ID="pnl2" runat="server" Visible="true">
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody"><b>Ipad</b></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">Ipad Air</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody"><b>Stand<b/></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">Revel iPad Stand</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
        </tr>        
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody"><b>Reader</b></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">iPP350</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
        </tr> 
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody"><b>Printer<b/></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">Epson T20</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
        </tr> 
        <tr ><td height="5px"></td></tr>
         <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody"><b>Router</b></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">Router/AP</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
        </tr> 
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody"><b>Cash Drawer</b></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">16*16</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
        </tr> 
        <tr ><td height="5px"></td></tr>      
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody"><b>Networking Kit (1 surge protector + 2 Data ethernet cables)</b></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">Included</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
        </tr>
        <tr ><td height="5px"></td></tr>     
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody"><b>Scanner (Datalogic GD4400 Gryphon)</b></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">$149</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        </asp:Panel>
        <tr height=25>
            <td align="left" valign="middle" colspan=4>
                <span class="LabelsHeader"><strong>Features/Modules</strong></span>
                <asp:LinkButton ID="btn3" CssClass="Link" runat="server" Text="Show/Hide" onclick="btn3_click" />                               
            </td>
        </tr>
        <asp:Panel ID="pnl3" runat="server" Visible="true">
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody"><b>Inventory Module</b></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">&nbsp &nbsp Inventory Ingredient Log Report</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">&nbsp &nbsp Inventory Log Report</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">&nbsp &nbsp Inventory Ingredient Summary</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody"><b>CRM Module</b></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">included</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">&nbsp &nbsp Customer Items Report</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">&nbsp &nbsp Customer Order Report</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody"><b>Vendors & PO Module</b></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody"><b>Reservations Module</b></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody"><b>Schedules Tab Module</b></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody"><b>Catering Module</b></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">QSR/TSR</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody"><b>Delivery Management Module</b></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">QSR/TSR</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody"><b>Loyalty</b></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">$25</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">&nbsp &nbsp Reward Card Report</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">included</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody"><b>Gift Card</b></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">$25</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">&nbsp &nbsp Gift Card Report</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">included</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">&nbsp &nbsp Gift Card Transactions Report</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">included</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        </asp:Panel>
        <tr height=25>
            <td align="left" valign="middle" colspan=4>
                <span class="LabelsHeader"><strong>Reports</strong></span>
                <asp:LinkButton ID="btn4" CssClass="Link" runat="server" Text="Show/Hide" onclick="btn4_click" />                               
            </td>
        </tr>
        <asp:Panel ID="pnl4" runat="server" Visible="true">

        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody"><b>Sales Summary</b></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">included</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody"><b>Product Mix</b></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">included</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody"><b>Order History</b></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">included</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
        </tr>
        <tr ><td height="5px"></td></tr>

        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody"><b>Payment Summary</b></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">included</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
        </tr>
        <tr ><td height="5px"></td></tr>
         <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody"><b>Discounts and Voids</b></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">included</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody"><b>Pay-outs/Pay-ins</b></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">included</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody"><b>Tax</b></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">included</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody"><b>Tills</b></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">included</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody"><b>Access Point Meraki Cloud Hosting</b></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">29.00</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody"><b>Action Log</b></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">Included</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody"><b>Commissions Report</b></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">Included</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody"><b>Employee Profit Report</b></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">Included</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody"><b>House Account Report</b></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">Included</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody"><b>Invoices Report</b></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">Included</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody"><b>Labor Report</b></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">Included</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody"><b>Product Component Report</b></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">Included</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody"><b>Returns Report</b></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">Included</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody"><b>Serial Number Report</b></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">Included</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody"><b>Service Fee Report</b></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">Included</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody"><b>Speed of Service Report</b></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">Included</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody"><b>Operations Report</b></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">Included</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        </asp:Panel>
        <tr height=25>
            <td align="left" valign="middle" colspan=4>
                <span class="LabelsHeader"><strong>Support</strong></span>
                <asp:LinkButton ID="btn5" CssClass="Link" runat="server" Text="Show/Hide" onclick="btn5_click" />                               
            </td>
        </tr>
        <asp:Panel ID="pnl5" runat="server" Visible="true">
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">Full range of support</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
        </tr>
       
        </asp:Panel>

    </table><br />
    <center>
        <a class="Link" target="_blank" href="equipment.aspx#31"><strong>
                        Order Now!!!</strong></a>
    </center><br />
    
</asp:Content>
