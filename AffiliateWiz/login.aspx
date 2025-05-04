<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login"
    Title="Partner Portal" Theme="AppTheme" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Partner Portal</title>
</head>
<body>
    <center>
        <form id="form1" runat="server">
            <div>
                <br />
                <br />
                <asp:Panel ID="pnlMainPage" runat="server">
                    <table border="0" width="500px" cellpadding="0" cellspacing="0">
                        <tr>
                            <td colspan="1" style="height: 20px">
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="1">
                                <table style="width: 90%; border-right: silver 1px solid; border-top: silver 1px solid;
                                    border-left: silver 1px solid; border-bottom: silver 1px solid; background-color: #f5f5f5"
                                    border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="center">
                                            <strong><span style="font-size: Medium; color: #064787; font-family: Arial">Welcome
                                                to the Commerce Technologies Portal.</span></strong> <span style="font-size: smaller;
                                                    color: #383838; font-family: Arial">
                                                    <p style="text-align: center">
                                                        <asp:Label ID="lblWelcome" runat="server" Font-Bold="True" Font-Names="Arial" Text="Please enter your Partner Portal Login Name and Password."></asp:Label></p>
                                                </span>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="1">
                                &nbsp;<asp:Label ID="lblError" runat="server" BackColor="Red" Font-Size="Medium"
                                    ForeColor="White" Visible="False"></asp:Label>&nbsp;<br />
                                <asp:ValidationSummary ID="ValidateSummary" runat="server" BackColor="#FFC0C0" BorderColor="red"
                                    BorderWidth="1px" ForeColor="Black" HeaderText="Please check the fields marked in red."
                                    Width="250px" Font-Names="Arial" Font-Size="Small" />
                                <br />
                                <asp:Label ID="lblNewSignup" runat="server" Font-Bold="True" Font-Names="Arial" ForeColor="SeaGreen"
                                    Text="Please close the browser to ensure logout." Visible="False"></asp:Label><br />
                                <asp:Label ID="lblLogoutTrue" runat="server" Font-Bold="True" Font-Names="Arial"
                                    ForeColor="SeaGreen" Text="Please close the browser to ensure logout." Visible="False"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 20px">
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="1">
                                <asp:Panel ID="pnlLogin" runat="server" Width="100%">
                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 60%; height: 200px;
                                        border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid;
                                        border-bottom: silver 1px solid">
                                        <tr>
                                            <td colspan="3" align="center" style="height: 20px; background-image: url(Images/topMain.gif);"
                                                valign="middle">
                                                <strong><span style="color: #ffffff; font-family: Arial;">Login Information</span></strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" valign="middle">
                                                <table border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td align="center" colspan="2" style="height: 44px;" valign="middle">
                                                            <strong><span style="color: #194390">
                                                                <asp:Label ID="lblHeader" Font-Names="Arial" Font-Size="small" runat="server" Text="Please provide your Partner Portal Login Name and Password"></asp:Label></span></strong></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" colspan="2" valign="middle">
                                                            <span style="color: #FF0000; font-family: Arial; font-size:8pt"><b>* - denotes a required field</b></span></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="height: 25px; width: 30%" valign="middle">
                                                            <asp:Label ID="lblLogin" runat="server" Text="Login Name" Font-Names="Arial" Font-Size="Small"></asp:Label></td>
                                                        <td align="left" style="height: 25px">
                                                            <asp:TextBox ID="txtLogin" runat="server" Width="120px" MaxLength="50"></asp:TextBox><span
                                                                style="color: #ff0000">* </span>
                                                            <asp:RequiredFieldValidator ID="ValidateEmailReqd" runat="server" ControlToValidate="txtLogin"
                                                                ErrorMessage="Login Name" EnableClientScript="False" Font-Names="Arial" Font-Size="Small"></asp:RequiredFieldValidator></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="height: 25px" valign="middle">
                                                            <asp:Label ID="lblPassword" runat="server" Text="Password" Font-Names="Arial" Font-Size="Small"></asp:Label></td>
                                                        <td align="left" style="height: 25px">
                                                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="120px" MaxLength="50"></asp:TextBox><span
                                                                style="color: #ff0000">*</span>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPassword"
                                                                ErrorMessage="Password" EnableClientScript="False" Font-Names="Arial" Font-Size="Small"></asp:RequiredFieldValidator></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" colspan="2">
                                                            &nbsp;<asp:Button ID="btnLogin" runat="server" OnClick="btnLogin_Click" Text="Login" /></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                        </tr>
                                        <tr>
                                        </tr>
                                        <tr>
                                            <td colspan="3" align="center" style="height: 20px; background-image: url(Images/topMain.gif)">
                                                <span style="font-family: Arial; font-size: x-small; color: White">Copyright © 2006
                                                    Commerce Technologies.</span>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
        </form>
    </center>
</body>
</html>
