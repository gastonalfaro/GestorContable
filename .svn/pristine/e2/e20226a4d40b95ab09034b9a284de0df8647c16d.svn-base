<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="ConsultaDeuda.aspx.cs" Inherits="Presentacion.RevelacionNotas.ConsultaDeuda" %>

<%@ Register Assembly="eWorld.UI.Compatibility, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI.Compatibility" TagPrefix="cc1" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>
<asp:Content ID="ContentScripts" ContentPlaceHolderID="ContenidoJS" runat="server">
    <script src="https://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
    <script src="Scripts/jquery.min.js" type="text/javascript"></script>
   
    <script src="/Compartidas/rmm-js/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            SearchText();
        });
        function SearchText() {
            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
        }
        function CallPrint(strid) {
            var prtContent = document.getElementById(strid);
            var WinPrint = window.open('', '', 'letf=0,top=0,width=800,height=500,toolbar=0,scrollbars=0,status=0,dir=ltr');
            WinPrint.document.write(prtContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
            prtContent.innerHTML = strOldOne;
        }
    </script>
    <style type="text/css">
        .auto-style1 {
            width: 288px;
        }
        .auto-style2 {
            width: 233px;
        }
        .auto-style3 {
            width: 158px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenido" runat="server">
    <div id="formularioRN">
        <h2>Información de Revelación</h2>
        <table style="width: 100%;">
            <tr>
                <td>
                    <asp:Label ID="lbltextoConsecutivo" runat="server" Text="Consecutivo:"></asp:Label>
                </td>
                <td class="auto-style2">
                    <asp:Label ID="lblConsecutivo" runat="server" Text="------"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblPAnual" runat="server" Text="Tipo de Deuda:"></asp:Label>
                </td>
                <td class="auto-style2">
                    <asp:Label ID="lblTipoDeuda" runat="server" Text="-----"></asp:Label>
                </td>
                <td class="auto-style3">
                    <asp:Label ID="lblCategoria" runat="server" Text="Categoría: "></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblCategorias" runat="server" Text="-----"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Periodo Anual:"></asp:Label>
                </td>
                <td class="auto-style2">
                    <asp:Label ID="lblPeriodoAnual" runat="server" Text="-----"></asp:Label>
                </td>
                <td class="auto-style3">
                    <asp:Label ID="Label3" runat="server" Text="Periodo Mensual: "></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblPeriodoMensual" runat="server" Text="-----"></asp:Label>
                </td>
            </tr>
            <%--            <tr>
                <td>
                    <asp:Label ID="lblEstadoFijo" runat="server" Text="Estado:"></asp:Label>
                </td>
                <td class="auto-style1">
                    <asp:Label ID="lblEstado" runat="server" Text="-----"></asp:Label>
                </td>
                <td></td>
            </tr>--%>

            <asp:Panel ID="pnlModificacion" runat="server" Visible="false">
                <tr>
                    <td>
                        <asp:Label ID="lblUltimoDiaMod" runat="server" Text="Fecha límite de edición:"></asp:Label>
                    </td>
                    <td class="auto-style1">
                        <asp:TextBox ID="txtFechaLimiteEdicion" runat="server" CssClass="js-date-picker FormatoTextBox"></asp:TextBox>
                        <%--<asp:Button ID="btnCambiarFecha" runat="server" Text="Aceptar" OnClick="btnCambiarFecha_Click" Visible="False" />--%>
                        <asp:Label ID="lblMensaje" runat="server" Font-Italic="True" ForeColor="Red" Text="Label" Visible="False"></asp:Label>
                    </td>
                    <td>
                        <asp:Button ID="btnHabilitar" runat="server" Text="Habilitar cambios" OnClick="btnHabilitar_Click" CssClass="ButtonNeutro"/>
                    </td>
                </tr>
            </asp:Panel>
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
    <asp:Button ID="btnImprimir" runat="server"  Text="Generar Reporte" OnClick="btnImprimir_Click" Visible="False" Width="140px" CssClass="ButtonNeutro" />
    <asp:Button ID="btnCancelar" runat="server" Text="Atrás" Width="100px" OnClick="btnCancelar_Click" CssClass="ButtonNeutro" />
    <%--    OnClientClick="javascript:CallPrint('formularioRN');"--%>
    <asp:HiddenField ID="hdnFecha" runat="server" />
    </asp:Content>

