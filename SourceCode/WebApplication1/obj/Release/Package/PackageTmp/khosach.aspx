<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="khosach.aspx.cs" Inherits="WebApplication1.khosach" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="main-product">
         <div class="df" >
               <hr /> <h3 class="a_decoration df_item">Thể loại sách</h3><hr />
                <div class="df_1">
                    <% 
                            List<string> matheloaiList = (List<string>)Session["matheloaiList"];
                            List<string> tentheloaiList = (List<string>)Session["tentheloaiList"];
                            for(int i = 0; i<tentheloaiList.Count; i++)
                            {%>
                               <hr /> <a class="a_decoration df_item" href="sachtheoloai.aspx?ml=<%= matheloaiList[i]%>"><%= tentheloaiList[i]%></a><hr />
                            <%}
                    
                    %>
                </div>
     </div> 
      <div  style="min-width: 76px;max-width: 76px;"></div>
    <div class="main">
    <% 
        List<string> maSachList = (List<string>)Session["MaSachList"];
        List<string> tenSachList = (List<string>)Session["TenSachList"];
        List<string> hinhanhList = (List<string>)Session["hinhanhList"];

        if (maSachList != null && tenSachList != null) 
        {
            for (int i = 0; i < maSachList.Count; i++) 
            { 
    %>
                <div class="hover-sanpham" style="margin-top: 10px;width:30%;margin-left:10px">
                    <a href="chitietsanpham.aspx?Id=<%= maSachList[i]%>" style="text-decoration:none; color:black;">
                    <div  style="display: flex;width:100%;box-shadow: 0px 0px 5px -2px;align-items: center;flex-direction: column; background-color:white; border-radius:5px;">
                        <img src="<%= hinhanhList[i] %>" style="width:100%" />
                        <hr style="width:100%; color:white;" />
                        <p> <%= tenSachList[i] %> </p>
                    </div>
                    </a>
                </div>
    <% 
            } 
        } 
    %> 
</div>
        
    </div>
  &nbsp
</asp:Content>
