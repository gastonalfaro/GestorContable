<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmPosicionesPresupuestarias.aspx.cs" Inherits="Presentacion.Mantenimiento.frmPosicionesPresupuestarias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenido" runat="server">
    <div class="FormatoBotones"></div> 
    <div class="col-md-12">
         <h3>Posiciones Presupuestarias</h3>
        <p>Consulta de Posiciones Presupuestarias del Sistema Gestor.</p>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="lblIdPosPre" runat="server" Text="Código:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"><asp:TextBox ID="txtBusqIdPosPre" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="lblNomPosPre" runat="server" Text="Descripción:" Font-Bold="true" ></asp:Label></div>
            <div class="col-md-5"><asp:TextBox ID="txtBusqNomPosPre" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="Label1" runat="server" Text="Ejercicio:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"><asp:TextBox ID="txtEjercicio" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="lblEntidadCP" runat="server" Text="Entidad Control Presupuestario:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"><asp:TextBox ID="txtIdEntidadCP" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-12" style="text-align:center;"><asp:Button ID="btnParametroConsultar" runat="server" Text="CONSULTAR" OnClick="btnParametroConsultar_Click" CssClass="ButtonNeutro" /></div>
    </div>
    <div class="col-md-12" > <asp:label ID="lblMensaje" runat="server" Visible="false" Font-Bold="true" ></asp:label></div>
    <div style="width: 100%; height: 100%; overflow: auto">
                  
        <asp:GridView ID="gvpPosicionesPresupuestarias" runat="server" AutoGenerateColumns="False"  ShowHeaderWhenEmpty="True"
            CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
            Width="100%" OnSelectedIndexChanged="gvpPosicionesPresupuestarias_SelectedIndexChanged" OnRowEditing="gvpPosicionesPresupuestarias_RowEditing"
            OnRowUpdating="gvpPosicionesPresupuestarias_RowUpdating" OnPageIndexChanging="gvpPosicionesPresupuestarias_PageIndexChanging"
            OnRowCancelingEdit="gvpPosicionesPresupuestarias_RowCancelingEdit" AllowPaging="true" PageSize="20">
                        <Columns>

                            <asp:TemplateField HeaderText="Código"   > 
                                <ItemTemplate>
                                    <asp:Label ID="lblIdPosPre" runat="server"  Text='<%# Bind("IdPosPre") %>'></asp:Label>
                                </ItemTemplate>                           
                                <FooterTemplate>
				                    <asp:TextBox ID="txtInsertIdPosPre" runat="server"  Text='<%# Bind("IdPosPre") %>' MaxLength="10" />
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Descripción" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtNomPosPre" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblNomPosPre" runat="server" Text='<%# Bind("NomPosPre") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditNomPosPre" runat="server" Width="50%" Text='<%# Bind("NomPosPre") %>' MaxLength="200"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Entidad C.P." > 
                                <ItemTemplate>
                                    <asp:Label ID="lblIdEntidadCP" runat="server"  Text='<%# Bind("IdEntidadCP") %>'></asp:Label>
                                </ItemTemplate>                           
                                <FooterTemplate>
				                    <asp:TextBox ID="txtInsertIdEntidadCP" runat="server"  Text='<%# Bind("IdEntidadCP") %>' MaxLength="10" />
                                </FooterTemplate>
                        
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Ejercicio" > 
                                <ItemTemplate>
                                    <asp:Label ID="lblEjercicio" runat="server"  Text='<%# Bind("IdEjercicio") %>'></asp:Label>
                                </ItemTemplate>                           
                                <FooterTemplate>
				                    <asp:TextBox ID="txtInsertEjercicio" runat="server"  Text='<%# Bind("IdEjercicio") %>' MaxLength="10" />
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Estado" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtDenominacion" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblDenominacion" runat="server" Text='<%# Bind("Estado") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditDenominacion" runat="server" Width="50%" Text='<%# Bind("Estado") %>' MaxLength="200"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>


                        </Columns>  
            <EditRowStyle BackColor="#999999" />

        </asp:GridView>
    </div>
</asp:Content>
