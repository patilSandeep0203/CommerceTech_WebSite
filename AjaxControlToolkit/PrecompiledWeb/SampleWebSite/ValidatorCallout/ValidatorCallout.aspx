<%@ page language="C#" masterpagefile="~/DefaultMaster.master" autoeventwireup="true" inherits="ValidatorCallout_ValidatorCallout, App_Web_uanxeqx_" title="ValidatorCallout Sample" theme="SampleSiteTheme" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
<asp:Content ContentPlaceHolderID="SampleContent" Runat="Server">
    <asp:ScriptManager runat="server" />
    <div class="demoarea">
        <div class="demoheading">ValidatorCallout Demonstration</div>
        <table>
            <tr>
                <td>Name:</td>
                <td><asp:TextBox runat="server" ID="NameTextBox"
                    BorderStyle="solid" BorderWidth="1px" BorderColor="#a9a9a9" /></td>
            </tr>
            <tr>
                <td>Phone Number:</td>
                <td><asp:TextBox runat="server" ID="PhoneNumberTextBox"
                    BorderStyle="solid" BorderWidth="1px" BorderColor="#a9a9a9" /></td>
            </tr>
        </table>
        <br />
        
        <asp:RequiredFieldValidator runat="server" ID="NReq"
            ControlToValidate="NameTextBox"
            Display="None"
            ErrorMessage="<b>Required Field Missing</b><br />A name is required." />
        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="NReqE"
            TargetControlID="NReq"
            HighlightCssClass="validatorCalloutHighlight" />
            
        <asp:RequiredFieldValidator runat="server" ID="PNReq"
            ControlToValidate="PhoneNumberTextBox"
            Display="None"
            ErrorMessage="<b>Required Field Missing</b><br />A phone number is required.<div style='margin-top:5px;padding:5px;border:1px solid #e9e9e9;background-color:white;'><b>Other Options:</b><br /><a href='javascript:alert(&quot;No phone number available in profile.&quot;);'>Extract from Profile</a></div>" />
        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="PNReqE"
            TargetControlID="PNReq"
            HighlightCssClass="validatorCalloutHighlight"
            Width="350px" />
            
        <asp:RegularExpressionValidator runat="server" ID="PNRegEx"
            ControlToValidate="PhoneNumberTextBox"
            Display="None"
            ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}"
            ErrorMessage="<b>Invalid Field</b><br />Please enter a phone number in the format:<br />(###) ###-####" />
        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="PNReqEx"
            TargetControlID="PNRegEx"
            HighlightCssClass="validatorCalloutHighlight" />
             
        <asp:Button ID="Button1" runat="server" Text="Submit" OnClick="Button1_OnClick" /><br /><br />
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <asp:Label id="lblMessage" runat="server" />
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <div class="demobottom"></div>
    
    <asp:Panel ID="Description_HeaderPanel" runat="server" Style="cursor: pointer;">
        <div class="heading">
            <asp:ImageButton ID="Description_ToggleImage" runat="server" ImageUrl="~/images/collapse.jpg" AlternateText="collapse" />
            ValidatorCallout Description
        </div>
    </asp:Panel>
    <asp:Panel ID="Description_ContentPanel" runat="server" Style="overflow: hidden;">
        <p>
            ValidatorCallout is an ASP.NET AJAX extender that enhances the functionality of existing ASP.NET
            validators.  To use this control, add an input field and a validator control as you normally would.
            Then add the ValidatorCallout and set its TargetControlID property to reference the validator control.
        </p>
    </asp:Panel>
    <asp:Panel ID="Properties_HeaderPanel" runat="server" Style="cursor: pointer;">
        <div class="heading">
            <asp:ImageButton ID="Properties_ToggleImage" runat="server" ImageUrl="~/images/expand.jpg" AlternateText="expand"  />
            ValidatorCallout Properties
        </div>
    </asp:Panel>
    <asp:Panel ID="Properties_ContentPanel" runat="server" Style="overflow: hidden;" Height="0px">
        <p>
            The control above is initialized with this code. The <em>italic</em> properties
            are optional:
        </p>
<pre>&lt;ajaxToolkit:ValidatorCalloutExtender 
    runat="Server"
    ID="PNReqE"
    TargetControlID="PNReq" 
    <em>Width</em>="350px"
    <em>HighlightCssClass</em>="highlight" 
    <em>WarningIconImageUrl</em>="warning.gif"
    <em>CloseImageUrl</em>="close.gif" /&gt;</pre>
        <ul>
            <li><strong>TargetControlID</strong> - The ID of the Validator to extend</li>
            <li><strong>Width</strong> - The width of the callout</li>
            <li><strong>HighlightCssClass</strong> - A CssClass to apply to the invalid field</li>
            <li><strong>WarningIconImageUrl</strong> - The path to a custom warning icon image</li>
            <li><strong>CloseImageUrl</strong> - The path to a custom close image</li>
         </ul>
    </asp:Panel>
    <asp:Panel ID="KnownIssues_HeaderPanel" runat="server" Style="cursor: pointer;">
        <div class="heading">
            <asp:ImageButton ID="KnownIssues_ToggleImage" runat="server" ImageUrl="~/images/collapse.jpg" AlternateText="collapse"  />
            ValidatorCallout Known Issues
        </div>
    </asp:Panel>
    <asp:Panel ID="KnownIssues_ContentPanel" runat="server" Style="overflow: hidden;">
        <p>
            The callouts do not currently display automatically after a server post-back and will only work for
            custom validators which utilize client-side validation. Even after a post-back the callout will
            display when the form is re-validated when a postback is attempted again.
        </p>
    </asp:Panel>
    
    <ajaxToolkit:CollapsiblePanelExtender  ID="demoCpe0"  runat="Server" 
        TargetControlID="KnownIssues_ContentPanel"
        ExpandControlID="KnownIssues_HeaderPanel"
        CollapseControlID="KnownIssues_HeaderPanel"
        Collapsed="False"
        ImageControlID="KnownIssues_ToggleImage" />
    <ajaxToolkit:CollapsiblePanelExtender ID="demoCpe1" runat="Server"
        TargetControlID="Description_ContentPanel"
        ExpandControlID="Description_HeaderPanel"
        CollapseControlID="Description_HeaderPanel"
        Collapsed="False"
        ImageControlID="Description_ToggleImage" />
    <ajaxToolkit:CollapsiblePanelExtender ID="demoCpe2" runat="Server"
        TargetControlID="Properties_ContentPanel"
        ExpandControlID="Properties_HeaderPanel"
        CollapseControlID="Properties_HeaderPanel"
        Collapsed="True"
        ImageControlID="Properties_ToggleImage" />
</asp:Content>