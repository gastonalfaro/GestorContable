<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmGirosEstimados.aspx.cs" Inherits="Presentacion.CalculosFinancieros.DeudaExterna.frmGirosEstimados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="encabezado" runat="server">
    <style type="text/css">
        .divStyle {
            margin-top: 1%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenido" runat="server">
    <div class="col-md-12">
        <h3>Giros Estimados </h3>
    </div>
    <div class="col-md-12">Consulta de Giros Estimados del Sistema Gestor.</div>

    <div cl class="col-md-12 divStyle">
        <div class="col-md-6 divStyle">
            <div class="col-md-4">
                <asp:Label ID="Label1" runat="server" Text="Id Préstamo:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7">
                <asp:TextBox ID="txtBusqIdPrestamo" runat="server" MaxLength="15" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>

        <div class="col-md-6 divStyle">
            <div class="col-md-4">
                <asp:Label ID="Label2" runat="server" Text="Id Tramo:" Font-Bold="true"></asp:Label>
            </div>
            <div class="col-md-7">
                <asp:TextBox ID="txtBusqIdTramo" runat="server" onkeypress="return AceptarSoloNumeros(event)" CssClass="FormatoTextBox"></asp:TextBox>
            </div>
        </div>

        <div class="col-md-6 divStyle">
            <div class="col-md-4">
                <asp:Label ID="lblFecha" runat="server" Text="Fecha: "></asp:Label>
            </div>
            <div class="col-md-7">
                <asp:TextBox ID="txtFecha" runat="server" CssClass="js-date-picker FormatoTextBox"></asp:TextBox>
            </div>
        </div>

        <div class="col-md-12 " style="margin-bottom:2%; margin-top:2%;">
            <div style="text-align: center;">
                <asp:Button ID="btnFormulariosConsultar" runat="server" Text="CONSULTAR" OnClick="btnFormulariosConsultar_Click" CssClass="ButtonNeutro" />
            </div>
        </div>

        <div class="col-md-12">
            <asp:Label ID="lblMensaje" runat="server" Visible="false" Font-Bold="true"></asp:Label>
        </div>
    </div>

    <div class="col-md-12 divStyle">
        <div style="width: 100%; height: 100%; overflow: auto">

            <asp:GridView ID="grdvFormularios" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
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

                    <asp:TemplateField HeaderText="Fecha Estimada" ItemStyle-HorizontalAlign="Center">
                        <FooterTemplate>
                            <asp:TextBox ID="txtFchEstimada" runat="server"></asp:TextBox>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblFchEstimada" runat="server" Text='<%# Eval("FchEstimada").ToString().Replace(" ","").Replace("12:00:00a.m.","") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtFchEstimada" runat="server" Width="90%" Text='<%# Eval("FchEstimada").ToString().Replace(" ","").Replace("12:00:00a.m.","") %>' MaxLength="50" />
                        </EditItemTemplate>

                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>

                    <%--               <asp:TemplateField HeaderText="Monto" ItemStyle-HorizontalAlign="Center">
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


                    <asp:BoundField DataField="Monto" DataFormatString="{0:N}" HeaderText="Monto">

                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>

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

