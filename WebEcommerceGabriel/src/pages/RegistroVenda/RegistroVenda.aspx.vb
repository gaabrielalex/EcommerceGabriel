Imports ModelsEcommerceGabrielX
Imports ServicesEcommerceGabrielX
Imports UtilsEcommerceGabrielX

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
		End Set
	End Property

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
		Dim idProduto As Integer = ProdutosDropDownList.SelectedValue
		Dim produto As Produto = _produtoService.ObterPorId(idProduto)
		Dim quantidade As Integer = Integer.Parse(QuantidadeTextFormField.Text)
		Dim precoUnitario As Decimal = Decimal.Parse(PrecoUnitTextFormField.Text)

		Dim itemVenda As ItemVenda = New ItemVenda(
			idVenda:=1,
			produto:=produto,
			quantidade:=quantidade,
			precoUnitario:=precoUnitario
		)

		Dim ItensVenda As List(Of ItemVenda) = Me.ItensVenda
		ItensVenda.Add(itemVenda)
		Me.ItensVenda = ItensVenda

		BindItensVenda()
		QuantidadeTextFormField.Text = ""
	End Sub

	Private Sub BindItensVenda()
		ItensVendaGW.DataSource = ItensVenda
		ItensVendaGW.DataBind()
	End Sub

	Protected Sub GerarVendaButton_Click(sender As Object, e As EventArgs)

	End Sub

	Protected Sub OKButton_Click(sender As Object, e As EventArgs)

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