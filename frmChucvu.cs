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
    public partial class frmChucvu : Form
    {
        string connectionString = "Data Source=LAPTOP-2IJKHN51;Initial Catalog=QLNS;Integrated Security=True";
        private DataTable dtChucVu;
        public frmChucvu()
        {
            InitializeComponent();
            LoadChucVu();
        }
        private void LoadChucVu()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT MACV, TENCV FROM CHUCVU";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    dtChucVu = new DataTable();
                    da.Fill(dtChucVu);

                    dgvChucVu.DataSource = dtChucVu;
                    dgvChucVu.AllowUserToAddRows = true; // Cho phép thêm hàng
                    dgvChucVu.ReadOnly = false;         // Cho phép chỉnh sửa hàng
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
                }
            }
        }

       

        private void dgvChucVu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvChucVu.Rows[e.RowIndex];

                // Hiển thị thông tin trong TextBox
                txtMaChucVu.Text = row.Cells["MACV"].Value.ToString();
                txtTenChucVu.Text = row.Cells["TENCV"].Value.ToString();
            }
        }
        private void dgvChucVu_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dgvChucVu.BeginEdit(true);  // Bắt đầu chỉnh sửa khi nhấp đúp
            }
        }

        private void tslThem_Click(object sender, EventArgs e)
        {
            DataRow newRow = dtChucVu.NewRow();  // Tạo hàng mới
            newRow["MaCV"] = txtMaChucVu.Text;
            newRow["TenCV"] = txtTenChucVu.Text;
            dtChucVu.Rows.Add(newRow);  // Thêm hàng vào DataTable
        }

        private void tslSua_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    if (!string.IsNullOrEmpty(txtMaChucVu.Text) && !string.IsNullOrEmpty(txtTenChucVu.Text))
                    {
                        string query = "UPDATE CHUCVU SET TENCV = @TENCV WHERE MACV = @MACV";
                        SqlCommand cmd = new SqlCommand(query, connection);
                        cmd.Parameters.AddWithValue("@TENCV", txtTenChucVu.Text);
                        cmd.Parameters.AddWithValue("@MACV", txtMaChucVu.Text);

                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("Cập nhật chức vụ thành công.");
                            LoadChucVu();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Mã và tên chức vụ không được để trống.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi sửa chức vụ: " + ex.Message);
                }
            }
        }

        private void tslXoa_Click(object sender, EventArgs e)
        {
            if (dgvChucVu.SelectedRows.Count > 0)  // Kiểm tra có hàng nào được chọn không
            {
                foreach (DataGridViewRow row in dgvChucVu.SelectedRows)
                {
                    if (!row.IsNewRow)  // Kiểm tra nếu không phải hàng mới
                    {
                        dgvChucVu.Rows.Remove(row);  // Xóa hàng khỏi DataGridView
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn chức vụ để xóa.");
            }
        }

        private void tslLuu_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlDataAdapter da = new SqlDataAdapter("SELECT MACV, TENCV FROM CHUCVU", connection);
                    SqlCommandBuilder cb = new SqlCommandBuilder(da);  // Tự động tạo lệnh Insert, Update, Delete

                    // Cập nhật cơ sở dữ liệu từ DataTable (dtChucVu)
                    da.Update(dtChucVu);

                    MessageBox.Show("Đã Lưu Thành Công");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi lưu dữ liệu: " + ex.Message);
                }
            }
        }

        private void tslPhongBan_Click(object sender, EventArgs e)
        {
            frmPhongBan frmPB = new frmPhongBan();

            frmPB.Show();
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
