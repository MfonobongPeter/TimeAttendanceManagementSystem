<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Test.aspx.cs" Inherits="Test" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></cc1:ToolkitScriptManager>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
         <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/calendar.gif" />
                <asp:Button ID="Button1" runat="server" Text="Button" />
        <cc1:CalendarExtender ID="TextBox1_CalendarExtender" runat="server" Enabled="True" TargetControlID="TextBox1" PopupButtonID="Image1" Format="dd/MM/yyyy">
        </cc1:CalendarExtender>
       
    </div>
    </form>
</body>
</html>
