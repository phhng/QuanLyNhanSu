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
        public frmHoSoNV()
        {
            InitializeComponent();
            dateNgaySinh.Format = DateTimePickerFormat.Custom;
            dateNgaySinh.CustomFormat = "dd/MM/yyyy";
        }
        private void setButton(bool a)
        {
            btnAvaUpload.Visible = !a;
            btnAvaXoa.Visible = !a;
            btnHuy.Enabled = !a;
            btnLuu.Enabled = !a;
            btnSuaThongTin.Enabled = a;
        }
    }
}
