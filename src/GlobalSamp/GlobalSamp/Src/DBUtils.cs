using MySql.Data.MySqlClient;
 
namespace Tutorial.SqlConn
{
    class DBUtils
    {
        public static MySqlConnection GetDBConnection()
        {
            string host = "localhost";
            int port = 1488;
            string database = "globalsamp";
            string username = "6yxau";
            string password = "12345";
 
            return DBMySQLUtils.GetDBConnection(host, port, database, username, password);
        }
      
    }
}