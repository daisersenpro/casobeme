<%@ Page Title="" Language="C#" MasterPageFile="~/MP.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="CRUD.Pages.Index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Inicio
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <form runat="server">
        <br />
        <div class="mx-auto" style="width:350px">
            <h2>Crear Registro Usuario</h2>
        </div>
        <br />
        <div class="container text-center">
            <div class="row">
                <div class="col">
                    <asp:Button runat="server" ID="BtnCreate" CssClass="btn btn-success" Text="CREAR" OnClick="BtnCreate_Click" style="width: 150px;" />
                </div>
            </div>
        </div>
        <br />
        <div class="container row">
            <div class="table small">
                <asp:GridView runat="server" ID="gvusuarios" class="table table-borderless table-hover">
                    <Columns>
                        <asp:TemplateField HeaderText="Opciones del Administrador">
                            <ItemTemplate>
                                <asp:Button runat="server" Text="LEER" CssClass="btn form-control-sm btn-primary" ID="BtnRead" OnClick="BtnRead_Click"/>
                                <asp:Button runat="server" Text="MODIFICAR" CssClass="btn form-control-sm btn-secondary" ID="BtnUpdate" OnClick="BtnUpdate_Click"/>
                                <asp:Button runat="server" Text="ELIMINAR" CssClass="btn form-control-sm btn-danger" ID="BtnDelete" OnClick="BtnDelete_Click"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </form>
</asp:Content>
