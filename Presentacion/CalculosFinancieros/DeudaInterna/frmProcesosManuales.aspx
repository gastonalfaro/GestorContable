<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmProcesosManuales.aspx.cs" Inherits="Presentacion.CalculosFinancieros.DeudaInterna.frmProcesosManuales" EnableEventValidation="false" %>

<%@ Register assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2" namespace="eWorld.UI" tagprefix="ew" %>

<%@ Register Assembly="eWorld.UI.Compatibility, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI.Compatibility" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoJS" runat="server">
    <script src="https://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
    <script src="Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="Scripts/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            SearchText();
        });
        function SearchText() {
            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
        }
    </script>
    <script>

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
    
    <style type="text/css"> .contenido { width:90%!important; } </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Contenido" runat="server">
    <div id="datos">
         <div class="col-md-12">  
             <h3>Procesos Manuales:</h3>
             <br />
                <h4>Criterios de Cálculos Manuales:</h4> <br />
        </div>
        <div class="col-md-12">
            <div class="col-md-2">Nemotécnico:</div>
            <div class="col-md-4">
                 <asp:DropDownList runat="server" ID="ddlNemotecnico" AutoPostBack="True" AppendDataBoundItems="True"  CssClass="chzn-select FormatoDropDownList" OnSelectedIndexChanged="ddlNemotecnico_SelectedIndexChanged">
                        <asp:ListItem Value="">-- Seleccione Opcion --</asp:ListItem>
                    </asp:DropDownList>
            </div>
            <div class="col-md-2">Número de Valor:</div>
            <div class="col-md-4"><asp:DropDownList ID="ddlNumValor" runat="server" AppendDataBoundItems="True" AutoPostBack="True"  CssClass="FormatoDropDownList">
                    <asp:ListItem Value="">-- Seleccione Opción --</asp:ListItem>
                </asp:DropDownList>
                </div>
        </div>
        
        <div class="col-md-12">
                <div class="col-md-2">Fecha:</div>
                <div class="col-md-4"><asp:TextBox ID="txtFchFin" runat="server" CssClass="js-date-picker FormatoTextBox"></asp:TextBox></div>
        
                <div class="col-md-2">Seleccione el Proceso:</div>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlProceso" runat="server" AutoPostBack="true" CssClass="FormatoDropDownList">
                    <asp:ListItem Value="CarVal">Cargar Colocaciones (Valores,Cupones) del día</asp:ListItem>
                    <asp:ListItem Value="ConCol">Contabilizar Colocaciones del día</asp:ListItem>
                    <asp:ListItem Value="CosTran">Costos de Transacción del día</asp:ListItem>
                    <asp:ListItem Value="CalDev">Cálcular Devengo del día</asp:ListItem>
                    <asp:ListItem Value="CarCan">Cargar Cancelaciones del día</asp:ListItem>
                    <asp:ListItem Value="PagCup">Pago de Cupones del día</asp:ListItem>
                    <asp:ListItem Value="ConDev">Contabilizar Devengos</asp:ListItem>
                    <asp:ListItem Value="RecVal">Reclasificaciones del mes</asp:ListItem>
                    <asp:ListItem Value="DifCam">Diferencial Cambiario</asp:ListItem>
                    <asp:ListItem Value="CieMes">Cierre de Mes</asp:ListItem>
                    <asp:ListItem Value="CieAno">Cierre de Año</asp:ListItem>
                    <asp:ListItem Value="CalSub">Calcular Subasta</asp:ListItem>
                    <asp:ListItem Value="ConSub">Contabilizar Subasta</asp:ListItem>
                    <asp:ListItem Value="CalCan">Calcular Canje</asp:ListItem>
                    <asp:ListItem Value="ConCan">Contabilizar Canje</asp:ListItem>
                    <asp:ListItem Value="ConPres">Contabilizar Prescripciones</asp:ListItem>
                    <asp:ListItem Value="ConMag">Contabilizar Magisterio</asp:ListItem>
                    <asp:ListItem Value="ConCanc">Contabilizar Cancelaciones</asp:ListItem>
                    <asp:ListItem Value="ConCanAnti">Contabilizar Cancelaciones Anticipadas</asp:ListItem>
                    <%--<asp:ListItem Value="DTSSIGADE">Ejecutar Extracción SIGADE</asp:ListItem>--%>
                    </asp:DropDownList></div>
        </div>
       
        <div class="col-md-12" style="text-align:center;">
             <br /><br /><br />
              <div class="col-md-3">
              <asp:Button ID="btnProceso" runat="server" CssClass="ButtonNeutro" Text="Ejecutar Proceso" Width="200px" OnClick="btnProceso_Click" />
        
           
            </div>
            <div class="col-md-3">
              <asp:Button ID="btnCanje" visible="false" runat="server" CssClass="ButtonNeutro" Text="Cálculo Canje" Width="200px" OnClick="btnCanje_Click" />
           
            </div>
            <div class="col-md-3">
              <asp:Button ID="btnSubasta" visible="false" runat="server" CssClass="ButtonNeutro" Text="Cálculo Subasta" Width="200px" OnClick="btnSubasta_Click" />
           
            </div>
             <div class="col-md-3">
             <asp:Button ID="btnContaCanje"  visible="false" runat="server" CssClass="ButtonNeutro" Text="Contabilizar Canje" Width="200px" OnClick="btnContaCanje_Click" />
        
            </div>

             <div class="col-md-3">
             <asp:Button ID="btnContaSubasta" visible="false" runat="server" CssClass="ButtonNeutro" Text="Contabilizar Subasta" Width="200px" OnClick="btnContaSubasta_Click" />
        
            </div>

       </div>

        <div class="col-md-12" style="text-align:center">
            <div class="col-md-4">
            </div>
        </div>
        <br />
<%--        <asp:Panel ID="PanelReporte" runat="server">
            <div align="center">
                <iframe height="800px" width="100%" src="../../../Compartidas/VisorReportes/VisorReportes.aspx" frameborder="0"></iframe>
            </div>
        </asp:Panel>--%>
        <br />
    </div>
</asp:Content>
