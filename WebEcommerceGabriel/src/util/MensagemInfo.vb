Public Class MensagemInfo

	Public Enum TiposMensagem
		Sucesso = 115
		Erro = 101
		Informacao = 105
	End Enum
	Public Property Mensagem As String
	Public Property Tipo As TiposMensagem
End Class
