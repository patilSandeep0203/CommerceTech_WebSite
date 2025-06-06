<%@ Page Language="C#" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">
    private void btnPostback1_Click(object sender, EventArgs args)
    {
        bool collapse = !bool.Parse(cpp1.ClientState);
        cpp1.Collapsed = collapse;
        cpp1.ClientState = collapse.ToString();
    }

    private void btnPostback2_Click(object sender, EventArgs args)
    {
        bool collapse = !bool.Parse(cpp2.ClientState);
        cpp2.Collapsed = collapse;
        cpp2.ClientState = collapse.ToString();
    }
</script>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>ClientState Manual Test</title>
</head>
<body><form id="Form1" runat="server"><div>
    Toggle the collapsed state in a postback by clicking the button and verify that it opens or closes.<br /><br />
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" />

    <asp:UpdatePanel ID="Update" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
        <ContentTemplate>
            UpdatePanel:<br />
            <asp:Button ID="btnPostback1" runat="server" OnClick="btnPostback1_Click" Text="Toggle" />
            <asp:Button ID="button1" runat="server" Text="Postback" />
            <asp:Panel ID="Panel1" runat="server">
                <div id="Header1">Header - <asp:Label ID="TextLabel1" runat="server">Initial content</asp:Label></div>
                <div id="panelParent1" style="border:2px red outset;">
                <asp:Panel ID="ContentPanel1" runat="server">
                    <p>This is sample content.<br />It spans a couple of lines.</p>
                </asp:Panel>
                </div>
            </asp:Panel>
            <ajaxToolkit:CollapsiblePanelExtender ID="cpp1" runat="Server"
                    TargetControlID="ContentPanel1"
                    ExpandControlID="Panel1"
                    CollapseControlID="Panel1"
                    ScrollContents="False"
                    Collapsed="False"
                    ExpandDirection="Vertical"
                    SuppressPostBack="true"
                    TextLabelID="TextLabel1"
                    CollapsedText="Collapsed"
                    ExpandedText="Opened" />
        </ContentTemplate>
    </asp:UpdatePanel>
    
    <br />
    <br />
    
    No UpdatePanel:<br />
    <asp:Button ID="btnPostback2" runat="server" OnClick="btnPostback2_Click" Text="Toggle" />
    <asp:Button ID="button2" runat="server" Text="Postback" />
    <asp:Panel ID="Panel2" runat="server">
        <div id="Header2">Header - <asp:Label ID="TextLabel2" runat="server">Initial content</asp:Label></div>
        <div id="panelParent2" style="border:2px red outset;">
        <asp:Panel ID="ContentPanel2" runat="server">
            <p>This is sample content.<br />It spans a couple of lines.</p>
        </asp:Panel>
        </div>
    </asp:Panel>
    <ajaxToolkit:CollapsiblePanelExtender ID="cpp2" runat="Server"
            TargetControlID="ContentPanel2"
            ExpandControlID="Panel2"
            CollapseControlID="Panel2"
            ScrollContents="False"
            Collapsed="False"
            ExpandDirection="Vertical"
            SuppressPostBack="true"
            TextLabelID="TextLabel2"
            CollapsedText="Collapsed"
            ExpandedText="Opened" />
    
    
</div></form></body>
</html>