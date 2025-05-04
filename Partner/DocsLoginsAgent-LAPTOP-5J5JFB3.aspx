<%@ Page Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="DocsLoginsAgent.aspx.cs"
    Inherits="DocsLoginsAgent" Title="Commerce Technologies - Partner Portal" Theme="AppTheme" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <link rel="icon" href="/favicon.ico" type="image/x-icon" />
    <link rel="shortcut icon" href="/favicon.ico" type="image/x-icon" />
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
        2
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
    <table cellspacing="0" cellpadding="0" border="0" style="width: 850px" class="SilverBorder">
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

            <td valign="top" align="center" width="34%">


                <table width="95%" border="0" cellspacing="2" class="SilverBorder">
                     <tr>
            <td style="height: 25px; background-color: #918EB9;" valign="middle" align="center" class="BorderBlack">
                <span class="MenuHeader"><strong>Services</strong></span></td>
           
        </tr>
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
                    <!--<tr>
                                    <td>
                                        <a class="One" href="../Optimal Forms/Interchange%20Compliance%20Guide.doc" target="_blank">
                                            Interchange Chart</a></td>
                                </tr>-->
                    <tr>
                        <td style="height: 15px">
                            <a class="One" href="../PriceComparison/Rate Comparison Template - 3 Tier.xls" target="_blank">
                                Rate Comparison Template - 3 Tier</a>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 15px">
                            <a class="One" href="../PriceComparison/Rate Comparison Template - Interchange Plus.xls"
                                target="_blank">Rate Comparison Template - Interchange</a>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="https://usa.visa.com/content/dam/VCOM/download/merchants/visa-usa-interchange-reimbursement-fees.pdf"
                                target="_blank">Visa USA Interchange Rates</a></td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="http://www.mastercard.com/us/merchant/pdf/MasterCard_Interchange_Rates_and_Criteria.pdf"
                                target="_blank">MasterCard Worldwide U.S. and Interregional<br/>
                                Interchange Rates</a></td>
                    </tr>
                    <tr>
                        <td style="background-image: url('Images/homeback.gif');" class="SilverBorder">
                            <b><span class="LabelsRed">Paya/Sage Payment Solutions</span></b></td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="../Sage/Forms/Applications/Application and Agreement for Merchant Account.pdf" target="_blank">Merchant
                                Application</a></td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="../affiliatewiz/Forms/Paya Sample Statement.pdf" target="_blank">Merchant
                                Sample Statement</a></td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="../affiliatewiz/Forms/Paya Merchant Account Cancellation Form.doc" target="new">
                                Merchant Account Cancellation Form</a></td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="../Sage/Forms/ADD DEBIT & EBT.doc" target="_blank">Debit Addendum</a></td>
                    </tr>

                    <tr>
                        <td>
                            <a class="One" href="../Sage/Programs/Product Grid TSYS-Retail 111110.xls" target="_blank">
                                Product Grid TSYS</a></td>
                    </tr>
                    <!--<tr>
                        <td>
                            <a class="One" href="https://partners.sagenorthamerica.com/" target="new">
                                Resource Center</a></td>
                    </tr>-->
                    <tr>
                        <td>
                            <a class="One" href="https://uno.eftsecure.net/SalesCenter/" target="new">
                                Sales Center</a></td>
                    </tr>
                    <!--<tr>
                        <td>
                            <a class="One" href="http://www.sagequickticket.com/" target="new">
                                Quick Ticket Login</a></td>
                    </tr>-->

                     <td>
                            <a class="One" href="../affiliatewiz/Forms/Paya Rate Change - Bankcard Rev 0615.DOCX" target="new">
                                Rate change - bankcard</a></td>
                    </tr>

                    <tr>
                        <td style="background-image: url('Images/homeback.gif');" class="SilverBorder">
                            <b><span class="LabelsRed">Intuit</span></b></td>
                    </tr>  
                    
                                    
                    <!--<tr>
                        <td>
                            <a class="One" href="https://intuit.channelbridge.net/  " target="_blank">Channel Bridge</a></td>
                    </tr>-->                  

                    <!--<tr>
                        <td>
                            <a class="One" href="https://merchant.intuit.com/signup/?b=gopayment_only&p=js19888gp"
                                target="_blank">GoPayment Application</a></td>
                    </tr>-->

                    <!--<tr>
                        <td>
                            <a class="One" href="/IMS Sales & Marketing Materials/GoPayment Merchant Account Service.pdf"
                                target="_blank">GoPayment Brochure</a></td>
                    </tr>-->
                    <!--<tr>
                        <td>
                            <a class="One" href="../IMS%20forms/IMS%20Product%20Matrix.xls" target="_blank">Product Matrix</a></td>
                    </tr>-->
                                        <tr>
                        <td>
                            <a class="One" href="https://intuit.ziftone.com/#/page/resource-library" target="_blank">QSP Resource Center</a></td>
                    </tr>

                    
                    <tr>
                        <td>
                            <a class="One" href="https://www.quickbase.com/db/main?a=SignIn" target="_blank">Intuit QuickBase</a></td>
                    </tr>
                    <!--<tr>
                        <td>
                            <a class="One" href="https://workplace.intuit.com/app/Account/Logon" target="_blank">Intuit Workplace</a></td>
                    </tr>-->
                    <!-- <tr>    
                        <td>
                            <a class="One" href="https://www.apps.commercetech.com/Intuit%20Marketing/QSP%20Pricing.xlsx
" target="_blank">Price Sheet</a></td>
                    </tr> -->
                    <!-- <tr>
	                    <td>
		                    <a class="One" href="https://intuit.ziftone.com/#/page/login" target="_blank">Zift Login</a>
	                    </td>
                    </tr> -->

                                                                                 <tr>
                        <td>
                            <a class="One" href="https://quickbooks.intuit.com/payments/legal/toc102017/acceptable-use/" target="_blank">Acceptable Use Policy</a></td>
                    </tr>
                    
                     <asp:Panel runat="server" ID="Pivotal">
                    <tr>
                        <td style="background-image: url('Images/homeback.gif');" class="SilverBorder">
                            <b><span class="LabelsRed">Nuvei/Pivotal</span></b></td>
                    </tr>
                        <tr>
                            <td>
                                <a class="One" href="https://nuvei.com/" target="_blank">Nuvei/Pivotal Live</a></td>
                        </tr>

                    </asp:Panel>
                    


                    <tr>
                        <td style="background-image: url('Images/homeback.gif');" class="SilverBorder">
                            <b><span class="LabelsRed">PaySafe/iPayment</span></b></td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="../Ipayment%20Forms/Applications/iPayment%20Application.pdf"
                                target="new">iPayment Merchant Application</a></td>
                    </tr>
                    <!--<tr>
                        <td>
                            
                            <a class="One" href="../Ipayment%20Forms/Applications/iPayment%20Program%20Guide.pdf"
                                target="new">iPayment Program Guide</a></td>
                    </tr>-->
                    <tr>
                        <td>
                        <!--<a class="One" href="file://Server-R310/shared/Processors/iPayment/Forms/Debit Card Acceptance/PIN Debit Card Addendum.pdf"
                                target="new">Debit Addendum</a>-->
                            <a class="One" href="../Ipayment%20Forms/Debit%20Card%20Acceptance/PIN%20Debit%20Card%20Addendum.pdf"
                                target="new">Debit Addendum</a></td>
                    </tr>
                    <!--<tr>
                        <td>
                            <a class="One" href="https://iaccess.merchant-info.com/Login.aspx"
                                target="new">iAccess</a></td>
                    </tr>-->
                    <tr>
                        <td>
                            <a class="One" href="https://iaccessportal.com/"
                                target="_blank">iAccess Login</a></td>
                    </tr>
                    
                    <!--<tr>
                        <td>
                            <a class="One" href="https://login.netbanx.com" target="_blank">iPayment back office
                                Login</a></td>
                    </tr>-->
                    <!--<tr>
                        <td>
                            <a class="One" href="https://e-info.ecx.com/" target="_blank"><span style="font-family: Arial;
                                font-size: small">Apollo Login</span></a></td>
                    </tr>
                    <tr>
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
                            <a class="One" href="../iPayment%20Forms/Resrvdsc_.doc" target="_blank">Reserve Form</a></td>
                    </tr>-->
                    <tr>
                        <td>
                            <asp:LinkButton ID="lnkbtniPayment" runat="server" OnClientClick="return false;"
                                CssClass="One">Other iPayment Forms and Guidelines</asp:LinkButton>
                            <!--<a class="One" href="iPayment.aspx" target="_blank">Other iPayment Forms and Guidelines</a>-->
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="../affiliatewiz/Forms/Ipayment%20Product%20Matrix.xls" target="_blank">
                                iPayment Product Matrix</a></td>
                    </tr>
                    <!--<tr>
                        <td style="background-image: url('Images/homeback.gif');" class="SilverBorder">
                            <b><span class="LabelsRed">Payvision</span></b></td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="http://iso.optimalpayments.com/default.asp?page=load" target="_blank">
                                Payvision Login</a></td>
                    </tr>
                    <tr height="10">
                    </tr>-->
                    <tr>
                            <td>
                                <a class="One" href="../Ipayment%20Forms/Example Statement.pdf" target="_blank">Sample Statement</a></td>
                    </tr>
                    <tr>
                        <td style="background-image: url('Images/homeback.gif');" class="SilverBorder">
                            <b><span class="LabelsRed">PaySafe/Optimal Payments</span></b></td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="https://login.netbanx.com" target="_blank">
                                Optimal Login</a></td>
                    </tr>
                    <tr>
                        <td>
                        <a class="One" href="../Optimal%20Forms/Application/Merrick_application.pdf" target="_blank">Merrick Application</a>
                        </td>
                    </tr>

                    <asp:Panel runat="server" ID="FTSLogin">
                    <tr>
                        <td style="background-image: url('Images/homeback.gif');" class="SilverBorder">
                            <b><span class="LabelsRed">CardConnect</span></b></td>
                    </tr>
                        <tr>
                            <td>
                                <a class="One" href="https://copilot.cardpointe.com/" target="_blank">CoPilot</a></td>
                        </tr>
                        <tr>
                            <td>
                                <a class="One" href="../CardConnect/Forms/Example Statement.pdf" target="_blank">Sample Statement</a></td>
                        </tr>
                    </asp:Panel>
                    </table>
                
                <table border="0" cellspacing="2" style="width: 95%;" class="SilverBorder">
                    <tr>
                        <td style="height: 20px; background-color: #565294;">
                            <b><span class="MenuHeader">Non Bankcard</span></b>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-image: url('Images/homeback.gif');" class="SilverBorder">
                            <b><span class="LabelsRed">Discover</span></b></td>
                    </tr>
                    <!--<tr>
                                    <td>
                                        <a class="One" href="https://www.discoverrap.com/app/main" target="_blank">Discover RAP Login</a>
                                    </td>
                                </tr>-->
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
                            <a class="One" href="../Amex%20Forms/Forms/Application/Amex OP Merchant App instructions.pdf" target="_blank">Amex Application</a>
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
                    <br />
                <table border="0" cellspacing="2" style="width: 95%" class="SilverBorder">
                    <tr>
                        <td style="height: 20px; background-color: #565294;">
                            <b><span class="MenuHeader">Check Services</span></b></td>
                    </tr>
                    
                    <!--<tr>
                                    <td>
                                        <a class="One" href="../forms/tcapplication.doc" target="_blank">Internet Application</a></td>
                                </tr>-->
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
                            <b><span class="LabelsRed">E-Check.net</span></b></td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="https://partner.authorize.net/widget/widget/RINT/SPA" target="_blank">
                                Partner Portal</a>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href=" https://cybersource.partner-experience.com/login?utm_media=owned&utm_medium=email&utm_campaign=global_Q1_PartnerPortalLaunch_owned_email&utm_content=website&utm_business=cybs&utm_source=eloqua&dcid=293420C4-6E67-ED11-9561-000D3A3411C3"
                                target="_blank">Marketing Portal</a>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="https://support.authorize.net/"
                                target="_blank">ECheck.net Support</a></td>
                    </tr>
                    
                    <tr>
                        <td style="background-image: url('Images/homeback.gif');" class="SilverBorder">
                            <b><span class="LabelsRed">Paya Services</span></b></td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="http://www.checktraining.com/ece" target="_blank">Check Services Website</a></td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href=" https://myonlinereports4.eftchecks.com" target="_blank">Admin Login</a></td>
                    </tr>
                    <tr>
                        <td style="background-image: url('Images/homeback.gif');" class="SilverBorder">
                            <b><span class="LabelsRed">TeleCheck</span> </b></td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="../Telecheck/TeleCheck Pricing & Procedures.pdf" target="_blank">
                                TeleCheck Pricing & Procedures</a></td>
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
                    <br/>
                                    <table border="0" cellspacing="2" style="width: 95%;" class="SilverBorder">
                    <tr>
                        <td style="height: 20px; background-color: #565294;">
                            <strong><span class="MenuHeader">Gift Cards/Loyalty</span></strong></td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="../Gift Card/Gift-Loyalty Buy Rates.xls" target="_blank">Gift/Loyalty
                                Card Buy Rates</a></td>
                    </tr>
                    <tr>
                        <td style="background-image: url('Images/homeback.gif');" class="SilverBorder">
                            <b><span class="LabelsRed">Paya Services</span></b></td>
                    </tr>
                    
                        <td>
                            <a class="One" href=" https://myonlinereports4.eftchecks.com" target="_blank">Admin Login</a></td>
                    </tr>
                    <!--<tr>
                        <td>
                            <a class="One" href="http://www.myonlinereports.com" target="_blank">Admin Login</a></td>
                    </tr>-->
                    </table>
                <br />               
                <table border="0" cellspacing="2" style="width: 95%;" class="SilverBorder">
                    <tr>
                        <td style="height: 20px; background-color: #565294;">
                            <strong><span class="MenuHeader">Business Funding</span></strong></td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="../affiliatewiz/Forms/Approval Criteria - BFS and AdvanceMe, Inc. Comparison.doc"
                                target="_blank">Cash Advance Approval Criteria</a></td>
                    </tr>
                    <!--<tr>
                        <td>
                            <a class="One" href="../Merchant Cash Advance/Business Financial Services/Forms/Cash Advance Program Procedures_iPayment.pdf"
                                target="_blank">Cash Advance Program Procedures</a>
                        </td>
                    </tr>-->
                    <!--<tr>
                        <td style="background-image: url('Images/homeback.gif');" class="SilverBorder">
                            <b><span class="LabelsRed">Business Financial Services</span><span class="LabelsRedLarge"> </span></b></td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="../Merchant Cash Advance/Business Financial Services/Forms/Application.pdf"
                                target="_blank">Application</a></td>
                    </tr>


                    <tr>
                        <td>
                            <a class="One" href="https://isoportal.bfscapital.com" target="_blank">BFS
                                Login</a></td>
                    </tr>-->
                    <!--<tr>
                        <td style="background-image: url('Images/homeback.gif');" class="SilverBorder">
                            <b><span class="LabelsRed">AdvanceMe, Inc.</span></b></td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="../Merchant Cash Advance/AdvanceMe/CAN PQ Form Editable (11 18 2013) - FINAL.pdf"
                                target="_blank">Pre-Qualification Form</a></td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="https://partners.capitalaccessnetwork.com"
                                target="_blank">Sales Access Portal</a></td>
                    </tr>-->

                    <tr>
                        <td style="background-image: url('Images/homeback.gif');" class="SilverBorder">
                            <b><span class="LabelsRed">RapidFinance</span></b></td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="../affiliatewiz/Forms/Application fillable (2021).pdf"
                                target="_blank">Application Form</a></td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="https://portal.rapidadvance.com/?getPartnerLoginPage=true"
                                target="_blank">RapidFinance Login</a></td>
                    </tr>
                    </table>
                <br />
                <table border="0" cellspacing="2" style="width: 95%;" class="SilverBorder">
                    <tr>
                        <td style="height: 20px; background-color: #565294;">
                            <strong><span class="MenuHeader">Payroll</span> </strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="https://quickbooks.intuit.com/partners/irp/payroll/?cid=irp-7"
                                target="_blank">Intuit Online Payroll Signup</a></td>
                    </tr>

                    <tr>
                        <td>
                            <a class="One" href="https://quickbooks.intuit.com/partners/irp/qbtime/?cid=irp-75"
                                target="_blank">QuickBooks Time</a></td>
                    </tr>
                    <!--<tr>
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
                    </tr>-->
                </table>
                <br />       
            </td>
            <td align="center" valign="top" width="33%">
                <table border="0" cellspacing="2" style="width: 95%;" class="SilverBorder">
                    
                     <tr>
            <td style="height: 25px; background-color: #918EB9;" valign="middle" align="center" class="BorderBlack">
                <span class="MenuHeader"><strong>Software</strong></span></td>
           
        </tr>
                    <tr>
                        <td style="height: 20px; background-color: #565294;">
                            <strong><span class="MenuHeader">Payment Gateways</span></strong></td>
                    </tr>
                    <!-- <tr>
                        <td>
                            <a class="One" href="../Gateway Forms/Comparison/Gateway comparison.xls" target="_blank">
                                Comparison Chart</a></td>
                    </tr>-->

                    <tr>
                        <td style="background-image: url('Images/homeback.gif');" class="SilverBorder">
                            <b><span class="LabelsRed">Authorize.net</span></b></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:HyperLink CssClass="One" ID="HyperLink1" runat="server" Target="_blank" NavigateUrl="https://account.authorize.net/interfaces/reseller/frontend/login.aspx">
                                        Authorize.Net Reseller Login</asp:HyperLink></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:HyperLink CssClass="One" Font-Names="Arial" Font-Size="Small" ID="HyperLink10"
                                runat="server" Target="_blank" NavigateUrl="https://www.youtube.com/user/AuthorizeNet">Authorize.Net videos</asp:HyperLink>&nbsp;</td>
                    </tr>
                    <!-- <tr>
                        <td>
                            <asp:HyperLink CssClass="One" Font-Names="Arial" Font-Size="Small" ID="HyperLink9"
                                runat="server" Target="_blank" NavigateUrl="https://www.authorize.net/solutions/merchantsolutions/merchantservices/syncforquickbooks/">Authorize.Net Sync for QuickBooks</asp:HyperLink>&nbsp;</td>
                    </tr> -->
                    <!-- <tr>
                        <td>
                            <asp:HyperLink CssClass="One" Font-Names="Arial" Font-Size="Small" ID="HyperLink11"
                                runat="server" Target="_blank" NavigateUrl="https://www.youtube.com/watch?v=m84WONiL5kQ">Authorize.Net Sync for QuickBooks demo</asp:HyperLink>&nbsp;</td>
                    </tr> -->
                    <!-- <tr>
                        <td>
                            <asp:HyperLink CssClass="One" Font-Names="Arial" Font-Size="Small" ID="HyperLink69"
                                runat="server" Target="_blank" NavigateUrl="https://cybersource.zinfi.net/concierge/ucm/#/cms/Home">Authorize.net's Partner Marketing Portal</asp:HyperLink>&nbsp;</td>
                    </tr> -->
                    <tr>
                        <td style="background-image: url('Images/homeback.gif');" class="SilverBorder">
                            <b><span class="LabelsRed">Plug'n Pay</span></b></td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="https://reseller.plugnpay.com/" target="_blank">Plug'n Pay Reseller
                                Login</a></td>
                    </tr>
                    <!--<tr>
                        <td>
                            <a class="One" href="http://na.sage.com/sage-payment-solutions/Sales-Resources/~/media/63CBE519BC444F7A8524A25C715608C1.pptx" target="_blank">ROAMpay Sage PowerPoint</a></td>
                    </tr>-->
                    <!--<tr>
                        <td>
                            <a class="One" href="../ROAMpay/ROAMPay_iPay(Agents).ppt" target="_blank">ROAMpay iPayment PowerPoint</a></td>
                    </tr>-->
                    <!--<tr>
                        <td>
                            <a class="One" href="https://www.qbplugin.com/iso/" target="_blank">Superior Payments Login</a></td>
                    </tr>-->
                    <tr>
                        <td style="background-image: url('Images/homeback.gif');" class="SilverBorder">
                            <b><span class="LabelsRed">Paya Virtual Terminal</span></b></td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="https://support.paya.com/44616-paya-virtual-terminal-all-articles/315928-sage-virtual-terminal-training-videos" target="_blank">Paya Basic Virtual Terminal Functionality</a></td>
                    </tr>
                    
                    <!--<tr>
                        <td>
                            <a class="One" href="https://sagena.webex.com/sagena/ldr.php?AT=pb&SP=MC&rID=10996542&rKey=e4da45b6a260a531" target="_blank">Sage Basic Virtual Terminal Functionality</a></td>
                    </tr>-->
                    <tr>
                        <td>
                            <a class="One" href="https://support.sagepayments.com/ics/support/kbanswer.asp?deptID=20000&task=knowledge&questionID=3111"
                                target="new">View Sage VT training videos</a></td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="https://support.paya.com/44651-shopping-cart-donate-now/310330-how-do-i-add-the-paya-virtual-terminal-shopping-cart-to-my-website" target="_blank">Paya VT/Shopping Cart Integration</a></td>
                    </tr>
                    <tr>
                    <td>
                            <a class="One" href="https://www.youtube.com/@PayaHQ/videos" target="_blank">Paya training</a></td>
                    </tr>

                    </table>


                                <table border="0" cellspacing="2" style="width: 95%" class="SilverBorder">
                    <tr>
                        <td style="height: 20px; background-color: #565294;">
                            <b><span class="MenuHeader">Point Of Sale</span></b></td>
                    </tr>
                      


                    <tr>
                        <td style="background-image: url('Images/homeback.gif');" class="SilverBorder">
                            <b><span class="LabelsRed">Clover</span></b></td>
                    </tr>

                    <tr>
                        <td>
                            <a class="One" href="https://www.clover.com/dashboard/login?webRedirectUrl=https://www.clover.com/developer-home&hardRedirect=true&context=developer"
                                target="_blank">Clover Demo</a>
                        </td>
                    </tr>

                    

                    <tr>
                        <td>
                           </td>
                    </tr>
                   
                    <tr>
                        <td style="background-image: url('Images/homeback.gif');" class="SilverBorder">
                            <b><span class="LabelsRed">Revel</span></b></td>
                    </tr>

                                       <tr>
                        <td>
                            <a class="One" href="https://comtech-partner.revelup.com"
                                target="_blank">Revel URL</a>
                        </td>
                    </tr>

                    

                   <tr>
                        <td>
                            <a class="One" href="https://revelsystems.force.com/partner/login"
                                target="_blank">Revel Partner Login</a>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="http://pages.revelsystems.com/Reseller-Lead-Form.html"
                                target="_blank">Lead form</a>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <a class="One" href="https://drive.google.com/drive/folders/0B3_bWiZxswLNfmlZN0hmdjQyVlVDNFdRSm1wMUdfekUzcVFDYkZnbExuSzJ3el90WDdMM1k"
                                target="_blank">Revel Documents </a>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <a class="One" href="http://revelsystems.com/revel-university/"
                                target="_blank">Revel University </a>
                        </td>
                    </tr>

                    <tr>
                        <td style="background-image: url('Images/homeback.gif');" class="SilverBorder">
                            <b><span class="LabelsRed">SwipeSimple</span></b></td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="https://swipesimple.com/sign_in" target="_blank">SwipeSimple Login</a></td>
                    </tr>
                    <!-- <tr>
                        <td>
                            <a class="One" href="http://www.shopkeep.com/signup-pr/partner-lead-submission/" target="_blank">ShopKeep POS Lead form</a></td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="http://www.shopkeep.com/partner-portal" target="_blank">ShopKeep Partner Portal</a></td>
                    </tr> -->
                    <tr>
                        <td style="height: 20px; background-color: #565294;">
                            <b><span class="MenuHeader">QuickBooks</span></b></td>
                    </tr>
                    <tr>
	                    <td style="background-image: url('Images/homeback.gif');" class="SilverBorder">
	                    <b><span class="LabelsRed">QuickBooks Desktop</span></b></td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="https://downloads.quickbooks.com/app/qbdt/products" target="_blank">Product downloads</a></td>
                    </tr>


                    

                   
                    </table>
                    <br/>
                    <table border="0" cellspacing="2" style="width: 95%;" class="SilverBorder">
                     <tr>
            <td style="height: 25px; background-color: #918EB9;" valign="middle" align="center" class="BorderBlack">
                <span class="MenuHeader"><strong>Hardware</strong></span></td>
           
        </tr>
        </table>

                        <table border="0" cellspacing="2" style="width: 95%;" class="SilverBorder">
                    <tr>
                        <td style="height: 20px; background-color: #565294;">
                            <strong><span class="MenuHeader">Terminals</span></strong></td>
                    </tr>

                    <tr>
                        <td style="background-image: url('Images/homeback.gif');" class="SilverBorder">
                            <b><span class="LabelsRed">PAX</span><span class="LabelsRedLarge"> </span></b></td>
                    </tr>



                    <tr>
                        <td>
                            <a class="One" href="https://paxus.paxstore.us/portal" target="_blank">PAX Store</a></td>
                    </tr>


                    <tr>
                        <td style="background-image: url('Images/homeback.gif');" class="SilverBorder">
                            <b><span class="LabelsRed">Ingenico</span></b></td>
                    </tr>
                    <!--<tr>
                        <td>
                            <a class="One" href="../Equipment and Supplies/Manufacturers/Ingenico/Terminal programing/Check Baseline, Clear terminal and Load via USB.docx"
                                target="_blank">Terminal programing</a></td>
                    </tr>-->

                    </table>
                    <br/>
                    <table border="0" cellspacing="2" style="width: 95%;" class="SilverBorder">
                    <tr>
                        <td style="height: 20px; background-color: #565294;">
                            <strong><span class="MenuHeader">Suppliers</span></strong></td>
                    </tr>
 <tr>
                        <td>
                            <asp:HyperLink CssClass="One" ID="posportal" runat="server"  Target="_blank"  NavigateUrl="https://buy.posportal.com/customer/account/create/">POS Portal</asp:HyperLink>&nbsp;</td>
                    </tr>

                    <tr>
                        <td>
                            <asp:HyperLink CssClass="One" ID="lnkTasq" runat="server" Target="_blank" NavigateUrl="https://accounts.businesstrack.com/idp/startSSO.ping?PartnerSpId=FDMarketplaceOnlineStore">Fiserv Hardware Solutions Login</asp:HyperLink>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:HyperLink CssClass="One" ID="HyperLink7" runat="server" Target="_blank" NavigateUrl="https://www.bluestoreinc.com">BlueStar Login</asp:HyperLink>&nbsp;</td>
                    </tr>

                    </table>
                    <br/>

         
            </td>

                        <td valign="top" align="center" width="33%">
                <table width="95%" border="0" cellspacing="2" class="SilverBorder">
                    <tr>
                        <td style="height: 20px; background-color: #565294;">
                            <strong><span class="MenuHeader">Other Logins</span></strong></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:HyperLink CssClass="One" ID="lnkebay" runat="server" NavigateUrl="http://www.ebay.com/"
                                Target="_blank">ebay</asp:HyperLink>&nbsp;</td>
                    </tr>

                    
                    <tr>
                        <td>
                            <asp:HyperLink CssClass="One" ID="lnkACTforWeb" runat="server"  Target="_blank"  NavigateUrl="https://apps.commercetech.com/APFW/default.aspx/">ACT for Web</asp:HyperLink>&nbsp;</td>
                    </tr>

                    

                   

                    <tr>
                        <td>
                            <asp:HyperLink CssClass="One" ID="lnkShowmyPC" runat="server" Target="_blank" NavigateUrl="https://commercetech.showmypc.com/">Remote Access (ShowMyPC)</asp:HyperLink>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:HyperLink CssClass="One" ID="HyperLink6" runat="server" Target="_blank" NavigateUrl="https://www.docusign.net/Member/MemberLogin.aspx">DocuSign Login</asp:HyperLink>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:HyperLink CssClass="One" ID="HyperLink8" runat="server" Target="_blank" NavigateUrl="https://www.merrilledge.com/">Merrill Edge</asp:HyperLink>&nbsp;</td>
                    </tr>

                    <!--<tr>
                        <td>
                            <asp:HyperLink CssClass="One" ID="HyperLink12" runat="server" Target="_blank" NavigateUrl="http://www.shareasale.com/">Share a sale (Webgility lead tracking)</asp:HyperLink>&nbsp;</td>
                    </tr>-->
                    <asp:Panel runat="server" ID="AdminLogins">
                        <tr>
                            <td style="background-image: url('Images/homeback.gif');" class="SilverBorder">
                                <b><span class="LabelsRed">Admin Logins</span></b></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:HyperLink CssClass="One" ID="lnkDell" runat="server" NavigateUrl="https://dell.com/"
                                    Target="_blank">Dell</asp:HyperLink>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:HyperLink CssClass="One" ID="HyperLink3" runat="server" NavigateUrl="http://www.godaddy.com/"
                                    Target="_blank">Godaddy (Email and Domains)</asp:HyperLink>&nbsp;</td>
                        </tr>
                        <!--<tr>
                            <td>
                                <asp:HyperLink CssClass="One" ID="HyperLink4" runat="server" NavigateUrl="http://www.instantssl.com"
                                    Target="_blank">Comodo Reseller Login</asp:HyperLink>&nbsp;</td>
                        </tr>-->
                        <tr>
                            <td>
                                <asp:HyperLink CssClass="One" ID="HyperLink5" runat="server" NavigateUrl="http://www.paypal.com"
                                    Target="_blank">Paypal Login</asp:HyperLink>&nbsp;</td>
                        </tr>
                        <tr height="10">
                        </tr>
                    </asp:Panel>
                </table>
                <br />



                <table border="0" cellspacing="2" style="width: 95%" class="SilverBorder">
                    <tr>
                        <td style="height: 20px; background-color: #565294;">
                            <strong><span class="MenuHeader">Miscellaneous</span></strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="https://commercetechnologies.sharepoint.com/:b:/s/Sales/EYf1REqUMclCt0I6EkxxqdYBMc09xQpRq8TxHaStg6RUKg?e=aLMeHP" target="_blank">Equipment sales agreement</a>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="http://www.fedex.com/us/" target="_blank">FedEx Tracking</a></td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="https://commercetechnologies.sharepoint.com/:w:/s/Sales/EVFPezbX88RHsbtzK5i6M-4Bju3tXcVqK-tjlGJMTlu7Pg?e=z1X1k7" target="_blank">Sales Job Descriptions</a></td>
                    </tr>



                    </table>
                <br />
                <table border="0" cellspacing="2" style="width: 95%;" class="SilverBorder">
                    <tr>
                        <td style="height: 20px; background-color: #565294;">
                            <strong><span class="MenuHeader">Advertising and Marketing</span> </strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="../Advertising-Marketing/Email Marketing Letters-Email Flyers/E-mail Flyers/Word Format/General Commerce Technologies Flyer.doc"
                                target="_blank">Commerce Technologies Flyer</a></td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="https://commercetechnologies.sharepoint.com/sites/MarketingTeam/Shared%20Documents/Direct%20Marketing/Blank%20Business%20Cards/BC-Template1-4.pdf?CT=1694039378061&OR=ItemsView"
                                target="_blank">Business Card Template</a>
                        </td>
                    </tr>
                    </table>

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
                            <b><span class="LabelsRed">Northern Leasing Systems</span></b></td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="../Northern%20Leasing/Applications/Northern%20Leasing%20Agreement - Standard.pdf"
                                target="_blank">Northern Lease Agreement (Standard)</a></td>
                    </tr>
                    <tr>
                        <td>
                            <a class="One" href="../Northern%20Leasing/Applications/Northern%20Leasing%20Agreement - SD, KS, TN, PA & VT.pdf"
                                target="_blank">Northern Lease Agreement (5 States)</a></td>
                    </tr>

                    <tr>
                        <td>
                            <a class="One" href="http://www.leasingsys.com/login.aspx?cid=NLSI" target="_blank">
                                Northern Login</a></td>
                    </tr>


                    </table>-->
                <br/>
                <table border="0" cellspacing="2" style="width: 95%;" class="SilverBorder">
                    <tr>
                        <td style="height: 20px; background-color: #565294;">
                            <strong><span class="MenuHeader">Hosting And Shopping Cart Services</span></strong></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:HyperLink ID="HyperLink2" runat="server" CssClass="One" NavigateUrl="https://login.bigcommerce.com/login"
                                Target="_blank">Bigcommerce Admin Login</asp:HyperLink></td>
                    </tr>
                    <!--<tr>
                        <td>
                            <a class="One" href="https://www.serrahost.com/ctc/ctcsignup/" target="_blank">ProStores
                                Order Form</a>
                        </td>
                    </tr>-->
                    </table>

                    <br/>
                    <table border="0" cellspacing="2" style="width: 95%;" class="SilverBorder">
                    <tr>
                        <td style="height: 20px; background-color: #565294;">
                            <strong><span class="MenuHeader">Webgility</span></strong></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:HyperLink ID="webgilitylink" runat="server" CssClass="One" NavigateUrl="https://www.webgility.com/refer-a-lead"
                                Target="_blank">Webgility Lead form</asp:HyperLink></td>
                    </tr>

                    </table>
                                       
            </td>
        </tr>
        <!--tr>
            <td style="height: 15px" colspan="3" align="center">
                <span class="LabelsRedLarge">* - Works only with iPayment&nbsp;</span></td>
        </tr-->
    </table>
    <div id="flyout" style="z-index: 2; display: none; border: solid 1px #D0D0D0; background-color: #FFFFFF;
        overflow: hidden;">
        &nbsp;</div>
    <div id="info" style="z-index: 2; display: none; border: solid 1px #CCCCCC; background-color: #ffffff;
        width: 500px; padding: 5px;">
        <div style="float: right; filter: alpha(opacity=0);" id="btnCloseParent">
            <asp:LinkButton ID="btnClose" CssClass="CloseButton" runat="server" OnClientClick="return false;"
                Text="" ToolTip="Close">X</asp:LinkButton>
        </div>
        <table width="500px" border="0" cellspacing="2" class="SilverBorder">
            <tr>
                <td align="left" style="height: 25px; background-color: #565294;">
                    <strong><span class="MenuHeader">iPayment Merchant Services Guide Documents</span></strong></td>
            </tr>
            <tr>
                <td align="left">
                    <a class="One" href="http://www.iPaymentinc.com/pdf/01%20IPI%20CBGuidelines%20.pdf"
                        target="_blank">Answers and Prevention Tips for Chargebacks and Retrievals</a></td>
            </tr>
            <tr>
                <td align="left">
                    <a class="One" href="http://www.iPaymentinc.com/pdf/02%20IPI%20DiscountRates.pdf"
                        target="_blank">How to Obtain the Best Possible Discount Rate</a></td>
            </tr>
            <tr>
                <td align="left">
                    <a class="One" href="http://www.iPaymentinc.com/pdf/03%20FAQ.pdf" target="_blank">About
                        Your Merchant Account</a></td>
            </tr>
            <tr>
                <td align="left">
                    <a class="One" href="http://www.iPaymentinc.com/pdf/10%20IPI%20MAD%20Reports.pdf"
                        target="_blank">Information Contained in the MAD Report</a></td>
            </tr>
            <tr>
                <td align="left">
                    <a class="One" href="http://www.iPaymentinc.com/pdf/Statement_Sample.pdf" target="_blank">
                        Sample of Your Merchant Statement</a></td>
            </tr>
            <tr>
                <td align="left">
                    <a class="One" href="http://www.iPaymentinc.com/pdf/08%20IPI%20Statement%20revsd.pdf"
                        target="_blank">Understanding Your Merchant Statement</a></td>
            </tr>
            <tr>
                <td align="left">
                    <a class="One" href="http://www.iPaymentinc.com/pdf/09%20IPI%20TranProcessing.pdf"
                        target="_blank">Proper Transaction Processing</a></td>
            </tr>
            <tr>
                <td align="left">
                    <a class="One" href="http://www.iPaymentinc.com/pdf/11%20IPI%20ElecCommRM.pdf" target="_blank">
                        E-Commerce Risk Management Guidelines</a></td>
            </tr>
            <!--<tr>
                <td align="left">
                    <a class="One" href="http://www.iPaymentinc.com/pdf/Debit%20Card%20Service%20Guide%20v0502.pdf"
                        target="_blank">Debit Card Service Guide</a></td>
            </tr>-->
            <tr>
                <td align="left">
                    <a class="One" href="http://www.iPaymentinc.com/pdf/12%20Glossary.pdf" target="_blank">
                        Glossary of Terms</a></td>
            </tr>
            <tr>
                <td align="left" style="height: 25px; background-color: #565294;">
                    <strong><span class="MenuHeader">Change / Request Forms</span></strong></td>
            </tr>
            <tr>
                <td align="left">
                    <a class="One" href="../iPayment%20Forms/ACH%20Debits%20Credits%20Change%20Form.pdf"
                        target="_blank">ACH Debits/Credits Change Request Form</a></td>
            </tr>
            <tr>
                <td align="left">
                    <a class="One" href="../iPayment%20Forms/Card%20Addition%20Change%20Form.pdf" target="_blank">
                        Card Addition/Change Request Form</a></td>
            </tr>
            <tr>
                <td align="left">
                    <a class="One" href="../iPayment%20Forms/Address%20Phone%20Fax%20Change%20Form.pdf"
                        target="_blank">Address/Phone/Fax Change Request Form</a></td>
            </tr>
            <tr>
                <td align="left">
                    <a class="One" href="../iPayment%20Forms/Processing%20Limit%20Change%20Form.pdf"
                        target="_blank">Processing Limit Change Form</a></td>
            </tr>
            <tr>
                <td align="left">
                    <a class="One" href="../iPayment%20Forms/Business%20Name%20Change%20Form.pdf" target="_blank">
                        Business Name Change Form</a></td>
            </tr>
            <tr>
                <td align="left">
                    <a class="One" href="../iPayment%20Forms/Close%20Merchant%20Account%20Form.pdf" target="_blank">
                        Close Merchant Account Request Form</a></td>
            </tr>
        </table>
    </div>
</asp:Content>
