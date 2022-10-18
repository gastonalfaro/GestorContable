<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmClasesDocumento.aspx.cs" Inherits="Presentacion.Mantenimiento.frmClasesDocumento" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoJS" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Contenido" runat="server">
    <div class="FormatoBotones">
        <asp:Button ID="btnClaseDocumentoNuevo" runat="server" Text="NUEVO" OnClick="btnClaseDocumentoNuevo_Click" Visible="false" CssClass="ButtonNeutro"/>
        <asp:Button ID="btnClaseDocumentoGuardar" runat="server" Text="GUARDAR" OnClick="btnClaseDocumentoGuardar_Click" Visible="false" CssClass="ButtonNeutro"/>
        <asp:Button ID="btnClaseDocumentoVolver" runat="server" Text="VOLVER" OnClick="btnClaseDocumentoVolver_Click" Visible="false" CssClass="ButtonNeutro"/>
               
    </div> 
    <div class="col-md-12" id="tblClasesDocumento">
        <h3>CLASES DE DOCUMENTO</h3>
        <p>Consulta de Clases de Documento del Sistema Gestor.</p>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="Label1" runat="server" Text="Código:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"><asp:TextBox ID="txtBusqIdClaseDocumento" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="Label2" runat="server" Text="Descripción:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"><asp:TextBox ID="txtBusqNomClaseDocumento" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-12" style="text-align:center;"> <asp:Button ID="btnClaseDocumentoConsultar" runat="server" Text="CONSULTAR" OnClick="btnClaseDocumentoConsultar_Click" CssClass="ButtonNeutro"/></div>
     <div class="col-md-12" style="text-align:center;"><asp:label ID="lblMensaje" runat="server" Visible="false" Font-Bold="true" ></asp:label></div>
    </div>
    <div style="width: 100%; height: 100%; overflow: auto">
   
        <asp:GridView ID="grdvClasesDocumento" runat="server" AutoGenerateColumns="False"  ShowHeaderWhenEmpty="True"
             CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
            Width="100%" OnSelectedIndexChanged="grdvClasesDocumento_SelectedIndexChanged" OnRowEditing="grdvClasesDocumento_RowEditing"
            OnRowUpdating="grdvClasesDocumento_RowUpdating" OnPageIndexChanging="grdvClasesDocumento_PageIndexChanging"
            OnRowCancelingEdit="grdvClasesDocumento_RowCancelingEdit" PageSize="20" AllowPaging="True">
                        <Columns>

                            <asp:TemplateField HeaderText="Código"  > 
                                <ItemTemplate>
                                    <asp:Label ID="lblIdClaseDocumento" runat="server" Text='<%# Bind("IdClaseDoc") %>'></asp:Label>
                                </ItemTemplate>                           
                                <FooterTemplate>
				                    <asp:TextBox ID="txtInsertIdClaseDocumento" runat="server"  Text='<%# Bind("IdClaseDoc") %>' MaxLength="4" />
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Descripción" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtNuevoNomClaseDoc" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblNombre" runat="server" Text='<%# Bind("NomClaseDoc") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditNomClaseDoc" runat="server" Width="90%" Text='<%# Bind("NomClaseDoc") %>' MaxLength="100"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Estado" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtEstado" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblEstado" runat="server" Text='<%# Bind("Estado") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditEstado" runat="server" Width="90%" Text='<%# Bind("Estado") %>' MaxLength="100"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                        </Columns>  

                        <EditRowStyle BackColor="#999999" />

                    </asp:GridView>
    </div>
</asp:Content>
