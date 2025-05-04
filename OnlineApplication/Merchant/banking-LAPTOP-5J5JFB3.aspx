<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="banking.aspx.cs"
    Inherits="Merchant_banking" Title="Commerce Technologies - Online Application"
    Theme="TextBoxSkin" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentHolder" runat="Server">
    <asp:Panel ID="pnlMainPage" runat="server">
        <table border="0" cellpadding="2" cellspacing="0" Width="100%" class="DivGreen">
            <tr>
                <td colspan="3" align="center" class="DivHeader2" height="26px">
                    <b><span class="MenuHeader">Merchant Account Signup Information</span></b>
                </td>
            </tr>
            <tr>
            <td colspan="3" align="center" class="DivHeader">
                <table border="0" cellpadding="0" cellspacing="0" Width="100%">
                    <tr>
                        <td class="DivHeader" align="center" width="14%">
                            <a href="cardpct.aspx" class="MenuCss">Card Percentages</a>
                        </td>
                        <td class="DivHeader" align="center" width="14%">
                            <a href="companyinfo.aspx" class="MenuCss">Contact Info</a>
                        </td>
                        <td class="DivHeader" align="center" width="14%">
                            <a href="businessinfo.aspx" class="MenuCss">Business Info</a>
                        </td>
                        <td class="DivHeader" align="center" width="14%">
                            <a href="principalinfo.aspx" class="MenuCss">Principal Info</a>
                        </td>
                        <td class="DivHeader2" align="center" width="14%">
                            <span class="MenuHeader">Banking Info</span>
                        </td>
                         <td class="DivHeader" align="center" width="14%">
                           <a href="/Application/UploadDocuSignDocs.aspx" class="MenuCss">Upload Documents</a> 
                        </td>
                        <td class="DivHeader" align="center" width="14%">
                            <a href="/Application/notice.aspx" class="MenuCss">Sign application</a>
                        </td>
                        
                    </tr>
                </table>
            </td>
            </tr>
            <!--<tr>
                <td colspan="3" align="center">
                    <a href="cardpct.aspx" onmouseover="document.mcardpct.src='/Application/Images/oa_CardPercentages_mouseout.gif'"
                        onmouseout="document.mcardpct.src='/Application/Images/oa_CardPercentages_mouseover.gif'">
                        <img name="mcardpct" style="border: 0" alt="" src="/Application/Images/oa_CardPercentages_mouseover.gif" /></a>
                    <a href="companyinfo.aspx" onmouseover="document.mcontact.src='/Application/Images/oa_Contact_mouseout.gif'"
                        onmouseout="document.mcontact.src='/Application/Images/oa_Contact_mouseover.gif'">
                        <img name="mcontact" style="border: 0" alt="" src="/Application/Images/oa_Contact_mouseover.gif" /></a>
                    <a href="businessinfo.aspx" onmouseover="document.mbusiness.src='/Application/Images/oa_Business_mouseout.gif'"
                        onmouseout="document.mbusiness.src='/Application/Images/oa_Business_mouseover.gif'"><img
                        name="mbusiness" style="border: 0" alt="" src="/Application/Images/oa_Business_mouseover.gif" /></a>
                    <a href="principalinfo.aspx" onmouseover="document.mprincipal.src='/Application/Images/oa_Principal_mouseout.gif'"
                        onmouseout="document.mprincipal.src='/Application/Images/oa_Principal_mouseover.gif'"><img
                        name="mprincipal" style="border: 0" alt="" src="/Application/Images/oa_Principal_mouseover.gif" /></a>
                    <img name="mbanking" style="border: 0" alt="" src="/Application/Images/oa_Banking.gif" />
                </td>
            </tr>-->
            <tr>
                <td colspan="3" align="center">
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                                        <cc1:PopupControlExtender ID="pceOD" runat="server" PopupControlID="pnlOD"
            TargetControlID="imgOD" Position="Bottom" />
        <cc1:PopupControlExtender ID="pceEBT" runat="server" PopupControlID="pnlEBTPopup"
            TargetControlID="imgEBT" Position="Bottom" />
                    <cc1:PopupControlExtender ID="PopupControlExtender1" runat="server" PopupControlID="pnlDiscover"
                        TargetControlID="imgDiscover" Position="Bottom" />
                    <cc1:PopupControlExtender ID="PopupControlExtender2" runat="server" PopupControlID="pnlAmex"
                        TargetControlID="imgAmex" Position="Bottom" />
                    <cc1:PopupControlExtender ID="PopupControlExtender3" runat="server" PopupControlID="pnlJCB"
                        TargetControlID="imgJCB" Position="Bottom" />
                    <cc1:PopupControlExtender ID="PopupControlExtender4" runat="server" PopupControlID="pnlAvgTicket"
                        TargetControlID="imgAvgTicket" Position="Bottom" />
                    <cc1:PopupControlExtender ID="PopupControlExtender5" runat="server" PopupControlID="pnlMaxTicket"
                        TargetControlID="imgMaxTicket" Position="Bottom" />
                    <cc1:PopupControlExtender ID="PopupControlExtender6" runat="server" PopupControlID="pnlMonVol"
                        TargetControlID="imgMonVol" Position="Bottom" />
                </td>
            </tr>
            <tr>
                <td colspan="3" align="center">
                    <asp:Label ID="lblError" runat="server" BackColor="Red" Font-Size="Medium" ForeColor="White"
                        Visible="False"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="3" align="center" class="DivHeader">
                    <strong><span class="MenuHeader">BANK ACCOUNT INFORMATION</span></strong></td>
            </tr>
            <tr>
                <td colspan="3" align="right" height="25px">                    
                    <!--<b><asp:HyperLink ID="lnkEditInfo" CssClass="Link" runat="server" NavigateUrl="bankinginfo.aspx?EditBanking=True">Edit Bank Account Information</asp:HyperLink></b>-->
                    <asp:Label ID="lblEdit" runat="server" Text='Click on "Edit" to modify Bank Account Information.'></asp:Label>
                    <asp:Button ID="btnEditInfo" CssClass="Link" runat="server" PostBackUrl="bankinginfo.aspx?EditBanking=True" Text="Edit" /></td>
            </tr>
            <tr>
                <td align="right" width="50%" valign="bottom">
                    <asp:Label ID="lblBankNameHeader" runat="server" Text="Bank Name" Font-Bold="False"></asp:Label></td>
                <td align="left" colspan="2">
                    &nbsp;<asp:Label ID="lblBankName" Font-Bold="true" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblOtherBankHeader" runat="server" Text="If Other, specify" Font-Bold="False"></asp:Label></td>
                <td align="left" colspan="2">
                    <strong>&nbsp;<asp:Label ID="lblOtherBank" runat="server" Font-Bold="True"></asp:Label></strong></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblBankAddressHeader" runat="server" Text="Bank Address" Font-Bold="False"></asp:Label></td>
                <td align="left" colspan="2">
                    &nbsp;<asp:Label ID="lblBankAddress" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblCityHeader" runat="server" Text="City" Font-Bold="False"></asp:Label></td>
                <td colspan="2" align="left">
                    &nbsp;<asp:Label ID="lblCity" Font-Bold="true" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblZipCodeHeader" runat="server" Text="Zip Code" Font-Bold="False"></asp:Label></td>
                <td align="left" colspan="2">
                    <strong>&nbsp;<asp:Label ID="lblZipCode" runat="server" Font-Bold="True"></asp:Label></strong></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblStateHeader" runat="server" Text="State" Font-Bold="False"></asp:Label></td>
                <td align="left" style="width: 25%">
                    &nbsp;<asp:Label ID="lblState" runat="server" Font-Bold="True"></asp:Label></td>
                <td align="left" valign="middle" style="width: 35%">
                    <asp:Label ID="lblRegionHeader" runat="server" Text="Region" Font-Bold="False"></asp:Label>&nbsp;
                    <asp:Label Font-Bold="True" ID="lblRegion" runat="server"></asp:Label></td>
            </tr>
               <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblCountryHeader" runat="server" Text="Country" Font-Bold="False"></asp:Label></td>
                <td align="left" colspan="2">
                    <strong>&nbsp;<asp:Label ID="lblCountry" runat="server" Font-Bold="True"></asp:Label></strong></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblPhoneHeader" runat="server" Text="Bank Phone Number  " Font-Bold="False"></asp:Label></td>
                <td colspan="2" align="left">
                    &nbsp;<asp:Label Font-Bold="True" ID="lblPhone" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblNameOnCheckingHeader" runat="server" Text="Name Exactly As It Appears On Checking Account"
                        Font-Bold="False"></asp:Label>
                </td>
                <td colspan="2" align="left" valign="middle">
                    &nbsp;<asp:Label ID="lblNameOnChecking" Font-Bold="true" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblRoutingNumberHeader" runat="server" Text="Bank Routing Number"
                        Font-Bold="False"></asp:Label></td>
                <td colspan="2" align="left" valign="middle">
                    &nbsp;<asp:Label ID="lblBankRoutingNumber" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblAcctNum" runat="server" Text="Bank Account Number" Font-Bold="False"></asp:Label>
                </td>
                <td colspan="2" align="left" valign="middle">
                    &nbsp;<asp:Label ID="lblAcctNumber" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="3" align="center" class="DivHeader">
                    <strong><span class="MenuHeader">PROCESSING LIMITS</span></strong></td>
            </tr>
            <tr>
                <td align="right" colspan="3" valign="middle" height="25px">
                    <!--<b><asp:HyperLink ID="lnkEditProcessing" CssClass="Link" runat="server" NavigateUrl="bankinginfo.aspx?EditProcessing=True">Edit Processing Limits</asp:HyperLink></b>-->
                    <asp:Label ID="lblEditProcessing" runat="server" Text='Click on "Edit" to modify Processing Limits.'></asp:Label>
                    <asp:Button ID="btnEditProcessing" CssClass="Link" runat="server" PostBackUrl="bankinginfo.aspx?EditProcessing=True" Text="Edit" />
                </td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblAvgTicketHeader" runat="server" Text="Average Ticket $" Font-Bold="False"></asp:Label>
                </td>
                <td colspan="2" align="left" valign="middle">
                    &nbsp;<asp:Label ID="lblAvgTicket" runat="server" Font-Bold="True"></asp:Label>
                    <asp:Image ID="imgAvgTicket" runat="server" ImageUrl="/Application/Images/help.gif" /></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblMaxTicketHeader" runat="server" Text="Max Ticket $" Font-Bold="False"></asp:Label></td>
                <td colspan="2" align="left">
                    &nbsp;<asp:Label ID="lblMaxTicket" runat="server" Font-Bold="True"></asp:Label>
                    <asp:Image ID="imgMaxTicket" runat="server" ImageUrl="/Application/Images/help.gif" /></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblMonVol" runat="server" Text="Estimated Monthly Volume $" Font-Bold="False"></asp:Label></td>
                <td colspan="2" align="left" valign="middle">
                    &nbsp;<asp:Label Font-Bold="true" ID="lblMonthlyVol" runat="server"></asp:Label>
                    <asp:Image ID="imgMonVol" runat="server" ImageUrl="/Application/Images/help.gif" />
                    
                            <asp:Button ID="btnShowPopup" runat="server" style="display:none" />
                    <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowPopup" PopupControlID="pnlpopup">
</cc1:ModalPopupExtender>
<asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="100px" Width="400px" style="display:none">
<table width="100%" style="border:Solid 2px #007b79; width:100%; height:100%" cellpadding="0" cellspacing="0">
<tr >
<td style=" height:10%; color:White; font-weight:bold;padding:3px; font-size:larger; font-family:Arial" align="Left">Confirm Box</td>
<td style=" color:White; font-weight:bold;padding:3px; font-size:10pt" align="Right"> 
<a href = "" onclick = "closepopup()"><!--<img src="Images/Close.gif" style="border :0px" align="right" alt="close"/>-->Close</a>
</td>
</tr>
<tr>
<td colspan="2" align="left" style="padding:5px; font-size:larger; font-family:Arial;">
<asp:Label ID="lblUser" style="padding:5px; font-size:13px; font-family:Arial; font-weight:bold" runat="server"/>
</td>
</tr>
<tr>
<td colspan="2"></td>
</tr>
<tr>
<td>
</td>
<td align="right" style="padding-right:15px">
<asp:Button ID="btnPOS2013Yes" OnClick="btnPOS2013Yes_Click" runat="server" Text="Yes"/>
<asp:Button ID="btnPOS2013No" OnClick="btnPOS2013No_Click" runat="server" Text="No" />
</td>
</tr>
</table>
</asp:Panel>
                    </td>
            </tr>
            <tr valign="top">
                <td colspan="3" align="center" class="DivHeader">
                    <strong><span class="MenuHeader">OTHER PAYMENT OPTIONS</span></strong>
                </td>
            </tr>
            <tr>
                <td align="right" colspan="3" height="25px">
                    <!--<b><asp:HyperLink ID="HyperLink1" CssClass="Link" runat="server" NavigateUrl="bankinginfo.aspx?EditNBC=True">Edit Non Bankcard Information</asp:HyperLink></b>-->
                    <asp:Label ID="lblEditNBC" runat="server" Text='Click on "Edit" to modify Non Bankcard Information.'></asp:Label>
                    <asp:Button ID="btnEditNBC" CssClass="Link" runat="server" PostBackUrl="bankinginfo.aspx?EditNBC=True" Text="Edit" />
                </td>
            </tr>
            <tr valign="top">
                <td align="right" valign="bottom">
                    <asp:Label ID="lblDiscoverHeader" runat="server" Text="Discover Card" Font-Bold="False"></asp:Label>
                </td>
                <td align="left" colspan="2" valign="middle">
                    <b>&nbsp;<asp:Label ID="lblDiscover" runat="server"></asp:Label>
                        <asp:Image ID="imgDiscover" runat="server" ImageUrl="/Application/Images/help.gif" /></b></td>
            </tr>
            <tr valign="top">
                <td align="right" colspan="3">
                    <asp:Panel ID="pnlDiscoverNum" runat="server" Height="100%" Width="100%">
                        <table border="0" width="100%">
                            <tr>
                                <td align="right" valign="middle">
                                    <asp:Label ID="lblExistingDiscoverNum" runat="server" Text="Existing Number" Font-Bold="False"></asp:Label></td>
                                <td align="left" colspan="2">
                                    &nbsp;<asp:Label ID="lblDiscoverNum" runat="server" Font-Bold="True"></asp:Label></td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr valign="top">
                <td align="right" valign="bottom">
                    <asp:Label ID="lblAmexHeader" runat="server" Text="Do you want to accept Amex?" Font-Bold="False"></asp:Label>
                </td>
                <td align="left" colspan="2" valign="middle">
                    <b>&nbsp;<asp:Label ID="lblAmex" runat="server"></asp:Label>
                        <asp:Image ID="imgAmex" runat="server" ImageUrl="/Application/Images/help.gif" /></b>
                </td>
            </tr>
            <tr valign="top">
                <td align="right" colspan="3">
                    <asp:Panel ID="pnlAmexNum" runat="server" Height="100%" Width="100%">
                        <table width="100%" border="0">
                            <tr>
                                <td align="right" valign="middle">
                                    <asp:Label ID="lblAmexNumHeader" runat="server" Text="Existing Number" Font-Bold="False"></asp:Label></td>
                                <td align="left" colspan="2">
                                    &nbsp;<asp:Label ID="lblAmexNum" runat="server" Font-Bold="True"></asp:Label>&nbsp;</td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblJCBHeader" runat="server" Text="Do you want to accept JCB?" Font-Bold="False"></asp:Label></td>
                <td align="left" colspan="2" valign="middle">
                    <b>&nbsp;<asp:Label ID="lblJCB" runat="server" Font-Bold="True"></asp:Label>
                        <asp:Image ID="imgJCB" runat="server" ImageUrl="/Application/Images/help.gif" /></b></td>
            </tr>
            <tr valign="top">
                <td align="right" colspan="3">
                    <asp:Panel ID="pnlJCBNum" runat="server" Height="100%" Width="100%">
                        <table border="0" width="100%">
                            <tr>
                                <td align="right" valign="middle">
                                    <asp:Label ID="lblJCBNumHeader" runat="server" Text="Existing Number" Font-Bold="False"></asp:Label></td>
                                <td align="left" colspan="2">
                                    &nbsp;<asp:Label ID="lblJCBNum" runat="server" Font-Bold="True"></asp:Label></td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="pnlOnlineDebit" runat="server" Text="Do you want to accept Online Debit?" Font-Bold="False"></asp:Label></td>
                <td align="left" colspan="2" valign="middle">
                    <b>&nbsp;<asp:Label ID="lblOnlineDebit" runat="server" Font-Bold="True"></asp:Label>
                        <asp:Image ID="imgOD" runat="server" ImageUrl="/Application/Images/help.gif" ToolTip="Help"/></b></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="pnlEBT" runat="server" Text="Do you want to accept Electronic Benefits Transfer (EBT)?" Font-Bold="False"></asp:Label></td>
                <td align="left" colspan="2" valign="middle">
                    <b>&nbsp;<asp:Label ID="lblEBT" runat="server" Font-Bold="True"></asp:Label>
                        <asp:Image ID="imgEBT" runat="server" ImageUrl="/Application/Images/help.gif" ToolTip="Help"/></b></td>
            </tr>


             <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="pnlEBTNum" runat="server" Text="FNS/FBS #" Font-Bold="False"></asp:Label></td>
                <td align="left" colspan="2" valign="middle">
                    <b>&nbsp;<asp:Label ID="lblUSDANum" runat="server" Font-Bold="True"></asp:Label>
                        </b></td>
            </tr>


            <tr>
                <td colspan="3" align="center">
                    <b><asp:HyperLink CssClass="Link" Target="_blank" ID="lnkAgreement" runat="server" NavigateUrl="https://www.apps.commercetech.com/Processing Info/Placement Agr 12-21-07.doc"
                            TabIndex="21" Font-Bold="true" Font-Names="Arial">Agreement</asp:HyperLink></b><br />
                    <asp:Label ID="lblAgreement" runat="server" Text="Submit this form to signify that you have read, understood and agreed to the Terms and Conditions of the above agreement."></asp:Label>
                </td>
            </tr>
            <tr height="20px">
            </tr> 
            <tr>
                <td colspan="3" align="center" height="25px">
                    <asp:Label ID="lblBottom" runat="server" Text='Click on "Submit" to continue your application.'></asp:Label><br/>
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" TabIndex="22" /></td>
            </tr>
            <tr>
                <td colspan="3" align="center">
                <asp:Panel Style="display: none; z-index: 1; text-align: center;" ID="pnlOD" runat="server" Width="300px" CssClass="DivHelp">
            This service is for card present accounts and requries a pin pad for accepting pin debit transactions.
            </asp:Panel>
            <asp:Panel Style="display: none; z-index: 1; text-align: center;" ID="pnlEBTPopup" runat="server" Width="300px" CssClass="DivHelp">
            This service is for retail business wishing to accept Electronic Benefits Transfer, better known as green stamps.
            </asp:Panel>
                    <asp:Panel ID="pnlDiscover" runat="server" Style="display: none; z-index: 1;" Width="250px" CssClass="DivHelp">
                        Discover Acceptance is automatically included with merchant accounts for all US Merchants. International Merchants: Select the option Yes - Existing if 
                        you have an existing Discover number. Enter the existing number in the following textbox.</asp:Panel>
                    <asp:Panel ID="pnlAmex" runat="server" Style="display: none; z-index: 1;" Width="250px" CssClass="DivHelp">
                        Select the option Yes - Existing if you have an existing Amex number. Enter the existing number in the following textbox.</asp:Panel>
                    <asp:Panel ID="pnlJCB" runat="server" Style="display: none; z-index: 1;" Width="250px" CssClass="DivHelp">
                        JCB Acceptance is automatically included with merchant accounts for all US Merchants. International Merchants: Select the option Yes - Existing if you 
                        have an existing JCB number. Enter the existing number in the following textbox.</asp:Panel>
                    <asp:Panel ID="pnlAvgTicket" runat="server" Style="display: none; z-index: 1;" Width="250px" CssClass="DivHelp">
                        Average Ticket: This is the average amount charged per sale.</asp:Panel>
                    <asp:Panel ID="pnlMaxTicket" runat="server" Style="display: none; z-index: 1;" Width="250px" CssClass="DivHelp">
                        Maximum Ticket: This is the maximum amount charged on sale. Maximum transaction rate must be greater than your average transaction.</asp:Panel>
                    <asp:Panel ID="pnlMonVol" runat="server" Style="display: none; z-index: 1;" Width="250px" CssClass="DivHelp">
                        Estimated Monthly Volume: Your average transaction is the average amount of one sale.Your monthly volume is your average transaction times the number of sales in a month.Your monthly volume should almost always be more than your average transaction value.</asp:Panel>
                </td>
            </tr>           
        </table>
    </asp:Panel>
        <script language="javascript" type="text/javascript">
function closepopup() {
$find('ModalPopupExtender1').hide();
}

</script>
</asp:Content>
