<%@ Page Language="C#" MasterPageFile="/Application/Main.master" AutoEventWireup="true" CodeFile="principal.aspx.cs"
    Inherits="Gateway_principal" Title="Commerce Technologies - Online Application"
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
                        <td class="DivHeader" width="20%">
                            <a href="businessinfo.aspx" class="MenuCss">Business Information</a>
                        </td>
                        <td class="DivHeader2" width="20%">
                            <span class="MenuHeader">Principal Information</span>
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
                        <img name="gcontact" style="border: 0" alt="" src="/Application/Images/oa_Contact_mouseover.gif" /></a>
                    <a href="businessinfo.aspx" onmouseover="document.gbusiness.src='/Application/Images/oa_Business_mouseout.gif'"
                        onmouseout="document.gbusiness.src='/Application/Images/oa_Business_mouseover.gif'">
                        <img name="gbusiness" style="border: 0" alt="" src="/Application/Images/oa_Business_mouseover.gif" /></a>
                    <img name="gprincipal" style="border: 0" alt="" src="/Application/Images/oa_Principal.gif" />
                    <a href="bankinginfo.aspx" onmouseover="document.gbanking.src='/Application/Images/oa_Banking_mouseout.gif'"
                        onmouseout="document.gbanking.src='/Application/Images/oa_Banking_mouseover.gif'">
                        <img name="gbanking" style="border: 0" alt="" src="/Application/Images/oa_Banking_mouseover.gif" /></a>
                    <a href="reprogram.aspx" onmouseover="document.gplatform.src='/Application/Images/oa_Platform_mouseout.gif'"
                        onmouseout="document.gplatform.src='/Application/Images/oa_Platform_mouseover.gif'">
                        <img name="gplatform" alt="" style="border: 0" src="/Application/Images/oa_Platform_mouseover.gif" /></a>
                </td>
            </tr>-->
            <tr>
                <td colspan="3" align="center">
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <cc1:PopupControlExtender ID="PopupControlExtender1" runat="server" PopupControlID="pnlSSN"
                        TargetControlID="imgP1SSN" Position="Bottom" />
                </tr>
            <tr>
                <td colspan="3" align="center">                    
                    <asp:Label ID="lblError" runat="server" BackColor="Red" Font-Size="Medium" ForeColor="White"
                        Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right" colspan="3" height="25px">
                    <asp:Label ID="lblEdit" runat="server" Text='Click on "Edit" to modify Principal Information.'></asp:Label>
                    <!--<asp:HyperLink ID="lnkEditInfo" CssClass="Link" runat="server" NavigateUrl="principalinfo.aspx?EditPrincipal=True">Edit Principal Information</asp:HyperLink></td>-->
                    <asp:Button ID="btnEditInfo" CssClass="Link" runat="server" PostBackUrl="principalinfo.aspx?EditPrincipal=True" Text="Edit" />                    
            </tr>
            <tr>
                <td colspan="3" align="center" class="DivHeader">
                    <b><span class="MenuHeader">PRINCIPAL #1</span></b>
                </td>
            </tr>
            <tr>
                <td align="right" width="50%" valign="bottom">
                    <asp:Label ID="lblP1FirstHeader" runat="server" Text="First Name" Font-Bold="False"></asp:Label>
                </td>
                <td align="left" colspan="2">
                    &nbsp;<asp:Label ID="lblP1FirstName" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblP1MiddleHeader" runat="server" Text="Middle Name" Font-Bold="False"></asp:Label></td>
                <td align="left" colspan="2">
                    &nbsp;<asp:Label ID="lblP1MiddleName" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblP1LastHeader" runat="server" Text="Last Name" Font-Bold="False"></asp:Label></td>
                <td align="left" colspan="2">
                    <b>&nbsp;<asp:Label ID="lblP1LastName" runat="server" Font-Bold="True"></asp:Label></b></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblP1EmailHeader" runat="server" Font-Bold="False" Text="Email Address"></asp:Label></td>
                <td align="left" colspan="2">
                    &nbsp;<asp:Label ID="lblP1Email" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblP1TitleHeader" runat="server" Text="Job Title" Font-Bold="False"></asp:Label></td>
                <td align="left" colspan="2">
                    <b>&nbsp;<asp:Label ID="lblP1Title" runat="server" Font-Bold="True"></asp:Label></b></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblP1StreetHeader" runat="server" Text="Address" Font-Bold="False"></asp:Label></td>
                <td align="left" colspan="2">
                    &nbsp;<asp:Label ID="lblP1Address" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                </td>
                <td align="left" colspan="2">
                    &nbsp;<asp:Label ID="lblP1Address2" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblP1CityHeader" runat="server" Text="City" Font-Bold="False"></asp:Label></td>
                <td align="left" colspan="2">
                    &nbsp;<asp:Label ID="lblP1City" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblP1StateHeader" runat="server" Text="State" Font-Bold="False"></asp:Label></td>
                <td align="left" colspan="1">
                    &nbsp;<asp:Label ID="lblP1State" runat="server" Font-Bold="True"></asp:Label></td>
                <td align="left" valign="bottom">
                    <asp:Label ID="lblP1RegionHeader" runat="server" Text="Region" Font-Bold="False"></asp:Label>&nbsp;
                    <asp:Label ID="lblP1Region" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblP1ZipHeader" runat="server" Text="Zip Code" Font-Bold="False"></asp:Label></td>
                <td align="left" colspan="2">
                    &nbsp;<asp:Label ID="lblP1ZipCode" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblP1CountryHeader" runat="server" Text="Country" Font-Bold="False"></asp:Label></td>
                <td align="left" colspan="2">
                    &nbsp;<asp:Label ID="lblP1Country" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblP1PhoneHeader" runat="server" Text="Home Phone" Font-Bold="False"></asp:Label></td>
                <td align="left" colspan="2">
                    &nbsp;<asp:Label ID="lblP1HomePhone" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblP1MobilePhoneHeader" runat="server" Text="Mobile Phone" Font-Bold="False"></asp:Label></td>
                <td align="left" colspan="2">
                    &nbsp;<asp:Label ID="lblP1MobilePhone" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblP1SSNHeader" runat="server" Text="Social Security #" Font-Bold="False"></asp:Label></td>
                <td align="left" colspan="2">
                    &nbsp;<asp:Label ID="lblP1SSN" runat="server" Font-Bold="True"></asp:Label>
                    <asp:Image ID="imgP1SSN" runat="server" ImageUrl="/Application/Images/help.gif" /></td>
            </tr>
            <tr height="20px">
            </tr> 
            <tr>
                <td colspan="3" align="right" height="25px">
                    <asp:Label ID="lblBottom" runat="server" Text='Click on "Submit" to continue your application.'></asp:Label>
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" TabIndex="48" /></td>
            </tr>
            <tr>
                <td colspan="3" align="center">
                    <asp:Panel ID="pnlSSN" runat="server" Style="display: none; z-index: 1;" Width="250px" CssClass="DivHelp">
                        Social Security Number: Your social security number in the form 111223333 (9 digits).</asp:Panel>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
