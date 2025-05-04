<%@ Page
    Language="C#"
    MasterPageFile="~/DefaultMaster.master"
    AutoEventWireup="true"
    Inherits="CommonPage"
    Title="Archived Toolkit Releases Notes" %>
<asp:Content ContentPlaceHolderID="SampleContent" Runat="Server">

    <p><strong><u>Release Notes</u></strong></p>
    <br />
    <p><u><strong>Version 1.0.10201.0</strong></u> - <em>Requires ASP.NET AJAX version 1.0</em></p>
    <p>This is an update release for the Toolkit. We have fixed some issues that were raised since the 10123 release.</p><br />
    <p><strong>General Notes:</strong></p>
    <p></p>
    <ul class="releaseList">
        <li>Fixed AutoComplete and TabPanel dispose issues exposed by UpdatePanels</li>
        <li>Fixed TextBoxWatermark issue where the textbox retained focus even though the watermark showed up</li>
        <li>Fixed Calendar localization issues</li>
        <li>Simplified ModalPopup drag behavior</li>
    </ul>
    <p></p><br />
    <p>Breaking Changes:</p>
    <p></p>
    <ul class="releaseList">
        <li>No breaking changes</li></ul>
    <p></p>
    <p>Known Issues:</p>
    <p></p>
    <ul class="releaseList">
        <li>No new issues</li></ul>

    <br />
    <br />    

    <p><u><strong>Version 1.0.10123.0</strong></u> - <em>Requires ASP.NET AJAX version 1.0</em></p>
    <p>This is a major release for the Toolkit. We have ported over all fixes in the development branch 
    and have brand new set of controls, feature changes and script base classes.</p><br />
    <p><strong>New Controls: </strong></p>
    <p></p>
    <ul class="releaseList">
        <li><strong>AutoComplete</strong>: The much-requested control that provides suggestions to complete input typed in a textbox</li>
        <li><strong>Calendar</strong>: Client-side calendar control</li>
        <li><strong>MaskedEdit</strong>: Control to enforce input entered into a text box is in a specific format complying with specific types using client-side validation</li>
        <li><strong>TabContainer</strong>: Tab strip control to organize page content</li>
    </ul><br />
    <p><strong>General Notes:</strong></p>
    <p></p>
    <ul class="releaseList">
        <li><strong>Updated the Toolkit framework, all controls, all samples, and all documentation for the new 
        <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="http://ajax.asp.net/">ASP.NET AJAX v1.0</asp:HyperLink></strong></li>
        <li><strong>Bug Fixes:</strong> Over 75 issue fixes in the Toolkit development branch are a part of this release making it those most stable and feature rich one so far</li>        
        <li><strong>ModalPopup changes:</strong> Added absolute positioning, drag/drop ability, async post-back support</li>
        <li><strong>RounderCorners:</strong> Added ability to specify which corners to round</li>
        <li><strong>ExtenderBaseControl/Script changes:</strong> Revised ExtenderControlBase architecture to make it easier to author new Toolkit Controls</li>
    </ul>
    <p></p><br />
    <p>Breaking Changes:</p>
    <p></p>
    <ul class="releaseList">
        <li>No breaking changes</li></ul>
    <p></p>
    <p>Known Issues:</p>
    <p></p>
    <ul class="releaseList">
        <li>No new issues</li></ul>

    <br />
    <br />
    
    <p><u><strong>Version 1.0.61214.0</strong></u> - <em>Requires ASP.NET AJAX v1.0 RC</em></p>
    <p></p>
    <p>General Notes:</p>
    <p></p>
    <ul class="releaseList">
        <li><strong>Updated the Toolkit framework, all controls, all samples, and all documentation for the new <asp:HyperLink ID="HyperLink12" runat="server" NavigateUrl="http://ajax.asp.net/">ASP.NET AJAX v1.0 RC</asp:HyperLink></strong></li>
        <li>Added missing Accordion property attributes</li>
        <li>Fixed Slider initialize, reposition, and default value code</li>
        <li>Enhanced ModalPopup tab-prevention for background elements</li>
    </ul>
    <p></p>
    <p>Breaking Changes</p>
    <p></p>
    <ul class="releaseList">
        <li>No breaking changes</li></ul>
    <p></p>
    <p>Known Issues</p>
    <p></p>
    <ul class="releaseList">
        <li>No new issues</li></ul>
    <br />
    <br />

    <p><u><strong>Version 1.0.61121.0</strong></u></p>
    <p></p>
    <p>General Notes:</p>
    <p></p>
    <ul class="releaseList">
        <li><strong>Removed AJAX Control Toolkit's dependency on Microsoft.Web.Extensions.Design.dll. This DLL is part of ASP.NET AJAX, but is not installed on machines without some flavor of Visual Studio already installed. The missing DLL caused problems for web servers (which typically do not have development tools installed on them).</strong></li>
        <li><strong>Moved the workaround for the scenario when EnableEventValidation=false (and ASP.NET AJAX doesn't render extenders or ScriptManager to the page during an async postback) up a level from CascadingDropDown to ExtenderBase so that all Toolkit controls will benefit</strong></li>
        <li>Revised ModalPopup server-side Show/Hide implementation to work even when the ModalPopupExtender is not part of the current async postback (regressed due to an ASP.NET AJAX change)</li>
        <li>Fixed an issue preventing ClientState from working properly inside an UpdatePanel (regressed due to an ASP.NET AJAX change)</li>
        <li>Fixed ModalPopup server-side Show/Hide to apply to only the next page load/async postback instead of applying to all subsequent page loads/async postbacks</li>
        <li>Updated Accordion to adjust its pane sizes after an async postback in order to avoid clipping the new pane contents if they were bigger than the old pane contents</li>
        <li>Fixed an issue with AjaxControlToolkit.IEDragDropManager.unregisterDropTarget that could cause a script error during page unload/dispose if multiple DragPanels were being used</li>
        <li>Fixed a width issue with AjaxControlToolkit.CommonToolkitScripts.getLocation</li>
        <li>Updated web.config files to include changes associated with ASP.NET AJAX Beta 2 and resolve issues running under IIS7</li>
    </ul>
    <p></p>
    <p>Breaking Changes</p>
    <p></p>
    <ul class="releaseList">
        <li>No breaking changes</li></ul>
    <p></p>
    <p>Known Issues</p>
    <p></p>
    <ul class="releaseList">
        <li>No new issues</li></ul>
    <br />
    <br />

    <p><u><strong>Version 1.0.61106.0</strong></u></p>
    <p></p>
    <p>General Notes:</p>
    <p></p>
    <ul class="releaseList">
        <li><strong>Updated the Toolkit framework, all controls, all samples, and all documentation for the new <asp:HyperLink ID="HyperLink11" runat="server" NavigateUrl="http://ajax.asp.net/">ASP.NET AJAX v1.0 Beta 2</asp:HyperLink></strong></li>
    </ul>
    <p></p>
    <p>Breaking Changes</p>
    <p></p>
    <ul class="releaseList">
        <li>No breaking changes</li></ul>
    <p></p>
    <p>Known Issues</p>
    <p></p>
    <ul class="releaseList">
        <li>No new issues</li></ul>
    <br />
    <br />
    
    <p><u><strong>Version 1.0.61020.0</strong></u></p>
    <p></p>
    <p>New controls (see sample pages on left):</p>
    <p></p>
    <ul class="releaseList">
        <li><strong>DropDown</strong>: Provides a dynamic drop-down functionality, similar to
            what is found in Windows Sharepoint Server.</li><li><strong>MutuallyExclusiveCheckbox</strong>: Allows picklists of mutually-exclusive
            values.</li><li><strong>ValidatorCallout</strong>: Adds great client-side UI to ASP.NET validators</li></ul>
    <p></p>
    <p>General Notes:</p>
    <p></p>
    <ul class="releaseList">
    <li><strong>Updated the Toolkit framework, all controls, all samples, and all documentation for the new <asp:HyperLink ID="HyperLink9" runat="server" NavigateUrl="http://ajax.asp.net/">ASP.NET AJAX v1.0 Beta</asp:HyperLink></strong></li><li>The ASP.NET AJAX Beta has a number of changes from earlier releases of "Atlas" - users are encouraged to familiarize themselves with those changes because many of them directly impact the Toolkit</li>
        <li>For Toolkit users (e.g. those not writing components, see this <a href="Walkthrough/AtlasToAspNetAjax.aspx">
            walkthrough</a> for migrating your pages.</li>
        <li>TemplateVSI now includes Web Site projects in addition to the Extender projects and Extender items it already had - C# and VB versions are available for all templates</li></ul>
    <p></p>
    <p>Breaking Changes</p>
    <p></p>
    <ul class="releaseList">
        <li>Technically, everything in this release is a breaking change since the syntax on
            the client- and server-side has changed. We have, however, worked hard to
            minimize the impact of this to page developers. The server-side move to this
            build is trivial, see the <a href="Walkthrough/AtlasToAspNetAjax.aspx">walkthrough</a>.</li><li>The ASP.NET AJAX Extensions DLL (now Microsoft.Web.Extensions.dll) is not included with the Toolkit and must be <asp:HyperLink ID="HyperLink10" runat="server" NavigateUrl="http://ajax.asp.net/">downloaded and installed separately</asp:HyperLink></li><li>Extenders no longer contain a set of properties classes. The property values
            for an extender are now directly on the extender object. This means that,
            in past cases where you had one extender and multiple properties objects, you will
            now have multiple extenders. This greatly simplifies the use of extenders.</li><li>The coding pattern for JavaScript behaviors has changed from the closure model to the Prototype model - all Toolkit behaviors have been updated. See documentation
                on the ASP.NET AJAX website for more information on this change.</li><li>The default Toolkit tag prefix changed from "atlasToolkit" to "ajaxToolkit"</li><li>Profile Binding is not supported in this release.</li></ul>
    <p></p>
    <p>Known Issues</p>
    <p></p>
    <ul class="releaseList">
        <li>Many of the controls are known to work well with the recently released Opera browser, though Opera support was not a focus of this release. In future releases,
            we plan to continue to improve our Opera support as ASP.NET AJAX does.</li></ul>
    <br />
    <br />
    <p><u><strong>Version 1.0.60914.0</strong></u></p>
    <p></p>
    <p>New controls (see sample pages on left):</p>
    <p></p>
    <ul class="releaseList">
        <li><strong>Animation</strong>: Adds powerful, easy to use animations to any element or control</li><li><strong>NoBot</strong>: Applies simple rules to prevent automated bots or scripts from posting to a page</li><li><strong>Slider</strong>: Adds an elegant scrollbar-like user interface for setting numeric values</li><li><strong>UpdatePanelAnimation</strong>: Animates page elements corresponding to hidden postback activity</li></ul>
    <p></p>
    <p>General Notes:</p>
    <p></p>
    <ul class="releaseList">
    <li><strong>Made Microsoft.AtlasControlExtender.dll functionality public and integrated it into AtlasControlToolkit.dll</strong></li><li><strong>Added animation framework that makes creating and running complex animations easy - see <asp:HyperLink ID="HyperLink8" runat="server" NavigateUrl="~/Walkthrough/UsingAnimations.aspx">Using animations</asp:HyperLink> for details</strong></li><li><strong>Fixed problem introduced in last release where extender entries in the properties window didn't expand when the '+' was clicked</strong></li><li>Significantly improved data binding implementation to avoid unnecessary problems</li><li>Changed load time of ClientState values to make them more easily accessible by extender code</li><li>Improved FindControlHelper implementation to enable it to successfully find controls in more scenarios</li><li>Added ASP.NET AJAX profile service support to the extender base class to make persisting values easier - see <asp:HyperLink ID="HyperLink5" runat="server" Text="Other neat stuff" NavigateUrl="~/Walkthrough/OtherNeatStuff.aspx"></asp:HyperLink> for details</li><li>Added CommonToolkitScripts.getCurrentStyle function to hide the cross-browser aspects of querying the current (computed) style of an element</li><li>Added DynamicPopulate*Base classes to make it easy to add DynamicPopulate functionality to any extender - see <asp:HyperLink ID="HyperLink6" runat="server" Text="Other neat stuff" NavigateUrl="~/Walkthrough/OtherNeatStuff.aspx"></asp:HyperLink> for details</li><li>Added PageRequestManagerID property to BehaviorBase to allow behaviors to easily hook up to partial update begin/end events - see <asp:HyperLink ID="HyperLink7" runat="server" Text="Other neat stuff" NavigateUrl="~/Walkthrough/OtherNeatStuff.aspx"></asp:HyperLink> for details</li><li>Modified all behaviors to raise ASP.NET AJAX's "PropertyChanged" event when any of their property values change</li><li>Revised Safari compatibility layer patching to allow event handler access of window.event (enabled by default by ASP.NET AJAX)</li><li>Improved handling of ID changes to controls that create extenders/properties</li><li>Added data binding support to Accordion to allow creation of panes from a data source</li><li>Modified CascadingDropDown to populate DropDownList.SelectedItem.Text so that both text and value can be accessed</li><li>CascadingDropDown now populates its DropDownList before Page_Load so the .SelectedValue will can be used in Page_Load</li><li>CascadingDropDown now supports 1:many parent:child relationships so that multiple CascadingDropDowns can have the same parent</li><li>ConfirmButton and ModalPopup updated to respect the "disabled" state of buttons and ignore disabled button clicks</li><li>Added Radius property to DropShadow extender for customizing the corner radius</li><li>HoverMenu, ModalPopup, and PopupControl now have DynamicPopulate functionality built-in (via the new DynamicPopulate*Base classes)</li><li>Improved ModalPopup behavior when used with UpdatePanels in order to avoid possible null dereferences</li><li>Added support for showing and hiding the ModalPopup display element from server-side code</li><li>PasswordStrength now supports customizable weightings of each password requirement class</li><li>PopupControl now works properly when its TargetControlID is within an UpdatePanel</li><li>PopupControl now supports .Commit calls specifying the empty string ("")</li><li>Reenabled postback mode for ReorderList to allow scenarios that require postbacks</li><li>Fixed ReorderList behavior when SortOrderField is set with a bound IList data source</li><li>ResizableControl implementation changed to work around some browser bugs that unnecessarily reloaded images</li><li>Fixed some cursor display issues when tabbing through TextBoxWatermark</li><li>Modified TextBoxWatermark to re-apply its watermark after UpdatePanel postbacks to avoid losing the watermark</li><li>Added ToggleButton support for disabled checkbox images by enabling different images for disabled checked/unchecked boxes</li><li>Other minor improvements throughout</li></ul>
    <p></p>
    <p>Breaking Changes</p>
    <p></p>
    <ul class="releaseList">
        <li>Removed obsolete Commit and Cancel method overrides of PopupControlExtender (were replaced by versions that do not take a Control parameter in the previous release)</li><li>Namespace of certain classes changed from "Microsoft.AtlasControlExtender" to "AtlasControlToolkit"</li><li>Removed GetAccordionPanes method on Accordion, replaced by Panes property. Existing pages may have a tag prefix on the Panes element (e.g. "&lt;cc1:Panes&gt;") that should be removed.</li></ul>
    <p></p>
    <p>Known Issues</p>
    <p></p>
    <ul class="releaseList">
        <li>No new issues</li></ul>
    <br />
    <br />
    <p><u><strong>Version 1.0.60731.0</strong></u></p>
    <p></p>
    <p>New controls (see sample pages on left):</p>
    <p></p>
    <ul class="releaseList">
        <li><strong>DynamicPopulate</strong>: (Re-)populates any element with HTML content downloaded from the server.</li><li><strong>FilteredTextBox</strong>: Prevents unwanted characters from being typed into a text box.</li><li><strong>PagingBulletedList</strong>: Adds flexible paging and sorting to any bulleted list.</li><li><strong>PasswordStrength</strong>: Provides interactive feedback about the strength of a password being created.</li><li><strong>Rating</strong>: Displays a "4 out of 5 stars" interface for ranking.</li></ul>
    <p></p>
    <p>General Notes:</p>
    <p></p>
    <ul class="releaseList">
    <li><strong>Updated to include/support the ASP.NET AJAX July CTP release</strong></li><li>Added support for data binding in Extender properties declarations - see <asp:HyperLink ID="HyperLink3" runat="server" Text="Other neat stuff" NavigateUrl="~/Walkthrough/OtherNeatStuff.aspx"></asp:HyperLink> for details</li><li>Changed ASP.NET AJAX control registration behavior to better enable multiple extenders to hook up to the same element (example: ASP.NET AJAX's AutoComplete and Toolkit's TextBoxWatermark)</li><li>Added getCurrentStyle and getInheritedBackgroundColor helpers to CommonToolkitScripts object in Common.js to isolate the relevant browser-specific code</li><li>Modified AlwaysVisibleControl to use CSS "position:fixed" style on browsers that support it</li><li>Enabled AutoPostBack scenario for CascadingDropDown DropDownLists so that a submit button is no longer necessary</li><li>Added "LoadingText" property to CascadingDropDown for specifying the text to display as the DropDownList populates from the server</li><li>Changed DropShadow to match the visibility state of its target</li><li>Added ReorderList support for Array and IList data sources</li><li>Changed ReorderList to use callbacks instead of postbacks for a more seamless user experience</li><li>Simplified ReorderList hook-up process to make it even easier to use</li><li>Added RoundedCorners support for hooking up to elements with "width=x%"</li><li>Added get_Text and set_Text methods to TextBoxWatermark to support client-side script access to the text in a watermarked text box</li><li>Improved automated testing coverage</li><li>Added debug mode to automated test framework to make it easier to identify exceptions</li><li>Added support for firing keyboard events to automated test framework</li><li>Removed double-encoding of script references in generated XML Script</li><li>Other minor improvements throughout</li></ul>
    <p></p>
    <p>Breaking Changes</p>
    <p></p>
    <ul class="releaseList">
        <li>No breaking changes</li></ul>
    <p></p>
    <p>Known Issues</p>
    <p></p>
    <ul class="releaseList">
        <li>No new issues</li></ul>
    <br />
    <br />
    <p><u><strong>Version 1.0.60626.0</strong></u></p>
    <p></p>
    <p>New controls (see sample pages on left):</p>
    <p></p>
    <ul class="releaseList">
        <li><strong>Accordion</strong>: Displays one pane at a time from a set of multiple panes.</li><li><strong>NumericUpDown</strong>: Lets you attach flexible up/down "spinners" to any TextBox.</li><li><strong>ResizableControl</strong>: Makes any control resizable.</li></ul>
    <p></p>
    <p>General Notes:</p>
    <p></p>
    <ul class="releaseList">
    <li><strong>Included custom test framework along with complete control tests - see <asp:HyperLink ID="HyperLink1" runat="server" Text="Automated Testing" NavigateUrl="~/Walkthrough/AutomatedTesting.aspx"></asp:HyperLink> for details</strong></li><li>Added ResolveControlID event to replace ResolveTargetControlID and enable page authors to identify controls that can't be located by the ExtenderControlBase class (e.g., because they're in an unusual naming container) - see <asp:HyperLink ID="HyperLink2" runat="server" Text="Other neat stuff" NavigateUrl="~/Walkthrough/OtherNeatStuff.aspx"></asp:HyperLink> for details</li><li>Moved getMarkupContext/removeObject workaround for ASP.NET AJAX dispose issue up to BehaviorBase script class; modified all behaviors to inherit from it</li><li>Changed the way ID properties are handled by the ExtenderControlBase class so that controls with IDs no longer cause problems in design mode</li><li>Modified ExtenderControlBase class to detect target controls marked Visible="false" and not render the corresponding Toolkit control</li><li>Fixed GetClientClassForControl method to match derived classes</li><li>Removed CollabpsibleContent UserControl from sample web site because it broke design mode; use CollapsiblePanel directly instead</li><li>Added "default value" setting to CascadingDropDown's CascadingDropDownNameValue class for specifying the default value</li><li>Fixed regression where CascadingDropDown DropDownList controls no longer fired the OnSelectedIndexChanged event</li><li>Fixed regression where CascadingDropDown's onXxxError methods weren't populating the error string correctly</li><li>Improved CollapsiblePanel sizing behavior</li><li>Fixed minor height miscalculation in DropShadow</li><li>Fixed ModalPopup bug where multiple background layers could be created</li><li>Improved ModalPopup popup technique to allow form controls within the popup to postback</li><li>Improved ModalPopup modality by disabling tab-able controls in the background</li><li>Worked around ModalPopup IE z-index issue by hiding dropdown controls in the background</li><li>Got PopupControl working within a repeater; improved server-side usability, general resiliency</li><li>Fixed various ReorderList functionality and sample issues</li><li>Added ItemsCollection to ReorderList</li><li>Fixed problem where using ToggleButton with AutoPostBack wasn't working</li><li>Improved ToggleButton decoy image tracking during page resize</li><li>Added "alt" image properties to ToggleButton images</li></ul>
    <p></p>
    <p>Breaking Changes</p>
    <p></p>
    <ul class="releaseList">
        <li>Removed obsolete ScriptReferenceCollection property (was replaced by RequiredScriptAttribte in previous release)</li></ul>
    <p></p>
    <p>Known Issues</p>
    <p></p>
    <ul class="releaseList">
        <li>No new issues</li></ul>
    <br />
    <br />
    <p><u><strong>Version 1.0.60504.0</strong></u></p>
    <p></p>
    <p>New controls (see sample pages on left):</p>
    <p></p>
    <ul class="releaseList">
        <li><strong>AlwaysVisibleControl</strong>: Docks any panel to the browser edge so it remains visible all the time.</li><li><strong>DropShadow</strong>: Adds attractive drop shadows to any control on the page!</li><li><strong>ModalPopup</strong>: Shows styled modal UI without using HTML dialogs!</li><li><strong>RoundedCorners</strong>: Rounds the corners of any control for a clean, professional look.</li></ul>
    <p></p>
    <p>General Notes:</p>
    <p></p>
    <ul class="releaseList">
    <li>Significantly improved support for Apple's Safari web browser by fixing most issues
        (see known issues below)</li><li>Enabled strong-name signing for AtlasControlToolkit.dll</li><li>Added ID property to TargetPropertiesBase. This allows behaviors to be accessed
            directly from script using the "$object('id')" statement.</li><li>Added GetUniqueID helper for building unique behavior IDs for behaviors that need
            ideas and are used in repeaters.</li><li>Changed XMLScript generation to bind to specific control types where possible. Old
            version always generated "&lt;control id='controlId&gt;" for all control types.
            Now for known types (e.g. TextBox), "&lt;textBox id='controlId'&gt;" Will be generated,
            which allows access to ASP.NET AJAX control class (e.g. this.control is Sys.UI.TextBox
            instead of Sys.UI.Control).</li><li>CascadingDropDown can now call PageMethod as well as web services (Just leave the
            ServicePath property blank).</li><li>CascadingDropDown now has a "SelectedValue" property to predefine what's selected.
            Note this should be the text representation of the value (not the text) that you
            want to select</li><li>CascadingDropDown's usage of web service callbacks has been optimized</li><li>Added support for virtual paths in control properties (properties that use URLs
            can now use the "~/File.htm" representation)</li><li>Added RequiredPropertyAttribute to simplify EnsureValid logic</li><li>Added RequiredScriptAttribute to simplify control sharing, as well as the ability
        to specify another extender type in the attribute. Doing so will cause the
        referenced extender's scripts to be loaded before the scripts for the given control.
        This allows usage of other extenders classes in a custom extender's behavior.</li><li>Improved PopupControl user interaction behavior - now dismisses on click instead
        of mouse leave</li><li>Improved CollapsiblePanel behavior, added ImageControlID property for expand/collapse images. Will now properly handle initially hidden panels (they need overflow
        set to hidden) so they don't initially show.</li><li>Added Common.js file for sharing common control/script code. To reference
        this code, add [RequiredScripts(typeof(CommonToolkitScripts)] to your extender type.</li><li>Changed from overloading DisplayName to using dedicated ClientPropertyNameAttribute
        for matching property names on TargetPropertiesBase to the client name</li><li>Modified sample pages to use CollapsiblePanel for section </li>
    <li>Miscellaneous fixes and incorporation of user feedback to all controls</li></ul>
    <p></p>
    <p>Breaking Changes</p>
    <p></p>
    <ul class="releaseList">
        <li>RequiredScripts property on ExtenderControlBase has been deprecated. Move
            to RequiredScriptsAttribute before next refresh.</li><li>ToggleButton's image properties are now typed as string instead of System.Uri</li><li>DisplayNameAttribute no longer modifies the name for client properties. This
            functionality has been moved to the ClientPropertyNameAttribute</li><li>String properties no longer default to null if empty, but rather "".</li><li>For Safari compat, type prefix names must be all lower-case. The ExtenderBaseControl
            now enforces this. If your components stop working when you install this release,
            find the "<em>Sys.TypeDescriptor.addType</em>" call in your JS behavior file and
            change the first parameter value to be only lower case characters.</li></ul>
    <p></p>
    <p>Known Issues</p>
    <p></p>
    <ul class="releaseList">
        <li>Since UpdatePanels don't work on Safari, PopupControl won't be able to populate
            the textboxes upon closing.</li><li>Using behaviors directly through script. Adding the ID property to the behaviors
            made them very easy to access directly through script. If you are using the
            behaviors directly from script, changing properties after they have been initialized.</li><li>If you are writing a custom behavior that binds to a DropDownList, you may run into
            issues. The workaround is to override GetClientClassForControl on your extender
            class and return "control". Note this will prevent you from accessing Sys.UI.Select
            features on the control class that is attached to your behavior.</li><li>On Internet Explorer, using the DropShadow control you may see a brief flash when
            it is reparenting items inside of the target panel.</li><li>On Safari, tabbing out of a textbox doesn't appear to cause an "onblur" event.
            This interferes with TextBoxWatermark replacing the watermark if the user tabs through.
            Works fine with the mouse.</li><li>TextBoxWatermark watermark isn't cleared before submit on Safari</li><li>PopupControl doesn't dismiss reliably on Safari when the mouse is clicked outside
            the popup.</li></ul>
    <br />
    <br />
    <p><u><strong>Version 1.0.0.0</strong></u></p>
    <p></p>
    <ul class="releaseList">
    <li>Initial release!</li><li>New controls:
    <ul class="releaseList">
    <li>CascadingDropDown: Easily link drop downs, complete with asynchronous population and no postbacks!</li><li>CollapsiblePanel:  This extender allows panels on your page to collapse and expand with no code.</li><li>ConfirmButton: This extender adds a confirm dialog to any Button, LinkButton, or ImageButton control.</li><li>DragPanel: Makes any panel into an object that you can drag around the page.</li><li>HoverMenu: Allows UI to pop up next to a control when the user hovers over it.</li><li>PopupControl: This extender turns any panel into a popup.</li><li>ReorderList: This control is a full-featured data-bound control that allows its elements to be reordered on the client via drag and drop.</li><li>TextBoxWatermark:  This extender adds "watermark" prompt text to TextBoxes on the page.</li><li>ToggleButton:  This extender turns an ASP.NET CheckBox into an image checkbox.</li></ul>
    </li>
    </ul>
</asp:Content>