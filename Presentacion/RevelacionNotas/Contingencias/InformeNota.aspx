<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="InformeNota.aspx.cs" Inherits="Presentacion.RevelacionNotas.Contingencias.InformeNota" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoJS" runat="server">
        <script src="https://code.jquery.com/ui/1.10.3/jquery-ui.js"></script> 
        <script src="../../Compartidas/rmm-js/jquery.min.js" type="text/javascript"></script>    
        <script src="/Compartidas/rmm-js/chosen.jquery.js" type="text/javascript"></script>
        <script src="../../Compartidas/jqueryPopbox/popBox1.3.0.min.js" type="text/javascript"></script>
        <script src="../../Compartidas/jqueryPopbox/popBox1.3.0.js" type="text/javascript"></script>
        <link href="../../Compartidas/jqueryPopbox/popBox1.3.0.css" rel="stylesheet" type="text/css" />
         <script type="text/javascript">
<%--             $(document).ready(function () { 
                 $('#<%=btnPrueba.ClientID%>').popBox();
             });--%>

         </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Contenido" runat="server">
    <div id="formularioRN" style="display:inline-block;">
        <h2>Informe de Contingencias</h2>
        <div class="col-md-6">
            <div class="col-md-8"><asp:Label ID="lblPAnual" runat="server" Text="Periodo Anual:" Font-Size="Medium"></asp:Label></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-8"><asp:Label ID="lblPMensual" runat="server" Text="Periodo Mensual: " Font-Size="Medium"></asp:Label></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-4"><asp:Label ID="lblMinisterio" runat="server" Text="Ministerio: "></asp:Label></div>
            <div class="col-md-7"><asp:DropDownList ID="ddlMinisterio" runat="server" CssClass="FormatoDropDownList"></asp:DropDownList></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-4"><asp:Label ID="lblEstadoProcesal" runat="server" Text="Tipo de Proceso (Área): "></asp:Label></div>
            <div class="col-md-7"><asp:DropDownList ID="ddlOpciones" runat="server" CssClass="FormatoDropDownList"></asp:DropDownList></div>
        </div>
        <div style="text-align:center;"><asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" CssClass="ButtonNeutro" /></div>
        
        
        <br/>
        <br/>
        <asp:GridView ID="gvRevSoc" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center" OnRowEditing="gvRevSoc_RowEditing" OnPageIndexChanging="gvRevSoc_PageIndexChanging" PageSize="10" AllowPaging="True"
              CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
            <Columns>
                <asp:TemplateField AccessibleHeaderText="IdSociedad" HeaderText="IdSociedad" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblIdSociedad" runat="server" Text='<%# Bind("IdSociedadGL") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField AccessibleHeaderText="IdOpcion" HeaderText="IdOpcion" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblTipoProceso" runat="server" Text='<%# Bind("TipoProceso") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField AccessibleHeaderText="NomSociedad" HeaderText="Ministerio">
                    <ItemTemplate>
                        <asp:Label ID="lblNomSociedad" runat="server" Text='<%# Bind("NomSociedad") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField AccessibleHeaderText="NombreTipoProceso" HeaderText="Tipo">
                    <ItemTemplate>
                        <asp:Label ID="lblNombreTipoProceso" runat="server" Text='<%# Bind("NomOpcion") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField  DataField="MontoTotalActivos"  DataFormatString="{0:N}" HeaderText="Monto Total Activos" >
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:TemplateField AccessibleHeaderText="TotalExpActivos" HeaderText="Expedientes Activos">
                    <ItemTemplate>
                        <asp:Label ID="lblTotalExpActivos" runat="server" Text='<%# Bind("TotalExpActivos") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField  DataField="MontoTotalPasivos"  DataFormatString="{0:N}" HeaderText="Monto Total Pasivos" >
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>   
                <asp:TemplateField AccessibleHeaderText="TotalExpPasivos" HeaderText="Expedientes Pasivos">
                    <ItemTemplate>
                        <asp:Label ID="lblTotalExpPasivos" runat="server" Text='<%# Bind("TotalExpPasivos") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField AccessibleHeaderText="Observaciones" HeaderText="Observaciones">
                    <ItemTemplate>
                        <asp:Label ID="lblObservaciones" runat="server" Text='<%# Bind("Observaciones") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField AccessibleHeaderText="Modificar" HeaderText="">
                    <ItemTemplate> 
                        <asp:LinkButton ID="lnkEditar" runat="server" CausesValidation="False" CommandName="Edit" Text="Editar"></asp:LinkButton> 
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EditRowStyle BackColor="#999999" />
        </asp:GridView>
    </div>
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Exportar" CssClass="ButtonNeutro" />
    <asp:HiddenField ID="hdnRev" runat="server" Visible="False" />
</asp:Content>
