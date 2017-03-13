<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title></title>
     <meta name="viewport" content="width=device-width" />
    <meta http-equiv="X-UA-Compatible" content="IE-EmulateIE7" />
    <link href="Content/RCOStyleSheet.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
        .lblLabelLogin
        {
            color: #CC3300;
            font-family:Calibri;
            font-size:small;
            color:red;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id = "wrapperLogin">  
          <div id="headerLogo">              
                <img src ="Images/wblogo.png" alt="Wema Logo" />
           </div>
        <div id="thinLineLogin">Wema Bank Plc, RC 575</div><br /><br /><br /><br />
        <div id ="containerLoginMain">
            <div id ="containerLoginLeft"></div>
            <div id ="containerLoginRight">Welcome to Wema Bank
                <br /><br />
                <div id ="rcoattnRegister">RCO Attendance Register <br /><br />
                   <div id = "loginControls">
                       <asp:Label ID="Label3" runat="server" Text="Username:" CssClass="Label"></asp:Label>
                       <br />
                       <asp:TextBox ID="txtUsername" runat="server" CssClass="txtBoxLogin" ></asp:TextBox>
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUsername" ErrorMessage="*" Font-Size="X-Small" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                       <br /><br />
                       <asp:Label ID="Label2" runat="server" Text="Password:" CssClass="Label"></asp:Label><br />
                       <asp:TextBox ID="txtPassword" runat="server" CssClass="txtBoxLogin" TextMode="Password"></asp:TextBox>
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword" ErrorMessage="*" Font-Size="X-Small" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                       <br />
                       <div style="text-align:center;">
                           <asp:Label ID="msgLabel" runat="server" Text="" CssClass="lblLabelLogin"></asp:Label> </div><br />
                       <div style ="float:right; padding-right:20px;">
                           <asp:ImageButton ID="ImgLogin" runat="server" ImageUrl ="~/Images/Enter.png" OnClick="ImgLogin_Click"/></div>
                   </div>
                </div>
                
            </div>
            
        </div>
    </div>
    </form>
</body>
</html>
