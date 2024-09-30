Public Class ProdutoDAOFactory

	Public Shared Function Criar() As ProdutoDAO
		Return New ProdutoDAO(
			bancoDeDados:=New BancoDeDados()
		)
	End Function
End Class
