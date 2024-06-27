using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


namespace MilkManage_App.Forms
{
    public partial class admin_Form : Form
    {
        DataBase dataBase = new DataBase();

        int selectdRow;

        private int id_users;
        private int employee_id, product_id, 
                    order_id, order_detail_id, 
                    delivery_id;

        enum RowState
        {
            Existed,
            New,
            Modified,
            ModifiedNew,
            Deleted
        }

        public admin_Form()
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterScreen;
            MaximizeBox = false;

            CreateColumns_Users();
            CreateColumns_Employees();
            CreateColumns_Products();
            CreateColumns_Orders();
            CreateColumns_OrderDetails();
            CreateColumns_Deliveries();
        }

        // Отображение и обновление таблиц
        private void CreateColumns_Users()
        {
            dataBase.openConnection();
            SqlCommand com = new SqlCommand(@"SELECT user_id as 'ID:', username as 'Логин пользователей:', password as 'Пароли:', 
					                          role as 'Тип пользователя:' from Users", dataBase.sqlConnection);

            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "Users");
            dataGridViewUsers.DataSource = ds.Tables[0];
            dataBase.closeConnection();
            dataGridViewUsers.AutoResizeColumns();
        }

        private void CreateColumns_Employees()
        {
            dataBase.openConnection();
            SqlCommand com = new SqlCommand(@"SELECT employee_id as 'ID:', name as 'ФИО:', position as 'Должность:', 
					                          start_date as 'Дата начала работы:' from Employees", dataBase.sqlConnection);

            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "Employees");
            dataGridViewEmployees.DataSource = ds.Tables[0];
            dataBase.closeConnection();
            dataGridViewEmployees.AutoResizeColumns();
        }

        private void CreateColumns_Products()
        {
            dataBase.openConnection();
            SqlCommand com = new SqlCommand(@"SELECT product_id as 'ID:', name  as 'Названия продуктов:', description as 'Описание продукции:', 
					                          price as 'Цена (в руб.):' from Products", dataBase.sqlConnection);

            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "Products");
            dataGridViewProducts.DataSource = ds.Tables[0];
            dataBase.closeConnection();
            dataGridViewProducts.AutoResizeColumns();
        }

        private void CreateColumns_Orders()
        {
            dataBase.openConnection();
            SqlCommand com = new SqlCommand(@"SELECT order_id as 'ID:', customer_name as 'Имя клиента:', order_date as 'Дата заказа:', 
					                          status as 'Статус заказа:', total_amount as 'Общая сумма заказа:' from Orders", dataBase.sqlConnection);

            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "Orders");
            dataGridViewOrders.DataSource = ds.Tables[0];
            dataBase.closeConnection();
            dataGridViewOrders.AutoResizeColumns();
        }

        private void CreateColumns_OrderDetails()
        {
            dataBase.openConnection();
            SqlCommand com = new SqlCommand(@"SELECT order_detail_id as 'ID:', order_id as 'ID Заказа:', product_id as 'ID Продукта:', 
					                          quantity as 'Количество продукции в заказе:' from OrderDetails", dataBase.sqlConnection);

            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "OrderDetails");
            dataGridViewOrderDetails.DataSource = ds.Tables[0];
            dataBase.closeConnection();
            dataGridViewOrderDetails.AutoResizeColumns();
        }

        private void CreateColumns_Deliveries()
        {
            dataBase.openConnection();
            SqlCommand com = new SqlCommand(@"SELECT delivery_id as 'ID:', order_id as 'ID Заказа:', delivery_date as 'Дата поставки:', 
					                          status as 'Статус поставки:' from Deliveries",  dataBase.sqlConnection);

            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "Deliveries");
            dataGridViewDeliveries.DataSource = ds.Tables[0];
            dataBase.closeConnection();
            dataGridViewDeliveries.AutoResizeColumns();
        }

        // Кнопки обновления таблиц
        private void ClearButton1_Click(object sender, EventArgs e)
        {
            CreateColumns_Users();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            CreateColumns_Employees();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            CreateColumns_Products();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            CreateColumns_Orders();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            CreateColumns_OrderDetails();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            CreateColumns_Deliveries();
        }

//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        // Реализация функции для пункта: Пользователи
        private void SearchBox_TextChanged(object sender, EventArgs e)  
        {
            string searchapString = $"SELECT user_id as 'ID:', username as 'Логин пользователей:', password as 'Пароли:', " +
                                    $"role as 'Тип пользователя:' from Users where username like '%" + SearchBox.Text + "%'";

            SqlCommand com = new SqlCommand(searchapString, dataBase.GetConnection());
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataSet ds = new DataSet();

            adapter.Fill(ds, "applicant");
            dataGridViewUsers.DataSource = ds.Tables[0];

            dataBase.closeConnection();
        }

        private void dataGridViewUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectdRow = e.RowIndex;

            if (e.RowIndex >= 0)
            {
                try
                {
                    DataGridViewRow row = dataGridViewUsers.Rows[selectdRow];

                    id_users = Convert.ToInt32(row.Cells[0].Value);
                    textBoxUser.Text = row.Cells[1].Value.ToString();
                    textBoxPassUser.Text = row.Cells[2].Value.ToString();
                    comboBox1.Text = row.Cells[3].Value.ToString();
                }

                catch
                {
                    MessageBox.Show("Вы выбрали пустую строчку!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
        }

        private void buttonNewUser_Click(object sender, EventArgs e)
        {
            try
            {
                string addCommandString = $"INSERT INTO Users (username, password, role) " +
                                          $"VALUES ('{textBoxUser.Text}', '{textBoxPassUser.Text}', '{comboBox1.Text}')";

                dataBase.openConnection();

                SqlCommand com = new SqlCommand(addCommandString, dataBase.GetConnection());
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "applicant");

                CreateColumns_Users();

                textBoxUser.Text = "";
                textBoxPassUser.Text = "";
                comboBox1.Text = "";

                dataBase.closeConnection();

                MessageBox.Show("Запись успешно создана!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            catch
            {
                MessageBox.Show("Ошибка при создании записи, такая запись существует!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonChangeUser_Click(object sender, EventArgs e)
        {
            // Проверяем наличие значений в текстовых полях перед формированием запроса
            if (string.IsNullOrEmpty(textBoxUser.Text) || string.IsNullOrEmpty(textBoxPassUser.Text) || string.IsNullOrEmpty(comboBox1.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Формируем SQL запрос с использованием параметров
            string changeCommandString = "UPDATE Users SET username = @Login, password = @Password, role = @Role WHERE user_id = @UserID;";

            // Открываем соединение с базой данных
            dataBase.openConnection();

            SqlCommand com = new SqlCommand(changeCommandString, dataBase.GetConnection());
            com.Parameters.AddWithValue("@Login", textBoxUser.Text);
            com.Parameters.AddWithValue("@Password", textBoxPassUser.Text);
            com.Parameters.AddWithValue("@Role", comboBox1.Text);
            com.Parameters.AddWithValue("@UserID", id_users);

            // Выполняем команду
            com.ExecuteNonQuery();

            // Обновляем DataGridView
            CreateColumns_Users();

            // Очищаем текстовые поля
            textBoxUser.Text = "";
            textBoxPassUser.Text = "";
            comboBox1.Text = "";

            // Закрываем соединение с базой данных
            dataBase.closeConnection();

            // Показываем сообщение об успешном изменении записи
            MessageBox.Show("Запись успешно изменена!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonDelUser_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxUser.Text) || string.IsNullOrEmpty(textBoxPassUser.Text) || string.IsNullOrEmpty(comboBox1.Text))
            {
                MessageBox.Show("Отсутсвует выбранная запись для удаления!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult qtdel = MessageBox.Show("Вы точно хотите удалить запись?", "Подтверждение удаления записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (qtdel == DialogResult.Yes)
            {
                try
                {
                    string delcommandString = $"DELETE Users where user_id = '{id_users}'";

                    dataBase.openConnection();

                    SqlCommand com = new SqlCommand(delcommandString, dataBase.GetConnection());
                    SqlDataAdapter adapter = new SqlDataAdapter(com);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds, "applicant");

                    CreateColumns_Users();

                    textBoxUser.Text = "";
                    textBoxPassUser.Text = "";
                    comboBox1.Text = "";

                    dataBase.closeConnection();

                    MessageBox.Show("Запись успешно удалена!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                catch
                {
                    MessageBox.Show($"Ошибка при удаление выбранной записи", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        // Реализация функции для пункта: Сотрудники
        private void SearchBox2_TextChanged(object sender, EventArgs e)   
        {
            string searchapString = $"SELECT employee_id as 'ID:', name as 'ФИО:', position as 'Должность:', " +
                                    $"start_date as 'Дата начала работы:' from Employees where name like '%" + SearchBox2.Text + "%'";

            SqlCommand com = new SqlCommand(searchapString, dataBase.GetConnection());
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataSet ds = new DataSet();

            adapter.Fill(ds, "applicant");
            dataGridViewEmployees.DataSource = ds.Tables[0];

            dataBase.closeConnection();
        }

        private void dataGridViewEmployees_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectdRow = e.RowIndex;

            if (e.RowIndex >= 0)
            {
                try
                {
                    DataGridViewRow row = dataGridViewEmployees.Rows[selectdRow];
                    object value = row.Cells[3].Value.ToString();
                    string stringValue = value.ToString().Replace("00:00:00", "");

                    employee_id = Convert.ToInt32(row.Cells[0].Value);
                    textBoxNameEnployer.Text = row.Cells[1].Value.ToString();
                    textBoxWork.Text = row.Cells[2].Value.ToString();
                    textBoxDateOfWork.Text = stringValue.ToString();
                }

                catch
                {
                    MessageBox.Show("Вы выбрали пустую строчку!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
        }

        private void buttonNewEmployer_Click(object sender, EventArgs e)
        {
            try
            {
                // Проверяем наличие значений в текстовых полях перед формированием запроса
                if (string.IsNullOrEmpty(textBoxNameEnployer.Text) || string.IsNullOrEmpty(textBoxWork.Text) || string.IsNullOrEmpty(textBoxDateOfWork.Text))
                {
                    MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string addCommandString = $"INSERT INTO Employees (name, position, start_date) " +
                                          $"VALUES ('{textBoxNameEnployer.Text}', '{textBoxWork.Text}', '{textBoxDateOfWork.Text}')";

                dataBase.openConnection();

                SqlCommand com = new SqlCommand(addCommandString, dataBase.GetConnection());
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "applicant");

                CreateColumns_Employees();

                textBoxNameEnployer.Text = "";
                textBoxWork.Text = "";
                textBoxDateOfWork.Text = "";

                dataBase.closeConnection();

                MessageBox.Show("Запись успешно создана!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            catch
            {
                MessageBox.Show("Ошибка при создании записи, такая запись существует!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonChangeEnployer_Click(object sender, EventArgs e)
        {
            // Проверяем наличие значений в текстовых полях перед формированием запроса
            if (string.IsNullOrEmpty(textBoxNameEnployer.Text) || string.IsNullOrEmpty(textBoxWork.Text) || string.IsNullOrEmpty(textBoxDateOfWork.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Формируем SQL запрос с использованием параметров
            string changeCommandString = "UPDATE Employees SET name = @Name, position = @Position, start_date = @Date WHERE employee_id = @EmployeeID;";

            // Открываем соединение с базой данных
            dataBase.openConnection();

            SqlCommand com = new SqlCommand(changeCommandString, dataBase.GetConnection());
            com.Parameters.AddWithValue("@Name", textBoxNameEnployer.Text);
            com.Parameters.AddWithValue("@Position", textBoxWork.Text);
            com.Parameters.AddWithValue("@Date", textBoxDateOfWork.Text);
            com.Parameters.AddWithValue("@EmployeeID", employee_id);

            // Выполняем команду
            com.ExecuteNonQuery();

            // Обновляем DataGridView
            CreateColumns_Employees();

            // Очищаем текстовые поля
            textBoxNameEnployer.Text = "";
            textBoxWork.Text = "";
            textBoxDateOfWork.Text = "";

            // Закрываем соединение с базой данных
            dataBase.closeConnection();

            // Показываем сообщение об успешном изменении записи
            MessageBox.Show("Запись успешно изменена!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonDelEmployer_Click(object sender, EventArgs e)
        {
            // Проверяем наличие значений в текстовых полях перед формированием запроса
            if (string.IsNullOrEmpty(textBoxNameEnployer.Text) || string.IsNullOrEmpty(textBoxWork.Text) || string.IsNullOrEmpty(textBoxDateOfWork.Text))
            {
                MessageBox.Show("Отсутсвует выбранная запись для удаления!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult qtdel = MessageBox.Show("Вы точно хотите удалить запись?", "Подтверждение удаления записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (qtdel == DialogResult.Yes)
            {
                try
                {
                    string delcommandString = $"DELETE Employees where employee_id = '{employee_id}'";

                    dataBase.openConnection();

                    SqlCommand com = new SqlCommand(delcommandString, dataBase.GetConnection());
                    SqlDataAdapter adapter = new SqlDataAdapter(com);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds, "applicant");

                    CreateColumns_Employees();

                    textBoxNameEnployer.Text = "";
                    textBoxWork.Text = "";
                    textBoxDateOfWork.Text = "";

                    dataBase.closeConnection();

                    MessageBox.Show("Запись успешно удалена!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                catch
                {
                    MessageBox.Show("Ошибка при удаление выбранной записи", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        // Реализация функции для пункта: Продукт
        private void SearchBox3_TextChanged(object sender, EventArgs e)  
        {
            string searchapString = $"SELECT product_id as 'ID:', name  as 'Названия продуктов:', description as 'Описание продукции:', price as 'Цена (в руб.):' " +
                                    $"from Products where name like '%" + SearchBox3.Text + "%'";

            SqlCommand com = new SqlCommand(searchapString, dataBase.GetConnection());
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataSet ds = new DataSet();

            adapter.Fill(ds, "applicant");
            dataGridViewProducts.DataSource = ds.Tables[0];

            dataBase.closeConnection();
        }

        private void dataGridViewProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            selectdRow = e.RowIndex;

            if (e.RowIndex >= 0)
            {
                try
                {
                    DataGridViewRow row = dataGridViewProducts.Rows[selectdRow];
                    object value = row.Cells[3].Value.ToString();
                    string stringValue = value.ToString().Replace(',', '.');

                    product_id = Convert.ToInt32(row.Cells[0].Value);
                    textBoxNameProduct.Text = row.Cells[1].Value.ToString();
                    textBoxProductDscr.Text = row.Cells[2].Value.ToString();
                    textBoxValuesProduct.Text = stringValue.ToString();
                }

                catch
                {
                    MessageBox.Show("Вы выбрали пустую строчку!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
        }

        private void buttonNewProduction_Click(object sender, EventArgs e)
        {
            try
            {
                // Проверяем наличие значений в текстовых полях перед формированием запроса
                if (string.IsNullOrEmpty(textBoxNameProduct.Text) || string.IsNullOrEmpty(textBoxProductDscr.Text) || string.IsNullOrEmpty(textBoxValuesProduct.Text))
                {
                    MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string addCommandString = $"INSERT INTO Products (name, description, price) " +
                                          $"VALUES ('{textBoxNameProduct.Text}', '{textBoxProductDscr.Text}', '{textBoxValuesProduct.Text}')";

                dataBase.openConnection();

                SqlCommand com = new SqlCommand(addCommandString, dataBase.GetConnection());
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "applicant");

                CreateColumns_Products();

                textBoxNameProduct.Text = "";
                textBoxProductDscr.Text = "";
                textBoxValuesProduct.Text = "";

                dataBase.closeConnection();

                MessageBox.Show("Запись успешно создана!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            catch
            {
                MessageBox.Show("Ошибка при создании записи, такая запись существует!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonChangeProduction_Click(object sender, EventArgs e)
        {
            // Проверяем наличие значений в текстовых полях перед формированием запроса
            if (string.IsNullOrEmpty(textBoxNameProduct.Text) || string.IsNullOrEmpty(textBoxProductDscr.Text) || string.IsNullOrEmpty(textBoxValuesProduct.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Формируем SQL запрос с использованием параметров
            string changeCommandString = "UPDATE Products SET name = @Name, " +
                                        "description = @Desc, price = @Price WHERE product_id = @ProductID;";

            // Открываем соединение с базой данных
            dataBase.openConnection();

            SqlCommand com = new SqlCommand(changeCommandString, dataBase.GetConnection());
            com.Parameters.AddWithValue("@Name", textBoxNameProduct.Text);
            com.Parameters.AddWithValue("@Desc", textBoxProductDscr.Text);
            com.Parameters.AddWithValue("@Price", textBoxValuesProduct.Text);
            com.Parameters.AddWithValue("@ProductID", product_id);

            // Выполняем команду
            com.ExecuteNonQuery();

            // Обновляем DataGridView
            CreateColumns_Products();

            // Очищаем текстовые поля
            textBoxNameProduct.Text = "";
            textBoxProductDscr.Text = "";
            textBoxValuesProduct.Text = "";

            // Закрываем соединение с базой данных
            dataBase.closeConnection();

            // Показываем сообщение об успешном изменении записи
            MessageBox.Show("Запись успешно изменена!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonDelProduction_Click(object sender, EventArgs e)
        {
            // Проверяем наличие значений в текстовых полях перед формированием запроса
            if (string.IsNullOrEmpty(textBoxNameProduct.Text) || string.IsNullOrEmpty(textBoxProductDscr.Text) || string.IsNullOrEmpty(textBoxValuesProduct.Text))
            {
                MessageBox.Show("Отсутсвует выбранная запись для удаления!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult qtdel = MessageBox.Show("Вы точно хотите удалить запись?", "Подтверждение удаления записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (qtdel == DialogResult.Yes)
            {
                try
                {
                    string delcommandString = $"DELETE Products where product_id = '{product_id}'";

                    dataBase.openConnection();

                    SqlCommand com = new SqlCommand(delcommandString, dataBase.GetConnection());
                    SqlDataAdapter adapter = new SqlDataAdapter(com);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds, "applicant");

                    CreateColumns_Products();

                    textBoxNameProduct.Text = "";
                    textBoxProductDscr.Text = "";
                    textBoxValuesProduct.Text = "";

                    dataBase.closeConnection();

                    MessageBox.Show("Запись успешно удалена!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                catch
                {
                    MessageBox.Show("Ошибка при удаление выбранной записи", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        // Реализация функции для пункта: Заказы
        private void SearchBox4_TextChanged(object sender, EventArgs e)
        {
            string searchapString = $"SELECT order_id as 'ID:', customer_name as 'Имя клиента:', order_date as 'Дата заказа:', " +
                                    $"status as 'Статус заказа:', total_amount as 'Общая сумма заказа:' from Orders where customer_name like '%" + SearchBox4.Text + "%'";

            SqlCommand com = new SqlCommand(searchapString, dataBase.GetConnection());
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataSet ds = new DataSet();

            adapter.Fill(ds, "applicant");
            dataGridViewOrders.DataSource = ds.Tables[0];

            dataBase.closeConnection();
        }

        private void dataGridViewOrders_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            selectdRow = e.RowIndex;

            if (e.RowIndex >= 0)
            {
                try
                {
                    DataGridViewRow row = dataGridViewOrders.Rows[selectdRow];
                    object value = row.Cells[4].Value.ToString();
                    string stringValue = value.ToString().Replace(',', '.');

                    order_id = Convert.ToInt32(row.Cells[0].Value);
                    textBoxNameClient.Text = row.Cells[1].Value.ToString();
                    textBoxDataOrder.Text = row.Cells[2].Value.ToString();
                    comboBox3.Text = row.Cells[3].Value.ToString();
                    textBoxValuesOrder.Text = stringValue.ToString();
                }

                catch
                {
                    MessageBox.Show("Вы выбрали пустую строчку!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void buttonNewOrder_Click(object sender, EventArgs e)
        {
            try
            {
                // Проверяем наличие значений в текстовых полях перед формированием запроса
                if (string.IsNullOrEmpty(textBoxNameClient.Text) || string.IsNullOrEmpty(textBoxDataOrder.Text) || string.IsNullOrEmpty(comboBox3.Text) || string.IsNullOrEmpty(textBoxValuesOrder.Text))
                {
                    MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string addCommandString = $"INSERT INTO Orders (customer_name, order_date, status, total_amount) " +
                                          $"VALUES ('{textBoxNameClient.Text}', '{textBoxDataOrder.Text}', '{comboBox3.Text}', '{textBoxValuesOrder.Text}')";

                dataBase.openConnection();

                SqlCommand com = new SqlCommand(addCommandString, dataBase.GetConnection());
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "applicant");

                CreateColumns_Orders();

                textBoxNameClient.Text = "";
                textBoxDataOrder.Text = "";
                comboBox3.Text = "";
                textBoxValuesOrder.Text = "";

                dataBase.closeConnection();

                MessageBox.Show("Запись успешно создана!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            catch
            {
                MessageBox.Show("Ошибка при создании записи, такая запись существует!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonChangeOrder_Click(object sender, EventArgs e)
        {
            // Проверяем наличие значений в текстовых полях перед формированием запроса
            if (string.IsNullOrEmpty(textBoxNameClient.Text) || string.IsNullOrEmpty(textBoxDataOrder.Text) || string.IsNullOrEmpty(comboBox3.Text) || string.IsNullOrEmpty(textBoxValuesOrder.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Формируем SQL запрос с использованием параметров
            string changeCommandString = "UPDATE Orders SET customer_name = @Name, " +
                                        "order_date = @DataOrder, status = @StatusOrder, total_amount = @ValuesOrder " +
                                        "WHERE order_id = @OrderID;";

            // Открываем соединение с базой данных
            dataBase.openConnection();

            SqlCommand com = new SqlCommand(changeCommandString, dataBase.GetConnection());
            com.Parameters.AddWithValue("@Name", textBoxNameClient.Text);
            com.Parameters.AddWithValue("@DataOrder", textBoxDataOrder.Text);
            com.Parameters.AddWithValue("@StatusOrder", comboBox3.Text);
            com.Parameters.AddWithValue("@ValuesOrder", textBoxValuesOrder.Text);
            com.Parameters.AddWithValue("@OrderID", order_id);

            // Выполняем команду
            com.ExecuteNonQuery();

            // Обновляем DataGridView
            CreateColumns_Orders();

            // Очищаем текстовые поля
            textBoxNameClient.Text = "";
            textBoxDataOrder.Text = "";
            comboBox3.Text = "";
            textBoxValuesOrder.Text = "";

            // Закрываем соединение с базой данных
            dataBase.closeConnection();

            // Показываем сообщение об успешном изменении записи
            MessageBox.Show("Запись успешно изменена!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonDelOrder_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxNameClient.Text) || string.IsNullOrEmpty(textBoxDataOrder.Text) || string.IsNullOrEmpty(comboBox3.Text) || string.IsNullOrEmpty(textBoxValuesOrder.Text))
            {
                MessageBox.Show("Отсутсвует выбранная запись для удаления!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult qtdel = MessageBox.Show("Вы точно хотите удалить запись?", "Подтверждение удаления записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (qtdel == DialogResult.Yes)
            {
                try
                {
                    string delcommandString = $"DELETE Orders where order_id = '{order_id}'";

                    dataBase.openConnection();

                    SqlCommand com = new SqlCommand(delcommandString, dataBase.GetConnection());
                    SqlDataAdapter adapter = new SqlDataAdapter(com);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds, "applicant");

                    CreateColumns_Orders();

                    textBoxNameClient.Text = "";
                    textBoxDataOrder.Text = "";
                    comboBox3.Text = "";
                    textBoxValuesOrder.Text = "";

                    dataBase.closeConnection();

                    MessageBox.Show("Запись успешно удалена!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                catch
                {
                    MessageBox.Show("Ошибка при удаление выбранной записи", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        // Реализация функции для пункта: Описание Заказа
        private void SearchBox5_TextChanged(object sender, EventArgs e)
        {
            string searchapString = $"SELECT order_detail_id as 'ID:', order_id as 'ID Заказа:', product_id as 'ID Продукта:', " +
                                    $"quantity as 'Количество продукции в заказе:' from OrderDetails where order_id like '%" + SearchBox5.Text + "%'";

            SqlCommand com = new SqlCommand(searchapString, dataBase.GetConnection());
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataSet ds = new DataSet();

            adapter.Fill(ds, "applicant");
            dataGridViewOrderDetails.DataSource = ds.Tables[0];

            dataBase.closeConnection();
        }

        private void dataGridViewOrderDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            selectdRow = e.RowIndex;

            if (e.RowIndex >= 0)
            {
                try
                {
                    DataGridViewRow row = dataGridViewOrderDetails.Rows[selectdRow];

                    order_detail_id = Convert.ToInt32(row.Cells[0].Value);
                    textBoxIdOrders.Text = row.Cells[1].Value.ToString();
                    textBoxIdProduct.Text = row.Cells[2].Value.ToString();
                    textBoxIdOrderInOrder.Text = row.Cells[3].Value.ToString();
                }

                catch
                {
                    MessageBox.Show("Вы выбрали пустую строчку!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void buttonNewDetOrder_Click(object sender, EventArgs e)
        {
            try
            {
                // Проверяем наличие значений в текстовых полях перед формированием запроса
                if (string.IsNullOrEmpty(textBoxIdOrders.Text) || string.IsNullOrEmpty(textBoxIdProduct.Text) || string.IsNullOrEmpty(textBoxIdOrderInOrder.Text))
                {
                    MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string addCommandString = $"INSERT INTO OrderDetails (order_id, product_id, quantity) " +
                                          $"VALUES ('{textBoxIdOrders.Text}', '{textBoxIdProduct.Text}', '{textBoxIdOrderInOrder.Text}')";

                dataBase.openConnection();

                SqlCommand com = new SqlCommand(addCommandString, dataBase.GetConnection());
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "applicant");

                CreateColumns_OrderDetails();

                textBoxIdOrders.Text = "";
                textBoxIdProduct.Text = "";
                textBoxIdOrderInOrder.Text = "";

                dataBase.closeConnection();

                MessageBox.Show("Запись успешно создана!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            catch
            {
                MessageBox.Show("Ошибка при создании записи, такая запись существует!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonChangeDetOrder_Click(object sender, EventArgs e)
        {
            // Проверяем наличие значений в текстовых полях перед формированием запроса
            if (string.IsNullOrEmpty(textBoxIdOrders.Text) || string.IsNullOrEmpty(textBoxIdProduct.Text) || string.IsNullOrEmpty(textBoxIdOrderInOrder.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Формируем SQL запрос с использованием параметров
            string changeCommandString = "UPDATE OrderDetails SET order_id = @OrderID, " +
                                         "product_id = @ProductID, quantity = @Quantity " +
                                         "WHERE order_detail_id = @OrderDetailID;";

            // Открываем соединение с базой данных
            dataBase.openConnection();

            SqlCommand com = new SqlCommand(changeCommandString, dataBase.GetConnection());
            com.Parameters.AddWithValue("@OrderID", textBoxIdOrders.Text);
            com.Parameters.AddWithValue("@ProductID", textBoxIdProduct.Text);
            com.Parameters.AddWithValue("@Quantity", textBoxIdOrderInOrder.Text);
            com.Parameters.AddWithValue("@OrderDetailID", order_detail_id);

            // Выполняем команду
            com.ExecuteNonQuery();

            // Обновляем DataGridView
            CreateColumns_OrderDetails();

            // Очищаем текстовые поля
            textBoxIdOrders.Text = "";
            textBoxIdProduct.Text = "";
            textBoxIdOrderInOrder.Text = "";

            // Закрываем соединение с базой данных
            dataBase.closeConnection();

            // Показываем сообщение об успешном изменении записи
            MessageBox.Show("Запись успешно изменена!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonDelDetOrder_Click(object sender, EventArgs e)
        {
            // Проверяем наличие значений в текстовых полях перед формированием запроса
            if (string.IsNullOrEmpty(textBoxIdOrders.Text) || string.IsNullOrEmpty(textBoxIdProduct.Text) || string.IsNullOrEmpty(textBoxIdOrderInOrder.Text))
            {
                MessageBox.Show("Отсутсвует выбранная запись для удаления!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult qtdel = MessageBox.Show("Вы точно хотите удалить запись?", "Подтверждение удаления записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (qtdel == DialogResult.Yes)
            {
                try
                {
                    string delcommandString = $"DELETE OrderDetails where order_detail_id = '{order_detail_id}'";

                    dataBase.openConnection();

                    SqlCommand com = new SqlCommand(delcommandString, dataBase.GetConnection());
                    SqlDataAdapter adapter = new SqlDataAdapter(com);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds, "applicant");

                    CreateColumns_OrderDetails();

                    textBoxIdOrders.Text = "";
                    textBoxIdProduct.Text = "";
                    textBoxIdOrderInOrder.Text = "";

                    dataBase.closeConnection();

                    MessageBox.Show("Запись успешно удалена!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                catch
                {
                    MessageBox.Show("Ошибка при удаление выбранной записи", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        // Реализация функции для пункта: Доставка
        private void SearchBox6_TextChanged(object sender, EventArgs e)
        {
            string searchapString = $"SELECT delivery_id as 'ID:', order_id as 'ID Заказа:', delivery_date as 'Дата поставки:', " +
                                    $"status as 'Статус поставки:' from Deliveries where order_id like '%" + tabControl.Text + "%'";

            SqlCommand com = new SqlCommand(searchapString, dataBase.GetConnection());
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataSet ds = new DataSet();

            adapter.Fill(ds, "applicant");
            dataGridViewDeliveries.DataSource = ds.Tables[0];

            dataBase.closeConnection();
        }

        private void dataGridViewDeliveries_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            selectdRow = e.RowIndex;

            if (e.RowIndex >= 0)
            {
                try
                {
                    DataGridViewRow row = dataGridViewDeliveries.Rows[selectdRow];
                    object value = row.Cells[2].Value.ToString();
                    string stringValue = value.ToString().Replace("00:00:00", "");

                    delivery_id = Convert.ToInt32(row.Cells[0].Value);
                    textBoxIdOrders2.Text = row.Cells[1].Value.ToString();
                    textBoxDateOrder.Text = stringValue.ToString();
                    comboBox4.Text = row.Cells[3].Value.ToString();
                }

                catch
                {
                    MessageBox.Show("Вы выбрали пустую строчку!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void buttonNewPostavki_Click(object sender, EventArgs e)
        {
            try
            {
                // Проверяем наличие значений в текстовых полях перед формированием запроса
                if (string.IsNullOrEmpty(textBoxIdOrders2.Text) || string.IsNullOrEmpty(textBoxDateOrder.Text) || string.IsNullOrEmpty(comboBox4.Text))
                {
                    MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string addCommandString = $"INSERT INTO Deliveries (order_id, delivery_date, status, updated_by) " +
                                          $"VALUES ('{textBoxIdOrders2.Text}', '{textBoxDateOrder.Text}', '{comboBox4.Text}', '{GlobalData.UserID}')";

                dataBase.openConnection();

                SqlCommand com = new SqlCommand(addCommandString, dataBase.GetConnection());
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "applicant");

                CreateColumns_Deliveries();

                textBoxIdOrders2.Text = "";
                textBoxDateOrder.Text = "";
                comboBox4.Text = "";

                dataBase.closeConnection();

                MessageBox.Show("Запись успешно создана!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            catch
            {
                MessageBox.Show("Ошибка при создании записи, такая запись существует!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonChangePostavki_Click(object sender, EventArgs e)
        {
            // Проверяем наличие значений в текстовых полях перед формированием запроса
            if (string.IsNullOrEmpty(textBoxIdOrders2.Text) || string.IsNullOrEmpty(textBoxDateOrder.Text) || string.IsNullOrEmpty(comboBox4.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Формируем SQL запрос с использованием параметров
            string changeCommandString = "UPDATE Deliveries SET order_id = @OrderID, " +
                                        "delivery_date = @DataOrder, status = @StatusOrder, updated_by = @Updated " +
                                        "WHERE delivery_id = @DeliveryID;";

            // Открываем соединение с базой данных
            dataBase.openConnection();

            SqlCommand com = new SqlCommand(changeCommandString, dataBase.GetConnection());
            com.Parameters.AddWithValue("@OrderID", textBoxIdOrders2.Text);
            com.Parameters.AddWithValue("@DataOrder", textBoxDateOrder.Text);
            com.Parameters.AddWithValue("@StatusOrder", comboBox4.Text);
            com.Parameters.AddWithValue("@Updated", GlobalData.UserID);
            com.Parameters.AddWithValue("@DeliveryID", delivery_id);

            // Выполняем команду
            com.ExecuteNonQuery();

            // Обновляем DataGridView
            CreateColumns_Deliveries();

            // Очищаем текстовые поля
            textBoxIdOrders2.Text = "";
            textBoxDateOrder.Text = "";
            comboBox4.Text = "";

            // Закрываем соединение с базой данных
            dataBase.closeConnection();

            // Показываем сообщение об успешном изменении записи
            MessageBox.Show("Запись успешно изменена!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonDelPostavki_Click(object sender, EventArgs e)
        {
            // Проверяем наличие значений в текстовых полях перед формированием запроса
            if (string.IsNullOrEmpty(textBoxIdOrders2.Text) || string.IsNullOrEmpty(textBoxDateOrder.Text) || string.IsNullOrEmpty(comboBox4.Text))
            {
                MessageBox.Show("Отсутсвует выбранная запись для удаления!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult qtdel = MessageBox.Show("Вы точно хотите удалить запись?", "Подтверждение удаления записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (qtdel == DialogResult.Yes)
            {
                try
                {
                    string delcommandString = $"DELETE Deliveries where delivery_id = '{delivery_id}'";

                    dataBase.openConnection();

                    SqlCommand com = new SqlCommand(delcommandString, dataBase.GetConnection());
                    SqlDataAdapter adapter = new SqlDataAdapter(com);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds, "applicant");

                    CreateColumns_Deliveries();

                    textBoxIdOrders2.Text = "";
                    textBoxDateOrder.Text = "";
                    comboBox4.Text = "";

                    dataBase.closeConnection();

                    MessageBox.Show("Запись успешно удалена!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                catch
                {
                    MessageBox.Show("Ошибка при удаление выбранной записи", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------      
        private void сохранитьТаблицКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "PDF Files|*.pdf";
            saveFileDialog1.Title = "Сохранить таблицу как PDF";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                DataGridView activeDataGridView = null;
                string nameTable = "";

                // Создаем словарь для соответствия вкладок DataGridView и названий таблиц
                Dictionary<TabPage, (DataGridView, string)> tabPageMappings = new Dictionary<TabPage, (DataGridView, string)>
                {
                    { tabPageUsers, (dataGridViewUsers, "Пользователей") },
                    { tabPageEmployees, (dataGridViewEmployees, "Сотрудников") },
                    { tabPageProducts, (dataGridViewProducts, "Продукции") },
                    { tabPageOrders, (dataGridViewOrders, "Заказов") },
                    { tabPageOrderDetails, (dataGridViewOrderDetails, "Описание заказов") },
                    { tabPageDeliveries, (dataGridViewDeliveries, "Поставок") }
                };

                // Определение активного DataGridView в зависимости от выбранной вкладки
                if (tabPageMappings.ContainsKey(tabControl.SelectedTab))
                {
                    (activeDataGridView, nameTable) = tabPageMappings[tabControl.SelectedTab];
                }
                else
                {
                    // Если вкладка не найдена в словаре (что маловероятно), можно добавить обработку ошибки или другое действие по умолчанию.
                    Console.WriteLine("Выбранная вкладка не поддерживается");
                }


                using (FileStream fs = new FileStream(saveFileDialog1.FileName, FileMode.Create))
                {
                    Document doc = new Document(PageSize.A4, 10, 10, 10, 10);
                    PdfWriter writer = PdfWriter.GetInstance(doc, fs);
                    doc.Open();

                    // Подключение шрифта, поддерживающего русский язык
                    string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "ARIAL.TTF");
                    BaseFont baseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                    iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, 12, iTextSharp.text.Font.NORMAL);

                    // Заголовок
                    Paragraph paragraph = new Paragraph($"Таблица данных '{nameTable}' - {DateTime.Now}", font);
                    paragraph.Alignment = Element.ALIGN_CENTER;
                    doc.Add(paragraph);
                    doc.Add(new Chunk("\n", font)); // Пустая строка

                    // Создание таблицы с нужным количеством столбцов
                    PdfPTable table = new PdfPTable(activeDataGridView.Columns.Count);
                    table.WidthPercentage = 100;

                    // Добавление заголовков столбцов
                    for (int j = 0; j < activeDataGridView.Columns.Count; j++)
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(activeDataGridView.Columns[j].HeaderText, font));
                        cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                        table.AddCell(cell);
                    }

                    // Добавление строк
                    for (int i = 0; i < activeDataGridView.Rows.Count; i++)
                    {
                        for (int j = 0; j < activeDataGridView.Columns.Count; j++)
                        {
                            if (activeDataGridView[j, i].Value != null)
                            {
                                table.AddCell(new Phrase(activeDataGridView[j, i].Value.ToString(), font));
                            }
                            else
                            {
                                table.AddCell(new Phrase(string.Empty, font));
                            }
                        }
                    }

                    doc.Add(table);
                    doc.Close();
                    writer.Close();

                    MessageBox.Show("Таблица успешно сохранена!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        
        private void выйтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataBase.closeConnection();
            this.Close();
        }
    } 
}
