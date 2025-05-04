<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Join.aspx.cs" Inherits="Join"
    Theme="AppTheme" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<script language="javascript" type="text/javascript">
//<![CDATA[
var cot_loc0=(window.location.protocol == "https:")? "https://secure.comodo.net/trustlogo/javascript/cot.js" :
"http://www.trustlogo.com/trustlogo/javascript/cot.js";
document.writeln('<scr' + 'ipt language="JavaScript" src="'+cot_loc0+'" type="text\/javascript">' + '<\/scr' + 'ipt>');
//]]>
</script>
<head id="Head1" runat="server">
    <title>Join us</title>
    <meta name="Description" content="Join the ecommerce affiliate program."/>
    <meta name="Keywords" content="join, merchant account, affiliate, agent, reseller, w-9 form, direct deposit" />
    <meta name="robots" content="index, follow" />
    <link type="text/css" href="styleAffWiz.css" rel="stylesheet" />
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

<script type="text/javascript" src='https://www.google.com/recaptcha/api.js'></script>
</head>

<a href="http://www.instantssl.com" id="comodoTL">SSL</a>
<script language="JavaScript" type="text/javascript">
COT("../images/secure_site.gif", "SC2", "none");
</script>
<body>
    <form id="form1" runat="server">
        <div>
            <center>
  
            <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>                
                <asp:Label ID="lblError" runat="server" Visible="False" ForeColor="White" Font-Size="Medium"
                    BackColor="Red"></asp:Label><br />
                <asp:ValidationSummary ID="ValidateSummary" runat="server" HeaderText="Please check the fields marked in red."
                    Width="250px" CssClass="DivHelp" Style="text-align:left"/>
                <span style="font-size: 10pt; font-family: Arial; color: #FF0000"><b>*</b> - denotes a required field<br />
                <b>**</b> - denotes a required field only for US Partners</span>
                <asp:Panel ID="pnlMain" runat="server" Width="550px">
                <table border="0" cellspacing="2" cellpadding="0" style="width: 550px" class="SilverBorder">
                    <tr>
                        <td align="center" style="height: 30px;" colspan="2" class="DivHeader">
                            <b>
                                <asp:Label ID="lblSignup" runat="server" Text="Affiliate "></asp:Label>
                                <asp:Label ID="lblType" runat="server" Text="Signup"></asp:Label></b>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 40%">
                            <span class="LabelsBodySmall">First Name</span></td>
                        <td align="left">
                            <asp:TextBox ID="txtFirstName" runat="server" Width="140px" TabIndex="1" MaxLength="100"></asp:TextBox>
                            <span class="LabelsRedLarge">*<asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                runat="server" ControlToValidate="txtFirstName" ErrorMessage="First Name" EnableClientScript="False"></asp:RequiredFieldValidator></span></td>
                    </tr>
                    <tr>
                        <td align="right">
                            <span class="LabelsBodySmall">Last Name</span></td>
                        <td align="left">
                            <asp:TextBox ID="txtLastName" runat="server" Width="140px" TabIndex="2" MaxLength="100"></asp:TextBox>
                            <span class="LabelsRedLarge">*<asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                                runat="server" ControlToValidate="txtLastName" EnableClientScript="False" ErrorMessage="Last Name"></asp:RequiredFieldValidator></span></td>
                    </tr>
                    <tr>
                        <td align="right">
                            <span class="LabelsBodySmall">Business Legal Name</span>
                            <asp:Label ID="lblLegalNameDesc" runat="server" Font-Size="Smaller" Text="(Full Personal Name if Sole Proprietorship)"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtCompanyName" runat="server" Width="140px" TabIndex="3" MaxLength="100"></asp:TextBox>
                            <span class="LabelsRedLarge">*<asp:RequiredFieldValidator ID="ValidatorCompanyName"
                                runat="server" ControlToValidate="txtCompanyName" EnableClientScript="False"
                                ErrorMessage="Company Name"></asp:RequiredFieldValidator></span></td>
                    </tr>
                    <tr>
                        <td align="right">
                            <span class="LabelsBodySmall">Business DBA (Doing Business As)</span></td>
                        <td align="left">
                            <asp:TextBox ID="txtDBAName" runat="server" Width="140px" TabIndex="4" MaxLength="100"></asp:TextBox>
                            <span class="LabelsRedLarge">*<asp:RequiredFieldValidator ID="RequiredFieldValidator4"
                                runat="server" ControlToValidate="txtDBAName" ErrorMessage="DBA" EnableClientScript="False"></asp:RequiredFieldValidator></span></td>
                    </tr>
                    <tr>
                        <td align="right">
                            <span class="LabelsBodySmall">Make Check Payable To</span></td>
                        <td align="left">
                            <asp:RadioButton ID="rdbLegalName" runat="server" GroupName="LegalDBA" Text="Legal Name"
                                TabIndex="5" />&nbsp;
                            <asp:Label ID="lblOR" runat="server" Text="or"></asp:Label>
                            <asp:RadioButton ID="rdbDBA" runat="server" GroupName="LegalDBA" Text="DBA Name"
                                TabIndex="6" />
                            <span class="LabelsRedLarge">*</span></td>
                    </tr>
                    <tr>
                        <td align="right" valign="top">
                            <span class="LabelsBodySmall">Legal Status</span></td>
                        <td align="left">
                            <asp:DropDownList ID="lstLegalStatus" runat="server" TabIndex="7">
                                <asp:ListItem>Corporation</asp:ListItem>
                                <asp:ListItem>Government</asp:ListItem>
                                <asp:ListItem>Sole Proprietorship</asp:ListItem>
                                <asp:ListItem>Legal/Medical Corp.</asp:ListItem>
                                <asp:ListItem>Int'l Org.</asp:ListItem>
                                <asp:ListItem>LLC</asp:ListItem>
                                <asp:ListItem>Non-Profit</asp:ListItem>
                                <asp:ListItem>Other</asp:ListItem>
                                <asp:ListItem>Partnership</asp:ListItem>
                                <asp:ListItem>Sole Proprietorship</asp:ListItem>
                                <asp:ListItem>Tax Exempt</asp:ListItem>
                                <asp:ListItem>Trust/State/Ass.</asp:ListItem>
                            </asp:DropDownList></td>                 
                    </tr>
                    <tr>
                        <td align="right" valign="top">
                            <asp:RadioButton ID="rdbTaxID" runat="server" GroupName="TaxSSN" Text="Federal Tax ID or" Checked="true"
                                TabIndex="8" /><asp:RadioButton ID="rdbSSN" runat="server" GroupName="TaxSSN" Text="Social Security Number" TabIndex="9" /></td>
                        <td align="left">
                            <asp:TextBox ID="txtTaxSSN" runat="server" Width="140px" MaxLength="15" TabIndex="10"></asp:TextBox>
                            <span class="LabelsRedLarge">**<asp:Label ID="lblTaxIDex" runat="server" Font-Size="Small"
                                ForeColor="Black" Text="ex. 111334444"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtTaxSSN"
                                    ErrorMessage="Tax ID or Social Security" EnableClientScript="False"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RESSN" runat="server" ControlToValidate="txtTaxSSN"
                                    EnableClientScript="False" ErrorMessage="Numbers Only" ValidationExpression="[0-9]*$"></asp:RegularExpressionValidator></span></td>
                    </tr>
                    <tr>
                        <td align="right">
                            <span class="LabelsBodySmall">Email</span></td>
                        <td align="left">
                            <asp:TextBox ID="txtEmail" runat="server" Width="140px" TabIndex="11" MaxLength="100"></asp:TextBox>
                            <span class="LabelsRedLarge">*<asp:RequiredFieldValidator ID="RequiredFieldValidator6"
                                runat="server" ControlToValidate="txtEmail" ErrorMessage="Email" EnableClientScript="False"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="ValidatorEmail" runat="server" ControlToValidate="txtEmail"
                                    EnableClientScript="False" ErrorMessage="Invalid Email Format" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></span></td>
                    </tr>
                    <asp:Panel ID="pnlReferral" Visible="false" runat="server">
                    <tr>
                        <td align="right" style="height: 10px">
                            <span class="LabelsBodySmall">Referral Source</span></td>
                        <td align="left" style="height: 10px">
                            <asp:DropDownList ID="lstReferral" runat="server" TabIndex="17" AutoPostBack="true" OnSelectedIndexChanged="lstReferral_SelectedIndexChanged" >
                            <asp:ListItem Value=""></asp:ListItem>
                            <asp:ListItem Value="Authorize.net"></asp:ListItem>
                            <asp:ListItem Value="Google"></asp:ListItem>
                            <asp:ListItem Value="Lotions & Lace"></asp:ListItem>                            
                            <asp:ListItem Value="Miva"></asp:ListItem>
                            <asp:ListItem Value="MSN"></asp:ListItem>
                            <asp:ListItem Value="storefront.net"></asp:ListItem>
                            <asp:ListItem Value="submitawebsite.com"></asp:ListItem>                            
                            <asp:ListItem Value="Success Systems"></asp:ListItem>                            
                            <asp:ListItem Value="Vitamin Power"></asp:ListItem>                            
                            <asp:ListItem Value="Yahoo"></asp:ListItem>
                            <asp:ListItem Value="Yellow Pages"></asp:ListItem>                            
                            <asp:ListItem Value="OTHER"></asp:ListItem>                            
                            </asp:DropDownList>
                          <span class="LabelsRedLarge">*<asp:RequiredFieldValidator ID="RFVReferral" Enabled="false"
                                runat="server" ControlToValidate="lstReferral" ErrorMessage="Referral" EnableClientScript="False">
                            </asp:RequiredFieldValidator></span>
                        </td>
                    </tr>
        
                     <tr>
                        <td align="right" style="height: 10px">
                            <span class="LabelsBodySmall">Specify</span></td>
                        <td align="left" style="height: 10px">
                             <asp:TextBox ID="txtSpecify" runat="server" Width="140px" TabIndex="11" MaxLength="100" Enabled="false"></asp:TextBox> 
                             <asp:RequiredFieldValidator ID="RFVSpecify" Enabled="false"
                                runat="server" ControlToValidate="txtSpecify" ErrorMessage="Specify Referral Source" EnableClientScript="False">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                       </asp:Panel>
                    <tr>
                        <td align="center" colspan="2" style="height: 10px">
                        <hr noshade size="1" width="90%">
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <span class="LabelsBodySmall"><b>Business Address (Must be a physical address, not a PO box)</b></span></td>
                    </tr>
                    <tr>
                        <td align="right">
                            <span class="LabelsBodySmall">Address</span></td>
                        <td align="left">
                            <asp:TextBox ID="txtAddress" runat="server" Width="140px" TabIndex="12" MaxLength="100"></asp:TextBox>
                            <span class="LabelsRedLarge">*
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7"
                                runat="server" ControlToValidate="txtAddress" ErrorMessage="Address" EnableClientScript="False">
                            </asp:RequiredFieldValidator>
                        </span></td>
                    </tr>
                    <tr>
                        <td align="right">
                            <span class="LabelsBodySmall">City</span></td>
                        <td align="left">
                            <asp:TextBox ID="txtCity" runat="server" Width="140px" TabIndex="13" MaxLength="100"></asp:TextBox>
                            <span class="LabelsRedLarge">*<asp:RequiredFieldValidator ID="RequiredFieldValidator8"
                                runat="server" ControlToValidate="txtCity" ErrorMessage="City" EnableClientScript="False"></asp:RequiredFieldValidator></span></td>
                    </tr>
                    <tr>
                        <td align="right">
                            <span class="LabelsBodySmall">State</span></td>
                        <td align="left">
                            <asp:DropDownList ID="lstState" runat="server" TabIndex="14">
                            </asp:DropDownList>
                            <span class="LabelsRedLarge">*</span></td>
                    </tr>
                    <tr>
                        <td align="right">
                            <span class="LabelsBodySmall">Region</span></td>
                        <td align="left">
                            <asp:TextBox ID="txtBusRegion" runat="server" Width="140px" TabIndex="15" MaxLength="100"></asp:TextBox>
                            <asp:Label ID="lblRegionEx" runat="server" Text="(If outside U.S.)"></asp:Label></td>
                    </tr>
                    <tr>
                        <td align="right">
                            <span class="LabelsBodySmall">Zip</span></td>
                        <td align="left">
                            <asp:TextBox ID="txtZip" runat="server" Width="140px" TabIndex="16" MaxLength="10"></asp:TextBox>
                            <span class="LabelsRedLarge">*<asp:RequiredFieldValidator ID="RequiredFieldValidator10"
                                runat="server" ControlToValidate="txtZip" ErrorMessage="Zip" EnableClientScript="False"></asp:RequiredFieldValidator></span></td>
                    </tr>
                    <tr>
                        <td align="right">
                            <span class="LabelsBodySmall">Country</span></td>
                        <td align="left">
                            <asp:DropDownList ID="lstCountry" runat="server" TabIndex="17" AutoPostBack="true" OnSelectedIndexChanged="lstCountry_SelectedIndexChanged">
                            </asp:DropDownList>
                            <span class="LabelsRedLarge">*<asp:RequiredFieldValidator ID="RequiredFieldValidator11"
                                runat="server" ControlToValidate="lstCountry" ErrorMessage="Country" EnableClientScript="False"></asp:RequiredFieldValidator></span></td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2" style="height: 10px">
                        <hr noshade size="1" width="90%">
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <span class="LabelsBodySmall"><b>Mailing Address (Checks will be mailed to this address)</b></span>
                            <br />
                            <asp:CheckBox ID="chkShippinhSame" runat="server" AutoPostBack="True" OnCheckedChanged="chkShippinhSame_CheckedChanged"
                                Text="Same as Business Address" TabIndex="18" /></td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <table border="0" cellspacing="2" cellpadding="0" style="width: 100%">
                                        <tr>
                                            <td align="right" style="width:40%">
                                                <span class="LabelsBodySmall">Address</span></td>
                                            <td align="left">
                                                <asp:TextBox ID="txtShippingAddress" runat="server" Width="140px" TabIndex="19" MaxLength="100"></asp:TextBox>
                                                <span class="LabelsRedLarge">*<asp:RequiredFieldValidator ID="RequiredFieldValidator13"
                                                    runat="server" ControlToValidate="txtShippingAddress" ErrorMessage="Address"
                                                    EnableClientScript="False"></asp:RequiredFieldValidator></span></td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <span class="LabelsBodySmall">City</span></td>
                                            <td align="left">
                                                <asp:TextBox ID="txtShippingCity" runat="server" Width="140px" TabIndex="20" MaxLength="100"></asp:TextBox>
                                                <span class="LabelsRedLarge">*<asp:RequiredFieldValidator ID="RequiredFieldValidator14"
                                                    runat="server" ControlToValidate="txtShippingCity" ErrorMessage="City" EnableClientScript="False"></asp:RequiredFieldValidator></span></td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <span class="LabelsBodySmall">State</span></td>
                                            <td align="left">
                                                <asp:DropDownList ID="lstShippingState" runat="server" TabIndex="21">
                                                </asp:DropDownList>
                                                <span class="LabelsRedLarge">*</span></td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <span class="LabelsBodySmall">Region</span></td>
                                            <td align="left">
                                                <asp:TextBox ID="txtShippingRegion" runat="server" Width="140px" TabIndex="22" MaxLength="100"></asp:TextBox>
                                                <asp:Label ID="lblShippingRegionEx" runat="server" Text="(If country not U.S.)"
                                                    Font-Size="Small"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <span class="LabelsBodySmall">Zip</span></td>
                                            <td align="left">
                                                <asp:TextBox ID="txtShippingZip" runat="server" Width="140px" TabIndex="23" MaxLength="10"></asp:TextBox>
                                                <span class="LabelsRedLarge">*<asp:RequiredFieldValidator ID="RequiredFieldValidator16"
                                                    runat="server" ControlToValidate="txtShippingZip" ErrorMessage="Zip" EnableClientScript="False"></asp:RequiredFieldValidator></span></td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <span class="LabelsBodySmall">Country</span></td>
                                            <td align="left">
                                                <asp:DropDownList ID="lstShippingCountry" runat="server" TabIndex="24" AutoPostBack="true" OnSelectedIndexChanged="lstShippingCountry_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <span class="LabelsRedLarge">*<asp:RequiredFieldValidator ID="RequiredFieldValidator17"
                                                    runat="server" ControlToValidate="lstShippingCountry" ErrorMessage="Country"
                                                    EnableClientScript="False"></asp:RequiredFieldValidator></span></td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="chkShippinhSame" EventName="CheckedChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2" style="height: 10px">
                        <hr noshade size="1" width="90%">
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <span class="LabelsBodySmall">Do you want to sign up for Direct Deposit? (Must be a US based Financial Institution)</span></td>
                        <td align="left">
                            <asp:RadioButton ID="rdbDDYes" runat="server" GroupName="DD" OnCheckedChanged="rdbDDYes_CheckedChanged"
                                Text="Yes" AutoPostBack="True" TabIndex="25" />&nbsp;<asp:RadioButton ID="rdbDDNo"
                                    runat="server" GroupName="DD" OnCheckedChanged="rdbDDYes_CheckedChanged" Text="No"
                                    AutoPostBack="True" Checked="True" TabIndex="26" /></td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <asp:Panel ID="pnlBanking" runat="server" Width="100%" Visible="False">
                                <table border="0" style="width: 100%" cellspacing="0" cellpadding="2">
                                    <tr>
                                        <td align="center" colspan="3" class="DivHeader">
                                            <span class="MenuHeader">Bank Account Information</span></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 40%">
                                            <span class="LabelsBodySmall">Bank Name</span></td>
                                        <td align="left">
                                            <asp:DropDownList ID="lstBankName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="lstBankName_SelectedIndexChanged"
                                                TabIndex="27">
                                            </asp:DropDownList>
                                            <span class="LabelsRedLarge">*</span>
                                            <asp:RequiredFieldValidator ID="ValidateBank" runat="server" ControlToValidate="lstBankName"
                                                EnableClientScript="False" ErrorMessage="Bank Name" Font-Bold="False"></asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <span class="LabelsBodySmall">If Other, specify</span></td>
                                        <td align="left">
                                            <asp:TextBox ID="txtOtherBank" runat="server" Enabled="False" MaxLength="64" TabIndex="28"
                                                Width="140px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="ValidateOtherBank" runat="server" ControlToValidate="txtOtherBank"
                                                EnableClientScript="False" Enabled="False" ErrorMessage="Other Bank Name" Font-Bold="False"></asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <span class="LabelsBodySmall">Bank Address</span></td>
                                        <td align="left">
                                            <asp:TextBox ID="txtBankAddress" runat="server" MaxLength="96" TabIndex="29" Width="140px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <span class="LabelsBodySmall">City</span></td>
                                        <td align="left">
                                            <asp:TextBox ID="TextBox1" runat="server" MaxLength="64" TabIndex="30" Width="140px"></asp:TextBox>
                                            <span
                                                style="color: #ff0000">*</span>
                                            <asp:RequiredFieldValidator ID="ValidateCity" runat="server" ControlToValidate="txtCity"
                                                EnableClientScript="False" ErrorMessage="Bank City" Font-Bold="False"></asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="middle">
                                            <span class="LabelsBodySmall">Zip</span></td>
                                        <td align="left">
                                            <asp:TextBox ID="txtZipCode" runat="server" MaxLength="30" TabIndex="31" Width="140px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="middle">
                                            <span class="LabelsBodySmall">State</span></td>
                                        <td align="left">
                                            <asp:DropDownList ID="lstBankState" runat="server" TabIndex="32">
                                            </asp:DropDownList>
                                            <span class="LabelsRedLarge">*</span>
                                            <asp:RequiredFieldValidator ID="ValidateState" runat="server" ControlToValidate="lstState"
                                                EnableClientScript="False" ErrorMessage="State" Font-Bold="False"></asp:RequiredFieldValidator>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <span class="LabelsBodySmall">Bank Phone Number</span></td>
                                        <td align="left">
                                            <asp:TextBox ID="TextBox2" runat="server" MaxLength="25" TabIndex="33" Width="140px"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorPhone" runat="server"
                                                ControlToValidate="txtPhone" EnableClientScript="False" ErrorMessage="Invalid Phone Number"
                                                ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}"></asp:RegularExpressionValidator></td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <span class="LabelsBodySmall">Name Exactly As It Appears On Checking Account</span>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtNameOnChecking" runat="server" MaxLength="50" TabIndex="34" Width="140px"></asp:TextBox>
                                            <span class="LabelsRedLarge">*</span>
                                            <asp:RequiredFieldValidator ID="ValidateCheckingAcct" runat="server" ControlToValidate="txtNameOnChecking"
                                                EnableClientScript="False" ErrorMessage="Name on Checking Acct." Font-Bold="False"></asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top">
                                            <span class="LabelsBodySmall">Bank Routing Number</span></td>
                                        <td align="left">
                                            <asp:TextBox ID="txtBankRoutingNumber" runat="server" MaxLength="9" TabIndex="35"
                                                Width="140px"></asp:TextBox>
                                            <span class="LabelsRedLarge">*</span><asp:Label ID="lblRoutingEx"
                                                    runat="server" Text="(9 digits long)"></asp:Label>
                                            <asp:RequiredFieldValidator ID="ValidateRoutingNo" runat="server" ControlToValidate="txtBankRoutingNumber"
                                                EnableClientScript="False" ErrorMessage="Routing Number"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RERoutingNum" runat="server" ControlToValidate="txtBankRoutingNumber"
                                                EnableClientScript="False" ErrorMessage="Numbers Only" ValidationExpression="[0-9]*$"></asp:RegularExpressionValidator></td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top">
                                            <span class="LabelsBodySmall">Bank Account Number</span>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtAcctNumber" runat="server" MaxLength="16" TabIndex="36" Width="140px"></asp:TextBox>
                                            <span class="LabelsRedLarge">*</span>
                                            <asp:RequiredFieldValidator ID="ValidateAcctNo" runat="server" ControlToValidate="txtAcctNumber"
                                                EnableClientScript="False" ErrorMessage="Account Number"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="REAcctNum" runat="server" ControlToValidate="txtAcctNumber"
                                                EnableClientScript="False" ErrorMessage="Numbers Only" ValidationExpression="[0-9]*$"></asp:RegularExpressionValidator></td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top">
                                            <asp:HyperLink ID="lnkAuthAgree" runat="server" CssClass="Link" Font-Names="Arial" Font-Size="Small"
                                                NavigateUrl="~/Direct Deposit Authorization Form.pdf" TabIndex="37" Target="_blank">Authorization Agreement</asp:HyperLink></td>
                                        <td align="left">
                                            <asp:Label ID="Label5" runat="server" Text="Download, complete and return this form."></asp:Label></td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2" style="height: 20px">
                        <hr noshade size="1" width="90%">
                        </td>
                    </tr>                    
                    <tr>
                        <td align="right">
                            <span class="LabelsBodySmall">Business Phone</span></td>
                        <td align="left">
                            <asp:TextBox ID="txtPhone" runat="server" Width="140px" TabIndex="38"></asp:TextBox>
                            <span class="LabelsRedLarge">*</span>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12"
                                runat="server" ControlToValidate="txtPhone" ErrorMessage="Business Phone" EnableClientScript="False"></asp:RequiredFieldValidator>
                             <span class="LabelsBodySmall">Ex: 323-965-2840</span>
                             <asp:RegularExpressionValidator ID="REValidatePhone" runat="server" ControlToValidate="txtPhone"
                                EnableClientScript="False" ErrorMessage="Invalid Format (Please enter US or International Phone formats)"
                                ValidationExpression="^(\+[1-9][0-9]*(\([0-9]*\)|-[0-9]*-))?[0]?[1-9][0-9\- ]*$"></asp:RegularExpressionValidator></td>
                    </tr><!--US  PHONE NUMBER REGULAR EXPRESSION VALIDATOR - ((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}-->
                    <tr>
                        <td align="right">
                            <span class="LabelsBodySmall">Home Phone</span></td>
                        <td align="left">
                            <asp:TextBox ID="txtHomePhone" runat="server" Width="140px" TabIndex="39"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <span class="LabelsBodySmall">Mobile Phone</span></td>
                        <td align="left">
                            <asp:TextBox ID="txtMobilePhone" runat="server" Width="140px" TabIndex="40"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <span class="LabelsBodySmall">Fax</span></td>
                        <td align="left">
                            <asp:TextBox ID="txtFax" runat="server" Width="140px" TabIndex="41"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtFax"
                                EnableClientScript="False" ErrorMessage="Invalid Format(Ex. 123-123-1234)"
                                ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}"></asp:RegularExpressionValidator></td>
                    </tr>
                    <tr>
                        <td align="right">
                            <span class="LabelsBodySmall">Site URL</span></td>
                        <td align="left">
                            <asp:TextBox ID="txtURL" runat="server" Width="140px" TabIndex="42" MaxLength="100"></asp:TextBox>
                            <span class="LabelsRedLarge"></span>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <span class="LabelsBodySmall">Comments</span></td>
                        <td align="left">
                            <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" Width="140px" TabIndex="43"
                                MaxLength="200"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right">
                            <span class="LabelsBodySmall">Notify me by Email when I make a Sale</span></td>
                        <td align="left">
                            <asp:DropDownList ID="lstNotify" runat="server" TabIndex="44">
                                <asp:ListItem>Yes</asp:ListItem>
                                <asp:ListItem>No</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2" style="height: 10px">
                        <hr noshade size="1" width="90%">
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <span class="LabelsBodySmall">User Name</span></td>
                        <td style="height: 25px; width: 60%;" align="left">
                            <asp:TextBox ID="txtUserName" runat="server" Width="140px" MaxLength="16" TabIndex="45"></asp:TextBox>
                            <span class="LabelsRedLarge">*<asp:RequiredFieldValidator ID="ValidatorUserName"
                                runat="server" ControlToValidate="txtUserName" EnableClientScript="False" ErrorMessage="User Name"></asp:RequiredFieldValidator></span></td>
                    </tr>
                    <tr>
                        <td align="right" valign="top">
                            <span class="LabelsBodySmall">Password</span></td>
                        <td align="left">
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="140px" MaxLength="16"
                                TabIndex="46"></asp:TextBox>
                            <span class="LabelsRedLarge">*</span>
                           <asp:RequiredFieldValidator ID="ValidatorPassword"
                                runat="server" ControlToValidate="txtPassword" EnableClientScript="False" ErrorMessage="Password">
                                </asp:RequiredFieldValidator>
                       <span class="LabelsBodySmall">Six characters minimum</span>
                                                          
                                </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top">
                            <span class="LabelsBodySmall">Confirm Password</span></td>
                        <td align="left">
                            <asp:TextBox ID="txtPasswordConfirm" runat="server" TextMode="Password" Width="140px"
                                MaxLength="16" TabIndex="47"></asp:TextBox>
                            <span class="LabelsRedLarge">*<asp:RequiredFieldValidator ID="ValidatorConfirmPassword"
                                runat="server" ControlToValidate="txtPasswordConfirm" EnableClientScript="False"
                                ErrorMessage="Confirm Password"></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtPasswordConfirm"
                                    ControlToValidate="txtPassword" EnableClientScript="False" ErrorMessage="Passwords do not match"></asp:CompareValidator></span></td>
                    </tr>
                    <tr>
                        <td align="right" valign="top">
                            <span class="LabelsBodySmall">Password Hint</span></td>
                        <td align="left">
                            <asp:TextBox ID="txtPasswordPhrase" runat="server" Width="140px" TabIndex="48" MaxLength="50"></asp:TextBox>
                            <span class="LabelsRedLarge">*
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                runat="server" ControlToValidate="txtPasswordPhrase" ErrorMessage="Password Hint"
                                EnableClientScript="False"></asp:RequiredFieldValidator>
                                </span></td>
                    </tr>
                    <tr height=10></tr>
                    <tr>
                        <td align="right" valign="middle">
                            <asp:HyperLink ID="lnkW9" runat="server" CssClass="Link" NavigateUrl="fw9.pdf" Target="_blank" Font-Names="Arial"
                                Font-Size="Small" TabIndex="49">W-9 Form</asp:HyperLink></td>
                        <td align="left" valign="middle">
                            <asp:Label ID="Label1" runat="server" Text="Download, complete and return this form."></asp:Label></td>
                    </tr>
                    <tr>
                        <td align="right" valign="middle">
                            <asp:HyperLink ID="lnkAgreement" runat="server" CssClass="Link" NavigateUrl="agreement.aspx" Target="_blank"
                                Font-Names="Arial" Font-Size="Small" TabIndex="50">Agreement</asp:HyperLink></td>
                        <td align="left" valign="middle">
                            <asp:Label ID="Label6" runat="server" Text="Download, complete and return this form."></asp:Label></td>
                    </tr>
                    <tr>
                        <td align="right" valign="middle">
                            <asp:HyperLink ID="lnkAuthorizationForm" runat="server" CssClass="Link" 
                            NavigateUrl="https://www.firstaffiliates.com/Sage/Forms/New Rep/Authorization Form.pdf" Target="_blank"
                                Font-Names="Arial" Font-Size="Small" TabIndex="51" Visible=false>Authorization Form</asp:HyperLink></td>
                        <td align="left" valign="middle">
                            <asp:Label ID="Label2" Visible=false runat="server" Text="Download, complete and return this form."></asp:Label></td>
                    </tr>
                    <tr height=10></tr>
                    <tr>
                        <td  align="center" valign="middle" colspan=2>
                            <asp:CheckBox ID="chkAgree" runat="server" OnCheckedChanged="chkAgree_CheckedChanged" Text="I Agree" AutoPostBack="True" TabIndex="52" />
                    </tr>
                    <tr height=10></tr>
                    <tr>
                        <td align="center" colspan="2" valign="middle">
                    <div class="g-recaptcha" data-sitekey="6LdWm_8SAAAAAK8Om3BDYVFw7zmYCTNB4RAvJCvL"></div>
                    </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2" valign="middle">
                            <asp:Button ID="btnSubmit" runat="server" Enabled=false Text="Submit" OnClick="btnSubmit_Click"
                                TabIndex="53" />
                            <br />
                            <asp:Label ID="lblAgreement" runat="server" Text="Submit this form to signify that you have read, understood and agreed to the Terms & Conditions of our Agreement"></asp:Label></td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center" style="height: 30px;" class="DivHeader">
                            <span style="font-family: Arial; font-size: x-small; color: White">Copyright © 2007
                                Commerce Technologies.</span>
                        </td>
                    </tr>
                </table>                
                </asp:Panel>
                <cc1:PasswordStrength ID="PasswordStrength1" runat="server" TargetControlID="txtPassword"
                DisplayPosition="RightSide"
                StrengthIndicatorType="Text"
                PreferredPasswordLength="6"
                PrefixText="Strength:"
                TextCssClass="PasswordCSS"
                TextStrengthDescriptions="Very Poor;Weak;Average;Strong;Excellent"
                MinimumNumericCharacters="0"
                MinimumSymbolCharacters="0"
                RequiresUpperAndLowerCaseCharacters="false" />
            </center>
        </div>
    </form>
</body>
</html>
