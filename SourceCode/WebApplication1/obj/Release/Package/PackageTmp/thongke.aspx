<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="thongke.aspx.cs" Inherits="WebApplication1.thongke" %>

<meta charset="utf-8" />
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link
        href="https://fonts.googleapis.com/css?family=Nunito:200,200i,300,300i,400,400i,600,600i,700,700i,800,800i,900,900i"
        rel="stylesheet">

    <!-- Custom styles for this template-->
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
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
                <div class="main-item2" style="box-shadow:none;display:flex; justify-content:space-around; min-height:102px; max-height:112px; ">
                    <div style="border-radius:5px;width:24%; box-shadow: 0px 0px 5px -2px; max-height:102px; min-height:102px; display:flex; align-items:center; ">
                        <div style="width:70%; margin-left:5%;">
                            <p style="color:#013584;">Sách được mượn</p>
                            <h3>
                                <asp:Label ID="lbldamuon" runat="server"></asp:Label>
                            </h3>
                        </div>
                        <div><img width="53" height="53" src="https://img.icons8.com/external-vectorslab-outline-color-vectorslab/53/external-Book-Reading-education-and-science-vectorslab-outline-color-vectorslab.png" alt="external-Book-Reading-education-and-science-vectorslab-outline-color-vectorslab"/></div>
                    </div>
                     <div style="border-radius:5px;width:24%; box-shadow: 0px 0px 5px -2px; max-height:102px; min-height:102px; display:flex; align-items:center; ">
                        <div style="width:70%; margin-left:5%;">
                           <p style="color:#013584;">Sách còn lại</p>
                            <h3>
                                <asp:Label ID="lblconlai" runat="server"></asp:Label>
                            </h3>
                        </div>
                        <div><img width="64" height="64" src="https://img.icons8.com/external-kiranshastry-lineal-color-kiranshastry/64/external-bookcase-interiors-kiranshastry-lineal-color-kiranshastry.png" alt="external-bookcase-interiors-kiranshastry-lineal-color-kiranshastry"/></div>
                    </div>
                    <div style="border-radius:5px;width:24%; box-shadow: 0px 0px 5px -2px; max-height:102px; min-height:102px; display:flex; align-items:center; ">
                        <div style="width:70%; margin-left:5%;">
                           <p style="color:#013584;">Thể loại</p>
                            <h3>
                                <asp:Label ID="lbltheloai" runat="server"></asp:Label>
                            </h3>
                            
                           
                        </div>
                        <div>
                            <img width="64" height="64" src="https://img.icons8.com/color/48/sorting-answers.png" alt="sorting-answers"/>
                            
                        </div>
                    </div>
                     <div style="border-radius:5px;width:24%; box-shadow: 0px 0px 5px -2px; max-height:102px; min-height:102px; display:flex; align-items:center; ">
                        <div style="width:70%; margin-left:5%;">
                           <p style="color:#013584;">Độc giả</p>
                            <h3>
                                <asp:Label ID="lbldocgia" runat="server"></asp:Label>
                            </h3>
                        </div>
                        <div><img width="64" height="64" src="https://img.icons8.com/color/48/man_reading_a_book.png" alt="man_reading_a_book"/></div>
                    </div>
                   </div>
                <div class="main-item3" style="display: flex;justify-content: space-around;align-items: center;" >
                    <div style="width:45%;">
                        <canvas id="myBarChart" style="width:100%;max-width:100%;min-width:80%; max-height:400px;"></canvas>
                    </div>
                    <div style="width:45%;">
                        <div id="chart_div" style="width: 500px; height: 300px;"></div>
                    </div>
            </div>
        </div>
    </div>
        </div>
    </form>


</body>
</html>