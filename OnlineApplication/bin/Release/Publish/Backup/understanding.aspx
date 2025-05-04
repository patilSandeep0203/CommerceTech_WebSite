<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="understanding.aspx.cs" Inherits="understanding" Title="Credit Card Processing, Merchant Services, Payment Processing"
    Theme="Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <br />
    <table border="0" width="95%" cellpadding="0" cellspacing="0">
        <tr>
            <td align="center">
                <span class="LabelsHeader"><b>This brief article is designed to give you a quick overview
                    of how e-commerce works</b></span></td>
        </tr>
        <tr>
            <td align="center">
                <span class="LabelsHeader"><b>CRITICAL COMPONENTS FOR E-COMMERCE ON THE WEB</b></span></td>
        </tr>
        <tr>
            <td style="height: 15px">
            </td>
        </tr>
        <tr>
            <td style="text-align: left;" class="LabelsHeader">
                <ol>
                    <li><b>Website</b>
                        <br />
                        <span class="LabelsBody">Your online connection to your prospects and customers. There are
                            a number of places on the web to get "free" websites - although you may be required
                            to display an advertisement. The design of your site is critical to the success
                            of your business! If it does not look like a professional, legitimate business -
                            NO ONE WILL ORDER ANYTHING - no matter how great the products you have. If you are
                            serious about doing business on the web, either go back to school and learn html,
                            java, cgi, etc., or hire a professional to design your site.<br />
                            <br />
                        </span></li>
                    <li><b>Merchant account</b>
                        <br />
                        <span class="LabelsBody">Bank authorized account which allows you to accept major credit
                            cards, electronic checks, etc. Many banks will not give you a merchant account if
                            you do business on the internet because they may classify it as high risk. Be careful
                            when looking for a merchant account provider! Watch out for hidden fees, high setup
                            fees, and other unscrupulous business practices.
                            <br />
                            <br />
                        </span></li>
                    <li><b>Payment Transaction Software</b>
                        <br />
                        <span class="LabelsBody">Software which actually processes customer order information, address,
                            credit card number, etc. The data is sent to a credit card authorization network
                            which verifies that the credit card is valid and verifies that the shipping address
                            matches the billing address. A possible warning sign that the card has been stolen
                            is that the billing and shipping addresses do not match. </span>
                        <br />
                        <br />
                    </li>
                    <li><b>Secure Server Connection - https://</b>
                        <br />
                        <span class="LabelsBody">Link to a special computer which encrypts confidential ordering
                            data for customer protection. You know you are on a secure server when the URL in
                            your browser says "https://". The "s" stands for "secure." If ordering information
                            is not sent via a secure server it can be intercepted by computer hackers.
                            <br />
                            <br />
                        </span></li>
                    <li><b>Shopping Cart</b>
                        <br />
                        <span class="LabelsBody">Software which allows you to accept product orders for multiple
                            products from your website. This software automatically calculates and totals orders
                            for your customers. Some setup must be done in the html code of your website, and
                            the shopping cart software must be installed on the server which hosts your site
                            or on the secure server which accepts sensitive ordering information.
                            <br />
                        </span></li>
                </ol>
            </td>
        </tr>
        <tr>
            <td style="height: 15px">
            </td>
        </tr>
        <tr>
            <td align="center">
                <span class="LabelsBody"><b>That's it!</b><br />
                    To understand how the money actually flows from a customer to a merchant's bank
                    account, <a class="Link" href="howitworks.aspx" onmouseover='status=" How it works! "; return true;'
                        onmouseout='status=" ";'>read on</a>.</span></td>
        </tr>
        <tr>
            <td colspan="4" align="center"><br />
                <a class="Link" href="javascript:window.history.back();" onmouseover='status=" Back to previous page "; return true;'
                    onmouseout='status=" ";'>
                    <img src="images/back-on.gif" name="back" width="75" height="25" alt="" border="0"></a>
            </td>
        </tr>
    </table>
</asp:Content>
