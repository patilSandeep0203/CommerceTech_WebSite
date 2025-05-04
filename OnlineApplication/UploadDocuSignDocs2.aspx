
    <%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="UploadDocuSignDocs.aspx.cs" Inherits="netimageupload.uploadfiles" Title="Commerce Technologies | Online Application"  %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContentHolder" runat="Server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

        <table border="0" cellpadding="2" cellspacing="0" Width="100%" class="DivGreen" align="center">


                <tr>
            <td colspan="3" align="center" class="DivHeader2" height="26px">
                <b>
                <asp:Label ID="lblHeading" runat="server" CssClass="MenuHeader" Text="Merchant Account / Payment Gateway Signup Information">
                </asp:Label></b>
            </td>
        </tr>
                    <td colspan="3" align="center" class="DivHeader">
                <table border="0" cellpadding="0" cellspacing="0" Width="100%">
                    <tr>
                        <td class="DivHeader" align="center" width="14%">
                            <a href="/Application/Merchant/cardpct.aspx" class="MenuCss">Card Percentages</a>
                        </td>
                        <td class="DivHeader" align="center" width="14%">
                            <a href="/Application/Merchant/companyinfo.aspx" class="MenuCss">Contact Info</a>
                        </td>
                        <td class="DivHeader" align="center" width="14%">
                            <a href="/Application/Merchant/businessinfo.aspx" class="MenuCss">Business Info</a>
                        </td>
                        <td class="DivHeader" align="center" width="14%">
                            <a href="/Application/Merchant/principalinfo.aspx" class="MenuCss">Principal Info</a>
                        </td>
                        <td class="DivHeader" align="center" width="14%">
                           <a href="/Application/Merchant/bankinginfo.aspx" class="MenuCss">Banking Info</a> 
                        </td>
                        <td class="DivHeader2" align="center" width="14%">
                            <span class="MenuHeader">Upload Documents</span>
                        </td>
                         <td class="DivHeader" align="center" width="14%">
                           <a href="/Application/notice.aspx" class="MenuCss">Sign application</a> 
                        </td>
                        
                    </tr>
                </table>
            </td>
        <tr>
            <td align="center" valign="middle">
                <asp:Label ID="lblError" runat="server" BackColor="Red" Font-Size="Medium" ForeColor="White"
                    Visible="False"></asp:Label></td>
        </tr>
        <tr height="20px"></tr>
        <tr>
            <td align="center">
                <asp:Panel ID="pnlIncomplete" runat="server" Width="100%" Visible="False"><asp:Label ID="lblIncomplete" runat="server" CssClass="LabelsRed" 
                Text="Your application is not complete. Click on the red button(s) to the left to complete your application.">
                </asp:Label><br/></asp:Panel>
                <asp:Panel ID="pnlIncompleteGW" runat="server" Width="100%" Visible="False"><asp:Label ID="lblIncompleteGW" runat="server" CssClass="LabelsRed" 
                Text="Your Payment Gateway information is not complete. Click on the red button(s) to the left to complete your application.">
                </asp:Label></asp:Panel>
            </td>
        </tr>

            <tr>
                <td align="center" height: 25px" colspan="2">
                    <asp:Panel ID="pnlAttach" runat="server" Width=100% Visible="True"><asp:Label ID="lbl111" runat="server" ></asp:Label>
            <span class="LabelsBody">Click on "Upload" the following documents or fax/email them to 310-321-5410/applications@commercetech.com:</span>
                <!--<ul class="LabelsBody" style="margin-left:50px; margin-top:0px; margin-bottom:0px;">
                <li>Voided or cancelled check pre-printed with your business name (in place of a temporary check, 
                            a bank signed letter with full account, routing number and business name will need to be provided)</li>
                <li>Business license, phone or utility bill or previous processing statement</li></ul> -->
            </asp:Panel>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">                    
                    <asp:Label CssClass="LabelsError" ID="lblOutput" runat="server"></asp:Label>
                </td>
            </tr>

            <tr height="25px"></tr>
            <tr>
                <td colspan="2" align="center">
                  
                    <asp:Label ID="lblFileName" Text="To verify banking information entered, upload a new, voided or cancelled/cleared check pre-printed with your business/personal name, or if a temporary or starter check, a bank signed letter or bank statement with full account, routing number and business name" runat="server">
                    <ul class="LabelsBody" style="margin-left:50px; margin-top:0px; margin-bottom:0px;">
                <li>To verify banking information entered, upload a new, voided or cancelled/cleared check pre-printed with your business/personal name</li>
                <li>or if a temporary or starter check, a bank signed letter or bank statement with full account, routing number and business name</li></ul> 
                    
                    </asp:Label>
                
                 
                
           
                
                </td>
            </tr>
            <tr>
            <td colspan="2" align="center">
                <input id="filUpload" type="file" name="filUpload" runat="server"/>
            </td>
            <tr>
            <tr height="25px"></tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:Label ID="Label2" Text="For additional verification, upload a Business license, phone or utility bill or previous processing statement" runat="server"></asp:Label>
                </td> 
            </tr>
            <tr>
                    <td colspan="2" align="center">
                        <input id="filUpload2" type="file" name="filUpload2" runat="server"/>
                    </td>
            </tr>
            <tr height="20px"></tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:Label ID="Label1" Text="Supported file format: pdf, jpg, doc, docx, txt, tif, png, xls, xlsx." runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td height="50px" align="center">
                    <asp:Button ID="btnUpload" runat="server" Text="Upload" Width="90px" Height="25px"></asp:Button>
                </td>
            </tr>
        </table>


</asp:Content>