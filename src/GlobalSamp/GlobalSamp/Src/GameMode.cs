using System;
using GlobalSamp.Player;
using SampSharp.GameMode;
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


            var inputDialog = PlayerManager.Instance.GetConnectionDialogForPlayer(player);
                // inputDialog.Response += ConnectionDialogResponse;
            inputDialog.Show(player);
        }

        private void ConnectionDialogResponse(object sender, DialogResponseEventArgs e)
        {
            
        }

        protected override void OnDialogResponse(BasePlayer player, DialogResponseEventArgs e)
        {
            base.OnDialogResponse(player, e);
        }
    }
}