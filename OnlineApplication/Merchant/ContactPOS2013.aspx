<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="ContactPOS2013.aspx.cs"
    Inherits="Merchant_ContactPOS2013" Title="Commerce Technologies - Online Application"
    Theme="TextBoxSkin" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentHolder" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table border="0" cellpadding="2" cellspacing="0" width="100%" class="DivGreen">

        <tr>
            <td colspan="3" align="center" class="DivHeader2" height="26px" >
                <b>
                <asp:Label ID="lblHeading" runat="server" text-color="white" CssClass="MenuHeader" Text="Merchant Account / Payment Gateway Signup Information">
                </asp:Label></b>
            </td>
        </tr>
        <tr>
            <td align="center" valign="middle">
                <asp:Label ID="lblError" runat="server" BackColor="Red" Font-Size="Medium" ForeColor="White"
                    Visible="False"></asp:Label></td>
        </tr>
        <tr height="50px"></tr>
        <tr>
            <td align="center">
            <br/>
                <b><span class="LabelsBody">Please contact your consultant to complete application.<br/><br/><br> </span></b>
            </td>

            
        </tr>
        <tr height="20px"></tr>
    </table>
</asp:Content>
