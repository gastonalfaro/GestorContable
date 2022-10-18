<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmElementosPEP.aspx.cs" Inherits="Presentacion.Mantenimiento.frmElementosPEP" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoJS" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Contenido" runat="server">
    <div class="FormatoBotones">
         <%--<asp:Button ID="btnNuevosElementosPEP" runat="server" Text="NUEVO" OnClick="btnNuevosElementosPEP_Click" />
            <asp:Button ID="btnGuardarElementosPEP" runat="server" Text="GUARDAR" OnClick="btnGuardarElementosPEP_Click" Visible="false" />
            <asp:Button ID="btnVolverElementosPEP" runat="server" Text="VOLVER" OnClick="btnVolverElementosPEP_Click" Visible="false" />
        --%>
    </div> 
    <div class="col-md-12" id="tblElementosPEP">
        <h3>ElementosPEP</h3>
        <p>Consultas de ElementosPEP del Sistema Gestor.</p>
            <div class="col-md-6">
                <div class="col-md-3"> <asp:Label ID="lblBusquedaElementoPEP" runat="server" Text="Código:" Font-Bold="true"></asp:Label></div>
                <div class="col-md-5"><asp:TextBox ID="txtBusquedaIdElementoPEP" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
            </div>
            <div class="col-md-6">
                <div class="col-md-3"><asp:Label ID="lblBusquedaNomElementoPEP" runat="server" Text="Descripción:" Font-Bold="true"></asp:Label></div>
                <div class="col-md-5"><asp:TextBox ID="txtBusquedaNomElementoPEP" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
            </div>
            <div class="col-md-12" style="text-align:center;"><asp:Button ID="btnConsultarElementoPEP" runat="server" Text="CONSULTAR" OnClick="btnConsultarElementoPEP_Click" CssClass="ButtonNeutro" /></div>
        <div class="col-md-12" style="text-align:center;"><asp:label ID="lblMensaje" runat="server" Visible="false" Font-Bold="true" ></asp:label></div>    
    </div>
         <div style="width: 100%; height: 100%; overflow: auto">
   
                    <asp:GridView ID="grdElementosPEP" runat="server" AutoGenerateColumns="False"  ShowHeaderWhenEmpty="True"
                         CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
                        Width="100%" OnSelectedIndexChanged="grdElementosPEP_SelectedIndexChanged" OnRowEditing="grdElementosPEP_RowEditing"
                        OnRowUpdating="grdElementosPEP_RowUpdating"  OnPageIndexChanging="grdElementosPEP_PageIndexChanging"
                        OnRowCancelingEdit="grdElementosPEP_RowCancelingEdit" AllowPaging="true" PageSize="20">
                                    <Columns>

                                        <asp:TemplateField HeaderText="Código" > 
                                            <ItemTemplate>
                                                <asp:Label ID="lblIdElementoPEP" runat="server" Text='<%# Bind("IdElementoPEP") %>'></asp:Label>
                                            </ItemTemplate>                           
                                            <FooterTemplate>
				                                <asp:TextBox ID="txtInsertarIdElementoPEP" runat="server" Text='<%# Bind("IdElementoPEP") %>' MaxLength="2" />
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Descripción" > 
                                            <ItemTemplate>
                                                <asp:Label ID="lblIdNomElementoPEP" runat="server"  Text='<%# Bind("NomElementoPEP") %>'></asp:Label>
                                            </ItemTemplate>                           
                                            <FooterTemplate>
				                                <asp:TextBox ID="txtInsertarNomElementoPEP" runat="server"  Text='<%# Bind("NomElementoPEP") %>' MaxLength="18" />
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Estado" >
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtEstado" runat="server"></asp:TextBox>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblEstado" runat="server"   TextMode="Number"  Text='<%# Bind("Estado") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
				                                <asp:TextBox ID="txtEditarEstado" runat="server" Width="50%"   TextMode="Number"  Text='<%# Bind("Estado") %>' MaxLength="100"  />
                                            </EditItemTemplate> 
                                        </asp:TemplateField>

                                    </Columns>  

                                    <EditRowStyle BackColor="#999999" />

                </asp:GridView>

    </div>
</asp:Content>
