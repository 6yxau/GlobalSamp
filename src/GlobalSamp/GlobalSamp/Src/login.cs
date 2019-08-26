using System;
using SampSharp.GameMode;
using SampSharp.GameMode.Display;
using SampSharp.GameMode.World;

namespace GlobalSamp
{
    public class login : BaseMode
    {
        protected override void OnPlayerConnected(BasePlayer player, EventArgs e)
        {
            base.OnPlayerConnected(player, e);

            player.SendClientMessage($"Добро пожаловать в GlobalSamp, {player.Name}!");
            
            var loginDialog = new InputDialog("Log In", "Please log in!", true, "Button1", "Button2");
            loginDialog.Show(player);
        }
    }
}