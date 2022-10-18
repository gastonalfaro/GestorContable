<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmCentrosBeneficio.aspx.cs" Inherits="Presentacion.Mantenimiento.frmCentrosBeneficio" %>

<asp:Content ID="Content3" ContentPlaceHolderID="encabezado" runat="server"></asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoJS" runat="server"></asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Contenido" runat="server">
    <div class="FormatoBotones">
        <%-- <asp:Button ID="btnCentroBeneficioNuevo" runat="server" Text="NUEVO" OnClick="btnCentroBeneficioNuevo_Click" />
            <asp:Button ID="btnCentroBeneficioGuardar" runat="server" Text="GUARDAR" OnClick="btnCentroBeneficioGuardar_Click" Visible="false" />
            <asp:Button ID="btnCentroBeneficioVolver" runat="server" Text="VOLVER" OnClick="btnCentroBeneficioVolver_Click" Visible="false" />
        --%>
    </div>
     <div class="col-md-12">
        <h3>CENTROS DE BENEFICIO</h3>
        <p>Consulta de Centros de Beneficio del Sistema Gestor.</p>
            <div class="col-md-6">
                <div class="col-md-3"><asp:Label ID="Label1" runat="server" Text="Código:" Font-Bold="true"></asp:Label></div>
                <div class="col-md-5"><asp:TextBox ID="txtBusqIdCentroBeneficio" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
            </div>
            <div class="col-md-6">
                <div class="col-md-3"><asp:Label ID="Label2" runat="server" Text="Descripción:" Font-Bold="true"></asp:Label></div>
                <div class="col-md-5"><asp:TextBox ID="txtBusqNomCentroBeneficio" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
            </div>
          <div class="col-md-6">
                <div class="col-md-3"><asp:Label ID="lblFchVigencia" runat="server" Text="Fecha:" Font-Bold="true"></asp:Label></div>
                <div class="col-md-5">
                    <asp:TextBox ID="txtFchVigencia" runat="server" CssClass="js-date-picker FormatoTextBox"></asp:TextBox>
                </div>
            </div>
          <div class="col-md-6">
                <div class="col-md-3"><asp:Label ID="lblIdSociedadCo" runat="server" Text="Sociedad Costo:" Font-Bold="true"></asp:Label></div>
                <div class="col-md-5"><asp:TextBox ID="txtIdSociedadCo" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
            </div>
          <div class="col-md-6">
                <div class="col-md-3"><asp:Label ID="lblIdSociedadFi" runat="server" Text="Sociedad FI:" Font-Bold="true"></asp:Label></div>
                <div class="col-md-5"><asp:TextBox ID="txtIdSociedadFi" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
            </div>
        <div class="col-md-12" style="text-align:center;"><asp:Button ID="btnCentroBeneficioConsultar" runat="server" Text="CONSULTAR" OnClick="btnCentroBeneficioConsultar_Click" CssClass="ButtonNeutro"/></div>
        <div class="col-md-12" style="text-align:center;"><asp:label ID="lblMensaje" runat="server" Visible="false" Font-Bold="true" ></asp:label></div>
    </div>
    <div style="width: 100%; height: 100%; overflow: auto">
       
        <asp:GridView ID="grdvCentrosBeneficio" runat="server" AutoGenerateColumns="False"  ShowHeaderWhenEmpty="True"
             CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
            Width="100%" OnSelectedIndexChanged="grdvCentrosBeneficio_SelectedIndexChanged" OnRowEditing="grdvCentrosBeneficio_RowEditing"
            OnRowUpdating="grdvCentrosBeneficio_RowUpdating" OnPageIndexChanging="grdvCentrosBeneficio_PageIndexChanging"
            OnRowCancelingEdit="grdvCentrosBeneficio_RowCancelingEdit" PageSize="20" AllowPaging="True">
                        <Columns>

                            <asp:TemplateField HeaderText="Código"  > 
                                <ItemTemplate>
                                    <asp:Label ID="lblIdCentroBeneficio" runat="server" Text='<%# Bind("IdCentroBeneficio") %>'></asp:Label>
                                </ItemTemplate>                           
                                <FooterTemplate>
				                    <asp:TextBox ID="txtInsertIdCentroBeneficio" runat="server"  Text='<%# Bind("IdCentroBeneficio") %>' MaxLength="4" />
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Descripción" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtNomCentroBeneficio" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblNomCentroBeneficio" runat="server" Text='<%# Bind("NomCentroBeneficio") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditNomCentroBeneficio" runat="server" Width="90%" Text='<%# Bind("NomCentroBeneficio") %>' MaxLength="100"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Sociedad Co" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtNomNuevoIdSociedadCo" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblIdSociedadCo" runat="server" Text='<%# Bind("IdSociedadCo") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditarIdSociedadCo" runat="server" Width="90%" Text='<%# Bind("IdSociedadCo") %>' MaxLength="100"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Sociedad Fi" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtNomNuevoIdSociedadFi" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblIdSociedadFi" runat="server" Text='<%# Bind("IdSociedadFi") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditIdSociedadFi" runat="server" Width="90%" Text='<%# Bind("IdSociedadFi") %>' MaxLength="100"  />
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

                            <asp:TemplateField HeaderText="Vigencia Desde" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtNomNuevoCentroBeneficio" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblNombre" runat="server" Text='<%# Bind("FchVigencia") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditNombre" runat="server" Width="90%" Text='<%# Bind("FchVigencia") %>' MaxLength="100"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Vigencia Hasta" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtNomNuevoFchVigenciaHasta" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblFchVigenciaHasta" runat="server" Text='<%# Bind("FchVigenciaHasta") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditFchVigenciaHasta" runat="server" Width="90%" Text='<%# Bind("FchVigenciaHasta") %>' MaxLength="100"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                        </Columns>  

                        <EditRowStyle BackColor="#999999" />

                    </asp:GridView>                
    </div>
</asp:Content>
