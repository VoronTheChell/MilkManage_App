using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MilkManage_App.Forms
{
    public partial class loaderForm : Form
    {
        DataBase dataBase = new DataBase();

        int selectdRow;

        private int product_id, order_id, 
                    order_detail_id, delivery_id;

        enum RowState
        {
            Existed,
            New,
            Modified,
            ModifiedNew,
            Deleted
        }

        public loaderForm()
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterScreen;
            MaximizeBox = false;

            CreateColumns_Products();
            CreateColumns_Orders();
            CreateColumns_OrderDetails();
            CreateColumns_Deliveries();
        }

        // Отображение и обновление таблиц
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
					                          status as 'Статус поставки:' from Deliveries", dataBase.sqlConnection);

            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "Deliveries");
            dataGridViewDeliveries.DataSource = ds.Tables[0];
            dataBase.closeConnection();
            dataGridViewDeliveries.AutoResizeColumns();
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

                // Определение активного DataGridView в зависимости от выбранной вкладки
                if (tabControl.SelectedTab == tabPageProducts)
                {
                    activeDataGridView = dataGridViewProducts;
                    nameTable = "Продукции";
                }

                else if (tabControl.SelectedTab == tabPageOrders)
                {
                    activeDataGridView = dataGridViewOrders;
                    nameTable = "Заказов";
                }

                else if (tabControl.SelectedTab == tabPageOrderDetails)
                {
                    activeDataGridView = dataGridViewOrderDetails;
                    nameTable = "Описание заказов";
                }

                else if (tabControl.SelectedTab == tabPageDeliveries)
                {
                    activeDataGridView = dataGridViewDeliveries;
                    nameTable = "Поставок";
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
