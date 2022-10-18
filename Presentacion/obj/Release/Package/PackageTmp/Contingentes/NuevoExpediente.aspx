<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master"  AutoEventWireup="true" CodeBehind="NuevoExpediente.aspx.cs" Inherits="Presentacion.Contingentes.NuevoExpediente" %>
<%--<%@ Register Assembly="MessageBox" Namespace="Utilities" TagPrefix="cc2" %>--%>
<%@ Register Assembly="eWorld.UI.Compatibility, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>
<asp:Content ID="Content1" ContentPlaceHolderID="encabezado" runat="server">
    
</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenido" runat="server">
    <div class="col-md-12">
          <asp:Image ID="Image5" runat="server" Height="25px" ImageUrl="~/Compartidas/imagenes/1442438239_icon-81-document-add.png" Width="28px" />
                Formulario Expediente
    </div>
    <div class="col-md-12" style="margin-bottom:10px;">
         <p class="auto-style99" style="font-size:small">* Indica que es un campo requerido.</p> 
    </div>
   
<div class="col-md-12" style="width:100%;display:block;">
    <div  class="col-md-6">
        <div class="row">
            <div class="col-md-5"><strong>Fecha de demanda</strong><p class="auto-style99">*</p></div>
            <div class="col-md-7">
                 <%--<li><a href='ReportesContingentes.aspx?rept=CXP'>Reporte de Cuentas por Pagar y Cobrar</a></li> --%>
                <asp:TextBox ID="txtFechaDemanda" CssClass="js-date-picker FormatoTextBox" runat="server"></asp:TextBox>
                <span class="auto-style99"></span>
                 <asp:HiddenField id="hdnbox" runat="server" />
                <%--<asp:TextBox ID="txtFechaDemanda" runat="server" TextMode="Date" Height="20px" Width="204px" CssClass="auto-style93"></asp:TextBox>--%>
        
            </div>
        </div>
          <div class="row">
            <div class="col-md-5"><strong>Administrado como</strong><p class="auto-style99">*</p></div>
            <div class="col-md-7">
                <asp:DropDownList ID="ddlManejadoComo" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlManejadoComo_SelectedIndexChanged" AutoPostBack="true"  CssClass="FormatoDropDownList">
                 <Items>
                    <asp:ListItem Text="---- Elegir Caso Contingente ----" Value="0" />
                  </Items>
                </asp:DropDownList>
                <asp:RequiredFieldValidator id="rfvManejadoComo" 
                     ControlToValidate="ddlManejadoComo" ErrorMessage="Seleccione un tipo de contingente.Campo requerido." ForeColor="Red" 
                     Display="Static" InitialValue="0" runat="server" style="font-size: xx-small"/>
            </div>
        </div>
          <div class="row">
            <div class="col-md-5"><strong>Expediente # <p class="auto-style99">*</p></strong></div>
            <div class="col-md-7">
                 <asp:TextBox ID="txtExpedientes" runat="server" placeholder="Formato: XX-XXXXXX-XXXX-XX" CssClass="FormatoTextBox" maxlength="17" ToolTip="El número de expediente debe tener 17 caracteres incluyendo los guiones. Ejemplo: 12-000055-1025-CA" ></asp:TextBox>
                <asp:RegularExpressionValidator ID="valPassword" runat="server"
                       ControlToValidate="txtExpedientes"
                       ErrorMessage="El número de expediente debe tener 17 caracteres"
                       ValidationExpression=".{17}.*" 
                    ForeColor="Red" style="font-size: xx-small"/>  
                <asp:RequiredFieldValidator id="rfvtxtExpedientes" runat="server"
                      ControlToValidate="txtExpedientes"
                      ErrorMessage="Número de expediente, es un campo requerido."
                      ForeColor="Red" style="font-size: xx-small">
                 </asp:RequiredFieldValidator>
                
            </div>
        </div>
          <div class="row">
            <div class="col-md-5"><strong><b><span>Número Expediente Administrativo</span></b> </strong></div>
            <div class="col-md-7"><asp:TextBox ID="txtOrigenExp" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
          <div class="row">
            <div class="col-md-5"><strong>Tipo de proceso (área)<p class="auto-style99">*</p></strong></div>
            <div class="col-md-7">
                <asp:DropDownList ID="DDLTipoProceso" runat="server" AppendDataBoundItems="true"  ClientIDMode="Static" CssClass="FormatoDropDownList">
                 <Items>
                    <asp:ListItem Text="--- Elegir Tipo Proceso ---" Value="0" />
                  </Items>
                </asp:DropDownList>
                 <asp:RequiredFieldValidator id="rfvTipoProceso" 
                 ControlToValidate="DDLTipoProceso" ErrorMessage="Seleccione el tipo de proceso.Campo requerido." ForeColor="Red" 
                 Display="Static" InitialValue="0" runat="server" style="font-size: xx-small"/>
            </div>
        </div>
          <div class="row">
            <div class="col-md-5"><strong>Motivo demanda<p class="auto-style99">*</p></strong></div>
            <div class="col-md-7">
                 <asp:DropDownList ID="ddlMotivoDemanda" AppendDataBoundItems="true" runat="server" CssClass="FormatoDropDownList">
                 <Items>
                    <asp:ListItem Text="---- Elegir Motivo ----" Value="0" />
                  </Items>
                 </asp:DropDownList>
                  <asp:RequiredFieldValidator id="rfvMotivoDemanda" 
                 ControlToValidate="ddlMotivoDemanda" ErrorMessage="Seleccione el motivo de la demanda.Campo Requerido." ForeColor="Red" 
                 Display="Static" InitialValue="0" runat="server" style="font-size: xx-small"/>
            </div>
        </div>
    </div>
    <div  class="col-md-6">
        <div class="row">
            <div class="col-md-5"><strong>Tipo de persona</strong></div>
            <div class="col-md-7">
                <asp:DropDownList ID="ddlTipoPersona" runat="server" AppendDataBoundItems="true" onchange="cargAnnombre()" CssClass="FormatoDropDownList">
                    <asp:ListItem Selected="True" Value="0">--- Tipo Persona ---</asp:ListItem>
                    <asp:ListItem Value="Fisico">Físico</asp:ListItem>
                    <asp:ListItem Value="Juridico">Jurídico</asp:ListItem>
                    <asp:ListItem Value="DIMEX">DIMEX</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div class="row">
            <div class="col-md-5"><strong>Cédula</strong></div>
            <div class="col-md-6" style="padding-right:5px;"> 
                <asp:Label ID="lblCedula" runat="server"></asp:Label>
                  <asp:TextBox ID="txtCedulaActor" runat="server" CssClass="FormatoTextBox" onkeyup="cargAnnombre()"  AutoPostBack="true" AutoCompleteType="Disabled" ></asp:TextBox>
                <span class="auto-style99">
                 <asp:RegularExpressionValidator ForeColor="Red" Display="Dynamic" ControlToValidate="txtCedulaActor" runat="server" ErrorMessage="Número de cédula inválido" ValidationExpression="\d+" style="font-size: xx-small; color: #FF9900"></asp:RegularExpressionValidator>
                </span>
                
            </div>
            <div class="col-md-1" style="padding:0px;"> <asp:ImageButton ID="btnAdd" runat="server"  OnClientClick="return cargarPersonas()" CssClass="ButtonNeutro" ImageUrl="~/Compartidas/imagenes/24-Plus.png" Style="padding:5px;width:25px;height:25px;" ToolTip="Agregar cédula"/>
               </div>
        </div>
        <div class="row">
            <div class="col-md-5"></div>
            <div class="col-md-7"> <asp:TextBox ID="txtConcatena" runat="server" TextMode="MultiLine" Height="52px" ClientIDMode="Static" CssClass="FormatoTextBox" Style="text-transform: capitalize;" onfocus = "capitalLetter()"></asp:TextBox></div>
        </div>
        <div class="row">
            <div class="col-md-5"><strong>Nombre</strong> <asp:Label ID="lblNombre2" runat="server"></asp:Label></div>
            <div class="col-md-6" style="padding-right:5px;"><asp:TextBox ID="txtNombreActor" runat="server" Height="60px" TextMode="MultiLine" CssClass="FormatoTextBox" ClientIDMode="Static"></asp:TextBox></div>
            <div class="col-md-1" style="padding:0px;"><asp:ImageButton ID="imgButton1" runat="server"  OnClick="Button1_Click" CssClass="ButtonNeutro" ImageUrl="~/Compartidas/imagenes/24-Atras.png" Style="padding:5px;width:25px;height:25px;" ToolTip="Limpiar nombres."/></div>
        </div>
        <div class="row">
            <div class="col-md-5"><strong>Nombre</strong><asp:Label ID="lblNombre1" runat="server"></asp:Label></div>
            <div class="col-md-7"> <asp:TextBox ID="txtNombre2" runat="server" Height="39px" CssClass="FormatoTextBox" TextMode="MultiLine" ReadOnly="True"></asp:TextBox></div>
        </div>
     
    </div>
    
    </div>
            
        <div style="text-align:center;"> <asp:Button ID="btnGuardarActualizar" runat="server" Text="Guardar" OnClick="BtnGuardarActualizar_Click" CssClass="ButtonNeutro"/></div>
   <table style="width: 100%" class="auto-style93">
              
       <tr> <td hidden="hidden" >Centro beneficio</td>
             <td>
                 <asp:DropDownList ID="DDLCentroBeneficio" runat="server" AppendDataBoundItems="true"  Visible="false" onchanged="SeleccionarCentroBeneficio()" ClientIDMode="Static" CssClass="FormatoDropDownList">
                  </asp:DropDownList>
                 <br />
            </td>
           </tr>
           <tr>
             <td hidden="hidden">Estado procesal</td> 
               <td><asp:DropDownList ID="DDLEstadoProcesal" runat="server" AppendDataBoundItems="true" onchange="SeleccionarEstadoProcesal()" ClientIDMode="Static" CssClass="FormatoDropDownList" Visible="False">
                 <Items>
                    <asp:ListItem Text="--- Elegir Estado Procesal---" Value="0" />
                  </Items>
                 </asp:DropDownList>
                <span class="auto-style99"><strong hidden="hidden">Requerido</strong></span></td>
        </tr>
       
       <tr> <td class="auto-style63" hidden="hidden" colspan="2">
           <asp:Panel ID="pnlPretensionModificar" runat="server" Visible="False">
           </asp:Panel>
           </td>
        </tr>
                    
    </table>
    <div><asp:Panel runat="server" Visible="false" ID="panelMensajes" CssClass="auto-style93">
        <br /><asp:Image ID="IMGCorrectoMsg" runat="server" ImageUrl="~/Compartidas/imagenes/1444297049_Check.png" Height="20px" Width="20px"/>
        &nbsp;&nbsp;<asp:Label ID="lblMsgCorrecto" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Green" Text="Prueba1"></asp:Label>
        <br />
        <asp:Image ID="IMGInCorrectoMsg" runat="server" ImageUrl="~/Compartidas/imagenes/1444297028_Delete.png" Height="20px" Width="20px"/>
        &nbsp&nbsp<asp:Label ID="lblMsgInCorrecto" runat="server" ForeColor="Red" Text="Prueba1"  Font-Bold="True" Font-Size="Small"></asp:Label>
        </asp:Panel></div>               
   
    </asp:Content>

<asp:Content ID="Content4" runat="server" contentplaceholderid="ContenidoJS">
      <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
      <script type="text/javascript" src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>
      <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.6.2/jquery.min.js" ></script>
      <script type="text/javascript" src="https://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
      <script type="text/javascript" src="https://jquery-blog-js.googlecode.com/files/SetCase.js"></script>

     <script type="text/javascript">
         $(document).ready(function () {
            
         });

         window.onload = function () {
             document.getElementById("txtConcatena").readOnly = true;
             document.getElementById("txtNombreActor").readOnly = true;
         }

         function capitalLetter() {
             debugger;
             var valor = $('#txtConcatena').val();
             $('#txtConcatena').Setcase({ valor: 'pascal' });
         }
         function cargarPersonas() {
             debugger;
             
             var areaValue = $('#txtConcatena').val();
             var ActorValue = $('#txtNombreActor').val();

             var CapitalConvert = "";
             
             $('#txtNombreActor').html(ActorValue + areaValue + '\n');
            
         }
         function cargAnnombre() {
             debugger;
             var ced = $('#<%=txtCedulaActor.ClientID %>').val();
             var tipo = $('#<%=ddlTipoPersona.ClientID %> option:selected').val();
             
             if (ced.length >= 10) {
                 $('#<%=txtConcatena.ClientID %>').val('');
                 $.getJSON('https://www.hacienda.go.cr/ldap/buscar_persona3.php', { cedula: ced, origen: tipo }, function (datos) {   //ESTE USA UN SERVICIO
                     if (datos["primer apellido"] == undefined && datos["segundo apellido"] == undefined)
                     { var html = ced+' '+datos["nombre"]; }
                     else if (datos["segundo apellido"] == undefined)
                     { var html = ced + ' ' + datos["nombre"] + ' ' + datos["primer apellido"]; }
                     else
                     { var html = ced + ' ' + datos["nombre"] + ' ' + datos["primer apellido"] + ' ' + datos["segundo apellido"]; }
                     $('#<%=txtConcatena.ClientID %>').val(html);
                     $('#btnAdd').focus;
                 });
             }
             else {
                 $("input[id$='Contenido_txtConcatena']").val('');
             }
         }
    </script>

    <style type="text/css">
        div#Contenido_calFechaDemanda_calendar { position:inherit!important }
        .FormatoTextBox, .FormatoDropDownList { width:100%!important; }
          .uppercase {
            text-transform: uppercase;
        } 
        .lowercase {
            text-transform: lowercase;
        } 
        .capitalize {
            text-transform: capitalize;
        }
       
        .titulotabla {
            text-align: left;
            font-weight: 700;
            color: #3366CC;
            font-size: medium;
        }
        .auto-style13 {
            height: 30px;
        }
        .auto-style17 {
            height: 30px;
            width: 158px;
        }
        .auto-style19 {
            height: 51px;
            width: 158px;
        }
        .auto-style20 {
            height: 36px;
            width: 158px;
        }
        .auto-style24 {
            height: 51px;
            width: 235px;
        }
        .auto-style27 {
            height: 30px;
            }
        .auto-style29 {
            height: 51px;
            width: 140px;
        }
        .auto-style30 {
            height: 36px;
            }
        .auto-style34 {
            height: 51px;
            width: 139px;
        }
        .auto-style37 {
            height: 68px;
            width: 158px;
        }
        .auto-style38 {
            height: 68px;
            width: 140px;
        }
        .auto-style40 {
            height: 68px;
            width: 235px;
        }
        .auto-style43 {
            height: 30px;
            width: 139px;
        }
        .auto-style46 {
        }
        .auto-style48 {
            width: 117px;
        }
        .auto-style49 {
        }
        .auto-style51 {
            width: 235px;
        }
        .auto-style52 {
            width: 139px;
        }
        .auto-style59 {
            height: 30px;
            width: 404px;
        }
        .auto-style62 {
            height: 36px;
            width: 404px;
        }
        .auto-style63 {            text-align: right;
        }
        .auto-style79 {
            width: 404px;
        }
        .auto-style86 {
            height: 37px;
            width: 404px;
        }
        .auto-style87 {
            height: 37px;
            width: 158px;
        }
        .auto-style88 {
            height: 37px;
            }
        .auto-style93 {
            font-size: small;
            font-weight: 700;
        }
        .auto-style95 {
            border: 1px solid #563d7c;
            border-radius: 5px;
            color: white;
            background-image: url('../Compartidas/imagenes/1444297118_Save.png');
            background-repeat: no-repeat;
            background-position: left;
            padding-left: 15px;
            background-color: #3366CC;
            font-size: small;
            padding-right: 10px;
            padding-top: 5px;
            padding-bottom: 5px;
        }
        .auto-style99 {
            font-size: medium;
            color: #FF0000;
            display: inline;
        }
        .auto-style100 {
            font-weight: bold;
        }
        .auto-style101 {
            font-size: small;
            font-weight: bold;
        }
        .auto-style102 {
            height: 45px;
            width: 404px;
            font-weight: bold;
        }
        .auto-style103 {
            height: 45px;
            width: 158px;
        }
        .auto-style104 {
            height: 45px;
            }
        .auto-style108 {
            text-align: right;
            width: 404px;
        }
        .auto-style111 {
            height: 60px;
        }
        .auto-style112 {
            height: 60px;
            width: 404px;
        }
        .auto-style113 {
            height: 60px;
            width: 158px;
        }
        .auto-style114 {
            height: 29px;
            width: 404px;
        }
        .auto-style115 {
            height: 29px;
            width: 158px;
        }
        .auto-style116 {
            height: 29px;
        }
        .auto-style118 {
            height: 50px;
            width: 158px;
        }
        .auto-style119 {
            height: 50px;
        }
        .auto-style120 {
            height: 50px;
            width: 404px;
        }
        .auto-style121 {
            width: 404px;
            height: 55px;
        }
        .auto-style122 {
            height: 55px;
        }
        .auto-style123 {
            height: 55px;
            width: 158px;
        }
        .auto-style124 {
            width: 158px;
        }
        .FormatoTextBox, .FormatoDropDownList { width:207px;height:25px; }
        .auto-style125 {
            height: 30px;
            width: 404px;
        }
        .auto-style126 {
            height: 45px;
            width: 404px;
            font-weight: bold;
        }
        .auto-style127 {
            height: 37px;
            width: 404px;
        }
        .auto-style128 {
            width: 404px;
        }
        .auto-style129 {
            height: 36px;
            width: 404px;
        }
        .auto-style130 {
            height: 49px;
            width: 404px;
        }
        </style>
</asp:Content>


