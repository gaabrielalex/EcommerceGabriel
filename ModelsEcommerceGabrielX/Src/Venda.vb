﻿
<Serializable>
Public Class Venda

	Public Property IdVenda As Integer?
	Public Property DataVenda As Date?
	Public Property NomeCliente As String
	Public Property ValorTotal As Decimal
	Public Property ItensVenda As List(Of ItemVenda)

	Public Sub New()
	End Sub

	Public Sub New(nomeCliente As String, valorTotal As Decimal, Optional idVenda As Integer = Nothing, Optional dataVenda As Date = Nothing, Optional itensVenda As List(Of ItemVenda) = Nothing)
		Me.IdVenda = idVenda
		Me.DataVenda = dataVenda
		Me.NomeCliente = nomeCliente
		Me.ValorTotal = valorTotal
		Me.ItensVenda = itensVenda
	End Sub
End Class
