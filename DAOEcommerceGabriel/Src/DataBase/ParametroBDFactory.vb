Public Class ParametroBDFactory

	Private Property _listaParametro As List(Of ParametroDB)

	Public Sub New()
		_listaParametro = New List(Of ParametroDB)
	End Sub

	Public Function Adicionar(nomeParametro As String, valorParametro As Object) As ParametroBDFactory
		_listaParametro.Add(New ParametroDB(nomeParametro, valorParametro))
		Return Me
	End Function

	Public Function ObterParametros() As List(Of ParametroDB)
		Return New List(Of ParametroDB)(_listaParametro)
	End Function
End Class
