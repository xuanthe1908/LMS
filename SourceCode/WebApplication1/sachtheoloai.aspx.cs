using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
namespace WebApplication1
{
    public partial class sachtheoloai : System.Web.UI.Page
    {
        ketnoi kn = new ketnoi();
        protected void Page_Load(object sender, EventArgs e)
        {
            hienthi();
        }
        protected void hienthi()
        {
            kn.con.Open();
            string Idloai = Request.QueryString["ml"];
            List<string> tentheloaiList = new List<string>();
            List<string> matheloaiList = new List<string>();
            SqlCommand cmdtheloai = new SqlCommand("select * from TheLoai", kn.con);
            SqlDataAdapter adaptertheloai = new SqlDataAdapter(cmdtheloai);
            DataTable tbtheloai = new DataTable();
            adaptertheloai.Fill(tbtheloai);
            foreach (DataRow row in tbtheloai.Rows)
            {
                string matheloai = Convert.ToString(row["MaTheLoai"]);
                string theloai = Convert.ToString(row["TenTheLoai"]);
                tentheloaiList.Add(theloai);
                matheloaiList.Add(matheloai);
            }
            Session["tentheloaiList"] = tentheloaiList;
            Session["matheloaiList"] = matheloaiList;

            SqlCommand cmdsach = new SqlCommand("select * from Sach where MaTheLoai ='" + Idloai +"'", kn.con);
            SqlDataAdapter adaptersach = new SqlDataAdapter(cmdsach);
            DataTable dataTable = new DataTable();
            adaptersach.Fill(dataTable);

            List<string> maSachList = new List<string>();
            List<string> tenSachList = new List<string>();
            List<string> hinhanhList = new List<string>();
            foreach (DataRow row in dataTable.Rows)
            {
                string masach = Convert.ToString(row["MaSach"]);
                string tensach = Convert.ToString(row["TenSach"]);
                string hinhanh = Convert.ToString(row["Hinhanh"]);

                hinhanhList.Add(hinhanh);
                maSachList.Add(masach);
                tenSachList.Add(tensach);
            }
            Session["MaSachList"] = maSachList;
            Session["TenSachList"] = tenSachList;
            Session["hinhanhList"] = hinhanhList;
            kn.con.Close();
        }

    }
}