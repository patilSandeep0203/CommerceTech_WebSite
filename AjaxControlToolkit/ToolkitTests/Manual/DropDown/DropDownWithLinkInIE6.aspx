<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    private void LB1_Click(object sender, EventArgs e)
    {
        L1.Text = "Clicked: " + DateTime.Now.ToString();
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="SM1" runat="Server"  />
        <ajaxToolkit:DropDownExtender ID="DDE1" runat="server" TargetControlID="LB1" DropDownControlID="Panel1" />
        <br />
        <asp:LinkButton ID="LB1" runat="server" Text="Click Here" Style="padding-right:17px;" OnClick="LB1_Click" />
        <asp:Panel ID="Panel1" runat="server" Width="125px" BorderColor="black" BorderWidth="1px" BorderStyle="Solid">
            This<br />
            Is<br />
            A<br />
            Test
        </asp:Panel>
        <asp:Label runat="server" ID="L1" />
    </div>
    </form>
</body>
</html>
