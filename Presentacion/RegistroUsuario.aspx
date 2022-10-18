<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="RegistroUsuario.aspx.cs" Inherits="Presentacion.Seguridad.GestionUsuarios.RegistroUsuario" %>
<asp:Content ID="ContentScripts" ContentPlaceHolderID="ContenidoJS" runat="server">
     <link rel="stylesheet" href="Compartidas/rmm-css/Login.css"/>
    
    <!--<script src="https://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
        <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.0.0-alpha1/jquery.min.js"></script>-->
        <script>
            $(document).ready(function () {
                cargAnnombre();
            });
            function cargAnnombre() {
                debugger;
                $('#<%=txtNombre.ClientID %>').val('');
                var ced = $('#<%=txtCedula.ClientID %>').val();
                var tipo = $('#<%=ddlOrigen.ClientID %> option:selected').val();
                if (ced.length >= 10) {
                    $('#<%=txtNombre.ClientID %>').val('');
                $.getJSON('https://www.hacienda.go.cr/ldap/buscar_persona3.php', { cedula: ced, origen: tipo }, function (datos) {   //ESTE USA UN SERVICIO
                    if (datos["primer apellido"] == undefined && datos["segundo apellido"] == undefined)
                    { var html = datos["nombre"]; }
                    else if (datos["segundo apellido"] == undefined)
                    { var html = datos["nombre"] + ' ' + datos["primer apellido"]; }
                    else
                    { var html = datos["nombre"] + ' ' + datos["primer apellido"] + ' ' + datos["segundo apellido"]; }
                    $('#<%=txtNombre.ClientID %>').val(html);
                });
            }
            else
            {
                $("input[id$='Contenido_txtNombre']").val('');
            }
        }
    </script>
        <style type="text/css">
            .auto-style1 {
                width: 288px;
            }
            .auto-style2 {
                width: 116px;
            }
            .login {
                width: 410px;
            }
        </style>
    </asp:Content> 
<asp:Content ID="Content1" ContentPlaceHolderID="encabezado" runat="server">
        <div class="rmm">
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenido" runat="server">
    <div class="login">   
     <table class="asd">     
        <tr>
            <td colspan="2" class="titulotabla">Registro de Usuario</td>
        </tr>
        <tr>
            <td class="auto-style2">
                Tipo Identificación:&nbsp;
            </td>
            <td class="auto-style1">
                <asp:DropDownList ID="ddlOrigen" runat="server" onchange="cargAnnombre()" >
                    <asp:ListItem Value="Fisico">Físico</asp:ListItem>
                    <asp:ListItem Value="DIMEX">DIMEX</asp:ListItem>
                </asp:DropDownList>            
            </td>
        </tr>

        <tr>
            <td class="auto-style2">
                Cédula
            </td>
            <td class="auto-style1">
                <asp:TextBox ID="txtCedula" runat="server"  MaxLength="13" onkeyup="cargAnnombre()" style="margin-right: 73px" OnTextChanged="txtCedula_TextChanged"/>
                <br />
                <asp:RegularExpressionValidator ForeColor="Red" Display="Dynamic" ControlToValidate="txtCedula" runat="server" ErrorMessage="Número de cédula inválido. Cédulas físicas inician con 0" ValidationExpression="\d+"></asp:RegularExpressionValidator>
            </td>            
        </tr>
        <tr>
            <td class="auto-style2">
                Nombre</td>
            <td class="auto-style1">
                <asp:TextBox ID="txtNombre" runat="server" ReadOnly="True" ClientIDMode="Static"></asp:TextBox>
             </td>
        </tr>
        <asp:Panel ID="pnlContrasena" runat="server">  
        <tr>
            
            <td class="auto-style2">
                <asp:Label ID="lblContrasena" runat="server" Text="Contraseña"></asp:Label>
            </td>
            <td class="auto-style1">
                <asp:TextBox ID="txtContrasena" runat="server" TextMode="Password" />
                <asp:Label id="label1" ToolTip="Los símbolos permitidos son: !,#,%,&,*,-,_,?@" runat="server" Font-Bold="True">?</asp:Label>
                <br />
           
                <asp:RegularExpressionValidator Display = "Dynamic" ForeColor="Red" ControlToValidate = "txtContrasena" ValidationExpression = "^[\s\S]{8,}$" runat="server" ErrorMessage="La contraseña debe tener mínimo 8 caracteres."></asp:RegularExpressionValidator>
            </td>
            
        </tr>
        <tr>
            <td class="auto-style2">
                <asp:Label ID="lblConfContrasena" runat="server" Text="Confirme la contraseña"></asp:Label>
            </td>
            <td class="auto-style1">
                <asp:TextBox ID="txtConfirmarContrasena" runat="server" TextMode="Password" />
                <br />
           
                <asp:CompareValidator ErrorMessage="Las contraseñas no concuerdan." ForeColor="Red" ControlToCompare="txtContrasena"
                    ControlToValidate="txtConfirmarContrasena" runat="server" />
            </td>
        </tr>
        </asp:Panel> 
        <tr>
            <td class="auto-style2">
                Correo electrónico
            </td>
            <td class="auto-style1">
                <asp:TextBox ID="txtEmail" runat="server" />
                <br />
                 <asp:RegularExpressionValidator runat="server" Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                    ControlToValidate="txtEmail" ForeColor="Red" ErrorMessage="Dirección de correo electrónico inválida." />
            </td>
        </tr>
        <tr>
            <td class="auto-style2">
               Confirme correo electrónico
            </td>
            <td class="auto-style1">
                <asp:TextBox ID="txtConfCorreo" runat="server" />
                <br />

                <asp:RegularExpressionValidator runat="server" Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                    ControlToValidate="txtEmail" ForeColor="Red" ErrorMessage="Dirección de correo electrónico inválida." />
            </td>
        </tr>
        <tr>
            <td class="auto-style2">
            </td>
            <td class="auto-style1">
                <asp:Button Text="Aceptar" runat="server" id="btnRegistrarUsuario" CssClass="ButtonNeutro" OnClick="btnRegistrarUsuario_Click" Width="30%" />
                <asp:Button Text="Atrás" runat="server" id="btnAtras" CssClass="ButtonNeutro" OnClick="btnAtras_Click" Width="30%" CausesValidation="False" />
            </td>
           
        </tr>
    </table>
        </div>
    <asp:Label id="lblMensajeConfirmacion" ForeColor="red" Font-Name="Verdana" Font-Size="10" runat="server" visible="false"/>
</asp:Content>
