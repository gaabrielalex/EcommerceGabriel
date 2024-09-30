Imports ModelsEcommerceGabrielX
Imports UtilsEcommerceGabrielX

Public Class VendaDAO
	Implements IDAO(Of Venda)

	Private ReadOnly _bancoDeDados As BancoDeDados

	Public Sub New(bancoDeDados As BancoDeDados)
		_bancoDeDados = bancoDeDados
	End Sub

	Public Function Inserir(venda As Venda) As Integer Implements IDAO(Of Venda).Inserir
		Dim query = "INSERT INTO venda (data_venda, nome_cliente, vlr_total_venda) 
					VALUES (@data_venda, @nome_cliente, @vlr_total_venda);
					SELECT SCOPE_IDENTITY();"
		Dim parametros = New ParametroBDFactory() _
								.Adicionar("@data_venda", venda.DataVenda) _
								.Adicionar("@nome_cliente", venda.NomeCliente) _
								.Adicionar("@vlr_total_venda", venda.ValorTotal) _
								.ObterParametros()

		Try
			Dim idVenda = Convert.ToInt32(_bancoDeDados.ExecutarComRetorno(query, parametros))
			Return idVenda
		Catch ex As Exception
			Throw New Erro($"Erro ao inserir pedido: {ex.ToString()}")
		End Try
	End Function

	Public Sub Editar(venda As Venda, id As Integer) Implements IDAO(Of Venda).Editar
		Throw New NotImplementedException()
	End Sub

	Public Function ListarTodos() As List(Of Venda) Implements IDAO(Of Venda).ListarTodos
		Throw New NotImplementedException()
	End Function

	Public Function ObterPorId(id As Integer) As Venda Implements IDAO(Of Venda).ObterPorId
		Dim query = "SELECT p.*, 
					COALESCE(SUM(ip.vlr_total_item), 0) AS vlr_total
					FROM venda p
						LEFT JOIN item_venda ip ON p.id_venda = ip.id_venda
					WHERE p.id_venda = @id_venda 
					GROUP BY p.id_venda, p.data_venda, p.nome_cliente, vlr_total_venda"

		Dim parametros = New ParametroBDFactory() _
							.Adicionar("@id_venda", id) _
							.ObterParametros()

		Try
			Dim listaVendas = ConverterReaderParaListaDeObjetos(_bancoDeDados.ConsultarReader(query, parametros))

			If listaVendas.Count = 0 Then
				Return Nothing
			End If
			Return listaVendas(0)
		Catch ex As Exception
			Throw New Erro($"Erro ao obter venda: {ex.ToString()}")
		End Try
	End Function

	Public Function ConverterReaderParaListaDeObjetos(reader As IEnumerable(Of IDataRecord)) As List(Of Venda) Implements IDAO(Of Venda).ConverterReaderParaListaDeObjetos
		Dim listaVendas = New List(Of Venda)

		For Each record In reader
			Dim venda = New Venda(
				idVenda:=record.GetInt32(record.GetOrdinal("id_pedido")),
				dataVenda:=record.GetDateTime(record.GetOrdinal("data_venda")),
				nomeCliente:=record.GetString(record.GetOrdinal("nome_cliente")),
				valorTotal:=record.GetDecimal(record.GetOrdinal("vlr_total"))
			)

			listaVendas.Add(venda)
		Next

		Return listaVendas
	End Function
End Class
