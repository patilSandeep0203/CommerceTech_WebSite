<%@ Page Language="C#" AutoEventWireup="true" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Calendar Manual Test: Work Item 4814 - Calendar does not work with Modal Popup</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager runat="server" />
        <asp:Panel runat="server" ID="Panel" Style="height:100px;width:200px;background-color:#e9e9e9;border:1px solid black;">        
            <asp:TextBox runat="server" ID="Text" />
            <ajaxToolkit:CalendarExtender runat="server" TargetControlID="Text" />        
        </asp:Panel>
        <asp:Button runat="Server" ID="Button" Text="Show Popup" />
        <ajaxToolkit:ModalPopupExtender runat="server" ID="Modal" TargetControlID="Button" PopupControlID="Panel" />

        <p>This manual test addresses Work Item 4814 "Calendar Extender in Modal Popup"</p>        
        <ol>
            <li>Click "Show Popup" to show the popup</li>
            <li>Set focus to the "TextBox"</li>
        </ol>
        Expected Behavior
        <ul>
            <li>Calendar appears above modal popup</li>
        </ul>
    
    </div>
    </form>
</body>
</html>
