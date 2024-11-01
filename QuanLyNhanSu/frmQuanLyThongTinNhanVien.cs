using QuanLyNhanSu.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyNhanSu
{
    public partial class frmQuanLyThongTinNhanVien : Form
    {
        GenericRepository<HOSONV> employeeRepository = new GenericRepository<HOSONV>();
        public bool themmoi = false;
        public frmQuanLyThongTinNhanVien()
        {
            InitializeComponent();
            dateNgaySinh.Format = DateTimePickerFormat.Custom;
            dateNgaySinh.CustomFormat = "dd/MM/yyyy";
        }
        private void setButton(bool b)
        {
            btnThem.Enabled = b;
            btnSua.Enabled = b;
            btnLuu.Enabled = !b;
            btnXoa.Enabled = b;
            btnHuy.Enabled = !b;
        }
        void setNull()
        {
            txtHoTen.Text = "";
            txtEmail.Text = "";
            txtDiaChi.Text = "";
            txtQueQuan.Text = "";
            txtCccdCmnd.Text = "";
            cmbChucVu.SelectedIndex = -1;
            cmbPhongBan.SelectedIndex = -1;
            cmbBangCap.SelectedIndex = -1;
        }

        private void HienThiPhongBan()
        {
            
        }
        private void HienThiChucVu()
        {
            
        }
        private void HienThiBangCap()
        {
            
        }
        private void HienThiNhanVien()
        {
            
        }
        private void clearBox()
        {
            txtHoTen.Clear();
            txtDiaChi.Clear();
            txtQueQuan.Clear();
            txtCccdCmnd.Clear();
            txtEmail.Clear();
            btnNam.Checked = true;
        }



        private void frmQuanLyThongTinNhanVien_Load(object sender, EventArgs e)
        {

            setNull();
            setButton(true);
            HienThiPhongBan();
            HienThiChucVu();
            HienThiBangCap();
            HienThiNhanVien();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            themmoi = true;
            //setNull(); 
            setButton(false);

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvNhanVien.SelectedRows.Count > 0)
            {
                themmoi = false;
                setButton(false);
                // Đưa dữ liệu từ bảng DataGridView vào các trường nhập liệu
                txtHoTen.Text = dgvNhanVien.CurrentRow.Cells["HOTEN"].Value.ToString();
                txtEmail.Text = dgvNhanVien.CurrentRow.Cells["EMAIL"].Value.ToString();
                txtDiaChi.Text = dgvNhanVien.CurrentRow.Cells["DIACHI"].Value.ToString();
                txtQueQuan.Text = dgvNhanVien.CurrentRow.Cells["QUEQUAN"].Value.ToString();
                txtCccdCmnd.Text = dgvNhanVien.CurrentRow.Cells["CCCD_CMND"].Value.ToString();

                // Kiểm tra và cập nhật các combo box
                cmbChucVu.SelectedValue = dgvNhanVien.CurrentRow.Cells["MACV"].Value ?? DBNull.Value;
                cmbPhongBan.SelectedValue = dgvNhanVien.CurrentRow.Cells["MAPB"].Value ?? DBNull.Value;
                cmbBangCap.SelectedValue = dgvNhanVien.CurrentRow.Cells["MABC"].Value ?? DBNull.Value;

                dateNgaySinh.Value = Convert.ToDateTime(dgvNhanVien.CurrentRow.Cells["NGAYSINH"].Value);
                btnNam.Checked = dgvNhanVien.CurrentRow.Cells["GIOITINH"].Value.ToString() == "Nam";
                btnNu.Checked = dgvNhanVien.CurrentRow.Cells["GIOITINH"].Value.ToString() == "Nữ";
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một nhân viên để sửa!", "Thông báo");
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            setNull();
            setButton(true);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            
        }

        private void frmQuanLyThongTinNhanVien_FormClosing(object sender, FormClosingEventArgs e)
        {
            var result = MessageBox.Show("Bạn có muốn thoát chương trình hay không", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                e.Cancel = true;
                return;
            }
        }

        private void dgvNhanVien_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvNhanVien.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvNhanVien.SelectedRows[0];

                // Hiển thị dữ liệu từ hàng đó lên các ô nhập liệu
                txtHoTen.Text = row.Cells["HOTEN"].Value.ToString();
                txtEmail.Text = row.Cells["EMAIL"].Value.ToString();
                txtDiaChi.Text = row.Cells["DIACHI"].Value.ToString();
                txtQueQuan.Text = row.Cells["QUEQUAN"].Value.ToString();
                txtCccdCmnd.Text = row.Cells["CCCD_CMND"].Value.ToString();

                // Hiển thị giá trị tên chức vụ (TENCV), tên phòng ban (TENPB), và trình độ (TRINHDO)
                cmbChucVu.Text = row.Cells["TENCV"].Value.ToString();  // Gán tên chức vụ
                cmbPhongBan.Text = row.Cells["TENPB"].Value.ToString(); // Gán tên phòng ban
                cmbBangCap.Text = row.Cells["TRINHDO"].Value.ToString(); // Gán trình độ

                dateNgaySinh.Value = Convert.ToDateTime(row.Cells["NGAYSINH"].Value);

                // Kiểm tra giới tính và cập nhật radio button
                if (row.Cells["GIOITINH"].Value.ToString() == "Nam")
                {
                    btnNam.Checked = true;
                }
                else
                {
                    btnNu.Checked = true;
                }
            }
        }
    }
}
