<%@ Page Language="C#" MasterPageFile="/Application/Main.master" AutoEventWireup="true" CodeFile="reprog.aspx.cs"
    Inherits="Gateway_reprog" Title="Commerce Technologies - Online Application" Theme="TextBoxSkin" %>

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
                        <td class="DivHeader" width="20%">
                            <a href="principalinfo.aspx" class="MenuCss">Principal Information</a>
                        </td>
                        <td class="DivHeader" width="20%">
                            <a href="bankinginfo.aspx" class="MenuCss">Banking Information</a>
                        </td>
                        <td class="DivHeader2" width="20%">
                            <span class="MenuHeader">Platform Information</span>
                        </td>
                    </tr>
                </table>
            </td>
            </tr>
            <!--<tr>
                <td colspan="3" align="center">
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <a href="companyinfo.aspx" onmouseover="document.gcontact.src='/Application/Images/oa_Contact_mouseout.gif'"
                        onmouseout="document.gcontact.src='/Application/Images/oa_Contact_mouseover.gif'">
                        <img name="gcontact" style="border: 0" alt="" src="/Application/Images/oa_Contact_mouseover.gif" /></a>
                    <a href="businessinfo.aspx" onmouseover="document.gbusiness.src='/Application/Images/oa_Business_mouseout.gif'"
                        onmouseout="document.gbusiness.src='/Application/Images/oa_Business_mouseover.gif'">
                        <img name="gbusiness" style="border: 0" alt="" src="/Application/Images/oa_Business_mouseover.gif" /></a>
                    <a href="principalinfo.aspx" onmouseover="document.gprincipal.src='/Application/Images/oa_Principal_mouseout.gif'"
                        onmouseout="document.gprincipal.src='/Application/Images/oa_Principal_mouseover.gif'">
                        <img name="gprincipal" style="border: 0" alt="" src="/Application/Images/oa_Principal_mouseover.gif" /></a>
                    <a href="bankinginfo.aspx" onmouseover="document.gbanking.src='/Application/Images/oa_Banking_mouseout.gif'"
                        onmouseout="document.gbanking.src='/Application/Images/oa_Banking_mouseover.gif'">
                        <img name="gbanking" border="0" alt="" src="/Application/Images/oa_Banking_mouseover.gif" /></a>
                    <img name="gplatform" alt="" style="border: 0" src="/Application/Images/oa_Platform.gif" />
                </td>
            </tr>-->
            <tr>
                <td colspan="3" align="center">                    
                    <asp:Label ID="lblError" runat="server" BackColor="Red" Font-Size="Medium" ForeColor="White"
                        Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right" colspan="2" height="25px">
                    <asp:Label ID="lblEdit" runat="server" Text='Click on "Edit" to modify Platform Information.'></asp:Label>
                    <!--<asp:HyperLink ID="lnkEditInfo" CssClass="Link" runat="server" NavigateUrl="reprogram.aspx?EditPlatform=True">Edit Platform Information</asp:HyperLink>-->
                    <asp:Button ID="btnEditInfo" CssClass="Link" runat="server" PostBackUrl="reprogram.aspx?EditPlatform=True" Text="Edit" />                    
                </td>
            </tr>
            <tr height="10px">
            </tr>
            <tr>
                <td align="right" width="50%" valign="bottom">
                    <asp:Label ID="lblQuestion" runat="server" Text="Do you have a merchant account that you want to use with this payment gateway?"
                        Font-Bold="False"></asp:Label>
                </td>
                <td align="left">
                    <asp:RadioButton ID="rdbYes" runat="server" GroupName="PrevMerchantAcct" Text="Yes"
                        AutoPostBack="True" Font-Bold="False" TabIndex="1" Enabled="False" />
                    <asp:RadioButton ID="rdbNo" runat="server" GroupName="PrevMerchantAcct" Text="No"
                        AutoPostBack="True" Font-Bold="False" TabIndex="2" Enabled="False" />
                </td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblPlatformHeader" runat="server" Text="Please Select Platform" Font-Bold="False"></asp:Label></td>
                <td align="left">
                    &nbsp;<asp:Label ID="lblPlatform" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblMerchantNumHeader" runat="server" Text="Merchant Number" Font-Bold="False"></asp:Label></td>
                <td align="left">
                    &nbsp;<asp:Label ID="lblMerchantNumber" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblMerchantIdHeader" runat="server" Text="Merchant ID Number" Font-Bold="False"></asp:Label></td>
                <td align="left">
                    &nbsp;<asp:Label ID="lblMerchantIDNumber" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblTIDHeader" runat="server" Text="Terminal ID Number" Font-Bold="False"></asp:Label></td>
                <td align="left">
                    &nbsp;<asp:Label ID="lblTerminalIDNumber" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblLoginIdHeader" runat="server" Text="Login ID Number" Font-Bold="False"></asp:Label></td>
                <td align="left">
                    &nbsp;<asp:Label ID="lblLoginIDNumber" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblBankNum" runat="server" Text="Bank ID Number (BIN)" Font-Bold="False"></asp:Label></td>
                <td align="left">
                    &nbsp;<asp:Label ID="lblBankIDNumber" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblAgentBankNumHeader" runat="server" Text="Agent Bank ID Number"
                        Font-Bold="False"></asp:Label></td>
                <td align="left">
                    &nbsp;<asp:Label ID="lblAgentBankNumber" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lnlAgentChainNumHeader" runat="server" Text="Agent Chain Number" Font-Bold="False"></asp:Label></td>
                <td align="left">
                    &nbsp;<asp:Label ID="lblAgentChainNumber" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblMCC" runat="server" Text="MCC Category Code" Font-Bold="False"></asp:Label></td>
                <td align="left">
                    &nbsp;<asp:Label ID="lblMCCCategoryCode" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblStoreNumHeader" runat="server" Text="Store / Client Number" Font-Bold="False"></asp:Label></td>
                <td align="left">
                    &nbsp;<asp:Label ID="lblStoreNumber" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr height="10px">
            </tr> 
            <tr>
                <td align="center" colspan="2">
                    <asp:HyperLink CssClass="Link" Target="_blank" ID="lnkAgreement" runat="server" NavigateUrl="~/Docs/Placement Agr 9-8-04.doc"
                        TabIndex="14" Font-Bold="true">Agreement</asp:HyperLink><br />
                    <asp:Label ID="lblAgreement" runat="server" Text="Submit this form to signify that you have read, understood and agreed to the Terms and Conditions of the above agreement."></asp:Label>
                </td>
            </tr>
            <tr height="20px">
            </tr> 
            <tr>
                <td colspan="2" align="right" height="25px">
                    <asp:Label ID="lblBottom" runat="server" Text='Click on "Submit" to continue your application.'></asp:Label>
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" TabIndex="15" /></td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
