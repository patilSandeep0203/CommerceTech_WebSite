<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="echeck.aspx.cs" Inherits="echeck" Title="Credit Card Processing, Merchant Services, Payment Processing"
    Theme="Admin" %>

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

    <cc1:AnimationExtender ID="AnimationExtender1" runat="server" TargetControlID="lnkbtnInfo">
        <Animations>
        <OnLoad><OpacityAction AnimationTarget="info" Opacity="0" /></OnLoad>
        <OnClick>
        <Sequence>                               
        <ScriptAction Script="Cover($get('ctl00_MainContent_lnkbtnInfo'), $get('flyout'));" />
        <StyleAction AnimationTarget="flyout" Attribute="display" Value="block"/>                            
        <Parallel AnimationTarget="flyout" Duration=".3" Fps="25">
        <Move Horizontal="150" Vertical="-50" />
        <Resize Width="300" Height="180" />
        <Color AnimationTarget="flyout" StartValue="#AAAAAA" EndValue="#f1fafa" Property="style" PropertyKey="backgroundColor" />                                
        </Parallel>                            
        <ScriptAction Script="Cover($get('flyout'), $get('info'), true);" />
        <StyleAction AnimationTarget="info" Attribute="display" Value="block"/>
        <FadeIn AnimationTarget="info" Duration=".2"/>                            
        <StyleAction AnimationTarget="flyout" Attribute="display" Value="none"/>
        <StyleAction AnimationTarget="info" Attribute="height" value="auto" />
        <Parallel Duration=".5">
        <Color AnimationTarget="info" StartValue="#383838" EndValue="#383838" Property="style" PropertyKey="color" />
        <Color AnimationTarget="info" StartValue="#febd0d" EndValue="#383838" Property="style" PropertyKey="borderColor" />
        </Parallel>
        <Parallel Duration=".5">
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
        <Parallel AnimationTarget="info" Duration=".3" Fps="15">
        <Scale ScaleFactor="0.05" Center="true" ScaleFont="true" FontUnit="px" />
        <FadeOut />
        </Parallel>
        <StyleAction AnimationTarget="info" Attribute="display" Value="none"/>
        <StyleAction AnimationTarget="info" Attribute="width" Value="300px"/>
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
    <span class="LabelsHeaderLarge"><b>Electronic Checks</b></span><br /><br />
    <table width="95%" border="0" cellspacing="0" cellpadding="5">
        <tr>
            <td align="left" valign="bottom">
                <span class="LabelsBody">Electronic Check Service is a payment service
                    that allows online and traditional merchants to accept and process electronic check
                    payments -- either directly from their website storefront or through the
                    Virtual Terminal. </span>
            </td>
            <td align="center" valign="middle" rowspan="2">
                <img src="images/ach.gif" height="150" width="150" border="0" alt="Electronic Check Service">
            </td>
        </tr>
        <tr>
            <td align="left" valign="middle">
                <span class="LabelsBody">Electronic Check Service makes it easy for online businesses to
                    add an electronic check payment option to their Website. Customers who wish to pay
                    for a purchase with an electronic check can simply select the electronic check option
                    and enter the requested bank account information. Electronic Check Service encrypts
                    the data using SSL 3.0 encryption technology and transmits it to the secure server
                    for posting. Transactions are automatically settled each day. Then the system debits
                    the consumer's account via the
                    <asp:LinkButton ID="lnkbtnInfo" CssClass="Link" runat="server" OnClientClick="return false;">Electronic Check Service</asp:LinkButton>
                    <span class="LabelsRed">*</span> and transfers the funds to the merchant's account.
                    If the account numbers are entered incorrectly or the account has insufficient funds,
                    the Electronic Check Service system notifies the merchant. </span>
                <div id="flyout" style="z-index: 2; display: none; border: solid 1px #D0D0D0; background-color: #FFFFFF;
                    overflow: hidden;">
                    &nbsp;</div>
                <div id="info" style="z-index: 2; display: none; font-size: 12px; border: solid 1px #CCCCCC;
                    background-color: #f1fafa; width: 300px; padding: 5px; font-family: Arial; font-size: 10pt;">
                    <div style="float: right; opacity: 0; filter: alpha(opacity=0);" id="btnCloseParent">
                        <asp:LinkButton ID="btnClose" runat="server" OnClientClick="return false;" Text=""
                            Style="text-align: right; border: outset thin white; padding: 5px; text-decoration: none;
                            background-color: #666666; color: White; text-align: center; font-weight: bold;"
                            ToolTip="Close">X</asp:LinkButton>
                    </div>
                    <span style="font-size: 8pt"><span style="font-family: Arial; color: #000000">*</span>The
                        Electronic Check Service is the electronic network which transfers and clears funds
                        between banking institutions on behalf of merchants and their customers. Each transaction
                        takes approximately 48 hours to clear, and we strongly suggest that merchants wait
                        five days from the initial transaction date before shipping any goods. This leaves
                        two days to debit the consumer's account, two days to deposit the funds into the
                        merchant's account, and one day for the bank to post the transaction. Because of
                        this unavoidable delay, Electronic Check Service should not be used by merchants
                        who sell soft or digital goods that are normally downloaded immediately upon completion
                        of the sale. </span>
                </div>
            </td>
        </tr>
        <tr>
            <td align="left" valign="middle" colspan="2">
                <span class="LabelsBody">Traditional merchants without commerce-enabled Web sites can also
                    use Electronic Check Service to handle periodic billings. Businesses such as health
                    clubs, tanning salons, subscription services, ISP's, membership companies, and health
                    care practitioners are typical examples of businesses that need the ability to periodically
                    deduct funds from their customer's checking accounts. Of course, customer approval
                    is always recommended, and merchants must sign a separate Electronic Check Service
                    agreement to perform these types of transactions. </span>
            </td>
        </tr>
        <tr>
            <td align="left" valign="middle" colspan="2">
                <span class="LabelsBody">If you would like to learn more about our Electronic Check Service
                    please read our <a class="Link" href="support.aspx" onmouseover='status=" Electronic check Support";return true;'
                        onmouseout='status=""; return true;'>Support Guide</a>. </span>
            </td>
        </tr>
        <tr>
            <td align="left" valign="middle" colspan="2">
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <div align="center" style="width: 100%;" class="DivHeader">
                    <span class="LabelsWhite"><b>Features and Benefits</b></span><br />
                    <div align="left" style="width: 100%;" class="DivWhiteSmoke">
                        <span class="LabelsBody">These services are available 24 hours a day, 7 days a week real-time
                            without ever needing to speak to customers. </span>
                        <br />
                        <ul class="Bullet">
                            <li style="padding-top:5px" class="LabelsBody">No Credit Turn Downs: Regardless of previous credit history</li>
                            <li class="LabelsBody">100% Financed: No large out-of-pocket initial investment</li>
                            <li class="LabelsBody">Fast Turnaround: Electronic check funds are transferred
                            into your account within 48 hours</li> 
                            <li class="LabelsBody">Access and process transactions anywhere around the
                            globe where you have Internet access</li>
                            <li class="LabelsBody">User friendly, easy to use program</li>
                            <li class="LabelsBody">Solution for automated processing of Internet merchants</li>
                            <li class="LabelsBody">Electronic Check feature - transfer funds directly to
                            merchant's bank account</li>
                            <li class="LabelsBody">Handles recurring monthly payments from check accounts</li>
                            <li class="LabelsBody">SSL security (best on the Internet today)</li>
                            <li class="LabelsBody">Reporting capabilities accessible via the Internet</li>
                            <li class="LabelsBody">Authorizes electronic check transaction within 5 seconds
                            and emails customer's order to merchants for fulfillment</li>
                            <li class="LabelsBody">Encourages impulse buying on the Internet</li>
                            <li class="LabelsBody">Converts your web site from an electronic brochure to
                            a commerce-enabled virtual storefront</li>
                            <li class="LabelsBody">Merchants credit card processing order form can be setup
                            to have the same look and feel as their e-commerce site</li>                           
                        </ul>
                    </div>
                </div>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="4" valign="bottom">
            <br />
            <asp:HyperLink CssClass="Link" Target="_blank" ID="lnkOnlineApplication" runat="server"><strong>
                        Apply Online Now!!!</strong></asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td align="center" valign="bottom" colspan=2><br />
                <a href="javascript:window.history.back();" onmouseover='status=" Back to previous page "; return true;'
                    onmouseout='status=" ";'>
                    <img src="images/back-on.gif" name="back" width="75" height="25" alt="" border="0"></a>
            </td>
        </tr>
    </table>
</asp:Content>
