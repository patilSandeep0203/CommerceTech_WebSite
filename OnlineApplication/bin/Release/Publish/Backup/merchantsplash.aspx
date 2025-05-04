<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="merchantsplash.aspx.cs" Inherits="merchantsplash" Title="Credit Card Processing, Merchant Services, Payment Processing"
    Theme="Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
	<br />
    <table align="right" cellspacing="2" cellpadding="0" border="0" style="width: 98%;">
        <tr>
            <td valign="middle" align="center" colspan="3" class="DivHeader">
                <span class="LabelsWhite"><strong>Merchant Account Logins</strong></span></td>
        </tr>
        <!--<tr>
            <td align="center" >
                <a class="Link" href="https://merchantcenter.intuit.com/innovative" target="_blank">
                <img alt="bp" src="images/bullet.gif" height="8" width="8" border="0" /></a>
            </td>
            <td align="left" width="25%">
                <a class="Link" href="https://merchantcenter.intuit.com/innovative" target="_blank">Intuit - I.M.S./I.P.S</a><span class="LabelsBodyLarge"> *</span><span 
                    style="vertical-align:top" class="LabelsBodySmall"> #</span></td>
            <td align="left">
                <span class="LabelsBody">Available for all merchants. Please call your sales consultant
                    to get set up.</span>
            </td>
        </tr>-->
       
       
        <tr>
            <td align="center">
                <a class="Link" href="https://accounts.cardconnect.com/auth/realms/cardconnect/protocol/openid-connect/auth?response_type=code&client_id=cardpointe&redirect_uri=https%3A%2F%2Fcardpointe.com%2Faccount%2Fsso%2Flogin&state=b61ab063-c980-4aa5-bbfa-ac2a26a1e8a4&login=true&scope=openid" target="_blank">
                    <img alt="bp" src="images/bullet.gif" height="8" width="8" border="0" /></a>
            </td>
            <td align="left" >
                <a class="Link" href="https://accounts.cardconnect.com/auth/realms/cardconnect/protocol/openid-connect/auth?response_type=code&client_id=cardpointe&redirect_uri=https%3A%2F%2Fcardpointe.com%2Faccount%2Fsso%2Flogin&state=b61ab063-c980-4aa5-bbfa-ac2a26a1e8a4&login=true&scope=openid" target="_blank">
                    CardConnect</a><span class="LabelsBodyLarge"> *</span> <span style="vertical-align:top" class="LabelsBodySmall"> #</span>
            </td>
            <td align="left">
                <span class="LabelsBody">Available for all merchants. Please call your sales consultant to get set up.</span><br/>
            </td>
        </tr>
       
        <tr>
            <td align="center" >
                <a class="Link" href="https://merchantcenter.intuit.com/qbms" target="_blank">
                <img alt="bp" src="images/bullet.gif" height="8" width="8" border="0" /></a>
            </td>
            <td align="left" width=25%>
                <a class="Link" href="https://merchantcenter.intuit.com/qbms" target="_blank">Intuit Payment Solutions/IMS</a><span class="LabelsBodyLarge"> *</span><span 
                    style="vertical-align:top" class="LabelsBodySmall"> #</span>
            </td>
            <td align="left">
                <span class="LabelsBody">Available for all merchants. Click on the link to the left and sign up under Merchant Login.</span>
            </td>
        </tr>

        <!--<tr>
            <td align="center" >
                <a class="Link" href="https://merchantcenter.intuit.com/posms" target="_blank">
                <img alt="bp" src="images/bullet.gif" height="8" width="8" border="0" /></a>
            </td>
            <td align="left" width=25%>
                <a class="Link" href="https://merchantcenter.intuit.com/posms" target="_blank">Intuit - QuickBooks POS</a><span class="LabelsBodyLarge"> *</span><span 
                    style="vertical-align:top" class="LabelsBodySmall"> #</span>
            </td>
            <td align="left">
                <span class="LabelsBody">Available for all merchants. Click on the link to the left and sign up under Merchant Login.</span>
            </td>
        </tr>-->
        <tr>
            <td align="center" >
                <a class="Link" href="https://iaccessportal.com/" target="_blank">
                    <img alt="bp" src="images/bullet.gif" height="8" width="8" border="0" /></a>
            </td>
            <td align="left">
                <a class="Link" href="https://iaccessportal.com/" target="_blank">PaySafe/iPayment, Inc.</a><span class="LabelsBodyLarge"> *</span><span 
                    style="vertical-align:top" class="LabelsBodySmall"> #</span>
            </td>
            <td align="left">
                <span class="LabelsBody">iAccess username is your iPayment merchant number and password can be obtained
                    by calling (800) 554-4777.</span>
            </td>
        </tr>
        <tr>
            <td align="center" >
                <a class="Link" href="https://www.mreports.com/portal/start/merrick" target="_blank">
                    <img alt="bp" src="images/bullet.gif" height="8" width="8" border="0" /></a>
            </td>
            <td align="left">
                <a class="Link" href="https://login.netbanx.com" target="_blank">
                    PaySafe/OptimalPayments</a><span class="LabelsBodyLarge"> *</span>
            </td>
            <td align="left">
                <span class="LabelsBody">Available for all merchants. Please call your sales consultant
                    to get set up.</span>
            </td>
        </tr>
        <!--<tr>
            <td align="center" >
                <a class="Link" href="https://secured.payvisionservices.com/Reporter/publish.htm" target="_blank">
                    <img alt="bp" src="images/bullet.gif" height="8" width="8" border="0" /></a>
            </td>
            <td align="left">
                <a class="Link" href="https://secured.payvisionservices.com/Reporter/publish.htm" target="_blank">
                    Payvision</a><span class="LabelsBodyLarge"> *</span>
            </td>
            <td align="left">
                <span class="LabelsBody">Available for all merchants. Please call your sales consultant
                    to get set up.</span>
            </td>
        </tr>-->
        <tr>
            <td align="center">
                <a class="Link" href="https://www.myvirtualreports.com/" target="_blank">
                    <img alt="bp" src="images/bullet.gif" height="8" width="8" border="0"/></a>
            </td>
            <td align="left" >
                <a class="Link" href="https://www.myvirtualreports.com/" target="_blank">
                    Paya/Sage</a><span class="LabelsBodyLarge"> *</span> <span 
                    style="vertical-align:top" class="LabelsBodySmall"> #</span>
            </td>
            <td align="left">
                <span class="LabelsBody">Available for all merchants. Click on the link to the left and sign up under Merchant Login.</span><br />
                <a class="LinkSmall" href="http://na.sage.com/us/sage-payment-solutions/support-resources/online-reporting/?pageid=OnlineReporting_skin" target="_blank">[View Demo]</a><br />
            </td>
        </tr>

                 <tr>
            <td align="center" >
                <a class="Link" href="https://www.pivotal360.com/Login.aspx?ReturnUrl=%2f" target="_blank">
                    <img alt="bp" src="images/bullet.gif" height="8" width="8" border="0" /></a>
            </td>
            <td align="left">
                <a class="Link" href="https://www.pivotal360.com/Login.aspx?ReturnUrl=%2f" target="_blank">Nuvei/Pivotal Payments</a><span class="LabelsBodyLarge"> *</span><span 
                    style="vertical-align:top" class="LabelsBodySmall"> #</span>
            </td>
            <td align="left">
                <span class="LabelsBody"></span>
            </td>
        </tr>

        <!--<tr>
            <td valign="bottom" align="left" colspan="3" >
                <span class="LabelsBodyLarge">*</span><span class="LabelsBodySmall"> Acquires Discover in addition to Visa and MC so Discover Merchant Account detail will 
                be located within this login rather than on the Non-Bankcard Discover web site below.</span>
            </td>
        </tr>-->
        <tr>
            <td valign="bottom" align="left" colspan="3" >
                <span style="vertical-align:text-bottom" class="LabelsBodySmall">#</span><span class="LabelsBodySmall"> Acquires American Express OptBlue in addition to Visa, MC and Discover so American 
                Express Merchant Account detail will be located within this login rather than on the Non Bankcard American Express web site below unlesss is required to remain on ESA.</span>
            </td>
        </tr>
        <tr>
        <td height="10px">
        </td>
        </tr>
        <tr>
            <td valign="middle" align="center" colspan="3" class="DivHeader">
                <span class="LabelsWhite"><b>Non-Bankcard Logins</b></span></td>
        </tr>
        <tr>
            <td align="left">
                <a class="Link" href="https://www125.americanexpress.com/merchant/online/omset/FrontServlet?ln=en&amp;frm=US&amp;request_type=authreg_main&amp;merch_ne=SSO_US_LogIn"
                    target="_blank">
                    <img alt="bp" src="images/bullet.gif" height="8" width="8" border="0" /></a>
            </td>
            <td align="left" width=25%>
                <a class="Link" href="https://www209.americanexpress.com/merchant/services/en_US/pages/home?inav=menu_business_merchhome"
                    target="_blank">American Express</a>
            </td>
            <td align="left">
                <span class="LabelsBody">If you have an American Express ESA Merchant Number, click here to
                    login</span>
            </td>
        </tr>
        <!--<tr>
            <td align="left">
                <a class="Link" href="https://servicecenter.discovernetwork.com/mrc/exec/summary.do"
                    target="_blank">
                    <img alt="bp" src="images/bullet.gif" height="8" width="8" border="0" /></a>
            </td>
            <td align="left">
                <a class="Link" href="https://servicecenter.discovernetwork.com/mrc/exec/summary.do"
                    target="_blank">Discover</a>
            </td>
            <td align="left">
                <span class="LabelsBody">If you have a Discover Merchant Number, click here to login</span>
            </td>
        </tr>-->
                          <tr>
        <td height="10px">
        </td>
        </tr>
        <tr>
            <td valign="middle" align="center" colspan="3" class="DivHeader">
                <b><span class="LabelsWhite">Payment Gateway/Virtual Terminal Logins</span></b></td>
        </tr>
        <tr>
            <td align="left" style="width: 8px">
                <a class="Link" href="https://account.authorize.net">
                    <img alt="bp" src="images/bullet.gif" height="8" width="8" border="0" /></a>
            </td>
            <td align="left" width=25%>
                <a class="Link" href="https://account.authorize.net/" target="_blank">
                    Authorize.net/QuickCommerce</a>
            </td>
            <td align="left">
                <span class="LabelsBody">If you have a Login and Password only, click to log in to Authorize.net or QuickCommerce.<br />
                <!--<a class="LinkSmall" href="https://test.authorize.net" 
                target="_blank">[Test Account -- Login: <b>AuthnetTest1</b> &nbsp; Password: <b>Succeed11</b>]</a><br />-->
                <a class="LinkSmall" href="https://test.authorize.net" 
                target="_blank">[Test Account -- Login: <b>AuthnetTest2</b> &nbsp; Password: <b>Succeed1!</b>]</a><br />
                <!--<a class="LinkSmall" href="https://test.authorize.net" 
                target="_blank">[Test Account -- Login: <b>AuthnetTest3</b> &nbsp; Password: <b>Succeed11</b>]</a></span><br />-->
            </td>
        </tr>
        <tr>
        	<td align="left" style="width: 8px">
        	    <a class="Link" href="https://vpos2.authorize.net/#!/login"  target="_blank">
        	        <img alt="bp" src="images/bullet.gif" height="8" width="8" border="0" /></a>
        	</td>
        	<td align="left">
                <a class="Link" target="_blank" href="https://vpos2.authorize.net/#!/login">
                    Authorize.net VPOS</a>
            </td>
			<td align="left">
			    <span class="LabelsBody">If you have a Login and Password only, click to log in to Authorize.net VPOS.<br /></span>
                <!--<a class="LinkSmall" href="https://vpos2.authorize.net/#!/login" 
                target="_blank">[VPOS Test Account -- Login: <b>vposdemo06</b> &nbsp; Password: <b>Authnet9</b>]</a>-->
            </td>
        </tr>
      
        <tr>
            <td align="left" style="width: 8px">
                <a class="Link" href="https://admin.firepay.com/falcon/login.jsp" target="_blank">
                    <img alt="bp" src="images/bullet.gif" height="8" width="8" border="0" /></a>
            </td>
            <td align="left" >
                <a class="Link" href="https://login.test.netbanx.com" target="_blank">PaySafe/OptimalPayments</a>
            </td>
            <td align="left">
                <span class="LabelsBody">If you have a Username and Password, click to log in to OptimalPayments.<br />
                    [Test Account -- User Name: <b>OptimalSales</b> &nbsp; Password: <b>Abcd1234!</b>]</span><br />
            </td>
        </tr>
                <tr>
            <td align="left" style="width: 8px">
                <a class="Link" href="https://www.sageexchange.com/sep/default.aspx" target="_blank">
                <img alt="bp" src="images/bullet.gif" height="8" width="8" border="0" /></a>
            </td>
            <td align="left" >
                <a class="Link" href="https://www.sageexchange.com/sep/default.aspx" target="_blank">
                    Paya/Sage Exchange</a>
            </td>
            <td align="left">
                <span class="LabelsBody"><br/></span>
                <span class="LabelsHeaderSmall"> 
                </span>
            </td>
        </tr>  
        <tr>
            <td align="left" style="width: 8px">
                <a class="Link" href="https://mysagemobile.com/" target="_blank">
                <img alt="bp" src="images/bullet.gif" height="8" width="8" border="0"/></a>
            </td>
            <td align="left" >
                <a class="Link" href="https://mysagemobile.com/" target="_blank">
                    Paya/Sage Mobile</a>
            </td>
            <td align="left">
                <span class="LabelsBody"><br /></span>
                <span class="LabelsHeaderSmall"> 
                </span>
            </td>
        </tr>    
        <tr>
            <td align="left" style="width: 8px">
                <a class="Link" href="https://www.sagepayments.net/virtualterminal" target="_blank">
                <img alt="bp" src="images/bullet.gif" height="8" width="8" border="0" /></a>
            </td>
            <td align="left" >
                <a class="Link" href="https://www.sagepayments.net/virtualterminal" target="_blank">
                    Paya/Sage Virtual Terminal</a>
            </td>
            <td align="left">
                <span class="LabelsBody">If you have an Account ID, Login ID, and Password, click to log
                    in to Sage Virtual Terminal.<br /></span>
                <span class="LabelsHeaderSmall">[Test Account -- Virtual Terminal ID and Username: <b>537265246813</b> &nbsp; Password: <b>Y2X2V2U1T1F4</b>]<br />
                    </span>
            </td>
        </tr>
        <!--<tr>
            <td align="left" style="width: 8px">
                <a class="Link" href="https://secured.payvisionservices.com/virtualpos/" target="_blank">
                    <img alt="bp" src="images/bullet.gif" height="8" width="8" border="0" /></a>
            </td>
            <td align="left" >
                <a class="Link" href="https://secured.payvisionservices.com/virtualpos/" target="_blank">
                    Payvision Virtual POS</a></td>
            <td align="left">
                <span class="LabelsBody">If you have a Username and Password, install the software and log in to the Payvision Virtual POS.<br /></span>
                    <span class="LabelsHeaderSmall">[Test Account -- Member Id: <b>test2937</b> &nbsp; Login: <b>PVTestAccount</b> &nbsp; Password: <b>VirtualPOS</b>]<br /></span>
            </td>
        </tr> -->
        <tr>
            <td align="left" style="width: 8px">
                <a class="Link" href="https://pay1.plugnpay.com/" target="_blank">
                    <img alt="bp" src="images/bullet.gif" height="8" width="8" border="0" /></a>
            </td>
            <td align="left" >
                <a class="Link" href="https://pay1.plugnpay.com/" target="_blank">Plug'n Pay Gateway</a>
            </td>
            <td align="left">
                <span class="LabelsBody">If you have a Username and Password, click to log in to Plug'n Pay Gateway.&nbsp;</span><br/> 
                <a class="LinkSmall" target="_blank" href="https://pay1.plugnpay.com/">[Test Account -- Login: <b>pnpdemo2</b> &nbsp; Password:<b> 55pnpdemo55</b>]<br/>
                </a>
            </td>
        </tr>
        <!--<tr>
            <td align="left" style="width: 8px">
                <a class="Link" href="http://merchantaccount.quickbooks.com/virtualterminalplus/">
                    <img alt="bp" src="images/bullet.gif" height="8" width="8" border="0" /></a>
            </td>
            <td align="left" >
                <a class="Link" target="_blank" href="http://merchantaccount.quickbooks.com/virtualterminalplus/">
                    QuickBooks<br />Virtual Terminal Plus</a>
            </td>
            <td align="left">
                <span class="LabelsBody">If you have a Login and Password only, click to log in to QuickBooks Virtual Terminal Plus.<br />
                <a class="LinkSmall" href="http://http-download.intuit.com/http.intuit/CMO/payments/demos/products/movies/2009/online_terminals.html?format=noscript?TB_iframe=true&height=520&width=770"
                        target="_blank">[View Demo]</a><br/></span>
            </td>
        </tr>-->

        <!--<tr>
            <td align="left" style="width: 8px">
                <a class="Link" href="https://www.quickcommerce.com/admin/login.taf" target="_blank">
                <img alt="bp" src="images/bullet.gif" height="8" width="8" border="0" /></a>
            </td>
            <td align="left" >
                <a class="Link" href="https://www.quickcommerce.com/admin/login.taf" target="_blank">
                    QuickCommerce<br />Pro/Merchant Partners</a>
            </td>
            <td align="left">
                <span class="LabelsBody">If you have an Account ID, Login ID, and Password, click to log in to QuickCommerce Pro.<br /></span>
                    <span class="LabelsHeaderSmall">[Test Account -- Account ID, Login and Password: <b>edemo</b>]<br />
                    </span>
            </td>
        </tr>-->

        <tr>
            <td align="left" style="width: 8px">
                <a class="Link" href="http://revelsystems.com/" target="_blank">
                <img alt="bp" src="images/bullet.gif" height="8" width="8" border="0" /></a>
            </td>
            <td align="left" >
                <a class="Link" href="http://revelsystems.com/" target="_blank">
                    Revel Systems</a>
            </td>
            <td align="left">
                <span class="LabelsBody"><br/> </span>                    
                <span class="LabelsHeaderSmall"></span>
            </td>
        </tr>
        <!--<tr>
            <td align="left" style="width: 8px">
                <a class="Link" href="https://my.roamdata.com/" target="_blank">
                <img alt="bp" src="images/bullet.gif" height="8" width="8" border="0" /></a>
            </td>
            <td align="left" >
                <a class="Link" href="https://my.roamdata.com/" target="_blank">
                    ROAMpay</a>
            </td>
            <td align="left">
                <span class="LabelsBody"><br/> </span>                    
                <span class="LabelsHeaderSmall"></span>
            </td>
        </tr>-->
    
        <tr>
        <td height="10px">
        </td>
        </tr>         
        <tr>
            <td valign="middle" align="center" colspan="3" class="DivHeader">
                <span class="LabelsWhite"><b>Check Services</b></span></td>
        </tr>
        <tr>
            <td align="left">
                <a class="Link" href="https://www.myonlinereports.com/WebUI/login.aspx"
                    target="_blank">
                    <img alt="bp" src="images/bullet.gif" height="8" width="8" border="0" /></a>
            </td>
            <td align="left" width=25%>
                <a class="Link" href="https://www.myonlinereports.com/WebUI/login.aspx"
                    target="_blank">Paya/Sage Payment Solutions EFT</a>
            </td>
            <td align="left">
                <span class="LabelsBody">If you have an Account ID, Login ID, and Password, click to log
                    in to Sage Payment Solutions EFT check service account.<br /></span>
                <a class="LinkSmall" target="_blank" href="https://demo.eftchecks.com  ">[Test Account -- Login: <b>demoiso</b> &nbsp; Password:<b> 123letmein</b>]<br/>
                    </span>
            </td>
        </tr>
                  <tr>
        <td height="10px">
        </td>
        </tr>
        <tr>
            <td valign="middle" align="center" colspan="3" class="DivHeader">
                <span class="LabelsWhite"><b>Gift/Loyalty Card Services</b></span></td>
        </tr>
        <tr>
            <td align="left">
                <a class="Link" href="https://www.myonlinereports.com/WebUI/login.aspx"
                    target="_blank">
                    <img alt="bp" src="images/bullet.gif" height="8" width="8" border="0" /></a>
            </td>
            <td align="left" width=25%>
                <a class="Link" href="https://www.myonlinereports.com/WebUI/login.aspx"
                    target="_blank">Paya/Sage Payment Solutions EFT</a>
            </td>
                        <td align="left">
                <span class="LabelsBody">If you have a Sage Payment Solutions EFT gift/loyalty card service account, click here to
                    login.<br /></span>
                <a class="LinkSmall" target="_blank" href="https://www.myonlinereports.com/WebUI/login.aspx">[Test Account -- Login: <b>JDemo</b> &nbsp; Password:<b> letmein1</b> &nbsp; Live test gift card number: <b> 02764405830803 </b>]<br/>
                    </span>
            </td>

        </tr>
                  <tr>
        <td height="10px">
        </td>
        </tr>
        <tr>
            <td valign="middle" align="center" colspan="3" class="DivHeader">
                <span class="LabelsWhite"><b>
Merchant Cash Advance</b></span></td>
        </tr>
        <tr>
            <td align="left">
                <a class="Link" href="https://www.bfscapital.com/online-statements/"
                    target="_blank">
                    <img alt="bp" src="images/bullet.gif" height="8" width="8" border="0" /></a>
            </td>
            <td align="left" width=25%>
                <a class="Link" href="https://www.bfscapital.com/online-statements/"
                    target="_blank">Business Financial Services</a>
            </td>
            <td align="left">
                <span class="LabelsBody">If you have a Business Financial Services account, click here to
                    login.</span>
            </td>
        </tr>
        <tr>
            <td align="left">
                <a class="Link" href="https://merchant.opticard.com/logon.asp"
                    target="_blank">
                    <img alt="bp" src="images/bullet.gif" height="8" width="8" border="0" /></a>
            </td>
            <td align="left" width=25%>
                <a class="Link" href="http://yourami.com/"
                    target="_blank">CAN Capital (AdvanceMe)</a>
            </td>
            <td align="left">
                <span class="LabelsBody">If you have a CAN Capital (AdvanceMe) account, click here to
                    login.</span>
            </td>
        </tr>
        <tr>
            <td align="left">
                <a class="Link" href="https://merchant.opticard.com/logon.asp"
                    target="_blank">
                    <img alt="bp" src="images/bullet.gif" height="8" width="8" border="0" /></a>
            </td>
            <td align="left" width=25%>
                <a class="Link" href="https://portal.rapidadvance.com/?getPartnerLoginPage=false"
                    target="_blank">Rapid Advance</a>
            </td>
            <td align="left">
                <span class="LabelsBody">If you have a Rapid Advance account, click here to
                    login.</span>
            </td>
        </tr>
                
        

          <tr>
        <td height="10px">
        </td>
        </tr>
                <tr>
            <td valign="middle" align="center" colspan="3" class="DivHeader">
                <span class="LabelsWhite"><b>
Payroll</b></span></td>
        </tr>
        <tr>
            <td align="left">
                <a class="Link" href="https://fullservicepayroll.intuit.com/login/ifspaclogin.jsf"
                    target="_blank">
                    <img alt="bp" src="images/bullet.gif" height="8" width="8" border="0" /></a>
            </td>
            <td align="left" width=25%>
                <a class="Link" href="https://fullservicepayroll.intuit.com/login/ifspaclogin.jsf"
                    target="_blank">Intuit Payroll Full Service </a>
            </td>
            <td align="left">
                <span class="LabelsBody">If you have a Intuit Payroll Full Service account, click here to
                    login.</span>
            </td>
        </tr>
        <tr>
            <td align="left">
                <a class="Link" href="https://iop.intuit.com/login/login2.jsp"
                    target="_blank">
                    <img alt="bp" src="images/bullet.gif" height="8" width="8" border="0" /></a>
            </td>
            <td align="left" width=25%>
                <a class="Link" href="https://iop.intuit.com/login/login2.jsp"
                    target="_blank">Intuit Online Payroll </a>
            </td>
            <td align="left">
                <span class="LabelsBody">If you have a Intuit Online Payroll account, click here to
                    login.</span>
            </td>
        </tr>
        <tr>
        <td height="20px">
        </td>
        </tr>

        
        
        </table>
</asp:Content>
