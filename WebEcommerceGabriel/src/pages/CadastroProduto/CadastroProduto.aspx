<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="CadastroProduto.aspx.vb" Inherits="WebEcommerceGabriel.CadastroProduto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
	<%@ Register TagPrefix="gp" TagName="TextFormField" Src="~/src/components/TextFormField/TextFormField.ascx" %>
	<%@ Register TagPrefix="gp" TagName="TituloMedio" Src="~/src/components/TituloMedio/TituloMedio.ascx" %>

	<link rel="stylesheet" href="/src/pages/CadastroProduto/CadastroProduto.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<asp:UpdatePanel runat="server">
		<ContentTemplate>
			<asp:Panel runat="server" ID="FormAddEditProdutoPanel" DefaultButton="SalvarButton">
				<gp:TituloMedio runat="server" ID="FormAddEditProdutoTituloMedio"></gp:TituloMedio>
				<div class="conteudo-FormAddEditProdutoPanel">
					<div class="row">
						<gp:TextFormField runat="server" ID="DescricaoTextFormField" LabelText="Descrição"
							ValidationGroup="CamposProduto" OnServerValidate="DescricaoTextFormField_ServerValidate" />
					</div>
					<div class="row">
						<gp:TextFormField runat="server" ID="VlrUnitarioTextFormField" LabelText="Valor Unitário" Format="dinheiro"
							ValidationGroup="CamposProduto" OnServerValidate="VlrUnitarioTextFormField_ServerValidate" />
					</div>
					<div class="row">
						<div class="buttons-FormAddEditProdutoPanel">
							<asp:Button runat="server" ID="CancelarButton" ValidationGroup="CamposProduto" Text="Cancelar" OnClick="CancelarButton_Click"
								CssClass="btn btn-secondary SubmitButtonModalFormAddEditProduto" CausesValidation="false" />
							<asp:Button runat="server" ID="SalvarButton" ValidationGroup="CamposProduto" Text="Salvar" OnClick="SalvarButton_Click"
								CssClass="btn btn-primary SubmitButtonModalFormAddEditProduto" />
						</div>
					</div>
				</div>
			</asp:Panel>
		</ContentTemplate>
	</asp:UpdatePanel>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="PageScripts" runat="server">
</asp:Content>
