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
    public partial class thongke : System.Web.UI.Page
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
                sachdamuon();
                sachconlai();
                theloai();
                docgia();
                bieudotron();
            }
        }
        protected void sachdamuon()
        {
            kn.con.Open();
            SqlCommand cmd = new SqlCommand("select sum(Soluong) from ChiTietMuon ", kn.con);

            lbldamuon.Text = cmd.ExecuteScalar().ToString();

            cmd.Dispose();

            kn.con.Close();
        }
        protected void sachconlai()
        {
            kn.con.Open();
            SqlCommand cmd = new SqlCommand("SELECT SUM(CONVERT(int, LTRIM(RTRIM(SoLuong)))) FROM Sach", kn.con);

            lblconlai.Text = cmd.ExecuteScalar().ToString();

            cmd.Dispose();

            kn.con.Close();
        }
        protected void theloai()
        {
            kn.con.Open();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM TheLoai", kn.con);
            int count = Convert.ToInt32(cmd.ExecuteScalar());

            lbltheloai.Text = count.ToString();

            cmd.Dispose();

            kn.con.Close();
        }
        protected void docgia()
        {
            kn.con.Open();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM DocGia", kn.con);
            int count = Convert.ToInt32(cmd.ExecuteScalar());

            lbldocgia.Text = count.ToString();

            cmd.Dispose();

            kn.con.Close();
        }

        protected void bieudotron()
        {
            Dictionary<string, int> dataPointsDict = new Dictionary<string, int>();

            // Truy vấn để lấy tổng số lượng từ hai bảng và so sánh
            SqlCommand cmd1 = new SqlCommand("SELECT SUM(CONVERT(int, LTRIM(RTRIM(SoLuong)))) AS Total FROM ChiTietMuon", kn.con);
            SqlCommand cmd2 = new SqlCommand("SELECT SUM(CONVERT(int, LTRIM(RTRIM(SoLuong)))) AS Total FROM Sach", kn.con);

            kn.con.Open();

            int total1 = 0;
            int total2 = 0;

            // Thực hiện truy vấn và lấy tổng số lượng từ mỗi bảng
            SqlDataReader reader1 = cmd1.ExecuteReader();
            if (reader1.Read())
            {
                if (!reader1.IsDBNull(reader1.GetOrdinal("Total")))
                {
                    total1 = Convert.ToInt32(reader1["Total"]);
                }
                else
                {
                    total1 = 0; // Nếu giá trị là DBNull, gán total1 = 0
                }
            }
               
            
            reader1.Close();

            SqlDataReader reader2 = cmd2.ExecuteReader();
            if (reader2.Read())
            {
                total2 = Convert.ToInt32(reader2["Total"]);
            }
            reader2.Close();

            kn.con.Close();

            // Thêm dữ liệu vào Dictionary để vẽ biểu đồ
            dataPointsDict["Sách đã mượn"] = total1;
            dataPointsDict["Sách còn lại"] = total2;

            // Tạo chuỗi JSON từ Dictionary để sử dụng trong biểu đồ
            string chartData = "['Category', 'Total']";
            foreach (var item in dataPointsDict)
            {
                chartData += $", ['{item.Key}', {item.Value}]";
            }

            // Script để vẽ biểu đồ Pie Chart
            string script = $@"
        <script type='text/javascript'>
            google.charts.load('current', {{ 'packages': ['corechart'] }});
            google.charts.setOnLoadCallback(drawChart);

            function drawChart() {{
                var data = google.visualization.arrayToDataTable([
                    {chartData}
                ]);

                var options = {{
                    title: 'So sánh tổng số lượng giữa sách đã mượn và sách còn lại'
                }};

                var chart = new google.visualization.PieChart(document.getElementById('chart_div'));
                chart.draw(data, options);
            }}
        </script>";

            // Thêm script vào trang
            ClientScript.RegisterStartupScript(this.GetType(), "MyPieChart", script);
        }



        protected void hienthi()
        {
            Dictionary<string, int> dataPointsDict = new Dictionary<string, int>();

            // Kết nối với cơ sở dữ liệu và thực hiện truy vấn
            SqlCommand cmd = new SqlCommand("SELECT SoLuong, TenSach FROM ChiTietMuon", kn.con);

            kn.con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string label = reader["TenSach"].ToString();
                int value = Convert.ToInt32(reader["SoLuong"]);

                if (dataPointsDict.ContainsKey(label))
                {
                    dataPointsDict[label] += value; // Nếu nhãn đã tồn tại, cộng giá trị vào tổng
                }
                else
                {
                    dataPointsDict[label] = value; // Nếu nhãn chưa tồn tại, thêm nhãn với giá trị mới
                }
            }
            kn.con.Close();

            // Tạo các danh sách từ Dictionary để tạo chuỗi cho biểu đồ
            List<int> dataPoints = new List<int>(dataPointsDict.Values);
            List<string> labels = new List<string>(dataPointsDict.Keys);

            string dataPointsArray = string.Join(",", dataPoints);
            string labelsArray = string.Join("','", labels);

            // Script để vẽ biểu đồ Area Chart
            string script = @"
                    <script>
                        var ctx = document.getElementById('myBarChart').getContext('2d');
                        var myAreaChart = new Chart(ctx, {
                            type: 'bar',
                            data: {
                                labels: ['" + labelsArray + @"'],
                                datasets: [{
                                    label: 'Số Lượng',
                                    data: [" + dataPointsArray + @"],
                                    backgroundColor: 'rgba(78, 115, 223, 0.5)',
                                    borderColor: 'rgba(78, 115, 223, 1)',
                                    borderWidth: 1,
                                }]
                            },
                            options: {
                                scales: {
                                    yAxes: [{
                                        ticks: {
                                            beginAtZero: true,
                                            stepSize: 1
                            }
                        }]
                    }
                    // Các tùy chọn khác cho biểu đồ
                }
            });
                    </script>";

            // Thêm script vào trang
            ClientScript.RegisterStartupScript(this.GetType(), "MyChart", script);
        }
    }
}