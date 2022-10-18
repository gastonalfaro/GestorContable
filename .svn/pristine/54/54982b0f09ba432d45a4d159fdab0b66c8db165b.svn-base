<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="ConsultarExpedientes.aspx.cs" Inherits="Presentacion.Contingentes.ConsultarExpedientes" %>
<%@ Register Assembly="eWorld.UI.Compatibility, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="encabezado" runat="server"></asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenido" runat="server">
    <div class="FormatoBotones"></div> 
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
    <div style="width:100%">
              
                    <asp:CheckBox ID="ckbNuevoAno" visible="false" runat="server" Text="Cambio de año" />
              
                    <asp:GridView ID="grdExpedientes" runat="server" AutoGenerateColumns="False" OnClientClick="return confirm('¿Seguro que desea Anular el Expediente Seleccionado?');"  DataKeyNames="IdExpediente" CellPadding="4" EmptyDataText="No hay registros para mostrar." ShowFooter="True" AllowPaging="True" AllowSorting="True" CaptionAlign="Top" OnPageIndexChanging="grdExp_PageIndexChanging" OnRowCommand="grdExpedientes_RowCommand" style="margin-right: 0px; text-align: center; font-size: small;" ForeColor="#333333" GridLines="None"
                          CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                       
                    <Columns>
                        <asp:TemplateField HeaderText="Num. Expediente"> 
                             
                            <ItemTemplate>                               
                                 <asp:Label ID="lblNumExpediente" runat="server" Visible="True" Text='<%# Bind("IdExpediente") %>' CssClass="Normal"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Num. Exp. Administrativo" Visible="false">
                           
                            <ItemTemplate>
                                <asp:Label ID="lblNumOrigenExpediente" runat="server" Visible="false" Text='<%# Bind("ExpedienteOrigen") %>' CssClass="Normal"></asp:Label>
                            </ItemTemplate>
                           
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tipo de Proceso">
                            
                             <ItemTemplate>
                                 <asp:Label ID="lblTipoProceso" runat="server" Visible="True" Text='<%# Bind("TipoProcesoExpediente") %>' CssClass="Normal"></asp:Label>
                            </ItemTemplate>
                             
                             <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cedula Actor" Visible="false">
                            
                            <ItemTemplate><asp:Label ID="lblCedActor" runat="server" Visible="True" Text='<%# Bind("CedActor") %>' CssClass="Normal"></asp:Label></ItemTemplate>
                            
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Actor(es)">
                            <ItemTemplate><asp:Label ID="lblNomActor" runat="server" Visible="True" Text='<%# Bind("NomActor") %>' CssClass="Normal"></asp:Label></ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left"/>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Demandado(s)">
                            <ItemTemplate><asp:Label ID="lblNomDemandado" runat="server" Visible="True" Text='<%# Bind("NomDemandado") %>' CssClass="Normal"></asp:Label></ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Estado Procesal" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblEstadoProcesal" runat="server" CssClass="Normal" Text='<%# Bind("EstadoProcesal") %>' Visible="True"></asp:Label>
                            </ItemTemplate>
                            
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Motivo Demanda">
                            <ItemTemplate><asp:Label ID="lblMotDemanda" runat="server" Visible="True" Text='<%# Bind("MotivoDemanda") %>' CssClass="Normal"></asp:Label></ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center"/>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField> 
                       <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:Image ID="mgEditar" runat="server" ImageUrl="~/Compartidas/imagenes/24x24-document-edit.png" Height="20px" Width="20px"/>
                                <a href='NuevoExpediente.aspx?id=<%# Eval("IdExpediente") %>&isAdd=false' target="_self">Modificar</a>
                                </ItemTemplate>
                            <HeaderStyle/>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:LinkButton ID="lkBotonAnular" runat="server" CommandArgument='<%#Eval("IdExpediente") %>' CommandName="Anular" OnCommand="lkBotonAnular_Command" OnClientClick="return confirm('¿Usted va Anular un expediente?');" Text="Anul">Anula</asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle/>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:Image ID="imgPretension" runat="server" ImageUrl="~/Compartidas/imagenes/24x24-search.png" Height="20px" Width="20px"/>
                                <a href='Pretenciones.aspx?id=<%# Eval("IdExpediente") %>&isAdd=false' target="_self">Pretensión</a>
                                </ItemTemplate>
                            <HeaderStyle/>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="" Visible="false">
                            <ItemTemplate>
                                <asp:Image ID="imgLiquidacion" runat="server" ImageUrl="~/Compartidas/imagenes/1446084246_02.png" Height="20px" Width="20px"/>
                                <a href='Liquidacion.aspx?id=<%# Eval("IdExpediente") %>&isAdd=false' target="_self">Liquidación</a>
                                </ItemTemplate>
                            <HeaderStyle/>
                            <ItemStyle HorizontalAlign="Center" />

                        </asp:TemplateField>
                    </Columns>
                        <EditRowStyle BackColor="#2461BF" />
                </asp:GridView>
                          
                   &nbsp; 
                <asp:Panel ID="GridPanel" runat="server" EnableViewState="true" ViewStateMode="Enabled" style="font-size: 9pt;" Visible="false">
                <div style="height: 29px; text-align: left">
                    <br /><asp:Image ID="Image4" runat="server" CssClass="auto-style53" Height="20px" ImageUrl="~/Compartidas/imagenes/24x24-document-add.png" Width="23px" />
                    <a href="NuevoExpediente.aspx?isAdd=true" target="_self"><span class="auto-style53">Nuevo Expediente</span></a><h5>
                        &nbsp;</h5>
                    </div>
               </asp:Panel>
                
                <br /><br />
                </div> 
                </asp:Content>
<asp:Content ID="Content4" runat="server" contentplaceholderid="ContenidoJS">
    <style type="text/css">

        /*Menu desplegable*/
        ul.sidenav {
  /* el rectángulo contenedor */
  list-style: none;
  margin: 0 auto;
  padding: 0;
  width: 250px;
  /* propiedades optativas */
  background-color: #CD853F;
  border: 1px solid #300;
  outline: 1px solid #FFF;
}
ul.sidenav li a {
  /* el enlace de cada item */
  display: block;
  text-decoration: none;
  /* propiedades optativas */
  background: transparent url(URL_imagen_icono) no-repeat 5px 7px;
  border-bottom: 1px solid #AD651F;
  border-top: 1px solid #300;
  color: #FFF;
  font-size: 18px;
  padding: 15px 10px 10px 45px;
  width: 195px;
}
ul.sidenav li a:hover {
  /* propiedades optativas */
  background: #CD853F url(URL_imagen_icono) no-repeat 5px 7px;
  border-top:1px solid #300;
}
ul.sidenav li span {
  /* el contenido permanece oculto por defecto */
  display:  none;
}
ul.sidenav li a:hover span {
  /* el contenido se muestra al pasar el cursor encima */
  display: block;
  /* propiedades optativas */
  font-size: 12px;
  padding: 10px 0;
  margin: 0 0 0 -30px;
}
       
       
        .titulotabla {
            text-align: left;
            font-weight: 700;
            color: #3366CC;
            font-size: medium;
        }
       
        .auto-style53 {
            text-align: left;
        }
       
        .auto-style64 {
            height: 36px;
        }
        .auto-style65 {
            height: 36px;
            width: 407px;
        }
        .auto-style66 {
            width: 407px;
        }
       
        </style>
</asp:Content>
