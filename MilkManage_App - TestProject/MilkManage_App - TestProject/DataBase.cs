﻿using System;
using System.Data.SqlClient;

namespace MilkManage_App___TestProject
{
    public class DataBase : IDisposable
    {
        public SqlConnection sqlConnection = new SqlConnection(@"Data Source=VORONPC\MSSQLS; Initial Catalog=MilkManageDB; Integrated Security=true");

        public int ExecuteNonQuery(string sql)
        {
            using (var command = new SqlCommand(sql, sqlConnection))
            {
                openConnection();
                int rowsAffected = command.ExecuteNonQuery();
                closeConnection();
                return rowsAffected;
            }
        }

        public object ExecuteScalar(string sql)
        {
            try
            {
                using (var command = new SqlCommand(sql, sqlConnection))
                {
                    openConnection();
                    object result = command.ExecuteScalar();
                    closeConnection();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error executing SQL query: " + ex.Message);
                return null; // Возвращаем null, чтобы показать, что произошла ошибка
            }
        }

        public void openConnection()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
        }

        public void closeConnection()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }

        public SqlConnection GetConnection()
        {
            return sqlConnection;
        }

        public void Dispose()
        {
            sqlConnection.Dispose();
        }
    }
}
