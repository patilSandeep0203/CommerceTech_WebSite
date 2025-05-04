<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="prodservices.aspx.cs" Inherits="prodservices" Title="Credit Card Processing, Merchant Services, Payment Processing"
    Theme="Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <br />
    <table width="98%" cellspacing="2" cellpadding="0" border="0" align="right">
        <tr class="DivHeader">
            <td valign="top" align="center" width=10%>
            </td>
            <td valign="top" width=20%>
                <b><span class="LabelsWhite">Products</span></b></td>
            <td valign="top">
                <b><span class="LabelsWhite">Description</span></b>
            </td>
        </tr>        
        <tr>
            <td valign="top" align="center">
                <img alt="Merchant Master" src="images/prodmaster.gif" border="0" width="50" height="25"></td>
            <td valign="top">
                <a class="Link" href="software.aspx">
                    Software</a></td>
            <td valign="top" align="left">
                <span class="LabelsBodySmall">Turn your computer into a credit card
                    terminal. Effective credit card, debit cards processing and check guarantee services
                    for mail, phone order, internet and non-internet businesses.
                </span>
            </td>
        </tr>
        <tr>
            <td valign="top" style="height: 5px" colspan="3" align="center">
            </td>
        </tr>
        <tr>
            <td valign="top" align="center">
                <img alt="Terminals and Printers" src="images/prodterm.gif" border="0" width="50" height="25"></td>
            <td valign="top">
                <a class="Link" href="Equipment.aspx">Equipment</a></td>
            <td valign="top" align="left">
                <span class="LabelsBodySmall">State-of-the-art technology for your
                    physical storefront. The perfect solution for traditional businesses looking for
                    a faster, easier way to authorize and manage credit, ATM, gift card and check guarantee
                    transactions. </span>
            </td>
        </tr>
        <tr>
            <td valign="top" style="height: 5px" colspan="3" align="center">
            </td>
        </tr>
        <tr>
            <td valign="top" align="center">
                <img alt="Terminals and Printers" src="Images/ProductImages/comodo.gif"border="0" width="50" /></td>
            <td valign="top">
                <a href="PCI.aspx" class="Link">HackerGuardian PCI Scan Compliancy</a></td>
            <td align="left" valign="top">
                <span class="LabelsBodySmall">Comodo HackerGuardian helps you achieve PCI compliance. As a PCI Security 
                Standards Council Approved Scanning Vendor (ASV), our service is a critical tool that helps you meet 
                requirements set by the PCI Security Standards Council.
                </span>
            </td>
        </tr>
        <tr>
            <td valign="top" style="height: 5px" colspan="3" align="center">
            </td>
        </tr>
        <tr>
            <td valign="top" align="center">
            <img alt="Terminals and Printers" src="Images/ProductImages/comodo.gif"border="0" width="50" /></td>
            <td valign="top">
                <a href="SSL.aspx" class="Link" >SSL Certificates</a></td>
            <td align="left" valign="top">
                <span class="LabelsBodySmall">Comodo is focused on providing next generation, trust and security enabling products, 
                services and solutions. Secure your web site with strong industry-standard 128/256 bit encryption with 
                trusted digital SSL certificate authentication and encryption.
                </span>
            </td>
        </tr>
        <tr height="10px"></tr>
        <tr class="DivHeader">
            <td valign="top" align="center" width=10%>
            </td>
            <td valign="top" width=20%>
                <b><span class="LabelsWhite">Services</span></b>
            </td>
            <td valign="top">
                <b><span class="LabelsWhite">Description</span></b>
            </td>
        </tr>
        <tr>
            <td valign="top" align="center">
                <img alt="Merchant Services" src="images/prodservices.gif" border="0" width="50" height="25"></td>
            <td valign="top">
                <a class="Link" href="merchant.aspx">US Merchant Services</a>
            </td>
            <td valign="top" align="left">
                <span class="LabelsBodySmall">A merchant account is
                    a commercial account which allows you to accept credit card transactions. Our strong bank relationship ensures an unprecedented 95% approval rate for
                    businesses including startup, home based and high risk. You must meet all of the
                    following 5 criteria in order to qualify for a US merchant Account:<br />
                    1. Signer must have a U.S. Checking account (personal accounts are fine)
                    <br />
                    2. U.S. business address
                    <br />
                    3. U.S. Social Security Number or Canadian Social Insurance Number.
                    <br />
                    4. At least 50% of credit cards accepted from U.S. cardholders.
                    <br />
                    5. Funds deposited in the U.S. dollar only. 6. If legal entity other than a Sole
                    Proprietor, a US FEIN which can be established by filling out and submitting the
                    following </span><a class="LinkSmall" href="http://www.ecenow.com/serrahost/fss4.pdf" target="_blank">
                        <span style="font-size: 8pt">application</span></a><span style="font-size: 8pt">.
                </span>
            </td>
        </tr>
        <tr>
            <td style="height: 5px" valign="top" colspan="3" align="center">
            </td>
        </tr>
        <tr>
            <td valign="top" align="center">
                <img src="images/prodservices.gif" alt="Merchant Services" border="0" width="50" height="25">
            </td>
            <td valign="top">
                <a class="Link" href="international.aspx">International<br />
                    Merchant<br />
                    Services</a>
            </td>
            <td valign="top" align="left">
                <span class="LabelsBodySmall">Businesses that do not
                    meet all of the above criteria must use our International solution. With an International
                    merchant account from us, you will be able to accept credit cards
                    from around the world regardless of where your business or customers are located.
                </span>
            </td>
        </tr>
        <tr>
            <td valign="top" style="height: 5px" colspan="3" align="center">
            </td>
        </tr>
        <tr>
            <td valign="top" align="center">
                <img alt="QuickCommerce" src="images/prodqc.gif" border="0" width="50" height="25"></td>
            <td valign="top">
                <a class="Link" href="gateways.aspx">
                    Secure Payment Gateways</a>
            </td>
            <td valign="top" align="left">
                <span class="LabelsBodySmall">Gateways enable you to automatically
                    accept, authorize and process secure credit card and electronic check orders directly
                    through your web site. </span>
            </td>
        </tr>
        <tr>
            <td valign="top" style="height: 5px" colspan="3" align="center">
            </td>
        </tr>
        <tr>
            <td valign="top" align="center">
                <img alt="Electronic Checks" src="images/prodach.gif" border="0" width="50" height="25"></td>
            <td valign="top">
                <a class="Link" href="echeck.aspx">Electronic Checks</a></td>
            <td valign="top" align="left">
                <span class="LabelsBodySmall">Process secure electronic
                    check transactions from any U.S bank.</span>
            </td>
        </tr>
        <tr>
            <td align="center" style="height: 5px" colspan="3" valign="top">
            </td>
        </tr>
        <tr>
            <td valign="top" align="center">                
                    <img alt="Cross Check" src="images/xcheck.gif" border="0" width="50" height="25">
            </td>
            <td valign="top">
                <a class="Link" href="CheckServices.aspx">Check Services</a></td>
            <td valign="top" align="left">
                <span class="LabelsBodySmall">Our objective is to increase profitability
                    and reduce risk for our valued customers with services which include standard check
                    guarantee programs for large and small retailers, warranty programs, Internet checks
                    and electronic check conversion.
                </span>
            </td>
        </tr>
        <tr>
            <td align="center" style="height: 5px" valign="top" colspan="3">
            </td>            
        </tr>
        <tr>
            <td valign="top" align="center">
                <img height="30" alt="Debit Cards" src="images/check.gif" width="40" border="0"/></td>
            <td valign="top">
                <span class="LabelsHeader">Debit Card Services</span></td>
            <td valign="top" align="left">
                <span class="LabelsBodySmall">Accept any debit card for one transaction
                    fee only. Currently available for retail business only. Give cash back with no risk.
                    Debit available on the Internet only if Visa/MC logo is visible on debit/check card.
                </span>
            </td>
        </tr>
        <tr>
            <td align="center" style="height: 5px" valign="top" colspan="3">
            </td>            
        </tr>
        <tr>
            <td valign="top" align="center">
                <img height="27" alt="Gift Cards" src="Images/Global e-Telecom Ala Carte Gift Card Sample 2a.jpg" width="50" border="0"/></td>
            <td valign="top">
                <a class="Link" href="gift.aspx">Gift/Loyalty
                    Card Services</a></td>
            <td valign="top" align="left">
                <span class="LabelsBodySmall">Retain and Promote repeat customers.
                    Good for layaway, gift certificates or many other uses. Works through your existing
                    equipment. </span>
            </td>
        </tr>
        <tr>
            <td align="center" style="height: 5px" valign="top" colspan="3">
            </td>            
        </tr>
        <tr>
            <td valign="top" align="center">
                <img alt="Business Cash Advance" src="images/bfs.gif" border="0" width="50px"/></td>
            <td valign="top" >
                <a class="Link" href="CashAdvance.aspx">Business Cash Advance
                    </a></td>
            <td valign="top" align="left">
                <span class="LabelsBodySmall">Provides business cash advances throughout the United States to its clients 
                    through accounts receivable financing, utilizing a merchant's sales volume.
                </span>
            </td>
        </tr>
        <tr>
            <td align="center" style="height: 5px" valign="top" colspan="3">
            </td>            
        </tr>
                <tr>
            <td valign="top" align="center">
                <img alt="Business Cash Advance" src="Images/EBT.gif" border="0" width="40px"/></td>
            <td valign="top">
                <span class="LabelsHeader">EBT</span></td>
            <td align="left" valign="top">
                <span class="LabelsBodySmall">Electronic Benefit Transfer (EBT) is an electronic system that allows 
                    a recipient to authorize transfer of their government benefits from a Federal account to a 
                    retailer account to pay for products received.
                </span>
            </td>
        </tr>
        <tr>
            <td align="center" style="height: 5px" valign="top" colspan="3">
            </td>            
        </tr>
        <tr>
            <td valign="top" align="center">
                <img alt="Business Cash Advance" src="Images/ProductImages/OnlinePayroll.gif" border="0" width="40px"/></td>
            <td valign="top">
                <a class="Link" href="Payroll.aspx">Payroll
                    </a></td>
            <td valign="top" align="left">
                <span class="LabelsBodySmall">Compare our two reliable and a convenient payroll service solutions for a hassle free way to 
                pay employees, file payroll taxes and send tax payments making it easy for you to manage payroll while saving time and money.
                </span>
            </td>
        </tr>
        <tr height="10px"></tr>
        <!--<tr>
            <td valign="top" align="center">
            </td>
            <td valign="top">
                <a href="VulnerabilityScan.aspx" class="LabelsHeader">HackerProof Vulnerability Scanning</a></td>
            <td align="left" valign="top">
                <span class="LabelsBodySmall">Convert more visitors into buyers with Comodo's HackerProof Trust Mark and Vulnerability Scanning.
                When you display the HackerProof trust mark, you can be sure that your site is verified by Comodo, the brand that over 100 
                million people associate with security and trust.</span>
            </td>
        </tr>-->
    </table>
</asp:Content>
