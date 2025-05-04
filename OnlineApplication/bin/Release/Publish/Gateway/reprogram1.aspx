<%@ Page Language="C#" MasterPageFile="/Application/Main.master" AutoEventWireup="true"
    CodeFile="reprogram.aspx.cs" Inherits="Gateway_reprogram" Title="Commerce Technologies - Online Application"
    Theme="TextBoxSkin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentHolder" runat="Server">
    <asp:Panel ID="pnlMainPage" runat="server">
        <table border="0" style="width: 100%;" class="DivTable">
            <tr>
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
            </tr>
            <tr>
                <td colspan="3" align="center" class="PageHeaderBG" style="height:25px">
                    <b><span class="MenuHeader">Gateway Signup
                        Information</span></b>
                </td>
            </tr>
            <tr>
                <td align="center" style="height: 31px" colspan="2">
                    <asp:Label ID="lblHeader" runat="server" Text="If you have an existing merchant account, please provide the following  information."></asp:Label>&nbsp;&nbsp;<br />
                    <asp:Label ID="lblError" runat="server" BackColor="Red" Font-Size="Medium" ForeColor="White"
                        Visible="False"></asp:Label><br />
                    <asp:ValidationSummary ID="ValidateSummary" runat="server" BackColor="#FFC0C0" BorderColor="red"
                        BorderWidth="1px" ForeColor="Black" HeaderText="Please check the fields marked in red."
                        Width="250px" />
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:Label ID="lblMessage" runat="server" Text='Since you have already signed up for a Merchant account with Commerce Technologies, please select "No" and click "Submit" to complete the Gateway application.'
                        Visible="False"></asp:Label></td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <span style="font-size: 10pt; font-family: Arial; color: #FF0000"><b>*</b> - denotes
                        a required field</span>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:Panel ID="pnlPlatform" runat="server" Width="100%">
                        <table border="0" style="width: 100%;">
                            <tr>
                                <td align="right" style="width: 40%">
                                    <asp:Label ID="lblQuestion" runat="server" Text="Do you have a previous Merchant Account Number?"
                                        Font-Bold="False"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:RadioButton ID="rdbYes" runat="server" GroupName="PrevMerchantAcct" Text="Yes"
                                        AutoPostBack="True" OnCheckedChanged="rdbYes_CheckedChanged" Font-Bold="False"
                                        TabIndex="1" />
                                    <asp:RadioButton ID="rdbNo" runat="server" GroupName="PrevMerchantAcct" Text="No"
                                        AutoPostBack="True" OnCheckedChanged="rdbYes_CheckedChanged" Font-Bold="False"
                                        TabIndex="2" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 40%">
                                    <asp:Label ID="lblPlatform" runat="server" Text="Please Select Platform" Font-Bold="False"></asp:Label></td>
                                <td align="left">
                                    <asp:DropDownList ID="lstPlatform" runat="server" AutoPostBack="True" OnSelectedIndexChanged="lstPlatform_SelectedIndexChanged"
                                        TabIndex="3">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem>FDR Omaha</asp:ListItem>
                                        <asp:ListItem>FDR Nashville</asp:ListItem>
                                        <asp:ListItem>Vital/Visanet</asp:ListItem>
                                        <asp:ListItem>Nova</asp:ListItem>
                                        <asp:ListItem>Global Payments</asp:ListItem>
                                        <asp:ListItem>North</asp:ListItem>
                                        <asp:ListItem>Paymentech</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="ValidatePlatform" runat="server" ControlToValidate="lstPlatform"
                                        ErrorMessage="Platform" Enabled="false" EnableClientScript="False"></asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 40%">
                                    <asp:Label ID="lblMerchantNum" runat="server" Text="Merchant Number" Font-Bold="False"></asp:Label></td>
                                <td align="left">
                                    <asp:TextBox ID="txtMerchantNumber" runat="server" MaxLength="16" Width="140px" TabIndex="4"></asp:TextBox><asp:RegularExpressionValidator
                                        ID="REMerchantNum" runat="server" ErrorMessage="Numbers Only" ValidationExpression="[0-9]*$"
                                        ControlToValidate="txtMerchantNumber" EnableClientScript="False"></asp:RegularExpressionValidator></td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 40%">
                                    <asp:Label ID="lblMerchantId" runat="server" Text="Merchant ID" Font-Bold="False"></asp:Label></td>
                                <td align="left">
                                    <asp:TextBox ID="txtMerchantIDNumber" runat="server" MaxLength="16" Width="140px"
                                        TabIndex="5"></asp:TextBox><asp:RegularExpressionValidator ID="REMID" runat="server"
                                            ControlToValidate="txtMerchantIDNumber" ErrorMessage="Numbers Only" ValidationExpression="[0-9]*$"
                                            EnableClientScript="False"></asp:RegularExpressionValidator></td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 40%">
                                    <asp:Label ID="lblTID" runat="server" Text="Terminal ID" Font-Bold="False"></asp:Label></td>
                                <td align="left">
                                    <asp:TextBox ID="txtTerminalIDNumber" runat="server" MaxLength="16" Width="140px"
                                        TabIndex="6"></asp:TextBox><asp:RegularExpressionValidator ID="RETID" runat="server"
                                            ControlToValidate="txtTerminalIDNumber" ErrorMessage="Numbers Only" ValidationExpression="[0-9]*$"
                                            EnableClientScript="False"></asp:RegularExpressionValidator></td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 40%">
                                    <asp:Label ID="lblLoginId" runat="server" Text="Login ID" Font-Bold="False"></asp:Label></td>
                                <td align="left">
                                    <asp:TextBox ID="txtLoginIDNumber" runat="server" MaxLength="16" Width="140px" TabIndex="7"></asp:TextBox><asp:RegularExpressionValidator
                                        ID="RELoginID" runat="server" ControlToValidate="txtLoginIDNumber" ErrorMessage="Numbers Only"
                                        ValidationExpression="[0-9]*$" EnableClientScript="False"></asp:RegularExpressionValidator></td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 40%">
                                    <asp:Label ID="lblBankNum" runat="server" Text="Bank ID Number (BIN)" Font-Bold="False"></asp:Label></td>
                                <td align="left">
                                    <asp:TextBox ID="txtBankNumber" runat="server" MaxLength="16" Width="140px" TabIndex="8"></asp:TextBox><asp:RegularExpressionValidator
                                        ID="REBankNum" runat="server" ControlToValidate="txtBankNumber" ErrorMessage="Numbers Only"
                                        ValidationExpression="[0-9]*$" EnableClientScript="False"></asp:RegularExpressionValidator></td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 40%">
                                    <asp:Label ID="lblAgentBankNum" runat="server" Text="Agent Bank ID Number" Font-Bold="False"></asp:Label></td>
                                <td align="left">
                                    <asp:TextBox ID="txtAgentBankNumber" runat="server" MaxLength="16" Width="140px"
                                        TabIndex="10"></asp:TextBox><asp:RegularExpressionValidator ID="REABN" runat="server"
                                            ControlToValidate="txtAgentBankNumber" ErrorMessage="Numbers Only" ValidationExpression="[0-9]*$"
                                            EnableClientScript="False"></asp:RegularExpressionValidator></td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 40%">
                                    <asp:Label ID="lnlAgentChainNum" runat="server" Text="Agent Chain Number" Font-Bold="False"></asp:Label></td>
                                <td align="left">
                                    <asp:TextBox ID="txtAgentChainNumber" runat="server" MaxLength="16" Width="140px"
                                        TabIndex="11"></asp:TextBox><asp:RegularExpressionValidator ID="REACN" runat="server"
                                            ControlToValidate="txtAgentChainNumber" ErrorMessage="Numbers Only" ValidationExpression="[0-9]*$"
                                            EnableClientScript="False"></asp:RegularExpressionValidator></td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 40%">
                                    <asp:Label ID="lblMCC" runat="server" Text="MCC Category Code" Font-Bold="False"></asp:Label></td>
                                <td align="left">
                                    <asp:TextBox ID="txtMCCCategoryCode" runat="server" MaxLength="16" Width="140px"
                                        TabIndex="12"></asp:TextBox><asp:RegularExpressionValidator ID="REMCC" runat="server"
                                            ControlToValidate="txtMCCCategoryCode" ErrorMessage="Numbers Only" ValidationExpression="[0-9]*$"
                                            EnableClientScript="False"></asp:RegularExpressionValidator></td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 40%">
                                    <asp:Label ID="lblStoreNum" runat="server" Text="Store / Client Number" Font-Bold="False"></asp:Label></td>
                                <td align="left">
                                    <asp:TextBox ID="txtStoreNumber" runat="server" MaxLength="16" Width="140px" TabIndex="13"></asp:TextBox><asp:RegularExpressionValidator
                                        ID="REStoreNum" runat="server" ControlToValidate="txtStoreNumber" ErrorMessage="Numbers Only"
                                        ValidationExpression="[0-9]*$" EnableClientScript="False"></asp:RegularExpressionValidator></td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 40%">
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:HyperLink CssClass="One" Target="_blank" Font-Bold="true" ID="lnkAgreement"
                        runat="server" NavigateUrl="~/Docs/Placement Agr 9-8-04.doc" TabIndex="14" Font-Names="Arial">Agreement</asp:HyperLink><br />
                    <asp:Label ID="lblAgreement" runat="server" Text="Submit this form to signify that you have read, understood and agreed to the Terms and Conditions of the above agreement."></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    &nbsp;<asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                        TabIndex="15" /></td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:Label ID="lblBottom" runat="server" Text='Click on "Submit" to save your application. You can continue it later if you do not wish to complete it entirely at this time. '></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center" valign="middle" colspan="2">
                    &nbsp;
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
