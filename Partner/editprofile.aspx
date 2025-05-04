<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="editprofile.aspx.cs"
    Inherits="editprofile" Title="Commerce Technologies - Online Application" Theme="TextBoxSkin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentHolder" runat="Server">
    <asp:ScriptManager ID="ScriptManagerEdit" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pnlMainPage" runat="server">
        <table width="100%" class="DivGreen" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="2" align="center" class="DivHeader2">
                    <b><span class="MenuHeader">Edit Profile</span></b>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2" >
                    <asp:Label ID="lblError" runat="server" BackColor="Red" Font-Size="Medium" ForeColor="White"
                        Visible="False"></asp:Label><br /><asp:ValidationSummary ID="ValidateSummary" runat="server"
                            BackColor="White" BorderColor="red" BorderWidth="1px" ForeColor="Black" HeaderText="Please check the fields marked in red."
                            Width="250px" />
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <span class="LabelsRed"><b>*</b> - denotes a required field</span></td>
            </tr>
            <tr height="10px">
            </tr>           
            <tr>
                <td colspan="2" align="center" valign="top">
                    <table style="width: 100%" border="0">
                    <tr>
                            <td align="center" width="100%" colspan="2">
                                <asp:Label ID="contact" runat="server" Text=""><strong>Contact Information</strong></asp:Label></td>
                            
                        </tr>
                        <tr>
                            <td align="right" width="40%">
                                <asp:Label ID="lblLoginNameHeader" runat="server" Text="Login Name"></asp:Label></td>
                            <td align="left">
                                <asp:Label ID="lblLoginName" runat="server" Font-Bold="True"></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="lblFirst" runat="server" Text="First Name"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtFirstName" runat="server" Width="140px" MaxLength="32"></asp:TextBox><span
                                    style="color: #ff0000">*</span>
                                <asp:RequiredFieldValidator ID="ValidateFirstName" runat="server" Display="Static"
                                    ControlToValidate="txtFirstName" ErrorMessage="First Name" EnableClientScript="False"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="lblLast" runat="server" Text="Last Name"></asp:Label></td>
                            <td align="left">
                                <asp:TextBox ID="txtLastName" runat="server" Width="140px" MaxLength="32"></asp:TextBox><span
                                    style="color: #ff0000">*</span>
                                <asp:RequiredFieldValidator ID="ValidateLastName" runat="server" ControlToValidate="txtLastName"
                                    ErrorMessage="Last Name" EnableClientScript="False"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label></td>
                            <td align="left">
                                <asp:TextBox ID="txtEmail" runat="server" MaxLength="50" Width="140px"></asp:TextBox><span
                                    style="color: #ff0000">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                        runat="server" ControlToValidate="txtEmail" EnableClientScript="False" ErrorMessage="Email"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="ValidateEmail" runat="server" ControlToValidate="txtEmail"
                                    EnableClientScript="False" ErrorMessage="Invalid Email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="lblTitle" runat="server" Text="Title"></asp:Label></td>
                            <td align="left">
                                <asp:TextBox ID="txtTitle" runat="server" MaxLength="50" Width="140px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="lblPhone" runat="server" Text="Business Phone"></asp:Label></td>
                            <td align="left">
                                <asp:TextBox ID="txtPhone" runat="server" Width="140px" MaxLength="16"></asp:TextBox>
                                <asp:Label ID="lblExt" runat="server" Text="Ext."></asp:Label>
                                <asp:TextBox ID="txtPhoneExt" runat="server" Width="40px" MaxLength="4"></asp:TextBox><asp:RegularExpressionValidator
                                    ID="RegularExpressionValidatorPhone" runat="server" ControlToValidate="txtPhone"
                                    ErrorMessage="Phone Number (ex. 123-123-1234)" ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}"
                                    EnableClientScript="False"></asp:RegularExpressionValidator>
                                <asp:RegularExpressionValidator ID="REExt" runat="server" ControlToValidate="txtPhoneExt"
                                    ErrorMessage="Numbers Only" ValidationExpression="[0-9]*$" EnableClientScript="False"></asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="lblHomePhone" runat="server" Text="Home Phone"></asp:Label></td>
                            <td align="left">
                                <asp:TextBox ID="txtHomePhone" runat="server" MaxLength="50" Width="140px"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtHomePhone"
                                    ErrorMessage="Phone Number (ex. 123-123-1234)" ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}"
                                    EnableClientScript="False"></asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="lblMobile" runat="server" Text="Mobile Phone"></asp:Label></td>
                            <td align="left">
                                <asp:TextBox ID="txtMobilePhone" runat="server" Width="140px" MaxLength="16"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidatorMobile" runat="server"
                                    ControlToValidate="txtMobilePhone" ErrorMessage="Mobile Number (ex. 123-123-1234)"
                                    ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}" EnableClientScript="False"></asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                <table style="width: 100%">
                                    <tr>
                                        <td align="right" width="40%">
                                            <asp:Label ID="lblPassword" runat="server" Text="Password"></asp:Label></td>
                                        <td align="left">
                                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="140px" MaxLength="50"></asp:TextBox>
                                            <span style="color: #ff0000">*</span>
                                            <asp:RequiredFieldValidator ID="ValidatePassword" runat="server" ControlToValidate="txtPassword"
                                                ErrorMessage="Password" EnableClientScript="False"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="ValidateCompare" runat="server" ControlToCompare="txtPassword"
                                                ControlToValidate="txtPasswordConfirm" ErrorMessage="Passwords do not match."
                                                EnableClientScript="False"></asp:CompareValidator></td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="lblConfirmPassword" runat="server" Text="Confirm Password"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtPasswordConfirm" runat="server" TextMode="Password" Width="140px"
                                                MaxLength="50"></asp:TextBox><span style="color: #ff0000">*</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorConfirmPWD" runat="server"
                                                ControlToValidate="txtPasswordConfirm" ErrorMessage="Confirm Password" EnableClientScript="False"></asp:RequiredFieldValidator></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr height="20px">
            </tr>
            <tr height="25px">
                <td colspan="2" align="center">
                    <asp:Button ID="btnContinue" runat="server" Text="Save" ToolTip="Clicking 'Save' will save your profile" OnClick="btnContinue_Click" />
                    &nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" /></td>
            </tr>        
        </table>
    </asp:Panel>
</asp:Content>
