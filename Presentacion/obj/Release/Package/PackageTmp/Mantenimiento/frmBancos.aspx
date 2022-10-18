<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmBancos.aspx.cs" Inherits="Presentacion.Mantenimiento.frmBancos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoJS" runat="server">
     <style type="text/css">
        .FormatoTextBox, .FormatoDropDownList { width:100%!important; }
      </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Contenido" runat="server">
    <div class="col-md-12">
        <h3>Bancos</h3>
        Mantenimiento de Bancos del Sistema Gestor.
    </div>
    <div class="col-md-12">
        <asp:Button ID="btnBancoNuevo" runat="server" Text="NUEVO" OnClick="btnBancoNuevo_Click"  Visible="false"  CssClass="ButtonNeutro"/>
        <asp:Button ID="btnBancoGuardar" runat="server" Text="GUARDAR" OnClick="btnBancoGuardar_Click" Visible="false" CssClass="ButtonNeutro" />
        <asp:Button ID="btnBancoVolver" runat="server" Text="VOLVER" OnClick="btnBancoVolver_Click" Visible="false" CssClass="ButtonNeutro"/>
    </div>
    <div  class="col-md-6">
        <div class="row">
            <div class="col-md-5"><asp:Label ID="Label1" runat="server" Text="Código:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7"><asp:TextBox ID="txtBusqIdBanco" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="row">
            <div class="col-md-5"><asp:Label ID="Label2" runat="server" Text="Descripción:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7"> <asp:TextBox ID="txtBusqNomBanco" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
    </div>
     <div class="col-md-12" style="text-align:center;">
            <asp:Button ID="btnBancosConsultar" runat="server" Text="CONSULTAR" OnClick="btnBancosConsultar_Click" CssClass="ButtonNeutro"/>
     </div>
    <div class="col-md-12" >
        <asp:label ID="lblMensaje" runat="server" Visible="false" Font-Bold="true" ></asp:label>
    </div>
    <div style="width: 100%; height: 100%; overflow: auto">

        <asp:GridView ID="grdvBancos" runat="server" AutoGenerateColumns="False"  ShowHeaderWhenEmpty="True"
             CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
            Width="100%" OnSelectedIndexChanged="grdvBancos_SelectedIndexChanged" OnRowEditing="grdvBancos_RowEditing"
            OnPageIndexChanging="grdvBancos_PageIndexChanging" OnRowDataBound="grdvBancos_RowDataBound"
            OnRowUpdating="grdvBancos_RowUpdating" OnRowUpdated="grdvBancos_RowUpdated"
            OnRowCancelingEdit="grdvBancos_RowCancelingEdit" AllowPaging="true" PageSize="20">
                        <Columns>

                            <asp:TemplateField HeaderText="Código" > 
                                <ItemTemplate>
                                    <asp:Label ID="lblIdBanco" runat="server" Text='<%# Bind("IdBanco") %>'></asp:Label>
                                </ItemTemplate>                           
                                <FooterTemplate>
				                    <asp:TextBox ID="txtInsertIdBanco" runat="server"  Text='<%# Bind("IdBanco") %>' MaxLength="4" />
                                </FooterTemplate>
                        
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Descripción" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtNomNuevoBanco" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblNomBanco" runat="server" Text='<%# Bind("NomBanco") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditNomBanco" runat="server" Width="90%" Text='<%# Bind("NomBanco") %>' MaxLength="100"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="ID Banco Propio" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtIdBancoPropio" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblIdBancoPropio" runat="server" Text='<%# Bind("IdBancoPropio") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditIdBancoPropio" runat="server" Width="90%" Text='<%# Bind("IdBancoPropio") %>' MaxLength="100"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="ID Sociedad Fi" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtIdSociedadFi" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblIdSociedadFi" runat="server" Text='<%# Bind("IdSociedadFi") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditIdSociedadFi" runat="server" Width="90%" Text='<%# Bind("IdSociedadFi") %>' MaxLength="100"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="País" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtPais" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblIdPais" runat="server" Text='<%# Bind("IdPais") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditIdPais" runat="server" Width="90%" Text='<%# Bind("IdPais") %>' MaxLength="100"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Telefonos" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtNuevoTelefonos" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblTelefonos" runat="server" Text='<%# Bind("Telefono") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditTelefonos" runat="server" Width="90%" Text='<%# Bind("Telefono") %>' MaxLength="100"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Contacto" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtNuevoContacto" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblContacto" runat="server" Text='<%# Bind("Contacto") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditContacto" runat="server" Width="90%" Text='<%# Bind("Contacto") %>' MaxLength="100"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>
                           
                            <asp:TemplateField HeaderText="Estado" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtNuevoEstado" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblEstado" runat="server" Text='<%# Bind("Estado") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditEstado" runat="server" Width="90%" Text='<%# Bind("Estado") %>' MaxLength="100"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="" ShowHeader="False" HeaderStyle-HorizontalAlign="Left">
                                <EditItemTemplate> 
                                    <asp:LinkButton ID="lnkActualizarParametro" runat="server" CausesValidation="True" CommandName="Update" Text="Actualizar"></asp:LinkButton> 
                                    <asp:LinkButton ID="lnkCancelarParametro" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancelar"></asp:LinkButton> 
                                </EditItemTemplate> 
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtEditarParametro" runat="server" CausesValidation="False" Text="Mostrar detalle" CommandName="Edit" ></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>  

            <EditRowStyle BackColor="#999999" />

        </asp:GridView>

    </div>
</asp:Content>
