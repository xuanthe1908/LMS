<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="quanlysach.aspx.cs" Inherits="WebApplication1.quanlysach" %>

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
                   
                        <div class="layout">
                        <asp:Label ID="Label1" runat="server" Text="Mã sách:"></asp:Label>
&nbsp;<asp:Label ID="lblerror" runat="server"></asp:Label>
                    <br />
                    <asp:TextBox ID="txtmasach" runat="server"></asp:TextBox>
                    <br />
                    <asp:Label ID="Label2" runat="server" Text="Tên sách:"></asp:Label>
                    <br />
                    <asp:TextBox ID="txttensach" runat="server"></asp:TextBox>
                    <br />
                    <asp:Label ID="Label8" runat="server" Text="Số Lượng:"></asp:Label>
                            &nbsp;<asp:Label ID="lblerrorsl" runat="server"></asp:Label>
                    <br />
                    <asp:TextBox ID="txtsoluong" runat="server"></asp:TextBox>
                    <br />
                    <asp:Label ID="Label3" runat="server" Text="Chi tiết:"></asp:Label>
                    <br />
                    <textarea class="ckeditor" runat="server" id ="ckcontent"></textarea>
                    <br />
                    <asp:Label ID="Label4" runat="server" Text="Thể loại:"></asp:Label>
                    <asp:DropDownList ID="DDLtheloai" runat="server" DataSourceID="SqlDataSource1" DataTextField="TenTheLoai" DataValueField="MaTheLoai">
                    </asp:DropDownList>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:QLTV_ASPConnectionString %>" SelectCommand="SELECT * FROM [TheLoai]"></asp:SqlDataSource>
                     <asp:Button ID="btn_xemtheloai" runat="server" OnClick="btn_xemtheloai_Click" Text="Xem" />
                    <br />
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:QLTV_ASPConnectionString %>" SelectCommand="SELECT * FROM [TheLoai]"></asp:SqlDataSource>
                    <asp:Label ID="Label6" runat="server" Text="Năm xuất bản:"></asp:Label>
                    <br />
                    <asp:TextBox ID="txtnamxb" runat="server"></asp:TextBox>
                    <br />
                    <asp:Label ID="Label7" runat="server" Text="Hình ảnh:"></asp:Label>
                    <br />
                    <asp:FileUpload ID="uphinhsach" runat="server" />
                    <br />
                    <asp:Button ID="btn_them" runat="server" OnClick="btn_them_Click" Text="Thêm" />
                    <asp:Button ID="btn_sua" runat="server" Text="Sửa" Visible="False" OnClick="btn_sua_Click"  />
                    <asp:Button ID="btn_xoa" runat="server" Text="Xoá" Visible="False" OnClick="btn_xoa_Click" />
                    <br />
                    </div>
                        
                    </div> 
                    <asp:GridView ID="gvsach" runat="server" AutoGenerateColumns="False" Height="187px" OnSelectedIndexChanged="gvsach_SelectedIndexChanged" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                        <Columns>
                            <asp:CommandField ShowSelectButton="True" />
                            <asp:BoundField DataField="MaSach" HeaderText="Mã sách" ReadOnly="True" />
                            <asp:BoundField DataField="TenSach" HeaderText="Tên sách" ItemStyle-Width="150px"/>
                            <asp:BoundField DataField="SoLuong" HeaderText="Số lượng" />
                            <asp:BoundField DataField="MaTheLoai" HeaderText="Mã thể loại" />          
                            <asp:BoundField DataField="NamXuatBan" HeaderText="Năm xuất bản" />
                            <asp:BoundField DataField="ChiTiet" HeaderText="Chi tiết" ItemStyle-Width="250px" ControlStyle-CssClass="chi_tiet" Visible="False" />
                            <asp:TemplateField HeaderText="Hình ảnh" ItemStyle-CssClass="hinhsach">
                                <ItemTemplate>
                                    <img src ="<%#Eval("Hinhanh")%>" style="height:116px;" />
                                </ItemTemplate>
                            </asp:TemplateField>
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
