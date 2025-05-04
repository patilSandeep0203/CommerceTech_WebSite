<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CheckServices.aspx.cs" Inherits="CheckServices" 
    Title="Check Services" Theme="Admin"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <br />
    <table width="95%" align="center" cellspacing="0" cellpadding="0" border="0">
        <tr>
            <td align="center">
                <span class="LabelsHeaderLarge"><strong>Check Services</strong></span><br /><br />
            </td>
        </tr>
        <tr>
            <td align="left">
                <span class="LabelsBody">Check Services are payment services that allow online and traditional merchants to accept and 
                process checks from consumer and corporate bank accounts safely and securely regardless of whether the business is retail, 
                mail/phone order or internet-based.<br /><br />
                Having the ability to accept checks is a vital component of the revenue flow for any business considering the fact that 
                1/3 of non cash payments are made with paper checks. Listed below are facts and statistics on the check processing industry.</span>
                <ul class="Bullet">
                    <li class="LabelsBody">25% of the buying public does not have a major credit card and 83% of the total consumers who do not have a credit card write personal checks when shopping. </li>
                    <li class="LabelsBody">Automated Clearing House (ACH) transactions hits a record of 23 billion transactions valued at more than $51.2 trillion in 2018. </li>
                    <li class="LabelsBody">96% of all checks are now electronified during the clearing process.</li>
                    <li class="LabelsBody">74% of all business-to-business payments are made by check</li>
                </ul>                
            </td>
        </tr>
        <tr><td height="15px"></td></tr>
        <tr>
            <td colspan="2" align="center">
                <div align="center" style="width: 70%;" class="DivHeader">
                    <span class="LabelsWhite"><b>Benefits of accepting checks</b></span><br />
                    <div align="left" style="width: 100%;" class="DivWhiteSmoke">
                        <ul class="Bullet">
                            <li style="padding-top:5px" class="LabelsBody">Increase sales by expanding payment options to your existing and new customers</li>
                            <li class="LabelsBody">Increase customer satisfaction</li>
                            <li class="LabelsBody">Easy access to funds in 2-3 business days since checks are automatically deposited into your checking account electronically</li> 
                            <li class="LabelsBody">No trips to bank - so no gas/labor expenses</li>
                            <li class="LabelsBody">No checks returned due to NSF if using guarantee </li>
                            <li class="LabelsBody">Completely eliminate bank NSF fees if using guarantee </li>
                            <li class="LabelsBody">Check services rates are usually lower than credit card rates</li>
                            <li class="LabelsBody">Easy reconciliation with access to online reports and statements </li>                                             
                        </ul>
                    </div>
                </div>
            </td>
        </tr>
        <tr height=15></tr>
        <tr>
            <td align="left">
                <span class="LabelsBody">You can accept and process checks you receive through</span><br /><br />
            </td>
        </tr>
        <tr>
            <td align="center">
                <img border="0" src="Images/CS1.jpg" alt="Check Services"/>
            </td>
        </tr>
        <tr height=20></tr>
        <tr>
            <td>
                <table align="center" width="100%" cellspacing=2 cellpadding=0 border=0 class="DivWhiteSmoke;">
                    <tr>
                        <td>
                            <img border="0" src="Images/POS.jpg" alt="Point of Sale" width="80px" />
                        </td>
                        <td align="left">
                            <span class="LabelsBody">For face-to-face transactions, Point of Sale Conversion enables merchants to 
                                process checks electronically... just like a credit card. </span>
                        </td>
                        <td>
                            <img border="0" src="Images/Check21.jpg" alt="Check 21+" width="80px" />
                        </td>
                        <td align="left">
                            <span class="LabelsBody">Accept any type of check, in a point of sale or consumer-not-present 
                                environment. Electronic deposit lets you skip the trip to the bank. </span>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <img border="0" src="Images/ChecksByPhone.jpg" alt="Checks By Phone" width="80px" />
                        </td>
                        <td align="left">
                            <span class="LabelsBody">Enabling merchants to accept and process checks from their customers over the phone.</span>
                        </td>
                        <td>
                            <img border="0" src="Images/PaperGuarantee.jpg" alt="Paper Guarantee" width="80px" />
                        </td>
                        <td align="left">
                            <span class="LabelsBody">Guaranteed funding on paper checks accepted face-to-face that are then 
                                manually deposited in the bank. </span>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <a href="echeck.aspx" class="Link"><img border="0" src="Images/ACHDebit.jpg" alt="ACH Debit" width="80px" /></a>
                        </td>
                        <td align="left">
                            <span class="LabelsBody">Process single or recurring ACH debit transactions from the checking account 
                                of a consumer or another business. </span>
                        </td>
                        <td>
                            <img border="0" src="Images/TeleDebit.jpg" alt="Tele-Debit" width="80px" />
                        </td>
                        <td align="left">
                            <span class="LabelsBody">Mobile Merchants can debit consumers checking account utilizing any touch 
                                tone phone. Guaranteed Funding is available. </span>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <img border="0" src="Images/ChecksByWeb.jpg" alt="Checks By Web" width="80px" />
                        </td>
                        <td align="left">
                            <span class="LabelsBody">Consumers initiate check payment on merchants website or payment 
                                gateway (Bill Payments or eCommerce). </span>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <!--<tr>
            <td colspan=2 align="center">
                <a class="Link" href="faqs.aspx#MCA">Click here</a><span class="LabelsBody"> for Frequently Asked Questions.</span>
            </td>
        </tr>-->
        <tr>
            <td align="center" valign="middle" colspan=2>
               <span class="LabelsBody"><a class="Link" href="faqs.aspx#ChkS1">Click here</a>
                for answers to frequently asked questions. </span>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="4" valign="bottom">
            <br />
            <asp:HyperLink CssClass="Link" Target="_blank" ID="lnkOnlineApplication" runat="server"><strong>
                        Apply Online Now!!!</strong></asp:HyperLink>
            </td>
        </tr>   
        <tr height=20></tr> 
    </table>
</asp:Content>

