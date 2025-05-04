<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="shopcart.aspx.cs" Inherits="shopcart" Title="Shopping Carts"
    Theme="Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
<br />
    <span class="LabelsHeaderLarge"><strong>Shopping cart Integration with Payment gateways</strong></span><br /><br />        
    <table align="right" width="98%" cellspacing=2 cellpadding=0 border=0 class="DivWhiteSmoke;">
        <tr>
            <td align="left" class="DivHeader" valign="middle" width="400px">
                <span class="MenuHeader"><b>&nbsp;Payment Gateways <br/>---------------------------------------------------<br/>Shopping Cart</b></span></td>
            <td align="center"  class="DivHeader" valign="middle" width="120px">
                <span class="MenuHeader"><b>Authorize.net/<br/>QuickCommerce</b></span></td>
            <td align="center"  class="DivHeader" valign="middle" width="120px">
                <span class="MenuHeader"><b>Paysafe/Optimal Payments/Fire pay/NETBANX</b></span></td>            
            <td align="center" class="DivHeader" valign="middle"  width="120px">
                <span class="MenuHeader"><b>Plug'n Pay</b></span></td>
            <td align="center"  class="DivHeader" valign="middle" width="120px">
                <span class="MenuHeader"><b>Intuit Payment Solutions/Quickbooks Merchant services</b></span></td>
            <td align="center"  class="DivHeader" valign="middle" width="120px">
                <span class="MenuHeader"><b>Paya/Sage Payment Solutions</b></span></td>
            <td align="center"  class="DivHeader" valign="middle" width="120px">
                <span class="MenuHeader"><b>Charge Anywhere</b></span></td>


        </tr>

        
        <tr height=25>
            <td align="left" valign="middle" colspan=6>
                <span class="LabelsHeader"><strong>Shopping Carts that have integrated our payment gateways and are Recommended and Supported.</strong></span>
                            
            </td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody"><a href="http://www.visioncoursemedia.com/" class="Link" target="_blank">BigCommerce</a></span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" /></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <asp:Panel ID="pnl1" runat="server" Visible="true">
        
                 <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody"><a href="http://www.visioncoursemedia.com/" class="Link" target="_blank">Magento</a></span></td>

            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
        </tr>
        <tr ><td height="5px"></td></tr>

         <!--<tr>
            <td align="left" valign="middle">
            <span class="LabelsBody"><a href="http://merchandizer.com/ecommerce-merchant-accounts.html" class="Link" target="_blank">Merchantdizer</a></span></td>

            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
        </tr>
        <tr ><td height="5px"></td></tr>-->
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody"><a href="http://www.pdgsoft.com/partners/ecx.htm" class="Link" target="_blank">PDG Software</a></span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
        </tr>
        
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody"><a href="http://www.shopsite.com/cc_101.html#providers" class="Link" target="_blank">Shopsite</a></span></td>

            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody"><a href="http://www.ultracart.com/resources/partners/supported-payment-gateways/usgateways/ecommerceexchangepro/" class="Link" target="_blank">UltraCart</a></span></td>

            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody"><a href="http://marketplace.x-cart.com/Payment-processing/USA/ECE-Payment-Gateways" class="Link" target="_blank">XCart</a></span></td>

            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
        </tr>
        
        <tr ><td height="5px"></td></tr>
        
        </asp:Panel>
        <tr height=25>
            <td align="left" valign="middle" colspan=6>
                <span class="LabelsHeader"><strong>Shopping Carts that have integrated our payment gateways and are Supported.</strong></span>
               
            </td>
        </tr>
        <asp:Panel ID="pnl2" runat="server" Visible="true">
        

                <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Cart32</span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
        </tr>   
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Cydec</span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>            
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">EC WARE<br/></span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"><br/></td>
            <td align="center" valign= "middle"><br/></td>
            <td align="center" valign= "middle"></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Miva Merchant</span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Premium Web cart (Merchant helper)</span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        
        <tr ><td height="5px"></td></tr>
        </asp:Panel>
        <tr height=25>
            <td align="left" valign="middle" colspan=6>
                <span class="LabelsHeader"><strong>Other Shopping Carts that have integrated our payment gateways.</strong></span>
                   
            </td>
        </tr>
        <asp:Panel ID="pnl3" runat="server" Visible="true">
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">1ShoppingCart</span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">1Touch <br/></span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">3dcart<br/></span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">.net cart<br/></span></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">AbleCommerce<br/></span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Agile Cart<br/></span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
        </tr>
               <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Agoracart<br/></span></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
        </tr>
                <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Americart<br/></span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
        </tr>
                <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">ASecureCart<br/></span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
        </tr>
                <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Ashopcommerce.com<br/></span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">AShop Software<br/></span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">AspDotNetStorefront Shopping Cart<br/></span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Avactis</span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">B2I_Shopping_Cart<br/></span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">BusyBeeCart<br/></span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Bv Commerce<br/></span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Cart Manager</span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">CartFusion<br/></span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Cartit<br/></span></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">CartObjects<br/></span></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Click Cart Pro<br/></span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">CoffeeCup Software<br/></span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Comersus<br/></span></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">CommerceCGI<br/></span></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Crystal Reports 2008</span></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
        </tr>
        
        <tr ><td height="5px"></td></tr>
                <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody"><a href="http://www.cs-cart.com/companion-solutions.html#ty;pagination_contents;/companion-solutions.html?page=3" class="Link" target="_blank">CS-Cart</a></span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"></td>
        </tr>  
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Dansie Shopping cart<br/></span></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
        </tr>
                <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Decentrix Website Solution<br/></span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">digiShop<br/></span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Distributrak<br/></span></td>
            <td align="center" valign= "middle"><br/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"><br/></td>
            <td align="center" valign= "middle"></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Easycart<br/></span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
        </tr>
        <tr ><td height="5px"></td></tr>
                <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Ebay<br/></span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"><br/></td>
            <td align="center" valign= "middle"><br/></td>
            <td align="center" valign= "middle"></td>
        </tr>
        <tr ><td height="5px"></td></tr>
                <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">eCartsoft<br/></span></td>
            <td align="center" valign= "middle"><br/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"><br/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
        </tr>
        <tr ><td height="5px"></td></tr>
                <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Ecommerce templates<br/></span></td>
            <td align="center" valign= "middle"><br/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Evocative Commerce<br/></span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
        </tr>
                <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Fleapay<br/></span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Future webs services<br/></span></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">GoDaddy<br/></span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">GoECart<br/></span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">HostedPCI Online Checkout/Call Center Edition<br/></span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">IA Modules</span></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">I-Bill IT<br/></span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Interspire/ Big Commerce<br/></span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
        </tr>
        
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">KeCommerce<br/></span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Kynet ltd<br/></span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">LiveMerchant<br/></span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Mac Commerce.com<br/></span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Magento<br/></span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Mch solutions<br/></span></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Media chase<br/></span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">MightyMerchant<br/></span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Modernbill<br/></span></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Movylo Shop<br/></span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">MyWebSiteTool<br/></span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Net Member Services<br/></span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">NetStores<br/></span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">nightmedia<br/></span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">onesmartcart<br/></span></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">OpenCart<br/></span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">OwnerReservations.com<br/></span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Pinnacle Cart<br/></span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">PrestaShop<br/></span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"><br/></td>
            <td align="center" valign= "middle"></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">ProductCart<br/></span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">QuickEStore<br/></span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
        </tr>
         <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">RazorCart<br/></span></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">RomanCart<br/></span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Ruby Business Platform<br/></span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">SecureNetShop<br/></span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Shop Factory e-commerce</span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Shopify</span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">ShopStoreNow<br/></span></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">ShopVisible<br/></span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
        </tr>
<tr ><td height="5px"></td></tr>
         <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">SightWorks<br/></span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">SmartPay<br/></span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
        </tr>
         <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Squirrel cart</span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">StoreFront</span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Storesonline<br/></span></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Ubercart<br/></span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Vendio</span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"><br/></td>
            <td align="center" valign= "middle"><br/></td>
            <td align="center" valign= "middle"><br/></td>
            <td align="center" valign= "middle"></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Virtualshop<br/></span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Volusion Ecommerce<br/></span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">webassist<br/></span></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">webbusinessWizard<br/></span></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">wix<br/></span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Woo Commerce<br/></span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
        </tr>
        

        
        
        
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Zen Cart<br/></span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/><br/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
        </tr>
        
        <tr ><td height="5px"></td></tr>
        </asp:Panel>

          
    </table><br />

    
</asp:Content>
