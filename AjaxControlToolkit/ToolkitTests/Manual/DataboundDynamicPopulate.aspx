<%@ Page Language="C#" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit"
  TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
  "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
  // (c) Copyright Microsoft Corporation.
  // This source is subject to the Microsoft Permissive License.
  // See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
  // All other rights reserved.
  [System.Web.Services.WebMethod]
  [System.Web.Script.Services.ScriptMethod]
  public static string GetContent(string contextKey)
  {
    // Create a random color
    string color = (new Random()).Next(0xffffff).ToString("x6");
    // Use the style specified by the page author
    string style = contextKey;
    // Display the current time
    string time = DateTime.Now.ToLongTimeString();
    // Compose the content to return
    return "<span style='color:#" + color + "; " + style + "'>" + time + "</span> ";
  }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>Dynamic ModalPopup Demonstration</title>

  <%-- Style the page so it looks pretty --%>
  <style type="text/css">
    body { font-family:Verdana;     font-size:10pt; }
    .background { background-color:Gray; }
    .popup { width:200px;             padding:10px;        background-color:White;
             border-style:solid;      border-color:Black;  border-width:2px;
             vertical-align: middle;  text-align:center; }
  </style>
</head>
<body>
  <form id="form1" runat="server">

    <%-- Atlas pages need a ScriptManager --%>
    <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <%-- A very simple data source to drive the demonstration --%>
    <asp:XmlDataSource ID="XmlDataSource1" runat="server">
      <Data>
        <items>
          <item style="font-weight:bold" />
          <item style="font-style:italic" />
          <item style="text-decoration:underline" />
        </items>
      </Data>
    </asp:XmlDataSource>

    <%-- A simple list of all the data items available --%>
    <asp:DataList ID="DataList1" runat="server" DataSourceID="XmlDataSource1">

      <HeaderTemplate>
        How would you like your dynamic content styled?
      </HeaderTemplate>

      <ItemTemplate>
        &bull; <asp:LinkButton ID="LinkButton" runat="server" Text='<%# Eval("style") %>' />

        <%-- The ModalPopupExtender, popping up Panel1 and dynamically populating Label1 --%>
        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender" runat="server"
          TargetControlID="LinkButton" PopupControlID="Panel1" OkControlID="Button1"
          BackgroundCssClass="background" DynamicControlID="Label1"
          DynamicContextKey='<%# Eval("style") %>' DynamicServiceMethod="GetContent" />
      </ItemTemplate>

    </asp:DataList>

    <%-- All ModalPopups share the same popup --%>
    <asp:Panel ID="Panel1" runat="server" CssClass="popup" style="display:none;">
      <p>This popup popped at <asp:Label ID="Label1" runat="server" /> and all was well.</p>
      <p><asp:Button ID="Button1" runat="server" Text="OK" /></p>
    </asp:Panel>

  </form>
</body>
</html>