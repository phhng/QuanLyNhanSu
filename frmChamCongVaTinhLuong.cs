using QuanLyNhanSu.QLNS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyNhanSu
{   
    public partial class frmChamCongVaTinhLuong : Form
    {
        private List<NhanVienCong> tongCongList = new List<NhanVienCong>();
        private DataTable chamCongTable;
       string connectionString = "Data Source=THAI_HOANG\\MAY05;Initial Catalog=QLNS1;Integrated Security=True;";

        public frmChamCongVaTinhLuong()
        {

            InitializeComponent();
            this.tongCongList = tongCongList;
            LoadThang();
            LoadChucVu();
            LoadPhongBan();
            LoadData();
            SetupDataGridView();
        }

        private void frmChamCongVaTinhLuong_Load(object sender, EventArgs e)
        {

            // Có thể để trống hoặc thực hiện thêm các thao tác khi form được tải
        }

        private void LoadThang()
        {
            cmbThang.Items.Clear();
            for (int month = 1; month <= 12; month++)
            {
                cmbThang.Items.Add($"Tháng {month:00}");
            }
            cmbThang.SelectedIndex = 0; // Chọn tháng đầu tiên mặc định
        }

        private void LoadChucVu()
        {
            cmbChucVu.Items.Clear();
            string[] chucVu = { "Giám đốc", "Nhân viên", "Quản lý", "Trưởng Phòng", "Thu Ngân" };
            cmbChucVu.Items.AddRange(chucVu);
            cmbChucVu.SelectedIndex = 0; // Chọn chức vụ đầu tiên mặc định
        }

        private void LoadPhongBan()
        {
            cmbPhongBan.Items.Clear();
            string[] phongBan = { "Phòng Kinh Doanh", "Phòng Tư Vấn", "Phòng Nhân Sự", "Phòng Marketing", "Phòng Kỹ Thuật" };
            cmbPhongBan.Items.AddRange(phongBan);
            cmbPhongBan.SelectedIndex = 0; // Chọn phòng ban đầu tiên mặc định
        }

        private void LoadData()
        {
            chamCongTable = new DataTable();

            // Thêm cột "Họ Tên"
            chamCongTable.Columns.Add("Họ Tên", typeof(string));


            // Thêm cột cho từng ngày (1 đến 31)
            for (int i = 1; i <= 31; i++)
            {
                chamCongTable.Columns.Add($"Ngày {i}", typeof(bool));
            }

            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT H.HOTEN FROM HOSONV H";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, sqlConn))
                {
                    sqlConn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DataRow row = chamCongTable.NewRow();
                            row["Họ Tên"] = reader["HOTEN"].ToString();

                            // Khởi tạo giá trị cho các ngày
                            for (int i = 1; i <= 31; i++)
                            {
                                row[$"Ngày {i}"] = false; // Hoặc giá trị mặc định khác
                            }

                            chamCongTable.Rows.Add(row);
                        }
                    }
                }
            }

            if (chamCongTable.Rows.Count == 0)
            {
                MessageBox.Show("Không tìm thấy dữ liệu nhân viên.");
            }

            // Gọi SetupDataGridView để thiết lập DataGridView
            //SetupDataGridView();
        }
        private void SetupDataGridView()
        {
            dgvChamCong.DataSource = chamCongTable; // Thiết lập DataSource

            // Nếu cần thiết, có thể định dạng cột ở đây
            dgvChamCong.Columns["Họ Tên"].ReadOnly = true;

            // Bạn cũng có thể thêm cột "Tổng Công" ở đây nếu cần
            dgvChamCong.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Tổng Công",
                HeaderText = "Tổng Công",
                ReadOnly = true
            });
        }

        private void dgvChamCong_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex > 0 && e.ColumnIndex < 33)
            {
                // Đảo ngược giá trị checkbox
                var cell = dgvChamCong.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (cell is DataGridViewCheckBoxCell checkBoxCell)
                {
                    checkBoxCell.Value = !(bool)(checkBoxCell.Value ?? false);
                    UpdateTongCong(e.RowIndex);
                    
                }
            }
        }

        private void UpdateTongCong(int rowIndex)
        {
            int totalDays = 0;
            for (int i = 1; i <= 31; i++)
            {
                bool isChecked = (bool)(dgvChamCong.Rows[rowIndex].Cells[$"Ngày {i}"].Value ?? false);
                if (isChecked)
                {
                    totalDays++;
                }
            }
            dgvChamCong.Rows[rowIndex].Cells["Tổng Công"].Value = totalDays;
        }

        private void btnDiLamHet_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvChamCong.Rows)
            {
                for (int i = 1; i <= 31; i++)
                {
                    row.Cells[$"Ngày {i}"].Value = true;
                }
                UpdateTongCong(row.Index);
            }
        }

        private void btnNghiHet_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvChamCong.Rows)
            {
                for (int i = 1; i <= 31; i++)
                {
                    row.Cells[$"Ngày {i}"].Value = false;
                }
                UpdateTongCong(row.Index);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        { // Tạo từ điển để lưu tổng công theo tên nhân viên
            Dictionary<string, int> tongCongDict = new Dictionary<string, int>();

            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {
                sqlConn.Open();

                foreach (DataRow row in chamCongTable.Rows)
                {
                    string hoTen = row["Họ Tên"].ToString();
                    int totalDays = 0;

                    for (int i = 1; i <= 31; i++)
                    {
                        bool isChecked = (bool)(row[$"Ngày {i}"] ?? false);
                        if (isChecked)
                        {
                            totalDays++;
                        }
                    }

                    // Lưu tổng công vào từ điển
                    if (!tongCongDict.ContainsKey(hoTen))
                    {
                        tongCongDict[hoTen] = totalDays;
                    }

                    // Thực hiện lưu vào cơ sở dữ liệu nếu cần
                    // ...
                }
            }

            // Sử dụng tổng công ở đây nếu cần
            foreach (var item in tongCongDict)
            {
                Console.WriteLine($"Tên: {item.Key}, Tổng Công: {item.Value}");
            }

            MessageBox.Show("Lưu chấm công thành công!");
        }
        // Phương thức để lấy giá trị MAPHIEU mới
        private int GetNewMaPhieu(SqlConnection sqlConn)
        {
            string sqlQuery = "SELECT ISNULL(MAX(MAPHIEU), 0) + 1 FROM CHAMCONG";
            using (SqlCommand cmd = new SqlCommand(sqlQuery, sqlConn))
            {
                return (int)cmd.ExecuteScalar();
            }
        }
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string hoTen = txtHoTen.Text.Trim();

            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT H.HOTEN FROM HOSONV H WHERE H.HOTEN LIKE @HoTen";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, sqlConn))
                {
                    cmd.Parameters.AddWithValue("@HoTen", $"%{hoTen}%");
                    sqlConn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        chamCongTable.Clear(); // Xóa dữ liệu cũ
                        while (reader.Read())
                        {
                            DataRow row = chamCongTable.NewRow();
                            row["Họ Tên"] = reader["HOTEN"].ToString();
                            chamCongTable.Rows.Add(row);
                        }
                    }
                }
            }

            SetupDataGridView();
        }

        private void btnTinhLuong_Click(object sender, EventArgs e)
        {
            int selectedMonth = cmbThang.SelectedIndex + 1;
            frmTinhLuong frmTinhLuong = new frmTinhLuong(tongCongList, selectedMonth);

            frmTinhLuong.Show();
           
            
        }

        private void cmbThang_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
public class NhanVienCong
{
    public string HoTen { get; set; }
    public int TongCong { get; set; }
}