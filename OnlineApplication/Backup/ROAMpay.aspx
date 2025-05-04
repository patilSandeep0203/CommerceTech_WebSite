<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ROAMpay.aspx.cs" Inherits="ROAMpay" 
    Title="Wireless Solutions" Theme="Admin"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
	<br />
    <table width="97%" align="right" cellspacing="0" cellpadding="0" border="0">
        <tr>
            <td colspan="2" align="center">
                <span class="LabelsHeaderLarge"><strong>ROAMpay</strong></span><br /><br />
            </td>
        </tr>
        <tr>
            <td align="center" valign="top">
                <img border="0" src="Images/ProductImages/roampay.gif" alt="ROAMpay" width="100px" />
            </td>
            <td align="left" valign="top">
                <span class="LabelsBody">
                    ROAMpay service works with both our iPayment and Sage processors. Merchants can process payments on their 
                    mobile device via a downloadable application or a web browser or with a card reader.</span>  
                    <table width="60%" align="center">
                        <tr>
                            <td>
                                <img border="0" src="Images/ProductImages/roampay.jpg" alt="ROAMpay" width="150px" />
                            </td>
                            <td align="left" valign="middle">
                                <span class="LabelsBody">                   
                                    <a href="http://mobile-solutions.ingenico.com/device-compatibility/" target="_blank" class="Link">Click 
                                    here</a> to view list of mobile devices compatible with the ROAMpay application and/or the ROAMpay card reader.</span>                    
                            </td>
                        </tr>
                    </table>                    
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                              
            </td>            
        </tr>
        <tr>
            <td colspan="2" align="center">
                <div align="center" style="width: 70%;" class="DivHeader">
                    <span class="LabelsWhite"><b>Benefits of the ROAMpay service</b></span><br />
                    <div align="left" style="width: 100%;" class="DivWhiteSmoke">
                        <ul class="Bullet">
                            <li style="padding-top:5px" class="LabelsBody">Record cash sales to track all customer sales</li>
                            <li class="LabelsBody">Record transaction when outside of wireless coverage and upload them when you are back in</li>
                            <li class="LabelsBody">Reverse look up automatically fills out entire addresses by only entering in a home phone number if a land line number is linked to the account.</li> 
                            <li class="LabelsBody">Direct Integration with most SAGE accounting and CRM&nbsp; 
							Products for automated accounting functions</li>
                            <li class="LabelsBody">Add an unlimited number of employees with a single account</li>
							<li class="LabelsBody">Export .CSV or EXCEL files to 
							view and track transactions on your PC or transfer 
							to Quicken or QuickBooker.</li>
							<li class="LabelsBody">White label the entire solution to help reinforce your brand.</li>
							<li class="LabelsBody">User management capabilities allowing you to remotely disable access to the application on a mobile device.</li>
							<li class="LabelsBody">Previous purchase recognition with product image and description</li>
							<li class="LabelsBody">Multiple language options available</li>
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
    </table>
</asp:Content>