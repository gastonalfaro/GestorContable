 <%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmDirecciones.aspx.cs" EnableViewState="true" Inherits="Presentacion.Mantenimiento.frmDirecciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoJS" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="server">
    <style type="text/css">
        .ajax__tab_xp .ajax__tab_tab { height: 100%!important; }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID ="encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Contenido" runat="server">
    <asp:HiddenField ID="hdDireccion" runat="server" />
    <div class="FormatoBotones">
         <asp:Button ID="btnNuevasDirecciones" runat="server" Text="NUEVO" OnClick="btnNuevasDirecciones_Click"  Visible="false" CssClass="ButtonNeutro"/>
        <asp:Button ID="btnGuardarDirecciones" runat="server" Text="GUARDAR" OnClick="btnGuardarDirecciones_Click" CssClass="ButtonNeutro"/>
        <asp:Button ID="btnVolverDirecciones" runat="server" Text="VOLVER" OnClick="btnVolverDirecciones_Click" CssClass="ButtonNeutro"/>
    </div> 
    <div class="col-md-12">
        <h3>INSTITUCIÓN</h3>
        <p>Consulta de Direcciones y Dependencias del Sistema Gestor.</p>
        <p></p>
        </div>
    <div class="col-md-12" style="margin-top: 1%;">
        <div class="col-md-6" style="margin-top: 1%;">
            <div class="col-md-4"><asp:Label ID="lblInstitucion" runat="server" Text="Institución:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-8"><asp:TextBox ID="txtInstitucion" runat="server" Enabled="false" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6" style="margin-top: 1%;">
            <div class="col-md-4"><asp:Label ID="lblNombreSoc" runat="server" Text="Nombre:" Font-Bold="true" ></asp:Label></div>
            <div class="col-md-8"> <asp:TextBox ID="txtNombreSoc" runat="server" Enabled="false"  CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        </div>


    <div class="col-md-12" style="margin-top: 1%;">
         <div class="col-md-6" style="margin-top: 1%;">
            <div class="col-md-4"><asp:Label ID="lblIdSociedadFI" runat="server" Text="Sociedad Financiera:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-8"> <asp:TextBox ID="txtIdSociedadFI" runat="server" Enabled="false" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
         <div class="col-md-6" style="margin-top: 1%;">
            <div class="col-md-4"><asp:Label ID="lblNombreSocFI" runat="server" Text="Nombre:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-8"> <asp:TextBox ID="txtNombreSocFI" runat="server" Enabled="false" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div> 
    </div>   
        
    <div class="col-md-12" style="margin-top: 1%;">
        <div class="col-md-6">
            <div class="col-md-4"> <asp:Label ID="Label1" runat="server" Text="Modificar Sociedad Financiera:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-8"><asp:DropDownList runat="server" ID="ddlSociedadesFi" CssClass="FormatoDropDownList"></asp:DropDownList></div>
        </div>

        <div class="col-md-6" >
            <div class="col-md-4"><asp:Label ID="lblEstado" runat="server" Text="Estado:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-8"> <asp:TextBox ID="txtEstadoS" runat="server" Enabled="true" CssClass="FormatoTextBox" MaxLength="5"></asp:TextBox></div>
        </div>
        </div>
     
         <%--<div class="col-md-6">
            <div class="col-md-4"><asp:Label ID="lblCorreo" runat="server" Text="Correo de Notificación:" Font-Bold="True" Visible="False"></asp:Label></div>
            <div class="col-md-8"><asp:TextBox ID="txtCorreo" runat="server" CssClass="FormatoTextBox" Visible="False" ></asp:TextBox></div>
        </div>--%>
     
        <div class="col-md-12" style="text-align:center;margin-top: 3%;"><asp:Button ID="btnConsultarDireccion" runat="server" Text="CONSULTAR" OnClick="btnConsultarDireccion_Click" CssClass="ButtonNeutro" Visible="False" /></div>
   
    <div class="col-md-12" ><asp:label ID="lblMensaje" runat="server" Visible="false" Font-Bold="true" ></asp:label></div>
     <div style="width: 100%; height: 100%; overflow: auto">
                            <ajaxToolkit:TabContainer runat="server" ID="tcntDirecciones" ActiveTabIndex="0">
                        
                        <ajaxToolkit:TabPanel runat="server" HeaderText="Dependencias" ID="tpnlOficinas">
                           
                            <ContentTemplate>
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                
                                    <asp:GridView ID="grdOficinas" runat="server" AutoGenerateColumns="False"  ShowHeaderWhenEmpty="True"
                                        CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
                                        Width="100%" OnSelectedIndexChanged="grdOficinas_SelectedIndexChanged" OnRowEditing="grdOficinas_RowEditing"
                                        OnRowUpdating="grdOficinas_RowUpdating" OnRowUpdated="grdOficinas_RowUpdated" OnPageIndexChanging="grdOficinas_PageIndexChanging"
                                        OnRowCancelingEdit="grdOficinas_RowCancelingEdit" AllowPaging="true" PageSize="20" ShowFooter="true" OnRowDataBound="grdOficinas_RowDataBound">
                                    <Columns>

                                        <asp:TemplateField HeaderText="Dependencia" > 
                                            <ItemTemplate>
                                                <asp:Label ID="lblIdOficina" runat="server" Text='<%# Bind("IdOficina") %>'></asp:Label>
                                            </ItemTemplate>                  
                                            <FooterTemplate>
				                                <asp:TextBox ID="txtInsertarIdOficina" runat="server"/>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Nombre" > 
                                            <ItemTemplate>
                                                <asp:Label ID="lblNomOficina" runat="server"  Text='<%# Bind("NomOficina") %>'></asp:Label>
                                            </ItemTemplate>      
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtEditarNomOficina" runat="server"  Text='<%# Bind("NomOficina") %>' MaxLength="200" />
                                            </EditItemTemplate>                     
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtInsertarNomOficina" runat="server" />
				                            </FooterTemplate>
                                        </asp:TemplateField>

                                          <asp:TemplateField HeaderText="Correo Notificación" > 
                                            <ItemTemplate>
                                                <asp:Label ID="lblCorreoNotifica" runat="server"  Text='<%# Bind("CorreoNotifica") %>'></asp:Label>
                                            </ItemTemplate>      
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtEditarCorreoNotifica" runat="server"  Text='<%# Bind("CorreoNotifica") %>' MaxLength="200" />
                                            </EditItemTemplate>                     
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtInsertarCorreoNotifica" runat="server" />
				                            </FooterTemplate>
                                        </asp:TemplateField>

                                        
                                          <asp:TemplateField HeaderText="Dirección" > 
                                            <ItemTemplate>
                                                <asp:Label ID="lblDireccion" runat="server"  Text='<%# Bind("IdDireccion") %>'></asp:Label>
                                            </ItemTemplate>      
                                            <EditItemTemplate>
                                               <asp:DropDownList ID="ddlEditaDireccion" runat="server"></asp:DropDownList>
                                            </EditItemTemplate>                     
                                            <FooterTemplate>
                                               <asp:DropDownList ID="ddlNuevaDireccion" runat="server"></asp:DropDownList>
				                            </FooterTemplate>
                                        </asp:TemplateField>


                                          <asp:TemplateField HeaderText="Usa Expediente" > 
                                            <ItemTemplate>
                                                <asp:Label ID="lblUsaExpediente" runat="server"  Text='<%# Bind("UsaExpediente") %>'></asp:Label>
                                            </ItemTemplate>      
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtEditarUsaExpediente" runat="server"  Text='<%# Bind("UsaExpediente") %>' MaxLength="1" />
                                            </EditItemTemplate>                     
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtInsertarUsaExpediente" runat="server" MaxLength="1" />
				                            </FooterTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Estado" >
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtInsertarOfiEstado" runat="server"></asp:TextBox>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblOfiEstado" runat="server" Text='<%# Bind("Estado") %>' MaxLength="5" ></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
				                                <asp:TextBox ID="txtEditarOfiEstado" runat="server" Text='<%# Bind("Estado") %>' MaxLength="5"  />
                                            </EditItemTemplate> 
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="" ShowHeader="False" HeaderStyle-HorizontalAlign="Left">
                                            <EditItemTemplate> 
                                                <asp:LinkButton ID="lnkActualizar" runat="server" CausesValidation="True" CommandName="Update" Text="Actualizar"></asp:LinkButton> 
                                                <asp:LinkButton ID="lnkCancelar" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancelar"></asp:LinkButton> 
                                            </EditItemTemplate> 
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtEditarOficina" runat="server" Text="Editar" CommandName="Edit" CausesValidation="False" ></asp:LinkButton>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lbtAgregarOficina" runat="server" Text="Agregar" CommandName="Select" ></asp:LinkButton>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        
                                    </Columns>  

                                    <EditRowStyle BackColor="#999999" />

                                </asp:GridView> </ContentTemplate>
                                    </asp:UpdatePanel>
                            </ContentTemplate>

                        </ajaxToolkit:TabPanel>
                        
                        <ajaxToolkit:TabPanel runat="server" HeaderText="Direcciones" ID="tpnlDirecciones">
                            <ContentTemplate>
                                <asp:GridView ID="grdDirecciones" runat="server" AutoGenerateColumns="False"  ShowHeaderWhenEmpty="True"
                                    CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
                                    Width="100%" OnSelectedIndexChanged="grdDirecciones_SelectedIndexChanged" OnRowEditing="grdDirecciones_RowEditing"
                                    OnRowUpdating="grdDirecciones_RowUpdating" OnRowUpdated="grdDirecciones_RowUpdated" OnPageIndexChanging="grdDirecciones_PageIndexChanging"
                                    OnRowCancelingEdit="grdDirecciones_RowCancelingEdit" AllowPaging="true" PageSize="20" ShowFooter="true">
                                    <Columns>

                                        <asp:TemplateField HeaderText="Dirección:" > 
                                            <ItemTemplate>
                                                <asp:Label ID="lblIdDireccion" runat="server" Text='<%# Bind("IdDireccion") %>'></asp:Label>
                                            </ItemTemplate>            
                                            <FooterTemplate>
				                                <asp:TextBox ID="txtInsertarIdDireccion" runat="server"  MaxLength="10" />
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Nombre" > 
                                            <ItemTemplate>
                                                <asp:Label ID="lblIdNomDireccion" runat="server"  Text='<%# Bind("NomDireccion") %>'></asp:Label>
                                            </ItemTemplate>   
                                                                    
                                            <FooterTemplate>
				                                <asp:TextBox ID="txtInsertarNomDireccion" runat="server" />
                                            </FooterTemplate>

                                            <EditItemTemplate>
				                                <asp:TextBox ID="txtEditarNomDireccion" runat="server" Text='<%# Bind("NomDireccion") %>' />
                                            </EditItemTemplate> 
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Estado" >
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtInsertarEstado" runat="server"></asp:TextBox>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblEstado" runat="server"  Text='<%# Bind("Estado") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
				                                <asp:TextBox ID="txtEditarEstado" runat="server" Text='<%# Bind("Estado") %>' MaxLength="5"  />
                                            </EditItemTemplate> 
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="" ShowHeader="False" HeaderStyle-HorizontalAlign="Left">
                                            <EditItemTemplate> 
                                                <asp:LinkButton ID="lnkActualizarDir" runat="server" CausesValidation="True" CommandName="Update" Text="Actualizar"></asp:LinkButton> 
                                                <asp:LinkButton ID="lnkCancelarDir" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancelar"></asp:LinkButton> 
                                            </EditItemTemplate> 
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtEditarDireccion" runat="server" Text="Editar" CommandName="Edit" CausesValidation="False" ></asp:LinkButton>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lbtAgregarDireccion" runat="server" Text="Agregar" CommandName="Select" ></asp:LinkButton>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                    </Columns>  
                                    <EditRowStyle BackColor="#999999" />
                                </asp:GridView>
                            </ContentTemplate>
                        </ajaxToolkit:TabPanel>

                        <ajaxToolkit:TabPanel runat="server" HeaderText="Servicios" ID="tpnlServicios">
                            <ContentTemplate>
                                <asp:GridView ID="grdServicios" runat="server" AutoGenerateColumns="False"  ShowHeaderWhenEmpty="True"
                                    CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
                                    Width="100%" OnSelectedIndexChanged="grdServicios_SelectedIndexChanged" 
                                    OnRowUpdating="grdServicios_RowUpdating" OnRowUpdated="grdServicios_RowUpdated" OnPageIndexChanging="grdServicios_PageIndexChanging"
                                    OnRowCancelingEdit="grdServicios_RowCancelingEdit" AllowPaging="true" PageSize="20" ShowFooter="true">
                                    <Columns>

                                        <asp:TemplateField HeaderText="Servicio" > 
                                            <ItemTemplate>
                                                <asp:Label ID="lblIdServicio" runat="server" Text='<%# Bind("IdServicio") %>'></asp:Label>
                                            </ItemTemplate>            
                                         <%--   <FooterTemplate>
				                                <asp:TextBox ID="txtInsertarIdServicio" runat="server"  MaxLength="10" />
                                            </FooterTemplate>--%> 
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Nombre" > 
                                            <ItemTemplate>
                                                <asp:Label ID="lblIdNomServicio" runat="server"  Text='<%# Bind("NomServicio") %>'></asp:Label>
                                            </ItemTemplate>   
                                            <EditItemTemplate>
				                                <asp:TextBox ID="txtEditarNomServicio" runat="server" Text='<%# Bind("NomServicio") %>' />
                                            </EditItemTemplate> 
                                           <%--  <FooterTemplate>
				                                <asp:TextBox ID="txtInsertarNomServicio" runat="server" />
                                            </FooterTemplate>--%> 
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Monto">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMonto" runat="server" Text='<%# Bind("Monto") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
				                                <asp:TextBox ID="txtEditarMonto" runat="server" TextMode="Number" Text='<%# Bind("Monto") %>'  />
                                            </EditItemTemplate> 
                                           <%--  <FooterTemplate>
				                                <asp:TextBox ID="txtInsertarMonto" runat="server" TextMode="Number" />
                                            </FooterTemplate>--%> 
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="CC Debe Dev" >
                                           <%--  <FooterTemplate>
                                                <asp:TextBox ID="txtCtaContableDebeActualDev" runat="server"></asp:TextBox>
                                            </FooterTemplate>--%> 
                                            <ItemTemplate>
                                                <asp:Label ID="lblCtaContableDebeActualDev" runat="server" Text='<%# Bind("CtaContableDebeActualDev") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
				                                <asp:TextBox ID="txtEditarCtaContableDebeActualDev" runat="server" Text='<%# Bind("CtaContableDebeActualDev") %>'/>
                                            </EditItemTemplate> 
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="CC Haber Dev" >
                                           <%--  <FooterTemplate>
                                                <asp:TextBox ID="txtCtaContableHaberActualDev" runat="server"></asp:TextBox>
                                            </FooterTemplate>--%> 
                                            <ItemTemplate>
                                                <asp:Label ID="lblCtaContableHaberActualDev" runat="server" Text='<%# Bind("CtaContableHaberActualDev") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
				                                <asp:TextBox ID="txtEditarCtaContableHaberActualDev" runat="server" Text='<%# Bind("CtaContableHaberActualDev") %>' />
                                            </EditItemTemplate> 
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="PosPre Dev" >
                                          <%--   <FooterTemplate>
                                                <asp:TextBox ID="txtIdPosPreActualDev" runat="server"></asp:TextBox>
                                            </FooterTemplate>--%> 
                                            <ItemTemplate>
                                                <asp:Label ID="lblIdPosPreActualDev" runat="server" Text='<%# Bind("IdPosPreActualDev") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
				                                <asp:TextBox ID="txtEditarIdPosPreActualDev" runat="server" Text='<%# Bind("IdPosPreActualDev") %>'  />
                                            </EditItemTemplate> 
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="CC Debe Per" >
                                          <%--   <FooterTemplate>
                                                <asp:TextBox ID="txtCtaContableDebeActualPer" runat="server"></asp:TextBox>
                                            </FooterTemplate>--%> 
                                            <ItemTemplate>
                                                <asp:Label ID="lblCtaContableDebeActualPer" runat="server" Text='<%# Bind("CtaContableDebeActualPer") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
				                                <asp:TextBox ID="txtEditarCtaContableDebeActualPer" runat="server" Text='<%# Bind("CtaContableDebeActualPer") %>' />
                                            </EditItemTemplate> 
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="CC Haber Per" >
                                          <%--   <FooterTemplate>
                                                <asp:TextBox ID="txtCtaContableHaberActualPer" runat="server"></asp:TextBox>
                                            </FooterTemplate>--%> 
                                            <ItemTemplate>
                                                <asp:Label ID="lblCtaContableHaberActualPer" runat="server" Text='<%# Bind("CtaContableHaberActualPer") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
				                                <asp:TextBox ID="txtEditarCtaContableHaberActualPer" runat="server" Text='<%# Bind("CtaContableHaberActualPer") %>'  />
                                            </EditItemTemplate> 
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="PosPre Per" >
                                          <%--   <FooterTemplate>
                                                <asp:TextBox ID="txtIdPosPreActualPer" runat="server"></asp:TextBox>
                                            </FooterTemplate>--%> 
                                            <ItemTemplate>
                                                <asp:Label ID="lblIdPosPreActualPer" runat="server" Text='<%# Bind("IdPosPreActualPer") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
				                                <asp:TextBox ID="txtEditarIdPosPreActualPer" runat="server" Text='<%# Bind("IdPosPreActualPer") %>' />
                                            </EditItemTemplate> 
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="CC Debe Dev" >
                                       <%--      <FooterTemplate>
                                                <asp:TextBox ID="txtCtaContableDebeVencidoDev1" runat="server"></asp:TextBox>
                                            </FooterTemplate>--%> 
                                            <ItemTemplate>
                                                <asp:Label ID="lblCtaContableDebeVencidoDev" runat="server" Text='<%# Bind("CtaContableDebeVencidoDev") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
				                                <asp:TextBox ID="txtEditarCtaContableDebeVencidoDev" runat="server" Text='<%# Bind("CtaContableDebeVencidoDev") %>' />
                                            </EditItemTemplate> 
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="CC Haber Dev" >
                                          <%--   <FooterTemplate>
                                                <asp:TextBox ID="txtCtaContableHaberVencidoDev1" runat="server"></asp:TextBox>
                                            </FooterTemplate>--%> 
                                            <ItemTemplate>
                                                <asp:Label ID="lblCtaContableHaberVencidolDev" runat="server" Text='<%# Bind("CtaContableHaberVencidoDev") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
				                                <asp:TextBox ID="txtEditarCtaContableHaberVencidoDev" runat="server"  Text='<%# Bind("CtaContableHaberVencidoDev") %>'  />
                                            </EditItemTemplate> 
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="PosPre Dev" >
                                          <%--   <FooterTemplate>
                                                <asp:TextBox ID="txtIdPosPreVencidoDev1" runat="server"></asp:TextBox>
                                            </FooterTemplate>--%> 
                                            <ItemTemplate>
                                                <asp:Label ID="lblIdPosPreVencidoDev" runat="server" Text='<%# Bind("IdPosPreVencidoDev") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
				                                <asp:TextBox ID="txtEditarIdPosPreVencidoDev" runat="server" Text='<%# Bind("IdPosPreVencidoDev") %>'  />
                                            </EditItemTemplate> 
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="CC Debe Dev" >
                                        <%--     <FooterTemplate>
                                                <asp:TextBox ID="txtCtaContableDebeVencidoDev2" runat="server"></asp:TextBox>
                                            </FooterTemplate>--%> 
                                            <ItemTemplate>
                                                <asp:Label ID="lblCtaContableDebeVencidoDev" runat="server" Text='<%# Bind("CtaContableDebeVencidoDev") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
				                                <asp:TextBox ID="txtEditarCtaContableDebeVencidoDev" runat="server" Text='<%# Bind("CtaContableDebeVencidoDev") %>'  />
                                            </EditItemTemplate> 
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="CC Haber Dev" >
                                        <%--     <FooterTemplate>
                                                <asp:TextBox ID="txtCtaContableHaberVencidoDev2" runat="server"></asp:TextBox>
                                            </FooterTemplate>--%> 
                                            <ItemTemplate>
                                                <asp:Label ID="lblCtaContableHaberVencidoDev" runat="server" Text='<%# Bind("CtaContableHaberVencidoDev") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
				                                <asp:TextBox ID="txtEditarCtaContableHaberVencidoDev" runat="server" Text='<%# Bind("NomServicio") %>'  />
                                            </EditItemTemplate> 
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="PosPre Dev" >
                                        <%--     <FooterTemplate>
                                                <asp:TextBox ID="txtIdPosPreVencidoDev2" runat="server"></asp:TextBox>
                                            </FooterTemplate>--%> 
                                            <ItemTemplate>
                                                <asp:Label ID="lblIdPosPreVencidoDev" runat="server" Text='<%# Bind("IdPosPreVencidoDev") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
				                                <asp:TextBox ID="txtEditarIdPosPreVencidoDev" runat="server" Text='<%# Bind("IdPosPreVencidoDev") %>'  />
                                            </EditItemTemplate> 
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Estado" >
                                         <%--    <FooterTemplate>
                                                <asp:TextBox ID="txtInsertarEstado" runat="server"></asp:TextBox>
                                            </FooterTemplate>--%> 
                                            <ItemTemplate>
                                                <asp:Label ID="lblEstado" runat="server"  Text='<%# Bind("Estado") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
				                                <asp:TextBox ID="txtEditarEstado" runat="server" Text='<%# Bind("Estado") %>'   />
                                            </EditItemTemplate> 
                                        </asp:TemplateField>
 <%--
                                        <asp:TemplateField HeaderText="" ShowHeader="False" HeaderStyle-HorizontalAlign="Left">
                                            <EditItemTemplate> 
                                                <asp:LinkButton ID="lnkActualizarDir" runat="server" CausesValidation="True" CommandName="Update" Text="Actualizar"></asp:LinkButton> 
                                                <asp:LinkButton ID="lnkCancelarDir" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancelar"></asp:LinkButton> 
                                            </EditItemTemplate> 
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtEditarDireccion" runat="server" Text="Editar" CommandName="Edit" CausesValidation="False" ></asp:LinkButton>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lbtAgregarDireccion" runat="server" Text="Agregar" CommandName="Select" ></asp:LinkButton>
                                            </FooterTemplate> 
                                        </asp:TemplateField>--%>

                                    </Columns>  
                                    <EditRowStyle BackColor="#999999" />
                                </asp:GridView>
                            </ContentTemplate>
                        </ajaxToolkit:TabPanel>

                    </ajaxToolkit:TabContainer>
    </div>
</asp:Content>
