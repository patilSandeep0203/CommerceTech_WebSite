<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="contactus.aspx.cs"
    Inherits="contactus" Title="Credit Card Processing, Merchant Services, Payment Processing"
    Theme="Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="lblError" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="Small"
        ForeColor="Red" Visible="False"></asp:Label><br />
    <table width="95%" cellspacing="0" cellpadding="5" border="0">
        <tr>
            <td align="center" valign="top" colspan=2>
                <table width="90%" cellspacing="2" cellpadding="2" border="0">
                    <tr>
                        <td align="center" colspan=3>
                            <span class="LabelsBody"><b><i>We are committed
                                to responding<br />to your inquiry within 24 hours or less.</i></b></span>
                            <br />
                            <br />
                        </td>
                    </tr>
                    <asp:Panel ID="pnlECEAddressInfo" runat="server" Visible="true">
                    <tr>
                        <td align="right" valign="top" width="34%">
                            <div style="width:80%" align="left">
                            <span class="LabelsBody"><b>Corporate Headquarters</b> <br />
                            3738 Bayer Ave. <br />
                            Suite 203<br />
                            Long Beach, CA 90808<br />
                            Tel: 800-477-5363 <br />
                            Tel: 310-321-5400 <br />
                            Fax: 310-321-5410 </span>
                            </div>
                        </td>
                        <td align="center" valign="top" width="33%">
                            <div style="width:70%" align="left">
                            <span class="LabelsBody"><b>Billings Office</b> <br/>
                            Tel: 800-822-7469 <br />
                            Tel: 406-656-4496 </span>
                            </div>
                        </td>
                        <td align="left" valign="top" width="33%">
                            <div style="width:70%" align="left">
                            <span class="LabelsBody"><b>Bozeman Office</b> <br />
                            Tel: 800-267-1196 <br />
                            Tel: 406-582-9006 </span>
                            </div>
                        </td>
                    </tr>
                    </asp:Panel>
                    <asp:Panel ID="pnlPartnerAddressInfo" runat="server" Visible="false">
                    <tr>
                        <td align="right" valign="top" width="60%" colspan="2">
                            <div style="width:70%" align="left">
                                <asp:Label CssClass="LabelsBody" ID=lblCompanyAddress runat="server"></asp:Label><br />
                            </div>
                        </td>
                        <td align="left" valign="top" width="40%">
                            <div style="width:90%" align="left">
                                <asp:Label ID=lblMailingAddress CssClass="LabelsBody" runat="server"></asp:Label><br />
                            </div>
                        </td>
                    </tr>
                    </asp:Panel>
                </table>
            </td>
        </tr>
        <tr height="10px"></tr>
        <tr>
            <td align="center" valign="middle" colspan=2>
                <table width="90%" cellspacing="2" cellpadding="2" border="0" class="DivWhiteSmoke;">
                    <tr>
                        <td align="left" class="DivHeader" valign="middle" width="25%">
                            <span class="MenuHeader"><b>&nbsp;Department</b></span></td>
                        <td align="center" class="DivHeader" valign="middle"  width="60%">
                            <span class="MenuHeader"><b>Description</b></span></td>
                        <td align="center"  class="DivHeader" valign="middle" width="15%">
                            <span class="MenuHeader"><b>Phone Queue Option</b></span></td>
                    </tr>
                    <tr height="25px">
                        <td align="left" valign="top">
                            <asp:HyperLink ID="lnkSales" runat="server" CssClass="Link" NavigateUrl="Email.aspx?EmailType=Sales">
                                <span class="Link"><b>Sales Department</b></span></asp:HyperLink></td>
                        <td align="left" valign="top"> 
                            <span class="LabelsBody">Questions about our products, services or partnership programs.</span></td>
                        <td align="center" valign="top"> 
                            <span class="LabelsBody">1</span></td>
                    </tr>
                    <tr height="25px">
                        <td align="left" valign="top">
                            <asp:HyperLink ID="lnkNewAccounts" runat="server" CssClass="Link" NavigateUrl="Email.aspx?EmailType=NA">
                                <span class="Link"><b>New Accounts</b></span></asp:HyperLink></td>
                        <td align="left" valign="top"> 
                            <span class="LabelsBody">Questions or help with orders or applications.</span></td>
                        <td align="center" valign="top"> 
                            <span class="LabelsBody">2</span></td>
                    </tr>
                    <tr height="25px">
                        <td align="left" valign="top">
                            <asp:HyperLink ID="lnkCustomerSupport" runat="server" CssClass="Link" NavigateUrl="Email.aspx?EmailType=CS">
                                <span class="Link"><b>Customer Support</b></span></asp:HyperLink></td>
                        <td align="left" valign="top"> 
                            <span class="LabelsBody">Questions or help with existing products and services.</span></td>
                        <td align="center" valign="top"> 
                            <span class="LabelsBody">2, 3</span></td>
                    </tr>
                    <tr height="25px">
                        <td align="left" valign="top">
                            <asp:HyperLink ID="lnkPartnerSupport" runat="server" CssClass="Link" NavigateUrl="Email.aspx?EmailType=PS">
                                <span class="Link"><b>Partner Support</b></span></asp:HyperLink></td>
                        <td align="left" valign="top"> 
                            <span class="LabelsBody">Questions or help with potential or existing customers.</span></td>
                        <td align="center" valign="top"> 
                            <span class="LabelsBody">4</span></td>
                    </tr>
                    <tr height="25px">
                        <td align="left" valign="top">
                            <asp:HyperLink ID="HyperLink1" runat="server" CssClass="Link" NavigateUrl="Email.aspx?EmailType=Accounting">
                                <span class="Link"><b>HR / Accounting</b></span></asp:HyperLink></td>
                        <td align="left" valign="top"> 
                            <span class="LabelsBody">Questions for the HR / Accounting Personnel.</span></td>
                        <td align="center" valign="top"> 
                            <span class="LabelsBody">5</span></td>
                    </tr>
                    <tr height="25px">
                        <td align="left" valign="top">
                            <asp:HyperLink ID="lnkTechnical" runat="server" CssClass="Link" NavigateUrl="Email.aspx?EmailType=TS">
                                <span class="Link"><b>Technical Support</b></span></asp:HyperLink></td>
                        <td align="left" valign="top"> 
                            <span class="LabelsBody">Questions or help with programming, technical issues or problems with our website.</span></td>
                        <td align="center" valign="top"> 
                            <span class="LabelsBody">3</span></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr height="10px"></tr>
        <tr>
            <td align="left" valign="middle">
                <span class="LabelsBody">"After submitting an information
                    request form to Commerce Technologies, I received a prompt response from a representative
                    who was very knowledgeable, and answered my questions thoroughly. The process was
                    very easy. My merchant account was approved and activated within a matter of days."<br />
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;~ Brian Petersen, <i>State of Jefferson Company</i>&nbsp;&nbsp;&nbsp;&nbsp;</span><br />
                <img src="images/cleardot.gif" width="300" height="1" />
            </td>
            <td width="200" align="center" valign="middle">
                <img src="images/splash-contact.gif" height="150" width="200" alt="Contact Us" border="0"></td>
        </tr>
    </table>
</asp:Content>
