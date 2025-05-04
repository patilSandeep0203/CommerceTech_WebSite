<%@ Page Language="C#" MasterPageFile="/Application/Main.master" AutoEventWireup="true" CodeFile="business.aspx.cs"
    Inherits="Gateway_business" Title="Commerce Technologies - Online Application"
    Theme="TextBoxSkin" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentHolder" runat="Server">
    <asp:Panel ID="pnlMainPage" runat="server">
        <table border="0" cellpadding="2" cellspacing="0" Width="100%" class="DivGreen">
            <tr>
                <td colspan="3" align="center" class="DivHeader2" height="26px">
                    <b><span class="MenuHeader">Gateway Signup Information</span></b>
                </td>
            </tr> 
            <tr>
            <td colspan="3" align="center" class="DivHeader">
                <table border="0" cellpadding="0" cellspacing="0" Width="100%">
                    <tr>
                        <td class="DivHeader" width="20%">
                            <a href="companyinfo.aspx" class="MenuCss">Contact Information</a>
                        </td>
                        <td class="DivHeader2" width="20%">
                            <span class="MenuHeader">Business Information</span>
                        </td>
                        <td class="DivHeader" width="20%">
                            <a href="principalinfo.aspx" class="MenuCss">Principal Information</a>
                        </td>
                        <td class="DivHeader" width="20%">
                            <a href="bankinginfo.aspx" class="MenuCss">Banking Information</a>
                        </td>
                        <td class="DivHeader" width="20%">
                            <a href="reprogram.aspx" class="MenuCss">Platform Information</a>
                        </td>
                    </tr>
                </table>
            </td>
            </tr>
            <!--<tr>
                <td colspan="3" align="center">
                    <a href="companyinfo.aspx" onmouseover="document.gcontact.src='/Application/Images/oa_Contact_mouseout.gif'"
                        onmouseout="document.gcontact.src='/Application/Images/oa_Contact_mouseover.gif'">
                        <img style="border: 0;" name="gcontact" alt="" src="/Application/Images/oa_Contact_mouseover.gif" /></a>
                    <img name="gbusiness" style="border: 0" alt="" src="/Application/Images/oa_Business.gif" />
                    <a href="principalinfo.aspx" onmouseover="document.gprincipal.src='/Application/Images/oa_Principal_mouseout.gif'"
                        onmouseout="document.gprincipal.src='/Application/Images/oa_Principal_mouseover.gif'">
                        <img name="gprincipal" style="border: 0" alt="" src="/Application/Images/oa_Principal_mouseover.gif" /></a>
                    <a href="bankinginfo.aspx" onmouseover="document.gbanking.src='/Application/Images/oa_Banking_mouseout.gif'"
                        onmouseout="document.gbanking.src='/Application/Images/oa_Banking_mouseover.gif'">
                        <img name="gbanking" style="border: 0" alt="" src="/Application/Images/oa_Banking_mouseover.gif" /></a>
                    <a href="reprogram.aspx" onmouseover="document.gplatform.src='/Application/Images/oa_Platform_mouseout.gif'"
                        onmouseout="document.gplatform.src='/Application/Images/oa_Platform_mouseover.gif'">
                        <img name="gplatform" alt="" style="border: 0" src="/Application/Images/oa_Platform_mouseover.gif" /></a></td>
            </tr>-->
            <tr>
                <td colspan="3" align="center">
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <cc1:PopupControlExtender ID="PopupControlExtender1" runat="server" PopupControlID="pnlOwnership"
                        TargetControlID="imgOwnership" Position="Bottom" />
                    <cc1:PopupControlExtender ID="PopupControlExtender2" runat="server" PopupControlID="pnlTaxID"
                        TargetControlID="imgTaxID" Position="Bottom" />
                    <cc1:PopupControlExtender ID="PopupControlExtender3" runat="server" PopupControlID="pnlProduct"
                        TargetControlID="imgProducts" Position="Bottom" />
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
                    <!--<asp:HyperLink ID="lnkEditInfo" CssClass="Link" runat="server" NavigateUrl="businessinfo.aspx?EditBusiness=True">Edit Business Information</asp:HyperLink>-->
                    <asp:Button ID="btnEditInfo" CssClass="Link" runat="server" PostBackUrl="businessinfo.aspx?EditBusiness=True" Text="Edit" />
                </td>
            </tr>
            <tr>
                <td align="center" colspan="3" class="DivHeader">
                    <b><span class="MenuHeader"">BILLING INFORMATION</span></b></td>
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
                <td align="right"  valign="bottom">
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
                    &nbsp;<asp:Label ID="lblCountry" runat="server" Font-Bold="True"></asp:Label></td>
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
                    <asp:Label ID="lblProductsSoldHeader" runat="server" Text="Products/Services Sold (Be Descriptive. 250 max Characters)"
                        Font-Bold="False"></asp:Label></td>
                <td colspan="2" style="text-align: left">
                    &nbsp;<asp:Label ID="lblProductsSold" runat="server" Font-Bold="True"></asp:Label>
                    <asp:Image ID="imgProducts" runat="server" ImageUrl="/Application/Images/help.gif" />
                </td>
            </tr>
            <tr height="20px">
            </tr>            
            <tr>
                <td colspan="3" align="right" height="25px">
                    <asp:Label ID="lblBottom" runat="server" Text='Click on "Submit" to continue your application.'></asp:Label>
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" TabIndex="26" /></td>
            </tr>
            <tr>
                <td colspan="3" align="center">
                    <asp:Panel ID="pnlOwnership" runat="server" Style="display: none; z-index: 1;" Width="250px" CssClass="DivHelp">
                        Ownership/Legal Status:Select how you have registered your business i.e., legal/medical, sole proprietorship, partnership, corporation, LLC, etc. Select Others if none of the options in the list apply to you.</asp:Panel>
                    <asp:Panel ID="pnlTaxID" runat="server" Style="display: none; z-index: 1;" Width="250px" CssClass="DivHelp">
                        Federal Tax ID: 9-digit number. Example: 123456789. This is also known as a tax payer ID. In case of sole proprietorship, this will be your Social Security Number. (No Dashes)</asp:Panel>
                    <asp:Panel ID="pnlProduct" runat="server" Style="display: none; z-index: 1;" Width="250px" CssClass="DivHelp">
                        Products/Services sold: A brief description of the products/services sold by your company. Please limit your description to 250 characters.</asp:Panel>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
