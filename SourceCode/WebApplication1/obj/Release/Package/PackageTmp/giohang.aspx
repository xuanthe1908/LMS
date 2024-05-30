<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="giohang.aspx.cs" Inherits="WebApplication1.giohang" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
     <div style="width:80%;margin-left:10%; background-color:white; margin-top:10px;box-shadow:0px 0px 5px -3px;border-radius:5px;">
         &nbsp
         <div style="width:96%;margin-left:2%;margin-top:10px;">
<asp:gridview id="gvgiohang" runat="server" autogeneratecolumns="False" onselectedindexchanged="gvgiohang_SelectedIndexChanged" Width="100%" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
        <Columns>
             <asp:CommandField ShowSelectButton="True" SelectText="Chọn" />
            <asp:BoundField DataField="Masach" HeaderText="Mã Sách" />
            <asp:BoundField DataField="Tensach" HeaderText="Tên Sách" />
            <asp:BoundField DataField="soluong" HeaderText="Số lượng" />
             <asp:TemplateField HeaderText="Hình ảnh" ItemStyle-CssClass="hinhsach">
                                <ItemTemplate>
                                    <img src ="<%#Eval("Hinhanh")%>" style="height:116px;" />
                                </ItemTemplate>

<ItemStyle CssClass="hinhsach"></ItemStyle>
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
      
         <br />
         <br />
             <hr />
         <div style="text-align:end; margin-bottom:10px;">
             <span>Tổng sách mượn:</span> <asp:Label ID="lb_tongsach" runat="server"></asp:Label><br />
             <asp:Button ID="btnthemmasachsua" runat="server" OnClick="btnthemmasachsua_Click" Text="Thêm số lượng" Visible="false" />
             <asp:Button ID="btnsuagiohang" runat="server" OnClick="btnsuagiohang_Click" Text="Sửa" Visible="false" />
             <asp:Button ID="btnxoagiohang" runat="server" OnClick="btnxoagiohang_Click" Text="Xoá" Visible="false" />
             <asp:Label ID="lblslsachsua" runat="server" Text="Nhập số lượng sửa:" Visible="false"></asp:Label>
              <asp:TextBox ID="txtslsachsua" runat="server" Visible="false"></asp:TextBox>
              <asp:TextBox ID="txtsuaxoagiohang" runat="server" Visible="false"></asp:TextBox>
             <% if(Session["hiengh"] != null)
                 {%>
                    <asp:Button ID="btn_xnmuon" runat="server" OnClick="btn_xnmuon_Click" Text="Xác nhận mượn" />
                 <%}
                      
             %>
         </div>
             &nbsp
         </div>    
 </div>
 </div>
   
    
   
</asp:Content>
