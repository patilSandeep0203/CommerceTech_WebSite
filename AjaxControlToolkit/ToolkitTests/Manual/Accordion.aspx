<%@ Page Language="C#" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Growing Accordion Manual Test</title>
    <link href="../Default.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
		function grow(e) {
			if (e.className == 'accordionContent') {
				var d = document.createElement('div');
				d.style.color = 'red';
				d.innerHTML = 'More content<br />More content<br />More content<br />More content<br />More content<br />More content';
				e.appendChild(d);
			}
			if (e.childNodes) {
				for (var i = 0; i < e.childNodes.length; i++) {
					grow(e.childNodes[i]);
				}
			}
		}

		function shrink(e) {
			if (e.className == 'accordionContent') {
				e.innerHTML = 'A little content';
			}
			if (e.childNodes) {
				for (var i = 0; i < e.childNodes.length; i++) {
					shrink(e.childNodes[i]);
				}
			}
		}
	</script>
</head>
<body><form id="Form1" runat="server"><div>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" />
    
    1. For accessibility, verify you can't tab into controls in hidden panes:<br />
    <ajaxToolkit:Accordion ID="Accordion3" runat="server" SelectedIndex="0"
        HeaderCssClass="accordionHeader" ContentCssClass="accordionContent"
        FadeTransitions="true" FramesPerSecond="40" TransitionDuration="100"
        SuppressHeaderPostbacks="true">
        <Panes>
            <ajaxToolkit:AccordionPane ID="AccordionPane10" runat="server">
                <Header><a href="#" style="color: White;">Header</a></Header>
                <Content>
                    Sample content with buttons:
                    <asp:Button ID="Button1" runat="server" Text="Button" />
                    <asp:Button ID="Button2" runat="server" Text="Button" />
                </Content>
            </ajaxToolkit:AccordionPane>
            <ajaxToolkit:AccordionPane ID="AccordionPane11" runat="server">
                <Header><a href="#" style="color: White;">Header</a></Header>
                <Content>
                    Sample content with buttons:
                    <asp:Button ID="Button3" runat="server" Text="Button" />
                    <asp:Button ID="Button4" runat="server" Text="Button" />
                </Content>
            </ajaxToolkit:AccordionPane>
            <ajaxToolkit:AccordionPane ID="AccordionPane12" runat="server">
                <Header><a href="#" style="color: White;">Header</a></Header>
                <Content>
                    Sample content with buttons:
                    <asp:Button ID="Button5" runat="server" Text="Button" />
                    <asp:Button ID="Button6" runat="server" Text="Button" />
                </Content>
            </ajaxToolkit:AccordionPane>        
        </Panes>
    </ajaxToolkit:Accordion>
    <br />
    <br />
    
    2. Resize the browser window and verify that databound controls do not disappear in IE6:<br />
    <asp:XmlDataSource ID="data" runat="server" DataFile="~/App_Data/CarsService.xml" XPath="/CarsService/make" />
    <ajaxToolkit:Accordion id="arrayBound2" runat="server" AutoSize="Fill" Height="200px" DataSourceID="data"
        HeaderCssClass="accordionHeader" ContentCssClass="accordionContent">
        <HeaderTemplate>Header</HeaderTemplate>
        <ContentTemplate>
            Data: <%# Eval("name")%> 
            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Eval("name") %>'></asp:TextBox>
            <asp:Button ID="Button2" runat="server" Text='<%# Eval("name") %>'></asp:Button>
        </ContentTemplate>
    </ajaxToolkit:Accordion>
    <br />
    <br />
    
    3. Make the accordion content <a href="#" onclick="grow(document.body); return false;" href="#">grow</a> or
    <a href="#" onclick="shrink(document.body); return false;">shrink</a> and verify the new content is still accessible:
    <br />
    AutoSize = "None":<br />
    <div style="border-bottom: solid 3px red;">
        <ajaxToolkit:Accordion ID="AccordionNone" runat="server" AutoSize="None"
            SelectedIndex="0" HeaderCssClass="accordionHeader" ContentCssClass="accordionContent"
            FadeTransitions="true" FramesPerSecond="40" TransitionDuration="100">
            <Panes>
                <ajaxToolkit:AccordionPane ID="AccordionPane1" runat="server">
                    <Header>Header 1</Header>
                    <Content>
                        A little content
                    </Content>
                </ajaxToolkit:AccordionPane>
                <ajaxToolkit:AccordionPane ID="AccordionPane2" runat="server">
                    <Header>Header 2</Header>
                    <Content>
                        A little content
                    </Content>
                </ajaxToolkit:AccordionPane>
                <ajaxToolkit:AccordionPane ID="AccordionPane3" runat="server">
                    <Header>Header 3</Header>
                    <Content>
                        A little content
                    </Content>
                </ajaxToolkit:AccordionPane>
            </Panes>
        </ajaxToolkit:Accordion>
    </div>
    <br />
    <br />

    AutoSize = "Limit":<br />
    <div style="border-bottom: solid 3px red;">
        <ajaxToolkit:Accordion ID="AccordionLimit" runat="server" AutoSize="Limit" Height="250px" 
            SelectedIndex="0" HeaderCssClass="accordionHeader" ContentCssClass="accordionContent"
            FadeTransitions="true" FramesPerSecond="40" TransitionDuration="100">
            <Panes>
                <ajaxToolkit:AccordionPane ID="AccordionPane4" runat="server">
                    <Header>Header 1</Header>
                    <Content>
                        A little content
                    </Content>
                </ajaxToolkit:AccordionPane>
                <ajaxToolkit:AccordionPane ID="AccordionPane5" runat="server">
                    <Header>Header 2</Header>
                    <Content>
                        A little content
                    </Content>
                </ajaxToolkit:AccordionPane>
                <ajaxToolkit:AccordionPane ID="AccordionPane6" runat="server">
                    <Header>Header 3</Header>
                    <Content>
                        A little content
                    </Content>
                </ajaxToolkit:AccordionPane>
            </Panes>
        </ajaxToolkit:Accordion>
    </div>
    <br />
    <br />
    
    AutoSize = "Fill":<br />
    <div style="border-bottom: solid 3px red;">
        <ajaxToolkit:Accordion ID="AccordionFill" runat="server" AutoSize="Fill" Height="250px" 
            SelectedIndex="0" HeaderCssClass="accordionHeader" ContentCssClass="accordionContent"
            FadeTransitions="true" FramesPerSecond="40" TransitionDuration="100">
            <Panes>
                <ajaxToolkit:AccordionPane ID="AccordionPane7" runat="server">
                    <Header>Header 1</Header>
                    <Content>
                        A little content
                    </Content>
                </ajaxToolkit:AccordionPane>
                <ajaxToolkit:AccordionPane ID="AccordionPane8" runat="server">
                    <Header>Header 2</Header>
                    <Content>
                        A little content
                    </Content>
                </ajaxToolkit:AccordionPane>
                <ajaxToolkit:AccordionPane ID="AccordionPane9" runat="server">
                    <Header>Header 3</Header>
                    <Content>
                        A little content
                    </Content>
                </ajaxToolkit:AccordionPane>
            </Panes>
        </ajaxToolkit:Accordion>
    </div>
    <br />
    <br />
</div></form></body>
</html>