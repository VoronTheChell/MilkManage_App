using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkManage_App
{
    internal class DataBase
    {
        public SqlConnection sqlConnection = new SqlConnection(@"Data Source=VORONPC\MSSQLS; Initial Catalog=MilkManageDB; Integrated Security=true");


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
    }

    public static class GlobalData
    {
        public static int UserID { get; set; }
    }
}
