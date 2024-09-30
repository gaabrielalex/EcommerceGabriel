<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="RegistroVenda.aspx.vb" Inherits="WebEcommerceGabriel.RegistroVenda" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
	<%@ Register TagPrefix="gp" TagName="TextFormField" Src="~/src/components/TextFormField/TextFormField.ascx" %>
	<%@ Register TagPrefix="gp" TagName="TituloMedio" Src="~/src/components/TituloMedio/TituloMedio.ascx" %>
	<%@ Register TagPrefix="gp" TagName="DropDownList" Src="~/src/components/GPDropDownList/GPDropDownList.ascx" %>

	<link rel="stylesheet" href="/src/pages/RegistroVenda/RegistroVenda.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<asp:UpdatePanel runat="server">
		<ContentTemplate>
			<div style="display: flex; justify-content: space-between; flex-direction: row">
				<asp:Panel runat="server" ID="RegistroVendaPanel" DefaultButton="GerarVendaButton">
					<gp:TituloMedio runat="server" ID="RegistroVendaTituloMedio" Text="Registro Venda"></gp:TituloMedio>
					<div class="conteudo-RegistroVendaPanel">

						<div class="row">
							<gp:TextFormField runat="server" ID="NomeClienteTextFormField" LabelText="Nome do Cliente"
								ValidationGroup="CamposVenda" OnServerValidate="NomeClienteTextFormField_ServerValidate" />
						</div>

						<hr />
						<br />

						<div class="row row-cols-auto">
							<div class="flex-grow-1">
								<gp:DropDownList runat="server" ID="ProdutosDropDownList" LabelText="Produto"
									ValidationGroup="CamposItemVenda" Style="width: 300px" OnSelectedIndexChanged="ProdutosDropDownList_SelectedIndexChanged" />
							</div>
							<div class="d-flex" style="display: flex; align-items: center; padding-top: 7px">
								<asp:Button runat="server" ID="InserirItemButton" Text="Inserir" CssClass="btn btn-secondary" OnClick="InserirItemButton_Click"
									ValidationGroup="CamposItemVenda" Style="height: 37px" />
							</div>
						</div>

						<div class="row">
							<gp:TextFormField runat="server" ID="QuantidadeTextFormField" ValidationGroup="CamposItemVenda" LabelText="Quantidade" Text="1" TextMode="Number" OnServerValidate="QuantidadeTextFormField_ServerValidate" />
						</div>

						<div class="row">
							<gp:TextFormField runat="server" ID="PrecoUnitTextFormField" LabelText="Preço Unitário" Format="dinheiro" Enabled="false" />
						</div>

						<div class="row">
							<div class="buttons-RegistroVendaPanel">
								<asp:Button runat="server" ID="OKButton" ValidationGroup="CamposVenda" Text="OK" OnClick="VoltarButton_Click"
									CssClass="btn btn-secondary SubmitButtonModalRegistroVenda" CausesValidation="false" />
								<asp:Button runat="server" ID="GerarVendaButton" ValidationGroup="CamposVenda" Text="Gerar Venda" OnClick="GerarVendaButton_Click"
									CssClass="btn btn-primary SubmitButtonModalRegistroVenda" CausesValidation="true" />
							</div>
						</div>
					</div>
				</asp:Panel>

				<div class="itens-venda">
					<div class="table-container">
						<asp:GridView
							ID="ItensVendaGW"
							runat="server"
							AutoGenerateColumns="False"
							Width="100%"
							Style="width: 700px;box-sizing: border-box;">
							<HeaderStyle BackColor="#212529" ForeColor="White" Font-Bold="True" />
							<Columns>
								<asp:BoundField DataField="Produto.Descricao" HeaderText="Produto" />
								<asp:BoundField DataField="Quantidade" HeaderText="Quantidade" />
								<asp:BoundField DataField="PrecoUnitario" HeaderText="Preço Unitário" DataFormatString="{0:C}" />
								<asp:BoundField DataField="VlrTotalItem" HeaderText="Total Item" DataFormatString="{0:C}" />

							</Columns>
							<EmptyDataTemplate>
								<div class="empty-data">
									Nenhum item inserido
								</div>
							</EmptyDataTemplate>
						</asp:GridView>

					</div>
					<div class="total-venda">
						<asp:Label runat="server" ID="TotalVendaLabel" CssClass="total-venda-label" Text="Total Venda: R$ 0,00" DataFormatString="{0:C}"></asp:Label>
					</div>
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
