<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="GPDropDownList.ascx.vb" Inherits="WebEcommerceGabriel.GPDropDownList" %>

<div class="gp-container-input <%=CssClass%>" style="<%=Style%>">
    <asp:Label ID="Label" runat="server"></asp:Label>
    <div class="gp-input">
        <asp:DropDownList ID="DropDownList" runat="server" CssClass="form-control form-select" OnSelectedIndexChanged="DropDownList_TextChanged"  OnTextChanged="DropDownList_TextChanged" AutoPostBack="True"></asp:DropDownList>
		<div class="gp-input-container-custom-validator ">
			<asp:CustomValidator ID="CustomValidator" runat="server" ControlToValidate="DropDownList" ErrorMessage=""
			CssClass="erro" OnServerValidate="CustomValidator_ServerValidate" ValidateEmptyText="true"></asp:CustomValidator>
			<%--ValidateEmptyText="true" serve para que o validation seja chamado mesmo que o campo seja nulo, desta pode ser feito 
			também validação de campo obrigatório no lado do servidor sem usar o require requireValidaion--%>
		</div>
    </div>
</div>