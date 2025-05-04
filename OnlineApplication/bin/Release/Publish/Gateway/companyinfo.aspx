<%@ Page Language="C#" MasterPageFile="/Application/Main.master" AutoEventWireup="true"
    CodeFile="companyinfo.aspx.cs" Inherits="Gateway_companyinfo" Title="Commerce Technologies - Online Application"
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
                        <td class="DivHeader2" width="20%">
                            <span class="MenuHeader">Contact Information</span>
                        </td>
                        <td class="DivHeader" width="20%">
                            <a href="businessinfo.aspx" class="MenuCss">Business Information</a>
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
                    <img name="gcontact" style="border: 0" alt="" src="/Application/Images/oa_Contact.gif" />
                    <a href="businessinfo.aspx" onmouseover="document.gbusiness.src='/Application/Images/oa_Business_mouseout.gif'"
                        onmouseout="document.gbusiness.src='/Application/Images/oa_Business_mouseover.gif'">
                        <img name="gbusiness" style="border: 0" alt="" src="/Application/Images/oa_Business_mouseover.gif" /></a>
                    <a href="principalinfo.aspx" onmouseover="document.gprincipal.src='/Application/Images/oa_Principal_mouseout.gif'"
                        onmouseout="document.gprincipal.src='/Application/Images/oa_Principal_mouseover.gif'">
                        <img name="gprincipal" style="border: 0" alt="" src="/Application/Images/oa_Principal_mouseover.gif" /></a>
                    <a href="bankinginfo.aspx" onmouseover="document.gbanking.src='/Application/Images/oa_Banking_mouseout.gif'"
                        onmouseout="document.gbanking.src='/Application/Images/oa_Banking_mouseover.gif'">
                        <img name="gbanking" style="border: 0" alt="" src="/Application/Images/oa_Banking_mouseover.gif" /></a>
                    <a href="reprogram.aspx" onmouseover="document.gplatform.src='/Application/Images/oa_Platform_mouseout.gif'"
                        onmouseout="document.gplatform.src='/Application/Images/oa_Platform_mouseover.gif'">
                        <img name="gplatform" alt="" style="border: 0" src="/Application/Images/oa_Platform_mouseover.gif" /></a>
                </td>
            </tr>-->
            <tr>
                <td align="center" colspan="3">
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <cc1:PopupControlExtender ID="PopupControlExtender1" runat="server" PopupControlID="pnlCompany"
                        TargetControlID="imgCompany" Position="Bottom" />
                    <cc1:PopupControlExtender ID="PopupControlExtender2" runat="server" PopupControlID="pnlDBA"
                        TargetControlID="imgDba" Position="Bottom" />
                    <cc1:PopupControlExtender ID="PopupControlExtender3" runat="server" PopupControlID="pnlAddress"
                        TargetControlID="imgAddress" Position="Bottom" />
                    <cc1:PopupControlExtender ID="PopupControlExtender4" runat="server" PopupControlID="pnlCustServ"
                        TargetControlID="imgCustServPhone" Position="Bottom" />
                </td>
            </tr>
            <tr>
                <td colspan="3" align="center">
                    <asp:Label ID="lblHeader" runat="server" Text="Please provide the following contact information about your company."></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="3" align="center">
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
                <td align="right" width="40%">
                    <asp:Label ID="lblCompanyName" runat="server" Text="Company (Legal Name)" Font-Bold="False"></asp:Label>
                </td>
                <td colspan="2" align="left">
                    <asp:TextBox ID="txtCompanyName" runat="server" Width="140px" MaxLength="30" TabIndex="1"></asp:TextBox><span
                        style="color: #ff0000">*</span>
                    <asp:Image ID="imgCompany" runat="server" ImageUrl="/Application/Images/help.gif" />
                    <asp:RequiredFieldValidator ID="ValidateCompany" runat="server" ErrorMessage="Company Name"
                        ControlToValidate="txtCompanyName" EnableClientScript="False"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblDBA" runat="server" Text="DBA (Doing Business As)" Font-Bold="False"></asp:Label>
                </td>
                <td colspan="2" align="left">
                    <asp:TextBox ID="txtDBA" runat="server" Width="140px" MaxLength="50" TabIndex="2"></asp:TextBox><span
                        style="color: #ff0000">*</span>
                    <asp:Image ID="imgDba" runat="server" ImageUrl="/Application/Images/help.gif" />
                    <asp:RequiredFieldValidator ID="ValidateDBA" runat="server" ErrorMessage="DBA" ControlToValidate="txtDBA"
                        EnableClientScript="False"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblAddress" runat="server" Text="Physical Business Address" Font-Bold="False"></asp:Label>
                </td>
                <td colspan="2" align="left">
                    <asp:TextBox ID="txtAddress" runat="server" Width="140px" MaxLength="30" TabIndex="3"></asp:TextBox><span
                        style="color: #ff0000">*</span>
                    <asp:Image ID="imgAddress" runat="server" ImageUrl="/Application/Images/help.gif" />
                    <asp:RequiredFieldValidator ID="ValidateAddress" runat="server" ErrorMessage="Address"
                        ControlToValidate="txtAddress" EnableClientScript="False"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td align="right">
                </td>
                <td colspan="2" align="left">
                    <asp:TextBox ID="txtAddress2" runat="server" Width="140px" MaxLength="30" TabIndex="4"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblCity" runat="server" Text="City" Font-Bold="False"></asp:Label>
                </td>
                <td colspan="2" align="left">
                    <asp:TextBox ID="txtCity" runat="server" Width="140px" MaxLength="20" TabIndex="5"></asp:TextBox><span
                        style="color: #ff0000">*</span>
                    <asp:RequiredFieldValidator ID="ValidateCity" runat="server" ErrorMessage="City"
                        ControlToValidate="txtCity" EnableClientScript="False"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblState" runat="server" Text="State" Font-Bold="False"></asp:Label>
                </td>
                <td align="left">
                    <asp:DropDownList ID="lstState" runat="server" TabIndex="6">
                    </asp:DropDownList><span style="color: #ff0000">*</span>
                    <asp:RequiredFieldValidator ID="ValidateState" runat="server" ErrorMessage="State"
                        ControlToValidate="lstState" EnableClientScript="False"></asp:RequiredFieldValidator></td>
                <td align="left">
                    <asp:Label ID="lblRegion" runat="server" Text="Region" Font-Bold="False"></asp:Label>&nbsp;
                    <asp:TextBox ID="txtRegion" runat="server" Width="100px" MaxLength="50" TabIndex="7"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblZipCode" runat="server" Text="Zip Code" Font-Bold="False"></asp:Label>
                </td>
                <td colspan="2" align="left">
                    <asp:TextBox ID="txtZipCode" runat="server" Width="80px" MaxLength="16" TabIndex="8"></asp:TextBox><span
                        style="color: #ff0000">*</span>
                    <asp:RequiredFieldValidator ID="ValidateZipCode" runat="server" ControlToValidate="txtZipCode"
                        ErrorMessage="ZipCode" Font-Bold="False" EnableClientScript="False"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblCountry" runat="server" Text="Country" Font-Bold="False"></asp:Label>
                </td>
                <td colspan="2" align="left">
                    <asp:DropDownList ID="lstCountry" runat="server" TabIndex="9">
                    </asp:DropDownList><span style="color: #ff0000">*</span>
                    <asp:RequiredFieldValidator ID="ValidateCountry" runat="server" ControlToValidate="lstCountry"
                        ErrorMessage="Country" Font-Bold="False" EnableClientScript="False"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblTBL" runat="server" Text="Time at Business Location" Font-Bold="False"></asp:Label>
                </td>
                <td colspan="2" align="left">
                    <asp:TextBox ID="txtTBLYears" runat="server" Width="40px" MaxLength="3" TabIndex="10"></asp:TextBox><span
                        style="color: #ff0000">*</span>
                    <asp:Label ID="lblTBLYears" runat="server" Text="Years And"></asp:Label>
                    <asp:RequiredFieldValidator ID="ValidateTIBL" runat="server" ControlToValidate="txtTBLYears"
                        ErrorMessage="Years" Font-Bold="False" EnableClientScript="False"></asp:RequiredFieldValidator>
                    <asp:TextBox ID="txtTBLMonths" runat="server" MaxLength="3" TabIndex="10" Width="40px"></asp:TextBox>
                    <span style="color: #ff0000">*</span><asp:Label ID="lblTBLMonths" runat="server"
                        Text="Months"></asp:Label>
                    <asp:RequiredFieldValidator ID="ValidatorTBLMonths" runat="server" ControlToValidate="txtTBLMonths"
                        ErrorMessage="Months" EnableClientScript="False"></asp:RequiredFieldValidator>
                    <asp:Label ID="lblTBLEx" runat="server" Text="(Enter 0 if Not Applicable)"></asp:Label>
                    <asp:RegularExpressionValidator ID="REYears" runat="server" ControlToValidate="txtTBLYears"
                        EnableClientScript="False" ErrorMessage="Numbers Only" ValidationExpression="[0-9]*$"></asp:RegularExpressionValidator><asp:RegularExpressionValidator
                            ID="REMonths" runat="server" ControlToValidate="txtTBLMonths" EnableClientScript="False"
                            ErrorMessage="Numbers Only" ValidationExpression="[0-9]*$"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblBusinessHours" runat="server" Text="Business Hours" Font-Bold="False"></asp:Label>
                </td>
                <td colspan="2" align="left">
                    <asp:TextBox ID="txtBusinessHours" runat="server" Width="80px" MaxLength="32" TabIndex="11"></asp:TextBox><span
                        style="color: #ff0000">*</span>
                    <asp:Label ID="lblBHoursEx" runat="server" Text="ex. 8am to 5pm"></asp:Label>
                    <asp:RequiredFieldValidator ID="ValidateHours" runat="server" ControlToValidate="txtBusinessHours"
                        ErrorMessage="Business Hours" Font-Bold="False" EnableClientScript="False"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblPhone" runat="server" Text="Business Phone" Font-Bold="False"></asp:Label>
                </td>
                <td colspan="2" align="left">
                    <asp:TextBox ID="txtPhone" runat="server" Width="120px" MaxLength="50" TabIndex="12"></asp:TextBox><span
                        style="color: #ff0000">*</span>&nbsp;
                    <asp:Label ID="lblPhoneEx" runat="server" Text="ex. 949-555-1212"></asp:Label>
                    <asp:Label ID="lblExt" runat="server" Text="Ext" Font-Bold="False"></asp:Label>
                    <asp:TextBox ID="txtPhoneExt" runat="server" Width="30px" MaxLength="8" TabIndex="13"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ValidatePhone" runat="server" ControlToValidate="txtPhone"
                        ErrorMessage="Phone" Font-Bold="False" EnableClientScript="False"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="REExt" runat="server" ControlToValidate="txtPhoneExt"
                        ErrorMessage="Numbers Only" ValidationExpression="[0-9]*$" EnableClientScript="False"></asp:RegularExpressionValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorBusinessPhone" runat="server"
                        ControlToValidate="txtPhone" ErrorMessage="Invalid Phone(ex. 123-123-1234)" ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}"
                        EnableClientScript="False"></asp:RegularExpressionValidator></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblFax" runat="server" Text="Fax" Font-Bold="False"></asp:Label>
                </td>
                <td colspan="2" align="left">
                    <asp:TextBox ID="txtFax" runat="server" Width="120px" MaxLength="50" TabIndex="14"></asp:TextBox>
                    <asp:Label ID="lblFaxEx" runat="server" Text="ex. 949-555-1212"></asp:Label>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorFax" runat="server"
                        ControlToValidate="txtFax" ErrorMessage="Invalid Fax Number(ex. 123-123-1234)"
                        ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}" EnableClientScript="False"></asp:RegularExpressionValidator></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblCustPhone" runat="server" Text="Customer Service Phone" Font-Bold="False"></asp:Label>
                </td>
                <td colspan="2" align="left">
                    <asp:TextBox ID="txtCustServPhone" runat="server" Width="120px" MaxLength="50" TabIndex="15"></asp:TextBox><span
                        style="color: #ff0000">*</span>&nbsp;
                    <asp:Image ID="imgCustServPhone" runat="server" ImageUrl="/Application/Images/help.gif" />
                    <asp:Label ID="Label2" runat="server" Text="ex. 949-555-1212"></asp:Label>
                    <asp:RequiredFieldValidator ID="ValidateCS" runat="server" ControlToValidate="txtCustServPhone"
                        ErrorMessage="Customer Service Phone" Font-Bold="False" EnableClientScript="False"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorCustServPhone" runat="server" ControlToValidate="txtCustServPhone"
                        ErrorMessage="Invalid Phone(ex. 123-123-1234)" ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}"
                        EnableClientScript="False"></asp:RegularExpressionValidator></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblWebsite" runat="server" Text="Website Address" Font-Bold="False"></asp:Label>
                    <asp:Label ID="lblhttp" runat="server" Text="http://" Font-Bold="False"></asp:Label>
                </td>
                <td colspan="2" align="left">
                    <asp:TextBox ID="txtWebsite" runat="server" Width="140px" MaxLength="100" TabIndex="16"></asp:TextBox>
                    <asp:Label ID="lblWebsiteEx" runat="server" Text="ex. www.mysite.com "></asp:Label></td>
            </tr>
            <tr height="20px">                
            </tr>
            <tr>
                <td align="right" colspan="3" height="25px">
                    <asp:Label ID="lblBottom" runat="server" Text='Click "Submit" to save your application and continue to the next page.'></asp:Label>
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" TabIndex="19" /></td>
            </tr>
            <!--<tr>
                <td align="right" colspan="3" height="25px">
                    <asp:Label ID="lblSave" runat="server" Text='Click "Save" to save your partially completed information.  You may finish completing this page another time'></asp:Label>
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" TabIndex="20" /></td>
            </tr>-->
            <tr>
                <td colspan="3" align="center">
                    <asp:Panel ID="pnlCompany" runat="server" Style="display: none; z-index: 1;" Width="250px" CssClass="DivHelp">
                        Company (Legal Name): This is the registered business name. It can be the same as the Doing Business As (DBA) name or different. Legal Name is the individual's Legal Name for Sole proprietorships.</asp:Panel>
                    <asp:Panel ID="pnlDBA" runat="server" Style="display: none; z-index: 1;" Width="250px" CssClass="DivHelp">
                        Doing Busniness As (DBA): This is the name of your company as it appears on the customer's billing statement. It may be different than your Company (Legal) Name.</asp:Panel>
                    <asp:Panel ID="pnlAddress" runat="server" Style="display: none; z-index: 1;" Width="250px" CssClass="DivHelp">
                        Physical Address: The address used when registering the business. May be the home address.</asp:Panel>
                    <asp:Panel ID="pnlCustServ" runat="server" Style="display: none; z-index: 1;" Width="250px">
                        Customer Service Phone: This is the phone number that will show up on the customer's credit card statments.</asp:Panel>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
