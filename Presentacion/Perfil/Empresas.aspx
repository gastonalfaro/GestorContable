<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="Empresas.aspx.cs" Inherits="Presentacion.Perfil.Empresas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoJS" runat="server">
        <script src="https://code.jquery.com/ui/1.10.3/jquery-ui.js" type="text/javascript"></script>

    <!--RAMSES JAVASCRIPT-->
        <script src="http://www.java.com/js/deployJava.js" type="text/javascript"></script>
        <script type="text/javascript">

            var XML_EMPRESA_SIGNED = "";
            var CEDULA = "";

            function get_info_empresa() {
                var id = document.getElementById("Contenido_txtIdentificacion").value;
                document.getElementById("Contenido_id_empresa").value = id;
                var nombre = document.getElementById("txtNombreEmp").value;
                var telefono = document.getElementById("Contenido_txtTelefonoEmp").value;
                var mail = document.getElementById("Contenido_txtCorreoEmp").value;
                return id + ";" + nombre + ";" + telefono + ";" + mail;
            }

            function write_xml_signed_on_input() {
                document.getElementById("Contenido_txb_out").value = XML_EMPRESA_SIGNED;
                document.getElementById("Contenido_cedula_persona").value = CEDULA;
                document.getElementById("Contenido_h_listen_firma").value = "C#234?9$#1$9238478rTXK";
            }//FUNCION
            
            function console_msg(msg) {
                window.Console.log(msg);
            }//FUNCION

            function firmar() {
                document.appletFirma.proceso_firma_digita();
            }//FUNCION

        </script>

        <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.0.0-alpha1/jquery.min.js"></script>
        <script type="text/javascript">
            $(document).ready(function () {
                cargaNombre();
            });
                var ced = $('#<%=txtIdentificacion.ClientID %>').val();
                var tipo = "Juridico";
                if (ced.length >= 10) {
                    $('#<%=txtNombreEmp.ClientID %>').val('');
                $.getJSON('https://www.hacienda.go.cr/ldap/buscar_persona3.php', { cedula: ced, origen: tipo }, function (datos) {   //ESTE USA UN SERVICIO
                    if (datos["primer apellido"] == undefined && datos["segundo apellido"] == undefined)
                    { var html = datos["nombre"]; }
                    else if (datos["segundo apellido"] == undefined)
                    { var html = datos["nombre"] + ' ' + datos["primer apellido"]; }
                    else
                    { var html = datos["nombre"] + ' ' + datos["primer apellido"] + ' ' + datos["segundo apellido"]; }
                    $('#<%=txtNombreEmp.ClientID %>').val(html);
                    $("#Contenido_nombre_empresa").val(html);
                });
            }
            else
            {
                $("input[id$='Contenido_txtNombre']").val('');
            }
            

            function confirmar(texto)
            {
                //"Es responsabilidad de cada empresa realizar las autorizaciones correspondientes para los respectivos pagos. Aquellas personas que realicen las autorizaciones a una empresa y que no estén facultados para ello se exponen  a las sanciones respectivas y estipuladas en los artículos XXXXXXXXX"

                var r = confirm(texto);
                if (r == true) {
                }
                else {

                    window.location.href = "/Principal.aspx";
                }
            }
    </script>
    <style type="text/css">
        .auto-style1 {
            width: 153px;
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
    <!--link href="../Compartidas/EstiloPagina.css" rel="stylesheet" type="text/css" /-->
    <br />
    <h2>Mis Sociedades</h2>
    <br />
    <div>
        <asp:Label ID="lblAdvertencia" runat="server" Text="Es responsabilidad de cada empresa realizar las autorizaciones correspondientes para los respectivos pagos.
Aquellas personas que realicen las autorizaciones a una empresa y que no estén facultados para ello se exponen  a las sanciones respectivas y estipuladas en los artículos XXXXXXXXX
" ForeColor="Red" Visible="False" Font-Italic="True"></asp:Label>
    <br />
    <br />
        <asp:Label ID="lblSinResultados" runat="server" Text="No existen sociedades registradas." ForeColor="Red" Visible="False" Font-Italic="True"></asp:Label>
        <asp:GridView ID="gvEmpresas" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center" OnRowCommand="gvEmpresas_RowCommand" OnRowUpdating="gvEmpresas_RowUpdating"
              CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
            <Columns>
                <asp:TemplateField AccessibleHeaderText="Cedula Juridica" HeaderText="Cédula Jurídica">
                    <ItemTemplate>
                        <asp:Label ID="lblIdEmpresa" runat="server" Text='<%# Bind("IdPersonaJuridica") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField AccessibleHeaderText="Nombre" HeaderText="Nombre">
                    <ItemTemplate>
                        <asp:Label ID="lblNombre" runat="server" Text='<%# Bind("Nombre") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField AccessibleHeaderText="Telefono" HeaderText="Teléfono">
                    <ItemTemplate>
                        <asp:Label ID="lblTelefono" runat="server" Text='<%# Bind("TelefonoEmpresa") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField AccessibleHeaderText="CorreoElectronico" HeaderText="Correo Electrónico">
                    <ItemTemplate>
                        <asp:Label ID="lblCorreo" runat="server" Text='<%# Bind("CorreoEmpresa") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="" ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkEditar" runat="server" CausesValidation="False" Text="Editar" CommandName="Update" ></asp:LinkButton> 
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="" ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkAutorizados" runat="server" CausesValidation="False" CommandName="autorizados" CommandArgument='<%#Eval("IdPersonaJuridica")%>' Text="Autorizados"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EditRowStyle BackColor="#999999" />
        </asp:GridView>
        <br />
        <asp:Button ID="btnNuevaEmpresa" runat="server" Text="Nueva" OnClick="btnNuevaEmpresa_Click" CausesValidation="False" CssClass="ButtonNeutro" Width="87px" />
    </div>
    <br />
    <div>
        <asp:Panel ID="pnlNuevaEmpresa" runat="server" Visible="false">
            <table style="width: 100%;">
                <tr>
                    <td>
                        <asp:Label ID="lblIdentificacion" runat="server" Text="Num Identificación"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtIdentificacion" runat="server" AutoPostBack="true" onkeyup="cargaNombre()" OnTextChanged="txtIdentificacion_TextChanged"></asp:TextBox>
                    </td>
                    <td class="auto-style1">
                        <asp:RegularExpressionValidator ForeColor="Red" Display="Dynamic" ControlToValidate="txtIdentificacion" runat="server" ErrorMessage="Número de cédula inválido." ValidationExpression="\d+"></asp:RegularExpressionValidator>
                    </td>
                     <td style="width: 117px">
                        <asp:Label ID="lblNombreEmp" runat="server" Text="Nombre"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNombreEmp" runat="server" ClientIDMode="Static" ReadOnly="true" Width="176px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 117px">
                        <asp:Label ID="lblTelefonoEmp" runat="server" Text="Teléfono"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtTelefonoEmp" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style1">
                        <asp:RegularExpressionValidator ForeColor="Red" Display="Dynamic" ControlToValidate="txtTelefonoEmp" runat="server" ErrorMessage="Número de teléfono inválido." ValidationExpression="\d+"></asp:RegularExpressionValidator>
                    </td>
                    <td>
                        <asp:Label ID="lblCorreoEmp" runat="server" Text="Correo Electrónico"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtCorreoEmp" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RegularExpressionValidator runat="server" Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                            ControlToValidate="txtCorreoEmp" ForeColor="Red" ErrorMessage="Dirección de correo electrónico inválida." />
                    </td>
                </tr>
            </table>



            <!--RAMSES APPLET-->
            <div id="div_applet">
                <div id="applet_box" runat="server">
                    <script type="text/javascript">
                        var attributes = {
                            id: 'appletFirma',
                            code: 'Vista.Applet_Firma_Digital_Empresas.class',
                            archive: 'Applet_EMPRESAS.jar',
                            width: 220, height: 100
                        };
                        var parameters = { fontSize: 16 };
                        var version = '1.6';
                        deployJava.runApplet(attributes, parameters, version);
                    </script>
                </div>

                <asp:HiddenField ID="txb_out" runat="server" />
                <asp:HiddenField ID="cedula_persona" runat="server" />
                <asp:HiddenField ID="id_empresa" runat="server" />
                <asp:HiddenField ID="h_listen_firma" runat="server" />
                <!--<input type="button" onclick="mtr()" value="Mostrar" />-->
            </div>

            <asp:Button ID="btnCrearEmpresa" runat="server" Text="Guardar" OnClientClick="firmar()" CssClass="ButtonNeutro" OnClick="btnCrearEmpresa_Click1" />
        </asp:Panel>
    </div>
</asp:Content>

