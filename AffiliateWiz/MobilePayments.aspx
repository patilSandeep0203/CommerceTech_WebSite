<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MobilePayments.aspx.cs" Inherits="MobilePayments" 
    Title="Wireless Solutions" Theme="Admin"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
	<br />
    <table width="97%" align="right" cellspacing="0" cellpadding="0" border="0">
        <tr>
            <td colspan="2" align="center">
                <span class="LabelsHeaderLarge"><strong>Mobile Payments</strong></span><br /><br />
            </td>
        </tr>
        <tr>
            <td align="left">
                <span class="LabelsBody">Mobile payments are the most popular type of wireless payments. With a full array of smart phones and tablets available, 
                you can convert to a mobile merchant by accepting payments directly on your mobile device. You can also manually enter all the 
                payment transaction details using your mobile phones keypad or virtual terminal (web-based transaction) 
				on a PC or MAC.
                </span>
            </td>
            <td align="center" width="25%">
                <img border="0" src="Images/Mobile-Payments.jpg" alt="Mobile Payments" width="150px" />
            </td>
        </tr>
        <tr><td height="15px"></td></tr>
        <tr>
            <td colspan="2" align="center">
                <div align="center" style="width: 80%;" class="DivHeader">
                    <span class="LabelsWhite"><b>Benefits of using mobile payments</b></span><br />
                    <div style="width: 100%; padding-top:5px; padding-bottom:5px" align="left" class="DivWhiteSmoke">
                    <span class="LabelsBody">In addition to the benefits of using Wireless Services, using your mobile phone for credit card 
                        payments has the following benefits  </span>
                        <ul class="Bullet">
                            <li class="LabelsBody">No costly equipment needed</li>
							<li class="LabelsBody">Enable users to select and manage inventory while on the field</li>
                            <li class="LabelsBody">Email receipts to yourself and your customer</li>
                            <li class="LabelsBody">Compatible with most Internet capable devices such as iPhone, Droid, BlackBerry, iPad, iPod Touch, etc.</li> 
                            <li class="LabelsBody">Add multiple phone lines and users to the same account</li>
                            <li class="LabelsBody">CRM function to help you capture customer data</li>
                            <li class="LabelsBody">Signature capture available on selected phones</li>  
                            <li class="LabelsBody">Provide personalized service by accessing customer information and payment history</li>    
                            <li class="LabelsBody">End-to-end encryption providing the highest levels of transaction data security</li>  
                            <li class="LabelsBody">Supports voids and refunds</li>     
                            <li class="LabelsBody">Resend receipts</li>    
                            <li class="LabelsBody">Multiple currency options available</li>                 
                        </ul>
                    </div>
                </div>
            </td>
        </tr>
        <tr><td height="15px"></td></tr>
        <tr>
            <td colspan="2" align="left">
                <span class="LabelsBody">To better serve our customers we have two types Mobile Payments services available. Click below to learn about each service.<br /><br /></span>
            </td>
        </tr> 
        <tr>
            <td colspan="2">
                <table width="50%" align="center" cellspacing="0" cellpadding="0" border="0">
                    <tr>
                        <td align="left">
                            <a class="Link" href="GoPayment.aspx"><img border="0" src="https://lh3.googleusercontent.com/VeIdX5c44a0s2MNxtxW7R4nVXgg0cK_nYNB5sJCJUM82dLn4B9edVwQY3MQzmWBIgkA_=w300-rw" alt="GoPayment" width="125px" /></a>
                        </td>
                        <td align="left">
                            <a class="Link" href="GoPayment.aspx">Intuit GoPayment</a>
                        </td>
                        <td width="20%"></td>
                        <td align="left">
                            <a class="Link" href="authnet.aspx"><img border="0" src="https://wooautoship.com/wp-content/uploads/edd/2015/04/authorize-net-autoship-woocommerce-500.png" alt="Authorize.net" width="125px" /></a>
                        </td>
                        <td align="left">
                            <a class="Link" href="authnet.aspx">Authorize.net</a>                        
                        </td>
                    </tr>
                </table>
            </td>
        </tr>  
        <tr>
            <td align="center" colspan="2" valign="bottom">
            <br />
            <asp:HyperLink CssClass="Link" Target="_blank" ID="lnkOnlineApplication" runat="server"><strong>
                        Apply Online Now!!!</strong></asp:HyperLink>
            </td>
        </tr> 
        </table>
</asp:Content>