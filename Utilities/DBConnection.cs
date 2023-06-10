using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Backuper.Utilities
{
    public class DBConnection
    {
        static string connectionString = @"Data Source=ATOM\SQLEXPRESS;Initial Catalog=StockDB;Integrated Security=true;Persist Security Info=false;";
        public static SqlConnection GetConnection()
        {
            SqlConnection cn = null;
            try
            {
                cn = new SqlConnection(connectionString);
            }
            catch (Exception ex)
            {
                throw new MyException(ex, "Database Connection Error", ex.Message, "Connection");
            }
            return cn;
        }
    }
}
