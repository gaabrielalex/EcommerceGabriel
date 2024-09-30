Imports ModelsEcommerceGabrielX
Imports UtilsEcommerceGabrielX

Public Class ItemVendaDAO
	Implements IDAO(Of ItemVenda)

	Private ReadOnly _bancoDeDados As BancoDeDados

	Public Sub New(bancoDeDados As BancoDeDados)
		_bancoDeDados = bancoDeDados
	End Sub

	Public Function Inserir(itemVenda As ItemVenda) As Integer Implements IDAO(Of ItemVenda).Inserir
		Dim query = "INSERT INTO item_venda (qtde, vlr_total_item, preco_unitario id_pedido, id_produto) 
						VALUES (@qtde, @vlr_total_item, @preco_unitario, @id_pedido, @id_produto)
						SELECT SCOPE_IDENTITY();"
		Dim parametros = New ParametroBDFactory() _
								.Adicionar("@qtde", itemVenda.Quantidade) _
								.Adicionar("@vlr_total_item", itemVenda.VlrTotalItem) _
								.Adicionar("@id_pedido", itemVenda.IdVenda) _
								.Adicionar("@id_produto", itemVenda.Produto.IdProduto) _
								.ObterParametros()

		Try
			Dim idItemVenda = Convert.ToInt32(_bancoDeDados.ExecutarComRetorno(query, parametros))
			Return idItemVenda
		Catch ex As Exception
			Throw New Erro($"Erro ao inserir item da venda: {ex.ToString()}")
		End Try
	End Function

	Public Sub Editar(itemVenda As ItemVenda, id As Integer) Implements IDAO(Of ItemVenda).Editar
		Throw New NotImplementedException()
	End Sub

	Public Function ListarTodos() As List(Of ItemVenda) Implements IDAO(Of ItemVenda).ListarTodos
		Throw New NotImplementedException()
	End Function

	Public Function ObterPorId(id As Integer) As ItemVenda Implements IDAO(Of ItemVenda).ObterPorId
		Throw New NotImplementedException()
	End Function

	Public Function ListarPorVenda(idPedido As Integer) As List(Of ItemVenda)
		Dim query = "SELECT * FROM item_venda WHERE id_pedido = @id_pedido"
		Dim parametros = New ParametroBDFactory() _
							.Adicionar("@id_pedido", idPedido) _
							.ObterParametros()

		Try
			Dim listaItens = ConverterReaderParaListaDeObjetos(_bancoDeDados.ConsultarReader(query, parametros))
			Return listaItens
		Catch ex As Exception
			Throw New Erro($"Erro ao listar itens da venda: {ex.ToString()}")
		End Try
	End Function

	Public Function ConverterReaderParaListaDeObjetos(reader As IEnumerable(Of IDataRecord)) As List(Of ItemVenda) Implements IDAO(Of ItemVenda).ConverterReaderParaListaDeObjetos
		Dim listaItens = New List(Of ItemVenda)

		For Each record In reader
			Dim itemVenda = New ItemVenda(
				idItemVenda:=record.GetInt32((record.GetOrdinal("id_item_venda"))),
				idVenda:=record.GetInt32((record.GetOrdinal("id_pedido"))),
				produto:=New Produto(
					idProduto:=record.GetInt32((record.GetOrdinal("id_produto"))),
					descricao:=record.GetString((record.GetOrdinal("descricao"))),
					precoUnitario:=record.GetDecimal((record.GetOrdinal("preco_unitario"))),
					saldoEstoque:=record.GetInt32((record.GetOrdinal("saldo_estoque")))
				),
				quantidade:=record.GetInt32((record.GetOrdinal("qtde"))),
				precoUnitario:=record.GetDecimal((record.GetOrdinal("preco_unitario")))
			)
		Next

		Return listaItens
	End Function
End Class
