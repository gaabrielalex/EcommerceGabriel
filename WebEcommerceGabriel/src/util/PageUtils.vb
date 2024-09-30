Imports System.Windows.Forms
Imports WebEcommerceGabriel.MensagemInfo
Public Class PageUtils

	Public Shared Sub MostrarMensagemViaToast(mensagem As String, tipo As TiposMensagem, page As Page)
		Dim script = $"myApp.showToast('{mensagem}', '{ChrW(tipo)}');"
		ScriptManager.RegisterStartupScript(page, page.GetType(), "showToast", script, True)
	End Sub
	Public Shared Sub MostrarMensagemViaToastComDelay(mensagem As String, tipo As TiposMensagem, page As Page)
		Dim script = $"
                setTimeout(() => {{
                    myApp.showToast('{mensagem}', '{ChrW(tipo)}');
                }}, 500);
            "
		ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "showToast", script, True)
	End Sub
	Public Shared Sub MostrarMensagemViaAPISistemaOperacionalLocal(mensagem As String, tipo As String, page As Page)
		Dim titulo As String
		If tipo <> "S" AndAlso tipo <> "E" Then
			Throw New ArgumentException("Tipo inválido. Deve ser 'S' ou 'E'.")
		End If

		If tipo = "S" Then
			tipo = "success"
			titulo = "Sucesso"
		Else
			tipo = "error"
			titulo = "Erro"
		End If

		Dim buttons As MessageBoxButtons = MessageBoxButtons.OK
		MessageBox.Show(mensagem, titulo, buttons)
	End Sub

	Public Shared Function SolicitarConfirmacaoViaAPISistemaOperacionalLocal(mensagem As String) As Boolean
		Dim buttons As MessageBoxButtons = MessageBoxButtons.YesNo
		Dim result As DialogResult
		result = MessageBox.Show(mensagem, "Confirmação", buttons)
		Return result = DialogResult.Yes
	End Function

	Public Shared Sub RedirecionarClienteParaEvitarResubimissaoDeFormulario(response As HttpResponse, request As HttpRequest, context As HttpContext)
		response.Redirect(request.RawUrl, False)
		context.ApplicationInstance.CompleteRequest()
	End Sub

	Public Shared Sub RedirecionarParaPagina(page As Page, request As HttpRequest, urlPagina As String, Optional mensagemAposRedirecionamento As String = "", Optional tipoMensagem As TiposMensagem = TiposMensagem.Sucesso)
		RedirecionarParaPaginaComDelay(page, request, urlPagina, 0, mensagemAposRedirecionamento, tipoMensagem)
	End Sub

	Public Shared Sub RedirecionarParaPaginaComDelay(page As Page, request As HttpRequest, urlPagina As String, Optional delay As Integer = 1500, Optional mensagemAposRedirecionamento As String = "", Optional tipoMensagem As TiposMensagem = TiposMensagem.Sucesso)
		urlPagina = page.ResolveUrl(urlPagina)

		Dim script = $"
                setTimeout(function() {{
                    window.location.href = '{urlPagina}';
                }}, {delay});"

		If Not String.IsNullOrEmpty(mensagemAposRedirecionamento) Then
			AdicionarMensagemParaSerExibidaNoProximoCarregamentoDePagina(page, mensagemAposRedirecionamento, tipoMensagem)
		End If
		ScriptManager.RegisterStartupScript(page, page.GetType(), "redirecionarComDelay", script, True)
	End Sub

	Public Shared Sub AdicionarMensagemParaSerExibidaNoProximoCarregamentoDePagina(page As Page, mensagem As String, tipo As TiposMensagem)
		Dim script = $"myApp.ServicoMensagensAoCarregarPaginas.adicionarMensagem('{mensagem}', '{ChrW(tipo)}');"
		ScriptManager.RegisterStartupScript(page, page.GetType(), "mensagemLocalStorage", script, True)
	End Sub
End Class
