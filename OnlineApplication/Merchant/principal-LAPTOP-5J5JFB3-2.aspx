<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="principal.aspx.cs"
    Inherits="Merchant_principal" Title="Commerce Technologies - Online Application"
    Theme="TextBoxSkin" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentHolder" runat="Server">
    <asp:Panel ID="pnlMainPage" runat="server">
        <table border="0" cellpadding="2" cellspacing="0" Width="100%" class="DivGreen">
            <tr>
                <td colspan="3" align="center" class="DivHeader2" height="26px">
                    <b><span class="MenuHeader">Merchant Account Signup Information</span></b>
                </td>
            </tr>
            <tr>
            <td colspan="3" align="center" class="DivHeader">
                <table border="0" cellpadding="0" cellspacing="0" Width="100%">
                    <tr>
                        <td class="DivHeader" align="center" width="16%">
                            <a href="cardpct.aspx" class="MenuCss">Card Percentages</a>
                        </td>
                        <td class="DivHeader" align="center" width="16%">
                            <a href="companyinfo.aspx" class="MenuCss">Contact Info</a>
                        </td>
                        <td class="DivHeader" align="center" width="16%">
                            <a href="businessinfo.aspx" class="MenuCss">Business Info</a>
                        </td>
                        <td class="DivHeader2" align="center" width="16%">
                            <span class="MenuHeader">Principal Info</span>
                        </td>
                        <td class="DivHeader" align="center" width="16%">
                            <a href="bankinginfo.aspx" class="MenuCss">Banking Info</a>
                        </td>
                        <td class="DivHeader" align="center" width="16%">
                            <a href="/Application/notice.aspx" class="MenuCss">Sign application</a>
                        </td>
                    </tr>
                </table>
            </td>
            </tr>
            <!--<tr>
                <td colspan="3" align="center">
                    <a href="cardpct.aspx" onmouseover="document.mcardpct.src='/Application/Images/oa_CardPercentages_mouseout.gif'"
                        onmouseout="document.mcardpct.src='/Application/Images/oa_CardPercentages_mouseover.gif'">
                        <img name="mcardpct" style="border: 0" alt="" src="/Application/Images/oa_CardPercentages_mouseover.gif" /></a>
                    <a href="companyinfo.aspx" onmouseover="document.mcontact.src='/Application/Images/oa_Contact_mouseout.gif'"
                        onmouseout="document.mcontact.src='/Application/Images/oa_Contact_mouseover.gif'">
                        <img name="mcontact" style="border: 0" alt="" src="/Application/Images/oa_Contact_mouseover.gif" /></a>
                    <a href="businessinfo.aspx" onmouseover="document.mbusiness.src='/Application/Images/oa_Business_mouseout.gif'"
                        onmouseout="document.mbusiness.src='/Application/Images/oa_Business_mouseover.gif'">
                        <img name="mbusiness" style="border: 0" alt="" src="/Application/Images/oa_Business_mouseover.gif" /></a>
                    <img name="mprincipal" style="border: 0" alt="" src="/Application/Images/oa_Principal.gif" />
                    <a href="bankinginfo.aspx" onmouseover="document.mbanking.src='/Application/Images/oa_Banking_mouseout.gif'"
                        onmouseout="document.mbanking.src='/Application/Images/oa_Banking_mouseover.gif'">
                        <img name="mbanking" style="border: 0" alt="" src="/Application/Images/oa_Banking_mouseover.gif" /></a>
                </td>
            </tr>-->
            <tr>
                <td colspan="3" align="center">
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <cc1:PopupControlExtender ID="PopupControlExtender1" runat="server" PopupControlID="pnlSSN"
                        TargetControlID="imgP1SSN" Position="Bottom" />
                    <cc1:PopupControlExtender ID="PopupControlExtender2" runat="server" PopupControlID="pnlOwnership"
                        TargetControlID="imgP1Ownership" Position="Bottom" />
                    <cc1:PopupControlExtender ID="PopupControlExtender3" runat="server" PopupControlID="pnlSSN"
                        TargetControlID="imgP2SSN" Position="Bottom" />
                    <cc1:PopupControlExtender ID="PopupControlExtender4" runat="server" PopupControlID="pnlOwnership"
                        TargetControlID="imgP2Ownership" Position="Bottom" />
                </td>
            </tr>
            <tr>
                <td colspan="3" align="center">
                    <asp:Label ID="lblError" runat="server" BackColor="Red" Font-Size="Medium" ForeColor="White"
                        Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right" colspan="3" height="25px">
                    <asp:Label ID="lblEdit" runat="server" Text='Click on "Edit" to modify Principal Information.'></asp:Label>
                    <!--<asp:HyperLink ID="lnkEditInfo" CssClass="Link" runat="server" NavigateUrl="principalinfo.aspx?EditPrincipal=True">Edit Principal Information</asp:HyperLink></td>-->
                    <asp:Button ID="btnEditInfo" CssClass="Link" runat="server" PostBackUrl="principalinfo.aspx?EditPrincipal=True" Text="Edit" />
            </tr>
            <tr>
                <td colspan="3" align="center" class="DivHeader">
                    <b><span class="MenuHeader">PRINCIPAL #1</span></b>
                </td>
            </tr>
            <tr>
                <td align="right" valign="bottom" Width="50%">
                    <asp:Label ID="lblP1FirstHeader" runat="server" Text="First Name" Font-Bold="False"></asp:Label></td>
                <td align="left" colspan="2">
                    &nbsp;<asp:Label ID="lblP1FirstName" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblP1MiddleHeader" runat="server" Text="Middle Name" Font-Bold="False"></asp:Label></td>
                <td align="left" colspan="2">
                    &nbsp;<asp:Label ID="lblP1MiddleName" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblP1LastHeader" runat="server" Text="Last Name" Font-Bold="False"></asp:Label></td>
                <td align="left" colspan="2">
                    <b>&nbsp;<asp:Label ID="lblP1LastName" runat="server" Font-Bold="True"></asp:Label></b></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblP1EmailHeader" runat="server" Font-Bold="False" Text="Email Address"></asp:Label></td>
                <td align="left" colspan="2">
                    &nbsp;<asp:Label ID="lblP1Email" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblP1TitleHeader" runat="server" Text="Job Title" Font-Bold="False"></asp:Label></td>
                <td align="left" colspan="2">
                    <b>&nbsp;<asp:Label ID="lblP1Title" runat="server" Font-Bold="True"></asp:Label></b></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblP1OwnershipHeader" runat="server" Text="Ownership Percentage" Font-Bold="False"></asp:Label>
                </td>
                <td align="left" colspan="2">
                    &nbsp;<asp:Label ID="lblP1Ownership" runat="server" Font-Bold="True"></asp:Label>
                    <asp:Label ID="lblP1PCT" runat="server" Text="%"></asp:Label>
                    <asp:Image ID="imgP1Ownership" runat="server" ImageUrl="/Application/Images/help.gif" /></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblP1StreetHeader" runat="server" Text="Address" Font-Bold="False"></asp:Label></td>
                <td align="left" colspan="2">
                    &nbsp;<asp:Label ID="lblP1Address" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                </td>
                <td align="left" colspan="2">
                    &nbsp;<asp:Label ID="lblP1Address2" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblP1CityHeader" runat="server" Text="City" Font-Bold="False"></asp:Label></td>
                <td align="left" colspan="2">
                    &nbsp;<asp:Label ID="lblP1City" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblP1StateHeader" runat="server" Text="State" Font-Bold="False"></asp:Label></td>
                <td align="left" colspan="1">
                    &nbsp;<asp:Label ID="lblP1State" runat="server" Font-Bold="True"></asp:Label></td>
                <td align="left" colspan="1">
                    <asp:Label ID="lblP1RegionHeader" runat="server" Text="Region" Font-Bold="False"></asp:Label>&nbsp;
                    <asp:Label ID="lblP1Region" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblP1ZipHeader" runat="server" Text="Zip Code" Font-Bold="False"></asp:Label></td>
                <td align="left" colspan="2">
                    &nbsp;<asp:Label ID="lblP1ZipCode" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblP1CountryHeader" runat="server" Text="Country" Font-Bold="False"></asp:Label></td>
                <td align="left" colspan="2">
                    &nbsp;<asp:Label ID="lblP1Country" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblP1YAAHeader" runat="server" Text="Time at this address?" Font-Bold="False"></asp:Label></td>
                <td align="left" colspan="2">
                    &nbsp;<asp:Label ID="lblP1YearsAtAddress" runat="server" Font-Bold="True"></asp:Label>
                    <asp:Label ID="lblP1Years" runat="server" Font-Bold="True">Years</asp:Label>
                    <asp:Label ID="lblP1And" runat="server" Font-Bold="True">And</asp:Label>
                    <asp:Label ID="lblP1MonthsAtAddress" runat="server" Font-Bold="True"></asp:Label>
                    <asp:Label ID="lblP1Months" runat="server" Font-Bold="True">Months</asp:Label></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblP1PhoneHeader" runat="server" Text="Home Phone" Font-Bold="False"></asp:Label></td>
                <td align="left" colspan="2">
                    &nbsp;<asp:Label ID="lblP1HomePhone" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblP1MobileHeader" runat="server" Text="Mobile Phone" Font-Bold="False"></asp:Label></td>
                <td align="left" colspan="2">
                    &nbsp;<asp:Label ID="lblP1MobilePhone" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblP1DriversNoHeader" runat="server" Text="Driver's License #" Font-Bold="False"></asp:Label></td>
                <td align="left" colspan="2">
                    &nbsp;<asp:Label ID="lblP1DriversLicenseNo" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblP1DriversExpHeader" runat="server" Text="Drivers License State"
                        Font-Bold="False"></asp:Label>
                </td>
                <td align="left" valign="top" colspan="2">
                    &nbsp;<asp:Label ID="lblP1DriversLicenseState" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblP1DriversLicenseExpHeader" runat="server" Font-Bold="False" Text="Drivers License Expiration Date"></asp:Label></td>
                <td align="left" colspan="2">
                    &nbsp;<asp:Label ID="lblP1DriversLicenseExp" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblP1DOBHeader" runat="server" Text="Date of Birth" Font-Bold="False"></asp:Label></td>
                <td align="left" colspan="2">
                    &nbsp;<asp:Label ID="lblP1DOB" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblP1RentOwnHeader" runat="server" Text="Do you rent or own?" Font-Bold="False"></asp:Label>
                </td>
                <td align="left" colspan="2">
                    &nbsp;<asp:Label ID="lblP1RentOwn" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblP1SSNHeader" runat="server" Text="Social Security #" Font-Bold="False"></asp:Label></td>
                <td align="left" colspan="2">
                    &nbsp;<asp:Label ID="lblP1SSN" runat="server" Font-Bold="True"></asp:Label>
                    <asp:Image ID="imgP1SSN" runat="server" ImageUrl="/Application/Images/help.gif" /></td>
            </tr>
            <tr>
                <td align="right" valign="bottom">
                    <asp:Label ID="lblSecondPrincipal" runat="server" Text="Do you have a second Principal?"
                        Font-Bold="False"></asp:Label>
                </td>
                <td align="left" colspan="2">
                    &nbsp;<asp:RadioButton ID="btnYes" runat="server" Font-Size="8pt" GroupName="SecondPrincipal"
                        Text="Yes" AutoPostBack="True" TabIndex="24" Enabled="False" />
                    <asp:RadioButton ID="btnNo" runat="server" Font-Size="8pt" GroupName="SecondPrincipal"
                        Text="No" AutoPostBack="True" TabIndex="26" Enabled="False" />
                <asp:Button ID="btnShowPopup" runat="server" style="display:none" />
                    <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowPopup" PopupControlID="pnlpopup"
>
</cc1:ModalPopupExtender>
<asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="100px" Width="400px" style="display:none">
<table width="100%" style="border:Solid 2px #007b79; width:100%; height:100%" cellpadding="0" cellspacing="0">
<tr >
<td style=" height:10%; color:White; font-weight:bold;padding:3px; font-size:larger; font-family:Arial" align="Left">Confirm Box</td>
<td style=" color:White; font-weight:bold;padding:3px; font-size:10pt" align="Right"> 
<a href = "" onclick = "closepopup()"><!--<img src="Images/Close.gif" style="border :0px" align="right" alt="close"/>-->Close</a>
</td>
</tr>
<tr>
<td colspan="2" align="left" style="padding:5px; font-size:larger; font-family:Arial;">
<asp:Label ID="lblUser" style="padding:5px; font-size:13px; font-family:Arial; font-weight:bold" runat="server"/>
</td>
</tr>
<tr>
<td colspan="2"></td>
</tr>
<tr>
<td>
</td>
<td align="right" style="padding-right:15px">
<asp:Button ID="btnPOS2013Yes" OnClick="btnPOS2013Yes_Click" runat="server" Text="Yes"/>
<asp:Button ID="btnPOS2013No" OnClick="btnPOS2013No_Click" runat="server" Text="No" />
</td>
</tr>
</table>
</asp:Panel></td>
            </tr>
            <tr>
                <td align="center" colspan="3">
                    <asp:Panel ID="pnlPrincipal2" runat="server" Height="100%" Visible="False" Width="100%">
                        <table border="0" width="100%">
                            <tr>
                                <td colspan="3" align="center" class="DivHeader">
                                    <b><span class="MenuHeader">PRINCIPAL #2</span></b>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" Width="50%" valign="bottom">
                                    <asp:Label ID="lblP2FirstHeader" runat="server" Text="First Name" Font-Bold="False"></asp:Label></td>
                                <td align="left" colspan="2">
                                    &nbsp;<asp:Label ID="lblP2FirstName" runat="server" Font-Bold="True"></asp:Label></td>
                            </tr>
                            <tr>
                                <td align="right" valign="bottom">
                                    <asp:Label ID="lblP2MiddleHeader" runat="server" Text="Middle Name" Font-Bold="False"></asp:Label></td>
                                <td align="left" colspan="2">
                                    &nbsp;<asp:Label ID="lblP2MiddleName" runat="server" Font-Bold="True"></asp:Label></td>
                            </tr>
                            <tr>
                                <td align="right" valign="bottom">
                                    <asp:Label ID="lblP2LastHeader" runat="server" Text="Last Name" Font-Bold="False"></asp:Label></td>
                                <td align="left" colspan="2">
                                    &nbsp;<asp:Label ID="lblP2LastName" runat="server" Font-Bold="True"></asp:Label></td>
                            </tr>
                            <tr>
                                <td align="right" valign="bottom">
                                    <asp:Label ID="lblP2EmailHeader" runat="server" Font-Bold="False" Text="Email Address"></asp:Label></td>
                                <td align="left" colspan="2">
                                    &nbsp;<asp:Label ID="lblP2Email" runat="server" Font-Bold="True"></asp:Label></td>
                            </tr>
                            <tr>
                                <td align="right" valign="bottom">
                                    <asp:Label ID="lblP2JobTitleHeader" runat="server" Text="Job Title" Font-Bold="False"></asp:Label></td>
                                <td align="left" colspan="2">
                                    &nbsp;<asp:Label ID="lblP2Title" runat="server" Font-Bold="True"></asp:Label></td>
                            </tr>
                            <tr>
                                <td align="right" valign="bottom">
                                    <asp:Label ID="lblP2OwnershipHeader" runat="server" Text="Ownership Percentage" Font-Bold="False"></asp:Label>
                                </td>
                                <td align="left" colspan="2">
                                    &nbsp;<asp:Label ID="lblP2Ownership" runat="server" Font-Bold="True"></asp:Label>
                                    <asp:Label ID="lblP2PCT" runat="server" Text="%"></asp:Label>
                                    <asp:Image ID="imgP2Ownership" runat="server" ImageUrl="/Application/Images/help.gif" /></td>
                            </tr>
                            <tr>
                                <td align="right" valign="bottom">
                                    <asp:Label ID="lblP2StreetHeader" runat="server" Text="Address" Font-Bold="False"></asp:Label></td>
                                <td align="left" colspan="2">
                                    &nbsp;<asp:Label ID="lblP2Address" runat="server" Font-Bold="True"></asp:Label></td>
                            </tr>
                            <tr>
                                <td align="right" valign="bottom">
                                </td>
                                <td align="left" colspan="2">
                                    &nbsp;<asp:Label ID="lblP2Address2" runat="server" Font-Bold="True"></asp:Label></td>
                            </tr>
                            <tr>
                                <td align="right" valign="bottom">
                                    &nbsp;<asp:Label ID="lblP2CityHeader" runat="server" Text="City" Font-Bold="False"></asp:Label></td>
                                <td align="left" colspan="2">
                                    &nbsp;<asp:Label ID="lblP2City" runat="server" Font-Bold="True"></asp:Label></td>
                            </tr>
                            <tr>
                                <td align="right" valign="bottom">
                                    <asp:Label ID="lblP2StateHeader" runat="server" Text="State" Font-Bold="False"></asp:Label></td>
                                <td align="left" colspan="1">
                                    &nbsp;<asp:Label ID="lblP2State" runat="server" Font-Bold="True"></asp:Label></td>
                                <td align="left" colspan="1">
                                    <asp:Label ID="lblP2RegionHeader" runat="server" Text="Region" Font-Bold="False"></asp:Label>&nbsp;
                                    <asp:Label ID="lblP2Region" runat="server" Font-Bold="True"></asp:Label></td>
                            </tr>
                            <tr>
                                <td align="right" valign="bottom">
                                    <asp:Label ID="lblP2ZipHeader" runat="server" Text="Zip Code" Font-Bold="False"></asp:Label></td>
                                <td align="left" colspan="2">
                                    &nbsp;<asp:Label ID="lblP2ZipCode" runat="server" Font-Bold="True"></asp:Label></td>
                            </tr>
                            <tr>
                                <td align="right" valign="bottom">
                                    <asp:Label ID="lblP2CountryHeader" runat="server" Text="Country" Font-Bold="False"></asp:Label></td>
                                <td align="left" colspan="2">
                                    &nbsp;<asp:Label ID="lblP2Country" runat="server" Font-Bold="True"></asp:Label></td>
                            </tr>
                            <tr>
                                <td align="right" valign="bottom">
                                    <asp:Label ID="lblP2YAAHeader" runat="server" Text="Time at this address?" Font-Bold="False"></asp:Label></td>
                                <td align="left" colspan="2">
                                    &nbsp;<asp:Label ID="lblP2YearsAtAddress" runat="server" Font-Bold="True"></asp:Label>
                                    <asp:Label ID="lblP2Years" runat="server" Font-Bold="True">Years</asp:Label>
                                    <asp:Label ID="lblP2And" runat="server" Font-Bold="True">And</asp:Label>
                                    <asp:Label ID="lblP2MonthsAtAddress" runat="server" Font-Bold="True"></asp:Label>
                                    <asp:Label ID="lblP2Months" runat="server" Font-Bold="True">Months</asp:Label></td>
                            </tr>
                            <tr>
                                <td align="right" valign="bottom">
                                    <asp:Label ID="lblP2PhoneHeader" runat="server" Text="Home Phone" Font-Bold="False"></asp:Label></td>
                                <td align="left" colspan="2">
                                    &nbsp;<asp:Label ID="lblP2HomePhone" runat="server" Font-Bold="True"></asp:Label></td>
                            </tr>
                            <tr>
                                <td align="right" valign="bottom">
                                    <asp:Label ID="lblP2MobileHeader" runat="server" Text="Mobile Phone" Font-Bold="False"></asp:Label></td>
                                <td align="left" colspan="2">
                                    &nbsp;<asp:Label ID="lblP2MobilePhone" runat="server" Font-Bold="True"></asp:Label></td>
                            </tr>
                            <tr>
                                <td align="right" valign="bottom">
                                    <asp:Label ID="lblP2DriversNumHeader" runat="server" Text="Driver's License #" Font-Bold="False"></asp:Label></td>
                                <td align="left" colspan="2">
                                    &nbsp;<asp:Label ID="lblP2DriversLicenseNo" runat="server" Font-Bold="True"></asp:Label></td>
                            </tr>
                            <tr>
                                <td align="right" valign="bottom">
                                    <asp:Label ID="lblP2DriversStateHeader" runat="server" Text="Drivers License State"
                                        Font-Bold="False"></asp:Label>
                                </td>
                                <td align="left" colspan="2">
                                    &nbsp;<asp:Label ID="lblP2DriversLicenseState" runat="server" Font-Bold="True"></asp:Label></td>
                            </tr>
                            <tr>
                                <td align="right" valign="bottom">
                                    <asp:Label ID="lblP2DriversLicenseExpHeader" runat="server" Font-Bold="False" Text="Drivers License Expiration Date"></asp:Label></td>
                                <td align="left" colspan="2">
                                    &nbsp;<asp:Label ID="lblP2DriversLicenseExp" runat="server" Font-Bold="True"></asp:Label></td>
                            </tr>
                            <tr>
                                <td align="right" valign="bottom">
                                    <asp:Label ID="lblP2DOBHeader" runat="server" Text="Date of Birth" Font-Bold="False"></asp:Label></td>
                                <td align="left" colspan="2">
                                    &nbsp;<asp:Label ID="lblP2DOB" runat="server" Font-Bold="True"></asp:Label></td>
                            </tr>
                            <tr>
                                <td align="right" valign="bottom">
                                    <asp:Label ID="lblP2RentOwnHeader" runat="server" Text="Do you rent or own?" Font-Bold="False"></asp:Label></td>
                                <td align="left" colspan="2">
                                    &nbsp;<asp:Label ID="lblP2RentOwn" runat="server" Font-Bold="True"></asp:Label></td>
                            </tr>
                            <tr>
                                <td align="right" valign="bottom">
                                    <asp:Label ID="lblP2SSNHeader" runat="server" Text="Social Security #" Font-Bold="False"></asp:Label></td>
                                <td align="left" colspan="2">
                                    &nbsp;<asp:Label ID="lblP2SSN" runat="server" Font-Bold="True"></asp:Label>
                                    <asp:Image ID="imgP2SSN" runat="server" ImageUrl="/Application/Images/help.gif" /></td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr height="20px">
            </tr> 
            <tr>
                <td colspan="3" align="center" height="25px">
                    <asp:Label ID="lblBottom" runat="server" Text='Click on "Submit" to continue your application.'></asp:Label><br/>
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" TabIndex="48" /></td>
            </tr>
            <tr>
                <td colspan="3" align="center">
                    <asp:Panel ID="pnlSSN" runat="server" Style="display: none; z-index: 1;" Width="250px" CssClass="DivHelp">
                        Your social security number in the form 111223333 (9 digits).</asp:Panel>
                    <asp:Panel ID="pnlOwnership" runat="server" Style="display: none; z-index: 1;" Width="250px" CssClass="DivHelp">
                        The percentage of the company that you own.</asp:Panel>
                </td>
            </tr>
        </table>
    </asp:Panel>
<script language="javascript" type="text/javascript">
function closepopup() {
$find('ModalPopupExtender1').hide();
}

</script>
</asp:Content>
