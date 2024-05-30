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
    public partial class quanlysachdg : System.Web.UI.Page
    {
        ketnoi kn = new ketnoi();
        protected void Page_Load(object sender, EventArgs e)
        {
            hienthi();
        }
        protected void hienthi()
        {
            kn.con.Open();
            SqlCommand cmd = new SqlCommand("select * from MuonSach where MaDocGia = '" + Session["madgxemsach"] + "'", kn.con);
            SqlDataReader da = cmd.ExecuteReader();
            gvtaikhoan.DataSource = da;
            gvtaikhoan.DataBind();
            kn.con.Close();
        }
    
        protected void gvmuonsach_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = gvtaikhoan.SelectedRow;
            btn_xemchitiet.Visible = true;
            
            string mamuon = Server.HtmlDecode(row.Cells[1].Text);
            string madg = Server.HtmlDecode(row.Cells[2].Text);
            string tendg = Server.HtmlDecode(row.Cells[3].Text);
            string tongsl = Server.HtmlDecode(row.Cells[4].Text);
            string ngaymuon = Server.HtmlDecode(row.Cells[5].Text);
            SqlDataAdapter da = new SqlDataAdapter("select * from MuonSach where MaMuon ='" + mamuon + "'", kn.con);
            DataTable tb = new DataTable();
            da.Fill(tb);
            if (tb.Rows.Count > 0)
            {
                txtmamuon.Text = mamuon;
                txtmadg.Text = madg;
                txttendg.Text = tendg;
                txttgianmuon.Text = ngaymuon;
                txttongsl.Text = tongsl;
            }
            hienthi();
        }
       
        protected void btn_xemchitiet_Click(object sender, EventArgs e)
        {
            Session["mamuon"] = txtmamuon.Text;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "SuccessMessage", "window.location='quanlymuonsach.aspx';", true);

        }
    }
}