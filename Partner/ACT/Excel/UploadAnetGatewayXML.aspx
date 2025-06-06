<%@ Page Language="C#" MasterPageFile="~/ACT/MasterPage/Admin.master" AutoEventWireup="true"
    CodeFile="UploadAnetGatewayXML.aspx.cs" Inherits="UploadAnetGatewayXML" Title="Commerce Technologies - Partner Portal"
    Theme="AppTheme" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="300">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Label ID="lblError" CssClass="LabelsError" EnableTheming="false" runat="server"
                Visible="False"></asp:Label><br />
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Panel ID="panSearch" runat="server" DefaultButton="btnSubmit" Width="100%" >
    <table cellpadding="0" cellspacing="0" border="0" style="width: 400px;" class="SilverBorder">
        <tr>
            <td align="center" colspan="3" style="height: 25px; background-image: url(../../Images/topMain.gif)">
                <b><span style="font-size: small; font-family: Arial; color: White">Upload Gateway XML from ACT!</span></b>
            </td>
        </tr>

        <tr>
            <td align="right" style="width: 30%" valign="top">
                <asp:Label ID="lblEmailHeader" Font-Bold="True" runat="server" Text="Look up in ACT! by "></asp:Label>&nbsp;
            </td>
            <td align="left">
                &nbsp;<asp:DropDownList ID="lstLookup" runat="server">
                    <asp:ListItem Selected="True">Email</asp:ListItem>
                    <asp:ListItem>Contact</asp:ListItem>
                    <asp:ListItem>CompanyName</asp:ListItem>
                    <asp:ListItem>DBA</asp:ListItem>
                </asp:DropDownList>&nbsp;
                <asp:TextBox ID="txtLookup" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtLookup"
                    EnableClientScript="False" ErrorMessage="Please enter the value to lookup"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td style="height: 5px" colspan="2">
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Search" /></td>
        </tr>
        <tr>
            <td style="height: 5px" colspan="2">
            </td>
        </tr>
    </table>
    </asp:Panel>
 
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <asp:Image ID="imgProgress" runat="server" ImageUrl="~/Images/indicator.gif" /><span
                class="LabelsRed"><b>Retrieving Data...Please Wait</b></span>
        </ProgressTemplate>
    </asp:UpdateProgress>
    
    <asp:UpdatePanel ID="UpdatePanelLeads" runat="server">
        <ContentTemplate>
    <asp:Label ID="lblContactID" runat="server" Font-Bold="True" Visible="False"></asp:Label><br />
    <br />
    <asp:Panel ID="pnlPlatform" runat="server" Visible="False" Width="100%">
        <table style="width: 610px;" class="DivGreen">
            <tr>
                <td align="center" colspan="4" style="background-image: url(../../Images/topMain.gif);
                    height: 25px">
                    <b><span class="MenuHeader">Additional Information</span></b>
                </td>
            </tr>
                        <tr>
            <td align="right" style="width: 25%">
                    <span class="LabelsSmall">Visa/Master (Merchant Number)</span></td>
                <td align="left" style="width: 25%">
                    <asp:TextBox ID="txtVisaMasterNumber" runat="server" TabIndex="21"></asp:TextBox></td>
                <td align="right" style="width: 25%">
                   </td>
                <td align="left" style="width: 25%">
                   </td>
            </tr>

            <tr>
                <td align="right" style="width: 25%">
                    <asp:Label ID="lblPlatform" runat="server" Text="Platform"></asp:Label></td>
                <td align="left" style="width: 25%">
                    <asp:DropDownList ID="lstPlatform" runat="server" AutoPostBack="True" 
                        TabIndex="11">
                        <asp:ListItem Value="15">Omaha</asp:ListItem>
                        <asp:ListItem Value="2">Nashville</asp:ListItem>
                        <asp:ListItem Value="4">Vital</asp:ListItem>
                        <asp:ListItem Value="1">Nova</asp:ListItem>
                        <asp:ListItem Value="7">Global Payments</asp:ListItem>
                        <asp:ListItem Value="11">Paymentech</asp:ListItem>
                    </asp:DropDownList></td>
                <td align="right" style="width: 25%">
                    <!--<span class="LabelsSmall">Login ID</span>--></td>
                <td align="left" style="width: 25%">
                    <!--<asp:TextBox ID="txtLoginID" runat="server" MaxLength="16" TabIndex="16"></asp:TextBox>--></td>
            </tr>
            <tr>
                <td align="right">
                    <span class="LabelsSmall">Merchant ID</span></td>
                <td align="left">
                    <asp:TextBox ID="txtMerchantID" runat="server" TabIndex="12"></asp:TextBox></td>
                <td align="right">
                    <span class="LabelsSmall">Terminal ID</span></td>
                <td align="left">
                    <asp:TextBox ID="txtTerminalID" runat="server" TabIndex="17"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="right">
                    <span class="LabelsSmall">Bank ID Number (BIN)</span></td>
                <td align="left">
                    <asp:TextBox ID="txtBINNumber" runat="server" TabIndex="18"></asp:TextBox></td>
                                    <td align="right">
                    <span class="LabelsSmall">Merchant Type</span></td>
                <td align="left">
                    <asp:DropDownList ID="DropDownListMerchantType" runat="server" TabIndex="22">
                        <asp:ListItem>E-Commerce</asp:ListItem>
                        <asp:ListItem>MOTO</asp:ListItem>
                        <asp:ListItem>Retail</asp:ListItem>
                    </asp:DropDownList></td>
                
            </tr>
            <tr>

            <tr>
                <td align="right">
                    <span class="LabelsSmall">Agent Bank Number</span></td>
                <td align="left">
                    <asp:TextBox ID="txtAgentBankNumber" runat="server" TabIndex="14"></asp:TextBox></td>

            </tr>

                        <td align="right">
                    <span class="LabelsSmall">Agent Chain Number</span></td>
                <td align="left">
                    <asp:TextBox ID="txtAgentChainNumber" runat="server" TabIndex="19"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="right">
                    <span class="LabelsSmall">MCC Category Code</span></td>
                <td align="left">
                    <asp:TextBox ID="txtMCCCode" runat="server" TabIndex="15"></asp:TextBox></td>
                
            </tr>

            <tr>
            <td align="right">
                    <span class="LabelsSmall">Store / Client Number</span></td>
                <td align="left">
                    <asp:TextBox ID="txtStoreNumber" runat="server" TabIndex="20"></asp:TextBox></td>
            </tr>
            <asp:Panel ID="pnlCNPWaive" Visible="false" runat="server">
            <tr>
                <td align="right">
                    <span class="LabelsSmall">Recurring Billing</span></td>
                <td align="left">
                    <asp:DropDownList ID="lstRecurringBilling" runat="server" TabIndex="22">
                        <asp:ListItem>No</asp:ListItem>
                        <asp:ListItem>Yes</asp:ListItem>
                    </asp:DropDownList></td>
                
                <!--<td align="right">
                    <span class="LabelsSmall">Waive first 250 transactions</span></td>
                <td align="left">
                    <asp:DropDownList ID="lstCNPWaive" runat="server" TabIndex="22">
                        <asp:ListItem Selected="True">No</asp:ListItem>
                        <asp:ListItem>Yes</asp:ListItem>
                    </asp:DropDownList></td>-->
            </tr></asp:Panel>
            <asp:Panel ID="pnlCPWaive" Visible="false" runat="server">
            <tr>
            
                <td align="right">
                    <span class="LabelsSmall">Shipped Goods</span></td>
                <td align="left">
                    <asp:DropDownList ID="lstShippedGoods" runat="server" TabIndex="23">
                        <asp:ListItem>No</asp:ListItem>
                        <asp:ListItem>Yes</asp:ListItem>
                    </asp:DropDownList></td>
                
                <td align="right">
                    <span class="LabelsSmall">Waive first 1000 transactions</span></td>
                <td align="left">
                    <asp:DropDownList ID="lstCPWaive" runat="server" TabIndex="22">
                        <asp:ListItem Selected="True">No</asp:ListItem>
                        <asp:ListItem>Yes</asp:ListItem>
                    </asp:DropDownList></td>
                
            </tr>
            <tr>
                <td align="right">
                    <span class="LabelsSmall">Subscription Sales</span></td>
                <td align="left">
                    <asp:DropDownList ID="lstSubsSales" runat="server" TabIndex="24">
                        <asp:ListItem>No</asp:ListItem>
                        <asp:ListItem>Yes</asp:ListItem>
                    </asp:DropDownList></td>
                <td align="right">
                </td>
                <td align="left">
                </td>
            </tr>
            </asp:Panel>
            <tr>
            <asp:Panel ID="pnlDeviceList" Visible="false" runat="server">
                <td align=right>
                    <asp:Label ID=lblDeviceList runat=server visible=false><span class="LabelsSmall">Device List</span></asp:Label></td>
                <td align=left>
                    <asp:DropDownList ID="lstDeviceList" runat="server" TabIndex="25" visible=false>
                        <asp:ListItem>UnattendedTerminal</asp:ListItem>
                        <asp:ListItem>Unconfigured</asp:ListItem>                        
                        <asp:ListItem>SelfServiceTerminal</asp:ListItem>
                        <asp:ListItem>ElectronicCashRegister</asp:ListItem>
                        <asp:ListItem selected="true">PCBasedTerminal</asp:ListItem>
                        <asp:ListItem>Airpay</asp:ListItem>
                        <asp:ListItem>WirelessPOS</asp:ListItem>
                        <asp:ListItem>WebSite</asp:ListItem>
                        <asp:ListItem>DialTerminal</asp:ListItem>
                        <asp:ListItem>VirtualTerminal</asp:ListItem>
                        <asp:ListItem>StoreController</asp:ListItem>
                    </asp:DropDownList></td>
                    </asp:Panel>
                <td align=right>
                    <asp:Label ID=lblRgstState runat=server visible=false><span class="LabelsSmall">Registered State</span></asp:Label></td>
                <td align=left>
                    <asp:TextBox ID=txtRgstState runat=server MaxLength = 2 TabIndex="26" visible=false></asp:TextBox></td>
            </tr>
            <tr>
                <td align=right>
                    <asp:Label ID=lblRgstZip runat=server visible=false><span class="LabelsSmall">Registered Zip Code</span></asp:Label></td>
                <td align=left>
                    <asp:TextBox ID=txtRgstZip runat=server MaxLength = 5 TabIndex="27" visible=false></asp:TextBox></td>
                <td align=right>
                    <asp:Label ID=lblRgstCountry runat=server visible=false><span class="LabelsSmall">Registered Country</span></asp:Label></td>
                <td align=left>
                    <asp:TextBox ID=txtRgstCountry runat=server MaxLength = 2 TabIndex="28" visible=false></asp:TextBox></td>                    
            </tr>
            <tr>
                <td align="center" colspan="4">
                    <asp:Button ID="btnOK" runat="server" OnClick="btnOK_Click" Text="OK" />
                    &nbsp;
                    <asp:Button ID="btnCancelPlatform" OnClick="btnCancelPlatform_Click" runat="server" 
                        Text="Cancel" /></td>
            </tr>
        </table>
    </asp:Panel><br />
    <!--<asp:Label ID="lblDownload" runat="server" Font-Bold="True" Visible="False"></asp:Label><br />-->
    <asp:GridView ID="grdPDF" runat="server" AutoGenerateColumns="False" CellPadding="4"
        ForeColor="#333333" GridLines="Vertical" OnRowCommand="grdPDF_RowCommand">
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <Columns>
            <asp:BoundField DataField="AppId" HeaderText="AppId" SortExpression="AppId">
                <HeaderStyle Font-Names="Arial" Font-Size="Small" />
            </asp:BoundField>
            <asp:BoundField DataField="ContactID" HeaderText="ContactID" SortExpression="ContactID">
                <HeaderStyle Font-Names="Arial" Font-Size="Small" />
            </asp:BoundField>
            <asp:BoundField DataField="Contact" HeaderText="Contact" SortExpression="Contact">
                <HeaderStyle Font-Names="Arial" Font-Size="Small" />
            </asp:BoundField>
            <asp:BoundField DataField="CompanyName" HeaderText="Company" SortExpression="CompanyName">
                <HeaderStyle Font-Names="Arial" Font-Size="Small" />
            </asp:BoundField>
            <asp:BoundField DataField="DBA" HeaderText="DBA" SortExpression="DBA">
                <HeaderStyle Font-Names="Arial" Font-Size="Small" />
            </asp:BoundField>
            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email">
                <HeaderStyle Font-Names="Arial" Font-Size="Small" />
            </asp:BoundField>
            <asp:BoundField DataField="PlatformID" HeaderText="Platform ID">
                <HeaderStyle Font-Names="Arial" Font-Size="Small" />
            </asp:BoundField>
            <asp:BoundField DataField="RepName" HeaderText="Sales Rep">
                <HeaderStyle Font-Names="Arial" Font-Size="Small" />
            </asp:BoundField>
            <asp:BoundField DataField="Processor" HeaderText="Processor">


                <HeaderStyle Font-Names="Arial" Font-Size="Small" />
            </asp:BoundField>
            <asp:BoundField DataField="MerchantStatus" HeaderText="Merchant Status">
                <HeaderStyle Font-Names="Arial" Font-Size="Small" />
            </asp:BoundField>
            <asp:BoundField DataField="Gateway" HeaderText="Gateway">
                <HeaderStyle Font-Names="Arial" Font-Size="Small" />
            </asp:BoundField>
            <asp:BoundField DataField="GatewayStatus" HeaderText="Gateway Status">
                <HeaderStyle Font-Names="Arial" Font-Size="Small" />
            </asp:BoundField>
            <asp:ButtonField CommandName="UploadGateway" Text="Upload Gateway">
                <ItemStyle Font-Names="Arial" Font-Size="Small" Font-Bold="True" />
                <HeaderStyle Font-Names="Arial" Font-Size="Small" />
            </asp:ButtonField>
        </Columns>
        <RowStyle Font-Names="Arial" Font-Size="X-Small" BackColor="#EDF7FF" ForeColor="#333333" />
        <EditRowStyle BackColor="#999999" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
    </asp:GridView>
    </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSubmit" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
