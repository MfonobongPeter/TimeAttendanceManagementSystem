<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Oops.aspx.cs" Inherits="ErrorPages_Oops" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="Content/RCOStyleSheet.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
   <div id ="wrapperCustomError">
        <div id ="CustomerrorLogo">
        </div><br /><br />
        <div id="CustomerrorMessage">
            <div id="CustomerrorMessage2">Service Temporarily Unavailable</div>
            <br />
            <div>                
                <div id ="CustomerrorImage"></div>
                Something unexpected happened. Our support team has been notified, please try 
                         again shortly or call customer service.
                <asp:Label ID="lblErrorCode" runat="server" Text=""></asp:Label>
        </div>
        </div>
        <br />
    </div>
    </form>
</body>
</html>
