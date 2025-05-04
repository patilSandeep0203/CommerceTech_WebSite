<%@ Page
    Language="C#"
    CodeFile="CollapsiblePanel.aspx.cs"
    Inherits="Automated_CollapsiblePanel"
    Title="CollapsiblePanel Tests"
    MasterPageFile="~/Default.master" %>

<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:Panel ID="Panel" runat="server">
        <div id="Header">Header - <asp:Label ID="TextLabel" runat="server">Initial content</asp:Label></div>
        <div id="panelParent" style="border:2px red outset;">
        <asp:Panel ID="ContentPanel" runat="server">
            <p>This is sample content.<br />It spans a couple of lines.</p>
        </asp:Panel>
        </div>
    </asp:Panel>

     <asp:Panel ID="HPanel" runat="server">
        <div id="Div1">Header - <asp:Label ID="HTextLabel" runat="server">Initial content</asp:Label></div>
        <span id="hpanelParent" style="border:2px red outset;">
        <asp:Panel ID="HContentPanel" runat="server">
            <p>This is sample content.<br />It spans a couple of lines.</p>
        </asp:Panel>
        </span>
    </asp:Panel>

    <ajaxToolkit:CollapsiblePanelExtender ID="cpp" runat="Server"
            TargetControlID="ContentPanel"
            ExpandControlID="Panel"
            CollapseControlID="Panel"
            ScrollContents="False"
            Collapsed="False"
            ExpandDirection="Vertical"
            SuppressPostBack="true"
            TextLabelID="TextLabel"
            CollapsedText="Collapsed"
            ExpandedText="Opened" />
    <ajaxToolkit:CollapsiblePanelExtender ID="cpe" runat="Server"
            TargetControlID="HContentPanel"
            ExpandControlID="HPanel"
            CollapseControlID="HPanel"
            Collapsed="False"
            ExpandDirection="Horizontal"
            ExpandedSize="600"
            SuppressPostBack="true"
            TextLabelID="HTextLabel"
            CollapsedText="Collapsed"
            ExpandedText="Opened" />

    <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />

    <script type="text/javascript">
        // (c) Copyright Microsoft Corporation.
        // This source is subject to the Microsoft Permissive License.
        // See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
        // All other rights reserved.
    
        // Script objects that should be loaded before we run
        var typeDependencies = ['AjaxControlToolkit.CollapsiblePanelBehavior'];
    
        // Test Harness
        var testHarness = null;

        // Controls in the test page
        var label = null;
        var hlabel= null;
        var panel = null;
        var hpanel = null;
        var panelParent = null;
        var hpanelParent = null;
        var btn = null;
        var behavior = null;

        var collapseComplete = false;
        var expandComplete = false;

        // Check if the content is collapsed
        function checkCollapsedV() {
            // sometimes this comes back 3 when the content is, in fact, collapsed.
            testHarness.assertTrue(panelParent.offsetHeight <= 5, "Content should be vertically collapsed, it's " + panelParent.offsetHeight + 'px');
            testHarness.assertEqual(label.innerHTML, 'Collapsed', "TextLabel should display 'Collapsed'");
        }

        // Check if the content is expanded
        function checkExpandedV() {
            testHarness.assertNotEqual(panelParent.offsetHeight, 0, "Content should be vertically expanded, it's " + panelParent.offsetHeight + 'px');
            testHarness.assertEqual(label.innerHTML, 'Opened', "TextLabel should display 'Opened'");
        }

          // Check if the content is collapsed
        function checkCollapsedH() {
            // sometimes this comes back 3 when the content is, in fact, collapsed.
            testHarness.assertTrue(hpanelParent.offsetWidth <= 5, "Content should be horizontally collapsed, it's " + hpanelParent.offsetWidth + 'px');
            testHarness.assertEqual(label.innerHTML, 'Collapsed', "TextLabel should display 'Collapsed'");
        }

        // Check if the content is expanded
        function checkExpandedH() {
            testHarness.assertNotEqual(hpanelParent.offsetWidth, 0, "Content should be horizontially expanded, it's " + hpanelParent.offsetWidth + 'px');
            testHarness.assertEqual(label.innerHTML, 'Opened', "TextLabel should display 'Opened'");
        }

        function checkBothExpanded() {
            checkExpandedV();
            checkExpandedH();
        }
        
        function checkBothCollapsed() {
            checkCollapsedV();
            checkCollapsedH();
        }
        
        function clickBoth() {
            testHarness.fireEvent(panel, 'onclick');
            testHarness.fireEvent(hpanel, 'onclick');
        }
        
        function waitCollapsed() {
            try { 
                checkBothCollapsed();
                return true;
            } catch (ex) {
                return false;
            }
        }
        
        function waitExpanded() {
            try { 
                checkBothExpanded();
                return true;
            } catch (ex) {
                return false;
            }
        }

        // Register the tests
        function registerTests(harness) {
            testHarness = harness;
            
            var waitLength = 150;
            

            // Get the controls on the page
            panel = testHarness.getElement('ctl00_ContentPlaceHolder1_Panel');
            hpanel = testHarness.getElement('ctl00_ContentPlaceHolder1_HPanel');
            label = testHarness.getElement('ctl00_ContentPlaceHolder1_TextLabel');
            hlabel = testHarness.getElement('ctl00_ContentPlaceHolder1_HTextLabel');
            panelParent = testHarness.getElement('panelParent');
            btn = testHarness.getElement('ctl00_ContentPlaceHolder1_Button1');
            hpanelParent = testHarness.getElement('hpanelParent');
            behavior = testHarness.getObject('ctl00_ContentPlaceHolder1_cpp');
            
            var test = testHarness.addTest('Initial State');
            test.addStep(checkBothExpanded);
            
            test = testHarness.addTest('Collapse');
            test.addStep(checkBothExpanded);
            test.addStep(clickBoth, waitCollapsed, waitLength, 5000);
                        
            test = testHarness.addTest('Expand');
            test.addStep(checkBothExpanded);
            test.addStep(clickBoth, waitCollapsed, waitLength, 5000);
            test.addStep(clickBoth, waitExpanded, waitLength, 5000);
            
            test = testHarness.addTest('Collapse Again');
            test.addStep(checkBothExpanded);
            test.addStep(clickBoth, waitCollapsed, waitLength, 5000);
            test.addStep(clickBoth, waitExpanded, waitLength, 5000);
            test.addStep(clickBoth, waitCollapsed, waitLength, 5000);
            
            test = testHarness.addTest('Collapsed preserved on PostBack');
            test.addStep(checkBothExpanded);
            test.addStep(clickBoth, waitCollapsed, waitLength, 5000);
            test.addPostBack(btn);
            test.addStep(checkBothCollapsed);
            
            test = testHarness.addTest('Expanded preserved on PostBack');
            test.addStep(checkBothExpanded);
            test.addStep(clickBoth, waitCollapsed, waitLength, 5000);
            test.addStep(clickBoth, waitExpanded, waitLength, 5000);
            test.addPostBack(btn);
            test.addStep(checkBothExpanded);
                       
            test = testHarness.addTest('Behavior Collapse');
            test.addStep(checkBothExpanded);
            test.addStep(function() { behavior.set_SuppressPostBack.call(behavior, false); });  // To avoid assignments to event.*
            test.addStep(function () { behavior._doClose.call(behavior); },
                function() { try { checkCollapsedV(); return true; } catch (ex) { return false; } }, waitLength, 5000);
            
            test = testHarness.addTest('Behavior Expand');
            test.addStep(checkBothExpanded);
            test.addStep(function() { behavior.set_SuppressPostBack.call(behavior, false); });  // To avoid assignments to event.*
            test.addStep(function () { behavior._doClose.call(behavior); },
                function() { try { checkCollapsedV(); return true; } catch (ex) { return false; } }, waitLength, 5000);
            test.addStep(function () { behavior._doOpen.call(behavior); },
                function() { try { checkExpandedV(); return true; } catch (ex) { return false; } }, waitLength, 5000);
        }
    </script>
</asp:Content>