<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="Autorizados.aspx.cs" Inherits="Presentacion.Perfil.Autorizados" %>
    
<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoJS" runat="server">
    <script src="https://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
    <!--RAMSES JAVASCRIPT-->
    <script src="https://www.java.com/js/deployJava.js" type="text/javascript"></script>
    <script type="text/javascript">

        var XML_AUTORIZACION_SIGNED = "";
        var CEDULA_PERSONA_QUE_AUTORIZA = "";//ESTE VALOR LO DA EL APPLET 
        var CEDULA_EMPRESA_DONDE_SE_AUTORIZA = '<%=Request.QueryString["Empresa"]%>';//ESTE VALOR LO DA EL ASPX.NET POR MEDIO DE LA URL USANDO [ GET ]

        function get_info_autorizacion() {
            var CEDULA_PERSONA_A_QUIEN_SE_AUTORIZA = document.getElementById("Contenido_txtIdentificacion").value;
            document.getElementById("Contenido_id_pesona_a_quien_se_autorisa").value = CEDULA_PERSONA_A_QUIEN_SE_AUTORIZA;
            return CEDULA_PERSONA_A_QUIEN_SE_AUTORIZA + ";" + CEDULA_EMPRESA_DONDE_SE_AUTORIZA;
        }
        function mtr() {
            alert(get_info_autorizacion());
        }

        function write_xml_signed_on_input() {
            document.getElementById("Contenido_txb_out").value = XML_AUTORIZACION_SIGNED;
            document.getElementById("Contenido_CEDULA_PERSONA_QUE_AUTORIZA").value = CEDULA_PERSONA_QUE_AUTORIZA
            document.getElementById("Contenido_h_listen_firma").value = "C#234?9$#1$9238478rTXK";
        }//FUNCION

        function firmar() {
            document.appletFirma.proceso_firma_digita();
        }//FUNCION

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Contenido" runat="server">
    <link href="../Compartidas/EstiloPagina.css" rel="stylesheet" type="text/css" />
        <br />
    <h2>Autorizados</h2>
    <br />
    <div>
        <asp:Label ID="lblSinResultados" runat="server" Text="No se han registrado autorizados" ForeColor="Red" Visible="False" Font-Italic="True"></asp:Label>
        <asp:GridView ID="gvAutorizados" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center" OnRowUpdating="gvAutorizados_RowUpdating"
              CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
            <Columns>
                <asp:TemplateField AccessibleHeaderText="Tipo" HeaderText="Tipo Id.">
                    <ItemTemplate>
                        <asp:Label ID="lblTipoId" runat="server" Text='<%# Bind("TipoIdPersonaAutorizada") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField AccessibleHeaderText="Identificación" HeaderText="Identificación">
                    <ItemTemplate>
                        <asp:Label ID="lblIdPersona" runat="server" Text='<%# Bind("IdPersonaAutorizada") %>'></asp:Label>
                    </ItemTemplate>

                </asp:TemplateField>
                <asp:TemplateField AccessibleHeaderText="Tipo" HeaderText="Tipo Id. Autorizador" Visible ="false">
                    <ItemTemplate>
                        <asp:Label ID="lblTipoIdPersonaAutoriza" runat="server" Text='<%# Bind("TipoIdPersonaAutoriza") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField AccessibleHeaderText="Identificación" HeaderText="Identificación" Visible ="false">
                    <ItemTemplate>
                        <asp:Label ID="lblIdPersonaAutoriza" runat="server" Text='<%# Bind("IdPersonaAutoriza") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField AccessibleHeaderText="Nombre" HeaderText="Nombre">
                    <ItemTemplate>
                        <asp:Label ID="lblNombrePersona" runat="server" Text='<%# Bind("NombrePersonaAutorizada") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField AccessibleHeaderText="Cuenta IBAN" HeaderText="Cuenta IBAN">
                    <ItemTemplate>
                        <asp:Label ID="lblCtaCliente" runat="server" Text='<%# Bind("CtaCliente") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField AccessibleHeaderText="Puesto" HeaderText="Puesto">
                    <ItemTemplate>
                        <asp:Label ID="lblPuesto" runat="server" Text='<%# Bind("PuestoPersonaAutorizada") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField AccessibleHeaderText="Estado" HeaderText="Estado">
                    <ItemTemplate>
                        <asp:Label ID="lblEstado" runat="server" Text='<%# Bind("Estado") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="" ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkEditar" runat="server" CausesValidation="False" Text="Editar" CommandName="Update"></asp:LinkButton> 
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EditRowStyle BackColor="#999999" />
        </asp:GridView>
        <br />
        <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" OnClick="btnNuevo_Click" CssClass="ButtonNeutro" Width="83px" />
        <asp:Button ID="btnAtras" runat="server" OnClick="Button1_Click" Text="Atrás" CssClass="ButtonNeutro" Width="78px" />
    </div>
    <br />
    <div>
        <asp:Panel ID="pnlNuevoAutorizado" runat="server" Visible="false">
            <table style="width: 100%;">
                <tr>
                    <td style="width: 117px">
                        <asp:Label ID="lblTipoIdentificacion" runat="server" Text="Tipo Identificación"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlTipoIdentificacion" runat="server" ReadOnly="True" OnSelectedIndexChanged="ddlTipoIdentificacion_SelectedIndexChanged">
                            <asp:ListItem Value="F">Físico</asp:ListItem>
                            <asp:ListItem Value="DI">DIMEX</asp:ListItem>
                        </asp:DropDownList>

                    </td>
                    <td>
                        <asp:Label ID="lblIdentificacion" runat="server" Text="Num Identificación"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtIdentificacion" runat="server" AutoPostBack="True" OnTextChanged="txtIdentificacion_TextChanged" ValidateRequestMode="Enabled"></asp:TextBox>
                        <asp:Label ID="lblNombre" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 117px">
                        <asp:Label ID="lblCuentaCliente" runat="server" Text="Cuenta IBAN"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtCuentaCliente" runat="server" Width="200px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblPuestoPersona" runat="server" Text="Puesto"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPuestoPersona" runat="server" ></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox ID="chkHabilitado" runat="server" Text="Activo" TextAlign="Left" />
                    </td>
                </tr><tr>
                    <td style="width: 117px">
                        <asp:Label ID="lblTipoIdentificacionAutoriza" Visible ="false"  runat="server" Text="Tipo Identificación Autorizador"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlTipoIdentificacionAutoriza"  Visible ="false" runat="server" ReadOnly="True" OnSelectedIndexChanged="ddlTipoIdentificacionAutoriza_SelectedIndexChanged">
                            <asp:ListItem Value="F">Físico</asp:ListItem>
                            <asp:ListItem Value="DI">DIMEX</asp:ListItem>
                        </asp:DropDownList>

                    </td>
                    <td>
                        <asp:Label ID="lblIdentificacionAutoriza"  Visible ="false" runat="server" Text="Num Identificación Autorizador"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtIdentificacionAutoriza" runat="server" Visible ="false" AutoPostBack="True" OnTextChanged="txtIdentificacionAutoriza_TextChanged" ValidateRequestMode="Enabled"></asp:TextBox>
                        <%--<asp:Label ID="Label3" runat="server" Text=""></asp:Label>--%>
                    </td>
                </tr>
                </table>
            <br />
            
            
            
            
            
            
            
            <!--_APPLET_-->
            <div id="div_applet">
                <div id="applet_box" visible="true"  runat="server">
                    <script type="text/javascript">
                        var attributes = {
                            id: 'appletFirma',
                            code: 'Vista.Applet_Firma_Digital_Autorizaciones.class',
                            archive: 'Applet_AUTORIZACIONES.jar',
                            width: 220, height: 100
                        };
                        var parameters = { fontSize: 16 };
                        var version = '1.6';
                        deployJava.runApplet(attributes, parameters, version);
                    </script>
                </div>

                <asp:HiddenField ID="CEDULA_PERSONA_QUE_AUTORIZA" runat="server" />
                <asp:HiddenField ID="TIPO_CEDULA_PERSONA_QUE_AUTORIZA" runat="server" />
                <asp:HiddenField ID="txb_out" runat="server" />
                <asp:HiddenField ID="id_pesona_a_quien_se_autorisa" runat="server" />
                <asp:HiddenField ID="tipo_id_pesona_a_quien_se_autorisa" runat="server" />
                <asp:HiddenField ID="h_listen_firma" runat="server" />
                <!--<input type="button" onclick="mtr()" value="Mostrar" />-->
            </div>
            
            
            
            
            
            
            
            
            
            <asp:Button ID="btnCrearAutorizado" runat="server" Text="Guardar" OnClientClick="firmar()" CssClass="ButtonNeutro" Width="100px" OnClick="btnCrearAutorizado_Click1" />
        </asp:Panel>
    </div>
</asp:Content>

