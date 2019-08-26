using System;
using MySql.Data.MySqlClient;
using SampSharp.GameMode;
using SampSharp.GameMode.Display;
using SampSharp.GameMode.World;
using Tutorial.SqlConn;

namespace GlobalSamp
{
    public class GameMode : BaseMode
    {

        protected override void OnInitialized(EventArgs e)
        {
            Console.WriteLine("\n----------------------------------");
            Console.WriteLine("      Welcome  to  GlobalSamp       ");
            Console.WriteLine("----------------------------------\n");
            
            base.OnInitialized(e);
        }
        
        protected override void OnPlayerConnected(BasePlayer player, EventArgs e)
        {
            base.OnPlayerConnected(player, e);
            
            player.SendClientMessage($"Добро пожаловать в GlobalSamp, {player.Name}!");
            
            MySqlConnection connection = DBUtils.GetDBConnection();
            connection.Open();
            try
            {
                string queryString = "SELECT COUNT(*) FROM users WHERE name='{player.Name}'";
                if (queryString == "0")
                {
                    var loginDialog = new InputDialog("Регистрация", "Пожалуйста, зарегистрируйтесь!", true, "Регистрация", "Выход");
                    loginDialog.Show(player);
                }
                else
                {
                    var loginDialog = new InputDialog("Вход", "Введите ваш пароль. Если Вы ещё не играли на нашем сервере, то смените ник.", true, "Вход", "Выход");
                    loginDialog.Show(player);
                }

            }
            finally
            {
                connection.Close(); 
                connection.Dispose();
                connection = null;
            }
            
        }

    }
}