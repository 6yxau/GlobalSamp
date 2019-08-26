using MySql.Data.MySqlClient;

namespace GlobalSamp.Dao
{
    public abstract class AbstractDao
    {
        protected MySqlConnection CreateConnection()
        {
            string host = "localhost";
            int port = 1488;
            string database = "globalsamp";
            string username = "root";
            string password = "12345";
 
            return GetDBConnection(host, port, database, username, password);
        }
        
        private MySqlConnection GetDBConnection(string host, int port, string database, string username, string password)
        {
            string connString =
                $"server={host};database={database};port={port.ToString()};uid={username};pwd={password}";

            MySqlConnection conn = new MySqlConnection(connString);
 
            return conn;
        }

    }
}