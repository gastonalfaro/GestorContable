<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmInteresesPagos.aspx.cs" Inherits="Presentacion.CalculosFinancieros.DeudaExterna.frmInteresesPagos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="encabezado" runat="server">
    <style type="text/css">
        .divStyle {margin-top:1%; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenido" runat="server">
    <div class="col-md-12"><h3>Intereses Pagos</h3></div>
    <div class="col-md-12">Consulta de Intereses Pagos del Sistema Gestor.</div>
    
    <div class="col-md-12">
    <div class="col-md-6 divStyle">
            <div class="col-md-4">
                <asp:Label ID="Label1" runat="server" Text="Id Préstamo:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7">
                <asp:TextBox ID="txtBusqIdPrestamo" runat="server" MaxLength="15" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>

    <div class="col-md-6 divStyle">
            <div class="col-md-4">
                <asp:Label ID="NumeroTramoLabel" runat="server" Text="Número Tramo: "></asp:Label>
            </div>
            <div class="col-md-7">
                <asp:TextBox ID="txtBusqIdTramo" runat="server" MaxLength="15" CssClass="FormatoTextBox"></asp:TextBox>
            </div>
    </div>


    <div class="col-md-6 divStyle">
            <div class="col-md-4">
                <asp:Label ID="FechaValorAcreedorLabel" runat="server" Text="Fecha Programada: "></asp:Label>
            </div>
            <div class="col-md-7">
                <asp:TextBox ID="txtFechaValorAcreedor" runat="server" CssClass="js-date-picker FormatoTextBox"></asp:TextBox>
            </div>
    </div>

    
    <div class="col-md-6 divStyle">
            <div class="col-md-4">
                <asp:Label ID="SecuenciaLabel" runat="server" Text="Secuencia: "></asp:Label>
            </div>
            <div class="col-md-7">
                <asp:TextBox ID="txtSecuencia" runat="server" CssClass="FormatoTextBox"></asp:TextBox>
            </div>
    </div>

    

    <div class="col-md-12" style="margin-bottom:2%; margin-top:2%;">
        <div style="text-align: center;">
            <asp:Button ID="btnFormulariosConsultar" runat="server" Text="CONSULTAR" OnClick="btnFormulariosConsultar_Click" CssClass="ButtonNeutro" />
        </div>
    </div>


    <div class="col-md-12">
         <asp:label ID="lblMensaje" runat="server" Visible="false" Font-Bold="true" ></asp:label>
    </div>

    </div>

    <div style="width: 100%; height: 100%; overflow: auto">                   
    <div style="margin-top:2%;">
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
                                    <asp:TextBox ID="txtInsertIdPrestamo" runat="server" Text='<%# Bind("IdPrestamo") %>' MaxLength="32" />
                                </FooterTemplate>

                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>

                           <asp:TemplateField HeaderText="Id Tramo" ItemStyle-HorizontalAlign="Center"> 
                                <ItemTemplate>
                                    <asp:Label ID="lblIdTramo" runat="server" Text='<%# Bind("IdTramo") %>'></asp:Label>
                                </ItemTemplate>                           
                                <FooterTemplate>
                                    <asp:TextBox ID="txtIdtramo" runat="server" Text='<%# Bind("IdTramo") %>' MaxLength="32" />
                                </FooterTemplate>

                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Fecha Programada" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblFchValorAcreedor" runat="server" Text='<%# (Eval("FchValorAcreedor")??"").ToString().Replace(" ","").Replace("12:00:00a.m.","") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtFchValorAcreedor" runat="server" Text='<%# (Eval("FchValorAcreedor")??"").ToString().Replace(" ","").Replace("12:00:00a.m.","") %>' MaxLength="32" />
                                </FooterTemplate>

                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>

                            
                            <asp:TemplateField HeaderText="Secuencia" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblSecuencia" runat="server" Text='<%# Bind("Secuencia") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtSecuencia" runat="server" Text='<%# Bind("Secuencia") %>' MaxLength="32" />
                                </FooterTemplate>

                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Fecha Tipo Cambio" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblFchTipoCambio" runat="server" Text='<%# (Eval("FchTipoCambio")??"").ToString().Replace(" ","").Replace("12:00:00a.m.","") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtFchTipoCambio" runat="server" Text='<%# (Eval("FchTipoCambio")??"").ToString().Replace(" ","").Replace("12:00:00a.m.","") %>' MaxLength="32" />
                                </FooterTemplate>

                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>


                         <%--   
                            <asp:TemplateField HeaderText="Monto" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblMonto" runat="server" Text='<%# Bind("Monto") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtMonto" runat="server" Text='<%# Bind("Monto") %>' MaxLength="32" />
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>--%>

                            <asp:BoundField  DataField="Monto"  DataFormatString="{0:N}" HeaderText="Monto" >

                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>

                            <asp:TemplateField HeaderText="MonedaPago" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblMonedaPago" runat="server" Text='<%# Bind("MonedaPago") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtMonedaPago" runat="server" Text='<%# Bind("MonedaPago") %>' MaxLength="32" />
                                </FooterTemplate>

                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="EstadoSigade" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblEstadoSigade" runat="server" Text='<%# Bind("EstadoSigade") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtEstadoSigade" runat="server" Text='<%# Bind("EstadoSigade") %>' MaxLength="32" />
                                </FooterTemplate>

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
     
    </div>
</asp:Content>


