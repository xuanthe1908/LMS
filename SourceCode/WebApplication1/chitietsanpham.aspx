<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="chitietsanpham.aspx.cs" Inherits="WebApplication1.chitietsanpham" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="infor-product">
    <div class="img-name-product">
        <div class="img-product">
            <img style="width:100%; border-right: 1px solid;" src="<%Response.Write(Session["imgsach"].ToString()); %>" />
        </div>
        <div class="name-product">
            <h1><%Response.Write(Session["tensach"].ToString()); %></h1>
            <p>Số lượng: <%Response.Write(Session["sl"].ToString()); %></p>
            <% if (Session["allow"] != null) {%> 
            <button id="muonsach" style="padding:7px;">Mượn sách</button>
            <div class="ttmuon">
            
                <asp:Label ID="Label4" runat="server" Text="Số lượng mượn"></asp:Label><br />
                <asp:Label ID="lberrorsoluong" runat="server" Text="" Font-Italic="True"></asp:Label><br/>
                <asp:TextBox ID="soluong" runat="server"></asp:TextBox><br/>
                <asp:Button ID="btn_muon" runat="server" Text="Mượn" OnClick="btn_muon_Click" style="margin-left: 70%;margin-top:5px;"/>
     
            </div>

            <%}
                else
                {%>
            <p>Bạn cần phải đăng nhập để mượn được sách!</p>
                <%}

                %>
            
        </div>
    </div>
    <div class="content-product">
       <%Response.Write(Session["chitiet"].ToString()); %>
    </div>
</div>
<script src="scripts/change.js" type="text/javascript"></script>
</asp:Content>
