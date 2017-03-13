<%@ Page Title="" Language="C#" MasterPageFile="~/Common/ISControl.master" AutoEventWireup="true" CodeFile="ViewRCOs.aspx.cs" Inherits="ViewRCOs" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id = "container">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

      <div class="RcoSearchOptionsDiv">
          <asp:Label ID="Label1" runat="server" Text="Search By:" CssClass="RcoSearchOptions"></asp:Label>
          <asp:RadioButton ID="RadioButton1" runat="server" Text="All" CssClass="RcoSearchOptions" GroupName="rco" AutoPostBack="True" OnCheckedChanged="RadioButton1_CheckedChanged" />
          <asp:RadioButton ID="rdbUsername" runat="server" Text="Username" CssClass="RcoSearchOptions" GroupName="rco" AutoPostBack="True" OnCheckedChanged="rdbUsername_CheckedChanged"  />
          <asp:RadioButton ID="rdbUsernameAndDate" runat="server" Text="Username & Date" CssClass="RcoSearchOptions" GroupName="rco" AutoPostBack="True" OnCheckedChanged="rdbUsernameAndDate_CheckedChanged" />
          <asp:RadioButton ID="rdbDate" runat="server" Text="Date" CssClass="RcoSearchOptions" GroupName="rco" AutoPostBack="True" OnCheckedChanged="rdbDate_CheckedChanged"/>
      </div>
        <br />
            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                <ProgressTemplate>
                    <div style="text-align:center;margin:0px auto 0px auto">
                        <asp:Image ID="Image1" runat="server"  ImageUrl ="~/Images/Animated.gif"/>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        <div class="RcoSearchControlsDiv">
            <asp:Label ID="lblUsername" runat="server" Text="Username:" CssClass="RcoSearchLabels"></asp:Label><asp:TextBox ID="txtUsername" runat="server" CssClass="textBoxReport" Enabled="False"></asp:TextBox><span> </span><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtUsername" Font-Size="X-Small" SetFocusOnError="True" ValidationGroup="sc" style="color: #FF3300"></asp:RequiredFieldValidator>
            <asp:Label ID="lblDate" runat="server" Text="Date:" CssClass="RcoSearchLabels"></asp:Label><asp:TextBox ID="txtDate" runat="server" Enabled="false" CssClass="textBoxReport" ></asp:TextBox>
            <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/calendar.gif" />
            <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtDate" PopupButtonID="Image2" Format="dd/MM/yyyy" FirstDayOfWeek="Sunday">
            </cc1:CalendarExtender>
            
            <span> </span>         
            <span> </span><asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" OnClick="btnSearch_Click" Enabled="false" ValidationGroup="sc" />
         </div><div id="containerReport">
                <asp:Label ID="lblNoRecord" runat="server" Text="" Visible="false" ForeColor="Red"></asp:Label>
                <%--<div style="margin:0px auto 0px auto; text-align:center;">--%>
         <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="1001px" Height="533px">
         </rsweb:ReportViewer>
               <%--</div>--%>
               </div>
          </ContentTemplate>
        </asp:UpdatePanel>      
    </div>
</asp:Content>

