<%@ Page Language="C#" MasterPageFile="site.master" AutoEventWireup="true" CodeFile="startMCA_old.aspx.cs"
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
                                <br />
                                <span class="LabelsBody">Do you process an average of $5,000 or more in credit card
                                    volume per month?</span><br />
                                <asp:RadioButton runat="server" ID="rdbMCA1Y" Text="Yes" GroupName="rbtnMCA1" AutoPostBack="True"
                                    OnCheckedChanged="rdbMCA_CheckedChanged" />
                                <asp:RadioButton runat="server" ID="rdbMCA1N" Text="No" GroupName="rbtnMCA1" AutoPostBack="True"
                                    OnCheckedChanged="rdbMCA_CheckedChanged" />
                                <br />
                                <br />
                                <asp:Panel ID="pnlMCA2" runat="server" Width="80%" Visible="False">
                                    <span class="LabelsBody">Do you process more than 80% in swiped transactions?</span><br />
                                    <asp:RadioButton runat="server" ID="rdbMCA2Y" Text="Yes" GroupName="rdbMCA2" AutoPostBack="True"
                                        OnCheckedChanged="rdbMCA_CheckedChanged" />
                                    <asp:RadioButton runat="server" ID="rdbMCA2N" Text="No" GroupName="rdbMCA2" AutoPostBack="True"
                                        OnCheckedChanged="rdbMCA_CheckedChanged" />
                                </asp:Panel>
                                <br />
                                <asp:Panel ID="pnlMCA3" runat="server" Width="80%" Visible="False">
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
                                            <span class="LabelsBodySmall">Auctions / Auction Houses</span>
                                            <br />
                                            <span class="LabelsBodySmall">Auto, ATV, RV and motorcycle dealerships</span>
                                            <br />
                                            <span class="LabelsBodySmall">Cellular phone stores</span>
                                            <br />
                                            <span class="LabelsBodySmall">Collection agencies</span>
                                            <br />
                                            <span class="LabelsBodySmall">Construction Companies</span>
                                            <br />
                                            <span class="LabelsBodySmall">Day Care Centers</span>
                                            <br />
                                            <span class="LabelsBodySmall">Entertainers / entertainment groups</span>
                                            <br />
                                            <span class="LabelsBodySmall">Flee markets</span>
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
                                            <span class="LabelsBodySmall">Theme / Amusement parks</span>
                                            <br />
                                            <span class="LabelsBodySmall">Taxis, limousines, shuttles, other transportation</span>
                                            <br />
                                            <span class="LabelsBodySmall">Travel Agencies / Tours</span>
                                            <br />
                                        </div>
                                    </div>
                                    <br />
                                    <asp:RadioButton runat="server" ID="rdbMCA3Y" Text="Yes" GroupName="rdbMCA3" AutoPostBack="true"
                                        OnCheckedChanged="rdbMCA_CheckedChanged" />
                                    <asp:RadioButton runat="server" ID="rdbMCA3N" Text="No" GroupName="rdbMCA3" AutoPostBack="true"
                                        OnCheckedChanged="rdbMCA_CheckedChanged" />
                                </asp:Panel>
                                <br />
                                <asp:Panel ID="pnlMCA4" runat="server" Width="80%" Visible="False">
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
                                    <asp:RadioButton runat="server" ID="rdbMCA4Y" Text="Yes" GroupName="rdbMCA4" AutoPostBack="true"
                                        OnCheckedChanged="rdbMCA_CheckedChanged" />
                                    <asp:RadioButton runat="server" ID="rdbMCA4N" Text="No" GroupName="rdbMCA4" AutoPostBack="true"
                                        OnCheckedChanged="rdbMCA_CheckedChanged" />
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
