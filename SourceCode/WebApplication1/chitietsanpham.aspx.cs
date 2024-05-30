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
    public partial class chitietsanpham : System.Web.UI.Page
    {
        ketnoi kn = new ketnoi();
        protected void Page_Load(object sender, EventArgs e)
        {
            hienthi();
        }
        protected void hienthi()
        {
            kn.con.Open();
            string productId = Request.QueryString["Id"];
            if (!string.IsNullOrEmpty(productId))
            {
                SqlCommand cmd = new SqlCommand("select * from Sach where MaSach ='" + productId + "'", kn.con);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                if (dataTable.Rows.Count > 0)
                {
                    string sl = dataTable.Rows[0]["SoLuong"].ToString();
                    string tensach = dataTable.Rows[0]["TenSach"].ToString();
                    string img = dataTable.Rows[0]["Hinhanh"].ToString();
                    string chitiet = dataTable.Rows[0]["ChiTiet"].ToString();
                    string masach = dataTable.Rows[0]["MaSach"].ToString();
                    Session["tensach"] = tensach;
                    Session["imgsach"] = img;
                    Session["chitiet"] = chitiet;
                    Session["sl"] = sl;
                }

            }
            kn.con.Close();
        }
        protected void btn_muon_Click(object sender, EventArgs e)
        {
            kn.con.Open();
            string productId = Request.QueryString["Id"];
            string sl = soluong.Text;


                string slmax = Session["sl"].ToString();
                int slValue, slmaxValue, sltronggioValue;
                if (int.TryParse(sl, out slValue) && int.TryParse(slmax, out slmaxValue))
                {
                    if(slValue <= 0)
                    {
                    lberrorsoluong.Text = "Số lượng mượn phải dương";
                    }else
                    if (slValue <= slmaxValue)
                    {
                    SqlCommand cmdcheckgh = new SqlCommand("select * from giohang where MaDG ='" + Session["madg"] + "'", kn.con);
                    SqlDataAdapter adaptercheckgh = new SqlDataAdapter(cmdcheckgh);
                    DataTable dataTable = new DataTable();
                    adaptercheckgh.Fill(dataTable);
                    if (dataTable.Rows.Count > 0)
                    {
                        bool productFound = false;
                        // Lặp qua từng sản phẩm trong giỏ hàng
                        for (int i = 0; i < dataTable.Rows.Count; i++)
                        {
                            string masachcheck = dataTable.Rows[i]["Masach"].ToString();
                            if (productId.ToString() == masachcheck)
                            {
                                productFound = true;
                                // Nếu sản phẩm đã có trong giỏ hàng, cập nhật số lượng
                                if (int.TryParse(dataTable.Rows[i]["soluong"].ToString(), out sltronggioValue))
                                {
                                    int slsaucheck = sltronggioValue + slValue;
                                    string sqlsaucheck = "UPDATE giohang SET SoLuong ='" + slsaucheck + "' WHERE Masach = '" + productId + "' AND MaDG = '" + Session["madg"] + "'";
                                    SqlCommand cmdsaucheck = new SqlCommand(sqlsaucheck, kn.con);
                                    cmdsaucheck.ExecuteNonQuery();
                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SuccessMessage", "alert('Thêm vào giỏ hàng thành công'); window.location='khosach.aspx';", true);
                                }
                                else
                                {
                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ErrorMessage", "alert('Lỗi khi cập nhật số lượng'); window.location='khosach.aspx';", true);
                                }
                                // Thoát khỏi vòng lặp sau khi cập nhật số lượng
                                break;
                            }
                        }

                        // Nếu sản phẩm không được tìm thấy trong giỏ hàng, thêm sản phẩm mới
                        if (!productFound)
                        {
                            string sql = "INSERT INTO Giohang (Masach, tensach, soluong, Hinhanh,  MaDG) VALUES (@productId, @tensach, @sl, @imgsach, @madg)";
                            SqlCommand cmd = new SqlCommand(sql, kn.con);
                            cmd.Parameters.AddWithValue("@productId", productId);
                            cmd.Parameters.AddWithValue("@tensach", Session["tensach"]);
                            cmd.Parameters.AddWithValue("@sl", sl);
                            cmd.Parameters.AddWithValue("@imgsach", Session["imgsach"]);
                            cmd.Parameters.AddWithValue("@madg", Session["madg"]);
                            cmd.ExecuteNonQuery();
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SuccessMessage", "alert('Thêm vào giỏ hàng thành công'); window.location='khosach.aspx';", true);

                        }
                    }
                    else
                    {
                        string sql = "INSERT INTO Giohang (Masach, tensach, soluong, Hinhanh, MaDG) VALUES (@productId, @tensach, @sl, @imgsach,  @madg)";
                        SqlCommand cmd = new SqlCommand(sql, kn.con);
                        cmd.Parameters.AddWithValue("@productId", productId);
                        cmd.Parameters.AddWithValue("@tensach", Session["tensach"]);
                        cmd.Parameters.AddWithValue("@sl", sl);
                        cmd.Parameters.AddWithValue("@imgsach", Session["imgsach"]);
                        cmd.Parameters.AddWithValue("@madg", Session["madg"]);
                        cmd.ExecuteNonQuery();
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SuccessMessage", "alert('Thêm vào giỏ hàng thành công'); window.location='khosach.aspx';", true);

                    }
                    int slmaxnew = slmaxValue - slValue;
                        string sqlslsaumuon = "UPDATE Sach SET SoLuong ='" + slmaxnew + "' WHERE MaSach = '" + productId + "'";
                        SqlCommand cmdslsaumuon = new SqlCommand(sqlslsaumuon, kn.con);
                        cmdslsaumuon.ExecuteNonQuery();


                        soluong.Text = "";
                        kn.con.Close();
                    }
                    else if (slValue > slmaxValue)
                        {
                            lberrorsoluong.Text = "Số lượng mượn quá lớn";
                        }

                }
            
        }
    }
}