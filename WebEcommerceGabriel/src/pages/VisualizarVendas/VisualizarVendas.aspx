<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="VisualizarVendas.aspx.vb" Inherits="WebEcommerceGabriel.VisualizarVendas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
	<%@ Register TagPrefix="gp" TagName="TituloMedio" Src="~/src/components/TituloMedio/TituloMedio.ascx" %>
	<link rel="stylesheet" href="/src/pages/VisualizarVendas/VisualizarVendas.css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<asp:UpdatePanel runat="server">
		<ContentTemplate>
			<asp:Panel runat="server" ID="ListsagemVendasPanel">
				<gp:TituloMedio runat="server" ID="VendasTituloMedio" Text="Relação das vendas"></gp:TituloMedio>

				<div class="table-container">
					<asp:GridView
						ID="VendasGW"
						runat="server"
						AutoGenerateColumns="False"
						OnRowCommand="VendasGW_RowCommand"
						Width="100%">
						<HeaderStyle BackColor="#212529" ForeColor="White" Font-Bold="True" />
						<Columns>
							<asp:BoundField DataField="IdVenda" HeaderText="Número" />
							<asp:BoundField DataField="NomeCliente" HeaderText="Cliente" />
							<asp:BoundField DataField="ValorTotal" HeaderText="Valor Total" DataFormatString="{0:c}" />
							<asp:TemplateField>
								<ItemTemplate>
									<asp:LinkButton runat="server" ID="VisualizarVendaButton"
										Text="Visualizar"
										CommandName="Visualizar"
										CommandArgument='<%# Eval("IdVenda") %>'>
									</asp:LinkButton>
								</ItemTemplate>
							</asp:TemplateField>
						</Columns>
						<EmptyDataTemplate>
							<div class="empty-data">
								Nenhuma venda encontrado.
							</div>
						</EmptyDataTemplate>
					</asp:GridView>
				</div>

				<div class="total-venda">
					<asp:Label runat="server" ID="TotalLabel" CssClass="total-venda-label" Text="Total: R$ 0,00" DataFormatString="{0:C}"></asp:Label>
				</div>
			</asp:Panel>
		</ContentTemplate>
	</asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageScripts" runat="server">
</asp:Content>

