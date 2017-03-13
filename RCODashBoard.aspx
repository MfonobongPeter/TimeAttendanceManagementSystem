<%@ Page Title="" Language="C#" MasterPageFile="~/Common/RCO.master" AutoEventWireup="true" CodeFile="RCODashBoard.aspx.cs" Inherits="RCODashBoard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    <div id="container">
      <div id ="containerControlDash">
         <div id="userManangement">Automated RCO Attendance Register</div><br />
          <asp:CheckBox ID="chkSignIn" runat="server" Text="Sign In" AutoPostBack="true" OnCheckedChanged="chkSignIn_CheckedChanged" />
                     <br />
                     <br />
          <asp:UpdateProgress ID="UpdateProgress1" runat="server">
              <ProgressTemplate>
                  <asp:Image ID="Image1" runat="server"  ImageUrl ="~/Images/Animated.gif"/>
              </ProgressTemplate>
          </asp:UpdateProgress>        
          <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
          <br />
          <br />
     </div>
    </div>           
    </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>

