using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyNhanSu
{
    public partial class frmHoSoNV : Form
    {
        private DateTime originalNgaySinh;
        private string originalGioiTinh;
        private string originalAvatarPath;
        private string avatarFilePath = string.Empty;
        public frmHoSoNV()
        {
            InitializeComponent();
        }

        private void chkShowPass_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShowPass.Checked)
            {
                txtPass.PasswordChar = (char)0;
            }
            else
            {
                txtPass.PasswordChar = '*';
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void suaThongTin()
        {
            //Hiện các button
            btnSuaThongTin.Visible = false;
            btnLuu.Visible = true;
            btnHuy.Visible = true;
            btnDong.Visible = true;
            btnAvaUpload.Visible = true;
            btnAvaXoa.Visible = true;
            //Sửa thông tin
            txtID.Enabled = false;
            txtHoTen.Enabled = false;
            txtChucVu.Enabled = false;
            txtPhongBan.Enabled = false;
            txtEmail.Enabled = false;
            txtPass.Enabled = false;
            dateNgaySinh.Enabled = true;
            btnNam.Enabled = true;
            btnNu.Enabled = true;
        }
        private void xemThongTin()
        {
            //Ẩn các button
            btnSuaThongTin.Visible = true;
            btnLuu.Visible = false;
            btnHuy.Visible = false;
            btnDong.Visible = true;
            btnAvaUpload.Visible = false;
            btnAvaXoa.Visible = false;
            //Chỉ xem thông tin
            txtID.Enabled = false;
            txtHoTen.Enabled = false;
            txtChucVu.Enabled = false;
            txtPhongBan.Enabled = false;
            txtEmail.Enabled = false;
            txtPass.Enabled = false;
            dateNgaySinh.Enabled = false;
            btnNam.Enabled = false;
            btnNu.Enabled = false;
        }

        private void btnSuaThongTin_Click(object sender, EventArgs e)
        {
            suaThongTin();
        }

        private void frmHoSoNV_Load(object sender, EventArgs e)
        {
            xemThongTin();
            originalNgaySinh = dateNgaySinh.Value;
            originalGioiTinh = btnNam.Checked ? "Nam" : "Nữ";
            originalAvatarPath = avatarFilePath;
        }
        private void ShowAvatar(string imagePath)
        {
            if (string.IsNullOrEmpty(imagePath))
            {
                picAvatar.Image = null;
            }
            else
            {
                try
                {
                    picAvatar.Image = Image.FromFile(imagePath);
                    picAvatar.Refresh();
                }
                catch (Exception)
                {
                    MessageBox.Show($"Không thể hiển thị ảnh tại: {imagePath}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    picAvatar.Image = null;
                }
            }
        }

        private void btnAvaUpload_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files (*.jpg; *.jpeg; *.png)|*.jpg; *.jpeg; *.png";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    avatarFilePath = openFileDialog.FileName;
                    try
                    {
                        picAvatar.Image = Image.FromFile(avatarFilePath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Không thể tải hình ảnh: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnAvaXoa_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa ảnh đại diện không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                picAvatar.Image = null;
                avatarFilePath = string.Empty;  // Cập nhật đường dẫn ảnh đại diện thành chuỗi rỗng
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            // Cập nhật thông tin ngày sinh và giới tính
            DateTime ngaySinhMoi = dateNgaySinh.Value;
            string gioiTinhMoi = btnNam.Checked ? "Nam" : "Nữ";

            // Thông báo cho người dùng biết thông tin đã được lưu
            MessageBox.Show("Thông tin đã được lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Lưu lại giá trị mới thành thông tin gốc
            originalNgaySinh = ngaySinhMoi;
            originalGioiTinh = gioiTinhMoi;
            originalAvatarPath = avatarFilePath;  // Lưu ảnh đại diện mới thành ảnh gốc

            // Trở về chế độ xem thông tin
            xemThongTin();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            // Khôi phục lại giá trị ban đầu của ngày sinh, giới tính và ảnh đại diện
            dateNgaySinh.Value = originalNgaySinh;
            if (originalGioiTinh == "Nam")
            {
                btnNam.Checked = true;
            }
            else
            {
                btnNu.Checked = true;
            }

            // Khôi phục lại ảnh đại diện
            if (!string.IsNullOrEmpty(originalAvatarPath))
            {
                try
                {
                    picAvatar.Image = Image.FromFile(originalAvatarPath);
                }
                catch (Exception)
                {
                    picAvatar.Image = null;
                }
            }
            else
            {
                picAvatar.Image = null;
            }
            avatarFilePath = originalAvatarPath;  // Khôi phục đường dẫn ảnh đại diện ban đầu

            // Trở về chế độ xem thông tin
            xemThongTin();
        }

        private void linkChangePass_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "frmDoiMatKhau")
                {
                    f.Activate();
                    f.BringToFront();
                    f.Show();
                    return;
                }
            }
            frmDoiMatKhau frmDoiMatKhau = new frmDoiMatKhau();
            frmDoiMatKhau.BringToFront();
            frmDoiMatKhau.Show();
        }

        private void frmHoSoNV_FormClosing(object sender, FormClosingEventArgs e)
        {
            var result = MessageBox.Show("Bạn có muốn thoát chương trình hay không", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                e.Cancel = true;
                return;
            }
        }
    }
}
