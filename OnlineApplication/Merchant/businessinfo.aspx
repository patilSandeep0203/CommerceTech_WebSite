<%@ Page Language="C#" MasterPageFile="/Application/Main.master" AutoEventWireup="true"
    CodeFile="businessinfo.aspx.cs" Inherits="Merchant_businessinfo" Title="Commerce Technologies - Online Application"
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
                        <td class="DivHeader" align="center" width="14%">
                            <asp:LinkButton ID="linkCardPct" CssClass="MenuHeader" Text="Card Percentages" runat="server" OnClick="linkCardPctClick"/>
                        </td>
                        <td class="DivHeader" align="center" width="14%">
	                        <asp:LinkButton ID="linkCompany" CssClass="MenuHeader" Text="Contact Info" runat="server" OnClick="linkCompanyClick"/>
                        </td>
                        <td class="DivHeader2" align="center" width="14%">
                            <asp:LinkButton ID="linkBusiness" CssClass="MenuHeader" Text="Business Info" runat="server" OnClick="linkBusinessClick"/>
                        </td>
                        <td class="DivHeader" align="center" width="14%">
                            <asp:LinkButton ID="linkPrincipal" CssClass="MenuHeader" Text="Principal Info" runat="server" OnClick="linkPrincipalClick"/>
                        </td>
                        <td class="DivHeader" align="center" width="14%">
                            <asp:LinkButton ID="linkBanking" CssClass="MenuHeader" Text="Banking Info" runat="server" OnClick="linkBankingClick"/>
                        </td>
                        <td class="DivHeader" align="center" width="14%">
                           <a href="/Application/UploadDocuSignDocs.aspx" class="MenuCss">Upload Documents</a> 
                        </td>
                        <td class="DivHeader" align="center" width="14%">
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
                    <img name="mbusiness" style="border: 0" alt="" src="/Application/Images/oa_Business.gif" />
                    <a href="principalinfo.aspx" onmouseover="document.mprincipal.src='/Application/Images/oa_Principal_mouseout.gif'"
                        onmouseout="document.mprincipal.src='/Application/Images/oa_Principal_mouseover.gif'">
                        <img name="mprincipal" style="border: 0" alt="" src="/Application/Images/oa_Principal_mouseover.gif" /></a>
                    <a href="bankinginfo.aspx" onmouseover="document.mbanking.src='/Application/Images/oa_Banking_mouseout.gif'"
                        onmouseout="document.mbanking.src='/Application/Images/oa_Banking_mouseover.gif'">
                        <img name="mbanking" style="border: 0" alt="" src="/Application/Images/oa_Banking_mouseover.gif" /></a>
                </td>
            </tr>-->
            <tr>
                <td align="center" colspan="3">
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <cc1:PopupControlExtender ID="PopupControlExtender1" runat="server" PopupControlID="pnlOwnership"
                        TargetControlID="imgOwnership" Position="Bottom" />
                    <cc1:PopupControlExtender ID="PopupControlExtender2" runat="server" PopupControlID="pnlTaxID"
                        TargetControlID="imgTaxID" Position="Bottom" />
                    <cc1:PopupControlExtender ID="PopupControlExtender3" runat="server" PopupControlID="pnlProduct"
                        TargetControlID="imgProducts" Position="Bottom" />
                    <cc1:PopupControlExtender ID="PopupControlExtender4" runat="server" PopupControlID="pnlCTMF"
                        TargetControlID="imgCTMF" Position="Bottom" />
                </td>
            </tr>
            <tr>
                <td align="center" colspan="3">
                    <asp:Label ID="lblHeader" runat="server" Text="Please provide the following Business information about your company."></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="3">
                    <asp:Label ID="lblError" runat="server" BackColor="Red" Font-Size="Medium" ForeColor="White"
                        Visible="False"></asp:Label><br />
                    <asp:ValidationSummary ID="ValidateSummary" runat="server" BackColor="#FFC0C0" BorderColor="red"
                        BorderWidth="1px" ForeColor="Black" HeaderText="Please check the fields marked in red."
                        Width="250px" />
                </td>
            </tr>
            <tr>
                <td align="center" colspan="3">
                    <span class="LabelsRed"><b>*</b> - denotes a required field</span></td>
            </tr>
            <tr>
                <td colspan="3" align="center" class="DivHeader">
                    <b><span class="MenuHeader">BILLING INFORMATION</span></b></td>
            </tr>
            <tr>
                <td width="40%">
                </td>
                <td align="left" colspan="2">
                    <asp:CheckBox ID="chkBillingAddrSame" runat="server" Text="Billing address same as Business address"
                        OnCheckedChanged="chk_CheckedChanged" AutoPostBack="True" TabIndex="1" />
                    <br />
                    <span class="LabelsBodySmall">(Monthly Billing Statements will be mailed to this address)</span></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblStreet" runat="server" Text="Address" Font-Bold="False"></asp:Label></td>
                <td colspan="2" style="text-align: left">
                    <asp:TextBox ID="txtAddress" runat="server" Width="140px" MaxLength="30" TabIndex="2" OnTextChanged="textChangedEventHandler"></asp:TextBox><span
                        style="color: red">*</span><asp:RequiredFieldValidator ID="ValidateAddress" runat="server"
                            ControlToValidate="txtAddress" ErrorMessage="Address" Font-Bold="False" EnableClientScript="False"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td align="right">
                </td>
                <td colspan="2" style="text-align: left">
                    <asp:TextBox ID="txtAddress2" runat="server" Width="140px" MaxLength="30" TabIndex="3" OnTextChanged="textChangedEventHandler"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lclCity" runat="server" Text="City" Font-Bold="False"></asp:Label></td>
                <td colspan="2" style="text-align: left">
                    <asp:TextBox ID="txtCity" runat="server" Width="140px" MaxLength="45" TabIndex="4" OnTextChanged="textChangedEventHandler"></asp:TextBox><span
                        style="color: red">*</span><asp:RequiredFieldValidator ID="ValidateCity" runat="server"
                            ControlToValidate="txtCity" ErrorMessage="City" Font-Bold="False" EnableClientScript="False"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblState" runat="server" Text="State" Font-Bold="False"></asp:Label></td>
                <td style="text-align: left">
                    <asp:DropDownList ID="lstState" runat="server" TabIndex="5" OnSelectedIndexChanged="textChangedEventHandler">
                    </asp:DropDownList><asp:Label ID="lblStateStar" runat="server" Font-Bold="False" ><span style="color: #ff0000; font-size:20px">*</span></asp:Label>
                    <asp:RequiredFieldValidator ID="ValidateState" runat="server" ControlToValidate="lstState"
                        ErrorMessage="State" Font-Bold="False" EnableClientScript="False"></asp:RequiredFieldValidator></td>
                <td style="text-align: left">
                    <asp:Label ID="lblRegion" runat="server" Text="Region"></asp:Label>
                    <asp:TextBox ID="txtRegion" runat="server" Width="100px" MaxLength="40" TabIndex="6" OnTextChanged="textChangedEventHandler"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblZipCode" runat="server" Text="Zip Code" Font-Bold="False"></asp:Label></td>
                <td colspan="2" style="text-align: left">
                    <asp:TextBox ID="txtZipCode" runat="server" Width="100px" MaxLength="16" TabIndex="7" OnTextChanged="textChangedEventHandler"></asp:TextBox><span
                        style="color: red">*</span>
                    <asp:Label ID="lblIntlZip" runat="server" Font-Size="Smaller" Text="Enter '00000' if not applicable"
                        Visible="False"></asp:Label>
                    <asp:RequiredFieldValidator ID="ValidateZip" runat="server" ControlToValidate="txtZipCode"
                        ErrorMessage="Zip Code" Font-Bold="False" EnableClientScript="False"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblCountry" runat="server" Text="Country" Font-Bold="False"></asp:Label></td>
                <td colspan="2" style="text-align: left">
                    <asp:DropDownList ID="lstCountry" runat="server" TabIndex="8" AutoPostBack="true"
                        OnSelectedIndexChanged="lstCountry_SelectedIndexChanged">
                    </asp:DropDownList><span style="color: red">*</span>
                    <asp:RequiredFieldValidator ID="ValidateCountry" runat="server" ControlToValidate="lstCountry"
                        ErrorMessage="Country" Font-Bold="False" EnableClientScript="False"></asp:RequiredFieldValidator>
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
                <td colspan="3" align="center" class="DivHeader">
                    <b><span class="MenuHeader">BUSINESS INFORMATION</span></b></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblOwnership" runat="server" Text="Ownership/Legal Status" Font-Bold="False"></asp:Label></td>
                <td colspan="2" style="text-align: left">
                    <asp:DropDownList ID="lstOwnership" runat="server" TabIndex="9" OnTextChanged="textChangedEventHandler">
                    </asp:DropDownList><span style="color: red">*</span>
                    <asp:Image ID="imgOwnership" runat="server" ImageUrl="/Application/Images/help.gif" />
                    <asp:RequiredFieldValidator ID="ValidateOwnership" runat="server" ControlToValidate="lstOwnership"
                        ErrorMessage="Ownership/Legal Status" EnableClientScript="False"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblTaxID" runat="server" Text="Federal Tax ID" Font-Bold="False" ></asp:Label></td>
                <td colspan="2" style="text-align: left">
                
                    <asp:TextBox ID="txtFederalID" runat="server" Width="100px" MaxLength="20" TabIndex="10" OnTextChanged="textChangedEventHandler"></asp:TextBox><asp:Label ID="lblFedTaxIDStar" runat="server" Font-Bold="False"><span
                        style="color: red; font-size:20px">*</span></asp:Label>
                    <asp:LinkButton ID="lnkFederalTaxID" CssClass="Link" runat="server" OnClick="lnkFederalTaxID_Click">Edit</asp:LinkButton>
                    <asp:Image ID="imgTaxID" runat="server" ImageUrl="/Application/Images/help.gif" />
                    <asp:RequiredFieldValidator ID="ValidateTaxID" runat="server" ControlToValidate="txtFederalID"
                        ErrorMessage="Federal Tax ID" EnableClientScript="False"></asp:RequiredFieldValidator>
                    <!--<asp:RegularExpressionValidator ID="RETaxID" runat="server" ControlToValidate="txtFederalID"
                        ErrorMessage="Numbers Only" ValidationExpression="[0-9]*$" EnableClientScript="False"></asp:RegularExpressionValidator>--></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblTIBL" runat="server" Text="Time in Business" Font-Bold="False"></asp:Label></td>
                <td colspan="2" style="text-align: left">
                    <asp:TextBox ID="txtYIB" runat="server" Width="50px" MaxLength="4" TabIndex="11" OnTextChanged="textChangedEventHandler"></asp:TextBox><span
                        style="color: red">*</span>
                    <asp:Label ID="lblYears" runat="server" Text="Years"></asp:Label>
                    <asp:RequiredFieldValidator ID="ValidateYears" runat="server" ControlToValidate="txtYIB"
                        ErrorMessage="Years" EnableClientScript="False"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="REYIB" runat="server" ControlToValidate="txtYIB"
                        ErrorMessage="Numbers Only" ValidationExpression="[0-9]*$" EnableClientScript="False"></asp:RegularExpressionValidator>&nbsp;
                    <asp:Label ID="lblAnd" runat="server" Text="And"></asp:Label>
                    <asp:TextBox ID="txtMIB" runat="server" Width="50px" MaxLength="4" TabIndex="12" OnTextChanged="textChangedEventHandler"></asp:TextBox><span
                        style="color: red">*</span>
                    <asp:Label ID="lblMonths" runat="server" Text="Months"></asp:Label>
                    <asp:RequiredFieldValidator ID="RequiredFieldMID" runat="server" ControlToValidate="txtMIB"
                        ErrorMessage="Months" EnableClientScript="False"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblNumberLocs" runat="server" Text="Number of Locations" Font-Bold="False"></asp:Label></td>
                <td colspan="2" style="text-align: left">
                    <asp:TextBox ID="txtNumLocs" runat="server" Width="50px" MaxLength="15" TabIndex="13" OnTextChanged="textChangedEventHandler"></asp:TextBox><span
                        style="color: red">*</span>
                    <asp:RequiredFieldValidator ID="ValidateLocations" runat="server" ControlToValidate="txtNumLocs"
                        ErrorMessage="Number of Locations" EnableClientScript="False"></asp:RequiredFieldValidator>
                    <!--<asp:RegularExpressionValidator ID="RENumLocs" runat="server" ControlToValidate="txtNumLocs"
                        ErrorMessage="Numbers Only" ValidationExpression="[0-9]*$" EnableClientScript="False"></asp:RegularExpressionValidator>-->
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblProductsSold" runat="server" Text="Products/Services Sold (Be Descriptive. 150 max Characters)"
                        Font-Bold="False"></asp:Label></td>
                <td colspan="2" style="text-align: left">
                    <asp:TextBox ID="txtProductsSold" runat="server" Height="50px" TextMode="MultiLine"
                        Width="180px" MaxLength="150" TabIndex="14" onKeyUp="javascript:CountProduct(this);" OnTextChanged="textChangedEventHandler"></asp:TextBox><span style="color: red">*</span>
                    <asp:Image ID="imgProducts" runat="server" ImageUrl="/Application/Images/help.gif" />
                    <asp:RegularExpressionValidator runat="server" ID="productLengthVal"
    ControlToValidate="txtProductsSold"
    ValidationExpression="^[\s\S]{0,150}$"
    ErrorMessage="Products/Services Sold: Please enter a maximum of 150 characters"
    Display="Dynamic">*</asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="ValidateProductsSold" runat="server" ControlToValidate="txtProductsSold"
                        ErrorMessage="Products Sold" EnableClientScript="False"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblApproxDays" runat="server" Text="Approx. Number of Days Until Product/Service is delivered"
                        Font-Bold="False"></asp:Label></td>
                <td colspan="2" style="text-align: left">
                    <asp:TextBox ID="txtNumDays" runat="server" Width="180px" MaxLength="10" TabIndex="15" OnTextChanged="textChangedEventHandler"></asp:TextBox><span
                        style="color: red">*</span>
                    <asp:RequiredFieldValidator ID="ValidateApproxDays" runat="server" ControlToValidate="txtNumDays"
                        ErrorMessage="Approx. number of days" EnableClientScript="False"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RENumDays" runat="server" ControlToValidate="txtNumDays"
                        ErrorMessage="Numbers Only" ValidationExpression="[0-9]*$" EnableClientScript="False"></asp:RegularExpressionValidator></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblAddlComments" runat="server" Text="Additional Delivery Comments (250 max Characters)"
                        Font-Bold="False"></asp:Label></td>
                <td colspan="2" style="text-align: left">
                    <asp:TextBox ID="txtAddlComments" runat="server" Height="50px" TextMode="MultiLine"
                        Width="180px" MaxLength="250" TabIndex="16" onKeyUp="javascript:CountDeliveryComment(this);" OnTextChanged="textChangedEventHandler"></asp:TextBox>
                        <asp:RegularExpressionValidator runat="server" ID="RegularExpressiontxtAddlComments"
    ControlToValidate="txtProductsSold"
    ValidationExpression="^[\s\S]{0,250}$"
    ErrorMessage="Additional Delivery Comments: Please enter a maximum of 250 characters"
    Display="Dynamic">*</asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblRefund" runat="server" Text="Refund Policy (Choose one)" Font-Bold="False"></asp:Label>
                </td>
                <td colspan="2" style="text-align: left">
                    <asp:DropDownList ID="lstRefundPolicy" runat="server" AutoPostBack="True" OnSelectedIndexChanged="lstRefundPolicy_SelectedIndexChanged"
                        TabIndex="17">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem>Exchange Only</asp:ListItem>
                        <asp:ListItem>No Refund</asp:ListItem>
                        <asp:ListItem>Refund within 30 days</asp:ListItem>
                        <asp:ListItem>Other</asp:ListItem>
                    </asp:DropDownList><span style="color: red">*</span><asp:RequiredFieldValidator ID="ValidateRefund"
                        runat="server" ControlToValidate="lstRefundPolicy" ErrorMessage="Refund Policy"
                        Font-Bold="False" EnableClientScript="False"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblOtherRefund" runat="server" Text="If Other, Specify" Font-Bold="False"></asp:Label></td>
                <td colspan="2" style="text-align: left">
                    <asp:TextBox ID="txtRefundOther" runat="server" Width="100px" MaxLength="250" TabIndex="18" OnTextChanged="textChangedEventHandler"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ValidateOtherRefund" runat="server" ControlToValidate="txtRefundOther"
                        ErrorMessage="Other Refund Policy" Font-Bold="False" EnableClientScript="False"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblBankruptcy" runat="server" Text="Have you filed bankruptcy before?"
                        Font-Bold="False"></asp:Label></td>
                <td colspan="2" style="text-align: left">
                    <asp:DropDownList ID="lstBankruptcy" runat="server" TabIndex="19" OnSelectedIndexChanged="textChangedEventHandler">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem>Yes</asp:ListItem>
                        <asp:ListItem>No</asp:ListItem>
                    </asp:DropDownList><span style="color: red"></span></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblPrevProcessed" runat="server" Text="Have you processed credit cards before?"
                        Font-Bold="False"></asp:Label></td>
                <td colspan="2" style="text-align: left">
                    <asp:DropDownList ID="lstPrevProcessed" runat="server" AutoPostBack="True" OnSelectedIndexChanged="lstPrevProcessed_SelectedIndexChanged"
                        TabIndex="20">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem>Yes</asp:ListItem>
                        <asp:ListItem>No</asp:ListItem>
                    </asp:DropDownList><span style="color: red">*</span>
                    <asp:RequiredFieldValidator ID="ValidatePrevProcessed" runat="server" ControlToValidate="lstPrevProcessed"
                        ErrorMessage="Processed credit cards before" EnableClientScript="False"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td align="left" colspan="3" valign="top">
                    <asp:Panel ID="pnlPrevProcessed" runat="server" Height="100%" Width="100%" HorizontalAlign="Left"
                        Visible="False">
                        <table border="0" width="100%" cellpadding="2" cellspacing="0">
                            <tr>
                                <td Width="40%" align="right">
                                    <asp:Label ID="lblPrevProcessor" runat="server" Text="If Yes, what was the processors name?"
                                        Font-Bold="False"></asp:Label></td>
                                <td colspan="2" align="left">
                                    <asp:DropDownList ID="lstProcessor" runat="server" AutoPostBack="True" OnSelectedIndexChanged="lstProcessor_SelectedIndexChanged"
                                        TabIndex="21">
                                    </asp:DropDownList><span style="color: red">*</span><asp:RequiredFieldValidator ID="ValidateProcessor"
                                        runat="server" ControlToValidate="lstProcessor" ErrorMessage="Previous Processor Name"
                                        EnableClientScript="False"></asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblOtherProcessor" runat="server" Text="If Other, specify" Font-Bold="False"></asp:Label></td>
                                <td colspan="2" align="left">
                                    <asp:TextBox ID="txtOtherProcessor" runat="server" Width="140px" MaxLength="64" TabIndex="22" OnTextChanged="textChangedEventHandler"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="ValidateOtherProcessor" runat="server" ControlToValidate="txtOtherProcessor"
                                        Enabled="false" ErrorMessage="Other Processor" EnableClientScript="False"></asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblPrevMerchantNo" runat="server" Text="What was the previous Merchant Account Number?"
                                        Font-Bold="False"></asp:Label></td>
                                <td colspan="2" align="left">
                                    <asp:TextBox ID="txtPrevMerchant" runat="server" Width="140px" MaxLength="20" TabIndex="23" OnTextChanged="textChangedEventHandler"></asp:TextBox><span
                                        style="color: red">*</span>
                                    <asp:LinkButton ID="lnkPrevMerchNum" CssClass="Link" runat="server" OnClick="lnkPrevMerchNum_Click">Edit</asp:LinkButton>
                                    <asp:RequiredFieldValidator ID="ValidatePrevMerchant" runat="server" ControlToValidate="txtPrevMerchant"
                                        ErrorMessage="Merchant Account No." EnableClientScript="False"></asp:RequiredFieldValidator>
                                    <!--<asp:RegularExpressionValidator ID="REPrevMerchant" runat="server" ControlToValidate="txtPrevMerchant"
                                        ErrorMessage="Numbers Only" ValidationExpression="^[0-9]{1,16}$" EnableClientScript="False"></asp:RegularExpressionValidator>--></td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblReasonLeaving" runat="server" Text="What was the reason for leaving the previous processor? "
                                        Font-Bold="False"></asp:Label></td>
                                <td colspan="2" align="left">
                                    <asp:TextBox ID="txtReasonForLeaving" runat="server" Height="50px" TextMode="MultiLine"
                                        Width="140px" MaxLength="100" TabIndex="24" OnTextChanged="textChangedEventHandler"></asp:TextBox><span style="color: red">*</span><asp:RequiredFieldValidator
                                            ID="ValidateReasonLeaving" runat="server" ControlToValidate="txtReasonForLeaving"
                                            ErrorMessage="Reason for Leaving" EnableClientScript="False"></asp:RequiredFieldValidator></td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <p style="margin-top: 0; margin-bottom: 0; text-align: right;">
                        <asp:Label ID="lblCTMF" runat="server" Text="Have you been terminated by a processor before  (ie, placed on the Combined Terminated Merchant File CTMF Match)?"
                            Font-Bold="False"></asp:Label></p>
                </td>
                <td colspan="2" style="text-align: left;">
                    <asp:DropDownList ID="lstCTMF" runat="server" TabIndex="25" OnSelectedIndexChanged="textChangedEventHandler">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem>Yes</asp:ListItem>
                        <asp:ListItem>No</asp:ListItem>
                    </asp:DropDownList><span style="color: red">*</span>
                    <asp:Image ID="imgCTMF" runat="server" ImageUrl="/Application/Images/help.gif" />
                    <asp:RequiredFieldValidator ID="ValidateCTMF" runat="server" ControlToValidate="lstCTMF"
                        ErrorMessage="CTMF" EnableClientScript="False"></asp:RequiredFieldValidator></td>
            </tr>
            <tr height="20px">                
            </tr>
            <tr>
                <td align="center" colspan="3" height="25px">
                    <asp:Label ID="lblSubmit" runat="server" Text='Click "Submit" to save your application and continue to the next page.'></asp:Label><br/>
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" TabIndex="26" /></td>
            </tr>
            <!--<tr>
                <td align="right" colspan="3" height="25px">
                    <asp:Label ID="lblSave" runat="server" Text='Click "Save" to save your partially completed information.  You may finish completing this page another time'></asp:Label>
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" TabIndex="27" /></td>
            </tr>-->            
            <tr>
                <td colspan="3" align="center">
                    <asp:Panel ID="pnlOwnership" runat="server" Style="display: none; z-index: 1;" Width="250px"
                        CssClass="DivHelp">
                        Select how you have registered your business i.e., legal/medical,
                        sole proprietorship, partnership, corporation, LLC, etc. Select Others if none of
                        the options in the list apply to you.</asp:Panel>
                    <asp:Panel ID="pnlTaxID" runat="server" Style="display: none; z-index: 1;" Width="250px"
                        CssClass="DivHelp">
                        9-digit number. Example: 123456789. This is also known as a tax
                        payer ID. In case of sole proprietorship, this will be your Social Security Number.
                        (No Dashes)</asp:Panel>
                    <asp:Panel ID="pnlProduct" runat="server" Style="display: none; z-index: 1;" Width="250px"
                        CssClass="DivHelp">
                        Products/Services sold: A brief description of the products/services sold by your
                        company. Please limit your description to 150 characters.</asp:Panel>
                    <asp:Panel ID="pnlCTMF" runat="server" Style="display: none; z-index: 1;" Width="250px"
                        CssClass="DivHelp">
                        Your former merchant service provider will put you on this list if you have done
                        anything illegal or have had excessive chargebacks that have not been paid.</asp:Panel>
                </td>
            </tr>
        </table>
    </asp:Panel>
            <script language="javascript" type="text/javascript">
function closepopup() {
$find('ModalPopupExtender1').hide();
}

</script>
</asp:Content>
