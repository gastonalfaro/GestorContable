<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmComisiones.aspx.cs" Inherits="Presentacion.CalculosFinancieros.DeudaExterna.frmComisiones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="encabezado" runat="server">  
        <style type="text/css">
            .divStyle { margin-top:1%;}
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenido" runat="server">
    <div class="col-md-12"><h3>Comisiones</h3></div>
        <br />
    <div class="col-md-12" style="margin-bottom:2%;">Consulta de Comisiones del Sistema Gestor.</div>
      
    <div  class="col-md-6 divStyle">       
        <div class="col-md-4"><asp:Label ID="IdPrestamoLabel" runat="server" Text="Id Préstamo:" Font-Bold="true"></asp:Label></div>
        <div class="col-md-6"> <asp:TextBox ID="txtBusqIdPrestamo" runat="server" MaxLength="15" CssClass="FormatoTextBox"></asp:TextBox></div>
    </div>
     

    <div  class="col-md-6 divStyle">
        <div class="col-md-4"><asp:Label ID="IdTramoLabel" runat="server" Text="Id Tramo:" Font-Bold="true"></asp:Label></div>
        <div class="col-md-6"> <asp:TextBox ID="txtBusqIdTramo" runat="server" MaxLength="15" CssClass="FormatoTextBox"></asp:TextBox></div>
    </div> 

    <div  class="col-md-6 divStyle">
        <div class="col-md-4"><asp:Label ID="IdComisionLabel" runat="server" Text="Id Comisión:" Font-Bold="true"></asp:Label></div>
        <div class="col-md-6"> <asp:TextBox ID="txtBusqIdComision" runat="server" onkeypress="return AceptarSoloNumeros(event)" CssClass="FormatoTextBox"></asp:TextBox></div>
    </div> 
    <%-----------------------------------------%>
    
    
    <div  class="col-md-6 divStyle">
        <div class="col-md-4"><asp:Label ID="TipoComisionLabel" runat="server" Text="Tipo Comisión:" Font-Bold="true"></asp:Label></div>
        <div class="col-md-6"> <asp:TextBox ID="txtBusqTipoComision" runat="server"  CssClass="FormatoTextBox"></asp:TextBox></div>
    </div>
     
    <div  class="col-md-6 divStyle">
        <div class="col-md-4"><asp:Label ID="FchEfectivoAPartirLabel" runat="server" Text="Fecha Desde:" Font-Bold="true"></asp:Label></div>
        <div class="col-md-6"> <asp:TextBox ID="txtBusqFchEfectivoAPartir" runat="server" MaxLength="15" CssClass="js-date-picker FormatoTextBox"></asp:TextBox></div>
    </div> 

    <div  class="col-md-6 divStyle">
        <div class="col-md-4"><asp:Label ID="FchHastaLabel" runat="server" Text="Fecha Hasta:" Font-Bold="true"></asp:Label></div>
        <div class="col-md-6"> <asp:TextBox ID="txtBusqFchHasta" runat="server" CssClass="js-date-picker FormatoTextBox"></asp:TextBox></div>
    </div> 
    <%-----------------------------------------%>
          
    <div  class="col-md-6 divStyle">
        <div class="col-md-4"><asp:Label ID="MonedaPagoLabel" runat="server" Text="Moneda Pago:" Font-Bold="true"></asp:Label></div>
        <div class="col-md-6"> <asp:TextBox ID="txtBusqMonedaPago" runat="server" MaxLength="3" CssClass="FormatoTextBox"></asp:TextBox></div>
    </div> 
    
    <div  class="col-md-6 divStyle">
            <div class="col-md-4"><asp:Label ID="PorcentajeLabel" runat="server" Text="Porcentaje:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-6"> <asp:TextBox ID="txtBusqPorcentaje" runat="server" MaxLength="15" CssClass="FormatoTextBox"></asp:TextBox></div>
    </div> 
    
    <div  class="col-md-6 divStyle">
        <div class="col-md-4"><asp:Label ID="PeriodoLabel" runat="server" Text="Período:" Font-Bold="true"></asp:Label></div>
        <div class="col-md-6"> <asp:TextBox ID="txtBusqPeriodo" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
    </div> 
    <%-----------------------------------------%>
    
    <div  class="col-md-6 divStyle">
        <div class="col-md-4"><asp:Label ID="AnnoLabel" runat="server" Text="Año:" Font-Bold="true"></asp:Label></div>
        <div class="col-md-6"> <asp:TextBox ID="txtBusqAnno" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
    </div> 


    <div  class="col-md-6 divStyle">
        <div class="col-md-4"><asp:Label ID="MesLabel" runat="server" Text="Mes:" Font-Bold="true"></asp:Label></div>
        <div class="col-md-6"> <asp:TextBox ID="txtBusqMes" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
    </div> 

        
    <div  class="col-md-6 divStyle">
            <div class="col-md-4"><asp:Label ID="TipoPagoLabel" runat="server" Text="Tipo Pago:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-6"> <asp:TextBox ID="txtBusqTipoPago" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
    </div> 
    
    <%-----------------------------------------%>
    <div class="col-md-12" style="margin-top:3%; margin-bottom:2%;">
        <div style="text-align:center;">
               <asp:Button ID="btnFormulariosConsultar" runat="server" Text="CONSULTAR" OnClick="btnFormulariosConsultar_Click" CssClass="ButtonNeutro"/>
        </div>
    </div>

    <div class="col-md-12">
         <asp:label ID="lblMensaje" runat="server" Visible="false" Font-Bold="true" ></asp:label>
    </div>
    <div style="width: 100%; height: 100%; overflow: auto">                   
                    
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
                            
                            <asp:TemplateField HeaderText="Id Comision">
                                <FooterTemplate>
                                    <asp:TextBox ID="txtIdComision" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblIdComision" runat="server" Text='<%# Bind("IdComision") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtIdComision" runat="server" Width="10%" Text='<%# Bind("IdComision") %>' MaxLength="32" />
                                </EditItemTemplate>
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

                            <asp:TemplateField HeaderText="Tipo Comision" ItemStyle-HorizontalAlign="Center">
                                <FooterTemplate>
                                    <asp:TextBox ID="txtTipoComision" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblTipoComision" runat="server" Text='<%# Bind("TipoComision") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtTipoComision" runat="server" Width="90%" Text='<%# Bind("TipoComision") %>' MaxLength="50" />
                                </EditItemTemplate>

                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>                


                              <asp:TemplateField HeaderText="Fecha Efectivo a Partir de" ItemStyle-HorizontalAlign="Center">
                                <FooterTemplate>
                                    <asp:TextBox ID="txtFchEfectivoAPartir" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblFchEfectivoAPartir" runat="server" Text='<%# Eval("FchEfectivoAPartir").ToString().Replace(" ","").Replace("12:00:00a.m.","") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtBusqFchEfectivoAPartir" runat="server" Width="90%" Text='<%# Eval("FchEfectivoAPartir").ToString().Replace(" ","").Replace("12:00:00a.m.","") %>' MaxLength="100" />
                                </EditItemTemplate>

                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>

                            

                           <asp:TemplateField HeaderText="Fecha Hasta" ItemStyle-HorizontalAlign="Center">
                                <FooterTemplate>
                                    <asp:TextBox ID="txtFchHasta" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblFchHasta" runat="server" Text='<%# Eval("FchHasta").ToString().Replace(" ","").Replace("12:00:00a.m.","") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtBusqFchHasta" runat="server" Width="90%" Text='<%# Eval("FchHasta").ToString().Replace(" ","").Replace("12:00:00a.m.","") %>' MaxLength="100" />
                                </EditItemTemplate>

                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="MonedaPago" ItemStyle-HorizontalAlign="Center">
                                <FooterTemplate>
                                    <asp:TextBox ID="txtMonedaPago" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblMonedaPago" runat="server" Text='<%# Bind("MonedaPago") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtMonedaPago" runat="server" Width="90%" Text='<%# Bind("MonedaPago") %>' MaxLength="10" />
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Porcentaje" ItemStyle-HorizontalAlign="Center">
                                <FooterTemplate>
                                    <asp:TextBox ID="txtPorcentaje" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblPorcentaje" runat="server" Text='<%# Bind("Porcentaje") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtPorcentaje" runat="server" Width="90%" Text='<%# Bind("Porcentaje") %>' MaxLength="50" />
                                </EditItemTemplate>

                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>

                           <%-- <asp:TemplateField HeaderText="Monto Pago" ItemStyle-HorizontalAlign="Center">
                                <FooterTemplate>
                                    <asp:TextBox ID="txtMontoPago" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblMontoPago" runat="server" Text='<%# Bind("MontoPago") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtMontoPago" runat="server" Width="90%" Text='<%# Bind("MontoPago") %>' MaxLength="100" />
                                </EditItemTemplate>

                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>--%>
                            

                            <asp:BoundField  DataField="MontoPago"  DataFormatString="{0:N}" HeaderText="Monto Pago" >
                            
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            
                            <asp:TemplateField HeaderText="Metodo Pago" ItemStyle-HorizontalAlign="Center">
                                <FooterTemplate>
                                    <asp:TextBox ID="txtMetodoPago" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblMetodoPago" runat="server" Text='<%# Bind("MetodoPago") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtMetodoPago" runat="server" Width="90%" Text='<%# Bind("MetodoPago") %>' MaxLength="50" />
                                </EditItemTemplate>

                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Fecha Primer Pago" ItemStyle-HorizontalAlign="Center">
                                <FooterTemplate>
                                    <asp:TextBox ID="txtFchPrimerPago" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblFchPrimerPago" runat="server" Text='<%# Eval("FchPrimerPago").ToString().Replace(" ","").Replace("12:00:00a.m.","") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtFchPrimerPago" runat="server" Width="90%" Text='<%# Eval("FchPrimerPago").ToString().Replace(" ","").Replace("12:00:00a.m.","") %>' MaxLength="50" />
                                </EditItemTemplate>

                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>


                            
                            <asp:TemplateField HeaderText="Fecha Último Pago" ItemStyle-HorizontalAlign="Center">
                                <FooterTemplate>
                                    <asp:TextBox ID="txtFchUltimoPago" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblFchUltimoPago" runat="server" Text='<%# Eval("FchUltimoPago").ToString().Replace(" ","").Replace("12:00:00a.m.","") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtFchUltimoPago" runat="server" Width="90%" Text='<%# Eval("FchUltimoPago").ToString().Replace(" ","").Replace("12:00:00a.m.","") %>' MaxLength="50" />
                                </EditItemTemplate>

                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Período" ItemStyle-HorizontalAlign="Center">
                                <FooterTemplate>
                                    <asp:TextBox ID="txtPeriodo" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblPeriodo" runat="server" Text='<%# Bind("Periodo") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtPeriodo" runat="server" Width="90%" Text='<%# Bind("Periodo") %>' MaxLength="50" />
                                </EditItemTemplate>

                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Año" ItemStyle-HorizontalAlign="Center">
                                <FooterTemplate>
                                    <asp:TextBox ID="txtAnno" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblAnno" runat="server" Text='<%# Bind("Anno") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtAnno" runat="server" Width="90%" Text='<%# Bind("Anno") %>' MaxLength="50" />
                                </EditItemTemplate>

                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Mes" ItemStyle-HorizontalAlign="Center">
                                <FooterTemplate>
                                    <asp:TextBox ID="txtMes" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblMes" runat="server" Text='<%# Bind("Mes") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtMes" runat="server" Width="90%" Text='<%# Bind("Mes") %>' MaxLength="50" />
                                </EditItemTemplate>

                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Tipo Pago" ItemStyle-HorizontalAlign="Center">
                                <FooterTemplate>
                                    <asp:TextBox ID="txtTipoPago" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblTipoPago" runat="server" Text='<%# Bind("TipoPago") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtTipoPago" runat="server" Width="90%" Text='<%# Bind("TipoPago") %>' MaxLength="50" />
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

