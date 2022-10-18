<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="ConsultaPendiente.aspx.cs" Inherits="Presentacion.RevelacionNotas.ConsultaPendiente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoJS" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Contenido" runat="server">
        <div id="formularioRN">
        <h2>Información de Revelación</h2>
        <table style="width: 100%;">
            <tr>
                <td>
                    <asp:Label ID="lblPAnual" runat="server" Text="Periodo Anual:"></asp:Label>
                </td>
                <td class="auto-style1">
                    <asp:Label ID="lblPAnualCons" runat="server" Text="-----"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblPMensual" runat="server" Text="Periodo Mensual: "></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblPMesCons" runat="server" Text="-----"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblNomInstitucion" runat="server" Text="Institucion:"></asp:Label>
                </td>
                <td class="auto-style1">
                    <asp:Label ID="lblInsCons" runat="server" Text="-----"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblEntidad" runat="server" Text="Entidad:"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblEntidadCons" runat="server" Text="-----"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblOficina" runat="server" Text="Oficina:"></asp:Label>
                </td>
                <td class="auto-style1">
                    <asp:Label ID="lblOficinaCons" runat="server" Text="-----"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblClaseCuentas" runat="server" Text="Clase de Cuentas:"></asp:Label>
                </td>
                <td class="auto-style1">
                    <asp:Label ID="lblClaseCuentasCons" runat="server" Text="------"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblGrupoCuentas" runat="server" Text="Grupo de Cuentas:"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblGrupoCons" runat="server" Text="-----"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblAuxCuentas" runat="server" Text="Auxiliares de cuentas:"></asp:Label>
                </td>
                <td class="auto-style1">
                    <asp:Label ID="lblAuxCuentasConsulta" runat="server" Text="------"></asp:Label>
                </td>
            </tr>
            <tr>

                <td>
                    <asp:Label ID="lblConcepto" runat="server" Text="Concepto:"></asp:Label>
                </td>
                <td class="auto-style1">
                    <asp:Label ID="lblConceptoConsulta" runat="server" Text="------"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblJustificacion" runat="server" Text="Justificacion:"></asp:Label>
                </td>
                <td colspan="3">
                    <asp:Label ID="lblJustificacionConsulta" runat="server" Text="------"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblEstadoFijo" runat="server" Text="Estado:"></asp:Label>
                </td>
                <td class="auto-style1">
                    <asp:Label ID="lblEstado" runat="server" Text="-----"></asp:Label>
                </td>
                <td></td>
            </tr>

        </table>
    </div>
    <br />
    <h3>Archivos de Revelación</h3>
    <asp:GridView ID="gvFiles" runat="server" Width="420px" AutoGenerateColumns="False" OnRowCommand="gvFiles_RowCommand" 
          CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
       
        <Columns>
            <asp:TemplateField HeaderText="IdArchivo" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblIdArchivo" runat="server"
                        Text='<%# Bind("IdArchivo") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Dato" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblDato" runat="server"
                        Text='<%# Bind("Dato") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Nombre de Archivo">
                <ItemTemplate>
                    <asp:Label ID="lblNombre" runat="server"
                        Text='<%# Bind("NombreArchivo") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Tamaño">
                <ItemTemplate>
                    <asp:Label ID="lblTamano" runat="server"
                        Text='<%# Bind("Tamano") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Fecha de subida">
                <ItemTemplate>
                    <asp:Label ID="lblFecha" runat="server"
                        Text='<%# Bind("FchCreacion") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="lnkDescargar" runat="server" CausesValidation="False" CommandName="consulta" CommandArgument='<%#Eval("IdArchivo")%>' Text="Descargar"></asp:LinkButton> 
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EditRowStyle BackColor="#999999" />
    </asp:GridView>
    <br />
    <asp:Panel ID="pnlModificacion" runat="server" Visible="false">
        <asp:Button ID="btnAutorizar" runat="server" OnClick="btnAutorizar_Click" Text="Autorizar" CssClass="ButtonNeutro" Width="115px"/>
        <asp:Button ID="btnRechazar" runat="server" OnClick="btnRechazar_Click" Text="Rechazar" CssClass="ButtonNeutro" Width="123px"/>
    </asp:Panel>
    <br />
    <asp:Button ID="btnImprimir" runat="server" Text="Generar Reporte" Width="147px" OnClick="btnImprimir_Click" CssClass="ButtonNeutro" />
    <asp:Button ID="btnCancelar" runat="server" Text="Atrás" Width="96px" OnClick="btnCancelar_Click" CssClass="ButtonNeutro"  />
    <asp:HiddenField ID="hdnFecha" runat="server" />
</asp:Content>
