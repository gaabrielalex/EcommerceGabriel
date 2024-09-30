<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="CadastroProduto.aspx.vb" Inherits="WebEcommerceGabriel.CadastroProduto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
	<%@ Register TagPrefix="gp" TagName="TextFormField" Src="~/src/components/TextFormField/TextFormField.ascx" %>
	<%@ Register TagPrefix="gp" TagName="TituloMedio" Src="~/src/components/TituloMedio/TituloMedio.ascx" %>

	<link rel="stylesheet" href="/src/pages/CadastroProduto/CadastroProduto.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<asp:UpdatePanel runat="server">
		<ContentTemplate>
			<div style="display:flex; justify-content: space-between; flex-direction: row">
				<asp:Panel runat="server" ID="FormAddEditProdutoPanel" DefaultButton="SalvarButton">
					<gp:TituloMedio runat="server" ID="FormAddEditProdutoTituloMedio"></gp:TituloMedio>
					<div class="conteudo-FormAddEditProdutoPanel">
						<div class="row row-cols-auto">
							<div class="flex-grow-1">							
								<gp:TextFormField runat="server" ID="CodTextFormField" LabelText="Cod. Produto"
									TextMode="Number" ValidationGroup="CamposProduto" Style="width: 300px" />
							</div>
							<div class="d-flex" style="display: flex; align-items: center; padding-top: 7px">
									<asp:Button runat="server" ID="BuscarButton" Text="Buscar" CssClass="btn btn-primary" OnClick="BuscarButton_Click" 
									CausesValidation="false" Style="height: 37px"/>
							</div>
						</div>
						<div class="row">
							<gp:TextFormField runat="server" ID="DescricaoTextFormField" LabelText="Descrição"
								ValidationGroup="CamposProduto" OnServerValidate="DescricaoTextFormField_ServerValidate" />
						</div>
						<div class="row">
							<gp:TextFormField runat="server" ID="PrecoUnitTextFormField" LabelText="Preço Unitário" Format="dinheiro"
								ValidationGroup="CamposProduto" OnServerValidate="PrecoUnitTextFormField_ServerValidate" />
						</div>
						<div class="row">
							<gp:TextFormField runat="server" ID="SaldoEstoqueTextFormField" LabelText="Saldo Estoque" TextMode="Number"
								ValidationGroup="CamposProduto" OnServerValidate="SaldoEstoqueTextFormField_ServerValidate" />
						</div>
						<div class="row">
							<div class="buttons-FormAddEditProdutoPanel">
								<asp:Button runat="server" ID="CancelarButton" ValidationGroup="CamposProduto" Text="Cancelar" OnClick="CancelarButton_Click"
									CssClass="btn btn-secondary SubmitButtonModalFormAddEditProduto" CausesValidation="false" />
								<asp:Button runat="server" ID="SalvarButton" ValidationGroup="CamposProduto" Text="Salvar" OnClick="SalvarButton_Click"
									CssClass="btn btn-primary SubmitButtonModalFormAddEditProduto" CausesValidation="true" />
							</div>
						</div>
					</div>
				</asp:Panel>
				<div style="margin-top: 40px">
					<asp:Button runat="server" ID="VoltarButton" Text="Voltar" CssClass="btn btn-secondary SubmitButtonModalFormAddEditPedido voltar-button" 
					CausesValidation="false" OnClick="VoltarButton_Click" />
				</div>
			</div>
		</ContentTemplate>
	</asp:UpdatePanel>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="PageScripts" runat="server">
</asp:Content>
