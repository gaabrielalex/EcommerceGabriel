Imports ModelsEcommerceGabrielX
Imports ServicesEcommerceGabrielX
Imports UtilsEcommerceGabrielX
Imports WebEcommerceGabriel.MensagemInfo

Public Class CadastroProduto
	Inherits System.Web.UI.Page

	Private ReadOnly _produtoService As ProdutoService = ProdutoServiceFactory.Criar()
	Private ReadOnly _errorHandler As ErrorHandler = New ErrorHandler()

	Public Property ModoAtual As ModosFormulario
		Get
			If (ViewState("ModoAtual") IsNot Nothing) Then
				Return CType(ViewState("ModoAtual"), ModosFormulario)
			End If
			Return ModosFormulario.Cadastrar
		End Get
		Set(value As ModosFormulario)
			ViewState("ModoAtual") = value
		End Set
	End Property

	Public Property ProdutoASerEditado As Produto
		Get
			If (ViewState("ProdutoASerEditado") IsNot Nothing) Then
				Return CType(ViewState("ProdutoASerEditado"), Produto)
			End If
			Return Nothing
		End Get
		Set(value As Produto)
			ViewState("ProdutoASerEditado") = value
		End Set
	End Property

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If Not Page.IsPostBack Then
			ConfigurarFormParaCadastro()
		End If
	End Sub

	Public Sub ConfigurarForm(modo As ModosFormulario, idProdutoParaEdicao As Integer?)
		ModoAtual = modo
		If modo = ModosFormulario.Cadastrar Then
			ConfigurarFormParaCadastro()

		ElseIf (modo = ModosFormulario.Editar And modo = ModosFormulario.Editar) Then
			ConfigurarFormParaEdicao(idProdutoParaEdicao.Value)
		End If
	End Sub

	Private Sub ConfigurarFormParaCadastro()
		FormAddEditProdutoTituloMedio.Text = "Cadastrar Produto"
		ProdutoASerEditado = Nothing
		ModoAtual = ModosFormulario.Cadastrar
		LimparCampos()
	End Sub

	Private Sub ConfigurarFormParaEdicao(idProdutoParaEdicao As Integer)
		Try
			ModoAtual = ModosFormulario.Editar
			ProdutoASerEditado = _produtoService.ObterPorId(idProdutoParaEdicao)
			If ProdutoASerEditado Is Nothing Then
				TratarProdutoNaoEncontrado()
				Return
			End If
			FormAddEditProdutoTituloMedio.Text = "Editar Produto"
			DescricaoTextFormField.Text = ProdutoASerEditado.Descricao
			PrecoUnitTextFormField.Text = ProdutoASerEditado.PrecoUnitario.ToString()
			SaldoEstoqueTextFormField.Text = ProdutoASerEditado.SaldoEstoque.ToString()
		Catch ex As Exception
			PageUtils.MostrarMensagemViaToast(_errorHandler.HandleErrorMessage("Erro ao obter produto para edição", ex), MensagemInfo.TiposMensagem.Erro, Me)
		End Try
	End Sub

	Private Sub LimparCampos()
		CodTextFormField.Text = String.Empty
		DescricaoTextFormField.Text = String.Empty
		PrecoUnitTextFormField.Text = String.Empty
		SaldoEstoqueTextFormField.Text = String.Empty
	End Sub

	Protected Sub DescricaoTextFormField_ServerValidate(source As Object, args As ServerValidateEventArgs)
		Dim descricao As String = args.Value

		'Validação se campo obrigatório
		If descricao = String.Empty Then
			DescricaoTextFormField.ErrorMessage = "Campo obrigatório!"
			args.IsValid = False
			Return
		End If

		'Validação de tamanho limite da string
		If descricao.Length > 100 Then
			DescricaoTextFormField.ErrorMessage = "Tamanho máximo de 100 caracteres excedido!"
			args.IsValid = False
			Return
		End If

		'Validação da já existência do produto
		Dim produtoJaExiste As Boolean = False
		Try
			If (ProdutoASerEditado IsNot Nothing AndAlso Not ProdutoASerEditado.Descricao.Equals(descricao) AndAlso ModoAtual = ModosFormulario.Editar) OrElse ModoAtual = ModosFormulario.Cadastrar Then

				produtoJaExiste = _produtoService.DescricaoJaExiste(descricao)
				If produtoJaExiste Then
					DescricaoTextFormField.ErrorMessage = "Produto já existente!"
					args.IsValid = False
					Return
				End If
			End If
		Catch ex As Exception
			PageUtils.MostrarMensagemViaToast(_errorHandler.HandleErrorMessage("Houve um erro ao verificar se produto já existe", ex), MensagemInfo.TiposMensagem.Erro, Me)
		End Try
	End Sub

	Protected Sub PrecoUnitTextFormField_ServerValidate(source As Object, args As ServerValidateEventArgs)
		Dim vlrUnitario As String = args.Value

		'Validação se campo obrigatório
		If vlrUnitario = String.Empty Then
			PrecoUnitTextFormField.ErrorMessage = "Campo obrigatório!"
			args.IsValid = False
			Return
		End If

		'Validação de valor numérico
		Dim vlrUnitarioDecimal As Decimal
		If Not Decimal.TryParse(vlrUnitario, vlrUnitarioDecimal) Then
			PrecoUnitTextFormField.ErrorMessage = "Valor inválido!"
			args.IsValid = False
			Return
		End If

		'Validação de valor máximo de dígitos
		Dim digitosString As String = vlrUnitarioDecimal.ToString()
		If Not digitosString.Contains(","c) Then
			digitosString &= ",0"
		End If
		Dim digitos As String() = digitosString.Split(","c)
		If digitos(0).Length > 5 OrElse digitos(1).Length > 2 Then
			PrecoUnitTextFormField.ErrorMessage = "Valor deve ter no máximo 7 dígitos, sendo 5 inteiros e 2 decimais!"
			args.IsValid = False
			Return
		End If
	End Sub

	Protected Sub SaldoEstoqueTextFormField_ServerValidate(source As Object, args As ServerValidateEventArgs)
		Dim saldoEstoque As String = args.Value

		'Validação se campo obrigatório
		If saldoEstoque = String.Empty Then
			SaldoEstoqueTextFormField.ErrorMessage = "Campo obrigatório!"
			args.IsValid = False
			Return
		End If

		'Validação de valor numérico
		Dim saldoEstoqueInt As Integer
		If Not Integer.TryParse(saldoEstoque, saldoEstoqueInt) Then
			SaldoEstoqueTextFormField.ErrorMessage = "Valor inválido!"
			args.IsValid = False
			Return
		End If

		'Validação de valor mínimo
		If saldoEstoqueInt < 0 Then
			SaldoEstoqueTextFormField.ErrorMessage = "Valor deve ser maior ou igual a 0!"
			args.IsValid = False
			Return
		End If

		'Validação de valor máximo de dígitos
		If saldoEstoque.Length > 5 Then
			SaldoEstoqueTextFormField.ErrorMessage = "Valor deve ter no máximo 5 dígitos!"
			args.IsValid = False
			Return
		End If

	End Sub

	Protected Sub CancelarButton_Click(sender As Object, e As EventArgs)
		ConfigurarFormParaCadastro()
	End Sub

	Protected Sub SalvarButton_Click(sender As Object, e As EventArgs)
		If (Not Page.IsValid) Then
			Return
		End If

		Dim produto = ObterDadosDoFormulario()
		If produto Is Nothing Then
			Return
		End If

		If ModoAtual = ModosFormulario.Cadastrar Then
			CadastrarProduto(produto)
		ElseIf ModoAtual = ModosFormulario.Editar Then
			EditarProduto(produto)
		End If
	End Sub

	Private Function ObterDadosDoFormulario() As Produto
		Dim produto As New Produto()
		produto.Descricao = DescricaoTextFormField.Text
		If Not Decimal.TryParse(PrecoUnitTextFormField.Text, produto.PrecoUnitario) Then
			PageUtils.MostrarMensagemViaToast("Favor informar valores numéricos no campo ""Valor Unitário""", TiposMensagem.Erro, Page)
			Return Nothing
		End If
		If Not Integer.TryParse(SaldoEstoqueTextFormField.Text, produto.SaldoEstoque) Then
			PageUtils.MostrarMensagemViaToast("Favor informar valores numéricos no campo ""Saldo em Estoque""", TiposMensagem.Erro, Page)
			Return Nothing
		End If
		Return produto
	End Function

	Private Sub CadastrarProduto(produto As Produto)
		Try
			_produtoService.Inserir(produto)
			TratarSucessoDaOperacao()
		Catch ex As Exception
			PageUtils.MostrarMensagemViaToast(_errorHandler.HandleErrorMessage("Houve um erro ao cadastrar produto", ex), MensagemInfo.TiposMensagem.Erro, Me)
		End Try
	End Sub

	Private Sub EditarProduto(produto As Produto)
		Try
			_produtoService.Editar(produto, ProdutoASerEditado.IdProduto.Value)
			TratarSucessoDaOperacao()
		Catch ex As Exception
			PageUtils.MostrarMensagemViaToast(_errorHandler.HandleErrorMessage("Houve um erro ao editar produto", ex), MensagemInfo.TiposMensagem.Erro, Me)
		End Try
	End Sub

	Private Sub TratarProdutoNaoEncontrado()
		PageUtils.MostrarMensagemViaToast("Produto não cadastrado!", TiposMensagem.Erro, Me)
		ConfigurarFormParaCadastro()
	End Sub

	Protected Sub BuscarButton_Click(sender As Object, e As EventArgs)
		Dim idProduto As Integer
		If Not Integer.TryParse(CodTextFormField.Text, idProduto) Then
			PageUtils.MostrarMensagemViaToast("Favor informar um valor numérico no campo ""Código""", TiposMensagem.Erro, Me)
			Return
		End If

		ConfigurarForm(ModosFormulario.Editar, idProduto)
	End Sub

	Protected Sub VoltarButton_Click(sender As Object, e As EventArgs)
		PageUtils.RedirecionarParaPagina(
			page:=Me,
			request:=Request,
			urlPagina:="~/"
		)
	End Sub

	Private Sub TratarSucessoDaOperacao()
		PageUtils.RedirecionarParaPagina(
			page:=Page,
			request:=Request,
			urlPagina:="~/",
			mensagemAposRedirecionamento:="Operação realizada com sucesso!",
			tipoMensagem:=TiposMensagem.Sucesso
		)
	End Sub
End Class