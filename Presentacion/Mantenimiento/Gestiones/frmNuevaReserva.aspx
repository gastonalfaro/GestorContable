<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmNuevaReserva.aspx.cs" Inherits="Presentacion.Mantenimiento.Gestiones.frmNuevaReserva" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoJS" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Contenido" runat="server">
    <div style="width: 100%; height: 100%; overflow: auto">
         
        <asp:Table ID="tblDirecciones" runat="server" Width="100%">

            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Left">
                    <h3>Revelación</h3>
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="Right" >
                    <%--<asp:Button ID="btnNuevasDirecciones" runat="server" Text="NUEVO" OnClick="btnNuevasDirecciones_Click"  Visible="false"/>
                    <asp:Button ID="btnGuardarDirecciones" runat="server" Text="GUARDAR" OnClick="btnGuardarDirecciones_Click" />
                    --%>
                    <asp:Button ID="btnVolverRevelaciones" runat="server" Text="VOLVER" OnClick="btnVolverRevelaciones_Click" CssClass="ButtonNeutro" />
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>      
                <asp:TableCell >Consulta de Revelación del Sistema Gestor.</asp:TableCell>
            </asp:TableRow>

            <asp:TableRow><asp:TableCell><br /></asp:TableCell></asp:TableRow>

            <asp:TableRow>
                <asp:TableCell ColumnSpan="2">
                    <asp:label ID="lblMensaje" runat="server" Visible="false" Font-Bold="true" ></asp:label>
                </asp:TableCell>
                <asp:TableHeaderCell><br /></asp:TableHeaderCell>
            </asp:TableRow>

        </asp:Table>

        <asp:Table ID="Table2" runat="server" Width="100%" >
            <asp:TableRow>
                <asp:TableCell Width="10%">
                    <asp:Table ID="Table1" runat="server" Width="100%">
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="lblIdReserva" runat="server" Text="Código Reserva:" Font-Bold="true"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:TextBox ID="txtIdReserva" runat="server" Enabled="false" CssClass="FormatoTextBox"></asp:TextBox>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:TableCell>
            </asp:TableRow>


            <asp:TableRow>
                <asp:TableCell ColumnSpan="3">
                    <asp:GridView ID="grdvParametros" runat="server" AutoGenerateColumns="False"  ShowHeaderWhenEmpty="True"
            Width="100%" OnSelectedIndexChanged="grdvParametros_SelectedIndexChanged" OnRowEditing="grdvParametros_RowEditing"
            CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
            OnRowUpdating="grdvParametros_RowUpdating" OnRowUpdated="grdvParametros_RowUpdated" OnPageIndexChanging="grdvParametros_PageIndexChanging"
            OnRowCancelingEdit="grdvParametros_RowCancelingEdit" OnSorting="grdvParametros_Sorting" AllowSorting="false"
                         AllowPaging="true" PageSize="20">
                        <Columns>

                            <asp:TemplateField HeaderText="Código" SortExpression="IdParametro"> 
                                <ItemTemplate>
                                    <asp:Label ID="lblIdParametro" runat="server" Text='<%# Bind("IdParametro") %>'></asp:Label>
                                </ItemTemplate>                           
                                <FooterTemplate>
				                    <asp:TextBox ID="txtInsertIdParametro" runat="server"  Text='<%# Bind("IdParametro") %>' MaxLength="15" />
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Descripción" SortExpression="DesParametro">
                                <FooterTemplate>
                                    <asp:TextBox ID="txtNomNuevoDesParametro" runat="server" Text='<%# Bind("DesParametro") %>'></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblDesParametro" runat="server" Text='<%# Bind("DesParametro") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditDesParametro" runat="server" Width="90%" Text='<%# Bind("DesParametro") %>' MaxLength="100"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="" ShowHeader="False" HeaderStyle-HorizontalAlign="Left">
                                <EditItemTemplate> 
                                    <asp:LinkButton ID="lnkActualizarParametro" runat="server" CausesValidation="True" CommandName="Update" Text="Actualizar"></asp:LinkButton> 
                                    <asp:LinkButton ID="lnkCancelarParametro" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancelar"></asp:LinkButton> 
                                </EditItemTemplate> 
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtEditarParametro" runat="server" CausesValidation="False" Text="Editar" CommandName="Edit" OnClick="lbtEditarParametro_Click"></asp:LinkButton>
                                    <asp:LinkButton ID="lbtDuplicarParametro" runat="server" CausesValidation="False" Text="Duplicar" CommandName="Edit" OnClick="lbtDuplicarParametro_Click"></asp:LinkButton>
                                </ItemTemplate>
                                <InsertItemTemplate>

                                </InsertItemTemplate>
                            </asp:TemplateField>

                        </Columns>  

                        <EditRowStyle BackColor="#999999" />

                    </asp:GridView>
                </asp:TableCell>
            </asp:TableRow>

            </asp:Table>

            <asp:Table ID="Table13" runat="server" Width="100%" >
            <asp:TableRow>
                
                <asp:TableCell Width="10%">
                    <asp:Table ID="tb_" runat="server" Width="100%" >
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="lblDetalle" runat="server" Text="Descripción:" Font-Bold="true" ></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:TextBox ID="txtDetalle" runat="server" Enabled="false" Width="100%" CssClass="FormatoTextBox"></asp:TextBox>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell Width="10%">
                    <asp:Table ID="Table3" runat="server" Width="100%">
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="lblPosicion" runat="server" Text="Posición:" Font-Bold="true"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:TextBox ID="txtPosicion" runat="server" Enabled="false" CssClass="FormatoTextBox"></asp:TextBox>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:TableCell>

                <asp:TableCell Width="10%">
                    <asp:Table ID="Table4" runat="server" Width="100%" >
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="lblIdPosPre" runat="server" Text="Id Posición Presupuestaria:" Font-Bold="true" ></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:TextBox ID="txtIdPosPre" runat="server" Enabled="false" Width="100%" CssClass="FormatoTextBox"></asp:TextBox>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell Width="10%">
                    <asp:Table ID="Table5" runat="server" Width="100%">
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="lblIdCentroGestor" runat="server" Text="Centro Gestor:" Font-Bold="true"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:TextBox ID="txtIdCentroGestor" runat="server" Enabled="false" CssClass="FormatoTextBox"></asp:TextBox>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:TableCell>

                <asp:TableCell Width="10%">
                    <asp:Table ID="Table6" runat="server" Width="100%" >
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="lblIdFondo" runat="server" Text="Fondo:" Font-Bold="true" ></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:TextBox ID="txtIdFondo" runat="server" Enabled="false" Width="100%" CssClass="FormatoTextBox"></asp:TextBox>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell Width="10%">
                    <asp:Table ID="Table7" runat="server" Width="100%">
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="lblSegmento" runat="server" Text="Segmento:" Font-Bold="true"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:TextBox ID="txtSegmento" runat="server" Enabled="false" CssClass="FormatoTextBox"></asp:TextBox>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:TableCell>

                <asp:TableCell Width="10%">
                    <asp:Table ID="Table8" runat="server" Width="100%" >
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="lblIdPrograma" runat="server" Text="Programa:" Font-Bold="true" ></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:TextBox ID="txtIdPrograma" runat="server" Enabled="false" Width="100%" CssClass="FormatoTextBox"></asp:TextBox>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell Width="10%">
                    <asp:Table ID="Table10" runat="server" Width="100%" >
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="lblIdCuentaContable" runat="server" Text="Cuenta Contable:" Font-Bold="true" ></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:TextBox ID="txtIdCuentaContable" runat="server" Enabled="false" CssClass="FormatoTextBox"></asp:TextBox>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:TableCell>

                <asp:TableCell Width="10%">
                    <asp:Table ID="Table9" runat="server" Width="100%">
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="lblIdCentroCosto" runat="server" Text="Centro Costo:" Font-Bold="true"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:TextBox ID="txtIdCentroCosto" runat="server" Enabled="false" Width="100%" CssClass="FormatoTextBox"></asp:TextBox>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell Width="10%">
                    <asp:Table ID="Table11" runat="server" Width="100%" >
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="lblElementoPEP" runat="server" Text="Elemento PEP:" Font-Bold="true" ></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:TextBox ID="txtElementoPEP" runat="server" Enabled="false" CssClass="FormatoTextBox" ></asp:TextBox>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:TableCell>

                <asp:TableCell Width="10%">
                    <asp:Table ID="Table12" runat="server" Width="100%">
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="lblBloqueado" runat="server" Text="Bloqueado:" Font-Bold="true"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:TextBox ID="txtBloqueado" runat="server" Enabled="false" Width="100%" CssClass="FormatoTextBox"></asp:TextBox>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell Width="20%"></asp:TableCell>
                <asp:TableCell Width="20%" ColumnSpan="1"  HorizontalAlign="Right">
                    <asp:Button ID="btnCrearReserva" runat="server" Text="CREAR" OnClick="btnCrearReserva_Click" CssClass="ButtonNeutro"/>
                </asp:TableCell>
            </asp:TableRow>

        </asp:Table>


    </div>
</asp:Content>
