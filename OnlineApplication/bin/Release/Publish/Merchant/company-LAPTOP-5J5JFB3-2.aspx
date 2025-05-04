<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="company.aspx.cs"
    Inherits="Merchant_company" Title="Commerce Technologies - Online Application"
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
                        <td class="DivHeader" align="center" width="16%">
                            <a href="cardpct.aspx" class="MenuCss">Card Percentages</a>
                        </td>
                        <td class="DivHeader2" align="center" width="16%">
	                        <span class="MenuHeader">Contact Info</span>
                        </td>
                        <td class="DivHeader" align="center" width="16%">
                            <a href="businessinfo.aspx" class="MenuCss">Business Info</a>
                        </td>
                        <td class="DivHeader" align="center" width="16%">
                            <a href="principalinfo.aspx" class="MenuCss">Principal Info</a>
                        </td>
                        <td class="DivHeader" align="center" width="16%">
                            <a href="bankinginfo.aspx" class="MenuCss">Banking Info</a>
                        </td>
                         <td class="DivHeader" align="center" width="16%">
                            <a href="/Application/notice.aspx" class="MenuCss">Sign application</a>
                        </td>
                    </tr>
                </table>
            </td>
            </tr>
            <!--<tr>
                <td colspan="3" align="left">
                    <a href="cardpct.aspx" onmouseover="document.mcardpct.src='/Application/Images/oa_CardPercentages_mouseout.gif'"
                        onmouseout="document.mcardpct.src='/Application/Images/oa_CardPercentages_mouseover.gif'">
                        <img name="mcardpct" style="border: 0" alt="" src="/Application/Images/oa_CardPercentages_mouseover.gif" /></a>
                    <img style="border: 0" alt="" src="/Application/Images/oa_Contact.gif" />
                    <a href="businessinfo.aspx" onmouseover="document.mbusiness.src='/Application/Images/oa_Business_mouseout.gif'"
                        onmouseout="document.mbusiness.src='/Application/Images/oa_Business_mouseover.gif'">
                        <img name="mbusiness" style="border: 0" alt="" src="/Application/Images/oa_Business_mouseover.gif" /></a>
                    <a href="principalinfo.aspx" onmouseover="document.mprincipal.src='/Application/Images/oa_Principal_mouseout.gif'"
                        onmouseout="document.mprincipal.src='/Application/Images/oa_Principal_mouseover.gif'">
                        <img name="mprincipal" style="border: 0" alt="" src="/Application/Images/oa_Principal_mouseover.gif" /></a>
                    <a href="bankinginfo.aspx" onmouseover="document.mbanking.src='/Application/Images/oa_Banking_mouseout.gif'"
                        onmouseout="document.mbanking.src='/Application/Images/oa_Banking_mouseover.gif'">
                        <img name="mbanking" style="border: 0" alt="" src="/Application/Images/oa_Banking_mouseover.gif" /></a></td>
            </tr>-->
            <tr>
                <td align="center" colspan="3">
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <cc1:PopupControlExtender ID="PopupControlExtender1" runat="server" PopupControlID="pnlCompany"
                        TargetControlID="imgCompany" Position="Bottom" />
                    <cc1:PopupControlExtender ID="PopupControlExtender2" runat="server" PopupControlID="pnlDBA"
                        TargetControlID="imgDba" Position="Bottom" />
                    <cc1:PopupControlExtender ID="PopupControlExtender3" runat="server" PopupControlID="pnlAddress"
                        TargetControlID="imgAddress" Position="Bottom" />
                    <cc1:PopupControlExtender ID="PopupControlExtender4" runat="server" PopupControlID="pnlCustServ"
                        TargetControlID="imgCustServPhone" Position="Bottom" />
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
                    <asp:Label ID="lblEdit" runat="server" Text='Click on "Edit" to modify Contact Information.'></asp:Label>
                    <!--<asp:HyperLink ID="lnkEditInfo" CssClass="Link" runat="server" Font-Bold="True" NavigateUrl="companyinfo.aspx?EditCompany=True">Edit Contact Information</asp:HyperLink>-->
                    <asp:Button ID="btnEditInfo" CssClass="Link" runat="server" PostBackUrl="companyinfo.aspx?EditCompany=True" Text="Edit" />
                </td>
            </tr>
            <tr height="10px">
            </tr>
            <tr>
                <td align="right" style="width: 50%" valign="bottom">
                    <asp:Label ID="lblCompanyNameHeader" runat="server" Text="Company (Legal Name)" Font-Bold="False"></asp:Label></td>
                <td colspan="2" align="left">
                    &nbsp;<asp:Label ID="lblCompanyName" runat="server" Font-Bold="True"></asp:Label>
                    <asp:Image ID="imgCompany" runat="server" ImageUrl="/Application/Images/help.gif" /></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblDBAHeader" runat="server" Text="DBA (Doing Business As)" Font-Bold="False"></asp:Label>
                </td>
                <td colspan="2" align="left">
                    &nbsp;<asp:Label ID="lblDBA" runat="server" Font-Bold="True"></asp:Label>
                    <asp:Image ID="imgDba" runat="server" ImageUrl="/Application/Images/help.gif" /></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblAddressHeader" runat="server" Text="Physical Business Address"
                        Font-Bold="False"></asp:Label>
                </td>
                <td colspan="2" align="left">
                    &nbsp;<asp:Label ID="lblAddress" runat="server" Font-Bold="True"></asp:Label>
                    <asp:Image ID="imgAddress" runat="server" ImageUrl="/Application/Images/help.gif" /></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                </td>
                <td colspan="2" align="left">
                    &nbsp;<asp:Label ID="lblAddress2" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblCityHeader" runat="server" Text="City" Font-Bold="False"></asp:Label>
                </td>
                <td colspan="2" align="left">
                    &nbsp;<asp:Label ID="lblCity" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblStateHeader" runat="server" Text="State" Font-Bold="False"></asp:Label>
                </td>
                <td align="left">
                    &nbsp;<asp:Label ID="lblState" runat="server" Font-Bold="True"></asp:Label></td>
                <td align="left">
                    <asp:Label ID="lblRegionHeader" runat="server" Text="Region" Font-Bold="False"></asp:Label>&nbsp;
                    <asp:Label ID="lblRegion" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblZipCodeHeader" runat="server" Text="Zip Code" Font-Bold="False"></asp:Label>
                </td>
                <td colspan="2" align="left">
                    &nbsp;<asp:Label ID="lblZip" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblCountryHeader" runat="server" Text="Country" Font-Bold="False"></asp:Label>
                </td>
                <td colspan="2" align="left">
                    &nbsp;<asp:Label ID="lblCountry" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td align="right"  valign="bottom">
                    <asp:Label ID="lblTBLHeader" runat="server" Text="Time at Business Location" Font-Bold="False"></asp:Label>
                </td>
                <td colspan="2" align="left">
                    &nbsp;<asp:Label ID="lblTIBLYears" runat="server" Font-Bold="True"></asp:Label>
                    <asp:Label ID="lblTBLYears" runat="server" Text="Years And"></asp:Label>
                    <asp:Label ID="lblTIBLMonths" runat="server" Font-Bold="True"></asp:Label>
                    <asp:Label ID="lblTBLMonths" runat="server" Text="Months"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblBusinessHoursHeader" runat="server" Text="Business Hours" Font-Bold="False"></asp:Label>
                </td>
                <td colspan="2" align="left">
                    &nbsp;<asp:Label ID="lblBusinessHours" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblPhoneHeader" runat="server" Text="Business Phone" Font-Bold="False"></asp:Label>
                </td>
                <td colspan="2" align="left">
                    &nbsp;<asp:Label ID="lblPhone" runat="server" Font-Bold="True"></asp:Label>&nbsp;
                    <asp:Label ID="lblExtHeader" runat="server" Font-Bold="False" Text="Ext"></asp:Label>&nbsp;<asp:Label
                        ID="lblExt" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblFaxHeader" runat="server" Text="Fax" Font-Bold="False"></asp:Label>
                </td>
                <td colspan="2" align="left">
                    &nbsp;<asp:Label ID="lblFax" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblCustPhoneHeader" runat="server" Text="Customer Service Phone" Font-Bold="False"></asp:Label>
                </td>
                <td colspan="2" align="left">
                    &nbsp;<asp:Label ID="lblCustomerServicePhone" runat="server" Font-Bold="True"></asp:Label>
                    <asp:Image ID="imgCustServPhone" runat="server" ImageUrl="/Application/Images/help.gif" /></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblWebsiteHeader" runat="server" Text="Website Address" Font-Bold="False"></asp:Label>
                    <asp:Label ID="lblhttp" runat="server" Text="http://" Font-Bold="False"></asp:Label>
                </td>
                <td colspan="2" align="left">
                    &nbsp;<asp:Label ID="lblWebsite" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr height="20px">
            </tr>
            <tr>
                <td colspan="3" align="center" height="25px">
                    <asp:Label ID="lblBottom" runat="server" Text='Click on "Submit" to continue your application.'></asp:Label><br/>
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" TabIndex="19" /></td>
            </tr>
            <tr>
                <td colspan="3" align="center">
                    <asp:Panel ID="pnlCompany" runat="server" Style="display: none; z-index: 1;" Width="250px"
                        CssClass="DivHelp">
                        This must exactly match the registered business name with the IRS for US entities. If there is not enouph space it is ok to truncate but do not abreviate.  It can be the same as the Doing Business As (DBA) name or different. It is the individual's Legal Name for Sole proprietorships.</asp:Panel>
                    <asp:Panel ID="pnlDBA" runat="server" Style="display: none; z-index: 1;" Width="250px"
                        CssClass="DivHelp">
                        This is the name of your company as it appears on the
                        customer's billing statement. It may be different than your Company (Legal) Name.</asp:Panel>
                    <asp:Panel ID="pnlAddress" runat="server" Style="display: none; z-index: 1;" Width="250px"
                        CssClass="DivHelp">
                        The address used when registering the business. May be the home
                        address.</asp:Panel>
                    <asp:Panel ID="pnlCustServ" runat="server" Style="display: none; z-index: 1;" Width="250px"
                        CssClass="DivHelp">
                        This is the phone number that will show up on the customer's
                        credit card statments.</asp:Panel>
                        
                                                                            <asp:Button ID="btnShowPopup" runat="server" style="display:none" />
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

</td>
</tr>
</table>
</asp:Panel>
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
