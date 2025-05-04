<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="merchant.aspx.cs" Inherits="merchant" Title="Credit Card Processing, Merchant Services, Payment Processing"
    Theme="Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <br />
    <table align="center" border="0" cellpadding="2" cellspacing="0" style="width:80%">
        <tr>
            <td align="center">
                <span class="LabelsHeaderLarge"><b>Merchant Account</b></span><br /><br />
            </td>
        </tr>
        <tr>
            <td align="left">
                <span class="LabelsBody">A Merchant account is a service that is required for any business to accept credit or debit cards from their customers. 
                With this service credit card transactions are sent electronically to merchant processing banks for authorization, capture and deposit.</span>
                <br />
                <br />
                <span class="LabelsBody">Various methods exist for presenting a credit card sale to the processor. 
                In all circumstances either the entire magnetic strip is read by a swipe through a credit card 
                terminal/reader or the credit card information is manually entered into a credit card terminal, 
                a computer/mobile phone or website. There are also credit cards that have built-in simchips so that 
                the merchants can now process by just bringing the cards close to or inserting them into 
                the EMV compliant contactless equipment. We will help you get set up quickly 
                and securely regardless of how you want to accept payments. 
                You can now accept Visa, Master, Discover, Amex, JCB, Diners Club, Carte Blanche, China UnionPay, 
                DinaCard and PayPal Transactions and review all these transactions in one easy monthly statement.</span>
                <br/><br/>
                <span class="LabelsBody">The process from taking the payment from your customers to receiving the deposit in the bank generally works as follows:</span>
                
                    <br />                
                    <ol class="LabelsBody">
                        <li>A customer presents their credit card for payment.</li>
                        <li>Using their credit card number, the merchant submits an electronic request to the processing network for "authorization to capture funds" from the cardholder's credit card account in the amount of the purchase. 
                        Traditionally, one would submit this request by swiping a credit card through a terminal/card reader provided by the merchant account provider. With Payment Gateways and mobile payments, 
                        this request is submitted electronically to the payment gateway servers, which then route the request along the processing network.</li>
                        <li>The processing network immediately receives the request and determines if the cardholder's account is valid and if the funds are available. 
                        If they are, the processing network returns an electronic response to the terminal or computer/mobile phone. This response is called an "authorization code", 
                        and this guarantees the capture of funds. The transaction and its associated authorization are stored in a "batch", where other transactions for that day reside.
                        </li>
                        <li>For card present transactions, the merchant can print a receipt for the customer’s signature using the electronic terminal or computer/mobile phone. 
                        As far as the customer is concerned, the transaction is complete. 
                        </li>
                        <li>The merchant, however, at the end of business day (usually), submits a final request to the processing network to go ahead 
                        and "capture the funds" that obtained authorizations during the course of business that day. 
                        This is called "settlement" or "settling your batch". This request is also submitted using the electronic terminal or the computer/mobile phone. 
                        The processing network immediately responses electronically and determines if the capture amounts contained in the request match the authorizations for each item. 
                        If so, the request is granted and an "Accepted" response is returned. 
                        A settlement report can be printed showing the grand totals by card type (Visa, MasterCard, American Express, Discover, etc) for the settled batch.</li>
                        <li>Within 24 to 72 hours (usually), the funds associated with the settled batch is deposited electronically into the merchant’s business bank account. 
                        Typically, the discount fees are deducted from the deposit before it is transferred to the bank account. 
                        Upon approval, these fees may be taken out at the end of the month.</li>
                        <li>At the end of the month, merchant account provider will mail or post online a merchant statement, 
                        detailing the credit card activity for the month and the associated fees that have been charged.  
                        </li>
                    </ol>
            </td>
        </tr>
        <tr>
            <td align="left">
                <b><span class="LabelsHeader">
                   Understanding your merchant provider’s rates and fees</span></b>
            </td>
        </tr>
        <tr>
            <td align="left">
                <span class="LabelsBody">All merchant service providers charge fees for merchant services. 
                Typically, these fees are broken down into 3 major categories: a discount rate, a transaction fee, and monthly fees. 
                A transaction is usually defined as any communication between you and the processing network. 
                A "credit" transaction is treated the same as a regular transaction. 
                </span>
                <ul class="Bullet">
                    <li><b><span class="LabelsHeader">Discount Rate: </span></b>
                        <span class="LabelsBody">This is the percentage of the total transaction amount that the bank will usually deduct prior to transferring the funds to the merchant’s bank account. 
                        Discount rates are determined by various factors like the type of business, how the payments are accepted (terminal or online etc) and many other factors. 
                        A higher rate may be charged on individual transactions if the transaction doesn't conform to certain qualifications. 
                        For instance, accepting a "Visa Business Card" credit card may cost more than debit or standard credit transactions. 
                        The reasons for these non-qualified transaction surcharges and complete details on all transaction qualifications should be discussed with merchant service provider. 
                        Address Verification (AVS) may also fall into this category when not used properly. <br/><br/>
                        If the merchant is set up on “tiered pricing” they will be charged a higher discount rate for these mid or non-qualified transactions. 
                        If the merchant is set up on “interchange pricing” they will be charged the same discount rate on all transactions 
                        and a separate “interchange fee” will also be charged to include all the surcharges for the downgrade transactions.</span>
                    </li>
                    <li><b><span class="LabelsHeader">Transaction Fees: </span></b>
                        <span class="LabelsBody">This is a flat amount that merchants pay for each transaction regardless of how much the transaction was for. Typical transaction fees range from 0 cents to 30 cents per
                        transaction.In some cases a “batch settlement” is treated as a transaction and will be charged a transaction fee.
                        </span>
                    </li>

                <li><b><span class="LabelsHeader">Monthly Fees: </span></b>
                        <span class="LabelsBody">These are fees charged for other account related services, such as customer service, 
                        tech support, online reporting, monthly statement, minimum monthly fees etc.
                        </span>
                </li>
            </ul>
            </td>
        </tr>
        <tr height=15></tr>
        <tr>
            <td align="left">
                <b><span class="LabelsHeader">
                   It is easy to get set up for a merchant account with us. </span></b>
            </td>
        </tr>
        <tr>
            <td align="left">
                <ul class="Bullet">
                    <li>
                        <span class="LabelsBody">Just click on the link below to apply online and answer a few questions.
                        </span>
                    </li>
                    <li>
                        <span class="LabelsBody">Attach a voided or cancelled check pre-printed with your business name (in place of a temporary check, a bank signed letter with full account, routing number and business name will need to be provided) 
                        and Business license, phone or utility bill or previous processing statement.
                        </span>
                </li>
                <li>
                        <span class="LabelsBody">Print and sign the pdf application and email/fax it to 310-321-5410 or sign and submit electronically.
                        </span>
                </li>
                <li>
                        <span class="LabelsBody">You will receive a response from us within 1 business day.
                        </span>
                </li>
            </ul>
            </td>
        </tr>
        <tr height=15></tr>
        <tr>
            <td colspan="2">
                <span class="LabelsBody"><a class="Link" href="faqs.aspx#MA">Click here</a> for Frequently Asked Questions about Merchant Accounts.</span>
            </td>
        </tr>
        <tr height=15></tr>
        <tr>
            <td align="center" valign="bottom">
                <asp:HyperLink CssClass="Link" Target="_blank" ID="lnkOnlineApplication" runat="server"><strong>
                        Apply Online Now!!!</strong></asp:HyperLink>
            </td>
        </tr>
        <tr height=20></tr>
    </table>
</asp:Content>
