using QuanLyNhanSu.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyNhanSu
{
    public partial class frmTrangThaiNhanVien : Form
    {
        GenericRepository<TRANGTHAINV> repository = new GenericRepository<TRANGTHAINV>();
        public frmTrangThaiNhanVien()
        {
            InitializeComponent();
        }

        private void frmTrangThaiNhanVien_Load(object sender, EventArgs e)
        {
            // Call method to load statuses into the combobox
            LoadForm();

        }

        private void LoadTrangThai()
        {
            // Create a generic repository for TRANGTHAINV
            var repository = new GenericRepository<TRANGTHAINV>();

            // Get all statuses from the TRANGTHAINV table
            List<string> trangthaiList = repository.GetSelected(s => s.TRANGTHAI);

            // Add a default "Select" option at the top of the combobox
            trangthaiList.Insert(0, "");

            // Bind the list of trangthai to the combobox
            cboTrangThai.DataSource = trangthaiList;
        }

        private void LoadChucVu()
        {
            var repository = new GenericRepository<CHUCVU>();
            List<string> trangthaiList = repository.GetSelected(c => c.TENCV);
            trangthaiList.Insert(0, "");
            cboChucVu.DataSource = trangthaiList;
        }

        private void LoadPhongBan()
        {
            var repository = new GenericRepository<PHONGBAN>();
            List<string> trangthaiList = repository.GetSelected(p => p.TENPB);
            trangthaiList.Insert(0, "");
            cboPhongBan.DataSource = trangthaiList;
        }
        private void LoadForm()
        {
            LoadChucVu();
            LoadPhongBan();
            LoadTrangThai();
            BindDataGridView();
        }

        private void BindDataGridView()
        {
            try
            {
                //var repository = new GenericRepository<TRANGTHAINV>();

                // Clear the current data in DataGridView before binding new data
                dgvNhanVien.Rows.Clear();
                // Retrieve search criteria from form inputs
                string searchIDOrName = txtIDorName.Text.ToUpper();  // For ID or Name
                string chucVu = cboChucVu.SelectedItem?.ToString();
                string phongBan = cboPhongBan.SelectedItem?.ToString();
                string trangThai = cboTrangThai.SelectedItem?.ToString();

                // Check if all filters are empty
                bool allFiltersEmpty = string.IsNullOrEmpty(searchIDOrName) &&
                                       string.IsNullOrEmpty(chucVu) &&
                                       string.IsNullOrEmpty(phongBan) &&
                                       string.IsNullOrEmpty(trangThai);
                // If all filters are empty, retrieve all data
                var result = repository.GetAll(nv =>
                    allFiltersEmpty || // If all filters are empty, retrieve all data
                    (string.IsNullOrEmpty(searchIDOrName) || nv.MANV.Contains(searchIDOrName) || nv.HOSONV.HOTEN.ToUpper().Contains(searchIDOrName)) &&
                    (string.IsNullOrEmpty(chucVu) || nv.NHANVIEN.CHUCVU?.TENCV == chucVu) &&
                    (string.IsNullOrEmpty(phongBan) || nv.NHANVIEN.PHONGBAN?.TENPB == phongBan) &&
                    (string.IsNullOrEmpty(trangThai) || nv.TRANGTHAI == trangThai)
                );
                if (result.Count == 0)
                {
                    MessageBox.Show("Không tồn tại nhân viên", "Thông báo", MessageBoxButtons.OK);
                }
                else
                {
                    foreach (var item in result)
                    {
                        int index = dgvNhanVien.Rows.Add();
                        dgvNhanVien.Rows[index].Cells[0].Value = item.MANV;
                        dgvNhanVien.Rows[index].Cells[1].Value = item.HOSONV.HOTEN;

                        // Check if CHUCVU is null
                        dgvNhanVien.Rows[index].Cells[2].Value = item.NHANVIEN.CHUCVU?.TENCV ?? "N/A";

                        // Check if PHONGBAN is null
                        dgvNhanVien.Rows[index].Cells[3].Value = item.NHANVIEN.PHONGBAN?.TENPB ?? "N/A";

                        // Check if TRANGTHAI is null
                        dgvNhanVien.Rows[index].Cells[4].Value = item.TRANGTHAI ?? "N/A";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    private void btnTim_Click(object sender, EventArgs e)
        {
            if (txtIDorName.Text == "" && cboChucVu.SelectedIndex == 0 && cboPhongBan.SelectedIndex == 0
                && cboTrangThai.SelectedIndex == 0)
            {
                MessageBox.Show("Không được để trống!", "Thông báo", MessageBoxButtons.OK);      
            }
            else
            {
                BindDataGridView();
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            SetField(true);
            txtIDorName.Text = string.Empty;
            cboChucVu.SelectedIndex = 0;
            cboPhongBan.SelectedIndex = 0;
            cboTrangThai.SelectedIndex = 0;
            BindDataGridView();
        }

        private void SetField(bool check)
        {
            txtIDorName.Enabled = check;
            cboChucVu.Enabled = check;
            cboPhongBan.Enabled = check;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                // Disable fields for editing
                SetField(false);

                // Retrieve the ID of the employee to be updated
                var id = txtIDorName.Text.Trim();
                if (string.IsNullOrEmpty(id))
                {
                    MessageBox.Show("Vui lòng chọn nhân viên để sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Find the employee by ID
                var nhanvien = repository.FindByEmployeeID(id);
                if (nhanvien == null)
                {
                    MessageBox.Show("Nhân viên không được tìm thấy", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Confirm the action with the user
                var confirm = MessageBox.Show("Xác nhận sửa trạng thái?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                {
                    // Update the status of the employee based on the selected value from the combo box
                    nhanvien.TRANGTHAI = cboTrangThai.SelectedItem?.ToString();  // Make sure to use SelectedItem

                    // Call repository update method to save the changes
                    repository.Update(nhanvien);  // Assuming this method exists and handles saving the entity

                    // Refresh the form after updating
                    txtIDorName.Text = string.Empty;
                    cboChucVu.SelectedIndex = 0;
                    cboPhongBan.SelectedIndex = 0;
                    cboTrangThai.SelectedIndex = 0;
                    BindDataGridView();
                    MessageBox.Show("Cập nhật trạng thái thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // If the user cancels the update, just enable the fields back for further use
                    SetField(true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Ensure fields are re-enabled at the end of the process
                SetField(true);
            }
        }


        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ensure the row and column indexes are valid
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dgvNhanVien.Rows.Count > e.RowIndex)
            {
                // Safely retrieve the value from the selected row's first cell (Mã NV)
                var cellValue = dgvNhanVien.Rows[e.RowIndex].Cells[0].Value;
                if (cellValue != null)
                {
                    string nvID = cellValue.ToString().Trim();

                    // Use the FindById method to find the employee by ID
                    // Example: Find an entity where MANV is the ID
                    var tt = new GenericRepository<TRANGTHAINV>();
                    var nv = tt.FindByEmployeeID(nvID);
                    //var nv = tt.FindById(nvID, nnv => nnv.MANV);

                    // Check if the employee was found and populate the form
                    if (nv != null)
                    {
                        txtIDorName.Text = nv.MANV?.Trim() ?? ""; // Ensure no null values for text boxes
                        cboChucVu.Text = nv.NHANVIEN.CHUCVU?.TENCV ?? "N/A";  // Handle potential null values
                        cboPhongBan.Text = nv.NHANVIEN.PHONGBAN?.TENPB ?? "N/A";
                        cboTrangThai.Text = nv.TRANGTHAI ?? "N/A";
                    }
                    else
                    {
                        // Show a message if the employee is not found
                        MessageBox.Show("Nhân viên không được tìm thấy", "Thông báo", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    // Handle the case where the selected cell is null (e.g., invalid row)
                    MessageBox.Show("Không thể lấy Mã NV từ ô đã chọn", "Thông báo", MessageBoxButtons.OK);
                }
            }
        }
    }
}