<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="loginadmin.aspx.cs" Inherits="WebApplication1.loginadmin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <link rel="stylesheet" href="admin-style.css" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width:100%;height:100vh;display:flex;justify-content: center;align-items: center;">
        <div class="bg-login">
            <img src="images/logo.png" width="100px;" />
            <div style="width: 90%;">

                <asp:Label ID="Label1" runat="server" Text="Tên đăng nhập"></asp:Label>
                <br />
                <asp:TextBox ID="txt_tkadmin" runat="server"></asp:TextBox>
                <br />
                <asp:Label ID="Label2" runat="server" Text="Mật khẩu"></asp:Label>
                <br />
                <asp:TextBox ID="txt_mkadmin" runat="server"  TextMode="Password"></asp:TextBox>
                
               
                <br />
            </div><br/>
             <asp:Button ID="btn_dnhapadmin" runat="server" Text="Đăng nhập" OnClick="btn_dnhapadmin_Click" />
        </div>
    </div>
    </form>
</body>
</html>
