<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Laptop.aspx.cs" Inherits="Laptop" Title="Credit Card Processing, Merchant Services, Payment Processing"
    Theme="Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <br />
    <table align="right" width="97%" cellspacing="0" cellpadding="0" border="0">
        <tr>
            <td colspan="2" align="center">
                <span class="LabelsHeaderLarge"><strong>Laptop Payments</strong></span><br /><br />
            </td>
        </tr>
        <tr>
            <td align="left">
                <span class="LabelsBody">Wireless functionality is a growing need most businesses. With a laptop 
                that can connect to the internet (through a wireless card or Wi-Fi network), you can now process transactions 
                wherever you go. The perfect solution for kiosks, outdoor booths, traveling sales forces, or anywhere else 
                you need to process transactions without wires. Credit cards are processed by using a virtual terminal 
                (web-based transaction), accounting or POS software. Card Readers can also be used to lower discount rates on swiped 
                transactions. <a href="equipment.aspx#7" class="Link" 
                title="Card Readers link">Click here</a> to see a list of card readers we carry.</span><br /><br />
            </td>
            <td align="center" width="25%">
                <img border="0" src="Images/Laptop.gif" alt="Mobile Payments" width="150px" />
            </td>
        </tr>
        <tr>
            <td colspan="2" align="left">
                <span class="LabelsHeader"><b>Virtual Terminals</b></span>: 
                <span class="LabelsBody">Virtual terminals are web-based, meaning that all transactions are run by using a web browser 
                such as Internet Explorer, Mozilla Firefox, Safari, Chrome, etc. that connects directly to the payment gateway's server. These 
                transactions occur in real-time and are completely secure. Examples of some of our payment gateways that have the capability 
                of running virtual terminal transactions with card readers are - </span>
                <ul class="Bullet">
                    <li class="LabelsBody">Authorize.net VPOS</li>
                    <li class="LabelsBody">Plug'n Pay</li>
                    <li class="LabelsBody">QuickBooks Merchant Services</li>
                    <li class="LabelsBody">Sage Gateway</li>
                </ul>
            </td>
        </tr>
        <tr height="20px"></tr>
        <tr>
            <td colspan="2" align="left">
                <span class="LabelsBody">Click here to learn more about our <a class="Link" href="POSSolutions.aspx">POS Software</a>. </span>
            </td>
        </tr>
        <tr height="20px"></tr>
        <tr>
            <td colspan="2" align="left">
                <span class="LabelsBody">Click and learn more about our <a class="Link" href="AccountingSoftware.aspx">Accounting Software</a>. </span>
            </td>
        </tr>
 
        <tr>
            <td colspan="2" align="center" valign="bottom">
            <br />
            <asp:HyperLink CssClass="Link" Target="_blank" ID="lnkOnlineApplication" runat="server"><strong>
                        Apply Online Now!!!</strong></asp:HyperLink>
            </td>
        </tr> 
    </table>
</asp:Content>
