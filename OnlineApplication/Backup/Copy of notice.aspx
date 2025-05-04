<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="notice_old.aspx.cs" Inherits="notice" Title="Commerce Technologies - Online Application" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentHolder" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table border="0" cellpadding="2" cellspacing="0" Width="100%" class="DivGreen">

        <tr>
            <td colspan="3" align="center" class="DivHeader2" height="26px">
                <b>
                <asp:Label ID="lblHeading" runat="server" CssClass="MenuHeader" Text="Merchant Account / Payment Gateway Signup Information">
                </asp:Label></b>
            </td>
        </tr>
        <tr>
            <td align="center" valign="middle">
                <asp:Label ID="lblError" runat="server" BackColor="Red" Font-Size="Medium" ForeColor="White"
                    Visible="False"></asp:Label></td>
        </tr>
        <tr height="20px"></tr>
        <tr>
            <td align="center">
                <asp:Panel ID="pnlIncomplete" runat="server" Width=100% Visible="False"><asp:Label ID="lblIncomplete" runat="server" CssClass="LabelsRed" 
                Text="Your application is not complete. Click on the red button(s) to the left to complete your application.">
                </asp:Label><br /></asp:Panel>
                <asp:Panel ID="pnlIncompleteGW" runat="server" Width=100% Visible="False"><asp:Label ID="lblIncompleteGW" runat="server" CssClass="LabelsRed" 
                Text="Your Payment Gateway information is not complete. Click on the red button(s) to the left to complete your application.">
                </asp:Label></asp:Panel>
            </td>
        </tr>
        <tr>
            <td align="left">
            <br />
            <asp:Panel ID="pnlMA" runat="server" Width=100% Visible="False">
            <span class="LabelsBody">Click on the "Open PDF Application" button below and follow these instructions to complete the application process:</span>
                <ul class="LabelsBody" style="margin-left:50px; margin-top:0px; margin-bottom:0px;">
                <li>Print the PDF application</li>
                <li>Make any corrections and fill out the blank areas</li>
                <li>Initial, sign and date where highlighted</li>
                <li>Fax or e-mail these documents to your sales consultant or our office (Fax: (310) 321-5410;<br />E-mail: applications@commercetech.com).
                    <ul><li>Completed application </li>
                        <li>Voided or cancelled check pre-printed with your business name (in place of temporary checks, 
                            a bank letter with full account and routing number will need to be provided)</li>
                        <li>Business license and/or utility bill</li>
                    </ul>
                </li></ul>
            </asp:Panel>
            </td>
        </tr>
        <tr height="20px"></tr>
        <tr>
            <td align="center">                
                <!--<span style="color: #000000">
                    <asp:Panel ID="pnlGatewayIncomplete" runat="server" Height="50px" Width="100%" Visible="False">
                        <span style="font-family: Arial; color: #FF0000">Your Payment Gateway information is
                            still Incomplete.<br />
                            Please</span>
                        <asp:HyperLink ID="lnlGatewayIncomplete" runat="server" Font-Names="Arial" Font-Size="Small"
                            Font-Bold="True" NavigateUrl="/Application/Gateway/reprogram.aspx">click here</asp:HyperLink>
                        <span style="font-family: Arial; color: #FF0000">to complete the Gateway information.</span></asp:Panel>
                </span>-->
                <asp:Label ID="lblComplete" runat="server" Text="Your Merchant application is now complete"
                    Visible="False" CssClass="LabelsBody" ></asp:Label><br />
                <asp:Panel ID="pnlGatewayOnly" runat="server" Width=100% Visible="False"><asp:Label ID="lblGatewayOnly" runat="server" CssClass="LabelsBody"
                    Text="You will soon receive a startup e-mail in MS Word or PDF format containing the information neccessary to start using your account. If 
                    you have any questions, contact your Sales Agent or click on the Contact Us link at the top of the page.">
                    </asp:Label><br /><br />
                    <span class="LabelsBody">If you do not have a existing merchant account, <asp:HyperLink ID="lnkMerchantSignup" runat="server" 
                        NavigateUrl="UpdateAcct.aspx" CssClass="Link"><b>click here</b></asp:HyperLink> to sign up for a Merchant Account.</span><br />
                </asp:Panel>
                <!--<b><span class="LabelsBody">Your Application has been saved with Application ID #: </span>
                    <asp:Label ID="lblAppId" runat="server"></asp:Label><br />
                </b><span class="LabelsBody">(Please retain this number for future reference.)</span>-->
                <asp:Panel ID="pnlCreatePDF" runat="server" Width="100%">
                    <asp:Label ID="lblCompleteMsg" runat="server" CssClass="LabelsBody" Text="You can click on the Create PDF button to generate the PDF for your application. Please print the document, make any 
                        corrections, sign and date where highlighted and fax with the voided check to (310) 321-5410.  If you have any questions, contact your Sales Agent or click on the 
                        Contact Us link at the top of the page." Visible="False"></asp:Label><br />
                    <asp:Button CssClass="Button" ID="btnCreatePDF" runat="server" Text="Open PDF Application" OnClientClick="aspnetForm.target ='_blank';" OnClick="btnCreatePDF_Click"/>    
                    <cc1:PopupControlExtender ID="PopupControlExtender3" runat="server" PopupControlID="pnlPDFNote" 
                        TargetControlID="imgPDFHelp1" Position="Bottom" />
                    <asp:Image ID="imgPDFHelp1" runat="server" ImageUrl="~/Images/help.gif" 
                        Style="cursor: pointer"/><br />
                    <asp:Panel ID="pnlSagePDF" runat="server" Visible="False" BackColor="Ivory" Width="80%" BorderWidth="1px" BorderColor="Red">
                        <div style="text-align: center">
                            <strong><span class="LabelsBodySmall">Click on each of the following to create Sage PDFs
                                and complete and fax them back at (310) 321-5410 </span>:<br />
                                <asp:LinkButton ID="btnSageApp" runat="server" OnClick="btnSageApp_Click" CssClass="Link" Font-Names="Arial" Font-Size="10pt" CausesValidation="False">Sage Application</asp:LinkButton><br />
                                <!--<asp:LinkButton ID="btnSageMOTO" runat="server" OnClick="btnSageMOTO_Click" CssClass="Link" Font-Names="Arial" Font-Size="10pt" CausesValidation="False">Sage MOTO Application</asp:LinkButton><br />-->
                                <asp:HyperLink ID="lnkSageAgreement" Font-Names="Arial" CssClass="Link" runat="server" NavigateUrl="Sage Merchant Agreement.pdf" Target="_blank">Sage Agreement</asp:HyperLink>
                            </strong>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlChasePDF" runat="server" Visible="False" BackColor="Ivory" Width="80%"
                                        BorderWidth="1px" BorderColor="Red">
                        <div style="text-align: center">
                            <strong><span class="LabelsBodySmall">Click on each of the following to create Chase PDFs
                                and complete and fax them back at (310) 321-5410 </span>:<br />
                                <asp:LinkButton ID="btnChaseMPA" runat="server" CssClass="Link" Font-Names="Arial"
                                    Font-Size="10pt" OnClick="btnChaseMPA_Click" CausesValidation="False">Chase Merchant Application and Agreement</asp:LinkButton><br />    
                                <asp:LinkButton ID="btnChaseFS3Tier" runat="server" CssClass="Link" Font-Names="Arial"
                                    Font-Size="10pt" OnClick="btnChaseFS3Tier_Click" Visible="false" CausesValidation="False">Chase Fee Schedule 3 tier</asp:LinkButton><br />    
                                <asp:LinkButton ID="btnChaseFSInterchangePlus" runat="server" CssClass="Link" Font-Names="Arial"
                                    Font-Size="10pt" OnClick="btnChaseFSInterchangePlus_Click" Visible="false" CausesValidation="False">Chase Fee Schedule Interchange Plus</asp:LinkButton><br />    
                            </strong>
                        </div>
                    </asp:Panel>
                    <asp:Label ID="lblDownload" runat="server" Font-Bold="True" Visible="False"></asp:Label><br />
                    <asp:Panel ID="pnlPDFNote" runat="server" BackColor="Ivory" BorderColor="silver"
                        BorderWidth="1px" Width="20%">
                        <asp:Label ID="lblPDFNote" runat="server" CssClass="LabelsBodySmall"
                            Text="NOTE: PDF Creation works only with the Microsoft Internet Explorer browser (Version 7.0 
                            or above), Mozilla Firefox (Version 2.0 or above) and Safari (Version 4.0 or above). For viewing PDFs, please install Abode 
                            Acrobat Reader Version 8.0 or higher. Download the latest version here: "></asp:Label><asp:HyperLink
                                ID="lnkAdobe" runat="server" CssClass="Link" NavigateUrl="http://www.adobe.com"
                                Target="_blank">www.adobe.com</asp:HyperLink></asp:Panel>
                </asp:Panel>
                <br /><br />
                <b><span class="LabelsBody">Thank you for applying with Commerce Technologies!</span></b>
                &nbsp;</td>
        </tr>
        <tr height="20px"></tr>
    </table>
</asp:Content>
