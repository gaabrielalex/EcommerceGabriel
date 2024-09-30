<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="TextFormField.ascx.vb" Inherits="WebEcommerceGabriel.TextFormField" %>

<link rel="stylesheet" href="\src\components\TextFormField\TextFormField.css" />

<div class="gp-container-input <%=CssClass%>" style="<%=Style%>">
    <asp:Label ID="LabelControl" runat="server"></asp:Label>
    <div class="gp-input">
        <input type="hidden" id="InputHiddenIdDado" runat="server" value=""/>
	    <asp:TextBox ID="TextBoxControl" runat="server" CssClass="form-control" ></asp:TextBox>
        <div class="gp-input-container-custom-validator ">
            <asp:CustomValidator ID="CustomValidator" runat="server" ControlToValidate="TextBoxControl" ErrorMessage=""
            CssClass="erro" OnServerValidate="CustomValidator_ServerValidate" ValidateEmptyText="true"></asp:CustomValidator>
            <%--ValidateEmptyText="true" serve para que o validation seja chamado mesmo que o campo seja nulo, desta pode ser feito 
            também validação de campo obrigatório no lado do servidor sem usar o require requireValidaion--%>
        </div>
    </div>
</div>
