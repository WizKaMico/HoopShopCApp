<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="HoopShopWeb.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>HOOP SHOP | MARKET CHECK</title>
    <link rel="stylesheet" href="./main/login-style.css">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
</head>
<body>
    <form id="form1" class="form" runat="server" style="margin-top:100px;">
        <img src="./main/logo/hoop-logo.png" style="width:100%;"/>
        <div class="row">
            <label for="email">Email</label>
            <asp:TextBox ID="txtEmail" runat="server" TextMode="Email"></asp:TextBox>
         </div>
         <div class="row">
            <label for="password">Password</label>
             <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
          </div>
          <asp:Button ID="submit" class="button" runat="server" Text="Login"  OnClick="btnLogin_Click"/>
    </form>
</body>
</html>
