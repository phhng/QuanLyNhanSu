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
            int temp = -1;
            //if có tên trong database là quản trị thì trả về 1
            //if else nvns thì trả về 2 còn lại là nhân viên,
            //hoặc k có chức vụ thì trả về -1 -> báo lỗi
            return temp;
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            if(txtUsername.Text == "" || txtPassword.Text == "")// Thêm database để đăng nhập
            {
                if (txtUsername.Text == "")
                {
                    err.SetError(txtUsername, "Không được để trống tên đăng nhập!");
                }
                else if (txtPassword.Text == "")
                {
                    err.SetError(txtPassword, "Không được để trống mật khẩu!");
                }
                else
                {
                    //Thêm khai báo lỗi dưới 2 fields
                }
            }
            string temp = ""; //đổi temp = hàm trong database
            if(txtUsername.Text != temp || txtPassword.Text != temp)
            {
                MessageBox.Show("Người dùng không tồn tại trong hệ thống!");
            }
            else if(txtUsername.Text == temp && txtPassword.Text != temp)
            {

            }
        }
    }
}
