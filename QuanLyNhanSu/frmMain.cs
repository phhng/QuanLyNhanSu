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
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }
        private void hideForm()
        {
            foreach (Form f in this.MdiChildren)
            {
                f.Hide();
            }
        }
        private void OpenForm<T>() where T : Form, new()
        {
            // Hide the main form or handle other logic if needed
            hideForm();

            // Check if the form is already opened
            foreach (Form form in this.MdiChildren)
            {
                if (form is T)
                {
                    form.Activate();                      // Activate the form
                    form.BringToFront();                  // Bring it to the front
                    form.WindowState = FormWindowState.Maximized; // Maximize the form
                    form.Show();                          // Show the form
                    return;
                }
            }

            // If the form is not already open, create a new instance
            T newForm = new T
            {
                MdiParent = this,                         // Set the MDI parent
                WindowState = FormWindowState.Maximized   // Maximize the form
            };
            newForm.BringToFront();                       // Bring to front
            newForm.Show();                               // Show the form
        }
        private void toolQLNV_Click(object sender, EventArgs e)
        {
            OpenForm<frmQuanLyThongTinNhanVien>();
        }
        private void toolCCTL_Click(object sender, EventArgs e)
        {
            OpenForm<frmChamCongVaTinhLuong>();
        }
        private void toolBCNS_Click(object sender, EventArgs e)
        {
            OpenForm<frmBaoCaoNhanSu>();
        }
    }
}
