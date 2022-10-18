<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="BitacoraErroresPorFechaProcesoDTSX.aspx.cs" Inherits="Presentacion.Consolidacion.Reportes.BitacoraErroresPorFechaProcesoDTSX" %>

<%@ Register Assembly="eWorld.UI.Compatibility, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI.Compatibility" TagPrefix="cc1" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoJS" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="server"></asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="encabezado" runat="server"></asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Enlaces" runat="server"></asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="Contenido" runat="server">
    <link href="../css/mhcr.css" rel="stylesheet"/>
    <table>
    <tr>
        <td>
            <asp:LinkButton ID="ViewBitacoraErrorLinkButton" runat="server" 
                    onclick="ViewBitacoraErrorLinkButton_Click" Visible="false">Parametros</asp:LinkButton>
        </td>
        <td>
            <asp:Label ID="SeparadorBitacoraErrorReporteLabel" runat="server" Text="|" Visible="false"></asp:Label>
        </td>
        <td>
                <asp:LinkButton ID="ViewReporteLinkButton" runat="server" 
                        onclick="ViewReporteLinkButton_Click" Visible="false">Reporte de Bitacora de Errores por Fecha y Proceso</asp:LinkButton> 
        </td>       
    </tr>
    </table>

    <br />
    <asp:Label ID="Label1" runat="server" Text="Bitácora de Errores por Fecha de Proceso."></asp:Label>

    <asp:Panel id="BitacoraErrorPanel" HorizontalAlign="Left" runat="Server">  
        <asp:MultiView id="BitacoraErrorMultiView" ActiveViewIndex="0" runat="Server">
            <asp:View id="BitacoraErrorView" runat="Server">                                                                               

                <asp:GridView ID="GridView" 
                    autogeneratecolumns="False"
                    datakeynames="Fecha, NombreProceso" 
                    runat="server"
                    PersistedSelection="true"
                    AllowPaging="True"
                    ShowFooter="True"
                    EnableEventValidation="false"
                     CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
                    OnRowDeleting="btnSelectBitacoraLinkButton_Click" 

                    OnPageIndexChanging="gridView_PageIndexChanging">

                    <columns>
                        <asp:templatefield>                   
                            <footertemplate>
                                  <asp:linkbutton id="btnSearch" runat="server" commandname="Search" text="Buscar" OnClick="btnSearch_Click"/>       
                            </footertemplate>
                        </asp:templatefield>
                      
                        <asp:TemplateField HeaderText="FECHA" SortExpression="Fecha">              
                            <ItemTemplate>
                                <asp:Label ID="ItemFechaLabel" runat="server" Text='<%# Bind("Fecha","{0:dd/MM/yyyy HH:mm:ss.fff}") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>   
                                <ew:CalendarPopup ID="FooterFechaCalendarPopup" runat="server" style="margin-right: 0px">
                                </ew:CalendarPopup>                                                                                                                                                  
                            </FooterTemplate>
                        </asp:TemplateField> 
            
                        <asp:TemplateField HeaderText="NOMBRE PROCESO" SortExpression="NombreProceso">              
                            <ItemTemplate>
                                <asp:Label ID="ItemNombreProcesoLabel" runat="server" Text='<%# Bind("NombreProceso") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="FooterNombreProcesoTextBox" runat="server" width="300" Text='<%# Bind("NombreProceso") %>'></asp:TextBox>                     
                            </FooterTemplate>
                        </asp:TemplateField>                            
                                                                                                                              
                        <asp:TemplateField HeaderText="CONTROLES DE EDICION">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnSelectBitacoraLinkButton" runat="server" CommandName="Delete" text="Seleccionar" />
                            </ItemTemplate>              
                        </asp:TemplateField>                       
                    </columns>
        
                    <PagerSettings 
                        Position="TopAndBottom" 
                        Mode="Numeric"
                        pagebuttoncount="10"/>
            
                </asp:GridView>

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





     

