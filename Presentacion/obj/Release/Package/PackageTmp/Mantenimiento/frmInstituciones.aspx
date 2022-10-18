<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmInstituciones.aspx.cs" Inherits="Presentacion.Mantenimiento.frmInstituciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoJS" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="encabezado" runat="server"></asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Enlaces" runat="server"></asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Contenido" runat="server">
    <div class="FormatoBotones">
        <asp:Button ID="btnInstitucionesoNuevo" runat="server" Text="NUEVO" OnClick="btnInstitucionesoNuevo_Click" CssClass="ButtonNeutro"/>
        <asp:Button ID="btnInstitucionesGuardar" runat="server" Text="GUARDAR" OnClick="btnInstitucionesGuardar_Click" Visible="false" CssClass="ButtonNeutro"/>
        <asp:Button ID="btnInstitucionesVolver" runat="server" Text="VOLVER" OnClick="btnInstitucionesVolver_Click" Visible="false" CssClass="ButtonNeutro"/>
             
    </div> 
    <div class="col-md-12" id="tblTposCambio">
        <h3>Instituciones</h3>
        <p>Mantenimiento de Instituciones del Sistema Gestor.</p>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="Label1" runat="server" Text="Código:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"><asp:TextBox ID="txbBusqIdSociedadGL" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="Label2" runat="server"   TextMode="Nombre"   Text="Fecha:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"><asp:TextBox ID="txbBusqDenominacion" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="Label3" runat="server" Text="Estado:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"><asp:TextBox ID="txbBusqEstado" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-12" style="text-align:center;"><asp:Button ID="btnInstitucionesConsultar" runat="server" Text="CONSULTAR" OnClick="btnInstitucionesConsultar_Click" CssClass="ButtonNeutro" /></div>
        <div class="col-md-12"><asp:label ID="lblMensaje" runat="server" Visible="false" Font-Bold="true" ></asp:label></div>
    </div>
    <div style="width: 100%; height: 100%; overflow: auto">
        <asp:GridView ID="grdvInstituciones" runat="server" AutoGenerateColumns="False"  ShowHeaderWhenEmpty="True"
             CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
            Width="100%" OnSelectedIndexChanged="grdvInstituciones_SelectedIndexChanged" OnRowEditing="grdvInstituciones_RowEditing"
            OnPageIndexChanging="grdvInstituciones_PageIndexChanging"
            OnRowCancelingEdit="grdvInstituciones_RowCancelingEdit">
                        <Columns>

                            <asp:CommandField ShowEditButton="False" />

                            <asp:TemplateField HeaderText="Código" > 
                                <ItemTemplate>
                                    <asp:Label ID="lblCodigo" runat="server"   TextMode="Date"    Text='<%# Bind("IdSociedadGL") %>'></asp:Label>
                                </ItemTemplate>                           
                                <FooterTemplate>
				                    <asp:TextBox ID="txbInsertCodigo" runat="server"   TextMode="Date"     Text='<%# Bind("IdSociedadGL") %>' MaxLength="2" />
                                </FooterTemplate>
                        
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Nombre Corto" > 
                                <ItemTemplate>
                                    <asp:Label ID="lblIdMoneda" runat="server"  Text='<%# Bind("Denominacion") %>'></asp:Label>
                                </ItemTemplate>                           
                                <FooterTemplate>
				                    <asp:TextBox ID="txbInsertIdMoneda" runat="server"  Text='<%# Bind("Denominacion") %>' MaxLength="18" />
                                </FooterTemplate>
                        
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Nombre Largo" > 
                                <ItemTemplate>
                                    <asp:Label ID="lblTpoTransaccion" runat="server" Text='<%# Bind("NbrSociedad") %>'></asp:Label>
                                </ItemTemplate>                           
                                <FooterTemplate>
				                    <asp:TextBox ID="txbInsertTpoTransaccion" runat="server"  Text='<%# Bind("NbrSociedad") %>' MaxLength="50" />
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="País" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txbIdPais" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblIdPais" runat="server"   TextMode="Number"  Text='<%# Bind("IdPais") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txbEditIdPais" runat="server" Width="50%"   TextMode="Number"  Text='<%# Bind("IdPais") %>' MaxLength="100"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Moneda" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txbMoneda" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblMoneda" runat="server"   TextMode="Number"  Text='<%# Bind("IdMoneda") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txbEditMoneda" runat="server" Width="50%"   TextMode="Number"  Text='<%# Bind("IdMoneda") %>' MaxLength="100"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Estado" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txbValor" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblValor" runat="server"   TextMode="Number"  Text='<%# Bind("Estado") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txbEditValor" runat="server" Width="50%"   TextMode="Number"  Text='<%# Bind("Estado") %>' MaxLength="100"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                        </Columns>  
                    </asp:GridView>
    </div>
</asp:Content>
