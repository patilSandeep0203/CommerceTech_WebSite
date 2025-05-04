<%@ Page Language="C#" MasterPageFile="/Application/Main.master" AutoEventWireup="true" CodeFile="company.aspx.cs"
    Inherits="Gateway_company" Title="Commerce Technologies - Online Application" Theme="TextBoxSkin" %>

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
                        <td class="DivHeader2" width="20%">
                            <span class="MenuHeader">Contact Information</span>
                        </td>
                        <td class="DivHeader" width="20%">
                            <a href="businessinfo.aspx" class="MenuCss">Business Information</a>
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
                    <img name="gcontact" style="border: 0" alt="" src="/Application/Images/oa_Contact.gif" />
                    <a href="businessinfo.aspx" onmouseover="document.gbusiness.src='/Application/Images/oa_Business_mouseout.gif'"
                        onmouseout="document.gbusiness.src='/Application/Images/oa_Business_mouseover.gif'">
                        <img name="gbusiness" style="border: 0" alt="" src="/Application/Images/oa_Business_mouseover.gif" /></a>
                    <a href="principalinfo.aspx" onmouseover="document.gprincipal.src='/Application/Images/oa_Principal_mouseout.gif'"
                        onmouseout="document.gprincipal.src='/Application/Images/oa_Principal_mouseover.gif'">
                        <img name="gprincipal" style="border: 0" alt="" src="/Application/Images/oa_Principal_mouseover.gif" /></a>
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
                    <!--<asp:HyperLink ID="lnkEditInfo" CssClass="Link" runat="server" NavigateUrl="companyinfo.aspx?EditCompany=True">Edit Contact Information</asp:HyperLink>-->
                    <asp:Button ID="btnEditInfo" CssClass="Link" runat="server" PostBackUrl="companyinfo.aspx?EditCompany=True" Text="Edit" />                    
                </td>
            </tr>
            <tr height="10px">
            </tr>
            <tr>
                <td align="right" width="50%" valign="bottom">
                    <asp:Label ID="lblCompanyNameHeader" runat="server" Text="Company (Legal Name)" Font-Bold="False"></asp:Label>
                </td>
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
                <td align="left" valign="bottom">
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
                <td align="right" valign="bottom">
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
                <td colspan="3" align="right" height="25px">
                    <asp:Label ID="lblBottom" runat="server" Text='Click on "Submit" to continue your application.'></asp:Label>
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" TabIndex="19" /></td>
            </tr>            
            <tr>
                <td colspan="3" align="center">
                    <asp:Panel ID="pnlCompany" runat="server" Style="display: none; z-index: 1;" Width="250px" CssClass="DivHelp">
                        Company (Legal Name): This is the registered business name. It can be the same as the Doing Business As (DBA) name or different. Legal Name is the individual's Legal Name for Sole proprietorships.</asp:Panel>
                    <asp:Panel ID="pnlDBA" runat="server" Style="display: none; z-index: 1;" Width="250px" CssClass="DivHelp">
                        Doing Busniness As (DBA): This is the name of your company as it appears on the customer's billing statement. It may be different than your Company (Legal) Name.</asp:Panel>
                    <asp:Panel ID="pnlAddress" runat="server" Style="display: none; z-index: 1;" Width="250px" CssClass="DivHelp">
                        Physical Address: The address used when registering the business. May be the home address.</asp:Panel>
                    <asp:Panel ID="pnlCustServ" runat="server" Style="display: none; z-index: 1;" Width="250px" CssClass="DivHelp">
                        Customer Service Phone: This is the phone number that will show up on the customer's credit card statments.</asp:Panel>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
