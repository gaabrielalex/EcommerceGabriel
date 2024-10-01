Imports ModelsEcommerceGabrielX
Imports ServicesEcommerceGabrielX
Imports UtilsEcommerceGabrielX
Imports WebEcommerceGabriel.MensagemInfo

Public Class VisualizarVendas
	Inherits System.Web.UI.Page

	Private ReadOnly _vendaService As VendaService = VendaServiceFactory.Criar()
	Private ReadOnly _errorHandler As ErrorHandler = New ErrorHandler()

	Public Property ListaVendas As List(Of Venda)
		Get
			If (ViewState("ListaVendas") IsNot Nothing) Then
				Return CType(ViewState("ListaVendas"), List(Of Venda))
			End If
			Return New List(Of Venda)
		End Get
		Set(value As List(Of Venda))
			ViewState("ListaVendas") = value
			AtualizarLabelTotalVenda()
		End Set
	End Property
	Private Sub AtualizarLabelTotalVenda()
		Dim vlrTotal As Decimal = CalcularTotal()
		Dim vlrTotalString As String = vlrTotal.ToString("C2", System.Globalization.CultureInfo.CreateSpecificCulture("pt-BR"))

		TotalLabel.Text = "Total: " + vlrTotalString
	End Sub

	Private Function CalcularTotal() As Decimal
		Dim total As Decimal = 0
		For Each venda As Venda In ListaVendas
			total += venda.ValorTotal
		Next

		Return total
	End Function

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If Not Page.IsPostBack Then
			CarregarVendas()
		End If
	End Sub

	Private Sub CarregarVendas()
		Try
			ListaVendas = _vendaService.ListarTodos()
			BindData()
		Catch ex As Exception
			PageUtils.MostrarMensagemViaToast(_errorHandler.HandleErrorMessage("Houve um erro ao carregar as vendas", ex), TiposMensagem.Erro, Me)
		End Try
	End Sub

	Private Sub BindData()
		VendasGW.DataSource = ListaVendas
		VendasGW.DataBind()
	End Sub

	Protected Sub VendasGW_RowCommand(sender As Object, e As GridViewCommandEventArgs)
		If e.CommandName = "Visualizar" Then
			Dim idVenda As Integer = Convert.ToInt32(e.CommandArgument)

			Response.Redirect("~/src/pages/VisualizarDetalhesVenda/VisualizarDetalhesVenda.aspx?idVenda=" & idVenda)
		End If
	End Sub
End Class