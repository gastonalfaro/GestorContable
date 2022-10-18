<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="ResolucionesConsultar.aspx.cs" Inherits="Presentacion.Contingentes.ResolucionesConsultar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoJS" runat="server">
     <style type="text/css">
         .Grid {
            font-size: x-small;
        }    
        
        .auto-style1 {
            height: 56px;
            width: 260px;
        }
        .titulotabla {
            text-align: left;
            font-weight: 700;
            color: #3366CC;
            font-size: medium;
        }
         .auto-style2 {
             font-size: medium;
         }
        </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="server">
     <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css" />
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>  
<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<script type="text/javascript">
    debugger;
    $("[src*=plus]").live("click", function () {
        $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
        $(this).attr("src", "../Compartidas/imagenes/minus.png");
    });
    $("[src*=minus]").live("click", function () {
        $(this).attr("src", "../Compartidas/imagenes/plus.png");
        $(this).closest("tr").next().remove();
    });
</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="encabezado" runat="server">
</asp:Content>
<%--<asp:Content ID="Content4" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>--%>
<asp:Content ID="Content5" ContentPlaceHolderID="Contenido" runat="server">
    <div class="col-md-12">
        <div class="col-md-6">
            <div class="col-md-3"><strong>Número de expediente</strong></div>
            <div class="col-md-5"> <asp:TextBox ID="txtNumExp" runat="server"  CssClass="FormatoTextBox" ></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-3"><strong>Fecha Inicio</strong></div>
            <div class="col-md-5">
               <asp:TextBox ID="txtFechaDesde" runat="server" CssClass="js-date-picker FormatoTextBox"></asp:TextBox>
            </div>
        </div>
        <div class="col-md-6">
            <div class="col-md-3"><strong>Fecha Fin</strong></div>
            <div class="col-md-5"><asp:TextBox ID="txtFechaHasta" runat="server" CssClass="js-date-picker FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-12" style="text-align:center;"><asp:Button ID="Button1" runat="server" Text="Consultar" OnClick="btnBuscar_Click" CssClass="ButtonNeutro"/></div>
    </div>
     <asp:GridView ID="grdExpedientes" runat="server" AutoGenerateColumns="False" EmptyDataText="No hay registros para mostrar."
    DataKeyNames="IdExpediente" OnRowDataBound="grdExpedientes_OnRowDataBound" CellPadding="4" EnableModelValidation="True" ForeColor="#333333" GridLines="None" Width="702px" Font-Size="Small"
           CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr" AllowPaging="True"  PageSize="10" OnPageIndexChanging="grdExpedientes_PageIndexChanging">
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                <img alt = "" style="cursor: pointer" src="../Compartidas/imagenes/plus.png" />
                <asp:Panel ID="pnlResoluciones" datasourceid="ResolucionesSqlDataSource"  runat="server" Style="display: none">
                    <asp:GridView ID="grdResoluciones" DataKeyNames="IdRes,EstadoResolucion" runat="server" AutoGenerateColumns="false" EmptyDataText="No hay registros para mostrar." CssClass = "ChildGrid"
                         EnableModelValidation="True" CellPadding="4" GridLines="None" ForeColor="#669999" Font-Size="Small" OnRowEditing="grdResoluciones_RowEditing" OnRowCommad="gvResoluciones_RowCommand" 
                        OnRowDataBound="grdResoluciones_OnRowDataBound">
                         <AlternatingRowStyle BackColor="White" ForeColor="#800000" />
                        <Columns>
                            <asp:BoundField DataField="IdRes" HeaderText="IdRes" />
                            <asp:BoundField DataField="IdResolucion" HeaderText="Resolución" />
                            <asp:BoundField  DataField="IdExpedienteFK" HeaderText="IdExpedienteFK" Visible="false"/>
                            <asp:BoundField  DataField="EstadoResolucion" HeaderText="EstadoResolucion" />
                            <asp:BoundField  DataField="FechResolucion" DataFormatString="{0:d}"  HeaderText="Fecha Resolución" />
                            <asp:BoundField  DataField="EstadoProcesalNom" HeaderText="Estado Procesal" />
                            <asp:BoundField  DataField="MontoPosibleReembolsoColones" HeaderText="Monto Remb. Colones" Visible="false"/>
                            <asp:BoundField  DataField="PosibleFechSalidaRecursos" DataFormatString="{0:d}"  HeaderText="Fecha Salida Recurs." Visible="false"/>
                            <asp:BoundField  DataField="Observacion" HeaderText="Observación" visible="false"/>
                             <asp:TemplateField HeaderText="">
                            <ItemTemplate>

                                <asp:Image Visible="false" ID="imgResoluciones" runat="server" ImageUrl="~/Compartidas/imagenes/24x24-document-edit.png" Height="20px" Width="20px"/>
                                <a href='Resoluciones.aspx?id=<%# Eval("IdExpedienteFK") %>&isAdd=false&Est=<%# Eval("EstadoResolucion") %>' target="_self">Modificar</a>
                                
                            </ItemTemplate>
                            <HeaderStyle/>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                             <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                <img alt = "" style="cursor: pointer" src="../Compartidas/imagenes/plus.png" />
                                <asp:Panel ID="pnlResoluciones" runat="server" Style="display: none">
                                 <asp:GridView ID="grdCobrosPagos" runat="server" AllowPaging="true" PageSize="2" EmptyDataText="No hay registros para mostrar.">
                                        
                                        <FooterStyle BackColor="White" ForeColor="#333333" />
                                        <RowStyle BackColor="White" ForeColor="#333333" />
                                        <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                                        <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                                    </asp:GridView> 
                                </asp:Panel>                             
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                         <FooterStyle BackColor="#5D7B9D" Font-Bold="True"  />
                        <HeaderStyle BackColor="#6699ff" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    </asp:GridView>
                </asp:Panel>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField ItemStyle-Width="150px" DataField="IdExpediente" HeaderText="Numero expediente" >
        <ItemStyle Width="150px"></ItemStyle>
        </asp:BoundField>
        <asp:BoundField ItemStyle-Width="150px" DataField="EstadoProcesalNom" HeaderText="Estado procesal" Visible="false">
        <ItemStyle Width="150px"></ItemStyle>
        </asp:BoundField>
        <asp:BoundField ItemStyle-Width="150px" DataField="EstadoExpediente" HeaderText="Estado" >
        <ItemStyle Width="150px"></ItemStyle>
        </asp:BoundField>
        <asp:BoundField ItemStyle-Width="150px" DataField="TipoExpediente" HeaderText="Tipo de Expediente" >
        <ItemStyle Width="150px"></ItemStyle>
        </asp:BoundField>
        <asp:BoundField ItemStyle-Width="150px" DataField="MotivoDemanda" HeaderText="Motivo" >
        <ItemStyle Width="150px"></ItemStyle>
        </asp:BoundField>
        <asp:BoundField ItemStyle-Width="150px" DataField="FechaDemanda" DataFormatString="{0:d}" HeaderText="Fecha de la Demanda" >
        <ItemStyle Width="150px"></ItemStyle>
        </asp:BoundField>
       
    </Columns>
        <EditRowStyle BackColor="#999999" />
</asp:GridView>
    <div><br /></div>
    <asp:Panel ID="pnlEdit" runat="server" visible="false" Width="703px">

    </asp:Panel>
</asp:Content>
