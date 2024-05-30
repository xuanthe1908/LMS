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
    public partial class giohang : System.Web.UI.Page
    {
        ketnoi kn = new ketnoi();
        protected void Page_Load(object sender, EventArgs e)
        {
            hienthi();
            tongsach();
        }
        protected void hienthi()
        {
            SqlDataAdapter dahien = new SqlDataAdapter("select * from Giohang where MaDG ='" + Session["madg"] + "'", kn.con);
            DataTable tb = new DataTable();
            dahien.Fill(tb);
            if (tb.Rows.Count > 0)
            {
                Session["hiengh"] = true;
                kn.con.Open();
                SqlCommand cmd = new SqlCommand("select * from Giohang where MaDG ='" + Session["madg"] + "'", kn.con);
                SqlDataReader da = cmd.ExecuteReader();
                gvgiohang.DataSource = da;
                gvgiohang.DataBind();
                kn.con.Close();
            }else
            {
                Session["hiengh"] = null;
            }
        }
      

        protected void gvgiohang_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = gvgiohang.SelectedRow;
            btnthemmasachsua.Visible = true;
            btnsuagiohang.Visible = true;
            btnxoagiohang.Visible = true;
            lblslsachsua.Visible = true;
            string masach = Server.HtmlDecode(row.Cells[1].Text);
            string soluong = Server.HtmlDecode(row.Cells[3].Text);
            SqlDataAdapter da = new SqlDataAdapter("select * from giohang where MaSach ='" + masach + "'", kn.con);
            DataTable tb = new DataTable();
            da.Fill(tb);
            if (tb.Rows.Count > 0)
            {
                SqlDataAdapter dasachtrongkho = new SqlDataAdapter("select * from Sach where MaSach ='" + masach + "'", kn.con);
                DataTable tbsachtronkho = new DataTable();
                dasachtrongkho.Fill(tbsachtronkho);
                if (tbsachtronkho.Rows.Count > 0)
                {
                    Session["slsachcu"] = tbsachtronkho.Rows[0]["SoLuong"].ToString();
                }
                Session["sachtronggio"] = soluong;
                txtslsachsua.Text = soluong;
                txtsuaxoagiohang.Text = masach;
            }
            hienthi();
        }
        private void tongsach()
        {

            kn.con.Open();
            SqlCommand cmd = new SqlCommand("select sum(soluong) from Giohang where MaDG='" + Session["madg"] + "'", kn.con);

            lb_tongsach.Text = cmd.ExecuteScalar().ToString();

            cmd.Dispose();

            kn.con.Close();
        }
        protected void btnthemmasachsua_Click(object sender, EventArgs e)
        {
            txtslsachsua.Visible = true;
        }
        protected void btnsuagiohang_Click(object sender, EventArgs e)
        {
            kn.con.Open();
            if (txtsuaxoagiohang.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "SuccessMessage", "alert('Không được để trống');window.location='giohang.aspx';", true);
            }
            else
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from giohang where  MaDG ='" + Session["madg"] + "'", kn.con);
                DataTable tb = new DataTable();
                da.Fill(tb);
                if (tb.Rows.Count > 0)
                {
                    string slmuon = txtslsachsua.Text;
                    string sltrongdb = Session["slsachcu"].ToString();
                    string slbd = Session["sachtronggio"].ToString();
                    int slbdValue, slmuonValue, sltrongdbVaule;
                    if (int.TryParse(slbd, out slbdValue) && int.TryParse(sltrongdb, out sltrongdbVaule) && int.TryParse(slmuon, out slmuonValue))
                    {
                        if(slmuonValue <= 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SuccessMessage", "alert('Số lượng mượn phải dương!'); window.location='giohang.aspx';", true);
                        }else
                        if (slmuonValue <= sltrongdbVaule)
                        {
                            int slhoanlai = slbdValue + sltrongdbVaule;

                            SqlCommand cmdtralai = new SqlCommand("UPDATE Sach SET SoLuong = @soluong WHERE MaSach = @masach", kn.con);
                            cmdtralai.Parameters.AddWithValue("@soluong", slhoanlai);
                            cmdtralai.Parameters.AddWithValue("@masach", txtsuaxoagiohang.Text);
                            cmdtralai.ExecuteNonQuery();
                            SqlCommand cmd = new SqlCommand("UPDATE giohang SET SoLuong = @soluong WHERE Masach = '" + txtsuaxoagiohang.Text + "'", kn.con);
                            cmd.Parameters.AddWithValue("@soluong", txtslsachsua.Text);
                            cmd.ExecuteNonQuery();
                            int slnew = slhoanlai - slmuonValue;
                            SqlCommand cmdsautru = new SqlCommand("UPDATE Sach SET SoLuong = @soluong WHERE MaSach = @masach", kn.con);
                            cmdsautru.Parameters.AddWithValue("@soluong", slnew);
                            cmdsautru.Parameters.AddWithValue("@masach", txtsuaxoagiohang.Text);
                            cmdsautru.ExecuteNonQuery();

                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SuccessMessage", "alert('Sửa thành công'); window.location='giohang.aspx';", true);

                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SuccessMessage", "alert('Số lượng quá lớn!'); window.location='giohang.aspx';", true);
                        }
                    }
                    }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SuccessMessage", "alert('Không tồn tại trong dữ liệu!');window.location='giohang.aspx';", true);
                }
            }
            kn.con.Close();
            txtsuaxoagiohang.Text = "";

        }
        protected void btnxoagiohang_Click(object sender, EventArgs e)
        {
            kn.con.Open();

            if (txtsuaxoagiohang.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "SuccessMessage", "alert('Không được để trống');window.location='giohang.aspx';", true);
            }
            else
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from giohang where  MaDG ='" + Session["madg"] + "'", kn.con);
                DataTable tb = new DataTable();
                da.Fill(tb);
                if (tb.Rows.Count > 0)
                {
                 
                    string sltrongdb = Session["slsachcu"].ToString();
                    string slbd = Session["sachtronggio"].ToString();
                    int slbdValue, sltrongdbVaule;
                    if (int.TryParse(slbd, out slbdValue) && int.TryParse(sltrongdb, out sltrongdbVaule))
                    {
                        int slhoanlai = slbdValue + sltrongdbVaule;

                        SqlCommand cmdtralai = new SqlCommand("UPDATE Sach SET SoLuong = @soluong WHERE MaSach = @masach", kn.con);
                        cmdtralai.Parameters.AddWithValue("@soluong", slhoanlai);
                        cmdtralai.Parameters.AddWithValue("@masach", txtsuaxoagiohang.Text);
                        cmdtralai.ExecuteNonQuery();
                        string sql = "DELETE from giohang where Masach = N'" + txtsuaxoagiohang.Text + "'";
                        SqlCommand cmd = new SqlCommand(sql, kn.con);
                        cmd.ExecuteNonQuery();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "SuccessMessage", "alert('Xoá thành công!');window.location='giohang.aspx';", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SuccessMessage", "alert('Không tồn tại trong dữ liệu!');window.location='giohang.aspx';", true);
                }

            }
            kn.con.Close();
            txtsuaxoagiohang.Text = "";
        }
        protected void btn_xnmuon_Click(object sender, EventArgs e)
        {
            kn.con.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO MuonSach (MaDocGia, TenDocGia, TongSoLuongMuon) VALUES (@MaDocGia, @TenDocGia, @TongSoLuongMuon) SELECT SCOPE_IDENTITY();", kn.con);

            cmd.Parameters.AddWithValue("@MaDocGia", Session["madg"]);
            cmd.Parameters.AddWithValue("@TenDocGia", Session["fullnamedg"]);
            string tongSoLuongMuon = lb_tongsach.Text.Trim();
            cmd.Parameters.AddWithValue("@TongSoLuongMuon", tongSoLuongMuon);

            int mamuon = Convert.ToInt32(cmd.ExecuteScalar());
            Session["mamuon"] = mamuon;
            int manv = 0;
            int trangthai = 0;
            SqlDataAdapter da = new SqlDataAdapter("select * from DocGia where MaDocGia ='" + Session["madg"] + "'", kn.con);
            DataTable tb = new DataTable();
            da.Fill(tb);
            if (tb.Rows.Count > 0)
            {
                SqlCommand addchitietmuon = new SqlCommand("INSERT INTO ChiTietMuon (MaMuon, DiaChi,NgayMuon, MaSach, TenSach, MaDocGia, TenDocGia, SoLuong, hinhsach, Email, manv, trangthai) SELECT @Mamuon AS MaMuon,@DiaChi AS DiaChi,GETDATE(), Masach, Tensach, @Madg AS MaDocGia, @Fullname AS TenDocGia, soluong, Hinhanh, @email AS Email, @manv AS manv,@trangthai AS trangthai  FROM Giohang WHERE MaDG = @Madg", kn.con);

                addchitietmuon.Parameters.AddWithValue("@trangthai", trangthai);
                addchitietmuon.Parameters.AddWithValue("@manv", manv);
                addchitietmuon.Parameters.AddWithValue("@email", tb.Rows[0]["Email"].ToString());
                addchitietmuon.Parameters.AddWithValue("@DiaChi", tb.Rows[0]["DiaChi"].ToString());
                addchitietmuon.Parameters.AddWithValue("@Mamuon", mamuon);
                addchitietmuon.Parameters.AddWithValue("@Madg", Session["madg"]);
                addchitietmuon.Parameters.AddWithValue("@Fullname", Session["fullnamedg"].ToString());

                addchitietmuon.ExecuteNonQuery();
            }


            SqlCommand xoagiohang = new SqlCommand("delete from Giohang where MaDG='" + Session["madg"]+ "'", kn.con);
            xoagiohang.ExecuteNonQuery();
            kn.con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SuccessMessage", "alert('Mượn thành công'); window.location='khosach.aspx';", true);

        }
    }
}