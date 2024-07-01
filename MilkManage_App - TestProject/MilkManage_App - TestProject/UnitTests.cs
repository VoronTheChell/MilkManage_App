using MilkManage_App___TestProject;
using System.Data.SqlClient;
using System.Data;
using Xunit;

namespace MilkManage_App___TestProject
{
    [CollectionDefinition("SequentialTests")]
    public class SequentialTestsCollection : ICollectionFixture<DatabaseFixture>
    {
        // Ёта коллекци€ не требует никакой дополнительной логики
    }

    public class DatabaseFixture : IDisposable
    {
        public DataBase Db { get; private set; }

        public DatabaseFixture()
        {
            // »нициализаци€ общих ресурсов
            Db = new DataBase();
        }

        public void Dispose()
        {
            // ќсвобождение ресурсов
            Db.Dispose();
        }
    }

    [Collection("SequentialTests")]
    public class DbConnections_UnitTest
    {
        private readonly DatabaseFixture _fixture;

        public DbConnections_UnitTest(DatabaseFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Test1_Open_Connection()
        {
            // Arrange
            var db = _fixture.Db;

            // Act
            db.openConnection();

            // Assert
            Assert.Equal(ConnectionState.Open, db.sqlConnection.State);
        }

        [Fact]
        public void Test3_Close_Connection()
        {
            // Arrange
            var db = _fixture.Db;
            db.openConnection(); // Open connection first

            // Act
            db.closeConnection();

            // Assert
            Assert.Equal(ConnectionState.Closed, db.sqlConnection.State);
        }

        [Fact]
        public void Test2_Get_Connection()
        {
            // Arrange
            var db = _fixture.Db;

            // Act
            var connection = db.GetConnection();

            // Assert
            Assert.NotNull(connection);
            Assert.IsType<SqlConnection>(connection);
        }
    }

    [Collection("SequentialTests")]
    public class reg_Db_UnitTest
    {
        private readonly DatabaseFixture _fixture;

        public reg_Db_UnitTest(DatabaseFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Test1_Insert_Data()
        {
            // Arrange
            var db = _fixture.Db;
            string sql = "INSERT INTO Users (username, password, role) VALUES ('HF', 'passTest', 'test_user')";

            // Act
            int rowsAffected = db.ExecuteNonQuery(sql);

            // Assert
            Assert.Equal(1, rowsAffected); // ѕровер€ем, что одна строка была вставлена
        }

        [Fact]
        public void Test2_Delete_Data()
        {
            // Arrange
            var db = _fixture.Db;
            string sql = "DELETE FROM Users WHERE role = 'test_user';";

            // Act
            int rowsAffected = db.ExecuteNonQuery(sql);

            // Assert
            Assert.True(rowsAffected >= 1); // ѕровер€ем, что хот€ бы одна строка была удалена
        }
    }

    [Collection("SequentialTests")]
    public class employer_Db_UnitTest
    {
        private readonly DatabaseFixture _fixture;

        public employer_Db_UnitTest(DatabaseFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Test1_Insert_Data()
        {
            // Arrange
            var db = _fixture.Db;
            string sql = "INSERT INTO Employees (name, position, start_date) VALUES ('John Doe', 'Manager', '2023-01-15')";

            // Act
            int rowsAffected = db.ExecuteNonQuery(sql);

            // Assert
            Assert.Equal(1, rowsAffected); // ѕровер€ем, что одна строка была вставлена
        }

        [Fact]
        public void Test2_Delete_Data()
        {
            // Arrange
            var db = _fixture.Db;
            string sql = "DELETE FROM Employees WHERE name = 'John Doe';";

            // Act
            int rowsAffected = db.ExecuteNonQuery(sql);

            // Assert
            Assert.True(rowsAffected >= 1); // ѕровер€ем, что хот€ бы одна строка была удалена
        }
    }

    [Collection("SequentialTests")]
    public class product_Db_UnitTest
    {
        private readonly DatabaseFixture _fixture;

        public product_Db_UnitTest(DatabaseFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Test1_Insert_Data()
        {
            // Arrange
            var db = _fixture.Db;
            string sql = "INSERT INTO Products (name, description, price) VALUES ('Milk', 'Fresh milk', 1.50)";

            // Act
            int rowsAffected = db.ExecuteNonQuery(sql);

            // Assert
            Assert.Equal(1, rowsAffected); // ѕровер€ем, что одна строка была вставлена
        }

        [Fact]
        public void Test2_Delete_Data()
        {
            // Arrange
            var db = _fixture.Db;
            string sql = "DELETE FROM Products WHERE name = 'Milk';";

            // Act
            int rowsAffected = db.ExecuteNonQuery(sql);

            // Assert
            Assert.True(rowsAffected >= 1); // ѕровер€ем, что хот€ бы одна строка была удалена
        }
    }

    [Collection("SequentialTests")]
    public class orders_Db_UnitTest
    {
        private readonly DatabaseFixture _fixture;

        public orders_Db_UnitTest(DatabaseFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Test1_Insert_Data()
        {
            // Arrange
            var db = _fixture.Db;
            string sql = "INSERT INTO Orders (customer_name, order_date, status, total_amount) VALUES ('Customer D', '2023-05-01', 'in production', 150.00)";

            // Act
            int rowsAffected = db.ExecuteNonQuery(sql);

            // Assert
            Assert.Equal(1, rowsAffected); // ѕровер€ем, что одна строка была вставлена
        }

        [Fact]
        public void Test2_Delete_Data()
        {
            // Arrange
            var db = _fixture.Db;
            string sql = "DELETE FROM Orders WHERE customer_name = 'Customer D';";

            // Act
            int rowsAffected = db.ExecuteNonQuery(sql);

            // Assert
            Assert.True(rowsAffected >= 1); // ѕровер€ем, что хот€ бы одна строка была удалена
        }
    }

    [Collection("SequentialTests")]
    public class OrderDetails_Db_UnitTest
    {
        private readonly DatabaseFixture _fixture;

        public OrderDetails_Db_UnitTest(DatabaseFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Test1_Insert_Data()
        {
            // Arrange
            var db = _fixture.Db;
            string sql = "INSERT INTO OrderDetails (order_id, product_id, quantity) VALUES (1, 3, 100)";

            // Act
            int rowsAffected = db.ExecuteNonQuery(sql);

            // Assert
            Assert.Equal(1, rowsAffected); // ѕровер€ем, что одна строка была вставлена
        }

        [Fact]
        public void Test2_Delete_Data()
        {
            // Arrange
            var db = _fixture.Db;
            string sql = "DELETE FROM OrderDetails WHERE order_id = 1 AND product_id = 3;";

            // Act
            int rowsAffected = db.ExecuteNonQuery(sql);

            // Assert
            Assert.True(rowsAffected >= 1); // ѕровер€ем, что хот€ бы одна строка была удалена
        }
    }

    [Collection("SequentialTests")]
    public class Deliveries_Db_UnitTest
    {
        private readonly DatabaseFixture _fixture;

        public Deliveries_Db_UnitTest(DatabaseFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Test1_Insert_Data()
        {
            // Arrange
            var db = _fixture.Db;
            string sql = "INSERT INTO Deliveries (order_id, delivery_date, status, updated_by) VALUES (2, '2023-05-05', 'in transit', 3)";

            // Act
            int rowsAffected = db.ExecuteNonQuery(sql);

            // Assert
            Assert.Equal(1, rowsAffected); // ѕровер€ем, что одна строка была вставлена
        }

        [Fact]
        public void Test2_Delete_Data()
        {
            // Arrange
            var db = _fixture.Db;
            string sql = "DELETE FROM Deliveries WHERE order_id = 2 AND delivery_date = '2023-05-05';";

            // Act
            int rowsAffected = db.ExecuteNonQuery(sql);

            // Assert
            Assert.True(rowsAffected >= 1); // ѕровер€ем, что хот€ бы одна строка была удалена
        }
    }
}
