<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="Resoluciones.aspx.cs" Inherits="Presentacion.Contingentes.Resoluciones" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Assembly="eWorld.UI.Compatibility, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2" Namespace="eWorld.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2" Namespace="eWorld.UI" TagPrefix="ew" %>
<asp:Content ID="Content1" ContentPlaceHolderID="encabezado" runat="server">
</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="Enlaces" runat="server">
<%--</asp:Content>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenido" runat="server">
            
    <asp:Label ID="Label2" runat="server" Text="Cambio de Mes" Visible="false"></asp:Label>
    <asp:CheckBox ID="ckbNuevoMes" runat="server" Visible="false" OnCheckedChanged="ckbNuevoMes_CheckedChanged" AutoPostBack="true"/>

    <asp:Label ID="Label3" runat="server" Text="Cambio de Año" Visible="false"></asp:Label>
    <asp:CheckBox ID="ckbNuevoAno" runat="server" Visible="false" OnCheckedChanged="ckbNuevoAno_CheckedChanged" AutoPostBack="true"/>


            <div class="col-md-12"><strong style="color: #3366CC">Resoluciones de expediente</strong></div>
                <div class="col-md-6">Num. Expediente <p class="auto-style99">*</p>
                     <asp:DropDownList ID="DDLExpedientes" runat="server" AppendDataBoundItems="true" style="width:50%!important" data-placeholder="--- Elegir Número Expediente ---" class="chzn-select FormatoDropDownList" OnSelectedIndexChanged="DDLExpedientes_SelectedIndexChanged" AutoPostBack="true">
                    <%--<asp:ListItem Selected="True" Value="0">--- Elegir Expedientes ---</asp:ListItem>--%>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator id="rfvExpediente" 
                        ControlToValidate="DDLExpedientes" ErrorMessage="Seleccione un expediente. Campo requerido." ForeColor="Red" 
                        Display="Static" InitialValue="0" runat="server" style="font-size: xx-small"/>
                    <%--<asp:SqlDataSource ID="SqlDSExpedientes" runat="server" ConnectionString="<%$ ConnectionStrings:GestNICSPDEVConnectionString %>" SelectCommand="SELECT IdExpediente,'+pIdExpediente+' : '+TipoExpediente AS NomExpediente FROM co.Expedientes where co.Expedientes.EstadoExpediente='Activo'"></asp:SqlDataSource>--%>
                </div>
      <asp:UpdatePanel  ID="up_DatosPrincipales" runat="server" UpdateMode="Conditional">
        <ContentTemplate>  
       
       
                <div class="col-md-12">
                     Declarar sin lugar
                    &nbsp;<asp:CheckBox ID="chkCxPCaC" runat="server" OnCheckedChanged="chkCxPCaC_CheckedChanged" AutoPostBack="true"/>

                    <asp:Label ID="Label1" runat="server" Text="Declar como incobrable" Visible="false"></asp:Label>
                    <asp:CheckBox ID="ckbIncobrable" runat="server" Visible="false" OnCheckedChanged="ckbIncobrable_CheckedChanged" AutoPostBack="true"/>

                </div>
                <div  class="col-md-6">
                    <div class="row">
                        <div class="col-md-5">Tipo de Resolución<p class="auto-style99">*</p></div>
                        <div class="col-md-7">
                                <asp:DropDownList ID="ddlEstadoResol" runat="server" AppendDataBoundItems="true" CssClass="FormatoDropDownList" OnSelectedIndexChanged="ddlEstadoResol_SelectedIndexChanged" AutoPostBack="True">
                                    <asp:ListItem Selected="True" Value="0">--- Elegir Tipo de Resolución ---</asp:ListItem>
                                    <asp:ListItem Selected="False" Value="Provisional 1">Provisional 1</asp:ListItem>
                                    <asp:ListItem Value="Provisional 2">Provisional 2</asp:ListItem>
                                    <asp:ListItem Value="En Firme">En Firme</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator id="rfvEstadoResol" 
                                 ControlToValidate="ddlEstadoResol" ErrorMessage="Seleccione tipo de resolución.Campo requerido." ForeColor="Red" 
                                 Display="Static" InitialValue="0" runat="server" style="font-size: xx-small"/>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-5">Estado procesal<p class="auto-style99">*</p></div>
                        <div class="col-md-7"> 
                            <asp:DropDownList ID="DDLEstadoProcesal" runat="server" AppendDataBoundItems="True" onchange="SeleccionarEstadoProcesal()" ClientIDMode="Static" CssClass="FormatoDropDownList" OnSelectedIndexChanged="DDLEstadoProcesal_SelectedIndexChanged">                   
                            </asp:DropDownList><%--DataSourceID="SqlEstadoProcesal" DataTextField="NomOpcion" DataValueField="NomOpcion" --%>
                                      <%--<Items>
                                <asp:ListItem Text="--- Elegir Estado Procesal---" Value="0" />
                             </Items>--%>
                            <asp:RequiredFieldValidator id="rfvEstadoProcesal" 
                             ControlToValidate="DDLEstadoProcesal" ErrorMessage="Seleccione un estado de resolución.Campo requerido." ForeColor="Red" 
                             Display="Static" InitialValue="0" runat="server" style="font-size: xx-small"/>
               
                        <%--<asp:SqlDataSource ID="SqlEstadoProcesal" runat="server" ConnectionString="<%$ ConnectionStrings:GestNICSPDEVConnectionString %>" SelectCommand="SELECT IdCatalogo, IdOpcion, ValOpcion, NomOpcion, Estado, UsrCreacion, FchCreacion, UsrModifica, FchModifica FROM ma.OpcionesCatalogos WHERE (IdCatalogo = '34') AND Estado = 'A' "></asp:SqlDataSource>--%>
                  </div>
                    </div>
                    <div class="row">
                        <div class="col-md-5">Num. Resolución<p class="auto-style99">*</p></div>
                        <div class="col-md-7">
                              <asp:TextBox ID="txtResolucionNum" runat="server" CssClass="FormatoTextBox"></asp:TextBox>
                              
                            <asp:RequiredFieldValidator id="rfvResolucionNum" runat="server"
                              ControlToValidate="txtResolucionNum"
                              ErrorMessage="Número de resolución, es un campo requerido."
                              ForeColor="Red" style="font-size: xx-small">
                             </asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-5">Fecha de Resolución<p class="auto-style99">*</p></div>
                        <div class="col-md-7">
                           
                            <asp:TextBox ID="txtFechaResolucion" runat="server" CssClass="js-date-picker  FormatoTextBox"></asp:TextBox>

                    
                            <asp:RequiredFieldValidator id="rfvFechaRes" runat="server" ControlToValidate="txtFechaResolucion" ErrorMessage="Fecha de resolución, es un campo requerido." ForeColor="Red" style="font-size: xx-small"></asp:RequiredFieldValidator>
                
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-5"><asp:Label ID="lblFechaSalidaEntrada" runat="server">Posible fecha de entrada de recursos</asp:Label></div>
                        <div class="col-md-7">
                            <asp:TextBox ID="txtFechSalidaRecur" runat="server" CssClass="js-date-picker  FormatoTextBox"></asp:TextBox>
                        </div>
                    </div>
           
                </div>

                <asp:UpdatePanel ID="up_DatosMontos" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div  class="col-md-6">
                             <div id="div_Desembolso" class="row" runat="server">
                                <div class="col-md-5">Posible Monto de Reembolso</div>
                                <div class="col-md-7">
                                    <asp:TextBox ID="txtMontoPosDesembolso" runat="server" onkeypress="return AceptarSoloNumeros(event)" CssClass="FormatoTextBox" OnTextChanged="txtMontoPosDesembolso_TextChanged" AutoPostBack="true" MaxLength="26" ></asp:TextBox>
                    
                                </div>
                            </div>
                            <div class="row" style="padding-bottom: 1%;">
                                <div class="col-md-5">Monto Principal</div>
                                <div class="col-md-7">                      
                                    <asp:TextBox ID="txtMontoPrincipal" runat="server" onkeypress="return AceptarSoloNumeros(event)" OnTextChanged="txtMontoPrincipal_TextChanged" AutoPostBack="true" CssClass="FormatoTextBox" MaxLength="26"></asp:TextBox>
                 
                                </div>
                            </div>
                            <div class="row" runat="server" id="rIntereses">
                                <div class="col-md-5">Monto Intereses</div>
                                <div class="col-md-7">
                                    <asp:TextBox ID="txtMontoIntereses" runat="server" onkeypress="return AceptarSoloNumeros(event)" OnTextChanged="txtMontoIntereses_TextChanged" AutoPostBack="true" CssClass="FormatoTextBox" MaxLength="26"></asp:TextBox>
                   
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-5">Moneda<p class="auto-style99">*</p></div>
                                <div class="col-md-7">
                                    <asp:DropDownList ID="DDLMoneda" runat="server" AppendDataBoundItems="true" AutoPostBack="true" CssClass="FormatoDropDownList" OnSelectedIndexChanged="DDLMoneda_SelectedIndexChanged">
                                    </asp:DropDownList>
        
                                     <asp:RequiredFieldValidator id="rfvMoneda" runat="server"
                                          ControlToValidate="DDLMoneda" InitialValue="0"
                                          ErrorMessage="La moneda, es un campo requerido."
                                          ForeColor="Red" style="font-size: xx-small">
                                         </asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-5">Monto Colones Principal</div>
                                <div class="col-md-7">
                                     <asp:TextBox ID="txtMontoColonesPrincipal" runat="server" BackColor="#FFCC66" CssClass="FormatoTextBox" ReadOnly="True"
                                          ></asp:TextBox>
                   
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-5">Valor Presente Principal</div>
                                <div class="col-md-7">
                                    <asp:TextBox ID="txtValorPresentePricipal" runat="server" BackColor="#FFCC66"  CssClass="FormatoTextBox" ReadOnly="True"></asp:TextBox>
                   
                                </div>
                            </div>
                            <div class="row" runat="server" id="rColonIntereses">
                                <div class="col-md-5">Monto Colones Intereses</div>
                                <div class="col-md-7">
                                     <asp:TextBox ID="txtMontoColonesIntereses" runat="server" BackColor="#FFCC66"  CssClass="FormatoTextBox" ReadOnly="True"></asp:TextBox>
                    
                                </div>
                            </div>
                            <div class="row" runat="server" id="rVPIntereses">
                                <div class="col-md-5">Valor Presente Intereses</div>
                                <div class="col-md-7">
                                    <asp:TextBox ID="txtValorPresenteIntereses" runat="server" BackColor="#FFCC66"  CssClass="FormatoTextBox" ReadOnly="True"></asp:TextBox>
                    
                                </div>
                            </div>
                        </div>

                        </ContentTemplate>
                </asp:UpdatePanel>
                    
      
    <table class="asd" style="width:100%">

        <tr>
            <td class="auto-style17" style="mso-fareast-font-family: &quot;Times New Roman&quot;; mso-ansi-language: ES-CR; mso-fareast-language: ES; mso-bidi-language: AR-SA; mso-bidi-font-weight: bold" colspan="3"><strong style="text-align: left">

      
                Calcular&nbsp; montos</strong></td>
            <td class="auto-style1">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="mso-fareast-font-family: &quot;Times New Roman&quot;; mso-ansi-language: ES-CR; mso-fareast-language: ES; mso-bidi-language: AR-SA; mso-bidi-font-weight: bold" rowspan="4" class="auto-style71"><strong>Indicadores Económicos BCCR</strong></td>
            <td class="auto-style75">

      
                <strong>TBP&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;</strong><asp:TextBox ID="txtTBP" runat="server" BorderStyle="None" CssClass="auto-style22" Enabled="False" ForeColor="Red" Height="16px" Width="67px"></asp:TextBox>
            </td>
            <td class="auto-style76" style="mso-fareast-font-family:Times New Roman; mso-ansi-language: ES-CR; mso-fareast-language: ES; mso-bidi-language: AR-SA; mso-bidi-font-weight: bold">&nbsp;</td>
            <td class="auto-style77">
                </td>
        </tr>
        <tr>
            <td class="auto-style16">
                <strong>Compra&nbsp;
                </strong>
                <span class="auto-style56">
                <strong>
                <asp:Image ID="Image5" runat="server" Height="16px" ImageUrl="~/Compartidas/imagenes/1446084309_49.png" Width="16px" />
                <asp:TextBox ID="txtCompra" runat="server" BorderStyle="None" CssClass="auto-style47" Enabled="False" ForeColor="Red" Height="16px" Width="65px"></asp:TextBox>
                </strong> </span>
            </td>
            <td class="auto-style53">
                
                <asp:TextBox ID="txtTiempo" runat="server" onkeypress="return AceptarSoloNumeros(event)" BackColor="White" Height="16px" Width="215px" Visible="False"></asp:TextBox>
                
            </td>
            <td class="auto-style14">
                
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style51">
                <strong>Venta</strong><span class="auto-style22"><span class="auto-style56"><strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Image ID="Image6" runat="server" Height="16px" ImageUrl="~/Compartidas/imagenes/1446084309_49.png" Width="16px" />
                <asp:TextBox ID="txtVenta" runat="server" BorderStyle="None" CssClass="auto-style47" Enabled="False" ForeColor="Red" Height="17px" Width="67px"></asp:TextBox>
                </strong> </span> </span>
            </td>
            <td class="auto-style54">

      
                <strong>&nbsp;</strong></td>
            <td class="auto-style52">

      
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style16">
                <strong>Euro</strong>&nbsp;&nbsp;&nbsp; &nbsp;
                <strong> <span class="auto-style22"><span class="auto-style56">
                &nbsp;<asp:Image ID="Image7" runat="server" Height="16px" ImageUrl="~/Compartidas/imagenes/1446084323_05.png" Width="16px" />
                </span> </span>
                </strong>&nbsp; <span class="auto-style22"><span class="auto-style56"><strong>
                <asp:TextBox ID="txtEuro" runat="server" BorderStyle="None" CssClass="auto-style47" Enabled="False" ForeColor="Red" Height="16px" Width="67px"></asp:TextBox>
                </strong> </span> </span>
            </td>
            <td class="auto-style53">&nbsp;</td>
            <td class="auto-style14">
                &nbsp;</td>
        </tr>
    </table>
            Observaciones
        <CKEditor:CKEditorControl ID="CKEditorObservaciones" runat="server" Width="100%" Height="156px"></CKEditor:CKEditorControl>
        </ContentTemplate>
    </asp:UpdatePanel>
  
                <div style="margin-top: 5px">
                    
            <label>Archivo adjuntar</label>
            <asp:FileUpload ID="FileUpload1" runat="server" Font-Bold="True"  CssClass="FormatoTextBox" Width="50%"/>
            <asp:Button ID="btnUpload" runat="server" OnClick="btnUpload_Click1" Text="Subir Archivo" CssClass="ButtonNeutro"  />
                  
 <asp:GridView ID="gvFiles" DataKeyNames="IdArchivo" runat="server" Width="420px" AutoGenerateColumns="False"  SortExpression="IdArchivo" OnRowDeleting="gvFiles_RowDeleting"
                        CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr" >
                                <Columns>
                                    <asp:TemplateField HeaderText="Adjunto">
                                        <ItemTemplate>
                                            <img alt="" src="../Compartidas/imagenes/24x24-paperclip.png" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="IdArchivo" HeaderText="" Visible="false"/>
                                    <asp:BoundField DataField="NombreArchivo" HeaderText="Nombre de Archivo" />
                                    <asp:BoundField DataField="Tamano" HeaderText="Tamaño" />
                                    <asp:BoundField DataField="IdResolucion" HeaderText="" Visible="false"/>
                                    <asp:TemplateField HeaderText="FchModifica" SortExpression="FchModifica" Visible="false"> 
                                        <ItemTemplate>
                                            <asp:Label ID="lblFchModifica" runat="server"  Text='<%# Bind("FchModifica") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                           <asp:HyperLink runat="server"
                                            NavigateUrl='<%# Eval("IdArchivo", "ObtenerArchivo.aspx?Id={0}&Flag=Pretension")  %>'
                                            Text="Descargar"></asp:HyperLink> 
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="" ShowHeader="False" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEliminar" runat="server" CausesValidation="False" CommandArgument='<%#Eval("IdArchivo") + ";" +Eval("IdResolucion")%>' CommandName="Delete" Text="Eliminar"></asp:LinkButton>
                                    </ItemTemplate>
                                    </asp:TemplateField>
                                        </Columns>
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                                </asp:GridView>
        </div>

     <table  style="width:100%">
         <tr>
             <td>
                <table style="width:100%;vertical-align:central;">
                    <tr>
                        <td style="text-align:right">
                            &nbsp;<asp:Button ID="btnModificar" runat="server" Text="Modificar" CssClass="ButtonNeutro" Visible="False" OnClick="btnModificar_Click"/>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align:center;"><asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="ButtonNeutro" OnClick="btnGuardar_Click"/>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align:center"><asp:Button ID="btnGenerarAsientos" runat="server" CssClass="ButtonNeutro" OnClick="btnGenerarAsientos_Click" ToolTip="Generar Asiento" Text="Generar Asiento" Visible="False"/>
                        </td>
                    </tr>
                </table>
                
                &nbsp;&nbsp&nbsp</td></tr>    
         
        <tr><td class="auto-style5" colspan="4">
                <asp:Panel ID="panelMensajes" runat="server" Visible="false">
        <br />
                    <asp:Image ID="IMGCorrectoMsg" runat="server" Height="20px" ImageUrl="~/Compartidas/imagenes/1444297049_Check.png" Width="20px" />
                    &nbsp;&nbsp;<asp:Label ID="lblMsgCorrecto" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Green" Text="Prueba1"></asp:Label>
        <br />
                    <asp:Image ID="IMGInCorrectoMsg" runat="server" Height="20px" ImageUrl="~/Compartidas/imagenes/1444297028_Delete.png" Width="20px" />
                    &nbsp;&nbsp;<asp:Label ID="lblMsgInCorrecto" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red" Text="Prueba1"></asp:Label>
                </asp:Panel>
            </td></tr>       
        </table>

      
    

</asp:Content>

<asp:Content ID="Content4" runat="server" contentplaceholderid="ContenidoJS">
    
    <script type="text/javascript" src="/Compartidas/rmm-js/jquery-ui.js"></script>

        <script type="text/javascript">
            $(document).ready(function () {
                SearchText();
            });
            function SearchText() {
                $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
            }

    </script> 
     
    <style type="text/css">
        .asd {
            width: 630px;
        }
        .Background
        {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }
        .Popup
        {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 400px;
            height: 350px;
        }
        .lbl
        {
            font-size:16px;
            font-style:italic;
            font-weight:bold;
        }

        .auto-style13 {}

        .auto-style14 {
            background-color: #CCCCCC;
        }
        .auto-style16 {
            width: 546px;
            text-align: left;
            background-color: #CCCCCC;
        }
        .auto-style17 {
            color: #336699;
            text-align: left;
        }
        .auto-style18 {
            background-color: #FFFFFF;
        }
        .auto-style19 {
            text-align: left;
            width: 149px;
            background-color: #FFFFFF;
            color: #000000;
        }
        .auto-style20 {
            width: 546px;
            text-align: left;
            background-color: #FFFFFF;
        }
        
        .auto-style22 {
            color: #000000;
        }
        .auto-style23 {
            background-color: #FFFFFF;
            color: #000000;
            width: 267px;
        }

        .auto-style25 {
            height: 83px;
            background-color: #FFFFFF;
        }
        .auto-style29 {
            height: 33px;
        }
        .auto-style30 {
            width: 546px;
            text-align: left;
            height: 33px;
        }
        .auto-style37 {
            height: 33px;
            width: 267px;
        }
        .auto-style43 {
            background-color: #FFFFFF;
            height: 41px;
        }
        .auto-style44 {
            width: 267px;
            height: 36px;
        }
        .auto-style45 {
            text-align: left;
            height: 36px;
            width: 546px;
        }
        .auto-style46 {
            text-align: left;
            width: 149px;
            height: 36px;
        }
        .auto-style47 {
            height: 32px;
        }
        .auto-style48 {
            height: 36px;
        }

        .auto-style49 {
            font-size: xx-small;
            color: #FF0000;
            background-color: #FFFFFF;
        }

        p.MsoNormal
	{margin-top:0cm;
	margin-right:0cm;
	margin-bottom:4pt;
	margin-left:0cm;
	font-size:11.0pt;
	font-family:"Calibri","sans-serif";
	}
        
        .auto-style51 {
            width: 546px;
            text-align: left;
            background-color: #CCCCCC;
            height: 24px;
        }
        .auto-style52 {
            background-color: #CCCCCC;
            height: 24px;
        }

        .auto-style53 {
            width: 149px;
            text-align: left;
            background-color: #CCCCCC;
        }
        .auto-style54 {
            width: 149px;
            text-align: left;
            background-color: #CCCCCC;
            height: 24px;
        }

        .auto-style56 {
            height: 203px;
        }

        .auto-style59 {
            width: 267px;
            height: 50px;
        }
        .auto-style61 {
            height: 50px;
        }
        .auto-style63 {
            height: 83px;
        }
        .auto-style71 {
            width: 267px;
        }
        
        .auto-style75 {
            width: 546px;
            text-align: left;
            background-color: #CCCCCC;
            height: 11px;
        }
        .auto-style76 {
            width: 149px;
            text-align: left;
            background-color: #CCCCCC;
            height: 11px;
        }
        .auto-style77 {
            background-color: #CCCCCC;
            height: 11px;
        }

        .auto-style79 {
            width: 546px;
        }
        
        .auto-style86 {
            color: #000000;
            width: 149px;
        }
        .FormatoTextBox, .FormatoDropDownList { width:208px;}
        </style>
</asp:Content>
