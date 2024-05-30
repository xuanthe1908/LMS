<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="admin.aspx.cs" Inherits="WebApplication1.admin" %>
<meta charset="utf-8" />
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link
        href="https://fonts.googleapis.com/css?family=Nunito:200,200i,300,300i,400,400i,600,600i,700,700i,800,800i,900,900i"
        rel="stylesheet">

    <!-- Custom styles for this template-->
    <script src="https://kit.fontawesome.com/f2ab765498.js" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/3.7.0/chart.min.js"></script>
    <link rel="stylesheet" href="admin-style.css" />
    <script src="scripts/change.js" type="text/javascript"></script>
</head>
<body style="height: 489px">
    <form id="form1" runat="server">
    <div class="body-item">
        <div class="body-item1">
            <div class="main-item1">
                <h2 style="color:white;max-height: 80.51px;min-height: 80.51px;display: flex;align-items: center;justify-content: center; margin:0px;"><a href="admin.aspx" style="color:white;">Trang Admin</a></h2>
                <hr/>
                <div class="admin-item">
                    <a href="thongke.aspx"><i class="fa-solid fa-square-poll-vertical" style="color: #ffffff; font-size: 1.625em;"></i>&nbsp Thống kê</a>
                    <hr/>
                    <a href="quanlysach.aspx"><i class="fa-solid fa-book fa-lg" style="color: #ffffff;font-size: 1.625em;"></i>&nbsp Quản lý sách</a>
                    <hr/>
                    <a href="quanlytaikhoan.aspx"><i class="fa-solid fa-users" style="color: #ffffff;font-size: 1.625em;"></i>&nbsp Quản lý tài khoản</a>
                    <hr/>
               </div>
                
            </div>
        </div>
        <div class="body-item2">
            <div class="nav-item1">
                 <%if (Session["admin"] !=null)
                    {%>
                    <div style="display:flex;flex-direction: column;align-items: flex-end; margin-right:15px; justify-content: center;">
                        <p>Xin chào:<a href="admin.aspx" "> <%Response.Write(Session["username"].ToString()); %></a></p>
                        <a style="color:black;" href="logoutadmin.aspx">Đăng xuất</a>
                    </div>
                    
                    <img src ="<%Response.Write(Session["imgadmin"].ToString()); %>"/>
                        <%}
                            else
                            {%>
                                <p></p>
                            <%}

                         %>
            </div>
            <div class="nav-item2">
                <div class="main-item2">
                    <%if (Session["admin"] !=null)
                    {%>
                    <div class="infor-admin">
                        <img style="width:300px" src ="<%Response.Write(Session["imgadmin"].ToString()); %>"/>
                       
                        
                        <div class="infor">
                            <div>
                                 <p><b>Tên quản trị: </b><p><br />
                                 <asp:TextBox ID="txtnameadmin" runat="server" ></asp:TextBox>
                            </div>
                            <div>
                                 <p><b>Mã nhân viên: </b><p><br />
                                 <asp:TextBox ID="txtmnv" runat="server" ReadOnly="True"></asp:TextBox>
                            </div>
                        </div>
                        <div class="infor">
                            <div>
                                 <p><b>Số điện thoại: </b><p><br />
                                 <asp:TextBox ID="txtsdt" runat="server"></asp:TextBox>
                            </div>
                            <div>
                                 <p><b>Ngày sinh: </b><p><br />
                                 <asp:TextBox ID="txtdate" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                     <div style="text-align:end;">
                            <button id="btnSuaTrangCaNhan">Chỉnh sửa trang cá nhân</button>
                     </div>
                    <div class="infor-admin change">
                       
                        <div class="infor">
                            <div>
                                 <p><b>Tên quản trị: </b><p>
                                 <asp:TextBox ID="TextBox1" runat="server" ></asp:TextBox><br />
                           
                                 <p><b>Số điện thoại: </b><p>
                                 <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                            
                                 <p><b>Ngày sinh: </b><p>
                                 <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                            </div>
                            
                        </div>
                            <div style="display: flex;flex-direction: column;justify-content: center;">
 
                                <asp:FileUpload ID="FileUpload1" runat="server" /><br /><br />
                                <asp:Button ID="btnsuaadmin" runat="server" Text="Sửa" OnClick="btnsuaadmin_Click" />

                            </div>
                    
                        </div>
                        <%}
                            else
                            {%>
                                <p></p>
                            <%}

                         %>
                    
                </div>
                <%--<div class="main-item3">
                    <div>
                        <canvas id="myBarChart" style="max-width:60%; max-height:400px; margin-left:20%;"></canvas>
                    </div>
            </div>--%>
        </div>
    </div>
    </form>

</body>
</html>
