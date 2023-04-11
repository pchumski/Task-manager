using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenadżerZadań
{
    public class Database
    {
        //private string connectionData;
        SqlConnection conn;
        public Database()
        {
            string path = Directory.GetCurrentDirectory();
            path = path.Substring(0, path.IndexOf("bin"));
            string connectionData = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + path + @"Database1.mdf;Integrated Security=True";
            conn = new SqlConnection(connectionData);
        }

       
        public SqlCommand OpenConnection(string sql)
        {
            conn.Open();
            SqlCommand command = new SqlCommand(sql, conn);
            return command;
        }

        public SqlCommand GetCommand(string sql)
        {
            SqlCommand command = new SqlCommand(sql, conn);
            return command;
        }

        public void CloseConnection()
        {
            conn.Close();
        }
    }
}
