<%@ Page Language="C#" AutoEventWireup="true"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Collapsible Panel Extender in Horizontal mode test</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="Server" ID="ScriptMgr1">
        </asp:ScriptManager>
        This is a collapsible panel example that uses fixed ExpandedSide(400px) and Horizontal expand direction with dashed border set. 
        <br /><em>Note: There is a style issue with FF(not control related) where the bottom border does not show up.</em>
        <br /> Click on the black bar to collapse and expand.<br /><br />
        <asp:Panel runat="server">
            <asp:Panel ID="Panel2" runat="server" CssClass="collapsePanelHeader" 
                BackColor="Black" Width="30px" style="position:absolute;left:0px;height:400px">
            </asp:Panel>
            <asp:Panel style="position:absolute;left:30px;border: 1px dashed #2F4F4F;background-color:chocolate;"
             ID="Panel1" runat="server" >
                <asp:Panel runat="Server" Height="398" >
                    <br />
                    <p>
                        blah blahblah blahblah blahblah blahblah blahblah blahblah blah blah blahblah blahblah
                        blahblah blahblah blahblah blahblah blah blah blahblah blahblah blahblah blahblah
                        blahblah blahblah blah blah blahblah blahblah blahblah blahblah blahblah blahblah
                        blah blah blahblah blahblah blahblah blahblah blahblah blahblah blah blah blahblah
                        blahblah blahblah blahblah blahblah blahblah blah
                        blah blahblah blahblah blahblah blahblah blahblah blahblah blah blah blahblah blahblah
                        blahblah blahblah blahblah blahblah blah blah blahblah blahblah blahblah blahblah
                        blahblah blahblah blah blah blahblah blahblah blahblah blahblah blahblah blahblah
                        blah blah blahblah blahblah blahblah blahblah blahblah blahblah blah blah blahblah
                        blahblah blahblah blahblah blahblah blahblah blah
                        blah blahblah blahblah blahblah blahblah blahblah blahblah blah blah blahblah blahblah
                        blahblah blahblah blahblah blahblah blah blah blahblah blahblah blahblah blahblah
                        blahblah blahblah blah blah blahblah blahblah blahblah blahblah blahblah blahblah
                        blah blah blahblah blahblah blahblah blahblah blahblah blahblah blah blah blahblah
                        blahblah blahblah blahblah blahblah blahblah blah
                    </p>
                </asp:Panel>
            </asp:Panel>
            <ajaxToolkit:CollapsiblePanelExtender runat="server" 
                ID="collapsiblePanel1"
                ExpandDirection="Horizontal" 
                TargetControlID="Panel1" 
                ExpandControlID="Panel2" 
                CollapseControlID="Panel2"
                Collapsed="False" 
                ExpandedSize="400" />
        </asp:Panel>
    </form>
</body>
</html>
