<%@ Page Language="C#" MasterPageFile="site.master" AutoEventWireup="true" CodeFile="start2.aspx.cs"
    Inherits="Questions2" Theme="TextBoxSkin" Title="Commerce Technologies - OnlineApplication" %>

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
                    <td>
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
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <cc1:AnimationExtender ID="AnimationExtender1" runat="server" TargetControlID="lnkbtnViewAcc">
                                    <Animations>
                                        <OnLoad><OpacityAction AnimationTarget="infoAcc" Opacity="0" /></OnLoad>
                                        <OnClick>
                                            <Sequence>                               
                                                <ScriptAction Script="Cover($get('ctl00_MainContent_lnkbtnViewAcc'), $get('flyoutAcc'));" />
                                                <StyleAction AnimationTarget="flyoutAcc" Attribute="display" Value="block"/>                            
                                                <Parallel AnimationTarget="flyoutAcc" Duration=".3" Fps="25">
                                                    <Move Horizontal="150" Vertical="0" />
                                                    <Resize Width="300" Height="20" />
                                                    <Color AnimationTarget="flyoutAcc" StartValue="#AAAAAA" EndValue="#f1fafa" Property="style" PropertyKey="backgroundColor" />                                
                                                </Parallel>                            
                                                <ScriptAction Script="Cover($get('flyoutAcc'), $get('infoAcc'), true);" />
                                                <StyleAction AnimationTarget="infoAcc" Attribute="display" Value="block"/>
                                                <FadeIn AnimationTarget="infoAcc" Duration=".2"/>                            
                                                <StyleAction AnimationTarget="flyoutAcc" Attribute="display" Value="none"/>
                                                <StyleAction AnimationTarget="infoAcc" Attribute="height" value="auto" />
                                                <Parallel Duration=".5">
                                                    <Color AnimationTarget="infoAcc" StartValue="#383838" EndValue="#383838" Property="style" PropertyKey="color" />
                                                    <Color AnimationTarget="infoAcc" StartValue="#febd0d" EndValue="#383838" Property="style" PropertyKey="borderColor" />
                                                </Parallel>
                                                <Parallel Duration=".5">
                                                    <Color AnimationTarget="infoAcc" StartValue="#383838" EndValue="#383838" Property="style" PropertyKey="color" />
                                                    <Color AnimationTarget="infoAcc" StartValue="#febd0d" EndValue="#383838" Property="style" PropertyKey="borderColor" />
                                                    <FadeIn AnimationTarget="btnCloseParentAcc" MaximumOpacity=".9" />                            
                                                </Parallel>
                                                
                                            </Sequence>
                                        </OnClick>
                                    </Animations>
                                </cc1:AnimationExtender>
                                <cc1:AnimationExtender ID="AnimationExtender2" runat="server" TargetControlID="btnCloseAcc">
                                    <Animations>
                                        <OnClick>
                                            <Sequence>
                                                <StyleAction AnimationTarget="infoAcc" Attribute="overflow" Value="hidden"/>
                                                <Parallel AnimationTarget="infoAcc" Duration=".3" Fps="15">
                                                    <Scale ScaleFactor="0.05" Center="true" ScaleFont="true" FontUnit="px" />
                                                    <FadeOut />
                                                </Parallel>
                                                <StyleAction AnimationTarget="infoAcc" Attribute="display" Value="none"/>
                                                <StyleAction AnimationTarget="infoAcc" Attribute="width" Value="300px"/>
                                                <StyleAction AnimationTarget="infoAcc" Attribute="height" Value=""/>
                                                <StyleAction AnimationTarget="infoAcc" Attribute="fontSize" Value="12px"/>
                                                <StyleAction AnimationTarget="btnCloseParentAcc" Attribute="opacity" value="0" />
                                                <StyleAction AnimationTarget="btnCloseParentAcc" Attribute="filter" value="alpha(opacity=0)" />                        
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
                                <cc1:AnimationExtender ID="AnimationExtender5" runat="server" TargetControlID="lnkbtnViewUnAccCountries">
                                    <Animations>
                                        <OnLoad><OpacityAction AnimationTarget="infoUnAccCountries" Opacity="0" /></OnLoad>
                                        <OnClick>
                                            <Sequence>                               
                                                <ScriptAction Script="Cover($get('ctl00_MainContent_lnkbtnViewUnAccCountries'), $get('flyoutUnAccCountries'));" />
                                                <StyleAction AnimationTarget="flyoutUnAccCountries" Attribute="display" Value="block"/>                            
                                                <Parallel AnimationTarget="flyoutUnAccCountries" Duration=".3" Fps="25">
                                                    <Move Horizontal="150" Vertical="0" />
                                                    <Resize Width="300" Height="20" />
                                                    <Color AnimationTarget="flyoutUnAccCountries" StartValue="#AAAAAA" EndValue="#f1fafa" Property="style" PropertyKey="backgroundColor" />                                
                                                </Parallel>                            
                                                <ScriptAction Script="Cover($get('flyoutUnAccCountries'), $get('infoUnAccCountries'), true);" />
                                                <StyleAction AnimationTarget="infoUnAccCountries" Attribute="display" Value="block"/>
                                                <FadeIn AnimationTarget="infoUnAccCountries" Duration=".2"/>                            
                                                <StyleAction AnimationTarget="flyoutUnAccCountries" Attribute="display" Value="none"/>
                                                <StyleAction AnimationTarget="infoUnAccCountries" Attribute="height" value="auto" />
                                                <Parallel Duration=".5">
                                                    <Color AnimationTarget="infoUnAccCountries" StartValue="#383838" EndValue="#383838" Property="style" PropertyKey="color" />
                                                    <Color AnimationTarget="infoUnAccCountries" StartValue="#febd0d" EndValue="#383838" Property="style" PropertyKey="borderColor" />
                                                </Parallel>
                                                <Parallel Duration=".5">
                                                    <Color AnimationTarget="infoUnAccCountries" StartValue="#383838" EndValue="#383838" Property="style" PropertyKey="color" />
                                                    <Color AnimationTarget="infoUnAccCountries" StartValue="#febd0d" EndValue="#383838" Property="style" PropertyKey="borderColor" />
                                                    <FadeIn AnimationTarget="btnCloseParentUnAccCountries" MaximumOpacity=".9" />                            
                                                </Parallel>
                                                
                                            </Sequence>
                                        </OnClick>
                                    </Animations>
                                </cc1:AnimationExtender>
                                <cc1:AnimationExtender ID="AnimationExtender6" runat="server" TargetControlID="btnCloseUnAccCountries">
                                    <Animations>
                                        <OnClick>
                                            <Sequence>
                                                <StyleAction AnimationTarget="infoUnAccCountries" Attribute="overflow" Value="hidden"/>
                                                <Parallel AnimationTarget="infoUnAccCountries" Duration=".3" Fps="15">
                                                    <Scale ScaleFactor="0.05" Center="true" ScaleFont="true" FontUnit="px" />
                                                    <FadeOut />
                                                </Parallel>
                                                <StyleAction AnimationTarget="infoUnAccCountries" Attribute="display" Value="none"/>
                                                <StyleAction AnimationTarget="infoUnAccCountries" Attribute="width" Value="300px"/>
                                                <StyleAction AnimationTarget="infoUnAccCountries" Attribute="height" Value=""/>
                                                <StyleAction AnimationTarget="infoUnAccCountries" Attribute="fontSize" Value="12px"/>
                                                <StyleAction AnimationTarget="btnCloseParentUnAccCountries" Attribute="opacity" value="0" />
                                                <StyleAction AnimationTarget="btnCloseParentUnAccCountries" Attribute="filter" value="alpha(opacity=0)" />                        
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
                                <cc1:AnimationExtender ID="AnimationExtender3" runat="server" TargetControlID="lnkbtnViewUnAcc">
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
                                <cc1:AnimationExtender ID="AnimationExtender4" runat="server" TargetControlID="btnCloseUnAcc">
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
                                <cc1:AnimationExtender ID="AnimationExtender7" runat="server" TargetControlID="lnkbtnIncorp">
                                    <Animations>
                                        <OnLoad><OpacityAction AnimationTarget="infoIncorp" Opacity="0" /></OnLoad>
                                        <OnClick>
                                            <Sequence>                               
                                                <ScriptAction Script="Cover($get('ctl00_MainContent_lnkbtnIncorp'), $get('flyoutIncorp'));" />
                                                <StyleAction AnimationTarget="flyoutIncorp" Attribute="display" Value="block"/>                            
                                                <Parallel AnimationTarget="flyoutIncorp" Duration=".3" Fps="25">
                                                    <Move Horizontal="150" Vertical="0" />
                                                    <Resize Width="300" Height="20" />
                                                    <Color AnimationTarget="flyoutIncorp" StartValue="#AAAAAA" EndValue="#f1fafa" Property="style" PropertyKey="backgroundColor" />                                
                                                </Parallel>                            
                                                <ScriptAction Script="Cover($get('flyoutIncorp'), $get('infoIncorp'), true);" />
                                                <StyleAction AnimationTarget="infoIncorp" Attribute="display" Value="block"/>
                                                <FadeIn AnimationTarget="infoIncorp" Duration=".2"/>                            
                                                <StyleAction AnimationTarget="flyoutIncorp" Attribute="display" Value="none"/>
                                                <StyleAction AnimationTarget="infoIncorp" Attribute="height" value="auto" />
                                                <Parallel Duration=".5">
                                                    <Color AnimationTarget="infoIncorp" StartValue="#383838" EndValue="#383838" Property="style" PropertyKey="color" />
                                                    <Color AnimationTarget="infoIncorp" StartValue="#febd0d" EndValue="#383838" Property="style" PropertyKey="borderColor" />
                                                </Parallel>
                                                <Parallel Duration=".5">
                                                    <Color AnimationTarget="infoIncorp" StartValue="#383838" EndValue="#383838" Property="style" PropertyKey="color" />
                                                    <Color AnimationTarget="infoIncorp" StartValue="#febd0d" EndValue="#383838" Property="style" PropertyKey="borderColor" />
                                                    <FadeIn AnimationTarget="btnCloseParentIncorp" MaximumOpacity=".9" />                            
                                                </Parallel>
                                                
                                            </Sequence>
                                        </OnClick>
                                    </Animations>
                                </cc1:AnimationExtender>
                                <cc1:AnimationExtender ID="AnimationExtender8" runat="server" TargetControlID="btnCloseIncorp">
                                    <Animations>
                                        <OnClick>
                                            <Sequence>
                                                <StyleAction AnimationTarget="infoIncorp" Attribute="overflow" Value="hidden"/>
                                                <Parallel AnimationTarget="infoIncorp" Duration=".3" Fps="15">
                                                    <Scale ScaleFactor="0.05" Center="true" ScaleFont="true" FontUnit="px" />
                                                    <FadeOut />
                                                </Parallel>
                                                <StyleAction AnimationTarget="infoIncorp" Attribute="display" Value="none"/>
                                                <StyleAction AnimationTarget="infoIncorp" Attribute="width" Value="300px"/>
                                                <StyleAction AnimationTarget="infoIncorp" Attribute="height" Value=""/>
                                                <StyleAction AnimationTarget="infoIncorp" Attribute="fontSize" Value="12px"/>
                                                <StyleAction AnimationTarget="btnCloseParentIncorp" Attribute="opacity" value="0" />
                                                <StyleAction AnimationTarget="btnCloseParentIncorp" Attribute="filter" value="alpha(opacity=0)" />                        
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
                                <span class="LabelsBody">Is your business listed in the following list?</span><br />
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
                                        <span class="LabelsBodySmall">Any Product or Service considered illegal</span>
                                        <br />
                                        <span class="LabelsBodySmall">Auction (MOTO/Internet)</span>
                                        <br />
                                        <span class="LabelsBodySmall">Auto Lending</span>
                                        <br />
                                        <span class="LabelsBodySmall">Bath Salts, Herbal Incense, or similar products</span>
                                        <br />
                                        <span class="LabelsBodySmall">Check Cashing, Wire Transfers and Other Payment Related Services</span>
                                        <br />
                                        <span class="LabelsBodySmall">Collections and Collection Agencies</span>
                                        <br />
                                        <span class="LabelsBodySmall">Credit Counseling and Credit Repair</span>
                                        <br />
                                        <span class="LabelsBodySmall">Drug Paraphenalia (MOTO/Internet)</span>

                                        <br />
                                        <span class="LabelsBodySmall">Escort Services</span>
                                        <br />
                                        <span class="LabelsBodySmall">Extended Warranty Programs</span>
                                        <br />
                                        <span class="LabelsBodySmall">Factoring, Third Party Processors and Aggregators</span>
                                        <br />
                                        <span class="LabelsBodySmall">Firearms and Ammunition (MOTO/Internet)</span>
                                        <br />
                                        <span class="LabelsBodySmall">Gambling (MOTO/Internet)</span>
                                        <br />
                                        <span class="LabelsBodySmall">Herbal Insense</span>
                                        <br />
                                        <span class="LabelsBodySmall">Loading of prepaid cards or mobile wallets</span>
                                        <br />
                                        <span class="LabelsBodySmall">Loan Modification</span>
                                        <br />
                                        <span class="LabelsBodySmall">Marijuana Dispensaries</span>
                                        <br />
                                        <span class="LabelsBodySmall">Membership or Future Delivery beyond 1 year</span>
                                        <br />
                                        <span class="LabelsBodySmall">Natural Sexual Enhancement Products</span>
                                        <br />
                                        <span class="LabelsBodySmall">Pharmacies (MOTO/Internet)</span>
                                        <br />
                                        <span class="LabelsBodySmall">Pre-paid phone cards</span>
                                        <br/>
                                        <span class="LabelsBodySmall">Pyramid Sales</span>
                                        <br />
                                        <span class="LabelsBodySmall">Racism, Violence, Abuse, Discrimination or Other Immoral Activities</span>
                                        <br />
                                        <span class="LabelsBodySmall">Replica and Counterfeit Products</span>
                                        <br />
                                        <span class="LabelsBodySmall">Technical Support (MOTO/Internet)</span>
                                        <br />
                                        <span class="LabelsBodySmall">Timeshare Programs</span>
                                        <br />
                                        <span class="LabelsBodySmall">Tobacco Products (MOTO/Internet)</span>
                                        <br /> 
                                        <span class="LabelsBodySmall">Travel Clubs</span>
                                        <br />
                                    </div>
                                </div>
                                <br />
                                <asp:RadioButton runat="server" ID="rbtnUnaccBusY" Text="Yes" GroupName="rbtnUnaccBus"
                                    AutoPostBack="true" OnCheckedChanged="rdb_CheckedChanged" />
                                <asp:RadioButton runat="server" ID="rbtnUnaccBusN" Text="No" GroupName="rbtnUnaccBus"
                                    AutoPostBack="true" OnCheckedChanged="rdb_CheckedChanged" />
                                <br />
                                <br />

                                <asp:Panel ID="pnlCanada" runat="server" Width="80%" Visible="False">
                                    <span class="LabelsBody">For customers outside the United States, is your business <i>
                                        based</i> in Canada?</span>
                                    <br />
                                    <asp:RadioButton ID="rdbCanadaYes" runat="server" GroupName="Country" Text="Yes"
                                        OnCheckedChanged="rdb_CheckedChanged" AutoPostBack="True" />
                                    <asp:RadioButton ID="rdbCanadaNo" runat="server" GroupName="Country" Text="No" 
                                        OnCheckedChanged="rdb_CheckedChanged" AutoPostBack="True" />
                                    <br />
                                    <br />
                                    <div style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid;
                                        width: 80%; border-bottom: silver 1px solid; background-color: #ffffce">
                                        <span class="LabelsBodySmall">For Merchants outside the US or Canada, we do not process
                                            Card Present Accounts or sell equipment outside the US.</span><br />
                                    </div>
                                    <br />
                                                                    <asp:Panel ID="pnl_AppFee" runat="server" Width="80%" Visible="False">
                                    <span class="LabelsBody">For customers outside the United States, are you willing to pay for a required $200 application fee?</span>
                                    <br />
                                    <asp:RadioButton ID="rdb_AppFeeYes" runat="server" GroupName="AppFee" Text="Yes"
                                        OnCheckedChanged="rdb_CheckedChanged" AutoPostBack="True" />
                                    <asp:RadioButton ID="rdb_AppFeeNo" runat="server" GroupName="AppFee" Text="No" 
                                        OnCheckedChanged="rdb_CheckedChanged" AutoPostBack="True" />
                                </asp:Panel>
                                <br/>
                                </asp:Panel>
                                <asp:Panel ID="pnl_MonVolume" runat="server" Width="80%" Visible="False">
                                    <span class="LabelsBody">Do you currently or plan to process an average of $50,000 or more in credit card volume per month?</span>
                                    <br />
                                    <asp:RadioButton ID="rdbMonVolYes" runat="server" GroupName="MonthVol" Text="Yes"
                                        OnCheckedChanged="rdb_CheckedChanged" AutoPostBack="True" />
                                    <asp:RadioButton ID="rdbMonVolNo" runat="server" GroupName="MonthVol" Text="No" 
                                        OnCheckedChanged="rdb_CheckedChanged" AutoPostBack="True" />
                                </asp:Panel>
                                <br/>
                                <asp:Panel ID="pnlOtherCountries" runat="server" Visible="False" Width="80%">
                                    <span class="LabelsBody">Is your business <i>based</i> in one of the following countries?</span><br />
                                    <asp:LinkButton runat="server" CssClass="Link" ID="lnkbtnViewAcc" OnClientClick="return false;">View List of Acceptable Countries</asp:LinkButton>
                                    <div id="flyoutAcc" style="z-index: 2; display: none; border: solid 1px #D0D0D0;
                                        background-color: #FFFFFF; overflow: hidden;">
                                        &nbsp;</div>
                                    <div id="infoAcc" style="z-index: 2; display: none; font-size: 12px; border: solid 1px #CCCCCC;
                                        background-color: #ffffff; width: 350px; padding: 5px; font-family: Arial; font-size: 10pt;">
                                        <div style="float: right; opacity: 0; filter: alpha(opacity=0);" id="btnCloseParentAcc">
                                            <asp:LinkButton ID="btnCloseAcc" runat="server" OnClientClick="return false;" Text=""
                                                Style="text-align: right; border: outset thin white; padding: 5px; text-decoration: none;
                                                background-color: #666666; color: White; text-align: center; font-weight: bold;"
                                                ToolTip="Close">X</asp:LinkButton>
                                        </div>
                                        <b><span style="font-family: Arial; font-size: small">Acceptable Countries</span></b>
                                        <span class="LabelsBodySmall">
                                            <table class="DivGreen">
                                                <tr>
                                                    <td width="33%">
                                                        <b>Europe</b><br />
                                                        Andorra<br />
                                                        Austria<br />
                                                        Belgium<br />
                                                        Channel Islands<br />
                                                        Croatia<br />
                                                        Czech Republic<br />
                                                        Denmark<br />
                                                        Estonia<br />
                                                        Finland<br />
                                                        France<br />
                                                        Germany<br />
                                                        Gibraltar<br />
                                                        Greece<br />
                                                        Hungary<br />
                                                        Iceland<br />
                                                        Ireland<br />
                                                        Israel<br />
                                                        Italy<br />
                                                        Kosovo<br />
                                                        Latvia<br />
                                                    </td>
                                                    <td width="33%">
                                                        Lithuania<br />
                                                        Luxembourg<br />
                                                        Malta<br />
                                                        Monaco<br />
                                                        Norway<br />
                                                        Poland<br />
                                                        Portugal<br />
                                                        Russia<br />
                                                        San Marino<br />
                                                        Slovakia<br />
                                                        Slovenia<br />
                                                        Spain<br />
                                                        Sweden<br />
                                                        Switzerland<br />
                                                        The Netherlands<br />
                                                        Turkey<br />
                                                        United Kingdom<br />
                                                        <br />
                                                        <b>North America</b><br />
                                                        Canada<br />
                                                        United States<br />
                                                    </td>
                                                    <td width="34%">
                                                        <b>Latin America</b><br />
                                                        Argentina<br />
                                                        Brazil<br />
                                                        Mexico<br />
                                                        <br />
                                                        <b>Asia Pacific</b><br />
                                                        Australia<br />
                                                        Hong Kong<br />
                                                        Japan<br />
                                                        Korea<br />
                                                        Malaysia<br />
                                                        New Zealand<br />
                                                        Singapore<br />
                                                        Taiwan<br />
                                                        Thailand<br />
                                                        <br />
                                                        China (by special request)<br />
                                                    </td>
                                                </tr>
                                            </table>
                                        </span>
                                    </div>
                                    <br />
                                    <asp:RadioButton ID="rdbOtherYes" runat="server" GroupName="OtherCountry" Text="Yes"
                                        OnCheckedChanged="rdb_CheckedChanged" AutoPostBack="True" />
                                    <asp:RadioButton ID="rdbOtherNo" runat="server" GroupName="OtherCountry" Text="No"
                                        OnCheckedChanged="rdb_CheckedChanged" AutoPostBack="True" />
                                    <br />
                                    <br />
                                </asp:Panel>
                                <asp:Panel ID="pnlUK" runat="server" Visible="False" Width="80%">
                                    <span class="LabelsBody">Are you <i>incorporated</i> or willing to incorporate in any of the following countries?</span>
                                    <asp:LinkButton runat="server" CssClass="Link" ID="lnkbtnIncorp" OnClientClick="return false;">View List of Countries</asp:LinkButton>
                                    <div id="flyoutIncorp" style="z-index: 2; display: none; border: solid 1px #D0D0D0;
                                        background-color: #FFFFFF; overflow: hidden;">
                                        &nbsp;</div>
                                    <div id="infoIncorp" style="z-index: 2; display: none; font-size: 12px; border: solid 1px #CCCCCC;
                                        background-color: #ffffff; width: 350px; padding: 5px; font-family: Arial; font-size: 10pt;">
                                        <div style="float: right; filter: alpha(opacity=0);" id="btnCloseParentIncorp">
                                        <asp:LinkButton ID="btnCloseIncorp" runat="server" OnClientClick="return false;" Text=""
                                                Style="text-align: right; border: outset thin white; padding: 5px; text-decoration: none;
                                                background-color: #666666; color: White; text-align: center; font-weight: bold;"
                                                ToolTip="Close">X</asp:LinkButton>
                                        </div>
                                        <b><span style="font-family: Arial; font-size: small">Acceptable Countries</span></b>
                                        <span class="LabelsBodySmall">
                                            <table class="DivGreen">
                                                <tr>
                                                    <td width="33%">
                                                        Andorra<br />
                                                        Austria<br />
                                                        Belgium<br />
                                                        Bulgaria<br />
                                                        Channel Islands (Guernsey and Jersey)<br />
                                                        Croatia<br />
                                                        Cypress<br />
                                                        Czech Republic<br />
                                                        Denmark<br />
                                                        Estonia<br />
                                                        Finland<br />
                                                        France<br />
                                                        Germany<br />
                                                    </td>
                                                    <td width="33%">
                                                        Gibraltar<br />
                                                        Greece<br />
                                                        Hungary<br />
                                                        Iceland<br />
                                                        Ireland<br />
                                                        Israel (only VISA)<br />
                                                        Italy<br />  
                                                        Kosovo<br />
                                                        Latvia<br />
                                                        Lithuania<br />
                                                        Luxembourg<br />
                                                        Malta<br />
                                                        Monaco<br />
                                                        Norway<br />
                                                    </td>
                                                    <td width="34%">
                                                        Poland<br />
                                                        Portugal<br />
                                                        Romania<br />
                                                        Russia<br />
                                                        San Marino<br />
                                                        Slovakia<br />
                                                        Slovenia<br />
                                                        Spain<br />
                                                        Sweden<br />
                                                        Switzerland<br />
                                                        The Netherlands<br />
                                                        The United Kingdom (including Isle of Man)<br />
                                                        Turkey<br />
                                                    </td>
                                                </tr>
                                            </table>
                                        </span>
                                    </div>
                                    <br /><asp:HyperLink ID="HyperLink1" CssClass="Link" Text="Click here to incorporate in the EU"
                                        NavigateUrl="http://fletcherkennedy.com/payvision-client.html" runat="server"
                                        Target="_blank" /><br />
                                    <asp:RadioButton ID="rdbUKYes" runat="server" GroupName="UK" Text="Yes" 
                                        OnCheckedChanged="rdb_CheckedChanged" AutoPostBack="True" />
                                    <asp:RadioButton ID="rdbUKNo" runat="server" GroupName="UK" Text="No" 
                                        OnCheckedChanged="rdb_CheckedChanged" AutoPostBack="True" />
                                    <br />
                                    <br />
                                </asp:Panel>
                                <asp:Panel ID="pnlUnAcceptableCountries" runat="server" Visible="False" Width="80%">
                                    <span class="LabelsBody">Is your business <i>based</i> in one of the following unacceptable
                                        countries?</span><br />
                                    <asp:LinkButton runat="server" CssClass="Link" ID="lnkbtnViewUnAccCountries" OnClientClick="return false;"
                                        Font-Names="Arial" Font-Size="small">View List of Unacceptable Countries</asp:LinkButton>
                                    <div id="flyoutUnAccCountries" style="z-index: 2; display: none; border: solid 1px #D0D0D0;
                                        background-color: #FFFFFF; overflow: hidden;">
                                        &nbsp;</div>
                                    <div id="infoUnAccCountries" style="z-index: 2; display: none; font-size: 12px; border: solid 1px #CCCCCC;
                                        background-color: #ffffff; width: 350px; padding: 5px; font-family: Arial; font-size: 10pt;">
                                        <div style="float: right; opacity: 0; filter: alpha(opacity=0);" id="btnCloseParentUnAccCountries">
                                            <asp:LinkButton ID="btnCloseUnAccCountries" runat="server" OnClientClick="return false;"
                                                Text="" Style="text-align: right; border: outset thin white; padding: 5px; text-decoration: none;
                                                background-color: #666666; color: White; text-align: center; font-weight: bold;"
                                                ToolTip="Close">X</asp:LinkButton>
                                        </div>
                                        <b><span style="font-family: Arial; font-size: small">Unacceptable Countries</span></b>
                                        <span class="LabelsBodySmall">
                                            <table class="DivGreen">
                                                <tr>
                                                    <td width="30%">
                                                        Afghanistan<br />
                                                        Albania<br />
                                                        Algeria<br />
                                                        Angola<br />
                                                        Armenia<br />
                                                        Azerbaijan<br />
                                                        Bangladesh<br />
                                                        Belarus<br />
                                                        Bosnia-Herzegovina<br />
                                                        Brazzaville<br/>
                                                        Bulgaria<br />
                                                        Burundi<br />
                                                        Cambodia<br />
                                                        Congo Brazzaville<br />
                                                        Cook Islands<br />
                                                        Cuba<br />
                                                        Egypt<br />
                                                        Eritrea<br />
                                                        Ethiopia<br />
                                                        Georgia<br />
                                                    </td>
                                                    <td width="30%">
                                                        Guatemala<br />
                                                        Haiti<br />
                                                        India<br/>
                                                        Indonesia<br />
                                                        Iran<br />
                                                        Iraq<br />
                                                        Kazakhstan<br />
                                                        Kyrgyzstan<br />
                                                        Laos<br />
                                                        Liberia<br />
                                                        Libya<br />
                                                        Macedonia<br />
                                                        Malaysia<br />
                                                        Moldova<br />
                                                        Mongolia<br />
                                                        Myanmar (Burma)<br />
                                                        Nauru<br />
                                                        Nigeria<br />
                                                        North Korea<br />
                                                        Pakistan<br />
                                                    </td>
                                                    <td width="40%">
                                                        Philippines<br />
                                                        Republic of Central Africa<br />
                                                        Romania<br />
                                                        Russian Federation<br />
                                                        Rwanda<br />
                                                        Serbia and Montenegro<br />
                                                        Sierra Leone<br />
                                                        Sudan<br />
                                                        Surinam<br />
                                                        Syria<br />
                                                        Tajikistan<br />
                                                        Turkmenistan<br />
                                                        Ukraine<br />
                                                        Uzbekistan<br />
                                                        Venezuela<br />
                                                        Yemen<br />
                                                        Yugoslavia<br />
                                                        Zaire<br />
                                                        Zimbabwe<br />
                                                    </td>
                                                </tr>
                                            </table>
                                        </span>
                                    </div>
                                    <br />
                                    <asp:RadioButton ID="rdbUnAccCountryYes" runat="server" GroupName="UnAccCountry"
                                        Text="Yes" OnCheckedChanged="rdb_CheckedChanged" AutoPostBack="True" />
                                    <asp:RadioButton ID="rdbUnAccCountryNo" runat="server" GroupName="UnAccCountry" Text="No"
                                        OnCheckedChanged="rdb_CheckedChanged" AutoPostBack="True" />
                                    <br />
                                    <br />
                                </asp:Panel>
                                <!--<asp:Panel ID="pnlProcess50K" runat="server" Visible="False" Width="80%">
                                    <span class="LabelsBody">Do you process or plan to process <i>more than $50,000 per month</i>?</span>
                                    <br />
                                    <asp:RadioButton ID="rdbProcess50KYes" runat="server" GroupName="Opt50K" Text="Yes" Font-Names="Arial"
                                        Font-Size="11pt" OnCheckedChanged="rdb_CheckedChanged" AutoPostBack="True" />
                                    <asp:RadioButton ID="rdbProcess50KNo" runat="server" GroupName="Opt50K" Text="No" Font-Names="Arial"
                                        Font-Size="11pt" OnCheckedChanged="rdb_CheckedChanged" AutoPostBack="True" />
                                </asp:Panel>
                                <br />-->                                
                                <asp:Panel ID="pnlKitts" runat="server" Visible="False" Width="80%">
                                    <span class="LabelsBody">If no, are you <i>incorporated</i> or willing to incorporate
                                        in the St. Kitts?</span>
                                    <br />
                                    <asp:HyperLink ID="HyperLink2" CssClass="Link" Text="Click here to incorporate in the St. Kitts"
                                        NavigateUrl="http://www.corporationcentre.ca/docen/pinc/home.asp?t=offshore"
                                        runat="server" Target="_blank" /><br />
                                    <asp:RadioButton ID="rdbKittsYes" runat="server" GroupName="Kitts" Text="Yes" 
                                        OnCheckedChanged="rdb_CheckedChanged" AutoPostBack="True" />
                                    <asp:RadioButton ID="rdbKittsNo" runat="server" GroupName="Kitts" Text="No" 
                                        OnCheckedChanged="rdb_CheckedChanged" AutoPostBack="True" />
                                    <br />
                                    <br />
                                </asp:Panel>
                                <asp:Panel ID="pnlLinks" runat="server" Visible="False" Width="80%">
                                    <span class="LabelsBody">In order for us to process your application 
                                        you must incorporate in a region that best fits your business. Please select EU or St.Kitts below.
                                        <br />
                                        <asp:RadioButton ID="rdbLinksEU" runat="server" GroupName="Links" Text="EU" 
                                            OnCheckedChanged="rdb_CheckedChanged" AutoPostBack="True" />
                                        <asp:HyperLink ID="lnkEU" CssClass="Link" Text="Click here to incorporate in the EU"
                                            NavigateUrl="http://fletcherkennedy.com/payvision-client.html" runat="server"
                                            Target="_blank" /><br />
                                        <asp:RadioButton ID="rdbLinksStKitts" runat="server" GroupName="Links" Text="St. Kitts"
                                            OnCheckedChanged="rdb_CheckedChanged" AutoPostBack="True" />
                                        <asp:HyperLink ID="lnkKitts" CssClass="Link" Text="Click here to incorporate in the St. Kitts"
                                            NavigateUrl="http://www.corporationcentre.ca/docen/pinc/home.asp?t=offshore"
                                            runat="server" Target="_blank" />
                                    </span>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <br />
                        <asp:Button ID="btnStartOver" runat="server" Text="Start Over" OnClick="btnStart_Click" />
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                        <asp:Button ID="btnContinue" runat="server" Text="Continue" OnClick="btnContinue_Click" />
                    </td>
                </tr>
            </table>
        </center>
    </div>
</asp:Content>
