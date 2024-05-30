<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="timkiem.aspx.cs" Inherits="WebApplication1.timkiem" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="main">
        
    <% 
    %>
                <div>Kết quả tìm kiếm cho: <b> <%Response.Write(Session["chitiet"].ToString()); %></b></div>
                <hr style="width:100%;"/>
                <%--<div style="margin-top: 10px;width:30%;margin-left:10px">
                    <a href="chitietsanpham.aspx?id=<%Response.Write(Session["masach"].ToString());%>" style="text-decoration:none; color:black;">
                    <div style="display: flex;width:100%;box-shadow: 0px 0px 5px -2px;align-items: center;flex-direction: column; background-color:white; border-radius:5px;">
                        <img src="<%Response.Write(Session["imgsach"].ToString()); %>" style="width:100%" />
                        <hr style="width:100%; color:white;" />
                        <p> <%Response.Write(Session["tensach"].ToString()); %> </p>
                    </div>
                    </a>
                </div>--%>



         <div class="hover-sanpham" style="margin-top: 10px;width:30%;margin-left:10px">
                    <a href="chitietsanpham.aspx?Id=<%Response.Write(Session["masach"].ToString());%>" style="text-decoration:none; color:black;">
                    <div  style="display: flex;width:100%;box-shadow: 0px 0px 5px -2px;align-items: center;flex-direction: column; background-color:white; border-radius:5px;">
                        <img src="<%Response.Write(Session["imgsach"].ToString()); %>" style="width:100%" />
                        <hr style="width:100%; color:white;" />
                        <p>  <%Response.Write(Session["tensach"].ToString()); %>  </p>
                    </div>
                    </a>
                </div>
       
    <% 

    %> 
</div>
     &nbsp
</asp:Content>
