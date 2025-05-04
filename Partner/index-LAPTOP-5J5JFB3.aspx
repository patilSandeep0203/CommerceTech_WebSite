<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="index"
    Title="Commerce Technologies - Partner Portal" Theme="AppTheme"  %> 
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<!--<script language="javascript" type="text/javascript">
//<![CDATA[
var cot_loc0=(window.location.protocol == "https:")? "https://secure.comodo.net/trustlogo/javascript/cot.js" :
"http://www.trustlogo.com/trustlogo/javascript/cot.js";
document.writeln('<scr' + 'ipt language="JavaScript" src="'+cot_loc0+'" type="text\/javascript">' + '<\/scr' + 'ipt>');
//]]>
</script>-->



<script language="javascript" type="text/javascript">
//<![CDATA[
    var cot_loc0 = (window.location.protocol == "https:") ? "https://secure.comodo.net/trustlogo/javascript/cot.js" :
"http://www.trustlogo.com/trustlogo/javascript/cot.js";
    document.writeln('<scr' + 'ipt language="JavaScript" src="' + cot_loc0 + '" type="text\/javascript">' + '<\/scr' + 'ipt>');
//]]>
</script>

<head id="Head1" runat="server">
    <title>Commerce Technologies - Partner Portal</title>
    <script type="text/javascript">

      var _gaq = _gaq || [];
      _gaq.push(['_setAccount', 'UA-22103620-2']);
      _gaq.push(['_trackPageview']);

      (function() {
        var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
        ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
        var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
      })();

    </script>

    <meta name="viewport" content="width=device-width, initial-scale=1"/>

</head>
<!--<a href="http://www.instantssl.com" id="comodoTL">SSL</a>-->
<!--<script language="JavaScript" type="text/javascript">
    COT("https://ssl.comodo.com/images/seals/sectigo_trust_seal_sm_2x.png", "SC2", "none");
</script>-->


<script type="text/javascript"> //<![CDATA[
    var tlJsHost = ((window.location.protocol == "https:") ? "https://secure.trust-provider.com/" : "http://www.trustlogo.com/");
    document.write(unescape("%3Cscript src='" + tlJsHost + "trustlogo/javascript/trustlogo.js' type='text/javascript'%3E%3C/script%3E"));
//]]></script>
<script language="JavaScript" type="text/javascript">
    TrustLogo("https://sectigo.com/images/seals/sectigo_trust_seal_md_2x.png", "SECDV", "none");
</script>

<body>
    <center>
        <form id="form1" runat="server">
            <div>
                <br />
                <br />
                <asp:Panel ID="pnlMainPage" runat="server">
                    <table border="0" width="500px" cellpadding="0" cellspacing="0">
                        <tr>
                            <td colspan="1" style="height: 20px">
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="1">
                                <table style="width: 90%; border-right: silver 1px solid; border-top: silver 1px solid;
                                    border-left: silver 1px solid; border-bottom: silver 1px solid; background-color: #f5f5f5"
                                    border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="center">
                                            <strong><span style="font-size: 16 px; color: #064787; font-family: Arial"></span></strong> <span style="font-size: 16 px;
                                                    color: #383838; font-family: Arial">
                                                   
                                                        <asp:Label ID="lblWelcome" runat="server" Font-Bold="True" Font-Names="Arial" Text=""></asp:Label>
                                                </span>
                                            
                                        </td>
                                        <img src="https://cdn11.bigcommerce.com/s-1x33si/images/stencil/original/image-manager/comtech.png?t=1666388176&_gl=1*a0teuj*_ga*MTAxMTMyODQ4LjE2NTQyODg3NzE.*_ga_WS2VZYPC6G*MTY2NjM4ODAyNi4yNDQuMS4xNjY2Mzg4MTgyLjU3LjAuMA..">
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="1">
                                &nbsp;<asp:Label ID="lblError" runat="server" BackColor="Red" Font-Size="16 px"
                                    ForeColor="White" Visible="False"></asp:Label>&nbsp;<br />
                                <asp:ValidationSummary ID="ValidateSummary" runat="server" BackColor="#FFC0C0" BorderColor="red"
                                    BorderWidth="1px" ForeColor="Black" HeaderText="Please check the fields marked in red."
                                    Width="250px" Font-Names="Arial" Font-Size="16 px" />
                                <br />
                                <asp:Label ID="lblNewSignup" runat="server" Font-Bold="True" Font-Names="Arial" ForeColor="SeaGreen"
                                    Text="Please close the browser to ensure logout." Visible="False"></asp:Label><br />
                                <asp:Label ID="lblLogoutTrue" runat="server" Font-Bold="True" Font-Names="Arial"
                                    ForeColor="SeaGreen" Text="Please close the browser to ensure logout." Visible="False"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 20px">
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="1">
                                <asp:Panel ID="pnlLogin" runat="server" Width="100%">
                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 60%; height: 200px;
                                        border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid;
                                        border-bottom: silver 1px solid">
                                        <tr>
                                            <td colspan="3" align="center" style="height: 20px; background-color: #565294;;"
                                                valign="middle">
                                                <strong><span style="color: #ffffff; font-family: Arial;">Login Information</span></strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" valign="middle">
                                                <table border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td align="center" colspan="2" style="height: 44px;" valign="middle">
                                                            <strong><span style="color: #194390">
                                                                <asp:Label ID="lblHeader" Font-Names="Arial" Font-Size="16 px" runat="server" Text="Please provide your Portal Login Name and Password"></asp:Label></span></strong></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" colspan="2" valign="middle">
                                                            <span style="color: #FF0000; font-family: Arial; font-size:16 px"><b>* - denotes a required field</b></span></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="height: 25px; width: 30%" valign="middle">
                                                            <asp:Label ID="lblLogin" runat="server" Text="Login Name" Font-Names="Arial" Font-Size="16 px"></asp:Label></td>
                                                        <td align="left" style="height: 25px">&nbsp;
                                                            <asp:TextBox ID="txtLogin" runat="server" Width="120px" MaxLength="50"></asp:TextBox><span
                                                                style="color: #ff0000">* </span>
                                                            <asp:RequiredFieldValidator ID="ValidateEmailReqd" runat="server" ControlToValidate="txtLogin"
                                                                ErrorMessage="Login Name" EnableClientScript="False" Font-Names="Arial" Font-Size="16 px"></asp:RequiredFieldValidator></td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td valign=top align=left><span style="font-family: Arial; font-size:16 px;">
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        <asp:HyperLink runat="server" NavigateUrl="https://commercetech.com/contact-us/" Text="Forgot Login Name?"></asp:HyperLink>
                                                        </span></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="height: 25px" valign="middle">
                                                            <asp:Label ID="lblPassword" runat="server" Text="Password" Font-Names="Arial" Font-Size="16 px"></asp:Label></td>
                                                        <td align="left" style="height: 25px">&nbsp;
                                                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="120px" MaxLength="50"></asp:TextBox><span
                                                                style="color: #ff0000">*</span>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPassword"
                                                                ErrorMessage="Password" EnableClientScript="False" Font-Names="Arial" Font-Size="16 px"></asp:RequiredFieldValidator></td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td valign=top align=left><span style="font-family: Arial; font-size:16 px;">
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        <asp:HyperLink runat="server" NavigateUrl="PortalForgotPassword.aspx" Text="Forgot Password?"></asp:HyperLink>
                                                        </span></td>
                                                    </tr>
                                                    <asp:Panel runat=server id=pnlPassphrase visible=false>
                                                    <tr>                                                       
                                                            <td align="right" style="height: 20px" valign=baseline>
                                                                <asp:Label ID="lblPassphrase" runat="server" Text="Retrieve password?" Font-Names="Arial" Font-Size="16 px"></asp:Label></td>
                                                            <td align="left" style="height: 20px" valign="middle">&nbsp;
                                                               <asp:Button ID="btnRestPasswordYes" runat="server" OnClick="btnRestPasswordYes_Click" Text="Yes" />&nbsp;<asp:Button ID="btnRestPasswordNo" runat="server" OnClick="btnRestPasswordNo_Click" Text="No" />
                                                               </td>
                                                    </tr>
                                                    </asp:Panel>
                                                     <asp:Panel runat=server id=pnlResetPasswordEmail visible=false>
                                                    <tr>                                                       
                                                            <td align="right" style="height: 20px" valign=baseline>
                                                                <asp:Label ID="Label1" runat="server" Text="Email" Font-Names="Arial" Font-Size="16 px"></asp:Label></td>
                                                            <td align="left" style="height: 20px" valign="middle">&nbsp;
                                                              <asp:TextBox ID="txtResetPasswordEmail" runat="server"  Width="120px" MaxLength="50"></asp:TextBox><asp:Button ID="btnSendPasswordtoEmail" runat="server" OnClick="btnSendPasswordtoEmail_Click" Text="Send" />
                                                               </td>
                                                    </tr>
                                                    </asp:Panel>
                                                    <tr height=30px>
                                                        <td valign=bottom align="center" colspan="2">
                                                            &nbsp;<asp:Button ID="btnLogin" runat="server" OnClick="btnLogin_Click" Text="Login" /></td>
                                                    </tr>
                                                    
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                        </tr>
                                        <tr>
                                        </tr>
                                        <tr>
                                            <td colspan="3" align="center" style="height: 20px; background-color: #565294;">
                                                <span style="font-family: Arial; font-size: x-small; color: White">
                                                    Commerce Technologies.</span>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
        </form>
    </center>
</body>
</html>
