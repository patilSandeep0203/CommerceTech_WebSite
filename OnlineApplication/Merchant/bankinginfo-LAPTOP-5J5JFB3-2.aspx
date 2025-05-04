<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="bankinginfo.aspx.cs" Inherits="Merchant_bankinginfo" Title="Commerce Technologies - Online Application"
    Theme="TextBoxSkin" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentHolder" runat="Server">
    <asp:Panel ID="pnlMainPage" runat="server">
        <table border="0" cellpadding="2" cellspacing="0" width="100%" class="DivGreen">
            <tr>
                <td colspan="3" align="center" class="DivHeader2" height="26px">
                    <b><span class="MenuHeader">Merchant Account Signup Information</span></b>
                </td>
            </tr>
            <tr>
            <td colspan="3" align="center" class="DivHeader">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="DivHeader" align="center" width="16%">
                            <asp:LinkButton ID="linkCardPct" CssClass="MenuHeader" Text="Card Percentages" runat="server" OnClick="linkCardPctClick"/>
                        </td>
                        <td class="DivHeader" align="center" width="16%">
	                        <asp:LinkButton ID="linkCompany" CssClass="MenuHeader" Text="Contact Info" runat="server" OnClick="linkCompanyClick"/>
                        </td>
                        <td class="DivHeader" align="center" width="16%">
                            <asp:LinkButton ID="linkBusiness" CssClass="MenuHeader" Text="Business Info" runat="server" OnClick="linkBusinessClick"/>
                        </td>
                        <td class="DivHeader" align="center" width="16%">
                            <asp:LinkButton ID="linkPrincipal" CssClass="MenuHeader" Text="Principal Info" runat="server" OnClick="linkPrincipalClick"/>
                        </td>
                        <td class="DivHeader2" align="center" width="16%">
                            <asp:LinkButton ID="linkBanking" CssClass="MenuHeader" Text="Banking Info" runat="server" OnClick="linkBankingClick"/>
                        </td>
                        <td class="DivHeader" align="center" width="16%">
                            <asp:LinkButton ID="linkPDF" CssClass="MenuHeader" Text="Sign application" runat="server" OnClick="linkPDFClick"/>
                        </td>
                    </tr>
                </table>
            </td>
            </tr>
            <!--<tr>
                <td colspan="3" align="center">
                    <a href="cardpct.aspx" onmouseover="document.mcardpct.src='/Application/Images/oa_CardPercentages_mouseout.gif'"
                        onmouseout="document.mcardpct.src='/Application/Images/oa_CardPercentages_mouseover.gif'">
                        <img name="mcardpct" style="border: 0" alt="" src="/Application/Images/oa_CardPercentages_mouseover.gif" /></a>
                    <a href="companyinfo.aspx" onmouseover="document.mcontact.src='/Application/Images/oa_Contact_mouseout.gif'"
                        onmouseout="document.mcontact.src='/Application/Images/oa_Contact_mouseover.gif'">
                        <img name="mcontact" style="border: 0" alt="" src="/Application/Images/oa_Contact_mouseover.gif" /></a>
                    <a href="businessinfo.aspx" onmouseover="document.mbusiness.src='/Application/Images/oa_Business_mouseout.gif'"
                        onmouseout="document.mbusiness.src='/Application/Images/oa_Business_mouseover.gif'"><img
                        name="mbusiness" style="border: 0" alt="" src="/Application/Images/oa_Business_mouseover.gif" /></a>
                    <a href="principalinfo.aspx" onmouseover="document.mprincipal.src='/Application/Images/oa_Principal_mouseout.gif'"
                        onmouseout="document.mprincipal.src='/Application/Images/oa_Principal_mouseover.gif'"><img
                        name="mprincipal" style="border: 0" alt="" src="/Application/Images/oa_Principal_mouseover.gif" /></a>
                    <img name="mbanking" style="border: 0" alt="" src="/Application/Images/oa_Banking.gif" />
                </td>
            </tr>-->
            <tr>
                <td colspan="3" align="center">
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <cc1:PopupControlExtender ID="pceOD" runat="server" PopupControlID="pnlOD"
            TargetControlID="imgOD" Position="Bottom" />
        <cc1:PopupControlExtender ID="pceEBT" runat="server" PopupControlID="pnlEBTPopup"
            TargetControlID="imgEBT" Position="Bottom" />
                    <cc1:PopupControlExtender ID="PopupControlExtender1" runat="server" PopupControlID="pnlDiscover"
                        TargetControlID="imgDiscover" Position="Bottom" />
                    <cc1:PopupControlExtender ID="PopupControlExtender2" runat="server" PopupControlID="pnlAmex"
                        TargetControlID="imgAmex" Position="Bottom" />
                    <cc1:PopupControlExtender ID="PopupControlExtender3" runat="server" PopupControlID="pnlJCB"
                        TargetControlID="imgJCB" Position="Bottom" />
                    <cc1:PopupControlExtender ID="PopupControlExtender4" runat="server" PopupControlID="pnlAvgTicket"
                        TargetControlID="imgAvgTicket" Position="Bottom" />
                    <cc1:PopupControlExtender ID="PopupControlExtender5" runat="server" PopupControlID="pnlMaxTicket"
                        TargetControlID="imgMaxTicket" Position="Bottom" />
                    <cc1:PopupControlExtender ID="PopupControlExtender6" runat="server" PopupControlID="pnlMonVol"
                        TargetControlID="imgMonVol" Position="Bottom" />
                </td>
            </tr>
            <tr>
                <td colspan="3" align="center">
                    <asp:Label ID="lblHeader" runat="server" Text="Please provide the following financial information about your company."></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="3" align="center">
                    <asp:Label ID="lblError" runat="server" BackColor="Red" Font-Size="Medium" ForeColor="White"
                        Visible="False"></asp:Label><br />
                    <asp:ValidationSummary ID="ValidateSummary" runat="server" BackColor="#FFC0C0" BorderColor="red"
                        BorderWidth="1px" ForeColor="Black" HeaderText="Please check the fields marked in red."
                        Width="250px" />
                </td>
            </tr>
            <tr>
                <td colspan="3" align="center">
                    <span class="LabelsRed"><b>*</b> - denotes a required field</span>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="3">
                    <asp:Panel ID="pnlBankingInfo" runat="server" Height="100%" Width="100%">
                        <table border="0" cellpadding="2" cellspacing="0" style="width: 100%;">                            
                            <tr>
                                <td colspan="3" align="center" class="DivHeader">
                                    <strong><span class="MenuHeader">BANK ACCOUNT INFORMATION</span></strong></td>
                            </tr>
                            <tr>
                                <td align="right" width="40%">
                                    <asp:Label ID="lblBankName" runat="server" Text="Bank Name" Font-Bold="False"></asp:Label></td>
                                <td align="left" colspan="2">
                                    <asp:DropDownList ID="lstBankName" runat="server" OnSelectedIndexChanged="lstBankName_SelectedIndexChanged"
                                        AutoPostBack="True" TabIndex="1">
                                    </asp:DropDownList><span style="color: red">*</span><asp:RequiredFieldValidator
                                        ID="ValidateBank" runat="server" ErrorMessage="Bank Name" Font-Bold="False" ControlToValidate="lstBankName"
                                        EnableClientScript="False"></asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblOtherBank" runat="server" Text="If Other, specify" Font-Bold="False"></asp:Label></td>
                                <td align="left" colspan="2">
                                    <asp:TextBox ID="txtOtherBank" runat="server" MaxLength="64" Width="140px" TabIndex="2"
                                        Enabled="False" OnTextChanged="textChangedEventHandler"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="ValidateOtherBank" runat="server" ControlToValidate="txtOtherBank"
                                        ErrorMessage="Other Bank Name" Font-Bold="False" Enabled="False" EnableClientScript="False"></asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblBankAddress" runat="server" Text="Bank Address" Font-Bold="False"></asp:Label></td>
                                <td align="left" colspan="2">
                                    <asp:TextBox ID="txtBankAddress" runat="server" MaxLength="96" Width="140px" TabIndex="3" OnTextChanged="textChangedEventHandler"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblCity" runat="server" Text="City" Font-Bold="False"></asp:Label></td>
                                <td colspan="2" align="left">
                                    <asp:TextBox ID="txtCity" runat="server" MaxLength="20" Width="140px" TabIndex="4" OnTextChanged="textChangedEventHandler"></asp:TextBox>
                                    </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblZipCode" runat="server" Text="Zip Code" Font-Bold="False"></asp:Label></td>
                                <td align="left" colspan="2">
                                    <asp:TextBox ID="txtZipCode" runat="server" MaxLength="30" Width="140px" TabIndex="5" OnTextChanged="textChangedEventHandler"></asp:TextBox>
                                    </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblState" runat="server" Text="State" Font-Bold="False"></asp:Label></td>
                                <td align="left">
                                    <asp:DropDownList ID="lstState" runat="server" TabIndex="6" OnSelectedIndexChanged="textChangedEventHandler">
                                    </asp:DropDownList><span style="color: red">*</span>
                                    <asp:RequiredFieldValidator ID="ValidateState" runat="server" ErrorMessage="State"
                                        Font-Bold="False" ControlToValidate="lstState" EnableClientScript="False"></asp:RequiredFieldValidator></td>
                                <td align="left" valign="middle">
                                    <asp:Label ID="lblRegion" runat="server" Text="Region" Font-Bold="False"></asp:Label>
                                    <asp:TextBox ID="txtRegion" runat="server" MaxLength="40" TabIndex="7" OnTextChanged="textChangedEventHandler"></asp:TextBox></td>
                            </tr>
                            
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblCountry" runat="server" Text="Country" Font-Bold="False"></asp:Label></td>
                                <td align="left" colspan="2">
                                    <asp:DropDownList ID="lstCountry" runat="server" TabIndex="6" AutoPostBack="true" OnSelectedIndexChanged="lstCountry_SelectedIndexChanged">
                                    </asp:DropDownList><span style="color: red">*</span>
                                    <asp:RequiredFieldValidator ID="ValidateCountry" runat="server" ErrorMessage="Country"
                                        Font-Bold="False" ControlToValidate="lstCountry" EnableClientScript="False"></asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblPhone" runat="server" Text="Bank Phone Number  " Font-Bold="False"></asp:Label></td>
                                <td colspan="2" align="left">
                                    <asp:TextBox ID="txtPhone" runat="server" MaxLength="25" Width="140px" TabIndex="8" OnTextChanged="textChangedEventHandler"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblNameOnChecking" runat="server" Text="Name Exactly As It Appears On Checking Account"
                                        Font-Bold="False"></asp:Label>
                                </td>
                                <td colspan="2" align="left">
                                    <asp:TextBox ID="txtNameOnChecking" runat="server" MaxLength="50" Width="140px" TabIndex="9" OnTextChanged="textChangedEventHandler"></asp:TextBox><span
                                        style="color: red">*</span>
                                    <asp:RequiredFieldValidator ID="ValidateCheckingAcct" runat="server" ErrorMessage="Name on Checking Acct."
                                        Font-Bold="False" ControlToValidate="txtNameOnChecking" EnableClientScript="False"></asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblRoutingNumber" runat="server" Text="Bank Routing Number" Font-Bold="False"></asp:Label></td>
                                <td colspan="2" align="left">
                                    <asp:TextBox ID="txtBankRoutingNumber" runat="server" MaxLength="9" Width="140px"
                                        TabIndex="10" OnTextChanged="textChangedEventHandler"></asp:TextBox><span style="color: red">*</span><asp:Label ID="lblRoutingEx"
                                            runat="server" Text="(9 digits long)"></asp:Label>
                                    <asp:LinkButton ID="lnkRoutingNum" runat="server" CssClass="Link" OnClick="lnkRoutingNum_Click">Edit</asp:LinkButton>
                                    <asp:RequiredFieldValidator ID="ValidateRoutingNo" runat="server" ErrorMessage="Routing Number"
                                        ControlToValidate="txtBankRoutingNumber" EnableClientScript="False"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RERoutingNum" runat="server" ControlToValidate="txtBankRoutingNumber"
                                        ErrorMessage="Numbers Only" ValidationExpression="[0-9]*$" EnableClientScript="False"></asp:RegularExpressionValidator></td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblAcctNum" runat="server" Text="Bank Account Number" Font-Bold="False"></asp:Label>
                                </td>
                                <td colspan="2" align="left">
                                    <asp:TextBox ID="txtAcctNumber" runat="server" MaxLength="20" Width="140px" TabIndex="11" OnTextChanged="textChangedEventHandler"></asp:TextBox><span
                                        style="color: red">*</span>
                                    <asp:LinkButton ID="lnkAccountNum" runat="server" CssClass="Link" OnClick="lnkAccountNum_Click">Edit</asp:LinkButton>
                                    <asp:RequiredFieldValidator ID="ValidateAcctNo" runat="server" ErrorMessage="Account Number"
                                        ControlToValidate="txtAcctNumber" EnableClientScript="False"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="REAcctNum" runat="server" ControlToValidate="txtAcctNumber"
                                        ErrorMessage="Numbers Only" ValidationExpression="[0-9]*$" EnableClientScript="False"></asp:RegularExpressionValidator></td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Button ID="btnShowPopup" runat="server" style="display:none" />
                    <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowPopup" PopupControlID="pnlpopup"
>
</cc1:ModalPopupExtender>
<asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="100px" Width="400px" style="display:none">
<table width="100%" style="border:Solid 2px #007b79; width:100%; height:100%" cellpadding="0" cellspacing="0">
<tr >
<td style=" height:10%; color:White; font-weight:bold;padding:3px; font-size:larger; font-family:Arial" align="Left">Confirm Box</td>
<td style=" color:White; font-weight:bold;padding:3px; font-size:10pt" align="Right"> 
<a href = "" onclick = "closepopup()"><!--<img src="Images/Close.gif" style="border :0px" align="right" alt="close"/>-->Close</a>
</td>
</tr>
<tr>
<td colspan="2" align="left" style="padding:5px; font-size:larger; font-family:Arial;">
<asp:Label ID="lblUser" style="padding:5px; font-size:13px; font-family:Arial; font-weight:bold" runat="server"/>
</td>
</tr>
<tr>
<td colspan="2"></td>
</tr>
<tr>
<td>
</td>
<td align="right" style="padding-right:15px">
<asp:Button ID="btnPOS2013Yes" OnClick="btnPOS2013Yes_Click" runat="server" Text="Yes"/>
<asp:Button ID="btnPOS2013No" OnClick="btnPOS2013No_Click" runat="server" Text="No" />
</td>
</tr>
</table>
</asp:Panel>

<asp:Button ID="btnShowNavigateNextPopup" runat="server" style="display:none" />
                    <cc1:ModalPopupExtender ID="NavigatePopupModalExtender" runat="server" TargetControlID="btnShowNavigateNextPopup" PopupControlID="pnlNavigateUnsaved"
>
</cc1:ModalPopupExtender>

<asp:Panel ID="pnlNavigateUnsaved" runat="server" BackColor="White" Height="100px" Width="400px" style="display:none">
<table width="100%" style="border:Solid 2px #007b79; width:100%; height:100%" cellpadding="0" cellspacing="0">
<tr >
<td style=" height:10%; color:White; font-weight:bold;padding:3px; font-size:larger; font-family:Arial" align="Left">Confirm Box</td>
<td style=" color:White; font-weight:bold;padding:3px; font-size:10pt" align="Right"> 
<a href = "" onclick = "closepopup()"><!--<img src="Images/Close.gif" style="border :0px" align="right" alt="close"/>-->Close</a>
</td>
</tr>
<tr>
<td colspan="2" align="left" style="padding:5px; font-size:larger; font-family:Arial;">
<asp:Label ID="lblUserNavigateUnsaved" style="padding:5px; font-size:13px; font-family:Arial; font-weight:bold" runat="server"/>
</td>
</tr>
<tr>
<td colspan="2"></td>
</tr>
<tr>
<td>
</td>
<td align="right" style="padding-right:15px">
<asp:Button ID="btnNavigateNext" OnClick="btnNavigateNext_Click" runat="server" Text="Yes"/>
<asp:Button ID="btnDoNotNavigate" OnClick="btnDoNotNavigate_Click" runat="server" Text="No" />
</td>
</tr>
</table>
</asp:Panel>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="3" valign="top">
                    <asp:Panel ID="pnlProcessingLimits" runat="server" Height="100%" Width="100%">
                        <table border="0" style="width: 100%;">
                            <tr>
                                <td colspan="3" align="center" class="DivHeader">
                                    <strong><span class="MenuHeader">PROCESSING LIMITS</span></strong></td>
                            </tr>                            
                            <tr>
                                <td align="right" width="40%">
                                    <asp:Label ID="lblAvgTicket" runat="server" Text="Average Ticket $" Font-Bold="False"></asp:Label>
                                </td>
                                <td colspan="2" align="left">
                                    <asp:TextBox ID="txtAvgTicket" runat="server" Width="90px" MaxLength="7" TabIndex="12" OnTextChanged="textChangedEventHandler"></asp:TextBox><span style="color: red">*</span>
                                    <asp:Image ID="imgAvgTicket" runat="server" ImageUrl="/Application/Images/help.gif" />
                                    <asp:Label ID="lblAvgTicketEx" runat="server" Text="(ex. $50)"></asp:Label>
                                    <asp:RequiredFieldValidator ID="ValidateAvgTicket" runat="server" ControlToValidate="txtAvgTicket"
                                        ErrorMessage="Average Ticket" EnableClientScript="False"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="REAvgTicket" runat="server" ControlToValidate="txtAvgTicket"
                                        ErrorMessage="Numbers Only" ValidationExpression="[0-9]*$" EnableClientScript="False"></asp:RegularExpressionValidator></td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblMaxTicket" runat="server" Text="Max Ticket $" Font-Bold="False"></asp:Label></td>
                                <td colspan="2" align="left">
                                    <asp:TextBox ID="txtMaxTicket" runat="server" Width="90px" MaxLength="7" TabIndex="13" OnTextChanged="textChangedEventHandler"></asp:TextBox><span
                                        style="color: red">*</span>
                                    <asp:Image ID="imgMaxTicket" runat="server" ImageUrl="/Application/Images/help.gif" />
                                    <asp:Label ID="lblMaxTicketEx" runat="server" Text="(ex. $600)"></asp:Label>
                                    <asp:RequiredFieldValidator ID="ValidateMaxTicket" runat="server" ControlToValidate="txtMaxTicket"
                                        ErrorMessage="Max Ticket" EnableClientScript="False"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="REMaxTicket" runat="server" ControlToValidate="txtMaxTicket"
                                        ErrorMessage="Numbers Only" ValidationExpression="[0-9]*$" EnableClientScript="False" ></asp:RegularExpressionValidator></td>                                       
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblMonVol" runat="server" Text="Estimated Monthly Volume $" Font-Bold="False"></asp:Label></td>
                                <td colspan="2" align="left">
                                    <asp:TextBox ID="txtMonthlyVol" runat="server" Width="90px" MaxLength="8" TabIndex="14" OnTextChanged="textChangedEventHandler"></asp:TextBox><span
                                        style="color: red">*</span>
                                    <asp:Image ID="imgMonVol" runat="server" ImageUrl="/Application/Images/help.gif" />
                                    <asp:Label ID="lblMonVolEx" runat="server" Text="(ex. $1000)"></asp:Label>
                                    <asp:RequiredFieldValidator ID="ValidateMonVol" runat="server" ControlToValidate="txtMonthlyVol"
                                        ErrorMessage="Monthly Volume" EnableClientScript="False"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="REMonVol" runat="server" ControlToValidate="txtMonthlyVol"
                                        ErrorMessage="Numbers Only" ValidationExpression="[0-9]*$" EnableClientScript="False"></asp:RegularExpressionValidator></td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>            
            <tr valign="top">
                <td align="center" colspan="3" valign="middle">
                    <asp:Panel ID="pnlNBC" runat="server" Height="100%" Width="100%">
                        <table border="0" style="width: 100%;">      
                            <tr>
                                <td colspan="3" align="center" class="DivHeader">
                                    <strong><span class="MenuHeader">OTHER PAYMENT OPTIONS</span></strong>
                                </td>
                            </tr>                      
                            <tr>
                                <td align="right" width="40%">
                                    <asp:Label ID="lblDiscover" runat="server" Text="Discover Card" Font-Bold="False"></asp:Label>
                                </td>
                                <td align="left" colspan="2">
                                    <asp:DropDownList ID="lstDiscover" enabled="false" runat="server" AutoPostBack="True" OnSelectedIndexChanged="lstDiscover_SelectedIndexChanged"
                                        TabIndex="15">
                                        <asp:ListItem Selected = "true"> </asp:ListItem>
                                        <asp:ListItem>Yes</asp:ListItem>
                                        <asp:ListItem>No</asp:ListItem>
                                        <asp:ListItem>Yes - Existing</asp:ListItem>
                                    </asp:DropDownList><span style="color: red">*</span>
                                    <asp:Image ID="imgDiscover" runat="server" ImageUrl="/Application/Images/help.gif" /><b>
                                        <asp:HyperLink ID="lnkDiscover" Target="_blank" runat="server" CssClass="LinkSmall"
                                            NavigateUrl="~/Docs/Discover.html">Included</asp:HyperLink></b>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="3">
                                    <asp:Panel ID="pnlDiscoverNum" runat="server" Height="100%" Width="100%">
                                        <table border="0" width="100%">
                                            <tr>
                                                <td align="right" width="40%">
                                                    <asp:Label ID="lblExistingDiscoverNum" runat="server" Text="Existing Number" Font-Bold="False"></asp:Label></td>
                                                <td align="left" colspan="2">
                                                    <asp:TextBox ID="txtDiscoverNum" runat="server" MaxLength="15" Width="140px" TabIndex="16" OnTextChanged="textChangedEventHandler"></asp:TextBox>
                                                    <asp:LinkButton ID="lnkPrevDisc" runat="server" CssClass="Link" OnClick="lnkPrevDisc_Click">Edit</asp:LinkButton>
                                                    <asp:RequiredFieldValidator ID="ValidateDiscoverNum" runat="server" ControlToValidate="txtDiscoverNum"
                                                        ErrorMessage="Discover Number" EnableClientScript="False"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="REDiscNum" runat="server" ControlToValidate="txtDiscoverNum"
                                                        ErrorMessage="Numbers Only" ValidationExpression="[0-9]*$" EnableClientScript="False"></asp:RegularExpressionValidator></td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblAmex" runat="server" Text="Do you want to accept American Express?"
                                        Font-Bold="False"></asp:Label>
                                </td>
                                <td align="left" colspan="2">
                                    <asp:DropDownList ID="lstAmex" runat="server" AutoPostBack="True" OnSelectedIndexChanged="lstAmex_SelectedIndexChanged"
                                        TabIndex="17">
                                        <asp:ListItem Selected = "true"> </asp:ListItem>
                                        
                                        <asp:ListItem>Yes</asp:ListItem>
                                        <asp:ListItem>No</asp:ListItem>
                                        <asp:ListItem>Yes - Existing</asp:ListItem>
                                    </asp:DropDownList><span style="color: red">*</span>
                                    <asp:Image ID="imgAmex" runat="server" ImageUrl="/Application/Images/help.gif" />
                                    <asp:HyperLink ID="lnkAmex" Target="_blank" runat="server" CssClass="LinkSmall" NavigateUrl="~/Docs/AMEX.html"
                                        Font-Bold="True">Recommended</asp:HyperLink></td>
                            </tr>
                            <tr>
                                <td align="right" colspan="3">
                                    <asp:Panel ID="pnlAmexNum" runat="server" Height="100%" Width="100%">
                                        <table width="100%" border="0">
                                            <tr>
                                                <td align="right" width="40%">
                                                    <asp:Label ID="lblAmexNum" runat="server" Text="Existing Number" Font-Bold="False"></asp:Label></td>
                                                <td align="left" colspan="2">
                                                    <asp:TextBox ID="txtAmexNum" runat="server" MaxLength="10" Width="140px" TabIndex="18" OnTextChanged="textChangedEventHandler"></asp:TextBox>
                                                    <asp:LinkButton ID="lnkPrevAmex" runat="server" CssClass="Link" OnClick="lnkPrevAmex_Click">Edit</asp:LinkButton>
                                                    <asp:RequiredFieldValidator ID="ValidateAmexNum" runat="server" ControlToValidate="txtAmexNum"
                                                        ErrorMessage="Amex Number" EnableClientScript="False"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="REAmexNum" runat="server" ControlToValidate="txtAmexNum"
                                                        ErrorMessage="Numbers Only" ValidationExpression="[0-9]*$" EnableClientScript="False"></asp:RegularExpressionValidator></td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr><asp:Panel ID="pnlJCB1" runat="server" Height="100%" Width="100%">
                                <td align="right">
                                    <asp:Label ID="lblJCB" runat="server" Text="Do you want to accept JCB?" Font-Bold="False"></asp:Label></td>
                                <td align="left" colspan="2">
                                    <asp:DropDownList ID="lstJCB" runat="server" AutoPostBack="True" OnSelectedIndexChanged="lstJCB_SelectedIndexChanged"
                                        TabIndex="19">
                                        <asp:ListItem Selected="True"> </asp:ListItem>
                                        <asp:ListItem>No</asp:ListItem>
                                        <asp:ListItem>Yes</asp:ListItem>
                                        <asp:ListItem>Yes - Existing</asp:ListItem>
                                    </asp:DropDownList><span style="color: red">*</span>
                                    <asp:Image ID="imgJCB" runat="server" ImageUrl="/Application/Images/help.gif" />
                                </td>
                                </asp:Panel>
                            </tr>
                            <tr><asp:Panel ID="pnlOnlineDebit" runat="server" Height="100%" Width="100%">

                                     <td align="right">
                                    <asp:Label ID="lblOnlineDebit" runat="server" Text="Do you want to accept Online Debit?" Font-Bold="False"></asp:Label></td>
                                <td align="left" colspan="2">
                                    <asp:DropDownList ID="lstOnlineDebit" runat="server" AutoPostBack="True"
                                        TabIndex="19" OnSelectedIndexChanged="textChangedEventHandler">
                                        <asp:ListItem > </asp:ListItem>
                                        <asp:ListItem Selected="True">No</asp:ListItem>
                                        <asp:ListItem>Yes</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Image ID="imgOD" runat="server" ImageUrl="/Application/Images/help.gif" ToolTip="Help"/>    
                                    
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorOnlineDebit" runat="server" ErrorMessage="Online Debit"
                                        Font-Bold="False" ControlToValidate="lstOnlineDebit" EnableClientScript="False"></asp:RequiredFieldValidator>                      
                                </td>
                                </asp:Panel>
                            </tr>
                             <tr><asp:Panel ID="pnlEBT" runat="server" Height="100%" Width="100%">

                                <td align="right">
                                <asp:Label ID="lblEBT" runat="server" Text="Do you want to accept Electronic Benefits Transfer (EBT)?" Font-Bold="False"></asp:Label></td>
                                <td align="left" colspan="2">
                                    <asp:DropDownList ID="lstEBT" runat="server" AutoPostBack="True" OnSelectedIndexChanged="lstEBT_SelectedIndexChanged"
                                        TabIndex="19" >
                                        <asp:ListItem > </asp:ListItem>
                                        <asp:ListItem Selected="True">No</asp:ListItem>
                                        <asp:ListItem>Yes</asp:ListItem>
                                        <asp:ListItem>Yes - Existing</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Image ID="imgEBT" runat="server" ImageUrl="/Application/Images/help.gif" ToolTip="Help"/>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorEBT" runat="server" ErrorMessage="EBT"
                                        Font-Bold="False" ControlToValidate="lstEBT" EnableClientScript="False"></asp:RequiredFieldValidator>
                                </td>
                                </asp:Panel>
                            </tr>
                            
                            <tr><td style="height: 20px;" align="right"><asp:Label ID="lblUSDA" runat="server" Visible="false" Text="FNS/FBS #"></asp:Label></td>
                                &nbsp;&nbsp;&nbsp;<td align="left" colspan="2">
                                <asp:TextBox ID="txtUSDA" runat="server" MaxLength="7" Visible="false" >
                                </asp:TextBox></td></tr>
                            <tr>
                                <td align="right" colspan="3">
                                    <asp:Panel ID="pnlJCBNum" runat="server" Height="100%" Width="100%">
                                        <table border="0" width="100%">
                                            <tr>
                                                <td align="right" width="40%">
                                                    <asp:Label ID="lblJCBNum" runat="server" Text="Existing Number" Font-Bold="False"></asp:Label></td>
                                                <td align="left" colspan="2">
                                                    <asp:TextBox ID="txtJCBNum" runat="server" MaxLength="16" Width="140px" TabIndex="20" OnTextChanged="textChangedEventHandler"></asp:TextBox><asp:RequiredFieldValidator
                                                        ID="ValidateJCBNum" runat="server" ControlToValidate="txtJCBNum" ErrorMessage="JCB Number"
                                                        EnableClientScript="False"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="REJCBNum" runat="server" ControlToValidate="txtJCBNum"
                                                        ErrorMessage="Numbers Only" ValidationExpression="[0-9]*$" EnableClientScript="False"></asp:RegularExpressionValidator></td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td colspan="3" align="center">
                    <b><asp:HyperLink CssClass="Link" Target="_blank" ID="lnkAgreement" runat="server" NavigateUrl="https://www.apps.commercetech.com/Processing Info/Placement Agr 12-21-07.doc"
                            TabIndex="21" Font-Names="Arial">Agreement</asp:HyperLink></b><br />
                    <asp:Label ID="lblAgreement" runat="server" Text="Submit this form to signify that you have read, understood and agreed to the Terms and Conditions of the above agreement."></asp:Label>
                </td>
            </tr>
            <tr height="20px">
            </tr> 
            <tr>
                <td align="center" colspan="3" height="25px">
                    <asp:Label ID="lblBottom" runat="server" Text='Click "Submit" to save your application and continue to the next page.'></asp:Label><br/>
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" TabIndex="22" /></td>
            </tr>
            <!--<tr>
                <td align="right" colspan="3" height="25px">
                    <asp:Label ID="lblSave" runat="server" Text='Click "Save" to save your partially completed information.  You may finish completing this page another time'></asp:Label>
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" TabIndex="23" /></td>
            </tr>-->
            <tr>
                <td colspan="3" align="center">
                    <asp:Panel ID="pnlDiscover" runat="server" Style="display: none; z-index: 1;" Width="250px" CssClass="DivHelp">
                        Discover Acceptance is automatically included with merchant accounts for all US Merchants. International Merchants: Select the option Yes - Existing if you have 
                        an existing Discover number. Enter the existing number in the following textbox.</asp:Panel>
                    <asp:Panel ID="pnlAmex" runat="server" Style="display: none; z-index: 1;" Width="250px" CssClass="DivHelp">
                       Select the option Yes - Existing if you process over 500K/year or if you are a Franchise and you have an Amex Number.  Enter the existing number in the following textbox. <br/> Select the option No- If you do not want to accept American Express <br/> Select the option Yes- if you process less then 500k/year or if you are not a franchise.</asp:Panel>
                    <asp:Panel ID="pnlJCB" runat="server" Style="display: none; z-index: 1;" Width="250px" CssClass="DivHelp">
                        JCB Acceptance is automatically included with merchant accounts for all US Merchants. International Merchants: Select the option Yes - Existing if you have 
                        an existing JCB number. Enter the existing number in the following textbox.</asp:Panel>
                    <asp:Panel ID="pnlAvgTicket" runat="server" Style="display: none; z-index: 1;" Width="250px" CssClass="DivHelp">
                        Average Ticket: This is the average amount charged per sale.</asp:Panel>
                    <asp:Panel ID="pnlMaxTicket" runat="server" Style="display: none; z-index: 1;" Width="250px" CssClass="DivHelp">
                        Maximum Ticket: This is the maximum amount charged on sale. Maximum transaction rate must be greater than your average transaction.</asp:Panel>
                    <asp:Panel ID="pnlMonVol" runat="server" Style="display: none; z-index: 1;" Width="250px" CssClass="DivHelp">
                        Estimated Monthly Volume: Your average transaction is the average amount of one sale.Your monthly volume is your average transaction times the number of sales in a month.Your monthly volume should almost always be more than your average transaction value.</asp:Panel>
                                    <asp:Panel Style="display: none; z-index: 1; text-align: center;" ID="pnlOD" runat="server" Width="300px" CssClass="DivHelp">
            This service is for card present accounts and requries a pin pad for accepting pin debit transactions.
            </asp:Panel>
            <asp:Panel Style="display: none; z-index: 1; text-align: center;" ID="pnlEBTPopup" runat="server" Width="300px" CssClass="DivHelp">
            This service is for retail business wishing to accept Electronic Benefits Transfer, better known as green stamps.
            </asp:Panel>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <script type="text/javascript">
        function ConfirmPOS2013()
        {
            if ( confirm("Are you using POS 2013?"))
            {
                window.location.href="ContactPOS2013.aspx";
            }else
            {
                window.location.href="https://merchant.intuit.com/signup/?a=chkt&api_key=inTuIt&b=posmswoc&offer_code=posms_001_n-2991-19888";
            }        
        }
    </script>
    <script language="javascript" type="text/javascript">
function closepopup() {
$find('ModalPopupExtender1').hide();
}

</script>
</asp:Content>
