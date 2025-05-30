<%@ Page Language="C#" AutoEventWireup="true" Culture="en-GB" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/C#" runat="server">
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        this.CultureLabel.Text = "Page Culture: " + this.Culture + "; Page UI culture: " + this.UICulture;
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Calendar Manual Test: Work Item 7265 - Calendar Extender in non en-US locales</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <br />
            <asp:Label ID="CultureLabel" runat="server"></asp:Label>        
            <br />
            <br />
            <asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="true" EnableScriptLocalization="true" runat="server" />
            <asp:TextBox runat="server" ID="Text" Text="4/12/2006" />
            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="Text" />
            <%=System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern %>
            <ol>
                <li>Set focus to the "TextBox"</li>
                <ul>
                    <li>Calendar should appear on December 4th, 2006</li>
                </ul>
                <li>Change date to December 5th, 2006</li>
                <ul>
                    <li>Textbox should say 5/12/2006</li>
                </ul>
            </ol>
        </div>
    </form>
</body>
</html>
