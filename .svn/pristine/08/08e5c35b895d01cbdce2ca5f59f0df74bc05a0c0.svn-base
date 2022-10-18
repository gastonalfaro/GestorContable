<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmCuentasContables.aspx.cs" Inherits="Presentacion.Mantenimiento.frmCuentasContables" %>
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
         <asp:Button ID="btnCuentasContablesVolver" runat="server" Text="VOLVER" OnClick="btnCuentasContablesVolver_Click" Visible="false" CssClass="ButtonNeutro"/>
    </div> 
    <div class="col-md-12" id="tblCuentasContables">
        <h3>CUENTAS CONTABLES</h3>
        <p>Consulta de Cuentas Contables del Sistema Gestor.</p>
        <div class="col-md-6">
            <div class="col-md-5"><asp:Label ID="lblIdCuentaContable" runat="server" Text="Código:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7"><asp:TextBox ID="txtIdCuentaContable" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-5"> <asp:Label ID="lblIdPlanCuenta" runat="server"  Text="Plan Cuenta:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7"><asp:TextBox ID="txtIdPlanCuenta" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-5"><asp:Label ID="lblIdGrupoCuenta" runat="server" Text="Grupo Cuenta:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7"><asp:TextBox ID="txtIdGrupoCuenta" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-5"><asp:Label ID="lblNomCuentaContable" runat="server" Text="Descripción:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7"><asp:TextBox ID="txtNomCuentaContable" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-5"><asp:Label ID="lblCuentaGrupo" runat="server" Text="Cuenta Grupo:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7"><asp:TextBox ID="txtCuentaGrupo" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-5"><asp:Label ID="lblSociedad" runat="server" Text="Sociedad:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7"><asp:TextBox ID="txtSociedad" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-12" style="text-align:center;"> <asp:Button ID="btnCuentasContablesConsultar" runat="server" Text="CONSULTAR" OnClick="btnCuentasContablesConsultar_Click" CssClass="ButtonNeutro" /></div>
        <div class="col-md-12"><asp:label ID="lblMensaje" runat="server" Visible="false" Font-Bold="true" ></asp:label></div>
    </div>
    <div style="width: 100%; height: 100%; overflow: auto">
  
        <asp:GridView ID="grdvCuentasContables" runat="server" AutoGenerateColumns="False"  ShowHeaderWhenEmpty="True"
            CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
            Width="100%" OnSelectedIndexChanged="grdvCuentasContables_SelectedIndexChanged" OnRowEditing="grdvCuentasContables_RowEditing"
            OnRowUpdating="grdvCuentasContables_RowUpdating" OnRowUpdated="grdvCuentasContables_RowUpdated" OnPageIndexChanging="grdvCuentasContables_PageIndexChanging"
            OnRowCancelingEdit="grdvCuentasContables_RowCancelingEdit" AllowPaging="true" PageSize="20">
                        <Columns>

                            <asp:TemplateField HeaderText="Código"  > 
                                <ItemTemplate>
                                    <asp:Label ID="lblIdCuentaContable" runat="server"   TextMode="Date"    Text='<%# Bind("IdCuentaContable") %>'></asp:Label>
                                </ItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Descripción" >
                                <ItemTemplate>
                                    <asp:Label ID="lblNomCuentaContable" runat="server"   TextMode="Number"  Text='<%# Bind("NomCuentaContable") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Plan Cuenta" > 
                                <ItemTemplate>
                                    <asp:Label ID="lblIdPlanCuenta" runat="server"  Text='<%# Bind("IdPlanCuenta") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Grupo Cuenta" > 
                                <ItemTemplate>
                                    <asp:Label ID="lblIdGrupoCuenta" runat="server" Text='<%# Bind("IdGrupoCuenta") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                           
                            <asp:TemplateField HeaderText="Cuenta Grupo" >
                                <ItemTemplate>
                                    <asp:Label ID="lblCuentaGrupo" runat="server"   TextMode="Number"  Text='<%# Bind("CuentaGrupo") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

              <%--              <asp:TemplateField HeaderText="Ind.Totales" >
                                <ItemTemplate>
                                    <asp:Label ID="lblIndTotales" runat="server"   TextMode="Number"  Text='<%# Bind("IndTotales") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Ind.Consolidacion" >
                                <ItemTemplate>
                                    <asp:Label ID="lblIndConsolidacion" runat="server"   TextMode="Number"  Text='<%# Bind("IndConsolidacion") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>

                            <asp:TemplateField HeaderText="Estado" >
                                <ItemTemplate>
                                    <asp:Label ID="lblEstado" runat="server"   TextMode="Number"  Text='<%# Bind("Estado") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>  

                        <EditRowStyle BackColor="#999999" />

            </asp:GridView>
             
    </div>
</asp:Content>


