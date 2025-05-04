<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="consultpage.aspx.cs" Inherits="consult" Title="Credit Card Processing, Merchant Services, Payment Processing"
    Theme="Admin" %>



<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
<br />
<script type="text/javascript" src='https://www.google.com/recaptcha/api.js'></script>
    <asp:Panel ID="pnlMainPage" runat="server" Width="95%">
    <table width="98%" cellspacing="0" cellpadding="2" border="0" align="right">
        <tr>
            <td align="left" colspan="2">
                <span class="LabelsBody">&nbsp;&nbsp;&nbsp;&nbsp;&quot;I have reviewed dozens of e-commerce firms
				to understand their offerings and services,
				and to gauge their willingness and ability to
				fully service their clients.&quot;
                <br/>
                    <br />
                &quot;I found Commerce Technologies to be a cut above
				and far ahead of all competitors. e-Commerce
				is the only merchant service I recommend to my clients.&quot;
				</span>
                <br/>
                <div align="right">
                    <span class="LabelsBody">&nbsp;&nbsp;&nbsp;&nbsp;~ Bruce Clay, <i>Internet Consultant</i>&nbsp;&nbsp;&nbsp;&nbsp;</span>
                </div>
            </td>
        </tr>
        <tr height="20px"></tr>
        <tr>
            <td colspan="2" align="center">
                <span class="LabelsHeader"><b>Studies have proven
                    that accepting credit cards enhances your company credibility. Customers are
                    also likely to spend more and make spontaneous purchases. Request a free consultation
                    today. There is absolutely no obligation.</b> </span>
            </td>
        </tr>
        <tr height="20px"></tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Label ID="lblError" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="Small" ForeColor="Red" Visible="False"></asp:Label>
                
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
            <b><span class="LabelsHeader">Within 24 hours you will be contacted by an E-Commerce Consultant.
                <br />
            (DURING OUR BUSINESS HOURS)</span></b>
            </td>
        </tr>
        <tr height="20px"></tr>
    </table>
    </asp:Panel>
</asp:Content>

