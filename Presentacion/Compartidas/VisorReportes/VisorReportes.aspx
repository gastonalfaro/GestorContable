<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VisorReportes.aspx.cs" Inherits="Presentacion.Compartidas.VisorReportes.VisorReportes" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="0">
    </asp:ScriptManager>
    <rsweb:ReportViewer ID="rVisor" runat="server" Font-Names="Verdana" Font-Size="8pt"
            Height="700px" ProcessingMode="Remote" Width="100%" oninit="rVisor_Init">
    </rsweb:ReportViewer>

    <div>    
    </div>
    </form>
</body>
</html>
