<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewRates.aspx.cs" Inherits="ViewRates"
    Theme="TextBoxSkin" %>

<!DOCtype html PUBLIC "-//W3C//Dtd XHTML 1.0 Transitional//EN" "http://www.w3.org/tr/xhtml1/Dtd/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View Rates</title>
    <link type="text/css" href="onlineappcss.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <table cellspacing="0" cellpadding="0" style="width: 500px;" border="0">
            <tr valign="top">
                <td align="center" valign="middle" class="DivHeader2">
                    <b><span class="MenuHeader">Pricing Information</span></b>
                </td>
            </tr>
            <tr valign="top">
                <td align="center">
                    <asp:Label ID="lblError" runat="server" BackColor="Red" Font-Size="Medium" ForeColor="White"
                        Visible="False"></asp:Label><br />
                    <table cellspacing="0" cellpadding="0" width="80%" border="0">
                        <tr>
                            <td align="center">
                                <table border="0" style="width: 100%;" class="DivGreen">
                                    <tr>
                                        <td align="center" colspan="2">
                                            <span class="LabelsHeaderSmall"><b>Services</b></span>
                                        </td>
                                    </tr>
                                    <asp:Panel ID="pnlMerchant" runat="server" Height="100%" Width="100%">
                                        <tr>
                                            <td width="70%" align="left">
                                                <span class="LabelsHeaderSmall"><b>Merchant Account</b></span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="70%" align="left">
                                                <span class="LabelsBodySmall"></span></td>
                                            <td align="left">
                                                <b><asp:Label ID="lblApplicationFee" runat="server"></asp:Label></b></td>
                                        </tr>
                                        <tr>
                                            <td width="70%" align="left">
                                                <span class="LabelsBodySmall"></span></td>
                                            <td align="left">
                                                <b><asp:Label ID="lblAppSetupFee" runat="server"></asp:Label></b></td>
                                        </tr>
                                        <tr>
                                            <td width="70%" align="left">
                                                <span class="LabelsBodySmall">Visa/MC Discount Rate</span></td>
                                            <td align="left">
                                                <b><asp:Label ID="lblDiscountRate" runat="server"></asp:Label></b></td>
                                        </tr>
                                        <tr>
                                            <td width="70%" align="left">
                                                <asp:Label ID="labelDebitRate" Text="Visa/MC Debit Rate" runat="server"></asp:Label></td>
                                            <td align="left">
                                                <b><asp:Label ID="lblDebitRate" runat="server"></asp:Label></b></td>
                                        </tr>
                                        <tr>
                                            <td width="70%" align="left">
                                                <span class="LabelsBodySmall">Per Authorization - All Card Types</span></td>
                                            <td align="left">
                                                <b><asp:Label ID="lblPerAuth" runat="server"></asp:Label></b></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 70%; height: 15px;" align="left">
                                                <span class="LabelsBodySmall">Monthly Minimum</span></td>
                                            <td align="left">
                                                <b><asp:Label ID="lblMonMin" runat="server"></asp:Label></b></td>
                                        </tr>
                                        <tr>
                                            <td width="70%" align="left">
                                                <span class="LabelsBodySmall">Customer Service Fee</span></td>
                                            <td align="left">
                                                <b><asp:Label ID="lblTollFreeService" runat="server"></asp:Label></b></td>
                                        </tr>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlDBEBT" runat="server" Height="100%" Width="100%">
                                    <tr>
                                        <td colspan="2" align="left">  
                                                    <asp:Table ID="tblDBEBT" runat="server" style="width: 100%;">
                                                    </asp:Table>
                                        </td>
                                    </tr>
                                                </asp:Panel>
                                    <asp:Panel ID="pnlGateway" runat="server" Height="100%" Width="100%">
                                        <tr height="10px"></tr>
                                        <tr>
                                            <td width="70%" align="left">
                                                <span class="LabelsHeaderSmall"><b>Payment Gateway</b></span></td>
                                            <td align="left">
                                                <b>
                                                    <asp:Label ID="lblGateway" runat="server"></asp:Label></b></td>
                                        </tr>
                                        <tr>
                                            <td width="70%" align="left">
                                                <span class="LabelsBodySmall"></span></td>
                                            <td align="left">
                                                <b><asp:Label ID="lblGatewaySetupFee" runat="server"></asp:Label></b></td>
                                        </tr>
                                        <tr>
                                            <td width="70%" align="left">
                                                <span class="LabelsBodySmall">Monthly Gateway Access</span></td>
                                            <td align="left">
                                                <b><asp:Label ID="lblMonthlyGatewayAccess" runat="server"></asp:Label></b></td>
                                        </tr>
                                        <tr>
                                            <td width="70%" align="left">
                                                <span class="LabelsBodySmall">Gateway Transaction Fee</span></td>
                                            <td align="left">
                                                <b><asp:Label ID="lblGatewayTransFee" runat="server"></asp:Label></b></td>
                                        </tr>
                                       <asp:Panel ID="pnlECheck" runat="server" visible="false" Height="100%" Width="100%">
                                         <tr>
                                            <td width="70%" align="center" colspan="2">
                                                <span class="LabelsBodySmall"><b>E-Check</b></span></td>
                                         </tr>
                                         <tr>
                                            <td width="70%" align="left">
                                                <span class="LabelsBodySmall">Monthly Minimum</span></td>
                                            <td align="left">
                                                <b><asp:Label ID="lblECheckMonMin" runat="server"></asp:Label></b></td>
                                        </tr>
                                         <tr>
                                            <td width="70%" align="left">
                                                <span class="LabelsBodySmall">Trans Fee</span></td>
                                            <td align="left">
                                                <b><asp:Label ID="lblECheckTransFee" runat="server"></asp:Label></b></td>
                                        </tr>
                                      </asp:Panel>
                                </asp:Panel>
                                <asp:Panel ID="pnlAdditionalServices" runat="server" Width="100%" Visible="false">
                                    <tr>
                                        <td colspan="2" align="left">                                        
                                            <asp:Table ID="tblAddlServices" runat="server" Width="100%">
                                            </asp:Table>                                        
                                        </td>
                                    </tr>
                                </asp:Panel>
                                <asp:Panel ID="pnlProducts" runat="server" Width="100%" Visible="false">
                                    <tr height="10px"></tr>
                                    <tr>
                                        <td colspan="2">                                            
                                            <asp:Table ID="tblProducts" runat="server" Width="100%">
                                            </asp:Table>
                                        </td>
                                    </tr>
                                </asp:Panel>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr height="20px">
            </tr>
            <tr height="25px">
                <td align="center">
                    <asp:Button ID="btnClose" runat="server" OnClick="btnClose_Click" Text="Close" />
                </td>
            </tr>
            <tr height="10px"></tr>
            <tr>
                <td align="center" class="DivHeader2">
                    <span class="MenuHeader">&nbsp;</span>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
