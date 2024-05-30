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
    public partial class theloai : System.Web.UI.Page
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
        protected void btn_sua_Click(object sender, EventArgs e)
        {
            kn.con.Open();
            btn_sua.Visible = false;
            btn_them.Visible = true;
            string matheloai = txtmatheloai.Text;
            string tentheloai = txttentheloai.Text;
            string sql = "UPDATE TheLoai SET TenTheLoai = @TenTheLoai WHERE MaTheLoai = @MaTheLoai";
            SqlCommand cmd = new SqlCommand(sql, kn.con);
            cmd.Parameters.AddWithValue("@TenTheLoai", tentheloai);
            cmd.Parameters.AddWithValue("@MaTheLoai", matheloai);
            cmd.ExecuteNonQuery();
            kn.con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SuccessMessage", "alert('Sửa thành công'); window.location='theloai.aspx';", true);

        }
        protected void btn_them_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("SELECT MAX(MaTheLoai) AS MaxMaTheLoai FROM TheLoai", kn.con);
            kn.con.Open();
            object maxMaTheLoai = cmd.ExecuteScalar();
            int newMaTheLoai = 0;

            if (maxMaTheLoai != null && maxMaTheLoai != DBNull.Value)
            {
                newMaTheLoai = Convert.ToInt32(maxMaTheLoai) +1;
                
            }

            string sql = "INSERT INTO TheLoai (MaTheLoai, TenTheLoai) VALUES (@matheloai, @tentheloai)";
            SqlCommand cmd1 = new SqlCommand(sql, kn.con);
            cmd1.Parameters.AddWithValue("@matheloai", newMaTheLoai);
            cmd1.Parameters.AddWithValue("@tentheloai", txttentheloai.Text);
            cmd1.ExecuteNonQuery();
            kn.con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SuccessMessage", "alert('Thêm thành công'); window.location='theloai.aspx';", true);

        }
        protected void hienthi()
        {
            kn.con.Open();
            SqlCommand cmd = new SqlCommand("select * from TheLoai", kn.con);
            SqlDataReader da = cmd.ExecuteReader();
            gvtheloai.DataSource = da;
            gvtheloai.DataBind();
            kn.con.Close();
        }
        protected void gvtheloai_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = gvtheloai.SelectedRow;
            btn_sua.Visible = true;
            Label1.Visible = true;
            txtmatheloai.Visible = true;
            btn_them.Visible = false;


            string matheloai = Server.HtmlDecode(row.Cells[1].Text);
            string tentheloai = Server.HtmlDecode(row.Cells[2].Text);
            SqlDataAdapter da = new SqlDataAdapter("select * from TheLoai where MaTheLoai ='" + matheloai + "'", kn.con);
            DataTable tb = new DataTable();
            da.Fill(tb);
            if (tb.Rows.Count > 0)
            {
                txtmatheloai.Text = matheloai;
                txttentheloai.Text = tentheloai;
            }
            hienthi();
        }
    }
}