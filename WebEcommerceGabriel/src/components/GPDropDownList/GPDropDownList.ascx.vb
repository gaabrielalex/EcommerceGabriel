Public Class GPDropDownList
	Inherits System.Web.UI.UserControl

    Public Property Style As String

    Private _cssClass As String
    Public Property CssClass As String
        Get
            Return _cssClass
        End Get
        Set(ByVal value As String)
            _cssClass += value.Insert(0, " ")
        End Set
    End Property

    Public ReadOnly Property LabelControl As Label
        Get
            Return Me.Label
        End Get
    End Property

    Public Property DropDownListControl As DropDownList
        Get
            Return Me.DropDownList
        End Get
        Set(ByVal value As DropDownList)
            Me.DropDownList = value
        End Set
    End Property

    Public Property CustomValidatorControl As CustomValidator
        Get
            Return Me.CustomValidator
        End Get
        Set(ByVal value As CustomValidator)
            Me.CustomValidator = value
        End Set
    End Property

    Public Property LabelText As String
        Get
            Return Me.Label.Text
        End Get
        Set(ByVal value As String)
            Me.Label.Text = value
        End Set
    End Property

    Public Property ValidationGroup As String
        Get
            Return Me.CustomValidator.ValidationGroup
        End Get
        Set(ByVal value As String)
            Me.CustomValidator.ValidationGroup = value
        End Set
    End Property

    Public Property ErrorMessage As String
        Get
            Return Me.CustomValidator.ErrorMessage
        End Get
        Set(ByVal value As String)
            Me.CustomValidator.ErrorMessage = value
        End Set
    End Property

    Public Property DataSource As Object
        Get
            Return Me.DropDownList.DataSource
        End Get
        Set(ByVal value As Object)
            Me.DropDownList.DataSource = value
        End Set
    End Property

    Public Property DataTextField As String
        Get
            Return Me.DropDownList.DataTextField
        End Get
        Set(ByVal value As String)
            Me.DropDownList.DataTextField = value
        End Set
    End Property

    Public Property DataValueField As String
        Get
            Return Me.DropDownList.DataValueField
        End Get
        Set(ByVal value As String)
            Me.DropDownList.DataValueField = value
        End Set
    End Property

    Public Property SelectedValue As String
        Get
            Return Me.DropDownList.SelectedValue
        End Get
        Set(ByVal value As String)
            Me.DropDownList.SelectedValue = value
        End Set
    End Property

    Public Sub ClearSelection()
        Me.DropDownList.ClearSelection()
    End Sub

    Public Overrides Sub DataBind()
        Me.DropDownList.DataBind()
    End Sub

    Public Event ServerValidate As ServerValidateEventHandler

    Protected Sub CustomValidator_ServerValidate(ByVal source As Object, ByVal args As ServerValidateEventArgs)
        RaiseEvent ServerValidate(source, args)
    End Sub

    Public Event SelectedIndexChanged As EventHandler
    Protected Sub DropDownList_TextChanged(sender As Object, e As EventArgs)
        RaiseEvent SelectedIndexChanged(sender, e)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)

    End Sub

End Class