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
    public partial class quanlytrasach : System.Web.UI.Page
    {
        ketnoi kn = new ketnoi();
        protected void Page_Load(object sender, EventArgs e)
        {
            hienthi();
        }
        protected void hienthi()
        {
            kn.con.Open();
            SqlCommand cmd = new SqlCommand("select * from SachTra where MaDocGia = '" + Session["madgxemsachtra"] + "'", kn.con);
            SqlDataReader da = cmd.ExecuteReader();
            gvmuonsach.DataSource = da;
            gvmuonsach.DataBind();
            kn.con.Close();
        }
        protected void gvmuonsach_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = gvmuonsach.SelectedRow;
            txtmasach.Enabled = false;
            btn_xoa.Visible = true;
            string masach = Server.HtmlDecode(row.Cells[1].Text);     
            SqlDataAdapter da = new SqlDataAdapter("select * from SachTra where MaSach ='" + masach + "'", kn.con);
            DataTable tb = new DataTable();
            da.Fill(tb);
            if (tb.Rows.Count > 0)
            {
                txtmasach.Text = masach;
            }
            hienthi();
        }
        protected void btn_xoa_Click(object sender, EventArgs e)
        {
            kn.con.Open();

            btn_xoa.Visible = false;
            string masach = txtmasach.Text;
            string sql = "DELETE from SachTra where MaSach = N'" + masach + "'";
            SqlCommand cmd = new SqlCommand(sql, kn.con);
            cmd.ExecuteNonQuery();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "SuccessMessage", "alert('Xoá thành công!');window.location='quanlytrasach.aspx';", true);

            kn.con.Close();
            txtmasach.Text = "";
        }
    }
}