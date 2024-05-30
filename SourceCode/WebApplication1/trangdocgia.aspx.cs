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
    public partial class trangdocgia : System.Web.UI.Page
    {
        ketnoi kn = new ketnoi();
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from DocGia where MaDocGia ='" + Session["madg"] + "'", kn.con);
            DataTable tb = new DataTable();
            da.Fill(tb);
            if (tb.Rows.Count > 0)
            {
               
                string mk = tb.Rows[0]["Matkhau"].ToString();
                email.Text = tb.Rows[0]["Email"].ToString();
                diachi.Text = tb.Rows[0]["DiaChi"].ToString();
                string fullname = tb.Rows[0]["Hoten"].ToString();
                string img = tb.Rows[0]["Hinhdaidien"].ToString();
                hoten.Text = fullname;
               
                Session["mk"] = mk;
                Session["diachi"] = diachi;
                Session["email"] = email;
                Session["fullname"] = fullname;
                Session["img"] = img;
                Session["allow"] = true;
            }
        }
        protected void btn_suahoten_Click(object sender, EventArgs e)
        {
            kn.con.Open();
            string sql = "UPDATE DocGia SET HoTen = N'" + txthoten.Text +"'WHERE MaDocGia = '" + Session["madg"] + "'";
            SqlCommand cmd = new SqlCommand(sql, kn.con);
            cmd.ExecuteNonQuery();
            kn.con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SuccessMessage", "alert('Sửa thành công, bạn cần đăng nhập lại'); window.location='logout.aspx';", true);

        }
        protected void btn_suadiachi_Click(object sender, EventArgs e)
        {
            kn.con.Open();
            string sql = "UPDATE DocGia SET DiaChi = N'" + txtdiachi.Text + "'WHERE MaDocGia = '" + Session["madg"] + "'";
            SqlCommand cmd = new SqlCommand(sql, kn.con);
            cmd.ExecuteNonQuery();
            kn.con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SuccessMessage", "alert('Sửa thành công, bạn cần đăng nhập lại'); window.location='logout.aspx';", true);

        }
        protected void btn_suahinh_Click(object sender, EventArgs e)
        {
            kn.con.Open();
            string filename = "images/" + DateTime.Now.ToString("ddMMyyyy_hhmmss_tt_") + uploadhinh.FileName;
            string filepath = MapPath(filename);
            uploadhinh.SaveAs(filepath);
            string sql = "UPDATE DocGia SET Hinhdaidien='" + filename + "'WHERE MaDocGia = '" + Session["madg"] + "'";
            SqlCommand cmd = new SqlCommand(sql, kn.con);
            cmd.ExecuteNonQuery();
            kn.con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SuccessMessage", "alert('Sửa thành công, bạn cần đăng nhập lại'); window.location='logout.aspx';", true);

        }
        protected void btn_suaemail_Click(object sender, EventArgs e)
        {
            kn.con.Open();
            string sql = "UPDATE DocGia SET Email = N'" + txtemail.Text + "'WHERE MaDocGia = '" + Session["madg"] + "'";
            SqlCommand cmd = new SqlCommand(sql, kn.con);
            cmd.ExecuteNonQuery();
            kn.con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SuccessMessage", "alert('Sửa thành công, bạn cần đăng nhập lại'); window.location='logout.aspx';", true);

        }
        protected void btn_suamk_Click(object sender, EventArgs e)
        {
            string pass = txtmkcu.Text;
            string passmoi = txtmkmoi.Text;
            string passmoimahoa = kn.mahoa(passmoi);
            string passcumahoa = kn.mahoa(pass);
            if(Session["mk"].ToString() == passcumahoa)
            {
                if(txtmkmoi.Text == txtxacnhan.Text)
                {
                    kn.con.Open();
                    string sql = "UPDATE DocGia SET Matkhau = N'" + passmoimahoa + "'WHERE MaDocGia = '" + Session["madg"] + "'";
                    SqlCommand cmd = new SqlCommand(sql, kn.con);
                    cmd.ExecuteNonQuery();
                    kn.con.Close();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SuccessMessage", "alert('Sửa thành công, bạn cần đăng nhập lại'); window.location='logout.aspx';", true);

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SuccessMessage", "alert('Mật khẩu xác nhận không khớp'); window.location='trangdocgia.aspx';", true);
                }

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SuccessMessage", "alert('Mật khẩu cũ không khớp'); window.location='trangdocgia.aspx';", true);
            }
        }
    }
}