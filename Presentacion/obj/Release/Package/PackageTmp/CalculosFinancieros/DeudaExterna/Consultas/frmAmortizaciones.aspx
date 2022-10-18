<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmAmortizaciones.aspx.cs" Inherits="Presentacion.CalculosFinancieros.DeudaExterna.frmAmortizaciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="encabezado" runat="server">
        <style type="text/css">
            .divStyle { margin-top:1%;}
        </style>  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenido" runat="server">
    <div class="col-md-12"><h3>Amortizaciones</h3></div>
    <div class="col-md-12">Consulta de Amortizaciones del Sistema Gestor.</div>
    
    <div  class="col-md-6 divStyle">
        <div class="row">
            <div class="col-md-3"><asp:Label ID="Label1" runat="server" Text="Id Préstamo:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7"> <asp:TextBox ID="txtBusqIdPrestamo" runat="server" MaxLength="15" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
    </div>
     <div  class="col-md-6 divStyle">
        <div class="row">
            <div class="col-md-3"><asp:Label ID="Label2" runat="server" Text="Id Tramo:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7"><asp:TextBox ID="txtBusqIdTramo" runat="server" MaxLength="4" onkeypress="return AceptarSoloNumeros(event)" CssClass="FormatoTextBox" ></asp:TextBox></div>
        </div>
    </div>
    <div class="col-md-6 divStyle">
        <div class="row">
            <div class="col-md-3">
                <asp:Label ID="FechaValorAcreedorLabel" runat="server" Text="Fecha Valor Acreedor: "></asp:Label></div>
            <div class="col-md-7">
                <asp:TextBox ID="txtFechaValorAcreedor" runat="server" CssClass="js-date-picker FormatoTextBox"></asp:TextBox></div>
        </div>
    </div>
    <div class="col-md-6 divStyle">
        <div class="row">
            <div class="col-md-3">
                <asp:Label ID="FechaRecepcionLabel" runat="server" Text="Fecha Programada: "></asp:Label></div>
            <div class="col-md-7">
                <asp:TextBox ID="txtBusqFechaRecepcion" runat="server" CssClass="js-date-picker FormatoTextBox"></asp:TextBox></div>
        </div>
    </div>
       <div class="col-md-6 divStyle">
           <div class="row">
               <div class="col-md-3">
                   <asp:Label ID="lblFechaTipoCambio" runat="server" Text="Fecha Tipo Cambio: "></asp:Label>
               </div>
               <div class="col-md-7">
                   <asp:TextBox ID="txtFechaTipoCambio" runat="server" CssClass="js-date-picker FormatoTextBox"></asp:TextBox>
               </div>
           </div>
    </div>
    <div class="col-md-12 divStyle">
        <div style="text-align:center;">
               <asp:Button ID="btnFormulariosConsultar" runat="server" Text="CONSULTAR" OnClick="btnFormulariosConsultar_Click" CssClass="ButtonNeutro"/>
        </div>
    </div>
  


     <div class="col-md-12 divStyle">
         <asp:label ID="lblMensaje" runat="server" Visible="false" Font-Bold="true" ></asp:label>
    </div>
    <div style="width: 100%; height: 100%; overflow: auto">                   
                    
          <div class="col-md-12"><h3>Resultados de la búsqueda</h3></div>
        <asp:GridView ID="grdvFormularios" runat="server" AutoGenerateColumns="False"  ShowHeaderWhenEmpty="True"
            Width="100%" CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
             PageSize="15" AllowPaging="True" OnPageIndexChanging="grdvFormularios_PageIndexChanging">
<AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                        <Columns>

                            <asp:TemplateField HeaderText="Id Préstamo" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblIdPrestamo" runat="server" Text='<%# Bind("IdPrestamo") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtInsertIdPrestamo" runat="server" Text='<%# Bind("IdPrestamo") %>' MaxLength="15" />
                                </FooterTemplate>

                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField> 

                            <asp:TemplateField HeaderText="Id Tramo">
                                <FooterTemplate>
                                    <asp:TextBox ID="txtIdTramo" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblIdTramo" runat="server" Text='<%# Bind("IdTramo") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEditIdTramo" runat="server" Width="10%" Text='<%# Bind("IdTramo") %>' MaxLength="32" />
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Fecha Programada" ItemStyle-HorizontalAlign="Center">
                                <FooterTemplate>
                                    <asp:TextBox ID="txtFchRecepcion" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblFchRecepcion" runat="server" Text='<%# Eval("FchRecepcion").ToString().Replace(" ","").Replace("12:00:00a.m.","") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEditFchRecepcion" runat="server" Width="90%" Text='<%# Eval("FchRecepcion").ToString().Replace(" ","").Replace("12:00:00a.m.","") %>' MaxLength="50" />
                                </EditItemTemplate>

                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>                


                             <%-- <asp:TemplateField HeaderText="Monto" ItemStyle-HorizontalAlign="Center">
                                <FooterTemplate>
                                    <asp:TextBox ID="txtMonto" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblMonto" runat="server" Text='<%# Bind("Monto") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtMonto" runat="server" Width="90%" Text='<%# Bind("Monto") %>' MaxLength="100" />
                                </EditItemTemplate>

                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>--%>


                            <asp:BoundField  DataField="Monto"  DataFormatString="{0:N}" HeaderText="Monto" >
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>


                            <asp:TemplateField HeaderText="Fecha Valor Acreedor" ItemStyle-HorizontalAlign="Center">
                                <FooterTemplate>
                                    <asp:TextBox ID="txtFchValorAcreedor" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblFchValorAcreedor" runat="server" Text='<%# Eval("FchValorAcreedor").ToString().Replace(" ","").Replace("12:00:00a.m.","") %>' ></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtFchValorAcreedor" runat="server" Width="90%" Text='<%# Eval("FchValorAcreedor").ToString().Replace(" ","").Replace("12:00:00a.m.","") %>' MaxLength="10" />
                                </EditItemTemplate>

                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>


                            

                            
                             <asp:TemplateField HeaderText="Fecha Tipo Cambio " ItemStyle-HorizontalAlign="Center">
                                <FooterTemplate>
                                    <asp:TextBox ID="txtFchTipoCambio" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblFchTipoCambio" runat="server" Text='<%# Eval("FchTipoCambio").ToString().Replace(" ","").Replace("12:00:00a.m.","") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtFchTipoCambio" runat="server" Width="90%" Text='<%# Eval("FchTipoCambio").ToString().Replace(" ","").Replace("12:00:00a.m.","") %>' MaxLength="100" />
                                </EditItemTemplate>

                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>


                                <asp:TemplateField HeaderText="Id Moneda " ItemStyle-HorizontalAlign="Center">
                                <FooterTemplate>
                                    <asp:TextBox ID="txtIdMoneda" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblIdMoneda" runat="server" Text='<%# Bind("IdMoneda") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtIdMoneda" runat="server" Width="90%" Text='<%# Bind("IdMoneda") %>' MaxLength="100" />
                                </EditItemTemplate>

                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Secuencia " ItemStyle-HorizontalAlign="Center">
                                <FooterTemplate>
                                    <asp:TextBox ID="txtSecuencia" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblSecuencia" runat="server" Text='<%# Bind("Secuencia") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEditSecuencia" runat="server" Width="90%" Text='<%# Bind("Secuencia") %>' MaxLength="100" />
                                </EditItemTemplate>

                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>



                            
                            <asp:TemplateField HeaderText="Modal " ItemStyle-HorizontalAlign="Center">
                                <FooterTemplate>
                                    <asp:TextBox ID="txtModal" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblModal" runat="server" Text='<%# Bind("Modal") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtModal" runat="server" Width="90%" Text='<%# Bind("Modal") %>' MaxLength="100" />
                                </EditItemTemplate>

                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>


                           <asp:TemplateField HeaderText="Estado Sigade " ItemStyle-HorizontalAlign="Center">
                                <FooterTemplate>
                                    <asp:TextBox ID="txtEstadoSigade" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblEstadoSigade" runat="server" Text='<%# Bind("EstadoSigade") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEstadoSigade" runat="server" Width="90%" Text='<%# Bind("EstadoSigade") %>' MaxLength="100" />
                                </EditItemTemplate>

                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>

                            
                           <asp:TemplateField HeaderText="Estado" ItemStyle-HorizontalAlign="Center">
                                <FooterTemplate>
                                    <asp:TextBox ID="txtEstado" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblEstado" runat="server" Text='<%# Bind("Estado") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEditEstado" runat="server" Width="90%" Text='<%# Bind("Estado") %>' MaxLength="100" />
                                </EditItemTemplate>

                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>

                  

                        </Columns>  

                        <EditRowStyle BackColor="#999999" />

<PagerStyle CssClass="pgr"></PagerStyle>
                    </asp:GridView>                     
               
    </div>
</asp:Content>

