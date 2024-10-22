using System;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace QuanLyNhanSu
{
    public partial class frmTimEmail : Form
    {
        private int wrongAttempts = 0; // Biến đếm số lần nhập sai
        public frmTimEmail()
        {
            InitializeComponent();
        }

        private void txtTimEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string email = txtTimEmail.Text.Trim();
                if (IsEmailValid(email))
                {
                    // Kiểm tra email trong database
                    if (CheckEmailExists(email))
                    {
                        string password = GetPasswordByEmail(email);
                        if (!string.IsNullOrEmpty(password))
                        {
                            SendEmail(email, password);
                            MessageBox.Show("Mật khẩu đã được gửi về email của bạn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Hide(); // Ẩn form hiện tại nếu cần

                        }
                    }
                    else
                    {
                        wrongAttempts++;
                        MessageBox.Show("Email không tồn tại trong hệ thống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        if (wrongAttempts >= 3)
                        {
                            // Quay về frmLogin nếu nhập sai quá 3 lần
                            frmLogin loginForm = new frmLogin();
                            loginForm.Show();
                            this.Hide();
                            
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Email không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        private bool IsEmailValid(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }
        private string GetPasswordByEmail(string email)
        {
            string password = null;

            using (SqlConnection conn = new SqlConnection("Data Source=THAI_HOANG\\MAY05;Initial Catalog=QLNS;Integrated Security=True"))
            {
                conn.Open();
                string query = "SELECT PASSWORD FROM USERS WHERE EMAIL = @Email";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    var result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        password = result.ToString();
                    }
                }
            }
            return password;
        }


        private void SendEmail(string recipientEmail, string password)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("thaihoang.congviec@gmail.com"); 
                mail.To.Add(recipientEmail);
                mail.Subject = "Bạn vừa yêu cầu quyền truy cập mật khẩu.";
                mail.Body = $"Mật khẩu của bạn là: {password}, vui lòng không cung cấp cho người khác!";

                SmtpClient client = new SmtpClient("smtp.gmail.com", 587); 
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("thaihoang.congviec@gmail.com", "uant poio kuqb sftg"); // Sử dụng mật khẩu ứng dụng
                client.EnableSsl = true; 
                client.Timeout = 200000; 

                client.Send(mail); // Gửi email


                frmBackDangNhap frm = new frmBackDangNhap(recipientEmail); // Truyền email người dùng
                frm.Show();
                frm.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi gửi email: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool CheckEmailExists(string email)
        {
            string connectionString = "Data Source=THAI_HOANG\\MAY05;Initial Catalog=QLNS;Integrated Security=True;";

            using (var connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM USERS WHERE EMAIL = @Email";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    connection.Open();
                    int count = (int)command.ExecuteScalar();
                    return count > 0; // Trả về true nếu email tồn tại
                }
            }
        }

        private void frmTimEmail_Load(object sender, EventArgs e)
        {

        }
    }
    }

