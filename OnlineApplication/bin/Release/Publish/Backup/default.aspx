<%@ Page Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="default.aspx.cs"
    Inherits="start" Title="Commerce Technologies - Online Application" Theme="TextBoxSkin" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentHolder" runat="Server">
    <asp:Panel ID="pnlMainPage" runat="server">
        <table style="display:none;">
            <tr>
                <td>
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <cc1:PopupControlExtender ID="PopupControlExtender1" runat="server" PopupControlID="pnlEmailHelp"   
                        TargetControlID="imgLogin" Position="Bottom" />
                </td>
                <asp:Image ID="imgLogin" runat="server" ImageUrl="Images/help.gif" style="display:none;" />
            </tr>            
            <tr>
                <td>
                    <asp:Label ID="lblError1" runat="server" BackColor="Red" Font-Size="Medium" ForeColor="White"
                        Visible="True" Text = "Test"></asp:Label>
                    <asp:ValidationSummary ID="ValidateSummary1" runat="server" Visible="false" ForeColor="Black" HeaderText="Please check the fields marked in red." Width="250px" />
                </td>
            </tr>
        </table>
        <div class="login-page">
            <div class="form">
                <div class="login-form">
                    <p class="title">Merchant Login</p>
                        <asp:TextBox ID="txtLoginName" placeholder="Login Name" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="ValidateLoginReqd" runat="server" ControlToValidate="txtLoginName"></asp:RequiredFieldValidator>
                        <p class="forgot"><asp:HyperLink runat="server" NavigateUrl="forgotPassword.aspx" Text="Forgot Password?"></asp:HyperLink></p>
                        <asp:TextBox ID="txtPassword" placeholder="Password" runat="server" type="password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="ValidatePassword" runat="server" ControlToValidate="txtPassword"
                            EnableClientScript="False" class="wrong"></asp:RequiredFieldValidator>
                        <asp:Button class="button" ID="Button1" runat="server" OnClick="btnLogin_Click" Text="Login"
                            style="font-family: sans-serif;
		                        text-transform: uppercase;
		                        outline: 0;
		                        background: #5652a2;
		                        width: 100%;
		                        border: 0;
		                        padding: 15px;
		                        color: #FFFFFF;
		                        font-size: 14px;
		                        -webkit-transition: all 0.3 ease;
		                        transition: all 0.3 ease;
		                        cursor: pointer;" />
                        <br />
                         <p> <asp:Label ID="lblError" runat="server" BackColor="Red" Font-Size="Medium" ForeColor="White"
                        Visible="False" ></asp:Label>
                    <asp:ValidationSummary ID="ValidateSummary" runat="server" Visible="false" ForeColor="Black" HeaderText="Please check the fields marked in red." Width="250px" /></p> 
                    <p class="message">Not registered? <a href="start.aspx">Create an account</a></p>
                </div>
            </div>
        </div>                
    </asp:Panel>
</asp:Content>
