﻿<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Toast.ascx.vb" Inherits="WebEcommerceGabriel.Toast" %>

<div id="container-toast-message" aria-live="polite" aria-atomic="true" class="d-flex justify-content-end align-items-end w-100;" style="position: fixed; top: 85px; right: 30px; z-index: 9999; overflow: hidden;">
	<div id="toastMessage" class="toast my-toast-message" role="alert" aria-live="assertive" aria-atomic="true">
		<div class="toast-header">
			<%--<img src="" class="rounded me-2" alt="...">--%>
			<strong class="me-auto">Bootstrap</strong>
			<small>Agora</small>
			<button type="button" class="btn-close" data-bs-dismiss="toast" aria-label="Close"></button>
		</div>
		<div class="toast-body">
			<!-- Mensagem será inserida aqui -->
		</div>
	</div>
</div>

<script type="text/javascript" src="\src\components\Toast\Toast.js"> </script>