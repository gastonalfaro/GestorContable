<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="EntidadesEntregaTardeEstadosFinancieros.aspx.cs" Inherits="Presentacion.Consolidacion.Reportes.EntidadesEntregaTardeEstadosFinancieros" %>

<%@ Register Assembly="eWorld.UI.Compatibility, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI.Compatibility" TagPrefix="cc1" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoJS" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Contenido" runat="server">

    <table>
    <tr>
        <td>
            <asp:LinkButton ID="ViewEntregaATiempoLinkButton" runat="server" 
                    onclick="ViewEntregaATiempoLinkButton_Click" Visible="false">Entrega a Tiempo</asp:LinkButton>
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
                            <asp:DropDownList ID="AmbitoConsolidacionDropDownList" runat="server" AutoPostBack="true"
                                OnDataBound="AmbitoConsolidacionDropDownList_Primero" 
                                DataTextField="NomCorto" DataValueField="IdAmbitoConsolidacion"
                                onselectedindexchanged="AmbitoConsolidacionDropDownList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>  
                        <td>
                            <asp:Label ID="EntidadLabel" runat="server" Text="Entidad: "></asp:Label>
                            <br/>
                            <asp:DropDownList ID="CatalogoEntidadesDropDownList" runat="server"
                                DataTextField="NomUnidad" DataValueField="IdUnidadConsolidacion">
                            </asp:DropDownList>
                        </td>     
                        <td>
                            <asp:Label ID="FechaInicioLabel" runat="server" Text="Fecha Desde: "></asp:Label>
                            <br/>
                            <ew:CalendarPopup ID="FechaInicioCalendarPopup" runat="server" style="margin-right: 0px">
                            </ew:CalendarPopup>
                        </td>     
                        <td>
                            <asp:Label ID="FechaFinLabel" runat="server" Text="Fecha Hasta: "></asp:Label>
                            <br/>
                            <ew:CalendarPopup ID="FechaFinCalendarPopup" runat="server" style="margin-right: 0px">
                            </ew:CalendarPopup>
                        </td>                                                                 
                    </tr>
                    <tr>
                        <td>
                            <br />
                            <asp:Button ID="VerReporteButton" runat="server" Text="Ver Reporte" ValidationGroup="ValidacionGeneral" OnClick="VerReporteButton_Click" />
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>

                <br />                                                                                                                                                                                                                                                         
            </asp:View>            
                    
            <asp:View ID="ReporteView" runat="Server">
                <div>
                    <iframe height="800px" width="100%" src="../../Compartidas/VisorReportes/VisorReportes.aspx" scrolling="no" frameborder="0">
                    </iframe>
                </div> 
            </asp:View>   
        </asp:MultiView>
        
        <asp:Label ID="lblError" runat="server" ForeColor="#FF3300"></asp:Label>
    </asp:Panel> 

</asp:Content>
