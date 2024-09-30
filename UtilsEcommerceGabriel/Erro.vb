Public Class Erro
	Inherits Exception

	Public Sub New(mensagem As String)
		MyBase.New(mensagem)
		RegistroLog.Log(mensagem)
	End Sub

End Class
