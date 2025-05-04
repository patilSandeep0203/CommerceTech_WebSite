<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="infocenter.aspx.cs" Inherits="infocenter" Title="Credit Card Processing, Merchant Services, Payment Processing"
    Theme="Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <br />
    <table width="95%" cellspacing="2" cellpadding="2" border="0">
        <tr>
            <td align="left" valign="middle">
                <span class="LabelsBody">"Commerce Technologies has been wonderful for our Internet business!
                    We were working with a credit card processing company that would send our funds
                    to us after 30 days or more. Now with e-Commerce, we are back in business with daily
                    deposits straight to our business checking account. Orders are processed within
                    24 hours directly from our website. It truly has been the sole solution to our Internet
                    commerce needs."<br />
                    <br />
                </span>
                <div align="right">
                    <span class="LabelsBody">&nbsp;&nbsp;&nbsp;&nbsp;~ Tyra Jackson, V.P. <i>Nikjak & Sons Import
                        Company</i>&nbsp;&nbsp;&nbsp;&nbsp;<br />
                    </span>
                    <img src="images/cleardot.gif" width="300" height="1">
                </div>
            </td>
            <td width="200" align="center" valign="middle">
                <img src="images/splash-info.gif" height="150" width="200" alt="Contact Us" border="0">
            </td>
        </tr>
        <tr>
            <td align="center" valign="top" colspan="2">
                <table width="95%" cellspacing="5" cellpadding="2" border="0">
                    <tr>
                        <td align="center" colspan="2">
                            <span class="LabelsBody"><b>Choose a section below by clicking on the appropriate link</b></span>
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="left" width=30%>
                            <a class="Link" href="understanding.aspx" onmouseover='status=" Understanding E-Commerce ";return(true);'
                                onmouseout='status="";'>Understanding E-Commerce</a>
                        </td>
                        <td valign="top" align="left">
                            <span class="LabelsBody">Have a trained E-Consultant answer your questions. No obligation.
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="left">
                            <a class="Link" href="howitworks.aspx" onmouseover='status=" How it works ";return(true);'
                                onmouseout='status="";'>How it works</a>
                        </td>
                        <td valign="top" align="left">
                            <span class="LabelsBody">Discover how and when your customer's funds reach your own business
                                bank account. </span>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="left">
                            <a class="Link" href="ecomsolutions.aspx">E-Commerce Solutions </a>
                        </td>
                        <td valign="top" align="left">
                            <span class="LabelsBody">View a list of our E-Commerce Solutions </span>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="left">
                            <a class="Link" href="faqs.aspx" onmouseover='status=" Frequently Asked Questions ";return(true);'
                                onmouseout='status="";'>Frequently Asked Questions</a>
                        </td>
                        <td valign="top" align="left">
                            <span class="LabelsBody">Understanding e-commerce can sometimes be confusing especially
                                with the rapid changes occurring on the web. Click here for quick answers to commonly
                                asked questions. </span>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="left">
                            <a class="Link" href="Glossary.aspx" onmouseover='status=" Glossary ";return(true);'
                                onmouseout='status="";'>E-Glossary</a>
                        </td>
                        <td valign="top" align="left">
                            <span class="LabelsBody">An alphabetical list of terms related to the credit card processing industry.</span>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <!-- BEGIN SHORT FORM -->
        <tr>
            <td align="center" valign="middle" colspan="2">
                <br />
                <br />
                <span class="LabelsBody"><b>Request a <a class="Link" href="consult.aspx">Free E-Consultation!</a>
                    Absolutely No Obligation!</b></span>
            </td>
        </tr>
        <tr>
            <td align="center" valign="bottom" colspan="2">
                <br />
                <a class="Link" href="javascript:window.history.back();" onmouseover='status=" Back to previous page "; return true;'
                    onmouseout='status=" ";'>
                    <img src="images/back-on.gif" name="back" width="75" height="25" alt="" border="0"></a><br />
            </td>
        </tr>
    </table>
</asp:Content>
