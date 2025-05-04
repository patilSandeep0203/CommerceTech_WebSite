<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ecomsolutions.aspx.cs" Inherits="ecomsolutions" Title="E-Commerce Solutions"
    Theme="Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <table align="center" width="95%" cellspacing="0" cellpadding="0" border="0">
        <tr>
            <td align="left">
                <br />
                <span class="LabelsBody">Commerce Technologies provides a complete secure transaction processing
                    payment gateway system, that acts like a standard authorization terminal in a secure
                    real-time environment. Use it to submit, authorize, capture, and settle credit card
                    transactions without the need for separate transaction terminal or processing software.
                    Transactions are authorized immediately upon submitting. </span>
            </td>
        </tr>
        <tr>
            <td align="center" valign="middle">
                <br />
                <table align="center" cellspacing="0" cellpadding="4" border="0" class="DivGreen">
                    <tr>
                        <td align="left">
                            <a href="echeck.aspx" class="Link" target="_self" onmouseover="status=&quot;Click Here to view the Electronic Checks page&quot;;return true;"
                                onmouseout="status=&quot; &quot;; return true;"><b>Electronic Checks</b></a>
                        </td>
                        <td align="left">
                            <span class="LabelsBody">ACH Electronic Checks. </span>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <a href="shopcart.aspx" class="Link" target="_self" onmouseover="status=&quot; Click here for information about Shopping Carts&quot;;return true;"
                                onmouseout="status=&quot; &quot;; return true;"><b>Shopping Carts</b></a>
                        </td>
                        <td align="left">
                            <span class="LabelsBody">View a list of our supported/recommended Shopping Carts. </span>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <a href="virtualterminal.aspx" class="Link" target="_self" onmouseover="status=&quot; View our extensive on-line documentation &quot;;return true;"
                                onmouseout="status=&quot; &quot;; return true;"><b>Virtual Terminal</b></a>
                        </td>
                        <td align="left">
                            <span class="LabelsBody">View our extensive on-line documentation. </span>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <a href="weblink.aspx" class="Link" target="_self" onmouseover="status=&quot; Click here for information about Weblink&quot;;return true;"
                                onmouseout="status=&quot; &quot;; return true;"><b>Web link</b></a>
                        </td>
                        <td align="left">
                            <span class="LabelsBody">Download AIM and SIM Integration Guide. </span>
                        </td>
                    </tr>                    
                </table>
            </td>
        </tr>
        <tr>
            <td align="center" valign="middle">
                <br />
                <table border="0" cellpadding="0" cellspacing="5" width="100%">
                    <tr>
                        <td valign="top" style="width: 48%">
                            <div align="center" style="width: 300px;" class="DivHeader">
                                <span class="LabelsWhite"><b>Why you should accept credit cards...</b></span><br />
                                <div align="left" style="width: 300px;" class="DivWhiteSmoke">
                                    <ul class="Bullet">
                                        <li style="padding-top:5px" class="LabelsBodySmall">Greater profits</li>
                                        <li style="padding-top:5px" class="LabelsBodySmall">Increased credibility</li>
                                        <li style="padding-top:5px" class="LabelsBodySmall">Customer convenience</li>
                                        <li style="padding-top:5px" class="LabelsBodySmall">Confidence and security</li>
                                        <li style="padding-top:5px" class="LabelsBodySmall">Fast access to funds</li>
                                    </ul>
                                </div>
                            </div>
                        </td>
                        <td align="center" style="width: 1%">
                            <img border="0" height="250" src="images/vr.gif" width="10" /></td>
                        <td align="center" valign="top">
                            <div align="center" style="width: 300px;" class="DivHeader">
                                <span class="LabelsWhite"><b>e-Commerce Features</b></span><br />
                                <div align="left" style="width: 300px;" class="DivWhiteSmoke">
                                    <ul class="Bullet">
                                        <li style="padding-top:5px" class="LabelsBodySmall">Bank approved merchant account - 95% approval</li>
                                        <li style="padding-top:5px" class="LabelsBodySmall">Fast 1-3 day setup</li>
                                        <li style="padding-top:5px" class="LabelsBodySmall">Web-based payment transaction software</li>
                                        <li style="padding-top:5px" class="LabelsBodySmall">Secure server connection</li>
                                        <li style="padding-top:5px" class="LabelsBodySmall">Seamless integration with your online storefront</li>
                                        <li style="padding-top:5px" class="LabelsBodySmall">Free customer service day or night including holidays</li>
                                        <li style="padding-top:5px" class="LabelsBodySmall">Daily & monthly merchant statements</li>
                                        <li style="padding-top:5px" class="LabelsBodySmall">Powerful reporting features</li>
                                        <li style="padding-top:5px" class="LabelsBodySmall">95% approval rate</li>
                                        <li style="padding-top:5px" class="LabelsBodySmall">Fast access to funds - 24 to
                                        48 hours from time of purchase</li>
                                    </ul>                                 
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center" valign="middle">
            </td>
        </tr>
        <tr>
            <td align="center" valign="middle">
                <hr width="90%" noshade size="1" />
                <span class="LabelsBody"><b>Click <a class="Link" href="prodservices.aspx">here</a> for our
                    Additional Products and Services</b></span><br />
                <hr width="90%" noshade size="1" />
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="center" valign="bottom">
                <br />
                <a href="javascript:window.history.back();" onmouseover='status=" Back to previous page "; return true;'
                    onmouseout='status=" ";'>
                    <img src="images/back-on.gif" name="back" alt="" border="0" width="75" height="25"></a>
            </td>
        </tr>
    </table>
</asp:Content>
