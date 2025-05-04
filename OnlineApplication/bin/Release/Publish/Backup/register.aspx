<%@ Page Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="register.aspx.cs"
    Inherits="register" Theme="TextBoxSkin" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentHolder" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <cc1:PopupControlExtender ID="PopupControlExtender1" runat="server" PopupControlID="pnlMerchantDesc"
                TargetControlID="imgMerchantDesc" Position="Bottom" />
            <cc1:PopupControlExtender ID="PopupControlExtender2" runat="server" PopupControlID="pnlGatewayDesc"
                TargetControlID="imgGatewayDesc" Position="Bottom" />
            <cc1:PopupControlExtender ID="PopupControlExtender3" runat="server" PopupControlID="pnlEquipDesc"
                TargetControlID="imgHelpEquip" Position="Bottom" />
            <cc1:PopupControlExtender ID="PopupControlExtender4" runat="server" PopupControlID="pnlOtherReferralHelp"
                TargetControlID="imgHelpOtherReferral" Position="Bottom" />
            <asp:Panel ID="pnlLink" runat="server" Visible="False" Width="100%">
                <asp:Label ID="lblDownloadMsg" runat="server" Text="Please download, print and complete the application and fax it at 310-321-5410"></asp:Label>
                <br />
                <asp:HyperLink ID="lnkDownloadApp" runat="server" Font-Bold="True">Download Application</asp:HyperLink></asp:Panel>
            <asp:Panel ID="pnlMainPage" runat="server">
                <table border="0" cellspacing="0" cellpadding="0" width="100%">
                    <tr valign="top" align="left">
                        <td align="center" colspan="3">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                <tr height="17px">
                                    <td align="center" colspan="2" class="DivHeader2"><!-- style="height: 30px; background-image: url(Images/topMain.gif);">-->
                                        <asp:Label ID="lblRegistrationHeader" Font-Bold="true" runat="server" CssClass="MenuHeader">Registration</asp:Label></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblError" runat="server" BackColor="Red" Font-Size="Medium" ForeColor="White"
                                            Visible="False"></asp:Label>
                                    </td>
                                </tr>            
                                <tr>
                                    <td align="center" colspan="2">
                                        <asp:ValidationSummary ID="ValidateSummary" Visible="false" runat="server" ForeColor="Black" HeaderText="Please check the fields marked in red."
                                            Width="250px" />
                                        </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center" valign="top">
                                        <table style="width: 100%" border="0">
                                            <tr>
                                                <td align="center" colspan="2"><br />
                                                    <asp:Panel ID="pnlRDB" runat="server" Height="100%" Width="100%" Visible="false">
                                                        <div id="radiobuttons" style="width: 50%;" class="DivGreen">
                                                            <b><span class="LabelsBody">Please select a Signup Type</span><span class="LabelsRedLarge">*</span></b>
                                                            <br/>
                                                            <table>
                                                            <tr>
                                                                <td align=left>
                                                                    <asp:RadioButton ID="rdbMerchant" runat="server" AutoPostBack="True" GroupName="MerchantGateway"
                                                                        Text="Merchant Account" OnCheckedChanged="rdbGateway_CheckedChanged" />
                                                                    <asp:Image ID="imgMerchantDesc" runat="server" ImageUrl="/Application/Images/help.gif" />
                                                                </td>
                                                                <td align=left>                                                                
                                                                    <asp:RadioButton ID="rdbGateway" runat="server" AutoPostBack="True" GroupName="MerchantGateway"
                                                                        OnCheckedChanged="rdbGateway_CheckedChanged" Text="Payment Gateway" />
                                                                    <asp:Image ID="imgGatewayDesc" runat="server" ImageUrl="/Application/Images/help.gif"/>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align=left>
                                                                    <asp:RadioButton ID="rdbEquipment" runat="server" AutoPostBack="True" GroupName="MerchantGateway"
                                                                        OnCheckedChanged="rdbGateway_CheckedChanged" Text="Additional Services Only" />
                                                                    <asp:Image ID="imgHelpEquip" runat="server" ImageUrl="/Application/Images/help.gif" />
                                                                </td>
                                                                <td align=left>
                                                                    <asp:RadioButton ID="rdbBoth" runat="server" AutoPostBack="True" GroupName="MerchantGateway"
                                                                        OnCheckedChanged="rdbGateway_CheckedChanged" Text="Merchant And Payment Gateway" />
                                                                </td>
                                                            </tr>
                                                            </table>
                                                            <br />
                                                        </div>
                                                    </asp:Panel>
                                                    <asp:Panel Style="display: none; z-index: 1; text-align: left;" 
                                                        ID="pnlMerchantDesc" runat="server" Width="600px" CssClass="DivHelp">
                                                                <b>A merchant account</b> is required to accept credit cards, Visa &amp; MasterCard.
                                                                A merchant account is a special account with a bank that is a member of the Visa
                                                                and MasterCard associations. Such a bank has been certified by Visa and MasterCard
                                                                associations and can provide you, the merchant, with all of the services related
                                                                to your merchant account.<br /><br />
                                                                Once your merchant account is setup and "live" on the credit card system, you can
                                                                accept credit cards from customers generally as follows:<br />
                                                                <ul><ol>
                                                                    <li style="text-align: left;">
                                                                        A customer presents his credit card for payment</li>
                                                                    <li style="text-align: left;">
                                                                        Using their credit card number, you submit an electronic request to the processing
                                                                        network for "authorization to capture funds" from the cardholder's credit card account
                                                                        in the amount of the purchase. Traditionally, one would submit this request by swiping
                                                                        a credit card through an electronic transaction terminal provided by the merchant
                                                                        account provider. With Payment Gateways, this request is provided electronically
                                                                        to our payment gateway servers, which then route the request along the processing
                                                                        network.</li>
                                                                    <li style="text-align: left;">
                                                                        The processing network immediately receives your electronic request and determines
                                                                        if the cardholder's account is valid and if the funds are available. If they are,
                                                                        the processing network returns an electronic response to your terminal or computer.
                                                                        This response is called an "authorization code", and is your guaranteed authorization
                                                                        to capture the funds. Typically, this code is a six-digit number. The transaction
                                                                        and its associated authorization are stored in a "batch", where other transactions
                                                                        for that day reside.</li>
                                                                    <li style="text-align: left;">
                                                                        You print a receipt for the customer using the electronic terminal or your computer
                                                                        and the customer signs the receipt. As far as the customer is concerned, the transaction
                                                                        is complete. As far as you the merchant are concerned, there is one more step to
                                                                        complete the transaction.</li>
                                                                    <li style="text-align: left;">
                                                                        At the end of your business day (usually), you submit a final request to the processing
                                                                        network to go ahead and "capture the funds" that you obtained authorizations for
                                                                        during the course of business that day. This is called "settlement" or "settling
                                                                        your batch". This request is also submitted using the electronic terminal or your
                                                                        computer. The processing network immediately receives your response electronically
                                                                        and determines if the capture amounts contained in your request match the authorizations
                                                                        for each item. If so, the request is granted and an "Accepted" response is returned
                                                                        to your electronic terminal or computer. A settlement report can be printed showing
                                                                        the grand totals by card type (Visa, MasterCard, American Express, Discover, etc)
                                                                        for the settled batch. Note: any corrections to your batch, such as voiding a transaction,
                                                                        must be made prior to settlement.</li>
                                                                    <li style="text-align: left;">
                                                                        Within 48 to 72 hours (usually), the funds associated with the batch you settled
                                                                        are deposited electronically into your business bank account. Typically, the discount
                                                                        rate you pay to your merchant account provider are deducted from the deposit before
                                                                        it transferred to your bank account, resulting in a "net deposit" of funds.</li>
                                                                    <li style="text-align: left;">
                                                                        At the end of the month, your merchant account provider will mail a statement to
                                                                        you, detailing the credit card activity for the month and the associated fees you
                                                                        have been charged for such.</li>
                                                                </ol>
                                                        </ul>
                                                    </asp:Panel>
                                                    <asp:Panel Style="display: none; z-index: 1;" ID="pnlGatewayDesc" runat="server" Width="400px" CssClass="DivHelp">
                                                        <b>A Gateway</b> enables you to automatically accept, authorize
                                                            and process secure credit card and electronic check orders directly through your
                                                            web site.
                                                    </asp:Panel>
                                                    <asp:Panel Style="display: none; z-index: 1;" ID="pnlEquipDesc" runat="server" Width="400px" CssClass="DivHelp">
                                                        State-of-the-art technology for your physical storefront.
                                                            The perfect solution for traditional businesses looking for a faster, easier way
                                                            to authorize and manage credit, ATM, gift card and check guarantee transactions.
                                                    </asp:Panel>
                                                    <asp:Panel Style="display: none; z-index: 1;" ID="pnlOtherReferralHelp" runat="server" Width="400px" CssClass="DivHelp">
                                                        Please be specific. For example, if you found us through a
                                                            search engine, please specify the search engine website address (Example - www.google.com
                                                            or www.yahoo.com). If you found us through any other website that is not listed
                                                            in the "How did you hear about us" list, then please specify the website address
                                                            (Example - &nbsp;www.website.com)</asp:Panel>
                                                </td>
                                            </tr>
                                            <asp:Panel ID="pnlRegistrationInfo" runat="server" HorizontalAlign=Justify>
                                            <tr>
                                            <td align="right" style="width: 40%">
                                                    <span class="LabelsBodySmall"></span></td>
                                                <td align="left" >
                                                    <span class="LabelsBodySmall"><strong>Contact Information</strong></span></td>
                                                
                                            </tr>
                                            <tr>
                                                <td align="right" style="width: 40%">
                                                    <span class="LabelsBodySmall">First Name</span></td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtFirstName" runat="server" Width="140px" MaxLength="32"></asp:TextBox>
                                                    <span class="LabelsRedLarge">*</span>
                                                    <asp:RequiredFieldValidator ID="ValidateFirstName" runat="server" Display="Static"
                                                        ControlToValidate="txtFirstName" ErrorMessage="First Name" EnableClientScript="False"></asp:RequiredFieldValidator></td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="LabelsBodySmall">Last Name</span></td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtLastName" runat="server" Width="140px" MaxLength="32"></asp:TextBox>
                                                    <span class="LabelsRedLarge">*</span>
                                                    <asp:RequiredFieldValidator ID="ValidateLastName" runat="server" ControlToValidate="txtLastName"
                                                        ErrorMessage="Last Name" EnableClientScript="False"></asp:RequiredFieldValidator></td>
                                            </tr>
                                            <tr>
                                                <td align="right" valign="top">
                                                    <span class="LabelsBodySmall">Email</span></td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtEmail" runat="server" Width="140px" MaxLength="50"></asp:TextBox>
                                                    <span class="LabelsRedLarge">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                                            runat="server" ControlToValidate="txtEmail" ErrorMessage="Email" EnableClientScript="False"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="ValidateEmail" runat="server" ControlToValidate="txtEmail"
                                                        ErrorMessage="Invalid Email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                        EnableClientScript="False"></asp:RegularExpressionValidator>
                                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtEmail"
                                                        ControlToValidate="txtEmailVerify" ErrorMessage="Email Address do not Match"
                                                        EnableClientScript="False"></asp:CompareValidator></td>
                                            </tr>
                                            <tr>
                                                <td align="right" valign="top">
                                                    <span class="LabelsBodySmall">Confirm Email</span></td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtEmailVerify" runat="server" MaxLength="50" Width="140px"></asp:TextBox>
                                                    <span class="LabelsRedLarge">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                                            runat="server" ControlToValidate="txtEmailVerify" EnableClientScript="False"
                                                            ErrorMessage="Confirm Email"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtEmailVerify"
                                                        EnableClientScript="False" ErrorMessage="Invalid Email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" valign="top">
                                                    <span class="LabelsBodySmall">Title</span></td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtTitle" runat="server" MaxLength="50" Width="140px"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td align="right" valign="top">
                                                    <span class="LabelsBodySmall">Business Phone</span></td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtPhone" runat="server" Width="140px" MaxLength="50"></asp:TextBox>                                                    
                                                        <span class="LabelsBodySmall">Ext.</span>
                                                    <asp:TextBox ID="txtPhoneExt" runat="server" Width="40px" MaxLength="4"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorPhone" runat="server"
                                                        ControlToValidate="txtPhone" ErrorMessage="Phone Number (ex. 123-123-1234)" ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}"
                                                        EnableClientScript="False"></asp:RegularExpressionValidator>
                                                    <asp:RegularExpressionValidator ID="REExt" runat="server" ControlToValidate="txtPhoneExt"
                                                        ErrorMessage="Numbers Only" ValidationExpression="[0-9]*$" EnableClientScript="False"></asp:RegularExpressionValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" valign="top">
                                                    <asp:Label ID="lblHomePhone" runat="server" Text="Home Phone" Visible="False"></asp:Label></td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtHomePhone" runat="server" MaxLength="50" Width="140px" Visible="False"></asp:TextBox>
</td>
                                            </tr>
                                            <tr>
                                                <td align="right" valign="top">
                                                    <span class="LabelsBodySmall">Mobile Phone</span></td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtMobilePhone" runat="server" Width="140px" MaxLength="50"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorMobile" runat="server"
                                                        ControlToValidate="txtMobilePhone" ErrorMessage="Mobile Number (ex. 123-123-1234)"
                                                        ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}" EnableClientScript="False"></asp:RegularExpressionValidator></td>
                                            </tr>
                                            <tr>
                                                <td align="right" valign="top">
                                                    <span class="LabelsBodySmall">How did you hear about us?</span>
                                                </td>
                                                <td align="left">                                                    
                                                        <asp:DropDownList ID="lstReferral" runat="server" OnSelectedIndexChanged="lstReferral_SelectedIndexChanged"
                                                            AutoPostBack="True"/>
                                                        <span class="LabelsRedLarge">*</span>
                                                        <asp:RequiredFieldValidator ID="ValidateReferral" runat="server"
                                                            ControlToValidate="lstReferral" ErrorMessage="Referral Source" EnableClientScript="False"></asp:RequiredFieldValidator></td>
                                            </tr>
                                            <tr>
                                                <td align="right" valign="top">
                                                    <span class="LabelsBodySmall">Other Referral Source</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtOtherReferral" runat="server" Width="140px" MaxLength="50"></asp:TextBox>&nbsp;
                                                    <asp:Image ID="imgHelpOtherReferral" runat="server" ImageUrl="/Application/Images/help.gif"/>
                                                    <asp:RequiredFieldValidator ID="ValidateOtherReferral" runat="server" ControlToValidate="txtOtherReferral"
                                                        ErrorMessage="Other Referral Source" EnableClientScript="False"></asp:RequiredFieldValidator></td>
                                            </tr>
                                            <tr>
                                                <td align="right" valign="top">
                                                </td>
                                                <td align="left">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" valign="top">
                                                    <span class="LabelsBodySmall">Login Name</span></td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtLoginName" runat="server" MaxLength="32" Width="140px"></asp:TextBox>
                                                    <span class="LabelsRedLarge">*</span>
                                                    <span class="LabelsBodySmall">(Minimum 5 Characters long)</span>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtLoginName"
                                                        EnableClientScript="False" ErrorMessage="Login Name"></asp:RequiredFieldValidator></td>
                                            </tr>
                                            <tr>
                                                <td align="right" valign="top">
                                                    <span class="LabelsBodySmall">Password</span></td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="140px" MaxLength="50"></asp:TextBox>
                                                    <span class="LabelsRedLarge">*</span>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorPWD" runat="server" ControlToValidate="txtPassword"
                                                        ErrorMessage="Password" EnableClientScript="False"></asp:RequiredFieldValidator><asp:CompareValidator
                                                            ID="CompareValidatorPassword" runat="server" ControlToCompare="txtPassword" ControlToValidate="txtPasswordConfirm"
                                                            ErrorMessage="Passwords do not match" EnableClientScript="False"></asp:CompareValidator></td>
                                            </tr>
                                            <tr>
                                                <td align="right" valign="top" style="">
                                                    <span class="LabelsBodySmall">Confirm Password</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtPasswordConfirm" runat="server" TextMode="Password" Width="140px"
                                                        MaxLength="50"></asp:TextBox>
                                                        <span class="LabelsRedLarge">*</span><asp:RequiredFieldValidator
                                                            ID="RequiredFieldValidatorConfirm" runat="server" ControlToValidate="txtPasswordConfirm"
                                                            ErrorMessage="Confirm Password" EnableClientScript="False"></asp:RequiredFieldValidator></td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" align="center" style="height: 30px">
                                                 <asp:Button ID="btnContinue" runat="server" Text="Continue" OnClick="btnContinue_Click" /></td>
                                            </tr><tr>
                                                <td colspan="3" align="center">
                                                <span class="LabelsBodySmall">Clicking "Continue" will save your application</span><br /><br />
                                                <span class="LabelsRedLarge"><b>*</b> - <span class="LabelsRed">denotes a required field</span></span></td></tr>
                                            </asp:Panel>
                                            
                                            <tr>
                                            <asp:Panel ID="pnlSameLoginID" runat="server" HorizontalAlign="Center"><br />
                                            <span class="LabelsBody">The Login Name you chose already exists. You have two options:<br />
                                                <table align=center width="300px">
                                                <tr><td align=center width="300px">1. Sign up with a different login name.<br />
                                                 <asp:imagebutton ID="Imagebutton1" onclick="NewLogin_Click" ImageUrl="Images/NewLogin.gif" runat=server width="80" height="20" /><br />
                                                    <br /></td></tr>
                                                    <tr><td>OR</td></tr>
                                                <tr><td align=center width="300px">2. Sign into the previous application.<br /><a href="default.aspx" >
                                                    <img name="signinBig" alt="" src="Images/SigninBig.gif" style="border: 0" width="80" height="20" /></a>
                                                </td></tr>
                                            </table></span></asp:Panel>
                                            </tr>

                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>

                    <tr>

                    </tr>

                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
