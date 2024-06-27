using MilkManage_App.Forms;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MilkManage_App
{
    public partial class loginUp_form : Form
    {

        DataBase dataBase = new DataBase();

        public loginUp_form()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            MaximizeBox = false;
        }

        private void loginUp_form_Load(object sender, EventArgs e)
        {
            textBox_password.PasswordChar = '●';
            btnNotSeePass.Visible = false;

            textBox_login.MaxLength = 50;
            textBox_password.MaxLength = 50;
        }

        private void textBoxToSubmit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void Show_Click(object sender, EventArgs e)
        {
            textBox_password.UseSystemPasswordChar = true;
            btnSeePass.Visible = false;
            btnNotSeePass.Visible = true;
        }

        private void Hide_Click(object sender, EventArgs e)
        {
            textBox_password.UseSystemPasswordChar = false;
            btnSeePass.Visible = true;
            btnNotSeePass.Visible = false;
        }


        private void LoginButton_Click(object sender, EventArgs e)
        {
            var loginUser = textBox_login.Text;
            var passUser = textBox_password.Text;

            string querystring = "SELECT role FROM Users WHERE username = @username AND password = @password";

            SqlCommand command = new SqlCommand(querystring, dataBase.GetConnection());
            command.Parameters.AddWithValue("@username", loginUser);
            command.Parameters.AddWithValue("@password", passUser);

            dataBase.openConnection();

            string status = (string)command.ExecuteScalar();

            dataBase.closeConnection();

            if (status != null)
            {
                MessageBox.Show("Вы успешно вошли!", "Успешно!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                takeIDUser(loginUser, passUser);

                textBox_login.Text = "";
                textBox_password.Text = "";

                Form formToShow = null;

                switch (status)
                {
                    case "администратор":
                        formToShow = new admin_Form();
                        break;
                    case "директор":
                        formToShow = new directorForm();
                        break;
                    case "грузчик":
                        formToShow = new loaderForm();
                        break;
                    default:
                        MessageBox.Show("Роль пользователя не распознана", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                }

                this.Hide();
                formToShow.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Ошибка входа!\nТакого пользователя не существует или вы ввели неверный пароль!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public void takeIDUser(string username, string password)
        {
            string connectionString = @"Data Source=VORONPC\MSSQLS; Initial Catalog=MilkManageDB; Integrated Security=true";
            string query = "SELECT user_id FROM Users WHERE username = @username AND password = @password";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);

                connection.Open();

                object result = command.ExecuteScalar();
                int userId = Convert.ToInt32(result);
                GlobalData.UserID = userId;
            }
        }

        private void Secret_Click(object sender, EventArgs e)
        {
            label3.Hide();
            label5.Show();
        }
    }
}
