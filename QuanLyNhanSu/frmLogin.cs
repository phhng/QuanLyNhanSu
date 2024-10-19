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
    public partial class frmLogin : Form
    {
        ErrorProvider err;
        public frmLogin()
        {
            InitializeComponent();
        }

        private int checkRole()
        {
            int role = -1;
            //if có tên trong database là quản trị thì trả về 1
            //if else nvns thì trả về 2 còn lại là nhân viên,
            //hoặc k có chức vụ thì trả về -1 -> báo lỗi
            return role;
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            try
            {
                bool hasError = false; //Hàm này dùng để ktra lỗi để k nhảy ra hàm thêm nếu có lỗi
                string ten = txtUsername.Text;
                string pass = txtPassword.Text;
                if (ten == "" || pass == "") // Thêm database để đăng nhập
                {
                    if (ten == "")
                    {
                        err.SetError(txtUsername, "Không được để trống tên đăng nhập!");
                        hasError = true;
                    }
                    else if (pass == "")
                    {
                        err.SetError(txtPassword, "Không được để trống mật khẩu!");
                        hasError = true;
                    }
                    else
                    {
                        MessageBox.Show("Không được để trống 2 khung!","Thông báo",MessageBoxButtons.OK);
                        hasError = true;
                    }
                }
                string temp = ""; //đổi temp = hàm trong database
                if (txtUsername.Text != temp || txtPassword.Text != temp)
                {
                    MessageBox.Show("Người dùng không tồn tại trong hệ thống!", "Thông báo", MessageBoxButtons.OK);
                    hasError = true;
                }
                else if (txtUsername.Text == temp && txtPassword.Text != temp)
                {

                    hasError = true;
                }
                if (!hasError)
                {
                    //thêm hàm nhập
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
