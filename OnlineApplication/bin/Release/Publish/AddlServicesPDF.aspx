<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="AddlServicesPDF.aspx.cs"
    Inherits="AddlServicesPDF" Theme="TextBoxSkin" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentHolder" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table border="0" cellspacing="0" cellpadding="0" style="width: 100%;" class="DivGreen">
        <tr>
            <td colspan="3" align="center" class="DivHeader2">
                <b><span class="MenuHeader">Additional Services Signup Information</span></b></td>
        </tr>
        <tr>
            <td align="center">
                <asp:Label ID="lblError" runat="server" BackColor="Red" Font-Size="Medium" ForeColor="White"
                    Visible="False"></asp:Label>&nbsp;
            </td>
        </tr>
        <tr>
            
            <td valign="middle" style="text-align: left;">
            <asp:Panel ID="pnlMsg" runat="server" Width="100%">
                <span class="LabelsBody">Click on the buttons below and follow these instructions to
                    complete the application process:</span>
                <ul class="LabelsBody" style="margin-left: 50px; margin-top: 0px; margin-bottom: 0px;">
                    <li>Print the document</li>
                    <li>Make any corrections and fill out the highlighted areas</li>
                    <li>Sign and date where highlighted</li>
                    <li>Fax completed application along with a voided check pre-printed with your business
                        name to your sales consultant or our general fax 1 (310) 321-5410</li>
                </ul></asp:Panel>
                <br />
                <asp:Panel ID="pnlCreateDDPDF" runat="server" Width="100%">
                    <table width="100%">
                        <tr height="25px">
                            <td align="right" valign="top" width="50%">
                                <span class="LabelsBody">Direct Debit Application - </span>
                            </td>
                            <td align="left" valign="top">
                                <!--<asp:ImageButton ID="imgCreateDDPDF" runat="server" ImageUrl="~/Images/CreatePDF.gif"
                        OnClick="imgCreateDDPDF_Click"/>-->
                                <asp:Button ID="btnCreateDDPDF" runat="server" Text="Create PDF" OnClick="imgCreateDDPDF_Click" />
                                <cc1:PopupControlExtender ID="PopupControlExtender1" runat="server" PopupControlID="pnlPDFNote"
                                    TargetControlID="imgPDFHelp3" Position="Bottom" />
                                <asp:Image ID="imgPDFHelp3" runat="server" ImageUrl="~/Images/help.gif" Style="cursor: pointer" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                 
                <asp:Panel ID="pnlCreateGiftCardPDF" runat="server" Width="100%">
                    <table width="100%">
                        <tr height="25px">
                            <td align="right" valign="top" width="50%">
                                <span class="LabelsBody">Sage Payment Solutions EFT Gift Card Application - </span>
                            </td>
                            <td align="left" valign="top">
                                <!--<asp:ImageButton ID="imgCreateGiftCardPDF" runat="server" ImageUrl="~/Images/CreatePDF.gif"
                        OnClick="imgCreateGiftCardPDF_Click" />-->
                                <asp:Button ID="btnCreateGiftCardPDF" runat="server" Text="Create PDF" OnClick="imgCreateGiftCardPDF_Click" />
                                <cc1:PopupControlExtender ID="PopupControlExtender2" runat="server" PopupControlID="pnlPDFNote"
                                    TargetControlID="imgPDFHelp2" Position="Bottom" />
                                <asp:Image ID="imgPDFHelp2" runat="server" ImageUrl="~/Images/help.gif" Style="cursor: pointer" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlRapidAdvance" runat="server" Width="100%">
                    <table width="100%">
                        <tr height="25px">
                            <td align="right" valign="top" width="50%">
                                <span class="LabelsBody">Merchant Cash Advance (RapidAdvance) - </span>
                            </td>
                            <td align="left" valign="top">
                                <asp:Button ID="Button2" runat="server" Text="Create PDF" OnClick="imgCreateRapidAdvancePDF_Click" />
                                <cc1:PopupControlExtender ID="PopupControlExtender5" runat="server" PopupControlID="pnlPDFNote"
                                    TargetControlID="RapidAdvancePDFHelp" Position="Bottom" />
                                <asp:Image ID="RapidAdvancePDFHelp" runat="server" ImageUrl="~/Images/help.gif" Style="cursor: pointer" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlMerchantFundingBFS" runat="server" Width="100%">
                    <table width="100%">
                        <tr height="25px">
                            <td align="right" valign="top" width="50%">
                                <span class="LabelsBody">Merchant Cash Advance (Business Financial Services) - </span>
                            </td>
                                                        <td align="left" valign="top">
                                <asp:Button ID="Button1" runat="server" Text="Create PDF" OnClick="imgCreateBFSPDF_Click" />

                                
                            </td>
                            <td align="left" valign="top">
                                <asp:HyperLink ID="btnMerchantFunding" runat="server" CssClass="Link" Font-Bold="True"
                                    Target="_blank" Text="" NavigateUrl="https://www.businessfinancialservices.com/applyaff.cfm?trackid=2087"></asp:HyperLink></td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlMerchantFundingAMI" runat="server" Width="100%">
                    <table width="100%">
                        <tr height="25px">
                            <td align="right" valign="top" width="50%">
                                <span class="LabelsBody">Merchant Cash Advance (AdvanceMe, Inc.) - </span>
                            </td>
                            <td align="left" valign="top">
                                <asp:Button ID="btnCreateAMIDF" runat="server" Text="Create PDF" OnClick="imgCreateAMIPDF_Click" />
                                <cc1:PopupControlExtender ID="PopupControlExtender4" runat="server" PopupControlID="pnlPDFNote"
                                    TargetControlID="imgPDFHelp1" Position="Bottom" />
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/help.gif" Style="cursor: pointer" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlCheckServices" runat="server" Width="100%">
                    <table width="100%">
                        <tr height="25px">
                            <td colspan="2" align="center" valign="top" width="70%">
                                <span class="LabelsBody">Your sales consultant will send you your check application within 24 hours. </span>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlPayroll" runat="server" Width="100%">
                    <table width="100%">
                        <tr height="25px">
                            <td colspan="2" align="center" valign="top" width="70%">
                                <span class="LabelsBody">You will be contacted by a payroll specialist within 24 hours.</span>
                            </td>
                            <!--<td align="left" valign="top">
                                <asp:HyperLink ID="btnPayroll" runat="server" CssClass="Link" Font-Bold="True" Target="_blank"
                                    Visible="false" Text="Sign Up" NavigateUrl="https://onlinepayroll.intuit.com/servlet/gateway?action=signupsb&SC=ISP15587361&KAD=79681"></asp:HyperLink>
                                <asp:Panel CssClass="Border" ID="pnlQBPayroll" runat="server" Visible="false">
                                    <span class="LabelsBody">Please call or email William Thomas and tell him you were referred
                                        by <b>Commerce Technologies</b> to sign up for the Intuit QuickBooks Payroll Assisted
                                        Service.<br />
                                        Phone Number: (800) 365-9618 ext. 33425<br />
                                        Email Address: William_Thomas@intuit.com </span>
                                </asp:Panel>
                            </td>-->
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlCreateLeasingPDF" runat="server" Width="100%">
                    <table width="100%">
                        <tr height="25px">
                            <td align="right" valign="top" width="50%">
                                <span class="LabelsBody">Northern Leasing Application - </span>
                            </td>
                            <td align="left" valign="top">
                                <!--<asp:ImageButton ID="imgCreatLeasingPDF" runat="server" ImageUrl="~/Images/CreatePDF.gif"
                        OnClick="imgCreateLeasingPDF_Click" />-->
                                <asp:Button ID="btnCreatLeasingPDF" runat="server" Text="Create PDF" OnClick="imgCreateLeasingPDF_Click" />
                                <cc1:PopupControlExtender ID="PopupControlExtender3" runat="server" PopupControlID="pnlPDFNote"
                                    TargetControlID="imgPDFHelp1" Position="Bottom" />
                                <asp:Image ID="imgPDFHelp1" runat="server" ImageUrl="~/Images/help.gif" Style="cursor: pointer" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Panel ID="pnlNoAddlServ" runat="server" Width="100%" ForeColor="Red">
                    <br />
                    <span class="LabelsRedLarge">No Additional Services PDFs found.</span><br />
                </asp:Panel>
            </td>
        </tr>
        <tr height="15px">
        </tr>
        <tr>
            <td align="center">
                <span class="LabelsBody">You will be contacted if we need more information or once your
                    application has been set up.
                    <br />
                    If you have any questions or to check on status of your application, please log
                    back in to this application, contact your sales consultant or click on the Contact
                    Us link at the top of the page and call our customer service department.</span><br />
                <br />
                <asp:Panel ID="pnlPDFNote" runat="server" BackColor="Ivory" BorderColor="silver"
                    BorderWidth="1px" Visible="false" Width="20%">
                    <asp:Label ID="lblPDFNote" runat="server" CssClass="Labels" Text="NOTE: PDF Creation works only with the Microsoft Internet Explorer browser (Version 6.0 or above) and Mozilla Firefox Version 2.0 or above. For Creating PDFs, please install Abode Acrobat Reader Version 8.0 or higher. Download the latest version here: "></asp:Label><asp:HyperLink
                        ID="HyperLink1" runat="server" CssClass="Link" NavigateUrl="http://www.adobe.com"
                        Target="_blank">www.adobe.com</asp:HyperLink></asp:Panel>
            </td>
        </tr>
        <tr>
            <td align="center" valign="middle" style="height: 30px">
                <br />
                <!--<a href="/Application/Merchant/Company.aspx" onmouseover='status=" Back to previous page "; return true;'
        onmouseout='status=" ";'>
        <img src="/Application/Images/qc-back_off.gif" name="back" width="75" height="25"
            alt="" style="border: 0" /></a>-->
                <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" ToolTip="Cancel and Go Back to Previous Page" />
            </td>
        </tr>
    </table>
</asp:Content>
