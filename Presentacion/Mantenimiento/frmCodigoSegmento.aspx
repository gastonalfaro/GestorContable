<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmCodigoSegmento.aspx.cs" Inherits="Presentacion.Mantenimiento.frmCodigoSegmento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenido" runat="server">


    <div class="col-md-12" id="tblCodSegmento">
        <h2>CÓDIGO DE SEGMENTO DE LAS INSTITUCIONES</h2>
        <br />
    </div>
    <%--CssClass="form-control"--%>

    <div class="form-group row">
        <div class="col-md-2">
            <asp:Label ID="cboEntidad" runat="server" Text="Entidad: "></asp:Label></div>
        <div class="col-md-5">
            <asp:DropDownList ID="cboEntidadDropDownList" runat="server" AutoPostBack="true"
                CssClass="chzn-select form-control"
                DataTextField="CodyNombre" DataValueField="IdSociedadGL"
                OnSelectedIndexChanged="cboEntidadDropDownList_SelectedIndexChanged">
            </asp:DropDownList>
        </div>
    </div>

    <div class="form-group row">
        <label for="txtCodSegmento" class="col-sm-2 col-form-label">Código Segmento</label>
        <div class="col-sm-5">
            <asp:TextBox ID="txtCodSegmento" runat="server" CssClass="form-control" MaxLength="3" TabIndex="1" ></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                ControlToValidate="txtCodSegmento"
                ErrorMessage="Código de Segmento solo acepta números" ForeColor="Red"
                ValidationExpression="^\d+(?:[\.]\d+)?$" ValidationGroup="NumericValidate">*
            </asp:RegularExpressionValidator>
        </div>
    </div>

    <div class="form-group row">
        <label class="col-sm-2 col-form-label"></label>
        <div class="col-sm-2">
            <asp:Button ID="btnGuardarCodSegmento" runat="server" Text="Guardar" OnClick="btnGuardarCodSegmento_Click" CssClass="btn btn-primary" TabIndex="2" />
        </div>

    </div>

    <div style="text-align:right">
            <label for="txtCodSegmento" >Buscar</label>
                <asp:TextBox ID="txtBuscar" runat="server"  MaxLength="30" ></asp:TextBox>
                <asp:Button ID="btnBuscar" runat="server" Text="..."   OnClick="btnBuscar_Click" ToolTip="Buscar Entidad"/>
       </div>        
        
        <asp:GridView ID="grdvCodigoSegmento" runat="server" OnSelectedIndexChanged="grdvCodigoSegmento_SelectedIndexChanged"
            AutoGenerateColumns="false" DataKeyNames="IdSociedad, NomSociedad, IdSegmento" AllowPaging="True" ShowFooter="false" CssClass="table table-striped table-hover" 
            EmptyDataText="No hay Datos" 
            AutoGenerateSelectButton="True"
            OnPageIndexChanging="grdvCodigoSegmento_PageIndexChanging" PageSize="15"
             >
            <Columns>
                <asp:TemplateField HeaderText="Código Sociedad" Visible="true" SortExpression="IdSociedad">
                    <ItemTemplate>
                        <asp:Label ID="IdSociedadLabel" runat="server" Text='<%# Bind("IdSociedad") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Nombre Sociedad" Visible="true" SortExpression="NomSociedad">
                    <ItemTemplate>
                        <asp:Label ID="NomSociedadLabel" runat="server" Text='<%# Bind("NomSociedad") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Código Segmento" Visible="true" SortExpression="IdSegmento">
                    <ItemTemplate>
                        <asp:Label ID="IdSegmentoLabel" runat="server" Text='<%# Bind("IdSegmento") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerSettings FirstPageText="Primera" LastPageText="Última" NextPageText="Siguiente" />
            <SelectedRowStyle BackColor="#A1DCF2" Font-Bold="True" ForeColor="#333333" />
        </asp:GridView>
    </div>
</asp:Content>
