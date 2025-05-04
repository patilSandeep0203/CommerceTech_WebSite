<%@ Page Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="default.aspx.cs"
    Inherits="start" Title="Commerce Technologies - Online Application" Theme="TextBoxSkin" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentHolder" runat="Server">
    <asp:Panel ID="pnlMainPage" runat="server">
        <table border="0" width="90%" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="1" style="height: 20px">
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <cc1:PopupControlExtender ID="PopupControlExtender1" runat="server" PopupControlID="pnlEmailHelp"
                        TargetControlID="imgLogin" Position="Bottom" />
                </td>
            </tr>
            <tr>
                <td align="center" colspan="1">
                    <table style="width: 60%" border="0" cellpadding="0" cellspacing="0" class="DivGreen">
                        <tr>
                            <td>
                                <ul>
                                    <li style="text-align: left"><span class="LabelsBody">Please provide the following information
                                        to log in to your account.</span></li>
                                    <li style="text-align: left"><span class="LabelsBody">If you are beginning a new application,
                                        click on the signup link below. </span></li>
                                    <li style="text-align: left"><span class="LabelsBody">Your application will be automatically
                                        saved when you click on the Submit button and you may choose to modify your application
                                        at anytime.</span></li>
                                </ul>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="1">
                    <asp:Label ID="lblError" runat="server" BackColor="Red" Font-Size="Medium" ForeColor="White"
                        Visible="False"></asp:Label>
                    <asp:ValidationSummary ID="ValidateSummary" runat="server" Visible="false" ForeColor="Black" HeaderText="Please check the fields marked in red." Width="250px" />
                </td>
            </tr>
            <tr>
                <td style="height: 20px">
                </td>
            </tr>
            <tr>
                <td align="center" height="140px">
                    <table border="0" cellpadding="2" cellspacing="0" style="width: 50%;" class="Border">
                        <tr>
                            <td colspan="2" align="center" valign="middle" class="DivHeader">                                
                                <span class="MenuHeader"><strong>Login Information</strong></span></td>
                        </tr>
                        <tr height="20px"></tr>
                        <tr>
                            <td align="right" width="30%" valign="bottom">
                                <asp:Label ID="lblLogin" runat="server" CssClass="LabelsBodySmall" Text="Login Name"/></td>
                            <td align="left" valign="middle">
                                <asp:TextBox ID="txtLoginName" runat="server" Width="120px" MaxLength="50" TabIndex="1"></asp:TextBox>
                                <span class="LabelsRedLarge">*</span>
                                <asp:Image ID="imgLogin" runat="server" ImageUrl="Images/help.gif"  />
                                <asp:RequiredFieldValidator ID="ValidateLoginReqd" runat="server" ControlToValidate="txtLoginName"
                                    ErrorMessage="Login Name" EnableClientScript="False"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td align="right" valign="bottom">
                                <asp:Label ID="lblPassword" runat="server" CssClass="LabelsBodySmall" Text="Password" /></td>
                            <td align="left" valign="middle">
                                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="120px" MaxLength="50"
                                    TabIndex="2"></asp:TextBox>
                                <span class="LabelsRedLarge">*</span>
                                <asp:RequiredFieldValidator ID="ValidatePassword" runat="server" ControlToValidate="txtPassword"
                                    ErrorMessage="Password" EnableClientScript="False"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2" valign="middle">
                                <span class="LabelsRed"><b>*</b> - denotes a required field</span></td>
                        </tr>
                        <tr >
                            <td align="center" colspan="2" >
                                <asp:Button ID="btnLogin" runat="server" OnClick="btnLogin_Click" Text="Login" TabIndex="3" />
                                <asp:Panel ID="pnlEmailHelp" runat="server" Width="250px" CssClass="DivHelp">
                                    Please log in using the Login Name and Password you entered when you registered for the Online Application.
                                </asp:Panel></td>
                        </tr>
                        <tr height="25px" >
                            <td align="center" colspan="2" valign="bottom">
                                 <span class="LabelsBodySmall">If you don't remember your login/password please <a class="LinkSmall" href="https://www.apps.commercetech.com/Affiliatewiz/contactus.aspx" target="_blank">contact us</a>.</span></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr height="30px">
            </tr>
            <tr>
                <td align="center">
                    <div style="width: 30%" class="DivGreen">
                        <span class="LabelsHeader"><strong>Not registered yet?</strong></span>
                        <br />
                        <br />
                        <asp:Button ID="btnSignUp" runat="server" Text="Sign Up" PostBackUrl="start.aspx"  />
                        <!--<a href="start.aspx" onmouseover="document.signup.src='Images/Signup_mouseover.gif'"
                            onmouseout="document.signup.src='Images/Signup.gif'">
                            <img name="signup" alt="" src="Images/Signup.gif" style="border: 0" width="80" height="20" /></a>-->
                    </div>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
