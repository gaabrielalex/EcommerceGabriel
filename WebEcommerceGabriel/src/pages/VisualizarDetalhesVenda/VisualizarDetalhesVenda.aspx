<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="VisualizarDetalhesVenda.aspx.vb" Inherits="WebEcommerceGabriel.VisualizarDetalhesVenda" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
	<%@ Register TagPrefix="gp" TagName="TituloMedio" Src="~/src/components/TituloMedio/TituloMedio.ascx" %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<asp:UpdatePanel runat="server">
		<ContentTemplate>
			<div style="display: flex; justify-content: space-between; flex-direction: row">
				<div>
					<gp:TituloMedio runat="server" ID="DetalhesVendaTituloMedio" Text="Detalhes da Venda"></gp:TituloMedio>

					<div>
						<strong>Número da Venda:</strong>
						<asp:Label runat="server" ID="NumeroVendaLabel" />
						<br />
						<strong>Data da Venda:</strong>
						<asp:Label runat="server" ID="DataVendaLabel" />
						<br />
						<strong>Cliente:</strong>
						<asp:Label runat="server" ID="ClienteLabel" />
					</div>

					<br />
					<br />
					<div style="margin-bottom: 25px">
						<h3>Itens da Venda</h3>
					</div>

					<asp:GridView ID="ItensVendaGW" runat="server" AutoGenerateColumns="False"
						Width="100%"
						Style="width: 700px; box-sizing: border-box;">
						<HeaderStyle BackColor="#212529" ForeColor="White" Font-Bold="True" />
						<Columns>
							<asp:BoundField DataField="Produto.IdProduto" HeaderText="Código" />
							<asp:BoundField DataField="Produto.Descricao" HeaderText="Descrição" />
							<asp:BoundField DataField="PrecoUnitario" HeaderText="Preço Vendido" DataFormatString="{0:c}" />
							<asp:BoundField DataField="Quantidade" HeaderText="Quantidade" />
							<asp:BoundField DataField="VlrTotalItem" HeaderText="Valor Total" DataFormatString="{0:c}" />
						</Columns>
					</asp:GridView>
				</div>

				<div style="margin-top: 40px">
					<asp:Button runat="server" ID="VoltarButton" Text="Voltar" CssClass="btn btn-secondary voltar-button"
						CausesValidation="false" OnClick="VoltarButton_Click" />
				</div>
			</div>
		</ContentTemplate>
	</asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageScripts" runat="server">
</asp:Content>
