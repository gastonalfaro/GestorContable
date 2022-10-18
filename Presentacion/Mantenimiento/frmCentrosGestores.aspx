<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmCentrosGestores.aspx.cs" Inherits="Presentacion.Mantenimiento.frmCentrosGestores" %>

<asp:Content ID="Content3" ContentPlaceHolderID="encabezado" runat="server"></asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoJS" runat="server"></asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="Contenido" runat="server">
    <div class="FormatoBotones">
        <%--<asp:Button ID="btnCentroGestorNuevo" runat="server" Text="NUEVO" OnClick="btnCentroGestorNuevo_Click" />
            <asp:Button ID="btnCentroGestorGuardar" runat="server" Text="GUARDAR" OnClick="btnCentroGestorGuardar_Click" Visible="false" />
            <asp:Button ID="btnCentroGestorVolver" runat="server" Text="VOLVER" OnClick="btnCentroGestorVolver_Click" Visible="false" />
        --%>
    </div> 
    <div class="col-md-12" id="tblCentrosGestores">
        <h3>CENTROS GESTORES</h3>
        <p>Consulta de Centros Gestores del Sistema Gestor.</p>
            <div class="col-md-6">
                <div class="col-md-3"><asp:Label ID="Label1" runat="server" Text="Código:" Font-Bold="true"></asp:Label></div>
                <div class="col-md-5"><asp:TextBox ID="txtBusqIdCentroGestor" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
            </div>
            <div class="col-md-6">
                <div class="col-md-3"><asp:Label ID="Label2" runat="server" Text="Descripción:" Font-Bold="true"></asp:Label></div>
                <div class="col-md-5"><asp:TextBox ID="txtBusqNomCentroGestor" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
            </div>
            <div class="col-md-6">
                <div class="col-md-3"><asp:Label ID="Label3" runat="server" Text="Fecha Vigencia Hasta:" Font-Bold="true"></asp:Label></div>
                <div class="col-md-5">
                       <asp:TextBox ID="txtFchVigenciaHasta" runat="server" CssClass="js-date-picker FormatoTextBox"></asp:TextBox>
                       
                </div>
            </div>
            <div class="col-md-12" style="text-align:center;"> <asp:Button ID="btnCentroGestorConsultar" runat="server" Text="CONSULTAR" OnClick="btnCentroGestorConsultar_Click" CssClass="ButtonNeutro" /></div>
             <div class="col-md-12" style="text-align:center;"><asp:label ID="lblMensaje" runat="server" Visible="false" Font-Bold="true" ></asp:label> </div>
    </div>
    <div style="width: 100%; height: 100%; overflow: auto">
        <asp:GridView ID="grdCentrosGestores" runat="server" AutoGenerateColumns="False"  ShowHeaderWhenEmpty="True"
            Width="100%" OnRowEditing="grdCentrosGestores_RowEditing"
             CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
            OnRowUpdating="grdCentrosGestores_RowUpdating" OnPageIndexChanging="grdCentrosGestores_PageIndexChanging"
            OnRowCancelingEdit="grdCentrosGestores_RowCancelingEdit" PageSize="20" AllowPaging="True">
                        <Columns>

                            <asp:TemplateField HeaderText="Código"   > 
                                <ItemTemplate>
                                    <asp:Label ID="lblIdCentroGestor" runat="server" Text='<%# Bind("IdCentroGestor") %>'></asp:Label>
                                </ItemTemplate>                           
                                <FooterTemplate>
				                    <asp:TextBox ID="txtInsertIdCentroGestor" runat="server"  Text='<%# Bind("IdCentroGestor") %>' MaxLength="4" />
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Descripción" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtDenominacion" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblDenominacion" runat="server"   TextMode="Number"  Text='<%# Bind("NomCentroGestor") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditDenominacion" runat="server" Width="50%"   TextMode="Number"  Text='<%# Bind("NomCentroGestor") %>' MaxLength="100"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Entidad CP" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtIdEntidadCP" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblIdIdEntidadCP" runat="server"   TextMode="Number"  Text='<%# Bind("IdEntidadCP") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditIdEntidadCP" runat="server" Width="50%"   TextMode="Number"  Text='<%# Bind("IdEntidadCP") %>' MaxLength="100"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Sociedad Fi" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtIdSociedadFi" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblIdSociedadFi" runat="server"   TextMode="Number"  Text='<%# Bind("IdSociedadFi") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditNomCentrosCosto" runat="server" Width="50%"   TextMode="Number"  Text='<%# Bind("IdSociedadFi") %>' MaxLength="100"  />
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
				                    <asp:TextBox ID="txtEditEstado" runat="server" Width="50%"   TextMode="Number"  Text='<%# Bind("Estado") %>' MaxLength="100"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Fecha Vigencia" > 
                                <ItemTemplate>
                                    <asp:Label ID="lblFchVigencia" runat="server"  Text='<%# Bind("FchVigencia") %>'></asp:Label>
                                </ItemTemplate>                           
                                <FooterTemplate>
				                    <asp:TextBox ID="txtInsertFchVigencia" runat="server"  Text='<%# Bind("FchVigencia") %>' MaxLength="18" />
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Vigencia Hasta" > 
                                <ItemTemplate>
                                    <asp:Label ID="lblFchVigenciaHasta" runat="server" Text='<%# Bind("FchVigenciaHasta") %>'></asp:Label>
                                </ItemTemplate>                           
                                <FooterTemplate>
				                    <asp:TextBox ID="txtInsertFchVigenciaHasta" runat="server"  Text='<%# Bind("FchVigenciaHasta") %>' MaxLength="50" />
                                </FooterTemplate>
                            </asp:TemplateField>

               <%--             <asp:TemplateField HeaderText="Centro Costo" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtNomCentroCosto" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblNomCentroCosto" runat="server"   TextMode="Number"  Text='<%# Bind("Denominacion") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditNomCentroCosto" runat="server" Width="50%"   TextMode="Number"  Text='<%# Bind("Denominacion") %>' MaxLength="100"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>--%>

                        </Columns>  

                        <EditRowStyle BackColor="#999999" />

            </asp:GridView>
    </div>
</asp:Content>
