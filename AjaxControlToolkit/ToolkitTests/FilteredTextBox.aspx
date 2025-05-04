<%@ Page 
    Language="C#" 
    MasterPageFile="~/Default.master" 
    AutoEventWireup="true" 
    CodeFile="FilteredTextBox.aspx.cs" 
    Inherits="FilteredTextBox" 
    Title="FilteredTextBox Tests" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"  
    TargetControlID="TextBox1"
    ValidChars="0" />
    
    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"   
    TargetControlID="TextBox2"
    FilterType="Numbers"/>
    
  <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"   
    TargetControlID="TextBox3"
    FilterType="UppercaseLetters" />
    
    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"  
    TargetControlID="TextBox4"
    FilterType="LowercaseLetters"/>
    
    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" 
    TargetControlID="TextBox5"
    ValidChars="$"
    FilterType="Custom,Numbers" />
    
Only zeroes are allowed: <asp:TextBox ID="TextBox1" runat="server" /><br />
Only numbers are allowed: <asp:TextBox ID="TextBox2" runat="server" /><br />
Only upper case letters are allowed: <asp:TextBox ID="TextBox3" runat="server" /><br />
Only lower case letters are allowed: <asp:TextBox ID="TextBox4" runat="server" /><br />
Only Number and custom: <asp:TextBox ID="TextBox5" runat="server" /><br />

    <script type="text/javascript">
        // (c) Copyright Microsoft Corporation.
        // This source is subject to the Microsoft Permissive License.
        // See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
        // All other rights reserved.
    
        // Script objects that should be loaded before we run
        var typeDependencies = ["AjaxControlToolkit.FilteredTextBoxBehavior"];
    
        // Test Harness
        var testHarness = null;

        // Control in the page
        var textbox1, textbox2, textbox3, textbox4, textbox5 = null;
        
        
        function fireKey(target, key) {
                ensureValid(); 
                                               
                if (target._processKey(key)) {                                                            
                    target.get_element().value = target.get_element().value + key;
                }                                
        }
      
        
        function checkText(target, text, expectedText) {
            target.get_element().value = "";
            
            for (var i = 0; i < text.length;i++) {                
                fireKey(target, text.charAt(i));
            }            
            testHarness.assertEqual(target.get_element().value, expectedText, "Value in " + target.get_id() + " should be '" + expectedText + "', not '" + target.get_element().value + "'");
        }
        
        function checkPaste(target, value, expectedText)
        {
            target.get_element().value = "";
            target.get_element().value = value; // triggers change
            target._onchange();
            
            testHarness.assertEqual(target.get_element().value, expectedText, "Value in " + target.get_id() + " should be '" + expectedText + "', not '" + target.get_element().value + "'");           
        }
                
        function ensureValid() {
            testHarness.assertNotNull(testHarness.getDocument().createEvent, 'Cannot run test - browser does not support document.createEvent (IE 6 does not)');
        }

        // Register the tests
        function registerTests(harness)
        {
            testHarness = harness;
            
            // Get the controls from the page
            textbox1 = testHarness.getObject("ctl00_ContentPlaceHolder1_FilteredTextBoxExtender1");
            textbox2 = testHarness.getObject("ctl00_ContentPlaceHolder1_FilteredTextBoxExtender2");
            textbox3 = testHarness.getObject("ctl00_ContentPlaceHolder1_FilteredTextBoxExtender3");
            textbox4 = testHarness.getObject("ctl00_ContentPlaceHolder1_FilteredTextBoxExtender4");
            textbox5 = testHarness.getObject("ctl00_ContentPlaceHolder1_FilteredTextBoxExtender5");
            
            // Check valid chars
            var test = testHarness.addTest('Type stuff');
            test.addStep(function(){checkText(textbox1, "0", "0");});
            test.addStep(function(){checkText(textbox1, "1", "");});           
            
            test = testHarness.addTest('Paste stuff');
            test.addStep(function(){checkPaste(textbox2, "123", "123");});
            test.addStep(function(){checkPaste(textbox2, "A12#DD{}#$3", "123");});
            test.addStep(function(){checkPaste(textbox2, "ABCD", "");});
            
            
            // Check post validation (e.g. when pasting invalid chars)
            test = testHarness.addTest('Test Uppercase');
            test.addStep(function(){checkText(textbox3, "ABCD", "ABCD");});
            test.addStep(function(){checkText(textbox3, "aBcD", "BD");});
            
            test = testHarness.addTest('Test Lowercase');
            test.addStep(function(){checkText(textbox4, "word", "word");});
            test.addStep(function(){checkText(textbox4, "BigBird", "igird");});
            
            test = testHarness.addTest('Test Custom');
            test.addStep(function(){checkText(textbox5, "abcd", "");});
            test.addStep(function(){checkText(textbox5, "$1234", "$1234");});
            
        }
    </script>

</asp:Content>

