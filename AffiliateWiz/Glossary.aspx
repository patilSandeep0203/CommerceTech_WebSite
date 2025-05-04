<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Glossary.aspx.cs" Inherits="Glossary_Glossary" 
    Title="Glossary" Theme="Admin"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <br />
    <table align="right" width="98%" cellspacing="0" cellpadding="0" border="0">
	<tr>
		<td align="center" valign="top">
		<span class="LabelsHeaderLarge"><b>E-Glossary</b></span><br /><br />
		<span class="LabelsBody">
		<asp:LinkButton ID="lnkA" CssClass="Link" runat="server" OnClick="lnkA_Click">A</asp:LinkButton> 
		| <asp:LinkButton ID="lnkB" CssClass="Link" runat="server" OnClick="lnkB_Click">B</asp:LinkButton> 
		| <asp:LinkButton ID="lnkC" CssClass="Link" runat="server" OnClick="lnkC_Click">C</asp:LinkButton>
		| <asp:LinkButton ID="lnkD" CssClass="Link" runat="server" OnClick="lnkD_Click">D</asp:LinkButton> 
		| <asp:LinkButton ID="lnkE" CssClass="Link" runat="server" OnClick="lnkE_Click">E</asp:LinkButton> 
		| <asp:LinkButton ID="lnkF" CssClass="Link" runat="server" OnClick="lnkF_Click">F</asp:LinkButton>
		| <asp:LinkButton ID="lnkG" CssClass="Link" runat="server" OnClick="lnkG_Click">G</asp:LinkButton> 
		| <asp:LinkButton ID="lnkH" CssClass="Link" runat="server" OnClick="lnkH_Click">H</asp:LinkButton> 
		| <asp:LinkButton ID="lnkI" CssClass="Link" runat="server" OnClick="lnkI_Click">I</asp:LinkButton>
		| <asp:LinkButton ID="lnkJ" CssClass="Link" runat="server" OnClick="lnkJ_Click">J</asp:LinkButton> 
		| <asp:LinkButton ID="lnkK" CssClass="Link" runat="server" OnClick="lnkK_Click">K</asp:LinkButton> 
		| <asp:LinkButton ID="lnkL" CssClass="Link" runat="server" OnClick="lnkL_Click">L</asp:LinkButton>
		| <asp:LinkButton ID="lnkM" CssClass="Link" runat="server" OnClick="lnkM_Click">M</asp:LinkButton> 
		| <asp:LinkButton ID="lnkN" CssClass="Link" runat="server" OnClick="lnkN_Click">N</asp:LinkButton> 
		| <asp:LinkButton ID="lnkO" CssClass="Link" runat="server" OnClick="lnkO_Click">O</asp:LinkButton>
		| <asp:LinkButton ID="lnkP" CssClass="Link" runat="server" OnClick="lnkP_Click">P</asp:LinkButton> 
		| <asp:LinkButton ID="lnkQ" CssClass="Link" runat="server" OnClick="lnkQ_Click">Q</asp:LinkButton> 
		| <asp:LinkButton ID="lnkR" CssClass="Link" runat="server" OnClick="lnkR_Click">R</asp:LinkButton>
		| <asp:LinkButton ID="lnkS" CssClass="Link" runat="server" OnClick="lnkS_Click">S</asp:LinkButton> 
		| <asp:LinkButton ID="lnkT" CssClass="Link" runat="server" OnClick="lnkT_Click">T</asp:LinkButton> 
		| <asp:LinkButton ID="lnkU" CssClass="Link" runat="server" OnClick="lnkU_Click">U</asp:LinkButton>
		| <asp:LinkButton ID="lnkV" CssClass="Link" runat="server" OnClick="lnkV_Click">V</asp:LinkButton> 
		| <asp:LinkButton ID="lnkW" CssClass="Link" runat="server" OnClick="lnkW_Click">W</asp:LinkButton> 
		| <asp:LinkButton ID="lnkX" CssClass="Link" runat="server" OnClick="lnkX_Click">X</asp:LinkButton>
		| <asp:LinkButton ID="lnkY" CssClass="Link" runat="server" OnClick="lnkY_Click">Y</asp:LinkButton> 
		| <asp:LinkButton ID="lnkZ" CssClass="Link" runat="server" OnClick="lnkZ_Click">Z</asp:LinkButton> 
		</span>
		<br /></td>
	</tr>	
	<tr><td height="15px"></td></tr>
	<asp:Panel ID="pnlDetail" runat="server">
	<tr>
		<td align="left" valign="top">     
		<span class="LabelsBody">  
		E-commerce is a general concept covering any business transaction executed electronically between parties such as companies (business-to-business), companies and consumers (business-to-consumer),
		consumers and consumers, business and the public sector, and between consumers and the public sector. 
		<br />
		<br />
		Electronic commerce comprises electronic trading of goods and services and falls into two categories:<br /></span>
		<ul class="Bullet">
			<li class="LabelsBody"><b>Indirect e-commerce:</b> electronic ordering of tangible goods; the actual goods are delivered in a traditional form (ie: USPS, etc.)</li>
			<li class="LabelsBody"><b>Direct e-commerce:</b> electronic ordering and delivery of intangible goods (electronic material ie: memberhsips, games, softwares, etc.)</li>
		</ul><br />
		<span class="LabelsBody">Even though e-commerce predates the Web for some time, it is the Web technology and its general access towards open network standards that today are the driving forces of e-commerce and that have
		put it on the global agenda. Historically, before e-commerce existed on the Internet, it was used mainly for business-to-business transactions in different forms of closed networks. 
		<br />
		<br />
		For the consumer, it is easy to appreciate the importance of e-commerce. Why waste time fighting the very real crowds in supermarkets, when, from the comfort of home, one can shop on-line at any time in
		virtual internet shopping malls, and have the goods delivered home directly. 
		<br />
		<br />
		For business, e-commerce offers enormous opportunities. It allows to trade at a low cost world-wide and it offers enterprises the chance to enter a global market right
		from start-up.
		</span>
		</td>
	</tr>
	</asp:Panel>
	<tr>
	    <td>
	        <asp:Table ID="tblGlossary" runat="server" CellPadding="2" CellSpacing="0" style="width:100%; ">
            </asp:Table>
	    </td>
	</tr>
	<tr>
		<td align="center" valign="bottom" colspan="4">
		<br />
		<br />
		<a href="javascript:window.history.back();" onMouseOver='swtch(23,back); status=" Back to previous page "; return true;' onMouseOut='swtch(24,back); status=" ";'>
		<img src="images/back-on.gif" name="back" width="75" height="25" alt="" border="0"></a>
		<br />
		<br />
		</td>
	</tr>	
    </table>
</asp:Content>
