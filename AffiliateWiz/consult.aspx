<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="consult.aspx.cs" Inherits="consult" Title="Credit Card Processing, Merchant Services, Payment Processing"
    Theme="Admin" %>



<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
<br />
<script type="text/javascript" src='https://www.google.com/recaptcha/api.js'></script>
    <asp:Panel ID="pnlMainPage" runat="server" Width="95%">
    <table align="center" width="100%" cellspacing="0" cellpadding="5" border="0">
        <tr>
            <td align="left">
                <span class="LabelsBody">&nbsp;&nbsp;&nbsp;&nbsp;"I just wanted
                    to tell you how pleased I am with the results I am getting from using the E-Commerce
                    Exchange service to place credit card ordering on my web site. My orders have tripled
                    over the past month from what I have previously averaged. I would highly recommend
                    this service to anyone that has a commercial web site. Thanks for providing a terrific
                    service."</span>
                <br/>
                <br/>
                <div align="right">
                    <span class="LabelsBody">&nbsp;&nbsp;&nbsp;&nbsp;~ John Clark, of <i>Compumarine</i>&nbsp;&nbsp;&nbsp;&nbsp;</span>
                </div>
            </td>
            <td align="left" valign="middle" style="font-size: 12pt">
                <img src="images/man.gif" alt="testimony" border="0" width="200" height="150"/></td>
        </tr>
        <tr>
            <td colspan="2" align="left">
                <span class="LabelsHeader"><b>Studies have proven
                    that accepting credit cards enhances your company credibility.&nbsp; Customers are
                    also likely to spend more and make spontaneous purchases.&nbsp; Request a free consultation
                    today. There is absolutely no obligation.</b> </span>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Label ID="lblError" runat="server" Font-Bold="True" Font-Names="Arial"
                Font-Size="Small" ForeColor="Red" Visible="False"></asp:Label>
                <table border="0" cellpadding="0" cellspacing="5" style="width:400px" class="DivGreen">
                    <tr>
                        <td align="center" colspan="2" valign="middle">
                            <span class="LabelsBody"><b>Request a</b></span><br />
                            <span class="LabelsHeader"><b>FREE CONSULTATION</b></span><br />
                            <span class="LabelsBody"><b>Join today! No Obligation!</b></span>
                        </td>
                    </tr>
                    <tr>
                     <td colspan="2" align="center">
                     <table border="0" cellpadding="0" cellspacing="5" style="width:400px" class="DivGreen">
                    
                     <tr>   
                     <td align = "center" colspan="2">                    
                     <a href="UnacceptableCountriesBusinesses.aspx" target="_self" class="Link"><b>Do you Qualify?</b></a>                
                     </td> <tr />
                     
                     <tr>
                        <td align="right" valign="middle" style="width:200px">
                                        <span class="LabelsBody">First Name</span></td>
                        <td align="left" valign="middle">
                                        <asp:TextBox ID="txtFirstName" runat="server" MaxLength="80" TabIndex="1"></asp:TextBox><span
                                            style="color: #ff0000">*</span><asp:RequiredFieldValidator ID="ValidateFirstName" runat="server" ControlToValidate="txtFirstName"
                                            Display="Static" EnableClientScript="False" ErrorMessage="First Name"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td align="right" valign="middle" style="width: 200px">
                                        <span class="LabelsBody">Last Name</span></td>
                        <td align="left" valign="middle">
                                        <asp:TextBox ID="txtLastName" runat="server" MaxLength="80" TabIndex="2"></asp:TextBox><span style="color: #ff0000">*</span><asp:RequiredFieldValidator ID="ValidateLastName" runat="server" ControlToValidate="txtLastName"
                                            EnableClientScript="False" ErrorMessage="Last Name"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td align="right" valign="middle" style="width: 200px">
                                        <span class="LabelsBody">Email</span></td>
                        <td align="left" valign="middle">
                                        <asp:TextBox ID="txtConsultEmail" runat="server" MaxLength="80" TabIndex="3"></asp:TextBox><span style="color: #ff0000">*</span><asp:RequiredFieldValidator
                                                ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtConsultEmail" EnableClientScript="False"
                                                ErrorMessage="Email"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                            ID="ValidateEmail" runat="server" ControlToValidate="txtConsultEmail" EnableClientScript="False"
                                            ErrorMessage="Invalid Email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></td>
                    </tr>
                    <tr>
                        <td align="right" valign="middle" style="width: 200px">
                                        <span class="LabelsBody">Country Code</span></td>
                        <td align="left" valign="middle">
                                        <asp:TextBox ID="txtCountryCode" runat="server" MaxLength="3" Width="35" Text="1" TabIndex="4"></asp:TextBox><asp:RangeValidator ID="RangeValidator1"
                                                runat="server" ControlToValidate="txtCountryCode" EnableClientScript="False" ErrorMessage="Country Code (ex: 1 for U.S.)"
                                                Type ="Integer" MaximumValue="999" MinimumValue="1" Font-Names="Arial" Font-Size="0.65em"></asp:RangeValidator></td>
                    </tr>
                    <tr>
                        <td align="right" valign="middle" style="width: 200px">
                                        <span class="LabelsBody">Home Phone</span></td>
                        <td align="left" valign="middle">
                                        <asp:TextBox ID="txtHomePhone" runat="server" MaxLength="80" TabIndex="5"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                                runat="server" ControlToValidate="txtHomePhone" EnableClientScript="False" ErrorMessage="Phone Number (ex. 1231231234)"
                                                ValidationExpression="\d+"></asp:RegularExpressionValidator></td>
                    </tr>
                    <tr>
                        <td align="right"valign="middle" style="width: 200px">
                                        <span class="LabelsBody">Work Phone</span></td>
                        <td align="left"valign="middle">
                                        <asp:TextBox ID="txtWorkPhone" runat="server" MaxLength="80" TabIndex="6"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtWorkPhone"
                                            EnableClientScript="False" ErrorMessage="Phone Number (ex. 1231231234)" 
                                            ValidationExpression="\d+"></asp:RegularExpressionValidator></td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2" valign="middle">
                    <div class="g-recaptcha" data-sitekey="6LdWm_8SAAAAAK8Om3BDYVFw7zmYCTNB4RAvJCvL"></div>
                    </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2" valign="middle">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" TabIndex="7" /></td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2" valign="middle">
                            <span class="LabelsRed">
                                <strong>*</strong> denotes a required field</span></td>
                    </tr>
                </table>
            </td>
            </table>
            </td></tr>
        <tr>
            <td colspan="2" align="center">
                <a class="Link" href="javascript:window.history.back();" onmouseout='status=" ";'
                    onmouseover='status=" Back to previous page "; return true;'><span style="text-decoration: underline">
                    </span>
                    <img alt="" border="0" height="25" name="back" src="images/back-on.gif" width="75" tabindex="8" /></a></td>
        </tr>
    </table>
    </asp:Panel>
</asp:Content>

