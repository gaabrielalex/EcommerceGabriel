Imports ModelsEcommerceGabrielX
Imports ServicesEcommerceGabrielX
Imports UtilsEcommerceGabrielX
Imports WebEcommerceGabriel.MensagemInfo

Public Class VisualizarDetalhesVenda
	Inherits System.Web.UI.Page

	Private ReadOnly _vendaService As VendaService = VendaServiceFactory.Criar()
	Private ReadOnly _errorHandler As ErrorHandler = New ErrorHandler()

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If Not Page.IsPostBack Then
			Dim idVenda As Integer = Convert.ToInt32(Request.QueryString("idVenda"))

			CarregarDetalhesVenda(idVenda)
		End If
	End Sub

	Private Sub CarregarDetalhesVenda(idVenda As Integer)
		Try
			Dim venda As Venda = _vendaService.ObterPorId(idVenda)

			NumeroVendaLabel.Text = venda.IdVenda.ToString()
			DataVendaLabel.Text = venda.DataVenda.Value.ToString("dd/MM/yyyy")
			ClienteLabel.Text = venda.NomeCliente

			ItensVendaGW.DataSource = venda.ItensVenda
			ItensVendaGW.DataBind()
		Catch ex As Exception
			PageUtils.MostrarMensagemViaToast(_errorHandler.HandleErrorMessage("Houve um erro ao carregar os detalhes da venda", ex), TiposMensagem.Erro, Page)
		End Try
	End Sub

	Protected Sub VoltarButton_Click(sender As Object, e As EventArgs)
		PageUtils.RedirecionarParaPagina(
			page:=Me,
			request:=Request,
			urlPagina:="~/src/pages/VisualizarVendas/VisualizarVendas"
		)
	End Sub

End Class