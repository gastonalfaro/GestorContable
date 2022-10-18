<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="ActualizaContrasennas.aspx.cs" Inherits="Presentacion.ActualizaContrasennas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoJS" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Contenido" runat="server">
    <div style="display:inline-block">
        <div class="col-md-12" style="text-align:center;margin:3px;background-color:lightgray;">
            <asp:Label Text="Se encriptan todas las contraseñas" runat="server" style="display:block;"></asp:Label>
            
            <asp:Label Text="Nota: Para encriptar todas las contraseñas se debe enviar en el SP ConsultarUsuarios la contraseña" runat="server" style="display:block;"></asp:Label>
            <asp:Button ID="btnActualizar" runat="server" CssClass="ButtonNeutro" Text="Actualizar Contraseñas Encriptadas" Width="400px" OnClick="btnActualizar_Click" Enabled="true"/> 

        </div>
          <div class="col-md-12" style="text-align:center;margin:3px;background-color:lightgray;">
            <asp:Label Text="Se encriptan todas las contraseñas ANTIGUAS" runat="server" style="display:block;"></asp:Label>
            <asp:Button ID="btnActualizarAntiguas" runat="server" CssClass="ButtonNeutro" Text="Actualizar Contraseñas Encriptadas ANTIGUAS" Width="400px" OnClick="btnActualizarAntiguas_Click" Enabled="false"/> 

        </div>


         <div class="col-md-12" style="text-align:center;margin:3px;background-color:lightgray;">
            <asp:Label Text="Encripta la contraseña seleccionada" runat="server" style="display:block;"></asp:Label>
             <asp:TextBox ID="txtContrasena" runat="server" style="display:block;" CssClass="FormatoTextBox"></asp:TextBox>
            <asp:Button ID="Button1" runat="server" CssClass="ButtonNeutro" Text="Encriptadas Contraseña" Width="400px" OnClick="btnEncriptaContrasena_Click"/> 
             <asp:Label runat="server" ID="lblTexto" style="display:block;padding:10px;background-color:gray;" Font-Bold="True" Font-Italic="True" ></asp:Label>
        </div>

         <div class="col-md-12" style="text-align:center;margin:3px;background-color:lightgray;">
            <asp:Label Text="Desencripta el texto seleccionada" runat="server" style="display:block;"></asp:Label>
            <asp:TextBox ID="txtDesencripta" runat="server" style="display:block;" CssClass="FormatoTextBox"></asp:TextBox>
            <asp:Button ID="Button2" runat="server" CssClass="ButtonNeutro" Text="Desencriptadas Contraseña" Width="400px" OnClick="btnDesencriptaContrasena_Click"/> 
            <asp:Label runat="server" ID="lblDesencriptado" style="display:block;padding:10px;background-color:gray;" Font-Bold="True" ></asp:Label>
        </div>
    </div>
</asp:Content>
