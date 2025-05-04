<%@ Page Language="c#" AutoEventWireup="false" CodeFile="UploadDocuSignDocs.aspx.cs" Inherits="netimageupload.uploadfiles"
    Title="Commerce Technologies - Partner Portal" Theme="TextBoxSkin" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<script language="javascript" type="text/javascript">
//<![CDATA[
var cot_loc0=(window.location.protocol == "https:")? "https://secure.comodo.net/trustlogo/javascript/cot.js" :
"http://www.trustlogo.com/trustlogo/javascript/cot.js";
document.writeln('<scr' + 'ipt language="JavaScript" src="'+cot_loc0+'" type="text\/javascript">' + '<\/scr' + 'ipt>');
//]]>
</script>
<head>
    <title>Commerce Technologies - Partner Portal</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio 7.0">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="PartnerCSS.css" type="text/css" rel="stylesheet" />
</head>
<a href="http://www.instantssl.com" id="comodoTL">SSL</a>

<script language="JavaScript" type="text/javascript">
COT("https://www.apps.commercetech.com/images/secure_site.gif", "SC2", "none");
</script>

<body>
    <form id="Form1" method="post" runat="server" enctype="multipart/form-data">
        <table style="width: 600px" class="SilverBorder" align="center">
            <tr>
                <td align="center" style="background-image: url(Images/topMain.gif); height: 25px" colspan="2">
                    <span class="MenuHeader"><b>Upload Attachment</b></span>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">                    
                    <asp:Label CssClass="LabelsError" ID="lblOutput" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:Label ID="Label1" Text="Supported file format: pdf, jpg, doc, docx, txt, tif, png, xls, xlsx." runat="server"></asp:Label>
                </td>
            </tr>
            <tr></tr>
            <tr>
                <td align="right"; style="width:45%">
                    <asp:Label ID="lblFileName" Text="Voided or Cancelled Check pre-printed with your business name" runat="server"></asp:Label>
                </td>
                <td style="width:55%">
                    <input id="filUpload" type="file" name="filUpload" runat="server"/>
                </td>
            </tr>
            <tr></tr>
            <tr>
                <td align="right"; style="width:45%">
                    <asp:Label ID="Label2" Text="Business license, phone or utility bill or previous processing statement" runat="server"></asp:Label>
                </td>
                <td style="width:55%">
                    <input id="filUpload2" type="file" name="filUpload2" runat="server"/>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="btnUpload" runat="server" Text="Upload" Width="90px" Height="25px"></asp:Button>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>