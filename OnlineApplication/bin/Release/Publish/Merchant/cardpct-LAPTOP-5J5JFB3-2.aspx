<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="cardpct.aspx.cs" Inherits="Merchant_cardpct" Title="Commerce Technologies - Online Application"
    Theme="TextBoxSkin" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentHolder" runat="Server">
<asp:Panel ID="pnlMainPage" runat="server">
    <table border="0" cellpadding="2" cellspacing="0" Width="100%" class="DivGreen">
        <tr>
            <td colspan="2" align="center" class="DivHeader2" height="26px">
                <b><span class="MenuHeader">Merchant Account Signup Information</span></b>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center" class="DivHeader">
                <table border="0" cellpadding="0" cellspacing="0" Width="100%">
                    <tr>
                        <td class="DivHeader2" align="center" width="16%">
                            <asp:LinkButton ID="linkCardPct" CssClass="MenuHeader" Text="Card Percentages" runat="server" OnClick="linkCardPctClick"/>
                        </td>
                        <td class="DivHeader" align="center" width="16%">
	                        <asp:LinkButton ID="linkCompany" CssClass="MenuHeader" Text="Contact Info" runat="server" OnClick="linkCompanyClick"/>
                        </td>
                        <td class="DivHeader" align="center" width="16%">
                            <asp:LinkButton ID="linkBusiness" CssClass="MenuHeader" Text="Business Info" runat="server" OnClick="linkBusinessClick"/>
                        </td>
                        <td class="DivHeader" align="center" width="16%">
                            <asp:LinkButton ID="linkPrincipal" CssClass="MenuHeader" Text="Principal Info" runat="server" OnClick="linkPrincipalClick"/>
                        </td>
                        <td class="DivHeader" align="center" width="16%">
                            <asp:LinkButton ID="linkBanking" CssClass="MenuHeader" Text="Banking Info" runat="server" OnClick="linkBankingClick"/>
                        </td>
                        <td class="DivHeader" align="center" width="16%">
                            <asp:LinkButton ID="linkPDF" CssClass="MenuHeader" Text="Sign application" runat="server" OnClick="linkPDFClick"/>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <!--<tr>
            <td colspan="3" align="center">
                <img name="cardpct" style="border: 0" alt="" src="/Application/Images/oa_CardPercentages.gif" />
                <a href="companyinfo.aspx" onmouseover="document.mcontact.src='/Application/Images/oa_Contact_mouseout.gif'"
                    onmouseout="document.mcontact.src='/Application/Images/oa_Contact_mouseover.gif'">
                    <img name="mcontact" style="border: 0" alt="" src="/Application/Images/oa_Contact_mouseover.gif" /></a>
                <a href="businessinfo.aspx" onmouseover="document.mbusiness.src='/Application/Images/oa_Business_mouseout.gif'"
                    onmouseout="document.mbusiness.src='/Application/Images/oa_Business_mouseover.gif'"><img
                    name="mbusiness" style="border: 0" alt="" src="/Application/Images/oa_Business_mouseover.gif" /></a>
                <a href="principalinfo.aspx" onmouseover="document.mprincipal.src='/Application/Images/oa_Principal_mouseout.gif'"
                    onmouseout="document.mprincipal.src='/Application/Images/oa_Principal_mouseover.gif'"><img
                    name="mprincipal" style="border: 0" alt="" src="/Application/Images/oa_Principal_mouseover.gif" /></a>
                <a href="bankinginfo.aspx" onmouseover="document.mbanking.src='/Application/Images/oa_Banking_mouseout.gif'"
                    onmouseout="document.mbanking.src='/Application/Images/oa_Banking_mouseover.gif'">
                    <img name="mbanking" style="border: 0" alt="" src="/Application/Images/oa_Banking_mouseover.gif" /></a>
            </td>
        </tr>-->
        <tr>
            <td>
            <asp:ScriptManager ID="ScriptManagerCardPCT" runat="server">
            </asp:ScriptManager>
            <cc1:PopupControlExtender ID="PopupControlExtender1" runat="server" PopupControlID="pnlSwiped"
                TargetControlID="imgSwiped" Position="Bottom" />
            <cc1:PopupControlExtender ID="PopupControlExtender2" runat="server" PopupControlID="pnlKeyedWith"
                TargetControlID="imgKeyed" Position="Bottom" />
            <cc1:PopupControlExtender ID="PopupControlExtender3" runat="server" PopupControlID="pnlKeyedWithout"
                TargetControlID="imgKeyedWithout" Position="Bottom" />        
            <cc1:PopupControlExtender ID="PopupControlExtender4" runat="server" PopupControlID="pnlOther"
                TargetControlID="imgOther" Position="Bottom" />
            <cc1:PopupControlExtender ID="PopupControlExtender5" runat="server" PopupControlID="pnlRestaurant"
                TargetControlID="imgRestaurant" Position="Bottom" />
            <cc1:PopupControlExtender ID="PopupControlExtender6" runat="server" PopupControlID="pnlMailOrder"
                TargetControlID="imgMailOrder" Position="Bottom" /></td>
                <cc1:PopupControlExtender ID="PopupControlExtender7" runat="server" PopupControlID="pnlInternetOrder"
                TargetControlID="imgInternetOrder" Position="Bottom" />
                <cc1:PopupControlExtender ID="PopupControlExtender8" runat="server" PopupControlID="pnlService"
                TargetControlID="imgService" Position="Bottom" />
                <cc1:PopupControlExtender ID="PopupControlExtender9" runat="server" PopupControlID="pnlRetail"
                TargetControlID="imgRetail" Position="Bottom" />
        </tr>
        <tr valign="top">
            <td align="center" valign="middle" colspan="2">
                <asp:Label ID="lblHeader" visible="false" runat="server" Text="Please provide the following information to begin your application"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center" valign="middle" colspan="2">
                <asp:Label ID="lblError" runat="server" BackColor="Red" Font-Size="Medium" ForeColor="White"
                    Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="right" height="25px">                
                <asp:Label ID="lblEdit" runat="server" Text='Click on "Edit" to modify Card Percentages.'></asp:Label>
                <!--<span style="font-size: 10pt; font-family: Arial"><b><asp:LinkButton ID="lnkEditInfo" CssClass="Link" Visible=false runat="server" OnClick="lnkEditInfo_Click">Edit Card Percentages</asp:LinkButton></b></span>-->
                <asp:Button ID="btnEditInfo" runat="server" OnClick="lnkEditInfo_Click" Text="Edit" Visible="True" /></td>
        </tr>

                    <tr>
                <td align="center" colspan="3">
                    <asp:Label ID="Label1" runat="server" BackColor="Red" Font-Size="Medium" ForeColor="White"
                        Visible="False"></asp:Label><br />
                    <asp:ValidationSummary ID="ValidateSummary" runat="server" BackColor="#FFC0C0" BorderColor="red"
                        BorderWidth="1px" ForeColor="Black" HeaderText="Please check the fields marked in red."
                        Width="250px" />
                </td>
            </tr>
        <tr>
            <td align="center" colspan="3">
                <span class="LabelsRed"><b>*</b> - denotes a required field</span></td>
        </tr>
        <tr valign="top">
            <td valign="middle" align="center" width="30%">
                <span class="LabelsBodySmall"><strong>Estimated Method of Sales<br />Percentages</strong><br />(Sum Must Equal 100%)</span>
            </td>
            <td align="center" valign="top" width="70%">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr valign="bottom">
                        <td align="left" style="width:30%">
                            <span class="LabelsBodySmall">Swiped</span></td>
                        <td align="left" style="width:30%">
                            <span class="LabelsBodySmall">Keyed with Imprint</span></td>
                        <td align="left" style="width:30%">
                            <span class="LabelsBodySmall">Keyed w/o Imprint</span></td>
                    </tr>
                    <tr valign="bottom">
                        <td align="left">
                            <asp:DropDownList ID="lstSwiped" runat="server" OnSelectedIndexChanged="textChangedEventHandler">
                            </asp:DropDownList>
                            <span class="LabelsBodySmall">%</span><span class="LabelsRedLarge">*</span>
                            
                            <asp:Image ID="imgSwiped" runat="server" ImageUrl="/Application/Images/help.gif"/>
                            <asp:RequiredFieldValidator ID="ValidatelstSwiped" runat="server"
                            ControlToValidate="lstSwiped" ErrorMessage="Swiped" Font-Bold="False" EnableClientScript="False"></asp:RequiredFieldValidator></td>
                        <td align="left">
                            <asp:DropDownList ID="lstKeyedWith" runat="server" OnSelectedIndexChanged="textChangedEventHandler">
                            </asp:DropDownList>
                            <span class="LabelsBodySmall">%</span><span class="LabelsRedLarge">*</span>  
                                                      
                                <asp:Image ID="imgKeyed" runat="server" ImageUrl="/Application/Images/help.gif"/>
                                <asp:RequiredFieldValidator ID="ValidatelstKeyedWith" runat="server"
                            ControlToValidate="lstKeyedWith" ErrorMessage="KeyedWith" Font-Bold="False" EnableClientScript="False"></asp:RequiredFieldValidator></td>
                        <td align="left">
                            <asp:DropDownList ID="lstKeyedWithout" runat="server" OnSelectedIndexChanged="textChangedEventHandler">
                            </asp:DropDownList>
                            <span class="LabelsBodySmall">%</span><span class="LabelsRedLarge">*</span> 
                                                                                       
                                <asp:Image ID="imgKeyedWithout" runat="server" ImageUrl="/Application/Images/help.gif"/>
                                 <asp:RequiredFieldValidator ID="ValidatelstKeyedWithout" runat="server"
                            ControlToValidate="lstKeyedWithout" ErrorMessage="KeyedWithout" Font-Bold="False" EnableClientScript="False"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td align="left" valign="top">
                            <span class="LabelsBodySmall">Credit Card will be<br />swiped through a<br />terminal.</span></td>
                        <td align="left">
                            <span class="LabelsBodySmall">Credit Card # will be<br />manually keyed, not<br />swiped. An imprint<br />will be taken.</span></td>
                        <td align="left" valign="top">
                            <span class="LabelsBodySmall">Credit Card # will be<br />manually keyed, not<br />swiped. An imprint<br />will NOT be taken.</span></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr valign="top">
            <td colspan="2" height="22px">
            </td>
        </tr>
        <tr>
            <td colspan="2">
            <hr color="a1b6b6" style="border:none; height:1px;"/>
            </td>
        </tr>
                <tr valign="top">
            <td colspan="2" height="10px">
            </td>
        </tr>
        <tr>
            <td valign="middle" align="center" width="35%">
                <span class="LabelsBodySmall"><strong>Estimated type of Business<br />Percentages</strong><br />(Sum Must Equal 100%)</span>
            </td>
            <td align="center" valign="top">
                <table border="0" cellpadding="0" cellspacing="0" Width="100%">
                    <tr valign="bottom">
                        <td align="left" style="width:30%">
                            <span class="LabelsBodySmall">Service</span></td>
                        <td align="left" style="width:30%">
                            <span class="LabelsBodySmall">Retail</span></td>
                        <td align="left" style="width:30%">
                            <span class="LabelsBodySmall">Restaurant</span></td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:DropDownList ID="lstService" runat="server" OnSelectedIndexChanged="textChangedEventHandler">
                            </asp:DropDownList>
                            <span class="LabelsBodySmall">%</span><span class="LabelsRedLarge"></span>
                            <asp:Image ID="imgService" runat="server" ImageUrl="/Application/Images/help.gif"/>
                            <asp:RequiredFieldValidator ID="ValidatelstService" runat="server"
                            ControlToValidate="lstService" ErrorMessage="Service" Font-Bold="False" EnableClientScript="False"></asp:RequiredFieldValidator>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="lstRetail" runat="server" OnSelectedIndexChanged="textChangedEventHandler">
                            </asp:DropDownList>
                            <span class="LabelsBodySmall">%</span><span class="LabelsRedLarge">*</span>
                            <asp:Image ID="imgRetail" runat="server" ImageUrl="/Application/Images/help.gif"/>
                            <asp:RequiredFieldValidator ID="ValidatelstRetail" runat="server"
                            ControlToValidate="lstRetail" ErrorMessage="Retail" Font-Bold="False" EnableClientScript="False"></asp:RequiredFieldValidator>
                        <a href="javascript:popupHelp(8)"></a>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="lstRestaurant" runat="server" OnSelectedIndexChanged="textChangedEventHandler">
                            </asp:DropDownList>
                            <span class="LabelsBodySmall">%</span><span class="LabelsRedLarge">*</span>

                                <asp:Image ID="imgRestaurant" runat="server" ImageUrl="/Application/Images/help.gif"/>
                                                            <asp:RequiredFieldValidator ID="ValidatelstRestaurant" runat="server"
                            ControlToValidate="lstRestaurant" ErrorMessage="Restaurant" Font-Bold="False" EnableClientScript="False"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr height=15></tr>
                    <tr valign="bottom">
                        <td align="left">
                            <span class="LabelsBodySmall">Mail/Phone Order</span></td>
                        <td align="left">
                            <span class="LabelsBodySmall">Internet</span></td>
                        <td align="left">
                            <span class="LabelsBodySmall">Other</span></td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:DropDownList ID="lstMail" runat="server" OnSelectedIndexChanged="textChangedEventHandler">
                            </asp:DropDownList>
                            <span class="LabelsBodySmall">%</span><span class="LabelsRedLarge">*</span>
                                                        
                            <asp:Image ID="imgMailOrder" runat="server" ImageUrl="/Application/Images/help.gif"/>
                            <asp:RequiredFieldValidator ID="ValidatelstMail" runat="server"
                            ControlToValidate="lstMail" ErrorMessage="Mail" Font-Bold="False" EnableClientScript="False"></asp:RequiredFieldValidator></td>
                        <td align="left">
                            <asp:DropDownList ID="lstInternet" runat="server" OnSelectedIndexChanged="textChangedEventHandler">
                            </asp:DropDownList>
                            <span class="LabelsBodySmall">%</span><span class="LabelsRedLarge">*</span>
                            <asp:Image ID="imgInternetOrder" runat="server" ImageUrl="/Application/Images/help.gif"/>
                            <asp:RequiredFieldValidator ID="ValidatelstInternet" runat="server"
                            ControlToValidate="lstInternet" ErrorMessage="Internet" Font-Bold="False" EnableClientScript="False"></asp:RequiredFieldValidator>
                            </td>
                        <td align="left">
                            <asp:DropDownList ID="lstOther" runat="server" OnSelectedIndexChanged="textChangedEventHandler">
                            </asp:DropDownList>
                            <span class="LabelsBodySmall">%</span><span class="LabelsRedLarge">*</span>
                                                        
                            <asp:Image ID="imgOther" runat="server" ImageUrl="/Application/Images/help.gif"/>
                            <asp:RequiredFieldValidator ID="ValidatelstOther" runat="server"
                            ControlToValidate="lstOther" ErrorMessage="Other" Font-Bold="False" EnableClientScript="False"></asp:RequiredFieldValidator>
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
</asp:Panel>

<asp:Button ID="btnShowNavigateNextPopup" runat="server" style="display:none" />
                    <cc1:ModalPopupExtender ID="NavigatePopupModalExtender" runat="server" TargetControlID="btnShowNavigateNextPopup" PopupControlID="pnlNavigateUnsaved"
>
</cc1:ModalPopupExtender>

<asp:Panel ID="pnlNavigateUnsaved" runat="server" BackColor="White" Height="100px" Width="400px" style="display:none">
<table width="100%" style="border:Solid 2px #007b79; width:100%; height:100%" cellpadding="0" cellspacing="0">
<tr >
<td style=" height:10%; color:White; font-weight:bold;padding:3px; font-size:larger; font-family:Arial" align="Left">Confirm Box</td>
<td style=" color:White; font-weight:bold;padding:3px; font-size:10pt" align="Right"> 
<a href = "" onclick = "closepopup()"><!--<img src="Images/Close.gif" style="border :0px" align="right" alt="close"/>-->Close</a>
</td>
</tr>
<tr>
<td colspan="2" align="left" style="padding:5px; font-size:larger; font-family:Arial;">
<asp:Label ID="lblUserNavigateUnsaved" style="padding:5px; font-size:13px; font-family:Arial; font-weight:bold" runat="server"/>
</td>
</tr>
<tr>
<td colspan="2"></td>
</tr>
<tr>
<td>
</td>
<td align="right" style="padding-right:15px">
<asp:Button ID="btnNavigateNext" OnClick="btnNavigateNext_Click" runat="server" Text="Yes"/>
<asp:Button ID="btnDoNotNavigate" OnClick="btnDoNotNavigate_Click" runat="server" Text="No" />
</td>
</tr>
</table>
</asp:Panel>
                        
                </td>
            </tr>
            <tr>
            <td colspan="2" align="center" height="25px">
               
        </tr>
            <tr>
            <td colspan="2" align="center" height="25px">
                <asp:Label ID="lblBottom" runat="server" Text='Click on "Submit" to continue your application.'></asp:Label><br/>
                <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" /></td>
        </tr>
        </table>
    </asp:Panel>
                            </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr height="20px">
        </tr>
        <!--<tr>
            <td colspan="2" align="right" height="25px">
            <asp:Label ID="lblSave" runat="server" Text='Click "Save" to save your partially completed information. You may finish completing this page another time'></asp:Label>
            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" Visible="False" /></td>
        </tr>-->
    </table>
    </asp:Panel>
    <asp:Panel ID="pnlSwiped" runat="server" Style="display: none; z-index: 1;" Width="250px"  CssClass="DivHelp">
        The percentage of your 
                                sales when the customer is physically present and their signature is verified 
                                and the card has been swiped through a terminal.</asp:Panel>                                
    <asp:Panel ID="pnlKeyedWith" runat="server" Style="display: none; z-index: 1;" Width="250px" CssClass="DivHelp">
    The percentage of your sales when the 
    customer is physically present and their signature is verified and the card has been manually imprinted.
    </asp:Panel>
    <asp:Panel ID="pnlKeyedWithout" runat="server" Style="display: none; z-index: 1;" Width="250px" CssClass="DivHelp">
    The percentage of your 
    sales when the customer is NOT physically present. The customer either placed the order online or the 
    transaction information was manually entered into a terminal.</asp:Panel>
    <asp:Panel ID="pnlMailOrder" runat="server" Style="display: none; z-index: 1;" Width="250px" CssClass="DivHelp">
    
    The percentage of your sales where you are selling your products/services and accepting payments for the same over the phone or in the mail.</asp:Panel>
    <asp:Panel ID="pnlRestaurant" runat="server" Style="display: none; z-index: 1;" Width="250px" CssClass="DivHelp">
    The percentage of your sales where you are selling food, beverages and other edibles while they are dining at your business location.</asp:Panel>
    <asp:Panel ID="pnlService" runat="server" Style="display: none; z-index: 1;" Width="250px" CssClass="DivHelp">
    The percentage of your sales where you are selling your skills or expertise like accounting, legal or medical services.
</asp:Panel>
<asp:Panel ID="pnlRetail" runat="server" Style="display: none; z-index: 1;" Width="250px" CssClass="DivHelp">
    The percentage of your sales where you are selling tangible products/ items like jewels or books to your customers face to face.
</asp:Panel>
<asp:Panel ID="pnlInternetOrder" runat="server" Style="display: none; z-index: 1;" Width="250px" CssClass="DivHelp">
    The percentage of your sales where you are selling on your website and your customers are initiating payments while shopping over the internet.
</asp:Panel>
    <asp:Panel ID="pnlOther" runat="server" Style="display: none; z-index: 1;" Width="250px" CssClass="DivHelp">
    The percentage of your sales where you are selling in any other method.</asp:Panel>
                <script language="javascript" type="text/javascript">
function closepopup() {
$find('ModalPopupExtender1').hide();
}

</script>
</asp:Content>
