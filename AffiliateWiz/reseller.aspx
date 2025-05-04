<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="reseller.aspx.cs" Inherits="reseller" 
Title="Credit Card Processing, Merchant Services, Payment Processing" Theme="Admin"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <blockquote style="text-align: center; width:95%" >
    <asp:Label ID="lblError" runat="server" Font-Bold="True" Font-Names="Arial"
        Font-Size="Small" ForeColor="Red" Visible="False"></asp:Label>
    <strong>
        <span class="LabelsHeaderLarge">Reseller Program</span></strong><br /><br />
        <div align=left>
            <span class="LabelsBody">Commerce Technologies is one of the industry’s leaders 
            for Internet, MOTO and Retail credit card processing. We are helping hundreds of new merchants monthly and this number continues 
            to grow. Due to the demand for our secure on-line payment processing, we have implemented the most innovative 
            marketing program designed specifically for financial institutions. Whether you represent a bank, savings & 
            loan, credit union, ISP, hosting association or insurance company, our Reseller Program is for you. Our expertise includes 
            start-ups to large companies. <br /><br />
            </span><span class="LabelsHeader">
            Here are some features and benefits of our <b>Bank Affiliate Program:</b>
            </span><br />
            <ul class="Bullet">
                <li class="LabelsBody">No-Risk Merchant Processing</li>
                <li class="LabelsBody">Fee Income</li>
                <li class="LabelsBody">New Depository Accounts</li>
                <li class="LabelsBody">Account Retention</li>
                <li class="LabelsBody">Advertising Support</li>
                <li class="LabelsBody">24 – Hours a Day Seven Days a Week Customer Service</li>
                <li class="LabelsBody">Over Ten Years Merchant Bankcard Business</li>
                <li class="LabelsBody">Offices Coast to Coast</li>
            </ul><br />
            <span class="LabelsBody">While the referral fee is good, the benefits from our Program will provide you new 
            depository accounts as well as retention of existing accounts and that is the very core for asset growth. 
            <br /><br />
            We look forward to the opportunity to service your organization. 
            <br /><br /></span>
            </div>          
    </blockquote>
    <a class="Link" href="join.aspx?SignupType=Reseller">
        <img height="30" alt="" src="images/joinnow.gif" width="100" border="0"></a>
    <br /><br />
</asp:Content>

