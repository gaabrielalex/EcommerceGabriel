
Imports System.Data.SqlClient

Public Class BancoDeDados

	Public Shared Function CriarConexao() As SqlConnection
		Return New SqlConnection("Data Source=GABRIELALEX\SQLEXPRESS;Initial Catalog=BD_Projeto_Marlon;Integrated Security=True;Encrypt=False;TrustServerCertificate=True")
	End Function

	Public Function ConsultarReader(sql As String) As IEnumerable(Of IDataRecord)
		Return ConsultarReader(sql, Nothing)
	End Function

	Public Iterator Function ConsultarReader(sql As String, parametros As List(Of ParametroDB)) As IEnumerable(Of IDataRecord)
		Dim conexao = CriarConexao()
		Dim comando = conexao.CreateCommand()
		AdicionarParametrosAQuey(comando, parametros)
		comando.CommandText = sql
		conexao.Open()

		Using reader = comando.ExecuteReader(CommandBehavior.CloseConnection)
			While reader.Read()
				Yield reader
			End While
		End Using
	End Function

	Public Function Executar(sql As String, parametros As List(Of ParametroDB), Optional ehProcedure As Boolean = False) As Integer
		Using conexao = CriarConexao()
			Using comando = conexao.CreateCommand()
				AdicionarParametrosAQuey(comando, parametros)

				If ehProcedure Then
					comando.CommandType = CommandType.StoredProcedure
				End If

				comando.CommandText = sql
				conexao.Open()
				Try
					Return comando.ExecuteNonQuery()
				Catch ex As Exception
					Throw
				Finally
					conexao.Close()
				End Try
			End Using
		End Using
	End Function

	Public Function ExecutarComRetorno(sql As String, parametros As List(Of ParametroDB)) As Object
		Using conexao = CriarConexao()
			Using comando = conexao.CreateCommand()
				AdicionarParametrosAQuey(comando, parametros)
				comando.CommandText = sql
				conexao.Open()

				Try
					Return comando.ExecuteScalar()
				Catch ex As Exception
					Throw
				Finally
					conexao.Close()
				End Try
			End Using
		End Using
	End Function

	Public Function ExecutarComTransacao(sql As String, parametros As List(Of ParametroDB), Optional ehProcedure As Boolean = False) As Integer
		Using conexao = CriarConexao()
			Using comando = conexao.CreateCommand()
				AdicionarParametrosAQuey(comando, parametros)

				If ehProcedure Then
					comando.CommandType = CommandType.StoredProcedure
				End If

				comando.CommandText = sql
				conexao.Open()
				Dim transacao = conexao.BeginTransaction()
				comando.Transaction = transacao

				Try
					Dim linhasAfetadas = comando.ExecuteNonQuery()
					transacao.Commit()
					Return linhasAfetadas
				Catch ex As Exception
					transacao.Rollback()
					Throw
				Finally
					conexao.Close()
				End Try
			End Using
		End Using
	End Function

	Private Sub AdicionarParametrosAQuey(sqlCommand As SqlCommand, parametros As List(Of ParametroDB))
		If parametros Is Nothing Then
			Return
		End If

		For n = 0 To parametros.Count - 1
			Dim filtro = sqlCommand.CreateParameter()
			filtro.ParameterName = parametros(n).Nome
			filtro.Value = parametros(n).Valor
			sqlCommand.Parameters.Add(filtro)
		Next
	End Sub

End Class
