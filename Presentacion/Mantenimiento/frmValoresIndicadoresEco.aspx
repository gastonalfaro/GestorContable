<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmValoresIndicadoresEco.aspx.cs" Inherits="Presentacion.Mantenimiento.frmValoresIndicadoresEco" %>


<asp:Content ID="Content1" ContentPlaceHolderID="encabezado" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContenidoJS" runat="server"></asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenido" runat="server">
    <div class="FormatoBotones">
         <%--<asp:Button ID="btnIndicadoresNuevo" runat="server" Text="NUEVO" OnClick="btnIndicadoresNuevo_Click" />
            <asp:Button ID="btnIndicadoresGuardar" runat="server" Text="GUARDAR" OnClick="btnIndicadoresGuardar_Click" Visible="false" />--%>
            <asp:Button ID="btnIndicadoresVolver" runat="server" Text="VOLVER" OnClick="btnIndicadoresVolver_Click" Visible="false" CssClass="ButtonNeutro" />
          
    </div> 
    <div class="col-md-12" id="tblIndicadoress">
        <h3>Valores Indicadores Económicos</h3>
        <p> Consulta de Valores de Indicadores del Sistema Gestor. </p>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="Label1" runat="server" Text="Código:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"><asp:TextBox ID="txtBusqIdIndicador" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="Label3" runat="server" Text="Fecha:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5">
                <asp:TextBox ID="txtBusqFchIndicador" runat="server" CssClass="js-date-picker FormatoTextBox"></asp:TextBox>
                
            </div>
        </div>
        <div class="col-md-12" style="text-align:center;"><asp:Button ID="btnIndicadoresConsultar" runat="server" Text="CONSULTAR" OnClick="btnIndicadoresConsultar_Click" CssClass="ButtonNeutro" /></div>
    </div>
    <div class="col-md-12" ><asp:label ID="lblMensaje" runat="server" Visible="false" Font-Bold="true" ></asp:label></div>
    <div style="width: 100%; height: 100%; overflow: auto">
     
        <asp:GridView ID="gvpValoresIndicadoresEco" runat="server" AutoGenerateColumns="False"  ShowHeaderWhenEmpty="True"
             CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
            Width="100%" OnSelectedIndexChanged="gvpValoresIndicadoresEco_SelectedIndexChanged" OnRowEditing="gvpValoresIndicadoresEco_RowEditing"
            OnRowUpdating="gvpValoresIndicadoresEco_RowUpdating" OnRowUpdated="gvpValoresIndicadoresEco_RowUpdated" OnPageIndexChanging="gvpValoresIndicadoresEco_PageIndexChanging"
            OnRowCancelingEdit="gvpValoresIndicadoresEco_RowCancelingEdit" AllowPaging="true" PageSize="20">
                        <Columns>

                            <asp:TemplateField HeaderText="Código" ItemStyle-HorizontalAlign="Center"> 
                                <ItemTemplate>
                                    <asp:Label ID="lblIdIndicador" runat="server" Text='<%# Bind("IdIndicadorEco") %>'></asp:Label>
                                </ItemTemplate>                           
                                <FooterTemplate>
				                    <asp:TextBox ID="txtInsertIdIndicador" runat="server"  Text='<%# Bind("IdIndicadorEco") %>' MaxLength="10" />
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Fecha" > 
                                <ItemTemplate>
                                    <asp:Label ID="lblFchIndicador" runat="server"  TextMode="Date"  Text='<%# Bind("FchReferencia") %>'></asp:Label>
                                </ItemTemplate>                           
                                <FooterTemplate>
				                    <asp:TextBox ID="txtInsertFchIndicador" runat="server"  TextMode="Date"  Text='<%# Bind("FchReferencia") %>' MaxLength="10" />
                                </FooterTemplate>
                            </asp:TemplateField>

                           
                            <asp:TemplateField HeaderText="Valor" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtValorIndicador" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblValorIndicador" runat="server" Text='<%# Bind("Valor") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditValorIndicador" runat="server" TextMode="Number" Text='<%# Bind("Valor") %>' MaxLength="25"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                        </Columns> 
            
                        <EditRowStyle BackColor="#999999" />
             
                    </asp:GridView>
    </div>
</asp:Content>

