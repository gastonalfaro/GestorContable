<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmEntidadesCP.aspx.cs" Inherits="Presentacion.Mantenimiento.frmEntidadesCP" %>
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
        <%--                    <asp:Button ID="btnNuevosEntidadesCP" runat="server" Text="NUEVO" OnClick="btnNuevosEntidadesCP_Click" />
        <asp:Button ID="btnGuardarEntidadesCP" runat="server" Text="GUARDAR" OnClick="btnGuardarEntidadesCP_Click" Visible="false" />
        <asp:Button ID="btnVolverEntidadesCP" runat="server" Text="VOLVER" OnClick="btnVolverEntidadesCP_Click" Visible="false" />--%>
    </div> 
    <div class="col-md-12" id="tblEntidadesCP">
        <h3>ENTIDADES DE CONTROL PRESUPUESTARIO</h3>
        <p>Consulta de Entidades de Control Presupuestario del Sistema Gestor.</p>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="lblIdEntidadCP" runat="server" Text="Código:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"><asp:TextBox ID="txtBusquedaIdEntidadCP" runat="server" Enabled="true" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="lblBusquedaNomEntidadCP" runat="server" Text="Descripción:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"><asp:TextBox ID="txtBusquedaNomEntidadCP" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="lblBusquedaIdMoneda" runat="server" Text="Moneda:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"><asp:TextBox ID="txtBusquedaIdMoneda" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-12" style="text-align:center;"><asp:Button ID="btnConsultarEntidadCP" runat="server" Text="CONSULTAR" OnClick="btnConsultarEntidadCP_Click" CssClass="ButtonNeutro" /></div>
        <div class="col-md-12" style="text-align:center;"> <asp:label ID="lblMensaje" runat="server" Visible="false" Font-Bold="true" ></asp:label></div>
    </div>
    <div style="width: 100%; height: 100%; overflow: auto">

        <asp:GridView ID="grdEntidadesCP" runat="server" AutoGenerateColumns="False"  ShowHeaderWhenEmpty="True"
             CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
            Width="100%" OnSelectedIndexChanged="grdEntidadesCP_SelectedIndexChanged" OnRowEditing="grdEntidadesCP_RowEditing"
            OnRowUpdating="grdEntidadesCP_RowUpdating" OnPageIndexChanging="grdEntidadesCP_PageIndexChanging"
            OnRowCancelingEdit="grdEntidadesCP_RowCancelingEdit" AllowPaging="true" PageSize="20">
                        <Columns>

                            <asp:TemplateField HeaderText="Código"   > 
                                <ItemTemplate>
                                    <asp:Label ID="lblIdEntidadCP" runat="server" Text='<%# Bind("IdEntidadCP") %>'></asp:Label>
                                </ItemTemplate>                           
                                <FooterTemplate>
				                    <asp:TextBox ID="txtInsertarIdEntidadCP" runat="server" Text='<%# Bind("IdEntidadCP") %>' MaxLength="2" />
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Descripción" > 
                                <ItemTemplate>
                                    <asp:Label ID="lblIdNomEntidadCP" runat="server"  Text='<%# Bind("NomEntidadCP") %>'></asp:Label>
                                </ItemTemplate>                           
                                <FooterTemplate>
				                    <asp:TextBox ID="txtInsertarNomEntidadCP" runat="server"  Text='<%# Bind("NomEntidadCP") %>' MaxLength="18" />
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Moneda" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtMoneda" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblMoneda" runat="server"   TextMode="Number"  Text='<%# Bind("IdMoneda") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditarMoneda" runat="server" Width="50%"   TextMode="Number"  Text='<%# Bind("IdMoneda") %>' MaxLength="100"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Estado" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtEstado" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblEstado" runat="server"   TextMode="Number"  Text='<%# Bind("Estado") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditarEstado" runat="server" Width="50%"   TextMode="Number"  Text='<%# Bind("Estado") %>' MaxLength="100"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                        </Columns>  

                        <EditRowStyle BackColor="#999999" />

            </asp:GridView>

    </div>
</asp:Content>
