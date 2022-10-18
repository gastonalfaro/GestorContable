<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmAreasFuncionales.aspx.cs" Inherits="Presentacion.Mantenimiento.frmAreasFuncionales" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoJS" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Contenido" runat="server">
        <div class="col-md-12" id="tblAreasFuncionales">
            <h3>ÁREAS FUNCIONALES</h3>
           <p>Consulta de Áreas Funcionales del Sistema Gestor.</p> 
            <div class="col-md-6">
                <div class="col-md-3"><asp:Label ID="Label1" runat="server" Text="Código:" Font-Bold="true" ></asp:Label></div>
                <div class="col-md-5"><asp:TextBox ID="txtBusqIdAreaFuncional" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
            </div>
            <div class="col-md-6">
                <div class="col-md-3"><asp:Label ID="Label2" runat="server" Text="Descripción:" Font-Bold="true"></asp:Label></div>
                <div class="col-md-5"><asp:TextBox ID="txtBusqNomAreaFuncional" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
            </div>
            <div class="col-md-12" style="text-align:center;"> 
                <asp:Button ID="btnAreaFuncionalConsultar" runat="server" Text="CONSULTAR" OnClick="btnAreaFuncionalConsultar_Click" CssClass="ButtonNeutro"/>
            </div>
            <div class="col-md-12" style="text-align:center;"> 
                <asp:label ID="lblMensaje" runat="server" Visible="false" Font-Bold="true" ></asp:label>
            </div>
        </div>
    <div style="width: 100%; height: 100%; overflow: auto">
        <asp:GridView ID="grdAreasFuncionales" runat="server" AutoGenerateColumns="False"  ShowHeaderWhenEmpty="True"
             CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
            Width="100%" OnSelectedIndexChanged="grdAreasFuncionales_SelectedIndexChanged" OnRowEditing="grdAreasFuncionales_RowEditing"
            OnRowUpdating="grdAreasFuncionales_RowUpdating" OnPageIndexChanging="grdAreasFuncionales_PageIndexChanging"
            OnRowCancelingEdit="grdAreasFuncionales_RowCancelingEdit" PageSize="20" AllowPaging="True">
                        <Columns>

                            <asp:TemplateField HeaderText="Código"  > 
                                <ItemTemplate>
                                    <asp:Label ID="lblIdAreaFuncional" runat="server" Text='<%# Bind("IdAreaFuncional") %>'></asp:Label>
                                </ItemTemplate>                           
                                <FooterTemplate>
				                    <asp:TextBox ID="txtInsertIdAreaFuncional" runat="server"  Text='<%# Bind("IdAreaFuncional") %>' MaxLength="4" />
                                </FooterTemplate>
                        
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Descripción" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtNomNuevoAreaFuncional" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblNombre" runat="server" Text='<%# Bind("NomAreaFuncional") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditNombre" runat="server" Width="90%" Text='<%# Bind("NomAreaFuncional") %>' MaxLength="100"  />
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

                        </Columns>  

                        <EditRowStyle BackColor="#999999" />

                    </asp:GridView>
    </div>
</asp:Content>
