﻿<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="CargaEntidad.aspx.cs" Inherits="Presentacion.Consolidacion.CargaEntidad" %>

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
<asp:Content ID="Content4" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Contenido" runat="server">
    
    <br />
    <asp:Label ID="Label1" runat="server" Text="Carga de Información por Parte de la Entidad."></asp:Label>

    <table>
    <tr>
        <td>
            <asp:LinkButton ID="ViewCargaEntidadLinkButton" runat="server" 
                    onclick="ViewCargaEntidadLinkButton_Click" Visible="false">Carga Entidad</asp:LinkButton>
        </td>
        <td>
            <asp:Label ID="SeparadorCargaEntidadArchivosAnexosLabel" runat="server" Text="|" Visible="false"></asp:Label>
        </td>
        <td>
                <asp:LinkButton ID="ViewArchivosAnexosLinkButton" runat="server" 
                        onclick="ViewArchivosAnexosLinkButton_Click" Visible="false">Archivos Anexos</asp:LinkButton> 
        </td>   

        <td>
            <asp:Label ID="SeparadorArchivosAnexosArchivosPlantillasLabel" runat="server" Text="|" Visible="false"></asp:Label>
        </td>
        <td>
                <asp:LinkButton ID="ViewArchivosPlantillasLinkButton" runat="server" 
                        onclick="ViewArchivosPlantillasLinkButton_Click" Visible="false">Archivos Plantillas</asp:LinkButton> 
        </td>  

        <td>
            <asp:Label ID="SeparadorCargaEntidadCorreosAutorizacionLabel" runat="server" Text="|" Visible="false"></asp:Label>
        </td>
        <td>
                <asp:LinkButton ID="ViewCorreosAutorizacionLinkButton" runat="server" 
                        onclick="ViewCorreosAutorizacionLinkButton_Click" Visible="false">Correos de Autorización</asp:LinkButton> 
        </td>            
    </tr>
    </table>
    <br/>

    <div class="col-md-12">
        <div class="col-md-6">
            <div class="col-md-6"><asp:Label ID="AmbitoConsolidacionLabel" runat="server" Text="Ámbito de Consolidación: "></asp:Label></div>
            <div class="col-md-9">
                <asp:DropDownList ID="AmbitoConsolidacionDropDownList" runat="server" AutoPostBack="true" 
                    OnDataBound="AmbitoConsolidacionDropDownList_Primero" 
                    DataTextField="NomCorto" DataValueField="IdAmbitoConsolidacion" CssClass="FormatoDropDownList"
                    onselectedindexchanged="AmbitoConsolidacionDropDownList_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
        </div>
        <div class="col-md-6">
            <div class="col-md-6"> <asp:Label ID="CatalogoEntidadesLabel" runat="server" Text="Entidad: "></asp:Label></div>
            <div class="col-md-9">
                 <asp:DropDownList ID="CatalogoEntidadesDropDownList" runat="server" CssClass="FormatoDropDownList"
                    DataTextField="NomUnidad" DataValueField="IdUnidadConsolidacion">
                </asp:DropDownList>
            </div>
        </div>
        <div class="col-md-6">
            <div class="col-md-6"><asp:Label ID="PeriodoLabel" runat="server" Text="Año: "></asp:Label></div>
            <div class="col-md-9">
                <asp:DropDownList ID="PeriodoDropDownList" runat="server"
                    OnDataBound="PeriodoDropDownList_Primero" CssClass="FormatoDropDownList"
                    DataTextField="Fecha" DataValueField="IdFecha">
                </asp:DropDownList>
            </div>
        </div>
        <div class="col-md-6">
            <div class="col-md-6"><asp:Label ID="UnidadTiempoPeriodoLabel" runat="server" Text="Unidad de Tiempo Periodo: "></asp:Label></div>
            <div class="col-md-9">
                <asp:DropDownList ID="UnidadTiempoPeriodoDropDownList" runat="server"
                    OnDataBound="CatalogoUnidadTiempoPeriodoDropDownList_Primero" CssClass="FormatoDropDownList"
                    DataTextField="Descripcion" DataValueField="UnidadTiempoPeriodo">
                </asp:DropDownList>
            </div>
        </div>
        <div class="col-md-12" style="text-align:center;"><asp:Button ID="InfoCargadaEstadosFinancierosArchivosButton" runat="server" Text="Ver Información Cargada" OnClick="InfoCargadaEstadosFinancierosArchivosButton_Click" CssClass="ButtonNeutro" Width="200px"/></div>
    </div>

    <asp:Panel id="CargaEntidadPanel" HorizontalAlign="Left" runat="Server">  
        <asp:MultiView id="CargaEntidadMultiView" ActiveViewIndex="0" runat="Server">
            <asp:View id="CargaEntidadView" runat="Server"> 
                <br/>
                <table>

                    <tr>
                        <td>
                            <%--<asp:Label ID="FechaPresentacionLabel" runat="server" Text="Fecha de Presentación: "></asp:Label>--%>
                            <br/>

                        </td>   
                        <td>
                            <%--<asp:TextBox ID="FechaPresentacionTextBox" runat="server" Enabled="false"></asp:TextBox>--%>
                        </td> 
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="FlujoEfectivoLabel" runat="server" Text="Estado de Flujo Efectivo: "></asp:Label>
                            <br/>

                        </td>   
                        <td>
                            <asp:FileUpload ID="FlujoEfectivoFileUpload" runat="server"/>
                        </td>  
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="CambioPatrimonioNetoLabel" runat="server" Text="Estado de Cambio de Patrimonio Neto: "></asp:Label>
                            <br/>                            

                        </td>   
                        <td>
                            <asp:FileUpload ID="CambioPatrimonioNetoFileUpload" runat="server"/>
                        </td>  
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="BalanceComprobacionLabel" runat="server" Text="Balance de Comprobación: "></asp:Label>
                            <br/>

                        </td>
                        <td>
                            <asp:FileUpload ID="BalanceComprobacionFileUpload" runat="server"/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="EstadoVariosLabel" runat="server" Text="Estados Varios: "></asp:Label>
                            <br/>
                        </td>
                        <td>
                            <asp:FileUpload ID="EstadoVariosFileUpload" runat="server"/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="NotasEstadosFinancierosLabel" runat="server" Text="Notas de los Estados Financieros: "></asp:Label>
                            <br/>
                        </td>
                        <td>
                            <asp:FileUpload ID="NotasEstadosFinancierosFileUpload" runat="server"/>
                        </td>   
                    </tr>
                    <tr>
                        <td>
                            <br />
                            <asp:Button ID="CargarArchivosButton" runat="server" Text="Cargar Archivos" ValidationGroup="ValidacionGeneral" OnClick="CargarArchivosButton_Click" CssClass="ButtonNeutro" Width="200px"/>
                        </td>
                        <td>

                        </td>
                    </tr>
                </table>      
                <br />                                                                                                                                                  

                <asp:GridView ID="EstadosFinancierosArchivosGridView" 
                    CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
                    autogeneratecolumns="False"
                    datakeynames="IdEntidad, IdEstadoFinanciero, Periodo, UnidadTiempoPeriodo" 
                    runat="server"
                    PersistedSelection="true"
                    AllowPaging="True"
                    ShowFooter="True"

                    OnRowDeleting="EstadosFinancierosArchivosGridView_RowDeleting"                     
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

                <br />              
                
                
                <table>                    
                    <tr>
                        <td>
                            <br />
                            <asp:Button ID="RevisionInstitucionButton" runat="server" Text="Pasar de Estado Revisión en Institución" ValidationGroup="ValidacionGeneral" OnClick="RevisionInstitucionButton_Click" CssClass="ButtonNeutro" Width="400px"/>

                        </td>
                        <td>

                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                            <asp:Button ID="RechazadoInstitucionButton" runat="server" Text="Rechazado, devolver el proceso" ValidationGroup="ValidacionGeneral" OnClick="RechazadoInstitucionButton_Click" CssClass="ButtonNeutro" Width="300px"/>

                        </td>
                        <td>
                            <br />
                            <asp:Button ID="AprobarInstitucionButton" runat="server" Text="Pasar de Estado Revisión Analista" ValidationGroup="ValidacionGeneral" OnClick="AprobarInstitucionButton_Click" CssClass="ButtonNeutro" Width="300px"/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                            <asp:Button ID="RechazadoAnalistaButton" runat="server" Text="Rechazado, devolver el proceso" ValidationGroup="ValidacionGeneral" OnClick="RechazadoAnalistaButton_Click" CssClass="ButtonNeutro" Width="300px"/>

                        </td>
                        <td>
                            <br />
                            <asp:Button ID="AprobarAnalistaButton" runat="server" Text="Aprobar proceso" ValidationGroup="ValidacionGeneral" OnClick="AprobarAnalistaButton_Click" CssClass="ButtonNeutro" Width="300px"/>
                        </td>
                    </tr>
                </table>
                <br />  
                                                                                                                                                                                                                                                           
            </asp:View>  

            <asp:View ID="ArchivosAnexosView" runat="Server">
                <div>

                    <table>
                        <tr>
                            <td>
 
                               <asp:Label ID="EstadoFinancieroLabel" runat="server" Text="Estado Financiero: "></asp:Label>
                                <br/>
                                <asp:DropDownList ID="EstadoFinancieroDropDownList" runat="server"
                                    OnDataBound="EstadoFinancieroDropDownList_Primero" CssClass="FormatoDropDownList"
                                    DataTextField="NombreEstadoFinanciero" DataValueField="IdEstadoFinanciero">
                                </asp:DropDownList>
                            </td>
                            <td>

                                <br/>
                            </td>  
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="ArchivosAnexosLabel" runat="server" Text="Archivos Anexos: "></asp:Label>
                                <br/>

                            </td>
                            <td>
                                <asp:FileUpload ID="ArchivoAnexoFileUpload" runat="server"/>
                            </td>  
                        </tr>
                        <tr>
                            <td>
                                <br />
                                <asp:Button ID="CargarArchivosAnexosButton" runat="server" Text="Cargar Archivos Anexos" ValidationGroup="ValidacionGeneral" OnClick="CargarArchivosAnexosButton_Click" CssClass="ButtonNeutro" Width="300px"/>

                            </td>
                            <td>

                            </td>
                        </tr>
                    </table>      
                    <br />                                                                                                                                                  

                    <asp:GridView ID="EstadosFinancierosArchivosAnexosGridView" 
                        autogeneratecolumns="False"
                        datakeynames="IdEntidad, IdEstadoFinanciero, Periodo, UnidadTiempoPeriodo" 
                        runat="server"
                        PersistedSelection="true"
                        AllowPaging="True"
                        ShowFooter="True"
                        CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
                        OnRowDeleting="EstadosFinancierosArchivosAnexosGridView_RowDeleting"                     
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

                    <br />    


                </div> 
            </asp:View> 

            <asp:View ID="ArchivosPlantillasView" runat="Server">
                <div>

                <table>

                    <tr>
                        <td>
 
                            <asp:Label ID="EstadoFinancieroPlantillasLabel" runat="server" Text="Estado Financiero: "></asp:Label>
                            <br/>
                            <asp:DropDownList ID="EstadoFinancieroPlantillasDropDownList" runat="server"
                                OnDataBound="EstadoFinancieroPlantillasDropDownList_Primero"  CssClass="FormatoDropDownList"
                                DataTextField="NombreEstadoFinanciero" DataValueField="IdEstadoFinanciero">
                            </asp:DropDownList>
                        </td>
                        <td>

                            <br/>
                        </td>  
                    </tr>

                    <tr>
                        <td>
                            <asp:Label ID="ArchivosPlantillasLabel" runat="server" Text="Archivos Plantillas de Estados Financieros: "></asp:Label>
                            <br/>
                        </td>   
                        <td>
                            <asp:FileUpload ID="ArchivosPlantillasFileUpload" runat="server"/>

                        </td>  
                    </tr>
                    <tr>
                        <td>
                            <br />
                            <asp:Button ID="ArchivosPlantillasButton" runat="server" Text="Cargar Plantilla de Estados Financieros" ValidationGroup="ValidacionGeneral" OnClick="ArchivosPlantillasButton_Click" CssClass="ButtonNeutro" Width="300px"/>

                        </td>
                        <td>

                        </td>
                    </tr>
                </table> 

                <br />                  

                <asp:GridView ID="EstadosFinancierosArchivosPlantillasGridView" 
                    autogeneratecolumns="False"
                    datakeynames="IdEstadoFinanciero, IdEstadoFinancieroArchivoPlantilla" 
                    runat="server"
                    PersistedSelection="true"
                    AllowPaging="True"
                    ShowFooter="True"
                    CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
                    OnRowDeleting="EstadosFinancierosArchivosPlantillasGridView_RowDeleting"                     
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
            
            <asp:View ID="CorreoAutorizacionView" runat="Server">
                <div>

                <table>
                    <tr>
                        <td>
                            <asp:Label ID="CorreosAutorizacionLabel" runat="server" Text="Correos de Autorización: "></asp:Label>
                            <br/>
                        </td>   
                        <td>
                            <asp:FileUpload ID="CorreosAutorizacionFileUpload" runat="server"/>

                        </td>  
                    </tr>
                    <tr>
                        <td>
                            <br />
                            <asp:Button ID="CorreosAutorizacionButton" runat="server" Text="Cargar Correo de Autorización" ValidationGroup="ValidacionGeneral" OnClick="CorreosAutorizacionButton_Click" CssClass="ButtonNeutro" Width="300px"/>

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

        <asp:ListBox ID="lsbError" runat="server" ForeColor="#FF3300" CssClass="FormatoTextBox"></asp:ListBox>
    </asp:Panel> 

</asp:Content>
