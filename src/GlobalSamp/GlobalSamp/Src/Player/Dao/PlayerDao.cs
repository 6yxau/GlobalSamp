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
            PlayerData result = new PlayerData();
            try
            {
                conn = CreateConnection();
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"SELECT * FROM users WHERE name='{name}'", conn);
                MySqlDataReader rd = cmd.ExecuteReader();
                if (!rd.HasRows)
                {
                    return default(PlayerData);
                }
                rd.Read();
                result.Id = rd.GetInt32(0);
                result.UserName = rd.GetString(1);
                result.Gender = rd.GetBoolean(2) ? PlayerGender.FEMALE : PlayerGender.MALE;
                result.Password = rd.GetString(3);
                result.Email = rd.GetString(4);
                result.date = rd.GetInt64(5);
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