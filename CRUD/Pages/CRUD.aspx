<%@ Page Title="" Language="C#" MasterPageFile="~/MP.Master" AutoEventWireup="true" CodeBehind="CRUD.aspx.cs" Inherits="CRUD.Pages.CRUD" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    CRUD
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script>
        function validarFormulario(validar) {
            if (validar) {
                var nombre = document.getElementById('<%= tbnombre.ClientID %>').value;
                var rut = document.getElementById('<%= tbrut.ClientID %>').value;
                var email = document.getElementById('<%= tbemail.ClientID %>').value;
                var fecha = document.getElementById('<%= tbdate.ClientID %>').value;

                if (nombre === "" || rut === "" || email === "" || fecha === "") {
                    alert("Por favor, complete todos los campos.");
                    return false;
                }
            }
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <br />
    <div class="mx-auto" style="width: 250px">
        <asp:Label runat="server" CssClass="h2" ID="lbltitulo"></asp:Label>
    </div>
    <form runat="server" class="h-100 d-flex align-items-center justify-content-center">
        <div>
            <div class="mb-3">
                <label class="form-label">Nombre</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="tbnombre"></asp:TextBox>
            </div>
            <div class="mb-3">
                <label class="form-label">Rut</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="tbrut"></asp:TextBox>
            </div>
            <div class="mb-3">
                <label class="form-label">Email</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="tbemail"></asp:TextBox>
            </div>
            <div class="mb-3">
                <label class="form-label">Fecha de Nacimiento</label>
                <asp:TextBox runat="server" TextMode="Date" CssClass="form-control" ID="tbdate"></asp:TextBox>
            </div>
            <asp:Button runat="server" CssClass="btn btn-primary" ID="BtnCreate" Text="CREAR" Visible="false" OnClick="BtnCreate_Click" OnClientClick="return validarFormulario(true);" />
            <asp:Button runat="server" CssClass="btn btn-primary" ID="BtnUpdate" Text="MODIFICAR" Visible="false" OnClick="BtnUpdate_Click" OnClientClick="return validarFormulario(true);" />
            <asp:Button runat="server" CssClass="btn btn-primary" ID="BtnDelete" Text="ELIMINAR" Visible="false" OnClick="BtnDelete_Click" />
            <asp:Button runat="server" CssClass="btn btn-primary btn-dark" ID="BtnVolver" Text="VOLVER" Visible="true" OnClick="BtnVolver_Click" />
        </div>
    </form>
</asp:Content>
