<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="software.aspx.cs" Inherits="software" 
    Title="Software" Theme="Admin"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <br />
    <table width="90%" align="center" cellspacing="0" cellpadding="0" border="0">
        <tr>
            <td align="center">
                <span class="LabelsHeaderLarge"><strong>Software</strong></span><br /><br />
            </td>
        </tr>
        <tr>
            <td>
                <table width="80%" align="center" cellspacing="4" cellpadding="0" border="0" class="DivGreen">
                    <tr>
                        <td align="left" width="30%">
                            <!--<a href="QuickBooksComparison.aspx" class="Link"><b>Financial/Accounting Software</b></a>-->
                            <a href="AccountingSoftware.aspx" class="Link"><b>Financial/Accounting Software</b></a>
                        </td>                        
                        <td align="left">
                            <span class="LabelsBody">Use Automated accounting software in your business for bookkeeping and payroll purposes. 
                            You can also accept payments from your customers using the financial software.</span>
                        </td>
                    </tr>
                    <tr height="10px"></tr>
                    <tr>
                        <td align="left">
                            <a href="POSSolutions.aspx" class="Link"><b>POS Software</b></a>
                        </td>
                        <td align="left">
                            <span class="LabelsBody">Use Point of Sale Software to run payments in your retail store, restaurant, professional service office, etc.</span>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr height=20></tr>
    </table>
</asp:Content>