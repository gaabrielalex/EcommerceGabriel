Imports ModelsEcommerceGabrielX
Imports ServicesEcommerceGabrielX
Imports UtilsEcommerceGabrielX
Imports WebEcommerceGabriel.MensagemInfo

Public Class RegistroVenda
	Inherits System.Web.UI.Page

	Private ReadOnly _vendaService As VendaService = VendaServiceFactory.Criar()
	Private ReadOnly _produtoService As ProdutoService = ProdutoServiceFactory.Criar()
	Private ReadOnly _errorHandler As ErrorHandler = New ErrorHandler()

	Public Property ItensVenda As List(Of ItemVenda)
		Get
			If (ViewState("ItensVenda") IsNot Nothing) Then
				Return CType(ViewState("ItensVenda"), List(Of ItemVenda))
			End If
			Return New List(Of ItemVenda)
		End Get
		Set(value As List(Of ItemVenda))
			ViewState("ItensVenda") = value

			AtualizarLabelTotalVenda()
		End Set
	End Property

	Private Sub AtualizarLabelTotalVenda()
		Dim vlrTotalString As String = CalcularTotalVenda().ToString()
		If vlrTotalString.Equals("0") Then
			vlrTotalString = "0,00"
		End If
		TotalVendaLabel.Text = "Total Venda: R$ " + vlrTotalString
	End Sub

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If Not Page.IsPostBack Then
			BindItensVenda()
			CarregarProdutos()
		End If
	End Sub

	Protected Sub ProdutosDropDownList_SelectedIndexChanged(sender As Object, e As EventArgs)
		Dim idProduto As Integer = ProdutosDropDownList.SelectedValue
		Dim produto As Produto = _produtoService.ObterPorId(idProduto)
		PrecoUnitTextFormField.Text = produto.PrecoUnitario.ToString()
	End Sub

	Private Sub CarregarProdutos()
		Dim produtoService As ProdutoService = ProdutoServiceFactory.Criar()
		Dim produtos As List(Of Produto) = _produtoService.ListarTodos()
		ProdutosDropDownList.DataSource = produtos
		ProdutosDropDownList.DataTextField = "Descricao"
		ProdutosDropDownList.DataValueField = "IdProduto"
		ProdutosDropDownList.DataBind()

		ProdutosDropDownList_SelectedIndexChanged(Nothing, Nothing)
	End Sub

	Protected Sub InserirItemButton_Click(sender As Object, e As EventArgs)
		Dim itemVenda As ItemVenda = ObterItemVenda()

		For Each item As ItemVenda In Me.ItensVenda
			If item.Produto.IdProduto = itemVenda.Produto.IdProduto Then
				PageUtils.MostrarMensagemViaToast("Produto já inserido!", TiposMensagem.Erro, Me)
				Return
			End If
		Next

		Dim saldoEstoque As Integer
		Try
			saldoEstoque = _produtoService.ObterSaldoEstoque(itemVenda.Produto.IdProduto)
			If saldoEstoque < itemVenda.Quantidade Then
				PageUtils.MostrarMensagemViaToast("Saldo em estoque insuficiente! Saldo atual: " + saldoEstoque.ToString(), TiposMensagem.Erro, Me)
				Return
			End If
		Catch ex As Exception
			PageUtils.MostrarMensagemViaToast(_errorHandler.HandleErrorMessage("Erro ao obter saldo em estoque", ex), TiposMensagem.Erro, Me)
		End Try

		If itemVenda.VlrTotalItem >= 1000000 Then
			PageUtils.MostrarMensagemViaToast("Valor total do item não pode ser maior ou igual a 1.000.000,00! Valor informado: " + itemVenda.VlrTotalItem.ToString(), TiposMensagem.Erro, Me)
			Return
		End If

		Dim ItensVenda As List(Of ItemVenda) = Me.ItensVenda
		ItensVenda.Add(itemVenda)
		Me.ItensVenda = ItensVenda

		BindItensVenda()
		ResetarCamposItemVenda()
	End Sub

	Private Sub BindItensVenda()
		ItensVendaGW.DataSource = ItensVenda
		ItensVendaGW.DataBind()
	End Sub

	Private Sub ResetarCamposItemVenda()
		QuantidadeTextFormField.Text = "1"
	End Sub

	Private Function ObterItemVenda() As ItemVenda
		Dim idProduto As Integer = ProdutosDropDownList.SelectedValue
		Dim produto As Produto = _produtoService.ObterPorId(idProduto)
		Dim quantidade As Integer = Integer.Parse(QuantidadeTextFormField.Text)
		Dim precoUnitario As Decimal = Decimal.Parse(PrecoUnitTextFormField.Text)

		Return New ItemVenda(
			idVenda:=1,
			produto:=produto,
			quantidade:=quantidade,
			precoUnitario:=precoUnitario
		)
	End Function

	Private Function CalcularTotalVenda() As Decimal
		Dim total As Decimal = 0
		For Each item As ItemVenda In ItensVenda
			total += item.VlrTotalItem
		Next

		Return total
	End Function

	Protected Sub GerarVendaButton_Click(sender As Object, e As EventArgs)

		If Not Page.IsValid Then
			Return
		End If

		Dim venda As Venda = ObterVenda()
		If venda.ItensVenda Is Nothing OrElse venda.ItensVenda.Count = 0 Then
			PageUtils.MostrarMensagemViaToast("Nenhum item inserido na venda!", TiposMensagem.Erro, Me)
			Return
		End If

		Try
			_vendaService.Inserir(Venda)
			PageUtils.MostrarMensagemViaToast("Venda realizada com sucesso!", TiposMensagem.Sucesso, Me)
			ResetarCamposVenda()
		Catch ex As Exception
			PageUtils.MostrarMensagemViaToast(_errorHandler.HandleErrorMessage("Erro ao realizar venda", ex), TiposMensagem.Erro, Me)
		End Try
	End Sub

	Private Function ObterVenda() As Venda
		Dim itensVenda As List(Of ItemVenda) = Me.ItensVenda
		Dim nomeCliente As String = NomeClienteTextFormField.Text

		Return New Venda(
			nomeCliente:=nomeCliente,
			valorTotal:=CalcularTotalVenda(),
			itensVenda:=itensVenda
		)
	End Function

	Private Sub ResetarCamposVenda()
		NomeClienteTextFormField.Text = String.Empty
		ResetarCamposItemVenda()
		ItensVenda = New List(Of ItemVenda)
		BindItensVenda()
	End Sub

	Protected Sub VoltarButton_Click(sender As Object, e As EventArgs)
		PageUtils.RedirecionarParaPagina(
			page:=Me,
			request:=Request,
			urlPagina:="~/"
		)
	End Sub

	Protected Sub NomeClienteTextFormField_ServerValidate(source As Object, args As ServerValidateEventArgs)
		Dim descricao As String = args.Value

		'Validação se campo obrigatório
		If descricao = String.Empty Then
			NomeClienteTextFormField.ErrorMessage = "Campo obrigatório!"
			args.IsValid = False
			Return
		End If

		'Validação de tamanho limite da string
		If descricao.Length > 100 Then
			NomeClienteTextFormField.ErrorMessage = "Tamanho máximo de 100 caracteres excedido!"
			args.IsValid = False
			Return
		End If
	End Sub

	Protected Sub QuantidadeTextFormField_ServerValidate(source As Object, args As ServerValidateEventArgs)
		Dim saldoEstoque As String = args.Value

		'Validação se campo obrigatório
		If saldoEstoque = String.Empty Then
			QuantidadeTextFormField.ErrorMessage = "Campo obrigatório!"
			args.IsValid = False
			Return
		End If

		'Validação de valor numérico
		Dim saldoEstoqueInt As Integer
		If Not Integer.TryParse(saldoEstoque, saldoEstoqueInt) Then
			QuantidadeTextFormField.ErrorMessage = "Valor inválido!"
			args.IsValid = False
			Return
		End If

		'Validação de valor mínimo
		If saldoEstoqueInt < 0 Then
			QuantidadeTextFormField.ErrorMessage = "Valor deve ser maior ou igual a 1!"
			args.IsValid = False
			Return
		End If

		'Validação de valor máximo de dígitos
		If saldoEstoque.Length > 5 Then
			QuantidadeTextFormField.ErrorMessage = "Valor deve ter no máximo 5 dígitos!"
			args.IsValid = False
			Return
		End If
	End Sub

End Class