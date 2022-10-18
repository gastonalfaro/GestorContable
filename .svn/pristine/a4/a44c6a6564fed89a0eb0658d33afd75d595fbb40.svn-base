<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmAsientosReversion.aspx.cs" Inherits="Presentacion.CalculosFinancieros.DeudaExterna.Mantenimiento.frmAsientosReversion" %>


<asp:Content ID="Content3" ContentPlaceHolderID="encabezado" runat="server"></asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoJS" runat="server">

    <style type="text/css">
        .auto-style1 { height: 19px; }
        .ButtonNeutro { width:200px;}
        .contenido { width:90%!important; } 
        .divstyle { margin-top:2%;}
    </style>
        
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="Contenido" runat="server">
    <div id="Asientos" style="display:inline-block;">
        <div><h3>Asientos Reversión</h3></div>
        <div class="col-md-12">
            <asp:Label ID="lblDatosBasicos" runat="server" Text="Datos Básicos:" Font-Bold="True" Font-Size="Large"></asp:Label>
        </div>
        <div class="col-md-6">
            <div class="row divstyle">
                <div class="col-md-4">
                    <asp:Label ID="lblFecha" runat="server" Text="Fecha de Documento"></asp:Label></div>
                <div class="col-md-7">
                    <asp:TextBox ID="txtFecha" runat="server" CssClass="js-date-picker FormatoTextBox"></asp:TextBox></div>
            </div>
            <div class="row divstyle">
                <div class="col-md-4">Referencia</div>
                <div class="col-md-7">
                    <asp:TextBox ID="txtReferencia" runat="server" CssClass="FormatoTextBox" MaxLength="16"></asp:TextBox></div>
            </div>                        
        </div>
        <div class="col-md-6">
            <div class="row divstyle">
                <div class="col-md-4">
                    <asp:Label ID="lblMoneda" runat="server" Text="Moneda"></asp:Label></div>
                <div class="col-md-7">
                    <asp:DropDownList ID="dbMoneda" runat="server" CssClass="chzn-select FormatoDropDownList">
                    </asp:DropDownList><%--DataSourceID="Monedas"--%> <%--DataTextField="NomMoneda" DataValueField="IdMoneda" --%>
                    <%--<asp:SqlDataSource ID="Monedas" runat="server" ConnectionString="<%$ ConnectionStrings:GestNICSPDEVConnectionString %>" SelectCommand="SELECT [IdMoneda], [NomMoneda] FROM [Monedas] where [IdMoneda] in ('EUR','USD','CRC','JPY') ORDER BY [NomMoneda]"></asp:SqlDataSource>--%>
                </div>
            </div>
            <div class="row divstyle">
                <div class="col-md-4">
                    <asp:Label ID="lblBloqueo" runat="server" Text="Bloquear encabezado"></asp:Label></div>
                <div class="col-md-7">
                    <asp:CheckBox ID="chkBloqueoEncabezado" runat="server" AutoPostBack="True" OnCheckedChanged="chkBloqueoEncabezado_CheckedChanged" /></div>
            </div>
        </div>

        <div class="col-md-12 divstyle">
            <asp:Label ID="lblDatosRubros" runat="server" Text="Rubros de asiento:" Font-Bold="True" Font-Size="Large"></asp:Label>
        </div>
        <div class="col-md-6">
            <div class="row divstyle">
                <div class="col-md-4">
                    <asp:Label ID="lblGrupoCuentas" runat="server" Text="Grupo de Cuentas"></asp:Label></div>
                <div class="col-md-7">
                    <asp:DropDownList ID="dbGrupoCuentas" runat="server" DataTextField="NomGrupoCuenta" DataValueField="IdGrupoCuenta" AutoPostBack="True" CssClass="chzn-select FormatoDropDownList" OnSelectedIndexChanged="dbGrupoCuentas_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row divstyle">
                <div class="col-md-4">
                    <asp:Label ID="lblCuentas" runat="server" Text="Cuentas"></asp:Label></div>
                <div class="col-md-7">
                    <asp:DropDownList ID="dbCuentas" runat="server" DataTextField="NomCuentaContable" DataValueField="IdCuentaContable" CssClass="chzn-select FormatoDropDownList">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row divstyle">
                <div class="col-md-4">Texto Informativo</div>
                <div class="col-md-7">
                    <asp:TextBox ID="txtTextoInfo" runat="server" CssClass="FormatoTextBox" MaxLength="50"></asp:TextBox></div>
            </div>
            
            <div class="row divstyle">
                <div class="col-md-4">Centro Beneficio</div>
                <div class="col-md-7">
                <asp:DropDownList ID="ddlCentroBeneficio" runat="server" AppendDataBoundItems="true" DataTextField="NomCentroBeneficio" DataValueField="IdCentroBeneficio"  CssClass="chzn-select FormatoDropDownList"> </asp:DropDownList>
                 
                </div>
            </div>
            <div class="row divstyle">
                <div class="col-md-4">Posposición Presupuestaria</div>
                <div class="col-md-7">
                    <asp:DropDownList ID="ddlPosPre" runat="server" CssClass="chzn-select FormatoDropDownList" DataTextField="NomPosPre" DataValueField="IdPosPre"></asp:DropDownList></div>
            </div>
            <div class="row divstyle">
                <div class="col-md-4">Fondo</div>
                <div class="col-md-7">
                    <asp:DropDownList ID="ddlFondo" runat="server" CssClass="chzn-select FormatoDropDownList" DataTextField="NomFondo" DataValueField="IdFondo"></asp:DropDownList></div>
            </div>
            
            <div class="row divstyle">
                <div class="col-md-4">Pos. Doc. Presupuestario</div>
                <div class="col-md-7">
                    <asp:TextBox ID="txtPosDocPres" CssClass="FormatoTextBox" runat="server"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="col-md-6">
            <div class="row divstyle">
                <div class="col-md-4">
                    <asp:Label ID="lblDebeHaber" runat="server" Text="Debe/Haber"></asp:Label></div>
                <div class="col-md-7">
                    <asp:DropDownList ID="dbDebeHaber" runat="server" CssClass="chzn-select FormatoDropDownList">
                        <asp:ListItem Value="40">Debe</asp:ListItem>
                        <asp:ListItem Value="50">Haber</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row divstyle">
                <div class="col-md-4">
                    <asp:Label ID="lblMonto" runat="server" Text="Monto"></asp:Label></div>
                <div class="col-md-7">
                    <asp:TextBox ID="txtMonto" runat="server" ToolTip="Monto a asignar para la cuenta seleccionada" CssClass="FormatoTextBox" onkeypress="return AceptarSoloNumerosMonto(event)" onclick="Formateo_Monto(this)" onchange="Formateo_Monto(this)"></asp:TextBox></div>
            </div>
            <div class="row divstyle">
                <div class="col-md-4">CentroCosto</div>
                <div class="col-md-7">
                    <asp:DropDownList ID="ddlCentroCosto" runat="server" CssClass="chzn-select FormatoDropDownList" DataTextField="NomCentroCosto" DataValueField="IdCentroCosto"></asp:DropDownList>
                </div>
            </div>
            <div class="row divstyle">
                <div class="col-md-4">Elemento PEP</div>
                <div class="col-md-7">
                    <asp:DropDownList ID="ddlElementoPEP" runat="server" CssClass="chzn-select FormatoDropDownList" DataTextField="NomElementoPEP" DataValueField="IdElementoPEP"></asp:DropDownList></div>
            </div>
            <div class="row divstyle">
                <div class="col-md-4">Centro Gestor</div>
                <div class="col-md-7">
                    <asp:DropDownList ID="ddlCentroGestor" runat="server" CssClass="chzn-select FormatoDropDownList" DataTextField="NomCentroGestor" DataValueField="IdCentroGestor"></asp:DropDownList></div>
            </div>
            <div class="row divstyle">
                <div class="col-md-4">Documento Presupuestario</div>
                <div class="col-md-7">
                    <asp:DropDownList ID="ddlDocPres" runat="server" CssClass="chzn-select FormatoDropDownList" DataTextField="NomReserva" DataValueField="IdReserva"></asp:DropDownList></div>
            </div>
        </div>
        <div class="col-md-12" style="text-align: center; margin-top:5%;">
            <asp:Button ID="btnAgregarRubro" runat="server" Text="Agregar Rubro" OnClick="btnAgregarRubro_Click" CssClass="ButtonNeutro" />
            <asp:Button ID="btnEnviarAsiento" runat="server" Text="Enviar Asiento" CssClass="ButtonNeutro" OnClientClick="return confirm('Una vez confirmado, el asiento se enviará a SIGAF. ¿Seguro que desea enviar el asiento?');" OnClick="btnEnviarAsiento_Click" />
            <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar Asiento" CssClass="ButtonNeutro" OnClientClick="return confirm('Si continua, el asiento se perderá. ¿Seguro que desea limpiar el asiento?');" OnClick="btnLimpiar_Click" />
        </div>

    </div>
    <%--<div class="col-md-12"  id="dgAsiento" style="overflow-y:auto">--%>
        <div style="overflow-y:auto; width: auto; margin-top:3%;">
        <asp:Label ID="lblAsiento" runat="server" Text="Asiento de Ajuste:" Font-Bold="True" Font-Size="Large"></asp:Label>        
        <asp:GridView ID="gvAsiento" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" OnRowCommand="BorrarRubro" ShowHeaderWhenEmpty="True" EmptyDataText="No hay registros para contabilizar." style="white-space:nowrap; align-content:center;"
              CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
            <Columns>
                <asp:BoundField DataField="Fecha" HeaderText="Fecha de Transacción" />
                <asp:BoundField DataField="Cuenta" HeaderText="Cuenta Contable" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre de Cuenta"  />
                <asp:BoundField DataField="Debe" HeaderText="Valor del Debe" />
                <asp:BoundField DataField="Haber" HeaderText="Valor del Haber" />
                <asp:BoundField DataField="Moneda" HeaderText="Moneda de  Transacción" />
                <asp:BoundField DataField="TextoInfo" HeaderText="Texto Informativo" />
                <asp:BoundField DataField="CentroCosto" HeaderText="Centro de Costo" />
                <asp:BoundField DataField="CentroBeneficio" HeaderText="Centro de Beneficio" />
                <asp:BoundField DataField="ElementoPEP" HeaderText="Elemento PEP" />
                <asp:BoundField DataField="PosPre" HeaderText="Posición Presupuestaria" />
                <asp:BoundField DataField="CentroGestor" HeaderText="Centro Gestor" />
                <asp:BoundField DataField="Fondo" HeaderText="Fondo" />
                <asp:BoundField DataField="DocPres" HeaderText="Documento Presupuestario" />
                <asp:BoundField DataField="PosDocPres" HeaderText="Pos. Doc. Presupuestario" />
                <asp:TemplateField HeaderText="Borrar" HeaderImageUrl="~/Compartidas/imagenes/1444297028_Delete.png">
                    <ItemTemplate>
                        <asp:Button ID="btnBorrar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClientClick="return confirm('Si elimina la fila, debe de incluirla nuevamente. ¿Seguro que desea eliminar la línea?');" CommandName="Borrar" runat="server" Text="Borrar" BackColor="#3366CC" ForeColor="White" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EditRowStyle BackColor="#999999" />
        </asp:GridView>

    </div>

</asp:Content>
