Public Class TituloMedio
	Inherits System.Web.UI.UserControl

	Public Property Text As String
		Get
			Return TituloLabel.Text
		End Get
		Set(value As String)
			TituloLabel.Text = value
		End Set

	End Property


	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

	End Sub

End Class