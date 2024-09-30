Imports DAOEcommerceGabrielX

Public Class VendaServiceFactory

	Public Shared Function Criar() As VendaService
		Dim vendaDAO As VendaDAO = VendaDAOFactory.Criar()
		Dim itemVendaDAO As ItemVendaDAO = ItemVendaDAOFactory.Criar()

		Return New VendaService(
			vendaDAO:=vendaDAO,
			itemVendaDAO:=itemVendaDAO
		)
	End Function
End Class
