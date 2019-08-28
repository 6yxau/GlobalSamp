using System;
using GlobalSamp.Dao;
using MySql.Data.MySqlClient;

namespace GlobalSamp.Player.Dao
{
    public sealed class PlayerDao : AbstractDao
    {
        public PlayerData GetPlayerModel(string name)
        {
            MySqlConnection conn = null;
            try
            {
                conn = CreateConnection();
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"SELECT * FROM users WHERE name='{name}'", conn);
                MySqlDataReader rd = cmd.ExecuteReader();
                if (!rd.HasRows)
                {
                    return null;
                }
                rd.Read();
                var result = new PlayerData
                {
                    Id = rd.GetInt32(0),
                    UserName = rd.GetString(1),
                    Gender = rd.GetBoolean(2) ? PlayerGender.FEMALE : PlayerGender.MALE,
                    Password = rd.GetString(3),
                    Email = rd.GetString(4),
                    date = rd.GetInt64(5)
                };
                return result;
            }
            catch (Exception e)
            {
                //TODO: Logging
                throw new ApplicationException(e.Message);
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