using System;
using System.Threading;
using GlobalSamp.Application.Translator;
using GlobalSamp.Dialog;
using GlobalSamp.Mapping;
using GlobalSamp.Player;
using SampSharp.GameMode;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Display;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.SAMP.Commands;
using SampSharp.GameMode.World;
using SampSharp.Streamer.World;

namespace GlobalSamp
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public sealed class GameMode : BaseMode
    {

        protected override void OnInitialized(EventArgs e)
        {
            try
            {
                MapManager.Instance.ApplyAppenders();
                DisableInteriorEnterExits();
                PickupManager.Instance.Configure();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw;
            }
            Console.WriteLine("Game mode initialized");
        }

        protected override void OnPlayerPickUpPickup(Pickup pickup, PlayerEventArgs e)
        {
            
        }

        protected override void OnPlayerConnected(BasePlayer player, EventArgs e)
        {
            MapManager.Instance.RemoveMappingForPlayer(player);
        }

        protected override void OnPlayerRequestClass(BasePlayer player, RequestClassEventArgs e)
        {
            player.ToggleSpectating(true);
            player.SendClientMessage(Translator.Instance.GetMessage("enter") + player.Name);
            PlayerData data = PlayerManager.Instance.GetPlayerData(player.Name);
            RegistrationDialog dialog = new RegistrationDialog(data != null);
            dialog.Response += dialog.OnInputRegistrationData;
            dialog.ShowAsync(player);
        }

        protected override void OnPlayerRequestSpawn(BasePlayer player, RequestSpawnEventArgs e)
        {
            e.PreventSpawning = true;
            player.SendClientMessage(Translator.Instance.GetMessage("if_spawn_click"));
        }
        

        protected override void OnPlayerSpawned(BasePlayer player, SpawnEventArgs e)
        {
            PlayerData data = PlayerManager.Instance.GetPlayerData(player.Name);
            if (!data.Authorized)
            {
                player.Kick();
            }
            
            player.Skin = 181;
        }

        protected override void OnPlayerDisconnected(BasePlayer player, DisconnectEventArgs e)
        {
            PlayerManager.Instance.RemovePlayerData(player.Name);
        }
        
        protected override void OnPlayerClickMap(BasePlayer player, PositionEventArgs e)
        {
            player.SetPositionFindZ(e.Position);
        }
    }
}