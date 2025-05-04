<%@ Page Language="C#" MasterPageFile="~/Default.master" Title="Untitled Page" %>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <ajaxToolkit:TabContainer runat="server" ID="TabContainer1">
        
        <ajaxToolkit:TabPanel runat="server" ID="TabPanel1" HeaderText="TabPanel1">
            <ContentTemplate>
                TabPanel1
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        
        <ajaxToolkit:TabPanel runat="server" ID="TabPanel2" HeaderText="TabPanel2" Enabled="false">
            <ContentTemplate>
                TabPanel2
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        
        <ajaxToolkit:TabPanel runat="server" ID="TabPanel3" HeaderText="TabPanel3">
            <ContentTemplate>
                TabPanel3
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        
        <ajaxToolkit:TabPanel runat="server" ID="TabPanel4" HeaderText="TabPanel4" DynamicServicePath="~/ToolkitTestService.asmx" DynamicServiceMethod="GetContextKey" DynamicContextKey="Web Service Success" />
        
    </ajaxToolkit:TabContainer>
    
    <asp:Button runat="Server" ID="Button1" />

    <script type="text/javascript">
        // (c) Copyright Microsoft Corporation.
        // This source is subject to the Microsoft Permissive License.
        // See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
        // All other rights reserved.
    
        // Script objects that should be loaded before we run
        var typeDependencies = ['AjaxControlToolkit.TabContainer'];

        // TestRunner
        var testHarness = null;
        var tabContainer1;
        var tabPanel1;
        var tabPanel2;
        var tabPanel3;
        var tabPanel4;
        var button;
        var result;
        
        // Register the tests
        function registerTests(harness)
        {
            var test;
            testHarness = harness;
            
            tabContainer1 = testHarness.getObject('<%=TabContainer1.ClientID%>');
            tabPanel1 = testHarness.getObject('<%=TabPanel1.ClientID%>');
            tabPanel2 = testHarness.getObject('<%=TabPanel2.ClientID%>');
            tabPanel3 = testHarness.getObject('<%=TabPanel3.ClientID%>');
            tabPanel4 = testHarness.getObject('<%=TabPanel4.ClientID%>');
            button = testHarness.getElement('<%=Button1.ClientID%>');

            tabPanel4.add_populated(function() { result = tabPanel4.get_element().innerHTML; });
            
            test = testHarness.addTest("Activate a visible TabPanel");
            test.addStep(function() { testHarness.fireEvent(tabPanel3.get_headerTab(), "onclick"); });
            test.addStep(function() { testHarness.assertTrue(tabPanel3._get_active(), "Panel was not activated"); });
            
            test = testHarness.addTest("Show/Hide a hidden TabPanel");
            test.addStep(function() { testHarness.assertTrue(tabPanel2._tab.style.display == "none", "Panel was not hidden"); });
            test.addStep(function() { tabPanel2.set_enabled(true); });
            test.addStep(function() { testHarness.assertTrue(tabPanel2._tab.style.display != "none", "Panel was not made visible"); });
            
            test = testHarness.addTest("Activate a hidden TabPanel");
            test.addStep(function() { testHarness.assertTrue(tabPanel2._tab.style.display == "none", "Panel was not hidden"); });
            test.addStep(function() { tabContainer1.set_activeTab(tabPanel2); });
            test.addStep(function() { testHarness.assertTrue(tabPanel2._tab.style.display != "none", "Panel was not made visible"); });
            test.addStep(function() { testHarness.assertTrue(tabPanel2._get_active(), "Panel was not made active"); });
            
            test = testHarness.addTest("Preserve changes after postback");
            test.addStep(function() { tabPanel2.set_enabled(true); });
            test.addStep(function() { tabPanel3.set_enabled(false); });
            test.addStep(function() { tabContainer1.set_activeTabIndex(1); });
            test.addPostBack(button);
            test.addStep(function() { testHarness.assertEqual(tabContainer1.get_activeTabIndex(), 1, "Did not persist active tab index"); });
            test.addStep(function() { testHarness.assertEqual(tabPanel2.get_enabled(), true); });
            test.addStep(function() { testHarness.assertEqual(tabPanel3.get_enabled(), false); });
            
            test = testHarness.addTest("Dynamic Population");
            test.addStep(function() { testHarness.fireEvent(tabPanel4.get_headerTab(), "onclick"); },
                function() { return result; },
                function() { testHarness.assertEqual(result, "Web Service Success", "Web service call failed: " + result);});
            
        }
    </script>
    
</asp:Content>

