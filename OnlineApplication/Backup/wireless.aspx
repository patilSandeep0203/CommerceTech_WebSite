<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="wireless.aspx.cs" Inherits="wireless" Title="Credit Card Processing, Merchant Services, Payment Processing"
    Theme="Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <br />
    <table align="right" width="97%" cellspacing="0" cellpadding="0" border="0">
        <tr>
            <td align="center">
                <span class="LabelsHeaderLarge"><strong>Wireless Services</strong></span><br /><br />
            </td>
        </tr>
        <tr>
            <td align="left">
                <span class="LabelsBody">With the ever growing need to process real-time and secure payments most 
                merchants are turning to a wireless solution. Wireless Services enable merchants to accept credit 
                card payments on-the-go. A web-enabled device such as a cell phone, PDA, tablet computer, laptop, 
                wireless terminal or Wi-Fi terminal is all that is needed. <br /><br />
                Whether you are a merchant that provides delivery services to customers or a merchant that needs that 
                extra option to accept payments while on-site at a customer location, wireless services are a perfect fit. 
                With wireless services you can realize the potential of your sales initiatives by mobilizing and using 
                tools you already own. It's a mobile world, it's an electronic world, it's time your electronic payments 
                were as mobile as your sales force.<br /><br /></span>
            </td>
        </tr>
        <tr>
            <td align="left">
                <span class="LabelsHeader"><strong>Below are some of the businesses that can effeciently use wireless services?</strong></span><br />
                <span class="LabelsBody">If you are a merchant that does some of your business remotely or on-site, wireless services 
                are for you. Below are some examples of merchants who are successfully using wireless services in their day-to-day transactions -</span>
                <table>
                    <tr>
                        <td width="50%">
                            <ul class="Bullet">
                                <li class="LabelsBody">Artists / Craftpersons</li>
                                <li class="LabelsBody">Carpenters</li>
                                <li class="LabelsBody">Charitable Donations</li>
                                <li class="LabelsBody">Distributed Sales Forces</li>
                                <li class="LabelsBody">Electricians</li>
                                <li class="LabelsBody">Fundraisers</li>
                                <li class="LabelsBody">General contractors</li>
                            </ul>
                        </td>
                        <td>
                            <ul class="Bullet">
                                <li class="LabelsBody">HVAC / AC Sales & Service</li>
                                <li class="LabelsBody">Landscapers</li>
                                <li class="LabelsBody">Mobile vendors: trade shows, fairs, kiosks</li>
                                <li class="LabelsBody">Plumbers</li>
                                <li class="LabelsBody">Private Practices / Small Business</li>
                                <li class="LabelsBody">Roofing / Siding Contractors</li>
                                <li class="LabelsBody">Taxi / Limo / Airport Transportation</li>
                            </ul>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr><td height="15px"></td></tr>
        <tr>
            <td colspan="2" align="center">
                <div align="center" style="width: 70%;" class="DivHeader">
                    <span class="LabelsWhite"><b>Benefits of using wireless services</b></span><br />
                    <div align="left" style="width: 100%;" class="DivWhiteSmoke">
                        <ul class="Bullet">
                            <li style="padding-top:5px" class="LabelsBody">Flexibility to accept payments anywhere at anytime</li>
                            <li class="LabelsBody">Save time by completing the transaction on the spot without having to go back to your 
                                office and manually entering payments/transactions</li>
                            <li class="LabelsBody">If you use a card reader, you may be eligible to receive swiped rates</li> 
                            <li class="LabelsBody">Run payments securely so customer data is always encrypted and never at risk - Fully PCI DSS certified.</li>
                            <li class="LabelsBody">Tax and tip calculator.</li>
                            <li class="LabelsBody">Manage multiple merchant accounts.</li>    
                            <li class="LabelsBody">Manage multiple users.</li>                          
                        </ul>
                    </div>
                </div>
            </td>
        </tr>
        <tr height=15></tr>
        <tr>
            <td>
                <table align="center" width="100%" cellspacing=2 cellpadding=0 border=0 class="DivWhiteSmoke;">
                    <tr>
                        <td colspan="2" align="left"><span class="LabelsBody">Click on each type of Wireless Service to determine the best solution for your business: <br /><br /></span></td>
                    </tr>
                    <tr>
                        <td>
                            <a class="Link" href="MobilePayments.aspx" title="Mobile Payments"><img border="0" src="Images/Mobile-Payments.jpg" alt="Mobile Payments" height="80px" /></a>
                        </td>
                        <td align="left">
                            <a class="Link" href="MobilePayments.aspx" title="Mobile Payemnts">Mobile Payments</a>
                        </td>
                        <td>
                            <a class="Link" href="Laptop.aspx" title="Laptop Payments"><img border="0" src="Images/Laptop.gif" alt="Laptop Payments" width="120px" /></a>
                        </td>
                        <td align="left">
                            <a class="Link" href="Laptop.aspx" title="Laptop Payments">Laptop Payments</a>
                        </td>
                    </tr>
                    <tr>
                         <td>
                            <a class="Link" href="fieldServiceMgmtES.aspx" title="Field Service Management"><img border="0" src="http://quickbooks.corrigo.com/wp-content/uploads/2017/08/logo.png" alt="Field Service Management" height="80px" /></a>
                        </td>
                        <td align="left">
                             <a class="Link" href="fieldServiceMgmtES.aspx" title="Field Service Management">Field Service Management</a>
                        </td>
                        <td>
                            <a class="Link" href="WirelessTerminals.aspx" title="Wireless Terminals"><img border="0" src="Images/ProductImages/nurit8020.jpg" alt="Wireless Terminals" height="80px" /></a>
                        </td>
                        <td align="left">
                            <a class="Link" href="WirelessTerminals.aspx" title="Wireless Terminals">Wireless Terminals</a>
                        </td>
                    </tr>                    
                </table>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="4" valign="bottom">
            <br />
            <asp:HyperLink CssClass="Link" Target="_blank" ID="lnkOnlineApplication" runat="server"><strong>
                        Apply Online Now!!!</strong></asp:HyperLink>
            </td>
        </tr> 
        <tr height=20></tr> 
    </table>
</asp:Content>
