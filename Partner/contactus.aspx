<%@ Page Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="contactus.aspx.cs"
    Inherits="contactus" Title="Commerce Technologies - Online Application" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentHolder" runat="Server">
   <asp:ScriptManager ID="ScriptManagerContact" runat="server">
    </asp:ScriptManager>
    <table Width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td align="center" valign="top">
                <br />
                <div style="width: 50%" class="DivGreen">
                    <span class="LabelsBody"><b>Commerce Technologies<br />
                        909 N. Sepulveda Blvd.<br />
                        Suite #480<br />
                        El Segundo, CA 90245</b></span>
                    <p>
                        <span class="LabelsBody"><b>Phone: (800) 477-5363 <br />
                        Fax: (310) 321-5410
                       </b></span></p>
                    <p>
                        <span class="LabelsBody"><b>Office Hours: 8
                            AM to 6 PM (Monday to Friday)</b></span></p>
                </div>
                <br />
                <br />
            </td>
        </tr>
        <tr>
            <td align="center">
                <table style="width: 80%;" border="0" cellpadding="0" cellspacing="0" class="DivGreen">
                    <tr>
                        <td align="left">
                            <br />
                            <p>
                                <a class="Link" href="mailto:sales@commercetech.com?subject=Online Application Sales Inquiry"><b>
                                    Sales Department</b></a>
                                <b><span class="LabelsBody">: Questions about our products, services or partnership programs.</span></b>
                            </p>
                            <p>
                                <a class="Link" href="mailto:applications@commercetech.com?subject=Online Application New Accounts Inquiry"><b>
                                    New Accounts</b></a>
                                <b><span class="LabelsBody">: Questions or help with incomplete orders or applications.</span></b>
                            </p>
                            <p>
                                <a class="Link" href="mailto:service@commercetech.com?subject=Online Application Customer Support Inquiry"><b>
                                    Customer Support</b></a>
                                <b><span class="LabelsBody">: Questions or help with existing services.</span></b>
                            </p> 
                            <p>
                                <a class="Link" href="mailto:support@commercetech.com?subject=Online Application Technical Support Inquiry">
                                    <b>Technical Support</b></a>
                                <b><span class="LabelsBody">: Questions or help with programming and technical issues</span></b>
                                <br />
                                <br />
                            </p>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center" valign="middle" style="height: 30px">
                <br />
                <br />
                <a href="javascript:window.history.back();" onmouseover='status=" Back to previous page "; return true;'
                    onmouseout='status=" ";'>
                    <img src="/OnlineApplication/Images/qc-back_off.gif" name="back" width="75" height="25"
                        alt="" style="border: 0" /></a>
            </td>
        </tr>
    </table>
</asp:Content>
