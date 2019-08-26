using System;
using GlobalSamp.Dao;
using MySql.Data.MySqlClient;

namespace GlobalSamp.Player.Dao
{
    public sealed class PlayerDao : AbstractDao
    {
        public bool IsUserRegistered(string playerName)
        {
            MySqlConnection conn = null;
            try
            {
                conn = CreateConnection();
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"SELECT COUNT(*) FROM users WHERE name={playerName}", conn);
                MySqlDataReader rd = cmd.ExecuteReader();
                rd.Read();
                return rd.GetString(1).Equals("0");
            }
            catch (Exception e)
            {
                //TODO: Logging
                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
        }
    }
}