<%@ Page Language="C#" MasterPageFile="/Application/Main.master" AutoEventWireup="true"
    CodeFile="notice.aspx.cs" Inherits="Merchant_notice" Title="Commerce Technologies - Online Application"
    Theme="TextBoxSkin" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentHolder" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table border="0" cellpadding="2" cellspacing="0" Width="100%" class="DivGreen">
        <tr>
            <td colspan="3" align="center" class="DivHeader2" height="26px">
                <b><span class="MenuHeader">Merchant Account Signup Information</span></b>
            </td>
        </tr>
        <tr>
            <td align="center">
                <a href="cardpct.aspx" onmouseover="document.mcardpct.src='/Application/Images/oa_CardPercentages_mouseout.gif'"
                    onmouseout="document.mcardpct.src='/Application/Images/oa_CardPercentages_mouseover.gif'">
                    <img name="mcardpct" style="border: 0" alt="" src="/Application/Images/oa_CardPercentages_mouseover.gif" /></a>
                <a href="companyinfo.aspx" onmouseover="document.mcontact.src='/Application/Images/oa_Contact_mouseout.gif'"
                    onmouseout="document.mcontact.src='/Application/Images/oa_Contact_mouseover.gif'">
                    <img name="mcontact" style="border: 0" alt="" src="/Application/Images/oa_Contact_mouseover.gif" /></a>
                <a href="businessinfo.aspx" onmouseover="document.mbusiness.src='/Application/Images/oa_Business_mouseout.gif'"
                    onmouseout="document.mbusiness.src='/Application/Images/oa_Business_mouseover.gif'">
                    <img name="mbusiness" style="border: 0" alt="" src="/Application/Images/oa_Business_mouseover.gif" /></a>
                <a href="principalinfo.aspx" onmouseover="document.mprincipal.src='/Application/Images/oa_Principal_mouseout.gif'"
                    onmouseout="document.mprincipal.src='/Application/Images/oa_Principal_mouseover.gif'">
                    <img name="mprincipal" style="border: 0" alt="" src="/Application/Images/oa_Principal_mouseover.gif" /></a>
                <a href="bankinginfo.aspx" onmouseover="document.mbanking.src='/Application/Images/oa_Banking_mouseout.gif'"
                    onmouseout="document.mbanking.src='/Application/Images/oa_Banking_mouseover.gif'">
                    <img name="mbanking" style="border: 0" alt="" src="/Application/Images/oa_Banking_mouseover.gif" /></a>
            </td>
        </tr>
        <tr>
            <td align="center" valign="middle">
                <asp:Label ID="lblError" runat="server" BackColor="Red" Font-Size="Medium" ForeColor="White"
                    Visible="False"></asp:Label></td>
        </tr>
        <tr>
            <td align="left">
            <br />
            <span style="font-family: Arial; color: #383838; font-size: small">Click on the button below and follow these instructions to complete the application process:</span>
                <ul style="margin-left:50px; margin-top:0px; margin-bottom:0px; font-family: Arial; color: #383838; font-size: small">
                <li>Print the document</li>
                <li>Make any corrections and fill out the blank areas</li>
                <li>Initial, sign and date where highlighted</li>
                <li>Fax completed application along with a voided check pre-printed with your business name, business license and/or 
                utility bill to your sales consultant or our general fax 1 (310) 321-5410</li></ul>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Label ID="lblIncomplete" runat="server" ForeColor="Red" Text="Your application is not complete. Click on the red button(s) to the left to complete your application."
                    Visible="False"></asp:Label><br />
                <span style="color: #000000">
                    <asp:Panel ID="pnlGatewayIncomplete" runat="server" Height="50px" Width="100%" Visible="False">
                        <span style="font-family: Arial; color: #FF0000">Your Payment Gateway information is
                            still Incomplete.<br />
                            Please</span>
                        <asp:HyperLink ID="lnlGatewayIncomplete" runat="server" Font-Names="Arial" Font-Size="Small"
                            Font-Bold="True" NavigateUrl="/Application/Gateway/reprogram.aspx">click here</asp:HyperLink>
                        <span style="font-family: Arial; color: #FF0000">to complete the Gateway information.</span></asp:Panel>
                </span>
                <asp:Label ID="lblComplete" runat="server" Text="Your Merchant application is now complete"
                    Visible="False" Font-Names="Arial" Font-Size="Larger"></asp:Label><br />
                <b><span style="font-family: Arial; color: #383838; font-size: small">Your Application
                    has been saved with Application ID #: </span>
                    <asp:Label ID="lblAppId" runat="server"></asp:Label><br />
                </b><span style="font-family: Arial; color: #383838; font-size: small">(Please retain
                    this number for future reference.)</span>
                <asp:Panel ID="pnlCreatePDF" runat="server" Width="100%">
                    <asp:Label ID="lblCompleteMsg" runat="server" Text="You can click on the Create PDF button to generate the PDF for your application. Please print the document, make any corrections, sign and date where highlighted and fax with the voided check to (310) 321-5410.  If you have any questions, contact your Sales Agent or click on the Contact Us link at the top of the page."
                        Visible="False"></asp:Label><br />
                    <asp:ImageButton ID="imgCreatePDF" runat="server" ImageUrl="~/Images/CreatePDF.gif"
                        OnClick="imgCreatePDF_Click" />
                    <cc1:PopupControlExtender ID="PopupControlExtender3" runat="server" PopupControlID="pnlPDFNote" 
                        TargetControlID="imgPDFHelp1" Position="Bottom" />
                    <asp:Image ID="imgPDFHelp1" runat="server" ImageUrl="~/Images/help.gif" 
                        Style="cursor: pointer"/><br />
                    <asp:Panel ID="pnlChasePDF" runat="server" BackColor="Ivory" BorderColor="Red" BorderWidth="1px"
                        Visible="False" Width="220px">
                        <strong><span style="font-size: small; color: #383838; font-family: Arial">Click on each of the following to 
                        create Chase PDFs and complete and fax them back at 1 (310) 321-5410<span
                                style="font-size: 12pt; color: #000000; font-family: Times New Roman"></span></span>:<br />
                            <span style="font-size: 8pt">
                                <asp:LinkButton ID="btnChaseAbout" runat="server" CssClass="Link" Font-Names="Arial"
                                    Font-Size="10pt" OnClick="btnChaseAbout_Click">About Merchant</asp:LinkButton><br />
                                <asp:LinkButton ID="btnChaseFee" runat="server" CssClass="Link" Font-Names="Arial"
                                    Font-Size="10pt" OnClick="btnChaseFee_Click">Fee Schedule</asp:LinkButton>
                                <br />
                                <asp:LinkButton ID="btnChaseMP" runat="server" CssClass="Link" Font-Names="Arial"
                                    Font-Size="10pt" OnClick="btnChaseMP_Click">Merchant Processing</asp:LinkButton><br />
                                <asp:LinkButton ID="btnCreditAdd" runat="server" CssClass="Link" Font-Names="Arial"
                                    Font-Size="10pt" OnClick="btnChaseCreditAdd_Click">Credit Addendum</asp:LinkButton><br />
                                <asp:HyperLink ID="lnkOpGuide" runat="server" CssClass="Link" Font-Names="Arial" Font-Size="10pt"
                                    NavigateUrl="../Chase Operating Guide.pdf" Target="_blank">Chase Operating Guide</asp:HyperLink>
                            </span></strong>
                    </asp:Panel>
                    <asp:Label ID="lblDownload" runat="server" Font-Bold="True" Visible="False"></asp:Label><br />
                    <asp:Panel ID="pnlPDFNote" runat="server" BackColor="Ivory" BorderColor="silver"
                        BorderWidth="1px" Width="20%">
                        <asp:Label ID="lblPDFNote" runat="server" Font-Names="Arial" Font-Size="Small" ForeColor="Red"
                            Text="NOTE: PDF Creation works only with the Microsoft Internet Explorer browser (Version 6.0 or above) and Mozilla Firefox Version 2.0 or above. For Creating PDFs, please install Abode Acrobat Reader Version 8.0 or higher. Download the latest version here: "></asp:Label><asp:HyperLink
                                ID="lnkAdobe" runat="server" Font-Names="Arial" Font-Size="Small" NavigateUrl="http://www.adobe.com"
                                Target="_blank">www.adobe.com</asp:HyperLink></asp:Panel>
                </asp:Panel>
                <br /><br />
                <b><span style="font-family: Arial; color: #383838; font-size: small">Thank you for
                    applying with Commerce Technologies!<br />
                </span></b>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="height: 31px" valign="middle">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
