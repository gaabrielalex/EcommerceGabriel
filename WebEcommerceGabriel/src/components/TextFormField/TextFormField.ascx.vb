Public Class TextFormField
	Inherits System.Web.UI.UserControl

	Public Property Style As String

	Public Property IdDato As String
		Get
			Return InputHiddenIdDado.Value
		End Get
		Set(value As String)
			InputHiddenIdDado.Value = value
		End Set

	End Property

	Public ReadOnly Property InputHiddenIdDadox As HtmlInputHidden
		Get
			Return InputHiddenIdDado
		End Get
	End Property

	Private _CssClass As String

	Public Property CssClass As String
		Get
			Return _CssClass
		End Get
		Set(value As String)
			_CssClass += value.Insert(0, " ")
		End Set

	End Property

	Private _Format As String

	Public Property Format As String
		Get
			Return _Format
		End Get
		Set(value As String)
			If (value = "dinheiro") Then
				TextBoxControl.CssClass += "dinheiro".Insert(0, " ")
				_Format = value
			End If
		End Set

	End Property

	Public ReadOnly Property Label As Label
		Get
			Return Me.LabelControl
		End Get
	End Property

	Public Property TextBox As TextBox
		Get
			Return Me.TextBoxControl
		End Get
		Set(value As TextBox)
			Me.TextBoxControl = value
		End Set

	End Property

	Public Property CustomValidatorControl As CustomValidator
		Get
			Return Me.CustomValidator
		End Get
		Set(value As CustomValidator)
			Me.CustomValidator = value
		End Set

	End Property

	Public Property Text As String
		Get
			Return TextBoxControl.Text
		End Get
		Set(value As String)
			TextBoxControl.Text = value
		End Set

	End Property

	Public Property LabelText As String
		Get
			Return LabelControl.Text
		End Get
		Set(value As String)
			LabelControl.Text = value
		End Set

	End Property

	Public Property ValidationGroup As String
		Get
			Return CustomValidator.ValidationGroup
		End Get
		Set(value As String)
			CustomValidator.ValidationGroup = value
		End Set

	End Property

	Public Property ErrorMessage As String
		Get
			Return CustomValidator.ErrorMessage
		End Get
		Set(value As String)
			CustomValidator.ErrorMessage = value
		End Set

	End Property

	Public Property Enabled As Boolean
		Get
			Return TextBoxControl.Enabled
		End Get
		Set(value As Boolean)
			TextBoxControl.Enabled = value
		End Set

	End Property

	Public Property EhMultiLinha As Boolean
		Get
			Return TextBoxControl.TextMode = TextBoxMode.MultiLine
		End Get
		Set(value As Boolean)
			If value Then
				TextBoxControl.TextMode = TextBoxMode.MultiLine
			End If
		End Set

	End Property

	Public Property TextMode As TextBoxMode
		Get
			Return TextBoxControl.TextMode
		End Get
		Set(value As TextBoxMode)
			TextBoxControl.TextMode = value
		End Set

	End Property

	Public Event ServerValidate As ServerValidateEventHandler

	Protected Sub CustomValidator_ServerValidate(ByVal source As Object, ByVal args As ServerValidateEventArgs)
		RaiseEvent ServerValidate(source, args)
	End Sub


	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

	End Sub

End Class