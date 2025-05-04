<%@ Page
    Language="C#"
    MasterPageFile="~/DefaultMaster.master"
    AutoEventWireup="true"
    Inherits="CommonPage"
    Title="MaskedEdit Sample"
    Theme="SampleSiteTheme" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
<asp:Content ContentPlaceHolderID="SampleContent" runat="Server">
    <asp:ScriptManager ID="SM1" runat="server" EnablePartialRendering="True" />
    <div class="demoarea">
        <div class="demoheading">MaskedEdit Demonstration</div>
        
        Enter a Date in this format: 99/99/9999 (culture sensitive)<br />
        <asp:TextBox ID="TextBox1" runat="server" Width="130px" ValidationGroup="Demo1" MaxLength="1" style="text-align:justify" /><br />
        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
            TargetControlID="TextBox1"
            Mask="99/99/9999"
            MessageValidatorTip="true"
            OnFocusCssClass="MaskedEditFocus"
            OnInvalidCssClass="MaskedEditError"
            MaskType="Date"
            DisplayMoney="Left"
            AcceptNegative="Left" />
        <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server"
            ControlExtender="MaskedEditExtender1"
            ControlToValidate="TextBox1"
            IsValidEmpty="False"
            EmptyValueMessage="Date is required"
            InvalidValueMessage="Date is invalid"
            ValidationGroup="Demo1"
            Display="Dynamic"
            TooltipMessage="Input a Date" />
        <br /><br />
        
        Enter a Number in this format: 99,999.99.<br />
        <asp:TextBox ID="TextBox2" runat="server" Width="130px" Height="16px" ValidationGroup="Demo1" /><br />
        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server"
            TargetControlID="TextBox2"
            Mask="99,999.99"
            MessageValidatorTip="true"
            OnFocusCssClass="MaskedEditFocus"
            OnInvalidCssClass="MaskedEditError"
            MaskType="Number"
            InputDirection="RightToLeft"
            AcceptNegative="Left"
            DisplayMoney="Left"/>
        <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator2" runat="server"
            ControlExtender="MaskedEditExtender2"
            ControlToValidate="TextBox2"
            IsValidEmpty="False"
            MaximumValue="12000"
            EmptyValueMessage="Number is required"
            InvalidValueMessage="Number is invalid"
            MaximumValueMessage="Number &gt; 12.000"
            MinimumValueMessage="Number &lt; -100"
            MinimumValue="-100"
            ValidationGroup="Demo1"
            Display="Dynamic"
            TooltipMessage="Input a number : -100 up to 12.000" />
        <br /><br />
        
        Enter Time in this format: 99:99:99 (type 'A' or 'P' to switch between AM/PM).<br />
        <asp:TextBox ID="TextBox3" runat="server" Width="130px" Height="16px" ValidationGroup="Demo1" /><br />
        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server"
            TargetControlID="TextBox3" 
            Mask="99:99:99"
            MessageValidatorTip="true"
            OnFocusCssClass="MaskedEditFocus"
            OnInvalidCssClass="MaskedEditError"
            MaskType="Time"
            AcceptAMPM="True"
            CultureName="en-US" />
        <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator3" runat="server"
            ControlExtender="MaskedEditExtender3"
            ControlToValidate="TextBox3"
            IsValidEmpty="False"
            EmptyValueMessage="Time is required"
            InvalidValueMessage="Time is invalid"
            ValidationGroup="Demo1"
            Display="Dynamic"
            TooltipMessage="Input a time" />
        <br />
        
        <asp:ValidationSummary runat="Server" ValidationGroup="Demo1" ID="validationSummary" ShowSummary="true" />
    </div>
    <div class="demobottom"></div>
       
    <asp:Panel ID="description_HeaderPanel" runat="server" Style="cursor: pointer;">
        <div class="heading">
            <asp:ImageButton ID="Description_ToggleImage" runat="server" ImageUrl="~/images/collapse.jpg" AlternateText="collapse"  />
            MaskedEdit Description</div>
    </asp:Panel>
    <asp:Panel ID="description_ContentPanel" runat="server" Style="overflow: hidden;">
        <p>
            The MaskedEdit control is an ASP.NET AJAX extender that can be attached to a TextBox control. 
            When using MaskedEdit the input is masked and the value is validated on the client according to
            the data type chosen.  To achieve the validation, the extender is hooked up to a MaskedEditValidator
            that verifies the input and feeds it to a set of ASP.NET validators.
        </p>
    </asp:Panel>
    
    <asp:Panel ID="properties_HeaderPanel" runat="server" Style="cursor: pointer;">
        <div class="heading">
            <asp:ImageButton ID="Properties_ToggleImage" runat="server" ImageUrl="~/images/expand.jpg" AlternateText="expand"  />
            MaskedEdit Properties
        </div>
    </asp:Panel>
    <asp:Panel ID="properties_ContentPanel" runat="server" Style="overflow: hidden;" Height="0px">
        <p>
            The control above is initialized with this code. The <em>italic</em>
            properties are optional.
        </p>
<pre>&lt;ajaxToolkit:MaskedEditExtender
    TargetControlID="TextBox2" 
    Mask="99,999.99"
    <em>MessageValidatorTip</em>="true" 
    <em>OnFocusCssClass</em>="MaskedEditFocus" 
    <em>OnInvalidCssClass</em>="MaskedEditError"
    <em>MaskType</em>="Number" 
    <em>InputDirection</em>="RightToLeft" 
    <em>AcceptNegative</em>="Left" 
    <em>DisplayMoney</em>="Left"/&gt;</pre>
        <ul>
            <li><strong>Mask Characters</strong>
                <br />
                9 - Only a numeric character<br />
                L - Only a letter<br />
                $ - Only a letter or a space<br />
                C - Only a custom character (case sensitive)<br />
                A - Only a letter or a custom character<br />
                N - Only a numeric or custom character<br />
                ? - Any character</li>
            <li><strong>Mask Delimiters<br />
            </strong>
                / - Date separator<br />
                : - Time separator<br />
                . - Decimal separator<br />
                , - <span style="font-size: 9pt; font-family: Tahoma">Thousand</span> separator<br />
                \ - Escape character<br />
                { - Initial delimiter for repetition of masks<br />
                } - Final delimiter for repetition of masks </li>
            <li><strong>Examples:</strong><br />
                9999999 - Seven numeric characters (equivalent to 9{7})<br />
                99\/99 - Four numeric characters separated in the middle by a "/"</li>
            <li><strong>AcceptAMPM</strong> - Whether an AM/PM symbol is displayed.<br />
                You can change between AM/PM by pressing 'A' or 'P'.<br />
                The default value is false.</li>
            <li><strong>AcceptNegative</strong> - Accepts the negative sign (-).<br />
                The default value is None.<br />
                None - do not show the negative sign<br />
                Left - show the negative sign on the left of the mask<br />
                Right - show the negative sign on the right of the mask</li>
            <li><strong>AutoComplete</strong> - Whether to automatically complete any empty mask characters
                not filled in by the user.  AutoComplete can be used when ValidType is Date, Time, or Number.<br />
                The default value is true.<br />
                If ValidType="Number" or ValidType="Time", empty mask characters will be filled with zeros.<br />
                If ValidType="Date", empty mask characters will be filled with the current date if they are all empty;
                otherwise remaining characters will filled in with zeros.</li>
            <li><strong>AutoCompleteValue</strong> - Default value to be used when AutoComplete is enabled.
                It is only used when ValidType="Date" or ValidType="Time".<br />
                Overwrite Current Date or Time.</li>
            <li><strong>Century</strong> - Default century used when a date mask only has two digits for the year
                (so setting Century="2000" will interpret 01/01/07 as January 1st, 2007).</li>
            <li><strong>DisplayMoney</strong> - How the currency symbol is displayed. The default value is None.<br />
                None - do not show the currency symbol<br />
                Left - show the currency symbol on the left of the mask<br />
                Right - show the currency symbol on the right of the mask</li>
            <li><strong>InputDirection</strong> - Text input direction<br />
                LeftToRight - Left to Right (Default)<br />
                RightToLeft - Right to left. </li>
            <li><strong>MaskType</strong> - Type of validation to perform:<br />
                None - No validation (default)<br />
                Number - Number validation<br />
                Date - Date validation<br />
                Time - Time validation</li>
            <li><strong>MessageValidatorTip</strong> - Message to display when editing in textbox.</li>
            <li><strong>OnFocusCssClass</strong> - Name of CSS class to use when the control receives focus</li>
            <li><strong>OnFocusCssNegative</strong> - Name of CSS class to use when the control with a negative value gets focus</li>
            <li><strong>OnBlurCssNegative</strong> - Name of CSS class to use when the control with a negative value loses focus</li>
            <li><strong>OnInvalidCssClass</strong> - Name of CSS class to use when the control is not valid</li>
            <li><strong>PromptChararacter</strong> - Prompt character to indicate masked characters not yet entered.
                The default value is <b>"_"</b>.</li>
        </ul>
    </asp:Panel>
    
    <asp:Panel ID="description_HeaderPanel1" runat="server" Style="cursor: pointer;">
        <div class="heading">
            <asp:ImageButton ID="Description_ToggleImage1" runat="server" ImageUrl="~/images/collapse.jpg" AlternateText="collapse"  />
            MaskedEdit Validator Description</div>
    </asp:Panel>
    <asp:Panel ID="description_ContentPanel1" runat="server" Style="overflow: hidden;">
        MaskedEditValidator is a custom validator attached to the extender and the textbox.   It verifies
        that the data is coherent on different levels (i.e., the date provided is a valid date, or the currency 
        is in the right format).  Once the MaskedEditValidator is attached to a validation group, you can
        perform server and client side validation on the input and display errors in your desired format.
        It extends from BaseValidator and performs five types of  validation: Required, RegularExpression,
        Compare or Length, DataType, and Custom.
    </asp:Panel>
    
    <asp:Panel ID="properties_HeaderPanel1" runat="server" Style="cursor: pointer;">
        <div class="heading">
            <asp:ImageButton ID="Properties_ToggleImage1" runat="server" ImageUrl="~/images/expand.jpg" AlternateText="expand"  />
            MaskedEditValidator Properties
        </div>
    </asp:Panel>
    <asp:Panel ID="properties_ContentPanel1" runat="server" Style="overflow: hidden;" Height="0px">
        The control above is initialized with this code. The <em>italic</em> properties are optional.
<pre>&lt;ajaxToolkit:MaskedEditValidator
    ControlExtender="MaskedEditExtender2"
    <em>ControlToValidate</em>="TextBox2" 
    <em>IsValidEmpty</em>="False" 
    <em>MaximumValue</em>="12000" 
    <em>EmptyValueMessage</em>="Number is required"
    <em>InvalidValueMessage</em>="Number is invalid" 
    <em>MaximumValueMessage</em>="Number > 12.000"
    <em>MinimumValueMessage</em>="Number < -100" 
    <em>MinimumValue</em>="-100" ValidationGroup="Demo1"
    <em>Display</em>="Dynamic" 
    <em>TooltipMessage</em>="Input a number: -100 up to 12.000"/&gt;</pre>
        <ul>
            <li><strong>ControlToValidate</strong> - ID of the textbox to validate.</li>
            <li><strong>ControlExtender</strong> - ID of the MaskedEditExtender attached to the textbox.</li>
            <li><strong>ClientValidationFunction</strong> - Client script used for custom validation</li>
            <li><strong>InitialValue </strong> - Initial value of the field to be validated.
                The default value is empty.</li>
            <li><strong>AcceptAMPM</strong> - Whether or not AM/PM is accepted on times.<br />
                The default value is false.</li>
            <li><strong>IsValidEmpty</strong> - Whether the textbox can be empty.  The default value
                is true.  If false, the RequiredValidator is executed.</li>
            <li><strong>MaximumValue</strong> - The maximum value of the input.  If set, CompareValidator is executed</li>
            <li><strong>MinimumValue</strong> - The minimum value of the input.  If set, CompareValidator is executed</li>
            <li><strong>MessageEmpty </strong> - Text message when RequiredValidator is executed</li>
            <li><strong>MaximumValueMessage </strong> - Text message displayed when MaximumValue CompareValidator fails</li>
            <li><strong>MinimumValueMessage </strong> - Text message displayed when MinimumValue CompareValidator fails</li>
            <li><strong>InvalidValueMessage </strong> - Text message displayed when DataTypeValidator fails.  <em>The DatatypeValidator gets
                its type from the ControlExtender and is always executed.</em></li>
            <li><strong>TooltipMessage </strong> - Message displayed when the textbox has focus and an empty value.</li>
            <li><strong>ValidationExpression</strong> - Regular expression used to validate the input.</li>
        </ul>
    </asp:Panel>

    <ajaxToolkit:CollapsiblePanelExtender ID="cpeDescription" runat="Server"
        TargetControlID="description_ContentPanel"
        ExpandControlID="description_HeaderPanel"
        CollapseControlID="description_HeaderPanel"
        Collapsed="False"
        ImageControlID="description_ToggleImage" />
    <ajaxToolkit:CollapsiblePanelExtender ID="cpeProperties" runat="Server"
        TargetControlID="properties_ContentPanel"
        ExpandControlID="properties_HeaderPanel"
        CollapseControlID="properties_HeaderPanel"
        Collapsed="True"
        ImageControlID="properties_ToggleImage" />
    <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="Server"
        TargetControlID="description_ContentPanel1"
        ExpandControlID="description_HeaderPanel1"
        CollapseControlID="description_HeaderPanel1"
        Collapsed="False"
        ImageControlID="description_ToggleImage1" />
    <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender2" runat="Server"
        TargetControlID="properties_ContentPanel1"
        ExpandControlID="properties_HeaderPanel1"
        CollapseControlID="properties_HeaderPanel1"
        Collapsed="True" 
        ImageControlID="properties_ToggleImage1" />
</asp:Content>
