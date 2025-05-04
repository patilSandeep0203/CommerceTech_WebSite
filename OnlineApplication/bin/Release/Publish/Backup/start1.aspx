<%@ Page Language="C#" MasterPageFile="site.master" AutoEventWireup="true" CodeFile="start1.aspx.cs"
    Inherits="Questions" Title="Commerce Technologies - Online Application"%>
    
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentHolder" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <cc1:PopupControlExtender ID="pceMA" runat="server" PopupControlID="pnlMA"
            TargetControlID="imgMA" Position="Bottom" />
        <cc1:PopupControlExtender ID="pceOD" runat="server" PopupControlID="pnlOD"
            TargetControlID="imgOD" Position="Bottom" />
        <cc1:PopupControlExtender ID="pceEBT" runat="server" PopupControlID="pnlEBT"
            TargetControlID="imgEBT" Position="Bottom" />
        <cc1:PopupControlExtender ID="pcePG" runat="server" PopupControlID="pnlPG"
            TargetControlID="imgPG" Position="Bottom" />
        <cc1:PopupControlExtender ID="pceCS" runat="server" PopupControlID="pnlCS"
            TargetControlID="imgCS" Position="Bottom" />
        <cc1:PopupControlExtender ID="pceGC" runat="server" PopupControlID="pnlGC"
            TargetControlID="imgGC" Position="Bottom" />
        <cc1:PopupControlExtender ID="pceMF" runat="server" PopupControlID="pnlMF"
            TargetControlID="imgMF" Position="Bottom" />
        <cc1:PopupControlExtender ID="pcePayroll" runat="server" PopupControlID="pnlPayroll"
            TargetControlID="imgPayroll" Position="Bottom" />
        <asp:Label ID="lblError" runat="server" Visible="False"></asp:Label>
        <br />
        <table style="width: 700px" border="0">
            <tr>
            <td>
            <asp:Panel ID="Panel" runat="server" Height="100%" Width="100%">
                <div style="width: 100%; text-align: left" align="center">
                    <strong><span class="LabelsHeader">Select the Services you want to sign up for below:</span></strong>
                    <br /><br />
                    <table class="LabelsBody" width="100%">
                        <tr>
                            <td align="left" width=60%>                                    
                                <asp:CheckBox ID=chkMA runat="Server" Text="Merchant Account" AutoPostBack="true" 
                                OnCheckedChanged="chkMA_CheckChanged"/>
                                <asp:Image ID="imgMA" runat="server" ImageUrl="/Application/Images/help.gif" ToolTip="Help" />
                            </td>
                            <td align="left">
                                <asp:CheckBox ID=chkCS runat="Server" Text="Check/ACH Services" OnCheckedChanged="chkCS_CheckChanged" />    
                                <asp:Image ID="imgCS" runat="server" ImageUrl="/Application/Images/help.gif" ToolTip="Help"/>                      
                            </td>    
                        </tr>
                        <tr height=10></tr>
                        <tr>
                            <td align="left">                                    
                                <asp:CheckBox ID=chkOD runat="Server" Text="Online Debit" />
                                <asp:Image ID="imgOD" runat="server" ImageUrl="/Application/Images/help.gif" ToolTip="Help"/>                          
                            </td>
                            <td align="left">
                                <asp:CheckBox ID=chkGC runat="Server" Text="Gift/Loyalty" /> 
                                <asp:Image ID="imgGC" runat="server" ImageUrl="/Application/Images/help.gif" ToolTip="Help"/>                         
                            </td>            
                        </tr>
                        <tr height=10></tr>
                        <tr>
                            <td align="left">                                    
                                <asp:CheckBox ID=chkEBT runat="Server" Text="EBT" /> 
                                <asp:Image ID="imgEBT" runat="server" ImageUrl="/Application/Images/help.gif" ToolTip="Help"/>                         
                            </td> 
                            <td align="left">
                                <asp:CheckBox ID=chkMF runat="Server" Text="Business Funding" AutoPostBack="true" 
                                OnCheckedChanged="chkMF_CheckChanged"/>
                                <asp:Image ID="imgMF" runat="server" ImageUrl="/Application/Images/help.gif" ToolTip="Help"/>                          
                            </td>                     
                        </tr>
                        <tr height=10></tr>
                        <tr>
                            <td align="left">
                                <asp:CheckBox ID=chkGateway runat="Server" Text="Payment Gateway" AutoPostBack="true" 
                                OnCheckedChanged="chkGateway_CheckChanged"/>   
                                <asp:Image ID="imgPG" runat="server" ImageUrl="/Application/Images/help.gif" ToolTip="Help"/>                       
                            </td>   
                            <td align="left">
                                <asp:CheckBox ID=chkPayroll runat="Server" Text="Payroll" />  
                                <asp:Image ID="imgPayroll" runat="server" ImageUrl="/Application/Images/help.gif" ToolTip="Help"/>                        
                            </td>                
                        </tr>
                        <tr height=10></tr>
                        <!--<tr>
                            <td align="left">                                   
                                <asp:CheckBox ID=chkWireless runat="Server" Text="Wireless Services" />   
                                <asp:Image ID="imgWS" runat="server" ImageUrl="/Application/Images/help.gif" />                       
                            </td>
                            <td align="left">
                                <asp:CheckBox ID=chkLease runat="Server" Text="Leasing" />  
                                <asp:Image ID="imgLease" runat="server" ImageUrl="/Application/Images/help.gif" />                        
                            </td>                      
                        </tr>-->
                        <tr height=25></tr>
                        <tr>
                            <td align="center">
                                <asp:Button ID="btnStartOver" CssClass="Button" runat="server" Text="Start Over" OnClick="btnStart_Click" />
                            </td>
                            <td align="center">
                                <asp:Button ID="btnContinue" CssClass="Button" runat="server" OnClick="btnContinue_Click" Text="Continue" />
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
            <asp:Panel Style="display: none; z-index: 1; text-align: center;" ID="pnlMA" runat="server" Width="300px" CssClass="DivHelp">
            This service gives merchants the ability to accept Visa, MasterCard and may include other card type acceptance such as Discover, American Express, Diners Club, Carte Blanche, China UnionPay and JCB.
            <a href="https://commercetech.com/merchant-accounts/" target="_blank" class="LinkSmall">More Info.</a>
            </asp:Panel>
            <asp:Panel Style="display: none; z-index: 1; text-align: center;" ID="pnlOD" runat="server" Width="300px" CssClass="DivHelp">
            This service is for card present accounts and requries a pin pad for accepting pin debit transactions.
            </asp:Panel>
            <asp:Panel Style="display: none; z-index: 1; text-align: center;" ID="pnlEBT" runat="server" Width="300px" CssClass="DivHelp">
            This service is for retail business wishing to accept Electronic Benefits Transfer, better known as green stamps.
            </asp:Panel>
            <!--<asp:Panel Style="display: none; z-index: 1; text-align: center;" ID="pnlWS" runat="server" Width="300px" CssClass="DivHelp">
            Similar to wireless services for your mobile phone, this services requires a wireless terminal for mobile processing.
            </asp:Panel>-->
            <asp:Panel Style="display: none; z-index: 1; text-align: center;" ID="pnlPG" runat="server" Width="300px" CssClass="DivHelp">
            This service allows the acceptance of services such as credit cards, check cards, ACH and gift/loyalty cards 
            in real-time through a virtual terminal, website or mobile phone application using a secure connection.
            <a href="https://commercetech.com/payment-gateways/" target="_blank" class="LinkSmall">More Info.</a>
            </asp:Panel>
            <asp:Panel Style="display: none; z-index: 1; text-align: center;" ID="pnlCS" runat="server" Width="300px" CssClass="DivHelp">
            Check Services include - <br /><b>ACH</b> allows businesses the ability to debit a checking account by entering information through a virtual terminal or real time acceptance over a website.<br />
            <b>Check Conversion</b> allows businesses to convert a paper check to an electronic deposit at the point of sale using a check scanner.<br />
            <b>Check Guarantee</b> allows businesses to manually enter, call in or scan checks for guarantee.<br />
            <a href="https://commercetech.com/check-services/" target="_blank" class="LinkSmall">More Info.</a>
            </asp:Panel>
            <!--<asp:Panel Style="display: none; z-index: 1; text-align: center;" ID="pnlLease" runat="server" Width="300px" CssClass="DivHelp">
            This service allows businesses to pay for equipment and software over time rather than having to pay upon purchase.
            </asp:Panel>-->
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
            </td>
            </tr>            
        </table>
        <br /> 
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
