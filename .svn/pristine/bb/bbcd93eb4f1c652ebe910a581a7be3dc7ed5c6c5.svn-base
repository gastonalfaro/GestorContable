<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="CargaCuentaCobrar.aspx.cs" Inherits="Presentacion.Contingentes.CargaCuentaCobrar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoJS" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Contenido" runat="server">
        
            <div class="col-md-12" style="text-align:center;">
            
                 <h3>Cargar pagos</h3>
                </div>
                 <div class="col-md-12" style="text-align:center;">
                    
                    <asp:HyperLink ID="hlDescargarFormato" runat="server">Descargar Formato de archivo</asp:HyperLink>
                   
                 </div>
                 <div class="col-md-12" style="text-align:center;">
                    <div class="col-md-3">

                    <asp:FileUpload ID="fucCargaArchivo" runat="server" />
                        </div>
                        <div class="col-md-3">
                    <asp:Button runat="server" ID="btnSubirArchivo" Text="Subir Archivo" OnClick="btnSubirArchivo_Click" CssClass="ButtonNeutro" />

                    </div>
                     <div class="col-md-6">
                    <asp:Button ID="btnCargarInfo" runat="server" OnClientClick="return confirm('Se cargarán los datos visualizados. ¿Seguro que desea continuar?');" OnClick="btnCargarInfo_Click" Text="Cargar" CssClass="ButtonNeutro" />

                    </div>
                 </div>
                 <div class="col-md-12">
                    
               
                    <asp:Label ID="lblMensaje" runat="server" Text="Estado de la carga: " Font-Bold="True"></asp:Label>
                     
                     <asp:Label ID="lblEstatus" runat="server" Text="" ></asp:Label>
                 </div>
                 <div class="col-md-12">
                    <asp:TextBox ID="txtError" runat="server" width="100%" TextMode="multiline" rows="10" ForeColor="#FF3300" Enabled="false" Visible="false" OnTextChanged="txtError_TextChanged"></asp:TextBox>
                                 
                 </div>
         
      <div class="col-md-12">
        <div style="overflow-x: auto; width: auto" align="center">
            <asp:GridView ID="grvCCSS" runat="server" CellPadding="4" ForeColor="#333333" AllowPaging="True" OnPageIndexChanging="grvCCSS_PageIndexChanging" Font-Size="X-Small" PageSize="20">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
        </div>
       </div>

</asp:Content>
