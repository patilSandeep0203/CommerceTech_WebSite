<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ResetPassword.aspx.cs" Inherits="ResetPassword" %>
<link href="PartnerCSS.css" rel="stylesheet" type="text/css" />
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>Reset Password</title>
</head>

    <body>
        
        <div class="ResetPasswordPage">
        <form id="form1" runat="server">
        
        <asp:Panel ID="ResetPwdPanel" runat="server" Visible="false" >
        
        <p class="ResetPasswordTitle">Reset Password</p>  
            <table class="ResetPasswordTable">
            <tr>
            <td style ="text-align:right;" >New password: </td>
                <td style ="text-align:left;">
                <asp:TextBox ID="txtNewPwd" runat="server"></asp:TextBox><br />
                <asp:RequiredFieldValidator ID="rfvNewPwd" runat="server"
                    ControlToValidate="txtNewPwd" Display="Dynamic"
                    ErrorMessage="Please enter new password" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
            <td style ="text-align:right;">Confirm Passsword: </td>
                <td style ="text-align:left;">
                <asp:TextBox ID="txtConfirmPwd" runat="server"></asp:TextBox><br />
                <asp:RequiredFieldValidator ID="rfvConfirmPwd" runat="server"
                    ControlToValidate="txtConfirmPwd" Display="Dynamic"
                    ErrorMessage="Please re-enter password to confirm" ForeColor="Red"
                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="cmvConfirmPwd" runat="server"
                    ControlToCompare="txtNewPwd" ControlToValidate="txtConfirmPwd"
                    Display="Dynamic" ErrorMessage="Password didn't match" ForeColor="Red"
                    SetFocusOnError="True"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
            <td>
                &nbsp;</td><td>
                <asp:Button ID="btnChangePwd" runat="server" Text="Change Password"
                        onclick="btnChangePwd_Click"
                        style="font-family: sans-serif;
		                                    text-transform: uppercase;
		                                    outline: 0;
		                                    background: #5652a2;
		                                    width: 85%;
		                                    border: 0;
		                                padding: 15px;
		                                color: #FFFFFF;
		                                font-size: 14px;
		                                -webkit-transition: all 0.3 ease;
		                                transition: all 0.3 ease;
		                                cursor: pointer;"/></td>
        </tr>
            <tr>
            <td colspan="2">
                <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
            </td>
            </tr>
        </table>   

    </asp:Panel>
    <asp:Label ID="lblExpired" runat="server" Text="" style="color: #000000">
    </asp:Label>

            </form>
        </div>
    </body>
</html>

