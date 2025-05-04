<%@ Page Language="C#" MasterPageFile="/Application/Main.master" AutoEventWireup="true"
    CodeFile="principalinfo.aspx.cs" Inherits="Merchant_principalinfo" Title="Commerce Technologies - Online Application"
    Theme="TextBoxSkin" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentHolder" runat="Server">
    <asp:Panel ID="pnlMainPage" runat="server">
        <table border="0" cellpadding="2" cellspacing="0" Width="100%" class="DivGreen">
            <tr>
                <td colspan="3" align="center" class="DivHeader2" height="26px">
                    <b><span class="MenuHeader">Merchant Account Signup Information</span></b>
                </td>
            </tr>
            <tr>
            <td colspan="3" align="center" class="DivHeader">
                <table border="0" cellpadding="0" cellspacing="0" Width="100%">
                    <tr>
                        <td class="DivHeader" align="center" width="16%">
                            <asp:LinkButton ID="linkCardPct" CssClass="MenuHeader" Text="Card Percentages" runat="server" OnClick="linkCardPctClick"/>
                        </td>
                        <td class="DivHeader" align="center" width="16%">
	                        <asp:LinkButton ID="linkCompany" CssClass="MenuHeader" Text="Contact Information" runat="server" OnClick="linkCompanyClick"/>
                        </td>
                        <td class="DivHeader" align="center" width="16%">
                            <asp:LinkButton ID="linkBusiness" CssClass="MenuHeader" Text="Business Information" runat="server" OnClick="linkBusinessClick"/>
                        </td>
                        <td class="DivHeader2" align="center" width="16%">
                            <asp:LinkButton ID="linkPrincipal" CssClass="MenuHeader" Text="Principal Information" runat="server" OnClick="linkPrincipalClick"/>
                        </td>
                        <td class="DivHeader" align="center" width="16%">
                            <asp:LinkButton ID="linkBanking" CssClass="MenuHeader" Text="Banking Information" runat="server" OnClick="linkBankingClick"/>
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
                        onmouseout="document.mbusiness.src='/Application/Images/oa_Business_mouseover.gif'">
                        <img name="mbusiness" style="border: 0" alt="" src="/Application/Images/oa_Business_mouseover.gif" /></a>
                    <img name="mprincipal" style="border: 0" alt="" src="/Application/Images/oa_Principal.gif" />
                    <a href="bankinginfo.aspx" onmouseover="document.mbanking.src='/Application/Images/oa_Banking_mouseout.gif'"
                        onmouseout="document.mbanking.src='/Application/Images/oa_Banking_mouseover.gif'">
                        <img name="mbanking" style="border: 0" alt="" src="/Application/Images/oa_Banking_mouseover.gif" /></a>
                </td>
            </tr>-->
            <tr>
                <td colspan="3" align="center">
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <cc1:PopupControlExtender ID="PopupControlExtender1" runat="server" PopupControlID="pnlSSN"
                        TargetControlID="imgP1SSN" Position="Bottom" />
                    <cc1:PopupControlExtender ID="PopupControlExtender2" runat="server" PopupControlID="pnlOwnership"
                        TargetControlID="imgP1Ownership" Position="Bottom" />
                    <cc1:PopupControlExtender ID="PopupControlExtender3" runat="server" PopupControlID="pnlSSN"
                        TargetControlID="imgP2SSN" Position="Bottom" />
                    <cc1:PopupControlExtender ID="PopupControlExtender4" runat="server" PopupControlID="pnlOwnership"
                        TargetControlID="imgP2Ownership" Position="Bottom" />
                </td>
            </tr>
            <tr>
                <td align="center" colspan="3">
                    <asp:Label ID="lblHeader" runat="server" Text="Please provide the following information about your company's Principals."></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="3">
                    <asp:Label ID="lblError" runat="server" BackColor="Red" Font-Size="Medium" ForeColor="White"
                        Visible="False"></asp:Label><br /><asp:ValidationSummary ID="ValidateSummary" runat="server"
                            BackColor="#FFC0C0" BorderColor="red" BorderWidth="1px" ForeColor="Black" HeaderText="Please check the fields marked in red."
                            Width="250px" />
                </td>
            </tr>
            <tr>
                <td align="center" colspan="3">
                    <span class="LabelsRed"><b>*</b> - denotes a required field</span>
                </td>
            </tr>
            <tr>
                <td colspan="3" align="center" class="DivHeader">
                    <b><span class="MenuHeader">PRINCIPAL #1</span></b>
                </td>
            </tr>
            <tr>
                <td align="right" width="40%">
                    <asp:Label ID="lblP1First" runat="server" Text="First Name" Font-Bold="False"></asp:Label></td>
                <td align="left" colspan="2">
                    <asp:TextBox ID="txtP1FirstName" runat="server" MaxLength="32" Width="140px" TabIndex="1" OnTextChanged="textChangedEventHandler"></asp:TextBox><span
                        style="color: red">*</span>
                    <asp:RequiredFieldValidator ID="ValidateP1FirstName" runat="server" ErrorMessage="First Name"
                        ControlToValidate="txtP1FirstName" EnableClientScript="False"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblP1Middle" runat="server" Text="Middle Name" Font-Bold="False"></asp:Label></td>
                <td align="left" colspan="2">
                    <asp:TextBox ID="txtP1MiddleName" runat="server" MaxLength="32" Width="140px" TabIndex="2"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblP1Last" runat="server" Text="Last Name" Font-Bold="False"></asp:Label></td>
                <td align="left" colspan="2">
                    <asp:TextBox ID="txtP1LastName" runat="server" MaxLength="32" Width="140px" TabIndex="3" OnTextChanged="textChangedEventHandler"></asp:TextBox><span
                        style="color: red">*</span>
                    <asp:RequiredFieldValidator ID="ValidateP1LastName" runat="server" ErrorMessage="Last Name"
                        ControlToValidate="txtP1LastName" EnableClientScript="False"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblP1Email" runat="server" Font-Bold="False" Text="Email Address"></asp:Label></td>
                <td align="left" colspan="2">
                    <asp:TextBox ID="txtP1Email" runat="server" MaxLength="60" TabIndex="4" Width="140px" OnTextChanged="textChangedEventHandler"></asp:TextBox><span
                        style="color: red">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtP1Email"
                        ErrorMessage="Email" Font-Bold="False" EnableClientScript="False"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                            ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Email Address"
                            ControlToValidate="txtP1Email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                            EnableClientScript="False"></asp:RegularExpressionValidator></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblP1Title" runat="server" Text="Job Title" Font-Bold="False"></asp:Label></td>
                <td align="left" colspan="2">
                    <asp:TextBox ID="txtP1JobTitle" runat="server" MaxLength="32" Width="140px" TabIndex="5" OnTextChanged="textChangedEventHandler"></asp:TextBox><span
                        style="color: red">*</span>
                    <asp:RequiredFieldValidator ID="ValidateP1JobTitle" runat="server" ErrorMessage="Job Title"
                        ControlToValidate="txtP1JobTitle" EnableClientScript="False"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblP1Ownership" runat="server" Text="Ownership Percentage" Font-Bold="False"></asp:Label>
                </td>
                <td align="left" colspan="2">
                    <asp:TextBox ID="txtP1OwnershipPCT" runat="server" Width="50px" MaxLength="8" TabIndex="6" OnTextChanged="textChangedEventHandler"></asp:TextBox>
                    <asp:Label ID="lblP1PCT" runat="server" Text="%"></asp:Label><span style="color: red">*</span>
                    <asp:Image ID="imgP1Ownership" runat="server" ImageUrl="/Application/Images/help.gif" />
                    <asp:RequiredFieldValidator ID="ValidateP1Ownership" runat="server" ErrorMessage="Ownership Percentage"
                        ControlToValidate="txtP1OwnershipPCT" Font-Bold="False" EnableClientScript="False"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="REP1PCT" runat="server" ControlToValidate="txtP1OwnershipPCT"
                        ErrorMessage="Numbers Only" ValidationExpression="[0-9]*$" Font-Bold="False"
                        EnableClientScript="False"></asp:RegularExpressionValidator>
                         <asp:RangeValidator id="P1OwnershipValidate"
           ControlToValidate="txtP1OwnershipPCT"
           MinimumValue="0"
           MaximumValue="100"
           Type="Integer"
           EnableClientScript="false"
            ErrorMessage="The Ownership Percentage must be from 0 to 100!"
           runat="server"/>
                        </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblP1Address" runat="server" Text="Home Address" Font-Bold="False"></asp:Label></td>
                <td align="left" colspan="2">
                    <asp:RadioButton ID="btnP1SameAsBusiness" runat="server" Font-Size="8pt" GroupName="P1Address"
                        Text="Same as Business" AutoPostBack="True" OnCheckedChanged="btnP1SameAsBusiness_CheckedChanged"
                        TabIndex="7" />&nbsp;
                    <asp:RadioButton ID="btnP1Other" runat="server" Font-Size="8pt" GroupName="P1Address"
                        Text="Other" AutoPostBack="True" OnCheckedChanged="btnP1SameAsBusiness_CheckedChanged"
                        TabIndex="7" />&nbsp;
                    <asp:RadioButton ID="btnP1SameAsBilling" runat="server" Font-Size="8pt" GroupName="P1Address"
                        Text="Same as Billing" AutoPostBack="True" OnCheckedChanged="btnP1SameAsBusiness_CheckedChanged"
                        TabIndex="8" /></td>
            </tr>
            <tr>
                <td align="right" valign="middle">
                    <asp:Label ID="lblP1Street" runat="server" Text="Address" Font-Bold="False"></asp:Label></td>
                <td align="left" colspan="2">
                    <asp:TextBox ID="txtP1Address" runat="server" MaxLength="30" Width="140px" TabIndex="8" OnTextChanged="textChangedEventHandler"></asp:TextBox><span
                        style="color: red">*</span>
                    <asp:RequiredFieldValidator ID="ValidateP1Address" runat="server" ErrorMessage="Address"
                        Font-Bold="False" ControlToValidate="txtP1Address" EnableClientScript="False"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td align="right" valign="middle">
                </td>
                <td align="left" colspan="2">
                    <asp:TextBox ID="txtP1Address2" runat="server" MaxLength="30" Width="140px" TabIndex="9" OnTextChanged="textChangedEventHandler"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblP1City" runat="server" Text="City" Font-Bold="False"></asp:Label></td>
                <td align="left" colspan="2">
                    <asp:TextBox ID="txtP1City" runat="server" MaxLength="20" Width="140px" TabIndex="10" OnTextChanged="textChangedEventHandler"></asp:TextBox><span
                        style="color: red">*</span>
                    <asp:RequiredFieldValidator ID="ValidateP1City" runat="server" ErrorMessage="City"
                        Font-Bold="False" ControlToValidate="txtP1City" EnableClientScript="False"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblP1State" runat="server" Text="State" Font-Bold="False"></asp:Label></td>
                <td align="left">
                    <asp:DropDownList ID="lstP1State" runat="server" TabIndex="11" OnSelectedIndexChanged="textChangedEventHandler">
                    </asp:DropDownList><asp:Label ID="lblP1StateStar" runat="server" Font-Bold="False"><span style="color: red; font-size:20px">*</span></asp:Label>
                    <asp:RequiredFieldValidator ID="ValidateP1State" runat="server" ErrorMessage="State"
                        ControlToValidate="lstP1State" EnableClientScript="False"></asp:RequiredFieldValidator>
                </td>
                <td align="left">
                    <asp:Label ID="lblP1Region" runat="server" Text="Region" Font-Bold="False"></asp:Label>&nbsp;
                    <asp:TextBox ID="txtP1Region" runat="server" MaxLength="40" Width="100px" TabIndex="12" OnTextChanged="textChangedEventHandler"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblP1Zip" runat="server" Text="Zip Code" Font-Bold="False"></asp:Label></td>
                <td align="left" colspan="2">
                    <asp:TextBox ID="txtP1ZipCode" runat="server" MaxLength="12" Width="140px" TabIndex="13" OnTextChanged="textChangedEventHandler"></asp:TextBox><span
                        style="color: red">*</span>
                    <asp:RequiredFieldValidator ID="ValidateP1ZipCode" runat="server" ErrorMessage="Zip Code"
                        Font-Bold="False" ControlToValidate="txtP1ZipCode" EnableClientScript="False"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblP1Country" runat="server" Text="Country" Font-Bold="False"></asp:Label></td>
                <td align="left" colspan="2">
                    <asp:DropDownList ID="lstP1Country" runat="server" TabIndex="14" AutoPostBack="true" OnSelectedIndexChanged="lstP1Country_SelectedIndexChanged">
                    </asp:DropDownList><span style="color: red">*</span>
                    <asp:RequiredFieldValidator ID="ValidateP1Country" runat="server" ErrorMessage="Country"
                        ControlToValidate="lstP1Country" EnableClientScript="False"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td align="right" valign="top">
                    <asp:Label ID="lblP1YAA" runat="server" Text="Time at this address?" Font-Bold="False"></asp:Label></td>
                <td align="left" colspan="2">
                    <asp:TextBox ID="txtP1YearsAtAddress" runat="server" Width="50px" MaxLength="16"
                        TabIndex="15" OnTextChanged="textChangedEventHandler"></asp:TextBox>
                    <asp:Label ID="lblP1TABLYears" runat="server" Text="Years"></asp:Label>
                    <asp:Label ID="lblP1And" runat="server" Text="And"></asp:Label>
                    <asp:DropDownList ID="lstP1Months" runat="server" OnSelectedIndexChanged="textChangedEventHandler">
                        <asp:ListItem Text=" " />
                        <asp:ListItem>0</asp:ListItem>
                        <asp:ListItem>1</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>6</asp:ListItem>
                        <asp:ListItem>7</asp:ListItem>
                        <asp:ListItem>8</asp:ListItem>
                        <asp:ListItem>9</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>11</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="lblP1TABLMonths" runat="server" Text="Months"></asp:Label>
                   
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtP1YearsAtAddress"
                        EnableClientScript="False" ErrorMessage="Numbers Only" ValidationExpression="[0-9]*$"></asp:RegularExpressionValidator>
                    <asp:Label ID="lblP1TABLEx" runat="server" Text="Enter 0 for years if not applicable"></asp:Label></td>
            </tr>
                        <tr>
                <td align="right">
                    <asp:Label ID="lblP1RentOwn" runat="server" Text="Do you rent or own?" Font-Bold="False"></asp:Label>
                </td>
                <td align="left" colspan="2">
                    <asp:DropDownList ID="lstP1RentOwn" runat="server" TabIndex="22" OnSelectedIndexChanged="textChangedEventHandler">
                        <asp:ListItem Text=" " />
                        <asp:ListItem>Rent</asp:ListItem>
                        <asp:ListItem>Own</asp:ListItem>
 
                    </asp:DropDownList>

                </td>
            </tr>
            <tr>
                <td align="right" valign="top">
                    <asp:Label ID="lblP1Phone" runat="server" Text="Home Phone" Font-Bold="False"></asp:Label></td>
                <td align="left" colspan="2">
                    <asp:TextBox ID="txtP1Phone" runat="server" MaxLength="20" Width="140px" TabIndex="16" OnTextChanged="textChangedEventHandler"></asp:TextBox><span
                        style="color: red">*</span>
                        
                    <asp:Label ID="lblP1PhoneEx" runat="server" Text="ex. 949-555-1212 "></asp:Label><asp:RegularExpressionValidator ID="RegularExpressionP1Phone" runat="server"
                            ControlToValidate="txtP1Phone" ErrorMessage="Invalid Phone Number" Font-Bold="False"
                            ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}" EnableClientScript="False"></asp:RegularExpressionValidator>

            </tr>
            <tr>
                <td align="right" valign="top">
                    <asp:Label ID="lblP1Mobile" runat="server" Text="Mobile Phone" Font-Bold="False"></asp:Label></td>
                <td align="left" colspan="2">
                    <b>
                        <asp:TextBox ID="txtP1MobilePhone" runat="server" MaxLength="20" Width="140px" TabIndex="17" OnTextChanged="textChangedEventHandler"></asp:TextBox>
                        
                        <!--<asp:RegularExpressionValidator ID="RegularExpressionValidatorMobilePhone1" runat="server"
                            ControlToValidate="txtP1MobilePhone" ErrorMessage="Invalid Phone Number" Font-Bold="False"
                            ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}" EnableClientScript="False"></asp:RegularExpressionValidator>--></b></td>
            </tr>
            <tr>
                <td align="right" valign="top">
                    <asp:Label ID="lblP1DriversNo" runat="server" Text="Driver's License #" Font-Bold="False"></asp:Label></td>
                <td align="left" colspan="2">
                    <asp:TextBox ID="txtP1DriversLicense" runat="server" MaxLength="16" Width="140px"
                        TabIndex="18" OnTextChanged="textChangedEventHandler"></asp:TextBox><asp:Label ID="lblP1DLStar" runat="server" Font-Bold="False"></asp:Label>
                    <asp:LinkButton ID="lnkP1DLNum" runat="server" CssClass="Link" OnClick="lnkP1DLNum_Click">Edit</asp:LinkButton>
                    <asp:RequiredFieldValidator ID="ValidateP1License" runat="server" ErrorMessage="Drivers License Number"
                        ControlToValidate="txtP1DriversLicense" EnableClientScript="False"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td align="right" valign="top">
                    <asp:Label ID="lblP1DriversExp" runat="server" Text="Drivers License State" Font-Bold="False"></asp:Label>
                </td>
                <td align="left" valign="top" colspan="2">
                    <asp:DropDownList ID="lstP1DriverState" runat="server" TabIndex="19" OnSelectedIndexChanged="textChangedEventHandler">
                    </asp:DropDownList><asp:Label ID="lblP1DLStateStar" runat="server" Font-Bold="False"></asp:Label>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="lstP1DriverState"
                        ErrorMessage="Drivers License State" EnableClientScript="False"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td align="right" valign="top">
                    <asp:Label ID="Label2" runat="server" Font-Bold="False" Text="Drivers License Expiration Date"></asp:Label></td>
                <td align="left" colspan="2">
                    <asp:TextBox ID="txtP1ExpDate" runat="server" MaxLength="30" Width="140px" TabIndex="20" OnTextChanged="textChangedEventHandler"></asp:TextBox><asp:Label ID="lblP1DLExpStar" runat="server"></asp:Label>
                    <asp:Label ID="lblDriversDateEx" runat="server" Text="ex. mm/dd/yyyy (Enter 01/01/1901 if not applicable)"></asp:Label>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorP1DrDate" runat="server"
                        ControlToValidate="txtP1ExpDate" ErrorMessage="Invalid Date Format" ValidationExpression="^(\d{1,2})(\/|-)(\d{1,2})(\/|-)(\d{2,4})$"
                        EnableClientScript="False"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="ValidateP1DriversExpDate" runat="server" ErrorMessage="Expiration Date"
                        ControlToValidate="txtP1ExpDate" EnableClientScript="False"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td align="right" valign="top">
                    <asp:Label ID="lblP1DOB" runat="server" Text="Date of Birth" Font-Bold="False"></asp:Label></td>
                <td align="left" colspan="2">
                    <asp:TextBox ID="txtP1DOB" runat="server" MaxLength="30" Width="140px" TabIndex="21" OnTextChanged="textChangedEventHandler"></asp:TextBox><span
                        style="color: red">*</span>
                    <asp:Label ID="lblP1DOBEx" runat="server" Text="ex.mm/dd/yyyy "></asp:Label>
                    <asp:RequiredFieldValidator ID="ValidateP1DOB" runat="server" ErrorMessage="Date of Birth"
                        ControlToValidate="txtP1DOB" EnableClientScript="False"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorP1DOB" runat="server"
                        ControlToValidate="txtP1DOB" ErrorMessage="Invalid Date Format" ValidationExpression="^(\d{1,2})(\/|-)(\d{1,2})(\/|-)(\d{2,4})$"
                        EnableClientScript="False"></asp:RegularExpressionValidator>
                </td>
            </tr>

            <tr>
                <td align="right" valign="top">
                    <asp:Label ID="lblP1SSNHeader" runat="server" Text="Social Security #" Font-Bold="False"></asp:Label></td>
                <td align="left" colspan="2">
                    <asp:TextBox ID="txtP1SSN" runat="server" MaxLength="20" Width="140px" TabIndex="23" OnTextChanged="textChangedEventHandler"></asp:TextBox><asp:Label ID="lblP1SSNStar" runat="server" ><span
                        style="color: red; font-size:20px">*</span></asp:Label>
                        <asp:LinkButton ID="lnkP1SSN" CssClass="Link" runat="server" OnClick="lnkP1SSN_Click">Edit</asp:LinkButton>
                    <asp:Image ID="imgP1SSN" runat="server" ImageUrl="/Application/Images/help.gif" />
                    <asp:RequiredFieldValidator ID="ValidateP1SSN" runat="server" ErrorMessage="Social Security Number"
                        ControlToValidate="txtP1SSN" EnableClientScript="False"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="REP1SSN" runat="server" ControlToValidate="txtP1SSN"
                        ErrorMessage="Numbers Only" ValidationExpression="[0-9]*$" EnableClientScript="False"></asp:RegularExpressionValidator></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblSecondPrincipal" runat="server" Text="Do you have a second Principal?"
                        Font-Bold="False"></asp:Label>
                </td>
                <td align="left" colspan="2">
                    <asp:RadioButton ID="btnYes" runat="server" Font-Size="8pt" GroupName="SecondPrincipal"
                        Text="Yes" AutoPostBack="True" OnCheckedChanged="rdb_CheckedChanged" TabIndex="24" />
                    <asp:RadioButton ID="btnNo" runat="server" Font-Size="8pt" GroupName="SecondPrincipal"
                        Text="No" AutoPostBack="True" OnCheckedChanged="rdb_CheckedChanged" TabIndex="26" />
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
                <td align="center" colspan="3">
                    <asp:Panel ID="pnlPrincipal2" runat="server" Height="100%" Visible="False" Width="100%">
                        <table border="0" width="100%">
                            <tr>
                                <td colspan="3" align="center" class="DivHeader">
                                    <b><span class="MenuHeader">PRINCIPAL #2</span></b>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 40%">
                                    <asp:Label ID="lblP2First" runat="server" Text="First Name" Font-Bold="False"></asp:Label></td>
                                <td align="left" colspan="2">
                                    <asp:TextBox ID="txtP2FirstName" runat="server" MaxLength="32" Width="140px" TabIndex="25" OnTextChanged="textChangedEventHandler"></asp:TextBox><span
                                        style="color: red">*</span>
                                    <asp:RequiredFieldValidator ID="ValidateP2FirstName" runat="server" ErrorMessage="First Name"
                                        ControlToValidate="txtP2FirstName" EnableClientScript="False"></asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblP2Middle" runat="server" Text="Middle Name" Font-Bold="False"></asp:Label></td>
                                <td align="left" colspan="2">
                                    <asp:TextBox ID="txtP2MiddleName" runat="server" MaxLength="32" Width="140px" TabIndex="26" OnTextChanged="textChangedEventHandler"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblP2Last" runat="server" Text="Last Name" Font-Bold="False"></asp:Label></td>
                                <td align="left" colspan="2">
                                    <asp:TextBox ID="txtP2LastName" runat="server" MaxLength="32" Width="140px" TabIndex="27" OnTextChanged="textChangedEventHandler"></asp:TextBox><span
                                        style="color: red">*</span>
                                    <asp:RequiredFieldValidator ID="ValidateP2LastName" runat="server" ErrorMessage="Last Name"
                                        ControlToValidate="txtP2LastName" EnableClientScript="False"></asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblP2Email" runat="server" Font-Bold="False" Text="Email Address"></asp:Label></td>
                                <td align="left" colspan="2">
                                    <asp:TextBox ID="txtP2Email" runat="server" MaxLength="32" TabIndex="28" Width="140px" OnTextChanged="textChangedEventHandler"></asp:TextBox>
                                    <span style="color: red">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtP2Email" runat="server" ControlToValidate="txtP2Email"
                        ErrorMessage="Principal 2 Email" Font-Bold="False" EnableClientScript="False"></asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtP2Email"
                                        ErrorMessage="Invalid Email Address" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        EnableClientScript="False"></asp:RegularExpressionValidator></td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblP2JobTitle" runat="server" Text="Job Title" Font-Bold="False"></asp:Label></td>
                                <td align="left" colspan="2">
                                    <asp:TextBox ID="txtP2JobTitle" runat="server" MaxLength="32" Width="140px" TabIndex="29" OnTextChanged="textChangedEventHandler"></asp:TextBox><span
                                        style="color: red">*</span>
                                    <asp:RequiredFieldValidator ID="ValidateP2JobTitle" runat="server" ErrorMessage="Job Title"
                                        ControlToValidate="txtP2JobTitle" EnableClientScript="False"></asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblP2Ownership" runat="server" Text="Ownership Percentage" Font-Bold="False"></asp:Label>
                                </td>
                                <td align="left" colspan="2">
                                    <asp:TextBox ID="txtP2OwnershipPCT" runat="server" Width="50px" MaxLength="8" TabIndex="30" OnTextChanged="textChangedEventHandler"></asp:TextBox>
                                    <asp:Label ID="lblP2PCT" runat="server" Text="%"></asp:Label><span style="color: red">*</span>
                                    <asp:Image ID="imgP2Ownership" runat="server" ImageUrl="/Application/Images/help.gif" />
                                    <asp:RequiredFieldValidator ID="ValidateP2Ownership" runat="server" ErrorMessage="Ownership Percentage"
                                        ControlToValidate="txtP2OwnershipPCT" Font-Bold="False" EnableClientScript="False"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                            ID="REP2PCT" runat="server" ControlToValidate="txtP2OwnershipPCT" ErrorMessage="Numbers Only"
                                            ValidationExpression="[0-9]*$" Font-Bold="False" EnableClientScript="False"></asp:RegularExpressionValidator>
                                            <asp:RangeValidator id="P2OwnershipValidate"
           ControlToValidate="txtP2OwnershipPCT"
           MinimumValue="0"
           MaximumValue="100"
           Type="Integer"
           Font-Bold="False"
           EnableClientScript="false"
            ErrorMessage="The Ownership Percentage must be from 0 to 100!"
           runat="server"/>
                                            </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblP2Address" runat="server" Text="Home Address" Font-Bold="False"></asp:Label></td>
                                <td align="left" colspan="2">
                                    <asp:RadioButton ID="btnP2SameAsBusiness" runat="server" Font-Size="8pt" GroupName="P2Address"
                                        Text="Same as Business" AutoPostBack="True" OnCheckedChanged="btnP1SameAsBusiness_CheckedChanged"
                                        TabIndex="31" />
                                    <asp:RadioButton ID="btnP2SameAsBilling" runat="server" Font-Size="8pt" GroupName="P2Address"
                                        Text="Same As Billing" AutoPostBack="True" OnCheckedChanged="btnP1SameAsBusiness_CheckedChanged"
                                        TabIndex="33" />
                                    <asp:RadioButton ID="btnP2SameasPrincipal" runat="server" Font-Size="8pt" GroupName="P2Address"
                                        Text="Same as Principal 1" AutoPostBack="True" OnCheckedChanged="btnP1SameAsBusiness_CheckedChanged"
                                        TabIndex="34" />
                                    <asp:RadioButton ID="btnP2Other" runat="server" Font-Size="8pt" GroupName="P2Address"
                                        Text="Other" AutoPostBack="True" OnCheckedChanged="btnP1SameAsBusiness_CheckedChanged"
                                        TabIndex="35" /></td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblP2Street" runat="server" Text="Address" Font-Bold="False"></asp:Label></td>
                                <td align="left" colspan="2">
                                    <asp:TextBox ID="txtP2Address" runat="server" MaxLength="30" Width="140px" TabIndex="32"></asp:TextBox><span
                                        style="color: red">*</span>
                                    <asp:RequiredFieldValidator ID="ValidateP2Address" runat="server" ErrorMessage="Address"
                                        Font-Bold="False" ControlToValidate="txtP2Address" EnableClientScript="False"></asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td align="right">
                                </td>
                                <td align="left" colspan="2">
                                    <asp:TextBox ID="txtP2Address2" runat="server" MaxLength="30" Width="140px" TabIndex="33"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblP2City" runat="server" Text="City" Font-Bold="False"></asp:Label></td>
                                <td align="left" colspan="2">
                                    <asp:TextBox ID="txtP2City" runat="server" MaxLength="20" Width="140px" TabIndex="34" OnTextChanged="textChangedEventHandler"></asp:TextBox><span
                                        style="color: red">*</span>
                                    <asp:RequiredFieldValidator ID="ValidateP2City" runat="server" ErrorMessage="City"
                                        ControlToValidate="txtP2City" EnableClientScript="False"></asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblP2State" runat="server" Text="State" Font-Bold="False"></asp:Label></td>
                                <td align="left">
                                    <asp:DropDownList ID="lstP2State" runat="server" TabIndex="35" OnSelectedIndexChanged="textChangedEventHandler">
                                    </asp:DropDownList><asp:Label ID="lblP2StateStar" runat="server"><span style="color: red; font-size:20px">*</span></asp:Label>
                                    <asp:RequiredFieldValidator ID="ValidateP2State" runat="server" ErrorMessage="State"
                                        ControlToValidate="lstP2State" EnableClientScript="False"></asp:RequiredFieldValidator></td>
                                <td align="left">
                                    <asp:Label ID="lblP2Region" runat="server" Text="Region" Font-Bold="False"></asp:Label>&nbsp;
                                    <asp:TextBox ID="txtP2Region" runat="server" MaxLength="40" Width="100px" TabIndex="36" OnTextChanged="textChangedEventHandler"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblP2Zip" runat="server" Text="Zip Code" Font-Bold="False"></asp:Label></td>
                                <td align="left" colspan="2">
                                    <asp:TextBox ID="txtP2ZipCode" runat="server" MaxLength="12" Width="140px" TabIndex="37" OnTextChanged="textChangedEventHandler"></asp:TextBox><span
                                        style="color: red">*</span>
                                    <asp:RequiredFieldValidator ID="ValidateP2ZipCode" runat="server" ErrorMessage="Zip Code"
                                        ControlToValidate="txtP2ZipCode" EnableClientScript="False"></asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblP2Country" runat="server" Text="Country" Font-Bold="False"></asp:Label></td>
                                <td align="left" colspan="2">
                                    <asp:DropDownList ID="lstP2Country" runat="server" TabIndex="38" AutoPostBack="true" OnSelectedIndexChanged="lstP2Country_SelectedIndexChanged">
                                    </asp:DropDownList><span style="color: red">*</span>
                                    <asp:RequiredFieldValidator ID="ValidateP2Country" runat="server" ErrorMessage="Country"
                                        ControlToValidate="lstP2Country" EnableClientScript="False"></asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td align="right" valign="top">
                                    <asp:Label ID="lblP2YAA" runat="server" Text="Time at this address?" Font-Bold="False"></asp:Label></td>
                                <td align="left" colspan="2">
                                    <asp:TextBox ID="txtP2YearsAtAddress" runat="server" Width="50px" MaxLength="16"
                                        TabIndex="39" OnTextChanged="textChangedEventHandler"></asp:TextBox>
                                    <asp:Label ID="lblP2TABLYears" runat="server" Text="Years"></asp:Label><asp:Label
                                        ID="lblP2And" runat="server" Text="And"></asp:Label><asp:DropDownList ID="lstP2Months"
                                            runat="server" OnSelectedIndexChanged="textChangedEventHandler">
                                            <asp:ListItem Text=" " />
                                            <asp:ListItem>0</asp:ListItem>
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                            <asp:ListItem>8</asp:ListItem>
                                            <asp:ListItem>9</asp:ListItem>
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>11</asp:ListItem>
                                        </asp:DropDownList><asp:Label ID="lblP2Months" runat="server" Text="Months"></asp:Label><asp:RequiredFieldValidator
                                            ID="ValidateP2YearsatAddress" runat="server" ErrorMessage="Years at Address"
                                            ControlToValidate="txtP2YearsAtAddress" EnableClientScript="False"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="ValidateREP2TABL" runat="server" ControlToValidate="txtP2YearsAtAddress"
                                        EnableClientScript="False" ErrorMessage="Numbers Only" ValidationExpression="[0-9]*$"></asp:RegularExpressionValidator>
                                    <asp:Label ID="Label1" runat="server" Text="Enter 0 for years if not applicable"></asp:Label></td>
                            </tr>
                                                        <tr>
                                <td align="right">
                                    <asp:Label ID="lblP2RentOwn" runat="server" Text="Do you rent or own?" Font-Bold="False"></asp:Label></td>
                                <td align="left" colspan="2">
                                    <asp:DropDownList ID="lstP2RentOwn" runat="server" TabIndex="46" OnSelectedIndexChanged="textChangedEventHandler">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem>Rent</asp:ListItem>
                                        <asp:ListItem>Own</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="ValidateP2RentOwn" runat="server" ErrorMessage="Rent or Own"
                                        Font-Bold="False" ControlToValidate="lstP2RentOwn" Enabled="False" EnableClientScript="False"></asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td align="right" valign="top">
                                    <asp:Label ID="lblP2Phone" runat="server" Text="Home Phone" Font-Bold="False"></asp:Label></td>
                                <td align="left" colspan="2">
                                    <asp:TextBox ID="txtP2Phone" runat="server" MaxLength="50" Width="140px" TabIndex="40" OnTextChanged="textChangedEventHandler"></asp:TextBox><span
                                        style="color: red">*</span>
                                    <asp:Label ID="lblP2PhoneEx" runat="server" Text="ex. 949-555-1212 "></asp:Label>
                                    <asp:RequiredFieldValidator ID="ValidateP2HomePhone" runat="server" ErrorMessage="Home Phone Number"
                                        ControlToValidate="txtP2Phone" EnableClientScript="False"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorPhone2" runat="server"
                                        ControlToValidate="txtP2Phone" ErrorMessage="Invalid Phone Number" ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}"
                                        EnableClientScript="False"></asp:RegularExpressionValidator></td>
                            </tr>
                            <tr>
                                <td align="right" valign="top">
                                    <asp:Label ID="lblP2Mobile" runat="server" Text="Mobile Phone" Font-Bold="False"></asp:Label></td>
                                <td align="left" colspan="2">
                                    <asp:TextBox ID="txtP2MobilePhone" runat="server" MaxLength="20" Width="140px" TabIndex="41" OnTextChanged="textChangedEventHandler"></asp:TextBox><!--<asp:RegularExpressionValidator
                                        ID="RegularExpressionValidatorMobilePhone2" runat="server" ControlToValidate="txtP2MobilePhone"
                                        ErrorMessage="Invalid Phone Number" ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}"
                                        EnableClientScript="False"></asp:RegularExpressionValidator>--></td>
                            </tr>
                            <tr>
                                <td align="right" valign="top">
                                    <asp:Label ID="lblP2DriversNum" runat="server" Text="Driver's License #" Font-Bold="False"></asp:Label></td>
                                <td align="left" colspan="2">
                                    <asp:TextBox ID="txtP2DriversLicense" runat="server" MaxLength="16" Width="140px"
                                        TabIndex="42" OnTextChanged="textChangedEventHandler"></asp:TextBox><asp:Label ID="lblP2DLStar" runat="server"></asp:Label>
                                    <asp:LinkButton ID="lnkP2DLNum" runat="server" CssClass="Link" OnClick="lnkP2DLNum_Click">Edit</asp:LinkButton>
                                    <asp:RequiredFieldValidator ID="ValidateP2DriversLicense" runat="server" ErrorMessage="Drivers License Number"
                                        ControlToValidate="txtP2DriversLicense" EnableClientScript="False"></asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td align="right" valign="top">
                                    <asp:Label ID="lblP2DriversExp" runat="server" Text="Drivers License State" Font-Bold="False"></asp:Label>
                                </td>
                                <td align="left" colspan="2">
                                    <asp:DropDownList ID="lstP2DriverState" runat="server" TabIndex="43" OnSelectedIndexChanged="textChangedEventHandler">
                                    </asp:DropDownList><asp:Label ID="lblP2DLStateStar" runat="server"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="lstP2DriverState"
                                        ErrorMessage="Drivers License State" EnableClientScript="False"></asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td align="right" valign="top">
                                    <asp:Label ID="lblP2DriversLicenseExp" runat="server" Font-Bold="False" Text="Drivers License Expiration Date"></asp:Label></td>
                                <td align="left" colspan="2">
                                    <asp:TextBox ID="txtP2ExpDate" runat="server" MaxLength="30" Width="140px" TabIndex="44" OnTextChanged="textChangedEventHandler"></asp:TextBox><asp:Label ID="lblP2DLExpStar" runat="server">
                    </asp:Label>
                                    <asp:Label ID="lblDriversExpEX" runat="server" Text="ex. mm/dd/yyyy (Enter 01/01/1901 if not applicable)"></asp:Label>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorP2DRDate" runat="server"
                                        ControlToValidate="txtP2ExpDate" ErrorMessage="Invalid Date Format" ValidationExpression="^(\d{1,2})(\/|-)(\d{1,2})(\/|-)(\d{2,4})$"
                                        EnableClientScript="False"></asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator ID="ValidateP2DriversExpDate" runat="server" ErrorMessage="Expiration Date"
                                        ControlToValidate="txtP2ExpDate" EnableClientScript="False"></asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td align="right" valign="top">
                                    <asp:Label ID="lblP2DOB" runat="server" Text="Date of Birth" Font-Bold="False"></asp:Label></td>
                                <td align="left" colspan="2">
                                    <asp:TextBox ID="txtP2DOB" runat="server" MaxLength="30" Width="140px" TabIndex="45" OnTextChanged="textChangedEventHandler"></asp:TextBox><span
                                        style="color: red">*</span>
                                    <asp:Label ID="lblP2DOBEx" runat="server" Text="ex.  mm/dd/yyyy "></asp:Label>
                                    <asp:RequiredFieldValidator ID="ValidateP2DOB" runat="server" ErrorMessage="Date of Birth"
                                        ControlToValidate="txtP2DOB" EnableClientScript="False"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorP2DOB" runat="server"
                                        ControlToValidate="txtP2DOB" ErrorMessage="Invalid Date Format" ValidationExpression="^(\d{1,2})(\/|-)(\d{1,2})(\/|-)(\d{2,4})$"
                                        EnableClientScript="False"></asp:RegularExpressionValidator></td>
                            </tr>

                            <tr>
                                <td align="right" valign="top">
                                    <asp:Label ID="lblP2SSNHeader" runat="server" Text="Social Security #" Font-Bold="False"></asp:Label></td>
                                <td align="left" colspan="2">
                                    <asp:TextBox ID="txtP2SSN" runat="server" MaxLength="20" Width="140px" TabIndex="47" OnTextChanged="textChangedEventHandler"></asp:TextBox><asp:Label ID="lblP2SSNStar" runat="server"><span
                                        style="color: red; font-size:20px">*</span></asp:Label>
                                        <asp:LinkButton ID="lnkP2SSN" runat="server" CssClass="Link" OnClick="lnkP2SSN_Click">Edit</asp:LinkButton>
                                    <asp:Image ID="imgP2SSN" runat="server" ImageUrl="/Application/Images/help.gif" />
                                    <asp:RequiredFieldValidator ID="ValidateP2SSN" runat="server" ErrorMessage="Social Security Number"
                                        ControlToValidate="txtP2SSN" EnableClientScript="False"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="REP2SSN" runat="server" ControlToValidate="txtP2SSN"
                                        ErrorMessage="Numbers Only" ValidationExpression="[0-9]*$" EnableClientScript="False"></asp:RegularExpressionValidator></td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr height="20px">
            </tr>            
            <tr>
                <td align="center" colspan="3" height="25px">
                    <asp:Label ID="lblBottom" runat="server" Text='Click "Submit" to save your application and continue to the next page.'></asp:Label><br/>
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" TabIndex="48" /></td>
            </tr>
            <!--<tr>
                <td align="right" colspan="3" height="25px">
                    <asp:Label ID="lblSave" runat="server" Text='Click "Save" to save your partially completed information.  You may finish completing this page another time'></asp:Label>
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" TabIndex="49" /></td>
            </tr>-->
            <tr>
                <td colspan="3" align="center">
                    <asp:Panel ID="pnlSSN" runat="server" Style="display: none; z-index: 1;" Width="250px" CssClass="DivHelp">
                        Your social security number in the form 111223333 (9 digits).</asp:Panel>
                    <asp:Panel ID="pnlOwnership" runat="server" Style="display: none; z-index: 1;" Width="250px" CssClass="DivHelp">
                        The percentage of the company that you own.</asp:Panel>
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
