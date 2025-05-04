<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="ProfileBinding.aspx.cs" Inherits="ProfileBinding" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div style="background-color:gold;width:500px;height:500px">
<%--
        <asp:Panel ID="Panel1" runat="server" style="background-color:purple;width:100px;height:100px">
            I move around.
        </asp:Panel>

        <asp:Button ID="Button1" runat="server" Text="I enjoy clicks" />

        <ajaxToolkit:DragPanelExtender ID="dpe1" runat="server" TargetControlID="Panel1" DragHandleID="Panel1">
                <ProfileBindings>
                    <ajaxToolkit:ProfilePropertyBinding ExtenderPropertyName="Location" ProfilePropertyName="dragLocation" />
                </ProfileBindings>
        </ajaxToolkit:DragPanelExtender>
--%>
    </div>
    
     <script type="text/javascript">
        // (c) Copyright Microsoft Corporation.
        // This source is subject to the Microsoft Permissive License.
        // See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
        // All other rights reserved.
        
        // Script objects that should be loaded before we run
        var testHarness = null;

        // Controls in the test page
        var panel = null;
        var behavior = null;
        
        var locationValue;

        // Register the tests
        function registerTests(harness) {
//            testHarness = harness;

//            // Get the controls on the page
//            panel = testHarness.getElement('ctl00_ContentPlaceHolder1_Panel1');
//            behavior = testHarness.getObject('dpe');            
//            
//            var locationValue = 123;
//            var test = testHarness.addTest('Load from profile');
//            
//            // wait a bit for the load to hapen
//            test.addStep(function(){}, 250);
//            
//            // check we're not at 0,0            
//            test.addStep(function() {
//                testHarness.assertTrue(panel.offsetLeft != 0 && panel.offsetRight != 0, "Failed to initialize values from profile");                
//                });           
//            
//            // change the position
//            test.addStep(function() {
//                behavior = testHarness.getObject('dpe');
//                behavior.set_location(locationValue + "," + locationValue );              
//            }, 500);    
//            
//            test = testHarness.addTest('Load from postback');
//            
//            test.addPostBack(testHarness.getElement('ctl00_ContentPlaceHolder1_Button1'));
//            
//            // wait a bit for the load to hapen
//            test.addStep(function(){}, 1000);
//            
//            
//            // check we're not at 0,0            
//            test.addStep(function() {                
//                testHarness.assertTrue(panel.offsetLeft == locationValue && panel.offsetTop == locationValue, "Failed to initialize values from profile");                
//                });           
//                
//            // move it again so the the value changes.
//            //            
//            test.addStep(function() {
//                behavior = testHarness.getObject('dpe');
//                behavior.set_location(25 + "," + 75);              
//            }, 500);    
        }
    </script>

</asp:Content>

