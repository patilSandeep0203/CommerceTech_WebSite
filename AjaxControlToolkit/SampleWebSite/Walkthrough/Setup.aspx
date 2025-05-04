<%@ Page
    Language="C#"
    MasterPageFile="~/DefaultMaster.master"
    Title="Setup" %>
<asp:Content ContentPlaceHolderID="SampleContent" Runat="Server">
    <div class="walkthrough">
        <div class="heading">Setup your environment</div>

        <div class="subheading">Install binaries</div>
        <ol>
            <li>Install the latest <a href="http://ajax.asp.net/">ASP.NET 2.0 AJAX Extensions</a> package</li>
            <li>Unpack the AJAX Control Toolkit into a folder on your computer</li>
        </ol>

        <div class="subheading">Configure Visual Studio 2005 and Visual Web Developer</div>
        <ol>
            <li>Create a new web site from the ASP.NET AJAX web site template by opening the "File" menu,
                clicking "New", "Web Site...", and picking "ASP.NET AJAX Web Site" under "My Templates"</li>
            <li>Right-click on the Toolbox and select "Add Tab", and add a tab called "AJAX Control
                Toolkit"</li>
            <li>Inside that tab, right-click on the Toolbox and select "Choose Items..."</li>
            <li>When the "Choose Toolbox Items" dialog appears, click the "Browse..." button. Navigate
                to the folder where you installed the ASP.NET AJAX Control Toolkit package. You will find
                a folder called "SampleWebSite", and under that another folder called "bin". Inside
                that folder, select "AjaxControlToolkit.dll" and click OK. Click OK again
                to close the Choose Items Dialog.</li>
            <li>You can now use the included sample controls in your web sites!</li>
        </ol>

        <div class="subheading">Install the "AJAX Control Extender"</div>
        <ol>
            <li><em>Note that the "AJAX Control Extender" is for building extenders and controls, not
                web sites, and therefore does not work with Visual Web Developer - it is for use with
                Visual C#, Visual Basic, or Visual Studio</em></li>
            <li>In the folder where you installed the AJAX Control Toolkit package, you will find
                a folder called "AjaxControlExtender" with a file called "AjaxControlExtender.vsi"
                inside it - double-click AjaxControlExtender.vsi to install it</li>
            <li>Choose which templates you would like to install (it is recommended to install all
                templates), then click "Next", then "Yes" to allow the unsigned content (note: as a public project,
                this content can't be signed by Microsoft), then "Finish"</li>
            <li>You have now installed the templates are ready to create your own Toolkit-based web site
                or ASP.NET AJAX Extenders!</li>
        </ol>
    </div>
</asp:Content>