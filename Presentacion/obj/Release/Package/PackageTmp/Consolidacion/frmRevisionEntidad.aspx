﻿<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmRevisionEntidad.aspx.cs" Inherits="Presentacion.Consolidacion.frmRevisionEntidad" %>

<%@ Register Assembly="eWorld.UI.Compatibility, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI.Compatibility" TagPrefix="cc1" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoJS" runat="server">
    <style type="text/css">
        .ButtonNeutro, .ButtonNeutro:hover {
            width:290px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Contenido" runat="server">

    <link href="../css/mhcr.css" rel="stylesheet"/>

    <br />
    <%--<asp:Label ID="Label1" runat="server" Text="Carga de Información por Parte de la Entidad."></asp:Label>--%>
    <h2>Revisión de Información por Parte de la Entidad.</h2>
    <br />
    <div class="col-md-8">
        

    <table class="table table-borderless">
    <tr>
        <td style="text-align:center">
            <asp:LinkButton ID="ViewCargaEntidadLinkButton" runat="server" 
                    onclick="ViewCargaEntidadLinkButton_Click" Visible="false">CARGA ENTIDAD</asp:LinkButton>
        </td>
        <td>
            <asp:Label ID="SeparadorCargaEntidadArchivosAnexosLabel" runat="server" Text="|" Visible="false"></asp:Label>
        </td>
        <td style="text-align:center">
                <asp:LinkButton ID="ViewArchivosAnexosLinkButton" runat="server" 
                        onclick="ViewArchivosAnexosLinkButton_Click" Visible="false">ARCHIVOS ANEXOS</asp:LinkButton> 
        </td>   

        <td>
            <asp:Label ID="SeparadorArchivosAnexosArchivosPlantillasLabel" runat="server" Text="|" Visible="false"></asp:Label>
        </td>
        <td style="text-align:center">
                <asp:LinkButton ID="ViewArchivosPlantillasLinkButton" runat="server" 
                        onclick="ViewArchivosPlantillasLinkButton_Click" Visible="false">ARCHIVOS PLANTILLAS</asp:LinkButton> 
        </td>  

        <td>
            <asp:Label ID="SeparadorCargaEntidadCorreosAutorizacionLabel" runat="server" Text="|" Visible="false"></asp:Label>
        </td>
        <td style="text-align:center">
                <asp:LinkButton ID="ViewCorreosAutorizacionLinkButton" runat="server" 
                        onclick="ViewCorreosAutorizacionLinkButton_Click" Visible="false">CORREOS DE AUTORIZACIÓN</asp:LinkButton> 
        </td>            
    </tr>
    </table>
        </div>
    <br/>

    <div class="col-md-12">
        <div class="col-md-6">
            <div class="col-md-4"> <asp:Label ID="AmbitoConsolidacionLabel" runat="server" Text="Ámbito de Consolidación: "></asp:Label></div>
            <div class="col-md-6"><asp:DropDownList ID="AmbitoConsolidacionDropDownList" runat="server" AutoPostBack="true"  CssClass="FormatoDropDownList"
                    OnDataBound="AmbitoConsolidacionDropDownList_Primero" 
                    DataTextField="NomCorto" DataValueField="IdAmbitoConsolidacion"
                    onselectedindexchanged="AmbitoConsolidacionDropDownList_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
        </div>
        <div class="col-md-6">
            <div class="col-md-4"><asp:Label ID="CatalogoEntidadesLabel" runat="server" Text="Entidad: "></asp:Label></div>
            <div class="col-md-6">
                  <asp:DropDownList ID="CatalogoEntidadesDropDownList" runat="server"  CssClass="FormatoDropDownList" 
                    DataTextField="NomUnidad" DataValueField="IdUnidadConsolidacion">
                </asp:DropDownList>
            </div>
        </div>
       
        <div><span>&nbsp;</span></div>
         <div class="col-md-6">
            <div class="col-md-4"><asp:Label ID="PeriodoLabel" runat="server" Text="Año: "></asp:Label></div>
            <div class="col-md-6">
                 <asp:DropDownList ID="PeriodoDropDownList" runat="server"  CssClass="FormatoDropDownList"
                    OnDataBound="PeriodoDropDownList_Primero"
                    DataTextField="Fecha" DataValueField="IdFecha">
                </asp:DropDownList>
            </div>
        </div>
               
        <div class="col-md-6">
            <div class="col-md-4"><asp:Label ID="UnidadTiempoPeriodoLabel" runat="server" Text="Unidad de Tiempo Periodo: "></asp:Label></div>
            <div class="col-md-6">
                <asp:DropDownList ID="UnidadTiempoPeriodoDropDownList" runat="server"  CssClass="FormatoDropDownList"
                    OnDataBound="CatalogoUnidadTiempoPeriodoDropDownList_Primero" style="width:100%"
                    DataTextField="Descripcion" DataValueField="UnidadTiempoPeriodo">
                </asp:DropDownList>
            </div>
        </div>
        <div><span>&nbsp;</span></div>
        <div class="col-md-12" style="text-align:center;">
            <%--<asp:Button ID="InfoCargadaEstadosFinancierosArchivosButton" runat="server" Text="Ver Información Cargada" OnClick="InfoCargadaEstadosFinancierosArchivosButton_Click" CssClass="ButtonNeutro"/>--%>
            <asp:Button ID="InfoCargadaEstadosFinancierosArchivosButton" runat="server" Text="Ver Información Cargada" OnClick="InfoCargadaEstadosFinancierosArchivosButton_Click" CssClass="btn btn-primary"/>
            
        </div>
    </div>

    <br />

    <asp:Panel id="CargaEntidadPanel" HorizontalAlign="Left" runat="Server">  
        <asp:MultiView id="CargaEntidadMultiView" ActiveViewIndex="0" runat="Server">
            <asp:View id="CargaEntidadView" runat="Server"> 
                <h2>Estados Financieros</h2> 
                <br/>                                                                                                                                                  

                <asp:GridView ID="EstadosFinancierosArchivosGridView" 
                    autogeneratecolumns="False"
                    datakeynames="IdEntidad, IdEstadoFinanciero, Periodo, UnidadTiempoPeriodo" 
                    runat="server"
                    PersistedSelection="true"
                    AllowPaging="True"
                    ShowFooter="True"
                   CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
                    OnRowCommand="EstadosFinancierosArchivosGridView_RowCommand"
                    OnPageIndexChanging="EstadosFinancierosArchivosGridView_PageIndexChanging">
        
                    <columns>  
                        <asp:TemplateField HeaderText="Codigo Id Entidad" Visible="false" SortExpression="IdEntidad">
                            <ItemTemplate>
                                <asp:Label ID="CodigoIdEntidadLabel" runat="server" Text='<%# Bind("IdEntidad") %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>                    
                        <asp:TemplateField HeaderText="Codigo Id Estado Financiero" Visible="false" SortExpression="IdEstadoFinanciero">
                            <ItemTemplate>
                                <asp:Label ID="CodigoIdEstadoFinancieroLabel" runat="server" Text='<%# Bind("IdEstadoFinanciero") %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Codigo Periodo" Visible="false" SortExpression="Periodo">
                            <ItemTemplate>
                                <asp:Label ID="CodigoPeriodoLabel" runat="server" Text='<%# Bind("Periodo") %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Codigo Unidad Tiempo Periodo" Visible="false" SortExpression="UnidadTiempoPeriodo">
                            <ItemTemplate>
                                <asp:Label ID="CodigoUnidadTiempoPeriodoLabel" runat="server" Text='<%# Bind("UnidadTiempoPeriodo") %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Codigo Id Estado Financiero Archivo" Visible="false" SortExpression="IdEstadoFinancieroArchivo">
                            <ItemTemplate>
                                <asp:Label ID="CodigoIdEstadoFinancieroArchivoLabel" runat="server" Text='<%# Bind("IdEstadoFinancieroArchivo") %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Nombre Estado Financiero" SortExpression="NombreEstadoFinanciero">
                            <ItemTemplate>
                                <asp:Label ID="ItemNombreEstadoFinancieroLabel" runat="server" Text='<%# Bind("NombreEstadoFinanciero") %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="Periodo" SortExpression="Periodo">
                            <ItemTemplate>
                                <asp:Label ID="ItemPeriodoLabel" runat="server" Text='<%# Bind("Periodo") %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>   
                        <asp:TemplateField HeaderText="Unidad Tiempo Periodo" SortExpression="DescripcionUnidadTiempoPeriodo">
                            <ItemTemplate>
                                <asp:Label ID="ItemDescripcionUnidadTiempoPeriodoLabel" runat="server" Text='<%# Bind("DescripcionUnidadTiempoPeriodo") %>'> 
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="Nombre Archivo" SortExpression="NombreArchivo">
                            <ItemTemplate>
                                <asp:Label ID="ItemNombreArchivoLabel" runat="server" Text='<%# Bind("NombreArchivo") %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>   
                        <asp:TemplateField HeaderText="Tipo Archivo" SortExpression="TipoArchivo">
                            <ItemTemplate>
                                <asp:Label ID="ItemTipoArchivoLabel" runat="server" Text='<%# Bind("TipoArchivo") %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>   
                        <asp:TemplateField HeaderText="Tamaño en Bytes" SortExpression="TamanoByteArchivo">
                            <ItemTemplate>
                                <asp:Label ID="ItemTamanoByteArchivoLabel" runat="server" Text='<%# Bind("TamanoByteArchivo") %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>   
                        <asp:TemplateField HeaderText="Fecha Archivo Carga" SortExpression="FechaArchivoCarga">
                            <ItemTemplate>
                                <asp:Label ID="ItemFechaArchivoCargaLabel" runat="server" Text='<%# Bind("FechaArchivoCarga") %>'> 
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>  
                        <asp:TemplateField HeaderText="Fecha Presentacion" SortExpression="FechaPresentacion">
                            <ItemTemplate>
                                <asp:Label ID="ItemFechaPresentacionLabel" runat="server" Text='<%# Bind("FechaPresentacion") %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>                                                                                                                                          
                        <asp:TemplateField HeaderText="Controles de Edición">
                            <ItemTemplate>
                                <asp:LinkButton ID="SelectDepartamentoLinkButton" runat="server" CommandName="Select" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' text="Descargar" />
                            </ItemTemplate>
                        </asp:TemplateField>                       
                    </columns>
        
                    <PagerSettings 
                        Position="TopAndBottom" 
                        Mode="Numeric"
                        pagebuttoncount="10"/>
            
                </asp:GridView>            

                <br />              
                
                
                <table>                    
                    <tr>
                        <td>
                            <br />
                            <%--<asp:Button ID="RechazadoInstitucionButton" runat="server" Text="Rechazado, devolver el proceso" ValidationGroup="ValidacionGeneral" OnClick="RechazadoInstitucionButton_Click" CssClass="ButtonNeutro"/>--%>
                            <asp:Button ID="RechazadoInstitucionButton" runat="server" Text="Rechazado, devolver el proceso" ValidationGroup="ValidacionGeneral" OnClick="RechazadoInstitucionButton_Click" CssClass="btn btn-primary"/>
                        </td>
                        <td>
                            <br />
                            <%--<asp:Button ID="AprobarInstitucionButton" runat="server" Text="Pasar de Estado Revisión Analista" ValidationGroup="ValidacionGeneral" OnClick="AprobarInstitucionButton_Click" CssClass="ButtonNeutro"/>--%>
                            <asp:Button ID="AprobarInstitucionButton" runat="server" Text="Pasar de Estado Revisión Analista" ValidationGroup="ValidacionGeneral" OnClick="AprobarInstitucionButton_Click" CssClass="btn btn-primary"/>
                            
                        </td>
                    </tr>
                </table>
                <br />  
                                                                                                                                                                                                                                                           
            </asp:View>  

            <asp:View ID="ArchivosAnexosView" runat="Server">
                <h2>Archivos Anexos</h2>
                <div>    
                    <br />                                                                                                                                                  

                    <asp:GridView ID="EstadosFinancierosArchivosAnexosGridView" 
                        autogeneratecolumns="False"
                        datakeynames="IdEntidad, IdEstadoFinanciero, Periodo, UnidadTiempoPeriodo" 
                        runat="server"
                        PersistedSelection="true"
                        AllowPaging="True"
                        ShowFooter="True"
                   CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
                        OnRowCommand="EstadosFinancierosArchivosAnexosGridView_RowCommand"
                        OnPageIndexChanging="EstadosFinancierosArchivosAnexosGridView_PageIndexChanging">
        
                        <columns>  
                            <asp:TemplateField HeaderText="Codigo Id Entidad" Visible="false" SortExpression="IdEntidad">
                                <ItemTemplate>
                                    <asp:Label ID="CodigoIdEntidadLabel" runat="server" Text='<%# Bind("IdEntidad") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>                    
                            <asp:TemplateField HeaderText="Codigo Id Estado Financiero" Visible="false" SortExpression="IdEstadoFinanciero">
                                <ItemTemplate>
                                    <asp:Label ID="CodigoIdEstadoFinancieroLabel" runat="server" Text='<%# Bind("IdEstadoFinanciero") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Codigo Periodo" Visible="false" SortExpression="Periodo">
                                <ItemTemplate>
                                    <asp:Label ID="CodigoPeriodoLabel" runat="server" Text='<%# Bind("Periodo") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Codigo Unidad Tiempo Periodo" Visible="false" SortExpression="UnidadTiempoPeriodo">
                                <ItemTemplate>
                                    <asp:Label ID="CodigoUnidadTiempoPeriodoLabel" runat="server" Text='<%# Bind("UnidadTiempoPeriodo") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Codigo Id Estado Financiero Archivo Anexo" Visible="false" SortExpression="IdEstadoFinancieroArchivoAnexo">
                                <ItemTemplate>
                                    <asp:Label ID="CodigoIdEstadoFinancieroArchivoAnexoLabel" runat="server" Text='<%# Bind("IdEstadoFinancieroArchivoAnexo") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Nombre Estado Financiero" SortExpression="NombreEstadoFinanciero">
                                <ItemTemplate>
                                    <asp:Label ID="ItemNombreEstadoFinancieroLabel" runat="server" Text='<%# Bind("NombreEstadoFinanciero") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="Periodo" SortExpression="Periodo">
                                <ItemTemplate>
                                    <asp:Label ID="ItemPeriodoLabel" runat="server" Text='<%# Bind("Periodo") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>   
                            <asp:TemplateField HeaderText="Unidad Tiempo Periodo" SortExpression="DescripcionUnidadTiempoPeriodo">
                                <ItemTemplate>
                                    <asp:Label ID="ItemDescripcionUnidadTiempoPeriodoLabel" runat="server" Text='<%# Bind("DescripcionUnidadTiempoPeriodo") %>'> 
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="Nombre Archivo" SortExpression="NombreArchivo">
                                <ItemTemplate>
                                    <asp:Label ID="ItemNombreArchivoLabel" runat="server" Text='<%# Bind("NombreArchivo") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>   
                            <asp:TemplateField HeaderText="Tipo Archivo" SortExpression="TipoArchivo">
                                <ItemTemplate>
                                    <asp:Label ID="ItemTipoArchivoLabel" runat="server" Text='<%# Bind("TipoArchivo") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>   
                            <asp:TemplateField HeaderText="Tamaño en Bytes" SortExpression="TamanoByteArchivo">
                                <ItemTemplate>
                                    <asp:Label ID="ItemTamanoByteArchivoLabel" runat="server" Text='<%# Bind("TamanoByteArchivo") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>   
                            <asp:TemplateField HeaderText="Fecha Archivo Carga" SortExpression="FechaArchivoCarga">
                                <ItemTemplate>
                                    <asp:Label ID="ItemFechaArchivoCargaLabel" runat="server" Text='<%# Bind("FechaArchivoCarga") %>'> 
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>  
                            <asp:TemplateField HeaderText="Fecha Presentacion" SortExpression="FechaPresentacion">
                                <ItemTemplate>
                                    <asp:Label ID="ItemFechaPresentacionLabel" runat="server" Text='<%# Bind("FechaPresentacion") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>                                                                                                                                          
                            <asp:TemplateField HeaderText="Controles de Edición">
                                <ItemTemplate>
                                    <asp:LinkButton ID="SelectDepartamentoLinkButton" runat="server" CommandName="Select" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' text="Descargar" />
                                </ItemTemplate>
                            </asp:TemplateField>                       
                        </columns>
        
                        <PagerSettings 
                            Position="TopAndBottom" 
                            Mode="Numeric"
                            pagebuttoncount="10"/>
            
                    </asp:GridView>            

                    <br />    


                </div> 
            </asp:View> 

            <asp:View ID="ArchivosPlantillasView" runat="Server">
                <h2>Plantillas Estados Financieros</h2> 
                <div>

                <br />                  

                <asp:GridView ID="EstadosFinancierosArchivosPlantillasGridView" 
                    autogeneratecolumns="False"
                    datakeynames="IdEstadoFinanciero, IdEstadoFinancieroArchivoPlantilla" 
                    runat="server"
                    PersistedSelection="true"
                    AllowPaging="True"
                    ShowFooter="True"
                   CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
                    OnRowCommand="EstadosFinancierosArchivoPlantillasGridView_RowCommand"
                    OnPageIndexChanging="EstadosFinancierosArchivosPlantillasGridView_PageIndexChanging">
        
                    <columns>                 
                        <asp:TemplateField HeaderText="Codigo Id Estado Financiero" Visible="false" SortExpression="IdEstadoFinanciero">
                            <ItemTemplate>
                                <asp:Label ID="CodigoIdEstadoFinancieroLabel" runat="server" Text='<%# Bind("IdEstadoFinanciero") %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Codigo Id Estado Financiero Archivo Plantilla" Visible="false" SortExpression="IdEstadoFinancieroArchivoPlantilla">
                            <ItemTemplate>
                                <asp:Label ID="CodigoIdEstadoFinancieroArchivoPlantillaLabel" runat="server" Text='<%# Bind("IdEstadoFinancieroArchivoPlantilla") %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Nombre Estado Financiero" SortExpression="NombreEstadoFinanciero">
                            <ItemTemplate>
                                <asp:Label ID="ItemNombreEstadoFinancieroLabel" runat="server" Text='<%# Bind("NombreEstadoFinanciero") %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nombre Archivo" SortExpression="NombreArchivo">
                            <ItemTemplate>
                                <asp:Label ID="ItemNombreArchivoLabel" runat="server" Text='<%# Bind("NombreArchivo") %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>   
                        <asp:TemplateField HeaderText="Tipo Archivo" SortExpression="TipoArchivo">
                            <ItemTemplate>
                                <asp:Label ID="ItemTipoArchivoLabel" runat="server" Text='<%# Bind("TipoArchivo") %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>      
                        <asp:TemplateField HeaderText="Fecha Archivo Carga" SortExpression="FechaArchivoCarga">
                            <ItemTemplate>
                                <asp:Label ID="ItemFechaArchivoCargaLabel" runat="server" Text='<%# Bind("FechaArchivoCarga") %>'> 
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>                                                                                                                                           
                        <asp:TemplateField HeaderText="Controles de Edición">
                            <ItemTemplate>
                                <asp:LinkButton ID="SelectDepartamentoLinkButton" runat="server" CommandName="Select" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' text="Descargar" />
                            </ItemTemplate>
                        </asp:TemplateField>                       
                    </columns>
        
                    <PagerSettings 
                        Position="TopAndBottom" 
                        Mode="Numeric"
                        pagebuttoncount="10"/>
            
                </asp:GridView>  

                </div> 
            </asp:View>  
            
            <asp:View ID="CorreoAutorizacionView" runat="Server">
                <h2>Correos de Autorización</h2>
                <div>

                <table>
                    <tr>
                        <td>
                            <asp:Label ID="CorreosAutorizacionLabel" runat="server" Text="Correos de Autorización: "></asp:Label>
                            <br/>
                        </td>   
                        <td>
                            <asp:FileUpload ID="CorreosAutorizacionFileUpload" runat="server" CssClass="FormatoTextBox"/>

                        </td>  
                    </tr>
                    <tr>
                        <td>
                            <br />
                            <%--<asp:Button ID="CorreosAutorizacionButton" runat="server" Text="Cargar Correo de Autorización" ValidationGroup="ValidacionGeneral" OnClick="CorreosAutorizacionButton_Click"  CssClass="ButtonNeutro" />--%>
                            <asp:Button ID="CorreosAutorizacionButton" runat="server" Text="Cargar Correo de Autorización" ValidationGroup="ValidacionGeneral" OnClick="CorreosAutorizacionButton_Click"  CssClass="btn btn-primary" />
                            
                        </td>
                        <td>

                        </td>
                    </tr>
                </table> 

                <br />
                    
                    <asp:GridView ID="CorreosAutorizacionAnexosGridView" 
                        autogeneratecolumns="False"
                        datakeynames="IdEntidad, IdEstadoFinanciero, Periodo, UnidadTiempoPeriodo" 
                        runat="server"
                        PersistedSelection="true"
                        AllowPaging="True"
                        ShowFooter="True"
                         CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
                        OnRowDeleting="EstadosFinancierosCorreosAutorizacionGridView_RowDeleting"                     
                        OnRowCommand="EstadosFinancierosCorreosAutorizacionGridView_RowCommand"
                        OnPageIndexChanging="EstadosFinancierosCorreosAutorizacionGridView_PageIndexChanging">
        
                        <columns>  
                            <asp:TemplateField HeaderText="Codigo Id Entidad" Visible="false" SortExpression="IdEntidad">
                                <ItemTemplate>
                                    <asp:Label ID="CodigoIdEntidadLabel" runat="server" Text='<%# Bind("IdEntidad") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>                    
                            <asp:TemplateField HeaderText="Codigo Id Estado Financiero" Visible="false" SortExpression="IdEstadoFinanciero">
                                <ItemTemplate>
                                    <asp:Label ID="CodigoIdEstadoFinancieroLabel" runat="server" Text='<%# Bind("IdEstadoFinanciero") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Codigo Periodo" Visible="false" SortExpression="Periodo">
                                <ItemTemplate>
                                    <asp:Label ID="CodigoPeriodoLabel" runat="server" Text='<%# Bind("Periodo") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Codigo Unidad Tiempo Periodo" Visible="false" SortExpression="UnidadTiempoPeriodo">
                                <ItemTemplate>
                                    <asp:Label ID="CodigoUnidadTiempoPeriodoLabel" runat="server" Text='<%# Bind("UnidadTiempoPeriodo") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Codigo Id Estado Financiero Archivo Anexo" Visible="false" SortExpression="IdEstadoFinancieroArchivoAnexo">
                                <ItemTemplate>
                                    <asp:Label ID="CodigoIdEstadoFinancieroArchivoAnexoLabel" runat="server" Text='<%# Bind("IdEstadoFinancieroArchivoAnexo") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Nombre Estado Financiero" SortExpression="NombreEstadoFinanciero">
                                <ItemTemplate>
                                    <asp:Label ID="ItemNombreEstadoFinancieroLabel" runat="server" Text='<%# Bind("NombreEstadoFinanciero") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="Periodo" SortExpression="Periodo">
                                <ItemTemplate>
                                    <asp:Label ID="ItemPeriodoLabel" runat="server" Text='<%# Bind("Periodo") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>   
                            <asp:TemplateField HeaderText="Unidad Tiempo Periodo" SortExpression="DescripcionUnidadTiempoPeriodo">
                                <ItemTemplate>
                                    <asp:Label ID="ItemDescripcionUnidadTiempoPeriodoLabel" runat="server" Text='<%# Bind("DescripcionUnidadTiempoPeriodo") %>'> 
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="Nombre Archivo" SortExpression="NombreArchivo">
                                <ItemTemplate>
                                    <asp:Label ID="ItemNombreArchivoLabel" runat="server" Text='<%# Bind("NombreArchivo") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>   
                            <asp:TemplateField HeaderText="Tipo Archivo" SortExpression="TipoArchivo">
                                <ItemTemplate>
                                    <asp:Label ID="ItemTipoArchivoLabel" runat="server" Text='<%# Bind("TipoArchivo") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>   
                            <asp:TemplateField HeaderText="Tamaño en Bytes" SortExpression="TamanoByteArchivo">
                                <ItemTemplate>
                                    <asp:Label ID="ItemTamanoByteArchivoLabel" runat="server" Text='<%# Bind("TamanoByteArchivo") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>   
                            <asp:TemplateField HeaderText="Fecha Archivo Carga" SortExpression="FechaArchivoCarga">
                                <ItemTemplate>
                                    <asp:Label ID="ItemFechaArchivoCargaLabel" runat="server" Text='<%# Bind("FechaArchivoCarga") %>'> 
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>                                                                                                                                          
                            <asp:TemplateField HeaderText="Controles de Edición">
                                <ItemTemplate>
                                    <asp:LinkButton ID="DeleteButton" runat="server" CausesValidation="False"
                                        CommandName="Delete" Text="Eliminar"
                                        OnClientClick="return confirm('Está seguro que desea eliminar este registro?');">
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="SelectDepartamentoLinkButton" runat="server" CommandName="Select" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' text="Descargar" />
                                </ItemTemplate>
                            </asp:TemplateField>                       
                        </columns>
        
                        <PagerSettings 
                            Position="TopAndBottom" 
                            Mode="Numeric"
                            pagebuttoncount="10"/>
            
                    </asp:GridView>                    

                </div> 
            </asp:View>                        
                    
  
        </asp:MultiView>

        <asp:TextBox ID="txtError" runat="server" TextMode="multiline" Width="825" rows="10" ForeColor="#FF3300" Enabled="false" Visible="false"></asp:TextBox>
    </asp:Panel> 

</asp:Content>