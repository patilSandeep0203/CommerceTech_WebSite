<%@ Page Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="DefaultError.aspx.cs"
    Inherits="DefaultError" Title="Commerce Technologies - Online Application" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentHolder" runat="Server">
    <br />
    <br />
    <br />
    <br />
    <br />
    <asp:Label ID="lblError" runat="server" BackColor="#FFC0C0" BorderColor="Red" BorderStyle="Solid" Font-Names="Arial" Font-Bold="true"
        BorderWidth="1px" Font-Size="Medium" ForeColor="Black">There was an error processing your request. Please contact Technical Support: <a href=mailto:support@commercetech.com?Subject=Online App Error, App ID: " + Session["AppId"].ToString() + "> support@commercetech.com. </a>.</asp:Label>
    <br />
    <br />
    <br />
    <br />
    <br />
</asp:Content>
