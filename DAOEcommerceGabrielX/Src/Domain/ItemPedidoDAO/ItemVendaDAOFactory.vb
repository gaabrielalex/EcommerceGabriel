Public Class ItemVendaDAOFactory

	Public Shared Function Criar() As ItemVendaDAO
		Return New ItemVendaDAO(
			bancoDeDados:=New BancoDeDados()
		)
	End Function
End Class
