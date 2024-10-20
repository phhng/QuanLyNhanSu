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
    public partial class frmChamCongVaTinhLuong : Form
    {
        public frmChamCongVaTinhLuong()
        {

            InitializeComponent();
          
        }
        private DataTable dtChamCong;
        private void InitializeDataGridView()
        {
            dgvChamCong.Rows.Add("NV001", "Nguyễn Văn A", "Phòng IT", DateTime.Today, false);
            dgvChamCong.Rows.Add("NV002", "Trần Thị B", "Phòng Nhân Sự", DateTime.Today, false);
            SetupCheckBoxColumn();

            dgvChamCong.DataSource = dtChamCong;

        }
        private void SetupCheckBoxColumn()
        {
            if (dgvChamCong.Columns["làm việc"] != null)
            {
                dgvChamCong.Columns.Remove("làm việc");
            }

            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn
            {
                Name = "làm việc",
                HeaderText = "làm việc",
                DataPropertyName = "làm việc",
                TrueValue = true,
                FalseValue = false
            };

            dgvChamCong.Columns.Add(checkBoxColumn);
        }

        private void btnTinhLuong_Click(object sender, EventArgs e)
        {
            frmTinhLuong frm = new frmTinhLuong();
            frm.Show();

        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            string phongBan = comboBox1.Text;
            string hoTen = textBox1.Text;
            DateTime ngayChamCong = dtpChamCong.Value;

            bool isDuplicate = dtChamCong.AsEnumerable().Any(row => row.Field<string>("Họ Tên") == hoTen);

            if (!isDuplicate)
            {
                dtChamCong.Rows.Add("NV003", hoTen, phongBan, ngayChamCong, false);
            }
            else
            {
                MessageBox.Show("Nhân viên này đã tồn tại trong danh sách chấm công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnNghiHet_Click(object sender, EventArgs e)
        {
            foreach (DataRow row in dtChamCong.Rows)
            {
                row["Làm việc"] = false;
            }
        }

        private void btnDiLamHet_Click(object sender, EventArgs e)
        {
            foreach (DataRow row in dtChamCong.Rows)
            {
                row["Làm việc"] = true;
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Dữ liệu đã được lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void frmChamCongVaTinhLuong_Load(object sender, EventArgs e)
        {
            InitializeDataGridView(); 
        }
    }
}
