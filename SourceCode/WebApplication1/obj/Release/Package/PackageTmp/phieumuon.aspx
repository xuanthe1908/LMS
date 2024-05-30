<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="phieumuon.aspx.cs" Inherits="WebApplication1.phieumuon" %>

<meta charset="utf-8" />
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link href="https://fonts.googleapis.com/css?family=Nunito:200,200i,300,300i,400,400i,600,600i,700,700i,800,800i,900,900i" rel="stylesheet">
    <script src="https://kit.fontawesome.com/f2ab765498.js" crossorigin="anonymous"></script>
    <script src="scripts/ckeditor/ckeditor.js"></script>
    <link rel="stylesheet" href="admin-style.css" />
</head>
<body style="height: 489px">
    <form id="form1" runat="server">
    <div class="body-item">
        <div class="body-item1">
            <div class="main-item1">
                <h2 style="color:white; max-height: 80.51px;min-height: 80.51px;display: flex;align-items: center;justify-content: center;"><a href="admin.aspx" style="color:white;">Trang Admin</a></h2>
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
                    &nbsp
                    <div class="main-item2-1 print-only" style="min-height:500px;">
                         &nbsp
                        <div style="width:94%; margin-left:3%;">
                           
                            <img style="width:10%;margin-left:45%;" src="images/logo.png">
                            <h3 style="text-align:center;">PHIẾU MƯỢN</h3>
                            <h4 style="text-align:center;">Thư viện Trà Vinh</h4>
                            <br />
                            <p>Địa chỉ: 126 Nguyễn Thiện Thành, Phường 5, TP Trà Vinh, Tỉnh Trà Vinh.</p>
                            <p>Tel. (02943) 855 246 - 102</p>
                            <p>Email: thuvien@gmail.com</p>
                        </div>


                         <asp:GridView ID="gvphieumuon" runat="server" AutoGenerateColumns="False" Height="90px" OnSelectedIndexChanged="gvphieumuon_SelectedIndexChanged" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                        <Columns>
                            <asp:BoundField DataField="TenSach" HeaderText="Tên sách" />
                            <asp:BoundField DataField="SoLuong" HeaderText="Số lượng" /> 
                            <asp:BoundField DataField="Madocgia" HeaderText="Mã độc giả" />                         
                            <asp:BoundField DataField="TenDocGia" HeaderText="Tên độc giả" />                                             
                            <asp:BoundField DataField="NgayMuon" HeaderText="Ngày mượn" />  
                            <asp:BoundField DataField="Ngaytra" HeaderText="Ngày trả" />         
                        </Columns>
                        <FooterStyle BackColor="White" ForeColor="#000066" />
        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
        <RowStyle ForeColor="#000066" />
        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#007DBB" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#00547E" />
                    </asp:GridView>
                    &nbsp
                        <div style="width:94%; margin-left:3%; display:flex; flex-direction:row; justify-content:space-between;">
                        <div>
                            <p><b>Người mượn</b></p>
                            <p><i>(Ký và ghi rõ họ tên)</i></p>
                        </div>
                        <div>
                            <p><b>Người cho mượn</b></p>
                            <p><i>(Ký và ghi rõ họ tên)</i></p>
                        </div>
                        </div>
                    </div> 
                   &nbsp
                   <div style="text-align:end;"> <asp:Button ID="btnPrint" runat="server" Text="In phiếu mượn" OnClientClick="printPage();" /></div>                
                </div>
                
                </div>
        </div>
       </div>
    </form>
    <script type="text/javascript">
    function printPage() {
        window.print();
    }
</script>
</body>
</html>

    