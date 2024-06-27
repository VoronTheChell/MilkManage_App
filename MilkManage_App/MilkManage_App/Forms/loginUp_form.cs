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

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string querystring = $"select * from Users where username = '{loginUser}' and password = '{passUser}'";

            SqlCommand command = new SqlCommand(querystring, dataBase.GetConnection());

            // Сommand Code
            adapter.SelectCommand = command;
            adapter.Fill(table);

            admin_Form adminForm = new admin_Form();
            directorForm directorForm = new directorForm();
            loaderForm loaderForm = new loaderForm();

            if (table.Rows.Count == 1)
            {
                MessageBox.Show("Вы успешно вошли!", "Успешно!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                takeIDUser(textBox_login.Text, textBox_password.Text);

                textBox_login.Text = "";
                textBox_password.Text = "";

                dataBase.openConnection();
                string checkStatus = $"SELECT role from Users where username like '{loginUser}'";
                SqlCommand commandCheck = new SqlCommand(checkStatus, dataBase.GetConnection());
                string status = ((string)commandCheck.ExecuteScalar());

                Console.WriteLine(status);

                switch (status)
                {
                    case "администратор":
                        {
                            this.Hide();
                            adminForm.ShowDialog();
                            this.Show();
                            break;
                        }

                    case "директор":
                        {
                            this.Hide();
                            directorForm.ShowDialog();
                            this.Show();
                            break;
                        }

                    case "грузчик":
                        {
                            this.Hide();
                            loaderForm.ShowDialog();
                            this.Show();
                            break;
                        }
                }



            }

            else
            {
                MessageBox.Show("Ошибка входа!\nТого пользователя не существует или вы вели не верный пароль!", "Ошибка...", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
