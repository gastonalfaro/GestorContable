<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="EdicionRevContingente.aspx.cs" Inherits="Presentacion.RevelacionNotas.Contingencias.EdicionRevContingente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoJS" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Contenido" runat="server">
    <div id="formularioRN" style="display:inline-block;">        
        <div>
            <h2>Informe de Contingencias</h2>
            <div class="col-md-6">
                <div class="col-md-4"><strong>Consecutivo: </strong></div>
                <div class="col-md-7"><asp:Label ID="lbltextoConsecutivo" runat="server"></asp:Label></div>
            </div>
            <div class="col-md-6">
                <div class="col-md-4"><strong>Periodo Anual: </strong></div>
                <div class="col-md-7"><asp:Label ID="lblPAnual" runat="server"></asp:Label></div>
            </div>
            <div class="col-md-6">
                <div class="col-md-4"><strong>Periodo Mensual: </strong></div>
                <div class="col-md-7"><asp:Label ID="lblPMensual" runat="server"></asp:Label></div>
            </div>
            <div class="col-md-6">
                <div class="col-md-4"><strong>Ministerio: </strong></div>
                <div class="col-md-7"><asp:Label ID="lblMinisterio" runat="server"></asp:Label></div>
            </div>
            <div class="col-md-6">
                <div class="col-md-4"><strong>Tipo: </strong></div>
                <div class="col-md-7"><asp:Label ID="lblTipo" runat="server" Text="Tipo: "></asp:Label></div>
            </div>
        </div>
        <div class="col-md-12">
            <h3>Activos</h3>
            <div class="col-md-6">
                <div class="col-md-4"><strong>Monto Total: </strong></div>
                <div class="col-md-7"><asp:Label ID="lblMontoActivos" runat="server"></asp:Label></div>
            </div>
            <div class="col-md-6">
                <div class="col-md-4"><strong>Total Expedientes: </strong></div>
                <div class="col-md-7"><asp:Label ID="lblExpedientesActivos" runat="server"></asp:Label></div>
            </div>
            <div class="col-md-12" style="text-align:center;"></div>
        </div>        
        <br />
        <div class="col-md-12">
            <h3>Pasivos</h3>
            <div class="col-md-6">
                <div class="col-md-4"><strong>Monto Total: </strong></div>
                <div class="col-md-7"><asp:Label ID="lblMontoPasivos" runat="server"></asp:Label></div>
            </div>
            <div class="col-md-6">
                <div class="col-md-4"><strong>Total Expedientes: </strong></div>
                <div class="col-md-7"><asp:Label ID="lblExpedientesPasivos" runat="server" Text=""></asp:Label></div>
            </div>
            <div class="col-md-12" style="text-align:center;"></div>
        </div>
        <div class="col-md-12">
            <h3>Observaciones</h3>
            <asp:TextBox ID="txtObservaciones" runat="server" Width="100%" Height="109px" TextMode="MultiLine" CssClass="FormatoTextBox"></asp:TextBox>
        </div>
        <div class="col-md-12" style="text-align:center;display:inline-block;">
            <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" OnClick="btnAceptar_Click" CssClass="ButtonNeutro"/>
            <asp:Button ID="btnAtras" runat="server" Text="Atrás" OnClick="btnAtras_Click" CssClass="ButtonNeutro" />
        </div>
    </div>
</asp:Content>
