<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SiteMap.aspx.cs" Inherits="SiteMap" 
    Title="Credit Card Processing, Merchant Services, Payment Processing" Theme="Admin"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <body>
        <asp:SiteMapDataSource id="nav1" runat="server" />
            <form id="Form1" runat="server">
            <asp:TreeView runat="server" DataSourceId="nav1" />
            </form>
    </body>
</html>