<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmUnidadesConsolidacion.aspx.cs" Inherits="Presentacion.Mantenimiento.frmUnidadesConsolidacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenido" runat="server">
    <div class="FormatoBotones">
        <%--<asp:Button ID="btnUnidadesConsolidacionNuevo" runat="server" Text="NUEVO" OnClick="btnUnidadesConsolidacionNuevo_Click" />
        <asp:Button ID="btnUnidadesConsolidacionGuardar" runat="server" Text="GUARDAR" OnClick="btnUnidadesConsolidacionGuardar_Click" Visible="false" />--%>
        <asp:Button ID="btnUnidadesConsolidacionVolver" runat="server" Text="VOLVER" OnClick="btnUnidadesConsolidacionVolver_Click" Visible="false" CssClass="ButtonNeutro" />       
    </div> 
    <div class="col-md-12" id="tblUnidadesConsolidacion">
        <h3>Unidades de Consolidación</h3>
        <p>Mantenimiento de Catálogos del Sistema Gestor.</p>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="Label1" runat="server" Text="Vista:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"><asp:TextBox ID="txtBusqVista" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="Label2" runat="server" Text="Unidad:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"><asp:TextBox ID="txtBusqIdUnidad" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-12" style="text-align:center;"><asp:Button ID="btnUnidadesConsolidacionConsultar" runat="server" Text="CONSULTAR" OnClick="btnUnidadesConsolidacionConsultar_Click" CssClass="ButtonNeutro" /></div>
    </div>
    <div class="col-md-12" ><asp:label ID="lblMensaje" runat="server" Visible="false" Font-Bold="true" ></asp:label></div>
    <div style="width: 100%; height: 100%; overflow: auto">
       
        <asp:GridView ID="gvpUnidadesConsolidacion" runat="server" AutoGenerateColumns="False"  ShowHeaderWhenEmpty="True"
            CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
            Width="100%" OnSelectedIndexChanged="gvpUnidadesConsolidacion_SelectedIndexChanged" OnRowEditing="gvpUnidadesConsolidacion_RowEditing"
            OnRowUpdating="gvpUnidadesConsolidacion_RowUpdating" OnRowUpdated="gvpUnidadesConsolidacion_RowUpdated" OnPageIndexChanging="gvpUnidadesConsolidacion_PageIndexChanging"
            OnRowCancelingEdit="gvpUnidadesConsolidacion_RowCancelingEdit" AllowPaging="true" PageSize="20">
                        <Columns>

                            <asp:CommandField ShowEditButton="False" />

                            <asp:TemplateField HeaderText="Vista" > 
                                <ItemTemplate>
                                    <asp:Label ID="lblVista" runat="server"   Text='<%# Bind("Vista") %>'></asp:Label>
                                </ItemTemplate>                           
                                <FooterTemplate>
				                    <asp:TextBox ID="txtInsertVista" runat="server"    Text='<%# Bind("Vista") %>' MaxLength="2" />
                                </FooterTemplate>
                        
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Código" > 
                                <ItemTemplate>
                                    <asp:Label ID="lblIdUnidadConsolidacion" runat="server"  Text='<%# Bind("IdUnidadConsolidacion") %>'></asp:Label>
                                </ItemTemplate>                           
                                <FooterTemplate>
				                    <asp:TextBox ID="txtInsertIdUnidadConsolidacion" runat="server"  Text='<%# Bind("IdUnidadConsolidacion") %>' MaxLength="18" />
                                </FooterTemplate>
                        
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Denominación" > 
                                <ItemTemplate>
                                    <asp:Label ID="lblNomCorto" runat="server" Text='<%# Bind("NomCorto") %>'></asp:Label>
                                </ItemTemplate>                           
                                <FooterTemplate>
				                    <asp:TextBox ID="txtInsertNomCorto" runat="server"  Text='<%# Bind("NomCorto") %>' MaxLength="50" />
                                </FooterTemplate>
                        
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Descripción" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtNomUnidad" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblNomUnidad" runat="server" Text='<%# Bind("NomUnidad") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditNomUnidad" runat="server" Width="50%" Text='<%# Bind("NomUnidad") %>' MaxLength="100"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                        </Columns>  
            <EditRowStyle BackColor="#999999" />
                    </asp:GridView>
    </div>
</asp:Content>

