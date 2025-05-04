<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="QuickBooksComparison.aspx.cs" 
    Inherits="QuickBooksComparison" Title="QuickBooks Comparison" Theme="Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
<br />
    <span class="LabelsHeaderLarge"><strong>QuickBooks 2021 Financial Software Comparison</strong></span><br /><br />        
    <table align="right" width="98%" cellspacing=2 cellpadding=0 border=0 class="DivWhiteSmoke;">
        <tr>
            <td align="left" class="DivHeader" valign="middle" width="440px">
                <span class="MenuHeader"><b>&nbsp;Features</b></span></td>
                <td align="center"  class="DivHeader" valign="middle" width="120px">
                <span class="MenuHeader"><b>QuickBooks Online Simple Start</b></span></td> 
            <td align="center"  class="DivHeader" valign="middle" width="120px">
                <span class="MenuHeader"><b>QuickBooks Online Essentials</b></span></td>            
            <td align="center" class="DivHeader" valign="middle"  width="120px">
                <span class="MenuHeader"><b>QuickBooks Online Plus</b></span></td>
                <td align="center" class="DivHeader" valign="middle"  width="120px">
                <span class="MenuHeader"><b>QuickBooks Online Advance</b></span></td>
            <td align="center"  class="DivHeader" valign="middle" width="120px">
                <span class="MenuHeader"><b>QuickBooks Pro</b></span></td>
                <td align="center"  class="DivHeader" valign="middle" width="120px">
                <span class="MenuHeader"><b>QuickBooks Desktop for Mac</b></span></td>
            <td align="center"  class="DivHeader" valign="middle" width="120px">
                <span class="MenuHeader"><b>QuickBooks Premier</b></span></td>
            <td align="center"  class="DivHeader" valign="middle" width="120px">
                <a href="QBEnterpriseSolutions.aspx" class="MenuHeader"><b>QuickBooks Enterprise Solutions</b></a></td>
        </tr>
        <tr height=25>
            <td align="left" valign="middle" colspan=6>
                <span class="LabelsHeader"><strong>Save time tracking finances</strong></span>
                <asp:LinkButton ID="btn1" CssClass="Link" runat="server" Text="Show/Hide" onclick="btn1_click" />                               
            </td>
        </tr>
        <asp:Panel ID="pnl1" runat="server" Visible="true">
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Maximum number of simultaneous users.<span style="vertical-align: text-top; font-size:xx-small">1</span></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">1 user</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">3 users</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">5 users</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">25 users</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">Up to 3 users</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">Up to 3 users</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">Up to 5 users</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">1-10, 30</span></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Track sales, sales taxes & customer payments.</span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
        </tr>

        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Send and track invoices.</span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
        </tr>

        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Accelerated invoicing</span></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
        </tr>

        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Print checks and record transactions</span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Accept credit card payments right in QuickBooks.<span style="vertical-align: text-top; font-size:xx-small">2</span></span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><span class="LabelsBody">Preferred Rates</span></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Access to product experts, unlimited technical support, and upgrades.<span style="vertical-align: text-top; font-size:xx-small">3</span></span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><span class="LabelsBody">With plus subscription</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">With plus subscription</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"><span class="LabelsBody">US-based Experts</span></td>
        </tr>

       
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Online backup and protection of your QuickBooks data.<span style="vertical-align: text-top; font-size:xx-small">3</span></span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><span class="LabelsBody">With plus subscription</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">With plus subscription</span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
        </tr>      
        
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Anywhere, anytime access.<span style="vertical-align: text-top; font-size:xx-small">4</span></span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">Hosting available for an add'l mo cost</span></td>
        </tr>   
        
         <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Set individual user permissions to control access to sensitive information.</span></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><span class="LabelsBody">Limited</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">Limited</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">Limited</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">Up to 115 different activities</span></td>
        </tr>    

        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Create customized financial reports with QuickBooks Statement Writer.</span><span style="vertical-align: text-top; font-size:xx-small">5</span></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">Accountant Ed Only</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></span></td>
        </tr>    

        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Track fixed assets such as computers and other office equipment with Fixed Asset Manager.</span></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">Accountant Ed Only</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></span></td>
        </tr>  

        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Work in two company files at the same time.</span><span style="vertical-align: text-top; font-size:xx-small">6</span></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">Accountant Ed Only</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></span></td>
        </tr>  

         <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Expanded list limits allows you to track 100,000+ customers, vendors and inventory items and employees.</span><span style="vertical-align: text-top; font-size:xx-small">7</span></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">100,000+</span></td>
        </tr>  

        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Leverage 14 predefined user roles to help youset up new users quickly.</span></td>
            
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></span></td>
        </tr> 

        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Complete more activities in multi-user mode.</span></td>
            
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></span></td>
        </tr> 

        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Premium care with Priority Circle</span></td>
            
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
             <td align="center" valign= "middle"><span class="LabelsBody"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></span></td>
        </tr> 


        <tr ><td height="5px"></td></tr>
        </asp:Panel>
        <tr height=25>
            <td align="left" valign="middle" colspan=6>
                <span class="LabelsHeader"><strong>Get the insights to make better decisions</strong></span>
                <asp:LinkButton ID="btn2" CssClass="Link" runat="server" Text="Show/Hide" onclick="btn2_click" />                               
            </td>
        </tr>
        <asp:Panel ID="pnl2" runat="server" Visible="true">
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">One-click financial, sales & tax reports.</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">20+</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">40+</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">65+</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">100+</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">100+</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">100+</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">150+<br/>Industry</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">150+<br/>Industry</span></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Import data from Excel, Quicken, Outlook Contacts, prior QuickBooks versions, & more.<span style="vertical-align: text-top; 
                font-size:xx-small">8</span></span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
        </tr>        
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Download or import your bank & credit transactions into QuickBooks.<span style="vertical-align: text-top; 
                font-size:xx-small">9</span></span></td>
                <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
        </tr>
        
        
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Get a consolidated view into your business with Company Snapshot</span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>            
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
        </tr>
        <tr ><td height="5px"></td></tr>      
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Industry-specific reports, sample files, menus & chart of accounts. </span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">Limited</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">Limited</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">Limited</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">Limited</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">Limited</span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
        </tr>
        <tr ><td height="5px"></td></tr>     
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Track and follow up on sales leads with the Lead Center.</span></td>
            <td><img src="BlankWhiteSmoke.bmp" alt="blank" /></td>
            <td><img src="BlankWhiteSmoke.bmp" alt="blank" /></td>
            <td><img src="BlankWhiteSmoke.bmp" alt="blank" /></td>
            <td><img src="BlankWhiteSmoke.bmp" alt="blank" /></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">See all your key customer information at a glance with the Customer Snapshot.</span></td>
            <td><img src="BlankWhiteSmoke.bmp" alt="blank" /></td>
            <td><img src="BlankWhiteSmoke.bmp" alt="blank" /></td>
            <td><img src="BlankWhiteSmoke.bmp" alt="blank" /></td>
            <td><img src="BlankWhiteSmoke.bmp" alt="blank" /></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Track international sales & expenses in multiple currencies.</span></td>
            <td><img src="BlankWhiteSmoke.bmp" alt="blank" /></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Class tracking</span></td>
            <td><img src="BlankWhiteSmoke.bmp" alt="blank" /></td>
            <td><img src="BlankWhiteSmoke.bmp" alt="blank" /></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Forecast sales & expenses. Easily create a business plan.</span></td>
            <td><img src="BlankWhiteSmoke.bmp" alt="blank" /></td>
            <td><img src="BlankWhiteSmoke.bmp" alt="blank" /></td>
            <td><img src="BlankWhiteSmoke.bmp" alt="blank" /></td>
            <td><img src="BlankWhiteSmoke.bmp" alt="blank" /></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Smart reporting powered by Fathom™</span></td>

            <td><img src="BlankWhiteSmoke.bmp" alt="blank" /></td>
            <td><img src="BlankWhiteSmoke.bmp" alt="blank" /></td>
            <td><img src="BlankWhiteSmoke.bmp" alt="blank" /></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">NEW! Access all of your QuickBooks data to create any report you need with Advanced Reporting.</span><span style="vertical-align: text-top; 
                font-size:xx-small">10</span></td>

            <td><img src="BlankWhiteSmoke.bmp" alt="blank" /></td>
            <td><img src="BlankWhiteSmoke.bmp" alt="blank" /></td>
            <td><img src="BlankWhiteSmoke.bmp" alt="blank" /></td>
            <td><img src="BlankWhiteSmoke.bmp" alt="blank" /></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Consolidate reports from multiple company files.</span><span style="vertical-align: text-top; 
                font-size:xx-small">11</span></td>
<td><img src="BlankWhiteSmoke.bmp" alt="blank" /></td>
            <td><img src="BlankWhiteSmoke.bmp" alt="blank" /></td>
            <td><img src="BlankWhiteSmoke.bmp" alt="blank" /></td>
            <td><img src="BlankWhiteSmoke.bmp" alt="blank" /></td>
            <td><img src="BlankWhiteSmoke.bmp" alt="blank" /></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Create custom reports with QDBC-compliant applications using a direct connection to the QuickBooks database.</span><span style="vertical-align: text-top; 
                font-size:xx-small">12</span></td>
<td><img src="BlankWhiteSmoke.bmp" alt="blank" /></td>
            <td><img src="BlankWhiteSmoke.bmp" alt="blank" /></td>
            <td><img src="BlankWhiteSmoke.bmp" alt="blank" /></td>
            <td><img src="BlankWhiteSmoke.bmp" alt="blank" /></td>
            <td><img src="BlankWhiteSmoke.bmp" alt="blank" /></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
        </tr>

        <tr ><td height="5px"></td></tr>
        </asp:Panel>
        <tr height=25>
            <td align="left" valign="middle" colspan=6>
                <span class="LabelsHeader"><strong>Efficiently manage inventory and pricing</strong></span>
                <asp:LinkButton ID="btn3" CssClass="Link" runat="server" Text="Show/Hide" onclick="btn3_click" />                               
            </td>
        </tr>
        <asp:Panel ID="pnl3" runat="server" Visible="true">
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Track & manage inventory & create purchase orders.</span></td>
            <td><img src="BlankWhiteSmoke.bmp" alt="blank" /></td>
            <td><img src="BlankWhiteSmoke.bmp" alt="blank" /></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
        </tr>
        <tr ><td height="5px"></td></tr>
                <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Set and manage inventory reorder points.</span></td>
            <td><img src="BlankWhiteSmoke.bmp" alt="blank" /></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>      
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
        </tr>
        <tr ><td height="5px"></td></tr>
                <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Easily find and locate inventory tasks all in one place with the inventory center.</span></td>
            <td><img src="BlankWhiteSmoke.bmp" alt="blank" /></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><span class="LabelsBody">Attach Inventory Images</span></td>
        </tr>
        <tr ><td height="5px"></td></tr>
                <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Manage inventory using bin location tracking, bar code scanning, serial number or lot tracking, FIFO costing, and multiple location inventory.</span><span style="vertical-align: text-top; 
                font-size:xx-small">13</span></td>
                <td><img src="BlankWhiteSmoke.bmp" alt="blank" /></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"><span class="LabelsBody">With Advanced Inventory</span></td>
        </tr>
        <tr ><td height="5px"></td></tr>
                <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Control, customize, and automate your pricing right inside QuickBooks.</span><span style="vertical-align: text-top; 
                font-size:xx-small">13</span></td>
                <td><img src="BlankWhiteSmoke.bmp" alt="blank" /></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"><span class="LabelsBody">With Advanced Pricing</span></td>
        </tr>
        </asp:Panel>

        <tr height=25>
            <td align="left" valign="middle" colspan=6>
                <span class="LabelsHeader"><strong>Integrated Services</strong></span>
                <asp:LinkButton ID="LinkButton1" CssClass="Link" runat="server" Text="Show/Hide" onclick="btn4_click" />                               
            </td>
        </tr>
         <asp:Panel ID="pnl4" runat="server" Visible="true">
        <tr ><td height="5px"></td></tr>
                <tr>
            <td align="left" valign="middle">
             <span class="LabelsBody"><a class="Link" target="_blank" href="CheckServices.aspx">Scan and deposit your checks with Intuit Check Solution for QuickBooks - has to be 2010 or later versions</a></span></td>
            <td><img src="BlankWhiteSmoke.bmp" alt="blank" /></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
        </tr>

        <tr ><td height="5px"></td></tr>
                <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody"><a class="Link" target="_blank" href="IntuitPayrollServices.aspx">Manage payroll & payroll taxes; offer Direct Deposit (sold separately)</a></span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
        </tr>

        
        <tr ><td height="5px"></td></tr>
                <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody"><a class="Link" target="_blank" href="merchant.aspx">Swipe credit cards using a card reader</a></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
        </tr>

        <tr ><td height="5px"></td></tr>
                <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody"><a class="Link" target="_blank" href="GoPayment.aspx">Integrates with GoPayment (Mobile Proceesing from Intuit)</a></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody"><a class="Link" target="_blank" href="https://www.firstaffiliates.com/affiliatewiz/fieldServiceMgmtES.aspx">Intuit Field Service Management</a></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
        </tr>
        <tr ><td height="5px"></td></tr>

                <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Integrates with Mac OS X tools such as Numbers, iChat, iCal, MobileMe, and AddressBook</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"></td>
        </tr>
        <tr ><td height="5px"></td></tr>

          <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody"><a class="Link" target="_blank" href="QBPOS.aspx">Integrates with QuickBooks POS</a></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
        </tr>
                 
         <tr ><td height="5px"></td></tr>
       <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody"><a class="Link" target="_blank" href="RevelPOS.aspx">Integrates with Revel POS</a></span></td>

                        <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><img src="https://www.firstaffiliates.com/Affiliatewiz/Images/check2.bmp" alt="check2"/></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>
            <td align="center" valign= "middle"><span class="LabelsBody"></span></td>

        </tr>
        <tr ><td height="5px"></td></tr>
                <tr>
            <td align="left" valign="middle">
            <span class="LabelsBody">Oldest version supported</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">N/A</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">N/A</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">N/A</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">N/A</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">2019</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">2019</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">2019</span></td>
            <td align="center" valign= "middle"><span class="LabelsBody">2019</span></td>
        </tr>
        <tr ><td height="5px"></td></tr>
        </asp:Panel>

         
    </table><br />
    <center>
        <a class="Link" target="_blank" href="https://store.ecenow.com/quickbooks/"><strong>
                        Order Now!!!</strong></a>
    </center><br />
    <table align="center" width="95%" cellspacing=2 cellpadding=0 border=0>
        <tr>
            <td align="left">
                <asp:LinkButton ID="lnkNotes" CssClass="Link" runat="server" Text="Notes and Disclosures" onclick="lnkNotes_click" />
            </td>
        </tr>
        <asp:Panel ID="pnlNotes" runat="server" Visible="false">
        <tr>
            <td align="left">
                <span class="LabelsBodySmall"><span style="vertical-align: text-top; font-size:xx-small">1 </span>
                QuickBooks Pro, Premier, and QuickBooks Enterprise: Requires purchase of a license for each additional user. All copies of QuickBooks must be the same version-year.
                </span>
            </td>
        </tr>
        <!--<tr>
            <td align="left">
                <span class="LabelsBodySmall"><span style="vertical-align: text-top; font-size:xx-small">2 </span>
                Registration and agreement to terms and conditions required.
                </span>
            </td>
        </tr>-->
        <tr>
            <td align="left">
                <span class="LabelsBodySmall"><span style="vertical-align: text-top; font-size:xx-small">2 </span>
                Intuit Merchant Service is an optional fee-based service. Subscription to QuickBooks is required, sold separately. Application approval required. Rates are determined by the financial institution and are subject to change without notice. Transactions are subject to Association guidelines. Terms, conditions, features, pricing, service and support options are subject to change without notice. For additional information see payments.intuit.com/payment-processing. 
                </span>
            </td>
        </tr>
                <tr>
            <td align="left">
                <span class="LabelsBodySmall"><span style="vertical-align: text-top; font-size:xx-small">3 </span>
For QuickBooks Online Plus: Phone support is free during the 30-day trial and included with your paid subscription to QuickBooks Online. Phone support is available Monday through Friday between 6 am - 6 pm PST. Your subscription must be current. Intuit reserves the right to limit the length of the call. Data access is subject to Internet or cellular provider network availability and occasional downtime due to events beyond our control. 128-bit Secure Sockets Layer (SSL) is the same encryption technology used by some of the world's top banking institutions to secure data that is sent over the Internet. Terms, conditions, features, pricing, service and support are subject to change without notice.For QuickBooks Pro/Premier Plus: 24/7 access to QuickBooks experts. Hours exclude occasional downtime due to system and server maintenance, company events, observed U.S. holidays and events beyond our control. Subject to change at any time without notice. Entire PC is limited to 100 GB total storage, which includes all 45 days of backups you have stored. Data is encrypted using AES 256-bit encryption. In order to back up files on a server, the files must be closed. Requires Internet access and QuickBooks Pro (Premier) Plus. Not available to QuickBooks for Mac or QuickBooks Online users. Not intended as a file transfer, remote access solution for your QuickBooks file. Intuit Data Protect is not intended as a HIPAA solution and its use will not assist with or ensure HIPAA compliance. Data Recovery Service coverage begins 15 business days after the date of Plan. Only new data recovery issues that arise after Plan enrollment are covered. Coverage is free for standard turnaround service only. Fees apply for expedited subscription. QuickBooks Pro/Premier Plus provides you with upgrades throughout the year, if and when they become available. Terms, conditions, pricing, features, service and support options are subject to change without notice.For QuickBooks Enterprise: The QuickBooks Enterprise subscription is good for 12 months from QuickBooks Enterprise purchase date subscription renewal date. You have the option to add Intuit Data Protect for free as an active subscriber to QuickBooks Enterprise. Service begins as of the date activated and continues through to the expiration date of your QuickBooks Enterprise subscription. Online Backup Service requires Internet access and an active QuickBooks Enterprise subscription. Entire PC is limited to 100GB total storage, which includes all 45 days which you have stored. In some cases data damage may be too extensive and the data file may be non-recoverable. QuickBooks support is available 24 hours a day, seven days a week; the U.S.-based QuickBooks Enterprise support team is available weekdays from 4:00 a.m.-7:00 p.m. Pacific Standard time. Support hours exclude occasional downtime due to system and server maintenance, company events, observed U.S. holidays and events beyond our control. Intuit reserves the right to limit each telephone contact to one hour and to one incident. Active QuickBooks Enterprise subscription members receive new versions of our products when and if released within 12 months of QuickBooks Enterprise purchase date or subscription renewal date. Online access to training is included with each QuickBooks Enterprise subscription. Terms, conditions, pricing, features, service and support options are subject to change without notice. See terms and conditions at qbes.com/terms.
                </span>
            </td>
        </tr>
        <tr>
            <td align="left">
                <span class="LabelsBodySmall"><span style="vertical-align: text-top; font-size:xx-small">4 </span>
For QuickBooks Online: requires a computer with a supported Internet browser (see System Requirements for a list of supported browsers) and an Internet connection (a high-speed connection is recommended). The QuickBooks Online mobile app works with iPhone, iPad, and Android phones and tablets. Devices sold separately; data plan required. Not all features are available on the mobile apps and mobile browser. QuickBooks Online mobile access is included with your QuickBooks Online subscription at no additional cost. Data access is subject to cellular/internet provider network availability and occasional downtime due to system and server maintenance and events beyond your control. Product registration required.For QuickBooks Pro/Premier Plus: Mobile devices sold separately. Mobile access requires data plan. Apps currently only compatible with select mobile devices (see details here: http://www.workplace.intuit.com/quickbooksconnect) and require registration with corresponding online services. Mobile application works with iOS and Android OS. Mobile and online access requires product registration and an Intuit account. Data access is subject to cellular provider network availability. Terms, conditions, features, availability, pricing, fees, service and support options subject to change without notice. For QuickBooks Enterprise: Internet Explorer 7, Firefox 3 for Windows or Mac, Safari 4 for Mac, Safari 5 for Windows, Chrome 3 or later for Windows or Chrome 4 or later for Mac. Internet connection required. Access is subject to Internet provider network availability and occasional downtime due to systems and server maintenance and events beyond our control. Subject to change without notice.
                </span>
            </td>
        </tr>
        <tr>
            <td align="left">
                <span class="LabelsBodySmall"><span style="vertical-align: text-top; font-size:xx-small">5 </span>
               Requires Microsoft Office 2003 or greater, sold separately. Does not work with Microsoft Office Student Edition or Standard Edition.
                </span>
            </td>
        </tr>        
        <tr>
            <td align="left">
                <span class="LabelsBodySmall"><span style="vertical-align: text-top; font-size:xx-small">6 </span>
                Some functionality may be limited when running two instances. See www.qbes.com/sysreq for more information. For QuickBooks Online: Internet connection required (high-speed connection recommended). Supported browsers: Chrome, Firefox, Internet Explorer 10, Safari 6.1. Also accessible via Chrome on Android and Safari on iOS 7. QuickBooks Online mobile app works with the iPhone, iPad, and Android phones and tablets. Not all features are available on mobile devices.
                </span>
            </td>
        </tr>
        <tr>
            <td align="left">
                <span class="LabelsBodySmall"><span style="vertical-align: text-top; font-size:xx-small">7 </span>
QuickBooks Enterprise allows you to add up to one million names (e.g. customers, vendors, employees) and up to one million items (e.g. inventory, non-inventory, and service items). Some performance degradation is likely as your lists approach these size thresholds.
                </span>
            </td>
        </tr>
        <tr>
            <td align="left">
                <span class="LabelsBodySmall"><span style="vertical-align: text-top; font-size:xx-small">8 </span>
                For QuickBooks Online: Microsoft Word and Excel integration requires Word and Excel 2007, 2010, 2016 or 365. Data can be imported to QuickBooks Online from the following QuickBooks desktop versions: Simple Start Edition; Pro 2017 through 2021; Premier Edition 2017 through 2021; QuickBooks Enterprise 2017 through 2021. Import from QuickBooks desktop must be completed within the first 30 days of setting up your new QuickBooks Online account. For QuickBooks Pro/Premier/QuickBooks Enterprise: Transfer data from Peachtree; Microsoft SBA; and Microsoft Office Accounting using free tool available at www.quickbooks.com/support. Transfer data directly from Quicken 2018-2021, QuickBooks 2017-2021 and Microsoft Excel 2007-2016 or 365.
                </span>
            </td>
        </tr>
        <tr>
            <td align="left">
                <span class="LabelsBodySmall"><span style="vertical-align: text-top; font-size:xx-small">9 </span>
                Online services vary by participating financial institutions or other parties and may be subject to application approval, additional terms, conditions, and fees.
                </span>
            </td>
        </tr>
        <tr>
            <td align="left">
                <span class="LabelsBodySmall"><span style="vertical-align: text-top; font-size:xx-small">10 </span>
Requires QuickBooks Enterprise 2021 with an active QuickBooks Enterprise subscription and an Internet connection. You'll automatically receive any new versions of our product that are released, when and if available, along with updates to your current version.
                </span>
            </td>
        </tr>
        <tr>
            <td align="left">
                <span class="LabelsBodySmall"><span style="vertical-align: text-top; font-size:xx-small">11</span>
                Requires Microsoft Excel 2007, 2010, 2016, or 365. Company files must all be on the same version of QuickBooks Enterprise.
                </span>
            </td>
        </tr>


        <tr>
            <td align="left">
                <span class="LabelsBodySmall"><span style="vertical-align: text-top; font-size:xx-small">12 </span>
Applications sold separately.
                </span>
            </td>
        </tr>

        <tr>
            <td align="left">
                <span class="LabelsBodySmall"><span style="vertical-align: text-top; font-size:xx-small">13 </span>
                Requires QuickBooks Enterprise 2021 with an active QuickBooks Enterprise subscription and an Internet connection. You'll automatically receive any new versions of our product that are released, when and if available, along with updates to your current version.
                </span>
            </td>
        </tr>

        </asp:Panel>

        </table>
        <table>
                
    </table>
</asp:Content>
