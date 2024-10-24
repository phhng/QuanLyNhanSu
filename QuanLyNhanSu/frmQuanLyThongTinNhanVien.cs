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
            /*
            btnThem.Enabled = b;
            btnSua.Enabled = b;
            btnLuu.Enabled = !b;
            btnXoa.Enabled = b;
            btnHuy.Enabled = !b;
            */
            btnThem.Enabled = b;
            btnSua.Enabled = b && dgvNhanVien.SelectedRows.Count > 0;
            btnLuu.Enabled = !b;
            btnXoa.Enabled = b && dgvNhanVien.SelectedRows.Count > 0;
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
            GenericRepository<PHONGBAN> phongBanRepository = new GenericRepository<PHONGBAN>();
            var phongBanList = phongBanRepository.GetAll();

            cmbPhongBan.DataSource = phongBanList;
            cmbPhongBan.DisplayMember = "TENPB";
            cmbPhongBan.ValueMember = "MAPB";
        }
        private void HienThiChucVu()
        {
            GenericRepository<CHUCVU> chucVuRepository = new GenericRepository<CHUCVU>();
            var chucVuList = chucVuRepository.GetAll();

            cmbChucVu.DataSource = chucVuList;
            cmbChucVu.DisplayMember = "TENCV";  
            cmbChucVu.ValueMember = "MACV";
        }
        private void HienThiBangCap()
        {
            GenericRepository<BANGCAP> bangCapRepository = new GenericRepository<BANGCAP>();
            var bangCapList = bangCapRepository.GetAll();

            cmbBangCap.DataSource = bangCapList;
            cmbBangCap.DisplayMember = "TRINHDO";  
            cmbBangCap.ValueMember = "MABC";
        }
        private void HienThiNhanVien()
        {
            var employeeList = employeeRepository.GetAll();
            dgvNhanVien.DataSource = employeeList;
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

        private void btnSua_Click(object sender, EventArgs ex)
        {
            //if (dgvNhanVien.SelectedRows.Count > 0)
            //{
            //    themmoi = false;
            //    setButton(false);

            //    //// Lấy thông tin từ các trường
            //    //string maNV = dgvNhanVien.CurrentRow.Cells["MANV"].Value.ToString();
            //    //var employee = employeeRepository.FindById(maNV, e => e.MAHS);

            //    //if (employee != null)
            //    //{
            //    //    txtHoTen.Text = employee.HOTEN;
            //    //    txtEmail.Text = employee.EMAIL;
            //    //    txtDiaChi.Text = employee.DIACHI;
            //    //    txtQueQuan.Text = employee.QUEQUAN;
            //    //    txtCccdCmnd.Text = employee.CCCD_CMND;
            //    //    cmbChucVu.SelectedValue = employee.MACV;
            //    //    cmbPhongBan.SelectedValue = employee.MAPB;
            //    //    cmbBangCap.SelectedValue = employee.MABC;
            //    //    dateNgaySinh.Value = employee.NGAYSINH;

            //    //    // Cập nhật giới tính
            //    //    btnNam.Checked = employee.GIOITINH == "Nam";
            //    //    btnNu.Checked = employee.GIOITINH == "Nữ";
            //    //}
            //}
            //else
            //{
            //    MessageBox.Show("Vui lòng chọn một nhân viên để sửa!", "Thông báo");
            //}
        }

        private void btnLuu_Click(object sender, EventArgs ex)
        {
            var employee = new HOSONV
            {
                HOTEN = txtHoTen.Text,
                NGAYSINH = dateNgaySinh.Value,
                GIOITINH = btnNam.Checked ? "Nam" : "Nữ",
                CCCD_CMND = txtCccdCmnd.Text,
                DIACHI = txtDiaChi.Text,
                QUEQUAN = txtQueQuan.Text,
                MABC = Convert.ToInt32(cmbBangCap.SelectedValue),
                // Các trường khác nếu cần
            };

            employeeRepository.InsertUpdate(employee, e => e.MAHS == employee.MAHS);
            HienThiNhanVien();  // Refresh lại danh sách nhân viên
            MessageBox.Show("Lưu thông tin thành công!", "Thông báo");
            setNull();
            setButton(true);
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            setNull();
            setButton(true);
        }

        private void btnXoa_Click(object sender, EventArgs ex)
        {
            if (dgvNhanVien.SelectedRows.Count > 0)
            {
                string maHS = dgvNhanVien.CurrentRow.Cells["MAHS"].Value.ToString();
                employeeRepository.DeleteById(maHS, e => e.MAHS);
                HienThiNhanVien();
                MessageBox.Show("Xóa nhân viên thành công!", "Thông báo");
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một nhân viên để xóa!", "Thông báo");
            }
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
