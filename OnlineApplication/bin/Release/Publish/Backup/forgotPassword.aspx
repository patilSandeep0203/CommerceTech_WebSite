<%@ Page Language="C#" AutoEventWireup="true" CodeFile="forgotPassword.aspx.cs" Inherits="forgotPassword" %>

<link href="PartnerCSS.css" rel="stylesheet" type="text/css" />

    <!DOCTYPE html>
    <html xmlns="http://www.w3.org/1999/xhtml">
    

    <head runat="server">
        <title>Forgot password ? Recover Password By Email Or User Name example</title>
    </head>
            
    <body>
        <div class="ForgotPasswordPage">
            <form id="form1" runat="server">
        
            
                <p class="ForgotPasswordTitle">Recover Password By User Name</p>
                    <table class="ForgotPasswordTable">
                    <tr>
                    <td style="text-align: center;">
                        <asp:TextBox ID="txtUserName" placeholder="UserName" runat="server" style="width:75%; height: 30px; "></asp:TextBox>
                    </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; padding-top: 25px">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit"
                            onclick="btnSubmit_Click"
                                style="font-family: sans-serif;
		                            text-transform: uppercase;
		                            outline: 0;
		                            background: #5652a2;
		                            width: 75%;
		                            border: 0;
		                            padding: 15px;
		                            color: #FFFFFF;
		                            font-size: 14px;
		                            -webkit-transition: all 0.3 ease;
		                            transition: all 0.3 ease;
		                            cursor: pointer;"/>
                        </td>
                    </tr>
                    <tr>
                    <td colspan="2">
                        <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    </table>
            
            </form>
        </div>
    </body>
         
    </html>
