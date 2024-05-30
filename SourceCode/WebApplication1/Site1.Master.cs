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
    public partial class Site1 : System.Web.UI.MasterPage
    {
        ketnoi kn = new ketnoi();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_dky_Click(object sender, EventArgs e)
        {
            string diachi = txt_diachi.Text;
            string email = txt_email.Text;
            string fullname = txt_fullname.Text;
            string username = txt_namedky.Text;
            string password = txt_passdky.Text;
            string repass = txt_repass.Text;
            string passmahoa = kn.mahoa(password);
            if (Page.IsValid)
            {
                if (password == repass)
                {
                    if (Upload_Avatar.HasFile && CheckFileType(Upload_Avatar.FileName))
                    {
                        if (diachi == "")
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SuccessMessage", "alert('Địa chỉ không được để trống!'); window.location='Default.aspx';", true);
                        }
                        else if(email == "")
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SuccessMessage", "alert('Email không được để trống!'); window.location='Default.aspx';", true);
                        }
                        else
                        {
                            kn.con.Open();
                            string fileName = "images/" + DateTime.Now.ToString("ddMMyyyy_hhmmss_tt_") + Upload_Avatar.FileName;
                            string filePath = MapPath(fileName);
                            Upload_Avatar.SaveAs(filePath);
                            string sql = "INSERT INTO DocGia (Taikhoan, Matkhau, Email, HoTen, Hinhdaidien, DiaChi) VALUES (@username, @passmahoa, @email, @fullname, @filename, @diachi)";
                            SqlCommand cmd = new SqlCommand(sql, kn.con);
                            cmd.Parameters.AddWithValue("@username", username);
                            cmd.Parameters.AddWithValue("@passmahoa", passmahoa);
                            cmd.Parameters.AddWithValue("@email", email);
                            cmd.Parameters.AddWithValue("@fullname", fullname);
                            cmd.Parameters.AddWithValue("@filename", fileName);
                            cmd.Parameters.AddWithValue("@diachi", diachi);
                            cmd.ExecuteNonQuery();

                            kn.con.Close();
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SuccessMessage", "alert('Bạn đã đăng ký thành công tài khoản!'); window.location='Default.aspx';", true);

                        }
                    }
                    else
                    {
                        if (diachi == "")
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SuccessMessage", "alert('Địa chỉ không được để trống!'); window.location='Default.aspx';", true);
                        }
                        else if (email == "")
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SuccessMessage", "alert('Email không được để trống!'); window.location='Default.aspx';", true);
                        }
                        else
                        {
                            kn.con.Open();

                            string sql = "INSERT INTO DocGia (Taikhoan, Matkhau, Email, HoTen, Hinhdaidien, DiaChi) VALUES (@username, @passmahoa, @email, @fullname, @filename, @diachi)";
                            SqlCommand cmd = new SqlCommand(sql, kn.con);
                            cmd.Parameters.AddWithValue("@username", username);
                            cmd.Parameters.AddWithValue("@passmahoa", passmahoa);
                            cmd.Parameters.AddWithValue("@email", email);
                            cmd.Parameters.AddWithValue("@fullname", fullname);
                            cmd.Parameters.AddWithValue("@filename", "");
                            cmd.Parameters.AddWithValue("@diachi", diachi);
                            cmd.ExecuteNonQuery();

                            kn.con.Close();
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SuccessMessage", "alert('Bạn đã đăng ký thành công tài khoản!'); window.location='Default.aspx';", true);
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ErrorMessage", "alert('Xác nhận mật khẩu không trùng khớp. Hãy thực hiện lại!');", true);
                }
            }
        }
        bool CheckFileType(string fileName)
        {
            string ext = Path.GetExtension(fileName);
            switch (ext.ToLower())
            {
                case ".gif":
                case ".png":
                case ".jpg":
                case ".jpeg":
                    return true;
                default:
                    return false;
            }
        }

        protected void btn_dnhap_Click(object sender, EventArgs e)
        {
            if (Session["Captcha"].ToString() != txtcapcha.Text)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SuccessMessage", "alert('Mã Captcha sai!'); window.location='Default.aspx';", true);
                txtcapcha.Text = "";
            }
            else
            {
                string passmahoa = kn.mahoa(txt_logpass.Text);
                SqlDataAdapter da = new SqlDataAdapter("select * from DocGia where Taikhoan ='" + txt_logname.Text + "' and Matkhau = '" + passmahoa + "'", kn.con);
                DataTable tb = new DataTable();
                da.Fill(tb);
                if (tb.Rows.Count > 0)
                {
                    string mk = tb.Rows[0]["Matkhau"].ToString();
                    string email = tb.Rows[0]["Email"].ToString();
                    string diachi = tb.Rows[0]["DiaChi"].ToString();
                    string madg = tb.Rows[0]["Madocgia"].ToString();
                    string fullname = tb.Rows[0]["Hoten"].ToString();
                    string img = tb.Rows[0]["Hinhdaidien"].ToString();
                    Session["mk"] = mk;
                    Session["diachi"] = diachi;
                    Session["email"] = email;
                    Session["madg"] = madg;
                    Session["name"] = txt_logname.Text;
                    Session["fullnamedg"] = fullname;
                    Session["img"] = img;
                    Session["allow"] = true;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SuccessMessage", "alert('Bạn đã đăng nhập thành công!'); window.location='Default.aspx';", true);

                }
                else ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ErrorMessage", "alert('Username or Password chưa đúng');", true);

            }
        }
        protected void btn_timkiem_Click(object sender, EventArgs e)
        {
            string searchProductName = txt_timkiem.Text;
            string encodedProductName = HttpUtility.UrlEncode(searchProductName);
                
              

                Response.Redirect("timkiem.aspx?search=" + encodedProductName);
            
          
        }

    }
}