<%@ Page Language="C#" MasterPageFile="/Application/Main.master" AutoEventWireup="true"
    CodeFile="principalinfo.aspx.cs" Inherits="Gateway_principalinfo" Title="Commerce Technologies - Online Application"
    Theme="TextBoxSkin" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentHolder" runat="Server">
    <asp:Panel ID="pnlMainPage" runat="server">
        <table border="0" cellpadding="2" cellspacing="0" Width="100%" class="DivGreen">
            <tr>
                <td colspan="3" align="center" class="DivHeader2" height="26px">
                    <b><span class="MenuHeader">Gateway Signup Information</span></b>
                </td>
            </tr> 
            <tr>
            <td colspan="3" align="center" class="DivHeader">
                <table border="0" cellpadding="0" cellspacing="0" Width="100%">
                    <tr>
                        <td class="DivHeader" width="20%">
                            <a href="companyinfo.aspx" class="MenuCss">Contact Information</a>
                        </td>
                        <td class="DivHeader" width="20%">
                            <a href="businessinfo.aspx" class="MenuCss">Business Information</a>
                        </td>
                        <td class="DivHeader2" width="20%">
                            <span class="MenuHeader">Principal Information</span>
                        </td>
                        <td class="DivHeader" width="20%">
                            <a href="bankinginfo.aspx" class="MenuCss">Banking Information</a>
                        </td>
                        <td class="DivHeader" width="20%">
                            <a href="reprogram.aspx" class="MenuCss">Platform Information</a>
                        </td>
                    </tr>
                </table>
            </td>
            </tr>
            <!--<tr>
                <td colspan="3" align="center">
                    <a href="companyinfo.aspx" onmouseover="document.gcontact.src='/Application/Images/oa_Contact_mouseout.gif'"
                        onmouseout="document.gcontact.src='/Application/Images/oa_Contact_mouseover.gif'">
                        <img name="gcontact" style="border: 0" alt="" src="/Application/Images/oa_Contact_mouseover.gif" /></a>
                    <a href="businessinfo.aspx" onmouseover="document.gbusiness.src='/Application/Images/oa_Business_mouseout.gif'"
                        onmouseout="document.gbusiness.src='/Application/Images/oa_Business_mouseover.gif'">
                        <img name="gbusiness" style="border: 0" alt="" src="/Application/Images/oa_Business_mouseover.gif" /></a>
                    <img name="gprincipal" style="border: 0" alt="" src="/Application/Images/oa_Principal.gif" />
                    <a href="bankinginfo.aspx" onmouseover="document.gbanking.src='/Application/Images/oa_Banking_mouseout.gif'"
                        onmouseout="document.gbanking.src='/Application/Images/oa_Banking_mouseover.gif'">
                        <img name="gbanking" style="border: 0" alt="" src="/Application/Images/oa_Banking_mouseover.gif" /></a>
                    <a href="reprogram.aspx" onmouseover="document.gplatform.src='/Application/Images/oa_Platform_mouseout.gif'"
                        onmouseout="document.gplatform.src='/Application/Images/oa_Platform_mouseover.gif'">
                        <img name="gplatform" alt="" style="border: 0" src="/Application/Images/oa_Platform_mouseover.gif" /></a>
                </td>
            </tr>-->
            <tr>
                <td colspan="3" align="center">
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <cc1:PopupControlExtender ID="PopupControlExtender1" runat="server" PopupControlID="pnlSSN"
                        TargetControlID="imgP1SSN" Position="Bottom" />
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
                        Visible="False"></asp:Label><br />
                    <asp:ValidationSummary ID="ValidateSummary" runat="server" BackColor="#FFC0C0" BorderColor="red"
                        BorderWidth="1px" ForeColor="Black" HeaderText="Please check the fields marked in red."
                        Width="250px" />
                </td>
            </tr>
            <tr>
                <td align="center" colspan="3" style="text-align: center">
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
                    <asp:Label ID="lblP1First" runat="server" Text="First Name" Font-Bold="False"></asp:Label>
                </td>
                <td align="left" colspan="2">
                    <asp:TextBox ID="txtP1FirstName" runat="server" MaxLength="32" Width="140px" TabIndex="1"></asp:TextBox><span
                        style="color: red">*</span>
                    <asp:RequiredFieldValidator ID="ValidateP1FirstName" runat="server" ErrorMessage="First Name"
                        ControlToValidate="txtP1FirstName" EnableClientScript="False"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblP1Middle" runat="server" Text="Middle Name" Font-Bold="False"></asp:Label></td>
                <td align="left" colspan="3">
                    <asp:TextBox ID="txtP1MiddleName" runat="server" MaxLength="32" Width="140px" TabIndex="2"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblP1Last" runat="server" Text="Last Name" Font-Bold="False"></asp:Label>
                </td>
                <td align="left" colspan="2">
                    <asp:TextBox ID="txtP1LastName" runat="server" MaxLength="32" Width="140px" TabIndex="3"></asp:TextBox><span
                        style="color: red">*</span>
                    <asp:RequiredFieldValidator ID="ValidateP1LastName" runat="server" ErrorMessage="Last Name"
                        ControlToValidate="txtP1LastName" EnableClientScript="False"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblP1Email" runat="server" Font-Bold="False" Text="Email Address"></asp:Label></td>
                <td align="left" colspan="2">
                    <asp:TextBox ID="txtP1Email" runat="server" MaxLength="60" TabIndex="4" Width="140px"></asp:TextBox><span
                        style="color: red">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtP1Email"
                        ErrorMessage="Email" Font-Bold="False" EnableClientScript="False"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtP1Email"
                        ErrorMessage="Invalid Email Address" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                        EnableClientScript="False"></asp:RegularExpressionValidator></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblP1Title" runat="server" Text="Job Title" Font-Bold="False"></asp:Label>
                </td>
                <td align="left" colspan="2">
                    <asp:TextBox ID="txtP1JobTitle" runat="server" MaxLength="32" Width="140px" TabIndex="5"></asp:TextBox><span
                        style="color: red">*</span>
                    <asp:RequiredFieldValidator ID="ValidateP1JobTitle" runat="server" ErrorMessage="Job Title"
                        ControlToValidate="txtP1JobTitle" EnableClientScript="False"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblP1Address" runat="server" Text="Home Address" Font-Bold="False"></asp:Label>
                </td>
                <td align="left" colspan="2">
                    <asp:RadioButton ID="btnP1SameAsBusiness" runat="server" Font-Size="8pt" GroupName="P1Address"
                        Text="Same as Business" AutoPostBack="True" OnCheckedChanged="btnP1SameAsBusiness_CheckedChanged"
                        TabIndex="6" />&nbsp;<asp:RadioButton ID="btnP1SameAsBilling" runat="server" Font-Size="8pt"
                            GroupName="P1Address" Text="Same as Billing" AutoPostBack="True" OnCheckedChanged="btnP1SameAsBusiness_CheckedChanged" />&nbsp;
                    <asp:RadioButton ID="btnP1Other" runat="server" Font-Size="8pt" GroupName="P1Address"
                        Text="Other" AutoPostBack="True" OnCheckedChanged="btnP1SameAsBusiness_CheckedChanged" /></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblP1Street" runat="server" Text="Address" Font-Bold="False"></asp:Label></td>
                <td align="left" colspan="2">
                    <asp:TextBox ID="txtP1Address" runat="server" MaxLength="30" Width="140px" TabIndex="7"></asp:TextBox><span
                        style="color: red">*</span>
                    <asp:RequiredFieldValidator ID="ValidateP1Address" runat="server" ErrorMessage="Address"
                        Font-Bold="False" ControlToValidate="txtP1Address" EnableClientScript="False"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td align="right">
                </td>
                <td align="left" colspan="2">
                    <asp:TextBox ID="txtP1Address2" runat="server" MaxLength="30" Width="140px" TabIndex="8"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblP1City" runat="server" Text="City" Font-Bold="False"></asp:Label>
                </td>
                <td align="left" colspan="2">
                    <asp:TextBox ID="txtP1City" runat="server" MaxLength="20" Width="140px" TabIndex="9"></asp:TextBox><span
                        style="color: red">*</span>
                    <asp:RequiredFieldValidator ID="ValidateP1City" runat="server" ErrorMessage="City"
                        Font-Bold="False" ControlToValidate="txtP1City" EnableClientScript="False"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblP1State" runat="server" Text="State" Font-Bold="False"></asp:Label></td>
                <td align="left" colspan="1">
                    <asp:DropDownList ID="lstP1State" runat="server" TabIndex="10">
                    </asp:DropDownList><span style="color: red">*</span>
                    <asp:RequiredFieldValidator ID="ValidateP1State" runat="server" ErrorMessage="State"
                        ControlToValidate="lstP1State" EnableClientScript="False"></asp:RequiredFieldValidator></td>
                <td align="left" colspan="1">
                    <asp:Label ID="lblP1Region" runat="server" Text="Region" Font-Bold="False"></asp:Label>&nbsp;
                    <asp:TextBox ID="txtP1Region" runat="server" MaxLength="40" TabIndex="11"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblP1Zip" runat="server" Text="Zip Code" Font-Bold="False"></asp:Label></td>
                <td align="left" colspan="2">
                    <asp:TextBox ID="txtP1ZipCode" runat="server" MaxLength="12" Width="140px" TabIndex="12"></asp:TextBox><span
                        style="color: red">*</span>
                    <asp:RequiredFieldValidator ID="ValidateP1ZipCode" runat="server" ErrorMessage="Zip Code"
                        Font-Bold="False" ControlToValidate="txtP1ZipCode" EnableClientScript="False"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblP1Country" runat="server" Text="Country" Font-Bold="False"></asp:Label></td>
                <td align="left" colspan="2">
                    <asp:DropDownList ID="lstP1Country" runat="server" TabIndex="13">
                    </asp:DropDownList><span style="color: red">*</span>
                    <asp:RequiredFieldValidator ID="ValidateP1Country" runat="server" ErrorMessage="Country"
                        ControlToValidate="lstP1Country" EnableClientScript="False"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblP1Phone" runat="server" Text="Home Phone" Font-Bold="False"></asp:Label></td>
                <td align="left" colspan="2">
                    <asp:TextBox ID="txtP1Phone" runat="server" MaxLength="20" Width="140px" TabIndex="14"></asp:TextBox><span
                        style="color: red">*</span>
                    <asp:Label ID="lblP1PhoneEx" runat="server" Text="ex. 949-555-1212 "></asp:Label>
                    <asp:RequiredFieldValidator ID="ValidateP1HomePhone" runat="server" ErrorMessage="Home Phone Number"
                        ControlToValidate="txtP1Phone" EnableClientScript="False"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorPhone" runat="server"
                        ControlToValidate="txtP1Phone" ErrorMessage="Invalid Phone Number" ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}"
                        EnableClientScript="False"></asp:RegularExpressionValidator></td>
            </tr>
            <tr>
                <td align="right" valign="top">
                    <asp:Label ID="lblP1Mobile" runat="server" Text="Mobile Phone" Font-Bold="False"></asp:Label></td>
                <td align="left" colspan="2">
                    <b>
                        <asp:TextBox ID="txtP1MobilePhone" runat="server" MaxLength="20" Width="140px" TabIndex="17"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorMobilePhone1" runat="server"
                            ControlToValidate="txtP1MobilePhone" ErrorMessage="Invalid Phone Number" Font-Bold="False"
                            ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}" EnableClientScript="False"></asp:RegularExpressionValidator></b></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblP1SSN" runat="server" Text="Social Security #" Font-Bold="False"></asp:Label></td>
                <td align="left" colspan="2" valign="top">
                    <asp:TextBox ID="txtP1SSN" runat="server" MaxLength="20" Width="140px" TabIndex="15"></asp:TextBox><span
                        style="color: red">*</span>
                    <asp:LinkButton ID="lnkP1SSN" CssClass="Link" runat="server" OnClick="lnkP1SSN_Click">Edit</asp:LinkButton>
                    <asp:Image ID="imgP1SSN" runat="server" ImageUrl="/Application/Images/help.gif" />
                    <asp:RequiredFieldValidator ID="ValidateP1SSN" runat="server" ErrorMessage="Social Security Number"
                        ControlToValidate="txtP1SSN" EnableClientScript="False"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="REP1SSN" runat="server" ControlToValidate="txtP1SSN"
                        ErrorMessage="Numbers Only" ValidationExpression="[0-9]*$" EnableClientScript="False"></asp:RegularExpressionValidator></td>
            </tr>
            <tr height="20px">
            </tr>            
            <tr>
                <td align="right" colspan="3" height="25px">
                    <asp:Label ID="lblBottom" runat="server" Text='Click "Submit" to save your application and continue to the next page.'></asp:Label>
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" TabIndex="16" /></td>
            </tr>
            <!--<tr>
                <td align="right" colspan="3" height="25px">
                    <asp:Label ID="lblSave" runat="server" Text='Click "Save" to save your partially completed information.  You may finish completing this page another time'></asp:Label>
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" TabIndex="17" /></td>
            </tr>-->
            <tr>
                <td colspan="3" align="center">
                    <asp:Panel ID="pnlSSN" runat="server" Style="display: none; z-index: 1;" Width="250px" CssClass="DivHelp">
                        Social Security Number: Your social security number in the form 111223333 (9 digits).</asp:Panel>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
