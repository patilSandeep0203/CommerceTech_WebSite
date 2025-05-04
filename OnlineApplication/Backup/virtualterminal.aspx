<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="virtualterminal.aspx.cs" Inherits="virtualterminal" Title="Credit Card Processing, Merchant Services, Payment Processing"
    Theme="Admin" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server" TargetControlID="pnlVT"
        ExpandControlID="pnlVirtualTerminal" CollapseControlID="pnlVirtualTerminal" Collapsed="True"
        TextLabelID="lblShowDetails" ExpandedText="(Hide)" CollapsedText="(Show)" ImageControlID="imgShowDetails"
        ExpandedImage="images/collapse_blue.jpg" CollapsedImage="images/expand_blue.jpg"
        SuppressPostBack="true" />
    <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender2" runat="server" TargetControlID="pnlWhat"
        ExpandControlID="pnlWhatHappens" CollapseControlID="pnlWhatHappens" Collapsed="True"
        TextLabelID="lblShowWhatHappens" ExpandedText="(Hide)" CollapsedText="(Show)"
        ImageControlID="imgShowWhatHappens" ExpandedImage="images/collapse_blue.jpg"
        CollapsedImage="images/expand_blue.jpg" SuppressPostBack="true" />
    <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender3" runat="server" TargetControlID="pnlConfig"
        ExpandControlID="pnlConfiguration" CollapseControlID="pnlConfiguration" Collapsed="True"
        TextLabelID="lblShowConfiguration" ExpandedText="(Hide)" CollapsedText="(Show)"
        ImageControlID="imgShowConfiguration" ExpandedImage="images/collapse_blue.jpg"
        CollapsedImage="images/expand_blue.jpg" SuppressPostBack="true" />
    <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender4" runat="server" TargetControlID="pnlOverview"
        ExpandControlID="pnlVTOverview" CollapseControlID="pnlVTOverview" Collapsed="false"
        TextLabelID="lblOverview" ExpandedText="(Hide)" CollapsedText="(Show)" ImageControlID="imgOverview"
        ExpandedImage="images/collapse_blue.jpg" CollapsedImage="images/expand_blue.jpg"
        SuppressPostBack="true" />
    <br />
    <a name="#00"></a>
    <table width="95%" cellspacing="0" cellpadding="5" border="0">
        <!--<tr>
        <td align="right" valign="middle">
            <img src="images/obullet.gif" height="8" width="8" border="0"></td>
        <td align="left" valign="top">
            <a href="#VirtualTerminalOverview" class="Link" onmouseover='status=" Virtual Terminal Overview ";return true;'
                onmouseout='status=""; return true;'><b>Virtual Terminal Overview</b></a>
        </td>
        </tr>
        <tr>
            <td align="right" valign="middle">
                <img src="images/obullet.gif" height="8" width="8" border="0"></td>
            <td align="left" valign="top">
                <a class="Link" href="#HowtouseVirtualTerminal" onmouseover='status=" How to use Virtual Terminal ";return true;'
                    onmouseout='status=""; return true;'><b>How to use Virtual Terminal</b></a>
            </td>
        </tr>
        <tr>
            <td align="right" valign="middle">
                <img src="images/obullet.gif" height="8" width="8" border="0"></td>
            <td align="left" valign="top">
                <a class="Link" href="#Whatactuallyhappenswhenatransaction" onmouseover='status=" What actually happens when a transaction is submitted from Virtual Terminal ";return true;'
                    onmouseout='status=""; return true;'><b>What actually happens when a transaction is
                        submitted from Virtual Terminal</b></a>
            </td>
        </tr>
        <tr>
            <td align="right" valign="middle">
                <img src="images/obullet.gif" height="8" width="8" border="0"></td>
            <td align="left" valign="top">
                <a class="Link" href="#ConfigurationOptionsforVirtualTermin" onmouseover='status=" Configuration Options for Virtual Terminal ";return true;'
                    onmouseout='status=""; return true;'><b>Configuration Options for Virtual Terminal</b></a>
            </td>
        </tr>
        <tr>
            <td align="center" valign="middle" colspan="2">
                <img src="images/qc-hr.gif" height="10" width="375" border="0">
            </td>
        </tr>
        -->
        <tr>
            <td align="left" valign="middle" colspan="4">
                <a name="#VirtualTerminalOverview"></a>
                <asp:Panel ID="pnlVTOverview" runat="server" Height="20px">
                    <div style="cursor: pointer; vertical-align: middle; width: 700px;
                        height: 20px;" class="DivHeader">
                        <div style="float: left; text-align: center; margin-left: 0px;">
                            <asp:Label ID="lblVTOverview" runat="server" Font-Bold="True" Font-Names="Arial"
                                ForeColor="White" Font-Size="Small" Text="Virtual Terminal Overview"></asp:Label>
                        </div>
                        <div style="float: left; margin-left: 20px;">
                            <asp:Label ID="lblOverview" runat="server" Font-Bold="True" Font-Names="Arial" ForeColor="White"
                                Font-Size="8pt">(Show)</asp:Label>
                        </div>
                        <div style="float: right; vertical-align: middle;">
                            <asp:Image ID="imgOverview" runat="server" ImageUrl="images/expand_blue.jpg" /></div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlOverview" runat="server" Width="700px">
                    <table class="Border">
                        <tr>
                            <td align="left" valign="middle" colspan="2">
                                <span class="LabelsBody">The Virtual Terminal should be used for manual entry of electronic
                                    check and credit card transactions on the e-Commerce system. Some of the reasons
                                    you might need to use Virtual Terminal are:</span>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle">
                            <ul class="Bullet">
                                <li style="padding-top:5px" class="LabelsBody">You received an order by phone, fax, or mail and need to process
                                    the transaction</li>
                                <li style="padding-top:5px" class="LabelsBody">You need to perform a credit transaction according to your own
                                    refund policy</li>
                                <li style="padding-top:5px" class="LabelsBody">You previously received a voice authorization by phone and now
                                    need to submit the transaction to be captured. (Also called a Post-Authorization)</li>
                            </ul>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="left">
                                <span class="LabelsBody">As you can see, the Virtual Terminal allows you to do many different
                                    kinds of transactions, including credits/returns (for refunding money to a customer's
                                    credit card), authorization only, post auth/ticket only, and of course a normal
                                    authorization. You don't need to have a web site to use Virtual Terminal. Simply
                                    login to e-Commerce and choose Virtual Terminal on the Merchant Menu.</span>
                                <br />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <br />
                <center>
                    <div style="width: 70%" class="DivGreen">
                        <span class="LabelsRed"><b>You can click on the following links to the Authorize.Net
                            test accounts</b></span>
                        <br />
                        <a class="Link" target="_blank" href="https://test.authorize.net/">(Test Account Login:
                            imactest Password: im13test)</a>
                        <br />
                        <a class="Link" target="_blank" href="https://anet.vpos.authorize.net/">(VPOS Test Account
                            -- Login: jamesvpostest915 Password: 492test84057)</a>
                    </div>
                </center>
            </td>
        </tr>
        <tr>
            <td align="center" valign="middle" colspan="4">
                <asp:UpdatePanel ID="UpdatePanelExample" runat="server">
                    <ContentTemplate>
                        <asp:LinkButton ID="lnkbtnExample" Font-Bold="true" CssClass="LabelsRed"
                            Font-Size="small" runat="server" OnClick="lnkbtnExample_Click">View Example</asp:LinkButton>
                        <asp:Panel ID="pnlExample" runat="server" Visible="false">
                            <table border="0" cellpadding="0" cellspacing="2" class="DivGreen">
                                <tr>
                                    <td align="center" colspan="2">
                                        <span class="LabelsHeader">
                                            <img src="images/ecommerce1.gif" height="75" width="350" border="0" alt="e-Commerce">
                                            <br />
                                            <b>Virtual Terminal<br />
                                            </b></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <span class="LabelsHeader"><b>Order Information</b> </span>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <span class="LabelsHeader"><b>Invoice #</b> </span>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="TextBox1" runat="server">123456</asp:TextBox><span class="LabelsHeader"><b>(Numbers
                                            only)</span></b>
                                    </td>
                                </tr>
                                <tr style="color: #000000">
                                    <td align="right">
                                        <span class="LabelsHeader"><b>Description</b> </span>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="TextBox2" runat="server">Test Transaction</asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <span class="LabelsHeader"><b>Amount</b></span>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="TextBox3" runat="server" Width="50px">100.00</asp:TextBox><span
                                            class="LabelsHeader"><b>(Include decimal point and 2 digits for cents)</b></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <span class="LabelsHeader"><b>Payment Method</b> </span>
                                    </td>
                                    <td align="left">
                                        <select name="METHOD" size="1">
                                            <option value="ACH">
                                            Checking Account (USA Only)
                                            <option selected>Visa</option>
                                            <option>MasterCard</option>
                                            <option>American Express</option>
                                            <option>Discover</option>
                                        </select>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <span class="LabelsHeader"><b>Transaction Type</b></span>
                                    </td>
                                    <td align="left">
                                        <input type="radio" checked name="type" value="NA"><span class="LabelsHeader"><b>Normal
                                            Transaction</b></span>
                                        <br />
                                        <input type="radio" name="type" value="AO"><span class="LabelsHeader"><b>Authorization
                                            Only</b></span><br />
                                        <input type="radio" name="type" value="CR"><span class="LabelsHeader"><b>Credit/Return</b></span>
                                        <span class="LabelsHeader"><b>&nbsp; - &nbsp;(Used for refunds)</b> </span>
                                        <br />
                                        <input type="radio" name="type" value="PA"><span class="LabelsHeader"><b>Post Auth/Ticket
                                            Only</b></span><span class="LabelsHeader"><b>&nbsp; - &nbsp;(Auth Code required below)</b></span>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <span class="LabelsHeader"><b>Credit Card Information</b> </span>
                                    </td>
                                </tr>
                                <tr style="color: #000000">
                                    <td align="right">
                                        <span class="LabelsHeader"><b>Card Number</b> </span>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="TextBox4" runat="server">4111111111111111</asp:TextBox><span style="font-family: Arial">
                                        <span class="LabelsHeader"><b>&nbsp;(No spaces or hyphens)</b></span>
                                    </td>
                                </tr>
                                <tr style="font-size: 12pt; font-family: Times New Roman">
                                    <td align="right">
                                        <span class="LabelsHeader"><b>Exp. Date</b> </span>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="TextBox5" runat="server">01/05/2007</asp:TextBox>
                                        <span class="LabelsHeader"><b>&nbsp;(mmyy or mm/yy)</b></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <span class="LabelsHeader"><b>Auth. Code</b></span>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox><span style="font-size: 8pt"><span
                                            style="font-family: Arial"><strong> </strong><span class="LabelsHeader"><b>&nbsp;(For
                                                Post Auth/Ticket Only transactions)</b></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <span class="LabelsHeader"><b>Checking Account Information<br />
                                            (if payment method is by Checking Account)<br />
                                        </b></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <span class="LabelsHeader"><b>Bank Name</b></span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox7" runat="server" />
                                        <span class="LabelsHeader"><b>&nbsp;(USA Banks only)</b></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <span class="LabelsHeader"><b>Account Number</b></span>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox>
                                        <span class="LabelsHeader"><b>&nbsp;(All numeric - no spaces)</b></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <span class="LabelsHeader"><b>ABA Routing Code</b> </span>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="TextBox9" runat="server">123456789</asp:TextBox>
                                        <span class="LabelsHeader"><b>( 9 digit number between the colons on check )</b></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <span class="LabelsHeader"><b>Customer Information</b></span> </span>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <span class="LabelsHeader"><b>Customer ID</b> </span>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="TextBox10" runat="server">Online Customer</asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <span class="LabelsHeader"><b>Name</b> </span>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="TextBox11" runat="server">John Doe</asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <span class="LabelsHeader"><b>Address</b> </span>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="TextBox12" runat="server">123 West 110 North</asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <span class="LabelsHeader"><b>City</b> </span>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="TextBox13" runat="server">Anywhere</asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <span class="LabelsHeader"><b>State</b> </span>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="TextBox14" runat="server" Width="20px">CA</asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <span class="LabelsHeader"><b>Zip</b> </span>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="TextBox15" runat="server">12345-1234</asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <span class="LabelsHeader"><b>Country</b> </span>
                                    </td>
                                    <td align="left">
                                        <select name="COUNTRY" size="1">
                                            <option>USA</option>
                                            <option>OTHER</option>
                                            <option>Argentina</option>
                                            <option>Australia</option>
                                            <option>Austria</option>
                                            <option>Belgium</option>
                                            <option>Bermuda</option>
                                            <option>Brazil</option>
                                            <option>Canada</option>
                                            <option>Chile</option>
                                            <option>China</option>
                                            <option>Colombia</option>
                                            <option>Czech Republic</option>
                                            <option>Denmark</option>
                                            <option>Fiji</option>
                                            <option>Finland</option>
                                            <option>France</option>
                                            <option>Germany</option>
                                            <option>Greece</option>
                                            <option>Guatemala</option>
                                            <option>Honduras</option>
                                            <option>Hong Kong</option>
                                            <option>Iceland</option>
                                            <option>India</option>
                                            <option>Ireland</option>
                                            <option>Israel</option>
                                            <option>Italy</option>
                                            <option>Japan</option>
                                            <option>Luxembourg</option>
                                            <option>Mexico</option>
                                            <option>Netherlands</option>
                                            <option>New Zealand</option>
                                            <option>Nicaragua</option>
                                            <option>North Korea</option>
                                            <option>Norway</option>
                                            <option>Paraguay</option>
                                            <option>Peru</option>
                                            <option>Phillipines</option>
                                            <option>Poland</option>
                                            <option>Portugal</option>
                                            <option>South Africa</option>
                                            <option>South Korea</option>
                                            <option>Spain</option>
                                            <option>Sweden</option>
                                            <option>Switzerland</option>
                                            <option>Taiwan</option>
                                            <option>Thailand</option>
                                            <option>Turkey</option>
                                            <option>UK</option>
                                            <option>Uruguay</option>
                                            <option>Venezuela</option>
                                        </select>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <span class="LabelsHeader"><b>Phone</b> </span>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="TextBox16" runat="server">(801) 818 3311</asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <span class="LabelsHeader"><b>Fax</b> </span>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="TextBox17" runat="server">(801) 818 3312</asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <span class="LabelsHeader"><b>Email</b> </span>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="TextBox18" runat="server">test@test.com</asp:TextBox></td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="lnkbtnExample" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td align="left" valign="middle" colspan="4">
                <a class="LinkSmall" href="#00" target="_self" onmouseover='status=" Back to the top ";return true;'
                    onmouseout='status=""; return true;'>Back to the top</a>
            </td>
        </tr>
        <tr>
            <td align="left" valign="middle" colspan="4">
                <a name="#HowtouseVirtualTerminal"></a>
                <asp:Panel ID="pnlVirtualTerminal" runat="server" Height="20px">
                    <div style="cursor: pointer; vertical-align: middle; width: 700px;height: 20px;" class="DivHeader">
                        <div style="float: left; text-align: center; margin-left: 0px;">
                            <asp:Label ID="lblHowTo" runat="server" Font-Bold="True" Font-Names="Arial" ForeColor="White"
                                Font-Size="Small" Text="How to use Virtual Terminal"></asp:Label>
                        </div>
                        <div style="float: left; margin-left: 20px;">
                            <asp:Label ID="lblShowDetails" runat="server" Font-Bold="True" Font-Names="Arial"
                                ForeColor="White" Font-Size="8pt">(Show)</asp:Label>
                        </div>
                        <div style="float: right; vertical-align: middle;">
                            <asp:Image ID="imgShowDetails" runat="server" ImageUrl="images/expand_blue.jpg" /></div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlVT" runat="server" Width="700px" CssClass="Border">
                    <span class="LabelsBody">To use Virtual Terminal, simply fill in the required fields and
                        submit the transaction by pressing the "Submit" button. You will receive an immediate
                        response from the e-Commerce system. The Virtual Terminal fields are described below:
                        <br />
                        <br />
                        <b>Invoice</b> - This is your own invoice number. The value of this field must be
                        all numbers. The invoice number will appear on the transaction receipt that your
                        customer receives as well as on the merchant's copy of the receipt.
                        <br />
                        <br />
                        <b>Description</b> - Enter any description you want here. The description will appear
                        on the transaction receipt that your customer receives as well as on the merchant's
                        copy of the receipt.
                        <br />
                        <br />
                        <b>Amount</b> - This is the transaction amount. You must include the decimal point
                        plus 2 digits after the decimal for the cents. You may not use a negative or zero
                        amount. The amount will appear on the transaction receipt that your customer receives
                        as well as on the merchant's copy of the receipt.
                        <br />
                        <br />
                        <b>Payment Method</b> - Select the appropriate payment method. For your information,
                        all Visa credit cards begin with " 4 ", MasterCard credit cards begin with " 5 ",
                        American Express credit cards begin with " 3 ", and Discover/Novus credit cards
                        begin with " 6 ". For ACH/Electronic check transactions, the customer's checking
                        account must be a U.S. checking account.
                        <br />
                        <br />
                        <b>Transaction Type</b> - For credit card transactions, choose one of four transaction
                        types:
                        <br />
                        <br />
                        <ul>
                            <u>Normal Authorization</u> - for regular transactions to charge a customer's credit
                            card. The transaction will be immediately authorized and will be included in the
                            day's settlement. This is the most common transaction type.
                            <br />
                            <br />
                            <u>Authorization Only</u> (Auth-Only)- to authorize a credit card transaction. An
                            Auth-Only transaction is NOT settled in the day's settlement. Use Auth-Only if you
                            are not shipping the order right away. In that case, you want the transaction authorized
                            so that you can make the commitments you need to make in order to prepare/ship the
                            order to the customer. Once the order has finally shipped (must be within 30 days
                            of authorization), you can include the transaction in that day's settlement. See
                            the subheading "Auth-Only Transactions" under the "Credit Card Activity" section
                            below for details on how to settle Auth- Only transactions.
                            <br />
                            <br />
                            <b>NOTE:</b> You can also use the credit card voice authorization network provided
                            directly by your merchant provider to obtain an authorization code for an Auth-Only
                            transaction. To use the voice authorization network, you use a touch tone telephone
                            to call a special access phone number, type in the details of the transaction you
                            want authorized, and you receive an authorization code as a response. In this case,
                            because you didn't use e-Commerce to obtain the authorization for the Auth-Only
                            transaction, you must SAVE the authorization code in order to finish the transaction
                            later when the order is shipped to the customer. See Post Auth/Ticket Only below.
                            <br />
                            <br />
                            <u>Credit/Return</u> - to issue a credit (refund) back to a customer. Always issue
                            credits to the same credit card that was originally charged. Be sure that your refund
                            policy is clearly disclosed to customer before they make a purchase from you, to
                            avoid potential chargeback disputes in the future.
                            <br />
                            <br />
                            <u>Post Auth/Ticket Only</u> - this transaction type should only be used if you
                            previously obtained an authorization code for a transaction using the voice authorization
                            network provided by your bank or merchant provider. Choose this type if you have
                            shipped the order and are now ready to settle the already authorized transaction.
                            The authorization code you received from the voice authorization system is required.
                            <br />
                            <br />
                            <u>Void</u> - You cannot void a transaction from Virtual Terminal. To void a transaction,
                            see the subheading "Voiding Transactions" in the "Credit Card Activity" section
                            below.
                        </ul>
                        <br />
                        <br />
                        <b>Card Number</b> - This is the credit card number of the cardholder making the
                        purchase. For your information, a Visa credit card number is either 13 or 16 digits,
                        a MasterCard credit card number is 16 digits, an American Express credit card number
                        is 15 digits, and a Discover/Novus credit card number is 16 digits. Do NOT use spaces
                        or hyphens when typing the card number. The card number will appear only on the
                        merchant's copy of the receipt.
                        <br />
                        <br />
                        <b>Exp. Date</b> - You must provide the expiration date of the credit card for all
                        transactions. Type the expiration date in the form mm/yy or mmyy. For cards that
                        expire in the year 2000 or 2001, use 00 or 01 for the year, respectively. The expiration
                        date will appear only on the merchant's copy of the receipt.
                        <br />
                        <br />
                        <b>Bank Name</b> - (Only visible for merchants that are configured to accept ACH/Electronic
                        Checks). This is the name of the checking account holder's bank. This must be a
                        bank that is established in the U.S. and that uses U.S. currency. The bank name
                        will appear only on the merchant's copy of the receipt.
                        <br />
                        <br />
                        <b>Account Number</b> - (Only visible for merchants that are configured to accept
                        ACH/Electronic Checks). This is the checking account number of the account holder.
                        The account number can be found at the bottom of a check. Don't mistake the account
                        number for the routing number or the check number, both of which are also found
                        at the bottom of a check. Do NOT use spaces or other characters; just the numbers.
                        A bank account number cannot be more than 17 digits. The account number will appear
                        only on the merchant's copy of the receipt.
                        <br />
                        <br />
                        <b>ABA Routing Code</b> - (Only visible for merchants that are configured to accept
                        ACH/Electronic Checks). This is the Transit/Routing Code for the bank of the account
                        holder. It must be 9 digits and can be found at the bottom of a check and is usually
                        between a pair of colons (<b>:</b>). Don't mistake the ABA routing code for the
                        account number or check number, both of which are also found at the bottom of a
                        check. Do NOT use spaces or other characters; just the numbers. The ABA routing
                        code will appear only on the merchant's copy of the receipt.
                        <br />
                        <br />
                        <b>Customer ID</b> - This is the ID you use for your customer. If you don't use
                        customer ID's in your business, consider using the customer's last name here, or
                        the text "Internet customer". The customer ID will appear on the transaction receipt
                        that your customer receives as well as on the merchant's copy of the receipt.
                        <br />
                        <br />
                        <b>Name</b> - This should be the cardholder's name. The name will appear on the
                        transaction receipt that your customer receives as well as on the merchant's copy
                        of the receipt.
                        <br />
                        <br />
                        <b>Address</b> - This should be the cardholder's address. The address field is required
                        for proper use of the Address Verification Service (AVS). A valid AVS response cannot
                        be provided without the address. For more information about AVS, see the section
                        above entitled "What is Address Verification Service (AVS) and how do I use it?".
                        The address will appear on the transaction receipt that your customer receives as
                        well as on the merchant's copy of the receipt.
                        <br />
                        <br />
                        <b>City</b> - This should be the cardholder's city. The city will appear on the
                        credit card transaction receipt that your customer receives as well as on the merchant's
                        copy of the receipt.
                        <br />
                        <br />
                        <b>State</b> - This should be the cardholder's state. For non-U.S. customers, use
                        whatever is appropriate in this field. The state will appear on the transaction
                        receipt that your customer receives as well as on the merchant's copy of the receipt.
                        <br />
                        <br />
                        <b>Zip</b> - This should be the cardholder's zip code. The zip field is required
                        for proper use of the Address Verification Service (AVS). A valid AVS response cannot
                        be provided without the zip code. For more information about AVS, see the section
                        above entitled "What is Address Verification Service (AVS) and how do I use it?".
                        For non-U.S. customers, use whatever is appropriate in this field. The zip code
                        will appear on the transaction receipt that your customer receives as well as on
                        the merchant's copy of the receipt.
                        <br />
                        <br />
                        <b>Country</b> - This should be the cardholder's country. Choose "Other" for countries
                        not found in the list. The country will appear on the transaction receipt that your
                        customer receives as well as on the merchant's copy of the receipt.
                        <br />
                        <br />
                        <b>Phone</b> - This should be the cardholder's phone number. You may use numbers
                        and characters in this field. The phone number will appear on the transaction receipt
                        that your customer receives as well as on the merchant's copy of the receipt.
                        <br />
                        <br />
                        <b>Fax</b> - This should be the cardholder's fax number. You may use numbers and
                        characters in this field. The fax number will appear on the transaction receipt
                        that your customer receives as well as on the merchant's copy of the receipt.
                        <br />
                        <br />
                        <b>E-mail</b> - Provide the customer's E-mail address here. If e-Commerce has been
                        configured to send a credit card transaction receipt by E-mail on your behalf, this
                        is the E-mail address that will be used for the customer. The E-mail address will
                        appear on the transaction receipt that your customer receives as well as on the
                        merchant's copy of the receipt.
                        <br />
                        <br />
                    </span>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td align="left" valign="middle" colspan="4">
                <a class="LinkSmall" href="#00" target="_self" onmouseover='status=" Back to the top ";return true;'
                    onmouseout='status=""; return true;'><span style="font-family: Arial; font-size: 8pt">
                        Back to the top</span> </a>
            </td>
        </tr>
        <tr>
            <td align="left" valign="middle" colspan="4">
                <a name="#Whatactuallyhappenswhenatransaction"></a>
                <asp:Panel ID="pnlWhatHappens" runat="server" Height="20px">
                    <div style="cursor: pointer; vertical-align: middle; width: 700px;height: 20px;" class="DivHeader">
                        <div style="float: left; text-align: center; margin-left: 0px;">
                            <asp:Label ID="lblWhatHappens" runat="server" Font-Bold="True" Font-Names="Arial"
                                ForeColor="White" Font-Size="Small" Text="What actually happens when a transaction is submitted from Virtual Terminal"></asp:Label>
                        </div>
                        <div style="float: left; margin-left: 20px;">
                            <asp:Label ID="lblShowWhatHappens" runat="server" Font-Bold="True" Font-Names="Arial"
                                ForeColor="White" Font-Size="8pt">(Show)</asp:Label>
                        </div>
                        <div style="float: right; vertical-align: middle;">
                            <asp:Image ID="imgShowWhatHappens" runat="server" ImageUrl="images/expand_blue.jpg" /></div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlWhat" runat="server" Width="700px">
                    <table class="Border">
                        <tr>
                            <td align="left" valign="middle" colspan="4">
                                <span class="LabelsBody">When a transaction is submitted from Virtual Terminal, this is
                                    exactly what happens: </span>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="width: 3%">
                                <img src="images/bullet.gif" height="8" width="8" border="0" alt="bp"/></td>
                            <td align="left" valign="top">
                                <span class="LabelsBody">The transaction fields are verified and checked for errors.</span>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle">
                                <img src="images/bullet.gif" height="8" width="8" border="0" alt="bp"/></td>
                            <td align="left" valign="top">
                                <span class="LabelsBody">The transaction is immediately transmitted to the credit card processing
                                    network for authorization.
                                    <br />
                                    (For credit card transactions only)</span>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle">
                                <img src="images/bullet.gif" height="8" width="8" border="0" alt="bp"/></td>
                            <td align="left" valign="top">
                                <span class="LabelsBody">A response from the processing network is received and the transaction
                                    is stored in the merchant's current batch awaiting settlement.<br />
                                    (For credit card transactions only)</span>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle">
                                <img src="images/bullet.gif" height="8" width="8" border="0" alt="bp"/></td>
                            <td align="left" valign="top">
                                <span class="LabelsBody">The transaction receipt is E-mailed to both the customer and the
                                    merchant according to the merchant's configuration.</span>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle">
                                <img src="images/bullet.gif" height="8" width="8" border="0" alt="bp"/></td>
                            <td align="left" valign="top">
                                <span class="LabelsBody">The transaction receipt is displayed on the screen for the merchant
                                    to print if they want.</span>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="left">
                <a class="LinkSmall" href="#00" target="_self" onmouseover='status=" Back to the top ";return true;'
                    onmouseout='status=""; return true;'>Back to the top</a>
            </td>
        </tr>
        <tr>
            <td align="left" valign="middle" colspan="4">
                <a name="#ConfigurationOptionsforVirtualTermin"></a>
                <asp:Panel ID="pnlConfiguration" runat="server" Height="20px">
                    <div style="cursor: pointer; vertical-align: middle; width: 700px;height: 20px;" class="DivHeader">
                        <div style="float: left; text-align: center; margin-left: 0px;">
                            <asp:Label ID="lblConfig" runat="server" Font-Bold="True" Font-Names="Arial" ForeColor="White"
                                Font-Size="Small" Text="Configuration options for Virtual Terminal"></asp:Label>
                        </div>
                        <div style="float: left; margin-left: 20px;">
                            <asp:Label ID="lblShowConfiguration" runat="server" Font-Bold="True" Font-Names="Arial"
                                ForeColor="White" Font-Size="8pt">(Show)</asp:Label>
                        </div>
                        <div style="float: right; vertical-align: middle;">
                            <asp:Image ID="imgShowConfiguration" runat="server" ImageUrl="images/expand_blue.jpg" /></div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlConfig" runat="server" Width="700px" CssClass="Border">
                    <span class="LabelsBody">You may find it cumbersome to have to fill in every field to do
                        a transaction. You can configure the system to allow fields to be left blank, or
                        just the opposite - to require that certain fields are not left blank.
                        <br />
                        <br />
                        You can also configure the system to send, or NOT send, the customer a transaction
                        receipt by E-mail on your behalf. Please note that disabling the customer's E-mail
                        receipt affects both Virtual Terminal transactions and WebLink transactions (those
                        that take place from your web site). The E-mailed receipts to the merchant can be
                        similarly configured.
                        <br />
                        <br />
                        Some configuration changes may only be made by your Commerce Technologies reseller
                        (usually). For complete details on how to make configuration changes to the e-Commerce
                        system, see the section entitled "Edit Configuration" later in this guide.
                        <br />
                        <br />
                    </span>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="left">
                <a class="LinkSmall" href="#00" target="_self" onmouseover='status=" Back to the top ";return true;'
                    onmouseout='status=""; return true;'>Back to the top</a>
            </td>
        </tr>
        <tr>
            <td align="center" valign="bottom" colspan="2">
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
