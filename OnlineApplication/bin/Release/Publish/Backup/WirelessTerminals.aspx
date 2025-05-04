<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="WirelessTerminals.aspx.cs" Inherits="WirelessTerminals" Title="Credit Card Processing, Merchant Services, Payment Processing"
    Theme="Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <br />
    <table align="right" width="97%" cellspacing="0" cellpadding="0" border="0">
        <tr>
            <td align="center">
                <span class="LabelsHeaderLarge"><strong>Wireless Terminals</strong></span><br /><br />
            </td>
        </tr>
        <tr>
            <td align="left">
                <span class="LabelsBody">Some businesses prefer using wireless terminals because they are always moving from location to location and need 
                something durable for rugged operation. Also, they would rather have one device that runs payments all the time instead of keeping track 
                of multiple devices. Typically these terminals work best for merchants at farmers/flee markets, tradeshows, fairs, kiosks, etc.<br /><br />
                Although these terminals require a separate wireless plan and have a larger equipment investment, they have the following 
                benefits - <br /><br /></span>
            </td>
        </tr>        
        <tr>
            <td align="center">
                <div align="center" style="width: 70%;" class="DivHeader">
                    <span class="LabelsWhite"><b>Benefits of using Wireless Terminals over Mobile Phones</b></span><br />
                    <div align="left" style="width: 100%;" class="DivWhiteSmoke">
                        <ul class="Bullet">
                            <li style="padding-top:5px" class="LabelsBody">Ability to have a printed receipt</li>
                            <li class="LabelsBody">Capable of handling multiple services on the same device such as online debit, 
                            check services, gift cards, etc.</li>
                            <li class="LabelsBody">All transactions are run on a single device eliminating the need to set up 
                            each user on a different device</li>
                            <li class="LabelsBody">We program the terminal and send it to you so you don’t have to spend any time 
                            setting up or configuring the terminal</li>                            
                        </ul>
                    </div>
                </div>
            </td>
        </tr>
        <tr height=15></tr>
        <tr>
            <td align="left">
                <span class="LabelsBody"><a class="Link" href="equipment.aspx#13" title="wireless terminals">Click here</a> to view a list of wireless terminals we carry.</span>
            </td>
        </tr>        
        <tr>
            <td align="center" valign="bottom">
            <br />
            <asp:HyperLink CssClass="Link" Target="_blank" ID="lnkOnlineApplication" runat="server"><strong>
                        Apply Online Now!!!</strong></asp:HyperLink>
            </td>
        </tr> 
        <tr height=20></tr> 
    </table>
</asp:Content>
