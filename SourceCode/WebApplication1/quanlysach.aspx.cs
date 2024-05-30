using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Data;

namespace WebApplication1
{
    public partial class quanlysach : System.Web.UI.Page
    {
        ketnoi kn = new ketnoi();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["MNV"] == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SuccessMessage", "window.location='loginadmin.aspx';", true);
            }
            else
            {
                hienthi();
            }
        }
        protected void hienthi()
        {
            kn.con.Open();
            SqlCommand cmd = new SqlCommand("select * from Sach", kn.con);
            SqlDataReader da = cmd.ExecuteReader();
            gvsach.DataSource = da;
            gvsach.DataBind();
            kn.con.Close();
        }

        protected void gvsach_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = gvsach.SelectedRow;
            txtmasach.Enabled = false;
            btn_them.Visible = false;
            btn_sua.Visible = true;
            btn_xoa.Visible = true;
            string masach = Server.HtmlDecode(row.Cells[1].Text);
            string tensach = Server.HtmlDecode(row.Cells[2].Text);
            string soluong = Server.HtmlDecode(row.Cells[3].Text);
            string matloai = HttpUtility.HtmlDecode(row.Cells[4].Text);
            string namxb = Server.HtmlDecode(row.Cells[5].Text);
            SqlDataAdapter da = new SqlDataAdapter("select * from Sach where MaSach ='" + masach + "'", kn.con);
            DataTable tb = new DataTable();
            da.Fill(tb);
            if (tb.Rows.Count > 0)
            {
                string chitiet = tb.Rows[0]["ChiTiet"].ToString();
                txtmasach.Text = masach;
                txtsoluong.Text = soluong;
                txttensach.Text = tensach;
                DDLtheloai.Text = matloai;
                txtnamxb.Text = namxb;
                ckcontent.InnerText = chitiet;
            }
            hienthi();
        }
        bool CheckFileType(string filename)
        {
            string ext = Path.GetExtension(filename);
            switch (ext.ToLower())
            {
                case ".gif":
                    return true;
                case ".png":
                    return true;
                case ".jpg":
                    return true;
                case ".jpeg":
                    return true;
                default:
                    return false;
            }

        }

        protected void btn_them_Click(object sender, EventArgs e)
        {
            kn.con.Open();
            txtmasach.Enabled = true;
            btn_sua.Visible = false;
            btn_xoa.Visible = false;
            string masach = txtmasach.Text;
            string tensach = txttensach.Text;

            string matloai = DDLtheloai.Text;
            string chitiet = ckcontent.InnerText;
            string namxb = txtnamxb.Text;
            string soluong = txtsoluong.Text;
            if (Page.IsValid && uphinhsach.HasFile && CheckFileType(uphinhsach.FileName))
            {
                string Query = "SELECT COUNT(*) FROM Sach WHERE MaSach = @masach";
                SqlCommand cmdktsach = new SqlCommand(Query, kn.con);
                cmdktsach.Parameters.AddWithValue("@masach", masach);
                int count = (int)
                cmdktsach.ExecuteScalar();
                if (count > 0)
                {
                    lblerror.Text = "Mã sách đã tồn tại";
                }
                else
                {
                    int slValue;
                    if (int.TryParse(soluong, out slValue))
                    {
                        if (slValue <= 0)
                        {
                            lblerrorsl.Text = "Số lượng phải dương";
                        }
                        else
                        {
                            string filename = "images/" + DateTime.Now.ToString("ddMMyyyy_hhmmss_tt_") + uphinhsach.FileName;
                            string filepath = MapPath(filename);
                            uphinhsach.SaveAs(filepath);
                            string sql = "INSERT INTO Sach (MaSach, TenSach, MaTheLoai, ChiTiet, NamXuatBan, Hinhanh, SoLuong) VALUES (@masach, @tensach, @matloai, @chitiet, @namxb, @filename, @soluong)";
                            SqlCommand cmd = new SqlCommand(sql, kn.con);
                            cmd.Parameters.AddWithValue("@masach", masach);
                            cmd.Parameters.AddWithValue("@tensach", tensach);
                            cmd.Parameters.AddWithValue("@matloai", matloai);
                            cmd.Parameters.AddWithValue("@chitiet", chitiet);
                            cmd.Parameters.AddWithValue("@namxb", namxb);
                            cmd.Parameters.AddWithValue("@filename", filename);
                            cmd.Parameters.AddWithValue("@soluong", soluong);

                            cmd.ExecuteNonQuery();
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SuccessMessage", "alert('Thêm thành công'); window.location='quanlysach.aspx';", true);

                        }
                    }
                    txtsoluong.Text = "";
                    ckcontent.InnerText = "";
                    txtmasach.Text = "";
                    txttensach.Text = "";
                    txtnamxb.Text = "";
                    kn.con.Close();
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SuccessMessage", "alert('Thêm sách bắt buộc phải có hình'); window.location='quanlysach.aspx';", true);
            }
        }
        protected void btn_sua_Click(object sender, EventArgs e)
        {
            kn.con.Open();
            txtmasach.Enabled = true;
            btn_them.Visible = true;
            btn_sua.Visible = false;
            btn_xoa.Visible = false;
            string masach = txtmasach.Text;
            string tensach = txttensach.Text;
            string matloai = DDLtheloai.Text;
            string namxb = txtnamxb.Text;
            string soluong = txtsoluong.Text;
            string chitiet = ckcontent.InnerText;
            if (Page.IsValid && uphinhsach.HasFile && CheckFileType(uphinhsach.FileName))
            {
                int slValue;
                if (int.TryParse(soluong, out slValue))
                {
                    if (slValue <= 0)
                    {
                        lblerrorsl.Text = "Số lượng phải dương";
                    }
                    else
                    {
                        string filename = "images/" + DateTime.Now.ToString("ddMMyyyy_hhmmss_tt_") + uphinhsach.FileName;
                        string filepath = MapPath(filename);
                        uphinhsach.SaveAs(filepath);
                        string sql = "UPDATE Sach SET TenSach =N'" + tensach + "', MaTheLoai =N'" + matloai + "', ChiTiet =N'" + chitiet + "', NamXuatBan =N'" + namxb + "', Hinhanh='" + filename + "', SoLuong='" + soluong + "' WHERE MaSach = N'" + masach + "'";
                        SqlCommand cmd = new SqlCommand(sql, kn.con);
                        cmd.ExecuteNonQuery();
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SuccessMessage", "alert('Sửa thành công'); window.location='quanlysach.aspx';", true);
                    }
                }
            }
            else
            {
                int slValue;
                if (int.TryParse(soluong, out slValue))
                {
                    if (slValue <= 0)
                    {
                        lblerrorsl.Text = "Số lượng phải dương";
                    }
                    else
                    {
                        string sql = "UPDATE Sach SET TenSach =N'" + tensach + "', MaTheLoai =N'" + matloai + "', ChiTiet =N'" + chitiet + "', NamXuatBan =N'" + namxb + "', SoLuong='" + soluong + "' WHERE MaSach = N'" + masach + "'";
                        SqlCommand cmd = new SqlCommand(sql, kn.con);
                        cmd.ExecuteNonQuery();
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SuccessMessage", "alert('Sửa thành công'); window.location='quanlysach.aspx';", true);
                    }
                }
            }
            txtsoluong.Text = "";
            ckcontent.InnerText = "";
            txtmasach.Text = "";
            txttensach.Text = "";
            txtnamxb.Text = "";
            kn.con.Close();
        }
        protected void btn_xemtheloai_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "SuccessMessage", "window.location='theloai.aspx';", true);
        }
        protected void btn_xoa_Click(object sender, EventArgs e)
        {
            kn.con.Open();
            txtmasach.Enabled = true;
            btn_them.Visible = true;
            btn_sua.Visible = false;
            btn_xoa.Visible = false;
            string masach = txtmasach.Text;
            string sql = "DELETE from Sach where MaSach = N'" + masach + "'";
            SqlCommand cmd = new SqlCommand(sql, kn.con);
            cmd.ExecuteNonQuery();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "SuccessMessage", "alert('Xoá thành công!');window.location='quanlysach.aspx';", true);
            txtsoluong.Text = "";
            ckcontent.InnerText = "";
            txtmasach.Text = "";
            txttensach.Text = "";
            txtnamxb.Text = "";
            kn.con.Close();
        }
    }
}