<%@ Page Language="C#" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Calendar Manual Test: Work Item 7317 - Calendar Extender does not close properly in FireFox</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager EnableScriptLocalization="true" EnableScriptGlobalization="true" runat="server" />
        <asp:TextBox runat="server" ID="Text" />
        <ajaxToolkit:CalendarExtender runat="server" TargetControlID="Text" />        
        
        <b>Description:</b>
        <p>
            The Calendar is not properly closing in FireFox after a series of specific events occur.  To reproduce this scenario follow the steps below and verify the results:
        </p>
        <ol>
            <li>Set focus to Textbox</li>
            <li>Click on a day in the month</li>
            <li>Click on the body of the document   
                <ul>
                    <li>FAIL if the the calendar is still visible.</li>
                    <li>Other observed effects:
                        <ul>
                            <li>When setting focus away, the calendar seems to animate as if the month was changed by clicking an arrow</li>
                        </ul>
                    </li>
                </ul>
            </li>
        </ol>
    </div>
    </form>
</body>
</html>
