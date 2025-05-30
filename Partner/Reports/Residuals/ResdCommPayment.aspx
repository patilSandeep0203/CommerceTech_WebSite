<%@ Page Culture="auto" UICulture="auto" Language="C#" MasterPageFile="~/Payroll/Admin.master" AutoEventWireup="true" CodeFile="ResdCommPayment.aspx.cs"
    Inherits="Payroll_ResdCommPayment" Title="Commerce Technologies - Partner Portal" Theme="AppTheme" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ScriptManager ID="ScriptManagerComm" runat="server"  EnableScriptGlobalization="true" EnableScriptLocalization="true">
    </asp:ScriptManager>
    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDatePaid">
    </cc1:CalendarExtender>
    <asp:Label ID="lblError" CssClass="LabelsError" EnableTheming="false" runat="server" Visible="False"></asp:Label><br />
    <table border="0" cellpadding="0" cellspacing="0" style="width: 260px;" class="SilverBorder">
        <tr>
            <td align="center" colspan="3" style="background-image: url(../Images/topMain.gif);
                height: 25px">
                <b><span class="MenuHeader">Residual/Commission Payment Summary</span></b></td>
        </tr>
        <tr>
            <td colspan="3" style="height: 5px">
            </td>
        </tr>
        <tr>
            <td align="right" style="width: 50%">
                <asp:Label ID="lblSelectMonth" runat="server" Font-Bold="True" Font-Names="Arial"
                    Font-Size="Smaller" Text="Select Month"></asp:Label>&nbsp;</td>
            <td align="left" colspan="2">
                <asp:DropDownList ID="lstMonth" runat="server" Width=100>
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td align="right" style="width: 50%">
                <asp:Label ID="lblPeriod" runat="server" Font-Bold="True" Font-Names="Arial"
                    Font-Size="8pt" Text="Select Period"></asp:Label>&nbsp;
            </td>
            <td align="left" colspan="2" style="width: 174px">
                <asp:DropDownList ID="lstPeriod" runat="server" Width=100>
                    <asp:ListItem Value=1>First Half</asp:ListItem>
                    <asp:ListItem Value=2>Second Half</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="3" style="height: 5px">
            </td>
        </tr>
        <tr>
            <td align="center" colspan="3">
                <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" />
            </td>
        </tr>
    </table>
    <br />
    <asp:HyperLink ID="lnkPrint" runat="server" CssClass="One" Font-Bold="True" Font-Names="Arial"
        Visible="false" Font-Size="Small" NavigateUrl="ResdCommPaymentPrint.aspx" Target="_blank">Print Report</asp:HyperLink><br />
    <br />
    <asp:Label ID="lblMonth" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="Smaller"></asp:Label><br />
    <asp:Panel ID="pnlConfirmation" runat="server" Visible="False">
        <asp:Label ID="lblUpdateHeader" runat="server" Font-Bold="True" Text="Update Information"></asp:Label>
        <table style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid;
            border-bottom: silver 1px solid">
            <tr>
                <td style="background-image: url(../Images/topMain.gif)">
                    <b><span class="MenuHeader">Confirmation Code</span></b></td>
                <td style="background-image: url(../Images/topMain.gif)">
                    <strong><span style="color: #ffffff; font-family: Arial; font-size:small">Date Paid</span></strong></td>
                <td style="background-image: url(../Images/topMain.gif)">
                    <b><span class="MenuHeader">Note</span></b></td>
                <td style="background-image: url(../Images/topMain.gif)">
                    <b><span class="MenuHeader">Partner ID</span></b></td>
                <td style="background-image: url(../Images/topMain.gif)">
                    <b><span class="MenuHeader">Company Name</span></b></td>
                <td style="background-image: url(../Images/topMain.gif)">
                    <b><span class="MenuHeader">Commission</span></b></td>
                <td style="background-image: url(../Images/topMain.gif)">
                    <b><span class="MenuHeader">Referral</span></b></td>   
                <td style="background-image: url(../Images/topMain.gif)">
                    <b><span class="MenuHeader">Residual</span></b></td>                  
                <td style="background-image: url(../Images/topMain.gif)">
                    <b><span class="MenuHeader">Carryover</span></b></td>
                <td style="background-image: url(../Images/topMain.gif)">
                    <b><span class="MenuHeader">Payment</span></b></td>
                    <td style="background-image: url(../Images/topMain.gif)">
                    <b><span class="MenuHeader">OfficeID</span></b></td>
            </tr>
            <tr>
                <td style="background-color: #fbfbfb">
                    <asp:TextBox ID="txtConfirmationCode" runat="server" Width="160px" MaxLength="32"></asp:TextBox>
                </td>
                <td style="background-color: #fbfbfb">
                    <asp:TextBox ID="txtDatePaid" AutoComplete="off" runat="server" Width="120px" MaxLength="32"></asp:TextBox>
                </td>
                <td style="background-color: #fbfbfb">
                 <asp:TextBox ID="txtNote" runat="server" Width="160px" MaxLength="256"></asp:TextBox>
                    </td>     
                <td style="background-color: #fbfbfb">
                    <asp:Label ID="lblAffiliateID" runat="server" Font-Bold="True"></asp:Label></td>
                <td style="background-color: #fbfbfb">
                    <asp:Label ID="lblCompanyName" runat="server" Font-Bold="True"></asp:Label></td>
                <td style="background-color: #fbfbfb">
                    <asp:Label ID="lblCommission" runat="server" Font-Bold="True"></asp:Label></td>
                <td style="background-color: #fbfbfb">
                    <asp:Label ID="lblReferral" runat="server" Font-Bold="True"></asp:Label></td>     
                <td style="background-color: #fbfbfb">
                    <asp:Label ID="lblResidual" runat="server" Font-Bold="True"></asp:Label></td>                    
                <td style="background-color: #fbfbfb">
                    <asp:TextBox ID="txtCarryover" runat="server" Width="40px" MaxLength="8"></asp:TextBox>
                   <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtCarryover"
                                            FilterType="Custom, Numbers" ValidChars=".-" />
                </td>                    
                <td style="background-color: #fbfbfb">
                    <asp:TextBox ID="txtPayment" runat="server" Width="40px" MaxLength="8"></asp:TextBox>
                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtPayment"
                        FilterType="Custom, Numbers" ValidChars="." />
                </td>
            </tr>
            <tr>
                <td colspan="3" style="background-color: #fbfbfb">
                    <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" Text="Update" />&nbsp;
                    <asp:Button ID="btnCancel" runat="server" CausesValidation="False" OnClick="btnCancel_Click"
                        Text="Cancel" /></td>
                <td colspan="6" align=right valign=bottom><span style="color: red; font-family: Arial; font-size:x-small">NOTE: </span>
                <span style="color:black; font-family: Arial; font-size:x-small">Payment will be updated once Confirmation Code is entered.</span></td>                        
            </tr>
        </table>
    </asp:Panel>
    <br />
    <asp:Panel ID="pnlEmployees" runat="server" Visible="False">
        <table border="0" cellspacing="0" style="background-color: #fbfbfb; border-right: silver 1px solid;
            border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;
            width: 900px">
            <tr>
                <td align="center" style="background-color:#5c995d;">
                    <b><span style="color: white; font-family: Arial;">Employees</span></b>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Panel ID="pnlCurrEmployees" runat="server">
                        <table border="0" cellspacing="0" style="width: 900px">
                            <tr>
                                <td align="center">
                                    <b><span style="color: #383838; font-family: Arial; font-size: small">Current Employees</span></b>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:GridView ID="grdEmployeeSummary" runat="server" AutoGenerateColumns="False"
                                        CellPadding="4" ForeColor="#333333" GridLines="Vertical" OnRowCommand="grdEmployeeSummary_RowCommand"
                                        OnRowDataBound="grdEmployeeSummary_RowDataBound" ShowFooter = "True" Width="900px"  DataKeyNames="ConfirmNum,ConfirmDate,Note,AffiliateID,Contact,CompanyName,CommTotal,BonusTotal,RefTotal,Residual,CarryoverBalance,Payment,OfficeID">
                                                    
                                            
                                        <FooterStyle BackColor="#EDF7FF" Font-Names="Arial" Font-Size="X-Small" Font-Bold="True" ForeColor="#284775" />
                                        <Columns>
                                            <asp:ButtonField CommandName="Confirmation" Text="Edit" FooterStyle-Font-Size="X-Small" FooterText="SubTotals:" ItemStyle-Width=50>
                                                <ItemStyle Font-Bold="True" Font-Names="Arial" Font-Size="X-Small" />
                                            </asp:ButtonField>

                                            <asp:BoundField DataField="ConfirmNum" HeaderText="Confirmation" ItemStyle-Width="70" ControlStyle-Width="50px" />
                                            <asp:BoundField DataField="ConfirmDate" HeaderText="Date Paid" ItemStyle-Width=70/>
                                            <asp:BoundField DataField="Note" HeaderText="Note" ItemStyle-Width=50/>
                                            <asp:BoundField DataField="AffiliateID" HeaderText="Partner ID" ItemStyle-Width=70/>
                                            <asp:BoundField DataField="Contact" HeaderText="Contact" ItemStyle-Width=70/>
                                            <asp:BoundField DataField="CompanyName" HeaderText="Company Name" ItemStyle-Width=90/>
                                            <asp:BoundField DataField="CommTotal" HeaderText="Commission" ItemStyle-Width=80 />
                                            <asp:BoundField DataField="BonusTotal" HeaderText="Bonus" ItemStyle-Width=40/>
                                            <asp:BoundField DataField="RefTotal" HeaderText="Referral" ItemStyle-Width=60/>
                                            <asp:BoundField DataField="Residual" HeaderText="Residual" ItemStyle-Width=60/>
                                            <asp:BoundField DataField="CarryOverBalance" HeaderText="Carryover" ItemStyle-Width=60/>
                                            <asp:BoundField HeaderText="Total" ItemStyle-Width=40/>
                                            <asp:BoundField DataField="Payment" HeaderText="Payment" ItemStyle-Width=60/>  

                                            

                                            <asp:BoundField DataField="OfficeID" HeaderText="Office" ItemStyle-Width=60 />                                           
                                        </Columns>
                                        <RowStyle BackColor="#EDF7FF" Font-Names="Arial" Font-Size="X-Small" ForeColor="#333333" />
                                        <EditRowStyle BackColor="#999999" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                        <HeaderStyle BackColor="#5D7B9D" CssClass="MenuHeader" />
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Panel ID="pnlPrevEmployees" runat="server">
                        <table border="0" cellspacing="0" style="width: 900px">
                            <tr>
                                <td align="center">
                                    <b><span style="color: #383838; font-family: Arial; font-size: small">Previous Employees</span></b>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:GridView ID="grdPrevEmployeeSummary" runat="server" AutoGenerateColumns="False"
                                        CellPadding="4" ForeColor="#333333" GridLines="Vertical" OnRowCommand="grdPrevEmployeeSummary_RowCommand"
                                        OnRowDataBound="grdPrevEmployeeSummary_RowDataBound" ShowFooter = "True" Width="900px" DataKeyNames="ConfirmNum,ConfirmDate,Note,AffiliateID,Contact,CompanyName,CommTotal,BonusTotal,RefTotal,Residual,CarryoverBalance,Payment,OfficeID">
                                        <FooterStyle BackColor="#EDF7FF" Font-Names="Arial" Font-Size="X-Small" Font-Bold="True" ForeColor="#284775" />
                                        <Columns>
                                            <asp:ButtonField CommandName="Confirmation" Text="Edit" FooterStyle-Font-Size="X-Small" FooterText="SubTotals:" ItemStyle-Width=60>
                                                <ItemStyle Font-Bold="True" Font-Names="Arial" Font-Size="X-Small" />
                                            </asp:ButtonField>
                                            
                                            
                                            <asp:BoundField DataField="ConfirmNum" HeaderText="Confirmation" ItemStyle-Width="70" ControlStyle-Width="50px" />
                                            <asp:BoundField DataField="ConfirmDate" HeaderText="Date Paid" ItemStyle-Width=70/>
                                            <asp:BoundField DataField="Note" HeaderText="Note" ItemStyle-Width=50/>
                                            <asp:BoundField DataField="AffiliateID" HeaderText="Partner ID" ItemStyle-Width=70/>
                                            <asp:BoundField DataField="Contact" HeaderText="Contact" ItemStyle-Width=70/>
                                            <asp:BoundField DataField="CompanyName" HeaderText="Company Name" ItemStyle-Width=90/>
                                            <asp:BoundField DataField="CommTotal" HeaderText="Commission" ItemStyle-Width=80 />
                                            <asp:BoundField DataField="BonusTotal" HeaderText="Bonus" ItemStyle-Width=40/>
                                            <asp:BoundField DataField="RefTotal" HeaderText="Referral" ItemStyle-Width=60/>
                                            <asp:BoundField DataField="Residual" HeaderText="Residual" ItemStyle-Width=60/>
                                            <asp:BoundField DataField="CarryOverBalance" HeaderText="Carryover" ItemStyle-Width=60/>
                                            <asp:BoundField HeaderText="Total" ItemStyle-Width=40/>
                                            <asp:BoundField DataField="Payment" HeaderText="Payment" ItemStyle-Width=60/>  
  

                                            <asp:BoundField DataField="OfficeID" HeaderText="Office" ItemStyle-Width=60 />   
                                        </Columns>
                                        <RowStyle BackColor="#EDF7FF" Font-Names="Arial" Font-Size="X-Small" ForeColor="#333333" />
                                        <EditRowStyle BackColor="#999999" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                        <HeaderStyle BackColor="#5D7B9D" CssClass="MenuHeader" />
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlPartners" runat="server" Visible="False">
        <table border="0" cellspacing="0" style="background-color: #fbfbfb; border-right: silver 1px solid;
            border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;
            width: 900px">
            <tr>
                <td align="center" style="background-color:#5c995d;">
                    <b><span style="color: white; font-family: Arial;">Partners</span></b>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Panel ID="pnlDDPartner" runat="server">
                        <table border="0" cellspacing="0" style="width: 900px">
                            <tr>
                                <td align="center">
                                    <b><span style="color: #383838; font-family: Arial; font-size: small">Direct Deposit</span></b>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:GridView ID="grdSummaryDD" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                        ForeColor="#333333" GridLines="Vertical" OnRowCommand="grdSummaryDD_RowCommand"
                                        OnRowDataBound="grdSummaryDD_RowDataBound" ShowFooter="True" Width ="900px" DataKeyNames="ConfirmNum,ConfirmDate,Note,AffiliateID,Contact,CompanyName,CommTotal,BonusTotal,RefTotal,Residual,CarryoverBalance,Payment,OfficeID">
                                        <FooterStyle BackColor="#EDF7FF" Font-Names="Arial" Font-Size="X-Small" Font-Bold="True" ForeColor="#284775" />
                                        <Columns>

                                        <asp:ButtonField CommandName="Confirmation" Text="Edit" FooterStyle-Font-Size="X-Small" FooterText="SubTotals:" ItemStyle-Width=60>
                                                <ItemStyle Font-Bold="True" Font-Names="Arial" Font-Size="X-Small" />
                                            </asp:ButtonField>                 
                                            
                                            <asp:BoundField DataField="ConfirmNum" HeaderText="Confirmation" ItemStyle-Width="70" ControlStyle-Width="50px" />
                                            <asp:BoundField DataField="ConfirmDate" HeaderText="Date Paid" ItemStyle-Width=70/>
                                            <asp:BoundField DataField="Note" HeaderText="Note" ItemStyle-Width=50/>
                                            <asp:BoundField DataField="AffiliateID" HeaderText="Partner ID" ItemStyle-Width=70/>
                                            <asp:BoundField DataField="Contact" HeaderText="Contact" ItemStyle-Width=70/>
                                            <asp:BoundField DataField="CompanyName" HeaderText="Company Name" ItemStyle-Width=90/>
                                            <asp:BoundField DataField="CommTotal" HeaderText="Commission" ItemStyle-Width=80 />
                                            <asp:BoundField DataField="BonusTotal" HeaderText="Bonus" ItemStyle-Width=40/>
                                            <asp:BoundField DataField="RefTotal" HeaderText="Referral" ItemStyle-Width=60/>
                                            <asp:BoundField DataField="Residual" HeaderText="Residual" ItemStyle-Width=60/>
                                            <asp:BoundField DataField="CarryOverBalance" HeaderText="Carryover" ItemStyle-Width=60/>
                                            <asp:BoundField HeaderText="Total" ItemStyle-Width=40/>
                                            <asp:BoundField DataField="Payment" HeaderText="Payment" ItemStyle-Width=60/>  
 

                                            <asp:BoundField DataField="OfficeID" HeaderText="Office" ItemStyle-Width=60 />   
                                        </Columns>
                                        <RowStyle BackColor="#EDF7FF" Font-Names="Arial" Font-Size="X-Small" ForeColor="#333333" />
                                        <EditRowStyle BackColor="#999999" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                        <HeaderStyle BackColor="#5D7B9D" CssClass="MenuHeader" />
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    </asp:GridView>
                                </td>
                            </tr>

                            <tr>
                                <td align="center">

                                         <asp:GridView ID="grdSummaryOfficeDD" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                       GridLines="Vertical" OnRowCommand="grdSummaryDD_RowCommand"
                                        OnRowDataBound="grdSummaryOfficeDD_RowDataBound" ShowFooter="True" Width ="900px" DataKeyNames="ConfirmNum,ConfirmDate,Note,AffiliateID,Contact,CompanyName,CommTotal,BonusTotal,RefTotal,Residual,CarryoverBalance,Payment,OfficeID">
                                        <FooterStyle BackColor="#EDF7FF" Font-Names="Arial" Font-Size="X-Small" Font-Bold="True" ForeColor="#284775" />
                                        <Columns>

                                        <asp:ButtonField CommandName="Confirmation"  FooterStyle-Font-Size="X-Small" FooterText="SubTotals:" ItemStyle-Width=60>
                                                <ItemStyle Font-Bold="True" Font-Names="Arial" Font-Size="X-Small" />
                                            </asp:ButtonField>
                                            
                                            <asp:BoundField DataField="ConfirmNum" HeaderText="Confirmation" ItemStyle-Width="70" ControlStyle-Width="50px" />
                                            <asp:BoundField DataField="ConfirmDate" HeaderText="Date Paid" ItemStyle-Width=70/>
                                            <asp:BoundField DataField="Note" HeaderText="Note" ItemStyle-Width=50/>
                                            <asp:BoundField DataField="AffiliateID" HeaderText="Partner ID" ItemStyle-Width=70/>
                                            <asp:BoundField DataField="Contact" HeaderText="Contact" ItemStyle-Width=70/>
                                            <asp:BoundField DataField="CompanyName" HeaderText="Company Name" ItemStyle-Width=90/>
                                            <asp:BoundField DataField="CommTotal" HeaderText="Commission" ItemStyle-Width=80 />
                                            <asp:BoundField DataField="BonusTotal" HeaderText="Bonus" ItemStyle-Width=40/>
                                            <asp:BoundField DataField="RefTotal" HeaderText="Referral" ItemStyle-Width=60/>
                                            <asp:BoundField DataField="Residual" HeaderText="Residual" ItemStyle-Width=60/>
                                            <asp:BoundField DataField="CarryOverBalance" HeaderText="Carryover" ItemStyle-Width=60/>
                                            <asp:BoundField HeaderText="Total" ItemStyle-Width=40/>
                                            <asp:BoundField DataField="Payment" HeaderText="Payment" ItemStyle-Width=60/>  


                                            <asp:BoundField DataField="OfficeID" HeaderText="Office" ItemStyle-Width=60 />   
                                        </Columns>
                                        <RowStyle BackColor="#EDF7FF" Font-Names="Arial" Font-Size="X-Small" ForeColor="#333333" />
                                        <EditRowStyle BackColor="#999999" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                        <HeaderStyle BackColor="#5D7B9D" CssClass="MenuHeader" />
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Panel ID="pnlBPPartners" runat="server">
                        <table border="0" cellspacing="0" style="width: 900px">
                            <tr>
                                <td align="center">
                                    <b><span style="color: #383838; font-family: Arial; font-size: small">Bill Pay</span></b>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:GridView ID="grdSummaryBP" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                        ForeColor="#333333" GridLines="Vertical" OnRowCommand="grdSummaryBP_RowCommand"
                                        OnRowDataBound="grdSummaryBP_RowDataBound" ShowFooter="True" Width ="900px" DataKeyNames="ConfirmNum,ConfirmDate,Note,AffiliateID,Contact,CompanyName,CommTotal,BonusTotal,RefTotal,Residual,CarryoverBalance,Payment,OfficeID">
                                        <FooterStyle BackColor="#EDF7FF" Font-Names="Arial" Font-Size=X-Small Font-Bold="True" ForeColor="#284775" />
                                        <Columns>
                                            <asp:ButtonField CommandName="Confirmation" Text="Edit" FooterStyle-Font-Size="X-Small" FooterText="SubTotals:" ItemStyle-Width=60>
                                                <ItemStyle Font-Bold="True" Font-Names="Arial" Font-Size="X-Small" />
                                            </asp:ButtonField>
                
                                            <asp:BoundField DataField="ConfirmNum" HeaderText="Confirmation" ItemStyle-Width="70" ControlStyle-Width="50px" />
                                            <asp:BoundField DataField="ConfirmDate" HeaderText="Date Paid" ItemStyle-Width=70/>
                                            <asp:BoundField DataField="Note" HeaderText="Note" ItemStyle-Width=50/>
                                            <asp:BoundField DataField="AffiliateID" HeaderText="Partner ID" ItemStyle-Width=70/>
                                            <asp:BoundField DataField="Contact" HeaderText="Contact" ItemStyle-Width=70/>
                                            <asp:BoundField DataField="CompanyName" HeaderText="Company Name" ItemStyle-Width=90/>
                                            <asp:BoundField DataField="CommTotal" HeaderText="Commission" ItemStyle-Width=80 />
                                            <asp:BoundField DataField="BonusTotal" HeaderText="Bonus" ItemStyle-Width=40/>
                                            <asp:BoundField DataField="RefTotal" HeaderText="Referral" ItemStyle-Width=60/>
                                            <asp:BoundField DataField="Residual" HeaderText="Residual" ItemStyle-Width=60/>
                                            <asp:BoundField DataField="CarryOverBalance" HeaderText="Carryover" ItemStyle-Width=60/>
                                            <asp:BoundField HeaderText="Total" ItemStyle-Width=40/>
                                            <asp:BoundField DataField="Payment" HeaderText="Payment" ItemStyle-Width=60/>  
 

                                            <asp:BoundField DataField="OfficeID" HeaderText="Office" ItemStyle-Width=60 />
                                        </Columns>
                                        <RowStyle BackColor="#EDF7FF" Font-Names="Arial" Font-Size="X-Small" ForeColor="#333333" />
                                        <EditRowStyle BackColor="#999999" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                        <HeaderStyle BackColor="#5D7B9D" CssClass="MenuHeader" />
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
            </tr>
        </table>
    </asp:Panel>
    <asp:Table ID="tblBPTotal" runat="server" CellPadding="0" CellSpacing="0" BackColor="#edf7ff" Width="900px" ForeColor="Black"></asp:Table>
    <br />
</asp:Content>
