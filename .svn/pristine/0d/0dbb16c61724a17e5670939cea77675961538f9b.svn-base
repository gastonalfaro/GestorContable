<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmNuevoServicio.aspx.cs" Inherits="Presentacion.Mantenimiento.Gestiones.frmNuevoServicio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoJS" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Contenido" runat="server">
    <div class="FormatoBotones"><asp:Button ID="btnServicioVolver" runat="server" Text="VOLVER" OnClick="btnServicioVolver_Click" CssClass="ButtonNeutro" /></div> 
   <div style="display:inline-block;">
     <div class="col-md-12">
        <h3>Servicios</h3>
        <p>Mantenimiento de Servicios del Sistema Gestor.</p>
        <div class="col-md-6">
            <div class="col-md-5"><asp:Label ID="Label1" runat="server" Text="Código Servicio:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7"><asp:TextBox ID="txtIdServicio" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-5"><asp:Label ID="Label4" runat="server" Text="Institución:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7">
                <asp:DropDownList ID="ddlSociedades" runat="server" OnSelectedIndexChanged="ddlSociedades_SelectedIndexChanged" OnTextChanged="ddlSociedades_TextChanged"
                        OnDataBinding="ddlSociedades_DataBinding" AutoPostBack="true" CssClass="FormatoDropDownList"></asp:DropDownList>
            </div>
        </div>
        <div class="col-md-6">
            <div class="col-md-5"><asp:Label ID="Label3" runat="server" Text="Oficina:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7"><asp:DropDownList ID="ddlOficinas" runat="server"  CssClass="FormatoDropDownList"></asp:DropDownList></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-5"><asp:Label ID="Label5" runat="server" Text="Monto:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7"><asp:TextBox ID="txtMonto" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-5"> <asp:Label ID="Label2" runat="server" Text="Descripción:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7"><asp:TextBox ID="txtDesServicio" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-5"><asp:Label ID="Label6" runat="server" Text="Permite Reserva:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7"><asp:CheckBox ID="ckbReserva" runat="server" /></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-5"><asp:Label ID="Label7" runat="server" Text="Cuenta Contable Debe Actual Dev:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7"><asp:TextBox ID="txtCCDebeActualDev" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-5"><asp:Label ID="Label8" runat="server" Text="Cuenta Contable Haber Actual Dev:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7"><asp:TextBox  ID="txtCCHaberActualDev" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-5"><asp:Label ID="Label15" runat="server" Text="Id PosPre Actual Dev:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7"><asp:TextBox ID="txtIdPosPreActualDev" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-5"><asp:Label ID="Label9" runat="server" Text="Cuenta Contable Debe Actual Per:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7"><asp:TextBox ID="txtCCDebeActualPer" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-5"><asp:Label ID="Label10" runat="server" Text="Cuenta Contable Haber Actual Per:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7"><asp:TextBox ID="txtCCHaberActualPer" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-5"><asp:Label ID="Label16" runat="server" Text="Id PosPre Actual Per:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7"><asp:TextBox ID="txtIdPosPreActualPer" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-5"><asp:Label ID="Label11" runat="server" Text="Cuenta Contable Debe Vencido Dev:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7"><asp:TextBox ID="txtCCDebeVencidoDev" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-5"><asp:Label ID="Label12" runat="server" Text="Cuenta Contable Haber Vencido Dev:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7"><asp:TextBox ID="txtCCHaberVencidoDev" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-5"><asp:Label ID="Label18" runat="server" Text="Id PosPre Vencido Dev:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7"><asp:TextBox ID="txtIdPosPreVencidoDev" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-5"><asp:Label ID="Label13" runat="server" Text="Cuenta Contable Debe Vencido Per:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7"><asp:TextBox ID="txtCCDebeVencidoPer" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-5"> <asp:Label ID="Label14" runat="server" Text="Cuenta Contable Haber Vencido Per:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7"><asp:TextBox  ID="txtCCHaberVencidoPer" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-5"><asp:Label ID="Label17" runat="server" Text="Id PosPre Vencido Per:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7"><asp:TextBox  ID="txtIdPosPreVencidoPer" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-5"><asp:Label ID="Label19" runat="server" Text="Estado:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7"><asp:CheckBox ID="ckbEstado" runat="server" /></div>
        </div>
        <div class="col-md-12" style="text-align:center;"> <asp:Button ID="btnCrearServicio" runat="server" Text="CREAR" OnClick="btnCrearServicio_Click1" CssClass="ButtonNeutro"/></div>
    </div>
    <div class="col-md-12" ><asp:label ID="lblMensaje" runat="server" Visible="false" Font-Bold="true" ></asp:label></div>
    </div>
</asp:Content>
