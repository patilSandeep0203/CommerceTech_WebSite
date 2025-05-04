<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="locked.aspx.cs" Inherits="locked" Title="Commerce Technologies - Online Application" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentHolder" Runat="Server">
    <asp:ScriptManager ID="ScriptManagerLocked" runat="server">
    </asp:ScriptManager>
    <br />
    <br />
    <br />
    <br />
    <br />
    <div align="center" class="DivGreen">
        <asp:Label ID="lblStatusHeader" runat="server" CssClass="LabelsHeaderLarge">Merchant Account Status: </asp:Label>
        <asp:Label ID="lblStatus" runat="server" Font-Bold="true" CssClass="LabelsHeaderLarge"></asp:Label><br />
        <asp:Label ID="lblStatusGWHeader" runat="server" CssClass="LabelsHeaderLarge">Payment Gateway Status: </asp:Label>
        <asp:Label ID="lblStatusGW" runat="server" Font-Bold="true" CssClass="LabelsHeaderLarge"></asp:Label>
    </div>        
    <br />
    <br />
    <br />
    <asp:Panel ID="pnlMessage" BorderColor="gray" BorderWidth="1px" runat="server" BackColor="#ffffce" Font-Bold="True"
        Width="99%">
        <span class="LabelsBody"> The status of your application prevents it from being edited. If you have any questions,
        please contact Commerce Technologies at 1-800-477-5363 or Email
        <br />
        <asp:HyperLink ID="lnkEmail" CssClass="Link" runat="server" NavigateUrl="mailto:service@commercetech.com">service@commercetech.com</asp:HyperLink></span></asp:Panel>
    <br />
    <br />
</asp:Content>

