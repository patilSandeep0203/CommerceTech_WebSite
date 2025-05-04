<%@ Page
    Language="C#"
    MasterPageFile="~/Default.master"
    AutoEventWireup="true"
    CodeFile="DataBinding.aspx.cs"
    Inherits="DataBinding"
    Title="DataBinding Tests" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:TextBox ID="TextBox" runat="server" />
    <br />

    <ajaxToolkit:TextBoxWatermarkExtender runat="server" TargetControlID="TextBox" WatermarkText='<%# "WatermarkText" %>' />

    <asp:Repeater ID="Repeater" runat="server">
        <ItemTemplate>

            <asp:TextBox ID="TextBox" runat="server" />
            <br />

            <ajaxToolkit:TextBoxWatermarkExtender runat="server" TargetControlID="TextBox" WatermarkText='<%# Eval("WatermarkText") %>'/>

        </ItemTemplate>
    </asp:Repeater>

    <script type="text/javascript">
    // (c) Copyright Microsoft Corporation.
    // This source is subject to the Microsoft Permissive License.
    // See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
    // All other rights reserved.

    // Script objects that should be loaded before we run
    var typeDependencies = ['AjaxControlToolkit.TextBoxWatermarkBehavior'];

     // Test Harness
    var testHarness = null;

    // Variables
    var tb = null;
    var tb0 = null;
    var tb1 = null;

    // Functions

    function VerifyDataBinding(t, v) {
        return function() {
            testHarness.assertTrue(v == t.value, "DataBinding unsuccessful for "+t.id);
        }
    }

    // Register the tests
    function registerTests(harness) {
        testHarness = harness;
        var test;

        tb = testHarness.getElement("ctl00_ContentPlaceHolder1_TextBox");
        tb0 = testHarness.getElement("ctl00_ContentPlaceHolder1_Repeater_ctl00_TextBox");
        tb1 = testHarness.getElement("ctl00_ContentPlaceHolder1_Repeater_ctl01_TextBox");

        test = testHarness.addTest("Simple DataBinding");
        test.addStep(VerifyDataBinding(tb, "WatermarkText"));

        test = testHarness.addTest("Eval DataBinding");
        test.addStep(VerifyDataBinding(tb0, "WatermarkText0"));
        test.addStep(VerifyDataBinding(tb1, "WatermarkText1"));
    }

    </script>

</asp:Content>
