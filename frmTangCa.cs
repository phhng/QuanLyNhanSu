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
    public partial class frmTangCa : Form
    {
        string connectionString = "Data Source=LAPTOP-2IJKHN51;Initial Catalog=QLNS1;Integrated Security=True";
        public frmTangCa()
        {
            InitializeComponent();
        }

        private void dgvTangCa_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
