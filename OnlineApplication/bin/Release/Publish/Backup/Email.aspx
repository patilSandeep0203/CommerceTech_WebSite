<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Email.aspx.cs" Inherits="Email" Title="Credit Card Processing, Merchant Services, Payment Processing"
    Theme="Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
<br />
<script type="text/javascript" src='https://www.google.com/recaptcha/api.js'></script>
    <asp:Panel ID="pnlMainPage" runat="server" Width="100%">
    <table align="center" width="95%" cellspacing="0" cellpadding="5" border="0">        
        <tr>
            <td>
                <span class="LabelsBody">Complete this email form if you have a questions about or for Commerce Technologies. Please include best contact methods (phone and/or email) and best times to reach you. Mark the checkbox to send a copy of the 
                email to yourself.</span>
            </td>
        </tr>
        <tr height="20px"></tr>
        <tr>
            <td align="center">
                <asp:Label ID="lblError" runat="server" Font-Bold="True" Font-Names="Arial"
                Font-Size="Small" ForeColor="Red" Visible="False"></asp:Label>
                     <table border="0" cellpadding="0" cellspacing="5" style="width:500px" class="DivGreen">
                     <tr>
                        <td align="center" colspan="3">
                            <asp:Label ID="lblEmailHeader" runat="server" CssClass="LabelsHeader"></asp:Label>
                        </td>
                     </tr>
                     <tr height="10px"></tr>
                     <tr>
                        <td align="right" valign="middle" width="20%">
                            <span class="LabelsBody">Full Name</span></td>
                        <td align="left" valign="middle" colspan="2">
                            <asp:TextBox ID="txtFullName" runat="server" MaxLength="80" TabIndex="1" Width="150px"></asp:TextBox><span
                                style="color: #ff0000">*</span><asp:RequiredFieldValidator ID="ValidateFullName" runat="server" ControlToValidate="txtFullName"
                                Display="Static" EnableClientScript="False" ErrorMessage="Name"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td align="right" valign="middle">
                            <span class="LabelsBody">From Email</span></td>
                        <td align="left" valign="middle" width="60%">
                            <asp:TextBox ID="txtEmailForm" runat="server" MaxLength="80" TabIndex="2" Width="150px"></asp:TextBox><span 
                            style="color: #ff0000">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEmailForm" EnableClientScript="False"
                                ErrorMessage="Email Address"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                ID="ValidateEmail" runat="server" ControlToValidate="txtEmailForm" EnableClientScript="False"
                                ErrorMessage="Invalid Email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></td>
                        <td align="left" valign="middle" width="20%">
                            <asp:CheckBox ID="chkCC" CssClass="LabelsBody" runat="server" Text="Copy myself" TabIndex="3" Checked="true" /></td>
                    </tr>
                    <tr>
                        <td align="right" valign="middle">
                            <span class="LabelsBody">Subject</span></td>
                        <td align="left" valign="middle" colspan="2">
                            <asp:TextBox ID="txtSubject" runat="server" MaxLength="80" TabIndex="4" Width="150px"></asp:TextBox><span
                                style="color: #ff0000">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSubject"
                                Display="Static" EnableClientScript="False" ErrorMessage="Subject"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td align="right" valign="top">
                            <span class="LabelsBody">Email Message</span></td>
                        <td align="left" valign="top" colspan="2">
                            <asp:TextBox ID="txtBody" runat="server" TextMode="MultiLine" Wrap="true" MaxLength="250" TabIndex="5"></asp:TextBox><span
                                style="color: #ff0000">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtBody"
                                Display="Static" EnableClientScript="False" ErrorMessage="Email Message"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2" valign="middle">
                    <div class="g-recaptcha" data-sitekey="6LdWm_8SAAAAAK8Om3BDYVFw7zmYCTNB4RAvJCvL"></div>
                    </td>
                    </tr>

                    <tr>
                        <td align="center" colspan="3" valign="middle">
                            <asp:Button ID="btnSubmit" runat="server" Text="Send Email" OnClick="btnSubmit_Click" TabIndex="6" /></td>
                    </tr>
                    <tr>
                        <td align="center" colspan="3" valign="middle">
                            <span class="LabelsRed">
                                <strong>*</strong> denotes a required field</span></td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <br /><span class="LabelsHeader"><b>You will be contacted by one of our representatives within 24 hours</b><br />
                            (DURING OUR BUSINESS HOURS).</span>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center">
                <br /><a class="Link" href="javascript:window.history.back();" onmouseout='status=" ";'
                    onmouseover='status=" Back to previous page "; return true;'><span style="text-decoration: underline">
                    </span>
                    <img alt="" border="0" height="25" name="back" src="images/back-on.gif" width="75" tabindex="8" /></a></td>
        </tr>
    </table>
    </asp:Panel>
</asp:Content>
