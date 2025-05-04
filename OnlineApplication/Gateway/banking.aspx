<%@ Page Language="C#" MasterPageFile="/Application/Main.master" AutoEventWireup="true" CodeFile="banking.aspx.cs"
    Inherits="Gateway_banking" Title="Commerce Technologies - Online Application" Theme="TextBoxSkin" %>

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
                        <td class="DivHeader" width="20%">
                            <a href="principalinfo.aspx" class="MenuCss">Principal Information</a>
                        </td>
                        <td class="DivHeader2" width="20%">
                            <span class="MenuHeader">Banking Information</span>
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
                    <a href="principalinfo.aspx" onmouseover="document.gprincipal.src='/Application/Images/oa_Principal_mouseout.gif'"
                        onmouseout="document.gprincipal.src='/Application/Images/oa_Principal_mouseover.gif'">
                        <img name="gprincipal" style="border: 0" alt="" src="/Application/Images/oa_Principal_mouseover.gif" /></a>
                    <img name="mbanking" style="border: 0" alt="" src="/Application/Images/oa_Banking.gif" />
                    <a href="reprogram.aspx" onmouseover="document.gplatform.src='/Application/Images/oa_Platform_mouseout.gif'"
                        onmouseout="document.gplatform.src='/Application/Images/oa_Platform_mouseover.gif'">
                        <img name="gplatform" alt="" style="border: 0" src="/Application/Images/oa_Platform_mouseover.gif" /></a>
                </td>
            </tr>-->
            <tr>
                <td colspan="3" align="center">
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <cc1:PopupControlExtender ID="PopupControlExtender1" runat="server" PopupControlID="pnlDiscover"
                        TargetControlID="imgDiscover" Position="Bottom" />
                    <cc1:PopupControlExtender ID="PopupControlExtender2" runat="server" PopupControlID="pnlAmex"
                        TargetControlID="imgAmex" Position="Bottom" />
                    <cc1:PopupControlExtender ID="PopupControlExtender3" runat="server" PopupControlID="pnlJCB"
                        TargetControlID="imgJCB" Position="Bottom" />
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
                    <asp:Button ID="btnEditInfo" CssClass="Link" runat="server" PostBackUrl="bankinginfo.aspx?EditBanking=True" Text="Edit" />
                </td>
            </tr>
            <tr>
                <td align="right" width=50% valign="bottom">
                    <asp:Label ID="lblBankNameHeader" runat="server" Text="Bank Name" Font-Bold="False"></asp:Label></td>
                <td align="left" colspan="2">
                    <strong>&nbsp;<asp:Label ID="lblBankName" runat="server"></asp:Label></strong></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblOtherBankHeader" runat="server" Text="If Other, specify" Font-Bold="False"></asp:Label></td>
                <td align="left" colspan="2">
                    &nbsp;<asp:Label ID="lblOtherBank" runat="server" Font-Bold="True"></asp:Label></td>
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
                    &nbsp;<asp:Label Font-Bold="true" ID="lblCity" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblZipCodeHeader" runat="server" Text="Zip Code" Font-Bold="False"></asp:Label></td>
                <td align="left" colspan="2">
                    &nbsp;<asp:Label ID="lblZipCode" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblStateHeader" runat="server" Text="State" Font-Bold="False"></asp:Label></td>
                <td align="left" style="width: 25%">
                    &nbsp;<asp:Label ID="lblState" runat="server" Font-Bold="True"></asp:Label></td>
                <td align="left"  valign="bottom" style="width: 35%">
                    <asp:Label ID="lblRegionHeader" runat="server" Text="Region" Font-Bold="False"></asp:Label>&nbsp;
                    <asp:Label Font-Bold="True" ID="lblRegion" runat="server"></asp:Label></td>
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
                        Font-Bold="False"></asp:Label></td>
                <td colspan="2" align="left">
                    &nbsp;<asp:Label Font-Bold="true" ID="lblNameOnChecking" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblRoutingNumberHeader" runat="server" Text="Bank Routing Number"
                        Font-Bold="False"></asp:Label></td>
                <td colspan="2" align="left">
                    &nbsp;<asp:Label ID="lblBankRoutingNumber" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblAcctNum" runat="server" Text="Bank Account Number" Font-Bold="False"></asp:Label></td>
                <td colspan="2" align="left">
                    &nbsp;<asp:Label ID="lblAcctNumber" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr valign="top">
                <td colspan="3" align="center" class="DivHeader">
                    <strong><span class="MenuHeader">NON-BANKCARD INFORMATION</span></strong>
                </td>
            </tr>
            <tr>
                <td align="right" colspan="3" height="25px">
                    <!--<b><asp:HyperLink ID="lnkProcessing" CssClass="Link" runat="server" NavigateUrl="bankinginfo.aspx?EditProcessing=True">Edit Non Bankcard Information</asp:HyperLink></b>-->
                    <asp:Label ID="lblEditNBC" runat="server" Text='Click on "Edit" to modify Non Bankcard Information.'></asp:Label>
                    <asp:Button ID="btnEditNBC" CssClass="Link" runat="server" PostBackUrl="bankinginfo.aspx?EditProcessing=True" Text="Edit" />
                </td>
            </tr>
            <tr valign="top">
                <td align="right" valign="bottom">
                    <asp:Label ID="lblDiscoverHeader" runat="server" Text="Discover Card" Font-Bold="False"></asp:Label>
                </td>
                <td align="left" colspan="2">
                    <b>&nbsp;<asp:Label ID="lblDiscover" runat="server"></asp:Label>
                        <asp:Image ID="imgDiscover" runat="server" ImageUrl="/Application/Images/help.gif" /></b><b></b>
                </td>
            </tr>
            <tr valign="top">
                <td align="right" colspan="3">
                    <asp:Panel ID="pnlDiscoverNum" runat="server" Height="100%" Width="100%">
                        <table border="0" width="100%">
                            <tr>
                                <td align="right" width="50%" valign="bottom">
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
                <td align="left" colspan="2">
                    <b>&nbsp;<asp:Label ID="lblAmex" runat="server"></asp:Label>
                        <asp:Image ID="imgAmex" runat="server" ImageUrl="/Application/Images/help.gif" />
                    </b>
                </td>
            </tr>
            <tr valign="top">
                <td align="right" colspan="3">
                    <asp:Panel ID="pnlAmexNum" runat="server" Height="100%" Width="100%">
                        <table width="100%" border="0">
                            <tr>
                                <td align="right" width="50%" valign="bottom">
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
                <td valign="top" align="left" colspan="2">
                    &nbsp;<asp:Label ID="lblJCB" runat="server" Font-Bold="True"></asp:Label>
                    <asp:Image ID="imgJCB" runat="server" ImageUrl="/Application/Images/help.gif" />
                </td>
            </tr>
            <tr valign="top">
                <td align="right" colspan="3">
                    <asp:Panel ID="pnlJCBNum" runat="server" Height="100%" Width="100%">
                        <table border="0" width="100%">
                            <tr>
                                <td align="right" width="50%" valign="bottom">
                                    <asp:Label ID="lblJCBNumHeader" runat="server" Text="Existing Number" Font-Bold="False"></asp:Label></td>
                                <td align="left" colspan="2">
                                    &nbsp;<asp:Label ID="lblJCBNum" runat="server" Font-Bold="True"></asp:Label></td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr height="20px">
            </tr> 
            <tr>
                <td colspan="3" align="right" height="25px">
                    <asp:Label ID="lblBottom" runat="server" Text='Click on "Submit" to continue your application.'></asp:Label>
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" TabIndex="22" /></td>
            </tr>
            <tr>
                <td colspan="3" align="center">
                    <asp:Panel ID="pnlDiscover" runat="server" Style="display: none; z-index: 1;" Width="250px" CssClass="DivHelp">
                        Select the option Yes - Existing if you have an existing Discover number. Enter the existing number in the following textbox.
                        </asp:Panel>
                    <asp:Panel ID="pnlAmex" runat="server" Style="display: none; z-index: 1;" Width="250px" CssClass="DivHelp">
                        Select the option Yes - Existing if you have an existing Amex number. Enter the existing number in the following textbox.</asp:Panel>
                    <asp:Panel ID="pnlJCB" runat="server" Style="display: none; z-index: 1;" Width="250px" CssClass="DivHelp">
                        Select the option Yes - Existing if you have an existing JCB number. Enter the existing number in the following textbox.</asp:Panel>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
