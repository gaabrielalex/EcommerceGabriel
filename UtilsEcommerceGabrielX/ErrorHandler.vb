Public Class ErrorHandler

    Public Function HandleErrorMessage(message As String) As String
        RegistroLog.Log(message)
        Return message
    End Function

    Public Function HandleErrorMessage(message As String, ex As Exception) As String
        RegistroLog.Log(ex.Message)
        Return message
    End Function
End Class
