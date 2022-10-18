<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmMonedas.aspx.cs" Inherits="Presentacion.Mantenimiento.frmMonedas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenido" runat="server">
    <div class="FormatoBotones">
        <asp:Button ID="btnMonedasNuevo" runat="server" Text="NUEVO" OnClick="btnMonedasNuevo_Click" Visible="false" CssClass="ButtonNeutro" />
        <asp:Button ID="btnMonedasGuardar" runat="server" Text="GUARDAR" OnClick="btnMonedasGuardar_Click" Visible="false"  CssClass="ButtonNeutro"/>
        <asp:Button ID="btnMonedasVolver" runat="server" Text="VOLVER" OnClick="btnMonedasVolver_Click" Visible="false"  CssClass="ButtonNeutro"/>        
    </div> 
    <div class="col-md-12" id="tblmonedas">
        <h3>MONEDAS</h3>
        <p>Consulta de Monedas del Sistema Gestor.</p>
            <div class="col-md-6">
                <div class="col-md-3"><asp:Label ID="Label1" runat="server" Text="Código:" Font-Bold="true"></asp:Label></div>
                <div class="col-md-5"><asp:TextBox ID="txtBusqIdMonedas" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
            </div>
            <div class="col-md-6">
                <div class="col-md-3"><asp:Label ID="Label2" runat="server" Text="Descripción:" Font-Bold="true"></asp:Label></div>
                <div class="col-md-5"><asp:TextBox ID="txtBusqNomMonedas" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
            </div>
            <div class="col-md-12" style="text-align:center;"><asp:Button ID="btnMonedasConsultar" runat="server" Text="CONSULTAR" OnClick="btnMonedasConsultar_Click" CssClass="ButtonNeutro"/></div>
            <div class="col-md-12"> <asp:label ID="lblMensaje" runat="server" Visible="false" Font-Bold="true" ></asp:label></div>    
    </div>
    <div style="width: 100%; height: 100%; overflow: auto">
        <asp:GridView ID="grdvMonedas" runat="server" AutoGenerateColumns="False"  ShowHeaderWhenEmpty="True"
             CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
            Width="100%" OnSelectedIndexChanged="grdvMonedas_SelectedIndexChanged" OnRowEditing="grdvMonedas_RowEditing"
            OnRowUpdating="grdvMonedas_RowUpdating" OnPageIndexChanging="grdvMonedas_PageIndexChanging"
            OnRowCancelingEdit="grdvMonedas_RowCancelingEdit" PageSize="20" AllowPaging="True">
                        <Columns>

                            <asp:TemplateField HeaderText="Código" ItemStyle-HorizontalAlign="Center"> 
                                <ItemTemplate>
                                    <asp:Label ID="lblIdMoneda" runat="server" Text='<%# Bind("IdMoneda") %>'></asp:Label>
                                </ItemTemplate>                           
                                <FooterTemplate>
				                    <asp:TextBox ID="txtInsertIdMoneda" runat="server"  Text='<%# Bind("IdMoneda") %>' MaxLength="4" />
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Descripción" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtNomNuevoMoneda" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblNomMoneda" runat="server" Text='<%# Bind("NomMoneda") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditNomMoneda" runat="server" Width="90%" Text='<%# Bind("NomMoneda") %>' MaxLength="100"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Estado" ItemStyle-HorizontalAlign="Center">
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
