using System;
using GlobalSamp.Player;
using SampSharp.GameMode;
using SampSharp.GameMode.Display;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.World;

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

            PlayerData data = PlayerManager.Instance.GetPlayerData(player.Name);
            
            string caption;
            string message;
            if (data.Equals(default(PlayerData)))
            {
                caption = "Регистрация";
                message = "Ваш логин: {ff0000}не зарегистрирован на сервере.\n Для регистрации введите пароль.";
            }
            else
            {
                caption = "Вход";
                message = "Ваш логин: {18ff00}зарегистрирован на сервере.\n Для входа введите пароль.";
            }
            
            var dialog = new InputDialog(caption, message, true, "Далее");
            
            dialog.Response += ConnectionDialogResponse;
            dialog.Show(player);
        }

        private void ConnectionDialogResponse(object sender, DialogResponseEventArgs e)
        {
            PlayerData data = PlayerManager.Instance.GetPlayerData(e.Player.Name);
            if (data.Password == e.InputText)
            {
                
            }
            else
            {
                
            }
        }

        protected override void OnDialogResponse(BasePlayer player, DialogResponseEventArgs e)
        {
            base.OnDialogResponse(player, e);
        }
    }
}