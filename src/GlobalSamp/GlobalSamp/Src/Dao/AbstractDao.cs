using MySql.Data.MySqlClient;

namespace GlobalSamp.Dao
{
    public abstract class AbstractDao
    {
        protected MySqlConnection CreateConnection()
        {
            string host = $"localhost";
            int port = 1488;
            string database = $"globalsamp";
            string username = $"6yxau";
            string password = $"12345";
 
            return GetDBConnection(host, port, database, username, password);
        }
        
        private MySqlConnection GetDBConnection(string host, int port, string database, string username, string password)
        {
            string connString = $"Server=" + $"localhost" + $";Database=" + $"globalsamp"
                                + $";port=" + $"1488" + $";User Id=" + $"6yxau" + $";password=" + $"12345";
 
            MySqlConnection conn = new MySqlConnection(connString);
 
            return conn;
        }

    }
}