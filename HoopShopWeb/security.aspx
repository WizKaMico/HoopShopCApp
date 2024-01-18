<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="security.aspx.cs" Inherits="HoopShopWeb.security" %>

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
        <center><asp:Label ID="lblEmail" runat="server" Text="" Style="font-weight: bold; text-align:center;"></asp:Label></center>
        <div class="row" style="margin-top:10px;">
            <label for="email">4-DIGIT CODE</label>
            <asp:TextBox ID="txtCode" class="rounded-input" runat="server" ToolTip="Enter 4-Digit Code" TextMode="Number"></asp:TextBox>
         </div>
          <asp:Button ID="btnValidate" class="button" runat="server" Text="VALIDATE" OnClick="btnValidate_Click"/>
    </form>
</body>
</html>
