<%@ Page
    Language="C#"
    MasterPageFile="~/DefaultMaster.master"
    Title="Calendar Sample"
    Culture="auto"
    UICulture="auto"
    Theme="SampleSiteTheme" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
<script type="text/C#" runat="server">
    protected override void  OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        this.cultureLabel.Text = "Culture: " + this.Culture + "; UICulture: " + this.UICulture;
    }
</script>
<asp:Content ContentPlaceHolderID="SampleContent" Runat="Server">
    <asp:ScriptManager runat="Server" EnableScriptGlobalization="true" EnableScriptLocalization="true" />
    <div class="demoarea">
        <div class="demoheading">Calendar Demonstration</div>
        <br />
        
        <b>Default calendar: </b><br />
        <asp:TextBox runat="server" ID="Date1" autocomplete="off" /><br />
        <ajaxToolkit:CalendarExtender ID="defaultCalendarExtender" runat="server" 
            TargetControlID="Date1" />
        <div style="font-size: 90%"><em>(Set the focus to the textbox to show the calendar)</em></div>
        <br /><br />
        
        <b>Calendar with a custom style and formatted date:</b><br />
        <asp:TextBox runat="server" ID="Date2" autocomplete="off" /><br />
        <ajaxToolkit:CalendarExtender ID="customCalendarExtender" runat="server"
            TargetControlID="Date2"
            CssClass="MyCalendar"
            Format="MMMM d, yyyy" />
        <div style="font-size: 90%"><em>(Set the focus to the textbox to show the calendar)</em></div>
        <br /><br />
        
        <b>Calendar with an associated button:</b><br />
        <asp:TextBox runat="server" ID="Date5" />
        <asp:ImageButton runat="Server" ID="Image1" ImageUrl="Calendar_scheduleHS.png" AlternateText="Click to show calendar" /><br />
        <ajaxToolkit:CalendarExtender ID="calendarButtonExtender" runat="server"
            TargetControlID="Date5"
            PopupButtonID="Image1" />
        <div style="font-size: 90%"><em>(Click the image button to show the calendar; this calendar dismisses automatically when you choose a date)</em></div>
    </div>
    <div class="demobottom"></div>

    <asp:Panel ID="Description_HeaderPanel" runat="server" Style="cursor: pointer;">
        <div class="heading">
            <asp:ImageButton ID="Description_ToggleImage" runat="server" ImageUrl="~/images/collapse.jpg" AlternateText="collapse"  />
            Calendar Description
        </div>
    </asp:Panel>
    <asp:Panel ID="Description_ContentPanel" runat="server" Style="overflow: hidden;">
        <p>
            Calendar is an ASP.NET AJAX extender that can be attached to any ASP.NET TextBox control. It provides
            client-side date-picking functionality with customizable date format and UI in a popup control. You can 
            interact with the calendar by clicking on a day to set the date, or the "Today" link to set the
            current date.
        </p>
        <br />
        <p>
            In addition, the left and right arrows can be used to move forward or back a month. By 
            clicking on the title of the calendar you can change the view from Days in the current month, to Months 
            in the current year.  Another click will switch to Years in the current Decade.  This action allows you 
            to easily jump to dates in the past or the future from within the calendar control.
        </p><br />
        <p>
            The page uses the following culture settings: 
            <asp:Label ID="cultureLabel" Font-Bold="true" runat="server" />
            which are picked up from the browser. The page properties have Culture="auto" and UICulture="auto" set to enable the same.
            See this <asp:HyperLink runat="server" Text="msdn article " NavigateUrl="http://msdn2.microsoft.com/en-us/library/bz9tc508.aspx" />
            for more information. The ScriptManager on this Calendar demo page has EnableScriptGlobalization="true"
            and EnableScriptLocalization="true".
        </p>
    </asp:Panel>
    <asp:Panel ID="Properties_HeaderPanel" runat="server" Style="cursor: pointer;">
        <div class="heading">
            <asp:ImageButton ID="Properties_ToggleImage" runat="server" ImageUrl="~/images/expand.jpg" AlternateText="expand"  />
            Calendar Properties
        </div>
    </asp:Panel>
    <asp:Panel ID="Properties_ContentPanel" runat="server" Style="overflow: hidden;" Height="0px">
        <p>
            The calendar associated with a button has been initialized with this code. The properties in <em>italic</em> 
            are optional:
        </p>
<pre>&lt;ajaxToolkit:Calendar runat="server"
    TargetControlID="Date1"
    <em>CssClass</em>="ClassName"
    <em>Format</em>="MMMM d, yyyy"
    <em>PopupButtonID</em>="Image1" /&gt;</pre>
        <ul>
            <li><strong>TargetControlID</strong> - The ID of the TextBox to extend with the calendar.</li>
            <li><strong>CssClass</strong> - Name of the CSS class used to style the calendar. Here is a 
                <asp:HyperLink ID="HyperLink1" runat="server" Text="blog post" 
                NavigateUrl="http://community.bennettadelson.com/blogs/rbuckton/archive/2007/02/02/Skinning-model-for-Calendar-and-Tabs-in-Ajax-Control-Toolkit.aspx" >
                </asp:HyperLink> by the Calendar control author, Ronald Buckton, on how to customize the Calendar look and feel.</li>
            <li><strong>Format</strong> - <a href="http://msdn2.microsoft.com/en-us/library/bb79761a-ca08-44ee-b142-b06b3e2fc22b.aspx">Format string</a>
                used to display the selected date.</li>
            <li><strong>PopupButtonID</strong> - The ID of a control to show the calendar popup when clicked.  If this value
                is not set, the calendar will pop up when the textbox receives focus.</li>
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