using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyNhanSu
{
    public partial class frmTinhLuong : Form
    {
        private string connectionString = "Data Source=THAI_HOANG\\MAY05;Initial Catalog=QLNS1;Integrated Security=True";
        private List<NhanVienCong> tongCongList;
        private int selectedMonth;

        public frmTinhLuong(List<NhanVienCong> tongCongList, int month)
        {
            InitializeComponent();
            this.tongCongList = tongCongList;
            this.selectedMonth = month;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string hoTen = txtHoTen.Text.Trim();
            string chucVu = cmbChucVu.SelectedItem?.ToString();
            string phongBan = cmbPhongBan.SelectedItem?.ToString();
            int thang = cmbThang.SelectedIndex + 1; // Lấy tháng từ combo box

            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {
                sqlConn.Open();

                string sql = @"
            SELECT hs.HOTEN, nv.MANV, cv.TENCV, pb.TENPB 
            FROM NHANVIEN nv 
            JOIN HOSONV hs ON nv.MAHS = hs.MAHS 
            JOIN CHUCVU cv ON nv.MACV = cv.MACV 
            JOIN PHONGBAN pb ON nv.MAPB = pb.MAPB 
            WHERE 1=1";

                if (!string.IsNullOrEmpty(hoTen))
                {
                    sql += " AND hs.HOTEN LIKE @HoTen";
                }
                if (!string.IsNullOrEmpty(chucVu))
                {
                    sql += " AND cv.TENCV = @ChucVu";
                }
                if (!string.IsNullOrEmpty(phongBan))
                {
                    sql += " AND pb.TENPB = @PhongBan";
                }

                sql += " AND EXISTS (SELECT 1 FROM CHAMCONG cc WHERE cc.MANV = nv.MANV AND cc.THANG = @Thang)"; // Lọc theo tháng

                using (SqlCommand cmd = new SqlCommand(sql, sqlConn))
                {
                    if (!string.IsNullOrEmpty(hoTen))
                    {
                        cmd.Parameters.AddWithValue("@HoTen", $"%{hoTen}%");
                    }
                    if (!string.IsNullOrEmpty(chucVu))
                    {
                        cmd.Parameters.AddWithValue("@ChucVu", chucVu);
                    }
                    if (!string.IsNullOrEmpty(phongBan))
                    {
                        cmd.Parameters.AddWithValue("@PhongBan", phongBan);
                    }
                    cmd.Parameters.AddWithValue("@Thang", thang); // Thêm tham số tháng

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        // Thêm cột "Tổng Công"
                        dt.Columns.Add("Tổng Công", typeof(int));

                        // Cập nhật giá trị "Tổng Công" từ danh sách
                        foreach (DataRow row in dt.Rows)
                        {
                            string hoTenFound = row["HOTEN"].ToString();
                            var nhanVienCong = tongCongList.FirstOrDefault(nv => nv.HoTen == hoTenFound);
                            row["Tổng Công"] = nhanVienCong?.TongCong ?? 0;
                        }

                        dgvTinhLuong.DataSource = dt;
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy nhân viên.");
                        dgvTinhLuong.DataSource = null;
                    }
                }
            }
        }
        private void LoadData()
        {
            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {
                sqlConn.Open();

                // Tạo DataTable để lưu kết quả
                DataTable dt = new DataTable();

                // Thực hiện truy vấn lấy thông tin nhân viên theo tháng
                string sql = "sp_LayChamCong @Thang = @thang";
                //string sql = "sp_LayChamCongNhanVien @Manv = @manv";

                using (SqlCommand cmd = new SqlCommand(sql, sqlConn))
                {
                    // Thay thế bằng biến toàn cục để lấy tháng
                    cmd.Parameters.AddWithValue("@thang", selectedMonth); // Biến selectedMonth là biến toàn cục

                    // Điền dữ liệu vào DataTable
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
                dt.Columns.Add("Tổng Công", typeof(int));
                // Cập nhật giá trị "Tổng Công" từ danh sách
                foreach (DataRow row in dt.Rows)
                {
                    string hoTen = row["HOTEN"].ToString();
                    var nhanVienCong = tongCongList.FirstOrDefault(nv => nv.HoTen == hoTen);
                    row["Tổng Công"] = nhanVienCong?.TongCong ?? 0; // Gán giá trị tổng công
                }

                // Gán DataTable cho DataGridView
                dgvTinhLuong.DataSource = dt;
            }
        }
        private void frmTinhLuong_Load(object sender, EventArgs e)
        {
            LoadData();
            
            cmbThang.SelectedIndex = selectedMonth - 1;
        }

        private void cmbThang_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dgvTinhLuong_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
