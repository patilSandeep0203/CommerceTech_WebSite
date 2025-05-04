<%@ Page Language="C#" MasterPageFile="site.master" AutoEventWireup="true" CodeFile="startGateway.aspx.cs"
    Inherits="QuestionsGateway" Theme="TextBoxSkin" Title="Commerce Technologies - OnlineApplication" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentHolder" runat="Server">

    <script type="text/javascript" language="javascript">
            function Cover(bottom, top, ignoreSize) {
                var location = Sys.UI.DomElement.getLocation(bottom);
                top.style.position = 'absolute';
                top.style.top = location.y + 'px';
                top.style.left = location.x + 'px';
                if (!ignoreSize) {
                    top.style.height = bottom.offsetHeight + 'px';
                    top.style.width = bottom.offsetWidth + 'px';
                }
            }
    </script>

    <div>
        <center>
            <table style="width: 550px;" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td align="center">
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Label ID="lblError" runat="server" Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <cc1:AnimationExtender ID="AnimationExtender1" runat="server" TargetControlID="lnkbtnViewUnAcc">
                                    <Animations>
                        <OnLoad><OpacityAction AnimationTarget="infoUnAcc" Opacity="0" /></OnLoad>
                        <OnClick>
                            <Sequence>                               
                                <ScriptAction Script="Cover($get('ctl00_MainContent_lnkbtnViewUnAcc'), $get('flyoutUnAcc'));" />
                                <StyleAction AnimationTarget="flyoutUnAcc" Attribute="display" Value="block"/>                            
                                <Parallel AnimationTarget="flyoutUnAcc" Duration=".3" Fps="25">
                                    <Move Horizontal="150" Vertical="0" />
                                    <Resize Width="300" Height="20" />
                                    <Color AnimationTarget="flyoutUnAcc" StartValue="#AAAAAA" EndValue="#f1fafa" Property="style" PropertyKey="backgroundColor" />                                
                                </Parallel>                            
                                <ScriptAction Script="Cover($get('flyoutUnAcc'), $get('infoUnAcc'), true);" />
                                <StyleAction AnimationTarget="infoUnAcc" Attribute="display" Value="block"/>
                                <FadeIn AnimationTarget="infoUnAcc" Duration=".2"/>                            
                                <StyleAction AnimationTarget="flyoutUnAcc" Attribute="display" Value="none"/>
                                <StyleAction AnimationTarget="infoUnAcc" Attribute="height" value="auto" />
                                <Parallel Duration=".5">
                                    <Color AnimationTarget="infoUnAcc" StartValue="#383838" EndValue="#383838" Property="style" PropertyKey="color" />
                                    <Color AnimationTarget="infoUnAcc" StartValue="#febd0d" EndValue="#383838" Property="style" PropertyKey="borderColor" />
                                </Parallel>
                                <Parallel Duration=".5">
                                    <Color AnimationTarget="infoUnAcc" StartValue="#383838" EndValue="#383838" Property="style" PropertyKey="color" />
                                    <Color AnimationTarget="infoUnAcc" StartValue="#febd0d" EndValue="#383838" Property="style" PropertyKey="borderColor" />
                                    <FadeIn AnimationTarget="btnCloseParentUnAcc" MaximumOpacity=".9" />                            
                                </Parallel>
                                
                            </Sequence>
                        </OnClick>
                                    </Animations>
                                </cc1:AnimationExtender>
                                <cc1:AnimationExtender ID="AnimationExtender2" runat="server" TargetControlID="btnCloseUnAcc">
                                    <Animations>
                        <OnClick>
                            <Sequence>
                                <StyleAction AnimationTarget="infoUnAcc" Attribute="overflow" Value="hidden"/>
                                <Parallel AnimationTarget="infoUnAcc" Duration=".3" Fps="15">
                                    <Scale ScaleFactor="0.05" Center="true" ScaleFont="true" FontUnit="px" />
                                    <FadeOut />
                                </Parallel>
                                <StyleAction AnimationTarget="infoUnAcc" Attribute="display" Value="none"/>
                                <StyleAction AnimationTarget="infoUnAcc" Attribute="width" Value="300px"/>
                                <StyleAction AnimationTarget="infoUnAcc" Attribute="height" Value=""/>
                                <StyleAction AnimationTarget="infoUnAcc" Attribute="fontSize" Value="12px"/>
                                <StyleAction AnimationTarget="btnCloseParentUnAcc" Attribute="opacity" value="0" />
                                <StyleAction AnimationTarget="btnCloseParentUnAcc" Attribute="filter" value="alpha(opacity=0)" />                        
                            </Sequence>
                        </OnClick>
                        <OnMouseOver>
                            <Color Duration=".2" StartValue="#FFFFFF" EndValue="#FF0000" Property="style" PropertyKey="color" />                            
                        </OnMouseOver>
                        <OnMouseOut>
                            <Color Duration=".2" EndValue="#FFFFFF" StartValue="#FF0000" Property="style" PropertyKey="color" />                            
                        </OnMouseOut>
                                    </Animations>
                                </cc1:AnimationExtender>
                                <br />
                                <span class="LabelsBody">How do you process or plan to process your credit cards?</span><br />
                                <div style="width: 45%" align="left">
                                    <asp:RadioButton runat="server" ID="rdbPG1a" Text="E-Commerce (Website or Shopping Cart)"
                                        GroupName="rbtnPG1" AutoPostBack="True" OnCheckedChanged="rdbPG_CheckedChanged" /><br />
                                    <asp:RadioButton runat="server" ID="rdbPG1b" Text="Wireless (Mobile Phone, Laptop or Tablet)"
                                        GroupName="rbtnPG1" AutoPostBack="True" OnCheckedChanged="rdbPG_CheckedChanged" /><br />
                                    <asp:RadioButton runat="server" ID="rdbPG1c" Text="Point-of-Sale (POS or Accounting Software)"
                                        GroupName="rbtnPG1" AutoPostBack="True" OnCheckedChanged="rdbPG_CheckedChanged" />
                                    <asp:RadioButton runat="server" ID="rdbPG1d" Text="Back Office (Virtual Terminal)"
                                        GroupName="rbtnPG1" AutoPostBack="True" OnCheckedChanged="rdbPG_CheckedChanged" />
                                </div>
                                <br /><br />
                                <asp:Panel ID="pnlPG2" runat="server" Width="80%" Visible="False">
                                    <span class="LabelsBody">Is your business listed in the following list? (If it is then Authorize.net will not be selected as your gateway.)</span><br />
                                    <asp:LinkButton runat="server" ID="lnkbtnViewUnAcc" CssClass="Link" OnClientClick="return false;">View List of Unacceptable Businesses</asp:LinkButton>
                                    <div id="flyoutUnAcc" style="z-index: 2; display: none; border: solid 1px #D0D0D0;
                                        background-color: #FFFFFF; overflow: hidden;">
                                        &nbsp;</div>
                                    <div id="infoUnAcc" style="z-index: 2; display: none; font-size: 12px; border: solid 1px #CCCCCC;
                                        background-color: #ffffff; width: 300px; padding: 5px; font-family: Arial; font-size: 10pt;">
                                        <div style="float: right; filter: alpha(opacity=0);" id="btnCloseParentUnAcc">
                                            <asp:LinkButton ID="btnCloseUnAcc" runat="server" OnClientClick="return false;" Text=""
                                                Style="text-align: right; border: outset thin white; padding: 5px; text-decoration: none;
                                                background-color: #666666; color: White; text-align: center; font-weight: bold;"
                                                ToolTip="Close">X</asp:LinkButton>
                                        </div>
                                        <b><span class="LabelsBody">Uncceptable Businesses</span></b>
                                        <div class="DivGreen">
                                            <span class="LabelsBodySmall">Adult related products and services</span>
                                            <br />
                                            <span class="LabelsBodySmall">Firearms </span>
                                            <br />
                                            <span class="LabelsBodySmall">Electronic cigarettes or any similar product</span>
                                            <br />                                            
                                        </div>
                                    </div>
                                    <br />
                                    <asp:RadioButton runat="server" ID="rdbPG2Y" Text="Yes" GroupName="rdbPG2" AutoPostBack="true"
                                        OnCheckedChanged="rdbPG_CheckedChanged" />
                                    <asp:RadioButton runat="server" ID="rdbPG2N" Text="No" GroupName="rdbPG2" AutoPostBack="true"
                                        OnCheckedChanged="rdbPG_CheckedChanged" />
                                </asp:Panel>
                                <br />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Button ID="btnStartOver" runat="server" Text="Start Over" OnClick="btnStart_Click" />
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                        <asp:Button ID="btnSkip" runat="server" Text="Skip" OnClick="btnSkip_Click" />
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                        <asp:Button ID="btnContinue" runat="server" Text="Continue" OnClick="btnContinue_Click" />
                    </td>
                </tr>
            </table>
        </center>
    </div>
</asp:Content>
