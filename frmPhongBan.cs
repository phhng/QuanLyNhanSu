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
    public partial class frmPhongBan : Form
    {

        string connectionString = "Data Source=LAPTOP-2IJKHN51;Initial Catalog=QLNS;Integrated Security=True";
        private DataTable dtPhongBan;
        public frmPhongBan()
        {
            InitializeComponent();
            LoadPhongBan();
        }
        private void LoadPhongBan()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT MaPB, TenPB FROM PHONGBAN";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    dtPhongBan = new DataTable();
                    da.Fill(dtPhongBan);

                    // Gán DataTable vào DataGridView
                    dgvPhongBan.DataSource = dtPhongBan;

                    // Cho phép người dùng thêm và sửa trực tiếp trên DataGridView
                    dgvPhongBan.AllowUserToAddRows = true;  // Cho phép thêm hàng
                    dgvPhongBan.ReadOnly = false;           // Cho phép chỉnh sửa
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
                }
            }
        }



        private void dgvPhongBan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Đảm bảo người dùng chọn một hàng hợp lệ
            {
                DataGridViewRow row = dgvPhongBan.Rows[e.RowIndex];

                // Hiển thị thông tin phòng ban trong các textbox
                txtMaPhongBan.Text = row.Cells["MaPB"].Value.ToString();
                txtTenPhongBan.Text = row.Cells["TenPB"].Value.ToString();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void tslThem_Click(object sender, EventArgs e)
        {
            DataRow newRow = dtPhongBan.NewRow();
            newRow["MaPB"] = txtMaPhongBan.Text;
            newRow["TenPB"] = txtTenPhongBan.Text;
            dtPhongBan.Rows.Add(newRow);
        }

        private void tslSua_Click(object sender, EventArgs e)
        {
            if (dgvPhongBan.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvPhongBan.SelectedRows[0];
                selectedRow.Cells["MaPB"].Value = txtMaPhongBan.Text;
                selectedRow.Cells["TenPB"].Value = txtTenPhongBan.Text;
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một phòng ban để sửa.");
            }
        }

        private void tslXoa_Click(object sender, EventArgs e)
        {
            if (dgvPhongBan.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvPhongBan.SelectedRows)
                {
                    if (!row.IsNewRow) // Kiểm tra không phải hàng mới
                    {
                        dgvPhongBan.Rows.Remove(row);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn phòng ban để xóa.");
            }
        }

        private void tslLuu_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SELECT MaPB, TenPB FROM PHONGBAN", connection);
                    SqlCommandBuilder cb = new SqlCommandBuilder(da); // Tự động tạo lệnh Insert, Update, Delete

                    // Lưu các thay đổi từ DataTable về database
                    da.Update(dtPhongBan);
                    MessageBox.Show("Lưu thành công tất cả thay đổi.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi lưu dữ liệu: " + ex.Message);
                }
            }
        }

        private void tslThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tslChucVu_Click(object sender, EventArgs e)
        {
            frmChucvu frmCV = new frmChucvu();

            frmCV.Show();
        }
    }
}
