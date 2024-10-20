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
    public partial class frmQuanLyThongTinNhanVien : Form
    {
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
    }
}
