<%@ Page
    Language="C#"
    MasterPageFile="~/DefaultMaster.master"
    AutoEventWireup="true"
    Inherits="CommonPage"
    Title="AutoComplete Sample"
    Theme="SampleSiteTheme" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
<asp:Content ContentPlaceHolderID="SampleContent" Runat="Server">
    <asp:ScriptManager runat="server" />
    <div class="demoarea">
        <div class="demoheading">AutoComplete Demonstration</div>
        <p/>
            Type some characters in this textbox.  The web service returns random words
            that start with the text you have typed.
            <br /><br />
            
            <asp:TextBox runat="server" ID="myTextBox" Width="300" autocomplete="off"/>
            <ajaxToolkit:AutoCompleteExtender
                runat="server" 
                ID="autoComplete1" 
                TargetControlID="myTextBox"
                ServicePath="AutoComplete.asmx" 
                ServiceMethod="GetCompletionList"
                MinimumPrefixLength="2" 
                CompletionInterval="1000"
                EnableCaching="true"
                CompletionSetCount="12" />

            <script type="text/javascript">
                // Work around browser behavior of "auto-submitting" simple forms
                var frm = document.getElementById("aspnetForm");
                if (frm) {
                    frm.onsubmit = function() { return false; };
                }
            </script>
            <%-- Prevent enter in textbox from causing the collapsible panel from operating --%>
            <input type="submit" style="display:none;" />
    </div>
    <div class="demobottom"></div>

    <asp:Panel ID="Description_HeaderPanel" runat="server" style="cursor: pointer;">
        <div class="heading">
            <asp:ImageButton ID="Description_ToggleImage" runat="server" ImageUrl="~/images/collapse.jpg" AlternateText="collapse"  />
            AutoComplete Description
        </div>
    </asp:Panel>
    <asp:Panel id="Description_ContentPanel" runat="server" style="overflow:hidden;">
        <p>
            AutoComplete is an ASP.NET AJAX extender that can be attached to any TextBox control, and will
            associate that control with a popup panel to display words that begin with the prefix typed into
            the textbox.  
        </p>
        <br />
        <p>
            The dropdown with candidate words supplied by a web service is positioned on the bottom left of
            the text box.
        </p>
        <br />
        <p>
            In the sample above, the textbox is associated with an AutoCompleteExtender that pulls words that
            start with the contents of the textbox using a web service.
        </p>
        <br />
        <p>
            When you have typed more content than the specified minimum word length, a popup will show words
            or phrases starting with that value.  Caching is turned on, so typing the same prefix multiple
            times results in only one call to the web service.
        </p>
    </asp:Panel>

    <asp:Panel ID="Properties_HeaderPanel" runat="server" style="cursor: pointer;">
        <div class="heading">
            <asp:ImageButton ID="Properties_ToggleImage" runat="server" ImageUrl="~/images/expand.jpg" AlternateText="expand"  />
            AutoComplete Properties
        </div>
    </asp:Panel>
    <asp:Panel id="Properties_ContentPanel" runat="server" style="overflow:hidden;" Height="0px">
        <p>
            The textbox is linked with an AutoCompleteExtender which is initialized with this code.
            The <em>italic</em> properties are optional:
        </p>
<pre>&lt;ajaxToolkit:AutoCompleteExtender 
    runat="server" 
    ID="autoComplete1" 
    TargetControlID="myTextBox"
    ServiceMethod="GetCompletionList"
    <em>ServicePath</em>="AutoComplete.asmx" 
    <em>MinimumPrefixLength</em>="2" 
    <em>CompletionInterval</em>="1000"
    <em>EnableCaching</em>="true"
    <em>CompletionSetCount</em>="12" /&gt;</pre>
        <ul>
            <li><strong>TargetControlID</strong> - The TextBox control where the user types content to be
                automatically completed</li>
            <li><strong>ServiceMethod</strong> - The web service method to be called.  The signature of this
                method must match the following:
<pre>
[System.Web.Services.WebMethod]
[System.Web.Script.Services.ScriptMethod]
public string[] GetCompletionList(string prefixText, int count) { ... }</pre>
                Note that you can replace "GetCompletionList" with a name of your choice, but the return type
                and parameter name and type must exactly match, including case.</li>
            <li><strong>ServicePath</strong> - The path to the web service that the extender will pull the
                word\sentence completions from.  If this is not provided, the service method should be a page
                method.</li>
           <li><strong>MinimumPrefixLength</strong> - Minimum number of characters that must be entered before
                getting suggestions from the web service.</li>
            <li><strong>CompletionInterval</strong> - Time in milliseconds when the timer will kick in to get
                suggestions using the web service.</li>        
            <li><strong>EnableCaching</strong> - Whether client side caching is enabled.</li>
            <li><strong>CompletionSetCount</strong> - Number of suggestions to be retrieved from the web service.</li>
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