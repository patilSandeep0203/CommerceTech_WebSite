<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="support.aspx.cs" Inherits="support" Title="Credit Card Processing, Merchant Services, Payment Processing"
    Theme="Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <br />
    <a class="Link" name="#00"></a>
    <table width="90%" cellspacing="0" cellpadding="0" border="0">
        <tr>
            <td align="left" valign="middle">
                <img src="images/bullet.gif" height="8" width="8" border="0" alt="bp"/>
            </td>
            <td align="left" valign="top">
                <a class="Link" href="#Electronicchecksoverview" onmouseover='status=" Electronic checks overview ";return true;'
                    onmouseout='status=""; return true;'><span style="font-family: Arial; font-size: 10pt;">
                        Electronic checks overview</span></a>
            </td>
        </tr>
        <tr>
            <td align="left" valign="middle">
                <img src="images/bullet.gif" height="8" width="8" border="0" alt="bp"/>
            </td>
            <td align="left" valign="top">
                <a class="Link" href="#WhatifthecustomerhasInsufficient" onmouseover='status=" What if the customer has Insufficient Funds? ";return true;'
                    onmouseout='status=""; return true;'><span style="font-family: Arial; font-size: 10pt;">
                        What if the customer has "Insufficient Funds" (NSF)?</span></a>
            </td>
        </tr>
        <tr>
            <td align="left" valign="middle">
                <img src="images/bullet.gif" height="8" width="8" border="0" alt="bp"/>
            </td>
            <td align="left" valign="top">
                <a class="Link" href="#Electronicchargeback" onmouseover='status=" What about Electronic check chargebacks? ";return true;'
                    onmouseout='status=""; return true;'><span style="font-family: Arial; font-size: 10pt;">
                        What about Electronic check chargebacks?</span></a>
            </td>
        </tr>
        <tr>
            <td align="left" valign="middle">
                <img src="images/bullet.gif" height="8" width="8" border="0" alt="bp"/>
            </td>
            <td align="left" valign="top">
                <a class="Link" href="#Electronicchecktransactions" onmouseover='status=" Electronic check transactions are NOT processed in real-time ";return true;'
                    onmouseout='status=""; return true;'><span style="font-family: Arial; font-size: 10pt;">
                        Electronic check transactions are NOT processed in real-time</span></a>
            </td>
        </tr>
        <tr>
            <td align="left" valign="middle">
                <img src="images/bullet.gif" height="8" width="8" border="0" alt="bp"/>
            </td>
            <td align="left" valign="top">
                <a class="Link" href="#ProcessingElectroniccheck" onmouseover='status=" Processing Electronic check transactions ";return true;'
                    onmouseout='status=""; return true;'><span style="font-family: Arial; font-size: 10pt;">
                        Processing Electronic check transactions</span></a>
            </td>
        </tr>
        <tr>
            <td align="left" valign="middle">
                <img src="images/bullet.gif" height="8" width="8" border="0" alt="bp"/>
            </td>
            <td align="left" valign="top">
                <a class="Link" href="#VirtualTerminaltoprocessElectronicchecks" onmouseover='status=" Using Virtual Terminal to process Electronic check transactions ";return true;'
                    onmouseout='status=""; return true;'><span style="font-family: Arial; font-size: 10pt;">
                        Using Virtual Terminal to process Electronic check transactions</span></a>
            </td>
        </tr>
        <tr>
            <td align="left" valign="middle">
                <img src="images/bullet.gif" height="8" width="8" border="0" alt="bp"/>
            </td>
            <td align="left" valign="top">
                <a class="Link" href="#WebLinktoacceptElectroniccheck" onmouseover='status=" Using WebLink to accept Electronic checks from your web site ";return true;'
                    onmouseout='status=""; return true;'><span style="font-family: Arial; font-size: 10pt;">
                        Using WebLink to accept Electronic checks from your web site</span></a>
            </td>
        </tr>
        <tr>
            <td align="left" valign="middle">
                <img src="images/bullet.gif" height="8" width="8" border="0" alt="bp"/>
            </td>
            <td align="left" valign="top">
                <a class="Link" href="#ElectroniccheckActivityMenu" onmouseover='status=" Using the Electronic check Activity Menu ";return true;'
                    onmouseout='status=""; return true;'><span style="font-family: Arial; font-size: 10pt;">
                        Using the Electronic check Activity Menu</span></a>
            </td>
        </tr>
        <tr>
            <td align="left" valign="middle">
                <img src="images/bullet.gif" height="8" width="8" border="0" alt="bp"/>
            </td>
            <td align="left" valign="top">
                <a class="Link" href="#Electroniccheckaccount" onmouseover='status=" How do I obtain Electronic check account? ";return true;'
                    onmouseout='status=""; return true;'><span style="font-family: Arial; font-size: 10pt;">
                        How do I obtain an Electronic check account?</span></a>
            </td>
        </tr>
        <tr>
            <td align="left" valign="middle" colspan="4">
                <a class="Link" name="Electronicchecksoverview"></a><span style="font-family: Arial;
                    font-size: 10pt;">                    
                    <b>
                        <br />
                        <hr noshade="noshade" size="1" style="font-weight: bold" />
                        <span style="font-size: 12pt; font-family: Times New Roman">&nbsp;</span>Electronic Checks Overview</b>
                </span>
            </td>
        </tr>
        <tr>
            <td align="left" valign="middle" colspan="4">
                <span style="font-family: Arial; font-size: 10pt;">
                    <br />
                    The system supports the processing of Electronic check transactions.
                    You must have a Electronic check processing account in order to accept
                    Electronic checks.
                    <br />
                    <br />
                    When you accept an Electronic check as a form of payment, you are allowing your
                    customers to give you their checking account information and the authorization to
                    debit their bank account directly as payment for goods or services. The customer
                    simply provides their bank name, checking account number, and ABA Transit/Routing
                    number. It is just as easy as providing a credit card as a form of payment. Using
                    your Electronic check account, the system then debits your
                    customer's checking account and transfers the funds first into your Electronic
                    check account, and then into your regular business checking account. The total time
                    that it takes for the funds to move from your customer's checking account through
                    the system and into your regular business checking account is approximately 5 business
                    days (Approximately 2 days slower than credit card funds). The flow of funds in
                    an Electronic check transaction is as follows:
                    <br />
                    <br />
                    Customer provides their checking information and authorization to merchant,
                    then..
                    <br />
                    <br />
                    The funds are deposited (by Electronic check) into the merchant's Electronic
                        check account and the customer's checking account is debited by the system
                        within 48 hours, then..
                    <br />
                    <br />
                    Once the funds have "cleared" successfully, the funds are transmitted (by Electronic
                        check) into the merchant's regular checking account within 48 hours.
                    <br />
                    <br />
                </span><a class="Link" href="#00" target="_self" onmouseover='status=" Back to the top ";return true;'
                    onmouseout='status=""; return true;'><span style="font-size: 8pt"><span style="font-family: Arial;">
                        Back to the top</span> </span></a>
                <hr noshade="noshade" size="1" />
            </td>
        </tr>
        <tr>
            <td align="left" valign="middle" colspan="4">
                <a class="Link" name="WhatifthecustomerhasInsufficient"></a><span style="font-family: Arial;
                    font-size: 10pt;">
                    <b>What If The Customer Has "Insufficient Funds" (NSF)?</b>
                </span>
            </td>
        </tr>
        <tr>
            <td align="left" valign="middle" colspan="4">
                <span style="font-family: Arial; font-size: 10pt;">
                    <br />
                    The risks associated with accepting Electronic checks are no different than those
                    associated with accepting credit cards. The funds from an Electronic check transaction
                    remain in your Electronic check account until the funds have cleared
                    or until the transaction is "returned" or "charged back". The system
                    will report/inform a merchant of a returned transaction and the cause thereof usually
                    within 2 to 3 days of the original transaction date. The two most common reasons
                    a transaction is returned are Insufficient Funds (NSF) or Invalid Acct (the bank
                    account number and routing number were typed incorrectly). Please note that a returned
                    transaction will cause a returned item fee to the merchant, which is usually $3.00
                    per item. It is a good idea to charge a customer a returned check fee for returned
                    transactions just like you would for returned checks. The typical fee to a customer
                    for a returned check or Electronic check transaction is $15.00. You must disclose
                    your returned check/returned Electronic check policy to your customer at the time
                    of sale.
                    <br />
                    <br />
                </span><a class="Link" href="#00" target="_self" onmouseover='status=" Back to the top ";return true;'
                    onmouseout='status=""; return true;'><span style="font-size: 8pt"><span style="font-family: Arial;">
                        Back to the top</span> </span></a>
                <hr noshade="noshade" size="1" />
            </td>
        </tr>
        <tr>
            <td align="left" valign="middle" colspan="4">
                <a class="Link" name="Electronicchargeback"></a><span style="font-family: Arial; font-size: 10pt;">
                    <b>What About Electronic Check Chargebacks?</b>
                </span>
            </td>
        </tr>
        <tr>
            <td align="left" valign="middle" colspan="4">
                <span style="font-family: Arial; font-size: 10pt;">
                    <br />
                    Remember also that a customer has basically the same rights to chargeback an Electronic
                    check transaction as they do a credit card transaction. You may be required to provide
                    proof of authorization or return the funds to the merchant. In the event of a chargeback,
                    the customer's bank will chargeback the funds immediately and it is the burden of
                    the merchant to seek recourse for the funds, as with credit card chargebacks. If
                    your customer feels that they have been charged erroneously or without their authorization
                    they may dispute this directly through their bank with a chargeback. You should
                    encourage your customers to contact you if they have any problems or concerns with
                    their order, and you should properly disclose your refund policy at the time of
                    sale to avoid potential chargebacks. It is also a good idea to store receipts and
                    other records or proof of service or delivery for transactions involving credit
                    card or Electronic check payments. For merchants that conduct their business carefully
                    and with adequate customer service, chargebacks should not be a problem, as most
                    customers will be willing to resolve any disputes they have directly with the merchant.
                    <br />
                    <br />
                </span><a class="Link" href="#00" target="_self" onmouseover='status=" Back to the top ";return true;'
                    onmouseout='status=""; return true;'><span style="font-size: 8pt"><span style="font-family: Arial;">
                        Back to the top</span> </span></a>
                <hr noshade="noshade" size="1" />
            </td>
        </tr>
        <tr>
            <td align="left" valign="middle" colspan="4">
                <a class="Link" name="Electronicchecktransactions"></a><span style="font-family: Arial;
                    font-size: 10pt;">
                    <b>Electronic Check Transactions Are NOT Processed In Real-Time</b>
                </span>
            </td>
        </tr>
        <tr>
            <td align="left" valign="middle" colspan="4">
                <span style="font-family: Arial; font-size: 10pt;">
                    <br />
                    The Electronic check system is not set-up for real-time processing, involving immediate
                    authorizations and instantaneous responses like the credit card networks are. An
                    Electronic check transaction is not a guarantee of funds until the customer's bank
                    account has been successfully debited, which takes at least 48 hours from the time
                    of the transaction. Therefore, it is NOT advisable to accept Electronic checks as
                    a form of payment for a product or service that you provide immediately at the time
                    of sale. It IS, however, entirely appropriate as a form of payment for a product
                    or service that you "deliver" or that is a recurring service. Many customers prefer
                    to have their checking account debited for a monthly subscription service. Others
                    may not have a credit card but want to purchase a product for delivery right from
                    your web site or over the phone using their checking account. Indicating to the
                    customer to "allow 2 to 3 weeks for delivery" would give you ample time to assure
                    that the funds have cleared prior to shipment of goods.
                    <br />
                    <br />
                </span><a class="Link" href="#00" target="_self" onmouseover='status=" Back to the top ";return true;'
                    onmouseout='status=""; return true;'><span style="font-size: 8pt"><span style="font-family: Arial;">
                        Back to the top</span> </span></a>
                <hr noshade="noshade" size="1" />
            </td>
        </tr>
        <tr>
            <td align="left" valign="middle" colspan="4">
                <a class="Link" name="ProcessingElectroniccheck"></a><span style="font-family: Arial;
                    font-size: 10pt;">
                    <b>Processing Electronic Check Transactions With </b></span><span style="font-family: Arial;
                        font-size: 10pt;"><b>e-</b></span><span style="font-family: Arial; font-size: 10pt;"><b>Commerce</b></span></td>
        </tr>
        <tr>
            <td align="left" valign="middle" colspan="4">
                <span style="font-family: Arial; font-size: 10pt;">
                    <br />
                    If you have a Electronic check account, the following features
                    are available to help you perform Electronic check transactions and to assist you
                    in managing your Electronic check transaction activity.
                    <br />
                    <br />
                    Virtual Terminal - (it is expanded to allow Electronic check transactions)
                    <br />
                    <br />
                    WebLink - (it is expanded to allow Electronic check transactions)
                    <br />
                    <br />
                    Electronic check Activity Menu - (the main Electronic check reporting menu)
                    <br />
                    <br />
                </span><a class="Link" href="#00" target="_self" onmouseover='status=" Back to the top ";return true;'
                    onmouseout='status=""; return true;'><span style="font-size: 8pt"><span style="font-family: Arial;">
                        Back to the top</span> </span></a>
                <hr noshade="noshade" size="1" />
            </td>
        </tr>
        <tr>
            <td align="left" valign="middle" colspan="4">
                <a class="Link" name="VirtualTerminaltoprocessElectronicchecks"></a><span style="font-family: Arial;
                    font-size: 10pt;">
                    <b>Using Virtual Terminal To Process Electronic Check Transactions</b>
                </span>
            </td>
        </tr>
        <tr>
            <td align="left" valign="middle" colspan="4">
                <span style="font-family: Arial; font-size: 10pt;">
                    <br />
                    For the basics on using Virtual Terminal see the section entitled &quot;
                    Virtual Terminal" above. Once your account is configured for Electronic
                    check acceptance, you will see an additional payment option call "Checking Account"
                    in the list of available payment methods. Three additional fields will also be visible:
                    "Bank name", "Account Number" and "ABA Code". The blue help prompts on the Virtual
                    Terminal window will assist you in providing the right information.
                    <br />
                    <br />
                </span><a class="Link" href="#00" target="_self" onmouseover='status=" Back to the top ";return true;'
                    onmouseout='status=""; return true;'><span style="font-size: 8pt"><span style="font-family: Arial;">
                        Back to the top</span> </span></a>
                <hr noshade="noshade" size="1" />
            </td>
        </tr>
        <tr>
            <td align="left" valign="middle" colspan="4">
                <a class="Link" name="WebLinktoacceptElectroniccheck"></a><span style="font-family: Arial;
                    font-size: 10pt;">
                    <b>Using WebLink To Accept Electronic Check From Your Web Site</b>
                </span>
            </td>
        </tr>
        <tr>
            <td align="left" valign="middle" colspan="4">
                <span style="font-family: Arial; font-size: 10pt;">Complete details on how to link your
                    web site to WebLink are provided in the section entitled &quot;
                    WebLink HTML Help". Please review the Electronic check overview for a complete understanding
                    of when it is appropriate to accept Electronic checks from customers directly on
                    your web site.
                    <br />
                    <br />
                </span><a class="Link" href="#00" target="_self" onmouseover='status=" Back to the top ";return true;'
                    onmouseout='status=""; return true;'><span style="font-size: 8pt"><span style="font-family: Arial;">
                        Back to the top</span> </span></a>
            </td>
        </tr>
        <tr>
            <td align="left" valign="middle" colspan="4">
                <a class="Link" name="ElectroniccheckActivityMenu"></a><span style="font-family: Arial;
                    font-size: 10pt;">
                    <b>Using The Electronic Check Activity Menu</b>
                </span>
            </td>
        </tr>
        <tr>
            <td align="left" valign="middle" colspan="4">
                <span style="font-family: Arial; font-size: 10pt;">The system provides excellent
                    reporting and Electronic check account management information to help you keep ahead
                    in your business. The Electronic check Activity Menu is actually very much like
                    an actual bank statement, where you see one statement for eElectronic check period
                    with a previous balance and ending balance for that period. However, unlike a bank
                    statement, there are links to more details right from the Electronic check statement,
                    and you can change the beginning and ending date of the statement in order to see
                    more or less information according to your needs.
                    <br />
                    <br />
                    A sample Electronic check Activity statement appears on the following page.
                    <br />
                    <br />
                    Whenever there is activity with your account, it is reported immediately on your
                    Electronic check statement. Your Electronic check statement is constantly being
                    updated by the system with current information pertaining to your Electronic
                    check account. The following activities are reported on the Electronic check Activity
                    statement:
                    <br />
                    <br />
                </span><a class="Link" href="#00" target="_self" onmouseover='status=" Back to the top ";return true;'
                    onmouseout='status=""; return true;'><span style="font-size: 8pt"><span style="font-family: Arial;">
                        Back to the top</span> </span></a>
                <hr noshade="noshade" size="1" />
            </td>
        </tr>
        <tr>
            <td align="center" valign="middle" colspan="4">
                <img src="images/vt-activity.gif" width="380" height="522" border="0">
            </td>
        </tr>
        <tr>
            <td align="left" valign="middle" colspan="4">
                <span style="font-family: Arial; font-size: 10pt;">
                    <br />
                    Submitted Items - (these are the items submitted to the system either by
                    you from Virtual Terminal or from your web site through WebLink. The total is the
                    amount that has been transferred from your customers' accounts into your
                    Electronic check account) You can click the link to view the individual transaction
                    details of the submitted items.
                    <br />
                    <br />
                    Returned Items - (such as insufficient funds and chargeback items) You can
                    click the link to view the individual transaction details of the returned items.
                    <br />
                    <br />
                    Funds transfer - (for funds that are being transferred from your
                    Electronic check account into your regular checking account)
                    <br />
                    <br />
                    Bank fees - (for miscellaneous fees associated with your Electronic
                    check account, such as returned item fees)
                    <br />
                    <br />
                    If you have questions about your Electronic check Activity statement and transactions
                    please contact technical support using the "Merchant Support" feature on the Merchant
                    Menu.
                    <br />
                    <br />
                </span><a class="Link" href="#00" target="_self" onmouseover='status=" Back to the top ";return true;'
                    onmouseout='status=""; return true;'><span style="font-size: 8pt"><span style="font-family: Arial;">
                        Back to the top</span> </span></a>
                <hr noshade="noshade" size="1" />
            </td>
        </tr>
        <tr>
            <td align="left" valign="middle" colspan="4">
                <a class="Link" name="Electroniccheckaccount"></a><span style="font-family: Arial;
                    font-size: 10pt;">
                    <br />
                    <b>How Do I Obtain An Electronic Check Account?</b>
                    <br />
                </span>
            </td>
        </tr>
        <tr>
            <td align="left" valign="middle" colspan="4">
                <span style="font-family: Arial; font-size: 10pt;">Click on <b>"APPLY"</b> or
                    <b>"INFO"</b> above to get additional information or to obtain an account.
                    <br />
                    <br />
                </span><a class="Link" href="#00" target="_self" onmouseover='status=" Back to the top ";return true;'
                    onmouseout='status=""; return true;'><span style="font-size: 8pt"><span style="font-family: Arial;">
                        Back to the top</span> </span></a>
                <hr noshade="noshade" size="1" />
            </td>
        </tr>
        <tr>
            <td align="center" valign="bottom" colspan="4">
                <br />
                <br />
                <a class="Link" href="javascript:window.history.back();" onmouseover='status=" Back to previous page "; return true;'
                    onmouseout='status=" ";'>
                    <img src="images/back-on.gif" name="back" width="75" height="25" alt="" border="0"></a>
                <br />
                <br />
            </td>
        </tr>
    </table>
</asp:Content>
