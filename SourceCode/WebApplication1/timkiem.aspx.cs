using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
namespace WebApplication1
{
    public partial class timkiem : System.Web.UI.Page
    {
        ketnoi kn = new ketnoi();
        protected void Page_Load(object sender, EventArgs e)
        {
            hienthi();

            int n = 5;
            for(int i=1; i<n; i++)
            {
                n--;
            }
            if (n == 0)
            {
                Session.Remove("masach");
                Session.Remove("tensach");
                Session.Remove("imgsach");
                Session.Remove("chitiet");
                Session.Remove("sl");
            }
            
        }
        protected void hienthi()
        {
            
            string search = Request.QueryString["search"];
            if (!string.IsNullOrEmpty(search))
            {
                SqlCommand cmd = new SqlCommand("select * from Sach where TenSach LIKE @search", kn.con);
                cmd.Parameters.AddWithValue("@search", "%" + search + "%");
                kn.con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                kn.con.Close();
                if (dataTable.Rows.Count > 0)
                {
                    string sl = dataTable.Rows[0]["SoLuong"].ToString();
                    string tensach = dataTable.Rows[0]["TenSach"].ToString();
                    string img = dataTable.Rows[0]["Hinhanh"].ToString();
                    string masach = dataTable.Rows[0]["MaSach"].ToString();
                    Session["masach"] = masach;
                    Session["tensach"] = tensach;
                    Session["imgsach"] = img;
                    Session["chitiet"] = search;
                    Session["sl"] = sl;

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SuccessMessage", "alert('Sách bạn tìm không tồn tại!'); window.location='Default.aspx';", true);
                }

            }
          
        }
    }
}