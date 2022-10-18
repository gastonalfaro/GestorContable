<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmCalculosFinancieros.aspx.cs" Inherits="Presentacion.CalculosFinancieros.frmCalculosFinancieros" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoJS" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="encabezado" runat="server">
    <div class="rmm" runat="server">
        <ul runat="server">
            <li id="liOBJ_CI" runat="server" visible="false"><a href='~/CapturaIngresos/frmCapturaIngresos.aspx' runat="server">Captura Ingresos</a></li>
            <li id="liOBJ_RN" runat="server" visible="false"><a href="~/RevelacionNotas/Formularios.aspx" runat="server">Revelación Notas</a></li>
            <li id="liOBJ_PC" runat="server" visible="false"><a href='~/Consolidacion/frmCargarArchivos.aspx' runat="server">Plan.Consolidación</a></li>
            <li id="liOBJ_CF" runat="server" visible="false"><a href='~/CalculosFinancieros/frmCalculosFinancieros.aspx' runat="server">Deuda Pública</a></li>
            <li id="liOBJ_CT" runat="server" visible="false"><a href='~/Contingentes/ConsultarExpedientes.aspx' runat="server">Contingentes</a></li>
            <li id="liOBJ_MA" runat="server" visible="false"><a href='~/Mantenimiento/frmParametros.aspx' runat="server">Mantenimiento</a></li>
			<li id="liOBJ_SG" runat="server" visible="false"><a href="~/Seguridad/Usuarios.aspx" runat="server">Seguridad</a></li>
        </ul>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Enlaces" runat="server">
    <h3>Deuda Interna</h3>
        <ul>
            <li><a class="vinculoizquierda" href="../CalculosFinancieros/DeudaInterna/frmCostoTransaccion.aspx">Agregar Costos de Transaccion</a></li>
            <li><a class="vinculoizquierda" href="../CalculosFinancieros/DeudaInterna/frmTitulosGarantia.aspx">Agregar Título en Garantía</a></li>
            <li><a class="vinculoizquierda" href="../CalculosFinancieros/DeudaInterna/frmCargarDatosRDs.aspx">Cargar datos de RDI y RDE</a></li>
            <li><a class="vinculoizquierda" href="../CalculosFinancieros/DeudaInterna/frmPagosCCSS.aspx">Cargar Pagos de CCSS</a></li>
            <li><a class="vinculoizquierda" href="../CalculosFinancieros/DeudaInterna/frmTrasladosMagisterio.aspx">Cargar traslados a Magisterio</a></li>
            <li><a class="vinculoizquierda" href="../CalculosFinancieros/DeudaInterna/frmIncluirDatos.aspx">Cargar Títulos Manualmente</a></li>
        </ul>
    <h3>Deuda Externa</h3>
        <ul>
            <li><a class="vinculoizquierda" href="../CalculosFinancieros/DeudaExterna/frmAsientosReversion.aspx">Asientos de Reversión</a></li>
        </ul>
        <h3>Reportes</h3>
        <ul>
            <li><a class="vinculoizquierda" href="../CalculosFinancieros/DeudaInterna/frmReporteCancelacion.aspx">Reporte de Cancelaciones</a></li>
            <li><a class="vinculoizquierda" href="../CalculosFinancieros/DeudaInterna/frmReporteOperacionesEspeciales.aspx">Reporte de Operaciones Especiales</a></li>
            <li><a class="vinculoizquierda" href="../CalculosFinancieros/DeudaInterna/frmReporteDevengosInt.aspx">Reporte de Devengo</a></li>
            <li><a class="vinculoizquierda" href="../CalculosFinancieros/DeudaInterna/frmReporteColocaciones.aspx">Reporte de Colocaciones</a></li>
            <li><a class="vinculoizquierda" href="../CalculosFinancieros/DeudaInterna/frmReporteNemotecnicosCPLP.aspx">Reporte de Nemotécnicos</a></li>
            <li><a class="vinculoizquierda" href="../CalculosFinancieros/DeudaInterna/frmReporteSaldosDI.aspx">Reporte de Saldos de Deuda Interna</a></li>
            <li><a class="vinculoizquierda" href="../CalculosFinancieros/DeudaInterna/frmReporteCuponesPagados.aspx">Reporte de Cupones Pagados</a></li>
        </ul>
        <h3 id="liMantenimientoCF">Mantenimiento</h3>
        <ul >
            <li id="lifrmParametros" visible="false" runat="server"><a href="../../Mantenimiento/frmParametros.aspx" class="vinculoizquierda">Parámetros</a></li>
            <li id="lifrmOperaciones" visible="false" runat="server"><a href="../../Mantenimiento/frmOperaciones.aspx" class="vinculoizquierda">Operaciones</a></li>
            <li id="lifrmSociedadesCo" visible="false" runat="server"><a href="../../Mantenimiento/frmSociedadesCo.aspx" class="vinculoizquierda">Sociedades de Costo</a></li>
            <li id="lifrmPropietarios" visible="false" runat="server"><a href="../../Mantenimiento/frmPropietarios.aspx" class="vinculoizquierda">Propietarios</a></li>
            <li id="lifrmNemotecnicos" visible="false" runat="server"><a href="../../Mantenimiento/frmNemotecnicos.aspx" class="vinculoizquierda">Nemotécnicos</a></li>
        </ul>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Contenido" runat="server">
    <h1>Módulo de Deuda Pública.</h1>
    <p>&nbsp;</p>
    <p>
        <asp:TextBox ID="TextBox1" runat="server" OnTextChanged="TextBox1_TextChanged" TextMode="Number"></asp:TextBox>
    </p>
    <p>&nbsp;</p>
    <p>&nbsp;</p>
    <h3>Submódulo de Deuda Interna.</h3>
    <p>En este submódulo de Deuda Interna se podrán realizar las siguientes tareas:</p>
    <ul runat="server">
            <li runat="server" visible="true">Carga de información de CCSS y Magisterio</li>
            <li runat="server" visible="true">Reportes varios sobre cancelaciones, operaciones especiales, entre otras.</li>
            <li runat="server" visible="true">Mantenimiento general del módulo</li>
        </ul>
</asp:Content>
