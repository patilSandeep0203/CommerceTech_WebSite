<%@ Page Language="C#" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        if (!IsPostBack)
        {
            ModalPopupExtender1.Show();
        }
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        ModalPopupExtender1.Show();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        ModalPopupExtender2.Show();
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Server Side Modal Popup</title>
    <link href="../Default.css" type="text/css" rel="stylesheet" />
</head>
<body><form id="Form1" runat="server"><div>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" />
    
    <asp:LinkButton ID="LinkButton1" runat="server">ModalPopup</asp:LinkButton>
    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="LinkButton1" PopupControlID="Panel1" CancelControlID="Button1" BackgroundCssClass="watermarked" />
    <asp:Panel ID="Panel1" runat="server" Height="50px" Width="125px" style="background-color:Lime">
        PANEL
        <asp:Button ID="Button1" runat="server" Text="Cancel" />
    </asp:Panel>
    <br />
    <asp:Button ID="Button4" runat="server" Text="Postback" />
    <asp:Button ID="Button5" runat="server" Text="Server Show" OnClick="Button5_Click" />
    <br />
    <br />
    
    
    
    <asp:Panel ID="Panel2" runat="server" Height="50px" Width="125px" style="background-color:MistyRose">
        PANEL
        <asp:Button ID="Button6" runat="server" Text="Cancel" />
    </asp:Panel>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:LinkButton ID="LinkButton2" runat="server">Async ModalPopup</asp:LinkButton>
            <br />
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="LinkButton2" PopupControlID="Panel2" CancelControlID="Button6" BackgroundCssClass="watermarked" />
            
            <asp:Button ID="Button2" runat="server" Text="Async Postback" />
            <asp:Button ID="Button3" runat="server" Text="Async Server Show" OnClick="Button3_Click" />
        </ContentTemplate>
    </asp:UpdatePanel>
</div></form></body>
</html>