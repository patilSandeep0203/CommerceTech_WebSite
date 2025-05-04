<%@ Page Language="C#" MasterPageFile="site.master" AutoEventWireup="true" CodeFile="start3.aspx.cs"
    Inherits="Questions3" Theme="TextBoxSkin" Title="Commerce Technologies - OnlineApplication" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentHolder" runat="Server">

    <script type="text/javascript" language="javascript">
            function Cover(bottom, top, ignoreSize) {
                var location = Sys.UI.DomElement.getLocation(bottom);
                top.style.position = 'absolute';
                top.style.top = location.y + 'px';
                top.style.left = location.x + 'px';
                if (!ignoreSize) {
                    top.style.height = bottom.offsetHeight + 'px';
                    top.style.width = bottom.offsetWidth + 'px';
                }
            }
    </script>

    <div>
        <center>
            <table style="width: 550px;" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td align="center">
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Label ID="lblError" runat="server" Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <cc1:AnimationExtender ID="AnimationExtender1" runat="server" TargetControlID="lnkbtnViewUnAcc">
                                    <Animations>
                        <OnLoad><OpacityAction AnimationTarget="infoUnAcc" Opacity="0" /></OnLoad>
                        <OnClick>
                            <Sequence>                               
                                <ScriptAction Script="Cover($get('ctl00_MainContent_lnkbtnViewUnAcc'), $get('flyoutUnAcc'));" />
                                <StyleAction AnimationTarget="flyoutUnAcc" Attribute="display" Value="block"/>                            
                                <Parallel AnimationTarget="flyoutUnAcc" Duration=".3" Fps="25">
                                    <Move Horizontal="150" Vertical="0" />
                                    <Resize Width="300" Height="20" />
                                    <Color AnimationTarget="flyoutUnAcc" StartValue="#AAAAAA" EndValue="#f1fafa" Property="style" PropertyKey="backgroundColor" />                                
                                </Parallel>                            
                                <ScriptAction Script="Cover($get('flyoutUnAcc'), $get('infoUnAcc'), true);" />
                                <StyleAction AnimationTarget="infoUnAcc" Attribute="display" Value="block"/>
                                <FadeIn AnimationTarget="infoUnAcc" Duration=".2"/>                            
                                <StyleAction AnimationTarget="flyoutUnAcc" Attribute="display" Value="none"/>
                                <StyleAction AnimationTarget="infoUnAcc" Attribute="height" value="auto" />
                                <Parallel Duration=".5">
                                    <Color AnimationTarget="infoUnAcc" StartValue="#383838" EndValue="#383838" Property="style" PropertyKey="color" />
                                    <Color AnimationTarget="infoUnAcc" StartValue="#febd0d" EndValue="#383838" Property="style" PropertyKey="borderColor" />
                                </Parallel>
                                <Parallel Duration=".5">
                                    <Color AnimationTarget="infoUnAcc" StartValue="#383838" EndValue="#383838" Property="style" PropertyKey="color" />
                                    <Color AnimationTarget="infoUnAcc" StartValue="#febd0d" EndValue="#383838" Property="style" PropertyKey="borderColor" />
                                    <FadeIn AnimationTarget="btnCloseParentUnAcc" MaximumOpacity=".9" />                            
                                </Parallel>
                                
                            </Sequence>
                        </OnClick>
                                    </Animations>
                                </cc1:AnimationExtender>
                                <cc1:AnimationExtender ID="AnimationExtender2" runat="server" TargetControlID="btnCloseUnAcc">
                                    <Animations>
                        <OnClick>
                            <Sequence>
                                <StyleAction AnimationTarget="infoUnAcc" Attribute="overflow" Value="hidden"/>
                                <Parallel AnimationTarget="infoUnAcc" Duration=".3" Fps="15">
                                    <Scale ScaleFactor="0.05" Center="true" ScaleFont="true" FontUnit="px" />
                                    <FadeOut />
                                </Parallel>
                                <StyleAction AnimationTarget="infoUnAcc" Attribute="display" Value="none"/>
                                <StyleAction AnimationTarget="infoUnAcc" Attribute="width" Value="300px"/>
                                <StyleAction AnimationTarget="infoUnAcc" Attribute="height" Value=""/>
                                <StyleAction AnimationTarget="infoUnAcc" Attribute="fontSize" Value="12px"/>
                                <StyleAction AnimationTarget="btnCloseParentUnAcc" Attribute="opacity" value="0" />
                                <StyleAction AnimationTarget="btnCloseParentUnAcc" Attribute="filter" value="alpha(opacity=0)" />                        
                            </Sequence>
                        </OnClick>
                        <OnMouseOver>
                            <Color Duration=".2" StartValue="#FFFFFF" EndValue="#FF0000" Property="style" PropertyKey="color" />                            
                        </OnMouseOver>
                        <OnMouseOut>
                            <Color Duration=".2" EndValue="#FFFFFF" StartValue="#FF0000" Property="style" PropertyKey="color" />                            
                        </OnMouseOut>
                                    </Animations>
                                </cc1:AnimationExtender>
                                <br />
                                <cc1:PopupControlExtender ID="pceQB" runat="server" PopupControlID="pnlQB" TargetControlID="imgQB"
                                    Position="Bottom" />
                                <cc1:PopupControlExtender ID="pceIG" runat="server" PopupControlID="pnlG" TargetControlID="imgQB3"
                                    Position="Bottom" />
                                <cc1:PopupControlExtender ID="pceINT" runat="server" PopupControlID="pnlINT" TargetControlID="imgQB2"
                                    Position="Bottom" />
                                <cc1:PopupControlExtender ID="pceGOPAY" runat="server" PopupControlID="pnlGOPAY" TargetControlID="imgGOPAY"
                                    Position="Bottom" />
                                <cc1:PopupControlExtender ID="pceQBPOS" runat="server" PopupControlID="pnlQBPOS"
                                    TargetControlID="imgQBPOS" Position="Bottom" />
                                <cc1:PopupControlExtender ID="pcePOS" runat="server" PopupControlID="pnlPOS"
                                    TargetControlID="imgPOS" Position="Bottom" />
                                <cc1:PopupControlExtender ID="pceQBDesc" runat="server" PopupControlID="pnlQBDesc" TargetControlID="imgQBDesc"
                                    Position="Bottom" />
                                <cc1:PopupControlExtender ID="pceSageDesc" runat="server" PopupControlID="pnlSageDesc" TargetControlID="imgSageDesc"
                                    Position="Bottom" />
                                <cc1:PopupControlExtender ID="PopupControlExtender2" runat="server" PopupControlID="pnlFiftyCent" TargetControlID="imgFiftyCent"
                                    Position="Bottom" />
                                <cc1:PopupControlExtender ID="PopupControlExtender1" runat="server" PopupControlID="pnlCurrencyDesc" TargetControlID="imgCurrencyDesc"
                                    Position="Bottom" />
                                <span class="LabelsBody">Is your business listed in the following list?</span><br />
                                <asp:LinkButton runat="server" ID="lnkbtnViewUnAcc" CssClass="Link" OnClientClick="return false;">View List of Unacceptable Businesses</asp:LinkButton>
                                <div id="flyoutUnAcc" style="z-index: 2; display: none; border: solid 1px #D0D0D0;
                                    background-color: #FFFFFF; overflow: hidden;">
                                    &nbsp;</div>
                                <div id="infoUnAcc" style="z-index: 2; display: none; font-size: 12px; border: solid 1px #CCCCCC;
                                    background-color: #ffffff; width: 340px; padding: 5px; font-family: Arial; font-size: 10pt;">
                                    <div style="float: right; filter: alpha(opacity=0);" id="btnCloseParentUnAcc">
                                        <asp:LinkButton ID="btnCloseUnAcc" runat="server" OnClientClick="return false;" Text=""
                                            Style="text-align: right; border: outset thin white; padding: 5px; text-decoration: none;
                                            background-color: #666666; color: White; text-align: center; font-weight: bold;"
                                            ToolTip="Close">X</asp:LinkButton>
                                    </div>
                                    <b><span class="LabelsBody">Unacceptable Businesses</span></b>
                                    <div class="DivGreen">
                                        <span class="LabelsBodySmall">Any Product or Service considered illegal</span>
                                        <br />
                                        <span class="LabelsBodySmall">Auction (MOTO/Internet)</span>
                                        <br />
                                        <span class="LabelsBodySmall">Auto Lending</span>
                                        <br />
                                        <span class="LabelsBodySmall">Bath Salts, Herbal Incense, or similar products</span>
                                        <br />

                                        <span class="LabelsBodySmall">Cannabidiol (CBD) (Card Present) </span>
                                        <br />
                                        <span class="LabelsBodySmall">Check Cashing, Wire Transfers and Other Payment Related Services</span>
                                        <br />
                                        <span class="LabelsBodySmall">Collections and Collection Agencies</span>
                                        <br />
                                        <span class="LabelsBodySmall">Credit Counseling and Credit Repair</span>
                                        <br />
                                        <span class="LabelsBodySmall">Drug Paraphernalia (MOTO/Internet)</span>
                                        <br />

                                        <span class="LabelsBodySmall">Escort Services</span>
                                        <br />
                                        <span class="LabelsBodySmall">Extended Warranty Programs</span>
                                        <br />
                                        <span class="LabelsBodySmall">Factoring, Third Party Processors and Aggregators</span>
                                        <br />
                                        <span class="LabelsBodySmall">Firearms and Ammunition (MOTO/Internet)</span>
                                        <br />
                                        <span class="LabelsBodySmall">Gambling (MOTO/Internet)</span>
                                        <br />
                                        <span class="LabelsBodySmall">Loading of prepaid cards or mobile wallets</span>
                                        <br />
                                        <span class="LabelsBodySmall">Loan Modification</span>
                                        <br />
                                        <span class="LabelsBodySmall">Marijuana Dispensaries</span>
                                        <br />
                                        <span class="LabelsBodySmall">Membership or Future Delivery beyond 1 year</span>
                                        <br />
                                        <span class="LabelsBodySmall">Natural Sexual Enhancement Products</span>
                                        <br />
                                        <span class="LabelsBodySmall">Pharmacies (MOTO/Internet)</span>
                                        <br />
                                        <span class="LabelsBodySmall">Pre-paid phone cards</span>
                                        <br/>
                                        <span class="LabelsBodySmall">Pyramid Sales</span>
                                        <br />
                                        <span class="LabelsBodySmall">Racism, Violence, Abuse, Discrimination or Other Immoral Activities</span>
                                        <br />
                                        <span class="LabelsBodySmall">Replica and Counterfeit Products</span>
                                        <br />
                                        <a href="Global.asax">Global.asax</a>
                                        <span class="LabelsBodySmall">Technical Support (MOTO/Internet)</span><a href="Global.asax">Global.asax</a>
                                        <br />
                                        <span class="LabelsBodySmall">Timeshare Programs</span>
                                        <br />
                                        <span class="LabelsBodySmall">Tobacco Products (MOTO/Internet)</span>
                                        <br /> 
                                        <span class="LabelsBodySmall">Travel Clubs</span>
                                        <br />
                                    </div>
                                </div>
                                <br />
                                <asp:RadioButton runat="server" ID="rbtnUnaccBusY" Text="Yes" GroupName="rbtnUnaccBus"
                                    AutoPostBack="true" OnCheckedChanged="rdbPOS_CheckedChanged" />
                                <asp:RadioButton runat="server" ID="rbtnUnaccBusN" Text="No" GroupName="rbtnUnaccBus"
                                    AutoPostBack="true" OnCheckedChanged="rdbPOS_CheckedChanged" />
                                <br />
                                <br />
                                <asp:Panel ID="pnl50Pct" runat="server" Width="80%" Visible="False">
                                    <span class="LabelsBody">Do US cardholders account for at least 50% of credit cards
                                        accepted?</span>
                                    <asp:Image ID="imgFiftyCent" runat="server" ImageUrl="/OnlineApplication/Images/help.gif"
                                        ToolTip="US Cardholders" /><br />
                                    <asp:RadioButton runat="server" ID="rbtn50PctY" Text="Yes" GroupName="rbtn50Pct"
                                        AutoPostBack="True" OnCheckedChanged="rdbPOS_CheckedChanged" />
                                    <asp:RadioButton runat="server" ID="rbtn50PctN" Text="No" GroupName="rbtn50Pct" AutoPostBack="True"
                                        OnCheckedChanged="rdbPOS_CheckedChanged" />
                                <br />
                                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                        <ContentTemplate>
                                            <asp:Panel Style="display: none; z-index: 1;" ID="pnlFiftyCent" runat="server" Width="400px"
                                                CssClass="DivHelp">
                                                To be considered a U.S. Merchant Account, you need to have at least half your company's clientele be U.S. based. If you have less than 50%, you will be redirected to the International Merchant Account Application where you will be able to finish filling out your application.
                                            </asp:Panel>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </asp:Panel>
                                <br />
                                <!--<asp:Panel ID="pnlFEIN" runat="server" Width="80%" Visible="False">
                                    <span class="LabelsBody">If a legal entity such as a Sole Preprietorship or other entity
                                        legally registered in the US, can you provide an FEIN (which can be established
                                        by filling out and submitting an <a class="Link" href="https://sa2.www4.irs.gov/modiein/individual/index.jsp"
                                            target="_blank" title="https://sa2.www4.irs.gov/modiein/individual/index.jsp">application</a>).</span><br />
                                    <asp:RadioButton runat="server" ID="rbtnFEINY" Text="Yes" GroupName="rbtnFEIN" AutoPostBack="True"
                                        OnCheckedChanged="rdbPOS_CheckedChanged" />
                                    <asp:RadioButton runat="server" ID="rbtnFEINN" Text="No" GroupName="rbtnFEIN" AutoPostBack="True"
                                        OnCheckedChanged="rdbPOS_CheckedChanged" />
                                </asp:Panel>
                                <br />-->
                                <asp:Panel ID="pnlOtherCurrencies" runat="server" Width="80%" Visible="False">
                                    <span class="LabelsBody">Do you want to offer your products and services in currencies
                                        other than the U. S. Dollar?</span>
                                    <asp:Image ID="imgCurrencyDesc" runat="server" ImageUrl="/OnlineApplication/Images/help.gif"
                                        ToolTip="Other Currency" /><br />
                                    <asp:RadioButton runat="server" ID="rbtnOtherCurrenciesY" Text="Yes" GroupName="rbtnOtherCurrencies"
                                        AutoPostBack="True" OnCheckedChanged="rdbPOS_CheckedChanged" />
                                    <asp:RadioButton runat="server" ID="rbtnOtherCurrenciesN" Text="No" GroupName="rbtnOtherCurrencies"
                                        AutoPostBack="True" OnCheckedChanged="rdbPOS_CheckedChanged" />
                                    <br />
                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                        <ContentTemplate>
                                            <asp:Panel Style="display: none; z-index: 1;" ID="pnlCurrencyDesc" runat="server" Width="400px"
                                                CssClass="DivHelp">
                                                If your company uses currencies other than the U.S. Dollar, you will be redirected to the International Merchant Account Application where you can finish filling out your information.
                                            </asp:Panel>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </asp:Panel>
                                <br />
                                <asp:Panel ID="pnlSage" runat="server" Width="80%" Visible="False">
                                    <span class="LabelsBody">Do you have or plan to use Sage ERP software?</span>
                                    <asp:Image ID="imgSageDesc" runat="server" ImageUrl="/OnlineApplication/Images/help.gif"
                                        ToolTip="Sage Description" /><br />
                                    <asp:RadioButton ID="rdbSageY" runat="server" GroupName="Sage" OnCheckedChanged="rdbPOS_CheckedChanged"
                                        Text="Yes" AutoPostBack="True" />
                                    <asp:RadioButton ID="rdbSageN" runat="server" GroupName="Sage" OnCheckedChanged="rdbPOS_CheckedChanged"
                                        Text="No" AutoPostBack="True" />
                                    <br />
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate>
                                            <asp:Panel Style="display: none; z-index: 1;" ID="pnlSageDesc" runat="server" Width="400px"
                                                CssClass="DivHelp">
                                                DacEasy by Sage, Sage Accpac ERP, Sage BusinessWorks, <asp:HyperLink ID="HyperLinkSageExchange" runat="server" CssClass="LinkSmall" Font-Names="Arial"
                                                    NavigateUrl="https://commercetech.com/sage/"
                                                    Target="_blank"><!--https://firstaffiliates.com/Affiliatewiz/SageExchange.aspx-->Sage Exchange</asp:HyperLink>, Sage Fundraising 50 & 100,
                                                Sage Intergy, Sage MAS 90, 200 & 500, Sage Millenium, Sage One, Sage Pro ERP and Simply Accounting by Sage are 
                                                business financial management and accounting programs that can be used with Sage merchant accounts.
                                            </asp:Panel>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </asp:Panel>
                                <br />
                                <asp:Panel ID="pnlQB1" runat="server" Width="80%" Visible="False">
                                    <span class="LabelsBody">Do you have or plan to use QuickBooks, or QuickBooks POS?</span>
                                    <asp:Image ID="imgQBDesc" runat="server" ImageUrl="/OnlineApplication/Images/help.gif"
                                        ToolTip="QuickBooks Description" /><br />
                                    <asp:RadioButton ID="rdbQBYes" runat="server" GroupName="QB" OnCheckedChanged="rdbPOS_CheckedChanged"
                                        Text="Yes" AutoPostBack="True" />
                                    <asp:RadioButton ID="rdbQBNo" runat="server" GroupName="QB" OnCheckedChanged="rdbPOS_CheckedChanged"
                                        Text="No" AutoPostBack="True" />
                                    <br />
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:Panel Style="display: none; z-index: 1;" ID="pnlQBDesc" runat="server" Width="400px"
                                                CssClass="DivHelp">
                                                QuickBooks and Quickbooks POS are business financial management and accounting programs
                                                developed by Intuit. You must select yes if you plan to use either of
                                                these programs in the future and would like to use the integrated merchant account
                                                services.
                                            </asp:Panel>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </asp:Panel>
                                <br />
                                <asp:Panel ID="pnlQB2" runat="server" Width="80%" Visible="False">
                                    <span class="LabelsBody">How do you plan to accept credit cards?</span><br />
                                    <!-- new code-->
                                    <asp:RadioButton ID="rdbGOPAY" runat="server" GroupName="POSQB" Text="Mobile Phone" OnCheckedChanged="rdbPOS_CheckedChanged"
                                        AutoPostBack="True" /><asp:Image ID="imgGOPAY" runat="server" ImageUrl="/OnlineApplication/Images/help.gif"
                                            ToolTip="GoPayment Description" />
                    
                                    <!-- ends -->
                                    <asp:RadioButton ID="rdbQB" runat="server" GroupName="POSQB" Text="QuickBooks" OnCheckedChanged="rdbPOS_CheckedChanged"
                                        AutoPostBack="True" /><asp:Image ID="imgQB" runat="server" ImageUrl="/OnlineApplication/Images/help.gif"
                                            ToolTip="QuickBooks Description" />
                                    <asp:RadioButton ID="rdbQBPOS" runat="server" GroupName="POSQB" Text="QuickBooks POS" OnCheckedChanged="rdbPOS_CheckedChanged" 
                                        AutoPostBack="True" Visible="False"/><asp:Image ID="imgQBPOS" runat="server" ImageUrl="/OnlineApplication/Images/help.gif" 
                                            ToolTip="QuickBooks POS Description" Visible="False"/>
                                    <asp:RadioButton ID="rdbPOS" runat="server" GroupName="POSQB" Text="Other POS" OnCheckedChanged="rdbPOS_CheckedChanged"
                                        AutoPostBack="True" /><asp:Image ID="imgPOS" runat="server" ImageUrl="/OnlineApplication/Images/help.gif"
                                            ToolTip="POS Description" />
                                    <br />
                                    <br />
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
                                            <asp:Panel Style="display: none; z-index: 1;" ID="pnlQB" runat="server" Width="400px"
                                                CssClass="DivHelp">
                                                Download
                                                <asp:HyperLink ID="HyperLink1" runat="server" CssClass="LinkSmall" Font-Names="Arial"
                                                    NavigateUrl="https://commercetech.com/payment-integrations/"
                                                    Target="_blank">Plug-In for QuickBooks</asp:HyperLink>
                                                for more information.
                                            </asp:Panel>
                                            
                                            <asp:Panel Style="display: none; z-index: 1;" ID="pnlGOPAY" runat="server" Width="400px"
                                                CssClass="DivHelp">
                                                Download
                                                <asp:HyperLink ID="HyperLink3" runat="server" CssClass="LinkSmall" Font-Names="Arial"
                                                    NavigateUrl="https://commercetech.com/gopayment-by-intuit/"
                                                    Target="_blank">QuickBooks GoPayment</asp:HyperLink>
                                                for more information.
                                            </asp:Panel>
                                            <asp:Panel Style="display: none; z-index: 1;" ID="pnlQBPOS" runat="server" Width="330px"
                                                CssClass="DivHelp">
                                                Download
                                                <asp:HyperLink ID="HyperLink2" runat="server" CssClass="LinkSmall" Font-Names="Arial"
                                                    NavigateUrl="https://commercetech.com/quickbooks-pos/"
                                                    Target="_blank">QuickBooks POS</asp:HyperLink>
                                                for more information.
                                            </asp:Panel>
                                            <asp:Panel Style="display: none; z-index: 1;" ID="pnlPOS" runat="server" Width="330px"
                                                CssClass="DivHelp">
                                                Other Point of Sale (POS) products includes <asp:HyperLink ID="HyperLink6" runat="server" CssClass="LinkSmall" Font-Names="Arial"
                                                    NavigateUrl="https://commercetech.com/pos-equipment-bundles/"
                                                    Target="_blank">Software</asp:HyperLink>, <asp:HyperLink ID="HyperLink7" runat="server" CssClass="LinkSmall" Font-Names="Arial"
                                                    NavigateUrl="https://commercetech.com/hardware/"
                                                    Target="_blank">Equipment</asp:HyperLink> or <asp:HyperLink ID="HyperLink8" runat="server" CssClass="LinkSmall" Font-Names="Arial"
                                                    NavigateUrl="https://commercetech.com/payment-gateways/"
                                                    Target="_blank">Payment Gateway</asp:HyperLink>.
                                            </asp:Panel>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </asp:Panel>
                                 <asp:Panel ID="pnlQB3" runat="server" Width="80%" Visible="False">
                                    <span class="LabelsBody">Which of the following Services you want along with QuickBooks?</span><br />
                                    
                                    <asp:CheckBox runat="server" id="rdbINT" Text="Internet" OnCheckedChanged="rdbPOS_CheckedChanged" 
                                    AutoPostBack="true" /><asp:Image ID="imgQB2" runat="server" ImageUrl="/OnlineApplication/Images/help.gif"
                                            ToolTip="QuickBooks Description" />
                                    
                                    <asp:CheckBox runat="server" id="rdbGP" Text="Mobile Phone" OnCheckedChanged="rdbPOS_CheckedChanged" 
                                    AutoPostBack="true" /><asp:Image ID="imgQB3" runat="server" ImageUrl="/OnlineApplication/Images/help.gif"
                                            ToolTip="GoPayment Description" />
                                            <br />
                                            <br />
                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                        <ContentTemplate>
                                            
                                            <asp:Panel Style="display: none; z-index: 1;" ID="pnlG" runat="server" Width="400px"
                                                CssClass="DivHelp">
                                                Download
                                                <asp:HyperLink ID="HyperLink5" runat="server" CssClass="LinkSmall" Font-Names="Arial"
                                                    NavigateUrl="https://commercetech.com/gopayment-by-intuit/"
                                                    Target="_blank">QuickBooks GoPayment</asp:HyperLink>
                                                for more information.
                                            </asp:Panel>
                                            <asp:Panel Style="display: none; z-index: 1;" ID="pnlINT" runat="server" Width="400px"
                                                CssClass="DivHelp">
                                                Download
                                                <asp:HyperLink ID="HyperLink4" runat="server" CssClass="LinkSmall" Font-Names="Arial"
                                                    NavigateUrl="https://commercetech.com/payment-integrations/"
                                                    Target="_blank">Plug-In for QuickBooks</asp:HyperLink>
                                                for more information.
                                            </asp:Panel>
                                           
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    </asp:Panel>
                                <!--<asp:Panel ID="pnlQBQuestion3" runat="server" Width="80%" Visible="False">
                                 <br /><asp:Label ID="lblQBQuestion3" runat="server" Font-Names="Arial" Text="Do you want to process through the QuickBooks gateway or a third party gateway?"></asp:Label><br />
                                <asp:RadioButton ID="rdbQBGateway" runat="server" GroupName="Gateway" Text="QuickBooks Gateway"
                                    OnCheckedChanged="rdbPOS_CheckedChanged" Font-Names="Arial" Font-Size="11pt"
                                    AutoPostBack="True" />
                                <asp:RadioButton ID="rdbThirdPartyGateway" runat="server" GroupName="Gateway" Text="Third Party"
                                    OnCheckedChanged="rdbPOS_CheckedChanged" Font-Names="Arial" Font-Size="11pt"
                                    AutoPostBack="True" /></asp:Panel>
                            
                            <asp:Panel ID="pnlQBQuestion4" runat="server" Width="80%" Visible="False">
                                <br /><asp:Label ID="lblQBShoppingCart" runat="server" Font-Names="Arial" Text="If you have a shopping cart, is it compatible with the QuickBooks Gateway?"></asp:Label><br />
                                <asp:RadioButton ID="rdbSCYes" runat="server" GroupName="ShoppingCart" Text="Yes"
                                    OnCheckedChanged="rdbPOS_CheckedChanged" Font-Names="Arial" Font-Size="11pt"
                                    AutoPostBack="True" />
                                <asp:RadioButton ID="rdbSCNo" runat="server" GroupName="ShoppingCart" Text="No" OnCheckedChanged="rdbPOS_CheckedChanged"
                                    Font-Names="Arial" Font-Size="11pt" AutoPostBack="True" /></asp:Panel>-->
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    	
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Button ID="btnStartOver" runat="server" Text="Start Over" OnClick="btnStart_Click" />
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                        <asp:Button ID="btnContinue" runat="server" Text="Continue" OnClick="btnContinue_Click" />
                    </td>
                </tr>
            </table>
        </center>
    </div>
</asp:Content>
