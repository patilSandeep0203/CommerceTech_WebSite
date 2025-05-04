<%@ Page 
    Language="C#" 
    MasterPageFile="~/DefaultMaster.master" 
    AutoEventWireup="true"
    Inherits="CommonPage" 
    Title="ModalPopup Sample"
    Theme="SampleSiteTheme" %>
<%@ Register 
    Assembly="AjaxControlToolkit" 
    Namespace="AjaxControlToolkit" 
    TagPrefix="ajaxToolkit" %>
<asp:Content ContentPlaceHolderID="SampleContent" runat="Server">
    <asp:ScriptManager runat="server" />
    <script type="text/javascript">
        var styleToSelect;
        function onOk() {
            $get('Paragraph1').className = styleToSelect;
        }
    </script>
    <div class="demoarea">
        <div class="demoheading">ModalPopup Demonstration</div>

        <p id="Paragraph1"><%= GetContentFillerText() %></p><br />
        <asp:LinkButton ID="LinkButton1" runat="server" Text="Click here to change the paragraph style" />
        
        <asp:Panel ID="Panel1" runat="server" Style="display: none" CssClass="modalPopup">
            <asp:Panel ID="Panel3" runat="server" Style="cursor: move;background-color:#DDDDDD;border:solid 1px Gray;color:Black">
                <div>
                    <p>Choose the paragraph style you would like:</p>
                </div>
            </asp:Panel>
                <div>
                    <p>
                        <input type="radio" name="Radio" id="RadioA" checked="checked"
                            onclick="styleToSelect = 'sampleStyleA';" />
                        <label for="RadioA" class="sampleStyleA"
                            style="padding: 3px;">Sample paragraph text</label>
                    </p>
                    <p>
                        <input type="radio" name="Radio" id="RadioB"
                            onclick="styleToSelect = 'sampleStyleB';" />
                        <label for="RadioB" class="sampleStyleB"
                            style="padding: 3px;">Sample paragraph text</label>
                    </p>
                    <p>
                        <input type="radio" name="Radio" id="RadioC"
                            onclick="styleToSelect = 'sampleStyleC';" />
                        <label for="RadioC" class="sampleStyleC"
                            style="padding: 3px;">Sample paragraph text</label>
                    </p>
                    <p>
                        <input type="radio" name="Radio" id="RadioD"
                            onclick="styleToSelect = 'sampleStyleD';" />
                        <label for="RadioD" class="sampleStyleD"
                            style="padding: 3px;">Sample paragraph text</label>
                    </p>
                    <p style="text-align: center;">
                        <asp:Button ID="OkButton" runat="server" Text="OK" />
                        <asp:Button ID="CancelButton" runat="server" Text="Cancel" />
                    </p>
                </div>
        </asp:Panel>
        
        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender" runat="server" 
            TargetControlID="LinkButton1"
            PopupControlID="Panel1" 
            BackgroundCssClass="modalBackground" 
            OkControlID="OkButton"
            OnOkScript="onOk()" 
            CancelControlID="CancelButton" 
            DropShadow="true"
            PopupDragHandleControlID="Panel3" />
    </div>
    <div class="demobottom"></div>
    <asp:Panel ID="Description_HeaderPanel" runat="server" Style="cursor: pointer;">
        <div class="heading">
            <asp:ImageButton ID="Description_ToggleImage" runat="server" ImageUrl="~/images/collapse.jpg" AlternateText="collapse" />
            ModalPopup Description
        </div>
    </asp:Panel>
    <asp:Panel ID="Description_ContentPanel" runat="server" Style="overflow: hidden;">
        <p>
            The ModalPopup extender allows a page to display content to the user in a "modal"
            manner which prevents the user from interacting with the rest of the page. The modal
            content can be any hierarchy of controls and is displayed above a background that
            can have a custom style applied to it. When displayed, only the modal content can
            be interacted with; clicking on the rest of the page does nothing. When the user
            is done interacting with the modal content, a click of an OK/Cancel control dismisses
            the modal content and optionally runs custom script. The custom script will typically
            be used to apply whatever changes were made while the modal mode was active. If
            a postback is required, simply allow the OK/Cancel control to postback and the page
            to re-render. You can also absolutely position a modal popup by setting the X and Y 
            properties. By default it is centered on the page, however if just X or Y is specified
            then it is centered vertically or horizontally.
        </p>
    </asp:Panel>
    <asp:Panel ID="Properties_HeaderPanel" runat="server" Style="cursor: pointer;">
        <div class="heading">
            <asp:ImageButton ID="Properties_ToggleImage" runat="server" ImageUrl="~/images/expand.jpg" AlternateText="expand" />
            ModalPopup Properties
        </div>
    </asp:Panel>
    <asp:Panel ID="Properties_ContentPanel" runat="server" Style="overflow: hidden;"
        Height="0px">
        <p>
            The control above is initialized with this code. The display on the modal popup element
            is set to none to avoid a flicker on render. 
            The <em>italic</em> properties
            are optional:
        </p>
        <pre>&lt;ajaxToolkit:ModalPopupExtender ID="MPE" runat="server"
    TargetControlID="LinkButton1"
    PopupControlID="Panel1"
    <em>BackgroundCssClass</em>="modalBackground" 
    <em>DropShadow</em>="true" 
    <em>OkControlID</em>="OkButton" 
    <em>OnOkScript</em>="onOk()"
    <em>CancelControlID</em>="CancelButton" 
    <em>PopupDragHandleControlID</em>="Panel3" /&gt;</pre>
        <ul>
            <li><strong>TargetControlID</strong> - The ID of the element that activates the modal
                popup</li>
            <li><strong>PopupControlID</strong> - The ID of the element to display as a modal popup</li>
            <li><strong>BackgroundCssClass</strong> - The CSS class to apply to the background when
                the modal popup is displayed</li>
            <li><strong>DropShadow</strong> - True to automatically add a drop-shadow to the modal
                popup</li>
            <li><strong>OkControlID</strong> - The ID of the element that dismisses the modal popup</li>
            <li><strong>OnOkScript</strong> - Script to run when the modal popup is dismissed with
                the OkControlID</li>
            <li><strong>OkCancelID</strong> - The ID of the element that cancels the modal popup</li>
            <li><strong>OkCancelScript</strong> - Script to run when the modal popup is dismissed
                with the CancelControlID</li>
            <li><strong>PopupDragHandleControlID</strong> - The ID of the embedded element that
                contains the popup header/title which will be used as a drag handle</li>
            <li><strong>X</strong> - The X coordinate of the top/left corner of the modal
                popup (the popup will be centered horizontally if not specified)</li>
            <li><strong>Y</strong> - The Y coordinate of the top/left corner of the modal
                popup (the popup will be centered vertically if not specified)</li>
        </ul>
    </asp:Panel>

    <script type="text/javascript">
        // The following snippet works around a problem where FloatingBehavior
        // doesn't allow drops outside the "content area" of the page - where "content
        // area" is a little unusual for our sample web pages due to their use of CSS
        // for layout.
        function setBodyHeightToContentHeight() {
            document.body.style.height = Math.max(document.documentElement.scrollHeight, document.body.scrollHeight) + "px";
        }
        setBodyHeightToContentHeight();
        $addHandler(window, "resize", setBodyHeightToContentHeight);    
    </script>

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