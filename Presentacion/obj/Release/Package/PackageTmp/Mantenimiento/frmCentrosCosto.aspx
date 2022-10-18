<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmCentrosCosto.aspx.cs" Inherits="Presentacion.Mantenimiento.frmCentrosCosto" %>

<asp:Content ID="Content3" ContentPlaceHolderID="encabezado" runat="server"></asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoJS" runat="server"></asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Contenido" runat="server">
    <div class="FormatoBotones">
          <%--<asp:Button ID="btnCentrosCostoNuevo" runat="server" Text="NUEVO" OnClick="btnCentrosCostoNuevo_Click" />
        <asp:Button ID="btnCentrosCostoGuardar" runat="server" Text="GUARDAR" OnClick="btnCentrosCostoGuardar_Click" Visible="false" />--%>
        <asp:Button ID="btnCentrosCostoVolver" runat="server" Text="VOLVER" OnClick="btnCentrosCostoVolver_Click" Visible="false" CssClass="ButtonNeutro"/>
    </div> 
    <div class="col-md-12" id="tblCentrosCosto">
        <h3>CENTROS DE COSTO</h3>
        <p>Consulta de Centros de Costo del Sistema Gestor.</p>
            <div class="col-md-6">
                <div class="col-md-3"><asp:Label ID="lblIdCentroCosto" runat="server" Text="Código:" Font-Bold="true"></asp:Label></div>
                <div class="col-md-5"><asp:TextBox ID="txtIdCentroCosto" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
            </div>
            <div class="col-md-6">
                <div class="col-md-3"> <asp:Label ID="FchVigencia" runat="server"   TextMode="Date"   Text="Fecha Vigencia Hasta:" Font-Bold="true"></asp:Label></div>
                <div class="col-md-5">
                    <asp:TextBox ID="txtFchVigencia"  runat="server" CssClass="js-date-picker FormatoTextBox"></asp:TextBox>                   
                </div>
            </div>
            <div class="col-md-6">
                <div class="col-md-3"><asp:Label ID="lblDescrib" runat="server" Text="Descripción:" Font-Bold="true"></asp:Label></div>
                <div class="col-md-5"><asp:TextBox ID="txtNomCentroCosto" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
            </div>
            <div class="col-md-6">
                <div class="col-md-3"><asp:Label ID="lblIdSociedadCo" runat="server" Text="Sociedad Co:" Font-Bold="true"></asp:Label></div>
                <div class="col-md-5"><asp:TextBox ID="txtIdSociedadCo" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
            </div>
            <div class="col-md-6">
                <div class="col-md-3"><asp:Label ID="lblIdSociedadFi" runat="server"   Text="Sociedad Fi:" Font-Bold="true"></asp:Label></div>
                <div class="col-md-5"><asp:TextBox ID="txtIdSociedadFi" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
            </div>
            <div class="col-md-12" style="text-align:center;">  <asp:Button ID="btnCentrosCostoConsultar" runat="server" Text="CONSULTAR" OnClick="btnCentrosCostoConsultar_Click" CssClass="ButtonNeutro" /></div>
             <div class="col-md-12" style="text-align:center;"> <asp:label ID="lblMensaje" runat="server" Visible="false" Font-Bold="true" ></asp:label></div>
     </div>
    <div style="width: 100%; height: 100%; overflow: auto">
                    <asp:GridView ID="grdvCentrosCosto" runat="server" AutoGenerateColumns="False"  ShowHeaderWhenEmpty="True"
            Width="100%" OnSelectedIndexChanged="grdvCentrosCosto_SelectedIndexChanged" OnRowEditing="grdvCentrosCosto_RowEditing"
            CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
            OnRowUpdating="grdvCentrosCosto_RowUpdating" OnRowUpdated="grdvCentrosCosto_RowUpdated" OnPageIndexChanging="grdvCentrosCosto_PageIndexChanging"
            OnRowCancelingEdit="grdvCentrosCosto_RowCancelingEdit" AllowPaging="true" PageSize="20">
                        <Columns>
                            <asp:TemplateField HeaderText="Código"   > 
                                <ItemTemplate>
                                    <asp:Label ID="lblIdCentroCosto" runat="server"   TextMode="Date"    Text='<%# Bind("IdCentroCosto") %>'></asp:Label>
                                </ItemTemplate>                           
                                <FooterTemplate>
				                    <asp:TextBox ID="txtInsertIdCentroCosto" runat="server" TextMode="Date" Text='<%# Bind("IdCentroCosto") %>' MaxLength="2" />
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Descripción" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtNomCentroCosto" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblNomCentroCosto" runat="server"   TextMode="Number"  Text='<%# Bind("NomCentroCosto") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditNomCentroCosto" runat="server" Width="50%"   TextMode="Number"  Text='<%# Bind("NomCentroCosto") %>' MaxLength="100"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Sociedad CO" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtIdSociedadCo" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblIdSociedadCo" runat="server"   TextMode="Number"  Text='<%# Bind("IdSociedadCo") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditIdSociedadCo" runat="server" Width="50%"   TextMode="Number"  Text='<%# Bind("IdSociedadCo") %>' MaxLength="100"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Sociedad Fi" >

                                <ItemTemplate>
                                    <asp:Label ID="lblIdSociedadFi" runat="server" Text='<%# Bind("IdSociedadFi") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Centro Beneficio" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtIdCentroBeneficio" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblIdCentroBeneficio" runat="server"   TextMode="Number"  Text='<%# Bind("IdCentroBeneficio") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditIdCentroBeneficio" runat="server" Width="50%"   TextMode="Number"  Text='<%# Bind("IdCentroBeneficio") %>' MaxLength="100"  />
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

                            <asp:TemplateField HeaderText="FchVigenciaHasta" > 
                                <ItemTemplate>
                                    <asp:Label ID="lblFchVigenciaHasta" runat="server" Text='<%# Bind("FchVigenciaHasta") %>'></asp:Label>
                                </ItemTemplate>                           
                                <FooterTemplate>
				                    <asp:TextBox ID="txtInsertFchVigenciaHasta" runat="server"  Text='<%# Bind("FchVigenciaHasta") %>' MaxLength="50" />
                                </FooterTemplate>
                            </asp:TemplateField>
                        </Columns>  
                        <EditRowStyle BackColor="#999999" />

            </asp:GridView>
    </div>
</asp:Content>

