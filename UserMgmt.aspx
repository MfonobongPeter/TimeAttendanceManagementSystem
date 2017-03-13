<%@ Page Title="" Language="C#" MasterPageFile="~/Common/ISControl.master" AutoEventWireup="true" CodeFile="UserMgmt.aspx.cs" Inherits="UserMgmt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="container">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
        <ContentTemplate>
      <div id ="containerControlDash">
         <div id="userManangement">User Management</div><br />
          
            <asp:Label ID="Label1" runat="server" CssClass="Label"  Text="Enter Username:"></asp:Label><br /><asp:TextBox ID="txtUserName" runat="server" AutoPostBack ="true" CssClass="txtBoxLogin" OnTextChanged="txtUserName_TextChanged" ValidationGroup="m"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtUserName" Font-Size="X-Small" ForeColor="#CC3300" SetFocusOnError="True" ValidationGroup="m"></asp:RequiredFieldValidator><br /><br />
          <asp:Label ID="Label2" runat="server"  CssClass="Label" Text="UserId:"></asp:Label><span> </span><asp:Label ID="lblUserIdDsp" runat="server" Text="" style="font-size: large; color: #FF3300; font-weight: 700;"></asp:Label><br /><br />
          <asp:Label ID="Label3" runat="server" CssClass="Label" Text="Select Role:"></asp:Label>
            <asp:RadioButtonList ID="RadioButtonList1" runat="server">              
                <asp:ListItem>RCO</asp:ListItem>
                <asp:ListItem>Audit</asp:ListItem>
                <asp:ListItem>ISControl</asp:ListItem>              
            </asp:RadioButtonList>
          <br /><br />            
          <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID ="UpdatePanel1">
              <ProgressTemplate>
                  <asp:Image ID="Image2" runat="server" ImageUrl ="~/Images/animated.gif" Height="18px" Width="19px" />
              </ProgressTemplate>
          </asp:UpdateProgress>
            <div style="padding-left:20px;"><asp:Label ID="lblMsg" runat="server" Text="" ></asp:Label></div><br />
          <div style="padding-left:20px;"><asp:Button ID="btnSave" runat="server" CssClass="button" Text="Create" OnClick="btnSave_Click" ValidationGroup="m" /><span> </span><span> </span>
              <asp:Button ID="btnUpdate" runat="server" CssClass="button" Text="Update" OnClick="btnUpdate_Click" ValidationGroup="m" />
          </div>         
     </div><br />
             <br />
            <asp:Label ID="lblUserActivation" runat="server" style="font-weight: 700; color: #009900"></asp:Label>
            <br />
         <asp:GridView ID="GridView1" AutoGenerateColumns="False" runat="server" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" ForeColor="Black" Width="1156px" DataKeyNames="username,staffId" OnRowCommand="GridView1_RowCommand" CellSpacing="2" OnRowDataBound="GridView1_RowDataBound">
        <Columns>  
              <asp:TemplateField HeaderText="S/N" ControlStyle-Width="20px" ItemStyle-HorizontalAlign ="Center" ItemStyle-VerticalAlign ="Middle">
                  <ItemTemplate>
                     <%# Container.DataItemIndex + 1 %>
                  </ItemTemplate>
                  <controlstyle width="20px" />
                  <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
              </asp:TemplateField>
            <asp:BoundField DataField="StaffId" HeaderText ="StaffID" />
            <asp:BoundField DataField="Username" HeaderText ="Username" />
            <asp:BoundField DataField="Surname" HeaderText ="Surname" /> 
            <asp:BoundField DataField="FirstName" HeaderText ="Firstname" /> 
            <asp:BoundField DataField="Branch" HeaderText ="Branch" />
            <asp:BoundField DataField="UserRoles" HeaderText ="UserRole" />
            <asp:BoundField DataField="DateCreated" HeaderText ="DateCreated" />
            <asp:BoundField DataField="IsActive" HeaderText ="IsActive" />
            <asp:TemplateField HeaderText="Action" ControlStyle-Width ="100">
                <ItemTemplate>
                    <asp:Button ID="btnActivate" runat="server" CommandName="activate" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text="Activate" OnClientClick="return confirm('Are you sure you want to activate this user?')" BackColor="Green" ForeColor="White" />
                 <asp:Button ID="btnDeactivate" runat="server" CommandName="deactivate" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text="Deactivate" OnClientClick="return confirm('Are you sure you want to deactivate this user?')" BackColor="Maroon" ForeColor="White" />
                </ItemTemplate>
                <controlstyle width="100px" />
                <ItemStyle Width="100px" />
            </asp:TemplateField>
              </Columns>
                <FooterStyle BackColor="#CCCCCC" />
                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                <RowStyle BackColor="White" />
                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                <sortedascendingcellstyle backcolor="#F1F1F1" />
                <sortedascendingheaderstyle backcolor="#808080" />
                <sorteddescendingcellstyle backcolor="#CAC9C9" />
                <sorteddescendingheaderstyle backcolor="#383838" />
             </asp:GridView>
         </ContentTemplate>
    </asp:UpdatePanel>  
     </div>
</asp:Content>

