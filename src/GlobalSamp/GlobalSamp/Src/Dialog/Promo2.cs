using GlobalSamp.Player;
using SampSharp.GameMode;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Display;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.World;

namespace GlobalSamp.Dialog
{
    public class Promo2 : InputDialog
    {
        public Promo2() : base(
            "Промокод",
            "Введите промокод \n" +
            "{ffffff}С помощью промокода {18ff00}вы получите преимущества {ffffff}на старте игры",
            false,
            "Ввести",
            "Пропустить")
        {
        }

        public void OnPromo2ChangedResponse(object sender, DialogResponseEventArgs e)
        {
            PlayerData data = PlayerManager.Instance.GetCachedPlayerData(e.Player.Name);
            if (data == null)
            {
                Response -= OnPromo2ChangedResponse;
                return;
                // TODO: Logging
            }
            if (e.DialogButton != DialogButton.Left)
            {
                ForcePlayerSpecifySkin(e.Player);
                Response -= OnPromo2ChangedResponse;
                return;
            }
            ForcePlayerSpecifySkin(e.Player);
            Response -= OnPromo2ChangedResponse;
        }

        private void ForcePlayerSpecifySkin(BasePlayer player)
        {
            player.SetSpawnInfo(1, 134, SpawnPosition.DEFAULT, 0f);
            player.VirtualWorld = 1;
            player.Spawn();
            player.Position = new Vector3(2175.77f, 1869.15f, 22.20f);
            player.Rotation = new Vector3(player.Rotation.X, player.Rotation.Y, -86);
            player.ToggleControllable(false);
            player.CameraPosition = new Vector3(2179.102f, 1867.9f, 22.9398f);
            player.SetCameraLookAt(new Vector3(2175.77f, 1869.15f, 22.20f), CameraCut.Cut);
            
            SelectSkinDialog dialog = new SelectSkinDialog();
            dialog.Response += dialog.OnSelectSkinResponse;
            dialog.Show(player);
        }
    }
}