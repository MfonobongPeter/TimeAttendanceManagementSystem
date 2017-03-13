<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EventLogPage.aspx.cs" Inherits="EventLogPage" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title></title>
     <meta name="viewport" content="width=device-width" />
    <meta http-equiv="X-UA-Compatible" content="IE-EmulateIE7" />
    <link href="Content/RCOStyleSheet.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div id ="wrapperCustomError">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div id = "wrapperLogin">
            <asp:Label ID="lblNoRecord" runat="server" Text="Label"></asp:Label>  <br />
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="1016px"></rsweb:ReportViewer>
        </div>
    </div>
    </form>
</body>
</html>
