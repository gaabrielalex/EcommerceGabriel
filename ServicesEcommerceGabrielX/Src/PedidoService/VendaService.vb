Imports DAOEcommerceGabrielX
Imports ModelsEcommerceGabrielX

Public Class VendaService

	Private ReadOnly _vendaDAO As VendaDAO
	Private ReadOnly _itemVendaDAO As ItemVendaDAO

	Public Sub New(vendaDAO As VendaDAO, itemVendaDAO As ItemVendaDAO)
		_vendaDAO = vendaDAO
		_itemVendaDAO = itemVendaDAO
	End Sub

	Public Function Inserir(venda As Venda) As Integer
		Dim idVenda = _vendaDAO.Inserir(venda)

		For Each itemVenda In venda.ItensVenda
			itemVenda.IdVenda = idVenda
			_itemVendaDAO.Inserir(itemVenda)
		Next

		Return idVenda
	End Function

	Public Function ObterPorId(id As Integer) As Venda
		Dim venda = _vendaDAO.ObterPorId(id)

		If venda IsNot Nothing Then
			venda.ItensVenda = _itemVendaDAO.ListarPorVenda(id)
		End If

		Return venda
	End Function

	Public Function ListarTodos() As List(Of Venda)
		Dim vendas = _vendaDAO.ListarTodos()

		For Each venda In vendas
			venda.ItensVenda = _itemVendaDAO.ListarPorVenda(venda.IdVenda.Value)
		Next

		Return vendas
	End Function

End Class
