using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
namespace WebApplication1
{
    public partial class loginadmin : System.Web.UI.Page
    {
        ketnoi kn = new ketnoi();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_dnhapadmin_Click(object sender, EventArgs e)
        {
            SqlDataAdapter adadmin = new SqlDataAdapter("select * from NhanVien where Taikhoan ='" + txt_tkadmin.Text + "' AND  Matkhau= '" + txt_mkadmin.Text +"'" , kn.con);
            DataTable tbadmin = new DataTable();
            adadmin.Fill(tbadmin);
            if (tbadmin.Rows.Count > 0)
            {
                string date = tbadmin.Rows[0]["NgaySinh"].ToString();
                string sdt = tbadmin.Rows[0]["Sodienthoai"].ToString();
                string fullname = tbadmin.Rows[0]["HoTen"].ToString();
                string img = tbadmin.Rows[0]["HinhDaiDien"].ToString();
                string mavn = tbadmin.Rows[0]["MaNV"].ToString();
                Session["date"] = date;
                Session["MNV"] = mavn;
                Session["sdt"] = sdt;
                Session["username"] = txt_tkadmin.Text;
                Session["fullname"] = fullname;
                Session["imgadmin"] = img;
                Session["admin"] = true;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SuccessMessage", "alert('Bạn đã đăng nhập thành công!'); window.location='thongke.aspx';", true);

            }else ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ErrorMessage", "alert('Username or Password chưa đúng');", true);


        }
                
  }
}
