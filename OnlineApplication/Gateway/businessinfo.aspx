<%@ Page Language="C#" MasterPageFile="/Application/Main.master" AutoEventWireup="true"
    CodeFile="businessinfo.aspx.cs" Inherits="Gateway_businessinfo" Title="Commerce Technologies - Online Application"
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
                        <td class="DivHeader2" width="20%">
                            <span class="MenuHeader">Business Information</span>
                        </td>
                        <td class="DivHeader" width="20%">
                            <a href="principalinfo.aspx" class="MenuCss">Principal Information</a>
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
                        <img style="border: 0;" name="gcontact" alt="" src="/Application/Images/oa_Contact_mouseover.gif" /></a>
                    <img name="gbusiness" style="border: 0" alt="" src="/Application/Images/oa_Business.gif" />
                    <a href="principalinfo.aspx" onmouseover="document.gprincipal.src='/Application/Images/oa_Principal_mouseout.gif'"
                        onmouseout="document.gprincipal.src='/Application/Images/oa_Principal_mouseover.gif'">
                        <img name="gprincipal" style="border: 0" alt="" src="/Application/Images/oa_Principal_mouseover.gif" /></a>
                    <a href="bankinginfo.aspx" onmouseover="document.gbanking.src='/Application/Images/oa_Banking_mouseout.gif'"
                        onmouseout="document.gbanking.src='/Application/Images/oa_Banking_mouseover.gif'">
                        <img name="gbanking" style="border: 0" alt="" src="/Application/Images/oa_Banking_mouseover.gif" /></a>
                    <a href="reprogram.aspx" onmouseover="document.gplatform.src='/Application/Images/oa_Platform_mouseout.gif'"
                        onmouseout="document.gplatform.src='/Application/Images/oa_Platform_mouseover.gif'">
                        <img name="gplatform" alt="" style="border: 0" src="/Application/Images/oa_Platform_mouseover.gif" /></a></td>
            </tr>-->
            <tr>
                <td colspan="3" align="center">
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <cc1:PopupControlExtender ID="PopupControlExtender1" runat="server" PopupControlID="pnlOwnership"
                        TargetControlID="imgOwnership" Position="Bottom" />
                    <cc1:PopupControlExtender ID="PopupControlExtender2" runat="server" PopupControlID="pnlTaxID"
                        TargetControlID="imgTaxID" Position="Bottom" />
                    <cc1:PopupControlExtender ID="PopupControlExtender3" runat="server" PopupControlID="pnlProduct"
                        TargetControlID="imgProducts" Position="Bottom" />
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
                    <span class="LabelsRed"><b>*</b> - denotes a required field</span>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="3" class="DivHeader">
                    <b><span class="MenuHeader">BILLING INFORMATION</span></b></td>
            </tr>
            <tr>
                <td width="40%">
                </td>
                <td align="left" colspan="2">
                    <asp:CheckBox ID="chkBillingAddrSame" runat="server" Text="Billing address same as Business address"
                        OnCheckedChanged="chk_CheckedChanged" AutoPostBack="True" TabIndex="1" />
                    <br />
                    <asp:Label ID="lblBusinessSame" runat="server" Text="(Monthly Billing Statements will be mailed to this address) "></asp:Label></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblStreet" runat="server" Text="Address" Font-Bold="False"></asp:Label></td>
                <td colspan="2" style="text-align: left">
                    <asp:TextBox ID="txtAddress" runat="server" Width="140px" MaxLength="30" TabIndex="2"></asp:TextBox><span
                        style="color: red">*</span><asp:RequiredFieldValidator ID="ValidateAddress" runat="server"
                            ControlToValidate="txtAddress" ErrorMessage="Address" Font-Bold="False" EnableClientScript="False"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td align="right">
                </td>
                <td colspan="2" style="text-align: left">
                    <asp:TextBox ID="txtAddress2" runat="server" Width="140px" MaxLength="30" TabIndex="3"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lclCity" runat="server" Text="City" Font-Bold="False"></asp:Label></td>
                <td colspan="2" style="text-align: left">
                    <asp:TextBox ID="txtCity" runat="server" Width="140px" MaxLength="45" TabIndex="4"></asp:TextBox><span
                        style="color: red">*</span><asp:RequiredFieldValidator ID="ValidateCity" runat="server"
                            ControlToValidate="txtCity" ErrorMessage="City" Font-Bold="False" EnableClientScript="False"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblState" runat="server" Text="State" Font-Bold="False"></asp:Label></td>
                <td style="text-align: left">
                    <asp:DropDownList ID="lstState" runat="server" TabIndex="5">
                    </asp:DropDownList><span style="color: red">*</span>
                    <asp:RequiredFieldValidator ID="ValidateState" runat="server" ControlToValidate="lstState"
                        ErrorMessage="State" Font-Bold="False" EnableClientScript="False"></asp:RequiredFieldValidator></td>
                <td style="text-align: left">
                    <asp:Label ID="lblRegion" runat="server" Text="Region" Font-Bold="False"></asp:Label>&nbsp;
                    <asp:TextBox ID="txtRegion" runat="server" Width="100px" MaxLength="40" TabIndex="6"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblZipCode" runat="server" Text="Zip Code" Font-Bold="False"></asp:Label></td>
                <td colspan="2" style="text-align: left">
                    <asp:TextBox ID="txtZipCode" runat="server" Width="100px" MaxLength="16" TabIndex="7"></asp:TextBox><span
                        style="color: red">*</span>
                    <asp:RequiredFieldValidator ID="ValidateZip" runat="server" ControlToValidate="txtZipCode"
                        ErrorMessage="Zip Code" Font-Bold="False" EnableClientScript="False"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblCountry" runat="server" Text="Country" Font-Bold="False"></asp:Label></td>
                <td colspan="2" style="text-align: left">
                    <asp:DropDownList ID="lstCountry" runat="server" TabIndex="8">
                    </asp:DropDownList><span style="color: red">*</span>
                    <asp:RequiredFieldValidator ID="ValidateCountry" runat="server" ControlToValidate="lstCountry"
                        ErrorMessage="Country" Font-Bold="False" EnableClientScript="False"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td colspan="3" align="center" class="DivHeader">
                    <b><span class="MenuHeader">BUSINESS INFORMATION</span></b></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblOwnership" runat="server" Text="Ownership/Legal Status" Font-Bold="False"></asp:Label></td>
                <td colspan="2" style="text-align: left">
                    <asp:DropDownList ID="lstOwnership" runat="server" TabIndex="9">
                    </asp:DropDownList><span style="color: red">*</span>
                    <asp:Image ID="imgOwnership" runat="server" ImageUrl="/Application/Images/help.gif" />
                    <asp:RequiredFieldValidator ID="ValidateOwnership" runat="server" ControlToValidate="lstOwnership"
                        ErrorMessage="Ownership/Legal Status" EnableClientScript="False"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblTaxID" runat="server" Text="Federal Tax ID" Font-Bold="False"></asp:Label></td>
                <td colspan="2" style="text-align: left">
                    <asp:TextBox ID="txtFederalID" runat="server" Width="100px" MaxLength="20" TabIndex="10"></asp:TextBox><span
                        style="color: red">*</span>
                    <asp:LinkButton ID="lnkFederalTaxID" CssClass="Link" runat="server" OnClick="lnkFederalTaxID_Click">Edit</asp:LinkButton>
                    <asp:Image ID="imgTaxID" runat="server" ImageUrl="/Application/Images/help.gif" />
                    <asp:RequiredFieldValidator ID="ValidateTaxID" runat="server" ControlToValidate="txtFederalID"
                        ErrorMessage="Federal Tax ID" EnableClientScript="False"></asp:RequiredFieldValidator>
                    <!--<asp:RegularExpressionValidator ID="RETaxID" runat="server" ControlToValidate="txtFederalID"
                        ErrorMessage="Numbers Only" ValidationExpression="[0-9]*$"></asp:RegularExpressionValidator></td>-->
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblTIBL" runat="server" Text="Time in Business" Font-Bold="False"></asp:Label></td>
                <td colspan="2" style="text-align: left">
                    <asp:TextBox ID="txtYIB" runat="server" Width="50px" MaxLength="4" TabIndex="11"></asp:TextBox><span
                        style="color: red">*</span>
                    <asp:Label ID="lblYears" runat="server" Text="Years" Font-Bold="False"></asp:Label>&nbsp;
                    <asp:RequiredFieldValidator ID="ValidateYears" runat="server" ControlToValidate="txtYIB"
                        ErrorMessage="Years" EnableClientScript="False"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="REYIB" runat="server" ControlToValidate="txtYIB"
                        ErrorMessage="Numbers Only" ValidationExpression="[0-9]*$" EnableClientScript="False"></asp:RegularExpressionValidator>
                    <asp:Label ID="lblAnd" runat="server" Text="And" Font-Bold="False"></asp:Label>&nbsp;
                    <asp:TextBox ID="txtMIB" runat="server" Width="50px" MaxLength="4" TabIndex="12"></asp:TextBox>
                    <asp:Label ID="lblMonths" runat="server" Text="Months" Font-Bold="False"></asp:Label></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblProductsSold" runat="server" Text="Products/Services Sold (Be Descriptive. 250 max Characters)"
                        Font-Bold="False"></asp:Label></td>
                <td colspan="2" style="text-align: left">
                    <asp:TextBox ID="txtProductsSold" runat="server" Height="50px" TextMode="MultiLine"
                        Width="250px" MaxLength="250" TabIndex="13"></asp:TextBox><span style="color: red">*</span>
                    <asp:Image ID="imgProducts" runat="server" ImageUrl="/Application/Images/help.gif" />
                    <asp:RequiredFieldValidator ID="ValidateProductsSold" runat="server" ControlToValidate="txtProductsSold"
                        ErrorMessage="Products Sold" EnableClientScript="False"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr height="20px">                
            </tr>
            <tr>
                <td align="right" colspan="3" height="25px">
                    <asp:Label ID="lblBottom" runat="server" Text='Click "Submit" to save your application and continue to the next page.'></asp:Label>
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" TabIndex="14" /></td>
            </tr>
            <!--<tr>
                <td align="right" colspan="3" height="25px">
                    <asp:Label ID="lblSave" runat="server" Text='Click "Save" to save your partially completed information.  You may finish completing this page another time'></asp:Label>
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" TabIndex="27" /></td>
            </tr>-->  
            <tr>
                <td colspan="3" align="center">
                    <asp:Panel ID="pnlOwnership" runat="server" Style="display: none; z-index: 1;" Width="250px" CssClass="DivHelp">
                        Ownership/Legal Status:Select how you have registered your business i.e., legal/medical, sole proprietorship, partnership, corporation, LLC, etc. Select Others if none of the options in the list apply to you.</asp:Panel>
                    <asp:Panel ID="pnlTaxID" runat="server" Style="display: none; z-index: 1;" Width="250px" CssClass="DivHelp">
                        Federal Tax ID: 9-digit number. Example: 123456789. This is also known as a tax payer ID. In case of sole proprietorship, this will be your Social Security Number. (No Dashes)</asp:Panel>
                    <asp:Panel ID="pnlProduct" runat="server" Style="display: none; z-index: 1;" Width="250px" CssClass="DivHelp">
                        Products/Services sold: A brief description of the products/services sold by your company. Please limit your description to 250 characters.</asp:Panel>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
