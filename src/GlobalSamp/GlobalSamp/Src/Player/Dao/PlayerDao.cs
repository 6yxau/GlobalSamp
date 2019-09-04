using System;
using GlobalSamp.Dao;
using MySql.Data.MySqlClient;

namespace GlobalSamp.Player.Dao
{
    public sealed class PlayerDao : AbstractDao
    {
        public PlayerData GetPlayerModel(string name)
        {
            using (MySqlConnection conn = CreateConnection())
            {
                try
                {
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
                        date = rd.GetInt64(5),
                        SkinColor = rd.GetBoolean(6) ? PlayerColor.BLACK : PlayerColor.WHITE,
                        Age = rd.GetInt16(7)
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
                    conn.Close();
                }
            }
            
        }

        public void AddPlayer(PlayerData player)
        {
            MySqlConnection conn = null;
            try
            {
                conn = CreateConnection();
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO users ('id', 'name', 'gender', 'password', 'email', 'date', 'SkinColor') VALUES " + 
                                                    $"('','', '+ Gender +', '+ Password +', '+ Email +', '+ date +', '+ SkinColor +', '+ Age +'",
                        conn);
                cmd.ExecuteNonQuery();
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