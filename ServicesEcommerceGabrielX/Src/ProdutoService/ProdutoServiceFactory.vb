Imports DAOEcommerceGabrielX

Public Class ProdutoServiceFactory

	Public Shared Function Criar() As ProdutoService
		Dim produtoDAO = ProdutoDAOFactory.Criar()
		Return New ProdutoService(
			produtoDAO:=produtoDAO
		)
	End Function
End Class
