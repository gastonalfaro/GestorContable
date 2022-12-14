<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="ReportesContingentes.aspx.cs" Inherits="Presentacion.Contingentes.ReportesContingentes" %>
<%@ Register Assembly="eWorld.UI.Compatibility, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content3" ContentPlaceHolderID="encabezado" runat="server">
     <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoJS" runat="server">

    <style type="text/css">
        
        .auto-style1 {
            height: 19px;
            text-align: right;
        }
        .auto-style2 {
            height: 19px;
            width: 86px;
        }
        .auto-style3 {
            width: 86px;
        }
        .auto-style4 {
            height: 19px;
            width: 384px;
        }
        .auto-style5 {
            width: 384px;
            text-align: left;
        }
        .auto-style6 {
            height: 19px;
            text-align: right;
            width: 261px;
        }
        .auto-style9 {
            width: 261px;
        }
        .auto-style10 {
            width: 384px;
        }
    .auto-style11 {
        height: 19px;
        text-align: right;
        width: 79px;
    }
    .auto-style12 {
        width: 79px;
    }
    </style>
</asp:Content>


<asp:Content ID="Content4" ContentPlaceHolderID="Contenido" runat="server">
    <link href="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/js/bootstrap.min.js"></script>
<link href="https://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/css/bootstrap-multiselect.css" rel="stylesheet" type="text/css" />
<script src="https://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/js/bootstrap-multiselect.js" type="text/javascript"></script>
<script type="text/javascript">
    $(function () {
        $('[id*=lstMinisterios]').multiselect({
            includeSelectAllOption: true            
        });
    });
</script>


 <table>
    <tr>
        <td>
            <asp:LinkButton ID="ViewParametrosLinkButton" runat="server" 
                    Visible="false">Parametros</asp:LinkButton>
        </td>
        <td>
            <asp:Label ID="SeparadorParametrosReporteLabel" runat="server" Text="|" Visible="false"></asp:Label>
        </td>
        <td>
                <asp:LinkButton ID="ViewReporteLinkButton" runat="server" 
                         Visible="false">Reporte</asp:LinkButton> 
        </td>       
    </tr>
    </table>

    <br />

    <asp:Panel id="CambioPatrimonioNetoAgregadoPanel" HorizontalAlign="Left" runat="Server">  
        <asp:MultiView id="ReportesContingentesMultiView" ActiveViewIndex="0" runat="Server">
            <asp:View id="ParametrosReportesContingentesView" runat="Server">                                                                                         
                     <%--<div  class="col-md-6">
                        <div class="col-md-5"><strong>Fecha Inicio :</strong></div>
                        <div class="col-md-7"><asp:TextBox ID="txtFechaInicio" runat="server" CssClass="js-date-picker FormatoTextBox"></asp:TextBox></div>
                    </div>                
                    <div  class="col-md-6">
                        <div class="col-md-5"><strong>Fecha Fin:</strong></div>
                        <div class="col-md-7">                            
                                <asp:TextBox ID="txtFechaFin" runat="server" CssClass=" js-date-picker FormatoTextBox"></asp:TextBox>
                         </div>
                    </div>--%>
                    <br/>
                    <br/>
                    <%--<div  class="col-md-6">
                        <div class="col-md-5"><strong>Cedula:</strong></div>
                        <div class="col-md-7">                            
                                <asp:TextBox ID="txtCedula" runat="server" CssClass="FormatoTextBox"></asp:TextBox>
                            </div>
                    </div>
                    <br/>
                        <div class="col-md-6">
                             <div class="col-md-5">
                                <asp:Label ID="lblMinisterios" runat="server" Text="Ministerios" Visible="false" Style="text-align: left"></asp:Label></div>
                            <div class="col-md-7">
                                <asp:ListBox ID="lstMinisterios" runat="server" CssClass="form-control" SelectionMode="Multiple" Visible="true" Height="200px" Width="200px" AutoPostBack="true"></asp:ListBox>                                                                                                
                                <%--<asp:HiddenField ID="HiddenField1" runat="server" />--%>
                            <%--</div>
                        </div>--%>
                    <br />
                    <br/>
                    <%--<div class="col-md-6">                                               
                        <div class="col-md-5"><asp:Label ID="etqMinisterios" runat="server" Text="Ministerios" Visible="true" Style="text-align: left"></asp:Label></div>
                        <div class="col-md-7"><asp:DropDownList ID="ddlMinisterios" runat="server" CssClass="form-control mdb-select md-form" Visible="true"><asp:ListItem Value="">-- Seleccione Opción --</asp:ListItem></asp:DropDownList></div>                                                                     
                    </div>                
                    <br />
                    <div  class="col-md-6" id="divEstado" runat="server">
                        <div class="col-md-5"><asp:Label ID="lblEstadoRes" runat="server" Text="Estado Resolución" Visible="False"></asp:Label></div>
                        <div class="col-md-7"> <asp:DropDownList ID="ddlEstadoRes" runat="server" Visible="False" CssClass="FormatoDropDownList">
                                <asp:ListItem>Provisional 1</asp:ListItem>
                                <asp:ListItem>Provisional 2</asp:ListItem>
                                <asp:ListItem>En Firme</asp:ListItem>
                            </asp:DropDownList>

                        </div>
                    </div>--%>
                
                <%--<div style="display:inline-block;" id="divBitacora"  visible="false" runat="server">
                     <div class="col-md-6">
                        <div class="col-md-5"><strong>Id Operación:</strong></div>
                        <div class="col-md-7"><asp:DropDownList ID="ddlIdOperacion" runat="server" CssClass="chzn-select FormatoDropDownList"><asp:ListItem Value="">-- Seleccione Opción --</asp:ListItem></asp:DropDownList></div>
                    </div>
                    <div class="col-md-6">
                        <div class="col-md-5"><strong>Id Sociedad GL:</strong></div>
                        <div class="col-md-7"><asp:DropDownList ID="ddlIdSociedad" runat="server" CssClass="chzn-select FormatoDropDownList"><asp:ListItem Value="">-- Seleccione Opción --</asp:ListItem></asp:DropDownList></div>
                    </div>
                     <div class="col-md-6">
                        <div class="col-md-5"><strong>Id Transacción:</strong></div>
                        <div class="col-md-7"><asp:DropDownList ID="ddlIdTransaccion" runat="server" CssClass="chzn-select FormatoDropDownList"><asp:ListItem Value="">-- Seleccione Opción --</asp:ListItem></asp:DropDownList></div>
                    </div>
                </div>
                <div class="col-md-12">
                    <div style="text-align:center"><asp:Button ID="btnReporte" runat="server" CssClass="ButtonNeutro" Text="Generar Reporte" Visible="True" Width="133px" /></div>
                </div>--%>
                <br />                                                                                                                                                                                                                                                         
            </asp:View>            
                    
            <asp:View ID="View1" runat="Server">
                <div>
                    <iframe height="800px" width="100%" src="../Compartidas/VisorReportes/VisorReportes.aspx"   frameborder="0">
                    </iframe>
                </div> 
            </asp:View>   
        </asp:MultiView>
        
        <asp:Label ID="lblError" runat="server" ForeColor="#FF3300"></asp:Label>
    </asp:Panel>







    </asp:Content>
