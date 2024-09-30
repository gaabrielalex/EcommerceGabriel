Imports DAOEcommerceGabrielX
Imports ModelsEcommerceGabrielX
Imports UtilsEcommerceGabrielX

Public Class ProdutoService

	Private ReadOnly _produtoDAO As ProdutoDAO

	Public Sub New(produtoDAO As ProdutoDAO)
		_produtoDAO = produtoDAO
	End Sub

	Public Function Inserir(produto As Produto) As Integer
		Return _produtoDAO.Inserir(produto)
	End Function

	Public Sub Editar(produto As Produto, id As Integer)
		_produtoDAO.Editar(produto, id)
	End Sub

	Public Function ListarTodos() As List(Of Produto)
		Return _produtoDAO.ListarTodos()
	End Function

	Public Function ObterPorId(id As Integer) As Produto
		Return _produtoDAO.ObterPorId(id)
	End Function

	Public Function DescricaoJaExiste(descricao As String) As Boolean
		Return _produtoDAO.DescricaoJaExiste(descricao)
	End Function

	Public Function ObterSaldoEstoque(idProduto As Integer) As Integer
		Dim produto = _produtoDAO.ObterPorId(idProduto)
		If produto Is Nothing Then
			Throw New Erro("Produto não encontrado")
		End If

		Return produto.SaldoEstoque
	End Function
End Class
