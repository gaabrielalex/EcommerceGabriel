Imports ModelsEcommerceGabriel
Imports UtilsEcommerceGabriel

Public Class ProdutoDAO
	Implements IDAO(Of Produto)

	Private ReadOnly _bancoDeDados As BancoDeDados

	Public Sub New(bancoDeDados As BancoDeDados)
		_bancoDeDados = bancoDeDados
	End Sub

	Public Function Inserir(produto As Produto) As Integer Implements IDAO(Of Produto).Inserir
		Dim query = "INSERT INTO produto (descricao, vlr_unitario) VALUES (@descricao, @vlr_unitario);
							SELECT SCOPE_IDENTITY();"
		Dim parametros = New ParametroBDFactory() _
							.Adicionar("@descricao", produto.Descricao) _
							.Adicionar("@preco_unitario", produto.PrecoUnitario) _
							.Adicionar("@saldo_estoque", produto.SaldoEstoque) _
							.ObterParametros()

		Try
			Dim idProduto = Convert.ToInt32(_bancoDeDados.ExecutarComRetorno(query, parametros))
			Return idProduto
		Catch ex As Exception
			Throw New Erro($"Erro ao inserir produto: {ex.ToString()}")
		End Try
	End Function

	Public Sub Editar(produto As Produto, id As Integer) Implements IDAO(Of Produto).Editar
		Dim query = "UPDATE produto SET descricao = @descricao, vlr_unitario = @vlr_unitario WHERE id_produto = @id_produto"
		Dim parametros = New ParametroBDFactory() _
							.Adicionar("@descricao", produto.Descricao) _
							.Adicionar("@preco_unitario", produto.PrecoUnitario) _
							.Adicionar("@saldo_estoque", produto.SaldoEstoque) _
							.Adicionar("@id_produto", id) _
							.ObterParametros()

		Try
			Dim linhasAfetadas = _bancoDeDados.Executar(query, parametros)
			If linhasAfetadas <= 0 Then
				Throw New Erro($"Erro ao editar produto: Nenhuma linha foi afetada - Id: " + id)
			End If
		Catch ex As Exception
			Throw New Erro($"Erro ao editar produto: {ex.ToString()}")
		End Try
	End Sub

	Public Function ListarTodos() As List(Of Produto) Implements IDAO(Of Produto).ListarTodos
		Dim query = "SELECT * FROM produto ORDER BY descricao"

		Try
			Dim listaProdutos = ConverterReaderParaListaDeObjetos(_bancoDeDados.ConsultarReader(query))
			Return listaProdutos
		Catch ex As Exception
			Throw New Erro($"Erro ao listar produtos: {ex.ToString()}")
		End Try
	End Function

	Public Function ObterPorId(id As Integer) As Produto Implements IDAO(Of Produto).ObterPorId
		Dim query = "SELECT * FROM produto where id_produto = @id_produto"
		Dim parametros = New ParametroBDFactory() _
								.Adicionar("@id_produto", id) _
								.ObterParametros()
		Try
			Dim listaProdutos = ConverterReaderParaListaDeObjetos(_bancoDeDados.ConsultarReader(query, parametros))
			If listaProdutos.Count = 0 Then
				Return Nothing
			End If
			Return listaProdutos(0)
		Catch ex As Exception
			Throw New Erro($"Erro ao obter produto: {ex.ToString()}")
		End Try


	End Function

	Public Function DescricaoJaExiste(descricao As String) As Boolean
		Dim query = "SELECT * FROM produto WHERE descricao COLLATE Latin1_General_CI_AI = @descricao"
		Dim parametros = New ParametroBDFactory() _
								.Adicionar("@descricao", descricao) _
								.ObterParametros()

		Try
			Dim reader = _bancoDeDados.ConsultarReader(query, parametros)
			If reader.Count > 0 Then
				Return True
			End If
			Return False
		Catch ex As Exception
			Throw New Erro($"Erro ao realizar verificação da já existência do produto: {ex.ToString()}")
		End Try
	End Function

	Public Function ConverterReaderParaListaDeObjetos(reader As IEnumerable(Of IDataRecord)) As List(Of Produto) Implements IDAO(Of Produto).ConverterReaderParaListaDeObjetos
		Dim listaProdutos = New List(Of Produto)

		For Each record In reader
			Dim produto = New Produto(
				idProduto:=record.GetInt32(record.GetOrdinal("id_produto")),
				descricao:=record.GetString(record.GetOrdinal("descricao")),
				precoUnitario:=record.GetDecimal(record.GetOrdinal("preco_unitario")),
				saldoEstoque:=record.GetInt32(record.GetOrdinal("saldo_estoque"))
			)
			listaProdutos.Add(produto)
		Next

		Return listaProdutos
	End Function

End Class
