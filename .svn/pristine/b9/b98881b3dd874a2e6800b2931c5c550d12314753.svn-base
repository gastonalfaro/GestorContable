<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmTiposCambio.aspx.cs" Inherits="Presentacion.Mantenimiento.frmTiposCambio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenido" runat="server">
    <div class="FormatoBotones">
        <%--<asp:Button ID="btnTiposCambioNuevo" runat="server" Text="NUEVO" OnClick="btnTiposCambioNuevo_Click" />
        <asp:Button ID="btnTiposCambioGuardar" runat="server" Text="GUARDAR" OnClick="btnTiposCambioGuardar_Click" Visible="false" />--%>
        <asp:Button ID="btnTiposCambioVolver" runat="server" Text="VOLVER" OnClick="btnTiposCambioVolver_Click" Visible="false" CssClass="ButtonNeutro" />
              
    </div> 
    <div class="col-md-12" id="tblTiposCambio">
        <h3>TIPOS DE CAMBIO MONETARIO</h3>
        <p>Consulta de Tipos de Cambio del Sistema Gestor.</p>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="Label1" runat="server" Text="Moneda:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"> <asp:TextBox ID="txtBusqMoneda" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="Label2" runat="server"  Text="Fecha:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"><asp:TextBox ID="txtBusqFchReferencia" runat="server" CssClass="js-date-picker FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="Label3" runat="server" Text="Transacción:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"><asp:TextBox ID="txtBusqTransaccion" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-12" style="text-align:center;"><asp:Button ID="btnTiposCambioConsultar" runat="server" Text="CONSULTAR" OnClick="btnTiposCambioConsultar_Click" CssClass="ButtonNeutro" />
            &nbsp<asp:Button ID="btnTiposCambioActualizar" runat="server" Text="Actualizar" OnClick="btnTiposCambioActualizar_Click" CssClass="ButtonNeutro" />
        </div>
    </div>
    <div class="col-md-12" ><asp:label ID="lblMensaje" runat="server" Visible="false" Font-Bold="true" ></asp:label></div>
    <div style="width: 100%; height: 100%; overflow: auto">
         <asp:GridView ID="grdvTiposCambio" runat="server" AutoGenerateColumns="False"  ShowHeaderWhenEmpty="True"
            CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
            Width="100%" OnSelectedIndexChanged="grdvTiposCambio_SelectedIndexChanged" OnRowEditing="grdvTiposCambio_RowEditing"
            OnRowUpdating="grdvTiposCambio_RowUpdating" OnRowUpdated="grdvTiposCambio_RowUpdated" OnPageIndexChanging="grdvTiposCambio_PageIndexChanging"
            OnRowCancelingEdit="grdvTiposCambio_RowCancelingEdit" PageSize="20" AllowPaging="True">
                        <Columns>

                            <asp:TemplateField HeaderText="Moneda" ItemStyle-HorizontalAlign="Center"> 
                                <ItemTemplate>
                                    <asp:Label ID="lblMoneda" runat="server"   TextMode="Date"    Text='<%# Bind("IdMoneda") %>'></asp:Label>
                                </ItemTemplate>                           
                                <FooterTemplate>
				                    <asp:TextBox ID="txtInsertMoneda" runat="server" TextMode="Date" Text='<%# Bind("IdMoneda") %>' MaxLength="2" />
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Fecha Referencia" ItemStyle-HorizontalAlign="Center"> 
                                <ItemTemplate>
                                    <asp:Label ID="lblIdMoneda" runat="server"  Text='<%# Bind("FchReferencia") %>'></asp:Label>
                                </ItemTemplate>                           
                                <FooterTemplate>
				                    <asp:TextBox ID="txtInsertIdMoneda" runat="server"  Text='<%# Bind("FchReferencia") %>' MaxLength="18" />
                                </FooterTemplate>
                        
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Tipo Transacción" > 
                                <ItemTemplate>
                                    <asp:Label ID="lblTipoTransaccion" runat="server" Text='<%# Bind("TipoTransaccion") %>'></asp:Label>
                                </ItemTemplate>                           
                                <FooterTemplate>
				                    <asp:TextBox ID="txtInsertTipoTransaccion" runat="server"  Text='<%# Bind("TipoTransaccion") %>' MaxLength="50" />
                                </FooterTemplate>
                        
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Valor" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtValor" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblValor" runat="server"   TextMode="Number"  Text='<%# Bind("Valor") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditValor" runat="server" Width="50%"   TextMode="Number"  Text='<%# Bind("Valor") %>' MaxLength="100"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                        </Columns>  

                        <EditRowStyle BackColor="#999999" />

                    </asp:GridView>
    </div>
</asp:Content>


