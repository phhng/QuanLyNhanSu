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
using Word = Microsoft.Office.Interop.Word;
using Excel = Microsoft.Office.Interop.Excel;


namespace QuanLyNhanSu
{
    public partial class frmBaoCaoNhanSu : Form
    {
        public frmBaoCaoNhanSu()
        {
            InitializeComponent();
        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {
           
        }
        
        private void frmBaoCaoNhanSu_Load(object sender, EventArgs e)
        {

            txtSearch.TextChanged += new EventHandler(txtSearch_TextChanged);
            txtSearch.Enabled = true;
            txtSearch.ReadOnly = false;
            LoadData();

            size.Items.AddRange(new object[] {
        2, 4, 6, 8, 12, 14, 16, 18, 20, 22
    });

            this.size.SelectedIndexChanged += new System.EventHandler(this.size_SelectedIndexChanged);

            // Thêm sự kiện cho DataGridView
            dgvBaoCaoNhanSu.CellValueChanged += dgvBaoCaoNhanSu_CellValueChanged;
            dgvBaoCaoNhanSu.CurrentCellDirtyStateChanged += dgvBaoCaoNhanSu_CurrentCellDirtyStateChanged;
        }

        private DataTable originalDataTable;
        private DataTable previousDataTable;
        private void LoadData()
        {
            string connectionString = "Data Source=THAI_HOANG\\MAY05;Initial Catalog=QLNS;Integrated Security=True";
            string query = "SELECT NV.MANV, NV.EMAIL, NV.LOAI_NV, HS.HOTEN, HS.NGAYSINH, HS.GIOITINH, HS.CCCD_CMND " +
                           "FROM NHANVIEN NV " +
                           "JOIN HOSONV HS ON NV.MAHS = HS.MAHS";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();

                try
                {
                    connection.Open();
                    dataAdapter.Fill(dataTable);
                    dgvBaoCaoNhanSu.DataSource = dataTable;
                    originalDataTable = dataTable.Copy();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi lấy dữ liệu: " + ex.Message);
                }
            }
        }

        private void word_Click(object sender, EventArgs e)
        {
            // Tạo một đối tượng Word
            Word.Application wordApp = new Word.Application();
            Word.Document doc = wordApp.Documents.Add();

            // Tạo bảng trong tài liệu Word
            Word.Table table = doc.Tables.Add(doc.Range(0, 0), dgvBaoCaoNhanSu.Rows.Count, dgvBaoCaoNhanSu.Columns.Count);
            table.Borders.Enable = 1;

            // Thêm tiêu đề cột
            for (int i = 0; i < dgvBaoCaoNhanSu.Columns.Count; i++)
            {
                table.Cell(1, i + 1).Range.Text = dgvBaoCaoNhanSu.Columns[i].HeaderText;
                table.Cell(1, i + 1).Range.Bold = 1; // Đặt tiêu đề cột in đậm
            }

            // Thêm dữ liệu từ DataGridView vào bảng
            for (int i = 0; i < dgvBaoCaoNhanSu.Rows.Count; i++)
            {
                for (int j = 0; j < dgvBaoCaoNhanSu.Columns.Count; j++)
                {
                    // Kiểm tra xem dòng có phải là dòng không
                    if (!dgvBaoCaoNhanSu.Rows[i].IsNewRow)
                    {
                        table.Cell(i + 2, j + 1).Range.Text = dgvBaoCaoNhanSu.Rows[i].Cells[j].Value?.ToString();
                    }
                }
            }

            // Hiển thị tài liệu Word
            wordApp.Visible = true;

            // Giải phóng tài nguyên
            System.Runtime.InteropServices.Marshal.ReleaseComObject(doc);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(wordApp);
        }

        private void excel_Click(object sender, EventArgs e)
        {
            // Tạo một ứng dụng Excel
            Excel.Application excelApp = new Excel.Application();
            excelApp.Visible = true; // Hiện Excel lên

            // Tạo một workbook và worksheet mới
            Excel.Workbook workbook = excelApp.Workbooks.Add();
            Excel.Worksheet worksheet = workbook.Worksheets[1];

            // Thêm tiêu đề cột từ DataGridView vào Excel
            for (int i = 0; i < dgvBaoCaoNhanSu.Columns.Count; i++)
            {
                worksheet.Cells[1, i + 1] = dgvBaoCaoNhanSu.Columns[i].HeaderText;
            }

            // Thêm dữ liệu từ DataGridView vào Excel
            for (int i = 0; i < dgvBaoCaoNhanSu.Rows.Count; i++)
            {
                for (int j = 0; j < dgvBaoCaoNhanSu.Columns.Count; j++)
                {
                    // Kiểm tra xem dòng có phải là dòng không
                    if (!dgvBaoCaoNhanSu.Rows[i].IsNewRow)
                    {
                        worksheet.Cells[i + 2, j + 1] = dgvBaoCaoNhanSu.Rows[i].Cells[j].Value?.ToString();
                    }
                }
            }

            // Giải phóng tài nguyên
            System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
        }

        private void size_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Kiểm tra xem có lựa chọn nào không
            if (size.SelectedItem != null)
            {
                // Lấy kích thước font từ ToolStripComboBox
                int fontSize;
                if (int.TryParse(size.SelectedItem.ToString(), out fontSize))
                {
                    // Thay đổi kích thước font cho DataGridView
                    foreach (DataGridViewColumn column in dgvBaoCaoNhanSu.Columns)
                    {
                        column.DefaultCellStyle.Font = new Font(dgvBaoCaoNhanSu.Font.FontFamily, fontSize);
                    }
                    dgvBaoCaoNhanSu.DefaultCellStyle.Font = new Font(dgvBaoCaoNhanSu.Font.FontFamily, fontSize);
                }
            }
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void refresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void save_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=THAI_HOANG\\MAY05;Initial Catalog=QLNS;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                int rowsAffected = 0;

                foreach (DataGridViewRow row in dgvBaoCaoNhanSu.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        string maNV = row.Cells["MANV"].Value?.ToString();
                        string email = row.Cells["EMAIL"].Value?.ToString();
                        string loaiNV = row.Cells["LOAI_NV"].Value?.ToString();
                        string hoTen = row.Cells["HOTEN"].Value?.ToString();

                        if (!string.IsNullOrEmpty(maNV) && !string.IsNullOrEmpty(hoTen))
                        {
                            string query = "IF EXISTS (SELECT 1 FROM NHANVIEN WHERE MANV = @maNV) " +
                                           "UPDATE NHANVIEN SET EMAIL = @Email, LOAI_NV = @LoaiNV WHERE MANV = @maNV " +
                                           "ELSE " +
                                           "INSERT INTO NHANVIEN (MANV, EMAIL, LOAI_NV) VALUES (@maNV, @Email, @LoaiNV)";

                            using (SqlCommand command = new SqlCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@maNV", maNV);
                                command.Parameters.AddWithValue("@Email", email);
                                command.Parameters.AddWithValue("@LoaiNV", loaiNV);

                                rowsAffected += command.ExecuteNonQuery(); // Count rows affected
                            }
                        }
                    }
                }

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Dữ liệu đã được lưu thành công!");
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Không có thay đổi nào để lưu!");
                }
            }
        }

        private void find_Click(object sender, EventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string filter = txtSearch.Text.Trim();

            // Kiểm tra nếu filter không rỗng
            if (!string.IsNullOrEmpty(filter))
            {
                (dgvBaoCaoNhanSu.DataSource as DataTable).DefaultView.RowFilter =
                   string.Format("MANV LIKE '%{0}%' OR HOTEN LIKE '%{0}%'", filter);
            }
            else
            {
                // Nếu không có filter, hiển thị tất cả dữ liệu
                LoadData();
            }
        }

        private void back_Click(object sender, EventArgs e)
        {
            if (originalDataTable != null)
            {
                previousDataTable = dgvBaoCaoNhanSu.DataSource as DataTable; // Save current state before changing
                dgvBaoCaoNhanSu.DataSource = originalDataTable.Copy(); // Revert to original
            }
            else
            {
                MessageBox.Show("Không có thay đổi nào để hoàn tác.");
            }
        }
    

        private void next_Click(object sender, EventArgs e)
        {
            if (previousDataTable != null)
            {
                dgvBaoCaoNhanSu.DataSource = previousDataTable.Copy(); // Restore the previous state
                previousDataTable = null; // Clear previous data after redoing
            }
            else
            {
                MessageBox.Show("Không có thao tác nào để tiến lại.");
            }
        }

        private void dgvBaoCaoNhanSu_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvBaoCaoNhanSu.IsCurrentCellDirty)
            {
                dgvBaoCaoNhanSu.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvBaoCaoNhanSu_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string maNV = dgvBaoCaoNhanSu.Rows[e.RowIndex].Cells["MANV"].Value?.ToString();
                string email = dgvBaoCaoNhanSu.Rows[e.RowIndex].Cells["EMAIL"].Value?.ToString();
                string loaiNV = dgvBaoCaoNhanSu.Rows[e.RowIndex].Cells["LOAI_NV"].Value?.ToString();

                // Gọi hàm lưu dữ liệu
                if (!string.IsNullOrEmpty(maNV))
                {
                    SaveData(maNV, email, loaiNV);
                }
            }
        }
        private void SaveData(string maNV, string email, string loaiNV)
        {
            string connectionString = "Data Source=THAI_HOANG\\MAY05;Initial Catalog=QLNS;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "IF EXISTS (SELECT 1 FROM NHANVIEN WHERE MANV = @maNV) " +
                               "UPDATE NHANVIEN SET EMAIL = @Email, LOAI_NV = @LoaiNV WHERE MANV = @maNV " +
                               "ELSE " +
                               "INSERT INTO NHANVIEN (MANV, EMAIL, LOAI_NV) VALUES (@maNV, @Email, @LoaiNV)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@maNV", maNV);
                    command.Parameters.AddWithValue("@Email", email ?? (object)DBNull.Value); // Chuyển đổi null thành DBNull
                    command.Parameters.AddWithValue("@LoaiNV", loaiNV ?? (object)DBNull.Value); // Chuyển đổi null thành DBNull

                    command.ExecuteNonQuery();
                }
            }
        }
    }
    }


