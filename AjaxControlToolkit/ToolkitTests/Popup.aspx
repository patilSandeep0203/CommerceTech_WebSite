<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Popup.aspx.cs" Inherits="Popup" MasterPageFile="~/Default.master" %>

<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:TextBox ID="T" runat="server" />
    <asp:Panel ID="P" runat="server" BorderStyle="Solid" BorderWidth="1" BorderColor="Black">
        Foo
    </asp:Panel>
    <ajaxToolkit:PopupExtender runat="Server" ID="PE" TargetControlID="P" ParentElementID="T" />


    <script type="text/javascript">
        // (c) Copyright Microsoft Corporation.
        // This source is subject to the Microsoft Permissive License.
        // See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
        // All other rights reserved.
    
        // Script objects that should be loaded before we run
        var typeDependencies = ['AjaxControlToolkit.PopupBehavior'];
    
        // TestRunner
        var testHarness = null;

        // Controls in the test page
        var textbox = null;
        var popup = null;

        // Ensure the popup is not displayed
        function checkHidden() {
            testHarness.assertEqual(popup.style.display, 'none', 'Popup should be hidden');
        }

        // Ensure the popup is displayed
        function checkVisible() {
            testHarness.assertEqual(popup.style.display, '', 'Popup should be visible');
        }

        function showPopup() {
            popup._behaviors[0].show();
        }
        
        function hidePopup() {
            popup._behaviors[0].hide();
        }
        
        // Register the tests
        function registerTests(harness)
        {
            testHarness = harness;

            // Get the controls on the page
            textbox = testHarness.getElement('ctl00_ContentPlaceHolder1_T');
            popup = testHarness.getElement('ctl00_ContentPlaceHolder1_P');
            
            var test = testHarness.addTest('Initial state');
            test.addStep(checkHidden);
            
            test = testHarness.addTest('Show');
            test.addStep(checkHidden);
            test.addStep(showPopup);
            test.addStep(checkVisible);

            test = testHarness.addTest('Hide');
            test.addStep(checkHidden);
            test.addStep(showPopup);
            test.addStep(checkVisible);
            test.addStep(hidePopup);
            test.addStep(checkHidden);
        }
    </script>
</asp:Content>