<%@ Page
    Language="C#"
    MasterPageFile="~/Default.master"
    CodeFile="AutoComplete.aspx.cs"
    Inherits="Automated_AutoComplete"
    Title="TextBoxWatermark Tests"
    %>
    
    <script runat="server">
        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public static string[] GetCompletionList(string prefixText, int count)
        {
            if (count == 0)
            {
                count = 10;
            }

            if (prefixText.Equals("xyz"))
            {
                return new string[0];
            }

            ArrayList items = new ArrayList(count);
            Random random = new Random();
            for (int i = 0; i < count; i++)
            {
                char c1 = (char)random.Next(33, 127);
                char c2 = (char)random.Next(33, 127);
                char c3 = (char)random.Next(33, 127);

                items.Add(prefixText + c1 + c2 + c3);
            }

            return (string[])items.ToArray(typeof(string));
        }
    </script>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:TextBox ID="TextBox1" runat="server" ></asp:TextBox>
    <div id="listID1"></div>
    <ajaxToolkit:AutoCompleteExtender 
        ID="AutoCompleteExtender1" 
        runat="server" 
        TargetControlID="TextBox1"
        ServiceMethod="GetCompletionList"
        ServicePath="ToolkitTestService.asmx"
        MinimumPrefixLength="3" 
        EnableCaching="true"
        CompletionSetCount="5" 
        CompletionInterval="1000" 
        BehaviorID="autoCompleteBehavior1"
        /> 
    <br /><br />    
    <asp:TextBox ID="TextBox2" runat="server" ></asp:TextBox>
    <ajaxToolkit:AutoCompleteExtender 
        ID="AutoCompleteExtender2" 
        runat="server" 
        TargetControlID="TextBox2"
        ServiceMethod="GetCompletionList"
        MinimumPrefixLength="2" 
        EnableCaching="false"
        CompletionSetCount="10" 
        CompletionInterval="2000" 
        BehaviorID="autoCompleteBehavior2"
        /> 

    <script type="text/javascript">
        // (c) Copyright Microsoft Corporation.
        // This source is subject to the Microsoft Permissive License.
        // See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
        // All other rights reserved.

        // Script objects that should be loaded before we run
        var typeDependencies = ['AjaxControlToolkit.AutoCompleteBehavior'];

        // Test Harness
        var testHarness = null;

        // Controls in the page
        var tb1 = null;
        var autoCompleteBehavior1 = null;
        
        var tb2 = null;
        var autoCompleteBehavior2 = null;
        
        // Ensure the textbox is in its empty state
        function checkEmptyState(autoCompleteList, textBox) {
            return function(){
                testHarness.fireEvent(textBox, 'onfocus');
                testHarness.assertEqual('', textBox.value, "TextBox's  text should be '' instead of '" + textBox.value + "'");
                var children = autoCompleteList.childNodes;            
                testHarness.assertEqual(children.length, 0, "Div should be have no content when textbox is empty");
            };
        }

        // Test the initial state of the control
        function testInitialState(autoCompleteList, textBox) {
            return function(){
                checkEmptyState(autoCompleteList, textBox);
            };
        }
        
        // reset content in textbox
        function resetTextBox(textBox) {
            return function(){
                textBox.value = '';
            };
        }
        
        // Test adding text focus from full control
        function setText(text, textBox) {
            return function(){
                textBox.value = text; 
                testHarness.fireEvent(textBox, 'onfocus');
            };
         }
        
        function checkAutoComplete(numMatches, autoCompleteList){
            return function(){
                var children = autoCompleteList.childNodes; 
                return (children.length == numMatches);
            };
        }
        
        function verifyAutoCompleteBehavior(text, numMatches, autoCompleteList)
        {       
            return function(){
                var children = autoCompleteList.childNodes; 
                testHarness.assertEqual(children.length, numMatches, "Div should have: " + numMatches + " when there are " + text.length + " characters in the textbox; Actual matches: " + children.length);
                if(children.length > 0)
                {
                    for (var i = 0; i < numMatches; i++) {
                        var child = children[i];
                        var match = child.innerHTML;
                        testHarness.assertEqual(match.indexOf(text), 0, "Every word should start with text: " + text + " Actual: " + match); 
                    }
                }
            };
        }
       
        // Register the tests
        function registerTests(harness)
        {
            testHarness = harness;
 
            // Get the first text box from the page
            tb1 = testHarness.getElement('ctl00_ContentPlaceHolder1_TextBox1');
            autoCompleteBehavior1 = testHarness.getObject('autoCompleteBehavior1');
                                   
            var test = testHarness.addTest('Initial1');
            test.addStep(testInitialState(autoCompleteBehavior1._completionListElement, tb1));
                     
            // addStep(Action, CheckComplete, Interval)   
            var test = testHarness.addTest('TextBox1_CharsLessThanLimit');
            test.addStep(setText('ab', tb1), checkAutoComplete(0, autoCompleteBehavior1._completionListElement), 50, 2000, verifyAutoCompleteBehavior('ab', 0, autoCompleteBehavior1._completionListElement));
            
            var test = testHarness.addTest('TextBox1_Reset1');
            test.addStep(resetTextBox(tb1));
            test.addStep(testInitialState(autoCompleteBehavior1._completionListElement, tb1));
                        
            var test = testHarness.addTest('TextBox1_CharsEqualToLimit')
            test.addStep(setText('abc', tb1), checkAutoComplete(5, autoCompleteBehavior1._completionListElement) , 50, 2000, verifyAutoCompleteBehavior('abc', 5, autoCompleteBehavior1._completionListElement));
                            
            var test = testHarness.addTest('TextBox1_Reset2');
            test.addStep(resetTextBox(tb1));
            test.addStep(testInitialState(autoCompleteBehavior1._completionListElement, tb1));            
            
            var test = testHarness.addTest('TextBox1_CharsGreaterThanLimit');
            test.addStep(setText('abcd', tb1), checkAutoComplete(5, autoCompleteBehavior1._completionListElement) , 50, 2000, verifyAutoCompleteBehavior('abcd', 5, autoCompleteBehavior1._completionListElement));
            
            var test = testHarness.addTest('TextBox1_Reset3');
            test.addStep(resetTextBox(tb1));
            test.addStep(testInitialState(autoCompleteBehavior1._completionListElement, tb1)); 
            
            // Get the second text box from the page
            tb2 = testHarness.getElement('ctl00_ContentPlaceHolder1_TextBox2');
            autoCompleteBehavior2 = testHarness.getObject('autoCompleteBehavior2');
                                   
            var test = testHarness.addTest('Initial2');
            test.addStep(testInitialState(autoCompleteBehavior2._completionListElement, tb2));
                     
            // addStep(Action, CheckComplete, Interval)   
            var test = testHarness.addTest('TextBox2_CharsLessThanLimit');
            test.addStep(setText('a', tb2), checkAutoComplete(0, autoCompleteBehavior2._completionListElement), 50, 3000, verifyAutoCompleteBehavior('a', 0, autoCompleteBehavior2._completionListElement));
            
            var test = testHarness.addTest('TextBox2_Reset1');
            test.addStep(resetTextBox(tb2));
            test.addStep(testInitialState(autoCompleteBehavior2._completionListElement, tb2));
                        
            var test = testHarness.addTest('TextBox2_CharsEqualToLimit')
            test.addStep(setText('ab', tb2), checkAutoComplete(10, autoCompleteBehavior2._completionListElement) , 50, 3000, verifyAutoCompleteBehavior('ab', 10, autoCompleteBehavior2._completionListElement));
                            
            var test = testHarness.addTest('TextBox2_Reset2');
            test.addStep(resetTextBox(tb2));
            test.addStep(testInitialState(autoCompleteBehavior2._completionListElement, tb2));            
            
            var test = testHarness.addTest('TextBox2_CharsGreaterThanLimit');
            test.addStep(setText('abcd', tb2), checkAutoComplete(10, autoCompleteBehavior2._completionListElement) , 50, 3000, verifyAutoCompleteBehavior('abcd', 10, autoCompleteBehavior2._completionListElement));
            
            var test = testHarness.addTest('TextBox2_Reset3');
            test.addStep(resetTextBox(tb2));
            test.addStep(testInitialState(autoCompleteBehavior2._completionListElement, tb2)); 
            
        }
    </script>
</asp:Content>
