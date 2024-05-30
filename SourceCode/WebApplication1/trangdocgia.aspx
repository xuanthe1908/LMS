<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="trangdocgia.aspx.cs" Inherits="WebApplication1.trangdocgia" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width:100%;">
            <div class="infor-docgia">
                &nbsp
                <div style="width: 94%;margin-left: 2%;display:flex;">
                   <img style="max-height:225px;max-width:225px;min-width: 210px;" src="<%Response.Write(Session["img"].ToString()); %>" />
                    <div style="display:flex; width:68%; justify-content: space-evenly; margin-left:20px;">
                         <div>
                             <p>Họ tên: <button id="suaten">sửa</button> <br /> <asp:Label ID="hoten" runat="server" ></asp:Label> </p>
                             <p>Địa chỉ: <button id="suadiachi">sửa</button><br /> <asp:Label ID="diachi" runat="server" ></asp:Label> </p>
                             
                             <p class="hidden-infor1">Họ tên: <br /><asp:textbox runat="server" ID="txthoten"></asp:textbox>
                                 <asp:button runat="server" text="Lưu" ID="btn_suahoten" OnClick="btn_suahoten_Click"/>
                             </p>
                             <p class="hidden-infor2">Địa chỉ: <br /><asp:textbox runat="server" ID="txtdiachi"></asp:textbox>
                                 <asp:button runat="server" text="Lưu" ID="btn_suadiachi" OnClick="btn_suadiachi_Click"/>
                             </p>
                             <button id="suahinh"><p style="padding: 7px;">sửa hình</p></button>
                             <div class="hidden-infor5">
                                 <asp:fileupload runat="server" ID="uploadhinh"></asp:fileupload> 
                                 <br />
                                 <asp:button runat="server" text="Lưu" ID="btn_suahinh" OnClick="btn_suahinh_Click" />
                             </div>
                        </div>
                         <div style=" margin-left: 14px;">
                             <p>Email: <button id="suaemail">sửa</button><br /> <asp:Label ID="email" runat="server" ></asp:Label> </p>
                             <button id="suamk" style="padding: 7px;"><p>sửa mật khẩu</p></button>
                             <p class="hidden-infor4">Email:<br /> <asp:textbox runat="server" ID="txtemail"></asp:textbox>
                              <asp:button runat="server" text="Lưu" ID="btn_suaemail" OnClick="btn_suaemail_Click"/>
                             </p>
                              <div class="hidden-infor3">
                             <p>Mật khẩu cũ: <br /> <asp:textbox runat="server" ID="txtmkcu" TextMode="Password"></asp:textbox>
                             </p>
                             <p>Mật khẩu mới: <asp:textbox runat="server" ID="txtmkmoi" TextMode="Password"></asp:textbox>
                             </p>
                             <p>Xác nhận:<br /> <asp:textbox runat="server" ID="txtxacnhan" TextMode="Password"></asp:textbox>
                             </p>
                                <asp:button runat="server" text="Lưu" ID="btn_suamk" OnClick="btn_suamk_Click"/>
                             </div>
                             </div>
                    </div>
                </div>
            &nbsp
            </div>
            <script src="scripts/change.js" type="text/javascript"></script>
    </div>
</asp:Content>
