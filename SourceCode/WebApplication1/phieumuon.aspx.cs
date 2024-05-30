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
    public partial class phieumuon : System.Web.UI.Page
    {
        ketnoi kn = new ketnoi();
        protected void Page_Load(object sender, EventArgs e)
        {
            hienthi();
        }
        protected void hienthi()
        {
            kn.con.Open();
            SqlCommand cmd = new SqlCommand("select * from ChiTietMuon where MaMuon = '" + Session["mamuon"] + "'", kn.con);
            SqlDataReader da = cmd.ExecuteReader();
            gvphieumuon.DataSource = da;
            gvphieumuon.DataBind();
            kn.con.Close();
        }
        protected void gvphieumuon_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}