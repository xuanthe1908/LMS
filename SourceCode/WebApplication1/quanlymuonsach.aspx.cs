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
using System.Diagnostics;

namespace WebApplication1
{
    public partial class quanlymuonsach : System.Web.UI.Page
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
                SqlDataAdapter da = new SqlDataAdapter("select * from ChiTietMuon where MaMuon ='" + Session["mamuon"] + "'", kn.con);
                DataTable tb = new DataTable();
                da.Fill(tb);
                if (tb.Rows.Count > 0)
                {
                    txtmamuon.Text = tb.Rows[0]["MaMuon"].ToString();
                    hienthi();
                }
                else
                {
                    kn.con.Open();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SuccessMessage", "window.location='quanlytaikhoan.aspx';", true);
                    string sql = "DELETE from MuonSach where MaMuon = N'" + Session["mamuon"] + "'";
                    SqlCommand cmd = new SqlCommand(sql, kn.con);
                    cmd.ExecuteNonQuery();
                    kn.con.Close();
                }
            }
        }
        protected void hienthi()
        {
            kn.con.Open();
            SqlCommand cmd = new SqlCommand("select * from ChiTietMuon where MaMuon = '" + Session["mamuon"] + "'", kn.con);
            SqlDataReader da = cmd.ExecuteReader();
            gvmuonsach.DataSource = da;
            gvmuonsach.DataBind();
            kn.con.Close();
        }
        protected void gvmuonsach_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = gvmuonsach.SelectedRow;
            txtmasach.Enabled = false;
            btn_sua.Visible = true;
            btn_xacnhan.Visible = true;
            Session["mamuon"] = Server.HtmlDecode(row.Cells[2].Text);
            Session["madgtrasach"] = Server.HtmlDecode(row.Cells[6].Text);
            Session["tensachtra"] = Server.HtmlDecode(row.Cells[4].Text);
            Session["tendocgiatrasach"] = Server.HtmlDecode(row.Cells[7].Text);
            string soluong = Server.HtmlDecode(row.Cells[5].Text);
            string masach = Server.HtmlDecode(row.Cells[3].Text);
            string ghichu = Server.HtmlDecode(row.Cells[10].Text);
           
            string ngaymuon = Server.HtmlDecode(row.Cells[12].Text);
            string ngaytra = Server.HtmlDecode(row.Cells[13].Text);
            string trangthai = Server.HtmlDecode(row.Cells[14].Text);
            SqlDataAdapter da = new SqlDataAdapter("select * from ChiTietMuon where MaSach ='" + masach + "'", kn.con);
            DataTable tb = new DataTable();
            da.Fill(tb);
            if (tb.Rows.Count > 0)
            {
       
                txtsoluong.Text = soluong;
                txtmasach.Text = masach;
                txtghichu.Text = ghichu;
                txtmanv.Text = Session["MNV"].ToString();
                txttgianmuon.Text = ngaymuon;
                txttgiantra.Text = ngaytra;
                txttrangthai.Text = trangthai;
            }
            hienthi();
        }
        protected void btn_sua_Click(object sender, EventArgs e)
        {
            kn.con.Open();

            btn_sua.Visible = false;
            btn_xacnhan.Visible = false;
            string soluong = txtsoluong.Text;
            string masach = txtmasach.Text;
            string ghichu = txtghichu.Text;
            string manv = txtmanv.Text;
            string ngaymuon = txttgianmuon.Text;
            string ngaytra = txttgiantra.Text;
            string trangthai = txttrangthai.Text;

            SqlDataAdapter adapcheckmanv = new SqlDataAdapter("select * from NhanVien where MaNV ='" + manv + "'", kn.con);
            DataTable tbcheckmanv = new DataTable();
            adapcheckmanv.Fill(tbcheckmanv);
            if (tbcheckmanv.Rows.Count > 0)
            {

                SqlCommand cmd = new SqlCommand("UPDATE ChiTietMuon SET SoLuong = @soluong,GhiChu = @ghichu, MaNV = @manv, NgayMuon = @ngaymuon, NgayTra = @ngaytra, trangthai = @trangthai WHERE MaSach = @masach", kn.con);

                cmd.Parameters.AddWithValue("@soluong", soluong);
                cmd.Parameters.AddWithValue("@ghichu", ghichu);
                cmd.Parameters.AddWithValue("@manv", manv);
                cmd.Parameters.AddWithValue("@ngaymuon", ngaymuon);
                cmd.Parameters.AddWithValue("@ngaytra", ngaytra);
                cmd.Parameters.AddWithValue("@trangthai", trangthai);
                cmd.Parameters.AddWithValue("@masach", masach);

                cmd.ExecuteNonQuery();
                kn.con.Close();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SuccessMessage", "alert('Sửa thành công'); window.location='quanlymuonsach.aspx';", true);
                txtsoluong.Text = "";
                txtmasach.Text = "";
                txtghichu.Text = "";
                txtmanv.Text = "";
                txttgianmuon.Text = "";
                txttgiantra.Text = "";
                txttrangthai.Text = "";
            }
            else
            {
                lberrormanv.Text = "Mã nhân viên không tồn tại!";
            }

        }
        protected void btn_phieumuon_Click(object sender, EventArgs e)
        {
            Session["mamuon-phieumuon"] = txtmamuon.Text;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "SuccessMessage", "window.location='phieumuon.aspx';", true);

        }

        protected void btn_xacnhan_Click(object sender, EventArgs e)
        {
            kn.con.Open();
            btn_sua.Visible = false;
            btn_xacnhan.Visible = false;

            string soluong = txtsoluong.Text;
            string masach = txtmasach.Text;
            string ghichu = txtghichu.Text;
            string manv = txtmanv.Text;
            string ngaymuon = txttgianmuon.Text;
        
            string trangthai = txttrangthai.Text;

            SqlDataAdapter adapcheckmanv = new SqlDataAdapter("select * from NhanVien where MaNV ='" + manv + "'", kn.con);
            DataTable tbcheckmanv = new DataTable();
            adapcheckmanv.Fill(tbcheckmanv);
            if (tbcheckmanv.Rows.Count > 0)
            {
                if (trangthai == "1")
                {
                    SqlDataAdapter da = new SqlDataAdapter("select * from Sach where MaSach ='" + masach + "'", kn.con);
                    DataTable tb = new DataTable();
                    da.Fill(tb);
                    if (tb.Rows.Count > 0)
                    {
                        string slmuon = soluong;
                        string slbd = tb.Rows[0]["SoLuong"].ToString();
                        int slbdValue, slmuonValue;
                        if (int.TryParse(slbd, out slbdValue) && int.TryParse(slmuon, out slmuonValue))
                        {
                            int slnew = slbdValue + slmuonValue;
                            SqlCommand cmd = new SqlCommand("UPDATE Sach SET SoLuong = @soluong WHERE MaSach = @masach", kn.con);
                            cmd.Parameters.AddWithValue("@soluong", slnew);
                            cmd.Parameters.AddWithValue("@masach", masach);
                            cmd.ExecuteNonQuery();

                            string sqlttst = "insert into SachTra (MaDocGia, MaSach, TenSach, TenDocGia, NgayMuon, NgayTra) values(@madgtrasach, @masach, @tensach, @tendocgia, @ngaymuon, GETDATE())";
                            SqlCommand cmdttst = new SqlCommand(sqlttst, kn.con);
                            cmdttst.Parameters.AddWithValue("@madgtrasach", Session["madgtrasach"]);
                            cmdttst.Parameters.AddWithValue("@masach", masach);
                            cmdttst.Parameters.AddWithValue("@tensach", Session["tensachtra"]);
                            cmdttst.Parameters.AddWithValue("@tendocgia", Session["tendocgiatrasach"]);
                            cmdttst.Parameters.AddWithValue("@ngaymuon", ngaymuon);
                           

                            cmdttst.ExecuteNonQuery();

                            string sql = "DELETE from ChiTietMuon where MaSach = N'" + masach + "' and MaMuon= '"+ Session["mamuon"]+"'";
                            SqlCommand cmdxoa = new SqlCommand(sql, kn.con);
                            cmdxoa.ExecuteNonQuery();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "SuccessMessage", "alert('Trả sách thành công!');window.location='quanlymuonsach.aspx';", true);

                            kn.con.Close();
                            txtmasach.Text = "";
                            txtghichu.Text = "";
                            txtmanv.Text = "";
                            txttgianmuon.Text = "";
                            txttgiantra.Text = "";
                            txttrangthai.Text = "";
                        }
                    }

                 }
                 else
                 {
                        SqlCommand cmd = new SqlCommand("UPDATE ChiTietMuon SET SoLuong = @soluong, GhiChu = @ghichu, MaNV = @manv, NgayMuon = @ngaymuon, NgayTra = GETDATE(), trangthai = @trangthai WHERE MaSach = @masach", kn.con);

                        cmd.Parameters.AddWithValue("@soluong", soluong);
                        cmd.Parameters.AddWithValue("@ghichu", ghichu);
                        cmd.Parameters.AddWithValue("@manv", manv);
                        cmd.Parameters.AddWithValue("@ngaymuon", ngaymuon);
                   
                        cmd.Parameters.AddWithValue("@trangthai", trangthai);
                        cmd.Parameters.AddWithValue("@masach", masach);

                        cmd.ExecuteNonQuery();



                        kn.con.Close();
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SuccessMessage", "alert('Xác nhận thành công'); window.location='quanlymuonsach.aspx';", true);
                        txtsoluong.Text = "";
                        txtmasach.Text = "";
                        txtghichu.Text = "";
                        txtmanv.Text = "";
                        txttgianmuon.Text = "";
                        txttgiantra.Text = "";
                        txttrangthai.Text = "";
                    } 
            }
            else
            {
                lberrormanv.Text = "Mã nhân viên không tồn tại!";
            }
            
        }

    }
}
