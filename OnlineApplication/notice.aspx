<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="notice.aspx.cs" Inherits="notice" Title="Commerce Technologies | Online Application" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentHolder" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table border="0" cellpadding="2" cellspacing="0" width="100%" class="DivGreen">

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
                         <td class="DivHeader" align="center" width="14%">
                           <a href="/Application/UploadDocuSignDocs.aspx" class="MenuCss">Upload Documents</a> 
                        </td>
                        <td class="DivHeader2" align="center" width="14%">
                            <span class="MenuHeader">Sign application</span>
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
        <td align="left">
        <asp:Panel ID="pnlAttach" runat="server" Width=100% Visible="false"><asp:Label ID="lbl111" runat="server" ></asp:Label>
            <span class="LabelsBody">Click on Attach Documents below to submit your application along with the following documents or fax/email them to 310-321-5410/applications@commercetech.com:</span>
                <ul class="LabelsBody" style="margin-left:50px; margin-top:0px; margin-bottom:0px;">
                <li>Voided or cancelled check pre-printed with your business name (in place of a temporary check, 
                            a bank signed letter with full account, routing number and business name will need to be provided)</li>
                <li>Business license, phone or utility bill or previous processing statement</li></ul>
            </asp:Panel>
        </td>
        </tr>
        <tr>
            <td align="center">
            <asp:Panel ID="uploadDocuSignDocs" runat="server" Width="100%" Visible="false">

                   <span class="LabelsSmall">			<asp:HyperLink ID="lnkUploadDocusign" runat="server" CssClass="One" NavigateUrl="UploadDocuSignDocs.aspx" Target="_blank" Font-Bold="True" Font-Names="Arial" Font-Size="Small">
			<br />
			Attach Documents
			</asp:HyperLink></span>
			 <cc1:PopupControlExtender ID="PopupControlExtender5" runat="server" PopupControlID="pnlAttachformat" 
                        TargetControlID="imgAttachformat" Position="Bottom" />
                    <asp:Image ID="imgAttachformat" runat="server" ImageUrl="~/Images/help.gif" 
                        Style="cursor: pointer"/><br/>

                </asp:Panel>
               
                <asp:Panel ID="pnlAttachformat" runat="server" BackColor="Ivory" BorderColor="silver"
                        BorderWidth="1px" Width="20%" Visible="false">
                        <asp:Label ID="lblAttachformat" runat="server" CssClass="LabelsBodySmall"
                            Text="Supported file format: pdf, jpg, doc, docx, txt, tif, png."></asp:Label></asp:Panel><br/>
            </td>
        </tr>


         <tr>
            <td align="left">
            
            <asp:Panel ID="pnlDigital" runat="server" Width=100% Visible="False">
            <span class="LabelsBody"> Click on the "DocuSign Application" below to complete the application process electronically.</span>
                <ul class="LabelsBody" style="margin-left:50px; margin-top:0px; margin-bottom:0px;">
                <li>You might be required to complete the 'ID Check' process by answering a few questions.</li> 
                <li>Sign and initial where required.</li>
                <li>If there are two principals, principal #1 will click on the Digital Signature and sign first and principal #2 will receive an email notification to review and sign the document. The application is considered complete only when both principals have signed the document and clicked on complete within DocuSign.</li>
                </ul>
            </asp:Panel>
            </td>
        </tr>
        <tr height="20px"></tr>
        <tr>
            <td align="center">                


                <!--<b><span class="LabelsBody">Your Application has been saved with Application ID #: </span>
                    <asp:Label ID="lblAppId" runat="server"></asp:Label><br />
                </b><span class="LabelsBody">(Please retain this number for future reference.)</span>-->
                <asp:Panel ID="pnlDigitalSig" runat="server" Width="100%">
                    <asp:Button CssClass="Button" ID="btnDigitalSignature" runat="server" Text="DocuSign Application" OnClick="btnDigitalSignature_ClickNew"/>  
                    <cc1:PopupControlExtender ID="PopupControlExtender4" runat="server" PopupControlID="pnlDigSignNote" 
                        TargetControlID="ImgDigitalSig" Position="Bottom" />
                    <asp:Image ID="ImgDigitalSig" runat="server" ImageUrl="~/Images/help.gif" 
                        Style="cursor: pointer"/><br/>
                </asp:Panel>
                <!--<asp:Panel ID="pnlGetEnvelopeStatus" runat="server" Width="100%">

                    <asp:Button CssClass="Button" ID="btnGetEnvelopeStatus" runat="server" Text="Get Envelope Status" OnClick="btnGetEnvelopeInfo_Click"/>  
                   <br />

                </asp:Panel>-->
                <asp:Panel ID="pnlDigSignNote" runat="server" BackColor="Ivory" BorderColor="silver"
                        BorderWidth="1px" Width="20%">
                        <asp:Label ID="lblDigSignNote" runat="server" CssClass="LabelsBodySmall"
                            Text="NOTE: Digital Signature works with the Microsoft Internet Explorer browser (Version 7.0 
                            or above), Google Chrome (Version 22 or above), Mozilla Firefox (Version 2.0 or above) and Safari (Version 4.0 or above). "></asp:Label></asp:Panel>
                  
                  
                   <asp:Panel ID="pnlSageDnCDocuSign" runat="server" Visible="False"  Width="80%">
                        <div style="text-align: center">
                            <strong>
                                <asp:HyperLink ID="lnkSageDnCDocuSign" Font-Names="Arial" CssClass="Link" runat="server" NavigateUrl="Sage Merchant Agreement.pdf" Target="_blank">Terms and Conditions</asp:HyperLink>
                            </strong>
                        </div>
                    </asp:Panel>          
                                           <!-- <asp:Panel ID="pnlLeaseDigSign" runat="server" Width="100%">

                   <asp:Button CssClass="Button" ID="btnLeaseDigSign" runat="server" Text="Sign Lease Application Digitally" OnClick="btnLeaseDigitalSignature_Click"/>  
                    <br />

                </asp:Panel>-->
                
                 
                &nbsp;</td>
        </tr>

                <tr>
            <td align="left">
            <br />
            <asp:Panel ID="pnlMA" runat="server" Width="100%" Visible="False">
            <span class="LabelsBody">Alternatively, click on the "PDF Application" button below and follow these instructions to complete the application process manually:</span>
                <ul class="LabelsBody" style="margin-left:50px; margin-top:0px; margin-bottom:0px;">
                <li>Print the PDF application</li>
                <li>Make any corrections and fill out the blank areas</li>
                <li>Initial, sign and date where highlighted</li>
                <li>Attach the signed application from the link below</li>
               </ul>
            </asp:Panel>
            </td>
        </tr>
        <tr height="20px"></tr>
        <tr>
            <td align="center">                

                <asp:Panel ID="pnlCreatePDF" runat="server" Width="100%">
                        

                    <asp:Button CssClass="Button" ID="btnCreatePDF" runat="server" Text="PDF Application" OnClientClick="aspnetForm.target ='_blank';" OnClick="btnCreatePDF_Click"/> 
                    
                    <cc1:PopupControlExtender ID="PopupControlExtender3" runat="server" PopupControlID="pnlPDFNote" 
                        TargetControlID="imgPDFHelp1" Position="Bottom" />
                    <asp:Image ID="imgPDFHelp1" runat="server" ImageUrl="~/Images/help.gif" 
                        Style="cursor: pointer"/>
                    <asp:Panel ID="pnlSagePDF" runat="server" Visible="False"  Width="80%">
                        <div style="text-align: center">
                            <strong><span class="LabelsBodySmall"><!--Click on each of the following to create Sage PDFs
                                and complete and fax them back at (310) 321-5410 --></span>
                                <!--<asp:LinkButton ID="btnSageApp" runat="server" OnClick="btnSageApp_Click" CssClass="Link" Font-Names="Arial" Font-Size="10pt" CausesValidation="False">Sage Application</asp:LinkButton><br />-->
                                <!--<asp:LinkButton ID="btnSageMOTO" runat="server" OnClick="btnSageMOTO_Click" CssClass="Link" Font-Names="Arial" Font-Size="10pt" CausesValidation="False">Sage MOTO Application</asp:LinkButton><br />-->
                                <asp:HyperLink ID="lnkSageAgreement" Font-Names="Arial" CssClass="Link" runat="server" NavigateUrl="http://www.sage.com/us/sage-payment-solutions/terms" Target="_blank">Terms and Conditions</asp:HyperLink><br/>
                               <asp:LinkButton ID="lnkAmendment1" Visible="False" Font-Names="Arial"  CssClass="Link" runat="server" color= "#026d6f" OnClick="lnkAmendment_Click" Target="_blank">Amendment </asp:LinkButton>
                            </strong>
                        </div>
                                        <asp:Label ID="lblComplete" runat="server" Text="Your Merchant application is now complete"
                    Visible="False" CssClass="LabelsBody" ></asp:Label>
                <asp:Panel ID="pnlGatewayOnly" runat="server" Width="100%" Visible="False"><asp:Label ID="lblGatewayOnly" runat="server" CssClass="LabelsBody"
                    Text="You will soon receive a startup e-mail in MS Word or PDF format containing the information neccessary to start using your account. If 
                    you have any questions, contact your Sales Agent or click on the Contact Us link at the top of the page.">
                    </asp:Label>
                    <span class="LabelsBody">If you do not have a existing merchant account, <asp:HyperLink ID="lnkMerchantSignup" runat="server" 
                        NavigateUrl="UpdateAcct.aspx" CssClass="Link"><b>click here</b></asp:HyperLink> to sign up for a Merchant Account.</span>
                </asp:Panel>
                    <asp:Label ID="lblCompleteMsg" runat="server" CssClass="LabelsBody" Text="You can click on the Create PDF button to generate the PDF for your application. Please print the document, make any 
                        corrections, sign and date where highlighted and fax with the voided check to (310) 321-5410.  If you have any questions, contact your Sales Agent or click on the 
                        Contact Us link at the top of the page." Visible="False"></asp:Label>
                    </asp:Panel>
                    <asp:HyperLink ID="lnkUploadPDF" runat="server" CssClass="One" NavigateUrl="UploadPDF.aspx" Target="_blank" Font-Bold="True" Font-Names="Arial" Font-Size="Small">
			<br />
			Attach Signed Application
			</asp:HyperLink>
                    <asp:Panel ID="pnlChasePDF" runat="server" Visible="False" BackColor="Ivory" Width="80%"
                                        BorderWidth="1px" BorderColor="Red">
                        <div style="text-align: center">
                            <strong><span class="LabelsBodySmall">Click on each of the following to create Chase PDFs
                                and complete and fax them back at (310) 321-5410 </span>:<br />
                                <asp:LinkButton ID="btnChaseMPA" runat="server" CssClass="Link" Font-Names="Arial"
                                    Font-Size="10pt" OnClick="btnChaseMPA_Click" CausesValidation="False">CardConnect Merchant Application and Agreement</asp:LinkButton><br />    
                                <asp:LinkButton ID="btnChaseFS3Tier" runat="server" CssClass="Link" Font-Names="Arial"
                                    Font-Size="10pt" OnClick="btnChaseFS3Tier_Click" Visible="false" CausesValidation="False">Chase Fee Schedule 3 tier</asp:LinkButton><br />    
                                <asp:LinkButton ID="btnChaseFSInterchangePlus" runat="server" CssClass="Link" Font-Names="Arial"
                                    Font-Size="10pt" OnClick="btnChaseFSInterchangePlus_Click" Visible="false" CausesValidation="False">Chase Fee Schedule Interchange Plus</asp:LinkButton><br />    
                            </strong>
                        </div>
                    </asp:Panel>
                    <asp:Label ID="lblDownload" runat="server" Font-Bold="True" Visible="False"></asp:Label>
                    <asp:Panel ID="pnlPDFNote" runat="server" BackColor="Ivory" BorderColor="silver"
                        BorderWidth="1px" Width="20%">
                        <asp:Label ID="lblPDFNote" runat="server" CssClass="LabelsBodySmall"
                            Text="NOTE: PDF Creation works only with the Microsoft Internet Explorer browser (Version 7.0 
                            or above), Google Chrome (Version 22 or above), Mozilla Firefox (Version 2.0 or above) and Safari (Version 4.0 or above). For viewing PDFs, please install Abode 
                            Acrobat Reader Version 8.0 or higher. Download the latest version here: "></asp:Label><asp:HyperLink
                                ID="lnkAdobe" runat="server" CssClass="Link" NavigateUrl="http://www.adobe.com"
                                Target="_blank">www.adobe.com</asp:HyperLink></asp:Panel>
                </asp:Panel>
                
                &nbsp;</td>
        </tr>
       

        <tr>
            <td align="center">

                <asp:Panel ID="pnlThankyou" runat="server"><b><span class="LabelsBody">Thank you for applying with Commerce Technologies!</span></b></asp:Panel>
            </td>
        </tr>
        <tr height="20px"></tr>
    </table>
</asp:Content>
