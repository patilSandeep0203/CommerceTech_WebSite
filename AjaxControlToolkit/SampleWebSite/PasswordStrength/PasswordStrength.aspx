<%@ Page
    Language="C#"
    MasterPageFile="~/DefaultMaster.master"
    AutoEventWireup="true"
    Inherits="CommonPage"
    Title="PasswordStrength Sample" 
    Theme="SampleSiteTheme" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
<asp:Content ContentPlaceHolderID="SampleContent" Runat="Server">
    <asp:ScriptManager runat="server" />
    <div class="demoarea">
        <div class="demoheading">PasswordStrength Demonstration</div>

        Simple Complexity, Text Indicator<br />
        <asp:TextBox ID="TextBox1" Width="150" runat="server" autocomplete="off" /><br />
        <asp:Label ID="TextBox1_HelpLabel" runat="server" /><br /><br />
        <ajaxToolkit:PasswordStrength ID="PasswordStrength1" runat="server" TargetControlID="TextBox1"
            DisplayPosition="RightSide"
            StrengthIndicatorType="Text"
            PreferredPasswordLength="10"
            PrefixText="Strength:"
            HelpStatusLabelID="TextBox1_HelpLabel"
            TextCssClass="TextIndicator_TextBox1"
            TextStrengthDescriptions="Very Poor;Weak;Average;Strong;Excellent"
            MinimumNumericCharacters="0"
            MinimumSymbolCharacters="0"
            RequiresUpperAndLowerCaseCharacters="false" />
        
        Average Complexity, Bar Indicator<br />
        <asp:TextBox ID="TextBox2" Width="150" TextMode="Password" runat="server" autocomplete="off" /><br />
        <asp:Label ID="TextBox2_HelpLabel" runat="server" /><br /><br />
        <ajaxToolkit:PasswordStrength ID="PasswordStrength2" runat="server" TargetControlID="TextBox2"
            DisplayPosition="RightSide" 
            StrengthIndicatorType="BarIndicator"
            PreferredPasswordLength="15"
            HelpStatusLabelID="TextBox2_HelpLabel"
            BarIndicatorCssClass="BarIndicator_TextBox2"
            BarBorderCssClass="BarBorder_TextBox2"
            MinimumNumericCharacters="1"
            MinimumSymbolCharacters="1"
            TextStrengthDescriptions="Very Poor;Weak;Average;Strong;Excellent"
            RequiresUpperAndLowerCaseCharacters="true" />
        
        High Complexity, Text Indicator, Help Indicator<br /><br />
        <asp:TextBox ID="TextBox3" Width="150" runat="server" autocomplete="off" />
        <ajaxToolkit:PasswordStrength ID="PasswordStrength3" runat="server" TargetControlID="TextBox3"
            DisplayPosition="BelowLeft"
            StrengthIndicatorType="Text"
            PreferredPasswordLength="20"
            PrefixText="Meets Policy? "
            TextCssClass="TextIndicator_TextBox3"
            MinimumNumericCharacters="2"
            MinimumSymbolCharacters="2"
            RequiresUpperAndLowerCaseCharacters="true"
            TextStrengthDescriptions="Not at all;Very Low compliance;Low Compliance;Average Compliance;Good Compliance;Very High Compliance;Yes"
            HelpHandleCssClass="TextIndicator_TextBox3_Handle"
            HelpHandlePosition="LeftSide" />
    </div>
    <div class="demobottom"></div>
    
    <asp:Panel ID="Description_HeaderPanel" runat="server" style="cursor: pointer;">
        <div class="heading">
            <asp:ImageButton ID="Description_ToggleImage" runat="server" ImageUrl="~/images/collapse.jpg" AlternateText="collapse" />
            PasswordStrength Description
        </div>
    </asp:Panel>
    <asp:Panel id="Description_ContentPanel" runat="server" style="overflow:hidden;">
        <p>
            PasswordStrength is an ASP.NET AJAX extender that can be attached to an ASP.NET TextBox control
            used for the entry of passwords. The PasswordStrength extender shows the strength of the password
            in the TextBox and updates itself as the user types the password. The indicator can display the
            strength of the password as a text message or with a progress bar indicator. The styling and
            position of both types of indicators is configurable. The required strength of the password is
            also configurable, allowing the page author to tailor the password strength requirements to their
            needs. The text messages that describe the current strength of the password can also be
            configured. Additionally, a help indicator can be used to provide explicit instructions about
            what changes are required to achieve a strong password.  The indicator is displayed when the user
            begins typing into the TextBox and is hidden from view once the TextBox loses focus.
        </p>
    </asp:Panel>

    <asp:Panel ID="Properties_HeaderPanel" runat="server" style="cursor: pointer;">
        <div class="heading">
            <asp:ImageButton ID="Properties_ToggleImage" runat="server" ImageUrl="~/images/expand.jpg" AlternateText="expand" />
            PasswordStrength Properties
        </div>
    </asp:Panel>
    <asp:Panel id="Properties_ContentPanel" runat="server" style="overflow:hidden;" Height="0px">
        <p>
            The control above is initialized with this code. The <em>italic</em> properties are optional:
        </p>
<pre>&lt;ajaxToolkit:PasswordStrength ID="PS" runat="server"
    TargetControlID="TextBox1"
    <em>DisplayPosition="RightSide"</em>
    <em>StrengthIndicatorType="Text"</em>
    <em>PreferredPasswordLength="10"</em>
    <em>PrefixText="Strength:"</em>
    <em>TextCssClass="TextIndicator_TextBox1"</em>
    <em>MinimumNumericCharacters="0"</em>
    <em>MinimumSymbolCharacters="0"</em>
    <em>RequiresUpperAndLowerCaseCharacters="false"</em>
    <em>TextStrengthDescriptions="Very Poor;Weak;Average;Strong;Excellent"</em>
    <em>CalculationWeightings="50;15;15;20"</em> /&gt;</pre>
        <ul>
            <li><strong>TargetControlID</strong> - ID of the TextBox to attach to</li>
            <li><strong>DisplayPosition</strong> - Positioning of the strength indicator relative to the
                target control</li>
            <li><strong>StrengthIndicatorType</strong> - Strength indicator type (Text or BarIndicator)</li>
            <li><strong>PreferredPasswordLength</strong> - Preferred length of the password</li>
            <li><strong>PrefixText</strong> - Text prefixed to the display text when StrengthIndicatorType=Text</li>
            <li><strong>TextCssClass</strong> - CSS class applied to the text display when StrengthIndicatorType=Text</li>
            <li><strong>MinimumNumericCharacters</strong> - Minimum number of numeric characters</li>
            <li><strong>MinimumSymbolCharacters</strong> - Minimum number of symbol characters (ex: $ ^ *)</li>
            <li><strong>RequiresUpperAndLowerCaseCharacters</strong> - Specifies whether mixed case characters
                are required</li>
            <li><strong>TextStrengthDescriptions</strong> - List of semi-colon separated descriptions used when
                StrengthIndicatorType=Text (Minimum of 2, maximum of 10; order is weakest to strongest)</li>
            <li><strong>CalculationWeightings</strong> - List of semi-colon separated numeric values used to
                determine the weighting of a strength characteristic. There must be 4 values specified which must
                total 100. The default weighting values are defined as 50;15;15;20. This corresponds to password
                length is 50% of the strength calculation, Numeric criteria is 15% of strength calculation,
                casing criteria is 15% of calculation, and symbol criteria is 20% of calculation. So the format is
                'A;B;C;D' where A = length weighting, B = numeric weighting, C = casing weighting,
                D = symbol weighting.</li>
            <li><strong>BarBorderCssClass</strong> - CSS class applied to the bar indicator's border when
                StrengthIndicatorType=BarIndicator</li>
            <li><strong>BarIndicatorCssClass</strong> - CSS class applied to the bar indicator's inner
                bar when StrengthIndicatorType=BarIndicator</li>
            <li><strong>HelpStatusLabelID</strong> - Control ID of the label used to display help text</li>
            <li><strong>HelpHandleCssClass</strong> - CSS class applied to the help element used to display a
                dialog box describing the password requirements</li>
            <li><strong>HelpHandlePosition</strong> - Positioning of the help handle element relative to the
                target control</li>
        </ul>
    </asp:Panel>

    <ajaxToolkit:CollapsiblePanelExtender ID="cpeDescription" runat="Server" 
        TargetControlID="Description_ContentPanel"
        ExpandControlID="Description_HeaderPanel"
        CollapseControlID="Description_HeaderPanel"
        Collapsed="False"
        ImageControlID="Description_ToggleImage" />
    <ajaxToolkit:CollapsiblePanelExtender ID="cpeProperties" runat="Server" 
        TargetControlID="Properties_ContentPanel"
        ExpandControlID="Properties_HeaderPanel"
        CollapseControlID="Properties_HeaderPanel"
        Collapsed="True"
        ImageControlID="Properties_ToggleImage" />
</asp:Content>