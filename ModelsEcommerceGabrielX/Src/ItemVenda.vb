
<Serializable>
Public Class ItemVenda

	Public Property IdItemVenda As Integer?
	Public Property IdVenda As Integer
	Public Property Produto As Produto
	Public Property Quantidade As Integer
	Public Property PrecoUnitario As Decimal
	Public ReadOnly Property VlrTotalItem As Decimal
		Get
			Return PrecoUnitario * Quantidade
		End Get
	End Property

	Public Sub New()
	End Sub

	Public Sub New(idVenda As Integer, produto As Produto, quantidade As Integer, precoUnitario As Decimal, Optional idItemVenda As Integer? = Nothing)
		Me.IdItemVenda = idItemVenda
		Me.IdVenda = idVenda
		Me.Produto = produto
		Me.Quantidade = quantidade
		Me.PrecoUnitario = precoUnitario
	End Sub
End Class
