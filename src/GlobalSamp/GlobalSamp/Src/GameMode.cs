using System;
using GlobalSamp.Dialog;
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

            PlayerData data = PlayerManager.Instance.GetPlayerData(player.Name);

            var dialog = new RegistrationDialog(data != null);
            
            dialog.Response += dialog.OnInputRegistrationData;
            dialog.ShowAsync(player);
        }

        protected override void OnPlayerSpawned(BasePlayer player, SpawnEventArgs e)
        {
            base.OnPlayerSpawned(player, e);

            PlayerData data = PlayerManager.Instance.GetPlayerData(player.Name);
            if (!data.Authorized)
            {
                player.Kick();
            }
        }

        protected override void OnPlayerDisconnected(BasePlayer player, DisconnectEventArgs e)
        {
            base.OnPlayerDisconnected(player, e);
            PlayerManager.Instance.RemovePlayerData(player.Name);
        }
    }
}