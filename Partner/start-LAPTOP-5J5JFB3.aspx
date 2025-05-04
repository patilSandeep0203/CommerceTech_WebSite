<%@ Page Language="C#" MasterPageFile="site.master" AutoEventWireup="true" CodeFile="start.aspx.cs"
    Inherits="Questions" Title="Commerce Technologies - Online Application" Theme="TextBoxSkin" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentHolder" runat="Server">
    <center>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
        <asp:Label ID="lblError" runat="server" Visible="False"></asp:Label>
        <br /><br />
        <cc1:PopupControlExtender ID="PopupControlExtender1" runat="server" PopupControlID="pnlPDFNote" 
                TargetControlID="imgBrowsers" Position="Bottom" />
        <asp:Image ID="imgBrowsers" runat="server" ImageUrl="~/Images/exclamation.gif"
                ToolTip="Compatible Browsers" /> 
        <asp:Panel ID="pnlPDFNote" runat="server" BackColor="Ivory" BorderColor="silver" BorderWidth="1px" Width="20%">
                <asp:Label ID="lblPDFNote" runat="server" CssClass="LabelsBodySmall"
                Text="NOTE: For the best experience with using the ECE Online Application, use Microsoft Internet Explorer browser (Version 7.0 
                or above), Mozilla Firefox (Version 2.0 or above) or Safari (Version 4.0 or above). In order to view PDFs, please install Abode 
                Acrobat Reader Version 8.0 or higher. Download the latest version at "></asp:Label><asp:HyperLink
                    ID="lnkAdobe" runat="server" CssClass="LinkSmall" NavigateUrl="http://www.adobe.com"
                    Target="_blank">www.adobe.com</asp:HyperLink>
        </asp:Panel>
        <br /><br />
        <div style="width: 35%" class="DivGreen">
            <span class="LabelsHeader"><strong>Already Registered? Click below to Sign In.</strong></span>
            <br />
            <br />
            <asp:Button ID="btnSignIn" runat="server" Text="Sign In" PostBackUrl="default.aspx"  />
            <!--<a href="default.aspx" onmouseover="document.signinBig.src='Images/SigninBig_mouseover.gif'"
                onmouseout="document.signinBig.src='Images/SigninBig.gif'">
                <img name="signinBig" alt="" src="Images/SigninBig.gif" style="border: 0" width="80" height="20" /></a>-->
        </div>
        <br /><br />
        <div style="width: 50%; text-align: left" align="center">
            <strong><span class="LabelsHeader">Not Registered? Get Started with the application
                by answering the question(s) below to determine if you qualify for our U.S. or international
                services.</span></strong>
            <br />
                <table width="100%">
                    <tr height=15px></tr>
                    <asp:Panel ID="pnlQuestions" runat="server" Visible="true">
                    <tr>
                        <td width="4%" align="left" valign="top">
                            <span class="LabelsBody">1.</span>
                        </td>
                        <td align="left">
                            <span class="LabelsBody">Do you have a U. S. checking account (personal accounts are okay)?</span>                            
                        </td>
                        <td align="right" width=20% valign="top">
                            <asp:radioButton runat=server CssClass="LabelsBody" ID=rbtn1Y Text=Yes GroupName=rbtn1 />
                            <asp:radioButton runat=server CssClass="LabelsBody" ID=rbtn1N Text=No GroupName=rbtn1 />
                        </td>
                    </tr>
                    <tr height=15px></tr>
                    <tr>
                        <td width="4%" align="left" valign="middle">
                            <span class="LabelsBody">2.</span>
                        </td>
                        <td align="left">
                            <span class="LabelsBody">Do you have a U. S. physical business and home address?</span></td>
                        <td align="right" width=20% valign="top">
                            <asp:radioButton runat=server CssClass="LabelsBody" ID=rbtn2Y Text=Yes GroupName=rbtn2 />
                            <asp:radioButton runat=server CssClass="LabelsBody" ID=rbtn2N Text=No GroupName=rbtn2 />
                        </td>
                    </tr>
                    <tr height=15px></tr>
                    <tr>
                        <td width="4%" align="left" valign="middle">
                            <span class="LabelsBody">3.</span>
                        </td>
                        <td align="left">
                            <span class="LabelsBody">Can the Principal provide a U. S. Social Security Number?</span>                            
                        </td>
                        <td align="right" width=20% valign="top">
                            <asp:radioButton runat=server CssClass="LabelsBody" ID=rbtn3Y Text=Yes GroupName=rbtn3 />
                            <asp:radioButton runat=server CssClass="LabelsBody" ID=rbtn3N Text=No GroupName=rbtn3 />
                        </td>
                    </tr>
                    </asp:Panel>
                    <tr height=15px></tr>                    
                </table>
                <br />
            <div align="center">
                <asp:Button ID="btnContinue" runat="server" OnClick="btnContinue_Click" Text="Continue"/>
            </div>
        </div> 
    </ContentTemplate>
    </asp:UpdatePanel>       
    </center>
</asp:Content>
