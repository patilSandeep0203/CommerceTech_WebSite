<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Edit.aspx.cs" Inherits="Edit"
    Title="Commerce Technologies - Partner Portal" Theme="AppTheme" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<script language="javascript" type="text/javascript">
//<![CDATA[
var cot_loc0=(window.location.protocol == "https:")? "https://secure.comodo.net/trustlogo/javascript/cot.js" :
"http://www.trustlogo.com/trustlogo/javascript/cot.js";
document.writeln('<scr' + 'ipt language="JavaScript" src="'+cot_loc0+'" type="text\/javascript">' + '<\/scr' + 'ipt>');
//]]>
</script>

<head id="Head1" runat="server">
    <title>Commerce Technologies - Partner Portal</title>
    <link href="~/PartnerCSS.css" type="text/css" rel="stylesheet" />
</head>

<a href="http://www.instantssl.com" id="comodoTL">SSL</a>
<script language="JavaScript" type="text/javascript">
COT("https://www.apps.commercetech.com/images/secure_site.gif", "SC2", "none");
</script>
<body>
    <center>        
        <form id="form1" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <div ID="pnlDiv" align="left"></div>
            <table style="width: 1050px" class="SilverBorder" align="center">
                <tr>
                    <td align="center" style="background-color: #565294;; height: 25px">
                        <span class="MenuHeader"><b>Edit Information</b></span>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <table ce
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <table cellpadding="0" cellspacing="2" style="width: 100%;" border="0">
                            <tr>
                                <td align="left" colspan="1" valign="top" style="width: 18%" class="DivGray">
                                    <asp:Panel ID="pnlACT" runat="server">
                                        <asp:UpdatePanel ID="UpdatePanelACT" runat="server">
                                            <ContentTemplate>
                                                <cc1:PopupControlExtender ID="PopupControlExtender2" runat="server" PopupControlID="pnlActUpdateNote"
                                                    TargetControlID="imgActExc" Position="Bottom" />
                                                <div style="width: 100%;" align="center" >
                                                    <div align="center">
                                                        <span class="LabelsSmall"><b>ACT! Features</b></span>
                                                        <asp:Image ID="imgActExc" runat="server" ImageUrl="~/Images/help.gif"
                                                            Style="cursor: pointer" ToolTip="Help" />
                                                    </div>
                                                    <asp:ImageButton ID="imgAddToACT" runat="server" ImageUrl="~/Images/AddToACT.gif"
                                                        OnClick="imgAddToACT_Click" />                                                                                            

                                                    <br />
                                                    <asp:ImageButton ID="imgUpdateInACT" runat="server" ImageUrl="~/Images/UpdateInACT.gif"
                                                        OnClick="imgUpdateInACT_Click" />
                                                </div>
                                                <asp:Panel ID="pnlActUpdateNote" runat="server" Style="display: none; z-index: 1;"
                                                    Width="250px" CssClass="DivHelp">
                                                    <ul class="LabelsSmall">
                                                        <li>"ADD TO ACT" adds everything to ACT including rates and sales opps. </li>
                                                        <li>"UPDATE" updates everything EXCEPT  Sales Opps. To add Sales Opps, go to the Sales Opps section
                                                            and click on "Add To ACT" next to the Sales Opp. </li>
                                                        <!--<li>"UPDATE RATES" updates Rates only. Click on "UPDATE RATES" whenever rates are changed
                                                            in the Partner Portal. </li>To Update Rates, click
                                                            on the "Update Rates" button below. -->
                                                        <li>
                                                            <span class="LabelsRedSmall">NOTE: Before you update record in ACT!, please check the
                                                            History for the record to see the changes made to the app.</span></li>
                                                    </ul>                                                    
                                                </asp:Panel>
                                                
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <br />
                                    </asp:Panel>
                                    
                                    
                                    <asp:Panel ID="pndAddlServicesPDF" runat="server" >
                                        <div style="width: 100%;" align="center">
                                      
                                            <div align="center">
                                                <b><span class="LabelsSmall">
                                                    <asp:Label ID="lblMCAPDF" runat="server" Font-Bold="True" Text=""></asp:Label>  <asp:Image ID="HelpMCAPDF" runat="server" ImageUrl="~/Images/help.gif"
                                                    Style="cursor: pointer" /><br/>
                                                    <asp:ImageButton CssClass="MenuLink" ID="ImageMCAPDF" runat="server" CausesValidation="false" ImageUrl="~/Images/CreateIMSPDF.gif"
                                                OnClientClick="form1.target ='_blank';" OnClick="imgMCAPDF_Click" />
                                                    

                                                    </span></b>
                                                <cc1:PopupControlExtender ID="PopupControlExtenderMCA" runat="server" PopupControlID="pnlPDFNote"
                                                    TargetControlID="HelpMCAPDF" Position="Bottom" />
                                                
                                            </div>
                                            
                                            <div align="center">
                                            <b><span class="LabelsSmall">
                                            <asp:Label ID="lblLeasePDF" runat="server" Font-Bold="True" Text=""></asp:Label><asp:Image ID="HelpLeasePDF" runat="server" ImageUrl="~/Images/help.gif"
                                                    Style="cursor: pointer" /><br/>
                                                    <asp:ImageButton CssClass="MenuLink" ID="ImageLeasePDF" runat="server" CausesValidation="false" ImageUrl="~/Images/CreateIMSPDF.gif"
                                                OnClientClick="form1.target ='_blank';" OnClick="imgLeasePDF_Click" />
                                                </span></b>
                                           <cc1:PopupControlExtender ID="PopupControlExtenderLease" runat="server" PopupControlID="pnlPDFNote"
                                                    TargetControlID="HelpLeasePDF" Position="Bottom" />
                                            </div>
                                            
                                            <div align="center">
                                            <b><span class="LabelsSmall">
                                            <asp:Label ID="lblGiftcardPDF" runat="server" Font-Bold="True" Text=""></asp:Label><asp:Image ID="HelpGiftPDF" runat="server" ImageUrl="~/Images/help.gif"
                                                    Style="cursor: pointer" /><br/>
                                                    <asp:ImageButton CssClass="MenuLink" ID="ImageGiftCardPDF" runat="server" CausesValidation="false" ImageUrl="~/Images/CreateIMSPDF.gif"
                                                OnClientClick="form1.target ='_blank';" OnClick="imgGiftCardPDF_Click" />
                                                </span></b>
                                            <cc1:PopupControlExtender ID="PopupControlExtenderGift" runat="server" PopupControlID="pnlPDFNote"
                                                    TargetControlID="HelpGiftPDF" Position="Bottom" />
                                            </div>
                                            
                                        </div>
                                    </asp:Panel>
                                    
                                    <br/>
                                    <asp:Panel ID="pnlDeleteApp" Width="100%" runat="server">
                                        <center>
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:ImageButton ID="imgDelete" runat="server" ImageUrl="~/Images/Delete.gif" OnClick="imgDelete_Click" /><br/>
                                                    <asp:Panel ID="pnlDeleteConfirm" runat="server" BackColor="#FFC0C0" BorderColor="Salmon"
                                                        BorderStyle="Double" Visible="False">
                                                        <asp:Image ID="imgExclamation" runat="server" ImageUrl="~/Images/exclamation.gif" />
                                                        <asp:Label ID="lblDeleteMsg" runat="server" Font-Bold="True" Font-Size="Medium" Text="">Confirm Delete?</asp:Label><br />
                                                        <asp:Button ID="btnDeleteYes" runat="server" OnClick="btnDeleteYes_Click" Text="Yes" />
                                                        <asp:Button ID="btnDeleteNo" runat="server" OnClick="btnDeleteNo_Click" Text="No" /></asp:Panel>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="imgDelete" EventName="Click" />
                                                    <asp:PostBackTrigger ControlID="btnDeleteYes" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </center>
                                    </asp:Panel>
                                    <br/>

                                    <br/>
                                </td>
                                <td align="center" colspan="2" valign="top">
                                    <asp:UpdatePanel ID="UpdatePanelDisplayInfo" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Panel ID="pnlDisplayGeneralInfo" runat="server" Width="100%">
                                                <table cellpadding="0" cellspacing="5" style="width: 60%;" border="0">
                                                    <tr>
                                                        <td align="right" style="width: 40%">
                                                            <span class="LabelsSmall">AppId</span></td>
                                                        <td align="left">
                                                            <asp:Label ID="lblAppId" runat="server" Font-Bold="True"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="LabelsSmall">First Name</span></td>
                                                        <td align="left">
                                                            <b><asp:Label ID="lblFirstNameValue" runat="server" Font-Bold="True"></asp:Label></b></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="LabelsSmall">Last Name</span></td>
                                                        <td align="left">
                                                            <b><asp:Label ID="lblLastNameValue" runat="server" Font-Bold="True"></asp:Label></b></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="LabelsSmall">Title</span></td>
                                                        <td align="left">
                                                            <b><asp:Label ID="lblTitleValue" runat="server" Font-Bold="True"></asp:Label></b></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="LabelsSmall">Business Phone</span></td>
                                                        <td align="left">
                                                            <b><asp:Label ID="lblPhoneValue" runat="server" Font-Bold="True"></asp:Label></b><asp:Label ID="lblExt" runat="server" Text=" Ext. "></asp:Label><b><asp:Label ID="lblPhoneExtValue" runat="server" Font-Bold="True"></asp:Label></b></td>
                                                    </tr>
                                                    <!--<tr>
                                                        <td align="right">
                                                            <span class="LabelsSmall">Home Phone</span></td>
                                                        <td align="left">
                                                            <b><asp:Label ID="lblHomePhoneValue" runat="server" Font-Bold="True"></asp:Label></b></td>
                                                    </tr>-->
                                                    <tr>
                                                        <td align="right">
                                                            <span class="LabelsSmall">Mobile Phone</span></td>
                                                        <td align="left">
                                                            <b><asp:Label ID="lblMobilePhoneValue" runat="server" Font-Bold="True"></asp:Label></b></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" colspan="2" style="height: 30px">
                                                            <asp:LinkButton ID="lnkbtnModify" Font-Bold="true" Font-Names="Arial" Font-Size="9"
                                                                runat="server" Text="Modify Profile" OnClick="lnkbtnModify_Click" CssClass="One" /></td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlModifyGeneralInfo" runat="server" Width="100%" Visible="false">
                                                <asp:Panel ID="pnlMainPage" runat="server">
                                                    <asp:ValidationSummary ID="ValidateSummary" runat="server" BackColor="#FFC0C0" BorderColor="red"
                                                        BorderWidth="1px" ForeColor="Black" HeaderText="Please check the fields marked in red."
                                                        Width="250px" />
                                                    <table border="0" cellpadding="0" cellspacing="5" width="100%">
                                                        <tr>
                                                            <td></td>
                                                            <td align="left">
                                                                <b><span class="LabelsRedLarge">*</span><span class="LabelsRed"> - denotes a required field</span></td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" style="width: 32%">
                                                                <span class="LabelsSmall"></span></td>
                                                            <td align="left">
                                                                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                                    <ContentTemplate>
                                                                        <span class="LabelsSmall"><b>Contact Information</b></span>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" style="width: 32%">
                                                                <span class="LabelsSmall">Login Name</span></td>
                                                            <td align="left">
                                                                <asp:UpdatePanel ID="UpdatePanelLoginName" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:TextBox ID="txtLoginName" runat="server" MaxLength="32" Width="140px" Enabled="False" TabIndex="1"></asp:TextBox>
                                                                        <asp:LinkButton ID="lnkbtnUpdateLoginName" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                            OnClick="lnkbtnUpdateLoginName_Click">Edit</asp:LinkButton>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" style="width: 32%">
                                                                <span class="LabelsSmall">First Name</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtFirstName" runat="server" Width="140px" MaxLength="32" TabIndex="2"></asp:TextBox>
                                                                <span class="LabelsRedLarge">*</span>
                                                                <asp:RequiredFieldValidator ID="ValidateFirstName" runat="server" Display="Static"
                                                                    ControlToValidate="txtFirstName" ErrorMessage="First Name" EnableClientScript="False"></asp:RequiredFieldValidator></td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <span class="LabelsSmall">Last Name</span></td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtLastName" runat="server" Width="140px" MaxLength="32" TabIndex="3"></asp:TextBox>
                                                                <span class="LabelsRedLarge">*</span>
                                                                <asp:RequiredFieldValidator ID="ValidateLastName" runat="server" ControlToValidate="txtLastName"
                                                                    ErrorMessage="Last Name" EnableClientScript="False"></asp:RequiredFieldValidator></td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <span class="LabelsSmall">Email</span></td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtEmail" runat="server" MaxLength="50" Width="140px" TabIndex="15"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <span class="LabelsSmall">Title</span></td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtTitle" runat="server" MaxLength="50" Width="140px" TabIndex="5"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <span class="LabelsSmall">Business Phone</span></td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtPhone" runat="server" Width="140px" MaxLength="16" TabIndex="6"></asp:TextBox>
                                                                <span class="LabelsSmall">Ext.</span>
                                                                <asp:TextBox ID="txtPhoneExt" runat="server" Width="40px" MaxLength="4" TabIndex="7"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="REExt" runat="server" ControlToValidate="txtPhoneExt"
                                                                    ErrorMessage="Numbers Only" ValidationExpression="[0-9]*$" EnableClientScript="False"></asp:RegularExpressionValidator></td>
                                                        </tr>
                                                        <!--<tr>
                                                            <td align="right">
                                                                <span class="LabelsSmall">Home Phone</span></td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtHomePhone" runat="server" MaxLength="50" Width="140px" TabIndex="8"></asp:TextBox>
                                                                </td>
                                                        </tr>-->
                                                        <tr>
                                                            <td align="right">
                                                                <span class="LabelsSmall">Mobile Phone</span></td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtMobilePhone" runat="server" Width="140px" MaxLength="16" TabIndex="9"></asp:TextBox>
                                                                </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <span class="LabelsSmall">Sales Rep</span></td>
                                                            <td align="left">
                                                                <asp:DropDownList ID="lstSalesRep" runat="server" TabIndex="10">
                                                                </asp:DropDownList>
                                                                <span class="LabelsRedLarge">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                                                    runat="server" ControlToValidate="lstSalesRep" ErrorMessage="Sales Rep"></asp:RequiredFieldValidator></td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" valign="middle">
                                                                <span class="LabelsSmall">Referred By<br />(DBA - Company Name - PartnerID)</span></td>
                                                            <td align="left">
                                                                <asp:DropDownList ID="lstReferredBy" runat="server" TabIndex="11">
                                                                </asp:DropDownList>
                                                                <span class="LabelsRedLarge">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                                                    runat="server" ControlToValidate="lstReferredBy" ErrorMessage="Referred By"></asp:RequiredFieldValidator></td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <span class="LabelsSmall">Other Referral</span></td>
                                                            <td align="left"><asp:DropDownList ID="lstOtherReferral" runat="server" TabIndex="12">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        
                                                        <tr>
                                                            <td colspan="2" align="center">
                                                                <asp:Button ID="btnContinue" runat="server" Text="Submit" OnClick="btnContinue_Click"
                                                                    TabIndex="16" />
                                                                &nbsp;&nbsp;
                                                                <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel"
                                                                    TabIndex="17" CausesValidation="False" UseSubmitBehavior="False" /></td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </asp:Panel>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <asp:Panel ID="pnlLoginAttempts" runat="server" Width="100%" CssClass="DivHelp">
                                        NOTE: This application has been locked because of too many login attempts. To unlock,
                                        click
                                        <asp:LinkButton ID="lnkbtnUnlock" runat="server" Font-Bold="true" Font-Names="Arial"
                                            Font-Size="Small" OnClick="lnkbtnUnlock_Click">here</asp:LinkButton></asp:Panel>
                                                             
                                            <asp:Panel ID="pnlChasePDF" runat="server" Visible="False" Width="300px" CssClass="DivHelp">
                                                <strong><span class="Labels">Choose the Chase PDF you want to create:</span></strong><br/>
                                                <!--<asp:LinkButton ID="btnChaseAbout" runat="server" CssClass="One" OnClick="btnChaseAbout_Click" CausesValidation="false">About Merchant</asp:LinkButton>
                                                <br />
                                                <asp:LinkButton ID="btnChaseFee" runat="server" CssClass="One" OnClick="btnChaseFee_Click" CausesValidation="false">Fee Schedule</asp:LinkButton>
                                                <br />
                                                <asp:LinkButton ID="btnChaseMP" runat="server" CssClass="One" OnClick="btnChaseMP_Click" CausesValidation="false">Merchant Processing</asp:LinkButton>
                                                <br />
                                                <asp:LinkButton ID="btnCreditAdd" runat="server" CssClass="One" OnClick="btnChaseCreditAdd_Click" CausesValidation="false">Credit Addendum</asp:LinkButton>
                                                <br />
                                                <asp:HyperLink CssClass="One" ID="lnkOpGuide" runat="server" NavigateUrl="~/PDF/Chase Operating Guide.pdf"
                                                    Target="_blank">Chase Operating Guide</asp:HyperLink>-->
                                                <asp:LinkButton ID="btnChaseMPA" runat="server" CssClass="One" Font-Names="Arial"
                                                    Font-Size="10pt" OnClick="btnChaseMPA_Click" CausesValidation="False">Chase Merchant Application and Agreement</asp:LinkButton><br />    
                                                <asp:LinkButton ID="btnChaseFS3Tier" runat="server" CssClass="One" Font-Names="Arial"
                                                    Font-Size="10pt" OnClick="btnChaseFS3Tier_Click" Visible="false" CausesValidation="False">Chase Fee Schedule 3 tier</asp:LinkButton><br />    
                                                <asp:LinkButton ID="btnChaseFSInterchangePlus" runat="server" CssClass="One" Font-Names="Arial"
                                                    Font-Size="10pt" OnClick="btnChaseFSInterchangePlus_Click" Visible="false" CausesValidation="False">Chase Fee Schedule Interchange Plus</asp:LinkButton><br />
                                            </asp:Panel>
                                            <asp:Panel ID="pnlSagePDF" runat="server" Visible="False" Width="300px" CssClass="DivHelp">
                                                <strong><span class="Labels">Choose the Sage PDF you want to create:</span></strong><br/>
                                                        <asp:LinkButton ID="btnSageApp" runat="server" CssClass="One" OnClick="btnSageApp_Click" CausesValidation="false">Sage Application</asp:LinkButton><br/>
                                                        <!--<asp:LinkButton ID="btnSageMOTO" runat="server" OnClick="btnSageMOTO_Click" CssClass="One">Sage MOTO Application</asp:LinkButton><br />-->
                                                        <asp:HyperLink ID="lnkSageAgreement" CssClass="One" runat="server" NavigateUrl="~/PDF/Sage Merchant Agreement.pdf" Target="_blank" color="blue">Terms and Conditions</asp:HyperLink>
                                                        <!--<asp:LinkButton ID="lnkAmendment" Visible="False" Font-Names="Arial" CssClass="Link" runat="server" OnClick="lnkAmendment_Click" >Amendment - Month to Month</asp:LinkButton>-->
                                            </asp:Panel>
                             
                                    
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate>
                                            <asp:Label ID="lblError" CssClass="LabelsError" EnableTheming="false" runat="server"
                                                Visible="False"></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>
                                <asp:Panel ID="pnlConfirm" runat="server" BackColor="#FFC0C0" BorderColor="Salmon"
                                    BorderStyle="Double" Visible="False" Width="200px">
                                    <asp:Label ID="lblErrorMessage" runat="server"></asp:Label><br />
                                    <asp:Label ID="lblMessage" runat="server" Text="Do you want to create a new record?"></asp:Label><br />
                                    <asp:Button ID="btnCreateRecordYes" runat="server" OnClick="btnCreateRecordYes_Click"
                                        Text="Yes" />
                                    <asp:Button ID="btnCreateRecordNo" runat="server" OnClick="btnCreateRecordNo_Click"
                                        Text="No" /></asp:Panel>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="imgAddToACT" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <asp:Panel ID="pnlPDFNote" runat="server" Style="display: none; z-index: 1;" Width="250px"
                            CssClass="DivHelp">
                            NOTE: PDF Creation works only with the Microsoft Internet Explorer (Version 7.0 
                            or above), Mozilla Firefox (Version 2.0 or above) and Safari (Version 4.0 or above). For viewing PDFs, please
                            install Abode Reader Version 8.0 or higher. Download the latest version here: 
                            <asp:HyperLink ID="lnkAdobe" CssClass="One" runat="server" NavigateUrl="http://www.adobe.com"
                                Target="_blank">www.adobe.com</asp:HyperLink></asp:Panel>
                        <cc1:TabContainer runat="server" ID="Tabs">
                            <cc1:TabPanel ID="TabNotes" runat="server" HeaderText="Notes">
                                <ContentTemplate>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Panel ID="pnlNotes" runat="server" Width="100%" BackColor="#ffffff" Visible="True">
                                                <div style="width: 100%;" align="center">
                                                    <asp:GridView ID="grdNotes" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                        ForeColor="#333333" GridLines="Vertical" OnRowDeleting="grdNotes_RowDeleting"
                                                        OnRowDataBound="grdNotes_RowDataBound">
                                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                        <Columns>
                                                            <asp:BoundField DataField="username" HeaderText="Modified By"></asp:BoundField>
                                                            <asp:BoundField DataField="DateRecorded" HeaderText="Date Recorded" />
                                                            <asp:BoundField DataField="NoteText" HeaderText="Note Text"></asp:BoundField>
                                                            <asp:BoundField DataField="NoteID" HeaderText="Note ID"></asp:BoundField>
                                                            <asp:CommandField CausesValidation="False" ShowDeleteButton="True" />
                                                        </Columns>
                                                        <RowStyle BackColor="#bce2d3" Font-Names="Arial" Font-Size="X-Small" ForeColor="#333333" />
                                                        <EditRowStyle BackColor="#999999" />
                                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="MenuHeader" BackColor="#5D7B9D" />
                                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                    </asp:GridView>
                                                    <br />
                                                    <asp:Label ID="lblNotesHeader" runat="server" Font-Bold="True" Font-Size="Medium"
                                                        Text="Add Notes"></asp:Label><br/>
                                                        <!--<asp:DropDownList ID="lstNoteType" runat="server" Font-Size="Medium">
                                                            <asp:ListItem Text="Notes" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text="Tracking Number" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="Pending reason" Value="2"></asp:ListItem>
                                                        </asp:DropDownList><br/>-->
                                                    <asp:TextBox ID="txtNotes" runat="server" TabIndex="35" TextMode="MultiLine"></asp:TextBox>
                                                    <div align="center">
                                                        <asp:CheckBox ID="chkNotify" runat="server" Font-Bold="True" TabIndex="36" Text="Notify New Account/Sales Support" />&nbsp;<br/>
                                                        <asp:Button ID="btnAddNote" runat="server" OnClick="btnAddNote_Click" TabIndex="37"
                                                            Text="Add Note" />
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </ContentTemplate>
                            </cc1:TabPanel>
                            <cc1:TabPanel ID="TabHistory" runat="server" HeaderText="History">
                                <ContentTemplate>
                                    <asp:UpdatePanel runat="server" ID="UpdatePanelHistory" UpdateMode="Always">
                                        <ContentTemplate>
                                            <asp:GridView ID="grdHistory" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                ForeColor="#333333" GridLines="Vertical">
                                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                <Columns>
                                                    <asp:BoundField DataField="Contact" HeaderText="Modified By" SortExpression="Contact"/>
                                                    <asp:BoundField DataField="Action" HeaderText="Action" SortExpression="Action" />
                                                    <asp:BoundField DataField="RecordedDate" HeaderText="Date Recorded" SortExpression="DateRecorded">
                                                    </asp:BoundField>
                                                </Columns>
                                                <RowStyle BackColor="Honeydew" Font-Names="Arial" Font-Size="X-Small" ForeColor="#333333" />
                                                <EditRowStyle BackColor="#999999" />
                                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                <HeaderStyle CssClass="MenuHeader" BackColor="#5D7B9D" />
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                            </asp:GridView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </ContentTemplate>                                
                            </cc1:TabPanel>
                            <cc1:TabPanel ID="TabSalesOpps" runat="server" HeaderText="Opportunities">
                                <ContentTemplate>
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:LinkButton ID="lnkAddSalesOpps" runat="server" Font-Bold="True" Font-Names="Arial"
                                                Font-Size="9" OnClick="lnkAddSalesOpps_Click" Font-Underline="false">Click here to Add a Sales Opportunity</asp:LinkButton>
                                            <asp:Panel ID="pnlAddOpp" runat="server" Width="100%" Visible="False">
                                                <asp:Label ID="lblAddSalesOpp" runat="server" Font-Bold="True" Text="Add Sales Opportunity"
                                                    Font-Names="Arial" Font-Size="Small"></asp:Label>
                                                <table class="BlueBorder" border="0" style="width:100%">
                                                    <tr>
                                                        <td class="DivBlue">
                                                            <b><span class="MenuHeader">Product Name</span></b>
                                                        </td>
                                                        <td class="DivBlue">
                                                            <b><span class="MenuHeader">Sell Price</span></b>
                                                        </td>
                                                        <td class="DivBlue">
                                                            <b><span class="MenuHeader">COG</span></b>
                                                        </td>
                                                        <td class="DivBlue">
                                                            <b><span class="MenuHeader">Qty</span></b>
                                                        </td>
                                                        <td class="DivBlue">
                                                            <b><span class="MenuHeader">Sub Total</span></b>
                                                        </td>
                                                        <td class="DivBlue">
                                                            <b><span class="MenuHeader">Sales Rep</span></b></td>
                                                        <td class="DivBlue">
                                                            <b><span class="MenuHeader">Payment Method</span></b></td>
                                                        <td class="DivBlue">
                                                            <b><span class="MenuHeader">Reprogram</span></b></td>
                                                            <td class="DivBlue">
                                                            <b><span class="MenuHeader">Terminal ID</span></b></td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top">
                                                            <asp:DropDownList ID="lstProductName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="lstProductName_SelectedIndexChanged">
                                                            </asp:DropDownList><span class="LabelsRedLarge">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                                                                    runat="server" ControlToValidate="lstProductName" ErrorMessage="Productname"></asp:RequiredFieldValidator></td>
                                                        <td valign="top">
                                                            <asp:TextBox ID="txtAddSellPrice" runat="server" MaxLength="7" Width="50"></asp:TextBox></td>
                                                        <td valign="top">
                                                            <asp:TextBox ID="txtAddCOG" runat="server" Width="50" Enabled="false"></asp:TextBox></td>
                                                        <td valign="top">
                                                            <asp:DropDownList ID="lstAddQuantity" runat="server" AutoPostBack="True" OnSelectedIndexChanged="lstAddQuantity_SelectedIndexChanged">
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
                                                                <asp:ListItem>12</asp:ListItem>
                                                                <asp:ListItem>13</asp:ListItem>
                                                                <asp:ListItem>14</asp:ListItem>
                                                                <asp:ListItem>15</asp:ListItem>
                                                                <asp:ListItem>16</asp:ListItem>
                                                                <asp:ListItem>17</asp:ListItem>
                                                                <asp:ListItem>18</asp:ListItem>
                                                                <asp:ListItem>19</asp:ListItem>
                                                                <asp:ListItem>20</asp:ListItem>
                                                                <asp:ListItem>21</asp:ListItem>
                                                                <asp:ListItem>22</asp:ListItem>
                                                                <asp:ListItem>23</asp:ListItem>
                                                                <asp:ListItem>24</asp:ListItem>
                                                                <asp:ListItem>25</asp:ListItem>
                                                                <asp:ListItem>26</asp:ListItem>
                                                                <asp:ListItem>27</asp:ListItem>
                                                                <asp:ListItem>28</asp:ListItem>
                                                                <asp:ListItem>29</asp:ListItem>
                                                                <asp:ListItem>30</asp:ListItem>
                                                                <asp:ListItem>31</asp:ListItem>
                                                                <asp:ListItem>32</asp:ListItem>
                                                                <asp:ListItem>33</asp:ListItem>
                                                                <asp:ListItem>34</asp:ListItem>
                                                                <asp:ListItem>35</asp:ListItem>
                                                                <asp:ListItem>36</asp:ListItem>
                                                                <asp:ListItem>37</asp:ListItem>
                                                                <asp:ListItem>38</asp:ListItem>
                                                                <asp:ListItem>39</asp:ListItem>
                                                                <asp:ListItem>40</asp:ListItem>
                                                                <asp:ListItem>41</asp:ListItem>
                                                                <asp:ListItem>42</asp:ListItem>
                                                                <asp:ListItem>43</asp:ListItem>
                                                                <asp:ListItem>44</asp:ListItem>
                                                                <asp:ListItem>45</asp:ListItem>
                                                                <asp:ListItem>46</asp:ListItem>
                                                                <asp:ListItem>47</asp:ListItem>
                                                                <asp:ListItem>48</asp:ListItem>
                                                                <asp:ListItem>49</asp:ListItem>
                                                                <asp:ListItem>50</asp:ListItem>
                                                                <asp:ListItem>51</asp:ListItem>
                                                                <asp:ListItem>52</asp:ListItem>
                                                                <asp:ListItem>53</asp:ListItem>
                                                                <asp:ListItem>54</asp:ListItem>
                                                                <asp:ListItem>55</asp:ListItem>
                                                                <asp:ListItem>56</asp:ListItem>
                                                                <asp:ListItem>57</asp:ListItem>
                                                                <asp:ListItem>58</asp:ListItem>
                                                                <asp:ListItem>59</asp:ListItem>
                                                                <asp:ListItem>60</asp:ListItem>
                                                                <asp:ListItem>61</asp:ListItem>
                                                                <asp:ListItem>62</asp:ListItem>
                                                                <asp:ListItem>63</asp:ListItem>
                                                                <asp:ListItem>64</asp:ListItem>
                                                                <asp:ListItem>65</asp:ListItem>
                                                                <asp:ListItem>66</asp:ListItem>
                                                                <asp:ListItem>67</asp:ListItem>
                                                                <asp:ListItem>68</asp:ListItem>
                                                                <asp:ListItem>69</asp:ListItem>
                                                                <asp:ListItem>70</asp:ListItem>
                                                                <asp:ListItem>71</asp:ListItem>
                                                                <asp:ListItem>72</asp:ListItem>
                                                                <asp:ListItem>73</asp:ListItem>
                                                                <asp:ListItem>74</asp:ListItem>
                                                                <asp:ListItem>75</asp:ListItem>
                                                                <asp:ListItem>76</asp:ListItem>
                                                                <asp:ListItem>77</asp:ListItem>
                                                                <asp:ListItem>78</asp:ListItem>
                                                                <asp:ListItem>79</asp:ListItem>
                                                                <asp:ListItem>80</asp:ListItem>
                                                                <asp:ListItem>81</asp:ListItem>
                                                                <asp:ListItem>82</asp:ListItem>
                                                                <asp:ListItem>83</asp:ListItem>
                                                                <asp:ListItem>84</asp:ListItem>
                                                                <asp:ListItem>85</asp:ListItem>
                                                                <asp:ListItem>86</asp:ListItem>
                                                                <asp:ListItem>87</asp:ListItem>
                                                                <asp:ListItem>88</asp:ListItem>
                                                                <asp:ListItem>89</asp:ListItem>
                                                                <asp:ListItem>90</asp:ListItem>
                                                                <asp:ListItem>91</asp:ListItem>
                                                                <asp:ListItem>92</asp:ListItem>
                                                                <asp:ListItem>93</asp:ListItem>
                                                                <asp:ListItem>94</asp:ListItem>
                                                                <asp:ListItem>95</asp:ListItem>
                                                                <asp:ListItem>96</asp:ListItem>
                                                                <asp:ListItem>97</asp:ListItem>
                                                                <asp:ListItem>98</asp:ListItem>
                                                                <asp:ListItem>99</asp:ListItem>
                                                                <asp:ListItem>100</asp:ListItem>
                                                                <asp:ListItem>101</asp:ListItem>
                                                                <asp:ListItem>102</asp:ListItem>
                                                                <asp:ListItem>103</asp:ListItem>
                                                                <asp:ListItem>104</asp:ListItem>
                                                                <asp:ListItem>105</asp:ListItem>
                                                                <asp:ListItem>106</asp:ListItem>
                                                                <asp:ListItem>107</asp:ListItem>
                                                                <asp:ListItem>108</asp:ListItem>
                                                                <asp:ListItem>109</asp:ListItem>
                                                                <asp:ListItem>110</asp:ListItem>
                                                                <asp:ListItem>111</asp:ListItem>
                                                                <asp:ListItem>112</asp:ListItem>
                                                                <asp:ListItem>113</asp:ListItem>
                                                                <asp:ListItem>114</asp:ListItem>
                                                                <asp:ListItem>115</asp:ListItem>
                                                                <asp:ListItem>116</asp:ListItem>
                                                                <asp:ListItem>117</asp:ListItem>
                                                                <asp:ListItem>118</asp:ListItem>
                                                                <asp:ListItem>119</asp:ListItem>
                                                                <asp:ListItem>120</asp:ListItem>
                                                                <asp:ListItem>121</asp:ListItem>
                                                                <asp:ListItem>122</asp:ListItem>
                                                                <asp:ListItem>123</asp:ListItem>
                                                                <asp:ListItem>124</asp:ListItem>
                                                                <asp:ListItem>125</asp:ListItem>
                                                                <asp:ListItem>126</asp:ListItem>
                                                                <asp:ListItem>127</asp:ListItem>
                                                                <asp:ListItem>128</asp:ListItem>
                                                                <asp:ListItem>129</asp:ListItem>
                                                                <asp:ListItem>130</asp:ListItem>
                                                                <asp:ListItem>131</asp:ListItem>
                                                                <asp:ListItem>132</asp:ListItem>
                                                                <asp:ListItem>133</asp:ListItem>
                                                                <asp:ListItem>134</asp:ListItem>
                                                                <asp:ListItem>135</asp:ListItem>
                                                                <asp:ListItem>136</asp:ListItem>
                                                                <asp:ListItem>137</asp:ListItem>
                                                                <asp:ListItem>138</asp:ListItem>
                                                                <asp:ListItem>139</asp:ListItem>
                                                                <asp:ListItem>140</asp:ListItem>
                                                                <asp:ListItem>141</asp:ListItem>
                                                                <asp:ListItem>142</asp:ListItem>
                                                                <asp:ListItem>143</asp:ListItem>
                                                                <asp:ListItem>144</asp:ListItem>
                                                                <asp:ListItem>145</asp:ListItem>
                                                                <asp:ListItem>146</asp:ListItem>
                                                                <asp:ListItem>147</asp:ListItem>
                                                                <asp:ListItem>148</asp:ListItem>
                                                                <asp:ListItem>149</asp:ListItem>
                                                                <asp:ListItem>150</asp:ListItem>
                                                                <asp:ListItem>151</asp:ListItem>
                                                                <asp:ListItem>152</asp:ListItem>
                                                                <asp:ListItem>153</asp:ListItem>
                                                                <asp:ListItem>154</asp:ListItem>
                                                                <asp:ListItem>155</asp:ListItem>
                                                                <asp:ListItem>156</asp:ListItem>
                                                                <asp:ListItem>157</asp:ListItem>
                                                                <asp:ListItem>158</asp:ListItem>
                                                                <asp:ListItem>159</asp:ListItem>
                                                                <asp:ListItem>160</asp:ListItem>
                                                                <asp:ListItem>161</asp:ListItem>
                                                                <asp:ListItem>162</asp:ListItem>
                                                                <asp:ListItem>163</asp:ListItem>
                                                                <asp:ListItem>164</asp:ListItem>
                                                                <asp:ListItem>165</asp:ListItem>
                                                                <asp:ListItem>166</asp:ListItem>
                                                                <asp:ListItem>167</asp:ListItem>
                                                                <asp:ListItem>168</asp:ListItem>
                                                                <asp:ListItem>169</asp:ListItem>
                                                                <asp:ListItem>170</asp:ListItem>
                                                                <asp:ListItem>171</asp:ListItem>
                                                                <asp:ListItem>172</asp:ListItem>
                                                                <asp:ListItem>173</asp:ListItem>
                                                                <asp:ListItem>174</asp:ListItem>
                                                                <asp:ListItem>175</asp:ListItem>
                                                                <asp:ListItem>176</asp:ListItem>
                                                                <asp:ListItem>177</asp:ListItem>
                                                                <asp:ListItem>178</asp:ListItem>
                                                                <asp:ListItem>179</asp:ListItem>
                                                                <asp:ListItem>180</asp:ListItem>
                                                                <asp:ListItem>181</asp:ListItem>
                                                                <asp:ListItem>182</asp:ListItem>
                                                                <asp:ListItem>183</asp:ListItem>
                                                                <asp:ListItem>184</asp:ListItem>
                                                                <asp:ListItem>185</asp:ListItem>
                                                                <asp:ListItem>186</asp:ListItem>
                                                                <asp:ListItem>187</asp:ListItem>
                                                                <asp:ListItem>188</asp:ListItem>
                                                                <asp:ListItem>189</asp:ListItem>
                                                                <asp:ListItem>190</asp:ListItem>
                                                                <asp:ListItem>191</asp:ListItem>
                                                                <asp:ListItem>192</asp:ListItem>
                                                                <asp:ListItem>193</asp:ListItem>
                                                                <asp:ListItem>194</asp:ListItem>
                                                                <asp:ListItem>195</asp:ListItem>
                                                                <asp:ListItem>196</asp:ListItem>
                                                                <asp:ListItem>197</asp:ListItem>
                                                                <asp:ListItem>198</asp:ListItem>
                                                                <asp:ListItem>199</asp:ListItem>
                                                                <asp:ListItem>401</asp:ListItem>
                                                                <asp:ListItem>200</asp:ListItem>
                                                                <asp:ListItem>202</asp:ListItem>
                                                                <asp:ListItem>203</asp:ListItem>
                                                                <asp:ListItem>204</asp:ListItem>
                                                                <asp:ListItem>205</asp:ListItem>
                                                                <asp:ListItem>206</asp:ListItem>
                                                                <asp:ListItem>207</asp:ListItem>
                                                                <asp:ListItem>208</asp:ListItem>
                                                                <asp:ListItem>209</asp:ListItem>
                                                                <asp:ListItem>210</asp:ListItem>
                                                                <asp:ListItem>211</asp:ListItem>
                                                                <asp:ListItem>212</asp:ListItem>
                                                                <asp:ListItem>213</asp:ListItem>
                                                                <asp:ListItem>214</asp:ListItem>
                                                                <asp:ListItem>215</asp:ListItem>
                                                                <asp:ListItem>216</asp:ListItem>
                                                                <asp:ListItem>217</asp:ListItem>
                                                                <asp:ListItem>218</asp:ListItem>
                                                                <asp:ListItem>219</asp:ListItem>
                                                                <asp:ListItem>220</asp:ListItem>
                                                                <asp:ListItem>221</asp:ListItem>
                                                                <asp:ListItem>222</asp:ListItem>
                                                                <asp:ListItem>223</asp:ListItem>
                                                                <asp:ListItem>224</asp:ListItem>
                                                                <asp:ListItem>225</asp:ListItem>
                                                                <asp:ListItem>226</asp:ListItem>
                                                                <asp:ListItem>227</asp:ListItem>
                                                                <asp:ListItem>228</asp:ListItem>
                                                                <asp:ListItem>229</asp:ListItem>
                                                                <asp:ListItem>230</asp:ListItem>
                                                                <asp:ListItem>231</asp:ListItem>
                                                                <asp:ListItem>232</asp:ListItem>
                                                                <asp:ListItem>233</asp:ListItem>
                                                                <asp:ListItem>234</asp:ListItem>
                                                                <asp:ListItem>235</asp:ListItem>
                                                                <asp:ListItem>236</asp:ListItem>
                                                                <asp:ListItem>237</asp:ListItem>
                                                                <asp:ListItem>238</asp:ListItem>
                                                                <asp:ListItem>239</asp:ListItem>
                                                                <asp:ListItem>240</asp:ListItem>
                                                                <asp:ListItem>241</asp:ListItem>
                                                                <asp:ListItem>242</asp:ListItem>
                                                                <asp:ListItem>243</asp:ListItem>
                                                                <asp:ListItem>244</asp:ListItem>
                                                                <asp:ListItem>245</asp:ListItem>
                                                                <asp:ListItem>246</asp:ListItem>
                                                                <asp:ListItem>247</asp:ListItem>
                                                                <asp:ListItem>248</asp:ListItem>
                                                                <asp:ListItem>249</asp:ListItem>
                                                                <asp:ListItem>250</asp:ListItem>
                                                                <asp:ListItem>251</asp:ListItem>
                                                                <asp:ListItem>252</asp:ListItem>
                                                                <asp:ListItem>253</asp:ListItem>
                                                                <asp:ListItem>254</asp:ListItem>
                                                                <asp:ListItem>255</asp:ListItem>
                                                                <asp:ListItem>256</asp:ListItem>
                                                                <asp:ListItem>257</asp:ListItem>
                                                                <asp:ListItem>258</asp:ListItem>
                                                                <asp:ListItem>259</asp:ListItem>
                                                                <asp:ListItem>260</asp:ListItem>
                                                                <asp:ListItem>261</asp:ListItem>
                                                                <asp:ListItem>262</asp:ListItem>
                                                                <asp:ListItem>263</asp:ListItem>
                                                                <asp:ListItem>264</asp:ListItem>
                                                                <asp:ListItem>265</asp:ListItem>
                                                                <asp:ListItem>266</asp:ListItem>
                                                                <asp:ListItem>267</asp:ListItem>
                                                                <asp:ListItem>268</asp:ListItem>
                                                                <asp:ListItem>269</asp:ListItem>
                                                                <asp:ListItem>270</asp:ListItem>
                                                                <asp:ListItem>271</asp:ListItem>
                                                                <asp:ListItem>272</asp:ListItem>
                                                                <asp:ListItem>273</asp:ListItem>
                                                                <asp:ListItem>274</asp:ListItem>
                                                                <asp:ListItem>275</asp:ListItem>
                                                                <asp:ListItem>276</asp:ListItem>
                                                                <asp:ListItem>277</asp:ListItem>
                                                                <asp:ListItem>278</asp:ListItem>
                                                                <asp:ListItem>279</asp:ListItem>
                                                                <asp:ListItem>280</asp:ListItem>
                                                                <asp:ListItem>281</asp:ListItem>
                                                                <asp:ListItem>282</asp:ListItem>
                                                                <asp:ListItem>283</asp:ListItem>
                                                                <asp:ListItem>284</asp:ListItem>
                                                                <asp:ListItem>285</asp:ListItem>
                                                                <asp:ListItem>286</asp:ListItem>
                                                                <asp:ListItem>287</asp:ListItem>
                                                                <asp:ListItem>288</asp:ListItem>
                                                                <asp:ListItem>289</asp:ListItem>
                                                                <asp:ListItem>290</asp:ListItem>
                                                                <asp:ListItem>291</asp:ListItem>
                                                                <asp:ListItem>292</asp:ListItem>
                                                                <asp:ListItem>293</asp:ListItem>
                                                                <asp:ListItem>294</asp:ListItem>
                                                                <asp:ListItem>295</asp:ListItem>
                                                                <asp:ListItem>296</asp:ListItem>
                                                                <asp:ListItem>297</asp:ListItem>
                                                                <asp:ListItem>298</asp:ListItem>
                                                                <asp:ListItem>299</asp:ListItem>
                                                                <asp:ListItem>300</asp:ListItem>
                                                                <asp:ListItem>301</asp:ListItem>
                                                                <asp:ListItem>302</asp:ListItem>
                                                                <asp:ListItem>303</asp:ListItem>
                                                                <asp:ListItem>304</asp:ListItem>
                                                                <asp:ListItem>305</asp:ListItem>
                                                                <asp:ListItem>306</asp:ListItem>
                                                                <asp:ListItem>307</asp:ListItem>
                                                                <asp:ListItem>308</asp:ListItem>
                                                                <asp:ListItem>309</asp:ListItem>
                                                                <asp:ListItem>310</asp:ListItem>
                                                                <asp:ListItem>311</asp:ListItem>
                                                                <asp:ListItem>312</asp:ListItem>
                                                                <asp:ListItem>313</asp:ListItem>
                                                                <asp:ListItem>314</asp:ListItem>
                                                                <asp:ListItem>315</asp:ListItem>
                                                                <asp:ListItem>316</asp:ListItem>
                                                                <asp:ListItem>317</asp:ListItem>
                                                                <asp:ListItem>318</asp:ListItem>
                                                                <asp:ListItem>319</asp:ListItem>
                                                                <asp:ListItem>320</asp:ListItem>
                                                                <asp:ListItem>321</asp:ListItem>
                                                                <asp:ListItem>322</asp:ListItem>
                                                                <asp:ListItem>323</asp:ListItem>
                                                                <asp:ListItem>324</asp:ListItem>
                                                                <asp:ListItem>325</asp:ListItem>
                                                                <asp:ListItem>326</asp:ListItem>
                                                                <asp:ListItem>327</asp:ListItem>
                                                                <asp:ListItem>328</asp:ListItem>
                                                                <asp:ListItem>329</asp:ListItem>
                                                                <asp:ListItem>330</asp:ListItem>
                                                                <asp:ListItem>331</asp:ListItem>
                                                                <asp:ListItem>332</asp:ListItem>
                                                                <asp:ListItem>333</asp:ListItem>
                                                                <asp:ListItem>334</asp:ListItem>
                                                                <asp:ListItem>335</asp:ListItem>
                                                                <asp:ListItem>336</asp:ListItem>
                                                                <asp:ListItem>337</asp:ListItem>
                                                                <asp:ListItem>338</asp:ListItem>
                                                                <asp:ListItem>339</asp:ListItem>
                                                                <asp:ListItem>340</asp:ListItem>
                                                                <asp:ListItem>341</asp:ListItem>
                                                                <asp:ListItem>342</asp:ListItem>
                                                                <asp:ListItem>343</asp:ListItem>
                                                                <asp:ListItem>344</asp:ListItem>
                                                                <asp:ListItem>345</asp:ListItem>
                                                                <asp:ListItem>346</asp:ListItem>
                                                                <asp:ListItem>347</asp:ListItem>
                                                                <asp:ListItem>348</asp:ListItem>
                                                                <asp:ListItem>349</asp:ListItem>
                                                                <asp:ListItem>350</asp:ListItem>
                                                                <asp:ListItem>351</asp:ListItem>
                                                                <asp:ListItem>352</asp:ListItem>
                                                                <asp:ListItem>353</asp:ListItem>
                                                                <asp:ListItem>354</asp:ListItem>
                                                                <asp:ListItem>355</asp:ListItem>
                                                                <asp:ListItem>356</asp:ListItem>
                                                                <asp:ListItem>357</asp:ListItem>
                                                                <asp:ListItem>358</asp:ListItem>
                                                                <asp:ListItem>359</asp:ListItem>
                                                                <asp:ListItem>360</asp:ListItem>
                                                                <asp:ListItem>361</asp:ListItem>
                                                                <asp:ListItem>362</asp:ListItem>
                                                                <asp:ListItem>363</asp:ListItem>
                                                                <asp:ListItem>364</asp:ListItem>
                                                                <asp:ListItem>365</asp:ListItem>
                                                                <asp:ListItem>366</asp:ListItem>
                                                                <asp:ListItem>367</asp:ListItem>
                                                                <asp:ListItem>368</asp:ListItem>
                                                                <asp:ListItem>369</asp:ListItem>
                                                                <asp:ListItem>370</asp:ListItem>
                                                                <asp:ListItem>371</asp:ListItem>
                                                                <asp:ListItem>372</asp:ListItem>
                                                                <asp:ListItem>373</asp:ListItem>
                                                                <asp:ListItem>374</asp:ListItem>
                                                                <asp:ListItem>375</asp:ListItem>
                                                                <asp:ListItem>376</asp:ListItem>
                                                                <asp:ListItem>377</asp:ListItem>
                                                                <asp:ListItem>378</asp:ListItem>
                                                                <asp:ListItem>379</asp:ListItem>
                                                                <asp:ListItem>380</asp:ListItem>
                                                                <asp:ListItem>381</asp:ListItem>
                                                                <asp:ListItem>382</asp:ListItem>
                                                                <asp:ListItem>383</asp:ListItem>
                                                                <asp:ListItem>384</asp:ListItem>
                                                                <asp:ListItem>385</asp:ListItem>
                                                                <asp:ListItem>386</asp:ListItem>
                                                                <asp:ListItem>387</asp:ListItem>
                                                                <asp:ListItem>388</asp:ListItem>
                                                                <asp:ListItem>389</asp:ListItem>
                                                                <asp:ListItem>390</asp:ListItem>
                                                                <asp:ListItem>391</asp:ListItem>
                                                                <asp:ListItem>392</asp:ListItem>
                                                                <asp:ListItem>393</asp:ListItem>
                                                                <asp:ListItem>394</asp:ListItem>
                                                                <asp:ListItem>395</asp:ListItem>
                                                                <asp:ListItem>396</asp:ListItem>
                                                                <asp:ListItem>397</asp:ListItem>
                                                                <asp:ListItem>398</asp:ListItem>
                                                                <asp:ListItem>399</asp:ListItem>
                                                                <asp:ListItem>400</asp:ListItem>
                                                                <asp:ListItem>401</asp:ListItem>
                                                                <asp:ListItem>402</asp:ListItem>
                                                                <asp:ListItem>403</asp:ListItem>
                                                                <asp:ListItem>404</asp:ListItem>
                                                                <asp:ListItem>405</asp:ListItem>
                                                                <asp:ListItem>406</asp:ListItem>
                                                                <asp:ListItem>407</asp:ListItem>
                                                                <asp:ListItem>408</asp:ListItem>
                                                                <asp:ListItem>409</asp:ListItem>
                                                                <asp:ListItem>410</asp:ListItem>
                                                                <asp:ListItem>411</asp:ListItem>
                                                                <asp:ListItem>412</asp:ListItem>
                                                                <asp:ListItem>413</asp:ListItem>
                                                                <asp:ListItem>414</asp:ListItem>
                                                                <asp:ListItem>415</asp:ListItem>
                                                                <asp:ListItem>416</asp:ListItem>
                                                                <asp:ListItem>417</asp:ListItem>
                                                                <asp:ListItem>418</asp:ListItem>
                                                                <asp:ListItem>419</asp:ListItem>
                                                                <asp:ListItem>420</asp:ListItem>
                                                                <asp:ListItem>421</asp:ListItem>
                                                                <asp:ListItem>422</asp:ListItem>
                                                                <asp:ListItem>423</asp:ListItem>
                                                                <asp:ListItem>424</asp:ListItem>
                                                                <asp:ListItem>425</asp:ListItem>
                                                                <asp:ListItem>426</asp:ListItem>
                                                                <asp:ListItem>427</asp:ListItem>
                                                                <asp:ListItem>428</asp:ListItem>
                                                                <asp:ListItem>429</asp:ListItem>
                                                                <asp:ListItem>430</asp:ListItem>
                                                                <asp:ListItem>431</asp:ListItem>
                                                                <asp:ListItem>432</asp:ListItem>
                                                                <asp:ListItem>433</asp:ListItem>
                                                                <asp:ListItem>434</asp:ListItem>
                                                                <asp:ListItem>435</asp:ListItem>
                                                                <asp:ListItem>436</asp:ListItem>
                                                                <asp:ListItem>437</asp:ListItem>
                                                                <asp:ListItem>438</asp:ListItem>
                                                                <asp:ListItem>439</asp:ListItem>
                                                                <asp:ListItem>440</asp:ListItem>
                                                                <asp:ListItem>441</asp:ListItem>
                                                                <asp:ListItem>442</asp:ListItem>
                                                                <asp:ListItem>443</asp:ListItem>
                                                                <asp:ListItem>444</asp:ListItem>
                                                                <asp:ListItem>445</asp:ListItem>
                                                                <asp:ListItem>446</asp:ListItem>
                                                                <asp:ListItem>447</asp:ListItem>
                                                                <asp:ListItem>448</asp:ListItem>
                                                                <asp:ListItem>449</asp:ListItem>
                                                                <asp:ListItem>450</asp:ListItem>
                                                                <asp:ListItem>451</asp:ListItem>
                                                                <asp:ListItem>452</asp:ListItem>
                                                                <asp:ListItem>453</asp:ListItem>
                                                                <asp:ListItem>454</asp:ListItem>
                                                                <asp:ListItem>455</asp:ListItem>
                                                                <asp:ListItem>456</asp:ListItem>
                                                                <asp:ListItem>457</asp:ListItem>
                                                                <asp:ListItem>458</asp:ListItem>
                                                                <asp:ListItem>459</asp:ListItem>
                                                                <asp:ListItem>460</asp:ListItem>
                                                                <asp:ListItem>461</asp:ListItem>
                                                                <asp:ListItem>462</asp:ListItem>
                                                                <asp:ListItem>463</asp:ListItem>
                                                                <asp:ListItem>464</asp:ListItem>
                                                                <asp:ListItem>465</asp:ListItem>
                                                                <asp:ListItem>466</asp:ListItem>
                                                                <asp:ListItem>467</asp:ListItem>
                                                                <asp:ListItem>468</asp:ListItem>
                                                                <asp:ListItem>469</asp:ListItem>
                                                                <asp:ListItem>470</asp:ListItem>
                                                                <asp:ListItem>471</asp:ListItem>
                                                                <asp:ListItem>472</asp:ListItem>
                                                                <asp:ListItem>473</asp:ListItem>
                                                                <asp:ListItem>474</asp:ListItem>
                                                                <asp:ListItem>475</asp:ListItem>
                                                                <asp:ListItem>476</asp:ListItem>
                                                                <asp:ListItem>477</asp:ListItem>
                                                                <asp:ListItem>478</asp:ListItem>
                                                                <asp:ListItem>479</asp:ListItem>
                                                                <asp:ListItem>480</asp:ListItem>
                                                                <asp:ListItem>481</asp:ListItem>
                                                                <asp:ListItem>482</asp:ListItem>
                                                                <asp:ListItem>483</asp:ListItem>
                                                                <asp:ListItem>484</asp:ListItem>
                                                                <asp:ListItem>485</asp:ListItem>
                                                                <asp:ListItem>486</asp:ListItem>
                                                                <asp:ListItem>487</asp:ListItem>
                                                                <asp:ListItem>488</asp:ListItem>
                                                                <asp:ListItem>489</asp:ListItem>
                                                                <asp:ListItem>490</asp:ListItem>
                                                                <asp:ListItem>491</asp:ListItem>
                                                                <asp:ListItem>492</asp:ListItem>
                                                                <asp:ListItem>493</asp:ListItem>
                                                                <asp:ListItem>494</asp:ListItem>
                                                                <asp:ListItem>495</asp:ListItem>
                                                                <asp:ListItem>496</asp:ListItem>
                                                                <asp:ListItem>497</asp:ListItem>
                                                                <asp:ListItem>498</asp:ListItem>
                                                                <asp:ListItem>499</asp:ListItem>
                                                                <asp:ListItem>500</asp:ListItem>
                                                            </asp:DropDownList></td>
                                                        <td valign="top">
                                                            <asp:TextBox ID="txtAddSubtotal" runat="server" MaxLength=6 Width=50 Enabled=false></asp:TextBox></td>
                                                        <td valign="top">
                                                            <asp:DropDownList ID="lstRepNameAdd" runat="server">
                                                            </asp:DropDownList></td>
                                                        <td valign="top">
                                                            <asp:DropDownList ID="lstPayment" runat="server">
                                                            <asp:ListItem>ACH Merchant</asp:ListItem>   
                                                            <asp:ListItem>Due from Vendor</asp:ListItem> 
                                                                <asp:ListItem Selected=True>Invoice Merchant</asp:ListItem>
                                                                 
                                                                
                                                                                                                            
                                                                </asp:DropDownList></td>
                                                        <td valign="top">
                                                            <asp:DropDownList ID="lstAddReprogram" runat="server" AutoPostBack="True"/></td>
                                                            <td valign="top">
                                                            <asp:TextBox ID="txtTerminalID" runat="server" MaxLength="7" Width="50"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="8" align="center">
                                                            <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="Add" />
                                                            &nbsp;
                                                            <asp:Button ID="btnCancelAdd" runat="server" CausesValidation="False" OnClick="btnCancelAdd_Click"
                                                                Text="Cancel" UseSubmitBehavior="False" /></td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlEditSalesOpp" runat="server" Width="100%" Visible="False">
                                                <asp:Label ID="lblEditSalesOpp" runat="server" Font-Bold="True" Text="Edit Sales Opportunity"
                                                    Font-Names="Arial" Font-Size="Small"></asp:Label>
                                                <table class="BlueBorder" border="0" style="width:100%">
                                                    <tr>
                                                        <td class="DivBlue">
                                                            <b><span class="MenuHeader">ID</span></b>
                                                        </td>
                                                        <td class="DivBlue">
                                                            <b><span class="MenuHeader">Product Name</span></b>
                                                        </td>
                                                        <td class="DivBlue">
                                                            <b><span class="MenuHeader">Sell Price</span></b>
                                                        </td>
                                                        <td class="DivBlue">
                                                            <b><span class="MenuHeader">COG</span></b>
                                                        </td>
                                                        <td class="DivBlue">
                                                            <b><span class="MenuHeader">Qty</span></b>
                                                        </td>
                                                        <td class="DivBlue">
                                                            <b><span class="MenuHeader">Sub Total</span></b>
                                                        </td>
                                                        <td class="DivBlue">
                                                            <b><span class="MenuHeader">Sales Rep</span></b></td>
                                                        <td class="DivBlue">
                                                            <b><span class="MenuHeader">Payment Method</span></b></td>
                                                        <td class="DivBlue">
                                                            <b><span class="MenuHeader">Reprogram</span></b></td>
                                                            <td class="DivBlue">
                                                            <b><span class="MenuHeader">Terminal ID</span></b></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblID" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="Small" /></td>
                                                        <td>
                                                            <asp:DropDownList ID="listEditProductName" runat="server" Font-Bold="True" AutoPostBack="True" Font-Names="Arial" Font-Size="Small" OnSelectedIndexChanged="lst_ProductNameChanged" /></td>
                                                        <td>
                                                            <asp:TextBox ID="txtSellPrice" runat="server" MaxLength="7" Width="50"/></td>
                                                        <td>
                                                            <asp:TextBox ID="lblCOG" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="Small" /></td>
                                                        <td>
                                                            <asp:DropDownList ID="lstQuantity" runat="server" AutoPostBack="True">
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
                                                                <asp:ListItem>12</asp:ListItem>
                                                                <asp:ListItem>13</asp:ListItem>
                                                                <asp:ListItem>14</asp:ListItem>
                                                                <asp:ListItem>15</asp:ListItem>
                                                                <asp:ListItem>16</asp:ListItem>
                                                                <asp:ListItem>17</asp:ListItem>
                                                                <asp:ListItem>18</asp:ListItem>
                                                                <asp:ListItem>19</asp:ListItem>
                                                                <asp:ListItem>20</asp:ListItem>
                                                                <asp:ListItem>21</asp:ListItem>
                                                                <asp:ListItem>22</asp:ListItem>
                                                                <asp:ListItem>23</asp:ListItem>
                                                                <asp:ListItem>24</asp:ListItem>
                                                                <asp:ListItem>25</asp:ListItem>
                                                                <asp:ListItem>26</asp:ListItem>
                                                                <asp:ListItem>27</asp:ListItem>
                                                                <asp:ListItem>28</asp:ListItem>
                                                                <asp:ListItem>29</asp:ListItem>
                                                                <asp:ListItem>30</asp:ListItem>
                                                                <asp:ListItem>31</asp:ListItem>
                                                                <asp:ListItem>32</asp:ListItem>
                                                                <asp:ListItem>33</asp:ListItem>
                                                                <asp:ListItem>34</asp:ListItem>
                                                                <asp:ListItem>35</asp:ListItem>
                                                                <asp:ListItem>36</asp:ListItem>
                                                                <asp:ListItem>37</asp:ListItem>
                                                                <asp:ListItem>38</asp:ListItem>
                                                                <asp:ListItem>39</asp:ListItem>
                                                                <asp:ListItem>40</asp:ListItem>
                                                                <asp:ListItem>41</asp:ListItem>
                                                                <asp:ListItem>42</asp:ListItem>
                                                                <asp:ListItem>43</asp:ListItem>
                                                                <asp:ListItem>44</asp:ListItem>
                                                                <asp:ListItem>45</asp:ListItem>
                                                                <asp:ListItem>46</asp:ListItem>
                                                                <asp:ListItem>47</asp:ListItem>
                                                                <asp:ListItem>48</asp:ListItem>
                                                                <asp:ListItem>49</asp:ListItem>
                                                                <asp:ListItem>50</asp:ListItem>
                                                                <asp:ListItem>51</asp:ListItem>
                                                                <asp:ListItem>52</asp:ListItem>
                                                                <asp:ListItem>53</asp:ListItem>
                                                                <asp:ListItem>54</asp:ListItem>
                                                                <asp:ListItem>55</asp:ListItem>
                                                                <asp:ListItem>56</asp:ListItem>
                                                                <asp:ListItem>57</asp:ListItem>
                                                                <asp:ListItem>58</asp:ListItem>
                                                                <asp:ListItem>59</asp:ListItem>
                                                                <asp:ListItem>60</asp:ListItem>
                                                                <asp:ListItem>61</asp:ListItem>
                                                                <asp:ListItem>62</asp:ListItem>
                                                                <asp:ListItem>63</asp:ListItem>
                                                                <asp:ListItem>64</asp:ListItem>
                                                                <asp:ListItem>65</asp:ListItem>
                                                                <asp:ListItem>66</asp:ListItem>
                                                                <asp:ListItem>67</asp:ListItem>
                                                                <asp:ListItem>68</asp:ListItem>
                                                                <asp:ListItem>69</asp:ListItem>
                                                                <asp:ListItem>70</asp:ListItem>
                                                                <asp:ListItem>71</asp:ListItem>
                                                                <asp:ListItem>72</asp:ListItem>
                                                                <asp:ListItem>73</asp:ListItem>
                                                                <asp:ListItem>74</asp:ListItem>
                                                                <asp:ListItem>75</asp:ListItem>
                                                                <asp:ListItem>76</asp:ListItem>
                                                                <asp:ListItem>77</asp:ListItem>
                                                                <asp:ListItem>78</asp:ListItem>
                                                                <asp:ListItem>79</asp:ListItem>
                                                                <asp:ListItem>80</asp:ListItem>
                                                                <asp:ListItem>81</asp:ListItem>
                                                                <asp:ListItem>82</asp:ListItem>
                                                                <asp:ListItem>83</asp:ListItem>
                                                                <asp:ListItem>84</asp:ListItem>
                                                                <asp:ListItem>85</asp:ListItem>
                                                                <asp:ListItem>86</asp:ListItem>
                                                                <asp:ListItem>87</asp:ListItem>
                                                                <asp:ListItem>88</asp:ListItem>
                                                                <asp:ListItem>89</asp:ListItem>
                                                                <asp:ListItem>90</asp:ListItem>
                                                                <asp:ListItem>91</asp:ListItem>
                                                                <asp:ListItem>92</asp:ListItem>
                                                                <asp:ListItem>93</asp:ListItem>
                                                                <asp:ListItem>94</asp:ListItem>
                                                                <asp:ListItem>95</asp:ListItem>
                                                                <asp:ListItem>96</asp:ListItem>
                                                                <asp:ListItem>97</asp:ListItem>
                                                                <asp:ListItem>98</asp:ListItem>
                                                                <asp:ListItem>99</asp:ListItem>
                                                                <asp:ListItem>100</asp:ListItem>
                                                                <asp:ListItem>101</asp:ListItem>
                                                                <asp:ListItem>102</asp:ListItem>
                                                                <asp:ListItem>103</asp:ListItem>
                                                                <asp:ListItem>104</asp:ListItem>
                                                                <asp:ListItem>105</asp:ListItem>
                                                                <asp:ListItem>106</asp:ListItem>
                                                                <asp:ListItem>107</asp:ListItem>
                                                                <asp:ListItem>108</asp:ListItem>
                                                                <asp:ListItem>109</asp:ListItem>
                                                                <asp:ListItem>110</asp:ListItem>
                                                                <asp:ListItem>111</asp:ListItem>
                                                                <asp:ListItem>112</asp:ListItem>
                                                                <asp:ListItem>113</asp:ListItem>
                                                                <asp:ListItem>114</asp:ListItem>
                                                                <asp:ListItem>115</asp:ListItem>
                                                                <asp:ListItem>116</asp:ListItem>
                                                                <asp:ListItem>117</asp:ListItem>
                                                                <asp:ListItem>118</asp:ListItem>
                                                                <asp:ListItem>119</asp:ListItem>
                                                                <asp:ListItem>120</asp:ListItem>
                                                                <asp:ListItem>121</asp:ListItem>
                                                                <asp:ListItem>122</asp:ListItem>
                                                                <asp:ListItem>123</asp:ListItem>
                                                                <asp:ListItem>124</asp:ListItem>
                                                                <asp:ListItem>125</asp:ListItem>
                                                                <asp:ListItem>126</asp:ListItem>
                                                                <asp:ListItem>127</asp:ListItem>
                                                                <asp:ListItem>128</asp:ListItem>
                                                                <asp:ListItem>129</asp:ListItem>
                                                                <asp:ListItem>130</asp:ListItem>
                                                                <asp:ListItem>131</asp:ListItem>
                                                                <asp:ListItem>132</asp:ListItem>
                                                                <asp:ListItem>133</asp:ListItem>
                                                                <asp:ListItem>134</asp:ListItem>
                                                                <asp:ListItem>135</asp:ListItem>
                                                                <asp:ListItem>136</asp:ListItem>
                                                                <asp:ListItem>137</asp:ListItem>
                                                                <asp:ListItem>138</asp:ListItem>
                                                                <asp:ListItem>139</asp:ListItem>
                                                                <asp:ListItem>140</asp:ListItem>
                                                                <asp:ListItem>141</asp:ListItem>
                                                                <asp:ListItem>142</asp:ListItem>
                                                                <asp:ListItem>143</asp:ListItem>
                                                                <asp:ListItem>144</asp:ListItem>
                                                                <asp:ListItem>145</asp:ListItem>
                                                                <asp:ListItem>146</asp:ListItem>
                                                                <asp:ListItem>147</asp:ListItem>
                                                                <asp:ListItem>148</asp:ListItem>
                                                                <asp:ListItem>149</asp:ListItem>
                                                                <asp:ListItem>150</asp:ListItem>
                                                                <asp:ListItem>151</asp:ListItem>
                                                                <asp:ListItem>152</asp:ListItem>
                                                                <asp:ListItem>153</asp:ListItem>
                                                                <asp:ListItem>154</asp:ListItem>
                                                                <asp:ListItem>155</asp:ListItem>
                                                                <asp:ListItem>156</asp:ListItem>
                                                                <asp:ListItem>157</asp:ListItem>
                                                                <asp:ListItem>158</asp:ListItem>
                                                                <asp:ListItem>159</asp:ListItem>
                                                                <asp:ListItem>160</asp:ListItem>
                                                                <asp:ListItem>161</asp:ListItem>
                                                                <asp:ListItem>162</asp:ListItem>
                                                                <asp:ListItem>163</asp:ListItem>
                                                                <asp:ListItem>164</asp:ListItem>
                                                                <asp:ListItem>165</asp:ListItem>
                                                                <asp:ListItem>166</asp:ListItem>
                                                                <asp:ListItem>167</asp:ListItem>
                                                                <asp:ListItem>168</asp:ListItem>
                                                                <asp:ListItem>169</asp:ListItem>
                                                                <asp:ListItem>170</asp:ListItem>
                                                                <asp:ListItem>171</asp:ListItem>
                                                                <asp:ListItem>172</asp:ListItem>
                                                                <asp:ListItem>173</asp:ListItem>
                                                                <asp:ListItem>174</asp:ListItem>
                                                                <asp:ListItem>175</asp:ListItem>
                                                                <asp:ListItem>176</asp:ListItem>
                                                                <asp:ListItem>177</asp:ListItem>
                                                                <asp:ListItem>178</asp:ListItem>
                                                                <asp:ListItem>179</asp:ListItem>
                                                                <asp:ListItem>180</asp:ListItem>
                                                                <asp:ListItem>181</asp:ListItem>
                                                                <asp:ListItem>182</asp:ListItem>
                                                                <asp:ListItem>183</asp:ListItem>
                                                                <asp:ListItem>184</asp:ListItem>
                                                                <asp:ListItem>185</asp:ListItem>
                                                                <asp:ListItem>186</asp:ListItem>
                                                                <asp:ListItem>187</asp:ListItem>
                                                                <asp:ListItem>188</asp:ListItem>
                                                                <asp:ListItem>189</asp:ListItem>
                                                                <asp:ListItem>190</asp:ListItem>
                                                                <asp:ListItem>191</asp:ListItem>
                                                                <asp:ListItem>192</asp:ListItem>
                                                                <asp:ListItem>193</asp:ListItem>
                                                                <asp:ListItem>194</asp:ListItem>
                                                                <asp:ListItem>195</asp:ListItem>
                                                                <asp:ListItem>196</asp:ListItem>
                                                                <asp:ListItem>197</asp:ListItem>
                                                                <asp:ListItem>198</asp:ListItem>
                                                                <asp:ListItem>199</asp:ListItem>
                                                                <asp:ListItem>200</asp:ListItem>
                                                                <asp:ListItem>201</asp:ListItem>
                                                                <asp:ListItem>202</asp:ListItem>
                                                                <asp:ListItem>203</asp:ListItem>
                                                                <asp:ListItem>204</asp:ListItem>
                                                                <asp:ListItem>205</asp:ListItem>
                                                                <asp:ListItem>206</asp:ListItem>
                                                                <asp:ListItem>207</asp:ListItem>
                                                                <asp:ListItem>208</asp:ListItem>
                                                                <asp:ListItem>209</asp:ListItem>
                                                                <asp:ListItem>210</asp:ListItem>
                                                                <asp:ListItem>211</asp:ListItem>
                                                                <asp:ListItem>212</asp:ListItem>
                                                                <asp:ListItem>213</asp:ListItem>
                                                                <asp:ListItem>214</asp:ListItem>
                                                                <asp:ListItem>215</asp:ListItem>
                                                                <asp:ListItem>216</asp:ListItem>
                                                                <asp:ListItem>217</asp:ListItem>
                                                                <asp:ListItem>218</asp:ListItem>
                                                                <asp:ListItem>219</asp:ListItem>
                                                                <asp:ListItem>220</asp:ListItem>
                                                                <asp:ListItem>221</asp:ListItem>
                                                                <asp:ListItem>222</asp:ListItem>
                                                                <asp:ListItem>223</asp:ListItem>
                                                                <asp:ListItem>224</asp:ListItem>
                                                                <asp:ListItem>225</asp:ListItem>
                                                                <asp:ListItem>226</asp:ListItem>
                                                                <asp:ListItem>227</asp:ListItem>
                                                                <asp:ListItem>228</asp:ListItem>
                                                                <asp:ListItem>229</asp:ListItem>
                                                                <asp:ListItem>230</asp:ListItem>
                                                                <asp:ListItem>231</asp:ListItem>
                                                                <asp:ListItem>232</asp:ListItem>
                                                                <asp:ListItem>233</asp:ListItem>
                                                                <asp:ListItem>234</asp:ListItem>
                                                                <asp:ListItem>235</asp:ListItem>
                                                                <asp:ListItem>236</asp:ListItem>
                                                                <asp:ListItem>237</asp:ListItem>
                                                                <asp:ListItem>238</asp:ListItem>
                                                                <asp:ListItem>239</asp:ListItem>
                                                                <asp:ListItem>240</asp:ListItem>
                                                                <asp:ListItem>241</asp:ListItem>
                                                                <asp:ListItem>242</asp:ListItem>
                                                                <asp:ListItem>243</asp:ListItem>
                                                                <asp:ListItem>244</asp:ListItem>
                                                                <asp:ListItem>245</asp:ListItem>
                                                                <asp:ListItem>246</asp:ListItem>
                                                                <asp:ListItem>247</asp:ListItem>
                                                                <asp:ListItem>248</asp:ListItem>
                                                                <asp:ListItem>249</asp:ListItem>
                                                                <asp:ListItem>250</asp:ListItem>
                                                                <asp:ListItem>251</asp:ListItem>
                                                                <asp:ListItem>252</asp:ListItem>
                                                                <asp:ListItem>253</asp:ListItem>
                                                                <asp:ListItem>254</asp:ListItem>
                                                                <asp:ListItem>255</asp:ListItem>
                                                                <asp:ListItem>256</asp:ListItem>
                                                                <asp:ListItem>257</asp:ListItem>
                                                                <asp:ListItem>258</asp:ListItem>
                                                                <asp:ListItem>259</asp:ListItem>
                                                                <asp:ListItem>260</asp:ListItem>
                                                                <asp:ListItem>261</asp:ListItem>
                                                                <asp:ListItem>262</asp:ListItem>
                                                                <asp:ListItem>263</asp:ListItem>
                                                                <asp:ListItem>264</asp:ListItem>
                                                                <asp:ListItem>265</asp:ListItem>
                                                                <asp:ListItem>266</asp:ListItem>
                                                                <asp:ListItem>267</asp:ListItem>
                                                                <asp:ListItem>268</asp:ListItem>
                                                                <asp:ListItem>269</asp:ListItem>
                                                                <asp:ListItem>270</asp:ListItem>
                                                                <asp:ListItem>271</asp:ListItem>
                                                                <asp:ListItem>272</asp:ListItem>
                                                                <asp:ListItem>273</asp:ListItem>
                                                                <asp:ListItem>274</asp:ListItem>
                                                                <asp:ListItem>275</asp:ListItem>
                                                                <asp:ListItem>276</asp:ListItem>
                                                                <asp:ListItem>277</asp:ListItem>
                                                                <asp:ListItem>278</asp:ListItem>
                                                                <asp:ListItem>279</asp:ListItem>
                                                                <asp:ListItem>280</asp:ListItem>
                                                                <asp:ListItem>281</asp:ListItem>
                                                                <asp:ListItem>282</asp:ListItem>
                                                                <asp:ListItem>283</asp:ListItem>
                                                                <asp:ListItem>284</asp:ListItem>
                                                                <asp:ListItem>285</asp:ListItem>
                                                                <asp:ListItem>286</asp:ListItem>
                                                                <asp:ListItem>287</asp:ListItem>
                                                                <asp:ListItem>288</asp:ListItem>
                                                                <asp:ListItem>289</asp:ListItem>
                                                                <asp:ListItem>290</asp:ListItem>
                                                                <asp:ListItem>291</asp:ListItem>
                                                                <asp:ListItem>292</asp:ListItem>
                                                                <asp:ListItem>293</asp:ListItem>
                                                                <asp:ListItem>294</asp:ListItem>
                                                                <asp:ListItem>295</asp:ListItem>
                                                                <asp:ListItem>296</asp:ListItem>
                                                                <asp:ListItem>297</asp:ListItem>
                                                                <asp:ListItem>298</asp:ListItem>
                                                                <asp:ListItem>299</asp:ListItem>
                                                                <asp:ListItem>300</asp:ListItem>
                                                                <asp:ListItem>301</asp:ListItem>
                                                                <asp:ListItem>302</asp:ListItem>
                                                                <asp:ListItem>303</asp:ListItem>
                                                                <asp:ListItem>304</asp:ListItem>
                                                                <asp:ListItem>305</asp:ListItem>
                                                                <asp:ListItem>306</asp:ListItem>
                                                                <asp:ListItem>307</asp:ListItem>
                                                                <asp:ListItem>308</asp:ListItem>
                                                                <asp:ListItem>309</asp:ListItem>
                                                                <asp:ListItem>310</asp:ListItem>
                                                                <asp:ListItem>311</asp:ListItem>
                                                                <asp:ListItem>312</asp:ListItem>
                                                                <asp:ListItem>313</asp:ListItem>
                                                                <asp:ListItem>314</asp:ListItem>
                                                                <asp:ListItem>315</asp:ListItem>
                                                                <asp:ListItem>316</asp:ListItem>
                                                                <asp:ListItem>317</asp:ListItem>
                                                                <asp:ListItem>318</asp:ListItem>
                                                                <asp:ListItem>319</asp:ListItem>
                                                                <asp:ListItem>320</asp:ListItem>
                                                                <asp:ListItem>321</asp:ListItem>
                                                                <asp:ListItem>322</asp:ListItem>
                                                                <asp:ListItem>323</asp:ListItem>
                                                                <asp:ListItem>324</asp:ListItem>
                                                                <asp:ListItem>325</asp:ListItem>
                                                                <asp:ListItem>326</asp:ListItem>
                                                                <asp:ListItem>327</asp:ListItem>
                                                                <asp:ListItem>328</asp:ListItem>
                                                                <asp:ListItem>329</asp:ListItem>
                                                                <asp:ListItem>330</asp:ListItem>
                                                                <asp:ListItem>331</asp:ListItem>
                                                                <asp:ListItem>332</asp:ListItem>
                                                                <asp:ListItem>333</asp:ListItem>
                                                                <asp:ListItem>334</asp:ListItem>
                                                                <asp:ListItem>335</asp:ListItem>
                                                                <asp:ListItem>336</asp:ListItem>
                                                                <asp:ListItem>337</asp:ListItem>
                                                                <asp:ListItem>338</asp:ListItem>
                                                                <asp:ListItem>339</asp:ListItem>
                                                                <asp:ListItem>340</asp:ListItem>
                                                                <asp:ListItem>341</asp:ListItem>
                                                                <asp:ListItem>342</asp:ListItem>
                                                                <asp:ListItem>343</asp:ListItem>
                                                                <asp:ListItem>344</asp:ListItem>
                                                                <asp:ListItem>345</asp:ListItem>
                                                                <asp:ListItem>346</asp:ListItem>
                                                                <asp:ListItem>347</asp:ListItem>
                                                                <asp:ListItem>348</asp:ListItem>
                                                                <asp:ListItem>349</asp:ListItem>
                                                                <asp:ListItem>350</asp:ListItem>
                                                                <asp:ListItem>351</asp:ListItem>
                                                                <asp:ListItem>352</asp:ListItem>
                                                                <asp:ListItem>353</asp:ListItem>
                                                                <asp:ListItem>354</asp:ListItem>
                                                                <asp:ListItem>355</asp:ListItem>
                                                                <asp:ListItem>356</asp:ListItem>
                                                                <asp:ListItem>357</asp:ListItem>
                                                                <asp:ListItem>358</asp:ListItem>
                                                                <asp:ListItem>359</asp:ListItem>
                                                                <asp:ListItem>360</asp:ListItem>
                                                                <asp:ListItem>361</asp:ListItem>
                                                                <asp:ListItem>362</asp:ListItem>
                                                                <asp:ListItem>363</asp:ListItem>
                                                                <asp:ListItem>364</asp:ListItem>
                                                                <asp:ListItem>365</asp:ListItem>
                                                                <asp:ListItem>366</asp:ListItem>
                                                                <asp:ListItem>367</asp:ListItem>
                                                                <asp:ListItem>368</asp:ListItem>
                                                                <asp:ListItem>369</asp:ListItem>
                                                                <asp:ListItem>370</asp:ListItem>
                                                                <asp:ListItem>371</asp:ListItem>
                                                                <asp:ListItem>372</asp:ListItem>
                                                                <asp:ListItem>373</asp:ListItem>
                                                                <asp:ListItem>374</asp:ListItem>
                                                                <asp:ListItem>375</asp:ListItem>
                                                                <asp:ListItem>376</asp:ListItem>
                                                                <asp:ListItem>377</asp:ListItem>
                                                                <asp:ListItem>378</asp:ListItem>
                                                                <asp:ListItem>379</asp:ListItem>
                                                                <asp:ListItem>380</asp:ListItem>
                                                                <asp:ListItem>381</asp:ListItem>
                                                                <asp:ListItem>382</asp:ListItem>
                                                                <asp:ListItem>383</asp:ListItem>
                                                                <asp:ListItem>384</asp:ListItem>
                                                                <asp:ListItem>385</asp:ListItem>
                                                                <asp:ListItem>386</asp:ListItem>
                                                                <asp:ListItem>387</asp:ListItem>
                                                                <asp:ListItem>388</asp:ListItem>
                                                                <asp:ListItem>389</asp:ListItem>
                                                                <asp:ListItem>390</asp:ListItem>
                                                                <asp:ListItem>391</asp:ListItem>
                                                                <asp:ListItem>392</asp:ListItem>
                                                                <asp:ListItem>393</asp:ListItem>
                                                                <asp:ListItem>394</asp:ListItem>
                                                                <asp:ListItem>395</asp:ListItem>
                                                                <asp:ListItem>396</asp:ListItem>
                                                                <asp:ListItem>397</asp:ListItem>
                                                                <asp:ListItem>398</asp:ListItem>
                                                                <asp:ListItem>399</asp:ListItem>
                                                                <asp:ListItem>400</asp:ListItem>
                                                                <asp:ListItem>401</asp:ListItem>
                                                                <asp:ListItem>402</asp:ListItem>
                                                                <asp:ListItem>403</asp:ListItem>
                                                                <asp:ListItem>404</asp:ListItem>
                                                                <asp:ListItem>405</asp:ListItem>
                                                                <asp:ListItem>406</asp:ListItem>
                                                                <asp:ListItem>407</asp:ListItem>
                                                                <asp:ListItem>408</asp:ListItem>
                                                                <asp:ListItem>409</asp:ListItem>
                                                                <asp:ListItem>410</asp:ListItem>
                                                                <asp:ListItem>411</asp:ListItem>
                                                                <asp:ListItem>412</asp:ListItem>
                                                                <asp:ListItem>413</asp:ListItem>
                                                                <asp:ListItem>414</asp:ListItem>
                                                                <asp:ListItem>415</asp:ListItem>
                                                                <asp:ListItem>416</asp:ListItem>
                                                                <asp:ListItem>417</asp:ListItem>
                                                                <asp:ListItem>418</asp:ListItem>
                                                                <asp:ListItem>419</asp:ListItem>
                                                                <asp:ListItem>420</asp:ListItem>
                                                                <asp:ListItem>421</asp:ListItem>
                                                                <asp:ListItem>422</asp:ListItem>
                                                                <asp:ListItem>423</asp:ListItem>
                                                                <asp:ListItem>424</asp:ListItem>
                                                                <asp:ListItem>425</asp:ListItem>
                                                                <asp:ListItem>426</asp:ListItem>
                                                                <asp:ListItem>427</asp:ListItem>
                                                                <asp:ListItem>428</asp:ListItem>
                                                                <asp:ListItem>429</asp:ListItem>
                                                                <asp:ListItem>430</asp:ListItem>
                                                                <asp:ListItem>431</asp:ListItem>
                                                                <asp:ListItem>432</asp:ListItem>
                                                                <asp:ListItem>433</asp:ListItem>
                                                                <asp:ListItem>434</asp:ListItem>
                                                                <asp:ListItem>435</asp:ListItem>
                                                                <asp:ListItem>436</asp:ListItem>
                                                                <asp:ListItem>437</asp:ListItem>
                                                                <asp:ListItem>438</asp:ListItem>
                                                                <asp:ListItem>439</asp:ListItem>
                                                                <asp:ListItem>440</asp:ListItem>
                                                                <asp:ListItem>441</asp:ListItem>
                                                                <asp:ListItem>442</asp:ListItem>
                                                                <asp:ListItem>443</asp:ListItem>
                                                                <asp:ListItem>444</asp:ListItem>
                                                                <asp:ListItem>445</asp:ListItem>
                                                                <asp:ListItem>446</asp:ListItem>
                                                                <asp:ListItem>447</asp:ListItem>
                                                                <asp:ListItem>448</asp:ListItem>
                                                                <asp:ListItem>449</asp:ListItem>
                                                                <asp:ListItem>450</asp:ListItem>
                                                                <asp:ListItem>451</asp:ListItem>
                                                                <asp:ListItem>452</asp:ListItem>
                                                                <asp:ListItem>453</asp:ListItem>
                                                                <asp:ListItem>454</asp:ListItem>
                                                                <asp:ListItem>455</asp:ListItem>
                                                                <asp:ListItem>456</asp:ListItem>
                                                                <asp:ListItem>457</asp:ListItem>
                                                                <asp:ListItem>458</asp:ListItem>
                                                                <asp:ListItem>459</asp:ListItem>
                                                                <asp:ListItem>460</asp:ListItem>
                                                                <asp:ListItem>461</asp:ListItem>
                                                                <asp:ListItem>462</asp:ListItem>
                                                                <asp:ListItem>463</asp:ListItem>
                                                                <asp:ListItem>464</asp:ListItem>
                                                                <asp:ListItem>465</asp:ListItem>
                                                                <asp:ListItem>466</asp:ListItem>
                                                                <asp:ListItem>467</asp:ListItem>
                                                                <asp:ListItem>468</asp:ListItem>
                                                                <asp:ListItem>469</asp:ListItem>
                                                                <asp:ListItem>470</asp:ListItem>
                                                                <asp:ListItem>471</asp:ListItem>
                                                                <asp:ListItem>472</asp:ListItem>
                                                                <asp:ListItem>473</asp:ListItem>
                                                                <asp:ListItem>474</asp:ListItem>
                                                                <asp:ListItem>475</asp:ListItem>
                                                                <asp:ListItem>476</asp:ListItem>
                                                                <asp:ListItem>477</asp:ListItem>
                                                                <asp:ListItem>478</asp:ListItem>
                                                                <asp:ListItem>479</asp:ListItem>
                                                                <asp:ListItem>480</asp:ListItem>
                                                                <asp:ListItem>481</asp:ListItem>
                                                                <asp:ListItem>482</asp:ListItem>
                                                                <asp:ListItem>483</asp:ListItem>
                                                                <asp:ListItem>484</asp:ListItem>
                                                                <asp:ListItem>485</asp:ListItem>
                                                                <asp:ListItem>486</asp:ListItem>
                                                                <asp:ListItem>487</asp:ListItem>
                                                                <asp:ListItem>488</asp:ListItem>
                                                                <asp:ListItem>489</asp:ListItem>
                                                                <asp:ListItem>490</asp:ListItem>
                                                                <asp:ListItem>491</asp:ListItem>
                                                                <asp:ListItem>492</asp:ListItem>
                                                                <asp:ListItem>493</asp:ListItem>
                                                                <asp:ListItem>494</asp:ListItem>
                                                                <asp:ListItem>495</asp:ListItem>
                                                                <asp:ListItem>496</asp:ListItem>
                                                                <asp:ListItem>497</asp:ListItem>
                                                                <asp:ListItem>498</asp:ListItem>
                                                                <asp:ListItem>499</asp:ListItem>
                                                                <asp:ListItem>500</asp:ListItem>
                                                            </asp:DropDownList></td>
                                                        <td>
                                                            <asp:Label ID="lblSubtotal" runat="server" Width="50" Font-Bold="True" Font-Names="Arial" Font-Size="Small" /></td>
                                                        <td>
                                                            <asp:DropDownList ID="lstRepName" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="Small" /></td>
                                                        <td>
                                                            <asp:DropDownList ID="lstEditPaymentMethod" runat="server">
                                                                <asp:ListItem></asp:ListItem>
                                                                <asp:ListItem>Invoice Merchant</asp:ListItem>
                                                                <asp:ListItem>ACH Merchant</asp:ListItem> 
                                                                <asp:ListItem>Lease Merchant</asp:ListItem>                                                                 
                                                            </asp:DropDownList></td>
                                                        <td>
                                                            <asp:DropDownList ID="lstReprogram" runat="server" AutoPostBack="True" /></td>
                                                            <td>
                                                            <asp:TextBox ID="txtTerminalIDEdit" runat="server" AutoPostBack="True" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="9" align="center">
                                                            <asp:Button ID="btnEditSubmit" runat="server" OnClick="btnEditSubmit_Click" Text="Submit" />
                                                            &nbsp;
                                                            <asp:Button ID="btnEditCancel" runat="server" CausesValidation="False" OnClick="btnEditCancel_Click"
                                                                Text="Cancel" UseSubmitBehavior="False" /></td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                            <br />
                                            <asp:GridView ID="grdSalesOpps" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                ForeColor="#333333" GridLines="Vertical" OnRowCommand="grdSalesOpps_RowCommand"
                                                OnRowDataBound="grdSalesOpps_RowDataBound" OnRowDeleting="grdSalesOpps_RowDeleting">
                                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                <Columns>
                                                    <asp:BoundField DataField="ID" HeaderText="ID"></asp:BoundField>
                                                    <asp:BoundField DataField="Product" HeaderText="Product Name"></asp:BoundField>
                                                    <asp:BoundField DataField="Price" HeaderText="Price"></asp:BoundField>
                                                    <asp:BoundField DataField="CostOfGoods" HeaderText="Cost Of Goods" />
                                                    <asp:BoundField DataField="Quantity" HeaderText="Quantity"></asp:BoundField>
                                                    <asp:BoundField DataField="Subtotal" HeaderText="Subtotal"></asp:BoundField>
                                                    <asp:BoundField DataField="Status" HeaderText="Status"></asp:BoundField>
                                                    <asp:BoundField DataField="Stage" HeaderText="Stage" />
                                                    <asp:BoundField DataField="RepName" HeaderText="RepName" />
                                                    <asp:BoundField DataField="PaymentMethod" HeaderText="Payment Method" />
                                                    <asp:BoundField DataField="Reprogram" HeaderText="Reprogram" />
                                                    <asp:BoundField DataField="Linked" HeaderText="Linked" />
                                                    <asp:BoundField DataField="IsAddedAct" HeaderText="Added/ Updated to Act" />
                                                    <asp:CommandField CausesValidation="False" ShowDeleteButton="True" />
                                                    <asp:ButtonField CommandName="RowEdit" Text="Edit">
                                                        <ItemStyle Font-Bold="True" Font-Names="Arial" Font-Size="X-Small" />
                                                    </asp:ButtonField> 
                                                    <asp:ButtonField CommandName="AddToACT" Text="Add/Update to ACT!">
                                                        <ItemStyle Font-Bold="True" Font-Names="Arial" Font-Size="X-Small" />
                                                    </asp:ButtonField>   
                                                </Columns>
                                                <RowStyle BackColor="#EDF7FF" Font-Names="Arial" Font-Size="X-Small" ForeColor="#333333" />
                                                <EditRowStyle BackColor="#999999" />
                                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                <HeaderStyle CssClass="MenuHeader" BackColor="#5D7B9D" />
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                            </asp:GridView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </ContentTemplate>                                
                            </cc1:TabPanel>
                            <cc1:TabPanel ID="TabRates" runat="server" HeaderText="Services">
                                <ContentTemplate>
                                    <div align="center">
                                        <asp:HyperLink ID="lnkModifyRates" CssClass="One" Font-Bold="true" Font-Names="Arial"
                                            Font-Size="9" runat="server">Click here to Modify</asp:HyperLink>
                                    </div>
                                    <table width="60%" cellspacing="0" cellpadding="0" border="0">
                                        <tr>
                                            <td align="center">
                                                <asp:Panel ID="pnlMerchant" runat="server" Width="100%">
                                                    <table border="0" cellpadding="0" cellspacing="5" style="width: 100%;" class="DivGreen">
                                                        <tr>
                                                            <td style="width: 35%; height: 15px" align="left">
                                                                <asp:Label Font-Bold="true" Font-Size="9" ID="lblMerchantAcc" runat="server" Text="Merchant Account"></asp:Label>
                                                            </td>
                                                            <td style="width: 35%; height: 15px" align="left">
                                    
                                                                <asp:Label ID="lblProcessorText" runat="server" Font-Bold="true"></asp:Label>
                                                            </td>
                                                            <td align="right" valgin="middle" style="height: 15px">
                                                            <asp:Panel ID="pnlPDF" runat="server" >
                                    <ContentTemplate>
                                        <div style="width: 100%; align:right; vertical-align:middle;">
                                            <!--<div style="float:left; ">
                                                <b><span class="LabelsSmall">
                                                    <asp:Label ID="lblProcessorPDF" runat="server" Font-Bold="True" Text=""></asp:Label></span></b>
                                                
                                            </div>-->
                                            <div style="float:right;">
                                            <asp:ImageButton CssClass="MenuLink" ID="imgCreatePDF" runat="server" CausesValidation="false" ImageUrl="~/Images/CreateIMSPDF.gif"
                                                OnClientClick="form1.target ='_blank';" OnClick="imgCreatePDF_Click" /><cc1:PopupControlExtender ID="PopupControlExtender1" runat="server" PopupControlID="pnlPDFNote"
                                                    TargetControlID="imgPDFHelp" Position="Bottom" />
                                                <asp:Image ID="imgPDFHelp" runat="server" ImageUrl="~/Images/help.gif"
                                                    Style="cursor: pointer" /><br/>
                                                    <!--<asp:HyperLink ID="lnk1" Visible="False" Font-Names="Arial" CssClass="Link" runat="server" NavigateUrl="~/PDF/Sage Merchant Agreement.pdf" Target="_blank">Terms and Conditions</asp:HyperLink>-->
                                                 <asp:HyperLink ID="lnkSageAgreement1" Visible="False" Font-Names="Arial" CssClass="Link" runat="server" NavigateUrl="http://www.sage.com/us/sage-payment-solutions/terms" Target="_blank">Terms and Conditions</asp:HyperLink>
                                                 <asp:LinkButton ID="lnkAmendment1" Visible="False" Font-Names="Arial"  CssClass="Link" runat="server" color= "#026d6f" OnClick="lnkAmendment_Click" >Amendment</asp:LinkButton>
                                            <br/>



                                            </div>
                                        </div>
                                    </ContentTemplate>
                                    </asp:Panel>
                                    
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 35%; height: 15px" align="left"></asp:Label>
                                                            </td>
                                                            <td style="width: 35%; height: 15px" align="left">
                                   <asp:Panel ID="pnlEnvStatus" Width="100%" runat="server">
                                    <center>
                                    <asp:Label Font-Bold="true" Font-Size="9" ID="lblEnvStatus" runat="server" ></asp:Label>
                                        </center>
                                      
                                    </asp:Panel>
                                                            </td>
                                                            <td align="right" valgin="middle" style="height: 15px">
                                                                <b><span class="LabelsSmall">
                                                                <asp:Panel ID="pnlDelDocuSignEnv" Width="100%" runat="server">
                                           <center>
                                            <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:LinkButton ID="lnkDelDocuSignEnv" Font-Bold="true" Font-Names="Arial" Font-Size="9"
                                                                runat="server" Text="Delete Envelope" OnClick="lnkDelDocuSignEnv_Click" CssClass="One" /><br/>
                                                    <asp:Panel ID="pnlDelDocuEnvConfirm" runat="server" BackColor="#FFC0C0" BorderColor="Salmon"
                                                        BorderStyle="Double" Visible="False">
                                                        <asp:Image ID="imgExclamationDelDocEnv" runat="server" ImageUrl="~/Images/exclamation.gif" />
                                                        <asp:Label ID="lblDeleteMsgDocEnv" runat="server" Font-Bold="True" Font-Size="Medium" Text="">Confirm Delete?</asp:Label><br />
                                                        <asp:Button ID="btnDeleteDocEnvYes" runat="server" OnClick="btnDelDocuSignEnvYes_Click" Text="Yes" />
                                                        <asp:Button ID="btnDeleteDocEnvNo" runat="server" OnClick="btnDelDocuSignEnvNo_Click" Text="No" /></asp:Panel>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="lnkDelDocuSignEnv" EventName="Click" />
                                                    <asp:PostBackTrigger ControlID="btnDeleteDocEnvYes" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </center>
                                      
                                    </asp:Panel></span></b></td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" colspan="2">
                                                                <asp:Label ID="lblAppFee" runat="server" Text="Application Fee"></asp:Label></td>
                                                            <td align="left">
                                                               <asp:Label ID="lblApplicationFee" runat="server"> <b><span class="LabelsSmall">$ </span></b></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" colspan="2">
                                                                <asp:Label ID="lblAppSetupFee" runat="server" Text="Setup Fee"></asp:Label></td>
                                                            <td align="left">
                                                               <asp:Label ID="lblApplicationSetupFee" runat="server"> <b><span class="LabelsSmall">$ </span></b></asp:Label></td>
                                                        </tr>
                                                         <tr>
                                                            <td align="left" colspan="2">
                                                                <asp:Label ID="labelDebitRate" Text="Visa/MC Debit Rate" runat="server"></asp:Label></td>
                                                            <td align="left">
                                                                <b>
                                                                    <asp:Label ID="lblDebitRate" runat="server"></asp:Label><span class="LabelsSmall"> %</span>
                                                                    </b></td>
                                                        </tr>  
                                                        <tr>
                                                            <td align="left" colspan="2">
                                                                <asp:Label ID="lblDiscRate" runat="server" Text="Visa/MC Discount Rate"></asp:Label></td>
                                                            <td align="left">
                                                                <b>
                                                                    <asp:Label ID="lblDiscountRate" runat="server"></asp:Label><span class="LabelsSmall"> %</span></b></td>
                                                        </tr>
                                                       
                                                        <tr>
                                                            <td align="left" colspan="2">
                                                                <asp:Label ID="lblPerAuthorization" runat="server" Text="Per Authorization - All Card Types"></asp:Label></td>
                                                            <td align="left">
                                                                <b><span class="LabelsSmall">$ </span><asp:Label ID="lblPerAuth" runat="server"></asp:Label></b></td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" colspan="2">
                                                                <asp:Label ID="lblTollFree" runat="server" Text="Customer Service Fee"></asp:Label></td>
                                                            <td align="left">
                                                                <b><span class="LabelsSmall">$ </span><asp:Label ID="lblTollFreeService" runat="server"></asp:Label></b></td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" colspan="2">
                                                                <asp:Label ID="lblMonthlyMin" runat="server" Text="Monthly Minimum"></asp:Label></td>
                                                            <td align="left">
                                                                <b><span class="LabelsSmall">$ </span><asp:Label ID="lblMonMin" runat="server"></asp:Label></b></td>
                                                        </tr>
                                                        
                                                    </table>
                                                </asp:Panel>
                                                <asp:Panel ID="pnlMerchantInfo" runat="server" Width="100%">
                                                    <table cellpadding="0" cellspacing="5" border="0" style="width: 100%;" class="DivGreen">
                                                        <tr>
                                                            <td align="left" style="width: 25%">
                                                                <span class="LabelsSmall"></span></td>
                                                            <td align="left" style="width: 25%">
                                                                <span class="LabelsSmall"></span></td>
                                                            
                                                            <td align="right" style="width: 25%">
                                                                <span class="LabelsSmall">Discover</span></td>
                                                            <td align="left" style="width: 25%">
                                                                <asp:Label ID="lblDiscoverText" runat="server" Font-Bold="true"></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" style="width: 25%">
                                                                <span class="LabelsSmall">Visa/Mastercard</span></td>
                                                            <td align="left" style="width: 25%">
                                                                <asp:Label ID="lblVisaMasterNumberText" runat="server" Font-Bold="true"></asp:Label></td>
                                                            <td align="right" style="width: 25%">
                                                                <span class="LabelsSmall">Amex</span></td>
                                                            <td align="left" style="width: 25%">
                                                                <asp:Label ID="lblAmexText" runat="server" Font-Bold="true"></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                        <td align="left">
                                                                <asp:DropDownList ID="lstMerchantStatus" runat="server" TabIndex="3" Width="170">
                                                                </asp:DropDownList></td>
                                                            <td align="left">
                                                                <span class="LabelsSmall"></span></td>
                                                            
                                                            <td align="right">
                                                                <span class="LabelsSmall">JCB</span></td>
                                                            <td align="left">
                                                                <asp:Label ID="lblJCBText" runat="server" Font-Bold="true"></asp:Label></td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>

                                                <!--<asp:Panel ID="pnlReprogram" runat="server" Width="100%" Visible="false">
                                                    <table cellpadding="0" cellspacing="5" border="0" style="width: 100%;" class="DivGreen">
                                                        <tr>
                                                            <td align="left" style="width: 25%">
                                                                <span class="LabelsSmall">Platform</span></td>
                                                            <td align="left" style="width: 25%">
                                                                <asp:Label ID="lblRPlatformACT" runat="server" Font-Bold="true"></asp:Label></td>
                                                            <td align="right" style="width: 25%">
                                                                <span class="LabelsSmall">Login ID</span></td>
                                                            <td align="left" style="width: 25%">
                                                                <asp:Label ID="lblRLoginIDText" runat="server" Font-Bold="true"></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <span class="LabelsSmall">Merchant ID</span></td>
                                                            <td align="left">
                                                                <asp:Label ID="lblRMerchantIDText" runat="server" Font-Bold="true"></asp:Label></td>
                                                            <td align="right">
                                                                <span class="LabelsSmall">Terminal ID</span></td>
                                                            <td align="left">
                                                                <asp:Label ID="lblRTerminalIDText" runat="server" Font-Bold="true"></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <span class="LabelsSmall">Bank ID Number (BIN)</span></td>
                                                            <td align="left">
                                                                <asp:Label ID="lblRBINNumberText" runat="server" Font-Bold="true"></asp:Label></td>
                                                            <td align="right">
                                                                <span class="LabelsSmall">Agent Chain Number</span></td>
                                                            <td align="left">
                                                                <asp:Label ID="lblRAgentChainNumberText" runat="server" Font-Bold="true"></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <span class="LabelsSmall">Agent Bank Number</span></td>
                                                            <td align="left">
                                                                <asp:Label ID="lblRAgentBankNumberText" runat="server" Font-Bold="true"></asp:Label></td>
                                                            <td align="right">
                                                                <span class="LabelsSmall">Store Number</span></td>
                                                            <td align="left">
                                                                <asp:Label ID="lblRStoreNumberText" runat="server" Font-Bold="true"></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <span class="LabelsSmall">MCC Category Code</span></td>
                                                            <td align="left">
                                                                <asp:Label ID="lblRMCCCodeText" runat="server" Font-Bold="true"></asp:Label></td>
                                                            <td align="right">
                                                            </td>
                                                            <td align="left">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>-->
                                                <asp:Panel ID="pnlPlatform" runat="server"  Visible="False" Width="100%">
                                                    <table cellpadding="0" cellspacing="5" border="0" style="width: 100%;" class="DivGreen">
                                                        <tr>
                                                            <!--<td align="left" style="width: 25%">
                                                                <span class="LabelsSmall"></span></td>-->
                                                            <td align="left" style="width: 50%" colspan="2">
                                                                <asp:DropDownList ID="lstPlatform" runat="server" TabIndex="4" Width="170px"></asp:DropDownList>
                                                                </td>
                                                            <td align="right" style="width: 25%">
                                                                <span class="LabelsSmall">Login ID</span></td>
                                                            <td align="left" style="width: 25%">
                                                                <asp:Label ID="lblLoginIDText" runat="server" Font-Bold="true"></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <span class="LabelsSmall">Merchant ID</span></td>
                                                            <td align="left">
                                                                <asp:Label ID="lblMerchantIDText" runat="server" Font-Bold="true"></asp:Label></td>
                                                            <td align="right">
                                                                <span class="LabelsSmall">Terminal ID</span></td>
                                                            <td align="left">
                                                                <asp:Label ID="lblTerminalIDText" runat="server" Font-Bold="true"></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <span class="LabelsSmall">Bank ID Number (BIN)</span></td>
                                                            <td align="left">
                                                                <asp:Label ID="lblBINNumberText" runat="server" Font-Bold="true"></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <span class="LabelsSmall">Agent Bank Number</span></td>
                                                            <td align="left">
                                                                <asp:Label ID="lblAgentBankNumberText" runat="server" Font-Bold="true"></asp:Label></td>
                                                                <td align="right">
                                                                <span class="LabelsSmall">Agent Chain Number</span></td>
                                                            <td align="left">
                                                                <asp:Label ID="lblAgentChainNumberText" runat="server" Font-Bold="true"></asp:Label></td>
                                                            
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <span class="LabelsSmall">MCC Category Code</span></td>
                                                            <td align="left">
                                                                <asp:Label ID="lblMCCCodeText" runat="server" Font-Bold="true"></asp:Label></td>
                                                            <td align="right">
                                                                <span class="LabelsSmall">Store Number</span></td>
                                                            <td align="left">
                                                                <asp:Label ID="lblStoreNumberText" runat="server" Font-Bold="true"></asp:Label></td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                                <asp:Panel ID="pnlDBEBT" runat="server" Width="100%">
                                                    <asp:Table ID="tblDBEBT" runat="server" Width="100%" style="width: 100%;" class="DivGreen">
                                                    </asp:Table>
                                                </asp:Panel>
                                            </td>
                                        </tr>                                      
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <asp:Panel ID="pnlGateway" runat="server" Height="100%" Width="100%">
                                                    <table border="0" style="width: 100%;" class="DivGreen">
                                                        <tr>
                                                            <td style="height: 18px; width: 70%;" align="left">
                                                                <asp:Label Font-Bold="true" ID="lblGatewayHeader" Font-Size="9" runat="server" Text="Payment Gateway"></asp:Label></td>
                                                            <td align="left" style="height: 18px">
                                                                <b>
                                                                    <asp:Label ID="lblGateway" runat="server"></asp:Label></b></td>
                                                        </tr>
                                                        <!--<tr>
                                                            <td align="left">
                                                                <asp:Label ID="lblSetupFee" runat="server" Text="Setup Fee"></asp:Label></td>
                                                            <td align="left" style="height: 15px;">
                                                               <asp:Label ID="lblGatewaySetupFee" runat="server"> <b><span class="LabelsSmall">$ </span></b></asp:Label></td>
                                                        </tr>-->
                                                        <tr>
                                                            <td align="left">
                                                                <asp:Label ID="lblGatewayAccess" runat="server" Text="Monthly Gateway Access"></asp:Label></td>
                                                            <td align="left" style="height: 15px;">
                                                                <b><span class="LabelsSmall">$ </span><asp:Label ID="lblMonthlyGatewayAccess" runat="server"></asp:Label></b></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 15px; width: 70%;" align="left">
                                                                <asp:Label ID="lblTransFee" runat="server" Text="Gateway Transaction Fee"></asp:Label></td>
                                                            <td align="left">
                                                                <b><span class="LabelsSmall">$ </span><asp:Label ID="lblGatewayTransFee" runat="server"></asp:Label></b></td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                                <asp:Panel ID="pnlGatewayInfo" runat="server" Visible="False" Width="100%">
                                                    <table cellpadding="0" cellspacing="5" border="0" style="width: 100%;" class="DivGreen">
                                                        <!--<tr>
                                                            <td align="right" style="width: 25%">
                                                                <span class="LabelsSmall">Gateway</span></td>
                                                            <td align="left" style="width: 25%">
                                                                <asp:Label ID="lblGatewayText" runat="server" Font-Bold="true"></asp:Label></td>
                                                            <td align="right" style="width: 25%">
                                                            </td>
                                                            <td align="left" style="width: 25%">
                                                            </td>
                                                        </tr>-->
                                                        <tr>
                                                            <td align="left">
                                                                <span class="LabelsSmall">Login/User ID</span></td>
                                                            <td align="left">
                                                                <asp:Label ID="lblLoginUserIDText" runat="server" Font-Bold="true"></asp:Label></td>
                                                            <td align="right">
                                                            </td>
                                                            <td align="left">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                        <td align="left">
                                                                <asp:DropDownList ID="lstGatewayStatus" runat="server" TabIndex="10" Width="170">
                                                                </asp:DropDownList></td>
                                                            <td align="left">
                                                                <span class="LabelsSmall"></span></td>
                                                            
                                                            <td align="left">
                                                            </td>
                                                            <td align="left">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <asp:Panel ID="pnlAdditionalServices" runat="server" Width="100%">
                                                    <asp:Table ID="tblAddlServices" runat="server" Width="100%" style="width: 100%;" class="DivGreen">
                                                    </asp:Table>
                                                </asp:Panel>

                                            </td>
                                        </tr>
                                        
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                        <td align="center">
                                        <div style="width: 100%;" align="center">
                                                <!--<asp:Label ID="lblContactHeader" Font-Bold="true" runat="server" Text="Contact:"></asp:Label>
                                                &nbsp;<asp:Label ID="lblContact" Font-Bold="true" runat="server"></asp:Label><br />-->
                                                
                                                <asp:Panel ID="pnlReprogramQuestion" runat="server" Width="100%" Visible="false">
                                                    <asp:Label ID="lblReprogramHeader" runat="server" Font-Size="9" Text=" Re-Program Existing Merchant Account"></asp:Label>&nbsp;
                                                    <asp:RadioButton ID="rdbYes" runat="server" GroupName="rdbReprogram" Text="Yes" OnCheckedChanged="rdbYes_CheckedChanged"
                                                        AutoPostBack="True" TabIndex="21" Enabled="false" />
                                                    <asp:RadioButton ID="rdbNo" runat="server" GroupName="rdbReprogram" Text="No" AutoPostBack="True"
                                                        OnCheckedChanged="rdbYes_CheckedChanged" Enabled="false" /><br />
                                                    <asp:Label ID="lblWarning" runat="server" Text='("Yes" if the customer already has a merchant account. "No" if no previous merchant account)'></asp:Label>
                                                    <br />
                                                </asp:Panel>
                                                
                                                <br />
                                                <br />
                                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                                                    TabIndex="32" />
                                            </div>
                                                      </td>
                                                      </tr>
                                         <Triggers>
                                            <asp:PostBackTrigger ControlID="btnSubmit" />
                                        </Triggers>
                                    </table>
                                </ContentTemplate>                                
                            </cc1:TabPanel>
                            
                            
                        </cc1:TabContainer>
                        <div align="center" style="height: 30px; background-color: #565294;">
                            <asp:Button ID="btnCreateIMSApp" runat="server" Text="Create IMS App"
                                Visible="False" />
                            <input type="button" value="Close" style="height: 25px; width: 50px;font-size:8pt; font-family:Arial" onclick="javascript:window.close();">
                        </div>
                    </td>
                </tr>
            </table>
        </form>
    </center>
</body>
</html>