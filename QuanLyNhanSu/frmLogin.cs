using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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
        private bool ValidateUser(string username, string password)
        {
            string connectionString = "Server=THAI_HOANG\\MAY05;Database=QLNS;Trusted_Connection=True;";

            using (var connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM USERS WHERE EMAIL = @Email AND PASSWORD = @Password";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", username);
                    command.Parameters.AddWithValue("@Password", password); // Hãy đảm bảo mật khẩu được băm trước khi lưu vào DB

                    connection.Open();
                    int count = (int)command.ExecuteScalar();
                    return count > 0; // Trả về true nếu tìm thấy người dùng
                }
            }
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            try
            {
                bool hasError = false; // Biến đánh dấu lỗi
                string ten = txtUsername.Text;
                string pass = txtPassword.Text;

                // Kiểm tra trường hợp rỗng
                if (string.IsNullOrWhiteSpace(ten) || string.IsNullOrWhiteSpace(pass))
                {
                    if (string.IsNullOrWhiteSpace(ten))
                    {
                        err.SetError(txtUsername, "Không được để trống tên đăng nhập!");
                    }
                    if (string.IsNullOrWhiteSpace(pass))
                    {
                        err.SetError(txtPassword, "Không được để trống mật khẩu!");
                    }
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK);
                    hasError = true;
                }

                if (!hasError)
                {
                    // Thay thế phương thức này bằng logic kiểm tra trong cơ sở dữ liệu của bạn
                    bool isValidUser = ValidateUser(ten, pass);
                    if (!isValidUser)
                    {
                        MessageBox.Show("Người dùng không tồn tại trong hệ thống!", "Thông báo", MessageBoxButtons.OK);
                        hasError = true;
                    }
                }

                if (!hasError)
                {
                    // Nếu xác thực thành công, mở frmMain
                    frmMain mainForm = new frmMain();
                    mainForm.Show();
                    this.Hide(); // Tùy chọn ẩn form đăng nhập
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

          
            frmTimEmail timEmailForm = new frmTimEmail();
            timEmailForm.Show();
            this.Hide(); 
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }
    }
    
}

