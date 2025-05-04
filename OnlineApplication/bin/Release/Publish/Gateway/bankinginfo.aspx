<%@ Page Language="C#" MasterPageFile="/Application/Main.master" AutoEventWireup="true"
    CodeFile="bankinginfo.aspx.cs" Inherits="Gateway_bankinginfo" Title="Commerce Technologies - Online Application"
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
                    <asp:Label ID="lblHeader" runat="server" Text="Please provide the following financial information about your company."></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="3" align="center">
                    <asp:Label ID="lblError" runat="server" BackColor="Red" Font-Size="Medium" ForeColor="White"
                        Visible="False"></asp:Label><br /><asp:ValidationSummary ID="ValidateSummary" runat="server"
                            BackColor="#FFC0C0" BorderColor="red" BorderWidth="1px" ForeColor="Black" HeaderText="Please check the fields marked in red."
                            Width="250px" />
                </td>
            </tr>
            <tr>
                <td colspan="3" align="center">
                    <span class="LabelsRed""><b>*</b> - denotes a required field</span>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="3">
                    <asp:Panel ID="pnlBanking" runat="server" Height="100%" Width="100%">
                        <table border="0" style="width: 100%;">
                            <tr>
                                <td colspan="3" align="center" class="DivHeader">
                                    <strong><span class="MenuHeader">BANK ACCOUNT INFORMATION</span></strong></td>
                            </tr>
                            <tr>
                                <td align="right" width="40%">
                                    <asp:Label ID="lblBankName" runat="server" Text="Bank Name" Font-Bold="False"></asp:Label></td>
                                <td align="left" colspan="2">
                                    <asp:DropDownList ID="lstBankName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="lstBankName_SelectedIndexChanged"
                                        TabIndex="1">
                                    </asp:DropDownList><span style="color: red">*</span><asp:RequiredFieldValidator
                                        ID="ValidateBank" runat="server" ErrorMessage="Bank Name" Font-Bold="False" ControlToValidate="lstBankName"
                                        EnableClientScript="False"></asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblOtherBank" runat="server" Text="If Other, specify" Font-Bold="False"></asp:Label></td>
                                <td align="left" colspan="2">
                                    <asp:TextBox ID="txtOtherBank" runat="server" MaxLength="64" Width="140px" TabIndex="2"
                                        Enabled="False"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="ValidateOtherBank" runat="server" ControlToValidate="txtOtherBank"
                                        ErrorMessage="Other Bank Name" Font-Bold="False" Enabled="False" EnableClientScript="False"></asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblBankAddress" runat="server" Text="Bank Address" Font-Bold="False"></asp:Label></td>
                                <td align="left" colspan="2">
                                    <asp:TextBox ID="txtBankAddress" runat="server" MaxLength="96" Width="140px" TabIndex="3"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblCity" runat="server" Text="City" Font-Bold="False"></asp:Label></td>
                                <td colspan="2" align="left">
                                    <asp:TextBox ID="txtCity" runat="server" MaxLength="20" Width="140px" TabIndex="4"></asp:TextBox><span
                                        style="color: red">*</span><asp:RequiredFieldValidator ID="ValidateCity" runat="server"
                                            ErrorMessage="Bank City" Font-Bold="False" ControlToValidate="txtCity" EnableClientScript="False"></asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblZipCode" runat="server" Text="Zip Code" Font-Bold="False"></asp:Label></td>
                                <td align="left" colspan="2">
                                    <asp:TextBox ID="txtZipCode" runat="server" MaxLength="30" Width="140px" TabIndex="5"></asp:TextBox><span
                                        style="color: red">* </span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtZipCode"
                                        EnableClientScript="False" ErrorMessage="Zipcode" Font-Bold="False"></asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblState" runat="server" Text="State" Font-Bold="False"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="lstState" runat="server" TabIndex="6">
                                    </asp:DropDownList><span style="color: red">*</span>
                                    <asp:RequiredFieldValidator ID="ValidateState" runat="server" ErrorMessage="State"
                                        Font-Bold="False" ControlToValidate="lstState" EnableClientScript="False"></asp:RequiredFieldValidator></td>
                                <td align="left">
                                    <asp:Label ID="lblRegion" runat="server" Text="Region" Font-Bold="False"></asp:Label>
                                    <asp:TextBox ID="txtRegion" runat="server" MaxLength="40" TabIndex="7"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblPhone" runat="server" Text="Bank Phone Number  " Font-Bold="False"></asp:Label></td>
                                <td colspan="2" align="left">
                                    <asp:TextBox ID="txtPhone" runat="server" MaxLength="25" Width="140px" TabIndex="8"></asp:TextBox>
                                    </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblNameOnChecking" runat="server" Text="Name Exactly As It Appears On Checking Account"
                                        Font-Bold="False"></asp:Label>
                                </td>
                                <td colspan="2" align="left">
                                    <asp:TextBox ID="txtNameOnChecking" runat="server" MaxLength="50" Width="140px" TabIndex="9"></asp:TextBox><span
                                        style="color: red">*</span><asp:RequiredFieldValidator ID="ValidateCheckingAcct"
                                            runat="server" ErrorMessage="Name on Checking Acct." Font-Bold="False" ControlToValidate="txtNameOnChecking"
                                            EnableClientScript="False"></asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblRoutingNumber" runat="server" Text="Bank Routing Number" Font-Bold="False"></asp:Label></td>
                                <td colspan="2" align="left">
                                    <asp:TextBox ID="txtBankRoutingNumber" runat="server" MaxLength="9" Width="140px"
                                        TabIndex="10"></asp:TextBox><span style="color: red">*</span>
                                    <asp:Label ID="lblRoutingEx" runat="server" Text="(9 digits long)"></asp:Label>
                                    <asp:LinkButton ID="lnkRoutingNum" runat="server" CssClass="Link" OnClick="lnkRoutingNum_Click">Edit</asp:LinkButton>
                                    <asp:RequiredFieldValidator ID="ValidateRoutingNo" runat="server" ErrorMessage="Routing Number"
                                        ControlToValidate="txtBankRoutingNumber" EnableClientScript="False"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RERoutingNum" runat="server" ControlToValidate="txtBankRoutingNumber"
                                        ErrorMessage="Numbers Only" ValidationExpression="[0-9]*$" EnableClientScript="False"></asp:RegularExpressionValidator></td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblAcctNum" runat="server" Text="Bank Account Number" Font-Bold="False"></asp:Label>
                                </td>
                                <td colspan="2" align="left">
                                    <asp:TextBox ID="txtAcctNumber" runat="server" MaxLength="20" Width="140px" TabIndex="11"></asp:TextBox><span
                                        style="color: red">*</span>
                                    <asp:LinkButton ID="lnkAccountNum" runat="server" CssClass="Link" OnClick="lnkAccountNum_Click">Edit</asp:LinkButton>
                                    <asp:RequiredFieldValidator ID="ValidateAcctNo" runat="server" ErrorMessage="Account Number"
                                        ControlToValidate="txtAcctNumber" EnableClientScript="False"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="REAcctNum" runat="server" ControlToValidate="txtAcctNumber"
                                        ErrorMessage="Numbers Only" ValidationExpression="[0-9]*$" EnableClientScript="False"></asp:RegularExpressionValidator></td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr valign="top">
                <td align="center" colspan="3" valign="middle">
                    <asp:Panel ID="pnlNBC" runat="server" Height="100%" Width="100%">
                        <table border="0" style="width: 100%;">
                            <tr>
                                <td colspan="3" align="center" class="DivHeader">
                                    <strong><span class="MenuHeader">NON-BANKCARD INFORMATION</span></strong>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" width="40%">
                                    <asp:Label ID="lblDiscover" runat="server" Text="Discover Card" Font-Bold="False"></asp:Label>
                                </td>
                                <td align="left" colspan="2">
                                    <asp:DropDownList ID="lstDiscover" runat="server" TabIndex="12" OnSelectedIndexChanged="lstDiscover_SelectedIndexChanged"
                                        AutoPostBack="True">
                                        <asp:ListItem Selected="True">Yes</asp:ListItem>
                                        <asp:ListItem>Yes - Existing</asp:ListItem>
                                    </asp:DropDownList><span style="color: red">*</span>
                                    <asp:HyperLink ID="lnkDiscover" Target="_blank" runat="server" CssClass="LinkSmall" 
                                        NavigateUrl="~/Docs/Discover.html">Included</asp:HyperLink>
                                    <asp:Image ID="imgDiscover" runat="server" ImageUrl="/Application/Images/help.gif" /></td>
                            </tr>
                            <tr>
                                <td align="right" colspan="3">
                                    <asp:Panel ID="pnlDiscoverNum" runat="server" Height="100%" Width="100%">
                                        <table border="0" width="100%">
                                            <tr>
                                                <td align="right" width="40%">
                                                    <asp:Label ID="lblExistingDiscoverNum" runat="server" Text="Existing Number" Font-Bold="False"></asp:Label></td>
                                                <td align="left" colspan="2">
                                                    <asp:TextBox ID="txtDiscoverNum" runat="server" MaxLength="15" Width="140px" TabIndex="16"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="ValidateDiscoverNum" runat="server" ControlToValidate="txtDiscoverNum"
                                                        ErrorMessage="Discover Number" EnableClientScript="False"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="REDiscNum" runat="server" ControlToValidate="txtDiscoverNum"
                                                        ErrorMessage="Numbers Only" ValidationExpression="[0-9]*$" EnableClientScript="False"></asp:RegularExpressionValidator></td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblAmex" runat="server" Text="Do you want to accept American Express?"
                                        Font-Bold="False"></asp:Label>
                                </td>
                                <td align="left" colspan="2">
                                    <asp:DropDownList ID="lstAmex" runat="server" TabIndex="13" OnSelectedIndexChanged="lstAmex_SelectedIndexChanged"
                                        AutoPostBack="True">
                                        <asp:ListItem>No</asp:ListItem>
                                        <asp:ListItem>Yes - Existing</asp:ListItem>
                                    </asp:DropDownList><span style="color: red">*</span>
                                    <asp:HyperLink ID="lnkAmex" Target="_blank" runat="server" CssClass="LinkSmall"
                                        NavigateUrl="~/Docs/AMEX.html">Recommended</asp:HyperLink>
                                    <asp:Image ID="imgAmex" runat="server" ImageUrl="/Application/Images/help.gif" /></td>
                            </tr>
                            <tr>
                                <td align="right" colspan="3">
                                    <asp:Panel ID="pnlAmexNum" runat="server" Height="100%" Width="100%">
                                        <table width="100%" border="0">
                                            <tr>
                                                <td align="right" width="40%">
                                                    <asp:Label ID="lblAmexNum" runat="server" Text="Existing Number" Font-Bold="False"></asp:Label></td>
                                                <td align="left" colspan="2">
                                                    <asp:TextBox ID="txtAmexNum" runat="server" MaxLength="10" Width="140px" TabIndex="18"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="ValidateAmexNum" runat="server" ControlToValidate="txtAmexNum"
                                                        ErrorMessage="Amex Number" EnableClientScript="False"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="REAmexNum" runat="server" ControlToValidate="txtAmexNum"
                                                        ErrorMessage="Numbers Only" ValidationExpression="[0-9]*$" EnableClientScript="False"></asp:RegularExpressionValidator></td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblJCB" runat="server" Text="Do you want to accept JCB?" Font-Bold="False"></asp:Label>
                                </td>
                                <td align="left" colspan="2">
                                    <asp:DropDownList ID="lstJCB" runat="server" TabIndex="14" OnSelectedIndexChanged="lstJCB_SelectedIndexChanged"
                                        AutoPostBack="True">
                                        <asp:ListItem>No</asp:ListItem>
                                        <asp:ListItem>Yes - Existing</asp:ListItem>
                                    </asp:DropDownList><span style="color: red">*
                                        <asp:Image ID="imgJCB" runat="server" ImageUrl="/Application/Images/help.gif" /></span></td>
                            </tr>
                            <tr>
                                <td align="right" colspan="3">
                                    <asp:Panel ID="pnlJCBNum" runat="server" Height="100%" Width="100%">
                                        <table border="0" width="100%">
                                            <tr>
                                                <td align="right" width="40%">
                                                    <asp:Label ID="lblJCBNum" runat="server" Text="Existing Number" Font-Bold="False"></asp:Label></td>
                                                <td align="left" colspan="2">
                                                    <asp:TextBox ID="txtJCBNum" runat="server" MaxLength="16" Width="140px" TabIndex="20"></asp:TextBox><asp:RequiredFieldValidator
                                                        ID="ValidateJCBNum" runat="server" ControlToValidate="txtJCBNum" ErrorMessage="JCB Number"
                                                        EnableClientScript="False"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="REJCBNum" runat="server" ControlToValidate="txtJCBNum"
                                                        ErrorMessage="Numbers Only" ValidationExpression="[0-9]*$" EnableClientScript="False"></asp:RegularExpressionValidator></td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr height="20px">
            </tr> 
            <tr>
                <td align="right" colspan="3" height="25px">
                    <asp:Label ID="lblBottom" runat="server" Text='Click "Submit" to save your application and continue to the next page.'></asp:Label>
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" TabIndex="15" /></td>
            </tr>
            <!--<tr>
                <td align="right" colspan="3" height="25px">
                    <asp:Label ID="lblSave" runat="server" Text='Click "Save" to save your partially completed information.  You may finish completing this page another time'></asp:Label>
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" TabIndex="16" /></td>
            </tr>-->
            <tr>
                <td colspan="3" align="center">
                    <asp:Panel ID="pnlDiscover" runat="server" Style="display: none; z-index: 1;" Width="250px" CssClass="DivHelp">
                        Select the option Yes - Existing if you have an existing Discover number. Enter the existing number in the following textbox.</asp:Panel>
                    <asp:Panel ID="pnlAmex" runat="server" Style="display: none; z-index: 1;" Width="250px" CssClass="DivHelp">
                        Select the option Yes - Existing if you have an existing Amex number. Enter the existing number in the following textbox.</asp:Panel>
                    <asp:Panel ID="pnlJCB" runat="server" Style="display: none; z-index: 1;" Width="250px" CssClass="DivHelp">
                        Select the option Yes - Existing if you have an existing JCB number. Enter the existing number in the following textbox.</asp:Panel>
                </td>
            </tr>
       </table>
    </asp:Panel>
</asp:Content>
