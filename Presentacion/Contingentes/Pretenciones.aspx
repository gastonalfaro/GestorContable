<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="Pretenciones.aspx.cs" Inherits="Presentacion.Contingentes.Pretenciones" %>


<%@ PreviousPageType VirtualPath="NuevoExpediente.aspx" %> 
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Assembly="eWorld.UI.Compatibility, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2" Namespace="eWorld.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2" Namespace="eWorld.UI" TagPrefix="ew" %>
<asp:Content ID="Content1" ContentPlaceHolderID="encabezado" runat="server">
      <script src="/Compartidas/rmm-js/chosen.jquery.js" type="text/javascript"></script> 
</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenido" runat="server">
    <div style="display:inline-block;">
    <div class="col-md-12"><strong><span class="auto-style4">Pretensión Inicial</span></strong></div>
    <div class="col-md-12" style="margin-bottom:10px;">
         <p class="auto-style99" style="font-size:small">* Indica que es un campo requerido.</p> 
    </div>
    <div  class="col-md-6">
        <div class="row">
            <div class="col-md-5"><span class="auto-style48">Número Expediente </span> <span class="auto-style12">*</span></div>
            <div class="col-md-7">
                <asp:DropDownList ID="DDLExpedientes" runat="server" AppendDataBoundItems="true" class="chzn-select" data-placeholder="--- Elegir Número Expediente ---" OnSelectedIndexChanged="DDLExpedientes_SelectedIndexChanged" AutoPostBack="true" Width="100%">
                 <asp:ListItem Selected="True" Value="0">--- Elegir Expediente ---</asp:ListItem>
                </asp:DropDownList><asp:Label ID="lblNumExpediente" Visible="false" runat="server" style="font-size: small"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-md-5"><span class="auto-style48">Monto principal</span></div>
            <div class="col-md-7">
                    <asp:TextBox ID="txtMontoPretension" runat="server" onkeypress="return AceptarSoloNumeros(event)" CssClass="FormatoTextBox" OnTextChanged="txtMontoPretension_TextChanged" AutoPostBack="true" MaxLength="26"></asp:TextBox>
            </div>
        </div>         
        <div class="row">
            <div class="col-md-5"><span class="auto-style48">Moneda</span><span class="auto-style12">*</span></div>
            <div class="col-md-7"> 
                <asp:DropDownList ID="DDLMoneda" AutoPostBack="true" AppendDataBoundItems="true" runat="server" CssClass="FormatoDropDownList" OnSelectedIndexChanged="DDLMoneda_SelectedIndexChanged">
                 
                    <asp:ListItem Selected="True" Value="0">--- Elegir Moneda ---</asp:ListItem>

                </asp:DropDownList>
            
            </div>
        </div>
        <div class="row">
            <div class="col-md-5"><span class="auto-style48">Monto principal colones</span></div>
            <div class="col-md-7"> <asp:TextBox ID="txtPretensionColones" runat="server" ReadOnly="True" style="background-color: #CCCCCC" CssClass="FormatoTextBox" ></asp:TextBox></div>
        </div>
    </div>
     <div  class="col-md-6">

         <div class="row">
            <div class="col-md-5"><span class="auto-style48">
                <asp:Label runat="server" ID="lbl_Fecha" Text="Posible Fecha"></asp:Label></span></div>
            <div class="col-md-7">
                 <asp:TextBox ID="txtFchVigencia" runat="server" CssClass="js-date-picker FormatoTextBox"></asp:TextBox>
                                
                   
            </div>
        </div>
         <div class="row">
            <div class="col-md-5" hidden="hidden"><span class="auto-style48">Monto Posible Rembolso Recursos</span> </div>
            <div class="col-md-7">
                <asp:TextBox ID="txtMontoRembolsoPos" onkeypress="return AceptarSoloNumeros(event)" runat="server" style="margin-top: 0px" Visible="False" CssClass="FormatoTextBox"></asp:TextBox>
                <asp:RangeValidator ID="rvMontoRembolsoPos"   runat="server" ControlToValidate="txtMontoRembolsoPos" ErrorMessage="Error" MaximumValue="9999" MinimumValue="0" SetFocusOnError="True" Type="Double" style="font-size: xx-small; color: #FF0000"></asp:RangeValidator>
            </div>
        </div>
         <div class="row">
            <div class="col-md-5"><span class="auto-style48">Valor Presente</span></div>
            <div class="col-md-7"> <asp:TextBox ID="txtValorPresente" runat="server" ReadOnly="True" CssClass="FormatoTextBox" BackColor="#CCCCCC" ></asp:TextBox></div>
        </div>
    </div>
    <table align="center" style="width:100%">
        <tr>
            <td class="auto-style52">
                <asp:TextBox ID="txtTiempo" runat="server" onkeypress="return AceptarSoloNumeros(event)" Height="16px" Width="121px" ReadOnly="True" Visible="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style23" colspan="12"><strong>Indicadores Economicos</strong></td>
        </tr>
        <tr>
            <td class="auto-style28" colspan="11">
                <span class="auto-style22"></span><strong>TBP&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</strong>&nbsp;&nbsp;&nbsp;&nbsp; <asp:TextBox ID="txtTBP" runat="server" BorderStyle="None" CssClass="auto-style22" Enabled="False" ForeColor="Red" Height="20px" Width="67px" style="margin:auto;"></asp:TextBox>
            </td>
            <td class="auto-style8"></td>
        </tr>
        <tr>
            <td class="auto-style29" colspan="11">
                <span class="auto-style56">
                <strong>Compra&nbsp;&nbsp;&nbsp;
                <asp:Image ID="Image5" runat="server" Height="16px" ImageUrl="~/Compartidas/imagenes/1446084309_49.png" Width="16px" />
                </strong>&nbsp; &nbsp; </span><asp:TextBox ID="txtCompra" runat="server" BorderStyle="None" CssClass="auto-style47" Enabled="False" ForeColor="Red" Height="16px" Width="66px"></asp:TextBox>
                <br />
            </td>
            <td class="auto-style30">
                <%--<asp:SqlDataSource ID="SqlDSExpedientes" runat="server" ConnectionString="<%$ ConnectionStrings:GestNICSPDEVConnectionString %>" SelectCommand="SELECT IdExpediente, IdSociedadGL, ExpedienteOrigen, IdExpediente+'-'+TipoExpediente as NomExpediente, EstadoExpediente, FechaDemanda, TipoProcesoExpediente, MotivoDemanda, MonedaPretension, TipoCambio, MontoPretension, MontoPretensionColones, EstadoPretension, PosibleFechEntradaRecursos, MontoValorPresente, EstadoProcesal, MontoPosibleReembolso FROM co.Expedientes WHERE co.Expedientes.EstadoExpediente='Activo'"></asp:SqlDataSource>--%>
                </td>
        </tr>
        <tr>
            <td class="auto-style31" colspan="11"><strong>Venta&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;<asp:Image ID="Image8" runat="server" Height="16px" ImageUrl="~/Compartidas/imagenes/1446084309_49.png" Width="16px" />
                &nbsp;&nbsp;&nbsp;&nbsp;
<asp:TextBox ID="txtVenta" runat="server" BorderStyle="None" CssClass="auto-style47" Enabled="False" ForeColor="Red" Height="16px" Width="69px"></asp:TextBox>
                </td>
            <td class="auto-style32" style="text-align: left">
                </td>
        </tr>
        <tr>
            <td colspan="11" class="auto-style27"><strong>EUR&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                <asp:Image ID="Image7" runat="server" Height="16px" ImageUrl="~/Compartidas/imagenes/1446084323_05.png" Width="16px" />
                &nbsp;</strong>&nbsp;&nbsp;
                <asp:TextBox ID="txtEuro" runat="server" BorderStyle="None" CssClass="auto-style47" Enabled="False" ForeColor="Red" Height="16px" Width="71px"></asp:TextBox>
            </td>
            <td style="text-align: right" class="auto-style22"></span></td>
        </tr>
        <tr>
            <td colspan="11" class="auto-style27">&nbsp;</td>
            <td style="text-align: right" class="auto-style22">&nbsp;</td>
        </tr>
        </table>
    <div class="col-md-12"><strong>Observaciones </strong>
        <CKEditor:CKEditorControl ID="CKEditorObservaciones" runat="server" Width="100%" Height="156px" style="text-align: left"></CKEditor:CKEditorControl>

    </div>
    <div class="col-md-12">
        <strong>Adjuntar Archivos </strong>
         <asp:FileUpload ID="FileUpload1" runat="server" Font-Bold="True" CssClass="FormatoTextBox" maxRequestLength="1073741824" />
        <asp:Button ID="btnUpload" runat="server" OnClick="btnUpload_Click1" Text="Subir Archivo" CssClass="ButtonNeutro"/>
               
    </div>
    <div class="col-md-12">
        
                    <asp:GridView ID="gvFiles" DataKeyNames="IdArchivo" runat="server" Width="722px" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" SortExpression="IdArchivo" OnRowDeleting="gvFiles_RowDeleting"
                          CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                       
                        <Columns>
                            <asp:TemplateField HeaderText="Adjunto">
                                <ItemTemplate>
                                    <img alt="" src="../Compartidas/imagenes/24x24-paperclip.png" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="IdArchivo" HeaderText="" Visible="false"/>
                            <asp:BoundField DataField="NombreArchivo" HeaderText="Nombre de Archivo" />
                            <asp:BoundField DataField="Tamano" HeaderText="Tamaño" />
                            <asp:BoundField DataField="IdExpediente" HeaderText="IdExpediente" visible="false"/>
                            
                            <asp:TemplateField HeaderText="FchModifica" SortExpression="FchModifica" Visible="false"> 
                                <ItemTemplate>
                                    <asp:Label ID="lblFchModifica" runat="server"  Text='<%# Bind("FchModifica") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                   <%--<asp:HyperLink runat="server"
                                   NavigateUrl="ObtenerArchivo.aspx?id=<%# Eval("IdArchivo") %>&Flag=Pretension"
                                    Text="Descargar"></asp:HyperLink> --%>
                                     <a href='ObtenerArchivo.aspx?id=<%# Eval("IdArchivo") %>&Flag=Pretension' target="_self">Descargar</a>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="" ShowHeader="False" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkEliminar" runat="server" CausesValidation="False" CommandArgument='<%#Eval("IdArchivo") + ";" +Eval("IdExpediente")%>' CommandName="Delete" Text="Eliminar"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                        </Columns>
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                    </asp:GridView>
    </div>
       
    <div class="col-md-12">
        <div style="text-align:center">
        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="ButtonNeutro" OnClick="btnGuardar_Click" ToolTip="Guardar datos " />
     
        </div>
    </div>
     </div>
</asp:Content>
<asp:Content ID="Content4" runat="server" contentplaceholderid="ContenidoJS">
    <script src="Scripts/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript">
            $(document).ready(function () {
                SearchText();
            });
            function SearchText() {
                $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
            }

    </script> 
    <style type="text/css">
         
        .FormatoTextBox, .FormatoDropDownList { width:100%!important; }
        .auto-style1 {
            font-size: xx-small;
        }
        .auto-style2 {
            font-size: xx-small;
            color: #FF0000;
        }
        .auto-style3 {
            font-size: medium;
        }
        .auto-style4 {
            font-size: medium;
            color: #3366CC;
        }
        .auto-style7 {
            height: 18px;
            background-color: #FFFFFF;
            width: 139px;
        }
        .auto-style8 {
            height: 17px;
            background-color: #FFFFFF;
        }
        .auto-style12 {
            color: #FF3300;
            font-size: medium;
        }
        .auto-style22 {
            background-color: #FFFFFF;
        }
        .auto-style23 {
        }
        .auto-style24 {
            width: 139px;
        }
        .GridViewStyle {
            text-align: left;
        }
        .auto-style25 {
            font-size: medium;
            color: #3366CC;
            }
        .auto-style26 {
        }
        .auto-style27 {
            background-color: #CCCCCC;
        }
        .auto-style28 {
            height: 17px;
            background-color: #CCCCCC;
        }
        .auto-style29 {
            background-color: #CCCCCC;
            height: 15px;
        }
        .auto-style30 {
            background-color: #FFFFFF;
            height: 15px;
        }
        .auto-style31 {
            background-color: #CCCCCC;
            height: 35px;
        }
        .auto-style32 {
            background-color: #FFFFFF;
            height: 35px;
        }
        .auto-style47 {}
        .auto-style48 {
            font-size: small;
            color: #000000;
        }
     </style>
</asp:Content>

