<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="AddlServices.aspx.cs"
    Inherits="AddlServices" Theme="TextBoxSkin" %>
    
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentHolder" runat="Server">
    <asp:Panel ID="pnlMainPage" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <cc1:PopupControlExtender ID="pceCS" runat="server" PopupControlID="pnlCS"
            TargetControlID="imgCS" Position="Bottom" />
            <cc1:PopupControlExtender ID="pceGC" runat="server" PopupControlID="pnlGC"
            TargetControlID="imgGC" Position="Bottom" />
            <cc1:PopupControlExtender ID="pceMF" runat="server" PopupControlID="pnlMF"
            TargetControlID="imgMF" Position="Bottom" />
            <cc1:PopupControlExtender ID="pcePayroll" runat="server" PopupControlID="pnlPayroll"
            TargetControlID="imgPayroll" Position="Bottom" />
        <table border="0" cellspacing="0" cellpadding="0" width="100%" class="DivGreen">
            <tr>
                <td align="center"  class="DivHeader2">
                    <strong><span class="MenuHeader">Additional
                        Services</span></strong>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Label ID="lblError" runat="server" BackColor="Red" Font-Size="Medium" ForeColor="White"
                        Visible="False"></asp:Label>&nbsp;
                </td>
            </tr>
            <tr>
                <td align="center" style="height: 15px">
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Label ID="lblAdditionalServices" runat="server" Font-Bold="True" Text="Select any Additional Services you want to sign up for (Optional)"></asp:Label></td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Panel ID="pnlCPServices" runat="server" Width="100%">
                        <table border="0" cellpadding="0" cellspacing="0" width="60%">
                            <!--<tr>
                                <td align="left" style="height: 25px;">
                                    <asp:CheckBox ID="chkOnlineDebit" runat="server" Text="Online Debit **" /></td>
                            </tr>
                            <tr height = "20px"></tr>
                            <tr>
                                <td align="left" style="height: 25px;">
                                    <asp:CheckBox ID="chkEBT" runat="server" Text="EBT *" AutoPostBack="True" OnCheckedChanged="chkEBT_CheckedChanged" /></td>
                            </tr>
                            <tr><td style="height: 20px;" align="left">
                                &nbsp;&nbsp;&nbsp;<asp:Label ID="lblUSDA" runat="server" Font-Bold="True" Visible="false" Text="USDA #"></asp:Label>&nbsp;
                                <asp:TextBox ID="txtUSDA" runat="server" MaxLength="7" Visible="false" >
                                </asp:TextBox></td></tr>-->
                            <asp:Panel ID="pnlWireless" runat="server" Visible="false">
                            <tr>
                                <td align="left" style="height: 25px;">
                                    <asp:CheckBox ID="chkWirelessServices" Visible="false" Enabled="false" runat="server" Text="Wireless Services *" /></td>
                            </tr>
                            <tr height = "20px"></tr></asp:Panel>
                        </table>
                    </asp:Panel>                    
                    <asp:Panel ID="pnlServices" runat="server" Width="100%">
                    <table border="0" cellpadding="0" cellspacing="0" width="72%">
                            <tr><td align="left" style="height: 25px;">
                                <asp:CheckBox ID="chkCheckServices" runat="server" OnCheckedChanged="chkCheckServices_CheckedChanged"
                                    Text="Check Guarantee, Conversion, Verification or ACH" AutoPostBack="true" />
                                    <asp:Image ID="imgCS" runat="server" ImageUrl="/Application/Images/help.gif" ToolTip="Help"/>
                            </td></tr>
                            <tr><td align="left">
                                &nbsp;&nbsp;&nbsp;<asp:Label ID="lblCheckServicesList" runat="server" Font-Bold="True" Text="Select Check Service" Visible="false"></asp:Label>&nbsp;
                                <asp:DropDownList ID="lstCheckServices" runat="server" Visible="false">
                                   
                                    <asp:ListItem>eCheck.Net</asp:ListItem>

                                </asp:DropDownList></td></tr>
                            <tr>
                                <td align="left" style="height: 25px;">
                                    <asp:CheckBox ID="chkGiftCard" runat="server" Text="Gift/Loyalty" OnCheckedChanged="chkGiftCard_CheckedChanged" AutoPostBack="true"/>
                                                                 <asp:Image ID="imgGC" runat="server" ImageUrl="/Application/Images/help.gif" ToolTip="Help"/>    
                                    </td>
                      
                            </tr>
                            <tr><td style="height: 20px" align="left">
                                &nbsp;&nbsp;&nbsp;<asp:Label ID="lblGiftCardType" runat="server" Font-Bold="True" Text="Gift Card Company" Visible="false"></asp:Label>&nbsp;
                                <asp:TextBox ID="txtGiftCardType" runat="server" ReadOnly="true" Visible="false" Width="150">
                                </asp:TextBox></td></tr>      
                                <asp:Panel ID="pnlMerchantFunding" runat="server">                      
                            <tr>
                                <td align="left" style="height: 25px;">
                                    <asp:CheckBox ID="chkMerchantFunding" runat="server" Text="Business Funding" AutoPostBack="True" OnCheckedChanged="chkMCA_CheckedChanged" />                                    
                               
                               <asp:Image ID="imgMF" runat="server" ImageUrl="/Application/Images/help.gif" ToolTip="Help"/> <br />                         
                               </td>
                            </tr> 

                            <tr><td style="height: 20px" align="left">
                            
                                &nbsp;&nbsp;&nbsp;<asp:Label ID="lblMCAType" runat="server" Font-Bold="True" Text="Merchant Cash Advance Company" Visible="false"></asp:Label>&nbsp;
                                <asp:DropDownList ID="lstMCAType" runat="server" Visible="false">
                                <asp:ListItem>RapidAdvance</asp:ListItem>
                                </asp:DropDownList></td>  <td style="height: 20px" align="left">
                                &nbsp;&nbsp;&nbsp;<asp:Label ID="lblMCACashDesired" runat="server" Font-Bold="True" Text="Amount desired" Visible="false" Width="150"><asp:TextBox ID="txtCashDesired" runat="server"  /></asp:Label>&nbsp;
                                                   
                                                    <br />
                                              </td>                          
                            </tr>
                            </asp:Panel> 
                            <tr>
                                <td align="left" style="height: 25px;">
                                    <asp:CheckBox ID="chkPayroll" runat="server" Text="Payroll" AutoPostBack="True" OnCheckedChanged="chkPayroll_CheckedChanged" /> <asp:Image ID="imgPayroll" runat="server" ImageUrl="/Application/Images/help.gif" ToolTip="Help"/>     </td>
                            </tr>
                            <tr><td style="height: 20px;" align="left">
                                &nbsp;&nbsp;&nbsp;<asp:Label ID="lblPayrollType" runat="server" Font-Bold="True" Text="Payroll Type"></asp:Label>&nbsp;
                                <asp:DropDownList ID="lstPayrollType" runat="server" Visible="false">
                                

                                    <asp:ListItem>Intuit QuickBooks Payroll Assisted</asp:ListItem>
                                    
                                    
                                   
                                </asp:DropDownList></td>
                            </tr>                              
                            <asp:Panel ID="pnlLease" Visible = "false" runat="server">
                            <tr>
                                <td align="left" style="height: 25px;">
                                    <asp:CheckBox ID="chkLease" runat="server" Enabled="false" Text="Lease **" AutoPostBack="True"/><br />                                    
                               </td>
                            </tr>
                            <tr><td style="height: 20px;" align="left">
                                &nbsp;&nbsp;&nbsp;<asp:Label ID="lblLeaseCompany" runat="server" Enabled="false" Font-Bold="True" Text="Lease Company"></asp:Label>&nbsp;
                                <asp:TextBox ID="txtLeaseCompany" runat="server" Enabled="false" Visible="false" Width="150">
                                </asp:TextBox></td>
                            </tr>
                            </asp:Panel>                        
                        </table>
                    </asp:Panel>
                </td>
            </tr>       
               
            <tr>
                <td align="center" style="height: 30px">
                    <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" ToolTip="Cancel and Go Back to Previous Page" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnContinue" runat="server" Text="Continue" OnClick="btnContinue_Click" ToolTip="Save Your Application" />                    
                </td>
            </tr>
            <asp:Panel Style="display: none; z-index: 1; text-align: center;" ID="pnlCS" runat="server" Width="300px" CssClass="DivHelp">
            Check Services include - <br /><b>ACH</b> allows businesses the ability to debit a checking account by entering information through a virtual terminal or real time acceptance over a website.<br />
            <b>Check Conversion</b> allows businesses to convert a paper check to an electronic deposit at the point of sale using a check scanner.<br />
            <b>Check Guarantee</b> allows businesses to manually enter, call in or scan checks for guarantee.<br />
            <a href="https://commercetech.com/check-services/" target="_blank" class="LinkSmall">More Info.</a>
            </asp:Panel>
            <asp:Panel Style="display: none; z-index: 1; text-align: center;" ID="pnlGC" runat="server" Width="300px" CssClass="DivHelp">
            This service allows businesses to sell and accept gift cards at the point of sale or over the internet. It also allows businesses to set up and track customer loyalty programs.
            <a href="https://commercetech.com/gift-loyalty-card-services/" target="_blank" class="LinkSmall">More Info.</a>
            </asp:Panel>
            <asp:Panel Style="display: none; z-index: 1; text-align: center;" ID="pnlMF" runat="server" Width="300px" CssClass="DivHelp">
            This service allows businesses to obtain cash for various business usage that they may not have otherwise been able to receive from a bank.
            <a href="https://commercetech.com/business-funding/" target="_blank" class="LinkSmall">More Info.</a>
            </asp:Panel>
             <asp:Panel Style="display: none; z-index: 1; text-align: center;" ID="pnlPayroll" runat="server" Width="300px" CssClass="DivHelp">
            This service allows you to pay employees, file payroll taxes and send tax payments in just a few clicks.
            <a href="https://commercetech.com/payroll/" target="_blank" class="LinkSmall">More Info.</a>
            </asp:Panel>
            <!--<tr>
                <td align="center">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblFooter" runat="server" Text='Clicking "Continue" will save your application'></asp:Label></td>
            </tr>-->
        </table>
    </asp:Panel>
</asp:Content>
