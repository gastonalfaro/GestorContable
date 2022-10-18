<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmProgramas.aspx.cs" Inherits="Presentacion.Mantenimiento.frmProgramas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenido" runat="server">
    <div class="FormatoBotones"></div> 
    <div class="col-md-12" id="tblParametros">
        <h3>Programas</h3>
        <p>Mantenimiento de Catálogos y Parámetros del Sistema Gestor.</p>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="lblIdPrograma" runat="server" Text="Código:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"><asp:TextBox ID="txtBusqIdPrograma" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="lblNomPrograma" runat="server" Text="Descripción:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"><asp:TextBox ID="txtBusqNomPrograma" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="lblEntidadCP" runat="server" Text="Entidad Control Presupuestario:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"><asp:TextBox ID="txtIdEntidadCP" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-12" style="text-align:center;"><asp:Button ID="btnConsultarPrograma" runat="server" Text="CONSULTAR" OnClick="btnConsultarPrograma_Click" CssClass="ButtonNeutro"/></div>
    </div>
    <div class="col-md-12" ><asp:label ID="lblMensaje" runat="server" Visible="false" Font-Bold="true" ></asp:label></div>
    <div style="width: 100%; height: 100%; overflow: auto">
       
        <asp:GridView ID="grdProgramas" runat="server" AutoGenerateColumns="False"  ShowHeaderWhenEmpty="True"
            CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
            Width="100%" OnSelectedIndexChanged="grdProgramas_SelectedIndexChanged" OnRowEditing="grdProgramas_RowEditing"
            OnRowUpdating="grdProgramas_RowUpdating" OnPageIndexChanging="grdProgramas_PageIndexChanging"
            OnRowCancelingEdit="grdProgramas_RowCancelingEdit" AllowPaging="true" PageSize="20">
                        <Columns>

                            <asp:TemplateField HeaderText="Código"  > 
                                <ItemTemplate>
                                    <asp:Label ID="lblIdPrograma" runat="server"  Text='<%# Bind("IdPrograma") %>'></asp:Label>
                                </ItemTemplate>                           
                                <FooterTemplate>
				                    <asp:TextBox ID="txtInsertIdPrograma" runat="server"  Text='<%# Bind("IdPrograma") %>' MaxLength="10" />
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Descripción" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtNomPrograma" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblNomPrograma" runat="server" Text='<%# Bind("NomPrograma") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditNomPrograma" runat="server" Width="50%" Text='<%# Bind("NomPrograma") %>' MaxLength="200"  />
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
