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
    public partial class quanlytaikhoan : System.Web.UI.Page
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
            List<string> madgList = new List<string>();
            List<string> tendgList = new List<string>();
            SqlCommand cmddg = new SqlCommand("select * from MuonSach", kn.con);
            SqlDataAdapter adapterdg = new SqlDataAdapter(cmddg);
            DataTable tbdg = new DataTable();
            adapterdg.Fill(tbdg);
            foreach (DataRow row in tbdg.Rows)
            {
                string madg = Convert.ToString(row["MaDocGia"]);
                string tendg = Convert.ToString(row["TenDocGia"]);
                tendgList.Add(tendg);
                madgList.Add(madg);
            }
            Session["madgList"] = madgList;
            Session["tendgList"] = tendgList;

            SqlCommand cmd = new SqlCommand("select * from DocGia ", kn.con);
            SqlDataReader da = cmd.ExecuteReader();
            gvtaikhoan.DataSource = da;
            gvtaikhoan.DataBind();
            kn.con.Close();
        }

        protected void gvtaikhoan_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = gvtaikhoan.SelectedRow;
            txtusername.Enabled = false;
            btn_xemsachtra.Visible = true;
            btn_xemsachmuon.Visible = true;
            btn_sua.Visible = true;
            btn_xoa.Visible = true;
            string madocgia = HttpUtility.HtmlDecode(row.Cells[1].Text);
            string username = HttpUtility.HtmlDecode(row.Cells[2].Text);
            string fullname = HttpUtility.HtmlDecode(row.Cells[4].Text);
            string diachi = HttpUtility.HtmlDecode(row.Cells[5].Text);
            string email = HttpUtility.HtmlDecode(row.Cells[6].Text);
            txtdiachi.Text = diachi;
            txtmadocgia.Text = madocgia;
            txtusername.Text = username;
            txtfullname.Text = fullname;
            txtemail.Text = email;
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

        protected void btn_sua_Click(object sender, EventArgs e)
        {
            kn.con.Open();
            txtusername.Enabled = true;
            btn_sua.Visible = false;
            btn_xoa.Visible = false;
            string diachi = txtdiachi.Text;
            string username = txtusername.Text;
            string fullname = txtfullname.Text;
            string email = txtemail.Text;
            if (Page.IsValid && uphinhsach.HasFile && CheckFileType(uphinhsach.FileName))
            {
                string filename = "images/" + DateTime.Now.ToString("ddMMyyyy_hhmmss_tt_") + uphinhsach.FileName;
                string filepath = MapPath(filename);
                uphinhsach.SaveAs(filepath);
                string sql = "UPDATE DocGia SET Email ='" + email + "',DiaChi ='" + diachi + "', HoTen = N'" + fullname + "', Hinhdaidien = N'" + filename + "' WHERE Taikhoan = N'" + username + "'";
                SqlCommand cmd = new SqlCommand(sql, kn.con);
                cmd.ExecuteNonQuery();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SuccessMessage", "alert('Sửa thành công'); window.location='quanlytaikhoan.aspx';", true);
            }
            else
            {
                string sql = "UPDATE DocGia SET Email ='" + email + "',DiaChi ='" + diachi + "', HoTen = N'" + fullname + "' WHERE Taikhoan = N'" + username + "'";
                SqlCommand cmd = new SqlCommand(sql, kn.con);
                cmd.ExecuteNonQuery();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SuccessMessage", "alert('Sửa thành công'); window.location='quanlytaikhoan.aspx';", true);
            }
            txtusername.Text = "";
            txtfullname.Text = "";
            txtemail.Text = "";
            kn.con.Close();
        }
        protected void btn_xoa_Click(object sender, EventArgs e)
        {
            kn.con.Open();
            txtusername.Enabled = true;
            btn_sua.Visible = false;
            btn_xoa.Visible = false;
            string username = txtusername.Text;
            string sql = "DELETE from DocGia where Taikhoan = N'" + username + "'";
            SqlCommand cmd = new SqlCommand(sql, kn.con);
            cmd.ExecuteNonQuery();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "SuccessMessage", "alert('Xoá thành công!');window.location='quanlytaikhoan.aspx';", true);
            txtusername.Text = "";
            txtemail.Text = "";
            txtfullname.Text = "";
            kn.con.Close();
        }
        protected void btn_xensachmuon_Click(object sender, EventArgs e)
        {
            Session["madgxemsach"] = txtmadocgia.Text;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "SuccessMessage", "window.location='quanlysachdg.aspx';", true);

        }
        protected void btn_xemsachtra_Click(object sender, EventArgs e)
        {
            Session["madgxemsachtra"] = txtmadocgia.Text;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "SuccessMessage", "window.location='quanlytrasach.aspx';", true);
        }
    }
}