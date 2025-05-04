<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="apply.aspx.cs" Inherits="apply"  Title="Credit Card Processing, Merchant Services, Payment Processing"
    Theme="Admin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
<br />
    <asp:Panel ID="pnlMainPage" runat="server" Width="100%">
    <table width="98%" cellspacing="0" cellpadding="2" border="0" align="right">
        <tr>
            <td align="left" colspan="2">
                <span class="LabelsBody">&nbsp;&nbsp;&nbsp;&nbsp;&quot;I have reviewed dozens of e-commerce firms
				to understand their offerings and services,
				and to gauge their willingness and ability to
				fully service their clients.&quot;
                <br/>
                    <br />
                &quot;I found Commerce Technologies to be a cut above
				and far ahead of all competitors. e-Commerce
				is the only merchant service I recommend to my clients.&quot;
				</span>
                <br/>
                <div align="right">
                    <span class="LabelsBody">&nbsp;&nbsp;&nbsp;&nbsp;~ Bruce Clay, <i>Internet Consultant</i>&nbsp;&nbsp;&nbsp;&nbsp;</span>
                </div>
            </td>
        </tr>
        <tr height="20px"></tr>
        <tr>
            <td colspan="2" align="center">
                <span class="LabelsHeader"><b>Studies have proven
                    that accepting credit cards enhances your company credibility. Customers are
                    also likely to spend more and make spontaneous purchases. Request a free consultation
                    today. There is absolutely no obligation.</b> </span>
            </td>
        </tr>
        <tr height="20px"></tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Label ID="lblError" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="Small" ForeColor="Red" Visible="False"></asp:Label>
                <table border="0" cellpadding="0" cellspacing="5" style="width:400px" class="DivGreen">
                    <tr>
                        <td align="center" colspan="2" valign="middle">
                            <span class="LabelsBody"><b>Request a</b></span><br />
                            <span class="LabelsHeader"><b>FREE APPLICATION</b></span><br />
                            <span class="LabelsBody"><b>Join today! No Obligation!</b></span>
                        </td>
                    </tr>
                    <tr>   
                        <td  colspan="2" align = "center"><a class="Link" href="UnacceptableCountriesBusinesses.aspx" target="_self"><b>Do you Qualify?</b></a></td> 
                    <tr />
                    <tr>
                        <td align="right" valign="middle" style="width:40%">
                                        <span class="LabelsBody">First Name</span></td>
                        <td align="left" valign="middle">
                                        <asp:TextBox ID="txtFirstName" runat="server" MaxLength="80" Width="140px" TabIndex="1"></asp:TextBox><span
                                            style="color: #ff0000">*</span><asp:RequiredFieldValidator ID="ValidateFirstName" runat="server" ControlToValidate="txtFirstName"
                                            Display="Static" EnableClientScript="False" ErrorMessage="First Name"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td align="right" valign="middle">
                                        <span class="LabelsBody">Last Name</span></td>
                        <td align="left" valign="middle">
                                        <asp:TextBox ID="txtLastName" runat="server" MaxLength="80" Width="140px" TabIndex="2"></asp:TextBox><span style="color: #ff0000">*</span><asp:RequiredFieldValidator ID="ValidateLastName" runat="server" ControlToValidate="txtLastName"
                                            EnableClientScript="False" ErrorMessage="Last Name"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td align="right" valign="middle">
                                        <span class="LabelsBody">Email</span></td>
                        <td align="left" valign="middle">
                                        <asp:TextBox ID="txtApplyEmail" runat="server" MaxLength="80" Width="140px" TabIndex="3"></asp:TextBox><span style="color: #ff0000">*</span><asp:RequiredFieldValidator
                                                ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtApplyEmail" EnableClientScript="False"
                                                ErrorMessage="Email"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                            ID="ValidateEmail" runat="server" ControlToValidate="txtApplyEmail" EnableClientScript="False"
                                            ErrorMessage="Invalid Email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2" valign="middle">
                            <hr size="1" width="90%" noshade />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="middle">
                            <span class="LabelsBody">Country Code</span></td>
                        <td align="left" valign="middle">
                                        <asp:TextBox ID="txtCountryCodeHome" runat="server" MaxLength="3" Width="35px" TabIndex="4" Text="1"></asp:TextBox>
                            <asp:RangeValidator ID="RangeValidator1"
                                                runat="server" ControlToValidate="txtCountryCodeHome" Type="Integer" EnableClientScript="False" ErrorMessage="Country Code (ex. 1 for U.S.)"
                                                MinimumValue="1" MaximumValue="999" CultureInvariantValues="True" Font-Names="Arial" Font-Size="0.65em"></asp:RangeValidator></td>
                    </tr>
                    <tr>
                        <td align="right" valign="middle">
                                        <span class="LabelsBody">Home Phone</span></td>
                        <td align="left" valign="middle">
                                        <span
                                            style="color: #ff0000"></span>
                                        <asp:TextBox ID="txtHomePhone" runat="server" MaxLength="80" Width="140px" TabIndex="5"></asp:TextBox><span
                                            style="color: #ff0000"><asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                                runat="server" ControlToValidate="txtHomePhone" EnableClientScript="False" ErrorMessage="Phone Number (ex. 1231231234)"
                                                ValidationExpression="\d+"></asp:RegularExpressionValidator></span></td>
                    </tr>
                    <tr>
                        <td align="right" valign="middle">
                                        <span class="LabelsBody">Mobile Phone</span></td>
                        <td align="left" valign="middle">
                                        <asp:TextBox ID="txtMobilePhone" runat="server" MaxLength="80" Width="140px" TabIndex="6"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtMobilePhone"
                                            EnableClientScript="False" ErrorMessage="Phone Number (ex. 1231231234)" ValidationExpression="\d+"></asp:RegularExpressionValidator></td>
                    </tr>
                    <tr>
                        <td align="right" valign="middle">
                                        <span class="LabelsBody">Work Phone</span></td>
                        <td align="left" valign="middle">
                                        <asp:TextBox ID="txtWorkPhone" runat="server" MaxLength="80" Width="140px" TabIndex="7"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtWorkPhone"
                                            EnableClientScript="False" ErrorMessage="Phone Number (ex. 1231231234)" ValidationExpression="\d+"></asp:RegularExpressionValidator></td>
                                            <!--"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}" --Validation for phone number with format xxx-xxx-xxxx -->
                    </tr>
                    <tr>
                        <td align="center" colspan="2" valign="middle">
                            <hr noshade="noshade" size="1" width="90%" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="middle">
                                        <span class="LabelsBody">Company</span></td>
                        <td align="left" valign="middle">
                                        <asp:TextBox ID="txtCompany" runat="server" MaxLength="80" Width="140px" TabIndex="8"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right" valign="middle">
                                        <span class="LabelsBody">Address</span></td>
                        <td align="left" valign="middle">
                                        <asp:TextBox ID="txtAddress" runat="server" MaxLength="80" Width="140px" TabIndex="9"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right" valign="middle">
                                        <span class="LabelsBody">City</span></td>
                        <td align="left" valign="middle">
                                        <asp:TextBox ID="txtCity" runat="server" MaxLength="80" Width="140px" TabIndex="10"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right" valign="middle">
                                        <span class="LabelsBody">State</span></td>
                        <td align="left" valign="middle">
                                        <asp:DropDownList ID="lstState" runat="server" TabIndex="11">
                                        </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td align="right" valign="middle">
                                        <span class="LabelsBody">Region</span></td>
                        <td align="left" valign="middle">
                                        <asp:TextBox ID="txtRegion" runat="server" MaxLength="80" Width="140px" TabIndex="12"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right" valign="middle">
                                        <span class="LabelsBody">Zip</span></td>
                        <td align="left" valign="middle">
                                        <asp:TextBox ID="txtZipCode" runat="server" MaxLength="80" TabIndex="13"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right" valign="middle">
                                        <span class="LabelsBody">Country</span></td>
                        <td align="left" valign="middle">
                                        <asp:DropDownList ID="lstCountry" runat="server" TabIndex="14">
                                        </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2" valign="middle">
                            <hr noshade="noshade" size="1" width="90%" />
                            </td>
                    </tr>
                    <tr>
                        <td align="right" valign="middle">
                                        <span class="LabelsBody">Website</span></td>
                        <td align="left" valign="middle">
                                        <asp:TextBox ID="txtURL" runat="server" MaxLength="80" Width="140px" TabIndex="15"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right" valign="middle">
                                        <span class="LabelsBody">Do you need a Shopping Cart?</span></td>
                        <td align="left" valign="middle">
                                        <asp:RadioButton ID="rdbYes" runat="server" GroupName="Cart" Text="Yes" TabIndex="16" />&nbsp;<asp:RadioButton
                                            ID="rdbNo" runat="server" Checked="True" GroupName="Cart" Text="No" TabIndex="17" /></td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2" valign="middle">
                                        <span class="LabelsBody">Comments</span></td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2" valign="middle">
                                        <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" Width="140px" TabIndex="18"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2" valign="middle">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" TabIndex="19" /></td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2" valign="middle">
                            <span class="LabelsRed">
                                <strong>*</strong> denotes a required field</span></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
            <b><span class="LabelsHeader">Within 24 hours you will be contacted by an E-Commerce Consultant.
                <br />
            (DURING OUR BUSINESS HOURS)</span></b>
            </td>
        </tr>
        <tr height="20px"></tr>
    </table>
    </asp:Panel>
</asp:Content>

