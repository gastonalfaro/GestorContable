<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Presentacion.Seguridad.GestionUsuarios.Login" %>
<asp:Content ID="ContentScripts" ContentPlaceHolderID="ContenidoJS" runat="server">
    <!--<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>-->
    <script type="text/javascript">
        var userName = "";
        var userID = "";
        function pulsado() {
            debugger;
            var name = userName;
            var address = userID;
            CargarAux();

            //PageMethods.ProcessIT(userName, userID, onSucess, onError);
            //function onSucess(result) {
            //    alert(result);
            //}
            //function onError(result) {
            //    alert('Something wrong.');
            //}
        }
        
        function pulsado() {
            var urlPagina = 'Login.aspx/AutenticarFirma';
            var par = JSON.stringify({ 'str_NombreUsuario': userName, 'str_Cedula': userID });
            $.ajax({
                //debugger
                url: urlPagina,
                type: 'POST',
                data: par,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    console.log(data);
                    switch(data.d)
                    {
                        case "-1":
                            {
                                //window.location = "http://localhost/Portal%20Sistema%20Gestor/RegistroUsuario.aspx"
                                window.location = "RegistroUsuario.aspx"
                                break;
                            }
                        case "-2":
                            {
                                $('#<%=lblMsg.ClientID%>').val("La cuenta se encuentra bloqueada");
                                break;
                            }
                        case "-3":
                            {
                                break;
                            }
                        case "00":
                            {
                                //window.location = "http://localhost/Portal%20Sistema%20Gestor/Principal.aspx";
                                window.location = "Principal.aspx?card=si";
                                break;
                            }
                        default:
                            {
                                $('#<%=lblMsg.ClientID%>').val("Ocurrió un error al realizar la operación");
                            }
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    debugger;
                }
            });
        }


    </script>
     <link rel="stylesheet" href="Compartidas/rmm-css/Login.css"/>
    <style type="text/css">
        .auto-style1 {
            width: 78px;
            height: 49px;
            margin-right: 0px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenido" runat="server">
     <section class="container">
        <div class="login">
            <asp:Panel ID="pnlDatosLogin" runat="server">
            <h1>Ingreso</h1>
            <div style="display:block;text-align:center;">
                <asp:TextBox ID="txtUsuario" runat="server"  placeholder="Usuario"></asp:TextBox>
                <asp:TextBox ID="txtContrasena" runat="server"  placeholder="Contraseña" TextMode="Password"></asp:TextBox>
            </div>   
            
        <p class="remember_me">
            <asp:CheckBox ID="chkPersistCookie" runat="server" Text="Recordarme" autopostback="false" />
        </p>
        <p class="submit">
            <asp:Button ID="btnInicioSesion" runat="server" Text="Login" CssClass="ButtonNeutro"  OnClick="btnInicioSesion_Click"/>
            <label> o </label>
            <asp:Button ID="btnRegistrar" runat="server" Text="Registrarse" CssClass="ButtonNeutro"  OnClick="btnRegistrar_Click"/>
        </p>    
        <asp:Label id="lblMsg" ForeColor="red" Font-Name="Verdana" Font-Size="10" runat="server" />
     
        <div class="login-help">
            <asp:HyperLink ID="lnkOlvCont" runat="server" NavigateUrl="~/RecuperarContrasena.aspx">Olvidó su contraseña?</asp:HyperLink>
        </div>
        <div>
            <h1>Desea ingresar con Firma Digital?</h1> 
            <asp:Button ID="btnFirmaDigital" runat="server" Text="Ingresar" CssClass="ButtonNeutro"  OnClick="btnFirmaDigital_Click" />
            <asp:ImageButton ID="ibtn_Java" src="../Compartidas/imagenes/Java.png"  runat="server" Height="30px" Width="48px" />
            <a href="https://www.java.com/es/download/">Descargar JAVA </a>
        </div>
                </asp:Panel>
        <asp:Panel ID="pnlConfirmacion" runat="server" Visible="False">
            <asp:Label ID="lblCodigo" runat="server" Text="Código: "></asp:Label>
            <br />
            <asp:TextBox ID="txtCodigo" runat="server"></asp:TextBox>
            <br />
            <div style="margin-bottom:1%; margin-top: 3%;">
            <asp:LinkButton runat="server" ID="lkbRecuperar" Text="Recuperar código" ToolTip="¿Olvidó su código de activación?" OnClick="lkbRecuperar_Click"/>
            </div>
            <asp:Label id="lblMensajeConfirmacion" ForeColor="red" Font-Name="Verdana" Font-Size="10" runat="server" />
            <br />
            <asp:Button ID="btnConfirmacion" runat="server" Text="Aceptar" OnClick="btnConfirmacion_Click" />
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />
        </asp:Panel>

                
    <asp:Panel ID="pnlCambioClave" runat="server" Visible="False">
        <table class="asd">
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="lblContrasenaActual" runat="server" Text="Contraseña actual: "></asp:Label>
                </td>
                <td class="auto-style1">
                    <asp:TextBox ID="txtContrasenaActual" runat="server" TextMode="Password"></asp:TextBox>
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="lblNuevaContrasena" runat="server" Text="Nueva contraseña: "></asp:Label>
                </td>
                <td class="auto-style1">
                    <asp:TextBox ID="txtNuevaContrasena" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:Label ID="label1" ToolTip="Los símbolos permitidos son: !,#,%,&,*,-,_,?,@" runat="server" Font-Bold="True">?</asp:Label>
                </td>
                <td>
                    <asp:RegularExpressionValidator Display="Dynamic" ForeColor="Red" ControlToValidate="txtNuevaContrasena" ValidationExpression="^[\s\S]{8,}$" runat="server" ErrorMessage="La contraseña debe tener mínimo 8 caracteres."></asp:RegularExpressionValidator>

                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="lblConfirmacion" runat="server" Text="Confirme la contraseña: "></asp:Label>
                </td>
                <td class="auto-style1">
                    <asp:TextBox ID="txtConfirmacion" runat="server" TextMode="Password"></asp:TextBox>
                </td>
                <td>
                    <asp:CompareValidator ErrorMessage="Las contraseñas no concuerdan." ForeColor="Red" ControlToCompare="txtNuevaContrasena"
                        ControlToValidate="txtConfirmacion" runat="server" />
                </td>
            </tr>
        </table>
        <asp:Button ID="btnAceptarCambio" runat="server" Text="Aceptar" OnClick="btnAceptarCambio_Click" />
    <br />
    </asp:Panel>
    <br />
    <asp:Panel ID="pnlAppletFirma" runat="server" Visible="false">
            <script src="http://www.java.com/js/deployJava.js" type="text/javascript"></script>
            <script type="text/javascript">
                var attributes = {
                    id: 'appletFirma',
                    code: 'Panels.AppletLogin.class',
                    archive: 'FirmaDigitalApplet.jar',
                    width: 400, height: 220
                };
                var parameters = { fontSize: 16 };
                var version = '1.6';
                deployJava.runApplet(attributes, parameters, version);
            </script>
    </asp:Panel>
        
    
    </div>
   
    
  </section>


</asp:Content>
