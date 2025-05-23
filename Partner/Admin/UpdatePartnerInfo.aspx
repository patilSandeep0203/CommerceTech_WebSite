<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdatePartnerInfo.aspx.cs" Inherits="UpdatePartnerInfo"
    Theme="AppTheme" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Commerce Technologies - Update Partner</title>
    <link href="../PartnerCSS.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <center>
        <form id="form1" runat="server">
            <asp:Label ID="lblError" CssClass="LabelsError" EnableTheming="false" runat="server"
                Visible="False"></asp:Label><br />
            <strong><span class="LabelsRed">Leave blank for any fields that do not apply.</span></strong>
            <table cellpadding="0" cellspacing="4" border="0" style="width: 500px;" class="DivGreen">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager> 
                <tr>
                    <td colspan="2" style="background-image: url(/PartnerPortal/Images/topMain.gif);
                        height: 25px">
                        <span class="MenuHeader"><b>Update Partner Information</b></span>
                    </td>
                </tr>
                <tr>
                <td align="right" style="width: 30%">
                    <span class="LabelsSmall">Partner ID</span></td>
                  <td align="left">
                    <b><asp:Label Font-Size=Larger ID=lblPartnerID runat=server></asp:Label></b></td>
                </tr>    
                <tr>
                    <td align="right" style="width: 30%">
                        <span class="LabelsSmall">First Name</span></td>
                    <td align="left">
                        <asp:TextBox ID="txtFirstName" width="140px" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td align="right" style="width: 30%">
                        <span class="LabelsSmall">Last Name</span></td>
                    <td align="left">
                        <asp:TextBox ID="txtLastName" width="140px" runat="server"></asp:TextBox></td>
                </tr>

                <tr>
                    <td align="right">
                        <span class="LabelsSmall">Company Name</span></td>
                    <td align="left">
                        <asp:TextBox ID="txtCompanyName" runat="server" Width=245></asp:TextBox><span class="LabelsRedLarge">*</span>
                    </td>
                        <asp:RequiredFieldValidator ID="RequiredFieldLegalName" runat="server" ControlToValidate="txtCompanyName"
                                            ErrorMessage="Company name"></asp:RequiredFieldValidator>
                </tr>
                <tr>
                    <td align="right">
                        <span class="LabelsSmall">DBA</span></td>
                    <td align="left">
                        <asp:TextBox ID="txtDBA" runat="server" Width=245></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <span class="LabelsSmall">Legal Status</span></td>
                    <td align="left">
                        <asp:DropDownList ID="lstLegalStatus" runat="server" TabIndex="17" Width=250 AutoPostBack="true">
                            <asp:ListItem></asp:ListItem>
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
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right"  valign="top">
                        <asp:RadioButton ID="rdbTaxID" runat="server" GroupName="TaxSSN" Text="Federal Tax ID OR" /><br />
                        <asp:RadioButton ID="rdbSSN" runat="server" GroupName="TaxSSN" Text="Social Security Number" /></td>
                    <td  align="left">
                        <asp:TextBox ID="txtTaxSSN" runat="server" MaxLength="9" Width="140px" >                                                       
                        </asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtTaxSSN"
                                        FilterType="Custom, Numbers" />
                        <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit"  OnClick="lnkEdit_Click"></asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <span class="LabelsSmall">Referral Partner DBA</span></td>
                    <td align="left">
                        <asp:DropDownList ID="lstReferral" runat="server" TabIndex="17" Width=250 AutoPostBack="true" OnSelectedIndexChanged="lstReferral_SelectedIndexChanged" >
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <span class="LabelsSmall">Non Partner Referral</span></td>
                    <td align="left">
                    <asp:DropDownList ID="lstOtherReferral" runat="server" TabIndex="17" Width=250 AutoPostBack="false" Enabled=false>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <span class="LabelsSmall">Office</span></td>
                    <td align="left">
                    <asp:DropDownList ID="lstOffice" runat="server" TabIndex="17" Width=250 AutoPostBack="true" Enabled=true>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr height=10px></tr>
                <tr>
                    <td align="center" colspan="2" >
                        <span class="LabelsSmall"><b>Ship to [Physical Address]</b></span></td>
                </tr>
                <tr>
                    <td align="right" >
                        <span class="LabelsSmall">Address</span></td>
                    <td  align="left">
                        <asp:TextBox ID="txtAddress" runat="server" Width="245px"></asp:TextBox>
                    </td>
                </tr>
                                <tr>
                    <td align="right" >
                        <span class="LabelsSmall"></span></td>
                    <td  align="left">
                        <asp:TextBox ID="txtAddress2" runat="server" Width="245px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" >
                        <span class="LabelsSmall">City</span></td>
                    <td  align="left">
                        <asp:TextBox ID="txtCity" runat="server" Width="140px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" >
                        <span class="LabelsSmall">State</span></td>
                    <td  align="left">
                        <asp:DropDownList ID="lstState" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right" >
                        <span class="LabelsSmall">Zip</span></td>
                    <td  align="left">
                        <asp:TextBox ID="txtZip" runat="server" Width="140px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" >
                        <span class="LabelsSmall">Country</span></td>
                    <td  align="left">
                        <asp:DropDownList ID="lstCountry" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2" >
                        <span class="LabelsSmall"><b>Bill to [Mailing Address]</b></span>
                        <asp:CheckBox ID="chkMailingSame" runat="server" AutoPostBack="True" OnCheckedChanged="chkMailingSame_CheckedChanged"
                            Text="Same as Business Address" /></td>
                </tr>
                <tr>
                    <td align="right" >
                        <span class="LabelsSmall">Address</span></td>
                    <td  align="left">
                        <asp:TextBox ID="txtMailingAddress" runat="server" Width="245px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" >
                        <span class="LabelsSmall"></span></td>
                    <td  align="left">
                        <asp:TextBox ID="txtMailingAddress2" runat="server" Width="245px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" >
                        <span class="LabelsSmall">City</span></td>
                    <td  align="left">
                        <asp:TextBox ID="txtMailingCity" runat="server" Width="140px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" >
                        <span class="LabelsSmall">State</span></td>
                    <td  align="left">
                        <asp:DropDownList ID="lstMailingState" runat="server" >
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right" >
                        <span class="LabelsSmall">Zip</span></td>
                    <td  align="left">
                        <asp:TextBox ID="txtMailingZip" runat="server" Width="140px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" >
                        <span class="LabelsSmall">Country</span></td>
                    <td  align="left">
                        <asp:DropDownList ID="lstMailingCountry" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2" >
                    </td>
                </tr>

                <tr>
                    <td align="right" >
                        <span class="LabelsSmall">Business Phone</span></td>
                    <td  align="left">
                        <asp:TextBox ID="txtPhone" runat="server" Width="140px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" >
                        <span class="LabelsSmall">Home Phone</span></td>
                    <td  align="left">
                        <asp:TextBox ID="txtHomePhone" runat="server" Width="140px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" >
                        <span class="LabelsSmall">Mobile Phone</span></td>
                    <td  align="left">
                        <asp:TextBox ID="txtMobilePhone" runat="server" Width="140px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" >
                        <span class="LabelsSmall">Fax</span></td>
                    <td  align="left">
                        <asp:TextBox ID="txtFax" runat="server" Width="140px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td align="right" style="width: 30%">
                        <span class="LabelsSmall">Email</span></td>
                    <td align="left">
                        <asp:TextBox ID="txtEmail" runat="server" Width=245></asp:TextBox></td>
                </tr> 
                <tr>
                    <td align="right" >
                        <span class="LabelsSmall">Website URL http://www.</span></td>
                    <td  align="left">
                        <asp:TextBox ID="txtURL" runat="server" Width="245px"></asp:TextBox>
                        <span class="LabelsRedLarge"></span>
                    </td>
                </tr>

                    <tr>
                        <td align="center" colspan="2">
                            <asp:Panel ID="pnlBanking" runat="server" Width="100%" Visible="True">
                                <table border="0" style="width: 100%" cellspacing="0" cellpadding="2">
                                    <tr>
                                        <td align="center" colspan="3"  class="DivBlue">
                                            <strong><span class="MenuHeader">Bank Account Information</span></strong></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 40%" valign="middle">
                                            <span class="LabelsSmall">Bank Name</span>
                                            </td>
                                        <td align="left" colspan="2" valign="middle">
                                            &nbsp;<asp:Label ID="lblBankName" Font-Bold="true" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 40%" valign="middle">
                                            <span class="LabelsSmall">If Other, specify</span>
                                            </td>
                                        <td align="left" colspan="2" valign="middle">
                                            <strong>&nbsp;<asp:Label ID="lblOtherBank" runat="server" Font-Bold="True"></asp:Label></strong></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 40%" valign="middle">
                                            <span class="LabelsSmall">Bank Address</span>
                                            </td>
                                        <td align="left" colspan="2" valign="middle">
                                            &nbsp;<asp:Label ID="lblBankAddress" runat="server" Font-Bold="True"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 40%" valign="middle">
                                            <span class="LabelsSmall">Bank City</span>
                                            </td>
                                        <td colspan="2" align="left" valign="middle">
                                            &nbsp;<asp:Label ID="lblBankCity" Font-Bold="True" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 40%" valign="middle">
                                            <span class="LabelsSmall">Zip Code</span>
                                            </td>
                                        <td align="left" colspan="2" valign="middle">
                                            <strong>&nbsp;<asp:Label ID="lblZipCode" runat="server" Font-Bold="True"></asp:Label></strong></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 40%" valign="middle">
                                            <span class="LabelsSmall">State</span>
                                            </td>
                                        <td align="left" colspan="2" valign="middle">
                                            &nbsp;<asp:Label ID="lblBankState" runat="server" Font-Bold="True"></asp:Label>&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 40%" valign="middle">
                                            <span class="LabelsSmall">Bank Phone Number</span>
                                            </td>
                                        <td colspan="2" align="left" valign="middle">
                                            &nbsp;<asp:Label Font-Bold="True" ID="lblBankPhone" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 40%" valign="middle">
                                            <span class="LabelsSmall">Name Exactly As It Appears On Checking Account</span>
                                        </td>
                                        <td colspan="2" align="left" valign="middle">
                                            &nbsp;<asp:Label ID="lblNameOnChecking" Font-Bold="true" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 40%" valign="middle">
                                            <span class="LabelsSmall">Bank Routing Number</span>
                                            </td>
                                        <td colspan="2" align="left" valign="middle">
                                            &nbsp;<asp:Label ID="lblBankRoutingNumber" runat="server" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 40%" valign="middle">
                                            <span class="LabelsSmall">Bank Account Number</span>
                                        </td>
                                        <td colspan="2" align="left" valign="middle">
                                            &nbsp;<asp:Label ID="lblAcctNumber" runat="server" Font-Bold="True"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" align="center">
                                            <span style="font-size: 10pt; font-family:Arial">
                                                <b><!--<asp:HyperLink ID="lnkEditInfo" CssClass="One" runat="server" NavigateUrl="EditBanking.aspx">Edit Bank Account Information</asp:HyperLink>-->
                                                <asp:LinkButton ID="lnkEditBankInfo" runat="server" Text="Edit Bank Account Information" OnClick="lnkEditBankInfo_Click"></asp:LinkButton></b></span></td>
                                    </tr>

                                </table>
                            </asp:Panel>
                        </td>
                    </tr>

                <tr height=10px></tr>
                <tr>
                    <td style="background-image: url(/PartnerPortal/Images/topMain.gif); height: 25px"
                        align="center" colspan="2">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                            TabIndex="41" />&nbsp;
                        <asp:Button ID="btnClose" runat="server" Text="Close" TabIndex="42" OnClick="btnClose_Click"
                            CausesValidation="False" />
                    </td>
                </tr>
                                    <tr>
                        <td align="center" colspan="2">
                            <span class="LabelsSmall">			<asp:HyperLink ID="lnkAddImage" runat="server" CssClass="One" NavigateUrl="~/UploadLogo.aspx" Target="_blank" Font-Bold="True" Font-Names="Arial" Font-Size="Small">
			<br />
			Click here to Upload a Logo<br />
			</asp:HyperLink></span></td>
                    </tr>
                
            </table>
        </form>
    </center>
</body>
</html>
