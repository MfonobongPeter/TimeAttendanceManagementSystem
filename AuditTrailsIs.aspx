<%@ Page Title="" Language="C#" MasterPageFile="~/Common/ISControl.master" AutoEventWireup="true" CodeFile="AuditTrailsIs.aspx.cs" Inherits="AuditTrailsIs" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="container">
    <div id="containerReport">
        <asp:Label ID="lblNoRecord" runat="server" Font-Size="Small" ForeColor="#CC3300"></asp:Label>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="1001px">
        </rsweb:ReportViewer>
    </div>
    </div>
</asp:Content>

