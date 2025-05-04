<%@ Page Language="C#" MasterPageFile="site.master" AutoEventWireup="true" CodeFile="startMCA.aspx.cs"
    Inherits="QuestionsMCA" Theme="TextBoxSkin" Title="Commerce Technologies - OnlineApplication" %>

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
                                                            <cc1:AnimationExtender ID="AnimationExtender5" runat="server" TargetControlID="lnkbtnViewUnAcc0">
                                    <Animations>
                        <OnLoad><OpacityAction AnimationTarget="infoUnAcc0" Opacity="0" /></OnLoad>
                        <OnClick>
                            <Sequence>                               
                                <ScriptAction Script="Cover($get('ctl00_MainContent_lnkbtnViewUnAcc'), $get('flyoutUnAcc'));" />
                                <StyleAction AnimationTarget="flyoutUnAcc0" Attribute="display" Value="block"/>                            
                                <Parallel AnimationTarget="flyoutUnAcc0" Duration=".3" Fps="25">
                                    <Move Horizontal="150" Vertical="0" />
                                    <Resize Width="300" Height="20" />
                                    <Color AnimationTarget="flyoutUnAcc0" StartValue="#AAAAAA" EndValue="#f1fafa" Property="style" PropertyKey="backgroundColor" />                                
                                </Parallel>                            
                                <ScriptAction Script="Cover($get('flyoutUnAcc0'), $get('infoUnAcc'), true);" />
                                <StyleAction AnimationTarget="infoUnAcc0" Attribute="display" Value="block"/>
                                <FadeIn AnimationTarget="infoUnAcc0" Duration=".2"/>                            
                                <StyleAction AnimationTarget="flyoutUnAcc0" Attribute="display" Value="none"/>
                                <StyleAction AnimationTarget="infoUnAcc0" Attribute="height" value="auto" />
                                <Parallel Duration=".5">
                                    <Color AnimationTarget="infoUnAcc0" StartValue="#383838" EndValue="#383838" Property="style" PropertyKey="color" />
                                    <Color AnimationTarget="infoUnAcc0" StartValue="#febd0d" EndValue="#383838" Property="style" PropertyKey="borderColor" />
                                </Parallel>
                                <Parallel Duration=".5">
                                    <Color AnimationTarget="infoUnAcc0" StartValue="#383838" EndValue="#383838" Property="style" PropertyKey="color" />
                                    <Color AnimationTarget="infoUnAcc0" StartValue="#febd0d" EndValue="#383838" Property="style" PropertyKey="borderColor" />
                                    <FadeIn AnimationTarget="btnCloseParentUnAcc0" MaximumOpacity=".9" />                            
                                </Parallel>
                                
                            </Sequence>
                        </OnClick>
                                    </Animations>
                                </cc1:AnimationExtender>
                                
                                <cc1:AnimationExtender ID="AnimationExtender6" runat="server" TargetControlID="btnCloseUnAcc0">
                                    <Animations>
                        <OnClick>
                            <Sequence>
                                <StyleAction AnimationTarget="infoUnAcc0" Attribute="overflow" Value="hidden"/>
                                <Parallel AnimationTarget="infoUnAcc0" Duration=".3" Fps="15">
                                    <Scale ScaleFactor="0.05" Center="true" ScaleFont="true" FontUnit="px" />
                                    <FadeOut />
                                </Parallel>
                                <StyleAction AnimationTarget="infoUnAcc0" Attribute="display" Value="none"/>
                                <StyleAction AnimationTarget="infoUnAcc0" Attribute="width" Value="300px"/>
                                <StyleAction AnimationTarget="infoUnAcc0" Attribute="height" Value=""/>
                                <StyleAction AnimationTarget="infoUnAcc0" Attribute="fontSize" Value="12px"/>
                                <StyleAction AnimationTarget="btnCloseParentUnAcc0" Attribute="opacity" value="0" />
                                <StyleAction AnimationTarget="btnCloseParentUnAcc0" Attribute="filter" value="alpha(opacity=0)" />                        
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
                                
                                <cc1:AnimationExtender ID="AnimationExtender3" runat="server" TargetControlID="lnkbtnViewUnAcc2">
                                    <Animations>
                        <OnLoad><OpacityAction AnimationTarget="infoUnAcc2" Opacity="0" /></OnLoad>
                        <OnClick>
                            <Sequence>                               
                                <ScriptAction Script="Cover($get('ctl00_MainContent_lnkbtnViewUnAcc'), $get('flyoutUnAcc2'));" />
                                <StyleAction AnimationTarget="flyoutUnAcc2" Attribute="display" Value="block"/>                            
                                <Parallel AnimationTarget="flyoutUnAcc2" Duration=".3" Fps="25">
                                    <Move Horizontal="150" Vertical="0" />
                                    <Resize Width="300" Height="20" />
                                    <Color AnimationTarget="flyoutUnAcc2" StartValue="#AAAAAA" EndValue="#f1fafa" Property="style" PropertyKey="backgroundColor" />                                
                                </Parallel>                            
                                <ScriptAction Script="Cover($get('flyoutUnAcc2'), $get('infoUnAcc2'), true);" />
                                <StyleAction AnimationTarget="infoUnAcc2" Attribute="display" Value="block"/>
                                <FadeIn AnimationTarget="infoUnAcc2" Duration=".2"/>                            
                                <StyleAction AnimationTarget="flyoutUnAcc2" Attribute="display" Value="none"/>
                                <StyleAction AnimationTarget="infoUnAcc2" Attribute="height" value="auto" />
                                <Parallel Duration=".5">
                                    <Color AnimationTarget="infoUnAcc2" StartValue="#383838" EndValue="#383838" Property="style" PropertyKey="color" />
                                    <Color AnimationTarget="infoUnAcc2" StartValue="#febd0d" EndValue="#383838" Property="style" PropertyKey="borderColor" />
                                </Parallel>
                                <Parallel Duration=".5">
                                    <Color AnimationTarget="infoUnAcc2" StartValue="#383838" EndValue="#383838" Property="style" PropertyKey="color" />
                                    <Color AnimationTarget="infoUnAcc2" StartValue="#febd0d" EndValue="#383838" Property="style" PropertyKey="borderColor" />
                                    <FadeIn AnimationTarget="btnCloseParentUnAcc2" MaximumOpacity=".9" />                            
                                </Parallel>
                                
                            </Sequence>
                        </OnClick>
                                    </Animations>
                                </cc1:AnimationExtender>
                                
                                <cc1:AnimationExtender ID="AnimationExtender4" runat="server" TargetControlID="btnCloseUnAcc2">
                                    <Animations>
                        <OnClick>
                            <Sequence>
                                <StyleAction AnimationTarget="infoUnAcc2" Attribute="overflow" Value="hidden"/>
                                <Parallel AnimationTarget="infoUnAcc2" Duration=".3" Fps="15">
                                    <Scale ScaleFactor="0.05" Center="true" ScaleFont="true" FontUnit="px" />
                                    <FadeOut />
                                </Parallel>
                                <StyleAction AnimationTarget="infoUnAcc2" Attribute="display" Value="none"/>
                                <StyleAction AnimationTarget="infoUnAcc2" Attribute="width" Value="300px"/>
                                <StyleAction AnimationTarget="infoUnAcc2" Attribute="height" Value=""/>
                                <StyleAction AnimationTarget="infoUnAcc2" Attribute="fontSize" Value="12px"/>
                                <StyleAction AnimationTarget="btnCloseParentUnAcc2" Attribute="opacity" value="0" />
                                <StyleAction AnimationTarget="btnCloseParentUnAcc2" Attribute="filter" value="alpha(opacity=0)" />                        
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
                                
                       <cc1:AnimationExtender ID="AnimationExtender7" runat="server" TargetControlID="lnkbtnViewUnAcc3">
                                    <Animations>
                        <OnLoad><OpacityAction AnimationTarget="infoUnAcc3" Opacity="0" /></OnLoad>
                        <OnClick>
                            <Sequence>                               
                                <ScriptAction Script="Cover($get('ctl00_MainContent_lnkbtnViewUnAcc'), $get('flyoutUnAcc3'));" />
                                <StyleAction AnimationTarget="flyoutUnAcc3" Attribute="display" Value="block"/>                            
                                <Parallel AnimationTarget="flyoutUnAcc3" Duration=".3" Fps="25">
                                    <Move Horizontal="150" Vertical="0" />
                                    <Resize Width="300" Height="20" />
                                    <Color AnimationTarget="flyoutUnAcc3" StartValue="#AAAAAA" EndValue="#f1fafa" Property="style" PropertyKey="backgroundColor" />                                
                                </Parallel>                            
                                <ScriptAction Script="Cover($get('flyoutUnAcc3'), $get('infoUnAcc3'), true);" />
                                <StyleAction AnimationTarget="infoUnAcc3" Attribute="display" Value="block"/>
                                <FadeIn AnimationTarget="infoUnAcc3" Duration=".2"/>                            
                                <StyleAction AnimationTarget="flyoutUnAcc3" Attribute="display" Value="none"/>
                                <StyleAction AnimationTarget="infoUnAcc3" Attribute="height" value="auto" />
                                <Parallel Duration=".5">
                                    <Color AnimationTarget="infoUnAcc3" StartValue="#383838" EndValue="#383838" Property="style" PropertyKey="color" />
                                    <Color AnimationTarget="infoUnAcc3" StartValue="#febd0d" EndValue="#383838" Property="style" PropertyKey="borderColor" />
                                </Parallel>
                                <Parallel Duration=".5">
                                    <Color AnimationTarget="infoUnAcc3" StartValue="#383838" EndValue="#383838" Property="style" PropertyKey="color" />
                                    <Color AnimationTarget="infoUnAcc3" StartValue="#febd0d" EndValue="#383838" Property="style" PropertyKey="borderColor" />
                                    <FadeIn AnimationTarget="btnCloseParentUnAcc3" MaximumOpacity=".9" />                            
                                </Parallel>
                                
                            </Sequence>
                        </OnClick>
                                    </Animations>
                                </cc1:AnimationExtender>
                                
                       <cc1:AnimationExtender ID="AnimationExtender8" runat="server" TargetControlID="btnCloseUnAcc3">
                                    <Animations>
                        <OnClick>
                            <Sequence>
                                <StyleAction AnimationTarget="infoUnAcc3" Attribute="overflow" Value="hidden"/>
                                <Parallel AnimationTarget="infoUnAcc3" Duration=".3" Fps="15">
                                    <Scale ScaleFactor="0.05" Center="true" ScaleFont="true" FontUnit="px" />
                                    <FadeOut />
                                </Parallel>
                                <StyleAction AnimationTarget="infoUnAcc3" Attribute="display" Value="none"/>
                                <StyleAction AnimationTarget="infoUnAcc3" Attribute="width" Value="300px"/>
                                <StyleAction AnimationTarget="infoUnAcc3" Attribute="height" Value=""/>
                                <StyleAction AnimationTarget="infoUnAcc3" Attribute="fontSize" Value="12px"/>
                                <StyleAction AnimationTarget="btnCloseParentUnAcc3" Attribute="opacity" value="0" />
                                <StyleAction AnimationTarget="btnCloseParentUnAcc3" Attribute="filter" value="alpha(opacity=0)" />                        
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
                                
                        <cc1:AnimationExtender ID="AnimationExtender9" runat="server" TargetControlID="lnkbtnViewUnAcc4">
                                    <Animations>
                        <OnLoad><OpacityAction AnimationTarget="infoUnAcc4" Opacity="0" /></OnLoad>
                        <OnClick>
                            <Sequence>                               
                                <ScriptAction Script="Cover($get('ctl00_MainContent_lnkbtnViewUnAcc'), $get('flyoutUnAcc4'));" />
                                <StyleAction AnimationTarget="flyoutUnAcc4" Attribute="display" Value="block"/>                            
                                <Parallel AnimationTarget="flyoutUnAcc4" Duration=".3" Fps="25">
                                    <Move Horizontal="150" Vertical="0" />
                                    <Resize Width="300" Height="20" />
                                    <Color AnimationTarget="flyoutUnAcc4" StartValue="#AAAAAA" EndValue="#f1fafa" Property="style" PropertyKey="backgroundColor" />                                
                                </Parallel>                            
                                <ScriptAction Script="Cover($get('flyoutUnAcc4'), $get('infoUnAcc4'), true);" />
                                <StyleAction AnimationTarget="infoUnAcc4" Attribute="display" Value="block"/>
                                <FadeIn AnimationTarget="infoUnAcc4" Duration=".2"/>                            
                                <StyleAction AnimationTarget="flyoutUnAcc4" Attribute="display" Value="none"/>
                                <StyleAction AnimationTarget="infoUnAcc4" Attribute="height" value="auto" />
                                <Parallel Duration=".5">
                                    <Color AnimationTarget="infoUnAcc4" StartValue="#383838" EndValue="#383838" Property="style" PropertyKey="color" />
                                    <Color AnimationTarget="infoUnAcc4" StartValue="#febd0d" EndValue="#383838" Property="style" PropertyKey="borderColor" />
                                </Parallel>
                                <Parallel Duration=".5">
                                    <Color AnimationTarget="infoUnAcc4" StartValue="#383838" EndValue="#383838" Property="style" PropertyKey="color" />
                                    <Color AnimationTarget="infoUnAcc4" StartValue="#febd0d" EndValue="#383838" Property="style" PropertyKey="borderColor" />
                                    <FadeIn AnimationTarget="btnCloseParentUnAcc4" MaximumOpacity=".9" />                            
                                </Parallel>
                                
                            </Sequence>
                        </OnClick>
                                    </Animations>
                                </cc1:AnimationExtender>
                                
                       <cc1:AnimationExtender ID="AnimationExtender10" runat="server" TargetControlID="btnCloseUnAcc4">
                                    <Animations>
                        <OnClick>
                            <Sequence>
                                <StyleAction AnimationTarget="infoUnAcc4" Attribute="overflow" Value="hidden"/>
                                <Parallel AnimationTarget="infoUnAcc4" Duration=".3" Fps="15">
                                    <Scale ScaleFactor="0.05" Center="true" ScaleFont="true" FontUnit="px" />
                                    <FadeOut />
                                </Parallel>
                                <StyleAction AnimationTarget="infoUnAcc4" Attribute="display" Value="none"/>
                                <StyleAction AnimationTarget="infoUnAcc4" Attribute="width" Value="300px"/>
                                <StyleAction AnimationTarget="infoUnAcc4" Attribute="height" Value=""/>
                                <StyleAction AnimationTarget="infoUnAcc4" Attribute="fontSize" Value="12px"/>
                                <StyleAction AnimationTarget="btnCloseParentUnAcc4" Attribute="opacity" value="0" />
                                <StyleAction AnimationTarget="btnCloseParentUnAcc4" Attribute="filter" value="alpha(opacity=0)" />                        
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
                                
                                
                                <cc1:AnimationExtender ID="AnimationExtender11" runat="server" TargetControlID="lnkbtnViewUnAcc5">
                                    <Animations>
                        <OnLoad><OpacityAction AnimationTarget="infoUnAcc5" Opacity="0" /></OnLoad>
                        <OnClick>
                            <Sequence>                               
                                <ScriptAction Script="Cover($get('ctl00_MainContent_lnkbtnViewUnAcc'), $get('flyoutUnAcc5'));" />
                                <StyleAction AnimationTarget="flyoutUnAcc5" Attribute="display" Value="block"/>                            
                                <Parallel AnimationTarget="flyoutUnAcc5" Duration=".3" Fps="25">
                                    <Move Horizontal="150" Vertical="0" />
                                    <Resize Width="300" Height="20" />
                                    <Color AnimationTarget="flyoutUnAcc5" StartValue="#AAAAAA" EndValue="#f1fafa" Property="style" PropertyKey="backgroundColor" />                                
                                </Parallel>                            
                                <ScriptAction Script="Cover($get('flyoutUnAcc5'), $get('infoUnAcc5'), true);" />
                                <StyleAction AnimationTarget="infoUnAcc5" Attribute="display" Value="block"/>
                                <FadeIn AnimationTarget="infoUnAcc5" Duration=".2"/>                            
                                <StyleAction AnimationTarget="flyoutUnAcc5" Attribute="display" Value="none"/>
                                <StyleAction AnimationTarget="infoUnAcc5" Attribute="height" value="auto" />
                                <Parallel Duration=".5">
                                    <Color AnimationTarget="infoUnAcc5" StartValue="#383838" EndValue="#383838" Property="style" PropertyKey="color" />
                                    <Color AnimationTarget="infoUnAcc5" StartValue="#febd0d" EndValue="#383838" Property="style" PropertyKey="borderColor" />
                                </Parallel>
                                <Parallel Duration=".5">
                                    <Color AnimationTarget="infoUnAcc5" StartValue="#383838" EndValue="#383838" Property="style" PropertyKey="color" />
                                    <Color AnimationTarget="infoUnAcc5" StartValue="#febd0d" EndValue="#383838" Property="style" PropertyKey="borderColor" />
                                    <FadeIn AnimationTarget="btnCloseParentUnAcc5" MaximumOpacity=".9" />                            
                                </Parallel>
                                
                            </Sequence>
                        </OnClick>
                                    </Animations>
                                </cc1:AnimationExtender>
                                
                       <cc1:AnimationExtender ID="AnimationExtender12" runat="server" TargetControlID="btnCloseUnAcc5">
                                    <Animations>
                        <OnClick>
                            <Sequence>
                                <StyleAction AnimationTarget="infoUnAcc5" Attribute="overflow" Value="hidden"/>
                                <Parallel AnimationTarget="infoUnAcc5" Duration=".3" Fps="15">
                                    <Scale ScaleFactor="0.05" Center="true" ScaleFont="true" FontUnit="px" />
                                    <FadeOut />
                                </Parallel>
                                <StyleAction AnimationTarget="infoUnAcc5" Attribute="display" Value="none"/>
                                <StyleAction AnimationTarget="infoUnAcc5" Attribute="width" Value="300px"/>
                                <StyleAction AnimationTarget="infoUnAcc5" Attribute="height" Value=""/>
                                <StyleAction AnimationTarget="infoUnAcc5" Attribute="fontSize" Value="12px"/>
                                <StyleAction AnimationTarget="btnCloseParentUnAcc5" Attribute="opacity" value="0" />
                                <StyleAction AnimationTarget="btnCloseParentUnAcc5" Attribute="filter" value="alpha(opacity=0)" />                        
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
                                <span class="LabelsBody">Have you had an open bankruptcy in the last 12 months?</span><br />
                                <asp:RadioButton runat="server" ID="rdbMCABankruptY" Text="Yes" GroupName="rbtnMCABankrupt" AutoPostBack="True"
                                    OnCheckedChanged="rdbMCA_CheckedChanged" />
                                <asp:RadioButton runat="server" ID="rdbMCABankruptN" Text="No" GroupName="rbtnMCABankrupt" AutoPostBack="True"
                                    OnCheckedChanged="rdbMCA_CheckedChanged" />
                                <br />
                                <br />
                                <asp:Panel ID="pnlMCAVolumn" runat="server" Width="80%" Visible="False">
                                    <span class="LabelsBody">Do you process an average of $3,500 or more in credit card volume per month?</span><br />
                                    <asp:RadioButton runat="server" ID="rdbMCAVolumnY" Text="Yes" GroupName="rdbMCAVolumn" AutoPostBack="True"
                                        OnCheckedChanged="rdbMCA_CheckedChanged" />
                                    <asp:RadioButton runat="server" ID="rdbMCAVolumnN" Text="No" GroupName="rdbMCAVolumn" AutoPostBack="True"
                                        OnCheckedChanged="rdbMCA_CheckedChanged" />
                                </asp:Panel>
                                <br />
                                <asp:Panel ID="pnlMCAOver1yr" runat="server" Width="80%" Visible="False">
                                    <span class="LabelsBody">Are you in business for over one year?</span><br />
                                    <asp:RadioButton runat="server" ID="rdbMCAOver1yrY" Text="Yes" GroupName="rdbMCAOver1yr" AutoPostBack="True"
                                        OnCheckedChanged="rdbMCA_CheckedChanged" />
                                    <asp:RadioButton runat="server" ID="rdbMCAOver1yrN" Text="No" GroupName="rdbMCAOver1yr" AutoPostBack="True"
                                        OnCheckedChanged="rdbMCA_CheckedChanged" />
                                        <br/>
                                </asp:Panel>
                                <br/>
                                <asp:Panel ID="pnlMCAOver9mon" runat="server" Width="80%" Visible="False">
                                    <span class="LabelsBody">Are you in business for over nine months?</span><br />
                                    <asp:RadioButton runat="server" ID="rdbMCAOver9monY" Text="Yes" GroupName="rdbMCAOver9mon" AutoPostBack="True"
                                        OnCheckedChanged="rdbMCA_CheckedChanged" />
                                    <asp:RadioButton runat="server" ID="rdbMCAOver9monN" Text="No" GroupName="rdbMCAOver9mon" AutoPostBack="True"
                                        OnCheckedChanged="rdbMCA_CheckedChanged" />
                                        <br/><br/>
                                </asp:Panel>
                                
                                <asp:Panel ID="pnlRapidAdvanceList" runat="server" Width="80%" Visible="False">
                                    <span class="LabelsBody">Is your business listed in the following list of unacceptable businesses for RapidAdvance?</span><br />
                                    <asp:LinkButton runat="server" ID="lnkbtnViewUnAcc0" CssClass="Link" OnClientClick="return false;">View List of Unacceptable Businesses</asp:LinkButton>
                                    <div id="flyoutUnAcc0" style="z-index: 2; display: none; border: solid 1px #D0D0D0;
                                        background-color: #FFFFFF; overflow: hidden;">
                                        &nbsp;</div>
                                    <div id="infoUnAcc0" style="z-index: 2; display: none; font-size: 12px; border: solid 1px #CCCCCC;
                                        background-color: #ffffff; width: 300px; padding: 5px; font-family: Arial; font-size: 10pt;">
                                        <div style="float: right; filter: alpha(opacity=0);" id="btnCloseParentUnAcc0">
                                            <asp:LinkButton ID="btnCloseUnAcc0" runat="server" OnClientClick="return false;" Text=""
                                                Style="text-align: right; border: outset thin white; padding: 5px; text-decoration: none;
                                                background-color: #666666; color: White; text-align: center; font-weight: bold;"
                                                ToolTip="Close">X</asp:LinkButton>
                                        </div>
                                        <b><span class="LabelsBody">Uncceptable Businesses</span></b>
                                        <div class="DivGreen">
                                            <span class="LabelsBodySmall">Adult-related</span>
                                            <br />
                                            <span class="LabelsBodySmall">Auctions / Auction houses</span>
                                            <br />
                                            <span class="LabelsBodySmall">Billing Services</span>
                                            <br />
                                            <span class="LabelsBodySmall">Casinos</span>
                                            <br />
                                            <span class="LabelsBodySmall">Collection agencies</span>
                                            <br />
                                            <span class="LabelsBodySmall">Construction Contractors</span>
                                            <br />
                                            <span class="LabelsBodySmall">Day Care Centers</span>
                                            <br />
                                            <span class="LabelsBodySmall">Direct Marketing / MLM</span>
                                            <br />
                                            <span class="LabelsBodySmall">Event Planning</span>
                                            <br />
                                            <span class="LabelsBodySmall">Guard Dog Service</span>
                                            <br />
                                            <span class="LabelsBodySmall">Home Based</span>
                                            <br />
                                            <span class="LabelsBodySmall">Insurance Companies</span>
                                            <br />
                                            <span class="LabelsBodySmall">Internet Sales(No Store front)</span>
                                            <br />
                                            <span class="LabelsBodySmall">Kiosks(Mobile)</span>
                                            <br />
                                            <span class="LabelsBodySmall">Law Firms</span>
                                            <br />
                                            <span class="LabelsBodySmall">Property Management/Reality</span>
                                            <br />
                                            <span class="LabelsBodySmall">RV dealers</span>
                                            <br />
                                            <span class="LabelsBodySmall">Tractor tailor Transportation</span>
                                            <br />
                                            <span class="LabelsBodySmall">Travel agencies / ToursWeight Loss Centers</span>
                                            <br />
                                        </div>
                                    </div>
                                    <br />
                                    <asp:RadioButton runat="server" ID="rdbRapidAdvanceListY" Text="Yes" GroupName="rdbRapidAdvanceList" AutoPostBack="True"
                                        OnCheckedChanged="rdbMCA_CheckedChanged" />
                                    <asp:RadioButton runat="server" ID="rdbRapidAdvanceListN" Text="No" GroupName="rdbRapidAdvanceList" AutoPostBack="True"
                                        OnCheckedChanged="rdbMCA_CheckedChanged" />
                                        <br/><br/>
                                </asp:Panel>
                                
                                <asp:Panel ID="pnlAdvanceMeList" runat="server" Width="80%" Visible="False">
                                    <span class="LabelsBody">Is your business listed in the following list of unacceptable businesses for AdvanceMe, Inc?</span><br />
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
                                            <span class="LabelsBodySmall">Adult-related</span>
                                            <br />
                                            <span class="LabelsBodySmall">Auctions/Auction houses</span>
                                            <br />
                                            <span class="LabelsBodySmall">Auto, ATV, RV and motorcycle dealerships</span>
                                            <br />
                                            <span class="LabelsBodySmall">Cellular phone stores</span>
                                            <br />
                                            <span class="LabelsBodySmall">Collection agencies</span>
                                            <br />
                                            <span class="LabelsBodySmall">Construction companies</span>
                                            <br />
                                            <span class="LabelsBodySmall">Day Care Centers</span>
                                            <br />
                                            <span class="LabelsBodySmall">Entertainers / entertainment groups</span>
                                            <br />
                                            <span class="LabelsBodySmall">Flea markets</span>
                                            <br />
                                            <span class="LabelsBodySmall">Furniture</span>
                                            <br />
                                            <span class="LabelsBodySmall">Gas Stations</span>
                                            <br />
                                            <span class="LabelsBodySmall">Golf Courses</span>
                                            <br />
                                            <span class="LabelsBodySmall">Grocery / Convenience stores</span>
                                            <br />
                                            <span class="LabelsBodySmall">Home Based</span>
                                            <br />
                                            <span class="LabelsBodySmall">Insurance Companies</span>
                                            <br />
                                            <span class="LabelsBodySmall">Jewelry Stores</span>
                                            <br />
                                            <span class="LabelsBodySmall">Management Companies</span>
                                            <br />
                                            <span class="LabelsBodySmall">More than 80% card not present</span>
                                            <br />
                                            <span class="LabelsBodySmall">Theme / Amusement parks</span>
                                            <br />
                                            <span class="LabelsBodySmall">Taxis, limousines, shuttles, other transportation</span>
                                            <br />
                                            <span class="LabelsBodySmall">Travel agencies / Tours</span>
                                            <br />
                                        </div>
                                    </div>
                                    <br />
                                    <asp:RadioButton runat="server" ID="rdbAdvanceMeListY" Text="Yes" GroupName="rdbAdvanceMeList" AutoPostBack="true"
                                        OnCheckedChanged="rdbMCA_CheckedChanged" />
                                    <asp:RadioButton runat="server" ID="rdbAdvanceMeListN" Text="No" GroupName="rdbAdvanceMeList" AutoPostBack="true"
                                        OnCheckedChanged="rdbMCA_CheckedChanged" /> <br /> <br/>
                                </asp:Panel>
                                
                                <asp:Panel ID="pnlBFSList" runat="server" Width="80%" Visible="False">
                                    <span class="LabelsBody">Is your business listed in the following list of unacceptable businesses for Business Financial Services?</span><br />
                                    <asp:LinkButton runat="server" ID="lnkbtnViewUnAcc2" CssClass="Link" OnClientClick="return false;">View List of Unacceptable Businesses</asp:LinkButton>
                                    <div id="flyoutUnAcc2" style="z-index: 2; display: none; border: solid 1px #D0D0D0;
                                        background-color: #FFFFFF; overflow: hidden;">
                                        &nbsp;</div>
                                    <div id="infoUnAcc2" style="z-index: 2; display: none; font-size: 12px; border: solid 1px #CCCCCC;
                                        background-color: #ffffff; width: 300px; padding: 5px; font-family: Arial; font-size: 10pt;">
                                        <div style="float: right; filter: alpha(opacity=0);" id="btnCloseParentUnAcc2">
                                            <asp:LinkButton ID="btnCloseUnAcc2" runat="server" OnClientClick="return false;"
                                                Text="" Style="text-align: right; border: outset thin white; padding: 5px; text-decoration: none;
                                                background-color: #666666; color: White; text-align: center; font-weight: bold;"
                                                ToolTip="Close">X</asp:LinkButton>
                                        </div>
                                        <b><span class="LabelsBody">Uncceptable Businesses</span></b>
                                        <div class="DivGreen">
                                            <span class="LabelsBodySmall">Adult Entertainment</span>
                                            <br />
                                            <span class="LabelsBodySmall">Auctions / Auction Houses</span>
                                            <br />
                                            <span class="LabelsBodySmall">Auto, ATV, RV and motorcycle dealerships</span>
                                            <br />
                                            <span class="LabelsBodySmall">Beauty amd Nutritional (Internet and Mail Order)</span>
                                            <br />
                                            <span class="LabelsBodySmall">Construction / Building / HVAC</span>
                                            <br />
                                            <span class="LabelsBodySmall">Direct Marketing / MLM</span>
                                            <br />
                                            <span class="LabelsBodySmall">Drop Shippers / Future Delivery</span>
                                            <br />
                                            <span class="LabelsBodySmall">Electronics</span>
                                            <br />
                                            <span class="LabelsBodySmall">Entertainment and Event Sales</span>
                                            <br />
                                            <span class="LabelsBodySmall">Fitness</span>
                                            <br />
                                            <span class="LabelsBodySmall">Furniture</span>
                                            <br />
                                            <span class="LabelsBodySmall">Gas Stations</span>
                                            <br />
                                            <span class="LabelsBodySmall">Glass Tinting</span>
                                            <br />
                                            <span class="LabelsBodySmall">Groceries International Foods</span>
                                            <br />
                                            <span class="LabelsBodySmall">Professional Services (accountants, attorneys, brokers)</span>
                                            <br />
                                            <span class="LabelsBodySmall">Schools, Learning, Tutoring</span>
                                            <br />
                                            <span class="LabelsBodySmall">Tire Stores (Over 50% tire)</span>
                                            <br />
                                            <span class="LabelsBodySmall">Travel Agencies / Tours</span>
                                            <br />
                                            <span class="LabelsBodySmall">Weapons Dealer</span>
                                            <br />
                                        </div>
                                    </div>
                                    <br />
                                    <asp:RadioButton runat="server" ID="rdbBFSListY" Text="Yes" GroupName="rdbBFSList" AutoPostBack="true"
                                        OnCheckedChanged="rdbMCA_CheckedChanged" />
                                    <asp:RadioButton runat="server" ID="rdbBFSListN" Text="No" GroupName="rdbBFSList" AutoPostBack="true"
                                        OnCheckedChanged="rdbMCA_CheckedChanged" /><br />
                                </asp:Panel>
                                
                                <asp:Panel ID="pnlMCAOver6mon" runat="server" Width="80%" Visible="False">
                                    <span class="LabelsBody">Are you in business for over six months?</span><br />
                                    <asp:RadioButton runat="server" ID="rdbMCAOver6monY" Text="Yes" GroupName="rdbMCAOver6mon" AutoPostBack="True"
                                        OnCheckedChanged="rdbMCA_CheckedChanged" />
                                    <asp:RadioButton runat="server" ID="rdbMCAOver6monN" Text="No" GroupName="rdbMCAOver6mon" AutoPostBack="True"
                                        OnCheckedChanged="rdbMCA_CheckedChanged" />
                                        <br/> <br/>
                                </asp:Panel>

                                <asp:Panel ID="pnlMCAOver3mon" runat="server" Width="80%" Visible="False">
                                    <span class="LabelsBody">Are you in business for over three months with three months processing history?</span><br />
                                    <asp:RadioButton runat="server" ID="rdbMCAOver3monY" Text="Yes" GroupName="rdbMCAOver3mon" AutoPostBack="True"
                                        OnCheckedChanged="rdbMCA_CheckedChanged" />
                                    <asp:RadioButton runat="server" ID="rdbMCAOver3monN" Text="No" GroupName="rdbMCAOver3mon" AutoPostBack="True"
                                        OnCheckedChanged="rdbMCA_CheckedChanged" />
                                        <br/><br/>
                                </asp:Panel>

                                <asp:Panel ID="pnlProcesstenthousand" runat="server" Width="80%" Visible="False">
                                    <span class="LabelsBody">Do you process an average of $10,000 or more in credit card volume per month?</span><br />
                                    <asp:RadioButton runat="server" ID="rdbProcesstenthousandY" Text="Yes" GroupName="rdbProcesstenthousand" AutoPostBack="True"
                                        OnCheckedChanged="rdbMCA_CheckedChanged" />
                                    <asp:RadioButton runat="server" ID="rdbProcesstenthousandN" Text="No" GroupName="rdbProcesstenthousand" AutoPostBack="True"
                                        OnCheckedChanged="rdbMCA_CheckedChanged" />
                                        <br/><br/>
                                </asp:Panel>

                                <asp:Panel ID="pnlRapidAdvanceListStarter" runat="server" Width="80%" Visible="False">
                                    <span class="LabelsBody">Is your business listed in the following list of unacceptable businesses for RapidAdvance?</span><br />
                                    <asp:LinkButton runat="server" ID="lnkbtnViewUnAcc3" CssClass="Link" OnClientClick="return false;">View List of Unacceptable Businesses</asp:LinkButton>
                                    <div id="flyoutUnAcc3" style="z-index: 2; display: none; border: solid 1px #D0D0D0;
                                        background-color: #FFFFFF; overflow: hidden;">
                                        &nbsp;</div>
                                    <div id="infoUnAcc3" style="z-index: 2; display: none; font-size: 12px; border: solid 1px #CCCCCC;
                                        background-color: #ffffff; width: 300px; padding: 5px; font-family: Arial; font-size: 10pt;">
                                        <div style="float: right; filter: alpha(opacity=0);" id="btnCloseParentUnAcc3">
                                            <asp:LinkButton ID="btnCloseUnAcc3" runat="server" OnClientClick="return false;" Text=""
                                                Style="text-align: right; border: outset thin white; padding: 5px; text-decoration: none;
                                                background-color: #666666; color: White; text-align: center; font-weight: bold;"
                                                ToolTip="Close">X</asp:LinkButton>
                                        </div>
                                        <b><span class="LabelsBody">Uncceptable Businesses</span></b>
                                                                                <div class="DivGreen">
                                            <span class="LabelsBodySmall">Adult-related</span>
                                            <br />
                                            <span class="LabelsBodySmall">Auctions / Auction houses</span>
                                            <br />
                                            <span class="LabelsBodySmall">Billing Services</span>
                                            <br />
                                            <span class="LabelsBodySmall">Casinos</span>
                                            <br />
                                            <span class="LabelsBodySmall">Collection agencies</span>
                                            <br />
                                            <span class="LabelsBodySmall">Construction Contractors</span>
                                            <br />
                                            <span class="LabelsBodySmall">Day Care Centers</span>
                                            <br />
                                            <span class="LabelsBodySmall">Direct Marketing / MLM</span>
                                            <br />
                                            <span class="LabelsBodySmall">Event Planning</span>
                                            <br />
                                            <span class="LabelsBodySmall">Guard Dog Service</span>
                                            <br />
                                            <span class="LabelsBodySmall">Home Based</span>
                                            <br />
                                            <span class="LabelsBodySmall">Insurance Companies</span>
                                            <br />
                                            <span class="LabelsBodySmall">Internet Sales(No Store front)</span>
                                            <br />
                                            <span class="LabelsBodySmall">Kiosks(Mobile)</span>
                                            <br />
                                            <span class="LabelsBodySmall">Law Firms</span>
                                            <br />
                                            <span class="LabelsBodySmall">Property Management/Reality</span>
                                            <br />
                                            <span class="LabelsBodySmall">RV dealers</span>
                                            <br />
                                            <span class="LabelsBodySmall">Tractor tailor Transportation</span>
                                            <br />
                                            <span class="LabelsBodySmall">Travel agencies / ToursWeight Loss Centers</span>
                                            <br />
                                        </div>
                                    </div>
                                    <br />
                                    <asp:RadioButton runat="server" ID="rdbRapidAdvanceListStarterY" Text="Yes" GroupName="rdbRapidAdvanceListStarterY" AutoPostBack="True"
                                        OnCheckedChanged="rdbMCA_CheckedChanged" />
                                    <asp:RadioButton runat="server" ID="rdbRapidAdvanceListStarterN" Text="No" GroupName="rdbRapidAdvanceListStarterN" AutoPostBack="True"
                                        OnCheckedChanged="rdbMCA_CheckedChanged" />
                                        <br/><br/>
                                </asp:Panel>
                                
                                <asp:Panel ID="pnlBFSListStarter" runat="server" Width="80%" Visible="False">
                                    <span class="LabelsBody">Is your business listed in the following list of unacceptable businesses for Business Financial Services?</span><br />
                                    <asp:LinkButton runat="server" ID="lnkbtnViewUnAcc4" CssClass="Link" OnClientClick="return false;">View List of Unacceptable Businesses</asp:LinkButton>
                                    <div id="flyoutUnAcc4" style="z-index: 2; display: none; border: solid 1px #D0D0D0;
                                        background-color: #FFFFFF; overflow: hidden;">
                                        &nbsp;</div>
                                    <div id="infoUnAcc4" style="z-index: 2; display: none; font-size: 12px; border: solid 1px #CCCCCC;
                                        background-color: #ffffff; width: 300px; padding: 5px; font-family: Arial; font-size: 10pt;">
                                        <div style="float: right; filter: alpha(opacity=0);" id="btnCloseParentUnAcc4">
                                            <asp:LinkButton ID="btnCloseUnAcc4" runat="server" OnClientClick="return false;"
                                                Text="" Style="text-align: right; border: outset thin white; padding: 5px; text-decoration: none;
                                                background-color: #666666; color: White; text-align: center; font-weight: bold;"
                                                ToolTip="Close">X</asp:LinkButton>
                                        </div>
                                        <b><span class="LabelsBody">Uncceptable Businesses</span></b>
                                        <div class="DivGreen">
                                            <span class="LabelsBodySmall">Adult Entertainment</span>
                                            <br />
                                            <span class="LabelsBodySmall">Auctions / Auction Houses</span>
                                            <br />
                                            <span class="LabelsBodySmall">Auto, ATV, RV and motorcycle dealerships</span>
                                            <br />
                                            <span class="LabelsBodySmall">Beauty amd Nutritional (Internet and Mail Order)</span>
                                            <br />
                                            <span class="LabelsBodySmall">Construction / Building / HVAC</span>
                                            <br />
                                            <span class="LabelsBodySmall">Direct Marketing / MLM</span>
                                            <br />
                                            <span class="LabelsBodySmall">Drop Shippers / Future Delivery</span>
                                            <br />
                                            <span class="LabelsBodySmall">Electronics</span>
                                            <br />
                                            <span class="LabelsBodySmall">Entertainment and Event Sales</span>
                                            <br />
                                            <span class="LabelsBodySmall">Fitness</span>
                                            <br />
                                            <span class="LabelsBodySmall">Furniture</span>
                                            <br />
                                            <span class="LabelsBodySmall">Gas Stations</span>
                                            <br />
                                            <span class="LabelsBodySmall">Glass Tinting</span>
                                            <br />
                                            <span class="LabelsBodySmall">Groceries International Foods</span>
                                            <br />
                                            <span class="LabelsBodySmall">Professional Services (accountants, attorneys, brokers)</span>
                                            <br />
                                            <span class="LabelsBodySmall">Schools, Learning, Tutoring</span>
                                            <br />
                                            <span class="LabelsBodySmall">Tire Stores (Over 50% tire)</span>
                                            <br />
                                            <span class="LabelsBodySmall">Travel Agencies / Tours</span>
                                            <br />
                                            <span class="LabelsBodySmall">Weapons Dealer</span>
                                            <br />
                                        </div>
                                    </div>
                                    <br />
                                    <asp:RadioButton runat="server" ID="rdbBFSListStarterY" Text="Yes" GroupName="rdbBFSListStarter" AutoPostBack="true"
                                        OnCheckedChanged="rdbMCA_CheckedChanged" />
                                    <asp:RadioButton runat="server" ID="rdbBFSListStarterN" Text="No" GroupName="rdbBFSListStarter" AutoPostBack="true"
                                        OnCheckedChanged="rdbMCA_CheckedChanged" /><br />
                                </asp:Panel>
                                
                                

                                <asp:Panel ID="pnlProcessFourthousand" runat="server" Width="80%" Visible="False">
                                    <span class="LabelsBody">Do you process an average of $4,000 or more in credit card volume per month?</span><br />
                                    <asp:RadioButton runat="server" ID="rdbProcessFourthousandY" Text="Yes" GroupName="rdbProcessFourthousand" AutoPostBack="True"
                                        OnCheckedChanged="rdbMCA_CheckedChanged" />
                                    <asp:RadioButton runat="server" ID="rdbProcessFourthousandN" Text="No" GroupName="rdbProcessFourthousand" AutoPostBack="True"
                                        OnCheckedChanged="rdbMCA_CheckedChanged" />
                                        <br/><br/>
                                </asp:Panel>
                                
                                <asp:Panel ID="pnlBFSListStater4000" runat="server" Width="80%" Visible="False">
                                    <span class="LabelsBody">Is your business listed in the following list of unacceptable businesses for Business Financial Services?</span><br />
                                    <asp:LinkButton runat="server" ID="lnkbtnViewUnAcc5" CssClass="Link" OnClientClick="return false;">View List of Unacceptable Businesses</asp:LinkButton>
                                    <div id="flyoutUnAcc5" style="z-index: 2; display: none; border: solid 1px #D0D0D0;
                                        background-color: #FFFFFF; overflow: hidden;">
                                        &nbsp;</div>
                                    <div id="infoUnAcc5" style="z-index: 2; display: none; font-size: 12px; border: solid 1px #CCCCCC;
                                        background-color: #ffffff; width: 300px; padding: 5px; font-family: Arial; font-size: 10pt;">
                                        <div style="float: right; filter: alpha(opacity=0);" id="btnCloseParentUnAcc5">
                                            <asp:LinkButton ID="btnCloseUnAcc5" runat="server" OnClientClick="return false;"
                                                Text="" Style="text-align: right; border: outset thin white; padding: 5px; text-decoration: none;
                                                background-color: #666666; color: White; text-align: center; font-weight: bold;"
                                                ToolTip="Close">X</asp:LinkButton>
                                        </div>
                                        <b><span class="LabelsBody">Uncceptable Businesses</span></b>
                                        <div class="DivGreen">
                                            <span class="LabelsBodySmall">Adult Entertainment</span>
                                            <br />
                                            <span class="LabelsBodySmall">Auctions / Auction Houses</span>
                                            <br />
                                            <span class="LabelsBodySmall">Auto, ATV, RV and motorcycle dealerships</span>
                                            <br />
                                            <span class="LabelsBodySmall">Beauty amd Nutritional (Internet and Mail Order)</span>
                                            <br />
                                            <span class="LabelsBodySmall">Construction / Building / HVAC</span>
                                            <br />
                                            <span class="LabelsBodySmall">Direct Marketing / MLM</span>
                                            <br />
                                            <span class="LabelsBodySmall">Drop Shippers / Future Delivery</span>
                                            <br />
                                            <span class="LabelsBodySmall">Electronics</span>
                                            <br />
                                            <span class="LabelsBodySmall">Entertainment and Event Sales</span>
                                            <br />
                                            <span class="LabelsBodySmall">Fitness</span>
                                            <br />
                                            <span class="LabelsBodySmall">Furniture</span>
                                            <br />
                                            <span class="LabelsBodySmall">Gas Stations</span>
                                            <br />
                                            <span class="LabelsBodySmall">Glass Tinting</span>
                                            <br />
                                            <span class="LabelsBodySmall">Groceries International Foods</span>
                                            <br />
                                            <span class="LabelsBodySmall">Professional Services (accountants, attorneys, brokers)</span>
                                            <br />
                                            <span class="LabelsBodySmall">Schools, Learning, Tutoring</span>
                                            <br />
                                            <span class="LabelsBodySmall">Tire Stores (Over 50% tire)</span>
                                            <br />
                                            <span class="LabelsBodySmall">Travel Agencies / Tours</span>
                                            <br />
                                            <span class="LabelsBodySmall">Weapons Dealer</span>
                                            <br />
                                        </div>
                                    </div>
                                    <br />
                                    <asp:RadioButton runat="server" ID="rdbBFSListStater4000Y" Text="Yes" GroupName="rdbBFSListStater4000" AutoPostBack="true"
                                        OnCheckedChanged="rdbMCA_CheckedChanged" />
                                    <asp:RadioButton runat="server" ID="rdbBFSListStater4000N" Text="No" GroupName="rdbBFSListStater4000" AutoPostBack="true"
                                        OnCheckedChanged="rdbMCA_CheckedChanged" /><br />
                                </asp:Panel>
                                
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
