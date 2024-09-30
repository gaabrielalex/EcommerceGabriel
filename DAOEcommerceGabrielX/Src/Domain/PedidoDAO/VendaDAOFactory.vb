Public Class VendaDAOFactory

	Public Shared Function Criar() As VendaDAO
		Return New VendaDAO(
			bancoDeDados:=New BancoDeDados()
		)
	End Function

End Class
