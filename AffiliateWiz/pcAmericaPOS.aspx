<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="pcAmericaPOS.aspx.cs" Inherits="pcAmericaPOS" 
    Title="Software" Theme="Admin"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <br />
    <table width="90%" align="center" cellspacing="0" cellpadding="0" border="0">
        <tr>
            <td align="center">
                <span class="LabelsHeaderLarge"><strong>pcAmerica POS</strong></span><br /><br />
            </td>
        </tr>
        <tr>
            <td>
                <table width="80%" align="center" cellspacing="4" cellpadding="0" border="0" class="DivGreen">
                    <tr>
                        <td align="left" width="30%">
                            <!--<a href="QuickBooksComparison.aspx" class="Link"><b>Financial/Accounting Software</b></a>-->
                            <a href="pcAmericaPOSRetail.aspx" class="Link"><b>Cash Register Express</b></a>
                        </td>                        
                        <td align="left">
                            <span class="LabelsBody">Cash Register Express (CRE) is a retail point of sale (POS) system that helps increase efficiency and accuracy, lower operating costs, and helps you manage your inventory.</span>
                        </td>
                    </tr>
                    <tr height="10px"></tr>
                    <tr>
                        <td align="left">
                            <a href="pcAmericaRestaurant.aspx" class="Link"><b>Restaurant Pro Express</b></a>
                        </td>
                        <td align="left">
                            <span class="LabelsBody">Restaurant Pro Express(RPE) is a touch screen restaurant point of sale (POS) system that provides fast operations, accurate order taking and detailed reports with an easy-to-use interface.</span>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr height=20></tr>
    </table>
</asp:Content>
