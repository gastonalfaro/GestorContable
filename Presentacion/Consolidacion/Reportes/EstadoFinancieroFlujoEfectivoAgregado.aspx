<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="EstadoFinancieroFlujoEfectivoAgregado.aspx.cs" Inherits="Presentacion.Consolidacion.Reportes.EstadoFinancieroFlujoEfectivoAgregado" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoJS" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="encabezado" runat="server"></asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Enlaces" runat="server"></asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Contenido" runat="server">
    <link href="../css/mhcr.css" rel="stylesheet"/>
    <table>
    <tr>
        <td>
            <asp:LinkButton ID="ViewCambioPatrimonioNetoAgregadoLinkButton" runat="server" 
                    onclick="ViewCambioPatrimonioNetoAgregadoLinkButton_Click" Visible="false">Parametros</asp:LinkButton>
        </td>
        <td>
            <asp:Label ID="SeparadorCambioPatrimonioNetoAgregadoReporteLabel" runat="server" Text="|" Visible="false"></asp:Label>
        </td>
        <td>
                <asp:LinkButton ID="ViewReporteLinkButton" runat="server" 
                        onclick="ViewReporteLinkButton_Click" Visible="false">Reporte de Flujo de Efectivo Agregado</asp:LinkButton> 
        </td>       
    </tr>
    </table>

    <br />
    <asp:Label ID="Label1" runat="server" Text="Reporte de Flujo de Efectivo Agregado."></asp:Label>

    <asp:Panel id="CambioPatrimonioNetoAgregadoPanel" HorizontalAlign="Left" runat="Server">  
        <asp:MultiView id="CambioPatrimonioNetoAgregadoMultiView" ActiveViewIndex="0" runat="Server">
            <asp:View id="CambioPatrimonioNetoAgregadoView" runat="Server">                                                                               

                <table>
                    <tr>
                        <td>
                            <asp:Label ID="EntidadLabel" runat="server" Text="Ámbito de Consolidación: "></asp:Label>
                            <br/>
                            <asp:DropDownList ID="AmbitoConsolidacionDropDownList" runat="server" AutoPostBack="true"
                                OnDataBound="AmbitoConsolidacionDropDownList_Primero"  CssClass="FormatoDropDownList"
                                DataTextField="NomCorto" DataValueField="IdAmbitoConsolidacion">
                            </asp:DropDownList>
                        </td>     
                        <td>
                            <asp:Label ID="PeriodoLabel" runat="server" Text="Año: "></asp:Label>
                            <br/>
                            <asp:DropDownList ID="PeriodoDropDownList" runat="server"
                                OnDataBound="PeriodoDropDownList_Primero"  CssClass="FormatoDropDownList"
                                DataTextField="Fecha" DataValueField="IdFecha">
                            </asp:DropDownList>
                        </td>     
                        <td>
                            <asp:Label ID="UnidadTiempoPeriodoLabel" runat="server" Text="Unidad de Tiempo Periodo: "></asp:Label>
                            <br/>
                            <asp:DropDownList ID="UnidadTiempoPeriodoDropDownList" runat="server"
                                OnDataBound="CatalogoUnidadTiempoPeriodoDropDownList_Primero"  CssClass="FormatoDropDownList"
                                DataTextField="Descripcion" DataValueField="UnidadTiempoPeriodo">
                            </asp:DropDownList>
                        </td>                                                                 
                    </tr>
                    <tr>
                        <td>
                            <br />
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
                <div style="text-align:center">
                            <asp:Button ID="VerReporteButton" runat="server" Text="Ver Reporte" ValidationGroup="ValidacionGeneral" CssClass="ButtonNeutro" OnClick="VerReporteButton_Click" /></div>
                <br />                                                                                                                                                                                                                                                         
            </asp:View>            
                    
            <asp:View ID="ReporteView" runat="Server">
                <div>
                    <iframe height="800px" width="100%" src="../../Compartidas/VisorReportes/VisorReportes.aspx"   frameborder="0">
                    </iframe>
                </div> 
            </asp:View>   
        </asp:MultiView>
        
        <asp:Label ID="lblError" runat="server" ForeColor="#FF3300"></asp:Label>
    </asp:Panel> 

</asp:Content>
