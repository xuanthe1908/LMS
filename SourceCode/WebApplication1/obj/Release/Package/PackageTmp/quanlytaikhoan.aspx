<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="quanlytaikhoan.aspx.cs" Inherits="WebApplication1.quanlytaikhoan" %>

<meta charset="utf-8" />
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link href="https://fonts.googleapis.com/css?family=Nunito:200,200i,300,300i,400,400i,600,600i,700,700i,800,800i,900,900i" rel="stylesheet">
    <script src="https://kit.fontawesome.com/f2ab765498.js" crossorigin="anonymous"></script>
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
                    <div style="width:250px; height:300px; box-shadow:0px 0px 5px -2px; position:absolute; right:60px;top:133px">
                        <div style="width:96%;margin-left:2%;">
                            <h4 style="text-align:center;">Độc giả đang mượn sách:</h4>
                            
                                    <table style="width:100%;">
                                        <tr>
                                            <th>
                                                Mã độc giả
                                            </th>
                                             <th>
                                                Tên độc giả
                                            </th>
                                        </tr>
                            <% 
                                 List<string> madgList = (List<string>)Session["madgList"];
                                 List<string> tendgList = (List<string>)Session["tendgList"];
                           

                                if (madgList != null && tendgList != null) 
                                {
                                    for (int i = 0; i < madgList.Count; i++) 
                                    {%> 
                                        <tr>
                                            <td style="text-align:center;">
                                                <%= madgList[i]%>
                                            </td>
                                            <td>
                                                <%= tendgList[i]%>
                                            </td>
                                        </tr>
                                    
                                    
                                    <%}
                                }
                                else
                                 {%>
                                    <tr>
                                            <td style="text-align:center;">
                                               
                                            </td>
                                            
                                        </tr>
                                 <%}
                            %>
                            </table> 
                        </div>
                        
                    </div>
                    &nbsp
                  <div class="main-item2-2">
                   <div class="layout">

                    <asp:TextBox ID="txtmadocgia" runat="server" Visible="False"></asp:TextBox>
                    <br />
                    <asp:Label ID="Label1" runat="server" Text="Tên đăng nhập:"></asp:Label>
&nbsp;<asp:Label ID="lblerror" runat="server"></asp:Label>
                    <br />
                    <asp:TextBox ID="txtusername" runat="server"></asp:TextBox>
                    <br />
                    
                    <asp:Label ID="Label6" runat="server" Text="Họ tên:"></asp:Label>
                    <br />
                    <asp:TextBox ID="txtfullname" runat="server"></asp:TextBox>
                    <br />
                    <asp:Label ID="Label3" runat="server" Text="Email:"></asp:Label>
                    <br />
                    <asp:TextBox ID="txtemail" runat="server"></asp:TextBox>
                    <br />
                    <asp:Label ID="Label4" runat="server" Text="Địa chỉ:"></asp:Label>
                    <br />
                    <asp:TextBox ID="txtdiachi" runat="server"></asp:TextBox>
                    <br />
                    <asp:Label ID="Label7" runat="server" Text="Hình ảnh:"></asp:Label>
                    <br />
                    <asp:FileUpload ID="uphinhsach" runat="server" />
                    <br />
                    <asp:Button ID="btn_sua" runat="server" Text="Sửa" Visible="False" OnClick="btn_sua_Click"  />
                    <asp:Button ID="btn_xoa" runat="server" Text="Xoá" Visible="False" OnClick="btn_xoa_Click" />
                    <asp:Button ID="btn_xemsachmuon" runat="server" Text="Xem sách mượn" Visible="False" OnClick="btn_xensachmuon_Click"  />
                    <asp:Button ID="btn_xemsachtra" runat="server" Text="Xem sách trả" Visible="False" OnClick="btn_xemsachtra_Click"  />
                    <br />
                   </div>
                  </div> 
                    <asp:GridView ID="gvtaikhoan" runat="server" AutoGenerateColumns="False" Height="187px" OnSelectedIndexChanged="gvtaikhoan_SelectedIndexChanged" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                        <Columns>
                            <asp:CommandField ShowSelectButton="True" />
                            <asp:BoundField DataField="Madocgia" HeaderText="Mã độc giả" />
                            <asp:BoundField DataField="Taikhoan" HeaderText="Tên đăng nhập" />
                            <asp:BoundField DataField="MatKhau" HeaderText="Mật khẩu" Visible="False" />
                            <asp:BoundField DataField="HoTen" HeaderText="Họ Tên" />
                            <asp:BoundField DataField="DiaChi" HeaderText="Địa chỉ" />
                            <asp:BoundField DataField="Email" HeaderText="Email" />
                            <asp:TemplateField HeaderText="Hình ảnh" ItemStyle-CssClass="hinhsach">
                                <ItemTemplate>
                                    <img src ="<%#Eval("Hinhdaidien")%>" style="height:100px;" />
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
