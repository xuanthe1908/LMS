using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
namespace WebApplication1
{
    public partial class admin : System.Web.UI.Page
    {
        ketnoi kn = new ketnoi();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["MNV"]== null)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SuccessMessage", "window.location='loginadmin.aspx';", true);
            }
            else
            {
                txtmnv.Text = Session["MNV"].ToString();
                SqlDataAdapter da = new SqlDataAdapter("Select * from  NhanVien where MaNV='" + txtmnv.Text + "'", kn.con);
                DataTable tb = new DataTable();
                da.Fill(tb);
                if (tb.Rows.Count > 0)
                {
                    txtnameadmin.Text = tb.Rows[0]["HoTen"].ToString();
                    txtsdt.Text = tb.Rows[0]["Sodienthoai"].ToString();
                    txtdate.Text = tb.Rows[0]["NgaySinh"].ToString();
                    txtmnv.Text = tb.Rows[0]["MaNV"].ToString();
                }
                
            }
            
        }
        //protected void hienthi()
        //{
        //    Dictionary<string, int> dataPointsDict = new Dictionary<string, int>();

        //    // Kết nối với cơ sở dữ liệu và thực hiện truy vấn
        //    SqlCommand cmd = new SqlCommand("SELECT SoLuong, TenSach FROM ChiTietMuon", kn.con);

        //    kn.con.Open();
        //    SqlDataReader reader = cmd.ExecuteReader();
        //    while (reader.Read())
        //    {
        //        string label = reader["TenSach"].ToString();
        //        int value = Convert.ToInt32(reader["SoLuong"]);

        //        if (dataPointsDict.ContainsKey(label))
        //        {
        //            dataPointsDict[label] += value; // Nếu nhãn đã tồn tại, cộng giá trị vào tổng
        //        }
        //        else
        //        {
        //            dataPointsDict[label] = value; // Nếu nhãn chưa tồn tại, thêm nhãn với giá trị mới
        //        }
        //    }
        //    kn.con.Close();

        //    // Tạo các danh sách từ Dictionary để tạo chuỗi cho biểu đồ
        //    List<int> dataPoints = new List<int>(dataPointsDict.Values);
        //    List<string> labels = new List<string>(dataPointsDict.Keys);

        //    string dataPointsArray = string.Join(",", dataPoints);
        //    string labelsArray = string.Join("','", labels);

        //    // Script để vẽ biểu đồ Area Chart
        //    string script = @"
        //            <script>
        //                var ctx = document.getElementById('myBarChart').getContext('2d');
        //                var myAreaChart = new Chart(ctx, {
        //                    type: 'bar',
        //                    data: {
        //                        labels: ['" + labelsArray + @"'],
        //                        datasets: [{
        //                            label: 'Số Lượng',
        //                            data: [" + dataPointsArray + @"],
        //                            backgroundColor: 'rgba(78, 115, 223, 0.5)',
        //                            borderColor: 'rgba(78, 115, 223, 1)',
        //                            borderWidth: 1,
        //                        }]
        //                    },
        //                    options: {
        //                        scales: {
        //                            yAxes: [{
        //                                ticks: {
        //                                    beginAtZero: true,
        //                                    stepSize: 1
        //                    }
        //                }]
        //            }
        //            // Các tùy chọn khác cho biểu đồ
        //        }
        //    });
        //            </script>";

        //    // Thêm script vào trang
        //    ClientScript.RegisterStartupScript(this.GetType(), "MyChart", script);
        //}
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
        protected void btnsuaadmin_Click(object sender, EventArgs e)
        {
            
            kn.con.Open();
            string manv = txtmnv.Text;
            string tennv = TextBox1.Text;
            string sdt = TextBox3.Text;
            string date = TextBox4.Text;
            if (Page.IsValid && FileUpload1.HasFile && CheckFileType(FileUpload1.FileName))
            {
                string filename = "images/" + DateTime.Now.ToString("ddMMyyyy_hhmmss_tt_") + FileUpload1.FileName;
                string filepath = MapPath(filename);
                FileUpload1.SaveAs(filepath);
                string sql = "UPDATE NhanVien SET Hinhdaidien='" + filename + "', HoTen = N'" + tennv + "', NgaySinh = N'" + date + "', Sodienthoai = N'" + sdt + "'WHERE MaNV = '" + manv + "'";
                SqlCommand cmd = new SqlCommand(sql, kn.con);
                cmd.ExecuteNonQuery();
                kn.con.Close();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SuccessMessage", "alert('Sửa thành công, bạn cần đăng nhập lại để có thể cập nhật'); window.location='logoutadmin.aspx';", true);
            }
            else
            {
                string sql = "UPDATE NhanVien SET HoTen = N'" + tennv + "', NgaySinh = N'" + date + "', Sodienthoai = N'" + sdt + "'WHERE MaNV = '" + manv + "'";
                SqlCommand cmd = new SqlCommand(sql, kn.con);
                cmd.ExecuteNonQuery();
                kn.con.Close();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SuccessMessage", "alert('Sửa thành công, bạn cần đăng nhập lại để có thể cập nhật'); window.location='logoutadmin.aspx';", true);
            }
            
        }
    }
}