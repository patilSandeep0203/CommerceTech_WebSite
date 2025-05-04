<%@ Page
    Language="C#"
    MasterPageFile="~/DefaultMaster.master"
    AutoEventWireup="true"
    Inherits="CommonPage"
    Title="ASP.NET AJAX Control Toolkit" %>
<asp:Content ContentPlaceHolderID="SampleContent" Runat="Server">
    <p>
    Welcome to the ASP.NET AJAX Control Toolkit sample website. Please choose from any of the samples on the left.
    </p>
    <br />
    <p><strong><u>Installation Files</u></strong></p>
    <br />
    <p>Please visit the <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="http://www.codeplex.com/Wiki/View.aspx?ProjectName=AtlasControlToolkit">AJAX Control Toolkit Project Page</asp:HyperLink> on CodePlex</p>
    <br />
    <p><strong><u>Release Notes</u></strong></p>

    <br />

    <p><u><strong>Version 1.0.10301.0</strong></u> - <em>Requires ASP.NET AJAX version 1.0</em></p>
    <p></p><br />
    <p><strong>New Controls:</strong></p>
    <p></p>
    <ul class="releaseList">
        <li><strong>ListSearch:</strong> Extender that searches for items in a listbox or dropdown</li>
        <li><strong>SlideShow:</strong> Extender for image controls that lets you transition through a set of pictures</li>
    </ul><br />
    <p><strong>General Notes:</strong></p>
    <p></p>
    <ul class="releaseList">
        <li>Skinning support added for extenders; all demo page collapsible panels are now themed</li>
        <li>Localization infrastructure added to Toolkit, specifically, the "Today" string in the Calendar control is localized in 14 languages</li>        
        <li>JavaScript comment stripper added to the Toolkit which cuts the size of the scripts by about half on average</li>
        <li>Accessibility fixes made to the website and controls in terms of keyboard support, specifically, Accordion, CollapsiblePanel, Calendar and NumericUpDown</li>
        <li>Accordion, Slider and ReorderList bug fixes</li>
    </ul>
    <p></p>
    <br />
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
    Link to Toolkit release <a href="ArchivedReleases.aspx">archive</a>.
</asp:Content>