<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="EntidadesEntregaTardeEstadosFinancieros.aspx.cs" Inherits="Presentacion.Consolidacion.Reportes.EntidadesEntregaTardeEstadosFinancieros" %>

<%@ Register Assembly="eWorld.UI.Compatibility, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI.Compatibility" TagPrefix="cc1" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

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
            <asp:LinkButton ID="ViewEntregaATiempoLinkButton" runat="server" 
                    onclick="ViewEntregaATiempoLinkButton_Click" Visible="false">Parametros</asp:LinkButton>
        </td>
        <td>
            <asp:Label ID="SeparadorEntregaATiempoReporteLabel" runat="server" Text="|" Visible="false"></asp:Label>
        </td>
        <td>
                <asp:LinkButton ID="ViewReporteLinkButton" runat="server" 
                        onclick="ViewReporteLinkButton_Click" Visible="false">Reporte</asp:LinkButton> 
        </td>       
    </tr>
    </table>

    <br />
    <asp:Label ID="Label1" runat="server" Text="Entidades que Entregaron Tarde los Estados Financieros."></asp:Label>

    <asp:Panel id="EntregaATiempoPanel" HorizontalAlign="Left" runat="Server">  
        <asp:MultiView id="EntregaATiempoMultiView" ActiveViewIndex="0" runat="Server">
            <asp:View id="EntregaATiempoView" runat="Server">                                                                               

                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="Ámbito de Consolidación: "></asp:Label>
                            <br/>
                            <asp:DropDownList ID="AmbitoConsolidacionDropDownList" runat="server" AutoPostBack="true" CssClass="FormatoDropDownList"
                                OnDataBound="AmbitoConsolidacionDropDownList_Primero" 
                                DataTextField="NomCorto" DataValueField="IdAmbitoConsolidacion"
                                onselectedindexchanged="AmbitoConsolidacionDropDownList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>  
                        <td>
                            <asp:Label ID="EntidadLabel" runat="server" Text="Entidad: "></asp:Label>
                            <br/>
                            <asp:DropDownList ID="CatalogoEntidadesDropDownList" runat="server"  CssClass="FormatoDropDownList"
                                DataTextField="NomUnidad" DataValueField="IdUnidadConsolidacion">
                            </asp:DropDownList>
                        </td>     
                        <td>
                            <asp:Label ID="PeriodoLabel" runat="server" Text="Año: "></asp:Label>
                            <br/>
                            <asp:DropDownList ID="PeriodoDropDownList" runat="server"  CssClass="FormatoDropDownList"
                                OnDataBound="PeriodoDropDownList_Primero" 
                                DataTextField="Fecha" DataValueField="IdFecha">
                            </asp:DropDownList>
                        </td> 
                        <td>
                            <asp:Label ID="UnidadTiempoPeriodoLabel" runat="server" Text="Unidad de Tiempo Periodo: "></asp:Label>
                            <br/>
                            <asp:DropDownList ID="UnidadTiempoPeriodoDropDownList" runat="server"  CssClass="FormatoDropDownList"
                                OnDataBound="CatalogoUnidadTiempoPeriodoDropDownList_Primero" 
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
                        <td>
                        </td>
                    </tr>
                </table>
                <div style="text-align:center"><asp:Button ID="VerReporteButton" runat="server" Text="Ver Reporte" ValidationGroup="ValidacionGeneral"  CssClass="ButtonNeutro" OnClick="VerReporteButton_Click" /></div>
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
