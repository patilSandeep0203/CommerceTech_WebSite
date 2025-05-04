<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="business.aspx.cs"
    Inherits="Merchant_business" Title="Commerce Technologies - Online Application"
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
                        <td class="DivHeader2" align="center" width="14%">
	                        <span class="MenuHeader">Business Info</span>
                        </td>
                        <td class="DivHeader" align="center" width="14%">
                            <a href="principalinfo.aspx" class="MenuCss">Principal Info</a>
                        </td>
                        <td class="DivHeader" align="center" width="14%">
                            <a href="bankinginfo.aspx" class="MenuCss">Banking Info</a>
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
                    <img name="mbusiness" style="border: 0" alt="" src="/Application/Images/oa_Business.gif" />
                    <a href="principalinfo.aspx" onmouseover="document.mprincipal.src='/Application/Images/oa_Principal_mouseout.gif'"
                        onmouseout="document.mprincipal.src='/Application/Images/oa_Principal_mouseover.gif'">
                        <img name="mprincipal" style="border: 0" alt="" src="/Application/Images/oa_Principal_mouseover.gif" /></a>
                    <a href="bankinginfo.aspx" onmouseover="document.mbanking.src='/Application/Images/oa_Banking_mouseout.gif'"
                        onmouseout="document.mbanking.src='/Application/Images/oa_Banking_mouseover.gif'">
                        <img name="mbanking" style="border: 0" alt="" src="/Application/Images/oa_Banking_mouseover.gif" /></a>
                </td>
            </tr>-->
            <tr>
                <td align="center" colspan="3">
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <cc1:PopupControlExtender ID="PopupControlExtender1" runat="server" PopupControlID="pnlOwnership"
                        TargetControlID="imgOwnership" Position="Bottom" />
                    <cc1:PopupControlExtender ID="PopupControlExtender2" runat="server" PopupControlID="pnlTaxID"
                        TargetControlID="imgTaxID" Position="Bottom" />
                    <cc1:PopupControlExtender ID="PopupControlExtender3" runat="server" PopupControlID="pnlProduct"
                        TargetControlID="imgProducts" Position="Bottom" />
                    <cc1:PopupControlExtender ID="PopupControlExtender4" runat="server" PopupControlID="pnlCTMF"
                        TargetControlID="imgCTMF" Position="Bottom" />
                </td>
            </tr>
            <tr>
                <td align="center" colspan="3">                    
                    <asp:Label ID="lblError" runat="server" BackColor="Red" Font-Size="Medium" ForeColor="White"
                        Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right" colspan="3" height="25px">
                    <asp:Label ID="lblEdit" runat="server" Text='Click on "Edit" to modify Business Information.'></asp:Label>
                    <!--<asp:HyperLink ID="lnkEditInfo" CssClass="Link" runat="server" NavigateUrl="businessinfo.aspx?EditBusiness=True">Edit Business Information</asp:HyperLink></td>-->
                    <asp:Button ID="btnEditInfo" CssClass="Link" runat="server" PostBackUrl="businessinfo.aspx?EditBusiness=True" Text="Edit" />
            </tr>
            <tr>
                <td colspan="3" align="center" class="DivHeader">
                    <b><span class="MenuHeader">BILLING INFORMATION</span></b></td>
            </tr>
            <tr>
                <td align="right" valign="bottom" width="50%">
                    <asp:Label ID="lblAddressHeader" runat="server" Text="Address" Font-Bold="False"></asp:Label></td>
                <td colspan="2" style="text-align: left">
                    &nbsp;<asp:Label ID="lblAddress" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                </td>
                <td colspan="2" style="text-align: left">
                    &nbsp;<asp:Label ID="lblAddress2" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lclCityHeader" runat="server" Text="City" Font-Bold="False"></asp:Label></td>
                <td colspan="2" style="text-align: left">
                    &nbsp;<asp:Label ID="lblCity" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblStateHeader" runat="server" Text="State" Font-Bold="False"></asp:Label></td>
                <td style="text-align: left">
                    &nbsp;<asp:Label ID="lblState" runat="server" Font-Bold="True"></asp:Label></td>
                <td style="text-align: left">
                    <asp:Label ID="lblRegionHeader" runat="server" Text="Region"></asp:Label>
                    <asp:Label ID="lblRegion" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblZipCodeHeader" runat="server" Text="Zip Code" Font-Bold="False"></asp:Label></td>
                <td colspan="2" style="text-align: left">
                    &nbsp;<asp:Label ID="lblZipCode" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblCountryHeader" runat="server" Text="Country" Font-Bold="False"></asp:Label></td>
                <td colspan="2" style="text-align: left">
                    &nbsp;<asp:Label ID="lblCountry" runat="server" Font-Bold="True"></asp:Label>                    <asp:Button ID="btnShowPopup" runat="server" style="display:none" />
                    <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowPopup" PopupControlID="pnlpopup"
>
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
</asp:Panel></td>
            </tr>
            <tr>
                <td colspan="3" align="center" class="DivHeader">
                    <b><span class="MenuHeader">BUSINESS INFORMATION</span></b></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblOwnershipHeader" runat="server" Text="Ownership/Legal Status" Font-Bold="False"></asp:Label></td>
                <td colspan="2" style="text-align: left">
                    &nbsp;<asp:Label ID="lblOwnership" runat="server" Font-Bold="True"></asp:Label>
                    <asp:Image ID="imgOwnership" runat="server" ImageUrl="/Application/Images/help.gif" />
                </td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblTaxIDHeader" runat="server" Text="Federal Tax ID" Font-Bold="False"></asp:Label></td>
                <td colspan="2" style="text-align: left">
                    &nbsp;<asp:Label ID="lblTaxID" runat="server" Font-Bold="True"></asp:Label>
                    <asp:Image ID="imgTaxID" runat="server" ImageUrl="/Application/Images/help.gif" /></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblTIBL" runat="server" Text="Time in Business" Font-Bold="False"></asp:Label>
                </td>
                <td colspan="2" style="text-align: left">
                    &nbsp;<asp:Label ID="lblYIB" runat="server" Font-Bold="True"></asp:Label>
                    <asp:Label ID="lblYearsHeader" runat="server" Text="Years"></asp:Label>&nbsp;
                    <asp:Label ID="lblAnd" runat="server" Text="And"></asp:Label>
                    <asp:Label ID="lblMIB" runat="server" Font-Bold="True"></asp:Label>
                    <asp:Label ID="lblMonthsHeader" runat="server" Text="Months"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblNumberLocsHeader" runat="server" Text="Number of Locations" Font-Bold="False"></asp:Label></td>
                <td colspan="2" style="text-align: left">
                    &nbsp;<asp:Label ID="lblNumLocs" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblProductsSoldHeader" runat="server" Text="Products/Services Sold (Be Descriptive. 250 max Characters)"
                        Font-Bold="False"></asp:Label></td>
                <td colspan="2" style="text-align: left">
                    &nbsp;<asp:Label ID="lblProductsSold" runat="server" Font-Bold="True"></asp:Label>
                    <asp:Image ID="imgProducts" runat="server" ImageUrl="/Application/Images/help.gif" />
                </td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblApproxDaysHeader" runat="server" Text="Approx. Number of Days Until Product/Service is delivered"
                        Font-Bold="False"></asp:Label></td>
                <td colspan="2" style="text-align: left">
                    &nbsp;<asp:Label ID="lblApproxDays" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblAddlCommentsHeader" runat="server" Text="Additional Delivery Comments (250 max Characters)"
                        Font-Bold="False"></asp:Label></td>
                <td colspan="2" style="text-align: left">
                    &nbsp;<asp:Label ID="lblAddlComments" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblRefundHeader" runat="server" Text="Refund Policy (Choose one)"
                        Font-Bold="False"></asp:Label>
                </td>
                <td colspan="2" style="text-align: left">
                    &nbsp;<asp:Label ID="lblRefund" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblOtherRefundHeader" runat="server" Text="If Other, Specify" Font-Bold="False"></asp:Label></td>
                <td colspan="2" style="text-align: left">
                    &nbsp;<asp:Label ID="lblOtherRefund" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblBankruptcyHeader" runat="server" Text="Have you filed bankruptcy before?"
                        Font-Bold="False"></asp:Label></td>
                <td colspan="2" style="text-align: left">
                    &nbsp;<asp:Label ID="lblBankruptcy" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblPrevProcessedHeader" runat="server" Text="Have you processed credit cards before?"
                        Font-Bold="False"></asp:Label></td>
                <td colspan="2" style="text-align: left">
                    &nbsp;<asp:Label ID="lblPrevProcessed" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td align="left" colspan="3" valign="top">
                    <asp:Panel ID="pnlPrevProcessed" runat="server" Height="100%" Width="100%" HorizontalAlign="Left"
                        Visible="False">
                        <table border="0" width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td Width="50%" align="right" valign="bottom">
                                    <asp:Label ID="lblPrevProcessorHeader" runat="server" Text="If Yes, what was the processors name?"
                                        Font-Bold="False"></asp:Label></td>
                                <td colspan="2" align="left">
                                    &nbsp;<asp:Label ID="lblPrevProcessor" runat="server" Font-Bold="True"></asp:Label></td>
                            </tr>
                            <tr>
                                <td align="right" valign="bottom">
                                    <asp:Label ID="lblOtherProcessorHeader" runat="server" Text="If Other, specify" Font-Bold="False"></asp:Label></td>
                                <td colspan="2" align="left">
                                    &nbsp;<asp:Label ID="lblOtherProcessor" runat="server" Font-Bold="True"></asp:Label></td>
                            </tr>
                            <tr>
                                <td align="right" valign="bottom">
                                    <asp:Label ID="lblPrevMerchantNoHeader" runat="server" Text="What was the previous Merchant Account Number?"
                                        Font-Bold="False"></asp:Label></td>
                                <td colspan="2" align="left">
                                    &nbsp;<asp:Label ID="lblPrevMerchantAcctNo" runat="server" Font-Bold="True"></asp:Label></td>
                            </tr>
                            <tr>
                                <td valign="bottom" align="right">
                                    <asp:Label ID="lblReasonLeavingHeader" runat="server" Text="What was the reason for leaving the previous processor? "
                                        Font-Bold="False"></asp:Label></td>
                                <td colspan="2" align="left">
                                    &nbsp;<asp:Label ID="lblReasonForLeaving" runat="server" Font-Bold="True"></asp:Label></td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <p style="margin-top: 0; margin-bottom: 0; text-align: right;">
                        <asp:Label ID="lblCTMFHeader" runat="server" Text="Have you been terminated by a processor before  (ie, placed on the Combined Terminated Merchant File CTMF Match)?"
                            Font-Bold="False"></asp:Label></p>
                </td>
                <td colspan="2" style="text-align: left;">
                    &nbsp;<asp:Label ID="lblCTMF" runat="server" Font-Bold="True"></asp:Label>
                    <asp:Image ID="imgCTMF" runat="server" ImageUrl="/Application/Images/help.gif" />
                </td>
            </tr>
            <tr height="20px">
            </tr>            
            <tr>
                <td colspan="3" align="center" height="25px">
                    <asp:Label ID="lblBottom" runat="server" Text='Click on "Submit" to continue your application.'></asp:Label><br/>
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" TabIndex="26" /></td>
            </tr>
            <tr>
                <td colspan="3" align="center">
                    <asp:Panel ID="pnlOwnership" runat="server" Style="display: none; z-index: 1;" Width="250px" CssClass="DivHelp">
                        Select how you have registered your business i.e., legal/medical, sole proprietorship, partnership, corporation, LLC, etc. Select Others if none of the options in the list apply to you.</asp:Panel>
                    <asp:Panel ID="pnlTaxID" runat="server" Style="display: none; z-index: 1;" Width="250px" CssClass="DivHelp">
                        9-digit number. This is also known as a tax payer ID. In case of sole proprietorship, this will be your Social Security Number. (No Dashes). This number exactly match the registered Legal name with the IRS for US entities.</asp:Panel>
                    <asp:Panel ID="pnlProduct" runat="server" Style="display: none; z-index: 1;" Width="250px" CssClass="DivHelp">
                        Products/Services sold: A brief description of the products/services sold by your company. Please limit your description to 250 characters.</asp:Panel>
                    <asp:Panel ID="pnlCTMF" runat="server" Style="display: none; z-index: 1;" Width="250px" CssClass="DivHelp">
                        Your former merchant service provider will put you on this list if you have done anything illegal or have had excessive chargebacks that have not been paid.</asp:Panel>
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
