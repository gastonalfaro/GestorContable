<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmFormulariosCaptura.aspx.cs" Inherits="Presentacion.CapturaIngresos.frmFormulariosCaptura" %>
<asp:Content ID="Content1" ContentPlaceHolderID="encabezado" runat="server">  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenido" runat="server">
    <div class="col-md-12"><h3>Formularios</h3></div>
    <div class="col-md-12">Consulta de Formularios del Sistema Gestor.</div>
    
    <div class="col-md-12">
        <div style="position:absolute;right:15px;">
              <asp:Button ID="btnFormulariosVolver" runat="server" Text="VOLVER" OnClick="btnFormulariosVolver_Click" Visible="false" CssClass="ButtonNeutro"/>   
        </div>
    </div>

    <div  class="col-md-6">
        <div class="row">
            <div class="col-md-5"><asp:Label ID="Label1" runat="server" Text="Número:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7"> <asp:TextBox ID="txtBusqIdFormularios" runat="server" onkeypress="return AceptarSoloNumeros(event)" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
         <div class="row">
            <div class="col-md-5"><asp:Label ID="Label3" runat="server" Text="Tipo Id.:" Font-Bold="true" ></asp:Label> </div>
            <div class="col-md-7">
                 <asp:DropDownList ID="ddlTipoPersona" runat="server" TextMode="Text" OnSelectedIndexChanged="ddlTipoPersona_SelectedIndexChanged" AutoPostBack="true" CssClass="FormatoDropDownList">
                    <asp:ListItem Value="">--Seleccione--</asp:ListItem>
                    <asp:ListItem Value="F">Fisico</asp:ListItem>
                    <asp:ListItem Value="J">Juridico</asp:ListItem>
                    <asp:ListItem Value="DI">DIMEX</asp:ListItem>
                </asp:DropDownList> 
            </div>
        </div>
    </div>
     <div  class="col-md-6">
        <div class="row">
            <div class="col-md-5"><asp:Label ID="Label2" runat="server" Text="Año:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7"><asp:TextBox ID="txtBusqAnnos" runat="server" MaxLength="4" onkeypress="return AceptarSoloNumeros(event)" CssClass="FormatoTextBox" ></asp:TextBox></div>
        </div>
         <div class="row">
            <div class="col-md-5"><asp:Label ID="Label4" runat="server" Text="Identificación:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7"> 
                <asp:TextBox  ID="txtIdPersona" runat="server" MaxLength="12"  AutoPostBack="true" CssClass="FormatoTextBox"></asp:TextBox>
                <asp:Label ID="lblNombre" runat="server" TextMode="SingleLine" Width="200"></asp:Label>
            </div>
        </div>
    </div>
    <div class="col-md-12">
        <div style="text-align:center;">
               <asp:Button ID="btnFormulariosConsultar" runat="server" Text="CONSULTAR" OnClick="btnFormulariosConsultar_Click" CssClass="ButtonNeutro"/>
        </div>
    </div>
    <div class="col-md-12">
         <asp:label ID="lblMensaje" runat="server" Visible="false" Font-Bold="true" ></asp:label>
    </div>
    <div style="width: 100%; height: 100%; overflow: auto">                   
                    
        <asp:GridView ID="grdvFormularios" runat="server" AutoGenerateColumns="False"  ShowHeaderWhenEmpty="True"
            Width="100%" OnSelectedIndexChanged="grdvFormularios_SelectedIndexChanged" OnRowEditing="grdvFormularios_RowEditing"
            OnRowUpdating="grdvFormularios_RowUpdating" OnPageIndexChanging="grdvFormularios_PageIndexChanging"
              CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
            OnRowCancelingEdit="grdvFormularios_RowCancelingEdit" PageSize="15" AllowPaging="True">
                        <Columns>

                            <asp:TemplateField HeaderText="Número" ItemStyle-HorizontalAlign="Center"> 
                                <ItemTemplate>
                                    <asp:Label ID="lblIdFormulario" runat="server" Text='<%# Bind("IdFormulario") %>'></asp:Label>
                                </ItemTemplate>                           
                                <FooterTemplate>
				                    <asp:TextBox ID="txtInsertIdFormulario" runat="server"  Text='<%# Bind("IdFormulario") %>' MaxLength="32" />
                                </FooterTemplate>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Año" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtAnno" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblAnno" runat="server" Text='<%# Bind("Anno") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditAnno" runat="server" Width="10%" Text='<%# Bind("Anno") %>' MaxLength="32"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Persona" ItemStyle-HorizontalAlign="Center">
                                <FooterTemplate>
                                    <asp:TextBox ID="txtIdPersona" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblPersona" runat="server" Text='<%# Bind("IdPersona") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditIdPersona" runat="server" Width="90%" Text='<%# Bind("IdPersona") %>' MaxLength="100"  />
                                </EditItemTemplate> 

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Persona Tramite" ItemStyle-HorizontalAlign="Center">
                                <FooterTemplate>
                                    <asp:TextBox ID="txtIdPersonaTramite" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblPersonaTramite" runat="server" Text='<%# Bind("IdPersonaTramite") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditIdPersonaTramite" runat="server" Width="90%" Text='<%# Bind("IdPersonaTramite") %>' MaxLength="100"  />
                                </EditItemTemplate> 

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Institución" ItemStyle-HorizontalAlign="Center">
                                <FooterTemplate>
                                    <asp:TextBox ID="txtIdSociedadGL" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblIdSociedadGL" runat="server" Text='<%# Bind("IdSociedadGL") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditIdSociedadGL" runat="server" Width="90%" Text='<%# Bind("IdSociedadGL") %>' MaxLength="100"  />
                                </EditItemTemplate> 

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Dependencia" ItemStyle-HorizontalAlign="Center">
                                <FooterTemplate>
                                    <asp:TextBox ID="txtIdOficina" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblIdOficina" runat="server" Text='<%# Bind("IdOficina") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditIdOficina" runat="server" Width="90%" Text='<%# Bind("IdOficina") %>' MaxLength="100"  />
                                </EditItemTemplate> 

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>

                                <asp:TemplateField HeaderText="Expediente" ItemStyle-HorizontalAlign="Center">
                                <FooterTemplate>
                                    <asp:TextBox ID="txtNroExpediente" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblNroExpediente" runat="server" Text='<%# Bind("NroExpediente") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditNroExpediente" runat="server" Width="90%" Text='<%# Bind("NroExpediente") %>' MaxLength="100"  />
                                </EditItemTemplate> 

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Descripción" ItemStyle-HorizontalAlign="Center">
                                <FooterTemplate>
                                    <asp:TextBox ID="txtDescripcion" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblDescripcion" runat="server" Text='<%# Bind("Descripcion") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditDescripcion" runat="server" Width="90%" Text='<%# Bind("Descripcion") %>' MaxLength="100"  />
                                </EditItemTemplate> 

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Moneda" ItemStyle-HorizontalAlign="Center">
                                <FooterTemplate>
                                    <asp:TextBox ID="txtMoneda" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblMoneda" runat="server" Text='<%# Bind("IdMoneda") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditMoneda" runat="server" Width="90%" Text='<%# Bind("IdMoneda") %>' MaxLength="100"  />
                                </EditItemTemplate> 

                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>                           

                            <asp:BoundField DataField="Monto" DataFormatString="{0:N}" HeaderText="Monto" NullDisplayText="0" />

                            <asp:TemplateField HeaderText="Estado" ItemStyle-HorizontalAlign="Center">
                                <FooterTemplate>
                                    <asp:TextBox ID="txtEstado" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblEstado" runat="server" Text='<%# Bind("DesEstado") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditEstado" runat="server" Width="90%" Text='<%# Bind("DesEstado") %>' MaxLength="100"  />
                                </EditItemTemplate> 

                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>

                            
                            <asp:TemplateField HeaderText="Ver" ShowHeader="True" HeaderStyle-HorizontalAlign="Left">
                                <EditItemTemplate> 
                                    <asp:LinkButton ID="lnkActualizarFormulario" runat="server" CausesValidation="True" CommandName="Update" Text="Actualizar"></asp:LinkButton> 
                                    <asp:LinkButton ID="lnkCancelarFormulario" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancelar"></asp:LinkButton> 
                                </EditItemTemplate> 
                                <ItemTemplate>
                                    <%--<asp:LinkButton ID="lbtEditarFormulario" runat="server" CausesValidation="False" CommandName="Edit" Text="Editar"></asp:LinkButton>--%>
                                    <asp:LinkButton ID="lbtIraFormulario" runat="server" CausesValidation="False" CommandName="Edit" Text="Ver"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>  

                        <EditRowStyle BackColor="#999999" />
                    </asp:GridView>                     
               
    </div>
</asp:Content>
