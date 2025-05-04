<%@ Page Language="C#" MasterPageFile="~/AgentMisc.master" AutoEventWireup="true"
    CodeFile="DocsLoginsAgent.aspx.cs" Inherits="DocsLoginsAgent" Title="Commerce Technologies - Partner Portal"
    Theme="AppTheme" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">

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

    <asp:ScriptManager ID="ScriptManagerDocs" runat="server">
    </asp:ScriptManager>
    <cc1:AnimationExtender ID="AnimationExtender1" runat="server" TargetControlID="lnkbtniPayment">
        <Animations>
                        <OnLoad><OpacityAction AnimationTarget="info" Opacity="0" /></OnLoad>
                        <OnClick>
                            <Sequence>                               
                                <ScriptAction Script="Cover($get('ctl00_ctl00_RootContent_MainContent_lnkbtniPayment'), $get('flyout'));" />
                                <StyleAction AnimationTarget="flyout" Attribute="display" Value="block"/>                            
                                <Parallel AnimationTarget="flyout" Duration=".1" Fps="25">
                                    <Move Horizontal="-100" Vertical="-250" />                                    
                                    <Color AnimationTarget="flyout" StartValue="#AAAAAA" EndValue="#f9f9d9" Property="style" PropertyKey="backgroundColor" />                                
                                </Parallel>                            
                                <ScriptAction Script="Cover($get('flyout'), $get('info'), true);" />
                                <StyleAction AnimationTarget="info" Attribute="display" Value="block"/>
                                <FadeIn AnimationTarget="info" Duration=".2"/>                            
                                <StyleAction AnimationTarget="flyout" Attribute="display" Value="none"/>
                                <StyleAction AnimationTarget="info" Attribute="height" value="auto" />
                                <Parallel Duration=".2">
                                    <Color AnimationTarget="info" StartValue="#383838" EndValue="#383838" Property="style" PropertyKey="color" />
                                    <Color AnimationTarget="info" StartValue="#febd0d" EndValue="#383838" Property="style" PropertyKey="borderColor" />
                                </Parallel>
                                <Parallel Duration=".2">
                                    <Color AnimationTarget="info" StartValue="#383838" EndValue="#383838" Property="style" PropertyKey="color" />
                                    <Color AnimationTarget="info" StartValue="#febd0d" EndValue="#383838" Property="style" PropertyKey="borderColor" />
                                    <FadeIn AnimationTarget="btnCloseParent" MaximumOpacity=".9" />                            
                                </Parallel>
                                
                            </Sequence>
                        </OnClick>
        </Animations>
    </cc1:AnimationExtender>
    <cc1:AnimationExtender ID="AnimationExtender2" runat="server" TargetControlID="btnClose">
        <Animations>
                        <OnClick>
                            <Sequence>
                                <StyleAction AnimationTarget="info" Attribute="overflow" Value="hidden"/>
                                <Parallel AnimationTarget="info" Duration=".1" Fps="15">
                                    <Scale ScaleFactor="0.05" Center="true" ScaleFont="true" FontUnit="px" />
                                    <FadeOut />
                                </Parallel>
                                <StyleAction AnimationTarget="info" Attribute="display" Value="none"/>
                                <StyleAction AnimationTarget="info" Attribute="width" Value="500px"/>
                                <StyleAction AnimationTarget="info" Attribute="height" Value=""/>
                                <StyleAction AnimationTarget="info" Attribute="fontSize" Value="12px"/>
                                <StyleAction AnimationTarget="btnCloseParent" Attribute="opacity" value="0" />
                                <StyleAction AnimationTarget="btnCloseParent" Attribute="filter" value="alpha(opacity=0)" />                        
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
    <table border="0" cellspacing="0" class="SilverBorder" style="width: 850px">
        <tr>
            <td style="height: 30px; background-color: #565294;" colspan="3"
                align="center">
                <span class="MenuHeader"><strong>Applications, Forms, Documents and Logins</strong></span></td>
        </tr>
        <tr>
            <td colspan="3" align="center" style="height: 30px">
                <span class="LabelsBlue"><strong>Click on any link below to access the website or form.</strong></span>
            </td>
        </tr>
        <tr>
            <td valign="top" align="center">
                <table width="95%" border="0" cellspacing="2" class="SilverBorder">
                    <tr>
                        <td style="height: 20px; background-color: #565294;">
                            <strong><span class="MenuHeader">Other Logins</span></strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:HyperLink CssClass="One" ID="lnkMail" runat="server" Target="_blank" NavigateUrl="http://email.secureserver.net">Webmail</asp:HyperLink>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:HyperLink CssClass="One" ID="lnkACTforWeb" runat="server" Target="_blank" NavigateUrl="https://www.apps.commercetech.com/apfw">ACT for Web</asp:HyperLink>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:HyperLink CssClass="One" ID="lnkVerifone" runat="server" Target="_blank" NavigateUrl="http://www.verifonezone.com">VeriFone Zone</asp:HyperLink>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:HyperLink CssClass="One" ID="lnkShowmyPC" runat="server" Target="_blank" NavigateUrl="http://ecenow.showmypc.com/">Remote Access (ShowMyPC)</asp:HyperLink>&nbsp;</td>
                    </tr>
                    </table>
                <br />
                <table border="0" cellspacing="2" style="width: 95%" class="SilverBorder">
                    <tr>
                        <td style="height: 20px; background-color: #565294;">
                            <b><span class="MenuHeader">POS</span></b></td>
                    </tr>
                    <tr>
                        <td style="height: 20px; background-color: #565294;">
                            <b><span class="MenuHeader">Tablet</span></b></td>
                    </tr>
                    <tr>
                        <td style="background-image: url('Images/homeback.gif');" class="SilverBorder">
                            <b><span class="LabelsRed">ShopKeep</span></b></td>
                    </tr>
                                        <tr>
                        <td>
                            <a class="One" href="http://www.shopkeep.com/signup-pr/partner-lead-submission/" target="_blank">ShopKeep POS Lead form</a></td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="http://www.shopkeep.com/partner-portal" target="_blank">ShopKeep Partner Portal</a></td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="https://verifone.webex.com/verifone/lsr.php?RCID=174088b5b75e42f1b0ba813c8fcf774d" target="_blank">
                                Introduction to Verifone Cloud POS (Recording)</a>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="/Software-POS/Verifone/Cloud POS/Marketing/Verifone_Cloud_POS_Presentation.pdf"
                                target="_blank">Verifone Cloud POS Presentation</a>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="/Software-POS/Verifone/Cloud POS/Marketing/Verifone_Cloud_POS_Reseller_kit.pdf"
                                target="_blank">Verifone Cloud POS Reseller kit</a>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-image: url('Images/homeback.gif');" class="SilverBorder">
                            <b><span class="LabelsRed">Revel</span></b></td>
                    </tr>

                    <tr>
                        <td>
                            <a class="One" href="http://revelsystems.com/revel-university/"
                                target="_blank">Revel University </a>
                        </td>
                    </tr>

                    <tr>
                        <td style="height: 20px; background-color: #565294;">
                            <b><span class="MenuHeader">Desktop</span></b></td>
                    </tr>
                    <tr>
                        <td style="background-image: url('Images/homeback.gif');" class="SilverBorder">
                            <b><span class="LabelsRed">QuickBooks POS</span></b></td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="http://dlm2.download.intuit.com/akdlm/SBD/QuickBooks/2018/Latest/QuickBooksPOSV18Trial30.exe"
                                target="_blank">Intuit POS v18 Desktop Trial Download </a>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-image: url('Images/homeback.gif');" class="SilverBorder">
                            <b><span class="LabelsRed">pcAmerica</span></b></td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="https://download2.pcamerica.com/12.7/"
                                target="_blank">pcAmerica trial version download </a>
                        </td>
                    </tr>
                    
                    <tr>
                        <td>
                            <a class="One" href="http://faq.pcamerica.com/"
                                target="_blank">pcAmerica F&Q </a>
                        </td>
                    </tr>

                    
                    </table>
                <table border="0" cellspacing="2" style="width: 95%" class="SilverBorder">
                    <tr>
                        <td style="height: 20px; background-color: #565294;">
                            <strong><span class="MenuHeader">Miscellaneous</span></strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="../Misc Forms/Equipment Sales Agreement.pdf" target="_blank">Equipment sales agreement</a>
                        </td>
                    </tr>
                    <tr style="font-size: 12pt">
                        <td>
                            <a class="One" href="http://www.fedex.com/us/" target="_blank">FedEx Tracking</a></td>
                    </tr>
                    </table>                
                <br />
                <table border="0" cellspacing="2" style="width: 95%;" class="SilverBorder">
                    <tr>
                        <td style="height: 20px; background-color: #565294;">
                            <strong><span class="MenuHeader">Advertising and Marketing</span></strong></td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="../Advertising-Marketing/Email Marketing Letters-Email Flyers/E-mail Flyers/Word Format/General Commerce Technologies Flyer.doc"
                                target="_blank">Commerce Technologies Flyer</a>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="../Advertising-Marketing/Business Cards-Letterhead-Envelopes/Business Card Template.pdf"
                                target="_blank">Business Card Template</a>
                        </td>
                </table>
                <br />
                <!--<table border="0" cellspacing="2" style="width: 95%" class="SilverBorder">
                    <tr>
                        <td style="height: 20px; background-color: #565294;">
                            <b><span class="MenuHeader">Leasing</span></b></td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="../Leasing%20Info/Lease%20Factors%20and%20Funding%20Procedures.xls"
                                target="_blank">Lease Factors and Funding Procedures</a></td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="../Leasing%20Info/Lease%20Equipment%20Caps.doc" target="_blank">
                                Lease Payment Caps</a>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-image: url('Images/homeback.gif');" class="SilverBorder">
                            <b><span style="font-family: Arial; font-size: small; color: Red">Northern Leasing Systems</span></b></td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="../Northern%20Leasing/Northern%20Leasing%20Agreement - Standard.pdf" target="_blank">Northern Lease Agreement (Standard)</a></td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="../Northern%20Leasing/Northern%20Leasing%20Agreement - SD, KS, TN, PA & VT.pdf" target="_blank">Northern Lease Agreement (5 States)</a></td>
                    </tr>

                    </table>-->              
                <br />
   
            </td>
            <td valign="top" align="center" width="34%">
                <table border="0" cellspacing="2" style="width: 95%;" class="SilverBorder">
                    <tr>
                        <td style="height: 20px; background-color: #565294; width: 100%">
                            <b><span class="MenuHeader">Processors</span></b></td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="../Comparison/Processor%20Buy%20Rate%20Comparison.xls" target="_blank">
                                Buy rate Comparison Chart</a></td>
                    </tr>
                                        <tr>
                        <td>
                            <a class="One" href="../Comparison/Processor Minimum Sell Rate Comparison Rev.3.21.2005.xls" target="_blank">
                                Sell rate Comparison Chart</a></td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="../Comparison/Processor%20Buy%20Rate%20Comparison.xls" target="_blank">
                                Comparison Chart</a></td>
                    </tr>
                    <tr style="font-size: 12pt">
                        <td>
                            <a class="One" href="../PriceComparison/Rate Comparison Template - 3 Tier.xls" target="_blank">
                                Rate Comparison Template - 3 Tier</a></td>
                    </tr>
                    <tr style="font-size: 12pt">
                        <td>
                            <a class="One" href="../PriceComparison/Rate Comparison Template - Interchange Plus.xls"
                                target="_blank">Rate Comparison Template - Interchange Plus</a></td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="https://usa.visa.com/dam/VCOM/download/merchants/visa-usa-interchange-reimbursement-fees-2016-april.pdf"
                                target="_blank">Visa USA Interchange Rates</a></td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="http://www.mastercard.com/us/merchant/pdf/MasterCard_Interchange_Rates_and_Criteria.pdf"
                                target="_blank">MasterCard Worldwide U.S. and Interregional<br />
                                Interchange Rates</a></td>
                    </tr>
                    <tr>
                        <td style="background-image: url('Images/homeback.gif');" class="SilverBorder">
                            <b><span class="LabelsRed">Sage Payment Solutions</span></b></td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="../Sage/Forms/Applications/Application for Merchant Account.pdf" target="new">Merchant
                                Application</a></td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="../Sage/Forms/Sample Statement.pdf" target="_blank">Merchant
                                Sample Statement</a></td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="../Sage/Forms/Merchant Account Cancellation Form.doc" target="new">
                                Merchant Account Cancellation Form</a></td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="../Sage/Programs/Product Grid Chase Paymentech.xls" target="_blank">
                                Product Grid Chase Paymentech</a></td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="../Sage/Programs/Product Grid TSYS-Retail 111110.xls" target="_blank">
                                Product Grid TSYS</a></td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="../Sage/Forms/ADD DEBIT & EBT.doc" target="_blank">Debit Addendum</a></td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="https://uno.eftsecure.net/SalesCenter/" target="new">
                                Sales Center</a></td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="http://www.sagequickticket.com/" target="new">
                                Quick Ticket Login</a></td>
                    </tr>
                    
                    <tr>
                        <td>
                            <a class="One" href="https://sagena.webex.com/sagena/lsr.php?AT=pb&SP=MC&rID=9766547&rKey=73fec82f0bb304e9"
                                target="new">Bank Card 101 Training</a></td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="https://sagena.webex.com/sagena/lsr.php?AT=pb&SP=MC&rID=9836102&rKey=175aed951aae4b18"
                                target="new">UNO Training</a></td>
                    </tr>
                    <td>
                            <a class="One" href="../Sage/Forms/Rate Change - Bankcard Rev 0615.DOCX" target="new">
                                Rate change - bankcard</a></td>
                    </tr>
                    <tr>
                        <td style="background-image: url('Images/homeback.gif');" class="SilverBorder">
                            <b><span class="LabelsRed">Intuit</span></b></td>
                    </tr> 
                    
                                        <tr>
                        <td>
                            <a class="One" href="https://www.apps.commercetech.com/Intuit%20Marketing/QSP%20Pricing.xlsx
" target="_blank">Price Sheet</a></td>
                    </tr>
                                       
                    <!--tr>
                        <td>
                            <a class="One" href="http://www.apps.commercetech.com/quickbooks" target="_blank">QuickBooks
                                Merchant Application</a></td>
                    </tr-->
                    
                   

                    <tr>
                        <td>
                            <a class="One" href="http://dlm2.download.intuit.com/akdlm/SBD/QuickBooks/2013/R1/QuickBooksPOSV11Trial30.exe" target="_blank">POS 2013 30 day trial</a></td>
                    </tr>

                    
                    <tr style="font-size: 12pt">
                        <td style="background-image: url('Images/homeback.gif');" class="SilverBorder">
                            <b><span class="LabelsRed">iPayment Inc.</span></b></td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="../IPayment%20Forms/Applications/IPayment%20Application.pdf"
                                target="new">iPayment Merchant Application</a></td>
                    </tr>
                    <tr>
                        <td>
                        <!--<a class="One" href="file://Server-R310/shared/Processors/iPayment/Forms/Debit Card Acceptance/PIN Debit Card Addendum.pdf"
                                target="new">Debit Addendum</a>-->
                            <a class="One" href="../Ipayment%20Forms/Debit%20Card%20Acceptance/PIN%20Debit%20Card%20Addendum.pdf"
                                target="new">Debit Addendum</a></td>
                    </tr>
                    <!--<tr>
                        <td>
                            <a class="One" href="../iPayment%20Forms/POS Retail Product Pricing & Flow.pdf" target="_blank">
                                POS Retail Product Pricing & Flow</a></td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="../iPayment%20Forms/POS Restaurant Product Pricing & Flow.pdf"
                                target="_blank">POS Restaurant Product Pricing & Flow</a></td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="../iPayment%20Forms/POS Rebate Promo.pdf" target="_blank">POS Rebate Promotion</a></td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="../iPayment%20Forms/Comstar for Quickbooks Setup Form.pdf" target="_blank">
                                Comstar for QuickBooks Setup</a></td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="../IPayment%20Forms/iPayment%20Reserve.doc" target="_blank">iPayment
                                Reserve Form</a></td>
                    </tr>-->
                    <tr>
                        <td>
                            <asp:LinkButton ID="lnkbtniPayment" runat="server" OnClientClick="return false;"
                                CssClass="One">Other iPayment Forms and Guidelines</asp:LinkButton>
                            <!--<a  class="One" href="iPayment.aspx" target="_blank"><span style="font-family:Arial; font-size:small">Other iPayment Forms
                                and Guidelines</span></a>-->
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="../Ipayment%20Forms/Ipayment%20Product%20Matrix.xls" target="_blank">
                                iPayment Product Matrix</a></td>
                    </tr>
                    
                                                            <tr>
                        <td style="background-image: url('Images/homeback.gif');" class="SilverBorder">
                            <b><span class="LabelsRed">CardConnect</span></b></td>
                    </tr>
                        
                        <tr>
                            <td>
                                <a class="One" href="../CardConnect/Forms/Card Connect Example Statement" target="_blank">Sample Statement</a></td>
                        </tr>
                    
                    </table>
                <br />
                <table border="0" cellspacing="2" style="width: 95%" class="SilverBorder">
                    <tr>
                        <td style="height: 20px; background-color: #565294;">
                            <b><span class="MenuHeader">Non Bankcard</span></b>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-image: url('Images/homeback.gif');" class="SilverBorder">
                            <b><span class="LabelsRed">Discover</span></b></td>
                    </tr>
                    <!--tr>
                        <td>
                            <a class="One" href="../Discover Forms/Discover Rate Chart.doc" target="_blank">Discover
                                Rate Chart</a>
                        </td>
                    </tr-->
                    <tr>
                        <td>
                            <a class="One" href="https://www.apps.commercetech.com/Affiliatewiz/Discover%20Interchange%20Rates.pdf"
                                target="_blank">Discover Interchange Matrix</a>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-image: url('Images/homeback.gif');" class="SilverBorder">
                            <b><span class="LabelsRed">Amex</span></b></td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="../Amex%20Forms/Amex app.pdf" target="_blank">Amex Application</a>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="../Amex%20Forms/Pricing/Appr franchise.pdf" target="_blank">Approved
                                Franchise List</a>
                        </td>
                    </tr>
                    <!--<tr>
                        <td>
                            <a class="One" href="../Amex Forms/Forms/B2B Reference Guide.pdf" target="_blank">B2B
                                Reference Guide</a>
                        </td>
                    </tr>-->
                    <tr>
                        <td>
                            <a class="One" href="../Amex%20Forms/Pricing/Amex%20US%20Merchant%20Pricing.pdf" target="_blank">
                                Amex U.S. Merchant Pricing</a></td>
                    </tr>
                    </table>
            </td>
            <td align="center" valign="top" width="33%">
                <table border="0" cellspacing="2" style="width: 95%;" class="SilverBorder">
                    <tr>
                        <td style="height: 20px; background-color: #565294;">
                            <strong><span class="MenuHeader">Payment Gateways</span></strong></td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="../Gateway%20Forms/Comparison/Gateway%20comparison.xls" target="_blank">
                                Gateway Comparison Chart</a></td>
                    </tr>
                    <!--<tr>
                        <td>
                            <a class="One" href="http://na.sage.com/sage-payment-solutions/Sales-Resources/~/media/63CBE519BC444F7A8524A25C715608C1.pptx" target="_blank">ROAMpay Sage PowerPoint</a></td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="../ROAMpay/ROAMPay_iPay(Agents).ppt" target="_blank">ROAMpay iPayment PowerPoint</a></td>
                    </tr>-->
                    <tr>
                        <td>
                            <asp:HyperLink CssClass="One" Font-Names="Arial" Font-Size="Small" ID="HyperLink1"
                                runat="server" Target="_blank" NavigateUrl="http://www.authorize.net/videos">Authorize.Net videos</asp:HyperLink>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:HyperLink CssClass="One" Font-Names="Arial" Font-Size="Small" ID="HyperLink9"
                                runat="server" Target="_blank" NavigateUrl="https://www.authorize.net/solutions/merchantsolutions/merchantservices/syncforquickbooks/">Authorize.Net Sync for QuickBooks</asp:HyperLink>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:HyperLink CssClass="One" Font-Names="Arial" Font-Size="Small" ID="HyperLink11"
                                runat="server" Target="_blank" NavigateUrl="https://www.youtube.com/watch?v=m84WONiL5kQ">Authorize.Net Sync for QuickBooks demo</asp:HyperLink>&nbsp;</td>
                    </tr>
                    <!--
                    
                    <tr>
                        <td>
                            <a class="One" href="https://reseller.plugnpay.com/" target="_blank"><span style="font-family: Arial;
                                font-size: small">Plug'n Pay Reseller Login</span></a></td>
                    </tr>-->
                    <tr>
                        <td>
                            <a class="One" href="https://support.sagepayments.com/link/portal/20000/20000/Article/3111/Sage-Virtual-Terminal-Training-Videos" target="_blank">Sage Basic Virtual Terminal Functionality</a></td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="https://sagena.webex.com/sagena/lsr.php?AT=pb&SP=MC&rID=11040152&rKey=b6871e6eaaf80c6a"
                                target="new">SageExchange.com Webinar</a></td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="http://www.youtube.com/watch?v=2JMgHB4myyM&feature=youtu.be" target="_blank">Sage Shopping Cart & Donate Now webinar</a></td>
                    </tr>
                    <tr>
                    <td>
                            <a class="One" href="http://www.youtube.com/watch?v=Qhir7lM2hoY&feature=youtu.be" target="_blank">Sage VT3 & Level III Processing webinar</a></td>
                    </tr>
                </table>                
                <br />
                <table border="0" cellspacing="2" style="width: 95%" class="SilverBorder">
                    <tr>
                        <td style="height: 20px; background-color: #565294;">
                            <b><span class="MenuHeader">Check Acceptance</span></b></td>
                    </tr>
                    
                    <tr>
                        <td style="background-image: url('Images/homeback.gif');" class="SilverBorder">
                            <b><span class="LabelsRed">CrossCheck</span></b></td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="http://ms.cross-check.com/CrossCheckFiles/WebAuthPortal" target="_blank">
                                CrossCheck Archive</a>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="/Crosscheck Forms/Check Guarantee-Plus Sales/standard agreement.pdf"
                                target="_blank">Retail Application</a>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="/Crosscheck Forms/Check Conversion Plus with-without Imaging/With Imaging/Conversion Plus imaging.pdf"
                                target="_blank">Check Conversion Plus with Imaging</a></td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="/Crosscheck Forms/Check Conversion Plus with-without Imaging/Without Imaging/Conversion plus nonimaging.pdf"
                                target="_blank">Non Imaging Check Conversion Plus</a>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-image: url('Images/homeback.gif');" class="SilverBorder">
                            <b><span class="LabelsRed">Sage Payment Solutions EFT</span></b></td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="http://www.checktraining.com/ece" target="_blank">Check Services Website</a></td>
                    </tr>
                    <tr>
                        <td style="background-image: url('Images/homeback.gif');" class="SilverBorder">
                            <b><span class="LabelsRed">TeleCheck</span> <span class="LabelsRedLarge">*</span></b></td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="../Telecheck/Forms/TeleCheck Application.pdf" target="_blank">TeleCheck
                                Application</a></td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="../Telecheck/Forms/TeleCheck Terms & Conditions.pdf" target="_blank">
                                TeleCheck Terms and Conditions</a></td>
                    </tr>
                    </table>
                <br />
                <table border="0" cellspacing="2" style="width: 95%;" class="SilverBorder">
                    <tr>
                        <td style="background-color: #565294; height: 20px">
                            <strong><span class="MenuHeader">Gift Cards/Loyalty</span></strong></td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="../Gift Card/Gift-Loyalty Buy Rates.xls" target="_blank">Gift/Loyalty
                                Card Buy Rates</a></td>
                    </tr>

                    <tr>
                        <td>
                            <a class="One" href="http://www.giftcardtraining.com/ece" target="_blank">Gift Card Website</a></td>
                    </tr>
                    </table>  
                <br />
                <table border="0" cellspacing="2" style="width: 95%;" class="SilverBorder">
                    <tr>
                        <td style="height: 20px; background-color: #565294;">
                            <strong><span class="MenuHeader">Merchant Cash Advance</span></strong></td>
                    </tr>
<tr>
                        <td>
                            <a class="One" href="../Merchant Cash Advance/Approval Criteria - BFS and AdvanceMe, Inc. Comparison.doc"
                                target="_blank">Cash Advance Approval Criteria</a></td>
                    </tr>
                    <!--<tr>
                        <td>
                            <a class="One" href="../Merchant Cash Advance/Business Financial Services/Forms/Cash Advance Program Procedures_iPayment.pdf"
                                target="_blank">Cash Advance Program Procedures</a>
                        </td>
                    </tr>-->
                    <tr>
                        <td style="background-image: url('Images/homeback.gif');" class="SilverBorder">
                            <b><span class="LabelsRed">Business Financial Services</span><span class="LabelsRedLarge"> </span></b></td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="../Merchant Cash Advance/Business Financial Services/Forms/General Authorization.pdf"
                                target="_blank">General Authorization Form</a></td>
                    </tr>



                    <tr>
                        <td style="background-image: url('Images/homeback.gif');" class="SilverBorder">
                            <b><span class="LabelsRed">AdvanceMe, Inc.</span></b></td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="../Merchant Cash Advance/AdvanceMe/CAN PQ Form Editable (11 18 2013) - FINAL.pdf"
                                target="_blank">Pre-Qualification Form</a></td>
                    </tr>


                    <tr>
                        <td style="background-image: url('Images/homeback.gif');" class="SilverBorder">
                            <b><span class="LabelsRed">RapidAdvance</span></b></td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="../Merchant Cash Advance/RapidAdvance/Forms/Application/Application (2013) fillable.pdf"
                                target="_blank">Application Form</a></td>
                    </tr>
                    
                    </table>

                    <table border="0" cellspacing="2" style="width: 95%;" class="SilverBorder">
                    <tr>
                        <td style="height: 20px; background-color: #565294;">
                            <strong><span class="MenuHeader">Payroll</span> </strong>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <a class="One" href="https://intuitcorp.webex.com/ec0701lsp13/eventcenter/recording/recordAction.do?theAction=poprecord&AT=pb&internalRecordTicket=4832534b0000000207817ebb178a960fe32000b85f8e90322aeb08e3331c8382b3b34fc251f5409f&renewticket=0&isurlact=true&recordID=79700247&apiname=lsr.php&format=short&needFilter=false&&SP=EC&rID=79700247&RCID=80e4de15b7e58eba3bc3a338a176b692&siteurl=intuitcorp&actappname=ec0701lsp13&a"
                                target="_blank">Intuit Payroll Solutions Webinar</a></td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="https://intuitcorp.quickbase.com/up/bfnmbge5d/g/rbik/eh/va/Connect%202015%20-%20Payroll.pptx"
                                target="_blank">Intuit Payroll Announcements</a></td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="https://intuitcorp.quickbase.com/up/bfnmbge5d/g/r32/eh/va/IFSP%20-IRP%20Training.ppt"
                                target="_blank">Intuit Payroll Solutions Presentation</a></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 15px">
                &nbsp;</td>
            <td style="height: 15px">
                <span class="LabelsRedLarge">* - Works only with iPayment</span> &nbsp;</td>
            <td style="height: 15px">
                &nbsp;</td>
        </tr>
    </table>
    <div id="flyout" style="z-index: 2; display: none; border: solid 1px #D0D0D0; background-color: #FFFFFF;
        overflow: hidden;">
        &nbsp;</div>
    <div id="info" style="z-index: 2; display: none; font-size: 12px; border: solid 1px #CCCCCC;
        background-color: #ffffff; width: 500px; padding: 5px; font-family: Arial; font-size: 10pt;">
        <div style="float: right; filter: alpha(opacity=0);" id="btnCloseParent">
            <asp:LinkButton ID="btnClose" CssClass="CloseButton" runat="server" OnClientClick="return false;"
                Text="" ToolTip="Close">X</asp:LinkButton>
        </div>
        <table width="500px" border="0" cellspacing="2" style="border-right: silver 1px solid;
            border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;
            background-color: #f5f5f5">
            <tr>
                <td align="left" style="height: 25px; background-color: #565294;">
                    <strong><span class="MenuHeader">iPayment Merchant Services Guide Documents</span></strong></td>
            </tr>
            <tr>
                <td align="left">
                    <a class="One" href="http://www.ipaymentinc.com/pdf/01%20IPI%20CBGuidelines%20.pdf"
                        target="_blank">Answers and Prevention Tips for Chargebacks and Retrievals</a></td>
            </tr>
            <tr>
                <td align="left">
                    <a class="One" href="http://www.ipaymentinc.com/pdf/02%20IPI%20DiscountRates.pdf"
                        target="_blank">How to Obtain the Best Possible Discount Rate</a></td>
            </tr>
            <tr>
                <td align="left">
                    <a class="One" href="http://www.ipaymentinc.com/pdf/03%20FAQ.pdf" target="_blank">About
                        Your Merchant Account</a></td>
            </tr>
            <tr>
                <td align="left">
                    <a class="One" href="http://www.ipaymentinc.com/pdf/10%20IPI%20MAD%20Reports.pdf"
                        target="_blank">Information Contained in the MAD Report</a></td>
            </tr>
            <tr>
                <td align="left">
                    <a class="One" href="http://www.ipaymentinc.com/pdf/Statement_Sample.pdf" target="_blank">
                        Sample of Your Merchant Statement</a></td>
            </tr>
            <tr>
                <td align="left">
                    <a class="One" href="http://www.ipaymentinc.com/pdf/08%20IPI%20Statement%20revsd.pdf"
                        target="_blank">Understanding Your Merchant Statement</a></td>
            </tr>
            <tr>
                <td align="left">
                    <a class="One" href="http://www.ipaymentinc.com/pdf/09%20IPI%20TranProcessing.pdf"
                        target="_blank">Proper Transaction Processing</a></td>
            </tr>
            <tr>
                <td align="left">
                    <a class="One" href="http://www.ipaymentinc.com/pdf/11%20IPI%20ElecCommRM.pdf" target="_blank">
                        E-Commerce Risk Management Guidelines</a></td>
            </tr>
            <tr>
                <td align="left">
                    <a class="One" href="http://www.ipaymentinc.com/pdf/Debit%20Card%20Service%20Guide%20v0502.pdf"
                        target="_blank">Debit Card Service Guide</a></td>
            </tr>
            <tr>
                <td align="left">
                    <a class="One" href="http://www.ipaymentinc.com/pdf/12%20Glossary.pdf" target="_blank">
                        Glossary of Terms</a></td>
            </tr>
            <tr>
                <td align="left" style="height: 25px; background-color: #565294;">
                    <strong><span class="MenuHeader">Change / Request Forms</span></strong></td>
            </tr>
            <tr>
                <td align="left">
                    <a class="One" href="../IPayment%20Forms/ACH%20Debits%20Credits%20Change%20Form.pdf"
                        target="_blank">CH Debits/Credits Change Request Form</a></td>
            </tr>
            <tr>
                <td align="left">
                    <a class="One" href="../IPayment%20Forms/Card%20Addition%20Change%20Form.pdf" target="_blank">
                        Card Addition/Change Request Form</a></td>
            </tr>
            <tr>
                <td align="left">
                    <a class="One" href="../IPayment%20Forms/Address%20Phone%20Fax%20Change%20Form.pdf"
                        target="_blank">Address/Phone/Fax Change Request Form</a></td>
            </tr>
            <tr>
                <td align="left">
                    <a class="One" href="../IPayment%20Forms/Processing%20Limit%20Change%20Form.pdf"
                        target="_blank">Processing Limit Change Form</a></td>
            </tr>
            <tr>
                <td align="left">
                    <a class="One" href="../IPayment%20Forms/Business%20Name%20Change%20Form.pdf" target="_blank">
                        Business Name Change Form</a></td>
            </tr>
            <tr>
                <td align="left">
                    <a class="One" href="../IPayment%20Forms/Close%20Merchant%20Account%20Form.pdf" target="_blank">
                        Close Merchant Account Request Form</a></td>
            </tr>
        </table>
    </div>
</asp:Content>
