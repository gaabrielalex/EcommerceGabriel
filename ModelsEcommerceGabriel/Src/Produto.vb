
<Serializable>
Public Class Produto

	Public Property IdProduto As Integer?
	Public Property Descricao As String
	Public Property PrecoUnitario As Decimal
	Public Property SaldoEstoque As Integer

	Public Sub New(idProduto As Integer?, descricao As String, precoUnitario As Decimal, saldoEstoque As Integer)
		Me.IdProduto = idProduto
		Me.Descricao = descricao
		Me.PrecoUnitario = precoUnitario
		Me.SaldoEstoque = saldoEstoque
	End Sub
End Class
