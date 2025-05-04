<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="weblink.aspx.cs" Inherits="weblink" Title="Credit Card Processing, Merchant Services, Payment Processing"
    Theme="Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <br />
    <table width="95%" cellspacing="0" cellpadding="0" border="0">
        <tr>
            <td align="center" colspan="2">
                <span class="LabelsHeader"><b>For your e-Commerce website</b></span><br /><br />
            </td>
        </tr>
        <tr>
            <td align="left" valign="middle" colspan="2">
                <span class="LabelsBody">Automate credit card transactions
                    directly from your Website by integrating to the Payment Gateway via
                    one of our connection methods:</span>
                <br />
                <br />
            </td>
        </tr>
        <tr>
            <td align="left" valign="middle" style="width: 3%">
                <img alt="bp" src="images/bullet.gif" border="0" height="8" width="8" />
            </td>
            <td align="left">
                <a class="Link" href="http://developer.authorize.net/guides/AIM" target="_blank"><b>Advanced Integration Method</b></a>
                <span class="LabelsHeader">(Online Guide)</span>
            </td>
        </tr>
        <tr>
            <td align="left" valign="middle">
            </td>
            <td align="left">
                <span class="LabelsBody">
                    <br />
                    This document details the technical specifications and features of the AIM connection
                    method. AIM uses a direct secure sockets layer (SSL) connection to the Payment Gateway
                    and is Authorize.Net's recommended connection method. </span>
                <br />
                <br />
            </td>
        </tr>
        <tr>
            <td align="left" valign="middle">
                <img alt="bp" src="images/bullet.gif" border="0" height="8" width="8" />
            </td>
            <td align="left">
                <a class="Link" href="http://developer.authorize.net/guides/SIM" target="_blank"><b>Simple Integration Method</b></a>
                <span class="LabelsHeader">(Online Guide)</span>
            </td>
        </tr>
        <tr>
            <td align="left" valign="middle">
            </td>
            <td align="left">
                <span class="LabelsBody">
                    <br />
                    This document details the technical specifications and features of the SIM connection
                    method. SIM uses a secure, encrypted HTTPS form POST connection to the Authorize.Net
                    Payment Gateway. With SIM, each transaction is authenticated using a unique fingerprint
                    (like a password). </span>
            </td>
        </tr>
        <tr>
            <td align="center" valign="bottom" colspan="4">
                <br /><a href="javascript:window.history.back();" onmouseover='status=" Back to previous page "; return true;'
                    onmouseout='status=" ";'>
                    <img alt="" src="images/back-on.gif" name="back" border="0" width="75" height="25" /></a>
            </td>
        </tr>
    </table>
</asp:Content>
