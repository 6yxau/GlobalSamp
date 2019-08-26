using System;
using MySql.Data.MySqlClient;
 
namespace Tutorial.SqlConn
{
    class DBMySQLUtils
    {
 
        public static MySqlConnection
            GetDBConnection(string host, int port, string database, string username, string password)
        {
            // Connection String.
            String connString = "Server=" + "localhost" + ";Database=" + "globalsamp"
                                + ";port=" + "1488" + ";User Id=" + "6yxau" + ";password=" + "12345";
 
            MySqlConnection conn = new MySqlConnection(connString);
 
            return conn;
        }
      
    }
}