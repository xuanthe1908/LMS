<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="quanlymuonsach.aspx.cs" Inherits="WebApplication1.quanlymuonsach" %>

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
                <h2 style="color:white;max-height: 80.51px;min-height: 80.51px;display: flex;align-items: center;justify-content: center;"><a href="admin.aspx" style="color:white;">Trang Admin</a></h2>
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
                    <div class="main-item2-1">
          
                        <div class="layoutmuonsach">
                                    <asp:Label ID="Label4" runat="server" Text="Mã mượn:"></asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtmamuon" runat="server" ReadOnly="True"></asp:TextBox>
                                    <br />
                                    <asp:Label ID="Label1" runat="server" Text="Mã sách:"></asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtmasach" runat="server" ReadOnly="True"></asp:TextBox>
                                    <br />
                                    <asp:Label ID="Label2" runat="server" Text="Số lượng:"></asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtsoluong" runat="server"></asp:TextBox>
                                    <br />
                                    <asp:Label ID="Label3" runat="server" Text="Ghi chú:"></asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtghichu" runat="server"></asp:TextBox>
                                    <br />
                                    <asp:Label ID="Label6" runat="server" Text="Mã nhân viên:"></asp:Label>
                                    <asp:Label ID="lberrormanv" runat="server" Text="" Font-Italic="True"></asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtmanv" runat="server" ReadOnly="True"></asp:TextBox>
                                    <br />
                                    <asp:Label ID="Label7" runat="server" Text="Thời gian mượn:"></asp:Label>
                                    <br />
                                    <asp:TextBox ID="txttgianmuon" runat="server"></asp:TextBox>
                                    <br />
                                    <asp:Label ID="Label12" runat="server" Text="Thời gian trả:"></asp:Label>
                                    <br />
                                    <asp:TextBox ID="txttgiantra" runat="server"></asp:TextBox>
                                    <br />
                                    <asp:Label ID="Label13" runat="server" Text="Trạng thái:"></asp:Label>
                                    <br />
                                    <asp:TextBox ID="txttrangthai" runat="server"></asp:TextBox>
                                    <br />
                            <div>
                                <asp:Button ID="btn_xacnhan" runat="server" Text="Xác nhận" Visible="False" OnClick="btn_xacnhan_Click"  />
                                <asp:Button ID="btn_sua" runat="server" Text="Sửa" Visible="False" OnClick="btn_sua_Click"  />
                                <asp:Button ID="btn_phieumuon" runat="server" Text="Xem phiếu mượn" Visible="True" OnClick="btn_phieumuon_Click" />
                                <br />
                            </div>
                        </div>
                        &nbsp
                    </div> 
                    <asp:GridView ID="gvmuonsach" runat="server" AutoGenerateColumns="False" Height="187px" OnSelectedIndexChanged="gvmuonsach_SelectedIndexChanged" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                        <Columns>
                            <asp:CommandField ShowSelectButton="True" />
                            <asp:TemplateField HeaderText="Hình ảnh" ItemStyle-CssClass="hinhsach">
                                <ItemTemplate>
                                    <img src ="<%#Eval("hinhsach")%>" style="height:116px;" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="MaMuon" HeaderText="Mã mượn" />
                            <asp:BoundField DataField="MaSach" HeaderText="Mã sách" />
                            <asp:BoundField DataField="TenSach" HeaderText="Tên sách" />
                            <asp:BoundField DataField="SoLuong" HeaderText="Số lượng" />
                            <asp:BoundField DataField="Madocgia" HeaderText="Mã độc giả" />          
                            <asp:BoundField DataField="Tendocgia" HeaderText="Tên độc giả" />
                            <asp:BoundField DataField="Diachi" HeaderText="Địa chỉ" />
                            <asp:BoundField DataField="Email" HeaderText="Email" />
                            <asp:BoundField DataField="ghichu" HeaderText="Ghi chú" />          
                            <asp:BoundField DataField="manv" HeaderText="Mã nhân viên" />
                            <asp:BoundField DataField="NgayMuon" HeaderText="Ngày mượn" />          
                            <asp:BoundField DataField="NgayTra" HeaderText="Ngày trả" />
                            <asp:BoundField DataField="trangthai" HeaderText="Trạng thái" />

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
                
                    
                </div>
                
                </div>
        </div>
    </div>
    </form>
</body>
</html>
