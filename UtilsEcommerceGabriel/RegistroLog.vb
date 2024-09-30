Imports System.IO

Public Class RegistroLog

	Private Shared caminhoExe = String.Empty

	Public Shared Function Log(strMensagem As String, Optional strNomeArquivo As String = "ArquivoLog")
		Try
			Dim caminhoExe = "C:\\Users\\GabrielSilva\\Desktop\\Projeto-Glayson\\VSProjetoGerenciamentoPedidos"
			Dim caminhoArquivo = Path.Combine(caminhoExe, strNomeArquivo)

			If (Not File.Exists(caminhoArquivo)) Then
				Dim arquivo = File.Create(caminhoArquivo)
				arquivo.Close()
			End If

			Using w As StreamWriter = File.AppendText(caminhoArquivo)
				AppendLog(strMensagem, w)
			End Using

			Return True

		Catch ex As Exception
			Return False
		End Try
	End Function

	Private Shared Sub AppendLog(logMensagem As String, txtWriter As TextWriter)
		Try
			txtWriter.Write("\r\nLog Entrada : ")
			txtWriter.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}")
			txtWriter.WriteLine("  :")
			txtWriter.WriteLine($"  :{logMensagem}")
			txtWriter.WriteLine("------------------------------------")
		Catch ex As Exception
			Throw
		End Try
	End Sub

End Class
