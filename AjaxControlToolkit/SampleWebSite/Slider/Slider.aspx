<%@ Page
    Language="C#"
    MasterPageFile="~/DefaultMaster.master"
    AutoEventWireup="true"
    CodeFile="Slider.aspx.cs"
    Inherits="Slider_Slider"
    Title="Slider Sample" 
    Theme="SampleSiteTheme" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
<asp:Content ContentPlaceHolderID="SampleContent" Runat="Server">
    <asp:ScriptManager id="ScriptManager1" runat="server" EnablePartialRendering="true" />
    <div class="demoarea">
        <div class="demoheading">Slider Demonstration</div>
        <table style="margin:auto">
            <tr>
                <td>
                    Horizontal Slider ranging from -100 to +100 with 5 discrete values (5 steps),
                    bound to a Label control. Changing the value will cause the Slider to trigger
                    an update of the UpdatePanel that displays the current date and time.
                </td>
                <td style="width:205px">
                    <table>
                        <tr>
                            <td style="width:140px">
                                <asp:TextBox ID="Slider1" runat="server" AutoPostBack="true" style="right:0px" Text="0" />
                            </td>
                            <td style="width:15px"></td>
                            <td style="width:auto">
                                <asp:Label ID="Slider1_BoundControl" runat="server" style="text-align:right" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <div style="padding-top:10px;text-align:center">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="conditional">
                                        <ContentTemplate>
                                            <asp:Label ID="lblUpdateDate" runat="server" style="font-size:80%;" Text="&nbsp;" />
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="Slider1" EventName="TextChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    Vertical Slider ranging from 0 to 100, bound to a TextBox. Clicking on the rail will cause 
                    the handle to slide with a nice animation effect.
                </td>
                <td style="height:166px;">
                    <table style="display:inline;">
                        <tr>
                            <td><asp:TextBox ID="Slider2" runat="server" /></td>
                            <td><asp:TextBox ID="Slider2_BoundControl" runat="server" Width="30" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

        <ajaxToolkit:SliderExtender ID="SliderExtender1" runat="server"
            BehaviorID="Slider1"
            TargetControlID="Slider1"
            Minimum="-100"
            Maximum="100"
            BoundControlID="Slider1_BoundControl"
            Steps="5" />
                                        
        <ajaxToolkit:SliderExtender ID="SliderExtender2" runat="server"
            BehaviorID="Slider2"
            TargetControlID="Slider2"
            BoundControlID="Slider2_BoundControl"
            Orientation="Vertical"
            EnableHandleAnimation="true" />
    </div>
    <div class="demobottom"></div>
    
    <asp:Panel ID="Description_HeaderPanel" runat="server" style="cursor: pointer;">
        <div class="heading">
            <asp:ImageButton ID="Description_ToggleImage" runat="server" ImageUrl="~/images/collapse.jpg" AlternateText="collapse" />
            Slider Description
        </div>
    </asp:Panel>
    <asp:Panel id="Description_ContentPanel" runat="server" style="overflow:hidden;">
        <p>
            The Slider extender allows to upgrade an asp:TextBox to a graphical slider that allows the user
            to choose a numeric value from a finite range. The Slider's orientation can be horizontal or 
            vertical and it can also act as a "discrete" slider, allowing only a specified number of values
            within its range.
            <br /><br />
            
            If the developer doesn't specify any parameters, the Slider is rendered with the default layout
            shown in the demonstration and its range of values goes from 0 to 100. The Slider's layout can be
            customized by providing CSS classes for the Slider's rail and handle. If handle animation is enabled,
            the handle slides to the specified point on the rail with a nice animation effect.
            <br /><br />
            
            When a value is chosen using the Slider, it is automatically persisted during full or partial
            postbacks. The developer can continue to reference the asp:TextBox to get and set the Slider's value.
            <br /><br />
            
            The Slider's value can be dynamically displayed in another asp:TextBox or an asp:Label. If a TextBox
            is used, the Slider's value can be updated through the bound TextBox.
            <br /><br />
            
            By declaring the extended TextBox as a trigger for an UpdatePanel, the Slider can fire the update 
            whenever the handle is released. By setting the RaiseChangeOnlyOnMouseUp to false, the update will be 
            fired as soon as the Slider's value changes.
        </p>
    </asp:Panel>

    <asp:Panel ID="Properties_HeaderPanel" runat="server" style="cursor: pointer;">
        <div class="heading">
            <asp:ImageButton ID="Properties_ToggleImage" runat="server" ImageUrl="~/images/expand.jpg" AlternateText="expand"  />
            Slider Properties
        </div>
    </asp:Panel>
    <asp:Panel id="Properties_ContentPanel" runat="server" style="overflow:hidden;" Height="0px">
        <p>
            The controls above are initialized with this code. The <em>italic</em> properties are optional:
        </p>
<pre>&lt;ajaxToolkit:SliderExtender ID="SliderExtender1" runat="server"
    TargetControlID="Slider1"
    <em>Minimum</em>="-100"
    <em>Maximum</em>="100"
    <em>BoundControlID</em>="Slider1_BoundControl"
    <em>Steps</em>="5" /&gt;
&lt;ajaxToolkit:SliderExtender ID="SliderExtender2" runat="server"
    TargetControlID="Slider2"
    <em>BoundControlID</em>="Slider2_BoundControl"
    <em>Orientation</em>="Vertical"
    <em>EnableHandleAnimation</em>="true" /&gt;</pre>
        <ul>
            <li><strong>Minimum</strong> - Minimum value allowed.</li>
            <li><strong>Maximum</strong> - Maximum value allowed.</li>
            <li><strong>Decimals</strong> - Number of decimal digits for the value.</li>
            <li><strong>Steps</strong> - Number of discrete values inside the slider's range.</li>
            <li><strong>Value</strong> - Current value of the slider</li>
            <li><strong>EnableHandleAnimation</strong> - Enable/disable the handle animation.</li>
            <li><strong>HandleAnimationDuration</strong> - Duration of the handle animation,
                in milliseconds.</li>
            <li><strong>RailCssClass</strong> - CSS class for the slider's rail.</li>
            <li><strong>HandleCssClass</strong> - CSS class for the slider's handle.</li>
            <li><strong>HandleImageURL</strong> - URL of the image to display as the slider's handle.</li>
            <li><strong>Length</strong> - Width/height of a horizontal/vertical slider when the
                default layout is used.</li>
            <li><strong>BoundControlID</strong> - ID of the TextBox or Label that dynamically displays
                the slider's value.</li>
            <li><strong>RaiseChangeOnlyOnMouseUp</strong> - If true, fires the change event on the
                extended TextBox only when the left mouse button is released.</li>
        </ul>
    </asp:Panel>

    <asp:Panel ID="TipsAndIssues_HeaderPanel" runat="server" style="cursor: pointer;">
        <div class="heading">
            <asp:ImageButton ID="TipsAndIssues_ToggleImage" runat="server" ImageUrl="~/images/expand.jpg" AlternateText="expand"  />
            Slider Tips
        </div>
    </asp:Panel>
    <asp:Panel id="TipsAndIssues_ContentPanel" runat="server" style="overflow:hidden;">
        <ul>
            <li>
                <strong>TextBox visibility.</strong>
                The asp:TextBox that the Slider is upgrading will be visible during the page loading,
                so it is usable if JavaScript is not enabled on the browser. Depending on your
                requirements, you can prevent the asp:TextBox from being visible
                by setting its display mode to "none".
            </li>
            <li>
                <strong>Inline - Block.</strong>
                While the TextBox is rendered as an inline element, the Slider will be 
                rendered as a block element. Consider using floats or a table when designing that 
                portion of layout. The CSS inline-block display mode is not supported by Internet 
                Explorer 6.
            </li>
            <li>
                <strong>CSS.</strong>
                When using custom CSS classes for the Slider's rail and handle, it's recommended to 
                specify positioning. The rail should have position:relative while the handle 
                should have position:absolute.<br />
                If a CSS class for the slider's rail is provided, the developer can choose to provide 
                a CSS class for the handle or the URL to an image to display as the handle.<br />
                Borders should be added to a wrapping &lt;div&gt;.
            </li>
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
    <ajaxToolkit:CollapsiblePanelExtender ID="cpeTipsAndIssues" runat="Server" 
        TargetControlID="TipsAndIssues_ContentPanel"
        ExpandControlID="TipsAndIssues_HeaderPanel"
        CollapseControlID="TipsAndIssues_HeaderPanel"
        Collapsed="True"
        ImageControlID="TipsAndIssues_ToggleImage" />
</asp:Content>