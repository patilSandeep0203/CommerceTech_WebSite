<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod]
    public static string[] GetCompletionList(string prefixText, int count)
    {
        return "this is sample data".Split(' ');
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AutoCompleteInUpdatePanel</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        Click "Submit" to test. A script error indicates failure.
        <br />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:TextBox ID="TextBox1" runat="server" />
                <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="TextBox1"
                    ServiceMethod="GetCompletionList" />
                <br />
                <asp:TextBox ID="TextBox2" runat="server" />
                <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="TextBox2"
                    ServiceMethod="GetCompletionList" />
                <br />
                <asp:Button ID="Button1" runat="server" Text="Submit" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
