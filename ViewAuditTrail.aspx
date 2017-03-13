<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Audit.master" AutoEventWireup="true" CodeFile="ViewAuditTrail.aspx.cs" Inherits="ViewAuditTrail" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id ="container">
    <div id="containerReport">
        <asp:Label ID="lblNoRecord" runat="server" Font-Size="Small" ForeColor="#CC3300"></asp:Label>
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="1001px" Height="533px">
         </rsweb:ReportViewer>
        </div>
    </div>
</asp:Content>

