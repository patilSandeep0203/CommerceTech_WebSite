<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="UpdateAcct.aspx.cs"
    Inherits="UpdateAcct" Title="Commerce Technologies - Online Application" Theme="TextBoxSkin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentHolder" runat="Server">
<asp:ScriptManager ID="ScriptManagerEdit" runat="server">
    </asp:ScriptManager>
    <table width="100%" class="DivGreen" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td align="center" colspan="2"  class="DivHeader2">
                <asp:Label ID="lblRegistrationHeader" runat="server" Font-Bold="True">Registration</asp:Label></td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Label ID="lblError" runat="server" BackColor="Red" Font-Size="Medium"
                    ForeColor="White" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="right" style="width: 40%" valign="middle">
                <asp:Label ID="lblFirst" runat="server" Text="First Name"></asp:Label></td>
            <td align="left" style="width: 75%" valign="top">
                &nbsp;<b><asp:Label ID="lblFirstNameValue" runat="server" Font-Bold="True"></asp:Label></b></td>
        </tr>
        <tr>
            <td align="right" valign="middle">
                <asp:Label ID="lblLast" runat="server" Text="Last Name"></asp:Label></td>
            <td align="left" valign="top">
                &nbsp;<b><asp:Label ID="lblLastNameValue" runat="server" Font-Bold="True"></asp:Label></b></td>
        </tr>
        <tr>
            <td align="right" valign="middle">
                <asp:Label ID="lblTitle" runat="server" Text="Title"></asp:Label></td>
            <td align="left" valign="top">
                <b>&nbsp;<asp:Label ID="lblTitleValue" runat="server" Font-Bold="True"></asp:Label></b></td>
        </tr>
        <tr>
            <td align="right" valign="top">
                <asp:Label ID="lblPhone" runat="server" Text="Business Phone"></asp:Label></td>
            <td align="left" valign="top">
                <b>&nbsp;<asp:Label ID="lblPhoneValue" runat="server" Font-Bold="True"></asp:Label></b>
                    <asp:Label ID="lblExt" runat="server" Text="Ext."></asp:Label><b>
                        <asp:Label ID="lblPhoneExtValue" runat="server" Font-Bold="True"></asp:Label></b>&nbsp;
            </td>
        </tr>
        <tr>
            <td align="right" valign="middle">
                <asp:Label ID="lblHomePhone" runat="server" Text="Home Phone"></asp:Label></td>
            <td align="left" valign="top">
                <b>&nbsp;<asp:Label ID="lblHomePhoneValue" runat="server" Font-Bold="True"></asp:Label></b></td>
        </tr>
        <tr>
            <td align="right" valign="middle">
                <asp:Label ID="lblMobile" runat="server" Text="Mobile Phone"></asp:Label></td>
            <td align="left" valign="top">
                &nbsp;<b><asp:Label ID="lblMobilePhoneValue" runat="server" Font-Bold="True"></asp:Label></b></td>
        </tr>
        <tr height="20px">
        </tr>
        <tr height="25px">
            <td align="center" colspan="2">
                <asp:Button ID="btnContinue" runat="server" Text="Continue" ToolTip="Click on 'Continue' to Sign up" OnClick="btnContinue_Click" />
                &nbsp;
                <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel" /></td>
        </tr>
        <tr>
            <td align="center" colspan="2">
            <asp:Label ID="lblFooter" runat="server" Text="Click on 'Continue' to Sign up"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
