<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" CodeBehind="Liquidacion.aspx.cs" Inherits="Presentacion.Contingentes.Liquidacion" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Assembly="eWorld.UI.Compatibility, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>
<asp:Content ID="Content1" ContentPlaceHolderID="encabezado" runat="server">

</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenido" runat="server">

     <div class="col-md-12"><span class="auto-style4">Liquidación</span></div>

    <div> 
        <asp:Button runat="server" ID="btn_Simulacion" Text="Simulación Diferencial Cambiario Mensual" OnClick="btn_Simulacion_Click" visible="false" CausesValidation="false"/> 
    </div>

    <div> 
        <br>
        <asp:Label ID="Label4" runat="server"  visible="false" Text="Cantidad de dias:     "></asp:Label>
        <asp:TextBox ID="txtbox_cantdias"  visible="false" runat="server" Height="16px" Width="37px"></asp:TextBox>
         <p class="auto-style99" style="font-size:small">&nbsp;</p> 
        <br>
        <asp:Button runat="server" ID="btn_Incobrabilidad" Text="Simulación Incobrabilidad" OnClick="btn_Incobrabilidad_Click"  visible="false" CausesValidation="false"/> 
    </div>

    <asp:Label ID="Label2" runat="server" Text="Cambio de Mes" Visible="false"></asp:Label>
    <asp:CheckBox ID="ckbNuevoMes" runat="server" Visible="false" AutoPostBack="true"/>

    <asp:Label ID="Label3" runat="server" Text="Cambio de Año" Visible="false"></asp:Label>
    <asp:CheckBox ID="ckbNuevoAno" runat="server" Visible="false" AutoPostBack="true"/>



     <div class="col-md-12" style="margin-bottom:10px;">
         <p class="auto-style99" style="font-size:small">* Indica que es un campo requerido.</p> 
    </div>
    <div  class="col-md-6">
        <div class="col-md-5">Num Expediente<p class="auto-style99">*</p></div>
        <div class="col-md-7">
            <asp:DropDownList ID="ddlIdExpediente" AppendDataBoundItems="true" runat="server" data-placeholder="---- Seleccione opcion ---" class="chzn-select" OnSelectedIndexChanged="ddlIdExpediente_SelectedIndexChanged" AutoPostBack="true" Width="100%">
            <Items>
                <asp:ListItem Text="---- Elegir Expediente ----" Value="0" />
                </Items>
            </asp:DropDownList>
            <asp:Label ID="lblNumExpediente" runat="server" style="color: #003399" Visible="False"></asp:Label>
            <asp:RequiredFieldValidator id="rfvNumExpediente" 
                ControlToValidate="ddlIdExpediente" ErrorMessage="Número expediente.Campo requerido." ForeColor="Red" 
                Display="Static" InitialValue="0" runat="server" style="font-size: xx-small"/>
        </div>

        <div class="col-md-12">
                    <asp:Label ID="Label1" runat="server" Text="Declar como incobrable" Visible="false"></asp:Label>
                    <asp:CheckBox ID="ckbIncobrable" runat="server" Visible="false" AutoPostBack="true"/>

                </div>
    </div>
    <asp:UpdatePanel ID="upDatosPrincipales" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
               
            <div class="col-md-6">
                <div class="col-md-5">Fecha de&nbsp; Liquidación<p class="auto-style99">
                    *</p>
                </div>
                <div class="col-md-7"> <asp:TextBox ID="txtFechaResolucion" runat="server" CssClass="js-date-picker FormatoTextBox"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvFechaResolucion" ControlToValidate="txtFechaResolucion" runat="server" ErrorMessage="Fecha de Liquidación.Campo requerido." ForeColor="Red" 
                Display="Static" style="font-size: xx-small"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="col-md-6">
                <div class="col-md-5">Num. Resolución Dictada<p class="auto-style99">*</p></div>
                <div class="col-md-7">
                    <asp:TextBox ID="txtNumResol"  runat="server" CssClass="FormatoTextBox"></asp:TextBox>
                    <asp:RequiredFieldValidator id="rfvEstadoResol" 
                        ControlToValidate="txtNumResol" ErrorMessage="Número de resolución.Campo requerido." ForeColor="Red" 
                        Display="Static" InitialValue="" runat="server" style="font-size: xx-small"/>
                </div>
            </div>
         
            <asp:UpdatePanel ID="upMontos" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <div style="display:inline-block;width:100%;">
                     <div style="width:50%">
                        
                        <div class="col-md-5">Moneda<p class="auto-style99">*</p></div>
                        <div class="col-md-7"> 
                            <asp:DropDownList ID="DDLMoneda" AppendDataBoundItems="true" runat="server"  CssClass="FormatoDropDownList" OnSelectedIndexChanged="DDLMoneda_SelectedIndexChanged" AutoPostBack="true">
                                 <Items>
                                    <asp:ListItem Value="0" Selected="True" >--- Elegir Moneda ---</asp:ListItem>                                     
                                    <asp:ListItem Value="CRC">CRC - Colones</asp:ListItem>
                                    <asp:ListItem Value="USD">USD - Dólares</asp:ListItem>
                                    <asp:ListItem Value="EUR">EUR - Euros</asp:ListItem>
                                  </Items>
                                </asp:DropDownList>
                            <asp:RequiredFieldValidator id="rfvMoneda" 
                                 ControlToValidate="DDLMoneda" ErrorMessage="Seleccione una moneda.Campo requerido." ForeColor="Red" 
                                 Display="Static" InitialValue="0" runat="server" style="font-size: xx-small"/>
            
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div  class="row">
                        <div class="col-md-5">Intereses</div>
                        <div class="col-md-7">  <asp:TextBox ID="txtIntereses" runat="server" onkeypress="return AceptarSoloNumeros(event)"  CssClass="FormatoTextBox" MaxLength="26" OnTextChanged="txtIntereses_TextChanged" AutoPostBack="true"></asp:TextBox></div>
                    </div>

                    <div  class="row">
                        <div class="col-md-5">Intereses Moratorios</div>
                        <div class="col-md-7"> <asp:TextBox ID="txtInteresesMoratorios" runat="server" onkeypress="return AceptarSoloNumeros(event)"  CssClass="FormatoTextBox"  MaxLength="26" OnTextChanged="txtInteresesMoratorios_TextChanged" AutoPostBack="true"></asp:TextBox></div>
                    </div>

                    <div  class="row">
                        <div class="col-md-5">Costas</div>
                        <div class="col-md-7"> <asp:TextBox ID="txtCostas" runat="server" onkeypress="return AceptarSoloNumeros(event)"  CssClass="FormatoTextBox"  MaxLength="26" OnTextChanged="txtCostas_TextChanged" AutoPostBack="true"></asp:TextBox></div>
                    </div>
                    
                    <div  class="row">
                        <div class="col-md-5">Daño Moral</div>
                        <div class="col-md-7">   <asp:TextBox ID="txtDannoMoral" runat="server" onkeypress="return AceptarSoloNumeros(event)"  CssClass="FormatoTextBox"  MaxLength="26" OnTextChanged="txtDannoMoral_TextChanged" AutoPostBack="true"></asp:TextBox></div>
                    </div>
                </div> 
                <div class="col-md-6">
                    <div  class="row">
                        <div class="col-md-5">Intereses en Colones</div>
                        <div class="col-md-7"><asp:TextBox ID="txtInteresesColones" runat="server" ReadOnly="True" style="background-color: #CCCCCC"  CssClass="FormatoTextBox"></asp:TextBox></div>
                    </div>
                    
                    <div  class="row">
                        <div class="col-md-5">Intereses moratorios en colones</div>
                        <div class="col-md-7"> <asp:TextBox ID="txtInteresesMoratoriosColones" runat="server" ReadOnly="True" style="background-color: #CCCCCC"  CssClass="FormatoTextBox"></asp:TextBox></div>
                    </div>

                    <div  class="row">
                            <div class="col-md-5">Costas en Colones</div>
                            <div class="col-md-7">  <asp:TextBox ID="txtCostasColones" runat="server" ReadOnly="True" style="background-color: #CCCCCC"  CssClass="FormatoTextBox"></asp:TextBox></div>
                    </div>

                    <div  class="row">
                        <div class="col-md-5">Daño Moral en Colones</div>
                        <div class="col-md-7"><asp:TextBox ID="txtDannoMoralColones" runat="server" ReadOnly="True" style="background-color: #CCCCCC"  CssClass="FormatoTextBox"></asp:TextBox></div>
                        <%--<asp:SqlDataSource ID="SqlDSMoneda" runat="server" ConnectionString="<%$ ConnectionStrings:GestNICSPDEVConnectionString %>" SelectCommand="select IdMoneda,IdMoneda+' - '+NomMoneda as Nombre from [ma].[Monedas] where IdMoneda IN ('USD', 'CRC' ,'EUR')"></asp:SqlDataSource>--%>
                    </div>
                </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            
           </ContentTemplate>
    </asp:UpdatePanel>
    <table class="asd" style="width:100%">
 
        <tr>
            <td class="auto-style5">&nbsp;</td>
            <td class="auto-style14">
                 </td>
            <td class="auto-style14">
                &nbsp;</td>
            <td class="auto-style51">

      
                &nbsp;</td>
            <td>
                <asp:Label ID="lblEstadoResolucion" runat="server" style="text-align: center" Text="Liquidacion" Visible="False"></asp:Label>
                <%--<asp:SqlDataSource ID="SqlDSExpedientes" runat="server" ConnectionString="<%$ ConnectionStrings:GestNICSPDEVConnectionString %>" SelectCommand="SELECT IdExpediente, IdExpediente + ' - ' + TipoExpediente AS NbrExpediente FROM co.Expedientes WHERE (EstadoExpediente = 'Activo')"></asp:SqlDataSource>--%>
            </td>
        </tr>  
        <tr><td class="auto-style5" rowspan="3"><strong>Indicadores Económicos BCCR</strong></td>
            <td class="auto-style48" colspan="2">
                <strong>Compra
                </strong>
                <span class="auto-style56">
                <strong>
                <asp:Image ID="Image8" runat="server" Height="16px" ImageUrl="~/Compartidas/imagenes/1446084309_49.png" Width="16px" />
                <asp:TextBox ID="txtCompra" runat="server" BorderStyle="None" CssClass="auto-style47" Enabled="False" ForeColor="Red" Height="17px" Width="61px"></asp:TextBox>
                </strong> </span>
            </td>
            <td class="auto-style6" rowspan="3">

      
                &nbsp;</td><td class="auto-style7" rowspan="2">&nbsp;&nbsp&nbsp</td></tr>       
        <tr>
            <td class="auto-style16" colspan="2">
                <strong>Venta</strong><span class="auto-style22"><span class="auto-style56"><strong>&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Image ID="Image9" runat="server" Height="16px" ImageUrl="~/Compartidas/imagenes/1446084309_49.png" Width="16px" />
                <asp:TextBox ID="txtVenta" runat="server" BorderStyle="None" CssClass="auto-style47" Enabled="False" ForeColor="Red" Height="16px" Width="62px"></asp:TextBox>
                </strong> </span> </span>
            </td>
            </tr>       
        <tr>
            <td class="auto-style16" colspan="2">
                <strong>Euro</strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span class="auto-style22"><span class="auto-style56"><strong>
                <asp:Image ID="Image10" runat="server" Height="16px" ImageUrl="~/Compartidas/imagenes/1446084323_05.png" Width="16px" />
                &nbsp;<asp:TextBox ID="txtEuro" runat="server" BorderStyle="None" CssClass="auto-style47" Enabled="False" ForeColor="Red" Height="16px" Width="63px"></asp:TextBox>
                </strong> </span> </span>
            </td>
            
            <td class="auto-style7">
                <asp:Button ID="btnGenerarAsientos" runat="server" CssClass="ButtonNeutro" OnClick="btnGenerarAsientos_Click" Text="Generar Asiento" ToolTip="Generar Asiento"  Visible="False" />
            </td>
            </tr> 
        <tr><td class="auto-style5">Observaciones</td>
            <td colspan="4"><CKEditor:CKEditorControl ID="CKEditorObservaciones" runat="server" Width="100%" Height="156px"></CKEditor:CKEditorControl></td>
        </tr>  
 
        </table>

    <label>Archivo adjuntar</label>
    <asp:FileUpload ID="FileUpload1" runat="server" Font-Bold="True"  CssClass="FormatoTextBox" style="width:50%!important"/>
     <asp:Button ID="btnSubirArhivo" runat="server" OnClick="btnSubirArhivo_Click" Text="Subir Archivo" CssClass="ButtonNeutro"/>
                   
     <asp:GridView ID="gvFiles" DataKeyNames="IdArchivo" runat="server"  Width="690px" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" SortExpression="IdArchivo" OnRowDeleting="gvFiles_RowDeleting"
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
                            <asp:BoundField DataField="IdExpediente" HeaderText="IdExpediente" Visible="false"/>
                                        
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
                        <asp:LinkButton ID="lnkEliminar" runat="server" CausesValidation="False" CommandArgument='<%#Eval("IdArchivo") + ";" +Eval("IdExpediente")%>' CommandName="Delete" Text="Eliminar"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                        </Columns>
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                    </asp:GridView>

    <div style="width:100%;text-align:center;">
        <asp:Button ID="btnModificar" runat="server" Text="Modificar" CssClass="ButtonNeutro" BackColor="#3366CC" Width="132px" OnClick="btnModificar_Click"  Visible="false"/><asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="ButtonNeutro" OnClick="btnGuardar_Click"/>
    </div>
</asp:Content>
<asp:Content ID="Content4" runat="server" contentplaceholderid="ContenidoJS">

      <script src="https://code.jquery.com/ui/1.10.3/jquery-ui.js"></script> 
     <script src="/Compartidas/rmm-js/chosen.jquery.js" type="text/javascript"></script>  
   
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
    <style type="text/css">
        .FormatoTextBox, .FormatoDropDownList { width:100%!important; }
        .auto-style1 {
            width: 239px;
        }
        .asd {
            width: 630px;
        }
        .auto-style4 {
            color: #3366CC;
            font-size: medium;
            font-weight: 700;
        }
        .auto-style5 {
            width: 177px;
        }
        .auto-style6 {
            width: 150px;
        text-align: center;
    }
        .auto-style7 {
            text-align: right;
        }
        .auto-style9 {
            height: 34px;
        }
        .auto-style13 {
            font-size: xx-small;
            color: #FF0000;
        }
        .auto-style14 {
        }
        .auto-style15 {
            height: 34px;
            }
        .auto-style17 {
            width: 177px;
            font-family: Arial, sans-serif;
            font-size: small;
        }
        .auto-style18 {
            font-size: medium;
        }
        .auto-style16 {
            text-align: left;
            background-color: #CCCCCC;
        }
        .auto-style47 {}
    .auto-style48 {
            text-align: left;
            background-color: #CCCCCC;
            height: 12px;
        }
        .auto-style49 {
            color: #FF0000;
            font-size: x-small;
        }
        .auto-style51 {
            width: 150px;
        }
        .GridViewStyle {}
        .auto-style22 {}
    </style>
</asp:Content>
