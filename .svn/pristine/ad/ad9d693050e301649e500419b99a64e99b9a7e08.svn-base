<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmNuevoTipoAsiento.aspx.cs" Inherits="Presentacion.Mantenimiento.Gestiones.frmNuevoTipoAsiento" %>
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
    <div  id="tblServicios" style="display:inline-block;width:100%">
        <h3>Tipos de Asiento</h3>
        <p> Mantenimiento de Tipos de Asiento del Sistema Gestor.</p>
        <div class="col-md-6">
            <div class="col-md-8"><asp:Label ID="Label35" runat="server" Text="Entidad CP:" Font-Bold="true"></asp:Label>   </div>
            <div class="col-md-7"><asp:DropDownList ID="ddlEntidadCP" runat="server" CssClass="FormatoTextBox" DataTextField="NomEntidadCP" DataValueField="IdEntidadCP" AutoPostBack="True" OnSelectedIndexChanged="CambioEntidadCP"></asp:DropDownList></div>

            <div class="col-md-8"><asp:Label ID="Label36" runat="server" Text="Sociedad Costos:" Font-Bold="true"></asp:Label>   </div>
            <div class="col-md-7"><asp:DropDownList ID="ddlSociedadCO" runat="server" CssClass="FormatoTextBox" DataTextField="NomSociedad" DataValueField="IdSociedadCo" AutoPostBack="True" OnSelectedIndexChanged="CambioSociedadCo"></asp:DropDownList></div>
 
            <div class="col-md-8"><asp:Label ID="Label37" runat="server" Text="Sociedad FI:" Font-Bold="true"></asp:Label>   </div>
            <div class="col-md-7"><asp:DropDownList ID="ddlSociedadFi" runat="server" CssClass="FormatoTextBox" DataTextField="NomSociedad" DataValueField="IdSociedadFi" AutoPostBack="True" OnSelectedIndexChanged="CambioSociedadFi"></asp:DropDownList></div>
        
            <div class="col-md-8"><asp:Label ID="Label34" runat="server" Text="Módulo:" Font-Bold="true"></asp:Label>   </div>
            <div class="col-md-7"><asp:DropDownList ID="ddlModulos" runat="server" CssClass="FormatoTextBox"></asp:DropDownList></div>
        
            <div class="col-md-8"><asp:Label ID="Label29" runat="server" Text="Código de Operación:" Font-Bold="true"></asp:Label>   </div>
            <div class="col-md-7"><asp:TextBox ID="txtIDOperacion" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        
            <div class="col-md-8"><asp:Label ID="Label1" runat="server" Text="Código:" Font-Bold="true"></asp:Label>   </div>
            <div class="col-md-7"><asp:TextBox ID="txtCodigo" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
       
            <div class="col-md-8"><asp:Label ID="Label30" runat="server" Text="Código Auxiliar:" Font-Bold="true"></asp:Label>   </div>
            <div class="col-md-7"><asp:TextBox ID="txtCodigoAuxiliar1" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        
            <div class="col-md-8"><asp:Label ID="Label31" runat="server" Text="Código  Auxiliar 2:" Font-Bold="true"></asp:Label>   </div>
            <div class="col-md-7"><asp:TextBox ID="txtCodigoAuxiliar2" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
       
            <div class="col-md-8"><asp:Label ID="Label32" runat="server" Text="Código Auxiliar 3:" Font-Bold="true"></asp:Label>   </div>
            <div class="col-md-7"><asp:TextBox ID="txtCodigoAuxiliar3" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        
            <div class="col-md-8"><asp:Label ID="Label33" runat="server" Text="Código Auxiliar 4:" Font-Bold="true"></asp:Label>   </div>
            <div class="col-md-7"><asp:TextBox ID="txtCodigoAuxiliar4" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        
            <div class="col-md-8"><asp:Label ID="Label38" runat="server" Text="Código Auxiliar 5:" Font-Bold="true"></asp:Label>   </div>
            <div class="col-md-7"><asp:TextBox ID="txtCodigoAuxiliar5" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
      
        
            <div class="col-md-8"><asp:Label ID="Label39" runat="server" Text="Código Auxiliar 6:" Font-Bold="true"></asp:Label>   </div>
            <div class="col-md-7"><asp:TextBox ID="txtCodigoAuxiliar6" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
      
        
            <div class="col-md-8"><asp:Label ID="Label40" runat="server" Text="Secuencia:" Font-Bold="true"></asp:Label>   </div>
            <div class="col-md-7"><asp:TextBox ID="txtSecuencia" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
      
      
            <div class="col-md-8"><asp:Label ID="Label4" runat="server" Text="Código Clave Contable:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7"><asp:TextBox ID="txtIDClaveContable1" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
       
            <div class="col-md-8"><asp:Label ID="Label12" runat="server" Text="Código Cuenta Contable:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7"><asp:TextBox ID="txtIDCuentaContable1" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>

            <div class="col-md-8"><asp:Label ID="Label3" runat="server" Text="Centro de costos:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7"><asp:DropDownList ID="ddlCentroCosto1" runat="server" CssClass="FormatoDropDownList" ></asp:DropDownList></div>
        
            <div class="col-md-8"><asp:Label ID="Label5" runat="server" Text="Centro de Beneficios:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7"> 
                <asp:DropDownList ID="ddlCentroBeneficio1" runat="server" AppendDataBoundItems="true" CssClass="FormatoDropDownList"> </asp:DropDownList>               
            </div>
       
            <div class="col-md-8"><asp:Label ID="Label2" runat="server" Text="Elemento PEP:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7">
                    <asp:DropDownList ID="ddlElementoPEP1" runat="server" CssClass="FormatoDropDownList" ></asp:DropDownList></div>
       
            <div class="col-md-8"><asp:Label ID="Label6" runat="server" Text="Posición Presupuestaria:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7"> 
                    <asp:DropDownList ID="ddlPosPre1" runat="server" CssClass="FormatoDropDownList"></asp:DropDownList></div>
        
            <div class="col-md-8"> <asp:Label ID="Label7" runat="server" Text="Centro Gestor:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7"><asp:DropDownList ID="ddlCentroGestor1" runat="server" CssClass="FormatoDropDownList" ></asp:DropDownList></div>
        
            <div class="col-md-8"><asp:Label ID="Label8" runat="server" Text="Programa:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7"><asp:TextBox ID="txtPrograma1" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>

            <div class="col-md-8"><asp:Label ID="Label15" runat="server" Text="Fondo:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7">  <asp:DropDownList ID="ddlFondo1" runat="server" CssClass="FormatoDropDownList"></asp:DropDownList></div>
        </div>

        <div class="col-md-6">
            <div class="col-md-8"><asp:Label ID="Label9" runat="server" Text="Documento Presupuestario:" Font-Bold="true"></asp:Label></div>
             <div class="col-md-7"><asp:DropDownList ID="ddlDocPre1" runat="server" CssClass="FormatoDropDownList"></asp:DropDownList></div>

            <div class="col-md-8"> <asp:Label ID="Label10" runat="server" Text="Posición Documento Presupuestario:" Font-Bold="true"></asp:Label></div>
           <div class="col-md-7"><asp:TextBox ID="txtDocPres1" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
       
            <div class="col-md-8"><asp:Label ID="Label16" runat="server" Text="Flujo Efectivo:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7"><asp:TextBox ID="txtFlujoEfectivo1" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
       
            <div class="col-md-8"><asp:Label ID="Label11" runat="server" Text="NICSP24:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7"><asp:TextBox ID="txtNICSP241" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
       
            <div class="col-md-8"><asp:Label ID="Label13" runat="server" Text="Código Clave Contable 2:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7"><asp:TextBox ID="txtIDClaveContable2" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
       
            <div class="col-md-8"><asp:Label ID="Label14" runat="server" Text="Código Cuenta Contable 2:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7"><asp:TextBox ID="txtIDCuentaContable2" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
       
            <div class="col-md-8"><asp:Label ID="Label17" runat="server" Text="Centro de costos 2:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7"><asp:DropDownList ID="ddlCentroCosto2" runat="server" CssClass="FormatoDropDownList" DataTextField="NomCentroCosto" DataValueField="IdCentroCosto"></asp:DropDownList></div>
       
            <div class="col-md-8"><asp:Label ID="Label18" runat="server" Text="Centro de Beneficios 2:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7"> 
                <asp:DropDownList ID="ddlCentroBeneficio2" runat="server" AppendDataBoundItems="true" DataTextField="NomCentroBeneficio" DataValueField="IdCentroBeneficio"  CssClass="FormatoDropDownList"> </asp:DropDownList>         
            </div>
       
            <div class="col-md-8"><asp:Label ID="Label20" runat="server" Text="Elemento PEP 2:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7">
                    <asp:DropDownList ID="ddlElementoPEP2" runat="server" CssClass="FormatoDropDownList" DataTextField="NomElementoPEP" DataValueField="IdElementoPEP"></asp:DropDownList></div>
       
            <div class="col-md-8"><asp:Label ID="Label21" runat="server" Text="Posición Presupuestaria 2:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7"> 
                    <asp:DropDownList ID="ddlPosPre2" runat="server" CssClass="FormatoDropDownList" DataTextField="NomPosPre" DataValueField="IdPosPre"></asp:DropDownList></div>
        
            <div class="col-md-8"> <asp:Label ID="Label22" runat="server" Text="Centro Gestor 2:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7">
                    <asp:DropDownList ID="ddlCentroGestor2" runat="server" CssClass="FormatoDropDownList" ></asp:DropDownList></div>
       
            <div class="col-md-8"><asp:Label ID="Label23" runat="server" Text="Programa 2:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7"><asp:TextBox ID="txtPrograma2" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        
            <div class="col-md-8"><asp:Label ID="Label24" runat="server" Text="Fondo 2:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7">  <asp:DropDownList ID="ddlFondo2" runat="server" CssClass="FormatoDropDownList" DataTextField="NomFondo" DataValueField="IdFondo"></asp:DropDownList></div>
        
            <div class="col-md-8"><asp:Label ID="Label25" runat="server" Text="Documento Presupuestario 2:" Font-Bold="true"></asp:Label></div>
             <div class="col-md-7"><asp:DropDownList ID="ddlDocPre2" runat="server" CssClass="FormatoDropDownList"></asp:DropDownList></div>
       
            <div class="col-md-8"> <asp:Label ID="Label26" runat="server" Text="Posición Documento Presupuestario 2:" Font-Bold="true"></asp:Label></div>
           <div class="col-md-7"><asp:TextBox ID="txtDocPres2" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
       
            <div class="col-md-8"><asp:Label ID="Label27" runat="server" Text="Flujo Efectivo 2:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7"><asp:TextBox ID="txtFlujoEfectivo2" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        
            <div class="col-md-8"><asp:Label ID="Label28" runat="server" Text="NICSP24 2:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7"><asp:TextBox ID="txtNICSP242" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        
            <div class="col-md-8"><asp:Label ID="Label19" runat="server" Text="Estado:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7"><asp:CheckBox ID="ckbEstado" runat="server" /></div>
        </div>
        <div class="col-md-12" style="text-align:center;">  <asp:Button ID="btnCrearServicio" runat="server" Text="CREAR" OnClick="btnCrearServicio_Click1" CssClass="ButtonNeutro" /></div>
    </div>
    <div class="col-md-12" ><asp:label ID="lblMensaje" runat="server" Visible="false" Font-Bold="true" ></asp:label></div>
</asp:Content>
