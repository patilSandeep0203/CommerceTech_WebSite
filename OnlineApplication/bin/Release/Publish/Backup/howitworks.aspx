<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="howitworks.aspx.cs" Inherits="howitworks" Title="Credit Card Processing, Merchant Services, Payment Processing"
    Theme="Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <br />
    <table align="center" width="95%" cellspacing="0" cellpadding="5" border="0">
        <tr>
            <td align="center" colspan="4">
                <img src="images/about-follow.jpg" height="250" width="475" border="0" alt="How it works">
        </tr>
        <tr>
            <td valign="top" align="left" colspan="3">
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <table style="width: 100%;" cellpadding="5" cellspacing="5" border="0">
                    <tr>
                        <td valign="top" align="left" class="DivGreen" width=30%>
                            <span style="font-family: Arial; font-size: 10pt; color: #DD1100"><b>1. Customer places
                                order</b></span><br />
                                <span class="LabelsBodySmall">A customer enters an order
                                on a merchant's website. The transaction information is instantly encrypted and
                                transmitted to the secure server, which is linked via a dedicated, leased
                                data line to the credit card authorization network. A dedicated data line means
                                faster transaction times. </span>
                        </td>
                        <td valign="top" align="left" class="DivGreen" width=40%>
                            <span style="font-family: Arial; font-size: 10pt; color: #194390"><b>2. e-Commerce verifies
                                with bank</b></span><br />
                                <span class="LabelsBodySmall">The transaction information
                                is sent by the secure server via leased data line to the credit card
                                authorization network where the validity of the card is checked and the availability
                                of funds on that account is verified. An authorization code is returned via leased
                                data line to the secure server. The authorization is encrypted by the
                                server and transmitted in encrypted form back to the web server of the
                                merchant which in turn triggers the fulfillment process of the order. </span>
                        </td>
                        <td valign="top" align="left" class="DivGreen" width=30%>
                            <span style="font-family: Arial; font-size: 10pt; color: #4B7837"><b>3. Receipt is issued
                                and merchant gets money</b></span><br />
                            <span class="LabelsBodySmall">An e-mail receipt is sent to
                                the customer and the merchant (either or both can be disabled at merchant's option).
                                Within 48 - 72 hours the merchant receives the money! </span>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="3">
                &nbsp;<strong><span style="font-size: 10pt; font-family: Arial">Request a </span></strong>
                <a class="Link" href="consult.aspx"><strong><span style="font-size: 10pt; font-family: Arial">
                    Free</span></strong></a><strong><span style="font-size: 10pt; font-family: Arial"> E-Consultation!&nbsp;&nbsp;Absolutely
                        No Obligation!</span></strong>
                <br />
                <br />
                <a class="Link" href="javascript:window.history.back();" onmouseover='status=" Back to previous page "; return true;'
                    onmouseout='status=" ";'>
                    <img src="images/back-on.gif" name="back" width="75" height="25" alt="" border="0"></a></td>
        </tr>
    </table>
</asp:Content>
